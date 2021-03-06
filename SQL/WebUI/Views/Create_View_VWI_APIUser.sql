--use BSIDNET_MMKSI_DMS_20180924_0100_1

create View VWI_APIUser
as 
select a.Id as Id,
	   a.UserName as UserName, 
	   a.PasswordHash as PasswordHash,
	   b.Id as ClientUserId,
	   e.Id as ClientRoleId,
	   g.Id as DealerId
from 
APIUser a inner join APIClientUser b on a.Id = b.UserId 
inner join APIClient  d on b.ClientId  = d.ClientId
inner join APIClientRole e on e.ClientId = d.ClientId
inner join Dealer g on a.DealerId = g.ID 
--where a.UserName = '100109@technosoft.com'