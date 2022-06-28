#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.AfterSales
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports MMKSI.DNetUpload.Utility

#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports KTB.DNet.BusinessFacade
Imports System.Text
#End Region



Public Class FrmServiceIncomingBP
    Inherits System.Web.UI.Page

#Region "Declaration"
    Private m_SaveFormPrivilege As Boolean = False
    Private ListModule As ArrayList
    Dim objAssistUploadLog As AssistUploadLog
    Private models As ArrayList

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            GetAssistUploadLog()

            tutupFrom.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
            bukaFrom.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
            If hAssistUploadLogID.Value = "" Then 'jika dibuka dari menu maka empty
                btnSave.Visible = False
                btnDownload.Visible = True
                btnDownload.Enabled = False
                btnBack.Visible = False
                lblKodeDealerDetail.Visible = False
                lblKodeDealerDetailSeparator.Visible = False
                lblDealerCode.Visible = False
                lblNamaDealerDetail.Visible = False
                lblNamaDealerDetailSeparator.Visible = False
                lblDealerName.Visible = False
                lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            Else
                'ddlStatus.Visible = False
                'lblStatusMenu.Visible = False
                'lblStatusSeparator.Visible = False
                btnDownload.Visible = False
                BindDataGrid(dtgServiceIncoming.CurrentPageIndex)
                LoadInformationHeader()
                txtKodeDealer.Visible = False
                lblSearchDealer.Visible = False
                lblKodeDealerMenu.Visible = False
                lblKodeDealerSeparator.Visible = False
            End If

        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        If txtKodeDealer.Text = "" And hAssistUploadLogID.Value = "" Then
            MessageBox.Show("Kode dealer harus diisi")
            Return
        End If

        SearchModuleByCriteria()
        btnDownload.Enabled = True
    End Sub


    Private Sub GetAssistUploadLog()
        Dim LogID As String = Request.QueryString("id")
        hAssistUploadLogID.Value = LogID
    End Sub

    Private Sub LoadInformationHeader()
        Dim criterias As CriteriaComposite = GetCriteria()
        Session("FrmServiceIncoming") = criterias
        Dim _query As String = criterias.ToString()
        hQuery.Value = _query

        Dim isFromDaftarUpload As Boolean = False
        If Not hAssistUploadLogID.Value = "" Then
            isFromDaftarUpload = True
        End If

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(AssistServiceIncoming), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))
        'Dim listAll As ArrayList = New AssistServiceIncomingFacade(User).Retrieve(criterias, sortColl)
        Dim listAll As ArrayList = New AssistServiceIncomingFacade(User).DownloadDataBySP(criterias, sortColl, isFromDaftarUpload)

        Dim siu As Int32 = 0
        For Each item As AssistServiceIncoming In listAll
            If (Not IsNothing(item.AssistWorkOrderCategory)) AndAlso _
                (item.AssistWorkOrderCategory.WorkOrderCategory = "GR" Or item.AssistWorkOrderCategory.WorkOrderCategory = "PM") Then
                siu = siu + 1
            End If
        Next
    End Sub

    Private Sub ActivateUserPrivilege()
        m_SaveFormPrivilege = SecurityProvider.Authorize(Context.User, SR.AfterSales_SvcIncoming_Ubah_Privilege)

        btnSave.Visible = m_SaveFormPrivilege

        If Not SecurityProvider.Authorize(Context.User, SR.AfterSales_DaftarUpload_View_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Data Upload - Service Incoming")
        End If

    End Sub

    Private Sub InitiatePage()
        'SetControlPrivilege()
        If Not IsNothing(Request.QueryString("du")) Then
            'chkTanggalBukaTransaksi.Checked = True
            'chkTanggalTutupTransaksi.Checked = True
        Else
            chkTanggalBukaTransaksi.Checked = True
            chkTanggalTutupTransaksi.Checked = True
        End If
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        If Not Session("DEALER") Is Nothing Then
            Dim arDeal As ArrayList = New ArrayList
            Dim objDeal As Dealer
            objDeal = CType(Session("DEALER"), Dealer)
            If Not objDeal Is Nothing Then
                lblDealerCode.Text = objDeal.DealerCode & " / " & objDeal.SearchTerm1
                lblDealerName.Text = objDeal.DealerName
                If objDeal.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    txtKodeDealer.Attributes.Add("readonly", "readonly")
                    txtKodeDealer.Text = objDeal.DealerCode
                    txtKodeDealer.BorderStyle = BorderStyle.None
                    lblSearchDealer.Visible = False
                    pnlMonitoring.Visible = False
                End If
                Dim objUserInfo As UserInfo = Session("LOGINUSERINFO")
            End If
        End If
        BindTempatPengerjaan()
        BindLayanan()
        BindWorkOrderCategory()
        BindWOStatus()
    End Sub

    Private Sub BindWorkOrderCategory()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistWorkOrderCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistWorkOrderCategory), "Status", MatchType.Exact, 1))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(AssistWorkOrderCategory), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))
        Dim Coll As ArrayList = New AssistWorkOrderCategoryFacade(User).Retrieve(criterias, sortColl)

        Dim item As ListItem
        item = New ListItem("Silahkan pilih", 0)
        ddlWorkOrderKategory.Items.Add(item)
        If Coll.Count > 0 Then
            For Each cat As AssistWorkOrderCategory In Coll
                item = New ListItem(cat.WorkOrderCategory & " | " & cat.Description, cat.ID)
                ddlWorkOrderKategory.Items.Add(item)
            Next
        End If
    End Sub

    Private Sub BindTempatPengerjaan()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistServicePlace), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServicePlace), "Status", MatchType.Exact, 1))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(AssistServicePlace), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))
        Dim Coll As ArrayList = New AssistServicePlaceFacade(User).Retrieve(criterias, sortColl)

        Dim item As ListItem
        item = New ListItem("Silahkan pilih", 0)
        ddlTempatPengerjaan.Items.Add(item)
        If Coll.Count > 0 Then
            For Each cat As AssistServicePlace In Coll
                item = New ListItem(cat.ServicePlaceCode & " | " & cat.Description, cat.ID)
                ddlTempatPengerjaan.Items.Add(item)
            Next
        End If
    End Sub

    Private Sub BindLayanan()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistServiceType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceType), "Status", MatchType.Exact, 1))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(AssistServiceType), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))
        Dim Coll As ArrayList = New AssistServiceTypeFacade(User).Retrieve(criterias, sortColl)

        Dim item As ListItem
        item = New ListItem("Silahkan pilih", 0)
        ddlLayanan.Items.Add(item)
        If Coll.Count > 0 Then
            For Each cat As AssistServiceType In Coll
                item = New ListItem(cat.Description, cat.ID)
                ddlLayanan.Items.Add(item)
            Next
        End If
    End Sub

    Private Sub BindWOStatus()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.StandardCode), "Category", MatchType.Exact, "WOStatus"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(StandardCode), "ValueId", Sort.SortDirection.ASC))
        Dim Coll As ArrayList = New StandardCodeFacade(User).Retrieve(criterias, sortColl)

        Dim item As ListItem
        item = New ListItem("Silahkan pilih", 0)
        ddlWOStatus.Items.Add(item)
        If Coll.Count > 0 Then
            For Each cat As StandardCode In Coll
                item = New ListItem(cat.ValueDesc, cat.ValueId)
                ddlWOStatus.Items.Add(item)
            Next
        End If
        If Not IsNothing(Request.QueryString("du")) Then
            ddlWOStatus.SelectedValue = 0
        Else
            ddlWOStatus.SelectedValue = 2
        End If
    End Sub

    Function GetCriteria() As CriteriaComposite
        Dim BukaAwal As New DateTime(CInt(bukaFrom.Value.Year), CInt(bukaFrom.Value.Month), CInt(bukaFrom.Value.Day), 0, 0, 0)
        Dim BukaAkhir As New DateTime(CInt(bukaTo.Value.Year), CInt(bukaTo.Value.Month), CInt(bukaTo.Value.Day), 0, 0, 0)

        Dim TutupAwal As New DateTime(CInt(tutupFrom.Value.Year), CInt(tutupFrom.Value.Month), CInt(tutupFrom.Value.Day), 0, 0, 0)
        Dim TutupAkhir As New DateTime(CInt(tutupTo.Value.Year), CInt(tutupTo.Value.Month), CInt(tutupTo.Value.Day), 0, 0, 0)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncomingBP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (chkTanggalBukaTransaksi.Checked) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncomingBP), "TglBukaTransaksi", MatchType.GreaterOrEqual, Format(BukaAwal, "yyyy-MM-dd")))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncomingBP), "TglBukaTransaksi", MatchType.Lesser, Format(BukaAkhir.AddDays(1), "yyyy-MM-dd")))
        End If

        If (chkTanggalTutupTransaksi.Checked) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncomingBP), "TglTutupTransaksi", MatchType.GreaterOrEqual, Format(TutupAwal, "yyyy-MM-dd")))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncomingBP), "TglTutupTransaksi", MatchType.Lesser, Format(TutupAkhir.AddDays(1), "yyyy-MM-dd")))
        End If

        If txtNoWorkOrder.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncomingBP), "NoWorkOrder", MatchType.Partial, txtNoWorkOrder.Text))
        End If

        If txtNoChassis.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncomingBP), "KodeChassis", MatchType.Partial, txtNoChassis.Text))
        End If

        If ddlWorkOrderKategory.SelectedValue > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncomingBP), "WorkOrderCategory.ID", MatchType.Exact, ddlWorkOrderKategory.SelectedValue))
        End If
        If ddlLayanan.SelectedValue > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncomingBP), "ServiceType.ID", MatchType.Exact, ddlLayanan.SelectedValue))
        End If

        If ddlWOStatus.SelectedValue > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncomingBP), "WOStatus", MatchType.Exact, ddlWOStatus.SelectedValue))
        End If

        If Not hAssistUploadLogID.Value = "" Then 'jika dibuka dari daftar upload
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncomingBP), "AssistUploadLog.ID", MatchType.Exact, hAssistUploadLogID.Value))
        Else
            If txtKodeDealer.Text.Trim() <> "" Then
                Dim strKodeDealerIn As String = "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"
                criterias.opAnd(New Criteria(GetType(AssistServiceIncomingBP), "Dealer.DealerCode", MatchType.InSet, strKodeDealerIn))
            End If

            'hanya yang sudah dikonfirmasi mmksi dan tidak double (sudah masuk ke BI)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncomingBP), "StatusAktif", MatchType.Exact, 1))

        End If

        Return criterias

    End Function

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totRow As Integer = 0
        If (indexPage >= 0) Then
            Dim criterias As CriteriaComposite

            If Not IsNothing(Session("FrmServiceIncoming")) Then
                criterias = Session("FrmServiceIncoming")
            Else
                criterias = GetCriteria()
            End If
            Dim isFromDaftarUpload As Boolean = False
            If Not hAssistUploadLogID.Value = "" Then
                isFromDaftarUpload = True
            End If
            'ListModule = New AssistServiceIncomingBPFacade(User).RetrieveActiveList(criterias, indexPage + 1, _
            '        dtgServiceIncoming.PageSize, totRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            ViewState("currSortTable") = GetType(AssistServiceIncomingBP)
            ListModule = New AssistServiceIncomingBPFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dtgServiceIncoming.PageSize,
                                                                                     totRow, CType(ViewState("CurrentSortColumn"), String),
                                                                                     CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            If Not IsNothing(ListModule) AndAlso ListModule.Count > 0 Then
                dtgServiceIncoming.DataSource = ListModule
                dtgServiceIncoming.VirtualItemCount = totRow
                dtgServiceIncoming.DataBind()
            Else
                dtgServiceIncoming.DataSource = New ArrayList
                dtgServiceIncoming.VirtualItemCount = 0
                dtgServiceIncoming.DataBind()
            End If


        End If
    End Sub

    Private Sub SearchModuleByCriteria()
        Session("FrmServiceIncoming") = GetCriteria()
        dtgServiceIncoming.CurrentPageIndex = 0
        BindDataGrid(dtgServiceIncoming.CurrentPageIndex)
    End Sub

    Protected Sub dtgServiceIncoming_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgServiceIncoming.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblTglBukaTransaksi As Label = CType(e.Item.FindControl("lblTglBukaTransaksi"), Label)
            Dim lblWaktuMasuk As Label = CType(e.Item.FindControl("lblWaktuMasuk"), Label)
            Dim lblTglTutupTransaksi As Label = CType(e.Item.FindControl("lblTglTutupTransaksi"), Label)
            Dim lblWaktuKeluar As Label = CType(e.Item.FindControl("lblWaktuKeluar"), Label)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            Dim lblKodeMekanik As Label = CType(e.Item.FindControl("lblKodeMekanik"), Label)
            Dim lblNoWorkOrder As Label = CType(e.Item.FindControl("lblNoWorkOrder"), Label)
            Dim lblNoChassis As Label = CType(e.Item.FindControl("lblNoChassis"), Label)
            Dim lblWorkOrderCategory As Label = CType(e.Item.FindControl("lblWorkOrderCategory"), Label)
            Dim lblKMService As Label = CType(e.Item.FindControl("lblKMService"), Label)
            Dim lblServiceTypeCode As Label = CType(e.Item.FindControl("lblServiceTypeCode"), Label)
            Dim lblTotalLC As Label = CType(e.Item.FindControl("lblTotalLC"), Label)
            Dim lblTotalSuborder As Label = CType(e.Item.FindControl("lblTotalSuborder"), Label)
            Dim lblTotalCat As Label = CType(e.Item.FindControl("lblTotalCat"), Label)

            Dim obj As AssistServiceIncomingBP = CType(e.Item.DataItem, AssistServiceIncomingBP)
            lblID.Text = obj.ID
            lblNo.Text = e.Item.ItemIndex + 1
            lblTglBukaTransaksi.Text = obj.TglBukaTransaksi.ToString("dd/MM/yyyy")
            lblWaktuMasuk.Text = obj.WaktuMasuk.ToString()
            lblTglTutupTransaksi.Text = obj.TglTutupTransaksi.ToString("dd/MM/yyyy")
            lblWaktuKeluar.Text = obj.WaktuKeluar.ToString()
            lblDealerCode.Text = obj.Dealer.DealerCode
            lblKodeMekanik.Text = obj.KodeMekanik
            lblNoWorkOrder.Text = obj.NoWorkOrder
            lblNoChassis.Text = obj.KodeChassis
            If Not IsNothing(obj.AssistWorkOrderCategory) Then
                lblWorkOrderCategory.Text = obj.AssistWorkOrderCategory.WorkOrderCategory
            End If
            lblKMService.Text = obj.KMService
            lblServiceTypeCode.Text = obj.ServiceTypeCode
            lblTotalLC.Text = CInt(obj.TotalLC)
            lblTotalSuborder.Text = CInt(obj.TotalSubOrder)
            lblTotalCat.Text = CInt(obj.TotalCat)
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If Not Session("PrevPage") Is Nothing AndAlso Not Session("PrevPage") = String.Empty Then
            Response.Redirect(Session("PrevPage").ToString())
        Else
            Response.Redirect("../login.aspx")
        End If
    End Sub

    Protected Sub dtgServiceIncoming_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgServiceIncoming.PageIndexChanged
        dtgServiceIncoming.SelectedIndex = -1
        dtgServiceIncoming.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgServiceIncoming.CurrentPageIndex)
    End Sub

    Protected Sub dtgServiceIncoming_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgServiceIncoming.SortCommand
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

        dtgServiceIncoming.SelectedIndex = -1
        dtgServiceIncoming.CurrentPageIndex = 0
        BindDataGrid(dtgServiceIncoming.CurrentPageIndex)
    End Sub
End Class