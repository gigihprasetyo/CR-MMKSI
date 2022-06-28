
#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports System.IO
Imports GlobalExtensions
#End Region

Public Class FrmTrCertificateConfig
    Inherits System.Web.UI.Page

    Private helpers As New TrainingHelpers(Me.Page)

    Private Sub TitleDescription()
        lblTitle.Text = "Training After Sales - Konfigurasi Sertifikat"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        helpers.CheckPrivilege("priv10B")
        If Not Page.IsPostBack Then
            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            TitleDescription()
            helpers.BindDDLStatus(DdlStatus)
            SetActiveControl(helpers.IsEdit)
            ReadCritriaSearch()
            BindDatagrid()
        End If
    End Sub

    Private Sub SetActiveControl(ByVal isActive As Boolean)
        btnSimpan.Visible = isActive
        btnBatal.Visible = isActive
    End Sub

    Protected Sub lbnDelete_Click(sender As Object, e As EventArgs) Handles lbnDelete.Click
        hdnFilePath.Value = String.Empty
        trUpload.Visible = True
        trDownload.Visible = False
    End Sub

    Protected Sub lbtnDownload_Click(sender As Object, e As EventArgs) Handles lbtnDownload.Click
        Try
            helpers.DownloadFile(hdnFilePath.Value)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If hdnFilePath.Value.IsNullorEmpty Then
            MessageBox.Show("Silahkan upload TTD")
            Return
        End If
        Try
            Dim result As Integer = 0
            Dim objDomain As New TrCertificateConfig
            objDomain.NamaTTD = txtnamaTTD.Text
            objDomain.JabatanTTD = txtJabatanTTD.Text
            objDomain.Description = txtDeskripsi.Text
            objDomain.Status = CInt(DdlStatus.SelectedValue)
            objDomain.PathTTD = hdnFilePath.Value
            result = New TrCertificateConfigFacade(User).Insert(objDomain)
            If Not result.Equals(-1) Then
                MessageBox.Show(SR.SaveSuccess)
                btnBatal_Click(sender, e)
                BindDatagrid()
                Return
            End If
        Catch
        End Try
        MessageBox.Show(SR.SaveFail)
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Me.ClearTextBoxWithPrefix("txt")
        trDownload.Visible = False
        trUpload.Visible = True
        DdlStatus.ClearSelection()
        btnSimpan.Enabled = True
        btnCari.Enabled = True
        lbtnDownload.Visible = True
        lbnDelete.Visible = True
        dtgConfigCer.SelectedIndex = -1
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        BindDatagrid()
    End Sub

    Private Sub BindDatagrid(Optional ByVal idxPage As Integer = 0)
        Dim totalRow As Integer = 0
        dtgConfigCer.DataSource = New TrCertificateConfigFacade(User).RetrieveActiveList(idxPage + 1, _
                                dtgConfigCer.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
                                CType(ViewState("CurrentSortDirect"), Sort.SortDirection), Criterias())
        dtgConfigCer.VirtualItemCount = totalRow
        dtgConfigCer.DataBind()
    End Sub

    Private Function Criterias() As CriteriaComposite
        Dim criteria As New CriteriaComposite(New Criteria(GetType(TrCertificateConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtnamaTTD.IsNotEmpty Then
            criteria.opAnd(New Criteria(GetType(TrCertificateConfig), "NamaTTD", MatchType.[Partial], txtnamaTTD.Text))
        End If

        If txtJabatanTTD.IsNotEmpty Then
            criteria.opAnd(New Criteria(GetType(TrCertificateConfig), "JabatanTTD", MatchType.[Partial], txtJabatanTTD.Text))
        End If

        If txtDeskripsi.IsNotEmpty Then
            criteria.opAnd(New Criteria(GetType(TrCertificateConfig), "Description", MatchType.[Partial], txtDeskripsi.Text))
        End If

        If DdlStatus.IsSelected Then
            criteria.opAnd(New Criteria(GetType(TrCertificateConfig), "Status", MatchType.Exact, DdlStatus.SelectedValue))
        End If
        SaveCriteriaSearch()
        Return criteria
    End Function

    Private Sub SaveCriteriaSearch()
        helpers.AddCriteria("Nama", txtnamaTTD.Text)
        helpers.AddCriteria("Jabatan", txtJabatanTTD.Text)
        helpers.AddCriteria("Status", DdlStatus.SelectedValue)
        helpers.AddCriteria("Keterangan", txtDeskripsi.Text)
        helpers.SaveCriteria()
    End Sub

    Private Sub ReadCritriaSearch()
        If Not helpers.IsNullCriteria Then
            txtnamaTTD.Text = helpers.GetStringCriteria("Nama")
            txtJabatanTTD.Text = helpers.GetStringCriteria("Jabatan")
            DdlStatus.SelectedValue = helpers.GetStringCriteria("Status")
            txtDeskripsi.Text = helpers.GetStringCriteria("Keterangan")
            helpers.ClearCriteria()
        End If
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try
            Dim rest As String = helpers.UploadFile(photoSrc, KTB.DNet.Lib.WebConfig.GetValue("TrClassMateri"), _
                                                    KTB.DNet.Lib.WebConfig.GetValue("MaximumTrClassMateriSize"))
            Dim errArr() As String = rest.Split("|")
            If errArr(0).Equals("Error") Then
                MessageBox.Show(errArr(1))
                Return
            Else
                hdnFilePath.Value = errArr(1)
            End If
            trDownload.Visible = True
            trUpload.Visible = False
            
        Catch ex As Exception
            MessageBox.Show("Error saat mengupload foto")
        End Try
    End Sub

    Private Sub dtgConfigCer_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgConfigCer.ItemCommand
        Select Case e.CommandName.ToLower()
            Case "view"
                Dim func As New TrCertificateConfigFacade(User)
                Dim hdnID As HiddenField = CType(e.Item.FindControl("hdnID"), HiddenField)
                Dim dataConfig As TrCertificateConfig = func.Retrieve(CInt(hdnID.Value))
                txtnamaTTD.Text = dataConfig.NamaTTD
                txtJabatanTTD.Text = dataConfig.JabatanTTD
                txtDeskripsi.Text = dataConfig.Description
                DdlStatus.ClearSelection()
                DdlStatus.Items.FindByValue(dataConfig.Status.ToString()).Selected = True
                trUpload.Visible = False
                trDownload.Visible = True
                If dataConfig.PathTTD.IsNullorEmpty Then
                    lbtnDownload.Visible = False
                    lbnDelete.Visible = False
                Else
                    lbtnDownload.Visible = True
                    lbnDelete.Visible = True
                End If
                btnSimpan.Enabled = False
                btnCari.Enabled = False
                Me.DisabledTextBoxWithPrefix("txt")
                'dtgConfigCer.SelectedIndex = e.Item.ItemIndex
                e.SelectedRows()
            Case "aktif"
                Dim func As New TrCertificateConfigFacade(User)
                Dim hdnID As HiddenField = CType(e.Item.FindControl("hdnID"), HiddenField)
                Dim dataConfig As TrCertificateConfig = func.Retrieve(CInt(hdnID.Value))

                Dim criteria As New CriteriaComposite(New Criteria(GetType(TrCertificateConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteria.opAnd(New Criteria(GetType(TrCertificateConfig), "Status", MatchType.Exact, 1))

                Dim arrDataAktif As ArrayList = func.Retrieve(criteria)
                For Each iData As TrCertificateConfig In arrDataAktif
                    iData.Status = 0
                    func.Update(iData)
                Next
                dataConfig.Status = 1
                func.Update(dataConfig)
                BindDatagrid(dtgConfigCer.CurrentPageIndex)
            Case "inaktif"
                Dim func As New TrCertificateConfigFacade(User)
                Dim hdnID As HiddenField = CType(e.Item.FindControl("hdnID"), HiddenField)
                Dim dataConfig As TrCertificateConfig = func.Retrieve(CInt(hdnID.Value))
                dataConfig.Status = 0
                func.Update(dataConfig)
                BindDatagrid(dtgConfigCer.CurrentPageIndex)
            Case "unduh"
                Dim hdnTTDpath As HiddenField = CType(e.Item.FindControl("hdnTTDpath"), HiddenField)
                Try
                    helpers.DownloadFile(hdnTTDpath.Value)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
        End Select
    End Sub

    Private Sub dtgConfigCer_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgConfigCer.ItemDataBound
        If e.IsRowItems Then
            Dim lblNo As Label = e.FindLabel("lblNo")
            Dim lblNama As Label = e.FindLabel("lblNama")
            Dim lbljabatan As Label = e.FindLabel("lbljabatan")
            Dim lblDescription As Label = e.FindLabel("lblDescription")
            Dim lblStatus As Label = e.FindLabel("lblStatus")
            Dim hdnTTDpath As HiddenField = e.FindHiddenField("hdnTTDpath")
            Dim hdnID As HiddenField = e.FindHiddenField("hdnID")
            Dim lbtnUnduh As LinkButton = e.FindLinkButton("lbtnUnduh")
            Dim lbtnAktif As LinkButton = e.FindLinkButton("lbtnAktif")
            Dim lbtnInAktif As LinkButton = e.FindLinkButton("lbtnInAktif")

            Dim data As TrCertificateConfig = e.DataItem(Of TrCertificateConfig)()
            hdnID.Value = data.ID.ToString
            lblNo.Text = e.CreateNumberPage()
            lblNama.Text = data.NamaTTD
            lbljabatan.Text = data.JabatanTTD
            lblDescription.Text = data.Description
            If Not String.IsNullorEmpty(data.PathTTD) Then
                hdnTTDpath.Value = data.PathTTD
            Else
                lbtnUnduh.Visible = False
            End If

            lblStatus.Text = DdlStatus.GetTextByValue(data.Status.ToString())
            lbtnAktif.Visible = helpers.IsEdit
            lbtnInAktif.Visible = helpers.IsEdit
            If helpers.IsEdit Then
                If data.Status = 1 Then
                    lbtnAktif.Visible = False
                    lbtnInAktif.Visible = True
                Else
                    lbtnAktif.Visible = True
                    lbtnInAktif.Visible = False
                End If
            End If
        End If
    End Sub

    Private Sub dtgConfigCer_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgConfigCer.PageIndexChanged
        ReadCritriaSearch()
        BindDatagrid(e.NewPageIndex)
    End Sub

    Private Sub dtgConfigCer_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgConfigCer.SortCommand
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
        dtgConfigCer.ClearSelectedRows()
        BindDatagrid()
    End Sub
End Class