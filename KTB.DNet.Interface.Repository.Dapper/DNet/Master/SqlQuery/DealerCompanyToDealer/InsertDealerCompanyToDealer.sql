

INSERT INTO DealerCompanyToDealer
(
	DealerCompanyID,
	DealerID,
	RowStatus,
	CreatedBy,
	CreatedTime,
	LastUpdateBy,
	LastUpdateTime
)
OUTPUT INSERTED.ID
VALUES
(
	@DealerCompanyID,
	@DealerID,
	@RowStatus,
	@CreatedBy,
	@CreatedTime,
	@LastUpdateBy,
	@LastUpdateTime
);  


