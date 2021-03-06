#Region " Summary "
'--------------------------------------------'
'-- Program Code : FrmWSCStatusList.aspx   --'
'-- Program Name : Daftar Status WSC       --'
'-- Description  :                         --'
'--------------------------------------------'
'-- Programmer   : Agus Pirnadi            --'
'-- Start Date   : Oct 25 2005             --'
'-- Update By    :                         --'
'-- Last Update  : Jan 24 2005             --'
'--------------------------------------------'
'-- Copyright ? 2005 by Intimedia          --'
'--------------------------------------------'
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
Imports System.Guid

Imports ICSharpCode.SharpZipLib.Core
Imports ICSharpCode.SharpZipLib.Zip
#End Region

#Region " Custom Namespace Imports "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

Public Class FrmWSCStatusList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeDealerBranch As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealerBranch As System.Web.UI.WebControls.Label
    Protected WithEvents lblClaimNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon4 As System.Web.UI.WebControls.Label
    Protected WithEvents txtClaimNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblColon2 As System.Web.UI.WebControls.Label
    Protected WithEvents icStartKirim As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEndKirim As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblVehicleType As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon5 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlVehicleType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents icStartProses As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEndProses As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblColon6 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgStatusList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents chkKirim As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkProses As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPath As System.Web.UI.WebControls.TextBox
    Protected WithEvents divPath As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlEvidenceType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblCategory As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblCategory2 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents ddlJenisWSC As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStatusWSC As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label

    Protected WithEvents chkRelease As System.Web.UI.WebControls.CheckBox
    Protected WithEvents icStartRelease As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEndRelease As KTB.DNet.WebCC.IntiCalendar
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        isAuthDelete = GetDeletePrivilege()
    End Sub

#End Region

#Region " Variable "
    Private WSCStatusList As ArrayList
    Private sessHelp As New SessionHelper
    Private objDealer As Dealer
    Private isAuthDelete As Boolean


#End Region

#Region " Custom Method "

    Private Function GetDeletePrivilege()
        Dim bReturn As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.WSCHapus_data_Privilege) Then
            bReturn = False
        Else
            bReturn = True
        End If
        Return bReturn
    End Function


    Private Function GetEditrivilege()
        Dim bReturn As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.WSCSaveData_Privilege) Then
            bReturn = False
        Else
            bReturn = True
        End If
        Return bReturn
    End Function

    Private Sub BindDropdownList()

        Dim listItemBlank As ListItem  '-- dummy item denoting ALL

        '-- Vehicle type criteria & sort
        Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.Exact, "A"))
        Dim sortColl2 As SortCollection = New SortCollection
        sortColl2.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))  '-- Sort by vechile type code
        Dim VehicleTypes As ArrayList = New VechileTypeFacade(User).RetrieveByCriteria(criterias2, sortColl2)

        '-- Vehicle type dropdownlist
        listItemBlank = New ListItem("Pilih", -1)

        ddlVehicleType.Items.Clear()
        ddlVehicleType.DataSource = New CategoryFacade(User).RetrieveActiveList("MMC")
        ddlVehicleType.DataTextField = "CategoryCode"
        ddlVehicleType.DataValueField = "ID"
        ddlVehicleType.DataBind()
        ddlVehicleType.Items.Insert(0, New ListItem("Pilih", -1))
        'ddlVehicleType.Items.Add(listItemBlank)
        'For Each item As VechileType In VehicleTypes
        '    Dim listItem As New ListItem(item.VechileTypeCode, item.ID)
        '    ddlVehicleType.Items.Add(listItem)
        'Next

        Dim listItemBlank2 As ListItem  '-- dummy item denoting ALL

        '-- Claim status dropdownlist
        'listItemBlank2 = New ListItem("Pilih", "")
        'ddlStatus.Items.Add(listItemBlank2)
        'For Each item As ListItem In LookUp.ArrayClaimStatus
        '    ddlStatus.Items.Add(item)
        'Next
        Dim arlWscStatus As ArrayList = LookUp.RetriveWSCStatus

        objDealer = Session("DEALER")
        If objDealer.Title <> EnumDealerTittle.DealerTittle.DEALER Then
            arlWscStatus.RemoveAt(0)
        End If
        ddlStatus.DataSource = arlWscStatus
        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataBind()
        ddlStatus.Items.Insert(0, New ListItem("Silakan Pilih", ""))

        '-- Category criteria & sort
        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'Dim sortColl As SortCollection = New SortCollection
        'sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code


        ''LOC 2014-09-04
        '' By ali
        '' Desc : Categori Code
        '-- Bind Category dropdownlist
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        ddlCategory.DataSource = New CategoryFacade(User).RetrieveActiveList(companyCode)
        ddlCategory.DataValueField = "CategoryCode"
        ddlCategory.DataTextField = "CategoryCode"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, "Pilih")
        ''End of LOC 2014-09-04

    End Sub

    Private Sub BindWSCEvidenceType()
        Dim _enumWSCEvidenceType As New EnumWSCEvidenceType
        Dim _arrTmp As New ArrayList
        _arrTmp = _enumWSCEvidenceType.WSCEvidenceTypeList

        ddlEvidenceType.DataSource = _arrTmp
        ddlEvidenceType.DataTextField = "WSCEvidenceTypeId"
        ddlEvidenceType.DataValueField = "WSCEvidenceTypeValue"
        ddlEvidenceType.DataBind()
        ddlEvidenceType.Items.Insert(0, "Pilih")
    End Sub

    Private Sub InitDate()
        '-- Init date

        icStartKirim.Value = New Date(Now.Year, Now.Month, Now.Day)  '-- Set start date with 01/01/Current-year
        icEndKirim.Value = New Date(Now.Year, Now.Month, Now.Day)    '-- End date with today's date

        icStartProses.Value = New Date(Now.Year, Now.Month, Now.Day)  '-- Set start date with 01/01/Current-year
        icEndProses.Value = New Date(Now.Year, Now.Month, Now.Day)    '-- End date with today's date

    End Sub

#End Region

#Region " EventHandler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            '--Check User Privilege
            If Not SecurityProvider.Authorize(Context.User, SR.WSCSTatusViewList_Privelege) Then
                Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar Status WSC")
            End If

            BindDropdownList()  '-- Init dropdownlist the first time this form is loaded
            BindWSCEvidenceType() '--init evidence type
            InitDate()  '-- Init date

            ViewState("CurrentSortColumn") = "ClaimNumber"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            Dim isKTB As Boolean = IIf(CType(Session("LOGINUSERINFO"), UserInfo).Dealer.Title = EnumDealerTittle.DealerTittle.KTB, True, False)

            lblCategory.Visible = isKTB
            lblCategory2.Visible = isKTB
            ddlCategory.Visible = isKTB

            For IC As Integer = 0 To dgStatusList.Columns.Count - 1
                If dgStatusList.Columns(IC).HeaderText.ToLower() = "kategori" Then
                    dgStatusList.Columns(IC).Visible = isKTB
                End If

            Next

            If Not IsNothing(sessHelp.GetSession("CriteriaFormWSCStatusList")) Then
                Dim criteria As Hashtable
                criteria = CType(sessHelp.GetSession("CriteriaFormWSCStatusList"), Hashtable)
                txtKodeDealer.Text = CType(criteria("DealerCode"), String)
                icStartKirim.Value = CType(criteria("StartKirim"), Date)
                icEndKirim.Value = CType(criteria("EndKirim"), Date)
                icStartProses.Value = CType(criteria("StartProses"), Date)
                icEndProses.Value = CType(criteria("EndProses"), Date)
                ddlStatus.SelectedValue = CType(criteria("Status"), String)
                ddlVehicleType.SelectedValue = CType(criteria("VehicleType"), String)
                icStartRelease.Value = CType(criteria("StartRelease"), Date)
                icEndRelease.Value = CType(criteria("EndRelease"), Date)

                If isKTB Then
                    ddlCategory.SelectedValue = CType(criteria("CategoryCode"), String)
                End If

                Me.txtClaimNo.Text = CType(criteria("ClaimNumber"), String)
                'RetrieveAllData() '-- Retrieve all data without paging
                Dim _pageIndex As Integer = CType(criteria("PageIndex"), Integer)
                dgStatusList.CurrentPageIndex = _pageIndex
                BindDatagrid(_pageIndex)
            Else
                btnSearch_Click(sender, e)
            End If
        End If

        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        lblSearchDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"
    End Sub

    Private Sub BindDatagrid(ByVal currentPageIndex As Integer)

        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(WSCHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        CreateCriteria(criterias)

        Dim totalRow As Integer = 0
        '-- Retrieve recordset
        WSCStatusList = New WSCHeaderFacade(User).RetrieveActiveList(criterias, currentPageIndex, dgStatusList.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        If WSCStatusList.Count = 0 Then
            '-- Bind and display
            dgStatusList.DataSource = New ArrayList

            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("WSC"))
            End If
        Else
            '-- Bind and display
            dgStatusList.DataSource = WSCStatusList

        End If

        dgStatusList.VirtualItemCount = totalRow
        dgStatusList.DataBind()


    End Sub

    Private Sub CreateCriteria(ByRef criterias As CriteriaComposite)
        '-- Dealer code
        If txtKodeDealer.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        End If

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCHeader), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If chkKirim.Checked Then
            '-- Periode kirim
            Dim StartKirim As New DateTime(CInt(icStartKirim.Value.Year), CInt(icStartKirim.Value.Month), CInt(icStartKirim.Value.Day), 0, 0, 0)
            Dim EndKirim As New DateTime(CInt(icEndKirim.Value.Year), CInt(icEndKirim.Value.Month), CInt(icEndKirim.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(WSCHeader), "CreatedTime", MatchType.GreaterOrEqual, Format(StartKirim, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(WSCHeader), "CreatedTime", MatchType.LesserOrEqual, Format(EndKirim, "yyyy-MM-dd HH:mm:ss")))
        End If

        If chkProses.Checked Then
            '-- Periode proses
            Dim StartProses As New DateTime(CInt(icStartProses.Value.Year), CInt(icStartProses.Value.Month), CInt(icStartProses.Value.Day), 0, 0, 0)
            Dim EndProses As New DateTime(CInt(icEndProses.Value.Year), CInt(icEndProses.Value.Month), CInt(icEndProses.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(WSCHeader), "DecideDate", MatchType.GreaterOrEqual, Format(StartProses, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(WSCHeader), "DecideDate", MatchType.LesserOrEqual, Format(EndProses, "yyyy-MM-dd HH:mm:ss")))
        End If

        If chkRelease.Checked Then
            '-- Periode Release
            Dim StartRelease As New DateTime(CInt(icStartRelease.Value.Year), CInt(icStartRelease.Value.Month), CInt(icStartRelease.Value.Day), 0, 0, 0)
            Dim EndRelease As New DateTime(CInt(icEndRelease.Value.Year), CInt(icEndRelease.Value.Month), CInt(icEndRelease.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ReleaseDate", MatchType.GreaterOrEqual, Format(StartRelease, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ReleaseDate", MatchType.LesserOrEqual, Format(EndRelease, "yyyy-MM-dd HH:mm:ss")))
        End If

        '-- Nomor klaim (LIKE '%%')
        If txtClaimNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ClaimNumber", MatchType.[Partial], txtClaimNo.Text.Trim()))
        End If

        '-- Tipe Kendaraan
        If ddlVehicleType.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ChassisMaster.VechileColor.VechileType.ID", MatchType.Exact, ddlVehicleType.SelectedValue))
        End If

        '-- Status klaim
        If ddlStatus.SelectedValue <> "" Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        '-- Evidence Type
        If ddlEvidenceType.SelectedValue <> "Pilih" Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ID", MatchType.InSet, "(select WSCHeaderID from WSCEvidence where RowStatus = 0 and EvidenceType=" & ddlEvidenceType.SelectedValue & ")"))
        End If

        If CType(Session("LOGINUSERINFO"), UserInfo).Dealer.Title = EnumDealerTittle.DealerTittle.KTB AndAlso ddlCategory.SelectedValue <> "Pilih" Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ChassisMaster.Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))
        End If

        If ddlJenisWSC.SelectedValue <> "Pilih" Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ClaimType", MatchType.Exact, ddlJenisWSC.SelectedValue))
        End If

        If ddlStatusWSC.SelectedValue <> "Pilih" Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ClaimStatus", MatchType.Exact, ddlStatusWSC.SelectedValue))
        End If

        If txtKodeDealerBranch.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "DealerBranch.DealerBranchCode", MatchType.InSet, "('" & txtKodeDealerBranch.Text.Replace(";", "','") & "')"))
        End If

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "Status", MatchType.No, CType(enumStatusWSC.Status.Baru, Integer)))
        End If
    End Sub

    Private Sub RetrieveAllData()

        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(WSCHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        CreateCriteria(criterias)
        CreateCriteriaForAllData(criterias)
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "Status", MatchType.No, CType(enumStatusWSC.Status.Baru, Integer)))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(WSCHeader), "Dealer.DealerCode", Sort.SortDirection.ASC))  '-- Kode dealer
        sortColl.Add(New Sort(GetType(WSCHeader), "ClaimNumber", Sort.SortDirection.ASC))        '-- Nomor klaim

        '-- Retrieve recordset
        Dim WSCAllStatusList As ArrayList = New WSCHeaderFacade(User).RetrieveByCriteria(criterias, sortColl)

        '-- Save into session
        sessHelp.SetSession("WSCAllStatusList", WSCAllStatusList)

    End Sub

    Private Sub CreateCriteriaForAllData(ByRef criterias As CriteriaComposite)
        '-- Dealer code
        If txtKodeDealer.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        End If

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCHeader), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If chkKirim.Checked Then
            '-- Periode kirim
            Dim StartKirim As New DateTime(CInt(icStartKirim.Value.Year), CInt(icStartKirim.Value.Month), CInt(icStartKirim.Value.Day), 0, 0, 0)
            Dim EndKirim As New DateTime(CInt(icEndKirim.Value.Year), CInt(icEndKirim.Value.Month), CInt(icEndKirim.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(WSCHeader), "CreatedTime", MatchType.GreaterOrEqual, Format(StartKirim, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(WSCHeader), "CreatedTime", MatchType.LesserOrEqual, Format(EndKirim, "yyyy-MM-dd HH:mm:ss")))
        End If

        If chkProses.Checked Then
            '-- Periode proses
            Dim StartProses As New DateTime(CInt(icStartProses.Value.Year), CInt(icStartProses.Value.Month), CInt(icStartProses.Value.Day), 0, 0, 0)
            Dim EndProses As New DateTime(CInt(icEndProses.Value.Year), CInt(icEndProses.Value.Month), CInt(icEndProses.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(WSCHeader), "DecideDate", MatchType.GreaterOrEqual, Format(StartProses, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(WSCHeader), "DecideDate", MatchType.LesserOrEqual, Format(EndProses, "yyyy-MM-dd HH:mm:ss")))
        End If

        '-- Nomor klaim (LIKE '%%')
        If txtClaimNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ClaimNumber", MatchType.[Partial], txtClaimNo.Text.Trim()))
        End If

        '-- Tipe Kendaraan
        If ddlVehicleType.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ChassisMaster.VechileColor.VechileType.ID", MatchType.Exact, ddlVehicleType.SelectedValue))
        End If

        '-- Status klaim
        If ddlStatus.SelectedValue <> "" Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        '-- Evidence Type
        If ddlEvidenceType.SelectedValue <> "Pilih" Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ID", MatchType.InSet, "(select WSCHeaderID from WSCEvidence where RowStatus = 0 and EvidenceType=" & ddlEvidenceType.SelectedValue & ")"))
        End If

        If ddlJenisWSC.SelectedValue <> "Pilih" Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ClaimType", MatchType.Exact, ddlJenisWSC.SelectedValue))
        End If

        If ddlStatusWSC.SelectedValue <> "Pilih" Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ClaimStatus", MatchType.Exact, ddlStatusWSC.SelectedValue))
        End If

        If CType(Session("LOGINUSERINFO"), UserInfo).Dealer.Title = EnumDealerTittle.DealerTittle.KTB AndAlso ddlCategory.SelectedValue <> "Pilih" Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ChassisMaster.Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))
        End If

        If ddlEvidenceType.SelectedValue <> "Pilih" Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ID", MatchType.InSet, "(select WSCHeaderID from WSCEvidence where RowStatus = 0 and EvidenceType=" & ddlEvidenceType.SelectedValue & ")"))
        End If

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        '-- Date validation
        If Not chkKirim.Checked And Not chkProses.Checked And Not chkRelease.Checked Then
            '-- At least one date range is set: Periode Kirim or Periode Proses
            MessageBox.Show("Periode Kirim, Periode Proses atau Periode Rilis harus diisi")
            Exit Sub  '-- Directly exits
        End If

        If chkKirim.Checked Then
            If icStartKirim.Value > icEndKirim.Value Then
                '-- It must be Start date <= End date
                MessageBox.Show("Interval tanggal kirim tidak valid")
                Exit Sub  '-- Directly exits
            Else
                If icEndKirim.Value.Subtract(icStartKirim.Value).Days > 65 Then
                    '-- The difference must be lesser or equal to 65 days
                    MessageBox.Show("Selisih tanggal kirim harus <= 65 hari")
                    Exit Sub  '-- Directly exits
                End If
            End If
        End If

        '-- Date validation
        If chkProses.Checked Then
            If icStartProses.Value > icEndProses.Value Then
                '-- It must be Start date <= End date
                MessageBox.Show("Interval tanggal proses tidak valid")
                Exit Sub  '-- Directly exits
            Else
                If icEndProses.Value.Subtract(icStartProses.Value).Days > 65 Then
                    '-- The difference must be lesser or equal to 65 days
                    MessageBox.Show("Selisih tanggal proses harus <= 65 hari")
                    Exit Sub  '-- Directly exits
                End If
            End If
        End If

        '-- Date validation
        If chkRelease.Checked Then
            If icStartRelease.Value > icEndRelease.Value Then
                '-- It must be Start date <= End date
                MessageBox.Show("Interval tanggal rilis tidak valid")
                Exit Sub  '-- Directly exits
            Else
                If icEndRelease.Value.Subtract(icStartRelease.Value).Days > 65 Then
                    '-- The difference must be lesser or equal to 65 days
                    MessageBox.Show("Selisih tanggal rilis harus <= 65 hari")
                    Exit Sub  '-- Directly exits
                End If
            End If
        End If

        SettingCriteria()
        RetrieveAllData() '-- Retrieve all data without paging
        dgStatusList.CurrentPageIndex = 0
        BindDatagrid(0)
        sessHelp.SetSession("PageNumber", dgStatusList.CurrentPageIndex)

    End Sub

    Private Sub dgStatusList_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgStatusList.ItemCommand

        Dim strCommand As String = String.Empty
        Dim lblPath As TextBox = FindControl("lblPath")
        strCommand = lblPath.Text

        If e.CommandName = "lnkClaimNumber" Then

            '-- Retrieve WSC header and its details of the chosen header
            Dim WSCHead As WSCHeader = New WSCHeaderFacade(User).Retrieve(CType(e.Item.Cells(0).Text, Integer))

            '-- Check to see if the record already got deleted
            If IsNothing(WSCHead) Then
                MessageBox.Show("Record ini sudah dihapus.")
                Exit Sub
            End If

            '-- Store WSC header and its details for display on form FrmWSCDetail
            sessHelp.SetSession("WSCHead", WSCHead)
            sessHelp.SetSession("PageNumber", dgStatusList.CurrentPageIndex)


            '-- Display WSC header and its details on WSC Detail Info page
            Response.Redirect("FrmWSCDetail.aspx")

        ElseIf e.CommandName = "lnkEmail" Then

            '-- Retrieve WSC header and its details of the chosen header
            Dim WSCHead As WSCHeader = New WSCHeaderFacade(User).Retrieve(CType(e.Item.Cells(0).Text, Integer))

            '-- Store WSC header and its details for display on form FrmWSCDetail
            sessHelp.SetSession("WSCHead", WSCHead)

            '-- Display WSC send email
            sessHelp.SetSession("PrevPage", Request.Url.ToString())
            Response.Redirect("FrmWSCSendEmail.aspx?type=ReadOnly")
        ElseIf e.CommandName = "lnkKwitansi" Then
            Dim linkButton As LinkButton = e.Item.FindControl("lnkKwitansi")
            'Dim fileInfox As New FileInfo(strCommand)
            Dim fileInfox
            'Try
            '    fileInfox = New FileInfo(strCommand)
            'Catch ex As Exception
            '    Dim uriString As String = New Uri(strCommand).LocalPath
            '    fileInfox = New FileInfo(uriString)
            'End Try

            If Not Request.Browser.Type.Contains("InternetExplorer") AndAlso Not strCommand.StartsWith("//") Then
                Dim idx As Integer = strCommand.IndexOf("//")
                strCommand = strCommand.Substring(idx, (strCommand.Length - idx)).Replace("/", "\")
            End If

            Dim fileExist As Boolean = CheckFileExist(strCommand)
            If fileExist Then
                Try
                    Response.Redirect("../Download.aspx?file=" & strCommand)
                Catch ex As Exception
                    MessageBox.Show(SR.DownloadFail(linkButton.Text))
                End Try
            Else
                MessageBox.Show(SR.FileNotFound(""))
            End If
        ElseIf e.CommandName = "lnkSurat" Then
            Dim linkButton As LinkButton = e.Item.FindControl("lnkSurat")
            Dim fileInfox
            'Try
            '    fileInfox = New FileInfo(strCommand)
            'Catch ex As Exception
            '    Dim uriString As String = New Uri(strCommand).LocalPath
            '    fileInfox = New FileInfo(uriString)
            'End Try
            Dim fileExist As Boolean = CheckFileExist(strCommand)
            If fileExist Then
                Try
                    Response.Redirect("../Download.aspx?file=" & strCommand)
                Catch ex As Exception
                    MessageBox.Show(SR.DownloadFail(linkButton.Text))
                End Try
            Else
                MessageBox.Show(SR.FileNotFound(""))
            End If
        ElseIf e.CommandName = "lnkTeknikal" Then
            Dim linkButton As LinkButton = e.Item.FindControl("lnkTeknikal")
            Dim fileInfox
            'Try
            '    fileInfox = New FileInfo(strCommand)
            'Catch ex As Exception
            '    Dim uriString As String = New Uri(strCommand).LocalPath
            '    fileInfox = New FileInfo(uriString)
            'End Try
            Dim fileExist As Boolean = CheckFileExist(strCommand)
            If fileExist Then
                Try
                    Response.Redirect("../Download.aspx?file=" & strCommand)
                Catch ex As Exception
                    MessageBox.Show(SR.DownloadFail(linkButton.Text))
                End Try
            Else
                MessageBox.Show(SR.FileNotFound(""))
            End If

        ElseIf e.CommandName = "lnkRepairWO" Then
            Dim linkButton As LinkButton = e.Item.FindControl("lnkRepairWO")
            Dim fileInfox
            'Try
            '    fileInfox = New FileInfo(strCommand)
            'Catch ex As Exception
            '    Dim uriString As String = New Uri(strCommand).LocalPath
            '    fileInfox = New FileInfo(uriString)
            'End Try
            Dim fileExist As Boolean = CheckFileExist(strCommand)
            If fileExist Then

                Try
                    Response.Redirect("../Download.aspx?file=" & strCommand)
                Catch ex As Exception
                    MessageBox.Show(SR.DownloadFail(linkButton.Text))
                End Try
            Else
                MessageBox.Show(SR.FileNotFound(""))
            End If
        ElseIf e.CommandName = "lnkPartBekas" Then
            Dim linkButton As LinkButton = e.Item.FindControl("lnkPartBekas")
            Dim fileInfox
            'Try
            '    fileInfox = New FileInfo(strCommand)
            'Catch ex As Exception
            '    Dim uriString As String = New Uri(strCommand).LocalPath
            '    fileInfox = New FileInfo(uriString)
            'End Try
            Dim fileExist As Boolean = CheckFileExist(strCommand)
            If fileExist Then

                Try
                    Response.Redirect("../Download.aspx?file=" & strCommand)
                Catch ex As Exception
                    MessageBox.Show(SR.DownloadFail(linkButton.Text))
                End Try
            Else
                MessageBox.Show(SR.FileNotFound(""))
            End If
        ElseIf e.CommandName = "lnkPhoto" Then
            Dim linkButton As LinkButton = e.Item.FindControl("lnkPhoto")
            Dim fileInfox
            'Try
            '    fileInfox = New FileInfo(strCommand)
            'Catch ex As Exception
            '    Dim uriString As String = New Uri(strCommand).LocalPath
            '    fileInfox = New FileInfo(uriString)
            'End Try
            Dim fileExist As Boolean = CheckFileExist(strCommand)
            If fileExist Then

                Try
                    Response.Redirect("../Download.aspx?file=" & strCommand)
                Catch ex As Exception
                    MessageBox.Show(SR.DownloadFail(linkButton.Text))
                End Try
            Else
                MessageBox.Show(SR.FileNotFound(""))
            End If
        ElseIf e.CommandName = "Edit" Then
            Dim WSCHead As WSCHeader = New WSCHeaderFacade(User).Retrieve(CType(e.CommandArgument, Integer))
            '-- Check to see if the record already got deleted
            If IsNothing(WSCHead) Then
                MessageBox.Show("Record ini sudah dihapus.")
                Exit Sub
            End If

            '-- Store WSC header and its details for display on form FrmWSCDetail
            sessHelp.SetSession("WSCHead", WSCHead)
            sessHelp.SetSession("PageNumber", dgStatusList.CurrentPageIndex)


            '-- Display WSC header and its details on WSC Detail Info page
            Server.Transfer("../Service/FrmWSCHeader.aspx?screenFrom=WSC&viewStateMode=1&WSCId=" & WSCHead.ID)

        ElseIf e.CommandName = "View" Then
            Dim WSCHead As WSCHeader = New WSCHeaderFacade(User).Retrieve(CType(e.CommandArgument, Integer))
            sessHelp.SetSession("WSCHead", WSCHead)
            '-- Check to see if the record already got deleted
            If IsNothing(WSCHead) Then
                MessageBox.Show("Record ini sudah dihapus.")
                Exit Sub
            End If

            Server.Transfer("../Service/FrmWSCHeader.aspx?screenFrom=WSC&viewStateMode=2&WSCId=" & WSCHead.ID)

        ElseIf e.CommandName = "Hapus" Then
            Try
                delete(e.Item.ItemIndex)
                MessageBox.Show(SR.DeleteSucces())
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail())
            End Try
        ElseIf e.CommandName = "lnkDownloadLampiran" Then
            Dim WSCHead As WSCHeader = New WSCHeaderFacade(User).Retrieve(CType(e.CommandArgument, Integer))
            Dim criterias As New CriteriaComposite(New Criteria(GetType(WSCEvidence), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(WSCEvidence), "WSCHeader.ID", MatchType.Exact, WSCHead.ID))
            Dim WSCEvidenceList As ArrayList = New WSCEvidenceFacade(User).Retrieve(criterias)
            If WSCEvidenceList.Count > 0 Then
                func_lnkDownloadLampiran(WSCHead, WSCEvidenceList)
            End If
        End If

    End Sub

    Protected Sub func_lnkDownloadLampiran(ByVal oWSCHeader As WSCHeader, ByVal _arrWSCEVIDENCE As ArrayList)
        Dim nameGuid As String = Guid.NewGuid().ToString().Substring(0, 5)
        If Not IsNothing(oWSCHeader.ChassisMaster.ChassisNumber) Then
            nameGuid = oWSCHeader.ChassisMaster.ChassisNumber & "_" & oWSCHeader.ClaimNumber
        End If
        Dim zipName = String.Empty
        ZipIt(_arrWSCEVIDENCE, nameGuid, zipName)
        Dim fInfo As FileInfo = New FileInfo(zipName)
        Try
            Response.Redirect("../Download.aspx?file=" & fInfo.FullName)
        Catch ex As Exception
            MessageBox.Show(SR.DownloadFail(fInfo.Name))
        End Try
    End Sub

    Private Sub ZipIt(ByVal arrLampiran As ArrayList, ByVal targetName As String, ByRef zipName As String)

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim TargetDirectory As String = KTB.DNet.Lib.WebConfig.GetValue("SAN")

        Try
            success = imp.Start()
            If success Then

                If arrLampiran.Count = 0 Then
                    MessageBox.Show("Tidak ada file yang akan didownload")
                    Return
                End If

                If targetName.Length = 0 Then
                    MessageBox.Show("Zip file name error")
                    Return
                End If

                zipName = TargetDirectory & KTB.DNet.Lib.WebConfig.GetValue("WSCEvidenceFileDirectory") & "\" & CType(Session("DEALER"), Dealer).DealerCode & "\" & targetName & ".zip"
                Dim zipInfo As FileInfo = New FileInfo(zipName)

                If Not zipInfo.Directory.Exists Then
                    zipInfo.Directory.Create()
                End If

                If zipInfo.Exists Then
                    zipInfo.Delete()
                End If

                Using strmZipOutputStream As New ZipOutputStream(File.Create(zipName))
                    strmZipOutputStream.SetLevel(9) ' Highest Compression
                    strmZipOutputStream.Finish()
                    strmZipOutputStream.Close()
                End Using


                Using zipFile As New ZipFile(zipName)
                    zipFile.BeginUpdate()

                    For Each _wscEvidence As WSCEvidence In arrLampiran
                        If _wscEvidence.IsFromPQR Then
                            Dim fInfo As FileInfo = New FileInfo(TargetDirectory & _wscEvidence.PQRFilePath)
                            zipFile.Add(fInfo.FullName)
                        Else
                            Dim fInfo As FileInfo = New FileInfo(TargetDirectory & _wscEvidence.PathFile)
                            zipFile.Add(fInfo.FullName)
                        End If
                    Next

                    zipFile.CommitUpdate()
                    zipFile.Close()
                End Using

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            imp.StopImpersonate()
            imp = Nothing
            Throw ex
        End Try
    End Sub

    Private Function CheckFileExist(ByVal fileinfo As FileInfo) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Try
            success = imp.Start()
            If success Then
                Return fileinfo.Exists
            End If
        Catch ex As Exception
            Return False
        Finally
            imp.StopImpersonate()
            imp = Nothing
        End Try

    End Function

    Private Function CheckFileExist(ByVal fileinfo As String) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Try
            success = imp.Start()
            If success Then
                Return New FileInfo(fileinfo).Exists
            End If
        Catch ex As Exception
            Return False
        Finally
            imp.StopImpersonate()
            imp = Nothing
        End Try

        Return False
    End Function

    Private Sub delete(ByVal nId As Integer)
        Dim objWSCHeaderFacade As WSCHeaderFacade = New WSCHeaderFacade(User)
        Dim objwschapus As WSCHeader = objWSCHeaderFacade.Retrieve(CInt(dgStatusList.Items(nId).Cells(0).Text))
        objWSCHeaderFacade.UpdateRowStatus(objwschapus)

        RetrieveAllData() '-- Retrieve all data without paging
        dgStatusList.CurrentPageIndex = 0
        BindDatagrid(0)
        sessHelp.SetSession("PageNumber", dgStatusList.CurrentPageIndex)
    End Sub

    Private Sub dgStatusList_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgStatusList.ItemDataBound
        '-- Handle data binding

        Dim RowValue As WSCHeader = CType(e.Item.DataItem, WSCHeader)

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '-- Grid detail items

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgStatusList.PageSize * dgStatusList.CurrentPageIndex)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            '20101102 - remark by anh 'memungkinkan untuk lebih dari 1 reason
            'Dim lblReason As Label = CType(e.Item.FindControl("lblReason"), Label)
            Dim lbtnAlasan As LinkButton = CType(e.Item.FindControl("btnAlasan"), LinkButton)
            Dim lblPartAmnt As Label = CType(e.Item.FindControl("lblPartAmnt"), Label)
            Dim lblLaborAmnt As Label = CType(e.Item.FindControl("lblLaborAmnt"), Label)
            Try
                lblStatus.Text = RowValue.StatusText
                If Not RowValue.Dealer Is Nothing Then
                    lblDealerCode.ToolTip = RowValue.Dealer.SearchTerm1  '-- Bind searchTerm1 as tooltip
                End If
                '20101102 - remark by anh 'memungkinkan untuk lebih dari 1 reason
                'If Not RowValue.Reason Is Nothing Then
                '    lblReason.ToolTip = RowValue.Reason.Description      '-- Bind reason as tooltip
                'End If

                'If Not RowValue.Reason Is Nothing Then
                '    Dim lblReason As Label = New Label
                '    lblReason.Text = RowValue.Reason.ReasonCode + "<br>"
                '    lblReason.ToolTip = RowValue.Reason.Description
                '    e.Item.Cells(3).Controls.Add(lblReason)
                'End If
                'If Not RowValue.Reason2 Is Nothing Then
                '    Dim lblReason2 As Label = New Label
                '    lblReason2.Text = RowValue.Reason2.ReasonCode + "<br>"
                '    lblReason2.ToolTip = RowValue.Reason2.Description
                '    e.Item.Cells(3).Controls.Add(lblReason2)
                'End If
                'If Not RowValue.Reason3 Is Nothing Then
                '    Dim lblReason3 As Label = New Label
                '    lblReason3.Text = RowValue.Reason3.ReasonCode + "<br>"
                '    lblReason3.ToolTip = RowValue.Reason3.Description
                '    e.Item.Cells(3).Controls.Add(lblReason3)
                'End If
                'If Not RowValue.Reason4 Is Nothing Then
                '    Dim lblReason4 As Label = New Label
                '    lblReason4.Text = RowValue.Reason4.ReasonCode + "<br>"
                '    lblReason4.ToolTip = RowValue.Reason4.Description
                '    e.Item.Cells(3).Controls.Add(lblReason4)
                'End If
                'If Not RowValue.Reason5 Is Nothing Then
                '    Dim lblReason5 As Label = New Label
                '    lblReason5.Text = RowValue.Reason5.ReasonCode + "<br>"
                '    lblReason5.ToolTip = RowValue.Reason5.Description
                '    e.Item.Cells(3).Controls.Add(lblReason5)
                'End If

                lblPartAmnt.Text = Format(RowValue.PartAmount, "#,##0")
                lblLaborAmnt.Text = Format(RowValue.LaborAmount, "#,##0")
                lbtnAlasan.Attributes("onclick") = "ShowReason(" + CType(e.Item.Cells(0).Text, String) + ")"

            Catch ex As Exception
            End Try


            '--Check Email Icon
            Dim lblEmail As LinkButton = CType(e.Item.FindControl("lnkEmail"), LinkButton)
            Dim linkbtnHapus As LinkButton = e.Item.FindControl("lbnHapus")
            linkbtnHapus.Text = "<img src=""../images/trash.gif"" border=""0"" alt=""Hapus""  onclick=""return confirm('Yakin ingin menghapus data ini?');"">"

            Dim lnkbtnEdit As LinkButton = e.Item.FindControl("lnkbtnEdit")
            Dim lnkbtnView As LinkButton = e.Item.FindControl("lnkbtnView")

            'If RowValue.StatusText = "Proses" And isAuthDelete Then
            '    linkbtnHapus.Visible = True
            'Else
            linkbtnHapus.Visible = False
            'End If

            If RowValue.StatusText = "Baru" Then
                lnkbtnEdit.Visible = True
                lnkbtnView.Visible = False
                If isAuthDelete Then
                    linkbtnHapus.Visible = True
                End If

            Else
                lnkbtnView.Visible = True

                lnkbtnEdit.Visible = False
                linkbtnHapus.Visible = False
            End If

            'Dim _id As Integer
            'Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
            'If Not objDealer Is Nothing Then
            '    _id = objDealer.ID
            'End If

            If RowValue.WSCDamageRequestParts.Count = 0 Then
                lblEmail.Visible = False
            End If

            '<img id='imgKwitansi' style="width:15px; height:15px;" alt="" onmouseout="Out();" onmouseover="Large(this)" />
            '--Image kwitansi, surat, teknikal
            Dim lbKwitansi As LinkButton = CType(e.Item.FindControl("lnkKwitansi"), LinkButton)
            Dim lbSurat As LinkButton = CType(e.Item.FindControl("lnkSurat"), LinkButton)
            Dim lbTeknikal As LinkButton = CType(e.Item.FindControl("lnkTeknikal"), LinkButton)
            Dim lnkRepairWO As LinkButton = CType(e.Item.FindControl("lnkRepairWO"), LinkButton)
            Dim lnkPartBekas As LinkButton = CType(e.Item.FindControl("lnkPartBekas"), LinkButton)
            Dim lnkPhoto As LinkButton = CType(e.Item.FindControl("lnkPhoto"), LinkButton)
            Dim lnkDownloadLampiran As LinkButton = CType(e.Item.FindControl("lnkDownloadLampiran"), LinkButton)

            lnkDownloadLampiran.Visible = False

            lbKwitansi.Visible = False
            lbSurat.Visible = False
            lbTeknikal.Visible = False

            lnkRepairWO.Visible = False
            lnkPartBekas.Visible = False
            lnkPhoto.Visible = False

            Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("WSCEvidenceFileDirectory")
            Dim fileInfo1 As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))
            Dim DestFullFilePath As String = fileInfo1.Directory.FullName '--& "\" & DestFile '-- Destination file

            Dim criterias As New CriteriaComposite(New Criteria(GetType(WSCEvidence), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(WSCEvidence), "WSCHeader.ID", MatchType.Exact, RowValue.ID))
            Dim WSCEvidenceList As ArrayList = New WSCEvidenceFacade(User).Retrieve(criterias)
            If WSCEvidenceList.Count > 0 Then
                lnkDownloadLampiran.Visible = True

                Dim _evidence As New WSCEvidence
                lbKwitansi.Text = String.Empty
                lbSurat.Text = String.Empty
                lbTeknikal.Text = String.Empty
                lnkRepairWO.Text = String.Empty
                lnkPartBekas.Text = String.Empty
                lnkPhoto.Text = String.Empty

                For Each _evidence In WSCEvidenceList
                    Dim dataFile As String = DestFullFilePath & "\" & _evidence.PathFile
                    'dataFile = dataFile.Replace("\", "/")
                    Select Case _evidence.EvidenceType
                        Case EnumWSCEvidenceType.WSCEvidenceType.KWITANSI_WSC
                            If dataFile.Substring(dataFile.Length - 4, 4).ToLower = ".jpg" Then
                                lbKwitansi.Text = lbKwitansi.Text & "<img src=""../images/download.gif"" lowsrc='" & dataFile & "' border=""0"" onmouseout =""HideEvidenceImage(this);""  onmouseover=""ShowEvidenceImage(this);"" > <br>"
                            Else
                                lbKwitansi.Text = lbKwitansi.Text & "<img src=""../images/download.gif"" border=""0"" lowsrc='" & dataFile & "' onmouseover=""SetPath(this);""> <br>"
                            End If
                            lbKwitansi.Visible = True
                        Case EnumWSCEvidenceType.WSCEvidenceType.SURAT_WSC
                            If dataFile.Substring(dataFile.Length - 4, 4).ToLower = ".jpg" Then
                                lbSurat.Text = lbSurat.Text & "<img src=""../images/download.gif"" lowsrc='" & dataFile & "' border=""0""   onmouseout=""HideEvidenceImage(this);"" onmouseover=""ShowEvidenceImage(this);"" > <br>"
                            Else
                                lbSurat.Text = lbSurat.Text & "<img src=""../images/download.gif"" border=""0"" lowsrc='" & dataFile & "' onmouseover=""SetPath(this);""> <br>"
                            End If
                            lbSurat.Visible = True
                        Case EnumWSCEvidenceType.WSCEvidenceType.TEKNIKAL_WSC
                            If dataFile.Substring(dataFile.Length - 4, 4).ToLower = ".jpg" Then
                                lbTeknikal.Text = lbTeknikal.Text & "<img src=""../images/download.gif"" lowsrc='" & dataFile & "' border=""0""   onmouseout=""HideEvidenceImage(this);"" onmouseover=""ShowEvidenceImage(this);"" >  <br>"
                            Else
                                lbTeknikal.Text = lbTeknikal.Text & "<img src=""../images/download.gif"" border=""0"" lowsrc='" & dataFile & "' onmouseover=""SetPath(this);"">  <br>"
                            End If
                            lbTeknikal.Visible = True
                        Case EnumWSCEvidenceType.WSCEvidenceType.REPAIR_WO
                            If dataFile.Substring(dataFile.Length - 4, 4).ToLower = ".jpg" Then
                                lnkRepairWO.Text = lnkRepairWO.Text & "<img src=""../images/download.gif"" lowsrc='" & dataFile & "' border=""0""   onmouseout=""HideEvidenceImage(this);"" onmouseover=""ShowEvidenceImage(this);"" >  <br>"
                            Else
                                lnkRepairWO.Text = lnkRepairWO.Text & "<img src=""../images/download.gif"" border=""0"" lowsrc='" & dataFile & "' onmouseover=""SetPath(this);"">  <br>"
                            End If
                            lnkRepairWO.Visible = True
                        Case EnumWSCEvidenceType.WSCEvidenceType.PART_BEKAS
                            If dataFile.Substring(dataFile.Length - 4, 4).ToLower = ".jpg" Then
                                lnkPartBekas.Text = lnkPartBekas.Text & "<img src=""../images/download.gif"" lowsrc='" & dataFile & "' border=""0""   onmouseout=""HideEvidenceImage(this);"" onmouseover=""ShowEvidenceImage(this);"" >  <br>"
                            Else
                                lnkPartBekas.Text = lnkPartBekas.Text & "<img src=""../images/download.gif"" border=""0"" lowsrc='" & dataFile & "' onmouseover=""SetPath(this);"">  <br>"
                            End If
                            lnkPartBekas.Visible = True
                        Case EnumWSCEvidenceType.WSCEvidenceType.PHOTO
                            If dataFile.Substring(dataFile.Length - 4, 4).ToLower = ".jpg" Then
                                lnkPhoto.Text = lnkPhoto.Text & "<img src=""../images/download.gif"" lowsrc='" & dataFile & "' border=""0""   onmouseout=""HideEvidenceImage(this);"" onmouseover=""ShowEvidenceImage(this);"" >  <br>"
                            Else
                                lnkPhoto.Text = lnkPhoto.Text & "<img src=""../images/download.gif"" border=""0"" lowsrc='" & dataFile & "' onmouseover=""SetPath(this);"">  <br>"
                            End If
                            lnkPhoto.Visible = True
                    End Select
                Next

            End If

            Dim lblReleaseDate As Label = CType(e.Item.FindControl("lblReleaseDate"), Label)
            Dim _releaseDate As Date = CDate(RowValue.ReleaseDate)
            lblReleaseDate.Text = _releaseDate
            If _releaseDate.Year < 2001 Then
                lblReleaseDate.Visible = False
            End If

        ElseIf e.Item.ItemType = ListItemType.Footer Then
            '-- Grid footer items

            '-- Summarize data
            Dim iTotBaru As Integer = 0       '-- Total WSC with Status = "Baru"
            Dim iTotProses As Integer = 0     '-- Total WSC with Status = "Proses"
            Dim iTotApprv As Integer = 0      '-- Total WSC with Status = "Selesai" & ClaimStatus = "APP"
            Dim iTotDisapp As Integer = 0     '-- Total WSC with Status = "Selesai" & ClaimStatus = "DAPP"
            Dim dAPPPartAmnt As Double = 0    '-- Approved total of part amount
            Dim dAPPLaborAmnt As Double = 0   '-- Approved total of labor cost
            Dim dDAPPPartAmnt As Double = 0   '-- Disapproved total of part amount
            Dim dDAPPLaborAmnt As Double = 0  '-- Disapproved total of labor cost

            '-- Read All status list
            Dim WSCAllStatusList As ArrayList = CType(sessHelp.GetSession("WSCAllStatusList"), ArrayList)

            If Not IsNothing(WSCAllStatusList) Then

                For Each item As WSCHeader In WSCAllStatusList
                    If item.Status = CType(enumStatusWSC.Status.Baru, String) Then
                        iTotBaru += 1
                    End If
                    If item.Status = CType(enumStatusWSC.Status.Proses, String) Then
                        iTotProses += 1
                    End If
                    If item.Status = CType(enumStatusWSC.Status.Selesai, String) And _
                       item.ClaimStatus = "APP" Then
                        iTotApprv += 1
                        dAPPPartAmnt += item.PartAmount
                        dAPPLaborAmnt += item.LaborAmount
                    End If
                    If item.Status = CType(enumStatusWSC.Status.Selesai, String) And _
                       item.ClaimStatus = "DAPP" Then
                        iTotDisapp += 1
                        dDAPPPartAmnt += item.PartAmount
                        dDAPPLaborAmnt += item.LaborAmount
                    End If
                Next

                '-- Total WSC with Status = "Baru"
                Dim lblBaru As Label = CType(e.Item.FindControl("lblBaru"), Label)
                lblBaru.Text = Format(iTotBaru, "#,##0")

                '-- Total WSC with Status = "Proses"
                Dim lblProses As Label = CType(e.Item.FindControl("lblProses"), Label)
                lblProses.Text = Format(iTotProses, "#,##0")

                '-- Total WSC with Status = "Selesai" & ClaimStatus = "APP"
                Dim lblApprove As Label = CType(e.Item.FindControl("lblApprove"), Label)
                lblApprove.Text = Format(iTotApprv, "#,##0")

                '-- Total WSC with Status = "Selesai" & ClaimStatus = "DAPP"
                Dim lblDisapprv As Label = CType(e.Item.FindControl("lblDisapprv"), Label)
                lblDisapprv.Text = Format(iTotDisapp, "#,##0")

                '-- Total Part amount approved
                Dim lblAPPPartAmnt As Label = CType(e.Item.FindControl("lblAPPPartAmnt"), Label)
                lblAPPPartAmnt.Text = Format(dAPPPartAmnt, "#,##0")

                '-- Total Labor cost approved
                Dim lblAPPLaborAmnt As Label = CType(e.Item.FindControl("lblAPPLaborAmnt"), Label)
                lblAPPLaborAmnt.Text = Format(dAPPLaborAmnt, "#,##0")

                '-- Total Part amount disapproved
                Dim lblDAPPPartAmnt As Label = CType(e.Item.FindControl("lblDAPPPartAmnt"), Label)
                lblDAPPPartAmnt.Text = Format(dDAPPPartAmnt, "#,##0")

                '-- Total Labor cost disapproved
                Dim lblDAPPLaborAmnt As Label = CType(e.Item.FindControl("lblDAPPLaborAmnt"), Label)
                lblDAPPLaborAmnt.Text = Format(dDAPPLaborAmnt, "#,##0")

            End If
        End If

    End Sub

    Private Sub dgStatusList_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgStatusList.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If

        BindDatagrid(dgStatusList.CurrentPageIndex + 1)
    End Sub

    Private Sub dgStatusList_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgStatusList.PageIndexChanged
        dgStatusList.CurrentPageIndex = e.NewPageIndex
        SettingCriteria()
        BindDatagrid(e.NewPageIndex + 1)
    End Sub

    Private Sub SettingCriteria()
        Dim criteria As Hashtable = New Hashtable(12)
        criteria.Add("DealerCode", txtKodeDealer.Text)
        criteria.Add("StartKirim", icStartKirim.Value)
        criteria.Add("EndKirim", icEndKirim.Value)
        criteria.Add("StartProses", icStartProses.Value)
        criteria.Add("EndProses", icEndProses.Value)
        criteria.Add("Status", ddlStatus.SelectedValue)
        criteria.Add("VehicleType", ddlVehicleType.SelectedValue)
        criteria.Add("ClaimNumber", txtClaimNo.Text)
        criteria.Add("PageIndex", dgStatusList.CurrentPageIndex)
        criteria.Add("ClaimType", ddlJenisWSC.SelectedValue)
        criteria.Add("ClaimStatus", ddlStatusWSC.SelectedValue)
        criteria.Add("StartRelease", icStartRelease.Value)
        criteria.Add("EndRelease", icEndRelease.Value)

        If CType(Session("LOGINUSERINFO"), UserInfo).Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            criteria.Add("CategoryCode", ddlCategory.SelectedValue)
        End If

        sessHelp.SetSession("CriteriaFormWSCStatusList", criteria)
    End Sub

#End Region

End Class
