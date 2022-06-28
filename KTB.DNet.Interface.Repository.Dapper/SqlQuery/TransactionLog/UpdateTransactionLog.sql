UPDATE [TransactionLog]
   SET [SenderIP] = @SenderIP
      ,[Username] = @Username
      ,[Token] = @Token
      ,[Endpoint] = @Endpoint
      ,[Input] = @Input
      ,[Output] = @Output
      ,[Status] = @Status
      ,[ParentId] = @ParentId
      ,[DealerCode] = @DealerCode
      ,[ClientId] = @ClientId
      ,[AppId] = @AppId
      ,[UpdatedBy] = @UpdatedBy
      ,[UpdatedTime] = @UpdatedTime
 WHERE Id = @Id

