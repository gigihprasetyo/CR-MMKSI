#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General
#End Region

Public Class FrmPartIncidentalDetailPO
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents dgPartList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblReqNumber As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private sessHelper As New SessionHelper
    Private intRemainQuantity As Integer = 0

    Private Sub BindDataGrid(ByVal idx As Integer)
        Dim totalRow As Integer = 0
        Dim obj As PartIncidentalPO
        'obj.PartIncidentalDetail.ID()
        Dim idPID As Integer = CInt(sessHelper.GetSession("IDPartIncidentalDetail"))
        Dim criterias As New CriteriaComposite(New Criteria(GetType(PartIncidentalPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PartIncidentalPO), "PartIncidentalDetail.ID", MatchType.Exact, idPID))
        Dim arrList As New ArrayList
        arrList = New PartIncidentalPOFacade(User).RetrieveByCriteria(criterias, idx + 1, _
            dgPartList.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        sessHelper.SetSession("ArrListDownloadPIPO", arrList)

        If arrList.Count > 0 Then
            Try
                lblDealerCode.Text = CType(arrList(0), PartIncidentalPO).PartIncidentalDetail.PartIncidentalHeader.Dealer.DealerCode
            Catch ex As Exception
                lblDealerCode.Text = ""
            End Try
            Try
                lblReqNumber.Text = CType(arrList(0), PartIncidentalPO).PartIncidentalDetail.PartIncidentalHeader.RequestNumber
            Catch ex As Exception
                lblReqNumber.Text = ""
            End Try
        End If

        dgPartList.DataSource = arrList
        dgPartList.VirtualItemCount = totalRow
        dgPartList.DataBind()
    End Sub
    Private Sub CheckPrivilege()

    End Sub
    Private Sub Initialize()
        ViewState("CurrentSortColumn") = "PartIncidentalDetail.PartIncidentalHeader.Dealer.DealerCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub
    Private Function getSparePartMaster(ByVal id As Integer) As SparePartMaster
        Dim spMasterFacade As SparePartMasterFacade = New SparePartMasterFacade(User)
        Return spMasterFacade.Retrieve(id)
    End Function

  

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        If Not IsPostBack Then
            Initialize()
            BindDataGrid(0)
        End If
    End Sub
    Private Sub dgPartList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPartList.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)


        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As PartIncidentalPO = CType(e.Item.DataItem, PartIncidentalPO)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgPartList.CurrentPageIndex * dgPartList.PageSize)
                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                lblDealerCode.Text = RowValue.PartIncidentalDetail.PartIncidentalHeader.Dealer.DealerCode
                Dim lblReqNo As Label = CType(e.Item.FindControl("lblReqNo"), Label)
                lblReqNo.Text = RowValue.PartIncidentalDetail.PartIncidentalHeader.RequestNumber
                Dim lblPKDate As Label = CType(e.Item.FindControl("lblPKDate"), Label)
                If RowValue.PartIncidentalDetail.PartIncidentalHeader.IncidentalDate < New DateTime(1900, 1, 1, 0, 0, 0) Then
                    lblPKDate.Text = ""
                Else
                    lblPKDate.Text = RowValue.PartIncidentalDetail.PartIncidentalHeader.IncidentalDate
                End If
                Dim lblPartNo As Label = CType(e.Item.FindControl("lblPartNo"), Label)
                lblPartNo.Text = RowValue.PartIncidentalDetail.SparePartMaster.PartNumber
                Dim lblPartName As Label = CType(e.Item.FindControl("lblPartName"), Label)
                lblPartName.Text = RowValue.PartIncidentalDetail.SparePartMaster.PartName
                Dim lblPlanDate As Label = CType(e.Item.FindControl("lblPlanDate"), Label)
                If RowValue.PartIncidentalDetail.PlanDate < New DateTime(1900, 1, 1, 0, 0, 0) Then
                    lblPlanDate.Text = ""
                Else
                    lblPlanDate.Text = RowValue.PartIncidentalDetail.PlanDate
                End If


                Dim lblPartSubNo As Label = CType(e.Item.FindControl("lblPartSubNo"), Label)
                Dim lblPartSubName As Label = CType(e.Item.FindControl("lblPartSubName"), Label)

                If RowValue.PartIncidentalDetail.SparePartMasterSubstitutionID > 0 Then
                    Dim spMasterSub As SparePartMaster = getSparePartMaster(RowValue.PartIncidentalDetail.SparePartMasterSubstitutionID)
                    If spMasterSub.ID > 0 Then
                        lblPartSubNo.Text = spMasterSub.PartNumber
                        lblPartSubName.Text = spMasterSub.PartName
                    End If
                End If

                If intRemainQuantity > 0 Then
                    intRemainQuantity = intRemainQuantity - RowValue.Alocation
                Else
                    intRemainQuantity = intRemainQuantity + (RowValue.PartIncidentalDetail.Quantity - RowValue.PartIncidentalDetail.Alokasi - RowValue.Alocation)
                End If


                Dim lblOrgQty As Label = CType(e.Item.FindControl("lblOrgQty"), Label)
                lblOrgQty.Text = RowValue.PartIncidentalDetail.Quantity
                Dim lblRemainQty As Label = CType(e.Item.FindControl("lblRemainQty"), Label)
                'lblRemainQty.Text = RowValue.PartIncidentalDetail.RemainQuantity
                lblRemainQty.Text = intRemainQuantity.ToString()
                Dim lblAlokasi As Label = CType(e.Item.FindControl("lblAlokasi"), Label)
                lblAlokasi.Text = RowValue.Alocation
            End If
        End If
    End Sub
    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("FrmPartIncidentalListPO.aspx")
    End Sub
    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Response.Redirect("FrmDownloadPartIncidentalPO.aspx")
    End Sub
End Class
