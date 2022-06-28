#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports KTB.DNet.UI.Helper
#End Region

Public Class frmSalesmanRekapSalesPerGroupDealer
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents txtAreaCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAreaDesc As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents dgResult As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlSalesmanUnit As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtCity As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchGroupDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtGroupDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealers As System.Web.UI.WebControls.Label
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    Private _SalesmanAreaFacade As New SalesmanAreaFacade(User)
    Private _SalesmanHeaderFacade As New SalesmanHeaderFacade(User)
    Private _create As Boolean
    Private _edit As Boolean
    Private _view As Boolean
    Private _delete As Boolean
    Private sessHelper As New SessionHelper
    Dim objJobPositionFacade As New JobPositionFacade(User)
    Private objDealer As Dealer

    Dim criterias As CriteriaComposite
    Dim BMCode As String = KTB.DNet.Lib.WebConfig.GetValue("SlmanCode")        'handle salesman
    Dim ManagerCode As String = KTB.DNet.Lib.WebConfig.GetValue("SCntCode")    'handle salesman counter
    Dim AssManCode As String = KTB.DNet.Lib.WebConfig.GetValue("SSpvCode")     'handle supervisor
    Dim spv1Code As String = KTB.DNet.Lib.WebConfig.GetValue("SManCode")       'handle sales manager
    Dim spv2Code As String = KTB.DNet.Lib.WebConfig.GetValue("BManCode")       'handle branch manager
    Dim spv3Code As String = "spv1"
    Dim SL1Code As String = "SL1"
    Dim SL2Code As String = "SL2"
    Dim TRCode As String = "TR"
    Dim arrList As New ArrayList
    Dim arrFilter As ArrayList
    Dim objSalesmanHeader As SalesmanHeader
#End Region

#Region "PrivateCustomMethods"
#Region "Need To Add"
    ' Untuk bind data yg bersangkutan - related
    Private Sub BindDropDownLists()
        ' dari enum
        'ddlSalesmanUnit.Items.Clear()
        'ddlSalesmanUnit.DataSource = EnumSalesUnit.RetriveSalesmanUnit
        'ddlSalesmanUnit.DataValueField = "ValStatus"
        'ddlSalesmanUnit.DataTextField = "NameStatus"
        'ddlSalesmanUnit.DataBind()
        CommonFunction.BindFromEnum("SalesmanUnit", ddlSalesmanUnit, Me.User, False, "NameStatus", "ValStatus")
    End Sub
    Private Sub ClearData()
        ddlSalesmanUnit.SelectedIndex = -1

        If dgResult.Items.Count > 0 Then
            dgResult.SelectedIndex = -1
        End If
    End Sub
    ' untuk update data yg sdh ada sebelumnya
    Private Sub UpdateArea()
        Dim objSalesmanArea As SalesmanArea = CType(sessHelper.GetSession("vsSalesmanArea"), SalesmanArea)
        objSalesmanArea.AreaDesc = txtAreaDesc.Text
        objSalesmanArea.City = txtCity.Text

        Dim nResult = New SalesmanAreaFacade(User).Update(objSalesmanArea)
        If nResult <= 0 Then
            MessageBox.Show("Record Gagal Diupdate")
        End If
    End Sub
    Private Sub Initialize()
        ClearData()
        ViewState("CurrentSortColumn") = "Dealer.ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub
    ' penambahan untuk delete data
    Private Sub DeleteArea(ByVal nID As Integer)
        Dim iRecordCount As Integer = 0

        Dim objSalesmanArea As SalesmanArea = New SalesmanAreaFacade(User).Retrieve(nID)
        Dim facade As SalesmanAreaFacade = New SalesmanAreaFacade(User)
        Dim iReturn As Integer = -1
        'iReturn = facade.DeleteTransaction(objSalesmanArea)
        iReturn = facade.DeleteFromDB(objSalesmanArea)
        If iReturn <= 0 Then
            MessageBox.Show("Record Gagal Dihapus")
        End If

        dgResult.CurrentPageIndex = 0
        BindDataGrid(dgResult.CurrentPageIndex)
    End Sub
    ' penambahan untuk view data
    Private Sub ViewArea(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objSalesmanArea As SalesmanArea = New SalesmanAreaFacade(User).Retrieve(nID)
        'Todo session
        'Session.Add("vsSalesmanArea", objSalesmanArea)
        sessHelper.SetSession("vsSalesmanArea", objSalesmanArea)
        txtAreaCode.Text = objSalesmanArea.AreaCode
        txtAreaDesc.Text = objSalesmanArea.AreaDesc
        txtCity.Text = objSalesmanArea.City
        Me.btnSimpan.Enabled = EditStatus
    End Sub
    ' ini perlu set security
    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0


        If CreateCriteria() = -3 Then
            dgResult.DataSource = Nothing
            dgResult.DataBind()
            MessageBox.Show("Kode dealer tidak valid.")
        Else
            Dim sortColl As SortCollection = New SortCollection

            'sortColl.Add(New Sort(GetType(SalesmanHeader), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
            sortColl.Add(New Sort(GetType(SalesmanHeader), "Dealer.DealerGroup.DealerGroupCode", CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
            arrList = _SalesmanHeaderFacade.Retrieve(criterias, sortColl)

            'arrList = _SalesmanHeaderFacade.RetrieveByCriteria(criterias, idxPage + 1, dgResult.PageSize, totalRow, _
            'CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

                Dim BMId As Integer = objJobPositionFacade.RetrieveIDByCode(BMCode)
                Dim ManagerId As Integer = objJobPositionFacade.RetrieveIDByCode(ManagerCode)
                Dim AssManId As Integer = objJobPositionFacade.RetrieveIDByCode(AssManCode)
                Dim spv1Id As Integer = objJobPositionFacade.RetrieveIDByCode(spv1Code)
                Dim spv2Id As Integer = objJobPositionFacade.RetrieveIDByCode(spv2Code)
                Dim strgroupDealerCode As String = ""
                arrFilter = New ArrayList
            For Each item As SalesmanHeader In arrList
                If Not IsNothing(item.Dealer.DealerGroup) Then
                    If (item.Dealer.DealerGroup.DealerGroupCode = strgroupDealerCode) Then
                        'update
                        Dim objSalesmanHeader As SalesmanHeader = CType(arrFilter(arrFilter.Count - 1), SalesmanHeader)
                        If (item.JobPosition.ID = BMId) Then
                            objSalesmanHeader.TotalBM = objSalesmanHeader.TotalBM + 1
                        ElseIf (item.JobPosition.ID = ManagerId) Then
                            objSalesmanHeader.TotalMgr = objSalesmanHeader.TotalMgr + 1
                        ElseIf (item.JobPosition.ID = AssManId) Then
                            objSalesmanHeader.TotalAMGR = objSalesmanHeader.TotalAMGR + 1
                        ElseIf (item.JobPosition.ID = spv1Id) Then
                            objSalesmanHeader.Totalspv1 = objSalesmanHeader.Totalspv1 + 1
                        ElseIf (item.JobPosition.ID = spv2Id) Then
                            objSalesmanHeader.Totalspv2 = objSalesmanHeader.Totalspv2 + 1
                        End If
                    Else
                        'insert
                        If (item.JobPosition.ID = BMId) Then
                            item.TotalBM = 1
                        ElseIf (item.JobPosition.ID = ManagerId) Then
                            item.TotalMgr = 1
                        ElseIf (item.JobPosition.ID = AssManId) Then
                            item.TotalAMGR = 1
                        ElseIf (item.JobPosition.ID = spv1Id) Then
                            item.Totalspv1 = 1
                        ElseIf (item.JobPosition.ID = spv2Id) Then
                            item.Totalspv2 = 1
                        End If
                        arrFilter.Add(item)
                    End If
                    strgroupDealerCode = item.Dealer.DealerGroup.DealerGroupCode

                End If
            Next

            'add by Ery MN
            If arrFilter.Count > 0 Then
                sessHelper.SetSession("ListSalesPerDealer", arrFilter)
            End If

            dgResult.DataSource = arrFilter
            dgResult.VirtualItemCount = arrFilter.Count
            dgResult.DataBind()
            End If

    End Sub
    Private Function CreateCriteria() As Integer
        objDealer = sessHelper.GetSession("DEALER")

        Dim criteriaDealer As CriteriaComposite
        Dim arrDealer As ArrayList
        Dim strDealerID As String = ""
        Dim strDefDate As String = "1753/01/01"

        'Get all DealerID according to DealerGroup entry 
        If txtGroupDealer.Text <> "" Then
            'DealerGroup.DealerGroupCode
            criteriaDealer = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriaDealer.opAnd(New Criteria(GetType(Dealer), "DealerGroup.DealerGroupCode", MatchType.InSet, "('" & txtGroupDealer.Text & "')"))

            arrDealer = New DealerFacade(User).RetrieveList(criteriaDealer, "DealerGroup.DealerGroupCode", Sort.SortDirection.ASC)

            If arrDealer.Count > 0 Then
                For Each item As Dealer In arrDealer
                    strDealerID = strDealerID & item.ID & ","
                Next
                strDealerID = Left(strDealerID, strDealerID.Length - 1)
            End If
        End If

        'Get all SalesmanHeader according to strDealerID
        criterias = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If ddlSalesmanUnit.SelectedItem.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, ddlSalesmanUnit.SelectedValue))
        End If
        'If ddlSalesmanUnit.SelectedValue = EnumSalesmanUnit.SalesmanUnit.Sparepart Then
        '    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SparePartIndicator", MatchType.Exact, 1))
        'ElseIf ddlSalesmanUnit.SelectedValue = EnumSalesmanUnit.SalesmanUnit.Unit Then
        '    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesUnitIndicator", MatchType.Exact, 1))
        'ElseIf ddlSalesmanUnit.SelectedValue = EnumSalesmanUnit.SalesmanUnit.Mekanik Then
        '    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "MechanicIndicator", MatchType.Exact, 1))
        'End If


        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If (txtDealerCode.Text.Trim <> String.Empty) Then
                criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
            End If
        Else
            If (txtDealerCode.Text.Trim <> String.Empty) Then
                If New DataOwner().IsdealerExistInGroup(txtDealerCode.Text.Trim, objDealer) Then
                    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
                Else
                    Return -3
                End If
            Else
                Dim strCrit As String = New DataOwner().GenerateDealerCodeSelection(objDealer, User)
                criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.InSet, strCrit))
            End If
        End If


        ' default criteria
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "ResignDate", MatchType.Exact, Date.Parse(strDefDate)))

        If strDealerID <> "" Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.ID", MatchType.InSet, "(" & strDealerID & ")"))
        End If

        Return 0
    End Function
#End Region

    Private Sub BindControlsAttribute()
        lblSearchGroupDealer.Attributes("onClick") = "ShowPPDealerSelection();"
    End Sub
    Private Function ValidateDealerSearching() As Boolean
        If ((txtDealerCode.Text <> String.Empty) And (txtGroupDealer.Text <> String.Empty)) Then
            MessageBox.Show("Pilih salah satu antara Kode Dealer atau Group Dealer sebagai kriteria.")
            Return False
        End If
        Return True
    End Function
#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        If Not IsPostBack Then
            Initialize()
            BindDropDownLists()
            BindControlsAttribute()
            lblDealers.Attributes("onclick") = "ShowPPDealerCodeSelection()"
            If Not IsNothing(Request.QueryString("Mode")) Then
                Select Case Request.QueryString("Mode")
                    Case "unit"
                        ddlSalesmanUnit.SelectedValue = EnumSalesmanUnit.SalesmanUnit.Unit
                        ddlSalesmanUnit.Enabled = False
                End Select

            End If
            BindDataGrid(0)

            If Val(Request.QueryString("IsFromProfile")) = 1 Then
                btnBack.Visible = True
            Else
                btnBack.Visible = False
            End If
        End If
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        sessHelper.SetSession("Status", "Insert")
        Response.Redirect("frmSalesmanRekapSalesPerGroupDealerDetail.aspx")
    End Sub
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        'to prevent error after sorting dummy property
        If ValidateDealerSearching() Then
            ViewState("CurrentSortColumn") = "Dealer.ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

            dgResult.CurrentPageIndex = 0
            BindDataGrid(0)
        End If
    End Sub
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim objSalesmanArea As SalesmanArea = New SalesmanArea
        Dim objSalesmanAreaFacade As SalesmanAreaFacade = New SalesmanAreaFacade(User)
        Dim nResult As Integer = -1
        txtAreaCode.ReadOnly = False
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not txtAreaCode.Text = String.Empty Then
                If objSalesmanAreaFacade.ValidateCode(txtAreaCode.Text) <= 0 Then
                    objSalesmanArea.AreaCode = txtAreaCode.Text
                    objSalesmanArea.AreaDesc = txtAreaDesc.Text
                    objSalesmanArea.City = txtCity.Text
                    nResult = New SalesmanAreaFacade(User).Insert(objSalesmanArea)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                    End If
                Else
                    MessageBox.Show(SR.DataIsExist("Kode Area"))
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Kode Area"))
            End If
        Else
            UpdateArea()
        End If

        ClearData()
        dgResult.CurrentPageIndex = 0
        BindDataGrid(dgResult.CurrentPageIndex)
    End Sub
    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        txtAreaCode.ReadOnly = False
    End Sub
    Private Sub dgResult_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResult.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            If Not (arrList Is Nothing) Then
                objSalesmanHeader = e.Item.DataItem
                Dim lblGroupDealer As Label = CType(e.Item.FindControl("lblGroupDealer"), Label)
                lblGroupDealer.Text = objSalesmanHeader.Dealer.DealerGroup.GroupName
                Dim lblBM As Label = CType(e.Item.FindControl("lblBM"), Label)
                lblBM.Text = objSalesmanHeader.TotalBM
                Dim lblManajer As Label = CType(e.Item.FindControl("lblManajer"), Label)
                lblManajer.Text = objSalesmanHeader.TotalMgr
                Dim lblAssManajer As Label = CType(e.Item.FindControl("lblAssManajer"), Label)
                lblAssManajer.Text = objSalesmanHeader.TotalAMGR
                Dim lblspv1 As Label = CType(e.Item.FindControl("lblspv1"), Label)
                lblspv1.Text = objSalesmanHeader.Totalspv1
                Dim lblspv2 As Label = CType(e.Item.FindControl("lblspv2"), Label)
                lblspv2.Text = objSalesmanHeader.Totalspv2
                Dim lblspv3 As Label = CType(e.Item.FindControl("lblspv3"), Label)
                lblspv3.Text = objSalesmanHeader.Totalspv3
                Dim lblsl1 As Label = CType(e.Item.FindControl("lblsl1"), Label)
                lblsl1.Text = objSalesmanHeader.Totalsl1
                Dim lblsl2 As Label = CType(e.Item.FindControl("lblsl2"), Label)
                lblsl2.Text = objSalesmanHeader.Totalsl2
                Dim lbltr As Label = CType(e.Item.FindControl("lbltr"), Label)
                lbltr.Text = objSalesmanHeader.TotalTR
                Dim lblTotal As Label = CType(e.Item.FindControl("lblTotal"), Label)
                lblTotal.Text = objSalesmanHeader.TotalBM + objSalesmanHeader.TotalMgr + objSalesmanHeader.TotalAMGR + objSalesmanHeader.Totalspv1 + objSalesmanHeader.Totalspv2 + objSalesmanHeader.Totalspv3 + objSalesmanHeader.Totalsl1 + objSalesmanHeader.Totalsl2 + objSalesmanHeader.TotalTR
            End If
        End If
        If (e.Item.ItemType = ListItemType.Footer) Then

            Dim total As Integer = 0
            'masukan ke BM
            For Each x As WebControls.DataGridItem In dgResult.Items
                total = total + CType(CType(x.FindControl("lblBM"), Label).Text, Integer)
            Next
            CType(e.Item.FindControl("lblTotalBM"), Label).Text = total.ToString("#,##0")

            'masukan ke MG
            total = 0
            For Each x As WebControls.DataGridItem In dgResult.Items
                total = total + CType(CType(x.FindControl("lblManajer"), Label).Text, Integer)
            Next
            CType(e.Item.FindControl("lblTotalMgr"), Label).Text = total.ToString("#,##0")

            'masukan ke ASSMG
            total = 0
            For Each x As WebControls.DataGridItem In dgResult.Items
                total = total + CType(CType(x.FindControl("lblAssManajer"), Label).Text, Integer)
            Next
            CType(e.Item.FindControl("lblTotalAMGR"), Label).Text = total.ToString("#,##0")

            'masukan ke SPV1
            total = 0
            For Each x As WebControls.DataGridItem In dgResult.Items
                total = total + CType(CType(x.FindControl("lblspv1"), Label).Text, Integer)
            Next
            CType(e.Item.FindControl("lblTotalSPV1"), Label).Text = total.ToString("#,##0")

            'masukan ke SPV2
            total = 0
            For Each x As WebControls.DataGridItem In dgResult.Items
                total = total + CType(CType(x.FindControl("lblspv2"), Label).Text, Integer)
            Next
            CType(e.Item.FindControl("lblTotalSPV2"), Label).Text = total.ToString("#,##0")

            'masukan ke SPV3
            total = 0
            For Each x As WebControls.DataGridItem In dgResult.Items
                total = total + CType(CType(x.FindControl("lblspv3"), Label).Text, Integer)
            Next
            CType(e.Item.FindControl("lblTotalSPV3"), Label).Text = total.ToString("#,##0")

            'masukan ke s11
            total = 0
            For Each x As WebControls.DataGridItem In dgResult.Items
                total = total + CType(CType(x.FindControl("lblsl1"), Label).Text, Integer)
            Next
            CType(e.Item.FindControl("lblTotalS11"), Label).Text = total.ToString("#,##0")

            'masukan ke s12
            total = 0
            For Each x As WebControls.DataGridItem In dgResult.Items
                total = total + CType(CType(x.FindControl("lblsl2"), Label).Text, Integer)
            Next
            CType(e.Item.FindControl("lblTotalS12"), Label).Text = total.ToString("#,##0")

            'masukan ke totalTR
            total = 0
            For Each x As WebControls.DataGridItem In dgResult.Items
                total = total + CType(CType(x.FindControl("lbltr"), Label).Text, Integer)
            Next
            CType(e.Item.FindControl("lblTotalTR"), Label).Text = total.ToString("#,##0")

            'masukan ke totalTotal
            total = 0
            For Each x As WebControls.DataGridItem In dgResult.Items
                total = total + CType(CType(x.FindControl("lblTotal"), Label).Text, Integer)
            Next
            CType(e.Item.FindControl("lblTotalTotal"), Label).Text = total.ToString("#,##0")

        End If
    End Sub
    Private Sub dgResult_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgResult.SortCommand
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

        arrFilter = CType(sessHelper.GetSession("ListSalesPerDealer"), ArrayList)
        If Not IsNothing(arrFilter) Then
            If arrFilter.Count > 0 Then
                dgResult.DataSource = CommonFunction.PageAndSortArraylist(CType(sessHelper.GetSession("ListSalesPerDealer"), ArrayList), dgResult.CurrentPageIndex, dgResult.PageSize, GetType(SalesmanHeader), e.SortExpression, ViewState("CurrentSortDirect"))
                dgResult.DataBind()
            End If
        End If
        'dgResult.SelectedIndex = -1
        'dgResult.CurrentPageIndex = 0
        'BindDataGrid(dgResult.CurrentPageIndex)
    End Sub
    Private Sub dgResult_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgResult.PageIndexChanged
        dgResult.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgResult.CurrentPageIndex)
    End Sub
    'add by ery, refer bug 1046
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("../General/FrmDealerProfileList.aspx?DealerID=" & sessHelper.GetSession("vDealerID"))
    End Sub
#End Region

#Region "Check Privilege"
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.TPDViewRecap_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Tenaga Penjual - Rekap Tenaga Penjual per Group Dealer")
        End If
    End Sub

#End Region






   

   End Class
