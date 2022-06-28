
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcComplainFollowupDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 10/14/2011 - 3:58:53 PM
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

    Public Class CcComplainFollowupDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCcComplainFollowupDetail"
        Private m_UpdateStatement As String = "up_UpdateCcComplainFollowupDetail"
        Private m_RetrieveStatement As String = "up_RetrieveCcComplainFollowupDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveCcComplainFollowupDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCcComplainFollowupDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ccComplainFollowupDetail As CcComplainFollowupDetail = Nothing
            While dr.Read

                ccComplainFollowupDetail = Me.CreateObject(dr)

            End While

            Return ccComplainFollowupDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ccComplainFollowupDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim ccComplainFollowupDetail As CcComplainFollowupDetail = Me.CreateObject(dr)
                ccComplainFollowupDetailList.Add(ccComplainFollowupDetail)
            End While

            Return ccComplainFollowupDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccComplainFollowupDetail As CcComplainFollowupDetail = CType(obj, CcComplainFollowupDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ccComplainFollowupDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccComplainFollowupDetail As CcComplainFollowupDetail = CType(obj, CcComplainFollowupDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int32, ccComplainFollowupDetail.Sequence)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, ccComplainFollowupDetail.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccComplainFollowupDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@Note", DbType.AnsiString, ccComplainFollowupDetail.Note)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, ccComplainFollowupDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CcComplainFollowupID", DbType.Int32, Me.GetRefObject(ccComplainFollowupDetail.CcComplainFollowup))

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

            Dim ccComplainFollowupDetail As CcComplainFollowupDetail = CType(obj, CcComplainFollowupDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ccComplainFollowupDetail.ID)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int32, ccComplainFollowupDetail.Sequence)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, ccComplainFollowupDetail.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccComplainFollowupDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@Note", DbType.AnsiString, ccComplainFollowupDetail.Note)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ccComplainFollowupDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@CcComplainFollowupID", DbType.Int32, Me.GetRefObject(ccComplainFollowupDetail.CcComplainFollowup))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CcComplainFollowupDetail

            Dim ccComplainFollowupDetail As CcComplainFollowupDetail = New CcComplainFollowupDetail

            ccComplainFollowupDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Sequence")) Then ccComplainFollowupDetail.Sequence = CType(dr("Sequence"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then ccComplainFollowupDetail.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ccComplainFollowupDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Note")) Then ccComplainFollowupDetail.Note = dr("Note").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ccComplainFollowupDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ccComplainFollowupDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then ccComplainFollowupDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then ccComplainFollowupDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CcComplainFollowupID")) Then
                ccComplainFollowupDetail.CcComplainFollowup = New CcComplainFollowup(CType(dr("CcComplainFollowupID"), Integer))
            End If

            Return ccComplainFollowupDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(CcComplainFollowupDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CcComplainFollowupDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CcComplainFollowupDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

