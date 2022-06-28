DELETE FROM ELMAH_Error
WHERE 
	  TimeUtc >= @From AND 
	  TimeUtc < @To 


