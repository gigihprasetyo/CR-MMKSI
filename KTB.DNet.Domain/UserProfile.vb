#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : UserProfile Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 1/16/2007 - 10:54:50 AM
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
    <Serializable(), TableInfo("UserProfile")> _
    Public Class UserProfile
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
        Private _registrationStatus As Short
        Private _activationCode As String = String.Empty
        Private _tempActivationCode As String = String.Empty
        Private _activationSentTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _sessionID As String = String.Empty
        Private _imageDescription As String = String.Empty
        Private _activationStatus As Short
        Private _userLockStatus As Short
        Private _birthDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _motherName As String = String.Empty
        Private _loginCount As Integer
        Private _question1 As String = String.Empty
        Private _question2 As String = String.Empty
        Private _question3 As String = String.Empty
        Private _question4 As String = String.Empty
        Private _question5 As String = String.Empty
        Private _answer1 As String = String.Empty
        Private _answer2 As String = String.Empty
        Private _answer3 As String = String.Empty
        Private _answer4 As String = String.Empty
        Private _answer5 As String = String.Empty
        Private _lastLoginIPAddress As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _transitionHP As String = String.Empty
        Private _transitionActivationCode As String = String.Empty
        Private _transitionProcessDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _userInfo As UserInfo
        Private _imageID As Integer
        Private _bingo As Bingo

#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("RegistrationStatus", "{0}")> _
        Public Property RegistrationStatus() As Short
            Get
                Return _registrationStatus
            End Get
            Set(ByVal value As Short)
                _registrationStatus = value
            End Set
        End Property


        <ColumnInfo("ActivationCode", "'{0}'")> _
        Public Property ActivationCode() As String
            Get
                Return _activationCode
            End Get
            Set(ByVal value As String)
                _activationCode = value
            End Set
        End Property


        <ColumnInfo("TempActivationCode", "'{0}'")> _
        Public Property TempActivationCode() As String
            Get
                Return _tempActivationCode
            End Get
            Set(ByVal value As String)
                _tempActivationCode = value
            End Set
        End Property


        <ColumnInfo("ActivationSentTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ActivationSentTime() As DateTime
            Get
                Return _activationSentTime
            End Get
            Set(ByVal value As DateTime)
                _activationSentTime = value
            End Set
        End Property


        <ColumnInfo("SessionID", "'{0}'")> _
        Public Property SessionID() As String
            Get
                Return _sessionID
            End Get
            Set(ByVal value As String)
                _sessionID = value
            End Set
        End Property


        <ColumnInfo("ImageDescription", "'{0}'")> _
        Public Property ImageDescription() As String
            Get
                Return _imageDescription
            End Get
            Set(ByVal value As String)
                _imageDescription = value
            End Set
        End Property


        <ColumnInfo("ActivationStatus", "{0}")> _
        Public Property ActivationStatus() As Short
            Get
                Return _activationStatus
            End Get
            Set(ByVal value As Short)
                _activationStatus = value
            End Set
        End Property


        <ColumnInfo("UserLockStatus", "{0}")> _
        Public Property UserLockStatus() As Short
            Get
                Return _userLockStatus
            End Get
            Set(ByVal value As Short)
                _userLockStatus = value
            End Set
        End Property


        <ColumnInfo("BirthDate", "'{0:yyyy/MM/dd}'")> _
        Public Property BirthDate() As DateTime
            Get
                Return _birthDate
            End Get
            Set(ByVal value As DateTime)
                _birthDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("MotherName", "'{0}'")> _
        Public Property MotherName() As String
            Get
                Return _motherName
            End Get
            Set(ByVal value As String)
                _motherName = value
            End Set
        End Property


        <ColumnInfo("LoginCount", "{0}")> _
        Public Property LoginCount() As Integer
            Get
                Return _loginCount
            End Get
            Set(ByVal value As Integer)
                _loginCount = value
            End Set
        End Property


        <ColumnInfo("Question1", "'{0}'")> _
        Public Property Question1() As String
            Get
                Return _question1
            End Get
            Set(ByVal value As String)
                _question1 = value
            End Set
        End Property


        <ColumnInfo("Question2", "'{0}'")> _
        Public Property Question2() As String
            Get
                Return _question2
            End Get
            Set(ByVal value As String)
                _question2 = value
            End Set
        End Property


        <ColumnInfo("Question3", "'{0}'")> _
        Public Property Question3() As String
            Get
                Return _question3
            End Get
            Set(ByVal value As String)
                _question3 = value
            End Set
        End Property


        <ColumnInfo("Question4", "'{0}'")> _
        Public Property Question4() As String
            Get
                Return _question4
            End Get
            Set(ByVal value As String)
                _question4 = value
            End Set
        End Property


        <ColumnInfo("Question5", "'{0}'")> _
        Public Property Question5() As String
            Get
                Return _question5
            End Get
            Set(ByVal value As String)
                _question5 = value
            End Set
        End Property


        <ColumnInfo("Answer1", "'{0}'")> _
        Public Property Answer1() As String
            Get
                Return _answer1
            End Get
            Set(ByVal value As String)
                _answer1 = value
            End Set
        End Property


        <ColumnInfo("Answer2", "'{0}'")> _
        Public Property Answer2() As String
            Get
                Return _answer2
            End Get
            Set(ByVal value As String)
                _answer2 = value
            End Set
        End Property


        <ColumnInfo("Answer3", "'{0}'")> _
        Public Property Answer3() As String
            Get
                Return _answer3
            End Get
            Set(ByVal value As String)
                _answer3 = value
            End Set
        End Property


        <ColumnInfo("Answer4", "'{0}'")> _
        Public Property Answer4() As String
            Get
                Return _answer4
            End Get
            Set(ByVal value As String)
                _answer4 = value
            End Set
        End Property


        <ColumnInfo("Answer5", "'{0}'")> _
        Public Property Answer5() As String
            Get
                Return _answer5
            End Get
            Set(ByVal value As String)
                _answer5 = value
            End Set
        End Property


        <ColumnInfo("LastLoginIPAddress", "'{0}'")> _
        Public Property LastLoginIPAddress() As String
            Get
                Return _lastLoginIPAddress
            End Get
            Set(ByVal value As String)
                _lastLoginIPAddress = value
            End Set
        End Property

        <ColumnInfo("TransitionHP", "'{0}'")> _
       Public Property TransitionHP() As String
            Get
                Return _transitionHP
            End Get
            Set(ByVal value As String)
                _transitionHP = value
            End Set
        End Property

        <ColumnInfo("TransitionActivationCode", "'{0}'")> _
     Public Property TransitionActivationCode() As String
            Get
                Return _transitionActivationCode
            End Get
            Set(ByVal value As String)
                _transitionActivationCode = value
            End Set
        End Property

        <ColumnInfo("TransitionProcessDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
      Public Property TransitionProcessDate() As DateTime
            Get
                Return _transitionProcessDate
            End Get
            Set(ByVal value As DateTime)
                _transitionProcessDate = value
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


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy() As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
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


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy() As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
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


        <ColumnInfo("UserID", "{0}"), _
        RelationInfo("UserInfo", "ID", "UserProfile", "UserID")> _
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

        <ColumnInfo("ImageID", "{0}")> _
        Public Property ImageID() As Integer

            Get
                Return _imageID
            End Get
            Set(ByVal value As Integer)
                _imageID = value
            End Set
        End Property
        <ColumnInfo("BingoID", "{0}"), _
        RelationInfo("Bingo", "ID", "UserProfile", "BingoID")> _
        Public Property Bingo() As Bingo
            Get
                Try
                    If Not IsNothing(Me._bingo) AndAlso (Not Me._bingo.IsLoaded) Then

                        Me._bingo = CType(DoLoad(GetType(Bingo).ToString(), _bingo.ID), Bingo)
                        Me._bingo.MarkLoaded()

                    End If

                    Return Me._bingo

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Bingo)

                Me._bingo = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._bingo.MarkLoaded()
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
        Private _userID As Integer
        <ColumnInfo("UserID", "{0}")> _
        Public Property UserID() As Integer

            Get
                Return _userID
            End Get
            Set(ByVal value As Integer)
                _userID = value
            End Set
        End Property
#End Region

    End Class
End Namespace

