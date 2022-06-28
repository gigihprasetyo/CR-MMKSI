Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Salesman

Public Class FrmSalesmanPartTarget
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents ddlMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgSalesmanTarget As System.Web.UI.WebControls.DataGrid
    Protected WithEvents hideSalesmanID As System.Web.UI.HtmlControls.HtmlInputHidden

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
    Private arlTarget As New ArrayList
    Private objDealer As Dealer
    Private oSalesFacade As New SalesmanHeaderFacade(User)
    Private oSalesTargetFacade As New SalesmanPartTargetFacade(User)
#End Region

#Region "Privilege"
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.Lihat_salesman_target_realisasi_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Part Employee - Entry Salesman Target dan Realisasi")
        End If
    End Sub
    Dim Priv_Input As Boolean = SecurityProvider.Authorize(context.User, SR.Input_salesman_target_realisasi_privilege)
    Dim Priv_Edit_Delete As Boolean = SecurityProvider.Authorize(context.User, SR.Edit_salesman_target_realisasi_privilege)
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
        sessHelper.SetSession("EmpTarget", Nothing)
        BindEmployeeTarget(0)
    End Sub

    Private Sub dgSalesmanTarget_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSalesmanTarget.ItemDataBound

        Dim lblSalesmanCodeF As Label
        Dim lblSalesmanCodeE As Label
        Dim txtSalesmanCodeF_T As TextBox
        Dim txtSalesmanCodeE_T As TextBox
        If e.Item.ItemType = ListItemType.Footer Then
            lblSalesmanCodeF = CType(e.Item.FindControl("lblSalesmanCodeF"), Label)
            lblSalesmanCodeF.Attributes("onclick") = "ShowSalesmanPart(this);"
            e.Item.Visible = Priv_Input
            txtSalesmanCodeF_T = CType(e.Item.FindControl("txtSalesmanCodeF"), TextBox)
            txtSalesmanCodeF_T.Attributes.Add("readonly", "readonly")
        ElseIf e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            lblSalesmanCodeE = CType(e.Item.FindControl("lblSalesmanCodeE"), Label)
            lblSalesmanCodeE.Attributes("onclick") = "ShowSalesmanPart(this);"
            lblSalesmanCodeE.Visible = False
            txtSalesmanCodeE_T = CType(e.Item.FindControl("txtSalesmanCodeE"), TextBox)
            txtSalesmanCodeE_T.Attributes.Add("readonly", "readonly")

        End If
        If Not e.Item.DataItem Is Nothing Then
            Dim objSalesmanPartTarget As SalesmanPartTarget = e.Item.DataItem
            If objSalesmanPartTarget.Realization < objSalesmanPartTarget.Target Then
                e.Item.BackColor = System.Drawing.Color.Yellow
            End If
            If e.Item.ItemType = ListItemType.Footer Then
                lblSalesmanCodeF = CType(e.Item.FindControl("lblSalesmanCodeF"), Label)
                lblSalesmanCodeF.Attributes("onclick") = "ShowSalesmanPart(this);"
            ElseIf e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                Dim lbtnHistory As LinkButton = CType(e.Item.FindControl("lbtnHistoryE"), LinkButton)
                lbtnHistory.Attributes.Add("onclick", String.Format("ShowPopUpHistory({0});return false;", objSalesmanPartTarget.ID))
                If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    If objSalesmanPartTarget.Realization > 0 Then
                        Dim txtRealisasiE As TextBox = CType(e.Item.FindControl("txtRealisasiE"), TextBox)
                        txtRealisasiE.Enabled = False
                    End If
                End If

            Else
                Dim lbtnHistory As LinkButton = CType(e.Item.FindControl("lbtnHistory"), LinkButton)
                lbtnHistory.Attributes.Add("onclick", String.Format("ShowPopUpHistory({0});return false;", objSalesmanPartTarget.ID))

                Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                lbtnEdit.Visible = Priv_Edit_Delete
                lbtnDelete.Visible = Priv_Edit_Delete
                If objSalesmanPartTarget.SalesmanHeader.Status = EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif Then
                    lbtnEdit.Visible = False
                    lbtnDelete.Visible = False
                End If

            End If

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            lblStatus.Text = CType(objSalesmanPartTarget.SalesmanHeader.Status, EnumSalesmanStatus.SalesmanStatus).ToString.Replace("_", " ")

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

    Private Sub dgSalesmanTarget_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesmanTarget.ItemCommand
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
                'Case "edit"
                '    dgSalesmanTarget.ShowFooter = False
                '    dgSalesmanTarget.EditItemIndex = CInt(e.Item.ItemIndex)
                '    BindEmployeeTarget(dgSalesmanTarget.CurrentPageIndex)
                '    EditCommand(e)
        End Select
    End Sub

    Private Sub dgSalesmanTarget_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesmanTarget.EditCommand
        dgSalesmanTarget.ShowFooter = False
        dgSalesmanTarget.EditItemIndex = CInt(e.Item.ItemIndex)
        BindEmployeeTarget(dgSalesmanTarget.CurrentPageIndex)
    End Sub

    Private Sub dgSalesmanTarget_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesmanTarget.CancelCommand
        dgSalesmanTarget.ShowFooter = True
        dgSalesmanTarget.EditItemIndex = -1
        BindEmployeeTarget(dgSalesmanTarget.CurrentPageIndex)
    End Sub

    Private Sub dgSalesmanTarget_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSalesmanTarget.PageIndexChanged
        dgSalesmanTarget.CurrentPageIndex = e.NewPageIndex
        BindEmployeeTarget(dgSalesmanTarget.CurrentPageIndex)
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim nResult As Integer = 0
        Dim arl As ArrayList = sessHelper.GetSession("EmpTarget")
        If arl.Count > 0 Then
            Try
                For Each item As SalesmanPartTarget In arl
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanPartTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SalesmanPartTarget), "SalesmanHeader.ID", MatchType.Exact, item.SalesmanHeader.ID))
                    criterias.opAnd(New Criteria(GetType(SalesmanPartTarget), "Year", MatchType.Exact, ddlYear.SelectedValue))
                    criterias.opAnd(New Criteria(GetType(SalesmanPartTarget), "Month", MatchType.Exact, ddlMonth.SelectedValue))

                    Dim arlSalesTarget As ArrayList = oSalesTargetFacade.RetrieveByCriteria(criterias)
                    If arlSalesTarget.Count > 0 Then
                        nResult = oSalesTargetFacade.Update(item)
                    Else
                        nResult = oSalesTargetFacade.Insert(item)
                        'item = oSalesTargetFacade.Retrieve(nResult)
                        item.ID = nResult
                    End If
                    If InsertHistory(item) < 0 Then
                        ' Message
                    End If
                Next
                MessageBox.Show(SR.SaveSuccess)
                BindEmployeeTarget(dgSalesmanTarget.CurrentPageIndex)
            Catch ex As Exception
                MessageBox.Show(SR.SaveFail)
            End Try
        Else
            MessageBox.Show("Tidak ada data Sales Target")
            Exit Sub
        End If

    End Sub
#End Region

#Region "Custom"

    Private Sub BindControlsAttribute()

        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        'lblShowSalesman.Attributes("onclick") = "ShowSalesmanSelection();"
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

    Private Sub BindEmployeeTarget()
        BindEmployeeTarget(0)
    End Sub

    Private Sub BindEmployeeTarget(ByVal idxPage As Integer)
        If IsValidParameter() Then
            Dim totalRow As Integer = 0
            If sessHelper.GetSession("EmpTarget") Is Nothing Then
                Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanPartTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                If txtDealerCode.Text.Trim <> String.Empty Then
                    Dim strKodeDealerIn As String = "('" & txtDealerCode.Text.Trim().Replace(";", "','") & "')"
                    criterias.opAnd(New Criteria(GetType(SalesmanPartTarget), "SalesmanHeader.Dealer.DealerCode", MatchType.InSet, strKodeDealerIn))
                End If

                criterias.opAnd(New Criteria(GetType(SalesmanPartTarget), "Year", MatchType.Exact, ddlYear.SelectedValue))
                criterias.opAnd(New Criteria(GetType(SalesmanPartTarget), "Month", MatchType.Exact, ddlMonth.SelectedValue))

                arlTarget = New SalesmanPartTargetFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dgSalesmanTarget.PageSize, totalRow, _
                sessHelper.GetSession("SortColTarget"), sessHelper.GetSession("SortDirectionTarget"))

            Else
                arlTarget = sessHelper.GetSession("EmpTarget")
            End If

            dgSalesmanTarget.CurrentPageIndex = idxPage
            dgSalesmanTarget.DataSource = arlTarget
            dgSalesmanTarget.VirtualItemCount = totalRow
            dgSalesmanTarget.DataBind()

            dgSalesmanTarget.ShowFooter = True
            btnSimpan.Enabled = True
            sessHelper.SetSession("EmpTarget", arlTarget)
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
            Dim arl As ArrayList = sessHelper.GetSession("EmpTarget")

            Dim txtSalesmanCode As TextBox = e.Item.FindControl("txtSalesmanCodeF")
            Dim txtTarget As TextBox = e.Item.FindControl("txtTargetF")
            Dim txtRealisasi As TextBox = e.Item.FindControl("txtRealisasiF")
            'Dim lblSalesName As Label = e.Item.FindControl("lblSalesmanName")

            If txtSalesmanCode.Text.Trim = "" Then
                MessageBox.Show("Kode salesman tidak boleh kosong")
                Exit Sub
            End If
            If txtTarget.Text.Trim = "" Then
                MessageBox.Show("Nilai target tidak boleh kosong")
                Exit Sub
            End If
            If txtRealisasi.Text.Trim = "" Then
                MessageBox.Show("Nilai realisasi tidak boleh kosong")
                Exit Sub
            End If

            Dim objSales As SalesmanHeader = New SalesmanHeaderFacade(User).RetrieveByCode(txtSalesmanCode.Text)
            If Not objSales Is Nothing Then
                'lblSalesName.Text = objSales.Name

                Dim objTarget As New SalesmanPartTarget

                objTarget.SalesmanHeader = objSales
                objTarget.Year = ddlYear.SelectedValue
                objTarget.Month = ddlMonth.SelectedValue
                objTarget.Period = New Date(CType(ddlYear.SelectedValue, Integer), CType(ddlMonth.SelectedValue, Integer), 1, 0, 0, 0)
                objTarget.Target = CType(IIf(txtTarget.Text.Trim = "", "0", txtTarget.Text.Trim), Decimal)
                objTarget.Realization = CType(IIf(txtRealisasi.Text.Trim = "", "0", txtRealisasi.Text.Trim), Decimal)
                If objTarget.Target > 0 Then
                    objTarget.Persentage = System.Math.Round((objTarget.Realization / objTarget.Target) * 100, 2)
                Else
                    objTarget.Persentage = 0
                End If

                'Insert the third person, but first check if they already exists.
                Dim isExist As Boolean = False
                For Each item As SalesmanPartTarget In arl
                    If item.SalesmanHeader.SalesmanCode = objTarget.SalesmanHeader.SalesmanCode Then
                        isExist = True
                    End If
                Next
                If Not isExist Then
                    arl.Add(objTarget)
                    BindEmployeeTarget(dgSalesmanTarget.CurrentPageIndex())
                Else
                    MessageBox.Show("Kode salesman sudah ada di daftar")
                End If

                'If Not arl.Contains(objTarget) Then
                '    arl.Add(objTarget)
                '    BindEmployeeTarget(dgSalesmanTarget.CurrentPageIndex())
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
            Dim arl As ArrayList = sessHelper.GetSession("EmpTarget")

            Dim txtSalesmanCode As TextBox = e.Item.FindControl("txtSalesmanCodeE")
            Dim txtTarget As TextBox = e.Item.FindControl("txtTargetE")
            Dim txtRealisasi As TextBox = e.Item.FindControl("txtRealisasiE")
            Dim lblSalesName As Label = e.Item.FindControl("lblSalesmanName")

            If txtSalesmanCode.Text.Trim = "" Then
                MessageBox.Show("Kode salesman tidak boleh kosong")
                Exit Sub
            End If
            If txtTarget.Text.Trim = "" Then
                MessageBox.Show("Nilai target tidak boleh kosong")
                Exit Sub
            End If
            If txtRealisasi.Text.Trim = "" Then
                MessageBox.Show("Nilai realisasi tidak boleh kosong")
                Exit Sub
            End If

            Dim objSales As SalesmanHeader = New SalesmanHeaderFacade(User).RetrieveByCode(txtSalesmanCode.Text)
            If Not objSales Is Nothing Then
                lblSalesName.Text = objSales.Name
                Dim objTarget As New SalesmanPartTarget

                objTarget = CType(arl.Item(e.Item.ItemIndex), SalesmanPartTarget)
                objTarget.SalesmanHeader = objSales
                objTarget.Year = ddlYear.SelectedValue
                objTarget.Month = ddlMonth.SelectedValue
                objTarget.Period = New Date(CType(ddlYear.SelectedValue, Integer), CType(ddlMonth.SelectedValue, Integer), 1, 0, 0, 0)
                objTarget.Target = CType(IIf(txtTarget.Text.Trim = "", "0", txtTarget.Text.Trim), Decimal)
                objTarget.Realization = CType(IIf(txtRealisasi.Text.Trim = "", "0", txtRealisasi.Text.Trim), Decimal)
                If objTarget.Target > 0 Then
                    objTarget.Persentage = System.Math.Round((objTarget.Realization / objTarget.Target) * 100, 2)
                Else
                    objTarget.Persentage = 0
                End If

                arl.Item(e.Item.ItemIndex) = objTarget

                dgSalesmanTarget.ShowFooter = True
                dgSalesmanTarget.EditItemIndex = -1
                BindEmployeeTarget(dgSalesmanTarget.CurrentPageIndex)
            Else
                MessageBox.Show("Tidak ada Salesman dengan kode tersebut")
            End If

        End If

    End Sub

    Private Sub DeleteCommand(ByVal e As DataGridCommandEventArgs, ByRef shouldReturn As Boolean)
        shouldReturn = False
        Dim lblSalesmanCode As Label = e.Item.FindControl("lblSalesmanCode")
        Dim lblTarget As Label = e.Item.FindControl("lblTarget")
        Dim lblRealisasi As Label = e.Item.FindControl("lblRealisasi")

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanPartTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanPartTarget), "SalesmanHeader.Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Trim))
        criterias.opAnd(New Criteria(GetType(SalesmanPartTarget), "Year", MatchType.Exact, ddlYear.SelectedValue))
        criterias.opAnd(New Criteria(GetType(SalesmanPartTarget), "Month", MatchType.Exact, ddlMonth.SelectedValue))

        Dim arlSalesTarget As ArrayList = oSalesTargetFacade.RetrieveByCriteria(criterias)
        If arlSalesTarget.Count > 0 Then
            Dim objSalesTarget As SalesmanPartTarget = arlSalesTarget(0)
            oSalesTargetFacade.Delete(objSalesTarget)
        End If
        Dim arl As ArrayList = sessHelper.GetSession("EmpTarget")
        arl.RemoveAt(e.Item.ItemIndex)
        BindEmployeeTarget(dgSalesmanTarget.CurrentPageIndex)
    End Sub

    Private Function InsertHistory(ByVal objSalesmanPartTarget As SalesmanPartTarget) As Integer
        Try

            Dim objSalesmanPartTargetHistFacade As New SalesmanPartTargetHistFacade(User)

            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanPartTargetHist), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanPartTargetHist), "SalesmanPartTarget.ID", MatchType.Exact, objSalesmanPartTarget.ID))
            criterias.opAnd(New Criteria(GetType(SalesmanPartTargetHist), "Target", MatchType.Exact, CDec(objSalesmanPartTarget.Target)))
            criterias.opAnd(New Criteria(GetType(SalesmanPartTargetHist), "Realization", MatchType.Exact, CDec(objSalesmanPartTarget.Realization)))

            Dim objSortCol As SortCollection = New SortCollection
            objSortCol.Add(New Sort(GetType(SalesmanPartTargetHist), "CreatedTime", Sort.SortDirection.DESC))

            Dim arlSalesTarget As ArrayList = objSalesmanPartTargetHistFacade.Retrieve(criterias, objSortCol)
            If arlSalesTarget.Count <= 0 Then
                Dim objHist As New SalesmanPartTargetHist
                objHist.SalesmanPartTarget = objSalesmanPartTarget
                objHist.Target = objSalesmanPartTarget.Target
                objHist.Realization = objSalesmanPartTarget.Realization
                objHist.Persentage = objSalesmanPartTarget.Persentage

                Return objSalesmanPartTargetHistFacade.Insert(objHist)
            Else
                Dim oHist As SalesmanPartTargetHist = CType(arlSalesTarget(0), SalesmanPartTargetHist)

                If Not (oHist.Target = objSalesmanPartTarget.Target And oHist.Realization = objSalesmanPartTarget.Realization) Then
                    Dim objHist As New SalesmanPartTargetHist
                    objHist.SalesmanPartTarget = objSalesmanPartTarget
                    objHist.Target = objSalesmanPartTarget.Target
                    objHist.Realization = objSalesmanPartTarget.Realization
                    objHist.Persentage = objSalesmanPartTarget.Persentage

                    Return objSalesmanPartTargetHistFacade.Insert(objHist)
                Else
                    Return 0
                End If
            End If

        Catch ex As Exception
            Dim str As String = String.Empty
        End Try

    End Function
#End Region


End Class
