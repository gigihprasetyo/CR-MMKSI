
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanPartTarget Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2013 
'// ---------------------
'// $History      : $
'// Generated on 8/27/2013 - 1:26:45 PM
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

    Public Class SalesmanPartTargetMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSalesmanPartTarget"
        Private m_UpdateStatement As String = "up_UpdateSalesmanPartTarget"
        Private m_RetrieveStatement As String = "up_RetrieveSalesmanPartTarget"
        Private m_RetrieveListStatement As String = "up_RetrieveSalesmanPartTargetList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSalesmanPartTarget"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim salesmanPartTarget As SalesmanPartTarget = Nothing
            While dr.Read

                salesmanPartTarget = Me.CreateObject(dr)

            End While

            Return salesmanPartTarget

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim salesmanPartTargetList As ArrayList = New ArrayList

            While dr.Read
                Dim salesmanPartTarget As SalesmanPartTarget = Me.CreateObject(dr)
                salesmanPartTargetList.Add(salesmanPartTarget)
            End While

            Return salesmanPartTargetList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanPartTarget As SalesmanPartTarget = CType(obj, SalesmanPartTarget)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanPartTarget.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanPartTarget As SalesmanPartTarget = CType(obj, SalesmanPartTarget)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int16, salesmanPartTarget.Year)
            DbCommandWrapper.AddInParameter("@Month", DbType.Int16, salesmanPartTarget.Month)
            DbCommandWrapper.AddInParameter("@Period", DbType.DateTime, salesmanPartTarget.Period)
            DbCommandWrapper.AddInParameter("@Target", DbType.Currency, salesmanPartTarget.Target)
            DbCommandWrapper.AddInParameter("@Realization", DbType.Currency, salesmanPartTarget.Realization)
            DbCommandWrapper.AddInParameter("@Persentage", DbType.Decimal, salesmanPartTarget.Persentage)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanPartTarget.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, salesmanPartTarget.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(salesmanPartTarget.SalesmanHeader))

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

            Dim salesmanPartTarget As SalesmanPartTarget = CType(obj, SalesmanPartTarget)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanPartTarget.ID)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int16, salesmanPartTarget.Year)
            DbCommandWrapper.AddInParameter("@Month", DbType.Int16, salesmanPartTarget.Month)
            DbCommandWrapper.AddInParameter("@Period", DbType.DateTime, salesmanPartTarget.Period)
            DbCommandWrapper.AddInParameter("@Target", DbType.Currency, salesmanPartTarget.Target)
            DbCommandWrapper.AddInParameter("@Realization", DbType.Currency, salesmanPartTarget.Realization)
            DbCommandWrapper.AddInParameter("@Persentage", DbType.Decimal, salesmanPartTarget.Persentage)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanPartTarget.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, salesmanPartTarget.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(salesmanPartTarget.SalesmanHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SalesmanPartTarget

            Dim salesmanPartTarget As SalesmanPartTarget = New SalesmanPartTarget

            salesmanPartTarget.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Year")) Then salesmanPartTarget.Year = CType(dr("Year"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Month")) Then salesmanPartTarget.Month = CType(dr("Month"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Period")) Then salesmanPartTarget.Period = CType(dr("Period"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Target")) Then salesmanPartTarget.Target = CType(dr("Target"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Realization")) Then salesmanPartTarget.Realization = CType(dr("Realization"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Persentage")) Then salesmanPartTarget.Persentage = CType(dr("Persentage"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesmanPartTarget.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesmanPartTarget.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesmanPartTarget.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then salesmanPartTarget.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesmanPartTarget.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then
                salesmanPartTarget.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanHeaderID"), Int32))
            End If

            Return salesmanPartTarget

        End Function

        Private Sub SetTableName()

            If Not (GetType(SalesmanPartTarget) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SalesmanPartTarget), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SalesmanPartTarget).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

