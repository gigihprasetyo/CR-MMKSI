
#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.AfterSales
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports MMKSI.DNetUpload.Utility
Imports System.Linq
Imports KTB.DNet.BusinessFacade.Service


#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

Public Class FrmDaftarUpload
    Inherits System.Web.UI.Page
    Dim sHRole As SessionHelper = New SessionHelper

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents rfvKode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgDaftarUpload As System.Web.UI.WebControls.DataGrid

    Protected WithEvents ddlTipePelaporan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnDownloadTemplate As System.Web.UI.WebControls.Button
    Protected WithEvents periodeFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents periodeTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnValidasi As System.Web.UI.WebControls.Button
    Protected WithEvents btnTolakValidasi As System.Web.UI.WebControls.Button
    Protected WithEvents btnKonfirmasi As System.Web.UI.WebControls.Button
    Protected WithEvents btnTolakKonfirmasi As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        isAuthDelete = GetDeletePrivilege()
    End Sub

#End Region

#Region "Declaration"
    Private m_bKonfirmasiFormPrivilege As Boolean = False
    Private m_bValidasiFormPrivilege As Boolean = False
    Private ListModule As ArrayList
    Private isAuthDelete As Boolean
#End Region

#Region "Event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            If GetSessionCriteria() Then
                BindDataGrid(dtgDaftarUpload.CurrentPageIndex)
            End If
        End If
    End Sub

    Private Sub SetSessionCriteria()
        Dim objDaftarUpload As ArrayList = New ArrayList
        objDaftarUpload.Add(txtKodeDealer.Text)
        objDaftarUpload.Add(periodeFrom.Value)
        objDaftarUpload.Add(periodeTo.Value)
        objDaftarUpload.Add(ddlTipePelaporan.SelectedIndex)
        objDaftarUpload.Add(ddlStatus.SelectedIndex)
        objDaftarUpload.Add(ddlMonth.SelectedIndex)
        objDaftarUpload.Add(ddlYear.SelectedIndex)
        sHRole.SetSession("SESSIONDAFTARUPLOAD", objDaftarUpload)
    End Sub

    Private Function GetSessionCriteria() As Boolean
        Dim objDaftarUpload As ArrayList = sHRole.GetSession("SESSIONDAFTARUPLOAD")
        If Not objDaftarUpload Is Nothing Then
            txtKodeDealer.Text = objDaftarUpload.Item(0)
            periodeFrom.Value = objDaftarUpload.Item(1)
            periodeTo.Value = objDaftarUpload.Item(2)
            ddlTipePelaporan.SelectedIndex = objDaftarUpload.Item(3)
            ddlStatus.SelectedIndex = objDaftarUpload.Item(4)
            ddlMonth.SelectedIndex = objDaftarUpload.Item(5)
            ddlYear.SelectedIndex = objDaftarUpload.Item(6)
            Return True
        End If
        Return False
    End Function

    Private Sub BindTipePelaporan()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistModule), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistModule), "Status", MatchType.Exact, 1))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(AssistModule), "Name", ViewState("CurrentSortDirect")))

        Dim moduleColl As ArrayList = New AssistModuleFacade(User).Retrieve(criterias, sortColl)

        Dim item As ListItem
        item = New ListItem("Silahkan pilih", 0)
        ddlTipePelaporan.Items.Add(item)
        If moduleColl.Count > 0 Then
            For Each cat As AssistModule In moduleColl
                item = New ListItem(cat.Name, cat.ID)
                ddlTipePelaporan.Items.Add(item)
            Next
        End If

    End Sub

    Private Sub BindMonth()
        '
        Dim al As ArrayList = enumMonthGet.RetriveMonth()
        ddlMonth.DataSource = enumMonthGet.RetriveMonth()
        ddlMonth.DataValueField = "ValStatus"
        ddlMonth.DataTextField = "NameStatus"
        ddlMonth.DataBind()

        ddlMonth.SelectedValue = DateTime.Now.Month - 1
    End Sub

    Private Sub BindYear()

        Dim a As Integer
        Dim now As DateTime = DateTime.Now
        For a = -1 To 1
            ddlYear.Items.Insert(0, New ListItem(now.AddYears(a).Year, now.AddYears(a).Year))
        Next
        ddlYear.SelectedValue = now.Year
    End Sub

    Private Sub BindStatus()
        Dim objUserInfo As UserInfo = sHRole.GetSession("LOGINUSERINFO")
        Dim listStatus As New EnumAssistStatusUpload
        Dim al As ArrayList = listStatus.RetrieveStatusType
        For Each item As enumassistupload In al
            If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                'Dealer only show status minimun "menunggu validasi"
                If item.ValStatus >= EnumAssistStatusUpload.StatusUpload.MenungguValidasi Then
                    ddlStatus.Items.Insert(0, New ListItem(item.NameStatus, item.ValStatus))
                End If
            Else
                'MMKSI only show status minimun "menunggu konfirmasi" and "menunggu validasi"
                If item.ValStatus >= EnumAssistStatusUpload.StatusUpload.MenungguKonfirmasi Then
                    ddlStatus.Items.Insert(0, New ListItem(item.NameStatus, item.ValStatus))
                End If
                If item.ValStatus = EnumAssistStatusUpload.StatusUpload.MenungguValidasi Then
                    ddlStatus.Items.Insert(0, New ListItem(item.NameStatus, item.ValStatus))
                End If
            End If
        Next
        ddlStatus.Items.Insert(0, New ListItem("Pilih Status", "-1"))

    End Sub


    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        SearchModuleByCriteria()
    End Sub

    Private Sub btnDownloadTemplate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownloadTemplate.Click
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistModule), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistModule), "ID", MatchType.Exact, ddlTipePelaporan.SelectedValue))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(AssistModule), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))
        Dim moduleColl As ArrayList = New AssistModuleFacade(User).Retrieve(criterias, sortColl)

        If moduleColl.Count > 0 Then
            Dim modules As AssistModule
            modules = moduleColl(0)
            Dim fullPath As String = Server.MapPath("~/TemplateFile/" + modules.TemplateFileName)
            If System.IO.File.Exists(fullPath) Then
                Response.Redirect("~/TemplateFile/" + modules.TemplateFileName)
            Else
                MessageBox.Show("File template tidak ditemukan")
            End If

        Else
            MessageBox.Show("File template tidak ditemukan")
        End If
    End Sub

    Private Sub dtgDaftarUpload_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDaftarUpload.SortCommand
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

        dtgDaftarUpload.SelectedIndex = -1
        dtgDaftarUpload.CurrentPageIndex = 0
        BindDataGrid(dtgDaftarUpload.CurrentPageIndex)
    End Sub


    Private Sub dtgDaftarUpload_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDaftarUpload.PageIndexChanged
        dtgDaftarUpload.SelectedIndex = -1
        dtgDaftarUpload.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgDaftarUpload.CurrentPageIndex)
    End Sub

#End Region

#Region "Custom"
    Private Function GetDeletePrivilege()
        Dim bReturn As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.AfterSales_DaftarUpload_Hapus_Privilege) Then
            bReturn = False
        Else
            bReturn = True
        End If
        Return bReturn
    End Function

    Private Sub SetControlPrivilege()
        'btnUpload.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bValidasiFormPrivilege = SecurityProvider.Authorize(Context.User, SR.AfterSales_DaftarUpload_Validasi_Privilege)
        m_bKonfirmasiFormPrivilege = SecurityProvider.Authorize(Context.User, SR.AfterSales_DaftarUpload_Konfirmasi_Privilege)

        btnValidasi.Visible = m_bValidasiFormPrivilege
        btnTolakValidasi.Visible = m_bValidasiFormPrivilege
        btnKonfirmasi.Visible = m_bKonfirmasiFormPrivilege
        btnTolakKonfirmasi.Visible = m_bKonfirmasiFormPrivilege

        If Not SecurityProvider.Authorize(Context.User, SR.AfterSales_DaftarUpload_View_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Data Upload - Daftar Upload")
        End If

    End Sub

    Private Sub InitiatePage()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        txtKodeDealer.Attributes.Add("onkeypress", "return alphaNumericExcept(event,'<>?*%$')")
        If Not sHRole.GetSession("DEALER") Is Nothing Then
            Dim arDeal As ArrayList = New ArrayList
            Dim objDeal As Dealer
            objDeal = CType(sHRole.GetSession("DEALER"), Dealer)
            If Not objDeal Is Nothing Then
                Dim objUserInfo As UserInfo = sHRole.GetSession("LOGINUSERINFO")
                lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
                If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    txtKodeDealer.Attributes.Add("readonly", "readonly")
                    txtKodeDealer.Text = objDeal.DealerCode
                    txtKodeDealer.BorderStyle = BorderStyle.None
                    lblSearchDealer.Visible = False
                Else
                    txtKodeDealer.ReadOnly = False
                End If
            End If
        End If
        BindTipePelaporan()
        BindStatus()
        BindMonth()
        BindYear()
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totRow As Integer = 0
        If (indexPage >= 0) Then

            Dim objUserInfo As UserInfo = sHRole.GetSession("LOGINUSERINFO")

            Dim TanggalAwal As New DateTime(CInt(periodeFrom.Value.Year), CInt(periodeFrom.Value.Month), CInt(periodeFrom.Value.Day), 0, 0, 0)
            Dim TanggalAkhir As New DateTime(CInt(periodeTo.Value.Year), CInt(periodeTo.Value.Month), CInt(periodeTo.Value.Day), 0, 0, 0)

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistUploadLog), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistUploadLog), "UploadTime", MatchType.GreaterOrEqual, Format(TanggalAwal, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistUploadLog), "UploadTime", MatchType.Lesser, Format(TanggalAkhir.AddDays(1), "yyyy-MM-dd HH:mm:ss")))

            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistUploadLog), "Month", MatchType.Exact, ddlMonth.SelectedValue.Trim))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistUploadLog), "Year", MatchType.Exact, ddlYear.SelectedValue.Trim))

            If txtKodeDealer.Text.Trim() <> "" Then
                Dim strKodeDealerIn As String = "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"
                criterias.opAnd(New Criteria(GetType(AssistUploadLog), "Dealer.DealerCode", MatchType.InSet, strKodeDealerIn))
            End If

            If ddlTipePelaporan.SelectedValue > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistUploadLog), "AssistModule.ID", MatchType.Exact, ddlTipePelaporan.SelectedValue))
            End If

            If ddlStatus.SelectedValue > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistUploadLog), "ValidateStatus", MatchType.Exact, ddlStatus.SelectedValue))
            Else
                If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistUploadLog), "ValidateStatus", MatchType.GreaterOrEqual, Convert.ToInt32(EnumAssistStatusUpload.StatusUpload.MenungguValidasi)))
                Else
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistUploadLog), "ValidateStatus", MatchType.No, Convert.ToInt32(EnumAssistStatusUpload.StatusUpload.GagalValidasySystem)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistUploadLog), "ValidateStatus", MatchType.No, Convert.ToInt32(EnumAssistStatusUpload.StatusUpload.TolakValidasi)))
                End If
            End If

            ListModule = New AssistUploadLogFacade(User).RetrieveActiveList(criterias, indexPage + 1, _
                    dtgDaftarUpload.PageSize, totRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            dtgDaftarUpload.DataSource = ListModule
            dtgDaftarUpload.VirtualItemCount = totRow
            dtgDaftarUpload.DataBind()
        End If
    End Sub

    Sub dtgDaftarUpload_ItemCommand(ByVal source As System.Object, ByVal e As DataGridCommandEventArgs)
        Select Case (e.CommandName)
            Case "Detail"
                sHRole.SetSession("PrevPage", Request.Url.ToString())
                SetSessionCriteria()
                If (e.Item.Cells(1).Text = Convert.ToInt32(EnumAssistModule.AssistModule.SVCIncomingReport)) Then
                    Response.Redirect("../AfterSales/FrmServiceIncoming.aspx?id=" & e.Item.Cells(0).Text & "&du=1")
                ElseIf (e.Item.Cells(1).Text = Convert.ToInt32(EnumAssistModule.AssistModule.SPartsSales)) Then
                    Response.Redirect("../AfterSales/FrmPartsSales.aspx?id=" & e.Item.Cells(0).Text)
                ElseIf (e.Item.Cells(1).Text = Convert.ToInt32(EnumAssistModule.AssistModule.SPartsStock)) Then
                    Response.Redirect("../AfterSales/FrmPartsStock.aspx?id=" & e.Item.Cells(0).Text & "&du=1")
                ElseIf (e.Item.Cells(1).Text = Convert.ToInt32(EnumAssistModule.AssistModule.DealerExpenseService)) Then
                    Response.Redirect("../AfterSales/FrmExpenseService.aspx?id=" & e.Item.Cells(0).Text)
                ElseIf (e.Item.Cells(1).Text = Convert.ToInt32(EnumAssistModule.AssistModule.DealerExpenseSpareParts)) Then
                    Response.Redirect("../AfterSales/FrmExpenseSpareparts.aspx?id=" & e.Item.Cells(0).Text)
                    'ElseIf (e.Item.Cells(1).Text = Convert.ToInt32(EnumAssistModule.AssistModule.SVCIncomingBPReport)) Then
                    '    Response.Redirect("../AfterSales/FrmServiceIncomingBP.aspx?id=" & e.Item.Cells(0).Text)
                Else
                    MessageBox.Show("Detail tidak ditemukan")
                End If
            Case "Delete"
                Try
                    DeleteAssistUpload(e.Item.Cells(0).Text)
                Catch ex As Exception
                    MessageBox.Show(SR.DeleteFail)
                End Try

                BindDataGrid(dtgDaftarUpload.CurrentPageIndex)
        End Select
    End Sub

    Private Sub DeleteAssistUpload(ByVal nID As Integer)
        Dim objAssistUploadLog As AssistUploadLog = New AssistUploadLogFacade(User).Retrieve(nID)
        If Not objAssistUploadLog Is Nothing Then
            Dim objAssistUploadLogFacade As AssistUploadLogFacade = New AssistUploadLogFacade(User)
            objAssistUploadLogFacade.DeleteFromDB(objAssistUploadLog)

            dtgDaftarUpload.CurrentPageIndex = 0
            BindDataGrid(dtgDaftarUpload.CurrentPageIndex)
        Else
            MessageBox.Show(SR.DeleteFail)
        End If
    End Sub

    Private Sub UpdateStatus(ByVal status As Int32)
        Try
            Dim strIDs As String = GetSelectedIDs(dtgDaftarUpload)
            If strIDs = ")" Then
                MessageBox.Show("Tidak ada data yang dipilih")
                Exit Sub
            End If

            If strIDs = "dealerstatusvalidation" Then
                MessageBox.Show("Hanya boleh validasi/batal untuk data yang statusnya 'Menunggu Validasi'")
                Exit Sub
            End If
            If strIDs = "mmksistatusvalidation" Then
                MessageBox.Show("Hanya boleh konfirmasi/batal untuk data yang statusnya 'Menunggu Konfirmasi'")
                Exit Sub
            End If

            If strIDs.Length > 0 Then
                UpdateStatusProcess(strIDs, status)
                GSRRilis(strIDs)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnValidasi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidasi.Click
        UpdateStatus(EnumAssistStatusUpload.StatusUpload.MenungguKonfirmasi)
    End Sub

    Private Sub btnBatalValidasi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTolakValidasi.Click
        UpdateStatus(EnumAssistStatusUpload.StatusUpload.TolakValidasi)
    End Sub

    Private Sub btnKonfirmasi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKonfirmasi.Click
        Try
            Dim strIDs As String = GetSelectedIDs(dtgDaftarUpload)
            If strIDs = ")" Then
                MessageBox.Show("Tidak ada data yang dipilih")
                Exit Sub
            End If

            If strIDs = "dealerstatusvalidation" Then
                MessageBox.Show("Hanya boleh validasi/batal untuk data yang statusnya 'Menunggu Validasi'")
                Exit Sub
            End If
            If strIDs = "mmksistatusvalidation" Then
                MessageBox.Show("Hanya boleh konfirmasi/batal untuk data yang statusnya 'Menunggu Konfirmasi'")
                Exit Sub
            End If

            If strIDs.Length > 0 Then
                Dim result As Boolean = New AssistUploadLogFacade(User).UpdateStatus(strIDs, EnumAssistStatusUpload.StatusUpload.Selesai)
                If result = True Then
                    MessageBox.Show("Update Status Berhasil")
                    BindDataGrid(dtgDaftarUpload.CurrentPageIndex)
                Else
                    MessageBox.Show(SR.SaveFail)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnBatalKonfirmasi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTolakKonfirmasi.Click
        UpdateStatus(EnumAssistStatusUpload.StatusUpload.TolakKonfirmasi)
    End Sub

    Private Sub UpdateStatusProcess(ByVal strIDs As String, ByVal status As Int32)
        Dim arlPFRH As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AssistUploadLog), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(AssistUploadLog), "ID", MatchType.InSet, strIDs))
        arlPFRH = New AssistUploadLogFacade(User).Retrieve(criterias)

        For Each item As AssistUploadLog In arlPFRH
            item.ValidateStatus = status
            Dim nResult = New AssistUploadLogFacade(User).Update(item)
        Next

        MessageBox.Show("Update Status Berhasil")
        BindDataGrid(dtgDaftarUpload.CurrentPageIndex)
    End Sub
    Private Sub GSRRilis(ByVal strID As String)
        Dim intIDSIU As Integer
        Dim strIDSIU As String = ""
        Dim arlPFRH As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AssistServiceIncoming), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(AssistServiceIncoming), "AssistUploadLog.ID", MatchType.Exact, strID))
        criterias.opAnd(New Criteria(GetType(AssistServiceIncoming), "WorkOrderCategoryCode", MatchType.InSet, "('FS01','FS02','PM')"))
        arlPFRH = New AssistServiceIncomingFacade(User).Retrieve(criterias)

        For Each item As AssistServiceIncoming In arlPFRH
            'item.ValidateStatus = status
            'If strIDSIU = "" Then
            '    strIDSIU = item.ID
            'Else
            '    strIDSIU = strIDSIU & "," & item.ID
            'End If
            intIDSIU = item.ID
            GSRRilisSIU(intIDSIU)
        Next
        'GSRRilisSIU(strIDSIU)
    End Sub

    Private Sub GSRRilisSIU(ByVal ID As Integer)
        Dim objServiceReminder As ServiceReminder = New ServiceReminder
        'objService = New ServiceStandardTimeFacade(User).Calculate(txtKodeDealer.Text.Trim(), "", ddlJenisKegiatan2.SelectedValue, ICPeriodFrom.Value)
        Dim RESULT As Integer = 0
        RESULT = New ServiceReminderFacade(User).GSRRilisSIU(ID)

    End Sub

    Private Function GetSelectedIDs(ByVal dg As DataGrid) As String
        Dim i As Integer = 0
        Dim strResult As String = "("
        Dim objUserInfo As UserInfo = sHRole.GetSession("LOGINUSERINFO")
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            For Each item As DataGridItem In dtgDaftarUpload.Items
                Dim chk As CheckBox = item.FindControl("cbCheck")
                If chk.Checked Then
                    Dim lblID As Label = CType(item.FindControl("lblID"), Label)
                    Dim lblStatusID As Label = CType(item.FindControl("lblStatusID"), Label)
                    If CInt(lblStatusID.Text) = EnumAssistStatusUpload.StatusUpload.MenungguValidasi Then
                        strResult = strResult & lblID.Text.ToString & ","
                    Else
                        Return "dealerstatusvalidation"

                    End If
                End If
            Next
        Else
            For Each item As DataGridItem In dtgDaftarUpload.Items
                Dim chk As CheckBox = item.FindControl("cbCheck")
                If chk.Checked Then
                    Dim lblID As Label = CType(item.FindControl("lblID"), Label)
                    Dim lblStatusID As Label = CType(item.FindControl("lblStatusID"), Label)
                    If CInt(lblStatusID.Text) = EnumAssistStatusUpload.StatusUpload.MenungguKonfirmasi Then
                        strResult = strResult & lblID.Text.ToString & ","
                    Else
                        Return "mmksistatusvalidation"
                    End If
                End If
            Next
        End If


        strResult = Left(strResult, strResult.Length - 1) & ")"
        Return strResult
    End Function

    Private Sub dtgDaftarUpload_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDaftarUpload.ItemDataBound
        Dim RowValue As AssistUploadLog = CType(e.Item.DataItem, AssistUploadLog)
        Dim linkbtnHapus As LinkButton = e.Item.FindControl("lbtnHapus")

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            linkbtnHapus.Visible = isAuthDelete
            linkbtnHapus.Attributes("onclick") = "return confirm('Apakah anda yakin akan menghapus data?');"

            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgDaftarUpload.CurrentPageIndex * dtgDaftarUpload.PageSize)
        End If

    End Sub
    Private Sub ViewModel(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objModule As AssistUploadLog = New AssistUploadLogFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsUploadReport", objModule)

    End Sub

    Private Sub SearchModuleByCriteria()
        dtgDaftarUpload.CurrentPageIndex = 0
        BindDataGrid(dtgDaftarUpload.CurrentPageIndex)
    End Sub
#End Region

End Class
