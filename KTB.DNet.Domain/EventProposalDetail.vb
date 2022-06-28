#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EventProposalDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 9/2/2009 - 5:33:28 PM
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
    <Serializable(), TableInfo("EventProposalDetail")> _
    Public Class EventProposalDetail
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
        Private _item As String = String.Empty
        Private _quantity As Integer
        Private _unitCost As Decimal
        Private _description As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _eventProposal As EventProposal
        Private _eventActivityType As EventActivityType
        Private _vechileType As VechileType



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


        <ColumnInfo("Item", "'{0}'")> _
        Public Property Item() As String
            Get
                Return _item
            End Get
            Set(ByVal value As String)
                _item = value
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


        <ColumnInfo("UnitCost", "{0}")> _
        Public Property UnitCost() As Decimal
            Get
                Return _unitCost
            End Get
            Set(ByVal value As Decimal)
                _unitCost = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
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


        <ColumnInfo("EventProposalID", "{0}"), _
        RelationInfo("EventProposal", "ID", "EventProposalDetail", "EventProposalID")> _
        Public Property EventProposal() As EventProposal
            Get
                Try
                    If Not isnothing(Me._eventProposal) AndAlso (Not Me._eventProposal.IsLoaded) Then

                        Me._eventProposal = CType(DoLoad(GetType(EventProposal).ToString(), _eventProposal.ID), EventProposal)
                        Me._eventProposal.MarkLoaded()

                    End If

                    Return Me._eventProposal

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As EventProposal)

                Me._eventProposal = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._eventProposal.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("EventActivityTypeID", "{0}"), _
        RelationInfo("EventActivityType", "ID", "EventProposalDetail", "EventActivityTypeID")> _
        Public Property EventActivityType() As EventActivityType
            Get
                Try
                    If Not isnothing(Me._eventActivityType) AndAlso (Not Me._eventActivityType.IsLoaded) Then

                        Me._eventActivityType = CType(DoLoad(GetType(EventActivityType).ToString(), _eventActivityType.ID), EventActivityType)
                        Me._eventActivityType.MarkLoaded()

                    End If

                    Return Me._eventActivityType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As EventActivityType)

                Me._eventActivityType = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._eventActivityType.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("VechileTypeID", "{0}"), _
        RelationInfo("VechileType", "ID", "EventProposalDetail", "VechileTypeID")> _
        Public Property VechileType() As VechileType
            Get
                Try
                    If Not isnothing(Me._vechileType) AndAlso (Not Me._vechileType.IsLoaded) Then

                        Me._vechileType = CType(DoLoad(GetType(VechileType).ToString(), _vechileType.ID), VechileType)
                        Me._vechileType.MarkLoaded()

                    End If

                    Return Me._vechileType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileType)

                Me._vechileType = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileType.MarkLoaded()
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
        Public ReadOnly Property TotalCost() As Decimal
            Get
                Return _unitCost * _quantity
            End Get
        End Property
#End Region

    End Class
End Namespace

