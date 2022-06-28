#Region "Custom Namespace Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports System.Collections.Generic
Imports System.Linq

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.UI.Helper
#End Region

Public Class FrmTrFreePass
    Inherits System.Web.UI.Page

    Dim gridColNo As Integer = 0
    Private helpers As New TrainingHelpers(Me.Page)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        helpers.CheckPrivilege("priv9B")
        If Not Page.IsPostBack Then
            InitForm()
        End If
    End Sub

    Private Sub InitForm()
        BindDdlFiscalYear()
        rowQty.Visible = True
        rowQtyUsed.Visible = True
        txtDealerCode.Text = String.Empty
        txtQty.Text = String.Empty
        lnkQtyUsed.Text = String.Empty
        BindDataGrid(0)
        btnCari.Enabled = True
        btnSimpan.Enabled = True
        SetActiveControl(helpers.IsEdit)
    End Sub

    Private Sub SetActiveControl(ByVal isActive As Boolean)
        btnSimpan.Visible = isActive
        btnBatal.Visible = isActive
    End Sub

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        InitForm()
    End Sub

    Private Sub BindDdlFiscalYear()
        Dim GetTahun As Integer = DateTime.Now.Year
        ddlFiscalYear.ClearSelection()
        ddlFiscalYear.Items.Clear()
        'Before

        ddlFiscalYear.Items.Add(New ListItem("Silakan Pilih", "-1"))

        For x As Integer = 4 To 0 Step -1
            Dim value1 As String = (GetTahun - x).ToString()
            Dim value2 As String = (GetTahun - x - 1).ToString()
            Dim value As String = String.Format("{0}/{1}", value2, value1)
            ddlFiscalYear.Items.Add(New ListItem(value, value))
        Next
        'After
        For x As Integer = 0 To 4
            Dim value1 As String = (GetTahun + x).ToString()
            Dim value2 As String = (GetTahun + x + 1).ToString()
            Dim value As String = String.Format("{0}/{1}", value1, value2)
            ddlFiscalYear.Items.Add(New ListItem(value, value))
        Next
        '  ddlFiscalYear.SelectedValue = String.Format("{0}/{1}", GetTahun.ToString(), (GetTahun + 1).ToString())
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Try

            If Not Page.IsValid Then
                Exit Sub
            End If

            Dim freePassData As TrFreePass = GetExistingFreePass()

            If freePassData.ID = 0 Then
                InsertNewFreepass()
            Else
                UpdateFreePass(freePassData)
            End If

            InitForm()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub InsertNewFreepass()
        Dim data As New TrFreePass
        data.Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim())
        data.FiscalYear = ddlFiscalYear.SelectedValue
        data.Status = 1
        data.RowStatus = 0
        data.Qty = CInt(txtQty.Text)
        data.QtyUsed = 0

        Dim result As Integer = New TrFreePassFacade(User).Insert(data)
        MessageBox.Show(SR.SaveSuccess)
    End Sub

    Private Sub UpdateFreePass(ByVal data As TrFreePass)

        If CInt(txtQty.Text) < data.QtyUsed Then
            Throw New Exception("Jumlah Free Pass yang telah terpakai melebihi jumlah yang anda input")
        End If

        data.Qty = CInt(txtQty.Text)

        Dim result As Integer = New TrFreePassFacade(User).Update(data)
        MessageBox.Show(SR.UpdateSucces)
    End Sub


    Private Function GetExistingFreePass() As TrFreePass
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrFreePass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrFreePass), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Trim()))
        criterias.opAnd(New Criteria(GetType(TrFreePass), "FiscalYear", MatchType.Exact, ddlFiscalYear.SelectedValue))

        Dim result As New TrFreePass

        Dim arlResult As ArrayList = New TrFreePassFacade(User).Retrieve(criterias)

        If arlResult.Count > 0 Then
            result = arlResult(0)
        End If


        Return result

    End Function



    Private Function CriteriaSearch() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrFreePass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtDealerCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(TrFreePass), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text))
        End If

        If ddlFiscalYear.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(TrFreePass), "FiscalYear", MatchType.Exact, ddlFiscalYear.SelectedValue))
        End If

        If txtQty.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(TrFreePass), "Qty", MatchType.Exact, CInt(txtQty.Text)))
        End If

        Return criterias
    End Function

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        gridColNo = 0
        dtgFreePass.DataSource = New TrFreePassFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, dtgFreePass.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgFreePass.VirtualItemCount = totalRow
        dtgFreePass.DataBind()
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Try
            BindDataGrid(0)
            btnSimpan.Enabled = True

            If dtgFreePass.Items.Count < 0 Then
                MessageBox.Show("Data tidak ditemukan")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dtgFreePass_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgFreePass.ItemCommand
        If (e.CommandName = "View") Then
            SendGridItemToField(e.Item.Cells(0).Text)
            btnSimpan.Enabled = False
        ElseIf (e.CommandName = "Edit") Then
            SendGridItemToField(e.Item.Cells(0).Text)
            btnSimpan.Enabled = True
        ElseIf (e.CommandName = "Delete") Then
            DeleteFreePass(e.Item.Cells(0).Text)
        End If
    End Sub

    Private Sub dtgFreePass_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgFreePass.ItemDataBound
        Try

            If Not e.Item.DataItem Is Nothing Then
                Dim data As TrFreePass = CType(e.Item.DataItem, TrFreePass)
                gridColNo += 1

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                Dim lblFiscalYear As Label = CType(e.Item.FindControl("lblFiscalYear"), Label)
                Dim lblQty As Label = CType(e.Item.FindControl("lblQty"), Label)
                Dim lbtnQtyUsed As LinkButton = CType(e.Item.FindControl("lbtnQtyUsed"), LinkButton)
                Dim btnUbah As LinkButton = CType(e.Item.FindControl("btnUbah"), LinkButton)
                Dim btnHapus As LinkButton = CType(e.Item.FindControl("btnHapus"), LinkButton)

                lblNo.Text = gridColNo
                lblDealerCode.Text = data.Dealer.DealerCode & " - " & data.Dealer.DealerName
                lblFiscalYear.Text = data.FiscalYear
                lblQty.Text = data.Qty
                lbtnQtyUsed.Text = data.QtyUsed
                lbtnQtyUsed.PostBackUrl = "FrmFreePassUsed.aspx?dealerCode=" & data.Dealer.DealerCode & "&fiscalYear=" & data.FiscalYear

                btnHapus.Visible = helpers.IsEdit
                btnUbah.Visible = helpers.IsEdit
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dtgFreePass_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgFreePass.PageIndexChanged
        dtgFreePass.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgFreePass.CurrentPageIndex)
    End Sub

    Private Sub dtgFreePass_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgFreePass.SortCommand
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

        dtgFreePass.CurrentPageIndex = 0
        BindDataGrid(dtgFreePass.CurrentPageIndex)
    End Sub

    Private Sub SendGridItemToField(id As Integer)
        Dim facade As TrFreePassFacade = New TrFreePassFacade(User)
        Dim data As TrFreePass = facade.Retrieve(id)

        txtDealerCode.Text = data.Dealer.DealerCode
        ddlFiscalYear.ClearSelection()
        ddlFiscalYear.Items.FindByValue(data.FiscalYear).Selected = True

        txtQty.Text = data.Qty
        lnkQtyUsed.Text = data.QtyUsed
        lnkQtyUsed.PostBackUrl = "FrmFreePassUsed.aspx?dealerCode=" & data.Dealer.DealerCode & "&fiscalYear=" & data.FiscalYear

    End Sub

    Private Sub DeleteFreePass(id As Integer)
        Try
            Dim facade As TrFreePassFacade = New TrFreePassFacade(User)
            Dim data As TrFreePass = facade.Retrieve(id)
            data.RowStatus = CType(DBRowStatus.Deleted, Short)
            facade.Delete(data)
            MessageBox.Show(SR.DeleteSucces)
            InitForm()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cvFiscalYear_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try
            If ddlFiscalYear.SelectedValue = "-1" Then
                cvFiscalYear.ErrorMessage = "* harus dipilih"
                args.IsValid = False
                Return
            End If

            If CInt(Left(ddlFiscalYear.SelectedValue, 4)) < DateTime.Now.Year Then
                cvFiscalYear.ErrorMessage = "* tahun fiskal tidak bisa dibawah tahun ini"
                args.IsValid = False
                Return
            End If

            args.IsValid = True

        Catch ex As Exception
            args.IsValid = False
        End Try
    End Sub

    Protected Sub cvDealer_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try
            If txtDealerCode.Text = String.Empty Then
                cvDealer.ErrorMessage = "* harus dipilih"
                args.IsValid = False

            Else

                Dim dealerData As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text)
                If dealerData.ID = 0 Then
                    cvDealer.ErrorMessage = "* tidak terdaftar dalam database"
                    args.IsValid = False
                Else
                    args.IsValid = True
                End If

            End If



        Catch ex As Exception
            args.IsValid = False
        End Try
    End Sub

End Class