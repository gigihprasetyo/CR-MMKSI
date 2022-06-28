USE [BSIDNET_MMKSI_DMS]
GO

INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
     VALUES ('DeliveryMethod',1,'Sea',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('DeliveryMethod',2,'Air',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('DeliveryMethod',3,'Land',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('PaymentGroup',1,'Week 1',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('PaymentGroup',2,'Week 2',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('PaymentGroup',3,'Week 3',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('PaymentGroup',4,'Week 4',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('PaymentGroup',5,'Week 5',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('Priority',1,'Reguler',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('Priority',2,'SpecialOrder',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('Priority',3,'Urgent',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('POOtherVendorState',1,'Open',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('POOtherVendorState',2,'On Approval',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('POOtherVendorState',3,'Rejected',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('POOtherVendorState',4,'Released',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('POOtherVendorState',5,'Canceled',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('POOtherVendorState',6,'PartialReceipt',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('POOtherVendorState',7,'Received',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('POOtherVendorState',8,'Completed',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('Taxable',1,'Taxable 1',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('Taxable',2,'Not Taxable 1',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('Taxable',3,'Taxable 2',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('Taxable',4,'Not Taxable 2',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('Taxable',5,'Taxable 3',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('Taxable',6,'Not Taxable 3',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('PurchaseFor',1,'SVC - Service',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('PurchaseFor',2,'Stk - Stock Item',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('PurchaseFor',3,'NST - Non Stock',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('PurchaseFor',4,'FAS - Fixed Asset',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('PurchaseFor',5,'VHC - Vehicle',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('FormSource',1,'Information',0,'MitraisTeam',GETDATE());
INSERT INTO [dbo].[StandardCode] ([Category],[ValueId],[ValueDesc],[RowStatus],[CreatedBy],[CreatedTime])
	 VALUES ('FormSource',2,'Service',0,'MitraisTeam',GETDATE());
GO


