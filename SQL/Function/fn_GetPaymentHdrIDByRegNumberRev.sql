/*
SELECT *
FROM dbo.fn_GetPaymentHdrIDByRegNumberRev('RF18000016')
-- =============================================*/


CREATE FUNCTION [dbo].[fn_GetPaymentHdrIDByRegNumberRev]
    (
      @RegNumberRevision VARCHAR(50)=null 
    )
RETURNS @Result TABLE
    (
      -- Add the column definitions for the TABLE variable here
      ID INT PRIMARY KEY CLUSTERED ,
      [RegNumber] VARCHAR(20)
    )
AS
    BEGIN
	-- Fill the table variable with the rows for your result set

        INSERT  INTO @Result
                ( ID ,
                  [RegNumber]
                )

                SELECT  X.ID ,
                        X.[RegNumber]
                FROM    ( select distinct a.ID, c.RegNumber
							from 
								RevisionPaymentHeader a (nolock)
									inner join 
								RevisionPaymentDetail b (nolock) on a.id = b.RevisionPaymentHeaderID
									inner join 
								RevisionFaktur c (nolock) on b.RevisionFakturID = c.ID
                        ) X
                WHERE   1 = 1
                        AND 
						RegNumber = @RegNumberRevision

        RETURN 
    END

