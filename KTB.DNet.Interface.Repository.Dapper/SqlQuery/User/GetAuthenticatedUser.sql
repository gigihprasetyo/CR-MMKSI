
SELECT 
      u.Id
      ,u.FirstName
      ,u.LastName
      ,u.Street1
      ,u.Street2
      ,u.Street3
      ,u.City
      ,u.State
      ,u.PostalCode
      ,u.Country
      ,u.Company
      ,u.Status
      ,u.IsActive
      ,u.DealerId
      ,u.CreatedBy
      ,u.CreatedTime
      ,u.UpdatedBy
      ,u.UpdatedTime
      ,u.Email
      ,u.EmailConfirmed
      ,u.PasswordHash
      ,u.SecurityStamp
      ,u.PhoneNumber
      ,u.PhoneNumberConfirmed
      ,u.TwoFactorEnabled
      ,u.LockoutEndDateUtc
      ,u.LockoutEnabled
      ,u.AccessFailedCount
      ,u.UserName
      ,d.DealerCode
  FROM APIUser u 
  LEFT JOIN Dealer d ON d.Id = u.DealerId
  WHERE LOWER(u.UserName) = LOWER(@Username)
  AND u.IsActive = 1 -- 20191230 active user only
  AND (@DealerCode = '' OR DealerCode = @DealerCode)



