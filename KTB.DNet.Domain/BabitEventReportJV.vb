
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitEventReportJV Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 03/10/2019 - 13:52:53
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
    <Serializable(), TableInfo("BabitEventReportJV")> _
    Public Class BabitEventReportJV
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
        Private _regNumber As String = String.Empty
        Private _textReceiptNo As String = String.Empty
        Private _textRefNo As String = String.Empty
        Private _noJV As String = String.Empty
        Private _isTransfer As Byte
        Private _tglProses As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _tglPencairan As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        'Private _dealerID As Short
        Private _dealer As Dealer



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


        <ColumnInfo("RegNumber", "'{0}'")> _
        Public Property RegNumber As String
            Get
                Return _regNumber
            End Get
            Set(ByVal value As String)
                _regNumber = value
            End Set
        End Property


        <ColumnInfo("TextReceiptNo", "'{0}'")> _
        Public Property TextReceiptNo As String
            Get
                Return _textReceiptNo
            End Get
            Set(ByVal value As String)
                _textReceiptNo = value
            End Set
        End Property


        <ColumnInfo("TextRefNo", "'{0}'")> _
        Public Property TextRefNo As String
            Get
                Return _textRefNo
            End Get
            Set(ByVal value As String)
                _textRefNo = value
            End Set
        End Property


        <ColumnInfo("NoJV", "'{0}'")> _
        Public Property NoJV As String
            Get
                Return _noJV
            End Get
            Set(ByVal value As String)
                _noJV = value
            End Set
        End Property


        <ColumnInfo("IsTransfer", "{0}")> _
        Public Property IsTransfer As Byte
            Get
                Return _isTransfer
            End Get
            Set(ByVal value As Byte)
                _isTransfer = value
            End Set
        End Property


        <ColumnInfo("TglProses", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property TglProses As DateTime
            Get
                Return _tglProses
            End Get
            Set(ByVal value As DateTime)
                _tglProses = value
            End Set
        End Property


        <ColumnInfo("TglPencairan", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property TglPencairan As DateTime
            Get
                Return _tglPencairan
            End Get
            Set(ByVal value As DateTime)
                _tglPencairan = value
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


        '    <ColumnInfo("DealerID", "{0}")> _
        '    Public Property DealerID As Short

        '        Get
        'return _dealerID}
        '        End Get
        '        Set(ByVal value As Short)
        '            _dealerID = value
        '        End Set
        '    End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "BabitEventReportJV", "DealerID")> _
        Public Property Dealer As Dealer
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

