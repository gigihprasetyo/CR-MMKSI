Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports System
Imports System.Drawing.Color
Imports System.Text
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Helper

Public Class FrmDealerBranchList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator7 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtAddress As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator10 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlProvince As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCity As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtPostCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents RegularExpressionValidator12 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator14 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dtgDealerBranch As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private _sessHelper As SessionHelper = New SessionHelper


#Region "Custom"
    Private Sub ActivateUserPrivilege()
        'If Not SecurityProvider.Authorize(Context.User, SR.IDTPViewCreate_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=GENERAL - Cabang Dealer")
        'End If

        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)

        If (objDealer.Title <> EnumDealerTittle.DealerTittle.KTB) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=General - Cabang Dealer")

        Else
            If Not SecurityProvider.Authorize(Context.User, SR.DealerBranch_List_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=General - Cabang Dealer, lihat_daftar_DealerBranch")
            End If
        End If

    End Sub

    Private Sub InitiatePage()
        ClearData()
        AssignAttributeControl()
        ViewState("CurrentSortColumn") = "Dealer.DealerCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub AssignAttributeControl()
        Dim lblPopUp As Label = CType(Page.FindControl("lblPopUpDealer"), Label)
        lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpSelectingDealer.aspx", "", 500, 760, "DealerSelection")
    End Sub

    Private Sub BindDatagrid(ByVal indexPage)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) And Not IsNothing(CType(_sessHelper.GetSession("Criteria"), CriteriaComposite)) Then
            dtgDealerBranch.DataSource = New DealerBranchFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession("Criteria"), CriteriaComposite), _
              indexPage + 1, dtgDealerBranch.PageSize, totalRow, _
              CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgDealerBranch.VirtualItemCount = totalRow
            dtgDealerBranch.DataBind()
        End If
    End Sub


    Private Sub CreateCriteriaSearch()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'objDealer = Session("DEALER")
        'If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        'End If
        If Not (txtKodeDealer.Text.Trim() = "") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "Dealer.DealerCode", MatchType.InSet, ConvertKodeDealer(txtKodeDealer.Text.Trim())))
        End If
        If Not (txtName.Text.Trim() = "") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "Name", MatchType.[Partial], txtName.Text.Trim))
        End If
        If Not (txtAddress.Text.Trim() = "") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "Address", MatchType.[Partial], txtAddress.Text.Trim))
        End If

        _sessHelper.SetSession("Criteria", criterias)
    End Sub

    Private Function ConvertKodeDealer(ByVal sKodeDealerColl As String)
        Dim sKodeDealerTemp() As String = sKodeDealerColl.Split(New Char() {";"})
        Dim sKodeDealer As String = ""
        For i As Integer = 0 To sKodeDealerTemp.Length - 1
            sKodeDealer = sKodeDealer & "'" & sKodeDealerTemp(i).Trim() & "'"

            If Not (i = sKodeDealerTemp.Length - 1) Then
                sKodeDealer = sKodeDealer & ","
            End If
        Next
        sKodeDealer = "(" & sKodeDealer & ")"
        Return sKodeDealer
    End Function

    Private Sub ClearData()
        Me.txtName.ReadOnly = False
        Me.txtAddress.ReadOnly = False
        Me.txtKodeDealer.Text() = String.Empty
        Me.txtName.Text() = String.Empty
        Me.txtAddress.Text() = String.Empty
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        If dtgDealerBranch.Items.Count > 0 Then
            dtgDealerBranch.SelectedIndex = -1
        End If
    End Sub

    Private Sub UpdateDealerBranch()
        Dim objDealerBranch As DealerBranch = CType(_sessHelper.GetSession("vsDealerBranch"), DealerBranch)
        objDealerBranch.Name = Me.txtName.Text
        objDealerBranch.Address = Me.txtAddress.Text
        Try
            Dim nResult = New DealerBranchFacade(User).Update(objDealerBranch)
        Catch ex As Exception
            MessageBox.Show(SR.UpdateFail)
        End Try

    End Sub

    Private Sub ViewDealerBranch(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objDealerBranch As DealerBranch = New DealerBranchFacade(User).Retrieve(nID)
        If Not objDealerBranch Is Nothing Then
            _sessHelper.SetSession("vsDealerBranch", objDealerBranch)
            Me.txtKodeDealer.Text = objDealerBranch.Dealer.DealerCode
            Me.txtName.Text = objDealerBranch.Name
            Me.txtAddress.Text = objDealerBranch.Address
            Me.btnSimpan.Enabled = EditStatus
        Else
            MessageBox.Show(SR.ViewFail)
        End If

    End Sub

    Private Sub DeleteDealerBranch(ByVal nID As Integer)
        'If New HelperFacade(User, GetType(Dealer)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(DealerBranch), nID), _
        '    CreateAggreateForCheckRecord(GetType(DealerBranch))) Then
        '    MessageBox.Show(SR.CannotDelete)
        'Else
        Dim objDealerBranch As DealerBranch = New DealerBranchFacade(User).Retrieve(nID)
        If Not objDealerBranch Is Nothing Then
            Dim objDealerBranchFacade As DealerBranchFacade = New DealerBranchFacade(User)
            'objDealerBranchFacade.DeleteFromDB(objDealerBranch)
            objDealerBranchFacade.Delete(objDealerBranch)
            dtgDealerBranch.CurrentPageIndex = 0
            BindDatagrid(dtgDealerBranch.CurrentPageIndex)
        Else
            MessageBox.Show(SR.DeleteFail)
        End If
        'End If
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, ByVal dealerBranchID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "ID", MatchType.Exact, dealerBranchID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            BindDatagrid(0)
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        CreateCriteriaSearch()
        dtgDealerBranch.CurrentPageIndex = 0
        BindDatagrid(dtgDealerBranch.CurrentPageIndex)
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim objDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).GetDealer(Me.txtKodeDealer.Text)
        If objDealer.ID > 0 Then
            Dim objDealerBranch As DealerBranch = New DealerBranch
            Dim objDealerBranchFacade As DealerBranchFacade = New DealerBranchFacade(User)
            Dim nResult = -1
            Me.txtName.Enabled = True
            If CType(ViewState("vsProcess"), String) = "Insert" Then

                If objDealerBranchFacade.ValidateName(Me.txtName.Text) = 0 Then
                    objDealerBranch.Dealer = objDealer
                    objDealerBranch.Name = Me.txtName.Text
                    objDealerBranch.Address = Me.txtAddress.Text
                    nResult = New DealerBranchFacade(User).Insert(objDealerBranch)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else

                        MessageBox.Show(SR.SaveSuccess)
                    End If
                Else
                    MessageBox.Show(SR.DataIsExist("Dealer Group"))
                End If
            Else
                UpdateDealerBranch()
            End If

            ClearData()
            CreateCriteriaSearch()
            dtgDealerBranch.CurrentPageIndex = 0
            BindDatagrid(dtgDealerBranch.CurrentPageIndex)
        Else
            MessageBox.Show("Tidak ada dealer dengan kode " & txtKodeDealer.Text.Trim)
        End If
        
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub dtgDealerBranch_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgDealerBranch.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewDealerBranch(e.Item.Cells(0).Text, False)
            Me.txtName.ReadOnly = True
            Me.txtAddress.ReadOnly = True
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewDealerBranch(e.Item.Cells(0).Text, True)
            dtgDealerBranch.SelectedIndex = e.Item.ItemIndex
            Me.txtName.ReadOnly = False
            Me.txtAddress.ReadOnly = False
        ElseIf e.CommandName = "Delete" Then
            DeleteDealerBranch(e.Item.Cells(0).Text)
        End If
    End Sub

    Private Sub dtgDealerBranch_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDealerBranch.SortCommand
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

        dtgDealerBranch.SelectedIndex = -1
        dtgDealerBranch.CurrentPageIndex = 0
        BindDatagrid(dtgDealerBranch.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgDealerBranch_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDealerBranch.PageIndexChanged
        dtgDealerBranch.SelectedIndex = -1
        dtgDealerBranch.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgDealerBranch.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgDealerBranch_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDealerBranch.ItemDataBound
        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgDealerBranch.CurrentPageIndex * dtgDealerBranch.PageSize)
        End If

        'tambahan Privilege
        ActivateUserPrivilege()
        'If Not e.Item.FindControl("btnUbah") Is Nothing Then
        '    CType(e.Item.FindControl("btnUbah"), LinkButton).Visible = bPrivilegeChangeGroup
        'End If

        'If Not e.Item.FindControl("btnHapus") Is Nothing Then
        '    CType(e.Item.FindControl("btnHapus"), LinkButton).Visible = bPrivilegeChangeGroup
        'End If

    End Sub
End Class
