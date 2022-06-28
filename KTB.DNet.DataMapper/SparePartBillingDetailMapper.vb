
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartBillingDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 9/29/2016 - 3:35:14 PM
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

    Public Class SparePartBillingDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartBillingDetail"
        Private m_UpdateStatement As String = "up_UpdateSparePartBillingDetail"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartBillingDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartBillingDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartBillingDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartBillingDetail As SparePartBillingDetail = Nothing
            While dr.Read

                sparePartBillingDetail = Me.CreateObject(dr)

            End While

            Return sparePartBillingDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartBillingDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartBillingDetail As SparePartBillingDetail = Me.CreateObject(dr)
                sparePartBillingDetailList.Add(sparePartBillingDetail)
            End While

            Return sparePartBillingDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartBillingDetail As SparePartBillingDetail = CType(obj, SparePartBillingDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartBillingDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartBillingDetail As SparePartBillingDetail = CType(obj, SparePartBillingDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@BillingItemNo", DbType.Int32, sparePartBillingDetail.BillingItemNo)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, sparePartBillingDetail.Quantity)
            DbCommandWrapper.AddInParameter("@ItemPrice", DbType.Currency, sparePartBillingDetail.ItemPrice)
            DbCommandWrapper.AddInParameter("@TotalPrice", DbType.Currency, sparePartBillingDetail.TotalPrice)
            DbCommandWrapper.AddInParameter("@Tax", DbType.Currency, sparePartBillingDetail.Tax)
            DbCommandWrapper.AddInParameter("@RetailPrice", DbType.Currency, sparePartBillingDetail.RetailPrice)
            DbCommandWrapper.AddInParameter("@Discount", DbType.Currency, sparePartBillingDetail.Discount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartBillingDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartBillingDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartDODetailID", DbType.Int32, Me.GetRefObject(sparePartBillingDetail.SparePartDODetail))
            DbCommandWrapper.AddInParameter("@SparePartBillingID", DbType.Int32, Me.GetRefObject(sparePartBillingDetail.SparePartBilling))

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

            Dim sparePartBillingDetail As SparePartBillingDetail = CType(obj, SparePartBillingDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartBillingDetail.ID)
            DbCommandWrapper.AddInParameter("@BillingItemNo", DbType.Int32, sparePartBillingDetail.BillingItemNo)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, sparePartBillingDetail.Quantity)
            DbCommandWrapper.AddInParameter("@ItemPrice", DbType.Currency, sparePartBillingDetail.ItemPrice)
            DbCommandWrapper.AddInParameter("@TotalPrice", DbType.Currency, sparePartBillingDetail.TotalPrice)
            DbCommandWrapper.AddInParameter("@Tax", DbType.Currency, sparePartBillingDetail.Tax)
            DbCommandWrapper.AddInParameter("@RetailPrice", DbType.Currency, sparePartBillingDetail.RetailPrice)
            DbCommandWrapper.AddInParameter("@Discount", DbType.Currency, sparePartBillingDetail.Discount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartBillingDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartBillingDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SparePartDODetailID", DbType.Int32, Me.GetRefObject(sparePartBillingDetail.SparePartDODetail))
            DbCommandWrapper.AddInParameter("@SparePartBillingID", DbType.Int32, Me.GetRefObject(sparePartBillingDetail.SparePartBilling))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartBillingDetail

            Dim sparePartBillingDetail As SparePartBillingDetail = New SparePartBillingDetail

            sparePartBillingDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BillingItemNo")) Then sparePartBillingDetail.BillingItemNo = CType(dr("BillingItemNo"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Quantity")) Then sparePartBillingDetail.Quantity = CType(dr("Quantity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ItemPrice")) Then sparePartBillingDetail.ItemPrice = CType(dr("ItemPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalPrice")) Then sparePartBillingDetail.TotalPrice = CType(dr("TotalPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Tax")) Then sparePartBillingDetail.Tax = CType(dr("Tax"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RetailPrice")) Then sparePartBillingDetail.RetailPrice = CType(dr("RetailPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Discount")) Then sparePartBillingDetail.Discount = CType(dr("Discount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartBillingDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartBillingDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartBillingDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartBillingDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartBillingDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartDODetailID")) Then
                sparePartBillingDetail.SparePartDODetail = New SparePartDODetail(CType(dr("SparePartDODetailID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartBillingID")) Then
                sparePartBillingDetail.SparePartBilling = New SparePartBilling(CType(dr("SparePartBillingID"), Integer))
            End If

            Return sparePartBillingDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartBillingDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartBillingDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartBillingDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

