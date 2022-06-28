#Region " Summary "
'-----------------------------------------------'
'-- Program Code : FrmGetPrice.aspx           --'
'-- Program Name : Informasi Harga Kendaraan  --'
'-- Description  :                            --'
'-----------------------------------------------'
'-- Programmer   : Agus Pirnadi               --'
'-- Start Date   : Nov 01 2005                --'
'-- Update By    :                            --'
'-- Last Update  : Feb 27 2006                --'
'-----------------------------------------------'
'-- Copyright © 2005 by Intimedia             --'
'-----------------------------------------------'
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System.IO
Imports System.Text
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Parser
#End Region

Public Class FrmGetPrice
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgPrice As System.Web.UI.WebControls.DataGrid
    Protected WithEvents chkAll As System.Web.UI.WebControls.CheckBox
    Protected WithEvents calCalendar As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnDnLoad As System.Web.UI.WebControls.Button
    Protected WithEvents ddlSubCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents trDealer As System.Web.UI.HtmlControls.HtmlTableRow

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Custom Variable "
    Private sessHelp As SessionHelper = New SessionHelper  '-- Session helper
#End Region

#Region " Custom Method "

    Private Sub BindDropDownList()
        '-- Bind dropdown list

        '-- Category criteria & sort
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code ascendingly

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

    Private Function bExistIn(ByVal objPrice As Price, ByVal PriceList As ArrayList) As Boolean
        For Each objSourcePrice As Price In PriceList
            If objPrice.VechileColor.ID = objSourcePrice.VechileColor.ID AndAlso objPrice.Dealer.ID = objSourcePrice.Dealer.ID Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function WritePriceData(ByRef sw As StreamWriter)

        '-- Retrieve vehicle price list from session
        Dim PriceList As ArrayList = CType(sessHelp.GetSession("PriceList"), ArrayList)

        Dim PriceLine As StringBuilder = New StringBuilder  '-- Price line in text file

        'PriceLine.Remove(0, PriceLine.Length)  '-- Empty price line

        'PriceLine.Append("Kode Kendaraan" & ";")       '-- Kode kendaraan
        'PriceLine.Append("Nama Kendaraan" & ";")  '-- Nama kendaraan
        'PriceLine.Append("Dealer" & ";")  '-- dealer
        'PriceLine.Append("Tgl. Mulai Berlaku" & ";")  '-- Valid from
        'PriceLine.Append("Harga Kendaraan" & ";")  '-- Vehicle price
        'PriceLine.Append("PPh22")         '-- PPh22 Amount

        'sw.WriteLine(PriceLine.ToString())  '-- Write price line
        For Each objVecPrice As Price In PriceList
            PriceLine.Remove(0, PriceLine.Length)  '-- Empty price line

            PriceLine.Append(objVecPrice.VechileColor.MaterialNumber.ToString() & ";")       '-- Kode kendaraan
            PriceLine.Append(objVecPrice.VechileColor.MaterialDescription.ToString() & ";")  '-- Nama kendaraan
            PriceLine.Append(objVecPrice.Dealer.DealerCode & ";")  '-- Dealer
            PriceLine.Append(Format(objVecPrice.ValidFrom, "dd/MM/yyyy") & ";")  '-- Valid from
            PriceLine.Append(Format(objVecPrice.VehiclePrice, "0") & ";")  '-- Vehicle price
            PriceLine.Append(Format(objVecPrice.PPh22Amount, "0"))         '-- PPh22 Amount

            sw.WriteLine(PriceLine.ToString())  '-- Write price line
        Next

    End Function

    Private Sub ReadData()
        '-- Read all data selected

        btnDnLoad.Enabled = False  '-- Init: Disable <Download> button

        '-- Compose search criteria
        Dim criterias As CriteriaComposite

        '-- Status
        criterias = New CriteriaComposite(New Criteria(GetType(Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If ddlCategory.SelectedValue <> "" Then  '-- Category
            criterias.opAnd(New Criteria(GetType(Price), "VechileColor.VechileType.Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))
        End If
        If ddlSubCategory.SelectedValue <> "-1" Then
            'CommonFunction.SetVehicleSubCategoryCriterias(ddlSubCategory, ddlCategory.SelectedItem.Text, criterias, "Price")
            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlSubCategory.SelectedValue
            criterias.opAnd(New Criteria(GetType(Price), "VechileColor.VechileType.VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))
        End If
        If ddlType.SelectedValue <> "" Then  '-- Vechile type
            criterias.opAnd(New Criteria(GetType(Price), "VechileColor.VechileType.VechileTypeCode", MatchType.Exact, ddlType.SelectedValue))
        End If

        '-- Type still active
        criterias.opAnd(New Criteria(GetType(Price), "VechileColor.VechileType.Status", MatchType.No, "X"))
        '-- Color still active
        criterias.opAnd(New Criteria(GetType(Price), "VechileColor.Status", MatchType.No, "X"))

        If Not chkAll.Checked Then
            criterias.opAnd(New Criteria(GetType(Price), "ValidFrom", MatchType.Lesser, calCalendar.Value.AddDays(1)))
        End If
        '-- Active material
        criterias.opAnd(New Criteria(GetType(Price), "VechileColor.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '-- Exclude special color
        criterias.opAnd(New Criteria(GetType(Price), "VechileColor.SpecialFlag", MatchType.Exact, ""))

        Dim oDealer As Dealer = Session("DEALER")
        If oDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, Short) Then
            criterias.opAnd(New Criteria(GetType(Price), "Dealer.ID", MatchType.Exact, oDealer.ID))
        Else
            If Me.txtKodeDealer.Text.Trim <> "" Then
                Dim sCodes As String = Me.txtKodeDealer.Text.Trim()
                sCodes = "'" & sCodes.Replace(";", "','") & "'"
                criterias.opAnd(New Criteria(GetType(Price), "Dealer.DealerCode", MatchType.InSet, "(" & sCodes & ")"))
            End If
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Price), "VechileColor.MaterialNumber", Sort.SortDirection.ASC))
        sortColl.Add(New Sort(GetType(Price), "ValidFrom", Sort.SortDirection.DESC))

        '-- Retrieve price list
        Dim TempList As ArrayList = New PriceFacade(User).RetrieveByCriteria(criterias, sortColl)

        Dim PriceList As New ArrayList  '-- Already selected price list

        If Not chkAll.Checked Then  '-- Valid from date criteria is concerned
            For Each objPrice As Price In TempList
                If Not bExistIn(objPrice, PriceList) Then
                    PriceList.Add(objPrice)
                End If
            Next
        Else
            PriceList = TempList  '-- "All" checkbox selected
        End If

        '-- Store vehicle price list into session for later use by Download
        sessHelp.SetSession("PriceList", PriceList)

        If PriceList.Count > 0 Then
            btnDnLoad.Enabled = True  '-- Enable <Download> button
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

    Private Sub ActivateUserPrivilege()
        '-- Assign privileges

        ''btnCari.Visible = SecurityProvider.Authorize(Context.User, SR.)
        btnDnLoad.Visible = SecurityProvider.Authorize(Context.User, SR.DownloadPriceInfo_Privilege)
    End Sub
    Private Sub BindVehicleType(ByVal IsClearAll As Boolean)
        ddlType.Items.Clear()
        If ddlSubCategory.SelectedValue <> "-1" And Not IsClearAll Then

            '-- Vehicle criteria & sort
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.No, "X"))  '-- Type still active
            criterias.opAnd(New Criteria(GetType(VechileType), "Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))
            'CommonFunction.SetVehicleSubCategoryCriterias(ddlSubCategory, ddlCategory.SelectedItem.Text, criterias, "VechileType")

            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlSubCategory.SelectedValue
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))

            Dim strSql2 As String = String.Format(
                String.Join(
                Environment.NewLine,
"SELECT VechileTypeID FROM VechileColorIsActiveOnPK WITH (NOLOCK) ",
"INNER JOIN VechileColor on VechileColorIsActiveOnPK.VehicleColorID=VechileColor.id",
"WHERE (VechileColorIsActiveOnPK.RowStatus = 0",
"AND VechileColorIsActiveOnPK.Status = 1",
"AND VechileColor.Status <> 'X')"
))

            criterias.opAnd(New Criteria(GetType(VechileType), "ID", MatchType.InSet, "(" & strSql2 & ")"))

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
            If Not SecurityProvider.Authorize(Context.User, SR.ViewPriceInfo_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Umum-Informasi Harga Kendaraan")
            End If
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            Dim oDealer As Dealer = Session("DEALER")
            If oDealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, Short) Then
                Me.dgPrice.Columns(4).Visible = True
            Else
                Me.dgPrice.Columns(4).Visible = False
                Me.trDealer.Style.Add("display", "none")
            End If
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

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click

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
        Dim PriceData As String = Server.MapPath("") & "\..\DataTemp\PriceData" & sSuffix & ".txt"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

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
                WritePriceData(sw)

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

        BindPage(0)  '-- Display page-1

    End Sub

    Private Sub dgPrice_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPrice.PageIndexChanged
        '-- Change datagrid page
        BindPage(e.NewPageIndex)
    End Sub

    Private Sub dgPrice_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPrice.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgPrice.CurrentPageIndex * dgPrice.PageSize)
            Try
                Dim lblDealerCode As Label = e.Item.FindControl("lblDealerCode")
                Dim oPrice As Price = e.Item.DataItem

                lblDealerCode.Text = oPrice.Dealer.DealerCode
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub ddlSubCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubCategory.SelectedIndexChanged
        BindVehicleType(False)
    End Sub

#End Region

    
End Class
