select TOP 1 Id, Endpoint, Status, CreatedBy, CreatedTime, UpdatedBy, UpdatedTime  FROM TransactionLog (NOLOCK) WHERE ParentID IS NOT NULL
AND CreatedTime BETWEEN DATEADD(DAY, -7, GETDATE()) AND GETDATE()
--check query data parent already exist or not, if not we can skip query checking parentid