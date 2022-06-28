
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : StockActual Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 12/14/2015 - 10:43:24 AM
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
    <Serializable(), TableInfo("StockActual")> _
    Public Class StockActual
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
        Private _month As Short
        Private _year As Short
        Private _stock As Integer
        Private _ratioEOM As Decimal
        Private _ratioSPM As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _vechileModel As VechileModel
        Private _stockTarget As StockTarget



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


        <ColumnInfo("Month", "{0}")> _
        Public Property Month As Short
            Get
                Return _month
            End Get
            Set(ByVal value As Short)
                _month = value
            End Set
        End Property


        <ColumnInfo("Year", "{0}")> _
        Public Property Year As Short
            Get
                Return _year
            End Get
            Set(ByVal value As Short)
                _year = value
            End Set
        End Property


        <ColumnInfo("Stock", "{0}")> _
        Public Property Stock As Integer
            Get
                Return _stock
            End Get
            Set(ByVal value As Integer)
                _stock = value
            End Set
        End Property


        <ColumnInfo("RatioEOM", "#,##0")> _
        Public Property RatioEOM As Decimal
            Get
                Return _ratioEOM
            End Get
            Set(ByVal value As Decimal)
                _ratioEOM = value
            End Set
        End Property


        <ColumnInfo("RatioSPM", "#,##0")> _
        Public Property RatioSPM As Decimal
            Get
                Return _ratioSPM
            End Get
            Set(ByVal value As Decimal)
                _ratioSPM = value
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
        RelationInfo("Dealer", "ID", "StockActual", "DealerID")> _
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

        <ColumnInfo("ModelID", "{0}"), _
        RelationInfo("VechileModel", "ID", "StockActual", "ModelID")> _
        Public Property VechileModel As VechileModel
            Get
                Try
                    If Not IsNothing(Me._vechileModel) AndAlso (Not Me._vechileModel.IsLoaded) Then

                        Me._vechileModel = CType(DoLoad(GetType(VechileModel).ToString(), _vechileModel.ID), VechileModel)
                        Me._vechileModel.MarkLoaded()

                    End If

                    Return Me._vechileModel

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileModel)

                Me._vechileModel = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileModel.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("StockTargetID", "{0}"), _
        RelationInfo("StockTarget", "ID", "StockActual", "StockTargetID")> _
        Public Property StockTarget As StockTarget
            Get
                Try
                    If Not IsNothing(Me._stockTarget) AndAlso (Not Me._stockTarget.IsLoaded) Then

                        Me._stockTarget = CType(DoLoad(GetType(StockTarget).ToString(), _stockTarget.ID), StockTarget)
                        Me._stockTarget.MarkLoaded()

                    End If

                    Return Me._stockTarget

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As StockTarget)

                Me._stockTarget = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._stockTarget.MarkLoaded()
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


        Dim _totalUnitPK As Integer
        Dim _currentRatio As Decimal

        Public Property TotalUnitPK() As Integer
            Get
                Return _totalUnitPK

            End Get
            Set(value As Integer)
                _totalUnitPK = value
            End Set
        End Property
        Public Property CurrentRatio() As Decimal
            Get
                Return _currentRatio

            End Get
            Set(value As Decimal)
                _currentRatio = value
            End Set
        End Property
        Public ReadOnly Property GetCurrentOrderLeft() As Integer
            Get
                Dim ret As Integer = 0
                Try
                    ret = CType(Math.Ceiling((_stockTarget.TargetRatio - _currentRatio) * _stockTarget.Target), Integer)
                    If ret < 0 Then
                        ret = 0
                    End If
                Catch ex As Exception

                End Try
                Return ret

            End Get
        End Property
        Public ReadOnly Property GetPeriodePK() As String
            Get
                Dim ret As String = ""
                Try
                    Dim periodedate As Date = New Date(Me.Year, Me.Month, 1).AddMonths(0)
                    ret = MonthName(periodedate.Month) & " " & periodedate.Year.ToString
                    
                Catch ex As Exception

                End Try
                Return ret

            End Get
        End Property
#End Region

    End Class
End Namespace

