#Region " Summary "
'--------------------------------------------------------'
'-- Program Code : FrmDisplayDepositC2.aspx            --'
'-- Program Name : DEPOSIT-Daftar Deposit C2           --'
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
Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.BusinessFacade
Imports KTB.DNET.BusinessFacade.Service
Imports KTB.DNET.BusinessFacade.Sparepart
Imports KTB.DNET.BusinessFacade.General
Imports KTB.DNET.BusinessFacade.Helper
Imports KTB.DNET.Security
Imports KTB.DNET.Utility
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
Imports OfficeOpenXml

#End Region

Public Class FrmDisplayDepositC2
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnDnLoad As System.Web.UI.WebControls.Button
    Protected WithEvents dgDepositC2List As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblTotDepC2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotDepositC2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDocDate As System.Web.UI.WebControls.Label
    Protected WithEvents icDocDateEnd As KTB.DNET.WebCC.IntiCalendar
    Protected WithEvents lblNotes As System.Web.UI.WebControls.Label
    Protected WithEvents icDocDateStart As KTB.DNET.WebCC.IntiCalendar
    Protected WithEvents btnPrint As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoDoc As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBillingNumber As System.Web.UI.WebControls.TextBox

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
    Private _DepositC2Lines As ArrayList = New ArrayList
    Private _vsCritSearch As String = "FrmDisplayDepositC2.CriteriaSearch"
#End Region

#Region " Custom Method "

    Private Sub DisplayDealer()
        '-- Display dealer info

        If Not IsNothing(sessHelp.GetSession("DEALER")) Then
            _Dealer = CType(sessHelp.GetSession("DEALER"), Dealer)
            lblDealerCode.Text = _Dealer.DealerCode
            txtKodeDealer.Text = _Dealer.DealerCode
            lblDealerName.Text = _Dealer.DealerName & " - " & _Dealer.SearchTerm2
            sessHelp.SetSession("Deposit2DEALER", _Dealer)
        End If
        sessHelp.SetSession("DepositC2Lines", _DepositC2Lines)
        'txtKodeDealer.Attributes.Add("readonly", "readonly")
    End Sub

    Private Sub InitDate()
        '-- Init date

        icDocDateStart.Value = New Date(Now.Year, 1, 1)  '-- Set start date with 01/01/Current-year
        icDocDateEnd.Value = New Date(Now.Year, Now.Month, Now.Day)  '-- Today's date
    End Sub

    Private Sub WriteDepositC2Data(ByRef sw As StreamWriter)

        Dim itemLine As StringBuilder = New StringBuilder  '-- DepositC2 line in text file

        _DepositC2Lines = CType(sessHelp.GetSession("DepositC2Lines"), ArrayList)

        itemLine.Remove(0, itemLine.Length)  '-- Empty DepositC2 line
        itemLine.Append(txtKodeDealer.Text & ";")   '-- Dealer code

        sw.WriteLine(itemLine.ToString())  '-- Write Deposit line

        For Each depoC2Line As DepositC2Line In _DepositC2Lines

            itemLine.Remove(0, itemLine.Length)  '-- Empty Deposit line
            itemLine.Append(IIf(Format(depoC2Line.DocumentDate, "ddMMyyyy") <> "01011753", _
                                Format(depoC2Line.DocumentDate, "ddMMyyyy") & ";", ";"))  '-- Document date
            itemLine.Append(depoC2Line.DocumentNo & ";")                        '-- Document No
            itemLine.Append(Format(depoC2Line.DepositC2Amnt, "0") & ";")  '-- DepositC2

            sw.WriteLine(itemLine.ToString())  '-- Write Deposit line
        Next

    End Sub

    Private Sub ActivateUserPrivilege()
        '-- Assign privileges

        btnSearch.Visible = SecurityProvider.Authorize(Context.User, SR.SearchDepositC2_Privilege)
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
                If Not SecurityProvider.Authorize(Context.User, SR.ViewDepositC2_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=Deposit-Daftar Deposit C2")
                End If

                DisplayDealer()  '-- Display dealer from login
                InitDate()       '-- Init date

                '-- Display grid column headers
                dgDepositC2List.DataSource = New ArrayList
                dgDepositC2List.DataBind()
                If Not IsNothing(sessHelp.GetSession("DepositC2Lines")) Then
                    '-- Retrieve Deposit list from session
                    _DepositC2Lines = CType(sessHelp.GetSession("DepositC2Lines"), ArrayList)
                    '-- Bind and display
                    '-- initial range date filter
                    dgDepositC2List.DataSource = _DepositC2Lines
                    dgDepositC2List.DataBind()
                Else
                    sessHelp.SetSession("sessRangeDate", String.Format("{0:dd/MM/yyyy}", DateTime.Now) & " S.D " & String.Format("{0:dd/MM/yyyy}", DateTime.Now))
                End If
            End If

            '--exclude  this privilege from Asra (BA)
            'ActivateUserPrivilege()  '-- Assign privileges
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        Else
            If Not IsPostBack Then
                If Not SecurityProvider.Authorize(Context.User, SR.ENHDealerDepositKTB_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=Deposit-Daftar Deposit C2")
                End If

                InitDate()       '-- Init date

                '-- Display grid column headers
                dgDepositC2List.DataSource = New ArrayList
                dgDepositC2List.DataBind()
                If Not IsNothing(sessHelp.GetSession("DepositC2Lines")) Then
                    '-- Retrieve Deposit list from session
                    _DepositC2Lines = CType(sessHelp.GetSession("DepositC2Lines"), ArrayList)
                    '-- Bind and display
                    '-- initial range date filter
                    dgDepositC2List.DataSource = _DepositC2Lines
                    dgDepositC2List.DataBind()
                Else
                    sessHelp.SetSession("sessRangeDate", String.Format("{0:dd/MM/yyyy}", DateTime.Now) & " S.D " & String.Format("{0:dd/MM/yyyy}", DateTime.Now))
                End If
            End If

            '--exclude  this privilege from Asra (BA)
            'ActivateUserPrivilege()  '-- Assign privileges
            'lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelectionKTB();"
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection()"
        End If

        btnPrint.Attributes("onclick") = GeneralScript.GetPopUpEventReference( _
            "../SparePart/FrmPrintDepositC2.aspx", "", 600, 800, "null")
        'txtKodeDealer.Attributes.Add("readonly", "readonly")

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim ObjDealer As Dealer = CType(Session("DEALER"), Dealer)
        '-- Get Current Dealer
        _Dealer = New DealerFacade(User).GetDealer(txtKodeDealer.Text)
        sessHelp.SetSession("Deposit2DEALER", _Dealer)
        '-- Display grid column headers
        dgDepositC2List.DataSource = New ArrayList
        dgDepositC2List.DataBind()

        btnDnLoad.Enabled = False  '-- Init: Disable <Download> button

        If CType(icDocDateStart.Value, Date) > CType(icDocDateEnd.Value, Date) Then
            '-- It must be Start date <= End date
            MessageBox.Show("Interval tanggal tidak valid")
            Exit Sub  '-- Directly exits
        End If

        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(DepositC2Line), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtNoDoc.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(DepositC2Line), "DocumentNo", MatchType.Exact, txtNoDoc.Text))
        End If

        If txtBillingNumber.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(DepositC2Line), "BillingNumber", MatchType.Exact, txtBillingNumber.Text))
        End If

        '-- Dealer
        If ObjDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(DepositC2Line), "DepositC2.Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text))
        Else
            criterias.opAnd(New Criteria(GetType(DepositC2Line), "DepositC2.Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        End If

        '-- Tgl dokumen
        Dim StartDate As New DateTime(CInt(icDocDateStart.Value.Year), CInt(icDocDateStart.Value.Month), CInt(icDocDateStart.Value.Day), 0, 0, 0)
        Dim EndDate As New DateTime(CInt(icDocDateEnd.Value.Year), CInt(icDocDateEnd.Value.Month), CInt(icDocDateEnd.Value.Day), 23, 59, 59)
        criterias.opAnd(New Criteria(GetType(DepositC2Line), "DocumentDate", MatchType.GreaterOrEqual, Format(StartDate, "yyyy-MM-dd HH:mm:ss")))
        criterias.opAnd(New Criteria(GetType(DepositC2Line), "DocumentDate", MatchType.LesserOrEqual, Format(EndDate, "yyyy-MM-dd HH:mm:ss")))

        '-- save date range to session
        sessHelp.SetSession("sessRangeDate", String.Format("{0:dd/MM/yyyy}", StartDate) & " S.D " & String.Format("{0:dd/MM/yyyy}", EndDate))
        '-- Retrieve a deposit C2 record
        _DepositC2Lines = New DepositC2LineFacade(User).Retrieve(criterias)

        sessHelp.SetSession(_vsCritSearch, criterias)

        '-- Calculate DepositC2 total amount
        Dim DepoC2Amnt As Double = 0
        For Each DepoC2Line As DepositC2Line In _DepositC2Lines
            DepoC2Amnt += DepoC2Line.DepositC2Amnt  '-- Sum up deposit C2 amount
        Next
        lblTotDepositC2.Text = Format(DepoC2Amnt, "#,##0")  '-- Display total of deposit C2 amount

        '-- Store DepositC2 list for later refresh
        sessHelp.SetSession("DepositC2Lines", _DepositC2Lines)

        '-- Bind and display
        dgDepositC2List.DataSource = _DepositC2Lines
        dgDepositC2List.DataBind()

        If _DepositC2Lines.Count <> 0 Then
            btnDnLoad.Enabled = True  '-- Enable <Download> button
        Else
            MessageBox.Show(SR.DataNotFound("Data"))
        End If

    End Sub

    Private Sub dgDepositC2List_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgDepositC2List.ItemCommand
        If e.CommandName = "Download" Then
            Dim lblNoFaktur As String = e.Item.Cells(4).Text

            Dim critEDoc As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartEDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'critEDoc.opAnd(New Criteria(GetType(SparePartBilling), "SparePartBilling.BillingNumber", MatchType.Exact, lblNoFaktur))
            critEDoc.opAnd(New Criteria(GetType(SparePartEDocument), "DocNumber", MatchType.Exact, lblNoFaktur))
            critEDoc.opAnd(New Criteria(GetType(SparePartEDocument), "DocType", MatchType.Exact, 2))
            Dim arlEDoc As ArrayList = New SparePartEDocumentFacade(User).Retrieve(critEDoc)
            If arlEDoc.Count > 0 Then
                Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & CType(arlEDoc(0), SparePartEDocument).Path
                Response.Redirect("../Download.aspx?file=" & PathFile)
            Else
                MessageBox.Show("Data tidak ditemukan")
                Exit Sub
            End If

            'Response.Redirect("../Download.aspx?file=" & KTB.DNet.Lib.WebConfig.GetValue("SAN") & e.CommandArgument)
        End If
    End Sub

    Private Sub dgDepositC2List_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDepositC2List.ItemDataBound
        '-- Handle databinding of template columns

        If Not e.Item.DataItem Is Nothing Then
            Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim lbtnDownload As LinkButton = CType(e.Item.FindControl("lbtnDownload"), LinkButton)
            Dim NoDoc As String = e.Item.Cells(4).Text

            Dim critEDoc As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartEDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critEDoc.opAnd(New Criteria(GetType(SparePartEDocument), "DocType", MatchType.Exact, 2))
            critEDoc.opAnd(New Criteria(GetType(SparePartEDocument), "DocNumber", MatchType.Exact, NoDoc))
            Dim arlEDoc As ArrayList = New SparePartEDocumentFacade(User).Retrieve(critEDoc)
            If arlEDoc.Count > 0 Then
                lbtnDownload.Visible = True
            Else
                lbtnDownload.Visible = False
            End If

            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = (e.Item.ItemIndex + 1).ToString()  '-- Column No


                lbtnDownload.Text = "<img src=""../images/download.gif"" border=""0"" alt=" & e.Item.Cells(4).Text & ">"
                lbtnDownload.Attributes.Add("OnClick", "return confirm('Anda Yakin Mau Download?');")
            End If

            e.Item.Cells(2).Text = CType(e.Item.DataItem, DepositC2Line).DepositC2.Dealer.DealerCode


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
        Dim DepositC2Data As String = Server.MapPath("") & "\..\DataTemp\DepositC2Data" & sSuffix & ".txt"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNET.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNET.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNET.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(DepositC2Data)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(DepositC2Data, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteDepositC2Data(sw)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\DepositC2Data" & sSuffix & ".txt")

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

    Private Sub dgDepositC2List_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDepositC2List.SortCommand
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

        Dim arlCompletelist As ArrayList = CType(sessHelp.GetSession("DepositC2Lines"), ArrayList)

        If Not arlCompletelist Is Nothing Then
            SortListControl(arlCompletelist, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Integer))
            dgDepositC2List.DataSource = arlCompletelist
            dgDepositC2List.DataBind()
        End If
    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        Dim crits As CriteriaComposite
        If dgDepositC2List.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        If Not IsNothing(sessHelp.GetSession(_vsCritSearch)) Then
            crits = CType(sessHelp.GetSession(_vsCritSearch), CriteriaComposite)
        End If
        arrData = New DepositC2LineFacade(User).Retrieve(crits)
        If arrData.Count > 0 Then
            CreateExcel("TOPSparepart-DisplayDepositC2", arrData)
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
            ws.Cells("C3").Value = "Document Date"
            ws.Cells("D3").Value = "Document No"
            ws.Cells("E3").Value = "Deposit C2"
            ws.Cells("F3").Value = "Billing No"

            For i As Integer = 0 To Data.Count - 1
                Dim item As DepositC2Line = Data(i)
                ws.Cells(i + 4, 1).Value = i + 1
                If IsNothing(item.DepositC2.Dealer) Then
                    ws.Cells(i + 4, 2).Value = ""
                Else
                    ws.Cells(i + 4, 2).Value = item.DepositC2.Dealer.DealerCode
                End If
                ws.Cells(i + 4, 3).Value = item.DocumentDate
                ws.Column(3).Style.Numberformat.Format = "DD/MM/YY"
                ws.Cells(i + 4, 4).Value = item.DocumentNo
                ws.Cells(i + 4, 5).Value = item.DepositC2Amnt.ToString("N0")
                ws.Cells(i + 4, 6).Value = item.BillingNumber
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
