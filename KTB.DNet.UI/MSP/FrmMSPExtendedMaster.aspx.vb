Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports OfficeOpenXml
Imports KTB.DNet.BusinessValidation.Helpers

Public Class FrmMSPExtendedMaster
    Inherits System.Web.UI.Page

    Private _view As Boolean = False
    Private _sessHelper As New SessionHelper
    Private _strSessSearch As String = "FrmMSPExtendedMaster.Criteria"
    Dim crt As CriteriaComposite
    Dim arr As ArrayList
    Dim sorts As SortCollection
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            BindDropDown()
            ViewState("CurrentSortColumn") = "StartDate"
            ViewState("CurrentSortDirect") = KTB.DNet.Domain.Search.Sort.SortDirection.DESC
        End If
    End Sub

    Private Sub BindDropDown()
        'dropdown category
        crt = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crt.opAnd(New Criteria(GetType(Category), "CategoryCode", MatchType.No, "CV"))
        sorts = New SortCollection
        sorts.Add(New Sort(GetType(Category), "CategoryCode", Search.Sort.SortDirection.ASC))
        arr = New CategoryFacade(User).RetrieveByCriteria(crt, sorts)

        ddlCategory.Items.Clear()
        ddlCategory.DataSource = arr
        ddlCategory.DataTextField = "CategoryCode".ToUpper
        ddlCategory.DataValueField = "ID"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlCategory.SelectedIndex = 0
        ddlCategory_SelectedIndexChanged(Me, System.EventArgs.Empty)

        ' dropdown status
        ddlStatus.Items.Clear()
        ddlStatus.DataSource = New EnumMSPMasterStatus().Retrieve()
        ddlStatus.DataTextField = "NameTitle".ToUpper
        ddlStatus.DataValueField = "ValTitle"
        ddlStatus.DataBind()
        ddlStatus.Items.Insert(0, New ListItem("Silakan Pilih", -1))
        ddlStatus.SelectedIndex = 0

        ' dropdown msptype
        crt = New CriteriaComposite(New Criteria(GetType(MSPExType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(MSPExType), "Sequence", Search.Sort.SortDirection.ASC))
        ddlMSPType.Items.Clear()
        ddlMSPType.DataSource = New MSPExTypeFacade(User).Retrieve(crt, sortColl)
        ddlMSPType.DataTextField = "Description"
        ddlMSPType.DataValueField = "ID"
        ddlMSPType.DataBind()
        ddlMSPType.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlMSPType.SelectedIndex = 0

    End Sub

    Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        ddlVechileModel.Items.Clear()
        If ddlCategory.SelectedIndex <> 0 Then
            CommonFunction.BindVehicleSubCategoryToDDL2(ddlVechileModel, ddlCategory.SelectedItem.Text)
        Else
            ddlVechileModel.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        End If
        ddlVechileModel.SelectedIndex = 0
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        crt = New CriteriaComposite(New Criteria(GetType(MSPExMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(crt)
        _sessHelper.SetSession(_strSessSearch, crt)
        dtgMSPExMaster.CurrentPageIndex = 0
        BindDatagrid(dtgMSPExMaster.CurrentPageIndex)
    End Sub

    Private Sub CreateCriteria(ByRef criterias As CriteriaComposite)
        Dim str As String = String.Empty
        Dim strValidate As String = String.Empty
        Dim isStrSql As Integer = 0
        'Dim strSql As String = "SELECT ID FROM MSPExMaster WHERE 0=0 "

        If ddlCategory.SelectedIndex <> 0 Then
            If ddlVechileModel.SelectedValue <> "-1" Then
                Dim strSql2 As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlVechileModel.SelectedValue
                criterias.opAnd(New Criteria(GetType(MSPExMaster), "VechileType.VechileModel.ID", MatchType.InSet, "(" & strSql2 & ")"))
            Else
                str = "(SELECT ID FROM VechileModel WHERE CategoryID = " + ddlCategory.SelectedValue + ")"
                criterias.opAnd(New Criteria(GetType(MSPExMaster), "VechileType.VechileModel.ID", MatchType.InSet, str))
            End If
        End If

        If ddlMSPType.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(MSPExMaster), "MSPExType.ID", MatchType.Exact, ddlMSPType.SelectedValue))
        End If
        If ddlStatus.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(MSPExMaster), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If
        If txtDuration.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(MSPExMaster), "Duration", MatchType.Exact, txtDuration.Text.Trim))
        End If
        If txtKm.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(MSPExMaster), "MSPExKM", MatchType.Exact, txtKm.Text.Trim))
        End If

        Dim strSql As String = "SELECT ID FROM MSPExMaster WHERE 0=0 "
        If chkStartDate.Checked Then
            strSql += " AND (StartDate BETWEEN '" + Format(StartDateFrom.Value, "yyyy-MM-dd") + "' AND '" + Format(StartDateTo.Value, "yyyy-MM-dd") + "')"
            isStrSql += 1
        End If

        If chkEndDate.Checked Then
            strSql += " AND (EndDate BETWEEN '" + Format(EndDateFrom.Value, "yyyy-MM-dd") + "' AND '" + Format(EndDateTo.Value, "yyyy-MM-dd") + "')"
            isStrSql += 1
        End If

        If isStrSql > 0 Then
            crt.opAnd(New Criteria(GetType(MSPExMaster), "ID", MatchType.InSet, "(" + strSql + ")"))
        End If

    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        arr = New MSPExMasterFacade(User).RetrieveByCriteria(CType(_sessHelper.GetSession(_strSessSearch), CriteriaComposite), indexPage + 1, dtgMSPExMaster.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgMSPExMaster.DataSource = arr
        dtgMSPExMaster.VirtualItemCount = totalRow
        dtgMSPExMaster.DataBind()
    End Sub

    Private Sub dtgMSPExMaster_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMSPExMaster.ItemDataBound
        'set no
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgMSPExMaster.CurrentPageIndex * dtgMSPExMaster.PageSize)
        End If

        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            Dim rowValue As MSPExMaster = CType(e.Item.DataItem, MSPExMaster)
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Then
                ' set tipe kendaraan
                Dim lblVechileModel As Label = CType(e.Item.FindControl("lblVechileModel"), Label)
                Dim lblMSPExType As Label = CType(e.Item.FindControl("lblMSPExType"), Label)
                Dim lblStartDate As Label = CType(e.Item.FindControl("lblStartDate"), Label)
                Dim lblEndDate As Label = CType(e.Item.FindControl("lblEndDate"), Label)
                Dim lblKm As Label = CType(e.Item.FindControl("lblKm"), Label)
                Dim lblAmount As Label = CType(e.Item.FindControl("lblAmount"), Label)
                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                Dim lblVechileTypeCode As Label = CType(e.Item.FindControl("lblVechileTypeCode"), Label)
                Dim lblPPN As Label = CType(e.Item.FindControl("lblPPN"), Label)
                Dim lblHargaInclPPN As Label = CType(e.Item.FindControl("lblHargaInclPPN"), Label)
                If Not IsNothing(lblVechileModel) Then
                    lblVechileModel.Text = rowValue.VechileType.Description
                    lblVechileTypeCode.Text = rowValue.VechileType.VechileTypeCode
                End If

                If Not IsNothing(lblMSPExType) Then
                    lblMSPExType.Text = rowValue.MSPExType.Code
                End If

                ' set tgl mulai berlaku
                If Not IsNothing(lblStartDate) Then
                    lblStartDate.Text = rowValue.StartDate.ToString("dd MMM yyyy")
                End If
                ' set tgl berlaku sampai
                If Not IsNothing(lblEndDate) Then
                    lblEndDate.Text = rowValue.EndDate.ToString("dd MMM yyyy")
                End If
                ' set km
                If Not IsNothing(lblKm) Then
                    lblKm.Text = String.Format("{0:#,##0}", Convert.ToDouble(rowValue.MSPExKM))
                End If
                ' set amount
                If Not IsNothing(lblAmount) Then
                    'lblAmount.Text = "Rp. " & rowValue.Amount.ToString("C")
                    'lblPPN.Text = "Rp. " & (rowValue.Amount * 0.1).ToString("C")
                    'lblHargaInclPPN.Text = "Rp. " & (rowValue.Amount * 1.1).ToString("C")

                    'Dim tglClaim As Date = DateTime.Now.Date
                    Dim ppnVal As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(rowValue.StartDate.Date, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
                    lblAmount.Text = rowValue.Amount.ToString("C")
                    lblPPN.Text = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnVal, dpp:=rowValue.Amount).ToString("C")
                    lblHargaInclPPN.Text = (CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnVal, dpp:=rowValue.Amount) + rowValue.Amount).ToString("C")
                End If
                ' set status
                If Not IsNothing(lblStatus) Then
                    lblStatus.Text = New EnumMSPMasterStatus().GetStatus(rowValue.Status)
                End If
            End If
        End If
    End Sub

    Private Sub dtgMSPExMaster_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgMSPExMaster.PageIndexChanged
        dtgMSPExMaster.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgMSPExMaster.CurrentPageIndex)
    End Sub

    Private Sub dtgMSPExMaster_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgMSPExMaster.SortCommand
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

        dtgMSPExMaster.SelectedIndex = -1
        dtgMSPExMaster.CurrentPageIndex = 0
        BindDatagrid(dtgMSPExMaster.CurrentPageIndex)
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        Dim crits As CriteriaComposite
        If dtgMSPExMaster.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        If Not IsNothing(_sessHelper.GetSession(_strSessSearch)) Then
            crits = CType(_sessHelper.GetSession(_strSessSearch), CriteriaComposite)
        End If
        ' mengambil data yang dibutuhkan
        arrData = New MSPExMasterFacade(User).Retrieve(crits)
        If arrData.Count > 0 Then
            CreateExcel("DaftarMSPExtendedMaster", arrData)
        End If

    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim oD As Dealer
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = FileName
            ws.Cells("A3").Value = "No" '1
            ws.Cells("B3").Value = "Model Kendaraan" '2
            ws.Cells("C3").Value = "Tipe MSP Extended" '3
            ws.Cells("D3").Value = "Tgl Mulai Berlaku" '4
            ws.Cells("E3").Value = "Berlaku Sampai Dengan" '5
            ws.Cells("F3").Value = "Durasi (Th)" '6
            ws.Cells("G3").Value = "KM" '7
            ws.Cells("H3").Value = "Harga MSP Extended (Rp)" '8
            ws.Cells("I3").Value = "Status" '9

            Dim idx As Integer = 0
            For i As Integer = 0 To Data.Count - 1
                Dim item As MSPExMaster = Data(i)
                ws.Cells(idx + 4, 1).Value = idx + 1
                ws.Cells(idx + 4, 2).Value = item.VechileType.VechileModel.VechileModelIndCode
                ws.Cells(idx + 4, 3).Value = item.MSPExType.Code
                ws.Cells(idx + 4, 4).Value = item.StartDate
                ws.Column(4).Style.Numberformat.Format = "DD/MM/YYYY"
                ws.Cells(idx + 4, 5).Value = item.EndDate
                ws.Column(5).Style.Numberformat.Format = "DD/MM/YYYY"
                ws.Cells(idx + 4, 6).Value = item.Duration
                ws.Cells(idx + 4, 7).Value = item.MSPExKM
                ws.Cells(idx + 4, 8).Value = item.Amount
                ws.Cells(idx + 4, 9).Value = item.Status
                idx = idx + 1

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
End Class