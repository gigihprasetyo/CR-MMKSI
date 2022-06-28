INSERT INTO [TransactionLog]
           ([SenderIP],[Username],[Token],[Endpoint],[Input]
           ,[Output],[Status],[ParentId],[DealerCode],[ClientId]
           ,[AppId],[CreatedBy],[CreatedTime],[UpdatedBy]
           ,[UpdatedTime])
OUTPUT INSERTED.Id
VALUES
           (@SenderIP,@Username,@Token,@Endpoint,@Input
           ,@Output,@Status,@ParentId,@DealerCode,@ClientId
           ,@AppId,@CreatedBy,@CreatedTime,@UpdatedBy
           ,@UpdatedTime)