USE [FinaroDB]
GO

/****** Object:  StoredProcedure [dbo].[spUpdateUserUnits]    Script Date: 03/13/2019 21:14:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spUpdateUserUnits]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spUpdateUserUnits]
GO

USE [FinaroDB]
GO

/****** Object:  StoredProcedure [dbo].[spUpdateUserUnits]    Script Date: 03/13/2019 21:14:04 ******/
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
    -- Insert statements for procedure here
		
	--NEW ENTRY
	IF NOT EXISTS (SELECT TOP 1 Id FROM UNITS WHERE UserId = @USERID AND EntityId = @ENTITYID) BEGIN
		INSERT INTO UNITS (UserId, EntityId, Units) VALUES
		(@USERID, @ENTITYID, @UNITS)
	END

	--UPDATE
	ELSE BEGIN
		UPDATE UNITS SET
		Units = Units + @UNITS
		WHERE UserId = @USERID
		AND EntityId = @ENTITYID
	END

END


GO

