SELECT 
/**PagingIndexQuery**/
	ID,
	Category,
	ValueId,
	ValueCode,
	ValueDesc,
	Sequence,
	RowStatus,
	CreatedBy,
	CreatedTime,
	LastUpdateBy,
	LastUpdateTime
	/**EndPagingIndexQuery**/
FROM StandardCode
WHERE
	@Keyword = '' OR
	ValueCode LIKE '%'+@Keyword+'%' OR
	ValueDesc LIKE '%'+@Keyword+'%' OR
	Category LIKE '%'+@Keyword+'%'
	

