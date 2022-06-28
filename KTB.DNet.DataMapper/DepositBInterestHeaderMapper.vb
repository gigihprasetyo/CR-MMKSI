
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositBInterestHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 3/14/2016 - 11:21:08 AM
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

    Public Class DepositBInterestHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDepositBInterestHeader"
        Private m_UpdateStatement As String = "up_UpdateDepositBInterestHeader"
        Private m_RetrieveStatement As String = "up_RetrieveDepositBInterestHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveDepositBInterestHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDepositBInterestHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim depositBInterestHeader As DepositBInterestHeader = Nothing
            While dr.Read

                depositBInterestHeader = Me.CreateObject(dr)

            End While

            Return depositBInterestHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim depositBInterestHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim depositBInterestHeader As DepositBInterestHeader = Me.CreateObject(dr)
                depositBInterestHeaderList.Add(depositBInterestHeader)
            End While

            Return depositBInterestHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositBInterestHeader As DepositBInterestHeader = CType(obj, DepositBInterestHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositBInterestHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositBInterestHeader As DepositBInterestHeader = CType(obj, DepositBInterestHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Periode", DbType.Int16, depositBInterestHeader.Periode)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int16, depositBInterestHeader.Year)
            DbCommandWrapper.AddInParameter("@InterestAmount", DbType.Currency, depositBInterestHeader.InterestAmount)
            DbCommandWrapper.AddInParameter("@TaxAmount", DbType.Currency, depositBInterestHeader.TaxAmount)
            DbCommandWrapper.AddInParameter("@NettoAmount", DbType.Currency, depositBInterestHeader.NettoAmount)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, depositBInterestHeader.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositBInterestHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, depositBInterestHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(depositBInterestHeader.Dealer))
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, Me.GetRefObject(depositBInterestHeader.ProductCategory))

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

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
            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositBInterestHeader As DepositBInterestHeader = CType(obj, DepositBInterestHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositBInterestHeader.ID)
            DbCommandWrapper.AddInParameter("@Periode", DbType.Int16, depositBInterestHeader.Periode)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int16, depositBInterestHeader.Year)
            DbCommandWrapper.AddInParameter("@InterestAmount", DbType.Currency, depositBInterestHeader.InterestAmount)
            DbCommandWrapper.AddInParameter("@TaxAmount", DbType.Currency, depositBInterestHeader.TaxAmount)
            DbCommandWrapper.AddInParameter("@NettoAmount", DbType.Currency, depositBInterestHeader.NettoAmount)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, depositBInterestHeader.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositBInterestHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, depositBInterestHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(depositBInterestHeader.Dealer))
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, Me.GetRefObject(depositBInterestHeader.ProductCategory))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DepositBInterestHeader

            Dim depositBInterestHeader As DepositBInterestHeader = New DepositBInterestHeader

            depositBInterestHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Periode")) Then depositBInterestHeader.Periode = CType(dr("Periode"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Year")) Then depositBInterestHeader.Year = CType(dr("Year"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("InterestAmount")) Then depositBInterestHeader.InterestAmount = CType(dr("InterestAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TaxAmount")) Then depositBInterestHeader.TaxAmount = CType(dr("TaxAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("NettoAmount")) Then depositBInterestHeader.NettoAmount = CType(dr("NettoAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then depositBInterestHeader.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then depositBInterestHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then depositBInterestHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then depositBInterestHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then depositBInterestHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then depositBInterestHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(depositBInterestHeader.Dealer))
            'DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, Me.GetRefObject(depositBInterestHeader.ProductCategory))

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                depositBInterestHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then
                depositBInterestHeader.ProductCategory = New ProductCategory(CType(dr("ProductCategoryID"), Integer))
            End If


            Return depositBInterestHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(DepositBInterestHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DepositBInterestHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DepositBInterestHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

