SELECT * FROM DealerBranch WITH (NOLOCK) 
WHERE RowStatus = 0 AND Status = 1 AND DealerID = @DealerID