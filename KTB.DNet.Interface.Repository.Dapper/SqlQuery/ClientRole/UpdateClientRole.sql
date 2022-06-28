UPDATE [APIClientRole]
   SET [ClientId] = @ClientId,
       [RoleId] = @RoleId, 
       [CreatedBy] = @CreatedBy, 
       [CreatedTime] = @CreatedTime, 
       [UpdatedBy] = @UpdatedBy, 
       [UpdatedTime] = @UpdatedTime
 WHERE Id = @Id
