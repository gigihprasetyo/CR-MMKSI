
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_ServiceHistorys Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 07/08/2018 - 9:55:07
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
    <Serializable(), TableInfo("VWI_ServiceHistory")> _
    Public Class VWI_ServiceHistory
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Long)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Long
        Private _chassisMasterID As Integer
        Private _kodeChassis As String = String.Empty
        Private _pKTDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _tglBukaTransaksi As Date
        Private _tglTutupTransaksi As Date
        Private _waktuMasuk As TimeSpan
        Private _waktuKeluar As TimeSpan
        Private _dealerCode As String = String.Empty
        Private _dealerBranchCode As String = String.Empty
        Private _workOrderType As String = String.Empty
        Private _workOrderCategoryCode As String = String.Empty
        Private _kMService As Integer
        Private _noWorkOrder As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _servicePlaceCode As String = String.Empty
        Private _serviceTypeCode As String = String.Empty

#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Long
            Get
                Return _iD
            End Get
            Set(ByVal value As Long)
                _iD = value
            End Set
        End Property


        <ColumnInfo("ChassisMasterID", "{0}")> _
        Public Property ChassisMasterID As Integer
            Get
                Return _chassisMasterID
            End Get
            Set(ByVal value As Integer)
                _chassisMasterID = value
            End Set
        End Property


        <ColumnInfo("KodeChassis", "'{0}'")> _
        Public Property KodeChassis As String
            Get
                Return _kodeChassis
            End Get
            Set(ByVal value As String)
                _kodeChassis = value
            End Set
        End Property


        <ColumnInfo("PKTDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PKTDate As DateTime
            Get
                Return _pKTDate
            End Get
            Set(ByVal value As DateTime)
                _pKTDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("TglBukaTransaksi", "{0}")> _
        Public Property TglBukaTransaksi As Date
            Get
                Return _tglBukaTransaksi
            End Get
            Set(ByVal value As Date)
                _tglBukaTransaksi = value
            End Set
        End Property


        <ColumnInfo("TglTutupTransaksi", "{0}")> _
        Public Property TglTutupTransaksi As Date
            Get
                Return _tglTutupTransaksi
            End Get
            Set(ByVal value As Date)
                _tglTutupTransaksi = value
            End Set
        End Property


        <ColumnInfo("WaktuMasuk", "{0}")> _
        Public Property WaktuMasuk As TimeSpan
            Get
                Return _waktuMasuk
            End Get
            Set(ByVal value As TimeSpan)
                _waktuMasuk = value
            End Set
        End Property


        <ColumnInfo("WaktuKeluar", "{0}")> _
        Public Property WaktuKeluar As TimeSpan
            Get
                Return _waktuKeluar
            End Get
            Set(ByVal value As TimeSpan)
                _waktuKeluar = value
            End Set
        End Property


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("DealerBranchCode", "'{0}'")> _
        Public Property DealerBranchCode As String
            Get
                Return _dealerBranchCode
            End Get
            Set(ByVal value As String)
                _dealerBranchCode = value
            End Set
        End Property

        <ColumnInfo("WorkOrderType", "'{0}'")> _
        Public Property WorkOrderType As String
            Get
                Return _workOrderType
            End Get
            Set(ByVal value As String)
                _workOrderType = value
            End Set
        End Property

        <ColumnInfo("WorkOrderCategoryCode", "'{0}'")> _
        Public Property WorkOrderCategoryCode As String
            Get
                Return _workOrderCategoryCode
            End Get
            Set(ByVal value As String)
                _workOrderCategoryCode = value
            End Set
        End Property


        <ColumnInfo("KMService", "{0}")> _
        Public Property KMService As Integer
            Get
                Return _kMService
            End Get
            Set(ByVal value As Integer)
                _kMService = value
            End Set
        End Property

        <ColumnInfo("NoWorkOrder", "'{0}'")> _
        Public Property NoWorkOrder As String
            Get
                Return _noWorkOrder
            End Get
            Set(ByVal value As String)
                _noWorkOrder = value
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

        <ColumnInfo("ServicePlaceCode", "'{0}'")> _
        Public Property ServicePlaceCode As String
            Get
                Return _servicePlaceCode
            End Get
            Set(ByVal value As String)
                _servicePlaceCode = value
            End Set
        End Property

        <ColumnInfo("ServiceTypeCode", "'{0}'")> _
        Public Property ServiceTypeCode As String
            Get
                Return _serviceTypeCode
            End Get
            Set(ByVal value As String)
                _serviceTypeCode = value
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

