#Region "Import Library"
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Imports System.Text
Imports System.IO
Imports System.Linq
#End Region

Public Class FrmEDocument
    Inherits System.Web.UI.Page

    Private _sessHelper As New SessionHelper
    Private _searchSess As String = "FrmEDocument.Criteria"
    Private _dataSess As String = "FrmEDocument.Data"
    Private objDealer As Dealer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objDealer = CType(_sessHelper.GetSession("DEALER"), Dealer)
        INitPriv()
        If Not IsPostBack Then
            BindDropDown()
            ViewState("CurrentSortColumn") = "[NomorPesanan]"
            ViewState("CurrentSortDirect") = KTB.DNet.Domain.Search.Sort.SortDirection.DESC
            lblPopUpDealer.Attributes("OnClick") = "showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);"


            dgEDoc.DataSource = New ArrayList
            dgEDoc.DataBind()
        End If

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            lblDealerCode.Visible = False
        Else
            txtKodeDealer.Visible = False
            lblPopUpDealer.Visible = False
            lblDealerCode.Text = objDealer.DealerCode
            lblDealerName.Text = objDealer.DealerCode & " / " & objDealer.DealerName
        End If
    End Sub

    Private Sub InitPriv()
        If Not SecurityProvider.Authorize(Context.User, SR.Lihat_Edocument_privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Download E-Document")
        End If
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        BindGrid(0)
    End Sub

    Private Sub BindDropDown()
        ddlDocumentType.DataSource = EnumSparepartEdoc.RetrieveAllDocumentType()
        ddlDocumentType.DataValueField = "ID"
        ddlDocumentType.DataTextField = "Value"
        ddlDocumentType.DataBind()
        ddlDocumentType.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
    End Sub

    Private Sub BindGrid_OLD(ByVal index As Integer)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartBillingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        SearchCriterisa_OLD(criterias)
        Dim totalRow As Integer = 0
        Dim arlData As ArrayList = New SparePartBillingDetailFacade(User).RetrieveActiveList(index + 1, dgEDoc.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection), criterias)

        If arlData.Count = 0 Then
            MessageBox.Show("Data tidak ditemukan")
            dgEDoc.DataSource = New ArrayList
            dgEDoc.DataBind()
            Exit Sub
        End If
        _sessHelper.SetSession(_dataSess, arlData)
        _sessHelper.SetSession(_searchSess, criterias)

        dgEDoc.CurrentPageIndex = index
        dgEDoc.DataSource = arlData
        dgEDoc.VirtualItemCount = totalRow
        dgEDoc.DataBind()
    End Sub

    Private Function SearchCriterisa_OLD(ByRef crit As CriteriaComposite) As CriteriaComposite
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If txtKodeDealer.Text.Trim.Length > 0 Then
                crit.opAnd(New Criteria(GetType(SparePartBillingDetail), "SparePartBilling.Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text.Trim))
            End If
        Else
            crit.opAnd(New Criteria(GetType(SparePartBillingDetail), "SparePartBilling.Dealer.DealerCode", MatchType.Exact, lblDealerCode.Text.Trim))
        End If

        If txtDONumber.Text.Trim.Length > 0 Then
            crit.opAnd(New Criteria(GetType(SparePartBillingDetail), "SparePartDODetail.SParepartDO.DONumber", MatchType.Exact, txtDONumber.Text.Trim))
        End If

        If txtNomorFaktur.Text.Trim.Length > 0 Then
            crit.opAnd(New Criteria(GetType(SparePartBillingDetail), "SparePartBilling.BillingNumber", MatchType.Exact, txtNomorFaktur.Text.Trim))
        End If

        If txtNoPesanan.Text.Trim.Length > 0 Then
            crit.opAnd(New Criteria(GetType(SparePartBillingDetail), "SparePartDODetail.SparePartPOEstimate.SparePartPO.PONumber", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        End If

        If ddlDocumentType.SelectedIndex <> 0 Then
            Dim critEDoc As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartEDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critEDoc.opAnd(New Criteria(GetType(SparePartEDocument), "DocType", MatchType.Exact, ddlDocumentType.SelectedValue))
            Dim arlEDoc As ArrayList = New SparePartEDocumentFacade(User).Retrieve(critEDoc)
            Dim DocNumber As String = String.Empty
            If arlEDoc.Count > 0 Then
                For Each item As SparePartEDocument In arlEDoc
                    If DocNumber.Length = 0 Then
                        DocNumber = item.DocNumber
                    Else
                        DocNumber = DocNumber & "," & item.DocNumber
                    End If
                Next
            End If

            Select Case ddlDocumentType.SelectedValue
                Case "0", "1", "8"
                    crit.opAnd(New Criteria(GetType(SparePartBillingDetail), "SparePartBilling.BillingNumber", MatchType.InSet, "('" & DocNumber & "')"))
                Case "2"
                    Dim critDepositC2Line As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositC2Line), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    critDepositC2Line.opAnd(New Criteria(GetType(DepositC2Line), "DocumentNo", MatchType.InSet, "('" & DocNumber & "')"))
                    Dim arlDepositC2Line As ArrayList = New DepositC2LineFacade(User).Retrieve(critDepositC2Line)
                    Dim DepositC2LineDocNumber As String = String.Empty
                    If arlDepositC2Line.Count > 0 Then
                        For Each item As DepositC2Line In arlDepositC2Line
                            If Not DepositC2LineDocNumber.Contains(item.DocumentNo) Then
                                If DepositC2LineDocNumber.Length = 0 Then
                                    DepositC2LineDocNumber = item.DocumentNo
                                Else
                                    DepositC2LineDocNumber = DepositC2LineDocNumber & "," & item.DocumentNo
                                End If
                            End If
                        Next
                    End If
                    crit.opAnd(New Criteria(GetType(SparePartBillingDetail), "SparePartBilling.BillingNumber", MatchType.InSet, "('" & DepositC2LineDocNumber & "')"))
                Case "3"
                    Dim critSPPenalty As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPPenaltyDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    critSPPenalty.opAnd(New Criteria(GetType(TOPSPPenaltyDetail), "TOPSPPenalty.DebitMemoNumber", MatchType.InSet, "('" & DocNumber & "')"))
                    Dim arlSPPenalty As ArrayList = New TOPSPPenaltyDetailFacade(User).Retrieve(critSPPenalty)
                    Dim SPPenaltyDocNumber As String = String.Empty
                    If arlSPPenalty.Count > 0 Then
                        For Each item As TOPSPPenaltyDetail In arlSPPenalty
                            If Not SPPenaltyDocNumber.Contains(item.TOPSPPenalty.DebitMemoNumber) Then
                                If SPPenaltyDocNumber.Length = 0 Then
                                    SPPenaltyDocNumber = item.TOPSPPenalty.DebitMemoNumber
                                Else
                                    SPPenaltyDocNumber = SPPenaltyDocNumber & "," & item.TOPSPPenalty.DebitMemoNumber
                                End If
                            End If
                        Next
                    End If
                    crit.opAnd(New Criteria(GetType(SparePartBillingDetail), "SparePartBilling.BillingNumber", MatchType.InSet, "('" & SPPenaltyDocNumber & "')"))
                Case "4", "5", "6", "7"
                    crit.opAnd(New Criteria(GetType(SparePartBillingDetail), "SparePartDODetail.SparePartDO.DONumber", MatchType.InSet, "('" & DocNumber & "')"))
            End Select

        End If


    End Function

    Private Sub BindGrid(ByVal index As Integer)
        Dim query As StringBuilder = New StringBuilder

        If Not SearchCriteria(query) Then
            Exit Sub
        End If
        'page
        query.Append(" '" & index & "',")
        'size
        query.Append(" '" & dgEDoc.PageSize & "',")
        'sort
        query.Append(" '" & ViewState("CurrentSortColumn") & "',")
        'direction
        query.Append(" '" & ViewState("CurrentSortDirect") & "'")

        Dim dsEDoc As DataSet = New SparePartEDocumentFacade(User).DoRetrieveDataset(query.ToString)
        For Each drJumlah As DataRow In dsEDoc.Tables(1).Rows
            If CType(drJumlah("Jumlah"), Integer) <> 0 Then
                dgEDoc.CurrentPageIndex = index
                dgEDoc.DataSource = dsEDoc.Tables(0).Rows
                dgEDoc.VirtualItemCount = CType(drJumlah("Jumlah"), Integer)
                dgEDoc.DataBind()
            Else
                dgEDoc.DataSource = New ArrayList
                dgEDoc.DataBind()
                MessageBox.Show("Data Tidak Ditemukan")
            End If
        Next

    End Sub

    Private Function SearchCriteria(ByRef query As StringBuilder) As Boolean
        'sp_RetrieveEDocumentData

        query.Append("EXEC sp_RetrieveEDocumentData ")
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If txtKodeDealer.Text.Trim.Length > 0 Then
                query.Append(" '" & txtKodeDealer.Text.Trim & "',")
            Else
                query.Append("'',")
            End If
        Else
            query.Append(" '" & lblDealerCode.Text.Trim & "',")
        End If

        If txtNoPesanan.Text.Trim.Length > 0 Then
            query.Append(" '" & txtNoPesanan.Text.Trim & "',")
        Else
            query.Append("'',")
        End If

        If txtDONumber.Text.Trim.Length > 0 Then
            query.Append(" '" & txtDONumber.Text.Trim & "',")
        Else
            query.Append("'',")
        End If

        If txtSONumber.Text.Trim.Length > 0 Then
            query.Append(" '" & txtSONumber.Text.Trim & "',")
        Else
            query.Append("'',")
        End If

        If txtNomorFaktur.Text.Trim.Length > 0 Then
            query.Append(" '" & txtNomorFaktur.Text.Trim & "',")
        Else
            query.Append("'',")
        End If

        If ddlDocumentType.SelectedIndex <> 0 Then
            query.Append(" '" & ddlDocumentType.SelectedValue & "',")
        Else
            query.Append("'',")
        End If

        If DateDiff(DateInterval.Day, icTglCetakFrom.Value, icTglCetakTo.Value) <= 65 Then
            query.Append(" '" & icTglCetakFrom.Value.ToString("MM/dd/yyyy") & "',")
            query.Append(" '" & icTglCetakTo.Value.ToString("MM/dd/yyyy") & "',")
        Else
            MessageBox.Show("Tanggal tidak boleh lebih dari 65 hari")
            Return False
        End If

        Return True
    End Function

    Protected Sub dgEDoc_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgEDoc.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim GridlblNo As Label = CType(e.Item.FindControl("GridlblNo"), Label)
            Dim GridlblDealerCode As Label = CType(e.Item.FindControl("GridlblDealerCode"), Label)
            Dim GridlblNoPemesanan As Label = CType(e.Item.FindControl("GridlblNoPemesanan"), Label)
            Dim GridlblSOMMKSI As Label = CType(e.Item.FindControl("GridlblSOMMKSI"), Label)
            Dim GridlblDOMMKSI As Label = CType(e.Item.FindControl("GridlblDOMMKSI"), Label)
            Dim GridlblNoFaktur As Label = CType(e.Item.FindControl("GridlblNoFaktur"), Label)
            Dim GridLnkDownloadFaktur As LinkButton = CType(e.Item.FindControl("GridLnkDownloadFaktur"), LinkButton)
            Dim GridLnkDownloadPenaltiPengembalianBarang As LinkButton = CType(e.Item.FindControl("GridLnkDownloadPenaltiPengembalianBarang"), LinkButton)
            Dim GridLnkDownloadEOPackListCase As LinkButton = CType(e.Item.FindControl("GridLnkDownloadEOPackListCase"), LinkButton)
            Dim GridLnkDownloadEOPackListSummary As LinkButton = CType(e.Item.FindControl("GridLnkDownloadEOPackListSummary"), LinkButton)
            Dim GridLnkDownloadROPackListCase As LinkButton = CType(e.Item.FindControl("GridLnkDownloadROPackListCase"), LinkButton)
            Dim GridLnkDownloadROPackListSummary As LinkButton = CType(e.Item.FindControl("GridLnkDownloadROPackListSummary"), LinkButton)
            Dim GridLnkDownloadCreditMemoManual As LinkButton = CType(e.Item.FindControl("GridLnkDownloadCreditMemoManual"), LinkButton)

            Dim rowValue As DataRow = CType(e.Item.DataItem, DataRow)

            GridlblNo.Text = e.Item.ItemIndex + 1 + (dgEDoc.CurrentPageIndex * dgEDoc.PageSize)
            GridlblDealerCode.Text = CType(rowValue("DealerCode"), String)
            GridlblNoPemesanan.Text = CType(rowValue("NomorPesanan"), String)
            GridlblSOMMKSI.Text = CType(rowValue("NomorSO"), String)
            GridlblDOMMKSI.Text = CType(rowValue("NomorDO"), String)
            GridlblNoFaktur.Text = CType(rowValue("NomorFaktur"), String)



            If rowValue("CreditMemoRetur").ToString <> "" Then
            End If

            If rowValue("CreditMemoRetur(Manual)").ToString <> "" Then
                GridLnkDownloadCreditMemoManual.CommandArgument = rowValue("CreditMemoRetur(Manual)").ToString
                GridLnkDownloadCreditMemoManual.Visible = True
            End If

            If rowValue("Faktur").ToString <> "" Then
                GridLnkDownloadFaktur.CommandArgument = rowValue("Faktur").ToString
                GridLnkDownloadFaktur.Visible = True
            End If

            If rowValue("EOPackingListCase").ToString <> "" Then
                GridLnkDownloadEOPackListCase.CommandArgument = rowValue("EOPackingListCase").ToString
                GridLnkDownloadEOPackListCase.Visible = True
            End If

            If rowValue("EOPackingListSummary").ToString <> "" Then
                GridLnkDownloadEOPackListSummary.CommandArgument = rowValue("EOPackingListSummary").ToString
                GridLnkDownloadEOPackListSummary.Visible = False
            End If

            If rowValue("ROPackingListCase").ToString <> "" Then
                GridLnkDownloadROPackListCase.CommandArgument = rowValue("ROPackingListCase").ToString
                GridLnkDownloadROPackListCase.Visible = True
            End If

            If rowValue("ROPackingListSummary").ToString <> "" Then
                GridLnkDownloadROPackListSummary.CommandArgument = rowValue("ROPackingListSummary").ToString
                GridLnkDownloadROPackListSummary.Visible = False
            End If

            If rowValue("PenaltyPengembalianBarang").ToString <> "" Then
                GridLnkDownloadPenaltiPengembalianBarang.CommandArgument = rowValue("PenaltyPengembalianBarang").ToString
                GridLnkDownloadPenaltiPengembalianBarang.Visible = True
            End If
        End If
    End Sub

    Protected Sub dgEDoc_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgEDoc.PageIndexChanged
        dgEDoc.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
    End Sub

    Protected Sub dgEDoc_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgEDoc.ItemCommand
        Select Case e.CommandName
            Case "Download"
                Dim dataFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & e.CommandArgument
                Dim fileInfox As New FileInfo(dataFile)
                Dim fileExist As Boolean = CheckFileExist(fileInfox)
                If fileExist Then
                    Try
                        Response.Redirect("../Download.aspx?file=" & dataFile)
                    Catch ex As Exception
                        MessageBox.Show(SR.DownloadFail(""))
                    End Try
                Else
                    MessageBox.Show(SR.FileNotFound(fileInfox.Name))
                End If
        End Select
    End Sub

    Private Function CheckFileExist(ByVal fileinfo As FileInfo) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Try
            success = imp.Start()
            If success Then
                Return fileinfo.Exists
            End If
        Catch ex As Exception
            Return False
        Finally
            imp.StopImpersonate()
            imp = Nothing
        End Try

    End Function

    Protected Sub txtKodeDealer_TextChanged(sender As Object, e As EventArgs) Handles txtKodeDealer.TextChanged
        Dim getDealer As Dealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text.Trim)
        lblDealerName.Text = getDealer.DealerCode & " / " & getDealer.DealerName
    End Sub
End Class