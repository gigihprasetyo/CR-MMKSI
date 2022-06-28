#Region " Customer Name Space "

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports System.IO
Imports System.Text
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
#End Region

Public Class FrmDurasiPM
    Inherits System.Web.UI.Page

#Region "Private variable"

    Private _sessHelper As SessionHelper = New SessionHelper
    Private viewPriv As Boolean
    Private inputPriv As Boolean
#End Region

    '---------------------------------------------------------------------------------------------------------'
#Region "Privilage"
    Private Sub InitiateAuthorization()
        If CType(Session("Dealer"), Dealer).Title = EnumDealerTittle.DealerTittle.DEALER Then
            If Not SecurityProvider.Authorize(Context.User, SR.PMKindDuration_View_Privilage) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Durasi PM")
            End If
        Else
            If Not SecurityProvider.Authorize(Context.User, SR.PMKindDuration_View_Privilage) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Durasi PM")
            Else
                viewPriv = SecurityProvider.Authorize(Context.User, SR.PMKindDuration_View_Privilage)
            End If
            If Not SecurityProvider.Authorize(Context.User, SR.PMKindDuration_Input_Privilage) Then
                inputPriv = False
            Else
                inputPriv = SecurityProvider.Authorize(Context.User, SR.PMKindDuration_Input_Privilage)
            End If
        End If
    End Sub
#End Region

#Region "CUSTOM sub/func"

    Private Sub bindDdlPMKind()
        Dim arrPMKind As ArrayList = New ArrayList

        arrPMKind = _sessHelper.GetSession("ARRPMKIND")

        If IsNothing(arrPMKind) OrElse arrPMKind.Count = 0 Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(PMKind), "KindCode", Sort.SortDirection.ASC))
            Dim PMKindFacade As PMKindFacade = New PMKindFacade(User)
            arrPMKind = PMKindFacade.Retrieve(criterias, sortColl)
            _sessHelper.SetSession("ARRPMKIND", arrPMKind)
        End If

        ddlPMKind.Items.Clear()
        ddlPMKind.Items.Add(New ListItem("Silahkan pilih", -1))

        For Each item As PMKind In arrPMKind
            Dim kindItem As New ListItem(item.KindCode + " - " + item.KindDescription, item.ID)
            ddlPMKind.Items.Add(kindItem)
        Next
    End Sub

    Private Sub bindDdlCategory()
        Dim arrCategory As ArrayList = New ArrayList

        arrCategory = _sessHelper.GetSession("ARRCATEGORY")

        If IsNothing(arrCategory) OrElse arrCategory.Count = 0 Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Category), "ProductCategory", MatchType.Exact, 1))
            Dim categoryFacade As CategoryFacade = New CategoryFacade(User)
            arrCategory = categoryFacade.Retrieve(criterias)
            _sessHelper.SetSession("ARRCATEGORY", arrCategory)
        End If

        ddlCategory.Items.Clear()
        ddlCategory.Items.Add(New ListItem("Silahkan pilih", -1))

        For Each item As Category In arrCategory
            Dim categoryItem As New ListItem(item.CategoryCode, item.ID)
            ddlCategory.Items.Add(categoryItem)
        Next
    End Sub

    Private Function validateDataForSave() As Boolean
        If ddlPMKind.SelectedValue = -1 Then
            MessageBox.Show("Silahkan pilih jenis PM !")
            Return False
        End If

        If ddlCategory.SelectedValue = -1 Then
            MessageBox.Show("Silahkan pilih kategori !")
            Return False
        End If

        If txtDuration.Text = "" OrElse String.IsNullorEmpty(txtDuration.Text) Then
            MessageBox.Show("Silahkan isi durasi !")
            Return False
        End If

        Return True
    End Function

    Private Sub initDgPMKindDuration()
        Dim searchCriteria As CriteriaComposite = CType(_sessHelper.GetSession("PMKINDDURATION_SEARCH"), CriteriaComposite)
        Dim criteria As CriteriaComposite

        If Not IsNothing(searchCriteria) Then
            criteria = searchCriteria
        Else
            criteria = New CriteriaComposite(New Criteria(GetType(PMKindDuration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        End If

        Dim arrPMKindDuration As ArrayList = New PMKindDurationFacade(User).Retrieve(criteria)

        _sessHelper.SetSession("ARRPMKINDDURATION", arrPMKindDuration)

        dgPMDuration.DataSource = arrPMKindDuration
        dgPMDuration.DataBind()
    End Sub

    Private Sub bindDgPMKindDuration()

    End Sub

    Private Function validateDurasiPM(ByVal pMKindToValidate As PMKindDuration) As Boolean
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMKindDuration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(PMKindDuration), "Category", MatchType.Exact, pMKindToValidate.Category.ID))
        criteria.opAnd(New Criteria(GetType(PMKindDuration), "PMKind", MatchType.Exact, pMKindToValidate.PMKind.ID))
        Dim arrPMKindDuration As ArrayList = New PMKindDurationFacade(User).Retrieve(criteria)

        If IsNothing(arrPMKindDuration) OrElse arrPMKindDuration.Count = 0 Then
            Return True
        Else
            Dim pMKindDuration_edit As PMKindDuration = CType(_sessHelper.GetSession("PMKINDDURATION_EDIT"), PMKindDuration)
            If Not IsNothing(pMKindDuration_edit) Then
                pMKindDuration_edit.Category.ID = CType(arrPMKindDuration(0), PMKindDuration).Category.ID
                pMKindDuration_edit.PMKind.ID = CType(arrPMKindDuration(0), PMKindDuration).PMKind.ID
                Return True
            End If

            Return False
        End If



    End Function

#End Region

#Region "EVENT"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            bindDdlPMKind()
            bindDdlCategory()

            initDgPMKindDuration()

            If inputPriv Then
                btnSave.Enabled = True
            Else
                btnSave.Enabled = False
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If validateDataForSave() Then
            Dim result As Integer = 0
            Dim pMKindDurationFacade As PMKindDurationFacade = New PMKindDurationFacade(User)
            Dim pmKindDuration As PMKindDuration = New PMKindDuration
            Dim pMKind As PMKind = New PMKind
            Dim category As Category = New Category
            pMKind.ID = ddlPMKind.SelectedValue
            category.ID = ddlCategory.SelectedValue

            'check if edit
            Dim pMKindDuration_edit As PMKindDuration = CType(_sessHelper.GetSession("PMKINDDURATION_EDIT"), PMKindDuration)
            If Not IsNothing(pMKindDuration_edit) Then
                pmKindDuration = pMKindDuration_edit
            End If

            pmKindDuration.PMKind = pMKind
            pmKindDuration.Category = category

            If txtDuration.Text > 65000 Then
                MessageBox.Show("Durasi tidak boleh lebih dari 65.000 hari")
                Return
            End If

            pmKindDuration.Duration = txtDuration.Text

            If validateDurasiPM(pmKindDuration) Then
                If Not IsNothing(pMKindDuration_edit) Then
                    result = pMKindDurationFacade.Update(pmKindDuration)
                Else
                    result = pMKindDurationFacade.Insert(pmKindDuration)
                End If

                If result > 0 Then
                    MessageBox.Show("Simpan data berhasil")
                    _sessHelper.RemoveSession("PMKINDDURATION_EDIT")
                    bindDdlPMKind()
                    bindDdlCategory()
                    txtDuration.Text = ""
                    btnSave.Enabled = True
                    initDgPMKindDuration()
                Else
                    MessageBox.Show("Simpan data gagal")
                End If
            Else
                MessageBox.Show("Duarsi PM dengan Category dan PMKind yang sama telah ada")
            End If
        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        bindDdlPMKind()
        bindDdlCategory()
        txtDuration.Text = ""
        If inputPriv Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
        _sessHelper.RemoveSession("PMKINDDURATION_EDIT")
        _sessHelper.RemoveSession("PMKINDDURATION_SEARCH")
    End Sub

    Protected Sub dgPMDuration_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgPMDuration.ItemCommand
        Dim arrPMKindDuration As ArrayList = CType(_sessHelper.GetSession("ARRPMKINDDURATION"), ArrayList)
        Dim tempPMKindDuration As PMKindDuration = New PMKindDuration

        If e.CommandName = "Edit" Then
            _sessHelper.RemoveSession("PMKINDDURATION_EDIT")
            tempPMKindDuration = arrPMKindDuration(e.Item.ItemIndex)
            ddlPMKind.SelectedValue = tempPMKindDuration.PMKind.ID
            ddlCategory.SelectedValue = tempPMKindDuration.Category.ID
            txtDuration.Text = tempPMKindDuration.Duration
            _sessHelper.SetSession("PMKINDDURATION_EDIT", tempPMKindDuration)

        ElseIf e.CommandName = "View" Then
            tempPMKindDuration = arrPMKindDuration(e.Item.ItemIndex)
            ddlPMKind.SelectedValue = tempPMKindDuration.PMKind.ID
            ddlCategory.SelectedValue = tempPMKindDuration.Category.ID
            txtDuration.Text = tempPMKindDuration.Duration

            btnSave.Enabled = False
        ElseIf e.CommandName = "Delete" Then
            tempPMKindDuration = arrPMKindDuration(e.Item.ItemIndex)
            Dim pMKindDurationFacade As PMKindDurationFacade = New PMKindDurationFacade(User)
            pMKindDurationFacade.Delete(tempPMKindDuration)
            initDgPMKindDuration()
        End If
    End Sub

    Protected Sub dgPMDuration_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgPMDuration.ItemDataBound
        Dim lblNo As Label
        Dim lblPMKind As Label
        Dim lblCategory As Label
        Dim lblDuration As Label
        Dim lbtnEdit As LinkButton
        Dim lbtnDelete As LinkButton
        Dim index = e.Item.ItemIndex
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        Dim arrPMKindDuration As ArrayList = CType(_sessHelper.GetSession("ARRPMKINDDURATION"), ArrayList)
        Dim tempPMKindDuration As PMKindDuration = New PMKindDuration

        If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem Then
            lblNo = CType(e.Item.FindControl("lblNo"), Label)
            lblPMKind = CType(e.Item.FindControl("lblPMKind"), Label)
            lblCategory = CType(e.Item.FindControl("lblCategory"), Label)
            lblDuration = CType(e.Item.FindControl("lblDuration"), Label)

            tempPMKindDuration = arrPMKindDuration(index)

            lblNo.Text = index + 1
            lblPMKind.Text = tempPMKindDuration.PMKind.KindCode
            lblCategory.Text = tempPMKindDuration.Category.CategoryCode
            lblDuration.Text = tempPMKindDuration.Duration

            If Not inputPriv Then
                lbtnDelete = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                lbtnEdit = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                lbtnEdit.Enabled = False
                lbtnDelete.Enabled = False
                lbtnDelete.Visible = False
                lbtnEdit.Visible = False
            End If
        End If

    End Sub


    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMKindDuration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If ddlPMKind.SelectedValue <> -1 Then
            criteria.opAnd(New Criteria(GetType(PMKindDuration), "PMKind", MatchType.Exact, CInt(ddlPMKind.SelectedValue)))
        End If

        If ddlCategory.SelectedValue <> -1 Then
            criteria.opAnd(New Criteria(GetType(PMKindDuration), "Category", MatchType.Exact, CInt(ddlCategory.SelectedValue)))
        End If

        If txtDuration.Text <> "" Then
            criteria.opAnd(New Criteria(GetType(PMKindDuration), "Duration", MatchType.Exact, CInt(txtDuration.Text)))
        End If

        _sessHelper.SetSession("PMKINDDURATION_SEARCH", criteria)
        initDgPMKindDuration()
    End Sub

#End Region

End Class