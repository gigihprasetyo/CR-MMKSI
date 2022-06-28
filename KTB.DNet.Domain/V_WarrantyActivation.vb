#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_WarrantyActivation Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/20/2020 - 12:23:06 PM
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
    <Serializable(), TableInfo("V_WarrantyActivation")> _
    Public Class V_WarrantyActivation
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
        Private _chassisNumber As String = String.Empty
        Private _soldDealerID As Short
        Private _customerName As String = String.Empty
        Private _pDIID As Integer
        Private _pDIStatus As String = String.Empty
        Private _pDIDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _pDICertificateFile As String = String.Empty
        Private _warantyActivationID As Integer
        Private _wADate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _waCertificateFile As String = String.Empty
        Private _waRequestor As String = String.Empty
        Private _sPKDealerCode As String = String.Empty
        Private _sPKDealerName As String = String.Empty
        Private _dSFilePath As String = String.Empty
        Private _pKTDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _waStatus As Integer
        Private _waStatusDesc As String = String.Empty
        Private _dealerGroupID As Integer
        Private _salesmanCode As String = String.Empty
        Private _salesmanName As String = String.Empty
        Private _pilotingWarranty As String = String.Empty
        Private _lastUpdateTimePKT As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateTimePDI As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
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

        <ColumnInfo("ChassisNumber", "'{0}'")> _
        Public Property ChassisNumber As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
            End Set
        End Property

        <ColumnInfo("SoldDealerID", "{0}")> _
        Public Property SoldDealerID As Short
            Get
                Return _soldDealerID
            End Get
            Set(ByVal value As Short)
                _soldDealerID = value
            End Set
        End Property

        <ColumnInfo("CustomerName", "'{0}'")> _
        Public Property CustomerName As String
            Get
                Return _customerName
            End Get
            Set(ByVal value As String)
                _customerName = value
            End Set
        End Property


        <ColumnInfo("PDIID", "{0}")> _
        Public Property PDIID As Integer
            Get
                Return _pDIID
            End Get
            Set(ByVal value As Integer)
                _pDIID = value
            End Set
        End Property


        <ColumnInfo("PDIStatus", "'{0}'")> _
        Public Property PDIStatus As String
            Get
                Return _pDIStatus
            End Get
            Set(ByVal value As String)
                _pDIStatus = value
            End Set
        End Property


        <ColumnInfo("PDIDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PDIDate As DateTime
            Get
                Return _pDIDate
            End Get
            Set(ByVal value As DateTime)
                _pDIDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PDICertificateFile", "'{0}'")> _
        Public Property PDICertificateFile As String
            Get
                Return _pDICertificateFile
            End Get
            Set(ByVal value As String)
                _pDICertificateFile = value
            End Set
        End Property


        <ColumnInfo("WarantyActivationID", "{0}")> _
        Public Property WarantyActivationID As Integer
            Get
                Return _warantyActivationID
            End Get
            Set(ByVal value As Integer)
                _warantyActivationID = value
            End Set
        End Property

        <ColumnInfo("WADate", "'{0:yyyy/MM/dd}'")> _
        Public Property WADate As DateTime
            Get
                Return _wADate
            End Get
            Set(ByVal value As DateTime)
                _wADate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("WaCertificateFile", "'{0}'")> _
        Public Property WaCertificateFile As String
            Get
                Return _waCertificateFile
            End Get
            Set(ByVal value As String)
                _waCertificateFile = value
            End Set
        End Property

        <ColumnInfo("WARequestor", "'{0}'")> _
        Public Property WARequestor As String
            Get
                Return _waRequestor
            End Get
            Set(ByVal value As String)
                _waRequestor = value
            End Set
        End Property

        <ColumnInfo("SPKDealerCode", "'{0}'")> _
        Public Property SPKDealerCode As String
            Get
                Return _sPKDealerCode
            End Get
            Set(ByVal value As String)
                _sPKDealerCode = value
            End Set
        End Property

        <ColumnInfo("SPKDealerName", "'{0}'")> _
        Public Property SPKDealerName As String
            Get
                Return _sPKDealerName
            End Get
            Set(ByVal value As String)
                _sPKDealerName = value
            End Set
        End Property

        <ColumnInfo("DSFilePath", "'{0}'")> _
        Public Property DSFilePath As String
            Get
                Return _dSFilePath
            End Get
            Set(ByVal value As String)
                _dSFilePath = value
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

        <ColumnInfo("WaStatus", "{0}")> _
        Public Property WaStatus As Integer
            Get
                Return _waStatus
            End Get
            Set(ByVal value As Integer)
                _waStatus = value
            End Set
        End Property

        <ColumnInfo("WaStatusDesc", "'{0}'")> _
        Public Property WaStatusDesc As String
            Get
                Return _waStatusDesc
            End Get
            Set(ByVal value As String)
                _waStatusDesc = value
            End Set
        End Property

        <ColumnInfo("DealerGroupID", "{0}")> _
        Public Property DealerGroupID As Integer
            Get
                Return _dealerGroupID
            End Get
            Set(ByVal value As Integer)
                _dealerGroupID = value
            End Set
        End Property

        <ColumnInfo("SalesmanCode", "'{0}'")> _
        Public Property SalesmanCode As String
            Get
                Return _salesmanCode
            End Get
            Set(ByVal value As String)
                _salesmanCode = value
            End Set
        End Property

        <ColumnInfo("SalesmanName", "'{0}'")> _
        Public Property SalesmanName As String
            Get
                Return _salesmanName
            End Get
            Set(ByVal value As String)
                _salesmanName = value
            End Set
        End Property

        <ColumnInfo("PilotingWarranty", "'{0}'")> _
        Public Property PilotingWarranty As String
            Get
                Return _pilotingWarranty
            End Get
            Set(ByVal value As String)
                _pilotingWarranty = value
            End Set
        End Property

        <ColumnInfo("LastUpdateTimePKT", "'{0}'")> _
        Public Property LastUpdateTimePKT As DateTime
            Get
                Return _lastUpdateTimePKT
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTimePKT = value
            End Set
        End Property

        <ColumnInfo("LastUpdateTimePDI", "'{0}'")> _
        Public Property LastUpdateTimePDI As DateTime
            Get
                Return _lastUpdateTimePDI
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTimePDI = value
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
