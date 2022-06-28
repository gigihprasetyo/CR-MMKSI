#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports System.IO
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions
#End Region

Public Class FrmCcCSAsignetoDealer
    Inherits System.Web.UI.Page

    Dim helpers As New TrainingHelpers(Me.Page)

    Private ReadOnly Property SalesmanHeaderID As Integer
        Get
            Try
                Return CInt(Request.QueryString("shID"))
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            InitPage()
            BindDataGrid()
        End If
    End Sub
    Private Sub InitPage()
        Dim sH As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(SalesmanHeaderID)
        lblTitle.Text = "Daftar CS Team - Asigne To Dealer"
        txtSalesmanCode.Text = sH.SalesmanCode
        txtName.Text = sH.Name
        txtPosition.Text = sH.JobPosition.Description
        txtSalesmanCode.Disabled()
        txtName.Disabled()
        txtPosition.Disabled()
    End Sub

    Private Sub BindDataGrid()
        dtgDealer.DataSource = New SalesmanHeaderToDealerFacade(User).GetDatabySalesmanHeader(SalesmanHeaderID)
        dtgDealer.DataBind()
    End Sub

    Protected Sub ddlDealer_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim ddl As DropDownList = CType(sender, DropDownList)
        Dim item As DataGridItem = CType(ddl.Parent.Parent, DataGridItem)
        If Not IsNothing(item) Then
            Dim lblDealerName As Label = item.FindLabel("lblDealerNameF")
            Dim lblKota As Label = item.FindLabel("lblKotaF")
            Dim lbtnAdd As LinkButton = item.FindLinkButton("lbtnAdd")

            If ddl.IsSelected Then
                Dim iDealer As Dealer = New DealerFacade(User).Retrieve(CInt(ddl.SelectedValue))
                lblDealerName.Text = iDealer.DealerName
                lblKota.Text = iDealer.City.CityName
                lbtnAdd.Visible = True
            Else
                lblDealerName.Text = String.Empty
                lblKota.Text = String.Empty
                lbtnAdd.Visible = False
            End If
        End If

    End Sub

    Private Sub dtgDealer_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgDealer.ItemCommand
        Select Case e.CommandName.ToLower()
            Case "tambah"
                Dim ddlDealer As DropDownList = CType(e.Item.FindControl("ddlDealer"), DropDownList)
                Dim sH As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(SalesmanHeaderID)
                Dim listData As List(Of SalesmanHeaderToDealer) = GetDataFromGrid()

                Dim newData As New SalesmanHeaderToDealer
                newData.SalesmanHeader = sH
                newData.Dealer = New DealerFacade(User).Retrieve(CInt(ddlDealer.SelectedValue))
                newData.IsMainDealer = False
                newData.Username = String.Empty
                newData.ID = 0
                listData.Add(newData)

                dtgDealer.DataSource = listData
                dtgDealer.DataBind()
            Case "hapus"
                Dim listData As List(Of SalesmanHeaderToDealer) = GetDataFromGrid()
                Dim lblDealerCode As Label = e.Item.FindLabel("lblDealerCode")
                listData.RemoveAll(Function(x) x.Dealer.DealerCode = lblDealerCode.Text.Trim)

                dtgDealer.DataSource = listData
                dtgDealer.DataBind()
        End Select
    End Sub

    Private Sub dtgDealer_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgDealer.ItemDataBound
        If e.IsRowItems Then
            Dim data As SalesmanHeaderToDealer = e.DataItem(Of SalesmanHeaderToDealer)()
            Dim hdnID As HiddenField = e.FindHiddenField("hdnID")
            Dim hdnIsMain As HiddenField = e.FindHiddenField("hdnIsMain")
            Dim lblDealerCode As Label = e.FindLabel("lblDealerCode")
            Dim lblDealerName As Label = e.FindLabel("lblDealerName")
            Dim lblKota As Label = e.FindLabel("lblKota")
            Dim txtUserName As TextBox = e.FindTextBox("txtUserName")
            Dim lbtnDelete As LinkButton = e.FindLinkButton("lbtnDelete")
            lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")

            hdnID.Value = data.ID.ToString
            hdnIsMain.Value = data.IsMainDealer.ToString()
            lblDealerCode.Text = data.Dealer.DealerCode
            lblDealerName.Text = data.Dealer.DealerName
            lblKota.Text = data.Dealer.City.CityName
            Dim pUserName As New SalesmanProfile
            If data.IsMainDealer Then
                If Not IsNothing(data.SalesmanHeader.SalesmanProfiles.Cast(Of SalesmanProfile).FirstOrDefault(Function(x) x.ProfileHeader.ID = 53)) Then
                    pUserName = data.SalesmanHeader.SalesmanProfiles.Cast(Of SalesmanProfile).FirstOrDefault(Function(x) x.ProfileHeader.ID = 53)
                    If pUserName.ProfileValue.IsNullorEmpty Then
                        txtUserName.Text = data.Username
                    Else
                        txtUserName.Text = pUserName.ProfileValue
                    End If
                    If Not txtUserName.IsEmpty Then
                        txtUserName.Disabled()
                    End If

                    lbtnDelete.Visible = False
                End If
            Else
                txtUserName.Text = data.Username
            End If

        ElseIf e.Item.ItemType = ListItemType.Footer Then
            Dim func As New SalesmanHeaderToDealerFacade(User)
            Dim sH As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(SalesmanHeaderID)
            Dim ddlDealer As DropDownList = CType(e.Item.FindControl("ddlDealer"), DropDownList)
            ddlDealer.ClearSelection()
            ddlDealer.Items.Clear()

            ddlDealer.Items.Add(New ListItem("Pilih Dealer", "-1"))

            Dim criterias As New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Dealer), "DealerGroup.ID", MatchType.Exact, sH.Dealer.DealerGroup.ID))

            Dim listDealer As List(Of Dealer) = New DealerFacade(User).Retrieve(criterias).Cast(Of Dealer).ToList()
            For Each iDealer As Dealer In GetDealerFromGrid()
                listDealer.RemoveAll(Function(x) x.ID = iDealer.ID)
            Next
           
            If sH.JobPosition.Code.ToUpper.Equals("CSO") Then
                Dim crits As New CriteriaComposite(New Criteria(GetType(SalesmanHeaderToDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crits.opAnd(New Criteria(GetType(SalesmanHeaderToDealer), "Dealer.DealerGroup.ID", MatchType.Exact, sH.Dealer.DealerGroup.ID))
                crits.opAnd(New Criteria(GetType(SalesmanHeaderToDealer), "SalesmanHeader.JobPosition.ID", MatchType.Exact, sH.JobPosition.ID))
                crits.opAnd(New Criteria(GetType(SalesmanHeaderToDealer), "SalesmanHeader.ID", MatchType.No, sH.ID))

                Dim arrSHtoDealer As ArrayList = func.Retrieve(crits)
                If arrSHtoDealer.IsItems Then
                    For Each iShToDealer As SalesmanHeaderToDealer In arrSHtoDealer
                        listDealer.RemoveAll(Function(x) x.ID = iShToDealer.Dealer.ID)
                    Next
                End If
            End If

            For Each nDealer As Dealer In listDealer
                ddlDealer.Items.Add(New ListItem(nDealer.DealerCode, nDealer.ID.ToString()))
            Next


            Dim lblDealerName As Label = e.FindLabel("lblDealerNameF")
            Dim lblKota As Label = e.FindLabel("lblKotaF")
            Dim lbtnAdd As LinkButton = e.FindLinkButton("lbtnAdd")
            lblDealerName.Text = String.Empty
            lblKota.Text = String.Empty
            lbtnAdd.Visible = False
            If ddlDealer.Items.Count.Equals(2) Then
                ddlDealer.SelectedIndex = 1
                lbtnAdd.Visible = True
                lblDealerName.Text = listDealer(0).DealerName
                lblKota.Text = listDealer(0).City.CityName
            Else
                ddlDealer.SelectedIndex = 0
            End If
        End If

    End Sub

    Private Function GetDealerFromGrid() As List(Of Dealer)
        Dim listDealer As New List(Of Dealer)
        Dim func As New DealerFacade(User)
        For Each item As DataGridItem In dtgDealer.Items
            Dim lblDealerCode As Label = item.FindLabel("lblDealerCode")
            listDealer.Add(func.Retrieve(lblDealerCode.Text))
        Next
        Return listDealer
    End Function

    Private Function GetDataFromGrid() As List(Of SalesmanHeaderToDealer)
        Dim listData As New List(Of SalesmanHeaderToDealer)
        Dim func As New DealerFacade(User)
        Dim sH As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(SalesmanHeaderID)
        For Each item As DataGridItem In dtgDealer.Items
            Dim rowValue As New SalesmanHeaderToDealer
            Dim hdnID As HiddenField = item.FindHiddenField("hdnID")
            Dim hdnIsMain As HiddenField = item.FindHiddenField("hdnIsMain")
            Dim txtUserName As TextBox = item.FindTextBox("txtUserName")
            Dim lblDealerCode As Label = item.FindLabel("lblDealerCode")

            rowValue.Dealer = func.Retrieve(lblDealerCode.Text)
            rowValue.IsMainDealer = CType(hdnIsMain.Value, Boolean)
            rowValue.SalesmanHeader = sH
            rowValue.Username = txtUserName.Text
            rowValue.Status = 1
            rowValue.ID = CInt(hdnID.Value)

            listData.Add(rowValue)
        Next
        Return listData

    End Function

    Protected Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect("FrmCcCSTeamList.aspx")
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim func As New SalesmanHeaderToDealerFacade(User)
        Dim sH As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(SalesmanHeaderID)
        Dim dataHist As List(Of SalesmanHeaderToDealer) = func.GetDatabySalesmanHeader(SalesmanHeaderID)
        Dim listData As List(Of SalesmanHeaderToDealer) = GetDataFromGrid()
        Try
            For Each iHist As SalesmanHeaderToDealer In dataHist
                If Not listData.Where(Function(x) x.Dealer.ID = iHist.Dealer.ID).IsData Then
                    iHist.RowStatus = CType(DBRowStatus.Deleted, Short)
                    func.Update(iHist)
                End If
            Next

            For Each iData As SalesmanHeaderToDealer In GetDataFromGrid()
                If dataHist.Where(Function(x) x.Dealer.ID = iData.Dealer.ID).IsData Then
                    Dim dtCurrent As SalesmanHeaderToDealer = dataHist.FirstOrDefault(Function(x) x.Dealer.ID = iData.Dealer.ID)
                    dtCurrent.Username = iData.Username
                    func.Update(dtCurrent)
                Else
                    If iData.Dealer.ID = sH.Dealer.ID Then
                        iData.IsMainDealer = True
                    End If
                    func.Insert(iData)
                End If
            Next
            MessageBox.Show(SR.SaveSuccess)
        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)
        End Try
        
    End Sub
End Class