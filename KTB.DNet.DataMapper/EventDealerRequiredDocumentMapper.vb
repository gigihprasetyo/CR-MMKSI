
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EventDealerRequiredDocument Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 27/05/2019 - 13:55:51
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

    Public Class EventDealerRequiredDocumentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEventDealerRequiredDocument"
        Private m_UpdateStatement As String = "up_UpdateEventDealerRequiredDocument"
        Private m_RetrieveStatement As String = "up_RetrieveEventDealerRequiredDocument"
        Private m_RetrieveListStatement As String = "up_RetrieveEventDealerRequiredDocumentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEventDealerRequiredDocument"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim eventDealerRequiredDocument As EventDealerRequiredDocument = Nothing
            While dr.Read

                eventDealerRequiredDocument = Me.CreateObject(dr)

            End While

            Return eventDealerRequiredDocument

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim eventDealerRequiredDocumentList As ArrayList = New ArrayList

            While dr.Read
                Dim eventDealerRequiredDocument As EventDealerRequiredDocument = Me.CreateObject(dr)
                eventDealerRequiredDocumentList.Add(eventDealerRequiredDocument)
            End While

            Return eventDealerRequiredDocumentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventDealerRequiredDocument As EventDealerRequiredDocument = CType(obj, EventDealerRequiredDocument)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventDealerRequiredDocument.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventDealerRequiredDocument As EventDealerRequiredDocument = CType(obj, EventDealerRequiredDocument)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DocumentType", DbType.Int32, eventDealerRequiredDocument.DocumentType)
            DbCommandWrapper.AddInParameter("@DocumentName", DbType.AnsiString, eventDealerRequiredDocument.DocumentName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventDealerRequiredDocument.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, eventDealerRequiredDocument.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@EventDealerHeaderID", DbType.Int32, Me.GetRefObject(eventDealerRequiredDocument.EventDealerHeader))

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

            Dim eventDealerRequiredDocument As EventDealerRequiredDocument = CType(obj, EventDealerRequiredDocument)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventDealerRequiredDocument.ID)
            DbCommandWrapper.AddInParameter("@DocumentType", DbType.Int32, eventDealerRequiredDocument.DocumentType)
            DbCommandWrapper.AddInParameter("@DocumentName", DbType.AnsiString, eventDealerRequiredDocument.DocumentName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventDealerRequiredDocument.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, eventDealerRequiredDocument.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@EventDealerHeaderID", DbType.Int32, Me.GetRefObject(eventDealerRequiredDocument.EventDealerHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EventDealerRequiredDocument

            Dim eventDealerRequiredDocument As EventDealerRequiredDocument = New EventDealerRequiredDocument

            eventDealerRequiredDocument.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentType")) Then eventDealerRequiredDocument.DocumentType = CType(dr("DocumentType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentName")) Then eventDealerRequiredDocument.DocumentName = dr("DocumentName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then eventDealerRequiredDocument.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then eventDealerRequiredDocument.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then eventDealerRequiredDocument.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then eventDealerRequiredDocument.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then eventDealerRequiredDocument.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("EventDealerHeaderID")) Then
                eventDealerRequiredDocument.EventDealerHeader = New EventDealerHeader(CType(dr("EventDealerHeaderID"), Integer))
            End If

            Return eventDealerRequiredDocument

        End Function

        Private Sub SetTableName()

            If Not (GetType(EventDealerRequiredDocument) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EventDealerRequiredDocument), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EventDealerRequiredDocument).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

