Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement

Public Class UserEmailValidation
    Public Sub New()

    End Sub

    Private _user As IPrincipal

    Public Sub GenerateEmailValidationButton(ByRef panel As Panel, ByVal user As System.Security.Principal.IPrincipal)
        _user = user
        Dim lbtnEmailValidator As LinkButton = New LinkButton
        lbtnEmailValidator.Text = "Validate Your Email"
        AddHandler lbtnEmailValidator.Click, AddressOf Me.ValidateEmail
        panel.Controls.Add(lbtnEmailValidator)


    End Sub

    Private Sub ValidateEmail(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'MessageBox.Show("xx")
        Dim sessHelp As KTB.DNet.Utility.SessionHelper = New KTB.DNet.Utility.SessionHelper
        Dim objUserInfo As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
        objUserInfo.EmailValidation = 1
        Dim result As Integer = New UserInfoFacade(_user).Update(objUserInfo)

        If result = -1 Then
            MessageBox.Show("Validasi Email Gagal")
        Else
            MessageBox.Show("Validasi Email Berhasil")
            sessHelp.SetSession("LOGINUSERINFO", objUserInfo)
            CType(sender, LinkButton).Visible = False
        End If

    End Sub

End Class
