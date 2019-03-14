USE [FinaroDB]
GO

/****** Object:  StoredProcedure [dbo].[spSelectMyOrders]    Script Date: 3/14/2019 4:59:02 PM ******/
DROP PROCEDURE [dbo].[spSelectMyOrders]
GO

/****** Object:  StoredProcedure [dbo].[spSelectMyOrders]    Script Date: 3/14/2019 4:59:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[spSelectMyOrders]
-- Add the parameters for the stored procedure here
@USERID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT ORDERS.[Id]
		  ,[OrderId]
		  ,[UserId]
		  ,UPPER(E.Name)[Name]
		  ,[EntityId]
		  ,[TradeTypeId]
		  ,[Price]
		  ,[Date]
		  ,Quantity
		  ,UnsetQuantity
		  ,[Status]
		  ,[TxHash]
	  FROM ORDERS
	  LEFT JOIN ENTITIES E ON E.ID = ORDERS.EntityId
	  WHERE [UserId] = @USERID

	END







GO

