
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanPartPerformance Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2013 
'// ---------------------
'// $History      : $
'// Generated on 8/27/2013 - 3:21:00 PM
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

    Public Class SalesmanPartPerformanceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSalesmanPartPerformance"
        Private m_UpdateStatement As String = "up_UpdateSalesmanPartPerformance"
        Private m_RetrieveStatement As String = "up_RetrieveSalesmanPartPerformance"
        Private m_RetrieveListStatement As String = "up_RetrieveSalesmanPartPerformanceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSalesmanPartPerformance"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim salesmanPartPerformance As SalesmanPartPerformance = Nothing
            While dr.Read

                salesmanPartPerformance = Me.CreateObject(dr)

            End While

            Return salesmanPartPerformance

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim salesmanPartPerformanceList As ArrayList = New ArrayList

            While dr.Read
                Dim salesmanPartPerformance As SalesmanPartPerformance = Me.CreateObject(dr)
                salesmanPartPerformanceList.Add(salesmanPartPerformance)
            End While

            Return salesmanPartPerformanceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanPartPerformance As SalesmanPartPerformance = CType(obj, SalesmanPartPerformance)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanPartPerformance.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanPartPerformance As SalesmanPartPerformance = CType(obj, SalesmanPartPerformance)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int16, salesmanPartPerformance.Year)
            DbCommandWrapper.AddInParameter("@Month", DbType.Int16, salesmanPartPerformance.Month)
            DbCommandWrapper.AddInParameter("@Period", DbType.DateTime, salesmanPartPerformance.Period)
            DbCommandWrapper.AddInParameter("@HargaJual", DbType.Currency, salesmanPartPerformance.HargaJual)
            DbCommandWrapper.AddInParameter("@HargaPokok", DbType.Currency, salesmanPartPerformance.HargaPokok)
            DbCommandWrapper.AddInParameter("@Profit", DbType.Currency, salesmanPartPerformance.Profit)
            DbCommandWrapper.AddInParameter("@Percentage", DbType.Decimal, salesmanPartPerformance.Percentage)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanPartPerformance.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, salesmanPartPerformance.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(salesmanPartPerformance.SalesmanHeader))

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

            Dim salesmanPartPerformance As SalesmanPartPerformance = CType(obj, SalesmanPartPerformance)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanPartPerformance.ID)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int16, salesmanPartPerformance.Year)
            DbCommandWrapper.AddInParameter("@Month", DbType.Int16, salesmanPartPerformance.Month)
            DbCommandWrapper.AddInParameter("@Period", DbType.DateTime, salesmanPartPerformance.Period)
            DbCommandWrapper.AddInParameter("@HargaJual", DbType.Currency, salesmanPartPerformance.HargaJual)
            DbCommandWrapper.AddInParameter("@HargaPokok", DbType.Currency, salesmanPartPerformance.HargaPokok)
            DbCommandWrapper.AddInParameter("@Profit", DbType.Currency, salesmanPartPerformance.Profit)
            DbCommandWrapper.AddInParameter("@Percentage", DbType.Decimal, salesmanPartPerformance.Percentage)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanPartPerformance.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, salesmanPartPerformance.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(salesmanPartPerformance.SalesmanHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SalesmanPartPerformance

            Dim salesmanPartPerformance As SalesmanPartPerformance = New SalesmanPartPerformance

            salesmanPartPerformance.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Year")) Then salesmanPartPerformance.Year = CType(dr("Year"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Month")) Then salesmanPartPerformance.Month = CType(dr("Month"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Period")) Then salesmanPartPerformance.Period = CType(dr("Period"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("HargaJual")) Then salesmanPartPerformance.HargaJual = CType(dr("HargaJual"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("HargaPokok")) Then salesmanPartPerformance.HargaPokok = CType(dr("HargaPokok"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Profit")) Then salesmanPartPerformance.Profit = CType(dr("Profit"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Percentage")) Then salesmanPartPerformance.Percentage = CType(dr("Percentage"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesmanPartPerformance.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesmanPartPerformance.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesmanPartPerformance.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then salesmanPartPerformance.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesmanPartPerformance.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then
                salesmanPartPerformance.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanHeaderID"), Int32))
            End If

            Return salesmanPartPerformance

        End Function

        Private Sub SetTableName()

            If Not (GetType(SalesmanPartPerformance) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SalesmanPartPerformance), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SalesmanPartPerformance).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

