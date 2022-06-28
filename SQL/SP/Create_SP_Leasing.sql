USE [BSIDNET_MMKSI_CR_IR]
GO

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