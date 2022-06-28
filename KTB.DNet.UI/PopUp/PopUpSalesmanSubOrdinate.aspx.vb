Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class PopUpSalesmanSubOrdinate
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        'Put user code to initialize the page here
        If Not IsPostBack Then
            BindDataGrid()
        End If
    End Sub


    Private sessHelper As New SessionHelper
    Private Sub BindDataGrid()
        Dim func As New SalesmanHeaderFacade(Me.User)
        Dim salesmanHeaderID As Integer = CInt(Request.QueryString("id"))

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "LeaderId", MatchType.Exact, salesmanHeaderID))
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.Exact, 2))

        Dim arrSalesman As ArrayList = func.Retrieve(criterias)

        Dim objSalesmanHeader As SalesmanHeader = func.Retrieve(salesmanHeaderID)
        lblSalesmanCode.Text = objSalesmanHeader.SalesmanCode
        lblNama.Text = objSalesmanHeader.Name

        Dim objAtasan As SalesmanHeader = func.Retrieve(objSalesmanHeader.LeaderId)
        sessHelper.SetSession("atasan", objAtasan)
        If arrSalesman.Count > 0 Then
            dgSalesman.DataSource = arrSalesman
            dgSalesman.PageSize = arrSalesman.Count
            dgSalesman.DataBind()

        End If

    End Sub

    Private Sub dgSalesman_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgSalesman.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim ddlAtasan As DropDownList = e.Item.FindControl("ddlAtasan")
            Dim sls As SalesmanHeader = CType(e.Item.DataItem, SalesmanHeader)
            Dim positionId As Integer = sls.JobPosition.ID

            If positionId = 2 Then
                positionId = 3
            End If

            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "ID", MatchType.No, sls.LeaderId))
            If Not IsNothing(sls.Dealer.ParentDealer) Then
                criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.ID", MatchType.InSet, "(" & sls.Dealer.ID & "," & sls.Dealer.ParentDealer.ID & ")"))
            Else
                criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.ID", MatchType.Exact, sls.Dealer.ID))
            End If
            'criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.ID", MatchType.Exact, sls.Dealer.ID))
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "JobPosition.ID", MatchType.Greater, positionId))
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "JobPosition.ID", MatchType.LesserOrEqual, positionId + 2))
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.Exact, CType(EnumSalesmanStatus.SalesmanStatus.Aktif, String)))
            'special for salesman unit
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, 1))

            Dim arrAtasan As ArrayList = New SalesmanHeaderFacade(Me.User).Retrieve(criterias)
            ddlAtasan.ClearSelection()
            ddlAtasan.Items.Clear()

            For Each iSls As SalesmanHeader In arrAtasan
                ddlAtasan.Items.Add(New ListItem(iSls.Name + " (" + iSls.JobPosition.Description + ")", iSls.ID.ToString()))
            Next
            
        End If
    End Sub

    Protected Sub btnProses_ServerClick(sender As Object, e As EventArgs)
        Try
            Dim func As New SalesmanHeaderFacade(Me.User)
            'Dim salesmanHeaderID As Integer = CInt(Request.QueryString("id"))

            'Dim objAtasan As SalesmanHeader = CType(sessHelper.GetSession("atasan"), SalesmanHeader)

            'Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(SalesmanHeader), "LeaderId", MatchType.Exact, salesmanHeaderID))
            'criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.Exact, 2))

            For Each item As DataGridItem In dgSalesman.Items
                Dim ddlAtasan As DropDownList = item.FindControl("ddlAtasan")
                Dim txtSalesmanCode As TextBox = item.FindControl("txtSalesmanCode")

                Dim objSls As SalesmanHeader = New SalesmanHeaderFacade(Me.User).Retrieve(txtSalesmanCode.Text)
                If ddlAtasan.Items.Count > 0 Then
                    objSls.LeaderId = CInt(ddlAtasan.SelectedValue)
                Else
                    objSls.LeaderId = Nothing
                End If

                func.Update(objSls)
            Next

            MessageBox.Show("Proses perubahan atasan berhasil")
        Catch ex As Exception
            MessageBox.Show("Proses perubahan atasan gagal")
        End Try
    End Sub

End Class