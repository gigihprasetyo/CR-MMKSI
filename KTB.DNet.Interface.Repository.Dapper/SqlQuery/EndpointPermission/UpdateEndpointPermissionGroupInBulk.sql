UPDATE [APIEndpointPermission]
   SET [EndpointGroup] = @EndpointGroupId
   ,[UpdatedBy] = @UpdatedBy
   ,[UpdatedTime] = GetDate()
 WHERE Id IN @EndpointIdList


