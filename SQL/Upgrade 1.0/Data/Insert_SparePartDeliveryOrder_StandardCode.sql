

INSERT INTO [dbo].[standardcode] 
            ([category],[valueid],[valuedesc],[rowstatus],[createdby], 
             [createdtime], 
             [lastupdateby],[lastupdatetime],[sequence]) 
VALUES      
('SparePartDeliveryOrder.DeliveryType',1,'Delivery',0,'ADMIN', '2018-03-21 15:01:16.960',NULL,NULL ,NULL ), 
('SparePartDeliveryOrder.DeliveryType',2,'Return',0,'ADMIN', '2018-03-21 15:01:16.960',NULL,NULL ,NULL ),
('SparePartDeliveryOrder.Handling',1,'No Action',0,'ADMIN', '2018-03-21 15:01:16.960',NULL,NULL ,NULL ),
('SparePartDeliveryOrder.Handling',2,'Release',0,'ADMIN', '2018-03-21 15:01:16.960',NULL,NULL ,NULL ),
('SparePartDeliveryOrder.Handling',3,'Cancel',0,'ADMIN', '2018-03-21 15:01:16.960',NULL,NULL ,NULL ),
('SparePartDeliveryOrder.Handling',4,'Invoice',0,'ADMIN', '2018-03-21 15:01:16.960',NULL,NULL ,NULL ),
('SparePartDeliveryOrder.State',1,'Open',0,'ADMIN', '2018-03-21 15:01:16.960',NULL,NULL ,NULL ),
('SparePartDeliveryOrder.State',2,'Released',0,'ADMIN', '2018-03-21 15:01:16.960',NULL,NULL ,NULL ),
('SparePartDeliveryOrder.State',3,'Canceled',0,'ADMIN', '2018-03-21 15:01:16.960',NULL,NULL ,NULL ),
('SparePartDeliveryOrder.State',4,'Invoiced',0,'ADMIN', '2018-03-21 15:01:16.960',NULL,NULL ,NULL )