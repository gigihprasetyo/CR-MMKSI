﻿SELECT [Id]
    ,[Name]
    ,[PermissionCode]
    ,[URI]
	,[EndpointGroup]
    ,[EndpointType]
    ,[OperationType]
    ,[Description]
    ,[IsScheduled]
    ,[IsLogEnabled]
    ,[IsRuntimeLogEnabled]
    ,[CreatedBy]
    ,[CreatedTime]
    ,[UpdatedBy]
    ,[UpdatedTime]
FROM [APIEndpointPermission]
WHERE Id = @Id