
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartOutstandingOrderDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 10/26/2015 - 4:30:58 PM
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

    Public Class SparePartOutstandingOrderDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartOutstandingOrderDetail"
        Private m_UpdateStatement As String = "up_UpdateSparePartOutstandingOrderDetail"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartOutstandingOrderDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartOutstandingOrderDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartOutstandingOrderDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartOutstandingOrderDetail As SparePartOutstandingOrderDetail = Nothing
            While dr.Read

                sparePartOutstandingOrderDetail = Me.CreateObject(dr)

            End While

            Return sparePartOutstandingOrderDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartOutstandingOrderDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartOutstandingOrderDetail As SparePartOutstandingOrderDetail = Me.CreateObject(dr)
                sparePartOutstandingOrderDetailList.Add(sparePartOutstandingOrderDetail)
            End While

            Return sparePartOutstandingOrderDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartOutstandingOrderDetail As SparePartOutstandingOrderDetail = CType(obj, SparePartOutstandingOrderDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartOutstandingOrderDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartOutstandingOrderDetail As SparePartOutstandingOrderDetail = CType(obj, SparePartOutstandingOrderDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PartNumber", DbType.AnsiString, sparePartOutstandingOrderDetail.PartNumber)
            DbCommandWrapper.AddInParameter("@PartName", DbType.AnsiString, sparePartOutstandingOrderDetail.PartName)
            DbCommandWrapper.AddInParameter("@OrderQty", DbType.Int32, sparePartOutstandingOrderDetail.OrderQty)
            DbCommandWrapper.AddInParameter("@AllocationQty", DbType.Int32, sparePartOutstandingOrderDetail.AllocationQty)
            DbCommandWrapper.AddInParameter("@AllocationAmount", DbType.Currency, sparePartOutstandingOrderDetail.AllocationAmount)
            DbCommandWrapper.AddInParameter("@OpenQty", DbType.Int32, sparePartOutstandingOrderDetail.OpenQty)
            DbCommandWrapper.AddInParameter("@OpenAmount", DbType.Currency, sparePartOutstandingOrderDetail.OpenAmount)
            DbCommandWrapper.AddInParameter("@SubtitutePartNumber", DbType.AnsiString, sparePartOutstandingOrderDetail.SubtitutePartNumber)
            DbCommandWrapper.AddInParameter("@EstimateFillDate", DbType.DateTime, sparePartOutstandingOrderDetail.EstimateFillDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sparePartOutstandingOrderDetail.Status)
            DbCommandWrapper.AddInParameter("@IsTransfer", DbType.Int16, sparePartOutstandingOrderDetail.IsTransfer)
            DbCommandWrapper.AddInParameter("@EstimateFillQty", DbType.Int32, sparePartOutstandingOrderDetail.EstimateFillQty)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartOutstandingOrderDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartOutstandingOrderDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartOutstandingOrderID", DbType.Int32, Me.GetRefObject(sparePartOutstandingOrderDetail.SparePartOutstandingOrder))

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

            Dim sparePartOutstandingOrderDetail As SparePartOutstandingOrderDetail = CType(obj, SparePartOutstandingOrderDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartOutstandingOrderDetail.ID)
            DbCommandWrapper.AddInParameter("@PartNumber", DbType.AnsiString, sparePartOutstandingOrderDetail.PartNumber)
            DbCommandWrapper.AddInParameter("@PartName", DbType.AnsiString, sparePartOutstandingOrderDetail.PartName)
            DbCommandWrapper.AddInParameter("@OrderQty", DbType.Int32, sparePartOutstandingOrderDetail.OrderQty)
            DbCommandWrapper.AddInParameter("@AllocationQty", DbType.Int32, sparePartOutstandingOrderDetail.AllocationQty)
            DbCommandWrapper.AddInParameter("@AllocationAmount", DbType.Currency, sparePartOutstandingOrderDetail.AllocationAmount)
            DbCommandWrapper.AddInParameter("@OpenQty", DbType.Int32, sparePartOutstandingOrderDetail.OpenQty)
            DbCommandWrapper.AddInParameter("@OpenAmount", DbType.Currency, sparePartOutstandingOrderDetail.OpenAmount)
            DbCommandWrapper.AddInParameter("@SubtitutePartNumber", DbType.AnsiString, sparePartOutstandingOrderDetail.SubtitutePartNumber)
            DbCommandWrapper.AddInParameter("@EstimateFillDate", DbType.DateTime, sparePartOutstandingOrderDetail.EstimateFillDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sparePartOutstandingOrderDetail.Status)
            DbCommandWrapper.AddInParameter("@IsTransfer", DbType.Int16, sparePartOutstandingOrderDetail.IsTransfer)
            DbCommandWrapper.AddInParameter("@EstimateFillQty", DbType.Int32, sparePartOutstandingOrderDetail.EstimateFillQty)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartOutstandingOrderDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartOutstandingOrderDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SparePartOutstandingOrderID", DbType.Int32, Me.GetRefObject(sparePartOutstandingOrderDetail.SparePartOutstandingOrder))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartOutstandingOrderDetail

            Dim sparePartOutstandingOrderDetail As SparePartOutstandingOrderDetail = New SparePartOutstandingOrderDetail

            sparePartOutstandingOrderDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PartNumber")) Then sparePartOutstandingOrderDetail.PartNumber = dr("PartNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PartName")) Then sparePartOutstandingOrderDetail.PartName = dr("PartName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderQty")) Then sparePartOutstandingOrderDetail.OrderQty = CType(dr("OrderQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AllocationQty")) Then sparePartOutstandingOrderDetail.AllocationQty = CType(dr("AllocationQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AllocationAmount")) Then sparePartOutstandingOrderDetail.AllocationAmount = CType(dr("AllocationAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("OpenQty")) Then sparePartOutstandingOrderDetail.OpenQty = CType(dr("OpenQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("OpenAmount")) Then sparePartOutstandingOrderDetail.OpenAmount = CType(dr("OpenAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("SubtitutePartNumber")) Then sparePartOutstandingOrderDetail.SubtitutePartNumber = dr("SubtitutePartNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EstimateFillDate")) Then sparePartOutstandingOrderDetail.EstimateFillDate = CType(dr("EstimateFillDate"), Date)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sparePartOutstandingOrderDetail.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("IsTransfer")) Then sparePartOutstandingOrderDetail.IsTransfer = CType(dr("IsTransfer"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("EstimateFillQty")) Then sparePartOutstandingOrderDetail.EstimateFillQty = CType(dr("EstimateFillQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartOutstandingOrderDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartOutstandingOrderDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartOutstandingOrderDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartOutstandingOrderDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartOutstandingOrderDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartOutstandingOrderID")) Then
                sparePartOutstandingOrderDetail.SparePartOutstandingOrder = New SparePartOutstandingOrder(CType(dr("SparePartOutstandingOrderID"), Integer))
            End If

            Return sparePartOutstandingOrderDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartOutstandingOrderDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartOutstandingOrderDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartOutstandingOrderDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

