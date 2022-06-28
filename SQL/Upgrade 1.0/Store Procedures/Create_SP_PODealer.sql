/****** Object:  Stored Procedure [dbo].[up_RetrievePODealer]    Script Date: 07 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrievePODealer]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrievePODealer]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrievePODealerList]    Script Date: 07 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrievePODealerList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrievePODealerList]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrievePODealer
	@DealerID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[DealerID],
	[DealerCode],
	[DealerName],
	[POHeaderID],
	[PONumber],
	[AllocQty],
	[Price],
	[Discount],
	[Interest],
	[ContractNumber],
	[PKNumber],
	[DealerPKNumber],
	[ProjectName],
	[SalesOrderID],
	[SONumber],
	[SODate],
	[PaymentRef],
	[SOType],
	[LastUpdateTime],
	[VehicleColorID],
	[BasePrice],
	[OptionPrice],
	[DiscountBeforeTax],
	[NetPrice],
	[TotalHarga],
	[PPN],
	[TotalHargaPPN],
	[TotalHargaPP]	
FROM	[dbo].[VWI_PODealer]

WHERE
	[DealerID] = @DealerID

SET NOCOUNT OFF

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrievePODealerList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[DealerID],
		[DealerCode],
		[DealerName],
		[POHeaderID],
		[PONumber],
		[AllocQty],
		[Price],
		[Discount],
		[Interest],
		[ContractNumber],
		[PKNumber],
		[DealerPKNumber],
		[ProjectName],
		[SalesOrderID],
		[SONumber],
		[SODate],
		[PaymentRef],
		[SOType],
		[LastUpdateTime],
		[VehicleColorID],
		[BasePrice],
		[OptionPrice],
		[DiscountBeforeTax],
		[NetPrice],
		[TotalHarga],
		[PPN],
		[TotalHargaPPN],
		[TotalHargaPP]		
		FROM	
		[dbo].[VWI_PODealer] 

SET NOCOUNT OFF

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
GRANT  EXECUTE  ON [dbo].up_RetrievePODealer TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrievePODealerList TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



