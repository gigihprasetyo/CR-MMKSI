Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Math
Imports System.IO
Imports System.Text

#Region "Custom NameSpace"
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Lib
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.PageHelper
Imports System.Text.RegularExpressions
Imports KTB.DNet.BusinessFacade.UserManagement
Imports System.Threading.Tasks
Imports System.Collections.Generic

#End Region


Public Class FrmUserActivationOTP
    Inherits System.Web.UI.Page
    Private ss As SessionHelper = New SessionHelper

   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'btnReset.Visible = False
        txtTimer.Text = ""
        If Not Page.IsPostBack Then
            ss.SetSession("OTPReload", True)
        End If
        btnReset.Attributes.Add("style", "visibility: hidden")
        OTPPage.ActivityType = Request.QueryString("Proses")
        txthome.text = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd((New Char() {"/"})) + "/"
    End Sub

    Private Sub SendSMS(ByVal hp As String, ByVal message As String)
        'Sms.Sendto(hp, message)

        Dim otpfunc As New OTPFunction

        otpfunc.SendSMSNotif(hp, message)
        If (Not otpfunc.boolReturn) Then
           
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Dim objuser As UserInfo = ss.GetSession("LOGINUSERINFO")

        Dim otpFunction As New OTPFunction
        Dim result As Integer = otpFunction.func_generateCodeOTP(objuser.HandPhone, objuser.ID)
        If (Not otpFunction.boolReturn) Then
            MessageBox.Show("Pengiriman SMS Gagal Silakan Hubungi Administrator Anda")
            Return
        End If
        OTPPage.StatusControl = True
    End Sub

    'Private Sub lbtnHome_Click(sender As Object, e As EventArgs) Handles lbtnHome.Click
    '    'Response.Redirect("Login.aspx", False)
    '    Server.Transfer("Login.aspx", False)
    'End Sub

End Class