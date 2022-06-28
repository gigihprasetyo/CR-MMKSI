Imports KTB.DNet.Utility
Imports System.Text
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade

Public Class PopUpTimeWork
    Inherits System.Web.UI.Page
    Private m_bInputPrivilege As Boolean = False
    Private sessHelper As SessionHelper = New SessionHelper
    Private objDealer As New Dealer
    Private crit As CriteriaComposite
    Private stallWorkingTimeFacade As StallWorkingTimeFacade = New StallWorkingTimeFacade(User)
    Private dealerFacade As DealerFacade = New DealerFacade(User)
    Private stallMasterFacade As StallMasterFacade = New StallMasterFacade(User)
    Private standardCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)
    'Private mode As String

#Region "Event Handler"
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        If Not IsPostBack Then
            InitVisitTypeDdl()

            Dim obj As StallWorkingTime = stallWorkingTimeFacade.Retrieve(CInt(Request.QueryString("id").ToString))
            hdID.Value = obj.ID
            lblTgl.Text = obj.Tanggal.ToString("dd MMMM yyyy")
            txtOptFrom.Text = obj.TimeStart.ToString("HH:mm")
            txtOptTo.Text = obj.TimeEnd.ToString("HH:mm")
            txtBreakFrom.Text = obj.RestTimeStart.ToString("HH:mm")
            txtBreakTo.Text = obj.RestTimeEnd.ToString("HH:mm")
            lblStall.Text = obj.StallMaster.StallCodeDealer
            If obj.IsHoliday = 1 Then
                rbYes.Checked = True
            Else
                rbNo.Checked = True
            End If

            ddlVisitType.Items.FindByValue(obj.VisitType).Selected = True
            txtNotes.Text = obj.Notes

            If obj.StallMaster.StallType = CInt(EnumStallMaster.TipeStall.Washing) Then
                ddlVisitType.Enabled = False
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        If Not Validate() Then
            Exit Sub
        End If

        Dim result As Integer = 0
        Dim obj As StallWorkingTime = New StallWorkingTime

        PopulateData(obj)
        result = stallWorkingTimeFacade.Update(obj)

        If result > 0 Then
            RegisterStartupScript("Close", "<script>onSuccess();</script>")
            Return
        Else
            MessageBox.Show("Simpan gagal.")
        End If
    End Sub
#End Region
    
#Region "Custom Method"
    Private Function PopulateData(ByRef obj As StallWorkingTime)
        obj = stallWorkingTimeFacade.Retrieve(CInt(hdID.Value))
        obj.Tanggal = CDate(lblTgl.Text)
        obj.TimeStart = CType(String.Format("{0} {1}", obj.Tanggal.ToShortDateString, txtOptFrom.Text), DateTime)
        obj.TimeEnd = CType(String.Format("{0} {1}", obj.Tanggal.ToShortDateString, txtOptTo.Text), DateTime)
        obj.RestTimeStart = CType(String.Format("{0} {1}", obj.Tanggal.ToShortDateString, txtBreakFrom.Text), DateTime)
        obj.RestTimeEnd = CType(String.Format("{0} {1}", obj.Tanggal.ToShortDateString, txtBreakTo.Text), DateTime)
        obj.StallMaster = stallMasterFacade.RetrieveStallCodeDealer(lblStall.Text, objDealer.ID)
        obj.Dealer = dealerFacade.Retrieve(objDealer.ID)
        obj.IsHoliday = IIf(rbYes.Checked, 1, 0)
        obj.VisitType = CInt(ddlVisitType.SelectedValue)
        obj.Notes = txtNotes.Text
    End Function

    Private Sub CheckPrivilege()
        m_bInputPrivilege = SecurityProvider.Authorize(Context.User, SR.WorkingConfigurationTime_Input_Privilage)

        If Not m_bInputPrivilege Or Request.QueryString("dealerCode").ToString <> objDealer.DealerCode Then
            txtOptFrom.Enabled = False
            txtOptTo.Enabled = False
            txtBreakFrom.Enabled = False
            txtBreakTo.Enabled = False
            rbYes.Enabled = False
            rbNo.Enabled = False
            btnSave.Visible = False
        End If
    End Sub

    Private Function Validate() As Boolean
        Dim msgErr As String = String.Empty
        If String.IsNullorEmpty(txtOptFrom.Text) Or String.IsNullorEmpty(txtOptTo.Text) Then
            msgErr = "Waktu operasional dari atau sampai harus diisi."
        ElseIf Convert.ToDateTime(txtOptFrom.Text) > Convert.ToDateTime(txtOptTo.Text) Then
            msgErr = "Waktu operasional dari tidak boleh lebih besar dari waktu operasional sampai."
        ElseIf String.IsNullorEmpty(txtBreakFrom.Text) Or String.IsNullorEmpty(txtBreakTo.Text) Then
            msgErr = "Waktu istirahat dari atau sampai harus diisi."
        ElseIf Convert.ToDateTime(txtBreakFrom.Text) > Convert.ToDateTime(txtBreakTo.Text) Then
            msgErr = "Waktu istirahat dari tidak boleh lebih besar dari waktu istirahat sampai."
            'ElseIf ddlVisitType.SelectedIndex = 0 Then
            '    msgErr = "Servis harus dipilih."
        End If

        If Not String.IsNullorEmpty(msgErr) Then
            MessageBox.Show(msgErr)
        End If

        Return String.IsNullorEmpty(msgErr)
    End Function

    Private Sub InitVisitTypeDdl()
        ddlVisitType.Items.Clear()

        crit = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "StallWorkingTime.VisitType"))

        Dim results As ArrayList = standardCodeFacade.Retrieve(crit)

        With ddlVisitType.Items
            For Each obj As StandardCode In results
                .Add(New ListItem(obj.ValueDesc, obj.ValueId))
            Next
        End With
    End Sub
#End Region
    
End Class