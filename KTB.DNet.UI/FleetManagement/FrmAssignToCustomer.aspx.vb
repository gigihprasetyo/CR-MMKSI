Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.Collections.Generic

Public Class FrmAssignToCustomer
    Inherits System.Web.UI.Page
    Private crt As CriteriaComposite
    Private objCustomer As Customer
    Private sesHelper As New SessionHelper
    Private facFleetCustomer As New FleetCustomerFacade(User)
    Private facCustomer As New CustomerFacade(User)
    Private facFleetCustomerToCustomer As New FleetCustomerToCustomerFacade(User)
    ' session
    Private ReadOnly varSessFleetCustomerID As String = "FleetCustomerID"
    Private ReadOnly varSessFCCustomer As String = "sessFleettoCustomer"
    Private ReadOnly varSessCustomer As String = "sessCustomer"
    Private _view As Boolean = False
    Private _input As Boolean = False
    Private _edit As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        CheckPrivilege()
        If Not IsPostBack() Then
            FillForm()
        End If
    End Sub

    Private Sub CheckPrivilege()
        Dim _view As Boolean = SecurityProvider.Authorize(Context.User, SR.FleetCustomer_List_Privilege)
        If Not _view Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FLEET MANAGEMENT - Assign to Customer")
        End If

        _input = SecurityProvider.Authorize(Context.User, SR.FleetCustomer_Input_Privilege)
        _edit = SecurityProvider.Authorize(Context.User, SR.FleetCustomer_Edit_Privilege)
        btnSave.Visible = _input
    End Sub

    Private Sub FillForm()
        Dim idFleetCustomer = sesHelper.GetSession(varSessFleetCustomerID)
        Dim sessCommandDelete As String = sesHelper.GetSession("CommandDelete")
        hdnFleetCustomerID.Value = idFleetCustomer

        crt = New CriteriaComposite(New Criteria(GetType(FleetCustomerToCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crt.opAnd(New Criteria(GetType(FleetCustomerToCustomer), "FleetCustomerID", MatchType.Exact, idFleetCustomer))

        If (sessCommandDelete <> "1") Then
            sesHelper.SetSession(varSessFCCustomer, facFleetCustomerToCustomer.Retrieve(crt))
        End If
        Dim obFc As New FleetCustomer
        obFc = New FleetCustomerFacade(User).Retrieve(CInt(idFleetCustomer))

        lblFleetCustomerCode.Text = obFc.Code
        lblFleetCustomerName.Text = obFc.Name

        BindGridCustomer()
    End Sub

    Private Sub BindGridCustomer()
        Dim dtFCCustomer As ArrayList = sesHelper.GetSession(varSessFCCustomer)

        dtgCustomerSelection.DataSource = dtFCCustomer
        dtgCustomerSelection.DataBind()

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim newFCCustomer As ArrayList = sesHelper.GetSession(varSessFCCustomer)
        Dim oldFCCustomer As ArrayList
        Dim fleetCustomerID As Integer = sesHelper.GetSession(varSessFleetCustomerID)

        crt = New CriteriaComposite(New Criteria(GetType(FleetCustomerToCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crt.opAnd(New Criteria(GetType(FleetCustomerToCustomer), "FleetCustomerID", MatchType.Exact, fleetCustomerID))
        oldFCCustomer = facFleetCustomerToCustomer.Retrieve(crt)

        If newFCCustomer.Count > 0 Then
            For Each itemNew As FleetCustomerToCustomer In newFCCustomer
                If itemNew.ID = 0 Then
                    If facFleetCustomerToCustomer.Insert(itemNew) = -1 Then
                        MessageBox.Show("Gagal input data konsumen. !")
                        Return
                    End If
                Else
                    If facFleetCustomerToCustomer.Update(itemNew) = -1 Then
                        MessageBox.Show("Gagal update data konsumen. !")
                        Return
                    End If
                    ' update jika id tidak sama dengan 0
                    Dim index As Integer = -1
                    If oldFCCustomer.Count > 0 Then
                        For i As Integer = 0 To oldFCCustomer.Count - 1
                            Dim itemOld As FleetCustomerToCustomer = oldFCCustomer(i)
                            If itemOld.ID = itemNew.ID Then
                                index = i
                            End If
                        Next
                    End If

                    ' remove at index di list data lama
                    If index > -1 Then
                        oldFCCustomer.RemoveAt(index)
                    End If
                End If
            Next
        End If

        ' hapus data lama
        If oldFCCustomer.Count > 0 Then
            For Each itemOld As FleetCustomerToCustomer In oldFCCustomer
                itemOld.RowStatus = DBRowStatus.Deleted
                If facFleetCustomerToCustomer.Update(itemOld) = -1 Then
                    MessageBox.Show("Gagal delete data konsumen. !")
                    Return
                End If
            Next

        End If

        Try
            facFleetCustomerToCustomer.UpdateCutomerToDealer(fleetCustomerID)
        Catch ex As Exception

        End Try
        MessageBox.Show("Simpan Berhasil")
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        RemoveALLSession()
        Response.Redirect("FrmFleetCustomerList.aspx")
    End Sub

    Private Sub RemoveAllSession()
        sesHelper.RemoveSession(varSessFCCustomer)
        sesHelper.RemoveSession(varSessFleetCustomerID)
        sesHelper.RemoveSession("CommandDelete")
    End Sub

    Protected Sub btnCustomerHelper_Click(sender As Object, e As EventArgs) Handles btnCustomerHelper.Click
        'get customer code from textbox hidden (xxx;xxx;xxx;)
        Dim customerCodeList As String = txtCustomerCodeList.Value
        If Not String.IsNullOrEmpty(customerCodeList) Then
            Dim arl As ArrayList = New ArrayList()

            ' split customer code text
            Dim splitCustomerCode() As String = customerCodeList.Split(";")
            Dim strCustomer As String = String.Empty
            For Each Str As String In splitCustomerCode
                If String.IsNullOrEmpty(strCustomer) Then
                    strCustomer = "('" & Str & "'"
                Else
                    strCustomer += "," & "'" & Str & "'"
                End If
            Next
            strCustomer += ")"

            Dim criterias As New CriteriaComposite(New Criteria(GetType(Customer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Customer), "Code", MatchType.InSet, strCustomer))
            Dim obj As ArrayList = New CustomerFacade(User).Retrieve(criterias)

            ' get session array list selected fleet customer to customer
            arl = sesHelper.GetSession(varSessFCCustomer)
            ' add new selected dealer to session array
            If Not arl Is Nothing Then
                For Each item As Customer In obj
                    Dim objFleetCustomerToCustomer As New FleetCustomerToCustomer()
                    objFleetCustomerToCustomer.ID = 0
                    objFleetCustomerToCustomer.CustomerID = item
                    objFleetCustomerToCustomer.FleetCustomerID = sesHelper.GetSession(varSessFleetCustomerID)
                    arl.Add(objFleetCustomerToCustomer)
                Next
            End If
            dtgCustomerSelection.DataSource = arl
            dtgCustomerSelection.DataBind()
            sesHelper.SetSession(varSessFCCustomer, arl)
        End If
    End Sub

    Protected Sub dtgCustomerSelection_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgCustomerSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            lblNo.Text = (e.Item.ItemIndex + 1).ToString()

            e.Item.DataItem.GetType().ToString()
            Dim RowValue As FleetCustomerToCustomer = CType(e.Item.DataItem, FleetCustomerToCustomer)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblCode As Label = CType(e.Item.FindControl("lblCustomerCode"), Label)
                Dim lblName As Label = CType(e.Item.FindControl("lblCustomerName"), Label)
                Dim lblAddress As Label = CType(e.Item.FindControl("lblCustomerAddress"), Label)
                Dim lblKelurahan As Label = CType(e.Item.FindControl("lblCustomerKelurahan"), Label)
                Dim lblKecamatan As Label = CType(e.Item.FindControl("lblCustomerKecamatan"), Label)
                Dim lblCity As Label = CType(e.Item.FindControl("lblCustomerCity"), Label)

                If Not IsNothing(RowValue.CustomerID) Then
                    lblCode.Text = RowValue.CustomerID.Code
                    lblName.Text = RowValue.CustomerID.Name1
                    lblAddress.Text = RowValue.CustomerID.Alamat
                    lblKelurahan.Text = RowValue.CustomerID.Kelurahan
                    lblKecamatan.Text = RowValue.CustomerID.Kecamatan
                    If Not IsNothing(RowValue.CustomerID.City) Then
                        lblCity.Text = RowValue.CustomerID.City.CityName
                    End If

                    Dim lb As LinkButton = CType(e.Item.FindControl("lbtnIsDefault"), LinkButton)
                    If RowValue.IsDefault = False Then
                        lb.Attributes.Add("OnClick", "return confirm('Apakah anda yakin untuk set as default');")
                        lb.CommandName = "IsDefault"
                        Dim img As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("imgCancelDefault"), System.Web.UI.WebControls.Image)
                        img.Visible = True
                    Else
                        lb.Attributes.Add("OnClick", "return confirm('Apakah anda yakin untuk cancel default');")
                        lb.CommandName = "CancelDefault"
                        Dim img As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("imgIsDefault"), System.Web.UI.WebControls.Image)
                        img.Visible = True
                    End If
                End If
            End If

            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
        End If
    End Sub

    Protected Sub dtgCustomerSelection_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgCustomerSelection.ItemCommand
        Dim sessFCCustomer As ArrayList = sesHelper.GetSession(varSessFCCustomer)
        Dim newSessFCCustomer As ArrayList = New ArrayList()
        Dim objFCCustomer As FleetCustomerToCustomer = New FleetCustomerToCustomer
        Dim newObjFCCustomer As FleetCustomerToCustomer = New FleetCustomerToCustomer
        Dim facFCCustomer As FleetCustomerToCustomerFacade = New FleetCustomerToCustomerFacade(User)
        Select Case e.CommandName.ToLower()
            Case "delete"
                sessFCCustomer.RemoveAt(e.Item.ItemIndex)
                dtgCustomerSelection.DataSource = sessFCCustomer

                sesHelper.SetSession("CommandDelete", "1")
                sesHelper.SetSession(varSessFCCustomer, sessFCCustomer)
                Response.Redirect("FrmAssignToCustomer.aspx")
            Case "isdefault"

                For i As Integer = 0 To sessFCCustomer.Count - 1
                    objFCCustomer = sessFCCustomer(i)
                    If i = e.Item.ItemIndex Then
                        objFCCustomer.IsDefault = True
                    Else
                        objFCCustomer.IsDefault = False
                    End If

                    newSessFCCustomer.Add(objFCCustomer)
                Next

                sesHelper.SetSession("CommandDelete", "1")
                sesHelper.SetSession(varSessFCCustomer, newSessFCCustomer)
                Response.Redirect("FrmAssignToCustomer.aspx")
            Case "canceldefault"
                For i As Integer = 0 To sessFCCustomer.Count - 1
                    objFCCustomer = sessFCCustomer(i)
                    If i = e.Item.ItemIndex Then
                        objFCCustomer.IsDefault = False
                    End If

                    newSessFCCustomer.Add(objFCCustomer)
                Next

                sesHelper.SetSession("CommandDelete", "1")
                sesHelper.SetSession(varSessFCCustomer, newSessFCCustomer)
                Response.Redirect("FrmAssignToCustomer.aspx")
        End Select
    End Sub
End Class