#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanUniformOrderHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/11/2007 - 01:30:30 PM
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

    Public Class SalesmanUniformOrderHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSalesmanUniformOrderHeader"
        Private m_UpdateStatement As String = "up_UpdateSalesmanUniformOrderHeader"
        Private m_RetrieveStatement As String = "up_RetrieveSalesmanUniformOrderHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveSalesmanUniformOrderHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSalesmanUniformOrderHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim salesmanUniformOrderHeader As SalesmanUniformOrderHeader = Nothing
            While dr.Read

                salesmanUniformOrderHeader = Me.CreateObject(dr)

            End While

            Return salesmanUniformOrderHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim salesmanUniformOrderHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim salesmanUniformOrderHeader As SalesmanUniformOrderHeader = Me.CreateObject(dr)
                salesmanUniformOrderHeaderList.Add(salesmanUniformOrderHeader)
            End While

            Return salesmanUniformOrderHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanUniformOrderHeader As SalesmanUniformOrderHeader = CType(obj, SalesmanUniformOrderHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanUniformOrderHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanUniformOrderHeader As SalesmanUniformOrderHeader = CType(obj, SalesmanUniformOrderHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@OrderNumber", DbType.AnsiString, salesmanUniformOrderHeader.OrderNumber)
            DbCommandWrapper.AddInParameter("@OrderDate", DbType.DateTime, salesmanUniformOrderHeader.OrderDate)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, salesmanUniformOrderHeader.Description)
            DbCommandWrapper.AddInParameter("@Note", DbType.AnsiString, salesmanUniformOrderHeader.Note)
            DbCommandWrapper.AddInParameter("@InvoiceNo", DbType.AnsiString, salesmanUniformOrderHeader.InvoiceNo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanUniformOrderHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, salesmanUniformOrderHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SalesmanUnifDistributionID", DbType.Int32, Me.GetRefObject(salesmanUniformOrderHeader.SalesmanUnifDistribution))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(salesmanUniformOrderHeader.Dealer))

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

            Dim salesmanUniformOrderHeader As SalesmanUniformOrderHeader = CType(obj, SalesmanUniformOrderHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanUniformOrderHeader.ID)
            DbCommandWrapper.AddInParameter("@OrderNumber", DbType.AnsiString, salesmanUniformOrderHeader.OrderNumber)
            DbCommandWrapper.AddInParameter("@OrderDate", DbType.DateTime, salesmanUniformOrderHeader.OrderDate)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, salesmanUniformOrderHeader.Description)
            DbCommandWrapper.AddInParameter("@Note", DbType.AnsiString, salesmanUniformOrderHeader.Note)
            DbCommandWrapper.AddInParameter("@InvoiceNo", DbType.AnsiString, salesmanUniformOrderHeader.InvoiceNo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanUniformOrderHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, salesmanUniformOrderHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SalesmanUnifDistributionID", DbType.Int32, Me.GetRefObject(salesmanUniformOrderHeader.SalesmanUnifDistribution))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(salesmanUniformOrderHeader.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SalesmanUniformOrderHeader

            Dim salesmanUniformOrderHeader As SalesmanUniformOrderHeader = New SalesmanUniformOrderHeader

            salesmanUniformOrderHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("OrderNumber")) Then salesmanUniformOrderHeader.OrderNumber = dr("OrderNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderDate")) Then salesmanUniformOrderHeader.OrderDate = CType(dr("OrderDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then salesmanUniformOrderHeader.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Note")) Then salesmanUniformOrderHeader.Note = dr("Note").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InvoiceNo")) Then salesmanUniformOrderHeader.InvoiceNo = dr("InvoiceNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesmanUniformOrderHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesmanUniformOrderHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesmanUniformOrderHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then salesmanUniformOrderHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesmanUniformOrderHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanUnifDistributionID")) Then
                salesmanUniformOrderHeader.SalesmanUnifDistribution = New SalesmanUnifDistribution(CType(dr("SalesmanUnifDistributionID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                salesmanUniformOrderHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return salesmanUniformOrderHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(SalesmanUniformOrderHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SalesmanUniformOrderHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SalesmanUniformOrderHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

