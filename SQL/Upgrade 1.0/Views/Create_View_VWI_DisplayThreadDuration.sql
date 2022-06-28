USE [BSIDNET_MMKSI_DMS_LOG]
GO

/****** Object:  View [dbo].[VWI_DisplayThreadDuration]    Script Date: 29/03/2018 15:52:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create View [dbo].[VWI_DisplayThreadDuration]
as 
select 
b.Id,
a.MethodName as 'Method',
c.UrlEndPoint as 'EndPoint',
c.Status as 'Status',
c.CreatedBy as 'Actor', 
a.DateCreated as 'Start',
b.DateCreated as 'End',
DATEDIFF(ms, a.DateCreated, b.DateCreated) as 'Duration(in miliseconds)'
from
[dbo].ThreadLog a inner join [dbo].ThreadLog b on a.Id = b.ParentId
inner join [dbo].TransactionLog c on b.LogId = c.Id
GO


