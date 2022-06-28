UPDATE 
TransactionRuntime SET 
TransactionLogId = @transactionLogId, 
MethodName = @methodName, 
StartedTime =  @startedTime, 
FinishedTime =  @finishedTime,
UpdatedBy = @updatedBy, 
UpdatedTime = @updatedTime 
WHERE Id = @id