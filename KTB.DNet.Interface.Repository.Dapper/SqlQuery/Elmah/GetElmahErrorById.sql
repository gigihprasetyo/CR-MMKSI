
SELECT 
    /**PagingIndexQuery**/
	   [ErrorId]
      ,[Application]
      ,[Host]
      ,[Type]
      ,[Source]
      ,[Message]
      ,[User]
      ,[StatusCode]
      ,[TimeUtc]
      ,[Sequence]
      ,[AllXml]
	  /**EndPagingIndexQuery**/
FROM [dbo].[ELMAH_Error]
WHERE ErrorId = @Id
