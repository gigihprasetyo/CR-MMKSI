Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports System.Collections.Generic

Public Class FrmDiscountMatrix
    Inherits System.Web.UI.Page

    Private _sessHelper As SessionHelper = New SessionHelper
    Private bPrivilegeChangesFleetGradeDiscount As Boolean = True

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ActivateUserPrivilege()
        If Not IsPostBack Then
            BindDDLGrade()
            InitiatePage()

            btnSimpan.Enabled = False
            btnSearch_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = bPrivilegeChangesFleetGradeDiscount
        btnBatal.Visible = bPrivilegeChangesFleetGradeDiscount
    End Sub

    Private Sub ActivateUserPrivilege()
        bPrivilegeChangesFleetGradeDiscount = True
        If Not SecurityProvider.Authorize(Context.User, SR.DP_DaftarDiscountMatrix_Lihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=DISCOUNT PROPOSAL - Discount Matrix")
        End If
    End Sub

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "Grade"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindDDLGrade()
        With ddlGrade
            .Items.Clear()
            .DataSource = New StandardCodeFacade(User).RetrieveByCategory("EnumFleetDiscount.Grade")
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
            .Items.Insert(0, New ListItem("Silakan Pilih ", -1))
            .SelectedIndex = 0
        End With
    End Sub
    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dtgFleetGradeDiscount.DataSource = New FleetGradeDiscountFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession("CRITERIAS"), CriteriaComposite), indexPage + 1, _
                    dtgFleetGradeDiscount.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgFleetGradeDiscount.VirtualItemCount = totalRow
            dtgFleetGradeDiscount.DataBind()
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If ddlGrade.SelectedIndex = 0 Then
            MessageBox.Show("Grade mohon diisi !")
            Exit Sub
        End If
        If txtVehicleType.Text.Trim = "" Then
            MessageBox.Show("Kode Kendaraan mohon diisi !")
            Exit Sub
        End If
        If txtDiscount.Text.Trim = "" OrElse txtDiscount.Text.Trim = "0" Then
            MessageBox.Show("Jumlah diskon mohon diisi !")
            Exit Sub
        End If
        If icPeriodStart.Value > icPeriodEnd.Value Then
            MessageBox.Show("Periode Awal tidak boleh lebih dari Periode Akhir !")
            Exit Sub
        End If

        Dim objFleetGradeDiscountFacade As FleetGradeDiscountFacade = New FleetGradeDiscountFacade(User)
        If objFleetGradeDiscountFacade.SearchGradeAndVehicleType(ddlGrade.SelectedValue, txtVehicleType.Text.Trim, hdnFleetGradeDiscountID.Value, CType(ViewState("vsMode"), String)) Then
            MessageBox.Show(SR.DataIsExist("Grade dan Kode Kendaraan"))
            Exit Sub
        End If

        Dim objFleetGradeDiscount As FleetGradeDiscount = New FleetGradeDiscount
        Dim nResult As Integer = -1
        If CType(ViewState("vsMode"), String) = "New" Then
            objFleetGradeDiscount.Grade = ddlGrade.SelectedValue
            objFleetGradeDiscount.VehicleType = txtVehicleType.Text.Trim
            objFleetGradeDiscount.Discount = txtDiscount.Text
            objFleetGradeDiscount.PeriodStart = icPeriodStart.Value
            objFleetGradeDiscount.PeriodEnd = icPeriodEnd.Value
            nResult = New FleetGradeDiscountFacade(User).Insert(objFleetGradeDiscount)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
                btnBatal_Click(Nothing, Nothing)
            End If
        Else
            nResult = UpdateFleetGradeDiscount()
            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
                btnBatal_Click(Nothing, Nothing)
            End If
        End If
        dtgFleetGradeDiscount.CurrentPageIndex = 0
        BindDatagrid(dtgFleetGradeDiscount.CurrentPageIndex)
    End Sub

    Private Function UpdateFleetGradeDiscount() As Integer
        If hdnFleetGradeDiscountID.Value.Trim = "" OrElse hdnFleetGradeDiscountID.Value.Trim = "0" Then
            Return -1
        End If
        Dim objFleetGradeDiscount As FleetGradeDiscount = New FleetGradeDiscountFacade(User).Retrieve(CInt(hdnFleetGradeDiscountID.Value))
        objFleetGradeDiscount.Grade = ddlGrade.SelectedValue
        objFleetGradeDiscount.VehicleType = txtVehicleType.Text.Trim
        objFleetGradeDiscount.Discount = txtDiscount.Text
        objFleetGradeDiscount.PeriodStart = icPeriodStart.Value
        objFleetGradeDiscount.PeriodEnd = icPeriodEnd.Value
        Try
            Return New FleetGradeDiscountFacade(User).Update(objFleetGradeDiscount)
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Private Sub dtgFleetGradeDiscount_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgFleetGradeDiscount.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objFleetGradeDiscount As FleetGradeDiscount = CType(e.Item.DataItem, FleetGradeDiscount)
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")

            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgFleetGradeDiscount.CurrentPageIndex * dtgFleetGradeDiscount.PageSize)

            'tambahan Privilege
            ActivateUserPrivilege()
            If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = bPrivilegeChangesFleetGradeDiscount
            End If

            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = bPrivilegeChangesFleetGradeDiscount
            End If

            Dim objStandarCode As New StandardCode
            objStandarCode = New StandardCodeFacade(User).GetByCategoryValue("EnumFleetDiscount.Grade", objFleetGradeDiscount.Grade)
            Try
                CType(e.Item.FindControl("lblGrade"), Label).Text = objStandarCode.ValueCode
                CType(e.Item.FindControl("lblVehicleType"), Label).Text = objFleetGradeDiscount.VehicleType
                CType(e.Item.FindControl("lblDiscount"), Label).Text = Format(objFleetGradeDiscount.Discount, "#,##0")
                CType(e.Item.FindControl("lblPeriode"), Label).Text = objFleetGradeDiscount.PeriodStart.ToString("dd/MM/yyyy") & " s/d " & objFleetGradeDiscount.PeriodEnd.ToString("dd/MM/yyyy")
            Catch
            End Try
        End If

    End Sub

    Private Sub dtgFleetGradeDiscount_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgFleetGradeDiscount.ItemCommand
        If e.CommandName = "Edit" Then
            ViewState.Add("vsMode", "Edit")
            ViewFleetGradeDiscount(e.Item.Cells(0).Text, True)
            dtgFleetGradeDiscount.SelectedIndex = e.Item.ItemIndex

            ddlGrade.Enabled = True
            txtVehicleType.Enabled = True
            cbPeriode.Enabled = True
            icPeriodStart.Enabled = True
            icPeriodEnd.Enabled = True
            txtDiscount.Enabled = True

        ElseIf e.CommandName = "Delete" Then
            Try
                DeleteFleetGradeDiscount(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearData()
        End If
    End Sub

    Private Sub ViewFleetGradeDiscount(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objFleetGradeDiscount As FleetGradeDiscount = New FleetGradeDiscountFacade(User).Retrieve(nID)
        If Not objFleetGradeDiscount Is Nothing Then
            hdnFleetGradeDiscountID.Value = nID
            ddlGrade.SelectedValue = objFleetGradeDiscount.Grade
            txtVehicleType.Text = objFleetGradeDiscount.VehicleType
            icPeriodStart.Value = objFleetGradeDiscount.PeriodStart
            icPeriodEnd.Value = objFleetGradeDiscount.PeriodEnd
            txtDiscount.Text = Format(objFleetGradeDiscount.Discount, "#,##0")
            Me.btnSimpan.Enabled = EditStatus
        Else
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub DeleteFleetGradeDiscount(ByVal nID As Integer)
        Dim objFleetGradeDiscount As FleetGradeDiscount = New FleetGradeDiscountFacade(User).Retrieve(nID)
        If Not objFleetGradeDiscount Is Nothing Then
            Dim nResult = New FleetGradeDiscountFacade(User).Delete(objFleetGradeDiscount)
            If nResult < 0 Then
                MessageBox.Show(SR.DeleteFail)
            End If
        Else
            MessageBox.Show(SR.DeleteFail)
        End If
        dtgFleetGradeDiscount.CurrentPageIndex = 0
        BindDatagrid(dtgFleetGradeDiscount.CurrentPageIndex)
    End Sub

    Private Sub ClearData()
        hdnFleetGradeDiscountID.Value = "0"
        ddlGrade.SelectedIndex = 0
        txtVehicleType.Text = String.Empty
        txtDiscount.Text = "0"
        cbPeriode.Checked = False
        icPeriodStart.Value = Date.Now
        icPeriodEnd.Value = Date.Now

        btnSimpan.Enabled = True
        ViewState.Add("vsMode", "New")

        ddlGrade.Enabled = True
        txtVehicleType.Enabled = True
        txtDiscount.Enabled = True
        cbPeriode.Enabled = True
        icPeriodStart.Enabled = True
        icPeriodEnd.Enabled = True
        dtgFleetGradeDiscount.SelectedIndex = -1
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        btnSearch_Click(Nothing, Nothing)
    End Sub

    Private Sub dtgFleetGradeDiscount_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgFleetGradeDiscount.PageIndexChanged
        dtgFleetGradeDiscount.SelectedIndex = -1
        dtgFleetGradeDiscount.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgFleetGradeDiscount.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgFleetGradeDiscount_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgFleetGradeDiscount.SortCommand
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

        dtgFleetGradeDiscount.SelectedIndex = -1
        dtgFleetGradeDiscount.CurrentPageIndex = 0
        BindDatagrid(dtgFleetGradeDiscount.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If cbPeriode.Checked = True Then
            If icPeriodStart.Value > icPeriodEnd.Value Then
                MessageBox.Show("Periode Awal lebih dari Periode Akhir")
                Exit Sub
            End If
        End If

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FleetGradeDiscount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(criterias)
        _sessHelper.SetSession("CRITERIAS", criterias)
        dtgFleetGradeDiscount.CurrentPageIndex = 0
        BindDatagrid(dtgFleetGradeDiscount.CurrentPageIndex)
        btnSimpan.Enabled = True
    End Sub

    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)
        If ddlGrade.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FleetGradeDiscount), "Grade", MatchType.Exact, ddlGrade.SelectedValue))
        End If
        If txtVehicleType.Text.Trim.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FleetGradeDiscount), "VehicleType", MatchType.Partial, txtVehicleType.Text.Trim))
        End If
        If cbPeriode.Checked Then
            If icPeriodStart.Value <= icPeriodEnd.Value Then
                criterias.opAnd(New Criteria(GetType(FleetGradeDiscount), "PeriodStart", MatchType.GreaterOrEqual, icPeriodStart.Value))
                criterias.opAnd(New Criteria(GetType(FleetGradeDiscount), "PeriodEnd", MatchType.LesserOrEqual, icPeriodEnd.Value))
            End If
        End If
        If txtDiscount.Text.Trim <> "" And txtDiscount.Text.Trim <> "0" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FleetGradeDiscount), "Discount", MatchType.Exact, txtDiscount.Text.Trim.Replace(".", "").Replace(",", "")))
        End If
    End Sub

    Private Sub LinkInformasiGrade_Click(sender As Object, e As EventArgs) Handles LinkInformasiGrade.Click
        Response.Redirect("FrmInformasiGrade.aspx")
    End Sub
End Class