
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Vw_ChassisMaster Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 03/02/2018 - 3:46:32 PM
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
    <Serializable(), TableInfo("Vw_ChassisMaster")> _
    Public Class Vw_ChassisMaster
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
        'Private _endCustomerID As Integer
        Private _chassisNumber As String = String.Empty
        'Private _categoryID As Byte
        'Private _vechileColorID As Short
        'Private _vehicleKindID As Integer
        'Private _soldDealerID As Short
        Private _dONumber As String = String.Empty
        Private _sONumber As String = String.Empty
        'Private _tOPID As Integer
        Private _discountAmount As Decimal
        Private _pONumber As String = String.Empty
        Private _engineNumber As String = String.Empty
        Private _serialNumber As String = String.Empty
        Private _dODate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _gIDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _parkingDays As Integer
        Private _parkingAmount As Decimal
        Private _fakturStatus As String = String.Empty
        Private _pendingDesc As String = String.Empty
        Private _isSAPDownload As String = String.Empty
        Private _stockDealer As Short
        Private _stockDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _productionYear As Short
        Private _stockStatus As String = String.Empty
        Private _lastUpdateProfile As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _alreadySaled As Byte
        Private _alreadySaledTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _chassisMasterDocumentID As Integer
        Private _engineImagePath As String = String.Empty
        Private _chassisImagePath As String = String.Empty
        Private _uploadDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _chassisMasterDocumentStatus As Short

        Private _endCustomer As EndCustomer
        Private _category As Category
        Private _dealer As Dealer
        Private _vehicleKind As VehicleKind
        Private _termOfPayment As TermOfPayment
        Private _vechileColor As VechileColor
        Private _chassisMasterDocument As ChassisMasterDocument
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

 

        <ColumnInfo("ChassisNumber", "'{0}'")> _
        Public Property ChassisNumber As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
            End Set
        End Property

        <ColumnInfo("EndCustomerID", "{0}"), _
    RelationInfo("EndCustomer", "ID", "Vw_ChassisMaster", "EndCustomerID")> _
        Public Property EndCustomer() As EndCustomer
            Get
                Try
                    If Not IsNothing(Me._endCustomer) AndAlso (Not Me._endCustomer.IsLoaded) Then

                        Me._endCustomer = CType(DoLoad(GetType(EndCustomer).ToString(), _endCustomer.ID), EndCustomer)
                        Me._endCustomer.MarkLoaded()

                    End If

                    Return Me._endCustomer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As EndCustomer)

                Me._endCustomer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._endCustomer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ChassisMasterDocumentID", "{0}"), _
   RelationInfo("ChassisMasterDocument", "ID", "Vw_ChassisMaster", "ChassisMasterDocumentID")> _
        Public Property ChassisMasterDocument() As ChassisMasterDocument
            Get
                Try
                    If Not IsNothing(Me._chassisMasterDocument) AndAlso (Not Me._chassisMasterDocument.IsLoaded) Then

                        Me._chassisMasterDocument = CType(DoLoad(GetType(ChassisMasterDocument).ToString(), _chassisMasterDocument.ID), ChassisMasterDocument)
                        Me._chassisMasterDocument.MarkLoaded()

                    End If

                    Return Me._chassisMasterDocument

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ChassisMasterDocument)

                Me._chassisMasterDocument = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._chassisMasterDocument.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("CategoryID", "{0}"), _
        RelationInfo("Category", "ID", "Vw_ChassisMaster", "CategoryID")> _
        Public Property Category() As Category
            Get
                Try
                    If Not IsNothing(Me._category) AndAlso (Not Me._category.IsLoaded) Then

                        Me._category = CType(DoLoad(GetType(Category).ToString(), _category.ID), Category)
                        Me._category.MarkLoaded()

                    End If

                    Return Me._category

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Category)

                Me._category = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._category.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("TOPID", "{0}"), _
        RelationInfo("TermOfPayment", "ID", "Vw_ChassisMaster", "TOPID")> _
        Public Property TermOfPayment() As TermOfPayment
            Get
                Try
                    If Not IsNothing(Me._termOfPayment) AndAlso (Not Me._termOfPayment.IsLoaded) Then

                        Me._termOfPayment = CType(DoLoad(GetType(TermOfPayment).ToString(), _termOfPayment.ID), TermOfPayment)
                        Me._termOfPayment.MarkLoaded()

                    End If

                    Return Me._termOfPayment

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TermOfPayment)

                Me._termOfPayment = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._termOfPayment.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("VechileColorID", "{0}"), _
        RelationInfo("VechileColor", "ID", "Vw_ChassisMaster", "VechileColorID")> _
        Public Property VechileColor() As VechileColor
            Get
                Try
                    If Not IsNothing(Me._vechileColor) AndAlso (Not Me._vechileColor.IsLoaded) Then

                        Me._vechileColor = CType(DoLoad(GetType(VechileColor).ToString(), _vechileColor.ID), VechileColor)
                        Me._vechileColor.MarkLoaded()

                    End If

                    Return Me._vechileColor

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileColor)

                Me._vechileColor = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileColor.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("VehicleKindID", "{0}"), _
    RelationInfo("VehicleKind", "ID", "Vw_ChassisMaster", "VehicleKindID")> _
        Public Property VehicleKind() As VehicleKind
            Get
                Try
                    If Not IsNothing(Me._vehicleKind) AndAlso (Not Me._vehicleKind.IsLoaded) Then

                        Me._vehicleKind = CType(DoLoad(GetType(VehicleKind).ToString(), _vehicleKind.ID), VehicleKind)
                        Me._vehicleKind.MarkLoaded()

                    End If

                    Return Me._vehicleKind

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VehicleKind)

                Me._vehicleKind = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vehicleKind.MarkLoaded()
                End If
            End Set
        End Property
        

        <ColumnInfo("SoldDealerID", "{0}"), _
         RelationInfo("Dealer", "ID", "Vw_ChassisMaster", "SoldDealerID")> _
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


        <ColumnInfo("DONumber", "'{0}'")> _
        Public Property DONumber As String
            Get
                Return _dONumber
            End Get
            Set(ByVal value As String)
                _dONumber = value
            End Set
        End Property


        <ColumnInfo("SONumber", "'{0}'")> _
        Public Property SONumber As String
            Get
                Return _sONumber
            End Get
            Set(ByVal value As String)
                _sONumber = value
            End Set
        End Property

         
        <ColumnInfo("DiscountAmount", "{0}")> _
        Public Property DiscountAmount As Decimal
            Get
                Return _discountAmount
            End Get
            Set(ByVal value As Decimal)
                _discountAmount = value
            End Set
        End Property


        <ColumnInfo("PONumber", "'{0}'")> _
        Public Property PONumber As String
            Get
                Return _pONumber
            End Get
            Set(ByVal value As String)
                _pONumber = value
            End Set
        End Property


        <ColumnInfo("EngineNumber", "'{0}'")> _
        Public Property EngineNumber As String
            Get
                Return _engineNumber
            End Get
            Set(ByVal value As String)
                _engineNumber = value
            End Set
        End Property


        <ColumnInfo("SerialNumber", "'{0}'")> _
        Public Property SerialNumber As String
            Get
                Return _serialNumber
            End Get
            Set(ByVal value As String)
                _serialNumber = value
            End Set
        End Property


        <ColumnInfo("DODate", "'{0:yyyy/MM/dd}'")> _
        Public Property DODate As DateTime
            Get
                Return _dODate
            End Get
            Set(ByVal value As DateTime)
                _dODate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("GIDate", "'{0:yyyy/MM/dd}'")> _
        Public Property GIDate As DateTime
            Get
                Return _gIDate
            End Get
            Set(ByVal value As DateTime)
                _gIDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ParkingDays", "{0}")> _
        Public Property ParkingDays As Integer
            Get
                Return _parkingDays
            End Get
            Set(ByVal value As Integer)
                _parkingDays = value
            End Set
        End Property


        <ColumnInfo("ParkingAmount", "{0}")> _
        Public Property ParkingAmount As Decimal
            Get
                Return _parkingAmount
            End Get
            Set(ByVal value As Decimal)
                _parkingAmount = value
            End Set
        End Property


        <ColumnInfo("FakturStatus", "'{0}'")> _
        Public Property FakturStatus As String
            Get
                Return _fakturStatus
            End Get
            Set(ByVal value As String)
                _fakturStatus = value
            End Set
        End Property


        <ColumnInfo("PendingDesc", "'{0}'")> _
        Public Property PendingDesc As String
            Get
                Return _pendingDesc
            End Get
            Set(ByVal value As String)
                _pendingDesc = value
            End Set
        End Property


        <ColumnInfo("IsSAPDownload", "'{0}'")> _
        Public Property IsSAPDownload As String
            Get
                Return _isSAPDownload
            End Get
            Set(ByVal value As String)
                _isSAPDownload = value
            End Set
        End Property


        <ColumnInfo("StockDealer", "{0}")> _
        Public Property StockDealer As Short
            Get
                Return _stockDealer
            End Get
            Set(ByVal value As Short)
                _stockDealer = value
            End Set
        End Property


        <ColumnInfo("StockDate", "'{0:yyyy/MM/dd}'")> _
        Public Property StockDate As DateTime
            Get
                Return _stockDate
            End Get
            Set(ByVal value As DateTime)
                _stockDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ProductionYear", "{0}")> _
        Public Property ProductionYear As Short
            Get
                Return _productionYear
            End Get
            Set(ByVal value As Short)
                _productionYear = value
            End Set
        End Property


        <ColumnInfo("StockStatus", "'{0}'")> _
        Public Property StockStatus As String
            Get
                Return _stockStatus
            End Get
            Set(ByVal value As String)
                _stockStatus = value
            End Set
        End Property


        <ColumnInfo("LastUpdateProfile", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateProfile As DateTime
            Get
                Return _lastUpdateProfile
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateProfile = value
            End Set
        End Property


        <ColumnInfo("AlreadySaled", "{0}")> _
        Public Property AlreadySaled As Byte
            Get
                Return _alreadySaled
            End Get
            Set(ByVal value As Byte)
                _alreadySaled = value
            End Set
        End Property


        <ColumnInfo("AlreadySaledTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property AlreadySaledTime As DateTime
            Get
                Return _alreadySaledTime
            End Get
            Set(ByVal value As DateTime)
                _alreadySaledTime = value
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


        <ColumnInfo("ChassisMasterDocumentID", "{0}")> _
        Public Property ChassisMasterDocumentID As Integer
            Get
                Return _chassisMasterDocumentID
            End Get
            Set(ByVal value As Integer)
                _chassisMasterDocumentID = value
            End Set
        End Property


        <ColumnInfo("EngineImagePath", "'{0}'")> _
        Public Property EngineImagePath As String
            Get
                Return _engineImagePath
            End Get
            Set(ByVal value As String)
                _engineImagePath = value
            End Set
        End Property


        <ColumnInfo("ChassisImagePath", "'{0}'")> _
        Public Property ChassisImagePath As String
            Get
                Return _chassisImagePath
            End Get
            Set(ByVal value As String)
                _chassisImagePath = value
            End Set
        End Property


        <ColumnInfo("UploadDate", "'{0:yyyy/MM/dd}'")> _
        Public Property UploadDate As DateTime
            Get
                Return _uploadDate
            End Get
            Set(ByVal value As DateTime)
                _uploadDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ChassisMasterDocumentStatus", "{0}")> _
        Public Property ChassisMasterDocumentStatus As Short
            Get
                Return _chassisMasterDocumentStatus
            End Get
            Set(ByVal value As Short)
                _chassisMasterDocumentStatus = value
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

