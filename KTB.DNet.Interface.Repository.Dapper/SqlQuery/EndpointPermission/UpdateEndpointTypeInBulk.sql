UPDATE [APIEndpointPermission]
   SET [EndpointType] = @EndpointTypeId
   ,[UpdatedBy] = @UpdatedBy
   ,[UpdatedTime] = GetDate()
 WHERE Id IN @EndpointIdList


