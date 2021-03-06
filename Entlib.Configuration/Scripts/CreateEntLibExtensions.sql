IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'EntLibExtensions')
	DROP DATABASE [EntLibExtensions]
GO

CREATE DATABASE [EntLibExtensions]
 COLLATE SQL_Latin1_General_CP1_CI_AS
GO

exec sp_dboption N'EntLibExtensions', N'autoclose', N'false'
GO

exec sp_dboption N'EntLibExtensions', N'bulkcopy', N'false'
GO

exec sp_dboption N'EntLibExtensions', N'trunc. log', N'false'
GO

exec sp_dboption N'EntLibExtensions', N'torn page detection', N'true'
GO

exec sp_dboption N'EntLibExtensions', N'read only', N'false'
GO

exec sp_dboption N'EntLibExtensions', N'dbo use', N'false'
GO

exec sp_dboption N'EntLibExtensions', N'single', N'false'
GO

exec sp_dboption N'EntLibExtensions', N'autoshrink', N'false'
GO

exec sp_dboption N'EntLibExtensions', N'ANSI null default', N'false'
GO

exec sp_dboption N'EntLibExtensions', N'recursive triggers', N'false'
GO

exec sp_dboption N'EntLibExtensions', N'ANSI nulls', N'false'
GO

exec sp_dboption N'EntLibExtensions', N'concat null yields null', N'false'
GO

exec sp_dboption N'EntLibExtensions', N'cursor close on commit', N'false'
GO

exec sp_dboption N'EntLibExtensions', N'default to local cursor', N'false'
GO

exec sp_dboption N'EntLibExtensions', N'quoted identifier', N'false'
GO

exec sp_dboption N'EntLibExtensions', N'ANSI warnings', N'false'
GO

exec sp_dboption N'EntLibExtensions', N'auto create statistics', N'true'
GO

exec sp_dboption N'EntLibExtensions', N'auto update statistics', N'true'
GO

if( (@@microsoftversion / power(2, 24) = 8) and (@@microsoftversion & 0xffff >= 724) )
	exec sp_dboption N'EntLibExtensions', N'db chaining', N'false'
GO

use [EntLibExtensions]

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[EntLib_GetConfig]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[EntLib_GetConfig]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[EntLib_SetConfig]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[EntLib_SetConfig]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Configuration_Parameter]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Configuration_Parameter]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[configparam_insupd]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[configparam_insupd]
GO

CREATE TABLE [dbo].[Configuration_Parameter] (
	[section_name] [varchar] (50) NOT NULL ,
	[section_value] [ntext] NOT NULL ,
	[lastmoddate] [datetime] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Configuration_parameter] WITH NOCHECK ADD 
	CONSTRAINT [PK_Configuration_parameter] PRIMARY KEY  CLUSTERED 
	(
		[section_name]
	)  ON [PRIMARY] 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].[EntLib_GetConfig] 
 @sectionName varchar(50) 
AS
SELECT section_value, lastmoddate
FROM Configuration_parameter 
WHERE section_name = @sectionName
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].[EntLib_SetConfig] 
(
	@section_name varchar(50),
	@section_value ntext
)
AS
IF( ( SELECT COUNT(*) FROM Configuration_parameter WHERE section_name = @section_name ) = 1 )
BEGIN
	UPDATE Configuration_parameter SET section_value = @section_value
	WHERE section_name = @section_name 
END
ELSE
BEGIN
	DECLARE @thisdate DATETIME
	SELECT @thisdate = GETDATE()
	INSERT INTO Configuration_parameter ( section_name, section_value, lastmoddate) 
	VALUES ( @section_name, @section_value, @thisdate)
END
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

CREATE TRIGGER configparam_insupd ON [dbo].[Configuration_Parameter] 
FOR UPDATE
AS
   IF (COLUMNS_UPDATED() & 2)  > 0
  BEGIN
	UPDATE Configuration_Parameter 
	SET lastmoddate = GETDATE()
	WHERE section_name = (SELECT ins.section_name FROM inserted ins)
  END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

delete from Configuration_parameter 

GO

