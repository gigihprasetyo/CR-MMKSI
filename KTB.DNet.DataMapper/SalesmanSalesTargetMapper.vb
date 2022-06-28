#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanSalesTarget Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/30/2007 - 4:22:10 PM
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

    Public Class SalesmanSalesTargetMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSalesmanSalesTarget"
        Private m_UpdateStatement As String = "up_UpdateSalesmanSalesTarget"
        Private m_RetrieveStatement As String = "up_RetrieveSalesmanSalesTarget"
        Private m_RetrieveListStatement As String = "up_RetrieveSalesmanSalesTargetList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSalesmanSalesTarget"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim salesmanSalesTarget As SalesmanSalesTarget = Nothing
            While dr.Read

                salesmanSalesTarget = Me.CreateObject(dr)

            End While

            Return salesmanSalesTarget

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim salesmanSalesTargetList As ArrayList = New ArrayList

            While dr.Read
                Dim salesmanSalesTarget As SalesmanSalesTarget = Me.CreateObject(dr)
                salesmanSalesTargetList.Add(salesmanSalesTarget)
            End While

            Return salesmanSalesTargetList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanSalesTarget As SalesmanSalesTarget = CType(obj, SalesmanSalesTarget)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanSalesTarget.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanSalesTarget As SalesmanSalesTarget = CType(obj, SalesmanSalesTarget)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@YearTarget", DbType.Int16, salesmanSalesTarget.YearTarget)
            DbCommandWrapper.AddInParameter("@MonthTarget", DbType.Int16, salesmanSalesTarget.MonthTarget)
            DbCommandWrapper.AddInParameter("@ValueTarget", DbType.Currency, salesmanSalesTarget.ValueTarget)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanSalesTarget.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, salesmanSalesTarget.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(salesmanSalesTarget.SalesmanHeader))

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

            Dim salesmanSalesTarget As SalesmanSalesTarget = CType(obj, SalesmanSalesTarget)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanSalesTarget.ID)
            DbCommandWrapper.AddInParameter("@YearTarget", DbType.Int16, salesmanSalesTarget.YearTarget)
            DbCommandWrapper.AddInParameter("@MonthTarget", DbType.Int16, salesmanSalesTarget.MonthTarget)
            DbCommandWrapper.AddInParameter("@ValueTarget", DbType.Currency, salesmanSalesTarget.ValueTarget)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanSalesTarget.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, salesmanSalesTarget.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(salesmanSalesTarget.SalesmanHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SalesmanSalesTarget

            Dim salesmanSalesTarget As SalesmanSalesTarget = New SalesmanSalesTarget

            salesmanSalesTarget.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("YearTarget")) Then salesmanSalesTarget.YearTarget = CType(dr("YearTarget"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("MonthTarget")) Then salesmanSalesTarget.MonthTarget = CType(dr("MonthTarget"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ValueTarget")) Then salesmanSalesTarget.ValueTarget = CType(dr("ValueTarget"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesmanSalesTarget.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesmanSalesTarget.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesmanSalesTarget.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then salesmanSalesTarget.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesmanSalesTarget.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then
                salesmanSalesTarget.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanHeaderID"), Integer))
            End If

            Return salesmanSalesTarget

        End Function

        Private Sub SetTableName()

            If Not (GetType(SalesmanSalesTarget) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SalesmanSalesTarget), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SalesmanSalesTarget).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

