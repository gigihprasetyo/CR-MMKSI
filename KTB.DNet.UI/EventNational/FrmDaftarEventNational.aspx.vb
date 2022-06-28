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

Public Class FrmDaftarEventNational
    Inherits System.Web.UI.Page

    Private sesHelper As New SessionHelper
    Private displayPriv As Boolean
    Private editPriv As Boolean
    Private deletePriv As Boolean
    Private oDealer As Dealer
    Private SessionGridDataNationalEvent As String = "FrmDaftarEventNational.NationalEventList"
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
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Authorization()
        If Not IsPostBack Then
            ViewState("currSortColumn") = "RegNumber"
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            txtKodeDealer.Text = SesDealer().DealerCode
            hdnDealer.Value = SesDealer().DealerCode
            lblKodeDealer.Text = SesDealer().DealerCode & " / " & SesDealer().DealerName

            PageInit()
            BindddlCity()
            BindddlVenue(ddlCity.SelectedValue)
            BindDDLStatus()
            '-- Restore selection criteria
            ReadCriteria()

            If IsLoginAsDealer() Then
                txtKodeDealer.Attributes("style") = "display:none"
                lblPopUpDealer.Attributes("style") = "display:none"
                lblKodeDealer.Attributes("style") = "display:table-row"
                trKota.Visible = False
                trVenue.Visible = False
                trPeriode.Visible = False
            Else
                trKodeDealer.Visible = False
                lblPopUpDealer.Attributes("style") = "display:none"
                txtKodeDealer.Attributes("style") = "display:none"
                lblKodeDealer.Attributes("style") = "display:none"

                txtKodeDealer.Text = ""
                btnDownloadExcel.Visible = True
                trStatus.Visible = False
                dgNationalEvent.Columns(10).Visible = False
            End If
            ReadData()   '-- Read all data matching criteria
            BindGrid(dgNationalEvent.CurrentPageIndex)  '-- Bind page-1
        End If

        lblPopUpDealer.Attributes("onclick") = "ShowPopUpDealer()"
        lblPopUpTO.Attributes("onclick") = "ShowPopUpTO()"
    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Private Sub Authorization()
        If Not SecurityProvider.Authorize(Context.User, SR.EVENTNASIONAL_DaftarEventNasional_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT NASIONAL - DAFTAR EVENT NASIONAL")
        Else
            displayPriv = SecurityProvider.Authorize(Context.User, SR.EVENTNASIONAL_DaftarEventNasional_Privilege)
            'editPriv = SecurityProvider.Authorize(Context.User, SR.EVENTNASIONAL_DaftarEventNasional_Edit_Privilege)
            'deletePriv = SecurityProvider.Authorize(Context.User, SR.EVENTNASIONAL_DaftarEventNasional_Delete_Privilege)
        End If
    End Sub

    Private Sub PageInit()
        icPeriodeStart.Value = Date.Now.AddMonths(-1)
        icPeriodeEnd.Value = Date.Now
    End Sub

    Private Sub ddlCity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCity.SelectedIndexChanged
        If ddlCity.SelectedIndex = 0 Then ddlVenue.SelectedIndex = 0
        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(NationalEventCity), "ID", MatchType.Exact, ddlCity.SelectedValue))
        Dim arrCity As ArrayList = New NationalEventCityFacade(User).Retrieve(crits)
        Dim objNationalEventCity As New NationalEventCity
        If arrCity.Count > 0 Then
            objNationalEventCity = CType(arrCity(0), NationalEventCity)
            BindddlVenue(objNationalEventCity.City.ID)
        End If
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim arrNationalEventList As ArrayList = CType(sesHelper.GetSession(SessionGridDataNationalEvent), ArrayList)
        If arrNationalEventList.Count <> 0 Then
            CommonFunction.SortListControl(arrNationalEventList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrNationalEventList, pageIndex, dgNationalEvent.PageSize)
            dgNationalEvent.DataSource = PagedList
            dgNationalEvent.VirtualItemCount = arrNationalEventList.Count()
            dgNationalEvent.DataBind()
        Else
            dgNationalEvent.DataSource = New ArrayList
            dgNationalEvent.VirtualItemCount = 0
            dgNationalEvent.CurrentPageIndex = 0
            dgNationalEvent.DataBind()
        End If
    End Sub

    Private Sub ReadData()
        '-- Row status = active
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEvent), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtKodeEvent.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(NationalEvent), "RegNumber", MatchType.Exact, txtKodeEvent.Text.Trim))
        End If

        If ddlCity.SelectedValue <> "-1" Then
            crit.opAnd(New Criteria(GetType(NationalEvent), "NationalEventCity.ID", MatchType.Exact, ddlCity.SelectedValue))
        End If

        If ddlVenue.SelectedValue <> "-1" Then
            crit.opAnd(New Criteria(GetType(NationalEvent), "NationalEventVenue.ID", MatchType.Exact, ddlVenue.SelectedValue))
        End If

        If cbDate.Checked Then
            Dim tglFrom As New Date(icPeriodeStart.Value.Year, icPeriodeStart.Value.Month, icPeriodeStart.Value.Day, 0, 0, 0)
            Dim tglTo As New Date(icPeriodeEnd.Value.Year, icPeriodeEnd.Value.Month, icPeriodeEnd.Value.Day, 23, 59, 59)
            crit.opAnd(New Criteria(GetType(NationalEvent), "PeriodStart", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")), "((", True)
            crit.opAnd(New Criteria(GetType(NationalEvent), "PeriodStart", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")), ")", False)

            crit.opOr(New Criteria(GetType(NationalEvent), "PeriodEnd", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")), "(", True)
            crit.opAnd(New Criteria(GetType(NationalEvent), "PeriodEnd", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")), "))", False)
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(NationalEvent), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        '-- Retrieve recordset
        Dim arrNationalEventList As ArrayList = New NationalEventFacade(User).RetrieveByCriteria(crit, sortColl)
        Dim arrNationalEventListFix As ArrayList = New ArrayList
        Dim arrNationalEventListFix2 As ArrayList = New ArrayList

        If IsLoginAsDealer() Then
            If Not IsNothing(SesDealer().City) AndAlso SesDealer().City.ID > 0 Then
                For Each arrNationalEvent As NationalEvent In arrNationalEventList
                    Dim arrDealerCityID() As String = arrNationalEvent.DealerCityID.Split(";")
                    Dim findDealer = Array.Find(arrDealerCityID, Function(s) s = SesDealer().City.ID)
                    If findDealer = SesDealer().City.ID Then
                        arrNationalEventListFix.Add(arrNationalEvent)
                    End If
                Next
            End If
        Else
            arrNationalEventListFix.AddRange(arrNationalEventList)
        End If


        If ddlStatus.SelectedValue <> "-1" Then
            For Each arrNationalEvent As NationalEvent In arrNationalEventListFix
                Dim critDetail As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critDetail.opAnd(New Criteria(GetType(NationalEventDetail), "NationalEvent.ID", MatchType.Exact, arrNationalEvent.ID))
                Dim arrNationalEventDetail As ArrayList = New NationalEventDetailFacade(User).Retrieve(critDetail)
                If arrNationalEventDetail.Count > 0 Then
                    If ddlStatus.SelectedValue = 1 Then
                        arrNationalEventListFix2.Add(arrNationalEvent)
                    End If
                Else
                    If ddlStatus.SelectedValue = 2 Then
                        arrNationalEventListFix2.Add(arrNationalEvent)
                    End If
                End If
            Next            
        Else
            arrNationalEventListFix2.AddRange(arrNationalEventListFix)
        End If

        sesHelper.SetSession(SessionGridDataNationalEvent, arrNationalEventListFix2)
        If arrNationalEventListFix2.Count <= 0 Then
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ReadData()
        'dgNationalEvent.CurrentPageIndex = 0 '-- Read all data matching criteria
        BindGrid(dgNationalEvent.CurrentPageIndex)  '-- Bind page-1
        StoreCriteria()
    End Sub

    Private Sub dgNationalEvent_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgNationalEvent.PageIndexChanged
        '-- Change datagrid page

        ReadData()
        dgNationalEvent.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
        StoreCriteria()
    End Sub

    Private Sub dgNationalEvent_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgNationalEvent.SortCommand
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
        dgNationalEvent.CurrentPageIndex = 0
        ReadData()
        BindGrid(dgNationalEvent.CurrentPageIndex)
        StoreCriteria()
    End Sub

    Private Sub ReadCriteria()
        Dim crit As Hashtable
        crit = CType(sesHelper.GetSession(SessionCriteriaProposalEvent), Hashtable)
        If Not crit Is Nothing Then
            txtKodeDealer.Text = CStr(crit.Item("DealerCode"))
            lblKodeDealer.Text = CStr(crit.Item("DealerCode"))
            txtKodeEvent.Text = CStr(crit.Item("DealerBranchCode"))
            cbDate.Checked = CBool(crit.Item("cbDate"))
            icPeriodeStart.Value = CStr(crit.Item("PeriodStart"))
            icPeriodeEnd.Value = CStr(crit.Item("PeriodEnd"))
            lblNamaEvent.Text = CStr(crit.Item("NamaEvent"))

            ViewState("currSortColumn") = CStr(crit.Item("currSortColumn"))
            ViewState("currSortDirection") = CStr(crit.Item("currSortDirection"))
            dgNationalEvent.CurrentPageIndex = CInt(crit.Item("PageIndex"))
        End If
    End Sub

    Private Sub StoreCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("DealerCode", txtKodeDealer.Text)
        crit.Add("DealerBranchCode", txtKodeEvent.Text)
        crit.Add("cbDate", cbDate.Checked)
        crit.Add("PeriodStart", icPeriodeStart.Value)
        crit.Add("PeriodEnd", icPeriodeEnd.Value)
        crit.Add("NamaEvent", lblNamaEvent.Text)

        crit.Add("PageIndex", dgNationalEvent.CurrentPageIndex)
        crit.Add("currSortColumn", ViewState("currSortColumn"))
        crit.Add("currSortDirection", ViewState("currSortDirection"))

        sesHelper.SetSession(SessionCriteriaProposalEvent, crit) '-- Store in session
    End Sub

    Protected Sub btnDownloadExcel_Click(sender As Object, e As EventArgs) Handles btnDownloadExcel.Click
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        If dgNationalEvent.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        ' mengambil data yang dibutuhkan
        arrData = CType(sesHelper.GetSession(SessionGridDataNationalEvent), ArrayList)
        If arrData.Count > 0 Then
            CreateExcel("DaftarEventNasional", arrData)
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
            ws.Cells("B3").Value = "Reg Number"
            ws.Cells("C3").Value = "Nama Event"
            ws.Cells("D3").Value = "Tipe Event"
            ws.Cells("E3").Value = "Kota Event"
            ws.Cells("F3").Value = "Venue Event"
            ws.Cells("G3").Value = "Area Dealer"
            ws.Cells("H3").Value = "Kota Dealer"
            ws.Cells("I3").Value = "Periode Event Mulai"
            ws.Cells("J3").Value = "Periode Event Sampai"
            ws.Cells("K3").Value = "Target Prospek"
            ws.Cells("L3").Value = "Target SPK"

            Dim strDealerCityCodes As String = String.Empty
            Dim strDealerCityName As String = String.Empty

            For i As Integer = 0 To Data.Count - 1
                Dim item As NationalEvent = Data(i)
                ws.Cells(i + 4, 1).Value = i + 1
                ws.Cells(i + 4, 2).Value = item.RegNumber
                ws.Cells(i + 4, 3).Value = item.NationalEventType.Name + " " + item.NationalEventCity.City.CityName
                ws.Cells(i + 4, 4).Value = item.NationalEventType.Name
                ws.Cells(i + 4, 5).Value = item.NationalEventCity.City.CityName
                ws.Cells(i + 4, 6).Value = item.NationalEventVenue.VenueName
                ws.Cells(i + 4, 7).Value = item.DealerArea1.Description

                strDealerCityCodes = String.Empty
                strDealerCityName = String.Empty
                If Not IsNothing(item.DealerCityID) AndAlso item.DealerCityID.Trim() <> "" Then
                    Dim oCity As New City
                    Dim arrDealerCityID As String() = item.DealerCityID.Split(";")
                    For Each dealerCityID As String In arrDealerCityID
                        oCity = New KTB.DNet.BusinessFacade.General.CityFacade(User).Retrieve(CInt(dealerCityID))
                        If Not IsNothing(oCity) AndAlso oCity.ID > 0 Then
                            If strDealerCityCodes = String.Empty Then
                                strDealerCityCodes = oCity.CityCode
                                strDealerCityName = oCity.CityName
                            Else
                                strDealerCityCodes += ";" & oCity.CityCode
                                strDealerCityName += ";" & oCity.CityName
                            End If
                        End If
                    Next
                End If

                ws.Cells(i + 4, 8).Value = strDealerCityName
                ws.Cells(i + 4, 9).Value = Format(item.PeriodStart, "dd/MM/yyyy")
                ws.Cells(i + 4, 10).Value = Format(item.PeriodEnd, "dd/MM/yyyy")
                ws.Cells(i + 4, 11).Value = item.TargetProspect
                ws.Cells(i + 4, 12).Value = item.TargetSPK
            Next

            Dim rowDtl As Integer = 0
            Dim wsDetail As ExcelWorksheet = CreateSheet(pck, "EVENT NASIONAL DETAIL")

            wsDetail.Cells("A1").Value = "EVENT NASIONAL DETAIL"
            wsDetail.Cells("A3").Value = "No"
            wsDetail.Cells("B3").Value = "Reg Number"
            wsDetail.Cells("C3").Value = "Nama Event"
            wsDetail.Cells("D3").Value = "Kota Dealer"
            wsDetail.Cells("E3").Value = "Kode Dealer"
            wsDetail.Cells("F3").Value = "Nama Dealer"
            wsDetail.Cells("G3").Value = "Nama PIC Dealer"
            wsDetail.Cells("H3").Value = "No Handphone PIC"
            wsDetail.Cells("I3").Value = "Email PIC"
            wsDetail.Cells("J3").Value = "Tanggal Join Event"
            wsDetail.Cells("K3").Value = "Kode Salesman"
            wsDetail.Cells("L3").Value = "Status Registrasi"

            For i As Integer = 0 To Data.Count - 1
                Dim item As NationalEvent = Data(i)

                Dim critDealer As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critDealer.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "City.ID", MatchType.InSet, "('" & item.DealerCityID.Replace(";", "','") & "')"))
                Dim sortCollDealer As SortCollection = New SortCollection
                sortCollDealer.Add(New Sort(GetType(KTB.DNet.Domain.Dealer), CType("DealerCode", String), CType(ViewState("currSortDirection"), Sort.SortDirection)))
                Dim arrDealer As ArrayList = New KTB.DNet.BusinessFacade.General.DealerFacade(User).RetrieveByCriteria(critDealer, sortCollDealer)

                For j As Integer = 0 To arrDealer.Count - 1
                    Dim itemDealer As Dealer = arrDealer(j)

                    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crit.opAnd(New Criteria(GetType(NationalEventDetail), "NationalEvent.ID", MatchType.Exact, item.ID))
                    crit.opAnd(New Criteria(GetType(NationalEventDetail), "Dealer.ID", MatchType.Exact, itemDealer.ID))
                    Dim sortCollDetail As SortCollection = New SortCollection
                    sortCollDetail.Add(New Sort(GetType(NationalEventDetail), CType("PICDealerName", String), CType(ViewState("currSortDirection"), Sort.SortDirection)))
                    Dim arrNationalEventDetailList As ArrayList = New NationalEventDetailFacade(User).RetrieveByCriteria(crit, sortCollDetail)

                    If arrNationalEventDetailList.Count > 0 Then
                        Dim itemDetail As NationalEventDetail = arrNationalEventDetailList(0)

                        wsDetail.Cells(rowDtl + 4, 1).Value = rowDtl + 1
                        wsDetail.Cells(rowDtl + 4, 2).Value = item.RegNumber
                        wsDetail.Cells(rowDtl + 4, 3).Value = item.NationalEventType.Name + " " + item.NationalEventCity.City.CityName
                        wsDetail.Cells(rowDtl + 4, 4).Value = itemDetail.Dealer.City.CityName
                        wsDetail.Cells(rowDtl + 4, 5).Value = itemDetail.Dealer.DealerCode
                        wsDetail.Cells(rowDtl + 4, 6).Value = itemDetail.Dealer.DealerName
                        wsDetail.Cells(rowDtl + 4, 7).Value = itemDetail.PICDealerName
                        wsDetail.Cells(rowDtl + 4, 8).Value = itemDetail.PICDealerHPNo
                        wsDetail.Cells(rowDtl + 4, 9).Value = itemDetail.PICDealerEmail

                        Dim critDate As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventDetailDate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        critDate.opAnd(New Criteria(GetType(NationalEventDetailDate), "NationalEventDetail.ID", MatchType.Exact, itemDetail.ID))
                        Dim sortCollDetailDate As SortCollection = New SortCollection
                        sortCollDetailDate.Add(New Sort(GetType(NationalEventDetailDate), CType("ActivityDate", String), CType(ViewState("currSortDirection"), Sort.SortDirection)))
                        Dim arrNationalEventDetailDateList As ArrayList = New NationalEventDetailDateFacade(User).RetrieveByCriteria(critDate, sortCollDetailDate)
                        Dim activityDate As String = ""
                        For z As Integer = 0 To arrNationalEventDetailDateList.Count - 1
                            Dim itemDetailDate As NationalEventDetailDate = arrNationalEventDetailDateList(z)
                            activityDate += Format(itemDetailDate.ActivityDate, "dd/MM/yyyy") + ";" + vbCrLf
                        Next
                        wsDetail.Cells(rowDtl + 4, 10).Value = activityDate

                        Dim critSalesman As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        critSalesman.opAnd(New Criteria(GetType(SalesmanHeader), "ID", MatchType.InSet, "('" & itemDetail.SalesmanID.Replace(";", "','") & "')"))
                        Dim sortCollSalesman As SortCollection = New SortCollection
                        sortCollSalesman.Add(New Sort(GetType(SalesmanHeader), CType("SalesmanCode", String), CType(ViewState("currSortDirection"), Sort.SortDirection)))
                        Dim arrSalesmanList As ArrayList = New KTB.DNet.BusinessFacade.Salesman.SalesmanHeaderFacade(User).Retrieve(critSalesman, sortCollSalesman)
                        Dim salesman As String = ""
                        For z As Integer = 0 To arrSalesmanList.Count - 1
                            Dim itemSalesman As SalesmanHeader = arrSalesmanList(z)
                            salesman += itemSalesman.SalesmanCode + " - " + itemSalesman.Name + ";"
                        Next
                        wsDetail.Cells(rowDtl + 4, 11).Value = salesman

                        wsDetail.Cells(rowDtl + 4, 12).Value = "Registered"
                    Else

                        wsDetail.Cells(rowDtl + 4, 1).Value = rowDtl + 1
                        wsDetail.Cells(rowDtl + 4, 2).Value = item.RegNumber
                        wsDetail.Cells(rowDtl + 4, 3).Value = item.NationalEventType.Name + " " + item.NationalEventCity.City.CityName
                        wsDetail.Cells(rowDtl + 4, 4).Value = itemDealer.City.CityName
                        wsDetail.Cells(rowDtl + 4, 5).Value = itemDealer.DealerCode
                        wsDetail.Cells(rowDtl + 4, 6).Value = itemDealer.DealerName

                        wsDetail.Cells(rowDtl + 4, 12).Value = "Not Registered"
                    End If
                    rowDtl += 1
                Next
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

    Protected Sub dgNationalEvent_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgNationalEvent.ItemCommand
        Select Case e.CommandName
            Case "Detail"
                Response.Redirect("FrmInputEventNational.aspx?Mode=View&NationalEventID=" & e.Item.Cells(0).Text)
            Case "Edit"
                Response.Redirect("FrmInputEventNational.aspx?Mode=Edit&NationalEventID=" & e.Item.Cells(0).Text)
            Case "Mapping"
                Response.Redirect("FrmRegistrasiEventNational.aspx?Mode=New&NationalEventID=" & e.Item.Cells(0).Text)
            Case "Delete"
                'DeleteNationalEvent(CInt(e.CommandArgument))
        End Select
    End Sub

    Private Sub BindddlCity()
        ddlCity.Items.Clear()
        ddlCity.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        With ddlCity.Items
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim arrCity As ArrayList = New NationalEventCityFacade(User).Retrieve(crits)
            For Each _neCity As NationalEventCity In arrCity
                .Add(New ListItem(_neCity.City.CityName, _neCity.ID))
            Next
        End With
    End Sub

    Private Sub BindddlVenue(ByVal intCityID As Integer)
        ddlVenue.Items.Clear()
        ddlVenue.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        With ddlVenue.Items
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventVenue), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(NationalEventVenue), "City.ID", MatchType.Exact, intCityID))
            Dim arrVenue As ArrayList = New NationalEventVenueFacade(User).Retrieve(crits)
            For Each _neVenue As NationalEventVenue In arrVenue
                .Add(New ListItem(_neVenue.VenueName, _neVenue.ID))
            Next
        End With
    End Sub

    Private Sub BindDDLStatus()
        With ddlStatus
            .Items.Clear()
            .DataTextField = "Status"
            .DataValueField = "ID"
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            .Items.Insert(1, New ListItem("Registered", 1))
            .Items.Insert(2, New ListItem("Not Registered", 2))
        End With
    End Sub

    Private Sub DeleteNationalEvent(ByVal intNationalEventID As Integer)
        Dim objBabitEventReportHeaderFacade As BabitEventReportHeaderFacade = New BabitEventReportHeaderFacade(User)
        Dim arrBabitEventReportHeader As ArrayList = New ArrayList
        Dim criteriasaa As New CriteriaComposite(New Criteria(GetType(BabitEventReportHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriasaa.opAnd(New Criteria(GetType(BabitEventReportHeader), "NationalEvent.ID", MatchType.Exact, intNationalEventID))
        arrBabitEventReportHeader = objBabitEventReportHeaderFacade.Retrieve(criteriasaa)
        If Not IsNothing(arrBabitEventReportHeader) AndAlso arrBabitEventReportHeader.Count > 0 Then
            MessageBox.Show("Nomor Reg Proposal ini sudah di pakai di Laporan Event")
            Exit Sub
        End If

        Dim objNationalEventFacade As NationalEventFacade = New NationalEventFacade(User)
        Dim objBabitEventProposalDetailFacade As BabitEventProposalDetailFacade = New BabitEventProposalDetailFacade(User)
        Dim objBabitEventProposalDocumentFacade As BabitEventProposalDocumentFacade = New BabitEventProposalDocumentFacade(User)
        'Dim objBabitEventProposalActivityFacade As BabitEventProposalActivityFacade = New BabitEventProposalActivityFacade(User)

        Dim objNationalEvent As NationalEvent = objNationalEventFacade.Retrieve(intNationalEventID)
        objNationalEvent.RowStatus = CType(DBRowStatus.Deleted, Short)

        '--untuk data biaya
        Dim arrBabitEventProposalDetail As ArrayList = New ArrayList
        arrBabitEventProposalDetail = New BabitEventProposalDetailFacade(User).RetrieveDataBabitEventProposalDetail(intNationalEventID, strTypeCode, strEnumBabitCategory, strValueCodeBiaya)
        For Each obj As BabitEventProposalDetail In arrBabitEventProposalDetail
            obj.RowStatus = CType(DBRowStatus.Deleted, Short)
        Next

        '--untuk data aktivitas
        Dim arrBabitEventProposalAct As ArrayList = New ArrayList
        arrBabitEventProposalAct = New BabitEventProposalDetailFacade(User).RetrieveDataBabitEventProposalDetail(intNationalEventID, strTypeCode, strEnumBabitCategory, strValueCodeAct)
        For Each obj As BabitEventProposalDetail In arrBabitEventProposalAct
            obj.RowStatus = CType(DBRowStatus.Deleted, Short)
        Next

        '---untuk data dokumen
        Dim arrBabitEventProposalDoc As ArrayList = New ArrayList
        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(BabitEventProposalDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(BabitEventProposalDocument), "NationalEvent.ID", MatchType.Exact, intNationalEventID))
        arrBabitEventProposalDoc = objBabitEventProposalDocumentFacade.Retrieve(criterias2)
        For Each obj As BabitEventProposalDocument In arrBabitEventProposalDoc
            obj.RowStatus = CType(DBRowStatus.Deleted, Short)
        Next

        Dim _result As Integer = 0
        '_result = New BabitEventProposalDetailFacade(User).UpdateTransaction(objNationalEvent, arrBabitEventProposalDetail, New ArrayList, arrBabitEventProposalDoc, New ArrayList, arrBabitEventProposalAct, New ArrayList)
        If _result > 0 Then
            MessageBox.Show("Delete Data Sukses")
        End If
        ReadData()
        BindGrid(dgNationalEvent.CurrentPageIndex)
    End Sub

    Protected Sub dgNationalEvent_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgNationalEvent.ItemDataBound
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblKodeEvent As Label = CType(e.Item.FindControl("lblKodeEvent"), Label)
        Dim lblNamaEvent As Label = CType(e.Item.FindControl("lblNamaEvent"), Label)
        Dim lblCityName As Label = CType(e.Item.FindControl("lblCityName"), Label)
        Dim lblVenueName As Label = CType(e.Item.FindControl("lblVenueName"), Label)
        Dim lblTargetProspek As Label = CType(e.Item.FindControl("lblTargetProspek"), Label)
        Dim lblTargetSPK As Label = CType(e.Item.FindControl("lblTargetSPK"), Label)
        Dim lblPeriodStart As Label = CType(e.Item.FindControl("lblPeriodStart"), Label)
        Dim lblPeriodEnd As Label = CType(e.Item.FindControl("lblPeriodEnd"), Label)
        Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)


        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As NationalEvent = CType(e.Item.DataItem, NationalEvent)
            lblNo.Text = (e.Item.ItemIndex + 1 + (dgNationalEvent.PageSize * dgNationalEvent.CurrentPageIndex)).ToString
            lblKodeEvent.Text = oData.RegNumber
            lblNamaEvent.Text = oData.NationalEventType.Name & " " & oData.NationalEventCity.City.CityName
            lblCityName.Text = oData.NationalEventCity.City.CityName
            lblVenueName.Text = oData.NationalEventVenue.VenueName
            lblPeriodStart.Text = oData.PeriodStart
            lblPeriodEnd.Text = oData.PeriodEnd
            lblTargetProspek.Text = oData.TargetProspect
            lblTargetSPK.Text = oData.TargetSPK

            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(NationalEventDetail), "NationalEvent.ID", MatchType.Exact, oData.ID))
            If IsLoginAsDealer() Then
                crit.opAnd(New Criteria(GetType(NationalEventDetail), "Dealer.ID", MatchType.Exact, SesDealer().ID))
            End If
            Dim arrNationalEventDetail As ArrayList = New NationalEventDetailFacade(User).Retrieve(crit)

            If arrNationalEventDetail.Count > 0 Then
                lblStatus.Text = "Registered"
            Else
                lblStatus.Text = "Not Registered"
            End If


            Dim lnkbtnDetail As LinkButton = CType(e.Item.FindControl("lnkbtnDetail"), LinkButton)
            Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
            Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)
            Dim lnkbtnMapping As LinkButton = CType(e.Item.FindControl("lnkbtnMapping"), LinkButton)
            If Not IsNothing(lnkbtnDetail) Then
                lnkbtnDetail.Visible = True
            End If
            If Not IsNothing(lnkbtnEdit) Then
                lnkbtnEdit.Visible = Not IsLoginAsDealer()
            End If
            If Not IsNothing(lnkbtnDelete) Then
                lnkbtnDelete.Visible = deletePriv
            End If
            If Not IsNothing(lnkbtnMapping) Then
                lnkbtnMapping.Visible = IsLoginAsDealer()
            End If
        End If
    End Sub

    Protected Sub hdnDealer_ValueChanged(sender As Object, e As EventArgs) Handles hdnDealer.ValueChanged
        Dim data As String() = hdnDealer.Value.Trim.Split(";")
        txtKodeDealer.Text = data(0)
    End Sub

    Protected Sub hdnTempEvent_ValueChanged(sender As Object, e As EventArgs) Handles hdnTempEvent.ValueChanged
        Dim data As String() = hdnTempEvent.Value.Trim.Split(";")
        txtKodeEvent.Text = data(0)
    End Sub

    Protected Sub txtKodeEvent_TextChanged(sender As Object, e As EventArgs) Handles txtKodeEvent.TextChanged
        If txtKodeEvent.Text.Trim = String.Empty Then
            lblNamaEvent.Text = ""
            Exit Sub
        End If
        Dim objNationalEvent As NationalEvent = New NationalEventFacade(User).Retrieve(txtKodeEvent.Text)
        If objNationalEvent.ID > 0 Then
            lblNamaEvent.Text = objNationalEvent.NationalEventType.Name & " " & objNationalEvent.NationalEventCity.City.CityName
        End If

    End Sub
End Class