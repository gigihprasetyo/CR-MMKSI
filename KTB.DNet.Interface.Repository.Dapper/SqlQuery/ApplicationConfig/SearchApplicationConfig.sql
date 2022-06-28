
SELECT 
    /**PagingIndexQuery**/
    [Id]
    ,[Name]
	,[ConfigKey]
    ,[Value]
	,[DataType]
    ,[IsActive]
	,[Description]
    ,[CreatedBy]
    ,[CreatedTime]
    ,[UpdatedBy]
    ,[UpdatedTime]
	/**EndPagingIndexQuery**/
FROM [dbo].[ApplicationConfig]
WHERE 
	@Keyword = '' OR 
	Name LIKE '%'+@Keyword+'%' OR 
	ConfigKey LIKE '%'+@Keyword+'%'

