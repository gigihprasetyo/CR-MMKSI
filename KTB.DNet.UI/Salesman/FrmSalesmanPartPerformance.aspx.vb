Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Salesman

Public Class FrmSalesmanPartPerformance
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dgSalesmanPerformance As System.Web.UI.WebControls.DataGrid

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
    Private arlPerformance As New ArrayList
    Private objDealer As Dealer
    Private oSalesFacade As New SalesmanHeaderFacade(User)
    Private oSalesPerformanceFacade As New SalesmanPartPerformanceFacade(User)
#End Region

#Region "Privilege"
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.Lihat_salesman_performance_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Part Employee - Entry Salesman Performance")
        End If
    End Sub
    Dim Priv_Input As Boolean = SecurityProvider.Authorize(context.User, SR.Input_salesman_performance_privilege)
    Dim Priv_Edit As Boolean = SecurityProvider.Authorize(context.User, SR.Edit_salesman_performance_privilege)
#End Region

#Region "Event handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        BindControlsAttribute()
        objDealer = (CType(sessHelper.GetSession("Dealer"), Dealer))
        If Not IsPostBack Then
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                txtDealerCode.Text = objDealer.DealerCode
                txtDealerCode.Enabled = False
                lblSearchDealer.Visible = False


            End If
            btnSimpan.Enabled = False
            BindDropdownlist()
        End If

    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        sessHelper.SetSession("EmpPerformance", Nothing)
        BindEmployeePerformance(0)


    End Sub

    Private Sub dgSalesmanPerformance_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesmanPerformance.ItemCommand
        Select Case (e.CommandName)
            Case "add"
                AddCommand(e)
            Case "update"
                UpdateCommand(e)
            Case "delete"
                Dim lShouldReturn As Boolean
                DeleteCommand(e, lShouldReturn)
                If lShouldReturn Then
                    Return
                End If
        End Select
    End Sub

    Private Sub dgSalesmanPerformance_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSalesmanPerformance.PageIndexChanged
        dgSalesmanPerformance.CurrentPageIndex = e.NewPageIndex
        BindEmployeePerformance(dgSalesmanPerformance.CurrentPageIndex)
    End Sub

    Private Sub dgSalesmanPerformance_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSalesmanPerformance.ItemDataBound

        Dim lblSalesmanCodeF As Label
        Dim lblSalesmanCodeE As Label
        Dim txtSalesmanCodeF_T As TextBox
        Dim txtSalesmanCodeE_T As TextBox
        If e.Item.ItemType = ListItemType.Footer Then
            lblSalesmanCodeF = CType(e.Item.FindControl("lblSalesmanCodeF"), Label)
            lblSalesmanCodeF.Attributes("onclick") = "ShowSalesmanPart(this);"

            txtSalesmanCodeF_T = CType(e.Item.FindControl("txtSalesmanCodeF"), TextBox)
            txtSalesmanCodeF_T.Attributes.Add("readonly", "readonly")
            e.Item.Visible = Priv_Input
        ElseIf e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            lblSalesmanCodeE = CType(e.Item.FindControl("lblSalesmanCodeE"), Label)
            lblSalesmanCodeE.Attributes("onclick") = "ShowSalesmanPart(this);"
            txtSalesmanCodeE_T = CType(e.Item.FindControl("txtSalesmanCodeE"), TextBox)
            txtSalesmanCodeE_T.Attributes.Add("readonly", "readonly")
            lblSalesmanCodeE.Visible = False
        End If
        If Not e.Item.DataItem Is Nothing Then
            Dim objSalesmanPartPerformance As SalesmanPartPerformance = e.Item.DataItem
            If e.Item.ItemType = ListItemType.Footer Then
                lblSalesmanCodeF = CType(e.Item.FindControl("lblSalesmanCodeF"), Label)
                lblSalesmanCodeF.Attributes("onclick") = "ShowSalesmanPart(this);"
            ElseIf e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                lblSalesmanCodeE = CType(e.Item.FindControl("lblSalesmanCodeE"), Label)
                lblSalesmanCodeE.Attributes("onclick") = "ShowSalesmanPart(this);"
                Dim lbtnHistory As LinkButton = CType(e.Item.FindControl("lbtnHistoryE"), LinkButton)
                lbtnHistory.Attributes.Add("onclick", String.Format("ShowPopUpHistory({0});return false;", objSalesmanPartPerformance.ID))
            Else
                Dim lbtnHistory As LinkButton = CType(e.Item.FindControl("lbtnHistory"), LinkButton)
                lbtnHistory.Attributes.Add("onclick", String.Format("ShowPopUpHistory({0});return false;", objSalesmanPartPerformance.ID))

                Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

                lbtnEdit.Visible = Priv_Edit
                lbtnDelete.Visible = Priv_Edit

                If objSalesmanPartPerformance.SalesmanHeader.Status = EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif Then
                    lbtnEdit.Visible = False
                    lbtnDelete.Visible = False
                End If
            End If

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            lblStatus.Text = CType(objSalesmanPartPerformance.SalesmanHeader.Status, EnumSalesmanStatus.SalesmanStatus).ToString.Replace("_", " ")

        End If
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                Dim txtSalesmanCodeE As TextBox = CType(e.Item.FindControl("txtSalesmanCodeE"), TextBox)
                txtSalesmanCodeE.Enabled = False
                lblSalesmanCodeE.Visible = False
            End If
            If e.Item.ItemType = ListItemType.Footer Then
                e.Item.Visible = False
                lblSalesmanCodeF.Visible = False
            End If
        End If
    End Sub

    Private Sub dgSalesmanPerformance_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesmanPerformance.EditCommand
        dgSalesmanPerformance.ShowFooter = False
        dgSalesmanPerformance.EditItemIndex = CInt(e.Item.ItemIndex)
        BindEmployeePerformance(dgSalesmanPerformance.CurrentPageIndex)
    End Sub

    Private Sub dgSalesmanPerformance_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesmanPerformance.CancelCommand
        dgSalesmanPerformance.ShowFooter = True
        dgSalesmanPerformance.EditItemIndex = -1
        BindEmployeePerformance(dgSalesmanPerformance.CurrentPageIndex)
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim nResult As Integer = 0
        Dim arl As ArrayList = sessHelper.GetSession("EmpPerformance")
        If arl.Count > 0 Then
            Try
                For Each item As SalesmanPartPerformance In arl
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanPartPerformance), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SalesmanPartPerformance), "SalesmanHeader.ID", MatchType.Exact, item.SalesmanHeader.ID))
                    criterias.opAnd(New Criteria(GetType(SalesmanPartPerformance), "Year", MatchType.Exact, ddlYear.SelectedValue))
                    criterias.opAnd(New Criteria(GetType(SalesmanPartPerformance), "Month", MatchType.Exact, ddlMonth.SelectedValue))

                    Dim arlSalesPerformance As ArrayList = oSalesPerformanceFacade.RetrieveByCriteria(criterias)
                    If arlSalesPerformance.Count > 0 Then
                        nResult = oSalesPerformanceFacade.Update(item)
                    Else
                        nResult = oSalesPerformanceFacade.Insert(item)
                        item.ID = nResult
                    End If
                    If InsertHistory(item) < 0 Then
                        ' Message
                    End If
                Next

                MessageBox.Show(SR.SaveSuccess)
            Catch ex As Exception
                MessageBox.Show(SR.SaveFail)
            End Try
        Else
            MessageBox.Show("Tidak ada data Sales Performance")
            Exit Sub
        End If

    End Sub

#End Region

#Region "Custom"

    Private Sub BindControlsAttribute()
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
    End Sub

    Private Sub BindDropdownlist()
        Try
            ddlMonth.Items.Clear()
            ddlMonth.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            For Each item As ListItem In LookUp.ArrayMonth()
                item.Selected = False
                ddlMonth.Items.Add(item)
            Next
            ddlMonth.ClearSelection()
            ddlMonth.SelectedIndex = CType(Format(DateTime.Now, "MM").ToString, Integer)
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlSPKMonth1, silahkan kirim error ini ke dnet admin")
        End Try

        Try
            ddlYear.Items.Clear()
            ddlYear.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            For Each item As ListItem In LookUp.ArrayYear(True, 2, 8, Date.Now.Year.ToString)
                item.Selected = False
                ddlYear.Items.Add(item)
            Next
            ddlYear.ClearSelection()
            ddlYear.SelectedValue = DateTime.Now.Year.ToString
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlSPKYear1, silahkan kirim error ini ke dnet admin")
        End Try
    End Sub

    Private Sub BindEmployeePerformance()
        BindEmployeePerformance(0)
        dgSalesmanPerformance.ShowFooter = True
        btnSimpan.Enabled = True
    End Sub

    Private Sub BindEmployeePerformance(ByVal idxPage As Integer)
        If IsValidParameter() Then
            Dim totalRow As Integer = 0
            If sessHelper.GetSession("EmpPerformance") Is Nothing Then
                Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanPartPerformance), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                If txtDealerCode.Text.Trim <> String.Empty Then
                    Dim strKodeDealerIn As String = "('" & txtDealerCode.Text.Trim().Replace(";", "','") & "')"
                    criterias.opAnd(New Criteria(GetType(SalesmanPartPerformance), "SalesmanHeader.Dealer.DealerCode", MatchType.InSet, strKodeDealerIn))
                End If
                'criterias.opAnd(New Criteria(GetType(SalesmanPartPerformance), "SalesmanHeader.Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Trim))
                criterias.opAnd(New Criteria(GetType(SalesmanPartPerformance), "Year", MatchType.Exact, ddlYear.SelectedValue))
                criterias.opAnd(New Criteria(GetType(SalesmanPartPerformance), "Month", MatchType.Exact, ddlMonth.SelectedValue))

                arlPerformance = New SalesmanPartPerformanceFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dgSalesmanPerformance.PageSize, totalRow, _
                sessHelper.GetSession("SortColTarget"), sessHelper.GetSession("SortDirectionTarget"))

            Else
                arlPerformance = sessHelper.GetSession("EmpPerformance")
            End If

            dgSalesmanPerformance.CurrentPageIndex = idxPage
            dgSalesmanPerformance.DataSource = arlPerformance
            dgSalesmanPerformance.VirtualItemCount = totalRow
            dgSalesmanPerformance.DataBind()

            dgSalesmanPerformance.ShowFooter = True
            btnSimpan.Enabled = True
            sessHelper.SetSession("EmpPerformance", arlPerformance)
        End If

    End Sub

    Private Function IsValidParameter() As Boolean
        'If txtDealerCode.Text.Trim = String.Empty Then
        '    MessageBox.Show("Tentukan Dealer terlebih dahulu")
        '    Return False
        'End If
        If ddlMonth.SelectedValue = -1 Then
            MessageBox.Show("Tentukan bulan terlebih dahulu")
            Return False
        End If
        If ddlYear.SelectedValue = -1 Then
            MessageBox.Show("Tentukan tahun terlebih dahulu")
            Return False
        End If
        Return True
    End Function

    Private Sub AddCommand(ByVal e As DataGridCommandEventArgs)
        If IsValidParameter() Then
            Dim arl As ArrayList = sessHelper.GetSession("EmpPerformance")

            Dim txtSalesmanCode As TextBox = e.Item.FindControl("txtSalesmanCodeF")
            Dim txtHargaJual As TextBox = e.Item.FindControl("txtHargaJualF")
            Dim txtHargaPokok As TextBox = e.Item.FindControl("txtHargaPokokF")
            'Dim txtIncentive As TextBox = e.Item.FindControl("txtIncentiveF")


            If txtSalesmanCode.Text.Trim = "" Then
                MessageBox.Show("Kode salesman tidak boleh kosong")
                Exit Sub
            End If
            If txtHargaJual.Text.Trim = "" Then
                MessageBox.Show("Nilai Harga Jual tidak boleh kosong")
                Exit Sub
            End If
            If txtHargaPokok.Text.Trim = "" Then
                MessageBox.Show("Nilai Harga Pokok tidak boleh kosong")
                Exit Sub
            End If

            'If txtIncentive.Text.Trim = "" Then
            '    MessageBox.Show("Nilai Incentive tidak boleh kosong")
            '    Exit Sub
            'End If

            Dim objSales As SalesmanHeader = New SalesmanHeaderFacade(User).RetrieveByCode(txtSalesmanCode.Text)
            If Not objSales Is Nothing Then
                Dim objPerformance As New SalesmanPartPerformance

                objPerformance.SalesmanHeader = objSales
                objPerformance.Year = ddlYear.SelectedValue
                objPerformance.Month = ddlMonth.SelectedValue
                objPerformance.Period = New Date(CType(ddlYear.SelectedValue, Integer), CType(ddlMonth.SelectedValue, Integer), 1, 0, 0, 0)
                objPerformance.HargaJual = CType(txtHargaJual.Text.Trim, Decimal)
                objPerformance.HargaPokok = CType(txtHargaPokok.Text.Trim, Decimal)
                objPerformance.Profit = objPerformance.HargaJual - objPerformance.HargaPokok
                If objPerformance.HargaJual > 0 Then
                    objPerformance.Percentage = System.Math.Round((objPerformance.Profit / objPerformance.HargaJual) * 100, 2)
                Else
                    objPerformance.Percentage = 0
                End If

                'Insert the third person, but first check if they already exists.
                Dim isExist As Boolean = False
                For Each item As SalesmanPartPerformance In arl
                    If item.SalesmanHeader.SalesmanCode = objPerformance.SalesmanHeader.SalesmanCode Then
                        isExist = True
                    End If
                Next
                If Not isExist Then
                    arl.Add(objPerformance)
                    BindEmployeePerformance(dgSalesmanPerformance.CurrentPageIndex())
                Else
                    MessageBox.Show("Kode salesman sudah ada di daftar")
                End If

                'If Not arl.Contains(objPerformance) Then
                '    arl.Add(objPerformance)
                '    BindEmployeePerformance(dgSalesmanPerformance.CurrentPageIndex())
                'Else
                '    MessageBox.Show("Kode salesman sudah ada di daftar")
                'End If


            Else
                MessageBox.Show("Tidak ada Salesman dengan kode tersebut")
            End If

        End If

    End Sub

    Private Sub UpdateCommand(ByVal e As DataGridCommandEventArgs)
        If IsValidParameter() Then
            Dim arl As ArrayList = sessHelper.GetSession("EmpPerformance")

            Dim txtSalesmanCode As TextBox = e.Item.FindControl("txtSalesmanCodeE")
            Dim txtHargaJual As TextBox = e.Item.FindControl("txtHargaJualE")
            Dim txtHargaPokok As TextBox = e.Item.FindControl("txtHargaPokokE")
           
            If txtSalesmanCode.Text.Trim = "" Then
                MessageBox.Show("Kode salesman tidak boleh kosong")
                Exit Sub
            End If
            If txtHargaJual.Text.Trim = "" Then
                MessageBox.Show("Nilai Harga Jual tidak boleh kosong")
                Exit Sub
            End If
            If txtHargaPokok.Text.Trim = "" Then
                MessageBox.Show("Nilai Harga Pokok tidak boleh kosong")
                Exit Sub
            End If

            'If txtIncentive.Text.Trim = "" Then
            '    MessageBox.Show("Nilai Incentive tidak boleh kosong")
            '    Exit Sub
            'End If

            Dim objSales As SalesmanHeader = New SalesmanHeaderFacade(User).RetrieveByCode(txtSalesmanCode.Text)
            If Not objSales Is Nothing Then
                Dim objPerformance As New SalesmanPartPerformance

                objPerformance = CType(arl.Item(e.Item.ItemIndex), SalesmanPartPerformance)
                objPerformance.Year = ddlYear.SelectedValue
                objPerformance.Month = ddlMonth.SelectedValue
                objPerformance.Period = New Date(CType(ddlYear.SelectedValue, Integer), CType(ddlMonth.SelectedValue, Integer), 1, 0, 0, 0)
                objPerformance.HargaJual = CType(txtHargaJual.Text.Trim, Decimal)
                objPerformance.HargaPokok = CType(txtHargaPokok.Text.Trim, Decimal)
                objPerformance.Profit = objPerformance.HargaJual - objPerformance.HargaPokok
                If objPerformance.HargaJual > 0 Then
                    objPerformance.Percentage = System.Math.Round((objPerformance.Profit / objPerformance.HargaJual) * 100, 2)
                Else
                    objPerformance.Percentage = 0
                End If

                arl.Item(e.Item.ItemIndex) = objPerformance

                dgSalesmanPerformance.ShowFooter = True
                dgSalesmanPerformance.EditItemIndex = -1
                BindEmployeePerformance(dgSalesmanPerformance.CurrentPageIndex)
            Else
                MessageBox.Show("Tidak ada Salesman dengan kode tersebut")
            End If

        End If

    End Sub

    Private Sub DeleteCommand(ByVal e As DataGridCommandEventArgs, ByRef shouldReturn As Boolean)
        shouldReturn = False
        Dim lblSalesmanCode As Label = e.Item.FindControl("lblSalesmanCode")

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanPartPerformance), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanPartPerformance), "SalesmanHeader.SalesmanCode", MatchType.Exact, lblSalesmanCode.Text.Trim))
        criterias.opAnd(New Criteria(GetType(SalesmanPartPerformance), "SalesmanHeader.Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Trim))
        criterias.opAnd(New Criteria(GetType(SalesmanPartPerformance), "Year", MatchType.Exact, ddlYear.SelectedValue))
        criterias.opAnd(New Criteria(GetType(SalesmanPartPerformance), "Month", MatchType.Exact, ddlMonth.SelectedValue))

        Dim arlSalesPerformance As ArrayList = oSalesPerformanceFacade.RetrieveByCriteria(criterias)
        If arlSalesPerformance.Count > 0 Then
            Dim objSalesPerformance As SalesmanPartPerformance = arlSalesPerformance(0)
            oSalesPerformanceFacade.Delete(objSalesPerformance)
        End If
        Dim arl As ArrayList = sessHelper.GetSession("EmpPerformance")
        arl.RemoveAt(e.Item.ItemIndex)
        BindEmployeePerformance(dgSalesmanPerformance.CurrentPageIndex)
    End Sub

    Private Function InsertHistory(ByVal objSalesmanPartPerformance As SalesmanPartPerformance) As Integer
        Try

            Dim objSalesmanPartPerformanceHistFacade As New SalesmanPartPerformanceHistFacade(User)

            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanPartPerformanceHist), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanPartPerformanceHist), "SalesmanPartPerformance.ID", MatchType.Exact, objSalesmanPartPerformance.ID))
            criterias.opAnd(New Criteria(GetType(SalesmanPartPerformanceHist), "HargaJual", MatchType.Exact, CDec(objSalesmanPartPerformance.HargaJual)))
            criterias.opAnd(New Criteria(GetType(SalesmanPartPerformanceHist), "HargaPokok", MatchType.Exact, CDec(objSalesmanPartPerformance.HargaPokok)))
            criterias.opAnd(New Criteria(GetType(SalesmanPartPerformanceHist), "Profit", MatchType.Exact, CDec(objSalesmanPartPerformance.Profit)))

            Dim objSortCol As SortCollection = New SortCollection
            objSortCol.Add(New Sort(GetType(SalesmanPartPerformanceHist), "CreatedTime", Sort.SortDirection.DESC))


            Dim arlSalesTarget As ArrayList = objSalesmanPartPerformanceHistFacade.Retrieve(criterias, objSortCol)
            If arlSalesTarget.Count <= 0 Then
                Dim objHist As New SalesmanPartPerformanceHist
                objHist.SalesmanPartPerformance = objSalesmanPartPerformance
                objHist.HargaPokok = objSalesmanPartPerformance.HargaPokok
                objHist.HargaJual = objSalesmanPartPerformance.HargaJual
                objHist.Profit = objSalesmanPartPerformance.Profit
                objHist.Percentage = objSalesmanPartPerformance.Percentage

                Return objSalesmanPartPerformanceHistFacade.Insert(objHist)
            Else
                Dim oHist As SalesmanPartPerformanceHist = CType(arlSalesTarget(0), SalesmanPartPerformanceHist)

                If Not (oHist.HargaJual = objSalesmanPartPerformance.HargaJual And oHist.HargaPokok = objSalesmanPartPerformance.HargaPokok) Then
                    Dim objHist As New SalesmanPartPerformanceHist
                    objHist.SalesmanPartPerformance = objSalesmanPartPerformance
                    objHist.HargaPokok = objSalesmanPartPerformance.HargaPokok
                    objHist.HargaJual = objSalesmanPartPerformance.HargaJual
                    objHist.Profit = objSalesmanPartPerformance.Profit
                    objHist.Percentage = objSalesmanPartPerformance.Percentage
                Else
                    Return 0
                End If
            End If

        Catch ex As Exception

        End Try

    End Function
#End Region


End Class
