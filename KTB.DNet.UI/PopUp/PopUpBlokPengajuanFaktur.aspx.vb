Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.FinishUnit

Public Class PopUpBlokPengajuanFaktur
    Inherits System.Web.UI.Page

    Private _sessHelper As New SessionHelper
    Private blockFaktur As String = "Block_Faktur_"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub DisplayPopUpDataGrid()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        CreateCriteria(criterias)
        _sessHelper.SetSession("PPCHASSISMASTER", criterias)
        dtgPPBlokPengajuanFaktur.CurrentPageIndex = 0
        DisplayDataSearch(dtgPPBlokPengajuanFaktur.CurrentPageIndex)
    End Sub

    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)
        If txtChassisNumber.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Partial, txtChassisNumber.Text.Trim))
        End If

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "FakturStatus", MatchType.Exact, 0))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "PendingDesc", MatchType.NotLike, blockFaktur))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "EndCustomerID", MatchType.IsNull, ""))
    End Sub

    Private Function RemoveCharacterBlock(ByVal pendingDesc As String) As String
        Dim strValue As String
        If Not pendingDesc = "" Then
            strValue = pendingDesc.Replace(blockFaktur, "")
        End If
        Return strValue
    End Function

    Private Sub DisplayDataSearch(ByVal indexPage As Integer)
        Dim arrList As ArrayList
        Dim arrListSource As New ArrayList
        Dim totalRow As Integer = 0

        If indexPage >= 0 Then
            arrList = New ChassisMasterFacade(User).RetrieveByCriteria(CType(_sessHelper.GetSession("PPCHASSISMASTER"), CriteriaComposite), indexPage + 1, dtgPPBlokPengajuanFaktur.PageSize, totalRow)

            For Each model As ChassisMaster In arrList
                Dim obj As New ChassisMaster
                obj.ID = model.ID
                obj.ChassisNumber = model.ChassisNumber
                obj.PendingDesc = RemoveCharacterBlock(model.PendingDesc)

                arrListSource.Add(obj)
            Next

            dtgPPBlokPengajuanFaktur.VirtualItemCount = totalRow
            dtgPPBlokPengajuanFaktur.DataSource = arrListSource
            dtgPPBlokPengajuanFaktur.DataBind()
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        DisplayPopUpDataGrid()
    End Sub

    Private Sub dtgPPBlokPengajuanFaktur_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgPPBlokPengajuanFaktur.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""rbSelectChassing"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
        End If
    End Sub
End Class