SELECT 
    /**PagingIndexQuery**/
	[Id]
      ,[Name]
      ,[Level]
      ,[CreatedBy]
      ,[CreatedTime]
      ,[UpdatedBy]
      ,[UpdatedTime]
      /**EndPagingIndexQuery**/
  FROM [dbo].[APIRole]
WHERE (@Keyword = '' OR lower(Name) like lower(@Keyword+'%'))

