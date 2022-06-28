#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_EventProposalExcel Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 9/10/2009 - 11:57:17 AM
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
    <Serializable(), TableInfo("V_EventProposalExcel")> _
    Public Class V_EventProposalExcel
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
        Private _eventProposalID As Integer
        Private _eventProposalStatus As Byte
        Private _eventName As String = String.Empty
        Private _activityName As String = String.Empty
        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _activitySchedule As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _activityPlace As String = String.Empty
        Private _totalCost As Decimal
        Private _tamuName As String = String.Empty
        Private _guestType As String = String.Empty
        Private _jabatanName As String = String.Empty
        Private _rowStatus As Integer
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


        <ColumnInfo("EventProposalID", "{0}")> _
        Public Property EventProposalID() As Integer
            Get
                Return _eventProposalID
            End Get
            Set(ByVal value As Integer)
                _eventProposalID = value
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


        <ColumnInfo("EventName", "'{0}'")> _
        Public Property EventName() As String
            Get
                Return _eventName
            End Get
            Set(ByVal value As String)
                _eventName = value
            End Set
        End Property


        <ColumnInfo("ActivityName", "'{0}'")> _
        Public Property ActivityName() As String
            Get
                Return _activityName
            End Get
            Set(ByVal value As String)
                _activityName = value
            End Set
        End Property


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("DealerName", "'{0}'")> _
        Public Property DealerName() As String
            Get
                Return _dealerName
            End Get
            Set(ByVal value As String)
                _dealerName = value
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

        <ColumnInfo("TotalCost", "{0}")> _
        Public Property TotalCost() As Decimal
            Get
                Return _totalCost
            End Get
            Set(ByVal value As Decimal)
                _totalCost = value
            End Set
        End Property


        <ColumnInfo("TamuName", "'{0}'")> _
        Public Property TamuName() As String
            Get
                Return _tamuName
            End Get
            Set(ByVal value As String)
                _tamuName = value
            End Set
        End Property


        <ColumnInfo("GuestType", "'{0}'")> _
        Public Property GuestType() As String
            Get
                Return _guestType
            End Get
            Set(ByVal value As String)
                _guestType = value
            End Set
        End Property


        <ColumnInfo("JabatanName", "'{0}'")> _
        Public Property JabatanName() As String
            Get
                Return _jabatanName
            End Get
            Set(ByVal value As String)
                _jabatanName = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus() As Integer
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Integer)
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

#End Region

    End Class
End Namespace

