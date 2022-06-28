Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.CallCenter
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports System.IO
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions
Imports OfficeOpenXml


Public Class FrmCcCSPerformanceCluster
    Inherits System.Web.UI.Page

    Public ReadOnly Property CcCSPerformnaceMasterID As Integer
        Get
            Try
                Return CInt(Request.QueryString("masterid"))
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Public ReadOnly Property IsEdit As Boolean
        Get
            Try
                Return IIf(CInt(Request.QueryString("isEdit")) = 1, True, False)
            Catch ex As Exception
                Return False
            End Try
        End Get
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            trAktif.Visible = IsEdit
            BindDdlPeriod(-35, 0)
            ChkBindJenisKendaraan()
            ChkBindTipeDealer()
            ViewState("mode") = "insert"
            InitForm()
        End If
    End Sub


    Private Sub BindDdlPeriod(ByVal minPeriod As Integer, ByVal maxPeriod As Integer)
        Dim DtStart As DateTime = DateTime.Now.AddMonths(minPeriod)
        Dim DtEnd As DateTime = DateTime.Now.AddMonths(maxPeriod)

        Dim criteriaCCperiod As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaCCperiod.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcPeriod), "YearMonth", MatchType.GreaterOrEqual, DtStart.ToString("yyyyMM")))
        criteriaCCperiod.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcPeriod), "YearMonth", MatchType.LesserOrEqual, DtEnd.ToString("yyyyMM")))

        Dim arrayListPeriode As ArrayList = New CcPeriodFacade(User).RetrieveByCriteria(criteriaCCperiod, "YearMonth", Sort.SortDirection.DESC)

        For Each item As CcPeriod In arrayListPeriode
            Dim desc As String = New Date(CInt(item.YearMonth.Substring(0, 4)), CInt(item.YearMonth.Substring(4, 2)), 1).ToString("MMM yyyy")
            Dim listItem As New ListItem(desc, item.ID)
            ddlPeriodeFrom.Items.Add(listItem)
            ddlPeriodeTo.Items.Add(listItem)
        Next
        Dim listitemBlank As New ListItem("Silahkan Pilih", "-1")
        ddlPeriodeFrom.Items.Insert(0, listitemBlank)
        ddlPeriodeTo.Items.Insert(0, listitemBlank)

    End Sub

    Private Sub ChkBindJenisKendaraan()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(CcVehicleCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(CcVehicleCategory), "Description", Sort.SortDirection.ASC))

        Dim myList As List(Of CcVehicleCategory) = New CcVehicleCategoryFacade(User).Retrieve(criterias, sortColl).Cast(Of CcVehicleCategory).ToList()

        chkVehicleType.BindChkList(myList, "ID", "Code")
    End Sub

    Private Sub ChkBindTipeDealer()
        chkDealerType.ClearSelection()
        chkDealerType.Items.Add(New ListItem("PC", "1"))
        chkDealerType.Items.Add(New ListItem("LCV", "2"))
        chkDealerType.Items.Add(New ListItem("PC/LCV", "3"))

    End Sub

    Private Sub InitForm()
        Dim cpMaster As CcCSPerformanceMaster = New CcCSPerformanceMasterFacade(Me.User).Retrieve(CcCSPerformnaceMasterID)
        lblPerformanceMaster.Text = cpMaster.Description

        Dim criterias As New CriteriaComposite(New Criteria(GetType(CcCSPerformanceCluster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(CcCSPerformanceCluster), "CcCSPerformanceMaster.ID", MatchType.Exact, CcCSPerformnaceMasterID))

        Dim arrCPCluster As ArrayList = New CcCSPerformanceClusterFacade(Me.User).Retrieve(criterias)
        If arrCPCluster.Count > 0 Then
            dtgCSPMCluster.DataSource = arrCPCluster.Cast(Of CcCSPerformanceCluster).ToList()
            dtgCSPMCluster.PageSize = arrCPCluster.Count
            dtgCSPMCluster.DataBind()
        Else
            dtgCSPMCluster.DataSource = New List(Of CcCSPerformanceCluster)
            dtgCSPMCluster.DataBind()
        End If
        If IsEdit Then
            trAktif.Visible = True
        End If

    End Sub

    Private Sub dtgCSPMCluster_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgCSPMCluster.ItemCommand
        Try
            Dim funcCluster As New CcCSPerformanceClusterFacade(Me.User)
            Dim funcClsDealer As New CcCSPerformanceClusterDealerFacade(Me.User)
            Dim clusterID As Integer = CInt(e.Item.Cells(0).Text)

            Select Case e.CommandName
                Case "hapus"
                    Dim objCluster As CcCSPerformanceCluster = funcCluster.Retrieve(clusterID)
                    funcCluster.Delete(objCluster)

                    MessageBox.Show(SR.DataDeleted("Cluster"))
                    InitForm()
                Case "edit"
                    ViewState("mode") = "edit"
                    ClearForm()
                    Dim objCluster As CcCSPerformanceCluster = funcCluster.Retrieve(clusterID)
                    hdnClusterID.Value = clusterID.ToString()
                    txtDescription.Text = objCluster.ClusterName
                    txtMinPoint.Text = objCluster.MinPoint.ToString
                    txtMaxPoint.Text = objCluster.MinPoint.ToString
                    ddlPeriodeFrom.SelectedValue = objCluster.StartPeriodCal.ToString
                    ddlPeriodeTo.SelectedValue = objCluster.EndPeriodCal.ToString
                    Dim listVehicleType As List(Of String) = objCluster.VehicleType.Split(", ").ToList()
                    For Each nItem As ListItem In chkVehicleType.Items
                        If listVehicleType.Where(Function(x) x.Trim = nItem.Text.Trim).Count > 0 Then
                            nItem.Selected = True
                        End If
                    Next
                    Dim listDealerType As List(Of String) = objCluster.DealerType.Split(", ").ToList()
                    For Each mItem As ListItem In chkDealerType.Items
                        If listDealerType.Where(Function(x) x.Trim = mItem.Text.Trim).Count > 0 Then
                            mItem.Selected = True
                        End If
                    Next
                    dtgCSPMCluster.SelectedIndex = e.Item.ItemIndex
                Case "generate"
                    Dim jumlahDealer As Integer = funcCluster.GenerateClusterDealer(clusterID, Me.User.Identity.Name)
                    If jumlahDealer > 0 Then
                        MessageBox.Show(String.Format("Generate cluster berhasil, {0} dealer ditambahkan.", jumlahDealer))
                        InitForm()
                    Else
                        MessageBox.Show("Tidak terdapat dealer yang memenuhi kriteria")
                    End If

                Case "add"
                    Dim txtDealer As TextBox = CType(e.Item.FindControl("txtClusterDealer"), TextBox)
                    If txtDealer.IsEmpty Then
                        MessageBox.Show("Dealer harus diisi")
                        Return
                    End If
                    Dim objDealer As Dealer = New DealerFacade(Me.User).Retrieve(txtDealer.Text)
                    If objDealer.ID = 0 Then
                        MessageBox.Show("Kode tidak valid")
                        Return
                    End If
                    Dim rest As Integer = -1
                    Dim objCluster As CcCSPerformanceCluster = funcCluster.Retrieve(clusterID)

                    Dim criterias As New CriteriaComposite(New Criteria(GetType(CcCSPerformanceClusterDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(CcCSPerformanceClusterDealer), "Dealer.DealerCode", MatchType.Exact, txtDealer.Text.Trim))
                    criterias.opAnd(New Criteria(GetType(CcCSPerformanceClusterDealer), "CcCSPerformanceCluster.CcCSPerformanceMaster.ID", MatchType.Exact, Me.CcCSPerformnaceMasterID))

                    Dim arrClusterDealer As ArrayList = funcClsDealer.Retrieve(criterias)
                    If arrClusterDealer.Count = 0 Then
                        Dim objClusterDealer As New CcCSPerformanceClusterDealer
                        objClusterDealer.Dealer = objDealer
                        objClusterDealer.CcCSPerformanceCluster = objCluster
                        rest = funcClsDealer.Insert(objClusterDealer)
                        If rest <> -1 Then
                            MessageBox.Show(SR.SaveSuccess)
                            InitForm()
                        Else
                            MessageBox.Show(SR.SaveFail)
                        End If
                    Else
                        MessageBox.Show("Dealer " + txtDealer.Text + " sudah terdaftar di Cluster " + CType(arrClusterDealer(0),  _
                                        CcCSPerformanceClusterDealer).CcCSPerformanceCluster.ClusterName)
                    End If

            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dtgCSPMCluster_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgCSPMCluster.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim funcPeriod As New CcPeriodFacade(Me.User)
            Dim funcCD As New CcCSPerformanceClusterDealerFacade(Me.User)

            Dim lblPeriod As Label = e.FindLabel("lblPeriod")
            Dim lblBatasPenjualan As Label = e.FindLabel("lblBatasPenjualan")
            Dim lblTipeKalkulasi As Label = e.FindLabel("lblTipeKalkulasi")
            Dim lblTipeKendaraan As Label = e.FindLabel("lblTipeKendaraan")
            Dim lblTipeDealer As Label = e.FindLabel("lblTipeDealer")
            Dim lblTotalDealer As Label = e.FindLabel("lblTotalDealer")
            Dim lblPopUpDealer As Label = e.FindLabel("lblPopUpDealer")
            Dim txtClusterDealer As TextBox = e.FindTextBox("txtClusterDealer")
            Dim lbtnGenerate As LinkButton = e.FindLinkButton("lbtnGenerate")
            Dim lbtnEdit As LinkButton = e.FindLinkButton("lbtnEdit")
            Dim lbtnHapus As LinkButton = e.FindLinkButton("lbtnHapus")
            Dim lblInfo As Label = e.FindLabel("lblInfo")
            Dim dtgDetail As DataGrid = CType(e.Item.FindControl("dtgDetail"), DataGrid)

            Dim rowValue As CcCSPerformanceCluster = e.DataItem(Of CcCSPerformanceCluster)()


            lblPopUpDealer.AddOnClick(String.Format("ShowPPDealerSelection('{0}')", txtClusterDealer.ClientID))
            Dim startPeriod As CcPeriod = funcPeriod.Retrieve(rowValue.StartPeriodCal)
            Dim endPeriod As CcPeriod = funcPeriod.Retrieve(rowValue.EndPeriodCal)

            Dim periodeAwal As String = New Date(CInt(startPeriod.YearMonth.Substring(0, 4)), CInt(startPeriod.YearMonth.Substring(4, 2)), 1).ToString("MMM yyyy")
            Dim periodeAkhir As String = New Date(CInt(endPeriod.YearMonth.Substring(0, 4)), CInt(endPeriod.YearMonth.Substring(4, 2)), 1).ToString("MMM yyyy")
            lblPeriod.Text = periodeAwal + " s/d " + periodeAkhir

            lblBatasPenjualan.Text = rowValue.MinPoint.ToString() + " s/d " + rowValue.MaxPoint.ToString()
            lblTipeKalkulasi.Text = IIf(rowValue.TypeCal = 1, "Rata-rata", "Total")
            lblTipeKendaraan.Text = rowValue.VehicleType
            lblTipeDealer.Text = rowValue.DealerType

            Dim criterias As New CriteriaComposite(New Criteria(GetType(CcCSPerformanceClusterDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CcCSPerformanceClusterDealer), "CcCSPerformanceCluster.ID", MatchType.Exact, rowValue.ID))

            Dim arrClusterDealer As ArrayList = funcCD.Retrieve(criterias)
            If arrClusterDealer.Count > 0 Then
                lblInfo.Visible = False
                dtgDetail.DataSource = arrClusterDealer.Cast(Of CcCSPerformanceClusterDealer).ToList()
                dtgDetail.PageSize = arrClusterDealer.Count
                dtgDetail.DataBind()
            End If
            lblTotalDealer.Text = arrClusterDealer.Count.ToString()
            lbtnGenerate.Visible = IsEdit
            lbtnEdit.Visible = IsEdit
            lbtnHapus.Visible = IsEdit
            lbtnHapus.Attributes.Add("OnClick", "return confirm('Yakin Cluster ini akan dihapus?');")
        End If

    End Sub

    Protected Sub LinkButtonNonActive_Click(sender As Object, e As EventArgs)
        Try
            Dim funcCLDealer As New CcCSPerformanceClusterDealerFacade(Me.User)
            Dim dgItem As DataGridItem = CType(sender, LinkButton).Parent.Parent
            Dim id As Integer = CInt(dgItem.Cells(0).Text)
            Dim objClusterDealer As CcCSPerformanceClusterDealer = funcCLDealer.Retrieve(id)
            funcCLDealer.Delete(objClusterDealer)

            MessageBox.Show(SR.DataDeleted("Dealer"))
            InitForm()
        Catch ex As Exception
            MessageBox.Show("Hapus data gagal")
        End Try

        Dim x As String = String.Empty
    End Sub

    Protected Sub dtgDetail_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim btnHapus As LinkButton = CType(e.Item.FindControl("LinkButtonNonActive"), LinkButton)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblTipeDealer As Label = CType(e.Item.FindControl("lblTipeDealer"), Label)

            Dim rowValue As CcCSPerformanceClusterDealer = e.DataItem(Of CcCSPerformanceClusterDealer)()
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DealerCategory), "Dealer.ID", MatchType.Exact, rowValue.Dealer.ID))
            Dim arrTipeCategory As ArrayList = New DealerCategoryFacade(Me.User).Retrieve(criterias)

            Dim strValue As String = String.Empty
            For Each iCtg As DealerCategory In arrTipeCategory
                strValue += iCtg.Category.CategoryCode + ", "
            Next
            If strValue.Length > 2 Then
                strValue = strValue.Remove(strValue.Length - 2, 2)
            End If
            lblTipeDealer.Text = strValue

            btnHapus.Visible = IsEdit
            lblNo.Text = e.CreateNumberPage()
            If IsEdit Then
                btnHapus.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            End If

        End If
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Try
            Dim funcCluster As New CcCSPerformanceClusterFacade(Me.User)

            Dim objCluster As New CcCSPerformanceCluster
            Dim listDealerType As New List(Of String)
            Dim listVehicleType As New List(Of String)

            For Each itemD As ListItem In chkDealerType.Items
                If itemD.Selected Then
                    listDealerType.Add(itemD.Text)
                End If
            Next
            For Each itemV As ListItem In chkVehicleType.Items
                If itemV.Selected Then
                    listVehicleType.Add(itemV.Text)
                End If
            Next

            objCluster.CcCSPerformanceMaster = New CcCSPerformanceMaster(CcCSPerformnaceMasterID)
            objCluster.ClusterName = txtDescription.Text
            objCluster.MinPoint = CInt(txtMinPoint.Text)
            objCluster.MaxPoint = CInt(txtMaxPoint.Text)
            objCluster.TypeCal = CInt(ddlTipeKalkulasi.SelectedValue)
            objCluster.StartPeriodCal = CInt(ddlPeriodeFrom.SelectedValue)
            objCluster.EndPeriodCal = CInt(ddlPeriodeTo.SelectedValue)
            objCluster.VehicleType = String.Join(", ", listVehicleType)
            objCluster.DealerType = String.Join(", ", listDealerType)

            If ViewState("mode") = "insert" Then
                funcCluster.Insert(objCluster)

                MessageBox.Show(SR.SaveSuccess)
                btnBatal_Click(sender, e)
            ElseIf ViewState("mode") = "edit" Then
                objCluster.ID = CInt(hdnClusterID.Value)
                funcCluster.Update(objCluster)

                MessageBox.Show(SR.SaveSuccess)
                btnBatal_Click(sender, e)
            End If

            InitForm()
        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)
        End Try
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        ViewState("mode") = "insert"
        ClearForm()
    End Sub
    Private Sub ClearForm()
        txtDescription.Text = String.Empty
        txtMinPoint.Text = String.Empty
        txtMaxPoint.Text = String.Empty
        chkDealerType.ClearSelection()
        chkVehicleType.ClearSelection()
        ddlPeriodeFrom.ClearSelection()
        ddlPeriodeTo.ClearSelection()
        ddlPeriodeFrom.SelectedIndex = 0
        ddlPeriodeTo.SelectedIndex = 0
        hdnClusterID.Value = String.Empty
        dtgCSPMCluster.SelectedIndex = -1
    End Sub

    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect("FrmCcCSPerformanceMaster.aspx")
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Try
            Using excelPackage As New ExcelPackage()
                Dim funcPeriod As New CcPeriodFacade(Me.User)
                Dim wsData As ExcelWorksheet = excelPackage.Workbook.Worksheets.Add("Data Siswa Kelas")
                Dim rowIdx As Integer = 1

                Dim objMaster As CcCSPerformanceMaster = New CcCSPerformanceMasterFacade(Me.User).Retrieve(Me.CcCSPerformnaceMasterID)

                wsData.Cells("A" & rowIdx.ToString()).ValueBold("DAFTAR PESERTA " + objMaster.Description)
                rowIdx += 1

                Dim startPeriode As CcPeriod = funcPeriod.Retrieve(objMaster.CcPeriodIDFrom)
                Dim endPeriode As CcPeriod = funcPeriod.Retrieve(objMaster.CcPeriodIDTo)

                Dim pAwal As Date = New Date(CInt(startPeriode.YearMonth.Substring(0, 4)), CInt(startPeriode.YearMonth.Substring(4, 2)), 1)
                Dim pAkhir As Date = New Date(CInt(endPeriode.YearMonth.Substring(0, 4)), CInt(endPeriode.YearMonth.Substring(4, 2)), 1)

                Dim strAwal As String = Bulan.FirstOrDefault(Function(x) x.Key = pAwal.Month).Value + " " + pAwal.Year.ToString
                Dim strAkhir As String = Bulan.FirstOrDefault(Function(x) x.Key = pAkhir.Month).Value + " " + pAkhir.Year.ToString

                wsData.Cells("A" & rowIdx.ToString()).ValueBold(String.Format("PERIODE : {0} s/d {1}", strAwal, strAkhir))
                rowIdx += 1
                rowIdx += 1

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcCSPerformanceCluster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(CcCSPerformanceCluster), "CcCSPerformanceMaster.ID", MatchType.Exact, Me.CcCSPerformnaceMasterID))


                Dim arrCluster As ArrayList = New CcCSPerformanceClusterFacade(Me.User).Retrieve(criterias)
                For Each iCluster As CcCSPerformanceCluster In arrCluster
                    Dim noUrut As Integer = 1

                    Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcCSPerformanceClusterDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crits.opAnd(New Criteria(GetType(CcCSPerformanceClusterDealer), "CcCSPerformanceCluster.ID", MatchType.Exact, iCluster.ID))

                    Dim arrClusterDealer As ArrayList = New CcCSPerformanceClusterDealerFacade(Me.User).Retrieve(crits)
                    'Dim startPeriod As CcPeriod = funcPeriod.Retrieve(iCluster.StartPeriodCal)
                    'Dim endPeriod As CcPeriod = funcPeriod.Retrieve(iCluster.EndPeriodCal)

                    'Dim periodeAwal As String = New Date(CInt(startPeriod.YearMonth.Substring(0, 4)), CInt(startPeriod.YearMonth.Substring(4, 2)), 1).ToString("MMM yyyy")
                    'Dim periodeAkhir As String = New Date(CInt(endPeriod.YearMonth.Substring(0, 4)), CInt(endPeriod.YearMonth.Substring(4, 2)), 1).ToString("MMM yyyy")
                    
                    'wsData.Cells("B" & rowIdx.ToString()).SetHeaderValue("Nama Cluster")
                    'wsData.Cells("B" & rowIdx.ToString()).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                    'wsData.Cells("C" & rowIdx.ToString()).ValueBold(iCluster.ClusterName)
                    'rowIdx += 1
                    'wsData.Cells("B" & rowIdx.ToString()).SetHeaderValue("Periode")
                    'wsData.Cells("B" & rowIdx.ToString()).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                    'wsData.Cells("C" & rowIdx.ToString()).ValueBold(periodeAwal + " s/d " + periodeAkhir)
                    'rowIdx += 1
                    'wsData.Cells("B" & rowIdx.ToString()).SetHeaderValue("Batas Penjualan")
                    'wsData.Cells("B" & rowIdx.ToString()).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                    'wsData.Cells("C" & rowIdx.ToString()).ValueBold(iCluster.MinPoint.ToString() + " s/d " + iCluster.MaxPoint.ToString())
                    'rowIdx += 1
                    'wsData.Cells("B" & rowIdx.ToString()).SetHeaderValue("Tipe Kalkulasi")
                    'wsData.Cells("B" & rowIdx.ToString()).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                    'wsData.Cells("C" & rowIdx.ToString()).ValueBold(IIf(iCluster.TypeCal = 1, "Rata-rata", "Total"))
                    'rowIdx += 1
                    'wsData.Cells("B" & rowIdx.ToString()).SetHeaderValue("Tipe Kendaraan")
                    'wsData.Cells("B" & rowIdx.ToString()).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                    'wsData.Cells("C" & rowIdx.ToString()).ValueBold(iCluster.VehicleType)
                    'rowIdx += 1
                    'wsData.Cells("B" & rowIdx.ToString()).SetHeaderValue("Tipe Dealer")
                    'wsData.Cells("B" & rowIdx.ToString()).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                    'wsData.Cells("C" & rowIdx.ToString()).ValueBold(iCluster.DealerType)
                    'rowIdx += 1
                    'wsData.Cells("B" & rowIdx.ToString()).SetHeaderValue("Total Dealer")
                    'wsData.Cells("B" & rowIdx.ToString()).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                    'wsData.Cells("C" & rowIdx.ToString()).ValueBold(arrClusterDealer.Count.ToString)
                    'rowIdx += 1
                    'rowIdx += 1

                    wsData.Cells("A" & rowIdx.ToString()).ValueBold("GRADE " + iCluster.ClusterName)
                    rowIdx += 1

                    CreateHeaderColumn(wsData, rowIdx)
                    rowIdx += 1

                    For Each iDealer As CcCSPerformanceClusterDealer In arrClusterDealer
                        Dim clmidx As Integer = 1
                        wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(noUrut.ToString(), Style.ExcelHorizontalAlignment.Center)
                        clmidx += 1
                        wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(iDealer.Dealer.DealerCode.ToString, Style.ExcelHorizontalAlignment.Center)
                        clmidx += 1
                        wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(iDealer.Dealer.DealerName, Style.ExcelHorizontalAlignment.Center)
                        clmidx += 1

                        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crit.opAnd(New Criteria(GetType(DealerCategory), "Dealer.ID", MatchType.Exact, iDealer.Dealer.ID))
                        Dim arrTipeCategory As ArrayList = New DealerCategoryFacade(Me.User).Retrieve(crit)

                        Dim strValue As String = String.Empty
                        For Each iCtg As DealerCategory In arrTipeCategory
                            strValue += iCtg.Category.CategoryCode + ", "
                        Next
                        If strValue.Length > 2 Then
                            strValue = strValue.Remove(strValue.Length - 2, 2)
                        End If
                        wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(strValue, Style.ExcelHorizontalAlignment.Center)
                        clmidx += 1
                        wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(iDealer.Dealer.DealerGroup.GroupName, Style.ExcelHorizontalAlignment.Center)
                        clmidx += 1
                        wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(iDealer.Dealer.City.CityName, Style.ExcelHorizontalAlignment.Center)
                        clmidx += 1
                        wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(iDealer.Dealer.Area2.Description, Style.ExcelHorizontalAlignment.Center)
                        clmidx += 1
                        wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(iDealer.Penjualan.ToString, Style.ExcelHorizontalAlignment.Center)
                        clmidx += 1
                        noUrut += 1
                        rowIdx += 1
                    Next
                    rowIdx += 1
                Next
                For colIdx As Integer = 2 To 8
                    wsData.Column(colIdx).AutoFit()
                Next

                Dim fileBytes = excelPackage.GetAsByteArray()
                Dim fileName As String = String.Format("ClusterDealer.xls")
                
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

        Catch ex As Exception
            MessageBox.Show("Download gagal")
        End Try
    End Sub

    Private Sub CreateHeaderColumn(ByVal wsData As ExcelWorksheet, ByVal rowIdx As Integer)
        Dim columnIdx As Integer = 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("No")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Kode")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Nama")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Tipe Dealer")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Group")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Kota")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Area")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Penjualan")
        columnIdx += 1
    End Sub

    Private ReadOnly Property Bulan As Dictionary(Of Integer, String)
        Get
            Dim dicBulan As New Dictionary(Of Integer, String)
            dicBulan.Add(1, "JANUARI")
            dicBulan.Add(2, "FEBRUARI")
            dicBulan.Add(3, "MARET")
            dicBulan.Add(4, "APRIL")
            dicBulan.Add(5, "MEI")
            dicBulan.Add(6, "JUNI")
            dicBulan.Add(7, "JULI")
            dicBulan.Add(8, "AGUSTUS")
            dicBulan.Add(9, "SEPTEMBER")
            dicBulan.Add(10, "OKTOBER")
            dicBulan.Add(11, "NOVEMBER")
            dicBulan.Add(12, "DESEMBER")
            Return dicBulan
        End Get
    End Property
End Class