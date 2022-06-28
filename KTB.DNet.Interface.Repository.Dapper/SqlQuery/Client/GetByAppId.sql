SELECT
	c.ClientId,
	c.Name
FROM 
	APIClient c
WHERE c.AppId = @appId
