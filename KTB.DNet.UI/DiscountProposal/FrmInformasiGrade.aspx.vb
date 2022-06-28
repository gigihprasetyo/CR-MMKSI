Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports System.Collections.Generic

Public Class FrmInformasiGrade
    Inherits System.Web.UI.Page

    Private _sessHelper As SessionHelper = New SessionHelper
    Private bPrivilegeChangesFleetGradeRetention As Boolean = True

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ActivateUserPrivilege()
        If Not IsPostBack Then
            BindddlGrade()
            BindddlOperator()
            InitiatePage()

            btnSimpan1.Enabled = False
            btnSimpan2.Enabled = False
            btnSearch1_Click(Nothing, Nothing)
            btnSearch2_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub SetControlPrivilege()
        btnSimpan1.Visible = bPrivilegeChangesFleetGradeRetention
        btnSimpan2.Visible = bPrivilegeChangesFleetGradeRetention
        btnBatal1.Visible = bPrivilegeChangesFleetGradeRetention
        btnBatal2.Visible = bPrivilegeChangesFleetGradeRetention
    End Sub

    Private Sub ActivateUserPrivilege()
        bPrivilegeChangesFleetGradeRetention = True
        'bPrivilegeChangesFleetGradeRetention = SecurityProvider.Authorize(Context.User, SR.ChangeFleetGradeRetention_Privilege)

        'If Not SecurityProvider.Authorize(Context.User, SR.ViewFleetGradeRetention1_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=DISCOUNT PROPOSAL - Informasi Grade")
        'End If
    End Sub

    Private Sub InitiatePage()
        ClearData1()
        ClearData2()
        SetControlPrivilege()
        ViewState("CurrentSortColumn1") = "Grade"
        ViewState("CurrentSortDirect1") = Sort.SortDirection.ASC
        ViewState("CurrentSortColumn2") = "Grade"
        ViewState("CurrentSortDirect2") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindddlGrade()
        With ddlGrade1
            .Items.Clear()
            .DataSource = New StandardCodeFacade(User).RetrieveByCategory("EnumFleetDiscount.Grade")
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
            .Items.Insert(0, New ListItem("Silakan Pilih ", -1))
            .SelectedIndex = 0
        End With

        With ddlGrade2
            .Items.Clear()
            .DataSource = New StandardCodeFacade(User).RetrieveByCategory("EnumFleetDiscount.Grade")
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
            .Items.Insert(0, New ListItem("Silakan Pilih ", -1))
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub BindddlOperator()
        With ddlOperator1
            .Items.Add(New ListItem("Silahkan Pilih", "-1"))
            .Items.Add(New ListItem("<", "0"))
            .Items.Add(New ListItem(">", "1"))
            .Items.Add(New ListItem("<=", "2"))
            .Items.Add(New ListItem(">=", "3"))
            .Items.Add(New ListItem("Range", "4"))
        End With
        ddlOperator1.SelectedIndex = 0

        With ddlOperator2
            .Items.Add(New ListItem("Silahkan Pilih", "-1"))
            .Items.Add(New ListItem("<", "0"))
            .Items.Add(New ListItem(">", "1"))
            .Items.Add(New ListItem("<=", "2"))
            .Items.Add(New ListItem(">=", "3"))
            .Items.Add(New ListItem("Range", "4"))
        End With
        ddlOperator2.SelectedIndex = 0
    End Sub

#Region "Panel1"
    Private Sub BindDatagrid1(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dtgFleetGradeRetention1.DataSource = New FleetGradeRetentionFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession("CRITERIAS1"), CriteriaComposite), indexPage + 1, _
                    dtgFleetGradeRetention1.PageSize, totalRow, CType(ViewState("CurrentSortColumn1"), String), CType(ViewState("CurrentSortDirect1"), Sort.SortDirection))
            dtgFleetGradeRetention1.VirtualItemCount = totalRow
            dtgFleetGradeRetention1.DataBind()
        End If
    End Sub

    Private Sub btnSimpan1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan1.Click
        If ddlGrade1.SelectedIndex = 0 Then
            MessageBox.Show("Grade mohon diisi !")
            Exit Sub
        End If
        If ddlOperator1.SelectedIndex = 0 Then
            MessageBox.Show("Operator mohon diisi !")
            Exit Sub
        End If
        If txtUnitFrom1.Text.Trim = "" Then
            MessageBox.Show("Jumlah unit mohon diisi !")
            Exit Sub
        End If
        If ddlOperator1.SelectedValue = "4" Then        '"Range"
            If txtUnitTo1.Text.Trim = "" Then
                MessageBox.Show("Unit Akhir mohon diisi !")
                Exit Sub
            End If
        Else
            txtUnitTo1.Text = 0
        End If
        Dim objFleetGradeRetentionFacade As FleetGradeRetentionFacade = New FleetGradeRetentionFacade(User)
        If objFleetGradeRetentionFacade.SearchGradeAndOperator(ddlGrade1.SelectedValue, ddlOperator1.SelectedValue, hdnFleetGradeRetentionID1.Value, CType(ViewState("vsMode1"), String)) Then
            MessageBox.Show(SR.DataIsExist("Grade dan Operator"))
            Exit Sub
        End If

        Dim objFleetGradeRetention As FleetGradeRetention = New FleetGradeRetention
        Dim nResult As Integer = -1
        If CType(ViewState("vsMode1"), String) = "New" Then
            objFleetGradeRetention.Category = 0
            objFleetGradeRetention.Grade = ddlGrade1.SelectedValue
            objFleetGradeRetention.VehicleType = ""
            objFleetGradeRetention.Operators = ddlOperator1.SelectedValue
            objFleetGradeRetention.UnitFrom = txtUnitFrom1.Text
            objFleetGradeRetention.UnitTo = txtUnitTo1.Text
            nResult = New FleetGradeRetentionFacade(User).Insert(objFleetGradeRetention)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
                btnBatal1_Click(Nothing, Nothing)
            End If
        Else
            nResult = UpdateFleetGradeRetention1()
            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
                btnBatal1_Click(Nothing, Nothing)
            End If
        End If
        dtgFleetGradeRetention1.CurrentPageIndex = 0
        BindDatagrid1(dtgFleetGradeRetention1.CurrentPageIndex)
    End Sub

    Private Function UpdateFleetGradeRetention1() As Integer
        If hdnFleetGradeRetentionID1.Value.Trim = "" OrElse hdnFleetGradeRetentionID1.Value.Trim = "0" Then
            Return -1
        End If
        Dim objFleetGradeRetention As FleetGradeRetention = New FleetGradeRetentionFacade(User).Retrieve(CInt(hdnFleetGradeRetentionID1.Value))
        objFleetGradeRetention.Category = 0
        objFleetGradeRetention.Grade = ddlGrade1.SelectedValue
        objFleetGradeRetention.VehicleType = ""
        objFleetGradeRetention.Operators = ddlOperator1.SelectedValue
        objFleetGradeRetention.UnitFrom = txtUnitFrom1.Text
        objFleetGradeRetention.UnitTo = txtUnitTo1.Text
        Try
            Return New FleetGradeRetentionFacade(User).Update(objFleetGradeRetention)
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Private Sub dtgFleetGradeRetention1_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgFleetGradeRetention1.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objFleetGradeRetention As FleetGradeRetention = CType(e.Item.DataItem, FleetGradeRetention)
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")

            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgFleetGradeRetention1.CurrentPageIndex * dtgFleetGradeRetention1.PageSize)

            'tambahan Privilege
            ActivateUserPrivilege()
            If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = bPrivilegeChangesFleetGradeRetention
            End If

            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = bPrivilegeChangesFleetGradeRetention
            End If

            Dim objStandarCode As New StandardCode
            objStandarCode = New StandardCodeFacade(User).GetByCategoryValue("EnumFleetDiscount.Grade", objFleetGradeRetention.Grade)
            Dim strOperators As String = String.Empty
            Select Case objFleetGradeRetention.Operators
                Case "0"
                    strOperators = "<"
                Case "1"
                    strOperators = ">"
                Case "2"
                    strOperators = "<="
                Case "3"
                    strOperators = ">="
                Case "4"
                    strOperators = "Range"
                Case Else
                    strOperators = ""
            End Select

            Dim strUnit As String = String.Empty
            If strOperators = "Range" Then
                strUnit = objFleetGradeRetention.UnitFrom.ToString() & " s/d " & objFleetGradeRetention.UnitTo.ToString()
            Else
                strUnit = objFleetGradeRetention.UnitFrom.ToString()
            End If
            Try
                CType(e.Item.FindControl("lblGrade"), Label).Text = objStandarCode.ValueCode
                CType(e.Item.FindControl("lblOperator"), Label).Text = strOperators
                CType(e.Item.FindControl("lblUnit"), Label).Text = strUnit
            Catch
            End Try
        End If

    End Sub

    Private Sub dtgFleetGradeRetention1_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgFleetGradeRetention1.ItemCommand
        If e.CommandName = "Edit" Then
            ViewState.Add("vsMode1", "Edit")
            ViewFleetGradeRetention1(e.Item.Cells(0).Text, True)
            dtgFleetGradeRetention1.SelectedIndex = e.Item.ItemIndex

            ddlGrade1.Enabled = True
            ddlOperator1.Enabled = True
            txtUnitFrom1.Enabled = True
            txtUnitTo1.Enabled = True

        ElseIf e.CommandName = "Delete" Then
            Try
                DeleteFleetGradeRetention1(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearData1()
        End If
    End Sub

    Private Sub ViewFleetGradeRetention1(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objFleetGradeRetention As FleetGradeRetention = New FleetGradeRetentionFacade(User).Retrieve(nID)
        If Not objFleetGradeRetention Is Nothing Then
            hdnFleetGradeRetentionID1.Value = nID
            ddlGrade1.SelectedValue = objFleetGradeRetention.Grade
            ddlOperator1.SelectedValue = objFleetGradeRetention.Operators
            ddlOperator1_SelectedIndexChanged(Nothing, Nothing)
            txtUnitFrom1.Text = objFleetGradeRetention.UnitFrom
            txtUnitTo1.Text = objFleetGradeRetention.UnitTo
            Me.btnSimpan1.Enabled = EditStatus
        Else
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub DeleteFleetGradeRetention1(ByVal nID As Integer)
        Dim objFleetGradeRetention As FleetGradeRetention = New FleetGradeRetentionFacade(User).Retrieve(nID)
        If Not objFleetGradeRetention Is Nothing Then
            Dim nResult = New FleetGradeRetentionFacade(User).Delete(objFleetGradeRetention)
            If nResult < 0 Then
                MessageBox.Show(SR.DeleteFail)
            End If
        Else
            MessageBox.Show(SR.DeleteFail)
        End If
        dtgFleetGradeRetention1.CurrentPageIndex = 0
        BindDatagrid1(dtgFleetGradeRetention1.CurrentPageIndex)
    End Sub

    Private Sub ClearData1()
        hdnFleetGradeRetentionID1.Value = "0"
        ddlGrade1.SelectedIndex = 0
        ddlOperator1.SelectedIndex = 0
        txtUnitFrom1.Text = "0"
        txtUnitTo1.Text = "0"
        lblStrip1.Visible = False
        txtUnitTo1.Visible = False

        btnSimpan1.Enabled = True
        ViewState.Add("vsMode1", "New")

        ddlGrade1.Enabled = True
        ddlOperator1.Enabled = True
        txtUnitFrom1.Enabled = True
        txtUnitTo1.Enabled = True
        dtgFleetGradeRetention1.SelectedIndex = -1
        dtgFleetGradeRetention1.ShowFooter = False
    End Sub

    Private Sub btnBatal1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal1.Click
        ClearData1()
        btnSearch1_Click(Nothing, Nothing)
    End Sub

    Private Sub dtgFleetGradeRetention1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgFleetGradeRetention1.PageIndexChanged
        dtgFleetGradeRetention1.SelectedIndex = -1
        dtgFleetGradeRetention1.CurrentPageIndex = e.NewPageIndex
        BindDatagrid1(dtgFleetGradeRetention1.CurrentPageIndex)
        ClearData1()
    End Sub

    Private Sub dtgFleetGradeRetention1_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgFleetGradeRetention1.SortCommand
        If CType(ViewState("CurrentSortColumn1"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect1"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect1") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect1") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn1") = e.SortExpression
            ViewState("CurrentSortDirect1") = Sort.SortDirection.ASC
        End If

        dtgFleetGradeRetention1.SelectedIndex = -1
        dtgFleetGradeRetention1.CurrentPageIndex = 0
        BindDatagrid1(dtgFleetGradeRetention1.CurrentPageIndex)
        ClearData1()
    End Sub

    Private Sub btnSearch1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch1.Click
        If ddlOperator1.SelectedValue = "4" Then        '"Range"
            If CInt(BlankToZerro(txtUnitFrom1.Text)) > CInt(BlankToZerro(txtUnitTo1.Text)) Then
                MessageBox.Show("Unit Awal lebih dari Unit Akhir")
                Exit Sub
            End If
        End If

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FleetGradeRetention), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria1(criterias)
        _sessHelper.SetSession("CRITERIAS1", criterias)
        dtgFleetGradeRetention1.CurrentPageIndex = 0
        BindDatagrid1(dtgFleetGradeRetention1.CurrentPageIndex)
        btnSimpan1.Enabled = True
    End Sub

    Private Sub CreateCriteria1(ByVal criterias As CriteriaComposite)
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FleetGradeRetention), "Category", MatchType.Exact, 0))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FleetGradeRetention), "VehicleType", MatchType.Exact, ""))
        If ddlGrade1.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FleetGradeRetention), "Grade", MatchType.Exact, ddlGrade1.SelectedValue))
        End If
        If ddlOperator1.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FleetGradeRetention), "Operators", MatchType.Exact, ddlOperator1.SelectedValue))
        End If

        If ddlOperator1.SelectedValue = "4" Then        '"Range"
            If CInt(BlankToZerro(txtUnitFrom1.Text)) <= CInt(BlankToZerro(txtUnitTo1.Text)) Then
                criterias.opAnd(New Criteria(GetType(FleetGradeRetention), "UnitFrom", MatchType.GreaterOrEqual, txtUnitFrom1.Text))
                criterias.opAnd(New Criteria(GetType(FleetGradeRetention), "UnitTo", MatchType.LesserOrEqual, txtUnitTo1.Text))
            End If
        Else
            If txtUnitFrom1.Text.Trim = 0 Then
                criterias.opAnd(New Criteria(GetType(FleetGradeRetention), "UnitFrom", MatchType.GreaterOrEqual, txtUnitFrom1.Text))
            Else
                criterias.opAnd(New Criteria(GetType(FleetGradeRetention), "UnitFrom", MatchType.Exact, txtUnitFrom1.Text))
            End If
        End If
    End Sub

    Private Sub ddlOperator1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlOperator1.SelectedIndexChanged
        If ddlOperator1.SelectedValue = "4" Then 'Range
            lblStrip1.Visible = True
            txtUnitTo1.Visible = True
        Else
            lblStrip1.Visible = False
            txtUnitTo1.Visible = False
            txtUnitTo1.Text = 0
        End If
    End Sub

    Private Sub btnKembali1_Click(sender As Object, e As EventArgs) Handles btnKembali1.Click
        Response.Redirect("FrmDiscountMatrix.aspx")
    End Sub

#End Region

#Region "Panel2"

    Private Sub BindDatagrid2(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dtgFleetGradeRetention2.DataSource = New FleetGradeRetentionFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession("CRITERIAS2"), CriteriaComposite), indexPage + 1, _
                    dtgFleetGradeRetention2.PageSize, totalRow, CType(ViewState("CurrentSortColumn2"), String), CType(ViewState("CurrentSortDirect2"), Sort.SortDirection))
            dtgFleetGradeRetention2.VirtualItemCount = totalRow
            dtgFleetGradeRetention2.DataBind()
        End If
    End Sub

    Private Sub btnSimpan2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan2.Click
        If ddlGrade2.SelectedIndex = 0 Then
            MessageBox.Show("Grade mohon diisi !")
            Exit Sub
        End If
        If txtVehicleType.Text = "" Then
            MessageBox.Show("Tipe Kendaraan mohon diisi !")
            Exit Sub
        End If
        If ddlOperator2.SelectedIndex = 0 Then
            MessageBox.Show("Operator mohon diisi !")
            Exit Sub
        End If
        If txtUnitFrom2.Text.Trim = "" Then
            MessageBox.Show("Jumlah unit mohon diisi !")
            Exit Sub
        End If
        If ddlOperator2.SelectedValue = "4" Then        '"Range"
            If txtUnitTo2.Text.Trim = "" Then
                MessageBox.Show("Unit Akhir mohon diisi !")
                Exit Sub
            End If
        Else
            txtUnitTo2.Text = 0
        End If
        Dim objFleetGradeRetentionFacade As FleetGradeRetentionFacade = New FleetGradeRetentionFacade(User)
        If objFleetGradeRetentionFacade.SearchGradeAndVehicleTypeAndOperator(ddlGrade2.SelectedValue, txtVehicleType.Text, ddlOperator2.SelectedValue, hdnFleetGradeRetentionID2.Value, CType(ViewState("vsMode2"), String)) Then
            MessageBox.Show(SR.DataIsExist("Grade, Tipe Kendaraan, dan Operator"))
            Exit Sub
        End If

        Dim objFleetGradeRetention As FleetGradeRetention = New FleetGradeRetention
        Dim nResult As Integer = -1
        If CType(ViewState("vsMode2"), String) = "New" Then
            objFleetGradeRetention.Category = 1
            objFleetGradeRetention.Grade = ddlGrade2.SelectedValue
            objFleetGradeRetention.VehicleType = txtVehicleType.Text.Trim
            objFleetGradeRetention.Operators = ddlOperator2.SelectedValue
            objFleetGradeRetention.UnitFrom = txtUnitFrom2.Text
            objFleetGradeRetention.UnitTo = txtUnitTo2.Text
            nResult = New FleetGradeRetentionFacade(User).Insert(objFleetGradeRetention)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
                btnBatal2_Click(Nothing, Nothing)
            End If
        Else
            nResult = UpdateFleetGradeRetention2()
            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
                btnBatal2_Click(Nothing, Nothing)
            End If
        End If
        dtgFleetGradeRetention2.CurrentPageIndex = 0
        BindDatagrid2(dtgFleetGradeRetention2.CurrentPageIndex)
    End Sub

    Private Function UpdateFleetGradeRetention2() As Integer
        If hdnFleetGradeRetentionID2.Value.Trim = "" OrElse hdnFleetGradeRetentionID2.Value.Trim = "0" Then
            Return -1
        End If
        Dim objFleetGradeRetention As FleetGradeRetention = New FleetGradeRetentionFacade(User).Retrieve(CInt(hdnFleetGradeRetentionID2.Value))
        objFleetGradeRetention.Category = 1
        objFleetGradeRetention.Grade = ddlGrade2.SelectedValue
        objFleetGradeRetention.VehicleType = txtVehicleType.Text.Trim
        objFleetGradeRetention.Operators = ddlOperator2.SelectedValue
        objFleetGradeRetention.UnitFrom = txtUnitFrom2.Text
        objFleetGradeRetention.UnitTo = txtUnitTo2.Text
        Try
            Return New FleetGradeRetentionFacade(User).Update(objFleetGradeRetention)
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Private Sub dtgFleetGradeRetention2_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgFleetGradeRetention2.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objFleetGradeRetention As FleetGradeRetention = CType(e.Item.DataItem, FleetGradeRetention)
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")

            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgFleetGradeRetention2.CurrentPageIndex * dtgFleetGradeRetention2.PageSize)

            'tambahan Privilege
            ActivateUserPrivilege()
            If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = bPrivilegeChangesFleetGradeRetention
            End If

            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = bPrivilegeChangesFleetGradeRetention
            End If

            Dim objStandarCode As New StandardCode
            objStandarCode = New StandardCodeFacade(User).GetByCategoryValue("EnumFleetDiscount.Grade", objFleetGradeRetention.Grade)
            Dim strOperators As String = String.Empty
            Select Case objFleetGradeRetention.Operators
                Case "0"
                    strOperators = "<"
                Case "1"
                    strOperators = ">"
                Case "2"
                    strOperators = "<="
                Case "3"
                    strOperators = ">="
                Case "4"
                    strOperators = "Range"
                Case Else
                    strOperators = ""
            End Select

            Dim strUnit As String = String.Empty
            If strOperators = "Range" Then
                strUnit = objFleetGradeRetention.UnitFrom.ToString() & " s/d " & objFleetGradeRetention.UnitTo.ToString()
            Else
                strUnit = objFleetGradeRetention.UnitFrom.ToString()
            End If
            Try
                CType(e.Item.FindControl("lblGrade"), Label).Text = objStandarCode.ValueCode
                CType(e.Item.FindControl("lblVehicleType"), Label).Text = objFleetGradeRetention.VehicleType
                CType(e.Item.FindControl("lblOperator"), Label).Text = strOperators
                CType(e.Item.FindControl("lblUnit"), Label).Text = strUnit
            Catch
            End Try
        End If

    End Sub

    Private Sub dtgFleetGradeRetention2_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgFleetGradeRetention2.ItemCommand
        If e.CommandName = "Edit" Then
            ViewState.Add("vsMode2", "Edit")
            ViewFleetGradeRetention2(e.Item.Cells(0).Text, True)
            dtgFleetGradeRetention2.SelectedIndex = e.Item.ItemIndex

            ddlGrade2.Enabled = True
            txtVehicleType.Enabled = True
            ddlOperator2.Enabled = True
            txtUnitFrom2.Enabled = True
            txtUnitTo2.Enabled = True

        ElseIf e.CommandName = "Delete" Then
            Try
                DeleteFleetGradeRetention2(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearData2()
        End If
    End Sub

    Private Sub ViewFleetGradeRetention2(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objFleetGradeRetention As FleetGradeRetention = New FleetGradeRetentionFacade(User).Retrieve(nID)
        If Not objFleetGradeRetention Is Nothing Then
            hdnFleetGradeRetentionID2.Value = nID
            ddlGrade2.SelectedValue = objFleetGradeRetention.Grade
            txtVehicleType.Text = objFleetGradeRetention.VehicleType
            ddlOperator2.SelectedValue = objFleetGradeRetention.Operators
            ddlOperator2_SelectedIndexChanged(Nothing, Nothing)
            txtUnitFrom2.Text = objFleetGradeRetention.UnitFrom
            txtUnitTo2.Text = objFleetGradeRetention.UnitTo
            Me.btnSimpan2.Enabled = EditStatus
        Else
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub DeleteFleetGradeRetention2(ByVal nID As Integer)
        Dim objFleetGradeRetention As FleetGradeRetention = New FleetGradeRetentionFacade(User).Retrieve(nID)
        If Not objFleetGradeRetention Is Nothing Then
            Dim nResult = New FleetGradeRetentionFacade(User).Delete(objFleetGradeRetention)
            If nResult < 0 Then
                MessageBox.Show(SR.DeleteFail)
            End If
        Else
            MessageBox.Show(SR.DeleteFail)
        End If
        dtgFleetGradeRetention2.CurrentPageIndex = 0
        BindDatagrid2(dtgFleetGradeRetention2.CurrentPageIndex)
    End Sub

    Private Sub ClearData2()
        hdnFleetGradeRetentionID2.Value = "0"
        ddlGrade2.SelectedIndex = 0
        txtVehicleType.Text = ""
        ddlOperator2.SelectedIndex = 0
        txtUnitFrom2.Text = "0"
        txtUnitTo2.Text = "0"
        lblStrip2.Visible = False
        txtUnitTo2.Visible = False

        btnSimpan2.Enabled = True
        ViewState.Add("vsMode2", "New")

        ddlGrade2.Enabled = True
        txtVehicleType.Enabled = True
        ddlOperator2.Enabled = True
        txtUnitFrom2.Enabled = True
        txtUnitTo2.Enabled = True
        dtgFleetGradeRetention2.SelectedIndex = -1
        dtgFleetGradeRetention2.ShowFooter = False
    End Sub

    Private Sub btnBatal2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal2.Click
        ClearData2()
        btnSearch2_Click(Nothing, Nothing)
    End Sub

    Private Sub dtgFleetGradeRetention2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgFleetGradeRetention2.PageIndexChanged
        dtgFleetGradeRetention2.SelectedIndex = -1
        dtgFleetGradeRetention2.CurrentPageIndex = e.NewPageIndex
        BindDatagrid2(dtgFleetGradeRetention2.CurrentPageIndex)
        ClearData2()
    End Sub

    Private Sub dtgFleetGradeRetention2_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgFleetGradeRetention2.SortCommand
        If CType(ViewState("CurrentSortColumn2"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect2"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect2") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect2") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn2") = e.SortExpression
            ViewState("CurrentSortDirect2") = Sort.SortDirection.ASC
        End If

        dtgFleetGradeRetention2.SelectedIndex = -1
        dtgFleetGradeRetention2.CurrentPageIndex = 0
        BindDatagrid2(dtgFleetGradeRetention2.CurrentPageIndex)
        ClearData2()
    End Sub

    Private Function BlankToZerro(ByVal _valueProperty As String) As Double
        If Len(_valueProperty.Trim) > 0 Then
            If InStr(_valueProperty.Trim, ".") > 0 OrElse InStr(_valueProperty.Trim, ",") > 0 Then
                _valueProperty = Replace(Replace(_valueProperty.Trim, ".", ""), ",", "")
                If _valueProperty.Trim = "" OrElse _valueProperty.Trim <= 0 Then
                    Return 0
                Else
                    Return Format(CDbl(_valueProperty), "#,##0")
                End If
            Else
                If _valueProperty.Trim = "" OrElse _valueProperty.Trim <= 0 Then
                    Return 0
                Else
                    Return CDbl(_valueProperty)
                End If
            End If
        Else
            Return 0
        End If
    End Function

    Private Sub btnSearch2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch2.Click
        If ddlOperator2.SelectedValue = "4" Then        '"Range"
            If CInt(BlankToZerro(txtUnitFrom2.Text)) > CInt(BlankToZerro(txtUnitTo2.Text)) Then
                MessageBox.Show("Unit Awal lebih dari Unit Akhir")
                Exit Sub
            End If
        End If

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FleetGradeRetention), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria2(criterias)
        _sessHelper.SetSession("CRITERIAS2", criterias)
        dtgFleetGradeRetention2.CurrentPageIndex = 0
        BindDatagrid2(dtgFleetGradeRetention2.CurrentPageIndex)
        btnSimpan2.Enabled = True
    End Sub

    Private Sub CreateCriteria2(ByVal criterias As CriteriaComposite)
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FleetGradeRetention), "Category", MatchType.Exact, 1))
        If ddlGrade2.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FleetGradeRetention), "Grade", MatchType.Exact, ddlGrade2.SelectedValue))
        End If
        If txtVehicleType.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FleetGradeRetention), "VehicleType", MatchType.Partial, txtVehicleType.Text.Trim))
        End If
        If ddlOperator2.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FleetGradeRetention), "Operators", MatchType.Exact, ddlOperator2.SelectedValue))
        End If
        If ddlOperator2.SelectedValue = "4" Then        '"Range"
            If CInt(BlankToZerro(txtUnitFrom2.Text)) <= CInt(BlankToZerro(txtUnitTo2.Text)) Then
                criterias.opAnd(New Criteria(GetType(FleetGradeRetention), "UnitFrom", MatchType.GreaterOrEqual, txtUnitFrom2.Text))
                criterias.opAnd(New Criteria(GetType(FleetGradeRetention), "UnitTo", MatchType.LesserOrEqual, txtUnitTo2.Text))
            End If
        Else
            If txtUnitFrom2.Text = 0 Then
                criterias.opAnd(New Criteria(GetType(FleetGradeRetention), "UnitFrom", MatchType.GreaterOrEqual, txtUnitFrom2.Text))
            Else
                criterias.opAnd(New Criteria(GetType(FleetGradeRetention), "UnitFrom", MatchType.Exact, txtUnitFrom2.Text))
            End If
        End If
    End Sub

    Private Sub ddlOperator2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlOperator2.SelectedIndexChanged
        If ddlOperator2.SelectedValue = "4" Then 'Range
            lblStrip2.Visible = True
            txtUnitTo2.Visible = True
        Else
            lblStrip2.Visible = False
            txtUnitTo2.Visible = False
            txtUnitTo2.Text = 0
        End If
    End Sub

    Private Sub btnKembali2_Click(sender As Object, e As EventArgs) Handles btnKembali2.Click
        Response.Redirect("FrmDiscountMatrix.aspx")
    End Sub
#End Region

End Class