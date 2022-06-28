SELECT 
      COUNT(Id)
FROM APIUser usr 
WHERE @DealerId Is NULL OR DealerId = @DealerId



