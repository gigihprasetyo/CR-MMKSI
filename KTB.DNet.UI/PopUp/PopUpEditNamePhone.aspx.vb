Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports System.IO
Imports System.Text
Imports KTB.DNet.WebApi.Models
Imports KTB.DNet.WebApi.Models.SalesForce
Imports System.Collections.Generic
Imports System.Linq
Public Class PopUpEditNamePhone
    Inherits System.Web.UI.Page

    Private sessHelper As SessionHelper = New SessionHelper
    Private crit As CriteriaComposite
    Private objDealer As New Dealer
    Private oLoginUser As New UserInfo
    Private standardCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)
    Private serviceBookingFacade As ServiceBookingFacade = New ServiceBookingFacade(User)
    Private appConfFacade As AppConfigFacade = New AppConfigFacade(User)
    Private ccRFacade As CustomerCaseResponseFacade = New CustomerCaseResponseFacade(User)
    
#Region " Event Handler"
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not String.IsNullorEmpty(Request.QueryString("id")) Then
                sessHelper.SetSession("id", Request.QueryString("id"))
                sessHelper.SetSession("mode", Request.QueryString("mode"))
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim intID As Integer = CInt(sessHelper.GetSession("id"))
        Dim strMode As String = sessHelper.GetSession("mode")

        If (txtName.Text = "" Or txtPhone.Text = "") Then
            MessageBox.Show("Semua Data Wajib Diisi.")
            Return
        End If

        'If oCrp.ProfileHeader.Code = "NO_HP" Then
        If (txtPhone.Text.Length < 8 Or txtPhone.Text.Length > 16) OrElse txtPhone.Text.Substring(0, 2) = "00" Then
            MessageBox.Show("Panjang karakter No HP harus lebih dari 8 digit dan kurang dari 16 digit")
            Exit Sub
        End If

        If Not (txtPhone.Text.StartsWith("08")) Then
            MessageBox.Show("No HP tidak benar. isi dengan format 08xxx")
            Exit Sub
        End If

        If Not IsNumeric(txtPhone.Text) Then
            MessageBox.Show("No HP tidak benar. isi dengan angka")
            Exit Sub
        End If

        'End If

        Dim objServiceReminder As ServiceReminder = New ServiceReminder
        Dim criteriasa As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ServiceReminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriasa.opAnd(New Criteria(GetType(ServiceReminder), "ID", MatchType.Exact, intID))
        Dim arrServiceReminder As ArrayList = New ServiceReminderFacade(User).Retrieve(criteriasa)
        If (arrServiceReminder.Count > 0) Then
            For i As Integer = 0 To arrServiceReminder.Count - 1
                If Not IsNothing(arrServiceReminder) AndAlso arrServiceReminder.Count > 0 Then
                    objServiceReminder = CType(arrServiceReminder(i), ServiceReminder)
                    If (strMode = "Pemilik") Then
                        objServiceReminder.CustomerName = txtName.Text.ToString()
                        objServiceReminder.CustomerPhoneNumber = txtPhone.Text.ToString()
                    Else
                        objServiceReminder.ContactPersonName = txtName.Text.ToString()
                        objServiceReminder.ContactPersonPhoneNumber = txtPhone.Text.ToString()
                    End If
                    Dim nResult = New ServiceReminderFacade(User).Update(objServiceReminder)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)

                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        RegisterStartupScript("Close", "<script>onSuccess();</script>")
                        Return
                    End If
                End If
            Next

        End If
    End Sub

#End Region

    

End Class