SELECT 
	[Id]
    ,[Name]
	,[ConfigKey]
    ,[Value]
	,[DataType]
    ,[IsActive]
	,[Description]
    ,[CreatedBy]
    ,[CreatedTime]
    ,[UpdatedBy]
    ,[UpdatedTime]
FROM [ApplicationConfig]
WHERE ConfigKey = @ConfigKey