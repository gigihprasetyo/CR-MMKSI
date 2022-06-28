#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PQRAdditionalInfoBB Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/27/2007 - 10:29:30 AM
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

    Public Class PQRAdditionalInfoBBMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPQRAdditionalInfoBB"
        Private m_UpdateStatement As String = "up_UpdatePQRAdditionalInfoBB"
        Private m_RetrieveStatement As String = "up_RetrievePQRAdditionalInfoBB"
        Private m_RetrieveListStatement As String = "up_RetrievePQRAdditionalInfoBBList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePQRAdditionalInfoBB"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim PQRAdditionalInfoBB As PQRAdditionalInfoBB = Nothing
            While dr.Read

                PQRAdditionalInfoBB = Me.CreateObject(dr)

            End While

            Return PQRAdditionalInfoBB

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim PQRAdditionalInfoBBList As ArrayList = New ArrayList

            While dr.Read
                Dim PQRAdditionalInfoBB As PQRAdditionalInfoBB = Me.CreateObject(dr)
                PQRAdditionalInfoBBList.Add(PQRAdditionalInfoBB)
            End While

            Return PQRAdditionalInfoBBList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim PQRAdditionalInfoBB As PQRAdditionalInfoBB = CType(obj, PQRAdditionalInfoBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, PQRAdditionalInfoBB.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim PQRAdditionalInfoBB As PQRAdditionalInfoBB = CType(obj, PQRAdditionalInfoBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Sender", DbType.AnsiString, PQRAdditionalInfoBB.Sender)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.AnsiString, PQRAdditionalInfoBB.Attachment)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, PQRAdditionalInfoBB.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, PQRAdditionalInfoBB.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@PQRHeaderBBID", DbType.Int32, Me.GetRefObject(PQRAdditionalInfoBB.PQRHeaderBB))

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

            Dim PQRAdditionalInfoBB As PQRAdditionalInfoBB = CType(obj, PQRAdditionalInfoBB)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, PQRAdditionalInfoBB.ID)
            DbCommandWrapper.AddInParameter("@Sender", DbType.AnsiString, PQRAdditionalInfoBB.Sender)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.AnsiString, PQRAdditionalInfoBB.Attachment)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, PQRAdditionalInfoBB.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, PQRAdditionalInfoBB.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@PQRHeaderBBID", DbType.Int32, Me.GetRefObject(PQRAdditionalInfoBB.PQRHeaderBB))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PQRAdditionalInfoBB

            Dim PQRAdditionalInfoBB As PQRAdditionalInfoBB = New PQRAdditionalInfoBB

            PQRAdditionalInfoBB.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Sender")) Then PQRAdditionalInfoBB.Sender = dr("Sender").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Attachment")) Then PQRAdditionalInfoBB.Attachment = dr("Attachment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then PQRAdditionalInfoBB.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then PQRAdditionalInfoBB.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then PQRAdditionalInfoBB.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then PQRAdditionalInfoBB.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then PQRAdditionalInfoBB.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PQRHeaderBBID")) Then
                PQRAdditionalInfoBB.PQRHeaderBB = New PQRHeaderBB(CType(dr("PQRHeaderBBID"), Integer))
            End If

            Return PQRAdditionalInfoBB

        End Function

        Private Sub SetTableName()

            If Not (GetType(PQRAdditionalInfoBB) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PQRAdditionalInfoBB), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PQRAdditionalInfoBB).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

