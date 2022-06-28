UPDATE [APIEndpointPermission]
   SET [OperationType] = @OperationTypeId
   ,[UpdatedBy] = @UpdatedBy
   ,[UpdatedTime] = GetDate()
 WHERE Id IN @EndpointIdList


