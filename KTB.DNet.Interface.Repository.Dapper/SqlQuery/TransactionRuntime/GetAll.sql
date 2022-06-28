SELECT * FROM TransactionRuntime runtime 
JOIN TransactionLog trxLog ON runtime.TransactionLogId = trxLog.Id 