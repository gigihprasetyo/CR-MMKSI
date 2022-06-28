
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanPartCodeHistory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 12/5/2011 - 1:52:36 PM
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

    Public Class SalesmanPartCodeHistoryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSalesmanPartCodeHistory"
        Private m_UpdateStatement As String = "up_UpdateSalesmanPartCodeHistory"
        Private m_RetrieveStatement As String = "up_RetrieveSalesmanPartCodeHistory"
        Private m_RetrieveListStatement As String = "up_RetrieveSalesmanPartCodeHistoryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSalesmanPartCodeHistory"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim salesmanPartCodeHistory As SalesmanPartCodeHistory = Nothing
            While dr.Read

                salesmanPartCodeHistory = Me.CreateObject(dr)

            End While

            Return salesmanPartCodeHistory

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim salesmanPartCodeHistoryList As ArrayList = New ArrayList

            While dr.Read
                Dim salesmanPartCodeHistory As SalesmanPartCodeHistory = Me.CreateObject(dr)
                salesmanPartCodeHistoryList.Add(salesmanPartCodeHistory)
            End While

            Return salesmanPartCodeHistoryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanPartCodeHistory As SalesmanPartCodeHistory = CType(obj, SalesmanPartCodeHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanPartCodeHistory.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanPartCodeHistory As SalesmanPartCodeHistory = CType(obj, SalesmanPartCodeHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, salesmanPartCodeHistory.SalesmanCode)
            DbCommandWrapper.AddInParameter("@DateIn", DbType.Object, salesmanPartCodeHistory.DateIn)
            DbCommandWrapper.AddInParameter("@DateOut", DbType.Object, salesmanPartCodeHistory.DateOut)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanPartCodeHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, salesmanPartCodeHistory.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(salesmanPartCodeHistory.SalesmanHeader))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(salesmanPartCodeHistory.Dealer))

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

            Dim salesmanPartCodeHistory As SalesmanPartCodeHistory = CType(obj, SalesmanPartCodeHistory)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanPartCodeHistory.ID)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, salesmanPartCodeHistory.SalesmanCode)
            DbCommandWrapper.AddInParameter("@DateIn", DbType.Object, salesmanPartCodeHistory.DateIn)
            DbCommandWrapper.AddInParameter("@DateOut", DbType.Object, salesmanPartCodeHistory.DateOut)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanPartCodeHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, salesmanPartCodeHistory.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(salesmanPartCodeHistory.SalesmanHeader))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(salesmanPartCodeHistory.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SalesmanPartCodeHistory

            Dim salesmanPartCodeHistory As SalesmanPartCodeHistory = New SalesmanPartCodeHistory

            salesmanPartCodeHistory.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanCode")) Then salesmanPartCodeHistory.SalesmanCode = dr("SalesmanCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DateIn")) Then salesmanPartCodeHistory.DateIn = CType(dr("DateIn"), Object)
            If Not dr.IsDBNull(dr.GetOrdinal("DateOut")) Then salesmanPartCodeHistory.DateOut = CType(dr("DateOut"), Object)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesmanPartCodeHistory.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesmanPartCodeHistory.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesmanPartCodeHistory.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then salesmanPartCodeHistory.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesmanPartCodeHistory.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then
                salesmanPartCodeHistory.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanHeaderID"), Int32))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                salesmanPartCodeHistory.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return salesmanPartCodeHistory

        End Function

        Private Sub SetTableName()

            If Not (GetType(SalesmanPartCodeHistory) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SalesmanPartCodeHistory), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SalesmanPartCodeHistory).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

