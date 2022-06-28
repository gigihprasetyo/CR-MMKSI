#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Salesman
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

Public Class FrmListSalesmanGrade
    Inherits System.Web.UI.Page

    Private helpers As TrainingHelpers = New TrainingHelpers(Me.Page, "Tenaga Penjual - Daftar Grade Salesman")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.view, SR.TrainingSalesViewSalesmanGrade_Privilege)
        helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.fullAccess, SR.TrainingSalesEditSalesmanGrade_Privilege)
        helpers.Privilage()
        If Not IsPostBack Then
            TitleDescription()
            InitPage()
            ViewState("CurrentSortColumn") = "SalesmanHeader.SalesmanCode"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            ViewState("Mode") = "insert"
            If IsNothing(helpers.GetSession("criterias")) Then
                CreateCriteria()
            End If
            BindDataGrid()
        End If
    End Sub

    Private Sub InitPage()
        If Me.IsDealer Then
            lblPopUpDealer.Visible = False
            txtKodeDealer.Text = Me.GetDealer.DealerCode + " - " + Me.GetDealer.DealerName
            txtKodeDealer.Disabled()
        End If
        BindDDLPeriode()
        BindDDLGrade()
        lblPopUpDealer.AddOnClick("ShowPPDealerSelection();")
        btnSimpan.Visible = helpers.IsEdit
        btnBatal.Visible = helpers.IsEdit
    End Sub

    Private Sub BindDDLPeriode()
        ddlPeriode.Enabled = True
        ddlPeriode.ClearSelection()
        ddlPeriode.Items.Clear()
        ddlPeriode.Items.Add(New ListItem("Silahkan pilih", "-1"))
        Dim arrPeriode As ArrayList = New StandardCodeFacade(Me.User).RetrieveByCategory("GradePeriode")
        For Each iPeriode As StandardCode In arrPeriode
            ddlPeriode.Items.Add(New ListItem(iPeriode.ValueCode, iPeriode.ValueId.ToString()))
        Next
        ddlPeriode.SelectedIndex = 0
    End Sub

    Private Sub BindDDLGrade()
        ddlGrade.Enabled = True
        ddlGrade.ClearSelection()
        ddlGrade.Items.Clear()
        ddlGrade.Items.Add(New ListItem("Silahkan pilih", "-1"))
        Dim arrGrade As ArrayList = New StandardCodeFacade(Me.User).RetrieveByCategory("SalesmanGrade")
        For Each iGrade As StandardCode In arrGrade
            ddlGrade.Items.Add(New ListItem(iGrade.ValueCode, iGrade.ValueId.ToString()))
        Next
        ddlGrade.SelectedIndex = 0
    End Sub

    Private Sub BindDataGrid(Optional ByVal pageIndex As Integer = 0)
        Dim totalRow As Integer = 0
        dtgSalesmanGrade.DataSource = New SalesmanGradeFacade(Me.User).RetrieveActiveList(CType(helpers.GetSession("criterias"), CriteriaComposite), pageIndex, _
                                      dtgSalesmanGrade.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
                                      CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgSalesmanGrade.VirtualItemCount = totalRow
        dtgSalesmanGrade.DataBind()
    End Sub

    Private Sub CreateCriteria()
        Dim criteria As New CriteriaComposite(New Criteria(GetType(SalesmanGrade), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If Me.IsKTB Then
            If txtKodeDealer.IsNotEmpty Then
                Dim dealerInSet As String = txtKodeDealer.Text.GenerateInSet()
                criteria.opAnd(New Criteria(GetType(SalesmanGrade), "SalesmanHeader.Dealer.DealerCode", MatchType.InSet, String.Format("({0})", dealerInSet)))
            End If
        End If

        If txtSalesmanCode.IsNotEmpty Then
            criteria.opAnd(New Criteria(GetType(SalesmanGrade), "SalesmanHeader.SalesmanCode", MatchType.Exact, txtSalesmanCode.Text))
        End If

        If txtSalesmanName.IsNotEmpty Then
            criteria.opAnd(New Criteria(GetType(SalesmanGrade), "SalesmanHeader.Name", MatchType.Partial, txtSalesmanName.Text))
        End If

        If txttahun.IsNotEmpty Then
            criteria.opAnd(New Criteria(GetType(SalesmanGrade), "Year", MatchType.Exact, txttahun.Text))
        End If

        If ddlPeriode.IsSelected Then
            criteria.opAnd(New Criteria(GetType(SalesmanGrade), "Period", MatchType.Exact, ddlPeriode.SelectedValue))
        End If

        If ddlGrade.IsSelected Then
            criteria.opAnd(New Criteria(GetType(SalesmanGrade), "Grade", MatchType.Exact, ddlGrade.SelectedValue))
        End If
        helpers.SetSession("criterias", criteria)

    End Sub

    Private Sub TitleDescription()
        lblPageTitle.Text = "Tenaga Penjual - Daftar Grade Salesman"
    End Sub

    Private Sub dtgSalesmanGrade_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgSalesmanGrade.ItemCommand
        Select Case e.CommandName.ToLower
            Case "detail"
                Dim hdnID As HiddenField = e.Item.FindControl("hdnID")
                Dim rowValue As SalesmanGrade = New SalesmanGradeFacade(Me.User).Retrieve(CInt(hdnID.Value))
                If Me.IsKTB Then
                    txtKodeDealer.Text = rowValue.SalesmanHeader.Dealer.DealerCode
                    txtKodeDealer.Disabled()
                End If
                txtSalesmanCode.Text = rowValue.SalesmanHeader.SalesmanCode
                txtSalesmanName.Text = rowValue.SalesmanHeader.Name
                txttahun.Text = rowValue.Year.ToString
                ddlGrade.SelectedValue = rowValue.Grade.ToString
                ddlPeriode.SelectedValue = rowValue.Period.ToString
                If rowValue.Score > 0 Then
                    txtScore.Text = rowValue.Score.ToString()
                End If


                txtSalesmanCode.Disabled()
                txtSalesmanName.Disabled()
                txttahun.Disabled()
                txtScore.Disabled()
                btnSimpan.Enabled = False
                ddlGrade.Enabled = False
                ddlPeriode.Enabled = False
                dtgSalesmanGrade.SelectedIndex = e.Item.ItemIndex
            Case "edit"
                ViewState("Mode") = "edit"
                Dim hdnID As HiddenField = e.Item.FindControl("hdnID")
                Dim rowValue As SalesmanGrade = New SalesmanGradeFacade(Me.User).Retrieve(CInt(hdnID.Value))
                If Me.IsKTB Then
                    txtKodeDealer.Text = rowValue.SalesmanHeader.Dealer.DealerCode
                    txtKodeDealer.Disabled()
                End If
                If rowValue.Score > 0 Then
                    txtScore.Text = rowValue.Score.ToString()
                End If
                txtSalesmanCode.Disabled()
                txtSalesmanName.Disabled()
                hdnGradeID.Value = hdnID.Value
                txtSalesmanCode.Text = rowValue.SalesmanHeader.SalesmanCode
                txtSalesmanName.Text = rowValue.SalesmanHeader.Name
                txttahun.Text = rowValue.Year.ToString
                ddlGrade.SelectedValue = rowValue.Grade.ToString
                ddlPeriode.SelectedValue = rowValue.Period.ToString
                btnSimpan.Enabled = True
                dtgSalesmanGrade.SelectedIndex = e.Item.ItemIndex
            Case "delete"
                Dim func As New SalesmanGradeFacade(Me.User)
                Try
                    Dim hdnID As HiddenField = e.Item.FindControl("hdnID")
                    Dim rowValue As SalesmanGrade = func.Retrieve(CInt(hdnID.Value))
                    func.Delete(rowValue)
                    MessageBox.Show(SR.DeleteSucces)

                    BindDataGrid()
                Catch ex As Exception
                    MessageBox.Show(SR.DeleteFail)
                End Try
        End Select
    End Sub

    Private Sub dtgSalesmanGrade_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgSalesmanGrade.ItemDataBound
        If e.IsRowItems Then
            Dim hdnID As HiddenField = e.FindHiddenField("hdnID")
            Dim lblNo As Label = e.FindLabel("lblNo")
            Dim lblSalesmanCode As Label = e.FindLabel("lblSalesmanCode")
            Dim lblKodeDealer As Label = e.FindLabel("lblKodeDealer")
            Dim lblNama As Label = e.FindLabel("lblNama")
            Dim lblTahun As Label = e.FindLabel("lblTahun")
            Dim lblPosisi As Label = e.FindLabel("lblPosisi")
            Dim lblPeriode As Label = e.FindLabel("lblPeriode")
            Dim lblGrade As Label = e.FindLabel("lblGrade")
            Dim lblScore As Label = e.FindLabel("lblProduktivity")
            Dim btnEdit As LinkButton = e.FindLinkButton("btnEdit")
            Dim btnHapus As LinkButton = e.FindLinkButton("btnHapus")
            Dim rowValue As SalesmanGrade = e.DataItem(Of SalesmanGrade)()
            btnHapus.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")

            If Not IsNothing(rowValue) Then
                lblNo.Text = e.CreateNumberPage
                hdnID.Value = rowValue.ID.ToString
                lblKodeDealer.Text = rowValue.SalesmanHeader.Dealer.DealerCode
                lblSalesmanCode.Text = rowValue.SalesmanHeader.SalesmanCode
                lblNama.Text = rowValue.SalesmanHeader.Name
                lblPosisi.Text = rowValue.SalesmanHeader.JobPosition.Description
                lblPeriode.Text = ddlPeriode.Items.FindByValue(rowValue.Period.ToString()).Text
                lblGrade.Text = ddlGrade.Items.FindByValue(rowValue.Grade.ToString()).Text
                lblTahun.Text = rowValue.Year.ToString()
                If rowValue.Score > 0 Then
                    lblScore.Text = rowValue.Score.ToString()
                Else
                    lblScore.Text = "-"
                End If

                btnEdit.Visible = helpers.IsEdit
                btnHapus.Visible = helpers.IsEdit
            End If
        End If
    End Sub


    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        CreateCriteria()
        BindDataGrid()
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        ViewState("Mode") = "insert"
        hdnGradeID.Value = String.Empty
        If Me.IsKTB Then
            txtKodeDealer.Clear()
        End If
        txtSalesmanCode.Clear()
        txtSalesmanName.Clear()
        txttahun.Clear()
        txtScore.Clear()
        BindDDLPeriode()
        BindDDLGrade()
        btnSimpan.Enabled = True
        dtgSalesmanGrade.SelectedIndex = -1
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim data As New SalesmanGrade
        Dim sH As SalesmanHeader = New SalesmanHeaderFacade(Me.User).Retrieve(txtSalesmanCode.Text.Trim)

        If Not ddlPeriode.IsSelected Then
            MessageBox.Show("Silahkan pilih periode")
            Return
        End If
        If Not ddlGrade.IsSelected Then
            MessageBox.Show("silahkan pilih grade")
            Return
        End If

        If String.IsNullorEmpty(txtScore.Text) AndAlso CInt(txtScore.Text) < 0 AndAlso CInt(txtScore.Text) > 100 Then
            MessageBox.Show("Isi Productivity 1 - 100")
            Return
        End If


        If IsNothing(sH) Then
            MessageBox.Show("Kode tidak terdaftar")
            Return
        Else
            If Not sH.Status = CType(EnumSalesmanStatus.SalesmanStatus.Aktif, String) Then
                MessageBox.Show("Status salesman tidak aktif")
                Return
            Else
                Dim listJobPosition As List(Of JobPositionToCategory) = New JobPositionToCategoryFacade(Me.User).RetrieveByCategory("1").Cast(Of  _
                                                JobPositionToCategory).ToList()
                If Not listJobPosition.Where(Function(x) x.JobPosition.ID = sH.JobPosition.ID).IsData Then
                    MessageBox.Show("Salesman bukan dari kategori sales")
                    Return
                End If
            End If
        End If
        
        Dim iRest As Integer
        If CType(ViewState("Mode"), String) = "insert" Then
            data.SalesmanHeader = sH
            data.Year = CInt(txttahun.Text)
            data.Period = CInt(ddlPeriode.SelectedValue)
            data.Grade = CInt(ddlGrade.SelectedValue)
            data.Score = Decimal.Parse(txtScore.Text)
            data.Status = 1
            iRest = New SalesmanGradeFacade(Me.User).Insert(data)
        Else
            data = New SalesmanGradeFacade(Me.User).Retrieve(CInt(hdnGradeID.Value))
            data.Year = CInt(txttahun.Text)
            data.Period = CInt(ddlPeriode.SelectedValue)
            data.Grade = CInt(ddlGrade.SelectedValue)
            data.Score = Decimal.Parse(txtScore.Text)
            iRest = New SalesmanGradeFacade(Me.User).Update(data)
        End If

        If iRest.Equals(-1) Then
            MessageBox.Show(SR.SaveFail)
        Else
            MessageBox.Show(SR.SaveSuccess)
            btnBatal_Click(sender, e)
            BindDataGrid(dtgSalesmanGrade.CurrentPageIndex)
        End If

    End Sub

    Private Sub dtgSalesmanGrade_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgSalesmanGrade.PageIndexChanged
        BindDataGrid(e.NewPageIndex)
    End Sub

    Private Sub dtgSalesmanGrade_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgSalesmanGrade.SortCommand
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
        dtgSalesmanGrade.SelectedIndex = -1
        BindDataGrid(dtgSalesmanGrade.CurrentPageIndex)
    End Sub

    Private Sub CreateHeaderColumn(ByVal wsData As ExcelWorksheet, ByVal rowIdx As Integer)
        Dim columnIdx As Integer = 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("No")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Kode Dealer")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Kode")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Nama")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Posisi")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Tahun Fiskal")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Periode")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Grade")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Productivity")
        columnIdx += 1
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Using excelPackage As New ExcelPackage()
            Dim wsData As ExcelWorksheet = excelPackage.Workbook.Worksheets.Add("Data Salesman Grade")
            Dim rowIdx As Integer = 1
            rowIdx += 1

            Dim noUrut As Integer = 1
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(CType(ViewState("CurrentSortColumn"), String))) And (Not IsNothing(CType(ViewState("CurrentSortDirect"), Sort.SortDirection))) Then
                sortColl.Add(New Sort(GetType(SalesmanGrade), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
            Else
                sortColl = Nothing
            End If
            Dim arrGrade As ArrayList = New SalesmanGradeFacade(Me.User).Retrieve(CType(helpers.GetSession("criterias"), CriteriaComposite), sortColl)

            CreateHeaderColumn(wsData, rowIdx)
            rowIdx += 1
            For Each iGrade As SalesmanGrade In arrGrade
                Dim clmidx As Integer = 1
                Dim rowValue As SalesmanHeader = iGrade.SalesmanHeader

                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(noUrut.ToString(), Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(rowValue.Dealer.DealerCode, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(rowValue.SalesmanCode, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(rowValue.Name, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(rowValue.JobPosition.Description, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(iGrade.Year.ToString(), Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(ddlPeriode.Items.FindByValue(iGrade.Period.ToString()).Text, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(ddlGrade.Items.FindByValue(iGrade.Grade.ToString()).Text, Style.ExcelHorizontalAlignment.Center)
                If iGrade.Score > 0 Then
                    wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(ddlGrade.Items.FindByValue(iGrade.Score.ToString()).Text, Style.ExcelHorizontalAlignment.Center)
                    clmidx += 1
                End If
                rowIdx += 1
                noUrut += 1
            Next
            rowIdx += 1
            rowIdx += 1
            For colIdx As Integer = 2 To 8
                wsData.Column(colIdx).AutoFit()
            Next

            Dim fileBytes = excelPackage.GetAsByteArray()
            Dim fileName As String = String.Format("DataSalesmanGrade.xls")

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
End Class