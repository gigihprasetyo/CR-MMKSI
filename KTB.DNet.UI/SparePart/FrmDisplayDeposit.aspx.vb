#Region " Summary "
'--------------------------------------------------------'
'-- Program Code : FrmDisplayDeposit.aspx              --'
'-- Program Name : DEPOSIT-Daftar Deposit              --'
'-- Description  :                                     --'
'--------------------------------------------------------'
'-- Programmer   : Agus Pirnadi                        --'
'-- Start Date   : Nov 14 2005                         --'
'-- Update By    :                                     --'
'-- Last Update  : Jan 06 2006                         --'
'--------------------------------------------------------'
'-- Copyright © 2005 by Intimedia                      --'
'--------------------------------------------------------'
#End Region

#Region " Custom Namespace Imports "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Sparepart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
Imports OfficeOpenXml

#End Region

Public Class FrmDisplayDeposit
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon1 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents lblBegBalc As System.Web.UI.WebControls.Label
    Protected WithEvents lblDr As System.Web.UI.WebControls.Label
    Protected WithEvents lblCr As System.Web.UI.WebControls.Label
    Protected WithEvents lblEndBalc As System.Web.UI.WebControls.Label
    Protected WithEvents lblBeginBalance As System.Web.UI.WebControls.Label
    Protected WithEvents lblDebit As System.Web.UI.WebControls.Label
    Protected WithEvents lblCredit As System.Web.UI.WebControls.Label
    Protected WithEvents lblEndingBalance As System.Web.UI.WebControls.Label
    Protected WithEvents dgDepositList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlPeriod As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnDnLoad As System.Web.UI.WebControls.Button
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents btnPrint As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDepositAwal As System.Web.UI.WebControls.Label
    Protected WithEvents lblRO As System.Web.UI.WebControls.Label
    Protected WithEvents lblGiroService As System.Web.UI.WebControls.Label
    Protected WithEvents lblService As System.Web.UI.WebControls.Label
    Protected WithEvents lblOnProcess As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoDocument As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRef As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoFaktur As System.Web.UI.WebControls.TextBox

    Protected WithEvents txtDebitAmount As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCreditAmount As System.Web.UI.WebControls.TextBox

    Protected WithEvents RODepC As System.Web.UI.HtmlControls.HtmlTableRow


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
    Private _Dealer As Dealer    '-- Dealer object
    Private sortColl As SortCollection = New SortCollection
    Private _vsCritSearch As String = "FrmDisplayDeposit.SearchCriteria"

    'Private ArlClaim As ArrayList
#End Region

#Region " Custom Method "

    Private Sub BindDropdownList()

        '-- DropDownList Rencana Penebusan
        ''ddlPeriod.Items.Insert(0, New ListItem("Bulan Sekarang", ""))
        For Each item As ListItem In LookUp.ArraylistMonth(True, 11, 0, DateTime.Now)
            ddlPeriod.Items.Add(item)
        Next

    End Sub

    Private Sub DisplayDealer()
        '-- Display dealer info

        If Not IsNothing(sessHelp.GetSession("DEALER")) Then
            _Dealer = CType(sessHelp.GetSession("DEALER"), Dealer)
            lblDealerCode.Text = _Dealer.DealerCode
            Me.txtKodeDealer.Text = _Dealer.DealerCode
            lblDealerName.Text = _Dealer.DealerName & " - " & _Dealer.SearchTerm2
            sessHelp.SetSession("DepositDEALER", _Dealer)
        End If

    End Sub

    Private Sub WriteDepositData(ByRef sw As StreamWriter)

        Dim itemLine As StringBuilder = New StringBuilder  '-- Deposit line in text file

        '-- Retrieve Deposit from session
        Dim oDeposit As Deposit = CType(sessHelp.GetSession("Deposit"), Deposit)

        itemLine.Remove(0, itemLine.Length)  '-- Empty Deposit line
        itemLine.Append(oDeposit.Dealer.DealerCode & ";")  '-- Dealer code
        itemLine.Append(oDeposit.Period & ";")             '-- Period
        itemLine.Append(Format(oDeposit.BegBalance, "0.##") & ";")   '-- Beginning balance
        itemLine.Append(Format(oDeposit.TotalDebit, "0.##") & ";")   '-- Total debit
        itemLine.Append(Format(oDeposit.TotalCredit, "0.##") & ";")  '-- Total credit
        itemLine.Append(Format(oDeposit.EndBalance, "0.##") & ";")   '-- Ending balance

        sw.WriteLine(itemLine.ToString())  '-- Write Deposit line

        '-- Retrieve Deposit lines from session
        Dim DepositList As ArrayList = CType(sessHelp.GetSession("DepositList"), ArrayList)

        For Each depoLine As DepositLine In DepositList

            itemLine.Remove(0, itemLine.Length)  '-- Empty Deposit line
            itemLine.Append(IIf(Format(depoLine.PostingDate, "ddMMyyyy") <> "01011753", _
                                Format(depoLine.PostingDate, "ddMMyyyy") & ";", ";"))  '-- Posting date
            itemLine.Append(IIf(Format(depoLine.ClearingDate, "ddMMyyyy") <> "01011753", _
                                Format(depoLine.ClearingDate, "ddMMyyyy") & ";", ";"))  '-- Clearing date
            itemLine.Append(Format(depoLine.Debit, "0") & ";")   '-- Debit
            itemLine.Append(Format(depoLine.Credit, "0") & ";")  '-- Credit
            itemLine.Append(depoLine.DocumentNo & ";")   '-- Document No
            itemLine.Append(depoLine.ReferenceNo & ";")  '-- Reference no
            itemLine.Append(depoLine.InvoiceNo & ";")  '-- Invoice no
            itemLine.Append(depoLine.Remark & ";")     '-- Remark

            sw.WriteLine(itemLine.ToString())  '-- Write Deposit line
        Next

    End Sub

    Private Sub ResetFields()
        '-- Reset fields & grid

        lblBeginBalance.Text = ""   '-- Saldo awal
        lblDebit.Text = ""          '-- Total debet
        lblCredit.Text = ""         '-- Total credit
        lblEndingBalance.Text = ""  '-- Saldo akhir

    End Sub

    Private Sub BindDatagrid(ByVal pageIndex As Integer)

        Dim ObjDealer As Dealer = CType(Session("DEALER"), Dealer)

        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(DepositLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '-- Dealer
        If ObjDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(DepositLine), "Deposit.Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text))
        Else
            criterias.opAnd(New Criteria(GetType(DepositLine), "Deposit.Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        End If

        '-- Deposit period
        If ddlPeriod.SelectedIndex = 0 Then
            '-- Today (Month-to-date)
            criterias.opAnd(New Criteria(GetType(DepositLine), "Deposit.Period", MatchType.Exact, CType(DateTime.Now().Month, String).PadLeft(2, "0") & CType(DateTime.Now().Year, String)))
        Else
            Dim tgl As DateTime = System.Convert.ToDateTime(ddlPeriod.SelectedItem.ToString)
            criterias.opAnd(New Criteria(GetType(DepositLine), "Deposit.Period", MatchType.Exact, CType(tgl.Month, String).PadLeft(2, "0") & CType(tgl.Year, String)))
        End If

        '-- TOP SP Enhancement
        If txtNoDocument.Text.Trim.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(DepositLine), "DocumentNo", MatchType.Exact, txtNoDocument.Text))
        End If

        If txtRef.Text.Trim.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(DepositLine), "ReferenceNo", MatchType.Exact, txtRef.Text))
        End If

        If txtNoFaktur.Text.Trim.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(DepositLine), "InvoiceNo", MatchType.Exact, txtNoFaktur.Text))
        End If

        If txtDebitAmount.Text.Trim.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(DepositLine), "Debit", MatchType.Exact, txtDebitAmount.Text.Trim.Replace(".", "")))
        End If

        If txtCreditAmount.Text.Trim.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(DepositLine), "Credit", MatchType.Exact, txtCreditAmount.Text.Trim.Replace(".", "")))
        End If


        Dim totalRow As Integer = 0

        'Dim DepositList As ArrayList = New DepositLineFacade(User).RetrieveByCriteria(criterias, pageIndex, dgDepositList.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        Dim DepositList As ArrayList = New DepositLineFacade(User).RetrieveByCriteria(criterias, pageIndex, dgDepositList.PageSize, totalRow, CType(Session("sessDefaultSorting"), SortCollection))

        sessHelp.SetSession(_vsCritSearch, criterias)

        If DepositList.Count > 0 Then
            dgDepositList.DataSource = DepositList
            dgDepositList.VirtualItemCount = totalRow
            dgDepositList.DataBind()
        Else
            dgDepositList.DataSource = New ArrayList
            dgDepositList.VirtualItemCount = totalRow
            dgDepositList.DataBind()
        End If

        If totalRow > 0 Then
            btnDnLoad.Enabled = True
        End If


    End Sub

    Private Sub ActivateUserPrivilege()
        '-- Assign privileges

        btnSearch.Visible = SecurityProvider.Authorize(Context.User, SR.SearchDeposit_Privilege)
    End Sub

#End Region

#Region " EventHandler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim ObjDealer As Dealer = CType(Session("DEALER"), Dealer)

        If Not IsNothing(Request.Form("txtDealerName")) Then
            Me.lblDealerName.Text = Request.Form("txtDealerName").ToString
        End If

        If ObjDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then

            If Not IsPostBack Then
                If Not SecurityProvider.Authorize(Context.User, SR.ViewDeposit_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Deposit")
                End If

                DisplayDealer()     '-- Display dealer from login
                BindDropdownList()  '-- Init dropdownlist the first time this form uploads

                '-- Display grid column headers
                dgDepositList.DataSource = New ArrayList
                dgDepositList.DataBind()

            End If
            btnPrint.Attributes("onclick") = GeneralScript.GetPopUpEventReference( _
                        "../SparePart/frmPrintDeposit.aspx", "", 600, 800, "null")
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            'ActivateUserPrivilege()  '-- Assign privileges

        Else

            If Not IsPostBack Then

                If Not SecurityProvider.Authorize(Context.User, SR.ENHDealerDepositKTB_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Deposit")
                End If

                BindDropdownList()  '-- Init dropdownlist the first time this form uploads

                '-- Display grid column headers
                dgDepositList.DataSource = New ArrayList
                dgDepositList.DataBind()

            End If
            btnPrint.Attributes("onclick") = GeneralScript.GetPopUpEventReference( _
                        "../SparePart/frmPrintDeposit.aspx", "", 600, 800, "null")
            'lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelectionKTB();"
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection()"

        End If
        'txtKodeDealer.Attributes.Add("readonly", "readonly")

        RODepC.Visible = False

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        btnDnLoad.Enabled = True  '-- Init: Disable <Download> button
        ResetFields()  '-- Reset fields
        '-- Get Current Dealer
        _Dealer = New DealerFacade(User).GetDealer(txtKodeDealer.Text)
        sessHelp.SetSession("DepositDEALER", _Dealer)

        '-- Row status = active
        Dim ObjDealer As Dealer = CType(Session("DEALER"), Dealer)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(Deposit), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '-- Dealer
        If ObjDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(Deposit), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text))
        Else
            criterias.opAnd(New Criteria(GetType(Deposit), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        End If

        '-- Deposit period
        If ddlPeriod.SelectedIndex = 0 Then
            '-- Today (Month-to-date)
            criterias.opAnd(New Criteria(GetType(Deposit), "Period", MatchType.Exact, CType(DateTime.Now().Month, String).PadLeft(2, "0") & CType(DateTime.Now().Year, String)))
        Else
            Dim tgl As DateTime = System.Convert.ToDateTime(ddlPeriod.SelectedItem.ToString)
            criterias.opAnd(New Criteria(GetType(Deposit), "Period", MatchType.Exact, CType(tgl.Month, String).PadLeft(2, "0") & CType(tgl.Year, String)))
        End If

        '-- Retrieve a deposit record
        Dim oDeposit As Deposit = New DepositFacade(User).RetrieveByCriteria(criterias)

        '-- Store Deposit for later use
        If Not sessHelp.GetSession("Deposit") Is Nothing Then
            sessHelp.RemoveSession("Deposit")
        End If
        sessHelp.SetSession("Deposit", oDeposit)

        '-- Display header
        If Not IsNothing(oDeposit) Then

            lblBeginBalance.Text = Format(oDeposit.BegBalance, "#,##0")   '-- Saldo awal
            lblDebit.Text = Format(oDeposit.TotalDebit, "#,##0")          '-- Total debet
            lblCredit.Text = Format(oDeposit.TotalCredit, "#,##0")        '-- Total credit
            lblEndingBalance.Text = Format(oDeposit.EndBalance, "#,##0")  '-- Saldo akhir

            lblDepositAwal.Text = Format(oDeposit.AvailableDeposit, "#,##0")  '-- Deposit awal
            lblRO.Text = Format(oDeposit.RO, "#,##0")  '-- RO
            lblService.Text = Format(oDeposit.Service, "#,##0")  '-- Service
            lblGiroService.Text = Format(oDeposit.GiroReceive, "#,##0")  '-- Giro Service
            lblOnProcess.Text = Format(oDeposit.InClearing, "#,##0")  '-- Service

        End If

        '-- Row status = active
        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(DepositLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '-- Dealer
        criterias2.opAnd(New Criteria(GetType(DepositLine), "Deposit.Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text))

        '-- Deposit period
        If ddlPeriod.SelectedIndex = 0 Then
            '-- Today (Month-to-date)
            criterias2.opAnd(New Criteria(GetType(DepositLine), "Deposit.Period", MatchType.Exact, CType(DateTime.Now().Month, String).PadLeft(2, "0") & CType(DateTime.Now().Year, String)))
        Else
            '-- Monthly
            Dim tgl As DateTime = System.Convert.ToDateTime(ddlPeriod.SelectedItem.ToString)
            criterias2.opAnd(New Criteria(GetType(DepositLine), "Deposit.Period", MatchType.Exact, CType(tgl.Month, String).PadLeft(2, "0") & CType(tgl.Year, String)))
        End If

        '--dk 060113 reference to CR no:
        '--about default sort order of Deposit Lines 
        '--define default sort collection 
        sortColl.Clear()
        sortColl.Add(New Sort(GetType(DepositLine), "PostingDate", Sort.SortDirection.ASC))
        sortColl.Add(New Sort(GetType(DepositLine), "Credit", Sort.SortDirection.DESC))
        sessHelp.SetSession("sessDefaultSorting", sortColl)

        '-- store retrived list to session
        If Not sessHelp.GetSession("DepositList") Is Nothing Then
            sessHelp.RemoveSession("DepositList")
        End If
        sessHelp.SetSession("DepositList", New DepositLineFacade(User).RetrieveByCriteria(criterias2, sortColl))

        '--Render arraylist to deposit datagrid
        dgDepositList.CurrentPageIndex = 0
        BindDatagrid(dgDepositList.CurrentPageIndex + 1)  '-- Display page-1

    End Sub

    Private Sub dgDepositList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgDepositList.ItemCommand
        'If e.CommandName = "Download" Then
        '    Dim lblNoFaktur As String = e.Item.Cells(8).Text

        '    Dim critEDoc As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartEDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    critEDoc.opAnd(New Criteria(GetType(SparePartEDocument), "DocType", MatchType.Exact, 2))
        '    critEDoc.opAnd(New Criteria(GetType(SparePartEDocument), "DocNumber", MatchType.Exact, lblNoFaktur))
        '    Dim arlEDoc As ArrayList = New SparePartEDocumentFacade(User).Retrieve(critEDoc)
        '    If arlEDoc.Count > 0 Then
        '        Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & CType(arlEDoc(0), SparePartEDocument).Path
        '        Response.Redirect("../Download.aspx?file=" & PathFile)
        '    Else
        '        MessageBox.Show("Data tidak ditemukan")
        '        Exit Sub
        '    End If
        'End If
    End Sub

    Private downloadPrivilege As Boolean

    Private Sub dgDepositList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDepositList.ItemDataBound
        '-- Handle data binding of template columns
        
        If Not e.Item.DataItem Is Nothing Then
            Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim ObjDepositLine As DepositLine = CType(e.Item.DataItem, DepositLine)

            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = (dgDepositList.CurrentPageIndex * dgDepositList.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No

                'Dim lbtnDownload As LinkButton = CType(e.Item.FindControl("lbtnDownload"), LinkButton)
                'lbtnDownload.Text = "<img src=""../images/download.gif"" border=""0"" alt=" & e.Item.Cells(6).Text & ">"
                'lbtnDownload.Attributes.Add("OnClick", "return confirm('Anda Yakin Mau Download?');")
            End If

            e.Item.Cells(2).Text = ObjDepositLine.Deposit.Dealer.DealerCode
        End If
    End Sub

    Private Sub btnDnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDnLoad.Click
        SetDownload()
    End Sub
    Private Sub btnDnLoad_Click_OLD(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '-- Download data in datagrid to text file

        '-- Generate timestamp
        Dim dt As DateTime = DateTime.Now
        Dim sSuffix As String = CType(dt.Year, String) & CType(dt.Month, String) & _
                                CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & _
                                CType(dt.Second, String) & CType(dt.Millisecond, String)

        '-- Temp file must be a randomly named text file!
        Dim DepositData As String = Server.MapPath("") & "\..\DataTemp\DepositData" & sSuffix & ".txt"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(DepositData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(DepositData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteDepositData(sw)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\DepositData" & sSuffix & ".txt")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try

    End Sub

    Private Sub SortListControl(ByRef pCompletelist As ArrayList, ByVal SortColumn As String, _
                                ByVal SortDirection As Integer)

        Dim IsAsc As Boolean = (SortDirection = Sort.SortDirection.ASC)

        Dim objListComparer As IComparer = New ListComparer(IsAsc, SortColumn)
        pCompletelist.Sort(objListComparer)

    End Sub

    Private Sub dgDepositList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDepositList.SortCommand
        '-- Sort datagrid
        sortColl.Clear()
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    sortColl.Add(New Sort(GetType(DepositLine), CType(ViewState("CurrentSortColumn"), String), Sort.SortDirection.DESC))
                    'ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    'ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
                    sortColl.Add(New Sort(GetType(DepositLine), CType(ViewState("CurrentSortColumn"), String), Sort.SortDirection.ASC))
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            sortColl.Add(New Sort(GetType(DepositLine), CType(ViewState("CurrentSortColumn"), String), Sort.SortDirection.ASC))
            'ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If
        '-- store sorting list to session
        sessHelp.SetSession("sessDefaultSorting", sortColl)

        '-- Retrieve Deposit lines from session
        Dim DepositList As ArrayList = CType(sessHelp.GetSession("DepositList"), ArrayList)

        If Not IsNothing(DepositList) AndAlso DepositList.Count <> 0 Then
            dgDepositList.CurrentPageIndex = 0
            BindDatagrid(dgDepositList.CurrentPageIndex + 1)  '-- Display page-1
        End If

    End Sub

    Private Sub dgDepositList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDepositList.PageIndexChanged
        '-- Select page

        dgDepositList.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(e.NewPageIndex + 1)
    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        Dim crits As CriteriaComposite
        If dgDepositList.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        If Not IsNothing(sessHelp.GetSession(_vsCritSearch)) Then
            crits = CType(sessHelp.GetSession(_vsCritSearch), CriteriaComposite)
        End If
        arrData = New DepositLineFacade(User).Retrieve(crits)
        If arrData.Count > 0 Then
            CreateExcel("TOPSparepart-DisplayDeposit", arrData)
        End If

    End Sub



    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim oD As Dealer
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = FileName
            ws.Cells("A3").Value = "No"
            ws.Cells("B3").Value = "Kode Dealer"
            ws.Cells("C3").Value = "Tgl Transaksi"
            ws.Cells("D3").Value = "Debet (RP)"
            ws.Cells("E3").Value = "Kredit (RP)"
            ws.Cells("F3").Value = "No Dokumen"
            ws.Cells("G3").Value = "Referensi"
            ws.Cells("H3").Value = "No Faktur"
            ws.Cells("I3").Value = "Keterangan"

            For i As Integer = 0 To Data.Count - 1
                Dim item As DepositLine = Data(i)
                ws.Cells(i + 4, 1).Value = i + 1
                If IsNothing(item.Deposit.Dealer) Then
                    ws.Cells(i + 4, 2).Value = ""
                Else
                    ws.Cells(i + 4, 2).Value = item.Deposit.Dealer.DealerCode
                End If
                ws.Cells(i + 4, 3).Value = item.PostingDate
                ws.Column(3).Style.Numberformat.Format = "DD/MM/YY"
                ws.Cells(i + 4, 4).Value = item.Debit.ToString("N0")
                ws.Cells(i + 4, 5).Value = item.Credit.ToString("N0")
                ws.Cells(i + 4, 6).Value = item.DocumentNo
                ws.Cells(i + 4, 7).Value = item.ReferenceNo
                ws.Cells(i + 4, 8).Value = item.InvoiceNo
                ws.Cells(i + 4, 9).Value = item.Remark
            Next

            CreateExcelFile(pck, FileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xls")
        End Using

    End Sub


    Private Function CreateSheet(pck As ExcelPackage, sheetName As String) As ExcelWorksheet
        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add(sheetName)
        ws.View.ShowGridLines = False
        Return ws
    End Function

    Private Sub CreateExcelFile(pck As ExcelPackage, fileName As String)
        Dim fileBytes = pck.GetAsByteArray()
        Response.Clear()

        'Response.AppendHeader("Content-Length", fileBytes.Length.ToString())
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName)
        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"  'xlsx
        Response.ContentType = "application/vnd.ms-excel" 'xls
        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()

    End Sub
#End Region

End Class
