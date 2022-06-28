SELECT  es.[Id]
    ,es.[EndpointId]
    ,es.[CreatedBy]
    ,es.[CreatedTime]
    ,es.[UpdatedBy]
    ,es.[UpdatedTime]
    ,es.[ScheduleId]
	,e.[Id]
      ,e.[Name]
      ,e.[PermissionCode]
      ,e.[URI]
      ,e.[EndpointType]
      ,e.[OperationType]
      ,e.[Description]
      ,e.[IsScheduled]
      ,e.[CreatedBy]
      ,e.[CreatedTime]
      ,e.[UpdatedBy]
      ,e.[UpdatedTime]
  FROM [dbo].[APIEndpointSchedule] es
  INNER JOIN [APIEndpointPermission] e on e.Id = es.EndpointId
  WHERE e.URI = @EndpointUrl

