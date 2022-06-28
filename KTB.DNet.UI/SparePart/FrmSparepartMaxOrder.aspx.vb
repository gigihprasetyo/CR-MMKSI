Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports System.String
Imports System.Text

Public Class FrmSparepartMaxOrder
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtSPNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchSparePart As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents txtMaxOrder As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents txtIDSPMaxOrder As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtIDSPMaster As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents dgSparePart As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Requiredfieldvalidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents chkQty As System.Web.UI.WebControls.CheckBox

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
    Private sessHelper As New SessionHelper
    Private SPMasterFacade As New SparePartMasterFacade(User)
    Private SPMaxOrderFacade As New SparePartMaxOrderFacade(User)
    Private _edit As Boolean
    Private _view As Boolean
#End Region

#Region "PrivateCustomMethods"
    Private Function IsQtyMaksValid(ByVal IDSPMaster As Integer) As Boolean
        Dim retValue As Boolean = False
        Dim ArrListSPMaxOrder As New ArrayList

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparepartMaxOrder), _
            "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SparepartMaxOrder), "SparePartMaster.ID", MatchType.Exact, IDSPMaster))

        ArrListSPMaxOrder = SPMaxOrderFacade.Retrieve(criterias)
        If ArrListSPMaxOrder.Count > 0 Then
            If CType(ArrListSPMaxOrder(0), SparepartMaxOrder).MaxRequest > 0 Then
                retValue = True
            Else
                retValue = False
            End If
        End If

        Return retValue
    End Function
    Private Sub EnableControl(ByVal isEnable As Boolean)
        txtSPNumber.ReadOnly = Not isEnable
        txtDescription.ReadOnly = True
        txtMaxOrder.ReadOnly = Not isEnable

        If isEnable Then
            lblSearchSparePart.Attributes("onclick") = "ShowPopUpSparePart()"
        Else
            lblSearchSparePart.Attributes("onclick") = ""
        End If

    End Sub
    'Private Sub ViewSparePartMaster(ByVal id As Integer, ByVal idSPMaxOrder As Integer)
    '    Dim ObjSPMaster As SparePartMaster = SPMasterFacade.Retrieve(id)
    '    txtSPNumber.Text = ObjSPMaster.PartNumber
    '    txtDescription.Text = ObjSPMaster.PartName
    '    txtIDSPMaster.Text = ObjSPMaster.ID.ToString()

    '    Dim ObjSPMaxOrder As New SparepartMaxOrder
    '    If idSPMaxOrder > 0 Then
    '        ObjSPMaxOrder = SPMaxOrderFacade.Retrieve(idSPMaxOrder)
    '        txtMaxOrder.Text = ObjSPMaxOrder.MaxRequest.ToString()
    '        txtIDSPMaxOrder.Text = ObjSPMaxOrder.ID.ToString()
    '    Else
    '        txtMaxOrder.Text = ""
    '        txtIDSPMaxOrder.Text = ""
    '    End If

    'End Sub

    Private Sub ViewSparePartMaster(ByVal id As String, ByVal idSPMaxOrder As String)
        Dim ObjSPMaster As SparePartMaster = SPMasterFacade.Retrieve(CInt(id))
        txtSPNumber.Text = ObjSPMaster.PartNumber
        txtDescription.Text = ObjSPMaster.PartName
        txtIDSPMaster.Text = ObjSPMaster.ID.ToString()

        Dim ObjSPMaxOrder As New SparepartMaxOrder
        If CInt(idSPMaxOrder) > 0 Then
            ObjSPMaxOrder = SPMaxOrderFacade.Retrieve(CInt(idSPMaxOrder))
            txtMaxOrder.Text = ObjSPMaxOrder.MaxRequest.ToString()
            txtIDSPMaxOrder.Text = ObjSPMaxOrder.ID.ToString()
        Else
            txtMaxOrder.Text = ""
            txtIDSPMaxOrder.Text = ""
        End If

    End Sub

    Private Sub CreateCriteria()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        If companyCode.Trim.ToUpper <> "" Then
            criterias.opAnd(New Criteria(GetType(SparePartMaster), "ProductCategory.Code", MatchType.Exact, companyCode.Trim.ToUpper))
        End If
        criterias.opAnd(New Criteria(GetType(SparePartMaster), "ActiveStatus", MatchType.Exact, CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short)))

        'If chkQty.Checked Then

        If chkQty.Checked Then
            Dim critSparepartMaxOrder As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparepartMaxOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If txtSPNumber.Text.Length > 0 Then
                critSparepartMaxOrder.opAnd(New Criteria(GetType(SparepartMaxOrder), "SparePartMaster.PartNumber", MatchType.Exact, txtSPNumber.Text.Trim()))
            End If
            If txtMaxOrder.Text.Length > 0 Then
                critSparepartMaxOrder.opAnd(New Criteria(GetType(SparepartMaxOrder), "MaxRequest", MatchType.Exact, CInt(txtMaxOrder.Text.Trim)))
            End If
            critSparepartMaxOrder.opAnd(New Criteria(GetType(SparepartMaxOrder), "MaxRequest", MatchType.Greater, 0))
            Dim arrListSPMaxOrder As ArrayList = SPMaxOrderFacade.Retrieve(critSparepartMaxOrder)
            Dim strIDSPMaster As New StringBuilder
            Dim ObjSPMaxOrder As New SparepartMaxOrder

            For i As Integer = 0 To arrListSPMaxOrder.Count - 1
                ObjSPMaxOrder = CType(arrListSPMaxOrder(i), SparepartMaxOrder)
                If strIDSPMaster.Length = 0 Then
                    strIDSPMaster.Append("(" + CType(ObjSPMaxOrder.SparePartMaster.ID, String))
                ElseIf strIDSPMaster.Length > 0 Then
                    strIDSPMaster.Append("," + CType(ObjSPMaxOrder.SparePartMaster.ID, String))
                End If
            Next i
            If strIDSPMaster.Length > 0 Then
                strIDSPMaster.Append(")")
                criterias.opAnd(New Criteria(GetType(SparePartMaster), "ID", MatchType.InSet, strIDSPMaster))
            End If
        End If

        'Else
        If txtSPNumber.Text.Trim().Length > 0 Then
            criterias.opAnd(New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.Exact, txtSPNumber.Text.Trim()))
        End If
        'End If

        sessHelper.SetSession("CRITERIAS", criterias)
    End Sub
    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As ArrayList = SPMasterFacade.RetrieveByCriteria(CType(sessHelper.GetSession("CRITERIAS"), CriteriaComposite), indexPage + 1, dgSparePart.PageSize, _
    totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgSparePart.DataSource = arrList
        dgSparePart.VirtualItemCount = totalRow
        dgSparePart.DataBind()

    End Sub
    Private Sub Initialize()
        txtSPNumber.Text = ""
        txtDescription.Text = ""
        txtMaxOrder.Text = ""
        txtIDSPMaster.Text = ""
        txtIDSPMaxOrder.Text = ""
        dgSparePart.SelectedIndex = -1
        ViewState("CurrentSortColumn") = "PartNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        btnSave.Enabled = False
    End Sub
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.ENHSparePartMaksimumStockView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=MASTER BARANG - Setting Maksimum Stock")
        End If
        _edit = SecurityProvider.Authorize(context.User, SR.ENHSparePartMaksimumStockEdit_Privilege)
        _view = SecurityProvider.Authorize(context.User, SR.ENHSparePartMaksimumStockView_Privilege)
    End Sub
#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        If Not IsPostBack Then
            txtIDSPMaster.Style("display") = "none"
            txtIDSPMaxOrder.Style("display") = "none"
            Initialize()
            EnableControl(True)
            CreateCriteria()
            BindDataGrid(0)
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtIDSPMaxOrder.Text.Trim().Length > 0 Then
            Dim ObjSPMaxOrder As SparepartMaxOrder = SPMaxOrderFacade.Retrieve(CType(txtIDSPMaxOrder.Text.Trim(), Integer))
            ObjSPMaxOrder.MaxRequest = CType(txtMaxOrder.Text.Trim(), Integer)
            Dim i As Integer = SPMaxOrderFacade.Update(ObjSPMaxOrder)
            If i = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
                EnableControl(True)
                Initialize()
                BindDataGrid(0)
            End If
        ElseIf txtIDSPMaxOrder.Text.Trim().Length <= 0 Then
            Dim ObjSPMaster As SparePartMaster = SPMasterFacade.Retrieve(CType(txtIDSPMaster.Text.Trim(), Integer))
            Dim ObjSPMaxOrder As New SparepartMaxOrder
            ObjSPMaxOrder.SparePartMaster = ObjSPMaster
            ObjSPMaxOrder.MaxRequest = CType(txtMaxOrder.Text.Trim(), Integer)
            Dim i As Integer = SPMaxOrderFacade.Insert(ObjSPMaxOrder)
            If i = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
                EnableControl(True)
                Initialize()
                BindDataGrid(0)
            End If
        End If
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Initialize()
        EnableControl(True)
        btnSearch.Enabled = True
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        CreateCriteria()
        btnSave.Enabled = False
        BindDataGrid(0)
    End Sub
    Private Sub dgSparePart_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSparePart.ItemDataBound
        If Not e.Item.ItemIndex = -1 Then
            Dim ObjSPMaster As SparePartMaster = CType(e.Item.DataItem, SparePartMaster)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparepartMaxOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparepartMaxOrder), "SparePartMaster.ID", MatchType.Exact, ObjSPMaster.ID))
            Dim arrList As ArrayList = SPMaxOrderFacade.Retrieve(criterias)

            If arrList.Count > 0 Then
                e.Item.Cells(1).Text = CType(arrList(0), SparepartMaxOrder).ID.ToString()
                If CType(arrList(0), SparepartMaxOrder).MaxRequest > 0 Then
                    e.Item.Cells(5).Text = CType(arrList(0), SparepartMaxOrder).MaxRequest.ToString()
                Else
                    e.Item.Cells(5).Text = "0"
                End If
            Else
                e.Item.Cells(1).Text = "0"
                e.Item.Cells(5).Text = "0"
            End If

            e.Item.Cells(2).Text = e.Item.ItemIndex + 1 + (dgSparePart.CurrentPageIndex * dgSparePart.PageSize)

            Dim lView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            Dim lEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lView.Visible = _view
            lEdit.Visible = _edit
        End If
    End Sub
    Private Sub dgSparePart_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSparePart.ItemCommand
        If e.CommandName = "View" Then

            dgSparePart.SelectedIndex = e.Item.ItemIndex
            EnableControl(False)
            If e.Item.Cells(1).Text.Trim().Length > 0 Then
                ViewSparePartMaster(e.Item.Cells(0).Text, e.Item.Cells(1).Text)
            Else
                ViewSparePartMaster(e.Item.Cells(0).Text, 0)
            End If
            btnSave.Enabled = False
            btnSearch.Enabled = False
        ElseIf e.CommandName = "Edit" Then

            dgSparePart.SelectedIndex = e.Item.ItemIndex
            EnableControl(True)
            If e.Item.Cells(1).Text.Trim().Length > 0 Then
                ViewSparePartMaster(e.Item.Cells(0).Text, e.Item.Cells(1).Text)
            Else
                ViewSparePartMaster(e.Item.Cells(0).Text, 0)
            End If
            btnSave.Enabled = True
            btnSearch.Enabled = False
        End If
    End Sub
    Private Sub dgSparePart_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSparePart.PageIndexChanged
        dgSparePart.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgSparePart.CurrentPageIndex)
    End Sub
    Private Sub dgSparePart_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSparePart.SortCommand
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

        dgSparePart.SelectedIndex = -1
        dgSparePart.CurrentPageIndex = 0
        BindDataGrid(dgSparePart.CurrentPageIndex)
    End Sub
#End Region

End Class
