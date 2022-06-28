
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : v_GetTokenExpired Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 12/10/2009 - 11:23:35 AM
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
    <Serializable(), TableInfo("v_GetTokenExpired")> _
    Public Class v_GetTokenExpired
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Short)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Short
        Private _handphone As String = String.Empty
        Private _name As String = String.Empty
        Private _email As String = String.Empty
        Private _activationCode As String = String.Empty
        Private _dealerCode As String = String.Empty
        Private _dayRemind As Integer
        Private _dateToday As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dateRemind As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _createdBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty

        Private _userName As String = String.Empty
        Private _tokenAlertTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dayAlertStatus As Integer




#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Short
            Get
                Return _iD
            End Get
            Set(ByVal value As Short)
                _iD = value
            End Set
        End Property


        <ColumnInfo("Name", "{0}")> _
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property


        <ColumnInfo("Handphone", "'{0}'")> _
        Public Property Handphone() As String
            Get
                Return _handphone
            End Get
            Set(ByVal value As String)
                _handphone = value
            End Set
        End Property


        <ColumnInfo("Email", "'{0}'")> _
        Public Property Email() As String
            Get
                Return _email
            End Get
            Set(ByVal value As String)
                _email = value
            End Set
        End Property

        <ColumnInfo("ActivationCode", "{0}")> _
       Public Property ActivationCode() As String
            Get
                Return _activationCode
            End Get
            Set(ByVal value As String)
                _activationCode = value
            End Set
        End Property

        <ColumnInfo("DealerCode", "{0}")> _
       Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("DayRemind", "{0}")> _
        Public Property DayRemind() As Integer
            Get
                Return _dayRemind
            End Get
            Set(ByVal value As Integer)
                _dayRemind = value
            End Set
        End Property


        <ColumnInfo("DateToday", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property DateToday() As DateTime
            Get
                Return _dateToday
            End Get
            Set(ByVal value As DateTime)
                _dateToday = value
            End Set
        End Property


        <ColumnInfo("DateRemind", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property DateRemind() As DateTime
            Get
                Return _dateRemind
            End Get
            Set(ByVal value As DateTime)
                _dateRemind = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus() As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy() As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy() As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("UserName", "'{0}'")> _
      Public Property username() As String
            Get
                Return _userName
            End Get
            Set(ByVal value As String)
                _userName = value
            End Set
        End Property

        <ColumnInfo("TokenAlertTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
              Public Property TokenAlertTime() As DateTime
            Get
                Return _tokenAlertTime
            End Get
            Set(ByVal value As DateTime)
                _tokenAlertTime = value
            End Set
        End Property

        <ColumnInfo("DayAlertStatus", "{0}")> _
               Public Property DayAlertStatus() As Integer
            Get
                Return _dayAlertStatus
            End Get
            Set(ByVal value As Integer)
                _dayAlertStatus = value
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

