#Region " Summary "
'--------------------------------------------------------'
'-- Program Code : FrmVehicleColorInformation.aspx     --'
'-- Program Name : UMUM-Informasi Warna Kendaraan      --'
'-- Description  :                                     --'
'--------------------------------------------------------'
'-- Programmer   : Agus Pirnadi                        --'
'-- Start Date   : Oct 10 2005                         --'
'-- Update By    :                                     --'
'-- Last Update  : Feb 27 2006                         --'
'--------------------------------------------------------'
'-- Copyright © 2005 by Intimedia                      --'
'--------------------------------------------------------'
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Domain.Search
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System.IO
Imports System.Text
#End Region

Public Class FrmVehicleColorInformation
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblType As System.Web.UI.WebControls.Label
    Protected WithEvents ddlType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblCategory As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnDnLoad As System.Web.UI.WebControls.Button
    Protected WithEvents dgColor As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents ddlSubCategory As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Private Variables "
    Private sessHelp As SessionHelper = New SessionHelper
#End Region

#Region " Custom Method "

    Private Sub bindDataDropList()
        '-- Fill in dropdownlist control

        '-- Category criteria & sort
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code

        '-- Bind Category dropdownlist
        ddlCategory.DataSource = New CategoryFacade(User).RetrieveByCriteria(criterias, sortColl)
        ddlCategory.DataValueField = "CategoryCode"
        ddlCategory.DataTextField = "CategoryCode"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Pilih", ""))

        ddlType.Items.Insert(0, New ListItem("Pilih", ""))

    End Sub

    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
        BindVehicleType(True)
    End Sub

    Private Function WriteColorData(ByRef sw As StreamWriter)

        '-- Retrieve vehicle color list from session
        Dim ColorList As ArrayList = CType(sessHelp.GetSession("ColorList"), ArrayList)

        Dim ColorLine As StringBuilder = New StringBuilder  '-- Color line in text file

        For Each objVecColor As VechileColor In ColorList
            ColorLine.Remove(0, ColorLine.Length)  '-- Empty price line

            ColorLine.Append(objVecColor.VechileType.Category.CategoryCode.ToString() & ";")  '-- Kode kategori
            ColorLine.Append(objVecColor.VechileType.VechileTypeCode.ToString() & ";")        '-- Kode tipe
            ColorLine.Append(objVecColor.ColorCode.ToString() & ";")     '-- Warna kendaraan
            ColorLine.Append(objVecColor.VechileType.Description.ToString() & ";")  '-- Name tipe
            ColorLine.Append(objVecColor.ColorEngName.ToString() & ";")  '-- Bhs. Inggris
            ColorLine.Append(objVecColor.ColorIndName.ToString())        '-- Bhs. Indonesia

            sw.WriteLine(ColorLine.ToString())  '-- Write color line
        Next

    End Function

    Private Sub ReadData()
        '-- Read all data selected

        btnDnLoad.Enabled = False  '-- Init: Disable <Download> button

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If ddlCategory.SelectedValue <> "" Then
            criterias.opAnd(New Criteria(GetType(VechileColor), "VechileType.Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))
        End If
        If ddlSubCategory.SelectedValue <> "-1" Then
            'CommonFunction.SetVehicleSubCategoryCriterias(ddlSubCategory, ddlCategory.SelectedItem.Text, criterias, "VechileColor")

            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlSubCategory.SelectedValue
            criterias.opAnd(New Criteria(GetType(VechileColor), "VechileType.VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))
        End If
        If ddlType.SelectedValue <> "" Then
            criterias.opAnd(New Criteria(GetType(VechileColor), "VechileType.VechileTypeCode", MatchType.Exact, ddlType.SelectedValue))
        End If

        '-- Type still active
        criterias.opAnd(New Criteria(GetType(VechileColor), "VechileType.Status", MatchType.No, "X"))

        '-- SpecialFlag <> 'X'
        criterias.opAnd(New Criteria(GetType(VechileColor), "SpecialFlag", MatchType.No, "X"))

        '-- Status <> 'X'
        criterias.opAnd(New Criteria(GetType(VechileColor), "Status", MatchType.No, "X"))

        '-- Color code never have value of 'zzzz'
        criterias.opAnd(New Criteria(GetType(VechileColor), "ColorCode", MatchType.No, "zzzz"))

        Dim ColorList As ArrayList = New VechileColorFacade(User).Retrieve(criterias)

        '-- Store vehicle color list into session for later use by Download
        sessHelp.SetSession("ColorList", ColorList)

        If ColorList.Count > 0 Then
            btnDnLoad.Enabled = True  '-- Enable <Download> button
        Else
            MessageBox.Show(SR.DataNotFound("Data"))
        End If

    End Sub

    Private Sub BindPage(ByVal pageIndex As Integer)
        '-- Bind page-i

        '-- Read ColorList from session
        Dim ColorList As ArrayList = CType(sessHelp.GetSession("ColorList"), ArrayList)

        If Not IsNothing(ColorList) AndAlso ColorList.Count <> 0 Then
            Try
                '-- Sort first
                SortListControl(ColorList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Catch ex As Exception
            End Try
            '-- Then paging
            Dim PagedList As ArrayList = ArrayListPager.DoPage(ColorList, pageIndex, dgColor.PageSize)
            dgColor.DataSource = PagedList
            dgColor.VirtualItemCount = ColorList.Count
            dgColor.CurrentPageIndex = pageIndex
            dgColor.DataBind()
        Else
            '-- Display datagrid header only
            dgColor.DataSource = New ArrayList
            dgColor.VirtualItemCount = 0
            dgColor.CurrentPageIndex = 0
            dgColor.DataBind()
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

    Private Sub ActivateUserPrivilege()
        '-- Assign privileges

        ''btnSearch.Visible = SecurityProvider.Authorize(Context.User, SR.)
        btnDnLoad.Visible = SecurityProvider.Authorize(Context.User, SR.DownloadColorInfo_Privilege)
    End Sub
    Private Sub BindVehicleType(ByVal IsClearAll As Boolean)
        ddlType.Items.Clear()
        If ddlSubCategory.SelectedValue <> "-1" And Not IsClearAll Then

            '-- Vehicle criteria & sort
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileType), "Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))
            criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.No, "X"))  '-- Type still active
            'CommonFunction.SetVehicleSubCategoryCriterias(ddlSubCategory, ddlCategory.SelectedItem.Text, criterias, "VechileType")

            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlSubCategory.SelectedValue
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))  '-- Sort by Vechile type code

            '-- Bind Vehicle type dropdownlist
            ddlType.DataSource = New VechileTypeFacade(User).RetrieveByCriteria(criterias, sortColl)
            ddlType.DataTextField = "VechileTypeCode"
            ddlType.DataValueField = "VechileTypeCode"
            ddlType.DataBind()
        End If
        ddlType.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item
    End Sub
#End Region

#Region " EventHandler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not IsPostBack Then
            If Not SecurityProvider.Authorize(Context.User, SR.ViewColorInfo_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Umum-Informasi Warna Kendaraan")
            End If

            bindDataDropList()
            CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)

            '-- Init sorting column and sort direction default
            ViewState("currSortColumn") = ""
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            '-- Display grid column headers
            dgColor.DataSource = New ArrayList
            dgColor.DataBind()
        End If

        ActivateUserPrivilege()  '-- Assign privileges
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        '-- Search records

        ReadData()   '-- Read all data matching criteria
        BindPage(0)  '-- Bind page-1
    End Sub

    Private Sub btnDnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDnLoad.Click
        '-- Download data in datagrid to text file

        '-- Generate timestamp
        Dim dt As DateTime = DateTime.Now
        Dim sSuffix As String = CType(dt.Year, String) & CType(dt.Month, String) & _
                                CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & _
                                CType(dt.Second, String) & CType(dt.Millisecond, String)

        '-- Temp file must be a randomly named text file!
        Dim ColorData As String = Server.MapPath("") & "\..\DataTemp\ColorData" & sSuffix & ".txt"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(ColorData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(ColorData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteColorData(sw)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download price data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\ColorData" & sSuffix & ".txt")

        Catch ex As Exception
            Dim errMess As String = ex.Message
        End Try

    End Sub

    Private Sub dgColor_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgColor.SortCommand
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

        BindPage(0)  '-- Display page-1

    End Sub

    Private Sub dgColor_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgColor.PageIndexChanged
        '-- Change datagrid page
        BindPage(e.NewPageIndex)
    End Sub

    Private Sub dgColor_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgColor.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgColor.CurrentPageIndex * dgColor.PageSize)
        End If
    End Sub

    Private Sub ddlSubCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubCategory.SelectedIndexChanged
        BindVehicleType(False)
    End Sub

#End Region

End Class
