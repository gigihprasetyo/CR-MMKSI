
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcContact Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2013 
'// ---------------------
'// $History      : $
'// Generated on 6/19/2013 - 2:49:25 PM
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
    <Serializable(), TableInfo("CcContact")> _
    Public Class CcContact
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Long)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Long
        'Private _dealerID As Short
        Private _dealer As Dealer
        Private _nonAuthorizedDealerID As Integer
        Private _sex As String = String.Empty
        Private _consumerName As String = String.Empty
        Private _handphoneNo As String = String.Empty
        Private _vehicleType As String = String.Empty
        Private _nameSTNK As String = String.Empty
        Private _addressSTNK As String = String.Empty
        Private _city As String = String.Empty
        Private _chassisNo As String = String.Empty
        Private _transactionDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _customerID As Integer
        Private _odometer As String = String.Empty
        Private _homePhoneAreaCode As String = String.Empty
        Private _homePhoneNo As String = String.Empty
        Private _officePhoneAreaCode As String = String.Empty
        Private _officePhoneNo As String = String.Empty
        Private _officePhoneNoExt As String = String.Empty
        Private _serviceType As String = String.Empty
        Private _ccContactStatusID As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _ccCustomerCategoryID As Short
        Private _ccPeriodID As Integer
        Private _ccVehicleCategoryID As Short



#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Long
            Get
                Return _iD
            End Get
            Set(ByVal value As Long)
                _iD = value
            End Set
        End Property


        '<ColumnInfo("DealerID", "{0}")> _
        'Public Property DealerID() As Short
        '    Get
        '        Return _dealerID
        '    End Get
        '    Set(ByVal value As Short)
        '        _dealerID = value
        '    End Set
        'End Property

        <ColumnInfo("DealerId", "{0}"), _
        RelationInfo("Dealer", "ID", "CcContact", "DealerId")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

                        Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), _dealer.ID), Dealer)
                        Me._dealer.MarkLoaded()

                    End If

                    Return Me._dealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._dealer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("NonAuthorizedDealerID", "{0}")> _
        Public Property NonAuthorizedDealerID() As Integer
            Get
                Return _nonAuthorizedDealerID
            End Get
            Set(ByVal value As Integer)
                _nonAuthorizedDealerID = value
            End Set
        End Property


        <ColumnInfo("Sex", "'{0}'")> _
        Public Property Sex() As String
            Get
                Return _sex
            End Get
            Set(ByVal value As String)
                _sex = value
            End Set
        End Property


        <ColumnInfo("ConsumerName", "'{0}'")> _
        Public Property ConsumerName() As String
            Get
                Return _consumerName
            End Get
            Set(ByVal value As String)
                _consumerName = value
            End Set
        End Property


        <ColumnInfo("HandphoneNo", "'{0}'")> _
        Public Property HandphoneNo() As String
            Get
                Return _handphoneNo
            End Get
            Set(ByVal value As String)
                _handphoneNo = value
            End Set
        End Property


        <ColumnInfo("VehicleType", "'{0}'")> _
        Public Property VehicleType() As String
            Get
                Return _vehicleType
            End Get
            Set(ByVal value As String)
                _vehicleType = value
            End Set
        End Property


        <ColumnInfo("NameSTNK", "'{0}'")> _
        Public Property NameSTNK() As String
            Get
                Return _nameSTNK
            End Get
            Set(ByVal value As String)
                _nameSTNK = value
            End Set
        End Property


        <ColumnInfo("AddressSTNK", "'{0}'")> _
        Public Property AddressSTNK() As String
            Get
                Return _addressSTNK
            End Get
            Set(ByVal value As String)
                _addressSTNK = value
            End Set
        End Property


        <ColumnInfo("City", "'{0}'")> _
        Public Property City() As String
            Get
                Return _city
            End Get
            Set(ByVal value As String)
                _city = value
            End Set
        End Property


        <ColumnInfo("ChassisNo", "'{0}'")> _
        Public Property ChassisNo() As String
            Get
                Return _chassisNo
            End Get
            Set(ByVal value As String)
                _chassisNo = value
            End Set
        End Property


        <ColumnInfo("TransactionDate", "'{0:yyyy/MM/dd}'")> _
        Public Property TransactionDate() As DateTime
            Get
                Return _transactionDate
            End Get
            Set(ByVal value As DateTime)
                _transactionDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("CustomerID", "{0}")> _
        Public Property CustomerID() As Integer
            Get
                Return _customerID
            End Get
            Set(ByVal value As Integer)
                _customerID = value
            End Set
        End Property


        <ColumnInfo("Odometer", "'{0}'")> _
        Public Property Odometer() As String
            Get
                Return _odometer
            End Get
            Set(ByVal value As String)
                _odometer = value
            End Set
        End Property


        <ColumnInfo("HomePhoneAreaCode", "'{0}'")> _
        Public Property HomePhoneAreaCode() As String
            Get
                Return _homePhoneAreaCode
            End Get
            Set(ByVal value As String)
                _homePhoneAreaCode = value
            End Set
        End Property


        <ColumnInfo("HomePhoneNo", "'{0}'")> _
        Public Property HomePhoneNo() As String
            Get
                Return _homePhoneNo
            End Get
            Set(ByVal value As String)
                _homePhoneNo = value
            End Set
        End Property


        <ColumnInfo("OfficePhoneAreaCode", "'{0}'")> _
        Public Property OfficePhoneAreaCode() As String
            Get
                Return _officePhoneAreaCode
            End Get
            Set(ByVal value As String)
                _officePhoneAreaCode = value
            End Set
        End Property


        <ColumnInfo("OfficePhoneNo", "'{0}'")> _
        Public Property OfficePhoneNo() As String
            Get
                Return _officePhoneNo
            End Get
            Set(ByVal value As String)
                _officePhoneNo = value
            End Set
        End Property


        <ColumnInfo("OfficePhoneNoExt", "'{0}'")> _
        Public Property OfficePhoneNoExt() As String
            Get
                Return _officePhoneNoExt
            End Get
            Set(ByVal value As String)
                _officePhoneNoExt = value
            End Set
        End Property


        <ColumnInfo("ServiceType", "'{0}'")> _
        Public Property ServiceType() As String
            Get
                Return _serviceType
            End Get
            Set(ByVal value As String)
                _serviceType = value
            End Set
        End Property


        <ColumnInfo("CcContactStatusID", "{0}")> _
        Public Property CcContactStatusID() As Short
            Get
                Return _ccContactStatusID
            End Get
            Set(ByVal value As Short)
                _ccContactStatusID = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus() As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy() As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy() As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("CcCustomerCategoryID", "{0}")> _
        Public Property CcCustomerCategoryID() As Short

            Get
                Return _ccCustomerCategoryID
            End Get
            Set(ByVal value As Short)
                _ccCustomerCategoryID = value
            End Set
        End Property
        <ColumnInfo("CcPeriodID", "{0}")> _
        Public Property CcPeriodID() As Integer

            Get
                Return _ccPeriodID
            End Get
            Set(ByVal value As Integer)
                _ccPeriodID = value
            End Set
        End Property
        <ColumnInfo("CcVehicleCategoryID", "{0}")> _
        Public Property CcVehicleCategoryID() As Short

            Get
                Return _ccVehicleCategoryID
            End Get
            Set(ByVal value As Short)
                _ccVehicleCategoryID = value
            End Set
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

