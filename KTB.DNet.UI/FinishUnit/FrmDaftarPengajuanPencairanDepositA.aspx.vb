Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Helper

Public Class FrmDaftarPengajuanPencairanDepositA
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblCode As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTipePengajuan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents icPeriodeFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPeriodeTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtNoPengajuan As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgDaftarPengajuanPencairanDepositA As System.Web.UI.WebControls.DataGrid
    Protected WithEvents cbPeriode As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnValidasi As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatalValidasi As System.Web.UI.WebControls.Button
    Protected WithEvents btnKonfirmasi As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatalKonfirmasi As System.Web.UI.WebControls.Button
    Protected WithEvents btnSetuju As System.Web.UI.WebControls.Button
    Protected WithEvents btnTolak As System.Web.UI.WebControls.Button
    Protected WithEvents ddlStatusPengajuan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnProses As System.Web.UI.WebControls.Button
    Protected WithEvents ddlAction As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblUbahStatus As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents txtNoReg As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlProductCategory As System.Web.UI.WebControls.DropDownList

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
    Private arlDepositAPencairan As ArrayList = New ArrayList
    Private arlDepositAPencairanFilter As ArrayList = New ArrayList

    Private _DepositAPencairanHFacade As New FinishUnit.DepositAPencairanHFacade(User)

    Dim sHelper As New SessionHelper
    Dim objUserInfo As UserInfo
    Dim IsDealer As Boolean = True
    Const DocTypePengajuan = 1
    Const DocTypeKuitansi = 2
#End Region

#Region "Custom Method"
    Sub BindTipePengajuan(ByVal ddl As DropDownList)
        ddl.DataSource = [Enum].GetNames(GetType(EnumDepositA.TipePengajuan))
        ddl.DataBind()
        ddl.Items.Insert(0, New ListItem("Silakan Pilih", "0"))
    End Sub

    Private Sub AddPeriodCriteria(ByVal criterias As CriteriaComposite, ByVal ColumnName As String)
        Dim dtStart As DateTime = New DateTime(icPeriodeFrom.Value.Year, icPeriodeFrom.Value.Month, _
                                    icPeriodeFrom.Value.Day, 0, 0, 0)
        Dim dtEnd As DateTime = New DateTime(icPeriodeTo.Value.Year, icPeriodeTo.Value.Month, _
                                    icPeriodeTo.Value.Day, 0, 0, 0)
        dtEnd = dtEnd.AddDays(1)

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), ColumnName, MatchType.GreaterOrEqual, dtStart))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), ColumnName, MatchType.Lesser, dtEnd))
    End Sub

    ''Private Function IsExist(ByVal DealerCode As String, ByVal arl As ArrayList) As Boolean
    ''    Dim bResult As Boolean = False
    ''    For Each item As DepositAPencairanH In arl
    ''        If item.Dealer.DealerCode.Trim().ToUpper() = DealerCode.Trim().ToUpper() Then
    ''            bResult = True
    ''            Exit For
    ''        End If
    ''    Next
    ''    Return bResult
    ''End Function

    Sub BindDatagridDaftarPencairan(ByVal pageIndex As Integer)
        Dim oDealer As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)
        If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If txtKodeDealer.Text.Trim = String.Empty Then
                MessageBox.Show("Tentukan kode dealer terlebih dahulu")
                Exit Sub
            End If
        End If
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (txtKodeDealer.Text.Trim() <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        End If
        If cbPeriode.Checked Then
            AddPeriodCriteria(criterias, "CreatedTime")
        End If


        If CInt(ddlProductCategory.SelectedValue) > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "ProductCategory.ID", MatchType.Exact, ddlProductCategory.SelectedValue))
        End If

        'Dim selectedTipe As TipePengajuan = CType([Enum].Parse(GetType(TipePengajuan), ddlTipePengajuan.SelectedValue), TipePengajuan)
        Dim selectedTipe As Integer = ddlTipePengajuan.SelectedIndex
        If selectedTipe > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "Type", MatchType.Exact, selectedTipe))
        End If

        If txtNoPengajuan.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "NoSurat", MatchType.[Partial], txtNoPengajuan.Text.Trim))
        End If

        If IsDealer Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "Status", MatchType.InSet, GetStatusPencairanDealerEnumValues()))
        Else
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "Status", MatchType.InSet, GetStatusPencairanKTBEnumValues()))
        End If

        Dim selectedStatus As Integer = CInt(ddlStatusPengajuan.SelectedValue)
        If selectedStatus <> 99 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "Status", MatchType.Exact, selectedStatus))
        End If

        If txtNoReg.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "NoReg", MatchType.Exact, txtNoReg.Text))
        End If

        SetButtonbyStatus(selectedStatus)

        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        If (Not IsNothing(ViewState("currSortColumn"))) And (Not IsNothing(ViewState("currSortDirection"))) Then
            sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositAPencairanH), ViewState("currSortColumn"), ViewState("currSortDirection")))
        Else
            sortColl = Nothing
        End If

        arlDepositAPencairan = New FinishUnit.DepositAPencairanHFacade(User).Retrieve(criterias, sortColl)
        Dim DealerCode As String = String.Empty
        arlDepositAPencairanFilter.Clear()
        For Each item As DepositAPencairanH In arlDepositAPencairan
            'If (Not IsExist(item.Dealer.DealerCode, arlDepositAPencairanFilter)) Then
            arlDepositAPencairanFilter.Add(item)
            'End If
        Next

        If (arlDepositAPencairanFilter.Count > 0) Then
            dgDaftarPengajuanPencairanDepositA.Visible = True
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arlDepositAPencairanFilter, pageIndex, dgDaftarPengajuanPencairanDepositA.PageSize)
            dgDaftarPengajuanPencairanDepositA.DataSource = PagedList
            dgDaftarPengajuanPencairanDepositA.VirtualItemCount = arlDepositAPencairanFilter.Count()
            dgDaftarPengajuanPencairanDepositA.DataBind()

            'dgDaftarPengajuanPencairanDepositA.Visible = True
            'dgDaftarPengajuanPencairanDepositA.DataSource = arlDepositAPencairanFilter
            'dgDaftarPengajuanPencairanDepositA.DataBind()
            sHelper.SetSession("VDaftarPengajuan", arlDepositAPencairanFilter)
            If IsDealer = False Then
                lblUbahStatus.Visible = True
                ddlAction.Visible = True
                btnProses.Visible = True
            End If
        Else
            dgDaftarPengajuanPencairanDepositA.Visible = False
            lblUbahStatus.Visible = False
            ddlAction.Visible = False
            btnProses.Visible = False
            MessageBox.Show("Data tidak ditemukan")
        End If

    End Sub

    Private Sub InitNonMandatoryObject()
        cbPeriode.Checked = False
        icPeriodeFrom.Enabled = False
        icPeriodeTo.Enabled = False

        SetButtonbyStatus(ddlStatusPengajuan.SelectedValue)
    End Sub

    Sub BindStatusPencairanDealer(ByVal ddl As DropDownList)
        'ddl.DataSource = [Enum].GetNames(GetType(StatusPencairanDealer))
        'ddl.DataBind()
        ddl.Items.Add(New ListItem("Silakan Pilih", "99"))
        ddl.Items.Add(New ListItem("Baru", EnumDepositA.StatusPencairanDealer.Baru))
        ddl.Items.Add(New ListItem("Validasi", EnumDepositA.StatusPencairanDealer.Validasi))
        ddl.Items.Add(New ListItem("Konfirmasi", EnumDepositA.StatusPencairanDealer.Konfirmasi))
        ddl.Items.Add(New ListItem("Setuju", EnumDepositA.StatusPencairanDealer.Setuju))
        ' ddl.Items.Add(New ListItem("Tolak", StatusPencairanDealer.Tolak))
        ddl.Items.Add(New ListItem("Blok", EnumDepositA.StatusPencairanDealer.Blok))
        ddl.Items.Add(New ListItem("Selesai", EnumDepositA.StatusPencairanDealer.Selesai))

    End Sub

    Sub BindStatusPencairanKTB(ByVal ddl As DropDownList)
        'ddl.DataSource = [Enum].GetNames(GetType(StatusPencairanKTB))
        'ddl.DataBind()
        'ddl.Items.Insert(0, New ListItem("Silakan Pilih", "99"))

        ddl.Items.Add(New ListItem("Silakan Pilih", "99"))
        ddl.Items.Add(New ListItem("Validasi", EnumDepositA.StatusPencairanKTB.Validasi))
        ddl.Items.Add(New ListItem("Konfirmasi", EnumDepositA.StatusPencairanKTB.Konfirmasi))
        ddl.Items.Add(New ListItem("Setuju", EnumDepositA.StatusPencairanKTB.Setuju))
        'ddl.Items.Add(New ListItem("Tolak", StatusPencairanKTB.Tolak))
        ddl.Items.Add(New ListItem("Blok", EnumDepositA.StatusPencairanKTB.Blok))
        ddl.Items.Add(New ListItem("Selesai", EnumDepositA.StatusPencairanKTB.Selesai))

    End Sub

    Private Function GetStatusPencairanKTBEnumValues() As String
        Dim strResult As String = " ("
        Dim item As String = String.Empty

        For Each item In [Enum].GetValues(GetType(EnumDepositA.StatusPencairanKTB))
            'arrResult = objEnum.GetValues(GetType(objEnum))
            strResult = strResult & item.ToString & ","
        Next
        strResult = Left(strResult, strResult.Length - 1) & ")"
        Return strResult
    End Function

    Private Function GetStatusPencairanDealerEnumValues() As String
        Dim strResult As String = " ("
        Dim item As String = String.Empty

        For Each item In [Enum].GetValues(GetType(EnumDepositA.StatusPencairanDealer))
            'arrResult = objEnum.GetValues(GetType(objEnum))
            strResult = strResult & item.ToString & ","
        Next
        strResult = Left(strResult, strResult.Length - 1) & ")"
        Return strResult
    End Function

    Private Sub ProsesFLow(ByVal Status As Integer)
        'validasi inputan
    End Sub

    Private Function InsertHistory(ByVal NoSurat As String, ByVal OldStatus As Integer, ByVal NewStatus As Integer, ByVal DocType As Integer)
        Dim objHistoryDepositAPencairan As DepositAStatusHistory = New DepositAStatusHistory
        objHistoryDepositAPencairan.DocNumber = NoSurat
        objHistoryDepositAPencairan.OldStatus = OldStatus
        objHistoryDepositAPencairan.NewStatus = NewStatus
        objHistoryDepositAPencairan.DocType = DocType
        Dim nResult As Integer = -1
        nResult = New FinishUnit.DepositAStatusHistoryFacade(User).Insert(objHistoryDepositAPencairan)
        Return nResult
    End Function

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        objUserInfo = sHelper.GetSession("LOGINUSERINFO")
        If objUserInfo.Dealer.Title = 1 Then
            IsDealer = False
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        Else
            IsDealer = True
            'lblSearchDealer.Visible = False
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            'txtKodeDealer.Text = objUserInfo.Dealer.DealerCode
            'txtKodeDealer.ReadOnly = True
            txtKodeDealer.Attributes.Add("readonly", "readonly")
        End If

        If Not IsPostBack Then
            BindTipePengajuan(ddlTipePengajuan)
            If IsDealer Then
                BindStatusPencairanDealer(ddlStatusPengajuan)
            Else
                BindStatusPencairanKTB(ddlStatusPengajuan)
            End If
            InitNonMandatoryObject()
            GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True)

            'ElseIf Session("IsBindDataGrid") Then
            '    BindDatagridDaftarPencairan()
        End If
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgDaftarPengajuanPencairanDepositA.CurrentPageIndex = 0
        BindDatagridDaftarPencairan(dgDaftarPengajuanPencairanDepositA.CurrentPageIndex)
    End Sub

    Private Sub dgDaftarPengajuanPencairanDepositA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDaftarPengajuanPencairanDepositA.ItemDataBound
        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgDaftarPengajuanPencairanDepositA.CurrentPageIndex * dgDaftarPengajuanPencairanDepositA.PageSize)

            Dim objPencairanDepositAH As DepositAPencairanH = CType(e.Item.DataItem, DepositAPencairanH)

            Dim lblTipe As Label = CType(e.Item.FindControl("lblTipe"), Label)
            Dim selectedTipe As EnumDepositA.TipePengajuan = CType([Enum].Parse(GetType(EnumDepositA.TipePengajuan), objPencairanDepositAH.Type), EnumDepositA.TipePengajuan)
            Dim SelectedTipeName As String = selectedTipe.GetName(GetType(EnumDepositA.TipePengajuan), objPencairanDepositAH.Type)
            lblTipe.Text = SelectedTipeName

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)

            Dim selectedStatus As EnumDepositA.StatusPencairanDealer = CType([Enum].Parse(GetType(EnumDepositA.StatusPencairanDealer), objPencairanDepositAH.Status), EnumDepositA.StatusPencairanDealer)
            Dim SelectedStatusName As String = selectedStatus.GetName(GetType(EnumDepositA.StatusPencairanDealer), objPencairanDepositAH.Status)
            lblStatus.Text = SelectedStatusName


            Dim txtJumlahDisetujui As TextBox = CType(e.Item.FindControl("txtJumlahDisetujui"), TextBox)
            txtJumlahDisetujui.Text = Format(objPencairanDepositAH.ApprovalAmount, "#,###")
            If txtJumlahDisetujui.Text = String.Empty Then
                txtJumlahDisetujui.Text = "0"
            End If

            If selectedStatus <> EnumDepositA.StatusPencairanDealer.Konfirmasi Then
                txtJumlahDisetujui.ReadOnly = True
            Else
                txtJumlahDisetujui.ReadOnly = False
            End If

            If selectedTipe = EnumDepositA.TipePengajuan.Offset And objPencairanDepositAH.DNNumber.Trim <> "" Then
                txtJumlahDisetujui.Text = Format(objPencairanDepositAH.DealerAmount, "#,###")
                txtJumlahDisetujui.ReadOnly = True
            End If

            Dim txtAlasan As TextBox = CType(e.Item.FindControl("txtAlasan"), TextBox)
            txtAlasan.Text = objPencairanDepositAH.KTBReason.Trim

            Dim lbViewDetail As LinkButton = CType(e.Item.FindControl("lbViewDetail"), LinkButton)
            lbViewDetail.Attributes("OnClick") = "showPopUp('../PopUp/PopUpPengajuanPencairanDepositAViewEdit.aspx?id=" & objPencairanDepositAH.ID & " ','',500,760,'');"
            lbViewDetail.ToolTip = "Lihat Detail"

            If IsDealer Then
                txtAlasan.ReadOnly = True
                txtJumlahDisetujui.ReadOnly = True
            Else
                txtAlasan.ReadOnly = False
                ' txtJumlahDisetujui.ReadOnly = False
            End If

            Dim lbViewFlow As LinkButton = CType(e.Item.FindControl("lbViewFlow"), LinkButton)
            lbViewFlow.Attributes("OnClick") = "showPopUp('../PopUp/PopUpFlowPencairanDepositA.aspx?DealerID=" & objPencairanDepositAH.Dealer.ID & "&NoReg=" & objPencairanDepositAH.NoReg & "&NoSurat=" & objPencairanDepositAH.NoSurat.ToString & " ','',500,600,'');"
            lbViewFlow.ToolTip = "Flow Dok"


            Dim lbViewStatusHistory As LinkButton = CType(e.Item.FindControl("lbViewStatus"), LinkButton)
            lbViewStatusHistory.Attributes("OnClick") = "showPopUp('../PopUp/PopUpHistoryPencairanDepositA.aspx?DealerID=" & objPencairanDepositAH.Dealer.ID & "&NoSurat=" & objPencairanDepositAH.NoSurat.ToString & "','',500,500,'');"
            lbViewStatusHistory.ToolTip = "History"

            Dim lbDelete As LinkButton = CType(e.Item.FindControl("lbDelete"), LinkButton)
            lbDelete.Attributes.Add("onclick", "return confirm('Anda yakin menghapus pencairan ini?');")
            lbDelete.ToolTip = "Hapus"
            lbDelete.CommandArgument = objPencairanDepositAH.ID

            If objPencairanDepositAH.Status > 0 Then
                lbDelete.Visible = False
            Else
                lbDelete.Visible = True
            End If
            If (objPencairanDepositAH.Type = CType(EnumDepositA.TipePengajuan.Offset, Short) OrElse objPencairanDepositAH.Type = CType(EnumDepositA.TipePengajuan.CashIncidental, Short)) _
                AndAlso objPencairanDepositAH.Status = CType(EnumDepositA.StatusPencairanDealer.Validasi, Short) Then
                'Dim txtJumlahDisetujui As TextBox = e.Item.FindControl("txtJumlahDisetujui")
                txtJumlahDisetujui.Enabled = False ' .ReadOnly = True
            End If

        ElseIf (e.Item.ItemType = ListItemType.Footer) Then
        End If
    End Sub

    Private Sub dgDaftarPengajuanPencairanDepositA_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDaftarPengajuanPencairanDepositA.PageIndexChanged
        dgDaftarPengajuanPencairanDepositA.CurrentPageIndex = e.NewPageIndex
        BindDatagridDaftarPencairan(e.NewPageIndex)
    End Sub

    Private Sub dgDaftarPengajuanPencairanDepositA_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDaftarPengajuanPencairanDepositA.ItemCommand
        If e.CommandName = "Delete" Then
            Dim objDepositPencairan As New DepositAPencairanH
            objDepositPencairan = New DepositAPencairanHFacade(User).Retrieve(CInt(e.CommandArgument))
            If objDepositPencairan.ID > 0 Then
                objDepositPencairan.RowStatus = -1

                Dim intResult = New DepositAPencairanHFacade(User).Update(objDepositPencairan)
                If intResult > -1 Then
                    Dim oAnnualDepositAHeaderArr As ArrayList
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "Dealer.ID", MatchType.Exact, objDepositPencairan.Dealer.ID))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "NettoAmount", MatchType.Exact, CDbl(objDepositPencairan.ApprovalAmount)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "LastUpdateTime", MatchType.GreaterOrEqual, objDepositPencairan.CreatedTime))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "LastUpdateTime", MatchType.LesserOrEqual, DateAdd(DateInterval.Minute, 1, objDepositPencairan.CreatedTime)))

                    oAnnualDepositAHeaderArr = New AnnualDepositAHeaderFacade(User).Retrieve(criterias)
                    If oAnnualDepositAHeaderArr.Count > 0 Then
                        For Each oAnnualDepositAHeader As AnnualDepositAHeader In oAnnualDepositAHeaderArr
                            oAnnualDepositAHeader.Status = 0 'StatusPencairanTahunan.DiAjukan
                            Dim intResultAnnual = New AnnualDepositAHeaderFacade(User).Update(oAnnualDepositAHeader)
                        Next
                    End If

                    MessageBox.Show(SR.DeleteSucces)
                    BindDatagridDaftarPencairan(dgDaftarPengajuanPencairanDepositA.CurrentPageIndex)
                Else
                    MessageBox.Show(SR.DeleteFail)
                End If
            End If
        ElseIf e.CommandName = "ViewDetail" Then
            btnSearch_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub cbPeriode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPeriode.CheckedChanged
        If cbPeriode.Checked Then
            icPeriodeFrom.Enabled = True
            icPeriodeTo.Enabled = True
        Else
            icPeriodeFrom.Enabled = False
            icPeriodeTo.Enabled = False
        End If
    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        'If Not SecurityProvider.Authorize(context.User, SR.DepositAView_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=FINISH UNIT - Daftar Pengajuan Deposit A")
        'End If

        If Not SecurityProvider.Authorize(context.User, SR.DepositA_proses_pengajuan_pencairan_depoA_lihat_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Deposit A - Lihat Proses Pengajuan Pencairan Deposit A")
            Me.btnProses.Visible = False
        End If

        If Not SecurityProvider.Authorize(context.User, SR.DepositA_proses_pengajuan_pencairan_depoA_lihat_detail_Privilege) Then
            Me.dgDaftarPengajuanPencairanDepositA.Columns(12).Visible = False
        End If

    End Sub
#End Region

#Region "Internal Enum"
    'Private Enum TipePengajuan
    '    Offset = 1
    '    'CashAnnual = 2
    '    CashTahunan = 2
    '    CashIncidental = 3
    '    CashInterest = 4
    'End Enum

    'Private Enum StatusPencairanDealer
    '    Baru = 0
    '    Validasi = 1
    '    Konfirmasi = 10
    '    Setuju = 11
    '    Tolak = 12
    '    Blok = 14
    '    Selesai = 16
    'End Enum

    'Private Enum StatusPencairanKTB
    '    Baru = 0
    '    Validasi = 1
    '    Konfirmasi = 10
    '    Setuju = 11
    '    Tolak = 12
    '    Blok = 14
    '    Selesai = 16
    'End Enum
#End Region

    Private Sub SetButtonbyStatus(ByVal Index As Integer)
        If IsDealer Then
            Dim selectedStatusDealer As EnumDepositA.StatusPencairanDealer = CType([Enum].Parse(GetType(EnumDepositA.StatusPencairanDealer), Index), EnumDepositA.StatusPencairanDealer)
            Dim SelectedStatusDealerName As String = selectedStatusDealer.GetName(GetType(EnumDepositA.StatusPencairanDealer), selectedStatusDealer)

            btnKonfirmasi.Visible = False
            btnBatalKonfirmasi.Visible = False
            btnSetuju.Visible = False
            btnTolak.Visible = False
            Select Case Index
                Case 0
                    btnValidasi.Enabled = True
                    btnBatalValidasi.Enabled = False
                Case 1
                    btnValidasi.Enabled = True
                    btnBatalValidasi.Enabled = True
                Case Else
                    btnValidasi.Enabled = True
                    btnBatalValidasi.Enabled = False
            End Select

            lblUbahStatus.Visible = False
            ddlAction.Visible = False
            btnProses.Visible = False

        Else
            'Dim selectedStatusKTB As StatusPencairanKTB = CType([Enum].Parse(GetType(StatusPencairanKTB), Index), StatusPencairanKTB)
            'Dim SelectedStatusKTBName As String = selectedStatusKTB.GetName(GetType(StatusPencairanKTB), selectedStatusKTB)

            btnValidasi.Visible = False
            btnBatalValidasi.Visible = False
            lblUbahStatus.Visible = True
            ddlAction.Visible = True
            btnProses.Visible = True

            'Select Case Index
            '    Case 1
            '        btnKonfirmasi.Enabled = True
            '        btnBatalKonfirmasi.Enabled = False
            '        btnSetuju.Enabled = False
            '        btnTolak.Enabled = False
            '    Case 10
            '        btnKonfirmasi.Enabled = False
            '        btnBatalKonfirmasi.Enabled = True
            '        btnSetuju.Enabled = True
            '        btnTolak.Enabled = True
            '    Case 11, 12
            '        btnKonfirmasi.Enabled = False
            '        btnBatalKonfirmasi.Enabled = False
            '        btnSetuju.Enabled = False
            '        btnTolak.Enabled = False
            '    Case Else
            '        btnKonfirmasi.Enabled = False
            '        btnBatalKonfirmasi.Enabled = False
            '        btnSetuju.Enabled = False
            '        btnTolak.Enabled = False
            'End Select
        End If
    End Sub

    Private Sub ddlStatusPengajuan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlStatusPengajuan.SelectedIndexChanged
        SetButtonbyStatus(ddlStatusPengajuan.SelectedIndex)
    End Sub

    Private Sub btnValidasi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidasi.Click
        ' Dim strPengajuanIDs As String = GetSelectedPengajuans(dgDaftarPengajuanPencairanDepositA)
        ' If strPengajuanIDs.Length > 0 Then
        ProsesPengajuan("Validasi")
        'Else
        '   MessageBox.Show("Pilih Pengajuan terlebih dahulu!")
        '  End If
    End Sub

    Private Function GetSelectedPengajuans(ByVal dg As DataGrid) As String
        Dim i As Integer = 0
        Dim strResult As String = "("

        For Each item As DataGridItem In dgDaftarPengajuanPencairanDepositA.Items
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If (chk.Checked) Then
                strResult = strResult & dgDaftarPengajuanPencairanDepositA.DataKeys().Item(i) & ","
            End If
            i = i + 1
        Next
        strResult = Left(strResult, strResult.Length - 1) & ")"
    End Function

    Private Function IsDataInDTGBValid(ByVal nStatus As Integer) As Boolean
        Select Case nStatus
            Case CType(EnumDepositA.StatusPencairanDealer.Setuju, Integer)
                For Each di As DataGridItem In Me.dgDaftarPengajuanPencairanDepositA.Items
                    Dim txtJumlahDisetujui As TextBox = di.FindControl("txtJumlahDisetujui")
                    Dim chkItemChecked As CheckBox = di.FindControl("chkItemChecked")
                    If chkItemChecked.Checked AndAlso (txtJumlahDisetujui.Text.Trim = "" OrElse Val(txtJumlahDisetujui.Text.Trim) <= 0) Then
                        Return False
                    End If
                Next
            Case 10000

        End Select
        Return True
    End Function

    Private Sub ProsesPengajuan(ByVal CommandName As String)
        Dim nResult As Integer = -1
        Dim Message As String
        Dim intStatus As Byte

        Select Case CommandName
            Case "Validasi"
                Message = "Validasi"
                intStatus = EnumDepositA.StatusPencairanDealer.Validasi
                ddlStatusPengajuan.SelectedValue = EnumDepositA.StatusPencairanDealer.Validasi

                'Perlu di buatkan method untuk looping dan update data sesuai yang di inginkan
            Case "BatalValidasi"
                Message = "Batal Validasi"
                intStatus = EnumDepositA.StatusPencairanDealer.Baru
                ddlStatusPengajuan.SelectedValue = EnumDepositA.StatusPencairanDealer.Baru

            Case "Konfirmasi"
                Message = "Konfirmasi"
                intStatus = EnumDepositA.StatusPencairanDealer.Konfirmasi
                ddlStatusPengajuan.SelectedValue = EnumDepositA.StatusPencairanDealer.Konfirmasi

            Case "BatalKonfirmasi"
                Message = "Batal Konfirmasi"
                intStatus = EnumDepositA.StatusPencairanDealer.Validasi
                ddlStatusPengajuan.SelectedValue = EnumDepositA.StatusPencairanDealer.Validasi
            Case "Setuju"
                Message = "Setuju"
                intStatus = EnumDepositA.StatusPencairanDealer.Setuju
                ddlStatusPengajuan.SelectedValue = EnumDepositA.StatusPencairanDealer.Setuju

            Case "BatalSetuju"
                Message = "Batal Setuju"
                intStatus = EnumDepositA.StatusPencairanDealer.Konfirmasi
                ddlStatusPengajuan.SelectedValue = EnumDepositA.StatusPencairanDealer.Konfirmasi

            Case "Tolak"
                Message = "Tolak"
                intStatus = EnumDepositA.StatusPencairanDealer.Tolak
                ddlStatusPengajuan.SelectedValue = EnumDepositA.StatusPencairanDealer.Tolak

            Case "Blok"
                Message = "Blok"
                intStatus = EnumDepositA.StatusPencairanDealer.Blok
                ddlStatusPengajuan.SelectedValue = EnumDepositA.StatusPencairanDealer.Blok

        End Select

        If Not IsDataInDTGBValid(intStatus) Then
            MessageBox.Show("Proses " & Message & " gagal")
            Exit Sub
        End If
        Dim intStatOri As Integer = intStatus
        Dim msgOri As String = Message
        For Each item As DataGridItem In dgDaftarPengajuanPencairanDepositA.Items
            Dim chkCek As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            intStatus = intStatOri
            Message = msgOri
            If chkCek.Checked Then
                Dim ArrPengajuan = New ArrayList
                If Not sHelper.GetSession("VDaftarPengajuan") Is Nothing Then
                    ArrPengajuan = sHelper.GetSession("VDaftarPengajuan")
                    Dim objDepositAPencairanH As DepositAPencairanH = CType(ArrPengajuan(item.ItemIndex), DepositAPencairanH)


                    'Validasi Selesai tidak dapat diubuah, harus dihapuis kwintansinya

                    If objDepositAPencairanH.Status = EnumDepositA.StatusPencairanKTB.Selesai Then
                        MessageBox.Show("Tidak bisa ubah status selesai")
                        Exit Sub
                    End If

                    If intStatus = EnumDepositA.StatusPencairanDealer.Blok Then
                        If objDepositAPencairanH.Status = EnumDepositA.StatusPencairanDealer.Baru OrElse objDepositAPencairanH.Status = EnumDepositA.StatusPencairanDealer.Validasi OrElse objDepositAPencairanH.Status = EnumDepositA.StatusPencairanDealer.Konfirmasi OrElse objDepositAPencairanH.Status = EnumDepositA.StatusPencairanDealer.Setuju Then

                        Else
                            MessageBox.Show("Tidak bisa ubah status")
                            Exit Sub
                        End If

                    End If

                    If intStatus = EnumDepositA.StatusPencairanDealer.Validasi Then
                        If objDepositAPencairanH.Status <> EnumDepositA.StatusPencairanDealer.Baru Then
                            MessageBox.Show("Tidak bisa ubah status")
                            Exit Sub
                        Else
                            If objDepositAPencairanH.Type = EnumDepositA.TipePengajuan.CashIncidental Then
                                intStatus = EnumDepositA.StatusPencairanDealer.Validasi
                                Message = "Validasi"
                            Else
                                intStatus = EnumDepositA.StatusPencairanDealer.Setuju
                                Message = "Setuju"
                            End If

                        End If
                        'If objDepositAPencairanH.Status <> StatusPencairanDealer.Konfirmasi Then
                        '    If objDepositAPencairanH.Status <> StatusPencairanDealer.Baru Then
                        '        MessageBox.Show("Tidak bisa ubah status")
                        '        Exit Sub
                        '    End If
                        'End If

                    ElseIf intStatus = EnumDepositA.StatusPencairanDealer.Konfirmasi Then
                        If objDepositAPencairanH.Status = EnumDepositA.StatusPencairanDealer.Setuju And objDepositAPencairanH.Type = EnumDepositA.TipePengajuan.Offset Then
                            '(objDepositAPencairanH.Type = TipePengajuan.CashInterest Or _
                            'objDepositAPencairanH.Type = TipePengajuan.Offset) Then
                            MessageBox.Show("Tidak bisa ubah status")
                            Exit Sub
                        End If
                        If objDepositAPencairanH.Type = EnumDepositA.TipePengajuan.CashAnnual Or objDepositAPencairanH.Type = EnumDepositA.TipePengajuan.CashInterest Then
                            intStatus = EnumDepositA.StatusPencairanDealer.Baru
                            Message = "Baru"
                        End If


                    ElseIf intStatus = EnumDepositA.StatusPencairanDealer.Setuju Then
                        If ddlTipePengajuan.SelectedIndex <> 2 And ddlTipePengajuan.SelectedIndex <> 4 Then
                            If objDepositAPencairanH.Status <> EnumDepositA.StatusPencairanDealer.Konfirmasi Then
                                MessageBox.Show("Tidak bisa ubah status")
                                Exit Sub
                            End If
                        End If
                    ElseIf intStatus = EnumDepositA.StatusPencairanDealer.Tolak Then
                        If objDepositAPencairanH.Status <> EnumDepositA.StatusPencairanDealer.Konfirmasi Then
                            MessageBox.Show("Tidak bisa ubah status")
                            Exit Sub
                        End If
                    End If

                    Dim txtJumlahDisetujui As TextBox = CType(item.FindControl("txtJumlahDisetujui"), TextBox)
                    If txtJumlahDisetujui.Text <> String.Empty Then
                        If CDec(txtJumlahDisetujui.Text) > objDepositAPencairanH.DealerAmount Then
                            MessageBox.Show("Jumlah Disetujui tidak boleh melebihi Jumlah Pengajuan")
                            Exit Sub
                        End If
                    End If

                    'insert history
                    nResult = InsertHistory(objDepositAPencairanH.NoSurat, objDepositAPencairanH.Status, intStatus, DocTypePengajuan)
                    If nResult > -1 Then
                        'Save Header
                        ' objDepositAPencairanH.Status = StatusPencairanDealer.Validasi
                        objDepositAPencairanH.Status = intStatus

                        objDepositAPencairanH.ApprovalAmount = txtJumlahDisetujui.Text
                        Dim txtAlasan As TextBox = CType(item.FindControl("txtAlasan"), TextBox)

                        objDepositAPencairanH.KTBReason = txtAlasan.Text
                        nResult = _DepositAPencairanHFacade.Update(objDepositAPencairanH)
                        If nResult <> -1 Then
                            'MessageBox.Show("Berhasil update menjadi " & objDepositAPencairanH.Status.ToString)
                            ' MessageBox.Show("Ubah Status berhasil")
                        Else
                            MessageBox.Show(SR.UpdateFail())
                            Exit Sub
                        End If
                    End If
                End If
            End If
        Next

        If nResult > -1 Then
            MessageBox.Show("Berhasil update menjadi " & Message)
            'If (CommandName = "Validasi") Then
            'ddlStatusPengajuan.SelectedValue = StatusPencairanDealer.Validasi
            'End If
            BindDatagridDaftarPencairan(dgDaftarPengajuanPencairanDepositA.CurrentPageIndex)
        Else
            MessageBox.Show("Data belum dipilih")
        End If
    End Sub

    Private Sub ProsesPengajuan2(ByVal CommandName As String)
        Dim nResult As Integer = -1
        Dim Message As String
        Dim intStatus As Byte

        Select Case CommandName
            Case "Validasi"
                Message = "Validasi"
                intStatus = EnumDepositA.StatusPencairanDealer.Validasi
                ddlStatusPengajuan.SelectedValue = EnumDepositA.StatusPencairanDealer.Validasi

                'Perlu di buatkan method untuk looping dan update data sesuai yang di inginkan
            Case "BatalValidasi"
                Message = "Batal Validasi"
                intStatus = EnumDepositA.StatusPencairanDealer.Baru
                ddlStatusPengajuan.SelectedValue = EnumDepositA.StatusPencairanDealer.Baru

            Case "Konfirmasi"
                Message = "Konfirmasi"
                intStatus = EnumDepositA.StatusPencairanDealer.Konfirmasi
                ddlStatusPengajuan.SelectedValue = EnumDepositA.StatusPencairanDealer.Konfirmasi

            Case "BatalKonfirmasi"
                Message = "Batal Konfirmasi"
                intStatus = EnumDepositA.StatusPencairanDealer.Validasi
                ddlStatusPengajuan.SelectedValue = EnumDepositA.StatusPencairanDealer.Validasi
            Case "Setuju"
                Message = "Setuju"
                intStatus = EnumDepositA.StatusPencairanDealer.Setuju
                ddlStatusPengajuan.SelectedValue = EnumDepositA.StatusPencairanDealer.Setuju

            Case "BatalSetuju"
                Message = "Batal Setuju"
                intStatus = EnumDepositA.StatusPencairanDealer.Konfirmasi
                ddlStatusPengajuan.SelectedValue = EnumDepositA.StatusPencairanDealer.Konfirmasi

            Case "Tolak"
                Message = "Tolak"
                intStatus = EnumDepositA.StatusPencairanDealer.Tolak
                ddlStatusPengajuan.SelectedValue = EnumDepositA.StatusPencairanDealer.Tolak

            Case "Blok"
                Message = "Blok"
                intStatus = EnumDepositA.StatusPencairanDealer.Blok
                ddlStatusPengajuan.SelectedValue = EnumDepositA.StatusPencairanDealer.Blok

        End Select

        If Not IsDataInDTGBValid(intStatus) Then
            MessageBox.Show("Proses " & Message & " gagal")
            Exit Sub
        End If

        For Each item As DataGridItem In dgDaftarPengajuanPencairanDepositA.Items
            Dim chkCek As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If chkCek.Checked Then
                Dim ArrPengajuan = New ArrayList
                If Not sHelper.GetSession("VDaftarPengajuan") Is Nothing Then
                    ArrPengajuan = sHelper.GetSession("VDaftarPengajuan")
                    Dim objDepositAPencairanH As DepositAPencairanH = CType(ArrPengajuan(item.ItemIndex), DepositAPencairanH)
                    If intStatus = EnumDepositA.StatusPencairanDealer.Konfirmasi Then
                        'If objDepositAPencairanH.Status <> StatusPencairanDealer.Validasi And objDepositAPencairanH.Status <> StatusPencairanDealer.Setuju Then
                        '    MessageBox.Show("Tidak bisa ubah status")
                        '    Exit Sub
                        'End If

                        If objDepositAPencairanH.Status = EnumDepositA.StatusPencairanDealer.Setuju And _
                            (objDepositAPencairanH.Type = EnumDepositA.TipePengajuan.CashInterest Or _
                            objDepositAPencairanH.Type = EnumDepositA.TipePengajuan.Offset) Then
                            MessageBox.Show("Tidak bisa ubah status")
                            Exit Sub
                        End If


                        If objDepositAPencairanH.Type = EnumDepositA.TipePengajuan.CashAnnual Or objDepositAPencairanH.Type = EnumDepositA.TipePengajuan.CashInterest Then
                            intStatus = EnumDepositA.StatusPencairanDealer.Baru
                            Message = "Baru"
                        End If

                    ElseIf intStatus = EnumDepositA.StatusPencairanDealer.Validasi Then
                        If objDepositAPencairanH.Status <> EnumDepositA.StatusPencairanDealer.Konfirmasi Then
                            If objDepositAPencairanH.Status <> EnumDepositA.StatusPencairanDealer.Baru Then
                                MessageBox.Show("Tidak bisa ubah status")
                                Exit Sub
                                'Else
                                '    intStatus = StatusPencairanDealer.Validasi
                            End If
                        Else

                        End If

                        If objDepositAPencairanH.Type = EnumDepositA.TipePengajuan.CashIncidental Then
                            intStatus = EnumDepositA.StatusPencairanDealer.Validasi
                            Message = "Validasi"
                        Else
                            intStatus = EnumDepositA.StatusPencairanDealer.Setuju
                            Message = "Setuju"
                        End If

                    ElseIf intStatus = EnumDepositA.StatusPencairanDealer.Setuju Then
                        If ddlTipePengajuan.SelectedIndex <> 2 And ddlTipePengajuan.SelectedIndex <> 4 Then
                            If objDepositAPencairanH.Status <> EnumDepositA.StatusPencairanDealer.Konfirmasi Then
                                MessageBox.Show("Tidak bisa ubah status")
                                Exit Sub
                            End If
                        End If
                    ElseIf intStatus = EnumDepositA.StatusPencairanDealer.Tolak Then
                        If objDepositAPencairanH.Status <> EnumDepositA.StatusPencairanDealer.Konfirmasi Then
                            MessageBox.Show("Tidak bisa ubah status")
                            Exit Sub
                        End If
                    End If

                    Dim txtJumlahDisetujui As TextBox = CType(item.FindControl("txtJumlahDisetujui"), TextBox)
                    If txtJumlahDisetujui.Text <> String.Empty Then
                        If CDec(txtJumlahDisetujui.Text) > objDepositAPencairanH.DealerAmount Then
                            MessageBox.Show("Jumlah Disetujui tidak boleh melebihi Jumlah Pengajuan")
                            Exit Sub
                        End If
                    End If

                    'insert history
                    nResult = InsertHistory(objDepositAPencairanH.NoSurat, objDepositAPencairanH.Status, intStatus, DocTypePengajuan)
                    If nResult > -1 Then
                        'Save Header
                        ' objDepositAPencairanH.Status = StatusPencairanDealer.Validasi
                        objDepositAPencairanH.Status = intStatus

                        objDepositAPencairanH.ApprovalAmount = txtJumlahDisetujui.Text
                        Dim txtAlasan As TextBox = CType(item.FindControl("txtAlasan"), TextBox)
                        'If txtAlasan.Text = String.Empty Then
                        '    If intStatus = StatusPencairanDealer.Setuju Or intStatus = StatusPencairanDealer.Tolak Then
                        '        MessageBox.Show("Alasan tidak boleh kosong")
                        '        BindDatagridDaftarPencairan()
                        '        Exit Sub
                        '    End If
                        'End If

                        objDepositAPencairanH.KTBReason = txtAlasan.Text
                        nResult = _DepositAPencairanHFacade.Update(objDepositAPencairanH)
                        If nResult <> -1 Then
                            'MessageBox.Show("Berhasil update menjadi " & objDepositAPencairanH.Status.ToString)
                            ' MessageBox.Show("Ubah Status berhasil")
                        Else
                            MessageBox.Show(SR.UpdateFail())
                            Exit Sub
                        End If
                    End If
                End If
            End If
        Next

        If nResult > -1 Then
            MessageBox.Show("Berhasil update menjadi " & Message)
            'If (CommandName = "Validasi") Then
            'ddlStatusPengajuan.SelectedValue = StatusPencairanDealer.Validasi
            'End If
            BindDatagridDaftarPencairan(dgDaftarPengajuanPencairanDepositA.CurrentPageIndex)
        Else
            MessageBox.Show("Data belum dipilih")
        End If
    End Sub

    Private Sub btnBatalValidasi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatalValidasi.Click
        ProsesPengajuan("BatalValidasi")
    End Sub

    Private Sub btnKonfirmasi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKonfirmasi.Click
        ProsesPengajuan("Konfirmasi")
    End Sub

    Private Sub btnBatalKonfirmasi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatalKonfirmasi.Click
        ProsesPengajuan("BatalKonfirmasi")
    End Sub

    Private Sub btnSetuju_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSetuju.Click
        ProsesPengajuan("Setuju")
    End Sub

    Private Sub btnTolak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTolak.Click
        ProsesPengajuan("Tolak")
    End Sub

    Private Sub btnProses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProses.Click

        If ddlAction.SelectedValue <> String.Empty AndAlso ddlAction.SelectedValue <> "-1" Then
            ProsesPengajuan(ddlAction.SelectedValue)
        End If

    End Sub



End Class

