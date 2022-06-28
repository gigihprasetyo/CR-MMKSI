#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmReason
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtReasonDesc As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgReason As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlJenisServis As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtReasonCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Private _sessHelper As SessionHelper = New SessionHelper
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Private m_bFormPrivilege As Boolean = False

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Method"

    Private Sub bindDdlJenisServis()
        ddlJenisServis.Items.Insert(0, New ListItem("WSC", "WSC"))
        ddlJenisServis.Items.Insert(0, New ListItem("FS", "FS"))
    End Sub

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "ReasonCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dtgReason.DataSource = New ReasonFacade(User).RetrieveActiveList(indexPage + 1, dtgReason.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgReason.VirtualItemCount = totalRow
            dtgReason.DataBind()
        End If
    End Sub

    Private Function UpdateReason() As Integer
        Dim objReason As Reason = CType(Session.Item("vsReason"), Reason)
        objReason.Description = txtReasonDesc.Text
        objReason.JenisService = ddlJenisServis.SelectedItem.Text
        Dim nResult = New ReasonFacade(User).Update(objReason)
        Return nResult
    End Function

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
      ByVal ReasonID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "Reason", MatchType.Exact, ReasonID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Function DeleteReason(ByVal nID As Integer) As Integer
        Dim iRecordCount As Integer = 0

        If New HelperFacade(User, GetType(FreeService)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(FreeService), nID), _
            CreateAggreateForCheckRecord(GetType(FreeService))) Then
            iRecordCount = iRecordCount + 1
        End If

        If iRecordCount > 0 Then
            Return 2
        Else
            Dim objReason As Reason = New ReasonFacade(User).Retrieve(nID)
            Dim Facade As ReasonFacade = New ReasonFacade(User)
            Return Facade.DeleteFromDB(objReason)
            dtgReason.CurrentPageIndex = 0
            BindDatagrid(dtgReason.CurrentPageIndex)
        End If
    End Function

    Private Sub ViewReason(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objReason As Reason = New ReasonFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsReason", objReason)
        txtReasonCode.Text = objReason.ReasonCode
        ddlJenisServis.SelectedValue = objReason.JenisService
        txtReasonDesc.Text = objReason.Description
        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub ClearData()
        txtReasonCode.Text() = String.Empty
        txtReasonDesc.Text = String.Empty
        btnSimpan.Enabled = True
        ddlJenisServis.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        txtReasonCode.ReadOnly = False
        txtReasonDesc.ReadOnly = False
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            BindDatagrid(0)
            bindDdlJenisServis()
        End If
    End Sub

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = m_bFormPrivilege
        btnBatal.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.ServiceAlasanPenolakanUpdate_Privilege)
            
        If Not SecurityProvider.Authorize(Context.User, SR.ServiceAlasanPenolakanView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SERVICE - Form Alasan Penolakan")
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim objReason As Reason = New Reason
        Dim objReasonFacade As ReasonFacade = New ReasonFacade(User)
        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not txtReasonCode.Text = String.Empty Then
                If objReasonFacade.ValidateCode(txtReasonCode.Text) <= 0 Then
                    objReason.ReasonCode = txtReasonCode.Text
                    objReason.JenisService = ddlJenisServis.SelectedItem.Text
                    objReason.Description = txtReasonDesc.Text
                    nResult = New ReasonFacade(User).Insert(objReason)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                    End If
                Else
                    MessageBox.Show(SR.DataIsExist("Kode Alasan"))
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Kode Alasan"))
            End If
        Else
            nResult = UpdateReason()
            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
            End If
        End If
        ClearData()
        'dtgReason.CurrentPageIndex = 0
        BindDatagrid(dtgReason.CurrentPageIndex)
    End Sub

    Private Sub dtgReason_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgReason.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
            End If
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                CType(e.Item.FindControl("lblNo"), Label).Text = CType(e.Item.ItemIndex + 1 + (dtgReason.CurrentPageIndex * dtgReason.PageSize), String)
            End If

            'tambahan Privilege
            'ActivateUserPrivilege()
            If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = m_bFormPrivilege
            End If

            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = m_bFormPrivilege
            End If
        End If
    End Sub

    Private Sub dtgFSKind_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgReason.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            txtReasonCode.ReadOnly = True
            ddlJenisServis.Enabled = False
            txtReasonDesc.ReadOnly = True
            ViewReason(e.Item.Cells(0).Text, False)
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewReason(e.Item.Cells(0).Text, True)
            dtgReason.SelectedIndex = e.Item.ItemIndex
            txtReasonCode.ReadOnly = True
            ddlJenisServis.Enabled = True
            txtReasonDesc.ReadOnly = False
        ElseIf e.CommandName = "Delete" Then
            Try
                Dim nResult = DeleteReason(e.Item.Cells(0).Text)
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
            BindDatagrid(dtgReason.CurrentPageIndex)
        End If
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub dtgReason_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgReason.SortCommand
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
        dtgReason.SelectedIndex = -1
        dtgReason.CurrentPageIndex = 0
        BindDatagrid(dtgReason.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgReason_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgReason.PageIndexChanged
        dtgReason.SelectedIndex = -1
        dtgReason.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgReason.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

End Class