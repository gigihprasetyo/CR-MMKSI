UPDATE StandardCodeChar
   SET 
	Category = @Category,
	ValueId = @ValueId,
	ValueCode = @ValueCode,
	ValueDesc = @ValueDesc,
	Sequence = @Sequence,
	RowStatus = @RowStatus,
	CreatedBy = @CreatedBy,
	CreatedTime = @CreatedTime,
	LastUpdateBy = @LastUpdateBy,
	LastUpdateTime = @LastUpdateTime
 WHERE ID = @ID


 


