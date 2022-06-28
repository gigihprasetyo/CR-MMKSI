#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositC2InterestHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/17/2020 - 10:55:54 AM
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

    Public Class DepositC2InterestHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDepositC2InterestHeader"
        Private m_UpdateStatement As String = "up_UpdateDepositC2InterestHeader"
        Private m_RetrieveStatement As String = "up_RetrieveDepositC2InterestHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveDepositC2InterestHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDepositC2InterestHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim depositC2InterestHeader As DepositC2InterestHeader = Nothing
            While dr.Read

                depositC2InterestHeader = Me.CreateObject(dr)

            End While

            Return depositC2InterestHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim depositC2InterestHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim depositC2InterestHeader As DepositC2InterestHeader = Me.CreateObject(dr)
                depositC2InterestHeaderList.Add(depositC2InterestHeader)
            End While

            Return depositC2InterestHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositC2InterestHeader As DepositC2InterestHeader = CType(obj, DepositC2InterestHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositC2InterestHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositC2InterestHeader As DepositC2InterestHeader = CType(obj, DepositC2InterestHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Periode", DbType.Int16, depositC2InterestHeader.Periode)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int16, depositC2InterestHeader.Year)
            DbCommandWrapper.AddInParameter("@InterestAmount", DbType.Currency, depositC2InterestHeader.InterestAmount)
            DbCommandWrapper.AddInParameter("@TaxAmount", DbType.Currency, depositC2InterestHeader.TaxAmount)
            DbCommandWrapper.AddInParameter("@NettoAmount", DbType.Currency, depositC2InterestHeader.NettoAmount)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, depositC2InterestHeader.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositC2InterestHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, depositC2InterestHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@FilePathKwitansi", DbType.AnsiString, depositC2InterestHeader.FilePathKwitansi)
            DbCommandWrapper.AddInParameter("@FilePathLetter", DbType.AnsiString, depositC2InterestHeader.FilePathLetter)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(depositC2InterestHeader.Dealer))
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int32, Me.GetRefObject(depositC2InterestHeader.ProductCategory))

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

            Dim depositC2InterestHeader As DepositC2InterestHeader = CType(obj, DepositC2InterestHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositC2InterestHeader.ID)
            DbCommandWrapper.AddInParameter("@Periode", DbType.Int16, depositC2InterestHeader.Periode)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int16, depositC2InterestHeader.Year)
            DbCommandWrapper.AddInParameter("@InterestAmount", DbType.Currency, depositC2InterestHeader.InterestAmount)
            DbCommandWrapper.AddInParameter("@TaxAmount", DbType.Currency, depositC2InterestHeader.TaxAmount)
            DbCommandWrapper.AddInParameter("@NettoAmount", DbType.Currency, depositC2InterestHeader.NettoAmount)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, depositC2InterestHeader.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositC2InterestHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, depositC2InterestHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@FilePathKwitansi", DbType.AnsiString, depositC2InterestHeader.FilePathKwitansi)
            DbCommandWrapper.AddInParameter("@FilePathLetter", DbType.AnsiString, depositC2InterestHeader.FilePathLetter)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(depositC2InterestHeader.Dealer))
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int32, Me.GetRefObject(depositC2InterestHeader.ProductCategory))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DepositC2InterestHeader

            Dim depositC2InterestHeader As DepositC2InterestHeader = New DepositC2InterestHeader

            depositC2InterestHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Periode")) Then depositC2InterestHeader.Periode = CType(dr("Periode"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Year")) Then depositC2InterestHeader.Year = CType(dr("Year"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("InterestAmount")) Then depositC2InterestHeader.InterestAmount = CType(dr("InterestAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TaxAmount")) Then depositC2InterestHeader.TaxAmount = CType(dr("TaxAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("NettoAmount")) Then depositC2InterestHeader.NettoAmount = CType(dr("NettoAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then depositC2InterestHeader.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then depositC2InterestHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then depositC2InterestHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then depositC2InterestHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then depositC2InterestHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then depositC2InterestHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("FilePathKwitansi")) Then depositC2InterestHeader.FilePathKwitansi = dr("FilePathKwitansi").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FilePathLetter")) Then depositC2InterestHeader.FilePathLetter = dr("FilePathLetter").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                depositC2InterestHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then
                depositC2InterestHeader.ProductCategory = New ProductCategory(CType(dr("ProductCategoryID"), Integer))
            End If

            Return depositC2InterestHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(DepositC2InterestHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DepositC2InterestHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DepositC2InterestHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
