Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.MDP
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade
Imports System.Linq
Imports System.Collections.Generic
Imports System.Data


Public Class FrmMDPDailyStock
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
#End Region

#Region "Custom Methods"

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.MDP_Alokasi_Unit_Harian_Lihat_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=MDPDealerDaily")
        End If
    End Sub

    Private Sub Initialization()
        ViewState.Add("SortColumn", "DealerCode")
        ViewState.Add("SortDirection", Sort.SortDirection.ASC)

        BindDdlCategory()
        BindDdlType()
        BindDdlTahunPerakitan()
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

    Private Sub BindDdlType()
        ddlTipe.Items.Clear()
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

        If ddlModel.SelectedValue = -1 Then
            ddlTipe.SelectedValue = -1
            Exit Sub
        End If
    End Sub

    Private Sub BindDdlTahunPerakitan()
        Me.ddlTahunPerakitan.DataSource = LookUp.ArraylistYear(True, 10, 1, DateTime.Now.Year.ToString)
        Me.ddlTahunPerakitan.DataBind()
        Me.ddlTahunPerakitan.SelectedValue = Date.Now.Year
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
        ddlTahunPerakitan.SelectedValue = Date.Now.Year
        Menu1.Items.Clear()
        GridView1.DataSource = Nothing
        GridView1.DataBind()
        GridView2.DataSource = Nothing
        GridView2.DataBind()
        GridView3.DataSource = Nothing
        GridView3.DataBind()
        btnCari.Enabled = True
    End Sub

    Private Sub SetSessionCriteria()
        Dim objSCMDPDaily As ArrayList = New ArrayList
        objSCMDPDaily.Add(txtKodeDealer.Text)
        objSCMDPDaily.Add(ddlKategori.SelectedIndex)
        objSCMDPDaily.Add(ddlModel.SelectedIndex)
        objSCMDPDaily.Add(ddlTipe.SelectedIndex)
        objSCMDPDaily.Add(ddlPeriode.SelectedIndex)
        objSCMDPDaily.Add(ddlTahunPerakitan.SelectedIndex)

        _sessHelper.SetSession("SESMDPDAILYPRODYEAR", ddlTahunPerakitan.SelectedValue)
        _sessHelper.SetSession("SESMDPDAILYCRITERIA", objSCMDPDaily)
    End Sub

    Private Function GetSessionCriteria() As Boolean
        Dim objSCMDPDaily As ArrayList = _sessHelper.GetSession("SESMDPDAILYCRITERIA")
        If Not objSCMDPDaily Is Nothing Then
            txtKodeDealer.Text = objSCMDPDaily.Item(0)
            ddlKategori.SelectedIndex = objSCMDPDaily.Item(1)
            ddlKategori_SelectedIndexChanged(Nothing, Nothing)
            ddlModel.SelectedIndex = objSCMDPDaily.Item(2)
            ddlTipe.SelectedIndex = objSCMDPDaily.Item(3)
            ddlPeriode.SelectedIndex = objSCMDPDaily.Item(4)
            ddlTahunPerakitan.SelectedIndex = objSCMDPDaily.Item(5)

            Return True
        End If
        Return False
    End Function

#End Region

#Region "Event Handler"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        CheckUserPrivilege()
        If Not Me.IsPostBack Then
            Initialization()
            BindDdlPeriode()

            Dim objUser As UserInfo = CType(_sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
            If objUser.Dealer.Title <> "1" Then
                txtKodeDealer.Text = objUser.Dealer.DealerCode
                txtKodeDealer.Enabled = False
                lblSearchDealer.Visible = False
            End If
            'Load_ListTable(0)
            If GetSessionCriteria() Then
                BindSearchGrid()
            End If
        End If
    End Sub

    Private Sub Load_ListTable(ByVal index As Integer)
        addTabs(index)
        addHeaderColumn()
        addColumn()
        BindGrid()
    End Sub

    Protected Sub GridView1_OnRowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then

            Dim row As GridViewRow = New GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Insert)
            Dim th As New TableHeaderCell()

            Dim MDPDailyStockList As DataSet = CType(_sessHelper.GetSession("sesMDPDailyStockList"), DataSet)

            th = New TableHeaderCell
            th.HorizontalAlign = HorizontalAlign.Center
            th.Text = ""
            row.Controls.Add(th)
            CType(Me.GridView1.Controls(0), Table).Controls.AddAt(0, row)

            th = New TableHeaderCell
            th.HorizontalAlign = HorizontalAlign.Center
            th.ColumnSpan = 3
            th.Text = "Model / Tipe / Warna"
            row.Controls.Add(th)
            CType(Me.GridView1.Controls(0), Table).Controls.AddAt(0, row)

            Dim i As Integer = 0
            Dim StartDate As Integer
            Dim EndDate As Integer
            For Each dt As DataRow In MDPDailyStockList.Tables(0).Rows
                Dim PeriodStartDate As DateTime = System.Convert.ToDateTime(dt("PeriodStartDate"))
                Dim PeriodEndDate As DateTime = System.Convert.ToDateTime(dt("PeriodEndDate"))
                StartDate = PeriodStartDate.Day
                EndDate = PeriodEndDate.Day

                'cek max periode date dalam 1 periode
                Dim checkDatePeriode As ArrayList = New MDPDealerDailyStockFacade(User).RetrieveMaxPeriodeDate(EndDate)

                Dim maxDate As Integer = Integer.MinValue

                For Each item As MDPDealerDailyStock In checkDatePeriode
                    maxDate = Math.Max(maxDate, item.PeriodeDate)
                Next

                If i = 0 Then
                    For Each dc As DataColumn In MDPDailyStockList.Tables(1).Columns
                        For clm As Integer = StartDate To maxDate
                            If clm.ToString = dc.ColumnName Then
                                th = New TableHeaderCell
                                th.HorizontalAlign = HorizontalAlign.Center
                                th.ColumnSpan = 2
                                th.Text = dc.ColumnName
                                th.Attributes.Add("onmouseover", "this.style.cursor='hand'")
                                Dim dateValue As String = PeriodStartDate.Year.ToString + "/" + PeriodStartDate.Month.ToString + "/" + dc.ColumnName.ToString
                                th.Attributes.Add("OnCLick", "OpenRedirect(" + "'" + dateValue + "'" + ")")
                                th.ToolTip = "Buat PO Draft"
                                row.Controls.Add(th)
                                CType(Me.GridView1.Controls(0), Table).Controls.AddAt(0, row)
                            End If
                        Next

                        If dc.ColumnName = maxDate.ToString Then
                            th = New TableHeaderCell
                            th.HorizontalAlign = HorizontalAlign.Center
                            th.ColumnSpan = 3
                            th.Text = "Total Per Bulan"
                            row.Controls.Add(th)
                            CType(Me.GridView1.Controls(0), Table).Controls.AddAt(0, row)
                            Exit Sub
                        End If
                    Next
                End If
            Next
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            Dim RowTotals(GridView1.Columns.Count - 1) As Integer
            For row As Integer = 0 To GridView1.Rows.Count - 1
                For cell As Integer = 4 To GridView1.Rows(row).Cells.Count - 1
                    If (IsNumeric(GridView1.Rows(row).Cells(cell).Text)) Then
                        RowTotals(cell) += Convert.ToInt32(GridView1.Rows(row).Cells(cell).Text)
                    End If
                Next
            Next

            e.Row.Cells(0).Text = "Total"
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            Dim intColSpan As Short = 4
            e.Row.Cells(0).ColumnSpan = intColSpan

            If intColSpan >= 1 Then
                For i As Integer = 1 To intColSpan - 1
                    e.Row.Cells.RemoveAt(e.Row.Cells.Count - i)
                Next
            End If


            For cell As Integer = 4 To GridView1.Columns.Count - 1
                e.Row.Cells(cell - 3).Text = RowTotals(cell)
            Next

        End If

    End Sub

    Protected Sub GridView2_OnRowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then

            Dim row As GridViewRow = New GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Insert)
            Dim th As New TableHeaderCell()

            Dim MDPDailyStockList As DataSet = CType(_sessHelper.GetSession("sesMDPDailyStockList"), DataSet)

            th = New TableHeaderCell
            th.HorizontalAlign = HorizontalAlign.Center
            th.Text = ""
            row.Controls.Add(th)
            CType(Me.GridView2.Controls(0), Table).Controls.AddAt(0, row)

            th = New TableHeaderCell
            th.HorizontalAlign = HorizontalAlign.Center
            th.ColumnSpan = 3
            th.Text = "Model / Tipe / Warna"
            row.Controls.Add(th)
            CType(Me.GridView2.Controls(0), Table).Controls.AddAt(0, row)

            Dim i As Integer = 0
            For Each dt As DataRow In MDPDailyStockList.Tables(0).Rows
                Dim PeriodStartDate As DateTime = System.Convert.ToDateTime(dt("PeriodStartDate"))
                Dim PeriodEndDate As DateTime = System.Convert.ToDateTime(dt("PeriodEndDate"))
                Dim StartDate As Integer = PeriodStartDate.Day
                Dim EndDate As Integer = PeriodEndDate.Day

                'cek max periode date dalam 1 periode
                Dim checkDatePeriode As ArrayList = New MDPDealerDailyStockFacade(User).RetrieveMaxPeriodeDate(EndDate)

                Dim maxDate As Integer = Integer.MinValue

                For Each item As MDPDealerDailyStock In checkDatePeriode
                    maxDate = Math.Max(maxDate, item.PeriodeDate)
                Next

                If i = 1 Then
                    For Each dc As DataColumn In MDPDailyStockList.Tables(1).Columns
                        For clm As Integer = StartDate To maxDate
                            If clm.ToString = dc.ColumnName Then
                                th = New TableHeaderCell
                                th.HorizontalAlign = HorizontalAlign.Center
                                th.ColumnSpan = 2
                                th.Text = dc.ColumnName
                                th.Attributes.Add("onmouseover", "this.style.cursor='hand'")
                                Dim dateValue As String = PeriodStartDate.Year.ToString + "/" + PeriodStartDate.Month.ToString + "/" + dc.ColumnName.ToString
                                th.Attributes.Add("OnCLick", "OpenRedirect(" + "'" + dateValue + "'" + ")")
                                th.ToolTip = "Buat PO Draft"
                                row.Controls.Add(th)
                                CType(Me.GridView2.Controls(0), Table).Controls.AddAt(0, row)
                            End If
                        Next
                        If dc.ColumnName = maxDate.ToString Then
                            th = New TableHeaderCell
                            th.HorizontalAlign = HorizontalAlign.Center
                            th.ColumnSpan = 3
                            th.Text = "Total Per Bulan"
                            row.Controls.Add(th)
                            CType(Me.GridView2.Controls(0), Table).Controls.AddAt(0, row)
                            Exit Sub
                        End If
                    Next
                End If
                i += 1
            Next
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            Dim RowTotals(GridView2.Columns.Count - 1) As Integer
            For row As Integer = 0 To GridView2.Rows.Count - 1
                For cell As Integer = 4 To GridView2.Rows(row).Cells.Count - 1
                    If (IsNumeric(GridView2.Rows(row).Cells(cell).Text)) Then
                        RowTotals(cell) += Convert.ToInt32(GridView2.Rows(row).Cells(cell).Text)
                    End If
                Next
            Next

            e.Row.Cells(0).Text = "Total"
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            Dim intColSpan As Short = 4
            e.Row.Cells(0).ColumnSpan = intColSpan

            If intColSpan >= 1 Then
                For i As Integer = 1 To intColSpan - 1
                    e.Row.Cells.RemoveAt(e.Row.Cells.Count - i)
                Next
            End If

            For cell As Integer = 4 To GridView2.Columns.Count - 1
                e.Row.Cells(cell - 3).Text = RowTotals(cell)
            Next

        End If
    End Sub

    Protected Sub GridView3_OnRowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView3.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then

            Dim row As GridViewRow = New GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Insert)
            Dim th As New TableHeaderCell()

            Dim MDPDailyStockList As DataSet = CType(_sessHelper.GetSession("sesMDPDailyStockList"), DataSet)

            th = New TableHeaderCell
            th.HorizontalAlign = HorizontalAlign.Center
            th.Text = ""
            row.Controls.Add(th)
            CType(Me.GridView3.Controls(0), Table).Controls.AddAt(0, row)

            th = New TableHeaderCell
            th.HorizontalAlign = HorizontalAlign.Center
            th.ColumnSpan = 3
            th.Text = "Model / Tipe / Warna"
            row.Controls.Add(th)
            CType(Me.GridView3.Controls(0), Table).Controls.AddAt(0, row)

            Dim i As Integer = 0
            For Each dt As DataRow In MDPDailyStockList.Tables(0).Rows
                Dim PeriodStartDate As DateTime = System.Convert.ToDateTime(dt("PeriodStartDate"))
                Dim PeriodEndDate As DateTime = System.Convert.ToDateTime(dt("PeriodEndDate"))
                Dim StartDate As Integer = PeriodStartDate.Day
                Dim EndDate As Integer = PeriodEndDate.Day

                'cek max periode date dalam 1 periode
                Dim checkDatePeriode As ArrayList = New MDPDealerDailyStockFacade(User).RetrieveMaxPeriodeDate(EndDate)

                Dim maxDate As Integer = Integer.MinValue

                For Each item As MDPDealerDailyStock In checkDatePeriode
                    maxDate = Math.Max(maxDate, item.PeriodeDate)
                Next

                If i = 2 Then
                    For Each dc As DataColumn In MDPDailyStockList.Tables(1).Columns
                        For clm As Integer = StartDate To maxDate
                            If clm.ToString = dc.ColumnName Then
                                th = New TableHeaderCell
                                th.HorizontalAlign = HorizontalAlign.Center
                                th.ColumnSpan = 2
                                th.Text = dc.ColumnName
                                th.Attributes.Add("onmouseover", "this.style.cursor='hand'")
                                Dim dateValue As String = PeriodStartDate.Year.ToString + "/" + PeriodStartDate.Month.ToString + "/" + dc.ColumnName.ToString
                                th.Attributes.Add("OnCLick", "OpenRedirect(" + "'" + dateValue + "'" + ")")
                                th.ToolTip = "Buat PO Draft"
                                row.Controls.Add(th)
                                CType(Me.GridView3.Controls(0), Table).Controls.AddAt(0, row)
                            End If
                        Next
                        If dc.ColumnName = maxDate.ToString Then
                            th = New TableHeaderCell
                            th.HorizontalAlign = HorizontalAlign.Center
                            th.ColumnSpan = 3
                            th.Text = "Total Per Bulan"
                            row.Controls.Add(th)
                            CType(Me.GridView3.Controls(0), Table).Controls.AddAt(0, row)
                            Exit Sub
                        End If
                    Next
                End If
                i += 1
            Next
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            Dim RowTotals(GridView3.Columns.Count - 1) As Integer
            For row As Integer = 0 To GridView3.Rows.Count - 1
                For cell As Integer = 4 To GridView3.Rows(row).Cells.Count - 1
                    If (IsNumeric(GridView3.Rows(row).Cells(cell).Text)) Then
                        RowTotals(cell) += Convert.ToInt32(GridView3.Rows(row).Cells(cell).Text)
                    End If
                Next
            Next

            e.Row.Cells(0).Text = "Total"
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            Dim intColSpan As Short = 4
            e.Row.Cells(0).ColumnSpan = intColSpan

            If intColSpan >= 1 Then
                For i As Integer = 1 To intColSpan - 1
                    e.Row.Cells.RemoveAt(e.Row.Cells.Count - i)
                Next
            End If

            For cell As Integer = 4 To GridView3.Columns.Count - 1
                e.Row.Cells(cell - 3).Text = RowTotals(cell)
            Next

        End If
    End Sub

    Private Sub addTabs(ByVal index As Integer)
        Dim mItem As New MenuItem

        Menu1.Items.Clear()

        BindDataGrid()

        Dim MDPDailyStockList As DataSet = CType(_sessHelper.GetSession("sesMDPDailyStockList"), DataSet)
        Dim value As Integer = 0

        If MDPDailyStockList.Tables(0).Rows.Count > 0 Then
            For Each dt As DataRow In MDPDailyStockList.Tables(0).Rows
                Dim PeriodStartDate As DateTime = System.Convert.ToDateTime(dt("PeriodStartDate"))
                Dim PeriodEndDate As DateTime = System.Convert.ToDateTime(dt("PeriodEndDate"))
                mItem = New MenuItem
                mItem.Text = "Periode " + PeriodStartDate.Day.ToString + " s/d " + PeriodEndDate.Day.ToString + " " + MonthName(PeriodEndDate.Month).ToString + " " + PeriodEndDate.Year.ToString
                mItem.Value = value
                If (value = index) Then
                    mItem.Selected = True
                End If
                Menu1.Items.Add(mItem)
                value += 1
            Next
        End If
        MultiView1.ActiveViewIndex = index
        MultiView1.Visible = True
    End Sub

    Private Sub addHeaderColumn()
        Dim bfield As New BoundField()

        Dim MDPDailyStockList As DataSet = CType(_sessHelper.GetSession("sesMDPDailyStockList"), DataSet)

        If MDPDailyStockList.Tables(0).Rows.Count > 0 Then

            For i As Integer = 0 To MDPDailyStockList.Tables(0).Rows.Count - 1
                If i = 0 Then
                    GridView1.Columns.Clear()

                    bfield = New BoundField()
                    bfield.HeaderText = "No"
                    bfield.DataField = "Nomor"
                    GridView1.Columns.Add(bfield)

                    bfield = New BoundField()
                    bfield.HeaderText = "Deskripsi Kendaraan"
                    bfield.DataField = "Model"
                    bfield.ItemStyle.CssClass = "hModel"
                    GridView1.Columns.Add(bfield)

                    bfield = New BoundField()
                    bfield.HeaderText = "Kode Tipe"
                    bfield.DataField = "KodeTipe"
                    GridView1.Columns.Add(bfield)

                    bfield = New BoundField()
                    bfield.HeaderText = "Kode Warna"
                    bfield.DataField = "KodeWarna"
                    GridView1.Columns.Add(bfield)
                End If

                If i = 1 Then
                    GridView2.Columns.Clear()

                    bfield = New BoundField()
                    bfield.HeaderText = "No"
                    bfield.DataField = "Nomor"
                    GridView2.Columns.Add(bfield)

                    bfield = New BoundField()
                    bfield.HeaderText = "Deskripsi Kendaraan"
                    bfield.DataField = "Model"
                    bfield.ItemStyle.CssClass = "hModel"
                    GridView2.Columns.Add(bfield)

                    bfield = New BoundField()
                    bfield.HeaderText = "Kode Tipe"
                    bfield.DataField = "KodeTipe"
                    GridView2.Columns.Add(bfield)

                    bfield = New BoundField()
                    bfield.HeaderText = "Kode Warna"
                    bfield.DataField = "KodeWarna"
                    GridView2.Columns.Add(bfield)
                End If

                If i = 2 Then
                    GridView3.Columns.Clear()

                    bfield = New BoundField()
                    bfield.HeaderText = "No"
                    bfield.DataField = "Nomor"
                    GridView3.Columns.Add(bfield)

                    bfield = New BoundField()
                    bfield.HeaderText = "Deskripsi Kendaraan"
                    bfield.DataField = "Model"
                    bfield.ItemStyle.CssClass = "hModel"
                    GridView3.Columns.Add(bfield)

                    bfield = New BoundField()
                    bfield.HeaderText = "Kode Tipe"
                    bfield.DataField = "KodeTipe"
                    GridView3.Columns.Add(bfield)

                    bfield = New BoundField()
                    bfield.HeaderText = "Kode Warna"
                    bfield.DataField = "KodeWarna"
                    GridView3.Columns.Add(bfield)
                End If

            Next
        End If

    End Sub

    Private Sub addColumn()
        Dim bfield As New BoundField()
        Dim MDPDailyStockList As DataSet = CType(_sessHelper.GetSession("sesMDPDailyStockList"), DataSet)

        'Columnn Periode
        Dim i As Integer = 0
        For Each dr As DataRow In MDPDailyStockList.Tables(0).Rows
            Dim PeriodStartDate As DateTime = System.Convert.ToDateTime(dr("PeriodStartDate"))
            Dim PeriodEndDate As DateTime = System.Convert.ToDateTime(dr("PeriodEndDate"))
            Dim StartDate As Integer = PeriodStartDate.Day
            Dim EndDate As Integer = PeriodEndDate.Day

            'cek max periode date dalam 1 periode
            Dim checkDatePeriode As ArrayList = New MDPDealerDailyStockFacade(User).RetrieveMaxPeriodeDate(EndDate)

            Dim maxDate As Integer = Integer.MinValue

            For Each item As MDPDealerDailyStock In checkDatePeriode
                maxDate = Math.Max(maxDate, item.PeriodeDate)
            Next

            For Each dc As DataColumn In MDPDailyStockList.Tables(1).Columns
                For clm As Integer = StartDate To maxDate
                    If clm.ToString = dc.ColumnName Then
                        If i = 0 Then
                            bfield = New BoundField()
                            bfield.HeaderText = "Delivery Plan"
                            bfield.DataField = dc.ColumnName
                            GridView1.Columns.Add(bfield)

                            bfield = New BoundField()
                            bfield.HeaderText = "Sisa PO Draft"
                            bfield.DataField = "Sisa" + dc.ColumnName.ToString
                            GridView1.Columns.Add(bfield)
                        End If
                        If i = 1 Then
                            bfield = New BoundField()
                            bfield.HeaderText = "Delivery Plan"
                            bfield.DataField = dc.ColumnName
                            GridView2.Columns.Add(bfield)

                            bfield = New BoundField()
                            bfield.HeaderText = "Sisa PO Draft"
                            bfield.DataField = "Sisa" + dc.ColumnName.ToString
                            GridView2.Columns.Add(bfield)
                        End If
                        If i = 2 Then
                            bfield = New BoundField()
                            bfield.HeaderText = "Delivery Plan"
                            bfield.DataField = dc.ColumnName
                            GridView3.Columns.Add(bfield)

                            bfield = New BoundField()
                            bfield.HeaderText = "Sisa PO Draft"
                            bfield.DataField = "Sisa" + dc.ColumnName.ToString
                            GridView3.Columns.Add(bfield)
                        End If
                    End If
                Next

                If dc.ColumnName = maxDate.ToString Then
                    If i = 0 Then
                        bfield = New BoundField()
                        bfield.HeaderText = "Total OC"
                        bfield.DataField = "TotalOC"
                        GridView1.Columns.Add(bfield)

                        bfield = New BoundField()
                        bfield.HeaderText = "Total DO"
                        bfield.DataField = "TotalDO"
                        GridView1.Columns.Add(bfield)

                        bfield = New BoundField()
                        bfield.HeaderText = "Total Sisa OC"
                        bfield.DataField = "TotalSisaOC"
                        GridView1.Columns.Add(bfield)
                    End If
                    If i = 1 Then
                        bfield = New BoundField()
                        bfield.HeaderText = "Total OC"
                        bfield.DataField = "TotalOC"
                        GridView2.Columns.Add(bfield)

                        bfield = New BoundField()
                        bfield.HeaderText = "Total DO"
                        bfield.DataField = "TotalDO"
                        GridView2.Columns.Add(bfield)

                        bfield = New BoundField()
                        bfield.HeaderText = "Total Sisa OC"
                        bfield.DataField = "TotalSisaOC"
                        GridView2.Columns.Add(bfield)
                    End If
                    If i = 2 Then
                        bfield = New BoundField()
                        bfield.HeaderText = "Total OC"
                        bfield.DataField = "TotalOC"
                        GridView3.Columns.Add(bfield)

                        bfield = New BoundField()
                        bfield.HeaderText = "Total DO"
                        bfield.DataField = "TotalDO"
                        GridView3.Columns.Add(bfield)

                        bfield = New BoundField()
                        bfield.HeaderText = "Total Sisa OC"
                        bfield.DataField = "TotalSisaOC"
                        GridView3.Columns.Add(bfield)
                    End If
                    Exit For
                End If
            Next
            i += 1
        Next

    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        SetSessionCriteria()
        BindSearchGrid()
    End Sub

    Private Sub BindSearchGrid()
        Load_ListTable(0)
    End Sub

    Private Function GetData() As DataSet
        Dim objDealer As New Dealer
        Dim objVechileColor As New VechileColor

        '-- Dealer Code
        If txtKodeDealer.Text <> "" Then
            objDealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text)
        End If

        '-- Periode
        Dim tgl As DateTime = System.Convert.ToDateTime(ddlPeriode.SelectedValue)
        Dim prodYear As String = ddlTahunPerakitan.SelectedValue

        '-- Category
        Dim KodeKategori As String
        If ddlKategori.SelectedValue <> -1 Then
            KodeKategori = ddlKategori.SelectedItem.Text
        End If

        Dim model As String
        If ddlModel.SelectedValue <> -1 Then
            model = ddlModel.SelectedItem.Text
        End If

        '-- Vechile type
        Dim tipe As String
        If ddlTipe.SelectedValue <> -1 Then
            tipe = ddlTipe.SelectedItem.Text
        End If

        Return searchResult(tgl.Month, tgl.Year, objDealer.ID, KodeKategori, model, tipe, prodYear)
    End Function

    Private Sub BindDataGrid()
        Dim dt As DataSet = GetData()
        Dim dtrow As New DataTable
        Me._sessHelper.SetSession("sesMDPDailyStockList", dt)
    End Sub

    Private Sub BindGrid()
        Dim MDPDailyStockList As DataSet = CType(_sessHelper.GetSession("sesMDPDailyStockList"), DataSet)
        If Not IsNothing(MDPDailyStockList) Then
            If MDPDailyStockList.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To MDPDailyStockList.Tables(0).Rows.Count - 1
                    If i = 0 Then
                        Me.GridView1.DataSource = MDPDailyStockList.Tables(1) 'dt.Tables(1)
                        Me.GridView1.DataBind()
                    End If
                    If i = 1 Then
                        Me.GridView2.DataSource = MDPDailyStockList.Tables(1)
                        Me.GridView2.DataBind()
                    End If
                    If i = 2 Then
                        Me.GridView3.DataSource = MDPDailyStockList.Tables(1)
                        Me.GridView3.DataBind()
                    End If
                Next
            Else
                MessageBox.Show(SR.DataNotFound("Data"))
                Me.GridView1.DataSource = Nothing
                Me.GridView1.DataBind()
                Me.GridView2.DataSource = Nothing
                Me.GridView2.DataBind()
                Me.GridView3.DataSource = Nothing
                Me.GridView3.DataBind()
            End If
        End If
    End Sub

    Public Function searchResult(ByVal pMonth As Short, ByVal pYear As Short, ByVal dealerID As Short, ByVal categoryCode As String, ByVal modelDesc As String, ByVal tipeCode As String, ByVal prodYear As Short) As DataSet
        Dim arrResult As New DataSet

        Dim qWhere As String = "where dd.rowStatus = 0 "
        qWhere += "and dd.PeriodMonth = " & pMonth & " "
        qWhere += "and dd.PeriodYear = " & pYear & " "
        qWhere += "and dd.ProductionYear = " & prodYear & " "
        If dealerID <> 0 Then
            qWhere += "and dd.dealerid = " & dealerID & " "
        End If
        If Not String.IsNullOrEmpty(categoryCode) Then
            qWhere += "and c.CategoryCode = ''" & categoryCode & "'' "
        End If
        If Not String.IsNullOrEmpty(modelDesc) Then
            qWhere += "and vm.Description = ''" & modelDesc & "'' "
        End If
        If Not String.IsNullOrEmpty(tipeCode) Then
            qWhere += "and vt.VechileTypeCode = ''" & tipeCode & "'' "
        End If

        arrResult = New MDPDealerDailyStockFacade(User).RetrieveFromSP(prodYear, pMonth, pYear, dealerID, qWhere)

        Return arrResult
    End Function

    Private Sub BindDdlPeriode()
        For Each item As ListItem In LookUp.ArraylistMonth(True, 6, 1, DateTime.Now)
            ddlPeriode.Items.Add(item)
        Next
        ddlPeriode.SelectedValue = Format(DateTime.Now, "MMM yyyy").ToString

    End Sub

    Private Sub ddlKategori_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlKategori.SelectedIndexChanged
        BindDdlType()
        FillModel(ddlKategori.SelectedValue)
    End Sub

#End Region

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub Menu1_MenuItemClick(sender As Object, e As MenuEventArgs) Handles Menu1.MenuItemClick
        Dim index = Int32.Parse(e.Item.Value)
        MultiView1.ActiveViewIndex = index
        addTabs(index)
        BindGrid()
    End Sub

    Private Sub ddlModel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlModel.SelectedIndexChanged
        BindDdlType()
    End Sub
End Class