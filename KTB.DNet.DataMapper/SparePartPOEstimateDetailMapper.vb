
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPOEstimateDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 10/27/2015 - 3:28:15 PM
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

    Public Class SparePartPOEstimateDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartPOEstimateDetail"
        Private m_UpdateStatement As String = "up_UpdateSparePartPOEstimateDetail"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartPOEstimateDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartPOEstimateDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartPOEstimateDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartPOEstimateDetail As SparePartPOEstimateDetail = Nothing
            While dr.Read

                sparePartPOEstimateDetail = Me.CreateObject(dr)

            End While

            Return sparePartPOEstimateDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartPOEstimateDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartPOEstimateDetail As SparePartPOEstimateDetail = Me.CreateObject(dr)
                sparePartPOEstimateDetailList.Add(sparePartPOEstimateDetail)
            End While

            Return sparePartPOEstimateDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPOEstimateDetail As SparePartPOEstimateDetail = CType(obj, SparePartPOEstimateDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPOEstimateDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPOEstimateDetail As SparePartPOEstimateDetail = CType(obj, SparePartPOEstimateDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@PartNumber", DbType.AnsiString, sparePartPOEstimateDetail.PartNumber)
            DbCommandWrapper.AddInParameter("@PartName", DbType.AnsiString, sparePartPOEstimateDetail.PartName)
            DbCommandWrapper.AddInParameter("@OrderQty", DbType.Int32, sparePartPOEstimateDetail.OrderQty)
            DbCommandWrapper.AddInParameter("@AllocQty", DbType.Int32, sparePartPOEstimateDetail.AllocQty)
            DbCommandWrapper.AddInParameter("@AllocationQty", DbType.Int32, sparePartPOEstimateDetail.AllocationQty)
            DbCommandWrapper.AddInParameter("@OpenQty", DbType.Int32, sparePartPOEstimateDetail.OpenQty)
            DbCommandWrapper.AddInParameter("@RetailPrice", DbType.Currency, sparePartPOEstimateDetail.RetailPrice)
            DbCommandWrapper.AddInParameter("@AltPartNumber", DbType.AnsiString, sparePartPOEstimateDetail.AltPartNumber)
            DbCommandWrapper.AddInParameter("@Discount", DbType.Currency, sparePartPOEstimateDetail.Discount)
            DbCommandWrapper.AddInParameter("@ItemStatus", DbType.AnsiString, sparePartPOEstimateDetail.ItemStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPOEstimateDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartPOEstimateDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartPOEstimateID", DbType.Int32, Me.GetRefObject(sparePartPOEstimateDetail.SparePartPOEstimate))

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

            Dim sparePartPOEstimateDetail As SparePartPOEstimateDetail = CType(obj, SparePartPOEstimateDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPOEstimateDetail.ID)
            DbCommandWrapper.AddInParameter("@PartNumber", DbType.AnsiString, sparePartPOEstimateDetail.PartNumber)
            DbCommandWrapper.AddInParameter("@PartName", DbType.AnsiString, sparePartPOEstimateDetail.PartName)
            DbCommandWrapper.AddInParameter("@OrderQty", DbType.Int32, sparePartPOEstimateDetail.OrderQty)
            DbCommandWrapper.AddInParameter("@AllocQty", DbType.Int32, sparePartPOEstimateDetail.AllocQty)
            DbCommandWrapper.AddInParameter("@AllocationQty", DbType.Int32, sparePartPOEstimateDetail.AllocationQty)
            DbCommandWrapper.AddInParameter("@OpenQty", DbType.Int32, sparePartPOEstimateDetail.OpenQty)
            DbCommandWrapper.AddInParameter("@RetailPrice", DbType.Currency, sparePartPOEstimateDetail.RetailPrice)
            DbCommandWrapper.AddInParameter("@AltPartNumber", DbType.AnsiString, sparePartPOEstimateDetail.AltPartNumber)
            DbCommandWrapper.AddInParameter("@Discount", DbType.Currency, sparePartPOEstimateDetail.Discount)
            DbCommandWrapper.AddInParameter("@ItemStatus", DbType.AnsiString, sparePartPOEstimateDetail.ItemStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPOEstimateDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartPOEstimateDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SparePartPOEstimateID", DbType.Int32, Me.GetRefObject(sparePartPOEstimateDetail.SparePartPOEstimate))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartPOEstimateDetail

            Dim sparePartPOEstimateDetail As SparePartPOEstimateDetail = New SparePartPOEstimateDetail

            sparePartPOEstimateDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PartNumber")) Then sparePartPOEstimateDetail.PartNumber = dr("PartNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PartName")) Then sparePartPOEstimateDetail.PartName = dr("PartName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderQty")) Then sparePartPOEstimateDetail.OrderQty = CType(dr("OrderQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AllocQty")) Then sparePartPOEstimateDetail.AllocQty = CType(dr("AllocQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AllocationQty")) Then sparePartPOEstimateDetail.AllocationQty = CType(dr("AllocationQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("OpenQty")) Then sparePartPOEstimateDetail.OpenQty = CType(dr("OpenQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RetailPrice")) Then sparePartPOEstimateDetail.RetailPrice = CType(dr("RetailPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AltPartNumber")) Then sparePartPOEstimateDetail.AltPartNumber = dr("AltPartNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Discount")) Then sparePartPOEstimateDetail.Discount = CType(dr("Discount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ItemStatus")) Then sparePartPOEstimateDetail.ItemStatus = dr("ItemStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartPOEstimateDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartPOEstimateDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartPOEstimateDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartPOEstimateDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartPOEstimateDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartPOEstimateID")) Then
                sparePartPOEstimateDetail.SparePartPOEstimate = New SparePartPOEstimate(CType(dr("SparePartPOEstimateID"), Integer))
            End If

            Return sparePartPOEstimateDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartPOEstimateDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartPOEstimateDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartPOEstimateDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

