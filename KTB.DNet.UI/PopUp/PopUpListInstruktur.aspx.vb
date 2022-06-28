#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.General

#End Region

Public Class PopUpListInstruktur
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindDataGrid()
        End If
    End Sub

    Private Sub BindDataGrid()

        Dim mrtcId As Integer = CInt(Request.QueryString("mrtcId"))

        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrMRTCPIC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrMRTCPIC), "TrMRTC.ID", MatchType.Exact, mrtcId))
        criterias.opAnd(New Criteria(GetType(TrMRTCPIC), "Type", MatchType.Exact, CInt(EnumTrMRTC.TypePIC.Instruktur)))

        Dim arlResult As ArrayList = New TrMRTCPICFacade(User).Retrieve(criterias)

        dtgUserInfo.DataSource = arlResult
        dtgUserInfo.DataBind()
    End Sub

    Private Sub dtgUserInfo_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgUserInfo.ItemDataBound
        Try
            If Not e.Item.DataItem Is Nothing Then
                Dim data As TrMRTCPIC = CType(e.Item.DataItem, TrMRTCPIC)

                Dim lblNama As Label = CType(e.Item.FindControl("lblNama"), Label)
                lblNama.Text = data.TrTrainee.Name
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class