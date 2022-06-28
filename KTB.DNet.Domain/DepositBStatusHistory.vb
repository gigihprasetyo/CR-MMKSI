
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositBStatusHistory Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 6/1/2016 - 11:19:33 AM
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
    <Serializable(), TableInfo("DepositBStatusHistory")> _
    Public Class DepositBStatusHistory
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
        Private _statusType As Short
        Private _oldStatus As Short
        Private _newStatus As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _depositBPencairanHeader As DepositBPencairanHeader
        Private _depositBDebitNote As DepositBDebitNote
        Private _depositBInterestHeader As DepositBInterestHeader
        Private _depositBKewajibanHeader As DepositBKewajibanHeader
        'Private _indentPartHeader As IndentPartHeader



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


        <ColumnInfo("StatusType", "{0}")> _
        Public Property StatusType As Short
            Get
                Return _statusType
            End Get
            Set(ByVal value As Short)
                _statusType = value
            End Set
        End Property


        <ColumnInfo("OldStatus", "{0}")> _
        Public Property OldStatus As Short
            Get
                Return _oldStatus
            End Get
            Set(ByVal value As Short)
                _oldStatus = value
            End Set
        End Property


        <ColumnInfo("NewStatus", "{0}")> _
        Public Property NewStatus As Short
            Get
                Return _newStatus
            End Get
            Set(ByVal value As Short)
                _newStatus = value
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


        <ColumnInfo("DepositBPencairanHeaderID", "{0}"), _
        RelationInfo("DepositBPencairanHeader", "ID", "DepositBStatusHistory", "DepositBPencairanHeaderID")> _
        Public Property DepositBPencairanHeader As DepositBPencairanHeader
            Get
                Try
                    If Not isnothing(Me._depositBPencairanHeader) AndAlso (Not Me._depositBPencairanHeader.IsLoaded) Then

                        Me._depositBPencairanHeader = CType(DoLoad(GetType(DepositBPencairanHeader).ToString(), _depositBPencairanHeader.ID), DepositBPencairanHeader)
                        Me._depositBPencairanHeader.MarkLoaded()

                    End If

                    Return Me._depositBPencairanHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DepositBPencairanHeader)

                Me._depositBPencairanHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._depositBPencairanHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DepositBDebitNoteID", "{0}"), _
        RelationInfo("DepositBDebitNote", "ID", "DepositBStatusHistory", "DepositBDebitNoteID")> _
        Public Property DepositBDebitNote As DepositBDebitNote
            Get
                Try
                    If Not isnothing(Me._depositBDebitNote) AndAlso (Not Me._depositBDebitNote.IsLoaded) Then

                        Me._depositBDebitNote = CType(DoLoad(GetType(DepositBDebitNote).ToString(), _depositBDebitNote.ID), DepositBDebitNote)
                        Me._depositBDebitNote.MarkLoaded()

                    End If

                    Return Me._depositBDebitNote

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DepositBDebitNote)

                Me._depositBDebitNote = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._depositBDebitNote.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DepositBInterestHID", "{0}"), _
        RelationInfo("DepositBInterestHeader", "ID", "DepositBStatusHistory", "DepositBInterestHID")> _
        Public Property DepositBInterestHeader As DepositBInterestHeader
            Get
                Try
                    If Not isnothing(Me._depositBInterestHeader) AndAlso (Not Me._depositBInterestHeader.IsLoaded) Then

                        Me._depositBInterestHeader = CType(DoLoad(GetType(DepositBInterestHeader).ToString(), _depositBInterestHeader.ID), DepositBInterestHeader)
                        Me._depositBInterestHeader.MarkLoaded()

                    End If

                    Return Me._depositBInterestHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DepositBInterestHeader)

                Me._depositBInterestHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._depositBInterestHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("KewajibanHeaderID", "{0}"), _
        RelationInfo("DepositBKewajibanHeader", "ID", "DepositBStatusHistory", "KewajibanHeaderID")> _
        Public Property DepositBKewajibanHeader As DepositBKewajibanHeader
            Get
                Try
                    If Not isnothing(Me._depositBKewajibanHeader) AndAlso (Not Me._depositBKewajibanHeader.IsLoaded) Then

                        Me._depositBKewajibanHeader = CType(DoLoad(GetType(DepositBKewajibanHeader).ToString(), _depositBKewajibanHeader.ID), DepositBKewajibanHeader)
                        Me._depositBKewajibanHeader.MarkLoaded()

                    End If

                    Return Me._depositBKewajibanHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DepositBKewajibanHeader)

                Me._depositBKewajibanHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._depositBKewajibanHeader.MarkLoaded()
                End If
            End Set
        End Property

        '<ColumnInfo("IndentPartEqHeaderID", "{0}"), _
        'RelationInfo("IndentPartHeader", "ID", "DepositBStatusHistory", "IndentPartEqHeaderID")> _
        'Public Property IndentPartHeader As IndentPartHeader
        '    Get
        '        Try
        '            If Not isnothing(Me._indentPartHeader) AndAlso (Not Me._indentPartHeader.IsLoaded) Then

        '                Me._indentPartHeader = CType(DoLoad(GetType(IndentPartHeader).ToString(), _indentPartHeader.ID), IndentPartHeader)
        '                Me._indentPartHeader.MarkLoaded()

        '            End If

        '            Return Me._indentPartHeader

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        End Try

        '        Return Nothing
        '    End Get

        '    Set(ByVal value As IndentPartHeader)

        '        Me._indentPartHeader = value
        '        If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
        '            Me._indentPartHeader.MarkLoaded()
        '        End If
        '    End Set
        'End Property



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

