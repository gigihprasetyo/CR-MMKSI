Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.Security

Imports System.IO
Imports System.Text


Public Class FrmPQRListBB
    Inherits System.Web.UI.Page

    Private oPQRHeaderBB As New PQRHeaderBB
    Private oPQRHeaderBBFacade As New PQRHeaderBBFacade(User)
    Private sessHelper As New SessionHelper

    Private oCategoryFacade As New CategoryFacade(User)
    Private _arrPQRHeaderBB As ArrayList
    Private objDealer As Dealer
    Private oLoginUser As UserInfo

    Protected WithEvents lblDealerSearch As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPQRNoSearch As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglApplySearch As System.Web.UI.WebControls.Label
    Protected WithEvents lblStat As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPqrType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents pnlChangeStatus As System.Web.UI.WebControls.Panel
    Protected WithEvents btnPreviousStatus As System.Web.UI.WebControls.Button
    Protected WithEvents btnNextStatus As System.Web.UI.WebControls.Button
    Protected WithEvents txtProcessBy As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblProcessBy As System.Web.UI.WebControls.Label
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents txtKodePosisi As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents chkFilterTanggal As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ddlCreator As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgListPQR As System.Web.UI.WebControls.DataGrid


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents txtPQRNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents icTglApplyDari As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglApplySampai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblPopUpDealerBranch As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealerBranch As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Cek privilege"
    Private Sub InitiateAuthorization()
        If Not (SecurityProvider.Authorize(context.User, SR.PQRListView_Privilege)) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PRODUCT QUALITY REPORT - Daftar PQR")
        End If
    End Sub
    Dim bCekGridPriv As Boolean = SecurityProvider.Authorize(context.User, SR.PQRReplyDiscuss_Privilege)
    Dim bCekDownloadPriv As Boolean = SecurityProvider.Authorize(context.User, SR.PQRListDownload_Privilege)
#End Region

    Private Sub BindKategori()
        ddlKategori.Items.Clear()
        ddlKategori.DataSource = oCategoryFacade.RetrieveActiveList()
        ddlKategori.DataTextField = "CategoryCode"
        ddlKategori.DataValueField = "ID"
        ddlKategori.DataBind()
        ddlKategori.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub

    Private Sub BindCreator()
        ddlCreator.Items.Clear()
        ddlCreator.Items.Insert(0, New ListItem("Semua", -1))
        ddlCreator.Items.Insert(1, New ListItem("Dealer", 1))
        ddlCreator.Items.Insert(2, New ListItem("STSD", 2))
    End Sub

    Private Sub BindPQRStatus(ByVal control As DropDownList)
        ddlStatus.Items.Clear()
        objDealer = Session("DEALER")
        'If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        ddlStatus.DataSource = New EnumPQR().RetrievePQRStatus()
        'Else
        'ddlStatus.DataSource = New EnumPQR().RetrievePQRStatus(True)
        'End If

        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataBind()
        ddlStatus.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub
    Private Sub ClearForm()
        'txtKodeDealer.Text = ""
        txtKodePosisi.Text = ""
        txtPQRNo.Text = ""
        icTglApplyDari.Value = DateTime.Today
        icTglApplySampai.Value = DateTime.Today
        ddlKategori.SelectedIndex = 0
        ddlStatus.SelectedIndex = 0
    End Sub
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If Not IsPostBack Then
            If oLoginUser.Dealer.Title.Trim = "0" Then 'Dealer
                lblKodeDealer.Text = oLoginUser.Dealer.DealerCode & " - " & oLoginUser.Dealer.DealerName
                txtKodeDealer.Text = oLoginUser.Dealer.DealerCode
                txtKodeDealer.Visible = False
                lblSearchDealer.Visible = False
                txtProcessBy.Visible = False
                lblProcessBy.Visible = False
            End If
            BindKategori()
            BindPqrType()
            BindPQRStatus(ddlStatus)
            BindCreator()
            ClearForm()
            GetSessionCriteria()
            BindToGrid(dgListPQR.CurrentPageIndex, True)
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        lblPopUpDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If icTglApplyDari.Value > icTglApplySampai.Value Then
            MessageBox.Show("Tanggal pencarian mulai tidak boleh lebih dari tanggal sampai dengan.")
        Else
            Dim objDealer As Dealer = Session.Item("DEALER")
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If ddlStatus.SelectedValue = EnumPQR.PQRStatus.Baru Then
                    If SecurityProvider.Authorize(context.User, SR.PQR_daftar_status_Baru_Privilege) Then
                        dgListPQR.CurrentPageIndex = 0
                        BindToGrid(dgListPQR.CurrentPageIndex, True)
                    Else
                        MessageBox.Show("Anda tidak berhak melihat data dengan status Baru")
                    End If
                Else
                    dgListPQR.CurrentPageIndex = 0
                    BindToGrid(dgListPQR.CurrentPageIndex, True)
                End If
            Else
                dgListPQR.CurrentPageIndex = 0
                BindToGrid(dgListPQR.CurrentPageIndex, True)
            End If
        End If
    End Sub

    Sub BindToGrid(ByVal currentPageIndex As Integer, ByVal isSearch As Boolean)
        Dim total As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRHeaderBB), "RowStatus", MatchType.No, CType(DBRowStatus.Deleted, Short)))

        Dim pqrType As Integer = ddlPqrType.SelectedIndex
        If pqrType > 0 Then
            criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "PQRType", MatchType.Exact, pqrType - 1))
        End If

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        Else
            If SecurityProvider.Authorize(context.User, SR.PQRNewSave_Privilege) Then
                criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "CreatedBy", MatchType.StartsWith, "000002"))
            End If
        End If

        'criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "RowStatus", MatchType.No, CType(EnumPQR.PQRStatus.Batal, Short)))
        If chkFilterTanggal.Checked Then
            criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "DocumentDate", MatchType.GreaterOrEqual, icTglApplyDari.Value.AddDays(0)))
            criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "DocumentDate", MatchType.Lesser, icTglApplySampai.Value.AddDays(1)))
        End If

        If txtKodeDealer.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "Dealer.DealerCode", MatchType.[Partial], txtKodeDealer.Text))
        If txtPQRNo.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "PQRNo", MatchType.[Partial], txtPQRNo.Text))
        If ddlKategori.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "Category.ID", MatchType.Exact, ddlKategori.SelectedValue))
        If ddlStatus.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "RowStatus", MatchType.Exact, ddlStatus.SelectedValue))
        If Not SecurityProvider.Authorize(context.User, SR.PQR_daftar_status_Baru_Privilege) Then
            criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "RowStatus", MatchType.No, CType(EnumPQR.PQRStatus.Baru, Short)))
        End If
        If txtKodePosisi.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "ID", MatchType.InSet, "(" & GenerateDamageCodeCriteria(txtKodePosisi.Text.Trim) & ")"))

        'If txtKodePosisi.Text <> String.Empty Then
        '    If txtKodePosisi.Text.Length < 2 Then
        '        MessageBox.Show("Pencarian kode posisi min 2 karakter")
        '        Return
        '    End If
        '    Dim SearchString As String = "''"
        '    Dim critKodePosisi As New CriteriaComposite(New Criteria(GetType(DeskripsiKodePosisi), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    critKodePosisi.opAnd(New Criteria(GetType(DeskripsiKodePosisi), "KodePosition", MatchType.StartsWith, txtKodePosisi.Text))
        '    Dim arKodePosisi As ArrayList = New DeskripsiKodePosisiFacade(User).Retrieve(critKodePosisi)

        '    If arKodePosisi.Count > 0 Then
        '        Dim ListIDKodePosisi As String = String.Empty
        '        For Each item As DeskripsiKodePosisi In arKodePosisi
        '            ListIDKodePosisi += item.ID.ToString() & ","
        '        Next
        '        ListIDKodePosisi = ListIDKodePosisi.Substring(0, ListIDKodePosisi.Length - 1)

        '        Dim critPQRDamageCodeBB As New CriteriaComposite(New Criteria(GetType(PQRDamageCodeBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '        critPQRDamageCodeBB.opAnd(New Criteria(GetType(PQRDamageCodeBB), "DeskripsiKodePosisi.ID", MatchType.InSet, "(" & ListIDKodePosisi & ")"))

        '        Dim arPQRDamageCodeBB As ArrayList = New PQRDamageCodeBBFacade(User).Retrieve(critPQRDamageCodeBB)
        '        If arPQRDamageCodeBB.Count > 0 Then
        '            For Each _PQRDamage As PQRDamageCodeBB In arPQRDamageCodeBB
        '                SearchString += "," & _PQRDamage.PQRHeaderBB.ID.ToString()
        '            Next
        '        End If
        '    End If
        '    criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "ID", MatchType.InSet, "(" & SearchString & ")"))
        'End If

        If txtProcessBy.Text <> String.Empty Then
            If txtProcessBy.Text.Trim.Split("-".ToCharArray(), 2).Length > 1 Then
                Dim sOrg As String
                Dim sUserName As String

                sOrg = txtProcessBy.Text.Trim.Split("-".ToCharArray(), 2)(0)
                sUserName = txtProcessBy.Text.Trim.Split("-".ToCharArray(), 2)(1)

                Dim lCriterias As New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                lCriterias.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, sOrg))

                Dim ListOrganization As ArrayList = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(lCriterias)
                If sOrg.Trim <> String.Empty Then
                    If ListOrganization.Count = 1 Then
                        criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "ConfirmBy", MatchType.[Partial], CType(ListOrganization(0), Dealer).ID.ToString & "%" & sUserName))
                    ElseIf ListOrganization.Count > 1 Then
                        For i As Integer = 0 To ListOrganization.Count - 1
                            If i = 0 Then
                                criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "ConfirmBy", MatchType.[Partial], CType(ListOrganization(i), Dealer).ID.ToString & "%" & sUserName), "(", True)
                            ElseIf i = ListOrganization.Count - 1 Then
                                criterias.opOr(New Criteria(GetType(PQRHeaderBB), "ConfirmBy", MatchType.[Partial], CType(ListOrganization(i), Dealer).ID.ToString & "%" & sUserName), ")", False)
                            Else
                                criterias.opOr(New Criteria(GetType(PQRHeaderBB), "ConfirmBy", MatchType.[Partial], CType(ListOrganization(i), Dealer).ID.ToString & "%" & sUserName))
                            End If
                        Next
                    Else
                        criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "ConfirmBy", MatchType.[Partial], txtProcessBy.Text.Trim.Split("-".ToCharArray(), 2)(1)))
                    End If
                Else
                    criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "ConfirmBy", MatchType.[Partial], txtProcessBy.Text.Trim.Split("-".ToCharArray(), 2)(1)))
                End If
            Else
                criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "ConfirmBy", MatchType.[Partial], txtProcessBy.Text))
            End If
        End If

        If ddlCreator.SelectedValue = 1 Then
            Dim sqlCreatedBy As String = "(select distinct(CreatedBy) from PQRHeaderBB where left(CreatedBy,6)='000002')"
            criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "CreatedBy", MatchType.NotInSet, sqlCreatedBy))
        ElseIf ddlCreator.SelectedValue = 2 Then
            criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "CreatedBy", MatchType.StartsWith, "000002"))
        End If


        If isSearch Then
            _arrPQRHeaderBB = oPQRHeaderBBFacade.RetrieveByCriteria(criterias, CType(ViewState("CurrentSortColumn"), String), _
                  CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            total = _arrPQRHeaderBB.Count
            sessHelper.SetSession("ListPQRDownload", _arrPQRHeaderBB)
        Else
            _arrPQRHeaderBB = oPQRHeaderBBFacade.RetrieveByCriteria(criterias, currentPageIndex + 1, dgListPQR.PageSize, _
                  total, CType(ViewState("CurrentSortColumn"), String), _
                  CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        End If

        sessHelper.SetSession("ListPQR", _arrPQRHeaderBB)

        If (_arrPQRHeaderBB.Count > 0) Then
            'pnlChangeStatus.Visible = True
            btnDownload.Enabled = bCekDownloadPriv
            dgListPQR.VirtualItemCount = total
            dgListPQR.DataSource = _arrPQRHeaderBB
            dgListPQR.DataBind()
            'SetStatusPanel()
        Else
            'pnlChangeStatus.Visible = False
            'MessageBox.Show(SR.DataNotFound("PQR"))
            dgListPQR.DataSource = New ArrayList
            dgListPQR.DataBind()
        End If
    End Sub

    Private Function GenerateDamageCodeCriteria(ByVal sCriteria As String) As String
        'Dim lCriterias As New CriteriaComposite(New Criteria(GetType(PQRDamageCodeBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'If sCriteria.Trim.Split(";").Length > 1 Then

        '    For i As Integer = 0 To sCriteria.Trim.Split(";").Length - 1
        '        If i = 0 Then
        '            lCriterias.opAnd(New Criteria(GetType(PQRDamageCodeBB), "DeskripsiKodePosisi.KodePosition", MatchType.StartsWith, sCriteria.Trim.Split(";")(i)), "(", True)
        '        ElseIf i = sCriteria.Trim.Split(";").Length - 1 Then
        '            lCriterias.opOr(New Criteria(GetType(PQRDamageCodeBB), "DeskripsiKodePosisi.KodePosition", MatchType.StartsWith, sCriteria.Trim.Split(";")(i)), ")", False)
        '        Else
        '            lCriterias.opOr(New Criteria(GetType(PQRDamageCodeBB), "DeskripsiKodePosisi.KodePosition", MatchType.StartsWith, sCriteria.Trim.Split(";")(i)))
        '        End If
        '    Next

        'Else
        '    lCriterias.opAnd(New Criteria(GetType(PQRDamageCodeBB), "DeskripsiKodePosisi.KodePosition", MatchType.StartsWith, sCriteria))
        'End If

        'Dim _arr As ArrayList = New PQRDamageCodeBBFacade(User).Retrieve(lCriterias)

        'Dim _tem As String = ""
        'For Each item As PQRDamageCodeBB In _arr
        '    _tem = _tem + item.PQRHeaderBB.ID.ToString + ","
        'Next
        'If _tem.Length > 0 Then
        '    _tem = _tem.Substring(0, _tem.Length - 1)
        'Else
        '    _tem = "0"
        'End If
        'Return _tem

        Dim insetStr As String = ""
        Dim critStr As String = ""
        If sCriteria.Trim.Split(";").Length = 1 Then
            insetStr = "select PQRHeaderBBID from PQRDamageCodeBB P join DeskripsiKodePosisi D on(P.DeskripsiKodePosisiID=D.ID) where D.KodePosition like '" & sCriteria & "%'"
        Else
            For i As Integer = 0 To sCriteria.Trim.Split(";").Length - 1
                If i = 0 Then
                    critStr = " D.KodePosition like '%" & sCriteria.Trim.Split(";")(i) & "'"
                    'ElseIf i = sCriteria.Trim.Split(";").Length - 1 Then
                    '    critStr = " D.KodePosition like '%" & sCriteria.Trim.Split(";")(i) & "'"
                Else
                    critStr = " or D.KodePosition like '%" & sCriteria.Trim.Split(";")(i) & "'"
                End If
            Next
            insetStr = "select PQRHeaderBBID from PQRDamageCodeBB P join DeskripsiKodePosisi D on(P.DeskripsiKodePosisiID=D.ID) where " & critStr
        End If

        Return insetStr

    End Function

    Private Sub dgListPQR_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgListPQR.SortCommand
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
        BindToGrid(dgListPQR.CurrentPageIndex, False)

    End Sub

    Private Sub dgListPQR_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgListPQR.ItemDataBound

        If e.Item.ItemIndex >= 0 Then
            Dim objPQRHeaderBB As PQRHeaderBB = CType(e.Item.DataItem, PQRHeaderBB)
            Dim chk As CheckBox = CType(e.Item.FindControl("chkSelection"), CheckBox)

            If oLoginUser.Dealer.Title.Trim = "0" Then 'dealer
                If objPQRHeaderBB.RowStatus = EnumPQR.PQRStatus.Selesai Then
                    chk.Visible = True
                Else
                    chk.Visible = False
                End If
            End If

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (e.Item.ItemIndex + 1) + (dgListPQR.CurrentPageIndex * dgListPQR.PageSize)


            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
            Dim lnkbtnView As LinkButton = CType(e.Item.FindControl("lnkbtnView"), LinkButton)
            lnkbtnEdit.Visible = False
            lnkbtnView.Visible = False
            Select Case CType(objPQRHeaderBB.RowStatus, EnumPQR.PQRStatus)
                Case EnumPQR.PQRStatus.Baru
                    lblStatus.Text = EnumPQR.PQRStatus.Baru.ToString
                    Select Case oLoginUser.Dealer.Title.Trim
                        Case "0" 'Dealer
                            If objPQRHeaderBB.CreatedBy.Substring(0, 6) = "000002" Then
                                lnkbtnView.Visible = True
                            Else
                                lnkbtnEdit.Visible = True
                            End If
                        Case "1" 'KTB
                            If SecurityProvider.Authorize(context.User, SR.PQRNewSave_Privilege) Then
                                'lnkbtnEdit.Visible = True
                                lnkbtnView.Visible = True
                            Else
                                'lnkbtnView.Visible = True
                                lnkbtnEdit.Visible = True
                            End If
                    End Select
                Case EnumPQR.PQRStatus.Batal
                    lblStatus.Text = EnumPQR.PQRStatus.Batal.ToString
                    lnkbtnView.Visible = True
                Case EnumPQR.PQRStatus.Proses
                    lblStatus.Text = EnumPQR.PQRStatus.Proses.ToString
                    Select Case oLoginUser.Dealer.Title.Trim
                        Case "0" 'Dealer
                            lnkbtnView.Visible = True
                        Case "1" 'KTB
                            If SecurityProvider.Authorize(context.User, SR.PQRNewSave_Privilege) Then
                                lnkbtnView.Visible = True
                            Else
                                lnkbtnEdit.Visible = True
                            End If
                    End Select
                Case EnumPQR.PQRStatus.Rilis
                    lblStatus.Text = EnumPQR.PQRStatus.Rilis.ToString
                    Select Case oLoginUser.Dealer.Title.Trim
                        Case "0" 'Dealer Bug 1824
                            lnkbtnView.Visible = True
                            lnkbtnEdit.Visible = False
                        Case "1" 'KTB
                            If SecurityProvider.Authorize(context.User, SR.PQRNewSave_Privilege) Then
                                lnkbtnView.Visible = True
                            Else
                                lnkbtnEdit.Visible = True
                            End If

                    End Select
                Case EnumPQR.PQRStatus.Selesai
                    lblStatus.Text = EnumPQR.PQRStatus.Selesai.ToString
                    lnkbtnView.Visible = True
                Case EnumPQR.PQRStatus.Validasi
                    lblStatus.Text = EnumPQR.PQRStatus.Validasi.ToString
                    Select Case oLoginUser.Dealer.Title.Trim
                        Case "0" 'Dealer Bug 1824
                            lnkbtnView.Visible = True
                            lnkbtnEdit.Visible = False
                        Case "1" 'KTB
                            If SecurityProvider.Authorize(Context.User, SR.PQRNewSave_Privilege) Then
                                lnkbtnView.Visible = True
                            Else
                                lnkbtnEdit.Visible = True
                            End If

                    End Select
            End Select

            Dim lblTglSelesai As Label = CType(e.Item.FindControl("lblTglSelesai"), Label)
            If objPQRHeaderBB.FinishDate < New DateTime(1900, 1, 1) Then
                lblTglSelesai.Text = ""
            Else
                lblTglSelesai.Text = objPQRHeaderBB.FinishDate.ToString("dd/MM/yyyy")
            End If


            Dim lblProcessUser As Label = CType(e.Item.FindControl("lblProcessUser"), Label)
            If objPQRHeaderBB.ConfirmBy <> String.Empty Then
                lblProcessUser.Text = CommonFunction.FormatSavedUser(objPQRHeaderBB.ConfirmBy, User)
            Else
                lblProcessUser.Text = ""
            End If

            Dim lblCreatedUser As Label = CType(e.Item.FindControl("lblCreatedUser"), Label)
            If objPQRHeaderBB.CreatedBy <> String.Empty Then
                lblCreatedUser.Text = CommonFunction.FormatSavedUser(objPQRHeaderBB.CreatedBy, User)
            Else
                lblCreatedUser.Text = ""
            End If

            Dim lblHistoryStatus As Label = CType(e.Item.FindControl("lblHistoryStatus"), Label)
            lblHistoryStatus.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpChangeStatusHistoryPQRBB.aspx?id=" & objPQRHeaderBB.ID, "", 400, 400, "PopUpHistory")



            Dim lnkEditWSC As LinkButton = CType(e.Item.FindControl("lnkEditWSC"), LinkButton)
            Dim _sesshelper As SessionHelper = New SessionHelper
            Dim _objDealer As Dealer = CType(_sesshelper.GetSession("DEALER"), Dealer)
            Dim _isDealer As Boolean = False
            Dim _isPqrWSC As Boolean = False
            Dim _isEmptyClaimDocument As Boolean = False
            If (_objDealer.DealerCode.Equals("MKS") = False) Then
                _isDealer = True
            End If
            If (objPQRHeaderBB.PQRType = 0) Then
                _isPqrWSC = True
            End If

            'Add by Reza
            Dim criterias As WSCHeader

            If lblStatus.Text = EnumPQR.PQRStatus.Batal.ToString OrElse objPQRHeaderBB.PQRType = 1 Then
                lnkEditWSC.Visible = False
            Else
                'lnkEditWSC.Visible = True
                If (_isPqrWSC = True And _isDealer = True) Then
                    lnkEditWSC.Visible = True
                Else
                    lnkEditWSC.Visible = False
                End If
            End If

            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(WSCHeader), "PQR", MatchType.Exact, objPQRHeaderBB.PQRNo))
            Dim PQRF As ArrayList = New WSCHeaderFacade(User).Retrieve(criteria)
            For Each asd As WSCHeader In PQRF
                If asd.PQR = objPQRHeaderBB.PQRNo Then
                    lnkEditWSC.Visible = False
                    Exit For
                Else
                    lnkEditWSC.Visible = True
                    Exit For
                End If
            Next


            'End

            Dim lnkbtnAdditionalInfoIcon As LinkButton = CType(e.Item.FindControl("lnkbtnAdditionalInfoIcon"), LinkButton)
            If objPQRHeaderBB.PQRAdditionalInfoBBs.Count > 0 Then
                lnkbtnAdditionalInfoIcon.Visible = bCekGridPriv

                Dim tempArr As ArrayList = objPQRHeaderBB.PQRAdditionalInfoBBs
                tempArr = CommonFunction.SortArraylist(tempArr, GetType(PQRAdditionalInfoBB), "CreatedTime", Sort.SortDirection.DESC)

                Dim obj As PQRAdditionalInfoBB = CType(tempArr(0), PQRAdditionalInfoBB)
                lnkbtnAdditionalInfoIcon.ToolTip = CommonFunction.FormatSavedUser(obj.CreatedBy, User) & " [" & obj.CreatedTime.ToString("dd/MM/yyyy HH:mm:ss") & "]"

                If obj.CreatedBy.Length > 6 Then
                    Dim LastPostedUser As Dealer = New General.DealerFacade(User).Retrieve(CInt(obj.CreatedBy.Substring(0, 6)))
                    If Not LastPostedUser Is Nothing Then
                        Dim img As HtmlImage = CType(lnkbtnAdditionalInfoIcon.FindControl("img"), HtmlImage)
                        If LastPostedUser.Title = 0 Then    'Dealer
                            img.Src = "../images/icon_mail_1.gif"
                        ElseIf LastPostedUser.Title = 1 Then    'KTB
                            Dim identity As System.Security.Principal.GenericIdentity = New System.Security.Principal.GenericIdentity(obj.CreatedBy)
                            Dim principal As System.Security.Principal.GenericPrincipal = New System.Security.Principal.GenericPrincipal(identity, New String() {SR.PQRNewSave_Privilege})
                            If SecurityProvider.Authorize(principal, SR.PQRNewSave_Privilege) Then
                                img.Src = "../images/icon_mail_1.gif"
                            Else
                                img.Src = "../images/icon_mail.gif"
                            End If

                        End If
                    End If
                End If

            Else
                lnkbtnAdditionalInfoIcon.Visible = False
            End If

            Dim lblKodePosisi As Label = e.Item.FindControl("lblKodePosisi")
            lblKodePosisi.Text = getPQRPosition(objPQRHeaderBB)


            Dim RowValue As PQRHeaderBB = CType(e.Item.DataItem, PQRHeaderBB)
            If (RowValue.PQRType = 0) Then
                e.Item.Cells(6).Text = "PQR WSC"
            Else
                e.Item.Cells(6).Text = "PQR Only"
            End If
        End If
    End Sub
    Private Sub dgListPQR_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgListPQR.ItemCommand
        Select Case e.CommandName
            Case "BWSC"
                oPQRHeaderBB = oPQRHeaderBBFacade.Retrieve(CType(e.CommandArgument, Integer))
                sessHelper.SetSession("PQR", oPQRHeaderBB)
                SetSessionCriteria()
                Server.Transfer("~/Service/FrmWSCHeaderBB.aspx?screenFrom=PQR&PQRId=" & oPQRHeaderBB.ID)
            Case "Edit"
                oPQRHeaderBB = oPQRHeaderBBFacade.Retrieve(CType(e.CommandArgument, Integer))
                sessHelper.SetSession("PQR", oPQRHeaderBB)
                SetSessionCriteria()
                Server.Transfer("~/PQR/FrmPQRHeaderBB.aspx?Mode=Edit")
                BindToGrid(dgListPQR.CurrentPageIndex, True)
            Case "View"
                oPQRHeaderBB = oPQRHeaderBBFacade.Retrieve(CType(e.CommandArgument, Integer))
                sessHelper.SetSession("PQR", oPQRHeaderBB)
                SetSessionCriteria()
                Server.Transfer("~/PQR/FrmPQRHeaderBB.aspx?Mode=View")
                BindToGrid(dgListPQR.CurrentPageIndex, True)
            Case "Delete" 'Delete this datagrid item 
                oPQRHeaderBB = oPQRHeaderBBFacade.Retrieve(CType(e.CommandArgument, Integer))
                oPQRHeaderBBFacade.DeleteTransaction(oPQRHeaderBB)
                BindToGrid(dgListPQR.CurrentPageIndex, True)
                'Try

                '    Dim deletedAttachment As PQRAttachmentBB = CType(_arrAttachmentBottom(e.Item.ItemIndex), PQRAttachmentBB)
                '    If Not deletedAttachment.NewItem Then
                '        RemovePQRAttachmentBB(deletedAttachment)
                '    End If
                '    _arrAttachmentBottom.RemoveAt(e.Item.ItemIndex)
                'Catch ex As Exception

                'End Try
            Case "AdditionalInfo"
                SetSessionCriteria()
                Response.Redirect("../PQR/FrmPQRAdditionalInfoBB.aspx?Mode=View&PQRID=" & e.CommandArgument & "&Src=ListPQR")
        End Select
    End Sub
    Private Sub SetStatusPanel()
        'If ddlStatus.SelectedValue = -1 Then
        '    pnlChangeStatus.Visible = False
        'Else
        '    pnlChangeStatus.Visible = True
        '    ddlStatus2.Items.Clear()
        '    ddlStatus2.Items.Add(New ListItem("Pilih Status Baru", -1))
        '    Select Case CType(ddlStatus.SelectedValue, EnumPQR.PQRStatus)
        '        Case EnumPQR.PQRStatus.Batal
        '            pnlChangeStatus.Visible = False
        '        Case EnumPQR.PQRStatus.Baru
        '            ddlStatus2.Items.Add(New ListItem("Batal PQR", EnumPQR.PQRStatus.Batal))
        '            ddlStatus2.Items.Add(New ListItem("Validasi PQR", EnumPQR.PQRStatus.Validasi))

        '        Case EnumPQR.PQRStatus.Validasi
        '            ddlStatus2.Items.Add(New ListItem("Batal Validasi PQR", EnumPQR.PQRStatus.Baru))
        '            ddlStatus2.Items.Add(New ListItem("Proses PQR", EnumPQR.PQRStatus.Proses))

        '        Case EnumPQR.PQRStatus.Proses
        '            ddlStatus2.Items.Add(New ListItem("Batal Proses PQR", EnumPQR.PQRStatus.Validasi))
        '            ddlStatus2.Items.Add(New ListItem("Rilis PQR", EnumPQR.PQRStatus.Rilis))

        '        Case EnumPQR.PQRStatus.Rilis
        '            ddlStatus2.Items.Add(New ListItem("Batal Rilis PQR", EnumPQR.PQRStatus.Proses))
        '            ddlStatus2.Items.Add(New ListItem("Selesai PQR", EnumPQR.PQRStatus.Selesai))

        '        Case EnumPQR.PQRStatus.Selesai
        '            pnlChangeStatus.Visible = False
        '    End Select
        'End If

        pnlChangeStatus.Visible = True
        ddlStatus2.Items.Clear()
        ddlStatus2.Items.Add(New ListItem("Silakan Pilih", "Silakan Pilih"))
        If SecurityProvider.Authorize(context.User, SR.PQRListStatusCancel_Privilege) Then
            ddlStatus2.Items.Add(New ListItem("Batal", "Batal"))
        End If
        If SecurityProvider.Authorize(context.User, SR.PQRListStatusValidate_Privilege) Then
            ddlStatus2.Items.Add(New ListItem("Validasi", "Validasi"))
        End If
        If SecurityProvider.Authorize(context.User, SR.PQRListStatusCancelValidate_Privilege) Then
            ddlStatus2.Items.Add(New ListItem("Batal Validasi", "Batal Validasi"))
        End If
        If SecurityProvider.Authorize(context.User, SR.PQRListStatusProcess_Privilege) Then
            ddlStatus2.Items.Add(New ListItem("Proses", "Proses"))
        End If
        If SecurityProvider.Authorize(context.User, SR.PQRListStatusCancelProcess_Privilege) Then
            ddlStatus2.Items.Add(New ListItem("Batal Proses", "Batal Proses"))
        End If
        If SecurityProvider.Authorize(context.User, SR.PQRListStatusRilis_Privilege) Then
            ddlStatus2.Items.Add(New ListItem("Rilis", "Rilis"))
        End If
        If SecurityProvider.Authorize(context.User, SR.PQRListStatusCancelRilis_Privilege) Then
            ddlStatus2.Items.Add(New ListItem("Batal Rilis", "Batal Rilis"))
        End If
        If SecurityProvider.Authorize(context.User, SR.PQRListStatusFinish_Privilege) Then
            ddlStatus2.Items.Add(New ListItem("Selesai", "Selesai"))
        End If
    End Sub

    Private Sub BindPqrType()
        ddlPqrType.Items.Clear()
        ddlPqrType.Items.Add(New ListItem("Silahkan Pilih", Nothing))
        ddlPqrType.Items.Add(New ListItem("PQR WSC", 0))
        ddlPqrType.Items.Add(New ListItem("PQR Only", 1))
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim i As Integer = 0
        Dim ErrMessage As String = String.Empty
        Dim arrList As ArrayList = New ArrayList
        Dim NewStatus As EnumPQR.PQRStatus

        For Each item As DataGridItem In dgListPQR.Items
            Dim chk As CheckBox = CType(item.FindControl("chkSelection"), CheckBox)
            If (chk.Checked) Then
                oPQRHeaderBB = CType(CType(sessHelper.GetSession("ListPQR"), ArrayList)(i), PQRHeaderBB) 'oPQRHeaderBBFacade.Retrieve(CInt(dgListPQR.DataKeys().Item(i)))
                Select Case ddlStatus2.SelectedValue
                    Case "Batal", "Validasi"
                        ' valid status = "Baru"
                        If ddlStatus2.SelectedValue = "Batal" Then
                            NewStatus = EnumPQR.PQRStatus.Batal
                        ElseIf ddlStatus2.SelectedValue = "Validasi" Then
                            NewStatus = EnumPQR.PQRStatus.Validasi
                        End If

                        If oPQRHeaderBB.RowStatus = EnumPQR.PQRStatus.Baru Then
                            arrList.Add(oPQRHeaderBB)
                        End If
                    Case "Batal Validasi", "Proses"
                        ' valid status = "Validasi"
                        If ddlStatus2.SelectedValue = "Batal Validasi" Then
                            NewStatus = EnumPQR.PQRStatus.Baru
                        ElseIf ddlStatus2.SelectedValue = "Proses" Then
                            NewStatus = EnumPQR.PQRStatus.Proses
                        End If

                        If oPQRHeaderBB.RowStatus = EnumPQR.PQRStatus.Validasi Then
                            arrList.Add(oPQRHeaderBB)
                        End If
                    Case "Batal Proses", "Rilis"
                        ' valid status = "Proses"
                        If ddlStatus2.SelectedValue = "Batal Proses" Then
                            NewStatus = EnumPQR.PQRStatus.Validasi
                        ElseIf ddlStatus2.SelectedValue = "Rilis" Then
                            NewStatus = EnumPQR.PQRStatus.Rilis
                        End If

                        If oPQRHeaderBB.RowStatus = EnumPQR.PQRStatus.Proses Then
                            arrList.Add(oPQRHeaderBB)
                        End If
                    Case "Batal Rilis", "Selesai"
                        ' valid status = "Rilis"
                        If ddlStatus2.SelectedValue = "Batal Rilis" Then
                            NewStatus = EnumPQR.PQRStatus.Proses
                        ElseIf ddlStatus2.SelectedValue = "Selesai" Then
                            NewStatus = EnumPQR.PQRStatus.Selesai
                        End If

                        If oPQRHeaderBB.RowStatus = EnumPQR.PQRStatus.Rilis Then
                            arrList.Add(oPQRHeaderBB)
                        End If
                End Select
            End If
            i = i + 1
        Next

        If (arrList.Count > 0) Then
            If ddlStatus2.SelectedValue = "Silakan Pilih" Then
                MessageBox.Show("Silakan Pilih Status Baru")
                Return
            Else
                If (oPQRHeaderBBFacade.UbahStatusPQRDocument(arrList, NewStatus, ErrMessage) = -1) Then
                    If ErrMessage = String.Empty Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(ErrMessage)
                    End If
                Else
                    MessageBox.Show(SR.SaveSuccess)
                    BindToGrid(dgListPQR.CurrentPageIndex, True)
                End If
            End If

        Else
            MessageBox.Show("Tidak ada data yg di pilih atau data yg valid.")
        End If

    End Sub
    Private Sub dgListPQR_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgListPQR.PageIndexChanged
        dgListPQR.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dgListPQR.CurrentPageIndex, False)
    End Sub
    Private Sub SetSessionCriteria()
        Dim arrLastState As ArrayList = New ArrayList
        arrLastState.Add(txtKodeDealer.Text)
        arrLastState.Add(txtPQRNo.Text)
        arrLastState.Add(icTglApplyDari.Value)
        arrLastState.Add(icTglApplySampai.Value)
        arrLastState.Add(txtKodePosisi.Text.Trim)

        arrLastState.Add(ddlKategori.SelectedIndex)
        arrLastState.Add(ddlStatus.SelectedIndex)
        arrLastState.Add(dgListPQR.CurrentPageIndex)
        arrLastState.Add(CType(ViewState("currSortColumn"), String))
        arrLastState.Add(CType(ViewState("currSortDirection"), Sort.SortDirection))
        arrLastState.Add(txtProcessBy.Text)
        arrLastState.Add(chkFilterTanggal.Checked)
        sessHelper.SetSession("PQRSESSIONLASTSTATE", arrLastState)
    End Sub
    Private Sub GetSessionCriteria()
        Dim arrLastState As ArrayList = sessHelper.GetSession("PQRSESSIONLASTSTATE")
        If Not arrLastState Is Nothing Then
            txtKodeDealer.Text = arrLastState.Item(0)
            'Dim str() As String = arrLastState.Item(1).ToString().Split(",")
            'For Each item As ListItem In lboxStatus.Items
            '    For i As Integer = 0 To str.Length - 1
            '        If item.Value.ToString = str(i).ToString Then
            '            item.Selected = True
            '            Exit For
            '        End If
            '    Next
            'Next
            txtPQRNo.Text = arrLastState.Item(1)
            icTglApplyDari.Value = arrLastState.Item(2)
            icTglApplySampai.Value = arrLastState.Item(3)
            txtKodePosisi.Text = arrLastState.Item(4)
            ddlKategori.SelectedIndex = arrLastState.Item(5)
            ddlStatus.SelectedIndex = arrLastState.Item(6)

            dgListPQR.CurrentPageIndex = arrLastState.Item(7)
            ViewState("currSortColumn") = arrLastState.Item(8)
            ViewState("currSortDirection") = arrLastState.Item(9)
            txtProcessBy.Text = arrLastState.Item(10)
            chkFilterTanggal.Checked = arrLastState.Item(11)
        Else
            ViewState("currSortColumn") = "Status"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            dgListPQR.CurrentPageIndex = 0

        End If
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim arlToDownload As ArrayList = New ArrayList
        Dim counter As Integer = 0
        Dim nChecked As Integer = 0

        ' Modified by Ikhsan, 11 September 2008
        ' Reported by Rina, as a bug
        ' start ------------------------------------------
        counter = 0


        'If dgListPQR.CurrentPageIndex = 0 Then
        '  counter = 0
        'Else
        '  counter = (dgListPQR.CurrentPageIndex * dgListPQR.PageSize)
        'End If
        ' End  ------------------------------------------

        For Each item As DataGridItem In dgListPQR.Items
            Dim chk As CheckBox = CType(item.FindControl("chkSelection"), CheckBox)

            If chk.Checked Then
                arlToDownload.Add(CType(sessHelper.GetSession("ListPQR"), ArrayList)(counter))
                nChecked += 1
            End If
            'If chk.Checked Then
            ' Modified by Ikhsan, 11 September 2008
            ' Reported by Rina, as a bug
            ' start ------------------------------------------
            'arlToDownload.Add(CType(sessHelper.GetSession("ListPQRDownload"), ArrayList)(counter))
            ' End  ------------------------------------------
            'End If
            counter += 1
        Next

        If nChecked = 0 Then
            DoDownload(CType(sessHelper.GetSession("ListPQRDownload"), ArrayList))
        Else
            DoDownload(arlToDownload)
        End If

        'If arlToDownload.Count = 0 Then
        '    MessageBox.Show("Tidak ada data yang dipilih")
        'Else
        '    DoDownload(arlToDownload)
        'End If
    End Sub

    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String

        sFileName = "DaftarPQR" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls

        '-- Temp file must be a randomly named file!
        Dim TraineeData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(TraineeData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(TraineeData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteTraineeData(sw, data)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub WriteTraineeData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        Dim sProfileConfig As String = KTB.DNet.Lib.WebConfig.GetValue("DownloadPQRFormatProfile")
        Dim header As String
        Dim oPQRHeaderBB As PQRHeaderBB

        If Not IsNothing(data) Then
            '-- Write column header
            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("PQR - Daftar PQR")

            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)

            If oLoginUser.Dealer.Title.Trim = "0" Then 'dealer
                itemLine.Append("Model" & tab)
                itemLine.Append("No Rangka" & tab)
                itemLine.Append("Subject" & tab)
                itemLine.Append("Penjelasan MMKSI" & tab)
                itemLine.Append("Bobot" & tab)
            Else
                'New Format
                itemLine.Append("Tgl Pembuatan" & tab)
                itemLine.Append("Tgl Kerusakan" & tab)
                itemLine.Append("Tgl Delivery" & tab)
                itemLine.Append("Tgl Buka Faktur" & tab)
                itemLine.Append("Tahun Produksi" & tab)
                itemLine.Append("No. Rangka" & tab)
                itemLine.Append("No. Mesin" & tab)
                itemLine.Append("Status" & tab)
                itemLine.Append("Penggunaan" & tab)
                itemLine.Append("Bentuk Body Sekarang" & tab)
                itemLine.Append("Muatan" & tab)
                itemLine.Append("Berat Muatan (Kg)" & tab)
                itemLine.Append("Perawatan" & tab)
                itemLine.Append("Frekwensi" & tab)
                itemLine.Append("Prioritas" & tab)
                itemLine.Append("Kota Operational" & tab)
                itemLine.Append("Kode Posisi" & tab)
                itemLine.Append("Part Name/No" & tab)
                itemLine.Append("Kecepatan" & tab)
                itemLine.Append("Subject" & tab)
                itemLine.Append("Gejala" & tab)
                itemLine.Append("Penyebab" & tab)
                itemLine.Append("Perbaikan" & tab)
                itemLine.Append("Catatan" & tab)
                itemLine.Append("Dealer" & tab)
                itemLine.Append("Odometer" & tab)
                itemLine.Append("No. PQR" & tab)
                itemLine.Append("No. PQR Ref" & tab)
                itemLine.Append("Tipe/Warna" & tab)
                itemLine.Append("Nama Pemilik" & tab)
                itemLine.Append("Area Oper. Jalan Tol (%)" & tab)
                itemLine.Append("Area Oper. Dalam Kota (%)" & tab)
                itemLine.Append("Area Oper. Off Road (%)" & tab)
                itemLine.Append("Area Oper. Pegunungan (%)" & tab)
                itemLine.Append("Kond. Jln. Asphalt (%)" & tab)
                itemLine.Append("Kond. Jln. Lumpur (%)" & tab)
                itemLine.Append("Kond. Jln. Semen (%)" & tab)
                itemLine.Append("Kond. Jln. Tanah (%)" & tab)
                itemLine.Append("Kode Kerusakan A" & tab)
                itemLine.Append("Kode Kerusakan B" & tab)
                itemLine.Append("Kode Kerusakan C" & tab)
                itemLine.Append("Penjelasan MMKSI" & tab)
                itemLine.Append("Bobot" & tab)
                itemLine.Append("Tgl. Selesai" & tab)
                itemLine.Append("Informasi Tambahan" & tab)
            End If

            sw.WriteLine(itemLine.ToString())
            Dim objPQRProfileBBFacade As PQRProfileBBFacade = New PQRProfileBBFacade(User)
            For Each item As PQRHeaderBB In data
                Try
                    itemLine.Remove(0, itemLine.Length)  '-- Empty line
                    If oLoginUser.Dealer.Title.Trim = "0" Then 'dealer
                        If item.RowStatus = EnumPQR.PQRStatus.Selesai Then
                            itemLine.Append(item.Category.CategoryCode & tab)
                            itemLine.Append(item.ChassisMasterBB.ChassisNumber & tab)
                            itemLine.Append(item.Subject & tab)
                            itemLine.Append(item.Solutions.Replace(Chr(13) & Chr(10), " ") & tab)
                            itemLine.Replace(Chr(13) & Chr(10), "")
                            sw.WriteLine(itemLine.ToString)
                        End If
                    Else
                        oPQRHeaderBB = item

                        'New Format
                        itemLine.Append(oPQRHeaderBB.DocumentDate.ToString("dd/MM/yyyy") & tab) 'itemLine.Append("Tgl Pembuatan" & tab)
                        itemLine.Append(oPQRHeaderBB.PQRDate.ToString("dd/MM/yyyy") & tab) 'itemLine.Append("Tgl Kerusakan" & tab)
                        itemLine.Append(oPQRHeaderBB.ChassisMasterBB.DODate.ToString("dd/MM/yyyy") & tab) 'itemLine.Append("Tgl Delivery" & tab)
                        'itemLine.Append(oPQRHeaderBB.ChassisMasterBB.EndCustomer.OpenFakturDate.ToString("dd/MM/yyyy") & tab) 'itemLine.Append("Tgl Buka Faktur" & tab)
                        If oPQRHeaderBB.ChassisMasterBB.EndCustomer Is Nothing Then
                            itemLine.Append("" & tab)
                        Else
                            itemLine.Append(oPQRHeaderBB.ChassisMasterBB.EndCustomer.OpenFakturDate.ToString("dd/MM/yyyy") & tab) 'itemLine.Append("Tgl Buka Faktur" & tab)
                        End If
                        itemLine.Append(oPQRHeaderBB.ChassisMasterBB.ProductionYear.ToString() & tab) 'itemLine.Append("Tahun Produksi" & tab)
                        itemLine.Append(oPQRHeaderBB.ChassisMasterBB.ChassisNumber & tab) 'itemLine.Append("No. Rangka" & tab)
                        itemLine.Append(oPQRHeaderBB.ChassisMasterBB.EngineNumber & tab) 'itemLine.Append("No. Mesin" & tab)
                        'Add by anh 20111020
                        itemLine.Append(CType(CInt(oPQRHeaderBB.RowStatus), EnumPQR.PQRStatus).ToString() & tab) 'itemLine.Append("No. Mesin" & tab)
                        'end Added by anh 20111020
                        itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 1, "PQR_GUNA") & tab) 'itemLine.Append("Penggunaan" & tab)
                        itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 1, "PQR_BODY") & tab) 'itemLine.Append("Bentuk Body Sekarang" & tab)
                        itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 1, "PQR_MUATAN") & tab) 'itemLine.Append("Muatan" & tab)
                        itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 1, "PQR_Beban") & tab) 'itemLine.Append("Berat Muatan (Kg)" & tab)
                        itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 1, "PQR_RAWAT") & tab) 'itemLine.Append("Perawatan" & tab)
                        itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 1, "PQR_FREKWENSI") & tab) 'itemLine.Append("Frekwensi" & tab)
                        itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 1, "PQR_PRIORITAS") & tab) 'itemLine.Append("Prioritas" & tab)
                        itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 1, "PQR_KOTA_OPERATIONAL") & tab) 'itemLine.Append("Kota Operational" & tab)

                        'itemLine.Append("" & tab) 'itemLine.Append("Kode Posisi" & tab)
                        If oPQRHeaderBB.PQRDamageCodeBBs.Count > 0 Then
                            Dim sKodePosisi As String = String.Empty
                            For Each it As PQRDamageCodeBB In oPQRHeaderBB.PQRDamageCodeBBs
                                sKodePosisi += it.DeskripsiKodePosisi.KodePosition & " - " & it.DeskripsiKodePosisi.Description & ""
                            Next
                            itemLine.Append(sKodePosisi & tab)
                        Else
                            itemLine.Append("" & tab)
                        End If
                        'itemLine.Append("" & tab) 'itemLine.Append("Part Name/No" & tab)
                        If oPQRHeaderBB.PQRPartsCodeBBs.Count > 0 Then
                            Dim sPart As String = String.Empty
                            For Each it As PQRPartsCodeBB In oPQRHeaderBB.PQRPartsCodeBBs
                                sPart += it.SparePartMaster.PartNumber & " - " & it.SparePartMaster.PartName & "<br>"
                            Next
                            itemLine.Append(sPart & tab)
                        Else
                            itemLine.Append("" & tab)
                        End If
                        itemLine.Append(oPQRHeaderBB.Velocity.ToString() & tab) 'itemLine.Append("Kecepatan" & tab)
                        itemLine.Append(oPQRHeaderBB.Subject & tab) 'itemLine.Append("Subject" & tab)
                        itemLine.Append(KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(oPQRHeaderBB.Symptomps) & tab) 'itemLine.Append("Gejala" & tab)
                        itemLine.Append(KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(oPQRHeaderBB.Causes) & tab)  'itemLine.Append("Penyebab" & tab)
                        itemLine.Append(KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(oPQRHeaderBB.Results) & tab) 'itemLine.Append("Perbaikan" & tab)
                        itemLine.Append(KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(oPQRHeaderBB.Notes) & tab) 'itemLine.Append("Catatan" & tab)
                        itemLine.Append(oPQRHeaderBB.Dealer.DealerCode & " - " & oPQRHeaderBB.Dealer.SearchTerm1 & tab) 'itemLine.Append("Dealer" & tab)
                        itemLine.Append(oPQRHeaderBB.OdoMeter.ToString("#,##0") & tab) 'itemLine.Append("Odometer" & tab)
                        itemLine.Append(oPQRHeaderBB.PQRNo & tab) 'itemLine.Append("No. PQR" & tab)
                        itemLine.Append(oPQRHeaderBB.RefPQRNo & tab) 'itemLine.Append("No. PQR Ref" & tab)
                        itemLine.Append(oPQRHeaderBB.ChassisMasterBB.VechileColor.MaterialNumber & " - " & oPQRHeaderBB.ChassisMasterBB.VechileColor.MaterialDescription & tab) 'itemLine.Append("Tipe/Warna" & tab)
                        'itemLine.Append("" & tab) 'itemLine.Append("Nama Pemilik" & tab)
                        If IsNothing(oPQRHeaderBB.ChassisMasterBB.EndCustomer) OrElse oPQRHeaderBB.ChassisMasterBB.EndCustomer.Customer Is Nothing Then
                            itemLine.Append("" & tab)
                        Else
                            itemLine.Append(oPQRHeaderBB.ChassisMasterBB.EndCustomer.Customer.Name1 & " - " & oPQRHeaderBB.ChassisMasterBB.EndCustomer.Customer.Alamat & tab)
                        End If
                        itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 2, "PQR_OP_TOL") & tab) 'itemLine.Append("Area Oper. Jalan Tol (%)" & tab)
                        itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 2, "PQR_OP_DKOTA") & tab) 'itemLine.Append("Area Oper. Dalam Kota (%)" & tab)
                        itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 2, "PQR_OP_OFRO") & tab) 'itemLine.Append("Area Oper. Off Road (%)" & tab)
                        itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 2, "PQR_OP_PGUNUNG") & tab) 'itemLine.Append("Area Oper. Pegunungan (%)" & tab)
                        itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 2, "PQR_KJ_ASP") & tab) 'itemLine.Append("Kond. Jln. Asphalt (%)" & tab)
                        itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 2, "PQR_KJ_LUMPUR") & tab) 'itemLine.Append("Kond. Jln. Lumpur (%)" & tab)
                        itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 2, "PQR_KJ_SEMEN") & tab) 'itemLine.Append("Kond. Jln. Semen (%)" & tab)
                        itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 2, "PQR_KJ_SOIL") & tab) 'itemLine.Append("Kond. Jln. Tanah (%)" & tab)

                        itemLine.Append(oPQRHeaderBB.CodeA & tab) 'itemLine.Append("No. PQR" & tab)
                        itemLine.Append(oPQRHeaderBB.CodeB & tab) 'itemLine.Append("No. PQR" & tab)
                        itemLine.Append(oPQRHeaderBB.CodeC & tab) 'itemLine.Append("No. PQR" & tab)
                        itemLine.Append(oPQRHeaderBB.Solutions & tab)   'itemLine.Append("Penjelasan KTB" & tab)
                        itemLine.Append(oPQRHeaderBB.Bobot & tab)  'itemLine.Append("Bobot" & tab)
                        itemLine.Append(oPQRHeaderBB.FinishDate.ToString("dd/MM/yyyy") & tab) 'itemLine.Append("Tgl Selesai" & tab)
                        Dim strAddInfo As String = ""
                        For Each oPQRAI As PQRAdditionalInfoBB In oPQRHeaderBB.PQRAdditionalInfoBBs
                            strAddInfo &= IIf(strAddInfo.Trim = "", "", ";") & GetAdditionalInfoString(oPQRAI)
                        Next
                        itemLine.Append(strAddInfo & tab)

                        itemLine.Replace(Chr(13) & Chr(10), "")
                        sw.WriteLine(itemLine.ToString)
                    End If
                Catch ex As Exception
                    Dim sError As String = ex.Message
                End Try
            Next
        End If
    End Sub

    Private Function GetAdditionalInfoString(ByRef oPQRAI As PQRAdditionalInfoBB) As String
        Dim str As String = ""
        Dim oDFac As DealerFacade = New DealerFacade(User)
        Dim oD As Dealer
        Dim UserName As String = ""

        If oPQRAI.LastUpdateBy.Trim.Length <= "000000".Length Then '"000000UserName"
        Else
            If oPQRAI.CreatedBy = oPQRAI.LastUpdateBy Then
            Else
                oPQRAI.CreatedBy = oPQRAI.LastUpdateBy 'temporary
                oPQRAI.CreatedTime = oPQRAI.LastUpdateTime
            End If
        End If
        oD = oDFac.Retrieve(CType(oPQRAI.CreatedBy.Substring(0, 6), Integer))
        UserName = oPQRAI.CreatedBy.Substring(6)
        str = oD.DealerCode & "." & UserName & "(" & oPQRAI.CreatedTime.ToString("ddMMMyy") & "):" & oPQRAI.Sender

        Return str
    End Function

    Private Function GetPQRProfileBBValue(ByVal PQRHeaderBBID As Integer, ByVal ProfileGroup As Integer, ByVal ProfileHeaderName As String) As String
        'Return ""
        'Exit Function
        Dim Sql As String = ""
        Dim oPQRPFac As PQRProfileBBFacade = New PQRProfileBBFacade(User)
        Dim cPQRP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRProfileBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlPQRP As New ArrayList
        Dim oPQRP As PQRProfileBB
        Dim Rsl As String = ""

        Sql &= " select distinct(pp.ID)" ' pfh.Code, pfh.Description, *  "
        Sql &= " from PQRHeaderBB ph "
        Sql &= " , PQRProfileBB pp"
        Sql &= " , ProfileGroup pg"
        Sql &= " , ProfileHeaderToGroup phg"
        Sql &= " , ProfileHeader pfh"
        Sql &= " where 1=1 "
        Sql &= " and ph.ID=" & PQRHeaderBBID.ToString
        Sql &= " and pp.PQRHeaderBBID=ph.ID"
        Sql &= " and pg.Code='pqr_prf" & IIf(ProfileGroup = 1, "", "_2") & "'"
        Sql &= " and pp.GroupID=pg.ID"
        Sql &= " and phg.ProfileGroupID=pg.ID"
        Sql &= " and phg.ProfileHeaderID=pfh.ID"
        Sql &= " and pp.ProfileHeaderID=pfh.ID"
        Sql &= " and pfh.Code='" & ProfileHeaderName & "'"

        cPQRP.opAnd(New Criteria(GetType(PQRProfileBB), "ID", MatchType.InSet, "(" & Sql & ")"))
        arlPQRP = oPQRPFac.Retrieve(cPQRP)
        If arlPQRP.Count > 0 Then
            oPQRP = CType(arlPQRP(0), PQRProfileBB)

            If ProfileHeaderName.Trim.ToUpper = "PQR_GUNA" OrElse ProfileHeaderName.Trim.ToUpper = "PQR_BODY" OrElse ProfileHeaderName.Trim.ToUpper = "PQR_RAWAT" OrElse ProfileHeaderName.Trim.ToUpper = "PQR_FREKWENSI" OrElse ProfileHeaderName.Trim.ToUpper = "PQR_PRIORITAS" Then
                Dim oPDFac As ProfileDetailFacade = New ProfileDetailFacade(User)
                Dim cPD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim aPD As New ArrayList
                Dim oPD As ProfileDetail

                cPD.opAnd(New Criteria(GetType(ProfileDetail), "ProfileHeader.ID", MatchType.Exact, oPQRP.ProfileHeader.ID))
                cPD.opAnd(New Criteria(GetType(ProfileDetail), "Code", MatchType.Exact, oPQRP.ProfileValue))
                aPD = oPDFac.Retrieve(cPD)
                If aPD.Count = 0 Then
                    Rsl = oPQRP.ProfileValue
                Else
                    oPD = CType(aPD(0), ProfileDetail)
                    Rsl = oPD.Description
                End If
            Else
                Rsl = oPQRP.ProfileValue
            End If
        Else
            Rsl = ""
        End If
        Return Rsl
    End Function

    Private Function getPQRPosition(ByVal objPQR As PQRHeaderBB) As String

        Dim strPosition As String = ""
        Dim arlPQRDamageCodeBB As ArrayList = objPQR.PQRDamageCodeBBs
        Dim count As Integer = 0
        If arlPQRDamageCodeBB.Count > 0 Then
            For Each item As PQRDamageCodeBB In arlPQRDamageCodeBB
                If item.RowStatus = CType(DBRowStatus.Active, Short) Then
                    count += 1
                    'If (count Mod 2) = 0 Then
                    strPosition &= item.DeskripsiKodePosisi.KodePosition & "<br>"
                    'Else
                    '    strPosition &= item.DeskripsiKodePosisi.KodePosition & ","
                    'End If
                End If
            Next
        End If

        If strPosition.Length > 0 Then
            'If (count Mod 2) = 0 Then
            strPosition = Left(strPosition, strPosition.Length - 4)
            'Else
            '    strPosition = Left(strPosition, strPosition.Length - 1)
            'End If
        End If

        Return strPosition

    End Function

End Class
