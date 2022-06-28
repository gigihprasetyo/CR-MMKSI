USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_RetrieveSPKCustomerList]    Script Date: 02/03/2018 11:43:08 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_RetrieveSPKCustomerList]

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[Code],
		[ReffCode],
		[TipeCustomer],
		[TipePerusahaan],
		[Name1],
		[Name2],
		[Name3],
		[Alamat],
		[Kelurahan],
		[Kecamatan],
		[PostalCode],
		[PreArea],
		[CityID],
		[PrintRegion],
		[PhoneNo],
		[OfficeNo],
		[HomeNo],
		[HpNo],
		[Email],
		[Status],
		[MCPStatus],
		[LKPPStatus],
		[SAPCustomerID],
		[LKPPReference],
		[BusinessSectorDetailID],
		[ImagePath],
		[RowStatus],
		[CreatedTime],
		[CreatedBy],
		[LastUpdateTime],
		[LastUpdateBy]		
		FROM	
		[dbo].[SPKCustomer] 

SET NOCOUNT OFF
