DELETE FROM TransactionLog
WHERE 
	  CreatedTime >= @From AND 
	  CreatedTime < @To AND
	  (@DealerCode = '' OR @DealerCode = DealerCode)


