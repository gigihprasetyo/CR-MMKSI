
SELECT /**PagingIndexQuery**/ErrorId,Application,Host,Type,Source,Message,[User],StatusCode,TimeUtc,Sequence,AllXml/**EndPagingIndexQuery**/
FROM [ELMAH_Error]
WHERE 
(@Application = '' OR lower(Application) = lower(@Application)) AND
(@AllXml = '' OR AllXml LIKE '%'+@AllXml+'%') AND
(@Host = '' OR Host LIKE '%'+@Host+'%') AND
(@Type = '' OR Type LIKE '%'+@Type+'%') AND
(@Message = '' OR Message LIKE '%'+@Message+'%') 
