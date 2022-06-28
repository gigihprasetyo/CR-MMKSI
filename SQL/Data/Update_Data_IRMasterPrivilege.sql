--USE [BSIDNET_MMKSI_DMS_20180924_0100_1]
--GO

 UPDATE [dbo].[Privilege]
   SET [Name] = 'sales_umum_master_karoseri_lihat'
      ,[Description] = 'Sales Umum Master Karoseri Lihat'
 WHERE Name like '%ir_view_masterkaroseri%'

 UPDATE [dbo].[Privilege]
   SET [Name] = 'sales_umum_master_leasing_lihat'
      ,[Description] = 'Sales Umum Master Leasing Lihat'
 WHERE Name like '%ir_view_masterleasing%'

 Update [dbo].[Privilege]
   SET [Name] = 'revisi_faktur_master_revision_price_lihat'
      ,[Description] = 'Revisi Faktur Master Revision Price Lihat'
 WHERE Name like '%IR_View_MasterHarga%'
GO