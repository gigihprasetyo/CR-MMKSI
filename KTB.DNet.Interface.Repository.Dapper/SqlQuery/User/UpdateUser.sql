
UPDATE APIUser SET
    FirstName = @FirstName,
    LastName = @LastName,
    PhoneNumber = @PhoneNumber,
    Email = @Email,
    Street1 = @Street1,
    Street2 = @Street2,
    Street3 = @Street3,
    City = @City,
    State = @State,
    PostalCode = @PostalCode,
    Country = @Country,
    Company = @Company,
    Status = @Status,
    DealerId = @DealerId,
    IsActive = @IsActive,
    UserName = @UserName,
    UpdatedBy = @UpdatedBy,
    UpdatedTime = @UpdatedTime,
    PasswordHash = @PasswordHash
WHERE 
    Id = @Id




