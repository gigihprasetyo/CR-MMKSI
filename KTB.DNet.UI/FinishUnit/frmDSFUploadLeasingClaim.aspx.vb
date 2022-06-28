Imports KTB.DNet.BusinessFacade.Benefit
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.DSFLeasingClaim
Imports System.Linq
Imports System.IO
Imports Excel
Imports OfficeOpenXml
Imports System.Text
Imports KTB.DNet.BusinessFacade
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Globalization
Imports KTB.DNet.Security

Public Class frmDSFUploadLeasingClaim
    Inherits System.Web.UI.Page

#Region "Private Property"
    Dim simpanPriv As Boolean = False

    Private sessHelper As New SessionHelper
    Private tempFilePath As String = Server.MapPath("") & "Benefit\"
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.Campaign_Upload_Leasing_Claim_Lihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=CAMPAIGN - UPLOAD CHASSIS CLAIM")
        Else
            simpanPriv = SecurityProvider.Authorize(Context.User, SR.Campaign_Upload_Leasing_Claim_Simpan_Privilege)
        End If
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            ViewState("currSortColumn") = "ID"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            InitDisplayPilihanClaim()
            InitDisplayStatusUpload()
            InitDisplayMainGrid(0)
            sessHelper.SetSession("excelValidated", Nothing)
            btnDownload.Visible = False
            'DeleteTempFiles()
            RegisterClientScriptBlock("hideLoader", "<script language=JavaScript>hideLoader();</script>")
        Else
            If sessHelper.GetSession("isUploadLoadingProcess") = True Then
                RegisterClientScriptBlock("showLoader", "<script language=JavaScript>showLoader();</script>")
            Else
                RegisterClientScriptBlock("hideLoader", "<script language=JavaScript>hideLoader();</script>")
            End If
        End If
        Panel2.Attributes("style") = "display:none;"
        panel1.Attributes("style") = "display:;"
    End Sub

    Private Sub DeleteTempFiles()
        Dim directoryName As String = Server.MapPath("") & "\Temp"
        For Each deleteFile As String In Directory.GetFiles(directoryName, "*.*")
            File.Delete(deleteFile)
        Next
    End Sub

    Private Sub InitDisplayPilihanClaim()
        Dim facade As New BenefitTypeFacade(User)
        Dim arlFacade As ArrayList = facade.RetrieveActiveList(1)

        ddlPilihanClaim.Items.Clear()

        For Each cat As BenefitType In arlFacade
            If cat.Status = 0 Then
                ddlPilihanClaim.Items.Add(New ListItem(cat.Name, cat.ID.ToString & ";" & cat.LeasingBox.ToString & ";" & cat.EventValidation.ToString))
            End If
        Next

    End Sub

    Private Sub InitDisplayStatusUpload()
        ddlStatusUpload.Items.Clear()
        ddlStatusUpload.Items.Add(New ListItem("Semua", 9))
        ddlStatusUpload.Items.Add(New ListItem("Valid", 1))
        ddlStatusUpload.Items.Add(New ListItem("Tidak Valid", 0))
    End Sub

    Private Sub InitDisplayMainGrid(ByVal pageNumber As Integer)
        Dim list As ArrayList = sessHelper.GetSession("DataForSave")
        Dim PagedList As ArrayList = New ArrayList

        If IsNothing(list) Then
            Dim _q = New List(Of DSFLeasingClaim) '(From ob As DSFLeasingClaim In list Where 0 = 1 Select ob)
            list = New ArrayList
            list.AddRange(_q)
        End If

        If (ddlStatusUpload.SelectedValue = 9) Then
            dgTable.DataSource = Nothing
            CommonFunction.SortListControl(list, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            PagedList = ArrayListPager.DoPage(list, pageNumber, dgTable.PageSize)
        Else
            Dim criteriaStatus As ValidateResult = CType(ddlStatusUpload.SelectedValue, ValidateResult)
            Dim _arrDSFClaim As List(Of DSFLeasingClaim)
            _arrDSFClaim = (From ob As DSFLeasingClaim In list
                           Where ob.ValidatingResult = criteriaStatus
                           ).ToList

            list = New ArrayList
            list.AddRange(_arrDSFClaim)
            CommonFunction.SortListControl(list, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            PagedList = ArrayListPager.DoPage(list, pageNumber, dgTable.PageSize)
        End If
        dgTable.DataSource = PagedList
        dgTable.VirtualItemCount = list.Count()
        dgTable.CurrentPageIndex = pageNumber
        dgTable.DataBind()
        lblRecordCount.Text = list.Count().ToString()
    End Sub

    Private Sub btnRefClaim_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefClaim.Click
        dgGridDetil.CurrentPageIndex = 0
        BindRefClaim(dgGridDetil.CurrentPageIndex)
    End Sub

    Private Sub BindRefClaim(ByVal pageIndex As Integer)
        Dim selectedClaimID As Integer = ddlPilihanClaim.SelectedItem.Value.Split(";")(0)
        Dim arrBenefitMasterHeader As ArrayList = New BenefitMasterHeaderFacade(User).BindGridDSFClaim(selectedClaimID)
        If arrBenefitMasterHeader.Count <> 0 Then
            CommonFunction.SortListControl(arrBenefitMasterHeader, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrBenefitMasterHeader, pageIndex, dgGridDetil.PageSize)
            dgGridDetil.DataSource = PagedList
            dgGridDetil.VirtualItemCount = arrBenefitMasterHeader.Count()
            dgGridDetil.DataBind()
        Else
            dgGridDetil.DataSource = New ArrayList
            dgGridDetil.VirtualItemCount = 0
            dgGridDetil.CurrentPageIndex = 0
            dgGridDetil.DataBind()
            MessageBox.Show("Data tidak ditemukan.")
        End If
        Panel2.Attributes("style") = "display:;"
        panel1.Attributes("style") = "display:none;"

    End Sub

    Private Sub dgGridDetil_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgGridDetil.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            Dim objDomain2 As BenefitMasterHeader = CType(e.Item.DataItem, BenefitMasterHeader)
            ' If e.Item.ItemType = ListItemType.Item Then
            If Not objDomain2 Is Nothing Then
                If Not e.Item.ItemType = ListItemType.EditItem Then
                    Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name='rb'>")
                    e.Item.Cells(0).Controls.Add(rdbChoice)

                    Dim lblIDoGridDetil As Label = CType(e.Item.FindControl("lblIDoGridDetil"), Label)
                    lblIDoGridDetil.Text = objDomain2.ID.ToString

                    Dim lblformula As Label = CType(e.Item.FindControl("lblformula"), Label)
                    lblformula.Text = objDomain2.Formula

                    Dim lblNoGridDetil As Label = CType(e.Item.FindControl("lblNoGridDetil"), Label)
                    lblNoGridDetil.Text = (e.Item.ItemIndex + 1 + (dgGridDetil.CurrentPageIndex * dgGridDetil.PageSize)).ToString

                    Dim lblnnosuratGridDetil As Label = CType(e.Item.FindControl("lblnnosuratGridDetil"), Label)
                    lblnnosuratGridDetil.Text = objDomain2.NomorSurat

                    Dim lblNoRegBenefitGridDetil As Label = CType(e.Item.FindControl("lblNoRegBenefitGridDetil"), Label)
                    lblNoRegBenefitGridDetil.Text = objDomain2.BenefitRegNo

                    Dim lbldeskripsiGridDetil As Label = CType(e.Item.FindControl("lbldeskripsiGridDetil"), Label)
                    lbldeskripsiGridDetil.Text = objDomain2.Remarks
                End If
            End If
        End If
    End Sub

    Private templateFileName As String = "UploadDSFClaim.xlsx"
    Private url_DownloadTemplate As String = "../downloadlocal.aspx?file=FinishUnit\"
    Private Sub LinkDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkDownload.Click
        Response.Redirect(url_DownloadTemplate & templateFileName)
        'Dim fileName As String = ""
        'If PumpDealerList(fileName) Then
        '    Response.Redirect(url_DownloadTemplate & fileName)
        'End If
    End Sub

    Private Function PumpDealerList(ByRef fileName As String) As Boolean
        Dim result As Boolean = New Boolean
        Dim _fileName As String = DateTime.Now.ToString("ddMMyyyHHmmss") & "_" & templateFileName
        Dim templateFileLocation As String = Server.MapPath("") & "\" & _fileName
        Dim objUploadHelper As New UploadToWebServer

        Try
            'objUploadHelper.Upload(fileUploadExcel.PostedFile.InputStream, templateFileLocation)
            'fileName = _fileName
            Dim fileInfo As FileInfo = New FileInfo(templateFileLocation)
            'Dim _excel As ExcelPackage = New ExcelPackage(fileInfo)
            '_excel.Workbook.Worksheets.Add("DealerList")
            'Dim _wsDealerList As ExcelWorksheet = _excel.Workbook.Worksheets("DealerList")
            'Dim critDealer As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'Dim arrDealer As ArrayList = New DealerFacade(User).Retrieve(critDealer)
            'Dim listDealer As List(Of Dealer) = New List(Of Dealer)(From d As Dealer In arrDealer Select d)
            'Dim i As Integer = 2

            '_wsDealerList.Cells(1, 1).Value = "Dealer Code"
            '_wsDealerList.Cells(1, 2).Value = "Dealer Name"

            'For Each dealer As KTB.DNet.Domain.Dealer In listDealer
            '    _wsDealerList.Cells(i, 1).Value = dealer.DealerCode
            '    _wsDealerList.Cells(i, 2).Value = dealer.DealerName
            '    i += 1
            'Next
            '_excel.SaveAs(fileInfo)

            Dim sfileName As String = "ExcellData.xlsx"
            Using package As New ExcelPackage(fileInfo)
                Dim worksheet As ExcelWorksheet = package.Workbook.Worksheets.FirstOrDefault()
                worksheet = package.Workbook.Worksheets.Add("Assessment Attempts")
                worksheet.Row(1).Height = 20

                worksheet.TabColor = Color.Gold
                worksheet.DefaultRowHeight = 12
                worksheet.Row(1).Height = 20

                worksheet.Cells(1, 1).Value = "Employee Number"
                worksheet.Cells(1, 2).Value = "Course Code"

                package.Workbook.Properties.Title = "Attempts"
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", String.Format("attachment;  filename={0}", "ExcellData.xlsx"))
                Response.BinaryWrite(package.GetAsByteArray())
            End Using

            result = True
        Catch ex As Exception
            result = False
        End Try
        Return result
    End Function

    Private Sub dgGridDetil_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgGridDetil.PageIndexChanged
        dgGridDetil.CurrentPageIndex = e.NewPageIndex
        BindRefClaim(e.NewPageIndex)
    End Sub

    Private Sub dgTable_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgTable.PageIndexChanged
        dgTable.CurrentPageIndex = e.NewPageIndex
        InitDisplayMainGrid(e.NewPageIndex)
        'BindExcelDataToGrid(e.NewPageIndex)
    End Sub

    Dim maxFileSize As Integer = 71680
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        btnUpload.Enabled = False
        If Not fileUploadExcel.HasFile Then
            MessageBox.Show("Silahkan pilih file excel terlebih dahulu")
        Else
            If IsValidFileSize() Then
                Try
                    DoUpload()
                Catch ex As Exception
                    MessageBox.Show("Gagal upload.")
                Finally

                End Try
            Else
                Dim _msg As String = String.Format("Ukuran file tidak boleh melebihi 70 KB.")
                MessageBox.Show(_msg)
            End If
        End If
        btnUpload.Enabled = True
    End Sub

    Private Function IsValidFileSize() As Boolean
        Dim result As Boolean = False
        If fileUploadExcel.FileBytes.Length <= maxFileSize Then
            result = True
        End If
        Return result
    End Function


    Private Sub ShowLoader(ByVal isShow As Boolean)
        If (isShow) Then
            sessHelper.SetSession("isUploadLoadingProcess", True)
        Else
            sessHelper.SetSession("isUploadLoadingProcess", False)
        End If
    End Sub

     

    Private Sub DoUpload()
        'ShowLoader(True)
        Dim list As ArrayList = New ArrayList
        Dim ext As String = System.IO.Path.GetExtension(fileUploadExcel.PostedFile.FileName)
        Dim SrcFile As String = "DSFUploadClaim" & ext 'Path.GetFileName(fileUploadExcel.PostedFile.FileName)
        Dim FolderForUpload As String = "DataTemp"
        Dim FileToUpload As String = FolderForUpload & "\" & DateTime.Now.ToString("ddMMyyyHHmmss") & SrcFile
        Dim targetFile As String = Server.MapPath("~\") & FileToUpload
        Dim objUploadHelper As New UploadToWebServer
        Dim objReader As IExcelDataReader = Nothing
        objUploadHelper.Upload(fileUploadExcel.PostedFile.InputStream, targetFile)
        Dim fileInfo As FileInfo = New FileInfo(targetFile)
        Dim _excel As ExcelPackage = New ExcelPackage(fileInfo)
        If ValidateAndWriteValidatingResult(_excel, list) Then
            If (list.Count > 0) Then
                sessHelper.SetSession("DataForSave", list)
                _excel.SaveAs(fileInfo)
                sessHelper.SetSession("fileUpDown", FileToUpload)
                BindExcelDataToGrid(0)
                ShowUploadSummary()
            Else
                MessageBox.Show("Data tidak ditemukan.")
            End If
        End If
        'ShowLoader(False)
    End Sub

    Private Function ValidateAndWriteValidatingResult(_excel As ExcelPackage, ByRef list As ArrayList) As Boolean
        Dim result As Boolean = New Boolean
        Try
            Dim _i As Integer = _excel.Workbook.Worksheets.Count
            Dim _ws As ExcelWorksheet = _excel.Workbook.Worksheets(1)
            Dim iHeaderRowPosition As Integer = 1
            Dim _startReadRow As Integer = _ws.Dimension.Start.Row + iHeaderRowPosition
            Dim dataRowCount As Integer = _ws.Dimension.Rows

            _ws.InsertColumn(2, 3)
            Dim colFromHex As Color = ColorTranslator.FromHtml("#d9e1f3")
            _ws.Cells(1, 2, 1, 4).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
            _ws.Cells(1, 2, 1, 4).Style.Fill.BackgroundColor.SetColor(colFromHex)
            _ws.Cells(1, 2).Value = "Dealer Code"
            _ws.Cells(1, 3).Value = "Dealer Name"
            _ws.Cells(1, 4).Value = "Reg Number"

            _ws.InsertColumn(8, 1)
            _ws.Cells(1, 8).Value = "Claim Date"
            _ws.Cells(1, 8, 1, 8).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
            _ws.Cells(1, 8, 1, 8).Style.Fill.BackgroundColor.SetColor(colFromHex)

            _ws.InsertColumn(14, 4)
            _ws.Cells(1, 14).Value = "Customer Name"
            _ws.Cells(1, 15).Value = "Unit"
            _ws.Cells(1, 16).Value = "Object Lease"
            _ws.Cells(1, 17).Value = "Production Year"
            _ws.Cells(1, 14, 1, 17).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
            _ws.Cells(1, 14, 1, 17).Style.Fill.BackgroundColor.SetColor(colFromHex)

            _ws.Cells(1, 28).Value = "Remarks Code"
            _ws.Cells(1, 29).Value = "Remarks Description"

            For iRow As Integer = _startReadRow To dataRowCount
                Dim critDealer As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim critBenClaimH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim critChassisMaster As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim xlsChassisNumber As String = _ws.Cells(iRow, 5).Text
                Dim xlsEngineNumber As String = _ws.Cells(iRow, 6).Text
                If xlsChassisNumber.Trim = "" And xlsEngineNumber.Trim = "" Then Continue For


                Dim dsfLeasingClaim As DSFLeasingClaim = New DSFLeasingClaim
                Dim objChassisMaster As New ChassisMaster
                Dim arrChassisMaster As New ArrayList

                'critChassisMaster.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, xlsChassisNumber))
                'critChassisMaster.opOr(New Criteria(GetType(ChassisMaster), "EngineNumber", MatchType.Exact, xlsEngineNumber))
                'arrChassisMaster = New ChassisMasterFacade(User).Retrieve(critChassisMaster)

                arrChassisMaster = New ChassisMasterFacade(User).RetrieveChassisFromSP(xlsChassisNumber, xlsEngineNumber)
                If Not IsNothing(arrChassisMaster) AndAlso arrChassisMaster.Count > 0 Then
                    objChassisMaster = CType(arrChassisMaster(0), ChassisMaster)
                End If

                dsfLeasingClaim.ChassisNumber = xlsChassisNumber
                dsfLeasingClaim.EngineNumber = xlsEngineNumber
                'dsfLeasingClaim.ChassisMaster = If(Not IsNothing(New ChassisMasterFacade(User).Retrieve(critChassisMaster)), New ChassisMasterFacade(User).Retrieve(critChassisMaster)(0), Nothing)
                dsfLeasingClaim.ChassisMaster = objChassisMaster

                dsfLeasingClaim.AssetSeqNo = _ws.Cells(iRow, 7).Text
                dsfLeasingClaim.AgreementNo = _ws.Cells(iRow, 9).Text
                dsfLeasingClaim.SKDNumber = _ws.Cells(iRow, 10).Text

                Dim sSKDDate As String = _ws.Cells(iRow, 11).Value.ToString()
                Dim _yyyy As Integer = CInt(sSKDDate.Substring(sSKDDate.Length - 4, 4))
                Dim _mm As Integer = CInt(sSKDDate.Substring(sSKDDate.Length - 6, 2))
                Dim _dd As Integer = CInt(sSKDDate.Substring(0, 2))
                dsfLeasingClaim.SKDDate = New Date(_yyyy, _mm, _dd)

                Dim sSKDApprovalDate As String = _ws.Cells(iRow, 12).Value.ToString()
                _yyyy = CInt(sSKDApprovalDate.Substring(sSKDApprovalDate.Length - 4, 4))
                _mm = CInt(sSKDApprovalDate.Substring(sSKDApprovalDate.Length - 6, 2))
                _dd = CInt(sSKDApprovalDate.Substring(0, 2))
                dsfLeasingClaim.SKDApprovalDate = New Date(_yyyy, _mm, _dd)

                Dim sGoLiveDate As String = _ws.Cells(iRow, 13).Value.ToString()
                _yyyy = CInt(sGoLiveDate.Substring(sGoLiveDate.Length - 4, 4))
                _mm = CInt(sGoLiveDate.Substring(sGoLiveDate.Length - 6, 2))
                _dd = CInt(sGoLiveDate.Substring(0, 2))
                dsfLeasingClaim.GoLiveDate = New Date(_yyyy, _mm, _dd)

                Dim _ATPMSubsidy As Decimal = 0
                dsfLeasingClaim.ATPMSubsidy = ConvertToDecimal(_ws.Cells(iRow, 18).Value)
                dsfLeasingClaim.SupplierName = _ws.Cells(iRow, 19).Text
                dsfLeasingClaim.ProgramName = _ws.Cells(iRow, 20).Text

                Dim sCollectionPeriod As String = _ws.Cells(iRow, 21).Text
                _yyyy = sCollectionPeriod.Substring(sCollectionPeriod.Length - 4, 4)
                _mm = sCollectionPeriod.Substring(0, 2)
                dsfLeasingClaim.CollectionPeriodMonth = CInt(_mm)
                dsfLeasingClaim.CollectionPeriodYear = CInt(_yyyy)
                dsfLeasingClaim.CollectionPeriod = _mm & "/" & _yyyy

                dsfLeasingClaim.TotalDP = ConvertToDecimal(_ws.Cells(iRow, 22).Value)
                dsfLeasingClaim.TotalAmountLease = ConvertToDecimal(_ws.Cells(iRow, 23).Value)
                dsfLeasingClaim.PeriodLease = If(IsNumeric(_ws.Cells(iRow, 24).Value), CInt(_ws.Cells(iRow, 24).Value), 0)
                dsfLeasingClaim.InterestLease = ConvertToDecimal(_ws.Cells(iRow, 25).Value)
                dsfLeasingClaim.Insurance = _ws.Cells(iRow, 26).Text
                dsfLeasingClaim.TypeInsurance = _ws.Cells(iRow, 27).Text
                dsfLeasingClaim.Status = 0
                dsfLeasingClaim.RegNumber = GetRegNumber(iRow - iHeaderRowPosition)
                dsfLeasingClaim.ClaimDate = Date.Now

                If Not IsNothing(objChassisMaster) AndAlso objChassisMaster.ID > 0 Then
                    _ws.Cells(iRow, 4).Value = "-"   'Reg Number
                    _ws.Cells(iRow, 8).Value = Date.Now.ToString("ddMMyyyy")    'Claim Date
                    _ws.Cells(iRow, 15).Value = 1   'Unit
                    SetValidatingResult(dsfLeasingClaim, xlsChassisNumber, xlsEngineNumber, iRow, _ws)
                Else
                    dsfLeasingClaim.Unit = 1
                    _ws.Cells(iRow, 14).Value = "-" 'Customer Name
                    _ws.Cells(iRow, 16).Value = "-"  'Object Lease
                    _ws.Cells(iRow, 17).Value = "-"   'Production Year

                    dsfLeasingClaim.ValidatingRemark = "Nomor Rangka dan Nomor Mesin tidak valid."
                    dsfLeasingClaim.ValidatingResult = KTB.DNet.Domain.DSFLeasingClaim.ValidateResult.NotValid
                    dsfLeasingClaim.ValidatingResultCode = ValidateResultCode.ChassisAndEngineNotValid
                    _ws.Cells(iRow, 2).Value = "-"
                    _ws.Cells(iRow, 3).Value = "-"
                End If
                _ws.Cells(iRow, 28).Value = "0" & CInt(dsfLeasingClaim.ValidatingResultCode).ToString()
                _ws.Cells(iRow, 29).Value = dsfLeasingClaim.ValidatingRemark

                If (list.Count > 0) Then
                    Dim _count = (From i As DSFLeasingClaim In list
                                   Where i.ChassisNumber = xlsChassisNumber AndAlso i.EngineNumber = xlsEngineNumber).FirstOrDefault()

                    If (Not IsNothing(_count)) Then
                        dsfLeasingClaim.ValidatingRemark = dsfLeasingClaim.ValidatingRemark & ". (Uploaded more than 1)"
                        dsfLeasingClaim.ValidatingResult = KTB.DNet.Domain.DSFLeasingClaim.ValidateResult.NotValid
                        _ws.Cells(iRow, 29).Value = dsfLeasingClaim.ValidatingRemark
                    End If
                End If

                list.Add(dsfLeasingClaim)
            Next
            result = True
        Catch ex As Exception
            result = False
        End Try
        Return result
    End Function

    Private Sub SetValidatingResult(DSFLeasingClaim As DSFLeasingClaim, xlsChassisNumber As String, xlsEngineNumber As String, iRow As Integer, _ws As ExcelWorksheet)
        Try
            If (Not IsNothing(DSFLeasingClaim.ChassisMaster)) Then
                If DSFLeasingClaim.ChassisMaster.ChassisNumber = xlsChassisNumber AndAlso DSFLeasingClaim.ChassisMaster.EngineNumber.Replace("-", "") = xlsEngineNumber Then
                    DSFLeasingClaim.CustomerName = DSFLeasingClaim.ChassisMaster.EndCustomer.Name1
                    _ws.Cells(iRow, 14).Value = DSFLeasingClaim.ChassisMaster.EndCustomer.Name1 'Customer Name
                    _ws.Cells(iRow, 16).Value = DSFLeasingClaim.ChassisMaster.VechileColor.MaterialDescription  'Object Lease
                    _ws.Cells(iRow, 17).Value = DSFLeasingClaim.ChassisMaster.ProductionYear   'Production Year

                    DSFLeasingClaim.ObjectLease = DSFLeasingClaim.ChassisMaster.VechileColor.MaterialDescription
                    DSFLeasingClaim.Dealer = DSFLeasingClaim.ChassisMaster.Dealer
                    'If Not IsAlreadyUploaded(xlsChassisNumber, xlsEngineNumber) Then
                    If Not IsAlreadyUploaded(DSFLeasingClaim.ChassisMaster) Then
                        If Not IsAlreadyClaimByDealer(DSFLeasingClaim.ChassisMaster) Then
                            'If Not IsAlreadyClaimByDealer(xlsChassisNumber, xlsEngineNumber) Then
                            DSFLeasingClaim.ValidatingRemark = "OK"
                            DSFLeasingClaim.ValidatingResult = KTB.DNet.Domain.DSFLeasingClaim.ValidateResult.Valid
                            DSFLeasingClaim.ValidatingResultCode = ValidateResultCode.OK
                        Else
                            Dim remark() As String = New DSFLeasingClaimFacade(User).RetrieveUploadRemark(xlsChassisNumber, xlsEngineNumber).ToString().Split(";")
                            DSFLeasingClaim.ValidatingRemark = remark(0)
                            DSFLeasingClaim.ValidatingResult = KTB.DNet.Domain.DSFLeasingClaim.ValidateResult.Valid
                            DSFLeasingClaim.ValidatingResultCode = remark(1)
                        End If
                    Else
                        DSFLeasingClaim.ValidatingRemark = "Claim sudah pernah diupload."
                        DSFLeasingClaim.ValidatingResult = ValidateResult.NotValid
                        DSFLeasingClaim.ValidatingResultCode = ValidateResultCode.ClaimSudahPernahDiupload
                    End If
                    DSFLeasingClaim.Unit = 1
                    _ws.Cells(iRow, 2).Value = DSFLeasingClaim.Dealer.DealerCode
                    _ws.Cells(iRow, 3).Value = DSFLeasingClaim.Dealer.DealerName
                Else
                    DSFLeasingClaim.Unit = 1
                    _ws.Cells(iRow, 14).Value = "-" 'Customer Name
                    _ws.Cells(iRow, 16).Value = "-"  'Object Lease
                    _ws.Cells(iRow, 17).Value = "-"   'Production Year
                    If DSFLeasingClaim.ChassisMaster.ChassisNumber <> xlsChassisNumber AndAlso DSFLeasingClaim.ChassisMaster.EngineNumber.Replace("-", "") <> xlsEngineNumber Then
                        DSFLeasingClaim.ValidatingRemark = "Nomor Rangka dan Nomor Mesin tidak valid."
                        DSFLeasingClaim.ValidatingResult = KTB.DNet.Domain.DSFLeasingClaim.ValidateResult.NotValid
                        DSFLeasingClaim.ValidatingResultCode = ValidateResultCode.ChassisAndEngineNotValid
                        _ws.Cells(iRow, 2).Value = "-"
                        _ws.Cells(iRow, 3).Value = "-"
                    Else
                        If DSFLeasingClaim.ChassisMaster.ChassisNumber <> xlsChassisNumber AndAlso DSFLeasingClaim.ChassisMaster.EngineNumber.Replace("-", "") = xlsEngineNumber Then
                            DSFLeasingClaim.ValidatingRemark = "Nomor Rangka tidak valid."
                            DSFLeasingClaim.ValidatingResult = KTB.DNet.Domain.DSFLeasingClaim.ValidateResult.NotValid
                            DSFLeasingClaim.ValidatingResultCode = ValidateResultCode.ChassisNotValid
                            _ws.Cells(iRow, 2).Value = "-"
                            _ws.Cells(iRow, 3).Value = "-"
                        Else
                            If DSFLeasingClaim.ChassisMaster.ChassisNumber = xlsChassisNumber AndAlso DSFLeasingClaim.ChassisMaster.EngineNumber.Replace("-", "") <> xlsEngineNumber Then
                                DSFLeasingClaim.ObjectLease = DSFLeasingClaim.ChassisMaster.VechileColor.MaterialDescription
                                DSFLeasingClaim.ValidatingRemark = "Nomor Mesin tidak valid."
                                DSFLeasingClaim.ValidatingResult = KTB.DNet.Domain.DSFLeasingClaim.ValidateResult.NotValid
                                DSFLeasingClaim.ValidatingResultCode = ValidateResultCode.EngineNotValid
                                _ws.Cells(iRow, 2).Value = "-"
                                _ws.Cells(iRow, 3).Value = "-"
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function ConvertToDecimal(ByVal valueFor As Object) As Decimal
        Dim result As Decimal
        If IsNumeric(valueFor) Then
            If TypeOf valueFor Is String Then
                result = Convert.ToDecimal(valueFor.ToString(), New CultureInfo("en-US"))
            Else
                If TypeOf valueFor Is Decimal Then
                    result = valueFor
                Else
                    If TypeOf valueFor Is Integer Then
                        result = Convert.ToDecimal(valueFor.ToString(), New CultureInfo("en-US"))
                    End If
                End If
            End If
        End If
        Return result
    End Function

    Private Function IsAlreadyUploaded(ByVal _chassisMaster As ChassisMaster) As Boolean
        'Private Function IsAlreadyUploaded(ByVal chassisno As String, ByVal engineno As String) As Boolean
        Dim result As Boolean = New Boolean
        'Dim dsfLeasingClaim As DSFLeasingClaim = New DSFLeasingClaim
        'Dim chassisMaster As ChassisMaster = New ChassisMaster
        'Dim critChassisMaster As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'critChassisMaster.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, chassisno))
        'critChassisMaster.opAnd(New Criteria(GetType(ChassisMaster), "EngineNumber", MatchType.Exact, engineno))
        'Dim arr As ArrayList = New ChassisMasterFacade(User).Retrieve(critChassisMaster)
        'If arr.Count > 0 Then
        'chassisMaster = CType(arr(0), ChassisMaster)

        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DSFLeasingClaim), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(DSFLeasingClaim), "ChassisMaster.ID", MatchType.Exact, _chassisMaster.ID))
        Dim arrl As ArrayList = New DSFLeasingClaimFacade(User).Retrieve(crit)
        result = arrl.Count > 0

        'End If
        Return result
    End Function

    Private Function IsAlreadyClaimByDealer(ByVal _chassisMaster As ChassisMaster) As Boolean
        'Private Function IsAlreadyClaimByDealer(ByVal chassisno As String, ByVal engineno As String) As Boolean
        Dim result As Boolean = New Boolean

        'Dim chassisMaster As ChassisMaster = New ChassisMaster
        'Dim critChassisMaster As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'critChassisMaster.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, chassisno))
        'critChassisMaster.opAnd(New Criteria(GetType(ChassisMaster), "EngineNumber", MatchType.Exact, engineno))
        'Dim arr As ArrayList = New ChassisMasterFacade(User).Retrieve(critChassisMaster)
        'If arr.Count > 0 Then
        '    chassisMaster = CType(arr(0), ChassisMaster)
        Dim benefitClaimByDealer As BenefitClaimDetails = New BenefitClaimDetails
        Dim critClaimByHeader As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDetails), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critClaimByHeader.opAnd(New Criteria(GetType(BenefitClaimDetails), "ChassisMaster.ID", MatchType.Exact, _chassisMaster.ID))
        Dim arrl As ArrayList = New BenefitClaimDetailsFacade(User).Retrieve(critClaimByHeader)
        result = arrl.Count > 0

        'End If
        Return result
    End Function

    Private Function IsDoubleInput() As Boolean
        Dim b As Boolean
        Return b
    End Function

    Private Sub BindExcelDataToGrid(ByVal pageIndex As Integer)
        Dim dataFor As ArrayList = sessHelper.GetSession("DataForSave")
        dgTable.DataSource = Nothing
        CommonFunction.SortListControl(dataFor, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
        Dim PagedList As ArrayList = ArrayListPager.DoPage(dataFor, pageIndex, dgTable.PageSize)
        dgTable.DataSource = PagedList
        dgTable.VirtualItemCount = dataFor.Count()
        dgTable.DataBind()
        btnDownload.Visible = True
        cbxDisclaimer.Visible = True
    End Sub

    Private Sub ShowUploadSummary()
        Dim dataFor As ArrayList = sessHelper.GetSession("DataForSave")
        Dim dataCount As Integer = dataFor.Count()
        Dim notValidDataCount As Integer = (From i As DSFLeasingClaim In dataFor
                                   Where i.ValidatingResult = ValidateResult.NotValid).Count()
        Dim validCount = dataCount - notValidDataCount
        lblRecordCount.Text = dataCount
        MessageBox.Show("Upload data berhasil. " & "Total " & dataCount.ToString() & " baris. " & "Valid " & validCount.ToString() & " baris. " & "Tidak valid = " & notValidDataCount.ToString() & " baris")
    End Sub

    Protected Sub dgTable_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgTable.ItemDataBound
        'Try
        Dim lblIDoGridDetil As Label = e.Item.FindControl("lblIDoGridDetil")
        Dim lblNoGrid As Label = e.Item.FindControl("lblNoGrid")
        Dim lblDealerNameGrid As Label = e.Item.FindControl("lblDealerNameGrid")
        Dim lblChassisNumberGrid As Label = e.Item.FindControl("lblChassisNumberGrid")
        Dim lblEngineNumberGrid As Label = e.Item.FindControl("lblEngineNumberGrid")
        Dim lblAssetSeqNoGrid As Label = e.Item.FindControl("lblAssetSeqNoGrid")
        Dim lblAgreementNumberGrid As Label = e.Item.FindControl("lblAgreementNumberGrid")
        Dim lblSKDNumberGrid As Label = e.Item.FindControl("lblSKDNumberGrid")
        Dim lblSKDDateGrid As Label = e.Item.FindControl("lblSKDDateGrid")
        Dim lblSKDApprovalDateGrid As Label = e.Item.FindControl("lblSKDApprovalDateGrid")
        Dim lblGoLiveDateGrid As Label = e.Item.FindControl("lblGoLiveDateGrid")
        Dim lblATPMSubsidyGrid As Label = e.Item.FindControl("lblATPMSubsidyGrid")
        Dim lblSupplierNameGrid As Label = e.Item.FindControl("lblSupplierNameGrid")
        Dim lblUnitGrid As Label = e.Item.FindControl("lblUnitGrid")
        Dim lblProgramNameGrid As Label = e.Item.FindControl("lblProgramNameGrid")
        Dim lblCollectionPeriodGrid As Label = e.Item.FindControl("lblCollectionPeriodGrid")
        Dim lblTotalDownPaymentGrid As Label = e.Item.FindControl("lblTotalDownPaymentGrid")
        Dim lblTotalAmountLeaseGrid As Label = e.Item.FindControl("lblTotalAmountLeaseGrid")
        Dim lblPeriodLeaseGrid As Label = e.Item.FindControl("lblPeriodLeaseGrid")
        Dim lblInterestLeaseGrid As Label = e.Item.FindControl("lblInterestLeaseGrid")
        Dim lblInsuranceGrid As Label = e.Item.FindControl("lblInsuranceGrid")
        Dim lblTypeInsuranceGrid As Label = e.Item.FindControl("lblTypeInsuranceGrid")
        Dim lblStatusUpload As Label = e.Item.FindControl("lblStatusUpload")
        Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
        Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As DSFLeasingClaim = CType(e.Item.DataItem, DSFLeasingClaim)
            'lblIDoGridDetil.Text = oData.ID
            lblNoGrid.Text = ((dgTable.PageSize * dgTable.CurrentPageIndex) + e.Item.ItemIndex + 1).ToString()
            lblDealerNameGrid.Text = If(IsNothing(oData.Dealer), "", oData.Dealer.DealerName)
            'lblChassisNumberGrid.Text = If(IsNothing(oData.ChassisMaster), oData.ChassisNumber, oData.ChassisMaster.ChassisNumber)
            'lblEngineNumberGrid.Text = If(IsNothing(oData.ChassisMaster), oData.EngineNumber, oData.ChassisMaster.EngineNumber)

            lblChassisNumberGrid.Text = oData.ChassisNumber
            lblEngineNumberGrid.Text = oData.EngineNumber
            'If Not IsNothing(oData.ChassisMaster) Then
            '    lblChassisNumberGrid.Text = oData.ChassisMaster.ChassisNumber
            'Else
            '    lblChassisNumberGrid.Text = oData.ChassisNumber
            'End If
            'If Not IsNothing(oData.ChassisMaster) Then
            '    lblEngineNumberGrid.Text = oData.ChassisMaster.EngineNumber
            'Else
            '    lblEngineNumberGrid.Text = oData.EngineNumber
            'End If

            lblAssetSeqNoGrid.Text = oData.AssetSeqNo
            lblAgreementNumberGrid.Text = oData.AgreementNo
            lblSKDNumberGrid.Text = oData.SKDNumber
            lblSKDDateGrid.Text = oData.SKDDate
            lblSKDApprovalDateGrid.Text = oData.SKDApprovalDate
            lblGoLiveDateGrid.Text = oData.GoLiveDate
            lblATPMSubsidyGrid.Text = oData.ATPMSubsidy
            lblSupplierNameGrid.Text = oData.SupplierName
            lblUnitGrid.Text = oData.Unit
            lblProgramNameGrid.Text = oData.ProgramName
            lblCollectionPeriodGrid.Text = oData.CollectionPeriod
            lblTotalDownPaymentGrid.Text = oData.TotalDP
            lblTotalAmountLeaseGrid.Text = oData.TotalAmountLease
            lblPeriodLeaseGrid.Text = oData.PeriodLease
            lblInterestLeaseGrid.Text = oData.InterestLease
            lblInsuranceGrid.Text = oData.Insurance
            lblTypeInsuranceGrid.Text = oData.TypeInsurance

            Dim message As String = oData.ValidatingRemark
            If oData.ValidatingResult = ValidateResult.Valid Then
                lblStatusUpload.Attributes("style") = "color: Green"
            Else
                e.Item.BackColor = Color.Red
            End If
            lblStatusUpload.Text = message

            lnkbtnEdit.Visible = False
            lnkbtnDelete.Visible = True

        End If

        'Catch ex As Exception

        'End Try


    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Download()
    End Sub

    Private Sub Download()
        Dim filePath As String = sessHelper.GetSession("fileUpDown")
        'filePath = "FinishUnit" & filePath
        filePath = filePath
        Response.Redirect("../downloadlocal.aspx?file=" & filePath)
    End Sub

    Private Function GetRegNumber(ByVal seq As Integer) As String
        Dim prefix As String = "DSFCL"
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DSFLeasingClaim), "RegNumber", MatchType.StartsWith, prefix))
        crit.opAnd(New Criteria(GetType(DSFLeasingClaim), "CreatedTime", MatchType.GreaterOrEqual, Date.Today.Year & Date.Today.Month.ToString("d2") & "01"))
        crit.opOr(New Criteria(GetType(DSFLeasingClaim), "CreatedTime", MatchType.Lesser, Date.Today.Year & Date.Today.AddMonths(1).Month.ToString("d2") & "01"))
        Dim arrl As ArrayList = New DSFLeasingClaimFacade(User).Retrieve(crit)
        Dim _return As String
        If arrl.Count > 0 Then
            Dim objBH As DSFLeasingClaim = CommonFunction.SortListControl(arrl, "RegNumber", Sort.SortDirection.DESC)(0)
            Dim noReg As String = objBH.RegNumber
            Dim lastNum As Integer = CInt(noReg.Substring(noReg.Length - (noReg.Length - 9)))
            seq = lastNum + seq
            Dim sSeqLength As String = seq.ToString().Length
            Dim numSegment As String = ""
            If sSeqLength = 1 Then
                numSegment = "0000" & seq.ToString()
            ElseIf sSeqLength = 2 Then
                numSegment = "000" & seq.ToString()
            ElseIf sSeqLength = 3 Then
                numSegment = "00" & seq.ToString()
            ElseIf sSeqLength = 4 Then
                numSegment = "0" & seq.ToString()
            ElseIf sSeqLength = 5 Then
                numSegment = "" & seq.ToString()
            End If
            '_return = prefix & Date.Today.ToString("yyMM") & (CInt(noReg.Substring(9, 5)) + seq).ToString("d5")
            _return = prefix & Date.Today.ToString("yyMM") & numSegment
        Else
            Dim sSeqLength As String = seq.ToString().Length
            Dim numSegment As String = ""
            If sSeqLength = 1 Then
                numSegment = "0000" & seq.ToString()
            ElseIf sSeqLength = 2 Then
                numSegment = "000" & seq.ToString()
            ElseIf sSeqLength = 3 Then
                numSegment = "00" & seq.ToString()
            ElseIf sSeqLength = 4 Then
                numSegment = "0" & seq.ToString()
            ElseIf sSeqLength = 5 Then
                numSegment = "" & seq.ToString()
            End If
            _return = prefix & Date.Today.ToString("yyMM") & numSegment
        End If
        Return _return
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim list As ArrayList = New ArrayList
        Dim _result As Integer
        list = sessHelper.GetSession("DataForSave")
        Dim _q_dataValidToSave = New List(Of DSFLeasingClaim)(From i As DSFLeasingClaim In list Where i.ValidatingResult = ValidateResult.Valid Select i)
        Dim dataValidToSave As ArrayList = New ArrayList()
        dataValidToSave.AddRange(_q_dataValidToSave)
        Dim notToSaveCount As Integer = (list.Count() - dataValidToSave.Count())
        Try
            _result = New DSFLeasingClaimFacade(User).InsertTransaction(dataValidToSave)
            If _result > 0 Then
                If dataValidToSave.Count() > 0 Then
                    MessageBox.Show("Data berhasil disimpan. " & "Total tersimpan " & dataValidToSave.Count().ToString() & " baris. " & "Tidak tersimpan " & notToSaveCount & " baris")
                Else
                    MessageBox.Show("Masih terdapat data bermasalah. Data tidak berhasil disimpan.")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Data tidak berhasil disimpan.")
        End Try
    End Sub

    Protected Sub dgTable_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgTable.ItemCommand
        Select Case e.CommandName
            Case "Edit"
            Case "Delete"
                DeleteCommand(e)
            Case "Add"
            Case "View"
            Case "AddKet"
        End Select
    End Sub

    Private Sub DeleteCommand(ByVal e As DataGridCommandEventArgs)
        Dim _list As ArrayList = New ArrayList
        Dim _lblChassisNumberGrid As Label = CType(e.Item.FindControl("lblChassisNumberGrid"), Label)
        Dim _lblEngineNumberGrid As Label = CType(e.Item.FindControl("lblEngineNumberGrid"), Label)
        Dim _itemToDelete As New DSFLeasingClaim

        _list = sessHelper.GetSession("DataForSave")
        For Each item As DSFLeasingClaim In _list
            If _lblChassisNumberGrid.Text.Replace(" ", "") = item.ChassisNumber.Replace(" ", "") _
                And _lblEngineNumberGrid.Text.Replace(" ", "") = item.EngineNumber.Replace(" ", "") Then
                _itemToDelete = item
            End If
            'If _lblChassisNumberGrid.Text.Replace(" ", "") = item.ChassisMaster.ChassisNumber.Replace(" ", "") _
            '    And _lblEngineNumberGrid.Text.Replace(" ", "") = item.ChassisMaster.EngineNumber.Replace(" ", "") Then
            '    _itemToDelete = item
            'End If
        Next
        _list.Remove(_itemToDelete)
        sessHelper.SetSession("DataForSave", _list)
        BindExcelDataToGrid(0)
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        InitDisplayMainGrid(0)
    End Sub

    Protected Sub cbxDisclaimer_CheckedChanged(sender As Object, e As EventArgs)
        btnSave.Visible = If(IsNothing(Request.Form("cbxDisclaimer")), False, simpanPriv)
    End Sub

End Class