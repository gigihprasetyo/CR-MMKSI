#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Claim

#End Region

Public Class FrmClaimGoodCondition
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dtgClaimGoodCondition As System.Web.UI.WebControls.DataGrid
    Protected WithEvents valcode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents valCondition As System.Web.UI.WebControls.RequiredFieldValidator

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
    Private arlGoodCondition As ArrayList
    Private objGoodCondition As ClaimGoodCondition
    Private sHelper As SessionHelper = New SessionHelper

#End Region

#Region "Custom Method"

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimGoodCondition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ClaimGoodCondition), "Code", MatchType.No, String.Empty))

            'arlGoodCondition = New ClaimGoodConditionFacade(User).RetrieveActiveList(indexPage + 1, dtgClaimGoodCondition.PageSize, totalRow, sHelper.GetSession("SortCol"), sHelper.GetSession("SortDirection"))
            arlGoodCondition = New ClaimGoodConditionFacade(User).RetrieveActiveList(indexPage + 1, dtgClaimGoodCondition.PageSize, totalRow, sHelper.GetSession("SortCol"), sHelper.GetSession("SortDirection"), criterias)
            dtgClaimGoodCondition.DataSource = arlGoodCondition
            dtgClaimGoodCondition.VirtualItemCount = totalRow
            dtgClaimGoodCondition.DataBind()
        End If
    End Sub

    Private Sub ClearData()
        txtCode.Text = ""
        txtDescription.Text = ""
        ViewState.Add("vsProcess", "Insert")
        dtgClaimGoodCondition.SelectedIndex = -1
        If CekBtnPriv() Then
            btnSimpan.Enabled = True
        Else
            btnSimpan.Enabled = False
        End If
    End Sub


#End Region

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.KondisiBarangView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=CLAIM - Kondisi Barang")
        End If
    End Sub

    Private Function CekBtnPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.KondisiBarangCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#Region "Event Hendlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        InitiateAuthorization()
        If Not IsPostBack Then
            ClearData()
            sHelper.SetSession("SortCol", "Code")
            sHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
            BindDataGrid(0)
        End If
        If CekBtnPriv() Then
            btnSimpan.Enabled = True
        Else
            btnSimpan.Enabled = False
        End If
    End Sub
#End Region


    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click

        If Not Page.IsValid Then
            Return
        End If

        'Check Code
        Dim Idedit As Integer
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            Idedit = 0
        ElseIf CType(ViewState("vsProcess"), String) = "Edit" Then
            Idedit = CType(sHelper.GetSession("objedit"), ClaimGoodCondition).ID
        End If
        Dim codeIsValid As Integer = New ClaimGoodConditionFacade(User).ValidateCode(txtCode.Text, Idedit)

        If codeIsValid > 0 Then
            MessageBox.Show("Kode Sudah Ada !!")
            Return
        End If

        'Transaction
        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "Insert" Then  '-- If Condition is Insert

            Dim objGoodCondition As ClaimGoodCondition = New ClaimGoodCondition
            objGoodCondition.Code = txtCode.Text.ToUpper
            objGoodCondition.Condition = txtDescription.Text.ToUpper
            nResult = New ClaimGoodConditionFacade(User).Insert(objGoodCondition)
        ElseIf CType(ViewState("vsProcess"), String) = "Edit" Then
            Dim objGoodCondition As ClaimGoodCondition = CType(sHelper.GetSession("objedit"), ClaimGoodCondition)
            objGoodCondition.Code = txtCode.Text.ToUpper
            objGoodCondition.Condition = txtDescription.Text.ToUpper
            nResult = New ClaimGoodConditionFacade(User).Update(objGoodCondition)
        End If

        If nResult = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            MessageBox.Show(SR.SaveSuccess)
        End If

        BindDataGrid(dtgClaimGoodCondition.CurrentPageIndex)
        ClearData()


    End Sub

    Private Sub dtgClaimGoodCondition_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgClaimGoodCondition.SelectedIndexChanged

    End Sub

    Private Sub dtgClaimGoodCondition_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClaimGoodCondition.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            If Not (arlGoodCondition Is Nothing) Then
                objGoodCondition = CType(arlGoodCondition(e.Item.ItemIndex), ClaimGoodCondition)
                If objGoodCondition.Code <> "" Or objGoodCondition.Code <> String.Empty Then
                    Dim _lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                    _lblNo.Text = CType(e.Item.ItemIndex + 1 + (dtgClaimGoodCondition.CurrentPageIndex * dtgClaimGoodCondition.PageSize), String)

                    Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                    _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                    _lbtnDelete.CommandArgument = objGoodCondition.ID.ToString

                    Dim _lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                    _lbtnEdit.CommandArgument = objGoodCondition.ID.ToString
                    Dim _lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
                    If CekBtnPriv() Then
                        _lbtnEdit.Visible = True
                        _lbtnDelete.Visible = True
                        _lbtnView.Visible = True
                    Else
                        _lbtnEdit.Visible = False
                        _lbtnDelete.Visible = False
                        _lbtnView.Visible = False
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub dtgClaimGoodCondition_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgClaimGoodCondition.ItemCommand

        If e.CommandName = "Delete" Then
            DeleteGoodCondition(CInt(e.CommandArgument))
        ElseIf e.CommandName = "Edit" Then           '-- Edit/Update Condition
            ViewState.Add("vsProcess", "Edit")
            dtgClaimGoodCondition.SelectedIndex = e.Item.ItemIndex
            Dim objClaimGoodCondition As ClaimGoodCondition = New ClaimGoodConditionFacade(User).Retrieve(CInt(e.CommandArgument))
            sHelper.SetSession("objedit", objClaimGoodCondition)
            txtCode.Text = objClaimGoodCondition.Code
            txtDescription.Text = objClaimGoodCondition.Condition
            btnSimpan.Enabled = True
        ElseIf e.CommandName = "View" Then
            dtgClaimGoodCondition.SelectedIndex = e.Item.ItemIndex
            Dim objClaimGoodCondition As ClaimGoodCondition = New ClaimGoodConditionFacade(User).Retrieve(CInt(e.CommandArgument))
            sHelper.SetSession("objedit", objClaimGoodCondition)
            txtCode.Text = objClaimGoodCondition.Code
            txtDescription.Text = objClaimGoodCondition.Condition
            btnSimpan.Enabled = False
        End If

    End Sub

    Private Sub DeleteGoodCondition(ByVal nID As Integer)
        Dim iRecordCount As Integer = 0

        Dim objClaimGoodCondition As ClaimGoodCondition = New ClaimGoodConditionFacade(User).Retrieve(nID)

        If objClaimGoodCondition.ClaimDetails.Count > 0 Or objClaimGoodCondition.ClaimHeaders.Count > 0 Then
            MessageBox.Show(SR.DeleteFail)
            Exit Sub
        End If

        Dim objClaimGoodConditionFacade As ClaimGoodConditionFacade = New ClaimGoodConditionFacade(User)
        objClaimGoodConditionFacade.DeleteFromDB(objClaimGoodCondition)

        BindDataGrid(dtgClaimGoodCondition.CurrentPageIndex)


    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub dtgClaimGoodCondition_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgClaimGoodCondition.SortCommand
        If e.SortExpression = sHelper.GetSession("SortCol") Then
            If sHelper.GetSession("SortDirection") = Sort.SortDirection.ASC Then
                sHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
            Else
                sHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        sHelper.SetSession("SortCol", e.SortExpression)
        BindDataGrid(0)
    End Sub
End Class
