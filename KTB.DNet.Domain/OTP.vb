
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : OTP Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 7/24/2018 - 10:45:16 AM
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
    <Serializable(), TableInfo("OTP")> _
    Public Class OTP
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
        Private _userInfoID As Integer
        Private _processType As Short
        Private _numberDestination As String = String.Empty
        Private _challengeCode As String = String.Empty
        Private _sMSValue As String = String.Empty
        Private _status As Integer
        Private _validUntil As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _userInfo As UserInfo


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

        <ColumnInfo("UserInfoID", "{0}"), _
       RelationInfo("UserInfo", "ID", "OTP", "UserInfoID")> _
        Public Property UserInfo() As UserInfo
            Get
                Try
                    If Not IsNothing(Me._userInfo) AndAlso (Not Me._userInfo.IsLoaded) Then
                        Me._userInfo = CType(DoLoad(GetType(UserInfo).ToString(), _userInfo.ID), UserInfo)
                        Me._userInfo.MarkLoaded()

                    End If

                    Return Me._userInfo

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As UserInfo)

                Me._userInfo = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._userInfo.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("UserInfoID", "{0}")> _
        Public Property UserInfoID As Integer
            Get
                Return _userInfoID
            End Get
            Set(ByVal value As Integer)
                _userInfoID = value
            End Set
        End Property


        <ColumnInfo("ProcessType", "{0}")> _
        Public Property ProcessType As Short
            Get
                Return _processType
            End Get
            Set(ByVal value As Short)
                _processType = value
            End Set
        End Property


        <ColumnInfo("NumberDestination", "'{0}'")> _
        Public Property NumberDestination As String
            Get
                Return _numberDestination
            End Get
            Set(ByVal value As String)
                _numberDestination = value
            End Set
        End Property


        <ColumnInfo("ChallengeCode", "'{0}'")> _
        Public Property ChallengeCode As String
            Get
                Return _challengeCode
            End Get
            Set(ByVal value As String)
                _challengeCode = value
            End Set
        End Property


        <ColumnInfo("SMSValue", "'{0}'")> _
        Public Property SMSValue As String
            Get
                Return _sMSValue
            End Get
            Set(ByVal value As String)
                _sMSValue = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Integer
            Get
                Return _status
            End Get
            Set(ByVal value As Integer)
                _status = value
            End Set
        End Property


        <ColumnInfo("ValidUntil", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidUntil As DateTime
            Get
                Return _validUntil
            End Get
            Set(ByVal value As DateTime)
                _validUntil = value
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


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
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

