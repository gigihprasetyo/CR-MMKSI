#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TOPSPAccountingNo Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2020 - 8:55:06 AM
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
    <Serializable(), TableInfo("TOPSPAccountingNo")> _
    Public Class TOPSPAccountingNo
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
        Private _kliringDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _kliringAmount As Decimal
        Private _tRNo As String = String.Empty
        Private _rowStatus As Short
        Private _createdby As String = String.Empty
        Private _createdtime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedby As String = String.Empty
        Private _lastUpdatedtime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _tOPSPTransferPayment As TOPSPTransferPayment



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


        <ColumnInfo("KliringDate", "'{0:yyyy/MM/dd}'")> _
        Public Property KliringDate As DateTime
            Get
                Return _kliringDate
            End Get
            Set(ByVal value As DateTime)
                _kliringDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("KliringAmount", "{0}")> _
        Public Property KliringAmount As Decimal
            Get
                Return _kliringAmount
            End Get
            Set(ByVal value As Decimal)
                _kliringAmount = value
            End Set
        End Property


        <ColumnInfo("TRNo", "'{0}'")> _
        Public Property TRNo As String
            Get
                Return _tRNo
            End Get
            Set(ByVal value As String)
                _tRNo = value
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


        <ColumnInfo("Createdby", "'{0}'")> _
        Public Property Createdby As String
            Get
                Return _createdby
            End Get
            Set(ByVal value As String)
                _createdby = value
            End Set
        End Property


        <ColumnInfo("Createdtime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property Createdtime As DateTime
            Get
                Return _createdtime
            End Get
            Set(ByVal value As DateTime)
                _createdtime = value
            End Set
        End Property


        <ColumnInfo("LastUpdatedby", "'{0}'")> _
        Public Property LastUpdatedby As String
            Get
                Return _lastUpdatedby
            End Get
            Set(ByVal value As String)
                _lastUpdatedby = value
            End Set
        End Property


        <ColumnInfo("LastUpdatedtime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdatedtime As DateTime
            Get
                Return _lastUpdatedtime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdatedtime = value
            End Set
        End Property


        <ColumnInfo("TOPSPTransferPaymentID", "{0}"), _
        RelationInfo("TOPSPTransferPayment", "ID", "TOPSPAccountingNo", "TOPSPTransferPaymentID")> _
        Public Property TOPSPTransferPayment As TOPSPTransferPayment
            Get
                Try
                    If Not IsNothing(Me._tOPSPTransferPayment) AndAlso (Not Me._tOPSPTransferPayment.IsLoaded) Then

                        Me._tOPSPTransferPayment = CType(DoLoad(GetType(TOPSPTransferPayment).ToString(), _tOPSPTransferPayment.ID), TOPSPTransferPayment)
                        Me._tOPSPTransferPayment.MarkLoaded()

                    End If

                    Return Me._tOPSPTransferPayment

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TOPSPTransferPayment)

                Me._tOPSPTransferPayment = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._tOPSPTransferPayment.MarkLoaded()
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
