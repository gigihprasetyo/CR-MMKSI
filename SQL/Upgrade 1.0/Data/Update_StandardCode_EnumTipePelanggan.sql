USE [BSIDNET_MMKSI_DMS_20180605_0100]
GO

UPDATE [dbo].[StandardCode]
   SET [Sequence] = 1
 WHERE Category = 'EnumTipePelanggan' and ValueDesc = 'Perusahaan'
GO

UPDATE [dbo].[StandardCode]
   SET [Sequence] = 2
 WHERE Category = 'EnumTipePelanggan' and ValueDesc = 'BUMN_Pemerintah'
GO

UPDATE [dbo].[StandardCode]
   SET [Sequence] = 3
 WHERE Category = 'EnumTipePelanggan' and ValueDesc = 'Perorangan'
GO

UPDATE [dbo].[StandardCode]
   SET [Sequence] = 4
WHERE Category = 'EnumTipePelanggan' and ValueDesc = 'Lainnya'
GO


