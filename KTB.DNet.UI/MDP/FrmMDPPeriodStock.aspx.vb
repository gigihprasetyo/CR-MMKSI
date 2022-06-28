#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.MDP
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports System.Linq
#End Region


Public Class FrmMDPPeriodStock
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Varibles"
    Private arlMDPPeriod As ArrayList
    Private _sessHelper As SessionHelper = New SessionHelper
    Private sessCriterias As String = "FrmMDPPeriodStock.sessCriterias"
#End Region

#Region "Custom Methods"

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.MDP_alokasi_unit_bulanan_lihat_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=MDPDealerMothly")
        End If
    End Sub

    Private Sub Initialization()
        ViewState.Add("SortColumn", "DealerCode")
        ViewState.Add("SortDirection", Sort.SortDirection.ASC)

        BindDdlCategory()
        BindDdlType()
    End Sub

    Private Sub BindDdlCategory()
        Dim objCFac As CategoryFacade = New CategoryFacade(User)
        Dim crtC As CriteriaComposite
        Dim arlC As ArrayList = New ArrayList
        Dim srtC As SortCollection = New SortCollection

        crtC = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        srtC.Add(New Sort(GetType(Category), "CategoryCode"))
        arlC = objCFac.Retrieve(crtC, srtC)
        ddlKategori.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each objC As Category In arlC
            ddlKategori.Items.Add(New ListItem(objC.CategoryCode, objC.ID))
        Next
        ddlKategori.SelectedValue = -1
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

    Private Sub BindDdlType() '(ByVal ModelID As Integer)
        ddlTipe.Items.Clear()
        'If ModelID > -1 Then
        Dim objVTFac As VechileTypeFacade = New VechileTypeFacade(User)
        Dim crtVT As CriteriaComposite
        Dim arlVT As ArrayList = New ArrayList
        Dim srtVT As SortCollection = New SortCollection

        ddlTipe.Items.Add(New ListItem("Silahkan Pilih", -1))

        crtVT = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtVT.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.Exact, "A"))
        crtVT.opAnd(New Criteria(GetType(VechileType), "Category.ID", MatchType.Exact, ddlKategori.SelectedValue))
        If ddlModel.SelectedValue <> -1 Then
            crtVT.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.Exact, ddlModel.SelectedValue))
        End If
        srtVT.Add(New Sort(GetType(VechileType), "VechileTypeCode"))
        arlVT = objVTFac.Retrieve(crtVT, srtVT)
        For Each objvt As VechileType In arlVT
            ddlTipe.Items.Add(New ListItem(objvt.VechileTypeCode, objvt.ID))
        Next
        'Else
        If ddlModel.SelectedValue = -1 Then
            ddlTipe.SelectedValue = -1
            Exit Sub
        End If
        'End If
        'ddlTipe.SelectedValue = -1
    End Sub


    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim TotalRow As Integer = 0
        If indexPage >= 0 Then
            arlMDPPeriod = New MDPDealerMonthlyStockFacade(User).RetrieveActiveList(indexPage + 1, dtgMain.PageSize, TotalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection), CType(_sessHelper.GetSession(Me.sessCriterias), CriteriaComposite))
            dtgMain.DataSource = arlMDPPeriod
            dtgMain.VirtualItemCount = TotalRow
            dtgMain.DataBind()
        End If
    End Sub

    Private Sub ClearData()
        Dim objUser As UserInfo = CType(_sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If objUser.Dealer.Title = "1" Then
            txtKodeDealer.Text() = String.Empty
        End If
        ddlKategori.SelectedValue = -1
        ddlModel.SelectedValue = -1
        ddlTipe.SelectedValue = -1
        ddlPeriode.SelectedValue = Format(DateTime.Now, "MMM yyyy").ToString
        dtgMain.DataSource = Nothing
        dtgMain.DataBind()
        btnCari.Enabled = True
    End Sub

    Private Sub DoDownload(ByVal arlPF As ArrayList)
        Dim sFileName As String
        sFileName = "MDP Period Stock [" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & "]"

        '-- Temp file must be a randomly named file!
        Dim File As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        Try
            Dim finfo As FileInfo = New FileInfo(File)
            If finfo.Exists Then
                finfo.Delete()  '-- Delete temp file if exists
            End If

            '-- Create file stream
            Dim fs As FileStream = New FileStream(File, FileMode.CreateNew)
            '-- Create stream writer
            Dim sw As StreamWriter = New StreamWriter(fs)

            '-- Write data to temp file
            WriteDataToExcell(sw, arlPF)

            sw.Close()
            fs.Close()

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls", True)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub WriteDataToExcell(ByVal sw As StreamWriter, ByVal arlPF As ArrayList)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        itemLine.Remove(0, itemLine.Length)  '-- Empty line
        itemLine.Append("MDP - PERIOD STOCK")
        sw.WriteLine(itemLine)
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)
        sw.WriteLine(" ")

        If (arlPF.Count > 0) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("NO" & tab)
            itemLine.Append("TIPE/WARNA" & tab)
            itemLine.Append("MODEL/TIPE/WARNA" & tab)
            itemLine.Append("TAHUN PERAKITAN/IMPOR" & tab)
            itemLine.Append("PERIODE MULAI" & tab)
            itemLine.Append("PERIODE AKHIR" & tab)
            itemLine.Append("STOK SEBELUMNYA" & tab)
            itemLine.Append("RENCANA ALOKASI" & tab)
            itemLine.Append("SISA STOK" & tab)

            sw.WriteLine(itemLine.ToString())
            Dim i As Integer = 1
            Try
                For Each item As MDPDealerMonthlyStock In arlPF
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(i.ToString & tab)
                    itemLine.Append(item.VechileColor.MaterialNumber & tab)
                    itemLine.Append(item.VechileColor.MaterialDescription & tab)
                    itemLine.Append(item.ProductionYear & tab)
                    itemLine.Append(item.PeriodStartDate & tab)
                    itemLine.Append(item.PeriodEndDate & tab)
                    itemLine.Append(item.CarryOverStock & tab)
                    itemLine.Append(item.PlanStock & tab)
                    itemLine.Append(item.RemainingStock & tab)

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

#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckUserPrivilege()
        If Not IsPostBack Then
            Initialization()
            BindDdlPeriode()
            Dim objUser As UserInfo = CType(_sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
            If objUser.Dealer.Title <> "1" Then
                txtKodeDealer.Text = objUser.Dealer.DealerCode
                txtKodeDealer.Enabled = False
                lblSearchDealer.Visible = False
            End If
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        BindSearchGrid()
    End Sub

    Private Sub BindSearchGrid()
        ReadData()   '-- Read all data matching criteria
        BindPage(0)  '-- Bind page-1
    End Sub

    Private Sub ReadData()
        '-- Read all data selected

        dtgMain.Visible = True
        btnDownload.Enabled = False  '-- Init: Disable <Download> button
        Dim objDealer As New Dealer
        Dim objVechileColor As New VechileColor
        '-- Search criteria:
        '-- Row status
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPDealerMonthlyStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtKodeDealer.Text <> "" Then
            objDealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text)
            criterias.opAnd(New Criteria(GetType(MDPDealerMonthlyStock), "Dealer.ID", MatchType.Exact, objDealer.ID))
            'objVechileColor = New VechileColorFacade(User).RetrieveByMaterialNumber(cols(3).Trim)
        End If

        '-- Periode
        Dim tgl As DateTime = System.Convert.ToDateTime(ddlPeriode.SelectedValue)
        criterias.opAnd(New Criteria(GetType(MDPDealerMonthlyStock), "PeriodMonth", MatchType.Exact, tgl.Month))
        criterias.opAnd(New Criteria(GetType(MDPDealerMonthlyStock), "PeriodYear", MatchType.Exact, tgl.Year))

        If ddlKategori.SelectedValue <> -1 Then  '-- Category
            criterias.opAnd(New Criteria(GetType(MDPDealerMonthlyStock), "VechileColor.VechileType.Category.CategoryCode", MatchType.Exact, ddlKategori.SelectedItem.Text))
        End If
        If ddlModel.SelectedValue <> -1 Then ''-- Model
            CommonFunction.SetVehicleSubCategoryCriterias(ddlModel, ddlKategori.SelectedItem.Text, criterias, "VechileColor")
        End If
        If ddlTipe.SelectedValue <> -1 Then  '-- Vechile type
            criterias.opAnd(New Criteria(GetType(MDPDealerMonthlyStock), "VechileColor.VechileType.VechileTypeCode", MatchType.Exact, ddlTipe.SelectedItem.Text))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(MDPDealerMonthlyStock), "VechileColor.VechileType.Category.CategoryCode", Sort.SortDirection.ASC))
        sortColl.Add(New Sort(GetType(MDPDealerMonthlyStock), "VechileColor.VechileType.VechileTypeCode", Sort.SortDirection.ASC))

        '-- Retrieve color list
        Dim arrMDPPeriodStockList As ArrayList = New MDPDealerMonthlyStockFacade(User).Retrieve(criterias, sortColl)

        Dim MDPPeriodStockList As New ArrayList

        ' loop MDPDealerMonthlyStock array
        For Each item As MDPDealerMonthlyStock In arrMDPPeriodStockList
            item.RemainingStock = searchResultRemainingStock(item.PeriodMonth, item.PeriodYear, item.Dealer.ID, item.VechileColor.ID, item.ProductionYear)
            MDPPeriodStockList.Add(item)
        Next

        '-- Store vehicle color list into session for later use by Download
        _sessHelper.SetSession("sesMDPPeriodStockList", MDPPeriodStockList)

        If MDPPeriodStockList.Count > 0 Then
            btnDownload.Enabled = True  '-- Enable <Download> button
        Else
            MessageBox.Show(SR.DataNotFound("Data"))
        End If

    End Sub

    Private Sub BindPage(ByVal pageIndex As Integer)
        '-- Bind page-i

        '-- Read MDPPeriodStockList from session
        Dim MDPPeriodStockList As ArrayList = CType(_sessHelper.GetSession("sesMDPPeriodStockList"), ArrayList)

        If Not IsNothing(MDPPeriodStockList) AndAlso MDPPeriodStockList.Count <> 0 Then
            '-- Then paging
            Dim PagedList As ArrayList = ArrayListPager.DoPage(MDPPeriodStockList, pageIndex, dtgMain.PageSize)
            dtgMain.DataSource = PagedList
            dtgMain.VirtualItemCount = MDPPeriodStockList.Count
            dtgMain.CurrentPageIndex = pageIndex
            dtgMain.DataBind()
        Else
            '-- Display datagrid header only
            dtgMain.DataSource = New ArrayList
            dtgMain.VirtualItemCount = 0
            dtgMain.CurrentPageIndex = 0
            dtgMain.DataBind()
        End If

    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            lblNo.Text = (dtgMain.PageSize * dtgMain.CurrentPageIndex) + e.Item.ItemIndex + 1
            Dim lblPeriodStartDate As Label = e.Item.FindControl("lblPeriodStart")
            lblPeriodStartDate.Text = e.Item.DataItem.PeriodStartDate
            Dim lblPeriodEndDate As Label = e.Item.FindControl("lblPeriodEnd")
            lblPeriodEndDate.Text = e.Item.DataItem.PeriodEndDate
        End If
    End Sub

    Public Function searchResultRemainingStock(ByVal pMonth As Short, ByVal pYear As Short, ByVal dealerID As Short, ByVal vehiceColorID As Short, ByVal prodYear As Short) As Integer
        Dim sisaStok As Integer = 0
        Dim arrResult As New DataSet
        arrResult = New MDPDealerMonthlyStockFacade(User).RetrieveFromSP(pMonth, pYear, dealerID, vehiceColorID, prodYear)
        If Not IsNothing(arrResult) AndAlso arrResult.Tables.Count > 0 Then
            For Each dr As DataRow In arrResult.Tables(0).Rows
                sisaStok = dr.ItemArray.FirstOrDefault()
            Next
        End If
        Return sisaStok
    End Function

    Private Sub dtgMain_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgMain.PageIndexChanged
        BindPage(e.NewPageIndex)
    End Sub

    Private Sub dtgMain_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgMain.SortCommand
        If ViewState.Item("SortColumn") = e.SortExpression Then
            If ViewState.Item("SortDirection") = Sort.SortDirection.ASC Then
                ViewState.Item("SortDirection") = Sort.SortDirection.DESC
            Else
                ViewState.Item("SortDirection") = Sort.SortDirection.ASC
            End If
        Else
            ViewState.Item("SortColumn") = e.SortExpression
            ViewState.Item("SortDirection") = Sort.SortDirection.ASC
        End If
        BindPage(0)

    End Sub

    Private Sub BindDdlPeriode()
        For Each item As ListItem In LookUp.ArraylistMonth(True, 6, 1, DateTime.Now)
            ddlPeriode.Items.Add(item)
        Next
        ddlPeriode.SelectedValue = Format(DateTime.Now, "MMM yyyy").ToString

    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        If dtgMain.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang akan didownload")
            Exit Sub
        Else
            Dim MDPPeriodStockList As ArrayList = CType(_sessHelper.GetSession("sesMDPPeriodStockList"), ArrayList)

            If Not IsNothing(MDPPeriodStockList) AndAlso MDPPeriodStockList.Count <> 0 Then
                DoDownload(MDPPeriodStockList)
            End If
        End If
    End Sub

    Private Sub ddlKategori_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlKategori.SelectedIndexChanged
        BindDdlType()
        FillModel(ddlKategori.SelectedValue)
    End Sub

#End Region

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub ddlModel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlModel.SelectedIndexChanged
        BindDdlType()
    End Sub
End Class
