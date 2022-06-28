Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Helper

Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports OfficeOpenXml

Public Class FrmBabitEventProposalList
    Inherits System.Web.UI.Page

    Private sesHelper As New SessionHelper
    Private displayPriv As Boolean
    Private editPriv As Boolean
    Private deletePriv As Boolean
    Private oDealer As Dealer
    Private SessionGridDataProposal As String = "FrmBabitEventProposalList.BabitEventProposalHeaderList"
    Private SessionCriteriaProposalEvent As String = "FrmBabitEventProposalList.CriteriaFrmBabitEventProposalList"
    Private Const strTypeCode As String = "V"
    Private Const strEnumBabitCategory As String = "EnumBabit.BabitParameterCategory"
    Private Const strValueCodeBiaya As String = "Biaya"
    Private Const strValueCodeAct As String = "Aktivitas"

    Private Property SesDealer() As Dealer
        Get
            Return CType(sesHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sesHelper.SetSession("DEALER", Value)
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Authorization()
        If Not IsPostBack Then
            ViewState("currSortColumn") = "EventRegNumber"
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            txtKodeDealer.Text = SesDealer().DealerCode
            hdnDealer.Value = SesDealer().DealerCode
            lblKodeDealer.Text = SesDealer().DealerCode & " / " & SesDealer().DealerName

            PageInit()
            BindDDLDealerCategory()

            '-- Restore selection criteria
            ReadCriteria()

            If IsLoginAsDealer() Then
                txtKodeDealer.Attributes("style") = "display:none"
                lblPopUpDealer.Attributes("style") = "display:none"
                lblKodeDealer.Attributes("style") = "display:table-row"

                trCategory.Attributes("style") = "display:none"
            Else
                lblPopUpDealer.Attributes("style") = "display:table-row"
                txtKodeDealer.Attributes("style") = "display:table-row"
                lblKodeDealer.Attributes("style") = "display:none"

                trCategory.Attributes("style") = "display:table-row"
                txtKodeDealer.Text = ""
            End If
            ReadData()   '-- Read all data matching criteria
            BindGrid(dgEventProposalList.CurrentPageIndex)  '-- Bind page-1

        End If

        lblPopUpDealer.Attributes("onclick") = "ShowPopUpDealer()"
        lblPopUpTO.Attributes("onclick") = "ShowPopUpTO()"
    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Private Sub Authorization()
        If Not SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Proposal_Display_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT - DAFTAR PROPOSAL EVENT")
        Else
            displayPriv = SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Proposal_Display_Privilege)
            editPriv = SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Proposal_Edit_Privilege)
            deletePriv = SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Proposal_Delete_Privilege)
        End If
    End Sub

    Private Sub PageInit()
        icPeriodeStart.Value = Date.Now.AddMonths(-1)
        icPeriodeEnd.Value = Date.Now
    End Sub

    Private Sub BindDDLDealerCategory()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, "MMC"))
        arrDDL = New CategoryFacade(User).Retrieve(criterias)

        With ddlCategory
            .Items.Clear()
            .DataSource = arrDDL
            .DataTextField = "CategoryCode"
            .DataValueField = "ID"
            .DataBind()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        End With
        ddlCategory.SelectedIndex = 0
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim arrBabitEventProposalHeaderList As ArrayList = CType(sesHelper.GetSession(SessionGridDataProposal), ArrayList)
        If arrBabitEventProposalHeaderList.Count <> 0 Then
            CommonFunction.SortListControl(arrBabitEventProposalHeaderList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrBabitEventProposalHeaderList, pageIndex, dgEventProposalList.PageSize)
            dgEventProposalList.DataSource = PagedList
            dgEventProposalList.VirtualItemCount = arrBabitEventProposalHeaderList.Count()
            dgEventProposalList.DataBind()
        Else
            dgEventProposalList.DataSource = New ArrayList
            dgEventProposalList.VirtualItemCount = 0
            dgEventProposalList.CurrentPageIndex = 0
            dgEventProposalList.DataBind()
        End If
    End Sub

    Private Sub ReadData()
        '-- Row status = active
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventProposalHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If ddlCategory.SelectedValue <> "-1" Then
            crit.opAnd(New Criteria(GetType(BabitEventProposalHeader), "EventDealerHeader.Category.ID", MatchType.Exact, ddlCategory.SelectedValue))
        End If

        If cbDate.Checked Then
            Dim tglFrom As New Date(icPeriodeStart.Value.Year, icPeriodeStart.Value.Month, icPeriodeStart.Value.Day, 0, 0, 0)
            Dim tglTo As New Date(icPeriodeEnd.Value.Year, icPeriodeEnd.Value.Month, icPeriodeEnd.Value.Day, 23, 59, 59)
            crit.opAnd(New Criteria(GetType(BabitEventProposalHeader), "PeriodStart", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")), "((", True)
            crit.opAnd(New Criteria(GetType(BabitEventProposalHeader), "PeriodStart", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")), ")", False)

            crit.opOr(New Criteria(GetType(BabitEventProposalHeader), "PeriodEnd", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")), "(", True)
            crit.opAnd(New Criteria(GetType(BabitEventProposalHeader), "PeriodEnd", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")), "))", False)
        End If

        If txtKodeDealer.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(BabitEventProposalHeader), "Dealer.DealerCode", MatchType.Partial, txtKodeDealer.Text))
        End If
        If txtKodeTempOut.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(BabitEventProposalHeader), "DealerBranch.DealerBranchCode", MatchType.InSet, "('" & txtKodeTempOut.Text.Replace(";", "','") & "')"))
        End If
        If txtEventProposalName.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(BabitEventProposalHeader), "EventProposalName", MatchType.Partial, txtEventProposalName.Text))
        End If
        If txtEventRegNumber.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(BabitEventProposalHeader), "EventRegNumber", MatchType.Partial, txtEventRegNumber.Text))
        End If

        If txtEventName.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(BabitEventProposalHeader), "EventDealerHeader.EventName", MatchType.Partial, Me.txtEventName.Text.Trim))
        End If
        If ddlCategory.SelectedValue <> "-1" Then
            Dim strSQL As String = "SELECT DealerID FROM DealerCategory WHERE CategoryID IN (SELECT ID FROM Category WHERE ID = " & ddlCategory.SelectedValue & ")"
            crit.opAnd(New Criteria(GetType(BabitEventProposalHeader), "Dealer.ID", MatchType.InSet, "(" & strSQL & ")"))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(BabitEventProposalHeader), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        '-- Retrieve recordset
        Dim arrBabitEventProposalHeaderList As ArrayList = New BabitEventProposalHeaderFacade(User).RetrieveByCriteria(crit, sortColl)
        sesHelper.SetSession(SessionGridDataProposal, arrBabitEventProposalHeaderList)
        If arrBabitEventProposalHeaderList.Count <= 0 Then
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ReadData()
        dgEventProposalList.CurrentPageIndex = 0 '-- Read all data matching criteria
        BindGrid(dgEventProposalList.CurrentPageIndex)  '-- Bind page-1
        StoreCriteria()
    End Sub

    Private Sub dgEventProposalList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgEventProposalList.PageIndexChanged
        '-- Change datagrid page

        ReadData()
        dgEventProposalList.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
        StoreCriteria()
    End Sub

    Private Sub dgEventProposalList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgEventProposalList.SortCommand
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

        '-- Bind page-1
        dgEventProposalList.CurrentPageIndex = 0
        ReadData()
        BindGrid(dgEventProposalList.CurrentPageIndex)
        StoreCriteria()
    End Sub

    Private Sub ReadCriteria()
        Dim crit As Hashtable
        crit = CType(sesHelper.GetSession(SessionCriteriaProposalEvent), Hashtable)
        If Not crit Is Nothing Then
            ddlCategory.SelectedValue = CStr(crit.Item("ddlCategory"))
            txtKodeDealer.Text = CStr(crit.Item("DealerCode"))
            lblKodeDealer.Text = CStr(crit.Item("DealerCode"))
            txtKodeTempOut.Text = CStr(crit.Item("DealerBranchCode"))
            cbDate.Checked = CBool(crit.Item("cbDate"))
            icPeriodeStart.Value = CStr(crit.Item("PeriodStart"))
            icPeriodeEnd.Value = CStr(crit.Item("PeriodEnd"))
            txtEventProposalName.Text = CStr(crit.Item("EventProposalName"))
            txtEventRegNumber.Text = CStr(crit.Item("EventRegNumber"))
            txtEventName.Text = CStr(crit.Item("EventName"))

            ViewState("currSortColumn") = CStr(crit.Item("currSortColumn"))
            ViewState("currSortDirection") = CStr(crit.Item("currSortDirection"))
            dgEventProposalList.CurrentPageIndex = CInt(crit.Item("PageIndex"))
        End If
    End Sub

    Private Sub StoreCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("ddlCategory", ddlCategory.SelectedValue)
        crit.Add("DealerCode", txtKodeDealer.Text)
        crit.Add("DealerBranchCode", txtKodeTempOut.Text)
        crit.Add("cbDate", cbDate.Checked)
        crit.Add("PeriodStart", icPeriodeStart.Value)
        crit.Add("PeriodEnd", icPeriodeEnd.Value)
        crit.Add("EventProposalName", txtEventProposalName.Text)
        crit.Add("EventRegNumber", txtEventRegNumber.Text)
        crit.Add("EventName", txtEventName.Text)

        crit.Add("PageIndex", dgEventProposalList.CurrentPageIndex)
        crit.Add("currSortColumn", ViewState("currSortColumn"))
        crit.Add("currSortDirection", ViewState("currSortDirection"))

        sesHelper.SetSession(SessionCriteriaProposalEvent, crit) '-- Store in session
    End Sub

    Protected Sub btnDownloadExcel_Click(sender As Object, e As EventArgs) Handles btnDownloadExcel.Click
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        If dgEventProposalList.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        ' mengambil data yang dibutuhkan
        arrData = CType(sesHelper.GetSession(SessionGridDataProposal), ArrayList)
        If arrData.Count > 0 Then
            CreateExcel("DAFTAR PROPOSAL EVENT", arrData)
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
            ws.Cells("B3").Value = "Status"
            ws.Cells("C3").Value = "Kode Dealer"
            ws.Cells("D3").Value = "Temporary Outlet"
            ws.Cells("E3").Value = "No. Reg Proposal"
            ws.Cells("F3").Value = "Nama Proposal Event"
            ws.Cells("G3").Value = "Tgl Periode Mulai"
            ws.Cells("H3").Value = "Tgl Periode Selesai"

            For i As Integer = 0 To Data.Count - 1
                Dim item As BabitEventProposalHeader = Data(i)
                ws.Cells(i + 4, 1).Value = i + 1
                ws.Cells(i + 4, 2).Value = IIf(item.EventStatus = 0, "Baru", IIf(item.EventStatus = 1, "Batal", "Konfirmasi"))
                If IsNothing(item.Dealer) Then
                    ws.Cells(i + 4, 3).Value = ""
                Else
                    ws.Cells(i + 4, 3).Value = item.Dealer.DealerCode
                End If
                If IsNothing(item.DealerBranch) Then
                    ws.Cells(i + 4, 4).Value = ""
                Else
                    ws.Cells(i + 4, 4).Value = item.DealerBranch.DealerBranchCode
                End If
                ws.Cells(i + 4, 5).Value = item.EventRegNumber
                ws.Cells(i + 4, 6).Value = item.EventProposalName
                ws.Cells(i + 4, 7).Value = Format(item.PeriodStart, "dd/MM/yyyy")
                ws.Cells(i + 4, 8).Value = Format(item.PeriodEnd, "dd/MM/yyyy")
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

    Protected Sub dgEventProposalList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgEventProposalList.ItemCommand
        Select Case e.CommandName
            Case "Detail"
                Response.Redirect("FrmInputBabitEventProposal.aspx?Mode=Detail&BabitEventProposalHeaderID=" & e.Item.Cells(0).Text)
            Case "Edit"
                Response.Redirect("FrmInputBabitEventProposal.aspx?Mode=Edit&BabitEventProposalHeaderID=" & e.Item.Cells(0).Text)
            Case "Delete"
                DeleteBabitEventProposal(CInt(e.CommandArgument))
        End Select
    End Sub

    Private Sub DeleteBabitEventProposal(ByVal intBabitEventProposalHdrID As Integer)
        Dim objBabitEventReportHeaderFacade As BabitEventReportHeaderFacade = New BabitEventReportHeaderFacade(User)
        Dim arrBabitEventReportHeader As ArrayList = New ArrayList
        Dim criteriasaa As New CriteriaComposite(New Criteria(GetType(BabitEventReportHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriasaa.opAnd(New Criteria(GetType(BabitEventReportHeader), "BabitEventProposalHeader.ID", MatchType.Exact, intBabitEventProposalHdrID))
        arrBabitEventReportHeader = objBabitEventReportHeaderFacade.Retrieve(criteriasaa)
        If Not IsNothing(arrBabitEventReportHeader) AndAlso arrBabitEventReportHeader.Count > 0 Then
            MessageBox.Show("Nomor Reg Proposal ini sudah di pakai di Laporan Event")
            Exit Sub
        End If

        Dim objBabitEventProposalHeaderFacade As BabitEventProposalHeaderFacade = New BabitEventProposalHeaderFacade(User)
        Dim objBabitEventProposalDetailFacade As BabitEventProposalDetailFacade = New BabitEventProposalDetailFacade(User)
        Dim objBabitEventProposalDocumentFacade As BabitEventProposalDocumentFacade = New BabitEventProposalDocumentFacade(User)
        'Dim objBabitEventProposalActivityFacade As BabitEventProposalActivityFacade = New BabitEventProposalActivityFacade(User)

        Dim objBabitEventProposalHeader As BabitEventProposalHeader = objBabitEventProposalHeaderFacade.Retrieve(intBabitEventProposalHdrID)
        objBabitEventProposalHeader.RowStatus = CType(DBRowStatus.Deleted, Short)

        '--untuk data biaya
        Dim arrBabitEventProposalDetail As ArrayList = New ArrayList
        arrBabitEventProposalDetail = New BabitEventProposalDetailFacade(User).RetrieveDataBabitEventProposalDetail(intBabitEventProposalHdrID, strTypeCode, strEnumBabitCategory, strValueCodeBiaya)
        For Each obj As BabitEventProposalDetail In arrBabitEventProposalDetail
            obj.RowStatus = CType(DBRowStatus.Deleted, Short)
        Next

        '--untuk data aktivitas
        Dim arrBabitEventProposalAct As ArrayList = New ArrayList
        arrBabitEventProposalAct = New BabitEventProposalDetailFacade(User).RetrieveDataBabitEventProposalDetail(intBabitEventProposalHdrID, strTypeCode, strEnumBabitCategory, strValueCodeAct)
        For Each obj As BabitEventProposalDetail In arrBabitEventProposalAct
            obj.RowStatus = CType(DBRowStatus.Deleted, Short)
        Next

        '---untuk data dokumen
        Dim arrBabitEventProposalDoc As ArrayList = New ArrayList
        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(BabitEventProposalDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(BabitEventProposalDocument), "BabitEventProposalHeader.ID", MatchType.Exact, intBabitEventProposalHdrID))
        arrBabitEventProposalDoc = objBabitEventProposalDocumentFacade.Retrieve(criterias2)
        For Each obj As BabitEventProposalDocument In arrBabitEventProposalDoc
            obj.RowStatus = CType(DBRowStatus.Deleted, Short)
        Next

        Dim _result As Integer = 0
        _result = New BabitEventProposalDetailFacade(User).UpdateTransaction(objBabitEventProposalHeader, arrBabitEventProposalDetail, New ArrayList, arrBabitEventProposalDoc, New ArrayList, arrBabitEventProposalAct, New ArrayList)
        If _result > 0 Then
            MessageBox.Show("Delete Data Sukses")
        End If
        ReadData()
        BindGrid(dgEventProposalList.CurrentPageIndex)
    End Sub

    Protected Sub dgEventProposalList_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgEventProposalList.ItemDataBound
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
        Dim lblTempOut As Label = CType(e.Item.FindControl("lblTempOut"), Label)
        Dim lblEventRegNumber As Label = CType(e.Item.FindControl("lblEventRegNumber"), Label)
        Dim lblEventProposalName As Label = CType(e.Item.FindControl("lblEventProposalName"), Label)
        'Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)

        Dim lblPeriodStart As Label = CType(e.Item.FindControl("lblPeriodStart"), Label)
        Dim lblPeriodEnd As Label = CType(e.Item.FindControl("lblPeriodEnd"), Label)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As BabitEventProposalHeader = CType(e.Item.DataItem, BabitEventProposalHeader)
            lblNo.Text = (e.Item.ItemIndex + 1 + (dgEventProposalList.PageSize * dgEventProposalList.CurrentPageIndex)).ToString

            'Select Case oData.EventStatus
            '    Case 0
            '        lblStatus.Text = "Baru"
            '    Case 1
            '        lblStatus.Text = "Batal"
            '    Case 2
            '        lblStatus.Text = "Konfirmasi"
            'End Select

            If IsNothing(oData.Dealer) Then
                'lblDealer.Text = oData.ID
            Else
                lblDealer.Text = oData.Dealer.DealerCode
            End If
            If IsNothing(oData.DealerBranch) Then
                'lblTempOut.Text = oData.ID
            Else
                lblTempOut.Text = oData.DealerBranch.DealerBranchCode
            End If
            lblEventRegNumber.Text = oData.EventRegNumber
            lblEventProposalName.Text = oData.EventProposalName
            lblPeriodStart.Text = oData.PeriodStart
            lblPeriodEnd.Text = oData.PeriodEnd

            Dim lnkbtnDetail As LinkButton = CType(e.Item.FindControl("lnkbtnDetail"), LinkButton)
            Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
            Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)
            If Not IsNothing(lnkbtnDetail) Then
                lnkbtnDetail.Visible = displayPriv
            End If
            If Not IsNothing(lnkbtnEdit) Then
                lnkbtnEdit.Visible = editPriv
            End If
            If Not IsNothing(lnkbtnDelete) Then
                lnkbtnDelete.Visible = deletePriv
            End If
        End If
    End Sub

    Protected Sub hdnDealer_ValueChanged(sender As Object, e As EventArgs) Handles hdnDealer.ValueChanged
        Dim data As String() = hdnDealer.Value.Trim.Split(";")
        txtKodeDealer.Text = data(0)
    End Sub

    Protected Sub hdnTempOut_ValueChanged(sender As Object, e As EventArgs) Handles hdnTempOut.ValueChanged
        Dim data As String() = hdnTempOut.Value.Trim.Split(";")
        txtKodeTempOut.Text = data(0)
    End Sub
End Class