
INSERT INTO [ApplicationConfig]
           ([Name]
		   ,[ConfigKey]
           ,[Value]
           ,[DataType]
		   ,[IsActive]
		   ,[Description]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[UpdatedBy]
           ,[UpdatedTime])
OUTPUT INSERTED.ID
VALUES
    (@Name
	,@ConfigKey
    ,@Value
	,@DataType
    ,@IsActive
	,@Description
    ,@CreatedBy
    ,@CreatedTime
    ,@UpdatedBy
    ,@UpdatedTime);  


