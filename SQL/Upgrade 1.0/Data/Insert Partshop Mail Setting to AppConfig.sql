USE [BSIDNET_MMKSI_DMS]

BEGIN TRAN
BEGIN
INSERT INTO [dbo].[AppConfig] ([Name],[Value],[AppID],[Status],[RowStatus],[CreatedBy],[CreatedTime])
     VALUES('SMTP' ,'admin.d-net@ktb.co.id','KTB.DNet.WebApi',0,0,'Admin',getdate());
INSERT INTO [dbo].[AppConfig] ([Name],[Value],[AppID],[Status],[RowStatus],[CreatedBy],[CreatedTime])
     VALUES('EmailFrom' ,'admin.d-net@ktb.co.id','KTB.DNet.WebApi',0,0,'Admin',getdate());
INSERT INTO [dbo].[AppConfig] ([Name],[Value],[AppID],[Status],[RowStatus],[CreatedBy],[CreatedTime])
     VALUES('EmailSPAdmin' ,'su.D-net@bsi.co.id','KTB.DNet.WebApi',0,0,'Admin',getdate());
END
COMMIT TRAN
