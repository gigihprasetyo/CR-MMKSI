
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : UserInfo Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 1/9/2007 - 10:44:09 AM
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
Imports KTB.DNet.DataMapper.Framework
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("UserInfo")> _
    Public Class UserInfo
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
        Private _organizationID As Integer
        Private _subOrganizationID As Integer
        Private _userName As String = String.Empty
        Private _password As Byte()
        Private _firstName As String = String.Empty
        Private _lastName As String = String.Empty
        Private _birthday As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _jobPositionOld As String = String.Empty
        Private _email As String = String.Empty
        Private _emailValidation As Short
        Private _picture As Byte()
        Private _handPhone As String = String.Empty
        Private _tokenAlertTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _telephone As String = String.Empty
        Private _question As String = String.Empty
        Private _answer As String = String.Empty
        Private _userStatus As Byte
        Private _loginFlag As String = String.Empty
        Private _lastLogin As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _messageNotification As Short
        Private _cCKey As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _dealerBranch As Dealer

        Private _jobPosition As JobPosition
        Private _userAlerts As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _userRoles As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _lOCKUSERs As System.Collections.ArrayList = New System.Collections.ArrayList

        Private _userProfile As UserProfile = New UserProfile(0)
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

        <ColumnInfo("OrganizationID", "{0}")> _
        Public Property OrganizationID() As Integer
            Get
                Return _organizationID
            End Get
            Set(ByVal value As Integer)
                _organizationID = value
            End Set
        End Property

        <ColumnInfo("SubOrganizationID", "{0}")> _
        Public Property SubOrganizationID() As Integer
            Get
                Return _subOrganizationID
            End Get
            Set(ByVal value As Integer)
                _subOrganizationID = value
            End Set
        End Property

        <ColumnInfo("UserName", "'{0}'")> _
        Public Property UserName() As String
            Get
                Return _userName
            End Get
            Set(ByVal value As String)
                _userName = value
            End Set
        End Property


        <ColumnInfo("Password", "{0}")> _
        Public Property Password() As Byte()
            Get
                Return _password
            End Get
            Set(ByVal value As Byte())
                _password = value
            End Set
        End Property


        <ColumnInfo("FirstName", "'{0}'")> _
        Public Property FirstName() As String
            Get
                Return _firstName
            End Get
            Set(ByVal value As String)
                _firstName = value
            End Set
        End Property


        <ColumnInfo("LastName", "'{0}'")> _
        Public Property LastName() As String
            Get
                Return _lastName
            End Get
            Set(ByVal value As String)
                _lastName = value
            End Set
        End Property

        <ColumnInfo("Birthday", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property Birthday() As DateTime
            Get
                Return _birthday
            End Get
            Set(ByVal value As DateTime)
                _birthday = value
            End Set
        End Property

        <ColumnInfo("JobPositionOld", "'{0}'")> _
        Public Property JobPositionOld() As String
            Get
                Return _jobPositionOld
            End Get
            Set(ByVal value As String)
                _jobPositionOld = value
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

        <ColumnInfo("EmailValidation", "{0}")> _
        Public Property EmailValidation() As Short
            Get
                Return _emailValidation
            End Get
            Set(ByVal value As Short)
                _emailValidation = value
            End Set
        End Property
        <ColumnInfo("Picture", "{0}")> _
        Public Property Picture() As Byte()
            Get
                Return _picture
            End Get
            Set(ByVal value As Byte())
                _picture = value
            End Set
        End Property

        <ColumnInfo("HandPhone", "'{0}'")> _
        Public Property HandPhone() As String
            Get
                Return _handPhone
            End Get
            Set(ByVal value As String)
                _handPhone = value
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


        <ColumnInfo("Telephone", "'{0}'")> _
        Public Property Telephone() As String
            Get
                Return _telephone
            End Get
            Set(ByVal value As String)
                _telephone = value
            End Set
        End Property


        <ColumnInfo("Question", "'{0}'")> _
        Public Property Question() As String
            Get
                Return _question
            End Get
            Set(ByVal value As String)
                _question = value
            End Set
        End Property


        <ColumnInfo("Answer", "'{0}'")> _
        Public Property Answer() As String
            Get
                Return _answer
            End Get
            Set(ByVal value As String)
                _answer = value
            End Set
        End Property


        <ColumnInfo("UserStatus", "{0}")> _
        Public Property UserStatus() As Byte
            Get
                Return _userStatus
            End Get
            Set(ByVal value As Byte)
                _userStatus = value
            End Set
        End Property


        <ColumnInfo("LoginFlag", "'{0}'")> _
        Public Property LoginFlag() As String
            Get
                Return _loginFlag
            End Get
            Set(ByVal value As String)
                _loginFlag = value
            End Set
        End Property


        <ColumnInfo("LastLogin", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastLogin() As DateTime
            Get
                Return _lastLogin
            End Get
            Set(ByVal value As DateTime)
                _lastLogin = value
            End Set
        End Property

        <ColumnInfo("MessageNotification", "{0}")> _
        Public Property MessageNotification() As Short
            Get
                Return _messageNotification
            End Get
            Set(ByVal value As Short)
                _messageNotification = value
            End Set
        End Property

        <ColumnInfo("JobPositionID", "{0}"), _
RelationInfo("JobPosition", "ID", "UserInfo", "JobPositionID")> _
        Public Property JobPosition() As JobPosition
            Get
                Try
                    If Not IsNothing(Me._jobPosition) AndAlso (Not Me._jobPosition.IsLoaded) Then

                        Me._jobPosition = CType(DoLoad(GetType(JobPosition).ToString(), _jobPosition.ID), JobPosition)
                        Me._jobPosition.MarkLoaded()

                    End If

                    Return Me._jobPosition

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As JobPosition)

                Me._jobPosition = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._jobPosition.MarkLoaded()
                End If
            End Set
        End Property
        <RelationInfo("UserInfo", "ID", "UserAlert", "UserInfoID")> _
        Public ReadOnly Property UserAlerts() As System.Collections.ArrayList
            Get
                Try
                    If (Me._userAlerts.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(UserAlert), "UserInfo", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(UserAlert), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._userAlerts = DoLoadArray(GetType(UserAlert).ToString, criterias)
                    End If

                    Return Me._userAlerts

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property


        <ColumnInfo("CCKey", "'{0}'")> _
        Public Property CCKey() As String
            Get
                Return _cCKey
            End Get
            Set(ByVal value As String)
                _cCKey = value
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


        <ColumnInfo("OrganizationID", "{0}"), _
        RelationInfo("Dealer", "ID", "UserInfo", "OrganizationID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

                        Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), _dealer.ID), Dealer)
                        Me._dealer.MarkLoaded()

                    End If

                    Return Me._dealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._dealer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("SubOrganizationID", "{0}"), _
        RelationInfo("Dealer", "ID", "UserInfo", "SubOrganizationID")> _
        Public Property DealerBranch() As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealerBranch) AndAlso (Not Me._dealerBranch.IsLoaded) Then

                        Me._dealerBranch = CType(DoLoad(GetType(Dealer).ToString(), _dealerBranch.ID), Dealer)
                        Me._dealerBranch.MarkLoaded()

                    End If

                    Return Me._dealerBranch

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._dealerBranch = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerBranch.MarkLoaded()
                End If
            End Set
        End Property

        <RelationInfo("UserInfo", "ID", "UserRole", "UserID")> _
        Public ReadOnly Property UserRoles() As System.Collections.ArrayList
            Get
                Try
                    If (Me._userRoles.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(UserRole), "UserInfo", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(UserRole), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._userRoles = DoLoadArray(GetType(UserRole).ToString, criterias)
                    End If

                    Return Me._userRoles

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("UserInfo", "ID", "LOCKUSER", "UserID")> _
        Public ReadOnly Property LOCKUSERs() As System.Collections.ArrayList
            Get
                Try
                    If (Me._lOCKUSERs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(LockUser), "UserInfo", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(LockUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._lOCKUSERs = DoLoadArray(GetType(LockUser).ToString, criterias)
                    End If

                    Return Me._lOCKUSERs

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
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


        <ColumnInfo("ID", "{0}"), _
        RelationInfo("UserInfo", "ID", "UserProfile", "UserID")> _
        Public ReadOnly Property UserProfile() As UserProfile
            Get
                Try
                    If IsNothing(Me._userProfile) OrElse (Me._userProfile.ID = 0) Then
                        Dim _criteria As Criteria = New Criteria(GetType(UserProfile), "UserInfo", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(UserProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Dim tempColl As ArrayList = DoLoadArray(GetType(UserProfile).ToString, criterias)

                        If (tempColl.Count > 0) Then
                            Me._userProfile = CType(tempColl(0), UserProfile)
                        Else
                            Me._userProfile = Nothing
                        End If
                    End If

                    Return Me._userProfile

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get
        End Property

        Public Shared Function Convert(ByVal UserInfo As String) As String
            Dim _newUserInfo As String
            Dim m_DealerMapper As IMapper
            Dim id As Integer

            If (UserInfo.Length > 6) AndAlso (Not UserInfo.ToUpper.Contains("_IF_")) AndAlso (Integer.TryParse(UserInfo.Substring(0, 6), id)) Then
                m_DealerMapper = MapperFactory.GetInstance.GetMapper(GetType(Dealer).ToString)

                Dim objDealer As Dealer = CType(m_DealerMapper.Retrieve(id), Dealer)
                If Not objDealer Is Nothing Then
                    _newUserInfo = objDealer.DealerCode & "-" & UserInfo.Substring(6)
                Else
                    _newUserInfo = "Invalid Dealer-" & UserInfo.Substring(6)
                End If
            ElseIf (UserInfo.Length > 6) AndAlso (UserInfo.ToUpper.Contains("_IF_")) Then
                Dim firstUnderscoreIndex As Integer = UserInfo.IndexOf("_")
                _newUserInfo = UserInfo.Remove(firstUnderscoreIndex, 1).Insert(firstUnderscoreIndex, "-")
            Else
                If UserInfo.Trim = "" Then
                    _newUserInfo = ""
                Else
                    _newUserInfo = "Invalid Dealer-" & UserInfo
                End If
            End If

            Return _newUserInfo
        End Function

        Public ReadOnly Property TotalPosting() As Integer
            Get
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumPost), "UserInfo.ID", MatchType.Exact, ID))
                criterias.opAnd(New Criteria(GetType(ForumPost), "isHeader", MatchType.Exact, 0))

                Dim m_ForumPostMapper As IMapper
                m_ForumPostMapper = MapperFactory.GetInstance.GetMapper(GetType(ForumPost).ToString)
                Dim ForumPostList As ArrayList = m_ForumPostMapper.RetrieveByCriteria(criterias)
                Return ForumPostList.Count
            End Get

        End Property

        Public Function IsAlreadyBranch() As Boolean
            If IsNothing(Me._dealerBranch) Then
                Return False
            Else
                If Me._dealerBranch.ID = 0 Then
                    Return False
                End If

            End If
            Return True
        End Function
#End Region

#Region "Custom Properties"

        Public ReadOnly Property RegistrationStatus() As String
            Get
                If IsNothing(Me.UserProfile) Then
                    Return "Belum Registrasi"
                End If
                If Me.UserProfile.ID <= 0 Then
                    Return "Belum Registrasi"
                End If
                If Me.UserProfile.ID > 0 Then
                    If Me.UserProfile.TransitionHP <> String.Empty Then
                        Return "Transisi"
                    End If
                    If Me.UserProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register And Me.UserProfile.ActivationStatus = EnumSE.ActivationCodeStatus.Active Then
                        Return "Teraktivasi"
                    End If
                    If Me.UserProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register And Me.UserProfile.ActivationStatus <> EnumSE.ActivationCodeStatus.Active Then
                        Return "Belum Aktivasi"
                    End If
                    If Me.UserProfile.RegistrationStatus <> EnumSE.RegistrationStatus.Register Then
                        Return "Reset Registrasi"
                    End If
                End If

            End Get
        End Property

        Public ReadOnly Property UserStatDesc() As String
            Get
                Dim status As String = "(" & EnumUserStatus.UserStatDesc(UserStatus) & ")" & "<BR>" & RegistrationStatus
                Return status
            End Get
        End Property

#End Region

    End Class
End Namespace

