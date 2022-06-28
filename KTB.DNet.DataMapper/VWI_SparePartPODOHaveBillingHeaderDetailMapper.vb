#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_SparePartPODOHaveBillingHeaderDetailMapper Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 04/03/2019 - 7:40:23
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

    Public Class VWI_SparePartPODOHaveBillingHeaderDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartPODOHaveBillingHeaderDetail"
        Private m_UpdateStatement As String = "up_UpdateSparePartPODOHaveBillingHeaderDetail"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartPODOHaveBillingHeaderDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartPODOHaveBillingHeaderDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartPODOHaveBillingHeaderDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pODO_HaveBillingHeaderDetail As VWI_SparePartPODOHaveBillingHeaderDetail = Nothing
            While dr.Read

                pODO_HaveBillingHeaderDetail = Me.CreateObject(dr)

            End While

            Return pODO_HaveBillingHeaderDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pODO_HaveBillingHeaderDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim pODO_HaveBillingHeaderDetail As VWI_SparePartPODOHaveBillingHeaderDetail = Me.CreateObject(dr)
                pODO_HaveBillingHeaderDetailList.Add(pODO_HaveBillingHeaderDetail)
            End While

            Return pODO_HaveBillingHeaderDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pODO_HaveBillingHeaderDetail As VWI_SparePartPODOHaveBillingHeaderDetail = CType(obj, VWI_SparePartPODOHaveBillingHeaderDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pODO_HaveBillingHeaderDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pODO_HaveBillingHeaderDetail As VWI_SparePartPODOHaveBillingHeaderDetail = CType(obj, VWI_SparePartPODOHaveBillingHeaderDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


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

            Dim pODO_HaveBillingHeaderDetail As VWI_SparePartPODOHaveBillingHeaderDetail = CType(obj, VWI_SparePartPODOHaveBillingHeaderDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_SparePartPODOHaveBillingHeaderDetail

            Dim pODO_HaveBillingHeaderDetail As VWI_SparePartPODOHaveBillingHeaderDetail = New VWI_SparePartPODOHaveBillingHeaderDetail

            pODO_HaveBillingHeaderDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartDOID")) Then pODO_HaveBillingHeaderDetail.SparePartDOID = CType(dr("SparePartDOID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DONumber")) Then pODO_HaveBillingHeaderDetail.DONumber = dr("DONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then pODO_HaveBillingHeaderDetail.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then pODO_HaveBillingHeaderDetail.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DoDate")) Then pODO_HaveBillingHeaderDetail.DoDate = CType(dr("DoDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DueDate")) Then pODO_HaveBillingHeaderDetail.DueDate = CType(dr("DueDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BillingDate")) Then pODO_HaveBillingHeaderDetail.BillingDate = CType(dr("BillingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BillingNumber")) Then pODO_HaveBillingHeaderDetail.BillingNumber = dr("BillingNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ExpeditionNo")) Then pODO_HaveBillingHeaderDetail.ExpeditionNo = dr("ExpeditionNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentValue")) Then pODO_HaveBillingHeaderDetail.TermOfPaymentValue = CType(dr("TermOfPaymentValue"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentCode")) Then pODO_HaveBillingHeaderDetail.TermOfPaymentCode = dr("TermOfPaymentCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentDesc")) Then pODO_HaveBillingHeaderDetail.TermOfPaymentDesc = dr("TermOfPaymentDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pODO_HaveBillingHeaderDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then pODO_HaveBillingHeaderDetail.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PartNumber")) Then pODO_HaveBillingHeaderDetail.PartNumber = dr("PartNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PartName")) Then pODO_HaveBillingHeaderDetail.PartName = dr("PartName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then pODO_HaveBillingHeaderDetail.Qty = CType(dr("Qty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Tax")) Then pODO_HaveBillingHeaderDetail.Tax = CType(dr("Tax"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Discount")) Then pODO_HaveBillingHeaderDetail.Tax = CType(dr("Discount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RetailPrice")) Then pODO_HaveBillingHeaderDetail.Tax = CType(dr("RetailPrice"), Decimal)
            Return pODO_HaveBillingHeaderDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_SparePartPODOHaveBillingHeaderDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_SparePartPODOHaveBillingHeaderDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_SparePartPODOHaveBillingHeaderDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


