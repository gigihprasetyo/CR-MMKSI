/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
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
ALTER TABLE dbo.TermOfPayment SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_SparePartPOTypeTOP
	(
	ID int NOT NULL IDENTITY (1, 1),
	SparePartPOType varchar(5) NULL,
	IsTOP bit NULL,
	TermOfPaymentIDNotTOP int NULL,
	RowStatus smallint NULL,
	CreatedBy varchar(20) NULL,
	CreatedTime datetime NULL,
	LastUpdateBy varchar(20) NULL,
	LastUpdateTime datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_SparePartPOTypeTOP SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_SparePartPOTypeTOP ON
GO
IF EXISTS(SELECT * FROM dbo.SparePartPOTypeTOP)
	 EXEC('INSERT INTO dbo.Tmp_SparePartPOTypeTOP (ID, SparePartPOType, IsTOP, RowStatus, CreatedBy, CreatedTime, LastUpdateBy, LastUpdateTime)
		SELECT ID, SparePartPOType, IsTOP, RowStatus, CreatedBy, CreatedTime, LastUpdateBy, LastUpdateTime FROM dbo.SparePartPOTypeTOP WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_SparePartPOTypeTOP OFF
GO
ALTER TABLE dbo.SparePartMasterTOP
	DROP CONSTRAINT [FK_SparePartMasterTOP[many]]_SparePartTypeTOP[one]]]
GO
DROP TABLE dbo.SparePartPOTypeTOP
GO
EXECUTE sp_rename N'dbo.Tmp_SparePartPOTypeTOP', N'SparePartPOTypeTOP', 'OBJECT' 
GO
ALTER TABLE dbo.SparePartPOTypeTOP ADD CONSTRAINT
	PK_SparePartTypeTOP PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SparePartPOTypeTOP ADD CONSTRAINT
	[FK_SparePartPOTypeTOP[one]]_TermOfPayment[one]]] FOREIGN KEY
	(
	TermOfPaymentIDNotTOP
	) REFERENCES dbo.TermOfPayment
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SparePartMasterTOP ADD CONSTRAINT
	[FK_SparePartMasterTOP[many]]_SparePartTypeTOP[one]]] FOREIGN KEY
	(
	SparePartPOTypeTOPID
	) REFERENCES dbo.SparePartPOTypeTOP
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SparePartMasterTOP SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
