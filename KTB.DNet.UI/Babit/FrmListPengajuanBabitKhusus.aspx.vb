Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class FrmListPengajuanBabitKhusus
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlJenisKegiatan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNoPengajuan As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlStart As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlEnd As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnProcess As System.Web.UI.WebControls.Button
    Protected WithEvents pnlChangeStatus As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlSearch As System.Web.UI.WebControls.Panel
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents pnlHead As System.Web.UI.WebControls.Panel
    Protected WithEvents lblSisa As System.Web.UI.WebControls.Label
    Protected WithEvents lblTerpakai As System.Web.UI.WebControls.Label
    Protected WithEvents lblAlokasiBabit As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTahun As System.Web.UI.WebControls.DropDownList
    Protected WithEvents hdnValNew As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents ddlTahunTo As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variables Declaration"
    Dim en As EnumBabit
    Dim objDealer As New Dealer
    Dim sHelper As New SessionHelper
#End Region

#Region "Custom Methods"

    Sub FillBabitAllocation(ByVal BabitAllocation As Integer, ByVal Sisa As Integer)
        lblAlokasiBabit.Text = BabitAllocation.ToString("#,##0")
        lblTerpakai.Text = (BabitAllocation - Sisa).ToString("#,##0")
        lblSisa.Text = Sisa.ToString("#,##0")
    End Sub

    Sub BindJenisKegiatan()
        ddlJenisKegiatan.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        ddlJenisKegiatan.Items.Add(New ListItem(EnumBabit.BabitProposalType.Even.ToString(), CInt(EnumBabit.BabitProposalType.Even).ToString()))
        ddlJenisKegiatan.Items.Add(New ListItem(EnumBabit.BabitProposalType.Pameran.ToString(), CInt(EnumBabit.BabitProposalType.Pameran).ToString()))
    End Sub

    Sub BindMonth()
        CommonFunction.BindFromEnum("Month", ddlStart, User, False, "NameStatus", "ValStatus")
        CommonFunction.BindFromEnum("Month", ddlEnd, User, False, "NameStatus", "ValStatus")
        ddlStart.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        ddlEnd.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
    End Sub

    Sub BindTahun()
        ddlTahun.Items.Clear()
        ddlTahun.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For i As Integer = DateTime.Today.Year - 5 To DateTime.Today.Year + 1
            ddlTahun.Items.Add(New ListItem(i.ToString, i))
        Next

        ddlTahunTo.Items.Clear()
        ddlTahunTo.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For i As Integer = DateTime.Today.Year - 5 To DateTime.Today.Year + 1
            ddlTahunTo.Items.Add(New ListItem(i.ToString, i))
        Next

    End Sub

    Sub BindChangeStatus()
        en = New EnumBabit
        ddlStatus.DataTextField = "BabitValue"
        ddlStatus.DataValueField = "BabitCode"
        Dim arl As New ArrayList
        Dim arlTmp As New ArrayList

        If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            arlTmp = en.StatusBabitProposalListDealer
            For Each item As BabitItem In arlTmp
                If (CekPrivilegeStatusBatal()) And (item.BabitValue = EnumBabit.StatusBabitProposal.Batal.ToString) Then
                    arl.Add(item)
                ElseIf (CekPrivilegeStatusValidation()) And (item.BabitValue = EnumBabit.StatusBabitProposal.Validasi.ToString) Then
                    arl.Add(item)
                End If
            Next
        ElseIf (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            arlTmp = en.StatusBabitProposalListKTB
            For Each item As BabitItem In arlTmp
                If (CekPrivilegeStatusProses()) And (item.BabitValue = EnumBabit.StatusBabitProposal.Proses.ToString) Then
                    arl.Add(item)
                ElseIf (CekPrivilegeStatusTolak()) And (item.BabitValue = EnumBabit.StatusBabitProposal.Tolak.ToString) Then
                    arl.Add(item)
                ElseIf (CekPrivilegeStatusDisetujui()) And (item.BabitValue = EnumBabit.StatusBabitProposal.Disetujui.ToString) Then
                    arl.Add(item)
                End If
            Next
        End If

        'arl = en.StatusBabitProposalList
        'Dim arlNew As New ArrayList
        'For Each item As BabitItem In arl
        '    If (item.BabitCode = CType(EnumBabit.StatusBabitProposal.Disetujui, Short)) Then
        '        arlNew.Add(item)
        '    End If
        '    If (item.BabitCode = CType(EnumBabit.StatusBabitProposal.Tolak, Short)) Then
        '        arlNew.Add(item)
        '    End If
        '    If (item.BabitCode = CType(EnumBabit.StatusBabitProposal.Validasi, Short)) Then
        '        arlNew.Add(item)
        '    End If
        'Next
        ddlStatus.DataSource = arl
        ddlStatus.DataBind()
        ddlStatus.Items.Insert(0, New ListItem("Silakan Pilih", "-1"))
        ddlStatus.SelectedIndex = 0
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "BabitKhususAmount", MatchType.Greater, 0))

        If (txtKodeDealer.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        End If

        If (txtNoPengajuan.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "NoPengajuan", MatchType.Exact, txtNoPengajuan.Text.Trim))
        End If

        'If (txtKodeDealer.Text.Trim = String.Empty And txtNoPengajuan.Text.Trim = String.Empty) Then
        '    MessageBox.Show("Kode dealer atau no pengajuan babit khusus harus diisi")
        '    Exit Sub
        'Else
        '    If (txtKodeDealer.Text.Trim <> String.Empty) Then
        '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text))
        '    End If
        '    If (txtNoPengajuan.Text.Trim <> String.Empty) Then
        '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "NoPengajuan", MatchType.Exact, txtNoPengajuan.Text.Trim))
        '    End If
        'End If

        If (ddlJenisKegiatan.SelectedValue <> "-1") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "ActivityType", MatchType.Exact, ddlJenisKegiatan.SelectedValue))
        End If

        If (ddlStart.SelectedValue <> "-1") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "BabitAllocation.Babit.StartPeriod", MatchType.LesserOrEqual, ddlStart.SelectedValue))
        End If
        'If (ddlEnd.SelectedValue <> "-1") Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "BabitAllocation.Babit.EndPeriod", MatchType.GreaterOrEqual, ddlEnd.SelectedValue))
        'End If
        If ddlTahun.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "BabitAllocation.Babit.BabitYear", MatchType.LesserOrEqual, ddlTahun.SelectedValue))
        End If
        If ddlTahunTo.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "BabitAllocation.Babit.BabitYearEnd", MatchType.GreaterOrEqual, ddlTahunTo.SelectedValue))
        End If
        If ddlEnd.SelectedValue <> "-1" Then
            If (ddlStart.SelectedValue = "-1") Or (ddlTahun.SelectedValue = "-1") Or (ddlTahunTo.SelectedValue = "-1") Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "BabitAllocation.Babit.EndPeriod", MatchType.GreaterOrEqual, ddlEnd.SelectedValue))
            End If
        End If

        Dim arl As New ArrayList
        Dim arlFirst As New ArrayList

        Dim bfacade As New BabitSalesComm.BabitProposalFacade(User)
        arlFirst = bfacade.RetrieveByCriteria(criterias, indexPage + 1, dtgList.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), String))

        For Each obj As BabitProposal In arlFirst
            If objDealer.TitleDealer = EnumDealerTittle.DealerTittle.KTB.ToString() Then
                If (bfacade.IsCreatedByKTB(obj, objDealer)) Then
                    arl.Add(obj)
                Else
                    If obj.Status <> EnumBabit.StatusBabitProposal.Baru Then
                        arl.Add(obj)
                    End If
                End If
            Else
                arl.Add(obj)
            End If
        Next

        If (ddlStart.SelectedValue <> "-1" And ddlTahun.SelectedValue <> "-1" And ddlEnd.SelectedValue <> "-1" And ddlTahunTo.SelectedValue <> "-1") Then
            Dim dtmFrom2 As DateTime = New DateTime(CInt(ddlTahun.SelectedValue), CInt(ddlStart.SelectedValue), 1)
            Dim dtmTo2 As DateTime = New DateTime(CInt(ddlTahunTo.SelectedValue), CInt(ddlEnd.SelectedValue), 1)
            Dim arl2 As ArrayList = bfacade.FilterBabitProposalByPeriodeList(arl, dtmFrom2, dtmTo2)
            arl = New ArrayList
            arl = arl2
            If IsNothing(arl) Then
                arl = New ArrayList
            End If
        End If

        dtgList.DataSource = arl
        dtgList.VirtualItemCount = totalRow
        dtgList.DataBind()
        If arl.Count > 0 Then
            pnlChangeStatus.Visible = True

            Dim totalAlokasi As Decimal = 0
            Dim totalSisa As Decimal = 0
            Dim totalDisetujui As Decimal = 0
            Dim tmpArl As ArrayList = CommonFunction.SortArraylist(arl, GetType(BabitProposal), "Dealer.ID", Sort.SortDirection.ASC)
            Dim tmpDealerId As Integer = 0
            For Each obj As BabitProposal In tmpArl
                If (tmpDealerId <> obj.Dealer.ID) Then
                    tmpDealerId = obj.Dealer.ID
                    totalAlokasi += obj.BabitAllocation.DanaBabit
                End If
                totalDisetujui += obj.KTBApprovalAmount
            Next
            lblAlokasiBabit.Text = String.Format("Rp.{0}", totalAlokasi.ToString("#,##0"))
            lblTerpakai.Text = String.Format("Rp.{0}", totalDisetujui.ToString("#,##0"))
            totalSisa = totalAlokasi - totalDisetujui
            lblSisa.Text = String.Format("Rp.{0}", totalSisa.ToString("#,##0"))
        Else
            pnlChangeStatus.Visible = False
            dtgList.DataSource = New ArrayList
            dtgList.DataBind()
            lblAlokasiBabit.Text = "Rp.0"
            lblTerpakai.Text = "Rp.0"
            lblSisa.Text = "Rp.0"
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub SetSessionCriteria()
        Dim arrLastState As ArrayList = New ArrayList
        arrLastState.Add(txtKodeDealer.Text)
        arrLastState.Add(ddlJenisKegiatan.SelectedValue)
        arrLastState.Add(txtNoPengajuan.Text)
        arrLastState.Add(ddlStart.SelectedValue)
        arrLastState.Add(ddlEnd.SelectedValue)
        arrLastState.Add(dtgList.CurrentPageIndex)
        'ToDo session
        sHelper.SetSession("BABITPROPOSALSPSESSIONLASTSTATE", arrLastState)
        'Session("BABITPROPOSALSPSESSIONLASTSTATE") = arrLastState
    End Sub

    Private Sub GetSessionCriteria()
        If Not sHelper.GetSession("BABITPROPOSALSPSESSIONLASTSTATE") Is Nothing Then
            Dim arrLastState As ArrayList = sHelper.GetSession("BABITPROPOSALSPSESSIONLASTSTATE")
            If Not arrLastState Is Nothing Then
                txtKodeDealer.Text = arrLastState.Item(0)
                ddlJenisKegiatan.SelectedValue = arrLastState.Item(1)
                txtNoPengajuan.Text = arrLastState.Item(2)
                ddlStart.SelectedValue = arrLastState.Item(3)
                ddlEnd.SelectedValue = arrLastState.Item(4)
                dtgList.CurrentPageIndex = arrLastState.Item(5)
                Session("BABITPROPOSALSPSESSIONLASTSTATE") = Nothing
                BindDataGrid(dtgList.CurrentPageIndex)
            End If
        End If
    End Sub

    Private Function ValidateProcess(ByVal StatusHid As String, ByVal StatusGrid As String, ByRef Status As String) As String
        Dim oDPA As DealerProposalAction
        oDPA = New DealerProposalAction(StatusHid)

        If (ddlStatus.SelectedValue = CType(EnumBabit.StatusBabitProposal.Validasi, Short)) Then
            If (oDPA.Submit = -1) Then
                Status = String.Empty
                Return "Status Babit proposal harus baru"
            End If
        End If
        'If (ddlStatus.SelectedValue = CType(EnumBabit.StatusBabitProposal.Tolak, Short)) Then
        '    If (oDPA.Submit = -1) Then
        '        If (oDPA.Verify = -1) Then
        '            Status = String.Empty
        '            Return "Status Babit proposal harus baru"
        '        End If
        '    End If
        'End If
        If (ddlStatus.SelectedValue = CType(EnumBabit.StatusBabitProposal.Batal, Short)) Then
            If (oDPA.Submit = -1) Then
                If (oDPA.Verify = -1) Then
                    Status = String.Empty
                    Return "Status Babit proposal harus baru"
                End If
            End If
        End If
        If (ddlStatus.SelectedValue = CType(EnumBabit.StatusBabitProposal.Proses, Short)) Then
            If (oDPA.Verify = -1) Then
                Status = String.Empty
                Return "Status Babit proposal harus validasi"
            End If
        End If

        If (ddlStatus.SelectedValue = CType(EnumBabit.StatusBabitProposal.Disetujui, Short)) Then
            If (oDPA.Approve = -1) Then
                Status = String.Empty
                Return "Status Babit proposal harus Proses"
            End If
        End If

        Status = StatusHid
    End Function

    Sub Update(ByVal Process As String)
        If ddlStatus.SelectedIndex < 1 Then
            MessageBox.Show("Silakan pilih status baru")
            Return
        End If

        If (hdnValNew.Value = "-1") Then
            MessageBox.Confirm(String.Format("Apakah anda yakin ingin {0} ?", ddlStatus.SelectedItem.Text), "hdnValNew")
            Return
        End If

        Dim arl As ArrayList = New ArrayList
        Dim oBabitProposal As BabitProposal
        Dim i As Integer = 0
        Dim Status As String = String.Empty
        Dim Amount As Decimal = 0

        For Each item As DataGridItem In dtgList.Items
            Dim strErrMsg As String
            Dim chkItemChecked As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            Dim lblStatus As Label = CType(item.FindControl("lblStatus"), Label)
            Dim lblStatusHid As Label = CType(item.FindControl("lblStatusHid"), Label)
            Dim txtPersetujuan As TextBox = CType(item.FindControl("txtPersetujuan"), TextBox)
            Dim lblPengajuanDana As Label = CType(item.FindControl("lblPengajuanDana"), Label)
            Dim txtNoPersetujuan As TextBox = CType(item.FindControl("txtNoPersetujuan"), TextBox)

            If (chkItemChecked.Checked) Then
                oBabitProposal = New BabitProposal
                oBabitProposal = New BabitSalesComm.BabitProposalFacade(User).Retrieve(Convert.ToInt32(dtgList.DataKeys().Item(i)))

                If (Status = String.Empty) Then
                    strErrMsg = ValidateProcess(lblStatusHid.Text.Trim, lblStatus.Text.Trim, Status)
                    '!!!!!!!! code injection --- terburu2
                    If (ddlStatus.SelectedValue = CType(EnumBabit.StatusBabitProposal.Tolak, Short)) Then
                        If (oBabitProposal.Status <> EnumBabit.StatusBabitProposal.Proses) Then
                            MessageBox.Show("Hanya proposal dengan status Proses yang bisa ditolak")
                            Exit Sub
                        End If
                    End If
                    '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                    If (strErrMsg <> String.Empty) Then
                        MessageBox.Show(strErrMsg)
                        Exit Sub
                    End If
                Else
                    If (lblStatusHid.Text.Trim = Status) Then
                        strErrMsg = ValidateProcess(lblStatusHid.Text.Trim, lblStatus.Text.Trim, Status)
                        If (strErrMsg <> String.Empty) Then
                            MessageBox.Show(strErrMsg)
                            Exit Sub
                        End If
                    Else
                        MessageBox.Show("Status Babit Proposal khusus yang dipilih tidak sama")
                        Status = String.Empty
                        Exit Sub
                    End If
                End If

                If oBabitProposal.BabitAllocation Is Nothing Then
                    MessageBox.Show("Proposal dengan nomor : " & oBabitProposal.NoPengajuan & " belum dialokasikan")
                    Exit Sub
                End If

                If (Process = CType(EnumBabit.StatusBabitProposal.Disetujui, Short)) Then
                    If (txtPersetujuan.Text.Trim <> String.Empty) Then
                        If (Convert.ToDecimal(lblPengajuanDana.Text) < Convert.ToDecimal(txtPersetujuan.Text)) Then
                            MessageBox.Show("Persetujuan dana babit tidak boleh lebih besar dari jumlah pengajuan")
                            Exit Sub
                        End If
                    Else
                        MessageBox.Show("Persetujuan dana babit harus diisi")
                        Exit Sub
                    End If
                    Amount += Convert.ToDecimal(txtPersetujuan.Text)

                    If (Amount > Convert.ToDecimal(oBabitProposal.BabitAllocation.SisaBabit)) Then
                        MessageBox.Show("Dana yg disetujui melebihi sisa alokasi babit " & "(" & oBabitProposal.BabitAllocation.SisaBabit & ")")
                        Exit Sub
                    End If
                    If (txtNoPersetujuan.Text.Trim = String.Empty) Then
                        MessageBox.Show("No Persetujuan Harus Diisi")
                        Exit Sub
                    End If
                    oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Disetujui, Short)
                    oBabitProposal.KTBApprovalAmount = txtPersetujuan.Text
                    oBabitProposal.TglTerimaEvidance = DateTime.Now
                ElseIf Process = CType(EnumBabit.StatusBabitProposal.Proses, Short) Then
                    oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Proses, Short)
                ElseIf Process = CType(EnumBabit.StatusBabitProposal.Validasi, Short) Then
                    oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Validasi, Short)
                ElseIf Process = CType(EnumBabit.StatusBabitProposal.Tolak, Short) Then
                    oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Tolak, Short)
                ElseIf Process = CType(EnumBabit.StatusBabitProposal.Batal, Short) Then
                    oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Batal, Short)
                End If
                oBabitProposal.NoPersetujuan = txtNoPersetujuan.Text
                arl.Add(oBabitProposal)
            End If
            i = i + 1
        Next

        If (arl.Count > 0) Then
            If (New BabitSalesComm.BabitProposalFacade(User).UpdateBabitProposal(arl) = 1) Then
                MessageBox.Show(SR.UpdateSucces)
                BindDataGrid(dtgList.CurrentPageIndex)
            Else
                MessageBox.Show(SR.UpdateFail)
            End If
        Else
            MessageBox.Show("Data Babit Proposal khusus belum di pilih")
        End If
    End Sub
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitKhususListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Pengajuan BABIT Khusus ")
        End If
    End Sub
    Private blnPrivilegeDetails As Boolean
    Private blnPrivilegeDownload As Boolean
    Private blnPrivilegeUploadRealization As Boolean
    Private blnPrivilegeDownloadRealization As Boolean

    Private Function CekPrivilegeStatusProses() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitKhususStatusProses_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekPrivilegeStatusTolak() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitKhususListStatusTolak_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekPrivilegeStatusDisetujui() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitKhususListStatusDisetujui_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub InitiateActionIcon()
        blnPrivilegeDetails = CekPrivilegeDetails()
        blnPrivilegeDownload = CekPrivilegeDownload()
        blnPrivilegeUploadRealization = CekPrivilegeUploadRealization()
        blnPrivilegeDownloadRealization = CekPrivilegeDownloadRealization()
    End Sub

    Private Function CekPrivilegeDetails() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitKhususListViewDetail_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekPrivilegeDownload() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitKhususListDownload_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekPrivilegeUploadRealization() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitKhusus_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekPrivilegeDownloadRealization() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitKhususDownloadListRealization_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekPrivilegeStatusBatal() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitKhususListStatusBatal_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekPrivilegeStatusValidation() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitKhususValidationStatus_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        objDealer = CType(Session("DEALER"), Dealer)
        InitiateAuthorization()
        InitiateActionIcon()
        If (Not IsPostBack) Then
            ViewState("currSortColumn") = "NoPengajuan"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                btnSave.Visible = False
                dtgList.Columns(7).Visible = False
                txtKodeDealer.Text = objDealer.DealerCode
                txtKodeDealer.Enabled = False
                pnlSearch.Visible = False
                pnlHead.Visible = False
            ElseIf (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
                btnSave.Visible = True
                lblKodeDealer.Attributes("onclick") = "ShowPPDealerSelection();"
                txtKodeDealer.Enabled = True
                pnlHead.Visible = True
            End If
            lblKodeDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            BindJenisKegiatan()
            BindMonth()
            BindTahun()
            BindChangeStatus()
            GetSessionCriteria()

            'add security
            If (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
                If Not CekPrivilegeStatusProses() Then
                    SetCompPrivilage(True, False)
                Else
                    SetCompPrivilage(True, True)
                End If

                If Not CekPrivilegeStatusTolak() Then
                    SetCompPrivilage(True, False)
                Else
                    SetCompPrivilage(True, True)
                End If

                If Not CekPrivilegeStatusDisetujui() Then
                    SetCompPrivilage(True, False)
                Else
                    SetCompPrivilage(True, True)
                End If
            Else
                ' case dealer
                If Not CekPrivilegeStatusBatal() Then
                    SetCompPrivilage(False, False)
                Else
                    SetCompPrivilage(False, True)
                End If
                If Not CekPrivilegeStatusValidation() Then
                    SetCompPrivilage(False, False)
                Else
                    SetCompPrivilage(False, True)
                End If
            End If
        Else
            If Request.Form("hdnValNew") = "1" Then
                btnProcess_Click(Nothing, Nothing)
                hdnValNew.Value = "-1"
            ElseIf Request.Form("hdnValNew") = "0" Then
                hdnValNew.Value = "-1"
            End If
        End If
    End Sub

    Private Function SetCompPrivilage(ByVal blnKTB As Boolean, ByVal blnAccess As Boolean)
        If blnKTB Then
            'btnProcess.Enabled = blnAccess
            btnSave.Enabled = blnAccess
            'dtgList.Columns(0).Visible = blnAccess  ' kolom centang item
        Else
            'btnProcess.Enabled = blnAccess
            'dtgList.Columns(0).Visible = blnAccess  ' kolom centang item
        End If
    End Function

    Private Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Update(ddlStatus.SelectedValue)
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If (CInt(ddlTahun.SelectedValue) <> -1 And CInt(ddlTahunTo.SelectedValue) <> -1) Then
            If (CInt(ddlTahun.SelectedValue) = CInt(ddlTahunTo.SelectedValue)) Then
                If (CInt(ddlStart.SelectedValue) <> -1 And CInt(ddlEnd.SelectedValue) <> -1) Then
                    If CInt(ddlStart.SelectedValue) > CInt(ddlEnd.SelectedValue) Then
                        MessageBox.Show("Periode awal harus lebih kecil dari akhir periode")
                        Exit Sub
                    End If
                End If
            ElseIf (CInt(ddlTahun.SelectedValue) > CInt(ddlTahunTo.SelectedValue)) Then
                MessageBox.Show("Periode awal harus lebih kecil dari akhir periode")
                Exit Sub
            End If
        End If
        dtgList.CurrentPageIndex = 0
        BindDataGrid(dtgList.CurrentPageIndex)
    End Sub

    Private Sub dtgList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgList.ItemDataBound
        If (e.Item.ItemIndex >= 0) Then
            Dim lNum As LiteralControl = New LiteralControl(e.Item.ItemIndex + 1 + (dtgList.CurrentPageIndex * dtgList.PageSize))
            e.Item.Cells(1).Controls.Add(lNum)

            Dim oBabitProposal As BabitProposal = CType(e.Item.DataItem, BabitProposal)
            Dim lblActivityType As Label = CType(e.Item.FindControl("lblActivityType"), Label)
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lbtnDownload As LinkButton = CType(e.Item.FindControl("lbtnDownload"), LinkButton)
            Dim txtPersetujuan As TextBox = CType(e.Item.FindControl("txtPersetujuan"), TextBox)
            Dim txtNoPersetujuan As TextBox = CType(e.Item.FindControl("txtNoPersetujuan"), TextBox)
            txtPersetujuan.Enabled = False
            txtNoPersetujuan.Enabled = False

            If (oBabitProposal.FileName <> String.Empty) Then
                lbtnDownload.Visible = True
            End If

            If (oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Baru, Short)) Then
                lbtnEdit.Visible = True
                lblStatus.Text = EnumBabit.StatusBabitProposal.Baru.ToString()
            ElseIf (oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Batal, Short)) Then
                lblStatus.Text = EnumBabit.StatusBabitProposal.Batal.ToString()
            ElseIf (oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Disetujui, Short)) Then
                lblStatus.Text = EnumBabit.StatusBabitProposal.Disetujui.ToString()
            ElseIf (oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Proses, Short)) Then
                lblStatus.Text = EnumBabit.StatusBabitProposal.Proses.ToString()
                txtPersetujuan.Enabled = True
                txtNoPersetujuan.Enabled = True
            ElseIf (oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Tolak, Short)) Then
                lblStatus.Text = EnumBabit.StatusBabitProposal.Tolak.ToString()
            ElseIf (oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Validasi, Short)) Then
                lblStatus.Text = EnumBabit.StatusBabitProposal.Validasi.ToString()
            End If

            If (objDealer.TitleDealer = EnumDealerTittle.DealerTittle.KTB.ToString()) Then
                lbtnEdit.Visible = True
                'Dim ktbid As Integer = 0
                ''cek if the proposal CreatedBy ktb user, so it can be edit by ktb
                'Try
                '    ktbid = CInt(oBabitProposal.CreatedBy.Substring(0, 6))
                '    If (ktbid = objDealer.ID) Then
                '        If (oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Baru, Short)) Then
                '            lbtnEdit.Visible = True
                '        End If
                '    Else
                '        lbtnEdit.Visible = False
                '    End If
                'Catch ex As Exception
                '    lbtnEdit.Visible = False
                'End Try
            End If

            If (oBabitProposal.ActivityType = CType(EnumBabit.BabitProposalType.Even, Short)) Then
                lblActivityType.Text = "Event" 'EnumBabit.BabitProposalType.Even.ToString()
            ElseIf (oBabitProposal.ActivityType = CType(EnumBabit.BabitProposalType.Iklan, Short)) Then
                lblActivityType.Text = EnumBabit.BabitProposalType.Iklan.ToString()
            ElseIf (oBabitProposal.ActivityType = CType(EnumBabit.BabitProposalType.Pameran, Short)) Then
                lblActivityType.Text = EnumBabit.BabitProposalType.Pameran.ToString()
            End If

            Dim lbtnUploadReal As LinkButton = CType(e.Item.FindControl("lbtnUploadReal"), LinkButton)
            lbtnUploadReal.Attributes("onClick") = GeneralScript.GetPopUpEventReference("../Babit/FrmBabitProposalUploadRealization.aspx?id=" & oBabitProposal.ID, "", 360, 700, "ViewForm")

            Dim lbtnDownloadReal As LinkButton = CType(e.Item.FindControl("lbtnDownloadReal"), LinkButton)
            If oBabitProposal.BabitRealizationFile.Trim = String.Empty Then
                lbtnDownloadReal.Visible = False
            Else
                lbtnDownloadReal.Visible = True
            End If

            'add security
            Dim lbtnDetails As LinkButton = CType(e.Item.FindControl("lbtnDetails"), LinkButton)
            If Not blnPrivilegeDetails Then
                lbtnDetails.Visible = False
            End If
            If Not blnPrivilegeDownload Then
                lbtnDownload.Visible = False
            End If
            If Not blnPrivilegeUploadRealization Then
                lbtnUploadReal.Visible = False
            End If
            If Not blnPrivilegeDownloadRealization Then
                lbtnDownloadReal.Visible = False
            End If

        End If
    End Sub

    Private Sub dtgList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgList.SortCommand
        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If
        BindDataGrid(dtgList.CurrentPageIndex)
    End Sub

    Private Sub dtgList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgList.PageIndexChanged
        dtgList.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgList.CurrentPageIndex)
    End Sub

    Private Sub dtgList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgList.ItemCommand
        Select Case e.CommandName
            Case "edit"
                SetSessionCriteria()
                Response.Redirect("../Babit/FrmPengajuanBabitKhusus.aspx?id=" & e.CommandArgument & "&Mode=Edit")
            Case "detail"
                SetSessionCriteria()
                Response.Redirect("../Babit/FrmPengajuanBabitKhusus.aspx?id=" & e.CommandArgument & "&Mode=View")
            Case "download"
                Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("BabitKhususDir") & "\" & e.CommandArgument
                Response.Redirect("../Download.aspx?file=" & PathFile)
            Case "uploadReal"
                BindDataGrid(dtgList.CurrentPageIndex)
                'Popup
            Case "downloadReal"
                Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("RealisasiBabit") & "\" & e.CommandArgument
                Response.Redirect("../Download.aspx?file=" & PathFile)
        End Select
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim arlToupdate As ArrayList = New ArrayList
        Dim i As Integer = 0
        For Each item As DataGridItem In dtgList.Items
            Dim Amount As Decimal = 0
            Dim txtPersetujuan As TextBox = CType(item.FindControl("txtPersetujuan"), TextBox)
            Dim lblPengajuanDana As Label = CType(item.FindControl("lblPengajuanDana"), Label)
            Dim chkItemChecked As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)

            Dim oBabitProposal As BabitProposal
            oBabitProposal = New BabitSalesComm.BabitProposalFacade(User).Retrieve(Convert.ToInt32(dtgList.DataKeys().Item(i)))

            If oBabitProposal.BabitAllocation Is Nothing Then
                MessageBox.Show("Proposal dengan nomor : " & oBabitProposal.NoPengajuan & " belum dialokasikan")
                Exit Sub
            End If

            If txtPersetujuan.Text.Trim <> String.Empty Then
                Amount += Convert.ToDecimal(txtPersetujuan.Text)

                If (Amount > Convert.ToDecimal(oBabitProposal.BabitAllocation.SisaBabit)) Then
                    MessageBox.Show("Dana yg disetujui melebihi sisa alokasi babit " & "(" & oBabitProposal.BabitAllocation.SisaBabit & ")")
                    Exit Sub
                End If

                If (Convert.ToDecimal(lblPengajuanDana.Text) < Convert.ToDecimal(txtPersetujuan.Text)) Then
                    MessageBox.Show("Persetujuan dana babit tidak boleh lebih besar dari jumlah pengajuan")
                    Exit Sub
                End If

                oBabitProposal.KTBApprovalAmount = CDec(txtPersetujuan.Text)
                arlToupdate.Add(oBabitProposal)
            End If

            i = i + 1
        Next
        If arlToupdate.Count > 0 Then
            Dim nresult As Integer = 0
            Try
                nresult = New BabitSalesComm.BabitProposalFacade(User).UpdateBabitProposal(arlToupdate)
                If nresult > 0 Then
                    MessageBox.Show(SR.UpdateSucces)
                Else
                    MessageBox.Show(SR.UpdateFail)
                End If
            Catch ex As Exception
                MessageBox.Show(SR.UpdateFail)
            End Try
        Else
            MessageBox.Show("Tidak Ada Data YAng Diupdate")
        End If
    End Sub

#End Region


End Class
