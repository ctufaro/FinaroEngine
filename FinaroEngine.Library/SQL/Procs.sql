

/****** Object:  StoredProcedure [dbo].[spSelectOrdersForMatch]    Script Date: 2/4/2019 12:42:47 PM ******/
DROP PROCEDURE [dbo].[spSelectOrdersForMatch]
GO

/****** Object:  StoredProcedure [dbo].[spSelectOrders]    Script Date: 2/4/2019 12:42:47 PM ******/
DROP PROCEDURE [dbo].[spSelectOrders]
GO

/****** Object:  StoredProcedure [dbo].[spSelectOrders]    Script Date: 2/4/2019 12:42:47 PM ******/
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

END

GO

/****** Object:  StoredProcedure [dbo].[spSelectOrdersForMatch]    Script Date: 2/4/2019 12:42:47 PM ******/
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
	@PRICE DECIMAL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET ARITHABORT ON;

	DECLARE @STRADETYPE INT
	IF(@TRADETYPEID = 1) BEGIN
		SET @STRADETYPE = 2
	END
	ELSE IF (@TRADETYPEID = 2) BEGIN
		SET @STRADETYPE = 1
	END

    SELECT * FROM ORDERS 
	WHERE EntityId = @ENTITYID 
	AND TradeTypeId = @STRADETYPE 
	AND [Status] < 3
	AND 
	((@TRADETYPEID = 1 AND Price <= @PRICE) 
    OR 
    (@TRADETYPEID = 2 AND Price >= @PRICE))
	ORDER BY 
	CASE WHEN @TRADETYPEID = 1 THEN Price END ASC,
	CASE WHEN @TRADETYPEID = 2 THEN Price END DESC, [Date];

END

GO

