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
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions
#End Region

Public Class FrmDaftarPembayaran
    Inherits System.Web.UI.Page

    Private helpers As TrainingHelpers = New TrainingHelpers(Me.Page, "Training After Sales - Daftar Pembayaran")

    Private Sub TitleDescription(ByVal areaid As String)
        If areaid.Equals("1") Then
            lblPageTitle.Text = "Training Sales - Daftar Pembayaran"
            hdnCategory.Value = "sales"
        ElseIf areaid.Equals("2") Then
            lblPageTitle.Text = "Training After Sales - Daftar Pembayaran"
            hdnCategory.Value = "ass"
        ElseIf areaid.Equals("3") Then
            lblPageTitle.Text = "Training Customer Satisfaction - Daftar Pembayaran"
            hdnCategory.Value = "cs"
        Else
            lblPageTitle.Text = "Training - Daftar Pembayaran"
            hdnCategory.Value = "ass"
        End If
        hylInfo.Attributes.Add("onclick", "ShowInformation();")
    End Sub

    Private ReadOnly Property AreaId As String
        Get
            Return Request.QueryString("area")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditDaftarPembayaran_Privilege)
        helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.view, SR.TrainingAssViewDaftarPembayaran_Privilege)
        helpers.Privilage()
        If Not IsPostBack Then
            TitleDescription(AreaId)
            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            BindDDLStatus()
            BindDDLBuktiPembayaran()
            BindDDLJenisPembyaran()

            If Me.IsDealer Then
                Dim dealer As Dealer = Me.GetDealer()
                txtKodeDealer.Text = dealer.DealerCode & " - " & dealer.DealerName
                txtKodeDealer.TextMode = TextBoxMode.MultiLine
                lblPopUpDealer.NonActiveControl()
                txtKodeDealer.Disabled()
                btnProses.NonActiveControl()
            Else
                btnDelete.NonActiveControl()
                btnValidasi.Text = "Batal Validasi"
            End If
            trAction.Visible = helpers.IsEdit
            BindingDataGrid()
        End If

    End Sub

    Private Sub BindDDLStatus()
        ddlStatus.ClearSelection()
        ddlStatus.Items.AddRange(GetType(EnumTagihanTraining.PembayaranStatus).GetItems())
        ddlStatus.Items.FindByValue("3").Enabled = False
        ddlStatus.SelectedIndex = 0
    End Sub

    Private Sub BindDDLBuktiPembayaran()
        ddlbuktiPembayaran.ClearSelection()
        ddlbuktiPembayaran.Items.AddRange(GetType(EnumTagihanTraining.BuktiPembayaran).GetItems())
        ddlbuktiPembayaran.SelectedIndex = 0
    End Sub

    Private Sub BindDDLJenisPembyaran()
        ddlTipePembayaran.ClearSelection()
        ddlTipePembayaran.Items.AddRange(GetType(EnumTagihanTraining.PaymentType).GetItems())
        ddlTipePembayaran.SelectedIndex = 0
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        BindingDataGrid()
    End Sub

    Private Sub BindingDataGrid(Optional ByVal pageNumber As Integer = 0)
        Dim dealer As Dealer = CType(helpers.GetSession("Dealer"), Dealer)
        Dim totalRow As Integer = 0
        Dim criteria As New CriteriaComposite(New Criteria(GetType(TrTraineePaymentHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txNoReg.IsNotEmpty Then
            criteria.opAnd("RegNumber", MatchType.Partial, txNoReg.Text)
        End If

        If dealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            criteria.opAnd(New Criteria(GetType(TrTraineePaymentHeader), "Dealer.CreditAccount", MatchType.Exact, dealer.CreditAccount))
        Else
            Dim dealerInSet As String = txtKodeDealer.Text.GenerateInSet()
            If Not String.IsNullOrEmpty(txtKodeDealer.Text) Then
                criteria.opAnd(New Criteria(GetType(TrTraineePaymentHeader), "Dealer.DealerCode", MatchType.InSet, String.Format("({0})", dealerInSet)))
            End If
        End If

        If ddlbuktiPembayaran.IsSelected Then
            If ddlbuktiPembayaran.SelectedValue = CType(EnumTagihanTraining.BuktiPembayaran.Sudah_Upload, String) Then
                criteria.opAnd(New Criteria(GetType(TrTraineePaymentHeader), "EvidencePath", MatchType.IsNotNull, Nothing))
            Else
                criteria.opAnd(New Criteria(GetType(TrTraineePaymentHeader), "EvidencePath", MatchType.IsNull, Nothing))
            End If
        End If

        If ddlTipePembayaran.IsSelected Then
            criteria.opAnd(New Criteria(GetType(TrTraineePaymentHeader), "PaymentType", MatchType.Exact, CInt(ddlTipePembayaran.SelectedValue)))
        End If

        If ddlStatus.IsSelected Then
            criteria.opAnd(New Criteria(GetType(TrTraineePaymentHeader), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If


        dtgPayment.DataSource = New TrTraineePaymentHeaderFacade(User).RetrieveActiveList(pageNumber, dtgPayment.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection), criteria)
        dtgPayment.DataBind()
    End Sub

    Private Sub dtgPayment_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgPayment.ItemCommand
        Select Case e.CommandName.ToLower()
            Case "detail"
                Dim hdnID As HiddenField = CType(e.Item.FindControl("hdnID"), HiddenField)
                Response.Redirect("FrmTrPaymentInput.aspx?area=" + AreaId + "&dataid=" + hdnID.Value + "&mode=1")
            Case "edit"
                Dim hdnID As HiddenField = CType(e.Item.FindControl("hdnID"), HiddenField)
                Dim dataPayment As TrTraineePaymentHeader = New TrTraineePaymentHeaderFacade(User).Retrieve(CInt(hdnID.Value))
                If dataPayment.Status = CType(EnumTagihanTraining.PembayaranStatus.Baru, Short) Then
                    Response.Redirect("FrmTrPaymentInput.aspx?area=" + AreaId + "&dataid=" + hdnID.Value + "&mode=3")

                Else
                    Response.Redirect("FrmTrPaymentInput.aspx?area=" + AreaId + "&dataid=" + hdnID.Value + "&mode=2")
                End If
            Case "downloadbukti"
                Dim hdnSourceFile As HiddenField = CType(e.Item.FindControl("hdnSourceFile"), HiddenField)
                Try
                    helpers.DownloadFile(hdnSourceFile.Value)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            Case "simpantr"
                Dim btnSimpantr As LinkButton = CType(e.Item.FindControl("btnSimpantr"), LinkButton)
                Dim btnEdittr As LinkButton = CType(e.Item.FindControl("btnEdittr"), LinkButton)
                Dim btnCanceltr As LinkButton = CType(e.Item.FindControl("btnCanceltr"), LinkButton)
                Dim txTrNumber As TextBox = CType(e.Item.FindControl("txTrNumber"), TextBox)
                Dim hdnID As HiddenField = CType(e.Item.FindControl("hdnID"), HiddenField)

                Try
                    Dim rowValue As TrTraineePaymentHeader = New TrTraineePaymentHeaderFacade(Me.User).Retrieve(CInt(hdnID.Value))
                    Dim func As New TrBillingHeaderFacade(Me.User)
                    For Each paymentDetail As TrTraineePaymentDetail In rowValue.ListTrTraineePaymentDetail
                        If Not IsNothing(paymentDetail.TrBillingHeader) Then
                            Dim billingHeader As TrBillingHeader = paymentDetail.TrBillingHeader
                            billingHeader.JVNumber = txTrNumber.Text
                            func.Update(billingHeader)
                        End If
                    Next

                    btnSimpantr.Visible = False
                    btnCanceltr.Visible = False
                    btnEdittr.Visible = True
                    txTrNumber.Disabled()
                    MessageBox.Show(SR.SaveSuccess)
                Catch ex As Exception
                    MessageBox.Show(SR.SaveFail)
                End Try
            Case "edittr"
                Dim btnSimpantr As LinkButton = CType(e.Item.FindControl("btnSimpantr"), LinkButton)
                Dim btnEdittr As LinkButton = CType(e.Item.FindControl("btnEdittr"), LinkButton)
                Dim btnCanceltr As LinkButton = CType(e.Item.FindControl("btnCanceltr"), LinkButton)
                Dim txTrNumber As TextBox = CType(e.Item.FindControl("txTrNumber"), TextBox)
                Try
                    btnSimpantr.Visible = True
                    btnCanceltr.Visible = True
                    btnEdittr.Visible = False
                    txTrNumber.Enable()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            Case "canceltr"
                Dim btnSimpantr As LinkButton = CType(e.Item.FindControl("btnSimpantr"), LinkButton)
                Dim btnEdittr As LinkButton = CType(e.Item.FindControl("btnEdittr"), LinkButton)
                Dim btnCanceltr As LinkButton = CType(e.Item.FindControl("btnCanceltr"), LinkButton)
                Dim txTrNumber As TextBox = CType(e.Item.FindControl("txTrNumber"), TextBox)
                Dim hdnID As HiddenField = CType(e.Item.FindControl("hdnID"), HiddenField)
                Try
                    Dim rowValue As TrTraineePaymentHeader = New TrTraineePaymentHeaderFacade(Me.User).Retrieve(CInt(hdnID.Value))
                    Dim paymentDetail As TrTraineePaymentDetail = rowValue.ListTrTraineePaymentDetail(0)
                    Try
                        If Not IsNothing(paymentDetail.TrBillingHeader) Then
                            txTrNumber.Text = paymentDetail.TrBillingHeader.JVNumber
                        End If
                    Catch
                    End Try

                    btnSimpantr.Visible = False
                    btnCanceltr.Visible = False
                    btnEdittr.Visible = True
                    txTrNumber.Disabled()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
        End Select
    End Sub

    Private Sub dtgPayment_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgPayment.ItemDataBound
        If Me.IsDealer Then
            e.HiddenColumnbyIndex(12)
        End If

        If e.Item.DataItem IsNot Nothing Then
            Dim data As TrTraineePaymentHeader = CType(e.Item.DataItem, TrTraineePaymentHeader)
            Dim chkItemChecked As CheckBox = e.FindCheckBox("chkItemChecked")
            Dim lblStatus As Label = e.FindLabel("lblStatus")
            Dim lblNo As Label = e.FindLabel("lblNo")
            Dim lblKodeDealer As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)
            Dim lblNoReg As Label = CType(e.Item.FindControl("lblNoReg"), Label)
            Dim lblJumlahTagihan As Label = CType(e.Item.FindControl("lblJumlahTagihan"), Label)
            Dim lbltipePembayaran As Label = CType(e.Item.FindControl("lbltipePembayaran"), Label)
            Dim lblDNNumber As Label = CType(e.Item.FindControl("lblDNNumber"), Label)
            Dim lbltglDibuat As Label = CType(e.Item.FindControl("lbltglDibuat"), Label)
            Dim lbltglActual As Label = CType(e.Item.FindControl("lbltglActual"), Label)
            Dim lblAmount As Label = CType(e.Item.FindControl("lblAmount"), Label)
            Dim lblActualAmount As Label = CType(e.Item.FindControl("lblActualAmount"), Label)
            Dim hdnID As HiddenField = CType(e.Item.FindControl("hdnID"), HiddenField)
            Dim hdnSourceFile As HiddenField = CType(e.Item.FindControl("hdnSourceFile"), HiddenField)
            Dim btnEdit As LinkButton = CType(e.Item.FindControl("btnEdit"), LinkButton)
            Dim btnDownloadBukti As LinkButton = CType(e.Item.FindControl("btnDownloadBukti"), LinkButton)
            Dim btnSimpantr As LinkButton = CType(e.Item.FindControl("btnSimpantr"), LinkButton)
            Dim btnEdittr As LinkButton = CType(e.Item.FindControl("btnEdittr"), LinkButton)
            Dim btnCanceltr As LinkButton = CType(e.Item.FindControl("btnCanceltr"), LinkButton)
            Dim txTrNumber As TextBox = CType(e.Item.FindControl("txTrNumber"), TextBox)

            lblNo.Text = e.CreateNumberPage()
            lblStatus.Text = [Enum].GetName(GetType(EnumTagihanTraining.PembayaranStatus), data.Status).Replace("_", " ")
            lbltipePembayaran.Text = [Enum].GetName(GetType(EnumTagihanTraining.PaymentType), CInt(data.PaymentType)).Replace("_", " ")
            lblKodeDealer.Text = data.Dealer.DealerCode
            lblNoReg.Text = data.RegNumber
            lbltglDibuat.Text = data.CreatedTime.DateToString
            lbltglActual.Text = data.ActualPaymentDate.DateToString
            lblAmount.Text = data.TotalAmount.AddThousandDelimiter()
            lblActualAmount.Text = data.ActualPaymentAmount.AddThousandDelimiter()
            hdnID.Value = data.ID

            If Not String.IsNullorEmpty(data.EvidencePath) Then
                hdnSourceFile.Value = data.EvidencePath
            Else
                btnDownloadBukti.Visible = False
            End If

            btnSimpantr.Visible = False
            btnEdittr.Visible = False
            btnCanceltr.Visible = False
            If Me.IsKTB Then
                btnEdittr.Visible = True
            End If
            txTrNumber.Disabled()

            Dim arrPayment As List(Of TrTraineePaymentDetail) = data.ListTrTraineePaymentDetail
            Try
                If Not IsNothing(arrPayment(0).TrBillingHeader) Then
                    txTrNumber.Text = arrPayment(0).TrBillingHeader.JVNumber
                End If
            Catch
            End Try

            Dim arrDNNumber As String = String.Empty
            For Each iPayment As TrTraineePaymentDetail In arrPayment
                If Not IsNothing(iPayment.TrBillingHeader) Then
                    arrDNNumber += iPayment.TrBillingHeader.DebitNoteNumber + ", "
                End If
            Next
            If arrDNNumber.Length >= 2 Then
                lblDNNumber.Text = arrDNNumber.Remove(arrDNNumber.Length - 2, 2)
            End If


            If data.Status = CType(EnumTagihanTraining.PembayaranStatus.Selesai, Short) Or Me.IsKTB Then
                btnEdit.Visible = False
            End If

            Dim criteria As New CriteriaComposite(New Criteria(GetType(TrTraineePaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(TrTraineePaymentDetail), "TrTraineePaymentHeader.ID", MatchType.Exact, data.ID))

            lblJumlahTagihan.Text = New TrTraineePaymentDetailFacade(User).Retrieve(criteria).Count
            If Not helpers.IsEdit Then
                chkItemChecked.Visible = False
                btnEdit.Visible = False
            End If


        End If

    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If isCheck() Then
            Try
                Dim listDataNew As List(Of TrBillingHeader) = New List(Of TrBillingHeader)
                Dim funcH As TrBillingHeaderFacade = New TrBillingHeaderFacade(User)
                Dim funcPH As TrTraineePaymentHeaderFacade = New TrTraineePaymentHeaderFacade(User)
                Dim funcPD As TrTraineePaymentDetailFacade = New TrTraineePaymentDetailFacade(User)

                'Dim listDataCheck As List(Of TrTraineePaymentHeader) = GetDataCheckBox()

                Dim dataDeleted As List(Of TrTraineePaymentHeader) = GetDataCheckBox().Where(Function(x) x.Status = CType(EnumTagihanTraining.PembayaranStatus.Baru, Short)).ToList()
                If dataDeleted.Count = 0 Then
                    MessageBox.Show("Hapus data Gagal. Tidak ada data pembayaran transfer dengan status baru")
                    Exit Sub
                End If

                Dim succesData As Integer = 0
                For Each data As TrTraineePaymentHeader In dataDeleted
                    Try
                        Dim criteria As New CriteriaComposite(New Criteria(GetType(TrTraineePaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteria.opAnd(New Criteria(GetType(TrTraineePaymentDetail), "TrTraineePaymentHeader.ID", MatchType.Exact, data.ID))

                        Dim dataArr As ArrayList = New TrTraineePaymentDetailFacade(User).Retrieve(criteria)
                        For Each itemData As TrTraineePaymentDetail In dataArr
                            Dim dataTagihan As TrBillingHeader = itemData.TrBillingHeader

                            'Delete detail payment
                            itemData.RowStatus = CType(DBRowStatus.Deleted, Short)
                            funcPD.Update(itemData)

                            'tagihan kembali ke Pembayaran Transfer
                            dataTagihan.Status = CType(EnumTagihanTraining.TagihanStatus.Pembayaran_Transfer, Short)
                            funcH.Update(dataTagihan)
                        Next
                        data.RowStatus = CType(DBRowStatus.Deleted, Short)
                        funcPH.Update(data)
                        succesData += 1
                    Catch
                        Continue For
                    End Try
                Next

                MessageBox.Show(String.Format("{0} data berhasil dihapus. {1} data gagal dihapus", succesData, (dataDeleted.Count - succesData).ToString()))
                BindingDataGrid(dtgPayment.CurrentPageIndex)
            Catch ex As Exception
                MessageBox.Show("hapus data gagal.")
            End Try
        End If
    End Sub

    Protected Sub btnValidasi_Click(sender As Object, e As EventArgs) Handles btnValidasi.Click
        If isCheck() Then
            Try
                Dim successMsg As String = String.Empty
                Dim listDataNew As List(Of TrBillingHeader) = New List(Of TrBillingHeader)
                Dim funcH As New TrBillingHeaderFacade(User)
                Dim funcPH As New TrTraineePaymentHeaderFacade(User)
                Dim funcPD As New TrTraineePaymentDetailFacade(User)

                'Dim listDataCheck As List(Of TrTraineePaymentHeader) = GetDataCheckBox()
                Dim dataValidate As New List(Of TrTraineePaymentHeader)
                Dim successData As Integer = 0
                If Me.IsDealer Then
                    dataValidate = GetDataCheckBox().Where(Function(x) x.Status = CType(EnumTagihanTraining.PembayaranStatus.Baru, Short)).ToList()
                    If dataValidate.Count = 0 Then
                        MessageBox.Show("Validasi data Gagal. Tidak ada data pembayaran transfer dengan status baru")
                        Exit Sub
                    End If

                    For Each data As TrTraineePaymentHeader In dataValidate
                        data.Status = CType(EnumTagihanTraining.PembayaranStatus.Validasi, Short)
                        funcPH.Update(data)
                        successData += 1
                    Next
                    successMsg = String.Format("{0} data berhasil divalidasi. {1} data gagal divalidasi", successData, (dataValidate.Count - successData).ToString)
                Else
                    dataValidate = GetDataCheckBox().Where(Function(x) x.Status = CType(EnumTagihanTraining.PembayaranStatus.Validasi, Short)).ToList()
                    If dataValidate.Count = 0 Then
                        MessageBox.Show("Batal Validasi Gagal. Tidak ada data pembayaran transfer dengan status validasi")
                        Exit Sub
                    End If

                    For Each data As TrTraineePaymentHeader In dataValidate
                        data.Status = CType(EnumTagihanTraining.PembayaranStatus.Baru, Short)
                        funcPH.Update(data)
                        successData += 1
                    Next
                    successMsg = String.Format("{0} data berhasil batal validasi. {1} data gagal batal validasi", successData, (dataValidate.Count - successData).ToString)
                End If

                MessageBox.Show(successMsg)
                BindingDataGrid(dtgPayment.CurrentPageIndex)
            Catch
                If Me.IsDealer Then
                    MessageBox.Show("Validasi data gagal.")
                Else
                    MessageBox.Show("Batal Validasi gagal.")
                End If
            End Try
        End If
    End Sub

    Protected Sub btnProses_Click(sender As Object, e As EventArgs) Handles btnProses.Click
        If isCheck() Then
            Try
                Dim listDataNew As List(Of TrBillingHeader) = New List(Of TrBillingHeader)
                Dim funcH As TrBillingHeaderFacade = New TrBillingHeaderFacade(User)
                Dim funcPH As TrTraineePaymentHeaderFacade = New TrTraineePaymentHeaderFacade(User)
                Dim funcPD As TrTraineePaymentDetailFacade = New TrTraineePaymentDetailFacade(User)

                Dim listDataCheck As List(Of TrTraineePaymentHeader) = GetDataCheckBox()
                For Each data As TrTraineePaymentHeader In listDataCheck.Where(Function(x) x.Status = CType(EnumTagihanTraining.PembayaranStatus.Validasi, Short))
                    Dim criteria As New CriteriaComposite(New Criteria(GetType(TrTraineePaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criteria.opAnd(New Criteria(GetType(TrTraineePaymentDetail), "TrTraineePaymentHeader.ID", MatchType.Exact, data.ID))

                    Dim dataArr As ArrayList = New TrTraineePaymentDetailFacade(User).Retrieve(criteria)
                    For Each itemData As TrTraineePaymentDetail In dataArr
                        Dim dataTagihan As TrBillingHeader = itemData.TrBillingHeader

                        'status tagihan Selesai
                        dataTagihan.Status = CType(EnumTagihanTraining.TagihanStatus.Selesai, Short)
                        funcH.Update(dataTagihan)
                    Next
                    data.Status = CType(EnumTagihanTraining.PembayaranStatus.Selesai, Short)
                    funcPH.Update(data)
                Next

                MessageBox.Show("Proses data berhasil.")
                BindingDataGrid(dtgPayment.CurrentPageIndex)
            Catch ex As Exception
                MessageBox.Show("Proses data gagal.")
            End Try
        End If
    End Sub

    Private Function isCheck() As Boolean
        For Each item As DataGridItem In dtgPayment.Items
            Dim chx As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If chx.Checked Then
                Return True
            End If
        Next
        MessageBox.Show("Silahkan Pilih daftar Transfer")
        Return False
    End Function

    Private Function GetDataCheckBox() As List(Of TrTraineePaymentHeader)
        Dim listResult As List(Of TrTraineePaymentHeader) = New List(Of TrTraineePaymentHeader)
        Dim func As TrTraineePaymentHeaderFacade = New TrTraineePaymentHeaderFacade(User)
        For Each item As DataGridItem In dtgPayment.Items
            Dim chx As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If chx.Checked Then
                Dim hdnID As HiddenField = CType(item.FindControl("hdnID"), HiddenField)
                listResult.Add(func.Retrieve(CInt(hdnID.Value)))
            End If
        Next

        Return listResult
    End Function

    Private Sub dtgPayment_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgPayment.PageIndexChanged
        BindingDataGrid(e.NewPageIndex)
    End Sub

    Private Sub dtgPayment_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgPayment.SortCommand
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

        dtgPayment.SelectedIndex = -1
        dtgPayment.CurrentPageIndex = 0
        BindingDataGrid(dtgPayment.CurrentPageIndex)
    End Sub
End Class