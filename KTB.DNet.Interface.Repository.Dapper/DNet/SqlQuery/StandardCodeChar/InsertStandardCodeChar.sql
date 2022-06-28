

INSERT INTO StandardCodeChar
(
	Category,
	ValueId,
	ValueCode,
	ValueDesc,
	Sequence,
	RowStatus,
	CreatedBy,
	CreatedTime,
	LastUpdateBy,
	LastUpdateTime
)
OUTPUT INSERTED.ID
VALUES
(
	@Category,
	@ValueId,
	@ValueCode,
	@ValueDesc,
	@Sequence,
	@RowStatus,
	@CreatedBy,
	@CreatedTime,
	@LastUpdateBy,
	@LastUpdateTime
);  


