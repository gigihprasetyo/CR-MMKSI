
SELECT 
	ID,
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
FROM StandardCode
WHERE ID = @Id