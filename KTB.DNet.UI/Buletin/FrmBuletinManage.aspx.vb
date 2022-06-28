#Region " Summary "
'--------------------------------------------'
'-- Program Code : FrmBuletinManage.aspx   --'
'-- Program Name : Daftar User             --'
'-- Description  :                         --'
'--------------------------------------------'
'-- Programmer   : UNKNOWN                 --'
'-- Start Date   : Jan 09 2006             --'
'-- Update By    :                         --'
'-- Last Update  : Jan 09 2006             --'
'--------------------------------------------'
'-- Copyright © 2005 by Intimedia          --'
'--------------------------------------------'
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security

Imports KTB.DNet.BusinessFacade.Buletin
Imports KTB.DNet.Parser
Imports System.Drawing.Color
#End Region

Public Class FrmBuletinManage
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnDelete1 As System.Web.UI.WebControls.Button
    Protected WithEvents btnDelete2 As System.Web.UI.WebControls.Button
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlSubCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtKeywords As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgBuletin As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlMonthPeriods As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYearPeriod As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtJudul As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDeskripsi As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Private Variables"
    Dim sessHelp As SessionHelper = New SessionHelper
    Private SeparatorKeywords As String = ","
#End Region

#Region " Custom Method "

    Private Sub Initialize()
        '-- Init objects

        '-- Clear text fields (keywords and year periods)
        txtKeywords.Text = String.Empty
        txtJudul.Text = String.Empty
        txtDeskripsi.Text = String.Empty
        ddlYearPeriod.SelectedIndex = 0

        '--Bind dropdownlist Year Period
        PopulateYearPeriod()

        '-- Bind dropdownlist month periods
        PopulateMonthPeriods()

        '-- Bind dropdownlist main category
        PopulateParent()

        '-- Bind dropdownlist sub category
        PopulateSelectionCategory(0)

        '-- Display grid column headers
        dtgBuletin.DataSource = New ArrayList
        dtgBuletin.DataBind()

        '-- Add confirmation javascript to delete button
        'SR.BuletinManage_Privilege
        btnDelete1.Attributes.Add("onclick", "javascript: return confirm('" & SR.DeleteConfirmation & "');")
        'btnDelete2.Attributes.Add("onclick", "javascript: return confirm('" & SR.DeleteConfirmation & "');")
        btnDelete2.Attributes.Add("onclick", "javascript: return ValidateDelete('chkTick');")

    End Sub

    Private Sub PopulateYearPeriod()
        Dim currentYear As Integer = Date.Now.Year
        Dim nextYear As Integer = Date.Now.Year + 1
        'Rina Request 18 Feb 08
        'ddlYearPeriod.Items.Add(currentYear)
        'ddlYearPeriod.Items.Add(nextYear)
        Dim idx As Integer = 0
        For i As Integer = (Date.Now.Year - 10) To Date.Now.Year + 1
            ddlYearPeriod.Items.Add(i)
            idx = idx + 1
        Next
        ddlYearPeriod.SelectedIndex = idx - 2
    End Sub

    Private Sub PopulateMonthPeriods()
        '-- Bind "Month Periods" dropdownlist
        ddlMonthPeriods.Items.Add(New ListItem("Pilih Bulan", 0))
        For Each item As ListItem In LookUp.ArrayMonth
            ddlMonthPeriods.Items.Add(item)
        Next
    End Sub

    Private Sub PopulateParent()
        Dim org As Dealer = CType(Session.Item("DEALER"), Dealer)
        Dim _BuletinFacade As BuletinCategoryFacade = New BuletinCategoryFacade(User)
        Dim list As ArrayList = _BuletinFacade.RetrieveParentList(org.Title)
        Dim li As New ListItem("Pilih Kategori", "0")
        ddlCategory.Items.Add(li)
        If list.Count > 0 Then
            For Each item As BuletinCategory In list
                li = New ListItem
                li.Text = item.Code
                li.Value = item.ID
                ddlCategory.Items.Add(li)
            Next
        End If
    End Sub

    Private Sub PopulateSelectionCategory(ByVal parent As Integer)
        ddlSubCategory.Items.Clear()
        Dim org As Dealer = CType(Session.Item("DEALER"), Dealer)
        Dim _item As New ListItem("Pilih Sub Kategori", "0")
        ddlSubCategory.Items.Add(_item)
        'ddlSubCategory.Enabled = False
        If parent > 0 Then
            Dim _BuletinFacade As BuletinCategoryFacade = New BuletinCategoryFacade(User)
            Dim list As ArrayList = _BuletinFacade.PopulateListView(parent, org.Title)
            Dim space As String = String.Empty
            If list.Count > 0 Then
                'ddlSubCategory.Enabled = True
                For Each item As BuletinCategory In list
                    space = BuildLeadingSpace(item.Leveling)
                    _item = New ListItem
                    _item.Value = item.ID
                    _item.Text = space & item.Code
                    ddlSubCategory.Items.Add(_item)
                    space = String.Empty
                Next
            End If
        End If
    End Sub

    Private Function BuildLeadingSpace(ByVal count As Integer) As String
        Dim space As String = String.Empty
        If count > 1 Then
            For i As Integer = 0 To count - 2
                space += "--"
            Next
            space = space & ">"
        End If
        Return space
    End Function

    Private Sub ReadCriteria()
        '-- Read selection criteria

        '-- Init sorting column and sort direction default
        ViewState("currSortColumn") = ""
        ViewState("currSortDirection") = Sort.SortDirection.ASC

        '-- Restore selection criteria
        Dim crits As Hashtable
        crits = CType(sessHelp.GetSession("FrmBuletinManageCrits"), Hashtable)
        If Not IsNothing(crits) Then
            Try
                ddlYearPeriod.SelectedValue = crits.Item("PeriodYear") 'CInt(crits.Item("PeriodYear")).ToString()
            Catch
                ddlYearPeriod.SelectedIndex = 0
            End Try
            'txtYearPeriods.Text = CInt(crits.Item("PeriodYear")).ToString()

            Try
                ddlMonthPeriods.SelectedIndex = CInt(crits.Item("PeriodMonth"))
            Catch
                ddlMonthPeriods.SelectedIndex = DateTime.Today.Month
            End Try

            Try
                ddlCategory.SelectedValue = CInt(crits.Item("BuletinCategory.TopParent")).ToString()
                ddlCategory_SelectedIndexChanged(Me, Nothing)
            Catch
                ddlCategory.SelectedIndex = 0
            End Try

            Try
                ddlSubCategory.SelectedValue = CInt(crits.Item("BuletinCategory.Parent")).ToString()
            Catch
                ddlSubCategory.SelectedIndex = 0
            End Try

            txtKeywords.Text = CStr(crits.Item("Keywords"))
            txtJudul.Text = CStr(crits.Item("Title"))
            txtDeskripsi.Text = CStr(crits.Item("Description"))

        End If

    End Sub

    Private Sub SaveCriteria()
        '-- Save selection criteria

        '-- Save selection criteria for later restore
        Dim crits As Hashtable = New Hashtable
        crits.Add("PeriodYear", ddlYearPeriod.SelectedValue) 'txtYearPeriods.Text.Trim())
        crits.Add("PeriodMonth", ddlMonthPeriods.SelectedIndex)
        crits.Add("BuletinCategory.TopParent", ddlCategory.SelectedValue)
        crits.Add("BuletinCategory.Parent", ddlSubCategory.SelectedValue)
        crits.Add("Keywords", txtKeywords.Text.Trim())
        crits.Add("Title", txtJudul.Text.Trim())
        crits.Add("Description", txtDeskripsi.Text.Trim())
        sessHelp.SetSession("FrmBuletinManageCrits", crits)
    End Sub

    Private Sub ReadData()
        '-- Read all data selected

        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Buletin), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '-- Get selected value of main categories
        Dim iSelectedCat As Integer = 0
        If ddlCategory.Items.Count > 0 Then
            iSelectedCat = CInt(ddlCategory.SelectedValue)
        End If

        '-- This condition only work when sub categories is selected
        Dim iSelectedSub As Integer = 0
        If ddlSubCategory.Items.Count > 0 Then
            '-- Get selected value of sub categories
            iSelectedSub = CInt(ddlSubCategory.SelectedValue)
            If iSelectedSub > 0 Then
                ' 1st version, use existing field to clause
                'If iSelectedCat > 0 Then
                'criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.TopParent", MatchType.Exact, iSelectedCat))
                'End If
                'criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.Parent", MatchType.Exact, iSelectedSub))
                'criterias.opOr(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.Exact, iSelectedSub))

                ' 2nd version, using looping list of array
                Dim list As New ArrayList ' this must be set to new for retrieve sub parent
                Dim _BuletinCategoryFacade As BuletinCategoryFacade = New BuletinCategoryFacade(User)
                Dim _BuletinCategory As BuletinCategory = _BuletinCategoryFacade.Retrieve(iSelectedSub)
                _BuletinCategoryFacade.RetrieveAllSubParentCategory(_BuletinCategory, list)

                ' check are we get the sub parent
                If (list.Count > 0) Then
                    Dim iEnds As Integer = list.Count - 1
                    Dim i As Integer

                    ' use the current selection sub categories
                    criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.Exact, _BuletinCategory.ID), "(", True)

                    ' and do looping to get sub parent list, under the selection sub categories
                    For i = 0 To iEnds
                        If i = iEnds Then
                            criterias.opOr(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.Exact, CType(list(i), BuletinCategory).ID), ")", False)
                        Else
                            criterias.opOr(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.Exact, CType(list(i), BuletinCategory).ID))
                        End If
                    Next
                Else
                    ' if we not get sub parent, use this criteria
                    ' the current selection sub categories
                    criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.Exact, _BuletinCategory.ID))
                End If
            End If
        End If

        '-- This condition only work when sub categories is not selected
        If iSelectedSub = 0 Then
            If iSelectedCat > 0 Then
                criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.TopParent", MatchType.Exact, iSelectedCat), "(", True)
                criterias.opOr(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.Exact, iSelectedCat), ")", False)
            End If
        End If

        '-- year periods
        'If txtYearPeriods.Text.Trim() <> "" Then
        '    If IsNumeric(txtYearPeriods.Text.Trim()) Then
        '        Dim iYear As Integer = CInt(txtYearPeriods.Text.Trim())
        '        criterias.opAnd(New Criteria(GetType(Buletin), "PeriodYear", MatchType.Exact, iYear))
        '    End If
        'End If
        If ddlYearPeriod.SelectedValue <> "" Then
            If IsNumeric(ddlYearPeriod.SelectedValue) Then
                Dim iYear As Integer = CInt(ddlYearPeriod.SelectedValue)
                criterias.opAnd(New Criteria(GetType(Buletin), "PeriodYear", MatchType.Exact, iYear))
            End If
        End If


        '-- month periods
        If ddlMonthPeriods.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(Buletin), "PeriodMonth", MatchType.Exact, ddlMonthPeriods.SelectedIndex))
        End If

        '-- keywords
        If txtKeywords.Text.Trim() <> "" Then
            ' separate keywords with separator
            Dim sKeys() As String = Split(txtKeywords.Text.Trim(), Me.SeparatorKeywords)

            If sKeys.GetLength(0) > 0 Then
                Dim iStart As Integer = sKeys.GetLowerBound(0)
                Dim iEnd As Integer = sKeys.GetUpperBound(0)
                Dim i As Integer

                ' then loop to search in keywords
                For i = iStart To iEnd
                    If (i = iStart) And (i = iEnd) Then
                        criterias.opAnd(New Criteria(GetType(Buletin), "Keywords", MatchType.[Partial], sKeys(i).Trim()))
                    ElseIf (i = iStart) Then
                        criterias.opAnd(New Criteria(GetType(Buletin), "Keywords", MatchType.[Partial], sKeys(i).Trim()), "(", True)
                    ElseIf (i = iEnd) Then
                        criterias.opOr(New Criteria(GetType(Buletin), "Keywords", MatchType.[Partial], sKeys(i).Trim()), ")", False)
                    Else
                        criterias.opOr(New Criteria(GetType(Buletin), "Keywords", MatchType.[Partial], sKeys(i).Trim()))
                    End If
                Next
            End If
        End If

        '-- Title
        If txtJudul.Text.Trim.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(Buletin), "Title", MatchType.[Partial], txtJudul.Text.Trim()))
        End If

        '-- Description
        If txtDeskripsi.Text.Trim.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(Buletin), "Description", MatchType.[Partial], txtDeskripsi.Text.Trim()))
        End If
        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Buletin), "PeriodYear", Sort.SortDirection.DESC))  '-- period of year
        sortColl.Add(New Sort(GetType(Buletin), "PeriodMonth", Sort.SortDirection.DESC))  '-- period of year

        '-- Retrieve recordset
        Dim BuletinList As ArrayList
        If viewstate("FromPrev") = 0 Then
            BuletinList = New BuletinFacade(User).Retrieve(criterias, sortColl)
        Else
            BuletinList = New BuletinFacade(User).Retrieve(sessHelp.GetSession("CritsBack"), sessHelp.GetSession("SortCollBack"))
        End If

        '-- Store BuletinList into session for later use
        sessHelp.SetSession("BuletinList", BuletinList)

        '--store criterias untuk button backnya
        sessHelp.SetSession("CritsBack", criterias)
        sessHelp.SetSession("SortCollBack", sortColl)

        ' to show / hide controls (button delete and datagrid buletin)
        Dim showbuttondeleteandgrid As Boolean = True ' while set to true

        ' check if selection criteria has data
        If (BuletinList.Count = 0) Then
            ' cause not exist then set variable to false
            showbuttondeleteandgrid = False

            ' if this is post back then give message
            If Page.IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        End If

        ' set controls to became visible / not
        btnDelete1.Visible = showbuttondeleteandgrid
        btnDelete2.Visible = showbuttondeleteandgrid
        dtgBuletin.Visible = showbuttondeleteandgrid

    End Sub

    Private Sub BindPage(ByVal pageIndex As Integer)
        '-- Bind page-i

        '-- Read BuletinList from session
        Dim BuletinList As ArrayList = CType(sessHelp.GetSession("BuletinList"), ArrayList)

        If BuletinList.Count <> 0 Then
            '-- Sort first
            If viewstate("FromPrev") = 0 Then
                SortListControl(BuletinList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Else
                SortListControl(BuletinList, CType(sessHelp.GetSession("BuletinSortColl"), String), CType(sessHelp.GetSession("BuletinSortDir"), Integer))
            End If


            '-- Then paging
            Dim PagedList As ArrayList
            If viewstate("FromPrev") = 0 Then
                PagedList = ArrayListPager.DoPage(BuletinList, pageIndex, dtgBuletin.PageSize)
                dtgBuletin.CurrentPageIndex = pageIndex
            Else
                PagedList = ArrayListPager.DoPage(BuletinList, sessHelp.GetSession("BuletinPageIndex"), dtgBuletin.PageSize)
                dtgBuletin.CurrentPageIndex = sessHelp.GetSession("BuletinPageIndex")
            End If

            dtgBuletin.DataSource = PagedList
            dtgBuletin.VirtualItemCount = BuletinList.Count()
            dtgBuletin.DataKeyField = "ID"
            dtgBuletin.DataBind()

            'Ditambahkan oleh Ery untuk kebutuhan simpan criteria
            sessHelp.SetSession("BuletinSortColl", ViewState("currSortColumn"))
            sessHelp.SetSession("BuletinSortDir", ViewState("currSortDirection"))
            sessHelp.SetSession("BuletinPageIndex", pageIndex)
            sessHelp.SetSession("BuletinList", BuletinList)
        Else
            '-- Display datagrid header only
            dtgBuletin.DataSource = New ArrayList
            dtgBuletin.VirtualItemCount = 0
            dtgBuletin.CurrentPageIndex = 0
            dtgBuletin.DataKeyField = "ID"
            dtgBuletin.DataBind()
        End If

    End Sub

    Private Sub SortListControl(ByRef pCompletelist As ArrayList, ByVal SortColumn As String, _
                                ByVal SortDirection As Integer)
        '-- Sort arraylist

        If SortColumn.Trim <> "" Then
            Dim isASC As Boolean = (SortDirection = Sort.SortDirection.ASC)  '-- Is sorted ascending?
            Dim objListComparer As IComparer = New ListComparer(isASC, SortColumn)
            pCompletelist.Sort(objListComparer)
        End If

    End Sub

    Private Sub DeleteDataFromGrid()
        '-- Loop into datagrid item to check wether the checkbox each row is checked or not
        Dim strNo As String = String.Empty
        For Each item As DataGridItem In dtgBuletin.Items
            Dim IsChecked As Boolean = CType(item.FindControl("chkTick"), CheckBox).Checked
            If IsChecked Then
                Try
                    '-- if checked then delete

                    DeleteBuletin(CInt(dtgBuletin.DataKeys.Item(item.ItemIndex)))
                Catch
                    Dim iNo As Integer = (dtgBuletin.CurrentPageIndex * dtgBuletin.PageSize + item.ItemIndex + 1)
                    If strNo = String.Empty Then
                        strNo = iNo.ToString()
                    Else
                        strNo = strNo + ", " + iNo.ToString()
                    End If
                End Try
            End If
        Next

        If strNo <> String.Empty Then
            MessageBox.Show("Data pada nomor " & strNo & " sudah ada yang download dan tidak dapat dihapus.")
        End If

        ReadCriteria()   '-- Read selection criteria
        ReadData()       '-- Read all data matching criteria
        BindPage(0)      '-- Bind page-1
    End Sub

    Private Sub DeleteBuletin(ByVal nID As Integer)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        If imp.Start() Then
            '-- Delete buletin

            ' set main folder
            'Dim sMainPhyFolderPath As String = Server.MapPath("../" + _
            '    Replace(KTB.DNet.Lib.WebConfig.GetValue("BuletinDestFileDirectory"), "\", "/"))
            Dim sMainPhyFolderPaths() As String = Split(KTB.DNet.Lib.WebConfig.GetValue("BuletinDirectory"), ";")

            ' get the object based on selected id
            Dim oBuletin As Buletin = New BuletinFacade(User).Retrieve(nID)
            If (oBuletin.MemberRead > 0) Then

                imp.StopImpersonate()
                imp = Nothing
                Throw New Exception("Member lebih dari 0")
            Else

                Dim nResult = New BuletinFacade(User).Delete(oBuletin, sMainPhyFolderPaths)
                imp.StopImpersonate()
            End If
            '-- This will delete the data in database and also the file too if exist
            'Dim nResult = New BuletinFacade(User).DeleteFromDB(oBuletin, sMainPhyFolderPath)
            'Dim nResult = New BuletinFacade(User).DeleteFromDB(oBuletin, sMainPhyFolderPaths)

        End If
        imp = Nothing
    End Sub

    Private Sub UserPrivilege()
        '-- Set user privileges

        '-- Get the session
        If Not IsNothing(sessHelp.GetSession("DEALER")) Then
            Dim objDealer As Dealer = CType(sessHelp.GetSession("DEALER"), Dealer)

            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                '-- As KTB user
                'If Not SecurityProvider.Authorize(Context.User, SR.AdminViewListOfUserKTB_Privilege) Then
                If Not SecurityProvider.Authorize(Context.User, SR.BuletinManage_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=BULETIN - Pengelolaan Buletin")
                End If
                Return

            End If
        End If

        '-- If not match then sent to Access Denied page.
        Server.Transfer("../FrmAccessDenied.aspx?modulName=BULETIN - Pengelolaan Buletin")
    End Sub

    Protected Function setPeriods(ByVal oYear As Object, ByVal oMonth As Object) As String
        '-- To set display of periods in datagrid
        Dim Retval As String = ""
        If Not IsNothing(oMonth) Then
            Retval = MonthName(CInt(oMonth))
        End If
        If Not IsNothing(oYear) Then
            Retval = Retval + " " + oYear.ToString()
        End If
        Return Retval
    End Function

    Protected Function setCategoryName(ByVal oCategoryID As Object) As String
        Dim Retval As String = ""

        ' check wether the id is entered in db
        If Not IsNothing(oCategoryID) Then
            Dim nID As Integer = CInt(oCategoryID)
            Dim _BuletinFacade As BuletinFacade = New BuletinFacade(User)

            ' give separator to separate data
            ' because in display it will looks like ex SP--SP1
            ' then we give separator '--'
            Dim separator As String = "--"

            ' finally get data
            Retval = _BuletinFacade.GetHierarchicalCategoryName(oCategoryID, separator)
        End If

        Return Retval
    End Function

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If Not IsPostBack Then
            UserPrivilege()  '-- Set user privileges
            Initialize()     '-- Init
            ReadCriteria()   '-- Read selection criteria

            Dim qs As String = Request.QueryString("isBack")
            If Not qs = "true" Then
                viewstate.Add("FromPrev", 0)
                ReadData()       '-- Read all data matching criteria
                BindPage(0)      '-- Bind page-1
            Else
                viewstate.Add("FromPrev", 1)
                'sessHelp.SetSession("BuletinSortColl", "Periode")
                'sessHelp.SetSession("BuletinSortDir", Sort.SortDirection.ASC)
                ReadData()
                BindPage(0)
            End If

        End If
    End Sub

    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        If ddlCategory.Items.Count > 0 Then
            Dim iSelect As Integer = CInt(ddlCategory.SelectedValue)
            '-- Bind dropdownlist sub category
            PopulateSelectionCategory(iSelect)
        Else
            'MessageBox.Show(SR.DataNotFound("Data"))
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        '-- Search Buletin
        viewstate.Add("FromPrev", 0)
        SaveCriteria()  '-- Save selection criteria
        ReadData()      '-- Read all data matching criteria
        BindPage(0)     '-- Bind page-1
    End Sub

    Private Sub dtgBuletin_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgBuletin.ItemDataBound
        '-- Handles data binding

        If e.Item.ItemIndex <> -1 Then

            'Dim obf As BuletinFacade = New BuletinFacade(User)
            'Dim objDomain As Buletin = New BuletinFacade(User).Retrieve(CInt(dtgBuletin.DataKeys.Item(e.Item.ItemIndex)))
            ' get the filename from db
            'Dim sFileName As String = objDomain.FileName.Trim()

            ' correct the path
            'Dim sPhysicalMainFolderPath As String = Server.MapPath("../" + _
            'Replace(KTB.DNet.Lib.WebConfig.GetValue("BuletinDestFileDirectory"), "\", "/"))

            'If sPhysicalMainFolderPath = "" Then
            'sPhysicalMainFolderPath = App.path
            'End If
            'If Right(sPhysicalMainFolderPath, 1) <> "\" Then sPhysicalMainFolderPath = sPhysicalMainFolderPath & "\"

            ' set the folder path that the filename exist
            'Dim sFilePath As String = ""

            'If sFileName <> "" Then
            '    Dim separator As String = "\"
            '    sFilePath = obf.GetHierarchicalCategoryName(objDomain.BuletinCategory.ID, separator)
            'End If

            ' and then added the filename
            'sFilePath = sPhysicalMainFolderPath + sFilePath + "\" + sFileName

            'e.Item.Cells(1).Text = (dtgBuletin.CurrentPageIndex * dtgBuletin.PageSize + e.Item.ItemIndex + 1).ToString() + "<br>" + sPhysicalMainFolderPath + "<br>" + sFilePath '-- Column No

            e.Item.Cells(1).Text = (dtgBuletin.CurrentPageIndex * dtgBuletin.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No

            'To show / hide the button update/edit
            'Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            'lbtnEdit.Visible = SecurityProvider.Authorize(Context.User, SR.AdminUpdateUserKTB_Privilege)
        End If
    End Sub

    Private Sub dtgBuletin_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgBuletin.SortCommand
        '-- Sort datagrid rows based on a column header clicked

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

        BindPage(0)  '-- Bind page-1

    End Sub

    Private Sub dtgBuletin_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgBuletin.ItemCommand

        If e.CommandName = "Edit" Then
            '-- Get the unique ID
            Dim iID As Integer = CInt(dtgBuletin.DataKeys.Item(e.Item.ItemIndex))

            '-- Go to Edit Buletin
            Server.Transfer("FrmBuletinUpload.aspx?id=" & iID.ToString() & "&isSelf=1&backurl=FrmBuletinManage.aspx")

            'untuk kebutuhan perubahan button hanya dari edit saja
            sessHelp.SetSession("BulletinMode", "Edit")
        ElseIf e.CommandName = "Delete" Then
            '-- Get the unique ID
            Dim iID As Integer = CInt(dtgBuletin.DataKeys.Item(e.Item.ItemIndex))

            '-- Delete Data
            DeleteBuletin(iID)

            ReadCriteria()   '-- Read selection criteria
            ReadData()       '-- Read all data matching criteria
            BindPage(0)      '-- Bind page-1
        End If

    End Sub

    Private Sub dtgBuletin_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgBuletin.PageIndexChanged
        '-- Change datagrid page
        BindPage(e.NewPageIndex)
    End Sub

    Private Sub btnDelete1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete1.Click
        DeleteDataFromGrid()
    End Sub

    Private Sub btnDelete2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete2.Click
        DeleteDataFromGrid()
    End Sub

#End Region

End Class
