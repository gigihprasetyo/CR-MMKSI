#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
Imports System.Reflection
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Helper

Imports OfficeOpenXml
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Public Class FrmReportSPKSalesEventNasional
    Inherits System.Web.UI.Page

#Region "Custom Variables Declaration"

    Dim sesHelper As New SessionHelper
    Dim objDealer As New Dealer
    Dim objSPKNationalEvent As SPKNationalEvent

    Const sessNationalEvent As String = "sessDataNationalEvent"
    Const sessReportSPKNationalEvent As String = "sessDataReportSPKNationalEvent"
    Const sessDeleteSPKNationalEvent As String = "sessDeleteDataSPKNationalEvent"

    Private arlReportSPKNationalEvent As ArrayList = New ArrayList
    Private intItemIndex As Integer = 0
    Private TargetDirectory As String = ""

    Dim IsDealer As Boolean = False
    Dim Mode As String = "New"

    Private Property SesDealer() As Dealer
        Get
            Return CType(sesHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sesHelper.SetSession("DEALER", Value)
        End Set
    End Property
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.EVENTNASIONAL_ReportSPKStatus_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT NASIONAL - REPORT SPK STATUS")
        End If
    End Sub
#End Region

#Region "Custom Methods"
    Private Sub BindddlProspekSPK()
        ddlProspekSPK.Items.Clear()
        ddlProspekSPK.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        ddlProspekSPK.Items.Add(New ListItem("SPK Sales", "0"))
        ddlProspekSPK.Items.Add(New ListItem("Prospect Customer", "1"))
        ddlProspekSPK.SelectedIndex = 0
    End Sub

    Private Sub LoadDataNationalEvent(strRegNumber As String)
        Dim intDealerID As Integer = 0
        Dim objNationalEvent As NationalEvent = New NationalEventFacade(User).Retrieve(strRegNumber)
        If Not IsNothing(objNationalEvent) AndAlso objNationalEvent.ID > 0 Then
            sesHelper.SetSession(sessNationalEvent, objNationalEvent)

            Me.lblEventName.Text = objNationalEvent.RegNumber & " - " & objNationalEvent.NationalEventType.Name & " " & objNationalEvent.NationalEventType.Description
            Me.lblPeriodStartEvent.Text = objNationalEvent.PeriodStart.ToString("dd-MM-yyyy")
            Me.lblPeriodEndEvent.Text = objNationalEvent.PeriodEnd.ToString("dd-MM-yyyy")
            Me.lblCityVenue.Text = objNationalEvent.NationalEventCity.City.CityName & " / " & objNationalEvent.NationalEventVenue.VenueName

            If SesDealer().TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString() Then
                intDealerID = SesDealer().ID
            End If

            Dim dealerCodeChampion As String = 0
            Dim dealerCodeWithlowestDO As String = 0

            arlReportSPKNationalEvent = New SPKNationalEventFacade(User).RetrieveReportSPKNationalEventBySP(intDealerID, objNationalEvent.RegNumber, dealerCodeChampion, dealerCodeWithlowestDO)
            sesHelper.SetSession(sessReportSPKNationalEvent, arlReportSPKNationalEvent)
            BindGridReportSPKNationalEvent(0)

            Dim intCountSPKwithFakturDate As Integer = 0
            Dim intCountSPKwithOutFakturDate As Integer = 0
            For Each _oReportSPKNationalEvent As SPKNationalEventReport In arlReportSPKNationalEvent
                If _oReportSPKNationalEvent.FakturDate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                    intCountSPKwithFakturDate += 1
                Else
                    intCountSPKwithOutFakturDate += 1
                End If
            Next
            Me.lblSPKsudahDO.Text = intCountSPKwithFakturDate
            Me.lblSPKOutstanding.Text = intCountSPKwithOutFakturDate
            Me.lblDealerChampion.Text = dealerCodeChampion
            Me.lblDealerwithlowestDO.Text = dealerCodeWithlowestDO

            Dim listNationalEventDetail As List(Of NationalEventDetail) = objNationalEvent.NationalEventDetails.Cast(Of NationalEventDetail)().ToList()
            Dim listDealers As List(Of String) = listNationalEventDetail.Select(Function(i) i.Dealer.DealerCode).GroupBy(Function(g) g).Select(Function(x) x.Key).ToList()
            Me.lblCountDealer.Text = listDealers.Count

            Dim arlSalesmanIDFinal As Dictionary(Of String, String) = New Dictionary(Of String, String)
            For Each _oNationalEventDetail As NationalEventDetail In objNationalEvent.NationalEventDetails
                Dim arlSalesmanID As String() = _oNationalEventDetail.SalesmanID.Split(";")
                If arlSalesmanID.Count > 0 Then
                    For Each _salesID As String In arlSalesmanID
                        If Not arlSalesmanIDFinal.ContainsKey(_salesID) Then
                            arlSalesmanIDFinal.Add(_salesID, _salesID)
                        End If
                    Next
                End If
            Next
            Me.lblCountSales.Text = arlSalesmanIDFinal.Count

            Dim ArrReportSPKByMKS As ArrayList = New System.Collections.ArrayList(
                                                    (From obj As SPKNationalEventReport In arlReportSPKNationalEvent.OfType(Of SPKNationalEventReport)()
                                                           Where obj.TypeInputSPK.Contains("BYMKS")
                                                            Select obj).ToList())
            txtSPKbyPromDept.Text = ArrReportSPKByMKS.Count

            'Dim intCountSPKEvent As Integer = 0
            'For Each objSPKEvent As SPKNationalEvent In objNationalEvent.SPKNationalEvents
            '    Dim criterias5 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '    criterias5.opAnd(New Criteria(GetType(SPKDetail), "SPKHeader.DealerSPKNumber", MatchType.Exact, objSPKEvent.SPKNumber))
            '    criterias5.opAnd(New Criteria(GetType(SPKDetail), "SPKHeader.SalesmanHeader.ID", MatchType.Exact, objSPKEvent.SalesmanHeader.ID))
            '    criterias5.opAnd(New Criteria(GetType(SPKDetail), "SPKHeader.Dealer.ID", MatchType.Exact, objSPKEvent.Dealer.ID))
            '    criterias5.opAnd(New Criteria(GetType(SPKDetail), "VechileColor.ID", MatchType.Exact, objSPKEvent.VechileColor.ID))
            '    Dim arlSPKDetail As ArrayList = New SPKDetailFacade(User).Retrieve(criterias5)
            '    If Not IsNothing(arlSPKDetail) AndAlso arlSPKDetail.Count > 0 Then
            '        intCountSPKEvent += arlSPKDetail.Count
            '    End If
            'Next

            Dim ArrReportSPKByDealer As ArrayList = New System.Collections.ArrayList(
                                                    (From obj As SPKNationalEventReport In arlReportSPKNationalEvent.OfType(Of SPKNationalEventReport)()
                                                           Where obj.TypeInputSPK.Contains("BYDEALER")
                                                            Select obj).ToList())
            lblSPKbyDealer.Text = ArrReportSPKByDealer.Count

            Me.lblTargetProspek.Text = objNationalEvent.TargetProspect
            Me.lblTargetSPK.Text = objNationalEvent.TargetSPK
            Me.lblRealisasiSPK.Text = ArrReportSPKByDealer.Count + ArrReportSPKByMKS.Count
        Else
            MessageBox.Show("Kode Event tidak terdaftar.")
            ClearAll()
        End If
    End Sub

    Private Sub ClearAll()
        txtRegNumber.Text = ""
        lblEventName.Text = ""
        lblPeriodStartEvent.Text = ""
        lblPeriodEndEvent.Text = ""
        lblCityVenue.Text = ""
        lblCountDealer.Text = "0"
        lblCountSales.Text = "0"
        txtSPKbyPromDept.Text = "0"
        lblSPKbyDealer.Text = "0"
        lblTargetProspek.Text = "0"
        lblTargetSPK.Text = "0"
        lblRealisasiSPK.Text = "0"
        lblSPKsudahDO.Text = "0"
        lblDealerChampion.Text = "0"
        lblDealerwithlowestDO.Text = "0"
        lblSPKOutstanding.Text = "0"

        sesHelper.SetSession(sessReportSPKNationalEvent, New ArrayList)
        BindGridReportSPKNationalEvent(0)
    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Sub BindGridReportSPKNationalEvent(ByVal pageIndex As Integer)
        arlReportSPKNationalEvent = CType(sesHelper.GetSession(sessReportSPKNationalEvent), ArrayList)
        If IsNothing(arlReportSPKNationalEvent) Then arlReportSPKNationalEvent = New ArrayList()

        If arlReportSPKNationalEvent.Count <> 0 Then
            CommonFunction.SortListControl(arlReportSPKNationalEvent, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arlReportSPKNationalEvent, pageIndex, dgReportSPKNationalEvent.PageSize)
            dgReportSPKNationalEvent.DataSource = PagedList
            dgReportSPKNationalEvent.VirtualItemCount = arlReportSPKNationalEvent.Count()
            dgReportSPKNationalEvent.DataBind()
        Else
            dgReportSPKNationalEvent.DataSource = New ArrayList
            dgReportSPKNationalEvent.VirtualItemCount = 0
            dgReportSPKNationalEvent.CurrentPageIndex = 0
            dgReportSPKNationalEvent.DataBind()
        End If
    End Sub

    Private Sub CreateExcelReportSPKNationalEvent(ByVal Data As ArrayList)
        Dim objNationalEvent As NationalEvent = New NationalEventFacade(User).Retrieve(txtRegNumber.Text.Trim)

        Dim templateFile As String = Server.MapPath("~\DataFile\Template\Babit\TemplateReportSPKNationalEvent.xlsx")
        'Dim fileExtention As String = System.IO.Path.GetExtension(templateFile)
        Dim NewFileName As String = "Report_" & objNationalEvent.NationalEventType.Name & " " & objNationalEvent.NationalEventCity.City.CityName & "_" & lblPeriodStartEvent.Text & " s.d " & lblPeriodEndEvent.Text & ".xlsx"
        'Dim strFileName As String = NewFileName
        'Dim strDestFile As String = "NationalEvent\ReportSPKNationalEvent\" & NewFileName
        'Dim NewFileCopy As String = TargetDirectory & strDestFile

        'If System.IO.File.Exists(templateFile) = False Then Exit Sub
        'If Not System.IO.Directory.Exists(NewFileCopy) Then
        '    System.IO.Directory.CreateDirectory(Path.GetDirectoryName(NewFileCopy))
        'End If
        'System.IO.File.Copy(templateFile, NewFileCopy)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then
                Dim fileInfo As FileInfo = New FileInfo(templateFile)

                Dim LF As Char = Chr(10)
                Dim CR As Char = Chr(13)
                Using pck As ExcelPackage = New ExcelPackage(fileInfo)
                    'Using pck As ExcelPackage = New ExcelPackage(templateFile)
                    Dim ws As ExcelWorksheet = pck.Workbook.Worksheets(1)

                    ws.Cells("C3").Value = lblEventName.Text
                    ws.Cells("C4").Value = lblPeriodStartEvent.Text & " s.d " & lblPeriodEndEvent.Text
                    ws.Cells("C5").Value = lblCityVenue.Text
                    ws.Cells("C6").Value = lblCountDealer.Text
                    ws.Cells("C7").Value = lblCountSales.Text
                    ws.Cells("C8").Value = txtSPKbyPromDept.Text
                    ws.Cells("C9").Value = lblSPKbyDealer.Text

                    ws.Cells("G3").Value = lblTargetProspek.Text
                    ws.Cells("G4").Value = lblTargetSPK.Text
                    ws.Cells("G5").Value = lblRealisasiSPK.Text
                    ws.Cells("G6").Value = lblSPKsudahDO.Text
                    ws.Cells("G7").Value = lblSPKOutstanding.Text
                    ws.Cells("G8").Value = lblDealerChampion.Text
                    ws.Cells("G9").Value = lblDealerwithlowestDO.Text

                    Dim idxRow As Integer = 1
                    Dim cellsfrom As String = String.Empty
                    Dim cellsto As String = String.Empty
                    If Not IsNothing(Data) AndAlso Data.Count > 0 Then
                        For i As Integer = 1 To Data.Count - 1
                            ws.InsertRow(14, 1)
                            cellsfrom = "A" & (i + 14).ToString & ":Z" & (i + 14).ToString
                            cellsto = "A14:Z14"
                            ws.Cells(cellsfrom).Copy(ws.Cells(cellsto))
                            If idxRow > 1 Then
                                ws.Cells(cellsto).Value = ""
                            End If
                            idxRow += 1
                        Next
                    End If

                    For i As Integer = 0 To Data.Count - 1
                        Dim item As SPKNationalEventReport = Data(i)

                        ws.Cells(i + 14, 1).Value = i + 1
                        ws.Cells(i + 14, 2).Value = item.SPKNumber
                        ws.Cells(i + 14, 3).Value = item.DealerSPKDate.ToString("dd/MM/yyyy")
                        ws.Cells(i + 14, 4).Value = item.CustomerName
                        ws.Cells(i + 14, 5).Value = New DealerFacade(User).Retrieve(item.DealerCode).City.CityName
                        ws.Cells(i + 14, 6).Value = New DealerFacade(User).Retrieve(item.DealerCode).DealerGroup.GroupName
                        ws.Cells(i + 14, 7).Value = item.DealerName
                        ws.Cells(i + 14, 9).Value = item.DealerCode
                        ws.Cells(i + 14, 10).Value = item.SalesName
                        ws.Cells(i + 14, 11).Value = item.SalesCode
                        ws.Cells(i + 14, 12).Value = If(item.Shift = 0, "", item.Shift)
                        ws.Cells(i + 14, 13).Value = item.VehicleTypeCategory
                        ws.Cells(i + 14, 14).Value = item.VehicleTypeName
                        ws.Cells(i + 14, 15).Value = item.VechileColorName
                        ws.Cells(i + 14, 16).Value = item.AssyYear
                        ws.Cells(i + 14, 17).Value = If(item.FakturDate.ToString("dd/MM/yyyy") = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime).ToString("dd/MM/yyyy"), "", item.FakturDate.ToString("dd/MM/yyyy"))
                        ws.Cells(i + 14, 18).Value = item.FakturNumber
                        ws.Cells(i + 14, 19).Value = item.Quantity.ToString
                        ws.Cells(i + 14, 20).Value = Format(item.DownPayment, "#,##0")
                        ws.Cells(i + 14, 21).Value = item.PaymentMethod
                        ws.Cells(i + 14, 22).Value = item.LeasingName
                        ws.Cells(i + 14, 23).Value = item.Remarks
                        ws.Cells(i + 14, 24).Value = item.TypeInputSPK
                    Next

                    Dim wsDetail As ExcelWorksheet = pck.Workbook.Worksheets(2)
                    Dim ArrReportSPKByDealer As ArrayList = New System.Collections.ArrayList(
                                                            (From obj As SPKNationalEventReport In Data.OfType(Of SPKNationalEventReport)()
                                                                   Where obj.TypeInputSPK.Contains("BYDEALER")
                                                                    Select obj).ToList())

                    idxRow = 1
                    cellsfrom = String.Empty
                    cellsto = String.Empty
                    If Not IsNothing(ArrReportSPKByDealer) AndAlso ArrReportSPKByDealer.Count > 0 Then
                        For i As Integer = 1 To ArrReportSPKByDealer.Count - 1
                            wsDetail.InsertRow(2, 1)
                            cellsfrom = "A" & (i + 2).ToString & ":Z" & (i + 2).ToString
                            cellsto = "A2:Z2"
                            wsDetail.Cells(cellsfrom).Copy(wsDetail.Cells(cellsto))
                            If idxRow > 1 Then
                                wsDetail.Cells(cellsto).Value = ""
                            End If
                            idxRow += 1
                        Next
                    End If

                    For i As Integer = 0 To ArrReportSPKByDealer.Count - 1
                        Dim item As SPKNationalEventReport = ArrReportSPKByDealer(i)

                        wsDetail.Cells(i + 2, 1).Value = i + 1
                        wsDetail.Cells(i + 2, 2).Value = item.SPKNumber
                        wsDetail.Cells(i + 2, 3).Value = item.DealerSPKDate.ToString("dd/MM/yyyy")
                        wsDetail.Cells(i + 2, 4).Value = item.CustomerName
                        wsDetail.Cells(i + 2, 5).Value = New DealerFacade(User).Retrieve(item.DealerCode).City.CityName
                        wsDetail.Cells(i + 2, 6).Value = New DealerFacade(User).Retrieve(item.DealerCode).DealerGroup.GroupName
                        wsDetail.Cells(i + 2, 7).Value = item.DealerName
                        wsDetail.Cells(i + 2, 8).Value = item.DealerCode
                        wsDetail.Cells(i + 2, 9).Value = item.SalesName
                        wsDetail.Cells(i + 2, 10).Value = item.SalesCode
                        wsDetail.Cells(i + 2, 11).Value = If(item.Shift = 0, "", item.Shift)
                        wsDetail.Cells(i + 2, 12).Value = item.VehicleTypeCategory
                        wsDetail.Cells(i + 2, 13).Value = item.VehicleTypeName
                        wsDetail.Cells(i + 2, 14).Value = item.VechileColorName
                        wsDetail.Cells(i + 2, 15).Value = item.AssyYear
                        wsDetail.Cells(i + 2, 16).Value = If(item.FakturDate.ToString("dd/MM/yyyy") = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime).ToString("dd/MM/yyyy"), "", item.FakturDate.ToString("dd/MM/yyyy"))
                        wsDetail.Cells(i + 2, 17).Value = item.FakturNumber
                        wsDetail.Cells(i + 2, 18).Value = item.Quantity.ToString
                        wsDetail.Cells(i + 2, 19).Value = Format(item.DownPayment, "#,##0")
                        wsDetail.Cells(i + 2, 20).Value = item.PaymentMethod
                        wsDetail.Cells(i + 2, 21).Value = item.LeasingName
                        wsDetail.Cells(i + 2, 22).Value = item.Remarks
                        wsDetail.Cells(i + 2, 23).Value = item.TypeInputSPK
                    Next

                    'pck.Save()
                    CreateExcelFile(pck, NewFileName)
                End Using

                imp.StopImpersonate()
                imp = Nothing

            End If
        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
        'Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & strDestFile)
    End Sub

    Private Sub CreateExcelFile(pck As ExcelPackage, fileName As String)
        Dim fileBytes = pck.GetAsByteArray()
        Response.Clear()

        'Response.AppendHeader("Content-Length", fileBytes.Length.ToString())
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName)
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" 'xls
        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()

    End Sub
#End Region

#Region "Event Handlers"

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        TargetDirectory = Server.MapPath("") + "\..\DataTemp\"
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If (Not IsPostBack) Then
            InitiateAuthorization()
            ViewState("currSortColumn") = "SPKNumber"
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            ClearAll()
            BindddlProspekSPK()
            lblPopUpRegNumberEvent.Attributes("onclick") = "ShowPopUpNationalEvent()"
        End If
    End Sub

    Private Sub dgReportSPKNationalEvent_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgReportSPKNationalEvent.ItemCommand
        Dim objSPKNationalEventReport As New SPKNationalEventReport
        Dim objSPKNationalEvent As New SPKNationalEvent
        Dim icDealerSPKDate As KTB.DNet.WebCC.IntiCalendar
        Dim txtECustomerName As TextBox
        Dim txtEShift As TextBox
        Dim txtEAssyYear As TextBox
        Dim txtEQuantity As TextBox
        Dim txtEDownPayment As TextBox
        Dim ddlEPaymentMethod As DropDownList
        Dim txtELeasingName As TextBox
        Dim hfELeasingID As TextBox
        Dim txtERemarks As TextBox

        Dim arlReportSPKNationalEvent As ArrayList = CType(sesHelper.GetSession(sessReportSPKNationalEvent), ArrayList)
        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            objSPKNationalEventReport = CType(arlReportSPKNationalEvent(e.Item.ItemIndex), SPKNationalEventReport)
            If Not IsNothing(objSPKNationalEventReport) AndAlso objSPKNationalEventReport.ID > 0 Then
                objSPKNationalEvent = New SPKNationalEventFacade(User).Retrieve(CType(objSPKNationalEventReport.SPKNationalEventID, Integer))
            End If
        End If

        Select Case e.CommandName
            Case "save" 'Update this datagrid item   
                icDealerSPKDate = CType(e.Item.FindControl("icDealerSPKDate"), KTB.DNet.WebCC.IntiCalendar)
                txtECustomerName = CType(e.Item.FindControl("txtECustomerName"), TextBox)
                txtEShift = CType(e.Item.FindControl("txtEShift"), TextBox)
                txtEAssyYear = CType(e.Item.FindControl("txtEAssyYear"), TextBox)
                txtEQuantity = CType(e.Item.FindControl("txtEQuantity"), TextBox)
                txtEDownPayment = CType(e.Item.FindControl("txtEDownPayment"), TextBox)
                ddlEPaymentMethod = CType(e.Item.FindControl("ddlEPaymentMethod"), DropDownList)
                txtELeasingName = CType(e.Item.FindControl("txtELeasingName"), TextBox)
                hfELeasingID = CType(e.Item.FindControl("hfELeasingID"), TextBox)
                txtERemarks = CType(e.Item.FindControl("txtERemarks"), TextBox)

                If objSPKNationalEvent.NationalEvent.PeriodStart > icDealerSPKDate.Value _
                    OrElse objSPKNationalEvent.NationalEvent.PeriodEnd < icDealerSPKDate.Value Then
                    MessageBox.Show("Tanggal SPK diluar dari Periode Event Nasional.")
                    Return
                End If

                If ddlEPaymentMethod.SelectedIndex = 0 Then
                    MessageBox.Show("Tipe Pembayaran harus diisi.")
                    Return
                End If
                If txtECustomerName.Text.Trim = "" Then
                    MessageBox.Show("Nama Konsumen harus diisi.")
                    Return
                End If
                If txtEShift.Text.Trim = "" Then
                    MessageBox.Show("Shift harus diisi.")
                    Return
                End If
                If txtEAssyYear.Text.Trim = "" Then
                    MessageBox.Show("Tahun Produksi harus diisi.")
                    Return
                End If
                If txtEQuantity.Text.Trim = "" OrElse txtEQuantity.Text.Trim = "0" Then
                    MessageBox.Show("Qty Event harus diisi.")
                    Return
                End If
                If txtEDownPayment.Text.Trim = "" OrElse txtEDownPayment.Text.Trim = "0" Then
                    MessageBox.Show("Tanda Jadi harus diisi.")
                    Return
                End If
                If hfELeasingID.Text.Trim = "" Then
                    MessageBox.Show("Leasing harus diisi.")
                    Return
                End If

                Try
                    Dim oSPKNationalEventReport As SPKNationalEventReport = CType(arlReportSPKNationalEvent(e.Item.ItemIndex), SPKNationalEventReport)
                    Dim oSPKNationalEvent As SPKNationalEvent = New SPKNationalEventFacade(User).Retrieve(CType(oSPKNationalEventReport.SPKNationalEventID, Integer))
                    oSPKNationalEvent.PaymentType = New PaymentTypeFacade(User).Retrieve(CType(ddlEPaymentMethod.SelectedValue, Integer))
                    oSPKNationalEvent.DealerSPKDate = icDealerSPKDate.Value
                    oSPKNationalEvent.CustomerName = txtECustomerName.Text
                    oSPKNationalEvent.Shift = txtEShift.Text.Trim
                    oSPKNationalEvent.AssyYear = txtEAssyYear.Text.Trim
                    oSPKNationalEvent.Quantity = txtEQuantity.Text.Trim
                    oSPKNationalEvent.DownPayment = txtEDownPayment.Text.Trim
                    oSPKNationalEvent.Leasing = New LeasingFacade(User).Retrieve(CType(hfELeasingID.Text.Trim(), Integer))
                    oSPKNationalEvent.Remarks = txtERemarks.Text.Trim
                    Dim result As Integer = New SPKNationalEventFacade(User).Update(oSPKNationalEvent)
                    dgReportSPKNationalEvent.EditItemIndex = -1
                    dgReportSPKNationalEvent.ShowFooter = True
                    If result > 0 Then
                        LoadDataNationalEvent(txtRegNumber.Text.Trim)
                        MessageBox.Show("Data berhasil disimpan")
                    End If
                Catch
                    MessageBox.Show("Data gagal disimpan")
                End Try

            Case "edit" 'Edit mode activated
                dgReportSPKNationalEvent.ShowFooter = False
                dgReportSPKNationalEvent.EditItemIndex = e.Item.ItemIndex

                sesHelper.SetSession(sessReportSPKNationalEvent, arlReportSPKNationalEvent)
                BindGridReportSPKNationalEvent(dgReportSPKNationalEvent.CurrentPageIndex)
            Case "delete" 'Delete this datagrid item 
                Dim result As Integer = 0
                Try
                    Dim oSPKNationalEventReport As SPKNationalEventReport = CType(arlReportSPKNationalEvent(e.Item.ItemIndex), SPKNationalEventReport)
                    Dim oDeleteSPKNationalEvent As SPKNationalEvent = New SPKNationalEventFacade(User).Retrieve(CType(oSPKNationalEventReport.SPKNationalEventID, Integer))
                    If oDeleteSPKNationalEvent.ID > 0 Then
                        oDeleteSPKNationalEvent.RowStatus = CType(DBRowStatus.Deleted, Short)
                        result = New SPKNationalEventFacade(User).Update(oDeleteSPKNationalEvent)
                    End If
                    arlReportSPKNationalEvent.RemoveAt(e.Item.ItemIndex)
                    If result > 0 Then
                        LoadDataNationalEvent(txtRegNumber.Text.Trim)
                        MessageBox.Show("Data berhasil dihapus")
                    End If
                Catch
                    MessageBox.Show("Data gagal dihapus")
                End Try

            Case "cancel" 'Cancel Update this datagrid item 
                dgReportSPKNationalEvent.EditItemIndex = -1
                dgReportSPKNationalEvent.ShowFooter = True
                sesHelper.SetSession(sessReportSPKNationalEvent, arlReportSPKNationalEvent)
                BindGridReportSPKNationalEvent(dgReportSPKNationalEvent.CurrentPageIndex)
        End Select

    End Sub

    Private Sub dgReportSPKNationalEvent_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgReportSPKNationalEvent.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgReportSPKNationalEvent.CurrentPageIndex * dgReportSPKNationalEvent.PageSize)

            Dim arlReportSPKNationalEvent As ArrayList = CType(sesHelper.GetSession(sessReportSPKNationalEvent), ArrayList)
            If Not IsNothing(arlReportSPKNationalEvent) AndAlso arlReportSPKNationalEvent.Count > 0 Then
                Dim objSPKNationalEventReport As SPKNationalEventReport = CType(e.Item.DataItem, SPKNationalEventReport)

                Dim lblEventName As Label = CType(e.Item.FindControl("lblEventName"), Label)
                lblEventName.Text = objSPKNationalEventReport.EventName

                Dim lblSPKNumber As Label = CType(e.Item.FindControl("lblSPKNumber"), Label)
                lblSPKNumber.Text = objSPKNationalEventReport.SPKNumber

                Dim lblDealerSPKDate As Label = CType(e.Item.FindControl("lblDealerSPKDate"), Label)
                lblDealerSPKDate.Text = If(objSPKNationalEventReport.DealerSPKDate = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime), "", objSPKNationalEventReport.DealerSPKDate.ToString("dd/MM/yyyy"))

                Dim lblCustomerName As Label = CType(e.Item.FindControl("lblCustomerName"), Label)
                lblCustomerName.Text = objSPKNationalEventReport.CustomerName

                Dim lblSalesmanName As Label = CType(e.Item.FindControl("lblSalesmanName"), Label)
                lblSalesmanName.Text = objSPKNationalEventReport.SalesName

                Dim lblSalesmanCode As Label = CType(e.Item.FindControl("lblSalesmanCode"), Label)
                lblSalesmanCode.Text = objSPKNationalEventReport.SalesCode

                Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
                lblDealerName.Text = objSPKNationalEventReport.DealerName

                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                lblDealerCode.Text = objSPKNationalEventReport.DealerCode

                Dim lblVehicleTypeCategory As Label = CType(e.Item.FindControl("lblVehicleTypeCategory"), Label)
                lblVehicleTypeCategory.Text = objSPKNationalEventReport.VehicleTypeCategory

                Dim lblVehicleTypeName As Label = CType(e.Item.FindControl("lblVehicleTypeName"), Label)
                lblVehicleTypeName.Text = objSPKNationalEventReport.VehicleTypeName

                Dim lblVechileColorName As Label = CType(e.Item.FindControl("lblVechileColorName"), Label)
                lblVechileColorName.Text = objSPKNationalEventReport.VechileColorName

                Dim lblAssyYear As Label = CType(e.Item.FindControl("lblAssyYear"), Label)
                lblAssyYear.Text = If(objSPKNationalEventReport.AssyYear = 0, "", objSPKNationalEventReport.AssyYear)

                Dim lblFakturDate As Label = CType(e.Item.FindControl("lblFakturDate"), Label)
                lblFakturDate.Text = If(objSPKNationalEventReport.FakturDate = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime), "", objSPKNationalEventReport.FakturDate.ToString("dd/MM/yyyy"))

                Dim lblFakturNumber As Label = CType(e.Item.FindControl("lblFakturNumber"), Label)
                lblFakturNumber.Text = objSPKNationalEventReport.FakturNumber

                Dim lblDownPayment As Label = CType(e.Item.FindControl("lblDownPayment"), Label)
                lblDownPayment.Text = objSPKNationalEventReport.DownPayment.ToString("#,##0")

                Dim lblQuantity As Label = CType(e.Item.FindControl("lblQuantity"), Label)
                lblQuantity.Text = objSPKNationalEventReport.Quantity

                Dim lblRemarks As Label = CType(e.Item.FindControl("lblRemarks"), Label)
                lblRemarks.Text = objSPKNationalEventReport.Remarks

                Dim lblShift As Label = CType(e.Item.FindControl("lblShift"), Label)
                lblShift.Text = objSPKNationalEventReport.Shift

                Dim lblPaymentMethod As Label = CType(e.Item.FindControl("lblPaymentMethod"), Label)
                lblPaymentMethod.Text = objSPKNationalEventReport.PaymentMethod

                Dim lblLeasingName As Label = CType(e.Item.FindControl("lblLeasingName"), Label)
                lblLeasingName.Text = objSPKNationalEventReport.LeasingName

                Dim lblLeasingID As Label = CType(e.Item.FindControl("lblLeasingID"), Label)
                lblLeasingID.Text = objSPKNationalEventReport.LeasingID

                Dim lblInputedBy As Label = CType(e.Item.FindControl("lblInputedBy"), Label)
                lblInputedBy.Text = objSPKNationalEventReport.TypeInputSPK

                Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

                If objSPKNationalEventReport.TypeInputSPK.ToString.Contains("BYDEALER") Then
                    lbtnEdit.Visible = False
                    lbtnDelete.Visible = False
                ElseIf objSPKNationalEventReport.TypeInputSPK.ToString.Contains("BYMKS") Then
                    If IsLoginAsDealer() Then
                        lbtnEdit.Visible = False
                        lbtnDelete.Visible = False
                    Else
                        lbtnEdit.Visible = True
                        lbtnDelete.Visible = True
                    End If
                Else
                    lbtnEdit.Visible = False
                    lbtnDelete.Visible = False
                End If

            End If
        End If

        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            Dim objSPKNationalEventReport As SPKNationalEventReport = CType(e.Item.DataItem, SPKNationalEventReport)
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgReportSPKNationalEvent.CurrentPageIndex * dgReportSPKNationalEvent.PageSize)

            Dim lblEEventName As Label = CType(e.Item.FindControl("lblEEventName"), Label)
            lblEEventName.Text = objSPKNationalEventReport.EventName

            Dim lblESPKNumber As Label = CType(e.Item.FindControl("lblESPKNumber"), Label)
            lblESPKNumber.Text = objSPKNationalEventReport.SPKNumber

            Dim lblESalesmanName As Label = CType(e.Item.FindControl("lblESalesmanName"), Label)
            lblESalesmanName.Text = objSPKNationalEventReport.SalesName

            Dim lblESalesmanCode As Label = CType(e.Item.FindControl("lblESalesmanCode"), Label)
            lblESalesmanCode.Text = objSPKNationalEventReport.SalesCode

            Dim lblEDealerName As Label = CType(e.Item.FindControl("lblEDealerName"), Label)
            lblEDealerName.Text = objSPKNationalEventReport.DealerName

            Dim lblEDealerCode As Label = CType(e.Item.FindControl("lblEDealerCode"), Label)
            lblEDealerCode.Text = objSPKNationalEventReport.DealerCode

            Dim lblEVehicleTypeCategory As Label = CType(e.Item.FindControl("lblEVehicleTypeCategory"), Label)
            lblEVehicleTypeCategory.Text = objSPKNationalEventReport.VehicleTypeCategory

            Dim lblEVehicleTypeName As Label = CType(e.Item.FindControl("lblEVehicleTypeName"), Label)
            lblEVehicleTypeName.Text = objSPKNationalEventReport.VehicleTypeName

            Dim lblEVechileColorName As Label = CType(e.Item.FindControl("lblEVechileColorName"), Label)
            lblEVechileColorName.Text = objSPKNationalEventReport.VechileColorName

            Dim lblEFakturDate As Label = CType(e.Item.FindControl("lblEFakturDate"), Label)
            lblEFakturDate.Text = If(objSPKNationalEventReport.FakturDate = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime), "", objSPKNationalEventReport.FakturDate.ToString("dd/MM/yyyy"))

            Dim lblEFakturNumber As Label = CType(e.Item.FindControl("lblEFakturNumber"), Label)
            lblEFakturNumber.Text = objSPKNationalEventReport.FakturNumber

            Dim ddlEPaymentMethod As DropDownList = CType(e.Item.FindControl("ddlEPaymentMethod"), DropDownList)
            Dim arrDDL As ArrayList = New ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(PaymentType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            arrDDL = New PaymentTypeFacade(User).Retrieve(criterias)
            With ddlEPaymentMethod
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "Description"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                .SelectedValue = objSPKNationalEventReport.PaymentMethodID
            End With
            ddlEPaymentMethod_SelectedIndexChanged(ddlEPaymentMethod, Nothing)

            Dim txtELeasingName As TextBox = CType(e.Item.FindControl("txtELeasingName"), TextBox)
            txtELeasingName.Text = objSPKNationalEventReport.LeasingName
            Dim hfELeasingID As TextBox = CType(e.Item.FindControl("hfELeasingID"), TextBox)
            hfELeasingID.Text = objSPKNationalEventReport.LeasingID
            Dim lblELeasingName As Label = CType(e.Item.FindControl("lblELeasingName"), Label)
            lblELeasingName.Attributes("onclick") = "ShowPPLeasingSelectionEdit(this,'" & ddlEPaymentMethod.SelectedValue & "');"

            Dim lblEInputedBy As Label = CType(e.Item.FindControl("lblEInputedBy"), Label)
            lblEInputedBy.Text = objSPKNationalEventReport.TypeInputSPK
        End If

    End Sub

    Public Sub ddlEPaymentMethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim arrDDL As ArrayList = New ArrayList
        Dim ddlEPaymentMethod As DropDownList = sender
        Dim gridItem As DataGridItem = ddlEPaymentMethod.Parent.Parent()
        Dim lblELeasingName As Label
        Dim txtELeasingName As TextBox

        If gridItem.DataSetIndex > -1 Then
            lblELeasingName = gridItem.FindControl("lblELeasingName")
            txtELeasingName = gridItem.FindControl("txtELeasingName")
        End If
        If ddlEPaymentMethod.SelectedValue = "2" Then  ' Tipe Kredit
            lblELeasingName.Visible = True
            txtELeasingName.Text = ""
        Else
            lblELeasingName.Visible = False
            txtELeasingName.Text = ddlEPaymentMethod.SelectedItem.Text
        End If
        lblELeasingName.Attributes("onclick") = "ShowPPLeasingSelectionEdit(this,'" & ddlEPaymentMethod.SelectedValue & "');"

    End Sub

    Private Sub txtRegNumber_TextChanged(sender As Object, e As EventArgs) Handles txtRegNumber.TextChanged
        Try
            If txtRegNumber.Text.Trim = "" Then
                ClearAll()
            Else
                LoadDataNationalEvent(txtRegNumber.Text)
            End If
        Catch
        End Try
    End Sub

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        If lblEventName.Text.Trim = "" Then
            MessageBox.Show("Data Event masih kosong !")
            Exit Sub
        End If
        Dim objNationalEvent As NationalEvent = CType(sesHelper.GetSession(sessNationalEvent), NationalEvent)
        If Not IsNothing(objNationalEvent) AndAlso objNationalEvent.ID > 0 Then
            arlReportSPKNationalEvent = CType(sesHelper.GetSession(sessReportSPKNationalEvent), ArrayList)
            'Dim _arlReportSPKNationalEvent As New ArrayListConverter(arlReportSPKNationalEvent)
            'Dim dtReportSPKNationalEvent As DataTable = _arlReportSPKNationalEvent.ToDataTable()
            'Dim dtReportSPKNationalEvent As DataTable = ConvertArrayListToDataTable(arlReportSPKNationalEvent)

            CreateExcelReportSPKNationalEvent(arlReportSPKNationalEvent)
        Else
            MessageBox.Show("Kode Event tidak valid !")
        End If
    End Sub

    Private Sub dgReportSPKNationalEvent_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgReportSPKNationalEvent.PageIndexChanged
        '-- Change datagrid page

        dgReportSPKNationalEvent.CurrentPageIndex = e.NewPageIndex
        BindGridReportSPKNationalEvent(e.NewPageIndex)
    End Sub

    Private Sub dgReportSPKNationalEvent_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgReportSPKNationalEvent.SortCommand
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
        dgReportSPKNationalEvent.CurrentPageIndex = 0
        BindGridReportSPKNationalEvent(dgReportSPKNationalEvent.CurrentPageIndex)
    End Sub

#End Region

End Class
