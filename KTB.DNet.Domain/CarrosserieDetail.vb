
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : CarrosserieDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 21/03/2018 - 10:52:40
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
    <Serializable(), TableInfo("CarrosserieDetail")> _
    Public Class CarrosserieDetail
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
        Private _pDIStateCode As Short
        Private _pDIStatusCode As Short
        Private _accessorriesDescription As String = String.Empty
        Private _accessorriesName As String = String.Empty
        Private _bUCode As String = String.Empty
        Private _bUName As String = String.Empty
        Private _kITName As String = String.Empty
        Private _pBUCode As String = String.Empty
        Private _pBUName As String = String.Empty
        Private _pDIDetailName As String = String.Empty
        Private _pDIReceiptDetailNo As String = String.Empty
        Private _pDIReceiptName As String = String.Empty
        Private _receiveQuantity As Double
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _carrosserieHeader As CarrosserieHeader



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


        <ColumnInfo("PDIStateCode", "{0}")> _
        Public Property PDIStateCode As Short
            Get
                Return _pDIStateCode
            End Get
            Set(ByVal value As Short)
                _pDIStateCode = value
            End Set
        End Property


        <ColumnInfo("PDIStatusCode", "{0}")> _
        Public Property PDIStatusCode As Short
            Get
                Return _pDIStatusCode
            End Get
            Set(ByVal value As Short)
                _pDIStatusCode = value
            End Set
        End Property


        <ColumnInfo("AccessorriesDescription", "'{0}'")> _
        Public Property AccessorriesDescription As String
            Get
                Return _accessorriesDescription
            End Get
            Set(ByVal value As String)
                _accessorriesDescription = value
            End Set
        End Property


        <ColumnInfo("AccessorriesName", "'{0}'")> _
        Public Property AccessorriesName As String
            Get
                Return _accessorriesName
            End Get
            Set(ByVal value As String)
                _accessorriesName = value
            End Set
        End Property


        <ColumnInfo("BUCode", "'{0}'")> _
        Public Property BUCode As String
            Get
                Return _bUCode
            End Get
            Set(ByVal value As String)
                _bUCode = value
            End Set
        End Property


        <ColumnInfo("BUName", "'{0}'")> _
        Public Property BUName As String
            Get
                Return _bUName
            End Get
            Set(ByVal value As String)
                _bUName = value
            End Set
        End Property


        <ColumnInfo("KITName", "'{0}'")> _
        Public Property KITName As String
            Get
                Return _kITName
            End Get
            Set(ByVal value As String)
                _kITName = value
            End Set
        End Property


        <ColumnInfo("PBUCode", "'{0}'")> _
        Public Property PBUCode As String
            Get
                Return _pBUCode
            End Get
            Set(ByVal value As String)
                _pBUCode = value
            End Set
        End Property


        <ColumnInfo("PBUName", "'{0}'")> _
        Public Property PBUName As String
            Get
                Return _pBUName
            End Get
            Set(ByVal value As String)
                _pBUName = value
            End Set
        End Property


        <ColumnInfo("PDIDetailName", "'{0}'")> _
        Public Property PDIDetailName As String
            Get
                Return _pDIDetailName
            End Get
            Set(ByVal value As String)
                _pDIDetailName = value
            End Set
        End Property


        <ColumnInfo("PDIReceiptDetailNo", "'{0}'")> _
        Public Property PDIReceiptDetailNo As String
            Get
                Return _pDIReceiptDetailNo
            End Get
            Set(ByVal value As String)
                _pDIReceiptDetailNo = value
            End Set
        End Property


        <ColumnInfo("PDIReceiptName", "'{0}'")> _
        Public Property PDIReceiptName As String
            Get
                Return _pDIReceiptName
            End Get
            Set(ByVal value As String)
                _pDIReceiptName = value
            End Set
        End Property


        <ColumnInfo("ReceiveQuantity", "#,##0")> _
        Public Property ReceiveQuantity As Double
            Get
                Return _receiveQuantity
            End Get
            Set(ByVal value As Double)
                _receiveQuantity = value
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


        <ColumnInfo("CarrosserieHeaderID", "{0}"), _
        RelationInfo("CarrosserieHeader", "ID", "CarrosserieDetail", "CarrosserieHeaderID")> _
        Public Property CarrosserieHeader As CarrosserieHeader
            Get
                Try
                    If Not isnothing(Me._carrosserieHeader) AndAlso (Not Me._carrosserieHeader.IsLoaded) Then

                        Me._carrosserieHeader = CType(DoLoad(GetType(CarrosserieHeader).ToString(), _carrosserieHeader.ID), CarrosserieHeader)
                        Me._carrosserieHeader.MarkLoaded()

                    End If

                    Return Me._carrosserieHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CarrosserieHeader)

                Me._carrosserieHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._carrosserieHeader.MarkLoaded()
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

