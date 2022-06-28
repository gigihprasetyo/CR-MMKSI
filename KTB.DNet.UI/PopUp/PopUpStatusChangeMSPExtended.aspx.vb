Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General

Public Class PopUpStatusChangeMSPExtended
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BindData(Request.QueryString("ID"))
    End Sub

    Sub BindData(ByVal ID As String)
        Dim arl As New ArrayList
        Dim oMSPExRegistration As MSPExRegistration = New MSPExRegistrationFacade(User).Retrieve(CInt(ID))
        arl = New StatusChangeHistoryFacade(User).RetrieveByDocumentRegNumber(oMSPExRegistration.RegNumber, "1")
        lblClaimNumber.Text = "MSP Extended Registration"
        lblTipeMSP.Text = oMSPExRegistration.MSPExMaster.MSPExType.Description
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
                lblOldStatus.Text = EnumMSPEx.GetStringValue(Obj.OldStatus)
            End If
            lblNewStatus.Text = EnumMSPEx.GetStringValue(Obj.NewStatus)
        End If
    End Sub
End Class