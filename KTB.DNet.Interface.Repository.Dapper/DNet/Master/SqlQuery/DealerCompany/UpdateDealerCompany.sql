UPDATE DealerCompany
   SET 
	DealerCompanyCode = @DealerCompanyCode,
	DealerCompanyName = @DealerCompanyName,
	DealerGroupID = @DealerGroupID,
	
	RowStatus = @RowStatus,
	LastUpdateBy = @LastUpdateBy,
	LastUpdateTime = @LastUpdateTime
 WHERE ID = @ID


 


