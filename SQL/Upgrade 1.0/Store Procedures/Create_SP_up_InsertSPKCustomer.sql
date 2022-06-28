USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_InsertSPKCustomer]    Script Date: 02/03/2018 11:22:09 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_InsertSPKCustomer]
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
INSERT	INTO	[dbo].[SPKCustomer]
		(
		  [Code]
		, [ReffCode]
		, [TipeCustomer]
		, [TipePerusahaan]
		, [Name1]
		, [Name2]
		, [Name3]
		, [Alamat]
		, [Kelurahan]
		, [Kecamatan]
		, [PostalCode]
		, [PreArea]
		, [CityID]
		, [PrintRegion]
		, [PhoneNo]
		, [OfficeNo]
		, [HomeNo]
		, [HpNo]
		, [Email]
		, [Status]
		, [MCPStatus]
		, [lkppstatus]
		, [SAPCustomerID]
		, [LKPPReference]
		, [BusinessSectorDetailID]
		, [ImagePath]
		, [RowStatus]
		, [CreatedTime]
		, [CreatedBy]
		, [LastUpdateTime]
		, [LastUpdateBy]
		)
 
VALUES
(
	@Code,
	@ReffCode,
	@TipeCustomer,
	@TipePerusahaan,
	@Name1,
	@Name2,
	@Name3,
	@Alamat,
	@Kelurahan,
	@Kecamatan,
	@PostalCode,
	@PreArea,
	@CityID,
	@PrintRegion,
	@PhoneNo,
	@OfficeNo,
	@HomeNo,
	@HpNo,
	@Email,
	@Status,
	@MCPStatus,
	@LKPPStatus,
	@SAPCustomerID ,
	@LKPPReference,
	@BusinessSectorDetailID,
	@ImagePath,
	@RowStatus,
	GETDATE(),	
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy)

	
SET @ID = @@IDENTITY
