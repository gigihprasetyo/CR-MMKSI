/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


set xact_abort on
go

begin transaction
go

exec sp_rename 'dbo.PK_MSPClaim', 'tmp__PK_MSPClaim', 'OBJECT'
go

exec sp_rename 'dbo.MSPClaim', 'tmp__MSPClaim_0', 'OBJECT'
go

create table MSPClaim(
  ID                       int          not null identity constraint PK_MSPClaim primary key,
  DealerID                 int          not null,
  PMHeaderID               int,
  MSPRegistrationHistoryID int          not null,
  ClaimNumber              varchar(20)  not null,
  ClaimDate                datetime     not null,
  Status                   smallint     not null,
  ChassisNumberID          int,
  StandKM                  int,
  PMKindID                 int,
  VisitType                varchar(5),
  ServiceDate              datetime,
  ReleaseDate              datetime,
  Remarks                  varchar(250),
  RowStatus                smallint     not null,
  CreatedBy                varchar(100),
  CreatedTime              datetime,
  LastUpdateBy             varchar(100),
  LastUpdateTime           datetime
)
go

set identity_insert MSPClaim on
go

insert into MSPClaim(ID,DealerID,PMHeaderID,MSPRegistrationHistoryID,ClaimNumber,ClaimDate,Status,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime) select ID,DealerID,PMHeaderID,MSPRegistrationHistoryID,ClaimNumber,ClaimDate,Status,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime from tmp__MSPClaim_0
go

set identity_insert MSPClaim off
go

drop table tmp__MSPClaim_0
go

commit
go


