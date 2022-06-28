
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPKDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 6/8/2012 - 11:37:46 AM
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
    <Serializable(), TableInfo("SPKDetail")> _
    Public Class SPKDetail
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
        Private _lineItem As Short
        Private _vehicleTypeCode As String = String.Empty
        Private _vehicleColorCode As String = String.Empty
        Private _vehicleColorName As String = String.Empty
        Private _additional As Byte
        Private _remarks As String = String.Empty
        Private _quantity As Integer
        Private _amount As Decimal
        Private _totalAmount As Decimal
        Private _rejectedReason As String = String.Empty
        Private _status As Byte
        Private _rowStatus As Short
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _createdBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _campaignName As String = String.Empty
        Private _eventType As Integer

        Private _category As Category
        Private _sPKHeader As SPKHeader
        Private _vechileColor As VechileColor
        Private _profileDetail As ProfileDetail
        Private _vehicleKind As VehicleKind

        Private _sPKProfiles As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _sPKDetailCustomers As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("LineItem", "{0}")> _
        Public Property LineItem() As Short
            Get
                Return _lineItem
            End Get
            Set(ByVal value As Short)
                _lineItem = value
            End Set
        End Property


        <ColumnInfo("VehicleTypeCode", "'{0}'")> _
        Public Property VehicleTypeCode() As String
            Get
                Return _vehicleTypeCode
            End Get
            Set(ByVal value As String)
                _vehicleTypeCode = value
            End Set
        End Property


        <ColumnInfo("VehicleColorCode", "'{0}'")> _
        Public Property VehicleColorCode() As String
            Get
                Return _vehicleColorCode
            End Get
            Set(ByVal value As String)
                _vehicleColorCode = value
            End Set
        End Property


        <ColumnInfo("VehicleColorName", "'{0}'")> _
        Public Property VehicleColorName() As String
            Get
                Return _vehicleColorName
            End Get
            Set(ByVal value As String)
                _vehicleColorName = value
            End Set
        End Property


        <ColumnInfo("Additional", "{0}")> _
        Public Property Additional() As Byte
            Get
                Return _additional
            End Get
            Set(ByVal value As Byte)
                _additional = value
            End Set
        End Property


        <ColumnInfo("Remarks", "'{0}'")> _
        Public Property Remarks() As String
            Get
                Return _remarks
            End Get
            Set(ByVal value As String)
                _remarks = value
            End Set
        End Property


        <ColumnInfo("Quantity", "{0}")> _
        Public Property Quantity() As Integer
            Get
                Return _quantity
            End Get
            Set(ByVal value As Integer)
                _quantity = value
            End Set
        End Property


        <ColumnInfo("Amount", "{0}")> _
        Public Property Amount() As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
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


        <ColumnInfo("RejectedReason", "'{0}'")> _
        Public Property RejectedReason() As String
            Get
                Return _rejectedReason
            End Get
            Set(ByVal value As String)
                _rejectedReason = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Byte
            Get
                Return _status
            End Get
            Set(ByVal value As Byte)
                _status = value
            End Set
        End Property


        <ColumnInfo("CampaignName", "'{0}'")> _
        Public Property CampaignName() As String
            Get
                Return _campaignName
            End Get
            Set(ByVal value As String)
                _campaignName = value
            End Set
        End Property

        <ColumnInfo("EventType", "{0}")> _
        Public Property EventType() As Integer
            Get
                Return _eventType
            End Get
            Set(ByVal value As Integer)
                _eventType = value
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


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
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


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
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


        <ColumnInfo("CategoryID", "{0}"), _
        RelationInfo("Category", "ID", "SPKDetail", "CategoryID")> _
        Public Property Category() As Category
            Get
                Try
                    If Not isnothing(Me._category) AndAlso (Not Me._category.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._category.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SPKHeaderID", "{0}"), _
        RelationInfo("SPKHeader", "ID", "SPKDetail", "SPKHeaderID")> _
        Public Property SPKHeader() As SPKHeader
            Get
                Try
                    If Not isnothing(Me._sPKHeader) AndAlso (Not Me._sPKHeader.IsLoaded) Then

                        Me._sPKHeader = CType(DoLoad(GetType(SPKHeader).ToString(), _sPKHeader.ID), SPKHeader)
                        Me._sPKHeader.MarkLoaded()

                    End If

                    Return Me._sPKHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPKHeader)

                Me._sPKHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sPKHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("VehicleColorID", "{0}"), _
        RelationInfo("VechileColor", "ID", "SPKDetail", "VehicleColorID")> _
        Public Property VechileColor() As VechileColor
            Get
                Try
                    If Not isnothing(Me._vechileColor) AndAlso (Not Me._vechileColor.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileColor.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ProfileDetailID", "{0}"), _
        RelationInfo("ProfileDetail", "ID", "SPKDetail", "ProfileDetailID")> _
        Public Property ProfileDetail() As ProfileDetail
            Get
                Try
                    If Not isnothing(Me._profileDetail) AndAlso (Not Me._profileDetail.IsLoaded) Then

                        Me._profileDetail = CType(DoLoad(GetType(ProfileDetail).ToString(), _profileDetail.ID), ProfileDetail)
                        Me._profileDetail.MarkLoaded()

                    End If

                    Return Me._profileDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ProfileDetail)

                Me._profileDetail = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._profileDetail.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("VehicleKindID", "{0}"), _
        RelationInfo("VehicleKind", "ID", "SPKDetail", "VehicleKindID")> _
        Public Property VehicleKind() As VehicleKind
            Get
                Try
                    If Not isnothing(Me._vehicleKind) AndAlso (Not Me._vehicleKind.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vehicleKind.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("SPKDetail", "ID", "SPKProfile", "SPKDetailID")> _
        Public ReadOnly Property SPKProfiles() As System.Collections.ArrayList
            Get
                Try
                    If (Me._sPKProfiles.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SPKProfile), "SPKDetail", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SPKProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sPKProfiles = DoLoadArray(GetType(SPKProfile).ToString, criterias)
                    End If

                    Return Me._sPKProfiles

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property


        <RelationInfo("SPKDetail", "ID", "SPKDetailCustomer", "SPKDetailID")> _
        Public ReadOnly Property SPKDetailCustomers() As System.Collections.ArrayList
            Get
                Try
                    If (Me._sPKDetailCustomers.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SPKDetailCustomer), "SPKDetail", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SPKDetailCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sPKDetailCustomers = DoLoadArray(GetType(SPKDetailCustomer).ToString, criterias)
                    End If

                    Return Me._sPKDetailCustomers

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

    End Class
End Namespace

