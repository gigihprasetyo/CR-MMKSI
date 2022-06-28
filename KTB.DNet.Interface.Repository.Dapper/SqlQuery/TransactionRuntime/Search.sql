
SELECT 
    /**PagingIndexQuery**/
       [Id]
      ,[TransactionLogId]
      ,[MethodName]
      ,[StartedTime]
      ,[FinishedTime]
      ,[CreatedBy]
      ,[CreatedTime]
      ,[UpdatedBy]
      ,[UpdatedTime]
    /**EndPagingIndexQuery**/
FROM [dbo].[TransactionRuntime] WITH(NOLOCK)
WHERE (@MethodName = '' OR (str(TransactionLogId) + MethodName) LIKE '%' + @MethodName + '%')
