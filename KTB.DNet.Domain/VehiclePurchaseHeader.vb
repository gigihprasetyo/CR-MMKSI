
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VehiclePurchaseHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 21/03/2018 - 17:21:55
'//
'// ===========================================================================	
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("VehiclePurchaseHeader")> _
    Public Class VehiclePurchaseHeader
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Integer
        Private _bUCode As String = String.Empty
        Private _bUName As String = String.Empty
        Private _deliveryMethod As String = String.Empty
        Private _description As String = String.Empty
        Private _pRPOTypeName As String = String.Empty
        Private _dMSPONo As String = String.Empty
        Private _dMSPOStatus As Integer
        Private _dMSPODate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _vendorDescription As String = String.Empty
        Private _vendor As String = String.Empty
        Private _purchaseOrderNo As String = String.Empty
        Private _purchaseReceiptNo As String = String.Empty
        Private _purchaseReceiptDetailNo As String = String.Empty
        Private _chassisModel As String = String.Empty
        Private _chassisNumberRegister As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _vehiclePurchaseDetails As System.Collections.ArrayList = New System.Collections.ArrayList()


#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("BUCode", "'{0}'")> _
        Public Property BUCode As String
            Get
                Return _bUCode
            End Get
            Set(ByVal value As String)
                _bUCode = value
            End Set
        End Property


        <ColumnInfo("BUName", "'{0}'")> _
        Public Property BUName As String
            Get
                Return _bUName
            End Get
            Set(ByVal value As String)
                _bUName = value
            End Set
        End Property


        <ColumnInfo("DeliveryMethod", "'{0}'")> _
        Public Property DeliveryMethod As String
            Get
                Return _deliveryMethod
            End Get
            Set(ByVal value As String)
                _deliveryMethod = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("PRPOTypeName", "'{0}'")> _
        Public Property PRPOTypeName As String
            Get
                Return _pRPOTypeName
            End Get
            Set(ByVal value As String)
                _pRPOTypeName = value
            End Set
        End Property


        <ColumnInfo("DMSPONo", "'{0}'")> _
        Public Property DMSPONo As String
            Get
                Return _dMSPONo
            End Get
            Set(ByVal value As String)
                _dMSPONo = value
            End Set
        End Property


        <ColumnInfo("DMSPOStatus", "{0}")> _
        Public Property DMSPOStatus As Integer
            Get
                Return _dMSPOStatus
            End Get
            Set(ByVal value As Integer)
                _dMSPOStatus = value
            End Set
        End Property


        <ColumnInfo("DMSPODate", "'{0:yyyy/MM/dd}'")> _
        Public Property DMSPODate As DateTime
            Get
                Return _dMSPODate
            End Get
            Set(ByVal value As DateTime)
                _dMSPODate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("VendorDescription", "'{0}'")> _
        Public Property VendorDescription As String
            Get
                Return _vendorDescription
            End Get
            Set(ByVal value As String)
                _vendorDescription = value
            End Set
        End Property


        <ColumnInfo("Vendor", "'{0}'")> _
        Public Property Vendor As String
            Get
                Return _vendor
            End Get
            Set(ByVal value As String)
                _vendor = value
            End Set
        End Property


        <ColumnInfo("PurchaseOrderNo", "'{0}'")> _
        Public Property PurchaseOrderNo As String
            Get
                Return _purchaseOrderNo
            End Get
            Set(ByVal value As String)
                _purchaseOrderNo = value
            End Set
        End Property


        <ColumnInfo("PurchaseReceiptNo", "'{0}'")> _
        Public Property PurchaseReceiptNo As String
            Get
                Return _purchaseReceiptNo
            End Get
            Set(ByVal value As String)
                _purchaseReceiptNo = value
            End Set
        End Property


        <ColumnInfo("PurchaseReceiptDetailNo", "'{0}'")> _
        Public Property PurchaseReceiptDetailNo As String
            Get
                Return _purchaseReceiptDetailNo
            End Get
            Set(ByVal value As String)
                _purchaseReceiptDetailNo = value
            End Set
        End Property


        <ColumnInfo("ChassisModel", "'{0}'")> _
        Public Property ChassisModel As String
            Get
                Return _chassisModel
            End Get
            Set(ByVal value As String)
                _chassisModel = value
            End Set
        End Property


        <ColumnInfo("ChassisNumberRegister", "'{0}'")> _
        Public Property ChassisNumberRegister As String
            Get
                Return _chassisNumberRegister
            End Get
            Set(ByVal value As String)
                _chassisNumberRegister = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property



        <RelationInfo("VehiclePurchaseHeader", "ID", "VehiclePurchaseDetail", "VehiclePurchaseHeaderID")> _
        Public ReadOnly Property VehiclePurchaseDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._vehiclePurchaseDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(VehiclePurchaseDetail), "VehiclePurchaseHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(VehiclePurchaseDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._vehiclePurchaseDetails = DoLoadArray(GetType(VehiclePurchaseDetail).ToString, criterias)
                    End If

                    Return Me._vehiclePurchaseDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property


#End Region

#Region "Generated Method"
        Public Function GetStrDate(ByVal dateInput As DateTime, ByVal dateFormat As String) As String
            If dateInput = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                Return ""
            Else
                Return Format(dateInput, dateFormat)
            End If
        End Function
#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

