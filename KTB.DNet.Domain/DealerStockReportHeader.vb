#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DealerStockReportHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/31/2007 - 01:16:14 PM
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
    <Serializable(), TableInfo("DealerStockReportHeader")> _
    Public Class DealerStockReportHeader
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
        Private _captureDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _periodMonth As Short
        Private _periodYear As String = String.Empty
        Private _captureBy As String = String.Empty
        Private _status As Short
        Private _salesVolume As Integer
        Private _carryOver As Integer
        Private _carryOver_Min1 As Integer
        Private _newOrder As Integer
        Private _beginingStock As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _vechileType As VechileType



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


        <ColumnInfo("CaptureDate", "'{0:yyyy/MM/dd}'")> _
        Public Property CaptureDate() As DateTime
            Get
                Return _captureDate
            End Get
            Set(ByVal value As DateTime)
                _captureDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PeriodMonth", "{0}")> _
        Public Property PeriodMonth() As Short
            Get
                Return _periodMonth
            End Get
            Set(ByVal value As Short)
                _periodMonth = value
            End Set
        End Property


        <ColumnInfo("PeriodYear", "'{0}'")> _
        Public Property PeriodYear() As String
            Get
                Return _periodYear
            End Get
            Set(ByVal value As String)
                _periodYear = value
            End Set
        End Property


        <ColumnInfo("CaptureBy", "'{0}'")> _
        Public Property CaptureBy() As String
            Get
                Return _captureBy
            End Get
            Set(ByVal value As String)
                _captureBy = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
            End Set
        End Property


        <ColumnInfo("SalesVolume", "{0}")> _
        Public Property SalesVolume() As Integer
            Get
                Return _salesVolume
            End Get
            Set(ByVal value As Integer)
                _salesVolume = value
            End Set
        End Property


        <ColumnInfo("CarryOver", "{0}")> _
        Public Property CarryOver() As Integer
            Get
                Return _carryOver
            End Get
            Set(ByVal value As Integer)
                _carryOver = value
            End Set
        End Property


        <ColumnInfo("CarryOver_Min1", "{0}")> _
        Public Property CarryOver_Min1() As Integer
            Get
                Return _carryOver_Min1
            End Get
            Set(ByVal value As Integer)
                _carryOver_Min1 = value
            End Set
        End Property


        <ColumnInfo("NewOrder", "{0}")> _
        Public Property NewOrder() As Integer
            Get
                Return _newOrder
            End Get
            Set(ByVal value As Integer)
                _newOrder = value
            End Set
        End Property


        <ColumnInfo("BeginingStock", "{0}")> _
        Public Property BeginingStock() As Integer
            Get
                Return _beginingStock
            End Get
            Set(ByVal value As Integer)
                _beginingStock = value
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


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "DealerStockReportHeader", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not isnothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("VechileTypeID", "{0}"), _
        RelationInfo("VechileType", "ID", "DealerStockReportHeader", "VechileTypeID")> _
        Public Property VechileType() As VechileType
            Get
                Try
                    If Not isnothing(Me._vechileType) AndAlso (Not Me._vechileType.IsLoaded) Then

                        Me._vechileType = CType(DoLoad(GetType(VechileType).ToString(), _vechileType.ID), VechileType)
                        Me._vechileType.MarkLoaded()

                    End If

                    Return Me._vechileType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileType)

                Me._vechileType = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileType.MarkLoaded()
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

