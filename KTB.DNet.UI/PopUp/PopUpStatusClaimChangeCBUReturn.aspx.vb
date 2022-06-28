Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General

Public Class PopUpStatusClaimChangeCBUReturn
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BindData(Request.QueryString("ID"))
    End Sub

    Sub BindData(ByVal ID As String)
        Dim arl As New ArrayList
        Dim oHeader As ChassisMasterClaimHeader = New ChassisMasterClaimHeaderFacade(User).Retrieve(CInt(ID))
        arl = New StatusChangeHistoryFacade(User).RetrieveByDocumentRegNumber(oHeader.ClaimNumber, "19")
        lblClaimNumber.Text = oHeader.ClaimNumber
        dgDetails.DataSource = arl
        dgDetails.DataBind()
    End Sub

    Private Sub dgDetails_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDetails.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            e.Item.Cells(0).Controls.Add(lNum)
            Dim Obj As StatusChangeHistory = CType(e.Item.DataItem, StatusChangeHistory)
            Dim lblOldStatus As Label = CType(e.Item.FindControl("lblOldStatus"), Label)
            Dim lblNewStatus As Label = CType(e.Item.FindControl("lblNewStatus"), Label)
            If Obj.OldStatus = -1 Then
                lblOldStatus.Text = ""
            Else
                lblOldStatus.Text = CType(Obj.OldStatus, EnumCBUReturn.StatusClaim).ToString()
            End If
            lblNewStatus.Text = CType(Obj.NewStatus, EnumCBUReturn.StatusClaim).ToString()
        End If
    End Sub
End Class