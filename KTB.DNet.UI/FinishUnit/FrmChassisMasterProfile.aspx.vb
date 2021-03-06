#Region " Summary "
'----------------------------------------------------'
'-- Program Code : FrmInvoiceResultList.aspx       --'
'-- Program Name : Daftar Status Faktur Kendaraan  --'
'-- Description  :                                 --'
'----------------------------------------------------'
'-- Programmer   : Agus Pirnadi                    --'
'-- Start Date   : Nov 01 2005                     --'
'-- Update By    :                                 --'
'-- Last Update  : Jan 02 2005                     --'
'----------------------------------------------------'
'-- Copyright ? 2005 by Intimedia                  --'
'----------------------------------------------------'
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
#End Region

Public Class FrmChassisMasterProfile
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblChassisNo As System.Web.UI.WebControls.Label
    Protected WithEvents txtChassisNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblColon4 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dgInvoiceList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblCategory As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDownload As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents btnDnLoad As System.Web.UI.WebControls.Button
    Protected WithEvents icStartPembuatan As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEndPembuatan As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlProvince As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCity As System.Web.UI.WebControls.DropDownList
    Protected WithEvents rdoBasedOnDealer As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rdoBasedOnCustomer As System.Web.UI.WebControls.RadioButton
    Protected WithEvents btnSAP As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Private fields "
    Dim sessHelp As SessionHelper = New SessionHelper
    Dim InvoiceList As New ArrayList  '-- List of invoice
    Dim _PCAccessAllowed As Boolean = False
    Dim _CVAccessAllowed As Boolean = False
    Dim _LCVAccessAllowed As Boolean = False
    Dim _DownloadAllowed As Boolean = False
    Dim _TransferAllowed As Boolean = False
    Dim _EditAllowed As Boolean = False
    Private objDealer As Dealer
#End Region

#Region " Custom Method "

    Private Sub BindDropdownList()
        '-- Category criteria & sort
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim cat As String = ""
        If _PCAccessAllowed Then
            cat = cat & "'PC',"
        End If
        If _CVAccessAllowed Then
            cat = cat & "'CV',"
        End If
        If _LCVAccessAllowed Then
            cat = cat & "'LCV',"
        End If
        If cat <> "" Then
            cat = "(" & cat.Substring(0, cat.Length - 1) & ")"
            criterias.opAnd(New Criteria(GetType(Category), "CategoryCode", MatchType.InSet, cat))
        End If
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code
        '-- Bind Category dropdownlist
        ddlCategory.DataSource = New CategoryFacade(User).RetrieveByCriteria(criterias, sortColl)
        ddlCategory.DataTextField = "CategoryCode"
        ddlCategory.DataValueField = "CategoryCode"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item

        CommonFunction.BindProvince(ddlProvince, User, True, False)
        ddlCity.Items.Add(New ListItem("Silahkan Pilih", ""))
    End Sub

    Private Function Transfer(ByVal DestFile As String, ByVal Val As String, ByVal arl As ArrayList, ByVal isTransfer As Boolean) As Boolean
        Dim success As Boolean = False
        Dim sw As StreamWriter
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim finfo As New FileInfo(DestFile)
        Try
            success = imp.Start()
            If success Then
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If
                If (isTransfer) Then
                    If (New ChassisMasterFacade(User).UpdateTransaction(arl) <> -1) Then
                        sw = New StreamWriter(DestFile)
                        sw.WriteLine(Val)
                        sw.Close()
                    Else
                        success = False
                    End If
                Else
                    sw = New StreamWriter(DestFile)
                    sw.WriteLine(Val)
                    sw.Close()
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            sw.Close()
            Throw ex
            Return success
        End Try
        Return success
    End Function

    Private Sub TransferDeTransfer(ByVal isTransfer As Boolean)
        Dim i As Integer = 0
        Dim CMP As ArrayList
        Dim arlCM As ArrayList = New ArrayList
        Dim IsCheck As Boolean = False
        Dim sb As New StringBuilder
        Dim filename = String.Format("{0}{1}{2}", "customerprofile", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\" & filename  '-- Destination file

        For Each item As DataGridItem In dgInvoiceList.Items
            Dim chk As CheckBox = CType(item.FindControl("chkSelect"), CheckBox)
            If (chk.Checked) Then
                IsCheck = True
                CMP = New ArrayList
                Dim CM As New ChassisMaster
                CM = New ChassisMasterFacade(User).Retrieve(Convert.ToInt32(dgInvoiceList.DataKeys().Item(i)))

                Dim CMPFacade As ChassisMasterProfileFacade = New ChassisMasterProfileFacade(User)
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterProfile), "ChassisMaster.ID", MatchType.Exact, Convert.ToInt32(dgInvoiceList.DataKeys().Item(i))))
                CMP = CMPFacade.Retrieve(criterias)

                If (isTransfer) Then
                    If (CM.IsSAPDownload.Trim() <> "X") Then
                        CM.IsSAPDownload = "X"
                        arlCM.Add(CM)
                        SetVal(CM, CMP, sb)
                    End If
                Else
                    If (CM.IsSAPDownload.Trim() = "X") Then
                        SetVal(CM, CMP, sb)
                    End If
                End If
            End If
            i = i + 1
        Next

        If IsCheck Then
            If (sb.Length > 0) Then
                If Transfer(DestFile, sb.ToString(), arlCM, isTransfer) Then
                    MessageBox.Show("Data berhasil di transfer ke SAP")
                Else
                    MessageBox.Show("Data gagal ditransfer ke SAP")
                End If
            Else
                If (isTransfer) Then
                    MessageBox.Show("Data pernah ditransfer ke SAP")
                Else
                    MessageBox.Show("Daftar pernah ditransfer ke SAP")
                End If
            End If
        Else
            MessageBox.Show("Daftar customer profile belum dipilih")
        End If

    End Sub

    Private Sub SetVal(ByVal CM As ChassisMaster, ByVal CMP As ArrayList, ByVal sb As StringBuilder)
        If (CMP.Count > 0) Then
            For Each item2 As ChassisMasterProfile In CMP
                Dim str As String
                For Each item3 As ProfileDetail In item2.ProfileHeader.ProfileDetails
                    str += item3.Code & "-"
                Next
                If (str <> String.Empty) Then
                    sb.Append(item2.ChassisMaster.ChassisNumber & ";" & item2.ProfileHeader.ID & ";" & str.Remove(str.Length - 1, 1) & Chr(13) & Chr(10))
                Else
                    sb.Append(item2.ChassisMaster.ChassisNumber & ";" & item2.ProfileHeader.ID & ";" & Chr(13) & Chr(10))
                End If
            Next
        Else
            sb.Append(CM.ChassisNumber & "; ;" & Chr(13) & Chr(10))
        End If
    End Sub

#End Region

#Region " EventHandler "

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.CustomerProfileListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=KONSUMEN - Daftar Profile ")
        End If
        Me.dgInvoiceList.Columns(9).Visible = SecurityProvider.Authorize(context.User, SR.FakturKendaraanPangajuanBuat_Privilege)
        _PCAccessAllowed = SecurityProvider.Authorize(context.User, SR.PKCategoryPC_Privilege)
        _CVAccessAllowed = SecurityProvider.Authorize(context.User, SR.PKCategoryCV_Privilege)
        _LCVAccessAllowed = SecurityProvider.Authorize(context.User, SR.PKCategoryLCV_Privilege)
        If (Not _PCAccessAllowed) And (Not _CVAccessAllowed) And (Not _LCVAccessAllowed) Then
            Me.btnSearch.Visible = False
            Me.ddlCategory.Visible = False
        End If
        _DownloadAllowed = SecurityProvider.Authorize(context.User, SR.CustomerProfileListDownload_Privilege)
        _TransferAllowed = SecurityProvider.Authorize(context.User, SR.CustomerProfileListSAPTransfer_Privilege)
        _EditAllowed = SecurityProvider.Authorize(context.User, SR.CustomerProfileListEdit_Privilege)

    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        btnSearch.Attributes.Add("onclick", "MakeValid();")
        btnDnLoad.Attributes.Add("onclick", "MakeValid();")
        btnSAP.Attributes.Add("onclick", "MakeValid();")
        InitiateAuthorization()
        If Not IsPostBack Then
            BindDropdownList()  '-- Bind dropdownlist
            ViewState("currSortColumn") = "ChassisNumber"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            Dim crit As Hashtable = New Hashtable
            crit = CType(Session("CriteriaFormInvoiceResultList"), Hashtable)
            If Not crit Is Nothing Then
                txtChassisNo.Text = CStr(crit.Item("ChassisNumber"))
                ddlCategory.SelectedValue = CStr(crit.Item("Category"))
                icStartPembuatan.Value = CType(crit("StartPembuatan"), Date)
                icEndPembuatan.Value = CType(crit("EndPembuatan"), Date)
                ddlProvince.SelectedValue = CStr(crit("Province"))
                ddlCity.SelectedValue = CStr(crit("City"))
                txtKodeDealer.Text = CStr(crit.Item("Dealer"))
                'chkTanggalPembuatan.Checked = CType(crit("chkTanggalPembuatan"), Boolean)
                dgInvoiceList.CurrentPageIndex = CInt(crit.Item("PageIndex"))
                rdoBasedOnDealer.Checked = CBool(crit("rdoDealer"))
                rdoBasedOnCustomer.Checked = CBool(crit("rdoCustomer"))
            End If
            ReadData()   '-- Read all data matching criteria
            BindPage(dgInvoiceList.CurrentPageIndex)  '-- Bind page-1
            'Bug Enhanc 
            'If CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo).Dealer.Title = 0 Then
            btnSAP.Visible = False
            'End If
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If DateDiff(DateInterval.Day, icStartPembuatan.Value, icEndPembuatan.Value, Microsoft.VisualBasic.FirstDayOfWeek.Monday, FirstWeekOfYear.Jan1) > 95 Then
            MessageBox.Show("Rentang tanggal pembuatan profile tidak boleh lebih dari 95 hari")
            Exit Sub
        ElseIf DateDiff(DateInterval.Day, icStartPembuatan.Value, icEndPembuatan.Value, Microsoft.VisualBasic.FirstDayOfWeek.Monday, FirstWeekOfYear.Jan1) < 0 Then
            MessageBox.Show("Rentang tanggal pembuatan profile tidak valid")
            Exit Sub
        Else
            storeCriteria()
            ReadData()   '-- Read all data matching criteria
            dgInvoiceList.CurrentPageIndex = 0
            BindPage(dgInvoiceList.CurrentPageIndex)  '-- Bind page-1
        End If
    End Sub
    Private Sub rdoBasedOnCustomer_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoBasedOnCustomer.CheckedChanged
        storeCriteria()
    End Sub

    Private Sub rdoBasedOnDealer_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoBasedOnDealer.CheckedChanged
        storeCriteria()
    End Sub
    Private Sub storeCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("ChassisNumber", txtChassisNo.Text)
        crit.Add("Category", ddlCategory.SelectedValue)
        crit.Add("StartPembuatan", icStartPembuatan.Value)
        crit.Add("EndPembuatan", icEndPembuatan.Value)
        crit.Add("Province", ddlProvince.SelectedValue)
        crit.Add("City", ddlCity.SelectedValue)
        crit.Add("Dealer", txtKodeDealer.Text)
        crit.Add("PageIndex", dgInvoiceList.CurrentPageIndex)
        'crit.Add("chkTanggalPembuatan", chkTanggalPembuatan.Checked)
        crit.Add("rdoDealer", rdoBasedOnDealer.Checked)
        crit.Add("rdoCustomer", rdoBasedOnCustomer.Checked)
        sessHelp.SetSession("CriteriaFormInvoiceResultList", crit)
    End Sub


    Private Sub SearchChassisMaster(ByVal CityIDs As String, ByVal IsSearchByCustomer As Boolean)
        'If chkTanggalPembuatan.Checked Then
        If icStartPembuatan.Value > icEndPembuatan.Value Then
            MessageBox.Show("Interval tanggal validasi tidak valid")
            Exit Sub  '-- Directly exits
        Else
            If icEndPembuatan.Value.Subtract(icStartPembuatan.Value).Days > 65 Then
                MessageBox.Show("Selisih tanggal validasi harus <= 65 hari")
                Exit Sub  '-- Directly exits
            End If
        End If
        'End If

        Dim criterias As New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If CityIDs.Length > 0 Then
            If IsSearchByCustomer Then
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.Customer.City.ID", MatchType.InSet, CityIDs))
            Else
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "Dealer.City.ID", MatchType.InSet, CityIDs))
            End If
        End If

        If txtChassisNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.[Partial], txtChassisNo.Text.Trim()))
        End If
        If ddlCategory.SelectedValue <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))
        End If

        Dim StartPembuatan As New DateTime(CInt(icStartPembuatan.Value.Year), CInt(icStartPembuatan.Value.Month), CInt(icStartPembuatan.Value.Day), 0, 0, 0)
        Dim EndPembuatan As New DateTime(CInt(icEndPembuatan.Value.Year), CInt(icEndPembuatan.Value.Month), CInt(icEndPembuatan.Value.Day), 23, 59, 59)

        criterias.opAnd(New Criteria(GetType(ChassisMaster), "LastUpdateProfile", MatchType.GreaterOrEqual, Format(StartPembuatan, "yyyy-MM-dd HH:mm:ss")))
        criterias.opAnd(New Criteria(GetType(ChassisMaster), "LastUpdateProfile", MatchType.LesserOrEqual, Format(EndPembuatan, "yyyy-MM-dd HH:mm:ss")))

        objDealer = Session("DEALER")


        If txtKodeDealer.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        Else
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
            End If
        End If

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(ChassisMaster), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))  '-- Nomor chassis


        Dim InvoiceResultList As ArrayList = New ChassisMasterFacade(User).RetrieveByCriteria(criterias, sortColl)

        sessHelp.SetSession("InvoiceResList", InvoiceResultList)
    End Sub

    Private Sub SearchByCustomer(ByVal CityIDs As String)
        Dim chassisMasterIDs As String = String.Empty
        Dim strCityIds As String = CityIDs

        'Dim customerProfileCrits As New CriteriaComposite(New Criteria(GetType(CustomerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If strCityIds.Length > 0 Then
            strCityIds = "(" + strCityIds + ")"
            'customerProfileCrits.opAnd(New Criteria(GetType(KTB.DNet.Domain.CustomerProfile), "Customer.City.ID", MatchType.InSet, strCityIds))

            'Dim arlCustomerProfile As ArrayList = New KTB.DNet.BusinessFacade.Profile.CustomerProfileFacade(User).RetrieveByCriteria(customerProfileCrits)

            'If arlCustomerProfile.Count > 0 Then
            '    For Each custProfile As CustomerProfile In arlCustomerProfile
            '        If Not custProfile.ProfileHeader Is Nothing Then
            '            For Each chassisMasterProfileItem As ChassisMasterProfile In custProfile.ProfileHeader.ChassisMasterProfiles
            '                If chassisMasterIDs.Length > 0 Then
            '                    chassisMasterIDs += ", "
            '                End If
            '                chassisMasterIDs += chassisMasterProfileItem.ChassisMaster.ID.ToString()
            '            Next
            '        End If
            '    Next
            'End If
        End If

        SearchChassisMaster(strCityIds, True)
    End Sub

    Private Sub SearchByDealer(ByVal CityIDs As String)
        Dim chassisMasterIDs As String = String.Empty
        Dim strCityIds As String = CityIDs

        'Dim crits As New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If strCityIds.Length > 0 Then
            strCityIds = "(" + strCityIds + ")"
            'crits.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "Dealer.City.ID", MatchType.InSet, strCityIds))

            'Dim arlChassisMaster As ArrayList = New ChassisMasterFacade(User).RetrieveByCriteria(crits)

            'If arlChassisMaster.Count > 0 Then
            '    For Each chassis As ChassisMaster In arlChassisMaster
            '        If chassisMasterIDs.Length > 0 Then
            '            chassisMasterIDs += ", "
            '        End If
            '        chassisMasterIDs += chassis.ID.ToString()
            '    Next
            'End If
        End If

        SearchChassisMaster(strCityIds, False)
    End Sub

    Private Sub ReadData()
        Dim arl As New ArrayList
        sessHelp.SetSession("InvoiceResList", arl)

        Dim strCityIDs As String = String.Empty
        If ddlProvince.SelectedValue <> "" Then
            If ddlCity.SelectedValue <> "" Then
                strCityIDs = ddlCity.SelectedValue
            Else
                Dim CityCrits As New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                CityCrits.opAnd(New Criteria(GetType(KTB.DNet.Domain.City), "Province.ID", MatchType.Exact, ddlProvince.SelectedValue))

                Dim arlCities As ArrayList = New CityFacade(User).Retrieve(CityCrits)
                For Each cityItem As City In arlCities
                    If strCityIDs.Length > 0 Then
                        strCityIDs += ","
                    End If
                    strCityIDs += cityItem.ID.ToString()
                Next
            End If
        End If


        If rdoBasedOnDealer.Checked Then
            SearchByDealer(strCityIDs)
        Else
            SearchByCustomer(strCityIDs)
        End If

        If CType(sessHelp.GetSession("InvoiceResList"), ArrayList).Count > 0 Then
            btnDnLoad.Enabled = _DownloadAllowed
            btnSAP.Enabled = _TransferAllowed
        Else
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
            btnDnLoad.Enabled = False
            btnSAP.Enabled = False
        End If
    End Sub

    Private Sub BindPage(ByVal pageIndex As Integer)
        Dim InvoiceResList As ArrayList = CType(sessHelp.GetSession("InvoiceResList"), ArrayList)
        If InvoiceResList.Count <> 0 Then
            SortListControl(InvoiceResList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(InvoiceResList, pageIndex, dgInvoiceList.PageSize)
            dgInvoiceList.DataSource = PagedList
            dgInvoiceList.VirtualItemCount = InvoiceResList.Count()
            dgInvoiceList.DataBind()
        Else
            dgInvoiceList.DataSource = New ArrayList
            dgInvoiceList.VirtualItemCount = 0
            dgInvoiceList.CurrentPageIndex = 0
            dgInvoiceList.DataBind()
        End If
    End Sub

    Private Sub SortListControl(ByRef pCompletelist As ArrayList, ByVal SortColumn As String, _
                                ByVal SortDirection As Integer)
    End Sub

    Private Sub dgInvoiceList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgInvoiceList.SortCommand
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

        '-- Bind page-1
        dgInvoiceList.CurrentPageIndex = 0
        ReadData()
        BindPage(dgInvoiceList.CurrentPageIndex)

    End Sub

    Private Sub dgInvoiceList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgInvoiceList.ItemCommand
        Dim lblCat As Label
        Dim cat As String
        'Todo session
        Session("PREVPAGE") = "FrmChassisMasterProfile.aspx"
        If (e.CommandName = "lnkView") Or (e.CommandName = "lnkEdit") Then
            lblCat = CType(e.Item.FindControl("lblCat"), Label)
            cat = lblCat.Text
        End If
        If e.CommandName = "lnkView" Then
            storeCriteria()
            Response.Redirect("FrmMasterProfile.aspx?Cat=" & cat & "&adsfadfadfw32342412412412424=0&kjhadshkf9784323247832493ihdufiue=17&hjkadsdhhdhkjdafkhdafklhdfahdsajklsdfjdsjjkldsaf9874553983423=" & e.Item.Cells(0).Text.Trim)
        End If
        If e.CommandName = "lnkEdit" Then
            storeCriteria()
            Response.Redirect("FrmMasterProfile.aspx?Cat=" & cat & "&adsfadfadfw32342412412412424=1&kjhadshkf9784323247832493ihdufiue=17&hjkadsdhhdhkjdafkhdafklhdfahdsajklsdfjdsjjkldsaf9874553983423=" & e.Item.Cells(0).Text.Trim)
        End If
    End Sub

    Private Sub dgInvoiceList_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgInvoiceList.ItemDataBound
        Dim RowValue As ChassisMaster = CType(e.Item.DataItem, ChassisMaster)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (dgInvoiceList.CurrentPageIndex * dgInvoiceList.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No
            Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
            Try
                lblDealer.ToolTip = RowValue.Dealer.SearchTerm1  '-- Bind searchTerm1 as tooltip
            Catch ex As Exception
            End Try
            Dim lblKodePelanggan As Label = CType(e.Item.FindControl("lblKodePelanggan"), Label)
            Dim lblNamaPelanggan As Label = CType(e.Item.FindControl("lblNamaPelanggan"), Label)
            Dim lblProfilePelanggan As Label = CType(e.Item.FindControl("lblProfilePelanggan"), Label)
            If Not lblProfilePelanggan Is Nothing Then
                lblProfilePelanggan.Text = "No"
            End If

            Dim LinkEdit As LinkButton = CType(e.Item.FindControl("LinkEdit"), LinkButton)
            Dim lnkView As LinkButton = CType(e.Item.FindControl("lnkView"), LinkButton)
            If RowValue.ChassisMasterProfiles.Count > 0 Then
                lnkView.Visible = True
            Else
                lnkView.Visible = False
            End If
            objDealer = Session("DEALER")
            'Enhance
            'If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            LinkEdit.Visible = False
            'Else
            ' LinkEdit.Visible = _EditAllowed
            ' End If

        If Not lblKodePelanggan Is Nothing Then
            If Not RowValue.EndCustomer Is Nothing Then
                If Not RowValue.EndCustomer.Customer Is Nothing Then
                        lblKodePelanggan.Text = RowValue.EndCustomer.Customer.Code
                        lblNamaPelanggan.Text = RowValue.EndCustomer.Customer.Name1
                    If Not RowValue.EndCustomer.Customer.CustomerProfiles Is Nothing Then
                        If Not lblProfilePelanggan Is Nothing Then
                            lblProfilePelanggan.Text = "Yes"
                        End If
                    End If
                End If
            End If
            End If
        End If
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim InvoiceList As New ArrayList  '-- List of invoices selected
        For Each item As DataGridItem In dgInvoiceList.Items
            If CType(item.FindControl("chkSelect"), CheckBox).Checked Then
                Dim id As Integer = item.Cells(0).Text  '-- ID column
                Dim Invoice As ChassisMaster = New ChassisMasterFacade(User).Retrieve(id)  '-- Retrieve this invoice based on ID
                If Invoice.FakturStatus = EnumChassisMaster.FakturStatus.Validasi Then
                    If Not IsNothing(Invoice.EndCustomer) Then
                        Invoice.FakturStatus = EnumChassisMaster.FakturStatus.Konfirmasi  '-- Change invoice status
                        Invoice.EndCustomer.ConfirmBy = User.Identity.Name  '-- Set its confirmator
                        Invoice.EndCustomer.ConfirmTime = Date.Now  '-- Set its confirmation date
                        InvoiceList.Add(Invoice)  '-- Add to list of invoices
                    End If
                End If
            End If
        Next

        '-- If there exists at least an invoice selected then do update transaction
        If InvoiceList.Count > 0 Then
            Dim ChassisFac As New ChassisMasterFacade(User)
            ChassisFac.UpdateTransaction(InvoiceList)  '-- Update list of invoice selected
            ReadData()  '-- Re-read all data to refresh changes
            BindPage(dgInvoiceList.CurrentPageIndex) '-- Re-bind current page
            MessageBox.Show("Konfirmasi data berhasil")
        Else
            MessageBox.Show("Konfirmasi data tidak bisa dilakukan\nkarena status faktur bukan 'validasi'")
        End If
    End Sub

    Private Sub btnTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TransferDeTransfer(True)
    End Sub

    Private Sub dgInvoiceList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgInvoiceList.PageIndexChanged
        dgInvoiceList.CurrentPageIndex = e.NewPageIndex
        BindPage(e.NewPageIndex)
    End Sub

    Private Sub btnDnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDnLoad.Click
        '-- Generate random number [0..9999]
        Dim sSuffix As String = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond
        '-- Temp file must be a randomly named text file!
        Dim InvoiceData As String = String.Empty

        Dim crit As Hashtable = New Hashtable
        crit = CType(Session("CriteriaFormInvoiceResultList"), Hashtable)
        Dim IsBasedOnDealer As Boolean = False
        If Not crit Is Nothing Then
            IsBasedOnDealer = CBool(crit("rdoDealer"))
        End If

        Dim strFile As String = String.Empty
        If IsBasedOnDealer Then
            strFile = "DataTemp\CustProfile_by_Dealer[" & sSuffix & "].xls"
        Else
            strFile = "DataTemp\CustProfile_by_Konsumen[" & sSuffix & "].xls"
        End If
        InvoiceData = Server.MapPath("") & "\..\" + strFile

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(InvoiceData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(InvoiceData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                DnLoadInvoiceData(sw, "LOCAL")

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If
            '-- Download data to client!
            Response.Redirect("../downloadLocal.aspx?file=" + strFile)
        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Function PopulateInvoice() As ArrayList
        Return CType(sessHelp.GetSession("InvoiceResList"), ArrayList)
    End Function

    Private Sub DnLoadInvoiceData(ByRef sw As StreamWriter, ByVal mode As String)
        Dim tab As Char  '-- Separator character <Tab>
        Dim InvoiceLine As StringBuilder = New StringBuilder  '-- Invoice line in text file
        Dim InvoiceResList As ArrayList = PopulateInvoice()

        Dim crit As Hashtable = New Hashtable
        crit = CType(Session("CriteriaFormInvoiceResultList"), Hashtable)
        Dim IsBasedOnDealer As Boolean = False
        Dim temp As String = String.Empty
        If mode = "SAP" Then
            tab = Chr(9)
            For Each objInvoice As ChassisMaster In InvoiceResList
                For Each objChassisMasterProfile As ChassisMasterProfile In objInvoice.ChassisMasterProfiles
                    InvoiceLine.Append(objInvoice.ChassisNumber + tab)
                    InvoiceLine.Append(objChassisMasterProfile.ProfileHeader.Code + tab)
                    temp = objChassisMasterProfile.ProfileValue.Trim
                    InvoiceLine.Append(temp.Trim)
                    InvoiceLine.Append(vbNewLine)
                    temp = String.Empty
                Next
            Next
        Else
            tab = Chr(9)
            If Not crit Is Nothing Then
                IsBasedOnDealer = CBool(crit("rdoDealer"))
            End If

            If IsBasedOnDealer Then
                InvoiceLine.Append("Kode Dealer" + tab)
                ' Modified by Ikhsan, 20081127
                ' Requested by Rina as Part of CR
                ' Adding New Column for Downloaded File
                ' Start -------------------------------
                InvoiceLine.Append("Nama Dealer" + tab)
                ' End ---------------------------------
                InvoiceLine.Append("Area" + tab)
            Else
                InvoiceLine.Append("Kode Customer" + tab)
                ' Modified by Ikhsan, 20081127
                ' Requested by Rina as Part of CR
                ' Adding New Column for Downloaded File
                ' Start -------------------------------
                InvoiceLine.Append("Nama Customer" + tab)
                ' End ---------------------------------
            End If
            InvoiceLine.Append("Provinsi" + tab)
            InvoiceLine.Append("Kota" + tab)
            ' Modified by Ikhsan, 20081127
            ' Requested by Rina as Part of CR
            ' Adding New Column for Downloaded File
            ' Start -------------------------------
            'InvoiceLine.Append("Kode Warna" + tab)
            InvoiceLine.Append("Tipe" + tab)
            InvoiceLine.Append("Deskripsi Kendaraan" + tab)
            ' End ---------------------------------
            InvoiceLine.Append("Kategori" + tab)
            InvoiceLine.Append("Chassis" + tab)
            'InvoiceLine.Append("Deskripsi Profile" + tab)
            'InvoiceLine.Append("Value" + tab)

            Dim criterias As New CriteriaComposite(New Criteria(GetType(ProfileHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ProfileHeader), "ID", MatchType.InSet, "(1,2,3,4,5,6,7,8,9)"))

            Dim arrList As ArrayList = New ProfileHeaderFacade(User).Retrieve(criterias)

            For Each objProfileHeader As ProfileHeader In arrList
                InvoiceLine.Append(objProfileHeader.Description + tab)
            Next

            InvoiceLine.Append(vbNewLine)

            For Each objInvoice As ChassisMaster In InvoiceResList
                If IsBasedOnDealer Then
                    InvoiceLine.Append(objInvoice.Dealer.DealerCode + tab)
                    ' Modified by Ikhsan, 20081127
                    ' Requested by Rina as Part of CR
                    ' Adding New Column for Downloaded File
                    ' Start -------------------------------
                    InvoiceLine.Append(objInvoice.Dealer.DealerName + tab)
                    ' End ---------------------------------
                    If Not objInvoice.Dealer.Area1 Is Nothing Then
                        InvoiceLine.Append(objInvoice.Dealer.Area1.Description + tab)
                    Else
                        InvoiceLine.Append(String.Empty + tab)
                    End If
                    InvoiceLine.Append(objInvoice.Dealer.Province.ProvinceName + tab)
                    InvoiceLine.Append(objInvoice.Dealer.City.CityName + tab)
                Else
                    InvoiceLine.Append(objInvoice.EndCustomer.Customer.Code + tab)
                    ' Modified by Ikhsan, 20081127
                    ' Requested by Rina as Part of CR
                    ' Adding New Column for Downloaded File
                    ' Start -------------------------------
                    InvoiceLine.Append(objInvoice.EndCustomer.Customer.Name1 + tab)
                    ' End ---------------------------------
                    InvoiceLine.Append(objInvoice.EndCustomer.Customer.City.Province.ProvinceName + tab)
                    InvoiceLine.Append(objInvoice.EndCustomer.Customer.City.CityName + tab)
                End If
                ' Modified by Ikhsan, 20081127
                ' Requested by Rina as Part of CR
                ' Adding New Column for Downloaded File
                ' Start -------------------------------
                'InvoiceLine.Append(objInvoice.VechileColor.ColorEngName + tab)
                InvoiceLine.Append(objInvoice.VechileColor.VechileType.VechileTypeCode + tab)
                InvoiceLine.Append(objInvoice.VechileColor.MaterialDescription + tab)
                ' End ---------------------------------
                InvoiceLine.Append(objInvoice.Category.Description + tab)
                InvoiceLine.Append(objInvoice.ChassisNumber + tab)

                Dim strLine As String = String.Empty

                For Each objChassisMasterProfile As ChassisMasterProfile In objInvoice.ChassisMasterProfiles
                    If objChassisMasterProfile.ProfileHeader.ID = 1 Then
                        strLine = InsertData(objChassisMasterProfile)
                        Exit For
                    End If
                Next

                InvoiceLine.Append(strLine + tab)
                strLine = String.Empty

                For Each objChassisMasterProfile As ChassisMasterProfile In objInvoice.ChassisMasterProfiles
                    If objChassisMasterProfile.ProfileHeader.ID = 2 Then
                        strLine = InsertData(objChassisMasterProfile)
                        Exit For
                    End If
                Next


                InvoiceLine.Append(strLine + tab)
                strLine = String.Empty

                For Each objChassisMasterProfile As ChassisMasterProfile In objInvoice.ChassisMasterProfiles
                    If objChassisMasterProfile.ProfileHeader.ID = 3 Then
                        strLine = InsertData(objChassisMasterProfile)
                        Exit For
                    End If
                Next

                InvoiceLine.Append(strLine + tab)
                strLine = String.Empty

                For Each objChassisMasterProfile As ChassisMasterProfile In objInvoice.ChassisMasterProfiles
                    If objChassisMasterProfile.ProfileHeader.ID = 4 Then
                        strLine = InsertData(objChassisMasterProfile)
                        Exit For
                    End If
                Next

                InvoiceLine.Append(strLine + tab)
                strLine = String.Empty

                For Each objChassisMasterProfile As ChassisMasterProfile In objInvoice.ChassisMasterProfiles
                    If objChassisMasterProfile.ProfileHeader.ID = 5 Then
                        strLine = InsertData(objChassisMasterProfile)
                        Exit For
                    End If
                Next

                InvoiceLine.Append(strLine + tab)
                strLine = String.Empty

                For Each objChassisMasterProfile As ChassisMasterProfile In objInvoice.ChassisMasterProfiles
                    If objChassisMasterProfile.ProfileHeader.ID = 6 Then
                        strLine = InsertData(objChassisMasterProfile)
                        Exit For
                    End If
                Next

                InvoiceLine.Append(strLine + tab)
                strLine = String.Empty

                For Each objChassisMasterProfile As ChassisMasterProfile In objInvoice.ChassisMasterProfiles
                    If objChassisMasterProfile.ProfileHeader.ID = 7 Then
                        strLine = InsertData(objChassisMasterProfile)
                        Exit For
                    End If
                Next

                InvoiceLine.Append(strLine + tab)
                strLine = String.Empty

                For Each objChassisMasterProfile As ChassisMasterProfile In objInvoice.ChassisMasterProfiles
                    If objChassisMasterProfile.ProfileHeader.ID = 8 Then
                        strLine = InsertData(objChassisMasterProfile)
                        Exit For
                    End If
                Next

                InvoiceLine.Append(strLine + tab)
                strLine = String.Empty

                For Each objChassisMasterProfile As ChassisMasterProfile In objInvoice.ChassisMasterProfiles
                    If objChassisMasterProfile.ProfileHeader.ID = 9 Then
                        strLine = InsertData(objChassisMasterProfile)
                        Exit For
                    End If
                Next

                InvoiceLine.Append(strLine)
                InvoiceLine.Append(vbNewLine)

            Next


            'For Each objInvoice As ChassisMaster In InvoiceResList
            '    For Each objChassisMasterProfile As ChassisMasterProfile In objInvoice.ChassisMasterProfiles
            '        If IsBasedOnDealer Then
            '            InvoiceLine.Append(objInvoice.Dealer.DealerCode + tab)
            '            ' Modified by Ikhsan, 20081127
            '            ' Requested by Rina as Part of CR
            '            ' Adding New Column for Downloaded File
            '            ' Start -------------------------------
            '            InvoiceLine.Append(objInvoice.Dealer.DealerName + tab)
            '            ' End ---------------------------------
            '            If Not objInvoice.Dealer.Area1 Is Nothing Then
            '                InvoiceLine.Append(objInvoice.Dealer.Area1.Description + tab)
            '            Else
            '                InvoiceLine.Append(String.Empty + tab)
            '            End If
            '            InvoiceLine.Append(objInvoice.Dealer.Province.ProvinceName + tab)
            '            InvoiceLine.Append(objInvoice.Dealer.City.CityName + tab)
            '        Else
            '            InvoiceLine.Append(objInvoice.EndCustomer.Customer.Code + tab)
            '            ' Modified by Ikhsan, 20081127
            '            ' Requested by Rina as Part of CR
            '            ' Adding New Column for Downloaded File
            '            ' Start -------------------------------
            '            InvoiceLine.Append(objInvoice.EndCustomer.Customer.Name1 + tab)
            '            ' End ---------------------------------
            '            InvoiceLine.Append(objInvoice.EndCustomer.Customer.City.Province.ProvinceName + tab)
            '            InvoiceLine.Append(objInvoice.EndCustomer.Customer.City.CityName + tab)
            '        End If
            '        ' Modified by Ikhsan, 20081127
            '        ' Requested by Rina as Part of CR
            '        ' Adding New Column for Downloaded File
            '        ' Start -------------------------------
            '        'InvoiceLine.Append(objInvoice.VechileColor.ColorEngName + tab)
            '        InvoiceLine.Append(objInvoice.VechileColor.VechileType.VechileTypeCode + tab)
            '        InvoiceLine.Append(objInvoice.VechileColor.MaterialDescription + tab)
            '        ' End ---------------------------------
            '        InvoiceLine.Append(objInvoice.Category.Description + tab)
            '        InvoiceLine.Append(objInvoice.ChassisNumber + tab)
            '        InvoiceLine.Append(objChassisMasterProfile.ProfileHeader.Description.Trim + tab)
            '        Dim tempVal As String = objChassisMasterProfile.ProfileValue.Trim.Replace(tab, "")
            '        If objChassisMasterProfile.ProfileHeader.ProfileDetails.Count > 0 Then
            '            For Each item As ProfileDetail In objChassisMasterProfile.ProfileHeader.ProfileDetails
            '                If item.Code = objChassisMasterProfile.ProfileValue Then
            '                    tempVal = item.Description.Trim.Replace(tab, "")
            '                End If
            '            Next
            '        End If
            '        InvoiceLine.Append(tempVal)
            '        InvoiceLine.Append(vbNewLine)
            '    Next
            'Next
        End If

        sw.WriteLine(InvoiceLine.ToString())
    End Sub

    Private Function InsertData(ByVal objChassisMasterProfile As ChassisMasterProfile) As String
        Dim tab As Char
        Dim tempVal As String = objChassisMasterProfile.ProfileValue.Trim.Replace(tab, "")
        If objChassisMasterProfile.ProfileHeader.ProfileDetails.Count > 0 Then
            For Each item As ProfileDetail In objChassisMasterProfile.ProfileHeader.ProfileDetails
                If item.Code = objChassisMasterProfile.ProfileValue Then
                    tempVal = item.Description.Trim.Replace(tab, "")
                End If
            Next
        End If

        Return tempVal
    End Function

    Private Sub btnRetransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TransferDeTransfer(False)
    End Sub

    Private Sub ddlProvince_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlProvince.SelectedIndexChanged
        If ddlProvince.SelectedValue = "" Then
            ddlCity.Items.Clear()
            ddlCity.Items.Add(New ListItem("Silahkah Pilih", ""))
        Else
            CommonFunction.BindCity(ddlCity, User, True, ddlProvince.SelectedValue, False)
        End If
    End Sub


    Private Sub btnSAP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSAP.Click
        Dim crit As Hashtable = New Hashtable
        crit = CType(Session("CriteriaFormInvoiceResultList"), Hashtable)
        Dim filename = String.Format("{0}{1}{2}", "csprof", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\" & filename  '-- Destination file
        If Transfer(DestFile) Then
            MessageBox.Show("Data berhasil diupload ke SAP")
        Else
            MessageBox.Show("Data gagal diupload ke SAP")
        End If
    End Sub

    Private Function Transfer(ByVal DestFile As String) As Boolean
        Dim success As Boolean = False
        Dim sw As StreamWriter
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim finfo As New FileInfo(DestFile)
        Try
            success = imp.Start()
            If success Then
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If
                If finfo.Exists Then
                    finfo.Delete()
                End If
                Dim fs As FileStream = New FileStream(DestFile, FileMode.CreateNew)
                sw = New StreamWriter(fs)
                DnLoadInvoiceData(sw, "SAP")
                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            sw.Close()
            Throw ex
            Return success
        End Try
        Return success
    End Function
#End Region

End Class


