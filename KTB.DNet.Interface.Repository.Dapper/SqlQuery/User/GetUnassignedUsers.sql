
SELECT DISTINCT
      usr.Id
      ,usr.UserName
  FROM APIUser usr
  JOIN APIClientUser clientUser ON usr.Id = clientUser.UserId
  WHERE clientUser.ClientId != @ClientId



