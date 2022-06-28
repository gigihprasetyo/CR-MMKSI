#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_EventProposalAgreement Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 11/23/2009 - 3:11:25 PM
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
    <Serializable(), TableInfo("V_EventProposalAgreement")> _
    Public Class V_EventProposalAgreement
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
        Private _dealerID As Short
        Private _activityTypeID As Integer
        Private _eventParameterID As Integer
        Private _activitySchedule As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _activityPlace As String = String.Empty
        Private _invitationNumber As Integer
        Private _attendantNumber As Integer
        Private _eventProposalStatus As Byte
        Private _eventAgreementStatus As Byte
        Private _comment As String = String.Empty
        Private _ravine As String = String.Empty
        Private _subDistrict As String = String.Empty
        Private _owner As Integer
        Private _driver As Integer
        Private _totalCost As Decimal
        Private _approveCost As Decimal
        Private _subsidiFile As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




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


        <ColumnInfo("DealerID", "{0}")> _
        Public Property DealerID() As Short
            Get
                Return _dealerID
            End Get
            Set(ByVal value As Short)
                _dealerID = value
            End Set
        End Property


        <ColumnInfo("ActivityTypeID", "{0}")> _
        Public Property ActivityTypeID() As Integer
            Get
                Return _activityTypeID
            End Get
            Set(ByVal value As Integer)
                _activityTypeID = value
            End Set
        End Property


        <ColumnInfo("EventParameterID", "{0}")> _
        Public Property EventParameterID() As Integer
            Get
                Return _eventParameterID
            End Get
            Set(ByVal value As Integer)
                _eventParameterID = value
            End Set
        End Property


        <ColumnInfo("ActivitySchedule", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ActivitySchedule() As DateTime
            Get
                Return _activitySchedule
            End Get
            Set(ByVal value As DateTime)
                _activitySchedule = value
            End Set
        End Property


        <ColumnInfo("ActivityPlace", "'{0}'")> _
        Public Property ActivityPlace() As String
            Get
                Return _activityPlace
            End Get
            Set(ByVal value As String)
                _activityPlace = value
            End Set
        End Property


        <ColumnInfo("InvitationNumber", "{0}")> _
        Public Property InvitationNumber() As Integer
            Get
                Return _invitationNumber
            End Get
            Set(ByVal value As Integer)
                _invitationNumber = value
            End Set
        End Property


        <ColumnInfo("AttendantNumber", "{0}")> _
        Public Property AttendantNumber() As Integer
            Get
                Return _attendantNumber
            End Get
            Set(ByVal value As Integer)
                _attendantNumber = value
            End Set
        End Property


        <ColumnInfo("EventProposalStatus", "{0}")> _
        Public Property EventProposalStatus() As Byte
            Get
                Return _eventProposalStatus
            End Get
            Set(ByVal value As Byte)
                _eventProposalStatus = value
            End Set
        End Property


        <ColumnInfo("EventAgreementStatus", "{0}")> _
        Public Property EventAgreementStatus() As Byte
            Get
                Return _eventAgreementStatus
            End Get
            Set(ByVal value As Byte)
                _eventAgreementStatus = value
            End Set
        End Property


        <ColumnInfo("Comment", "'{0}'")> _
        Public Property Comment() As String
            Get
                Return _comment
            End Get
            Set(ByVal value As String)
                _comment = value
            End Set
        End Property


        <ColumnInfo("Ravine", "'{0}'")> _
        Public Property Ravine() As String
            Get
                Return _ravine
            End Get
            Set(ByVal value As String)
                _ravine = value
            End Set
        End Property


        <ColumnInfo("SubDistrict", "'{0}'")> _
        Public Property SubDistrict() As String
            Get
                Return _subDistrict
            End Get
            Set(ByVal value As String)
                _subDistrict = value
            End Set
        End Property


        <ColumnInfo("Owner", "{0}")> _
        Public Property Owner() As Integer
            Get
                Return _owner
            End Get
            Set(ByVal value As Integer)
                _owner = value
            End Set
        End Property


        <ColumnInfo("Driver", "{0}")> _
        Public Property Driver() As Integer
            Get
                Return _driver
            End Get
            Set(ByVal value As Integer)
                _driver = value
            End Set
        End Property


        <ColumnInfo("TotalCost", "{0}")> _
        Public Property TotalCost() As Decimal
            Get
                Return _totalCost
            End Get
            Set(ByVal value As Decimal)
                _totalCost = value
            End Set
        End Property


        <ColumnInfo("ApproveCost", "{0}")> _
        Public Property ApproveCost() As Decimal
            Get
                Return _approveCost
            End Get
            Set(ByVal value As Decimal)
                _approveCost = value
            End Set
        End Property


        <ColumnInfo("SubsidiFile", "'{0}'")> _
        Public Property SubsidiFile() As String
            Get
                Return _subsidiFile
            End Get
            Set(ByVal value As String)
                _subsidiFile = value
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
        Private _activityType As ActivityType
        <ColumnInfo("ActivityTypeID", "{0}"), _
        RelationInfo("ActivityType", "ID", "V_EventProposalAgreement", "ActivityTypeID")> _
        Public Property ActivityType() As ActivityType
            Get
                Try
                    If IsNothing(_activityType) Then
                        Me._activityType = CType(DoLoad(GetType(ActivityType).ToString(), _activityTypeID), ActivityType)
                        Me._activityType.MarkLoaded()
                    End If
                    Return Me._activityType
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get
            Set(ByVal Value As ActivityType)
                Me._activityType = Value
                If (Not IsNothing(Value)) AndAlso (CType(Value, DomainObject)).IsLoaded Then
                    Me._activityType.MarkLoaded()
                End If
            End Set
        End Property
        Private _dealer As Dealer
        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "V_EventProposalAgreement", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If IsNothing(_dealer) Then
                        Me._dealer = CType(DoLoad(GetType(Dealer).ToString, _dealerID), Dealer)
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
            Set(ByVal Value As Dealer)
                Me._dealer = Value
                If (Not IsNothing(Value)) AndAlso (CType(Value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property
        Private _eventParameter As EventParameter
        <ColumnInfo("EventParameterID", "{0}"), _
        RelationInfo("EventParameter", "ID", "V_EventProposalAgreement", "EventParameterID")> _
        Public Property EventParameter() As EventParameter
            Get
                Try
                    If IsNothing(_eventParameter) Then
                        Me._eventParameter = CType(DoLoad(GetType(EventParameter).ToString(), _eventParameterID), EventParameter)
                        Me._eventParameter.MarkLoaded()
                    End If
                    Return Me._eventParameter
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get
            Set(ByVal Value As EventParameter)
                Me._eventParameter = Value
                If (Not IsNothing(Value)) AndAlso (CType(Value, DomainObject)).IsLoaded Then
                    Me._eventParameter.MarkLoaded()
                End If
            End Set
        End Property
#End Region

    End Class
End Namespace

