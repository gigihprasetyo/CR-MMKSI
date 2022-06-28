/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/

USE [BSIDNET_MMKSI_DMS]
GO

BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.SparePartPRFromVendor
	(
	ID int NOT NULL IDENTITY (1, 1),
	PRNumber nvarchar(50) NULL,
	PONumber varchar(100) NULL,
	Owner varchar(100) NOT NULL,
	APVoucherNumber varchar(100) NULL,
	AssignLandedCost bit NULL,
	AutoInvoiced bit NULL,
	DealerCode varchar(100) NOT NULL,
	DeliveryOrderDate datetime NULL,
	DeliveryOrderNumber varchar(50) NULL,
	EventData varchar(4000) NULL,
	EventData2 text NULL,
	GrandTotal money NULL,
	Handling smallint NULL,
	LoadData bit NULL,
	PackingSlipDate datetime NULL,
	PackingSlipNumber varchar(50) NULL,
	PRReferenceRequired bit NULL,
	ReturnPRNumber varchar(100) NULL,
	State smallint NULL,
	TotalBaseAmount money NULL,
	TotalConsumptionTax1Amount money NULL,
	TotalConsumptionTax2Amount money NULL,
	TotalConsumptionTaxAmount money NULL,
	TotalTitleRegistrationFree money NULL,
	TransactionDate datetime NOT NULL,
	TransferOrderRequestingNumber varchar(100) NULL,
	Type smallint NOT NULL,
	VendorDescription varchar(100) NULL,
	Vendor varchar(100) NULL,
	VendorInvoiceNumber varchar(50) NULL,
	WONumber varchar(100) NULL,
	RowStatus smallint NULL,
	CreatedBy varchar(100) NULL,
	CreatedTime datetime NULL,
	LastUpdateBy varchar(100) NULL,
	LastUpdateTime datetime NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.SparePartPRFromVendor ADD CONSTRAINT
	PK_SparePartPRFromVendor PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SparePartPRFromVendor SET (LOCK_ESCALATION = TABLE)
GO
COMMIT