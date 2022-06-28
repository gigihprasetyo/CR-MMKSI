
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 08/05/2019 - 7:53:22
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
    <Serializable(), TableInfo("BabitHeader")> _
    Public Class BabitHeader
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
        Private _babitRegNumber As String = String.Empty
        Private _babitDealerNumber As String = String.Empty
        Private _allocationType As Short
        Private _periodStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _periodEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _babitStatus As Short
        Private _location As String = String.Empty
        Private _prospectTarget As Integer
        Private _luasArea As Integer
        Private _invitationQty As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _marboxID As String = String.Empty
        Private _notes As String = String.Empty
        Private _approvalNumber As String = String.Empty
        Private _sPKTarget As Integer
        Private _babitDealerGroup As String = String.Empty
        Private _eventTypeID As Short
        Private _vechileTypeKind As String = String.Empty
        Private _vechileTypeName As String = String.Empty
        Private _qtyUnit As Integer
        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _totalUnit As Integer = 0

        Private _dealer As Dealer
        Private _dealerBranch As DealerBranch
        Private _city As City
        Private _babitMasterLocation As BabitMasterLocation
        Private _babitMasterEventType As BabitMasterEventType

        Private _babitEventDetails As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _babitDocuments As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("BabitRegNumber", "'{0}'")> _
        Public Property BabitRegNumber As String
            Get
                Return _babitRegNumber
            End Get
            Set(ByVal value As String)
                _babitRegNumber = value
            End Set
        End Property


        <ColumnInfo("BabitDealerNumber", "'{0}'")> _
        Public Property BabitDealerNumber As String
            Get
                Return _babitDealerNumber
            End Get
            Set(ByVal value As String)
                _babitDealerNumber = value
            End Set
        End Property


        <ColumnInfo("AllocationType", "{0}")> _
        Public Property AllocationType As Short
            Get
                Return _allocationType
            End Get
            Set(ByVal value As Short)
                _allocationType = value
            End Set
        End Property


        <ColumnInfo("PeriodStart", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property PeriodStart As DateTime
            Get
                Return _periodStart
            End Get
            Set(ByVal value As DateTime)
                _periodStart = value
            End Set
        End Property


        <ColumnInfo("PeriodEnd", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property PeriodEnd As DateTime
            Get
                Return _periodEnd
            End Get
            Set(ByVal value As DateTime)
                _periodEnd = value
            End Set
        End Property


        <ColumnInfo("BabitStatus", "{0}")> _
        Public Property BabitStatus As Short
            Get
                Return _babitStatus
            End Get
            Set(ByVal value As Short)
                _babitStatus = value
            End Set
        End Property


        <ColumnInfo("Location", "'{0}'")> _
        Public Property Location As String
            Get
                Return _location
            End Get
            Set(ByVal value As String)
                _location = value
            End Set
        End Property


        <ColumnInfo("ProspectTarget", "{0}")> _
        Public Property ProspectTarget As Integer
            Get
                Return _prospectTarget
            End Get
            Set(ByVal value As Integer)
                _prospectTarget = value
            End Set
        End Property


        <ColumnInfo("LuasArea", "{0}")> _
        Public Property LuasArea As Integer
            Get
                Return _luasArea
            End Get
            Set(ByVal value As Integer)
                _luasArea = value
            End Set
        End Property


        <ColumnInfo("InvitationQty", "{0}")> _
        Public Property InvitationQty As Integer
            Get
                Return _invitationQty
            End Get
            Set(ByVal value As Integer)
                _invitationQty = value
            End Set
        End Property


        <ColumnInfo("MarboxID", "'{0}'")> _
        Public Property MarboxID As String
            Get
                Return _marboxID
            End Get
            Set(ByVal value As String)
                _marboxID = value
            End Set
        End Property


        <ColumnInfo("Notes", "'{0}'")> _
        Public Property Notes As String
            Get
                Return _notes
            End Get
            Set(ByVal value As String)
                _notes = value
            End Set
        End Property


        <ColumnInfo("ApprovalNumber", "'{0}'")> _
        Public Property ApprovalNumber As String
            Get
                Return _approvalNumber
            End Get
            Set(ByVal value As String)
                _approvalNumber = value
            End Set
        End Property


        <ColumnInfo("SPKTarget", "{0}")> _
        Public Property SPKTarget As Integer
            Get
                Return _sPKTarget
            End Get
            Set(ByVal value As Integer)
                _sPKTarget = value
            End Set
        End Property


        <ColumnInfo("BabitDealerGroup", "'{0}'")> _
        Public Property BabitDealerGroup As String
            Get
                Return _babitDealerGroup
            End Get
            Set(ByVal value As String)
                _babitDealerGroup = value
            End Set
        End Property

        Public Property EventTypeID As Short
            Get
                Return _eventTypeID
            End Get
            Set(ByVal value As Short)
                _eventTypeID = value
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
        RelationInfo("Dealer", "ID", "BabitHeader", "DealerID")> _
        Public Property Dealer As Dealer
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

        <ColumnInfo("DealerBranchID", "{0}"), _
        RelationInfo("DealerBranch", "ID", "BabitHeader", "DealerBranchID")> _
        Public Property DealerBranch As DealerBranch
            Get
                Try
                    If Not isnothing(Me._dealerBranch) AndAlso (Not Me._dealerBranch.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerBranch.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("CityID", "{0}"), _
        RelationInfo("City", "ID", "BabitHeader", "CityID")> _
        Public Property City As City
            Get
                Try
                    If Not IsNothing(Me._city) AndAlso (Not Me._city.IsLoaded) Then

                        Me._city = CType(DoLoad(GetType(City).ToString(), _city.ID), City)
                        Me._city.MarkLoaded()
                    End If
                    Return Me._city
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(ByVal value As City)
                Me._city = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._city.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("BabitMasterEventTypeID", "{0}"), _
        RelationInfo("BabitMasterEventType", "ID", "BabitHeader", "BabitMasterEventTypeID")> _
        Public Property BabitMasterEventType As BabitMasterEventType
            Get
                Try
                    If Not IsNothing(Me._BabitMasterEventType) AndAlso (Not Me._BabitMasterEventType.IsLoaded) Then

                        Me._BabitMasterEventType = CType(DoLoad(GetType(BabitMasterEventType).ToString(), _BabitMasterEventType.ID), BabitMasterEventType)
                        Me._BabitMasterEventType.MarkLoaded()
                    End If
                    Return Me._BabitMasterEventType
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(ByVal value As BabitMasterEventType)
                Me._BabitMasterEventType = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._BabitMasterEventType.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("BabitMasterLocationID", "{0}"), _
        RelationInfo("BabitMasterLocation", "ID", "BabitHeader", "BabitMasterLocationID")> _
        Public Property BabitMasterLocation As BabitMasterLocation
            Get
                Try
                    If Not IsNothing(Me._babitMasterLocation) AndAlso (Not Me._babitMasterLocation.IsLoaded) Then

                        Me._babitMasterLocation = CType(DoLoad(GetType(BabitMasterLocation).ToString(), _babitMasterLocation.ID), BabitMasterLocation)
                        Me._babitMasterLocation.MarkLoaded()
                    End If
                    Return Me._babitMasterLocation
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(ByVal value As BabitMasterLocation)
                Me._babitMasterLocation = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._babitMasterLocation.MarkLoaded()
                End If
            End Set
        End Property

        <RelationInfo("BabitHeader", "ID", "BabitEventDetail", "BabitHeaderID")> _
        Public ReadOnly Property BabitEventDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._babitEventDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BabitEventDetail), "BabitHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BabitEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._babitEventDetails = DoLoadArray(GetType(BabitEventDetail).ToString, criterias)
                    End If

                    Return Me._babitEventDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("BabitHeader", "ID", "BabitDocument", "BabitHeaderID")> _
        Public ReadOnly Property BabitDocuments As System.Collections.ArrayList
            Get
                Try
                    If (Me._babitDocuments.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BabitDocument), "BabitHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BabitDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._babitDocuments = DoLoadArray(GetType(BabitDocument).ToString, criterias)
                    End If

                    Return Me._babitDocuments

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
        Public Property VechileTypeKind As String
            Get
                Return _vechileTypeKind
            End Get
            Set(ByVal value As String)
                _vechileTypeKind = value
            End Set
        End Property

        Public Property VechileTypeName As String
            Get
                Return _vechileTypeName
            End Get
            Set(ByVal value As String)
                _vechileTypeName = value
            End Set
        End Property

        Public Property QtyUnit As Integer
            Get
                Return _qtyUnit
            End Get
            Set(ByVal value As Integer)
                _qtyUnit = value
            End Set
        End Property

        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property

        Public Property DealerName As String
            Get
                Return _dealerName
            End Get
            Set(ByVal value As String)
                _dealerName = value
            End Set
        End Property

        Public Property TotalUnit As Integer
            Get
                Return _totalUnit
            End Get
            Set(ByVal value As Integer)
                _totalUnit = value
            End Set
        End Property

#End Region

    End Class
End Namespace

