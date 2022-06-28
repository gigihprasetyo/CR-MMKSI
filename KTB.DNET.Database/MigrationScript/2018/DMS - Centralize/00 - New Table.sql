set xact_abort on
go

begin transaction
go

create table SparePartConversion(
  ID                int         not null identity constraint PK_SparePartConversion primary key,
  SparePartMasterID int,
  UoMto             varchar(18),
  Qty               int,
  RowStatus         smallint,
  CreatedBy         varchar(20),
  CreatedTime       datetime,
  LastUpdateBy      varchar(20),
  LastUpdateTime    datetime
)
go

commit
go


