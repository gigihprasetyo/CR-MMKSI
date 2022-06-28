#Region " Summary "
'-------------------------------------------------'
'-- Program Code : FrmVehicleColor.aspx         --'
'-- Program Name : UMUM-Daftar Warna Kendaraan  --'
'-- Description  :                              --'
'-------------------------------------------------'
'-- Programmer   : Agus Pirnadi                 --'
'-- Start Date   : Sep 27 2005                  --'
'-- Update By    :                              --'
'-- Last Update  : Feb 27 2006                  --'
'-------------------------------------------------'
'-- Copyright © 2005 by Intimedia               --'
'-------------------------------------------------'
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Parser
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System.IO
Imports System.Text
Imports KTB.DNet.BusinessFacade

#End Region

Public Class FrmVehicleColor
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgCategory As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents btnStore As System.Web.UI.WebControls.Button
    Protected WithEvents File1 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents DataFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents ddlType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgVehicleColor As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgColorUpload As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnDnLoad As System.Web.UI.WebControls.Button
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
    Private blnUploadColorList_Privilege As Boolean = False
#End Region

#Region " Custom Method "

    Private Sub BindDropDownList()

        '-- Bind Status dropdownlist
        ddlStatus.Items.Clear()
        Dim Activeitem As New ListItem("Aktif", "")
        ddlStatus.Items.Add(Activeitem)
        Dim Deleteitem As New ListItem("Non-Aktif", "X")
        ddlStatus.Items.Add(Deleteitem)

        '-- Category criteria & sort
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code

        '-- Bind Category dropdownlist
        ddlCategory.DataSource = New CategoryFacade(User).RetrieveByCriteria(criterias, sortColl)
        ddlCategory.DataTextField = "CategoryCode"
        ddlCategory.DataValueField = "CategoryCode"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item

        ddlType.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item

    End Sub

    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
        BindVehicleType(True)
    End Sub

    Private Sub UnBindSearchGrid()
        dgVehicleColor.DataSource = Nothing
        dgVehicleColor.Visible = False
    End Sub

    Private Sub UnBindUpload()
        btnStore.Enabled = False  '-- Disable <Simpan> button
        dgColorUpload.DataSource = Nothing
        dgColorUpload.Visible = False
    End Sub

    Private Sub BindUpload()

        btnStore.Enabled = False   '-- Disable <Simpan> button
        btnDnLoad.Enabled = False  '-- Disable <Download> button
        dgColorUpload.Visible = True

        If (Not DataFile.PostedFile Is Nothing) And (DataFile.PostedFile.ContentLength > 0) Then
            Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName)   '-- Source file name
            Dim TempFile As String = Server.MapPath("") & "\..\DataTemp\" & SrcFile  '-- Temp file

            '-- Impersonation to manipulate file in server
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

            Try
                If imp.Start() Then

                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(DataFile.PostedFile.InputStream, TempFile)  '-- Copy as temp file

                    imp.StopImpersonate()
                    imp = Nothing

                    Dim parser As IParser = New VehicleColorParser  '-- Declare parser Vehicle color

                    '-- Parse temp file and store result into list
                    Dim arList As ArrayList = CType(parser.ParseNoTransaction(TempFile, "User"), ArrayList)

                    '-- Check errors if any
                    Dim bError As Boolean = False
                    For Each vicColor As VechileColor In arList
                        If Not vicColor.ErrorMessage = String.Empty Then
                            bError = True
                            Exit For
                        End If
                    Next

                    If Not bError Then
                        btnStore.Enabled = True '-- Enable <Simpan> button
                        sessHelp.SetSession("vicColor", arList)  '-- Store arList into session
                    End If

                    '-- Bind list to datagrid
                    dgColorUpload.DataSource = arList
                    dgColorUpload.DataBind()

                End If

            Catch Exc As Exception
                MessageBox.Show(SR.UploadFail(SrcFile))
            End Try
        Else
            MessageBox.Show(SR.FileNotSelected)
        End If

    End Sub

    Private Function WriteColorData(ByRef sw As StreamWriter)

        '-- Retrieve vehicle color list from session
        Dim ColorList As ArrayList = CType(sessHelp.GetSession("ColorList"), ArrayList)

        Dim colorLine As StringBuilder = New StringBuilder  '-- Color line in text file

        For Each objVecColor As VechileColor In ColorList
            colorLine.Remove(0, colorLine.Length)  '-- Empty color line

            colorLine.Append(objVecColor.Status & ";")          '-- Status: blank=Active, X=Deleted
            colorLine.Append(objVecColor.SpecialFlag & ";")     '-- Warna khusus
            colorLine.Append(objVecColor.MaterialNumber & ";")  '-- Material number
            colorLine.Append(objVecColor.MarketCode & ";")      '-- Market code
            colorLine.Append(objVecColor.HeaderBOM & ";")       '-- Header BOM
            colorLine.Append(objVecColor.ColorEngName & ";")    '-- B.Inggris
            colorLine.Append(objVecColor.ColorIndName & ";")    '-- B.Indonesia
            colorLine.Append(objVecColor.MaterialDescription)   '-- Description

            sw.WriteLine(colorLine.ToString())  '-- Write color line into text file
        Next

    End Function

    Private Sub BindSearchGrid()
        ReadData()   '-- Read all data matching criteria
        BindPage(0)  '-- Bind page-1
    End Sub

    Private Function IsExistCode(ByVal nTypeID As Integer, ByVal sCode As String) As Boolean
        '-- Return the existence of vehicle color in table

        Dim objVechileColorFacade As VechileColorFacade = New VechileColorFacade(User)
        Return objVechileColorFacade.ValidateCode(nTypeID, sCode) > 0
    End Function

    Private Sub InsertVechileColor(ByVal vicColor As VechileColor)
        '-- Insert vehicle color

        Dim objVechileColorFacade As VechileColorFacade = New VechileColorFacade(User)
        vicColor.VechileType = New VechileTypeFacade(User).Retrieve(vicColor.VechileType.ID)
        objVechileColorFacade.Insert(vicColor)
    End Sub

    Private Sub UpdateVechileColor(ByVal vicColor As VechileColor)
        '-- Update vehicle color

        Dim objVechileColorFacade As VechileColorFacade = New VechileColorFacade(User)
        vicColor.VechileType = New VechileTypeFacade(User).Retrieve(vicColor.VechileType.ID)
        vicColor.ID = New VechileColorFacade(User).Retrieve(vicColor.VechileType.ID, vicColor.ColorCode).ID
        objVechileColorFacade.Update(vicColor)
    End Sub

    Private Sub DeleteVehicleColor(ByVal nID As Integer)
        '-- Delete vehicle color

        Dim objVehicleColor As VechileColor = New VechileColorFacade(User).Retrieve(nID)
        objVehicleColor.Status = "X"
        Dim nResult = New VechileColorFacade(User).Update(objVehicleColor)
    End Sub

    Private Sub ActivateVehicleColor(ByVal nID As Integer)
        '-- Activate vehicle color

        Dim objVehicleColor As VechileColor = New VechileColorFacade(User).Retrieve(nID)
        objVehicleColor.Status = ""
        Dim nResult = New VechileColorFacade(User).Update(objVehicleColor)
    End Sub

    Private Sub ReadData()
        '-- Read all data selected

        dgVehicleColor.Visible = True
        btnDnLoad.Enabled = False  '-- Init: Disable <Download> button

        '-- Search criteria:

        '-- Row status
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '-- Status
        criterias.opAnd(New Criteria(GetType(VechileColor), "Status", MatchType.Exact, ddlStatus.SelectedValue))

        If ddlCategory.SelectedValue <> "" Then  '-- Category
            criterias.opAnd(New Criteria(GetType(VechileColor), "VechileType.Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))
        End If
        If ddlSubCategory.SelectedValue <> "-1" Then
            'CommonFunction.SetVehicleSubCategoryCriterias(ddlSubCategory, ddlCategory.SelectedItem.Text, criterias, "VechileColor")

            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlSubCategory.SelectedValue
            criterias.opAnd(New Criteria(GetType(VechileColor), "VechileType.VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))
        End If
        If ddlType.SelectedValue <> "" Then  '-- Vechile type
            criterias.opAnd(New Criteria(GetType(VechileColor), "VechileType.VechileTypeCode", MatchType.Exact, ddlType.SelectedValue))
        End If
        '-- Type still active
        criterias.opAnd(New Criteria(GetType(VechileColor), "VechileType.Status", MatchType.No, "X"))

        '-- Color code never have value of 'zzzz'
        criterias.opAnd(New Criteria(GetType(VechileColor), "ColorCode", MatchType.No, "zzzz"))

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(VechileColor), "VechileType.Category.CategoryCode", Sort.SortDirection.ASC))
        sortColl.Add(New Sort(GetType(VechileColor), "VechileType.VechileTypeCode", Sort.SortDirection.ASC))
        sortColl.Add(New Sort(GetType(VechileColor), "ColorCode", Sort.SortDirection.ASC))

        '-- Retrieve color list
        Dim ColorList As ArrayList = New VechileColorFacade(User).RetrieveByCriteria(criterias, sortColl)

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
            Dim PagedList As ArrayList = ArrayListPager.DoPage(ColorList, pageIndex, dgVehicleColor.PageSize)
            dgVehicleColor.DataSource = PagedList
            dgVehicleColor.VirtualItemCount = ColorList.Count
            dgVehicleColor.CurrentPageIndex = pageIndex
            dgVehicleColor.DataBind()
        Else
            '-- Display datagrid header only
            dgVehicleColor.DataSource = New ArrayList
            dgVehicleColor.VirtualItemCount = 0
            dgVehicleColor.CurrentPageIndex = 0
            dgVehicleColor.DataBind()
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

        ''DataFile.Visible = SecurityProvider.Authorize(Context.User, SR.)
        blnUploadColorList_Privilege = SecurityProvider.Authorize(Context.User, SR.UploadColorList_Privilege)
        btnUpload.Visible = blnUploadColorList_Privilege
        ''btnSave.Visible = SecurityProvider.Authorize(Context.User, SR.)
        btnDnLoad.Visible = SecurityProvider.Authorize(Context.User, SR.DownloadColorList_Privilege)
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
            If Not SecurityProvider.Authorize(Context.User, SR.ViewColorList_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Umum-Daftar Warna Kendaraan")
            End If

            BindDropDownList()  '-- Bind dropdownlists at first time
            CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)

            '-- Init sorting column and sort direction default
            ViewState("currSortColumn") = ""
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            '-- Display grid column headers
            dgVehicleColor.DataSource = New ArrayList
            dgVehicleColor.DataBind()
        End If

        ActivateUserPrivilege()  '-- Assign privileges
    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        '-- Upload data from text file

        If Not Page.IsValid Then  '-- Postback validation
            Exit Sub
        End If

        UnBindSearchGrid()
        BindUpload()

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        UnBindUpload()    '-- Unbind & hide Upload Color datagrid
        BindSearchGrid()  '-- Bind Color datagrid
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

                '-- Close stream & file
                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download color data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\ColorData" & sSuffix & ".txt")

        Catch ex As Exception
            Dim errMess As String = ex.Message
        End Try

    End Sub

    Private Sub dgVehicleColor_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgVehicleColor.SortCommand
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

    Private Sub dgVehicleColor_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgVehicleColor.PageIndexChanged
        '-- Change datagrid page
        BindPage(e.NewPageIndex)
    End Sub

    Private Sub btnStore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStore.Click
        '-- Insert or update vehicle color one-by-one

        '-- Retrieve arList from session
        Dim arList As ArrayList = CType(sessHelp.GetSession("vicColor"), ArrayList)

        For Each vicColor As VechileColor In arList
            If Not IsExistCode(vicColor.VechileType.ID, vicColor.ColorCode) Then
                InsertVechileColor(vicColor)  '-- Insert vehicle color
            Else
                UpdateVechileColor(vicColor)  '-- Update vehicle color
            End If
        Next

        btnStore.Enabled = False  '-- Disable <Simpan> button
        MessageBox.Show(SR.SaveSuccess)
    End Sub

    Private Sub dgVehicleColor_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgVehicleColor.ItemDataBound

        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgVehicleColor.CurrentPageIndex * dgVehicleColor.PageSize)
        End If

        '-- Handle LinkButton appearance
        Dim RowValue As VechileColor = CType(e.Item.DataItem, VechileColor)
        Try
            If RowValue.Status = "" Then  '-- Active
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = blnUploadColorList_Privilege
                CType(e.Item.FindControl("lbtnActive"), LinkButton).Visible = False
            ElseIf RowValue.Status = "X" Then  '-- Deleted
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = False
                CType(e.Item.FindControl("lbtnActive"), LinkButton).Visible = blnUploadColorList_Privilege
            End If
        Catch ex As Exception
        End Try

        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            '-- Confirm deletion
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", _
                New CommonFunction().PreventDoubleClickAtGrid(CType(e.Item.FindControl("lbtnDelete"), LinkButton), "Hapus record ini?"))
        End If
        If Not e.Item.FindControl("lbtnActive") Is Nothing Then
            '-- Confirm activation
            CType(e.Item.FindControl("lbtnActive"), LinkButton).Attributes.Add("OnClick", "return confirm('Aktifkan record ini?');")
        End If

    End Sub

    Private Sub dgVehicleColor_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgVehicleColor.ItemCommand
        If e.CommandName = "Delete" Then
            DeleteVehicleColor(e.Item.Cells(0).Text)  '-- Delete vehicle color
            BindSearchGrid()  '-- Bind Color datagrid
        ElseIf e.CommandName = "Active" Then
            ActivateVehicleColor(e.Item.Cells(0).Text)  '-- Activate vehicle color
            BindSearchGrid()  '-- Bind Color datagrid
        End If

    End Sub

    Private Sub dgColorUpload_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgColorUpload.ItemDataBound
        '-- Handle template databinding on dgColorUpload

        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgColorUpload.CurrentPageIndex * dgColorUpload.PageSize)
            '-- Convert DataItem to VechileColor object
            Dim RowValue As VechileColor = CType(e.Item.DataItem, VechileColor)

            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                '-- Locate position
                Dim lblCategoryCode As Label = CType(e.Item.FindControl("lblCategoryCode"), Label)
                Dim lblVehicleType As Label = CType(e.Item.FindControl("lblVehicleType"), Label)
                Try
                    lblCategoryCode.Text = RowValue.VechileType.Category.CategoryCode  '-- Kategori
                    lblVehicleType.Text = RowValue.VechileType.VechileTypeCode  '-- Kode tipe

                Catch ex As Exception
                    '-- If their objects is nothing then set with empty string
                    lblCategoryCode.Text = ""
                    lblVehicleType.Text = ""
                End Try

            End If
        End If
    End Sub

    Private Sub ddlSubCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubCategory.SelectedIndexChanged
        BindVehicleType(False)
    End Sub

#End Region
End Class
