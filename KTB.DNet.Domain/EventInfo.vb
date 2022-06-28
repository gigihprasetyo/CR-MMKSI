#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EventInfo Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/9/2007 - 2:14:52 PM
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
    <Serializable(), TableInfo("EventInfo")> _
    Public Class EventInfo
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
        Private _babitAllocationID As Integer
        Private _eventRequestNo As String = String.Empty
        Private _eventApprovalNo As String = String.Empty
        Private _dateStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dateEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _location As String = String.Empty
        Private _numOfInvitation As Integer
        Private _areaCoordinator As String = String.Empty
        Private _observer As String = String.Empty
        Private _eventInfoStatus As Byte
        Private _isConfirmed As Byte
        Private _confirmedDateStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _confirmedDateEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _confirmedLocation As String = String.Empty
        Private _confirmedNumOfInvitation As Integer
        Private _confirmedTotalCost As Decimal
        Private _confirmedEstFileUpload As String = String.Empty
        Private _confirmedComment As String = String.Empty
        Private _isRealization As Byte
        Private _eventRealizationNo As String = String.Empty
        Private _realDateStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _realDateEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _realLocation As String = String.Empty
        Private _realNumOfInvitation As Integer
        Private _realNumOfParticipants As Integer
        Private _realTotalCost As Decimal
        Private _realCostDetailFile As String = String.Empty
        Private _realVideoFile As String = String.Empty
        Private _realMatPromoFile As String = String.Empty
        Private _realComment As String = String.Empty
        Private _requestDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _requestTotalCost As Decimal
        Private _approvalCost As Decimal
        Private _realApprovalCost As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _eventType As EventType
        Private _eventMaster As EventMaster

        Private _eventSaless As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("BabitAllocationID", "{0}")> _
        Public Property BabitAllocationID() As Integer
            Get
                Return _babitAllocationID
            End Get
            Set(ByVal value As Integer)
                _babitAllocationID = value
            End Set
        End Property


        <ColumnInfo("EventRequestNo", "'{0}'")> _
        Public Property EventRequestNo() As String
            Get
                Return _eventRequestNo
            End Get
            Set(ByVal value As String)
                _eventRequestNo = value
            End Set
        End Property


        <ColumnInfo("EventApprovalNo", "'{0}'")> _
        Public Property EventApprovalNo() As String
            Get
                Return _eventApprovalNo
            End Get
            Set(ByVal value As String)
                _eventApprovalNo = value
            End Set
        End Property


        <ColumnInfo("DateStart", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property DateStart() As DateTime
            Get
                Return _dateStart
            End Get
            Set(ByVal value As DateTime)
                _dateStart = value
            End Set
        End Property


        <ColumnInfo("DateEnd", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property DateEnd() As DateTime
            Get
                Return _dateEnd
            End Get
            Set(ByVal value As DateTime)
                _dateEnd = value
            End Set
        End Property


        <ColumnInfo("Location", "'{0}'")> _
        Public Property Location() As String
            Get
                Return _location
            End Get
            Set(ByVal value As String)
                _location = value
            End Set
        End Property


        <ColumnInfo("NumOfInvitation", "{0}")> _
        Public Property NumOfInvitation() As Integer
            Get
                Return _numOfInvitation
            End Get
            Set(ByVal value As Integer)
                _numOfInvitation = value
            End Set
        End Property


        <ColumnInfo("AreaCoordinator", "'{0}'")> _
        Public Property AreaCoordinator() As String
            Get
                Return _areaCoordinator
            End Get
            Set(ByVal value As String)
                _areaCoordinator = value
            End Set
        End Property


        <ColumnInfo("Observer", "'{0}'")> _
        Public Property Observer() As String
            Get
                Return _observer
            End Get
            Set(ByVal value As String)
                _observer = value
            End Set
        End Property


        <ColumnInfo("EventInfoStatus", "{0}")> _
        Public Property EventInfoStatus() As Byte
            Get
                Return _eventInfoStatus
            End Get
            Set(ByVal value As Byte)
                _eventInfoStatus = value
            End Set
        End Property


        <ColumnInfo("IsConfirmed", "{0}")> _
        Public Property IsConfirmed() As Byte
            Get
                Return _isConfirmed
            End Get
            Set(ByVal value As Byte)
                _isConfirmed = value
            End Set
        End Property


        <ColumnInfo("ConfirmedDateStart", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ConfirmedDateStart() As DateTime
            Get
                Return _confirmedDateStart
            End Get
            Set(ByVal value As DateTime)
                _confirmedDateStart = value
            End Set
        End Property


        <ColumnInfo("ConfirmedDateEnd", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ConfirmedDateEnd() As DateTime
            Get
                Return _confirmedDateEnd
            End Get
            Set(ByVal value As DateTime)
                _confirmedDateEnd = value
            End Set
        End Property


        <ColumnInfo("ConfirmedLocation", "'{0}'")> _
        Public Property ConfirmedLocation() As String
            Get
                Return _confirmedLocation
            End Get
            Set(ByVal value As String)
                _confirmedLocation = value
            End Set
        End Property


        <ColumnInfo("ConfirmedNumOfInvitation", "{0}")> _
        Public Property ConfirmedNumOfInvitation() As Integer
            Get
                Return _confirmedNumOfInvitation
            End Get
            Set(ByVal value As Integer)
                _confirmedNumOfInvitation = value
            End Set
        End Property


        <ColumnInfo("ConfirmedTotalCost", "{0}")> _
        Public Property ConfirmedTotalCost() As Decimal
            Get
                Return _confirmedTotalCost
            End Get
            Set(ByVal value As Decimal)
                _confirmedTotalCost = value
            End Set
        End Property


        <ColumnInfo("ConfirmedEstFileUpload", "'{0}'")> _
        Public Property ConfirmedEstFileUpload() As String
            Get
                Return _confirmedEstFileUpload
            End Get
            Set(ByVal value As String)
                _confirmedEstFileUpload = value
            End Set
        End Property


        <ColumnInfo("ConfirmedComment", "'{0}'")> _
        Public Property ConfirmedComment() As String
            Get
                Return _confirmedComment
            End Get
            Set(ByVal value As String)
                _confirmedComment = value
            End Set
        End Property


        <ColumnInfo("IsRealization", "{0}")> _
        Public Property IsRealization() As Byte
            Get
                Return _isRealization
            End Get
            Set(ByVal value As Byte)
                _isRealization = value
            End Set
        End Property


        <ColumnInfo("EventRealizationNo", "'{0}'")> _
        Public Property EventRealizationNo() As String
            Get
                Return _eventRealizationNo
            End Get
            Set(ByVal value As String)
                _eventRealizationNo = value
            End Set
        End Property


        <ColumnInfo("RealDateStart", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property RealDateStart() As DateTime
            Get
                Return _realDateStart
            End Get
            Set(ByVal value As DateTime)
                _realDateStart = value
            End Set
        End Property


        <ColumnInfo("RealDateEnd", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property RealDateEnd() As DateTime
            Get
                Return _realDateEnd
            End Get
            Set(ByVal value As DateTime)
                _realDateEnd = value
            End Set
        End Property


        <ColumnInfo("RealLocation", "'{0}'")> _
        Public Property RealLocation() As String
            Get
                Return _realLocation
            End Get
            Set(ByVal value As String)
                _realLocation = value
            End Set
        End Property


        <ColumnInfo("RealNumOfInvitation", "{0}")> _
        Public Property RealNumOfInvitation() As Integer
            Get
                Return _realNumOfInvitation
            End Get
            Set(ByVal value As Integer)
                _realNumOfInvitation = value
            End Set
        End Property


        <ColumnInfo("RealNumOfParticipants", "{0}")> _
        Public Property RealNumOfParticipants() As Integer
            Get
                Return _realNumOfParticipants
            End Get
            Set(ByVal value As Integer)
                _realNumOfParticipants = value
            End Set
        End Property


        <ColumnInfo("RealTotalCost", "{0}")> _
        Public Property RealTotalCost() As Decimal
            Get
                Return _realTotalCost
            End Get
            Set(ByVal value As Decimal)
                _realTotalCost = value
            End Set
        End Property


        <ColumnInfo("RealCostDetailFile", "'{0}'")> _
        Public Property RealCostDetailFile() As String
            Get
                Return _realCostDetailFile
            End Get
            Set(ByVal value As String)
                _realCostDetailFile = value
            End Set
        End Property


        <ColumnInfo("RealVideoFile", "'{0}'")> _
        Public Property RealVideoFile() As String
            Get
                Return _realVideoFile
            End Get
            Set(ByVal value As String)
                _realVideoFile = value
            End Set
        End Property


        <ColumnInfo("RealMatPromoFile", "'{0}'")> _
        Public Property RealMatPromoFile() As String
            Get
                Return _realMatPromoFile
            End Get
            Set(ByVal value As String)
                _realMatPromoFile = value
            End Set
        End Property


        <ColumnInfo("RealComment", "'{0}'")> _
        Public Property RealComment() As String
            Get
                Return _realComment
            End Get
            Set(ByVal value As String)
                _realComment = value
            End Set
        End Property


        <ColumnInfo("RequestDate", "'{0:yyyy/MM/dd}'")> _
        Public Property RequestDate() As DateTime
            Get
                Return _requestDate
            End Get
            Set(ByVal value As DateTime)
                _requestDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("RequestTotalCost", "{0}")> _
        Public Property RequestTotalCost() As Decimal
            Get
                Return _requestTotalCost
            End Get
            Set(ByVal value As Decimal)
                _requestTotalCost = value
            End Set
        End Property


        <ColumnInfo("ApprovalCost", "{0}")> _
        Public Property ApprovalCost() As Decimal
            Get
                Return _approvalCost
            End Get
            Set(ByVal value As Decimal)
                _approvalCost = value
            End Set
        End Property


        <ColumnInfo("RealApprovalCost", "{0}")> _
        Public Property RealApprovalCost() As Decimal
            Get
                Return _realApprovalCost
            End Get
            Set(ByVal value As Decimal)
                _realApprovalCost = value
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


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "EventInfo", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not isnothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("EventTypeID", "{0}"), _
        RelationInfo("EventType", "ID", "EventInfo", "EventTypeID")> _
        Public Property EventType() As EventType
            Get
                Try
                    If Not isnothing(Me._eventType) AndAlso (Not Me._eventType.IsLoaded) Then

                        Me._eventType = CType(DoLoad(GetType(EventType).ToString(), _eventType.ID), EventType)
                        Me._eventType.MarkLoaded()

                    End If

                    Return Me._eventType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As EventType)

                Me._eventType = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._eventType.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("EventMasterID", "{0}"), _
        RelationInfo("EventMaster", "ID", "EventInfo", "EventMasterID")> _
        Public Property EventMaster() As EventMaster
            Get
                Try
                    If Not isnothing(Me._eventMaster) AndAlso (Not Me._eventMaster.IsLoaded) Then

                        Me._eventMaster = CType(DoLoad(GetType(EventMaster).ToString(), _eventMaster.ID), EventMaster)
                        Me._eventMaster.MarkLoaded()

                    End If

                    Return Me._eventMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As EventMaster)

                Me._eventMaster = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._eventMaster.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("EventInfo", "ID", "EventSales", "EventInfoID")> _
        Public ReadOnly Property EventSaless() As System.Collections.ArrayList
            Get
                Try
                    If (Me._eventSaless.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(EventSales), "EventInfo", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(EventSales), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._eventSaless = DoLoadArray(GetType(EventSales).ToString, criterias)
                    End If

                    Return Me._eventSaless

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
#Region "Custom Properties"

        Public ReadOnly Property NumOfSalesPC() As Integer
            Get

                If Me.ID = 0 Then
                    Return 0
                End If

                Dim counter As Integer = 0
                For Each itemSales As EventSales In Me.EventSaless
                    If itemSales.VechileType.Category.CategoryCode.Trim.ToUpper = "PC" Then
                        counter += 1
                    End If
                Next

                Return counter

            End Get
        End Property

        Public ReadOnly Property NumOfSalesLCV() As Integer
            Get
                'Todo Aggregate
                If Me.ID = 0 Then
                    Return 0
                End If

                Dim counter As Integer = 0
                For Each itemSales As EventSales In Me.EventSaless
                    If itemSales.VechileType.Category.CategoryCode.Trim.ToUpper = "LCV" Then
                        counter += 1
                    End If
                Next

                Return counter

            End Get
        End Property

        Public ReadOnly Property NumOfSalesCV() As Integer
            Get
                'Todo Aggregate

                If Me.ID = 0 Then
                    Return 0
                End If

                Dim counter As Integer = 0
                For Each itemSales As EventSales In Me.EventSaless
                    If itemSales.VechileType.Category.CategoryCode.Trim.ToUpper = "CV" Then
                        counter += 1
                    End If
                Next

                Return counter

            End Get
        End Property

        Public ReadOnly Property TotalOfSales() As Integer
            Get
                'Todo Aggregate
                If Me.ID = 0 Then
                    Return 0
                End If

                Dim counter As Integer = 0
                For Each itemSales As EventSales In Me.EventSaless
                    If itemSales.VechileType.Category.CategoryCode.Trim.ToUpper = "CV" Then
                        counter += 1
                    ElseIf itemSales.VechileType.Category.CategoryCode.Trim.ToUpper = "LCV" Then
                        counter += 1
                    ElseIf itemSales.VechileType.Category.CategoryCode.Trim.ToUpper = "PC" Then
                        counter += 1
                    End If
                Next

                Return counter
            End Get
        End Property
#End Region

    End Class
End Namespace

