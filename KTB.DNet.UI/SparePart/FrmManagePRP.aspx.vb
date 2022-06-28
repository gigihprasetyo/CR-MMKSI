Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security

Namespace KTB.DNet.UI.SparePart

    Public Class FrmManagePRP
        Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents btnFilter As System.Web.UI.WebControls.Button
        Protected WithEvents dtgPRP As System.Web.UI.WebControls.DataGrid
        Protected WithEvents btnDelete As System.Web.UI.WebControls.Button
        Protected WithEvents ddlBeginMonth As System.Web.UI.WebControls.DropDownList
        Protected WithEvents txtEndYear As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtBeginYear As System.Web.UI.WebControls.TextBox
        Protected WithEvents ddlEndMonth As System.Web.UI.WebControls.DropDownList
        Protected WithEvents periodValidator As System.Web.UI.WebControls.CustomValidator
        Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object
        Private bPrivilegeMngPRP As Boolean = False

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Dim sHPrp As SessionHelper = New SessionHelper
        Private RootDir As String = KTB.DNet.Lib.WebConfig.GetValue("SPFileDirectory")

#Region "Event Method"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            ActivateUserPrivilege()
            If Not IsPostBack Then
                InitiatePage()
                BindDataGrid(0)
            End If
        End Sub
        Private Sub SetControlPrivilege()
            btnDelete.Visible = bPrivilegeMngPRP
            '--060204 in active by request fo BA 
            'btnFilter.Visible = bPrivilegeMngPRP
        End Sub

        Private Sub ActivateUserPrivilege()
            bPrivilegeMngPRP = SecurityProvider.Authorize(Context.User, SR.SearchManagePRP_Privilege)

            If Not SecurityProvider.Authorize(Context.User, SR.ViewManagePRP_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=PARTSHOP REWARD PROGRAM - Pengelolaan PRP")
            End If
        End Sub
        Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click
            FilterData()
        End Sub

        Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
            Try
                DeleteAll()
                dtgPRP.CurrentPageIndex = 0
                BindDataGrid(dtgPRP.CurrentPageIndex)
            Catch ex As Exception
            End Try
        End Sub

        Private Sub dtgPRP_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPRP.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgPRP.CurrentPageIndex * dtgPRP.PageSize)
                If Not e.Item.FindControl("lnkDelete") Is Nothing Then
                    CType(e.Item.FindControl("lnkDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                End If

                Dim cbItem As CheckBox = CType(e.Item.FindControl("cbItem"), CheckBox)
                cbItem.Attributes.Add("onclick", "EnableDelete('cbItem')")
                If Not e.Item.DataItem Is Nothing Then
                    Dim lblPeriod As Label = CType(e.Item.FindControl("lblPeriod"), Label)
                    Dim lblCategory As Label = CType(e.Item.FindControl("lblCategory"), Label)
                    Dim RowValue As PRPFile = CType(e.Item.DataItem, PRPFile)
                    lblPeriod.Text = Format(RowValue.Period, "MMMM yyyy")
                    lblCategory.Text = RowValue.PRPCategory.CategoryName
                End If
            End If

            dtgPRP.Columns(6).Visible = bPrivilegeMngPRP
            dtgPRP.Columns(1).Visible = bPrivilegeMngPRP
        End Sub

        Private Sub dtgPRP_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPRP.ItemCommand
            If e.CommandName = "Delete" Then
                Try
                    DeletePRP(e.Item.Cells(0).Text)
                    BindDataGrid(dtgPRP.CurrentPageIndex)
                    MessageBox.Show(SR.DeleteSucces)
                Catch ex As Exception
                    MessageBox.Show(SR.DeleteFail)
                End Try
            End If
        End Sub

        Private Sub dtgPRP_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPRP.PageIndexChanged
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

            dtgPRP.CurrentPageIndex = 0
            BindDataGrid(dtgPRP.CurrentPageIndex)
        End Sub

#End Region

#Region "Custom Method"

        Private Sub InitiatePage()
            BindMonth()
            BindCategory()
            SetControlPrivilege()
            ClearPage()
            ViewState("vsSortColumn") = "Period"
            ViewState("vsSortDirect") = Sort.SortDirection.ASC
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
            bPrivilegeMngPRP = SecurityProvider.Authorize(Context.User, SR.SearchManagePRP_Privilege)
            dtgPRP.DataBind()
        End Sub

        Private Sub BindCategory()
            Dim prpCategories As ArrayList = New PRPCategoryFacade(User).RetrieveList()
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
            txtBeginYear.Text = ""
            txtEndYear.Text = ""
            ddlCategory.SelectedIndex = 0
            btnDelete.Enabled = False
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

        Private Function IsControlValid(ByVal controlToValidate As String, ByVal errorMessage As String) As Boolean
            periodValidator.ControlToValidate = controlToValidate
            periodValidator.ErrorMessage = errorMessage
            Try
                If periodValidator.FindControl(controlToValidate).GetType Is GetType(DropDownList) Then
                    Dim ddlMonth As DropDownList = periodValidator.FindControl(controlToValidate)
                    periodValidator.IsValid = ddlMonth.SelectedIndex > 0
                    Return periodValidator.IsValid
                ElseIf periodValidator.FindControl(controlToValidate).GetType Is GetType(TextBox) Then
                    Dim ddlYear As TextBox = periodValidator.FindControl(controlToValidate)
                    If (ddlYear.Text = "") Then
                        periodValidator.IsValid = True
                    ElseIf ((ddlYear.Text.Length < 4) Or Not IsNumeric(ddlYear.Text) Or (CInt(ddlYear.Text) < 1900)) Then
                        If CInt(ddlYear.Text) < 1900 Then
                            periodValidator.ErrorMessage += " dan harus >= 1900"
                        End If
                        periodValidator.IsValid = False
                    End If
                    Return periodValidator.IsValid
                End If
            Catch
                periodValidator.IsValid = False
                Return periodValidator.IsValid
            End Try

            periodValidator.IsValid = True
            Return periodValidator.IsValid
        End Function

        Private Function ValidPage() As Boolean
            If ddlBeginMonth.SelectedIndex > 0 Then
                If txtBeginYear.Text = "" Then
                    periodValidator.ControlToValidate = txtBeginYear.ID
                    periodValidator.ErrorMessage = "Tahun awal masih kosong"
                    periodValidator.IsValid = False
                    Return False
                Else
                    If Not IsControlValid(txtBeginYear.ID, "Format tahun xxxx, x=angka") Then
                        Return False
                    End If
                End If
            Else
                If txtBeginYear.Text <> "" And Not IsControlValid(txtBeginYear.ID, "Format tahun xxxx, x=angka") Then
                    Return False
                End If
            End If
            If ddlEndMonth.SelectedIndex > 0 Then
                If txtEndYear.Text = "" Then
                    periodValidator.ControlToValidate = txtEndYear.ID
                    periodValidator.ErrorMessage = "Tahun akhir masih kosong"
                    periodValidator.IsValid = False
                    Return False
                Else
                    If Not IsControlValid(txtEndYear.ID, "Format tahun xxxx, x = angka") Then
                        Return False
                    End If
                End If
            Else
                If txtEndYear.Text <> "" And Not IsControlValid(txtEndYear.ID, "Format tahun xxxx, x = angka") Then
                    Return False
                End If
            End If
            Return True
        End Function

        Private Sub DeletePRP(ByVal nID As Integer)
            Dim objFacade As PRPFileFacade = New PRPFileFacade(User)
            Dim objPRP As PRPFile = objFacade.Retrieve(nID)
            If IsNothing(objPRP) Then
                MessageBox.Show(SR.DataNotFound("PRP"))
            Else
                'TODO : Deleting Physical file is not implemented yet
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
                Dim filename As String = RootDir & "\" & objPRP.Filename

                Try
                    If objFacade.DeleteFromDB(objPRP) < 0 Then
                        Throw New Exception(SR.DeleteFail)
                    End If
                    Dim objDeleteFile As TransferFile = New TransferFile(_user, _password, _webServer)
                    objDeleteFile.deleteFile(filename)
                    dtgPRP.CurrentPageIndex = 0
                Catch ex As IO.IOException

                Catch ex As Exception
                    Throw
                End Try
            End If
        End Sub

        Private Sub DeleteAll()
            For row As Integer = 0 To dtgPRP.Items.Count - 1
                If dtgPRP.Items(row).ItemType = ListItemType.Item Or dtgPRP.Items(row).ItemType = ListItemType.AlternatingItem Then
                    Dim cbItem As CheckBox = dtgPRP.Items(row).FindControl("cbItem")
                    If Not IsNothing(cbItem) And cbItem.Checked Then
                        DeletePRP(CInt(dtgPRP.Items(row).Cells(0).Text))
                    End If
                End If
            Next
            dtgPRP.CurrentPageIndex = 0
        End Sub

        Private Sub FilterData()
            sHPrp.RemoveSession("PRPCriteria")
            If ValidPage() Then
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
                    If txtBeginYear.Text <> "" Then
                        beginPeriod = New DateTime(CInt(txtBeginYear.Text), CInt(ddlBeginMonth.SelectedValue), 1, 0, 0, 0)
                        critBeginPeriod = New Criteria(GetType(PRPFile), "Period", MatchType.GreaterOrEqual, beginPeriod)
                        listOfCrit.Add(critBeginPeriod)
                    End If
                Else
                    If txtBeginYear.Text <> "" Then
                        beginPeriod = New DateTime(CInt(txtBeginYear.Text), 1, 1, 0, 0, 0)
                        critBeginPeriod = New Criteria(GetType(PRPFile), "Period", MatchType.GreaterOrEqual, beginPeriod)
                        listOfCrit.Add(critBeginPeriod)
                    End If
                End If
                If ddlEndMonth.SelectedIndex > 0 Then
                    If txtEndYear.Text <> "" Then
                        Dim eYear As Integer = CInt(txtEndYear.Text)
                        Dim eMonth As Integer = CInt(ddlEndMonth.SelectedValue)
                        Dim eDay As Integer = Date.DaysInMonth(eYear, eMonth)
                        endPeriod = New DateTime(eYear, eMonth, eDay, 23, 59, 59)
                        critEndPeriod = New Criteria(GetType(PRPFile), "Period", MatchType.LesserOrEqual, endPeriod)
                        listOfCrit.Add(critEndPeriod)
                    End If
                Else
                    If txtEndYear.Text <> "" Then
                        Dim eYear As Integer = CInt(txtEndYear.Text)
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
                    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PRPFile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    For Each eachCrit As Criteria In listOfCrit
                        crit.opAnd(eachCrit)
                    Next
                    sHPrp.SetSession("PRPCriteria", crit)
                    dtgPRP.CurrentPageIndex = 0
                    BindDataGrid(0)
                End If
            End If
        End Sub
#End Region
    End Class

End Namespace
