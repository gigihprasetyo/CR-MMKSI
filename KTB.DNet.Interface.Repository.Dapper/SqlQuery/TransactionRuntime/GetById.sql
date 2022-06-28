SELECT * FROM TransactionRuntime runtime WITH(NOLOCK) 
JOIN TransactionLog trxLog WITH(NOLOCK) ON runtime.TransactionLogId = trxLog.Id 
WHERE runtime.Id = @id