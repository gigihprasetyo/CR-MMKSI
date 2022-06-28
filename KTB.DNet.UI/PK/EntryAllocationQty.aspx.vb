#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade
#End Region

Public Class EntryAllocationQty
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblKategori As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents dtgEntryAllocation As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblMaterialNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblTipe As System.Web.UI.WebControls.Label
    Protected WithEvents lblModelTipeWarna As System.Web.UI.WebControls.Label
    Protected WithEvents lblTahunPerakitan As System.Web.UI.WebControls.Label
    Protected WithEvents lblKategoriValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTipeValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblMaterialNumberValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblMaterialDescriptionValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTahunPerakitanValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalProduksi As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalProduksiValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblSisaProduksi As System.Web.UI.WebControls.Label
    Protected WithEvents lblSisaProduksiValue As System.Web.UI.WebControls.Label
    Protected WithEvents btnHitung As System.Web.UI.WebControls.Button
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents lblPeriodeAlokasi As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriodeAlokasiValue As System.Web.UI.WebControls.Label
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private ArrListPKDetail As ArrayList
    Private objPKdetail As PKDetail
    Private sessionHelper As New SessionHelper
    Private MessageSPL As String
    Private isPassSPL As Boolean
    Private arrPengajuan As New ArrayList
    Private objDealerSalesTarget As DealerSalesTarget
#End Region

#Region "Custom Method"

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "PKHeader.PKNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
    End Sub

    Private Sub BindDataToGrid()
        objPKdetail = sessionHelper.GetSession("AllocatedPKDetail")
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.PKStatus", MatchType.InSet, String.Format("('{0}','{1}')", CType(enumStatusPK.Status.Konfirmasi, Short), CType(enumStatusPK.Status.Tunggu_Diskon, Short))))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.OrderType", MatchType.Exact, objPKdetail.PKHeader.OrderType))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "VechileColor.ID", MatchType.Exact, objPKdetail.VechileColor.ID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.ProductionYear", MatchType.Exact, objPKdetail.PKHeader.ProductionYear))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, objPKdetail.PKHeader.RequestPeriodeMonth))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, objPKdetail.PKHeader.RequestPeriodeYear))
        ArrListPKDetail = New PKDetailFacade(User).Retrieve(criterias, CType(ViewState("CurrentSortColumn"), String), _
                CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        'ArrListPKDetail = New PKDetailFacade(User).Retrieve(criterias)
        dtgEntryAllocation.DataSource = ArrListPKDetail
        sessionHelper.SetSession("EntryPKDetail", ArrListPKDetail)
        dtgEntryAllocation.DataBind()
        lblSisaProduksiValue.Text = (CInt(lblTotalProduksiValue.Text) - HitungSisaProduksi()).ToString
    End Sub

    Private Function HitungSisaProduksi() As Integer
        objPKdetail = sessionHelper.GetSession("AllocatedPKDetail")
        Dim int As Integer = 0
        'For i As Integer = 0 To ArrListPKDetail.Count - 1
        '    Dim txtBox As TextBox = dtgEntryAllocation.Items.Item(i).FindControl("TextBox1")
        '    int = int + CInt(txtBox.Text)
        'Next
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.PKStatus", MatchType.InSet, String.Format("('{0}','{1}')", CType(enumStatusPK.Status.Konfirmasi, Short), CType(enumStatusPK.Status.Tunggu_Diskon, Short))))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.OrderType", MatchType.Exact, objPKdetail.PKHeader.OrderType))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "VechileColor.ID", MatchType.Exact, objPKdetail.VechileColor.ID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.ProductionYear", MatchType.Exact, objPKdetail.PKHeader.ProductionYear))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, objPKdetail.PKHeader.RequestPeriodeMonth))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, objPKdetail.PKHeader.RequestPeriodeYear))
        Dim ArrList As ArrayList = New PKDetailFacade(User).Retrieve(criterias)
        For Each item As PKDetail In ArrList
            Dim isBeingAllocatedPK As Boolean = False
            For Each dtgItem As DataGridItem In dtgEntryAllocation.Items
                If CInt(dtgItem.Cells(0).Text) = item.ID Then
                    isBeingAllocatedPK = True
                    Dim txtBox As TextBox = dtgItem.FindControl("TextBox1")
                    int = int + CInt(txtBox.Text)
                    Exit For
                End If
            Next
            If Not isBeingAllocatedPK Then
                int += item.ResponseQty
            End If
        Next
        Return int
    End Function

    Private Function GetLatestValidFrom(ByVal objPKDetail1 As PKDetail) As DateTime
        Dim curDate As DateTime = New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0)
        Dim criteriaGetLatestValidFrom As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSalesTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaGetLatestValidFrom.opAnd(New Criteria(GetType(DealerSalesTarget), "ValidFrom", MatchType.LesserOrEqual, curDate))
        criteriaGetLatestValidFrom.opAnd(New Criteria(GetType(DealerSalesTarget), "Dealer.ID", MatchType.Exact, objPKDetail1.PKHeader.Dealer.ID))
        criteriaGetLatestValidFrom.opAnd(New Criteria(GetType(DealerSalesTarget), "VehicleModel.ID", MatchType.Exact, objPKDetail1.VechileType.VechileModel.ID))

        Dim sortCollValidFrom As SortCollection = New SortCollection
        sortCollValidFrom.Add(New Sort(GetType(DealerSalesTarget), "ValidFrom", Sort.SortDirection.DESC))

        Dim LatestValidfrom As DateTime = New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0)
        Dim arrGetLatestValidFrom As ArrayList = New DealerSalesTargetFacade(User).Retrieve(criteriaGetLatestValidFrom, sortCollValidFrom)
        If arrGetLatestValidFrom.Count > 0 Then
            Dim objLatestValidFrom As DealerSalesTarget = arrGetLatestValidFrom(0)
            LatestValidfrom = objLatestValidFrom.ValidFrom
        End If
        Return LatestValidfrom
    End Function

    Private Function GetFreeDays(ByVal TotalQty As Integer, ByVal objPKDetail1 As PKDetail) As Integer
        Dim FreeDays As Integer = 0
        Dim curDate As DateTime = New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0)
        Dim LatestValidfrom As DateTime = GetLatestValidFrom(objPKDetail1)

        Dim criteriaDealerSalesTarget As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSalesTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaDealerSalesTarget.opAnd(New Criteria(GetType(DealerSalesTarget), "ValidFrom", MatchType.LesserOrEqual, curDate))
        criteriaDealerSalesTarget.opAnd(New Criteria(GetType(DealerSalesTarget), "ValidFrom", MatchType.GreaterOrEqual, LatestValidfrom))
        criteriaDealerSalesTarget.opAnd(New Criteria(GetType(DealerSalesTarget), "MaxQuantity", MatchType.GreaterOrEqual, TotalQty))
        criteriaDealerSalesTarget.opAnd(New Criteria(GetType(DealerSalesTarget), "Dealer.ID", MatchType.Exact, objPKDetail1.PKHeader.Dealer.ID))
        criteriaDealerSalesTarget.opAnd(New Criteria(GetType(DealerSalesTarget), "VehicleModel.ID", MatchType.Exact, objPKDetail1.VechileType.VechileModel.ID))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(DealerSalesTarget), "Sequence", Sort.SortDirection.ASC))

        objDealerSalesTarget = New DealerSalesTarget
        Dim arrDealerSalesTarget As ArrayList = New DealerSalesTargetFacade(User).Retrieve(criteriaDealerSalesTarget, sortColl)
        If arrDealerSalesTarget.Count > 0 Then
            objDealerSalesTarget = arrDealerSalesTarget(0)
            FreeDays = objDealerSalesTarget.FreeDays
        End If
        Return FreeDays
    End Function

    Private Function GetSisaProgram(ByVal objPKDetail1 As PKDetail, ByRef isExistDealerSalesTarget As Boolean) As Integer
        Dim curDate As DateTime = New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0)
        Dim ListFreeDays As String = GetListFreeDays(objPKDetail1)
        Dim TotalQty As Integer = GetTotalAllocation(objPKDetail1, ListFreeDays)

        Dim LatestValidfrom As DateTime = GetLatestValidFrom(objPKDetail1)

        Dim arrDealerSalesTarget As ArrayList = GetDealerSalesTarget(objPKDetail1)

        If arrDealerSalesTarget.Count > 0 Then
            isExistDealerSalesTarget = True
        End If

        Dim SelisihQty As Integer = 0
        For Each DSTitem As DealerSalesTarget In arrDealerSalesTarget
            SelisihQty = DSTitem.MaxQuantity - TotalQty
            If SelisihQty > 0 Then
                Exit For
            End If
        Next

        Return SelisihQty
    End Function

    Private Function GetTotalAllocation(ByVal objPKDetail1 As PKDetail, ByVal FreeDays As String) As Integer
        Dim TotalQty As Integer = 0
        Dim SubCategoryVehicle As Integer = CType(ViewState("SubCategoryVehicle"), Integer)
        Dim objGetAllocationPK As sp_GetPKAllocation = New sp_GetPKAllocation
        Dim arrGetAllocationPK As ArrayList = _
            New sp_GetPKAllocationFacade(User).RetrieveFromSP(objPKDetail1.PKHeader.Dealer.DealerCode, _
                                                              objPKDetail1.PKHeader.RequestPeriodeMonth, _
                                                              objPKDetail1.PKHeader.RequestPeriodeYear, _
                                                              SubCategoryVehicle, FreeDays)

        If arrGetAllocationPK.Count > 0 Then
            objGetAllocationPK = CType(arrGetAllocationPK(0), sp_GetPKAllocation)
            TotalQty = objGetAllocationPK.TotalQty
        End If
        Return TotalQty
    End Function

    Private Function GetDealerSalesTarget(ByVal objPKDetail1 As PKDetail) As ArrayList
        Dim curDate As DateTime = New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0)
        Dim LatestValidfrom As DateTime = GetLatestValidFrom(objPKDetail1)

        Dim criteriaDealerSalesTarget As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSalesTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaDealerSalesTarget.opAnd(New Criteria(GetType(DealerSalesTarget), "ValidFrom", MatchType.LesserOrEqual, curDate))
        criteriaDealerSalesTarget.opAnd(New Criteria(GetType(DealerSalesTarget), "ValidFrom", MatchType.GreaterOrEqual, LatestValidfrom))
        criteriaDealerSalesTarget.opAnd(New Criteria(GetType(DealerSalesTarget), "Dealer.ID", MatchType.Exact, objPKDetail1.PKHeader.Dealer.ID))
        criteriaDealerSalesTarget.opAnd(New Criteria(GetType(DealerSalesTarget), "VehicleModel.ID", MatchType.Exact, objPKDetail1.VechileType.VechileModel.ID))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(DealerSalesTarget), "Sequence", Sort.SortDirection.ASC))

        Dim arrDealerSalesTarget As ArrayList = New DealerSalesTargetFacade(User).Retrieve(criteriaDealerSalesTarget, sortColl)

        Return arrDealerSalesTarget
    End Function

    Private Function GetListFreeDays(ByVal objPKDetail1 As PKDetail) As String
        Dim SubCategoryVehicle As Integer = CType(ViewState("SubCategoryVehicle"), Integer)
        Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & SubCategoryVehicle

        Dim criteriaListFreeDays As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaListFreeDays.opAnd(New Criteria(GetType(PKDetail), "FreeDays", MatchType.IsNotNull, Nothing))
        criteriaListFreeDays.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, objPKDetail1.PKHeader.RequestPeriodeMonth))
        criteriaListFreeDays.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, objPKDetail1.PKHeader.RequestPeriodeYear))
        criteriaListFreeDays.opAnd(New Criteria(GetType(PKDetail), "PKHeader.Dealer.ID", MatchType.Exact, objPKDetail1.PKHeader.Dealer.ID))
        criteriaListFreeDays.opAnd(New Criteria(GetType(PKDetail), "VechileType.VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(PKDetail), "FreeDays", Sort.SortDirection.ASC))

        Dim arrListFreeDays As ArrayList = New PKDetailFacade(User).Retrieve(criteriaListFreeDays, sortColl)

        Dim ListFreeDays As String = String.Empty
        Dim arrFilteredFD As ArrayList = New ArrayList
        For Each itemFiltered As PKDetail In arrListFreeDays
            If arrFilteredFD.IndexOf(itemFiltered.FreeDays) < 0 Then
                If objPKDetail1.FreeDays > 0 Then
                    If itemFiltered.FreeDays <= objPKDetail1.FreeDays Then
                        arrFilteredFD.Add(itemFiltered.FreeDays)
                    End If
                Else
                    If arrFilteredFD.Count < 2 Then
                        arrFilteredFD.Add(itemFiltered.FreeDays)
                    End If
                End If
            End If
        Next

        For i As Integer = 0 To arrFilteredFD.Count - 1
            ListFreeDays += arrFilteredFD(i).ToString() + ","
        Next
        If ListFreeDays.Trim = "" Then
            ListFreeDays = "0"
        Else
            'ListFreeDays = "0, " + ListFreeDays
            ListFreeDays = ListFreeDays.Substring(0, ListFreeDays.Length - 1)
        End If


        Return ListFreeDays
    End Function

    Private Function CheckFreeDays(ByVal objPKDetail1 As PKDetail, ByVal SisaProgram As Integer) As ArrayList
        Dim SubCategoryVehicle As Integer = CType(ViewState("SubCategoryVehicle"), Integer)
        Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & SubCategoryVehicle

        Dim criteriaListFreeDays As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaListFreeDays.opAnd(New Criteria(GetType(PKDetail), "FreeDays", MatchType.Greater, objPKDetail1.FreeDays))
        criteriaListFreeDays.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, objPKDetail1.PKHeader.RequestPeriodeMonth))
        criteriaListFreeDays.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, objPKDetail1.PKHeader.RequestPeriodeYear))
        criteriaListFreeDays.opAnd(New Criteria(GetType(PKDetail), "PKHeader.Dealer.ID", MatchType.Exact, objPKDetail1.PKHeader.Dealer.ID))
        criteriaListFreeDays.opAnd(New Criteria(GetType(PKDetail), "VechileType.VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))

        Dim arrListFreeDays As ArrayList = New PKDetailFacade(User).Retrieve(criteriaListFreeDays)

        Return arrListFreeDays
    End Function
#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ActivateUserPrivilege()
        If Not IsPostBack Then
            sessionHelper.RemoveSession("PengajuanPK")
            ViewState("OrderType") = Request.QueryString("OrderType")
            ViewState("ProductionYear") = Request.QueryString("ProductionYear")
            ViewState("Category") = Request.QueryString("Category")
            ViewState("Type") = Request.QueryString("Type")
            ViewState("MaterialNumber") = Request.QueryString("MaterialNumber")
            ViewState("Periode") = Request.QueryString("Periode")

            objPKdetail = sessionHelper.GetSession("AllocatedPKDetail")
            lblKategoriValue.Text = objPKdetail.PKHeader.Category.CategoryCode
            lblMaterialDescriptionValue.Text = objPKdetail.VechileColor.MaterialDescription
            lblMaterialNumberValue.Text = objPKdetail.VechileColor.MaterialNumber
            lblPeriodeAlokasiValue.Text = CType(CInt(objPKdetail.PKHeader.RequestPeriodeMonth) - 1, enumMonth.Month).ToString & " " & objPKdetail.PKHeader.RequestPeriodeYear.ToString
            lblTahunPerakitanValue.Text = objPKdetail.PKHeader.ProductionYear
            lblTipeValue.Text = objPKdetail.VechileColor.VechileType.VechileTypeCode
            lblTotalProduksiValue.Text = CInt(Request.QueryString("TotalPP"))

            If CType(Request.QueryString("SubCategoryVehicle"), Integer) <= 0 Then
                Dim SCVcriterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                SCVcriterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "VechileModel.ID", MatchType.Exact, objPKdetail.VechileType.VechileModel.ID))
                Dim arSCV As ArrayList = New SubCategoryVehicleToModelFacade(User).Retrieve(SCVcriterias)
                If arSCV.Count > 0 Then
                    Dim objSCV As SubCategoryVehicleToModel = New SubCategoryVehicleToModel
                    objSCV = arSCV(0)
                    ViewState("SubCategoryVehicle") = objSCV.SubCategoryVehicle.ID
                End If
            Else
                ViewState("SubCategoryVehicle") = Request.QueryString("SubCategoryVehicle")
            End If

            InitiatePage()
            BindDataToGrid()
        End If
    End Sub

    Private Function GetSisaStock(ByVal pKDetail As PKDetail) As Integer
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKProductionPlan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PKProductionPlan), "VechileColor.MaterialNumber", MatchType.Exact, pKDetail.VechileColor.MaterialNumber))
        criterias.opAnd(New Criteria(GetType(PKProductionPlan), "PeriodMonth", MatchType.Exact, pKDetail.PKHeader.RequestPeriodeMonth))
        criterias.opAnd(New Criteria(GetType(PKProductionPlan), "PeriodYear", MatchType.Exact, pKDetail.PKHeader.RequestPeriodeYear))
        criterias.opAnd(New Criteria(GetType(PKProductionPlan), "ProductionYear", MatchType.Exact, pKDetail.PKHeader.ProductionYear))
        'Dim arlPKProductionPlan As ArrayList = New PKProductionPlanFacade(User).Retrieve(criterias)
        Dim arlPKProductionPlan As ArrayList = New PKProductionPlanFacade(User).RetrieveRencanaProduksi_DisplayDealerOrder(pKDetail.PKHeader.RequestPeriodeMonth, pKDetail.PKHeader.RequestPeriodeYear, pKDetail.VechileColor.MaterialNumber, pKDetail.PKHeader.ProductionYear)

        If arlPKProductionPlan.Count <> 0 Then
            Return CInt(CType(arlPKProductionPlan(0), PKProductionPlan).PlanQty) - CInt(CType(arlPKProductionPlan(0), PKProductionPlan).TotalSelesai) - CInt(CType(arlPKProductionPlan(0), PKProductionPlan).TotalReleaseAndAgree)
        Else
            Return 0
        End If
    End Function

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.AlokasiPKViewDetailPrivilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Alokasi PK")
        End If
        btnSimpan.Visible = SecurityProvider.Authorize(Context.User, SR.AlokasiPKSaveDetailPrivilege)
    End Sub

    Private Sub dtgEntryAllocation_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEntryAllocation.SortCommand
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
        dtgEntryAllocation.SelectedIndex = -1
        BindDataToGrid()
    End Sub

    Sub dtgEntryAllocation_ItemDataBound(ByVal Sender As Object, ByVal E As DataGridItemEventArgs)
        If Not (ArrListPKDetail.Count = 0 Or E.Item.ItemIndex = -1) Then
            Dim objPKDetail1 As PKDetail = ArrListPKDetail(E.Item.ItemIndex)
            E.Item.Cells(1).Text = (E.Item.ItemIndex + 1 + (dtgEntryAllocation.PageSize * dtgEntryAllocation.CurrentPageIndex)).ToString
            E.Item.Cells(3).Text = objPKDetail1.PKHeader.Dealer.DealerCode & " - " & objPKDetail1.PKHeader.Dealer.SearchTerm1
            E.Item.Cells(4).Text = objPKDetail1.PKHeader.Dealer.DealerName
            E.Item.Cells(6).Text = objPKDetail1.PKHeader.ProjectName

            Dim arrDealerSalesTarget As ArrayList = GetDealerSalesTarget(objPKDetail1)
            'Dim ListFreeDays As String = GetListFreeDays(objPKDetail1)

            'Dim TotalQty As Integer = GetTotalAllocation(objPKDetail1, ListFreeDays)
            Dim strSisaProgram As String = String.Empty
            Dim arrTotalQty As ArrayList = New ArrayList
            Dim isMaxQty As Boolean = False
            Dim oDST As DealerSalesTarget

            For i As Integer = 0 To arrDealerSalesTarget.Count - 1
                oDST = New DealerSalesTarget
                oDST = CType(arrDealerSalesTarget(i), DealerSalesTarget)
                'For Each LFD As DealerSalesTarget In arrDealerSalesTarget
                Dim strFreeDays As String = String.Empty
                For Each LFD2 As DealerSalesTarget In arrDealerSalesTarget
                    If LFD2.FreeDays <= oDST.FreeDays Then
                        strFreeDays += oDST.FreeDays & ","
                    End If
                Next
                strFreeDays = "0," & strFreeDays
                If strFreeDays.Length > 1 Then
                    strFreeDays = strFreeDays.Substring(0, strFreeDays.Length - 1)
                End If
                Dim TotalQty As Integer = GetTotalAllocation(objPKDetail1, strFreeDays)
                Dim Sisa As Integer = oDST.MaxQuantity - TotalQty
                If i = arrDealerSalesTarget.Count - 1 Then
                    isMaxQty = True
                End If
                If isMaxQty Then
                    strSisaProgram += "Sisa FD " & oDST.FreeDays.ToString & ": " & "-" + vbCrLf
                Else
                    strSisaProgram += "Sisa FD " & oDST.FreeDays.ToString & ": " & Sisa.ToString + vbCrLf
                End If
                'arrTotalQty.Add(TotalQty)
                'Next
            Next

            Dim lblCurrentFreeDays As Label = CType(E.Item.FindControl("lblCurrentFreeDays"), Label)
            Dim lblSisaProgram As Label = CType(E.Item.FindControl("lblSisaProgram"), Label)
            Dim lblPesan As Label = CType(E.Item.FindControl("lblPesan"), Label)
            Dim TextBox As TextBox = CType(E.Item.FindControl("TextBox1"), TextBox)
            Dim lblAlokasiAwal As Label = CType(E.Item.FindControl("lblAlokasiAwal"), Label)
            Dim lblVehicleModel As Label = CType(E.Item.FindControl("lblVehicleModel"), Label)

            lblVehicleModel.Text = objPKDetail1.VechileType.VechileModel.ID
            lblAlokasiAwal.Text = objPKDetail1.ResponseQty

            'Dim SisaProgram As Integer = 0
            'Dim FreeDays As Integer = 0
            'Dim QtyBerlebih As Integer = 0

            'Dim SisaProgramPK As Integer = 0
            'Dim arrSisaProgram As ArrayList = New ArrayList
            'If objPKDetail1.FreeDays = 0 Then
            '    For i As Integer = 0 To arrDealerSalesTarget.Count - 1
            '        objDealerSalesTarget = New DealerSalesTarget
            '        objDealerSalesTarget = CType(arrDealerSalesTarget(i), DealerSalesTarget)
            '        SisaProgram = objDealerSalesTarget.MaxQuantity - TotalQty
            '        If i = arrDealerSalesTarget.Count - 1 Then
            '            isMaxQty = True
            '        End If
            '        If SisaProgram > 0 Then
            '            'FreeDays = objDealerSalesTarget.FreeDays
            '            Exit For
            '        End If
            '    Next
            'Else
            '    For i As Integer = 0 To arrDealerSalesTarget.Count - 1
            '        objDealerSalesTarget = New DealerSalesTarget
            '        objDealerSalesTarget = CType(arrDealerSalesTarget(i), DealerSalesTarget)
            '        SisaProgram = objDealerSalesTarget.MaxQuantity - TotalQty
            '        If i = arrDealerSalesTarget.Count - 1 Then
            '            isMaxQty = True
            '        End If
            '        If SisaProgram >= 0 Then
            '            arrSisaProgram.Add(SisaProgram)
            '            'FreeDays = objDealerSalesTarget.FreeDays
            '            Exit For
            '        End If
            '    Next
            'End If

            'If objPKDetail1.FreeDays > 0 Then
            lblCurrentFreeDays.Text = objPKDetail1.FreeDays.ToString()
            'Else
            '    lblCurrentFreeDays.Text = FreeDays.ToString()
            'End If

            If arrDealerSalesTarget.Count > 0 Then
                lblSisaProgram.Text = strSisaProgram 'IIf(isMaxQty, "-", SisaProgram.ToString())

                Dim SelisihAlokasi As Integer = 0
                Dim Pengajuan As Integer = 0
                If arrPengajuan Is Nothing Then
                    arrPengajuan = CType(sessionHelper.GetSession("PengajuanPK"), ArrayList)
                End If

                Dim arrPKMessage As ArrayList
                If Not IsNothing(sessionHelper.GetSession("PengajuanPK")) Then
                    arrPKMessage = CType(sessionHelper.GetSession("PengajuanPK"), ArrayList)
                Else
                    arrPKMessage = New ArrayList
                End If
                'If arrPengajuan.Count > 0 Then
                '    If E.Item.ItemIndex > arrPengajuan.Count - 1 Then
                '        Pengajuan = 0
                '    Else
                '        Pengajuan = arrPengajuan(E.Item.ItemIndex)
                '    End If
                'End If

                'If Pengajuan <> 0 Then
                '    lblPesan.Text = "Maximum Jumlah Alokasi seharusnya " + (SisaProgram + CType(TextBox.Text, Integer)).ToString()
                'End If
                For Each objPKMessage As PKMessage In arrPKMessage
                    If objPKDetail1.ID = objPKMessage.PKDetailID Then
                        If objPKMessage.isInvalid Then
                            lblPesan.Text = "Maximum Jumlah Alokasi seharusnya " + objPKMessage.MaxAlokasi.ToString()
                        End If
                        Exit For
                    End If
                Next
            End If

            Dim lblDealerBranchCode As Label = CType(E.Item.FindControl("lblDealerBranchCode"), Label)

            If Not IsNothing(objPKDetail1.PKHeader.DealerBranch) Then
                lblDealerBranchCode.Text = objPKDetail1.PKHeader.DealerBranch.DealerBranchCode & " / " & objPKDetail1.PKHeader.DealerBranch.Term1
            End If

            Dim rangeValidator As RangeValidator = E.Item.FindControl("RangeValidator1")
            rangeValidator.MaximumValue = CInt(objPKDetail1.TargetQty)
            E.Item.Cells(11).Text = objPKDetail1.PKHeader.Purpose
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        sessionHelper.RemoveSession("PengajuanPK")
        btnHitung_Click(Nothing, Nothing)
        If isPassSPL Then
            If (CInt(lblSisaProduksiValue.Text) >= 0) Then
                For i As Integer = 0 To ArrListPKDetail.Count - 1
                    Dim objPKDetail1 As PKDetail = ArrListPKDetail(i)
                    Dim objPKMessage As PKMessage = New PKMessage

                    Dim AlokasiAwal As Integer = CType(CType(dtgEntryAllocation.Items.Item(i).FindControl("lblAlokasiAwal"), Label).Text, Integer)
                    Dim PenambahanAlokasi As Integer = CType(CType(dtgEntryAllocation.Items.Item(i).FindControl("TextBox1"), TextBox).Text, Integer) - AlokasiAwal

                    'If CType(dtgEntryAllocation.Items.Item(i).FindControl("lblSisaProgram"), Label).Text.Trim <> "-" AndAlso CType(dtgEntryAllocation.Items.Item(i).FindControl("lblSisaProgram"), Label).Text.Trim <> String.Empty Then
                    '    Dim SisaProgram As Integer = CType(CType(dtgEntryAllocation.Items.Item(i).FindControl("lblSisaProgram"), Label).Text, Integer)
                    '    If SisaProgram + AlokasiAwal < CType(CType(dtgEntryAllocation.Items.Item(i).FindControl("TextBox1"), TextBox).Text, Integer) Then
                    '        If PenambahanAlokasi > 0 Then
                    '            objPKMessage.PKDetailID = objPKDetail1.ID
                    '            objPKMessage.isInvalid = True
                    '            objPKMessage.MaxAlokasi = SisaProgram + AlokasiAwal
                    '            arrPengajuan.Add(objPKMessage)
                    '        End If
                    '        Continue For
                    '    End If
                    'End If
                    objPKDetail1.ResponseQty = CType(dtgEntryAllocation.Items.Item(i).FindControl("TextBox1"), TextBox).Text
                    Dim objPKDetailFacade As New PKDetailFacade(User)
                    objPKDetailFacade.Update(objPKDetail1)

                    Dim spFac As sp_GetPKAllocationFacade = New sp_GetPKAllocationFacade(User)
                    spFac.UpdateFreeDays(objPKDetail1.VechileColor.ID, objPKDetail1.PKHeader.RequestPeriodeMonth, _
                                         objPKDetail1.PKHeader.RequestPeriodeYear, objPKDetail1.PKHeader.Dealer.ID, _
                                         objPKDetail1.VechileType.VechileModel.ID)
                    objPKMessage.PKDetailID = objPKDetail1.ID
                    objPKMessage.isInvalid = False
                    objPKMessage.MaxAlokasi = 0
                    arrPengajuan.Add(objPKMessage)
                Next
                sessionHelper.SetSession("PengajuanPK", arrPengajuan)

                lblError.Text = ""
                MessageBox.Show(SR.SaveSuccess)
                BindDataToGrid()
            End If
        End If
    End Sub

    Private Sub btnHitung_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHitung.Click
        If Not Page.IsValid Then
            Return
        End If
        ArrListPKDetail = sessionHelper.GetSession("EntryPKDetail")
        Dim failed As Boolean = False
        Dim listPKDetail As ArrayList = GetPKSPL()
        If listPKDetail.Count > 0 Then
            If Not CekPKSPL(listPKDetail) Then
                MessageBox.Show("Total Alokasi Melebihi Aplikasi " & MessageSPL)
                failed = True
            End If
        Else
            MessageBox.Show("Item alokasi tidak ada yang valid.")
        End If
        If Not failed Then
            lblTotalProduksiValue.Text = GetSisaStock(sessionHelper.GetSession("AllocatedPKDetail"))
            lblSisaProduksiValue.Text = (CInt(lblTotalProduksiValue.Text) - HitungSisaProduksi()).ToString
            If lblSisaProduksiValue.Text < 0 Then
                MessageBox.Show("Total Alokasi Melebihi Sisa Stock")
            End If
        End If
    End Sub

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
                    obj = New PKSPL(item.PKHeader.SPLNumber, listDetail)
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
                        obj = New PKSPL(item.PKHeader.SPLNumber, listDetail)
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
                    MessageSPL = item.Code
                    Return False
                End If
            Next
        End If
        Return True
    End Function

    Private Function GetPKSPL() As ArrayList
        Dim list As ArrayList = New ArrayList
        For Each dtgItem As DataGridItem In dtgEntryAllocation.Items
            Dim txtBox As TextBox = dtgItem.FindControl("TextBox1")
            Dim id As Integer = CInt(dtgItem.Cells(0).Text)
            Dim opkDetail As PKDetail = New PKDetailFacade(User).Retrieve(id)
            'If (opkDetail.PKHeader.PKType = LookUp.enumPurpose.Khusus) And (opkDetail.PKHeader.SPLNumber <> "") Then
            If CInt(txtBox.Text) >= 0 Then
                opkDetail.ResponseQty = CInt(txtBox.Text)
                list.Add(opkDetail)
            End If
            'End If
        Next
        Return list
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

    Private Function GetRemainingSPL(ByVal objPKDetail As PKDetail) As Integer
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SPLDetail), "SPL.SPLNumber", MatchType.Exact, objPKDetail.PKHeader.SPLNumber.Trim))
        criterias.opAnd(New Criteria(GetType(SPLDetail), "PeriodMonth", MatchType.Exact, objPKDetail.PKHeader.RequestPeriodeMonth))
        criterias.opAnd(New Criteria(GetType(SPLDetail), "PeriodYear", MatchType.Exact, objPKDetail.PKHeader.RequestPeriodeYear))
        criterias.opAnd(New Criteria(GetType(SPLDetail), "VechileType.ID", MatchType.Exact, GetVehicleType(objPKDetail.VehicleTypeCode).ID))
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
        objPKdetail = sessionHelper.GetSession("AllocatedPKDetail")
        Dim int As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim filter As String = "(" & CType(enumStatusPK.Status.Konfirmasi, Integer) & "," & CType(enumStatusPK.Status.Rilis, Integer) & "," & CType(enumStatusPK.Status.Setuju, Integer) & "," & CType(enumStatusPK.Status.Selesai, Integer) & ")"
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.PKStatus", MatchType.InSet, filter))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "VechileColor.VechileType.ID", MatchType.Exact, objPKdetail.VechileColor.VechileType.ID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, objPKdetail.PKHeader.RequestPeriodeMonth))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, objPKdetail.PKHeader.RequestPeriodeYear))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.SPLNumber", MatchType.Exact, splNumber))
        Dim ArrList As ArrayList = New PKDetailFacade(User).Retrieve(criterias)
        For Each item As PKDetail In ArrList
            Dim isBeingAllocatedPK As Boolean = False
            For Each dtgItem As DataGridItem In dtgEntryAllocation.Items
                If (item.PKHeader.PKType = LookUp.enumPurpose.Khusus) And (item.PKHeader.SPLNumber <> "") Then
                    If CInt(dtgItem.Cells(0).Text) = item.ID Then
                        isBeingAllocatedPK = True
                        Dim txtBox As TextBox = dtgItem.FindControl("TextBox1")
                        int = int + CInt(txtBox.Text)
                        Exit For
                    End If
                End If
            Next
            If Not isBeingAllocatedPK Then
                int += item.ResponseQty
            End If
        Next
        Return int
    End Function

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        sessionHelper.RemoveSession("EntryPKDetail")
        Session.Remove("AllocatedPKDetail")
        Response.Redirect("../PK/DisplayDealerOrderQty.aspx?OrderType=" & ViewState("OrderType") & "&ProductionYear=" & ViewState("ProductionYear") & "&Category=" & ViewState("Category") & "&Type=" & ViewState("Type") & "&MaterialNumber=" & ViewState("MaterialNumber") & "&Periode=" & ViewState("Periode"))
    End Sub

#End Region
    Sub dtgEntryAllocation_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs)
        If e.CommandName = "View" Then
            Dim lblNoRegPK As LinkButton = e.Item.FindControl("lbtnNoRegPK")
            Dim DealerCode As String = e.Item.Cells(3).Text.Split("-")(0)
            If e.Item.Cells(11).Text = "1" Then
                sessionHelper.SetSession("PrevPage", Request.Url.ToString())
                Response.Redirect("../PK/PesananKendaraanBiasa.aspx?PKNumber=" & lblNoRegPK.Text & "&DealerCode=" & DealerCode)
            Else
                sessionHelper.SetSession("PrevPage", Request.Url.ToString())
                Response.Redirect("../PK/PesananKendaraanKhusus.aspx?PKNumber=" & lblNoRegPK.Text & "&DealerCode=" & DealerCode)
            End If
        End If
    End Sub
End Class

Public Class PKSPL
    Private _val As ArrayList
    Private _code As String
    Private _modeltype As String

    Public Sub New()

    End Sub
    Public Sub New(ByVal code As String, ByVal val As ArrayList, Optional ByVal modeltype As String = "")
        _code = code
        _val = val
        _modeltype = modeltype
    End Sub

    Public Property Code() As String
        Get
            Return _code
        End Get
        Set(ByVal Value As String)
            _code = Value
        End Set
    End Property

    Public Property Val() As ArrayList
        Get
            Return _val
        End Get
        Set(ByVal Value As ArrayList)
            _val = Value
        End Set
    End Property

    Public Property ModelType() As String
        Get
            Return _modeltype
        End Get
        Set(ByVal Value As String)
            _modeltype = Value
        End Set
    End Property


End Class

Public Class PKMessage
    Private _pkDetailID As Integer
    Private _maxAlokasi As String
    Private _isInvalid As Boolean

    Public Property PKDetailID() As Integer
        Get
            Return _pkDetailID
        End Get
        Set(ByVal Value As Integer)
            _pkDetailID = Value
        End Set
    End Property

    Public Property MaxAlokasi() As String
        Get
            Return _maxAlokasi
        End Get
        Set(ByVal Value As String)
            _maxAlokasi = Value
        End Set
    End Property

    Public Property isInvalid() As Boolean
        Get
            Return _isInvalid
        End Get
        Set(ByVal Value As Boolean)
            _isInvalid = Value
        End Set
    End Property

End Class