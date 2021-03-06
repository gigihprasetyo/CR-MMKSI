#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.Configuration
#End Region

Public Class FrmFreePPh
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents chkPeriod As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblCreatedBy As System.Web.UI.WebControls.Label
    Protected WithEvents ddlOrgType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents icStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtNoSurat As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents btnBaru As System.Web.UI.WebControls.Button
    Protected WithEvents dtgMain As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblKTBApprovalBy As System.Web.UI.WebControls.Label
    Protected WithEvents lblApprovedBy As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblIsi As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variables"
    Private sHelper As SessionHelper = New SessionHelper
#End Region

#Region "Custom Methods"

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.Bebas_pph_lihat_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Bebas PPh")
        End If
        If Not SecurityProvider.Authorize(context.User, SR.Bebas_pph_simpan_privilege) Then
            btnSimpan.Visible = False
        End If
    End Sub

    Private Sub Initialization()
        Dim oD As Dealer = CType(Session("DEALER"), Dealer)

        BindDdlOrgType()
        ddlOrgType.Enabled = False
        If oD.Title = EnumDealerTittle.DealerTittle.KTB Then
            ddlOrgType.SelectedValue = 1
            txtDealerCode.Enabled = True
            txtDealerCode.Text = ""
            btnBaru.Visible = False
            btnSimpan.Visible = False
            btnBatal.Visible = False
        Else
            ddlOrgType.SelectedValue = 2
            txtDealerCode.Enabled = False
            txtDealerCode.Text = oD.DealerCode
            lblSearchDealer.Visible = False
        End If
        viewstate.Add("EditMode", "List")
        viewstate.Add("FreePPhID", -1)
        LockControl()

        sHelper.SetSession("SQLOrderField", "ID")
        sHelper.SetSession("SQLOrderDirection", Sort.SortDirection.ASC)
    End Sub

    Private Sub BindDdlOrgType()
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        If companyCode = "MMC" Then
            With ddlOrgType
                .Items.Clear()
                .Items.Add(New ListItem("MKS", 1))
                .Items.Add(New ListItem("Dealer", 2))
            End With
        Else
            With ddlOrgType
                .Items.Clear()
                .Items.Add(New ListItem("KTB", 1))
                .Items.Add(New ListItem("Dealer", 2))
            End With
        End If
        
    End Sub

    Private Sub LockControl()
        Dim strMode As String = CType(viewstate.Item("EditMode"), String).ToUpper
        lblIsi.Visible = False
        If strMode = "List".ToUpper Then
            btnBaru.Enabled = True
            btnSimpan.Enabled = False
            btnBatal.Enabled = False
            btnCari.Enabled = True
            chkPeriod.Visible = True
            'ClearData()
        ElseIf strMode = "Add".ToUpper Then
            btnBaru.Enabled = False
            btnSimpan.Enabled = True
            btnBatal.Enabled = True
            btnCari.Enabled = False
            chkPeriod.Visible = False
            InitNewData()
        ElseIf strMode = "Edit".ToUpper Then
            btnBaru.Enabled = False
            btnSimpan.Enabled = True
            btnBatal.Enabled = True
            btnCari.Enabled = False
            chkPeriod.Visible = False
            Dim oD As Dealer = CType(Session("DEALER"), Dealer)
            If oD.Title = EnumDealerTittle.DealerTittle.KTB Then 'add with privilege filtering
                btnCari.Enabled = True
            End If
        ElseIf strMode = "View".ToUpper Then
            btnBaru.Enabled = False
            btnSimpan.Enabled = False
            btnBatal.Enabled = False
            btnCari.Enabled = True
            chkPeriod.Visible = False
            Dim oD As Dealer = CType(Session("DEALER"), Dealer)
            If oD.Title = EnumDealerTittle.DealerTittle.KTB Then 'add with privilege filtering
                btnCari.Enabled = True
            End If
        End If
    End Sub

    Private Sub InitNewData()
        Dim oUI As UserInfo = CType(sHelper.GetSession("LOGINUSERINFO"), UserInfo)

        ddlOrgType.SelectedValue = 2
        txtDealerCode.Text = oUI.Dealer.DealerCode
        icStart.Value = Now
        icEnd.Value = Now
        lblCreatedBy.Text = oUI.Dealer.DealerCode & " - " & oUI.UserName
        lblApprovedBy.Text = ""
    End Sub

    Private Sub ClearData()
        icStart.Value = Now
        icEnd.Value = Now
        txtNoSurat.Text = ""
        lblCreatedBy.Text = ""
        lblApprovedBy.Text = ""
    End Sub

    Private Sub BindDTG()
        Dim crtFP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreePPh), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim objFPFac As FreePPhFacade = New FreePPhFacade(User)
        Dim arlFP As New ArrayList
        Dim srtFP As New SortCollection

        If txtDealerCode.Text.Trim <> "" Then
            crtFP.opAnd(New Criteria(GetType(FreePPh), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
        End If
        If chkPeriod.Checked Then
            'crtFP.opAnd(New Criteria(GetType(FreePPh), "PeriodStart", MatchType.LesserOrEqual, Format(icStart.Value, "MM/dd/yyyy")))
            'crtFP.opAnd(New Criteria(GetType(FreePPh), "PeriodEnd", MatchType.GreaterOrEqual, Format(icEnd.Value, "MM/dd/yyyy")))
            crtFP.opAnd(New Criteria(GetType(FreePPh), "PeriodStart", MatchType.GreaterOrEqual, Format(icStart.Value, "MM/dd/yyyy")))
            crtFP.opAnd(New Criteria(GetType(FreePPh), "PeriodStart", MatchType.LesserOrEqual, Format(icEnd.Value, "MM/dd/yyyy")))
        End If
        If txtNoSurat.Text.Trim <> "" Then
            crtFP.opAnd(New Criteria(GetType(FreePPh), "LetterNumber", MatchType.[Partial], txtNoSurat.Text))
        End If
        srtFP.Add(New Sort(GetType(FreePPh), sHelper.GetSession("SQLOrderField"), sHelper.GetSession("SQLOrderDirection")))
        arlFP = objFPFac.Retrieve(crtFP, srtFP)
        dtgMain.DataSource = arlFP
        dtgMain.DataBind()

    End Sub

    Private Sub CreateSQLOrder(ByVal sField As String)
        If sField.ToUpper = CType(sHelper.GetSession("SQLOrderField"), String).ToUpper Then
            If sHelper.GetSession("SQLOrderDirection") = Sort.SortDirection.ASC Then
                sHelper.SetSession("SQLOrderDirection", Sort.SortDirection.DESC)
            Else
                sHelper.SetSession("SQLOrderDirection", Sort.SortDirection.ASC)
            End If
        Else
            sHelper.SetSession("SQLOrderField", sField)
            sHelper.SetSession("SQLOrderDirection", Sort.SortDirection.ASC)
        End If
    End Sub

    Private Sub ShowDetail(ByVal ID As Integer)
        Dim objFPFac As FreePPhFacade = New FreePPhFacade(User)
        Dim objFP As FreePPh

        objFP = objFPFac.Retrieve(ID)
        If Not IsNothing(objFP) AndAlso objFP.ID > 0 Then
            viewstate.Item("FreePPhID") = ID
            icStart.Value = objFP.PeriodStart
            icEnd.Value = objFP.PeriodEnd
            txtNoSurat.Text = objFP.LetterNumber
            lblCreatedBy.Text = objFP.Dealer.DealerCode & " - " & objFP.ProposedBy & " , pada tanggal " & Format(objFP.ProposedDate, "dd MMM yyyy")
            lblApprovedBy.Text = objFP.ProposedBy & " , pada tanggal " & Format(objFP.ProposedDate, "dd MMM yyyy")
            If objFP.Status = enumFreePPhStatus.FreePPhStatus.Approved Then
                btnSimpan.Enabled = False
            End If
        Else
            ClearData()
        End If
    End Sub

    Private Sub UpdateStatus(ByVal ID As Integer, ByVal NewStatus As Integer)
        Dim objFPFac As FreePPhFacade = New FreePPhFacade(User)
        Dim objFP As FreePPh

        objFP = objFPFac.Retrieve(ID)
        If Not IsNothing(objFP) AndAlso objFP.ID > 0 Then
            If objFP.Status = enumFreePPhStatus.FreePPhStatus.Approved Then
                If NewStatus <> enumFreePPhStatus.FreePPhStatus.Approved Then
                    'MessageBox.Show("Update Status gagal, Bebas PPH sudah di-approve")
                    'Exit Sub
                End If
            End If

            Dim objUser As UserInfo = CType(Session("LOGINUSERINFO"), UserInfo)
            objFP.KTBApprovalBy = objUser.UserName
            objFP.KTBApprovalDate = Date.Now

            objFP.Status = NewStatus

            objFPFac.Update(objFP)
            If NewStatus = enumFreePPhStatus.FreePPhStatus.Approved Then
                UpdateCHAndPO(objFP)
            End If
        End If
    End Sub

    Private Sub DeleteData(ByVal ID As Integer)
        Dim objFPFac As FreePPhFacade = New FreePPhFacade(User)
        Dim objFP As FreePPh

        objFP = objFPFac.Retrieve(ID)
        If Not IsNothing(objFP) AndAlso objFP.ID > 0 Then
            objFPFac.Delete(objFP)
        End If
    End Sub

    Private Sub UpdateCHAndPO(ByVal objFP As FreePPh)
        Dim objD As Dealer = objFP.Dealer ' CType(sHelper.GetSession("DEALER"), Dealer)
        Dim objPOFac As POHeaderFacade = New POHeaderFacade(User)
        Dim crtPO As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlPO As New ArrayList
        Dim objCHFac As ContractHeaderFacade = New ContractHeaderFacade(User)
        Dim objCH As ContractHeader

        crtPO.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.ID", MatchType.Exact, objD.ID))
        crtPO.opAnd(New Criteria(GetType(POHeader), "CreatedTime", MatchType.GreaterOrEqual, Format(objFP.PeriodStart, "MM/dd/yyyy")))
        'crtPO.opAnd(New Criteria(GetType(POHeader), "CreatedTime", MatchType.LesserOrEqual, Format(objFP.PeriodEnd, "MM/dd/yyyy")))
        'Start  :by dna;on:20120910;for:yurike;bug
        crtPO.opAnd(New Criteria(GetType(POHeader), "CreatedTime", MatchType.Lesser, Format(objFP.PeriodEnd.AddDays(1), "MM/dd/yyyy 00:00:00")))
        'End    :by dna;on:20120910;for:yurike;bug
        crtPO.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.Exact, CType(enumStatusPO.Status.Baru, Short).ToString))
        arlPO = objPOFac.Retrieve(crtPO)
        For Each oPO As POHeader In arlPO
            oPO.FreePPh22Indicator = 0
            objPOFac.Update(oPO)

            objCH = oPO.ContractHeader
            objCH.FreePPh22Indicator = 0
            objCHFac.Update(objCH)
        Next
    End Sub
#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckUserPrivilege()
        If Not IsPostBack() Then
            Initialization()
        End If
    End Sub

    Private Sub btnBaru_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBaru.Click
        viewstate.Item("EditMode") = "Add"
        LockControl()
    End Sub

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        BindDTG()
        viewstate.Item("EditMode") = "List"
        LockControl()
        ClearData()
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        BindDTG()
        viewstate.Item("EditMode") = "List"
        LockControl()
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim objFPFac As FreePPhFacade = New FreePPhFacade(User)
        Dim objFP As FreePPh
        Dim objD As Dealer
        Dim objUI As UserInfo = CType(sHelper.GetSession("LOGINUSERINFO"), UserInfo)
        Dim IsNew As Boolean = False

        If txtNoSurat.Text.Trim = "" Then
            lblIsi.Visible = True
            MessageBox.Show("No surat harus diisi")
            Exit Sub
        End If
        'Data Validation
        'Data Saving
        If CType(viewstate.Item("EditMode"), String).ToUpper = "Edit".ToUpper Then
            objFP = objFPFac.Retrieve(CType(viewstate.Item("FreePPhID"), Integer))
            IsNew = False
        Else
            objFP = New FreePPh
            IsNew = True
        End If
        objD = New DealerFacade(User).Retrieve(txtDealerCode.Text)
        objFP.DealerID = objD.ID
        objFP.PeriodStart = icStart.Value
        objFP.PeriodEnd = icEnd.Value
        objFP.LetterNumber = txtNoSurat.Text
        objFP.ProposedBy = objUI.UserName
        objFP.ProposedDate = CDate(Now)
        Try
            If IsNew Then
                objFPFac.Insert(objFP)
            Else
                objFPFac.Update(objFP)
            End If
            MessageBox.Show(SR.SaveSuccess)
        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)
        Finally
            'Set to Normal Mode (koyo game ae cak)
            viewstate.Item("EditMode") = "List"
            LockControl()
            ClearData()
            BindDTG()
        End Try
    End Sub

    Private Sub dtgMain_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgMain.SortCommand
        CreateSQLOrder(e.SortExpression)
        BindDTG()
    End Sub

    Private Sub dtgMain_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.ItemCommand
        Dim lblID As Label = e.Item.FindControl("lblID")
        If e.CommandName.ToUpper = "Edit".ToUpper Then
            Dim lblStatusOri As Label = e.Item.FindControl("lblStatusOri")

            viewstate.Item("EditMode") = "Edit"
            LockControl()
            ShowDetail(CType(lblID.Text, Integer))
        ElseIf e.CommandName.ToUpper = "View".ToUpper Then
            Dim lblStatusOri As Label = e.Item.FindControl("lblStatusOri")

            viewstate.Item("EditMode") = "View"
            LockControl()
            ShowDetail(CType(lblID.Text, Integer))
        ElseIf e.CommandName.ToUpper = "Approve".ToUpper Then
            'add privilege filtering 
            UpdateStatus(CType(lblID.Text, Integer), enumFreePPhStatus.FreePPhStatus.Approved)
            viewstate.Item("EditMode") = "List"
            LockControl()
            BindDTG()
        ElseIf e.CommandName.ToUpper = "Reject".ToUpper Then
            'add privilege filtering 
            UpdateStatus(CType(lblID.Text, Integer), enumFreePPhStatus.FreePPhStatus.Rejected)
            viewstate.Item("EditMode") = "List"
            LockControl()
            BindDTG()
        ElseIf e.CommandName.ToUpper = "Delete".ToUpper Then
            DeleteData(CType(lblID.Text, Integer))
            viewstate.Item("EditMode") = "List"
            LockControl()
            BindDTG()
        End If
    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblKTBApprovalDate As Label = e.Item.FindControl("lblKTBApprovalDate")
            Dim lblStatus As Label = e.Item.FindControl("lblStatus")
            Dim oD As Dealer = CType(Session("DEALER"), Dealer)
            Dim nStatus As Integer
            Dim lblNo As Label = e.Item.FindControl("lblNo")

            lblNo.Text = e.Item.ItemIndex + 1
            If CDate(lblKTBApprovalDate.Text) < DateSerial(1990, 1, 1) Then
                lblKTBApprovalDate.Text = ""
            Else
                lblKTBApprovalDate.Text = Format(CDate(lblKTBApprovalDate.Text), "dd/MM/yyyy")
            End If

            nStatus = CType(lblStatus.Text, Integer)
            If nStatus = enumFreePPhStatus.FreePPhStatus.Approved Then
                lblStatus.Text = "Approved"
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = False
            ElseIf nStatus = enumFreePPhStatus.FreePPhStatus.Rejected Then
                lblStatus.Text = "Rejected"
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = True
            Else
                lblStatus.Text = ""
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = True
            End If

            If oD.Title = EnumDealerTittle.DealerTittle.KTB Then 'add with privilege filtering
                Dim lbtnApprove As LinkButton = e.Item.FindControl("lbtnApprove")
                Dim lbtnReject As LinkButton = e.Item.FindControl("lbtnReject")

                If nStatus = enumFreePPhStatus.FreePPhStatus.Approved Then
                    lbtnApprove.Visible = False
                ElseIf nStatus = enumFreePPhStatus.FreePPhStatus.Rejected Then
                    lbtnReject.Visible = False
                Else
                End If
                dtgMain.Columns(10).Visible = True
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False
                CType(e.Item.FindControl("lbtnView"), LinkButton).Visible = True
            Else
                dtgMain.Columns(10).Visible = False
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = True
                CType(e.Item.FindControl("lbtnView"), LinkButton).Visible = False
            End If
        End If
    End Sub

#End Region
End Class
