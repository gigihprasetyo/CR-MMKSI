
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ServiceReminder Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 03/07/2020 - 12:24:21
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
    <Serializable(), TableInfo("ServiceReminder")> _
    Public Class ServiceReminder
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
        Private _salesforceID As String = String.Empty
        'Private _dealerID As Integer
        Private _chassisNumber As String = String.Empty
        'Private _chassisMasterID As Integer
        Private _engineNumber As String = String.Empty
        Private _vehicleType As String = String.Empty
        Private _wONumber As String = String.Empty
        Private _serviceReminderDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _maxFUDealerDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _bookingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _bookingTime As String = String.Empty
        Private _caseNumber As String = String.Empty
        'Private _assistServiceIncomingID As Integer
        Private _serviceActualDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _customerName As String = String.Empty
        Private _customerPhoneNumber As String = String.Empty
        Private _contactPersonName As String = String.Empty
        Private _contactPersonPhoneNumber As String = String.Empty
        'Private _pMKindID As Integer
        Private _transactionType As Byte
        Private _actualKM As Integer
        Private _pKTDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _sourceFlag As Short
        Private _status As Byte
        Private _remark As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _chassisMaster As ChassisMaster
        Private _assistServiceIncoming As AssistServiceIncoming
        Private _pMKind As PMKind
        Private _actualServiceDealer As Dealer
        Private _dealerBranch As DealerBranch
        Private _actualServiceDealerBranch As DealerBranch
        Private _category As Category

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


        <ColumnInfo("SalesforceID", "'{0}'")> _
        Public Property SalesforceID As String
            Get
                Return _salesforceID
            End Get
            Set(ByVal value As String)
                _salesforceID = value
            End Set
        End Property


        '<ColumnInfo("DealerID", "{0}")> _
        'Public Property DealerID As Integer
        '    Get
        '        Return _dealerID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _dealerID = value
        '    End Set
        'End Property


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "ServiceReminder", "DealerID")> _
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


        <ColumnInfo("ChassisNumber", "'{0}'")> _
        Public Property ChassisNumber As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
            End Set
        End Property


        '<ColumnInfo("ChassisMasterID", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        'Public Property ChassisMasterID As DateTime
        '    Get
        '        Return _chassisMasterID
        '    End Get
        '    Set(ByVal value As DateTime)
        '        _chassisMasterID = value
        '    End Set
        'End Property


        <ColumnInfo("ChassisMasterID", "{0}"), _
        RelationInfo("ChassisMaster", "ID", "ServiceReminder", "ChassisMasterID")> _
        Public Property ChassisMaster() As ChassisMaster
            Get
                Try
                    If Not IsNothing(Me._chassisMaster) AndAlso (Not Me._chassisMaster.IsLoaded) Then

                        Me._chassisMaster = CType(DoLoad(GetType(ChassisMaster).ToString(), _chassisMaster.ID), ChassisMaster)
                        Me._chassisMaster.MarkLoaded()

                    End If

                    Return Me._chassisMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ChassisMaster)

                Me._chassisMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._chassisMaster.MarkLoaded()
                End If
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


        <ColumnInfo("VehicleType", "'{0}'")> _
        Public Property VehicleType As String
            Get
                Return _vehicleType
            End Get
            Set(ByVal value As String)
                _vehicleType = value
            End Set
        End Property


        <ColumnInfo("WONumber", "'{0}'")> _
        Public Property WONumber As String
            Get
                Return _wONumber
            End Get
            Set(ByVal value As String)
                _wONumber = value
            End Set
        End Property


        <ColumnInfo("ServiceReminderDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ServiceReminderDate As DateTime
            Get
                Return _serviceReminderDate
            End Get
            Set(ByVal value As DateTime)
                _serviceReminderDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("MaxFUDealerDate", "'{0:yyyy/MM/dd}'")> _
        Public Property MaxFUDealerDate As DateTime
            Get
                Return _maxFUDealerDate
            End Get
            Set(ByVal value As DateTime)
                _maxFUDealerDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("BookingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property BookingDate As DateTime
            Get
                Return _bookingDate
            End Get
            Set(ByVal value As DateTime)
                _bookingDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("BookingTime", "'{0:HH:mm:ss}'")> _
        Public Property BookingTime As String
            Get
                Return _bookingTime
            End Get
            Set(ByVal value As String)
                _bookingTime = value
            End Set
        End Property


        <ColumnInfo("CaseNumber", "'{0}'")> _
        Public Property CaseNumber As String
            Get
                Return _caseNumber
            End Get
            Set(ByVal value As String)
                _caseNumber = value
            End Set
        End Property


        '<ColumnInfo("AssistServiceIncomingID", "{0}")> _
        'Public Property AssistServiceIncomingID As Integer
        '    Get
        '        Return _assistServiceIncomingID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _assistServiceIncomingID = value
        '    End Set
        'End Property


        <ColumnInfo("AssistServiceIncomingID", "{0}"), _
        RelationInfo("AssistServiceIncoming", "ID", "ServiceReminder", "AssistServiceIncomingID")> _
        Public Property AssistServiceIncoming() As AssistServiceIncoming
            Get
                Try
                    If Not IsNothing(Me._assistServiceIncoming) AndAlso (Not Me._assistServiceIncoming.IsLoaded) AndAlso _assistServiceIncoming.ID > 0 Then

                        Me._assistServiceIncoming = CType(DoLoad(GetType(AssistServiceIncoming).ToString(), _assistServiceIncoming.ID), AssistServiceIncoming)
                        Me._assistServiceIncoming.MarkLoaded()

                    End If

                    Return Me._assistServiceIncoming

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As AssistServiceIncoming)

                Me._assistServiceIncoming = value
                If value.ID = 0 Then
                    Dim i As Integer = 0
                    Me._assistServiceIncoming = Nothing
                End If
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._assistServiceIncoming.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("ServiceActualDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ServiceActualDate As DateTime
            Get
                Return _serviceActualDate
            End Get
            Set(ByVal value As DateTime)
                _serviceActualDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("CustomerName", "'{0}'")> _
        Public Property CustomerName As String
            Get
                Return _customerName
            End Get
            Set(ByVal value As String)
                _customerName = value
            End Set
        End Property


        <ColumnInfo("CustomerPhoneNumber", "'{0}'")> _
        Public Property CustomerPhoneNumber As String
            Get
                Return _customerPhoneNumber
            End Get
            Set(ByVal value As String)
                _customerPhoneNumber = value
            End Set
        End Property


        <ColumnInfo("ContactPersonName", "'{0}'")> _
        Public Property ContactPersonName As String
            Get
                Return _contactPersonName
            End Get
            Set(ByVal value As String)
                _contactPersonName = value
            End Set
        End Property


        <ColumnInfo("ContactPersonPhoneNumber", "'{0}'")> _
        Public Property ContactPersonPhoneNumber As String
            Get
                Return _contactPersonPhoneNumber
            End Get
            Set(ByVal value As String)
                _contactPersonPhoneNumber = value
            End Set
        End Property


        '<ColumnInfo("ServiceType", "{0}")> _
        'Public Property ServiceType As Byte
        '    Get
        '        Return _serviceType
        '    End Get
        '    Set(ByVal value As Byte)
        '        _serviceType = value
        '    End Set
        'End Property


        <ColumnInfo("PMKindID", "{0}"), _
        RelationInfo("PMKind", "ID", "ServiceReminder", "PMKindID")> _
        Public Property PMKind() As PMKind
            Get
                Try
                    If Not IsNothing(Me._pMKind) AndAlso (Not Me._pMKind.IsLoaded) Then

                        Me._pMKind = CType(DoLoad(GetType(PMKind).ToString(), _pMKind.ID), PMKind)
                        Me._pMKind.MarkLoaded()

                    End If

                    Return Me._pMKind

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PMKind)

                Me._pMKind = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pMKind.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("TransactionType", "{0}")> _
        Public Property TransactionType As Byte
            Get
                Return _transactionType
            End Get
            Set(ByVal value As Byte)
                _transactionType = value
            End Set
        End Property


        <ColumnInfo("ActualKM", "{0}")> _
        Public Property ActualKM As Integer
            Get
                Return _actualKM
            End Get
            Set(ByVal value As Integer)
                _actualKM = value
            End Set
        End Property


        <ColumnInfo("PKTDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PKTDate As DateTime
            Get
                Return _pKTDate
            End Get
            Set(ByVal value As DateTime)
                _pKTDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("SourceFlag", "{0}")> _
        Public Property SourceFlag As Short
            Get
                Return _sourceFlag
            End Get
            Set(ByVal value As Short)
                _sourceFlag = value
            End Set
        End Property

        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Byte
            Get
                Return _status
            End Get
            Set(ByVal value As Byte)
                _status = value
            End Set
        End Property


        <ColumnInfo("Remark", "'{0}'")> _
        Public Property Remark As String
            Get
                Return _remark
            End Get
            Set(ByVal value As String)
                _remark = value
            End Set
        End Property


        <ColumnInfo("ActualServiceDealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "ServiceReminder", "ActualServiceDealerID")> _
        Public Property ActualServiceDealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._actualServiceDealer) AndAlso (Not Me._actualServiceDealer.IsLoaded) AndAlso _actualServiceDealer.ID > 0 Then

                        Me._actualServiceDealer = CType(DoLoad(GetType(Dealer).ToString(), _actualServiceDealer.ID), Dealer)
                        Me._actualServiceDealer.MarkLoaded()

                    End If

                    Return Me._actualServiceDealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)
                Me._actualServiceDealer = value
                If value.ID = 0 Then
                    Dim i As Integer = 0
                    Me._actualServiceDealer = Nothing
                End If
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._actualServiceDealer.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("DealerBranchID", "{0}"), _
        RelationInfo("DealerBranch", "ID", "ServiceReminder", "DealerBranchID")> _
        Public Property DealerBranch() As DealerBranch
            Get
                Try
                    If Not IsNothing(Me._dealerBranch) AndAlso (Not Me._dealerBranch.IsLoaded) AndAlso _dealerBranch.ID > 0 Then

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
                If value.ID = 0 Then
                    Dim i As Integer = 0
                    Me._dealerBranch = Nothing
                End If
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerBranch.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("ActualServiceDealerBranchID", "{0}"), _
        RelationInfo("DealerBranch", "ID", "ServiceReminder", "ActualServiceDealerBranchID")> _
        Public Property ActualServiceDealerBranch() As DealerBranch
            Get
                Try
                    If Not IsNothing(Me._actualServiceDealerBranch) AndAlso (Not Me._actualServiceDealerBranch.IsLoaded) AndAlso _actualServiceDealerBranch.ID > 0 Then

                        Me._actualServiceDealerBranch = CType(DoLoad(GetType(DealerBranch).ToString(), _actualServiceDealerBranch.ID), DealerBranch)
                        Me._actualServiceDealerBranch.MarkLoaded()

                    End If

                    Return Me._actualServiceDealerBranch

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DealerBranch)
                Me._actualServiceDealerBranch = value
                If value.ID = 0 Then
                    Dim i As Integer = 0
                    Me._actualServiceDealerBranch = Nothing
                End If
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._actualServiceDealerBranch.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("CategoryID", "{0}"), _
        RelationInfo("Category", "ID", "ServiceReminder", "CategoryID")> _
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


