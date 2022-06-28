
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanPartPerformanceHist Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2013 
'// ---------------------
'// $History      : $
'// Generated on 8/27/2013 - 3:21:18 PM
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

    Public Class SalesmanPartPerformanceHistMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSalesmanPartPerformanceHist"
        Private m_UpdateStatement As String = "up_UpdateSalesmanPartPerformanceHist"
        Private m_RetrieveStatement As String = "up_RetrieveSalesmanPartPerformanceHist"
        Private m_RetrieveListStatement As String = "up_RetrieveSalesmanPartPerformanceHistList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSalesmanPartPerformanceHist"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim salesmanPartPerformanceHist As SalesmanPartPerformanceHist = Nothing
            While dr.Read

                salesmanPartPerformanceHist = Me.CreateObject(dr)

            End While

            Return salesmanPartPerformanceHist

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim salesmanPartPerformanceHistList As ArrayList = New ArrayList

            While dr.Read
                Dim salesmanPartPerformanceHist As SalesmanPartPerformanceHist = Me.CreateObject(dr)
                salesmanPartPerformanceHistList.Add(salesmanPartPerformanceHist)
            End While

            Return salesmanPartPerformanceHistList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanPartPerformanceHist As SalesmanPartPerformanceHist = CType(obj, SalesmanPartPerformanceHist)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanPartPerformanceHist.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanPartPerformanceHist As SalesmanPartPerformanceHist = CType(obj, SalesmanPartPerformanceHist)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@HargaJual", DbType.Currency, salesmanPartPerformanceHist.HargaJual)
            DbCommandWrapper.AddInParameter("@HargaPokok", DbType.Currency, salesmanPartPerformanceHist.HargaPokok)
            DbCommandWrapper.AddInParameter("@Profit", DbType.Currency, salesmanPartPerformanceHist.Profit)
            DbCommandWrapper.AddInParameter("@Percentage", DbType.Decimal, salesmanPartPerformanceHist.Percentage)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanPartPerformanceHist.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, salesmanPartPerformanceHist.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SalesmanPartPerformanceID", DbType.Int32, Me.GetRefObject(salesmanPartPerformanceHist.SalesmanPartPerformance))

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

            Dim salesmanPartPerformanceHist As SalesmanPartPerformanceHist = CType(obj, SalesmanPartPerformanceHist)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanPartPerformanceHist.ID)
            DbCommandWrapper.AddInParameter("@HargaJual", DbType.Currency, salesmanPartPerformanceHist.HargaJual)
            DbCommandWrapper.AddInParameter("@HargaPokok", DbType.Currency, salesmanPartPerformanceHist.HargaPokok)
            DbCommandWrapper.AddInParameter("@Profit", DbType.Currency, salesmanPartPerformanceHist.Profit)
            DbCommandWrapper.AddInParameter("@Percentage", DbType.Decimal, salesmanPartPerformanceHist.Percentage)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanPartPerformanceHist.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, salesmanPartPerformanceHist.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SalesmanPartPerformanceID", DbType.Int32, Me.GetRefObject(salesmanPartPerformanceHist.SalesmanPartPerformance))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SalesmanPartPerformanceHist

            Dim salesmanPartPerformanceHist As SalesmanPartPerformanceHist = New SalesmanPartPerformanceHist

            salesmanPartPerformanceHist.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("HargaJual")) Then salesmanPartPerformanceHist.HargaJual = CType(dr("HargaJual"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("HargaPokok")) Then salesmanPartPerformanceHist.HargaPokok = CType(dr("HargaPokok"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Profit")) Then salesmanPartPerformanceHist.Profit = CType(dr("Profit"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Percentage")) Then salesmanPartPerformanceHist.Percentage = CType(dr("Percentage"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesmanPartPerformanceHist.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesmanPartPerformanceHist.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesmanPartPerformanceHist.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then salesmanPartPerformanceHist.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesmanPartPerformanceHist.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanPartPerformanceID")) Then
                salesmanPartPerformanceHist.SalesmanPartPerformance = New SalesmanPartPerformance(CType(dr("SalesmanPartPerformanceID"), Integer))
            End If

            Return salesmanPartPerformanceHist

        End Function

        Private Sub SetTableName()

            If Not (GetType(SalesmanPartPerformanceHist) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SalesmanPartPerformanceHist), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SalesmanPartPerformanceHist).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

