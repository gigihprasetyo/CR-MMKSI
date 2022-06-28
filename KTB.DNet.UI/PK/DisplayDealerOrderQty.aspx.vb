Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text


Public Class DisplayDealerOrderQty
    Inherits System.Web.UI.Page
    Protected WithEvents ddlTahunPerakitan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents ddlTipeWarna As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTipe As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTahunPerakitan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTipeWarna As System.Web.UI.WebControls.Label
    Protected WithEvents lblKategori As System.Web.UI.WebControls.Label
    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblJenisPesanan As System.Web.UI.WebControls.Label
    Protected WithEvents ddlJenisPesanan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPeriode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents ddlSubCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTipe As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnHitungAlokasi As System.Web.UI.WebControls.Button


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents dtgProduction As System.Web.UI.WebControls.DataGrid

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
    Private sessionHelper As New SessionHelper
    Private TotPesanan As Double = 0
    Private TotAlokasi As Double = 0
    Private TotPesanBelumAlokasi As Double = 0
    Private TotSisaStok As Double = 0
    Private isPassSPL As Boolean = False
    Private ArrListPKDetail As New ArrayList
    Private MessageSPL As String = String.Empty

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ActivateUserPrivilege()
        If Not IsPostBack Then
            BindddlJenisPesanan()
            BindddlTahunPerakitan()
            BindddlCategory()
            CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlKategori.SelectedItem.Text)
            BindddlPeriode()
            If ddlKategori.Items.Count <> 0 Then
                BindddlTipe(ddlKategori.SelectedItem.ToString)
                If ddlTipe.Items.Count <> 0 Then
                    BindddlTipeWarna(ddlTipe.SelectedItem.ToString)
                End If
            End If
            If (Request.QueryString("OrderType") <> String.Empty) Then
                'ViewState("OrderType") = Request.QueryString("OrderType")
                'ViewState("ProductionYear") = Request.QueryString("ProductionYear")
                'ViewState("Category") = Request.QueryString("Category")
                'ViewState("Type") = Request.QueryString("Type")
                'ViewState("MaterialNumber") = Request.QueryString("MaterialNumber")
                'ViewState("Periode") = Request.QueryString("Periode")
                If IsNothing(CType(sessionHelper.GetSession("FrmUploadAlokasi"), Hashtable)) Then
                    RefreshPKDetail()
                End If
            End If
            If ddlKategori.Items.Count = 0 OrElse ddlJenisPesanan.Items.Count = 0 Then
                btnCari.Enabled = False
            End If
            ReadCriteria()
            RetrievePKDetail()
        Else
            Dim hasilCari As ArrayList = New ArrayList
            If Not IsNothing(sessionHelper.GetSession("GroupPKDetail")) Then
                hasilCari = sessionHelper.GetSession("GroupPKDetail")
            Else
                RetrievePKDetail()
                hasilCari = sessionHelper.GetSession("GroupPKDetail")
            End If
            If hasilCari.Count > 0 Then
                btnHitungAlokasi.Visible = True
            Else
                btnHitungAlokasi.Visible = False
            End If
            'btnSimpan.Visible = False
        End If
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.AlokasiPKViewListPrivilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar Alokasi PK")
        End If
    End Sub

    Private Sub RefreshPKDetail()
        ddlJenisPesanan.SelectedValue = Request.QueryString("OrderType")
        ddlJenisPesanan_SelectedIndexChanged(Nothing, Nothing)
        ddlTahunPerakitan.SelectedValue = Request.QueryString("ProductionYear")
        ddlKategori.SelectedValue = Request.QueryString("Category")
        ddlKategori_SelectedIndexChanged(Nothing, Nothing)
        ddlTipe.SelectedValue = Request.QueryString("Type")
        ddlTipe_SelectedIndexChanged(Nothing, Nothing)
        ddlTipeWarna.SelectedValue = Request.QueryString("MaterialNumber")
        ddlPeriode.SelectedValue = Request.QueryString("Periode")
    End Sub

    Private Sub BindddlPeriode()
        Me.ddlPeriode.DataSource = LookUp.ArraylistMonth(True, 0, 1, DateTime.Now)
        Me.ddlPeriode.DataBind()
        Me.ddlPeriode.SelectedValue = Format(DateTime.Now, "MMM yyyy")
        'ddlPeriode.SelectedIndex = 0
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
        Try
            ddlJenisPesanan.SelectedValue = 1
        Catch
        End Try
        ddlJenisPesanan_SelectedIndexChanged(Nothing, Nothing)
        'lblPeriodeAlokasiValue.Text = Format(DateTime.Now.AddMonths(1), "MMM yyyy")
    End Sub
    Private Sub BindddlTahunPerakitan()
        Me.ddlTahunPerakitan.DataSource = ListTahunPerakitan() 'LookUp.ArraylistYear(True, 10, 1, DateTime.Now.Year.ToString)
        Me.ddlTahunPerakitan.DataBind()
        Me.ddlTahunPerakitan.SelectedValue = Date.Now.Year
    End Sub
    Private Function ListTahunPerakitan()
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColorIsActiveOnPK), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "Status", MatchType.Exact, 1))
        Dim arrList As ArrayList = New VechileColorIsActiveOnPKFacade(User).Retrieve(criteria)
        Dim list As List(Of VechileColorIsActiveOnPK) = arrList.Cast(Of VechileColorIsActiveOnPK)().ToList()
        Return From data In list Group data By keys = New With {Key data.ProductionYear} Into Group Select keys.ProductionYear Order By ProductionYear Descending
    End Function
    Private Sub BindddlCategory()

        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim arrayListCategory As ArrayList = New CategoryFacade(User).RetrieveActiveList(companyCode)

        For Each item As Category In arrayListCategory
            Dim listItem As New ListItem(item.CategoryCode, item.ID)
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

        Try
            ddlKategori.SelectedValue = 1  'Default PC
        Catch
        End Try

    End Sub
    Private Sub SaveCriteria()
        Dim crits As Hashtable = New Hashtable
        crits.Add("JenisPesanan", ddlJenisPesanan.SelectedValue)
        crits.Add("Periode", ddlPeriode.SelectedValue)
        crits.Add("TahunPerakitan", ddlTahunPerakitan.SelectedValue)
        crits.Add("Kategori", ddlKategori.SelectedValue)
        crits.Add("SubCategory", ddlSubCategory.SelectedValue)
        crits.Add("Tipe", ddlTipe.SelectedValue)
        crits.Add("TipeWarna", ddlTipeWarna.SelectedValue)
        sessionHelper.SetSession("FrmUploadAlokasi", crits)
    End Sub
    Private Sub ReadCriteria()
        Dim crits As Hashtable
        crits = CType(sessionHelper.GetSession("FrmUploadAlokasi"), Hashtable)
        If Not IsNothing(crits) Then
            Try
                ddlJenisPesanan.SelectedValue = crits.Item("JenisPesanan")
                ddlPeriode.SelectedValue = crits.Item("Periode")
                ddlTahunPerakitan.SelectedValue = crits.Item("TahunPerakitan")
                ddlKategori.SelectedValue = crits.Item("Kategori")
                ddlKategori_SelectedIndexChanged(Nothing, Nothing)
                ddlSubCategory.SelectedValue = crits.Item("SubCategory")
                ddlSubCategory_SelectedIndexChanged(Nothing, Nothing)
                ddlTipe.SelectedValue = crits.Item("Tipe")
                ddlTipe_SelectedIndexChanged(Nothing, Nothing)
                ddlTipeWarna.SelectedValue = crits.Item("TipeWarna")
            Catch
                ddlJenisPesanan.SelectedIndex = 0
                ddlPeriode.SelectedIndex = 0
                ddlTahunPerakitan.SelectedIndex = 0
                ddlKategori.SelectedIndex = 0
                ddlSubCategory.SelectedIndex = 0
                ddlTipe.SelectedIndex = 0
                ddlTipeWarna.SelectedIndex = 0
            End Try
        End If
    End Sub

    Private Sub BindddlTipe(ByVal Category As String)
        ddlTipe.Items.Clear()
        If ddlSubCategory.SelectedValue <> "-1" Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "Category.CategoryCode", MatchType.Exact, Category))
            'CommonFunction.SetVehicleSubCategoryCriterias(ddlSubCategory, ddlKategori.SelectedItem.Text, criterias, "VechileType")

            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlSubCategory.SelectedValue
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))

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

    Private Sub BindddlTipe(ByVal Category As String, ByVal assemblyyear As Integer)
        ddlTipe.Items.Clear()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim strSql As String = String.Format(
            String.Join(
            Environment.NewLine,
"SELECT VechileTypeID FROM VechileColorIsActiveOnPK WITH (NOLOCK) ",
"INNER JOIN VechileColor on VechileColorIsActiveOnPK.VehicleColorID=VechileColor.id",
"WHERE (VechileColorIsActiveOnPK.RowStatus = 0",
"AND VechileColorIsActiveOnPK.ProductionYear = {0}",
"AND VechileColorIsActiveOnPK.Status = 1",
"AND VechileColor.Status <> 'X')"
), assemblyyear)

        criterias.opAnd(New Criteria(GetType(VechileType), "ID", MatchType.InSet, "(" & strSql & ")"))
        criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.No, "X"))
        Dim VechileTypeColl As ArrayList = New VechileTypeFacade(User).Retrieve(criterias)

        If ddlSubCategory.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "Category.CategoryCode", MatchType.Exact, Category))


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
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "VechileType.VechileTypeCode", MatchType.Exact, KodeTipe))
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

    Private Sub BindddlTipeWarna(ByVal KodeTipe As String, ByVal assemblyyear As Integer)
        ddlTipeWarna.Items.Clear()
        If ddlTipe.SelectedIndex <> 0 Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim strSql As String = String.Format(
                String.Join(
                Environment.NewLine,
    "Select A.VehicleColorID",
    "FROM VechileColorIsActiveOnPK AS A with (nolock) ",
    "WHERE ",
    "(",
    "	A.RowStatus = 0",
    "	AND A.ProductionYear = {0}",
    "	AND A.Status = 1",
    ")"
    ), assemblyyear)

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

    Private ViewAlokasiPrivilege As Boolean
    Sub dtgProduction_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If Not (arlPKDetail Is Nothing) Then
            If Not (arlPKDetail.Count = 0 Or e.Item.ItemIndex = -1) Then
                If e.Item.ItemIndex = 0 Then
                    ViewAlokasiPrivilege = SecurityProvider.Authorize(Context.User, SR.AlokasiPKViewDetailPrivilege)
                End If
                objPKDetail = arlGroupPKDetail(e.Item.ItemIndex)
                e.Item.Cells(2).Text = (e.Item.ItemIndex + 1 + (dtgProduction.PageSize * dtgProduction.CurrentPageIndex)).ToString
                e.Item.Cells(3).Text = objPKDetail.VechileColor.MaterialDescription
                e.Item.Cells(4).Text = objPKDetail.VechileColor.MaterialNumber
                e.Item.Cells(5).Text = objPKDetail.PKHeader.ProductionYear
                e.Item.Cells(6).Text = CountTotalQty(objPKDetail).ToString
                TotPesanan += CType(e.Item.Cells(6).Text, Double)
                e.Item.Cells(7).Text = CountTotalAlokasi(objPKDetail).ToString
                TotAlokasi += CType(e.Item.Cells(7).Text, Double)
                e.Item.Cells(8).Text = CInt(e.Item.Cells(6).Text) - CInt(e.Item.Cells(7).Text)
                TotPesanBelumAlokasi += CType(e.Item.Cells(8).Text, Double)

                Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                lbtnEdit.Text = "<img src=""../images/edit.gif"" border=""0"" alt=""Edit"">"
                If Not ViewAlokasiPrivilege Then
                    lbtnEdit.Visible = False
                End If
                e.Item.Cells(13).Text = IIf(CInt(e.Item.Cells(7).Text) > 0, "*", "")
                e.Item.BackColor = IIf(CInt(e.Item.Cells(7).Text) > 0, System.Drawing.ColorTranslator.FromHtml("#00ffff"), Color.Empty)
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKProductionPlan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(PKProductionPlan), "VechileColor.MaterialNumber", MatchType.Exact, e.Item.Cells(4).Text))
                Dim tgl As DateTime = System.Convert.ToDateTime(ddlPeriode.SelectedValue)
                criterias.opAnd(New Criteria(GetType(PKProductionPlan), "PeriodMonth", MatchType.Exact, tgl.Month))
                criterias.opAnd(New Criteria(GetType(PKProductionPlan), "PeriodYear", MatchType.Exact, tgl.Year))
                criterias.opAnd(New Criteria(GetType(PKProductionPlan), "ProductionYear", MatchType.Exact, e.Item.Cells(5).Text))
                'Dim arlPKProductionPlan As ArrayList = New PKProductionPlanFacade(User).Retrieve(criterias)
                Dim arlPKProductionPlan As ArrayList = New PKProductionPlanFacade(User).RetrieveRencanaProduksi_DisplayDealerOrder(tgl.Month, tgl.Year, e.Item.Cells(4).Text, CType(e.Item.Cells(5).Text, Integer))
                If arlPKProductionPlan.Count <> 0 Then
                    'e.Item.Cells(10).Text = CInt(CType(arlPKProductionPlan(0), PKProductionPlan).PlanQty) + CInt(CType(arlPKProductionPlan(0), PKProductionPlan).CarryOverPreviousQty) - CInt(CType(arlPKProductionPlan(0), PKProductionPlan).UnselledStock) - CInt(CType(arlPKProductionPlan(0), PKProductionPlan).TotalReleaseAndAgree)

                    e.Item.Cells(10).Text = CInt(CType(arlPKProductionPlan(0), PKProductionPlan).PlanQty) - CInt(CType(arlPKProductionPlan(0), PKProductionPlan).TotalSelesai) - CInt(CType(arlPKProductionPlan(0), PKProductionPlan).TotalReleaseAndAgree)
                    TotSisaStok += e.Item.Cells(10).Text
                Else
                    e.Item.Cells(10).Text = "0"
                End If
                e.Item.Cells(9).Text = (CType(e.Item.Cells(10).Text, Integer) - CType(e.Item.Cells(7).Text, Integer)).ToString 'Sisa Stok Setelah Alokasi (unit)
                e.Item.Cells(11).Text = "0"
                'Dim isHitungAlokasi As Boolean = sessionHelper.GetSession("sessionHitungALokasi")
                'If isHitungAlokasi Then
                '    e.Item.Cells(10).Text = "9"
                'Else
                '    e.Item.Cells(10).Text = "0"
                'End If

            End If
            If Not (arlPKDetail.Count = 0) And e.Item.ItemType = ListItemType.Footer Then
                e.Item.Cells(5).Text = "SubTotal :"
                e.Item.Cells(6).Text = FormatNumber(TotPesanan, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                e.Item.Cells(7).Text = FormatNumber(TotAlokasi, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                e.Item.Cells(8).Text = FormatNumber(TotPesanBelumAlokasi, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                e.Item.Cells(10).Text = FormatNumber(TotSisaStok, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
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
        If arlGroupPKDetail.Count <= 0 Then
            MessageBox.Show("Data Tidak Ditemukan")
            btnHitungAlokasi.Visible = False
        End If
    End Sub

    Private Sub RetrievePKDetail()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.PKStatus", MatchType.InSet, String.Format("('{0}','{1}')", CType(enumStatusPK.Status.Konfirmasi, Short), CType(enumStatusPK.Status.Tunggu_Diskon, Short))))
        'Dim objVehicleColor As VechileColor = New VechileColorFacade(User).Retrieve("ZZZZ")
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "VechileColor.ColorCode", MatchType.No, "ZZZZ"))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.Category.ID", MatchType.Exact, ddlKategori.SelectedValue))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.ProductionYear", MatchType.Exact, ddlTahunPerakitan.SelectedValue))
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
        Dim tgl As DateTime = CType(ddlPeriode.SelectedValue, DateTime)
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
        dtgProduction.DataBind()
        If arlGroupPKDetail.Count > 0 Then
            btnHitungAlokasi.Visible = True
        End If
    End Sub
    Sub dtgProduction_Edit(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        arlPKDetail = sessionHelper.GetSession("GroupPKDetail")
        objPKDetail = arlPKDetail(e.Item.ItemIndex)
        sessionHelper.SetSession("AllocatedPKDetail", objPKDetail)
        sessionHelper.RemoveSession("GroupPKDetail")
        SaveCriteria()
        Response.Redirect("../PK/EntryAllocationQty.aspx?TotalPP=" & e.Item.Cells(10).Text & "&OrderType=" & ddlJenisPesanan.SelectedValue & "&ProductionYear=" & ddlTahunPerakitan.SelectedValue & "&Category=" & ddlKategori.SelectedValue & "&Type=" & ddlTipe.SelectedValue & "&MaterialNumber=" & ddlTipeWarna.SelectedValue & "&Periode=" & ddlPeriode.SelectedValue & "&SubCategoryVehicle=" & ddlSubCategory.SelectedValue)
    End Sub

    Private Sub ddlJenisPesanan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlJenisPesanan.SelectedIndexChanged
        If ddlJenisPesanan.SelectedIndex = 0 Then
            ddlPeriode.SelectedIndex = 0
        Else
            ddlPeriode.SelectedIndex = 1
        End If
    End Sub

    Private Sub ddlKategori_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlKategori.SelectedIndexChanged
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlKategori.SelectedItem.Text)
        BindddlTipe(ddlKategori.SelectedItem.ToString)
        BindddlTipeWarna(ddlTipe.SelectedItem.ToString)
    End Sub


    Private Sub ddlTipe_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipe.SelectedIndexChanged
        BindddlTipeWarna(ddlTipe.SelectedItem.ToString, CInt(ddlTahunPerakitan.SelectedValue))
    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        SaveCriteria()
        Response.Redirect("../PK/frmUploadAlokasi.aspx")
    End Sub

    Private Sub ddlSubCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubCategory.SelectedIndexChanged
        BindddlTipe(ddlKategori.SelectedItem.Text, CInt(ddlTahunPerakitan.SelectedValue))
    End Sub

    Protected Sub btnHitungAlokasi_Click(sender As Object, e As EventArgs) Handles btnHitungAlokasi.Click
        Dim dgItem As DataGridItem
        Dim cbx As System.Web.UI.WebControls.CheckBox
        Dim selectedCount As Integer = 0
        For Each dgItem In dtgProduction.Items
            cbx = dgItem.FindControl("ChkExport")
            If cbx.Checked Then
                selectedCount += 1
                Dim A As Double = CType(dgItem.Cells(8).Text, Double)
                Dim B As Double = CType(dgItem.Cells(10).Text, Double)
                Dim C As Double = If(B >= A, If(A = 0, 0, A), 0)
                dgItem.Cells(11).Text = C.ToString
            End If
        Next
        If selectedCount > 0 Then
            btnSimpan.Visible = True
        Else
            btnSimpan.Visible = False
        End If
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim arrList As ArrayList = New ArrayList
        Dim arrPKDetailList As ArrayList = New ArrayList
        Dim sb As StringBuilder = New StringBuilder
        Dim GroupPKDetail As ArrayList = sessionHelper.GetSession("GroupPKDetail")
        Dim dgItem As DataGridItem
        Dim cbx As System.Web.UI.WebControls.CheckBox
        Dim tempList As List(Of PKDetail) = New List(Of PKDetail)(From data As PKDetail In GroupPKDetail Select data)
        Dim _OrderType As Integer = 0, _ProductionYear As Integer = 0, _RequestPeriodMonth As Integer = 0, _RequestPeriodYear As Integer = 0

        Dim selectedCount As Integer = 0
        For Each dgItem In dtgProduction.Items
            cbx = dgItem.FindControl("ChkExport")
            If cbx.Checked Then
                selectedCount += 1
            End If
        Next
        If selectedCount <= 0 Then
            MessageBox.Show("Mohon ceklist salah satu item terlebih dahulu")
            Exit Sub
        End If

        Dim sbFailedToSave1 As StringBuilder = New StringBuilder()
        Dim sbFailedToSave2 As StringBuilder = New StringBuilder()
        Dim sbFailedToSave3 As StringBuilder = New StringBuilder()
        Dim isAlokasiAll0 As Boolean = True
        For Each dgItem In dtgProduction.Items
            cbx = dgItem.FindControl("ChkExport")
            If cbx.Checked Then
                Dim _NilaiAlokasi As String = dgItem.Cells(10).Text
                If Not IsNothing(_NilaiAlokasi) Then
                    If (Not _NilaiAlokasi = "0") Or (Not _NilaiAlokasi = "") Then
                        If (CType(_NilaiAlokasi, Integer) > 0) Then
                            isAlokasiAll0 = False
                            Dim _ID As Integer = CType(dgItem.Cells(1).Text, Integer)
                            objPKDetail = tempList.Where(Function(i) i.ID = _ID).FirstOrDefault

                            Dim criterias As New CriteriaComposite(New Criteria(GetType(PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.PKStatus", MatchType.InSet, String.Format("('{0}','{1}')", CType(enumStatusPK.Status.Konfirmasi, Short), CType(enumStatusPK.Status.Tunggu_Diskon, Short))))
                            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.OrderType", MatchType.Exact, objPKDetail.PKHeader.OrderType))
                            criterias.opAnd(New Criteria(GetType(PKDetail), "VechileColor.ID", MatchType.Exact, objPKDetail.VechileColor.ID))
                            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.ProductionYear", MatchType.Exact, objPKDetail.PKHeader.ProductionYear))
                            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, objPKDetail.PKHeader.RequestPeriodeMonth))
                            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, objPKDetail.PKHeader.RequestPeriodeYear))
                            ArrListPKDetail = New PKDetailFacade(User).Retrieve(criterias)

                            MessageSPL = String.Empty
                            Dim failed As Boolean = False
                            Dim listPKDetail As ArrayList = GetPKSPL(ArrListPKDetail)
                            If listPKDetail.Count > 0 Then
                                If Not CekPKSPL(listPKDetail) Then
                                    'MessageBox.Show("Total Alokasi Melebihi Aplikasi " & MessageSPL)
                                    sbFailedToSave1.Append(MessageSPL + " \n")
                                    failed = True
                                    Continue For
                                End If
                            Else
                                'MessageBox.Show("Item alokasi tidak ada yang valid.")
                                sbFailedToSave2.Append("Item alokasi tidak ada yang valid." + " \n")
                                Continue For
                            End If
                            If Not failed Then
                                Dim dblTotalProduksi As Double = 0
                                Dim dblSisaProduksi As Double = 0
                                dblTotalProduksi = GetSisaStock(objPKDetail)
                                dblSisaProduksi = (dblTotalProduksi - HitungSisaProduksi()).ToString
                                If dblSisaProduksi < 0 Then
                                    'MessageBox.Show("Total Alokasi Melebihi Sisa Stock")
                                    Dim msg As String = String.Format("{0}{1}, Total Alokasi Melebihi Sisa Stock.", objPKDetail.VehicleTypeCode, objPKDetail.VehicleColorCode)
                                    sbFailedToSave3.Append(msg + " \n")
                                    Continue For
                                End If
                            End If

                            _OrderType = objPKDetail.PKHeader.OrderType
                            _ProductionYear = objPKDetail.PKHeader.ProductionYear
                            _RequestPeriodMonth = objPKDetail.PKHeader.RequestPeriodeMonth
                            _RequestPeriodYear = objPKDetail.PKHeader.RequestPeriodeYear
                            arrList.Add(objPKDetail.VechileColor.ID)
                            sb.Append(objPKDetail.VechileColor.ID.ToString + ";")
                            arrPKDetailList.Add(ArrListPKDetail)
                        End If
                    End If
                End If
            End If
        Next
        If Not sbFailedToSave1.Length = 0 Then
            Dim msg As String = String.Format("Model Tipe : \n{0}", sbFailedToSave1.ToString())
            MessageBox.Show(msg)
            Exit Sub
        End If
        If Not sbFailedToSave2.Length = 0 Then
            MessageBox.Show(sbFailedToSave2.ToString())
            Exit Sub
        End If
        If Not sbFailedToSave3.Length = 0 Then
            Dim msg As String = String.Format("Model Tipe : \n{0}", sbFailedToSave3.ToString())
            MessageBox.Show(msg)
            Exit Sub
        End If
        If sb.ToString.Length > 0 Then
            sb.Remove(sb.Length - 1, 1) 'Remove last char ";"
            If UpdateToDB(sb.ToString, _OrderType, _ProductionYear, _RequestPeriodMonth, _RequestPeriodYear) Then
                Dim spFac As sp_GetPKAllocationFacade = New sp_GetPKAllocationFacade(User)
                For Each arr As ArrayList In arrPKDetailList
                    For Each oPKDetail As PKDetail In arr
                        spFac.UpdateFreeDays(oPKDetail.VechileColor.ID, oPKDetail.PKHeader.RequestPeriodeMonth, _
                                             oPKDetail.PKHeader.RequestPeriodeYear, oPKDetail.PKHeader.Dealer.ID, _
                                             oPKDetail.VechileType.VechileModel.ID)
                    Next
                Next
                RetrievePKDetail()
                MessageBox.Show("Simpan Sukses")
            Else
                MessageBox.Show("Simpan Gagal")
            End If
        Else
            If isAlokasiAll0 Then
                MessageBox.Show("Tidak ada data yang disimpan, semua alokasi 0")
            Else
                MessageBox.Show("Simpan Sukses")
            End If
        End If
        
    End Sub

    Private Function GetPKSPL(ByRef arr As ArrayList) As ArrayList
        Dim list As ArrayList = New ArrayList
        For Each Item As PKDetail In arr
            Dim opkDetail As PKDetail = New PKDetailFacade(User).Retrieve(Item.ID)
            'If (opkDetail.PKHeader.PKType = LookUp.enumPurpose.Khusus) And (opkDetail.PKHeader.SPLNumber <> "") Then
            opkDetail.ResponseQty = opkDetail.TargetQty
            list.Add(opkDetail)
            'End If
        Next
        Return list
    End Function

    Private Function HitungSisaProduksi() As Integer
        Dim int As Integer = 0

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.PKStatus", MatchType.InSet, String.Format("('{0}','{1}')", CType(enumStatusPK.Status.Konfirmasi, Short), CType(enumStatusPK.Status.Tunggu_Diskon, Short))))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.OrderType", MatchType.Exact, objPKdetail.PKHeader.OrderType))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "VechileColor.ID", MatchType.Exact, objPKdetail.VechileColor.ID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.ProductionYear", MatchType.Exact, objPKdetail.PKHeader.ProductionYear))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, objPKdetail.PKHeader.RequestPeriodeMonth))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, objPKdetail.PKHeader.RequestPeriodeYear))
        Dim ArrList As ArrayList = New PKDetailFacade(User).Retrieve(criterias)
        For Each item As PKDetail In ArrList
            item.ResponseQty = item.TargetQty
            int += item.ResponseQty
        Next
        Return int
    End Function

    Private Function GetSisaStock(ByVal pKDetail As PKDetail) As Integer
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKProductionPlan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PKProductionPlan), "VechileColor.MaterialNumber", MatchType.Exact, pKDetail.VechileColor.MaterialNumber))
        criterias.opAnd(New Criteria(GetType(PKProductionPlan), "PeriodMonth", MatchType.Exact, pKDetail.PKHeader.RequestPeriodeMonth))
        criterias.opAnd(New Criteria(GetType(PKProductionPlan), "PeriodYear", MatchType.Exact, pKDetail.PKHeader.RequestPeriodeYear))
        criterias.opAnd(New Criteria(GetType(PKProductionPlan), "ProductionYear", MatchType.Exact, pKDetail.PKHeader.ProductionYear))
        'Dim arlPKProductionPlan As ArrayList = New PKProductionPlanFacade(User).Retrieve(criterias)
        Dim arlPKProductionPlan As ArrayList = New PKProductionPlanFacade(User).RetrieveRencanaProduksi_DisplayDealerOrder(pKDetail.PKHeader.RequestPeriodeMonth, pKDetail.PKHeader.RequestPeriodeYear, pKDetail.VechileColor.MaterialNumber, pKDetail.PKHeader.ProductionYear)
        If arlPKProductionPlan.Count <> 0 Then
            '    Return CInt(CType(arlPKProductionPlan(0), PKProductionPlan).PlanQty) + CInt(CType(arlPKProductionPlan(0), PKProductionPlan).CarryOverPreviousQty) - CInt(CType(arlPKProductionPlan(0), PKProductionPlan).UnselledStock) - CInt(CType(arlPKProductionPlan(0), PKProductionPlan).TotalReleaseAndAgree)

            Return CInt(CType(arlPKProductionPlan(0), PKProductionPlan).PlanQty) - CInt(CType(arlPKProductionPlan(0), PKProductionPlan).TotalSelesai) - CInt(CType(arlPKProductionPlan(0), PKProductionPlan).TotalReleaseAndAgree)
        Else
            Return 0
        End If
    End Function

    Private Function CekPKSPL(ByVal listPKDetail As ArrayList) As Boolean
        Dim list As ArrayList = New ArrayList
        Dim listDetail As ArrayList = New ArrayList
        Dim obj As PKSPL
        Dim match As Boolean = False
        isPassSPL = True
        For Each item As PKDetail In listPKDetail
            If (item.PKHeader.PKType = LookUp.enumPurpose.Khusus) And (item.PKHeader.SPLNumber <> "") Then
                match = False
                If list.Count = 0 Then
                    listDetail = New ArrayList
                    listDetail.Add(item)
                    'obj = New PKSPL(item.PKHeader.SPLNumber, listDetail)
                    obj = New PKSPL(item.PKHeader.SPLNumber, listDetail, String.Format("{0}{1}", item.VehicleTypeCode, item.VehicleColorCode))
                    list.Add(obj)
                Else
                    For i As Integer = 0 To list.Count - 1
                        Dim objPKSPL As PKSPL = CType(list(i), PKSPL)
                        If objPKSPL.Code = item.PKHeader.SPLNumber Then
                            objPKSPL.Val.Add(item)
                            match = True
                            Exit For
                        End If
                    Next
                    If Not match Then
                        listDetail = New ArrayList
                        listDetail.Add(item)
                        'obj = New PKSPL(item.PKHeader.SPLNumber, listDetail)
                        obj = New PKSPL(item.PKHeader.SPLNumber, listDetail, String.Format("{0}{1}", item.VehicleTypeCode, item.VehicleColorCode))
                        list.Add(obj)
                    End If
                End If
            End If
        Next
        'isPassSPL = True
        If list.Count > 0 Then
            For Each item As PKSPL In list
                Dim listProcess As ArrayList = item.Val
                Dim VechType As String
                Dim pMonth As Integer
                Dim pYear As Integer
                For Each items As PKDetail In listProcess
                    VechType = items.VehicleTypeCode
                    pMonth = items.PKHeader.RequestPeriodeMonth
                    pYear = items.PKHeader.RequestPeriodeYear
                Next
                Dim objSPL As SPL = New SPLFacade(User).Retrieve(item.Code)
                Dim SPLQty As Integer = GetSPLQuantity(objSPL, VechType, pMonth, pYear)
                Dim splCurrentUsed As Integer = HitungSisaSPL(item.Code)
                If (SPLQty - splCurrentUsed) < 0 Then
                    isPassSPL = False
                    MessageSPL = String.Format("{0} Melebihi Total Aplikasi {1}", item.ModelType, item.Code)
                    Return False
                End If
            Next
        End If
        Return True
    End Function

    Private Function GetSPLQuantity(ByVal objSPL As SPL, ByVal typeCode As String, ByVal periodMonth As Integer, ByVal periodYear As Integer) As Integer
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SPLDetail), "SPL.SPLNumber", MatchType.Exact, objSPL.SPLNumber))
        criterias.opAnd(New Criteria(GetType(SPLDetail), "PeriodMonth", MatchType.Exact, periodMonth))
        criterias.opAnd(New Criteria(GetType(SPLDetail), "PeriodYear", MatchType.Exact, periodYear))
        criterias.opAnd(New Criteria(GetType(SPLDetail), "VechileType.ID", MatchType.Exact, GetVehicleType(typeCode).ID))
        Dim objSPLDetailFacade As SPLDetailFacade = New SPLDetailFacade(User)
        Dim objSPLList As ArrayList = objSPLDetailFacade.Retrieve(criterias)
        Dim objSPLDetail As SPLDetail
        If objSPLList.Count > 0 Then
            objSPLDetail = CType(objSPLList(0), SPLDetail)
            Return objSPLDetail.Quantity
        End If
        Return 0
    End Function

    Private Function GetVehicleType(ByVal code As String) As VechileType
        Dim objVecTypeFacade As VechileTypeFacade = New VechileTypeFacade(User)
        Return objVecTypeFacade.Retrieve(code)
    End Function

    Private Function HitungSisaSPL(ByVal splNumber As String) As Integer
        Dim int As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim filter As String = "(" & CType(enumStatusPK.Status.Konfirmasi, Integer) & "," & CType(enumStatusPK.Status.Rilis, Integer) & "," & CType(enumStatusPK.Status.Setuju, Integer) & "," & CType(enumStatusPK.Status.Selesai, Integer) & ")"
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.PKStatus", MatchType.InSet, filter))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "VechileColor.VechileType.ID", MatchType.Exact, objPKDetail.VechileColor.VechileType.ID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, objPKDetail.PKHeader.RequestPeriodeMonth))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, objPKDetail.PKHeader.RequestPeriodeYear))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.SPLNumber", MatchType.Exact, splNumber))
        Dim ArrList As ArrayList = New PKDetailFacade(User).Retrieve(criterias)
        For Each item As PKDetail In ArrList
            item.ResponseQty = item.TargetQty
            int += item.ResponseQty
        Next
        Return int
    End Function


    Private Function UpdateToDB(dataVechileColor As String, _OrderType As Integer, _ProductionYear As Integer, _RequestPeriodMonth As Integer, _RequestPeriodYear As Integer) As Boolean
        Dim result As Boolean = False
        Try
            result = New PKDetailFacade(User).UpdateResponseQtyAsAllocationQty(dataVechileColor, _OrderType, _ProductionYear, _RequestPeriodMonth, _RequestPeriodYear)
        Catch ex As Exception
        End Try
        Return result
    End Function

End Class