#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_SPKCustomerHaveRequest Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 11/10/2018 - 9:53:33
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
    <Serializable(), TableInfo("VWI_SPKCustomerHaveRequest")> _
    Public Class VWI_SPKCustomerHaveRequest
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
        Private _dealerBranchCode As String = String.Empty
        Private _sPKNumber As String = String.Empty
        Private _requestNo As String = String.Empty
        Private _customerCode As String = String.Empty
        Private _identityNumber As String = String.Empty
        Private _requestDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
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

        <ColumnInfo("DealerBranchCode", "'{0}'")> _
        Public Property DealerBranchCode As String
            Get
                Return _dealerBranchCode
            End Get
            Set(ByVal value As String)
                _dealerBranchCode = value
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

        <ColumnInfo("RequestNo", "'{0}'")> _
        Public Property RequestNo As String
            Get
                Return _requestNo
            End Get
            Set(ByVal value As String)
                _requestNo = value
            End Set
        End Property

        <ColumnInfo("CustomerCode", "'{0}'")> _
        Public Property CustomerCode As String
            Get
                Return _customerCode
            End Get
            Set(ByVal value As String)
                _customerCode = value
            End Set
        End Property

        <ColumnInfo("IdentityNumber", "'{0}'")> _
        Public Property IdentityNumber As String
            Get
                Return _identityNumber
            End Get
            Set(ByVal value As String)
                _identityNumber = value
            End Set
        End Property

        <ColumnInfo("RequestDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property RequestDate As DateTime
            Get
                Return _requestDate
            End Get
            Set(ByVal value As DateTime)
                _requestDate = value
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


