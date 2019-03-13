USE [Sandbox]
GO

/****** Object:  StoredProcedure [dbo].[spUpdateUserUnits]    Script Date: 3/13/2019 4:03:34 PM ******/
DROP PROCEDURE [dbo].[spUpdateUserUnits]
GO

/****** Object:  StoredProcedure [dbo].[spUpdateUserUnits]    Script Date: 3/13/2019 4:03:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateUserUnits] 
	-- Add the parameters for the stored procedure here
	@USERID INT,
	@ENTITYID INT,
	@UNITS DECIMAL(18,0)
AS
BEGIN
	SET NOCOUNT ON;
			
	--INSERT
	IF NOT EXISTS (SELECT TOP 1 Id FROM UNITS WHERE UserId = @USERID AND EntityId = @ENTITYID) BEGIN
		INSERT INTO UNITS (UserId, EntityId, Units) VALUES
		(@USERID, @ENTITYID, @UNITS)
	END

	--UPDATE
	ELSE BEGIN
		UPDATE UNITS SET
		Units = @UNITS
		WHERE UserId = @USERID
		AND EntityId = @ENTITYID
	END
END




GO

