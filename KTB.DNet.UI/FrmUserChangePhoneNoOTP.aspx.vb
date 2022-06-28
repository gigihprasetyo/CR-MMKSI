Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Math
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports Newtonsoft.Json

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
Imports KTB.DNet.WebAPI.SMSGetway
#End Region

Public Class FrmUserChangePhoneNoOTP
    Inherits System.Web.UI.Page
    Private ss As SessionHelper = New SessionHelper

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objUsIn As UserInfo
        objUsIn = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        If Not String.IsNullOrEmpty(objUsIn.HandPhone) Then
            lblNomorHPLama.Text = objUsIn.HandPhone
        Else
            lblNomorHPLama.Text = objUsIn.Telephone
        End If

        If Page.IsPostBack Then
            If CType(ViewState("vsProcess"), String) = "True" Then
                objUsIn = New UserInfoFacade(User).Retrieve(objUsIn.ID)
                If Not String.IsNullOrEmpty(objUsIn.HandPhone) Then
                    lblNomorHPLama.Text = objUsIn.HandPhone
                Else
                    lblNomorHPLama.Text = objUsIn.Telephone
                End If

            Else

            End If
        Else
            ss.SetSession("OTPReload", False)
            otpdiv.Visible = False
            divPhoneNumber.Visible = True
        End If

        btnReset.Attributes.Add("style", "visibility: hidden")

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        'generateCodeOTP()
        Dim otpfunc As New OTPFunction

        otpfunc.generateCodeOTP(CType(ViewState("PhoneNumber"), String))

        If (Not otpfunc.boolReturn) Then
            MessageBox.Show("Pengiriman SMS Gagal Silakan Hubungi Administrator Anda")
            Return
        End If
        OTPPage.StatusControl = True
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim status As Boolean = False

        Dim phonenumner As New Regex("^[0-9]{4}$")
        If Not phonenumner.IsMatch(txtPhoneNo1.Text) Then
            MessageBox.Show("Format Nomor HP tidak Valid")
            status = False
            ViewState("vsProcess") = CType("False", String)
            Return
        Else
            status = True
        End If

        If Not phonenumner.IsMatch(txtPhoneNo2.Text) Then
            MessageBox.Show("Format Nomor HP tidak Valid")
            status = False
            ViewState("vsProcess") = CType("False", String)
            Return
        Else
            status = True
        End If

        phonenumner = New Regex("^[0-9]")
        If Not phonenumner.IsMatch(txtPhoneNo3.Text) Then
            MessageBox.Show("Format Nomor HP tidak Valid")
            status = False
            ViewState("vsProcess") = CType("False", String)
            Return
        Else
            status = True
        End If

        If status = True Then

            ViewState("vsProcess") = CType("True", String)
            ViewState("PhoneNumber") = CType(txtPhoneNo1.Text + txtPhoneNo2.Text + txtPhoneNo3.Text, String)
            Dim otpfunc As New OTPFunction


            otpfunc.generateCodeOTPChangeNoPhone(CType(ViewState("PhoneNumber"), String))
            If (Not otpfunc.boolReturn) Then
                MessageBox.Show("Pengiriman SMS Gagal Silakan Hubungi Administrator Anda")
                Return
            End If

            otpdiv.Visible = True

            btnSimpan.Enabled = False

            ss.SetSession("OTPReload", True)

        End If

    End Sub

    Public Function CalculateTimeDifference(startDate As DateTime, endDate As DateTime) As String
        Dim days As Integer = 0
        Dim hours As Integer = 0
        Dim mins As Integer = 0
        Dim secs As Integer = 0
        Dim final As String = String.Empty
        If endDate > startDate Then
            days = (endDate - startDate).Days
            hours = (endDate - startDate).Hours
            mins = (endDate - startDate).Minutes
            secs = (endDate - startDate).Seconds
            final = String.Format("{0} days {1} hours {2} mins {3} secs", days, hours, mins, secs)
        End If
        Return final
    End Function
    
    Private Sub txtTimer_TextChanged(sender As Object, e As EventArgs) Handles txtTimer.TextChanged
        If txtTimer.Text = "0" Then
            btnReset.Visible = True
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("UserManagement/frmSecurityPage.aspx", True)
    End Sub
End Class