/*
   Friday, 2 March 201810:40:45 AM
   User: dms_user
   Server: 172.17.31.122
   Database: BSIDNET_MMKSI_DMS
   Application: 
   Revision: 
			change BenefitMasterHeaderID to CampaignName
			change IndustrialSectorID to BusinessSectordetailID
*/

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
ALTER TABLE dbo.BusinessSectorDetail SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SAPCustomer
	DROP CONSTRAINT [FK_SAPCustomer[many]]_IndustrialSector[one]]]
GO
ALTER TABLE dbo.IndustrialSector SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SAPCustomer
	DROP CONSTRAINT [FK_SAPCustomer[many]]_BenefitMasterHeader[one]]]
GO
ALTER TABLE dbo.BenefitMasterHeader SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SAPCustomer ADD
	CampaignName varchar(50) NULL,
	BusinessSectorDetailID int NULL
GO
ALTER TABLE dbo.SAPCustomer ADD CONSTRAINT
	[FK_SAPCustomer[many]]_BusinessSectorDetail[one]]] FOREIGN KEY
	(
	BusinessSectorDetailID
	) REFERENCES dbo.BusinessSectorDetail
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SAPCustomer
	DROP COLUMN BenefitMasterHeaderID, IndustrialSectorID
GO
ALTER TABLE dbo.SAPCustomer SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
