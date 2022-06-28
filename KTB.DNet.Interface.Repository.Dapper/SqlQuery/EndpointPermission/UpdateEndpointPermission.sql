UPDATE [APIEndpointPermission]
   SET [Name] = @Name
      ,[PermissionCode] = @PermissionCode
      ,[URI] = @URI
	  ,[EndpointGroup] = @EndpointGroup
      ,[EndpointType] = @EndpointType
      ,[OperationType] = @OperationType
      ,[Description] = @Description
      ,[IsScheduled] = @IsScheduled
      ,[IsLogEnabled] = @IsLogEnabled
      ,[IsRuntimeLogEnabled] = @IsRuntimeLogEnabled 
      ,[CreatedBy] = @CreatedBy
      ,[CreatedTime] = @CreatedTime
      ,[UpdatedBy] = @UpdatedBy
      ,[UpdatedTime] = @UpdatedTime
 WHERE Id = @Id


