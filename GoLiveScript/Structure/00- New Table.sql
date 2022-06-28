set xact_abort on
go

begin transaction
go

create table APIClient(
  ClientId    uniqueidentifier not null constraint [PK_dbo.APIClient] primary key,
  [Name]      nvarchar(max),
  SecretKey   uniqueidentifier not null,
  AppId       uniqueidentifier not null,
  CreatedBy   nvarchar(max),
  CreatedTime datetime,
  UpdatedBy   nvarchar(max),
  UpdatedTime datetime
)
go



create index IX_AppId on APIClient(AppId)
go

create table APIClientPermission(
  Id           int              not null identity constraint [PK_dbo.APIClientPermission] primary key,
  ClientId     uniqueidentifier not null,
  PermissionId int              not null,
  CreatedBy    nvarchar(max),
  CreatedTime  datetime,
  UpdatedBy    nvarchar(max),
  UpdatedTime  datetime
)
go


create index IX_ClientId on APIClientPermission(ClientId)
go

create index IX_PermissionId on APIClientPermission(PermissionId)
go

create table APIClientRole(
  Id          int              not null identity constraint [PK_dbo.APIClientRole] primary key,
  ClientId    uniqueidentifier not null,
  RoleId      int              not null,
  CreatedBy   nvarchar(max),
  CreatedTime datetime,
  UpdatedBy   nvarchar(max),
  UpdatedTime datetime
)
go



create index IX_RoleId on APIClientRole(RoleId)
go

create table APIClientUser(
  Id           int              not null identity constraint [PK_dbo.APIClientUser] primary key,
  ClientId     uniqueidentifier not null,
  UserId       int              not null,
  Token        nvarchar(max),
  TokenExpired datetime,
  LastActivity datetime,
  LastLogin    datetime,
  CreatedBy    nvarchar(max),
  CreatedTime  datetime,
  UpdatedBy    nvarchar(max),
  UpdatedTime  datetime
)
go


create index IX_UserId on APIClientUser(UserId)
go

create table APIEndpointPermission(
  Id             int           not null identity constraint [PK_dbo.APIEndpointPermission] primary key,
  [Name]         nvarchar(max),
  PermissionCode nvarchar(max),
  URI            nvarchar(max),
  EndpointType   int           not null,
  OperationType  int           not null,
  Description    nvarchar(max),
  IsScheduled    bit           not null,
  CreatedBy      nvarchar(max),
  CreatedTime    datetime,
  UpdatedBy      nvarchar(max),
  UpdatedTime    datetime
)
go

create table APIEndpointSchedule(
  Id          int           not null identity constraint [PK_dbo.APIEndpointSchedule] primary key,
  EndpointId  int           not null,
  ScheduleId  int           not null,
  CreatedBy   nvarchar(max),
  CreatedTime datetime,
  UpdatedBy   nvarchar(max),
  UpdatedTime datetime
)
go




create table APIRole(
  Id          int           not null identity constraint [PK_dbo.APIRole] primary key,
  [Name]      nvarchar(256) not null,
  [Level]     nvarchar(max),
  CreatedBy   nvarchar(max),
  CreatedTime datetime,
  UpdatedBy   nvarchar(max),
  UpdatedTime datetime
)
go

create unique index IX_Name on APIRole([Name])
go

create table APIRolePermission(
  Id           int           not null identity constraint [PK_dbo.APIRolePermission] primary key,
  ClientRoleId int           not null,
  PermissionId int           not null,
  CreatedBy    nvarchar(max),
  CreatedTime  datetime,
  UpdatedBy    nvarchar(max),
  UpdatedTime  datetime
)
go



create table APISchedule(
  Id           int            not null identity constraint [PK_dbo.APISchedule] primary key,
  [Name]       nvarchar(max),
  ScheduleType int            not null,
  ScheduleDay  int,
  MonthDay     int,
  ScheduleTime time not null,
  Interval     int            not null,
  DealerCode   nvarchar(max),
  CreatedBy    nvarchar(max),
  CreatedTime  datetime,
  UpdatedBy    nvarchar(max),
  UpdatedTime  datetime
)
go

create table APIThrottle(
  Id            int           not null identity constraint [PK_dbo.APIThrottle] primary key,
  EndpointId    int           not null,
  RequestLimit  int           not null,
  TimeInSeconds int           not null,
  [Enable]      bit           not null,
  CreatedBy     nvarchar(max),
  CreatedTime   datetime,
  UpdatedBy     nvarchar(max),
  UpdatedTime   datetime
)
go

alter table APIThrottle add
  constraint [FK_dbo.APIThrottle_dbo.APIEndpointPermission_EndpointId] foreign key(EndpointId) references APIEndpointPermission(Id) on delete cascade
go

create table APIUser(
  Id                   int           not null identity constraint [PK_dbo.APIUser] primary key,
  FirstName            nvarchar(max),
  LastName             nvarchar(max),
  Street1              nvarchar(max),
  Street2              nvarchar(max),
  Street3              nvarchar(max),
  City                 nvarchar(max),
  State                nvarchar(max),
  PostalCode           nvarchar(max),
  Country              nvarchar(max),
  Company              nvarchar(max),
  Status               bit           not null,
  IsActive             bit           not null,
  DealerId             smallint,
  CreatedBy            nvarchar(max),
  CreatedTime          datetime,
  UpdatedBy            nvarchar(max),
  UpdatedTime          datetime,
  Email                nvarchar(256),
  EmailConfirmed       bit           not null,
  PasswordHash         nvarchar(max),
  SecurityStamp        nvarchar(max),
  PhoneNumber          nvarchar(max),
  PhoneNumberConfirmed bit           not null,
  TwoFactorEnabled     bit           not null,
  LockoutEndDateUtc    datetime,
  LockoutEnabled       bit           not null,
  AccessFailedCount    int           not null,
  UserName             nvarchar(256) not null
)
go

alter table APIUser add
  constraint [FK_dbo.APIUser_dbo.Dealer_DealerId] foreign key(DealerId) references Dealer(ID)
go

create index IX_DealerId on APIUser(DealerId)
go

create unique index UserNameIndex on APIUser(UserName)
go

create table APIUserClaim(
  Id         int           not null identity constraint [PK_dbo.APIUserClaim] primary key,
  UserId     int           not null,
  ClaimType  nvarchar(max),
  ClaimValue nvarchar(max)
)
go

alter table APIUserClaim add
  constraint [FK_dbo.APIUserClaim_dbo.APIUser_UserId] foreign key(UserId) references APIUser(Id) on delete cascade
go

create table APIUserLogin(
  LoginProvider nvarchar(128) not null,
  ProviderKey   nvarchar(128) not null,
  UserId        int           not null,

  constraint [PK_dbo.APIUserLogin] primary key(LoginProvider,ProviderKey,UserId)
)
go

alter table APIUserLogin add
  constraint [FK_dbo.APIUserLogin_dbo.APIUser_UserId] foreign key(UserId) references APIUser(Id) on delete cascade
go

create table APIUserPermission(
  Id                     int           not null identity constraint [PK_dbo.APIUserPermission] primary key,
  ClientUserId           int           not null,
  PermissionId           int           not null,
  IsCustomPermission     bit           not null,
  IsDismantledPermission bit           not null,
  CreatedBy              nvarchar(max),
  CreatedTime            datetime,
  UpdatedBy              nvarchar(max),
  UpdatedTime            datetime
)
go


create table APIUserRole(
  UserId      int           not null,
  RoleId      int           not null,
  CreatedBy   nvarchar(max),
  CreatedTime datetime,
  UpdatedBy   nvarchar(max),
  UpdatedTime datetime,

  constraint [PK_dbo.APIUserRole] primary key(UserId,RoleId)
)
go

alter table APIUserRole add
  constraint [FK_dbo.APIUserRole_dbo.APIRole_RoleId] foreign key(RoleId) references APIRole(Id) on delete cascade,
  constraint [FK_dbo.APIUserRole_dbo.APIUser_UserId] foreign key(UserId) references APIUser(Id) on delete cascade
go

create table APPayment(
  ID                   int          not null identity constraint PK_APPayment primary key,
  [Owner]              varchar(100),
  APPaymentNo          varchar(50),
  APReferenceNo        varchar(100),
  APVoucherReferenceNo varchar(100),
  AppliedToDocument    money,
  BU                   varchar(100),
  Cancelled            bit,
  CashAndBank          varchar(100),
  MethodOfPayment      varchar(100),
  AvailableBalance     money,
  State                smallint,
  TotalChangeAmount    money,
  TotalPaymentAmount   money,
  TransactionDate      datetime,
  Type                 smallint,
  VendorDescription    varchar(100),
  Vendor               varchar(100),
  RowStatus            smallint,
  CreatedBy            varchar(100),
  CreatedTime          datetime,
  LastUpdateBy         varchar(100),
  LastUpdateTime       datetime
)
go

create table APPaymentDetail(
  ID                        int          not null identity constraint PK_APPaymentDetail primary key,
  APPaymentID               int,
  [Owner]                   varchar(100),
  APPaymentDetailNo         varchar(100),
  APPaymentNo               varchar(100),
  BU                        varchar(100),
  ChangeAmount              money,
  Description               varchar(100),
  DifferenceValue           float,
  ExternalDocumentNo        varchar(50),
  ExternalDocumentType      smallint,
  APVoucherNo               varchar(100),
  OrderDate                 datetime,
  OrderNoNVSOReferral       varchar(100),
  OrderNoOutsourceWorkOrder varchar(100),
  OrderNo                   varchar(100),
  OrderNoUVSOReferral       varchar(100),
  OutstandingBalance        money,
  PaymentAmount             money,
  PaymentSlipNo             varchar(50),
  ReceiptFromVendor         bit,
  RemainingBalance          money,
  SourceType                smallint,
  TransactionDocument       varchar(100),
  Vendor                    varchar(100),
  RowStatus                 smallint,
  CreatedBy                 varchar(100),
  CreatedTime               datetime,
  LastUpdateBy              varchar(100),
  LastUpdateTime            datetime
)
go

alter table APPaymentDetail add
  constraint [FK_APPaymentDetail[many]]_APPayment[one]]] foreign key(APPaymentID) references APPayment(ID)
go

create table ARReceipt(
  ID                          int          not null identity constraint PK_ARReceipt primary key,
  [Owner]                     varchar(100),
  GeneratedToken              varchar(36),
  ARInvoiceReferenceNo        varchar(100),
  ARReceiptNo                 varchar(50),
  ARReceiptReferenceNo        varchar(100),
  Type                        smallint,
  BookingFee                  bit,
  BU                          varchar(100),
  Cancelled                   bit,
  CashAndBank                 varchar(100),
  Customer                    varchar(100),
  CustomerNo                  varchar(100),
  EndOrderDate                datetime,
  MethodOfPayment             varchar(100),
  AvailableBalance            money,
  StartOrderDate              datetime,
  State                       smallint,
  AppliedToDocument           money,
  TotalAmountBase             money,
  TotalChangeAmount           money,
  TotalOutstandingBalanceBase money,
  TotalReceiptAmount          money,
  TotalRemainingBalanceBase   money,
  TransactionDate             datetime,
  RowStatus                   smallint,
  CreatedBy                   varchar(100),
  CreatedTime                 datetime,
  LastUpdateBy                varchar(100),
  LastUpdateTime              datetime
)
go

create table ARReceiptDetail(
  ID                  int          not null identity constraint PK_ARReceiptDetail primary key,
  ARReceiptID         int,
  [Owner]             varchar(100),
  DetailNo            varchar(50),
  ARReceiptNo         varchar(100),
  BU                  varchar(100),
  ChangeAmount        money,
  Customer            varchar(100),
  Description         varchar(100),
  DifferenceValue     float,
  InvoiceNo           varchar(100),
  OrderDate           datetime,
  OrderNo             varchar(100),
  OrderNoSO           varchar(100),
  OrderNoUVSO         varchar(100),
  OrderNoWO           varchar(100),
  OutstandingBalance  money,
  PaidBackToCustomer  bit,
  ReceiptAmount       money,
  RemainingBalance    money,
  SourceType          smallint,
  TransactionDocument varchar(100),
  RowStatus           smallint,
  CreatedBy           varchar(100),
  CreatedTime         datetime,
  LastUpdateBy        varchar(100),
  LastUpdateTime      datetime
)
go

alter table ARReceiptDetail add
  constraint [FK_ARReceiptDetail[many]]_ARReceipt[one]]] foreign key(ARReceiptID) references ARReceipt(ID)
go

create table BusinessSectorDetail(
  ID                     int          not null identity constraint PK_BusinessSectorDetail primary key,
  BusinessSectorHeaderID int,
  BusinessDomain         varchar(100),
  RowStatus              smallint,
  CreatedBy              varchar(20),
  CreatedTime            datetime,
  LastUpdateBy           varchar(20),
  LastUpdateTime         datetime
)
go


create table BusinessSectorHeader(
  ID                 int          not null identity constraint PK_BusinessSectorHeader primary key,
  BusinessSectorName varchar(500),
  RowStatus          smallint,
  CreatedBy          varchar(20),
  CreatedTime        datetime,
  LastUpdateBy       varchar(20),
  LastUpdateTime     datetime
)
go

create table CarrosserieDetail(
  ID                      int          not null identity constraint PK_CarrosserieDetail primary key,
  CarrosserieHeaderID     int,
  PDIStateCode            smallint,
  PDIStatusCode           smallint,
  AccessorriesDescription varchar(100),
  AccessorriesName        varchar(100),
  BUCode                  varchar(20),
  BUName                  varchar(100),
  KITName                 varchar(100),
  PBUCode                 varchar(20),
  PBUName                 varchar(100),
  PDIDetailName           varchar(100),
  PDIReceiptDetailNo      varchar(50),
  PDIReceiptName          varchar(100),
  ReceiveQuantity         float,
  RowStatus               smallint,
  CreatedBy               varchar(20),
  CreatedTime             datetime,
  LastUpdateBy            varchar(20),
  LastUpdateTime          datetime
)
go



create table CarrosserieHeader(
  ID                int          not null identity constraint PK_CarrosserieHeader primary key,
  PDIStateCode      smallint,
  PDIStatusCode     smallint,
  BUCode            varchar(50),
  BUName            varchar(100),
  PDIName           varchar(100),
  PDIReceiptNo      varchar(50),
  PDIReceiptRefName varchar(100),
  PDIReceiptStatus  smallint,
  TransactionDate   datetime,
  TransactionType   smallint,
  VendorName        varchar(100),
  ChassisNumber     varchar(20),
  RowStatus         smallint,
  CreatedBy         varchar(20),
  CreatedTime       datetime,
  LastUpdateBy      varchar(20),
  LastUpdateTime    datetime
)
go

create table CustomerGroup(
  ID             int           not null identity constraint PK_CustomerGroup primary key,
  Code           varchar(20),
  [Name]         varchar(150),
  Description    nvarchar(250),
  RowStatus      smallint,
  CreatedBy      varchar(50),
  CreatedTime    datetime,
  LastUpdateBy   varchar(50),
  LastUpdateTime datetime
)
go

create table CustomerRequestOCR(
  ID                int         not null identity constraint PK_CustomerRequestOCR primary key,
  CustomerRequestID int,
  OCRIdentityID     int,
  RowStatus         smallint,
  CreatedBy         varchar(20),
  CreatedTime       datetime,
  LastUpdateBy      varchar(20),
  LastUpdateTime    datetime
)
go

create table DealerSystems(
  ID                             int         not null identity constraint PK_DealerSystems primary key,
  DealerID                       smallint,
  SystemID                       int,
  isSPKMatchFaktur               bit,
  isOnlyUploadPhotoTenagaPenjual bit,
  RowStatus                      smallint,
  CreatedBy                      varchar(20),
  CreatedTime                    datetime,
  LastUpdateBy                   varchar(20),
  LastUpdateTime                 datetime
)
go

create table DMSWOWarrantyClaim(
  ID              int           not null identity constraint PK_DMSWOWarrantyClaim primary key,
  DealerID        smallint,
  DealerBranchID  int,
  ChassisNumber   varchar(20),
  isBB            bit,
  WorkOrderNumber varchar(50),
  FailureDate     datetime,
  ServiceDate     datetime,
  [Owner]         varchar(50),
  Mileage         int,
  ServiceBuletin  varchar(50),
  Symptoms        varchar(1000),
  Causes          varchar(1000),
  Results         varchar(1000),
  Notes           varchar(1000),
  RowStatus       smallint,
  CreateBy        varchar(20),
  CreatedTime     datetime,
  LastUpdateBy    varchar(20),
  LastUpdateTime  datetime
)
go

alter table DMSWOWarrantyClaim add
  constraint [FK_DMSWOWarrantyClaim[many]]_Dealer[one]]] foreign key(DealerID) references Dealer(ID),
  constraint [FK_DMSWOWarrantyClaim[many]]_DealerBranch[one]]] foreign key(DealerBranchID) references DealerBranch(ID)
go
 

create table FleetCustomer(
  ID                     int           not null identity constraint PK_FleetCustomer primary key,
  CustomerGroupID        int,
  ProvinceID             int           not null,
  PreArea                varchar(50),
  CityID                 smallint      not null,
  BusinessSectorDetailId int           not null,
  RatioMatrixID          int,
  CategoryIndex          int,
  TypeIndex              int,
  Code                   varchar(30),
  [Name]                 varchar(50),
  Gedung                 varchar(50),
  Alamat                 varchar(150),
  Kecamatan              varchar(75),
  Kelurahan              varchar(75),
  PostalCode             varchar(10),
  Email                  nvarchar(50),
  PhoneNo                varchar(15),
  TipeCustomer           int,
  IdentityType           int,
  IdentityNumber         varchar(30),
  Attachment             varchar(100),
  ClassificationIndex    int,
  FleetNickName          varchar(50),
  FleetNote              varchar(1000),
  RowStatus              smallint,
  CreatedBy              varchar(50),
  CreatedTime            datetime,
  LastUpdatedBy          varchar(50),
  LastUpdatedTime        datetime
)
go

alter table FleetCustomer add
  constraint [FK_FleetCustomer[many]]_CustomerGroup[one]]] foreign key(CustomerGroupID) references CustomerGroup(ID)
go

create table FleetCustomerContact(
  ID              int          not null identity constraint PK_FleetCustomerContact primary key,
  FleetCustomerID int          not null,
  [Name]          varchar(50),
  Position        varchar(50),
  PhoneNo         varchar(20),
  Handphone       varchar(20),
  Email           nvarchar(50),
  RowStatus       smallint,
  CreatedBy       varchar(50),
  CreatedTime     datetime,
  LastUpdatedBy   varchar(50),
  LastUpdatedTime datetime
)
go

create table FleetCustomerToCustomer(
  ID              int         not null identity constraint PK_FleetCustomerToCustomer primary key,
  FleetCustomerID int         not null,
  CustomerID      int         not null,
  IsDefault       bit,
  RowStatus       smallint,
  CreatedBy       varchar(50),
  CreatedTime     datetime,
  LastUpdatedBy   varchar(50),
  LastUpdatedTime datetime
)
go

create table FleetCustomerToDealer(
  ID              int         not null identity constraint PK_FleetCustomerToDealer primary key,
  FleetCustomerID int         not null,
  DealerID        smallint    not null,
  RowStatus       smallint,
  CreatedBy       varchar(50),
  CreatedTime     datetime,
  LastUpdatedBy   varchar(50),
  LastUpdatedTime datetime
)
go

create table InventoryTransaction(
  ID                     int          not null identity constraint PK_InventoryTransaction primary key,
  [Owner]                varchar(100),
  DealerCode             varchar(100),
  InventoryTransactionNo varchar(100),
  InventoryTransferNo    varchar(100),
  PersonInCharge         varchar(100),
  ProcessCode            varchar(10),
  SourceData             varchar(50),
  State                  smallint,
  TransactionDate        datetime,
  TransactionType        smallint,
  WONo                   varchar(100),
  RowStatus              smallint,
  CreatedBy              varchar(20),
  CreatedTime            datetime,
  LastUpdateBy           varchar(20),
  LastUpdateTime         datetime
)
go

create table InventoryTransactionDetail(
  ID                      int          not null identity constraint PK_InventoryTransactionDetail primary key,
  [Owner]                 varchar(100),
  BaseQuantity            float,
  BatchNo                 varchar(100),
  BU                      varchar(100),
  Department              varchar(100),
  Description             varchar(100),
  FromBU                  varchar(100),
  InventoryTransactionID  int,
  InventoryTransactionNo  varchar(100),
  InventoryTransferDetail varchar(100),
  InventoryUnit           varchar(100),
  Location                varchar(100),
  ProductCrossReference   varchar(100),
  ProductDescription      varchar(100),
  Product                 varchar(100),
  Quantity                float,
  ReasonCode              varchar(100),
  ReferenceNo             varchar(100),
  RegisterSerialNumber    varchar(100),
  RunningNumber           int,
  SerialNo                varchar(100),
  ServicePartsAndMaterial varchar(100),
  Site                    varchar(100),
  SourceData              varchar(100),
  StockNumber             varchar(100),
  StockNumberNV           varchar(100),
  TotalCost               money,
  TransactionType         smallint,
  TransactionUnit         varchar(100),
  UnitCost                money,
  VIN                     varchar(100),
  Warehouse               varchar(100),
  RowStatus               smallint,
  CreatedBy               varchar(20),
  CreatedTime             datetime,
  LastUpdateBy            varchar(20),
  LastUpdateTime          datetime
)
go

alter table InventoryTransactionDetail add
  constraint [FK_InventoryTransactionDetail[many]]_InventoryTransaction[one]]] foreign key(InventoryTransactionID) references InventoryTransaction(ID)
go

create table InventoryTransfer(
  ID                  int          not null identity constraint PK_InventoryTransfer primary key,
  [Owner]             varchar(100),
  FromDealer          varchar(100),
  FromSite            varchar(100),
  InventoryTransferNo varchar(50),
  ItemTypeForTransfer smallint,
  PersonInCharge      varchar(100),
  ReceiptDate         datetime,
  ReceiptNo           varchar(100),
  ReferenceNo         varchar(100),
  SearchVehicle       varchar(50),
  SourceData          varchar(50),
  State               smallint,
  ToDealer            varchar(100),
  ToSite              varchar(100),
  TransactionDate     datetime,
  TransactionType     smallint,
  TransferStatus      smallint,
  TransferStep        bit,
  WONo                varchar(100),
  RowStatus           smallint,
  CreatedBy           varchar(100),
  CreatedTime         datetime,
  LastUpdateBy        varchar(100),
  LastUpdateTime      datetime
)
go

create table InventoryTransferDetail(
  ID                      int          not null identity constraint PK_InventoryTransferDetail primary key,
  InventoryTransferID     int,
  [Owner]                 varchar(100),
  BaseQuantity            float,
  ConsumptionTaxIn        varchar(100),
  ConsumptionTaxOut       varchar(100),
  FromBatchNo             varchar(100),
  FromDealer              varchar(100),
  FromConfiguration       varchar(100),
  FromExteriorColor       varchar(100),
  FromInteriorColor       varchar(100),
  FromLocation            varchar(100),
  FromSerialNo            varchar(100),
  FromSite                varchar(100),
  FromStyle               varchar(100),
  FromWarehouse           varchar(100),
  InventoryTransferNo     varchar(100),
  InventoryUnit           varchar(100),
  ProductDescription      varchar(100),
  Product                 varchar(100),
  Quantity                float,
  Remarks                 varchar(100),
  ServicePartsandMaterial varchar(100),
  SourceData              varchar(50),
  StockNumber             varchar(100),
  StockNumberNV           varchar(100),
  StockNumberLookupName   varchar(200),
  StockNumberLookupType   int,
  ToBatchNo               varchar(100),
  ToDealer                varchar(100),
  ToConfiguration         varchar(100),
  ToExteriorColor         varchar(100),
  ToInteriorColor         varchar(100),
  ToLocation              varchar(100),
  ToSerialNo              varchar(100),
  ToSite                  varchar(100),
  ToStyle                 varchar(100),
  ToWarehouse             varchar(100),
  TransactionUnit         varchar(100),
  VIN                     varchar(50),
  RowStatus               smallint,
  CreatedBy               varchar(100),
  CreatedTime             datetime,
  LastUpdateBy            varchar(100),
  LastUpdateTime          datetime
)
go

alter table InventoryTransferDetail add
  constraint [FK_InventoryTransferDetail[many]]_InventoryTransfer[one]]] foreign key(InventoryTransferID) references InventoryTransfer(ID)
go

create table Karoseri(
  ID             int           not null identity constraint PK_Karoseri primary key,
  Code           varchar(16),
  [Name]         varchar(50),
  City           varchar(50),
  Alamat         varchar(100),
  Kelurahan      varchar(50),
  Kecamatan      varchar(50),
  Province       varchar(50),
  PostalCode     varchar(10),
  PhoneNo        varchar(30),
  Fax            varchar(20),
  WebSite        varchar(20),
  Email          nvarchar(255),
  ContactPerson  varchar(50),
  HP             varchar(20),
  Status         tinyint,
  RowStatus      smallint      not null,
  CreatedBy      varchar(20),
  CreatedTime    datetime,
  LastUpdateBy   varchar(20),
  LastUpdateTime datetime
)
go

create table Leasing(
  ID               int           not null identity constraint PK_Leasing primary key,
  LeasingGroupName varchar(50),
  LeasingCode      varchar(16),
  LeasingName      varchar(50),
  City             varchar(50),
  Alamat           varchar(100),
  Kelurahan        varchar(50),
  Kecamatan        varchar(50),
  Province         varchar(50),
  PostalCode       varchar(10),
  PhoneNo          varchar(30),
  Fax              varchar(20),
  WebSite          varchar(20),
  Email            nvarchar(255),
  ContactPerson    varchar(50),
  HP               varchar(20),
  Status           tinyint,
  RowStatus        smallint      not null,
  CreatedBy        varchar(20),
  CreatedTime      datetime,
  LastUpdateBy     varchar(20),
  LastUpdateTime   datetime
)
go

create table MsApplication(
  AppId                    uniqueidentifier not null constraint [PK_dbo.MsApplication] primary key,
  [Name]                   nvarchar(max),
  DeploymentJenkinsJobName nvarchar(max),
  DeploymentBackupFolder   nvarchar(max),
  CreatedBy                nvarchar(max),
  CreatedTime              datetime,
  UpdatedBy                nvarchar(max),
  UpdatedTime              datetime
)
go

create table MsApplicationPermission(
  Id           int              not null identity constraint [PK_dbo.MsApplicationPermission] primary key,
  AppId        uniqueidentifier not null,
  PermissionId int              not null,
  CreatedBy    nvarchar(max),
  CreatedTime  datetime,
  UpdatedBy    nvarchar(max),
  UpdatedTime  datetime
)
go



create table POOtherVendor(
  ID                        int          not null identity constraint PK_POOtherVendor primary key,
  [Owner]                   varchar(100),
  Address1                  varchar(100),
  Address2                  varchar(100),
  Address3                  varchar(100),
  AllocationPeriod          varchar(100),
  Balance                   money,
  DealerCode                varchar(100),
  City                      varchar(100),
  CloseRespon               varchar(100),
  Country                   varchar(100),
  DeliveryMethod            smallint,
  Description               varchar(100),
  DownPayment               money,
  DownPaymentAmountPaid     money,
  DownPaymentIsPaid         bit,
  EventDate                 varchar(100),
  ExternalDocNo             varchar(100),
  FormSource                smallint,
  GrandTotal                money,
  PaymentGroup              smallint,
  PersonInCharge            varchar(100),
  PostalCode                varchar(100),
  Priority                  smallint,
  Province                  varchar(100),
  PRPOType                  varchar(100),
  PurchaseOrderNo           varchar(100),
  SONo                      varchar(100),
  Site                      varchar(100),
  State                     smallint,
  StockReferenceNo          varchar(100),
  Taxable                   smallint,
  TermsOfPayment            varchar(100),
  TotalAmountBeforeDiscount money,
  TotalBaseAmount           money,
  TotalConsumptionTaxAmount money,
  TotalDiscountAmount       money,
  TotalTitleRegistrationFee money,
  PurchaseOrderDate         datetime,
  VendorDescription         varchar(100),
  Vendor                    varchar(100),
  Warehouse                 varchar(100),
  WONo                      varchar(100),
  RowStatus                 smallint,
  CreatedBy                 varchar(20),
  CreatedTime               datetime,
  LastUpdateBy              varchar(20),
  LastUpdateTime            datetime
)
go

create table POOtherVendorDetail(
  ID                        int          not null identity constraint PK_POOtherVendorDetail primary key,
  POOtherVendorID           int,
  [Owner]                   varchar(100),
  DealerCode                varchar(100),
  CloseLine                 bit,
  CloseReason               varchar(100),
  Completed                 bit,
  ConsumptionTax1Amount     money,
  ConsumptionTax1           varchar(100),
  ConsumptionTax2Amount     money,
  ConsumptionTax2           varchar(100),
  Department                varchar(100),
  Description               varchar(100),
  DiscountAmount            money,
  DiscountPercentage        float,
  EventData                 varchar(100),
  FormSource                smallint,
  BaseQtyOrder              float,
  BaseQtyReceipt            float,
  BaseQtyReturn             float,
  InventoryUnit             varchar(100),
  ProductCrossReference     varchar(100),
  ProductDescription        varchar(100),
  Product                   varchar(100),
  ProductSubstitute         varchar(100),
  ProductVariant            varchar(100),
  ProductVolume             float,
  ProductWeight             float,
  PromisedDate              datetime,
  PurchaseFor               smallint,
  PurchaseOrderNo           varchar(100),
  PurchaseRequisitionDetail varchar(100),
  PurchaseUnit              varchar(100),
  QtyOrder                  float,
  QtyReceipt                float,
  QtyReturn                 float,
  RecallProduct             bit,
  ReferenceNo               varchar(100),
  RequiredDate              datetime,
  SalesOrderDetail          varchar(100),
  ScheduledShippingDate     datetime,
  ServicePartsAndMaterial   varchar(100),
  ShippingDate              datetime,
  Site                      varchar(100),
  StockNumber               varchar(100),
  TitleRegistrationFee      money,
  TotalAmount               money,
  TotalAmountBeforeDiscount money,
  TotalBaseAmount           money,
  TotalConsumptionTaxAmount money,
  TotalVolume               float,
  TotalWeight               float,
  TransactionAmount         money,
  UnitCost                  money,
  Warehouse                 varchar(100),
  RowStatus                 smallint,
  CreatedBy                 varchar(20),
  CreatedTime               datetime,
  LastUpdateBy              varchar(20),
  LastUpdateTime            datetime
)
go

alter table POOtherVendorDetail add
  constraint [FK_POOtherVendorDetail[many]]_POOtherVendor[one]]] foreign key(POOtherVendorID) references POOtherVendor(ID)
go

create table RevisionChassisMasterProfile(
  ID              int          not null identity constraint PK_RevisionChassisMasterProfile primary key,
  ChassisMasterID int,
  EndCustomerID   int,
  ProfileHeaderID tinyint,
  GroupID         tinyint,
  ProfileValue    varchar(250),
  RowStatus       smallint,
  CreatedBy       varchar(20),
  CreatedTime     datetime,
  LastUpdateBy    varchar(20),
  LastUpdateTime  datetime
)
go

alter table RevisionChassisMasterProfile add
  constraint [FK_RevisionChassisMasterProfile[many]]_ChassisMaster[one]]] foreign key(ChassisMasterID) references ChassisMaster(ID),
  constraint [FK_RevisionChassisMasterProfile[many]]_ProfileGroup[one]]] foreign key(GroupID) references ProfileGroup(ID),
  constraint [FK_RevisionChassisMasterProfile[many]]_ProfileHeader[one]]] foreign key(ProfileHeaderID) references ProfileHeader(ID),
  constraint [FK_RevisionChassisMasterProfile[one]]_EndCustomer[one]]] foreign key(EndCustomerID) references EndCustomer(ID)
go

create table RevisionFaktur(
  ID                  int          not null identity constraint PK_RevisionFaktur primary key,
  ChassisMasterID     int,
  EndCustomerID       int,
  OldEndCustomerID    int,
  RegNumber           varchar(15),
  RevisionStatus      smallint,
  RevisionTypeID      smallint,
  IsPay               smallint,
  NewValidationDate   datetime,
  NewValidationBy     varchar(20),
  NewConfirmationDate datetime,
  NewConfirmationBy   varchar(20),
  Remark              varchar(200),
  RowStatus           smallint,
  CreatedBy           varchar(20),
  CreatedTime         datetime,
  LastUpdateBy        varchar(20),
  LastUpdateTime      datetime
)
go

alter table RevisionFaktur add
  constraint [FK_RevisionFaktur[many]]_ChassisMaster[one]]] foreign key(ChassisMasterID) references ChassisMaster(ID),
  constraint [FK_RevisionFaktur[one]]_EndCustomer[one]]] foreign key(EndCustomerID) references EndCustomer(ID),
  constraint [FK_RevisionFaktur[one]]_OldEndCustomer[one]]] foreign key(OldEndCustomerID) references EndCustomer(ID)
go

create table RevisionPaymentDetail(
  ID                      int          not null identity constraint PK_RevisionPaymentDetail primary key,
  RevisionFakturID        int,
  RevisionPaymentHeaderID int,
  RevisionSAPDocID        int,
  IsCancel                smallint,
  CancelReason            varchar(250),
  RowStatus               smallint,
  CreatedBy               varchar(20),
  CreatedTime             datetime,
  LastUpdateBy            varchar(20),
  LastUpdateTime          datetime
)
go

create table RevisionPaymentHeader(
  ID                   int          not null identity constraint PK_RevisionPaymentHeader primary key,
  DealerID             int,
  PaymentType          varchar(3),
  RegNumber            varchar(15),
  RevisionPaymentDocID int,
  SlipNumber           varchar(20),
  TotalAmount          money,
  Status               smallint,
  EvidencePath         varchar(150),
  ActualPaymentDate    datetime,
  ActualPaymentAmount  money,
  AccDocNumber         varchar(30),
  GyroDate             datetime,
  RowStatus            smallint,
  CreatedBy            varchar(20),
  CreatedTime          datetime,
  LastUpdateBy         varchar(20),
  LastUpdateTime       datetime
)
go

create table RevisionPrice(
  ID             int           not null identity constraint PK_RevisionPrice primary key,
  CategoryID     tinyint,
  RevisionTypeID int,
  Amount         money,
  ValidFrom      smalldatetime,
  RowStatus      smallint,
  CreatedBy      varchar(20),
  CreatedTime    datetime,
  LastUpdateBy   varchar(20),
  LastUpdateTime datetime
)
go



create table RevisionSAPDoc(
  ID               int          not null identity constraint PK_RevisionSAPDoc primary key,
  RevisionFakturID int,
  DebitChargeNo    varchar(10),
  DCAmount         money,
  DebitMemoNo      varchar(15),
  DMAmount         money,
  RowStatus        smallint,
  CreatedBy        varchar(100),
  CreatedTime      datetime,
  LastUpdateBy     varchar(100),
  LastUpdateTime   datetime
)
go

create table RevisionSPKFaktur(
  ID             int         not null identity constraint PK_RevisionSPKFaktur primary key,
  SPKHeaderID    int,
  EndCustomerID  int,
  RowStatus      smallint,
  CreatedTime    datetime,
  CreatedBy      varchar(20),
  LastUpdateTime datetime,
  LastUpdateBy   varchar(20)
)
go

alter table RevisionSPKFaktur add
  constraint [FK_RevisionSPKFaktur[many]]_SPKHeader[one]]] foreign key(SPKHeaderID) references SPKHeader(ID),
  constraint [FK_RevisionSPKFaktur[one]]_EndCustomer[one]]] foreign key(EndCustomerID) references EndCustomer(ID)
go

create table RevisionType(
  ID             int          not null identity constraint PK_RevisionTypeMaster primary key,
  Description    varchar(100),
  RevisionCode   varchar(5),
  RowStatus      smallint,
  CreatedBy      varchar(20),
  CreatedTime    datetime,
  LastUpdateBy   varchar(20),
  LastUpdateTime datetime
)
go

create table ServiceTemplate(
  ID                  int          not null identity constraint PK_ServiceTemplate primary key,
  SvcTemplateCode     varchar(20),
  SvcTemplateDesc     varchar(500),
  FSKindID            int,
  PMKindID            int,
  VechileTypeID       int,
  VechileTypeCode     varchar(10),
  WorkOrderCategoryID int,
  RowStatus           smallint,
  CreatedBy           varchar(20),
  CreatedTime         datetime,
  LastUpdateBy        varchar(20),
  LastUpdateTime      datetime
)
go

create table ServiceTemplateActivityDetail(
  ID                              int         not null identity constraint PK_ServiceTemplateActivityDetail primary key,
  ServiceTemplateID               int,
  ServiceTemplateActivityHeaderId int,
  ProductTypeID                   smallint,
  SparePartMasterID               int,
  Description                     varchar(50),
  Qty                             float,
  Price                           money,
  RowStatus                       smallint,
  CreatedBy                       varchar(20),
  CreatedTime                     datetime,
  LastUpdateBy                    varchar(20),
  LastUpdateTime                  datetime
)
go

create table ServiceTemplateActivityHeader(
  ID                          int         not null identity constraint PK_ServiceTemplateActivityHeader primary key,
  ServiceTemplateID           int,
  ServiceTemplateActivityDesc varchar(50),
  Duration                    float,
  RowStatus                   smallint,
  CreatedBy                   varchar(20),
  CreatedTime                 datetime,
  LastUpdateBy                varchar(20),
  LastUpdateTime              datetime
)
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

create table SparePartDeliveryOrder(
  ID                                  int          not null identity constraint PK_SparePartDeliveryOrderCustomer primary key,
  [Owner]                             varchar(100),
  Address1                            varchar(100),
  Address2                            varchar(100),
  Address3                            varchar(100),
  Address4                            varchar(100),
  BusinessPhone                       varchar(60),
  BU                                  varchar(100),
  CancellationDate                    datetime,
  City                                varchar(100),
  CustomerContacts                    varchar(100),
  Customer                            varchar(100),
  CustomerNo                          varchar(50),
  DeliveryAddress                     varchar(100),
  DeliveryOrderNo                     varchar(50),
  DeliveryType                        int,
  ExternalReferenceNo                 varchar(50),
  GrandTotal                          money,
  Status                              smallint,
  MethodofPayment                     varchar(100),
  OrderType                           varchar(100),
  ReferenceNo                         varchar(100),
  Salesperson                         varchar(100),
  State                               smallint,
  TermofPayment                       varchar(100),
  TotalAmountBeforeDiscount           money,
  TotalBaseAmount                     money,
  TotalDiscountAmount                 money,
  TotalMiscChargeBaseAmount           money,
  TotalMiscChargeConsumptionTaxAmount money,
  TotalReceipt                        money,
  TotalConsumptionTaxAmount           money,
  TransactionDate                     datetime,
  RowStatus                           smallint,
  CreatedBy                           varchar(20),
  CreatedTime                         datetime,
  LastUpdateBy                        varchar(20),
  LastUpdateTime                      datetime
)
go

create table SparePartDeliveryOrderDetail(
  ID                        int          not null identity constraint PK_SparePartDeliveryOrderDetail primary key,
  SparePartDeliveryOrderID  int,
  [Owner]                   varchar(100),
  AmountBeforeDiscount      money,
  BaseAmount                money,
  BaseQtyDelivered          float,
  BaseQtyOrder              float,
  BatchNo                   varchar(100),
  BU                        varchar(100),
  ConsumptionTax1Amount     money,
  ConsumptionTax1           varchar(100),
  ConsumptionTax2Amount     money,
  ConsumptionTax2           varchar(100),
  DeliveryOrderDetail       varchar(100),
  DeliveryOrderNo           varchar(100),
  DiscountAmount            money,
  DiscountBaseAmount        money,
  DiscountPercentage        float,
  Location                  varchar(100),
  ProductCrossReference     varchar(100),
  ProductDescription        varchar(100),
  Product                   varchar(100),
  PromiseDate               datetime,
  QtyDelivered              float,
  QtyOrder                  float,
  RequestDate               datetime,
  RunningNumber             int,
  SalesOrderDetail          varchar(100),
  SalesUnit                 varchar(100),
  Site                      varchar(100),
  TotalAmount               money,
  TotalConsumptionTaxAmount money,
  TransactionAmount         money,
  UnitPrice                 money,
  Warehouse                 varchar(100),
  RowStatus                 smallint,
  CreatedBy                 varchar(20),
  CreatedTime               datetime,
  LastUpdateBy              varchar(20),
  LastUpdateTime            datetime
)
go

alter table SparePartDeliveryOrderDetail add
  constraint [FK_SparePartDeliveryOrderDetail[many]]_SparePartDeliveryOrder[one]]] foreign key(SparePartDeliveryOrderID) references SparePartDeliveryOrder(ID)
go

create table SparePartMasterTOP(
  ID                   int         not null identity constraint PK_SparePartMasterTOP primary key,
  SparePartPOTypeTOPID int,
  SparePartMasterID    int,
  Status               bit,
  RowStatus            smallint,
  CreatedBy            varchar(20),
  CreatedTime          datetime,
  LastUpdateBy         varchar(20),
  LastUpdateTime       datetime
)
go



create table SparePartPOTypeTOP(
  ID                    int         not null identity constraint PK_SparePartTypeTOP primary key,
  SparePartPOType       varchar(5),
  IsTOP                 bit,
  TermOfPaymentIDNotTOP int,
  RowStatus             smallint,
  CreatedBy             varchar(20),
  CreatedTime           datetime,
  LastUpdateBy          varchar(20),
  LastUpdateTime        datetime
)
go



create table SparePartPRDetailFromVendor(
  ID                        int           not null identity constraint PK_SparePartPRDetailFromVendor primary key,
  PRDetailNumber            varchar(50),
  SparePartPRID             int           not null,
  PRNumber                  varchar(100)  not null,
  [Owner]                   varchar(100)  not null,
  BaseReceivedQuantity      float,
  BatchNumber               varchar(100),
  DealerCode                varchar(100),
  ChassisModel              varchar(50),
  ChassisNumberRegister     varchar(50),
  ConsumptionTax1Amount     money,
  ConsumptionTax1           varchar(100),
  ConsumptionTax2Amount     money,
  ConsumptionTax2           varchar(100),
  DiscountAmount            money,
  EngineNumber              varchar(50),
  EventData                 varchar(1000),
  InventoryUnit             varchar(100),
  KeyNumber                 varchar(50),
  LandedCost                money,
  Location                  varchar(100),
  ProductDescription        varchar(100),
  Product                   varchar(100)  not null,
  ProductVolume             float,
  ProductWeight             float,
  PurchaseUnit              varchar(100)  not null,
  ReceivedQuantity          float         not null,
  ReferenceNumber           varchar(50),
  ReturnPRDetail            varchar(100),
  ServicePartsAndMaterial   varchar(100),
  Site                      varchar(100)  not null,
  StockNumber               varchar(100),
  TitleRegistrationFee      money,
  TotalAmount               money,
  TotalBaseAmount           money,
  TotalConsumptionTaxAmount money,
  TotalVolume               float,
  TotalWeight               float,
  TransactionAmount         money,
  UnitCost                  money         not null,
  Warehouse                 varchar(100)  not null,
  RowStatus                 smallint,
  CreatedBy                 varchar(100),
  CreatedTime               datetime,
  LastUpdateBy              varchar(100),
  LastUpdateTime            datetime
)
go



create table SparePartPRFromVendor(
  ID                            int           not null identity constraint PK_SparePartPRFromVendor primary key,
  PRNumber                      nvarchar(50),
  PONumber                      varchar(100),
  [Owner]                       varchar(100)  not null,
  APVoucherNumber               varchar(100),
  AssignLandedCost              bit,
  AutoInvoiced                  bit,
  DealerCode                    varchar(100)  not null,
  DeliveryOrderDate             datetime,
  DeliveryOrderNumber           varchar(50),
  EventData                     varchar(4000),
  EventData2                    text,
  GrandTotal                    money,
  Handling                      smallint,
  LoadData                      bit,
  PackingSlipDate               datetime,
  PackingSlipNumber             varchar(50),
  PRReferenceRequired           bit,
  ReturnPRNumber                varchar(100),
  State                         smallint,
  TotalBaseAmount               money,
  TotalConsumptionTax1Amount    money,
  TotalConsumptionTax2Amount    money,
  TotalConsumptionTaxAmount     money,
  TotalTitleRegistrationFree    money,
  TransactionDate               datetime      not null,
  TransferOrderRequestingNumber varchar(100),
  Type                          smallint      not null,
  VendorDescription             varchar(100),
  Vendor                        varchar(100),
  VendorInvoiceNumber           varchar(50),
  WONumber                      varchar(100),
  RowStatus                     smallint,
  CreatedBy                     varchar(100),
  CreatedTime                   datetime,
  LastUpdateBy                  varchar(100),
  LastUpdateTime                datetime
)
go

create table SparePartSalesOrder(
  ID                        int          not null identity constraint PK_SparePartSalesOrder primary key,
  SalesChannel              smallint,
  [Owner]                   varchar(100),
  Status                    smallint,
  DealerCode                varchar(100),
  Customer                  varchar(100),
  CustomerNo                varchar(50),
  DownPaymentAmount         money,
  DownPaymentAmountReceived money,
  DownPaymentIsPaid         bit,
  ExternalReferenceNo       varchar(50),
  GrandTotal                money,
  Handling                  smallint,
  MethodOfPayment           varchar(100),
  OrderType                 varchar(100),
  SalesOrderNo              varchar(50),
  SalesPerson               varchar(100),
  ShipmentType              varchar(50),
  State                     varchar(50),
  TermOfPayment             varchar(100),
  TotalAmountBeforeDiscount money,
  TotalBaseAmount           money,
  TotalConsumptionTaxAmount money,
  TotalDiscountAmount       money,
  TotalReceipt              money,
  TransactionDate           datetime,
  RowStatus                 smallint,
  CreatedBy                 varchar(20),
  CreatedTime               datetime,
  LastUpdateBy              varchar(20),
  LastUpdateTime            datetime
)
go

create table SparePartSalesOrderDetail(
  ID                        int           not null identity constraint PK_SparePartSalesOrderDetail primary key,
  SparePartSalesOrderID     int,
  [Owner]                   varchar(100),
  Status                    smallint,
  AmountBeforeDiscount      money,
  BaseAmount                money,
  KodeDealer                varchar(100),
  ConsumptionTax1Amount     money,
  ConsumptionTax1           varchar(100),
  ConsumptionTax2Amount     money,
  ConsumptionTax2           varchar(100),
  DiscountAmount            money,
  DiscountPercentAge        decimal(18,0),
  ProductCrossReference     varchar(100),
  ProductDescription        varchar(100),
  Product                   varchar(100),
  PromiseDate               datetime,
  QtyDelivered              float,
  QtyOrder                  float,
  RequestDate               datetime,
  SalesOrderDetailID        varchar(50),
  SalesOrderNo              varchar(100),
  SalesUnit                 varchar(100),
  Site                      varchar(100),
  TotalAmount               money,
  TotalConsumptionTaxAmount money,
  TransactionAmount         money,
  UnitPrice                 money,
  Warehouse                 varchar(100),
  RowStatus                 smallint,
  CreatedBy                 varchar(20),
  CreatedTime               datetime,
  LastUpdateBy              varchar(20),
  LastUpdateTime            datetime
)
go

alter table SparePartSalesOrderDetail add
  constraint [FK_SparePartSalesOrderDetail[many]]_SparePartSalesOrder[one]]] foreign key(SparePartSalesOrderID) references SparePartSalesOrder(ID)
go

create table SPKChassis(
  ID              int         not null identity constraint PK_SPKChassis primary key,
  SPKDetailID     int         not null,
  ChassisMasterID int         not null,
  MatchingType    smallint,
  MatchingDate    datetime,
  MatchingNumber  varchar(50),
  ReferenceNumber varchar(50),
  KeyNumber       varchar(50),
  RowStatus       smallint,
  CreatedBy       varchar(50),
  CreatedTime     datetime,
  LastUpdateBy    varchar(50),
  LastUpdateTime  datetime
)
go

alter table SPKChassis add
  constraint [FK_SPKChassis[many]]_ChassisMaster[one]]] foreign key(ChassisMasterID) references ChassisMaster(ID),
  constraint [FK_SPKChassis[many]]_SPKDetail[one]]] foreign key(SPKDetailID) references SPKDetail(ID)
go

create table StandardCodeChar(
  ID             int          not null identity constraint PK_StandardCodeChar primary key,
  Category       varchar(100),
  ValueId        varchar(5)   not null,
  ValueCode      varchar(200),
  ValueDesc      varchar(200),
  Sequence       int,
  RowStatus      smallint,
  CreatedBy      varchar(20),
  CreatedTime    datetime,
  LastUpdateBy   varchar(20),
  LastUpdateTime datetime
)
go

create table TOPBlockStatus(
  ID                  int         not null identity constraint PK_TOPBlockStatus primary key,
  SparePartPOStatusID int,
  Status              int,
  RowStatus           smallint,
  CreatedBy           varchar(20),
  CreatedTime         datetime,
  LastUpdateBy        varchar(20),
  LastUpdateTime      datetime
)
go

alter table TOPBlockStatus add
  constraint [FK_TOPBlockStatus[one]]_SparePartPOStatus[one]]] foreign key(SparePartPOStatusID) references SparePartPOStatus(ID)
go

create table TOPCreditAccount(
  ID                  int         not null identity constraint PK_TOPCreditAccount primary key,
  DealerID            smallint,
  TermOfPaymentID     int,
  KelipatanPembayaran int,
  Status              smallint,
  RowStatus           smallint,
  CreatedBy           varchar(20),
  CreatedTime         datetime,
  LastUpdateBy        varchar(20),
  LastUpdateTime      datetime
)
go

alter table TOPCreditAccount add
  constraint [FK_TOPCreditAccount[one]]_Dealer[one]]] foreign key(DealerID) references Dealer(ID),
  constraint [FK_TOPCreditAccount[one]]_TermOfPayment[one]]] foreign key(TermOfPaymentID) references TermOfPayment(ID)
go

create table TOPSPDeposit(
  ID                 int         not null identity constraint PK_TOPSPDeposit primary key,
  SparePartBillingID int,
  AmountC2           money,
  RowStatus          smallint,
  CreatedBy          varchar(20),
  CreatedTime        datetime,
  LastUpdateBy       varchar(20),
  LastUpdateTime     datetime
)
go

alter table TOPSPDeposit add
  constraint FK_TOPSPDeposit_SparePartBilling foreign key(SparePartBillingID) references SparePartBilling(ID)
go

create table TOPSPDueDate(
  ID                 int         not null identity constraint PK_TOPSPDueDate primary key,
  SparePartBillingID int,
  DueDate            datetime,
  RowStatus          smallint,
  CreatedBy          varchar(20),
  CreatedTime        datetime,
  LastUpdateBy       varchar(20),
  LastUpdateTime     datetime
)
go

create table TOPSPTransferActual(
  ID                     int          not null identity constraint PK_TOPSPTransferActual primary key,
  TOPSPTransferPaymentID int,
  RefTransferBank        varchar(100),
  Amount                 money,
  PostingDate            datetime,
  RowStatus              smallint,
  CreatedBy              varchar(20),
  CreatedTime            datetime,
  LastUpdateBy           varchar(20),
  LastUpdateTime         datetime
)
go



create table TOPSPTransferCeiling(
  ID                int         not null identity constraint PK__TOPSPTra__3214EC276FB9597F primary key,
  CreditAccount     varchar(20),
  ProductCategoryID smallint,
  PaymentType       smallint,
  EffectiveDate     datetime,
  BalanceBefore     money,
  AvailableCeiling  money,
  LastSyncDate      datetime,
  RowStatus         smallint,
  CreatedBy         varchar(20),
  CreatedTime       datetime,
  LastUpdatedBy     varchar(20),
  LastUpdatedTime   datetime
)
go

create table TOPSPTransferCeilingDetail(
  ID                     int         not null identity constraint PK__TOPSPTra__3214EC277389EA63 primary key,
  TOPSPTransferCeilingID int,
  SparepartBillingID     int,
  TOPSPTransferPaymentID int,
  Amount                 money,
  IsIncome               smallint,
  Status                 smallint,
  RowStatus              smallint,
  CreatedBy              varchar(20),
  CreatedTime            datetime,
  LastUpdatedBy          varchar(20),
  LastUpdatedTime        datetime
)
go

create table TOPSPTransferPayment(
  ID                         int         not null identity constraint PK_TOPSPTransferPayment primary key,
  DealerID                   smallint,
  CreditAccount              varchar(6),
  RegNumber                  varchar(15),
  DueDate                    datetime,
  PaymentPurposeID           tinyint,
  TransferPlanDate           datetime,
  BankID                     int,
  TOPSPTransferPaymentIDReff int,
  IsAccelerated              smallint,
  Status                     smallint,
  ValidatedBy                varchar(20),
  ValidatedTime              datetime,
  ConfirmedBy                varchar(20),
  ConfirmedTime              datetime,
  CanceledBy                 varchar(20),
  CanceledTime               datetime,
  TransferAmount             money,
  TransferActualDate         datetime,
  RowStatus                  smallint,
  CreatedBy                  varchar(20),
  CreatedTime                datetime,
  LastUpdateBy               varchar(20),
  LastUpdateTime             datetime
)
go

alter table TOPSPTransferPayment add
  constraint [FK_TOPSPTransferPayment[many]]_Dealer[one]]] foreign key(DealerID) references Dealer(ID),
  constraint [FK_TOPSPTransferPayment[many]]_PaymentPurpose[one]]] foreign key(PaymentPurposeID) references PaymentPurpose(ID)
go

create table TOPSPTransferPaymentDetail(
  ID                     int         not null identity constraint PK_TOPSPTransferPaymentDetail primary key,
  TOPSPTransferPaymentID int,
  SparePartBillingID     int,
  Amount                 money,
  RowStatus              smallint,
  CreatedBy              varchar(20),
  CreatedTime            datetime,
  LastUpdateBy           varchar(20),
  LastUpdateTime         datetime
)
go

alter table TOPSPTransferPaymentDetail add
  constraint [FK_TOPSPTransferPaymentDetail[many]]_TOPSPTransferPayment[one]]] foreign key(TOPSPTransferPaymentID) references TOPSPTransferPayment(ID),
  constraint [FK_TOPSPTransferPaymentDetail[one]]_SparePartBilling[one]]] foreign key(SparePartBillingID) references SparePartBilling(ID)
go

create table VehiclePurchaseDetail(
  ID                      int          not null identity constraint PK_VehiclePurchaseDetail primary key,
  VehiclePurchaseHeaderID int,
  BUCode                  varchar(20),
  BUName                  varchar(100),
  CloseLine               bit,
  CloseLineName           varchar(100),
  CloseReason             varchar(100),
  Completed               bit,
  CompletedName           varchar(100),
  ProductDescription      varchar(100),
  ProductName             varchar(100),
  ProductVariantName      varchar(100),
  PODetail                varchar(50),
  POName                  varchar(100),
  PRDetailName            varchar(100),
  PurchaseUnitName        varchar(100),
  QtyOrder                float,
  QtyReceipt              float,
  QtyReturn               float,
  RecallProduct           bit,
  RecallProductName       varchar(50),
  SODetailName            varchar(100),
  ScheduledShippingDate   datetime,
  ServicePartsAndMaterial varchar(100),
  ShippingDate            datetime,
  Site                    varchar(100),
  StockNumberName         varchar(100),
  RowStatus               smallint,
  CreatedBy               varchar(50),
  CreatedTime             datetime,
  LastUpdateBy            varchar(50),
  LastUpdateTime          datetime
)
go



create table VehiclePurchaseHeader(
  ID                      int          not null identity constraint PK_VehiclePurchaseHeader primary key,
  BUCode                  varchar(20),
  BUName                  varchar(100),
  DeliveryMethod          varchar(10),
  Description             varchar(100),
  PRPOTypeName            varchar(100),
  DMSPONo                 varchar(50),
  DMSPOStatus             int,
  DMSPODate               datetime,
  VendorDescription       varchar(100),
  Vendor                  varchar(100),
  PurchaseOrderNo         varchar(50),
  PurchaseReceiptNo       varchar(50),
  PurchaseReceiptDetailNo varchar(50),
  ChassisModel            varchar(50),
  ChassisNumberRegister   varchar(50),
  RowStatus               smallint,
  CreatedBy               varchar(50),
  CreatedTime             datetime,
  LastUpdateBy            varchar(50),
  LastUpdateTime          datetime
)
go


alter table APIClient add
  constraint [FK_dbo.APIClient_dbo.MsApplication_AppId] foreign key(AppId) references MsApplication(AppId) on delete cascade
 

alter table APIClientPermission add
  constraint [FK_dbo.APIClientPermission_dbo.APIClient_ClientId] foreign key(ClientId) references APIClient(ClientId) on delete cascade,
  constraint [FK_dbo.APIClientPermission_dbo.APIEndpointPermission_PermissionId] foreign key(PermissionId) references APIEndpointPermission(Id) on delete cascade
 
alter table APIClientRole add
  constraint [FK_dbo.APIClientRole_dbo.APIClient_ClientId] foreign key(ClientId) references APIClient(ClientId) on delete cascade,
  constraint [FK_dbo.APIClientRole_dbo.APIRole_RoleId] foreign key(RoleId) references APIRole(Id) on delete cascade
 
alter table APIClientUser add
  constraint [FK_dbo.APIClientUser_dbo.APIClient_ClientId] foreign key(ClientId) references APIClient(ClientId) on delete cascade,
  constraint [FK_dbo.APIClientUser_dbo.APIUser_UserId] foreign key(UserId) references APIUser(Id) on delete cascade
 

create index IX_EndpointId on APIEndpointSchedule(EndpointId)
 

create index IX_ScheduleId on APIEndpointSchedule(ScheduleId)
 

 alter table APIEndpointSchedule add
  constraint [FK_dbo.APIEndpointSchedule_dbo.APIEndpointPermission_EndpointId] foreign key(EndpointId) references APIEndpointPermission(Id) on delete cascade,
  constraint [FK_dbo.APIEndpointSchedule_dbo.APISchedule_ScheduleId] foreign key(ScheduleId) references APISchedule(Id) on delete cascade


alter table APIRolePermission add
  constraint [FK_dbo.APIRolePermission_dbo.APIClientRole_ClientRoleId] foreign key(ClientRoleId) references APIClientRole(Id) on delete cascade,
  constraint [FK_dbo.APIRolePermission_dbo.APIEndpointPermission_PermissionId] foreign key(PermissionId) references APIEndpointPermission(Id) on delete cascade
 
 
 
alter table APIUserPermission add
  constraint [FK_dbo.APIUserPermission_dbo.APIClientUser_ClientUserId] foreign key(ClientUserId) references APIClientUser(Id) on delete cascade,
  constraint [FK_dbo.APIUserPermission_dbo.APIEndpointPermission_PermissionId] foreign key(PermissionId) references APIEndpointPermission(Id) on delete cascade
 
create index IX_ClientUserId on APIUserPermission(ClientUserId)
 

 alter table SparePartPOTypeTOP add
  constraint [FK_SparePartPOTypeTOP[one]]_TermOfPayment[one]]] foreign key(TermOfPaymentIDNotTOP) references TermOfPayment(ID)
 
 
alter table BusinessSectorDetail add
  constraint [FK_BusinessSectorDetail[many]]_BusinessSectorHeader[one]]] foreign key(BusinessSectorHeaderID) references BusinessSectorHeader(ID)
 

 alter table MsApplicationPermission add
  constraint [FK_dbo.MsApplicationPermission_dbo.APIEndpointPermission_PermissionId] foreign key(PermissionId) references APIEndpointPermission(Id) on delete cascade,
  constraint [FK_dbo.MsApplicationPermission_dbo.MsApplication_AppId] foreign key(AppId) references MsApplication(AppId) on delete cascade


alter table CarrosserieDetail add
  constraint [FK_CarrosserieDetail[many]]_CarrosserieHeader[one]]] foreign key(CarrosserieHeaderID) references CarrosserieHeader(ID)
 GO

 alter table SparePartMasterTOP add
  constraint [FK_SparePartMasterTOP[many]]_SparePartMaster[one]]] foreign key(SparePartMasterID) references SparePartMaster(ID),
  constraint [FK_SparePartMasterTOP[many]]_SparePartTypeTOP[one]]] foreign key(SparePartPOTypeTOPID) references SparePartPOTypeTOP(ID)
 
 alter table SparePartPRDetailFromVendor add
  constraint [FK_SparePartPRDetailFromVendor[many]]_SparePartPRFromVendor[one]]] foreign key(SparePartPRID) references SparePartPRFromVendor(ID)
 

 alter table VehiclePurchaseDetail add
  constraint [FK_VehiclePurchaseDetail[many]]_VehiclePurchaseHeader[one]]] foreign key(VehiclePurchaseHeaderID) references VehiclePurchaseHeader(ID)
 
 alter table RevisionPrice add
  constraint [FK_RevisionPrice[many]]_Category[one]]] foreign key(CategoryID) references Category(ID),
  constraint [FK_RevisionPrice[many]]_RevisionType[one]]] foreign key(RevisionTypeID) references RevisionType(ID)
 

 alter table TOPSPTransferActual add
  constraint FK_TOPSPTransferActual_TOPSPTransferPayment foreign key(TOPSPTransferPaymentID) references TOPSPTransferPayment(ID)
 
 


 
create index IX_ClientId on APIClientUser(ClientId)
 

create index IX_ClientRoleId on APIRolePermission(ClientRoleId)
 

create index IX_PermissionId on APIRolePermission(PermissionId)
 

create index IX_EndpointId on APIThrottle(EndpointId)
 
create index IX_UserId_APIUserClaim on APIUserClaim(UserId)
 

create index IX_RoleId on APIUserRole(RoleId)
 

 create index IX_UserId on APIUserRole(UserId)
 create index IX_PermissionId on APIUserPermission(PermissionId)
 create index IX_UserId on APIUserLogin(UserId)


commit
go



