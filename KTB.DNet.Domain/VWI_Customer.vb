
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VCustomer Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 17/05/2018 - 10:33:09
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
    <Serializable(), TableInfo("VWI_Customer")> _
    Public Class VWI_Customer
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
        Private _customerCode As String = String.Empty
        Private _customerRequestId As Integer
        Private _dealerCode As String = String.Empty
        Private _sPKNumber As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




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


        <ColumnInfo("CustomerCode", "'{0}'")> _
        Public Property CustomerCode As String
            Get
                Return _customerCode
            End Get
            Set(ByVal value As String)
                _customerCode = value
            End Set
        End Property


        <ColumnInfo("CustomerRequestId", "{0}")> _
        Public Property CustomerRequestId As Integer
            Get
                Return _customerRequestId
            End Get
            Set(ByVal value As Integer)
                _customerRequestId = value
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


        <ColumnInfo("SPKNumber", "'{0}'")> _
        Public Property SPKNumber As String
            Get
                Return _sPKNumber
            End Get
            Set(ByVal value As String)
                _sPKNumber = value
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

