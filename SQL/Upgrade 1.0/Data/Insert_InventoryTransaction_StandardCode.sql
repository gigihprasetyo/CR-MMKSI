USE [BSIDNET_MMKSI_DMS]
GO

INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
     VALUES ('InventoryTransaction.State',1,'Open',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
     VALUES ('InventoryTransaction.State',2,'Partial Released',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
     VALUES ('InventoryTransaction.State',3,'Released',0,'MitraisTeam',GETDATE());

INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
     VALUES ('InventoryTransaction.TransactionType',1,'Issue',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
     VALUES ('InventoryTransaction.TransactionType',2,'Receipt',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
     VALUES ('InventoryTransaction.TransactionType',3,'WO Issue',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
     VALUES ('InventoryTransaction.TransactionType',4,'WO Return',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
     VALUES ('InventoryTransaction.TransactionType',5,'Inventory Transfer Receipt',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
     VALUES ('InventoryTransaction.TransactionType',6,'IT Issue',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
     VALUES ('InventoryTransaction.TransactionType',7,'IT Receipt',0,'MitraisTeam',GETDATE());

