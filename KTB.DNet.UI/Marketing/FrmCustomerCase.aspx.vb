Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports System.IO
Imports System.Text



Public Class FrmCustomerCase
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealerSearch As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents dgList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    'btnSearch

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variable"
    Private sessHelper As New SessionHelper
    Private bEditPriv As Boolean
    Private bViewPriv As Boolean
    Private bDownloadPriv As Boolean
#End Region


#Region "Custom Method"

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.Customer_Case_View_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Marketing - Case Management")
        End If
        bViewPriv = True 'SecurityProvider.Authorize(context.User, SR.KirimKendaraanListView_Privilege)
        bEditPriv = SecurityProvider.Authorize(Context.User, SR.Customer_Case_Edit_Privilege)
        bDownloadPriv = True 'SecurityProvider.Authorize(context.User, SR.KirimKendaraanListDownload_Privilege)
    End Sub

    Private Sub ClearForm()
        txtKodeDealer.Text = ""
        txtKonsumen.Text = ""
        txtNoMesin.Text = ""
        txtNoPolisi.Text = ""
        txtNoRangka.Text = ""
        ddlCategory.SelectedIndex = -1
        ddlCategory1.SelectedIndex = -1
        ddlCategory2.SelectedIndex = -1
        ddlCategory3.SelectedIndex = -1
        ddlCategory4.SelectedIndex = -1
        ddlStatus.SelectedIndex = -1
    End Sub
    Private Sub SetSessionCriteria()
        Dim arrLastState As ArrayList = New ArrayList
        arrLastState.Add(txtKodeDealer.Text)
        arrLastState.Add(txtKonsumen.Text)
        arrLastState.Add(txtNoMesin.Text)
        arrLastState.Add(txtNoPolisi.Text)
        arrLastState.Add(txtNoRangka.Text)
        arrLastState.Add(icStartDate.Value)
        arrLastState.Add(icEndDate.Value)
        arrLastState.Add(ddlCategory.SelectedIndex)
        arrLastState.Add(ddlCategory1.SelectedIndex)
        arrLastState.Add(ddlCategory2.SelectedIndex)
        arrLastState.Add(ddlCategory3.SelectedIndex)
        arrLastState.Add(ddlCategory4.SelectedIndex)
        arrLastState.Add(ddlStatus.SelectedIndex)
        arrLastState.Add(dgList.CurrentPageIndex)
        arrLastState.Add(CType(ViewState("currSortColumn"), String))
        arrLastState.Add(CType(ViewState("currSortDirection"), Sort.SortDirection))
        sessHelper.SetSession("CustomerCaseSessionState", arrLastState)
    End Sub
    Private Sub GetSessionCriteria()
        Dim arrLastState As ArrayList = sessHelper.GetSession("CustomerCaseSessionState")
        If Not arrLastState Is Nothing Then
            txtKodeDealer.Text = arrLastState.Item(0)
            txtKonsumen.Text = arrLastState.Item(1)
            txtNoMesin.Text = arrLastState.Item(2)
            txtNoPolisi.Text = arrLastState.Item(3)
            txtNoRangka.Text = arrLastState.Item(4)
            icStartDate.Value = arrLastState.Item(5)
            icEndDate.Value = arrLastState.Item(6)
            ddlCategory.SelectedIndex = arrLastState.Item(7)
            ddlCategory1.SelectedIndex = arrLastState.Item(8)
            ddlCategory2.SelectedIndex = arrLastState.Item(9)
            ddlCategory3.SelectedIndex = arrLastState.Item(10)
            ddlCategory4.SelectedIndex = arrLastState.Item(11)
            ddlStatus.SelectedIndex = arrLastState.Item(12)
            dgList.CurrentPageIndex = CInt(arrLastState.Item(13))
            ViewState("currSortColumn") = arrLastState.Item(14)
            ViewState("currSortDirection") = arrLastState.Item(15)
            BindToGrid(dgList.CurrentPageIndex)
        Else
            If CType(sessHelper.GetSession("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.DEALER Then
                txtKodeDealer.Text = CType(sessHelper.GetSession("DEALER"), Dealer).DealerCode
            End If
            ViewState("currSortColumn") = "CreatedTime"
            ViewState("currSortDirection") = Sort.SortDirection.DESC
            dgList.CurrentPageIndex = 0
        End If


    End Sub

    Private Sub BindCategory(ByVal ddl As DropDownList, ByVal IDReff As Integer)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CaseCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If IDReff > 0 Then
            criterias.opAnd(New Criteria(GetType(CaseCategory), "IDReff", MatchType.Exact, IDReff))
        Else
            criterias.opAnd(New Criteria(GetType(CaseCategory), "Level", MatchType.Exact, 1))
        End If

        Dim oCaseCategoryFacade As CaseCategoryFacade = New CaseCategoryFacade(User)
        Dim _arrCaseCategory As ArrayList = oCaseCategoryFacade.Retrieve(criterias)

        ddl.Items.Clear()
        ddl.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each cat As CaseCategory In _arrCaseCategory
            ddl.Items.Add(New ListItem(cat.Name, cat.ID))
        Next
    End Sub

    Private Sub BindToGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerCase), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(CustomerCase), "CaseDate", MatchType.GreaterOrEqual, icStartDate.Value))
        criterias.opAnd(New Criteria(GetType(CustomerCase), "CaseDate", MatchType.LesserOrEqual, icEndDate.Value.AddDays(1)))
        If txtKonsumen.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(CustomerCase), "CustomerName", MatchType.[Partial], txtKonsumen.Text))
        If txtNoMesin.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(CustomerCase), "EngineNumber", MatchType.[Partial], txtNoMesin.Text))
        If txtNoPolisi.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(CustomerCase), "PlateNumber", MatchType.[Partial], txtNoPolisi.Text))
        If txtNoRangka.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(CustomerCase), "ChassisNumber", MatchType.[Partial], txtNoRangka.Text))
        If txtNoCase.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(CustomerCase), "CaseNumber", MatchType.[Partial], txtNoCase.Text))

        If (ddlStatus.Items.Count > 0) AndAlso (ddlStatus.SelectedIndex <> 0) Then criterias.opAnd(New Criteria(GetType(CustomerCase), "Status", MatchType.Exact, ddlStatus.SelectedValue))

        If (ddlCategory.Items.Count > 0) AndAlso (ddlCategory.SelectedIndex <> 0) Then criterias.opAnd(New Criteria(GetType(CustomerCase), "Category", MatchType.EndsWith, ddlCategory.SelectedItem.Text.Trim.Replace(" ", "%")))
        If (ddlCategory1.Items.Count > 0) AndAlso (ddlCategory1.SelectedIndex <> 0) Then criterias.opAnd(New Criteria(GetType(CustomerCase), "SubCategory1", MatchType.EndsWith, ddlCategory1.SelectedItem.Text.Trim.Replace(" ", "%")))
        If (ddlCategory2.Items.Count > 0) AndAlso (ddlCategory2.SelectedIndex <> 0) Then criterias.opAnd(New Criteria(GetType(CustomerCase), "SubCategory2", MatchType.EndsWith, ddlCategory2.SelectedItem.Text.Trim.Replace(" ", "%")))
        If (ddlCategory3.Items.Count > 0) AndAlso (ddlCategory3.SelectedIndex <> 0) Then criterias.opAnd(New Criteria(GetType(CustomerCase), "SubCategory3", MatchType.EndsWith, ddlCategory3.SelectedItem.Text.Trim.Replace(" ", "%")))
        If (ddlCategory4.Items.Count > 0) AndAlso (ddlCategory4.SelectedIndex <> 0) Then criterias.opAnd(New Criteria(GetType(CustomerCase), "SubCategory4", MatchType.EndsWith, ddlCategory4.SelectedItem.Text.Trim.Replace(" ", "%")))

        If txtKodeDealer.Text.Trim() <> "" Then
            Dim strKodeDealerIn As String = "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"
            criterias.opAnd(New Criteria(GetType(CustomerCase), "Dealer.DealerCode", MatchType.InSet, strKodeDealerIn))
        Else
            Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                Dim strDealerIn As String = DealerFacade.GenerateDealerCodeSelection(objDealer, User)
                criterias.opAnd(New Criteria(GetType(CustomerCase), "Dealer.DealerCode", MatchType.InSet, strDealerIn))
            End If
        End If

        Dim oCustomerCaseFacade As CustomerCaseFacade = New CustomerCaseFacade(User)
        Dim _arrCustomerCase As ArrayList = oCustomerCaseFacade.RetrieveByCriteria(criterias, currentPageIndex + 1, dgList.PageSize, total, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))

        sessHelper.SetSession("CustomerCaseListSession", _arrCustomerCase)
        sessHelper.SetSession("CustomerCaseSessionCriterias", criterias)

        dgList.VirtualItemCount = total
        dgList.DataSource = _arrCustomerCase
        dgList.DataBind()

        lblTotalData.Text = total.ToString("#,##0")

    End Sub

#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            BindStatus()
            BindCategory(ddlCategory, 0)
            ClearForm()
            GetSessionCriteria()
        End If
    End Sub

    Private Sub BindStatus()
        Dim arl As ArrayList = EnumCustomerCaseResponse.RetriveCustomerPurpose(True)
        ddlStatus.Items.Clear()
        For Each item As EnumCustomerCaseResponseOp In arl
            ddlStatus.Items.Add(New ListItem(item.NameStatus, item.ValStatus))
        Next

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgList.CurrentPageIndex = 0
        BindToGrid(dgList.CurrentPageIndex)
    End Sub
    Private Sub dgList_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgList.SortCommand
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

        dgList.SelectedIndex = -1
        dgList.CurrentPageIndex = 0
        BindToGrid(dgList.CurrentPageIndex)
    End Sub
    Private Sub dgList_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgList.PageIndexChanged
        dgList.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dgList.CurrentPageIndex)
    End Sub
    Private Sub dgList_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgList.ItemDataBound

        If e.Item.ItemIndex >= 0 Then '
            Dim RowValue As CustomerCase = CType(e.Item.DataItem, CustomerCase)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
            Dim lnkbtnView As LinkButton = CType(e.Item.FindControl("lnkbtnView"), LinkButton)

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (e.Item.ItemIndex + 1) + (dgList.CurrentPageIndex * dgList.PageSize)
            lnkbtnEdit.Visible = bEditPriv
            lblStatus.Text = EnumCustomerCaseResponse.GetStringCustomerResponse(RowValue.Status)

            'Dim arr As New ArrayList
            'Dim sortColl As SortCollection = New SortCollection
            'sortColl.Add(New Sort(GetType(CustomerCaseResponse), "CreatedTime", Sort.SortDirection.ASC))
            'Dim _criterias As New CriteriaComposite(New Criteria(GetType(CustomerCaseResponse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '_criterias.opAnd(New Criteria(GetType(CustomerCaseResponse), "CustomerCase.ID", MatchType.Exact, RowValue.ID))
            'arr = New CustomerCaseResponseFacade(User).Retrieve(_criterias, sortColl)
            'If arr.Count > 0 Then
            '    Dim objCustResp As CustomerCaseResponse = CType(arr(arr.Count - 1), CustomerCaseResponse)
            '    If objCustResp.Status = EnumCustomerCaseResponse.CustomerCaseResponse.Closed Then
            '        lnkbtnEdit.Visible = False
            '    End If
            'End If

            If RowValue.Status = EnumCustomerCaseResponse.CustomerCaseResponse.Closed Then
                lnkbtnEdit.Visible = False
            End If
        End If

    End Sub
    Private Sub dgList_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgList.ItemCommand
        SetSessionCriteria()
        Select Case e.CommandName
            Case "Edit"
                Server.Transfer("~/Marketing/FrmCustomerCaseResponse.aspx?mode=edit&caseId=" & e.CommandArgument)
            Case "View"
                Server.Transfer("~/Marketing/FrmCustomerCaseResponse.aspx?mode=view&caseId=" & e.CommandArgument)
        End Select
    End Sub

#End Region

    Private Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        If ddlCategory.SelectedIndex <> -1 Then
            BindCategory(ddlCategory1, CInt(ddlCategory.SelectedValue))
        Else
            ddlCategory1.Items.Clear()
            ddlCategory1.Enabled = False
            ddlCategory2.Items.Clear()
            ddlCategory2.Enabled = False
            ddlCategory3.Items.Clear()
            ddlCategory3.Enabled = False
            ddlCategory4.Items.Clear()
            ddlCategory4.Enabled = False
        End If
    End Sub

    Private Sub ddlCategory1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory1.SelectedIndexChanged
        If ddlCategory1.SelectedIndex <> -1 Then
            ddlCategory2.Enabled = True
            BindCategory(ddlCategory2, CInt(ddlCategory1.SelectedValue))
        Else
            ddlCategory2.Items.Clear()
            ddlCategory2.Enabled = False
            ddlCategory3.Items.Clear()
            ddlCategory3.Enabled = False
            ddlCategory4.Items.Clear()
            ddlCategory4.Enabled = False
        End If
    End Sub

    Private Sub ddlCategory2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory2.SelectedIndexChanged
        If ddlCategory2.SelectedIndex <> -1 Then
            ddlCategory3.Enabled = True
            BindCategory(ddlCategory3, CInt(ddlCategory2.SelectedValue))
        Else
            ddlCategory3.Items.Clear()
            ddlCategory3.Enabled = False
            ddlCategory4.Items.Clear()
            ddlCategory4.Enabled = False
        End If
    End Sub

    Private Sub ddlCategory3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory3.SelectedIndexChanged
        If ddlCategory3.SelectedIndex <> -1 Then
            ddlCategory4.Enabled = True
            BindCategory(ddlCategory4, CInt(ddlCategory3.SelectedValue))
        Else
            ddlCategory4.Items.Clear()
            ddlCategory4.Enabled = False
        End If
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        'sessHelper.SetSession("CustomerCaseListSession", _arrCustomerCase)
        Dim arrListToDownload As New ArrayList
        If Not sessHelper.GetSession("CustomerCaseListSession") Is Nothing Then
            arrListToDownload = CType(sessHelper.GetSession("CustomerCaseListSession"), ArrayList)
        End If
        If arrListToDownload.Count > 0 Then
            DoDownload(arrListToDownload)
        Else
            MessageBox.Show("Tidak ada data yang di download")
        End If
    End Sub


    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "CustomerCase" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond
        Dim SAPCustomerData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(SAPCustomerData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(SAPCustomerData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteSAPKonsumenData(sw, data)
                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Download data gagal")
            Exit Sub
        End Try

        'Response.Write("<script language='javascript'>window.open('../downloadlocal.aspx?file=" & "DataTemp/" & sFileName & ".xls" & "');</script>")

        Response.Redirect("../downloadlocal.aspx?file=" & "DataTemp\" & sFileName & ".xls", False)
    End Sub

    Private Sub WriteSAPKonsumenData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Customer Case")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)

            Dim obj As New SAPCustomer

            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)

            itemLine.Append("No" & tab)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Nama Dealer" & tab)
            itemLine.Append("Nomor Case" & tab)
            itemLine.Append("Tanggal Case" & tab)
            itemLine.Append("Nama Konsumen" & tab)
            itemLine.Append("Phone" & tab)
            itemLine.Append("Email" & tab)
            itemLine.Append("No. Polisi" & tab)
            itemLine.Append("No. Rangka" & tab)
            itemLine.Append("No. Mesin" & tab)
            itemLine.Append("Kategori" & tab)
            itemLine.Append("Sub Kategori 1" & tab)
            itemLine.Append("Sub Kategori 2" & tab)
            itemLine.Append("Sub Kategori 3" & tab)
            itemLine.Append("Sub Kategori 4" & tab)
            itemLine.Append("Last Status" & tab)
            itemLine.Append("Last Update Time Status" & tab)
            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            For Each item As CustomerCase In data

                ' Add start changes 2017/12/27 by miyuki
                Dim arr As New ArrayList
                Dim objCustResp As CustomerCaseResponse = New CustomerCaseResponse
                Dim sortColl As SortCollection = New SortCollection
                sortColl.Add(New Sort(GetType(CustomerCaseResponse), "CreatedTime", Sort.SortDirection.ASC))
                Dim _criterias As New CriteriaComposite(New Criteria(GetType(CustomerCaseResponse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                _criterias.opAnd(New Criteria(GetType(CustomerCaseResponse), "CustomerCase.ID", MatchType.Exact, item.ID))
                arr = New CustomerCaseResponseFacade(User).Retrieve(_criterias, sortColl)
                If (Not arr Is Nothing) And arr.Count > 0 Then
                    objCustResp = CType(arr(arr.Count - 1), CustomerCaseResponse)
                End If
                ' add end changes

                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(i.ToString & tab)
                itemLine.Append(item.Dealer.DealerCode & tab)
                itemLine.Append(item.Dealer.DealerName & tab)
                itemLine.Append(item.CaseNumber & tab)
                itemLine.Append(item.CaseDate.ToString("yyyy/MM/dd") & tab)
                itemLine.Append(item.CustomerName & tab)
                itemLine.Append("'" & item.Phone & tab)
                itemLine.Append(item.Email & tab)
                itemLine.Append(item.PlateNumber & tab)
                itemLine.Append(item.ChassisNumber & tab)
                itemLine.Append(item.EngineNumber & tab)
                itemLine.Append(item.Category & tab)
                itemLine.Append(item.SubCategory1 & tab)
                itemLine.Append(item.SubCategory2 & tab)
                itemLine.Append(item.SubCategory3 & tab)
                itemLine.Append(item.SubCategory4 & tab)
                itemLine.Append(EnumCustomerCaseResponse.GetStringCustomerResponse(objCustResp.Status) & tab)
                itemLine.Append(objCustResp.CreatedTime & tab)
                sw.WriteLine(itemLine.ToString())
                i = i + 1
            Next

        End If
    End Sub
End Class
