USE [Sandbox]
GO

/****** Object:  UserDefinedFunction [dbo].[Flip]    Script Date: 2/8/2019 4:37:42 PM ******/
DROP FUNCTION [dbo].[Flip]
GO

/****** Object:  UserDefinedFunction [dbo].[Flip]    Script Date: 2/8/2019 4:37:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Flip]
(
	-- Add the parameters for the function here
	@TRADETYPEID INT,
	@PRICE DECIMAL(18,10)
)
RETURNS DECIMAL(18,10)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @RETPRICE DECIMAL(18,10)

	-- Add the T-SQL statements to compute the return value here
	IF @TRADETYPEID = 1 BEGIN
		SET @RETPRICE = @PRICE * -1
	END
	ELSE BEGIN
		SET @RETPRICE = @PRICE
	END

	-- Return the result of the function
	RETURN @RETPRICE

END

GO

