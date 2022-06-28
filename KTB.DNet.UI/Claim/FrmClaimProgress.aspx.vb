Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Claim
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security

Public Class FrmClaimProgress
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents dtgClaimProgress As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtProgress As System.Web.UI.WebControls.TextBox
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

#Region "Deklarasi"
    Private criterias As CriteriaComposite
    Dim sHelper As New SessionHelper
#End Region

#Region "Custom Method"
    Private Sub BindToGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If indexPage >= 0 Then

            criterias = New CriteriaComposite(New Criteria(GetType(ClaimProgress), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ClaimProgress), "Progress", MatchType.No, ""))
            Dim arlClaimProgress As ArrayList = New ClaimProgressFacade(User).RetrieveActiveList(criterias, indexPage + 1, dtgClaimProgress.PageSize, totalRow, viewstate("SortColCP"), viewstate("SortDirectionCP"))
            dtgClaimProgress.DataSource = arlClaimProgress
            dtgClaimProgress.VirtualItemCount = totalRow
            If indexPage = 0 Then
                dtgClaimProgress.CurrentPageIndex = 0
            End If

            dtgClaimProgress.DataBind()
        End If
    End Sub

    Private Sub MapToControl(ByVal objDomain As ClaimProgress)
        txtProgress.Text = objDomain.Progress
    End Sub

    Private Sub ClearControl()
        txtProgress.Text = ""
    End Sub

#End Region

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ClaimProgressParameterView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=CLAIM - Claim Progress Parameter")
        End If
    End Sub

    Private Function CekBtnPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.ClaimProgressParameterCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            viewstate.Add("SortColCP", "Progress")
            viewstate.Add("SortDirectionCP", Sort.SortDirection.ASC)
            BindToGrid(0)
        End If
        If CekBtnPriv() Then
            btnSimpan.Enabled = True
        Else
            btnSimpan.Enabled = False
        End If
    End Sub

    Private Sub dtgClaimProgress_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgClaimProgress.PageIndexChanged
        dtgClaimProgress.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgClaimProgress.CurrentPageIndex)
    End Sub

    Private Sub dtgClaimProgress_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgClaimProgress.SortCommand
        If e.SortExpression = viewstate("SortColCP") Then
            If viewstate("SortDirectionCP") = Sort.SortDirection.ASC Then
                viewstate.Add("SortDirectionCP", Sort.SortDirection.DESC)
            Else
                viewstate.Add("SortDirectionCP", Sort.SortDirection.ASC)
            End If
        End If
        viewstate.Add("SortColCP", e.SortExpression)
        BindToGrid(0)
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim status As String
        Dim progress As String
        Dim objClaimProgressFacade As New ClaimProgressFacade(User)

        If txtProgress.Text <> String.Empty Then
            progress = txtProgress.Text.Trim

            If viewstate("SaveStatus") = "Edit" Then
                'update
                Dim objClaimProgress As ClaimProgress = sHelper.GetSession("objDomainClaimStatus")
                objClaimProgress.Progress = progress

                If objClaimProgressFacade.ValidateProgress(progress.ToLower) > 0 Then
                    MessageBox.Show("Progress: " & progress & " sudah pernah dibuat")
                Else
                    Try
                        Dim iresult As Integer = New ClaimProgressFacade(User).Update(objClaimProgress)
                        If iresult <> -1 Then
                            BindToGrid(0)
                            ClearControl()
                            MessageBox.Show("Update Berhasil")

                        Else
                            MessageBox.Show("Update gagal")
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Update gagal")
                    End Try
                    viewstate.Add("SaveStatus", "Add")
                End If
            Else
                'insert
                Dim objClaimProgress As New ClaimProgress
                objClaimProgress.Progress = progress

                If objClaimProgressFacade.ValidateProgress(progress.ToLower) > 0 Then
                    MessageBox.Show("Progress: " & progress & " sudah pernah dibuat")
                Else
                    Try
                        Dim iresult As Integer = New ClaimProgressFacade(User).Insert(objClaimProgress)
                        BindToGrid(0)
                        ClearControl()
                        MessageBox.Show("Insert Berhasil")
                    Catch ex As Exception
                        MessageBox.Show("Insert gagal")
                    End Try
                End If
            End If
        Else
            MessageBox.Show("Field progress tidak boleh kosong")
        End If
    End Sub

    Private Sub dtgClaimProgress_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClaimProgress.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1

            Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            Dim _lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim _lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            If CekBtnPriv() Then
                _lbtnDelete.Visible = True
                _lbtnEdit.Visible = True
                _lbtnView.Visible = True
            Else
                _lbtnDelete.Visible = False
                _lbtnEdit.Visible = False
                _lbtnView.Visible = False
            End If
        End If
    End Sub

    Private Sub dtgClaimProgress_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgClaimProgress.ItemCommand
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim objDomain As ClaimProgress = New ClaimProgressFacade(User).Retrieve(CInt(lblID.Text))

            If e.CommandName = "View" Then
                viewstate.Add("SaveStatus", "View")
                btnBack.Visible = True
                btnSimpan.Enabled = False
                MapToControl(objDomain)
            ElseIf e.CommandName = "Edit" Then
                viewstate.Add("SaveStatus", "Edit")
                btnBack.Visible = True
                sHelper.SetSession("objDomainClaimStatus", objDomain)
                MapToControl(objDomain)
            ElseIf e.CommandName = "Delete" Then
                DeleteClaim(lblID.Text)
            End If
        End If
    End Sub

    Private Function CekProgressIDTransaction(ByVal objDomain As ClaimProgress) As Integer
        Dim nresult As Integer = 1
        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimHeader), "ClaimProgress.ID", MatchType.Exact, objDomain.ID))
        Dim arlData As ArrayList = New ClaimHeaderFacade(User).Retrieve(crits)
        If arlData.Count > 0 Then
            nresult = -1
        End If

        Return nresult
    End Function
    'todo: add cek to transaction for delete
    Private Sub DeleteClaim(ByVal nID As Integer)
        Dim iRecordCount As Integer = 0
        Dim oClaimProgress As ClaimProgress = New ClaimProgressFacade(User).Retrieve(nID)
        If CekProgressIDTransaction(oClaimProgress) = -1 Then
            MessageBox.Show("Tidak bisa menghapus data!\n Progress ID sudah dipakai dalam Transaksi")
        Else
            Dim oFacade As ClaimProgressFacade = New ClaimProgressFacade(User)
            oFacade.DeleteFromDB(oClaimProgress)
        End If

        BindToGrid(dtgClaimProgress.CurrentPageIndex)
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        txtProgress.Enabled = True
        txtProgress.Text = ""
        btnSimpan.Enabled = True
    End Sub
End Class
