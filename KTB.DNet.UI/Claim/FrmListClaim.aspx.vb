Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.IO

Public Class FrmListClaim
    Inherits System.Web.UI.Page

#Region "Custom Variable Declaration"

    Dim sesHelper As SessionHelper = New SessionHelper
    Private ArlClaimHeader As ArrayList
    Protected WithEvents pnlSearch As System.Web.UI.WebControls.Panel
    Protected WithEvents icFakturDateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblStatu As System.Web.UI.WebControls.Label
    Protected WithEvents icClaimDateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icFakturDateUntil As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icClaimDateUntil As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlStatus2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents pnlChangeStatus As System.Web.UI.WebControls.Panel
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
    Protected WithEvents lstStatus As System.Web.UI.WebControls.ListBox
    Protected WithEvents lblGrandTotal As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Dim oDealer As Dealer

#End Region

#Region "Custom Method"
    Sub BindStatus(ByVal ddl As DropDownList)
        Dim items As New ArrayList
        If ddl.ID = "ddlStatus2" Then
            Dim _arrStatus As ArrayList = New EnumClaimStatus().RetrieveStatusForChangeDealer()
            For Each item As EnumClaimStatusProp In _arrStatus
                If CekPrivStatusKirim() And item.NameStatus = EnumClaimStatus.ClaimStatus.Dikirim.ToString Then
                    items.Add(item)
                End If
                If CekPrivStatusBatal() And item.NameStatus = EnumClaimStatus.ClaimStatus.Batal.ToString Then
                    items.Add(item)
                End If
            Next
        Else
            Dim _arrStatus As ArrayList = New EnumClaimStatus().RetrieveStatus()
            For Each item As EnumClaimStatusProp In _arrStatus
                If CekPrivStatusKirim() And item.NameStatus = EnumClaimStatus.ClaimStatus.Dikirim.ToString Then
                    items.Add(item)
                End If
                If CekPrivStatusBatal() And item.NameStatus = EnumClaimStatus.ClaimStatus.Batal.ToString Then
                    items.Add(item)
                End If
            Next
            'ddl.DataSource = New EnumClaimStatus().RetrieveStatus()
        End If

        ddl.DataSource = items
        ddl.DataTextField = "NameStatus"
        ddl.DataValueField = "ValStatus"
        ddl.DataBind()
        ddl.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub

    Sub BindListStatus()
        lstStatus.DataSource = New EnumClaimStatus().RetrieveStatus()
        lstStatus.DataTextField = "NameStatus"
        lstStatus.DataValueField = "ValStatus"
        lstStatus.DataBind()
    End Sub

    Sub BindToGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim criteriaDetail As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'If ddlStatus.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimHeader), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        Dim strData As String
        For Each item As ListItem In lstStatus.Items
            If item.Selected = True Then
                strData &= item.Value & ";"
            End If
        Next
        If strData <> "" Then
            strData = Left(strData, strData.Length - 1)
            viewstate.Add("ListStatus", strData)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimHeader), "Status", MatchType.InSet, "(" & strData.Replace(";", ",") & ")"))
            criteriaDetail.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.Status", MatchType.InSet, "(" & strData.Replace(";", ",") & ")"))
        End If


        If txtKodeDealer.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
            criteriaDetail.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        End If

        If chkFakturDate.Checked Then
            If icFakturDateFrom.Value <= icFakturDateUntil.Value Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimHeader), "SparePartPOStatus.BillingDate", MatchType.GreaterOrEqual, icFakturDateFrom.Value))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimHeader), "SparePartPOStatus.BillingDate", MatchType.LesserOrEqual, icFakturDateUntil.Value.AddDays(1)))
                criteriaDetail.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.SparePartPOStatus.BillingDate", MatchType.GreaterOrEqual, icFakturDateFrom.Value))
                criteriaDetail.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.SparePartPOStatus.BillingDate", MatchType.LesserOrEqual, icFakturDateUntil.Value.AddDays(1)))
            Else
                MessageBox.Show("Tanggal faktur sampai harus lebih besar atau sama dengan tanggal faktur dari")
                Exit Sub
            End If
        End If

        If chkClaimDate.Checked Then
            If icClaimDateFrom.Value <= icClaimDateUntil.Value Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimHeader), "ClaimDate", MatchType.GreaterOrEqual, icClaimDateFrom.Value))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimHeader), "ClaimDate", MatchType.LesserOrEqual, icClaimDateUntil.Value.AddDays(1)))
                criteriaDetail.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.ClaimDate", MatchType.GreaterOrEqual, icClaimDateFrom.Value))
                criteriaDetail.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.ClaimDate", MatchType.LesserOrEqual, icClaimDateUntil.Value.AddDays(1)))
            Else
                MessageBox.Show("Tanggal claim sampai harus lebih besar atau sama dengan tanggal claim dari")
                Exit Sub
            End If
        End If

        If txtClaimNo.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimHeader), "ClaimNo", MatchType.Exact, txtClaimNo.Text))
            criteriaDetail.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.ClaimNo", MatchType.Exact, txtClaimNo.Text))
        End If

        ArlClaimHeader = New Claim.ClaimHeaderFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dtgListClaim.PageSize, _
              total, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        If (ArlClaimHeader.Count > 0) Then
            pnlChangeStatus.Visible = True
            dtgListClaim.Visible = True
            dtgListClaim.VirtualItemCount = total
            dtgListClaim.DataSource = ArlClaimHeader
            dtgListClaim.DataBind()
        Else
            pnlChangeStatus.Visible = False
            dtgListClaim.Visible = False
            MessageBox.Show(SR.DataNotFound("Daftar Claim"))
        End If
        Dim TotalClaim As Decimal = 0

        criteriaDetail.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.Status", MatchType.InSet, "(" & CInt(EnumClaimStatus.ClaimStatus.Selesai) & "," & CInt(EnumClaimStatus.ClaimStatus.Selesai) & ")"))
        criteriaDetail.opAnd(New Criteria(GetType(ClaimDetail), "StatusDetail", MatchType.No, CInt(EnumClaimStatusDetail.ClaimStatusDetail.Ditolak)))

        TotalClaim = New KTB.DNet.BusinessFacade.Claim.ClaimDetailFacade(User).GetTotalClaim(criteriaDetail)
        lblGrandTotal.Text = TotalClaim.ToString("#,###")

    End Sub

    Private Sub SetSessionCriteria()
        Dim arrLastState As ArrayList = New ArrayList
        arrLastState.Add(txtKodeDealer.Text)
        arrLastState.Add(txtClaimNo.Text)
        'arrLastState.Add(ddlStatus.SelectedValue)
        arrLastState.Add(viewstate("ListStatus"))
        arrLastState.Add(dtgListClaim.CurrentPageIndex)
        arrLastState.Add(IIf(chkFakturDate.Checked, True, False))
        arrLastState.Add(IIf(chkClaimDate.Checked, True, False))
        arrLastState.Add(icFakturDateFrom.Value)
        arrLastState.Add(icFakturDateUntil.Value)
        arrLastState.Add(icClaimDateFrom.Value)
        arrLastState.Add(icClaimDateUntil.Value)
        Session("CLAIMSESSIONLASTSTATE") = arrLastState
    End Sub

    Private Sub GetSessionCriteria()
        If Not Session("CLAIMSESSIONLASTSTATE") Is Nothing Then
            Dim arrLastState As ArrayList = Session("CLAIMSESSIONLASTSTATE")
            If Not arrLastState Is Nothing Then
                txtKodeDealer.Text = arrLastState(0)
                txtClaimNo.Text = arrLastState(1)
                'ddlStatus.SelectedValue = arrLastState(2)
                If arrLastState(2) <> "" Then
                    For Each item As ListItem In lstStatus.Items
                        Dim strData() As String = arrLastState(2).ToString().Split(";")
                        For Each i As String In strData
                            If item.Value = i Then
                                item.Selected = True
                            End If
                        Next
                    Next
                End If
                dtgListClaim.CurrentPageIndex = arrLastState(3)
                chkFakturDate.Checked = arrLastState(4)
                chkClaimDate.Checked = arrLastState(5)
                icFakturDateFrom.Value = arrLastState(6)
                icFakturDateUntil.Value = arrLastState(7)
                icClaimDateFrom.Value = arrLastState(8)
                icClaimDateUntil.Value = arrLastState(9)
                Session("CLAIMSESSIONLASTSTATE") = Nothing
                BindToGrid(dtgListClaim.CurrentPageIndex)
            End If
        End If
    End Sub

#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblClaimNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon4 As System.Web.UI.WebControls.Label
    Protected WithEvents txtClaimNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblColon2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon5 As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents chkFakturDate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkClaimDate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblCodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgListClaim As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.StatusClaimListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=CLAIM - Daftar Status")
        End If
    End Sub
    Private Function CekPrivStatusKirim() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.StatusClaimListStatusDikirim_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekPrivStatusBatal() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.StatusClaimListStatusBatal_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekPrivDetail() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.StatusClaimListViewDetail_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Function CekPrivDownload() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.StatusClaimListDownload_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekPrivCatKTB() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.StatusListKTBNote_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#Region "Event Handlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        'Bug 983
        btnDownload.Visible = False
        oDealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        dtgListClaim.Columns(8).Visible = False
        If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            txtKodeDealer.Text = oDealer.DealerCode
            lblKodeDealer.Text = oDealer.DealerCode & " - " & oDealer.DealerName
            txtKodeDealer.Enabled = False
            pnlSearch.Visible = False
        ElseIf (oDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            txtKodeDealer.Enabled = True
        End If

        If Not IsPostBack Then
            ViewState("currSortColumn") = "Status"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            'BindStatus(ddlStatus)
            BindStatus(ddlStatus2)
            BindListStatus()
            GetSessionCriteria()

        End If
        If CekPrivDownload() Then
            btnDownload.Visible = True
        Else
            btnDownload.Visible = False
        End If
    End Sub

    Private Sub dtgListClaim_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgListClaim.ItemDataBound
        If (e.Item.ItemIndex >= 0) Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgListClaim.CurrentPageIndex * dtgListClaim.PageSize)

            Dim i As Integer = 0
            Dim Da As DealerAdditional = New DealerAdditional
            Dim arrDA As ArrayList = New ArrayList
            Dim TotalChDetails As Integer = 0
            Dim ch As ClaimHeader = CType(e.Item.DataItem, ClaimHeader)

            For i = 0 To ch.ClaimDetails.Count - 1
                Dim cd As ClaimDetail = New ClaimDetail
                cd = ch.ClaimDetails(i)
                'Bug 1420 & 300
                If cd.StatusDetail <> EnumClaimStatusDetail.ClaimStatusDetail.Ditolak And ch.Status = EnumClaimStatus.ClaimStatus.Selesai Then
                    TotalChDetails = TotalChDetails + (cd.ApprovedQty * cd.SparePartPOStatusDetail.ClaimPriceUnit)
                End If
            Next

            Dim Status As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim Total As Label = CType(e.Item.FindControl("lblTotal"), Label)
            Dim ETA As Label = CType(e.Item.FindControl("lblETA"), Label)
            Dim lbtnPopUp As LinkButton = CType(e.Item.FindControl("lbtnPopUp"), LinkButton)
            Dim lbtnFrmClaim As LinkButton = CType(e.Item.FindControl("lbtnFrmClaim"), LinkButton)
            Dim lnbNoClaim As LinkButton = CType(e.Item.FindControl("lnbNoClaim"), LinkButton)
            Dim lblNoClaim As Label = CType(e.Item.FindControl("lblNoClaim"), Label)
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim lbtnDetails As LinkButton = e.Item.FindControl("lbtnDetails")
            Dim btnDownloadCM As LinkButton = CType(e.Item.FindControl("btnDownloadCM"), LinkButton)

            lbtnPopUp.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpKTBNote.aspx?ID=" & ch.ID & "&isKTB=1', '', 300, 500, KTBNote);return false;")

            lnbNoClaim.Text = ch.ClaimNo
            lblNoClaim.Text = ch.ClaimNo

            'refer bug 257
            If ch.Status = EnumClaimStatus.ClaimStatus.Selesai Or ch.Status = EnumClaimStatus.ClaimStatus.Complete_Selesai Then
                If ch.FakturRetur <> "" Then
                    lnbNoClaim.Attributes.Add("onclick", "showPopUp('../Claim/FrmPrintClaim.aspx?ID=" & ch.ID & "', '', 600, 800, KTBNote);return false;")
                End If
                lblNoClaim.Visible = False
                lnbNoClaim.Visible = True
            Else
                lblNoClaim.Visible = True
                lnbNoClaim.Visible = False
            End If

            If (ch.FakturRetur <> String.Empty) Then
                'lblNoClaim.Visible = False
                'lnbNoClaim.Visible = True
                lbtnEdit.Visible = False
                'lnbNoClaim.Attributes.Add("onclick", "showPopUp('../Claim/FrmPrintClaim.aspx?ID=" & ch.ID & "', '', 600, 800, KTBNote);return false;")
            Else
                'lnbNoClaim.Attributes.Add("onclick", "showPopUp('../Claim/FrmPrintClaim.aspx?ID=" & ch.ID & "', '', 600, 800, KTBNote);return false;")
                'lblNoClaim.Visible = False
                'lnbNoClaim.Visible = True
            End If

            If (ch.ReceivedDate.ToString("dd/MM/yyyy") <> "01/01/1753") Then
                ETA.Text = ch.ReceivedDate.ToString("dd/MM/yyyy")
            Else
                arrDA = New General.DealerAdditionalFacade(User).RetrieveByDealerID(ch.Dealer.ID)
                If (arrDA.Count > 0) Then
                    Da = arrDA(0)
                    ETA.Text = Da.ClaimETA
                End If
            End If

            If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                lbtnFrmClaim.Visible = True
                lbtnFrmClaim.Attributes.Add("onclick", "showPopUp('../Claim/FrmClaimProcess.aspx?ID=" & ch.ID & "', '', 600, 800, KTBNote);return false;")
            End If

            If (ch.UploadFileName <> "") Then
                Dim lbtnDownload As LinkButton = CType(e.Item.FindControl("lbtnDownload"), LinkButton)
                lbtnDownload.Visible = True
            End If

            Dim critEDoc As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartEDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critEDoc.opAnd(New Criteria(GetType(SparePartEDocument), "DocType", MatchType.Exact, 1))
            critEDoc.opAnd(New Criteria(GetType(SparePartEDocument), "DocNumber", MatchType.Exact, ch.FakturRetur))
            Dim arlEDoc As ArrayList = New SparePartEDocumentFacade(User).Retrieve(critEDoc)
            If arlEDoc.Count > 0 Then
                btnDownloadCM.Visible = True
            Else
                btnDownloadCM.Visible = False
            End If

            'If (ch.FakturRetur <> String.Empty) Then
            '    btnDownloadCM.Visible = True
            'Else
            '    btnDownloadCM.Visible = False
            'End If



            If ch.Status <> EnumClaimStatus.ClaimStatus.Baru Then
                lbtnEdit.Visible = False
                lbtnDelete.Visible = False
            Else
                lbtnEdit.Visible = True
                lbtnDelete.Visible = True
            End If

            'Bug 1419
            If CekPrivDetail() Then
                'If ch.Status <> EnumClaimStatus.ClaimStatus.Proses Then
                '    lbtnDetails.Visible = True
                'Else
                '    lbtnDetails.Visible = False
                'End If
                lbtnDetails.Visible = True
            Else
                lbtnDetails.Visible = False
            End If

            If CekPrivCatKTB() Then
                If ch.KTBNote = "" Then
                    lbtnPopUp.Visible = False
                Else
                    lbtnPopUp.Visible = True
                End If
            Else
                lbtnPopUp.Visible = False
            End If

            Total.Text = TotalChDetails.ToString("#,##0")

            If ch.Status = EnumClaimStatus.ClaimStatus.Baru Then
                Status.Text = "Baru"
            ElseIf ch.Status = EnumClaimStatus.ClaimStatus.Dikirim Then
                Status.Text = "Dikirim"
            ElseIf ch.Status = EnumClaimStatus.ClaimStatus.Batal Then
                Status.Text = "Batal"
                'ElseIf ch.Status = EnumClaimStatus.ClaimStatus.Ditolak Then
                '    Status.Text = "Ditolak"
            ElseIf ch.Status = EnumClaimStatus.ClaimStatus.Proses Then
                Status.Text = "Proses"
            ElseIf ch.Status = EnumClaimStatus.ClaimStatus.Selesai Then
                Status.Text = "Selesai"
            ElseIf ch.Status = EnumClaimStatus.ClaimStatus.Complete_Selesai Then
                Status.Text = "Complete Selesai"
            End If
            i = 0
        End If
    End Sub

    Private Sub dtgListClaim_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgListClaim.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If
        BindToGrid(dtgListClaim.CurrentPageIndex)
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If chkClaimDate.Checked = False And chkFakturDate.Checked = False Then
            MessageBox.Show("Tanggal Faktur Atau Tanggal Claim Harus Dipilih")
            Exit Sub
        End If

        dtgListClaim.CurrentPageIndex = 0
        BindToGrid(dtgListClaim.CurrentPageIndex)
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim i As Integer = 0
        Dim CH As ClaimHeader
        Dim arrList As ArrayList = New ArrayList
        For Each item As DataGridItem In dtgListClaim.Items
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If (chk.Checked) Then
                If (ddlStatus2.SelectedValue = "-1") Then
                    MessageBox.Show("Status yg diubah harus dipilih")
                    Exit Sub
                End If
                CH = New ClaimHeader
                Dim CHFacade As Claim.ClaimHeaderFacade = New Claim.ClaimHeaderFacade(User)
                CH = CHFacade.Retrieve(Convert.ToInt32(dtgListClaim.DataKeys().Item(i)))

                'Check Status
                If CH.Status = EnumClaimStatus.ClaimStatus.Selesai Or CH.Status = EnumClaimStatus.ClaimStatus.Complete_Selesai Then
                    MessageBox.Show("Status " & EnumClaimStatus.ClaimStatus.Selesai.ToString & " tidak dapat diubah ")
                    Exit Sub
                End If

                If CH.Status = EnumClaimStatus.ClaimStatus.Dikirim And CInt(ddlStatus2.SelectedValue) = EnumClaimStatus.ClaimStatus.Batal Then
                    MessageBox.Show("Status " & EnumClaimStatus.ClaimStatus.Dikirim.ToString & " tidak dapat diubah menjadi " & ddlStatus2.SelectedItem.Text)
                    Exit Sub
                End If
                If CH.Status = EnumClaimStatus.ClaimStatus.Batal And CInt(ddlStatus2.SelectedValue) = EnumClaimStatus.ClaimStatus.Dikirim Then
                    MessageBox.Show("Status " & EnumClaimStatus.ClaimStatus.Batal.ToString & " tidak dapat diubah menjadi " & ddlStatus2.SelectedItem.Text)
                    Exit Sub
                End If
                'End Check Status

                'start:check valid date 
                If CType(ddlStatus2.SelectedValue, Short) = EnumClaimStatus.ClaimStatus.Dikirim Then
                    Dim limitDate As Date = Now
                    If Me.isValidClaimDate(CH) = False Then
                        Exit Sub
                    End If
                End If
                'end:check valid date 

                CH.Status = ddlStatus2.SelectedValue
                CH.ClaimDate = DateTime.Now

                'See UseCase for status mapping
                If CH.Status = EnumClaimStatus.ClaimStatus.Dikirim Then
                    CH.StatusKTB = EnumClaimProgress.ClaimProgressKTB.Baru
                ElseIf CH.Status = EnumClaimStatus.ClaimStatus.Baru Or CH.Status = EnumClaimStatus.ClaimStatus.Batal Then
                    CH.StatusKTB = EnumClaimProgress.ClaimProgressKTB.BelumDikirim
                End If

                arrList.Add(CH)
            End If
            i = i + 1
        Next
        If (arrList.Count > 0) Then

            'Check Status
            Dim ClaimNoError As String = ""
            For Each itemClaim As ClaimHeader In arrList
                Dim itemClaimDB As ClaimHeader = New Claim.ClaimHeaderFacade(User).Retrieve(itemClaim.ID)
                If itemClaimDB.Status > EnumClaimStatus.ClaimStatus.Batal Then
                    ClaimNoError = ClaimNoError & itemClaimDB.ClaimNo & ";"
                End If
            Next

            If ClaimNoError <> "" Then
                MessageBox.Show("Proses Simpan Gagal. Status Claim Nomor ( " & ClaimNoError & " ) Sudah Diproses.")
                Exit Sub
            End If

            If (New Claim.ClaimHeaderFacade(User).UpdateClaimHeader(arrList) = 1) Then
                MessageBox.Show(SR.SaveSuccess)
                BindToGrid(dtgListClaim.CurrentPageIndex)
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        Else
            MessageBox.Show("Daftar claim belum dipilih")
        End If
    End Sub
    Private Function isValidClaimDate(ByVal oCH As ClaimHeader) As Boolean
        Dim limitDate = Me.GetLimitDate(oCH)
        If Date.Today > limitDate Then
            MessageBox.Show("Batas waktu pengajuan claim " & oCH.ClaimNo & " sudah lewat ( " & Format(limitDate, "dd-MM-yyyy") & " )")

            Return False
        End If
        Return True
    End Function
    Private Function GetLimitDate(ByVal oCH As ClaimHeader) As Date
        Dim objPO As SparePartPOStatus = oCH.SparePartPOStatus
        Dim estArrival As Integer = 0
        Dim reasonLimit As Integer = 0
        Dim fakturdate As Date = objPO.BillingDate
        Dim objDealerx As Dealer = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo).Dealer

        Dim objDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(CInt(objDealerx.ID))

        If objDealer.DealerAdditionals.Count > 0 Then
            estArrival = CDbl(CType(objDealer.DealerAdditionals(0), DealerAdditional).ClaimETA)
        End If

        Dim objReason As ClaimReason = oCH.ClaimReason ' New ClaimReasonFacade(User).Retrieve(CInt(ddlReasonClaimHeader.SelectedValue))
        reasonLimit = objReason.TimeLimit

        Return objPO.BillingDate.AddDays(estArrival + reasonLimit)

    End Function

    Private Sub dtgListClaim_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgListClaim.ItemCommand

        If (e.CommandName = "detail") Then
            SetSessionCriteria()
            Response.Redirect("../Claim/FrmClaimDetail.aspx?ClaimHeaderID=" & e.CommandArgument & "&View=True&ViewKTB=false")
        End If
        If (e.CommandName = "edit") Then
            Dim objClaim As ClaimHeader = New Claim.ClaimHeaderFacade(User).Retrieve(CInt(e.CommandArgument))
            If objClaim.Status <> EnumClaimStatus.ClaimStatus.Baru Then
                MessageBox.Show("Anda tidak dapat mengubah data claim selain claim dengan status 'Baru'")
                Exit Sub
            End If
            SetSessionCriteria()
            Response.Redirect("../Claim/FrmClaimDetail.aspx?ClaimHeaderID=" & e.CommandArgument & "&View=False&ViewKTB=false")
        End If
        If (e.CommandName = "download") Then
            Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("ClaimEvidenceDir") & "\" & e.CommandArgument
            Response.Redirect("../Download.aspx?file=" & PathFile)
        End If

        If (e.CommandName = "Delete") Then
            Dim objClaim As ClaimHeader = New Claim.ClaimHeaderFacade(User).Retrieve(CInt(e.CommandArgument))
            If objClaim.Status <> EnumClaimStatus.ClaimStatus.Baru Then
                MessageBox.Show("Anda tidak dapat menghapus data claim selain claim dengan status 'Baru'")
                Exit Sub
            End If
            Dim objfacade As Claim.ClaimHeaderFacade = New Claim.ClaimHeaderFacade(User)
            objfacade.DeleteTransaction(objClaim)
            BindToGrid(dtgListClaim.CurrentPageIndex)

        End If

        If (e.CommandName = "DownloadCM") Then
            Dim lblNoFaktur As Label = CType(e.Item.FindControl("lblFakturRetur"), Label)
            Dim critEDoc As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartEDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critEDoc.opAnd(New Criteria(GetType(SparePartEDocument), "DocType", MatchType.Exact, 1))
            critEDoc.opAnd(New Criteria(GetType(SparePartEDocument), "DocNumber", MatchType.Exact, lblNoFaktur.Text))
            'critEDoc.opAnd(New Criteria(GetType(SparePartBilling), "SparePartBilling.BillingNumber", MatchType.Exact, lblNoFaktur.Text))
            Dim arlEDoc As ArrayList = New SparePartEDocumentFacade(User).Retrieve(critEDoc)
            If arlEDoc.Count > 0 Then
                Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & CType(arlEDoc(0), SparePartEDocument).Path
                Response.Redirect("../Download.aspx?file=" & PathFile)
            Else
                MessageBox.Show("Data tidak ditemukan")
                Exit Sub
            End If

        End If

    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim i As Integer = 0
        Dim CH As ClaimHeader
        Dim CD As ArrayList
        Dim success As Boolean = False
        Dim Connect As Boolean = False
        Dim sw As StreamWriter
        Dim filename = String.Format("{0}{1}{2}", "claimord", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("ClaimDownload") & "\" & filename  '-- Destination file
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        success = imp.Start()
        For Each item As DataGridItem In dtgListClaim.Items
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If (chk.Checked) Then
                If (Connect = False) Then                    
                    Dim finfo As New FileInfo(DestFile)
                    Try
                        If success Then
                            If Not finfo.Directory.Exists Then
                                Directory.CreateDirectory(finfo.DirectoryName)
                            End If
                            sw = New StreamWriter(DestFile)
                            Connect = True
                        End If
                    Catch ex As Exception
                        Throw ex
                        Exit Sub
                    End Try
                End If
                CH = New ClaimHeader
                Dim CHFacade As Claim.ClaimHeaderFacade = New Claim.ClaimHeaderFacade(User)
                CH = CHFacade.Retrieve(Convert.ToInt32(dtgListClaim.DataKeys().Item(i)))

                'H;billingnumber;claimnumber;claimdate;dealercode;claimreasoncode;claimID 
                'D;partnumber;partquantity
                sw.WriteLine("H;" & CH.SparePartPOStatus.BillingNumber & ";" & CH.ClaimNo & ";" & CH.ClaimDate.ToString("ddMMyyyy") & ";" & CH.Dealer.DealerCode & ";" & CH.ClaimReason.Code & ";" & CH.ID)

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.ID", MatchType.Exact, CH.ID))
                CD = New ArrayList
                CD = New Claim.ClaimDetailFacade(User).RetrieveActiveList(criterias)

                For Each item2 As ClaimDetail In CD
                    sw.WriteLine("D;" & item2.SparePartPOStatusDetail.SparePartMaster.PartNumber & ";" & item2.ApprovedQty)
                Next
            End If
            i = i + 1
        Next
        If (success = True) Then
            sw.Close()
            Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("ClaimDownload") & "\" & filename
            Response.Redirect("../Download.aspx?file=" & PathFile)
            imp.StopImpersonate()
            imp = Nothing
        Else
            MessageBox.Show("Daftar claim belum dipilih")
        End If
    End Sub
#End Region

    Private Sub dtgListClaim_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgListClaim.PageIndexChanged
        dtgListClaim.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgListClaim.CurrentPageIndex)

    End Sub
End Class
