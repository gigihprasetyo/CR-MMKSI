
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrClassAllocation Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/14/2005 - 10:28:46 AM
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
    <Serializable(), TableInfo("TrClassAllocation")> _
    Public Class TrClassAllocation
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
        Private _allocated As Integer
        Private _lastAllocated As Integer
        Private _cancelReason As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _classID As Integer
        Private _trClass As TrClass
        Private _dealer As Dealer



#End Region

#Region "Public Properties"

        Public ReadOnly Property ClassCode() As String
            Get
                If Not IsNothing(Me._trClass) Then Return Me._trClass.ClassCode
                Return ""
            End Get
        End Property

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("Allocated", "{0}")> _
        Public Property Allocated() As Integer
            Get
                Return _allocated
            End Get
            Set(ByVal value As Integer)
                _allocated = value
            End Set
        End Property


        <ColumnInfo("LastAllocated", "{0}")> _
        Public Property LastAllocated() As Integer
            Get
                Return _lastAllocated
            End Get
            Set(ByVal value As Integer)
                _lastAllocated = value
            End Set
        End Property


        <ColumnInfo("CancelReason", "'{0}'")> _
        Public Property CancelReason() As String
            Get
                Return _cancelReason
            End Get
            Set(ByVal value As String)
                _cancelReason = value
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




        <ColumnInfo("ClassID", "{0}"), _
        RelationInfo("TrClass", "ID", "TrClassAllocation", "ClassID")> _
        Public Property TrClass() As TrClass
            Get
                Try
                    If Not IsNothing(Me._trClass) AndAlso (Not Me._trClass.IsLoaded) Then

                        Me._trClass = CType(DoLoad(GetType(TrClass).ToString(), _trClass.ID), TrClass)
                        Me._trClass.MarkLoaded()

                    End If

                    Return Me._trClass

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrClass)

                Me._trClass = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trClass.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "TrClassAllocation", "DealerID")> _
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

        <ColumnInfo("ClassID", "{0}")> _
        Public Property ClassID() As Integer
            Get
                Return _classID
            End Get
            Set(ByVal value As Integer)
                _classID = value
            End Set
        End Property

#End Region

#Region "Non Generated Public Properties"
        Private _isAlreadyAllocated As Boolean = True
        Public Property IsAlreadyAllocated() As Boolean
            Get
                Return _isAlreadyAllocated
            End Get
            Set(ByVal value As Boolean)
                _isAlreadyAllocated = value
            End Set
        End Property

        Private _isPickOnSearch As Boolean = True
        Public Property IsPickOnSearch() As Boolean
            Get
                Return _isPickOnSearch
            End Get
            Set(ByVal value As Boolean)
                _isPickOnSearch = value
            End Set
        End Property

        Private _allocatedBefore As Integer = 0
        Public Property AllocatedBefore() As Integer
            Get
                Return _allocatedBefore
            End Get
            Set(ByVal value As Integer)
                _allocatedBefore = value
            End Set
        End Property

#End Region

#Region "Custom Method"

        'For Optimizing
        Private _history As String
        Public Property History() As String
            Get
                Return _history
            End Get
            Set(ByVal value As String)
                _history = value
            End Set
        End Property


        Private _allocatedTaken As Integer
        Public Property AllocatedTaken() As Integer
            Get
                Return _allocatedTaken
            End Get
            Set(ByVal value As Integer)
                _allocatedTaken = value
            End Set
        End Property

#End Region
    End Class
End Namespace

