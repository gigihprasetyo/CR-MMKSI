#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ClaimDebitNoteDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 8/6/2020 - 5:55:25 PM
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
    <Serializable(), TableInfo("ClaimDebitNoteDetail")> _
    Public Class ClaimDebitNoteDetail
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
        'Private _claimDebitNoteID As Integer
        'private _sparePartDODetailID as integer 		
        Private _qty As Integer
        Private _amount As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _claimDebitNote As ClaimDebitNote
        Private _sparePartDODetail As SparePartDODetail

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


        '<ColumnInfo("ClaimDebitNoteID", "{0}")> _
        'Public Property ClaimDebitNoteID As Integer
        '    Get
        '        Return _claimDebitNoteID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _claimDebitNoteID = value
        '    End Set
        'End Property


        '<ColumnInfo("SparePartDODetailID", "{0}")> _
        'Public Property SparePartDODetailID As Integer
        '    Get
        '        Return _sparePartDODetailID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _sparePartDODetailID = value
        '    End Set
        'End Property


        <ColumnInfo("Qty", "{0}")> _
        Public Property Qty As Integer
            Get
                Return _qty
            End Get
            Set(ByVal value As Integer)
                _qty = value
            End Set
        End Property


        <ColumnInfo("Amount", "{0}")> _
        Public Property Amount As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
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


        <ColumnInfo("ClaimDebitNoteID", "{0}"), _
        RelationInfo("ClaimDebitNote", "ID", "ClaimDebitNoteDetail", "ClaimHeaderID")> _
        Public Property ClaimDebitNote() As ClaimDebitNote
            Get
                Try
                    If Not IsNothing(Me._claimDebitNote) AndAlso (Not Me._claimDebitNote.IsLoaded) Then
                        Me._claimDebitNote = CType(DoLoad(GetType(ClaimDebitNote).ToString(), _claimDebitNote.ID), ClaimDebitNote)
                        Me._claimDebitNote.MarkLoaded()
                    End If
                    Return Me._claimDebitNote
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get
            Set(ByVal value As ClaimDebitNote)
                Me._claimDebitNote = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._claimDebitNote.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("SparePartDODetailID", "{0}"), _
        RelationInfo("SparePartDODetail", "ID", "ClaimDebitNoteDetail", "SparePartDODetailID")> _
        Public Property SparePartDODetail() As SparePartDODetail
            Get
                Try
                    If Not IsNothing(Me._sparePartDODetail) AndAlso (Not Me._sparePartDODetail.IsLoaded) Then
                        Me._sparePartDODetail = CType(DoLoad(GetType(SparePartDODetail).ToString(), _sparePartDODetail.ID), SparePartDODetail)
                        Me._sparePartDODetail.MarkLoaded()
                    End If
                    Return Me._sparePartDODetail
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get
            Set(ByVal value As SparePartDODetail)
                Me._sparePartDODetail = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartDODetail.MarkLoaded()
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
