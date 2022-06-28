
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AssistServiceIncoming Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 1/17/2018 - 9:25:52 AM
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
    <Serializable(), TableInfo("AssistServiceIncoming")> _
    Public Class AssistServiceIncoming
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
        Private _tglBukaTransaksi As Object
        Private _waktuMasuk As Object
        Private _tglTutupTransaksi As Object
        Private _waktuKeluar As Object
        Private _dealerCode As String = String.Empty
        Private _dealerBranchCode As String = String.Empty
        Private _trTraineMekanikID As Integer
        Private _kodeMekanik As String = String.Empty
        Private _noWorkOrder As String = String.Empty
        Private _kodeChassis As String = String.Empty
        Private _workOrderCategoryCode As String = String.Empty
        Private _kMService As Integer
        Private _servicePlaceCode As String = String.Empty
        Private _serviceTypeCode As String = String.Empty
        Private _totalLC As Decimal
        Private _metodePembayaran As String = String.Empty
        Private _model As String = String.Empty
        Private _transmition As String = String.Empty
        Private _driveSystem As String = String.Empty
        Private _remarksSystem As String = String.Empty
        Private _remarksSpecial As String = String.Empty
        Private _remarksBM As String = String.Empty
        Private _statusAktif As Short
        'new cols
        Private _customerOwnerName As String
        Private _customerOwnerPhoneNumber As String
        Private _customerVisitName As String
        Private _customerVisitPhoneNumber As String
        'end new cols
        Private _woStatus As Short
        Private _validateSystemStatus As Short

        Private _stallCode As String
        Private _bookingCode As String

        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _assistuploadLog As AssistUploadLog
        Private _chassismaster As ChassisMaster
        Private _dealer As Dealer
        Private _dealerBranch As DealerBranch
        Private _assistWorkOrderCategory As AssistWorkOrderCategory
        Private _assistServicePlace As AssistServicePlace
        Private _assistServiceType As AssistServiceType

        Private _stallMaster As StallMaster
        Private _serviceBooking As ServiceBooking

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


        <ColumnInfo("TglBukaTransaksi", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property TglBukaTransaksi As Object
            Get
                Return _tglBukaTransaksi
            End Get
            Set(ByVal value As Object)
                _tglBukaTransaksi = value
            End Set
        End Property


        <ColumnInfo("WaktuMasuk", "'{0:HH:mm:ss}'")> _
        Public Property WaktuMasuk As Object
            Get
                Return _waktuMasuk
            End Get
            Set(ByVal value As Object)
                _waktuMasuk = value
            End Set
        End Property


        <ColumnInfo("TglTutupTransaksi", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property TglTutupTransaksi As Object
            Get
                Return _tglTutupTransaksi
            End Get
            Set(ByVal value As Object)
                _tglTutupTransaksi = value
            End Set
        End Property


        <ColumnInfo("WaktuKeluar", "'{0:HH:mm:ss}'")> _
        Public Property WaktuKeluar As Object
            Get
                Return _waktuKeluar
            End Get
            Set(ByVal value As Object)
                _waktuKeluar = value
            End Set
        End Property


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property

        <ColumnInfo("DealerBranchCode", "'{0}'")> _
        Public Property DealerBranchCode As String
            Get
                Return _dealerBranchCode
            End Get
            Set(ByVal value As String)
                _dealerBranchCode = value
            End Set
        End Property


        <ColumnInfo("TrTraineMekanikID", "{0}")> _
        Public Property TrTraineMekanikID As Integer
            Get
                Return _trTraineMekanikID
            End Get
            Set(ByVal value As Integer)
                _trTraineMekanikID = value
            End Set
        End Property


        <ColumnInfo("KodeMekanik", "'{0}'")> _
        Public Property KodeMekanik As String
            Get
                Return _kodeMekanik
            End Get
            Set(ByVal value As String)
                _kodeMekanik = value
            End Set
        End Property


        <ColumnInfo("NoWorkOrder", "'{0}'")> _
        Public Property NoWorkOrder As String
            Get
                Return _noWorkOrder
            End Get
            Set(ByVal value As String)
                _noWorkOrder = value
            End Set
        End Property

        <ColumnInfo("KodeChassis", "'{0}'")> _
        Public Property KodeChassis As String
            Get
                Return _kodeChassis
            End Get
            Set(ByVal value As String)
                _kodeChassis = value
            End Set
        End Property


        <ColumnInfo("WorkOrderCategoryCode", "'{0}'")> _
        Public Property WorkOrderCategoryCode As String
            Get
                Return _workOrderCategoryCode
            End Get
            Set(ByVal value As String)
                _workOrderCategoryCode = value
            End Set
        End Property


        <ColumnInfo("KMService", "{0}")> _
        Public Property KMService As Integer
            Get
                Return _kMService
            End Get
            Set(ByVal value As Integer)
                _kMService = value
            End Set
        End Property

        <ColumnInfo("ServicePlaceCode", "'{0}'")> _
        Public Property ServicePlaceCode As String
            Get
                Return _servicePlaceCode
            End Get
            Set(ByVal value As String)
                _servicePlaceCode = value
            End Set
        End Property

        <ColumnInfo("ServiceTypeCode", "'{0}'")> _
        Public Property ServiceTypeCode As String
            Get
                Return _serviceTypeCode
            End Get
            Set(ByVal value As String)
                _serviceTypeCode = value
            End Set
        End Property


        <ColumnInfo("TotalLC", "{0}")> _
        Public Property TotalLC As Decimal
            Get
                Return _totalLC
            End Get
            Set(ByVal value As Decimal)
                _totalLC = value
            End Set
        End Property


        <ColumnInfo("MetodePembayaran", "'{0}'")> _
        Public Property MetodePembayaran As String
            Get
                Return _metodePembayaran
            End Get
            Set(ByVal value As String)
                _metodePembayaran = value
            End Set
        End Property


        <ColumnInfo("Model", "'{0}'")> _
        Public Property Model As String
            Get
                Return _model
            End Get
            Set(ByVal value As String)
                _model = value
            End Set
        End Property


        <ColumnInfo("Transmition", "'{0}'")> _
        Public Property Transmition As String
            Get
                Return _transmition
            End Get
            Set(ByVal value As String)
                _transmition = value
            End Set
        End Property


        <ColumnInfo("DriveSystem", "'{0}'")> _
        Public Property DriveSystem As String
            Get
                Return _driveSystem
            End Get
            Set(ByVal value As String)
                _driveSystem = value
            End Set
        End Property


        <ColumnInfo("RemarksSystem", "'{0}'")> _
        Public Property RemarksSystem As String
            Get
                Return _remarksSystem
            End Get
            Set(ByVal value As String)
                _remarksSystem = value
            End Set
        End Property

        <ColumnInfo("RemarksSpecial", "'{0}'")> _
        Public Property RemarksSpecial As String
            Get
                Return _remarksSpecial
            End Get
            Set(ByVal value As String)
                _remarksSpecial = value
            End Set
        End Property

        <ColumnInfo("RemarksBM", "'{0}'")> _
        Public Property RemarksBM As String
            Get
                Return _remarksBM
            End Get
            Set(ByVal value As String)
                _remarksBM = value
            End Set
        End Property


        <ColumnInfo("StatusAktif", "{0}")> _
        Public Property StatusAktif As Short
            Get
                Return _statusAktif
            End Get
            Set(ByVal value As Short)
                _statusAktif = value
            End Set
        End Property

        <ColumnInfo("WOStatus", "{0}")> _
        Public Property WOStatus As Short
            Get
                Return _woStatus
            End Get
            Set(ByVal value As Short)
                _woStatus = value
            End Set
        End Property


        <ColumnInfo("ValidateSystemStatus", "{0}")> _
        Public Property ValidateSystemStatus As Short
            Get
                Return _validateSystemStatus
            End Get
            Set(ByVal value As Short)
                _validateSystemStatus = value
            End Set
        End Property

        <ColumnInfo("StallCode", "'{0}'")> _
        Public Property StallCode As String
            Get
                Return _stallCode
            End Get
            Set(ByVal value As String)
                _stallCode = value
            End Set
        End Property

        <ColumnInfo("BookingCode", "'{0}'")> _
        Public Property BookingCode As String
            Get
                Return _bookingCode
            End Get
            Set(ByVal value As String)
                _bookingCode = value
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

        <ColumnInfo("DealerID", "{0}"), _
       RelationInfo("Dealer", "ID", "AssistServiceIncoming", "DealerID")> _
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

        <ColumnInfo("DealerBranchID", "{0}"), _
       RelationInfo("DealerBranch", "ID", "AssistServiceIncoming", "DealerBranchID")> _
        Public Property DealerBranch() As DealerBranch
            Get
                Try
                    If Not IsNothing(Me._dealerBranch) AndAlso (Not Me._dealerBranch.IsLoaded) Then

                        Me._dealerBranch = CType(DoLoad(GetType(DealerBranch).ToString(), _dealerBranch.ID), DealerBranch)
                        Me._dealerBranch.MarkLoaded()

                    End If

                    Return Me._dealerBranch

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DealerBranch)

                Me._dealerBranch = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerBranch.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("AssistUploadLogID", "{0}"), _
        RelationInfo("AssistUploadLog", "ID", "AssistServiceIncoming", "AssistUploadLogID")> _
        Public Property AssistUploadLog() As AssistUploadLog
            Get
                Try
                    If Not IsNothing(Me._assistuploadLog) AndAlso (Not Me._assistuploadLog.IsLoaded) Then

                        Me._assistuploadLog = CType(DoLoad(GetType(AssistUploadLog).ToString(), _assistuploadLog.ID), AssistUploadLog)
                        Me._assistuploadLog.MarkLoaded()

                    End If

                    Return Me._assistuploadLog

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As AssistUploadLog)

                Me._assistuploadLog = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._assistuploadLog.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ChassisMasterID", "{0}"), _
        RelationInfo("ChassisMaster", "ID", "AssistServiceIncoming", "ChassisMasterID")> _
        Public Property ChassisMaster() As ChassisMaster
            Get
                Try
                    If Not IsNothing(Me._chassismaster) AndAlso (Not Me._chassismaster.IsLoaded) Then

                        Me._chassismaster = CType(DoLoad(GetType(ChassisMaster).ToString(), _chassismaster.ID), ChassisMaster)
                        Me._chassismaster.MarkLoaded()

                    End If

                    Return Me._chassismaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ChassisMaster)

                Me._chassismaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._chassismaster.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("WorkOrderCategoryID", "{0}"), _
       RelationInfo("AssistWorkOrderCategory", "ID", "AssistServiceIncoming", "WorkOrderCategoryID")> _
        Public Property AssistWorkOrderCategory() As AssistWorkOrderCategory
            Get
                Try
                    If Not IsNothing(Me._assistWorkOrderCategory) AndAlso (Not Me._assistWorkOrderCategory.IsLoaded) Then

                        Me._assistWorkOrderCategory = CType(DoLoad(GetType(AssistWorkOrderCategory).ToString(), _assistWorkOrderCategory.ID), AssistWorkOrderCategory)
                        Me._assistWorkOrderCategory.MarkLoaded()

                    End If

                    Return Me._assistWorkOrderCategory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As AssistWorkOrderCategory)

                Me._assistWorkOrderCategory = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._assistWorkOrderCategory.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ServicePlaceID", "{0}"), _
      RelationInfo("AssistServicePlace", "ID", "AssistServiceIncoming", "ServicePlaceID")> _
        Public Property AssistServicePlace() As AssistServicePlace
            Get
                Try
                    If Not IsNothing(Me._assistServicePlace) AndAlso (Not Me._assistServicePlace.IsLoaded) Then

                        Me._assistServicePlace = CType(DoLoad(GetType(AssistServicePlace).ToString(), _assistServicePlace.ID), AssistServicePlace)
                        Me._assistServicePlace.MarkLoaded()

                    End If

                    Return Me._assistServicePlace

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As AssistServicePlace)

                Me._assistServicePlace = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._assistServicePlace.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ServiceTypeID", "{0}"), _
     RelationInfo("AssistServiceType", "ID", "AssistServiceIncoming", "ServiceTypeID")> _
        Public Property AssistServiceType() As AssistServiceType
            Get
                Try
                    If Not IsNothing(Me._assistServiceType) AndAlso (Not Me._assistServiceType.IsLoaded) Then

                        Me._assistServiceType = CType(DoLoad(GetType(AssistServiceType).ToString(), _assistServiceType.ID), AssistServiceType)
                        Me._assistServiceType.MarkLoaded()

                    End If

                    Return Me._assistServiceType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As AssistServiceType)

                Me._assistServiceType = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._assistServiceType.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("CustomerOwnerName", "'{0}'")> _
        Public Property CustomerOwnerName As String
            Get
                Return _customerOwnerName
            End Get
            Set(ByVal value As String)
                _customerOwnerName = value
            End Set
        End Property


        <ColumnInfo("CustomerOwnerPhoneNumber", "'{0}'")> _
        Public Property CustomerOwnerPhoneNumber As String
            Get
                Return _customerOwnerPhoneNumber
            End Get
            Set(ByVal value As String)
                _customerOwnerPhoneNumber = value
            End Set
        End Property


        <ColumnInfo("CustomerVisitName", "'{0}'")> _
        Public Property CustomerVisitName As String
            Get
                Return _customerVisitName
            End Get
            Set(ByVal value As String)
                _customerVisitName = value
            End Set
        End Property


        <ColumnInfo("CustomerVisitPhoneNumber", "'{0}'")> _
        Public Property CustomerVisitPhoneNumber As String
            Get
                Return _customerVisitPhoneNumber
            End Get
            Set(ByVal value As String)
                _customerVisitPhoneNumber = value
            End Set
        End Property

        <ColumnInfo("StallMasterID", "{0}"), _
       RelationInfo("StallMaster", "ID", "AssistServiceIncoming", "StallMasterID")> _
        Public Property StallMaster() As StallMaster
            Get
                Try
                    If Not IsNothing(Me._stallMaster) AndAlso (Not Me._stallMaster.IsLoaded) Then

                        Me._stallMaster = CType(DoLoad(GetType(StallMaster).ToString(), _stallMaster.ID), StallMaster)
                        Me._stallMaster.MarkLoaded()

                    End If

                    Return Me._stallMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As StallMaster)

                Me._stallMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._stallMaster.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ServiceBookingID", "{0}"), _
       RelationInfo("ServiceBooking", "ID", "AssistServiceIncoming", "ServiceBookingID")> _
        Public Property ServiceBooking() As ServiceBooking
            Get
                Try
                    If Not IsNothing(Me._serviceBooking) AndAlso (Not Me._serviceBooking.IsLoaded) Then

                        Me._serviceBooking = CType(DoLoad(GetType(ServiceBooking).ToString(), _serviceBooking.ID), ServiceBooking)
                        Me._serviceBooking.MarkLoaded()

                    End If

                    Return Me._serviceBooking

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ServiceBooking)

                Me._serviceBooking = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._serviceBooking.MarkLoaded()
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

#Region "Custom Method"

#End Region

    End Class
End Namespace

