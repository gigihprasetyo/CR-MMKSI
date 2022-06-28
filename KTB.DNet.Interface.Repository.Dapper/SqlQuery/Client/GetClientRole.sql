SELECT
	r.Id,
	r.Name,
	r.[Level]
FROM 
	APIClientRole cr
	JOIN APIRole r ON r.Id = cr.RoleId
WHERE cr.ClientId = @clientId
