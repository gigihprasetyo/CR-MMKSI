
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DMSWorkOrderWSCStatus Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 20/07/2018 - 5:50:52
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
    <Serializable(), TableInfo("VWI_DMSWorkOrderWSCStatus")> _
    Public Class VWI_DMSWorkOrderWSCStatus
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
        Private _dealerCode As String = String.Empty
        Private _chassisNumber As String = String.Empty
        Private _workOrderNumber As String = String.Empty
        Private _pQRNo As String = String.Empty
        Private _pQRType As Integer
        Private _pQRTypeText As String = String.Empty
        Private _pQRDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _pQRStatus As Short
        Private _pQRStatusText As String = String.Empty
        Private _claimType As String = String.Empty
        Private _claimNumber As String = String.Empty
        Private _description As String = String.Empty
        Private _claimStatus As String = String.Empty
        Private _wSCStatus As String = String.Empty
        Private _wSCStatusText As String = String.Empty
        Private _laborAmount As Decimal = 0
        Private _partAmount As Decimal = 0
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

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


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
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


        <ColumnInfo("WorkOrderNumber", "'{0}'")> _
        Public Property WorkOrderNumber As String
            Get
                Return _workOrderNumber
            End Get
            Set(ByVal value As String)
                _workOrderNumber = value
            End Set
        End Property


        <ColumnInfo("PQRNo", "'{0}'")> _
        Public Property PQRNo As String
            Get
                Return _pQRNo
            End Get
            Set(ByVal value As String)
                _pQRNo = value
            End Set
        End Property


        <ColumnInfo("PQRType", "{0}")> _
        Public Property PQRType As Integer
            Get
                Return _pQRType
            End Get
            Set(ByVal value As Integer)
                _pQRType = value
            End Set
        End Property


        <ColumnInfo("PQRTypeText", "'{0}'")> _
        Public Property PQRTypeText As String
            Get
                Return _pQRTypeText
            End Get
            Set(ByVal value As String)
                _pQRTypeText = value
            End Set
        End Property


        <ColumnInfo("PQRDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PQRDate As DateTime
            Get
                Return _pQRDate
            End Get
            Set(ByVal value As DateTime)
                _pQRDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PQRStatus", "{0}")> _
        Public Property PQRStatus As Short
            Get
                Return _pQRStatus
            End Get
            Set(ByVal value As Short)
                _pQRStatus = value
            End Set
        End Property


        <ColumnInfo("PQRStatusText", "'{0}'")> _
        Public Property PQRStatusText As String
            Get
                Return _pQRStatusText
            End Get
            Set(ByVal value As String)
                _pQRStatusText = value
            End Set
        End Property


        <ColumnInfo("ClaimType", "'{0}'")> _
        Public Property ClaimType As String
            Get
                Return _claimType
            End Get
            Set(ByVal value As String)
                _claimType = value
            End Set
        End Property


        <ColumnInfo("ClaimNumber", "'{0}'")> _
        Public Property ClaimNumber As String
            Get
                Return _claimNumber
            End Get
            Set(ByVal value As String)
                _claimNumber = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("ClaimStatus", "'{0}'")> _
        Public Property ClaimStatus As String
            Get
                Return _claimStatus
            End Get
            Set(ByVal value As String)
                _claimStatus = value
            End Set
        End Property


        <ColumnInfo("WSCStatus", "'{0}'")> _
        Public Property WSCStatus As String
            Get
                Return _wSCStatus
            End Get
            Set(ByVal value As String)
                _wSCStatus = value
            End Set
        End Property


        <ColumnInfo("WSCStatusText", "'{0}'")>
        Public Property WSCStatusText As String
            Get
                Return _wSCStatusText
            End Get
            Set(ByVal value As String)
                _wSCStatusText = value
            End Set
        End Property


        <ColumnInfo("LaborAmount", "{0}")>
        Public Property LaborAmount As Decimal
            Get
                Return _laborAmount
            End Get
            Set(value As Decimal)
                _laborAmount = value
            End Set
        End Property


        <ColumnInfo("PartAmount", "{0}")>
        Public Property PartAmount As Decimal
            Get
                Return _partAmount
            End Get
            Set(value As Decimal)
                _partAmount = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
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

