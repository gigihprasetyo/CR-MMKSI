#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FreeService Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/30/2005 - 1:03:40 PM
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
    <Serializable(), TableInfo("FreeService")> _
    Public Class FreeService
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++
        Private _iD As Integer
        Private _status As String = String.Empty
        Private _mileAge As Integer
        Private _serviceDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _soldDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _notificationNumber As String = "0"
        Private _notificationType As String = String.Empty
        Private _totalAmount As Decimal
        Private _labourAmount As Decimal
        Private _partAmount As Decimal
        Private _pPNAmount As Decimal
        Private _pPHAmount As Decimal
        Private _reject As String = String.Empty
        Private _releaseBy As String = String.Empty
        Private _releaseDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _visitType As String = String.Empty
        Private _workOrderNumber As String = String.Empty
        Private _cashBack As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealerBranch As DealerBranch
        Private _reason As Reason
        Private _chassisMaster As ChassisMaster
        Private _fSKind As FSKind
        Private _dealer As Dealer
        Private _fleetRequest As FleetRequest

        Private _fileName As String = String.Empty
        Private _filePath As String = String.Empty



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


        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property


        <ColumnInfo("MileAge", "{0}")> _
        Public Property MileAge() As Integer
            Get
                Return _mileAge
            End Get
            Set(ByVal value As Integer)
                _mileAge = value
            End Set
        End Property


        <ColumnInfo("ServiceDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ServiceDate() As DateTime
            Get
                Return _serviceDate
            End Get
            Set(ByVal value As DateTime)
                _serviceDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("SoldDate", "'{0:yyyy/MM/dd}'")> _
        Public Property SoldDate() As DateTime
            Get
                Return _soldDate
            End Get
            Set(ByVal value As DateTime)
                _soldDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("NotificationNumber", "'{0}'")> _
        Public Property NotificationNumber() As String
            Get
                Return _notificationNumber
            End Get
            Set(ByVal value As String)
                _notificationNumber = value
            End Set
        End Property


        <ColumnInfo("NotificationType", "'{0}'")> _
        Public Property NotificationType() As String
            Get
                Return _notificationType
            End Get
            Set(ByVal value As String)
                _notificationType = value
            End Set
        End Property


        <ColumnInfo("TotalAmount", "{0}")> _
        Public Property TotalAmount() As Decimal
            Get
                Return _totalAmount
            End Get
            Set(ByVal value As Decimal)
                _totalAmount = value
            End Set
        End Property


        <ColumnInfo("LabourAmount", "{0}")> _
        Public Property LabourAmount() As Decimal
            Get
                Return _labourAmount
            End Get
            Set(ByVal value As Decimal)
                _labourAmount = value
            End Set
        End Property


        <ColumnInfo("PartAmount", "{0}")> _
        Public Property PartAmount() As Decimal
            Get
                Return _partAmount
            End Get
            Set(ByVal value As Decimal)
                _partAmount = value
            End Set
        End Property


        <ColumnInfo("PPNAmount", "{0}")> _
        Public Property PPNAmount() As Decimal
            Get
                Return _pPNAmount
            End Get
            Set(ByVal value As Decimal)
                _pPNAmount = value
            End Set
        End Property


        <ColumnInfo("PPHAmount", "{0}")> _
        Public Property PPHAmount() As Decimal
            Get
                Return _pPHAmount
            End Get
            Set(ByVal value As Decimal)
                _pPHAmount = value
            End Set
        End Property


        <ColumnInfo("Reject", "'{0}'")> _
        Public Property Reject() As String
            Get
                Return _reject
            End Get
            Set(ByVal value As String)
                _reject = value
            End Set
        End Property


        <ColumnInfo("ReleaseBy", "'{0}'")> _
        Public Property ReleaseBy() As String
            Get
                Return _releaseBy
            End Get
            Set(ByVal value As String)
                _releaseBy = value
            End Set
        End Property


        <ColumnInfo("ReleaseDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ReleaseDate() As DateTime
            Get
                Return _releaseDate
            End Get
            Set(ByVal value As DateTime)
                _releaseDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("VisitType", "'{0}'")> _
        Public Property VisitType() As String
            Get
                Return _visitType
            End Get
            Set(ByVal value As String)
                _visitType = value
            End Set
        End Property

        <ColumnInfo("WorkOrderNumber", "'{0}'")> _
        Public Property WorkOrderNumber() As String
            Get
                Return _workOrderNumber
            End Get
            Set(ByVal value As String)
                _workOrderNumber = value
            End Set
        End Property

        <ColumnInfo("CashBack", "{0}")> _
        Public Property CashBack() As Decimal
            Get
                Return _cashBack
            End Get
            Set(ByVal value As Decimal)
                _cashBack = value
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


        <ColumnInfo("Reason", "{0}"), _
        RelationInfo("Reason", "ID", "FreeService", "Reason")> _
        Public Property Reason() As Reason
            Get
                Try
                    If Not IsNothing(Me._reason) AndAlso (Not Me._reason.IsLoaded) Then

                        Me._reason = CType(DoLoad(GetType(Reason).ToString(), _reason.ID), Reason)
                        Me._reason.MarkLoaded()

                    End If

                    Return Me._reason

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Reason)

                Me._reason = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._reason.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ChassisMasterID", "{0}"), _
        RelationInfo("ChassisMaster", "ID", "FreeService", "ChassisMasterID")> _
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

        <ColumnInfo("FSKindID", "{0}"), _
        RelationInfo("FSKind", "ID", "FreeService", "FSKindID")> _
        Public Property FSKind() As FSKind
            Get
                Try
                    If Not IsNothing(Me._fSKind) AndAlso (Not Me._fSKind.IsLoaded) Then

                        Me._fSKind = CType(DoLoad(GetType(FSKind).ToString(), _fSKind.ID), FSKind)
                        Me._fSKind.MarkLoaded()

                    End If

                    Return Me._fSKind

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As FSKind)

                Me._fSKind = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._fSKind.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ServiceDealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "FreeService", "ServiceDealerID")> _
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
        RelationInfo("DealerBranch", "ID", "FreeService", "DealerBranchID")> _
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

        <ColumnInfo("FleetRequestID", "{0}"), _
        RelationInfo("FleetRequest", "ID", "FreeService", "FleetRequestID")> _
        Public Property FleetRequest As FleetRequest
            Get
                Try
                    If Not IsNothing(Me._fleetRequest) AndAlso (Not Me._fleetRequest.IsLoaded) Then

                        Me._fleetRequest = CType(DoLoad(GetType(FleetRequest).ToString(), _fleetRequest.ID), FleetRequest)
                        Me._fleetRequest.MarkLoaded()

                    End If

                    Return Me._fleetRequest

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As FleetRequest)

                Me._fleetRequest = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._fleetRequest.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("FileName", "'{0}'")> _
        Public Property FileName() As String
            Get
                Return _fileName
            End Get
            Set(ByVal value As String)
                _fileName = value
            End Set
        End Property

        <ColumnInfo("FilePath", "'{0}'")> _
        Public Property FilePath() As String
            Get
                Return _filePath
            End Get
            Set(ByVal value As String)
                _filePath = value
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

#Region "Non_generated Properties"
        Private _ChassisNumberMsg As String = String.Empty
        Private _EngineNumberMsg As String = String.Empty
        Private _DealerBranchCodeMsg As String = String.Empty
        Private _FSKindMsg As String = String.Empty
        Private _FSDateMsg As String = String.Empty
        Private _SoldDateMsg As String = String.Empty
        Private _MileAgeMsg As String = String.Empty

        Public Property ChassisNumberMsg() As String
            Get
                Return _ChassisNumberMsg
            End Get
            Set(ByVal value As String)
                _ChassisNumberMsg = value
            End Set
        End Property


        Public Property EngineNumberMsg() As String
            Get
                Return _EngineNumberMsg
            End Get
            Set(value As String)
                _EngineNumberMsg = value
            End Set
        End Property

        Public Property DealerBranchCodeMsg() As String
            Get
                Return _DealerBranchCodeMsg
            End Get
            Set(ByVal value As String)
                _DealerBranchCodeMsg = value
            End Set
        End Property

        Public Property FSKindMsg() As String
            Get
                Return _FSKindMsg
            End Get
            Set(ByVal value As String)
                _FSKindMsg = value
            End Set
        End Property

        Public Property FSDateMsg() As String
            Get
                Return _FSDateMsg
            End Get
            Set(ByVal value As String)
                _FSDateMsg = value
            End Set
        End Property

        Public Property SoldDateMsg() As String
            Get
                Return _SoldDateMsg
            End Get
            Set(ByVal value As String)
                _SoldDateMsg = value
            End Set
        End Property

        Public Property MileAgeMsg() As String
            Get
                Return _MileAgeMsg
            End Get
            Set(ByVal value As String)
                _MileAgeMsg = value
            End Set
        End Property
#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
