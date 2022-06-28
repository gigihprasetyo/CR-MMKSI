Imports System.IO
Imports System
Imports System.Text
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports System.Drawing.Color
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports OfficeOpenXml
Imports ICSharpCode.SharpZipLib.Zip

#End Region



Public Class FrmDaftarBuktiPotongInterest
    Inherits System.Web.UI.Page

    Dim enumStatusPPHInterest As New Dictionary(Of Integer, String)
    Private oDealer As New Dealer
    Private ListPriv As Boolean
    Private InputPriv As Boolean
    Private sessionName As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        oDealer = Session("DEALER")

        If Not IsPostBack Then
            InitPriv()
            Dim pageID As String = Guid.NewGuid().ToString()
            sessionName = pageID.Substring(pageID.Length = 10)
            If Not IsNothing(Request.QueryString("sesID")) AndAlso Request.QueryString("sesID").ToString() <> String.Empty Then
                ViewState("sessionName") = Request.QueryString("sesID").ToString()
                Dim page As Integer = Request.QueryString("page")
                dgDaftarBukti.CurrentPageIndex = page
                bindDataGrid(page)
            Else
                ViewState("sessionName") = sessionName
            End If
            If oDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                txtDealerCode.Text = oDealer.DealerCode
            End If
            setControl()
            bindDdlStatus()
            bindDdlProcess()
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Session("SEARCHCRITERIPPHHEADER" + ViewState("sessionName")) = Nothing
        If oDealer.Title <> EnumDealerTittle.DealerTittle.KTB AndAlso txtDealerCode.Text = String.Empty Then
            MessageBox.Show("Silahkan Masukan Kode Dealer")
            Return
        End If
        dgDaftarBukti.CurrentPageIndex = 0
        bindDataGrid(0)
    End Sub

    Protected Sub dgDaftarBukti_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgDaftarBukti.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            Dim arrData As ArrayList = dgDaftarBukti.DataSource
            Dim obj As InterestPPHHeader = arrData(e.Item.ItemIndex)

            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            Dim lblNoReg As Label = CType(e.Item.FindControl("lblNoReg"), Label)
            Dim lblNoBuktiPotong As Label = CType(e.Item.FindControl("lblNoBuktiPotong"), Label)
            Dim lblPeriodePajak As Label = CType(e.Item.FindControl("lblPeriodePajak"), Label)
            Dim lblTanggalPengajuan As Label = CType(e.Item.FindControl("lblTanggalPengajuan"), Label)
            Dim lblTotalPPH As Label = CType(e.Item.FindControl("lblTotalPPH"), Label)
            Dim lblNPWPPemotong As Label = CType(e.Item.FindControl("lblNPWPPemotong"), Label)
            Dim lblCatatan As Label = CType(e.Item.FindControl("lblCatatan"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnDownloadEv As LinkButton = CType(e.Item.FindControl("lbtnDownloadEv"), LinkButton)
            Dim lbtnDownloadRef As LinkButton = CType(e.Item.FindControl("lbtnDownloadRef"), LinkButton)


            lblID.Text = obj.ID
            lblNo.Text = e.Item.ItemIndex + 1 + (dgDaftarBukti.PageSize * dgDaftarBukti.CurrentPageIndex)
            lblDealerCode.Text = obj.Dealer.DealerCode
            lblNoReg.Text = obj.NoReg
            lblNoBuktiPotong.Text = obj.WitholdingNumber
            lblPeriodePajak.Text = obj.TaxPeriod.ToString("MM/yyyy")
            lblTanggalPengajuan.Text = obj.CreatedTime.ToString("dd/MM/yyyy")
            lblTotalPPH.Text = obj.TotalPPHAmount.ToString("#,###.00")
            lblNPWPPemotong.Text = obj.DealerNPWP
            lblCatatan.Text = obj.Remark
            lblStatus.Text = enumStatusPPHInterest(obj.SubmissionStatus)
            If obj.EvidencePDFPath <> String.Empty Then
                lbtnDownloadEv.Visible = True
                lbtnDownloadEv.ToolTip = "Evidence"
            End If
            If obj.ReferenceDocPath <> String.Empty Then
                lbtnDownloadRef.Visible = True
                lbtnDownloadRef.ToolTip = "Reference"
            End If
            lbtnEdit.Visible = False
            If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER And obj.SubmissionStatus = 0 Then
                lbtnEdit.Visible = True
            End If
            If oDealer.Title = EnumDealerTittle.DealerTittle.KTB And obj.SubmissionStatus = 1 Then
                lbtnEdit.Visible = True
            End If
        End If
    End Sub

    Protected Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        Dim objToSend As ArrayList = collectSelectedData()
        Dim isValidData As Boolean = False
        If objToSend.Count = 0 Then
            MessageBox.Show("Tidak ada data yang dipilih")
            Return
        End If
        Dim strMSgInvalid As String = String.Empty

        enumStatusPPHInterest = Session("ENUMSTATUS")
        If ddlProcess.SelectedValue = 2 Then
            isValidData = validateDataToProcess(objToSend, 1)
            strMSgInvalid = "hanya bisa tolak dari status validasi"
            If isValidData Then
                updateStatus(objToSend, 2)
            End If
        ElseIf ddlProcess.SelectedValue = 3 Then
            isValidData = validateDataToProcess(objToSend, 1)
            strMSgInvalid = "hanya bisa proses ke SAP dari status validasi"
            If isValidData Then     '
                If TransferToSAP(objToSend) Then
                    updateStatus(objToSend, 3)
                End If
            End If
        ElseIf ddlProcess.SelectedValue = -3 Then
            isValidData = validateDataToProcess(objToSend, 1)
            strMSgInvalid = "hanya bisa Batal validasi dari status validasi"
            If isValidData Then     '
                updateStatus(objToSend, 0)
            End If
        ElseIf ddlProcess.SelectedValue = 1 Then
            isValidData = validateDataToProcess(objToSend, 0)
            strMSgInvalid = "hanya bisa valid dari status draft"
            If isValidData Then     '
                updateStatus(objToSend, 1)
            End If
        ElseIf ddlProcess.SelectedValue = 5 Then
            isValidData = validateDataToProcess(objToSend, 2)
            strMSgInvalid = "hanya bisa batal dari status tolak"
            If isValidData Then     '
                updateStatus(objToSend, 5)
            End If
        End If

        If Not isValidData Then
            MessageBox.Show("Periksa kembali status data yang akan diproses!" + vbNewLine + strMSgInvalid)
        Else
            MessageBox.Show(String.Format("{0} data diproses ke status {1}", objToSend.Count, enumStatusPPHInterest(ddlProcess.SelectedValue)))
            bindDataGrid(0)
        End If

    End Sub


#Region "Custom method"

    Private Sub InitPriv()
        ListPriv = SecurityProvider.Authorize(Context.User, SR.SOPPHForm_List_Privilage)
        InputPriv = SecurityProvider.Authorize(Context.User, SR.SOPPhForm_input_Privilage)
        If Not InputPriv Then
        End If

        If Not ListPriv Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Status DO - Daftar Bukti Potong Interest")
        End If
    End Sub

    Private Sub setControl()
        icPeriodStart.Value = New DateTime(Now.Year, Now.Month, 1)
        icPeriodEnd.Value = icPeriodStart.Value.AddMonths(1).AddSeconds(-1)
    End Sub

    Private Sub bindDdlStatus()
        Dim crit As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, 0))
        crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "enumStatusPPHInterest"))
        Dim arrStatus As ArrayList = New StandardCodeFacade(User).Retrieve(crit)

        ddlStatus.Items.Add(New ListItem("Semua", -2))
        For Each s As StandardCode In arrStatus
            ddlStatus.Items.Add(New ListItem(s.ValueDesc, s.ValueId))
            enumStatusPPHInterest(s.ValueId) = s.ValueDesc
        Next
        Session("ENUMSTATUS") = enumStatusPPHInterest
    End Sub

    Private Sub bindDdlProcess()
        oDealer = CType(Session("DEALER"), Dealer)
        Dim enumForMKS As Integer() = {2, 3}
        Dim enumForDealer As Integer() = {1, 5}
        Dim crit As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, 0))
        crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "enumStatusPPHInterest"))
        Dim arrStatus As ArrayList = New StandardCodeFacade(User).Retrieve(crit)
        ddlProcess.Items.Add(New ListItem("Silahkan Pilih", -2))
        Dim i As Integer = 0
        For Each s As StandardCode In arrStatus
            If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                If enumForMKS.Contains(s.ValueId) Then
                    ddlProcess.Items.Add(New ListItem(s.ValueDesc, s.ValueId))
                    If i = 0 Then
                        ddlProcess.Items.Add(New ListItem("Batal Validasi", -3))
                        enumStatusPPHInterest(-3) = "Batal Validasi"
                        i = 2
                    End If

                End If
            Else
                If enumForDealer.Contains(s.ValueId) Then
                    ddlProcess.Items.Add(New ListItem(s.ValueDesc, s.ValueId))
                End If
            End If
            enumStatusPPHInterest(s.ValueId) = s.ValueDesc
        Next
        Session("ENUMSTATUS") = enumStatusPPHInterest
    End Sub

    Private Sub bindDataGrid(ByVal pgIndex As Integer)
        Dim crit As CriteriaComposite
        crit = Session("SEARCHCRITERIPPHHEADER" + ViewState("sessionName"))
        If IsNothing(crit) Then
            crit = getSearchCrit()
            Session("SEARCHCRITERIPPHHEADER" + ViewState("sessionName")) = crit
        Else
            crit = Session("SEARCHCRITERIPPHHEADER" + ViewState("sessionName"))
        End If
        Dim totRow As Integer = 0
        Dim arrData As ArrayList = New InterestPPHHeaderFacade(User).RetrieveActiveList(crit, pgIndex + 1, dgDaftarBukti.PageSize, totRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
        dgDaftarBukti.VirtualItemCount = totRow
        retrieveDetail(arrData)
        enumStatusPPHInterest = Session("ENUMSTATUS")
        Session("ARRDATA" + ViewState("sessionName")) = arrData
        dgDaftarBukti.DataSource = arrData
        dgDaftarBukti.DataBind()
    End Sub

    Private Sub retrieveDetail(ByRef arrData As ArrayList)
        Dim objDetailFacade As New InterestPPHDetailFacade(User)
        For Each obj As InterestPPHHeader In arrData
            obj.InterestPPHDetails = objDetailFacade.RetrieveDetails(obj.ID)
        Next
    End Sub

    Private Function getSearchCrit() As CriteriaComposite
        Dim crit As New CriteriaComposite(New Criteria(GetType(InterestPPHHeader), "RowStatus", MatchType.Exact, 0))
        If Not txtDealerCode.Text.Trim = String.Empty Then
            crit.opAnd(New Criteria(GetType(InterestPPHHeader), "Dealer.DealerCode", MatchType.InSet,  "('" & txtDealerCode.Text.Trim.Replace(";", "','").Trim() & "')"))
        End If

        If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            crit.opAnd(New Criteria(GetType(InterestPPHHeader), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(oDealer, User)))
        End If

        Dim endDate As DateTime = icPeriodEnd.Value.Add(New TimeSpan("23", "59", "59"))
        crit.opAnd(New Criteria(GetType(InterestPPHHeader), "CreatedTime", MatchType.GreaterOrEqual, icPeriodStart.Value))
        crit.opAnd(New Criteria(GetType(InterestPPHHeader), "CreatedTime", MatchType.LesserOrEqual, endDate))
        If Not ddlStatus.SelectedValue = -2 Then
            crit.opAnd(New Criteria(GetType(InterestPPHHeader), "SubmissionStatus", MatchType.Exact, ddlStatus.SelectedValue))
        End If
        If Not txtNoReg.Text.Trim = String.Empty Then
            crit.opAnd(New Criteria(GetType(InterestPPHHeader), "NoReg", MatchType.Partial, txtNoReg.Text.Trim))
        End If

        If Not txtNoBuktiPotong.Text.Trim = String.Empty Then
            crit.opAnd(New Criteria(GetType(InterestPPHHeader), "WitholdingNumber", MatchType.Partial, txtNoBuktiPotong.Text.Trim))
        End If
        Return crit
    End Function

#End Region

    Private Function collectSelectedData()
        Dim arrData As ArrayList = Session("ARRDATA" + ViewState("sessionName"))
        Dim arrResult As New ArrayList()
        Dim cnt As Integer = 0
        Dim chkAll As Boolean = IIf(txtChkAll.Text = "1", True, False)
        If chkAll Then
            Dim crit As CriteriaComposite
            crit = Session("SEARCHCRITERIPPHHEADER" + ViewState("sessionName"))
            If IsNothing(crit) Then
                crit = getSearchCrit()
                Session("SEARCHCRITERIPPHHEADER" + ViewState("sessionName")) = crit
            Else
                crit = Session("SEARCHCRITERIPPHHEADER" + ViewState("sessionName"))
            End If
            Dim totRow As Integer = 0
            arrResult = New InterestPPHHeaderFacade(User).Retrieve(crit)
        Else
            For Each row As DataGridItem In dgDaftarBukti.Items
                Dim cbSelected As CheckBox = row.FindControl("chkAdd")

                If cbSelected.Checked = True Then
                    Dim objToAdd As InterestPPHHeader = CType(arrData(cnt), InterestPPHHeader)
                    arrResult.Add(objToAdd)
                End If
                cnt = cnt + 1
            Next
        End If
        Return arrResult
    End Function

    Private Sub updateStatus(ByVal arrData As ArrayList, ByVal status As Short)
        Dim SOInterestFacade As New SalesOrderInterestFacade(User)
        Dim InterestPPHHeaderFacade As New InterestPPHHeaderFacade(User)
        For Each dt As InterestPPHHeader In arrData
            Dim idToEdit As Integer = dt.ID
            Dim objToEdit As InterestPPHHeader = New InterestPPHHeaderFacade(User).Retrieve(idToEdit)
            Dim arrDetails As ArrayList = New InterestPPHDetailFacade(User).RetrieveDetails(idToEdit)
            Dim arrSOInterest As New ArrayList()

            objToEdit.SubmissionStatus = status
            InterestPPHHeaderFacade.Update(objToEdit)
            For Each dtl As InterestPPHDetail In arrDetails
                Dim tempSOInterest As SalesOrderInterest = SOInterestFacade.Retrieve(dtl.SalesOrderInterest.ID)
                tempSOInterest.Status = IIf(status = 5, -1, status)
                tempSOInterest.InterestPPHHeader = Nothing
                Dim res = SOInterestFacade.Update(tempSOInterest)
            Next

        Next
    End Sub

    Private Function TransferToSAP(ByVal arrDataToSend As ArrayList) As Boolean
        Dim NewArl As ArrayList = New ArrayList
        Dim sb As StringBuilder = New StringBuilder
        Dim filename = String.Format("{0}{1}{2}{3}", "PPHInterest", Date.Now.ToString("ddMMyyyy_HHmmss"), "_" & Strings.Left(Guid.NewGuid().ToString().Substring(10), 4), ".txt")
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder").ToString & "\FinishUnit\PPHOnline\" & filename
        Dim HistoryFolderSAP As String = String.Empty
        HistoryFolderSAP = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder").ToString & "\FinishUnit\PPHOnline\History"

        For Each obj As InterestPPHHeader In arrDataToSend
            sb.Append("H;" & obj.NoReg & ";" & CInt(obj.TotalPPHAmount) & ";" & obj.WitholdingDate.ToString("yyyyMMdd") & Chr(13) & Chr(10))
            For Each objD As InterestPPHDetail In obj.InterestPPHDetails
                sb.Append("D;" & obj.NoReg & ";" & objD.SalesOrderInterest.BillingNumber & Chr(13) & Chr(10))
            Next
        Next

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        success = imp.Start()

        If success Then
            Try
                Dim DestFileInfo As New FileInfo(DestFile)
                If Not DestFileInfo.Directory.Exists Then
                    Directory.CreateDirectory(DestFileInfo.DirectoryName)
                End If

                If HistoryFolderSAP <> String.Empty Then
                    Dim directoryHistory As New DirectoryInfo(HistoryFolderSAP)
                    If Not directoryHistory.Exists Then
                        Directory.CreateDirectory(HistoryFolderSAP)
                    End If
                End If

                Dim objFileStream As New FileStream(DestFile, FileMode.Append, FileAccess.Write)
                Dim objStreamWriter As New StreamWriter(objFileStream)
                objStreamWriter.WriteLine(sb)
                objStreamWriter.Close()
                MessageBox.Show("Data berhasil dikirim ke SAP")
                Return True
            Catch ex As Exception

                MessageBox.Show("Gagal kirim file ke SAP.")
            Finally
                imp.StopImpersonate()
                imp = Nothing
            End Try
        Else
            Return False
            MessageBox.Show("Gagal akses file ke SAP.")
        End If



    End Function

    Private Sub rejectData(ByVal arrData As ArrayList)
        For Each dt As InterestPPHHeader In arrData

        Next
    End Sub

    Private Function validateDataToProcess(ByVal arrData As ArrayList, ByVal allowedStatusBfr As Integer) As Boolean
        For Each dt As InterestPPHHeader In arrData
            If dt.SubmissionStatus <> allowedStatusBfr Then
                Return False
            End If
        Next

        Return True
    End Function

    Protected Sub dgDaftarBukti_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgDaftarBukti.ItemCommand
        If e.Item.ItemIndex >= 0 Then
            Dim arrData As ArrayList = Session("ARRDATA" + ViewState("sessionName"))
            Dim obj As InterestPPHHeader = arrData(e.Item.ItemIndex)
            If e.CommandName = "edit" Then
                Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
                Server.Transfer("../PPHOnline/FrmInputBuktiPotong.aspx?mode=edit&id=" & lblID.Text & "&sesID=" & ViewState("sessionName") & "&page=" & dgDaftarBukti.CurrentPageIndex)
            ElseIf e.CommandName = "view" Then
                Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
                Server.Transfer("../PPHOnline/FrmInputBuktiPotong.aspx?mode=view&id=" & lblID.Text & "&sesID=" & ViewState("sessionName") & "&page=" & dgDaftarBukti.CurrentPageIndex)
            ElseIf e.CommandName = "DownloadEv" Then
                Dim filePath As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") + obj.EvidencePDFPath
                Response.Redirect("../Download.aspx?file=" & filePath & "&name=" & Path.GetFileNameWithoutExtension(filePath))
            ElseIf e.CommandName = "DownloadRef" Then
                Dim filePath As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") + obj.ReferenceDocPath
                Response.Redirect("../Download.aspx?file=" & filePath & "&name=" & Path.GetFileNameWithoutExtension(filePath))
            End If
        End If
    End Sub

    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            enumStatusPPHInterest = CType(Session("ENUMSTATUS"), Dictionary(Of Integer, String))
            dgDaftarBukti.DataSource = New InterestPPHHeaderFacade(User).RetrieveActiveList(CType(Session("SEARCHCRITERIPPHHEADER" + ViewState("sessionName")), CriteriaComposite), indexPage + 1, dgDaftarBukti.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dgDaftarBukti.VirtualItemCount = totalRow
            dgDaftarBukti.DataBind()
        End If

    End Sub

    Protected Sub dgDaftarBukti_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgDaftarBukti.PageIndexChanged
        dgDaftarBukti.SelectedIndex = -1
        dgDaftarBukti.CurrentPageIndex = e.NewPageIndex
        bindDataGrid(dgDaftarBukti.CurrentPageIndex)
    End Sub

    Protected Sub dgDaftarBukti_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgDaftarBukti.SortCommand
        If CType(ViewState("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currentSortColumn") = e.SortExpression
            ViewState("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dgDaftarBukti.SelectedIndex = -1
        dgDaftarBukti.CurrentPageIndex = 0
        bindGridSorting(dgDaftarBukti.CurrentPageIndex)
    End Sub

    Private Sub SetDownload()

        Dim arrData As New ArrayList

        If dgDaftarBukti.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        arrData = collectSelectedData()

        If arrData.Count > 0 Then
            Dim strFileName As String = "Daftar Bukti Potong Interest"
            CreateExcel(strFileName, arrData)
        Else
            MessageBox.Show("Tidak ada data yang dipilih")
        End If

    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        enumStatusPPHInterest = CType(Session("ENUMSTATUS"), Dictionary(Of Integer, String))
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)
            Dim ws1 As ExcelWorksheet = CreateSheet(pck, "Details")

            ws.Cells("A3").Value = "No"
            ws.Cells("B3").Value = "Kode Dealer"
            ws.Cells("C3").Value = "No Pengajuan"
            ws.Cells("D3").Value = "No Bukti Potong"
            ws.Cells("E3").Value = "Tahun Pajak"
            ws.Cells("F3").Value = "Masa Pajak"
            ws.Cells("G3").Value = "NPWP Pemotong"
            ws.Cells("H3").Value = "Nama Pemotong"
            ws.Cells("I3").Value = "Kode Objek Pajak"
            ws.Cells("J3").Value = "Jumlah Bruto"
            ws.Cells("K3").Value = "PPH dipotong"
            ws.Cells("L3").Value = "Tanggal Pemotongan"
            ws.Cells("M3").Value = "NAMA DOK REF"

            ws.Cells("N3").Value = "DPP"
            ws.Cells("O3").Value = "PPH"
            ws.Cells("P3").Value = "Status"

            ws.Cells("A3:Q3").Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid
            ws.Cells("A3:Q3").Style.Fill.BackgroundColor.SetColor(Color.LightSeaGreen)
            ws.Cells("A3:Q3").AutoFitColumns()

            'detail
            ws1.Cells("A3").Value = "No"
            ws1.Cells("B3").Value = "Kode Dealer"
            ws1.Cells("C3").Value = "No Pengajuan"
            ws1.Cells("D3").Value = "No Bukti Potong"
            ws1.Cells("E3").Value = "Tahun Pajak"
            ws1.Cells("F3").Value = "Masa Pajak"
            ws1.Cells("G3").Value = "NPWP Pemotong"
            ws1.Cells("H3").Value = "Nama Pemotong"
            ws1.Cells("I3").Value = "Kode Objek Pajak"
            ws1.Cells("J3").Value = "Billing No"
            ws1.Cells("K3").Value = "Tanggal Billing"
            ws1.Cells("L3").Value = "SO No"
            ws1.Cells("M3").Value = "DPP"
            ws1.Cells("N3").Value = "PPH"
            ws1.Cells("O3").Value = "Doc Number"

            ws1.Cells("A3:O3").Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid
            ws1.Cells("A3:O3").Style.Fill.BackgroundColor.SetColor(Color.LightSeaGreen)
            ws1.Cells("A3:O3").AutoFitColumns()
  

            Dim rowStart As Integer = 4
            Dim rowStartDetail As Integer = 4

            For i As Integer = 0 To Data.Count - 1

                Dim itemDetail As InterestPPHHeader = Data(i)

                ws.Cells(String.Format("A{0}", rowStart)).Value = rowStart - 3
                If Not IsNothing(itemDetail.Dealer) Then
                    ws.Cells(String.Format("B{0}", rowStart)).Value = itemDetail.Dealer.DealerCode
                End If
                ws.Cells(String.Format("C{0}", rowStart)).Value = itemDetail.NoReg
                ws.Cells(String.Format("D{0}", rowStart)).Value = itemDetail.WitholdingNumber
                ws.Cells(String.Format("E{0}", rowStart)).Value = itemDetail.TaxPeriod.ToString("yyyy")
                ws.Cells(String.Format("F{0}", rowStart)).Value = itemDetail.TaxPeriod.ToString("MM")
                ws.Cells(String.Format("G{0}", rowStart)).Value = itemDetail.DealerNPWP
                ws.Cells(String.Format("H{0}", rowStart)).Value = itemDetail.DealerTaxName
                ws.Cells(String.Format("I{0}", rowStart)).Value = "24-100-01"
                ws.Cells(String.Format("J{0}", rowStart)).Value = itemDetail.TotalDPPAmount
                ws.Cells(String.Format("J{0}", rowStart)).Style.Numberformat.Format = "#,##0"
                ws.Cells(String.Format("K{0}", rowStart)).Value = itemDetail.TotalPPHAmount
                ws.Cells(String.Format("K{0}", rowStart)).Style.Numberformat.Format = "#,##0"
                ws.Cells(String.Format("L{0}", rowStart)).Value = itemDetail.WitholdingDate
                ws.Cells(String.Format("L{0}", rowStart)).Style.Numberformat.Format = "dd/MM/yyyy"
                ws.Cells(String.Format("M{0}", rowStart)).Value = itemDetail.ReferenceDocName

                'detail
             

                Dim DPP As Double = 0
                Dim pph As Double = 0
                Try
                    For Each dt As InterestPPHDetail In itemDetail.PPHSubmissionDetails
                        If IsNothing(dt.SalesOrderInterest) Then
                            Continue For
                        End If
                        DPP = DPP + dt.SalesOrderInterest.DPPAmount
                        pph = pph + dt.SalesOrderInterest.PPHAmount

                        ws1.Cells(String.Format("A{0}", rowStartDetail)).Value = rowStartDetail - 3
                        If Not IsNothing(itemDetail.Dealer) Then
                            ws1.Cells(String.Format("B{0}", rowStartDetail)).Value = dt.SalesOrderInterest.Dealer.DealerCode
                        End If
                        ws1.Cells(String.Format("C{0}", rowStartDetail)).Value = itemDetail.NoReg
                        ws1.Cells(String.Format("D{0}", rowStartDetail)).Value = itemDetail.WitholdingNumber
                        ws1.Cells(String.Format("E{0}", rowStartDetail)).Value = itemDetail.TaxPeriod.ToString("yyyy")
                        ws1.Cells(String.Format("F{0}", rowStartDetail)).Value = itemDetail.TaxPeriod.ToString("MM")
                        ws1.Cells(String.Format("G{0}", rowStartDetail)).Value = itemDetail.DealerNPWP
                        ws1.Cells(String.Format("H{0}", rowStartDetail)).Value = itemDetail.DealerTaxName
                        ws1.Cells(String.Format("I{0}", rowStartDetail)).Value = "24-100-01"
                        ws1.Cells(String.Format("J{0}", rowStartDetail)).Value = dt.SalesOrderInterest.BillingNumber
                        ws1.Cells(String.Format("K{0}", rowStartDetail)).Value = dt.SalesOrderInterest.BillingDate
                        ws1.Cells(String.Format("K{0}", rowStartDetail)).Style.Numberformat.Format = "dd/MM/yyyy"
                        ws1.Cells(String.Format("L{0}", rowStartDetail)).Value = dt.SalesOrderInterest.SONumber
                        ws1.Cells(String.Format("M{0}", rowStartDetail)).Value = dt.SalesOrderInterest.DPPAmount
                        ws1.Cells(String.Format("N{0}", rowStartDetail)).Value = dt.SalesOrderInterest.PPHAmount
                        '   ws1.Cells(String.Format("J{0}", rowStartDetail)).Style.Numberformat.Format = "#,##0"

                        ws1.Cells(String.Format("M{0}", rowStartDetail)).Style.Numberformat.Format = "#,##0"
                        ws1.Cells(String.Format("N{0}", rowStartDetail)).Style.Numberformat.Format = "#,##0"
                        ws1.Cells(String.Format("O{0}", rowStartDetail)).Value = dt.SalesOrderInterest.DocNumber
                         
                      
                        rowStartDetail = rowStartDetail + 1
                    Next
                Catch ex As Exception

                End Try


                'End Detail
              
             

                ws.Cells(String.Format("N{0}", rowStart)).Value = DPP
                ws.Cells(String.Format("N{0}", rowStart)).Style.Numberformat.Format = "#,##0"

                ws.Cells(String.Format("O{0}", rowStart)).Value = pph
                ws.Cells(String.Format("O{0}", rowStart)).Style.Numberformat.Format = "#,##0"

                ws.Cells(String.Format("P{0}", rowStart)).Value = enumStatusPPHInterest(itemDetail.SubmissionStatus)
                rowStart += 1
            Next



            CreateExcelFile(pck, FileName & "_" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xlsx")
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
        'Response.AppendHeader("Content-Disposition", String.Format("attachment; filename=""{0}""; size={1}; creation-date={2}; modification-date={2}; read-date={2}", fileName, fileBytes.Length, DateTime.Now.ToString("R")))
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName)
        Response.ContentType = "application/vnd.ms-excel"
        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()

    End Sub

    Protected Sub btnDownlaod_Click(sender As Object, e As EventArgs) Handles btnDownlaod.Click
        SetDownload()
    End Sub

    Private Sub createZip(ByVal DirectoryPath As String, ByVal OutputFilePath As String, ByVal id As String, Optional ByVal CompressionLevel As Integer = 9)
        Try
            Dim filenames As String() = Directory.GetFiles(DirectoryPath)

            Using OutputStream As ZipOutputStream = New ZipOutputStream(File.Create(OutputFilePath))
                OutputStream.SetLevel(CompressionLevel)
                Dim buffer As Byte() = New Byte(4095) {}

                For Each file As String In filenames
                    If 1 = 1 Then

                        Dim entry As ZipEntry = New ZipEntry(Path.GetFileName(file))
                        entry.DateTime = DateTime.Now
                        OutputStream.PutNextEntry(entry)

                        Using fs As FileStream = IO.File.OpenRead(file)
                            Dim sourceBytes As Integer

                            Do
                                sourceBytes = fs.Read(buffer, 0, buffer.Length)
                                OutputStream.Write(buffer, 0, sourceBytes)
                            Loop While sourceBytes > 0
                        End Using
                    End If
                Next

                OutputStream.Finish()
                OutputStream.Close()
            End Using

        Catch ex As Exception
            MessageBox.Show(String.Format("Berkas gagal di kompres : {0}", ex.Message))
        End Try
    End Sub

    Private Sub copyFilesFromServer(ByVal arrData As ArrayList)
        Dim uniqueID As String = DateTime.Now.ToString("yyyyMMddHHmmss") + Guid.NewGuid().ToString().Substring(0, 4)
        Dim localFolder As String = "..\DataTemp\PPHOnline\"
        If Not System.IO.Directory.Exists(Server.MapPath(localFolder + uniqueID + "\")) Then
            System.IO.Directory.CreateDirectory(Server.MapPath(localFolder + uniqueID + "\"))
        End If
        Dim sapFolder As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder")
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Try
            Dim ok As Boolean = imp.Start()
            If ok Then
                For Each obj As InterestPPHHeader In arrData
                    If obj.EvidencePDFPath <> "" AndAlso obj.ReferenceDocPath <> "" Then
                        Dim finfoEv As New FileInfo(sapFolder + obj.EvidencePDFPath)
                        Dim finfoRef As New FileInfo(sapFolder + obj.ReferenceDocPath)
                        finfoEv.CopyTo(Server.MapPath(localFolder) + uniqueID + "\" + String.Format("{0}_{1}_{2}_EBukPot{3}", obj.Dealer.DealerCode, obj.NoReg, obj.WitholdingNumber, Path.GetExtension(obj.EvidencePDFPath)), True)
                        finfoRef.CopyTo(Server.MapPath(localFolder) + uniqueID + "\" + String.Format("{0}_{1}_{2}_Reference{3}", obj.Dealer.DealerCode, obj.NoReg, obj.WitholdingNumber, Path.GetExtension(obj.ReferenceDocPath)), True)
                    End If
                  
                    'finfoRef.CopyTo(Server.MapPath(localFolder) + uniqueID + "\" + Path.GetFileName(obj.ReferenceDocPath), True)
                Next
            End If
            imp.StopImpersonate()

            createZip(Server.MapPath(localFolder) + uniqueID + "\", Server.MapPath(localFolder) + uniqueID + "\" + uniqueID + "list_evidence.zip", uniqueID)
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\PPHOnline\" & uniqueID + "\" + uniqueID + "list_evidence.zip")
        Catch ex As Exception
            imp.StopImpersonate()
        End Try
        imp = Nothing
    End Sub

    Protected Sub btnDownloadMultipleAttachment_Click(sender As Object, e As EventArgs) Handles btnDownloadMultipleAttachment.Click
        Dim arrData As New ArrayList

        If dgDaftarBukti.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        arrData = collectSelectedData()

        If arrData.Count > 0 Then
            Dim strFileName As String = "Daftar Bukti Potong Interest"
            copyFilesFromServer(arrData)
        Else
            MessageBox.Show("Tidak ada data yang dipilih")
        End If
    End Sub
End Class