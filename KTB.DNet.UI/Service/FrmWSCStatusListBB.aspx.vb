#Region " Summary "
'--------------------------------------------'
'-- Program Code : FrmWSCStatusListBB.aspx   --'
'-- Program Name : Daftar Status WSC       --'
'-- Description  :                         --'
'--------------------------------------------'
'-- Programmer   : Agus Pirnadi            --'
'-- Start Date   : Oct 25 2005             --'
'-- Update By    :                         --'
'-- Last Update  : Jan 24 2005             --'
'--------------------------------------------'
'-- Copyright © 2005 by Intimedia          --'
'--------------------------------------------'
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
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

Public Class FrmWSCStatusListBB
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
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
    Protected WithEvents ddlEvidenceType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dgStatusList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents chkKirim As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkProses As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPath As System.Web.UI.WebControls.TextBox
    Protected WithEvents divPath As System.Web.UI.HtmlControls.HtmlGenericControl

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
        ddlVehicleType.Items.Add(listItemBlank)
        For Each item As VechileType In VehicleTypes
            Dim listItem As New ListItem(item.VechileTypeCode, item.ID)
            ddlVehicleType.Items.Add(listItem)
        Next

        Dim listItemBlank2 As ListItem  '-- dummy item denoting ALL

        '-- Claim status dropdownlist
        'listItemBlank2 = New ListItem("Pilih", "")
        'ddlStatus.Items.Add(listItemBlank2)
        'For Each item As ListItem In LookUp.ArrayClaimStatus
        '    ddlStatus.Items.Add(item)
        'Next

        ddlStatus.DataSource = LookUp.RetriveWSCStatus
        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataBind()
        ddlStatus.Items.Insert(0, New ListItem("Silakan Pilih", ""))
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
                Me.txtClaimNo.Text = CType(criteria("ClaimNumber"), String)
                'RetrieveAllData() '-- Retrieve all data without paging
                Dim _pageIndex As Integer = CType(criteria("PageIndex"), Integer)
                dgStatusList.CurrentPageIndex = _pageIndex
                BindDatagrid(_pageIndex)
            End If
        End If

        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
    End Sub

    Private Sub BindDatagrid(ByVal currentPageIndex As Integer)

        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(WSCHeaderBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '-- Dealer code
        If txtKodeDealer.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(WSCHeaderBB), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        End If

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCHeaderBB), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If chkKirim.Checked Then
            '-- Periode kirim
            Dim StartKirim As New DateTime(CInt(icStartKirim.Value.Year), CInt(icStartKirim.Value.Month), CInt(icStartKirim.Value.Day), 0, 0, 0)
            Dim EndKirim As New DateTime(CInt(icEndKirim.Value.Year), CInt(icEndKirim.Value.Month), CInt(icEndKirim.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(WSCHeaderBB), "CreatedTime", MatchType.GreaterOrEqual, Format(StartKirim, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(WSCHeaderBB), "CreatedTime", MatchType.LesserOrEqual, Format(EndKirim, "yyyy-MM-dd HH:mm:ss")))
        End If

        If chkProses.Checked Then
            '-- Periode proses
            Dim StartProses As New DateTime(CInt(icStartProses.Value.Year), CInt(icStartProses.Value.Month), CInt(icStartProses.Value.Day), 0, 0, 0)
            Dim EndProses As New DateTime(CInt(icEndProses.Value.Year), CInt(icEndProses.Value.Month), CInt(icEndProses.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(WSCHeaderBB), "DecideDate", MatchType.GreaterOrEqual, Format(StartProses, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(WSCHeaderBB), "DecideDate", MatchType.LesserOrEqual, Format(EndProses, "yyyy-MM-dd HH:mm:ss")))
        End If

        If chkRelease.Checked Then
            '-- Periode Release
            Dim StartRelease As New DateTime(CInt(icStartRelease.Value.Year), CInt(icStartRelease.Value.Month), CInt(icStartRelease.Value.Day), 0, 0, 0)
            Dim EndRelease As New DateTime(CInt(icEndRelease.Value.Year), CInt(icEndRelease.Value.Month), CInt(icEndRelease.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(WSCHeaderBB), "ReleaseDate", MatchType.GreaterOrEqual, Format(StartRelease, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(WSCHeaderBB), "ReleaseDate", MatchType.LesserOrEqual, Format(EndRelease, "yyyy-MM-dd HH:mm:ss")))
        End If

        '-- Nomor klaim (LIKE '%%')
        If txtClaimNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(WSCHeaderBB), "ClaimNumber", MatchType.[Partial], txtClaimNo.Text.Trim()))
        End If

        '-- Tipe Kendaraan
        If ddlVehicleType.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(WSCHeaderBB), "ChassisMasterBB.VechileColor.VechileType.ID", MatchType.Exact, ddlVehicleType.SelectedValue))
        End If

        '-- Status klaim
        If ddlStatus.SelectedValue <> "" Then
            criterias.opAnd(New Criteria(GetType(WSCHeaderBB), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        '-- Evidence Type
        If ddlEvidenceType.SelectedValue <> "Pilih" Then
            criterias.opAnd(New Criteria(GetType(WSCHeaderBB), "ID", MatchType.InSet, "(select WSCHeaderBBID from WSCEvidenceBB where RowStatus = 0 and EvidenceType=" & ddlEvidenceType.SelectedValue & ")"))
        End If

        Dim totalRow As Integer = 0
        '-- Retrieve recordset
        WSCStatusList = New WSCHeaderBBFacade(User).RetrieveActiveList(criterias, currentPageIndex, dgStatusList.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

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

    Private Sub RetrieveAllData()

        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(WSCHeaderBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '-- Dealer code
        If txtKodeDealer.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(WSCHeaderBB), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        End If

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCHeaderBB), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If chkKirim.Checked Then
            '-- Periode kirim
            Dim StartKirim As New DateTime(CInt(icStartKirim.Value.Year), CInt(icStartKirim.Value.Month), CInt(icStartKirim.Value.Day), 0, 0, 0)
            Dim EndKirim As New DateTime(CInt(icEndKirim.Value.Year), CInt(icEndKirim.Value.Month), CInt(icEndKirim.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(WSCHeaderBB), "CreatedTime", MatchType.GreaterOrEqual, Format(StartKirim, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(WSCHeaderBB), "CreatedTime", MatchType.LesserOrEqual, Format(EndKirim, "yyyy-MM-dd HH:mm:ss")))
        End If

        If chkProses.Checked Then
            '-- Periode proses
            Dim StartProses As New DateTime(CInt(icStartProses.Value.Year), CInt(icStartProses.Value.Month), CInt(icStartProses.Value.Day), 0, 0, 0)
            Dim EndProses As New DateTime(CInt(icEndProses.Value.Year), CInt(icEndProses.Value.Month), CInt(icEndProses.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(WSCHeaderBB), "DecideDate", MatchType.GreaterOrEqual, Format(StartProses, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(WSCHeaderBB), "DecideDate", MatchType.LesserOrEqual, Format(EndProses, "yyyy-MM-dd HH:mm:ss")))
        End If

        '-- Nomor klaim (LIKE '%%')
        If txtClaimNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(WSCHeaderBB), "ClaimNumber", MatchType.[Partial], txtClaimNo.Text.Trim()))
        End If

        '-- Tipe Kendaraan
        If ddlVehicleType.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(WSCHeaderBB), "ChassisMasterBB.VechileColor.VechileType.ID", MatchType.Exact, ddlVehicleType.SelectedValue))
        End If

        '-- Status klaim
        If ddlStatus.SelectedValue <> "" Then
            criterias.opAnd(New Criteria(GetType(WSCHeaderBB), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        '-- Evidence Type
        If ddlEvidenceType.SelectedValue <> "Pilih" Then
            criterias.opAnd(New Criteria(GetType(WSCHeaderBB), "ID", MatchType.InSet, "(select WSCHeaderBBID from WSCEvidenceBB where RowStatus = 0 and EvidenceType=" & ddlEvidenceType.SelectedValue & ")"))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(WSCHeaderBB), "Dealer.DealerCode", Sort.SortDirection.ASC))  '-- Kode dealer
        sortColl.Add(New Sort(GetType(WSCHeaderBB), "ClaimNumber", Sort.SortDirection.ASC))        '-- Nomor klaim

        '-- Retrieve recordset
        Dim WSCAllStatusList As ArrayList = New WSCHeaderBBFacade(User).RetrieveByCriteria(criterias, sortColl)

        '-- Save into session
        sessHelp.SetSession("WSCAllStatusList", WSCAllStatusList)

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        '-- Date validation
        If Not chkKirim.Checked And Not chkProses.Checked Then
            '-- At least one date range is set: Periode Kirim or Periode Proses
            MessageBox.Show("Periode Kirim atau Periode Proses harus diisi")
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
            Dim WSCHead As WSCHeaderBB = New WSCHeaderBBFacade(User).Retrieve(CType(e.Item.Cells(0).Text, Integer))

            '-- Check to see if the record already got deleted
            If IsNothing(WSCHead) Then
                MessageBox.Show("Record ini sudah dihapus.")
                Exit Sub
            End If

            '-- Store WSC header and its details for display on form FrmWSCDetailBB
            sessHelp.SetSession("WSCHead", WSCHead)
            sessHelp.SetSession("PageNumber", dgStatusList.CurrentPageIndex)


            '-- Display WSC header and its details on WSC Detail Info page
            Response.Redirect("FrmWSCDetailBB.aspx")

        ElseIf e.CommandName = "lnkEmail" Then

            '-- Retrieve WSC header and its details of the chosen header
            Dim WSCHead As WSCHeaderBB = New WSCHeaderBBFacade(User).Retrieve(CType(e.Item.Cells(0).Text, Integer))

            '-- Store WSC header and its details for display on form FrmWSCDetailBB
            sessHelp.SetSession("WSCHead", WSCHead)

            '-- Display WSC send email
            sessHelp.SetSession("PrevPage", Request.Url.ToString())
            Response.Redirect("FrmWSCSendEmailBB.aspx?type=ReadOnly")
        ElseIf e.CommandName = "lnkKwitansi" Then
            Dim linkButton As LinkButton = e.Item.FindControl("lnkKwitansi")
            'Dim fileInfox As New FileInfo(strCommand)
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
            'Dim fileInfox As New FileInfo(strCommand)
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
            'Dim fileInfox As New FileInfo(strCommand)
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
            Dim WSCHead As WSCHeaderBB = New WSCHeaderBBFacade(User).Retrieve(CType(e.CommandArgument, Integer))
            '-- Check to see if the record already got deleted
            If IsNothing(WSCHead) Then
                MessageBox.Show("Record ini sudah dihapus.")
                Exit Sub
            End If

            '-- Store WSC header and its details for display on form FrmWSCDetail
            sessHelp.SetSession("WSCHead", WSCHead)
            sessHelp.SetSession("PageNumber", dgStatusList.CurrentPageIndex)


            '-- Display WSC header and its details on WSC Detail Info page
            Server.Transfer("../Service/FrmWSCHeaderBB.aspx?screenFrom=WSC&viewStateMode=1&WSCId=" & WSCHead.ID)

        ElseIf e.CommandName = "View" Then
            Dim WSCHead As WSCHeaderBB = New WSCHeaderBBFacade(User).Retrieve(CType(e.CommandArgument, Integer))
            '-- Check to see if the record already got deleted
            If IsNothing(WSCHead) Then
                MessageBox.Show("Record ini sudah dihapus.")
                Exit Sub
            End If

            Server.Transfer("../Service/FrmWSCHeaderBB.aspx?screenFrom=WSC&viewStateMode=2&WSCId=" & WSCHead.ID)

        ElseIf e.CommandName = "Hapus" Then
            Try
                delete(e.Item.ItemIndex)
                MessageBox.Show(SR.DeleteSucces())
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail())
            End Try
        End If

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
        Dim objWSCHeaderBBFacade As WSCHeaderBBFacade = New WSCHeaderBBFacade(User)
        Dim objwschapus As WSCHeaderBB = objWSCHeaderBBFacade.Retrieve(CInt(dgStatusList.Items(nId).Cells(0).Text))
        objWSCHeaderBBFacade.UpdateRowStatus(objwschapus)

        RetrieveAllData() '-- Retrieve all data without paging
        dgStatusList.CurrentPageIndex = 0
        BindDatagrid(0)
        sessHelp.SetSession("PageNumber", dgStatusList.CurrentPageIndex)
    End Sub

    Private Sub dgStatusList_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgStatusList.ItemDataBound
        '-- Handle data binding

        Dim RowValue As WSCHeaderBB = CType(e.Item.DataItem, WSCHeaderBB)

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '-- Grid detail items

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgStatusList.PageSize * dgStatusList.CurrentPageIndex)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            '20101102 - remark by anh 'memungkinkan untuk lebih dari 1 reason
            'Dim lblReason As Label = CType(e.Item.FindControl("lblReason"), Label)
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

                If Not RowValue.Reason Is Nothing Then
                    Dim lblReason As Label = New Label
                    lblReason.Text = RowValue.Reason.ReasonCode + "<br>"
                    lblReason.ToolTip = RowValue.Reason.Description
                    e.Item.Cells(3).Controls.Add(lblReason)
                End If
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

            Catch ex As Exception
            End Try


            '--Check Email Icon
            Dim lblEmail As LinkButton = CType(e.Item.FindControl("lnkEmail"), LinkButton)
            Dim linkbtnHapus As LinkButton = e.Item.FindControl("lbnHapus")
            linkbtnHapus.Text = "<img src=""../images/trash.gif"" border=""0"" alt=""Hapus"">"
            
            Dim lnkbtnEdit As LinkButton = e.Item.FindControl("lnkbtnEdit")
            Dim lnkbtnView As LinkButton = e.Item.FindControl("lnkbtnView")

            If RowValue.StatusText = "Proses" And isAuthDelete Then
                linkbtnHapus.Visible = True
            Else
                linkbtnHapus.Visible = False
            End If

            If RowValue.StatusText = "Baru" Then
                lnkbtnEdit.Visible = True
                lnkbtnView.Visible = False
            Else
                lnkbtnView.Visible = True
                lnkbtnEdit.Visible = False
            End If
            'Dim _id As Integer
            'Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
            'If Not objDealer Is Nothing Then
            '    _id = objDealer.ID
            'End If

            If RowValue.WSCDamageRequestPartBBs.Count = 0 Then
                lblEmail.Visible = False
            End If

            '<img id='imgKwitansi' style="width:15px; height:15px;" alt="" onmouseout="Out();" onmouseover="Large(this)" />
            '--Image kwitansi, surat, teknikal
            Dim lbKwitansi As LinkButton = CType(e.Item.FindControl("lnkKwitansi"), LinkButton)
            Dim lbSurat As LinkButton = CType(e.Item.FindControl("lnkSurat"), LinkButton)
            Dim lbTeknikal As LinkButton = CType(e.Item.FindControl("lnkTeknikal"), LinkButton)
            lbKwitansi.Visible = False
            lbSurat.Visible = False
            lbTeknikal.Visible = False

            Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("WSCEvidenceBBFileDirectory")
            Dim fileInfo1 As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))
            Dim DestFullFilePath As String = fileInfo1.Directory.FullName '--& "\" & DestFile '-- Destination file

            Dim criterias As New CriteriaComposite(New Criteria(GetType(WSCEvidenceBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(WSCEvidenceBB), "WSCHeaderBB.ID", MatchType.Exact, RowValue.ID))
            Dim WSCEvidenceBBList As ArrayList = New WSCEvidenceBBFacade(User).Retrieve(criterias)
            If WSCEvidenceBBList.Count > 0 Then
                Dim _evidence As New WSCEvidenceBB
                lbKwitansi.Text = String.Empty
                lbSurat.Text = String.Empty
                lbTeknikal.Text = String.Empty

                For Each _evidence In WSCEvidenceBBList
                    Dim dataFile As String = DestFullFilePath & "\" & _evidence.PathFile
                    'dataFile = dataFile.Replace("\", "/")
                    Select Case _evidence.EvidenceType
                        Case EnumWSCEvidenceType.WSCEvidenceType.KWITANSI_WSC
                            If dataFile.Substring(dataFile.Length - 4, 4).ToLower = ".jpg" Then
                                lbKwitansi.Text = lbKwitansi.Text & "<img src=""../images/download.gif"" lowsrc='" & dataFile & "' border=""0"" onmouseout =""HideEvidenceImage();""  onmouseover=""ShowEvidenceImage(this);"" > <br>"
                            Else
                                lbKwitansi.Text = lbKwitansi.Text & "<img src=""../images/download.gif"" border=""0"" lowsrc='" & dataFile & "' onmouseover=""SetPath(this);""> <br>"
                            End If
                            lbKwitansi.Visible = True
                        Case EnumWSCEvidenceType.WSCEvidenceType.SURAT_WSC
                            If dataFile.Substring(dataFile.Length - 4, 4).ToLower = ".jpg" Then
                                lbSurat.Text = lbSurat.Text & "<img src=""../images/download.gif"" lowsrc='" & dataFile & "' border=""0""   onmouseout=""HideEvidenceImage();"" onmouseover=""ShowEvidenceImage(this);"" > <br>"
                            Else
                                lbSurat.Text = lbSurat.Text & "<img src=""../images/download.gif"" border=""0"" lowsrc='" & dataFile & "' onmouseover=""SetPath(this);""> <br>"
                            End If
                            lbSurat.Visible = True
                        Case EnumWSCEvidenceType.WSCEvidenceType.TEKNIKAL_WSC
                            If dataFile.Substring(dataFile.Length - 4, 4).ToLower = ".jpg" Then
                                lbTeknikal.Text = lbTeknikal.Text & "<img src=""../images/download.gif"" lowsrc='" & dataFile & "' border=""0""   onmouseout=""HideEvidenceImage();"" onmouseover=""ShowEvidenceImage(this);"" >  <br>"
                            Else
                                lbTeknikal.Text = lbTeknikal.Text & "<img src=""../images/download.gif"" border=""0"" lowsrc='" & dataFile & "' onmouseover=""SetPath(this);"">  <br>"
                            End If
                            lbTeknikal.Visible = True
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

                For Each item As WSCHeaderBB In WSCAllStatusList
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
        Dim criteria As Hashtable = New Hashtable(9)
        criteria.Add("DealerCode", txtKodeDealer.Text)
        criteria.Add("StartKirim", icStartKirim.Value)
        criteria.Add("EndKirim", icEndKirim.Value)
        criteria.Add("StartProses", icStartProses.Value)
        criteria.Add("EndProses", icEndProses.Value)
        criteria.Add("Status", ddlStatus.SelectedValue)
        criteria.Add("VehicleType", ddlVehicleType.SelectedValue)
        criteria.Add("ClaimNumber", txtClaimNo.Text)
        criteria.Add("PageIndex", dgStatusList.CurrentPageIndex)
        sessHelp.SetSession("CriteriaFormWSCStatusList", criteria)
    End Sub

#End Region

End Class
