#Region "Custom Namespace Imports"
Imports KTB.DNet.Parser
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper

#End Region

Public Class frmDealerStockRatio
    Inherits System.Web.UI.Page



#Region "Custom Variable Declaration"
    Private StockActualArrayList As ArrayList
    Private sessionHelper As New SessionHelper

#End Region

#Region "Custom Method"



    Private Sub BindToGrid()
        StockActualArrayList = sessionHelper.GetSession("DealerStockRatio")
        If Not ((StockActualArrayList Is Nothing) OrElse (StockActualArrayList.Count <= 0)) Then
            dtgDealerStockRatio.DataSource = StockActualArrayList
            dtgDealerStockRatio.DataBind()
        End If
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            FillCategory()
            ViewState("CurrentSortColumn") = "Dealer.DealerCode"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            btnGetDealer.Style("display") = "none"

            ddlTahun.Items.Clear()

            ddlTahun.Items.Add(New ListItem("Silahkan Pilih", "-1"))
            For i As Integer = DateTime.Today.Year To DateTime.Today.Year - 5 Step -1
                ddlTahun.Items.Add(New ListItem(i.ToString, i))
            Next
            'ddlTahun.Items.Add(New ListItem(DateTime.Today.Year.ToString, DateTime.Today.Year))
            'ddlTahun.Items.Add(New ListItem((DateTime.Today.Year - 1).ToString, DateTime.Today.Year - 1))
            'ddlTahun.Items.Add(New ListItem((DateTime.Today.Year - 2).ToString, DateTime.Today.Year - 2))

            ddlMonthPeriodeFrom.Items.Clear()
            ddlMonthPeriodeFrom.Items.Add(New ListItem("Silahkan Pilih", "-1"))
            ddlMonthPeriodeFrom.Items.Add(New ListItem("Januari", 1))
            ddlMonthPeriodeFrom.Items.Add(New ListItem("Februari", 2))
            ddlMonthPeriodeFrom.Items.Add(New ListItem("Maret", 3))
            ddlMonthPeriodeFrom.Items.Add(New ListItem("April", 4))
            ddlMonthPeriodeFrom.Items.Add(New ListItem("Mei", 5))
            ddlMonthPeriodeFrom.Items.Add(New ListItem("Juni", 6))
            ddlMonthPeriodeFrom.Items.Add(New ListItem("Juli", 7))
            ddlMonthPeriodeFrom.Items.Add(New ListItem("Agustus", 8))
            ddlMonthPeriodeFrom.Items.Add(New ListItem("September", 9))
            ddlMonthPeriodeFrom.Items.Add(New ListItem("Oktober", 10))
            ddlMonthPeriodeFrom.Items.Add(New ListItem("November", 11))
            ddlMonthPeriodeFrom.Items.Add(New ListItem("Desember", 12))

            GetDealer()
            Dim objDealer As Dealer = CType(Session("sesDealer"), Dealer)
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                lblDealerCode.Text = objDealer.DealerCode
            Else
                lblDealerCode.Text = ""
            End If
        End If

    End Sub





    Private Sub ActivateUserPrivilege()
        btnDownLoad.Visible = SecurityProvider.Authorize(Context.User, SR.Download_Dealer_Stock_Ratio_Privilege)

        Dim org As Dealer = CType(Session("DEALER"), Dealer)
        If org.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If Not SecurityProvider.Authorize(Context.User, SR.Lihat_Dealer_Stock_Ratio_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=PEMESANAN - Estimasi Pesanan")
            End If

            txtDealerCode.Visible = False
            lblSearchDealer.Visible = False
            lblDealerCode.Visible = True
        Else
            If Not SecurityProvider.Authorize(Context.User, SR.Lihat_Dealer_Stock_Ratio_Privilege) Then
                Response.Redirect("../frmAccessDenied.aspx?modulName=Rencana Produksi")
            End If

            txtDealerCode.Visible = True
            lblSearchDealer.Visible = True
            lblDealerCode.Visible = False
        End If
    End Sub


    Private Sub SortListControl(ByRef ListControl As ArrayList, ByVal SortColumn As String, _
            ByVal SortDirection As Sort.SortDirection)

        Dim IsAsc As Boolean = True
        If SortDirection = Sort.SortDirection.ASC Then
            IsAsc = True
        ElseIf SortDirection = Sort.SortDirection.DESC Then
            IsAsc = False
        End If

        If SortColumn = "City.CityName" Then
            Dim objCompareCityName As IComparer = New CompareCityName(IsAsc)
            ListControl.Sort(objCompareCityName)
        Else
            Dim objListComparer As IComparer = New ListComparer(IsAsc, SortColumn)
            ListControl.Sort(objListComparer)
        End If
    End Sub

    Private Sub dtgDealerStockRatio_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDealerStockRatio.SortCommand
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
        StockActualArrayList = DealerStockRatioTransferData()

        SortListControl(StockActualArrayList, CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        'dtgDealerStockRatio.SelectedIndex = -1
        'dtgDealerStockRatio.CurrentPageIndex = 0

        dtgDealerStockRatio.DataSource = StockActualArrayList
        dtgDealerStockRatio.DataBind()
        'BindData()
    End Sub


    Sub dtgDealerStockRatio_ItemDataBound(ByVal Sender As Object, ByVal E As DataGridItemEventArgs) Handles dtgDealerStockRatio.ItemDataBound
        StockActualArrayList = sessionHelper.GetSession("DealerStockRatio")
        If Not (StockActualArrayList.Count = 0 Or E.Item.ItemIndex = -1) Then
            Dim lblNo As Label = CType(E.Item.FindControl("lblNo"), Label)
            lblNo.Text = (E.Item.ItemIndex + 1 + (dtgDealerStockRatio.PageSize * dtgDealerStockRatio.CurrentPageIndex)).ToString

        End If


    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        'btnSimpan.Enabled = False
        '--set to none page mode
        dtgDealerStockRatio.AllowSorting = True


        '--remove all datas
        dtgDealerStockRatio.DataSource = Nothing
        dtgDealerStockRatio.DataBind()

        sessionHelper.RemoveSession("DealerStockRatio")

        '--start search query and bind new data to grid
        BindData()
        If dtgDealerStockRatio.Items.Count > 0 Then
            btnDownLoad.Enabled = True
        Else
            btnDownLoad.Enabled = False
        End If
    End Sub

    Private Sub BindData()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StockActual), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim org As Dealer = CType(Session("DEALER"), Dealer)
        If org.Title = EnumDealerTittle.DealerTittle.DEALER Then
            Dim objDealer As Dealer = CType(Session("sesDealer"), Dealer)
            criterias.opAnd(New Criteria(GetType(StockActual), "Dealer.ID", MatchType.Exact, objDealer.ID))
        ElseIf (Not String.IsNullOrEmpty(txtDealerCode.Text)) Then
            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim)
            If Not String.IsNullOrEmpty(objDealer.Title) AndAlso objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                criterias.opAnd(New Criteria(GetType(StockActual), "Dealer.DealerCode", MatchType.Exact, objDealer.DealerCode))
            Else
                criterias.opAnd(New Criteria(GetType(StockActual), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
            End If
        End If

        criterias.opAnd(New Criteria(GetType(StockActual), "Dealer.ID", MatchType.No, 1))
        criterias.opAnd(New Criteria(GetType(StockActual), "Dealer.Title", MatchType.Exact, "0"))
        criterias.opAnd(New Criteria(GetType(StockActual), "Dealer.Status", MatchType.Exact, "1"))

        If (ddlCategory.SelectedValue <> -1) Then
            criterias.opAnd(New Criteria(GetType(StockActual), "VechileModel.Category.ID", MatchType.Exact, CType(ddlCategory.SelectedValue, Integer)))
        End If
        If (ddlModel.SelectedValue <> -1) Then
            criterias.opAnd(New Criteria(GetType(StockActual), "VechileModel.ID", MatchType.Exact, CType(ddlModel.SelectedValue, Integer)))
        End If
        If (ddlMonthPeriodeFrom.SelectedValue <> -1 AndAlso ddlTahun.SelectedValue <> -1) Then
            Dim datePeriode As Date = New Date(CType(ddlTahun.SelectedValue, Integer), CType(ddlMonthPeriodeFrom.SelectedValue, Integer), 1).AddMonths(-1)

            criterias.opAnd(New Criteria(GetType(StockActual), "Month", MatchType.Exact, datePeriode.Month))
            criterias.opAnd(New Criteria(GetType(StockActual), "Year", MatchType.Exact, datePeriode.Year))
        End If


        StockActualArrayList = New StockActualFacade(User).Retrieve(criterias, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        sessionHelper.SetSession("DealerStockRatio", StockActualArrayList)
        If StockActualArrayList.Count > 0 Then
            BindToGrid()
        Else
            BindDataFromStockTarget()
        End If

    End Sub

    Private Sub BindDataFromStockTarget()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StockTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim org As Dealer = CType(Session("DEALER"), Dealer)
        If org.Title = EnumDealerTittle.DealerTittle.DEALER Then
            Dim objDealer As Dealer = CType(Session("sesDealer"), Dealer)
            criterias.opAnd(New Criteria(GetType(StockTarget), "Dealer.ID", MatchType.Exact, objDealer.ID))
        ElseIf (Not String.IsNullOrEmpty(txtDealerCode.Text)) Then
            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim)
            If Not String.IsNullOrEmpty(objDealer.Title) AndAlso objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                criterias.opAnd(New Criteria(GetType(StockTarget), "Dealer.DealerCode", MatchType.Exact, objDealer.DealerCode))
            Else
                criterias.opAnd(New Criteria(GetType(StockTarget), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
            End If
        End If

        criterias.opAnd(New Criteria(GetType(StockTarget), "Dealer.ID", MatchType.No, 1))
        criterias.opAnd(New Criteria(GetType(StockTarget), "Dealer.Title", MatchType.Exact, "0"))
        criterias.opAnd(New Criteria(GetType(StockTarget), "Dealer.Status", MatchType.Exact, "1"))

        If (ddlCategory.SelectedValue <> -1) Then
            criterias.opAnd(New Criteria(GetType(StockTarget), "VechileModel.Category.ID", MatchType.Exact, CType(ddlCategory.SelectedValue, Integer)))
        End If
        If (ddlModel.SelectedValue <> -1) Then
            criterias.opAnd(New Criteria(GetType(StockTarget), "VechileModel.ID", MatchType.Exact, CType(ddlModel.SelectedValue, Integer)))
        End If
        If (ddlMonthPeriodeFrom.SelectedValue <> -1) AndAlso (ddlTahun.SelectedValue <> -1) Then
            Dim firstday As New Date(ddlTahun.SelectedValue, ddlMonthPeriodeFrom.SelectedValue, 1)
            Dim lastday As New Date(ddlTahun.SelectedValue, ddlMonthPeriodeFrom.SelectedValue, Date.DaysInMonth(ddlTahun.SelectedValue, ddlMonthPeriodeFrom.SelectedValue))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.StockTarget), "ValidFrom", MatchType.GreaterOrEqual, firstday))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.StockTarget), "ValidFrom", MatchType.Lesser, lastday))
        End If

        Dim stockTargetList = New StockTargetFacade(User).Retrieve(criterias)

        If stockTargetList.Count > 0 Then
            StockActualArrayList = New ArrayList
            Dim _actualFacade As New StockActualFacade(User)
            For Each _stocktarget As StockTarget In stockTargetList
                Dim _stockActual As New StockActual
                _stockActual.Dealer = _stocktarget.Dealer
                _stockActual.StockTarget = _stocktarget
                _stockActual.ID = -1
                _stockActual.Year = Year(_stocktarget.ValidFrom)
                _stockActual.Month = Month(_stocktarget.ValidFrom)
                _stockActual.RowStatus = DBRowStatus.Active
                _stockActual.VechileModel = _stocktarget.VechileModel
                _stockActual.MarkLoaded()
                _actualFacade.ForcePopulateData(_stockActual)
                StockActualArrayList.Add(_stockActual)
            Next
            sessionHelper.SetSession("DealerStockRatio", StockActualArrayList)
            BindToGrid()
        Else
            dtgDealerStockRatio.DataSource = StockActualArrayList
            dtgDealerStockRatio.DataBind()
            MessageBox.Show("Data Stock Ratio Tidak Ditemukan")
        End If
    End Sub


    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownLoad.Click
        Try
            'donas 20151209 : get from session
            Dim aTSs As ArrayList = sessionHelper.GetSession("DealerStockRatio")
            DoDownload(aTSs)
            'DoDownload(DealerStockRatioTransferData)
        Catch ex As Exception
            MessageBox.Show("Gagal Download File.")
        End Try
    End Sub

    Private Function DealerStockRatioTransferData() As ArrayList
        'Dim oDataGridItem As DataGridItem
        'Dim oExArgs As New System.Collections.ArrayList
        'Dim objDealerStockRatio As New StockActualFacade(User)

        'For Each oDataGridItem In dtgDealerStockRatio.Items
        '    Dim _stockActual As New KTB.DNet.Domain.StockActual
        '    _stockActual.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
        '    _stockActual = objDealerStockRatio.Retrieve(_stockActual.ID)
        '    oExArgs.Add(_stockActual)
        'Next
        StockActualArrayList = sessionHelper.GetSession("DealerStockRatio")

        Return StockActualArrayList
    End Function
    Private Sub DoDownload(ByRef arlDPK As ArrayList)
        Dim sFileName As String
        sFileName = "DealerStockRatio" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ""

        '-- Temp file must be a randomly named file!
        Dim oFile As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(oFile)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(oFile, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                MessageBox.Show(arlDPK.Count.ToString())
                WriteDataToExcell(sw, arlDPK)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls", True)

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub WriteDataToExcell(ByVal sw As StreamWriter, ByRef arlDPK As ArrayList)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder

        MessageBox.Show(arlDPK.Count.ToString() & " in writedatatoexcel")
        If (arlDPK.Count > 0) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No" & tab)
            itemLine.Append("Dealer" & tab)
            itemLine.Append("Kategori" & tab)
            itemLine.Append("Model" & tab)
            itemLine.Append("Periode" & tab)
            itemLine.Append("Target" & tab)
            itemLine.Append("Target Ratio" & tab)
            itemLine.Append("Stok Dealer(EOM)" & tab)
            itemLine.Append("Stok Ratio(EOM)" & tab)
            itemLine.Append("Ratio Saat Ini" & tab)
            itemLine.Append("Total Unit PK Telah Diajukan" & tab)
            itemLine.Append("Kekurangan Order" & tab)

            sw.WriteLine(itemLine.ToString())
            Dim i As Integer = 1
            Try
                For Each item As StockActual In arlDPK
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(i.ToString & tab)
                    itemLine.Append(item.Dealer.DealerCode & tab)
                    If Not IsNothing(item.VechileModel) Then
                        itemLine.Append(item.VechileModel.Category.Description.ToString & tab)
                        itemLine.Append(item.VechileModel.Description.ToString & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If Not IsNothing(item.Month) Then
                        itemLine.Append(MonthName(item.Month, True))
                    Else
                        itemLine.Append("")
                    End If
                    itemLine.Append("-")

                    If Not IsNothing(item.Year) Then
                        itemLine.Append(item.Year.ToString() & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If (IsNothing(item.StockTarget)) Then
                        itemLine.Append("-" & tab)
                        itemLine.Append("-" & tab)
                    Else

                        If Not IsNothing(item.StockTarget.Target) Then
                            itemLine.Append(item.StockTarget.Target.ToString & tab)
                        Else
                            itemLine.Append("-" & tab)
                        End If
                        If Not IsNothing(item.StockTarget.TargetRatio) Then
                            itemLine.Append(item.StockTarget.TargetRatio.ToString() & tab)
                        Else
                            itemLine.Append("-" & tab)
                        End If
                    End If


                    If Not IsNothing(item.Stock) Then
                        itemLine.Append(item.Stock.ToString() & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If Not IsNothing(item.RatioSPM) Then
                        itemLine.Append(item.RatioSPM.ToString() & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If Not IsNothing(item.CurrentRatio) Then
                        itemLine.Append(item.CurrentRatio.ToString() & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If Not IsNothing(item.TotalUnitPK) Then
                        itemLine.Append(item.TotalUnitPK.ToString() & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If Not IsNothing(item.GetCurrentOrderLeft) Then
                        itemLine.Append(item.GetCurrentOrderLeft.ToString() & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If

                    sw.WriteLine(itemLine.ToString())
                    i = i + 1
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If

        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)

    End Sub
    Private Sub FillCategory()
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        ddlCategory.Items.Clear()
        Dim objCategory As New CategoryFacade(User)
        ddlCategory.DataSource = objCategory.RetrieveActiveList(companyCode)
        ddlCategory.DataTextField = "Description"
        ddlCategory.DataValueField = "ID"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        FillModel(-1)
    End Sub


    Private Sub FillModel(ByVal CategoryID As Integer)
        ddlModel.Items.Clear()
        If CategoryID > -1 Then
            Dim objModel As New VechileModelFacade(User)
            Dim crit As New CriteriaComposite(New Criteria(GetType(VechileModel), "RowStatus", CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(VechileModel), "Category", CategoryID))
            ddlModel.DataSource = objModel.RetrieveList("Description", Sort.SortDirection.ASC, crit)
            ddlModel.DataTextField = "Description"
            ddlModel.DataValueField = "ID"
            ddlModel.DataBind()
            ddlModel.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        Else
            ddlModel.Items.Insert(0, New ListItem("Pilih Kategori", -1))
        End If
    End Sub

    Private Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        FillModel(ddlCategory.SelectedValue)
    End Sub

#End Region

    Protected Sub btnGetDealer_Click(sender As Object, e As EventArgs) Handles btnGetDealer.Click
        If txtDealerCode.Text.Length > 0 Then
            Dim ObjDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim())
        End If
    End Sub

    Protected Sub chkIsKTBBlockAll_CheckedChanged(sender As Object, e As EventArgs)
        For Each row As DataGridItem In dtgDealerStockRatio.Items
            Dim chkbox As CheckBox = CType(row.FindControl("chkIsKTBBlock"), CheckBox)
            If (chkbox IsNot Nothing) Then
                chkbox.Checked = CType(sender, CheckBox).Checked
            End If
        Next

    End Sub

    Protected Sub chkIsDealerBlockAll_CheckedChanged(sender As Object, e As EventArgs)
        For Each row As DataGridItem In dtgDealerStockRatio.Items
            Dim chkbox As CheckBox = row.FindControl("chkIsDealerBlock")
            If (chkbox IsNot Nothing) Then
                chkbox.Checked = CType(sender, CheckBox).Checked
            End If
        Next

    End Sub
    Private Sub GetDealer()
        If Not IsNothing(Session("DEALER")) Then
            sessionHelper.SetSession("sesDealer", Session("DEALER"))
        Else

        End If
    End Sub

End Class