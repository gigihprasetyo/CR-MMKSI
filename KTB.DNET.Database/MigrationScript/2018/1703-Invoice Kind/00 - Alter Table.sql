SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE Invoice DROP
CONSTRAINT [FK_SalesOrder[one]]_Invoice[many]]] 
GO

EXEC sp_rename 'dbo.PK_Invoice', 'tmp__PK_Invoice', 'OBJECT'
GO

EXEC sp_rename 'dbo.Invoice', 'tmp__Invoice_0', 'OBJECT'
GO

CREATE TABLE Invoice
    (
      ID INT NOT NULL
             IDENTITY
             CONSTRAINT PK_Invoice PRIMARY KEY ,
      InvoiceNumber VARCHAR(10) NOT NULL ,
      InvoiceDate DATETIME NOT NULL ,
      Amount MONEY NOT NULL ,
      SalesOrderID INT NOT NULL ,
      InvoiceType VARCHAR(4) NOT NULL ,
      InvoiceRef VARCHAR(10) ,
      LogisticDNID INT ,
      InvoiceKind INT ,
      RowStatus SMALLINT ,
      CreatedBy VARCHAR(20) ,
      CreatedTime DATETIME ,
      LastUpdateBy VARCHAR(20) ,
      LastUpdateTime DATETIME
    )
GO

ALTER TABLE Invoice ADD
CONSTRAINT [FK_SalesOrder[one]]_Invoice[many]]] FOREIGN KEY(SalesOrderID) REFERENCES SalesOrder(ID)
GO

CREATE INDEX IX_InvoiceDate ON Invoice(InvoiceDate)
GO

CREATE INDEX IX_Invoice_Reff ON Invoice(InvoiceRef)
GO

GRANT SELECT ON Invoice TO ccUser
GO

SET IDENTITY_INSERT Invoice ON
GO

INSERT  INTO Invoice
        ( ID ,
          InvoiceNumber ,
          InvoiceDate ,
          Amount ,
          SalesOrderID ,
          InvoiceType ,
          InvoiceRef ,
          LogisticDNID ,
          RowStatus ,
          CreatedBy ,
          CreatedTime ,
          LastUpdateBy ,
          LastUpdateTime
        )
        SELECT  ID ,
                InvoiceNumber ,
                InvoiceDate ,
                Amount ,
                SalesOrderID ,
                InvoiceType ,
                InvoiceRef ,
                LogisticDNID ,
                RowStatus ,
                CreatedBy ,
                CreatedTime ,
                LastUpdateBy ,
                LastUpdateTime
        FROM    tmp__Invoice_0
GO

SET IDENTITY_INSERT Invoice OFF
GO

DROP TABLE tmp__Invoice_0
GO

COMMIT
GO


