
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ChassisMasterBB Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 3/20/2006 - 11:36:37 AM
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
    <Serializable(), TableInfo("ChassisMasterBB")> _
    Public Class ChassisMasterBB
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
        Private _chassisNumber As String = String.Empty
        'Private _vehicleKindID As Integer
        Private _dONumber As String = String.Empty
        Private _sONumber As String = String.Empty
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
        Private _stockStatus As String = String.Empty
        Private _lastUpdateProfile As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _alreadySaled As Byte

        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _Unit As Integer
        Private _stockDealer As Integer
        Private _stockDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _productionYear As Integer
        Private _dealer As Dealer
        Private _endCustomer As EndCustomer
        Private _category As Category
        Private _termOfPayment As TermOfPayment
        Private _vechileColor As VechileColor

        Private _vehicleKind As VehicleKind

        Private _ChassisMasterBBProfiles As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _dealerStockReportDetails As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _pMHeaders As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _deliveryCustomerDetails As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _stockMovements As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _sPAFDocs As System.Collections.ArrayList = New System.Collections.ArrayList


#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("ChassisNumber", "'{0}'")> _
        Public Property ChassisNumber() As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
            End Set
        End Property

        '<ColumnInfo("VehicleKindID", "{0}")> _
        '  Public Property VehicleKindID() As Integer
        '    Get
        '        Return _vehicleKindID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _vehicleKindID = value
        '    End Set
        'End Property

        <ColumnInfo("DONumber", "'{0}'")> _
        Public Property DONumber() As String
            Get
                Return _dONumber
            End Get
            Set(ByVal value As String)
                _dONumber = value
            End Set
        End Property


        <ColumnInfo("SONumber", "'{0}'")> _
        Public Property SONumber() As String
            Get
                Return _sONumber
            End Get
            Set(ByVal value As String)
                _sONumber = value
            End Set
        End Property


        <ColumnInfo("DiscountAmount", "{0}")> _
        Public Property DiscountAmount() As Decimal
            Get
                Return _discountAmount
            End Get
            Set(ByVal value As Decimal)
                _discountAmount = value
            End Set
        End Property


        <ColumnInfo("PONumber", "'{0}'")> _
        Public Property PONumber() As String
            Get
                Return _pONumber
            End Get
            Set(ByVal value As String)
                _pONumber = value
            End Set
        End Property


        <ColumnInfo("EngineNumber", "'{0}'")> _
        Public Property EngineNumber() As String
            Get
                Return _engineNumber
            End Get
            Set(ByVal value As String)
                _engineNumber = value
            End Set
        End Property


        <ColumnInfo("SerialNumber", "'{0}'")> _
        Public Property SerialNumber() As String
            Get
                Return _serialNumber
            End Get
            Set(ByVal value As String)
                _serialNumber = value
            End Set
        End Property


        <ColumnInfo("DODate", "'{0:yyyy/MM/dd}'")> _
        Public Property DODate() As DateTime
            Get
                Return _dODate
            End Get
            Set(ByVal value As DateTime)
                _dODate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("GIDate", "'{0:yyyy/MM/dd}'")> _
        Public Property GIDate() As DateTime
            Get
                Return _gIDate
            End Get
            Set(ByVal value As DateTime)
                _gIDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ParkingDays", "{0}")> _
        Public Property ParkingDays() As Integer
            Get
                Return _parkingDays
            End Get
            Set(ByVal value As Integer)
                _parkingDays = value
            End Set
        End Property


        <ColumnInfo("ParkingAmount", "{0}")> _
        Public Property ParkingAmount() As Decimal
            Get
                Return _parkingAmount
            End Get
            Set(ByVal value As Decimal)
                _parkingAmount = value
            End Set
        End Property


        <ColumnInfo("FakturStatus", "'{0}'")> _
        Public Property FakturStatus() As String
            Get
                Return _fakturStatus
            End Get
            Set(ByVal value As String)
                _fakturStatus = value
            End Set
        End Property


        <ColumnInfo("PendingDesc", "'{0}'")> _
        Public Property PendingDesc() As String
            Get
                Return _pendingDesc
            End Get
            Set(ByVal value As String)
                _pendingDesc = value
            End Set
        End Property
        <ColumnInfo("IsSAPDownload", "'{0}'")> _
         Public Property IsSAPDownload() As String
            Get
                Return _isSAPDownload
            End Get
            Set(ByVal value As String)
                _isSAPDownload = value
            End Set
        End Property

        <ColumnInfo("StockDealer", "{0}")> _
        Public Property StockDealer() As Integer
            Get
                Return _stockDealer
            End Get
            Set(ByVal value As Integer)
                _stockDealer = value
            End Set
        End Property


        <ColumnInfo("StockDate", "'{0:yyyy/MM/dd}'")> _
        Public Property StockDate() As DateTime
            Get
                Return _stockDate
            End Get
            Set(ByVal value As DateTime)
                _stockDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("ProductionYear", "{0}")> _
        Public Property ProductionYear() As Integer
            Get
                Return _productionYear
            End Get
            Set(ByVal value As Integer)
                _productionYear = value
            End Set
        End Property


        <ColumnInfo("StockStatus", "'{0}'")> _
        Public Property StockStatus() As String
            Get
                Return _stockStatus
            End Get
            Set(ByVal value As String)
                _stockStatus = value
            End Set
        End Property

        <ColumnInfo("LastUpdateProfile", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
          Public Property LastUpdateProfile() As DateTime
            Get
                Return _lastUpdateProfile
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateProfile = value
            End Set
        End Property


        <ColumnInfo("AlreadySaled", "{0}")> _
        Public Property AlreadySaled() As Byte
            Get
                Return _alreadySaled
            End Get
            Set(ByVal value As Byte)
                _alreadySaled = value
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

        <ColumnInfo("SoldDealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "ChassisMasterBB", "SoldDealerID")> _
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

        <ColumnInfo("EndCustomerID", "{0}"), _
        RelationInfo("EndCustomer", "ID", "ChassisMasterBB", "EndCustomerID")> _
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

        <ColumnInfo("CategoryID", "{0}"), _
        RelationInfo("Category", "ID", "ChassisMasterBB", "CategoryID")> _
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
        RelationInfo("TermOfPayment", "ID", "ChassisMasterBB", "TOPID")> _
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
        RelationInfo("VechileColor", "ID", "ChassisMasterBB", "VechileColorID")> _
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

        'backbone
        '<RelationInfo("ChassisMasterBB", "ID", "ChassisMasterBBProfile", "ChassisMasterBBID")> _
        'Public ReadOnly Property ChassisMasterBBProfiles() As System.Collections.ArrayList
        '    Get
        '        Try
        '            If (Me._ChassisMasterBBProfiles.Count < 1) Then
        '                Dim _criteria As Criteria = New Criteria(GetType(ChassisMasterBBProfile), "ChassisMasterBB", Me.ID)
        '                Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
        '                criterias.opAnd(New Criteria(GetType(ChassisMasterBBProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '                Me._ChassisMasterBBProfiles = DoLoadArray(GetType(ChassisMasterBBProfile).ToString, criterias)
        '            End If

        '            Return Me._ChassisMasterBBProfiles

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        End Try

        '        Return Nothing

        '    End Get
        'End Property

        <RelationInfo("ChassisMasterBB", "ID", "DealerStockReportDetail", "ChassisMasterBBID")> _
        Public ReadOnly Property DealerStockReportDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._dealerStockReportDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DealerStockReportDetail), "ChassisMasterBB", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DealerStockReportDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._dealerStockReportDetails = DoLoadArray(GetType(DealerStockReportDetail).ToString, criterias)
                    End If

                    Return Me._dealerStockReportDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("ChassisMasterBB", "ID", "PMHeader", "ChassisNumberID")> _
        Public ReadOnly Property PMHeaders() As System.Collections.ArrayList
            Get
                Try
                    If (Me._pMHeaders.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PMHeader), "ChassisMasterBB", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._pMHeaders = DoLoadArray(GetType(PMHeader).ToString, criterias)
                    End If

                    Return Me._pMHeaders

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("ChassisMasterBB", "ID", "DeliveryCustomerDetail", "ChassisMasterBBID")> _
        Public ReadOnly Property DeliveryCustomerDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._deliveryCustomerDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DeliveryCustomerDetail), "ChassisMasterBB", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DeliveryCustomerDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._deliveryCustomerDetails = DoLoadArray(GetType(DeliveryCustomerDetail).ToString, criterias)
                    End If

                    Return Me._deliveryCustomerDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("ChassisMasterBB", "ID", "StockMovement", "ChassisMasterBBID")> _
        Public ReadOnly Property StockMovements() As System.Collections.ArrayList
            Get
                Try
                    If (Me._stockMovements.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(StockMovement), "ChassisMasterBB", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(StockMovement), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._stockMovements = DoLoadArray(GetType(StockMovement).ToString, criterias)
                    End If

                    Return Me._stockMovements

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("ChassisMasterBB", "ID", "SPAFDoc", "ChassisMasterBBID")> _
        Public ReadOnly Property SPAFDocs() As System.Collections.ArrayList
            Get
                Try
                    If (Me._sPAFDocs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SPAFDoc), "ChassisMasterBB", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SPAFDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sPAFDocs = DoLoadArray(GetType(SPAFDoc).ToString, criterias)
                    End If

                    Return Me._sPAFDocs

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property


        <ColumnInfo("VehicleKindID", "{0}"), _
        RelationInfo("VehicleKind", "ID", "ChassisMasterBB", "VehicleKindID")> _
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

#Region "Custom Properties"

        Public ReadOnly Property FakturStatusDesc() As String
            Get
                Return EnumChassisMaster.FakturStatusDesc(FakturStatus)
            End Get
        End Property

        Public ReadOnly Property MaterialNumberText() As String
            Get
                If Not IsNothing(VechileColor) Then
                    Return VechileColor.MaterialNumber
                Else
                    Return ""
                End If
            End Get
        End Property

        Public ReadOnly Property DODateText() As String
            Get
                Return IIf(Format(DODate, "dd/MM/yyyy") = "01/01/1753" Or Format(DODate, "dd/MM/yyyy") = "01/01/1900", _
                           "", Format(DODate, "dd/MM/yyyy"))
            End Get
        End Property

        Public ReadOnly Property SaveTimeText() As String
            Get
                If Not IsNothing(EndCustomer) Then
                    Return IIf(Format(EndCustomer.SaveTime, "dd/MM/yyyy") = "01/01/1753" Or Format(EndCustomer.SaveTime, "dd/MM/yyyy") = "01/01/1900", _
                               "", Format(EndCustomer.SaveTime, "dd/MM/yyyy"))
                Else
                    Return ""
                End If
            End Get
        End Property

        Public ReadOnly Property ValidateDateText() As String
            Get
                If Not IsNothing(EndCustomer) Then
                    Return IIf(Format(EndCustomer.ValidateTime, "dd/MM/yyyy") = "01/01/1753" Or Format(EndCustomer.ValidateTime, "dd/MM/yyyy") = "01/01/1900", _
                               "", Format(EndCustomer.ValidateTime, "dd/MM/yyyy"))
                Else
                    Return ""
                End If
            End Get
        End Property

        Public ReadOnly Property ValidateByText() As String
            Get
                Try
                    Return UserInfo.Convert(EndCustomer.ValidateBy)
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property

        Public ReadOnly Property ConfirmDateText() As String
            Get
                If Not IsNothing(EndCustomer) Then
                    Return IIf(Format(EndCustomer.ConfirmTime, "dd/MM/yyyy") = "01/01/1753" Or Format(EndCustomer.ConfirmTime, "dd/MM/yyyy") = "01/01/1900", _
                               "", Format(EndCustomer.ConfirmTime, "dd/MM/yyyy"))
                Else
                    Return ""
                End If
            End Get
        End Property

        Public ReadOnly Property PrintedTimeText() As String
            Get
                If Not IsNothing(EndCustomer) Then
                    Return IIf(Format(EndCustomer.PrintedTime, "dd/MM/yyyy") = "01/01/1753" Or Format(EndCustomer.PrintedTime, "dd/MM/yyyy") = "01/01/1900", _
                               "", Format(EndCustomer.PrintedTime, "dd/MM/yyyy"))
                Else
                    Return ""
                End If
            End Get
        End Property

        Public ReadOnly Property FakturNumberText() As String
            Get
                If Not IsNothing(EndCustomer) Then
                    Return EndCustomer.FakturNumber
                Else
                    Return ""
                End If
            End Get
        End Property

        Public ReadOnly Property EndCustNameText() As String
            Get
                If Not IsNothing(EndCustomer) Then
                    Return EndCustomer.Customer.Name1
                Else
                    Return ""
                End If
            End Get
        End Property

        Public ReadOnly Property AddressText() As String
            Get
                If Not IsNothing(EndCustomer) Then
                    Return EndCustomer.Customer.Alamat
                Else
                    Return ""
                End If
            End Get
        End Property

        Public Property Unit() As String
            Get
                Return _Unit
            End Get
            Set(ByVal Value As String)
                _Unit = Value
            End Set
        End Property

        Public ReadOnly Property VechileType() As String
            Get
                If Not IsNothing(VechileColor) Then
                    Return VechileColor.VechileType.VechileTypeCode
                Else
                    Return ""
                End If
            End Get
        End Property

        Public ReadOnly Property VechileTypeAndColor() As String
            Get
                If Not IsNothing(VechileColor) Then
                    Return VechileColor.VechileType.VechileTypeCode & " - " & VechileColor.ColorCode & " " & VechileColor.ColorIndName
                Else
                    Return ""
                End If
            End Get
        End Property

        Private _IsChangedWSM As Boolean

        Public Property IsChangedWSM() As Boolean
            Get
                Return _IsChangedWSM
            End Get
            Set(ByVal value As Boolean)
                _IsChangedWSM = value
            End Set
        End Property

#End Region

    End Class
End Namespace

