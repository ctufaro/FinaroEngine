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


/****** Object:  StoredProcedure [dbo].[spSelectMyBalance]    Script Date: 3/15/2019 1:01:45 PM ******/
DROP PROCEDURE [dbo].[spSelectMyBalance]
GO

/****** Object:  StoredProcedure [dbo].[spSelectMyBalance]    Script Date: 3/15/2019 1:01:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spSelectMyBalance]
-- Add the parameters for the stored procedure here
@USERID INT
AS
BEGIN

	SET NOCOUNT ON;

	SELECT ISNULL(SUM([UNITS] * [MARKETPRICE]),0) AS UserBalance FROM UNITS U
	LEFT JOIN MARKET_DATA M ON U.EntityId = M.EntityId
	WHERE U.UserId = @USERID

	END


/****** Object:  StoredProcedure [dbo].[spSelectMyUnits]    Script Date: 3/15/2019 1:17:28 PM ******/
DROP PROCEDURE [dbo].[spSelectMyUnits]
GO

/****** Object:  StoredProcedure [dbo].[spSelectMyUnits]    Script Date: 3/15/2019 1:17:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spSelectMyUnits]
-- Add the parameters for the stored procedure here
@USERID INT,
@ENTITYID INT
AS
BEGIN

	SET NOCOUNT ON;

	SELECT TOP 1 Units FROM UNITS WHERE UserId = @USERID AND EntityId = @ENTITYID

	END

GO

