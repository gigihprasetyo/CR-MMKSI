/*
SELECT *
FROM dbo.fn_GetPaymentHdrIDByChassisNo('RF18000016')
-- =============================================*/

CREATE FUNCTION [dbo].[fn_GetPaymentHdrIDByChassisNo]
    (
      @ChassisNo VARCHAR(50)=null 
    )
RETURNS @Result TABLE
    (
      -- Add the column definitions for the TABLE variable here
      ID INT PRIMARY KEY CLUSTERED ,
      [ChassisNumber] VARCHAR(20)
    )
AS
    BEGIN
	-- Fill the table variable with the rows for your result set

        INSERT  INTO @Result
                ( ID ,
                  [ChassisNumber]
                )

                SELECT  X.ID ,
                        X.[ChassisNumber]
                FROM    (
							select distinct a.ID, d.ChassisNumber
							from 
								RevisionPaymentHeader a (nolock)
									inner join 
								RevisionPaymentDetail b (nolock) on a.id = b.RevisionPaymentHeaderID
									inner join 
								RevisionFaktur c (nolock) on b.RevisionFakturID = c.ID
									inner join 
								ChassisMaster d (nolock) on c.ChassisMasterID = d.ID
                        ) X
                WHERE   1 = 1
                        AND 
						ChassisNumber = @ChassisNo

        RETURN 
    END

