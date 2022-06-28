
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartDODetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 9/29/2016 - 4:23:07 PM
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

    Public Class SparePartDODetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartDODetail"
        Private m_UpdateStatement As String = "up_UpdateSparePartDODetail"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartDODetail"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartDODetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartDODetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartDODetail As SparePartDODetail = Nothing
            While dr.Read

                sparePartDODetail = Me.CreateObject(dr)

            End While

            Return sparePartDODetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartDODetailList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartDODetail As SparePartDODetail = Me.CreateObject(dr)
                sparePartDODetailList.Add(sparePartDODetail)
            End While

            Return sparePartDODetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartDODetail As SparePartDODetail = CType(obj, SparePartDODetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartDODetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartDODetail As SparePartDODetail = CType(obj, SparePartDODetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ItemNoDO", DbType.Int32, sparePartDODetail.ItemNoDO)
            DbCommandWrapper.AddInParameter("@ItemNoSO", DbType.Int32, sparePartDODetail.ItemNoSO)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, sparePartDODetail.Qty)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartDODetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartDODetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(sparePartDODetail.SparePartMaster))
            DbCommandWrapper.AddInParameter("@SparePartDOID", DbType.Int32, Me.GetRefObject(sparePartDODetail.SparePartDO))
            DbCommandWrapper.AddInParameter("@SparePartPOEstimateID", DbType.Int32, Me.GetRefObject(sparePartDODetail.SparePartPOEstimate))

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

            Dim sparePartDODetail As SparePartDODetail = CType(obj, SparePartDODetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartDODetail.ID)
            DbCommandWrapper.AddInParameter("@ItemNoDO", DbType.Int32, sparePartDODetail.ItemNoDO)
            DbCommandWrapper.AddInParameter("@ItemNoSO", DbType.Int32, sparePartDODetail.ItemNoSO)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, sparePartDODetail.Qty)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartDODetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartDODetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(sparePartDODetail.SparePartMaster))
            DbCommandWrapper.AddInParameter("@SparePartDOID", DbType.Int32, Me.GetRefObject(sparePartDODetail.SparePartDO))
            DbCommandWrapper.AddInParameter("@SparePartPOEstimateID", DbType.Int32, Me.GetRefObject(sparePartDODetail.SparePartPOEstimate))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartDODetail

            Dim sparePartDODetail As SparePartDODetail = New SparePartDODetail

            sparePartDODetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ItemNoDO")) Then sparePartDODetail.ItemNoDO = CType(dr("ItemNoDO"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ItemNoSO")) Then sparePartDODetail.ItemNoSO = CType(dr("ItemNoSO"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then sparePartDODetail.Qty = CType(dr("Qty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartDODetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartDODetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartDODetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartDODetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartDODetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartMasterID")) Then
                sparePartDODetail.SparePartMaster = New SparePartMaster(CType(dr("SparePartMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartDOID")) Then
                sparePartDODetail.SparePartDO = New SparePartDO(CType(dr("SparePartDOID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartPOEstimateID")) Then
                sparePartDODetail.SparePartPOEstimate = New SparePartPOEstimate(CType(dr("SparePartPOEstimateID"), Integer))
            End If

            Return sparePartDODetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartDODetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartDODetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartDODetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

