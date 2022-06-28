
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPackingDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 10/5/2016 - 9:27:34 AM
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

    Public Class SparePartPackingDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartPackingDetail"
        Private m_UpdateStatement As String = "up_UpdateSparePartPackingDetail"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartPackingDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartPackingDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartPackingDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartPackingDetail As SparePartPackingDetail = Nothing
            While dr.Read

                sparePartPackingDetail = Me.CreateObject(dr)

            End While

            Return sparePartPackingDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartPackingDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartPackingDetail As SparePartPackingDetail = Me.CreateObject(dr)
                sparePartPackingDetailList.Add(sparePartPackingDetail)
            End While

            Return sparePartPackingDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPackingDetail As SparePartPackingDetail = CType(obj, SparePartPackingDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPackingDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPackingDetail As SparePartPackingDetail = CType(obj, SparePartPackingDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@InternalHUItemNo", DbType.StringFixedLength, sparePartPackingDetail.InternalHUItemNo)
            DbCommandWrapper.AddInParameter("@DOItemNo", DbType.Int32, sparePartPackingDetail.DOItemNo)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Decimal, sparePartPackingDetail.Qty)
            DbCommandWrapper.AddInParameter("@UoM", DbType.AnsiString, sparePartPackingDetail.UoM)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPackingDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartPackingDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartDOID", DbType.Int32, Me.GetRefObject(sparePartPackingDetail.SparePartDO))
            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(sparePartPackingDetail.SparePartMaster))
            DbCommandWrapper.AddInParameter("@SparePartPackingID", DbType.Int32, Me.GetRefObject(sparePartPackingDetail.SparePartPacking))

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

            Dim sparePartPackingDetail As SparePartPackingDetail = CType(obj, SparePartPackingDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPackingDetail.ID)
            DbCommandWrapper.AddInParameter("@InternalHUItemNo", DbType.StringFixedLength, sparePartPackingDetail.InternalHUItemNo)
            DbCommandWrapper.AddInParameter("@DOItemNo", DbType.Int32, sparePartPackingDetail.DOItemNo)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Decimal, sparePartPackingDetail.Qty)
            DbCommandWrapper.AddInParameter("@UoM", DbType.AnsiString, sparePartPackingDetail.UoM)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPackingDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartPackingDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SparePartDOID", DbType.Int32, Me.GetRefObject(sparePartPackingDetail.SparePartDO))
            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(sparePartPackingDetail.SparePartMaster))
            DbCommandWrapper.AddInParameter("@SparePartPackingID", DbType.Int32, Me.GetRefObject(sparePartPackingDetail.SparePartPacking))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartPackingDetail

            Dim sparePartPackingDetail As SparePartPackingDetail = New SparePartPackingDetail

            sparePartPackingDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("InternalHUItemNo")) Then sparePartPackingDetail.InternalHUItemNo = dr("InternalHUItemNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DOItemNo")) Then sparePartPackingDetail.DOItemNo = CType(dr("DOItemNo"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then sparePartPackingDetail.Qty = CType(dr("Qty"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("UoM")) Then sparePartPackingDetail.UoM = dr("UoM").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartPackingDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartPackingDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartPackingDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartPackingDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartPackingDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartDOID")) Then
                sparePartPackingDetail.SparePartDO = New SparePartDO(CType(dr("SparePartDOID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartMasterID")) Then
                sparePartPackingDetail.SparePartMaster = New SparePartMaster(CType(dr("SparePartMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartPackingID")) Then
                sparePartPackingDetail.SparePartPacking = New SparePartPacking(CType(dr("SparePartPackingID"), Integer))
            End If

            Return sparePartPackingDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartPackingDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartPackingDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartPackingDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

