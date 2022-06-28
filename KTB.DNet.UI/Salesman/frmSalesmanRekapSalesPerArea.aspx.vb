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

#End Region


Public Class frmSalesmanRekapSalesPerArea
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
    Protected WithEvents ddlArea As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents icStartDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEndDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents dgResult As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlSalesmanUnit As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtCity As System.Web.UI.WebControls.TextBox

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
    Private _SalesmanAreaAssign As New SalesmanAreaAssignFacade(User)
    Private sessHelper As New SessionHelper
    Dim objJobPositionFacade As New JobPositionFacade(User)

    Dim criterias As CriteriaComposite
    Dim BMCode As String = KTB.DNet.Lib.WebConfig.GetValue("SlmanCode")        'handle salesman
    Dim ManagerCode As String = KTB.DNet.Lib.WebConfig.GetValue("SCntCode")    'handle salesman counter
    Dim AssManCode As String = KTB.DNet.Lib.WebConfig.GetValue("SSpvCode")     'handle supervisor
    Dim spv1Code As String = KTB.DNet.Lib.WebConfig.GetValue("SManCode")       'handle sales manager
    Dim spv2Code As String = KTB.DNet.Lib.WebConfig.GetValue("BManCode")       'handle branch manager

    'Dim spv3Code As String = "spv1"
    'Dim SL1Code As String = "SL1"
    'Dim SL2Code As String = "SL2"
    'Dim TRCode As String = "TR"
    '-------------------------------
    'Dim BMCode As String = "BM"
    'Dim ManagerCode As String = "MGR"
    'Dim AssManCode As String = "AMGR"
    'Dim spv1Code As String = "spv1"
    'Dim spv2Code As String = "spv2"
    Dim spv3Code As String = "spv1"
    Dim SL1Code As String = "SL1"
    Dim SL2Code As String = "SL2"
    Dim TRCode As String = "TR"
    Dim arrList As New ArrayList
    Dim objSalesmanHeader As SalesmanHeader
    Dim objSalesmanAreaAssign As SalesmanAreaAssign
    Dim BMId, ManagerId, AssManId, spv1Id, spv2Id, spv3Id, SL1Id, SL2Id, TRId As Integer
    Dim areaCode As String = ""
    Dim arrFilter As ArrayList = New ArrayList


#End Region

#Region "PrivateCustomMethods"




#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        If Not IsPostBack Then
            SetSetting()
            Initialize()
            BindDropDownLists()
            'BindControlsAttribute()
            BindDataGrid(0)
        End If
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        sessHelper.SetSession("Status", "Insert")
        Response.Redirect("frmSalesmanRekapSalesPerAreaDetail.aspx")
    End Sub
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgResult.CurrentPageIndex = 0
        sessHelper.RemoveSession("criteriaPerArea")
        BindDataGrid(0)
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
        If Not e.Item.DataItem Is Nothing Then
            objSalesmanAreaAssign = e.Item.DataItem
            Dim lblArea As Label = CType(e.Item.FindControl("lblArea"), Label)
            lblArea.Text = objSalesmanAreaAssign.SalesmanArea.AreaCode

            With objSalesmanAreaAssign.SalesmanHeader

                Dim lblBM As Label = CType(e.Item.FindControl("lblBM"), Label)
                lblBM.Text = .TotalBM
                Dim lblManajer As Label = CType(e.Item.FindControl("lblManajer"), Label)
                lblManajer.Text = .TotalMgr
                Dim lblAssManajer As Label = CType(e.Item.FindControl("lblAssManajer"), Label)
                lblAssManajer.Text = .TotalAMGR
                Dim lblspv1 As Label = CType(e.Item.FindControl("lblspv1"), Label)
                lblspv1.Text = .Totalspv1
                Dim lblspv2 As Label = CType(e.Item.FindControl("lblspv2"), Label)
                lblspv2.Text = .Totalspv2
                Dim lblspv3 As Label = CType(e.Item.FindControl("lblspv3"), Label)
                lblspv3.Text = .Totalspv3
                Dim lblsl1 As Label = CType(e.Item.FindControl("lblsl1"), Label)
                lblsl1.Text = .Totalsl1
                Dim lblsl2 As Label = CType(e.Item.FindControl("lblsl2"), Label)
                lblsl2.Text = .Totalsl2
                Dim lbltr As Label = CType(e.Item.FindControl("lbltr"), Label)
                lbltr.Text = .TotalTR
                Dim lblTotal As Label = CType(e.Item.FindControl("lblTotal"), Label)
                lblTotal.Text = .TotalBM + .TotalMgr + .TotalAMGR + .Totalspv1 + .Totalspv2 + .Totalspv3 + .Totalsl1 + .Totalsl2 + .TotalTR

            End With
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
        'modify by Ronny --sorting and paging in arraylist
        Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

            Case Sort.SortDirection.ASC
                ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

            Case Sort.SortDirection.DESC
                ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End Select

        arrFilter = CType(sessHelper.GetSession("listSalesmanArea"), ArrayList)
        If Not IsNothing(arrFilter) Then
            If arrFilter.Count > 0 Then
                arrFilter = CommonFunction.PageAndSortArraylist(arrFilter, dgResult.CurrentPageIndex, dgResult.PageSize, GetType(SalesmanAreaAssign), e.SortExpression, ViewState("CurrentSortDirect"))
                dgResult.DataSource = arrFilter
                dgResult.DataBind()
            End If
        End If

        'end modify
    End Sub
    Private Sub dgResult_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgResult.PageIndexChanged
        dgResult.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgResult.CurrentPageIndex)
    End Sub

#End Region

    
    

    #Region "Need To Add"
    ' Untuk bind data yg bersangkutan - related
    Private Sub BindDropDownLists()
        ' dari enum
        CommonFunction.BindFromEnum("SalesmanUnit", ddlSalesmanUnit, Me.User, True, "NameStatus", "ValStatus")

        'dari database
        CommonFunction.BindSalesmanArea(ddlArea, Me.User, True)

    End Sub

    Private Sub ClearData()
        ddlSalesmanUnit.SelectedIndex = -1
        ddlArea.SelectedIndex = -1
        icStartDate.Value = DateTime.Now
        icEndDate.Value = DateTime.Now

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
        ViewState("CurrentSortColumn") = "SalesmanArea.AreaCode"
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
        If (IsNothing(sessHelper.GetSession("criteriaPerArea"))) Then
            CreateCriteria()
            sessHelper.SetSession("criteriaPerArea", criterias)
        End If

        Dim sortColl As SortCollection = New SortCollection
        If (Not IsNothing(CType(ViewState("CurrentSortColumn"), String))) And (Not IsNothing(CType(ViewState("CurrentSortColumn"), String))) Then
            sortColl.Add(New Sort(GetType(SalesmanAreaAssign), "SalesmanArea.AreaCode", CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
        Else
            sortColl = Nothing
        End If

        'arrList = _SalesmanAreaAssign.RetrieveByCriteria(CType(sessHelper.GetSession("criteriaPerArea"), CriteriaComposite), idxPage + 1, dgResult.PageSize, totalRow, _
        'CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        arrList = _SalesmanAreaAssign.Retrieve(CType(sessHelper.GetSession("criteriaPerArea"), CriteriaComposite), sortColl)

        BMId = objJobPositionFacade.RetrieveIDByCode(BMCode)
        ManagerId = objJobPositionFacade.RetrieveIDByCode(ManagerCode)
        AssManId = objJobPositionFacade.RetrieveIDByCode(AssManCode)
        spv1Id = objJobPositionFacade.RetrieveIDByCode(spv1Code)
        spv2Id = objJobPositionFacade.RetrieveIDByCode(spv2Code)
        'spv3Id = objJobPositionFacade.RetrieveIDByCode(spv3Code)
        'SL1Id = objJobPositionFacade.RetrieveIDByCode(SL1Code)
        'SL2Id = objJobPositionFacade.RetrieveIDByCode(SL2Code)
        'TRId = objJobPositionFacade.RetrieveIDByCode(TRCode)

        If (ddlSalesmanUnit.SelectedIndex <> 0) Then
            For Each item As SalesmanAreaAssign In arrList
                If (item.SalesmanHeader.SalesIndicator = ddlSalesmanUnit.SelectedValue) Then
                    CountPosition(item)
                End If
            Next
        Else
            For Each item As SalesmanAreaAssign In arrList
                CountPosition(item)
            Next
        End If
        'modify by Ronny --create session
        If arrFilter.Count > 0 Then
            sessHelper.SetSession("listSalesmanArea", arrFilter)
        End If
        'end modify

        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        'dgResult.DataSource = CommonFunction.PageArraylist(arrFilter, idxPage, dgResult.PageSize)
        dgResult.DataSource = CommonFunction.PageAndSortArraylist(arrFilter, idxPage, dgResult.PageSize, GetType(SalesmanAreaAssign), "SalesmanArea.AreaCode", ViewState("CurrentSortDirect"))

        dgResult.VirtualItemCount = arrFilter.Count
        dgResult.DataBind()
    End Sub
    Private Sub CountPosition(ByVal item As SalesmanAreaAssign)
        With item.SalesmanHeader.JobPosition
            If (item.SalesmanArea.AreaCode = areaCode) Then
                'update
                Dim objSalesmanAreaAssign As SalesmanAreaAssign = CType(arrFilter(arrFilter.Count - 1), SalesmanAreaAssign)
                If (.ID = BMId) Then
                    objSalesmanAreaAssign.SalesmanHeader.TotalBM = objSalesmanAreaAssign.SalesmanHeader.TotalBM + 1
                ElseIf (.ID = ManagerId) Then
                    objSalesmanAreaAssign.SalesmanHeader.TotalMgr = objSalesmanAreaAssign.SalesmanHeader.TotalMgr + 1
                ElseIf (.ID = AssManId) Then
                    objSalesmanAreaAssign.SalesmanHeader.TotalAMGR = objSalesmanAreaAssign.SalesmanHeader.TotalAMGR + 1
                ElseIf (.ID = spv1Id) Then
                    objSalesmanAreaAssign.SalesmanHeader.Totalspv1 = objSalesmanAreaAssign.SalesmanHeader.Totalspv1 + 1
                ElseIf (.ID = spv2Id) Then
                    objSalesmanAreaAssign.SalesmanHeader.Totalspv2 = objSalesmanAreaAssign.SalesmanHeader.Totalspv2 + 1
                    'ElseIf (.ID = spv3Id) Then
                    '    objSalesmanAreaAssign.SalesmanHeader.Totalspv3 = objSalesmanAreaAssign.SalesmanHeader.Totalspv3 + 1
                    'ElseIf (.ID = SL1Id) Then
                    '    objSalesmanAreaAssign.SalesmanHeader.Totalsl1 = objSalesmanAreaAssign.SalesmanHeader.Totalsl1 + 1
                    'ElseIf (.ID = SL2Id) Then
                    '    objSalesmanAreaAssign.SalesmanHeader.Totalsl2 = objSalesmanAreaAssign.SalesmanHeader.Totalsl2 + 1
                    'ElseIf (.ID = TRId) Then
                    '    objSalesmanAreaAssign.SalesmanHeader.TotalTR = objSalesmanAreaAssign.SalesmanHeader.TotalTR + 1
                End If
            Else
                'insert
                If (.ID = BMId) Then
                    item.SalesmanHeader.TotalBM = 1
                ElseIf (.ID = ManagerId) Then
                    item.SalesmanHeader.TotalMgr = 1
                ElseIf (.ID = AssManId) Then
                    item.SalesmanHeader.TotalAMGR = 1
                ElseIf (.ID = spv1Id) Then
                    item.SalesmanHeader.Totalspv1 = 1
                ElseIf (.ID = spv2Id) Then
                    item.SalesmanHeader.Totalspv2 = 1
                    'ElseIf (.ID = spv3Id) Then
                    '    item.SalesmanHeader.Totalspv3 = 1
                    'ElseIf (.ID = SL1Id) Then
                    '    item.SalesmanHeader.Totalsl1 = 1
                    'ElseIf (.ID = SL2Id) Then
                    '    item.SalesmanHeader.Totalsl2 = 1
                    'ElseIf (.ID = TRId) Then
                    '    item.SalesmanHeader.TotalTR = 1
                End If
                arrFilter.Add(item)
            End If
            areaCode = item.SalesmanArea.AreaCode
        End With
    End Sub

    Private Sub CreateCriteria()
        Dim strDefDate As String = "1753/01/01"
        criterias = New CriteriaComposite(New Criteria(GetType(SalesmanAreaAssign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If (ddlArea.SelectedIndex <> 0) Then
            criterias.opAnd(New Criteria(GetType(SalesmanAreaAssign), "SalesmanArea.ID", MatchType.Exact, ddlArea.SelectedValue))
        End If

        criterias.opAnd(New Criteria(GetType(SalesmanAreaAssign), "SalesmanHeader.ResignDate", MatchType.Exact, Date.Parse(strDefDate)))
        criterias.opAnd(New Criteria(GetType(SalesmanAreaAssign), "SalesmanHeader.HireDate", MatchType.GreaterOrEqual, Format(icStartDate.Value, "yyyy-MM-dd HH:mm:ss")))
        criterias.opAnd(New Criteria(GetType(SalesmanAreaAssign), "SalesmanHeader.HireDate", MatchType.LesserOrEqual, Format(icEndDate.Value.AddDays(1), "yyyy-MM-dd HH:mm:ss")))
    End Sub

    Private Sub SetSetting()
        If Not IsNothing(Request.QueryString("Mode")) Then
            Select Case Request.QueryString("Mode")
                Case "unit"
                    ddlSalesmanUnit.SelectedValue = EnumSalesmanUnit.SalesmanUnit.Unit
                Case "part"
                    ddlSalesmanUnit.SelectedValue = EnumSalesmanUnit.SalesmanUnit.Sparepart
                Case "servis"
                    ddlSalesmanUnit.SelectedValue = EnumSalesmanUnit.SalesmanUnit.Mekanik
            End Select
            ddlSalesmanUnit.Enabled = False
        End If

    End Sub
#End Region

#Region "Check Privilege"
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.TPAViewRecap_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Tenaga Penjual - Rekap Tenaga Penjual per Area")
        End If

        '_create = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiBuat_Privilege)
        '_edit = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiUbah_Privilege)
        '_view = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiDetail_Privilege)

        ''lbtnNew.Visible = _create
        'btnSearch.Visible = _view

    End Sub
#End Region

End Class
