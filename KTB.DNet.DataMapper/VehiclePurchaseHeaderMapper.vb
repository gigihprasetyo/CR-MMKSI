
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : MitraisTeam
'// PURPOSE       : VehiclePurchaseHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 21/03/2018 - 17:23:25
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

    Public Class VehiclePurchaseHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVehiclePurchaseHeader"
        Private m_UpdateStatement As String = "up_UpdateVehiclePurchaseHeader"
        Private m_RetrieveStatement As String = "up_RetrieveVehiclePurchaseHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveVehiclePurchaseHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVehiclePurchaseHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vehiclePurchaseHeader As VehiclePurchaseHeader = Nothing
            While dr.Read

                vehiclePurchaseHeader = Me.CreateObject(dr)

            End While

            Return vehiclePurchaseHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vehiclePurchaseHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim vehiclePurchaseHeader As VehiclePurchaseHeader = Me.CreateObject(dr)
                vehiclePurchaseHeaderList.Add(vehiclePurchaseHeader)
            End While

            Return vehiclePurchaseHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vehiclePurchaseHeader As VehiclePurchaseHeader = CType(obj, VehiclePurchaseHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vehiclePurchaseHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vehiclePurchaseHeader As VehiclePurchaseHeader = CType(obj, VehiclePurchaseHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@BUCode", DbType.AnsiString, vehiclePurchaseHeader.BUCode)
            DbCommandWrapper.AddInParameter("@BUName", DbType.AnsiString, vehiclePurchaseHeader.BUName)
            DbCommandWrapper.AddInParameter("@DeliveryMethod", DbType.AnsiString, vehiclePurchaseHeader.DeliveryMethod)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, vehiclePurchaseHeader.Description)
            DbCommandWrapper.AddInParameter("@PRPOTypeName", DbType.AnsiString, vehiclePurchaseHeader.PRPOTypeName)
            DbCommandWrapper.AddInParameter("@DMSPONo", DbType.AnsiString, vehiclePurchaseHeader.DMSPONo)
            DbCommandWrapper.AddInParameter("@DMSPOStatus", DbType.Int32, vehiclePurchaseHeader.DMSPOStatus)
            DbCommandWrapper.AddInParameter("@DMSPODate", DbType.DateTime, vehiclePurchaseHeader.DMSPODate)
            DbCommandWrapper.AddInParameter("@VendorDescription", DbType.AnsiString, vehiclePurchaseHeader.VendorDescription)
            DbCommandWrapper.AddInParameter("@Vendor", DbType.AnsiString, vehiclePurchaseHeader.Vendor)
            DbCommandWrapper.AddInParameter("@PurchaseOrderNo", DbType.AnsiString, vehiclePurchaseHeader.PurchaseOrderNo)
            DbCommandWrapper.AddInParameter("@PurchaseReceiptNo", DbType.AnsiString, vehiclePurchaseHeader.PurchaseReceiptNo)
            DbCommandWrapper.AddInParameter("@PurchaseReceiptDetailNo", DbType.AnsiString, vehiclePurchaseHeader.PurchaseReceiptDetailNo)
            DbCommandWrapper.AddInParameter("@ChassisModel", DbType.AnsiString, vehiclePurchaseHeader.ChassisModel)
            DbCommandWrapper.AddInParameter("@ChassisNumberRegister", DbType.AnsiString, vehiclePurchaseHeader.ChassisNumberRegister)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vehiclePurchaseHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, vehiclePurchaseHeader.LastUpdateBy)
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

            Dim vehiclePurchaseHeader As VehiclePurchaseHeader = CType(obj, VehiclePurchaseHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vehiclePurchaseHeader.ID)
            DbCommandWrapper.AddInParameter("@BUCode", DbType.AnsiString, vehiclePurchaseHeader.BUCode)
            DbCommandWrapper.AddInParameter("@BUName", DbType.AnsiString, vehiclePurchaseHeader.BUName)
            DbCommandWrapper.AddInParameter("@DeliveryMethod", DbType.AnsiString, vehiclePurchaseHeader.DeliveryMethod)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, vehiclePurchaseHeader.Description)
            DbCommandWrapper.AddInParameter("@PRPOTypeName", DbType.AnsiString, vehiclePurchaseHeader.PRPOTypeName)
            DbCommandWrapper.AddInParameter("@DMSPONo", DbType.AnsiString, vehiclePurchaseHeader.DMSPONo)
            DbCommandWrapper.AddInParameter("@DMSPOStatus", DbType.Int32, vehiclePurchaseHeader.DMSPOStatus)
            DbCommandWrapper.AddInParameter("@DMSPODate", DbType.DateTime, vehiclePurchaseHeader.DMSPODate)
            DbCommandWrapper.AddInParameter("@VendorDescription", DbType.AnsiString, vehiclePurchaseHeader.VendorDescription)
            DbCommandWrapper.AddInParameter("@Vendor", DbType.AnsiString, vehiclePurchaseHeader.Vendor)
            DbCommandWrapper.AddInParameter("@PurchaseOrderNo", DbType.AnsiString, vehiclePurchaseHeader.PurchaseOrderNo)
            DbCommandWrapper.AddInParameter("@PurchaseReceiptNo", DbType.AnsiString, vehiclePurchaseHeader.PurchaseReceiptNo)
            DbCommandWrapper.AddInParameter("@PurchaseReceiptDetailNo", DbType.AnsiString, vehiclePurchaseHeader.PurchaseReceiptDetailNo)
            DbCommandWrapper.AddInParameter("@ChassisModel", DbType.AnsiString, vehiclePurchaseHeader.ChassisModel)
            DbCommandWrapper.AddInParameter("@ChassisNumberRegister", DbType.AnsiString, vehiclePurchaseHeader.ChassisNumberRegister)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vehiclePurchaseHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, vehiclePurchaseHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VehiclePurchaseHeader

            Dim vehiclePurchaseHeader As VehiclePurchaseHeader = New VehiclePurchaseHeader

            vehiclePurchaseHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BUCode")) Then vehiclePurchaseHeader.BUCode = dr("BUCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BUName")) Then vehiclePurchaseHeader.BUName = dr("BUName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DeliveryMethod")) Then vehiclePurchaseHeader.DeliveryMethod = dr("DeliveryMethod").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then vehiclePurchaseHeader.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PRPOTypeName")) Then vehiclePurchaseHeader.PRPOTypeName = dr("PRPOTypeName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DMSPONo")) Then vehiclePurchaseHeader.DMSPONo = dr("DMSPONo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DMSPOStatus")) Then vehiclePurchaseHeader.DMSPOStatus = CType(dr("DMSPOStatus"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DMSPODate")) Then vehiclePurchaseHeader.DMSPODate = CType(dr("DMSPODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("VendorDescription")) Then vehiclePurchaseHeader.VendorDescription = dr("VendorDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Vendor")) Then vehiclePurchaseHeader.Vendor = dr("Vendor").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PurchaseOrderNo")) Then vehiclePurchaseHeader.PurchaseOrderNo = dr("PurchaseOrderNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PurchaseReceiptNo")) Then vehiclePurchaseHeader.PurchaseReceiptNo = dr("PurchaseReceiptNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PurchaseReceiptDetailNo")) Then vehiclePurchaseHeader.PurchaseReceiptDetailNo = dr("PurchaseReceiptDetailNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisModel")) Then vehiclePurchaseHeader.ChassisModel = dr("ChassisModel").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumberRegister")) Then vehiclePurchaseHeader.ChassisNumberRegister = dr("ChassisNumberRegister").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then vehiclePurchaseHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then vehiclePurchaseHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then vehiclePurchaseHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then vehiclePurchaseHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vehiclePurchaseHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return vehiclePurchaseHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(VehiclePurchaseHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VehiclePurchaseHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VehiclePurchaseHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

