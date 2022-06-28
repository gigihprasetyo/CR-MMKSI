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


Public Class FrmTrPaymentInput
    Inherits System.Web.UI.Page

    Private helpers As TrainingHelpers = New TrainingHelpers(Me.Page, "Training After Sales - Input Pembayaran Transfer")

    Private Sub TitleDescription(ByVal areaid As String)
        If areaid.Equals("1") Then
            lblPageTitle.Text = "Training Sales - Input Pembayaran Transfer"
            hdnCategory.Value = "sales"
        ElseIf areaid.Equals("2") Then
            lblPageTitle.Text = "Training After Sales - Input Pembayaran Transfer"
            hdnCategory.Value = "ass"
        ElseIf areaid.Equals("3") Then
            lblPageTitle.Text = "Training Customer Satisfaction - Input Pembayaran Transfer"
            hdnCategory.Value = "cs"
        Else
            lblPageTitle.Text = "Training - Input Pembayaran Transfer"
            hdnCategory.Value = "ass"
        End If
        hylInfo.Attributes.Add("onclick", "ShowInformation();")
    End Sub

    Private ReadOnly Property AreaId As String
        Get
            Return Request.QueryString("area")
        End Get
    End Property
    Private ReadOnly Property Mode As String
        Get
            Try
                If Request.QueryString("mode") Is Nothing Then
                    Return ""
                End If

                Return Request.QueryString("mode")
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property
    Private ReadOnly Property dataID As String
        Get
            Try
                Return Request.QueryString("dataid")
            Catch ex As Exception
                Return "0"
            End Try
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsDealer Then
            helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditInputPembayaran_Privilege)
            helpers.Privilage()
        End If

        If Not IsPostBack Then
            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            TitleDescription(AreaId)
            If String.IsNullorEmpty(Mode) Then
                Dim dealer As Dealer = CType(helpers.GetSession("Dealer"), Dealer)
                fillDataDealer(dealer)
                trUpload.NonVisible()
                trDownload.NonVisible()
                dtgEntryInvRevPayment.DataSource = New List(Of TrBillingHeader)()
                dtgEntryInvRevPayment.DataBind()
            Else

                btnBack.ActiveControl()

                Dim dataList As List(Of TrBillingHeader) = New List(Of TrBillingHeader)()
                Dim data As TrTraineePaymentHeader = New TrTraineePaymentHeaderFacade(User).Retrieve(CInt(dataID))
                fillDataDealer(data.Dealer)
                lblRegNumber.Text = data.RegNumber
                Dim criteria As New CriteriaComposite(New Criteria(GetType(TrTraineePaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteria.opAnd(New Criteria(GetType(TrTraineePaymentDetail), "TrTraineePaymentHeader.ID", MatchType.Exact, data.ID))

                Dim dataArr As ArrayList = New TrTraineePaymentDetailFacade(User).Retrieve(criteria)
                For Each itemData As TrTraineePaymentDetail In dataArr
                    dataList.Add(itemData.TrBillingHeader)
                Next
                hdnPaymentID.Value = data.ID
                lblTotalDN.Text = dataList.Count
                lblTotalAmount.Text = (Aggregate amount In dataList
                                      Into Sum(amount.Total)).AddThousandDelimiter()
                icTglTransfer.Value = data.ActualPaymentDate
                hdnFilePath.Value = data.EvidencePath

                If Mode.Equals("1") Then
                    dtgEntryInvRevPayment.ShowFooter = False
                    icTglTransfer.Enabled = False
                    btnSave.NonActiveControl()
                    trUpload.NonVisible()
                    If data.EvidencePath.IsNullorEmpty Then
                        lbtnDownload.NonVisible()
                    End If
                    lbnDelete.NonVisible()
                ElseIf Mode.Equals("2") Then
                    dtgEntryInvRevPayment.ShowFooter = False
                    icTglTransfer.Enabled = False
                    btnSave.NonActiveControl()
                    If data.EvidencePath.IsNullorEmpty Then
                        trDownload.NonVisible()
                    Else
                        trUpload.NonVisible()
                    End If
                ElseIf Mode.Equals("3") Then
                    icTglTransfer.Enabled = False
                    trDownload.NonVisible()
                    trUpload.NonVisible()
                End If

                dtgEntryInvRevPayment.DataSource = dataList
                dtgEntryInvRevPayment.DataBind()
            End If

        End If
    End Sub
    Sub fillDataDealer(ByVal oD As Dealer)
        ltrDealerCode.Text = String.Format("{0} / {1}", oD.CreditAccount.ToString(), oD.DealerName)
    End Sub

    Private Sub dtgEntryInvRevPayment_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgEntryInvRevPayment.ItemCommand
        Select Case e.CommandName.ToLower
            Case "retrievedata"
                Dim ddl As DropDownList = CType(e.Item.FindControl("ddlDN_Number"), DropDownList)
                Dim lblDealerCodeF As Label = CType(e.Item.FindControl("lblDealerCodeF"), Label)
                Dim lblDealerNameF As Label = CType(e.Item.FindControl("lblDealerNameF"), Label)
                Dim lblRegNumberF As Label = CType(e.Item.FindControl("lblRegNumberF"), Label)
                Dim lblDebitChargeNoF As Label = CType(e.Item.FindControl("lblDebitChargeNoF"), Label)
                Dim lblAmountF As Label = CType(e.Item.FindControl("lblAmountF"), Label)

                If ddl.IsSelected Then
                    Dim data As TrBillingHeader = New TrBillingHeaderFacade(User).Retrieve(CInt(ddl.SelectedValue))
                    lblDealerCodeF.Text = data.DealerCode
                    lblDealerNameF.Text = New DealerFacade(Me.User).Retrieve(data.DealerCode).DealerName
                    lblRegNumberF.Text = data.RequestID
                    lblAmountF.Text = data.Total.AddThousandDelimiter()
                Else
                    lblDealerCodeF.Text = String.Empty
                    lblDealerNameF.Text = String.Empty
                    lblRegNumberF.Text = String.Empty
                    lblAmountF.Text = String.Empty
                End If
            Case "tambah"
                Dim ddl As DropDownList = CType(e.Item.FindControl("ddlDN_Number"), DropDownList)
                If ddl.IsSelected Then
                    Dim data As TrBillingHeader = New TrBillingHeaderFacade(User).Retrieve(CInt(ddl.SelectedValue))
                    Dim listData As List(Of TrBillingHeader) = GetDataFromDtg()
                    listData.Add(data)

                    lblTotalDN.Text = listData.Count
                    lblTotalAmount.Text = (Aggregate amount In listData
                                          Into Sum(amount.Total)).AddThousandDelimiter()
                    dtgEntryInvRevPayment.DataSource = listData
                    dtgEntryInvRevPayment.DataBind()
                Else
                    MessageBox.Show("Silahka pilih Nomor Debit Note")
                End If
            Case "delete"
                Dim func As New TrBillingHeaderFacade(User)
                Dim funcPD As New TrTraineePaymentDetailFacade(User)
                Dim lblDN_Number As Label = CType(e.Item.FindControl("lblDN_Number"), Label)
                Dim listID As List(Of String) = GetIDFromDtg()
                listID.Remove(lblDN_Number.Text)
                Dim criterias As New CriteriaComposite(New Criteria(GetType(TrBillingHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(TrBillingHeader), "DebitNoteNumber", MatchType.InSet, listID.GenerateInSet()))
                Dim listData As List(Of TrBillingHeader) = New List(Of TrBillingHeader)

                If Not listID.Count.Equals(0) Then
                    listData = func.Retrieve(criterias).Cast(Of TrBillingHeader).ToList()
                End If

                'Dim dataExist As TrBillingHeader = func.Retrieve(lblDN_Number.Text)
                'Dim criteria As New CriteriaComposite(New Criteria(GetType(TrTraineePaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criteria.opAnd(New Criteria(GetType(TrTraineePaymentDetail), "TrBillingHeader.ID", MatchType.Exact, dataExist.ID))

                'Dim arrPayment As ArrayList = funcPD.Retrieve(criteria)
                'If arrPayment.IsItems Then
                '    helpers.SetSession("dataDelete", New ListItem(lblDN_Number.Text, dataExist.ID.ToString()))
                'End If

                lblTotalDN.Text = listData.Count
                lblTotalAmount.Text = (Aggregate amount In listData
                                      Into Sum(amount.Total)).AddThousandDelimiter()
                dtgEntryInvRevPayment.DataSource = listData
                dtgEntryInvRevPayment.DataBind()
        End Select
    End Sub
    Private Function GetDataFromDtg() As List(Of TrBillingHeader)
        Dim listRest As List(Of TrBillingHeader) = New List(Of TrBillingHeader)
        For Each itemdtg As DataGridItem In dtgEntryInvRevPayment.Items
            If itemdtg.ItemType <> ListItemType.Footer Then
                Dim lblDN_Number As Label = CType(itemdtg.FindControl("lblDN_Number"), Label)
                Dim data As TrBillingHeader = New TrBillingHeaderFacade(User).Retrieve(lblDN_Number.Text)
                listRest.Add(data)
            End If
        Next
        Return listRest
    End Function

    Private Function GetIDFromDtg() As List(Of String)
        Dim listRest As List(Of String) = New List(Of String)
        For Each itemdtg As DataGridItem In dtgEntryInvRevPayment.Items
            If itemdtg.ItemType <> ListItemType.Footer Then
                Dim lblDN_Number As Label = CType(itemdtg.FindControl("lblDN_Number"), Label)
                listRest.Add(lblDN_Number.Text)
            End If
        Next
        Return listRest
    End Function

    Private Sub dtgEntryInvRevPayment_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgEntryInvRevPayment.ItemDataBound
        Dim dealer As Dealer = CType(helpers.GetSession("Dealer"), Dealer)
        If e.Item.ItemType = ListItemType.Footer Then
            Dim ddl As DropDownList = CType(e.Item.FindControl("ddlDN_Number"), DropDownList)
            ddl.ClearSelection()
            ddl.Items.Clear()
            ddl.Items.Add(New ListItem("Silahkan Pilih", "-1"))

            Dim criterias As New CriteriaComposite(New Criteria(GetType(TrBillingHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrBillingHeader), "DealerCode", MatchType.Exact, dealer.DealerCode))
            criterias.opAnd(New Criteria(GetType(TrBillingHeader), "Status", MatchType.Exact, CType(EnumTagihanTraining.TagihanStatus.Pembayaran_Transfer, Short)))
            Dim dataReqID As List(Of String) = GetIDFromDtg()

            Dim funcPD As New TrTraineePaymentDetailFacade(User)
            Dim criteria As New CriteriaComposite(New Criteria(GetType(TrTraineePaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(TrTraineePaymentDetail), "TrTraineePaymentHeader.ID", MatchType.Exact, CInt(dataID)))


            If dataReqID.Count > 0 Then
                criterias.opAnd(New Criteria(GetType(TrBillingHeader), "DebitNoteNumber", MatchType.NotInSet, dataReqID.GenerateInSet(False)))
                criteria.opAnd(New Criteria(GetType(TrTraineePaymentDetail), "TrBillingHeader.DebitNoteNumber", MatchType.NotInSet, dataReqID.GenerateInSet(False)))
            End If

            Dim data As ArrayList = New TrBillingHeaderFacade(User).Retrieve(criterias)
            For Each itemdata As TrBillingHeader In data
                ddl.Items.Add(New ListItem(itemdata.DebitNoteNumber, itemdata.ID.ToString()))
            Next

            Dim arrPayment As ArrayList = funcPD.Retrieve(criteria)
            For Each itemdata As TrTraineePaymentDetail In arrPayment
                ddl.Items.Add(New ListItem(itemdata.TrBillingHeader.DebitNoteNumber, itemdata.TrBillingHeader.ID.ToString()))
            Next

            'If Not IsNothing(helpers.GetSession("dataDelete")) Then
            '    Dim lItem As ListItem = CType(helpers.GetSession("dataDelete"), ListItem)
            '    If Not ddl.Items.Contains(lItem) Then
            '        ddl.Items.Add(lItem)
            '    End If
            '    helpers.RemoveSession("dataDelete")
            'End If

            ddl.SelectedIndex = 0
        ElseIf e.Item.DataItem IsNot Nothing Then
            Dim lblDN_Number As Label = CType(e.Item.FindControl("lblDN_Number"), Label)
            Dim lblDealerCodeF As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            Dim lblDealerNameF As Label = CType(e.Item.FindControl("lblDealerName"), Label)
            Dim lblRegNumberF As Label = CType(e.Item.FindControl("lblRegNumber"), Label)
            Dim lblDebitChargeNoF As Label = CType(e.Item.FindControl("lblDebitChargeNo"), Label)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            Dim lblAmountF As Label = CType(e.Item.FindControl("lblAmount"), Label)
            Dim data As TrBillingHeader = CType(e.Item.DataItem, TrBillingHeader)

            lblDN_Number.Text = data.DebitNoteNumber
            lblDealerCodeF.Text = data.DealerCode
            lblDealerNameF.Text = New DealerFacade(Me.User).Retrieve(data.DealerCode).DealerName
            lblRegNumberF.Text = data.RequestID
            lblAmountF.Text = data.Total.AddThousandDelimiter()

        End If
        If Mode.Equals("1") Then
            e.Item.Cells(5).Visible = False
        Else
            If Not IsNothing(dataID) Then
                Dim data As TrTraineePaymentHeader = New TrTraineePaymentHeaderFacade(User).Retrieve(CInt(dataID))
                If data.Status <> CType(EnumTagihanTraining.PembayaranStatus.Baru, Short) Then
                    e.Item.Cells(5).Visible = False
                End If
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim funcPH As TrTraineePaymentHeaderFacade = New TrTraineePaymentHeaderFacade(User)
        Dim funcPD As TrTraineePaymentDetailFacade = New TrTraineePaymentDetailFacade(User)
        Dim funcBH As TrBillingHeaderFacade = New TrBillingHeaderFacade(User)

        If dtgEntryInvRevPayment.Items.Count.Equals(0) Then
            MessageBox.Show("Masukan data tagihan yang akan di transfer")
            Return
        End If

        Try
            Dim dataParent As TrTraineePaymentHeader = New TrTraineePaymentHeader()
            dataParent.Dealer = Me.GetDealer()
            dataParent.EvidencePath = hdnFilePath.Value
            dataParent.TotalAmount = CType(lblTotalAmount.Text.Replace(".", ""), Decimal)
            dataParent.PaymentType = CType(EnumTagihanTraining.PaymentType.Transfer, String)
            dataParent.ActualPaymentDate = icTglTransfer.Value
            dataParent.Status = CType(EnumTagihanTraining.PembayaranStatus.Baru, Short)
            Dim dataList As List(Of TrBillingHeader) = New List(Of TrBillingHeader)()

            If Mode.Equals("3") Then
                dataParent.RegNumber = lblRegNumber.Text
                dataParent.ID = CInt(hdnPaymentID.Value)
                funcPH.Update(dataParent)
                Dim listID As List(Of String) = GetIDFromDtg()
                Dim criteria As New CriteriaComposite(New Criteria(GetType(TrTraineePaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteria.opAnd(New Criteria(GetType(TrTraineePaymentDetail), "TrTraineePaymentHeader.ID", MatchType.Exact, dataParent.ID))

                Dim dataArr As ArrayList = New TrTraineePaymentDetailFacade(User).Retrieve(criteria)
                For Each itemData As TrTraineePaymentDetail In dataArr
                    dataList.Add(itemData.TrBillingHeader)
                    Dim itemExist As TrBillingHeader = itemData.TrBillingHeader
                    If listID.Where(Function(x) x = itemExist.DebitNoteNumber).Count = 0 Then
                        'hapus payment detail
                        itemData.RowStatus = CType(DBRowStatus.Deleted, Short)
                        funcPD.Update(itemData)

                        'status billing kembali ke Pembayaran Transfer
                        itemExist.Status = CType(EnumTagihanTraining.TagihanStatus.Pembayaran_Transfer, Short)
                        funcBH.Update(itemExist)
                    End If
                Next

            Else
                dataParent.RegNumber = GenerateRegNumber(CType(helpers.GetSession("Dealer"), Dealer))
                dataParent.ID = funcPH.Insert(dataParent)
            End If

            For Each item As DataGridItem In dtgEntryInvRevPayment.Items
                Dim lblDN_Number As Label = CType(item.FindControl("lblDN_Number"), Label)
                If dataList.Count.Equals(0) Or dataList.Where(Function(x) x.DebitNoteNumber = CInt(lblDN_Number.Text)).Count = 0 Then
                    Dim dataTagihan As TrBillingHeader = funcBH.Retrieve(lblDN_Number.Text)
                    Dim dataChild As TrTraineePaymentDetail = New TrTraineePaymentDetail()
                    dataChild.TrTraineePaymentHeader = dataParent
                    dataChild.TrBillingHeader = dataTagihan
                    funcPD.Insert(dataChild)
                    dataTagihan.Status = CType(EnumTagihanTraining.TagihanStatus.Proses_Transfer, Short)
                    funcBH.Update(dataTagihan)
                End If
            Next
            If String.IsNullOrEmpty(Mode) Then
                dtgEntryInvRevPayment.DataSource = New List(Of TrBillingHeader)()
                dtgEntryInvRevPayment.DataBind()
                lblTotalDN.Text = "0"
                lblTotalAmount.Text = "0"
            End If

            MessageBox.Show(SR.SaveSuccess)
        Catch ex As Exception
            MessageBox.Show("Data gagal disimpan")
        End Try

    End Sub

    Function GenerateRegNumber(ByVal oDealer As Dealer) As String
        Dim strRegNoRevision As String = vbEmpty
        Dim strSeqNumber As String = String.Empty
        Dim strSql As String = String.Empty

        strSql = "SELECT Top 1 TrTraineePaymentHeader.RegNumber "
        strSql += "FROM TrTraineePaymentHeader with (nolock) "
        strSql += "WHERE(TrTraineePaymentHeader.RowStatus = 0) "
        strSql += "AND LEFT(Year(getdate()),2) + SUBSTRING(RegNumber, 7, 2) = Year(getdate()) "
        strSql += "ORDER BY Right(TrTraineePaymentHeader.RegNumber, 4) DESC "

        Dim noUrutan As String = New TrTraineePaymentHeaderFacade(User).GetRegisterNumber(strSql)
        If noUrutan.Length > 0 Then
            strSeqNumber = Right("0000" + CType(CType(Right(noUrutan, 4), Integer) + 1, String), 4)
        Else
            strSeqNumber = "0001"
        End If
        strRegNoRevision = "6" + Mid(oDealer.DealerCode, 2, oDealer.DealerCode.Length).ToString + Right(DatePart(DateInterval.Year, DateTime.Now), 2).ToString + strSeqNumber

        Return strRegNoRevision
    End Function

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("FrmDaftarPembayaran.aspx?area=" + AreaId)
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim rest As String = helpers.UploadFile(photoSrc, KTB.DNet.Lib.WebConfig.GetValue("FakturPajak"), _
                                                     KTB.DNet.Lib.WebConfig.GetValue("MaximumFakturPajakSize"))
        Dim errArr() As String = rest.Split("|")
        If errArr(0).Equals("Error") Then
            MessageBox.Show(errArr(1))
            Return
        Else
            hdnFilePath.Value = errArr(1)
        End If
        trDownload.ActiveControl()
        trUpload.NonActiveControl()

        If Not hdnFilePath.Value.IsNullorEmpty And Not IsNothing(dataID) Then
            Try
                Dim func As New TrTraineePaymentHeaderFacade(Me.User)
                Dim datas As TrTraineePaymentHeader = func.Retrieve(CInt(dataID))
                datas.EvidencePath = hdnFilePath.Value
                func.Update(datas)
            Catch
            End Try
        End If

    End Sub

    Protected Sub lbnDelete_Click(sender As Object, e As EventArgs) Handles lbnDelete.Click
        hdnFilePath.Value = String.Empty
        If Not IsNothing(dataID) Then
            Try
                Dim func As New TrTraineePaymentHeaderFacade(Me.User)
                Dim datas As TrTraineePaymentHeader = func.Retrieve(CInt(dataID))
                datas.EvidencePath = hdnFilePath.Value
                func.Update(datas)
            Catch
            End Try
        End If
        trUpload.ActiveControl()
        trDownload.NonActiveControl()
    End Sub

    Protected Sub lbtnDownload_Click(sender As Object, e As EventArgs) Handles lbtnDownload.Click
        Try
            helpers.DownloadFile(hdnFilePath.Value)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class