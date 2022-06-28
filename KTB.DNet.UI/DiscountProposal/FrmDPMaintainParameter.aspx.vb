Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports System.Linq
Imports System.Collections.Generic
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports System.IO
Imports System.Text
Imports KTB.DNet.BusinessFacade.PK
Imports OfficeOpenXml


Public Class FrmDPMaintainParameter
    Inherits System.Web.UI.Page

#Region "Custom Variable"
    Private _sessHelper As SessionHelper = New SessionHelper
#End Region

#Region "Custom Function"
    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.DP_MaintainParameter_Lihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=DISCOUNT PROPOSAL - Maintain Parameter")
        End If
    End Sub

    Private Sub bindDdlType()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumDiscountProposal.ParameterType"))
        criterias.opAnd(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, 0))
        criterias.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.InSet, "(1,3)"))
        Dim arrDPParamType As ArrayList = New StandardCodeFacade(User).Retrieve(criterias)
        _sessHelper.SetSession("ENUMPARAMTYPE", arrDPParamType)
        ddlType.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each item As StandardCode In arrDPParamType
            Dim temp As New ListItem(item.ValueDesc, item.ValueId)
            ddlType.Items.Add(temp)
        Next
    End Sub

    Private Sub bindDdlStatus()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumDiscountProposal.StatusParameter"))
        criterias.opAnd(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, 0))
        Dim arrDPParamType As ArrayList = New StandardCodeFacade(User).Retrieve(criterias)
        ddlStatus.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each item As StandardCode In arrDPParamType
            Dim temp As New ListItem(item.ValueDesc, item.ValueId)
            ddlStatus.Items.Add(temp)
        Next
    End Sub

    Private Sub BindDataGrid(ByVal index As Integer, Optional searchCriterias As CriteriaComposite = Nothing)
        Dim _arrList As ArrayList = New ArrayList
        'Dim _arrList As List(Of Object) = New List(Of Object)
        Dim PagedList As ArrayList = New ArrayList
        Dim _totalRow As Integer = 0
        Dim criterias As CriteriaComposite
        If IsNothing(searchCriterias) Then
            criterias = New CriteriaComposite(New Criteria(GetType(DiscountProposalParameter), "RowStatus", MatchType.Exact, 0))
        Else
            criterias = searchCriterias
        End If

        PagedList = New DiscountProposalParameterFacade(User).RetrieveByCriteria(criterias, index + 1, dtgParameter.PageSize, _totalRow)
        If Not IsNothing(PagedList) Then
            _arrList = PagedList
        End If
        dtgParameter.VirtualItemCount = _totalRow
        dtgParameter.DataSource = _arrList
        dtgParameter.DataBind()
    End Sub

    Private Function getParameterTypeDesc(ByVal type As Integer) As String
        Dim arrDPParamType As ArrayList = CType(_sessHelper.GetSession("ENUMPARAMTYPE"), ArrayList)
        For Each item As StandardCode In arrDPParamType
            If item.ValueId = type Then
                Return item.ValueDesc
            End If
        Next
    End Function

    Private Function getSearchCriterias() As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalParameter), "RowStatus", MatchType.Exact, 0))

        If ddlType.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(DiscountProposalParameter), "ParameterType", MatchType.Exact, ddlType.SelectedValue))
        End If
        If txtParameterName.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(DiscountProposalParameter), "ParameterName", MatchType.Partial, txtParameterName.Text.Trim))
        End If
        If ddlStatus.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(DiscountProposalParameter), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        Return criterias
    End Function

#End Region

#Region "Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ActivateUserPrivilege()
        If Not IsPostBack Then
            bindDdlType()
            bindDdlStatus()
            BindDataGrid(0)
        End If
    End Sub
#End Region

    Protected Sub dtgParameter_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgParameter.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim objDPParam As DiscountProposalParameter = CType(CType(dtgParameter.DataSource, ArrayList)(e.Item.ItemIndex), DiscountProposalParameter)

            Dim lblID As Label = CType(e.Item.FindControl("lblId"), Label)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblType As Label = CType(e.Item.FindControl("lblType"), Label)
            Dim lblParamName As Label = CType(e.Item.FindControl("lblParamName"), Label)
            Dim lbtnStatus As LinkButton

            lblID.Text = objDPParam.ID
            lblNo.Text = e.Item.ItemIndex + 1 + (dtgParameter.CurrentPageIndex * dtgParameter.PageSize)
            lblType.Text = getParameterTypeDesc(objDPParam.ParameterType)
            lblParamName.Text = objDPParam.ParameterName

            If objDPParam.Status = 1 Then
                lbtnStatus = CType(e.Item.FindControl("lbtnNonActive"), LinkButton)
                lbtnStatus.Visible = False
            Else
                lbtnStatus = CType(e.Item.FindControl("lbtnActive"), LinkButton)
                lbtnStatus.Visible = False
            End If
        End If
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        dtgParameter.CurrentPageIndex = 0
        BindDataGrid(0, getSearchCriterias())
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ddlType.ClearSelection()
        ddlStatus.ClearSelection()
        txtParameterName.Text = ""
        ddlType.Enabled = True

        If Not IsNothing(CType(_sessHelper.GetSession("EDITPARAM"), DiscountProposalParameter)) Then
            _sessHelper.RemoveSession("EDITPARAM")
        End If

        BindDataGrid(0, getSearchCriterias())
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim objDPParamToSave As DiscountProposalParameter = New DiscountProposalParameter
        Dim result As Integer = 0
        Dim isEdit As Boolean = False
        If Not IsNothing(_sessHelper.GetSession("EDITPARAM")) Then
            objDPParamToSave = CType(_sessHelper.GetSession("EDITPARAM"), DiscountProposalParameter)
            isEdit = True
        End If

        If ddlType.SelectedValue = -1 Then
            MessageBox.Show("Silahkan pilih Tipe")
            Exit Sub
        Else
            objDPParamToSave.ParameterType = ddlType.SelectedValue
        End If

        If txtParameterName.Text.Trim = "" Then
            MessageBox.Show("Silahkan isi nama parameter")
            Exit Sub
        Else
            objDPParamToSave.ParameterName = txtParameterName.Text
        End If

        If ddlStatus.SelectedValue = -1 Then
            MessageBox.Show("Silahkan pilih Status")
            Exit Sub
        Else
            objDPParamToSave.Status = ddlStatus.SelectedValue
        End If

        If isEdit Then
            result = New DiscountProposalParameterFacade(User).Update(objDPParamToSave)
        Else
            result = New DiscountProposalParameterFacade(User).Insert(objDPParamToSave)
        End If

        If result > 0 Then
            MessageBox.Show("Simpan data sukses")
            btnCancel_Click(New Object(), New System.EventArgs)
            BindDataGrid(0)
        Else
            MessageBox.Show("Simpan data gagal")
        End If
    End Sub

    Protected Sub dtgParameter_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgParameter.PageIndexChanged
        dtgParameter.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgParameter.CurrentPageIndex, getSearchCriterias())
    End Sub

    Protected Sub dtgParameter_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgParameter.ItemCommand
        If e.CommandName <> "Page" Then
            Dim lblID As Label = CType(e.Item.FindControl("lblId"), Label)
            Dim objDPParam As DiscountProposalParameter = New DiscountProposalParameterFacade(User).Retrieve(CInt(lblID.Text))
            If e.CommandName = "Activate" Then
                objDPParam.Status = 1
            ElseIf e.CommandName = "Deactivate" Then
                objDPParam.Status = 0
            ElseIf e.CommandName = "Edit" Then
                ddlType.SelectedValue = objDPParam.ParameterType
                ddlType.Enabled = False
                txtParameterName.Text = objDPParam.ParameterName
                ddlStatus.SelectedValue = objDPParam.Status
                _sessHelper.SetSession("EDITPARAM", objDPParam)
                Exit Sub
            End If

            Dim result As Integer = New DiscountProposalParameterFacade(User).Update(objDPParam)
            If result > 0 Then
                BindDataGrid(dtgParameter.CurrentPageIndex, getSearchCriterias())
            Else
                MessageBox.Show("Gagal merubah status")
            End If
        End If
    End Sub
End Class