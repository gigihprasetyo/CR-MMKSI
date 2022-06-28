#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmFSKind
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
    Protected WithEvents ddlFSType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPM As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dtgFSKind As System.Web.UI.WebControls.DataGrid
    Protected WithEvents TxtKM As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Private _sessHelper As SessionHelper = New SessionHelper
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator4 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator6 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator7 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator8 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents cbStatus As CheckBox
    Protected WithEvents btnCari As Button

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
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++
#Region "Custom Method"

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "KindCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            '    dtgFSKind.DataSource = New FSKindFacade(User).RetrieveActiveList(indexPage + 1, dtgFSKind.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
            '      CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            '    dtgFSKind.VirtualItemCount = totalRow
            '    dtgFSKind.DataBind()

            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            CreateCriteria(crit)
            Dim arrData As ArrayList = New FSKindFacade(User).RetrieveByCriteria(crit, indexPage + 1, dtgFSKind.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgFSKind.VirtualItemCount = totalRow
            dtgFSKind.DataSource = arrData
            dtgFSKind.DataBind()
        End If
    End Sub

    Private Sub CreateCriteria(ByRef crit As CriteriaComposite)
        If txtKindCode.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(FSKind), "KindCode", MatchType.Partial, txtKindCode.Text))
        End If

        If TxtKM.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(FSKind), "KM", MatchType.Exact, TxtKM.Text))
        End If

        If txtKindDesc.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(FSKind), "KindDescription", MatchType.Partial, txtKindDesc.Text))
        End If

        'If ddlFSType.SelectedIndex <> 0 Then
        '    crit.opAnd(New Criteria(GetType(FSKind), "FSType", MatchType.Exact, ddlFSType.SelectedValue))
        'End If

        If cbStatus.Checked Then
            crit.opAnd(New Criteria(GetType(FSKind), "Status", MatchType.Exact, 0))
        End If
    End Sub
    Private Function UpdateFSKind() As Integer
        Dim objFSKind As FSKind = CType(Session.Item("vsFSKind"), FSKind)
        objFSKind.KindDescription = txtKindDesc.Text
        objFSKind.FSType = ddlFSType.SelectedValue
        objFSKind.PMKind = New PMKindFacade(User).Retrieve(CInt(ddlPM.SelectedValue))
        objFSKind.KM = TxtKM.Text
        If cbStatus.Checked Then
            objFSKind.Status = 0
        Else
            objFSKind.Status = -1
        End If
        Dim nResult = New FSKindFacade(User).Update(objFSKind)
        Return nResult
    End Function

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
          ByVal KindID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "FSKind", MatchType.Exact, KindID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Function DeleteFSKind(ByVal nID As Integer) As Integer
        Dim iRecordCount As Integer = 0
        If New HelperFacade(User, GetType(FreeService)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(FreeService), nID), _
            CreateAggreateForCheckRecord(GetType(FreeService))) Then
            iRecordCount = iRecordCount + 1
        End If
        If iRecordCount > 0 Then
            Return 2
        Else
            Dim objFSKind As FSKind = New FSKindFacade(User).Retrieve(nID)
            Dim Facade As FSKindFacade = New FSKindFacade(User)
            Return Facade.DeleteFromDB(objFSKind)
            dtgFSKind.CurrentPageIndex = 0
            BindDatagrid(dtgFSKind.CurrentPageIndex)
        End If
    End Function

    Private Sub ViewFSKind(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objFSKind As FSKind = New FSKindFacade(User).Retrieve(nID)
        _sessHelper.SetSession("vsFSKind", objFSKind)
        txtKindCode.Text = objFSKind.KindCode
        txtKindDesc.Text = objFSKind.KindDescription
        If objFSKind.FSType = "" Then
            ddlFSType.SelectedValue = ""
        Else
            ddlFSType.SelectedValue = objFSKind.FSType
        End If

        ddlPM.ClearSelection()
        If Not objFSKind.PMKind Is Nothing Then
            ddlPM.Items.FindByValue(objFSKind.PMKind.ID).Selected = True
        Else
            ddlPM.SelectedIndex = 0
        End If

        If objFSKind.Status = 0 Then
            cbStatus.Checked = True
        Else
            cbStatus.Checked = False
        End If
        TxtKM.Text = objFSKind.KM
        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub ClearData()
        txtKindCode.Text() = String.Empty
        txtKindDesc.Text = String.Empty
        TxtKM.Text = String.Empty
        ddlPM.SelectedIndex = 0
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        txtKindCode.ReadOnly = False
        TxtKM.ReadOnly = False
        txtKindDesc.ReadOnly = False
        ddlFSType.Enabled = True
        ddlPM.Enabled = True
        BindDDLFSType()
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            BindDatagrid(0)
            BindDDLFSType()
            BindDDLPMKind()
        End If
        'Put user code to initialize the page here
    End Sub
    Private Sub SetControlPrivilege()
        'btnSimpan.Visible = m_bFormPrivilege
        btnSimpan.Visible = False
        btnBatal.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.ServiceJenisFSUpdate_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.ServiceJenisFSView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FREE SERVICE - Form Jenis Free Service")
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        'If ddlFSType.SelectedValue = "Pilih Tipe FS" Then
        '    MessageBox.Show("Mohon pilih Tipe FS")
        '    Exit Sub
        'End If

        If ddlPM.SelectedIndex = 0 Then
            MessageBox.Show("Mohon pilih Jenis PM")
            Exit Sub
        End If
        Dim objFSKind As FSKind = New FSKind
        Dim objFSKindFacade As FSKindFacade = New FSKindFacade(User)
        Dim objPMKindFacade As PMKindFacade = New PMKindFacade(User)
        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not txtKindCode.Text = String.Empty Then
                If objFSKindFacade.ValidateCode(txtKindCode.Text) <= 0 Then
                    objFSKind.KindCode = txtKindCode.Text
                    objFSKind.KM = TxtKM.Text
                    objFSKind.KindDescription = txtKindDesc.Text
                    objFSKind.FSType = ddlFSType.SelectedValue
                    objFSKind.PMKind = objPMKindFacade.Retrieve(CInt(ddlPM.SelectedValue))
                    If cbStatus.Checked Then
                        objFSKind.Status = 0
                    Else
                        objFSKind.Status = -1
                    End If
                    nResult = objFSKindFacade.Insert(objFSKind)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        ClearData()
                        dtgFSKind.SelectedIndex = -1
                    End If
                Else
                    MessageBox.Show(SR.DataIsExist("Kode Free Service"))
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Kode Free Service"))
            End If
        Else
            nResult = UpdateFSKind()
            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
                ClearData()
                dtgFSKind.SelectedIndex = -1
            End If
        End If

        'dtgReason.CurrentPageIndex = 0
        BindDatagrid(dtgFSKind.CurrentPageIndex)
    End Sub

    Private Sub dtgFSKind_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgFSKind.ItemDataBound
        'If Not e.Item.DataItem Is Nothing Then
        Dim rowVal As FSKind = CType(e.Item.DataItem, FSKind)
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = CType(e.Item.ItemIndex + 1 + (dtgFSKind.CurrentPageIndex * dtgFSKind.PageSize), String)
            If rowVal.Status = 0 Then
                CType(e.Item.FindControl("lbtnActive"), LinkButton).Visible = False
                CType(e.Item.FindControl("lblStatus"), Label).Text = "Aktif"
            Else
                CType(e.Item.FindControl("lbtnInactive"), LinkButton).Visible = False
                CType(e.Item.FindControl("lblStatus"), Label).Text = "Pasif"
            End If

            If Not rowVal.PMKind Is Nothing Then
                CType(e.Item.FindControl("lblPM"), Label).Text = String.Format("{0} - {1}", rowVal.PMKind.KindCode, rowVal.PMKind.KindDescription)
            Else
                CType(e.Item.FindControl("lblPM"), Label).Text = ""
            End If
        End If

        'tambahan Privilege
        'ActivateUserPrivilege()
        If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
            'CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = m_bFormPrivilege
            CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False
        End If

        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            'CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = m_bFormPrivilege
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = False
        End If
        Dim lblFSType As Label = CType(e.Item.FindControl("lblFSType"), Label)
        If Not IsNothing(rowVal) Then
            Try
                'If rowVal.FSType.Length > 1 Then
                '    lblFSType.Text = rowVal.FSType.ToString
                'Else
                lblFSType.Text = New EnumFSKind().TypeByIndex(Integer.Parse(rowVal.FSType))
                'End If
            Catch
                lblFSType.Text = String.Empty
            End Try
        End If



        'End If
    End Sub

    Private Sub dtgFSKind_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgFSKind.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            txtKindCode.ReadOnly = True
            TxtKM.ReadOnly = True
            txtKindDesc.ReadOnly = True
            ddlPM.Enabled = False
            ddlFSType.Enabled = False
            ViewFSKind(e.Item.Cells(0).Text, False)
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewFSKind(e.Item.Cells(0).Text, True)
            dtgFSKind.SelectedIndex = e.Item.ItemIndex
            txtKindCode.ReadOnly = True
            TxtKM.ReadOnly = False
            txtKindDesc.ReadOnly = False
            ddlFSType.Enabled = True
            ddlPM.Enabled = True
        ElseIf e.CommandName = "Delete" Then
            Try
                Dim nResult = DeleteFSKind(e.Item.Cells(0).Text)
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
            BindDatagrid(dtgFSKind.CurrentPageIndex)

        ElseIf e.CommandName = "Active" Then
            ActivateParameter(e.Item.Cells(0).Text)
            BindDatagrid(dtgFSKind.CurrentPageIndex)  '-- Bind page-1

        ElseIf e.CommandName = "Inactive" Then
            InActivateParameter(e.Item.Cells(0).Text)
            BindDatagrid(dtgFSKind.CurrentPageIndex)  '-- Bind page-1
        End If
    End Sub

    Private Sub ActivateParameter(ByVal nID As Integer)
        '-- Activate Parameter
        Dim oFSKind As FSKind = New FSKindFacade(User).Retrieve(nID)
        oFSKind.Status = 0  '-- Parameter Aktif
        Dim nResult = New FSKindFacade(User).Update(oFSKind)
    End Sub

    Private Sub InActivateParameter(ByVal nID As Integer)
        '-- Inactivate Parameter
        Dim oFSKind As FSKind = New FSKindFacade(User).Retrieve(nID)
        oFSKind.Status = 1  '-- Parameter Tidak Aktif
        Dim nResult = New FSKindFacade(User).Update(oFSKind)
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        dtgFSKind.SelectedIndex = -1
    End Sub

    Private Sub dtgReason_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgFSKind.SortCommand
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
        dtgFSKind.SelectedIndex = -1
        dtgFSKind.CurrentPageIndex = 0
        BindDatagrid(dtgFSKind.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgReason_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgFSKind.PageIndexChanged
        dtgFSKind.SelectedIndex = -1
        dtgFSKind.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgFSKind.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub BindDDLFSType()
        ddlFSType.Items.Clear()
        Dim listTitle As New EnumFSKind
        Dim al2 As ArrayList = listTitle.RetrieveFSType
        For Each item As EnumFSType In al2
            ddlFSType.Items.Add(New ListItem(item.NameTitle, item.ValTitle))
        Next
        ddlFSType.Items.Insert(0, "Pilih Tipe FS")

        ddlFSType.SelectedValue = CInt(EnumFSKind.FSType.Regular).ToString()
    End Sub

    Private Sub BindDDLPMKind()
        Dim objPMKindFacade As PMKindFacade = New PMKindFacade(User)
        ddlPM.Items.Clear()

        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim results As ArrayList = objPMKindFacade.Retrieve(crit)

        With ddlPM.Items
            For Each obj As PMKind In results
                .Add(New ListItem(String.Format("{0} - {1}", obj.KindCode, obj.KindDescription), obj.ID))
            Next
        End With

        ddlPM.Items.Insert(0, "Pilih Jenis PM")
    End Sub

#End Region

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        BindDatagrid(0)
    End Sub
End Class