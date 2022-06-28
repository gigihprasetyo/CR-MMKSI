/****
	- add new field Hnadphone varchar(20)
	- 12 Jul 2018
	- Mitrais Team
****/

USE [BSIDNET_MMKSI_DMS_20180605_0100]
GO
/****** Object:  StoredProcedure [dbo].[up_InsertFleetCustomerContact]    Script Date: 12/07/2018 10:26:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_InsertFleetCustomerContact]
	@ID int OUTPUT,
	@FleetCustomerID int,
	@Name varchar(50),
	@Position varchar(50),
	@PhoneNo varchar(20),
	@Handphone varchar(20),
	@Email nvarchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(50),
	--@CreatedTime datetime,
	@LastUpdatedBy varchar(50)
	
AS
INSERT	INTO	[dbo].[FleetCustomerContact]
VALUES
(
	@FleetCustomerID,
	@Name,
	@Position,
	@PhoneNo,
	@Handphone,
	@Email,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdatedBy,
	GETDATE())

	
SET @ID = @@IDENTITY




/*------------------------------------------------------------------------------------------------------------------*/

/****** Object:  StoredProcedure [dbo].[up_RetrieveFleetCustomerContact]    Script Date: 12/07/2018 10:27:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_RetrieveFleetCustomerContact]
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[FleetCustomerID],
	[Name],
	[Position],
	[PhoneNo],
	[Handphone],
	[Email],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdatedBy],
	[LastUpdatedTime]	
FROM	[dbo].[FleetCustomerContact]

WHERE
	[ID] = @ID

SET NOCOUNT OFF




/*------------------------------------------------------------------------------------------------------------------*/
/****** Object:  StoredProcedure [dbo].[up_RetrieveFleetCustomerContactList]    Script Date: 12/07/2018 10:27:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_RetrieveFleetCustomerContactList]

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[FleetCustomerID],
		[Name],
		[Position],
		[PhoneNo],
		[Handphone],
		[Email],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdatedBy],
		[LastUpdatedTime]		
		FROM	
		[dbo].[FleetCustomerContact] 

SET NOCOUNT OFF




/*------------------------------------------------------------------------------------------------------------------*/
/****** Object:  StoredProcedure [dbo].[up_UpdateFleetCustomerContact]    Script Date: 12/07/2018 10:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_UpdateFleetCustomerContact]
	@ID int OUTPUT,
	@FleetCustomerID int,
	@Name varchar(50),
	@Position varchar(50),
	@PhoneNo varchar(20),
	@Handphone varchar(20),
	@Email nvarchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(50),
	--@CreatedTime datetime,
	@LastUpdatedBy varchar(50)
	
AS

UPDATE	[dbo].[FleetCustomerContact]
SET
	[FleetCustomerID] = @FleetCustomerID,
	[Name] = @Name,
	[Position] = @Position,
	[PhoneNo] = @PhoneNo,
	[Handphone] = @Handphone,
	[Email] = @Email,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdatedBy] = @LastUpdatedBy,
	[LastUpdatedTime] = GETDATE()
WHERE
	[ID] = @ID





/*------------------------------------------------------------------------------------------------------------------*/
/****** Object:  StoredProcedure [dbo].[up_ValidateFleetCustomerContact]    Script Date: 12/07/2018 10:28:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_ValidateFleetCustomerContact]
	@Result	varchar(1000),
	@ID int OUTPUT,
	@FleetCustomerID int,
	@Name varchar(50),
	@Position varchar(50),
	@PhoneNo varchar(20),
	@Handphone varchar(20),
	@Email nvarchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(50),
	@CreatedTime datetime,
	@LastUpdatedBy varchar(50),
	@LastUpdatedTime datetime	
AS

SET	@Result = ''