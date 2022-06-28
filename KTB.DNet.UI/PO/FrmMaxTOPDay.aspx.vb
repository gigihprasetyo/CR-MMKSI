#Region " Summary "
'--------------------------------------------------------'
'-- Program Code : FrmMaxTOPDay.aspx     --'
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
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System.IO
Imports System.Text
#End Region

Public Class FrmMaxTOPDay
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
    Protected WithEvents txtNewMaxTOPDay As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSimpanAll As System.Web.UI.WebControls.Button
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlFactoring As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlProvince As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkMaxTOPDay As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtMaxTOPDay As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lsbProvince As System.Web.UI.WebControls.ListBox
    Protected WithEvents chkCODH As CheckBox
    Protected WithEvents btnSaveCOD As Button
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
    Private _sessData As String = "FrmMaxTOPDay._sessData"
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
        'Dim ColorList As ArrayList = CType(sessHelp.GetSession("ColorList"), ArrayList)
        Dim ColorList As ArrayList = CType(sessHelp.GetSession(Me._sessData), ArrayList)

        Dim ColorLine As StringBuilder = New StringBuilder  '-- Color line in text file
        For Each obj As sp_MaxTOPDay In ColorList
            ColorLine.Remove(0, ColorLine.Length)  '-- Empty price line
            ColorLine.Append(obj.DealerCode.ToString() & ";")  '-- Kode dealer
            ColorLine.Append(obj.Factoring.ToString() & ";")  '-- Kode dealer
            ColorLine.Append(obj.ProvinceName.ToString() & ";")  '-- Kode dealer
            ColorLine.Append(obj.CategoryCode.ToString() & ";")  '-- Kode dealer
            ColorLine.Append(obj.VechileTypeCode.ToString() & ";")  '-- Kode dealer
            ColorLine.Append(obj.Normal.ToString() & ";")  '-- Kode dealer
            sw.WriteLine(ColorLine.ToString())  '-- Write color line
        Next
        'remarked by anh 20150224
        'For Each objVecColor As VechileColor In ColorList
        '    ColorLine.Remove(0, ColorLine.Length)  '-- Empty price line

        '    ColorLine.Append(objVecColor.VechileType.Category.CategoryCode.ToString() & ";")  '-- Kode kategori
        '    ColorLine.Append(objVecColor.VechileType.VechileTypeCode.ToString() & ";")        '-- Kode tipe
        '    ColorLine.Append(objVecColor.ColorCode.ToString() & ";")     '-- Warna kendaraan
        '    ColorLine.Append(objVecColor.VechileType.Description.ToString() & ";")  '-- Name tipe
        '    ColorLine.Append(objVecColor.ColorEngName.ToString() & ";")  '-- Bhs. Inggris
        '    ColorLine.Append(objVecColor.ColorIndName.ToString())        '-- Bhs. Indonesia

        '    sw.WriteLine(ColorLine.ToString())  '-- Write color line
        'Next

    End Function

    Private Function GetVTIDs() As String
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If ddlCategory.SelectedIndex <> 0 Then ' .SelectedValue <> "" Then
            criterias.opAnd(New Criteria(GetType(VechileColor), "VechileType.Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))
        End If
        If ddlSubCategory.SelectedIndex <> 0 Then ' .SelectedValue <> -1 Then
            'CommonFunction.SetVehicleSubCategoryCriterias(ddlSubCategory, ddlCategory.SelectedItem.Text, criterias, "VechileColor")

            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlSubCategory.SelectedValue
            criterias.opAnd(New Criteria(GetType(VechileColor), "VechileType.VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))
        End If
        If ddlType.SelectedIndex <> 0 Then ' .SelectedValue <> "" Then
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
        Dim sIDs As String = String.Empty

        For Each oC As VechileColor In ColorList
            sIDs &= IIf(sIDs = String.Empty, "", ",")
            If sIDs.Length > 0 AndAlso sIDs.IndexOf("," & oC.VechileType.ID.ToString) >= 0 Then
            Else
                sIDs &= oC.VechileType.ID.ToString
            End If
        Next
        Return sIDs
    End Function

    Private Sub BindPage(ByVal pageIndex As Integer, Optional ByVal isCOD As Integer = -1)
        Dim aDatas As New ArrayList
        Dim oSMTDFac As New sp_MaxTOPDayFacade(User)
        Dim DealerIDs As String = String.Empty
        Dim ProvinceIDs As String = String.Empty
        Dim oCategory As Category
        Dim CategoryID As Integer = 0
        Dim VechileTypeIDs As String = String.Empty
        Dim TotalRow As Integer = 0
        Dim ExecBy As String
        Dim oUI As UserInfo = CType(Session("LOGINUSERINFO"), UserInfo)
        Dim IsFactoring As Short = 0
        Dim MaxTOP As Integer = -1

        If Me.txtKodeDealer.Text.Trim <> String.Empty Then

            Dim aDs As ArrayList
            Dim cD As New CriteriaComposite(New Criteria(GetType(Dealer), "DealerCode", MatchType.InSet, "('" & Me.txtKodeDealer.Text.Trim.Replace(";", "','") & "')"))
            aDs = New DealerFacade(User).Retrieve(cD)
            For Each oD As Dealer In aDs
                DealerIDs &= IIf(DealerIDs = String.Empty, "", ",") & oD.ID.ToString
            Next
        End If
        ProvinceIDs = String.Empty
        For Each oLI As ListItem In Me.lsbProvince.Items
            If oLI.Selected = True Then
                ProvinceIDs &= IIf(ProvinceIDs.Trim = String.Empty, "", ",") & oLI.Value
            End If
        Next
        'If Me.ddlProvince.SelectedIndex <> 0 Then
        '    ProvinceIDs = Me.ddlProvince.SelectedValue
        'End If

        'If Category selected, subcategory not selected
        If Me.ddlCategory.SelectedIndex <> 0 AndAlso Me.ddlSubCategory.SelectedIndex = 0 Then
            oCategory = New CategoryFacade(User).Retrieve(CType(Me.ddlCategory.SelectedValue, String))
            If Not IsNothing(oCategory) AndAlso oCategory.ID > 0 Then
                CategoryID = oCategory.ID
            End If
        End If

        'If Category & subcategory selected & Type not selected
        If Me.ddlCategory.SelectedIndex <> 0 AndAlso Me.ddlSubCategory.SelectedIndex <> 0 AndAlso Me.ddlType.SelectedIndex = 0 Then
            VechileTypeIDs = Me.GetVTIDs()
        End If

        'If Type selected
        If Me.ddlType.SelectedIndex <> 0 Then
            Dim oVT As VechileType = New VechileTypeFacade(User).Retrieve(Me.ddlType.SelectedValue)
            If IsNothing(oVT) = False AndAlso oVT.ID > 0 Then
                VechileTypeIDs = oVT.ID.ToString
            End If
        End If

        'If Me.ddlSubCategory.SelectedIndex = 0 Then
        '    If Me.ddlCategory.SelectedIndex <> 0 Then
        '        oCategory = New CategoryFacade(User).Retrieve(CType(Me.ddlCategory.SelectedValue, String))
        '        If Not IsNothing(oCategory) AndAlso oCategory.ID > 0 Then
        '            CategoryID = oCategory.ID
        '        End If
        '    End If
        'Else
        '    VechileTypeIDs = Me.GetVTIDs()
        'End If

        If Me.chkMaxTOPDay.Checked Then
            Try
                MaxTOP = Me.txtMaxTOPDay.Text
            Catch ex As Exception
                MaxTOP = 0
                Me.txtMaxTOPDay.Text = 0
            End Try
            If MaxTOP < 0 Then MaxTOP = 0
        End If
        IsFactoring = Me.ddlFactoring.SelectedValue
        ExecBy = oUI.ID.ToString.PadLeft(6, "0") & oUI.UserName

        If pageIndex >= 0 Then
            btnDnLoad.Enabled = False  '-- Init: Disable <Download> button
            aDatas = oSMTDFac.RetrieveFromSP(0, pageIndex, Me.dgColor.PageSize, IsFactoring, 0, 0, DealerIDs, ProvinceIDs, CategoryID, VechileTypeIDs, MaxTOP, TotalRow, ExecBy)
            Me.sessHelp.SetSession(Me._sessData, aDatas)
            If aDatas.Count > 0 Then
                TotalRow = CType(aDatas(0), sp_MaxTOPDay).RowStatus
            End If
            Me.dgColor.CurrentPageIndex = pageIndex
            Me.dgColor.DataSource = aDatas
            Me.dgColor.VirtualItemCount = TotalRow
            Me.dgColor.DataBind()
            If aDatas.Count > 0 Then
                btnDnLoad.Enabled = True  '-- Enable <Download> button
            Else
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        Else

            Dim nTOP As Integer
            Try
                nTOP = Me.txtNewMaxTOPDay.Text
            Catch ex As Exception
                nTOP = 0
                MessageBox.Show("Jumlah Maks TOP Tidak Valid")
                Exit Sub
            End Try

            If dgColor.Items.Count <= 0 Then
                MessageBox.Show("Belum ada data yang dipilih.")
                Exit Sub
            End If

            Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")



            For Each dgItem As DataGridItem In dgColor.Items
                Dim strTmp As String = dgItem.Cells(GetColIndexByHeaderName(dgColor, "Kode/Tipe")).Text
                Dim oVT As VechileType = New VechileTypeFacade(User).Retrieve(strTmp.Trim)
                If Not IsNothing(oVT) AndAlso oVT.ProductCategory.Code.Trim <> companyCode Then
                    MessageBox.Show("Ada Tipe yang tidak terdapat pada Kategori Produk " & companyCode)
                    Exit Sub
                End If
            Next

            If IsValidTOPDay(nTOP) = False Then Exit Sub
            If isCOD = 1 Then
                aDatas = oSMTDFac.RetrieveFromSP(1, pageIndex, Me.dgColor.PageSize, IsFactoring, nTOP, nTOP, DealerIDs, ProvinceIDs, CategoryID, VechileTypeIDs, -1, TotalRow, ExecBy, IIf(chkCODH.Checked, 1, 0))
            Else
                aDatas = oSMTDFac.RetrieveFromSP(1, pageIndex, Me.dgColor.PageSize, IsFactoring, nTOP, nTOP, DealerIDs, ProvinceIDs, CategoryID, VechileTypeIDs, -1, TotalRow, ExecBy)
            End If

            Me.sessHelp.SetSession(Me._sessData, aDatas)
            MessageBox.Show(SR.SaveSuccess)
            BindPage(0)
        End If
    End Sub

    Private Function IsValidTOPDay(ByVal nNewDay As Integer) As Boolean
        Dim aTPs As ArrayList
        Dim cTP As New CriteriaComposite(New Criteria(GetType(TermOfPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sTP As New SortCollection
        Dim Min As Integer, Max As Integer

        If nNewDay = 0 Then Return True

        cTP.opAnd(New Criteria(GetType(TermOfPayment), "PaymentType", MatchType.Exact, CType(enumPaymentType.PaymentType.TOP, Short)))
        sTP.Add(New Sort(GetType(TermOfPayment), "TermOfPaymentValue", Sort.SortDirection.ASC))
        aTPs = New TermOfPaymentFacade(User).Retrieve(cTP, sTP)
        If aTPs.Count < 1 Then
            MessageBox.Show(nNewDay.ToString & " Tidak Valid.")
            Return False
        Else
            Min = CType(aTPs(0), TermOfPayment).TermOfPaymentValue
            If Min > 0 Then Min = 0
            Max = CType(aTPs(aTPs.Count - 1), TermOfPayment).TermOfPaymentValue
            If nNewDay < Min OrElse nNewDay > Max Then
                MessageBox.Show(nNewDay.ToString & " Tidak Valid. Range Valid " & Min.ToString() & " - " & Max.ToString())
                Return False
            End If
        End If
        Return True
    End Function

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
            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where SubCategoryVehicleID = " & ddlSubCategory.SelectedValue
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))  '-- Sort by Vechile type code

            '-- Bind Vehicle type dropdownlist
            'ddlType.DataSource = New VechileTypeFacade(User).RetrieveByCriteria(criterias, sortColl)
            'ddlType.DataTextField = "VechileTypeCode"
            'ddlType.DataValueField = "VechileTypeCode"
            'ddlType.DataBind()
            Dim aVTs As ArrayList = New VechileTypeFacade(User).RetrieveByCriteria(criterias, sortColl)
            ddlType.Items.Clear()
            ddlType.Items.Add(New ListItem("Pilih", ""))
            For Each oVT As VechileType In aVTs
                ddlType.Items.Add(New ListItem(oVT.VechileTypeCode & " (" & oVT.Description & ")", oVT.VechileTypeCode))
            Next
        End If

        'ddlType.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item
    End Sub

    Private Sub BindDdlProvince()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim aPs As ArrayList

        criterias.opAnd(New Criteria(GetType(Province), "ID", MatchType.Greater, 0))
        aPs = New ProvinceFacade(User).RetrieveActiveList(criterias, "ProvinceName", Sort.SortDirection.ASC)

        'ddlProvince.DataSource = New ProvinceFacade(User).RetrieveActiveList(criterias, "ProvinceName", Sort.SortDirection.ASC)
        'ddlProvince.DataTextField = "ProvinceName"
        'ddlProvince.DataValueField = "ID"
        'ddlProvince.DataBind()
        'lsbProvince.Items.Insert(0, New ListItem("", "0"))
        For Each oP As Province In aPs
            lsbProvince.Items.Add(New ListItem(oP.ProvinceName, oP.ID))
        Next
    End Sub

    Private Sub CheckPrivilege()
        Dim IsLihat As Boolean = False
        Dim IsEdit As Boolean = False

        IsLihat = SecurityProvider.Authorize(Context.User, SR.Setting_maks_hari_top_lihat_privilege)
        IsEdit = SecurityProvider.Authorize(Context.User, SR.Setting_maks_hari_top_ubah_privilege)
        If IsLihat = False AndAlso IsEdit = False Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Maintenance-Maksimum Hari TOP")
        End If
        Me.btnSimpanAll.Enabled = IsEdit
        Me.txtNewMaxTOPDay.Enabled = IsEdit
        Me.txtNewMaxTOPDay.ReadOnly = Not IsEdit
        Me.dgColor.Columns(Me.dgColor.Columns.Count - 2).Visible = IsEdit
    End Sub
#End Region

#Region " EventHandler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not IsPostBack Then
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            Me.ddlFactoring.Items.Add(New ListItem("Non Factoring", 0))
            Me.ddlFactoring.Items.Add(New ListItem("Factoring", 1))
            BindDdlProvince()

            CheckPrivilege()

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

        'ReadData()   '-- Read all data matching criteria
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

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgColor.CurrentPageIndex * dgColor.PageSize)
            Dim txtMaxTOP As TextBox = e.Item.FindControl("txtMaxTOP")
            Dim chkCOD As CheckBox = e.Item.FindControl("chkCOD")
            Dim IsFactoring As Short = 0
            Dim aDatas As ArrayList = Me.sessHelp.GetSession(Me._sessData)
            If IsNothing(aDatas) OrElse aDatas.Count < 1 Then Exit Sub
            Dim oSMD As sp_MaxTOPDay = aDatas(e.Item.ItemIndex)
            Dim lblFactoring As Label = e.Item.FindControl("lblFactoring")
            Dim oVT As VechileType = New VechileTypeFacade(User).Retrieve(oSMD.VechileTypeCode)
            Dim lblDescription As Label = e.Item.FindControl("lblDescription")

            IsFactoring = Me.ddlFactoring.SelectedValue
            lblDescription.Text = oVT.Description
            If IsFactoring = 0 Then
                txtMaxTOP.Text = oSMD.Normal
                lblFactoring.Text = "N"
            Else
                txtMaxTOP.Text = oSMD.Factoring
                lblFactoring.Text = "F"
            End If

            If oSMD.IsCOD = 1 Then
                chkCOD.Checked = IIf(oSMD.IsCOD = 1, True, False)
            End If
            Dim lblHistoryStatus As Label = e.Item.FindControl("lblHistoryStatus")
            If oSMD.ID > 0 Then
                Dim sUrl As String = "../Popup/PopupDataHistory.aspx?TableName=MaxTOPDay&TableID=" & oSMD.ID.ToString
                sUrl &= "&FieldName=" & IIf(IsFactoring, "Factoring", "Normal")
                sUrl = "ShowDataHistory('" & sUrl & "');"
                lblHistoryStatus.Attributes.Add("OnClick", sUrl)
            Else
                lblHistoryStatus.Attributes.Add("OnClick", "alert('Data Belum Disimpan')")
            End If
        End If
    End Sub

    Private Sub ddlSubCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubCategory.SelectedIndexChanged
        BindVehicleType(False)
    End Sub

    Private Sub dgColor_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgColor.ItemCommand
        If e.CommandName.Trim.ToUpper = "Save".ToUpper Then
            Dim aDatas As ArrayList = Me.sessHelp.GetSession(Me._sessData)
            If IsNothing(aDatas) OrElse aDatas.Count < 1 Then Exit Sub
            Dim oSMD As sp_MaxTOPDay = aDatas(e.Item.ItemIndex)
            Dim txtMaxTOP As TextBox = e.Item.FindControl("txtMaxTOP")
            Dim chkCOD As CheckBox = e.Item.FindControl("chkCOD")
            'Dim lblFactoring As Label = e.Item.FindControl("lblFactoring")
            'Dim IsFactoring As Short = 0
            'Dim oMTD As MaxTOPDay
            'Dim oMTDFac As New MaxTOPDayFacade(User)

            'IsFactoring = Me.ddlFactoring.SelectedValue

            'If oSMD.ID > 0 Then
            '    oMTD = oMTDFac.Retrieve(oSMD.ID)
            'Else
            '    oMTD = New MaxTOPDay
            '    With oMTD
            '        .DealerID = 0
            '        .VechileTypeID = 0
            '        .Normal = 0
            '        .Factoring = 0
            '    End With
            'End If
            'If IsFactoring = 0 Then
            '    oMTD.Normal = txtMaxTOP.Text
            'Else
            '    oMTD.Factoring = txtMaxTOP.Text
            'End If
            'If oMTD.ID < 1 Then
            '    oMTD.ID = oMTDFac.Insert(oMTD)
            'Else
            '    oMTD.ID = oMTDFac.Update(oMTD)
            'End If
            'If oMTD.ID > 0 Then
            '    MessageBox.Show(SR.SaveSuccess)
            'Else
            '    MessageBox.Show(SR.SaveFail)
            'End If

            Dim oSMTDFac As New sp_MaxTOPDayFacade(User)
            Dim DealerIDs As String = String.Empty
            Dim ProvinceIDs As String = String.Empty
            Dim oCategory As Category
            Dim CategoryID As Integer = 0
            Dim VechileTypeIDs As String = String.Empty
            Dim TotalRow As Integer = 0
            Dim ExecBy As String
            Dim oUI As UserInfo = CType(Session("LOGINUSERINFO"), UserInfo)
            Dim IsFactoring As Short = 0

            DealerIDs = oSMD.DealerID.ToString
            VechileTypeIDs = oSMD.VechileTypeID.ToString

            IsFactoring = Me.ddlFactoring.SelectedValue
            ExecBy = oUI.ID.ToString.PadLeft(6, "0") & oUI.UserName

            Dim nTOP As Integer
            Try
                nTOP = txtMaxTOP.Text
            Catch ex As Exception
                nTOP = 0
                MessageBox.Show("Jumlah Maks TOP Tidak Valid")
                Exit Sub
            End Try
            If IsValidTOPDay(nTOP) = False Then Exit Sub

            Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
            Dim oVT As VechileType = New VechileTypeFacade(User).Retrieve(CInt(VechileTypeIDs))

            If oVT.ProductCategory.Code.Trim <> companyCode Then
                MessageBox.Show("Tipe tidak terdapat pada Kategori Produk " & companyCode)
                Exit Sub
            End If

            aDatas = oSMTDFac.RetrieveFromSP(1, 0, Me.dgColor.PageSize, IsFactoring, nTOP, nTOP, DealerIDs, ProvinceIDs, CategoryID, VechileTypeIDs, -1, TotalRow, ExecBy, IIf(chkCOD.Checked, 1, 0))
            MessageBox.Show(SR.SaveSuccess)
        End If
    End Sub

    Private Sub btnSimpanAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpanAll.Click
        BindPage(-1)
    End Sub

    Private Function GetColIndexByHeaderName(ByVal objDg As DataGrid, ByVal strHeaderName As String) As Integer
        Dim idxCol As Integer = 0
        For Each colItem As DataGridColumn In objDg.Columns
            If colItem.HeaderText.ToUpper = strHeaderName.ToUpper Then
                Exit For
            End If
            idxCol = idxCol + 1
        Next
        Return idxCol
    End Function
#End Region

    Protected Sub btnSaveCOD_Click(sender As Object, e As EventArgs) Handles btnSaveCOD.Click
        BindPage(-1, 1)
    End Sub
End Class
