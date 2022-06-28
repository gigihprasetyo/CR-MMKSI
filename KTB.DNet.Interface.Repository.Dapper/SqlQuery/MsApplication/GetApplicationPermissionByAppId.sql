SELECT [Id]
      ,[AppId]
      ,[PermissionId]
      ,[CreatedBy]
      ,[CreatedTime]
      ,[UpdatedBy]
      ,[UpdatedTime]
  FROM [MsApplicationPermission]
WHERE AppId = @AppId


