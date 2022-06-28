
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_ClaimDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 08/16/2017 - 3:25:53 PM
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

    Public Class V_ClaimDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_ClaimDetail"
        Private m_UpdateStatement As String = "up_UpdateV_ClaimDetail"
        Private m_RetrieveStatement As String = "up_RetrieveV_ClaimDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveV_ClaimDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_ClaimDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_ClaimDetail As V_ClaimDetail = Nothing
            While dr.Read

                v_ClaimDetail = Me.CreateObject(dr)

            End While

            Return v_ClaimDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_ClaimDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim v_ClaimDetail As V_ClaimDetail = Me.CreateObject(dr)
                v_ClaimDetailList.Add(v_ClaimDetail)
            End While

            Return v_ClaimDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_ClaimDetail As V_ClaimDetail = CType(obj, V_ClaimDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_ClaimDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_ClaimDetail As V_ClaimDetail = CType(obj, V_ClaimDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_ClaimDetail.ID)
            DbCommandWrapper.AddInParameter("@ClaimHeaderID", DbType.Int32, Me.GetRefObject(v_ClaimDetail.ClaimHeader))
            DbCommandWrapper.AddInParameter("@SparePartPOStatusDetailId", DbType.Int32, Me.GetRefObject(v_ClaimDetail.SparePartPOStatusDetail))
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, v_ClaimDetail.Qty)
            DbCommandWrapper.AddInParameter("@ApprovedQty", DbType.Int32, v_ClaimDetail.ApprovedQty)
            DbCommandWrapper.AddInParameter("@ClaimGoodConditionID", DbType.Int32, Me.GetRefObject(v_ClaimDetail.ClaimGoodCondition))
            DbCommandWrapper.AddInParameter("@StatusDetail", DbType.Byte, v_ClaimDetail.StatusDetail)

            DbCommandWrapper.AddInParameter("@StatusDetailKTB", DbType.Byte, v_ClaimDetail.StatusDetailKTB)
            DbCommandWrapper.AddInParameter("@Keterangan", DbType.AnsiString, v_ClaimDetail.Keterangan)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_ClaimDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_ClaimDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@QtyClaim", DbType.Int32, v_ClaimDetail.QtyClaim)


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

            Dim v_ClaimDetail As V_ClaimDetail = CType(obj, V_ClaimDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_ClaimDetail.ID)
            DbCommandWrapper.AddInParameter("@ClaimHeaderID", DbType.Int32, Me.GetRefObject(v_ClaimDetail.ClaimHeader))
            DbCommandWrapper.AddInParameter("@SparePartPOStatusDetailId", DbType.Int32, Me.GetRefObject(v_ClaimDetail.SparePartPOStatusDetail))
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, v_ClaimDetail.Qty)
            DbCommandWrapper.AddInParameter("@ApprovedQty", DbType.Int32, v_ClaimDetail.ApprovedQty)
            DbCommandWrapper.AddInParameter("@ClaimGoodConditionID", DbType.Int32, Me.GetRefObject(v_ClaimDetail.ClaimGoodCondition))
            DbCommandWrapper.AddInParameter("@StatusDetail", DbType.Byte, v_ClaimDetail.StatusDetail)
            DbCommandWrapper.AddInParameter("@StatusDetailKTB", DbType.Byte, v_ClaimDetail.StatusDetailKTB)
            DbCommandWrapper.AddInParameter("@Keterangan", DbType.AnsiString, v_ClaimDetail.Keterangan)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_ClaimDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_ClaimDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@QtyClaim", DbType.Int32, v_ClaimDetail.QtyClaim)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_ClaimDetail

            Dim v_ClaimDetail As V_ClaimDetail = New V_ClaimDetail

            v_ClaimDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimHeaderID")) Then
                v_ClaimDetail.ClaimHeader = New ClaimHeader(CType(dr("ClaimHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartPOStatusDetailId")) Then
                v_ClaimDetail.SparePartPOStatusDetail = New SparePartPOStatusDetail(CType(dr("SparePartPOStatusDetailId"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then v_ClaimDetail.Qty = CType(dr("Qty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ApprovedQty")) Then v_ClaimDetail.ApprovedQty = CType(dr("ApprovedQty"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("ClaimGoodConditionID")) Then
                v_ClaimDetail.ClaimGoodCondition = New ClaimGoodCondition(CType(dr("ClaimGoodConditionID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("StatusDetail")) Then v_ClaimDetail.StatusDetail = CType(dr("StatusDetail"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("StatusDetailKTB")) Then v_ClaimDetail.StatusDetailKTB = CType(dr("StatusDetailKTB"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Keterangan")) Then v_ClaimDetail.Keterangan = dr("Keterangan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_ClaimDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_ClaimDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_ClaimDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_ClaimDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_ClaimDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("QtyClaim")) Then v_ClaimDetail.QtyClaim = CType(dr("QtyClaim"), Integer)

            Return v_ClaimDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_ClaimDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_ClaimDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_ClaimDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

