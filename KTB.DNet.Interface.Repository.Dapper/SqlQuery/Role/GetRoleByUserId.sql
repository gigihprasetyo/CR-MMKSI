SELECT
   a.* 
FROM
   APIRole a 
   INNER JOIN
      APIUserRole b 
      ON a.Id = b.RoleId 
WHERE
   b.UserId = @UserId