﻿
SELECT [Id]
      ,[ClientRoleId]
      ,[PermissionId]
      ,[CreatedBy]
      ,[CreatedTime]
      ,[UpdatedBy]
      ,[UpdatedTime]
  FROM [APIRolePermission] 
  WHERE ClientRoleId = @ClientRoleId