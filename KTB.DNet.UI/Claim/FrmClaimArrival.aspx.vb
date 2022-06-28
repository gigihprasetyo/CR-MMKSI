Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Claim
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class FrmClaimArrival
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents icClaimDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtClaimNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchClaim As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealerName As System.Web.UI.WebControls.TextBox
    Protected WithEvents icClaimArrival As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents dtgClaimArrival As System.Web.UI.WebControls.DataGrid
    Protected WithEvents valName As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents chkClaimDate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkClaimArrival As System.Web.UI.WebControls.CheckBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


#Region "Custom Variable Declaration"
    Dim sesHelper As SessionHelper = New SessionHelper
    Private ArlClaimHeader As New ArrayList
    Dim oDealer As Dealer
    Dim a As ArrayList
    Dim oCH As ClaimHeader
#End Region

#Region "Custom Method"

    Private Sub BindStatus(ByVal ddL As DropDownList)
        Dim _ConditionFcd As New ClaimGoodConditionFacade(User)
        Dim arrCondition As ArrayList = _ConditionFcd.RetrieveList
        ddL.DataSource = arrCondition
        ddL.DataTextField = "Condition"
        ddL.DataValueField = "ID"
        ddL.DataBind()
        ddL.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub

    Private Sub ClearData()
        txtClaimNo.Text = ""
        txtDescription.Text = ""
        icClaimArrival.Enabled = True
        icClaimDate.Enabled = True
        txtDealerCode.Text = ""
        txtDealerName.Text = ""
        txtClaimNo.Enabled = True
        txtClaimNo.ReadOnly = False
        txtDealerCode.Enabled = True
        txtDealerCode.ReadOnly = False
        txtDescription.ReadOnly = False
        chkClaimArrival.Enabled = True
        chkClaimDate.Enabled = True
        btnSearch.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = True
        lblSearchClaim.Visible = True
        btnSearch.Enabled = True
        dtgClaimArrival.DataSource = Nothing
        dtgClaimArrival.DataBind()
    End Sub

    Private Sub bindData()
        a = sesHelper.GetSession("sesClaimHeader")
        oCH = CType(a(0), ClaimHeader)

        Dim totalRow As Integer = 0

        dtgClaimArrival.DataSource = a
        dtgClaimArrival.VirtualItemCount = totalRow
        dtgClaimArrival.DataBind()

        If a.Count = 0 And IsPostBack Then
            MessageBox.Show(SR.DataNotFound("Data"))
        End If

        'icClaimDate.Value = oCH.ClaimDate

        'txtDealerCode.Text = oCH.Dealer.DealerCode
        'txtDealerName.Text = oCH.Dealer.DealerName
        'If IsNothing(oCH.ClaimGoodCondition) Then
        '    ddlCondition.SelectedIndex = -1
        'Else
        '    ddlCondition.SelectedValue = oCH.ClaimGoodCondition.ID
        'End If
        'icClaimArrival.Value = Date.Now
        'txtDescription.Text = oCH.ReceivedDescription
        'icClaimArrival.Enabled = True
        'ddlCondition.Enabled = True
        'txtDescription.ReadOnly = False
        'btnSave.Enabled = True
        'btnCancel.Enabled = True
    End Sub
#End Region

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.PenerimaanBarang_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=CLAIM - Penerimaan Barang")
        End If
    End Sub

    Private Function CekBtnPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PenerimaanBarangCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        oDealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        'txtDealerCode.Text = oDealer.DealerCode
        'txtDealerName.Text = oDealer.DealerName

        If Not IsPostBack Then
            sesHelper.SetSession("arrival", "false")
            'BindStatus(ddlCondition)
            lblSearchClaim.Attributes("onClick") = "ShowPPClaimSelectionOne();"
            'ClearData()
            ReadCriteria()
            txtDealerName.Attributes.Add("readonly", "readonly")
        End If
        If CekBtnPriv() Then
            btnCancel.Enabled = True
        Else
            btnCancel.Enabled = False
        End If
    End Sub
    Public Function CreateCriteria() As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtClaimNo.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(ClaimHeader), "ClaimNo", MatchType.Exact, txtClaimNo.Text))
        End If

        If chkClaimDate.Checked Then
            criterias.opAnd(New Criteria(GetType(ClaimHeader), "ClaimDate", MatchType.GreaterOrEqual, Format(icClaimDate.Value, "yyyy-MM-dd HH:mm:ss")))
        End If

        If txtDealerCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(ClaimHeader), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text))
        End If

        If txtDealerName.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(ClaimHeader), "Dealer.DealerName", MatchType.Exact, txtDealerName.Text))
        End If

        If chkClaimArrival.Checked Then
            criterias.opAnd(New Criteria(GetType(ClaimHeader), "ReceivedDate", MatchType.GreaterOrEqual, Format(icClaimArrival.Value, "yyyy-MM-dd HH:mm:ss")))
        End If

        If txtDescription.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(ClaimHeader), "ReceivedDescription", MatchType.[Partial], txtDescription.Text))
        End If
        Return criterias
    End Function

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim fcdClaimHD As New ClaimHeaderFacade(User)
        Dim strClaim As String = String.Empty
        sesHelper.RemoveSession("sesClaimHeader")
        SaveCriteria()
        Dim criterias As CriteriaComposite = CreateCriteria()

        ArlClaimHeader.Clear()
        ArlClaimHeader = fcdClaimHD.Retrieve(criterias)
        If IsNothing(ArlClaimHeader) Or ArlClaimHeader.Count < 1 Then
            strClaim = txtClaimNo.Text
            'ClearData()
            txtClaimNo.Text = strClaim
            MessageBox.Show(SR.DataNotFound("Claim No." & strClaim))
        Else
            sesHelper.SetSession("sesClaimHeader", ArlClaimHeader)
            bindData()
        End If
        btnSave.Enabled = False
        If CekBtnPriv() Then
            btnCancel.Enabled = True
        Else
            btnCancel.Enabled = False
        End If
    End Sub
    Private Sub SaveCriteria()
        Dim crits As Hashtable = New Hashtable
        crits.Add("ClaimNo", txtClaimNo.Text)
        crits.Add("chkClaimDate", chkClaimDate.Checked)
        crits.Add("ClaimDate", icClaimDate.Value)
        crits.Add("DealerCode", txtDealerCode.Text)
        crits.Add("DealerName", txtDealerName.Text)
        crits.Add("chkClaimArrival", chkClaimArrival.Checked)
        crits.Add("ReceivedDate", icClaimArrival.Value)
        'crits.Add("ClaimGoodConditionID", ddlCondition.SelectedValue)
        crits.Add("ReceivedDescription", txtDescription.Text)
        sesHelper.SetSession("ClaimArrivalCriteria", crits)
    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim nResult As Integer
        Dim a As ArrayList = sesHelper.GetSession("sesClaimHeader")
        Dim objCH As ClaimHeader = CType(a(0), ClaimHeader)
        Dim objGC As ClaimGoodCondition

        'If ddlCondition.SelectedIndex > 0 Then
        '    objGC = New ClaimGoodConditionFacade(User).Retrieve(CInt(ddlCondition.SelectedValue))
        '    objCH.ClaimGoodCondition = objGC
        'End If

        If chkClaimArrival.Checked Then
            objCH.ReceivedDate = icClaimArrival.Value
        End If

        objCH.ReceivedDescription = txtDescription.Text

        Try
            nResult = New ClaimHeaderFacade(User).Update(objCH)
            bindData()
            lblSearchClaim.Visible = True
            btnSearch.Enabled = True
        Catch ex As Exception
            MessageBox.Show("Gagal Simpan Data Kedatangan Barang " & ex.Message)
        End Try
        If nResult <> -1 Then
            MessageBox.Show(SR.SaveSuccess)
        Else
            MessageBox.Show(SR.SaveFail)
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClearData()
    End Sub

    Private Sub dtgClaimArrival_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClaimArrival.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            If Not (a Is Nothing) Then
                oCH = a(e.Item.ItemIndex)
                Dim lblClaimNo As LinkButton = CType(e.Item.FindControl("lblClaimNo"), LinkButton)
                lblClaimNo.Text = oCH.ClaimNo
                lblClaimNo.CommandName = "ClaimNo"
                lblClaimNo.CommandArgument = oCH.ID

                Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
                lbtnView.CommandArgument = oCH.ID

                Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                lbtnEdit.CommandArgument = oCH.ID
            End If
        End If
    End Sub

    Private Sub dtgClaimArrival_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgClaimArrival.ItemCommand
        If e.CommandName = "ClaimNo" Then
            sesHelper.SetSession("arrival", "true")
            Response.Redirect("../Claim/FrmClaimDetail.aspx?ClaimHeaderID=" & e.CommandArgument & "&ViewKTB=0&View=0&isTerima=1")
        ElseIf e.CommandName = "Edit" Then
            ViewData(CType(e.CommandArgument, Integer), 0)
            lblSearchClaim.Visible = False
            btnSearch.Enabled = False
            If CekBtnPriv() Then
                btnSave.Enabled = True
                btnCancel.Enabled = True
            Else
                btnSave.Enabled = False
                btnCancel.Enabled = False
            End If
        ElseIf e.CommandName = "View" Then
            ViewData(CType(e.CommandArgument, Integer), 1)
        End If

    End Sub
    Private Sub ReadCriteria()
        Dim crits As Hashtable
        crits = CType(sesHelper.GetSession("ClaimArrivalCriteria"), Hashtable)

        If Not IsNothing(crits) Then
            txtClaimNo.Text = CStr(crits.Item("ClaimNo"))
            chkClaimDate.Checked = CBool(crits.Item("chkClaimDate"))
            icClaimDate.Value = CDate(crits.Item("ClaimDate"))
            txtDealerCode.Text = CStr(crits.Item("DealerCode"))
            txtDealerName.Text = CStr(crits.Item("DealerName"))
            chkClaimArrival.Checked = CBool(crits.Item("chkClaimArrival"))
            icClaimArrival.Value = CDate(crits.Item("ReceivedDate"))
            'ddlCondition.SelectedValue = CStr(crits.Item("ClaimGoodConditionID"))
            txtDescription.Text = CStr(crits.Item("ReceivedDescription"))
            sesHelper.SetSession("ClaimArrivalCriteria", crits)
            bindData()
        End If
    End Sub
    Private Sub ViewData(ByVal ID As Integer, ByVal Mode As Integer)
        Dim objClaimHeader As ClaimHeader = New ClaimHeaderFacade(User).Retrieve(ID)
        icClaimDate.Value = objClaimHeader.ClaimDate

        txtDealerCode.Text = objClaimHeader.Dealer.DealerCode
        txtDealerName.Text = objClaimHeader.Dealer.DealerName

        icClaimArrival.Value = objClaimHeader.ReceivedDate
        txtDescription.Text = objClaimHeader.ReceivedDescription

        chkClaimArrival.Enabled = False
        chkClaimDate.Enabled = False

        If (Mode = 1) Then
            txtClaimNo.ReadOnly = True
            icClaimDate.Enabled = False
            txtDealerCode.ReadOnly = True
            txtDealerName.ReadOnly = True
            icClaimArrival.Enabled = False
            'ddlCondition.Enabled = False
            txtDescription.ReadOnly = False
            btnSave.Enabled = False
            btnCancel.Enabled = True
            txtDescription.ReadOnly = True

        Else
            txtClaimNo.ReadOnly = True
            icClaimDate.Enabled = True
            txtDealerCode.ReadOnly = True
            txtDealerName.ReadOnly = True
            icClaimArrival.Enabled = True
            icClaimArrival.Value = DateTime.Now
            'ddlCondition.Enabled = True
            txtDescription.ReadOnly = False
            btnSave.Enabled = True
            btnCancel.Enabled = True
            chkClaimArrival.Enabled = True
            'chkClaimArrival.Checked = True
            chkClaimDate.Enabled = False
        End If
    End Sub
#End Region

    Private Sub txtClaimNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtClaimNo.TextChanged
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtClaimNo.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(ClaimHeader), "ClaimNo", MatchType.Exact, txtClaimNo.Text))
        End If

        Dim fcdClaimHD As New ClaimHeaderFacade(User)

        ArlClaimHeader.Clear()
        ArlClaimHeader = fcdClaimHD.Retrieve(criterias)
        If ArlClaimHeader.Count > 0 Then
            ViewData(CType(ArlClaimHeader(0), ClaimHeader).ID, 0)
        End If
    End Sub
End Class
