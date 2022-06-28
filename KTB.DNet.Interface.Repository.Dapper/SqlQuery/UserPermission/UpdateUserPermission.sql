
UPDATE [APIUserPermission]
   SET [ClientUserId] = @ClientUserId
      ,[PermissionId] = @PermissionId
      ,[IsCustomPermission] = @IsCustomPermission
      ,[IsDismantledPermission] = @IsDismantledPermission
      ,[CreatedBy] = @CreatedBy
      ,[CreatedTime] = @CreatedTime
      ,[UpdatedBy] = @UpdatedBy
      ,[UpdatedTime] = @UpdatedTime
 WHERE Id = @Id


