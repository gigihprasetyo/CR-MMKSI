UPDATE  m
SET     StandKM = p.StandKM ,
        ReleaseDate = p.ReleaseDate ,
        ServiceDate = p.ServiceDate ,
        Remarks = p.Remarks ,
        PMKindID = p.PMKindID ,
        VisitType = p.VisitType ,
        ChassisNumberID = p.ChassisNumberID,
		CreatedBy = p.CreatedBy
FROM    dbo.MSPClaim m
        INNER JOIN dbo.PMHeader p ON m.PMHeaderID = p.ID


		
INSERT INTO [dbo].[AppConfig]
		(
		  [Name] ,
		  [Value] ,
		  [AppID] ,
		  [Status] ,
		  [RowStatus] ,
		  [CreatedBy] ,
		  [CreatedTime] ,
		  [LastUpdateBy] ,
		  [LastUpdateTime]
		)
VALUES	(
		  'MSPBackwardInput' , -- Name - varchar(250)
		  'False' , -- Value - varchar(2000)
		  '' , -- AppID - varchar(50)
		  0 , -- Status - smallint
		  0 , -- RowStatus - smallint
		  '' , -- CreatedBy - varchar(20)
		  GETDATE() , -- CreatedTime - datetime
		  '' , -- LastUpdateBy - varchar(20)
		  GETDATE()  -- LastUpdateTime - datetime
		)