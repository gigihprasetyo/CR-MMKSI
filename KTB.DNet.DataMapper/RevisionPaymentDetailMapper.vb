
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : RevisionPaymentDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 9/4/2018 - 9:11:57 AM
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

    Public Class RevisionPaymentDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertRevisionPaymentDetail"
        Private m_UpdateStatement As String = "up_UpdateRevisionPaymentDetail"
        Private m_RetrieveStatement As String = "up_RetrieveRevisionPaymentDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveRevisionPaymentDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteRevisionPaymentDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim revisionPaymentDetail As RevisionPaymentDetail = Nothing
            While dr.Read

                revisionPaymentDetail = Me.CreateObject(dr)

            End While

            Return revisionPaymentDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim revisionPaymentDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim revisionPaymentDetail As RevisionPaymentDetail = Me.CreateObject(dr)
                revisionPaymentDetailList.Add(revisionPaymentDetail)
            End While

            Return revisionPaymentDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim revisionPaymentDetail As RevisionPaymentDetail = CType(obj, RevisionPaymentDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, revisionPaymentDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim revisionPaymentDetail As RevisionPaymentDetail = CType(obj, RevisionPaymentDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@IsCancel", DbType.Int16, revisionPaymentDetail.IsCancel)
            DbCommandWrapper.AddInParameter("@CancelReason", DbType.AnsiString, revisionPaymentDetail.CancelReason)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, revisionPaymentDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, revisionPaymentDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@RevisionFakturID", DbType.Int32, Me.GetRefObject(revisionPaymentDetail.RevisionFaktur))
            DbCommandWrapper.AddInParameter("@RevisionPaymentHeaderID", DbType.Int32, Me.GetRefObject(revisionPaymentDetail.RevisionPaymentHeader))
            DbCommandWrapper.AddInParameter("@RevisionSAPDocID", DbType.Int32, Me.GetRefObject(revisionPaymentDetail.RevisionSAPDoc))

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

            Dim revisionPaymentDetail As RevisionPaymentDetail = CType(obj, RevisionPaymentDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, revisionPaymentDetail.ID)
            DbCommandWrapper.AddInParameter("@IsCancel", DbType.Int16, revisionPaymentDetail.IsCancel)
            DbCommandWrapper.AddInParameter("@CancelReason", DbType.AnsiString, revisionPaymentDetail.CancelReason)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, revisionPaymentDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, revisionPaymentDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@RevisionFakturID", DbType.Int32, Me.GetRefObject(revisionPaymentDetail.RevisionFaktur))
            DbCommandWrapper.AddInParameter("@RevisionPaymentHeaderID", DbType.Int32, Me.GetRefObject(revisionPaymentDetail.RevisionPaymentHeader))
            DbCommandWrapper.AddInParameter("@RevisionSAPDocID", DbType.Int32, Me.GetRefObject(revisionPaymentDetail.RevisionSAPDoc))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As RevisionPaymentDetail

            Dim revisionPaymentDetail As RevisionPaymentDetail = New RevisionPaymentDetail

            revisionPaymentDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsCancel")) Then revisionPaymentDetail.IsCancel = CType(dr("IsCancel"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CancelReason")) Then revisionPaymentDetail.CancelReason = dr("CancelReason").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then revisionPaymentDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then revisionPaymentDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then revisionPaymentDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then revisionPaymentDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then revisionPaymentDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("RevisionFakturID")) Then
                revisionPaymentDetail.RevisionFaktur = New RevisionFaktur(CType(dr("RevisionFakturID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("RevisionPaymentHeaderID")) Then
                revisionPaymentDetail.RevisionPaymentHeader = New RevisionPaymentHeader(CType(dr("RevisionPaymentHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("RevisionSAPDocID")) Then
                revisionPaymentDetail.RevisionSAPDoc = New RevisionSAPDoc(CType(dr("RevisionSAPDocID"), Integer))
            End If

            Return revisionPaymentDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(RevisionPaymentDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(RevisionPaymentDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(RevisionPaymentDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

