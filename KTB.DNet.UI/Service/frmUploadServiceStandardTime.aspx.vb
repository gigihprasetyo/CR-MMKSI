#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.AfterSales
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.WebCC
Imports MMKSI.DNetUpload.Utility
Imports System.Collections.Generic
Imports OfficeOpenXml
Imports GlobalExtensions
Imports KTB.DNet.BusinessValidation

#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Reflection

#End Region

Public Class frmUploadServiceStandardTime
    Inherits System.Web.UI.Page
#Region " Private fields "
    Private sessHelper As SessionHelper = New SessionHelper
    Private crit As CriteriaComposite
    Private objDealer As New Dealer
    Private oLoginUser As New UserInfo
    Private m_bInputPrivilege As Boolean = False
    Private stdFacade As StandardCodeFacade = New StandardCodeFacade(User)
    Private vechileModelFacade As VechileModelFacade = New VechileModelFacade(User)
    Private fSKindFacade As FSKindFacade = New FSKindFacade(User)
    Private pMKindFacade As PMKindFacade = New PMKindFacade(User)
    Private recallCategoryFacade As RecallCategoryFacade = New RecallCategoryFacade(User)
    Private serviceBookingFacade As ServiceBookingFacade = New ServiceBookingFacade(User)
    Private freeServiceFacade As FreeServiceFacade = New FreeServiceFacade(User)
    Private recallServiceFacade As RecallServiceFacade = New RecallServiceFacade(User)
    Private pmHeaderFacade As PMHeaderFacade = New PMHeaderFacade(User)
    Private vechileTypeFacade As VechileTypeFacade = New VechileTypeFacade(User)
    Private chassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
    Private dealerFacade As DealerFacade = New DealerFacade(User)
    Private stallMasterFacade As StallMasterFacade = New StallMasterFacade(User)
    Private assistServiceTypeFacade As AssistServiceTypeFacade = New AssistServiceTypeFacade(User)
    Dim isDealerDMS As Boolean = False
    Private isDealerPiloting As Boolean = False
    Private _arlServStdTime As ArrayList = New ArrayList
    Private arrSST As ArrayList = New ArrayList
    Private objSrvST As New ServiceStandardTime
#End Region

#Region "Event Handler"

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Server.ScriptTimeout = 300
        CheckPrivilege()
        If Not IsPostBack Then
            ResetControl()
            If Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                lblDealerCode.Text = objDealer.DealerCode
            End If
        End If
    End Sub
    Protected Sub LnkDownloadTemplate_Click(sender As Object, e As EventArgs) Handles LnkDownloadTemplate.Click
        If (ddlAssistServiceType.SelectedIndex <> 0 And ddlJenisKegiatan.SelectedIndex <> 0) Then
            ReadData()
            Dim arrServiceStandard As ArrayList = CType(sessHelper.GetSession("ServiceStandardTimeList"), ArrayList)
            'Dim aStatus As New ArrayList
            If arrServiceStandard.Count <> 0 Then
                '   DoDownload(arrStallMaster)
                SetDownload()
            Else
                'MessageBox.Show("Belum ada data untuk pencarian ini.")
                SetDownload()

            End If
        Else
            MessageBox.Show("Pilih Dahulu Assist Service Type Dan Jenis Kegiatan Yang Akan Di Download.")
        End If
    End Sub
    'Protected Sub LnkDownloadTemplate_Click(sender As Object, e As EventArgs) Handles LnkDownloadTemplate.Click
    '    'Dim strName As String = KTB.DNet.Lib.WebConfig.GetValue("FILE_STANDARD_SERVICE_TIME") '"CUSTOMER_AWAL.xls"
    '    'Response.Redirect("../downloadlocal.aspx?file=" & strName)
    '    'objs
    '    'Dim obj As TrClass = New TrClassFacade(User).Retrieve(hdnClassCode.Value)
    '    'Dim tipeNilai As String = String.Empty

    '    Using excelPackage As New ExcelPackage()
    '        Dim wsData As ExcelWorksheet = excelPackage.Workbook.Worksheets.Add("Service Standard Time")
    '        Dim rowIdx As Integer = 1
    '        wsData.Cells("A" & rowIdx.ToString()).ValueBold("No")
    '        rowIdx += 1
    '        rowIdx += 1
    '        wsData.Cells("B" & rowIdx.ToString()).ValueBold("Assist Service Type")
    '        wsData.Cells("C" & rowIdx.ToString()).ValueBold("Model Kendaraan")

    '        wsData.Cells("D" & rowIdx.ToString()).ValueBold("Tipe Kendaraan")
    '        wsData.Cells("E" & rowIdx.ToString()).ValueBold("Jenis Kegiatan")
    '        rowIdx += 1
    '        wsData.Cells("B" & rowIdx.ToString()).ValueBold("Mulai ")
    '        wsData.Cells("C" & rowIdx.ToString()).ValueBold(dataKelas.StartDate.DateToString())
    '        wsData.Cells("E" & rowIdx.ToString()).ValueBold("Selesai ")
    '        wsData.Cells("F" & rowIdx.ToString()).ValueBold(dataKelas.FinishDate.DateToString())
    '        rowIdx += 1

    '        Dim dtgInputNilai As DataGrid = Nothing
    '        Select Case hdnJenisNilai.Value
    '            Case "0"
    '                tipeNilai = "Angka"
    '                dtgInputNilai = dtgClassRegistration
    '            Case "1"
    '                tipeNilai = "Sikap"
    '                dtgInputNilai = dtgClassRegistration2
    '        End Select

    '        Dim listDisabled As New List(Of String)
    '        For Each dtItem As DataGridItem In dtgInputNilai.Items
    '            If dtItem.IsRowItems Then
    '                For idx As Integer = 1 To 9
    '                    Dim txtTest As TextBox = dtItem.FindTextBox("txtTest" + idx.ToString)
    '                    If Not txtTest.Visible Then
    '                        listDisabled.Add("Test " + idx.ToString)
    '                        listDisabled.Add("Nilai Sikap " + idx.ToString)
    '                    End If
    '                Next
    '                Exit For
    '            End If
    '        Next
    '        rowIdx += 1
    '        Dim columnIdx As Integer = 1

    '        For Each dtColumn As DataGridColumn In dtgInputNilai.Columns
    '            If dtColumn.Visible And listDisabled.IndexOf(dtColumn.HeaderText) <= -1 And dtColumn.HeaderText.NotNullorEmpty Then
    '                If dtColumn.HeaderText.StartsWith("Test") And dtColumn.HeaderText.IndexOf("A") = -1 Then
    '                    Dim evaluationCode As String = dataKelas.TrCourse.CourseCode.ToUpper() + "-A" + CInt(dtColumn.HeaderText.Replace("Test ", "")).GenerateIncrement(2)
    '                    Dim crit As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '                    crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, dataKelas.TrCourse.ID))
    '                    crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "EvaluationCode", MatchType.Exact, evaluationCode))
    '                    Dim dataEva As TrCourseEvaluation = CType(New TrCourseEvaluationFacade(User).Retrieve(crit)(0), TrCourseEvaluation)
    '                    wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue(dataEva.Name)
    '                ElseIf dtColumn.HeaderText.StartsWith("Nilai") Then
    '                    Dim evaluationCode As String = dataKelas.TrCourse.CourseCode.ToUpper() + "-B" + CInt(dtColumn.HeaderText.Replace("Nilai Sikap ", "")).GenerateIncrement(2)
    '                    Dim crit As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '                    crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, dataKelas.TrCourse.ID))
    '                    crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "EvaluationCode", MatchType.Exact, evaluationCode))
    '                    Dim dataEva As TrCourseEvaluation = CType(New TrCourseEvaluationFacade(User).Retrieve(crit)(0), TrCourseEvaluation)
    '                    wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue(dataEva.Name)
    '                Else
    '                    If tipeNilai.Equals("Angka") Then
    '                        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue(dtColumn.HeaderText.Replace("Catatan", "Status Lulus"))
    '                    Else
    '                        If dtColumn.HeaderText.IndexOf("Catatan") = -1 Then
    '                            wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue(dtColumn.HeaderText)
    '                        End If
    '                    End If

    '                End If
    '                columnIdx += 1
    '            End If
    '        Next
    '        rowIdx += 1
    '        For Each dtItem As DataGridItem In dtgInputNilai.Items
    '            If dtItem.IsRowItems Then


    '                Dim clmIdx As Integer = 1
    '                Dim lblNo As Label = dtItem.FindLabel("lblNo")
    '                wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(lblNo.Text, Style.ExcelHorizontalAlignment.Center)
    '                clmIdx += 1

    '                Dim lblSalesmanCode As Label = dtItem.FindLabel("lblSalesmanCode")
    '                Dim lblRegCode As Label = dtItem.FindLabel("lblTrId")

    '                If AreaId.IsNullorEmpty Then
    '                    wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(lblRegCode.Text, Style.ExcelHorizontalAlignment.Left)
    '                Else
    '                    If AreaId.Equals("1") Or AreaId.Equals("3") Then
    '                        wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(lblSalesmanCode.Text, Style.ExcelHorizontalAlignment.Left)
    '                    Else
    '                        wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(lblRegCode.Text, Style.ExcelHorizontalAlignment.Left)
    '                    End If
    '                End If
    '                clmIdx += 1

    '                Dim lblTraineeName As Label = dtItem.FindLabel("lblTraineeName")
    '                wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(lblTraineeName.Text, Style.ExcelHorizontalAlignment.Left)
    '                clmIdx += 1

    '                Dim lblDealerName As Label = dtItem.FindLabel("lblKodeOrg")
    '                wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(lblDealerName.Text, Style.ExcelHorizontalAlignment.Left)
    '                clmIdx += 1

    '                If tipeNilai.Equals("Angka") Then
    '                    Dim TxtTestAwal As TextBox = dtItem.FindTextBox("TxtTestAwal")
    '                    wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(TxtTestAwal.Text, Style.ExcelHorizontalAlignment.Center)
    '                    clmIdx += 1
    '                End If

    '                For idx As Integer = 1 To 9
    '                    Dim txtInput As TextBox = dtItem.FindTextBox("txtTest" + idx.ToString)
    '                    If txtInput.Visible Then
    '                        wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(txtInput.Text, Style.ExcelHorizontalAlignment.Center)
    '                        clmIdx += 1
    '                    End If
    '                Next

    '                If tipeNilai.Equals("Angka") Then
    '                    Dim TxtTestAkhir As TextBox = dtItem.FindTextBox("TxtTestAkhir")
    '                    wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(TxtTestAkhir.Text, Style.ExcelHorizontalAlignment.Center)
    '                    clmIdx += 1

    '                    Dim chkPass As CheckBox = dtItem.FindCheckBox("chkPass")
    '                    If chkPass.Checked Then
    '                        wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue("Ya", Style.ExcelHorizontalAlignment.Center)
    '                    End If
    '                    clmIdx += 1
    '                End If
    '                If dtItem.BackColor = OrangeRed Then
    '                    For nCol As Integer = 1 To clmIdx - 1
    '                        Dim nCells As ExcelRange = wsData.Cells(nCol.ExcelColumnName & rowIdx.ToString)
    '                        nCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid
    '                        nCells.Style.Fill.BackgroundColor.SetColor(Color.OrangeRed)
    '                    Next
    '                End If
    '                rowIdx += 1
    '            End If
    '        Next
    '        For colIdx As Integer = 2 To columnIdx - 1
    '            wsData.Column(colIdx).AutoFit()
    '        Next

    '        Dim fileBytes = excelPackage.GetAsByteArray()

    '        Dim fileName As String = String.Format("InputDataNilai_{0}_{1}.xls", tipeNilai, dataKelas.ClassCode)

    '        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
    '        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
    '        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
    '        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

    '        Dim success As Boolean = False

    '        Try
    '            success = imp.Start()
    '            If success Then
    '                File.WriteAllBytes(Server.MapPath("~/DataTemp/" & fileName), fileBytes)
    '                imp.StopImpersonate()
    '            End If

    '        Catch ex As Exception
    '            Exit Sub

    '        End Try

    '        Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & fileName)
    '    End Using
    'End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)

            If (Not DataFile.PostedFile Is Nothing) And (DataFile.PostedFile.ContentLength > 0) Then
                'cek maxFileSize first
                Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))
                'Start  :Donas,20140603:failed to upload by dealer (specific)
            'If 1 = 2 AndAlso DataFile.PostedFile.ContentType.ToString <> "application/vnd.ms-excel" And _
            '     DataFile.PostedFile.ContentType.ToString <> "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" Then
            If DataFile.PostedFile.ContentType.ToString <> "application/vnd.ms-excel" And _
             DataFile.PostedFile.ContentType.ToString <> "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" Then
                'DataFile.PostedFile.ContentType.ToString <> "application/octet-stream" Then

                'If Not (DataFile.PostedFile.ContentType.ToString = "application/vnd.ms-excel" Or _
                '            DataFile.PostedFile.ContentType.ToString = "application/octet-stream" Or _
                '            DataFile.PostedFile.ContentType.ToString = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" Or _
                '            DataFile.PostedFile.ContentType.ToString = "application/x-zip-compressed") Then
                MessageBox.Show("Extension file tidak sesuai. Ubah ke *.xlsx (excel 2007).")
                Exit Sub
            End If
            'Start  :Donas,20140603:failed to upload by dealer (specific)
            'remark by anh, interop dah pake 2007
            'Dim path As System.IO.Path
            'Dim ext As String = path.GetExtension(DataFile.PostedFile.FileName).Trim.ToLower

            'If ext <> ".xls" Then
            '    MessageBox.Show("Extension file " & DataFile.PostedFile.FileName & " tidak sesuai. Mohon convert file ke *.xls (excel 2003) terlebih dahulu.")
            '    Exit Sub
            'End If
            'End    : Donas,20140603:failed to upload by dealer (specific)
            If DataFile.PostedFile.ContentLength > maxFileSize Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                Exit Sub
            End If

            Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName)   '-- Source file name
            SrcFile = New Date().Now.ToString("yyyyMMddhhmmss") & SrcFile
            'Dim DestFile As String = Server.MapPath("") & "\..\DataFile\" & SrcFile  '-- Destination file
            Dim TempFile As String = Server.MapPath("") & "\..\DataTemp\" & SrcFile  '-- Temporary file

            'Todo session
            'Session.Add("DestFile", DestFile)  '-- Store Destination file path into session
            'Todo session
            'Session.Add("TempFile", TempFile)  '-- Store Temporary file path into session


            '-- Impersonation to manipulate file in server
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

            Try
                If imp.Start() Then

                    '-- Copy data file from client to server temporary folder
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(DataFile.PostedFile.InputStream, TempFile)

                    imp.StopImpersonate()
                    imp = Nothing

                    '-- Declare & instantiate parser
                    Dim parser As UploadServiceStdTimeParser = New UploadServiceStdTimeParser(objDealer, DataFile.PostedFile.ContentType.ToString)

                    '-- Parse data file and store result into arraylist
                    'Dim arlCustomer As ArrayList = CType(parser.ParseExcelNoTransaction(TempFile, "[Sheet1$]", "User"), ArrayList)
                    Dim arlCustomer As ArrayList = CType(parser.ParseExcelNoTransaction(TempFile, "[Sheet1$A1:S152]", "User"), ArrayList)
                    If arlCustomer.Count > 0 Then
                        If parser.IsAllDataValid Then
                            btnSimpan.Enabled = True
                        Else
                            btnSimpan.Enabled = False
                        End If
                    Else
                        If Not IsNothing(parser.GetErrorMessage()) Then
                            MessageBox.Show(parser.GetErrorMessage())
                        Else
                            MessageBox.Show("Tidak ada data yang diupload dari excel.")
                        End If
                        'MessageBox.Show(parser.GetErrorMessage())

                        btnSimpan.Enabled = False
                    End If

                    sessHelper.SetSession("ServiceStandardTime", arlCustomer)
                    'objSrvST = CType(sessHelper.GetSession("ServiceStandardTime"), ServiceStandardTime) 'sessHelper.SetSession("ServiceStandardTime", arlCustomer)
                    dtgServiceStandardTime.Visible = True
                    dtgServiceStandardTime.DataSource = Nothing  '-- Reset datagrid first
                    dtgServiceStandardTime.CurrentPageIndex = 0
                    BindUploadServiceStandardTime()
                End If

            Catch Exc As Exception
                MessageBox.Show(SR.UploadFail(SrcFile))
            End Try
        Else
            MessageBox.Show(SR.FileNotSelected)
        End If

    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs)
        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Dim objUser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        _arlServStdTime = sessHelper.GetSession("ServiceStandardTime")
        Dim _arlSST As ArrayList = sessHelper.GetSession("ServiceStandardTime")
        Dim objSST As ServiceStandardTime = New ServiceStandardTime
        Dim _nResult As Integer = 0
        Dim test As Integer = 0
        Dim strAssist As String = ""
        'objSST = CType(_arlSST(0), ServiceStandardTime)
        If Not IsNothing(_arlSST) Then
            If _arlSST.Count > 0 Then
                Dim _errMsg As StringBuilder = New StringBuilder
                Dim oFacade As New ServiceStandardTimeFacade(User)
                Dim iSuccess As Integer = 0
                For Each _sst As ServiceStandardTime In _arlSST
                    test = test + 1
                    If _sst.ErrorMessage = String.Empty Then
                        Dim criterias33 As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias33.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.StallServiceType"))
                        criterias33.opAnd(New Criteria(GetType(StandardCode), "ValueDesc", MatchType.Exact, _sst.AssistServiceTypeCode))
                        Dim arrDDLs As ArrayList = New StandardCodeFacade(User).Retrieve(criterias33)
                        Dim objStandardCodes As New StandardCode
                        If Not IsNothing(arrDDLs) AndAlso arrDDLs.Count > 0 Then
                            objStandardCodes = CType(arrDDLs(0), StandardCode)
                            _sst.AssistServiceTypeCode = objStandardCodes.ValueCode
                        End If

                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ServiceStandardTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ServiceStandardTime), "Dealer.DealerCode", MatchType.Exact, lblDealerCode.Text))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ServiceStandardTime), "AssistServiceTypeCode", MatchType.Exact, _sst.AssistServiceTypeCode))
                        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ServiceStandardTime), "VechileModel.ID", MatchType.Exact, _sst.VechileModel.ID))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ServiceStandardTime), "VechileType.ID", MatchType.Exact, _sst.VechileType.ID))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ServiceStandardTime), "ServiceTypeID", MatchType.Exact, _sst.ServiceTypeID))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ServiceStandardTime), "KindCode", MatchType.Exact, _sst.KindCode))


                        Dim arlSST As ArrayList = oFacade.Retrieve(criterias)
                        If arlSST.Count > 0 Then
                            objSST = CType(arlSST(0), ServiceStandardTime)
                            _sst.ID = objSST.ID
                            _nResult = oFacade.Update(_sst)

                            'Return nResult
                        Else
                            _sst.Dealer = objDealer
                            _sst.VechileModel = _sst.VechileType.VechileModel
                            '_sst.AssistServiceTypeCode = _sst.AssistServiceTypeCode + test.ToString()
                            _nResult = oFacade.Insert(_sst)
                            'If _nResult = -1 Then
                            '    MessageBox.Show(SR.SaveFail)
                            'Else
                            '    MessageBox.Show(SR.SaveSuccess)
                            'End If
                        End If
                    Else
                        MessageBox.Show(test.ToString())
                    End If
                Next

                If _nResult = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show(SR.SaveSuccess)
                    'ViewState.Add("vsProcess", "Add")
                End If

            End If


            Page_Load(Nothing, EventArgs.Empty)
            dtgServiceStandardTime.Visible = False
            btnSimpan.Enabled = False
        Else
            MessageBox.Show("Lakukan Upload Excel sebelum Simpan.")
        End If


    End Sub

    Private Sub dtgServiceStandardTime_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgServiceStandardTime.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Try
                'Dim objContact As CcContact = e.Item.DataItem

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = (dtgServiceStandardTime.CurrentPageIndex * dtgServiceStandardTime.PageSize + e.Item.ItemIndex + 1).ToString

                Dim lbJenisKegiatan As Label = CType(e.Item.FindControl("lbJenisKegiatan"), Label)

                Dim lbJenisService As Label = CType(e.Item.FindControl("lbJenisService"), Label)
                Dim lbAssistServiceType As Label = e.Item.FindControl("lbAssistServiceType")


                If lbAssistServiceType.Text = "SB" Then
                    lbAssistServiceType.Text = "Regular"
                End If
                Select Case lbJenisKegiatan.Text
                    Case 1
                        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias2.opAnd(New Criteria(GetType(FSKind), "KindCode", MatchType.Exact, lbJenisService.Text))
                        Dim arrFSK2 As ArrayList = New FSKindFacade(User).Retrieve(criterias2)
                        Dim objFSKind As New FSKind
                        If Not IsNothing(arrFSK2) AndAlso arrFSK2.Count > 0 Then
                            objFSKind = CType(arrFSK2(0), FSKind)
                            lbJenisService.Text = objFSKind.KindCode & " - " & objFSKind.KindDescription
                        End If

                    Case 2
                        Dim criterias0 As New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias0.opAnd(New Criteria(GetType(PMKind), "KindCode", MatchType.Exact, lbJenisService.Text))
                        Dim arrFSK As ArrayList = New PMKindFacade(User).Retrieve(criterias0)
                        Dim objPMKind As New PMKind
                        If Not IsNothing(arrFSK) AndAlso arrFSK.Count > 0 Then
                            objPMKind = CType(arrFSK(0), PMKind)
                            lbJenisService.Text = objPMKind.KindCode & " - " & objPMKind.KindDescription
                        End If
                    Case 3
                        Dim criteriasa As New CriteriaComposite(New Criteria(GetType(RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteriasa.opAnd(New Criteria(GetType(RecallCategory), "RecallRegNo", MatchType.Exact, lbJenisService.Text))
                        Dim arrRC As ArrayList = New RecallCategoryFacade(User).Retrieve(criteriasa)
                        Dim objRecallCategory As New RecallCategory
                        If Not IsNothing(arrRC) AndAlso arrRC.Count > 0 Then
                            objRecallCategory = CType(arrRC(0), RecallCategory)
                            lbJenisService.Text = objRecallCategory.RecallRegNo & " - " & objRecallCategory.Description
                        End If
                    Case 4
                        Dim criterias01 As New CriteriaComposite(New Criteria(GetType(GRKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias01.opAnd(New Criteria(GetType(GRKind), "KindCode", MatchType.Exact, lbJenisService.Text))
                        Dim arrFSK As ArrayList = New GRKindFacade(User).Retrieve(criterias01)
                        Dim objPMKind As New GRKind
                        If Not IsNothing(arrFSK) AndAlso arrFSK.Count > 0 Then
                            objPMKind = CType(arrFSK(0), GRKind)
                            lbJenisService.Text = objPMKind.KindCode & " - " & objPMKind.KindDescription
                        End If
                End Select

                Dim criterias3 As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias3.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.ServiceType"))
                criterias3.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, lbJenisKegiatan.Text))
                Dim arrDDL As ArrayList = New StandardCodeFacade(User).Retrieve(criterias3)
                Dim objStandardCode As New StandardCode
                If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
                    objStandardCode = CType(arrDDL(0), StandardCode)
                    lbJenisKegiatan.Text = objStandardCode.ValueDesc
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub dtgServiceStandardTime_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgServiceStandardTime.PageIndexChanged
        '-- Change datagrid page

        dtgServiceStandardTime.CurrentPageIndex = e.NewPageIndex
        BindPage(e.NewPageIndex)

    End Sub

    

#End Region

#Region "Custom Method"

    Private Sub ReadData()
        '-- Read all data selected
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ServiceStandardTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Dim objUser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If Not objDealer Is Nothing Then
            If Not objDealer.DealerGroup Is Nothing Then
                criterias.opAnd(New Criteria(GetType(ServiceStandardTime), "Dealer.DealerGroup.DealerGroupCode", MatchType.Exact, objDealer.DealerGroup.DealerGroupCode))
            End If

        End If


        
        If ddlJenisKegiatan.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(ServiceStandardTime), "ServiceTypeID", MatchType.Exact, ddlJenisKegiatan.SelectedValue))
        End If

        If ddlAssistServiceType.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(ServiceStandardTime), "AssistServiceTypeCode", MatchType.Exact, ddlAssistServiceType.SelectedValue))
        End If

        
        'criterias.opAnd(New Criteria(GetType(ServiceStandardTime), "ID", MatchType.Exact, "59382"))

        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

        Dim arrServiceStandard As ArrayList = New ServiceStandardTimeFacade(User).Retrieve(criterias)


        '-- Store InvoiceReqList into session for later use
        sessHelper.SetSession("ServiceStandardTimeList", arrServiceStandard)

    End Sub

    Private Sub CheckPrivilege()
        m_bInputPrivilege = SecurityProvider.Authorize(Context.User, SR.ServiceSTD_Input_Privilage)
        isDealerPiloting = TCHelper.GetActiveTCResult(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.PilotingUploadServiceStandardTime))
        If (Not m_bInputPrivilege Or Not isDealerPiloting) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Stall - Upload Service Standard Time")
        End If
    End Sub

    Private Sub InitDdl()
        Dim results As ArrayList

        ddlJenisKegiatan.Items.Clear()

        crit = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.ServiceType"))

        results = stdFacade.Retrieve(crit)

        With ddlJenisKegiatan.Items
            For Each obj As StandardCode In results
                .Add(New ListItem(obj.ValueDesc, obj.ValueId))
            Next
        End With

        ddlJenisKegiatan.Items.Insert(0, "Silahkan Pilih")

        ddlAssistServiceType.Items.Clear()

        'crit = New CriteriaComposite(New Criteria(GetType(AssistServiceType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'crit.opAnd(New Criteria(GetType(AssistServiceType), "ID", MatchType.InSet, "(1,9)"))

        'results = assistServiceTypeFacade.Retrieve(crit)
        'Dim strServiceTypeCode As String = ""
        'With ddlAssistServiceType.Items
        '    For Each obj As AssistServiceType In results
        '        '.Add(New ListItem(obj.ServiceTypeCode, obj.ID))ddl
        '        If (obj.ServiceTypeCode.ToString() = "SB") Then
        '            strServiceTypeCode = "Regular"
        '            .Add(New ListItem(strServiceTypeCode, obj.ID))
        '        Else
        '            .Add(New ListItem(obj.ServiceTypeCode, obj.ID))
        '        End If
        '    Next
        'End With
        crit = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.StallServiceType"))

        results = New StandardCodeFacade(User).Retrieve(crit)
        With ddlAssistServiceType.Items
            For Each obj As StandardCode In results
                .Add(New ListItem(obj.ValueDesc, obj.ValueCode))
                'If (obj.ServiceTypeCode.ToString() = "SB") Then
                '    strServiceTypeCode = "Regular"
                '    .Add(New ListItem(strServiceTypeCode, obj.ID))
                'Else
                '    .Add(New ListItem(obj.ServiceTypeCode, obj.ID))
                'End If

            Next
        End With

        ddlAssistServiceType.Items.Insert(0, "Silahkan Pilih")

    End Sub

    Private Sub ResetControl()
        InitDdl()
        SetControl(True)
    End Sub

    Private Sub SetControl(ByVal opt As Boolean)
        ddlAssistServiceType.Enabled = opt
        ddlJenisKegiatan.Enabled = opt
    End Sub

    Private Sub BindUploadServiceStandardTime()
        Dim totalRow As Integer = 0
        _arlServStdTime = New ArrayList
        Dim _arlValid As ArrayList = New ArrayList
        Dim _arlInValid As ArrayList = New ArrayList

        Try
            _arlServStdTime = sessHelper.GetSession("ServiceStandardTime")

            If Not IsNothing(_arlServStdTime) Then

                'objSrvST = CType(sessHelper.GetSession("ServiceStandardTime"), ServiceStandardTime) 'sessHelper.SetSession("ServiceStandardTime", arlCustomer)


                sessHelper.SetSession("ServiceStandardTime", _arlServStdTime)

                For Each _c As ServiceStandardTime In _arlServStdTime
                    If _c.ErrorMessage <> String.Empty Then
                        btnSimpan.Enabled = False
                        Exit For
                    Else
                        btnSimpan.Enabled = True
                    End If
                Next


                totalRow = _arlServStdTime.Count
                dtgServiceStandardTime.DataSource = _arlServStdTime
                dtgServiceStandardTime.VirtualItemCount = totalRow
                dtgServiceStandardTime.DataBind()
                '           lblMessage.Text = "Jumlah data : " & _arlServStdTime.Count & " ( Valid : " & _arlValid.Count & " ; Tidak Valid : " & _arlInValid.Count & " )"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            lblMessage.Text = ""
        End Try
    End Sub

    Private Sub BindPage(ByVal pageIndex As Integer)
        Dim arrServiceStandard As ArrayList = CType(sessHelper.GetSession("ServiceStandardTime"), ArrayList)
        Dim aStatus As New ArrayList
        If arrServiceStandard.Count <> 0 Then
            ' SortListControl(arrStallMaster, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrServiceStandard, pageIndex, dtgServiceStandardTime.PageSize)
            dtgServiceStandardTime.DataSource = PagedList
            dtgServiceStandardTime.VirtualItemCount = arrServiceStandard.Count()
            dtgServiceStandardTime.DataBind()
        Else
            dtgServiceStandardTime.DataSource = New ArrayList
            dtgServiceStandardTime.VirtualItemCount = 0
            dtgServiceStandardTime.CurrentPageIndex = 0
            dtgServiceStandardTime.DataBind()
        End If
    End Sub

#End Region

#Region "download excel"

    Private Sub SetDownload()
        Dim arrData As New DataTable
        Dim crits As CriteriaComposite
        

        'If Not IsNothing(sessHelp.GetSession("criteriadownload")) Then
        '    crits = CType(sessHelp.GetSession("criteriadownload"), CriteriaComposite)
        'End If
        ' mengambil data yang dibutuhkan
        Dim arrServiceBooking As ArrayList = CType(sessHelper.GetSession("ServiceStandardTimeList"), ArrayList)
        'Dim propertiesinfo As PropertyInfo() = arrServiceBooking(0).GetType().GetProperties()

        'data kosong download headernya saja
        'If arrServiceBooking.Count < 1 Then
        '    MessageBox.Show("Tidak ada data yang di download")
        '    Return
        'End If

        'For Each pf As PropertyInfo In propertiesinfo
        '    Dim dc As DataColumn = New DataColumn(pf.Name)
        '    dc.DataType = pf.PropertyType
        '    arrData.Columns.Add(dc)
        'Next

        'For Each ar As Object In arrFlatRate
        '    Dim dr As DataRow = arrData.NewRow
        '    Dim pf As PropertyInfo() = ar.GetType().GetProperties()

        '    For Each prop As PropertyInfo In pf
        '        dr(prop.Name) = prop.GetValue(ar, Nothing)
        '    Next
        '    arrData.Rows.Add(dr)
        'Next

        'arrData = New VW_FlatRateMasterFacade(User).GetDownLoadExcel(crits.ToString())

        'If arrServiceBooking.Count > 0 Then
        '    CreateExcel("ServiceStandardTime", arrServiceBooking)
        'End If
        'If arrServiceBooking.Count > 0 Then
        CreateExcel("ServiceStandardTime", arrServiceBooking)
        'End If
    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As DataTable)
        Using pck As New ExcelPackage()
            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            'Create Header Column
            ws.Cells("A1").ValueBold(FileName)
            Dim rowIndex As Integer = 3
            Dim ColumnIndex As Integer = 1
            Dim lastColumn As Integer = 0
            ws.Cells(rowIndex, ColumnIndex).ValueBold("No")
            ws.Cells(rowIndex, ColumnIndex).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid
            ws.Cells(rowIndex, ColumnIndex).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray)

            For Each dColumn As DataColumn In Data.Columns
                ColumnIndex += 1
                ws.Cells(rowIndex, ColumnIndex).ValueBold(dColumn.ColumnName)
                ws.Cells(rowIndex, ColumnIndex).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid
                ws.Cells(rowIndex, ColumnIndex).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray)
            Next
            lastColumn = ColumnIndex

            'Create Data
            Dim noUrutan As Integer = 1
            For Each dRow As DataRow In Data.Rows
                rowIndex += 1
                ColumnIndex = 1
                ws.Cells(rowIndex, ColumnIndex).SetValue(noUrutan.ToString())
                For Each dColumn As DataColumn In Data.Columns
                    ColumnIndex += 1
                    ws.Cells(rowIndex, ColumnIndex).SetValue(dRow(dColumn.ColumnName).ToString())
                Next
                noUrutan += 1
            Next
            ws.Cells(3, 2, rowIndex, lastColumn).AutoFilter = True
            For colIdx As Integer = 1 To Data.Columns.Count + 1
                ws.Column(colIdx).AutoFit()
            Next
            CreateExcelFile(pck, FileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xlsx")
        End Using
    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim oD As Dealer
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, "Sheet1")

            'ws.Cells("A1").Value = FileName
            ws.Cells("A1").Value = "No"
            'ws.Cells("B3").Value = "Kode Dealer"
            ws.Cells("B1").Value = "Assist Service Type"
            ws.Cells("C1").Value = "Kode Tipe Kendaraan"
            ws.Cells("D1").Value = "Tipe Kendaraan"
            ws.Cells("E1").Value = "Jenis Kegiatan"
            ws.Cells("F1").Value = "Jenis Service"
            ws.Cells("G1").Value = "Standard Waktu Dealer (Jam)"
            ws.Cells("H1").Value = "Standard Waktu System (Jam)"

            'Dim standardCodeStatusClaimList As List(Of StandardCode) = New StandardCodeFacade(Me.User).RetrieveByCategory("ChassisMasterClaim.StatusClaim").Cast(Of  _
            '                                    StandardCode).ToList()
            'Dim standardCodeStatusProsesReturList As List(Of StandardCode) = New StandardCodeFacade(Me.User).RetrieveByCategory("ChassisMasterClaim.StatusProsesRetur").Cast(Of  _
            'StandardCode).ToList()
            Dim strJenisServis As String = ""
            Dim strAssist As String = ""
            Dim strJenisKegiatan As String = ""
            Dim idx As Integer = 0
            For i As Integer = 0 To Data.Count - 1
                Dim item As ServiceStandardTime = Data(i)

                ws.Cells(idx + 2, 1).Value = idx + 1
                'ws.Cells(idx + 2, 2).Value = item.Dealer.DealerCode

                Dim criterias33 As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias33.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.StallServiceType"))
                criterias33.opAnd(New Criteria(GetType(StandardCode), "ValueCode", MatchType.Exact, item.AssistServiceTypeCode))
                Dim arrDDLs As ArrayList = New StandardCodeFacade(User).Retrieve(criterias33)
                Dim objStandardCodes As New StandardCode
                If Not IsNothing(arrDDLs) AndAlso arrDDLs.Count > 0 Then
                    objStandardCodes = CType(arrDDLs(0), StandardCode)
                    strAssist = objStandardCodes.ValueDesc
                End If

                ws.Cells(idx + 2, 2).Value = strAssist

                ws.Cells(idx + 2, 3).Value = item.VechileType.VechileTypeCode
                ws.Cells(idx + 2, 4).Value = item.VechileType.Description
                Dim criterias3 As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias3.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.ServiceType"))
                criterias3.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, item.ServiceTypeID))
                Dim arrDDL As ArrayList = New StandardCodeFacade(User).Retrieve(criterias3)
                Dim objStandardCode As New StandardCode
                If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
                    objStandardCode = CType(arrDDL(0), StandardCode)
                    strJenisKegiatan = objStandardCode.ValueDesc
                End If

                ws.Cells(idx + 2, 5).Value = strJenisKegiatan

                Select Case item.ServiceTypeID
                    Case 1
                        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias2.opAnd(New Criteria(GetType(FSKind), "KindCode", MatchType.Exact, item.KindCode))
                        Dim arrFSK2 As ArrayList = New FSKindFacade(User).Retrieve(criterias2)
                        Dim objFSKind As New FSKind
                        If Not IsNothing(arrFSK2) AndAlso arrFSK2.Count > 0 Then
                            objFSKind = CType(arrFSK2(0), FSKind)
                            strJenisServis = objFSKind.KindCode 'objFSKind.KindDescription 'objFSKind.KindCode & " - " & objFSKind.KindDescription
                        End If

                    Case 2
                        Dim criterias0 As New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias0.opAnd(New Criteria(GetType(PMKind), "KindCode", MatchType.Exact, item.KindCode))
                        Dim arrFSK As ArrayList = New PMKindFacade(User).Retrieve(criterias0)
                        Dim objPMKind As New PMKind
                        If Not IsNothing(arrFSK) AndAlso arrFSK.Count > 0 Then
                            objPMKind = CType(arrFSK(0), PMKind)
                            strJenisServis = objPMKind.KindCode 'objPMKind.KindDescription 'objPMKind.KindCode & " - " & objPMKind.KindDescription
                        End If
                    Case 3
                        Dim criteriasa As New CriteriaComposite(New Criteria(GetType(RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteriasa.opAnd(New Criteria(GetType(RecallCategory), "RecallRegNo", MatchType.Exact, item.KindCode))
                        Dim arrRC As ArrayList = New RecallCategoryFacade(User).Retrieve(criteriasa)
                        Dim objRecallCategory As New RecallCategory
                        If Not IsNothing(arrRC) AndAlso arrRC.Count > 0 Then
                            objRecallCategory = CType(arrRC(0), RecallCategory)
                            strJenisServis = objRecallCategory.RecallRegNo 'objRecallCategory.Description 'objRecallCategory.RecallRegNo & " - " & objRecallCategory.Description
                        End If
                    Case 4
                        Dim criterias01 As New CriteriaComposite(New Criteria(GetType(GRKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias01.opAnd(New Criteria(GetType(GRKind), "KindCode", MatchType.Exact, item.KindCode))
                        Dim arrFSK As ArrayList = New GRKindFacade(User).Retrieve(criterias01)
                        Dim objPMKind As New GRKind
                        If Not IsNothing(arrFSK) AndAlso arrFSK.Count > 0 Then
                            objPMKind = CType(arrFSK(0), GRKind)
                            strJenisServis = objPMKind.KindCode 'objPMKind.KindDescription 'objPMKind.KindCode & " - " & objPMKind.KindDescription
                        End If
                End Select


                ws.Cells(idx + 2, 6).Value = strJenisServis
                ws.Cells(idx + 2, 7).Value = item.DealerStandardTime 'Format(Double.Parse(item.DealerStandardTime), "0.00")
                ws.Cells(idx + 2, 8).Value = item.SystemStandardTime 'Format(Double.Parse(item.SystemStandardTime), "0.00")

                idx = idx + 1
            Next

            CreateExcelFile(pck, FileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xlsx")
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

        Response.AppendHeader("Content-Length", fileBytes.Length.ToString())
        Response.AppendHeader("Content-Disposition", String.Format("attachment; filename=""{0}"";", fileName))
        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"  'xlsx
        Response.ContentType = "application/vnd.ms-excel" 'xls
        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()

    End Sub
#End Region
    
    Protected Sub btnBatal_Click(sender As Object, e As EventArgs)
        ResetControl()
        'ReadData()
        'dtgServiceStandardTime.CurrentPageIndex = 0
        'BindPage(dtgServiceStandardTime.CurrentPageIndex)
        dtgServiceStandardTime.DataSource = Nothing
        dtgServiceStandardTime.Visible = False
        Session.Remove("ServiceStandardTime")
    End Sub
End Class