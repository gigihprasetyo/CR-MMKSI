set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

--====================================EXECUTE FUNCTION =====================================================================================================

alter FUNCTION ufn_CreatePartShopCode ( @CityPartID AS INT )
RETURNS VARCHAR(10)
AS
    BEGIN 

        DECLARE @CityCode AS VARCHAR(4)
        DECLARE @ICode AS INT = 0
        DECLARE @MaxPartShopCode AS VARCHAR(2)
        DECLARE @PartShopCode AS VARCHAR(6)
        DECLARE @return VARCHAR(10)

        SELECT  @CityCode = CityCode
        FROM    CityPart
        WHERE   ID = @CityPartID 

        SELECT  @ICode = ISNULL(CONVERT(INTEGER, RIGHT(PartShopCode, 4)), 0)
        FROM    PartShop
        WHERE   LEFT(PartShopCode, 4) = @CityCode 
	
        SET @ICode = @ICode + 1 
        SELECT  @return = (@CityCode + RIGHT(( POWER(10, 4) + @ICode ), 4))
	
        RETURN @return

	  --SELECT  @CityCode = CityCode
	  --FROM	  CityPart
	  --WHERE	  ID = @CityPartID
	  --SET @ICode = ( SELECT	ISNULL(MAX(RIGHT(PartShopCode, 2)), 0)
			--		 FROM	PartShop
			--		 WHERE	CityPartID = @CityPartID
			--		 AND RowStatus=0
			--	   )
	  --SET @MaxPartShopCode = CAST(( CAST(@ICode AS INTEGER) + 1 ) AS VARCHAR(2))			
	  --SET @MaxPartShopCode = REPLICATE('0', 2 - LEN(@MaxPartShopCode))
		 -- + @MaxPartShopCode
	  --SET @PartShopCode = @CityCode + CONVERT(VARCHAR(2), @MaxPartShopCode)

	  --RETURN @PartShopCode

    END
go

commit
go


