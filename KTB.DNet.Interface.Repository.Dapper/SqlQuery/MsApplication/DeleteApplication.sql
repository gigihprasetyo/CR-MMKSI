DELETE FROM [MsApplicationPermission]
      WHERE AppId = @AppId
      
DELETE FROM [MsApplication]
      WHERE AppId = @AppId