#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EventReport Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 11/14/2009 - 14:15:40
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

    Public Class EventReportMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEventReport"
        Private m_UpdateStatement As String = "up_UpdateEventReport"
        Private m_RetrieveStatement As String = "up_RetrieveEventReport"
        Private m_RetrieveListStatement As String = "up_RetrieveEventReportList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEventReport"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim eventReport As EventReport = Nothing
            While dr.Read

                eventReport = Me.CreateObject(dr)

            End While

            Return eventReport

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim eventReportList As ArrayList = New ArrayList

            While dr.Read
                Dim eventReport As EventReport = Me.CreateObject(dr)
                eventReportList.Add(eventReport)
            End While

            Return eventReportList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventReport As EventReport = CType(obj, EventReport)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventReport.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventReport As EventReport = CType(obj, EventReport)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Jumlah", DbType.Decimal, eventReport.Jumlah)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, eventReport.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, eventReport.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventReport.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)

            DbCommandWrapper.AddInParameter("@EventProposalID", DbType.Int32, Me.GetRefObject(eventReport.EventProposal))
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(eventReport.VechileType))

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

            Dim eventReport As EventReport = CType(obj, EventReport)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventReport.ID)
            DbCommandWrapper.AddInParameter("@Jumlah", DbType.Decimal, eventReport.Jumlah)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, eventReport.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, eventReport.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventReport.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            'DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)


            DbCommandWrapper.AddInParameter("@EventProposalID", DbType.Int32, Me.GetRefObject(eventReport.EventProposal))
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(eventReport.VechileType))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EventReport

            Dim eventReport As EventReport = New EventReport

            eventReport.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Jumlah")) Then eventReport.Jumlah = CType(dr("Jumlah"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then eventReport.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then eventReport.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then eventReport.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then eventReport.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then eventReport.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then eventReport.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then eventReport.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EventProposalID")) Then
                eventReport.EventProposal = New EventProposal(CType(dr("EventProposalID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then
                eventReport.VechileType = New VechileType(CType(dr("VechileTypeID"), Short))
            End If

            Return eventReport

        End Function

        Private Sub SetTableName()

            If Not (GetType(EventReport) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EventReport), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EventReport).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

