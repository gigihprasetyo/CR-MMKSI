USE [BSIDNET_MMKSI_CR_IR]
GO

/****** Object:  StoredProcedure [dbo].[up_DeleteKaroseri]    Script Date: 02/08/2018 15:33:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, February 28, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

create PROCEDURE [dbo].[up_DeleteKaroseri]
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[Karoseri]
WHERE
	[ID] = @ID


GO




/****** Object:  StoredProcedure [dbo].[up_InsertKaroseri]    Script Date: 02/08/2018 15:33:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, February 28, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

create PROCEDURE [dbo].[up_InsertKaroseri]
	@ID int OUTPUT,
	@Code varchar(16),
	@Name varchar(50),
	@City varchar(50),
	@Alamat varchar(100),
	@Kelurahan varchar(50),
	@Kecamatan varchar(50),
	@Province varchar(50),
	@PostalCode varchar(10),
	@PhoneNo varchar(30),
	@Fax varchar(20),
	@WebSite varchar(20),
	@Email nvarchar(255),
	@ContactPerson varchar(50),
	@HP varchar(20),
	@Status tinyint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[Karoseri]
VALUES
(
	@Code,
	@Name,
	@City,
	@Alamat,
	@Kelurahan,
	@Kecamatan,
	@Province,
	@PostalCode,
	@PhoneNo,
	@Fax,
	@WebSite,
	@Email,
	@ContactPerson,
	@HP,
	@Status,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY


GO




/****** Object:  StoredProcedure [dbo].[up_RetrieveKaroseri]    Script Date: 02/08/2018 15:34:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, February 28, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

create PROCEDURE [dbo].[up_RetrieveKaroseri]
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[Code],
	[Name],
	[City],
	[Alamat],
	[Kelurahan],
	[Kecamatan],
	[Province],
	[PostalCode],
	[PhoneNo],
	[Fax],
	[WebSite],
	[Email],
	[ContactPerson],
	[HP],
	[Status],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[Karoseri]

WHERE
	[ID] = @ID

SET NOCOUNT OFF


GO




/****** Object:  StoredProcedure [dbo].[up_RetrieveKaroseriList]    Script Date: 02/08/2018 15:34:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, February 28, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

create PROCEDURE [dbo].[up_RetrieveKaroseriList]

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[Code],
		[Name],
		[City],
		[Alamat],
		[Kelurahan],
		[Kecamatan],
		[Province],
		[PostalCode],
		[PhoneNo],
		[Fax],
		[WebSite],
		[Email],
		[ContactPerson],
		[HP],
		[Status],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[Karoseri] 

SET NOCOUNT OFF


GO



/****** Object:  StoredProcedure [dbo].[up_UpdateKaroseri]    Script Date: 02/08/2018 15:35:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, February 28, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

create PROCEDURE [dbo].[up_UpdateKaroseri]
	@ID int OUTPUT,
	@Code varchar(16),
	@Name varchar(50),
	@City varchar(50),
	@Alamat varchar(100),
	@Kelurahan varchar(50),
	@Kecamatan varchar(50),
	@Province varchar(50),
	@PostalCode varchar(10),
	@PhoneNo varchar(30),
	@Fax varchar(20),
	@WebSite varchar(20),
	@Email nvarchar(255),
	@ContactPerson varchar(50),
	@HP varchar(20),
	@Status tinyint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[Karoseri]
SET
	[Code] = @Code,
	[Name] = @Name,
	[City] = @City,
	[Alamat] = @Alamat,
	[Kelurahan] = @Kelurahan,
	[Kecamatan] = @Kecamatan,
	[Province] = @Province,
	[PostalCode] = @PostalCode,
	[PhoneNo] = @PhoneNo,
	[Fax] = @Fax,
	[WebSite] = @WebSite,
	[Email] = @Email,
	[ContactPerson] = @ContactPerson,
	[HP] = @HP,
	[Status] = @Status,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID


GO


/****** Object:  StoredProcedure [dbo].[up_ValidateKaroseri]    Script Date: 02/08/2018 15:35:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, February 28, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

create PROCEDURE [dbo].[up_ValidateKaroseri]
	@Result	varchar(1000),
	@ID int OUTPUT,
	@Code varchar(16),
	@Name varchar(50),
	@City varchar(50),
	@Alamat varchar(100),
	@Kelurahan varchar(50),
	@Kecamatan varchar(50),
	@Province varchar(50),
	@PostalCode varchar(10),
	@PhoneNo varchar(30),
	@Fax varchar(20),
	@WebSite varchar(20),
	@Email nvarchar(255),
	@ContactPerson varchar(50),
	@HP varchar(20),
	@Status tinyint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS

SET	@Result = ''


GO






