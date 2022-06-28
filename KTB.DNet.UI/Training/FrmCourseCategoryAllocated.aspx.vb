#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports OfficeOpenXml
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions
#End Region

#Region ".NET Namespace Imports"
Imports System.Text
Imports System.IO
#End Region

Public Class FrmCourseCategoryAllocated
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

    End Sub

    Private ReadOnly Property AreaId As String
        Get
            Return Request.QueryString("area")
        End Get
    End Property

    Enum GridActive
        None = 0
        CreateAndUpdate = 1
        Show = 2
        Upload = 3
    End Enum

#End Region

    Private helpers As TrainingHelpers = New TrainingHelpers(Me.Page)
    Private bPrivilegeChangeAllocation As Boolean = False

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Page.Server.ScriptTimeout = 2000
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        helpers.CheckPrivilege("priv7B")
        If Not Page.IsPostBack Then
            TitleDescription(AreaId)
            ViewState("CurrentSortColumn") = "Dealer.DealerCode"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection()"
            lblSearchKodeKategori.Attributes("onclick") = "ShowPPCourseSelection2()"
            ActiveGrid(GridActive.None)
            SetActiveControl(helpers.IsEdit)
        End If
    End Sub

    Private Sub SetActiveControl(ByVal isActive As Boolean)
        trUpload.Visible = isActive
        trTemplete.Visible = isActive
        btnSetAllocation.Visible = isActive
        btnClear.Visible = isActive
    End Sub

    Private Sub TitleDescription(ByVal areaid As String)
        If areaid.Equals("1") Then
            lblTitle.Text = "Training Sales - Alokasi"
            hdnCategory.Value = "sales"
        ElseIf areaid.Equals("2") Then
            lblTitle.Text = "Training After Sales - Alokasi"
            hdnCategory.Value = "ass"
        Else
            lblTitle.Text = "Training Customer Satisfaction - Alokasi"
            hdnCategory.Value = "cs"
        End If
        hdnArea.Value = areaid
    End Sub

    Protected Sub linkTemplate_Click(sender As Object, e As EventArgs) Handles linkTemplate.Click
        Dim template As ExcelTemplate = New ExcelTemplate(Me.Page)
        template.FileName = "Template_UploadAlokasi.xlsx"
        template.SheetName = "UploadAlokasi"
        template.Judul = "Upload Alokasi per kategori kursus"
        template.AddField(1, "Kode Kategori Kursus")
        template.AddField(2, "Kode Dealer")
        template.AddField(3, "Alokasi")
        template.DownLoad()
    End Sub

    Protected Sub btnSetAllocation_Click(sender As Object, e As EventArgs) Handles btnSetAllocation.Click
        If txtKodeDealer.IsEmpty() Then
            MessageBox.Show("Kode Dealer kosong")
            Exit Sub
        End If
        ActiveGrid(GridActive.CreateAndUpdate)
        SaveCriteria()
        Dim datas As ArrayList = GetDataAllocation()
        If datas.Count.Equals(0) Then
            dtgTrCourseAllocation.PageSize = 25
        Else
            dtgTrCourseAllocation.PageSize = datas.Count
        End If

        dtgTrCourseAllocation.DataSource = datas
        dtgTrCourseAllocation.DataBind()
    End Sub

    Private Sub SaveCriteria()
        helpers.AddCriteria("kategorikursus", txtKodeKategori.Text)
        helpers.AddCriteria("kodeDealer", txtKodeDealer.Text)
        helpers.SaveCriteria()
    End Sub

    Private Sub ReadCriteria()
        If Not helpers.IsNullCriteria Then
            txtKodeKategori.Text = helpers.GetStringCriteria("kategorikursus")
            txtKodeDealer.Text = helpers.GetStringCriteria("kodeDealer")
        End If

    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        ActiveGrid(GridActive.Show)
        BindingGridCari(0)
    End Sub

    Private Sub BindingGridCari(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim kategoriInSet As String = txtKodeKategori.Text.GenerateInSet()
        Dim dealerInSet As String = txtKodeDealer.Text.GenerateInSet()

        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseCategoryAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(GetType(TrCourseCategoryAllocation), "TrCourseCategory.JobPositionCategory.AreaID", MatchType.Exact, CInt(AreaId))
        If Not String.IsNullorEmpty(txtKodeKategori.Text) Then
            criteria.opAnd(New Criteria(GetType(TrCourseCategoryAllocation), "TrCourseCategory.Code", MatchType.InSet, String.Format("({0})", kategoriInSet)))
        End If
        If Not String.IsNullorEmpty(txtKodeDealer.Text) Then
            criteria.opAnd(New Criteria(GetType(TrCourseCategoryAllocation), "Dealer.DealerCode", MatchType.InSet, String.Format("({0})", dealerInSet)))
        End If

        Try
            grid.DataSource = New TrCourseCategoryAllocationFacade(User).RetrieveByCriteria(criteria, indexPage + 1, grid.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            grid.VirtualItemCount = totalRow
            grid.DataBind()
        Catch
            grid.DataSource = New List(Of TrCourseCategory)
            grid.CurrentPageIndex = 0
            grid.DataBind()
            MessageBox.Show(SR.DataNotFound("Data Alokasi"))
            Exit Sub
        End Try

        If totalRow.Equals(0) Then
            MessageBox.Show(SR.DataNotFound("Data Alokasi"))
        End If

    End Sub

    Private Sub ActiveGrid(ByVal isActive As GridActive)
        dtgTrCourseAllocation.Visible = False
        grid.Visible = False
        dtgUpload.Visible = False
        btnSimpan.Visible = True
        btnBatal.Visible = True
        btnDownload.Visible = True
        btnSimpan.Enabled = True
        btnBatal.Enabled = True
        btnDownload.Enabled = False

        Select Case isActive
            Case GridActive.None
                hdnActiveGrid.Value = "none"
                btnSimpan.Visible = False
                btnBatal.Visible = False
                btnDownload.Visible = False
            Case GridActive.CreateAndUpdate
                hdnActiveGrid.Value = "createorupdate"
                dtgTrCourseAllocation.Visible = True
                grid.DataSource = New List(Of TrCourseCategoryAllocation)
                grid.DataBind()
                dtgUpload.DataSource = New List(Of TrCourseCategoryAllocation)
                dtgUpload.DataBind()
            Case GridActive.Show
                hdnActiveGrid.Value = "showOnly"
                grid.Visible = True
                btnDownload.Enabled = True
                btnSimpan.Enabled = False
                btnBatal.Enabled = False
                dtgTrCourseAllocation.DataSource = New List(Of TrCourseCategoryAllocation)
                dtgTrCourseAllocation.DataBind()
                dtgUpload.DataSource = New List(Of TrCourseCategoryAllocation)
                dtgUpload.DataBind()
            Case GridActive.Upload
                hdnActiveGrid.Value = "Upload"
                dtgUpload.Visible = True
                dtgTrCourseAllocation.DataSource = New List(Of TrCourseCategoryAllocation)
                dtgTrCourseAllocation.DataBind()
                grid.DataSource = New List(Of TrCourseCategoryAllocation)
                grid.DataBind()
        End Select
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim fileName As String = String.Empty
        Dim listAllo As List(Of TrCourseCategoryAllocation) = New List(Of TrCourseCategoryAllocation)
        Dim listError As List(Of ErrorExcelUpload) = New List(Of ErrorExcelUpload)
        Dim arrExtension() As String = {".xls", ".xlsx"}
        Dim resultUpload As String = helpers.UploadFile(fileUpload, KTB.DNet.Lib.WebConfig.GetValue("ClassUpload"), _
                                                    KTB.DNet.Lib.WebConfig.GetValue("MaximumClassUploadSize"), arrExtension)
        Dim errArr() As String = resultUpload.Split("|")
        If errArr(0).Equals("Error") Then
            MessageBox.Show(errArr(1))
            Return
        Else
            fileName = KTB.DNet.Lib.WebConfig.GetValue("SAN") & errArr(1)
        End If
        Dim fileInfo As FileInfo = New FileInfo(fileName)
        Using excelPkg As New ExcelPackage(fileInfo)
            Using ws As ExcelWorksheet = excelPkg.Workbook.Worksheets.Item(1)
                Dim ColumnCount As Integer = ws.Dimension.End.Column
                Dim RowCount As Integer = ws.Dimension.End.Row
                If ColumnCount < 3 Then
                    MessageBox.Show("Format Excel Tidak Sesuai. Silahkan download template di link yang telah disediakan")
                    Exit Sub
                End If
                For idx As Integer = 4 To RowCount
                    Dim data As New TrCourseCategoryAllocation
                    data.ID = 0
                    Dim validasi As ExcelValidation = New ExcelValidation(ws)
                    Dim KodeKursus As ExcelField = validasi.Create("Kode Kategori Kursus", idx, 1, "required")
                    Dim KodeDealer As ExcelField = validasi.Create("Kode Dealer", idx, 2, "required")
                    Dim alokasi As ExcelField = validasi.Create("Alokasi", idx, 3, "required,numeric")

                    Dim listErrorfield As List(Of ErrorExcelUpload) = validasi.Validate()
                    If Not listErrorfield.Count.Equals(0) Then
                        listError.AddRange(listErrorfield)
                        Continue For
                    End If
                    Dim isValid As Boolean = True
                    Dim courseCtg As TrCourseCategory = New TrCourseCategoryFacade(User).Retrieve(KodeKursus.Value)
                    If courseCtg.ID.Equals(0) Then
                        listErrorfield.Add(validasi.CreateCustomError(KodeKursus, "tidak ditemukan."))
                        isValid = False
                    Else
                        If Not courseCtg.JobPositionCategory.AreaID.Equals(CInt(AreaId)) Then
                            listErrorfield.Add(validasi.CreateCustomError(KodeKursus, "tidak ditemukan."))
                            isValid = False
                        End If
                    End If

                    Dim dealer As Dealer = New DealerFacade(User).Retrieve(KodeDealer.Value)
                    If dealer.ID.Equals(0) Then
                        listErrorfield.Add(validasi.CreateCustomError(KodeDealer, "tidak ditemukan."))
                        isValid = False
                    End If

                    If isValid Then
                        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseCategoryAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteria.opAnd(GetType(TrCourseCategoryAllocation), "TrCourseCategory.JobPositionCategory.AreaID", MatchType.Exact, CInt(AreaId))
                        criteria.opAnd(New Criteria(GetType(TrCourseCategoryAllocation), "TrCourseCategory.Code", MatchType.Exact, KodeKursus.Value))
                        criteria.opAnd(New Criteria(GetType(TrCourseCategoryAllocation), "Dealer.DealerCode", MatchType.Exact, KodeDealer.Value))

                        Dim datasAllo As ArrayList = New TrCourseCategoryAllocationFacade(User).Retrieve(criteria)
                        If datasAllo.Count > 0 Then
                            'Dim dataCurr As TrCourseCategoryAllocation = CType(datasAllo(0), TrCourseCategoryAllocation)
                            data = CType(datasAllo(0), TrCourseCategoryAllocation)
                            'listErrorfield.Add(validasi.CreateCustomError(KodeKursus, "sudah ditambahkan."))
                        End If
                    End If

                    If Not listErrorfield.Count.Equals(0) Then
                        listError.AddRange(listErrorfield)
                        Continue For
                    End If

                    If listError.Count.Equals(0) Then
                        If data.ID <> 0 Then
                            data.LastAllocated = data.Allocated
                        End If
                        data.Dealer = dealer
                        data.TrCourseCategory = courseCtg
                        data.Allocated = CInt(alokasi.Value)

                        listAllo.Add(data)
                    End If
                Next
                If Not listError.Count.Equals(0) Then
                    ActiveGrid(GridActive.None)
                    helpers.SetSession("namaFile", fileUpload.PostedFile.FileName)
                    helpers.SetSession("dataError", listError)
                    Me.Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "document.getElementById('btnShowPopup').click();", True)
                Else
                    If helpers.GetSession("namaFile") IsNot Nothing Then
                        helpers.RemoveSession("namaFile")
                    End If
                    If helpers.GetSession("dataError") IsNot Nothing Then
                        helpers.RemoveSession("dataError")
                    End If
                    ActiveGrid(GridActive.Upload)
                    helpers.SetSession("dataUpload", listAllo)
                    dtgUpload.DataSource = listAllo
                    dtgUpload.VirtualItemCount = listAllo.Count
                    dtgUpload.DataBind()
                    btnUpload.Enabled = True
                End If
            End Using
        End Using
    End Sub

    Private Function GetDataAllocation() As ArrayList
        Dim dealerSetIn As String = String.Empty
        Dim categorySetIn As String = String.Empty

        If txtKodeDealer.IsNotEmpty Then
            dealerSetIn = txtKodeDealer.Text.GenerateInSet()
        End If
        If txtKodeKategori.IsNotEmpty Then
            categorySetIn = txtKodeKategori.Text.GenerateInSet()
        End If

        Return New TrCourseCategoryAllocationFacade(User).RetrieveAllocation(CInt(AreaId), dealerSetIn, categorySetIn)
    End Function

    Private Sub dtgTrCourseAllocation_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgTrCourseAllocation.ItemCommand
        Select Case e.CommandName.ToLower()
            Case "delete"
                Dim idCourseAllo As Integer = CInt(e.Item.Cells(0).Text)
                Dim dataDelete As TrCourseCategoryAllocation
                Dim datas As List(Of TrCourseCategoryAllocation) = GetDataGridAllocation()

                If idCourseAllo.Equals(0) Then
                    Dim lblCourseCategoryCode As Label = CType(e.Item.FindControl("lblCourseCategoryCode"), Label)
                    Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                    dataDelete = datas.FirstOrDefault(Function(x) _
                                x.Dealer.DealerCode = lblDealerCode.Text And _
                                x.TrCourseCategory.Code = lblCourseCategoryCode.Text)

                Else
                    Dim data As TrCourseCategoryAllocation = New TrCourseCategoryAllocationFacade(User).Retrieve(idCourseAllo)
                    data.RowStatus = CType(DBRowStatus.Deleted, Short)
                    Dim func As TrCourseCategoryAllocationFacade = New TrCourseCategoryAllocationFacade(User)
                    func.Update(data)
                    dataDelete = datas.FirstOrDefault(Function(x) _
                                x.Dealer.DealerCode = data.Dealer.DealerCode And _
                                x.TrCourseCategory.Code = data.TrCourseCategory.Code)
                End If

                datas.Remove(dataDelete)
                If datas.Count.Equals(0) Then
                    dtgTrCourseAllocation.PageSize = 25
                Else
                    dtgTrCourseAllocation.PageSize = datas.Count
                End If

                dtgTrCourseAllocation.DataSource = datas
                dtgTrCourseAllocation.DataBind()
        End Select
    End Sub

    Private Sub dtgTrCourseAllocation_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgTrCourseAllocation.ItemDataBound
        If e.IsRowItems Then
            e.Item.Cells(2).Text = e.CreateNumberPage()
            Dim data As TrCourseCategoryAllocation = CType(e.Item.DataItem, TrCourseCategoryAllocation)
            Dim lblUpdate As Label = CType(e.Item.FindControl("lblLastUpdateTime"), Label)
            If Not e.Item.FindControl("txtAllocated") Is Nothing Then
                CType(e.Item.FindControl("txtAllocated"), TextBox).Text = data.Allocated
            End If
            lblUpdate.Text = data.LastUpdateTime.DateToString()

        End If
    End Sub

    Private Sub grid_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles grid.ItemCommand

    End Sub

    Private Sub grid_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles grid.ItemDataBound
        If e.IsRowItems Then
            e.Item.Cells(0).Text = e.CreateNumberPage()
            Dim data As TrCourseCategoryAllocation = e.DataItem(Of TrCourseCategoryAllocation)()
            Dim lblUpdate As Label = CType(e.Item.FindControl("lblLastUpdateTimeInGrid"), Label)

            lblUpdate.Text = data.LastUpdateTime.DateToString
        End If
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Try
            Select Case hdnActiveGrid.Value.ToLower()
                Case "createorupdate"
                    Dim dataAllocation As List(Of TrCourseCategoryAllocation) = GetDataGridAllocation()
                    Dim func As TrCourseCategoryAllocationFacade = New TrCourseCategoryAllocationFacade(User)
                    For Each itemAllocation As TrCourseCategoryAllocation In dataAllocation
                        If itemAllocation.ID.Equals(0) Then
                            func.Insert(itemAllocation)
                        Else
                            Dim dataExist As TrCourseCategoryAllocation = New TrCourseCategoryAllocationFacade(User).Retrieve(itemAllocation.ID)
                            If Not dataExist.Allocated.Equals(itemAllocation.Allocated) Then
                                itemAllocation.LastAllocated = dataExist.Allocated
                            End If
                            func.Update(itemAllocation)
                        End If
                    Next
                Case "upload"
                    Dim dataAlloc As List(Of TrCourseCategoryAllocation) = CType(helpers.GetSession("dataUpload"), List(Of TrCourseCategoryAllocation))
                    Dim func As TrCourseCategoryAllocationFacade = New TrCourseCategoryAllocationFacade(User)
                    For Each itemAllocation As TrCourseCategoryAllocation In dataAlloc
                        If itemAllocation.ID = 0 Then
                            func.Insert(itemAllocation)
                        Else
                            func.Update(itemAllocation)
                        End If
                    Next
            End Select
            ActiveGrid(GridActive.Show)
            BindingGridCari(0)
            MessageBox.Show(SR.SaveSuccess)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Function GetDataGridAllocation() As List(Of TrCourseCategoryAllocation)
        Dim result As List(Of TrCourseCategoryAllocation) = New List(Of TrCourseCategoryAllocation)
        For Each itemData As DataGridItem In dtgTrCourseAllocation.Items
            Dim data As TrCourseCategoryAllocation = New TrCourseCategoryAllocation()
            Dim lblCourseCode As Label = CType(itemData.FindControl("lblCourseCategoryCode"), Label)
            Dim lblDealerCode As Label = CType(itemData.FindControl("lblDealerCode"), Label)
            Dim lblLastAllocated As Label = CType(itemData.FindControl("lblLastAllocated"), Label)
            Dim txtAllo As TextBox = CType(itemData.FindControl("txtAllocated"), TextBox)
            data.ID = CInt(itemData.Cells(0).Text)
            data.Dealer = New DealerFacade(User).Retrieve(lblDealerCode.Text)
            data.TrCourseCategory = New TrCourseCategoryFacade(User).Retrieve(lblCourseCode.Text)
            data.Allocated = CInt(txtAllo.Text)
            data.LastAllocated = CInt(lblLastAllocated.Text)
            result.Add(data)
        Next

        Return result
    End Function

    Private Sub grid_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles grid.PageIndexChanged
        grid.SelectedIndex = -1
        grid.CurrentPageIndex = e.NewPageIndex
        BindingGridCari(grid.CurrentPageIndex)
    End Sub

    Private Sub grid_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles grid.SortCommand
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
        grid.SelectedIndex = -1
        grid.CurrentPageIndex = 0
        BindingGridCari(grid.CurrentPageIndex)

    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Dim index As Integer = 1
        Dim kategoriInSet As String = txtKodeKategori.Text.GenerateInSet()
        Dim dealerInSet As String = txtKodeDealer.Text.GenerateInSet()

        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseCategoryAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(GetType(TrCourseCategoryAllocation), "TrCourseCategory.JobPositionCategory.AreaID", MatchType.Exact, CInt(AreaId))
        If Not String.IsNullorEmpty(txtKodeKategori.Text) Then
            criteria.opAnd(New Criteria(GetType(TrCourseCategoryAllocation), "TrCourseCategory.Code", MatchType.InSet, String.Format("({0})", kategoriInSet)))
        End If
        If Not String.IsNullorEmpty(txtKodeDealer.Text) Then
            criteria.opAnd(New Criteria(GetType(TrCourseCategoryAllocation), "Dealer.DealerCode", MatchType.InSet, String.Format("({0})", dealerInSet)))
        End If

        Dim datasAllo As ArrayList = New TrCourseCategoryAllocationFacade(User).Retrieve(criteria)

        Dim template As ExcelTemplate = New ExcelTemplate(Me.Page)
        template.FileName = "TrainingAlokasiDealer.xlsx"
        template.SheetName = "Alokasi"
        template.Judul = "Alokasi Dealer per kategori kursus"
        template.AddField(1, "No")
        template.AddField(2, "Kode Kategori")
        template.AddField(3, "Kode Dealer")
        template.AddField(4, "Nama Dealer")
        template.AddField(5, "Kota")
        template.AddField(6, "Area")
        template.AddField(7, "Jumlah Alokasi")
        template.AddField(8, "Alokasi sebelumnya")
        template.AddField(9, "Update Terakhir")
        template.AddField(10, "Alasan")

        For Each item As TrCourseCategoryAllocation In datasAllo
            Dim tglLastUpdate As String = String.Empty
            If Not item.LastUpdateTime.Equals(New DateTime(1753, 1, 1)) Then
                tglLastUpdate = item.LastUpdateTime.ToString("dd/MM/yyyy")
            End If

            Dim dataRow As List(Of ExcelTemplateColumn) = New List(Of ExcelTemplateColumn)
            dataRow.Add(New ExcelTemplateColumn(1, index.ToString()))
            dataRow.Add(New ExcelTemplateColumn(2, item.TrCourseCategory.Code))
            dataRow.Add(New ExcelTemplateColumn(3, item.Dealer.DealerCode))
            dataRow.Add(New ExcelTemplateColumn(4, item.Dealer.DealerName))
            dataRow.Add(New ExcelTemplateColumn(5, item.Dealer.City.CityName))
            If item.Dealer.MainArea IsNot Nothing Then
                dataRow.Add(New ExcelTemplateColumn(6, item.Dealer.MainArea.Description))
            End If
            dataRow.Add(New ExcelTemplateColumn(7, item.Allocated.ToString()))
            dataRow.Add(New ExcelTemplateColumn(8, item.LastAllocated.ToString()))
            dataRow.Add(New ExcelTemplateColumn(9, tglLastUpdate))
            dataRow.Add(New ExcelTemplateColumn(10, item.CancelReason))

            template.AddValue(index, dataRow)
            index = index + 1
        Next

        template.DownLoad()
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        ActiveGrid(GridActive.CreateAndUpdate)
        ReadCriteria()
        Dim datas As ArrayList = GetDataAllocation()
        If datas.Count.Equals(0) Then
            dtgTrCourseAllocation.PageSize = 25
        Else
            dtgTrCourseAllocation.PageSize = datas.Count
        End If

        dtgTrCourseAllocation.DataSource = datas
        dtgTrCourseAllocation.DataBind()
    End Sub

    Private Sub dtgUpload_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgUpload.ItemCommand
        Select Case e.CommandName.ToLower()
            Case "delete"
                Dim dataAlloc As List(Of TrCourseCategoryAllocation) = CType(helpers.GetSession("dataUpload"), List(Of TrCourseCategoryAllocation))
                Dim lblCourseCtgCode As Label = CType(e.Item.FindControl("lblCourseCtgCode"), Label)
                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)

                Dim dataDelete As TrCourseCategoryAllocation = dataAlloc.FirstOrDefault(Function(x) _
                                  x.Dealer.DealerCode = lblDealerCode.Text And _
                                  x.TrCourseCategory.Code = lblCourseCtgCode.Text)
                dataAlloc.Remove(dataDelete)

                If dataAlloc.Count.Equals(0) Then
                    ActiveGrid(GridActive.None)
                    Exit Sub
                End If

                dtgUpload.DataSource = dataAlloc
                dtgUpload.VirtualItemCount = dataAlloc.Count
                dtgUpload.DataBind()
        End Select
    End Sub

    Private Sub dtgUpload_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgUpload.ItemDataBound
        If e.IsRowItems Then
            Dim rowValue As TrCourseCategoryAllocation = e.DataItem(Of TrCourseCategoryAllocation)()
            e.Item.Cells(0).Text = e.CreateNumberPage()
            Dim hdnID As HiddenField = e.FindHiddenField("hdnID")
            hdnID.Value = rowValue.ID.ToString()
        End If
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Me.Page.ClearTextBoxWithPrefix("txtKode")
        Me.txtKodeKategori.Focus()
    End Sub
End Class