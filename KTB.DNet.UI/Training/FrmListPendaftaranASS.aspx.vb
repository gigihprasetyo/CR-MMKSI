#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports System.IO
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions
Imports OfficeOpenXml
#End Region

Public Class FrmListPendaftaranASS
    Inherits System.Web.UI.Page

    Private helpers As New TrainingHelpers(Me.Page, "Training After Sales - List Pendaftaran")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.view, SR.TrainingAssViewListPendaftaran_Privilege)
        helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditListPendaftaran_Privilege)
        helpers.Privilage()
        If Not IsPostBack Then
            BindDdlFiscalYear()
            If Not helpers.IsNullCriteria Then
                ReadCritriaSearch()
                btnCari_Click(sender, e)
            Else
                defaultPage()
            End If
            ViewState("CurrentSortColumn") = "Dealer.ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            lblPopUpCourse.AddOnClick("ShowPPCourseSelection('ass');")
            lblPopUpClass.AddOnClick("ShowPopupClassSelection();")
            lblPopUpDealer.AddOnClick("ShowPPDealerSelection();")
            btnCancel.Attributes.Add("OnClick", "return confirm('Apakah yakin siswa tersebut dicancel?');")
        End If
    End Sub

    Private Sub BindDdlFiscalYear()
        Dim GetTahun As Integer = DateTime.Now.Year
        ddlFiscalYear.ClearSelection()
        ddlFiscalYear.Items.Clear()
        'Before
        For x As Integer = 4 To 0 Step -1
            Dim value1 As String = (GetTahun - x).ToString()
            Dim value2 As String = (GetTahun - x - 1).ToString()
            Dim value As String = String.Format("{0}/{1}", value2, value1)
            ddlFiscalYear.Items.Add(New ListItem(value, value))
        Next
        'After
        For x As Integer = 0 To 4
            Dim value1 As String = (GetTahun + x).ToString()
            Dim value2 As String = (GetTahun + x + 1).ToString()
            Dim value As String = String.Format("{0}/{1}", value1, value2)
            ddlFiscalYear.Items.Add(New ListItem(value, value))
        Next
        ddlFiscalYear.SelectedValue = String.Format("{0}/{1}", GetTahun.ToString(), (GetTahun + 1).ToString())
        'ddlFiscalYear.Items.Insert(0, New ListItem("Silakan Pilih", "-1"))
    End Sub

    Private Sub defaultPage(Optional isActive As Boolean = False)
        lblPageTitle.Text = "Training After Sales - List Pendaftaran"
        lblDealerCode.Text = Me.GetDealer.DealerCode
        lblDealerName.Text = Me.GetDealer.DealerName
        If Me.IsDealer Then
            trSearch.Visible = False
        End If

        'trDataSiswa.Visible = isActive
        panelGrid.Visible = isActive
        trAction.Visible = isActive
        If Not helpers.IsEdit Then
            trAction.Visible = False
        End If
    End Sub

    Private Sub SaveCriteriaSearch()
        helpers.ClearCriteria()
        helpers.AddCriteria("DealerCode", txtKodeDealer.Text)
        helpers.AddCriteria("CourseCode", txtKodeKategori.Text)
        helpers.AddCriteria("ClassCode", txtClassCode.Text)
        helpers.AddCriteria("RegNo", txtNoReg.Text)
        helpers.AddCriteria("TrNama", txtTraineeName.Text)
        helpers.AddCriteria("FiscalYear", ddlFiscalYear.SelectedValue)
        helpers.SaveCriteria()
    End Sub

    Private Sub ReadCritriaSearch()
        txtKodeDealer.Text = helpers.GetStringCriteria("DealerCode")
        txtKodeKategori.Text = helpers.GetStringCriteria("CourseCode")
        txtClassCode.Text = helpers.GetStringCriteria("ClassCode")
        txtNoReg.Text = helpers.GetStringCriteria("RegNo")
        txtTraineeName.Text = helpers.GetStringCriteria("TrNama")
        ddlFiscalYear.SelectedValue = helpers.GetStringCriteria("FiscalYear")

    End Sub

    'Private Sub BindDDLTahunFiskal(ByVal ddl As DropDownList)
    '    Dim GetTahun As Integer = DateTime.Now.Year
    '    ddl.ClearSelection()
    '    ddl.Items.Clear()
    '    'Before
    '    For x As Integer = 10 To 0 Step -1
    '        Dim value1 As String = (GetTahun - x).ToString()
    '        Dim value2 As String = (GetTahun - x - 1).ToString()
    '        Dim value As String = String.Format("{0}/{1}", value2, value1)
    '        ddl.Items.Add(New ListItem(value, value))
    '    Next
    '    'After
    '    For x As Integer = 0 To 10
    '        Dim value1 As String = (GetTahun + x).ToString()
    '        Dim value2 As String = (GetTahun + x + 1).ToString()
    '        Dim value As String = String.Format("{0}/{1}", value1, value2)
    '        ddl.Items.Add(New ListItem(value, value))
    '    Next
    '    ddl.SelectedValue = String.Format("{0}/{1}", GetTahun.ToString(), (GetTahun + 1).ToString())
    'End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Me.ClearTextBoxWithPrefix("txt")
        defaultPage()
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        defaultPage()
        SaveCriteriaSearch()
        Dim criteria As ICriteria = CriteriaSearch()
        BindDataGrid(criteria)
        defaultPage(True)
    End Sub


    Private Function JumlahTerdaftar(ByVal id As Integer) As Integer
        Dim objCAFac As TrBookingCourseFacade = New TrBookingCourseFacade(User)
        Dim arlCA As ArrayList = New ArrayList
        Dim crtCA As CriteriaComposite
        Dim Tot As Integer = 0

        crtCA = New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtCA.opAnd(New Criteria(GetType(TrBookingCourse), "TrClassRegistration.TrClass.ID", id))
        Dim aggCA As Aggregate = New Aggregate(GetType(TrBookingCourse), "ID", AggregateType.Count)
        Tot = objCAFac.GetAggregateResult(aggCA, crtCA)
        Return Tot
    End Function

    Public Sub BindDataGrid(ByVal criteria As ICriteria, Optional indexPage As Integer = 0)
        Dim func As New TrClassRegistrationFacade(User)
        Dim totalRow As Integer = 0

        Dim datas As ArrayList = func.RetrieveActiveList(indexPage + 1, dtgHeader.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
                               CType(ViewState("CurrentSortDirect"), Sort.SortDirection), criteria)
        dtgHeader.CurrentPageIndex = indexPage
        dtgHeader.DataSource = datas
        dtgHeader.VirtualItemCount = totalRow
        dtgHeader.DataBind()
    End Sub

    Private Function CriteriaSearch() As CriteriaComposite
        Dim criteria As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        

        If txtKodeKategori.IsNotEmpty Then
            criteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrClass.TrCourse.CourseCode", MatchType.Exact, txtKodeKategori.Text.Trim))
        End If

        If txtClassCode.IsNotEmpty Then
            criteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrClass.ClassCode", MatchType.Exact, txtClassCode.Text.Trim))
        End If

        If txtNoReg.IsNotEmpty Then
            criteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrTrainee.ID", MatchType.Exact, txtNoReg.Text.Trim))
        End If

        criteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrClass.FiscalYear", MatchType.Exact, ddlFiscalYear.SelectedValue))

        If cbDate.Checked = True Then
            Dim tglMulai As DateTime = New Date(icStart.Value.Year, icStart.Value.Month, icStart.Value.Day, 0, 0, 0)
            Dim tglAkhir As DateTime = New Date(icEnd.Value.Year, icEnd.Value.Month, icEnd.Value.Day, 23, 59, 59)
            criteria.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.StartDate", MatchType.GreaterOrEqual, tglMulai))
            criteria.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.StartDate", MatchType.LesserOrEqual, tglAkhir))
        End If

        If txtTraineeName.IsNotEmpty Then
            criteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrTrainee.Name", MatchType.Partial, txtTraineeName.Text.Trim))
        End If

        If Me.IsDealer Then
            criteria.opAnd(New Criteria(GetType(TrClassRegistration), "Dealer.ID", MatchType.Exact, Me.GetDealer.ID))
            
        Else
            Dim dealerInSet As String = txtKodeDealer.Text.GenerateInSet()
            If Not String.IsNullorEmpty(txtKodeDealer.Text) Then
                criteria.opAnd(New Criteria(GetType(TrClassRegistration), "Dealer.DealerCode", MatchType.InSet, String.Format("({0})", dealerInSet)))
            End If
            
        End If
        criteria.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.TrCourse.JobPositionCategory.AreaID", MatchType.Exact, 2))
        helpers.SetSession("criteria", criteria)

        Return criteria
    End Function

    Private Sub dtgHeader_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgHeader.ItemCommand
        Select Case e.CommandName.ToLower()
            Case "replace"
                Dim strUrl As String = "FrmReplacePendaftaran.aspx?id={0}&classcode={1}&dealercode={2}"
                Dim hdnId As String = CType(e.Item.FindControl("hdnID"), HiddenField).Value
                Dim dealerCode As String = CType(e.Item.FindControl("lblDealerCode"), Label).Text
                Dim classCode As String = CType(e.Item.FindControl("lnkClass"), HyperLink).Text

                Response.Redirect(String.Format(strUrl, hdnId, classCode, dealerCode))
            Case "add"
                Dim classCode As String = CType(e.Item.FindControl("lnkClass"), HyperLink).Text
                Response.Redirect("FrmDaftarTrainingMKS.aspx?classcode=" + classCode)
        End Select
    End Sub

    Private Sub dtgHeader_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgHeader.ItemDataBound
        If e.IsRowItems Then
            Dim hdnID As HiddenField = e.FindHiddenField("hdnID")
            Dim lblNo As Label = e.FindLabel("lblNo")
            Dim lblNoReg As Label = e.FindLabel("lblNoReg")
            Dim lblNamaSiswa As Label = e.FindLabel("lblNamaSiswa")
            Dim lblDealerCode As Label = e.FindLabel("lblDealerCode")
            Dim lblDealerName As Label = e.FindLabel("lblDealerName")
            Dim lblDealerCity As Label = e.FindLabel("lblDealerCity")
            Dim lblClassName As Label = e.FindLabel("lblClassName")
            Dim lblPosisi As Label = e.FindLabel("lblPosisi")
            Dim lblStatus As Label = e.FindLabel("lblStatus")
            Dim lblRemark As Label = e.FindLabel("lblRemark")
            Dim lblTanggalMulai As Label = e.FindLabel("lblTanggalMulai")
            Dim lblTanggalMulaiKerja As Label = e.FindLabel("lblTanggalMulaiKerja")
            Dim lblTanggalSelesai As Label = e.FindLabel("lblTanggalSelesai")
            Dim lbtnReplace As LinkButton = e.FindLinkButton("lbtnReplace")
            Dim lbtnAdd As LinkButton = e.FindLinkButton("lbtnAdd")
            Dim chk As CheckBox = e.FindCheckBox("chkItemChecked")
            Dim lblSize As Label = e.FindLabel("lblSize")
            Dim lblGender As Label = e.FindLabel("lblGender")
            chk.Visible = False

            Dim rowValue As TrClassRegistration = e.DataItem(Of TrClassRegistration)()
            If Not IsNothing(rowValue) Then
                Dim dataClass As TrClass = rowValue.TrClass
                hdnID.Value = rowValue.ID
              
                lblNo.Text = e.CreateNumberPage()
                lblNoReg.Text = rowValue.TrTrainee.ID.ToString
                lblTanggalMulaiKerja.Text = rowValue.TrTrainee.StartWorkingDate.DateToString()
                lblDealerCode.Text = rowValue.Dealer.DealerCode
                lblDealerName.Text = rowValue.Dealer.DealerName
                lblDealerCity.Text = rowValue.Dealer.City.CityName
                lblNamaSiswa.Text = rowValue.TrTrainee.Name
                'lblKodeDealer.Text = rowValue.Dealer.DealerCode
                lblTanggalMulai.Text = dataClass.StartDate.DateToString
                lblTanggalSelesai.Text = dataClass.FinishDate.DateToString
                lblRemark.Text = rowValue.Notes
                lblClassName.Text = rowValue.TrClass.ClassName
                lblSize.Text = rowValue.TrTrainee.ShirtSize
                lblGender.Text = EnumGender.GetStringGender(rowValue.TrTrainee.Gender)
                Try
                    lblPosisi.Text = rowValue.TrTrainee.RefJobPosition.Description
                Catch
                End Try
                Dim lnk As HyperLink = CType(e.Item.FindControl("lnkClass"), HyperLink)
                If Not lnk Is Nothing Then
                    lnk.Text = dataClass.ClassCode
                    lnk.NavigateUrl = "javascript:popUpClassInformation('" & dataClass.ClassCode & "');"
                End If
                If rowValue.Dealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
                    If dataClass.TrCourse.PaymentType = CType(EnumTrCourse.PaymentType.CHARGE, Short) And dataClass.FinishDate > Me.DateNow Then
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBillingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(TrBillingDetail), "TrBookingCourse.TrClassRegistration.ID", MatchType.Exact, rowValue.ID))

                        Dim arrDataTagihan As ArrayList = New TrBillingDetailFacade(User).Retrieve(criterias)
                        If arrDataTagihan.Count > 0 Then
                            Select Case CType(arrDataTagihan(0), TrBillingDetail).TrBillingHeader.Status
                                Case CType(EnumTagihanTraining.TagihanStatus.Selesai, Short)
                                    lblStatus.Text = "Terdaftar"
                                    lbtnReplace.Visible = True
                                Case CType(EnumTagihanTraining.TagihanStatus.Validasi, Short)
                                    lblStatus.Text = "Proses Debit Note"
                                Case CType(EnumTagihanTraining.TagihanStatus.Disetujui, Short)
                                    lblStatus.Text = "Proses Debit Note"
                                Case CType(EnumTagihanTraining.TagihanStatus.Konfirmasi, Short)
                                    lblStatus.Text = "Proses Debit Note"
                                Case CType(EnumTagihanTraining.TagihanStatus.Pembayaran_Transfer, Short)
                                    lblStatus.Text = "Proses Transfer"
                                Case CType(EnumTagihanTraining.TagihanStatus.Proses_Transfer, Short)
                                    lblStatus.Text = "Proses Transfer"
                                Case CType(EnumTagihanTraining.TagihanStatus.Pencairan_Deposit_B, Short)
                                    lblStatus.Text = "Proses Pencairan Deposit B"
                            End Select
                        Else
                            lbtnReplace.Visible = True
                            lblStatus.Text = "Belum disetujui"
                            chk.Visible = True
                        End If
                    Else
                        If dataClass.TrCourse.PaymentType = CType(EnumTrCourse.PaymentType.FREE, Short) And dataClass.FinishDate > Me.DateNow Then
                            chk.Visible = True
                            lbtnReplace.Visible = True
                            lbtnAdd.Visible = True
                        End If
                        lblStatus.Text = New EnumTrClassRegistration().StatusByIndex(CInt(CType(DataBinder.Eval(e.Item.DataItem, "Status"), String)))
                    End If
                Else

                    lbtnReplace.Visible = False
                    lblStatus.Text = New EnumTrClassRegistration().StatusByIndex(CInt(CType(DataBinder.Eval(e.Item.DataItem, "Status"), String)))
                End If

                If dataClass.FinishDate < Me.DateNow() Then
                    lbtnReplace.Visible = False
                    lbtnAdd.Visible = False
                    chk.Visible = False
                End If

                If Not helpers.IsEdit Then
                    chk.Visible = False
                    lbtnAdd.Visible = False
                    lbtnReplace.Visible = False
                End If

            End If
        End If
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Using excelPackage As New ExcelPackage()
            Dim wsData As ExcelWorksheet = excelPackage.Workbook.Worksheets.Add("Data Siswa Kelas")
            Dim rowIdx As Integer = 1
            Dim noUrut As Integer = 1

            CreateHeaderColumn(wsData, rowIdx)
            rowIdx += 1
            Dim criterias As ICriteria = helpers.GetSession("criteria")
            Dim sortColl As SortCollection = New SortCollection
            
            If (Not IsNothing(CType(ViewState("CurrentSortColumn"), String))) And (Not IsNothing(CType(ViewState("CurrentSortDirect"), Sort.SortDirection))) Then
                sortColl.Add(New Sort(GetType(TrClassRegistration), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
            Else
                sortColl = Nothing
            End If

            Dim dataDownload As ArrayList = New TrClassRegistrationFacade(Me.User).Retrieve(criterias, sortColl)
            For Each dataReg As TrClassRegistration In dataDownload
                Dim dataClass As TrClass = dataReg.TrClass
                Dim status As String = String.Empty
                Dim clmidx As Integer = 1
                If dataReg.Dealer.IsDealer Then
                    If dataClass.TrCourse.PaymentType = CType(EnumTrCourse.PaymentType.CHARGE, Short) And dataClass.FinishDate > Me.DateNow Then
                        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBillingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crits.opAnd(New Criteria(GetType(TrBillingDetail), "TrBookingCourse.TrClassRegistration.ID", MatchType.Exact, dataReg.ID))

                        Dim arrDataTagihan As ArrayList = New TrBillingDetailFacade(User).Retrieve(crits)
                        If arrDataTagihan.Count > 0 Then
                            Select Case CType(arrDataTagihan(0), TrBillingDetail).TrBillingHeader.Status
                                Case CType(EnumTagihanTraining.TagihanStatus.Selesai, Short)
                                    status = "Terdaftar"
                                Case CType(EnumTagihanTraining.TagihanStatus.Validasi, Short)
                                    status = "Proses Debit Note"
                                Case CType(EnumTagihanTraining.TagihanStatus.Disetujui, Short)
                                    status = "Proses Debit Note"
                                Case CType(EnumTagihanTraining.TagihanStatus.Konfirmasi, Short)
                                    status = "Proses Debit Note"
                                Case CType(EnumTagihanTraining.TagihanStatus.Pembayaran_Transfer, Short)
                                    status = "Proses Transfer"
                                Case CType(EnumTagihanTraining.TagihanStatus.Proses_Transfer, Short)
                                    status = "Proses Transfer"
                                Case CType(EnumTagihanTraining.TagihanStatus.Pencairan_Deposit_B, Short)
                                    status = "Proses Pencairan Deposit B"
                            End Select
                        Else
                            status = "Belum disetujui"
                        End If
                    Else
                        status = New EnumTrClassRegistration().StatusByIndex(dataReg.Status)
                    End If
                Else
                    status = New EnumTrClassRegistration().StatusByIndex(dataReg.Status)
                End If
                Dim rowValue As TrTrainee = dataReg.TrTrainee

                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(noUrut.ToString(), Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(rowValue.ID.ToString, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(rowValue.Name, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(rowValue.Dealer.DealerCode, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(rowValue.RefJobPosition.Description, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(dataClass.ClassCode, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(dataClass.StartDate.DateToString, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(dataClass.FinishDate.DateToString, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(status, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(dataReg.Notes, Style.ExcelHorizontalAlignment.Center)
                rowIdx += 1
                noUrut += 1

            Next
            rowIdx += 1
            rowIdx += 1
            For colIdx As Integer = 2 To 10
                wsData.Column(colIdx).AutoFit()
            Next

            Dim fileBytes = excelPackage.GetAsByteArray()
            Dim fileName As String = String.Format("DaftarSiswaKelas.xls")
            'Me.Response.Clear()
            'Me.Server.ScriptTimeout = 2000
            'Me.Response.AppendHeader("Content-Length", fileBytes.Length.ToString())
            'Me.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" 'xls
            'Me.Response.AddHeader("content-disposition", String.Format(" filename={0}", fileName))
            'Me.Response.BinaryWrite(fileBytes)
            'Me.Response.Flush()
            'Me.Response.[End]()

            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

            Dim success As Boolean = False

            Try
                success = imp.Start()
                If success Then
                    File.WriteAllBytes(Server.MapPath("~/DataTemp/" & fileName), fileBytes)
                    imp.StopImpersonate()
                End If

            Catch ex As Exception
                Exit Sub

            End Try

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & fileName)

        End Using
    End Sub

    Private Sub CreateHeaderColumn(ByVal wsData As ExcelWorksheet, ByVal rowIdx As Integer)
        Dim columnIdx As Integer = 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("No")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("No. Reg")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Nama Siswa")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Kode Org.")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Posisi")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Kode Kelas")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Tanggal Mulai")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Tanggal Selesai")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Status")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Remark")
        columnIdx += 1

    End Sub

    Private Sub dtgHeader_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgHeader.PageIndexChanged
        Dim criteria As ICriteria = helpers.GetSession("criteria")
        BindDataGrid(criteria, e.NewPageIndex)
    End Sub

    Private Sub dtgHeader_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgHeader.SortCommand
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
        Dim criterias2 As ICriteria = helpers.GetSession("criteria")
        BindDataGrid(criterias2, dtgHeader.CurrentPageIndex)
    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim listID As New List(Of Integer)
        For Each eItem As DataGridItem In dtgHeader.Items
            Dim chk As CheckBox = eItem.FindCheckBox("chkItemChecked")
            If chk.Checked Then
                Dim hdnID As HiddenField = eItem.FindHiddenField("hdnID")
                listID.Add(CInt(hdnID.Value))
            End If
        Next
        If listID.Count = 0 Then
            MessageBox.Show("Silahkan pilih siswa!")
            Return
        End If
        Dim funcReg As New TrClassRegistrationFacade(User)
        Dim funcB As New TrBookingCourseFacade(User)
        Dim funcBC As New TrBookingClassFacade(User)
        Dim nProses As Integer = 0
        For Each id As Integer In listID
            Try
                Dim dataReg As TrClassRegistration = funcReg.Retrieve(id)

                Dim criteria As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrBookingCourse), "TrClassRegistration.ID", MatchType.Exact, id))

                Dim dataBooking As TrBookingCourse = CType(funcB.Retrieve(criteria)(0), TrBookingCourse)

                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrBookingClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrBookingClass), "TrClassRegistration.ID", MatchType.Exact, id))

                Dim dataBClass As TrBookingClass = CType(funcBC.Retrieve(criterias)(0), TrBookingClass)

                funcBC.Delete(dataBClass)
                funcReg.Delete(dataReg)
                dataBooking.TrClassRegistration = Nothing
                funcB.Update(dataBooking)
                nProses += 1
            Catch ex As Exception
                Continue For
            End Try
        Next
        MessageBox.Show(String.Format("{0} siswa berhasil dicancel. {1} siswa gagal dicancel.", nProses, (listID.Count - nProses)))

        Dim criterias2 As ICriteria = helpers.GetSession("criteria")
        BindDataGrid(criterias2, dtgHeader.CurrentPageIndex)
    End Sub
End Class