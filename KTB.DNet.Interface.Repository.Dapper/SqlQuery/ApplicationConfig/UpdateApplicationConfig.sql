UPDATE [ApplicationConfig]
   SET [Name] = @Name
      ,[ConfigKey] = @ConfigKey
      ,[Value] = @Value
	  ,[DataType] = @DataType
      ,[IsActive] = @IsActive
	  ,[Description] = @Description
      ,[UpdatedBy] = @UpdatedBy
      ,[UpdatedTime] = @UpdatedTime
 WHERE Id = @Id

