Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports System.Collections.Generic
Imports System.Linq
Imports KTB.DNet.BusinessFacade.Helper

Public Class FrmServiceTemplateFS
    Inherits System.Web.UI.Page
    Private sessHelper As SessionHelper = New SessionHelper
    Private m_bLihatPrivilege As Boolean = False
    Private crit As CriteriaComposite
    Private objDealer As New Dealer
    Private oLoginUser As New UserInfo
    Private serviceTemplateFSLaborFacade As ServiceTemplateFSLaborFacade = New ServiceTemplateFSLaborFacade(User)
    Private vW_ServiceTemplateFSLaborFacade As KTB.DNet.BusinessFacade.VW_ServiceTemplateLaborFacade = New KTB.DNet.BusinessFacade.VW_ServiceTemplateLaborFacade(User)
    Private fSKindFacade As FSKindFacade = New FSKindFacade(User)
    Private categoryFacade As CategoryFacade = New CategoryFacade(User)
    Private vechileModelFacade As VechileModelFacade = New VechileModelFacade(User)
    Private vechileTypeFacade As VechileTypeFacade = New VechileTypeFacade(User)

#Region "Event Handler"
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        If Not IsPostBack Then
            txtKodeDealer.Attributes.Add("readonly", True)
            If Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                txtKodeDealer.Text = objDealer.DealerCode
                lblPopupDealer.Visible = False
            Else
                txtKodeDealer.Text = ""
                lblPopupDealer.Visible = True
            End If

            'InitJenisFSDdl()
            InitKategoriDdl()
            ddlKategori_SelectedIndexChanged(Nothing, Nothing)
            ddlVehicleModel_SelectedIndexChanged(Nothing, Nothing)
            'RefreshGrid()
            ReadData()
            dgServiceTemplate.CurrentPageIndex = 0
            BindPage(dgServiceTemplate.CurrentPageIndex)
        End If
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs)
        'RefreshGrid()
        ReadData()
        dgServiceTemplate.CurrentPageIndex = 0
        BindPage(dgServiceTemplate.CurrentPageIndex)
    End Sub

    Protected Sub dgServiceTemplate_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs)
        'RefreshGrid(e.NewPageIndex)
        'dgServiceTemplate.CurrentPageIndex = e.NewPageIndex
        dgServiceTemplate.CurrentPageIndex = e.NewPageIndex
        BindPage(e.NewPageIndex)
    End Sub

    Protected Sub dgServiceTemplate_SortCommand(source As Object, e As DataGridSortCommandEventArgs)
        If e.SortExpression = ViewState.Item("SortCol") Then
            If ViewState.Item("SortDirection") = Sort.SortDirection.ASC Then
                ViewState.Item("SortDirection") = Sort.SortDirection.DESC
            Else
                ViewState.Item("SortDirection") = Sort.SortDirection.ASC
            End If
        End If
        ViewState.Item("SortCol") = e.SortExpression
        dgServiceTemplate.SelectedIndex = -1
        dgServiceTemplate.CurrentPageIndex = 0
        BindPage(0)
    End Sub

    Protected Sub dgServiceTemplate_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
        Dim RowValue As VW_ServiceTemplateLabor = CType(e.Item.DataItem, VW_ServiceTemplateLabor)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblJenisFS As Label = CType(e.Item.FindControl("lblJenisFS"), Label)
            Dim lbDetail As LinkButton = CType(e.Item.FindControl("lbDetail"), LinkButton)

            lblNo.Text = e.Item.ItemIndex + 1 + (dgServiceTemplate.PageSize * dgServiceTemplate.CurrentPageIndex)
            'lblJenisFS.Text = String.Format("{0} - {1}", RowValue.KindCode, RowValue.KindDescription)
            'lblJenisFS.ToolTip = String.Format("{0} - {1}", RowValue.FSKind.KindCode, RowValue.FSKind.KindDescription)
            lbDetail.OnClientClick = String.Format("showDetail('{0}','{1}','{2}','{3}');", RowValue.Dealer.DealerCode, RowValue.VechileTypeCode, RowValue.KindCode, RowValue.TemplateType)
        End If
    End Sub

    Protected Sub ddlKategori_SelectedIndexChanged(sender As Object, e As EventArgs)
        ddlVehicleModel.Items.Clear()

        If ddlKategori.SelectedIndex <> 0 Then
            crit = New CriteriaComposite(New Criteria(GetType(VechileModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(VechileModel), "Category.ID", MatchType.Exact, CShort(ddlKategori.SelectedValue)))

            Dim results As ArrayList = vechileModelFacade.Retrieve(crit)

            With ddlVehicleModel.Items
                For Each obj As VechileModel In results
                    .Add(New ListItem(String.Format("{0} - {1}", obj.VechileModelCode, obj.IndDescription), obj.ID))
                Next
            End With
        End If

        ddlVehicleModel.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Protected Sub ddlVehicleModel_SelectedIndexChanged(sender As Object, e As EventArgs)
        ddlVehicleType.Items.Clear()

        If ddlVehicleModel.SelectedIndex <> 0 Then
            crit = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.Exact, CShort(ddlVehicleModel.SelectedValue)))
            crit.opAnd(New Criteria(GetType(VechileType), "Category.ID", MatchType.Exact, CShort(ddlKategori.SelectedValue)))

            Dim results As ArrayList = vechileTypeFacade.Retrieve(crit)

            With ddlVehicleType.Items
                For Each obj As VechileType In results
                    .Add(New ListItem(obj.VechileTypeCode, obj.ID))
                Next
            End With
        End If

        ddlVehicleType.Items.Insert(0, "Silahkan Pilih")
    End Sub
#End Region

#Region "Custom Method"
    Private Sub CheckPrivilege()
        m_bLihatPrivilege = SecurityProvider.Authorize(Context.User, SR.ServiceTemplateFS_View_Privilage)

        If Not m_bLihatPrivilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Service Template - Free Service")
        End If
    End Sub
    Private Sub ReadData()
        Dim totalRow As Integer
        Dim ctrValues As New ArrayList

        crit = New CriteriaComposite(New Criteria(GetType(VW_ServiceTemplateLabor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If Not String.IsNullorEmpty(txtKodeDealer.Text) Then
            crit.opAnd(New Criteria(GetType(VW_ServiceTemplateLabor), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text))
        End If

        If ddlTipe.SelectedValue.ToString() <> "0" Then
            crit.opAnd(New Criteria(GetType(VW_ServiceTemplateLabor), "TemplateType", MatchType.Exact, "'" & ddlTipe.SelectedValue & "'"))
        End If

        If (txtKode.Text <> "") Then
            crit.opAnd(New Criteria(GetType(VW_ServiceTemplateLabor), "KindCode", MatchType.Exact, "'" & txtKode.Text & "'"))
        End If


        If ddlVehicleType.SelectedIndex <> 0 Then
            crit.opAnd(New Criteria(GetType(VW_ServiceTemplateLabor), "VechileTypeID", MatchType.Exact, CShort(ddlVehicleType.SelectedValue)))
        End If

        Dim data As ArrayList = vW_ServiceTemplateFSLaborFacade.Retrieve(crit)
        sessHelper.SetSession("ServiceTemplateList", data)
    End Sub

    Private Sub RefreshGrid(Optional indexPage As Integer = 0)
        
        'If data.Count = 0 Then
        '    dgServiceTemplate.CurrentPageIndex = 0
        'Else
        '    dgServiceTemplate.CurrentPageIndex = indexPage
        'End If

        'dgServiceTemplate.DataSource = data
        'dgServiceTemplate.VirtualItemCount = data.Count
        'dgServiceTemplate.DataBind()
    End Sub

    Private Sub BindPage(ByVal pageIndex As Integer)
        Dim arrServiceTemplate As ArrayList = CType(sessHelper.GetSession("ServiceTemplateList"), ArrayList)
        Dim aStatus As New ArrayList
        If arrServiceTemplate.Count <> 0 Then
            ' SortListControl(arrStallMaster, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrServiceTemplate, pageIndex, dgServiceTemplate.PageSize)
            dgServiceTemplate.DataSource = PagedList
            dgServiceTemplate.VirtualItemCount = arrServiceTemplate.Count()
            dgServiceTemplate.DataBind()
        Else
            dgServiceTemplate.DataSource = New ArrayList
            dgServiceTemplate.VirtualItemCount = 0
            dgServiceTemplate.CurrentPageIndex = 0
            dgServiceTemplate.DataBind()
        End If
    End Sub

    'Private Sub InitJenisFSDdl()
    '    ddlJenisFS.Items.Clear()

    '    crit = New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    Dim results As ArrayList = fSKindFacade.Retrieve(crit)

    '    With ddlJenisFS.Items
    '        For Each obj As FSKind In results
    '            .Add(New ListItem(String.Format("{0} - {1}", obj.KindCode, obj.KindDescription), obj.ID))
    '        Next
    '    End With

    '    ddlJenisFS.Items.Insert(0, "Silahkan Pilih")
    'End Sub

    Private Sub InitKategoriDdl()
        ddlKategori.Items.Clear()

        crit = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim results As ArrayList = categoryFacade.Retrieve(crit)

        With ddlKategori.Items
            For Each obj As Category In results
                .Add(New ListItem(obj.CategoryCode, obj.ID))
            Next
        End With

        ddlKategori.Items.Insert(0, "Silahkan Pilih")
    End Sub
#End Region
End Class