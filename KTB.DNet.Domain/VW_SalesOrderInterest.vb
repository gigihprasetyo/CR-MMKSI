#Region "Summary"
'// ===========================================================================
'// AUTHOR        : dnet
'// PURPOSE       : SalesOrderInterest Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 8/9/2021 - 10:41:29 AM
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
    <Serializable(), TableInfo("VW_SalesOrderInterest")> _
    Public Class VW_SalesOrderInterest
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
        Private _sONumber As String = String.Empty
        'Private _salesOrderID As Integer
        Private _billingNumber As String = String.Empty
        Private _billingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _docReference As String = String.Empty
        'Private _dealerID As Integer
        Private _dPPAmount As Decimal
        Private _pPHAmount As Decimal
        Private _additionalAmount As Decimal
        Private _trType As String = String.Empty
        Private _status As Short
        Private _rowStatus As Short
        Private _DocNumber As String
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _salesOrder As SalesOrder
        Private _dealer As Dealer
        Private _interestPPHHeader As InterestPPHHeader

        Private _noReg As String
        Private _submissionStatus As Short
        Private _pONumber As String = String.Empty


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


        <ColumnInfo("SONumber", "'{0}'")> _
        Public Property SONumber As String
            Get
                Return _sONumber
            End Get
            Set(ByVal value As String)
                _sONumber = value
            End Set
        End Property


        '<ColumnInfo("SalesOrderID", "{0}")> _
        'Public Property SalesOrderID As Integer
        '    Get
        '        Return _salesOrderID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _salesOrderID = value
        '    End Set
        'End Property


        <ColumnInfo("BillingNumber", "'{0}'")> _
        Public Property BillingNumber As String
            Get
                Return _billingNumber
            End Get
            Set(ByVal value As String)
                _billingNumber = value
            End Set
        End Property


        <ColumnInfo("BillingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property BillingDate As DateTime
            Get
                Return _billingDate
            End Get
            Set(ByVal value As DateTime)
                _billingDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("DocReference", "'{0}'")> _
        Public Property DocReference As String
            Get
                Return _docReference
            End Get
            Set(ByVal value As String)
                _docReference = value
            End Set
        End Property


        '<ColumnInfo("DealerID", "{0}")> _
        'Public Property DealerID As Integer
        '    Get
        '        Return _dealerID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _dealerID = value
        '    End Set
        'End Property


        <ColumnInfo("DPPAmount", "{0}")> _
        Public Property DPPAmount As Decimal
            Get
                Return _dPPAmount
            End Get
            Set(ByVal value As Decimal)
                _dPPAmount = value
            End Set
        End Property


        <ColumnInfo("PPHAmount", "{0}")> _
        Public Property PPHAmount As Decimal
            Get
                Return _pPHAmount
            End Get
            Set(ByVal value As Decimal)
                _pPHAmount = value
            End Set
        End Property


        <ColumnInfo("AdditionalAmount", "{0}")> _
        Public Property AdditionalAmount As Decimal
            Get
                Return _additionalAmount
            End Get
            Set(ByVal value As Decimal)
                _additionalAmount = value
            End Set
        End Property


        <ColumnInfo("TrType", "'{0}'")> _
        Public Property TrType As String
            Get
                Return _trType
            End Get
            Set(ByVal value As String)
                _trType = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
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
       RelationInfo("Dealer", "ID", "VW_SalesOrderInterest", "DealerID")> _
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


        <ColumnInfo("SalesOrderID", "{0}"), _
       RelationInfo("SalesOrder", "ID", "VW_SalesOrderInterest", "SalesOrderID")> _
        Public Property SalesOrder() As SalesOrder
            Get
                Try
                    If Not IsNothing(Me._salesOrder) AndAlso (Not Me._salesOrder.IsLoaded) Then

                        Me._salesOrder = CType(DoLoad(GetType(SalesOrder).ToString(), _salesOrder.ID), SalesOrder)
                        Me._salesOrder.MarkLoaded()

                    End If

                    Return Me._salesOrder

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesOrder)

                Me._salesOrder = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesOrder.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("InterestPPHHeaderID", "{0}"), _
       RelationInfo("InterestPPHHeader", "ID", "VW_SalesOrderInterest", "InterestPPHHeaderID")> _
        Public Property InterestPPHHeader() As InterestPPHHeader
            Get
                Try
                    If Not IsNothing(Me._interestPPHHeader) AndAlso (Not Me._interestPPHHeader.IsLoaded) Then

                        Me._interestPPHHeader = CType(DoLoad(GetType(InterestPPHHeader).ToString(), _interestPPHHeader.ID), InterestPPHHeader)
                        Me._interestPPHHeader.MarkLoaded()

                    End If

                    Return Me._interestPPHHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As InterestPPHHeader)

                Me._interestPPHHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._interestPPHHeader.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("NoReg", "'{0}'")> _
        Public Property NoReg As String
            Get
                Return _noReg
            End Get
            Set(ByVal value As String)
                _noReg = value
            End Set
        End Property


        <ColumnInfo("SubmissionStatus", "'{0}'")> _
        Public Property SubmissionStatus As String
            Get
                Return _submissionStatus
            End Get
            Set(ByVal value As String)
                _submissionStatus = value
            End Set
        End Property


        <ColumnInfo("PONumber", "'{0}'")> _
        Public Property PONumber As String
            Get
                Return _pONumber
            End Get
            Set(ByVal value As String)
                _pONumber = value
            End Set
        End Property


        <ColumnInfo("DocNumber", "'{0}'")> _
        Public Property DocNumber As String
            Get
                Return _DocNumber
            End Get
            Set(ByVal value As String)
                _DocNumber = value
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
