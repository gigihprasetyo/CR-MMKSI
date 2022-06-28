Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports System.Web
Imports System.Web.UI


Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Lib
Imports System.Security.Principal

Partial Public Class Login
    Inherits Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            RefreshCaptch()
            Session("SPK") = Nothing
        End If
    End Sub

    Protected Sub LogIn(sender As Object, e As EventArgs)
        If IsValid Then

            If IsNothing(Session("CaptchaImageText")) Then
                RefreshCaptch()
                ErrorMessage.Visible = True
                FailureText.Text = "Kode Captcha Salah"
                Return
            Else
                If Session("CaptchaImageText").ToString().ToLower() <> CodeNumberTextBox.Text.ToLower() Then
                    '    RefreshCaptch()
                    ErrorMessage.Visible = True
                    FailureText.Text = "Kode Captcha Salah"
                    Return
                End If
            End If
            ' Validate the user password
            Dim crtFP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            crtFP.opAnd(New Criteria(GetType(SPKHeader), "SPKNumber", MatchType.Exact, txtSPK.Text.Trim()))
            crtFP.opAnd(New Criteria(GetType(SPKHeader), "ValidationKey", MatchType.Exact, Password.Text.Trim()))

            Dim arlFP As New ArrayList
            arlFP = New SPKHeaderFacade(User).Retrieve(crtFP)

            If Not IsNothing(arlFP) AndAlso arlFP.Count > 0 Then
                Session("SPK") = CType(arlFP(0), SPKHeader)

                Try
                    Dim User As New System.Security.Principal.GenericPrincipal(New GenericIdentity("IndentSPK"), Nothing)
                    Dim objSPK As SPKIndentLog = New SPKIndentLog()
                    objSPK.SPKNumber = txtSPK.Text.Trim()
                    Dim i = New SPKIndentLogFacade(User).Insert(objSPK)
                    HttpContext.Current.User = New GenericPrincipal(New GenericIdentity(objSPK.SPKNumber), Nothing)

                    FormsAuthentication.SetAuthCookie(objSPK.SPKNumber, True)


                Catch ex As Exception
                    ErrorMessage.Visible = True
                    FailureText.Text = ex.Message.ToString()
                End Try
                Response.Redirect("Information.aspx")
            Else
                RefreshCaptch()
                ErrorMessage.Visible = True
                FailureText.Text = "Kode Booking tidak terdaftar / Password Salah"
            End If
        End If
    End Sub


    Private Function GenerateRandomCode() As String
        Try
            Dim generator As RandomGenerator = New RandomGenerator
            Return generator.GenarateRandomCharacterOnly(5).ToUpper

        Catch ex As Exception
            Return "ACFDE"
        End Try
    End Function



    Private Sub RefreshCaptch()
        CodeNumberTextBox.Text = String.Empty
        Session("CaptchaImageText") = GenerateRandomCode()
        captchaImg.ImageUrl = "JpegImage.aspx"
    End Sub

    Protected Sub f5_Click(sender As Object, e As EventArgs) Handles f5.Click
        RefreshCaptch()
    End Sub
End Class
