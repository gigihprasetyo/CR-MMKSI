#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_SPKTracking Domain Object.
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
    <Serializable(), TableInfo("VWI_SPKTracking")> _
    Public Class VWI_SPKTracking
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
        Private _sPKNumber As String = String.Empty
        Private _dealerSPKNumber As String = String.Empty
        Private _dealerSPKDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _status As String = String.Empty
        Private _statusDescription As String = String.Empty
        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _branchName As String = String.Empty
        Private _salesmanCode As String = String.Empty
        Private _salesmanName As String = String.Empty
        Private _sPKCustomer As String = String.Empty
        Private _rejectedReason As String = String.Empty
        Private _dealerCity As String = String.Empty
        Private _customerType As String = String.Empty
        Private _pilotingSPKMatching As String = String.Empty
        Private _rowStatus As Short
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _createdBy As String
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String
        Private _sPKDetail As String = String.Empty
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

        <ColumnInfo("SPKNumber", "'{0}'")> _
        Public Property SPKNumber As String
            Get
                Return _sPKNumber
            End Get
            Set(ByVal value As String)
                _sPKNumber = value
            End Set
        End Property

        <ColumnInfo("DealerSPKNumber", "'{0}'")> _
        Public Property DealerSPKNumber As String
            Get
                Return _dealerSPKNumber
            End Get
            Set(ByVal value As String)
                _dealerSPKNumber = value
            End Set
        End Property

        <ColumnInfo("DealerSPKDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DealerSPKDate As DateTime
            Get
                Return _dealerSPKDate
            End Get
            Set(ByVal value As DateTime)
                _dealerSPKDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property


        <ColumnInfo("StatusDescription", "'{0}'")> _
        Public Property StatusDescription As String
            Get
                Return _statusDescription
            End Get
            Set(ByVal value As String)
                _statusDescription = value
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


        <ColumnInfo("DealerName", "'{0}'")> _
        Public Property DealerName As String
            Get
                Return _dealerName
            End Get
            Set(ByVal value As String)
                _dealerName = value
            End Set
        End Property


        <ColumnInfo("BranchName", "'{0}'")> _
        Public Property BranchName As String
            Get
                Return _branchName
            End Get
            Set(ByVal value As String)
                _branchName = value
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

        <ColumnInfo("SPKCustomer", "'{0}'")> _
        Public Property SPKCustomer As String
            Get
                Return _sPKCustomer
            End Get
            Set(ByVal value As String)
                _sPKCustomer = value
            End Set
        End Property

        <ColumnInfo("RejectedReason", "'{0}'")> _
        Public Property RejectedReason As String
            Get
                Return _rejectedReason
            End Get
            Set(ByVal value As String)
                _rejectedReason = value
            End Set
        End Property

        <ColumnInfo("DealerCity", "'{0}'")> _
        Public Property DealerCity As String
            Get
                Return _dealerCity
            End Get
            Set(ByVal value As String)
                _dealerCity = value
            End Set
        End Property

        <ColumnInfo("CustomerType", "'{0}'")> _
        Public Property CustomerType As String
            Get
                Return _customerType
            End Get
            Set(ByVal value As String)
                _customerType = value
            End Set
        End Property

        <ColumnInfo("PilotingSPKMatching", "'{0}'")> _
        Public Property PilotingSPKMatching As String
            Get
                Return _pilotingSPKMatching
            End Get
            Set(ByVal value As String)
                _pilotingSPKMatching = value
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

        <ColumnInfo("CreatedTime", "{0}")> _
        Public Property CreatedTime As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
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

        <ColumnInfo("LastUpdateTime", "{0}")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
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

        <ColumnInfo("SPKDetail", "'{0}'")> _
        Public Property SPKDetail As String
            Get
                Return _sPKDetail
            End Get
            Set(ByVal value As String)
                _sPKDetail = value
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
