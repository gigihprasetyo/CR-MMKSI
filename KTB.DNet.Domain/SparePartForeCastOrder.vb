#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartForeCastOrder Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 8/18/2021 - 3:09:49 PM
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
    <Serializable(), TableInfo("SparePartForeCastOrder")> _
    Public Class SparePartForeCastOrder
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
        'Private _dealerID As Integer
        'Private _sparePartForecastMasterID As Integer
        Private _poNumber As String = String.Empty
        Private _poDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _requestQty As Integer
        Private _requestDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _approvedQty As Integer
        Private _sendDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _noAWB As String = String.Empty
        Private _sONumber As String = String.Empty
        Private _dONumber As String = String.Empty
        Private _billingNumber As String = String.Empty
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedBy As String = String.Empty
        Private _lastUpdatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _dealer As Dealer
        Private _sparePartForecastMaster As SparePartForecastMaster


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


        '<ColumnInfo("DealerID", "{0}")> _
        'Public Property DealerID As Integer
        '    Get
        '        Return _dealerID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _dealerID = value
        '    End Set
        'End Property


        '<ColumnInfo("SparePartForecastMasterID", "{0}")> _
        'Public Property SparePartForecastMasterID As Integer
        '    Get
        '        Return _sparePartForecastMasterID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _sparePartForecastMasterID = value
        '    End Set
        'End Property


        <ColumnInfo("PoNumber", "'{0}'")> _
        Public Property PoNumber As String
            Get
                Return _poNumber
            End Get
            Set(ByVal value As String)
                _poNumber = value
            End Set
        End Property


        <ColumnInfo("PoDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PoDate As DateTime
            Get
                Return _poDate
            End Get
            Set(ByVal value As DateTime)
                _poDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("RequestQty", "{0}")> _
        Public Property RequestQty As Integer
            Get
                Return _requestQty
            End Get
            Set(ByVal value As Integer)
                _requestQty = value
            End Set
        End Property


        <ColumnInfo("RequestDate", "'{0:yyyy/MM/dd}'")> _
        Public Property RequestDate As DateTime
            Get
                Return _requestDate
            End Get
            Set(ByVal value As DateTime)
                _requestDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ApprovedQty", "{0}")> _
        Public Property ApprovedQty As Integer
            Get
                Return _approvedQty
            End Get
            Set(ByVal value As Integer)
                _approvedQty = value
            End Set
        End Property


        <ColumnInfo("SendDate", "'{0:yyyy/MM/dd}'")> _
        Public Property SendDate As DateTime
            Get
                Return _sendDate
            End Get
            Set(ByVal value As DateTime)
                _sendDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("NoAWB", "'{0}'")> _
        Public Property NoAWB As String
            Get
                Return _noAWB
            End Get
            Set(ByVal value As String)
                _noAWB = value
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


        <ColumnInfo("DONumber", "'{0}'")> _
        Public Property DONumber As String
            Get
                Return _dONumber
            End Get
            Set(ByVal value As String)
                _dONumber = value
            End Set
        End Property


        <ColumnInfo("BillingNumber", "'{0}'")> _
        Public Property BillingNumber As String
            Get
                Return _billingNumber
            End Get
            Set(ByVal value As String)
                _billingNumber = value
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
        RelationInfo("Dealer", "ID", "SparePartForeCastOrder", "DealerID")> _
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

        <ColumnInfo("SparePartForecastMasterID", "{0}"), _
        RelationInfo("SparePartForecastMaster", "ID", "SparePartForeCastOrder", "SparePartForecastMasterID")> _
        Public Property SparePartForecastMaster As SparePartForecastMaster
            Get
                Try
                    If Not IsNothing(Me._sparePartForecastMaster) AndAlso (Not Me._sparePartForecastMaster.IsLoaded) Then

                        Me._sparePartForecastMaster = CType(DoLoad(GetType(SparePartForecastMaster).ToString(), _sparePartForecastMaster.ID), SparePartForecastMaster)
                        Me._sparePartForecastMaster.MarkLoaded()

                    End If

                    Return Me._sparePartForecastMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartForecastMaster)

                Me._sparePartForecastMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartForecastMaster.MarkLoaded()
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
