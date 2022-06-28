Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit

Public Class FrmPQRListKondisiBB
    Inherits System.Web.UI.Page

    Private oPQRHeaderBB As New PQRHeaderBB
    Private oPQRHeaderBBFacade As New PQRHeaderBBFacade(User)
    Private sessHelper As New SessionHelper

    Private oCategoryFacade As New CategoryFacade(User)
    Private _arrPQRHeaderBB As ArrayList
    Private oLoginUser As UserInfo
    Dim TotalBobot As Integer
    Dim TotalInterval As Long

    Protected WithEvents lblDealerSearch As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealerBranch As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblNoChasisSearch As System.Web.UI.WebControls.Label
    Protected WithEvents lblPQRNoSearch As System.Web.UI.WebControls.Label
    Protected WithEvents lblKategoriSearch As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglApplySearch As System.Web.UI.WebControls.Label
    Protected WithEvents lblStat As System.Web.UI.WebControls.Label
    Protected WithEvents btnPreviousStatus As System.Web.UI.WebControls.Button
    Protected WithEvents btnNextStatus As System.Web.UI.WebControls.Button
    Protected WithEvents lblProcessTimeAvg As System.Web.UI.WebControls.Label
    Protected WithEvents lblProcessTimeAvgVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblBobotAvg As System.Web.UI.WebControls.Label
    Protected WithEvents lblBobotAvgVal As System.Web.UI.WebControls.Label
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents icTglValidateDari As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglValidateSampai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblProsesOleh As System.Web.UI.WebControls.Label
    Protected WithEvents txtProsesOleh As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSubject As System.Web.UI.WebControls.Label
    Protected WithEvents dgListPQR As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblDamageCode As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeKerusakan As System.Web.UI.WebControls.TextBox
    Protected WithEvents cbDate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtSubject As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeDealerBranch As System.Web.UI.WebControls.TextBox

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents txtNoChasis As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPQRNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not (SecurityProvider.Authorize(context.User, SR.PQRListConditionView_Privilege)) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PRODUCT QUALITY REPORT - Daftar Kondisi")
        End If
    End Sub

    Dim bCekPriv As Boolean = SecurityProvider.Authorize(context.User, SR.PQRListStatusConditionDownload_Privilege)
#End Region

    Private Sub BindKategori()
        ddlKategori.Items.Clear()
        ddlKategori.DataSource = oCategoryFacade.RetrieveActiveList()
        ddlKategori.DataTextField = "CategoryCode"
        ddlKategori.DataValueField = "ID"
        ddlKategori.DataBind()
        ddlKategori.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub
    Private Sub BindPQRStatus(ByVal control As DropDownList)
        ddlStatus.Items.Clear()
        'Dim objDealer As Dealer = Session("DEALER")
        'If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        ddlStatus.DataSource = New EnumPQR().RetrievePQRStatus()
        'Else
        'ddlStatus.DataSource = New EnumPQR().RetrievePQRStatus(True)
        'End If

        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataBind()
        ddlStatus.Items.RemoveAt(0)
        ddlStatus.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub
    Private Sub ClearForm()
        txtKodeDealer.Text = ""
        txtNoChasis.Text = ""
        txtPQRNo.Text = ""
        txtProsesOleh.Text = ""
        cbDate.Checked = False
        icTglValidateDari.Value = DateTime.Today
        icTglValidateSampai.Value = DateTime.Today
        ddlKategori.SelectedIndex = 0
        ddlStatus.SelectedIndex = 0


    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If Not IsPostBack Then
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            lblSearchDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"
            ViewState("currSortColumn") = "Status"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            BindKategori()
            BindPQRStatus(ddlStatus)
            ClearForm()
            GetSessionCriteria()
            BindToGrid(dgListPQR.CurrentPageIndex)
        End If
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgListPQR.CurrentPageIndex = 0
        BindToGrid(dgListPQR.CurrentPageIndex)
    End Sub
    Sub BindToGrid(ByVal currentPageIndex As Integer, Optional ByVal IsForDownloadPurpose As Boolean = False)
        TotalBobot = 0
        TotalInterval = 0

        Dim total As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRHeaderBB), "RowStatus", MatchType.No, CType(DBRowStatus.Deleted, Short)))
        criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "RowStatus", MatchType.No, CType(EnumPQR.PQRStatus.Batal, Short)))
        criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "RowStatus", MatchType.No, CType(EnumPQR.PQRStatus.Baru, Short)))
        'If cbDate.Checked = True Then'make validatetime as mandatory for selection criteria
        criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "ValidationTime", MatchType.GreaterOrEqual, icTglValidateDari.Value))
        criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "ValidationTime", MatchType.LesserOrEqual, icTglValidateSampai.Value.AddDays(1)))
        'End If
        If SecurityProvider.Authorize(context.User, SR.PQRNewSave_Privilege) Then
            criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "CreatedBy", MatchType.StartsWith, "000002"))
        End If
        If txtKodeDealer.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "Dealer.DealerCode", MatchType.[Partial], txtKodeDealer.Text))
        If txtPQRNo.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "PQRNo", MatchType.[Partial], txtPQRNo.Text))
        If txtNoChasis.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "ChassisMasterBB.ChassisNumber", MatchType.[Partial], txtNoChasis.Text))
        If ddlKategori.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "Category.ID", MatchType.Exact, ddlKategori.SelectedValue))
        If ddlStatus.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "RowStatus", MatchType.Exact, ddlStatus.SelectedValue))
	If txtKodeDealerBranch.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(PQRHeader), "DealerBranch.DealerBranchCode", MatchType.InSet, "('" & txtKodeDealerBranch.Text.Replace(";", "','") & "')"))
        End If
        If txtSubject.Text <> String.Empty Then
            If txtSubject.Text.Split(";").Length > 1 Then

                For i As Integer = 0 To txtSubject.Text.Split(";").Length - 1
                    If i = 0 Then
                        criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "Subject", MatchType.[Partial], txtSubject.Text.Split(";")(i)), "(", True)
                    ElseIf i = txtSubject.Text.Split(";").Length - 1 Then
                        criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "Subject", MatchType.[Partial], txtSubject.Text.Split(";")(i)), ")", False)
                    Else
                        criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "Subject", MatchType.[Partial], txtSubject.Text.Split(";")(i)))
                    End If
                Next
            Else
                criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "Subject", MatchType.[Partial], txtSubject.Text))
            End If
        End If

        If txtKodeKerusakan.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "ID", MatchType.InSet, "(" & GenerateDamageCodeCriteria(txtKodeKerusakan.Text.Trim) & ")"))

        'If txtProsesOleh.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "ConfirmBy", MatchType.Partial, txtProsesOleh.Text))
        If txtProsesOleh.Text <> String.Empty Then
            If txtProsesOleh.Text.Trim.Split("-".ToCharArray(), 2).Length > 1 Then
                Dim sOrg As String
                Dim sUserName As String

                sOrg = txtProsesOleh.Text.Trim.Split("-".ToCharArray(), 2)(0)
                sUserName = txtProsesOleh.Text.Trim.Split("-".ToCharArray(), 2)(1)

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
                        criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "ConfirmBy", MatchType.[Partial], txtProsesOleh.Text.Trim.Split("-".ToCharArray(), 2)(1)))
                    End If
                Else
                    criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "ConfirmBy", MatchType.[Partial], txtProsesOleh.Text.Trim.Split("-".ToCharArray(), 2)(1)))
                End If
            Else
                criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "ConfirmBy", MatchType.[Partial], txtProsesOleh.Text))
            End If
        End If

        If IsForDownloadPurpose Then
            '_arrPQRHeaderBB = oPQRHeaderBBFacade.RetrieveByCriteria(criterias, currentPageIndex + 1, dgListPQR.PageSize, _
            '              total, CType(ViewState("CurrentSortColumn"), String), _
            '              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            _arrPQRHeaderBB = oPQRHeaderBBFacade.RetrieveByCriteria(criterias, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            sessHelper.SetSession("PQRDafterKondisi", _arrPQRHeaderBB)
            Exit Sub
        Else
            _arrPQRHeaderBB = oPQRHeaderBBFacade.RetrieveByCriteria(criterias, currentPageIndex + 1, dgListPQR.PageSize, _
                          total, CType(ViewState("CurrentSortColumn"), String), _
                          CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        End If



        If (_arrPQRHeaderBB.Count > 0) Then
            dgListPQR.VirtualItemCount = total
            dgListPQR.DataSource = _arrPQRHeaderBB
            dgListPQR.DataBind()

            sessHelper.SetSession("PQRDafterKondisi", _arrPQRHeaderBB)

            btnDownload.Visible = bCekPriv
            lblBobotAvgVal.Visible = True
            lblProcessTimeAvgVal.Visible = True

            lblBobotAvgVal.Text = (TotalBobot / total).ToString("#,##0.##")
            Dim ts As New TimeSpan((TotalInterval / total))
            lblProcessTimeAvgVal.Text = ts.Days.ToString & " Hari " & ts.Hours.ToString & " Jam " & ts.Minutes.ToString & " Menit"

        Else
            dgListPQR.DataSource = New ArrayList
            dgListPQR.DataBind()

            sessHelper.SetSession("PQRDafterKondisi", Nothing)

            btnDownload.Visible = False
            lblBobotAvgVal.Visible = False
            lblProcessTimeAvgVal.Visible = False
        End If
    End Sub
    Private Function GenerateDamageCodeCriteria(ByVal sCriteria As String) As String

        Dim strSQL As String = "Select PQRHeaderBBID from PQRDamageCodeBB a join DeskripsiKodePosisi b on(a.DeskripsiKodePosisiID=b.ID)  where a.RowStatus=" & CType(DBRowStatus.Active, Short).ToString & " "
        'Dim lCriterias As New CriteriaComposite(New Criteria(GetType(PQRDamageCodeBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If sCriteria.Trim.Split(";").Length > 1 Then

            For i As Integer = 0 To sCriteria.Trim.Split(";").Length - 1
                If i = 0 Then
                    strSQL &= " and ( KodePosition like '%" & sCriteria.Trim.Split(";")(i) & "%' "
                    'lCriterias.opAnd(New Criteria(GetType(PQRDamageCodeBB), "DeskripsiKodePosisi.KodePosition", MatchType.Partial, sCriteria.Trim.Split(";")(i)), "(", True)
                ElseIf i = sCriteria.Trim.Split(";").Length - 1 Then
                    strSQL &= " or KodePosition like '%" & sCriteria.Trim.Split(";")(i) & "%') "
                    'lCriterias.opOr(New Criteria(GetType(PQRDamageCodeBB), "DeskripsiKodePosisi.KodePosition", MatchType.Partial, sCriteria.Trim.Split(";")(i)), ")", False)
                Else
                    strSQL &= " or KodePosition like '%" & sCriteria.Trim.Split(";")(i) & "%' "
                    'lCriterias.opOr(New Criteria(GetType(PQRDamageCodeBB), "DeskripsiKodePosisi.KodePosition", MatchType.Partial, sCriteria.Trim.Split(";")(i)))
                End If
            Next

        Else
            strSQL &= " and KodePosition like '%" & sCriteria & "%' "
            'lCriterias.opAnd(New Criteria(GetType(PQRDamageCodeBB), "DeskripsiKodePosisi.KodePosition", MatchType.Partial, sCriteria))
        End If

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
        Return strSQL

    End Function
    Private Sub dgListPQR_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgListPQR.SortCommand
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
        BindToGrid(dgListPQR.CurrentPageIndex)

    End Sub
    Private Sub dgListPQR_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgListPQR.ItemDataBound

        If e.Item.ItemIndex >= 0 Then
            Dim objPQRHeaderBB As PQRHeaderBB = CType(e.Item.DataItem, PQRHeaderBB)
            Dim chk As CheckBox = CType(e.Item.FindControl("chkSelection"), CheckBox)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (e.Item.ItemIndex + 1) + (dgListPQR.CurrentPageIndex * dgListPQR.PageSize)

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Select Case CType(objPQRHeaderBB.RowStatus, EnumPQR.PQRStatus)
                Case EnumPQR.PQRStatus.Baru
                    lblStatus.Text = EnumPQR.PQRStatus.Baru.ToString
                Case EnumPQR.PQRStatus.Batal
                    lblStatus.Text = EnumPQR.PQRStatus.Batal.ToString
                Case EnumPQR.PQRStatus.Proses
                    lblStatus.Text = EnumPQR.PQRStatus.Proses.ToString
                Case EnumPQR.PQRStatus.Rilis
                    lblStatus.Text = EnumPQR.PQRStatus.Rilis.ToString
                Case EnumPQR.PQRStatus.Selesai
                    lblStatus.Text = EnumPQR.PQRStatus.Selesai.ToString
                Case EnumPQR.PQRStatus.Validasi
                    lblStatus.Text = EnumPQR.PQRStatus.Validasi.ToString
            End Select

            'If CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo).Dealer.Title.Trim = "0" Then 'dealer
            chk.Visible = True

            'End If

            TotalBobot = TotalBobot + objPQRHeaderBB.Bobot

            Dim d As New DateTime(1753, 1, 1)
            Dim ts As TimeSpan
            ts = objPQRHeaderBB.IntervalProcess.Subtract(d)

            Dim lblConfirmBy As Label = CType(e.Item.FindControl("lblConfirmBy"), Label)
            If objPQRHeaderBB.ConfirmBy = String.Empty Then
                lblConfirmBy.Text = ""
            Else
                lblConfirmBy.Text = CommonFunction.FormatSavedUser(objPQRHeaderBB.ConfirmBy, User)
            End If

            Dim lblTglRilis As Label = CType(e.Item.FindControl("lblTglRilis"), Label)
            If objPQRHeaderBB.RealeseTime < New DateTime(1970, 1, 1) Then
                lblTglRilis.Text = ""
            Else
                lblTglRilis.Text = objPQRHeaderBB.RealeseTime.ToString("dd/MM/yyyy")
            End If

            Dim lblTglValidasi As Label = CType(e.Item.FindControl("lblTglValidasi"), Label)
            If objPQRHeaderBB.ValidationTime < New DateTime(1970, 1, 1) Then
                lblTglValidasi.Text = ""
            Else
                lblTglValidasi.Text = objPQRHeaderBB.ValidationTime.ToString("dd/MM/yyyy")
            End If

            Dim lblInterval As Label = CType(e.Item.FindControl("lblInterval"), Label)
            'lblInterval.Text = ts.Hours.ToString & " Jam " & ts.Minutes.ToString & " Menit"

            lblInterval.Text = objPQRHeaderBB.Interval.Days.ToString & " Hari " & objPQRHeaderBB.Interval.Hours.ToString & " Jam " & objPQRHeaderBB.Interval.Minutes.ToString & " Menit"

            If objPQRHeaderBB.Interval.Days > 3 And objPQRHeaderBB.Interval.Days <= 7 And ((objPQRHeaderBB.RowStatus = CType(EnumPQR.PQRStatus.Proses, Short)) Or (objPQRHeaderBB.RowStatus = CType(EnumPQR.PQRStatus.Validasi, Short)) Or (objPQRHeaderBB.RowStatus = CType(EnumPQR.PQRStatus.Rilis, Short))) Then
                e.Item.BackColor = Color.Yellow
            End If
            If objPQRHeaderBB.Interval.Days > 7 And ((objPQRHeaderBB.RowStatus = CType(EnumPQR.PQRStatus.Proses, Short)) Or (objPQRHeaderBB.RowStatus = CType(EnumPQR.PQRStatus.Validasi, Short)) Or (objPQRHeaderBB.RowStatus = CType(EnumPQR.PQRStatus.Rilis, Short))) Then
                e.Item.BackColor = Color.Red
            End If

            TotalInterval = TotalInterval + objPQRHeaderBB.Interval.Ticks
        End If


    End Sub
    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim nChecked As Integer = 0
        Dim arlPQRToDownload As New ArrayList

        _arrPQRHeaderBB = sessHelper.GetSession("PQRDafterKondisi")

        For Each di As DataGridItem In dgListPQR.Items
            Dim chk As CheckBox = CType(di.FindControl("chkSelection"), CheckBox)
            If chk.Checked Then
                arlPQRToDownload.Add(CType(_arrPQRHeaderBB(di.ItemIndex), PQRHeaderBB))
                nChecked += 1
            End If
        Next
        If nChecked = 0 Then
            BindToGrid(Me.dgListPQR.CurrentPageIndex, True) ' update session for data download
        Else
            sessHelper.SetSession("PQRDafterKondisi", arlPQRToDownload)
        End If
        Response.Redirect("./FrmDownloadPQRListKondisiBB.aspx")
    End Sub

    Private Sub dgListPQR_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgListPQR.PageIndexChanged
        dgListPQR.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dgListPQR.CurrentPageIndex)
    End Sub

    Private Sub dgListPQR_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgListPQR.ItemCommand
        Select Case e.CommandName
            Case "ViewPQR"
                oPQRHeaderBB = oPQRHeaderBBFacade.Retrieve(CType(e.CommandArgument, Integer))
                sessHelper.SetSession("PQR", oPQRHeaderBB)
                SetSessionCriteria()
                Server.Transfer("~/PQR/FrmPQRHeaderBB.aspx?Mode=View&Src=PQRListKondisi")
        End Select

    End Sub

    Private Sub SetSessionCriteria()
        Dim arrLastState As ArrayList = New ArrayList
        arrLastState.Add(txtKodeDealer.Text)
        arrLastState.Add(txtPQRNo.Text)
        arrLastState.Add(icTglValidateDari.Value)
        arrLastState.Add(icTglValidateSampai.Value)
        arrLastState.Add(txtSubject.Text)

        arrLastState.Add(txtNoChasis.Text)
        arrLastState.Add(ddlKategori.SelectedIndex)
        arrLastState.Add(ddlStatus.SelectedIndex)
        arrLastState.Add(dgListPQR.CurrentPageIndex)
        arrLastState.Add(CType(ViewState("currSortColumn"), String))
        arrLastState.Add(CType(ViewState("currSortDirection"), Sort.SortDirection))
        arrLastState.Add(txtProsesOleh.Text)
        arrLastState.Add(txtKodeKerusakan.Text)
        arrLastState.Add(cbDate.Checked)

        sessHelper.SetSession("PQRKONDISISESSIONLASTSTATE", arrLastState)
    End Sub
    Private Sub GetSessionCriteria()
        Dim arrLastState As ArrayList = sessHelper.GetSession("PQRKONDISISESSIONLASTSTATE")
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
            icTglValidateDari.Value = arrLastState.Item(2)
            icTglValidateSampai.Value = arrLastState.Item(3)
            txtSubject.Text = arrLastState.Item(4)
            txtNoChasis.Text = arrLastState.Item(5)
            ddlKategori.SelectedIndex = arrLastState.Item(6)
            ddlStatus.SelectedIndex = arrLastState.Item(7)
            cbDate.Checked = arrLastState.Item(13)
            dgListPQR.CurrentPageIndex = arrLastState.Item(8)
            ViewState("currSortColumn") = arrLastState.Item(9)
            ViewState("currSortDirection") = arrLastState.Item(10)
            txtProsesOleh.Text = arrLastState.Item(11)
            txtKodeKerusakan.Text = arrLastState.Item(12)
        Else
            ViewState("currSortColumn") = "Status"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            dgListPQR.CurrentPageIndex = 0

        End If
    End Sub

End Class
