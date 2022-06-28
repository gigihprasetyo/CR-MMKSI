#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmPMKind
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtKindCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKindDesc As System.Web.UI.WebControls.TextBox
    Protected WithEvents TxtKM As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Private _sessHelper As SessionHelper = New SessionHelper
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator4 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator6 As System.Web.UI.WebControls.RequiredFieldValidator
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents dtgPMKind As System.Web.UI.WebControls.DataGrid
    Private m_bFormPrivilege As Boolean = False

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.PMJenisMasterView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PERIODICAL MAINTENANCE - Jenis PM")
        End If
    End Sub

    Dim bCekPriv As Boolean = SecurityProvider.Authorize(context.User, SR.PMJenisMasterEdit_Privilege)
    Private Sub CekBtnPriv()
        btnSimpan.Enabled = bCekPriv
        btnBatal.Enabled = bCekPriv
    End Sub
#End Region

#Region "Custom Method"

    Private Sub InitiatePage()
        ClearData()
        'SetControlPrivilege()
        ViewState("CurrentSortColumn") = "KindCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dtgPMKind.DataSource = New PMKindFacade(User).RetrieveActiveList(indexPage + 1, dtgPMKind.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgPMKind.VirtualItemCount = totalRow
            dtgPMKind.DataBind()
        End If
    End Sub

    Private Function UpdatePMKind() As Integer
        Dim objPMKind As PMKind = CType(Session.Item("vsPMKind"), PMKind)
        objPMKind.KindDescription = txtKindDesc.Text
        objPMKind.KM = TxtKM.Text
        Dim nResult = New PMKindFacade(User).Update(objPMKind)
        Return nResult
    End Function

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
          ByVal KindID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "PMKind", MatchType.Exact, KindID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Function DeletePMKind(ByVal nID As Integer) As Integer
        Dim iRecordCount As Integer = 0
        'If New HelperFacade(User, GetType(FreeService)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(FreeService), nID), _
        '    CreateAggreateForCheckRecord(GetType(FreeService))) Then
        '    iRecordCount = iRecordCount + 1
        'End If
        'If iRecordCount > 0 Then
        '    Return 2
        'Else
        Dim objPMKind As PMKind = New PMKindFacade(User).Retrieve(nID)
        Dim Facade As PMKindFacade = New PMKindFacade(User)
        Return Facade.DeleteFromDB(objPMKind)
        dtgPMKind.CurrentPageIndex = 0
        BindDatagrid(dtgPMKind.CurrentPageIndex)
        'End If
    End Function

    Private Sub ViewPMKind(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objPMKind As PMKind = New PMKindFacade(User).Retrieve(nID)
        _sessHelper.SetSession("vsPMKind", objPMKind)
        txtKindCode.Text = objPMKind.KindCode
        txtKindDesc.Text = objPMKind.KindDescription
        TxtKM.Text = objPMKind.KM.ToString("#,##0")
        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub ClearData()
        txtKindCode.Text() = String.Empty
        txtKindDesc.Text = String.Empty
        TxtKM.Text = String.Empty
        'btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        txtKindCode.ReadOnly = False
        TxtKM.ReadOnly = False
        txtKindDesc.ReadOnly = False
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'ActivateUserPrivilege()
        InitiateAuthorization()
        CekBtnPriv()
        If Not IsPostBack Then
            InitiatePage()
            BindDatagrid(0)
        End If
        'Put user code to initialize the page here
    End Sub
    Private Sub SetControlPrivilege()
        btnSimpan.Visible = m_bFormPrivilege
        btnBatal.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.ServiceJenisFSUpdate_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.ServiceJenisFSView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FREE SERVICE - Jenis PM")
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim objPMKind As PMKind = New PMKind
        Dim objPMKindFacade As PMKindFacade = New PMKindFacade(User)
        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not txtKindCode.Text = String.Empty Then
                If objPMKindFacade.ValidateCode(txtKindCode.Text) <= 0 Then
                    objPMKind.KindCode = txtKindCode.Text
                    objPMKind.KM = TxtKM.Text
                    objPMKind.KindDescription = txtKindDesc.Text
                    nResult = New PMKindFacade(User).Insert(objPMKind)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        ClearData()
                        dtgPMKind.SelectedIndex = -1
                    End If
                Else
                    MessageBox.Show(SR.DataIsExist("Kode PM"))
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Kode PM"))
            End If
        Else
            nResult = UpdatePMKind()
            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
                ClearData()
                dtgPMKind.SelectedIndex = -1
            End If
        End If

        'dtgReason.CurrentPageIndex = 0
        BindDatagrid(dtgPMKind.CurrentPageIndex)
    End Sub

    Private Sub dtgPMKind_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPMKind.ItemDataBound
        'If Not e.Item.DataItem Is Nothing Then
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = CType(e.Item.ItemIndex + 1 + (dtgPMKind.CurrentPageIndex * dtgPMKind.PageSize), String)
        End If

        'tambahan Privilege
        'ActivateUserPrivilege()
        If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
            CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = bCekPriv
        End If

        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = bCekPriv
        End If

        'End If
    End Sub

    Private Sub dtgPMKind_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPMKind.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            txtKindCode.ReadOnly = True
            TxtKM.ReadOnly = True
            txtKindDesc.ReadOnly = True

            ViewPMKind(e.Item.Cells(0).Text, False)
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewPMKind(e.Item.Cells(0).Text, True)
            dtgPMKind.SelectedIndex = e.Item.ItemIndex
            txtKindCode.ReadOnly = True
            TxtKM.ReadOnly = False
            txtKindDesc.ReadOnly = False
        ElseIf e.CommandName = "Delete" Then
            Try
                Dim nResult = DeletePMKind(e.Item.Cells(0).Text)
                If nResult = 2 Then
                    MessageBox.Show(SR.CannotDelete)
                ElseIf nResult = -1 Then
                    MessageBox.Show(SR.DeleteFail)
                Else
                    MessageBox.Show(SR.DeleteSucces)
                End If
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearData()
            BindDatagrid(dtgPMKind.CurrentPageIndex)
        End If
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        dtgPMKind.SelectedIndex = -1
    End Sub

    Private Sub dtgReason_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPMKind.SortCommand
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
        dtgPMKind.SelectedIndex = -1
        dtgPMKind.CurrentPageIndex = 0
        BindDatagrid(dtgPMKind.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgReason_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPMKind.PageIndexChanged
        dtgPMKind.SelectedIndex = -1
        dtgPMKind.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgPMKind.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

End Class