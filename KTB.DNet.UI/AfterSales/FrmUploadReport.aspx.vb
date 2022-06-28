
#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.AfterSales
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports MMKSI.DNetUpload.Utility
Imports KTB.DNet.BusinessFacade.General
Imports System.Collections.Generic
Imports System.Linq
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

Public Class FrmUploadReport
    Inherits System.Web.UI.Page
    Dim sHRole As SessionHelper = New SessionHelper

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents rfvKode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgUploadReport As System.Web.UI.WebControls.DataGrid

    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents ddlMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTipePelaporan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList

    Protected WithEvents hUploadURL As System.Web.UI.WebControls.HiddenField
    Protected WithEvents hUploadURLtemp As System.Web.UI.WebControls.HiddenField
    Protected WithEvents hDownloadURL As System.Web.UI.WebControls.HiddenField
    Protected WithEvents btnDownloadTemplate As System.Web.UI.WebControls.Button
    Protected WithEvents periodeFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents periodeTo As KTB.DNet.WebCC.IntiCalendar

    Protected WithEvents lblfile As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Declaration"
    Private m_bFormPrivilege As Boolean = False
    Private ListModule As ArrayList
    Private oDealerSystems As DealerSystems
    Private maxUploadRows As Integer = 0
#End Region

#Region "Event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        GetDealerSystems()
        If Not IsPostBack Then
            InitiatePage()
        End If
        SetLabelFileText()

    End Sub

    Private Sub SetLabelFileText()
        Dim _value As Integer = GetMaxRowAllowToUpload()
        Dim _stringToAppend As String = ""
        If Not _value = 0 Then
            _stringToAppend = String.Format("dan maksimal jumlah baris tidak lebih dari {0} baris", _value - 1)
        End If
        lblfile.Text = String.Format("*Pastikan file yang diupload berdasarkan dari template yang telah disediakan {0}", _stringToAppend)
    End Sub

    Private Function GetMaxRowAllowToUpload() As Integer
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "Name", MatchType.Exact, "AssistMaxRows"))
        Dim objAppConfig As AppConfig = New AppConfigFacade(User).Retrieve(criterias).Cast(Of AppConfig).ToList().FirstOrDefault()
        Return IIf(Not IsNothing(objAppConfig), objAppConfig.Value, 0)
    End Function

    Private Sub BindMonth()
        '
        Dim al As ArrayList = enumMonthGet.RetriveMonth()
        ddlMonth.DataSource = enumMonthGet.RetriveMonth()
        ddlMonth.DataValueField = "ValStatus"
        ddlMonth.DataTextField = "NameStatus"
        ddlMonth.DataBind()

    End Sub

    Private Sub BindYear()

        Dim a As Integer
        Dim now As DateTime = DateTime.Now
        For a = -1 To 1
            ddlYear.Items.Insert(0, New ListItem(now.AddYears(a).Year, now.AddYears(a).Year))
        Next
        ddlYear.SelectedValue = now.Year
    End Sub

    Private Sub dtgUploadReport_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgUploadReport.ItemCommand
        If e.CommandName = "Download" Then
            Dim objDeal As Dealer
            Dim type As Label = CType(e.Item.FindControl("lblTemplateFileName"), Label)
            objDeal = CType(sHRole.GetSession("DEALER"), Dealer)
            Dim url As String = KTB.DNet.Lib.WebConfig.GetValue("AssistDownloadByIDURL").ToString()
            Dim _key = InsertUpdateAssistKeyUpload()
            Dim enc As Encryptor = New Encryptor()
            Dim objUserInfo As UserInfo = sHRole.GetSession("LOGINUSERINFO")
            url = url.Replace("[DEALERCODE]", objDeal.DealerCode)
            url = url.Replace("[ASSISTMODULETYPE]", type.Text.Replace(".xlsx", ""))
            url = url.Replace("[FILEID]", e.Item.Cells(0).Text)
            url = url.Replace("[USERNAMELOGIN]", enc.Encrypt(objUserInfo.UserName, System.Text.Encoding.Unicode))
            url = url.Replace("[KEY]", _key)

            hDownloadURL.Value = url

            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(Page), "Script", "openWindowDownload();", True)
        End If
    End Sub

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
                'tambahan CR Body&Paint
                item = New ListItem(cat.Name & " | " & cat.TemplateFileName.Replace(".xlsx", "").Replace(".xls", ""), cat.ID)
                Dim objDeal As Dealer
                objDeal = CType(sHRole.GetSession("DEALER"), Dealer)
                If objDeal.OrganizationBranchType = 5 Then
                    ddlTipePelaporan.Items.Add(item)
                Else
                    If oDealerSystems.SystemID = 1 Then
                        ddlTipePelaporan.Items.Add(item)
                    Else
                        If cat.ID = 3 OrElse cat.ID = 5 Then
                            ddlTipePelaporan.Items.Add(item)
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub BindStatus()

        Dim listStatus As New EnumAssistStatusUpload
        Dim al As ArrayList = listStatus.RetrieveStatusType
        For Each item As enumassistupload In al
            ddlStatus.Items.Insert(0, New ListItem(item.NameStatus, item.ValStatus))
        Next
        ddlStatus.Items.Insert(0, New ListItem("Pilih Status", "-1"))

    End Sub


    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        SearchModuleByCriteria()
    End Sub

    Private Sub btnDownloadTemplate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownloadTemplate.Click
        If (ddlTipePelaporan.SelectedValue = 0) Then
            MessageBox.Show("Tipe pelaporan data belum dipilih")
        Else

            Dim words As String() = ddlTipePelaporan.SelectedItem.Text.Split(New Char() {"|"c})
            'Dim file As System.IO.FileInfo = New FileInfo(Server.MapPath("~/AssistTemplate/" & words(1).ToString().Trim() & ".xlsx"))
            Response.Redirect("../downloadlocal.aspx?file=AssistTemplate\" & words(1).ToString().Trim() & ".xlsx")
            'If file.Exists Then
            '    'Response.Clear()
            '    'Response.ClearHeaders() 'harus diberi clear headers, jika tidak di server tidak bisa download
            '    Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
            '    'Response.AddHeader("Content-Length", file.Length.ToString())
            '    'Response.ContentType = "Application/x-msexcel"
            '    Response.ContentType = "application/x-download"
            '    Response.WriteFile(file.FullName)
            '    Response.End()

            'Else
            '    Response.Write("This file does not exist.")
            'End If
        End If
    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Try
            If (ddlTipePelaporan.SelectedValue = 0) Then
                MessageBox.Show("Tipe pelaporan data belum dipilih")
            Else
                Dim objDeal As Dealer
                objDeal = CType(sHRole.GetSession("DEALER"), Dealer)
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistCutOffPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistCutOffPeriod), "Status", MatchType.Exact, 1))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistCutOffPeriod), "Month", MatchType.Exact, ddlMonth.SelectedValue.Trim))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistCutOffPeriod), "Year", MatchType.Exact, ddlYear.SelectedValue.Trim))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistCutOffPeriod), "Dealer.ID", MatchType.Exact, objDeal.ID))
                Dim sortColl As SortCollection = New SortCollection
                sortColl.Add(New Sort(GetType(AssistCutOffPeriod), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))
                Dim moduleColl As ArrayList = New AssistCutOffPeriodFacade(User).Retrieve(criterias, sortColl)
                If moduleColl.Count = 0 Then
                    Dim enc As Encryptor = New Encryptor()
                    Dim _key = InsertUpdateAssistKeyUpload()
                    Dim url As String = hUploadURL.Value
                    'Dim url As String = hUploadURL.Value
                    Dim words As String() = ddlTipePelaporan.SelectedItem.Text.Split(New Char() {"|"c})
                    url = url.Replace("[FILEFORMATNAME]", words(1).Trim())
                    url = url.Replace("[MONTH]", ddlMonth.SelectedValue)
                    url = url.Replace("[YEAR]", ddlYear.SelectedValue)
                    url = url.Replace("[MODULEID]", ddlTipePelaporan.SelectedValue)
                    url = url.Replace("[KEY]", _key)
                    hUploadURLtemp.Value = url
                    System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(Page), "Script", "openWindowUpload();", True)
                Else
                    MessageBox.Show("Periode sudah closing")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Function InsertUpdateAssistKeyUpload() As String
        Dim objUserInfo As UserInfo = sHRole.GetSession("LOGINUSERINFO")
        Dim enc As Encryptor = New Encryptor()
        Dim nResult As Integer
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistKeyUpload), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistKeyUpload), "UserName", MatchType.Exact, objUserInfo.UserName))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(AssistKeyUpload), "ID", ViewState("CurrentSortDirect")))
        Dim moduleColl As ArrayList = New AssistKeyUploadFacade(User).Retrieve(criterias, sortColl)
        Dim _key As String = enc.Encrypt(DateTime.Now.ToString("yyyy-MM-dd hh"), System.Text.Encoding.Unicode)

        If (moduleColl.Count > 0) Then
            'Update Key
            Dim assistkey As AssistKeyUpload = moduleColl(0)
            If (assistkey.Key <> _key) Then
                assistkey.Key = _key
                nResult = New AssistKeyUploadFacade(User).Update(assistkey)
            End If
        Else
            'Insert
            Dim objKeyFacade As AssistKeyUploadFacade = New AssistKeyUploadFacade(User)
            Dim objKey As AssistKeyUpload = New AssistKeyUpload

            objKey.UserName = objUserInfo.UserName
            objKey.Key = _key
            nResult = New AssistKeyUploadFacade(User).Insert(objKey)
        End If
        Return _key
    End Function

    Private Sub dtgUploadReport_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgUploadReport.SortCommand
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

        dtgUploadReport.SelectedIndex = -1
        dtgUploadReport.CurrentPageIndex = 0
        BindDataGrid(dtgUploadReport.CurrentPageIndex)
    End Sub


    Private Sub dtgUploadReport_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgUploadReport.PageIndexChanged
        dtgUploadReport.SelectedIndex = -1
        dtgUploadReport.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgUploadReport.CurrentPageIndex)
    End Sub

#End Region

#Region "Custom"

    Private Sub SetControlPrivilege()
        btnUpload.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.AfterSales_UploadReport_Process_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.AfterSales_UploadReport_View_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Transaksi - Upload Report")
        End If

    End Sub

    Private Sub InitiatePage()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

        If Not sHRole.GetSession("DEALER") Is Nothing Then
            Dim arDeal As ArrayList = New ArrayList
            Dim objDeal As Dealer
            objDeal = CType(sHRole.GetSession("DEALER"), Dealer)
            If Not objDeal Is Nothing Then
                lblDealerCode.Text = objDeal.DealerCode & " / " & objDeal.SearchTerm1
                lblDealerName.Text = objDeal.DealerName

                Dim url As String = KTB.DNet.Lib.WebConfig.GetValue("AssistUploadURL").ToString()
                Dim enc As Encryptor = New Encryptor()
                Dim objUserInfo As UserInfo = sHRole.GetSession("LOGINUSERINFO")
                url = url.Replace("[DEALERID]", objDeal.ID)
                url = url.Replace("[DEALERCODE]", objDeal.DealerCode)
                url = url.Replace("[USERNAME]", User.Identity.Name)
                url = url.Replace("[USERNAMELOGIN]", enc.Encrypt(objUserInfo.UserName, System.Text.Encoding.Unicode))
                hUploadURL.Value = url
            End If
        End If

        BindMonth()
        BindYear()
        BindTipePelaporan()
        BindStatus()
    End Sub

    Private Sub GetDealerSystems()
        '--anh
        Dim oDealerSystemsList As ArrayList = New ArrayList
        oDealerSystems = New DealerSystems

        Dim objDeal As Dealer
        objDeal = CType(sHRole.GetSession("DEALER"), Dealer)
        If Not objDeal Is Nothing Then
            If objDeal.Title = EnumDealerTittle.DealerTittle.DEALER Then
                Dim criteriads As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerSystems), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriads.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerSystems), "Dealer.ID", MatchType.Exact, objDeal.ID))

                oDealerSystemsList = New KTB.DNet.BusinessFacade.General.DealerSystemsFacade(User).Retrieve(criteriads)
                If oDealerSystemsList.Count > 0 Then
                    oDealerSystems = CType(oDealerSystemsList(0), DealerSystems)
                End If

            End If
        End If

        '--anh

    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totRow As Integer = 0
        If (indexPage >= 0) Then
            Dim objDeal As Dealer
            objDeal = CType(sHRole.GetSession("DEALER"), Dealer)

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistUploadLog), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim TanggalAwal As New DateTime(CInt(periodeFrom.Value.Year), CInt(periodeFrom.Value.Month), CInt(periodeFrom.Value.Day), 0, 0, 0)
            Dim TanggalAkhir As New DateTime(CInt(periodeTo.Value.Year), CInt(periodeTo.Value.Month), CInt(periodeTo.Value.Day), 0, 0, 0)

            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistUploadLog), "UploadTime", MatchType.GreaterOrEqual, Format(TanggalAwal, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistUploadLog), "UploadTime", MatchType.Lesser, Format(TanggalAkhir.AddDays(1), "yyyy-MM-dd HH:mm:ss")))

            If ddlTipePelaporan.SelectedValue > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistUploadLog), "AssistModule.ID", MatchType.Exact, ddlTipePelaporan.SelectedValue))
            Else
                If oDealerSystems.SystemID <> 1 Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistUploadLog), "AssistModule.ID", MatchType.InSet, ("(3,5)")))
                End If
            End If

            If ddlStatus.SelectedValue >= 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistUploadLog), "ValidateStatus", MatchType.Exact, ddlStatus.SelectedValue))
            End If
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistUploadLog), "Dealer.ID", MatchType.Exact, objDeal.ID))

            ListModule = New AssistUploadLogFacade(User).RetrieveActiveList(criterias, indexPage + 1, _
                    dtgUploadReport.PageSize, totRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            dtgUploadReport.DataSource = ListModule
            dtgUploadReport.VirtualItemCount = totRow
            dtgUploadReport.DataBind()
        End If
    End Sub

    Private Sub dtgUploadReport_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgUploadReport.ItemDataBound
        Dim RowValue As AssistUploadLog = CType(e.Item.DataItem, AssistUploadLog)

        If Not e.Item.FindControl("btnUpload") Is Nothing Then
            CType(e.Item.FindControl("btnUpload"), LinkButton).Visible = m_bFormPrivilege
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgUploadReport.CurrentPageIndex * dtgUploadReport.PageSize)
        End If

    End Sub
    Private Sub ViewModel(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objModule As AssistUploadLog = New AssistUploadLogFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsUploadReport", objModule)

    End Sub

    Private Sub SearchModuleByCriteria()
        dtgUploadReport.CurrentPageIndex = 0
        BindDataGrid(dtgUploadReport.CurrentPageIndex)
    End Sub
#End Region

End Class
