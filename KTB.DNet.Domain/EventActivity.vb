#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EventActivity Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/28/2007 - 8:59:39 AM
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
    <Serializable(), TableInfo("EventActivity")> _
    Public Class EventActivity
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
        Private _place As String = String.Empty
        Private _comsumption As Decimal
        Private _entertainment As Decimal
        Private _equipment As Decimal
        Private _others As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _bPEvent As BPEvent



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


        <ColumnInfo("Place", "'{0}'")> _
        Public Property Place() As String
            Get
                Return _place
            End Get
            Set(ByVal value As String)
                _place = value
            End Set
        End Property


        <ColumnInfo("Comsumption", "{0}")> _
        Public Property Comsumption() As Decimal
            Get
                Return _comsumption
            End Get
            Set(ByVal value As Decimal)
                _comsumption = value
            End Set
        End Property


        <ColumnInfo("Entertainment", "{0}")> _
        Public Property Entertainment() As Decimal
            Get
                Return _entertainment
            End Get
            Set(ByVal value As Decimal)
                _entertainment = value
            End Set
        End Property


        <ColumnInfo("Equipment", "{0}")> _
        Public Property Equipment() As Decimal
            Get
                Return _equipment
            End Get
            Set(ByVal value As Decimal)
                _equipment = value
            End Set
        End Property


        <ColumnInfo("Others", "{0}")> _
        Public Property Others() As Decimal
            Get
                Return _others
            End Get
            Set(ByVal value As Decimal)
                _others = value
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


        <ColumnInfo("BPEventID", "{0}"), _
        RelationInfo("BPEvent", "ID", "EventActivity", "BPEventID")> _
        Public Property BPEvent() As BPEvent
            Get
                Try
                    If Not isnothing(Me._bPEvent) AndAlso (Not Me._bPEvent.IsLoaded) Then

                        Me._bPEvent = CType(DoLoad(GetType(BPEvent).ToString(), _bPEvent.ID), BPEvent)
                        Me._bPEvent.MarkLoaded()

                    End If

                    Return Me._bPEvent

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BPEvent)

                Me._bPEvent = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._bPEvent.MarkLoaded()
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

