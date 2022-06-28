#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
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
Imports System.Linq


#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Reflection

#End Region

Public Class frmAllocationRealTimeService
    Inherits System.Web.UI.Page

#Region " Private fields "
    Dim sessHelp As SessionHelper = New SessionHelper
    Dim InvoiceList As New ArrayList  '-- List of invoice
    Dim _PCAccessAllowed As Boolean = False
    Dim _CVAccessAllowed As Boolean = False
    Dim _LCVAccessAllowed As Boolean = False
    Private objDealer As Dealer
    Dim isDealerDMS As Boolean = False
    Private isDealerPiloting As Boolean = False

#End Region

#Region "Custom Method"
    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.AlocationRealtimeService_View_Privilage) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Service - Allocation Realtime Service")
        End If
    End Sub
    Private Sub InitData()
        'If Session("sessCM") Is Nothing Then
        Dim objDealer As Dealer = CType(sessHelp.GetSession("DEALER"), Dealer)
        Dim objUser As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
        ' Dim mode As String = "add"
        If Not objDealer Is Nothing Then
            'txtKodeDealer.Text = objDealer.DealerCode
            'txtNamaDealer.Text = objDealer.DealerName
            Dim dealerSystems As DealerSystems = New DealerSystemsFacade(User).RetrieveByDealerCode(objDealer.DealerCode)

            If Not IsNothing(dealerSystems) AndAlso dealerSystems.SystemID <> 1 Then
                isDealerDMS = True
            End If

            If (isDealerDMS = True) Then
                btnSave.Visible = False

            End If

            If Not SecurityProvider.Authorize(Context.User, SR.AllocationRealTimeService_Input_Privilage) Then
                btnSave.Visible = False
                btnImport.Visible = False
            End If

            sessHelp.SetSession("isDealerDMS", isDealerDMS)
            isDealerPiloting = TCHelper.GetActiveTCResult(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.PilotingStall))
            sessHelp.SetSession("isDealerPiloting", isDealerPiloting)
        End If

        'bindBodyPaint()
        'bindKategori()
        'Bindlokasi()
        'bindStatus()
        'BindTipe()
        'bindDDLBodiPaint()
        'bindDDLKategori()
        'bindDDLlokasi()
        'bindDDLstatus()
        'bindDDLTipe()
    End Sub
    Private Sub ReadData()
        '-- Read all data selected
        'Dim criterias As New CriteriaComposite()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VWI_AllocationRealTimeService), "ID", MatchType.Greater, 0))

        Dim objDealer As Dealer = CType(sessHelp.GetSession("DEALER"), Dealer)
        Dim objUser As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
        ' Dim mode As String = "add"
        If Not objDealer Is Nothing Then
            If Not objDealer.DealerGroup Is Nothing Then
                criterias.opAnd(New Criteria(GetType(VWI_AllocationRealTimeService), "DealerCode", MatchType.Exact, objDealer.DealerCode))
            End If
            'txtKodeDealer.Text = objDealer.DealerCode
            'txtNamaDealer.Text = objDealer.DealerName

        End If

        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayDate", MatchType.Exact, FakturDate.Day))

        '-- Row status = active
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(AllocationRealTimeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If (txtKodeDealer.Text <> "") Then
            criterias.opAnd(New Criteria(GetType(VWI_AllocationRealTimeService), "DealerCode", MatchType.Exact, txtKodeDealer.Text))
        End If

        'If txtAlokasiStall.Text.Trim() <> "" Then
        '    criterias.opAnd(New Criteria(GetType(AllocationRealTimeService), "AllokasiStall", MatchType.Exact, txtAlokasiStall.Text.Trim()))
        'End If

        'If (txtCurrent.Text <> "") Then
        '    criterias.opAnd(New Criteria(GetType(AllocationRealTimeService), "CurrentStall", MatchType.Exact, txtCurrent.Text.Trim()))
        'End If

        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

        'Dim arrAllocationRealTimeService As ArrayList = New AllocationRealTimeServiceFacade(User).RetrieveByCriteria(criterias, 1, 25, 25)
        Dim AllocationRealTimeService As List(Of VWI_AllocationRealTimeService) = New KTB.DNet.BusinessFacade.Service.VWI_AllocationRealTimeServiceFacade(User).RetrieveByCriteria(criterias).Cast(Of VWI_AllocationRealTimeService).ToList
        Dim arrAllocationRealTimeService = New ArrayList
        For Each objs As VWI_AllocationRealTimeService In AllocationRealTimeService
            '.Add(New ListItem(obj.ID & " - " & obj.Name, obj.ID))
            'objARS.ID = objs.ID
            'objARS.DealerCode = objs.DealerCode
            'objARS.AlokasiStall = objs.AlokasiStall
            'objARS.CurrentStall = objs.CurrentStall
            'For Each dr As DataRow In tbFinalObject.Rows
            '    objCostList.add(dr)
            'Next
            arrAllocationRealTimeService.add(objs)
        Next
        '-- Store InvoiceReqList into session for later use
        sessHelp.SetSession("AllocationRealTimeServiceList", arrAllocationRealTimeService)

    End Sub
    Private Sub BindPage(ByVal pageIndex As Integer)
        Dim arrAllocationRealTimeService As ArrayList = CType(sessHelp.GetSession("AllocationRealTimeServiceList"), ArrayList)

        Dim aStatus As New ArrayList
        If arrAllocationRealTimeService.Count <> 0 Then
            ' SortListControl(arrAllocationRealTimeService, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrAllocationRealTimeService, pageIndex, dtgStallMaster.PageSize)
            dtgStallMaster.DataSource = PagedList
            dtgStallMaster.VirtualItemCount = arrAllocationRealTimeService.Count()
            dtgStallMaster.DataBind()
        Else
            dtgStallMaster.DataSource = New ArrayList
            dtgStallMaster.VirtualItemCount = 0
            dtgStallMaster.CurrentPageIndex = 0
            dtgStallMaster.DataBind()
        End If
    End Sub
    Private Sub ClearData()
        lblSearchDealer.Visible = True
        HiddenField1.Value = String.Empty
        HiddenField2.Value = String.Empty
        txtKodeDealer.Text = String.Empty
        txtNamaDealer.Text = String.Empty
        txtAlokasiStall.Text = String.Empty '"Auto Generate"
        txtCurrent.Text = "[Auto Generate]"
        ReadData()
        dtgStallMaster.CurrentPageIndex = 0
        BindPage(dtgStallMaster.CurrentPageIndex)
        ViewState.Add("vsProcess", "Insert")
    End Sub
    Private Function InsertModel() As Integer
        Dim objAllocation As AllocationRealTimeService = New AllocationRealTimeService
        Dim nResult As Integer
        'objAllocation.ID = HiddenField3.Value
        objAllocation.Dealer = New DealerFacade(User).GetDealer(txtKodeDealer.Text)
        '        objStallMaster.Dealer.DealerCode = txtKodeDealer.Text
        objAllocation.AlokasiStall = txtAlokasiStall.Text
        'objAllocation.CurrentStall = 0
        objAllocation.RowStatus = 0
        nResult = New AllocationRealTimeServiceFacade(User).Insert(objAllocation)
        If nResult = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            MessageBox.Show(SR.SaveSuccess)
        End If
        Return nResult
    End Function
    Private Function UpdateModel() As Integer
        Dim objAllocation As AllocationRealTimeService = New AllocationRealTimeService
        'Dim nResult As Integer
        objAllocation.ID = HiddenField3.Value
        objAllocation.Dealer = New DealerFacade(User).GetDealer(txtKodeDealer.Text)
        '        objStallMaster.Dealer.DealerCode = txtKodeDealer.Text
        objAllocation.AlokasiStall = txtAlokasiStall.Text
        'objAllocation.CurrentStall = txtCurrent.Text 'objAllocation.CurrentStall
        objAllocation.RowStatus = 0
        Dim nResult = New AllocationRealTimeServiceFacade(User).Update(objAllocation)
        If nResult = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            MessageBox.Show(SR.SaveSuccess)
        End If
        Return nResult
    End Function
#End Region

#Region "Event Handlers"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        If Not IsPostBack Then
            ActivateUserPrivilege()
            ClearData()
            hdnMCPConfirmation.Value = -1
            'ActivateUserPrivilege()
            InitData()
            'ViewState.Add("vsProcess", "Insert")
            ReadData()
            dtgStallMaster.CurrentPageIndex = 0
            BindPage(dtgStallMaster.CurrentPageIndex)
        End If
        txtKodeDealer.Text = HiddenField1.Value
        txtNamaDealer.Text = HiddenField2.Value
    End Sub
    Private Sub dtgStallMaster_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgStallMaster.ItemCommand
        If e.CommandName = "lnkDetail" Then
            lblSearchDealer.Visible = False
            ViewState.Add("vsProcess", "Edit")
            Dim lbNo As Label = e.Item.FindControl("lbNo")
            HiddenField3.Value = e.Item.Cells(0).Text
            Dim lbKodeDealer As Label = e.Item.FindControl("lbKodeDealer")
            'Dim lbKodeDealer As Label = e.Item.FindControl("lbKodeDealer")
            Dim lbAlokasiStall As Label = e.Item.FindControl("lbAlokasiStall")
            Dim lbCurrentStall As Label = e.Item.FindControl("lbCurrentStall")
            Dim lnkDetail As LinkButton = e.Item.FindControl("lnkDetail")

            txtKodeDealer.Text = lbKodeDealer.Text
            HiddenField1.Value = lbKodeDealer.Text
            txtAlokasiStall.Text = lbAlokasiStall.Text
            txtCurrent.Text = lbCurrentStall.Text

            Dim objDealer As Dealer = New Dealer
            objDealer = New DealerFacade(User).GetDealer(txtKodeDealer.Text)

            txtNamaDealer.Text = objDealer.DealerName
            HiddenField2.Value = objDealer.DealerName
            lnkDetail.Visible = True
            'If (isDealerDMS = True) Then
            '    lnkDetail.Visible = False
            'End If

        End If
    End Sub
    Private Sub dtgStallMaster_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgStallMaster.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lbNo"), Label)
            Dim lnkDetail As LinkButton = CType(e.Item.FindControl("lnkDetail"), LinkButton)
            Dim lbKodeDealer As Label = CType(e.Item.FindControl("lbKodeDealer"), Label)
            Dim lbAlokasiStall As Label = e.Item.FindControl("lbAlokasiStall")
            Dim lbCurrentStall As Label = e.Item.FindControl("lbCurrentStall")

            lblNo.Text = (dtgStallMaster.CurrentPageIndex * dtgStallMaster.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No

            Dim objDealer As Dealer = CType(sessHelp.GetSession("DEALER"), Dealer)
            Dim objUser As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)

            lnkDetail.Visible = True
            'Dim dealerSystems As DealerSystems = New DealerSystemsFacade(User).RetrieveByDealerCode(objDealer.DealerCode)

            'If Not IsNothing(dealerSystems) AndAlso dealerSystems.SystemID <> 1 Then
            '    lnkDetail.Visible = False
            'Else
            '    If Not objDealer Is Nothing Then
            '        If (objDealer.DealerCode = lbKodeDealer.Text) Then
            '            lnkDetail.Visible = True
            '        Else
            '            lnkDetail.Visible = False
            '        End If
            '    End If
            'End If

        End If
    End Sub
    Private Sub dtgStallMaster_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgStallMaster.PageIndexChanged
        '-- Change datagrid page

        dtgStallMaster.CurrentPageIndex = e.NewPageIndex
        BindPage(e.NewPageIndex)

    End Sub
    Protected Sub btnDownload_Click(sender As Object, e As EventArgs)
        Dim arrStallMaster As ArrayList = CType(sessHelp.GetSession("AllocationRealTimeServiceList"), ArrayList)
        'Dim aStatus As New ArrayList
        If arrStallMaster.Count <> 0 Then
            '   DoDownload(arrStallMaster)
            SetDownload()
        End If
    End Sub
    Protected Sub LnkDownloadTemplate_Click(sender As Object, e As EventArgs) Handles LnkDownloadTemplate.Click
        Dim arrServiceStandard As ArrayList = CType(sessHelp.GetSession("AllocationRealTimeServiceList"), ArrayList)
            'Dim aStatus As New ArrayList
            If arrServiceStandard.Count <> 0 Then
                '   DoDownload(arrStallMaster)
                SetDownload()
            Else
                'MessageBox.Show("Belum ada data untuk pencarian ini.")
                SetDownload()

            End If
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        If Not Page.IsValid Then
            Return
        End If

        txtKodeDealer.Text = HiddenField1.Value
        txtNamaDealer.Text = HiddenField2.Value
        If (txtKodeDealer.Text = "" Or txtAlokasiStall.Text = "") Then
            MessageBox.Show("Semua Data Wajib Diisi.")
            Return
        End If

        If (CType(ViewState("vsProcess"), String) = "Edit") Then
            If CInt(txtCurrent.Text) > CInt(txtAlokasiStall.Text) Then
                If CInt(txtAlokasiStall.Text) <> 0 Then
                    MessageBox.Show("Alokasi Stall tidak boleh lebih kecil dari Current Stall.")
                    Return
                Else
                    UpdateModel()
                End If
            Else
                UpdateModel()
            End If
        Else
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VWI_AllocationRealTimeService), "DealerCode", MatchType.Exact, txtKodeDealer.Text))
            'criterias.opAnd(New Criteria(GetType(VWI_AllocationRealTimeService), "DealerCode", MatchType.Exact, txtKodeDealer.Text))
            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

            Dim AllocationRealTimeService As List(Of VWI_AllocationRealTimeService) = New KTB.DNet.BusinessFacade.Service.VWI_AllocationRealTimeServiceFacade(User).RetrieveByCriteria(criterias).Cast(Of VWI_AllocationRealTimeService).ToList
            Dim arrAlloc = New ArrayList
            For Each objs As VWI_AllocationRealTimeService In AllocationRealTimeService
                '.Add(New ListItem(obj.ID & " - " & obj.Name, obj.ID))
                'objARS.ID = objs.ID
                'objARS.DealerCode = objs.DealerCode
                'objARS.AlokasiStall = objs.AlokasiStall
                'objARS.CurrentStall = objs.CurrentStall
                'For Each dr As DataRow In tbFinalObject.Rows
                '    objCostList.add(dr)
                'Next
                arrAlloc.Add(objs)
            Next

            If (arrAlloc.Count <> 0) Then
                Dim objAlloc As New VWI_AllocationRealTimeService
                objAlloc = CType(arrAlloc(0), VWI_AllocationRealTimeService)
                If objAlloc.ID > 0 Then
                    If CInt(objAlloc.CurrentStall) > CInt(txtAlokasiStall.Text) Then
                        If CInt(txtAlokasiStall.Text) <> 0 Then
                            MessageBox.Show("Alokasi Stall tidak boleh lebih kecil dari Current Stall.")
                            Return
                        Else
                            txtCurrent.Text = objAlloc.CurrentStall
                            HiddenField3.Value = objAlloc.ID
                            UpdateModel()
                        End If
                    Else
                        txtCurrent.Text = objAlloc.CurrentStall
                        HiddenField3.Value = objAlloc.ID
                        UpdateModel()
                    End If
                Else
                    If CInt(objAlloc.CurrentStall) > CInt(txtAlokasiStall.Text) Then
                        If CInt(txtAlokasiStall.Text <> 0) Then
                            MessageBox.Show("Alokasi Stall tidak boleh lebih kecil dari Current Stall.")
                            Return
                        Else
                            InsertModel()
                        End If
                    Else
                        InsertModel()
                    End If
                End If
            Else
                InsertModel()
            End If
        End If
        ClearData()
    End Sub
    Protected Sub btnCari_Click(sender As Object, e As EventArgs)
        'If txtKodeDealer.Text <> "" Then
        ReadData()
        dtgStallMaster.CurrentPageIndex = 0
        BindPage(dtgStallMaster.CurrentPageIndex)
        'Else
        'MessageBox.Show("Pilih Kode Dealer terlebih dahulu.")
        'Return
        'End If
    End Sub
    Protected Sub btnBatal_Click(sender As Object, e As EventArgs)
        ClearData()
    End Sub
    Protected Sub btnImport_Click(sender As Object, e As EventArgs)
        RegisterStartupScript("Open", String.Format("<script>ShowPopUpUpload();</script>"))
        Return
    End Sub
    Protected Sub HiddenField1_ValueChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub txtKodeDealer_TextChanged(sender As Object, e As EventArgs)
        If (txtKodeDealer.Text <> "") Then
            Dim objAllocate As AllocationRealTimeService = New AllocationRealTimeService
            objAllocate.Dealer = New DealerFacade(User).GetDealer(txtKodeDealer.Text)
            txtNamaDealer.Text = objAllocate.Dealer.DealerName
        End If
    End Sub
#End Region

#Region "download excel"
    Private Sub SetDownload()
        Dim arrData As New DataTable
        Dim crits As CriteriaComposite
        If dtgStallMaster.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        'If Not IsNothing(sessHelp.GetSession("criteriadownload")) Then
        '    crits = CType(sessHelp.GetSession("criteriadownload"), CriteriaComposite)
        'End If
        ' mengambil data yang dibutuhkan
        Dim arrServiceBooking As ArrayList = CType(sessHelp.GetSession("AllocationRealTimeServiceList"), ArrayList)
        Dim propertiesinfo As PropertyInfo() = arrServiceBooking(0).GetType().GetProperties()

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

        If arrServiceBooking.Count > 0 Then
            CreateExcel("AllocationRealTimeService", arrServiceBooking)
        End If

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

            ws.Cells("A1").Value = FileName
            ws.Cells("A1").Value = "No"
            ws.Cells("B1").Value = "Kode Dealer"
            ws.Cells("C1").Value = "Alokasi Stall"
            ws.Cells("D1").Value = "Current Stall"

            Dim idx As Integer = 0
            For i As Integer = 0 To Data.Count - 1
                Dim item As VWI_AllocationRealTimeService = Data(i)

                ws.Cells(idx + 2, 1).Value = idx + 1
                ws.Cells(idx + 2, 2).Value = item.DealerCode
                ws.Cells(idx + 2, 3).Value = item.AlokasiStall

                ws.Cells(idx + 2, 4).Value = item.CurrentStall
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

    
End Class