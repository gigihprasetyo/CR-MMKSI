SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE FSCampaignDealer DROP
CONSTRAINT [FK_FSCampaignDealer[many]]_FSCampaign[one]]] 
GO

ALTER TABLE FSCampaignKind DROP
CONSTRAINT [FK_FSCampaignKind[many]]_FSCampaign[one]]] 
GO

ALTER TABLE FSCampaignVehicle DROP
CONSTRAINT [FK_FSCampaignVehicle[many]]_FSCampaign[one]]] 
GO

EXEC sp_rename
	'dbo.PK_FSCampaign' ,
	'tmp__PK_FSCampaign' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.FSCampaign' ,
	'tmp__FSCampaign_0' ,
	'OBJECT'
GO

CREATE TABLE FSCampaign
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_FSCampaign PRIMARY KEY ,
		 Description NVARCHAR(100) ,
		 ErrMessage NVARCHAR(100) ,
		 DateFrom DATETIME ,
		 DateTo DATETIME ,
		 DealerChecked BIT ,
		 FSTypeChecked BIT ,
		 VehicleTypeChecked BIT ,
		 FakturDateChecked BIT ,
		 Status SMALLINT ,
		 ExtendedFleetChecked BIT ,
		 RetailChecked BIT ,
		 OpenFakturDateFrom DATETIME ,
		 OpenFakturDateTo DATETIME ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateTime DATETIME ,
		 LastUpdateBy VARCHAR(20)
	   )
GO

GRANT SELECT ON FSCampaign TO ccUser
GO

SET IDENTITY_INSERT FSCampaign ON
GO

INSERT	INTO FSCampaign
		(
		  ID ,
		  Description ,
		  ErrMessage ,
		  DateFrom ,
		  DateTo ,
		  DealerChecked ,
		  FSTypeChecked ,
		  VehicleTypeChecked ,
		  FakturDateChecked ,
		  Status ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateTime ,
		  LastUpdateBy
		)
SELECT	id ,
		description ,
		ErrMessage ,
		DateFrom ,
		DateTo ,
		DealerChecked ,
		FSTypeChecked ,
		VehicleTypeChecked ,
		FakturDateChecked ,
		Status ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateTime ,
		LastUpdateBy
FROM	tmp__FSCampaign_0
GO

SET IDENTITY_INSERT FSCampaign OFF
GO

DROP TABLE tmp__FSCampaign_0
GO

ALTER TABLE FSCampaignVehicle ADD
CONSTRAINT [FK_FSCampaignVehicle[many]]_FSCampaign[one]]] FOREIGN KEY(CampaignID) REFERENCES FSCampaign(ID)
GO

ALTER TABLE FSCampaignKind ADD
CONSTRAINT [FK_FSCampaignKind[many]]_FSCampaign[one]]] FOREIGN KEY(CampaignID) REFERENCES FSCampaign(ID)
GO

ALTER TABLE FSCampaignDealer ADD
CONSTRAINT [FK_FSCampaignDealer[many]]_FSCampaign[one]]] FOREIGN KEY(CampaignID) REFERENCES FSCampaign(ID)
GO

COMMIT
GO


