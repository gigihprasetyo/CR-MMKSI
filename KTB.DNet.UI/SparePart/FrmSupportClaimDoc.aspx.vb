#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security

#End Region
Public Class FrmSupportClaimDoc
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindDDLStatus()
        End If
        BindGrid()
    End Sub

    Private Sub BindDDLStatus()
        With ddlStatus.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", -1))
            .Add(New ListItem("Aktif", 1))
            .Add(New ListItem("Tidak Aktif", 0))
        End With
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If ValidateData() Then
            Dim supportDoc As New SPSupportClaimDoc
            supportDoc.DocumentName = txtDocumentName.Text.Trim
            supportDoc.Description = txtDescription.Text.Trim
            supportDoc.Status = ddlStatus.SelectedValue
            Dim result As Integer = 0
            If hdnID.Value = "" Then
                result = New SPSupportClaimDocFacade(User).Insert(supportDoc)
            Else
                supportDoc.ID = hdnID.Value
                result = New SPSupportClaimDocFacade(User).Update(supportDoc)
            End If
            If result > 0 Then
                MessageBox.Show("Simpan berhasil")
                BindGrid()
                ClearAll()
            Else
                MessageBox.Show("Simpan gagal")
            End If
        End If
    End Sub

    Private Sub BindGrid()
        Dim crit As New CriteriaComposite(New Criteria(GetType(SPSupportClaimDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlDoc As ArrayList = New SPSupportClaimDocFacade(User).Retrieve(crit)

        dgDocClaim.DataSource = arlDoc
        dgDocClaim.DataBind()
    End Sub


    Private Function ValidateData() As Boolean
        If txtDocumentName.Text.Trim.Length = 0 Then
            MessageBox.Show("Nama Dokumen masih kosong")
            Return False
        End If

        If txtDescription.Text.Trim.Length = 0 Then
            MessageBox.Show("Deskripsi masih kosong")
            Return False
        End If

        If ddlStatus.SelectedIndex = 0 Then
            MessageBox.Show("Silahkan pilih status")
            Return False
        End If

        Return True
    End Function

    Private Sub ClearAll()
        hdnID.Value = ""
        txtDocumentName.Text = ""
        txtDescription.Text = ""
        ddlStatus.SelectedIndex = 0
        txtDocumentName.Enabled = True
        txtDescription.Enabled = True
        ddlStatus.Enabled = True
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearAll()
    End Sub

    Protected Sub dgDocClaim_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgDocClaim.ItemCommand
        'If e.CommandSource = "Page" OrElse e.CommandSource = "Sort" Then
        '    Exit Sub
        'End If
        Dim doc As SPSupportClaimDoc = New SPSupportClaimDocFacade(User).Retrieve(CType(e.Item.Cells(0).Text, Integer))
        Select Case e.CommandName
            Case "View"
                txtDocumentName.Text = doc.DocumentName
                txtDescription.Text = doc.Description
                ddlStatus.SelectedValue = doc.Status
                txtDocumentName.Enabled = False
                txtDescription.Enabled = False
                ddlStatus.Enabled = False
            Case "Edit"
                hdnID.Value = doc.ID
                txtDocumentName.Text = doc.DocumentName
                txtDescription.Text = doc.Description
                ddlStatus.SelectedValue = doc.Status
                txtDocumentName.Enabled = True
                txtDescription.Enabled = True
                ddlStatus.Enabled = True
            Case "Delete"
                Dim varSPSupportClaimDoc As New SPSupportClaimDocFacade(User)
                varSPSupportClaimDoc.Delete(doc)
                BindGrid()
        End Select
    End Sub

    Protected Sub dgDocClaim_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgDocClaim.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.Item Then
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim supportDoc As SPSupportClaimDoc = CType(e.Item.DataItem, SPSupportClaimDoc)
            If supportDoc.Status = 1 Then
                lblStatus.Text = "Aktif"
            Else
                lblStatus.Text = "Tidak Aktif"
            End If
            lblNo.Text = e.Item.ItemIndex + 1
        End If
    End Sub
End Class