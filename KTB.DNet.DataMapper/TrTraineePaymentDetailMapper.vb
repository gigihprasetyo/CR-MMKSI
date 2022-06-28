#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrTraineePaymentDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 8/26/2019 - 1:07:04 PM
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

    Public Class TrTraineePaymentDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrTraineePaymentDetail"
        Private m_UpdateStatement As String = "up_UpdateTrTraineePaymentDetail"
        Private m_RetrieveStatement As String = "up_RetrieveTrTraineePaymentDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveTrTraineePaymentDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrTraineePaymentDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trTraineePaymentDetail As TrTraineePaymentDetail = Nothing
            While dr.Read

                trTraineePaymentDetail = Me.CreateObject(dr)

            End While

            Return trTraineePaymentDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trTraineePaymentDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim trTraineePaymentDetail As TrTraineePaymentDetail = Me.CreateObject(dr)
                trTraineePaymentDetailList.Add(trTraineePaymentDetail)
            End While

            Return trTraineePaymentDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trTraineePaymentDetail As TrTraineePaymentDetail = CType(obj, TrTraineePaymentDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trTraineePaymentDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trTraineePaymentDetail As TrTraineePaymentDetail = CType(obj, TrTraineePaymentDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@TrBillingHeaderID", DbType.Int32, Me.GetRefObject(trTraineePaymentDetail.TrBillingHeader))
            DbCommandWrapper.AddInParameter("@TrTraineePaymentHeaderID", DbType.Int32, Me.GetRefObject(trTraineePaymentDetail.TrTraineePaymentHeader))
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trTraineePaymentDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trTraineePaymentDetail.LastUpdateBy)
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

            Dim trTraineePaymentDetail As TrTraineePaymentDetail = CType(obj, TrTraineePaymentDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trTraineePaymentDetail.ID)
            DbCommandWrapper.AddInParameter("@TrBillingHeaderID", DbType.Int32, Me.GetRefObject(trTraineePaymentDetail.TrBillingHeader))
            DbCommandWrapper.AddInParameter("@TrTraineePaymentHeaderID", DbType.Int32, Me.GetRefObject(trTraineePaymentDetail.TrTraineePaymentHeader))
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trTraineePaymentDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trTraineePaymentDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrTraineePaymentDetail

            Dim trTraineePaymentDetail As TrTraineePaymentDetail = New TrTraineePaymentDetail

            trTraineePaymentDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TrBillingHeaderID")) Then trTraineePaymentDetail.TrBillingHeader = New TrBillingHeader(CType(dr("TrBillingHeaderID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("TrTraineePaymentHeaderID")) Then trTraineePaymentDetail.TrTraineePaymentHeader = New TrTraineePaymentHeader(CType(dr("TrTraineePaymentHeaderID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trTraineePaymentDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trTraineePaymentDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trTraineePaymentDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trTraineePaymentDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trTraineePaymentDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return trTraineePaymentDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrTraineePaymentDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrTraineePaymentDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrTraineePaymentDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
