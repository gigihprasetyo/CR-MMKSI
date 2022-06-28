#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_SparePartPODOHaveBillingDetailMapper Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 01/03/2019 - 7:40:23
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

    Public Class VWI_SparePartPODOHaveBillingDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartPODOHaveBillingDetail"
        Private m_UpdateStatement As String = "up_UpdateSparePartPODOHaveBillingDetail"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartPODOHaveBillingDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartPODOHaveBillingDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartPODOHaveBillingDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pODO_HaveBillingDetailDetail As VWI_SparePartPODOHaveBillingDetail = Nothing
            While dr.Read

                pODO_HaveBillingDetailDetail = Me.CreateObject(dr)

            End While

            Return pODO_HaveBillingDetailDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pODO_HaveBillingDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim pODO_HaveBillingDetail As VWI_SparePartPODOHaveBillingDetail = Me.CreateObject(dr)
                pODO_HaveBillingDetailList.Add(pODO_HaveBillingDetail)
            End While

            Return pODO_HaveBillingDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pODO_HaveBillingDetail As VWI_SparePartPODOHaveBillingDetail = CType(obj, VWI_SparePartPODOHaveBillingDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pODO_HaveBillingDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pODO_HaveBillingDetail As VWI_SparePartPODOHaveBillingDetail = CType(obj, VWI_SparePartPODOHaveBillingDetail)
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

            Dim pODO_HaveBillingDetail As VWI_SparePartPODOHaveBillingDetail = CType(obj, VWI_SparePartPODOHaveBillingDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_SparePartPODOHaveBillingDetail

            Dim pODO_HaveBillingDetail As VWI_SparePartPODOHaveBillingDetail = New VWI_SparePartPODOHaveBillingDetail

            pODO_HaveBillingDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartDOID")) Then pODO_HaveBillingDetail.SONumber = CType(dr("SparePartDOID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then pODO_HaveBillingDetail.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PartNumber")) Then pODO_HaveBillingDetail.PartNumber = dr("PartNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PartName")) Then pODO_HaveBillingDetail.PartName = dr("PartName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then pODO_HaveBillingDetail.Qty = CType(dr("Qty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Tax")) Then pODO_HaveBillingDetail.Tax = CType(dr("Tax"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Discount")) Then pODO_HaveBillingDetail.Tax = CType(dr("Discount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RetailPrice")) Then pODO_HaveBillingDetail.Tax = CType(dr("RetailPrice"), Decimal)
            Return pODO_HaveBillingDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_SparePartPODOHaveBillingDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_SparePartPODOHaveBillingDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_SparePartPODOHaveBillingDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

