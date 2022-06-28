#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_CampaignReport Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 07/03/2018 - 13:17:15
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
    <Serializable(), TableInfo("VWI_CampaignReport")> _
    Public Class VWI_CampaignReport
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
        Private _headerID As Integer
        Private _nomorSurat As String = String.Empty
        Private _status As Short
        Private _benefitRegNo As String = String.Empty
        Private _remarks As String = String.Empty
        Private _rowStatus As Short
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        'Private _detailRowStatus As Short
        Private _dealerID As Short
        Private _dealerCode As String = String.Empty
        Private _fakturValidationStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _fakturValidationEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _fakturOpenStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _fakturOpenEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _vehicleTypeID As Short
        Private _vehicleTypeCode As String = String.Empty
        Private _vehicleTypeDesc As String = String.Empty        




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

        <ColumnInfo("HeaderID", "'{0}'")> _
        Public Property HeaderID As Integer
            Get
                Return _headerID
            End Get
            Set(ByVal value As Integer)
                _headerID = value
            End Set
        End Property

        <ColumnInfo("NomorSurat", "'{0}'")> _
        Public Property NomorSurat As String
            Get
                Return _nomorSurat
            End Get
            Set(ByVal value As String)
                _nomorSurat = value
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


        <ColumnInfo("BenefitRegNo", "{0}")> _
        Public Property BenefitRegNo As String
            Get
                Return _benefitRegNo
            End Get
            Set(ByVal value As String)
                _benefitRegNo = value
            End Set
        End Property


        <ColumnInfo("Remarks", "'{0}'")> _
        Public Property Remarks As String
            Get
                Return _remarks
            End Get
            Set(ByVal value As String)
                _remarks = value
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


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        '<ColumnInfo("DetailRowStatus", "{0}")> _
        'Public Property DetailRowStatus As Short
        '    Get
        '        Return _detailRowStatus
        '    End Get
        '    Set(ByVal value As Short)
        '        _detailRowStatus = value
        '    End Set
        'End Property


        <ColumnInfo("DealerID", "{0}")> _
        Public Property DealerID As Short
            Get
                Return _dealerID
            End Get
            Set(ByVal value As Short)
                _dealerID = value
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


        <ColumnInfo("FakturValidationStart", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property FakturValidationStart As DateTime
            Get
                Return _fakturValidationStart
            End Get
            Set(ByVal value As DateTime)
                _fakturValidationStart = value
            End Set
        End Property


        <ColumnInfo("FakturValidationEnd", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property FakturValidationEnd As DateTime
            Get
                Return _fakturValidationEnd
            End Get
            Set(ByVal value As DateTime)
                _fakturValidationEnd = value
            End Set
        End Property


        <ColumnInfo("FakturOpenStart", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property FakturOpenStart As DateTime
            Get
                Return _fakturOpenStart
            End Get
            Set(ByVal value As DateTime)
                _fakturOpenStart = value
            End Set
        End Property


        <ColumnInfo("FakturOpenEnd", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property FakturOpenEnd As DateTime
            Get
                Return _fakturOpenEnd
            End Get
            Set(ByVal value As DateTime)
                _fakturOpenEnd = value
            End Set
        End Property


        <ColumnInfo("VehicleTypeID", "{0}")> _
        Public Property VehicleTypeID As Short
            Get
                Return _vehicleTypeID
            End Get
            Set(ByVal value As Short)
                _vehicleTypeID = value
            End Set
        End Property


        <ColumnInfo("VehicleTypeCode", "'{0}'")> _
        Public Property VehicleTypeCode As String
            Get
                Return _vehicleTypeCode
            End Get
            Set(ByVal value As String)
                _vehicleTypeCode = value
            End Set
        End Property


        <ColumnInfo("VehicleTypeDesc", "'{0}'")> _
        Public Property VehicleTypeDesc As String
            Get
                Return _vehicleTypeDesc
            End Get
            Set(ByVal value As String)
                _vehicleTypeDesc = value
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