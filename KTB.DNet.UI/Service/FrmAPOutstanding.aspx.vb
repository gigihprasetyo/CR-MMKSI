Imports System.IO
Imports System.Threading
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports System.Globalization
Imports System.Collections.Generic
Imports KTB.DNet.BusinessFacade

Public Class FrmAPOutstanding
    Inherits System.Web.UI.Page

    Private ArlMonthly As ArrayList
    Private objMonthy As MonthlyDocument
    Private sessHelper As New SessionHelper
    Private objDealer As Dealer

    Private Sub ActivateUserPrivilege()
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        If Not SecurityProvider.Authorize(Context.User, SR.DokumenServiceView_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=AP Outstanding")
        End If
    End Sub

    Private Sub RetrieveMaster()
        '--DropDownList JenisDokumen
        'add privilege
        Dim arlList As New ArrayList
        'If Not SecurityProvider.Authorize(Context.User, SR.DokumenServiceAll_Privilege) Then
        '    Me.ddlJenisDokumen.DataSource = New ArrayList
        '    Me.ddlJenisDokumen.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        'Else
        Dim arlListTemp As ArrayList = MonthlyDocumentType.RetrieveDocumentType()
        For Each item As MonthlyDocumentTypeListItem In arlListTemp
            If SecurityProvider.Authorize(Context.User, SR.DocServiceIndepB_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Deposit_B_Interest Then
                    arlList.Add(item)
                End If
            End If
            If SecurityProvider.Authorize(Context.User, SR.DocServiceKudepB_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Deposit_B_Kwitansi Then
                    arlList.Add(item)
                End If
            End If
            If SecurityProvider.Authorize(Context.User, SR.DocServicePMLett_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Periodical_Maintenance_Letter Then
                    arlList.Add(item)
                End If
            End If
            If SecurityProvider.Authorize(Context.User, SR.DokumenService_wscsta_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Warranty_ESP_Status_List Then
                    arlList.Add(item)
                End If
            End If
            If SecurityProvider.Authorize(Context.User, SR.DokumenService_lwsc01_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Warranty_ESP_Letter Then
                    arlList.Add(item)
                End If
            End If
            If SecurityProvider.Authorize(Context.User, SR.DokumenService_lfsc01_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_Service_Campaign_Letter Then
                    arlList.Add(item)
                End If
            End If
            If SecurityProvider.Authorize(Context.User, SR.DokumenService_lfs001_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_Service_Regular_Letter Then
                    arlList.Add(item)
                End If
            End If
            If SecurityProvider.Authorize(Context.User, SR.DokumenService_lpdi01_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.PDI_Letter Then
                    arlList.Add(item)
                End If
            End If
            If SecurityProvider.Authorize(Context.User, SR.DokumenService_kfsc01_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_Free_Service_Campaign Then
                    arlList.Add(item)
                End If
            End If
            If SecurityProvider.Authorize(Context.User, SR.DokumenService_kwsc01_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_ESP_Warranty Then
                    arlList.Add(item)
                End If
            End If
            If SecurityProvider.Authorize(Context.User, SR.DokumenService_Depb01_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Deposit_B_Report Then
                    arlList.Add(item)
                End If
            End If
            If SecurityProvider.Authorize(Context.User, SR.DokumenService_lpdi02_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kend_Belum_PDI_List Then
                    arlList.Add(item)
                End If
            End If

            'farid additional 20190828 ---------------------------------------------------------------------------------
            If SecurityProvider.Authorize(Context.User, SR.Free_service_regular_status_list_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_service_regular_status_list Then
                    arlList.Add(item)
                End If
            End If

            If SecurityProvider.Authorize(Context.User, SR.Free_service_campaign_status_list_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_service_campaign_status_list Then
                    arlList.Add(item)
                End If
            End If

            If SecurityProvider.Authorize(Context.User, SR.Free_maintenance_status_list_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_maintenance_status_list Then
                    arlList.Add(item)
                End If
            End If

            If SecurityProvider.Authorize(Context.User, SR.Free_labor_status_list_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_labor_status_list Then
                    arlList.Add(item)
                End If
            End If
            'farid additional 20190828 ---------------------------------------------------------------------------------


            'Tambahan CR Standard
            If SecurityProvider.Authorize(Context.User, SR.DokumenService_fll01_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_ESP_Labour_Letter Then
                    arlList.Add(item)
                End If
            End If

            If SecurityProvider.Authorize(Context.User, SR.DokumenService_kfl01_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_ESP_Free_Labour Then
                    arlList.Add(item)
                End If
            End If

            If SecurityProvider.Authorize(Context.User, SR.dokumen_service_lfm01_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_Maintenance_Letter Then
                    arlList.Add(item)
                End If
            End If

            If SecurityProvider.Authorize(Context.User, SR.dokumen_service_kfm01_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_Free_Maintenance Then
                    arlList.Add(item)
                End If
            End If

            If SecurityProvider.Authorize(Context.User, SR.Kwitansi_Warranty_Spare_Part_Accessories_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_Warranty_Spare_Part_Accessories Then
                    arlList.Add(item)
                End If
            End If

            If SecurityProvider.Authorize(Context.User, SR.Kwitansi_Free_Maintenance_and_Campaign_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_Free_Maintenance_and_Campaign Then
                    arlList.Add(item)
                End If
            End If

            If SecurityProvider.Authorize(Context.User, SR.Free_Maintenance_and_campaign_letter_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_Maintenance_and_campaign_letter Then
                    arlList.Add(item)
                End If
            End If

        Next
        Dim aList As New ArrayList
        For Each item As MonthlyDocumentTypeListItem In arlList
            If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kend_Belum_PDI_List Then
            Else
                aList.Add(item)
            End If
        Next
        arlList = aList
        Me.ddlJenisDokumen.DataSource = arlList
        Me.ddlJenisDokumen.DataTextField = "NameStatus"
        Me.ddlJenisDokumen.DataValueField = "ValStatus"
        Me.ddlJenisDokumen.DataBind()
        Me.ddlJenisDokumen.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        'End If


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ActivateUserPrivilege()
        ViewState("CurrentSortColumn") = "PeriodeMonth"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        If Not IsPostBack Then
            RetrieveMaster()
            dtgAPoutstanding.DataSource = New ArrayList
            dtgAPoutstanding.DataBind()
        End If

        If objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            txtKodeDealer.Text = objDealer.DealerCode
            txtKodeDealer.Enabled = False
            lblSearchDealer.Visible = False
            txtArea.Text = objDealer.Area1.Description
            txtArea.Enabled = False
        End If

        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
    End Sub

    Private Sub BindToDataGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim accNoEmpty As String = String.Empty
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "AccountingNo", MatchType.No, "'" & accNoEmpty & "'"))

        If Not String.IsNullorEmpty(txtKodeDealer.Text) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        End If

        If Not String.IsNullorEmpty(txtArea.Text) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Dealer.Area1.Description", MatchType.Partial, txtArea.Text))
        End If

        If Not String.IsNullorEmpty(txtNoReff.Text) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "BillingNo", MatchType.InSet, "('" & txtNoReff.Text.Replace(";", "','") & "')"))
        End If

        If ddlJenisDokumen.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Kind", MatchType.Exact, ddlJenisDokumen.SelectedValue))
        
        If Not String.IsNullorEmpty(txtNoAccounting.Text) Then
            Dim strQuery As String = "(select id from MonthlyDocument (nolock) where AccountingNo like '%" & txtNoAccounting.Text.Trim & "%')"
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "id", MatchType.InSet, strQuery))
        End If

        If Not String.IsNullorEmpty(txtClearingNo.Text) Then
            Dim strQuery As String = "(select id from MonthlyDocument (nolock) where NoClearing like '%" & txtClearingNo.Text.Trim & "%')"
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "id", MatchType.InSet, strQuery))
        End If

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "BillingDate", MatchType.GreaterOrEqual, DueDateFrom.Value))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "BillingDate", MatchType.LesserOrEqual, DueDateTo.Value))

        ArlMonthly = New MonthlyDocumentFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dtgAPoutstanding.PageSize, _
              total, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        If ArlMonthly.Count = 0 Then
            MessageBox.Show("Data Tidak ditemukan")
        End If

        dtgAPoutstanding.VirtualItemCount = total
        dtgAPoutstanding.DataSource = ArlMonthly
        sessHelper.SetSession("MonthlyDocument", ArlMonthly)
        sessHelper.SetSession("CRITERIASfrMonthly", criterias)
        dtgAPoutstanding.DataBind()

    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        dtgAPoutstanding.CurrentPageIndex = 0
        BindToDataGrid(dtgAPoutstanding.CurrentPageIndex)
    End Sub

    Sub dtgAPoutstanding_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgAPoutstanding.ItemDataBound
        If Not IsNothing(ArlMonthly) Then
            If (ArlMonthly.Count > 0 And e.Item.ItemIndex <> -1) Then
                objMonthy = ArlMonthly(e.Item.ItemIndex)
                Dim assignment As String = String.Empty

                CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgAPoutstanding.CurrentPageIndex * dtgAPoutstanding.PageSize)

                If objMonthy.Kind > -1 Then
                    CType(e.Item.FindControl("lblJenisDokument"), Label).Text = CType(objMonthy.Kind, MonthlyDocumentType.MonthlyDocumentTypeEnum).ToString
                End If

                If Not IsNothing(objMonthy.Dealer) Then
                    CType(e.Item.FindControl("lblDealerCode"), Label).Text = objMonthy.Dealer.DealerCode
                    CType(e.Item.FindControl("lblDealerName"), Label).Text = objMonthy.Dealer.DealerName

                    Dim dlr As Dealer = New DealerFacade(User).Retrieve(objMonthy.Dealer.ID)
                    If dlr.ID <> 0 Then
                        If Not IsNothing(dlr.MainArea) Then
                            CType(e.Item.FindControl("lblRegion"), Label).Text = dlr.MainArea.Description
                        Else
                            CType(e.Item.FindControl("lblRegion"), Label).Text = ""
                        End If

                        If Not IsNothing(dlr.Area1) Then
                            CType(e.Item.FindControl("lblArea"), Label).Text = dlr.Area1.Description
                        Else
                            CType(e.Item.FindControl("lblArea"), Label).Text = ""
                        End If

                    End If
                End If
                
                CType(e.Item.FindControl("lblDocNo"), Label).Text = objMonthy.AccountingNo
                CType(e.Item.FindControl("lblReffNo"), Label).Text = objMonthy.BillingNo

                If objMonthy.SettlementDate = "1753-01-01" Then
                    CType(e.Item.FindControl("lblPostingDate"), Label).Text = ""
                Else
                    CType(e.Item.FindControl("lblPostingDate"), Label).Text = Format(objMonthy.SettlementDate, "yyyyMMdd")
                End If
                'CType(e.Item.FindControl("lblPostingDate"), Label).Text = objMonthy.SettlementDate
                CType(e.Item.FindControl("lblYear"), Label).Text = objMonthy.PeriodeYear
                'CType(e.Item.FindControl("lblActualTransfer"), Label).Text = objMonthy.ActualTransferDate
                If objMonthy.ActualTransferDate = "1753-01-01" Then
                    CType(e.Item.FindControl("lblActualTransfer"), Label).Text = ""
                Else
                    CType(e.Item.FindControl("lblActualTransfer"), Label).Text = Format(objMonthy.ActualTransferDate, "yyyyMMdd")
                End If

                If objMonthy.id <> 0 Then
                    Dim mdEvid As MonthlyDocumentToFakturEvidance = New MonthlyDocumentToFakturEvidanceFacade(User).RetrieveByMDId(objMonthy.id)
                    If mdEvid.ID <> 0 AndAlso Not String.IsNullorEmpty(mdEvid.FakturNumber) Then
                        CType(e.Item.FindControl("lblAssignment"), Label).Text = mdEvid.FakturNumber
                        assignment = mdEvid.FakturNumber
                    Else
                        If objMonthy.BillingDate = "1753-01-01" Then
                            CType(e.Item.FindControl("lblAssignment"), Label).Text = ""
                        Else
                            CType(e.Item.FindControl("lblAssignment"), Label).Text = Format(objMonthy.BillingDate, "yyyyMMdd")
                        End If

                    End If
                End If

                CType(e.Item.FindControl("lblParkedName"), Label).Text = objMonthy.ParkedName
                If objMonthy.Amount = 0 Then
                    CType(e.Item.FindControl("lblAmount"), Label).Text = ""
                Else
                    CType(e.Item.FindControl("lblAmount"), Label).Text = objMonthy.Amount.ToString("N0")
                End If

                CType(e.Item.FindControl("lblCurrency"), Label).Text = objMonthy.Currencies
                CType(e.Item.FindControl("lblText"), Label).Text = objMonthy.Description
                If objMonthy.NoClearing = 0 Then
                    CType(e.Item.FindControl("lblClearing"), Label).Text = ""
                Else
                    CType(e.Item.FindControl("lblClearing"), Label).Text = objMonthy.NoClearing
                End If

                If objMonthy.TransferDate <> "1753-01-01" Then
                    CType(e.Item.FindControl("lblEstimasiRT"), Label).Text = Format(objMonthy.TransferDate, "dd.MM.yyyy")
                Else
                    CType(e.Item.FindControl("lblEstimasiRT"), Label).Text = ""
                End If

                Dim kind As Integer = objMonthy.Kind
                If kind = 0 Or kind = 10 Then
                    CType(e.Item.FindControl("lblDocument"), Label).Text = "DEPOSIT"
                ElseIf kind = 1 Or kind = 6 Or kind = 7 Or kind = 22 Then
                    CType(e.Item.FindControl("lblDocument"), Label).Text = "WSC"
                ElseIf kind = 3 Then
                    CType(e.Item.FindControl("lblDocument"), Label).Text = "PDI"
                ElseIf kind = 2 Or kind = 4 Or kind = 5 Or kind = 11 Or kind = 12 Or kind = 13 Or kind = 14 Or kind = 15 Or kind = 19 Or kind = 23 Then
                    CType(e.Item.FindControl("lblDocument"), Label).Text = "FS"
                ElseIf kind = 8 Then
                    CType(e.Item.FindControl("lblDocument"), Label).Text = "PM"
                Else
                    CType(e.Item.FindControl("lblDocument"), Label).Text = "-"
                End If


                If objMonthy.NoClearing <> 0 AndAlso Not String.IsNullorEmpty(objMonthy.Description) AndAlso Not String.IsNullorEmpty(assignment) Then
                    CType(e.Item.FindControl("lblReason"), Label).Text = "Already Paid"
                ElseIf String.IsNullorEmpty(objMonthy.Description) AndAlso String.IsNullorEmpty(assignment) Then
                    CType(e.Item.FindControl("lblReason"), Label).Text = "Invoice not received"
                ElseIf objMonthy.NoClearing = 0 AndAlso Not String.IsNullorEmpty(assignment) Then
                    CType(e.Item.FindControl("lblReason"), Label).Text = "Process"
                ElseIf Not String.IsNullorEmpty(objMonthy.ActualTransferDate) AndAlso objMonthy.NoClearing <> 0 Then
                    CType(e.Item.FindControl("lblReason"), Label).Text = "Already Paid"
                Else
                    CType(e.Item.FindControl("lblReason"), Label).Text = "-"
                End If


            End If
        End If
    End Sub

    Private Sub dtgAPoutstanding_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgAPoutstanding.PageIndexChanged
        dtgAPoutstanding.CurrentPageIndex = e.NewPageIndex
        BindToDataGrid(e.NewPageIndex + 1)
    End Sub
End Class