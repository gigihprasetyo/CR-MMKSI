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

Public Class FrmBabitReportReceiptList
    Inherits System.Web.UI.Page

    Private oDealer As Dealer
    Private sessHelper As New SessionHelper
    Private arlCheckedItemColl As ArrayList = New ArrayList
    Private SessionCriteriaBabit = "FrmBabitReportReceiptList.CriteriaList"
    Private displayPriv As Boolean
    Private editPriv As Boolean
    Private generateJVPriv As Boolean
    Private validasiPriv As Boolean
    Private batalValidasiPriv As Boolean
    Private konfirmasiPriv As Boolean
    Private batalKonfirmasiPriv As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)

        Authorization()
        GetRegNumber(1)
        If Not IsPostBack Then
            PageInit()
            BindGrid(0)
        End If
        lblPopUpDealer.Attributes("onclick") = "ShowPopUpDealer()"

    End Sub

    Protected Sub btnDownloadExcel_Click(sender As Object, e As EventArgs) Handles btnDownloadExcel.Click
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        Dim crits As CriteriaComposite
        If dgListKuitansi.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        If Not IsNothing(sessHelper.GetSession("criteriadownload")) Then
            crits = CType(sessHelper.GetSession("criteriadownload"), CriteriaComposite)
        End If
        ' mengambil data yang dibutuhkan
        arrData = New BabitReportReceiptFacade(User).Retrieve(crits)
        If arrData.Count > 0 Then
            CreateExcel("DaftarKuitansi", arrData)
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
            ws.Cells("E3").Value = "No. Reg Babit"
            ws.Cells("F3").Value = "No. Kuitansi"
            ws.Cells("G3").Value = "No Acc"
            ws.Cells("H3").Value = "No Pengajuan JV"

            For i As Integer = 0 To Data.Count - 1
                Dim oBabitReportReceipt As BabitReportReceipt = Data(i)
                Dim item As BabitHeader = oBabitReportReceipt.BabitReportHeader.BabitHeader
                ws.Cells(i + 4, 1).Value = i + 1
                Select Case oBabitReportReceipt.Status
                    Case 0
                        ws.Cells(i + 4, 2).Value = "Baru"
                    Case 1
                        ws.Cells(i + 4, 2).Value = "Validasi"
                    Case 2
                        ws.Cells(i + 4, 2).Value = "Konfirmasi"
                    Case 3
                        ws.Cells(i + 4, 2).Value = "Proses JV"
                    Case 4
                        ws.Cells(i + 4, 2).Value = "Selesai"
                End Select

                If IsNothing(item.Dealer) Then
                    ws.Cells(i + 4, 3).Value = ""
                    ws.Cells(i + 4, 4).Value = ""
                Else
                    ws.Cells(i + 4, 3).Value = item.Dealer.DealerCode
                    ws.Cells(i + 4, 4).Value = item.Dealer.DealerName
                End If

                ws.Cells(i + 4, 5).Value = item.BabitRegNumber
                ws.Cells(i + 4, 6).Value = oBabitReportReceipt.ReceiptNo
                ws.Cells(i + 4, 7).Value = "Belom ada tabelnya"

                Dim criter As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportJVtoReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criter.opAnd(New Criteria(GetType(BabitReportJVtoReceipt), "BabitReportReceipt.ID", MatchType.Exact, oBabitReportReceipt.ID))
                Dim oBRJVR As ArrayList = New BabitReportJVtoReceiptFacade(User).Retrieve(criter)
                If oBRJVR.Count > 0 Then
                    ws.Cells(i + 4, 8).Value = CType(oBRJVR(0), BabitReportJVtoReceipt).BabitReportJV.NoJV
                Else
                    ws.Cells(i + 4, 8).Value = ""
                End If
                'ws.Cells(i + 4, 8).Value = oBabitReportJVtoReceipt.BabitReportJV.NoJV
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

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindGrid(0)
        StoreCriteria()
        If dgListKuitansi.Items.Count = 0 Then
            MessageBox.Show("Data list tidak ditemukan")
        End If
    End Sub

    Private Function GetCheckedItem() As ArrayList
        Dim arrGrid As ArrayList = CType(sessHelper.GetSession("FrmBabitReportReceiptList"), ArrayList)
        Dim arlCheckedItem As ArrayList = New ArrayList
        Dim nIndeks As Integer
        For Each dtgItem As DataGridItem In dgListKuitansi.Items
            nIndeks = dtgItem.ItemIndex
            Dim objCM As BabitReportReceipt = CType(arrGrid(nIndeks), BabitReportReceipt)
            If CType(dtgItem.Cells(2).FindControl("chkSelect"), CheckBox).Checked Then
                If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    Dim txtNoAcc As TextBox = CType(dtgItem.Cells(9).FindControl("txtNoAcc"), TextBox)
                    Dim lblNoAcc As Label = CType(dtgItem.Cells(9).FindControl("lblNoAcc"), Label)
                    If txtNoAcc.Text <> "" Then
                        objCM.MasterAccrued = New MasterAccruedFacade(User).Retrieve(txtNoAcc.Text)
                    ElseIf lblNoAcc.Text <> "" Then
                        objCM.MasterAccrued = New MasterAccruedFacade(User).Retrieve(lblNoAcc.Text)
                    Else
                        MessageBox.Show("Nomor Accrued belum diinputkan pada kuitansi : " & objCM.ReceiptNo)
                        Return New ArrayList
                    End If
                End If
                arlCheckedItem.Add(objCM)
            End If
        Next

        Return arlCheckedItem
    End Function

    Private Function GetUnCheckedItem() As ArrayList
        Dim arrGrid As ArrayList = CType(sessHelper.GetSession("FrmBabitReportReceiptList"), ArrayList)
        Dim arlCheckedItem As ArrayList = New ArrayList
        Dim nIndeks As Integer
        For Each dtgItem As DataGridItem In dgListKuitansi.Items
            nIndeks = dtgItem.ItemIndex
            Dim objCM As BabitReportReceipt = CType(arrGrid(nIndeks), BabitReportReceipt)
            If Not CType(dtgItem.Cells(2).FindControl("chkSelect"), CheckBox).Checked Then
                arlCheckedItem.Add(objCM)
            End If
        Next

        Return arlCheckedItem
    End Function

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

        If Not IsNothing(CType(sessHelper.GetSession("FrmBabitReportReceiptList"), ArrayList)) Then
            sessHelper.SetSession("_PgIdxBefore", CType(sessHelper.GetSession("_PgIdxNext"), Integer))
            sessHelper.SetSession("_PgIdxNext", pageIndex)

            arlCheckedItemColl = GetCheckedItem()
            Dim currentPage As String = CType(sessHelper.GetSession("_PgIdxBefore"), String)
            sessHelper.SetSession("babitSessReceiptListsProcess" + currentPage, arlCheckedItemColl)

            Dim arlUnCheckedItemColl As ArrayList = GetUnCheckedItem()
            currentPage = CType(sessHelper.GetSession("_PgIdxBefore"), String)
            sessHelper.SetSession("babitSessReceiptListsProcess2" + currentPage, arlUnCheckedItemColl)
        End If

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(BabitReportReceipt), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        'Dim listSource As ArrayList = New BabitReportJVtoReceiptFacade(User).Retrieve(crit, sortColl)
        Dim listSource As ArrayList = New BabitReportReceiptFacade(User).Retrieve(crit, sortColl)
        If listSource.Count <> 0 Then
            CommonFunction.SortListControl(listSource, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(listSource, pageIndex, dgListKuitansi.PageSize)
            sessHelper.SetSession("FrmBabitReportReceiptList", PagedList)
            dgListKuitansi.DataSource = PagedList
            dgListKuitansi.VirtualItemCount = listSource.Count
            dgListKuitansi.DataBind()
        Else
            dgListKuitansi.DataSource = New ArrayList
            dgListKuitansi.VirtualItemCount = 0
            dgListKuitansi.CurrentPageIndex = 0
            dgListKuitansi.DataBind()
        End If
    End Sub

    Private Function SearchCriteria() As CriteriaComposite
        Dim strSql As String = String.Empty
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(BabitReportReceipt), "BabitReportHeader.BabitHeader.BabitStatus", MatchType.Exact, 5))

        'TODO
        If txtNoKuitansi.Text <> "" Then
            'crit.opAnd(New Criteria(GetType(BabitReportJVtoReceipt), "BabitReportReceipt.BabitReportHeader.BabitReportJVtoReceipt.BabitDealerNumber", MatchType.Partial, txtNoKuitansi.Text))
            crit.opAnd(New Criteria(GetType(BabitReportReceipt), "ReceiptNo", MatchType.Partial, txtNoKuitansi.Text))
        End If

        If txtKodeDealer.Text <> "" Then
            crit.opAnd(New Criteria(GetType(BabitReportReceipt), "BabitReportHeader.BabitHeader.Dealer.DealerCode", MatchType.Partial, txtKodeDealer.Text))
        End If

        If Not oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            crit.opAnd(New Criteria(GetType(BabitReportReceipt), "BabitReportHeader.BabitHeader.Dealer.DealerCode", MatchType.Partial, lblKodeDealer.Text.Trim.Split("/")(0).Trim))
        End If

        If ddlBabitType.SelectedIndex <> 0 Then
            crit.opAnd(New Criteria(GetType(BabitReportReceipt), "BabitReportHeader.BabitHeader.BabitMasterEventType.ID", MatchType.Exact, ddlBabitType.SelectedValue))
        End If

        'TODO
        If txtNoRegBabit.Text <> "" Then
            crit.opAnd(New Criteria(GetType(BabitReportReceipt), "BabitReportHeader.BabitHeader.BabitRegNumber", MatchType.Partial, txtNoRegBabit.Text))
        End If

        'TODO
        If ddlStatus.SelectedIndex <> 0 Then
            crit.opAnd(New Criteria(GetType(BabitReportReceipt), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        sessHelper.SetSession("criteriadownload", crit)
        Return crit
    End Function

    Private Sub ReadCriteria()
        Dim crit As Hashtable
        crit = CType(sessHelper.GetSession(SessionCriteriaBabit), Hashtable)
        If Not crit Is Nothing Then
            txtKodeDealer.Text = CStr(crit.Item("DealerCode"))
            lblKodeDealer.Text = CStr(crit.Item("DealerCode"))
            ddlBabitType.SelectedValue = CStr(crit.Item("ddlBabitType"))
            txtNoRegBabit.Text = CStr(crit.Item("txtNoRegBabit"))
            txtNoKuitansi.Text = CStr(crit.Item("txtNoKuitansi"))
            ddlStatus.SelectedValue = CStr(crit.Item("ddlStatus"))

            ViewState("currSortColumn") = CStr(crit.Item("currSortColumn"))
            ViewState("currSortDirection") = CStr(crit.Item("currSortDirection"))
            dgListKuitansi.CurrentPageIndex = CInt(crit.Item("PageIndex"))
        End If
    End Sub

    Private Sub StoreCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("DealerCode", txtKodeDealer.Text)
        crit.Add("ddlBabitType", ddlBabitType.SelectedValue)
        crit.Add("txtNoRegBabit", txtNoRegBabit.Text)
        crit.Add("txtNoKuitansi", txtNoKuitansi.Text)
        crit.Add("ddlStatus", ddlStatus.SelectedValue)

        crit.Add("PageIndex", dgListKuitansi.CurrentPageIndex)
        crit.Add("currSortColumn", ViewState("currSortColumn"))
        crit.Add("currSortDirection", ViewState("currSortDirection"))

        sessHelper.SetSession(SessionCriteriaBabit, crit)  '-- Store in session
    End Sub

    Private Sub PageInit()
        BindDDLs()
        ViewState("currSortColumn") = "ID"
        ViewState("currSortDirection") = Sort.SortDirection.DESC
    End Sub

    Private Sub Authorization()
        If Not SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_BabitKuitansi_Detail_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=BABIT - DAFTAR KUITANSI BABIT")
        Else
            displayPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_BabitKuitansi_Detail_Privilege)
            editPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_BabitKuitansi_Edit_Privilege)
            generateJVPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_BabitKuitansi_GenerateJV_Privilege)
            validasiPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_BabitKuitansi_Validasi_Privilege)
            batalValidasiPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_BabitKuitansi_Batal_Validasi_Privilege)
            konfirmasiPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_BabitKuitansi_Konfirmasi_Privilege)
            batalKonfirmasiPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_BabitKuitansi_Batal_Konfirmasi_Privilege)
        End If

        If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            txtKodeDealer.Visible = True
            lblKodeDealer.Visible = False
            lblPopUpDealer.Visible = True
            btnGenerateJV.Visible = generateJVPriv
        Else
            txtKodeDealer.Text = oDealer.DealerCode
            lblKodeDealer.Text = oDealer.DealerCode & " / " & oDealer.DealerName

            txtKodeDealer.Visible = False
            lblKodeDealer.Visible = True
            lblPopUpDealer.Visible = False
            btnGenerateJV.Visible = False
        End If
    End Sub

    Private Sub BindDDLs()
        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterEventType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlBMET As ArrayList = New BabitMasterEventTypeFacade(User).Retrieve(crits)
        ddlBabitType.Items.Clear()
        With ddlBabitType.Items
            .Add(New ListItem("Silahkan Pilih", "-1", True))
            For Each type As BabitMasterEventType In arlBMET
                If type.FormType < 4 Then
                    .Add(New ListItem(type.TypeName, type.ID))
                End If
            Next
        End With

        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumBabit.BabitReceiptStatus"))
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

        If Not oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            ddlStatus.SelectedValue = -1
        Else
            ddlStatus.SelectedValue = 2
        End If

        ddlAction.Items.Clear()
        ddlAction.Items.Add(New ListItem("Silahkan Pilih", -1))
        With ddlAction.Items
            If Not oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                If validasiPriv Then
                    .Add(New ListItem("Validasi", "0"))
                End If
                If batalValidasiPriv Then
                    .Add(New ListItem("Batal Validasi", "1"))
                End If
            Else
                If konfirmasiPriv Then
                    .Add(New ListItem("Konfirmasi", "2"))
                End If
                If batalKonfirmasiPriv Then
                    .Add(New ListItem("Batal Konfirmasi", "3"))
                End If
            End If
        End With
    End Sub

    Protected Sub btnProses_Click(sender As Object, e As EventArgs) Handles btnProses.Click
        Dim arrChecked As ArrayList = GetCheckedItem()
        If ddlAction.SelectedIndex = 0 Then
            MessageBox.Show("Status belum dipilih")
            Exit Sub
        End If
        If arrChecked.Count = 0 Then
            MessageBox.Show("Tidak ada data kuitansi yang di pilih")
            Exit Sub
        End If
        Dim arrProcessed As ArrayList = New ArrayList
        For Each oBabitReportReceipt As BabitReportReceipt In arrChecked
            Select Case ddlAction.SelectedValue
                Case 0 'Validasi
                    If oBabitReportReceipt.Status <> 0 Then
                        MessageBox.Show("Proses kuitansi hanya untuk Kuitansi status Baru")
                        Exit Sub
                    Else
                        oBabitReportReceipt.Status = 1
                    End If
                Case 1 'Batal Validasi
                    If oBabitReportReceipt.Status <> 1 Then
                        MessageBox.Show("Proses kuitansi hanya untuk Kuitansi status Validasi")
                        Exit Sub
                    Else
                        oBabitReportReceipt.Status = 0
                    End If
                Case 2 'Konfirmasi
                    If oBabitReportReceipt.Status <> 1 Then
                        MessageBox.Show("Proses kuitansi hanya untuk Kuitansi status Validasi")
                        Exit Sub
                    Else
                        oBabitReportReceipt.Status = 2
                    End If
                Case 3 'Batal Konfirmasi
                    If oBabitReportReceipt.Status <> 2 Then
                        MessageBox.Show("Proses kuitansi hanya untuk Kuitansi status Konfirmasi")
                        Exit Sub
                    Else
                        oBabitReportReceipt.Status = 1
                    End If
            End Select
            arrProcessed.Add(oBabitReportReceipt)
        Next
        Dim _result = -1
        For Each item As BabitReportReceipt In arrProcessed
            _result = New BabitReportReceiptFacade(User).Update(item)
        Next
        If _result <> -1 Then
            MessageBox.Show("Proses Update Status Sukses")
        Else
            MessageBox.Show("Proses Update Status Gagal")
        End If
        BindGrid(0)
    End Sub

    Protected Sub btnGenerateJV_Click(sender As Object, e As EventArgs) Handles btnGenerateJV.Click
        Dim arrChecked As ArrayList = GetCheckedItem()
        If arrChecked.Count = 0 Then
            MessageBox.Show("Tidak ada data kuitansi yang di pilih")
            Exit Sub
        End If
        Dim arrBRJV As ArrayList = New ArrayList()
        Dim dealerTemp As Dealer
        Dim dealerCodeTemp As String = ""
        For Each brReceipt As BabitReportReceipt In arrChecked
            If brReceipt.Status <> 2 Then
                'MessageBox.Show("Pengajuan JV hanya untuk Kuitansi status Konfirmasi")
                MessageBox.Show("Generate JV hanya dapat dilakukan untuk status kuitansi konfirmasi")
                Exit Sub
            End If

            If dealerCodeTemp <> "" Then
                If dealerCodeTemp <> brReceipt.BabitReportHeader.Dealer.DealerCode Then
                    MessageBox.Show("Bundling kuitansi untuk pengajuan hanya boleh untuk dealer yang sama")
                    Exit Sub
                End If
            Else
                dealerCodeTemp = brReceipt.BabitReportHeader.Dealer.DealerCode
            End If
            dealerTemp = brReceipt.BabitReportHeader.Dealer

            'arrBRJV.Add(brReceipt)
        Next

        Dim seq As Integer = 1
        Dim arrJV As New ArrayList
        Dim arrJVtoReceipt As New ArrayList
        Dim arrReceipt As New ArrayList
        For Each reportReceipt As BabitReportReceipt In arrChecked
            reportReceipt.Status = 3
            arrReceipt.Add(reportReceipt)

            Dim brJV As BabitReportJV = New BabitReportJV()
            brJV.Dealer = dealerTemp
            brJV.RegNumber = GetRegNumber(seq)
            arrJV.Add(brJV)
            seq += 1

            Dim JVtoReceipt As BabitReportJVtoReceipt = New BabitReportJVtoReceipt()
            JVtoReceipt.BabitReportJV = brJV
            JVtoReceipt.BabitReportReceipt = reportReceipt
            arrJVtoReceipt.Add(JVtoReceipt)
        Next
        Dim _result As Integer = 0
        If arrJVtoReceipt.Count > 0 Then        
            _result = New BabitReportJVtoReceiptFacade(User).InsertTransaction(arrReceipt, arrJV, arrJVtoReceipt)
            If _result = 7 Then
                MessageBox.Show("Pengajuan JV berhasil dibuat")
            Else
                MessageBox.Show("Pengajuan JV gagal dibuat")
            End If
        End If

        BindGrid(0)
    End Sub

    Private Function GetRegNumber(ByVal seq As Integer) As String
        'Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportJV), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportJV), "CreatedTime", MatchType.GreaterOrEqual, Date.Today.Year.ToString))
        crit.opAnd(New Criteria(GetType(BabitReportJV), "CreatedTime", MatchType.Lesser, Date.Today.AddYears(1).Year.ToString))
        Dim arrl As ArrayList = New BabitReportJVFacade(User).Retrieve(crit)
        Dim _return As String
        If arrl.Count > 0 Then
            Dim objBH As BabitReportJV = CommonFunction.SortListControl(arrl, "RegNumber", Sort.SortDirection.DESC)(0)
            Dim noReg As String = objBH.RegNumber
            _return = "JVB" & Date.Today.ToString("yy") & (CInt(noReg.Substring(5, 5)) + seq).ToString("d5")
        Else
            _return = "JVB" & Date.Today.ToString("yy") & CInt(seq).ToString("d5")
        End If
        Return _return
    End Function

    Protected Sub dgListKuitansi_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgListKuitansi.ItemDataBound
        Dim objBR As BabitReportReceipt = New BabitReportReceipt
        Dim chkSelect As CheckBox = CType(e.Item.FindControl("chkSelect"), CheckBox)
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
        Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
        Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
        Dim lblRegBabit As Label = CType(e.Item.FindControl("lblRegBabit"), Label)
        Dim lblReceipt As Label = CType(e.Item.FindControl("lblReceipt"), Label)
        Dim lblNoAcc As Label = CType(e.Item.FindControl("lblNoAcc"), Label)
        Dim lblNoJV As Label = CType(e.Item.FindControl("lblNoJV"), Label)
        Dim lnkbtnDetail As LinkButton = CType(e.Item.FindControl("lnkbtnDetail"), LinkButton)
        Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
        Dim txtNoAcc As TextBox = CType(e.Item.FindControl("txtNoAcc"), TextBox)
        Dim hdnNoAcc As HiddenField = CType(e.Item.FindControl("hdnNoAcc"), HiddenField)
        Dim lblPopUpNoAcc As Label = CType(e.Item.FindControl("lblPopUpNoAcc"), Label)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As BabitReportReceipt = CType(e.Item.DataItem, BabitReportReceipt)
            chkSelect.Checked = False
            Dim currentPage As String = CType(sessHelper.GetSession("_PgIdxNext"), String)
            Dim arrGridDF As ArrayList = CType(sessHelper.GetSession("babitSessReceiptListsProcess" + currentPage), ArrayList)
            If IsNothing(arrGridDF) Then arrGridDF = New ArrayList
            For Each oDF As BabitReportReceipt In arrGridDF
                If objBR.ID = oDF.ID Then
                    chkSelect.Checked = True
                    Exit For
                End If
            Next
            Dim oBH As BabitHeader = oData.BabitReportHeader.BabitHeader
            lblNo.Text = (dgListKuitansi.PageSize * dgListKuitansi.CurrentPageIndex) + e.Item.ItemIndex + 1
            If IsNothing(oData.BabitReportHeader.BabitHeader.Dealer) Then
                lblDealerCode.Text = oData.ID
                lblDealerName.Text = oData.ID
            Else
                lblDealerCode.Text = oBH.Dealer.DealerCode
                lblDealerName.Text = oBH.Dealer.DealerName
            End If

            lblRegBabit.Text = oBH.BabitRegNumber
            lblReceipt.Text = oData.ReceiptNo
            If Not IsNothing(oData.MasterAccrued) Then
                lblNoAcc.Text = oData.MasterAccrued.AccKey
                txtNoAcc.Text = oData.MasterAccrued.AccKey
            Else
                lblNoAcc.Text = ""
                txtNoAcc.Text = ""
            End If
            Dim criter As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportJVtoReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criter.opAnd(New Criteria(GetType(BabitReportJVtoReceipt), "BabitReportReceipt.ID", MatchType.Exact, oData.ID))
            Dim oBRJVR As ArrayList = New BabitReportJVtoReceiptFacade(User).Retrieve(criter)
            If oBRJVR.Count > 0 Then
                lblNoJV.Text = CType(oBRJVR(0), BabitReportJVtoReceipt).BabitReportJV.RegNumber
            Else
                lblNoJV.Text = ""
            End If

            If Not IsNothing(lnkbtnDetail) Then
                lnkbtnDetail.Visible = displayPriv
            End If
            lnkbtnEdit.Visible = False

            Select Case oData.Status
                Case 0
                    'lblStatus.Text = "Baru"
                    If oDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                        lnkbtnEdit.Visible = editPriv
                    End If
                Case 1
                    'lblStatus.Text = "Validasi"
                Case 2
                    'lblStatus.Text = "Konfirmasi"
                Case 3
                    'lblStatus.Text = "Proses JV"
                Case 4
                    'lblStatus.Text = "Selesai"
            End Select
            lblStatus.Text = CommonFunction.GetEnumDescription(oData.Status, "EnumBabit.BabitReceiptStatus")

            If Not oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                txtNoAcc.Visible = False
                lblPopUpNoAcc.Visible = False
                lblNoAcc.Visible = True
            Else
                Select Case oData.Status
                    Case 0, 1, 2
                        lblNoAcc.Visible = False
                        txtNoAcc.Visible = True
                        lblPopUpNoAcc.Visible = True
                    Case 3, 4
                        lblNoAcc.Visible = True
                        txtNoAcc.Visible = False
                        lblPopUpNoAcc.Visible = False
                End Select
            End If
        End If
    End Sub

    Protected Sub dgListKuitansi_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgListKuitansi.ItemCommand
        Dim oBabitReportHeader As BabitReportHeader = New BabitReportHeader
        Dim objBR As New BabitReportReceipt
        If e.Item.ItemType <> ListItemType.Pager AndAlso e.Item.ItemType <> ListItemType.Header Then
            objBR = New BabitReportReceiptFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
            oBabitReportHeader = objBR.BabitReportHeader
        End If
        Select Case e.CommandName
            Case "Detail"
                Response.Redirect("FrmInputBabitReportReceipt.aspx?From=List&Mode=Detail&BabitReportHeaderID=" & oBabitReportHeader.ID)
            Case "Edit"
                Response.Redirect("FrmInputBabitReportReceipt.aspx?From=List&Mode=Edit&BabitReportHeaderID=" & oBabitReportHeader.ID)
        End Select
    End Sub

    Private Sub dgListKuitansi_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgListKuitansi.PageIndexChanged
        dgListKuitansi.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
        StoreCriteria()
    End Sub

    Private Sub dgListKuitansi_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgListKuitansi.SortCommand
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
        dgListKuitansi.CurrentPageIndex = 0
        BindGrid(dgListKuitansi.CurrentPageIndex)
        StoreCriteria()
    End Sub
End Class