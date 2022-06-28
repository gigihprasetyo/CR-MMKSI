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
ALTER TABLE dbo.SparePartPRFromVendor SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.SparePartPRDetailFromVendor
	(
	ID int NOT NULL IDENTITY (1, 1),
	PRDetailNumber varchar(50) NULL,
	SparePartPRID int NOT NULL,
	PRNumber varchar(100) NOT NULL,
	Owner varchar(100) NOT NULL,
	BaseReceivedQuantity float NULL,
	BatchNumber varchar(100) NULL,
	DealerCode varchar(100) NULL,
	ChassisModel varchar(50) NULL,
	ChassisNumberRegister varchar(50) NULL,
	ConsumptionTax1Amount money NULL,
	ConsumptionTax1 varchar(100) NULL,
	ConsumptionTax2Amount money NULL,
	ConsumptionTax2 varchar(100) NULL,
	DiscountAmount money NULL,
	EngineNumber varchar(50) NULL,
	EventData varchar(1000) NULL,
	InventoryUnit varchar(100) NULL,
	KeyNumber varchar(50) NULL,
	LandedCost money NULL,
	Location varchar(100) NULL,
	ProductDescription varchar(100) NULL,
	Product varchar(100) NOT NULL,
	ProductVolume float NULL,
	ProductWeight float NULL,
	PurchaseUnit varchar(100) NOT NULL,
	ReceivedQuantity float NOT NULL,
	ReferenceNumber varchar(50) NULL,
	ReturnPRDetail varchar(100) NULL,
	ServicePartsAndMaterial varchar(100) NULL,
	Site varchar(100) NOT NULL,
	StockNumber varchar(100) NULL,
	TitleRegistrationFee money NULL,
	TotalAmount money NULL,
	TotalBaseAmount money NULL,
	TotalConsumptionTaxAmount money NULL,
	TotalVolume float NULL,
	TotalWeight float NULL,
	TransactionAmount money NULL,
	UnitCost money NOT NULL,
	Warehouse varchar(100) NOT NULL,
	RowStatus smallint NULL,
	CreatedBy varchar(100) NULL,
	CreatedTime datetime NULL,
	LastUpdateBy varchar(100) NULL,
	LastUpdateTime datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.SparePartPRDetailFromVendor ADD CONSTRAINT
	PK_SparePartPRDetailFromVendor PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SparePartPRDetailFromVendor ADD CONSTRAINT
	[FK_SparePartPRDetailFromVendor[many]]_SparePartPRFromVendor[one]]] FOREIGN KEY
	(
	SparePartPRID
	) REFERENCES dbo.SparePartPRFromVendor
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SparePartPRDetailFromVendor SET (LOCK_ESCALATION = TABLE)
GO
COMMIT