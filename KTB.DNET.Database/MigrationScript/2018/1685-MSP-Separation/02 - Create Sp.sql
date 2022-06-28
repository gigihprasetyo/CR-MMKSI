set xact_abort on
go

begin transaction
go

set ANSI_NULLS off
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, July 16, 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

create PROCEDURE up_RetrievePMKind_forMSP @KM INT
AS
    SET TRANSACTION ISOLATION LEVEL READ COMMITTED

    SET NOCOUNT ON
 

    SELECT TOP 1
            a.ID ,
            a.KindCode ,
            a.KM ,
            a.KindDescription ,
            a.RowStatus ,
            a.CreatedBy ,
            a.CreatedTime ,
            a.LastUpdateBy ,
            a.LastUpdateTime
    FROM    dbo.PMKind a
            INNER JOIN dbo.MSPDurationPMKind b ON a.KindCode = b.PMKindCode
    WHERE   1 = 1
            AND a.RowStatus = 0
            AND b.RowStatus = 0
            AND a.KM >= @KM
    ORDER BY a.KM ASC

    SET NOCOUNT OFF
go

commit
go


