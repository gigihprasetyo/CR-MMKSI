#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ClaimDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/8/2007 - 09:03:24 AM
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

    Public Class ClaimDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertClaimDetail"
        Private m_UpdateStatement As String = "up_UpdateClaimDetail"
        Private m_RetrieveStatement As String = "up_RetrieveClaimDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveClaimDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteClaimDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim claimDetail As ClaimDetail = Nothing
            While dr.Read

                claimDetail = Me.CreateObject(dr)

            End While

            Return claimDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim claimDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim claimDetail As ClaimDetail = Me.CreateObject(dr)
                claimDetailList.Add(claimDetail)
            End While

            Return claimDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim claimDetail As ClaimDetail = CType(obj, ClaimDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, claimDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim claimDetail As ClaimDetail = CType(obj, ClaimDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, claimDetail.Qty)
            DbCommandWrapper.AddInParameter("@ApprovedQty", DbType.Int32, claimDetail.ApprovedQty)
            DbCommandWrapper.AddInParameter("@StatusDetail", DbType.Byte, claimDetail.StatusDetail)
            DbCommandWrapper.AddInParameter("@StatusDetailKTB", DbType.Byte, claimDetail.StatusDetailKTB)
            DbCommandWrapper.AddInParameter("@Keterangan", DbType.AnsiString, claimDetail.Keterangan)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, claimDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, claimDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ClaimHeaderID", DbType.Int32, Me.GetRefObject(claimDetail.ClaimHeader))
            DbCommandWrapper.AddInParameter("@ClaimGoodConditionID", DbType.Int32, Me.GetRefObject(claimDetail.ClaimGoodCondition))
            DbCommandWrapper.AddInParameter("@SparePartPOStatusDetailId", DbType.Int32, Me.GetRefObject(claimDetail.SparePartPOStatusDetail))

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

            Dim claimDetail As ClaimDetail = CType(obj, ClaimDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, claimDetail.ID)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, claimDetail.Qty)
            DbCommandWrapper.AddInParameter("@ApprovedQty", DbType.Int32, claimDetail.ApprovedQty)
            DbCommandWrapper.AddInParameter("@StatusDetail", DbType.Byte, claimDetail.StatusDetail)
            DbCommandWrapper.AddInParameter("@StatusDetailKTB", DbType.Byte, claimDetail.StatusDetailKTB)
            DbCommandWrapper.AddInParameter("@Keterangan", DbType.AnsiString, claimDetail.Keterangan)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, claimDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, claimDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ClaimHeaderID", DbType.Int32, Me.GetRefObject(claimDetail.ClaimHeader))
            DbCommandWrapper.AddInParameter("@ClaimGoodConditionID", DbType.Int32, Me.GetRefObject(claimDetail.ClaimGoodCondition))
            DbCommandWrapper.AddInParameter("@SparePartPOStatusDetailId", DbType.Int32, Me.GetRefObject(claimDetail.SparePartPOStatusDetail))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ClaimDetail

            Dim claimDetail As ClaimDetail = New ClaimDetail

            claimDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then claimDetail.Qty = CType(dr("Qty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ApprovedQty")) Then claimDetail.ApprovedQty = CType(dr("ApprovedQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("StatusDetail")) Then claimDetail.StatusDetail = CType(dr("StatusDetail"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("StatusDetailKTB")) Then claimDetail.StatusDetailKTB = CType(dr("StatusDetailKTB"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Keterangan")) Then claimDetail.Keterangan = dr("Keterangan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then claimDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then claimDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then claimDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then claimDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then claimDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimHeaderID")) Then
                claimDetail.ClaimHeader = New ClaimHeader(CType(dr("ClaimHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimGoodConditionID")) Then
                claimDetail.ClaimGoodCondition = New ClaimGoodCondition(CType(dr("ClaimGoodConditionID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartPOStatusDetailId")) Then
                claimDetail.SparePartPOStatusDetail = New SparePartPOStatusDetail(CType(dr("SparePartPOStatusDetailId"), Integer))
            End If

            Return claimDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(ClaimDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ClaimDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ClaimDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

