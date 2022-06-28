
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : RedemptionDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 4/16/2010 - 9:36:39 AM
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
    <Serializable(), TableInfo("RedemptionDetail")> _
    Public Class RedemptionDetail
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
        Private _requestQty As Integer
        Private _respondQty As Integer
        Private _isManualAlloc As Short
        Private _rowStatus As Short
        Private _isInProcess As Short
        Private _oriRequestQty As Integer
        Private _oriRespondQty As Integer
        Private _sequence As Integer
        Private _ceiling As Decimal
        Private _stock As Integer
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        'Private _redemptionHeaderID As Integer
        'Private _dealerID As Short
        'Private _termOfPaymentID As Integer

        Private _redemptionHeader As RedemptionHeader
        Private _dealer As Dealer
        Private _termOfPayment As TermOfPayment


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


        <ColumnInfo("RequestQty", "{0}")> _
        Public Property RequestQty() As Integer
            Get
                Return _requestQty
            End Get
            Set(ByVal value As Integer)
                _requestQty = value
            End Set
        End Property


        <ColumnInfo("RespondQty", "{0}")> _
        Public Property RespondQty() As Integer
            Get
                Return _respondQty
            End Get
            Set(ByVal value As Integer)
                _respondQty = value
            End Set
        End Property

        <ColumnInfo("IsManualAlloc", "{0}")> _
        Public Property IsManualAlloc() As Short
            Get
                Return _IsManualAlloc
            End Get
            Set(ByVal value As Short)
                _IsManualAlloc = value
            End Set
        End Property

        <ColumnInfo("IsInProcess", "{0}")> _
        Public Property IsInProcess() As Short
            Get
                Return _isInProcess
            End Get
            Set(ByVal value As Short)
                _isInProcess = value
            End Set
        End Property


        <ColumnInfo("OriRequestQty", "{0}")> _
        Public Property OriRequestQty() As Integer
            Get
                Return _oriRequestQty
            End Get
            Set(ByVal value As Integer)
                _oriRequestQty = value
            End Set
        End Property


        <ColumnInfo("OriRespondQty", "{0}")> _
        Public Property OriRespondQty() As Integer
            Get
                Return _oriRespondQty
            End Get
            Set(ByVal value As Integer)
                _oriRespondQty = value
            End Set
        End Property


        <ColumnInfo("Sequence", "{0}")> _
        Public Property Sequence() As Integer
            Get
                Return _sequence
            End Get
            Set(ByVal value As Integer)
                _sequence = value
            End Set
        End Property


        <ColumnInfo("Ceiling", "{0}")> _
        Public Property Ceiling() As Decimal
            Get
                Return _ceiling
            End Get
            Set(ByVal value As Decimal)
                _ceiling = value
            End Set
        End Property


        <ColumnInfo("Stock", "{0}")> _
        Public Property Stock() As Integer
            Get
                Return _stock
            End Get
            Set(ByVal value As Integer)
                _stock = value
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


        '<ColumnInfo("RedemptionHeaderID", "{0}")> _
        'Public Property RedemptionHeaderID() As Integer

        '    Get
        '        Return _redemptionHeaderID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _redemptionHeaderID = value
        '    End Set
        'End Property

        <ColumnInfo("RedemptionHeaderID", "{0}"), _
        RelationInfo("RedemptionHeader", "ID", "RedemptionDetail", "RedemptionHeaderID")> _
        Public Property RedemptionHeader() As RedemptionHeader
            Get
                Try
                    If Not IsNothing(Me._redemptionHeader) AndAlso (Not Me._redemptionHeader.IsLoaded) Then

                        Me._redemptionHeader = CType(DoLoad(GetType(RedemptionHeader).ToString(), _redemptionHeader.ID), RedemptionHeader)
                        Me._redemptionHeader.MarkLoaded()

                    End If

                    Return Me._redemptionHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As RedemptionHeader)

                Me._redemptionHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._redemptionHeader.MarkLoaded()
                End If
            End Set
        End Property

        '<ColumnInfo("DealerID", "{0}")> _
        'Public Property DealerID() As Short

        '    Get
        '        Return _dealerID
        '    End Get
        '    Set(ByVal value As Short)
        '        _dealerID = value
        '    End Set
        'End Property

        <ColumnInfo("DealerID", "{0}"), _
               RelationInfo("Dealer", "ID", "RedemptionDetail", "DealerID")> _
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

        '<ColumnInfo("TermOfPaymentID", "{0}")> _
        'Public Property TermOfPaymentID() As Integer

        '    Get
        '        Return _termOfPaymentID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _termOfPaymentID = value
        '    End Set
        'End Property

        <ColumnInfo("TermOfPaymentID", "{0}"), _
        RelationInfo("TermOfPayment", "ID", "RedemptionDetail", "TermOfPaymentID")> _
        Public Property TermOfPayment() As TermOfPayment
            Get
                Try
                    If Not IsNothing(Me._termOfPayment) AndAlso (Not Me._termOfPayment.IsLoaded) Then

                        Me._termOfPayment = CType(DoLoad(GetType(TermOfPayment).ToString(), _termOfPayment.ID), TermOfPayment)
                        Me._termOfPayment.MarkLoaded()

                    End If

                    Return Me._termOfPayment

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TermOfPayment)

                Me._termOfPayment = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._termOfPayment.MarkLoaded()
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

