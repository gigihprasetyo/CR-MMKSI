#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.SPAF
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region

Public Class FrmDaftarStatusSPAF
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents calDari As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents calSampai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlAction As System.Web.UI.WebControls.DropDownList
    Protected WithEvents CustomValidator1 As System.Web.UI.WebControls.CustomValidator
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    'Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lboxStatus As System.Web.UI.WebControls.ListBox
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents lblQuantity As System.Web.UI.WebControls.Label
    Protected WithEvents ddlDocType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lboxTipe As System.Web.UI.WebControls.ListBox
    Protected WithEvents lblNoRangka As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalHarga As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalHargaValue As System.Web.UI.WebControls.Label
    Protected WithEvents dtgSPAF As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtNoRangka As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents cbPeriodePersetujuan As System.Web.UI.WebControls.CheckBox
    Protected WithEvents calDariSetuju As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents calSampaiSetuju As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents pnlUtama As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlUpload As System.Web.UI.WebControls.Panel
    Protected WithEvents fileUploadSPAFDoc As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents txtStatusTolak As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents pnlStatus As System.Web.UI.WebControls.Panel
    Protected WithEvents cbPeriodeKirim As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents btnGeneralDownload As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownloadDoc As System.Web.UI.WebControls.Button
    Protected WithEvents ddlDealer As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnProsesAll As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownloadAll As System.Web.UI.WebControls.Button
    Protected WithEvents btnProses As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownloadGeneralAll As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownloadDocAll As System.Web.UI.WebControls.Button

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
    Private _ListSPAF As ArrayList
    Private _ListSPAFDoc As ArrayList
    Private objDealer As Dealer
    Private sessionHelper As New sessionHelper
#End Region

#Region "Custom Method"

    Private Function GetSelectedItem(ByVal listboxStatus As ListBox, ByVal tipe As Integer) As String
        Dim _strStatus As String = String.Empty
        For Each item As ListItem In listboxStatus.Items
            If item.Selected Then
                If _strStatus = String.Empty Then
                    If tipe = 0 Then
                        _strStatus = item.Value
                    Else
                        _strStatus = "'" & item.Value & "'"
                    End If
                Else
                    If tipe = 0 Then
                        _strStatus = _strStatus & "," & item.Value
                    Else
                        _strStatus = _strStatus & "," & "'" & item.Value & "'"
                    End If
                End If
            End If
        Next
        Return _strStatus
    End Function

    Private Sub BindTodtgSPAF(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.V_LeasingDaftarDokumen), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        objDealer = Session("DEALER")

        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_LeasingDaftarDokumen), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        'If txtKodeDealer.Text <> String.Empty Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_LeasingDaftarDokumen), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        'End If

        If ddlDealer.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_LeasingDaftarDokumen), "Dealer.DealerCode", MatchType.InSet, "('" & ddlDealer.SelectedValue.Replace(";", "','") & "')"))

        End If

        If lboxStatus.SelectedIndex <> -1 Then
            Dim SelectedStatus As String = GetSelectedItem(lboxStatus, 0)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_LeasingDaftarDokumen), "Status", MatchType.InSet, "(" & SelectedStatus & ")"))
        End If

        Dim tglDari As DateTime
        Dim tglSampai As DateTime

        If cbPeriodeKirim.Checked = True Then
            tglDari = New DateTime(Me.calDari.Value.Year, Me.calDari.Value.Month, Me.calDari.Value.Day)
            tglSampai = New DateTime(Me.calSampai.Value.Year, Me.calSampai.Value.Month, Me.calSampai.Value.Day)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_LeasingDaftarDokumen), "PostingDate", MatchType.GreaterOrEqual, tglDari))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_LeasingDaftarDokumen), "PostingDate", MatchType.LesserOrEqual, tglSampai))
        End If

        Dim tglDariSetuju As DateTime
        Dim tglSampaiSetuju As DateTime
        If cbPeriodePersetujuan.Checked = True Then
            tglDariSetuju = New DateTime(Me.calDariSetuju.Value.Year, Me.calDariSetuju.Value.Month, Me.calDariSetuju.Value.Day, 0, 0, 0)
            tglSampaiSetuju = New DateTime(Me.calSampaiSetuju.Value.Year, Me.calSampaiSetuju.Value.Month, Me.calSampaiSetuju.Value.Day, 0, 0, 0).AddDays(1)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_LeasingDaftarDokumen), "TglSetuju", MatchType.GreaterOrEqual, tglDariSetuju))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_LeasingDaftarDokumen), "TglSetuju", MatchType.LesserOrEqual, tglSampaiSetuju))
        End If

        If ddlDocType.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_LeasingDaftarDokumen), "DocType", MatchType.Exact, ddlDocType.SelectedValue))
        End If

        If txtNoRangka.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_LeasingDaftarDokumen), "ChassisMaster.ChassisNumber", MatchType.Exact, txtNoRangka.Text.Trim.ToUpper))
        End If

        If lboxTipe.SelectedIndex <> -1 Then
            Dim SelectedStatus As String = GetSelectedItem(lboxTipe, 1)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_LeasingDaftarDokumen), "ChassisMaster.VechileColor.VechileType.VechileTypeCode", MatchType.InSet, "(" & SelectedStatus & ")"))
        End If

        If IsUserKtb Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_LeasingDaftarDokumen), "Status", MatchType.No, CType(EnumSPAFSubsidy.SPAFDocStatus.Baru, Int32)))
        End If

        _ListSPAF = New V_LeasingDaftarDokumenFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dtgSPAF.PageSize, _
                       total, CType(ViewState("CurrentSortColumn"), String), _
                       CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        _ListSPAFDoc = New V_LeasingDaftarDokumenFacade(User).Retrieve(criterias)

        dtgSPAF.DataSource = _ListSPAF
        dtgSPAF.VirtualItemCount = total
        'BindAll
        Dim _arrListSPAF As New ArrayList
        _arrListSPAF = New V_LeasingDaftarDokumenFacade(User).Retrieve(criterias)
        sessionHelper.SetSession("DAFTAR_DOC_SPAF", _arrListSPAF)
       


        Dim _ListSPAFALL As ArrayList = New V_LeasingDaftarDokumenFacade(User).RetrieveList(criterias, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        If _ListSPAF.Count > 0 Then
            dtgSPAF.DataBind()
            Session.Item("SPAFDOC") = _ListSPAFALL
        Else
            dtgSPAF.DataBind()
            Session.Item("SPAFDOC") = Nothing
            MessageBox.Show("Data Tidak Ditemukan")
        End If
    End Sub

    Private Sub TotalHarga()
        If Not IsNothing(_ListSPAFDoc) Then
            Dim tot As Double = 0
            Dim totQty As Integer = 0
            If ddlDocType.SelectedValue = EnumSPAFSubsidy.DocumentType.SPAF Then
                For Each item As V_LeasingDaftarDokumen In _ListSPAFDoc
                    tot += item.SPAF
                Next
            Else
                For Each item As V_LeasingDaftarDokumen In _ListSPAFDoc
                    tot += item.Subsidi
                Next
            End If

            lblTotalHargaValue.Text = FormatNumber(tot, 0, , , TriState.UseDefault)
            lblQuantity.Text = FormatNumber(_ListSPAFDoc.Count, 0, , , TriState.UseDefault) & " Unit"
        End If
    End Sub

    Private Sub ActivateUserPrivilege()
        'If Not SecurityProvider.Authorize(Context.User, SR.DaftarStatusPOView_Privilege) Then
        '    Response.Redirect("../frmAccessDenied.aspx?modulName=Proses Status PO")
        'End If

    End Sub

    Private Function IsFindValid() As Boolean
        Dim IsValid As Boolean = True
        Dim strMsg As New StringBuilder
        If ddlDocType.SelectedIndex = 0 Then
            IsValid = False
            strMsg.Append("Tipe dokumen harus dipilih")
        End If
        If strMsg.Length > 0 Then
            MessageBox.Show(strMsg.ToString)
        End If
        Return IsValid
    End Function
    Private ReadOnly Property IsUserKtb() As Boolean
        Get
            Dim objUserInfo As UserInfo = (New sessionHelper).GetSession("LOGINUSERINFO")
            Return Not IsNothing(objUserInfo.Dealer) AndAlso objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB
        End Get
    End Property
#End Region

#Region "EventHandler"

    Private Sub dtgSPAF_ItemDataBound1(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSPAF.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            Dim objSPAFDoc As V_LeasingDaftarDokumen
            objSPAFDoc = CType(e.Item.DataItem, V_LeasingDaftarDokumen)
            e.Item.Cells(2).Text = (e.Item.ItemIndex + 1 + (dtgSPAF.PageSize * dtgSPAF.CurrentPageIndex)).ToString
            'e.Item.Cells(3).Text = CType(objSPAFDoc.Status, EnumSPAFSubsidy.SPAFDocStatus).ToString

            'untuk tooltip status
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            lblStatus.Text = CType(objSPAFDoc.Status, EnumSPAFSubsidy.SPAFDocStatus).ToString
            If objSPAFDoc.Status = EnumSPAFSubsidy.SPAFDocStatus.Ditolak Then
                lblStatus.ToolTip = objSPAFDoc.AlasanPenolakan
            End If

            Dim lblKodeDealer As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)
            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(objSPAFDoc.OrderDealer)
            lblKodeDealer.Text = objDealer.SearchTerm1
            Dim lblLeasing As Label = CType(e.Item.FindControl("lblLeasing"), Label)
            lblLeasing.Text = objSPAFDoc.Dealer.SearchTerm1
            'Dim lblAF As Label = CType(e.Item.FindControl("lblAF"), Label)
            'Dim af As Decimal = objSPAFDoc.SPAF
            'lblAF.Text = FormatNumber(af, 0, , , TriState.False)


            Dim lblCM As Label = CType(e.Item.FindControl("lblCM"), Label)
            lblCM.Text = objSPAFDoc.ChassisMaster.ChassisNumber

            Dim lbtnUpload1 As LinkButton = CType(e.Item.FindControl("lbtnUpload1"), LinkButton)
            Dim lbtnUpload2 As LinkButton = CType(e.Item.FindControl("lbtnUpload2"), LinkButton)
            Dim lbtnDownload1 As LinkButton = CType(e.Item.FindControl("lbtnDownload1"), LinkButton)
            Dim lbtnDownload2 As LinkButton = CType(e.Item.FindControl("lbtnDownload2"), LinkButton)
            Dim lbtnDeleteFile1 As LinkButton = CType(e.Item.FindControl("lbtnDeleteFile1"), LinkButton)
            Dim lbtnDeleteFile2 As LinkButton = CType(e.Item.FindControl("lbtnDeleteFile2"), LinkButton)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim lbtnHistory As LinkButton = CType(e.Item.FindControl("lbtnHistory"), LinkButton)
            Dim strFile1 As String = String.Empty
            Dim strFile2 As String = String.Empty
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            If imp.Start() Then
                Dim dirInfor As New DirectoryInfo(String.Format("{0}{1}\{2}", _
                                KTB.DNet.Lib.WebConfig.GetValue("SAN"), _
                                KTB.DNet.Lib.WebConfig.GetValue("DaftarDokumenDestFileDirectory"), objSPAFDoc.ID))

                If dirInfor.Exists Then
                    For i As Integer = 0 To dirInfor.GetFiles.Length - 1
                        Dim fullPath As FileInfo = dirInfor.GetFiles.GetValue(i)
                        If i = 0 Then
                            strFile1 = fullPath.FullName
                        ElseIf i = 1 Then
                            strFile2 = fullPath.FullName
                        Else
                            Exit For
                        End If
                    Next
                End If

                imp.StopImpersonate()
                imp = Nothing
            End If

            lbtnHistory.Attributes.Add("onclick", String.Format("ShowPopUpHistorySPAF({0});return false;", objSPAFDoc.ID))
            lbtnUpload1.Attributes.Add("onclick", String.Format("ShowPopUpUploadDocument({0})", objSPAFDoc.ID))

            lbtnUpload1.Visible = strFile1 = String.Empty AndAlso (objSPAFDoc.Status = EnumSPAFSubsidy.SPAFDocStatus.Validasi Or objSPAFDoc.Status = EnumSPAFSubsidy.SPAFDocStatus.Baru) _
                AndAlso CheckBtnGridPriv()             'AndAlso objSPAFDoc.Status = EnumSPAFSubsidy.SPAFDocStatus.Validasi
            lbtnUpload2.Attributes.Add("onclick", String.Format("ShowPopUpUploadDocument({0})", objSPAFDoc.ID))
            lbtnUpload2.Visible = strFile2 = String.Empty AndAlso (objSPAFDoc.Status = EnumSPAFSubsidy.SPAFDocStatus.Validasi Or objSPAFDoc.Status = EnumSPAFSubsidy.SPAFDocStatus.Baru) _
                AndAlso CheckBtnGridPriv() 'AndAlso objSPAFDoc.Status = EnumSPAFSubsidy.SPAFDocStatus.Validasi

            lbtnDownload1.CommandArgument = String.Format("{0}\{1}\{2}", _
                KTB.DNet.Lib.WebConfig.GetValue("DaftarDokumenDestFileDirectory"), _
                objSPAFDoc.ID, Path.GetFileName(strFile1))
            lbtnDownload1.Visible = strFile1.Length > 0
            lbtnDownload2.CommandArgument = String.Format("{0}\{1}\{2}", _
                KTB.DNet.Lib.WebConfig.GetValue("DaftarDokumenDestFileDirectory"), _
                objSPAFDoc.ID, Path.GetFileName(strFile2))
            lbtnDownload2.Visible = strFile2.Length > 0
            lbtnDeleteFile1.CommandArgument = strFile1
            lbtnDeleteFile1.Attributes.Add("onclick", "return confirm('Yakin hapus file?');")
            lbtnDeleteFile1.Visible = strFile1.Length > 0 AndAlso CheckBtnGridPriv() AndAlso Not IsUserKtb
            lbtnDeleteFile2.CommandArgument = strFile2
            lbtnDeleteFile2.Attributes.Add("onclick", "return confirm('Yakin hapus file?');")
            lbtnDeleteFile2.Visible = strFile2.Length > 0 AndAlso CheckBtnGridPriv() AndAlso Not IsUserKtb
            lbtnDelete.Attributes.Add("onclick", String.Format("return confirm('{0}');", SR.DeleteConfirmation))
            lbtnDelete.CommandArgument = objSPAFDoc.ID
        End If
    End Sub

    Private Sub dtgSPAF_SortCommand1(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSPAF.SortCommand
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
        dtgSPAF.SelectedIndex = -1
        'dtgSPAF.CurrentPageIndex = 0
        BindTodtgSPAF(dtgSPAF.CurrentPageIndex)
    End Sub

    Private Sub dtgSPAF_PageIndexChanged1(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSPAF.PageIndexChanged
        dtgSPAF.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        InitiateAuthorization()
        If Not IsPostBack Then
            InitiatePage()
            'BindGrid()

            If CheckBtnDownloadPriv() = False Then
                btnDownload.Enabled = False
                btnDownloadAll.Enabled = False
            Else
                btnDownload.Enabled = True
                btnDownloadAll.Enabled = True
            End If
        End If
        'lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        btnProses.Attributes.Add("OnClick", "return confirm('Yakin mau melakukan proses?');")


        pnlUpload.Visible = False
    End Sub

    Sub dtgSPAF_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs)
        dtgSPAF.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Private Sub InitiatePage()
        StoreConditionKirim = ""
        StoreConditionPersetujuan = ""
        ViewState("CurrentSortColumn") = "ReffLetter"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        Dim arlDocType As ArrayList = New EnumSPAFSubsidy().RetrieveSPAFType

        Dim arlDocTypeWithPriv As New ArrayList
        For Each item As EnumSPAF In arlDocType
            If item.NameStatus = EnumSPAFSubsidy.DocumentType.SPAF.ToString Then
                If Not CheckOptionSPAFPriv() = False Then
                    arlDocTypeWithPriv.Add(item)
                End If
            ElseIf item.NameStatus = EnumSPAFSubsidy.DocumentType.Subsidi.ToString Then
                If Not CheckOptionSubsidiPriv() = False Then
                    arlDocTypeWithPriv.Add(item)
                End If
            End If
        Next
        ddlDocType.DataSource = arlDocTypeWithPriv
        ddlDocType.DataTextField = "NameStatus"
        ddlDocType.DataValueField = "ValStatus"
        ddlDocType.DataBind()
        ddlDocType.Items.Insert(0, New ListItem("Silahkan Pilih", -1))

        If IsUserKtb Then
            lboxStatus.Items.Clear()
            For Each stat As EnumSPAF In New EnumSPAFSubsidy().RetrieveSPAFDocStatus
                If stat.ValStatus <> EnumSPAFSubsidy.SPAFDocStatus.Baru Then
                    lboxStatus.Items.Add(New ListItem(stat.NameStatus, stat.ValStatus))
                End If
            Next
        Else
            lboxStatus.DataSource = New EnumSPAFSubsidy().RetrieveSPAFDocStatus
            lboxStatus.DataTextField = "NameStatus"
            lboxStatus.DataValueField = "ValStatus"
            lboxStatus.DataBind()
        End If

        Dim vcTypeFacade As VechileTypeFacade = New VechileTypeFacade(User)

        Dim crit_vcType As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit_vcType.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.Exact, "A"))

        Dim _sortColl As SortCollection = New SortCollection
        _sortColl.Add(New Sort(GetType(VechileType), "Description", Sort.SortDirection.ASC))

        Dim listVC As ArrayList = vcTypeFacade.Retrieve(crit_vcType, _sortColl)

        lboxTipe.DataSource = listVC
        lboxTipe.DataTextField = "Description"
        lboxTipe.DataValueField = "VechileTypeCode"
        lboxTipe.DataBind()

        BindActionStatusWithPriv()

        calDari.Value = Now
        calSampai.Value = Now
        calDariSetuju.Value = Now
        calSampaiSetuju.Value = Now
        Dim objUserInfo As UserInfo = (New sessionHelper).GetSession("LOGINUSERINFO")
        If Not IsNothing(objUserInfo.Dealer) AndAlso objUserInfo.Dealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            'txtKodeDealer.Text = objUserInfo.Dealer.DealerCode
            'txtKodeDealer.Enabled = False
            ddlDealer.SelectedValue = objUserInfo.Dealer.DealerCode
            btnDownloadDoc.Visible = False
        End If

        'Invisible Button Download All
        Me.btnDownloadAll.Visible = False
        Me.btnDownloadDocAll.Visible = False
        Me.btnDownloadGeneralAll.Visible = False

    End Sub

    Private Sub BindGrid()
        BindTodtgSPAF(dtgSPAF.CurrentPageIndex)
        TotalHarga()
    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        If IsFindValid() Then
            dtgSPAF.CurrentPageIndex = 0
            If ddlDocType.SelectedValue = EnumSPAFSubsidy.DocumentType.SPAF Then
                dtgSPAF.Columns(12).SortExpression = "SPAF"
                dtgSPAF.Columns(12).HeaderText = "SPAF per unit"
                dtgSPAF.Columns(14).HeaderText = "SPAF setelah PPh"
            Else
                dtgSPAF.Columns(12).SortExpression = "Subsidi"
                dtgSPAF.Columns(12).HeaderText = "Subsidi per unit"
                dtgSPAF.Columns(14).HeaderText = "Subsidi setelah PPh"
            End If
            If cbPeriodeKirim.Checked Then
                StoreConditionKirim = String.Format("{0} - {1}", calDari.Value.ToString("dd/MM/yyyy"), _
                    calSampai.Value.ToString("dd/MM/yyyy"))
            End If
            If cbPeriodePersetujuan.Checked Then
                StoreConditionPersetujuan = String.Format("{0} - {1}", calDariSetuju.Value.ToString("dd/MM/yyyy"), _
                    calSampaiSetuju.Value.ToString("dd/MM/yyyy"))
            End If
            StoreConditionType = ddlDocType.SelectedValue
            BindGrid()
        End If
    End Sub

    Private Sub btnProses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProses.Click
        If Me.ddlAction.SelectedIndex <> -1 Then
            If (ddlAction.SelectedValue = EnumSPAFSubsidy.Action.BatalProses) Then
                btnSave_Click(sender, e)

            Else

                Dim al As ArrayList
                al = PopulateSPAF(Me.ddlAction.SelectedValue)
                If al.Count > 0 Then
                    Dim objSpafFacade As SPAFFacade = New SPAFFacade(User)

                    Dim _statusUpdate As Integer = objSpafFacade.ProsesStatus(al)
                    'BindTodtgSPAF(dtgSPAF.CurrentPageIndex)
                    'refreshGrid
                    If _statusUpdate > 0 Then
                        MessageBox.Show("Update Status Sukses")
                    Else
                        MessageBox.Show("Update Status gagal")
                    End If

                    btnFind_Click(sender, e)
                Else
                    MessageBox.Show("Tidak ada Document yang dapat diproses " & Me.ddlAction.SelectedItem.Text)
                End If
            End If

        End If
    End Sub


    Private Function PopulateSPAFAll(ByVal type As Integer) As ArrayList
        Dim item As DataGridItem
        Dim coll As New ArrayList
        Dim _spaff As SPAFDoc
        Dim strMsg As String

        Dim spafFacade As spafFacade = New spafFacade(User)

        Dim _leasingDoc As New V_LeasingDaftarDokumen
        Dim _lstDoc As New ArrayList
        _lstDoc = CType(sessionHelper.GetSession("DAFTAR_DOC_SPAF"), ArrayList)
        For Each _leasingDoc In _lstDoc
            Dim id As Integer = _leasingDoc.ID

            _spaff = spafFacade.Retrieve(id)
            Select Case (type)
                Case 0 'dihapus
                    If _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Baru Then
                        _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Deleted
                        _spaff.TglSetuju = New Date(1900, 1, 1)
                        coll.Add(_spaff)
                    End If
                Case 1 'diValidasi
                    If _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Baru Then
                        _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Validasi
                        _spaff.TglSetuju = New Date(1900, 1, 1)
                        coll.Add(_spaff)
                    End If
                Case 2 'diProses
                    If _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Validasi Then
                        _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Proses
                        _spaff.TglSetuju = New Date(1900, 1, 1)
                        coll.Add(_spaff)
                    End If
                Case 3 'Batal validasi
                    If _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Validasi Then
                        _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Baru
                        _spaff.TglSetuju = New Date(1900, 1, 1)
                        coll.Add(_spaff)
                    End If
                Case 4 'batal Proses
                    If _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Proses Then
                        _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Validasi
                        _spaff.TglSetuju = New Date(1900, 1, 1)
                        coll.Add(_spaff)
                    End If
                Case 5 'Disetujui
                    If _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Proses Then
                        _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Disetujui
                        _spaff.TglSetuju = Now
                        _spaff = GetLatesSpaffPrice(_spaff)
                        coll.Add(_spaff)
                    End If
                Case 6 'ditolak
                    If _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Proses Then
                        _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Ditolak
                        coll.Add(_spaff)
                    End If
            End Select
            'End If
        Next
        Return coll
    End Function

    Private Function PopulateSPAF(ByVal type As Integer) As ArrayList
        Dim item As DataGridItem
        Dim coll As New ArrayList
        Dim _spaff As SPAFDoc
        Dim strMsg As String

        Dim spafFacade As spafFacade = New spafFacade(User)

        For Each item In Me.dtgSPAF.Items
            If CType(item.FindControl("chkSelect"), CheckBox).Checked Then
                Dim id As Integer = item.Cells(0).Text
                _spaff = spafFacade.Retrieve(id)
                Select Case (type)
                    Case 0 'dihapus
                        If _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Baru Then
                            _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Deleted
                            _spaff.TglSetuju = New Date(1900, 1, 1)
                            coll.Add(_spaff)
                        End If
                    Case 1 'diValidasi
                        If _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Baru Then
                            _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Validasi
                            _spaff.TglSetuju = New Date(1900, 1, 1)
                            coll.Add(_spaff)
                        End If
                    Case 2 'diProses
                        If _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Validasi Then
                            _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Proses
                            _spaff.TglSetuju = New Date(1900, 1, 1)
                            coll.Add(_spaff)
                        End If
                    Case 3 'Batal validasi
                        If _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Validasi Then
                            _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Baru
                            _spaff.TglSetuju = New Date(1900, 1, 1)
                            coll.Add(_spaff)
                        End If
                    Case 4 'batal Proses
                        If _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Proses Then
                            _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Validasi
                            _spaff.TglSetuju = New Date(1900, 1, 1)
                            coll.Add(_spaff)
                        End If
                    Case 5 'Disetujui
                        If _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Proses Then
                            _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Disetujui
                            _spaff.TglSetuju = Now
                            _spaff = GetLatesSpaffPrice(_spaff)
                            coll.Add(_spaff)
                        End If
                    Case 6 'ditolak
                        If _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Proses Then
                            _spaff.Status = EnumSPAFSubsidy.SPAFDocStatus.Ditolak
                            coll.Add(_spaff)
                        End If
                End Select
            End If
        Next
        Return coll
    End Function

    Private Function GetLatesSpaffPrice(ByVal objSpaf As SPAFDoc) As SPAFDoc
        Dim arrlCM As New ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ConditionMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ConditionMaster), "VechileType.ID", MatchType.Exact, objSpaf.ChassisMaster.VechileColor.VechileType.ID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ConditionMaster), "ValidFrom", MatchType.LesserOrEqual, objSpaf.DateLetter))
        Dim isSPAF As Boolean
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Search.Sort(GetType(ConditionMaster), "ValidFrom", Sort.SortDirection.DESC))

        arrlCM = New ConditionMasterFacade(User).Retrieve(criterias, sortColl)
        isSPAF = IIf(Convert.ToBoolean(Request.QueryString("isSPAF")) = True, True, False)
        If objSpaf.DocType = EnumSPAFSubsidy.DocumentType.SPAF Then
            isSPAF = True
        Else
            isSPAF = False
        End If
        If arrlCM.Count = 0 Then
            objSpaf.RetailPrice = 0
            objSpaf.SPAF = 0
            objSpaf.Subsidi = 0
        Else
            objSpaf.RetailPrice = CType(arrlCM(0), ConditionMaster).RetailPrice
            objSpaf.SPAF = IIf(isSPAF, (CType(arrlCM(0), ConditionMaster).SPAF / 100) * CType(arrlCM(0), ConditionMaster).RetailPrice, 0)
            objSpaf.Subsidi = IIf(isSPAF, 0, CType(arrlCM(0), ConditionMaster).Subsidi)
        End If
        Return objSpaf
    End Function

    Private Function SaveFile() As Integer
        Dim idUpload As Integer = CInt(viewstate("IDUpload"))
        Dim objSPAFDoc As SPAFDoc = New SPAFFacade(User).Retrieve(idUpload)
        Dim objUserInfo As UserInfo = sessionHelper.GetSession("LOGINUSERINFO")
        Dim objDealer As Dealer = objSPAFDoc.Dealer
        If objUserInfo.Dealer.ID <> objDealer.ID Then
            MessageBox.Show("Anda tidak memiliki hak untuk upload file")
            Return -1
        Else
            If fileUploadSPAFDoc.Value <> "" OrElse fileUploadSPAFDoc.Value <> String.Empty Then
                Dim _filename As String = System.IO.Path.GetFileName(fileUploadSPAFDoc.PostedFile.FileName)
                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("SPAFDoc") & "\" & _filename      '-- Destination file
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False

                Dim finfo As FileInfo
                Try
                    success = imp.Start()
                    If success Then
                        finfo = New FileInfo(DestFile)

                        If Not finfo.Directory.Exists Then
                            Directory.CreateDirectory(finfo.DirectoryName)
                        End If

                        Dim ext As String = System.IO.Path.GetExtension(fileUploadSPAFDoc.PostedFile.FileName)
                        If ext = ".exe" Then
                            MessageBox.Show("Tidak menerima format ekstensi EXE")
                            Return -1
                        Else
                            If finfo.Exists Then
                                finfo.Delete()
                            End If

                            fileUploadSPAFDoc.PostedFile.SaveAs(DestFile)
                            objSPAFDoc.UploadFile = _filename
                            objSPAFDoc.UploadBy = objUserInfo.Dealer.DealerCode
                            sessionHelper.SetSession("objSPAFDoc", objSPAFDoc)
                            Return 1
                        End If

                        imp.StopImpersonate()
                        imp = Nothing
                    End If
                Catch ex As Exception
                    Return -1
                End Try
            End If
        End If
    End Function

    Private Function GetIDChosen() As String
        Dim st As New StringBuilder("")
        For Each dt As DataGridItem In dtgSPAF.Items
            Dim chkSelect As CheckBox = dt.FindControl("chkSelect")
            If chkSelect.Checked Then
                If st.Length > 0 Then
                    st.Append(",")
                End If
                st.Append(dt.Cells(0).Text.Trim)
            End If
        Next
        Return st.ToString
    End Function

    Private Function GetIDChosenAll() As String
        Dim st As New StringBuilder("")
        Dim _leasingDoc As New V_LeasingDaftarDokumen
        Dim _lstDoc As New ArrayList
        _lstDoc = CType(sessionHelper.GetSession("DAFTAR_DOC_SPAF"), ArrayList)
        For Each _leasingDoc In _lstDoc
            If st.Length > 0 Then
                st.Append(",")
            End If
            st.Append(_leasingDoc.ID.ToString().Trim)
        Next
        'For Each dt As DataGridItem In dtgSPAF.Items
        '    'Dim chkSelect As CheckBox = dt.FindControl("chkSelect")
        '    'If chkSelect.Checked Then
        '    If st.Length > 0 Then
        '        st.Append(",")
        '    End If
        '    st.Append(dt.Cells(0).Text.Trim)
        '    'End If
        'Next
        Return st.ToString
    End Function

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        'Dim idin As String = GetIDChosen()
        'If idin.Length > 0 Then
        '    If StoreConditionPersetujuan.Length > 0 Then
        '        Response.Redirect(String.Format("FrmDaftarStatusSPAFDownload.aspx?DocType={0}&Periode={1}&idin={2}", _
        '            StoreConditionType, StoreConditionPersetujuan, idin))
        '    Else
        '        Response.Redirect(String.Format("FrmDaftarStatusSPAFDownload.aspx?DocType={0}&idin={1}", _
        '            StoreConditionType, idin))
        '    End If
        'Else
        '    MessageBox.Show(SR.DataNotChooseYet("Daftar Dokumen"))
        'End If
        Me.btnDownloadAll_Click(sender, e)
    End Sub

#End Region

#Region "Cek Privilege"
    Private Function CheckOptionSPAFPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.SPAFSaveUpload_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CheckOptionSubsidiPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.SPAFSaveSubsidiUpload_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.SPAFDocListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SPAF & SUBSIDI - Daftar Dokumen")
        End If
    End Sub

    Private Function CheckBtnGridPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.SPAFDocListStatusUploadBukti_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CheckBtnDownloadPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.SPAFDownloadDocStatusList_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub BindActionStatusWithPriv()
        Dim arlDocType As ArrayList = New EnumSPAFSubsidy().RetrieveAction
        'implement privilege
        Dim arlDocTypePriv As New ArrayList
        For Each item As EnumSPAF In arlDocType
            Select Case item.ValStatus
                Case EnumSPAFSubsidy.Action.Validasi
                    If SecurityProvider.Authorize(context.User, SR.SPAFDocListValidationStatus_Privilege) Then
                        arlDocTypePriv.Add(item)
                    End If
                Case EnumSPAFSubsidy.Action.BatalValidasi
                    If SecurityProvider.Authorize(context.User, SR.SPAFDocListValidationStatusCancel_Privilege) Then
                        arlDocTypePriv.Add(item)
                    End If
                Case EnumSPAFSubsidy.Action.Hapus
                    If SecurityProvider.Authorize(context.User, SR.SPAFDocListStatusDelete_Privilege) Then
                        arlDocTypePriv.Add(item)
                    End If
                Case EnumSPAFSubsidy.Action.Proses
                    If SecurityProvider.Authorize(context.User, SR.SPAFDocListStatusProcess_Privilege) Then
                        arlDocTypePriv.Add(item)
                    End If
                Case EnumSPAFSubsidy.Action.BatalProses
                    If SecurityProvider.Authorize(context.User, SR.SPAFDocListCancelProcess_Privilege) Then
                        arlDocTypePriv.Add(item)
                    End If
                Case EnumSPAFSubsidy.Action.Disetujui
                    If SecurityProvider.Authorize(context.User, SR.SPAFDocListApproved_Privilege) Then
                        arlDocTypePriv.Add(item)
                    End If
                    'Case EnumSPAFSubsidy.Action.Ditolak
                    '    If SecurityProvider.Authorize(context.User, SR.SPAFDocListReject_Privilege) Then
                    '        arlDocTypePriv.Add(item)
                    '    End If
            End Select
        Next
        ddlAction.DataSource = arlDocTypePriv
        ddlAction.DataTextField = "NameStatus"
        ddlAction.DataValueField = "ValStatus"
        ddlAction.DataBind()
        ddlAction.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
    End Sub
#End Region

    Private Sub dtgSPAF_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgSPAF.ItemCommand
        Dim lbtnUpload1 As LinkButton = CType(e.Item.FindControl("lbtnUpload1"), LinkButton)
        Dim lbtnDownload1 As LinkButton = CType(e.Item.FindControl("lbtnDownload1"), LinkButton)
        Dim lbtnUpload2 As LinkButton = CType(e.Item.FindControl("lbtnUpload2"), LinkButton)
        Dim lbtnDownload2 As LinkButton = CType(e.Item.FindControl("lbtnDownload2"), LinkButton)
        Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
        Select Case e.CommandName
            Case "Upload"
                BindTodtgSPAF(dtgSPAF.CurrentPageIndex)
            Case "Download"
                Dim file As New FileInfo(String.Format("{0}{1}", _
                    KTB.DNet.Lib.WebConfig.GetValue("SAN"), e.CommandArgument))
                Response.Redirect(String.Format("~/Download.aspx?file={0}", file.FullName))
            Case "Delete"
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                If imp.Start() Then
                    Dim spafFacade As New V_LeasingDaftarDokumenFacade(User)
                    spafFacade.Delete(spafFacade.Retrieve(e.CommandArgument))
                    Dim dirInfor As New DirectoryInfo(String.Format("{0}{1}\{2}", _
                        KTB.DNet.Lib.WebConfig.GetValue("SAN"), _
                        KTB.DNet.Lib.WebConfig.GetValue("DaftarDokumenDestFileDirectory"), e.CommandArgument))
                    If dirInfor.Exists Then
                        dirInfor.Delete()
                    End If
                    imp.StopImpersonate()
                    imp = Nothing
                    MessageBox.Show(SR.DeleteSucces)
                    BindTodtgSPAF(dtgSPAF.CurrentPageIndex)
                End If
            Case "DeleteFile"
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                If imp.Start() Then
                    Dim fileDoc As New FileInfo(e.CommandArgument)
                    fileDoc.Delete()
                    If fileDoc.Exists Then
                        MessageBox.Show("Delete file tidak berhasil")
                    Else
                        MessageBox.Show("Delete file berhasil")
                        BindTodtgSPAF(dtgSPAF.CurrentPageIndex)
                    End If
                    imp.StopImpersonate()
                    imp = Nothing
                End If
        End Select
    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        If SaveFile() <> -1 Then
            Dim objSPAF As SPAFDoc = sessionHelper.GetSession("objSPAFDoc")
            If (New SPAFFacade(User).Update(objSPAF) <> -1) Then
                MessageBox.Show("Upload file berhasil")
                pnlUpload.Visible = False
                pnlUtama.Visible = True
                BindTodtgSPAF(dtgSPAF.CurrentPageIndex)
            Else
                MessageBox.Show("Upload file gagal")
            End If
        End If
    End Sub

    Private Sub ddlAction_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlAction.SelectedIndexChanged
        If ddlAction.SelectedValue = EnumSPAFSubsidy.Action.Ditolak Then
            pnlStatus.Visible = True
            btnDownload.Enabled = False
            btnDownloadAll.Enabled = False
            btnFind.Enabled = False
            btnProses.Enabled = False
            btnProsesAll.Enabled = False
        ElseIf ddlAction.SelectedValue = EnumSPAFSubsidy.Action.BatalProses Then
            pnlStatus.Visible = True
            btnCancel.Visible = False
            btnSave.Visible = False
            btnDownload.Enabled = False
            btnDownloadAll.Enabled = False
            btnFind.Enabled = False
            btnProses.Enabled = True
            btnProsesAll.Enabled = False
        Else
            pnlStatus.Visible = False
            If CheckBtnDownloadPriv() Then
                btnDownload.Enabled = True
                btnDownloadAll.Enabled = True
            Else
                btnDownload.Enabled = False
                btnDownloadAll.Enabled = False
            End If
            btnFind.Enabled = True
            btnProses.Enabled = True
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        pnlStatus.Visible = False
        btnDownload.Enabled = True
        btnDownloadAll.Enabled = True
        btnFind.Enabled = True
        btnProses.Enabled = True
        ddlAction.SelectedIndex = 0
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim arlTolak As New ArrayList
        Dim objSPAFDoc As SPAFDoc
        For Each item As DataGridItem In dtgSPAF.Items
            Dim chkCek As CheckBox = CType(item.FindControl("chkSelect"), CheckBox)
            If chkCek.Checked Then
                Dim id As Integer = CInt(item.Cells(0).Text)
                objSPAFDoc = New SPAFFacade(User).Retrieve(id)
                If ddlAction.SelectedValue = EnumSPAFSubsidy.Action.Ditolak Then
                    If objSPAFDoc.Status = EnumSPAFSubsidy.SPAFDocStatus.Proses Then
                        objSPAFDoc.Status = EnumSPAFSubsidy.SPAFDocStatus.Ditolak
                        objSPAFDoc.AlasanPenolakan = txtStatusTolak.Text.Trim
                        arlTolak.Add(objSPAFDoc)
                    Else
                        arlTolak = New ArrayList
                        Exit For
                    End If
                ElseIf ddlAction.SelectedValue = EnumSPAFSubsidy.Action.BatalProses Then
                    If objSPAFDoc.Status = EnumSPAFSubsidy.SPAFDocStatus.Proses Then
                        objSPAFDoc.Status = EnumSPAFSubsidy.SPAFDocStatus.Validasi
                        objSPAFDoc.AlasanPenolakan = txtStatusTolak.Text.Trim
                        arlTolak.Add(objSPAFDoc)
                    Else
                        arlTolak = New ArrayList
                        Exit For
                    End If
                End If

            End If
        Next

        If arlTolak.Count > 0 Then
            If (New SPAFFacade(User).ProsesStatus(arlTolak) <> -1) Then
                BindTodtgSPAF(dtgSPAF.CurrentPageIndex)
                btnCancel_Click(Nothing, Nothing)
                MessageBox.Show("Update Status Sukses")
            Else
                MessageBox.Show("Gagal melakukan update status Batal Proses")
            End If
        Else
            MessageBox.Show("Gagal melakukan update status Batal Proses")
        End If
    End Sub

    Private Property StoreConditionKirim() As String
        Get
            Return ViewState("ConditionKirim")
        End Get
        Set(ByVal Value As String)
            ViewState("ConditionKirim") = Value
        End Set
    End Property

    Private Property StoreConditionType() As String
        Get
            Return ViewState("ConditionType")
        End Get
        Set(ByVal Value As String)
            ViewState("ConditionType") = Value
        End Set
    End Property

    Private Property StoreConditionPersetujuan() As String
        Get
            Return ViewState("ConditionPersetujuan")
        End Get
        Set(ByVal Value As String)
            ViewState("ConditionPersetujuan") = Value
        End Set
    End Property

    Private Sub btnGeneralDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGeneralDownload.Click
        'Dim idin As String = GetIDChosen()
        'If idin.Length > 0 Then
        '    If StoreConditionKirim.Length > 0 AndAlso StoreConditionPersetujuan.Length > 0 Then
        '        Response.Redirect(String.Format("FrmDaftarStatusSPAFGeneralDownload.aspx?DocType={0}&Kirim={1}&Periode={2}&idin={3}", _
        '            StoreConditionType, StoreConditionKirim, StoreConditionPersetujuan, idin))
        '    ElseIf cbPeriodeKirim.Checked Then
        '        Response.Redirect(String.Format("FrmDaftarStatusSPAFGeneralDownload.aspx?DocType={0}&Kirim={1}&idin={2}", _
        '            StoreConditionType, StoreConditionKirim, idin))
        '    ElseIf cbPeriodePersetujuan.Checked Then
        '        Response.Redirect(String.Format("FrmDaftarStatusSPAFGeneralDownload.aspx?DocType={0}&Periode={1}&idin={2}", _
        '            StoreConditionType, StoreConditionPersetujuan, idin))
        '    Else
        '        Response.Redirect(String.Format("FrmDaftarStatusSPAFGeneralDownload.aspx?DocType={0}&idin={1}", _
        '            StoreConditionType, idin))
        '    End If
        'Else
        '    MessageBox.Show(SR.DataNotChooseYet("Daftar Dokumen"))
        'End If
        Me.btnDownloadGeneralAll_Click(sender, e)
    End Sub

    Private Sub btnDownloadDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownloadDoc.Click
        'Dim idin As String = GetIDChosen()
        'If idin.Length > 0 Then
        '    Response.Redirect(String.Format("FrmDownloadDoc.aspx?DocType={0}&idin={1}", _
        '        StoreConditionType, idin))
        'Else
        '    MessageBox.Show(SR.DataNotChooseYet("Daftar Dokumen"))
        'End If
        Me.btnDownloadDocAll_Click(sender, e)
    End Sub

    Private Sub ddlDocType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlDocType.SelectedIndexChanged
        If (ddlDocType.SelectedValue = "0") Then
            lblTotalHarga.Text = "Total SPAF"
        Else
            lblTotalHarga.Text = "Total Subsidy"

        End If
    End Sub

   
    Private Sub btnDownloadAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownloadAll.Click
        Dim idin As String = GetIDChosenAll()
        If idin.Length > 0 Then
            If StoreConditionPersetujuan.Length > 0 Then
                Response.Redirect(String.Format("FrmDaftarStatusSPAFDownload.aspx?DocType={0}&Periode={1}&idin={2}", _
                    StoreConditionType, StoreConditionPersetujuan, idin))
            Else
                Response.Redirect(String.Format("FrmDaftarStatusSPAFDownload.aspx?DocType={0}&idin={1}", _
                    StoreConditionType, idin))
            End If
        Else
            MessageBox.Show(SR.DataNotChooseYet("Daftar Dokumen"))
        End If
    End Sub

    Private Sub btnProsesAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProsesAll.Click
        If Me.ddlAction.SelectedIndex <> -1 Then
            Dim al As ArrayList
            al = PopulateSPAFAll(Me.ddlAction.SelectedValue)
            If al.Count > 0 Then
                Dim objSpafFacade As SPAFFacade = New SPAFFacade(User)

                Dim _statusUpdate As Integer = objSpafFacade.ProsesStatus(al)
                'BindTodtgSPAF(dtgSPAF.CurrentPageIndex)
                'refreshGrid
                If _statusUpdate > 0 Then
                    MessageBox.Show("Update Status Sukses")
                Else
                    MessageBox.Show("Update Status gagal")
                End If

                btnFind_Click(sender, e)
            Else
                MessageBox.Show("Tidak ada Document yang dapat diproses " & Me.ddlAction.SelectedItem.Text)
            End If
        End If
    End Sub

    Private Sub btnDownloadDocAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownloadDocAll.Click
        Dim idin As String = GetIDChosenAll()
        If idin.Length > 0 Then
            Response.Redirect(String.Format("FrmDownloadDoc.aspx?DocType={0}&idin={1}", _
                StoreConditionType, idin))
        Else
            MessageBox.Show(SR.DataNotChooseYet("Daftar Dokumen"))
        End If
    End Sub

    Private Sub btnDownloadGeneralAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownloadGeneralAll.Click
        Dim idin As String = GetIDChosenAll()
        If idin.Length > 0 Then
            If StoreConditionKirim.Length > 0 AndAlso StoreConditionPersetujuan.Length > 0 Then
                Response.Redirect(String.Format("FrmDaftarStatusSPAFGeneralDownload.aspx?DocType={0}&Kirim={1}&Periode={2}&idin={3}", _
                    StoreConditionType, StoreConditionKirim, StoreConditionPersetujuan, idin))
            ElseIf cbPeriodeKirim.Checked Then
                Response.Redirect(String.Format("FrmDaftarStatusSPAFGeneralDownload.aspx?DocType={0}&Kirim={1}&idin={2}", _
                    StoreConditionType, StoreConditionKirim, idin))
            ElseIf cbPeriodePersetujuan.Checked Then
                Response.Redirect(String.Format("FrmDaftarStatusSPAFGeneralDownload.aspx?DocType={0}&Periode={1}&idin={2}", _
                    StoreConditionType, StoreConditionPersetujuan, idin))
            Else
                Response.Redirect(String.Format("FrmDaftarStatusSPAFGeneralDownload.aspx?DocType={0}&idin={1}", _
                    StoreConditionType, idin))
            End If
        Else
            MessageBox.Show(SR.DataNotChooseYet("Daftar Dokumen"))
        End If
    End Sub

  
End Class