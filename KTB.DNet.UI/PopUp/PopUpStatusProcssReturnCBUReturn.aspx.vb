Imports System.Linq
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General

Public Class PopUpStatusProcssReturnCBUReturn
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BindData(Request.QueryString("ID"))
    End Sub

    Sub BindData(ByVal ID As String)
        Dim arl As New ArrayList
        Dim oHeader As ChassisMasterClaimHeader = New ChassisMasterClaimHeaderFacade(User).Retrieve(CInt(ID))
        lblClaimNumber.Text = oHeader.ClaimNumber
        arl = GenerateData(oHeader)
        dgDetails.DataSource = arl
        dgDetails.DataBind()
    End Sub

    Private Sub dgDetails_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDetails.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            e.Item.Cells(0).Controls.Add(lNum)
            Dim Obj As Hashtable = CType(e.Item.DataItem, Hashtable)
            Dim lblProses As Label = CType(e.Item.FindControl("lblProses"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lblProcessDate As Label = CType(e.Item.FindControl("lblProcessDate"), Label)
            Dim lblProcessBy As Label = CType(e.Item.FindControl("lblProcessBy"), Label)

            lblProses.Text = Obj.Item("Proses").ToString.Replace("_", " ")
            lblStatus.Text = Obj.Item("Status").ToString
            lblProcessDate.Text = Obj.Item("Tanggal").ToString
            lblProcessBy.Text = Obj.Item("By").ToString
        End If
    End Sub

    Private Function GenerateData(ByVal oHeader As ChassisMasterClaimHeader) As ArrayList
        Dim _return As New ArrayList
        Dim arl As ArrayList = New StatusChangeHistoryFacade(User).RetrieveByDocumentRegNumber(oHeader.ClaimNumber, "20")

        For i As Integer = 0 To 6
            Dim _hash As New Hashtable
            If arl.Count > 0 Then
                Dim query As StatusChangeHistory = (From oData As StatusChangeHistory In arl
                                Where oData.OldStatus = i
                                Order By oData.LastUpdateTime Descending
                                Select oData).FirstOrDefault()
                If Not IsNothing(query) Then
                    _hash.Add("Proses", CType(i, EnumCBUReturn.StatusProsesRetur).ToString)
                    _hash.Add("Status", EnumCBUReturn.StatusClaim.Proses.ToString)
                    _hash.Add("Tanggal", query.CreatedTime.ToString("dd/MM/yyyy HH:mm"))
                    _hash.Add("By", query.CreatedBy)
                Else
                    _hash.Add("Proses", CType(i, EnumCBUReturn.StatusProsesRetur).ToString)
                    _hash.Add("Status", "N/A")
                    _hash.Add("Tanggal", "N/A")
                    _hash.Add("By", "N/A")
                End If
            Else
                _hash.Add("Proses", CType(i, EnumCBUReturn.StatusProsesRetur).ToString)
                _hash.Add("Status", "N/A")
                _hash.Add("Tanggal", "N/A")
                _hash.Add("By", "N/A")
            End If
            _return.Add(_hash)
        Next

        Return _return
    End Function

End Class