GO

/****** Object:  StoredProcedure [dbo].[spSelectOrdersForMatch]    Script Date: 2/5/2019 4:48:51 PM ******/
DROP PROCEDURE [dbo].[spSelectOrdersForMatch]
GO

/****** Object:  StoredProcedure [dbo].[spSelectOrders]    Script Date: 2/5/2019 4:48:51 PM ******/
DROP PROCEDURE [dbo].[spSelectOrders]
GO

/****** Object:  StoredProcedure [dbo].[spSelectOrders]    Script Date: 2/5/2019 4:48:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spSelectOrders]
	-- Add the parameters for the stored procedure here
	@ENTITYID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET ARITHABORT ON;


    SELECT * FROM ORDERS 
	WHERE EntityId = @ENTITYID 
	AND [Status] < 3
	ORDER BY TradeTypeId DESC, Price DESC, [DATE] 

END

GO

/****** Object:  StoredProcedure [dbo].[spSelectOrdersForMatch]    Script Date: 2/5/2019 4:48:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spSelectOrdersForMatch]
	-- Add the parameters for the stored procedure here
	@ENTITYID INT,
	@TRADETYPEID INT,
	@PRICE DECIMAL(18,10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET ARITHABORT ON;

	IF(@TRADETYPEID = 1) BEGIN
		--I'M BUYING, SHOW ME THE LOWEST PRICE SALES
		SELECT * FROM ORDERS 
		WHERE EntityId = @ENTITYID 
		AND TradeTypeId = 2 
		AND [Status] < 3
		AND Price <= @PRICE
		ORDER BY PRICE ASC, [Date]
	END
	ELSE IF (@TRADETYPEID = 2) BEGIN
		--I'M SELLING, SHOW ME THE HIGHEST PRICE BUYS
		SELECT * FROM ORDERS 
		WHERE EntityId = @ENTITYID 
		AND TradeTypeId = 1 
		AND [Status] < 3
		AND Price >= @PRICE
		ORDER BY PRICE DESC, [Date]
	END

END

GO

