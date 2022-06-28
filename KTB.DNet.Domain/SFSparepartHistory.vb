
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SFSparepartHistory Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 03/07/2018 - 2:48:14 PM
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
    <Serializable(), TableInfo("SFSparepartHistory")> _
    Public Class SFSparepartHistory
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
        Private _keyID As String = String.Empty
        Private _isSynchronizeSF As Boolean
        Private _synchronizeDateSF As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _isSynchronizeMMID As Boolean
        Private _synchronizeDateMMID As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _isActive As Boolean
        Private _sFID As String = String.Empty
        Private _rowStatus As Short
        Private _retrySF As Short
        Private _retryMMID As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        'Private _objPMHeader As PMHeader
        Private _objAssistPartSales As AssistPartSales
        'Private _objEndCustomer As EndCustomer
        'Private _objAssistServiceIncoming As AssistServiceIncoming


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

        <ColumnInfo("KeyID", "'{0}'")> _
        Public Property KeyID As String
            Get
                Return _keyID
            End Get
            Set(ByVal value As String)
                _keyID = value
            End Set
        End Property

        '<ColumnInfo("PMHeaderID", "{0}"), _
        '    RelationInfo("PMHeader", "ID", "SFSparepartHistory", "PMHeaderID")> _
        'Public Property PMHeader As PMHeader
        '    Get
        '        Try
        '            If Not IsNothing(Me._objPMHeader) AndAlso (Not Me._objPMHeader.IsLoaded) AndAlso _objPMHeader.ID > 0 Then
        '                Me._objPMHeader = CType(DoLoad(GetType(PMHeader).ToString(), _objPMHeader.ID), PMHeader)
        '                Me._objPMHeader.MarkLoaded()
        '            End If

        '            Return Me._objPMHeader
        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If
        '        End Try

        '        Return Nothing
        '    End Get

        '    Set(ByVal value As PMHeader)
        '        Me._objPMHeader = value
        '        If value.ID = 0 Then
        '            Dim i As Integer = 0
        '            Me._objPMHeader = Nothing
        '        End If
        '        If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
        '            Me._objPMHeader.MarkLoaded()
        '        End If
        '    End Set
        'End Property

        <ColumnInfo("AssistPartSalesID", "{0}"), _
            RelationInfo("AssistPartSales ", "ID", "SFSparepartHistory", "AssistPartSalesID")> _
        Public Property AssistPartSales As AssistPartSales
            Get
                Try
                    If Not IsNothing(Me._objAssistPartSales) AndAlso (Not Me._objAssistPartSales.IsLoaded) Then
                        Me._objAssistPartSales = CType(DoLoad(GetType(AssistPartSales).ToString(), _objAssistPartSales.ID), AssistPartSales)
                        Me._objAssistPartSales.MarkLoaded()
                    End If

                    Return Me._objAssistPartSales
                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                End Try

                Return Nothing
            End Get

            Set(ByVal value As AssistPartSales)

                Me._objAssistPartSales = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._objAssistPartSales.MarkLoaded()
                End If
            End Set
        End Property

        '<ColumnInfo("EndCustomerID", "{0}"), _
        '    RelationInfo("EndCustomer ", "ID", "SFSparepartHistory", "EndCustomerID")> _
        'Public Property EndCustomer As EndCustomer
        '    Get
        '        Try
        '            If Not IsNothing(Me._objEndCustomer) AndAlso (Not Me._objEndCustomer.IsLoaded) Then
        '                Me._objEndCustomer = CType(DoLoad(GetType(EndCustomer).ToString(), _objEndCustomer.ID), EndCustomer)
        '                Me._objEndCustomer.MarkLoaded()
        '            End If

        '            Return Me._objEndCustomer
        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If
        '        End Try

        '        Return Nothing
        '    End Get

        '    Set(ByVal value As EndCustomer)

        '        Me._objEndCustomer = value
        '        If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
        '            Me._objEndCustomer.MarkLoaded()
        '        End If
        '    End Set
        'End Property

        '<ColumnInfo("AssistServiceIncomingID", "{0}"), _
        '    RelationInfo("AssistServiceIncoming ", "ID", "SFSparepartHistory", "AssistServiceIncomingID")> _
        'Public Property AssistServiceIncoming As AssistServiceIncoming
        '    Get
        '        Try
        '            If Not IsNothing(Me._objAssistServiceIncoming) AndAlso (Not Me._objAssistServiceIncoming.IsLoaded) AndAlso _objAssistServiceIncoming.ID > 0 Then
        '                Me._objAssistServiceIncoming = CType(DoLoad(GetType(AssistServiceIncoming).ToString(), _objAssistServiceIncoming.ID), AssistServiceIncoming)
        '                Me._objAssistServiceIncoming.MarkLoaded()
        '            End If

        '            Return Me._objAssistServiceIncoming
        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If
        '        End Try

        '        Return Nothing
        '    End Get

        '    Set(ByVal value As AssistServiceIncoming)
        '        Me._objAssistServiceIncoming = value
        '        If value.ID = 0 Then
        '            Dim i As Integer = 0
        '            Me._objAssistServiceIncoming = Nothing
        '        End If
        '        If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
        '            Me._objAssistServiceIncoming.MarkLoaded()
        '        End If
        '    End Set
        'End Property

        <ColumnInfo("IsSynchronizeSF", "{0}")> _
        Public Property IsSynchronizeSF As Boolean
            Get
                Return _isSynchronizeSF
            End Get
            Set(ByVal value As Boolean)
                _isSynchronizeSF = value
            End Set
        End Property


        <ColumnInfo("SynchronizeDateSF", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property SynchronizeDateSF As DateTime
            Get
                Return _synchronizeDateSF
            End Get
            Set(ByVal value As DateTime)
                _synchronizeDateSF = value
            End Set
        End Property

        <ColumnInfo("IsSynchronizeMMID", "{0}")> _
        Public Property IsSynchronizeMMID As Boolean
            Get
                Return _isSynchronizeMMID
            End Get
            Set(ByVal value As Boolean)
                _isSynchronizeMMID = value
            End Set
        End Property


        <ColumnInfo("SynchronizeDateMMID", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property SynchronizeDateMMID As DateTime
            Get
                Return _synchronizeDateMMID
            End Get
            Set(ByVal value As DateTime)
                _synchronizeDateMMID = value
            End Set
        End Property


        <ColumnInfo("IsActive", "{0}")> _
        Public Property IsActive As Boolean
            Get
                Return _isActive
            End Get
            Set(ByVal value As Boolean)
                _isActive = value
            End Set
        End Property


        <ColumnInfo("SFID", "'{0}'")> _
        Public Property SFID As String
            Get
                Return _sFID
            End Get
            Set(ByVal value As String)
                _sFID = value
            End Set
        End Property

        <ColumnInfo("RetrySF", "{0}")> _
        Public Property RetrySF As Short
            Get
                Return _retrySF
            End Get
            Set(ByVal value As Short)
                _retrySF = value
            End Set
        End Property

        <ColumnInfo("RetryMMID", "{0}")> _
        Public Property RetryMMID As Short
            Get
                Return _retryMMID
            End Get
            Set(ByVal value As Short)
                _retryMMID = value
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

