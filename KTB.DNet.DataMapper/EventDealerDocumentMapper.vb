
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EventDealerDocument Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 15/05/2019 - 8:10:40
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

    Public Class EventDealerDocumentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEventDealerDocument"
        Private m_UpdateStatement As String = "up_UpdateEventDealerDocument"
        Private m_RetrieveStatement As String = "up_RetrieveEventDealerDocument"
        Private m_RetrieveListStatement As String = "up_RetrieveEventDealerDocumentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEventDealerDocument"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim eventDealerDocument As EventDealerDocument = Nothing
            While dr.Read

                eventDealerDocument = Me.CreateObject(dr)

            End While

            Return eventDealerDocument

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim eventDealerDocumentList As ArrayList = New ArrayList

            While dr.Read
                Dim eventDealerDocument As EventDealerDocument = Me.CreateObject(dr)
                eventDealerDocumentList.Add(eventDealerDocument)
            End While

            Return eventDealerDocumentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventDealerDocument As EventDealerDocument = CType(obj, EventDealerDocument)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventDealerDocument.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventDealerDocument As EventDealerDocument = CType(obj, EventDealerDocument)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Path", DbType.AnsiString, eventDealerDocument.Path)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, eventDealerDocument.FileName)
            DbCommandWrapper.AddInParameter("@DocumentName", DbType.AnsiString, eventDealerDocument.DocumentName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventDealerDocument.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, eventDealerDocument.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            'DbCommandWrapper.AddInParameter("@EventDealerHeaderID", DbType.Int32, eventDealerDocument.EventDealerHeaderID)
            'DbCommandWrapper.AddInParameter("@EventDealerRequiredDocumentID", DbType.Int32, eventDealerDocument.EventDealerRequiredDocumentID)

            DbCommandWrapper.AddInParameter("@EventDealerHeaderID", DbType.Int32, Me.GetRefObject(eventDealerDocument.EventDealerHeader))
            'DbCommandWrapper.AddInParameter("@EventDealerRequiredDocumentID", DbType.Int32, Me.GetRefObject(eventDealerDocument.EventDealerRequiredDocument))


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

            Dim eventDealerDocument As EventDealerDocument = CType(obj, EventDealerDocument)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventDealerDocument.ID)
            DbCommandWrapper.AddInParameter("@Path", DbType.AnsiString, eventDealerDocument.Path)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, eventDealerDocument.FileName)
            DbCommandWrapper.AddInParameter("@DocumentName", DbType.AnsiString, eventDealerDocument.DocumentName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventDealerDocument.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, eventDealerDocument.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            'DbCommandWrapper.AddInParameter("@EventDealerHeaderID", DbType.Int32, eventDealerDocument.EventDealerHeaderID)
            'DbCommandWrapper.AddInParameter("@EventDealerRequiredDocumentID", DbType.Int32, eventDealerDocument.EventDealerRequiredDocumentID)
            DbCommandWrapper.AddInParameter("@EventDealerHeaderID", DbType.Int32, Me.GetRefObject(eventDealerDocument.EventDealerHeader))
            'DbCommandWrapper.AddInParameter("@EventDealerRequiredDocumentID", DbType.Int32, Me.GetRefObject(eventDealerDocument.EventDealerRequiredDocument))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EventDealerDocument

            Dim eventDealerDocument As EventDealerDocument = New EventDealerDocument

            eventDealerDocument.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Path")) Then eventDealerDocument.Path = dr("Path").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then eventDealerDocument.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentName")) Then eventDealerDocument.DocumentName = dr("DocumentName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then eventDealerDocument.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then eventDealerDocument.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then eventDealerDocument.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then eventDealerDocument.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then eventDealerDocument.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            'If Not dr.IsDBNull(dr.GetOrdinal("EventDealerHeaderID")) Then
            '    eventDealerDocument.EventDealerHeaderID = CType(dr("EventDealerHeaderID"), Integer)
            'End If
            'If Not dr.IsDBNull(dr.GetOrdinal("EventDealerRequiredDocumentID")) Then
            '    eventDealerDocument.EventDealerRequiredDocumentID = CType(dr("EventDealerRequiredDocumentID"), Integer)
            'End If
            If Not dr.IsDBNull(dr.GetOrdinal("EventDealerHeaderID")) Then
                eventDealerDocument.EventDealerHeader = New EventDealerHeader(CType(dr("EventDealerHeaderID"), Integer))
            End If
            'If Not dr.IsDBNull(dr.GetOrdinal("EventDealerRequiredDocumentID")) Then
            '    eventDealerDocument.EventDealerRequiredDocument = New EventDealerRequiredDocument(CType(dr("EventDealerRequiredDocumentID"), Integer))
            'End If

            Return eventDealerDocument

        End Function

        Private Sub SetTableName()

            If Not (GetType(EventDealerDocument) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EventDealerDocument), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EventDealerDocument).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

