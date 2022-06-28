/****** KAROSERI ******/
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





/****** LEASING ******/
/****** Object:  StoredProcedure [dbo].[up_RetrieveLeasing]    Script Date: 02/08/2018 15:26:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, March 02, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
create PROCEDURE [dbo].[up_RetrieveLeasing]
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
	[ID],
	LeasingGroupName,
	[LeasingCode],
	[LeasingName],
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
FROM	[dbo].[Leasing]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
GO


----------------------------------------------------------------------------------------
/****** Object:  StoredProcedure [dbo].[up_RetrieveLeasingList]    Script Date: 02/08/2018 15:27:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, March 02, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
create PROCEDURE [dbo].[up_RetrieveLeasingList]

AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
		[ID],
		LeasingGroupName,
		[LeasingCode],
		[LeasingName],
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
		[dbo].[Leasing] 

SET NOCOUNT OFF
GO


----------------------------------------------------------------------------------------
/****** Object:  StoredProcedure [dbo].[up_UpdateLeasing]    Script Date: 02/08/2018 15:28:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, March 02, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
create PROCEDURE [dbo].[up_UpdateLeasing]
	@ID int OUTPUT,
	@LeasingGroupName varchar(50),
	@LeasingCode varchar(16),
	@LeasingName varchar(50),
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

UPDATE	[dbo].[Leasing]
SET	
	LeasingGroupName=@LeasingGroupName,
	[LeasingCode] = @LeasingCode,
	[LeasingName] = @LeasingName,
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


----------------------------------------------------------------------------------------
/****** Object:  StoredProcedure [dbo].[up_ValidateLeasing]    Script Date: 02/08/2018 15:28:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, March 02, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
create PROCEDURE [dbo].[up_ValidateLeasing]
	@Result	varchar(1000),
	@ID int OUTPUT,
	@LeasingGroupName varchar(50),
	@LeasingCode varchar(16),
	@LeasingName varchar(50),
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


----------------------------------------------------------------------------------------
/****** Object:  StoredProcedure [dbo].[up_InsertLeasing]    Script Date: 02/08/2018 15:29:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, March 02, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
create PROCEDURE [dbo].[up_InsertLeasing]
	@ID int OUTPUT,
	@LeasingGroupName varchar(50),
	@LeasingCode varchar(16),
	@LeasingName varchar(50),
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
INSERT	INTO	[dbo].[Leasing]
VALUES
(
	@LeasingGroupName,
	@LeasingCode,
	@LeasingName,
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


----------------------------------------------------------------------------------------
/****** Object:  StoredProcedure [dbo].[up_DeleteLeasing]    Script Date: 02/08/2018 15:30:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, March 02, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
create PROCEDURE [dbo].[up_DeleteLeasing]
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[Leasing]
WHERE
	[ID] = @ID
GO





/****** REVISIONFAKTUR ******/
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 09 Agustus 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_InsertRevisionFaktur
	@ID int OUTPUT,
	@ChassisMasterID int,
	@EndCustomerID int,
	@OldEndCustomerID int,
	@RegNumber varchar(15),
	@RevisionStatus smallint,
	@RevisionTypeID smallint,
	@IsPay smallint,
	@NewValidationDate datetime,
	@NewValidationBy varchar(20),
	@NewConfirmationDate datetime,
	@NewConfirmationBy varchar(20),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[RevisionFaktur]
VALUES
(
	@ChassisMasterID,
	@EndCustomerID,
	@OldEndCustomerID,
	@RegNumber,
	@RevisionStatus,
	@RevisionTypeID,
	@IsPay,
	@NewValidationDate,
	@NewValidationBy,
	@NewConfirmationDate,
	@NewConfirmationBy,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())
	
SET @ID = @@IDENTITY

--CREATE Autonumber @RegNumber/Nomor Pengajuan
DECLARE @SeqNum int

SELECT @SeqNum = MAX(RIGHT(RegNumber,6))
FROM RevisionFaktur 
WHERE '20'+SUBSTRING(RegNumber,3,2) = Year(getdate())

SET @SeqNum = ISNULL(@SeqNum,0)+1
SELECT @RegNumber='RF'+RIGHT(CONVERT(CHAR(4),Year(getdate())),2)+REPLICATE('0',6-LEN(@SeqNum))+CONVERT(VARCHAR(6),@SeqNum)

UPDATE RevisionFaktur SET RegNumber=@RegNumber
WHERE ID=@ID

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 09 Agustus 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_RetrieveRevisionFaktur
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[ChassisMasterID],
	[EndCustomerID],
	[OldEndCustomerID],
	[RegNumber],
	[RevisionStatus],
	[RevisionTypeID],
	[IsPay],
	[NewValidationDate],
	[NewValidationBy],
	[NewConfirmationDate],
	[NewConfirmationBy],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[RevisionFaktur]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 09 Agustus 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_RetrieveRevisionFakturList
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
		[ID],
		[ChassisMasterID],
		[EndCustomerID],
		[OldEndCustomerID],
		[RegNumber],
		[RevisionStatus],
		[RevisionTypeID],
		[IsPay],
		[NewValidationDate],
		[NewValidationBy],
		[NewConfirmationDate],
		[NewConfirmationBy],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[RevisionFaktur] 

SET NOCOUNT OFF
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 09 Agustus 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_UpdateRevisionFaktur
	@ID int OUTPUT,
	@ChassisMasterID int,
	@EndCustomerID int,
	@OldEndCustomerID int,
	@RegNumber varchar(15),
	@RevisionStatus smallint,
	@RevisionTypeID smallint,
	@IsPay smallint,
	@NewValidationDate datetime,
	@NewValidationBy varchar(20),
	@NewConfirmationDate datetime,
	@NewConfirmationBy varchar(20),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[RevisionFaktur]
SET
	[ChassisMasterID] = @ChassisMasterID,
	[EndCustomerID] = @EndCustomerID,
	[OldEndCustomerID] = @OldEndCustomerID,
	[RegNumber] = @RegNumber,
	[RevisionStatus] = @RevisionStatus,
	[RevisionTypeID] = @RevisionTypeID,
	[IsPay] = @IsPay,
	[NewValidationDate] = @NewValidationDate,
	[NewValidationBy] = @NewValidationBy,
	[NewConfirmationDate] = @NewConfirmationDate,
	[NewConfirmationBy] = @NewConfirmationBy,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 09 Agustus 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_DeleteRevisionFaktur
	@ID int OUTPUT	
AS
DELETE
FROM	[dbo].[RevisionFaktur]
WHERE
	[ID] = @ID
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 09 Agustus 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_ValidateRevisionFaktur
	@Result	varchar(1000),
	@ID int OUTPUT,
	@ChassisMasterID int,
	@EndCustomerID int,
	@OldEndCustomerID int,
	@RegNumber varchar(15),
	@RevisionStatus smallint,
	@RevisionTypeID smallint,
	@IsPay smallint,
	@NewValidationDate datetime,
	@NewValidationBy varchar(20),
	@NewConfirmationDate datetime,
	@NewConfirmationBy varchar(20),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS
SET	@Result = ''
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO





/***** REVISIONCHASSISMASTERPROFILE *****/
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_InsertRevisionChassisMasterProfile
	@ID int OUTPUT,
	@ChassisMasterID int,
	@EndCustomerID int,
	@ProfileHeaderID tinyint,
	@GroupID tinyint,
	@ProfileValue varchar(250),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[RevisionChassisMasterProfile]
VALUES
(
	@ChassisMasterID,
	@EndCustomerID,
	@ProfileHeaderID,
	@GroupID,
	@ProfileValue,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())
	
SET @ID = @@IDENTITY
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_RetrieveRevisionChassisMasterProfile
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
	[ID],
	[ChassisMasterID],
	[EndCustomerID],
	[ProfileHeaderID],
	[GroupID],
	[ProfileValue],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[RevisionChassisMasterProfile]
WHERE
	[ID] = @ID

SET NOCOUNT OFF
GO
SET QUOTED_IDENTIFIER OFF 
GO


SET ANSI_NULLS ON 
GO
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_RetrieveRevisionChassisMasterProfileList
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
		[ID],
		[ChassisMasterID],
		[EndCustomerID],
		[ProfileHeaderID],
		[GroupID],
		[ProfileValue],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[RevisionChassisMasterProfile] 

SET NOCOUNT OFF
GO
SET QUOTED_IDENTIFIER OFF 
GO


SET ANSI_NULLS ON 
GO
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_UpdateRevisionChassisMasterProfile
	@ID int OUTPUT,
	@ChassisMasterID int,
	@EndCustomerID int,
	@ProfileHeaderID tinyint,
	@GroupID tinyint,
	@ProfileValue varchar(250),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
UPDATE	[dbo].[RevisionChassisMasterProfile]
SET
	[ChassisMasterID] = @ChassisMasterID,
	[EndCustomerID] = @EndCustomerID,
	[ProfileHeaderID] = @ProfileHeaderID,
	[GroupID] = @GroupID,
	[ProfileValue] = @ProfileValue,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_DeleteRevisionChassisMasterProfile
	@ID int OUTPUT	
AS
DELETE
FROM	[dbo].[RevisionChassisMasterProfile]
WHERE
	[ID] = @ID
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_ValidateRevisionChassisMasterProfile
	@Result	varchar(1000),
	@ID int OUTPUT,
	@ChassisMasterID int,
	@EndCustomerID int,
	@ProfileHeaderID tinyint,
	@GroupID tinyint,
	@ProfileValue varchar(250),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS
SET	@Result = ''
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO





/***** REVISIONPRICE *****/
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_InsertRevisionPrice
	@ID int OUTPUT,
	@CategoryID int,
	@Amount money,
	@ValidFrom smalldatetime,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[RevisionPrice]
VALUES
(
	@CategoryID,
	@Amount,
	@ValidFrom,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())	
SET @ID = @@IDENTITY
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_RetrieveRevisionPrice
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
	[ID],
	[CategoryID],
	[Amount],
	[ValidFrom],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[RevisionPrice]

WHERE
	[ID] = @ID
SET NOCOUNT OFF
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_RetrieveRevisionPriceList
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
		[ID],
		[CategoryID],
		[Amount],
		[ValidFrom],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[RevisionPrice] 
SET NOCOUNT OFF
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_UpdateRevisionPrice
	@ID int OUTPUT,
	@CategoryID int,
	@Amount money,
	@ValidFrom smalldatetime,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
UPDATE	[dbo].[RevisionPrice]
SET
	[CategoryID] = @CategoryID,
	[Amount] = @Amount,
	[ValidFrom] = @ValidFrom,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_DeleteRevisionPrice
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[RevisionPrice]
WHERE
	[ID] = @ID
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_ValidateRevisionPrice
	@Result	varchar(1000),
	@ID int OUTPUT,
	@CategoryID int,
	@Amount money,
	@ValidFrom smalldatetime,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS
SET	@Result = ''
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO





/***** REVISIONSPKFAKTUR *****/
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_InsertRevisionSPKFaktur
	@ID int OUTPUT,
	@SPKHeaderID int,
	@EndCustomerID int,
	@RowStatus smallint,
	--@CreatedTime datetime,
	@CreatedBy varchar(20),
	--@LastUpdateTime datetime,
	@LastUpdateBy varchar(20)
AS
INSERT	INTO	[dbo].[RevisionSPKFaktur]
VALUES
(
	@SPKHeaderID,
	@EndCustomerID,
	@RowStatus,
	GETDATE(),	
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy)
SET @ID = @@IDENTITY
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_RetrieveRevisionSPKFaktur
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
	[ID],
	[SPKHeaderID],
	[EndCustomerID],
	[RowStatus],
	[CreatedTime],
	[CreatedBy],
	[LastUpdateTime],
	[LastUpdateBy]	
FROM	[dbo].[RevisionSPKFaktur]
WHERE
	[ID] = @ID
SET NOCOUNT OFF
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_RetrieveRevisionSPKFakturList
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
		[ID],
		[SPKHeaderID],
		[EndCustomerID],
		[RowStatus],
		[CreatedTime],
		[CreatedBy],
		[LastUpdateTime],
		[LastUpdateBy]		
		FROM	
		[dbo].[RevisionSPKFaktur] 
SET NOCOUNT OFF
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_UpdateRevisionSPKFaktur
	@ID int OUTPUT,
	@SPKHeaderID int,
	@EndCustomerID int,
	@RowStatus smallint,
	--@CreatedTime datetime,
	@CreatedBy varchar(20),
	--@LastUpdateTime datetime,
	@LastUpdateBy varchar(20)
AS
UPDATE	[dbo].[RevisionSPKFaktur]
SET
	[SPKHeaderID] = @SPKHeaderID,
	[EndCustomerID] = @EndCustomerID,
	[RowStatus] = @RowStatus,
	--[CreatedTime] = @CreatedTime,
	[CreatedBy] = @CreatedBy,
	[LastUpdateTime] = GETDATE(),
	[LastUpdateBy] = @LastUpdateBy	
WHERE
	[ID] = @ID
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_DeleteRevisionSPKFaktur
	@ID int OUTPUT	
AS
DELETE
FROM	[dbo].[RevisionSPKFaktur]
WHERE
	[ID] = @ID
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_ValidateRevisionSPKFaktur
	@Result	varchar(1000),
	@ID int OUTPUT,
	@SPKHeaderID int,
	@EndCustomerID int,
	@RowStatus smallint,
	@CreatedTime datetime,
	@CreatedBy varchar(20),
	@LastUpdateTime datetime,
	@LastUpdateBy varchar(20)	
AS
SET	@Result = ''
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO





/**** REVISIONTYPE ****/
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_InsertRevisionType
	@ID int OUTPUT,
	@Description varchar(100),
	@RevisionCode varchar(5),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[RevisionType]
VALUES
(
	@Description,
	@RevisionCode,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())
SET @ID = @@IDENTITY
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_RetrieveRevisionType
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
	[ID],
	[Description],
	[RevisionCode],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[RevisionType]
WHERE
	[ID] = @ID
SET NOCOUNT OFF
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_RetrieveRevisionTypeList
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
		[ID],
		[Description],
		[RevisionCode],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[RevisionType] 
SET NOCOUNT OFF
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_UpdateRevisionType
	@ID int OUTPUT,
	@Description varchar(100),
	@RevisionCode varchar(5),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
UPDATE	[dbo].[RevisionType]
SET
	[Description] = @Description,
	[RevisionCode] = @RevisionCode,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_DeleteRevisionType
	@ID int OUTPUT	
AS
DELETE
FROM	[dbo].[RevisionType]
WHERE
	[ID] = @ID
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].up_ValidateRevisionType
	@Result	varchar(1000),
	@ID int OUTPUT,
	@Description varchar(100),
	@RevisionCode varchar(5),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS
SET	@Result = ''
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO





/***** SPKCHASSIS *****/
/****** Object:  StoredProcedure [dbo].[up_DeleteSPKChassis]    Script Date: 09/08/2018 13:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_DeleteSPKChassis]
	@ID int OUTPUT	
AS
DELETE
FROM	[dbo].[SPKChassis]
WHERE
	[ID] = @ID
GO


/****** Object:  StoredProcedure [dbo].[up_InsertSPKChassis]    Script Date: 09/08/2018 13:05:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_InsertSPKChassis]
	@ID int OUTPUT,
	@SPKDetailID int,
	@ChassisMasterID int,
	@MatchingType smallint,
	@MatchingDate datetime,
	@MatchingNumber varchar(50),
	@ReferenceNumber varchar(50),
	@KeyNumber varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(50),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(50)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[SPKChassis]
VALUES
(
	@SPKDetailID,
	@ChassisMasterID,
	@MatchingType,
	@MatchingDate,
	@MatchingNumber,
	@ReferenceNumber,
	@KeyNumber,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())	
SET @ID = @@IDENTITY
GO


/****** Object:  StoredProcedure [dbo].[up_RetrieveSPKChassis]    Script Date: 09/08/2018 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_RetrieveSPKChassis]
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
	[ID],
	[SPKDetailID],
	[ChassisMasterID],
	[MatchingType],
	[MatchingDate],
	[MatchingNumber],
	[ReferenceNumber],
	[KeyNumber],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[SPKChassis]
WHERE
	[ID] = @ID
SET NOCOUNT OFF
GO


/****** Object:  StoredProcedure [dbo].[up_RetrieveSPKChassisList]    Script Date: 09/08/2018 13:06:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_RetrieveSPKChassisList]
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
		[ID],
		[SPKDetailID],
		[ChassisMasterID],
		[MatchingType],
		[MatchingDate],
		[MatchingNumber],
		[ReferenceNumber],
		[KeyNumber],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[SPKChassis] 
SET NOCOUNT OFF
GO


/****** Object:  StoredProcedure [dbo].[up_UpdateSPKChassis]    Script Date: 09/08/2018 13:07:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_UpdateSPKChassis]
	@ID int OUTPUT,
	@SPKDetailID int,
	@ChassisMasterID int,
	@MatchingType smallint,
	@MatchingDate datetime,
	@MatchingNumber varchar(50),
	@ReferenceNumber varchar(50),
	@KeyNumber varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(50),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(50)	--@LastUpdateTime datetime	
AS
UPDATE	[dbo].[SPKChassis]
SET
	[SPKDetailID] = @SPKDetailID,
	[ChassisMasterID] = @ChassisMasterID,
	[MatchingType] = @MatchingType,
	[MatchingDate] = @MatchingDate,
	[MatchingNumber] = @MatchingNumber,
	[ReferenceNumber] = @ReferenceNumber,
	[KeyNumber] = @KeyNumber,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
GO


/****** Object:  StoredProcedure [dbo].[up_ValidateSPKChassis]    Script Date: 09/08/2018 13:07:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_ValidateSPKChassis]
	@Result	varchar(1000),
	@ID int OUTPUT,
	@SPKDetailID int,
	@ChassisMasterID int,
	@MatchingType smallint,
	@MatchingDate datetime,
	@MatchingNumber varchar(50),
	@ReferenceNumber varchar(50),
	@KeyNumber varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(50),
	@CreatedTime datetime,
	@LastUpdateBy varchar(50),
	@LastUpdateTime datetime	
AS
SET	@Result = ''
GO





/***** UPDATE ENDCUSTOMER *****/
/****** Object:  StoredProcedure [dbo].[up_InsertEndCustomer]    Script Date: 18/09/2018 11:45:45 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[up_InsertEndCustomer]
	@ID int OUTPUT,
	@ProjectIndicator varchar(1),
	@RefChassisNumberID int,
	@CustomerID int,
	@Name1 varchar(50),
	@FakturDate datetime,
	@OpenFakturDate datetime,
	@FakturNumber varchar(18),
	@AreaViolationFlag varchar(50),
	@AreaViolationPaymentMethodID tinyint,
	@AreaViolationyAmount money,
	@AreaViolationBankName varchar(30),
	@AreaViolationGyroNumber varchar(30),
	@PenaltyFlag varchar(50),
	@PenaltyPaymentMethodID tinyint,
	@PenaltyAmount money,
	@PenaltyBankName varchar(30),
	@PenaltyGyroNumber varchar(30),
	@ReferenceLetterFlag varchar(1),
	@ReferenceLetter varchar(40),
	@SaveBy varchar(20),
	@SaveTime datetime,
	@ValidateBy varchar(20),
	@ValidateTime datetime,
	@ConfirmBy varchar(20),
	@ConfirmTime datetime,
	@DownloadBy varchar(20),
	@DownloadTime datetime,
	@PrintedBy varchar(20),
	@PrintedTime datetime,
	@CleansingCustomerID int,
	@MCPHeaderID int, 
	@MCPStatus SMALLINT,
	@LKPPHeaderID int, 
	@LKPPStatus SMALLINT,
	@Remark1 varchar(255),
	@Remark2 varchar(255),
	@HandoverDate datetime,
	@IsTemporary smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[EndCustomer]
VALUES
(
	@ProjectIndicator,
	@RefChassisNumberID,
	@CustomerID,
	@Name1,
	@FakturDate,
	@OpenFakturDate,
	@FakturNumber,
	@AreaViolationFlag,
	@AreaViolationPaymentMethodID,
	@AreaViolationyAmount,
	@AreaViolationBankName,
	@AreaViolationGyroNumber,
	@PenaltyFlag,
	@PenaltyPaymentMethodID,
	@PenaltyAmount,
	@PenaltyBankName,
	@PenaltyGyroNumber,
	@ReferenceLetterFlag,
	@ReferenceLetter,
	@SaveBy,
	@SaveTime,
	@ValidateBy,
	@ValidateTime,
	@ConfirmBy,
	@ConfirmTime,
	@DownloadBy,
	@DownloadTime,
	@PrintedBy,
	@PrintedTime,
	@CleansingCustomerID,
	@MCPHeaderID,
	@MCPStatus,
	@LKPPHeaderID,
	@LKPPStatus,
	@Remark1,
	@Remark2,
	@HandoverDate,
	@IsTemporary,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())
SET @ID = @@IDENTITY


---------------------------------------------------------------
/****** Object:  StoredProcedure [dbo].[up_RetrieveEndCustomer]    Script Date: 18/09/2018 11:47:21 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[up_RetrieveEndCustomer]
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
	[ID],
	[ProjectIndicator],
	[RefChassisNumberID],
	[CustomerID],
	[Name1],
	[FakturDate],
	[OpenFakturDate],
	[FakturNumber],
	[AreaViolationFlag],
	[AreaViolationPaymentMethodID],
	[AreaViolationyAmount],
	[AreaViolationBankName],
	[AreaViolationGyroNumber],
	[PenaltyFlag],
	[PenaltyPaymentMethodID],
	[PenaltyAmount],
	[PenaltyBankName],
	[PenaltyGyroNumber],
	[ReferenceLetterFlag],
	[ReferenceLetter],
	[SaveBy],
	[SaveTime],
	[ValidateBy],
	[ValidateTime],
	[ConfirmBy],
	[ConfirmTime],
	[DownloadBy],
	[DownloadTime],
	[PrintedBy],
	[PrintedTime],
	[CleansingCustomerID],
	[MCPHeaderID],
	[MCPStatus],
	[LKPPHeaderID],
	[LKPPStatus],
	[Remark1],
	[Remark2],
	[HandoverDate],
	[IsTemporary],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[EndCustomer]
WHERE
	[ID] = @ID
SET NOCOUNT OFF


---------------------------------------------------------------
/****** Object:  StoredProcedure [dbo].[up_RetrieveEndCustomerList]    Script Date: 18/09/2018 11:47:48 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[up_RetrieveEndCustomerList]
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
		[ID],
		[ProjectIndicator],
		[RefChassisNumberID],
		[CustomerID],
		[Name1],
		[FakturDate],
		[OpenFakturDate],
		[FakturNumber],
		[AreaViolationFlag],
		[AreaViolationPaymentMethodID],
		[AreaViolationyAmount],
		[AreaViolationBankName],
		[AreaViolationGyroNumber],
		[PenaltyFlag],
		[PenaltyPaymentMethodID],
		[PenaltyAmount],
		[PenaltyBankName],
		[PenaltyGyroNumber],
		[ReferenceLetterFlag],
		[ReferenceLetter],
		[SaveBy],
		[SaveTime],
		[ValidateBy],
		[ValidateTime],
		[ConfirmBy],
		[ConfirmTime],
		[DownloadBy],
		[DownloadTime],
		[PrintedBy],
		[PrintedTime],
		[CleansingCustomerID],
		[MCPHeaderID],
		[MCPStatus],
		[LKPPHeaderID],
		[LKPPStatus],
		[Remark1],
		[Remark2],
		[HandoverDate],
		[IsTemporary],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[EndCustomer] 

SET NOCOUNT OFF


---------------------------------------------------------------
/****** Object:  StoredProcedure [dbo].[up_UpdateEndCustomer]    Script Date: 18/09/2018 11:48:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
/*
2015/11/25 - add recalculation MCPDetail.UnitRemain
*/
---------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[up_UpdateEndCustomer]
    @ID INT OUTPUT ,
    @ProjectIndicator VARCHAR(1) ,
    @RefChassisNumberID INT ,
    @CustomerID INT ,
    @Name1 VARCHAR(50) ,
    @FakturDate DATETIME ,
    @OpenFakturDate DATETIME ,
    @FakturNumber VARCHAR(18) ,
    @AreaViolationFlag VARCHAR(50) ,
    @AreaViolationPaymentMethodID TINYINT ,
    @AreaViolationyAmount MONEY ,
    @AreaViolationBankName VARCHAR(30) ,
    @AreaViolationGyroNumber VARCHAR(30) ,
    @PenaltyFlag VARCHAR(50) ,
    @PenaltyPaymentMethodID TINYINT ,
    @PenaltyAmount MONEY ,
    @PenaltyBankName VARCHAR(30) ,
    @PenaltyGyroNumber VARCHAR(30) ,
    @ReferenceLetterFlag VARCHAR(1) ,
    @ReferenceLetter VARCHAR(40) ,
    @SaveBy VARCHAR(20) ,
    @SaveTime DATETIME ,
    @ValidateBy VARCHAR(20) ,
    @ValidateTime DATETIME ,
    @ConfirmBy VARCHAR(20) ,
    @ConfirmTime DATETIME ,
    @DownloadBy VARCHAR(20) ,
    @DownloadTime DATETIME ,
    @PrintedBy VARCHAR(20) ,
    @PrintedTime DATETIME ,
    @CleansingCustomerID INT ,
    @MCPHeaderID INT ,
    @MCPStatus SMALLINT ,
	@LKPPHeaderID int, 
	@LKPPStatus SMALLINT,
    @Remark1 VARCHAR(255) ,
    @Remark2 VARCHAR(255) ,
	@HandoverDate DATETIME,
	@IsTemporary SMALLINT,
    @RowStatus SMALLINT ,
    @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
    @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
    BEGIN

	--Temp MCP before changed
       
        DECLARE @Temp_Table AS TABLE
            (
              MCPHeaderIDBefore INT,
		 	  LKPPHeaderIDBefore INT
            )
		 
        UPDATE  [dbo].[EndCustomer]
        SET     [ProjectIndicator] = @ProjectIndicator ,
                [RefChassisNumberID] = @RefChassisNumberID ,
                [CustomerID] = @CustomerID ,
                [Name1] = @Name1 ,
                [FakturDate] = @FakturDate ,
                [OpenFakturDate] = @OpenFakturDate ,
                [FakturNumber] = @FakturNumber ,
                [AreaViolationFlag] = @AreaViolationFlag ,
                [AreaViolationPaymentMethodID] = @AreaViolationPaymentMethodID ,
                [AreaViolationyAmount] = @AreaViolationyAmount ,
                [AreaViolationBankName] = @AreaViolationBankName ,
                [AreaViolationGyroNumber] = @AreaViolationGyroNumber ,
                [PenaltyFlag] = @PenaltyFlag ,
                [PenaltyPaymentMethodID] = @PenaltyPaymentMethodID ,
                [PenaltyAmount] = @PenaltyAmount ,
                [PenaltyBankName] = @PenaltyBankName ,
                [PenaltyGyroNumber] = @PenaltyGyroNumber ,
                [ReferenceLetterFlag] = @ReferenceLetterFlag ,
                [ReferenceLetter] = @ReferenceLetter ,
                [SaveBy] = @SaveBy ,
                [SaveTime] = @SaveTime ,
                [ValidateBy] = @ValidateBy ,
                [ValidateTime] = @ValidateTime ,
                [ConfirmBy] = @ConfirmBy ,
                [ConfirmTime] = @ConfirmTime ,
                [DownloadBy] = @DownloadBy ,
                [DownloadTime] = @DownloadTime ,
                [PrintedBy] = @PrintedBy ,
                [PrintedTime] = @PrintedTime ,
                [CleansingCustomerID] = @CleansingCustomerID ,
                [MCPHeaderID] = @MCPHeaderID ,
                [MCPStatus] = @MCPStatus ,
				[LKPPHeaderID] = @LKPPHeaderID ,
                [LKPPStatus] = @LKPPStatus ,
				[Remark1] = @Remark1 ,
                [Remark2] = @Remark2 ,
				[HandoverDate]=@HandoverDate,
				[IsTemporary] = @IsTemporary,
                [RowStatus] = @RowStatus ,
                [CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
                [LastUpdateBy] = @LastUpdateBy ,
                [LastUpdateTime] = GETDATE()
        OUTPUT  Deleted.MCPHeaderID, deleted.LKPPHeaderID
                INTO @Temp_Table
        WHERE   [ID] = @ID

/*Validasi update rows*/
     
        DECLARE @MCPHeaderIDBefore INT = NULL
		DECLARE @LKPPHeaderIDBefore INT = NULL
	
        SELECT  @MCPHeaderIDBefore = MCPHeaderIDBefore,
				@LKPPHeaderIDBefore = LKPPHeaderIDBefore
        FROM    @Temp_Table
      
        BEGIN
            SET NOCOUNT ON
		  
			 -- Update Previous MCP
            IF @MCPHeaderIDBefore IS NOT NULL
                BEGIN
                    EXEC up_RecalculateMCP @MCPHeaderIDBefore

                END
           
				--update current MCP
            IF @MCPHeaderID IS NOT NULL AND @MCPHeaderIDBefore<> @MCPHeaderID
                BEGIN 
                    EXEC up_RecalculateMCP @MCPHeaderID
                END
		 
		     -- Update Previous LKPP
            IF @LKPPHeaderIDBefore IS NOT NULL
                BEGIN
                    EXEC up_RecalculateLKPP @LKPPHeaderIDBefore
                END

					--update current LKPP
            IF @LKPPHeaderID IS NOT NULL AND @LKPPHeaderIDBefore<> @LKPPHeaderID
                BEGIN 
                    EXEC up_RecalculateLKPP @LKPPHeaderID
                END

            SET NOCOUNT OFF 
        END
        
    END


-----------------------------------------------------------------
/****** Object:  StoredProcedure [dbo].[up_ValidateEndCustomer]    Script Date: 18/09/2018 11:48:48 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[up_ValidateEndCustomer]
	@Result	varchar(1000),
	@ID int OUTPUT,
	@ProjectIndicator varchar(1),
	@RefChassisNumberID int,
	@CustomerID int,
	@Name1 varchar(50),
	@FakturDate datetime,
	@OpenFakturDate datetime,
	@FakturNumber varchar(18),
	@AreaViolationFlag varchar(50),
	@AreaViolationPaymentMethodID tinyint,
	@AreaViolationyAmount money,
	@AreaViolationBankName varchar(30),
	@AreaViolationGyroNumber varchar(30),
	@PenaltyFlag varchar(50),
	@PenaltyPaymentMethodID tinyint,
	@PenaltyAmount money,
	@PenaltyBankName varchar(30),
	@PenaltyGyroNumber varchar(30),
	@ReferenceLetterFlag varchar(1),
	@ReferenceLetter varchar(40),
	@SaveBy varchar(20),
	@SaveTime datetime,
	@ValidateBy varchar(20),
	@ValidateTime datetime,
	@ConfirmBy varchar(20),
	@ConfirmTime datetime,
	@DownloadBy varchar(20),
	@DownloadTime datetime,
	@PrintedBy varchar(20),
	@PrintedTime datetime,
	@CleansingCustomerID int,
	@MCPHeaderID int,
	@MCPStatus SMALLINT,
	@LKPPHeaderID int,
	@LKPPStatus SMALLINT,
	@Remark1 varchar(255),
	@Remark2 varchar(255),
	@HandoverDate datetime,
	@IsTemporary smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS
SET	@Result = ''





/***** REVISIONPAYMNETDETAIL *****/
/****** Object:  StoredProcedure [dbo].[up_DeleteRevisionPaymentDetail]    Script Date: 01/10/2018 14:22:04 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_DeleteRevisionPaymentDetail]
	@ID int OUTPUT	
AS
DELETE
FROM	[dbo].[RevisionPaymentDetail]
WHERE
	[ID] = @ID
GO


/****** Object:  StoredProcedure [dbo].[up_InsertRevisionPaymentDetail]    Script Date: 01/10/2018 14:23:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_InsertRevisionPaymentDetail]
	@ID int OUTPUT,
	@RevisionFakturID int,
	@RevisionPaymentHeaderID int,
	@RevisionSAPDocID int,
	@IsCancel smallint,
	@CancelReason varchar(250),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[RevisionPaymentDetail]
VALUES
(
	@RevisionFakturID,
	@RevisionPaymentHeaderID,
	@RevisionSAPDocID,
	@IsCancel,
	@CancelReason,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())
SET @ID = @@IDENTITY
GO


/****** Object:  StoredProcedure [dbo].[up_RetrieveRevisionPaymentDetail]    Script Date: 01/10/2018 14:24:58 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_RetrieveRevisionPaymentDetail]
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
	[ID],
	[RevisionFakturID],
	[RevisionPaymentHeaderID],
	[RevisionSAPDocID],
	[IsCancel],
	[CancelReason],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[RevisionPaymentDetail]
WHERE
	[ID] = @ID
SET NOCOUNT OFF
GO


/****** Object:  StoredProcedure [dbo].[up_RetrieveRevisionPaymentDetailList]    Script Date: 01/10/2018 14:25:37 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_RetrieveRevisionPaymentDetailList]
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
		[ID],
		[RevisionFakturID],
		[RevisionPaymentHeaderID],
		[RevisionSAPDocID],
		[IsCancel],
		[CancelReason],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[RevisionPaymentDetail] 
SET NOCOUNT OFF
GO


/****** Object:  StoredProcedure [dbo].[up_UpdateRevisionPaymentDetail]    Script Date: 01/10/2018 14:26:06 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_UpdateRevisionPaymentDetail]
	@ID int OUTPUT,
	@RevisionFakturID int,
	@RevisionPaymentHeaderID int,
	@RevisionSAPDocID int,
	@IsCancel smallint,
	@CancelReason varchar(250),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
UPDATE	[dbo].[RevisionPaymentDetail]
SET
	[RevisionFakturID] = @RevisionFakturID,
	[RevisionPaymentHeaderID] = @RevisionPaymentHeaderID,
	[RevisionSAPDocID] = @RevisionSAPDocID,
	[IsCancel] = @IsCancel,
	[CancelReason] = @CancelReason,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
GO


/****** Object:  StoredProcedure [dbo].[up_ValidateRevisionPaymentDetail]    Script Date: 01/10/2018 14:26:58 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_ValidateRevisionPaymentDetail]
	@Result	varchar(1000),
	@ID int OUTPUT,
	@RevisionFakturID int,
	@RevisionPaymentHeaderID int,
	@RevisionSAPDocID int,
	@IsCancel smallint,
	@CancelReason varchar(250),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS
SET	@Result = ''
GO





/***** REVISIONPAYMENTHEADER *****/
/****** Object:  StoredProcedure [dbo].[up_DeleteRevisionPaymentHeader]    Script Date: 01/10/2018 14:28:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_DeleteRevisionPaymentHeader]
	@ID int OUTPUT	
AS
DELETE
FROM	[dbo].[RevisionPaymentHeader]
WHERE
	[ID] = @ID
GO


/****** Object:  StoredProcedure [dbo].[up_InsertRevisionPaymentHeader]    Script Date: 01/10/2018 14:29:10 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_InsertRevisionPaymentHeader]
	@ID int OUTPUT,
	@DealerID int,
	@PaymentType varchar(3),
	@RegNumber varchar(15),
	@RevisionPaymentDocID int,
	@SlipNumber varchar(20),
	@TotalAmount money,
	@Status smallint,
	@EvidencePath varchar(150),
	@ActualPaymentDate datetime,
	@ActualPaymentAmount money,
	@AccDocNumber varchar(30),
	@GyroDate datetime,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

--CREATE Autonumber Nomor Pembayaran
DECLARE @SeqNum int, @DealerCode as char(5)

SELECT @DealerCode=RIGHT(RTRIM(DealerCode),5) FROM Dealer WHERE ID=@DealerID

SELECT @SeqNum = MAX(RIGHT(RegNumber,4))
FROM RevisionPaymentHeader with (nolock)
WHERE '20'+SUBSTRING(RegNumber,7,2) = Year(getdate())

SET @SeqNum = ISNULL(@SeqNum,0)+1
SELECT @RegNumber='3' + @DealerCode + RIGHT(CONVERT(CHAR(4),Year(getdate())),2) + REPLICATE('0',4-LEN(@SeqNum)) + CONVERT(VARCHAR(4), @SeqNum)

INSERT	INTO	[dbo].[RevisionPaymentHeader]
VALUES
(
	@DealerID,
	@PaymentType,
	@RegNumber,
	@RevisionPaymentDocID,
	@SlipNumber,
	@TotalAmount,
	@Status,
	@EvidencePath,
	@ActualPaymentDate,
	@ActualPaymentAmount,
	@AccDocNumber,
	@GyroDate,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())	
SET @ID = @@IDENTITY
GO


/****** Object:  StoredProcedure [dbo].[up_RetrieveRevisionPaymentHeader]    Script Date: 01/10/2018 14:29:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_RetrieveRevisionPaymentHeader]
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
	[ID],
	[DealerID],
	[PaymentType],
	[RegNumber],
	[RevisionPaymentDocID],
	[SlipNumber],
	[TotalAmount],
	[Status],
	[EvidencePath],
	[ActualPaymentDate],
	[ActualPaymentAmount],
	[AccDocNumber],
	[GyroDate],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[RevisionPaymentHeader]
WHERE
	[ID] = @ID
SET NOCOUNT OFF
GO


/****** Object:  StoredProcedure [dbo].[up_RetrieveRevisionPaymentHeaderList]    Script Date: 01/10/2018 14:31:14 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_RetrieveRevisionPaymentHeaderList]
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
		[ID],
		[DealerID],
		[PaymentType],
		[RegNumber],
		[RevisionPaymentDocID],
		[SlipNumber],
		[TotalAmount],
		[Status],
		[EvidencePath],
		[ActualPaymentDate],
		[ActualPaymentAmount],
		[AccDocNumber],
		[GyroDate],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[RevisionPaymentHeader] 
SET NOCOUNT OFF
GO


/****** Object:  StoredProcedure [dbo].[up_UpdateRevisionPaymentHeader]    Script Date: 01/10/2018 14:31:44 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_UpdateRevisionPaymentHeader]
	@ID int OUTPUT,
	@DealerID int,
	@PaymentType varchar(3),
	@RegNumber varchar(15),
	@RevisionPaymentDocID int,
	@SlipNumber varchar(20),
	@TotalAmount money,
	@Status smallint,
	@EvidencePath varchar(150),
	@ActualPaymentDate datetime,
	@ActualPaymentAmount money,
	@AccDocNumber varchar(30),
	@GyroDate datetime,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
UPDATE	[dbo].[RevisionPaymentHeader]
SET
	[DealerID] = @DealerID,
	[PaymentType] = @PaymentType,
	[RegNumber] = @RegNumber,
	[RevisionPaymentDocID] = @RevisionPaymentDocID,
	[SlipNumber] = @SlipNumber,
	[TotalAmount] = @TotalAmount,
	[Status] = @Status,
	[EvidencePath] = @EvidencePath,
	[ActualPaymentDate] = @ActualPaymentDate,
	[ActualPaymentAmount] = @ActualPaymentAmount,
	[AccDocNumber] = @AccDocNumber,
	[GyroDate] = @GyroDate,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
GO


/****** Object:  StoredProcedure [dbo].[up_ValidateRevisionPaymentHeader]    Script Date: 01/10/2018 14:32:15 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_ValidateRevisionPaymentHeader]
	@Result	varchar(1000),
	@ID int OUTPUT,
	@DealerID int,
	@PaymentType varchar(3),
	@RegNumber varchar(15),
	@RevisionPaymentDocID int,
	@SlipNumber varchar(20),
	@TotalAmount money,
	@Status smallint,
	@EvidencePath varchar(150),
	@ActualPaymentDate datetime,
	@ActualPaymentAmount money,
	@AccDocNumber varchar(30),
	@GyroDate datetime,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS
SET	@Result = ''
GO





/***** REVISIONSAPDOC *****/
/****** Object:  StoredProcedure [dbo].[up_InsertRevisionSAPDoc]    Script Date: 19/09/2018 16:53:20 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_InsertRevisionSAPDoc]
	@ID int OUTPUT,
	@RevisionFakturID int,
	@DebitChargeNo varchar(10),
	@DCAmount money,
	@DebitMemoNo varchar(15),
	@DMAmount money,
	@RowStatus smallint,
	@CreatedBy varchar(100),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(100)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[RevisionSAPDoc]
VALUES
(
	@RevisionFakturID,
	@DebitChargeNo,
	@DCAmount,
	@DebitMemoNo,
	@DMAmount,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())
SET @ID = @@IDENTITY


-----------------------------------------
/****** Object:  StoredProcedure [dbo].[up_RetrieveRevisionSAPDoc]    Script Date: 19/09/2018 16:56:43 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_RetrieveRevisionSAPDoc]
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
	[ID],
	[RevisionFakturID],
	[DebitChargeNo],
	[DCAmount],
	[DMAmount],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[RevisionSAPDoc]
WHERE
	[ID] = @ID
SET NOCOUNT OFF


-----------------------------------------
/****** Object:  StoredProcedure [dbo].[up_RetrieveRevisionSAPDocList]    Script Date: 19/09/2018 16:54:04 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_RetrieveRevisionSAPDocList]
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
		[ID],
		[RevisionFakturID],
		[DebitChargeNo],
		[DCAmount],
		[DebitMemoNo],
		[DMAmount],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[RevisionSAPDoc] 

SET NOCOUNT OFF


-------------------------------------------------
/****** Object:  StoredProcedure [dbo].[up_UpdateRevisionSAPDoc]    Script Date: 19/09/2018 16:54:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_UpdateRevisionSAPDoc]
	@ID int OUTPUT,
	@RevisionFakturID int,
	@DebitChargeNo varchar(10),
	@DCAmount money,
	@DebitMemoNo varchar(15),
	@DMAmount money,
	@RowStatus smallint,
	@CreatedBy varchar(100),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(100)	--@LastUpdateTime datetime	
AS
UPDATE	[dbo].[RevisionSAPDoc]
SET
	[RevisionFakturID] = @RevisionFakturID,
	[DebitChargeNo] = @DebitChargeNo,
	[DCAmount] = @DCAmount,
	[DebitMemoNo] = @DebitMemoNo,
	[DMAmount] = @DMAmount,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID


------------------------------------------------
/****** Object:  StoredProcedure [dbo].[up_ValidateRevisionSAPDoc]    Script Date: 19/09/2018 16:55:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[up_ValidateRevisionSAPDoc]
	@Result	varchar(1000),
	@ID int OUTPUT,
	@RevisionFakturID int,
	@DebitChargeNo varchar(10),
	@DCAmount money,
	@DebitMemoNo varchar(15),
	@DMAmount money,
	@RowStatus smallint,
	@CreatedBy varchar(100),
	@CreatedTime datetime,
	@LastUpdateBy varchar(100),
	@LastUpdateTime datetime	
AS
SET	@Result = ''






/***** DEALER SYSTEM *****/

/****** Object:  Stored Procedure [dbo].[up_UpdateDealerSystems]    Script Date: 25 September 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_InsertDealerSystems]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_InsertDealerSystems]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveDealerSystems]    Script Date: 25 September 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveDealerSystems]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveDealerSystems]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveDealerSystemsList]    Script Date: 25 September 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveDealerSystemsList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveDealerSystemsList]
GO


/****** Object:  Stored Procedure [dbo].[up_UpdateDealerSystems]    Script Date: 25 September 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_UpdateDealerSystems]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_UpdateDealerSystems]
GO


/****** Object:  Stored Procedure [dbo].[up_DeleteDealerSystems]    Script Date: 25 September 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_DeleteDealerSystems]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_DeleteDealerSystems]
GO


/****** Object:  Stored Procedure [dbo].[up_ValidateDealerSystems]    Script Date: 25 September 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_ValidateDealerSystems]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_ValidateDealerSystems]
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertDealerSystems
	@ID int OUTPUT,
	@DealerID smallint,
	@SystemID int,
	@isSPKMatchFaktur bit,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[DealerSystems]
VALUES
(
	@DealerID,
	@SystemID,
	@isSPKMatchFaktur,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveDealerSystems
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[DealerID],
	[SystemID],
	[isSPKMatchFaktur],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[DealerSystems]

WHERE
	[ID] = @ID

SET NOCOUNT OFF

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveDealerSystemsList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[DealerID],
		[SystemID],
		[isSPKMatchFaktur],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[DealerSystems] 

SET NOCOUNT OFF

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdateDealerSystems
	@ID int OUTPUT,
	@DealerID smallint,
	@SystemID int,
	@isSPKMatchFaktur bit,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[DealerSystems]
SET
	[DealerID] = @DealerID,
	[SystemID] = @SystemID,
	[isSPKMatchFaktur] = @isSPKMatchFaktur,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteDealerSystems
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[DealerSystems]
WHERE
	[ID] = @ID

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidateDealerSystems
	@Result	varchar(1000),
	@ID int OUTPUT,
	@DealerID smallint,
	@SystemID int,
	@isSPKMatchFaktur bit,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS

SET	@Result = ''

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
GRANT  EXECUTE  ON [dbo].up_InsertDealerSystems TO [bsi];

GRANT  EXECUTE  ON [dbo].up_UpdateDealerSystems TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveDealerSystems TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveDealerSystemsList TO [bsi];

GRANT  EXECUTE  ON [dbo].up_ValidateDealerSystems TO [bsi];

GRANT  EXECUTE  ON [dbo].up_DeleteDealerSystems TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO




