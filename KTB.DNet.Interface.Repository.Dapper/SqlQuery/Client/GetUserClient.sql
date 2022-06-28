SELECT
	c.ClientId,
	c.Name
FROM 
	APIClientUser cu
	JOIN APIClient c ON c.ClientId = cu.ClientId
WHERE cu.UserId = @userId
