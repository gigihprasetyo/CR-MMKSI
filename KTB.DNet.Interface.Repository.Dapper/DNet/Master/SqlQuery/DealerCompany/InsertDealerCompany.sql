

INSERT INTO DealerCompany
(
	DealerCompanyCode,
	DealerCompanyName,
	DealerGroupID,
	RowStatus,
	CreatedBy,
	CreatedTime,
	LastUpdateBy,
	LastUpdateTime
)
OUTPUT INSERTED.ID
VALUES
(
	@DealerCompanyCode,
	@DealerCompanyName,
	@DealerGroupID,
	@RowStatus,
	@CreatedBy,
	@CreatedTime,
	@LastUpdateBy,
	@LastUpdateTime
);  


