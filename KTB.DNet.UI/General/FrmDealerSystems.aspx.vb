Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports System.Collections.Generic

Public Class FrmDealerSystems
    Inherits System.Web.UI.Page

    Private _sesshelper As SessionHelper = New SessionHelper
    Private systemDict As New Dictionary(Of Integer, String)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"

        If Not IsPostBack Then
            BindDataGrid(0)
            DisableControlDetailView()
            Dim systemList As ArrayList = New StandardCodeFacade(User).RetrieveByCategory("DealerSystemID")

            ddlSystemID.DataSource = systemList
            ddlSystemID.DataTextField = "ValueDesc"
            ddlSystemID.DataValueField = "ValueId"
            ddlSystemID.DataBind()
            ddlSystemID.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item

            ddlDetailSystemId.DataSource = systemList
            ddlDetailSystemId.DataTextField = "ValueDesc"
            ddlDetailSystemId.DataValueField = "ValueId"
            ddlDetailSystemId.DataBind()
            ddlDetailSystemId.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item
        End If
    End Sub

    Private Sub ActivateUserPrivilege()
        Dim objDealer As Dealer = CType(_sesshelper.GetSession("DEALER"), Dealer)

        If Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=GENERAL - Group")
        End If
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim result = New StandardCodeFacade(User).RetrieveByCategory("DealerSystemID")

        For Each e As StandardCode In result
            systemDict.Add(e.ValueId, e.ValueDesc)
        Next

        Dim totalRow As Integer = 0

        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(DealerSystems), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '-- Dealer code
        If txtKodeDealer.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(DealerSystems), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        End If

        If ddlSystemID.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(DealerSystems), "SystemID", MatchType.Exact, ddlSystemID.SelectedValue))
        End If

        If (indexPage >= 0) Then
            dtgDealerSystems.DataSource = New DealerSystemsFacade(User).RetrieveActiveList(criterias, indexPage + 1, dtgDealerSystems.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgDealerSystems.VirtualItemCount = totalRow
            dtgDealerSystems.DataBind()
        End If
    End Sub

    Private Sub dtgDealerSystems_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDealerSystems.ItemDataBound
        Dim RowValue As DealerSystems = CType(e.Item.DataItem, DealerSystems)

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgDealerSystems.CurrentPageIndex * dtgDealerSystems.PageSize)

            CType(e.Item.FindControl("lblDealerCode"), Label).Text = RowValue.Dealer.DealerCode

            Dim systemDesc As String
            systemDict.TryGetValue(RowValue.SystemID, systemDesc)
            CType(e.Item.FindControl("lblSystem"), Label).Text = systemDesc
        End If
    End Sub

    Private Sub dtgDealerSystems_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgDealerSystems.ItemCommand

        If e.CommandName = "View" Then
            Dim dealerSystems As DealerSystems = New DealerSystemsFacade(User).Retrieve(CType(e.Item.Cells(0).Text, Integer))

            If Not IsNothing(dealerSystems) Then
                DisableControlDetailView()

                lblDealer.Text = dealerSystems.Dealer.DealerCode + " / " + dealerSystems.Dealer.DealerName
                ddlDetailSystemId.SelectedValue = dealerSystems.SystemID
                chkIsSPKMatchFaktur.Checked = dealerSystems.isSPKMatchFaktur
                chkIsSPKDNet.Checked = dealerSystems.isSPKDNET
                chkIsOnlyUploadPhotoTenagaPenjual.Checked = dealerSystems.isOnlyUploadPhotoTenagaPenjual
                icGoLiveDate.Value = dealerSystems.GoLiveDate

                btnBatal.Visible = True
            End If
        End If
        If e.CommandName = "Edit" Then
            Dim dealerSystems As DealerSystems = New DealerSystemsFacade(User).Retrieve(CType(e.Item.Cells(0).Text, Integer))

            If Not IsNothing(dealerSystems) Then
                EnableControlDetailView()

                hdnID.Value = dealerSystems.ID
                lblDealer.Text = dealerSystems.Dealer.DealerCode + "/" + dealerSystems.Dealer.DealerName
                ddlDetailSystemId.SelectedValue = dealerSystems.SystemID
                chkIsSPKMatchFaktur.Checked = dealerSystems.isSPKMatchFaktur
                chkIsSPKDNet.Checked = dealerSystems.isSPKDNET
                chkIsOnlyUploadPhotoTenagaPenjual.Checked = dealerSystems.isOnlyUploadPhotoTenagaPenjual
                icGoLiveDate.Value = dealerSystems.GoLiveDate

                btnBatal.Visible = True
                btnSimpan.Visible = True
            End If
        End If

    End Sub

    Private Sub dtgDealerSystems_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDealerSystems.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If

        dtgDealerSystems.SelectedIndex = -1
        dtgDealerSystems.CurrentPageIndex = 0
        BindDataGrid(dtgDealerSystems.CurrentPageIndex)
    End Sub

    Private Sub dtgDealerSystems_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDealerSystems.PageIndexChanged
        dtgDealerSystems.SelectedIndex = -1
        dtgDealerSystems.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgDealerSystems.CurrentPageIndex)
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindDataGrid(0)
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If txtKodeDealer.Text.Trim() <> "" Then
            If ddlSystemID.SelectedValue <> "" Then
                Dim listofdealerCode As String() = txtKodeDealer.Text.Trim().Split(New Char() {";"c})

                Dim succesDealerCode As String = String.Empty
                Dim failedDealerCode As String = String.Empty
                Dim existDealerCode As String = String.Empty
                Dim notfoundDealerCode As String = String.Empty
                Dim Msg As String = String.Empty

                For Each code As String In listofdealerCode
                    code = code.Trim()
                    Dim dealerSystemfacade As DealerSystemsFacade = New DealerSystemsFacade(User)
                    Dim data As DealerSystems = dealerSystemfacade.RetrieveByDealerCode(code)

                    If Not IsNothing(data) Then
                        existDealerCode += code + ","
                    Else
                        Dim dealerData As Dealer = New DealerFacade(User).Retrieve(code)

                        If dealerData.ID > 0 Then
                            Dim newData As DealerSystems = New DealerSystems()
                            newData.Dealer = dealerData
                            newData.SystemID = CType(ddlSystemID.SelectedValue, Integer)
                            newData.isSPKMatchFaktur = 0
                            newData.isOnlyUploadPhotoTenagaPenjual = 0
                            newData.isSPKDNET = 1

                            Dim result As Integer = dealerSystemfacade.Insert(newData)
                            If result > -1 Then
                                succesDealerCode += code + ","
                            Else
                                failedDealerCode += code + ","
                            End If
                        Else
                            notfoundDealerCode += code + ","
                        End If
                    End If
                Next

                If existDealerCode.Length > 0 Then
                    Msg += "- Dealer code " + existDealerCode.Remove(existDealerCode.Length - 1) + " sudah ada pada dealer system!\n"
                End If
                If notfoundDealerCode.Length > 0 Then
                    Msg += "- Dealer code " + notfoundDealerCode.Remove(notfoundDealerCode.Length - 1) + " tidak valid!\n"
                End If
                If succesDealerCode.Length > 0 Then
                    Msg += "- Dealer code " + succesDealerCode.Remove(succesDealerCode.Length - 1) + " berhasil di input.\n"
                End If
                If failedDealerCode.Length > 0 Then
                    Msg += "- Dealer code " + failedDealerCode.Remove(failedDealerCode.Length - 1) + " gagal di input.\n"
                End If
                If Msg.Length > 0 Then
                    MessageBox.Show(Msg)
                End If

                BindDataGrid(0)
            Else
                MessageBox.Show("Pilih System terlebih dahulu!")
            End If
        Else
            MessageBox.Show("Masukan dealer code!")
        End If
    End Sub

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        ClearControlDetailView()
        DisableControlDetailView()
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If Not String.IsNullOrEmpty(hdnID.Value) Then
            Dim dealerSystemfacade As DealerSystemsFacade = New DealerSystemsFacade(User)
            Dim dealerSystems As DealerSystems = dealerSystemfacade.Retrieve(CType(hdnID.Value, Integer))

            If Not IsNothing(dealerSystems) Then
                dealerSystems.SystemID = CType(ddlDetailSystemId.SelectedValue, Integer)
                dealerSystems.isSPKMatchFaktur = chkIsSPKMatchFaktur.Checked
                dealerSystems.isOnlyUploadPhotoTenagaPenjual = chkIsOnlyUploadPhotoTenagaPenjual.Checked
                dealerSystems.isSPKDNET = chkIsSPKDNet.Checked
                dealerSystems.GoLiveDate = icGoLiveDate.Value
                dealerSystems.LastUpdateBy = User.Identity.Name
                dealerSystems.LastUpdateTime = Now

                Dim result As Integer = dealerSystemfacade.Update(dealerSystems)
                If result > -1 Then
                    MessageBox.Show("Update data berhasil.")
                    BindDataGrid(0)
                Else
                    MessageBox.Show("Update data gagal.")
                End If
            End If
        End If
    End Sub

    Private Sub ClearControlDetailView()
        hdnID.Value = String.Empty
        lblDealer.Text = String.Empty
        ddlDetailSystemId.SelectedIndex = 0
        chkIsSPKMatchFaktur.Checked = False
        chkIsSPKDNet.Checked = False
        chkIsOnlyUploadPhotoTenagaPenjual.Checked = False
        icGoLiveDate.Value = Now

        btnBatal.Visible = False
        btnSimpan.Visible = False
    End Sub

    Private Sub DisableControlDetailView()
        ddlDetailSystemId.Enabled = False
        chkIsSPKMatchFaktur.Enabled = False
        chkIsSPKDNet.Enabled = False
        chkIsOnlyUploadPhotoTenagaPenjual.Enabled = False
        icGoLiveDate.Enabled = False

        btnSimpan.Visible = False
    End Sub

    Private Sub EnableControlDetailView()
        ddlDetailSystemId.Enabled = True
        chkIsSPKMatchFaktur.Enabled = True
        chkIsSPKDNet.Enabled = True
        chkIsOnlyUploadPhotoTenagaPenjual.Enabled = True
        icGoLiveDate.Enabled = True
    End Sub
End Class