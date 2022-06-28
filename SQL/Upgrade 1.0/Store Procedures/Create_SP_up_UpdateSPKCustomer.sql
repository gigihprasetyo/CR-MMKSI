USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_UpdateSPKCustomer]    Script Date: 02/03/2018 13:24:49 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_UpdateSPKCustomer]
	@ID int OUTPUT,
	@Code varchar(50),
	@ReffCode varchar(50),
	@TipeCustomer smallint,
	@TipePerusahaan smallint,
	@Name1 nvarchar(50),
	@Name2 nvarchar(50),
	@Name3 nvarchar(50),
	@Alamat nvarchar(100),
	@Kelurahan nvarchar(50),
	@Kecamatan nvarchar(50),
	@PostalCode nvarchar(10),
	@PreArea varchar(20),
	@CityID smallint,
	@PrintRegion varchar(1),
	@PhoneNo nvarchar(30),
	@OfficeNo nvarchar(30),
	@HomeNo nvarchar(30),
	@HpNo nvarchar(30),
	@Email nvarchar(50),
	@Status int,
	@MCPStatus smallint,
	@LKPPStatus SMALLINT,
	@SAPCustomerID int,
	@LKPPReference VARCHAR(50),
	@BusinessSectorDetailID INT,
	@ImagePath NVARCHAR(200),
	@RowStatus smallint,
	--@CreatedTime datetime,
	@CreatedBy nvarchar(20),
	--@LastUpdateTime datetime,
	@LastUpdateBy nvarchar(20)
	
AS

UPDATE	[dbo].[SPKCustomer]
SET
	[Code] = @Code,
	[ReffCode] = @ReffCode,
	[TipeCustomer] = @TipeCustomer,
	[TipePerusahaan] = @TipePerusahaan,
	[Name1] = @Name1,
	[Name2] = @Name2,
	[Name3] = @Name3,
	[Alamat] = @Alamat,
	[Kelurahan] = @Kelurahan,
	[Kecamatan] = @Kecamatan,
	[PostalCode] = @PostalCode,
	[PreArea] = @PreArea,
	[CityID] = @CityID,
	[PrintRegion] = @PrintRegion,
	[PhoneNo] = @PhoneNo,
	[OfficeNo] = @OfficeNo,
	[HomeNo] = @HomeNo,
	[HpNo] = @HpNo,
	[Email] = @Email,
	[Status] = @Status,
	[MCPStatus] = @MCPStatus,
	[LKPPStatus] = @LKPPStatus,
	[SAPCustomerID] = @SAPCustomerID,
	[LKPPReference] = @LKPPReference,
	[BusinessSectorDetailID] = @BusinessSectorDetailID,
	[ImagePath] = @ImagePath,
	[RowStatus] = @RowStatus,
	--[CreatedTime] = @CreatedTime,
	[CreatedBy] = @CreatedBy,
	[LastUpdateTime] = GETDATE(),
	[LastUpdateBy] = @LastUpdateBy	
WHERE
	[ID] = @ID
