#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MonthlyDocument Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 24/11/2005 - 10:08:58
'//
'// ===========================================================================	
#End Region


#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Data
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Microsoft.Practices.EnterpriseLibrary.Logging
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.DataMapper

    Public Class MonthlyDocumentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMonthlyDocument"
        Private m_UpdateStatement As String = "up_UpdateMonthlyDocument"
        Private m_RetrieveStatement As String = "up_RetrieveMonthlyDocument"
        Private m_RetrieveListStatement As String = "up_RetrieveMonthlyDocumentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMonthlyDocument"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim monthlyDocument As MonthlyDocument = Nothing
            While dr.Read

                monthlyDocument = Me.CreateObject(dr)

            End While

            Return monthlyDocument

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim monthlyDocumentList As ArrayList = New ArrayList

            While dr.Read
                Dim monthlyDocument As MonthlyDocument = Me.CreateObject(dr)
                monthlyDocumentList.Add(monthlyDocument)
            End While

            Return monthlyDocumentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim monthlyDocument As MonthlyDocument = CType(obj, MonthlyDocument)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@id", DbType.Int32, monthlyDocument.id)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim monthlyDocument As MonthlyDocument = CType(obj, MonthlyDocument)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@id", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Kind", DbType.Int32, monthlyDocument.Kind)
            DbCommandWrapper.AddInParameter("@PeriodeMonth", DbType.Int16, monthlyDocument.PeriodeMonth)
            DbCommandWrapper.AddInParameter("@PeriodeYear", DbType.Int16, monthlyDocument.PeriodeYear)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, monthlyDocument.FileName)
            DbCommandWrapper.AddInParameter("@FileSize", DbType.Int32, monthlyDocument.FileSize)
            DbCommandWrapper.AddInParameter("@LastDownloadBy", DbType.AnsiString, monthlyDocument.LastDownloadBy)
            DbCommandWrapper.AddInParameter("@LastDownloadDate", DbType.DateTime, monthlyDocument.LastDownloadDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, monthlyDocument.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, monthlyDocument.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@TransferDate", DbType.DateTime, monthlyDocument.TransferDate)
            DbCommandWrapper.AddInParameter("@BillingDate", DbType.DateTime, monthlyDocument.BillingDate)
            DbCommandWrapper.AddInParameter("@BillingNo", DbType.String, monthlyDocument.BillingNo)
            DbCommandWrapper.AddInParameter("@AccountingNo", DbType.String, monthlyDocument.AccountingNo)
            DbCommandWrapper.AddInParameter("@TaxNo", DbType.String, monthlyDocument.TaxNo)

            DbCommandWrapper.AddInParameter("@NameofBank", DbType.AnsiString, monthlyDocument.NameofBank)
            DbCommandWrapper.AddInParameter("@AccountNumberBank", DbType.AnsiString, monthlyDocument.AccountNumberBank)
            DbCommandWrapper.AddInParameter("@AmountTransfer", DbType.Currency, monthlyDocument.AmountTransfer)

            DbCommandWrapper.AddInParameter("@ParkedName", DbType.AnsiString, monthlyDocument.ParkedName)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, monthlyDocument.Amount)
            DbCommandWrapper.AddInParameter("@Currencies", DbType.AnsiString, monthlyDocument.Currencies)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, monthlyDocument.Description)
            DbCommandWrapper.AddInParameter("@NoClearing", DbType.Int32, monthlyDocument.NoClearing)
            DbCommandWrapper.AddInParameter("@SettlementDate", DbType.DateTime, monthlyDocument.SettlementDate)

            DbCommandWrapper.AddInParameter("@ActualTransferDate", DbType.DateTime, monthlyDocument.ActualTransferDate)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(monthlyDocument.Dealer))
            DBCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int32, Me.GetRefObject(monthlyDocument.ProductCategory))
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@id"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "id")

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DbCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DbCommandWrapper.AddInParameter("@id", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim monthlyDocument As MonthlyDocument = CType(obj, MonthlyDocument)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@id", DbType.Int32, monthlyDocument.id)
            DbCommandWrapper.AddInParameter("@Kind", DbType.Int32, monthlyDocument.Kind)
            DbCommandWrapper.AddInParameter("@PeriodeMonth", DbType.Int16, monthlyDocument.PeriodeMonth)
            DbCommandWrapper.AddInParameter("@PeriodeYear", DbType.Int16, monthlyDocument.PeriodeYear)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, monthlyDocument.FileName)
            DbCommandWrapper.AddInParameter("@FileSize", DbType.Int32, monthlyDocument.FileSize)
            DbCommandWrapper.AddInParameter("@LastDownloadBy", DbType.AnsiString, monthlyDocument.LastDownloadBy)
            DbCommandWrapper.AddInParameter("@LastDownloadDate", DbType.DateTime, monthlyDocument.LastDownloadDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, monthlyDocument.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, monthlyDocument.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@TransferDate", DbType.DateTime, monthlyDocument.TransferDate)
            DbCommandWrapper.AddInParameter("@BillingDate", DbType.DateTime, monthlyDocument.BillingDate)
            DbCommandWrapper.AddInParameter("@BillingNo", DbType.String, monthlyDocument.BillingNo)
            DbCommandWrapper.AddInParameter("@AccountingNo", DbType.String, monthlyDocument.AccountingNo)
            DbCommandWrapper.AddInParameter("@TaxNo", DbType.String, monthlyDocument.TaxNo)

            DbCommandWrapper.AddInParameter("@NameofBank", DbType.AnsiString, monthlyDocument.NameofBank)
            DbCommandWrapper.AddInParameter("@AccountNumberBank", DbType.AnsiString, monthlyDocument.AccountNumberBank)
            DbCommandWrapper.AddInParameter("@AmountTransfer", DbType.Currency, monthlyDocument.AmountTransfer)

            DbCommandWrapper.AddInParameter("@ParkedName", DbType.AnsiString, monthlyDocument.ParkedName)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, monthlyDocument.Amount)
            DbCommandWrapper.AddInParameter("@Currencies", DbType.AnsiString, monthlyDocument.Currencies)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, monthlyDocument.Description)
            DbCommandWrapper.AddInParameter("@NoClearing", DbType.Int32, monthlyDocument.NoClearing)
            DbCommandWrapper.AddInParameter("@SettlementDate", DbType.DateTime, monthlyDocument.SettlementDate)

            DbCommandWrapper.AddInParameter("@ActualTransferDate", DbType.DateTime, monthlyDocument.ActualTransferDate)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(monthlyDocument.Dealer))
            DBCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int32, Me.GetRefObject(monthlyDocument.ProductCategory))
            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MonthlyDocument

            Dim monthlyDocument As MonthlyDocument = New MonthlyDocument

            monthlyDocument.id = CType(dr("id"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Kind")) Then monthlyDocument.Kind = CType(dr("Kind"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodeMonth")) Then monthlyDocument.PeriodeMonth = CType(dr("PeriodeMonth"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodeYear")) Then monthlyDocument.PeriodeYear = CType(dr("PeriodeYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then monthlyDocument.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileSize")) Then monthlyDocument.FileSize = CType(dr("FileSize"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LastDownloadBy")) Then monthlyDocument.LastDownloadBy = dr("LastDownloadBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastDownloadDate")) Then monthlyDocument.LastDownloadDate = CType(dr("LastDownloadDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then monthlyDocument.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then monthlyDocument.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then monthlyDocument.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then monthlyDocument.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then monthlyDocument.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                monthlyDocument.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then
                monthlyDocument.ProductCategory = New ProductCategory(CType(dr("ProductCategoryID"), Integer))
            End If


            If Not dr.IsDBNull(dr.GetOrdinal("BillingDate")) Then monthlyDocument.BillingDate = CType(dr("BillingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TransferDate")) Then monthlyDocument.TransferDate = CType(dr("TransferDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BillingNo")) Then monthlyDocument.BillingNo = dr("BillingNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AccountingNo")) Then monthlyDocument.AccountingNo = dr("AccountingNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TaxNo")) Then monthlyDocument.TaxNo = dr("TaxNo").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("NameofBank")) Then monthlyDocument.NameofBank = dr("NameofBank").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AccountNumberBank")) Then monthlyDocument.AccountNumberBank = dr("AccountNumberBank").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AmountTransfer")) Then monthlyDocument.AmountTransfer = CType(dr("AmountTransfer"), Decimal)

            If Not dr.IsDBNull(dr.GetOrdinal("ParkedName")) Then monthlyDocument.ParkedName = dr("ParkedName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then monthlyDocument.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Currencies")) Then monthlyDocument.Currencies = dr("Currencies").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then monthlyDocument.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NoClearing")) Then monthlyDocument.NoClearing = CType(dr("NoClearing"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SettlementDate")) Then monthlyDocument.SettlementDate = CType(dr("SettlementDate"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("ActualTransferDate")) Then monthlyDocument.ActualTransferDate = CType(dr("ActualTransferDate"), DateTime)


            Return monthlyDocument

        End Function

        Private Sub SetTableName()

            If Not (GetType(MonthlyDocument) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MonthlyDocument), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MonthlyDocument).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

