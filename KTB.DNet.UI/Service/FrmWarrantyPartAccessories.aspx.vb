Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports OfficeOpenXml

Public Class FrmWarrantyPartAccessories
    Inherits System.Web.UI.Page
    Private sHSP As SessionHelper = New SessionHelper
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Cari()
        End If
        lblSearchDealer.Attributes("onclick") = "ShowModelSelection();"
    End Sub

    Private Sub Cari(Optional index As Integer = 0)
        Dim crit As New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(SparePartMaster), "IsWarranty", MatchType.Exact, 1))
        CreateCriteria(crit)
        dtgRole.CurrentPageIndex = index
        BindDataGrid(index)
        'txtKodeDealer.Text = ""
        'hdnKodeDealer.Value = ""
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            Dim dt As ArrayList
            Try
                dt = New SparePartMasterFacade(User).RetrieveActiveList(CType(sHSP.GetSession("FrmWarrantyPartAccessories.criteria"), CriteriaComposite), indexPage + 1, dtgRole.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            Catch
                MessageBox.Show("Harap periksa kembali kategori pencarian anda")
                dt = New ArrayList
                totalRow = dt.Count
            End Try
            dtgRole.DataSource = dt
            dtgRole.VirtualItemCount = totalRow
            dtgRole.DataBind()
        End If
    End Sub

    Private Sub CreateCriteria(ByVal crit As CriteriaComposite)
        'If hdnKodeDealer.Value <> "" Then
        '    crit.opAnd(New Criteria(GetType(SparePartMaster), "ModelCode", MatchType.InSet, "('" & hdnKodeDealer.Value.Replace(";", "','") & "')"))
        'End If
        If txtKodeDealer.Text <> "" Then
            crit.opAnd(New Criteria(GetType(SparePartMaster), "ModelCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        End If
        If cfRole.ColumnName <> "ALL" Then
            Dim myArray() As String = Split(cfRole.KeyWord, ";")
            If myArray.Length > 1 Then
                If cfRole.ColumnName = dtgRole.Columns(7).SortExpression Then
                    For i As Integer = 0 To myArray.Length - 1
                        Select Case (myArray(i).ToUpper)
                            Case "AKTIF"
                                If i = 0 Then
                                    crit.opAnd(New Criteria(GetType(SparePartMaster), cfRole.ColumnName, cfRole.OperatorName, 1), "(", True)
                                ElseIf i = myArray.Length - 1 Then
                                    crit.opOr(New Criteria(GetType(SparePartMaster), cfRole.ColumnName, cfRole.OperatorName, 1), ")", False)
                                Else
                                    crit.opOr(New Criteria(GetType(SparePartMaster), cfRole.ColumnName, cfRole.OperatorName, 1))
                                End If
                            Case "TIDAK AKTIF"
                                If i = 0 Then
                                    crit.opAnd(New Criteria(GetType(SparePartMaster), cfRole.ColumnName, cfRole.OperatorName, 0), "(", True)
                                ElseIf i = myArray.Length - 1 Then
                                    crit.opOr(New Criteria(GetType(SparePartMaster), cfRole.ColumnName, cfRole.OperatorName, 0), ")", False)
                                Else
                                    crit.opOr(New Criteria(GetType(SparePartMaster), cfRole.ColumnName, cfRole.OperatorName, 0))
                                End If
                        End Select
                    Next
                Else
                    For i As Integer = 0 To myArray.Length - 1
                        If i = 0 Then
                            crit.opAnd(New Criteria(GetType(SparePartMaster), cfRole.ColumnName, cfRole.OperatorName, myArray(i)), "(", 1)
                        ElseIf i = myArray.Length - 1 Then
                            crit.opOr(New Criteria(GetType(SparePartMaster), cfRole.ColumnName, cfRole.OperatorName, myArray(i)), ")", 0)
                        Else
                            crit.opOr(New Criteria(GetType(SparePartMaster), cfRole.ColumnName, cfRole.OperatorName, myArray(i)))
                        End If
                    Next
                End If
            Else
                If cfRole.ColumnName = dtgRole.Columns(7).SortExpression Then
                    Select Case (myArray(0).ToUpper)
                        Case "AKTIF"
                            crit.opAnd(New Criteria(GetType(SparePartMaster), cfRole.ColumnName, cfRole.OperatorName, 1))
                        Case "TIDAK AKTIF"
                            crit.opAnd(New Criteria(GetType(SparePartMaster), cfRole.ColumnName, cfRole.OperatorName, 0))
                        Case Else
                            crit.opAnd(New Criteria(GetType(SparePartMaster), cfRole.ColumnName, cfRole.OperatorName, myArray(0)))
                    End Select
                Else
                    crit.opAnd(New Criteria(GetType(SparePartMaster), cfRole.ColumnName, cfRole.OperatorName, myArray(0)))
                End If
            End If
        End If
        sHSP.SetSession("FrmWarrantyPartAccessories.criteria", crit)
    End Sub

    Private Sub cfRole_Filter(ByVal sender As Object, ByVal FilterArg As FilterCompositeControl.OnFilterArgs) Handles cfRole.Filter
        Cari()
    End Sub

    Protected Sub dtgRole_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgRole.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim RowValue As SparePartMaster = CType(e.Item.DataItem, SparePartMaster)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblNomorBarang As Label = CType(e.Item.FindControl("lblNomorBarang"), Label)
            Dim lblNamaBarang As Label = CType(e.Item.FindControl("lblNamaBarang"), Label)
            Dim lblProduk As Label = CType(e.Item.FindControl("lblProduk"), Label)
            Dim lblModel As Label = CType(e.Item.FindControl("lblModel"), Label)
            Dim lblHargaEceran As Label = CType(e.Item.FindControl("lblHargaEceran"), Label)
            Dim lblBarangPengganti As Label = CType(e.Item.FindControl("lblBarangPengganti"), Label)
            Dim lblTipe As Label = CType(e.Item.FindControl("lblTipe"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)

            lblNo.Text = e.Item.ItemIndex + 1 + (dtgRole.CurrentPageIndex * dtgRole.PageSize)
            lblNomorBarang.Text = RowValue.PartNumber
            lblNamaBarang.Text = RowValue.PartName
            lblProduk.Text = RowValue.ProductCategory.Code
            lblModel.Text = RowValue.ModelCode
            lblHargaEceran.Text = RowValue.RetalPrice.ToString("#,##0")
            lblBarangPengganti.Text = RowValue.AltPartNumber
            lblTipe.Text = RowValue.ProductType
            If RowValue.ActiveStatus = 0 Then
                lblStatus.Text = "Aktif"
            ElseIf RowValue.ActiveStatus = 1 Then
                lblStatus.Text = "Tidak Aktif"
            End If
            'lblStatus.Text = IIf(RowValue.ActiveStatus = 0, "Aktif", "Tidak Aktif")

            SetSparePartMasterAltButton(e, RowValue.AltPartNumber)
        End If
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Cari()
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        Dim crits As CriteriaComposite
        If dtgRole.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        If Not IsNothing(sHSP.GetSession("FrmWarrantyPartAccessories.criteria")) Then
            crits = CType(sHSP.GetSession("FrmWarrantyPartAccessories.criteria"), CriteriaComposite)
        End If
        ' mengambil data yang dibutuhkan
        arrData = New SparePartMasterFacade(User).Retrieve(crits)
        If arrData.Count > 0 Then
            CreateExcel("Master Data -  Sparepart & Accessories Warranty", arrData)
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
            ws.Cells("B3").Value = "Nomor Barang"
            ws.Cells("C3").Value = "Nama Barang"
            ws.Cells("D3").Value = "Produk"
            ws.Cells("E3").Value = "Model"
            ws.Cells("F3").Value = "Harga Eceran"
            ws.Cells("G3").Value = "Barang Pengganti"
            ws.Cells("H3").Value = "Tipe"
            ws.Cells("I3").Value = "Status"

            For i As Integer = 0 To Data.Count - 1
                Dim oSparePartMaster As SparePartMaster = Data(i)
                ws.Cells(i + 4, 1).Value = i + 1
                ws.Cells(i + 4, 2).Value = oSparePartMaster.PartNumber
                ws.Cells(i + 4, 3).Value = oSparePartMaster.PartName
                If IsNothing(oSparePartMaster.ProductCategory) Then
                    ws.Cells(i + 4, 4).Value = ""
                Else
                    ws.Cells(i + 4, 4).Value = oSparePartMaster.ProductCategory.Code
                End If
                ws.Cells(i + 4, 5).Value = oSparePartMaster.ModelCode
                ws.Cells(i + 4, 6).Value = oSparePartMaster.RetalPrice.ToString("#,##0")
                ws.Cells(i + 4, 7).Value = oSparePartMaster.AltPartNumber
                ws.Cells(i + 4, 8).Value = oSparePartMaster.ProductType
                ws.Cells(i + 4, 9).Value = IIf(oSparePartMaster.ActiveStatus = 0, "Aktif", "Tidak Aktif")
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

    Protected Sub dtgRole_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgRole.PageIndexChanged
        Cari(e.NewPageIndex)
    End Sub

    Private Sub SetSparePartMasterAltButton(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs, ByVal altNumer As String)
        If altNumer.Trim.Length > 0 Then
            CType(e.Item.FindControl("btnLihat"), LinkButton).Attributes("onclick") = GeneralScript.GetPopUpEventReference( _
                      "../SparePart/frmSparePartMasterWarranty.aspx?NoSparePartAlt=" & altNumer + "", "", 600, 800, "null")
        Else
            CType(e.Item.FindControl("btnLihat"), LinkButton).Visible = False
        End If
    End Sub
End Class