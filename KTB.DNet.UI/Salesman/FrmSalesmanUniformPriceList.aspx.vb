#Region "Cutom Namespace Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security

#End Region

Public Class FrmSalesmanUniformPriceList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents txtHargaNormal As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHargaDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgSalesmanUniformPriceList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlUnifDistributionCode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button

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
    Private _SalesmanUniformFacade As New SalesmanUniformFacade(User)
    Private _createPriv As Boolean = False
    Private sessHelper As New SessionHelper

#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        _createPriv = CheckCreatePrivilege()
        btnSimpan.Visible = _createPriv
        btnCancel.Visible = _createPriv
        If Not IsPostBack Then
            BindDropDownLists()
            Initialize()
            BindDataGrid(0)
        End If
    End Sub
    Private Sub dgSalesmanUniformPriceList_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesmanUniformPriceList.ItemCommand
        If e.CommandName = "Edit" Then
            View(CInt(e.Item.Cells(0).Text), True)
            ViewState.Add("vsProcess", "Edit")
            dgSalesmanUniformPriceList.SelectedIndex = e.Item.ItemIndex
            ' btnCancel.Visible = True
        ElseIf e.CommandName = "Delete" Then
            Delete(e.Item.Cells(0).Text)
        End If
    End Sub

    Private Sub dgSalesmanUniformPriceList_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSalesmanUniformPriceList.PageIndexChanged
        dgSalesmanUniformPriceList.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgSalesmanUniformPriceList.CurrentPageIndex)
    End Sub

    Private Sub dgSalesmanUniformPriceList_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSalesmanUniformPriceList.ItemDataBound
        If e.Item.ItemIndex >= 0 Then
            Dim objSalesmanUniform As SalesmanUniform = e.Item.DataItem
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgSalesmanUniformPriceList.CurrentPageIndex * dgSalesmanUniformPriceList.PageSize)

            Dim lblSalesmanUnifDistributionCodeNew As Label = CType(e.Item.FindControl("lblSalesmanUnifDistributionCode"), Label)
            lblSalesmanUnifDistributionCodeNew.Text = objSalesmanUniform.SalesmanUnifDistribution.SalesmanUnifDistributionCode

            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dibatalkan status resignnya?');")
                lbtnDelete.Visible = _createPriv
            End If

            If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
                Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                lbtnEdit.Visible = _createPriv
            End If
        End If
    End Sub
    Private Sub dgSalesmanUniformPriceList_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSalesmanUniformPriceList.SortCommand
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
        dgSalesmanUniformPriceList.SelectedIndex = -1
        dgSalesmanUniformPriceList.CurrentPageIndex = 0
        BindDataGrid(dgSalesmanUniformPriceList.CurrentPageIndex)
    End Sub


    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim objSalesmanUniform As New SalesmanUniform
        Dim status As String = CType(ViewState("vsProcess"), String)
        Dim nResult As Integer = -1

        If ValidateSave() Then
            Dim totalRow As Integer = 0
            Dim arrList As New ArrayList
            Select Case status
                Case "Default"
                    ' uniform distribution & uniform must identic
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanUniform), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    If ddlUnifDistributionCode.SelectedValue <> "-1" Then
                        criterias.opAnd(New Criteria(GetType(SalesmanUniform), "SalesmanUnifDistribution.SalesmanUnifDistributionCode", MatchType.Exact, ddlUnifDistributionCode.SelectedItem.Text))
                    End If

                    arrList = _SalesmanUniformFacade.RetrieveByCriteria(criterias, 1, dgSalesmanUniformPriceList.PageSize, totalRow, _
                            CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

                    If arrList.Count > 0 Then
                        MessageBox.Show("Kode Pesanan tersebut sudah ada sebelumnya, Silakan pilih dengan Kode Pesanan yang lainnya untuk insert")
                        Return
                    End If

                    objSalesmanUniform = New SalesmanUniform
                    objSalesmanUniform.SalesmanUnifDistribution = New SalesmanUnifDistributionFacade(User).Retrieve(ddlUnifDistributionCode.SelectedItem.Text)
                    'objSalesmanUniform.Description = txtKeterangan.Text
                    'objSalesmanUniform.SalesmanUniformCode = txtKodeSeragam.Text
                    objSalesmanUniform.NormalPrice = Convert.ToDecimal(txtHargaNormal.Text)
                    objSalesmanUniform.DealerPrice = Convert.ToDecimal(txtHargaDealer.Text)

                    nResult = _SalesmanUniformFacade.Insert(objSalesmanUniform)
                Case "Edit"
                    objSalesmanUniform = CType(sessHelper.GetSession("vsSalesmanUniform"), SalesmanUniform)

                    ' uniform distribution & uniform must identic
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanUniform), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    If ddlUnifDistributionCode.SelectedValue <> "-1" Then
                        criterias.opAnd(New Criteria(GetType(SalesmanUniform), "SalesmanUnifDistribution.SalesmanUnifDistributionCode", MatchType.Exact, ddlUnifDistributionCode.SelectedItem.Text))
                    End If
                    'criterias.opAnd(New Criteria(GetType(SalesmanUniform), "SalesmanUniformCode", MatchType.Exact, txtKodeSeragam.Text))
                    criterias.opAnd(New Criteria(GetType(SalesmanUniform), "ID", MatchType.No, objSalesmanUniform.ID))

                    arrList = _SalesmanUniformFacade.RetrieveByCriteria(criterias, 1, dgSalesmanUniformPriceList.PageSize, totalRow, _
                            CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

                    If arrList.Count > 0 Then
                        MessageBox.Show("Kode Pesanan tersebut sudah ada sebelumnya, Silakan pilih dengan Kode Pesanan yang lainnya untuk update")
                        Return
                    End If


                    objSalesmanUniform.SalesmanUnifDistribution = New SalesmanUnifDistributionFacade(User).Retrieve(ddlUnifDistributionCode.SelectedItem.Text)
                    objSalesmanUniform.NormalPrice = Convert.ToDecimal(txtHargaNormal.Text)
                    objSalesmanUniform.DealerPrice = Convert.ToDecimal(txtHargaDealer.Text)
                    nResult = _SalesmanUniformFacade.Update(objSalesmanUniform)
            End Select

            If nResult > 0 Then
                MessageBox.Show(SR.SaveSuccess)
                ViewState.Add("vsProcess", "Default")
                ClearData()
                dgSalesmanUniformPriceList.CurrentPageIndex = 0
                BindDataGrid(dgSalesmanUniformPriceList.CurrentPageIndex)
            Else
                MessageBox.Show(SR.SaveFail)
            End If

        End If


    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClearData()
        ViewState.Add("vsProcess", "Default")
    End Sub
#End Region

#Region "Need To Add"
    Private Function ValidateSave() As Boolean
        If ddlUnifDistributionCode.SelectedIndex = 0 Then
            MessageBox.Show("Silakan pilih kode pembagian seragam")
            Return False
        End If
        If txtHargaNormal.Text = String.Empty Then
            MessageBox.Show("Silakan masukan harga normal")
            Return False
        Else
            If Not IsNumeric(txtHargaNormal.Text) Then
                MessageBox.Show("Harga harus numeric")
                Return False
            Else
                If CInt(txtHargaNormal.Text) < 0 Then
                    MessageBox.Show("Harga tidak boleh minus")
                    Return False
                End If
            End If

        End If

        If txtHargaDealer.Text = String.Empty Then
            MessageBox.Show("Silakan masukan harga dealer")
            Return False
        Else
            If Not IsNumeric(txtHargaDealer.Text) Then
                MessageBox.Show("Harga harus numeric")
                Return False
            Else
                If CInt(txtHargaDealer.Text) < 0 Then
                    MessageBox.Show("Harga tidak boleh minus")
                    Return False
                End If
            End If

        End If
        Return True
    End Function

    Private Sub ClearData()
        ddlUnifDistributionCode.SelectedIndex = 0
        txtHargaNormal.Text = String.Empty
        txtHargaDealer.Text = String.Empty
    End Sub
    Private Sub Initialize()
        ClearData()
        ViewState("CurrentSortColumn") = "SalesmanUniformCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ViewState.Add("vsProcess", "Default")
    End Sub

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanUniform), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        arrList = _SalesmanUniformFacade.RetrieveActiveList(idxPage + 1, dgSalesmanUniformPriceList.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgSalesmanUniformPriceList.DataSource = arrList
        dgSalesmanUniformPriceList.VirtualItemCount = totalRow
        If arrList.Count > 0 Then
            dgSalesmanUniformPriceList.DataBind()
        End If
    End Sub

    Private Sub BindDropDownLists()
        CommonFunction.BindSalesmanUnifDistributionCode(ddlUnifDistributionCode, Me.User, True)
    End Sub

    Private Sub Delete(ByVal nID As Integer)
        Dim objSalesmanUniform As SalesmanUniform = _SalesmanUniformFacade.Retrieve(nID)
        If Not objSalesmanUniform Is Nothing Then
            Try
                If _SalesmanUniformFacade.DeleteFromDB(objSalesmanUniform) <= 0 Then
                    MessageBox.Show(SR.DeleteFail)
                Else
                    MessageBox.Show(SR.DeleteSucces)
                    ClearData()
                End If
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
        End If

        BindDataGrid(dgSalesmanUniformPriceList.CurrentPageIndex)
    End Sub

    Private Sub View(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objSalesmanUniform As SalesmanUniform = _SalesmanUniformFacade.Retrieve(nID)
        sessHelper.SetSession("vsSalesmanUniform", objSalesmanUniform)

        ddlUnifDistributionCode.SelectedValue = objSalesmanUniform.SalesmanUnifDistribution.ID

        txtHargaNormal.Text = objSalesmanUniform.NormalPrice.ToString("#,##0")
        txtHargaDealer.Text = objSalesmanUniform.DealerPrice.ToString("#,##0")
    End Sub
#End Region

#Region "Privilege"

    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.PengaturanHargaSeragamView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Seragam Tenaga Penjual - Pengaturan Harga Seragam")
        End If
    End Sub
    Private Function CheckCreatePrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.UniformPriceCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region

End Class

