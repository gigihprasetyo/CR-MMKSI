#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BPEvent Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 9/8/2006 - 1:39:26 PM
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

    Public Class BPEventMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBPEvent"
        Private m_UpdateStatement As String = "up_UpdateBPEvent"
        Private m_RetrieveStatement As String = "up_RetrieveBPEvent"
        Private m_RetrieveListStatement As String = "up_RetrieveBPEventList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBPEvent"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim bPEvent As BPEvent = Nothing
            While dr.Read

                bPEvent = Me.CreateObject(dr)

            End While

            Return bPEvent

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim bPEventList As ArrayList = New ArrayList

            While dr.Read
                Dim bPEvent As BPEvent = Me.CreateObject(dr)
                bPEventList.Add(bPEvent)
            End While

            Return bPEventList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim bPEvent As BPEvent = CType(obj, BPEvent)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, bPEvent.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim bPEvent As BPEvent = CType(obj, BPEvent)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Place", DbType.AnsiString, bPEvent.Place)
            DbCommandWrapper.AddInParameter("@StartEventDate", DbType.DateTime, bPEvent.StartEventDate)
            DbCommandWrapper.AddInParameter("@EndEventDate", DbType.DateTime, bPEvent.EndEventDate)
            DbCommandWrapper.AddInParameter("@EventSize", DbType.AnsiString, bPEvent.EventSize)
            DbCommandWrapper.AddInParameter("@NumberOfDay", DbType.Int32, bPEvent.NumberOfDay)
            DbCommandWrapper.AddInParameter("@SalesTarget", DbType.Currency, bPEvent.SalesTarget)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, bPEvent.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, bPEvent.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim bPEvent As BPEvent = CType(obj, BPEvent)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, bPEvent.ID)
            DbCommandWrapper.AddInParameter("@Place", DbType.AnsiString, bPEvent.Place)
            DbCommandWrapper.AddInParameter("@StartEventDate", DbType.DateTime, bPEvent.StartEventDate)
            DbCommandWrapper.AddInParameter("@EndEventDate", DbType.DateTime, bPEvent.EndEventDate)
            DbCommandWrapper.AddInParameter("@EventSize", DbType.AnsiString, bPEvent.EventSize)
            DbCommandWrapper.AddInParameter("@NumberOfDay", DbType.Int32, bPEvent.NumberOfDay)
            DbCommandWrapper.AddInParameter("@SalesTarget", DbType.Currency, bPEvent.SalesTarget)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, bPEvent.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, bPEvent.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BPEvent

            Dim bPEvent As BPEvent = New BPEvent

            bPEvent.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Place")) Then bPEvent.Place = dr("Place").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StartEventDate")) Then bPEvent.StartEventDate = CType(dr("StartEventDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EndEventDate")) Then bPEvent.EndEventDate = CType(dr("EndEventDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EventSize")) Then bPEvent.EventSize = dr("EventSize").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NumberOfDay")) Then bPEvent.NumberOfDay = CType(dr("NumberOfDay"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesTarget")) Then bPEvent.SalesTarget = CType(dr("SalesTarget"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then bPEvent.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then bPEvent.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then bPEvent.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then bPEvent.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then bPEvent.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return bPEvent

        End Function

        Private Sub SetTableName()

            If Not (GetType(BPEvent) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BPEvent), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BPEvent).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

