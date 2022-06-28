Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security

Namespace KTB.DNet.UI.SparePart

    Public Class FrmDisplayPRP
        Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents btnDnLoad As System.Web.UI.WebControls.Button
        Protected WithEvents DropDownList1 As System.Web.UI.WebControls.DropDownList
        Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox
        Protected WithEvents DropDownList2 As System.Web.UI.WebControls.DropDownList
        Protected WithEvents btnFilter As System.Web.UI.WebControls.Button
        Protected WithEvents dtgPRP As System.Web.UI.WebControls.DataGrid
        Protected WithEvents ddlBeginMonth As System.Web.UI.WebControls.DropDownList
        Protected WithEvents ddlEndMonth As System.Web.UI.WebControls.DropDownList
        Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
        Protected WithEvents lblKodeDealerValue As System.Web.UI.WebControls.Label
        Protected WithEvents lblNamaDealerValue As System.Web.UI.WebControls.Label

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object
        Private bPrivilegeDaftarPRP As Boolean = False
        Private bPrivilegeDaftarPRPDealer As Boolean = False
        Protected WithEvents ddlBeginYear As System.Web.UI.WebControls.DropDownList
        Protected WithEvents ddlEndYear As System.Web.UI.WebControls.DropDownList
        Protected WithEvents periodValidator As System.Web.UI.WebControls.CustomValidator
        Private bPrivilegeDaftarPRPToko As Boolean = False

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Dim sHPrp As SessionHelper = New SessionHelper
        Private Const PRPCategoryActive = 0
        Private Const PRPCategoryInactive = 1
        Private RootDestDir As String = KTB.DNet.Lib.WebConfig.GetValue("SPFileDirectory")

#Region "Event Method"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not IsNothing(sHPrp.GetSession("DEALER")) Then
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                ActivateUserPrivilege()
                If Not IsPostBack Then
                    InitiatePage()
                    BindDataGrid(0)
                End If
            End If
        End Sub


        Private Sub ActivateUserPrivilege()
            'Dim objDealer As Dealer = CType(sHPrp.GetSession("DEALER"), Dealer)
            'If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            bPrivilegeDaftarPRPDealer = SecurityProvider.Authorize(Context.User, SR.DownloadDaftarPRPPerDealer_Privilege)

            bPrivilegeDaftarPRPToko = SecurityProvider.Authorize(Context.User, SR.DownloadDaftarPRPPerToko_Privilege)
            'End If

            If Not SecurityProvider.Authorize(Context.User, SR.ViewListPRP_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=PARTSHOP REWARD PROGRAM - Daftar PRP")
            End If
        End Sub

        Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click
            FilterData()
        End Sub

        Private Sub dtgPRP_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPRP.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgPRP.CurrentPageIndex * dtgPRP.PageSize)

                If Not e.Item.DataItem Is Nothing Then
                    Dim lblPeriod As Label = CType(e.Item.FindControl("lblPeriod"), Label)
                    Dim lblCategory As Label = CType(e.Item.FindControl("lblCategory"), Label)
                    Dim RowValue As PRPFile = CType(e.Item.DataItem, PRPFile)
                    lblPeriod.Text = Format(RowValue.Period, "MMMM yyyy")
                    lblCategory.Text = RowValue.PRPCategory.CategoryName

                    Dim lnkDisplayDealer As LinkButton = CType(e.Item.FindControl("lnkDisplayDealer"), LinkButton)
                    Dim lnkDisplayToko As LinkButton = CType(e.Item.FindControl("lnkDisplayToko"), LinkButton)
                    lnkDisplayDealer.Enabled = RowValue.PRPCategory.Status = PRPCategoryActive
                    lnkDisplayToko.Enabled = RowValue.PRPCategory.Status = PRPCategoryActive
                    If RowValue.PRPCategory.Status = PRPCategoryActive Then
                        lnkDisplayDealer.Text = "<img src='../images/detail.gif' border='0' alt='PerDealer'>"
                        lnkDisplayToko.Text = "<img src='../images/detail.gif' border='0' alt='PerDealer'>"
                    ElseIf RowValue.PRPCategory.Status = PRPCategoryInactive Then
                        lnkDisplayDealer.Text = "<img src='../images/in-aktif.gif' border='0' alt='PerDealer'>"
                        lnkDisplayToko.Text = "<img src='../images/in-aktif.gif' border='0' alt='PerDealer'>"
                    End If

                    If RowValue.Period.Year >= 2009 Then
                        lnkDisplayDealer.Visible = False
                    End If
                End If
            End If

            dtgPRP.Columns(6).Visible = bPrivilegeDaftarPRPDealer
            dtgPRP.Columns(7).Visible = bPrivilegeDaftarPRPToko
        End Sub

        Private Sub dtgPRP_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPRP.ItemCommand
            Try
                If e.CommandName = "DisplayDealer" Then
                    Dim filePath As String = RootDestDir & "\" & e.Item.Cells(4).Text
                    If FileExist(filePath) Then
                        sHPrp.SetSession("dspFilename", e.Item.Cells(4).Text)
                        Response.Redirect("FrmDisplayPRPDealer.aspx")
                    Else
                        MessageBox.Show(SR.FileNotFound(e.Item.Cells(4).Text))
                    End If
                ElseIf e.CommandName = "DisplayToko" Then
                    Dim filePath As String = RootDestDir & "\" & e.Item.Cells(4).Text
                    If FileExist(filePath) Then
                        sHPrp.SetSession("dspFilename", e.Item.Cells(4).Text)
                        Response.Redirect("FrmDisplayPRPToko.aspx")
                    Else
                        MessageBox.Show(SR.FileNotFound(e.Item.Cells(4).Text))
                    End If
                End If
            Catch
                MessageBox.Show("File tidak dapat diakses, harap hubungi Administrator")
            End Try
        End Sub

        Private Sub dtgPRP_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPRP.PageIndexChanged
            dtgPRP.SelectedIndex = -1
            dtgPRP.CurrentPageIndex = e.NewPageIndex
            BindDataGrid(dtgPRP.CurrentPageIndex)
        End Sub

        Private Sub dtgPRP_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPRP.SortCommand
            If CType(ViewState("vsSortColumn"), String) = e.SortExpression Then
                Select Case CType(ViewState("vsSortDirect"), Sort.SortDirection)

                    Case Sort.SortDirection.ASC
                        ViewState("vsSortDirect") = Sort.SortDirection.DESC

                    Case Sort.SortDirection.DESC
                        ViewState("vsSortDirect") = Sort.SortDirection.ASC
                End Select
            Else
                ViewState("vsSortColumn") = e.SortExpression
                ViewState("vsSortDirect") = Sort.SortDirection.ASC
            End If

            dtgPRP.SelectedIndex = -1
            dtgPRP.CurrentPageIndex = 0
            BindDataGrid(dtgPRP.CurrentPageIndex)
        End Sub

#End Region

#Region "Custom Method"

        Private Sub InitiatePage()
            Dim objDealer As Dealer = sHPrp.GetSession("DEALER")
            If Not objDealer Is Nothing Then
                lblKodeDealerValue.Text = objDealer.DealerCode
                lblNamaDealerValue.Text = objDealer.DealerName & " / " & objDealer.SearchTerm2
            End If
            BindMonth()
            BindCategory()
            BindYear()
            ClearPage()
            ViewState("vsSortColumn") = "Period"
            ViewState("vsSortDirect") = Sort.SortDirection.DESC
        End Sub

        Private Sub BindDataGrid(ByVal indexPage As Integer)
            Dim crit As CriteriaComposite = CType(sHPrp.GetSession("PRPCriteria"), CriteriaComposite)

            Dim Data As ArrayList
            Dim totalRow As Integer
            If IsNothing(crit) Then
                Data = New PRPFileFacade(User).RetrieveList(indexPage + 1, dtgPRP.PageSize, totalRow, ViewState("vsSortColumn"), ViewState("vsSortDirect"))
            Else
                Dim sortColl As SortCollection = New SortCollection

                If (Not IsNothing(ViewState("vsSortColumn"))) And (Not IsNothing(ViewState("vsSortDirect"))) Then
                    sortColl.Add(New Sort(GetType(PRPFile), ViewState("vsSortColumn"), viewstate("vsSortDirect")))
                Else
                    sortColl = Nothing
                End If

                Data = New PRPFileFacade(User).RetrieveActiveList(indexPage + 1, dtgPRP.PageSize, totalRow, crit, sortColl)
            End If

            dtgPRP.DataSource = Data
            dtgPRP.VirtualItemCount = totalRow
            dtgPRP.DataBind()
        End Sub

        Private Sub BindCategory()
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PRPCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'crit.opAnd(New Criteria(GetType(PRPCategory), "Status", MatchType.Exact, 0))
            Dim prpCategories As ArrayList = New PRPCategoryFacade(User).Retrieve(crit)
            Dim lItem As ListItem
            For Each prpCat As PRPCategory In prpCategories
                lItem = New ListItem(prpCat.CategoryName, prpCat.ID)
                ddlCategory.Items.Add(lItem)
            Next
            lItem = New ListItem("Silahkan Pilih", "")
            ddlCategory.Items.Insert(0, lItem)
        End Sub

        Private Sub ClearPage()
            ddlBeginMonth.SelectedIndex = 0
            ddlEndMonth.SelectedIndex = 0
            ddlBeginYear.SelectedIndex = 0
            ddlEndYear.SelectedIndex = 0
            ddlCategory.SelectedIndex = 0
        End Sub

        Private Sub BindMonth()
            Dim lItem As ListItem

            For numMonth As Integer = 1 To 12
                lItem = New ListItem(MonthName(numMonth), CStr(numMonth))
                ddlBeginMonth.Items.Add(lItem)
                ddlEndMonth.Items.Add(lItem)
            Next

            lItem = New ListItem("Pilih Bulan", "0")
            ddlBeginMonth.Items.Insert(0, lItem)
            ddlEndMonth.Items.Insert(0, lItem)

        End Sub

        Private Sub BindYear()
            Dim lItem As ListItem
            Dim numYear As Integer = Now.Year
            For year As Integer = numYear - 2 To numYear + 1
                lItem = New ListItem(CStr(year), CStr(year))
                ddlBeginYear.Items.Add(lItem)
                ddlEndYear.Items.Add(lItem)
            Next

            lItem = New ListItem("Pilih Tahun", "0")
            ddlBeginYear.Items.Insert(0, lItem)
            ddlEndYear.Items.Insert(0, lItem)
        End Sub


        Private Function ValidPage() As Boolean
            If ddlBeginMonth.SelectedIndex > 0 Then
                If ddlBeginYear.SelectedIndex = 0 Then
                    periodValidator.ControlToValidate = ddlBeginYear.ID
                    periodValidator.ErrorMessage = "Tahun awal masih kosong"
                    periodValidator.IsValid = False
                    Return False
                End If
            End If
            If ddlEndMonth.SelectedIndex > 0 Then
                If ddlEndYear.SelectedIndex = 0 Then
                    periodValidator.ControlToValidate = ddlEndYear.ID
                    periodValidator.ErrorMessage = "Tahun akhir masih kosong"
                    periodValidator.IsValid = False
                    Return False
                End If
            End If
            Return True
        End Function

        Private Function FilterData()
            sHPrp.RemoveSession("PRPCriteria")
            If ValidPage() Then
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PRPFile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim listOfCrit As ArrayList = New ArrayList
                Dim critBeginPeriod As Criteria
                Dim critEndPeriod As Criteria
                Dim beginPeriod As DateTime
                Dim endPeriod As DateTime
                If ddlCategory.SelectedIndex > 0 Then
                    Dim critCategory = New Criteria(GetType(PRPFile), "PRPCategory.ID", MatchType.Exact, CInt(ddlCategory.SelectedValue))
                    listOfCrit.Add(critCategory)
                End If
                If ddlBeginMonth.SelectedIndex > 0 Then
                    If ddlBeginYear.SelectedIndex <> 0 Then
                        beginPeriod = New DateTime(CInt(ddlBeginYear.SelectedValue), CInt(ddlBeginMonth.SelectedValue), 1, 0, 0, 0)
                        critBeginPeriod = New Criteria(GetType(PRPFile), "Period", MatchType.GreaterOrEqual, beginPeriod)
                        listOfCrit.Add(critBeginPeriod)
                    End If
                Else
                    If ddlBeginYear.SelectedIndex <> 0 Then
                        beginPeriod = New DateTime(CInt(ddlBeginYear.SelectedValue), 1, 1, 0, 0, 0)
                        critBeginPeriod = New Criteria(GetType(PRPFile), "Period", MatchType.GreaterOrEqual, beginPeriod)
                        listOfCrit.Add(critBeginPeriod)
                    End If
                End If
                If ddlEndMonth.SelectedIndex > 0 Then
                    If ddlEndYear.SelectedIndex <> 0 Then
                        Dim eYear As Integer = CInt(ddlEndYear.SelectedValue)
                        Dim eMonth As Integer = CInt(ddlEndMonth.SelectedValue)
                        Dim eDay As Integer = Date.DaysInMonth(eYear, eMonth)
                        endPeriod = New DateTime(eYear, eMonth, eDay, 23, 59, 59)
                        critEndPeriod = New Criteria(GetType(PRPFile), "Period", MatchType.LesserOrEqual, endPeriod)
                        listOfCrit.Add(critEndPeriod)
                    End If
                Else
                    If ddlEndYear.SelectedIndex <> 0 Then
                        Dim eYear As Integer = CInt(ddlEndYear.SelectedValue)
                        Dim eMonth As Integer = 12
                        Dim eDay As Integer = Date.DaysInMonth(eYear, eMonth)
                        endPeriod = New DateTime(eYear, eMonth, eDay, 23, 59, 59)
                        critEndPeriod = New Criteria(GetType(PRPFile), "Period", MatchType.LesserOrEqual, endPeriod)
                        listOfCrit.Add(critEndPeriod)
                    End If
                End If
                If Not IsNothing(critBeginPeriod) And Not IsNothing(critEndPeriod) And beginPeriod > endPeriod Then
                    periodValidator.ErrorMessage = SR.InvalidRangeDate
                    periodValidator.IsValid = False
                End If
                If periodValidator.IsValid Then
                    For critCount As Integer = 0 To listOfCrit.Count - 1
                        crit.opAnd(CType(listOfCrit(critCount), Criteria))
                    Next
                    sHPrp.SetSession("PRPCriteria", crit)
                    dtgPRP.CurrentPageIndex = 0
                    BindDataGrid(0)
                End If
            End If
        End Function

        Private Function FileExist(ByVal filename As String) As Boolean
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

            Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

            Try
                sapImp.Start()

                Try
                    Dim fInfo As System.IO.FileInfo = New System.IO.FileInfo(filename)
                    Return fInfo.Exists
                Catch
                    Throw
                Finally
                    sapImp.StopImpersonate()
                End Try
            Catch
                Throw New Exception("File tidak dapat diakses")
            End Try
        End Function


#End Region

    End Class
End Namespace