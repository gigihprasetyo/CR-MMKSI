USE [BSIDNET_MMKSI_DMS]

INSERT INTO StandardCode 
(Category, ValueId, ValueDesc, RowStatus, CreatedBy, CreatedTime)
VALUES 
('EnumDNET.enumSAPCustomer', 5, 'No Prospect', 0, 'ADMIN', GETDATE()),
('EnumDNET.enumSAPCustomer', 6, 'Cancelled', 0, 'ADMIN', GETDATE());