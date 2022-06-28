Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports System.IO
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Claim
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.SparePart

Public Class FrmClaimList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents chkTglClaim As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkTglFaktur As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoClaim As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents icTglFakturFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglFakturUntil As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglClaimFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglClaimUntil As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents dtgClaimList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatusChange As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents lblTotalClaim As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownloadExcel As System.Web.UI.WebControls.Button
    Protected WithEvents ddlProgressChanges As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSimpanProgress As System.Web.UI.WebControls.Button
    Protected WithEvents lstStatus As System.Web.UI.WebControls.ListBox
    Protected WithEvents btnDownloadAll As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownloadCM As System.Web.UI.WebControls.Button


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Deklarasi"
    Dim criterias As CriteriaComposite
    Dim sHelper As New SessionHelper
#End Region

#Region "Custom Method"

    Private Sub MapProgressStatus(ByVal ddlProgress As DropDownList)
        Dim objClaimProgress As New ClaimProgress
        objClaimProgress.ID = 0
        objClaimProgress.Progress = "Silahkan Pilih"

        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimProgress), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(ClaimProgress), "Progress", MatchType.No, ""))
        Dim arlProgress As ArrayList = New ClaimProgressFacade(User).Retrieve(crits)
        arlProgress.Insert(0, objClaimProgress)
        ddlProgress.DataSource = arlProgress
        ddlProgress.DataTextField = "Progress"
        ddlProgress.DataValueField = "ID"
        ddlProgress.DataBind()
    End Sub

    Private Sub ResetCheckbox()
        For Each item As DataGridItem In dtgClaimList.Items
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If chk.Checked Then
                chk.Checked = False
            End If
        Next
    End Sub

    Sub MapListStatus()
        lstStatus.DataSource = New EnumClaimProgress().RetrieveStatusKTB
        lstStatus.DataTextField = "NameStatus"
        lstStatus.DataValueField = "ValStatus"
        lstStatus.DataBind()
    End Sub

    Private Sub MapDDLClaimStatus()
        Dim _enumClaimProgress As New EnumClaimProgress
        Dim _arrTmp As New ArrayList
        Dim _arrFin As New ArrayList
        _arrTmp = _enumClaimProgress.RetrieveStatusKTB

        For Each item As ClaimProgressDealer In _arrTmp
            If CekPrivStatusBaru() And item.ValStatus = EnumClaimProgress.ClaimProgressKTB.Baru Then
                _arrFin.Add(item)
            End If

            If CekPrivStatusProses() And item.ValStatus = EnumClaimProgress.ClaimProgressKTB.Diproses Then
                _arrFin.Add(item)
            End If

            If CekPrivStatusSelesai() And item.ValStatus = EnumClaimProgress.ClaimProgressKTB.Selesai Then
                _arrFin.Add(item)
            End If
        Next
        ddlStatusChange.DataSource = _arrFin
        ddlStatusChange.DataTextField = "NameStatus"
        ddlStatusChange.DataValueField = "ValStatus"
        ddlStatusChange.DataBind()
        ddlStatusChange.Items.Insert(0, "Pilih")
        If CekPrivStatusBatalSelesai() Then
            ddlStatusChange.Items.Add(New ListItem("Batal Selesai", "A"))
        End If
    End Sub

    Private Sub BindTogrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If indexPage >= 0 Then



            Dim ArlClaimHeader As ArrayList = New ClaimHeaderFacade(User).RetrieveActiveList(sHelper.GetSession("CriteriaClaimHeader"), indexPage + 1, dtgClaimList.PageSize, _
                totalRow, CType(ViewState("CurrSortCL"), String), CType(ViewState("currSortDirCLn"), Sort.SortDirection))
            dtgClaimList.DataSource = ArlClaimHeader
            dtgClaimList.VirtualItemCount = totalRow
            'If indexPage = 0 Then
            'dtgClaimList.CurrentPageIndex = 0
            'End If
            dtgClaimList.CurrentPageIndex = indexPage
            SaveCriteriasForLaterUse()
            dtgClaimList.DataBind()

            Dim criterias As CriteriaComposite
            criterias = sHelper.GetSession("CriteriaClaimDetail")

            Dim Total As Decimal = New ClaimDetailFacade(User).GetTotalClaim(criterias)
            lblTotalClaim.Text = Total.ToString("#,###")
            sHelper.RemoveSession("arlTrans")
        End If
    End Sub

    Private Sub BindTogrid(ByVal indexPage As Integer, ByVal criterias As CriteriaComposite)
        Dim totalRow As Integer = 0
        If indexPage >= 0 Then

            Dim ArlClaimHeader As ArrayList = New ClaimHeaderFacade(User).RetrieveActiveList(criterias, indexPage + 1, dtgClaimList.PageSize, _
                totalRow, CType(ViewState("CurrSortCL"), String), CType(ViewState("currSortDirCLn"), Sort.SortDirection))
            dtgClaimList.DataSource = ArlClaimHeader
            dtgClaimList.VirtualItemCount = totalRow
            'If indexPage = 0 Then
            '    dtgClaimList.CurrentPageIndex = 0
            'End If
            dtgClaimList.CurrentPageIndex = indexPage
            SaveCriteriasForLaterUse()

            dtgClaimList.DataBind()
            sHelper.RemoveSession("arlTrans")
        End If
    End Sub

    Private Function CreateCriteria() As Boolean
        Dim ht As New Hashtable


        criterias = New CriteriaComposite(New Criteria(GetType(ClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ClaimHeader), "StatusKTB", MatchType.No, CInt(EnumClaimProgress.ClaimProgressKTB.BelumDikirim)))

        Dim criteriaDetail As New CriteriaComposite(New Criteria(GetType(ClaimDetail), "ClaimHeader.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaDetail.opAnd(New Criteria(GetType(ClaimDetail), "ClaimHeader.StatusKTB", MatchType.No, CInt(EnumClaimProgress.ClaimProgressKTB.BelumDikirim)))

        Dim criteriaDownload As New CriteriaComposite(New Criteria(GetType(ClaimDetail), "ClaimHeader.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaDownload.opAnd(New Criteria(GetType(ClaimDetail), "ClaimHeader.StatusKTB", MatchType.No, CInt(EnumClaimProgress.ClaimProgressKTB.BelumDikirim)))


        'get the dealercode
        If txtKodeDealer.Text <> String.Empty Then
            Dim strDealerCode As String = txtKodeDealer.Text.Replace(";", "','")
            criterias.opAnd(New Criteria(GetType(ClaimHeader), "Dealer.DealerCode", MatchType.InSet, "('" & strDealerCode & "')"))
            criteriaDetail.opAnd(New Criteria(GetType(ClaimDetail), "ClaimHeader.Dealer.DealerCode", MatchType.InSet, "('" & strDealerCode & "')"))
            criteriaDownload.opAnd(New Criteria(GetType(ClaimDetail), "ClaimHeader.Dealer.DealerCode", MatchType.InSet, "('" & strDealerCode & "')"))
            ht.Add("DealerCode", txtKodeDealer.Text.Trim)
        Else
            ht.Add("DealerCode", "")
        End If

        'Dim obj As ClaimDetail
        'obj.ClaimHeader.Dealer.DealerCode()

        If txtNoClaim.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(ClaimHeader), "ClaimNo", MatchType.Exact, txtNoClaim.Text.Trim))
            criteriaDetail.opAnd(New Criteria(GetType(ClaimDetail), "ClaimHeader.ClaimNo", MatchType.Exact, txtNoClaim.Text.Trim))
            criteriaDownload.opAnd(New Criteria(GetType(ClaimDetail), "ClaimHeader.ClaimNo", MatchType.Exact, txtNoClaim.Text.Trim))
            ht.Add("ClaimNo", txtNoClaim.Text)
        Else
            ht.Add("ClaimNo", "")
        End If

        If chkTglFaktur.Checked Then
            If icTglFakturFrom.Value <= icTglFakturUntil.Value Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimHeader), "SparePartPOStatus.BillingDate", MatchType.GreaterOrEqual, icTglFakturFrom.Value))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimHeader), "SparePartPOStatus.BillingDate", MatchType.LesserOrEqual, icTglFakturUntil.Value.AddDays(1)))
                criteriaDetail.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.SparePartPOStatus.BillingDate", MatchType.GreaterOrEqual, icTglFakturFrom.Value))
                criteriaDetail.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.SparePartPOStatus.BillingDate", MatchType.LesserOrEqual, icTglFakturUntil.Value.AddDays(1)))
                criteriaDownload.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.SparePartPOStatus.BillingDate", MatchType.GreaterOrEqual, icTglFakturFrom.Value))
                criteriaDownload.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.SparePartPOStatus.BillingDate", MatchType.LesserOrEqual, icTglFakturUntil.Value.AddDays(1)))
            Else
                MessageBox.Show("Tanggal faktur sampai harus lebih besar atau sama dengan tanggal faktur dari")
                Return False
            End If
            ht.Add("FakturFrom", icTglFakturFrom.Value)
            ht.Add("FakturUntil", icTglFakturUntil.Value)
        End If

        If chkTglClaim.Checked Then
            If icTglClaimFrom.Value <= icTglClaimUntil.Value Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimHeader), "ClaimDate", MatchType.GreaterOrEqual, icTglClaimFrom.Value))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimHeader), "ClaimDate", MatchType.LesserOrEqual, icTglClaimUntil.Value.AddDays(1)))
                criteriaDetail.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.ClaimDate", MatchType.GreaterOrEqual, icTglClaimFrom.Value))
                criteriaDetail.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.ClaimDate", MatchType.LesserOrEqual, icTglClaimUntil.Value.AddDays(1)))
                criteriaDownload.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.ClaimDate", MatchType.GreaterOrEqual, icTglClaimFrom.Value))
                criteriaDownload.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.ClaimDate", MatchType.LesserOrEqual, icTglClaimUntil.Value.AddDays(1)))
            Else
                MessageBox.Show("Tanggal claim sampai harus lebih besar atau sama dengan tanggal claim dari")
                Return False
            End If
            ht.Add("ClaimFrom", icTglClaimFrom.Value)
            ht.Add("ClaimUntil", icTglClaimUntil.Value)
        End If

        'If ddlClaimStatus.SelectedIndex <> 0 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimHeader), "StatusKTB", MatchType.Exact, ddlClaimStatus.SelectedValue))
        'End If
        Dim strData As String
        For Each item As ListItem In lstStatus.Items
            If item.Selected = True Then
                strData &= item.Value & ";"
            End If
        Next
        If strData <> "" Then
            strData = Left(strData, strData.Length - 1)
            ViewState.Add("ListStatus", strData)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimHeader), "StatusKTB", MatchType.InSet, "(" & strData.Replace(";", ",") & ")"))
            criteriaDetail.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.StatusKTB", MatchType.InSet, "(" & strData.Replace(";", ",") & ")"))
            criteriaDownload.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.StatusKTB", MatchType.InSet, "(" & strData.Replace(";", ",") & ")"))
            ht.Add("StatusKTB", strData)
        End If

        criteriaDetail.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.Status", MatchType.InSet, "(" & CInt(EnumClaimStatus.ClaimStatus.Selesai) & "," & CInt(EnumClaimStatus.ClaimStatus.Complete_Selesai) & ")"))
        criteriaDetail.opAnd(New Criteria(GetType(ClaimDetail), "StatusDetail", MatchType.No, CInt(EnumClaimStatusDetail.ClaimStatusDetail.Ditolak)))

        sHelper.SetSession("CriteriaClaimDetail", criteriaDetail)
        sHelper.SetSession("CriteriaClaimDetailDownload", criteriaDownload)
        sHelper.SetSession("CriteriaClaimHeader", criterias)
        sHelper.SetSession("SaveHTCriteria", ht)

        Return True
    End Function

    'Private Sub BindClaimStatusLookUp()
    '    criterias = New CriteriaComposite(New Criteria(GetType(ClaimStatusKTB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    Dim arlClaimStatusLookUp As ArrayList = New ClaimStatusKTBFacade(User).Retrieve(criterias)
    '    sHelper.SetSession("ClaimStatusLookUp", arlClaimStatusLookUp)
    'End Sub

    Private Sub SaveCriteriasForLaterUse()
        sHelper.SetSession("CritClaimHeaderBack", sHelper.GetSession("CriteriaClaimHeader"))
        sHelper.SetSession("GridPageIndexBack", dtgClaimList.CurrentPageIndex)
        sHelper.SetSession("CurrSortColBack", ViewState("CurrSortCL"))
        sHelper.SetSession("currSortDirCLnBack", ViewState("currSortDirCLn"))
        sHelper.SetSession("ListData", ViewState("ListStatus"))
    End Sub

    Private Sub MapCriteriaSearch()
        Dim ht As Hashtable = CType(sHelper.GetSession("SaveHTCriteria"), Hashtable)
        txtKodeDealer.Text = CType(ht.Item("DealerCode"), String)
        txtNoClaim.Text = CType(ht.Item("ClaimNo"), String)
        If Not ht.Item("FakturFrom") Is Nothing Then
            icTglFakturFrom.Value = ht.Item("FakturFrom")
        End If
        If Not ht.Item("FakturUntil") Is Nothing Then
            icTglFakturUntil.Value = ht.Item("FakturUntil")
        End If
        If Not ht.Item("ClaimFrom") Is Nothing Then
            icTglClaimFrom.Value = ht.Item("ClaimFrom")
        End If
        If Not ht.Item("ClaimUntil") Is Nothing Then
            icTglClaimUntil.Value = ht.Item("ClaimUntil")
        End If


        Dim strData As String = ht.Item("StatusKTB")

        If strData <> "" Then
            Dim strValue() As String = strData.Split(";")
            For Each item As ListItem In lstStatus.Items
                For Each i As String In strValue
                    If item.Value = i Then
                        item.Selected = True
                    End If
                Next
            Next
        End If

        CreateCriteria()
    End Sub
#End Region

#Region "Privilage"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.StatusClaimListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=CLAIM - Daftar Status")
        End If
    End Sub
    Private Function CekPrivStatusBaru() As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.StatusListCreateNewStatus_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Function CekPrivStatusProses() As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.StatusListCreateStatusProcess_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Function CekPrivStatusSelesai() As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.StatusListCreateStatusSelesai_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Function CekPrivStatusBatalSelesai() As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.StatusListCreateStatusSelesaiBatalSelesai_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Function CekPrivLihatHistory() As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.HistoryStatusView_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Function CekPrivLihatDetailClaim() As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.ClaimDetailView_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
    'Private Function CekPrivLihatCatatanKTB() As Boolean
    '    If Not SecurityProvider.Authorize(context.User, SR.StatusListViewKTBNotes_Privilege) Then
    '        Return False
    '    Else
    '        Return True
    '    End If
    'End Function
    Private Function CekPrivDownloadEvidence() As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.StatusListDownloadEvidance_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Function CekPrivCtkFormClaim() As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.StatusListCetakFormClaim_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Function CekPrivCtkFormClaimProses() As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.StatusListCetakFormClaimProgress_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        dtgClaimList.Columns(11).Visible = False

        If Not IsPostBack Then
            Dim qsIsBack As Integer = CInt(Request.QueryString("isBack"))
            If qsIsBack = 1 Then
                BindTogrid(CInt(sHelper.GetSession("GridPageIndexBack")), sHelper.GetSession("CriteriaClaimHeader"))
                MapDDLClaimStatus()
                MapListStatus()
                MapProgressStatus(ddlProgressChanges)
                lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"

                ViewState.Add("CurrSortCL", sHelper.GetSession("CurrSortColBack"))
                ViewState.Add("currSortDirCLn", sHelper.GetSession("currSortDirCLnBack"))
                MapCriteriaSearch()
            Else
                Dim refresh As String = CStr(sHelper.GetSession("RefreshData"))
                If refresh <> "" Then
                    BindTogrid(0, sHelper.GetSession("CriteriaClaimHeader"))
                End If
                MapDDLClaimStatus()
                MapListStatus()
                MapProgressStatus(ddlProgressChanges)
                lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"

                ViewState.Add("CurrSortCL", "Dealer.DealerCode")
                ViewState.Add("currSortDirCLn", Sort.SortDirection.ASC)
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dtgClaimList.CurrentPageIndex = 0
        'BindClaimStatusLookUp()
        If CreateCriteria() Then
            BindTogrid(dtgClaimList.CurrentPageIndex)
        End If
    End Sub

    Private Sub dtgClaimList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgClaimList.SortCommand
        If CType(ViewState("CurrSortCL"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirCLn"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirCLn") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currSortDirCLn") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrSortCL") = e.SortExpression
            ViewState("currSortDirCLn") = Sort.SortDirection.ASC
        End If
        BindTogrid(dtgClaimList.CurrentPageIndex)
    End Sub

    Private Sub dtgClaimList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgClaimList.PageIndexChanged
        dtgClaimList.CurrentPageIndex = e.NewPageIndex
        BindTogrid(dtgClaimList.CurrentPageIndex)
    End Sub

    Private Sub dtgClaimList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClaimList.ItemDataBound
        Dim _enumClaimProgress As New EnumClaimProgress
        Dim arlClaimStatus As ArrayList = sHelper.GetSession("ClaimStatusLookUp")

        If e.Item.ItemIndex <> -1 Then
            'set status dan progress
            Dim lblStatusKTB As Label = CType(e.Item.FindControl("lblStatusKTB"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lblProgress As Label = CType(e.Item.FindControl("lblProgress"), Label)
            Dim ltbnNoClaim As LinkButton = CType(e.Item.FindControl("ltbnNoClaim"), LinkButton)
            Dim lblNoFaktur As Label = CType(e.Item.FindControl("lblNoFaktur"), Label)

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dtgClaimList.CurrentPageIndex * dtgClaimList.PageSize)

            'Set Checked Box after changed status
            If Not sHelper.GetSession("arlTrans") Is Nothing Then
                Dim arlTrans As ArrayList = sHelper.GetSession("arlTrans")
                If arlTrans.Count <> 0 Then
                    For Each itemHist As ClaimStatusHistory In arlTrans
                        If itemHist.ClaimHeader.ID = CType(e.Item.DataItem, ClaimHeader).ID Then
                            Dim chkItemChecked As CheckBox = e.Item.FindControl("chkItemChecked")
                            chkItemChecked.Checked = True
                            Exit For
                        End If
                    Next
                End If
            End If


            'mod by Ery
            Select Case CInt(lblStatusKTB.Text)
                Case _enumClaimProgress.ClaimProgressKTB.Baru
                    lblStatus.Text = "Baru"
                Case _enumClaimProgress.ClaimProgressKTB.Diproses
                    lblStatus.Text = "DiProses"
                Case _enumClaimProgress.ClaimProgressKTB.Selesai
                    lblStatus.Text = "Selesai"
                Case _enumClaimProgress.ClaimProgressKTB.Complete_Selesai
                    lblStatus.Text = "Complete Selesai"
            End Select
            'Select Case CInt(lblStatusKTB.Text)
            '    Case _enumClaimProgress.ClaimProgressKTB.Baru
            '        lblStatus.Text = "Baru"
            '        For Each item As ClaimStatusKTB In arlClaimStatus
            '            If item.EnumStatus = _enumClaimProgress.ClaimProgressKTB.Baru Then
            '                lblProgress.Text = item.Progress
            '                Exit For
            '            End If
            '        Next
            '    Case _enumClaimProgress.ClaimProgressKTB.Diproses
            '        lblStatus.Text = "DiProses"
            '        For Each item As ClaimStatusKTB In arlClaimStatus
            '            If item.EnumStatus = _enumClaimProgress.ClaimProgressKTB.Diproses Then
            '                lblProgress.Text = item.Progress
            '                Exit For
            '            End If
            '        Next
            '    Case _enumClaimProgress.ClaimProgressKTB.Selesai
            '        lblStatus.Text = "Selesai"
            '        For Each item As ClaimStatusKTB In arlClaimStatus
            '            If item.EnumStatus = _enumClaimProgress.ClaimProgressKTB.Selesai Then
            '                lblProgress.Text = item.Progress
            '                Exit For
            '            End If
            '        Next
            'End Select


            'count total
            Dim lblTotalClaim As Label = CType(e.Item.FindControl("lblTotalClaim"), Label)
            Dim TotalChDetails As Integer = 0
            Dim ch As ClaimHeader = CType(e.Item.DataItem, ClaimHeader)

            For i As Integer = 0 To ch.ClaimDetails.Count - 1
                Dim cd As ClaimDetail = New ClaimDetail
                cd = ch.ClaimDetails(i)
                'Bug 1420 & 300
                If cd.StatusDetail <> EnumClaimStatusDetail.ClaimStatusDetail.Ditolak And (ch.StatusKTB = EnumClaimProgress.ClaimProgressKTB.Selesai Or ch.StatusKTB = EnumClaimProgress.ClaimProgressKTB.Complete_Selesai) Then
                    TotalChDetails = TotalChDetails + (cd.ApprovedQty * cd.SparePartPOStatusDetail.ClaimPriceUnit)
                End If
            Next
            lblTotalClaim.Text = TotalChDetails.ToString("#,###")
            If lblTotalClaim.Text = "" Then
                lblTotalClaim.Text = 0
            End If

            'count ETA
            Dim lblETA As Label = CType(e.Item.FindControl("lblETA"), Label)
            Dim Da As DealerAdditional = New DealerAdditional
            If (ch.ReceivedDate.ToString("dd/MM/yyyy") <> "01/01/1753") Then
                lblETA.Text = ch.ReceivedDate.ToString("dd/MM/yyyy")
            Else
                Dim arrDA = New DealerAdditionalFacade(User).RetrieveByDealerID(ch.Dealer.ID)
                If (arrDA.Count > 0) Then
                    Da = arrDA(0)
                    lblETA.Text = Da.ClaimETA
                End If
            End If

            Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnStatusChanges As LinkButton = CType(e.Item.FindControl("lbtnStatusChanges"), LinkButton)
            Dim lbtnPopUp As LinkButton = CType(e.Item.FindControl("lbtnPopUp"), LinkButton)
            Dim lbtnClaimForm As LinkButton = CType(e.Item.FindControl("lbtnClaimForm"), LinkButton)
            Dim lbtnEvidence As LinkButton = CType(e.Item.FindControl("lbtnEvidence"), LinkButton)
            Dim lbtnNoClaim As LinkButton = CType(e.Item.FindControl("lbtnNoClaim"), LinkButton)
            Dim btnDownloadCM As LinkButton = CType(e.Item.FindControl("btnDownloadCM"), LinkButton)

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

            'SET PRIVILAGE
            If CekPrivLihatHistory() Then
                lbtnStatusChanges.Visible = True
            Else
                lbtnStatusChanges.Visible = False
            End If


            If CekPrivCtkFormClaim() Then
                If CInt(lblStatusKTB.Text) = _enumClaimProgress.ClaimProgressKTB.Selesai Or CInt(lblStatusKTB.Text) = _enumClaimProgress.ClaimProgressKTB.Complete_Selesai Then
                    lbtnNoClaim.Attributes.Add("onclick", "showPopUp('../Claim/FrmPrintClaim.aspx?ID=" & ch.ID & "', '', 600, 800, KTBNote);return false;")
                Else
                    lbtnNoClaim.Enabled = False
                End If
            Else
                lbtnNoClaim.Enabled = False
            End If
            If CekPrivCtkFormClaimProses() Then
                lbtnClaimForm.Visible = True
            Else
                lbtnClaimForm.Visible = False
            End If
            If CekPrivDownloadEvidence() Then
                lbtnPopUp.Visible = True
                If ch.UploadFileName = "" OrElse ch.UploadFileName = String.Empty Then
                    lbtnEvidence.Visible = False
                Else
                    lbtnEvidence.Visible = True
                End If
            Else
                lbtnPopUp.Visible = False
                lbtnEvidence.Visible = False
            End If
            Dim objUser As UserInfo = CType(sHelper.GetSession("LOGINUSERINFO"), UserInfo)
            lbtnPopUp.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpKTBNote.aspx?ID=" & ch.ID & "&isKTB=" & objUser.Dealer.Title.ToString & "', '', 300, 500, KTBNote);return false;")
            lbtnStatusChanges.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpClaimStatusChanges.aspx?ClaimHeaderID=" & ch.ID & "&StatusKTB=" & ch.StatusKTB & "', '', 500, 760, null);return false;")
            lbtnClaimForm.Attributes.Add("onclick", "showPopUp('../Claim/FrmClaimProcess.aspx?ID=" & ch.ID & "&time=" & Date.Now & "', '', 600, 800, null);return false;")

            lbtnNoClaim.Text = ch.ClaimNo
            'refer bug 1324
            'If (ch.FakturRetur <> String.Empty) Then
            '    lbtnNoClaim.Attributes.Add("onclick", "showPopUp('../Claim/FrmPrintClaim.aspx?ID=" & ch.ID & "', '', 600, 800, KTBNote);return false;")
            'Else
            '    lbtnNoClaim.Enabled = False
            'End If


            'If ch.UploadFileName <> String.Empty Then
            '    Dim lbtnEvidence As LinkButton = CType(e.Item.FindControl("lbtnEvidence"), LinkButton)
            '    lbtnEvidence.Visible = True
            'End If


            'modify by Ery
            'Login KTB edit hanya bisa bila status=proses
            If objUser.Dealer.Title = "1" Then
                If ch.StatusKTB = EnumClaimProgress.ClaimProgressKTB.Diproses Then
                    lbtnView.Visible = False
                    lbtnEdit.Visible = CekPrivStatusProses()
                Else
                    lbtnView.Visible = CekPrivLihatDetailClaim()
                    lbtnEdit.Visible = False
                End If

            Else
                If ch.Status = EnumClaimStatus.ClaimStatus.Baru Then
                    lbtnView.Visible = False
                    lbtnEdit.Visible = CekPrivStatusProses()
                Else
                    lbtnView.Visible = CekPrivLihatDetailClaim()
                    lbtnEdit.Visible = False
                End If
            End If

            'set tooltip for dealer base on Search2
            Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(lblDealer.Text.Trim)
            lblDealer.ToolTip = objDealer.SearchTerm2
        End If
    End Sub

    Private Sub dtgClaimList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgClaimList.ItemCommand
        If (e.CommandName = "ViewDetails") Then
            Response.Redirect("../Claim/FrmClaimDetail.aspx?ClaimHeaderID=" & e.CommandArgument & "&View=True&&ViewKTB=true")
        End If
        If (e.CommandName = "DownloadCE") Then
            If e.CommandArgument <> "" Then
                Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("ClaimEvidenceDir") & "\" & e.CommandArgument.ToString().Replace("/", "\")
                Response.Redirect("../Download.aspx?file=" & PathFile)
            Else
                MessageBox.Show("File tidak ditemukan")
            End If
        End If
        If (e.CommandName = "edit") Then
            Dim objClaim As ClaimHeader = New ClaimHeaderFacade(User).Retrieve(CInt(e.CommandArgument))
            If objClaim.StatusKTB = EnumClaimProgress.ClaimProgressKTB.Selesai Or objClaim.StatusKTB = EnumClaimProgress.ClaimProgressKTB.Complete_Selesai Then
                MessageBox.Show("Anda tidak dapat mengubah data claim dengan status 'Selesai'")
                Exit Sub
            End If
            Response.Redirect("../Claim/FrmClaimDetail.aspx?ClaimHeaderID=" & e.CommandArgument & "&View=False&ViewKTB=true")
        End If
        If (e.CommandName = "DownloadCM") Then
            'Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "\DataFile\Sparepart\Edoc\" & e.CommandArgument.ToString() & ".pdf"
            'Dim x As String = "EOPackListSummary_7820254721_202101291546"
            'Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "\DataFile\Sparepart\Edoc\" & x & ".pdf"


            Dim lblNoFaktur As Label = CType(e.Item.FindControl("lblFakturRetur"), Label)
            'lblNoFaktur.Text = 7820254479

            Dim critEDoc As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartEDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critEDoc.opAnd(New Criteria(GetType(SparePartEDocument), "DocType", MatchType.Exact, 1))
            critEDoc.opAnd(New Criteria(GetType(SparePartEDocument), "DocNumber", MatchType.Exact, lblNoFaktur.Text))
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

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim arlList As New ArrayList
        Dim CH As ClaimHeader
        Dim newStatus As Byte
        Dim oldStatus As Byte

        If ddlStatusChange.SelectedIndex <> 0 Then
            For Each item As DataGridItem In dtgClaimList.Items
                Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
                If chk.Checked Then

                    Dim lblID As Label = CType(item.FindControl("lblID"), Label)
                    CH = New ClaimHeaderFacade(User).Retrieve(CInt(lblID.Text))

                    'get the old status from claim header
                    oldStatus = CH.StatusKTB
                    If ddlStatusChange.SelectedValue <> "A" Then
                        newStatus = CByte(ddlStatusChange.SelectedValue)
                    Else
                        newStatus = CByte(EnumClaimProgress.ClaimProgressKTB.Diproses)
                    End If


                    'Check Status
                    Dim x As Integer
                    x = EnumClaimProgress.ClaimProgressKTB.Diproses

                    If ddlStatusChange.SelectedValue = x.ToString And CH.StatusKTB.ToString <> EnumClaimProgress.ClaimProgressKTB.Baru Then
                        MessageBox.Show("Proses Gagal, Ubah status Diproses hanya boleh untuk status Baru. ( Record ke " & (item.ItemIndex + 1).ToString & " )")
                        Exit Sub
                    End If

                    If CH.StatusKTB = EnumClaimProgress.ClaimProgressKTB.Complete_Selesai Then
                        MessageBox.Show("Status Complete Selesai tidak dapat diubah ( Record ke " & (item.ItemIndex + 1).ToString & " )")
                        Exit Sub
                    End If

                    If CH.StatusKTB = EnumClaimProgress.ClaimProgressKTB.Selesai And Not (ddlStatusChange.SelectedValue = "A") Then
                        MessageBox.Show("Status Selesai hanya dapat diubah ke batal selesai ( Record ke " & (item.ItemIndex + 1).ToString & " )")
                        Exit Sub
                    End If


                    x = EnumClaimProgress.ClaimProgressKTB.Selesai
                    If ddlStatusChange.SelectedValue = x.ToString And CH.StatusKTB.ToString <> EnumClaimProgress.ClaimProgressKTB.Diproses Then
                        MessageBox.Show("Proses Gagal, Ubah status Selesai hanya boleh untuk status DiProses . ( Record ke " & (item.ItemIndex + 1).ToString & " )")
                        Exit Sub
                    ElseIf ddlStatusChange.SelectedValue = x.ToString And CH.StatusKTB.ToString = EnumClaimProgress.ClaimProgressKTB.Diproses Then
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(ClaimDetail), "ClaimHeader.ID", MatchType.Exact, CH.ID))
                        criterias.opAnd(New Criteria(GetType(ClaimDetail), "StatusDetail", MatchType.Exact, CInt(EnumClaimStatusDetail.ClaimStatusDetail.Baru)))

                        Dim arlUnProses As ArrayList = New ClaimDetailFacade(User).Retrieve(criterias)
                        If arlUnProses.Count <> 0 Then
                            MessageBox.Show("Proses Gagal, Detail Status Belum Dilengkapi. ( Record ke " & (item.ItemIndex + 1).ToString & " )")
                            Exit Sub
                        End If

                    End If

                    If ddlStatusChange.SelectedValue = "A" And CH.StatusKTB.ToString <> EnumClaimProgress.ClaimProgressKTB.Selesai Then
                        MessageBox.Show("Proses Gagal, Proses Batal Selesai hanya boleh untuk status Selesai. ( Record ke " & (item.ItemIndex + 1).ToString & " )")
                        Exit Sub
                    End If

                    'record the changes and insert to history
                    Dim CHHistory As New ClaimStatusHistory
                    CHHistory.Progress = CInt(ddlProgressChanges.SelectedValue)
                    CHHistory.OldProgress = CInt(CH.ClaimProgress.ID)
                    '"A" adalah proses Batal Selesai
                    If ddlStatusChange.SelectedValue = "A" Then
                        CHHistory.Status = EnumClaimProgress.ClaimProgressKTB.Diproses
                        CH.StatusKTB = EnumClaimProgress.ClaimProgressKTB.Diproses
                    Else
                        'CHHistory.Status = CByte(ddlStatusChange.SelectedValue)
                        CH.StatusKTB = CByte(ddlStatusChange.SelectedValue)
                    End If

                    CHHistory.ClaimHeader = CH

                    arlList.Add(CHHistory)
                End If
            Next

            If arlList.Count > 0 Then
                If (New ClaimStatusHistoryFacade(User).UpdateCHTransaction(arlList, newStatus, oldStatus) = 1) Then
                    MessageBox.Show(SR.SaveSuccess)
                    sHelper.SetSession("arlTrans", arlList)
                    BindTogrid(dtgClaimList.CurrentPageIndex)
                    Dim x As Integer = EnumClaimProgress.ClaimProgressKTB.Selesai
                    If ddlStatusChange.SelectedValue = x.ToString Then
                        btnDownload_Click(Me, EventArgs.Empty)
                    End If
                End If
            Else
                MessageBox.Show("Daftar claim belum dipilih")
            End If
        Else
            MessageBox.Show("Silahkan pilih perubahan yang diinginkan")
        End If
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click

        'Check Status and valid items
        Dim isNotRejected As Boolean = False
        For Each item As DataGridItem In dtgClaimList.Items
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If chk.Checked Then

                Dim lblID As Label = CType(item.FindControl("lblID"), Label)
                Dim CHx As ClaimHeader = New ClaimHeaderFacade(User).Retrieve(CInt(lblID.Text))

                If CHx.StatusKTB <> EnumClaimProgress.ClaimProgressKTB.Selesai And CHx.StatusKTB <> EnumClaimProgress.ClaimProgressKTB.Complete_Selesai Then
                    MessageBox.Show("Proses Gagal, Download hanya bisa dilakukan untuk status Selesai. ( record ke " & item.ItemIndex + 1 & " )")
                    Exit Sub
                End If

                For Each itemDetail As ClaimDetail In CHx.ClaimDetails
                    If isNotRejected = False And itemDetail.StatusDetail <> EnumClaimStatusDetail.ClaimStatusDetail.Ditolak Then
                        isNotRejected = True
                        Exit For
                    End If

                Next

            End If
        Next

        If isNotRejected = False Then
            If Not sender Is Me Then
                MessageBox.Show("Tidak ada detail barang yang bisa didownload")
            End If
            Exit Sub
        End If


        Dim i As Integer = 0
        Dim CH As ClaimHeader
        Dim CD As ArrayList
        Dim success As Boolean = False
        Dim Connect As Boolean = False
        Dim sw As StreamWriter
        Dim filename = String.Format("{0}{1}{2}", "claimord", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")
        'Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("ClaimDownload") & "\" & filename  '-- Destination file
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\Sparepart\Claim\" & filename   '-- Destination file
        If (Connect = False) Then
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            Dim finfo As New FileInfo(DestFile)
            Try
                success = imp.Start()
                If success Then
                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If
                    sw = New StreamWriter(DestFile)
                    Connect = True
                    imp.StopImpersonate()
                    imp = Nothing
                End If
            Catch ex As Exception
                Throw ex
                Exit Sub
            End Try
        End If

        For Each item As DataGridItem In dtgClaimList.Items
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If (chk.Checked) Then

                CH = New ClaimHeader
                Dim CHFacade As ClaimHeaderFacade = New ClaimHeaderFacade(User)
                Dim lblID As Label = CType(item.FindControl("lblID"), Label)
                CH = CHFacade.Retrieve(Convert.ToInt32(lblID.Text))

                Dim HeaderStr As String = "H;" & CH.SparePartPOStatus.BillingNumber & ";" & CH.ClaimNo & ";" & CH.ClaimDate.ToString("ddMMyyyy") & ";" & CH.Dealer.DealerCode & ";" & CH.ClaimReason.Code & ";" & CH.ID & ";" & CH.SparePartPOStatus.SONumber
                'sw.WriteLine("H;" & CH.SparePartPOStatus.BillingNumber & ";" & CH.ClaimReason.Code & ";" & CH.ClaimNo & ";" & CH.ClaimDate.ToString("ddMMyyyy") & ";" & CH.ID)
                'sw.WriteLine()

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.ID", MatchType.Exact, CH.ID))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ApprovedQty", MatchType.No, 0))

                CD = New ArrayList
                CD = New ClaimDetailFacade(User).RetrieveActiveList(criterias)

                If CD.Count > 0 Then
                    sw.WriteLine(HeaderStr)
                    For Each item2 As ClaimDetail In CD
                        'sw.WriteLine("D;" & item2.SparePartPOStatusDetail.SparePartMaster.PartNumber & ";" & item2.Qty)
                        sw.WriteLine("D;" & item2.SparePartPOStatusDetail.SparePartMaster.PartNumber & ";" & item2.ApprovedQty)
                    Next
                End If
            End If
        Next
        If (success = True) Then
            sw.Close()
            'Bug 983 download langsung ke SAP bukan ke lokal
            MessageBox.Show("Data Berhasil Didownload ke SAP Server")
            'Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\" & filename
            'If Not sender Is Me Then
            '   Response.Redirect("../Download.aspx?file=" & DestFile)
            'End If

        Else
            MessageBox.Show("Daftar claim belum dipilih")
        End If
    End Sub

    Private Sub btnDownloadExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownloadExcel.Click
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrData As ArrayList
        Dim strCH_id As String
        If dtgClaimList.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        Else
            For Each item As DataGridItem In dtgClaimList.Items
                Dim chkItemCheckedNew As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
                Dim lblIDNew As Label = CType(item.FindControl("lblID"), Label)
                ' perubahan , semua data grid yg didownload, bukan yg dicentang saja, refer bug 591
                'If chkItemCheckedNew.Checked Then
                If strCH_id = "" Then
                    strCH_id = lblIDNew.Text
                Else
                    strCH_id = strCH_id & ";" & lblIDNew.Text
                End If
                'End If
            Next
        End If

        If strCH_id = "" Then
            MessageBox.Show("Tidak ada data yang dipilih untuk didownload")
        Else
            ' mengambil data yang dibutuhkan
            criterias = New CriteriaComposite(New Criteria(GetType(ClaimDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ClaimDetail), "ClaimHeader.ID", MatchType.InSet, CommonFunction.GetStrValue(strCH_id, ";", ",")))
            arrData = New ClaimDetailFacade(User).Retrieve(criterias)
            If arrData.Count > 0 Then
                DoDownload(arrData)
            End If
        End If
    End Sub

    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "RekapClaim" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond

        Dim RekapClaimData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(RekapClaimData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(RekapClaimData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteRekapClaimData(sw, data)
                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")
        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub WriteRekapClaimData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Claim - Daftar Claim")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No" & tab)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Nama Dealer" & tab)
            itemLine.Append("SearchTerm2" & tab)
            itemLine.Append("Nomor Claim" & tab)
            itemLine.Append("Tanggal Claim" & tab)
            itemLine.Append("Reference Faktur" & tab)
            itemLine.Append("Faktur Retur" & tab)
            itemLine.Append("Faktur Retur Date" & tab)
            itemLine.Append("Alasan Claim" & tab)
            itemLine.Append("Kode Barang" & tab)
            itemLine.Append("Nama Barang" & tab)
            itemLine.Append("Quantity Claim" & tab)
            itemLine.Append("Quantity Approve" & tab)
            itemLine.Append("Harga Satuan" & tab)
            itemLine.Append("Kondisi Barang" & tab)
            itemLine.Append("Keterangan" & tab)
            itemLine.Append("Tanggal Selesai" & tab)
            itemLine.Append("Status Detail" & tab)
            itemLine.Append("No DO" & tab)

            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            For Each item As ClaimDetail In data
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(i.ToString & tab)
                itemLine.Append(item.ClaimHeader.Dealer.DealerCode & tab)
                itemLine.Append(item.ClaimHeader.Dealer.DealerName & tab)
                itemLine.Append(item.ClaimHeader.Dealer.SearchTerm2 & tab)
                itemLine.Append(item.ClaimHeader.ClaimNo & tab)
                itemLine.Append(item.ClaimHeader.ClaimDate.ToString("dd/MM/yyyy") & tab)
                itemLine.Append(item.SparePartPOStatusDetail.SparePartPOStatus.BillingNumber & tab)
                itemLine.Append(item.ClaimHeader.FakturRetur & tab)

                If item.ClaimHeader.FakturReturDate.Year <> 1753 Then
                    itemLine.Append(item.ClaimHeader.FakturReturDate & tab)
                Else
                    itemLine.Append("-" & tab)
                End If

                itemLine.Append(item.ClaimHeader.ClaimReason.Reason & tab)
                itemLine.Append(item.SparePartPOStatusDetail.SparePartMaster.PartNumber & tab)
                itemLine.Append(item.SparePartPOStatusDetail.SparePartMaster.PartName & tab)
                itemLine.Append(item.Qty & tab)
                itemLine.Append(item.ApprovedQty & tab)
                itemLine.Append(item.SparePartPOStatusDetail.ClaimPriceUnit.ToString("###0") & tab)
                itemLine.Append(item.ClaimGoodCondition.Condition & tab)
                itemLine.Append(Replace(CType(item.Keterangan, String), vbTab, "") & tab)
                If item.StatusDetail = EnumClaimProgress.ClaimProgressKTB.Selesai Then
                    itemLine.Append(item.LastUpdateTime.ToString("dd/MM/yyyy") & tab)
                Else
                    itemLine.Append("" & tab)
                End If
                itemLine.Append(CType(item.StatusDetailKTB, EnumClaimStatusDetail.ClaimStatusDetailKTB).ToString.Replace("_", " ") & tab)
                itemLine.Append(item.SparePartPOStatusDetail.DONumber & tab)

                'itemLine.Append(CType(item.StatusDetail, EnumClaimProgress.ClaimProgressKTB).ToString & tab)


                'itemLine.Append(item.OnTheRoadPrice.ToString("#,##0") & tab)
                'Try
                '    itemLine.Append(item.Dealer.Area1.AreaCode & tab)
                'Catch ex As Exception
                '    itemLine.Append("" & tab)
                'End Try
                sw.WriteLine(itemLine.ToString())
                i = i + 1
            Next

        End If
    End Sub

    Private Sub btnSimpanProgress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpanProgress.Click
        Dim CH As ClaimHeader
        Dim arlList As New ArrayList

        If ddlProgressChanges.SelectedIndex <> 0 Then
            Dim oldProgress As Integer
            Dim ubahProgress As Integer = ddlProgressChanges.SelectedValue
            Dim objClaimProgress As ClaimProgress = New ClaimProgressFacade(User).Retrieve(ubahProgress)

            For Each item As DataGridItem In dtgClaimList.Items
                Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
                If chk.Checked Then
                    Dim lblID As Label = CType(item.FindControl("lblID"), Label)
                    CH = New ClaimHeaderFacade(User).Retrieve(CInt(lblID.Text))

                    'cek status
                    If Not CH.StatusKTB = EnumClaimProgress.ClaimProgressKTB.Diproses Then
                        MessageBox.Show("Status Progress hanya bisa diubah apabila status DiProses")
                        Exit Sub
                    End If

                    'ubah ClaimHistory
                    Dim CHHistory As New ClaimStatusHistory
                    CHHistory.Status = CByte(CH.StatusKTB)
                    CHHistory.Progress = CByte(ubahProgress)
                    CHHistory.OldProgress = CInt(CH.ClaimProgress.ID)

                    CHHistory.ClaimHeader = CH
                    arlList.Add(CHHistory)
                End If
            Next

            If arlList.Count > 0 Then
                If (New ClaimStatusHistoryFacade(User).UpdateCHTransaction(arlList, objClaimProgress) = 1) Then
                    MessageBox.Show(SR.SaveSuccess)
                    ResetCheckbox()
                    sHelper.SetSession("arlTrans", arlList)
                    BindTogrid(dtgClaimList.CurrentPageIndex)
                End If
            Else
                MessageBox.Show("Daftar claim belum dipilih")
            End If
        Else
            MessageBox.Show("Silahkan pilih perubahan Progress yang diinginkan")
        End If
    End Sub

    Private Sub btnDownloadAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownloadAll.Click

        If dtgClaimList.Items.Count < 1 Then
            MessageBox.Show("Tampilkan dulu data yang akan didownload")
            Exit Sub
        End If

        Dim sortColl As SortCollection = New SortCollection

        If (Not IsNothing(CType(ViewState("CurrSortCL"), String))) And (Not IsNothing(CType(ViewState("currSortDirCLn"), String))) Then
            sortColl.Add(New Sort(GetType(ClaimHeader), CType(ViewState("CurrSortCL"), String), CType(ViewState("currSortDirCLn"), String)))
        Else
            'sortColl = Nothing
            ViewState.Add("CurrSortCL", "Dealer.DealerCode")
            ViewState.Add("currSortDirCLn", Sort.SortDirection.ASC)
            sortColl.Add(New Sort(GetType(ClaimHeader), CType(ViewState("CurrSortCL"), String), CType(ViewState("currSortDirCLn"), String)))
        End If

        Dim ArlClaimDetail As ArrayList = New ClaimDetailFacade(User).RetrieveActiveList(sHelper.GetSession("CriteriaClaimDetailDownload"), "ClaimHeader." & CType(ViewState("CurrSortCL"), String), CType(ViewState("currSortDirCLn"), String))

        DoDownload(ArlClaimDetail)

    End Sub
End Class
