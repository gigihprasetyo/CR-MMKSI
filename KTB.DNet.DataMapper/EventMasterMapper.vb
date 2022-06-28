#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EventMaster Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/21/2007 - 9:26:36 AM
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

    Public Class EventMasterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEventMaster"
        Private m_UpdateStatement As String = "up_UpdateEventMaster"
        Private m_RetrieveStatement As String = "up_RetrieveEventMaster"
        Private m_RetrieveListStatement As String = "up_RetrieveEventMasterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEventMaster"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim eventMaster As EventMaster = Nothing
            While dr.Read

                eventMaster = Me.CreateObject(dr)

            End While

            Return eventMaster

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim eventMasterList As ArrayList = New ArrayList

            While dr.Read
                Dim eventMaster As EventMaster = Me.CreateObject(dr)
                eventMasterList.Add(eventMaster)
            End While

            Return eventMasterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventMaster As EventMaster = CType(obj, EventMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventMaster.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventMaster As EventMaster = CType(obj, EventMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@EventNo", DbType.AnsiString, eventMaster.EventNo)
            DbCommandWrapper.AddInParameter("@Period", DbType.Int16, eventMaster.Period)
            DbCommandWrapper.AddInParameter("@StartMonth", DbType.Int16, eventMaster.StartMonth)
            DbCommandWrapper.AddInParameter("@EndMonth", DbType.Int16, eventMaster.EndMonth)
            DbCommandWrapper.AddInParameter("@FileMaterialName", DbType.AnsiString, eventMaster.FileMaterialName)
            DbCommandWrapper.AddInParameter("@FileDirectionName", DbType.AnsiString, eventMaster.FileDirectionName)
            DbCommandWrapper.AddInParameter("@FileProposalName", DbType.AnsiString, eventMaster.FileProposalName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, eventMaster.LastUpdateBy)
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

            Dim eventMaster As EventMaster = CType(obj, EventMaster)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventMaster.ID)
            DbCommandWrapper.AddInParameter("@EventNo", DbType.AnsiString, eventMaster.EventNo)
            DbCommandWrapper.AddInParameter("@Period", DbType.Int16, eventMaster.Period)
            DbCommandWrapper.AddInParameter("@StartMonth", DbType.Int16, eventMaster.StartMonth)
            DbCommandWrapper.AddInParameter("@EndMonth", DbType.Int16, eventMaster.EndMonth)
            DbCommandWrapper.AddInParameter("@FileMaterialName", DbType.AnsiString, eventMaster.FileMaterialName)
            DbCommandWrapper.AddInParameter("@FileDirectionName", DbType.AnsiString, eventMaster.FileDirectionName)
            DbCommandWrapper.AddInParameter("@FileProposalName", DbType.AnsiString, eventMaster.FileProposalName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, eventMaster.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EventMaster

            Dim eventMaster As EventMaster = New EventMaster

            eventMaster.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EventNo")) Then eventMaster.EventNo = dr("EventNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Period")) Then eventMaster.Period = CType(dr("Period"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("StartMonth")) Then eventMaster.StartMonth = CType(dr("StartMonth"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("EndMonth")) Then eventMaster.EndMonth = CType(dr("EndMonth"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("FileMaterialName")) Then eventMaster.FileMaterialName = dr("FileMaterialName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileDirectionName")) Then eventMaster.FileDirectionName = dr("FileDirectionName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileProposalName")) Then eventMaster.FileProposalName = dr("FileProposalName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then eventMaster.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then eventMaster.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then eventMaster.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then eventMaster.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then eventMaster.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return eventMaster

        End Function

        Private Sub SetTableName()

            If Not (GetType(EventMaster) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EventMaster), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EventMaster).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

