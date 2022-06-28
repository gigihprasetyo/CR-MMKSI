Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Service

Public Class PopUpServiceTemplateDetail
    Inherits System.Web.UI.Page
    Private serviceTemplateFSPartHeaderFacade As ServiceTemplateFSPartHeaderFacade = New ServiceTemplateFSPartHeaderFacade(User)
    Private vW_ServiceTemplateHeaderFacade As KTB.DNet.BusinessFacade.VW_ServiceTemplateHeaderFacade = New KTB.DNet.BusinessFacade.VW_ServiceTemplateHeaderFacade(User)
    Private crit As CriteriaComposite

#Region "Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblKodeDealer.Text = Request.QueryString("DealerCode").ToString
            RefreshGrid()
        End If
    End Sub

    Protected Sub dgServiceTemplateDet_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
        'Dim RowValue As ServiceTemplateFSPartDetail = CType(e.Item.DataItem, ServiceTemplateFSPartDetail)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgServiceTemplateDet.PageSize * dgServiceTemplateDet.CurrentPageIndex)
        End If
    End Sub
#End Region

#Region "Custom Method"
    'Private Sub RefreshGrid(Optional indexPage As Integer = 0)
    '    Dim totalRow As Integer

    '    crit = New CriteriaComposite(New Criteria(GetType(ServiceTemplateFSPartHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    crit.opAnd(New Criteria(GetType(ServiceTemplateFSPartHeader), "VechileType.ID", MatchType.Exact, CInt(Request.QueryString("vTypeId").ToString)))
    '    crit.opAnd(New Criteria(GetType(ServiceTemplateFSPartHeader), "FSKind.ID", MatchType.Exact, CInt(Request.QueryString("vFsKindId").ToString)))

    '    Dim data As ArrayList = serviceTemplateFSPartHeaderFacade.RetrieveByCriteria(crit, indexPage + 1, dgServiceTemplateDet.PageSize, totalRow, ViewState.Item("SortCol"), ViewState.Item("SortDirection"))
    '    Dim detail As ArrayList = New ArrayList

    '    If data.Count = 0 Then
    '        dgServiceTemplateDet.CurrentPageIndex = 0
    '    Else
    '        Dim obj As ServiceTemplateFSPartHeader = CType(data(0), ServiceTemplateFSPartHeader)
    '        lblFS.Text = String.Format("{0} - {1}", obj.FSKind.KindCode, obj.FSKind.KindDescription)
    '        lblTipe.Text = obj.VechileType.VechileTypeCode
    '        detail = obj.ServiceTemplateFSPartDetails
    '        dgServiceTemplateDet.CurrentPageIndex = indexPage
    '    End If

    '    dgServiceTemplateDet.DataSource = detail
    '    dgServiceTemplateDet.VirtualItemCount = totalRow
    '    dgServiceTemplateDet.DataBind()
    'End Sub

    Private Sub RefreshGrid(Optional indexPage As Integer = 0)
        Dim totalRow As Integer

        crit = New CriteriaComposite(New Criteria(GetType(VW_ServiceTemplateHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(VW_ServiceTemplateHeader), "VechileTypeCode", MatchType.Exact, "'" & Request.QueryString("vTypeId").ToString & "'"))
        crit.opAnd(New Criteria(GetType(VW_ServiceTemplateHeader), "KindCode", MatchType.Exact, "'" & Request.QueryString("vFsKindId").ToString & "'"))
        crit.opAnd(New Criteria(GetType(VW_ServiceTemplateHeader), "TemplateType", MatchType.Exact, "'" & Request.QueryString("TemplateType").ToString & "'"))

        Dim data As ArrayList = vW_ServiceTemplateHeaderFacade.Retrieve(crit)
        Dim detail As ArrayList = New ArrayList

        If data.Count = 0 Then
            dgServiceTemplateDet.CurrentPageIndex = 0
        Else
            Dim obj As VW_ServiceTemplateHeader = CType(data(0), VW_ServiceTemplateHeader)
            lblTipe.Text = obj.TemplateType 'String.Format("{0} - {1}", obj.FSKind.KindCode, obj.FSKind.KindDescription)
            lblTipeKendaraan.Text = obj.VechileTypeCode
            lblCode.Text = obj.KindCode & "(" & obj.KindDescription & ")"
            detail = obj.ServiceTemplateFSPartDetails
            dgServiceTemplateDet.CurrentPageIndex = indexPage
        End If

        dgServiceTemplateDet.DataSource = detail
        dgServiceTemplateDet.VirtualItemCount = totalRow
        dgServiceTemplateDet.DataBind()
    End Sub
#End Region
End Class