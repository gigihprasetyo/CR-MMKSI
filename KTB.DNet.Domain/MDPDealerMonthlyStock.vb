
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MDPDealerMonthlyStock Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 21/11/2018 - 10:47:21
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
    <Serializable(), TableInfo("MDPDealerMonthlyStock")> _
    Public Class MDPDealerMonthlyStock
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
        Private _dealerID As Short
        Private _periodMonth As Short
        Private _periodYear As Short
        Private _productionYear As Short
        Private _periodStartDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _periodEndDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _carryOverStock As Integer
        Private _planStock As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedBy As String = String.Empty
        Private _lastUpdatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _remainingStock As Integer
        Private _dealer As Dealer
        Private _vechileColor As VechileColor


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

        <ColumnInfo("PeriodMonth", "{0}")> _
        Public Property PeriodMonth As Short
            Get
                Return _periodMonth
            End Get
            Set(ByVal value As Short)
                _periodMonth = value
            End Set
        End Property


        <ColumnInfo("PeriodYear", "{0}")> _
        Public Property PeriodYear As Short
            Get
                Return _periodYear
            End Get
            Set(ByVal value As Short)
                _periodYear = value
            End Set
        End Property


        <ColumnInfo("ProductionYear", "{0}")> _
        Public Property ProductionYear As Short
            Get
                Return _productionYear
            End Get
            Set(ByVal value As Short)
                _productionYear = value
            End Set
        End Property


        <ColumnInfo("PeriodStartDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PeriodStartDate As DateTime
            Get
                Return _periodStartDate
            End Get
            Set(ByVal value As DateTime)
                _periodStartDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PeriodEndDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PeriodEndDate As DateTime
            Get
                Return _periodEndDate
            End Get
            Set(ByVal value As DateTime)
                _periodEndDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("CarryOverStock", "{0}")> _
        Public Property CarryOverStock As Integer
            Get
                Return _carryOverStock
            End Get
            Set(ByVal value As Integer)
                _carryOverStock = value
            End Set
        End Property


        <ColumnInfo("PlanStock", "{0}")> _
        Public Property PlanStock As Integer
            Get
                Return _planStock
            End Get
            Set(ByVal value As Integer)
                _planStock = value
            End Set
        End Property

        Public Property RemainingStock As Integer
            Get
                Return _remainingStock
            End Get
            Set(ByVal value As Integer)
                _remainingStock = value
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

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "MDPDealerMonthlyStock", "DealerID")> _
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

        <ColumnInfo("VehicleColorID", "{0}"), _
        RelationInfo("VechileColor", "ID", "MDPDealerMonthlyStock", "VehicleColorID")> _
        Public Property VechileColor() As VechileColor
            Get
                Try
                    If Not IsNothing(Me._vechileColor) AndAlso (Not Me._vechileColor.IsLoaded) Then

                        Me._vechileColor = CType(DoLoad(GetType(VechileColor).ToString(), _vechileColor.ID), VechileColor)
                        Me._vechileColor.MarkLoaded()

                    End If

                    Return Me._vechileColor

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileColor)

                Me._vechileColor = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileColor.MarkLoaded()
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

