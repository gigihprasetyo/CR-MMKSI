#Region " Summary "
'--------------------------------------------'
'-- Program Code : FrmPrice.aspx           --'
'-- Program Name : Daftar Harga Kendaraan  --'
'-- Description  :                         --'
'--------------------------------------------'
'-- Programmer   : Agus Pirnadi            --'
'-- Start Date   : Nov 01 2005             --'
'-- Update By    :                         --'
'-- Last Update  : Feb 27 2006             --'
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
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade

#End Region

Public Class FrmPrice
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
    Protected WithEvents dgPriceUpload As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgPrice As System.Web.UI.WebControls.DataGrid
    Protected WithEvents calCalendar As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents chkAll As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnDnLoad As System.Web.UI.WebControls.Button
    Protected WithEvents btnDnLoadtoExcel As System.Web.UI.WebControls.Button
    Protected WithEvents ddlSubCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Custom Variable Declaration "
    Private sessHelp As SessionHelper = New SessionHelper
    Dim sHelper As New SessionHelper
    Dim blnUploadPriceList_Privilege As Boolean = False
#End Region

#Region " Custom Method "

    Private Sub BindDropDownList()

        '-- Bind Status dropdownlist
        ddlStatus.Items.Clear()
        Dim Activeitem As New ListItem("Active", CType(DBRowStatus.Active, Short))
        ddlStatus.Items.Add(Activeitem)
        Dim Deleteitem As New ListItem("Delete", CType(DBRowStatus.Deleted, Short))
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
        dgPrice.DataSource = Nothing
        dgPrice.Visible = False
    End Sub

    Private Sub UnBindUpload()
        btnStore.Enabled = False  '-- Disable <Simpan> button
        dgPriceUpload.DataSource = Nothing
        dgPriceUpload.Visible = False
    End Sub

    Private Sub BindUpload()

        btnStore.Enabled = False   '-- Init. Disable <Simpan> button
        btnDnLoad.Enabled = False  '-- Disable <Download> button
        btnDnLoadtoExcel.Enabled = False  '-- Disable <Download> button
        dgPriceUpload.Visible = True
        dgPriceUpload.DataSource = New ArrayList
        dgPriceUpload.DataBind()

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
                    objUpload.Upload(DataFile.PostedFile.InputStream, TempFile)

                    imp.StopImpersonate()
                    imp = Nothing

                    Dim parser As IParser = New PriceParser  '-- Declare parser Price

                    '-- Parse data file and store result into list
                    Dim arList As ArrayList = CType(parser.ParseNoTransaction(TempFile, "User"), ArrayList)

                    '-- Check errors if any
                    Dim bError As Boolean = False
                    For Each vicPrice As Price In arList
                        If Not vicPrice.ErrorMessage = String.Empty Then
                            bError = True
                            Exit For
                        End If
                    Next

                    If Not bError Then
                        btnStore.Enabled = True '-- Enable <Simpan> button
                        sessHelp.SetSession("vicPrice", arList)  '-- Store arList into session
                    End If

                    '-- Bind list to datagrid
                    dgPriceUpload.DataSource = arList
                    dgPriceUpload.DataBind()

                End If

            Catch Exc As Exception
                MessageBox.Show(SR.UploadFail(SrcFile))
            End Try
        Else
            MessageBox.Show(SR.FileNotSelected)
        End If

    End Sub

    Private Sub BindSearchGrid(Optional ByVal IDx As Integer = 0)
        dgPrice.CurrentPageIndex = IDx
        ReadData(dgPrice.CurrentPageIndex)   '-- Read all data matching criteria
        ' BindPage(0)  '-- Bind page-1
    End Sub

    Private Sub CreateCriterias()
        '-- Compose search criteria
        Dim criterias As CriteriaComposite

        '-- Status
        criterias = New CriteriaComposite(New Criteria(GetType(V_Price), "RowStatus", MatchType.Exact, ddlStatus.SelectedValue))

        If ddlCategory.SelectedValue <> "" Then  '-- Category
            criterias.opAnd(New Criteria(GetType(V_Price), "VechileColor.VechileType.Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))
        End If
        If ddlCategory.SelectedValue <> "" AndAlso ddlSubCategory.SelectedValue <> "-1" Then
            'CommonFunction.SetVehicleSubCategoryCriterias(ddlSubCategory, ddlCategory.SelectedItem.Text, criterias, "v_Price")
            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlSubCategory.SelectedValue
            criterias.opAnd(New Criteria(GetType(V_Price), "VechileColor.VechileType.VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))
        End If
        If ddlType.SelectedValue <> "" Then  '-- Vechile type
            criterias.opAnd(New Criteria(GetType(V_Price), "VechileColor.VechileType.VechileTypeCode", MatchType.Exact, ddlType.SelectedValue))
        End If

        '-- Type still active
        criterias.opAnd(New Criteria(GetType(V_Price), "VechileColor.VechileType.Status", MatchType.No, "X"))
        '-- Color still active
        criterias.opAnd(New Criteria(GetType(V_Price), "VechileColor.Status", MatchType.No, "X"))

        Dim Tgl As String = calCalendar.Value.AddDays(1).ToString("yyyy/MM/dd")

        If Not chkAll.Checked Then
            Dim Curr As String = " (v_Price.ID IN ( SELECT p.ID FROM ( SELECT   p.*, RN = (ROW_NUMBER() OVER( PARTITION BY  p.DealerID,  p.VechileColorID  ORDER BY  p.ValidFrom DESC)) FROM dbo.Price	p (NOLOCK) WHERE p.RowStatus={0} AND p.ValidFrom<='{1}' AND p.DealerID IS NOT NULL )p WHERE p.RN=1 )  )  AND"
            Curr = String.Format(Curr, ddlStatus.SelectedValue.ToString(), Tgl)
            criterias.opAnd(New Criteria(GetType(V_Price), "ValidFrom", MatchType.Lesser, calCalendar.Value.AddDays(1)), Curr, True)
        End If

        If Me.txtKodeDealer.Text.Trim <> "" Then
            Dim sCodes As String = Me.txtKodeDealer.Text.Trim()
            sCodes = "'" & sCodes.Replace(";", "','") & "'"
            criterias.opAnd(New Criteria(GetType(V_Price), "Dealer.DealerCode", MatchType.InSet, "(" & sCodes & ")"))
        End If

        sessHelp.SetSession("CriteaPriceList", criterias)
    End Sub

    Private Function bExistIn(ByVal objPrice As Price, ByVal PriceList As ArrayList) As Boolean
        For Each objSourcePrice As Price In PriceList
            If objPrice.VechileColor.ID = objSourcePrice.VechileColor.ID AndAlso objPrice.Dealer.ID = objSourcePrice.Dealer.ID Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function IsExistCode(ByVal nTypeID As Integer, ByVal dValidFrom As DateTime, ByVal DealerID As Integer) As Boolean

        '-- Return the existence of price in table
        Return New PriceFacade(User).ValidateCode(nTypeID, dValidFrom, DealerID) > 0

    End Function

    Private Sub InsertPrice(ByVal vicPrice As Price)
        '-- Insert price

        vicPrice.VechileColor = New VechileColorFacade(User).Retrieve(vicPrice.VechileColor.ID)
        Dim objPriceFacade As PriceFacade = New PriceFacade(User)
        objPriceFacade.Insert(vicPrice)
    End Sub

    Private Sub UpdatePrice(ByVal vicPrice As Price)
        '-- Update price

        vicPrice.VechileColor = New VechileColorFacade(User).Retrieve(vicPrice.VechileColor.ID)
        vicPrice.ID = New PriceFacade(User).Retrieve(vicPrice.VechileColor.ID, vicPrice.ValidFrom, vicPrice.Dealer.ID).ID
        Dim objPriceFacade As PriceFacade = New PriceFacade(User)
        objPriceFacade.Update(vicPrice)

    End Sub

    Private Sub DeletePrice(ByVal nID As Integer)
        '-- Delete price

        Dim objPrice As Price = New PriceFacade(User).Retrieve(nID)
        objPrice.RowStatus = DBRowStatus.Deleted
        Dim nResult = New PriceFacade(User).Update(objPrice)
    End Sub

    Private Sub ActivatePrice(ByVal nID As Integer)
        '-- Activate price

        Dim objPrice As Price = New PriceFacade(User).Retrieve(nID)
        objPrice.RowStatus = DBRowStatus.Active
        Dim nResult = New PriceFacade(User).Update(objPrice)
    End Sub

    Private Function WritePriceData(ByRef sw As StreamWriter, ByVal Arr_v_price As ArrayList)

        '-- Retrieve vehicle price list from session
        'Dim PriceList As ArrayList = CType(sessHelp.GetSession("PriceList"), ArrayList)

        Dim PriceLine As StringBuilder = New StringBuilder  '-- Price line in text file

        For Each objVecPrice As V_Price In Arr_v_price
            PriceLine.Remove(0, PriceLine.Length)  '-- Empty price line
            PriceLine.Append(objVecPrice.MaterialNumber & ";")  '-- Material number
            '  PriceLine.Append(objVecPrice.VechileColor.MaterialNumber & ";")  '-- Material number
            PriceLine.Append(objVecPrice.DealerCode & ";") '-- Dealer Code
            'PriceLine.Append(objVecPrice.Dealer.DealerCode & ";") '-- Dealer Code
            PriceLine.Append(Format(objVecPrice.ValidFrom, "dd/MM/yyyy") & ";") '-- Valid from
            PriceLine.Append(Format(objVecPrice.BasePrice, "0") & ";")    '-- Base price
            PriceLine.Append(Format(objVecPrice.OptionPrice, "0") & ";")  '-- Option price
            PriceLine.Append(Format(objVecPrice.PPN_BM, "0.00") & ";")    '-- PPnBM
            PriceLine.Append(Format(objVecPrice.PPN, "0.00") & ";")       '-- PPN
            PriceLine.Append(Format(objVecPrice.PPh22, "0.00") & ";")     '-- PPh22
            PriceLine.Append(Format(objVecPrice.Interest, "0.000") & ";")  '-- Interest
            PriceLine.Append(Format(objVecPrice.FactoringInt, "0.000") & ";")   '-- Factoring Interest
            PriceLine.Append(Format(objVecPrice.PPh23, "0.00") & ";")     '-- PPh23
            PriceLine.Append(objVecPrice.Status.ToString & ";")               '-- Status


            sw.WriteLine(PriceLine.ToString())  '-- Write color line
        Next

    End Function

    Private Sub ActivateUserPrivilege()
        '-- Assign privileges

        ''DataFile.Visible = SecurityProvider.Authorize(Context.User, SR.)
        blnUploadPriceList_Privilege = SecurityProvider.Authorize(Context.User, SR.UploadPriceList_Privilege)
        btnUpload.Visible = blnUploadPriceList_Privilege
        ''btnSave.Visible = SecurityProvider.Authorize(Context.User, SR.)
        ''btnSearch.Visible = SecurityProvider.Authorize(Context.User, SR.)
        btnDnLoad.Visible = SecurityProvider.Authorize(Context.User, SR.DownLoadPriceList_Privilege)
        btnDnLoadtoExcel.Visible = SecurityProvider.Authorize(Context.User, SR.DownLoadPriceList_Privilege)
        Dim ColIdx As Integer = CommonFunction.GetColumnIndexOfDTG(Me.dgPrice, "Factoring Interest (%)")
        If ColIdx >= 0 Then Me.dgPrice.Columns(ColIdx).Visible = SecurityProvider.Authorize(Context.User, SR.Factoring_interest_harga_kendaraan_lihat_privilege)
    End Sub

    Private Sub ReadData(Optional ByVal indexPage As Integer = 0)
        '-- Read all data selected

        dgPrice.Visible = True
        btnDnLoad.Enabled = False  '-- Init: Disable <Download> button
        btnDnLoadtoExcel.Enabled = False  '-- Init: Disable <Download> button

        '-- Compose search criteria
        Dim criterias As CriteriaComposite
        criterias = CType(Session("CriteaPriceList"), CriteriaComposite)
        ''-- Status
        'criterias = New CriteriaComposite(New Criteria(GetType(Price), "RowStatus", MatchType.Exact, ddlStatus.SelectedValue))

        'If ddlCategory.SelectedValue <> "" Then  '-- Category
        '    criterias.opAnd(New Criteria(GetType(Price), "VechileColor.VechileType.Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))
        'End If
        'If ddlSubCategory.SelectedValue <> -1 Then
        '    CommonFunction.SetVehicleSubCategoryCriterias(ddlSubCategory, ddlCategory.SelectedItem.Text, criterias, "Price")
        'End If
        'If ddlType.SelectedValue <> "" Then  '-- Vechile type
        '    criterias.opAnd(New Criteria(GetType(Price), "VechileColor.VechileType.VechileTypeCode", MatchType.Exact, ddlType.SelectedValue))
        'End If

        ''-- Type still active
        'criterias.opAnd(New Criteria(GetType(Price), "VechileColor.VechileType.Status", MatchType.No, "X"))
        ''-- Color still active
        'criterias.opAnd(New Criteria(GetType(Price), "VechileColor.Status", MatchType.No, "X"))

        'If Not chkAll.Checked Then
        '    '' criterias.opAnd(New Criteria(GetType(Price), "ValidFrom", MatchType.Lesser, calCalendar.Value.AddDays(1)))
        '    criterias.opAnd(New Criteria(GetType(V_Price), "ValidFrom", MatchType.Lesser, calCalendar.Value.AddDays(1)), "( (ROW_NUMBER() OVER( PARTITION BY  Price.DealerID,  Price.VechileColorID  ORDER BY   Price.ValidFrom DESC)) = 1) AND ", True)

        'Else

        'End If
        'If Me.txtKodeDealer.Text.Trim <> "" Then
        '    Dim sCodes As String = Me.txtKodeDealer.Text.Trim()
        '    sCodes = "'" & sCodes.Replace(";", "','") & "'"
        '    criterias.opAnd(New Criteria(GetType(Price), "Dealer.DealerCode", MatchType.InSet, "(" & sCodes & ")"))
        'End If
        ''-- Sorted by
        'Dim sortColl As SortCollection = New SortCollection
        'sortColl.Add(New Sort(GetType(Price), "VechileColor.MaterialNumber", Sort.SortDirection.ASC))
        'sortColl.Add(New Sort(GetType(Price), "ValidFrom", Sort.SortDirection.DESC))

        '-- Retrieve price list
        Dim TotalRow As Integer = 0
        Dim TempList As ArrayList ' = New PriceFacade(User).RetrieveByCriteria(criterias, sortColl)
        TempList = New V_PriceFacade(User).RetrieveActiveList(criterias, indexPage + 1, dgPrice.PageSize, TotalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))

        Dim PriceList As New ArrayList  '-- Already selected price list

        If Not chkAll.Checked Then  '-- Valid from date criteria is concerned
            'For Each objPrice As Price In TempList
            '    If Not bExistIn(objPrice, PriceList) Then
            '        PriceList.Add(objPrice)
            '    End If
            'Next
            PriceList = TempList
        Else
            PriceList = TempList  '-- "All" checkbox selected
        End If

        '-- Store vehicle price list into session for later use by Download
        ' sessHelp.SetSession("PriceList", PriceList)


        'If PriceList.Count > 0 Then
        '    btnDnLoad.Enabled = True  '-- Enable <Download> button
        'Else
        '    MessageBox.Show(SR.DataNotFound("Data"))
        'End If


        If Not IsNothing(PriceList) AndAlso PriceList.Count <> 0 Then
            dgPrice.DataSource = PriceList
            dgPrice.VirtualItemCount = TotalRow
            dgPrice.DataBind()
        Else
            '-- Display datagrid header only
            dgPrice.DataSource = New ArrayList
            dgPrice.VirtualItemCount = 0
            dgPrice.CurrentPageIndex = 0
            dgPrice.DataBind()
        End If


        If PriceList.Count > 0 Then
            btnDnLoad.Enabled = True  '-- Enable <Download> button
            btnDnLoadtoExcel.Enabled = True  '-- Enable <Download> button
        Else
            MessageBox.Show(SR.DataNotFound("Data"))
        End If

    End Sub

    Private Sub BindPage(ByVal pageIndex As Integer)
        '-- Bind page-i

        '-- Read PriceList from session
        Dim PriceList As ArrayList = CType(sessHelp.GetSession("PriceList"), ArrayList)

        If Not IsNothing(PriceList) AndAlso PriceList.Count <> 0 Then
            Try
                '-- Sort first
                SortListControl(PriceList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Catch ex As Exception
            End Try
            '-- Then paging
            Dim PagedList As ArrayList = ArrayListPager.DoPage(PriceList, pageIndex, dgPrice.PageSize)
            dgPrice.DataSource = PagedList
            dgPrice.VirtualItemCount = PriceList.Count()
            dgPrice.CurrentPageIndex = pageIndex
            dgPrice.DataBind()
        Else
            '-- Display datagrid header only
            dgPrice.DataSource = New ArrayList
            dgPrice.VirtualItemCount = 0
            dgPrice.CurrentPageIndex = 0
            dgPrice.DataBind()
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

    Private Sub BindVehicleType(ByVal IsClearAll As Boolean)
        ddlType.Items.Clear()
        If ddlSubCategory.SelectedValue <> "-1" And Not IsClearAll Then
            '-- Vehicle criteria & sort
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.No, "X"))  '-- Type still active
            criterias.opAnd(New Criteria(GetType(VechileType), "Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))

            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlSubCategory.SelectedValue
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))

            'CommonFunction.SetVehicleSubCategoryCriterias(ddlSubCategory, ddlCategory.SelectedItem.Text, criterias, "VechileType")

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
        Page.Server.ScriptTimeout = 300

        If Not IsPostBack Then
            If Not SecurityProvider.Authorize(Context.User, SR.ViewPriceList_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Umum-Daftar Harga Kendaraan")
            End If

            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            BindDropDownList()  '-- Bind dropdownlists at first time
            CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)

            '-- Init sorting column and sort direction default
            ViewState("currSortColumn") = ""
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            '-- Display grid column headers
            dgPrice.DataSource = New ArrayList
            dgPrice.DataBind()
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
        UnBindUpload()
        CreateCriterias()
        BindSearchGrid()  '-- Bind grid
    End Sub

    Private Function getPrice(ByVal VCID As Integer, ByVal ValidFrom As Date, ByVal DealerID As Integer)
        Dim cP As New CriteriaComposite(New Criteria(GetType(Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim oP As New Price
        Dim aPs As ArrayList

        cP.opAnd(New Criteria(GetType(Price), "VechileColor.ID", MatchType.Exact, VCID))
        cP.opAnd(New Criteria(GetType(Price), "Dealer.ID", MatchType.Exact, DealerID))
        cP.opAnd(New Criteria(GetType(Price), "ValidFrom", MatchType.Exact, ValidFrom))
        aPs = New PriceFacade(User).Retrieve(cP)
        If aPs.Count > 0 Then
            oP = aPs(0)
        Else
            oP = New Price

        End If
        Return oP
    End Function

    Private Sub btnStore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStore.Click
        '-- Insert or update price one-by-one

        '-- Retrieve arList from session
        Dim arList As ArrayList = CType(sessHelp.GetSession("vicPrice"), ArrayList)

        'validate arList
        Dim aPrices As New ArrayList
        Dim isAll As Boolean = False
        Dim aDs As New ArrayList
        Dim cD As New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim PID As Integer

        cD.opAnd(New Criteria(GetType(Dealer), "Status", MatchType.Exact, 1))
        cD.opAnd(New Criteria(GetType(Dealer), "Title", MatchType.Exact, 0))
        cD.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.NotInSet, "'KTB','BAS','180002'"))
        '    aDs = New DealerFacade(User).Retrieve(cD)

        Dim Objsp_Price As sp_Price

        For Each vicPrice As Price In arList
            Objsp_Price = New sp_Price
            If Not IsNothing(vicPrice.Dealer) AndAlso vicPrice.Dealer.ID >= 1 Then
                Objsp_Price.DealerCode = vicPrice.Dealer.DealerCode
            Else
                Objsp_Price.DealerCode = String.Empty
            End If

            If Not IsNothing(vicPrice.VechileColor.MaterialNumber) AndAlso vicPrice.VechileColor.ID >= 1 Then
                Objsp_Price.VechileColorID = vicPrice.VechileColor.ID
            End If
            Objsp_Price.OptionPrice = vicPrice.OptionPrice
            Objsp_Price.PPh22 = vicPrice.PPh22
            Objsp_Price.PPh23 = vicPrice.PPh23
            Objsp_Price.BasePrice = vicPrice.BasePrice
            Objsp_Price.DiscountReward = vicPrice.DiscountReward
            Objsp_Price.FactoringInt = vicPrice.FactoringInt
            Objsp_Price.Interest = vicPrice.Interest
            Objsp_Price.PPN = vicPrice.PPN
            Objsp_Price.PPN_BM = vicPrice.PPN_BM
            Objsp_Price.RowStatus = vicPrice.RowStatus
            Objsp_Price.ValidFrom = vicPrice.ValidFrom
            Objsp_Price.Status = vicPrice.Status
            Objsp_Price.LastUpdateBy = User.Identity.Name
            Objsp_Price.CreatedBy = User.Identity.Name

            Dim objsp_PriceFacade As sp_PriceFacade = New sp_PriceFacade(User)
            objsp_PriceFacade.Insert(Objsp_Price)
        Next

        ''COmented 1
        'For Each vicPrice As Price In arList
        '    isAll = False
        '    If Not IsNothing(vicPrice.Dealer) AndAlso vicPrice.Dealer.ID = -1 Then
        '        isAll = True
        '    End If
        '    If isAll Then
        '        For Each oD As Dealer In aDs
        '            oD.MarkLoaded()
        '            Dim oPrice As Price = Me.getPrice(vicPrice.VechileColor.ID, vicPrice.ValidFrom, oD.ID)
        '            If Not IsNothing(oPrice) AndAlso oPrice.ID > 0 Then
        '                PID = oPrice.ID
        '                oPrice = New Price
        '                oPrice = vicPrice
        '                oPrice.ID = PID
        '                oPrice.Dealer = oD
        '            Else
        '                oPrice = vicPrice
        '                oPrice.Dealer = oD
        '                oPrice.ID = 0
        '            End If

        '            'aPrices.Add(oPrice)
        '            If Not IsExistCode(oPrice.VechileColor.ID, oPrice.ValidFrom, oPrice.Dealer.ID) Then
        '                InsertPrice(oPrice)  '-- Insert price
        '            Else
        '                UpdatePrice(oPrice)  '-- Update price
        '            End If
        '        Next
        '    Else
        '        'aPrices.Add(vicPrice)
        '        Dim oPrice = New Price
        '        oPrice = vicPrice
        '        If Not IsExistCode(oPrice.VechileColor.ID, oPrice.ValidFrom, oPrice.Dealer.ID) Then
        '            InsertPrice(oPrice)  '-- Insert price
        '        Else
        '            UpdatePrice(oPrice)  '-- Update price
        '        End If
        '    End If
        'Next
        ''END of COmented 1

        'For Each vicPrice As Price In aPrices
        '    If Not IsExistCode(vicPrice.VechileColor.ID, vicPrice.ValidFrom, vicprice.Dealer.ID) Then
        '        InsertPrice(vicPrice)  '-- Insert price
        '    Else
        '        UpdatePrice(vicPrice)  '-- Update price
        '    End If
        'Next

        btnStore.Enabled = False  '-- Disable <Simpan> button
        MessageBox.Show(SR.SaveSuccess)
    End Sub

    Private Sub dgPrice_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPrice.ItemDataBound

        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgPrice.CurrentPageIndex * dgPrice.PageSize)
        End If
        '-- Handle LinkButton appearance
        Dim RowValue As V_Price = CType(e.Item.DataItem, V_Price)
        Try
            If RowValue.RowStatus = DBRowStatus.Active Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = blnUploadPriceList_Privilege
                CType(e.Item.FindControl("lbtnActive"), LinkButton).Visible = False
            ElseIf RowValue.RowStatus = DBRowStatus.Deleted Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = False
                CType(e.Item.FindControl("lbtnActive"), LinkButton).Visible = blnUploadPriceList_Privilege
            End If
        Catch ex As Exception
        End Try

        Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
        If Not IsNothing(lblDealer) Then
            Try
                lblDealer.Text = RowValue.Dealer.DealerCode
            Catch ex As Exception
                lblDealer.Text = "[semua]"
            End Try
        End If

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

    Private Sub dgPrice_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPrice.ItemCommand

        If e.CommandName = "Delete" Then
            DeletePrice(e.Item.Cells(0).Text)  '-- Delete price
            BindSearchGrid()  '-- Refresh grid
        ElseIf e.CommandName = "Active" Then
            ActivatePrice(e.Item.Cells(0).Text)  '-- Activate price
            BindSearchGrid()  '-- Refresh grid
        End If

    End Sub

    Private Sub dgPriceUpload_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPriceUpload.ItemDataBound
        '-- Handle databinding

        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgPriceUpload.CurrentPageIndex * dgPriceUpload.PageSize)
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As Price = CType(e.Item.DataItem, Price)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then

                Dim lblKodeKendaraan As Label = CType(e.Item.FindControl("lblKodeKendaraan"), Label)
                Dim lblNamaKendaraan As Label = CType(e.Item.FindControl("lblNamaKendaraan"), Label)
                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                Try
                    lblKodeKendaraan.Text = RowValue.VechileColor.MaterialNumber
                    lblNamaKendaraan.Text = RowValue.VechileColor.MaterialDescription
                Catch ex As Exception
                    lblKodeKendaraan.Text = ""
                    lblNamaKendaraan.Text = ""
                End Try
                Try
                    lblDealerCode.Text = RowValue.Dealer.DealerCode
                Catch ex As Exception
                    lblDealerCode.Text = "[semua]"
                End Try
            End If

        End If
    End Sub

    Private Sub btnDnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDnLoad.Click
        '-- Download data in datagrid to text file

        '-- Generate timestamp
        Dim dt As DateTime = DateTime.Now
        Dim sSuffix As String = CType(dt.Year, String) & CType(dt.Month, String) & _
                                CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & _
                                CType(dt.Second, String) & CType(dt.Millisecond, String)

        '-- Temp file must be a randomly named text file!
        Dim PriceData As String = Server.MapPath("") & "\..\DataTemp\PriceData" & sSuffix & ".txt"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        '''SearchData
        ''' 

        Dim criterias As CriteriaComposite
        criterias = CType(Session("CriteaPriceList"), CriteriaComposite)
        Dim sortColl As SortCollection = New SortCollection
        If (Not IsNothing(ViewState("currSortColumn"))) AndAlso (Not IsNothing(ViewState("currSortColumn"))) AndAlso ViewState("currSortColumn") <> "" Then
            sortColl.Add(New Sort(GetType(V_Price), ViewState("currSortColumn").ToString(), CType(ViewState("currSortDirection"), Sort.SortDirection)))
        Else
            sortColl = Nothing
        End If


        Dim TempList As ArrayList ' = New PriceFacade(User).RetrieveByCriteria(criterias, sortColl)
        TempList = New V_PriceFacade(User).Retrieve(criterias, sortColl)

        '''
        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(PriceData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(PriceData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WritePriceData(sw, TempList)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download price data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\PriceData" & sSuffix & ".txt")

        Catch ex As Exception
            Dim errMess As String = ex.Message
        End Try

    End Sub

    Private Sub dgPrice_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPrice.SortCommand
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

        dgPrice.SelectedIndex = -1
        dgPrice.CurrentPageIndex = 0
        BindSearchGrid(dgPrice.CurrentPageIndex)

        ' BindPage(0)  '-- Display page-1

    End Sub

    Private Sub dgPrice_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPrice.PageIndexChanged
        '-- Change datagrid page
        ' BindPage(e.NewPageIndex)

        dgPrice.SelectedIndex = -1
        dgPrice.CurrentPageIndex = e.NewPageIndex
        BindSearchGrid(dgPrice.CurrentPageIndex)

    End Sub

    Private Sub ddlSubCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubCategory.SelectedIndexChanged
        BindVehicleType(False)
    End Sub

    Private Sub WritePriceDataExcel(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then

            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Umum - Daftar Harga Kendaraan")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No" & tab)
            itemLine.Append("Kode Kendaraan" & tab)
            itemLine.Append("Nama Kendaraan" & tab)
            itemLine.Append("Dealer" & tab)
            itemLine.Append("Mulai Berlaku" & tab)
            itemLine.Append("Harga Pokok (Rp)" & tab)
            itemLine.Append("Harga Lain-lain (Rp)" & tab)
            itemLine.Append("PPN BM (%)" & tab)
            itemLine.Append("PPN (%)" & tab)
            itemLine.Append("PPh 22 (%)" & tab)
            itemLine.Append("Interest (%)" & tab)
            itemLine.Append("Factoring Interest (%)" & tab)
            itemLine.Append("Discount Reward (%)" & tab)
            itemLine.Append("PPh 23 (%)" & tab)

            sw.WriteLine(itemLine.ToString())

            'Dim i As Integer = 1
            'Dim DealerCode As String = ""
            Dim Number As Integer
            For Each item As V_Price In data
                'If item.RowStatus = 0 Then
                Number += 1
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(Number.ToString & tab)
                itemLine.Append(item.MaterialNumber & tab)
                itemLine.Append(item.VechileColor.MaterialDescription & tab)
                itemLine.Append(item.DealerCode & tab)
                itemLine.Append(Format(item.ValidFrom, "dd/MM/yyyy") & tab)
                itemLine.Append(Format(item.BasePrice, "0") & tab)
                itemLine.Append(Format(item.OptionPrice, "0") & tab)
                itemLine.Append(Format(item.PPN_BM, "0.00") & tab)
                itemLine.Append(Format(item.PPN, "0.00") & tab)
                itemLine.Append(Format(item.PPh22, "0.00") & tab)
                itemLine.Append(Format(item.Interest, "0.000") & tab)
                itemLine.Append(Format(item.FactoringInt, "0.000") & tab)
                itemLine.Append(Format(item.DiscountReward, "0.00") & tab)
                itemLine.Append(FormatNumber(item.PPh23, 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault) & tab)
                sw.WriteLine(itemLine.ToString())
                'End If
            Next
        End If
    End Sub

    Protected Sub btnDnLoadtoExcel_Click(sender As Object, e As EventArgs) Handles btnDnLoadtoExcel.Click
        '-- Download data in datagrid to text file

        '-- Generate timestamp
        Dim dt As DateTime = DateTime.Now
        Dim sSuffix As String = CType(dt.Year, String) & CType(dt.Month, String) & _
                                CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & _
                                CType(dt.Second, String) & CType(dt.Millisecond, String)

        '-- Temp file must be a randomly named text file!
        Dim PriceData As String = Server.MapPath("") & "\..\DataTemp\PriceData" & sSuffix & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        '''SearchData
        ''' 

        Dim criterias As CriteriaComposite
        criterias = CType(Session("CriteaPriceList"), CriteriaComposite)
        Dim sortColl As SortCollection = New SortCollection
        If (Not IsNothing(ViewState("currSortColumn"))) AndAlso (Not IsNothing(ViewState("currSortColumn"))) AndAlso ViewState("currSortColumn") <> "" Then
            sortColl.Add(New Sort(GetType(V_Price), ViewState("currSortColumn").ToString(), CType(ViewState("currSortDirection"), Sort.SortDirection)))
        Else
            sortColl = Nothing
        End If


        Dim TempList As ArrayList ' = New PriceFacade(User).RetrieveByCriteria(criterias, sortColl)
        TempList = New V_PriceFacade(User).Retrieve(criterias, sortColl)

        '''
        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(PriceData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(PriceData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WritePriceDataExcel(sw, TempList)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download price data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\PriceData" & sSuffix & ".xls")

        Catch ex As Exception
            Dim errMess As String = ex.Message
        End Try

    End Sub

#End Region

End Class
