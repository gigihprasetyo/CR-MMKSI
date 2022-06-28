
SELECT 
    /**PagingIndexQuery**/
    [Id]
      ,[Username]
      ,[Endpoint]
      ,[Activity]
      ,[ActivityTime]
      ,[ActivityDesc]
      ,[DealerCode]
      ,[AppId]
      ,[CreatedBy]
      ,[CreatedTime]
      ,[UpdatedBy]
      ,[UpdatedTime]
	/**EndPagingIndexQuery**/
  FROM [UserActivity]
WHERE ((@Keyword = '' ) 
	OR ((Username LIKE '%' + @Keyword + '%')
		OR (ActivityDesc LIKE '%' + @Keyword + '%')))
	AND (@DealerCode = '' OR lower(DealerCode) = lower(@DealerCode))

