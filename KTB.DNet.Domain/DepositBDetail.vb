
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositBDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 3/14/2016 - 11:11:36 AM
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
    <Serializable(), TableInfo("DepositBDetail")> _
    Public Class DepositBDetail
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
        Private _tipe As String = String.Empty
        Private _statusDebet As Integer
        Private _amount As Decimal
        Private _description As String = String.Empty
        Private _reff As String = String.Empty
        Private _documentNumber As String = String.Empty
        Private _transactionDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _depositBHeader As DepositBHeader



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


        <ColumnInfo("Tipe", "'{0}'")> _
        Public Property Tipe As String
            Get
                Return _tipe
            End Get
            Set(ByVal value As String)
                _tipe = value
            End Set
        End Property


        <ColumnInfo("StatusDebet", "{0}")> _
        Public Property StatusDebet As Integer
            Get
                Return _statusDebet
            End Get
            Set(ByVal value As Integer)
                _statusDebet = value
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


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("Reff", "'{0}'")> _
        Public Property Reff As String
            Get
                Return _reff
            End Get
            Set(ByVal value As String)
                _reff = value
            End Set
        End Property


        <ColumnInfo("DocumentNumber", "'{0}'")> _
        Public Property DocumentNumber As String
            Get
                Return _documentNumber
            End Get
            Set(ByVal value As String)
                _documentNumber = value
            End Set
        End Property


        <ColumnInfo("TransactionDate", "'{0:yyyy/MM/dd}'")> _
        Public Property TransactionDate As DateTime
            Get
                Return _transactionDate
            End Get
            Set(ByVal value As DateTime)
                _transactionDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("DepositBID", "{0}"), _
        RelationInfo("DepositBHeader", "ID", "DepositBDetail", "DepositBID")> _
        Public Property DepositBHeader As DepositBHeader
            Get
                Try
                    If Not isnothing(Me._depositBHeader) AndAlso (Not Me._depositBHeader.IsLoaded) Then

                        Me._depositBHeader = CType(DoLoad(GetType(DepositBHeader).ToString(), _depositBHeader.ID), DepositBHeader)
                        Me._depositBHeader.MarkLoaded()

                    End If

                    Return Me._depositBHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DepositBHeader)

                Me._depositBHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._depositBHeader.MarkLoaded()
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

