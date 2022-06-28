SELECT 
	  cr.[Id]
      ,cr.[ClientId]
      ,cr.[RoleId]
      ,cr.[CreatedBy]
      ,cr.[CreatedTime]
      ,cr.[UpdatedBy]
      ,cr.[UpdatedTime]
	  ,c.[ClientId]
      ,c.[Name]
      ,c.[SecretKey]
      ,c.[AppId]
      ,c.[CreatedBy]
      ,c.[CreatedTime]
      ,c.[UpdatedBy]
      ,c.[UpdatedTime]
	  ,r.[Id]
      ,r.[Name]
      ,r.[Level]
      ,r.[CreatedBy]
      ,r.[CreatedTime]
      ,r.[UpdatedBy]
      ,r.[UpdatedTime]
  FROM [dbo].[APIClientRole] cr
  JOIN APIClient c on cr.ClientId = c.ClientId
  JOIN APIRole r on r.Id = cr.RoleId
WHERE cr.ClientId = @ClientId
