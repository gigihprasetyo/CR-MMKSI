Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Security

Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports OfficeOpenXml
Imports System.Linq

Public Class FrmBabitJVList
    Inherits System.Web.UI.Page

    Private oDealer As Dealer
    Private sessHelper As New SessionHelper
    Private arlCheckedItemColl As ArrayList = New ArrayList
    Private SessionCriteriaBabit = "FrmBabitJVList.CriteriaList"
    Private PageSession = "FrmBabitJVList"
    Private GridIndexSessionBefore = "FrmBabitJVList_PgIdxBefore"
    Private GridIndexSessionNext = "FrmBabitJVList_PgIdxNext"
    Private listProcessSession = "babitSessBabitJVListProcess"
    Private listProcessSession2 = "babitSessBabitJVListProcess2"
    Private displayPriv As Boolean
    Private editPriv As Boolean
    Private transferSAPPriv As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Authorization()

        If Not IsPostBack Then
            PageInit()
            BindGrid(0)
        End If
        lblPopUpDealer.Attributes("onclick") = "ShowPopUpDealer()"

    End Sub

    Private Sub Authorization()
        If Not SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_Pencairan_Babit_Detail_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=BABIT - DAFTAR PENCAIRAN BABIT")
        Else
            displayPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_Pencairan_Babit_Detail_Privilege)
            editPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_Pencairan_Babit_Edit_Privilege)
            transferSAPPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_Pencairan_Babit_TransferSAP_Privilege)
        End If

        If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            txtKodeDealer.Visible = True
            lblKodeDealer.Visible = False
            lblPopUpDealer.Visible = True
            btnTransfer.Visible = transferSAPPriv
            btnTransferUlang.Visible = transferSAPPriv
        Else
            txtKodeDealer.Text = oDealer.DealerCode
            lblKodeDealer.Text = oDealer.DealerCode & " / " & oDealer.DealerName

            txtKodeDealer.Visible = False
            lblKodeDealer.Visible = True
            lblPopUpDealer.Visible = False
            btnTransfer.Visible = False
            btnTransferUlang.Visible = False
        End If
    End Sub

    Private Sub PageInit()
        bindDDLs()
        ViewState("currSortColumn") = "ID"
        ViewState("currSortDirection") = Sort.SortDirection.DESC

        'Jika Login Dealer
        If oDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            dgListJV.Columns(dgListJV.Columns.Count - 4).Visible = False    'Column Tgl Proses
            dgListJV.Columns(dgListJV.Columns.Count - 3).Visible = False    'Column No JV
        End If
    End Sub

    Private Sub bindDDLs()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumBabit.BabitJVStatus"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(StandardCode), "Sequence", CType(Sort.SortDirection.ASC, Sort.SortDirection)))
        arrDDL = New StandardCodeFacade(User).Retrieve(criterias, sortColl)
        With ddlStatus
            .Items.Clear()
            .DataSource = arrDDL
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
        End With
        ddlStatus.Items.Insert(0, New ListItem("Silahkan Piih ", -1))
        ddlStatus.SelectedIndex = 0
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim total As Integer = 0
        Dim crit As CriteriaComposite
        If Not IsNothing(ViewState("Back")) Then
            ReadCriteria()
            crit = SearchCriteria()
            ViewState.Remove("Back")
        Else
            crit = SearchCriteria()
        End If

        If Not IsNothing(CType(sessHelper.GetSession(PageSession), ArrayList)) Then
            sessHelper.SetSession(GridIndexSessionBefore, CType(sessHelper.GetSession(GridIndexSessionNext), Integer))
            sessHelper.SetSession(GridIndexSessionNext, pageIndex)

            arlCheckedItemColl = GetCheckedItem()
            Dim currentPage As String = CType(sessHelper.GetSession(GridIndexSessionBefore), String)
            sessHelper.SetSession(listProcessSession + currentPage, arlCheckedItemColl)

            Dim arlUnCheckedItemColl As ArrayList = GetUnCheckedItem()
            currentPage = CType(sessHelper.GetSession(GridIndexSessionBefore), String)
            sessHelper.SetSession(listProcessSession2 + currentPage, arlUnCheckedItemColl)
        End If

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(BabitReportJV), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        'Dim listSource As ArrayList = New BabitReportJVtoReceiptFacade(User).Retrieve(crit, sortColl)
        Dim listSource As ArrayList = New BabitReportJVFacade(User).Retrieve(crit, sortColl)
        If listSource.Count <> 0 Then
            CommonFunction.SortListControl(listSource, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(listSource, pageIndex, dgListJV.PageSize)
            sessHelper.SetSession(PageSession, PagedList)
            dgListJV.DataSource = PagedList
            dgListJV.VirtualItemCount = listSource.Count
            dgListJV.DataBind()
        Else
            dgListJV.DataSource = New ArrayList
            dgListJV.VirtualItemCount = 0
            dgListJV.CurrentPageIndex = 0
            dgListJV.DataBind()
        End If
    End Sub

    Private Sub ReadCriteria()
        Dim crit As Hashtable
        crit = CType(sessHelper.GetSession(SessionCriteriaBabit), Hashtable)
        If Not crit Is Nothing Then
            txtKodeDealer.Text = CStr(crit.Item("DealerCode"))
            lblKodeDealer.Text = CStr(crit.Item("DealerCode"))
            ddlStatus.SelectedValue = CStr(crit.Item("ddlStatus"))
            ViewState("currSortColumn") = CStr(crit.Item("currSortColumn"))
            ViewState("currSortDirection") = CStr(crit.Item("currSortDirection"))
            dgListJV.CurrentPageIndex = CInt(crit.Item("PageIndex"))
        End If
    End Sub

    Private Function SearchCriteria() As CriteriaComposite
        Dim strSql As String = String.Empty
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportJV), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtKodeDealer.Text <> "" Then
            crit.opAnd(New Criteria(GetType(BabitReportJV), "Dealer.DealerCode", MatchType.Partial, txtKodeDealer.Text))
        End If

        If Not oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            crit.opAnd(New Criteria(GetType(BabitReportJV), "Dealer.DealerCode", MatchType.Partial, lblKodeDealer.Text.Trim.Split("/")(0).Trim))
        End If

        If txtNoJV.Text <> "" Then
            crit.opAnd(New Criteria(GetType(BabitReportJV), "RegNumber", MatchType.Partial, txtNoJV.Text))
        End If

        'TODO
        If ddlStatus.SelectedIndex <> 0 Then
            crit.opAnd(New Criteria(GetType(BabitReportJV), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        sessHelper.SetSession("criteriadownload", crit)
        Return crit
    End Function

    Private Function GetCheckedItem() As ArrayList
        Dim arrGrid As ArrayList = CType(sessHelper.GetSession(PageSession), ArrayList)
        Dim arlCheckedItem As ArrayList = New ArrayList
        Dim nIndeks As Integer
        For Each dtgItem As DataGridItem In dgListJV.Items
            nIndeks = dtgItem.ItemIndex
            Dim objCM As BabitReportJV = CType(arrGrid(nIndeks), BabitReportJV)
            If CType(dtgItem.Cells(2).FindControl("chkSelect"), CheckBox).Checked Then
                arlCheckedItem.Add(objCM)
            End If
        Next

        Return arlCheckedItem
    End Function

    Private Function GetUnCheckedItem() As ArrayList
        Dim arrGrid As ArrayList = CType(sessHelper.GetSession(PageSession), ArrayList)
        Dim arlCheckedItem As ArrayList = New ArrayList
        Dim nIndeks As Integer
        For Each dtgItem As DataGridItem In dgListJV.Items
            nIndeks = dtgItem.ItemIndex
            Dim objCM As BabitReportJV = CType(arrGrid(nIndeks), BabitReportJV)
            If Not CType(dtgItem.Cells(2).FindControl("chkSelect"), CheckBox).Checked Then
                arlCheckedItem.Add(objCM)
            End If
        Next

        Return arlCheckedItem
    End Function

    Protected Sub dgListJV_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgListJV.ItemDataBound
        Dim objBR As BabitReportJV = New BabitReportJV
        Dim chkSelect As CheckBox = CType(e.Item.FindControl("chkSelect"), CheckBox)
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
        Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
        Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
        Dim lblNoPengajuanJV As Label = CType(e.Item.FindControl("lblNoPengajuanJV"), Label)
        Dim lblTglProcess As Label = CType(e.Item.FindControl("lblTglProcess"), Label)
        Dim lblNoJV As Label = CType(e.Item.FindControl("lblNoJV"), Label)
        Dim lblTglCair As Label = CType(e.Item.FindControl("lblTglCair"), Label)
        Dim lblNoRefJV As Label = CType(e.Item.FindControl("lblNoRefJV"), Label)
        Dim lnkbtnDetail As LinkButton = CType(e.Item.FindControl("lnkbtnDetail"), LinkButton)
        Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As BabitReportJV = CType(e.Item.DataItem, BabitReportJV)
            chkSelect.Checked = False
            Dim currentPage As String = CType(sessHelper.GetSession(GridIndexSessionNext), String)
            Dim arrGridDF As ArrayList = CType(sessHelper.GetSession(listProcessSession + currentPage), ArrayList)
            If IsNothing(arrGridDF) Then arrGridDF = New ArrayList
            For Each oDF As BabitReportJV In arrGridDF
                If objBR.ID = oDF.ID Then
                    chkSelect.Checked = True
                    Exit For
                End If
            Next

            lblNo.Text = (dgListJV.PageSize * dgListJV.CurrentPageIndex) + e.Item.ItemIndex + 1
            If IsNothing(oData.Dealer) Then
                lblDealerCode.Text = oData.ID
                lblDealerName.Text = oData.ID
            Else
                lblDealerCode.Text = oData.Dealer.DealerCode
                lblDealerName.Text = oData.Dealer.DealerName
            End If

            lblNoPengajuanJV.Text = oData.RegNumber
            If oData.TglProses.Year > 2000 Then
                lblTglProcess.Text = oData.TglProses.ToString("dd/MM/yyyy")
            Else
                lblTglProcess.Text = ""
            End If
            If oData.TglPencairan.Year > 2000 Then
                lblTglCair.Text = oData.TglPencairan.ToString("dd/MM/yyyy")
            Else
                lblTglCair.Text = ""
            End If
            lblNoJV.Text = oData.NoJV
            lblNoRefJV.Text = oData.TextRefNo

            lnkbtnEdit.Visible = editPriv
            lnkbtnDetail.Visible = displayPriv

            Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumBabit.BabitJVStatus"))
            criterias.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, oData.Status))
            Dim arrDDL As ArrayList = New StandardCodeFacade(User).Retrieve(criterias)
            Dim objStandardCode As New StandardCode
            If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
                objStandardCode = CType(arrDDL(0), StandardCode)
                lblStatus.Text = objStandardCode.ValueDesc
            End If
            If oData.Status <> 0 Then
                lnkbtnEdit.Visible = False
            End If

            'jika login dealer then button edit is disable
            If oDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                lnkbtnEdit.Visible = False
            End If
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindGrid(0)
        StoreCriteria()
        If dgListJV.Items.Count = 0 Then
            MessageBox.Show("Data list tidak ditemukan")
        End If
    End Sub

    Private Sub StoreCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("DealerCode", txtKodeDealer.Text)
        crit.Add("ddlStatus", ddlStatus.SelectedValue)
        crit.Add("PageIndex", dgListJV.CurrentPageIndex)
        crit.Add("currSortColumn", ViewState("currSortColumn"))
        crit.Add("currSortDirection", ViewState("currSortDirection"))

        sessHelper.SetSession(SessionCriteriaBabit, crit)  '-- Store in session
    End Sub

    Protected Sub btnDownloadExcel_Click(sender As Object, e As EventArgs) Handles btnDownloadExcel.Click
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        Dim crits As CriteriaComposite
        If dgListJV.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        If Not IsNothing(sessHelper.GetSession("criteriadownload")) Then
            crits = CType(sessHelper.GetSession("criteriadownload"), CriteriaComposite)
        End If
        ' mengambil data yang dibutuhkan
        arrData = New BabitReportJVFacade(User).Retrieve(crits)
        If arrData.Count > 0 Then
            CreateExcel("Daftar Pencairan Babit", arrData)
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
            ws.Cells("D3").Value = "Nama Dealer"
            ws.Cells("E3").Value = "No Dokumen"
            ws.Cells("F3").Value = "No Referensi Text"
            ws.Cells("G3").Value = "Tgl Cair"
            If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                ws.Cells("H3").Value = "Tgl Proses"
                ws.Cells("I3").Value = "No JV"
            End If

            For i As Integer = 0 To Data.Count - 1
                Dim oBabitReportJV As BabitReportJV = Data(i)
                ws.Cells(i + 4, 1).Value = i + 1
                'Select Case oBabitReportJV.Status
                '    Case 0
                '        ws.Cells(i + 4, 2).Value = "Baru"
                '    Case 1
                '        ws.Cells(i + 4, 2).Value = "Proses"
                '    Case 2
                '        ws.Cells(i + 4, 2).Value = "Cair"
                'End Select
                ws.Cells(i + 4, 2).Value = CommonFunction.GetEnumDescription(oBabitReportJV.Status, "EnumBabit.BabitJVStatus")

                If IsNothing(oBabitReportJV.Dealer) Then
                    ws.Cells(i + 4, 3).Value = ""
                    ws.Cells(i + 4, 4).Value = ""
                Else
                    ws.Cells(i + 4, 3).Value = oBabitReportJV.Dealer.DealerCode
                    ws.Cells(i + 4, 4).Value = oBabitReportJV.Dealer.DealerName
                End If

                ws.Cells(i + 4, 5).Value = oBabitReportJV.RegNumber
                ws.Cells(i + 4, 6).Value = oBabitReportJV.TextRefNo
                ws.Cells(i + 4, 7).Value = Format(oBabitReportJV.TglPencairan, "dd/MM/yyyy").ToString
                If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    ws.Cells(i + 4, 8).Value = Format(oBabitReportJV.TglProses, "dd/MM/yyyy").ToString
                    ws.Cells(i + 4, 9).Value = oBabitReportJV.NoJV
                End If

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
        Response.ContentType = "application/vnd.ms-excel" 'xls
        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()

    End Sub

    Protected Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click, btnTransferUlang.Click
        'Tambahkan logic ketika tombol Transfer dan Transfer Ulang dg ketentuan berikut :
        '1:.Update BabitReportJV.Status = 1
        '2:.Update BabitReportJV.IsTransfer = 1
        '3:.Update BabitReportJV.TglProses = getdate()
        '4:.Update BabitReportJV.LastUpdateBy & BabitReportJV.LastUpdateTime
        '5.Buat WSM transfer ke SAP (lihat detail di sheet WSM to SAP)
        'create txt file to folder :
        'QA: \\172.17.31.121\MDnet\SAP\FinishUnit\Campaign
        'Prod: \\172.17.31.62\MDNet\SAP\FinishUnit\campaign
        'nama File : JVBBTCMPGN [Timestamp].txt
        Dim arrchk As ArrayList = GetCheckedItem()
        If Not IsNothing(arrchk) AndAlso arrchk.Count = 0 Then
            MessageBox.Show("Tidak ada data JV yang di pilih")
            Exit Sub
        ElseIf IsNothing(arrchk) Then
            MessageBox.Show("Tidak ada data JV yang di pilih")
            Exit Sub
        End If

        If CType(sender, Button).ClientID = "btnTransfer" Then
            For Each bJV As BabitReportJV In arrchk
                If bJV.Status <> 0 Then
                    MessageBox.Show("Transfer JV hanya dapat dilakukan untuk status JV Baru")
                    Exit Sub
                End If
            Next
        End If
        If CType(sender, Button).ClientID = "btnTransferUlang" Then
            For Each bJV As BabitReportJV In arrchk
                If bJV.Status <> 1 Then
                    MessageBox.Show("Transfer Ulang JV hanya dapat dilakukan untuk status JV Proses")
                    Exit Sub
                End If
            Next
        End If

        Dim sb As StringBuilder = New StringBuilder
        Dim sbTextReceiptNo As StringBuilder = New StringBuilder
        Dim sbTextRefNo As StringBuilder = New StringBuilder
        Dim errFlag As Boolean = False
        Dim arrSuccess As New ArrayList

        For Each oBJV As BabitReportJV In arrchk
            errFlag = False
            If oBJV.TextReceiptNo.Trim = "" Then
                sbTextReceiptNo.Append(oBJV.RegNumber & ", ")
                errFlag = True
            End If
            If oBJV.TextRefNo.Trim = "" Then
                sbTextRefNo.Append(oBJV.RegNumber & ", ")
                errFlag = True
            End If
            If errFlag = True Then
                Continue For
            End If

            oBJV.Status = 1
            oBJV.IsTransfer = 1
            oBJV.TglProses = Date.Now
            arrSuccess.Add(oBJV)
        Next

        If sbTextReceiptNo.ToString().Trim <> "" Then
            sb.Append("- Nomor Referensi Kuitansi masih kosong untuk Nomor JV: " & Left(sbTextReceiptNo.ToString().Trim, Len(sbTextReceiptNo.ToString().Trim) - 1) & ". Silahkan input Nomor Referensi Kuitansi terlebih dahulu\n\n")
        End If
        If sbTextRefNo.ToString().Trim <> "" Then
            sb.Append("- Teks Referensi masih kosong untuk Nomor JV: " & Left(sbTextRefNo.ToString().Trim, Len(sbTextRefNo.ToString().Trim) - 1) & ". Silahkan input Teks Referensi terlebih dahulu\n\n")
        End If
        If sb.ToString().Trim <> "" Then
            MessageBox.Show(sb.ToString())
            'Exit Sub
        End If

        WriteBabitJV(arrSuccess)

        dgListJV.CurrentPageIndex = 0
        BindGrid(dgListJV.CurrentPageIndex)
        StoreCriteria()
    End Sub

    Private Sub WriteBabitJV(arrSuccess As ArrayList)
        Dim _return As Integer = 0
        Dim lines As New StringBuilder
        'Dim arrSuccess As ArrayList = GetCheckedItem()
        If arrSuccess.Count = 0 Then
            MessageBox.Show("Tidak ada data JV yang sukses transfer")
            Exit Sub
        End If

        Dim separator As String = ";"
        Dim index As Integer = 0
        Dim errorMes As String = ""

        For Each oBJV As BabitReportJV In arrSuccess
            Dim line As New System.Text.StringBuilder
            Dim item As BabitReportJVtoReceipt = New BabitReportJVtoReceiptFacade(User).RetrieveByBabitReportJV(oBJV.ID)

            Dim FakturPajakDate As Date = Date.Now
            Dim ReceiptDate As Date = Date.Now
            Dim q As New BabitReportJVtoReceiptFacade(User)
            q.RetrieveOldestDate(oBJV.ID, FakturPajakDate, ReceiptDate)
            Dim Total As Decimal = 0
            If Not item Is Nothing Then
                line.Append("H")
                line.Append(separator)
                line.Append(item.BabitReportJV.Dealer.DealerCode)
                line.Append(separator)
                line.Append(item.BabitReportJV.RegNumber)
                line.Append(separator)
                line.Append(FakturPajakDate.ToString("yyyyMMdd"))
                line.Append(separator)
                line.Append(ReceiptDate.ToString("yyyyMMdd"))
                line.Append(separator)
                line.Append(item.BabitReportJV.TextReceiptNo)
                line.Append(separator)
                'line.Append(item.BabitReportJV.DealerBankAccount.BankAccount)
                If Not IsNothing(item.BabitReportReceipt.DealerBankAccount) Then
                    line.Append(item.BabitReportReceipt.DealerBankAccount.BankAccount)
                Else
                    line.Append("")
                End If
                line.Append(separator)
                line.Append(item.BabitReportJV.TextRefNo)
                line.Append(separator)

                line.Append(vbNewLine)

                Dim arrDetail As ArrayList = New BabitReportJVtoReceiptFacade(User).RetrieveArrBabitReportReceipt(oBJV.ID)
                Dim indexMCC As Integer = 0
                For Each detail As BabitReportJVtoReceipt In arrDetail
                    Dim critBabitDealerAllocation As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitDealerAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    critBabitDealerAllocation.opAnd(New Criteria(GetType(BabitDealerAllocation), "BabitHeader.ID", MatchType.Exact, detail.BabitReportReceipt.BabitReportHeader.BabitHeader.ID))
                    Dim arrBDA As ArrayList = New BabitDealerAllocationFacade(User).Retrieve(critBabitDealerAllocation)
                    'Dim BDA As BabitDealerAllocation = New BabitDealerAllocation
                    Dim MCC As MasterCostCenter = New MasterCostCenter
                    If arrBDA.Count > 0 Then
                        For Each BDA As BabitDealerAllocation In arrBDA
                            line.Append("D")
                            line.Append(separator)
                            line.Append("ACC")
                            line.Append(separator)
                            'BDA = arrBDA(0)

                            Dim critMCC As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MasterCostCenter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            critMCC.opAnd(New Criteria(GetType(MasterCostCenter), "Value", MatchType.Exact, BDA.BabitCategory))
                            critMCC.opAnd(New Criteria(GetType(MasterCostCenter), "Type", MatchType.Exact, 1))
                            critMCC.opAnd(New Criteria(GetType(MasterCostCenter), "Status", MatchType.Exact, 1))
                            Dim arrMCC As ArrayList = New MasterCostCenterFacade(User).Retrieve(critMCC)

                            If arrMCC.Count > 0 Then
                                Try
                                    MCC = arrMCC(indexMCC)
                                Catch
                                    MCC = arrMCC(0)
                                End Try
                            End If
                            line.Append(MCC.CostCenterCode)
                            line.Append(separator)
                            If Not IsNothing(detail.BabitReportReceipt.MasterAccrued) Then
                                line.Append(detail.BabitReportReceipt.MasterAccrued.BussinessAreaCode)
                            End If
                            line.Append(separator)

                            Dim ioValue As String = ""
                            Select Case detail.BabitReportReceipt.BabitReportHeader.BabitHeader.AllocationType
                                Case 0
                                    ioValue = "Reguler"
                                Case 1
                                    ioValue = "Special"
                            End Select

                            Dim critMIO As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MasterInternalOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            critMIO.opAnd(New Criteria(GetType(MasterInternalOrder), "Value", MatchType.Exact, ioValue))
                            critMIO.opAnd(New Criteria(GetType(MasterInternalOrder), "Type", MatchType.Exact, 1))
                            critMIO.opAnd(New Criteria(GetType(MasterInternalOrder), "Status", MatchType.Exact, 1))
                            Dim arrMIO As ArrayList = New MasterInternalOrderFacade(User).Retrieve(critMIO)
                            Dim MIO As MasterInternalOrder = New MasterInternalOrder
                            If arrMIO.Count > 0 Then
                                MIO = arrMIO(0)
                            End If
                            line.Append(MIO.InternalOrderCode)
                            line.Append(separator)

                            Dim endDetail1 As String = ""
                            Select Case detail.BabitReportReceipt.BabitReportHeader.BabitHeader.BabitMasterEventType.ID
                                Case 4 'Iklan
                                    Try
                                        line.Append(CInt(arrBDA(indexMCC).SubsidyAmount))
                                    Catch
                                        line.Append(CInt(BDA.SubsidyAmount))
                                    End Try
                                    Total += BDA.SubsidyAmount
                                    endDetail1 = "By. IKLANTERPADU: "
                                Case Else
                                    line.Append(CInt(detail.BabitReportReceipt.ClaimAmount))
                                    Total += detail.BabitReportReceipt.ClaimAmount
                                    endDetail1 = "By. PAMERANTERPADU: "
                            End Select

                            line.Append(separator)
                            line.Append(detail.BabitReportReceipt.MasterAccrued.AccKey)
                            line.Append(separator)
                            line.Append(endDetail1 & detail.BabitReportReceipt.BabitReportHeader.BabitHeader.ApprovalNumber.Replace(Chr(160), Chr(32)))
                            line.Append(vbNewLine)


                            line.Append("D")
                            line.Append(separator)
                            line.Append("VAT")
                            line.Append(separator)
                            Select Case detail.BabitReportReceipt.BabitReportHeader.BabitHeader.BabitMasterEventType.ID
                                Case 4 'Iklan
                                    Try
                                        line.Append(CInt((10 / 100) * (arrBDA(indexMCC).SubsidyAmount)))
                                    Catch
                                        line.Append(CInt((10 / 100) * (BDA.SubsidyAmount)))
                                    End Try
                                Case Else
                                    line.Append(CInt(detail.BabitReportReceipt.VATTotal))
                            End Select
                            line.Append(separator)
                            line.Append(detail.BabitReportReceipt.FakturPajakNo)
                            line.Append(separator)
                            line.Append(endDetail1 & detail.BabitReportReceipt.BabitReportHeader.BabitHeader.ApprovalNumber.Replace(Chr(160), Chr(32)))
                            line.Append(vbNewLine)

                            indexMCC += 1

                        Next
                    End If
                Next

                line.Append("D")
                line.Append(separator)
                line.Append("WHT")
                line.Append(separator)
                line.Append(CInt((2 / 100) * Total))
                line.Append(separator)
                line.Append(Date.Now.ToString("yyyyMMdd"))
                line.Append(separator)
                line.Append(item.BabitReportJV.TextRefNo)
                line.Append(vbNewLine)
                lines.Append(line)

            End If
        Next
        If errorMes.Length <> 0 Then
            MessageBox.Show(errorMes)
            Exit Sub
        Else
            For Each oBJV As BabitReportJV In arrSuccess
                Dim item As BabitReportJVtoReceipt = New BabitReportJVtoReceiptFacade(User).RetrieveByBabitReportJV(oBJV.ID)
                If Not item Is Nothing Then
                    _return = New BabitReportJVFacade(User).Update(oBJV)
                End If
            Next
            If lines.ToString().Trim <> "" Then
                DoSendSAP(lines)
            Else
                MessageBox.Show("Tidak ada data yang diupload ke SAP")
            End If
        End If
    End Sub
    Private Sub DoSendSAP(ByVal lines As StringBuilder)
        Dim datetimenow As String = Now.ToString("yyyyMMddHmmss")

        Dim ClaimDataPath As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\FinishUnit\babit\JVBBTCMPGN" & datetimenow & ".txt"
        'Dim ClaimDataPath As String = "D:\Data\FinishUnit\campaign\JVBBTCMPGN" & datetimenow & ".txt"

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        imp.Start()
        Try
            Dim dirInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(Path.GetDirectoryName(ClaimDataPath))

            If Not dirInfo.Exists Then
                dirInfo.Create()
            End If
            Dim fs As FileStream = New FileStream(ClaimDataPath, FileMode.CreateNew)
            Dim sw As StreamWriter = New StreamWriter(fs)

            sw.WriteLine(lines.ToString)
            sw.Close()
            fs.Close()

            imp.StopImpersonate()
            imp = Nothing
            MessageBox.Show("Data berhasil diupload ke SAP")

        Catch ex As Exception
            Dim errMess As String = ex.Message
        End Try


        Dim debug = ""
    End Sub

    Protected Sub dgListJV_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgListJV.ItemCommand
        Select Case e.CommandName
            Case "Detail"
                Response.Redirect("FrmBabitJV.aspx?Mode=Detail&BabitReportJVID=" & e.Item.Cells(0).Text)
            Case "Edit"
                Response.Redirect("FrmBabitJV.aspx?Mode=Edit&BabitReportJVID=" & e.Item.Cells(0).Text)
        End Select
    End Sub

    Private Sub dgListJV_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgListJV.PageIndexChanged
        dgListJV.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
        StoreCriteria()
    End Sub

    Private Sub dgListJV_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgListJV.SortCommand
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
        dgListJV.CurrentPageIndex = 0
        BindGrid(dgListJV.CurrentPageIndex)
        StoreCriteria()
    End Sub
End Class