Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Public Class frmDisplayPKReleaseHeader
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblJenisPesanan As System.Web.UI.WebControls.Label
    Protected WithEvents ddlJenisPesanan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblKategori As System.Web.UI.WebControls.Label
    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents lblTipe As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTipe As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTipeWarna As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTipeWarna As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgProduction As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlPeriode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlSubCategory As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private arlPKDetail As ArrayList
    Private arlGroupPKDetail As ArrayList
    Private objPKDetail As PKDetail
    Private sessionHelper As New sessionHelper
    Private TotPesanan As Double = 0
    Private TotRilis As Double = 0
    Private TotSisaStok As Double = 0

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ActivateUserPrivilege()
        If Not IsPostBack Then
            BindddlJenisPesanan()
            BindddlPeriode()
            BindddlCategory()
            CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlKategori.SelectedItem.Text)

            If ddlKategori.Items.Count <> 0 Then
                BindddlTipe(ddlKategori.SelectedItem.ToString)
                If ddlTipe.Items.Count <> 0 Then
                    BindddlTipeWarna(ddlTipe.SelectedItem.ToString)
                End If
            End If
            If (Request.QueryString("OrderType") <> String.Empty) Then
                ViewState("OrderType") = Request.QueryString("OrderType")
                ViewState("Periode") = Request.QueryString("Periode")
                ViewState("Category") = Request.QueryString("Category")
                ViewState("Type") = Request.QueryString("Type")
                ViewState("MaterialNumber") = Request.QueryString("MaterialNumber")
                RefreshPKDetail()
            End If
            If ddlKategori.Items.Count = 0 OrElse ddlJenisPesanan.Items.Count = 0 Then
                btnCari.Enabled = False
            End If
        End If
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.DaftarStatusRilisView_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar Status Rilis PK")
        End If
    End Sub

    Private Sub RefreshPKDetail()
        ddlJenisPesanan.SelectedValue = Request.QueryString("OrderType")
        ddlJenisPesanan_SelectedIndexChanged(Nothing, Nothing)
        ddlPeriode.SelectedValue = Request.QueryString("Periode")
        ddlKategori.SelectedValue = Request.QueryString("Category")
        ddlKategori_SelectedIndexChanged(Nothing, Nothing)
        ddlTipe.SelectedValue = Request.QueryString("Type")
        ddlTipe_SelectedIndexChanged(Nothing, Nothing)
        ddlTipeWarna.SelectedValue = Request.QueryString("MaterialNumber")
        btnCari_Click(Nothing, Nothing)
    End Sub

    Private Sub BindddlJenisPesanan()
        For Each item As ListItem In LookUp.ArrayJenisPesanan
            If item.Value = 0 Then
                If SecurityProvider.Authorize(Context.User, SR.PKKindBulanan_Privilege) Then
                    ddlJenisPesanan.Items.Add(item)
                End If
            Else
                If SecurityProvider.Authorize(Context.User, SR.PKKindTambahan_Privilege) Then
                    ddlJenisPesanan.Items.Add(item)
                End If
            End If
        Next
        ddlJenisPesanan.SelectedIndex = 0
        ddlJenisPesanan_SelectedIndexChanged(Nothing, Nothing)
        'lblPeriodeAlokasiValue.Text = Format(DateTime.Now.AddMonths(1), "MMM yyyy")
    End Sub
    Private Sub BindddlPeriode()
        Me.ddlPeriode.DataSource = LookUp.ArraylistMonth(True, 6, 1, DateTime.Now)
        Me.ddlPeriode.DataBind()
        Me.ddlPeriode.SelectedValue = Format(DateTime.Now, "MMM yyyy")
    End Sub
    Private Sub BindddlCategory()
        Dim arrayListCategory As ArrayList = New PKHeaderFacade(User).RetrieveListCategory

        For Each item As Category In arrayListCategory
            Dim listItem As New listItem(item.CategoryCode, item.ID)
            If item.CategoryCode = "PC" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryPC_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                    ddlKategori.Items.Add(listItem)
                End If
            ElseIf item.CategoryCode = "LCV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryLCV_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                    ddlKategori.Items.Add(listItem)
                End If
            ElseIf item.CategoryCode = "CV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryCV_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                    ddlKategori.Items.Add(listItem)
                End If
            End If
        Next
    End Sub

    Private Sub BindddlTipe(ByVal Category As String)
        ddlTipe.Items.Clear()
        If ddlSubCategory.SelectedValue <> "-1" Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim year As Integer = CInt(ddlPeriode.SelectedValue.Split(" ")(1))
            Dim strSql As String = String.Format(
                String.Join(
                Environment.NewLine,
    "SELECT VechileTypeID FROM VechileColorIsActiveOnPK WITH (NOLOCK) ",
    "INNER JOIN VechileColor on VechileColorIsActiveOnPK.VehicleColorID=VechileColor.id",
    "WHERE (VechileColorIsActiveOnPK.RowStatus = 0",
    "AND VechileColorIsActiveOnPK.Status = 1",
    "AND VechileColorIsActiveOnPK.ProductionYear = {0}",
    "AND VechileColor.Status <> 'X')"
    ), year)

            criterias.opAnd(New Criteria(GetType(VechileType), "ID", MatchType.InSet, "(" & strSql & ")"))
            criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.No, "X"))

            Dim strSql2 As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlSubCategory.SelectedValue
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql2 & ")"))

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("VechileTypeCode")) Then
                sortColl.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            ddlTipe.DataSource = New VechileTypeFacade(User).Retrieve(criterias, sortColl)
            ddlTipe.DataTextField = "VechileTypeCode"
            ddlTipe.DataValueField = "id"
            ddlTipe.DataBind()
        End If
        ddlTipe.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Private Sub BindddlTipeWarna(ByVal KodeTipe As String)
        ddlTipeWarna.Items.Clear()
        If ddlTipe.SelectedIndex <> 0 Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim year As Integer = CInt(ddlPeriode.SelectedValue.Split(" ")(1))
            Dim strSql As String = String.Format(
                String.Join(
                Environment.NewLine,
    "Select A.VehicleColorID",
    "FROM VechileColorIsActiveOnPK AS A with (nolock) ",
    "WHERE ",
    "(1=1 ",
    "AND A.RowStatus = 0 ",
    "AND A.ProductionYear = {0} ",
    "AND A.Status = 1",
    ")"
    ), year)


            criterias.opAnd(New Criteria(GetType(VechileColor), "ID", MatchType.InSet, "(" & strSql & ")"))
            criterias.opAnd(New Criteria(GetType(VechileColor), "VechileType.VechileTypeCode", MatchType.Exact, KodeTipe))
            criterias.opAnd(New Criteria(GetType(VechileColor), "Status", MatchType.No, "X"))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("MaterialNumber")) Then
                sortColl.Add(New Sort(GetType(VechileColor), "MaterialNumber", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            ddlTipeWarna.DataSource = New VechileColorFacade(User).Retrieve(criterias, sortColl)
            ddlTipeWarna.DataTextField = "MaterialNumber"
            ddlTipeWarna.DataValueField = "id"
            ddlTipeWarna.DataBind()
        End If
        ddlTipeWarna.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Private RilisDetailViewPrivilege As Boolean
    Sub dtgProduction_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If Not (arlPKDetail Is Nothing) Then
            If Not (arlPKDetail.Count = 0 Or e.Item.ItemIndex = -1) Then
                If e.Item.ItemIndex = 0 Then
                    RilisDetailViewPrivilege = SecurityProvider.Authorize(Context.User, SR.DaftarStatusRilisViewDetail_Privilege)
                End If
                objPKDetail = arlGroupPKDetail(e.Item.ItemIndex)
                e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dtgProduction.PageSize * dtgProduction.CurrentPageIndex)).ToString
                e.Item.Cells(2).Text = objPKDetail.VechileColor.MaterialDescription
                e.Item.Cells(3).Text = objPKDetail.VechileColor.MaterialNumber
                e.Item.Cells(4).Text = objPKDetail.PKHeader.ProductionYear

                e.Item.Cells(5).Text = CountTotalQty(objPKDetail).ToString
                TotPesanan += CType(e.Item.Cells(5).Text, Double)

                e.Item.Cells(6).Text = CountTotalAlokasi(objPKDetail).ToString
                TotRilis += CType(e.Item.Cells(6).Text, Double)

                'e.Item.Cells(6).Text = CInt(e.Item.Cells(4).Text) - CInt(e.Item.Cells(5).Text)
                Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                lbtnEdit.Text = "<img src=""../images/detail.gif"" border=""0"" alt=""Lihat"">"
                If Not RilisDetailViewPrivilege Then
                    lbtnEdit.Visible = False
                End If
                'e.Item.Cells(8).Text = IIf(CInt(e.Item.Cells(5).Text) > 0, "*", "")
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKProductionPlan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(PKProductionPlan), "VechileColor.MaterialNumber", MatchType.Exact, e.Item.Cells(3).Text))
                Dim tgl As DateTime = System.Convert.ToDateTime(ddlPeriode.SelectedValue)
                criterias.opAnd(New Criteria(GetType(PKProductionPlan), "PeriodMonth", MatchType.Exact, tgl.Month))
                criterias.opAnd(New Criteria(GetType(PKProductionPlan), "PeriodYear", MatchType.Exact, tgl.Year))
                criterias.opAnd(New Criteria(GetType(PKProductionPlan), "ProductionYear", MatchType.Exact, e.Item.Cells(4).Text))
                Dim arlPKProductionPlan As ArrayList = New PKProductionPlanFacade(User).Retrieve(criterias)
                If arlPKProductionPlan.Count <> 0 Then
                    e.Item.Cells(7).Text = CInt(CType(arlPKProductionPlan(0), PKProductionPlan).PlanQty) + CInt(CType(arlPKProductionPlan(0), PKProductionPlan).CarryOverPreviousQty) - CInt(CType(arlPKProductionPlan(0), PKProductionPlan).UnselledStock) - CInt(CType(arlPKProductionPlan(0), PKProductionPlan).TotalReleaseAndAgree)
                    TotSisaStok += CType(e.Item.Cells(7).Text, Double)
                Else
                    e.Item.Cells(7).Text = "0"
                End If

            End If
            If Not (arlPKDetail.Count = 0) And e.Item.ItemType = ListItemType.Footer Then
                e.Item.Cells(4).Text = "SubTotal "
                e.Item.Cells(5).Text = FormatNumber(TotPesanan, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                e.Item.Cells(6).Text = FormatNumber(TotRilis, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                e.Item.Cells(7).Text = FormatNumber(TotSisaStok, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            End If
        End If
    End Sub

    Private Function CountTotalQty(ByVal pKDetail As PKDetail) As Integer
        Dim Total As Integer = 0
        For Each item As pKDetail In arlPKDetail
            If (item.VechileColor.ID = pKDetail.VechileColor.ID) Then
                If item.PKHeader.ProductionYear = pKDetail.PKHeader.ProductionYear Then
                    If item.PKHeader.RequestPeriodeMonth = pKDetail.PKHeader.RequestPeriodeMonth Then
                        If item.PKHeader.RequestPeriodeYear = pKDetail.PKHeader.RequestPeriodeYear Then
                            Total = Total + CInt(item.TargetQty)
                        End If
                    End If
                End If
            End If
        Next
        Return Total
    End Function

    Private Function CountTotalAlokasi(ByVal pKDetail As PKDetail) As Integer
        Dim Total As Integer = 0
        For Each item As pKDetail In arlPKDetail
            If (item.VechileColor.ID = pKDetail.VechileColor.ID) Then
                If item.PKHeader.ProductionYear = pKDetail.PKHeader.ProductionYear Then
                    If item.PKHeader.RequestPeriodeMonth = pKDetail.PKHeader.RequestPeriodeMonth Then
                        If item.PKHeader.RequestPeriodeYear = pKDetail.PKHeader.RequestPeriodeYear Then
                            Total = Total + CInt(item.ResponseQty)
                        End If
                    End If
                End If
            End If
        Next
        Return Total
    End Function

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        RetrievePKDetail()
    End Sub



    Private Sub RetrievePKDetail()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.PKStatus", MatchType.InSet, "(" & CType(enumStatusPK.Status.Rilis, Integer) & "," & CType(enumStatusPO.Status.Selesai, Integer) & "," & CType(enumStatusPO.Status.Setuju, Integer) & ")"))
        'Dim objVehicleColor As VechileColor = New VechileColorFacade(User).Retrieve("ZZZZ")
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "VechileColor.ColorCode", MatchType.No, "ZZZZ"))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.Category.ID", MatchType.Exact, ddlKategori.SelectedValue))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.ProductionYear", MatchType.Exact, ddlTahunPerakitan.SelectedValue))
        If ddlSubCategory.SelectedValue <> "-1" Then
            'CommonFunction.SetVehicleSubCategoryCriterias(ddlSubCategory, ddlKategori.SelectedItem.Text, criterias, "PKDetail")
            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlSubCategory.SelectedValue
            criterias.opAnd(New Criteria(GetType(PKDetail), "VechileColor.VechileType.VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))
        End If
        If ddlTipe.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "VechileColor.VechileType.ID", MatchType.Exact, ddlTipe.SelectedValue))
        End If
        If ddlTipeWarna.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "VechileColor.ID", MatchType.Exact, ddlTipeWarna.SelectedValue))
        End If
        Dim tgl As DateTime = System.Convert.ToDateTime(ddlPeriode.SelectedValue)
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, tgl.Month))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, tgl.Year))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.OrderType", MatchType.Exact, ddlJenisPesanan.SelectedValue))

        Dim sortColl As SortCollection = New SortCollection
        If (Not IsNothing("VechileColor.MaterialNumber")) Then
            sortColl.Add(New Sort(GetType(PKDetail), "VechileColor.MaterialNumber", Sort.SortDirection.ASC))
        Else
            sortColl = Nothing
        End If
        arlPKDetail = New PKDetailFacade(User).Retrieve(criterias, sortColl)
        arlGroupPKDetail = New ArrayList
        For Each item As PKDetail In arlPKDetail
            If (arlGroupPKDetail.Count <> 0) Then
                Dim IsAlreadyExist As Boolean = False
                For Each item1 As PKDetail In arlGroupPKDetail
                    If item1.VechileColor.ID = item.VechileColor.ID Then
                        If item1.PKHeader.ProductionYear = item.PKHeader.ProductionYear Then
                            If item1.PKHeader.RequestPeriodeMonth = item.PKHeader.RequestPeriodeMonth Then
                                If item1.PKHeader.RequestPeriodeYear = item.PKHeader.RequestPeriodeYear Then
                                    IsAlreadyExist = True
                                    Exit For
                                End If
                            End If
                        End If
                    End If
                Next
                If Not IsAlreadyExist Then
                    arlGroupPKDetail.Add(item)
                End If
            Else
                arlGroupPKDetail.Add(item)
            End If
        Next
        sessionHelper.SetSession("GroupPKDetail", arlGroupPKDetail)
        BindToGrid()
    End Sub

    Private Sub BindToGrid()
        dtgProduction.DataSource = arlGroupPKDetail
        If arlGroupPKDetail.Count > 0 Then
            dtgProduction.DataBind()
        Else
            dtgProduction.DataBind()
            MessageBox.Show("Data Tidak Ditemukan")
        End If

    End Sub
    Sub dtgProduction_Edit(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        arlPKDetail = sessionHelper.GetSession("GroupPKDetail")
        objPKDetail = arlPKDetail(e.Item.ItemIndex)
        'Todo session
        Session("AllocatedPKDetail") = objPKDetail
        sessionHelper.RemoveSession("GroupPKDetail")
        Response.Redirect("../PK/frmDisplayPKReleaseDetail.aspx?TotalPP=" & e.Item.Cells(7).Text & "&OrderType=" & ddlJenisPesanan.SelectedValue & "&Periode=" & ddlPeriode.SelectedValue & "&Category=" & ddlKategori.SelectedValue & "&Type=" & ddlTipe.SelectedValue & "&MaterialNumber=" & ddlTipeWarna.SelectedValue)
    End Sub

    Private Sub ddlJenisPesanan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlJenisPesanan.SelectedIndexChanged
        'If ddlJenisPesanan.SelectedIndex = 0 Then
        '    lblPeriodeAlokasiValue.Text = Format(DateTime.Now.AddMonths(1), "MMM yyyy")
        'Else
        '    lblPeriodeAlokasiValue.Text = Format(DateTime.Now, "MMM yyyy")
        'End If
    End Sub

    Private Sub ddlKategori_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlKategori.SelectedIndexChanged
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlKategori.SelectedItem.Text)
        'BindVehicleType(True)
        BindddlTipe(ddlKategori.SelectedItem.ToString)
        BindddlTipeWarna(ddlTipe.SelectedItem.ToString)
    End Sub


    Private Sub ddlTipe_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipe.SelectedIndexChanged
        BindddlTipeWarna(ddlTipe.SelectedItem.ToString)
    End Sub

    Private Sub ddlSubCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubCategory.SelectedIndexChanged
        BindddlTipe(ddlKategori.SelectedItem.ToString)
        BindddlTipeWarna(ddlTipe.SelectedItem.ToString)
    End Sub
End Class
