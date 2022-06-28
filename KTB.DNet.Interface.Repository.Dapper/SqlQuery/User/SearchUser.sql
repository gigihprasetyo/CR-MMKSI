
SELECT 
 /**PagingIndexQuery**/
      usr.Id
      ,usr.FirstName
      ,usr.LastName
      ,usr.Street1
      ,usr.Street2
      ,usr.Street3
      ,usr.City
      ,usr.State
      ,usr.PostalCode
      ,usr.Country
      ,usr.Company
      ,usr.Status
      ,usr.IsActive
      ,usr.DealerId
      ,usr.CreatedBy
      ,usr.CreatedTime
      ,usr.UpdatedBy
      ,usr.UpdatedTime
      ,usr.Email
      ,usr.EmailConfirmed
      ,usr.PasswordHash
      ,usr.SecurityStamp
      ,usr.PhoneNumber
      ,usr.PhoneNumberConfirmed
      ,usr.TwoFactorEnabled
      ,usr.LockoutEndDateUtc
      ,usr.LockoutEnabled
      ,usr.AccessFailedCount
      ,usr.UserName
      /**EndPagingIndexQuery**/
  FROM APIUser usr 
  WHERE
  (@DealerId Is NULL OR @DealerId = usr.DealerId) AND
  (@Keyword = '' OR usr.UserName LIKE '%'+@Keyword+'%' OR usr.Email LIKE '%'+@Keyword+'%')



