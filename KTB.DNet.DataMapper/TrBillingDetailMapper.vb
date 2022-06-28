#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrBillingDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 8/19/2019 - 4:11:19 PM
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

    Public Class TrBillingDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrBillingDetail"
        Private m_UpdateStatement As String = "up_UpdateTrBillingDetail"
        Private m_RetrieveStatement As String = "up_RetrieveTrBillingDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveTrBillingDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrBillingDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim TrBillingDetail As TrBillingDetail = Nothing
            While dr.Read

                TrBillingDetail = Me.CreateObject(dr)

            End While

            Return TrBillingDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim TrBillingDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim TrBillingDetail As TrBillingDetail = Me.CreateObject(dr)
                TrBillingDetailList.Add(TrBillingDetail)
            End While

            Return TrBillingDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim TrBillingDetail As TrBillingDetail = CType(obj, TrBillingDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, TrBillingDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim TrBillingDetail As TrBillingDetail = CType(obj, TrBillingDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@TrBillingHeaderID", DbType.Int32, Me.GetRefObject(TrBillingDetail.TrBillingHeader))
            DbCommandWrapper.AddInParameter("@TrBookingCourseID", DbType.Int32, Me.GetRefObject(TrBillingDetail.TrBookingCourse))
            DbCommandWrapper.AddInParameter("@IsVoucherUsed", DbType.Boolean, TrBillingDetail.IsVoucherUsed)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, TrBillingDetail.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, TrBillingDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, TrBillingDetail.LastUpdateBy)
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

            Dim TrBillingDetail As TrBillingDetail = CType(obj, TrBillingDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, TrBillingDetail.ID)
            DbCommandWrapper.AddInParameter("@TrBillingHeaderID", DbType.Int32, Me.GetRefObject(TrBillingDetail.TrBillingHeader))
            DbCommandWrapper.AddInParameter("@TrBookingCourseID", DbType.Int32, Me.GetRefObject(TrBillingDetail.TrBookingCourse))
            DbCommandWrapper.AddInParameter("@IsVoucherUsed", DbType.Boolean, TrBillingDetail.IsVoucherUsed)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, TrBillingDetail.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, TrBillingDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, TrBillingDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrBillingDetail

            Dim TrBillingDetail As TrBillingDetail = New TrBillingDetail

            TrBillingDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TrBillingHeaderID")) Then TrBillingDetail.TrBillingHeader = New TrBillingHeader(CType(dr("TrBillingHeaderID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("TrBookingCourseID")) Then TrBillingDetail.TrBookingCourse = New TrBookingCourse(CType(dr("TrBookingCourseID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("IsVoucherUsed")) Then TrBillingDetail.IsVoucherUsed = CType(dr("IsVoucherUsed"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then TrBillingDetail.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then TrBillingDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then TrBillingDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then TrBillingDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then TrBillingDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then TrBillingDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return TrBillingDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrBillingDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrBillingDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrBillingDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
