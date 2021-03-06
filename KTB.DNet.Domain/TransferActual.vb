
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TransferActual Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 02/12/2018 - 2:59:46 PM
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
    <Serializable(), TableInfo("TransferActual")> _
    Public Class TransferActual
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
        'Private _transferPaymentID As Integer
        Private _refTransferbank As String = String.Empty
        Private _amount As Decimal
        Private _postingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedBy As String = String.Empty
        Private _lastUpdatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _transferPayment As TransferPayment


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


        '<ColumnInfo("TransferPaymentID", "{0}")> _
        'Public Property TransferPaymentID As Integer
        '    Get
        '        Return _transferPaymentID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _transferPaymentID = value
        '    End Set
        'End Property

        <ColumnInfo("TransferPaymentID", "{0}"), _
     RelationInfo("TransferPayment", "ID", "TransferActual", "TransferPaymentID")> _
        Public Property TransferPayment As TransferPayment
            Get
                Try
                    If Not IsNothing(Me._transferPayment) AndAlso (Not Me._transferPayment.IsLoaded) Then

                        Me._transferPayment = CType(DoLoad(GetType(TransferPayment).ToString(), _transferPayment.ID), TransferPayment)
                        Me._transferPayment.MarkLoaded()

                    End If

                    Return Me._transferPayment

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TransferPayment)

                Me._transferPayment = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._transferPayment.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("RefTransferbank", "'{0}'")> _
        Public Property RefTransferbank As String
            Get
                Return _refTransferbank
            End Get
            Set(ByVal value As String)
                _refTransferbank = value
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


        <ColumnInfo("PostingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PostingDate As DateTime
            Get
                Return _postingDate
            End Get
            Set(ByVal value As DateTime)
                _postingDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("LastUpdatedBy", "'{0}'")> _
        Public Property LastUpdatedBy As String
            Get
                Return _lastUpdatedBy
            End Get
            Set(ByVal value As String)
                _lastUpdatedBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdatedTime As DateTime
            Get
                Return _lastUpdatedTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdatedTime = value
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

