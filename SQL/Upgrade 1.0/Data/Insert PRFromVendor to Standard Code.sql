

USE [BSIDNET_MMKSI_DMS]
GO

BEGIN TRAN
insert into StandardCode values('PRFromVendorHandling', 1, 'No Action', 0, 'Mitrais Team', GETDATE(), NULL, NULL, NULL);

insert into StandardCode values('PRFromVendorHandling', 2, 'Release', 0, 'Mitrais Team', GETDATE(), NULL, NULL, NULL);

insert into StandardCode values('PRFromVendorState', 1, 'Open', 0, 'Mitrais Team', GETDATE(), NULL, NULL, NULL);

insert into StandardCode values('PRFromVendorState', 2, 'Partial Release', 0, 'Mitrais Team', GETDATE(), NULL, NULL, NULL);

insert into StandardCode values('PRFromVendorState', 3, 'Release', 0, 'Mitrais Team', GETDATE(), NULL, NULL, NULL);

insert into StandardCode values('PRFromVendorType', 1, 'Receipt', 0, 'Mitrais Team', GETDATE(), NULL, NULL, NULL);

insert into StandardCode values('PRFromVendorType', 2, 'Return', 0, 'Mitrais Team', GETDATE(), NULL, NULL, NULL);
COMMIT