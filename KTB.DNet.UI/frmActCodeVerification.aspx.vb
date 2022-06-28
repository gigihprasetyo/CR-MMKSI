Imports System
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Security
Imports System.Text
Imports System.Security.Principal
Imports System.Web.Security
Imports System.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Lib


Public Class frmActCodeVerification
    Inherits System.Web.UI.Page
    Protected WithEvents btnProsess As System.Web.UI.WebControls.Button
    Protected WithEvents txtKodeAktivasi As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Private ss As SessionHelper = New SessionHelper
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents LinkButton1 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents MyImage As System.Web.UI.WebControls.Image



#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    'Private authenticationProvider As IAuthenticationProvider
    'Private rolesProvider As IRolesProvider
    Private identity As IIdentity

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            LoadTemporaryUser()
        End If
        btnKembali.Attributes.Add("onclick", "return confirm('Anda yakin akan keluar dari halaman ini ?');")
    End Sub

    Private Sub LoadTemporaryUser()
        Dim objTempUser As UserInfo = CType(Session.Item("TEMPORARYUSER"), UserInfo)
        If Not objTempUser Is Nothing Then
            Dim objTempUserProfile As UserProfile = CType(Session.Item("TEMPORARYUSERPROFILE"), UserProfile)
            MyImage.ImageUrl = "WebResources/GetImage.aspx?title=" & objTempUserProfile.ImageDescription & " &id=" & objTempUserProfile.ImageID
        Else
            Response.Redirect("login.aspx")
        End If
    End Sub

    Private Function DoValidateActivationCode(ByVal clientActCode As String) As Boolean
        Dim objTempUser As UserInfo = CType(Session.Item("TEMPORARYUSER"), UserInfo)
        Dim objTempUserProfile As UserProfile = CType(Session.Item("TEMPORARYUSERPROFILE"), UserProfile)
        Dim objTempBIngo As Bingo = CType(Session.Item("BINGO"), Bingo)
        Dim img As Integer = Session.Item("IMAGE")
        Dim actCode As String = Session.Item("ACTCODE")
        Dim valid As Boolean = True

        If objTempUser Is Nothing Then
            valid = False
        End If
        If objTempUserProfile Is Nothing Then
            valid = False
        End If

        If objTempBIngo Is Nothing Then
            valid = False
        End If

        If actCode = String.Empty Then
            valid = False
        End If

        If valid Then
            If txtKodeAktivasi.Text.Trim <> String.Empty Then
                If txtKodeAktivasi.Text.Trim.ToUpper = actCode.Trim.ToUpper Then
                    Dim _userInfoFacade As UserInfoFacade = New UserInfoFacade(User)
                    objTempUserProfile.Bingo = objTempBIngo
                    objTempUserProfile.ActivationCode = clientActCode
                    objTempUserProfile.ActivationSentTime = Now
                    objTempUserProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register
                    objTempUserProfile.ActivationStatus = EnumSE.ActivationCodeStatus.Active
                    objTempUser = New UserInfoFacade(User).Retrieve(objTempUser.ID)
                    Dim i As Integer = _userInfoFacade.RegisterUser(objTempUser, objTempUserProfile)
                    objTempUser = New UserInfoFacade(User).Retrieve(objTempUser.ID)
                    ss.SetSession("LOGINUSERINFO", objTempUser)
                Else
                    MessageBox.Show("Kode Aktivasi tidak sesuai")
                    Return False
                End If
            Else
                MessageBox.Show("Kode Aktivasi tidak boleh kosong")
                Return False
            End If
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub btnProsess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProsess.Click
        If DoValidateActivationCode(txtKodeAktivasi.Text.Trim.ToUpper) Then
            Session.Remove("TEMPORARYUSER")
            Session.Remove("TEMPORARYUSERPROFILE")
            Session.Remove("ACTCODE")
            Session.Remove("BINGO")
            Session.Remove("IMAGE")
            Response.Redirect("UserManagement\frmVerification.aspx")
        End If
    End Sub

    Private Sub LinkButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        FormsAuthentication.SignOut()
        Session.RemoveAll()
        Response.Redirect("login.aspx")
    End Sub

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        Response.Redirect("frmSecondLogin.aspx")
    End Sub

    Private Sub LinkButton1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        FormsAuthentication.SignOut()
        Session.RemoveAll()
        Response.Redirect("login.aspx")
    End Sub
End Class
