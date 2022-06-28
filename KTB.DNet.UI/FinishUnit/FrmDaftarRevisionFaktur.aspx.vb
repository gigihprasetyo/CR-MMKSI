#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
Imports System.Linq
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.WebCC
Imports KTB.DNet.SAP
Imports KTB.DNet.UI.Helper
Imports System.Globalization

#End Region

Public Class FrmDaftarRevisionFaktur
    Inherits System.Web.UI.Page

#Region " Private fields "
    Dim sessHelp As SessionHelper = New SessionHelper
    Dim InvoiceRevisionList As New ArrayList  '-- List of invoice
    Dim _PCAccessAllowed As Boolean = False
    Dim _CVAccessAllowed As Boolean = False
    Dim _LCVAccessAllowed As Boolean = False
    Dim _allowEdit As Boolean = False
    Private objDealer As Dealer
#End Region

#Region " EventHandler "
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        btnProcess.Attributes.Add("onclick", "return IsChecked();")
        btnTransfer.Attributes.Add("onclick", "return IsChecked();")
        btnConfirm.Attributes.Add("onclick", "return IsChecked();")
        btnProcess.Attributes.Add("onclick", "MakeValid();")
        btnTransfer.Attributes.Add("onclick", "MakeValid();")
        btnSearch.Attributes.Add("onclick", "MakeValid();")
        btnRetransfer.Attributes.Add("onclick", "MakeValid();")
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        lblSearchRemark.Attributes("onclick") = "ShowPPRemark();"
        InitiateAuthorization()
        If Not IsPostBack Then
            objDealer = Session("DEALER")
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                btnDownload.Visible = True
            End If

            chkRequest.Checked = True
            icRequestStart.Value = Date.Now.AddDays(-64)



            BindDropdownList()  '-- Bind dropdownlist
            BindListBoxList()
            BindRevisionTypeDropDownList(ddlRevisionType)
            BindPaymentStatusDropDownList(ddlPaymentStatus)
            ViewState("currSortColumn") = "RegNumber"
            ViewState("currSortDirection") = Sort.SortDirection.DESC
            Dim crit As Hashtable = New Hashtable
            crit = CType(Session("CriteriaFormInvoiceRevisionList"), Hashtable)
            If Not crit Is Nothing Then
                txtKodeDealer.Text = CStr(crit.Item("Dealer"))
                txtNoRequest.Text = CStr(crit.Item("RegNumber"))
                txtRevisionFakturNo.Text = CStr(crit.Item("RevisionFakturNo"))
                txtChassisNo.Text = CStr(crit.Item("ChassisNumber"))
                lboxStatus.Items(0).Selected = CType(crit("Validasi"), Boolean)
                lboxStatus.Items(1).Selected = CType(crit("Konfirmasi"), Boolean)
                lboxStatus.Items(2).Selected = CType(crit("Proses"), Boolean)
                lboxStatus.Items(3).Selected = CType(crit("Selesai"), Boolean)
                ddlCategory.SelectedValue = CStr(crit.Item("Category"))
                Try
                    CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)

                    ddlSubCategory.SelectedValue = CStr(crit.Item("ddlSubCategory"))
                Catch ex As Exception

                End Try

                ddlRevisionType.SelectedValue = CStr(crit.Item("Type"))
                ddlPembayaran.SelectedValue = CStr(crit.Item("IsPay"))
                ddlIsTemporary.SelectedValue = CStr(crit.Item("IsTemporary"))
                ddlPaymentStatus.SelectedValue = CStr(crit.Item("PaymentStatus"))
                chkRequest.Checked = CType(crit("chkRequest"), Boolean)
                icRequestStart.Value = CType(crit("icRequestStart"), Date)
                icRequestEnd.Value = CType(crit("icRequestEnd"), Date)
                chkValidPeriod.Checked = CType(crit("chkValidPeriod"), Boolean)
                icStartValid.Value = CType(crit("StartValid"), Date)
                icEndValid.Value = CType(crit("EndValid"), Date)
                dgInvoiceRevisionList.CurrentPageIndex = CInt(crit.Item("PageIndex"))
                ReadData()   '-- Read all data matching criteria
                BindPage(dgInvoiceRevisionList.CurrentPageIndex)  '-- Bind page-1
            Else
                ReadData()   '-- Read all data matching criteria
                'BindPage(dgInvoiceRevisionList.CurrentPageIndex)  '-- Bind page-1
            End If

        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If ddlCategory.SelectedValue = "" Then
                MessageBox.Show("Silahkan untuk memilih Kategori Kendaraan !!")
                ddlCategory.Focus()
                Exit Sub
            End If

            If ddlIsTemporary.SelectedValue = "-1" Then
                MessageBox.Show("Silahkan untuk memilih Kategori Temporary !!")
                ddlIsTemporary.Focus()
                Exit Sub
            End If
        End If

        storeCriteria()
        ReadData()   '-- Read all data matching criteria
        dgInvoiceRevisionList.CurrentPageIndex = 0
        BindPage(dgInvoiceRevisionList.CurrentPageIndex)  '-- Bind page-1
    End Sub

    Private Sub dgInvoiceRevisionList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgInvoiceRevisionList.SortCommand
        '-- Sort datagrid rows based on a column header clicked
        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If

        '-- Bind page-1
        dgInvoiceRevisionList.CurrentPageIndex = 0
        ReadData()
        BindPage(dgInvoiceRevisionList.CurrentPageIndex)

    End Sub

    Private Sub dgInvoiceRevisionList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgInvoiceRevisionList.PageIndexChanged
        '-- Change datagrid page
        dgInvoiceRevisionList.CurrentPageIndex = e.NewPageIndex
        BindPage(e.NewPageIndex)
    End Sub

    Private Sub dgInvoiceRevisionList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgInvoiceRevisionList.ItemCommand

        If e.CommandName = "lnkDetail" Then
            '-- Retrieve Invoice and its related end customer if any
            Dim revisionFaktur As RevisionFaktur = New RevisionFakturFacade(User).Retrieve(CType(e.Item.Cells(0).Text, Integer))

            '-- Store Invoice and its related end customer for display on form FrmEntryInvoiceRevision.aspx
            sessHelp.SetSession("ChassisMaster", revisionFaktur.ChassisMaster)
            sessHelp.SetSession("RevisionFaktur", revisionFaktur)
            storeCriteria()

            '-- Store the calling page
            sessHelp.SetSession("FrmEntryInvoiceRevision_CalledBy", "FrmDaftarRevisionFaktur.aspx")
            Response.Redirect("FrmEntryInvoiceRevision.aspx?t=" & e.Item.Cells(1).Text & "&mode=" & EnumDNET.enumFormMode.View)

        ElseIf e.CommandName = "ReviseInvoice" Then
            '-- Retrieve Invoice and its related end customer if any
            Dim RevisionFaktur As RevisionFaktur = New RevisionFakturFacade(User).Retrieve(CType(e.Item.Cells(0).Text, Integer))

            '-- Store Invoice and its related end customer for display on form FrmEntryInvoiceRevision.aspx
            sessHelp.SetSession("ChassisMaster", RevisionFaktur.ChassisMaster)
            sessHelp.SetSession("RevisionFaktur", RevisionFaktur)

            storeCriteria()

            If RevisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Baru Or RevisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Validasi Then
                '-- Store the calling page
                sessHelp.SetSession("FrmEntryInvoiceRevision_CalledBy", "FrmDaftarRevisionFaktur.aspx")

                '-- Display Invoice and its related end customer on Entry Invoice page
                Response.Redirect("FrmEntryInvoiceRevision.aspx?t=" & e.Item.Cells(1).Text & "&mode=" & EnumDNET.enumFormMode.Edit)
            End If

        ElseIf e.CommandName = "ShowHistory" Then
            '-- Retrieve Invoice and its related end customer if any
            Dim revisionFaktur As RevisionFaktur = New RevisionFakturFacade(User).Retrieve(CType(e.Item.Cells(0).Text, Integer))

            '-- Retrieve Invoice and its related end customer if any
            'Dim chassisMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(revisionFaktur.ChassisMaster.ID)

            '-- Store Invoice and its related end customer for display on form FrmEntryInvoice.aspx
            'sessHelp.SetSession("ChassisMaster", chassisMaster)

            '-- Store the calling page
            sessHelp.SetSession("FrmInvoiceRevisionHistory_CalledBy", "FrmDaftarRevisionFaktur.aspx")

            storeCriteria()

            '-- Display Invoice and its related end customer on Entry Invoice page
            Response.Redirect("FrmInvoiceRevisionHistory.aspx?ChassisMasterID=" & revisionFaktur.ChassisMaster.ID)
        End If

    End Sub

    Private Sub dgInvoiceRevisionList_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgInvoiceRevisionList.ItemDataBound
        Dim RowValue As RevisionFaktur = CType(e.Item.DataItem, RevisionFaktur)

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '-- Grid detail items

            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionPaymentDetail), "RowStatus", MatchType.Exact, (CType(DBRowStatus.Active, Short))))
            crit.opAnd(New Criteria(GetType(RevisionPaymentDetail), "RevisionFaktur.ID", MatchType.Exact, RowValue.ID))
            Dim arlRevDet As ArrayList = New RevisionPaymentDetailFacade(User).Retrieve(crit)
            If RowValue.IsPay = 1 Then
                If arlRevDet.Count = 0 Then
                    Dim intervalFaktur As Integer = DateDiff(DateInterval.Day, RowValue.NewConfirmationDate, Date.Now)
                    If intervalFaktur >= 3 AndAlso intervalFaktur <= 7 Then
                        e.Item.BackColor = Color.Yellow
                    ElseIf intervalFaktur >= 7 Then
                        e.Item.BackColor = Color.Red
                    End If
                End If
            End If

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (dgInvoiceRevisionList.CurrentPageIndex * dgInvoiceRevisionList.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No

            objDealer = Session("DEALER")
            'Add Revision Button
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER And _allowEdit Then
                Dim lnkReviseInvoice As LinkButton = CType(e.Item.FindControl("lnkReviseInvoice"), LinkButton)
                If RowValue.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Baru Or RowValue.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Validasi Then
                    lnkReviseInvoice.Visible = True
                End If
            End If

            'Add Status History
            Dim lnkHistoryStatus As LinkButton = CType(e.Item.FindControl("lblHistoryStatus"), LinkButton)
            lnkHistoryStatus.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpRevisionStatusHistory.aspx?No=" & RowValue.RegNumber, "", 400, 400, "popupopened")

            'Add Revision History button
            Dim lnkShowHistory As LinkButton = CType(e.Item.FindControl("lnkShowHistory"), LinkButton)
            If RowValue.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Selesai Then 'AndAlso RowValue.V_EndCustomerRev.EndCustomerID <> RowValue.V_EndCustomerRev.ID Then 'sdh pernah di revisi
                lnkShowHistory.Visible = True
            Else
                lnkShowHistory.Visible = False
            End If

            Dim lnkRemarkGrid As LinkButton = CType(e.Item.FindControl("lnkRemarkGrid"), LinkButton)
            If (RowValue.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Validasi) Then
                lnkRemarkGrid.Visible = True
                If RowValue.Remark.Trim <> String.Empty Then
                    Dim str() As String = RowValue.Remark.Split(Chr(1))
                    If str.Length = 3 Then
                        lnkRemarkGrid.ToolTip = str(2) & Chr(13) & Chr(10) & "Dibuat Oleh: " & UserInfo.Convert(str(0)) & Chr(13) & Chr(10) & "Dibuat Pada: " & str(1)
                    Else
                        lnkRemarkGrid.ToolTip = RowValue.Remark
                    End If
                    lnkRemarkGrid.Visible = True
                Else
                    lnkRemarkGrid.ToolTip = "Belum ada Pending Reason"
                    lnkRemarkGrid.Visible = False
                End If
            End If

            Dim lblRemarkMatching As Label = CType(e.Item.FindControl("lblRemarkMatching"), Label)
            If RowValue.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Konfirmasi Then
                If RowValue.Remark.Trim <> String.Empty Then
                    lblRemarkMatching.Visible = True
                    lblRemarkMatching.Text = RowValue.Remark
                End If
            End If

            If RowValue.RevisionStatus <> EnumDNET.enumFakturKendaraanRev.Selesai Then
                If (DateTime.Now.Date - RowValue.CreatedTime.Date).Days >= 30 Then
                    e.Item.BackColor = System.Drawing.Color.Red
                End If
            End If
        End If
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Dim InvoiceList, revisionFakturList As New ArrayList  '-- List of invoices selected
        Dim mess As String = String.Empty

        '-- Iterate all records in grid "dgInvoiceRevisionList"
        For Each item As DataGridItem In dgInvoiceRevisionList.Items

            '-- If it is selected then process it
            If CType(item.FindControl("chkSelect"), CheckBox).Checked Then

                Dim id As Integer = item.Cells(0).Text  '-- ID column
                Dim revisionFaktur As RevisionFaktur = New RevisionFakturFacade(User).Retrieve(id)  '-- Retrieve this invoice based on ID

                Dim messTemp As String = String.Empty
                If revisionFaktur.RevisionType.ID = 2 Then
                    CommonFunction.RevFakturIsValidData(revisionFaktur.ChassisMaster, messTemp)
                    If messTemp.Length > 0 Then
                        mess &= revisionFaktur.ChassisMaster.ChassisNumber & " " & messTemp & "\n"
                        Dim rFacade As Integer = New RevisionFakturFacade(User).UpdateRemark(revisionFaktur, messTemp)
                    End If
                End If

                '-- Only invoices with status 'Validasi' and with Revision Faktur defined
                If Not IsNothing(revisionFaktur) AndAlso messTemp.Length = 0 Then
                    If revisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Validasi Then

                        revisionFaktur.RevisionStatus = EnumChassisMaster.FakturStatus.Konfirmasi  '-- Change revision invoice status
                        revisionFaktur.NewConfirmationBy = User.Identity.Name  '-- Set its confirmator
                        revisionFaktur.NewConfirmationDate = Date.Now  '-- Set its confirmation date

                        If revisionFaktur.OldEndCustomer.IsTemporary = CType(EnumEndCustomer.TemporaryFaktur.Temporary, Short) Then
                            revisionFaktur.IsPay = EnumDNET.enumPaymentOption.TidakBayar
                        End If

                        revisionFakturList.Add(revisionFaktur)  '-- Add to list of Infoicerevision
                    End If
                End If
            End If
        Next

        '-- If there exists at least an Infoicerevision selected then do update transaction
        If revisionFakturList.Count > 0 Then
            Dim revisionFakturFac As New RevisionFakturFacade(User)
            revisionFakturFac.UpdateTransaction(revisionFakturList)  '-- Update list of invoice selected

            ReadData()  '-- Re-read all data to refresh changes
            BindPage(dgInvoiceRevisionList.CurrentPageIndex) '-- Re-bind current page

            If mess.Trim.Length > 0 Then
                MessageBox.Show("Sebagian Proses Gagal\n" & mess)
            Else
                MessageBox.Show("Konfirmasi data berhasil")
            End If
        Else
            If mess.Trim.Length > 0 Then
                MessageBox.Show("Sebagian Proses Gagal\n" & mess)
            Else
                MessageBox.Show("Konfirmasi data tidak bisa dilakukan\nkarena status revisi faktur bukan 'validasi'")
            End If
        End If

    End Sub

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        Dim isPayRevisionFakturList, revisionFakturList As New ArrayList  '-- List of invoices selected

        '-- Iterate all records in grid "dgInvoiceRevisionList"
        For Each item As DataGridItem In dgInvoiceRevisionList.Items

            '-- If it is selected then process it
            If CType(item.FindControl("chkSelect"), CheckBox).Checked Then
                Dim id As Integer = item.Cells(0).Text  '-- ID column
                Dim revisionFaktur As RevisionFaktur = New RevisionFakturFacade(User).Retrieve(id)  '-- Retrieve this invoice based on ID

                '-- Only invoices with status 'Validasi' and with End Customer defined
                If Not IsNothing(revisionFaktur) Then
                    If revisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Konfirmasi Then

                        revisionFaktur.IsPay = ddlKategoriPembayaran.SelectedValue
                        revisionFakturList.Add(revisionFaktur)  '-- Add to list of Infoicerevision

                        ' set pay data if payment method is "Bayar"
                        If (revisionFaktur.IsPay = EnumDNET.enumPaymentOption.Bayar) Then
                            isPayRevisionFakturList.Add(revisionFaktur)
                        End If
                    End If
                End If
            End If
        Next

        '-- If there exists at least an EndCustomerRev selected then do update transaction
        If revisionFakturList.Count > 0 Then
            Dim revisionFakturFac As New RevisionFakturFacade(User)
            Dim result As Integer = revisionFakturFac.UpdateTransaction(revisionFakturList)  '-- Update list of invoice selected

            ReadData()  '-- Re-read all data to refresh changes
            BindPage(dgInvoiceRevisionList.CurrentPageIndex) '-- Re-bind current page

            Dim errMsg As String = String.Empty

            If result <> -1 And errMsg = String.Empty Then
                MessageBox.Show("Opsi pembayaran berhasil disimpan")
            End If

            If errMsg <> "" Then MessageBox.Show(errMsg)

            If (isPayRevisionFakturList.Count = 0) Then
                Exit Sub
            End If

            TransferFakturs(isPayRevisionFakturList, errMsg)
        Else
            MessageBox.Show("Opsi Pembayaran tidak bisa disimpan\nkarena status faktur bukan 'konfirmasi'")
        End If
    End Sub

    Private Sub btnTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransfer.Click
        Dim revisionFakturList, newKTPList, newKTPProcessList, successDataList As New ArrayList
        Dim strMsg As String = ""
        Dim result As Integer = -1

        For Each item As DataGridItem In dgInvoiceRevisionList.Items
            If CType(item.FindControl("chkSelect"), CheckBox).Checked Then
                Dim id As Integer = item.Cells(0).Text
                If id > 0 Then
                    Dim revisionFaktur As RevisionFaktur = New RevisionFakturFacade(User).Retrieve(id)

                    Dim messTemp As String = String.Empty
                    If revisionFaktur.RevisionType.ID = 2 Then
                        CommonFunction.RevFakturIsValidData(revisionFaktur.ChassisMaster, messTemp)
                        If messTemp.Length > 0 Then
                            strMsg &= "Transfer Gagal untuk No Chassis " & revisionFaktur.ChassisMaster.ChassisNumber & ". " & messTemp & "\n"
                            Dim rFacade As Integer = New RevisionFakturFacade(User).UpdateRemark(revisionFaktur, messTemp)
                        End If
                    End If

                    If Not IsNothing(revisionFaktur) AndAlso messTemp.Length = 0 Then
                        If revisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Konfirmasi Then
                            If revisionFaktur.IsPay = EnumDNET.enumPaymentOption.TidakBayar Then
                                '-- set status to proses
                                revisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Proses
                            ElseIf revisionFaktur.IsPay = EnumDNET.enumPaymentOption.Bayar Then
                                If IsPaid(revisionFaktur) Then
                                    '-- set status to proses
                                    revisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Proses
                                Else
                                    strMsg = strMsg & "Transfer Gagal untuk No Chassis " & revisionFaktur.ChassisMaster.ChassisNumber & ". Status pembayaran revisi faktur belum selesai.\n"
                                End If
                            End If

                            If revisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Proses Then
                                If revisionFaktur.RevisionType.ID = EnumDNET.enumRevType.RS Then
                                    newKTPList.Add(revisionFaktur)
                                Else
                                    revisionFakturList.Add(revisionFaktur)
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Next

        If (revisionFakturList.Count = 0 AndAlso newKTPList.Count = 0) Then
            If strMsg = String.Empty Then
                MessageBox.Show("Transfer ke SAP Gagal. Tidak ada Data Faktur dengan Status Konfirmasi")
                Exit Sub
            Else
                MessageBox.Show(strMsg)
                Exit Sub
            End If

        End If

        Dim revisionFakturFac As New RevisionFakturFacade(User)
        If revisionFakturList.Count > 0 Then
            result = revisionFakturFac.UpdateTransaction(revisionFakturList)
            If result <> -1 Then
                successDataList.AddRange(revisionFakturList)
            End If
        End If

        Dim InvalidMatchingList As ArrayList = New ArrayList
        If newKTPList.Count > 0 Then
            For Each revision As RevisionFaktur In newKTPList

                Dim dealerSystems As DealerSystems = New DealerSystemsFacade(User).RetrieveByDealerCode(revision.ChassisMaster.Dealer.DealerCode)
                Dim isDealerPiloting As Boolean = KTB.DNet.BusinessValidation.TCHelper.GetActiveTCResult(revision.ChassisMaster.Dealer.ID, CInt(EnumDealerTransType.DealerTransKind.PilotingSPKMatching))
                If IsNothing(dealerSystems) Then
                    strMsg = strMsg & "Konfigurasi Dealer systems untuk Dealer" + revision.ChassisMaster.Dealer.DealerName + "(" + revision.ChassisMaster.Dealer.DealerCode + ") tidak ditemukan. \n"
                Else
                    If dealerSystems.isSPKMatchFaktur OrElse isDealerPiloting Then
                        Dim spkChassisList As ArrayList = New SPKChassisFacade(User).RetrieveByChassisID(revision.ChassisMaster.ID)

                        If Not IsNothing(spkChassisList) Then
                            If spkChassisList.Count > 0 Then
                                Dim spkChassis As SPKChassis = New SPKChassis
                                spkChassis = spkChassisList(0)
                                If spkChassis.MatchingType = 1 Or spkChassis.MatchingType = 3 Then
                                    'If spkChassis.SPKDetail.SPKHeader.ID = revision.EndCustomer.RevisionSPKFaktur.SPKHeader.ID Then
                                    If Not IsNothing(revision.EndCustomer.Customer.MyCustomerRequest.SPKDetailCustomer) Then
                                        If spkChassis.SPKDetail.ID = revision.EndCustomer.Customer.MyCustomerRequest.SPKDetailCustomer.SPKDetail.ID Then
                                            newKTPProcessList.Add(revision)
                                        Else
                                            strMsg = strMsg & "Proses matching Chassis " & revision.ChassisMaster.ChassisNumber & " masih menggunakan nomor spk lama, silahkan match dengan spk baru.\n"
                                            Dim revisionInvalid As RevisionFaktur = New RevisionFakturFacade(User).Retrieve(revision.ID)
                                            revisionInvalid.Remark = "Proses matching chassis masih menggunakan nomor spk lama, silahkan match dengan spk baru"
                                            InvalidMatchingList.Add(revisionInvalid)
                                        End If
                                    Else
                                        strMsg = strMsg & "No Chassis " & revision.ChassisMaster.ChassisNumber & " menggunakan kode pelanggan yang tidak terdapat pada detail data SPK.\n"
                                        Dim revisionInvalid As RevisionFaktur = New RevisionFakturFacade(User).Retrieve(revision.ID)
                                        revisionInvalid.Remark = "No Chassis menggunakan kode pelanggan yang tidak terdapat pada detail data SPK"
                                        InvalidMatchingList.Add(revisionInvalid)
                                    End If
                                Else
                                    strMsg = strMsg & "No Chassis " & revision.ChassisMaster.ChassisNumber & " belum d matching dengan SPK yang sesuai.\n"
                                    Dim revisionInvalid As RevisionFaktur = New RevisionFakturFacade(User).Retrieve(revision.ID)
                                    revisionInvalid.Remark = "No Chassis belum d matching dengan SPK yang sesuai"
                                    InvalidMatchingList.Add(revisionInvalid)
                                End If
                            Else
                                strMsg = strMsg & "No Chassis " & revision.ChassisMaster.ChassisNumber & " belum memiliki data SPK matching. Silahkan Match Chassis dengan SPK Baru.\n"
                                Dim revisionInvalid As RevisionFaktur = New RevisionFakturFacade(User).Retrieve(revision.ID)
                                revisionInvalid.Remark = "No Chassis belum memiliki data SPK matching. Silahkan Match Chassis dengan SPK Baru."
                                InvalidMatchingList.Add(revisionInvalid)
                            End If
                        Else
                            strMsg = strMsg & "No Chassis " & revision.ChassisMaster.ChassisNumber & " belum memiliki data SPK matching. Silahkan Match Chassis dengan SPK Baru.\n"
                            Dim revisionInvalid As RevisionFaktur = New RevisionFakturFacade(User).Retrieve(revision.ID)
                            revisionInvalid.Remark = "No Chassis belum memiliki data SPK matching. Silahkan Match Chassis dengan SPK Baru."
                            InvalidMatchingList.Add(revisionInvalid)
                        End If
                    Else
                        newKTPProcessList.Add(revision)
                    End If
                End If
            Next
        End If

        If newKTPProcessList.Count > 0 Then
            result = revisionFakturFac.UpdateTransaction(newKTPProcessList)
            If result <> -1 Then
                successDataList.AddRange(newKTPProcessList)
            End If
        End If

        If InvalidMatchingList.Count > 0 Then
            result = revisionFakturFac.UpdateTransaction(InvalidMatchingList)
        End If

        If successDataList.Count > 0 Then
            Dim BFileDataList As ArrayList = New ArrayList
            BFileDataList = New System.Collections.ArrayList((From item As RevisionFaktur In successDataList.OfType(Of RevisionFaktur)()
                    Where item.RevisionType.ID <> EnumDNET.enumRevType.RT
                    Select item).ToList())

            Dim AFileDataList As ArrayList = New ArrayList
            AFileDataList = New System.Collections.ArrayList((From item As RevisionFaktur In successDataList.OfType(Of RevisionFaktur)()
                    Where item.RevisionType.ID = EnumDNET.enumRevType.RT
                    Select item).ToList())



            If BFileDataList.Count > 0 Then
                ' create csfile txt, only for type B
                TransferChasisMasterProfile(BFileDataList)
                ' create fkfile txt for type B
                Transfer("B", BFileDataList, strMsg)
            End If

            If AFileDataList.Count > 0 Then
                ' create fkfile txt for type A
                Transfer("A", AFileDataList, strMsg)
            End If

        End If

        ReadData()  '-- Re-read all data to refresh changes
        BindPage(dgInvoiceRevisionList.CurrentPageIndex) '-- Re-bind current page

        If result <> -1 And strMsg = String.Empty Then
            MessageBox.Show("Data berhasil ditransfer")
        End If

        If strMsg <> "" Then MessageBox.Show(strMsg)

    End Sub

    Private Sub btnRetransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRetransfer.Click
        Dim BFileDataList, AFileDataList As New ArrayList
        Dim strMsg As String = ""

        Dim mess As String = String.Empty
        For Each item As DataGridItem In dgInvoiceRevisionList.Items
            If CType(item.FindControl("chkSelect"), CheckBox).Checked Then
                Dim id As Integer = item.Cells(0).Text
                If id > 0 Then
                    Dim revisionFaktur As RevisionFaktur = New RevisionFakturFacade(User).Retrieve(id)

                    Dim messTemp As String = String.Empty
                    If revisionFaktur.RevisionType.ID = 2 Then
                        CommonFunction.RevFakturIsValidData(revisionFaktur.ChassisMaster, messTemp)
                        If messTemp.Length > 0 Then
                            mess &= revisionFaktur.ChassisMaster.ChassisNumber & " " & messTemp & "\n"
                            Dim rFacade As Integer = New RevisionFakturFacade(User).UpdateRemark(revisionFaktur, messTemp)
                        End If
                    End If

                    If Not IsNothing(revisionFaktur) AndAlso messTemp.Length = 0 Then
                        If revisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Proses Then
                            If revisionFaktur.RevisionType.ID <> EnumDNET.enumRevType.RT Then
                                BFileDataList.Add(revisionFaktur)
                            ElseIf revisionFaktur.RevisionType.ID = EnumDNET.enumRevType.RT Then
                                AFileDataList.Add(revisionFaktur)
                            End If
                        End If
                    End If
                End If
            End If
        Next

        If mess.Length > 0 Then
            MessageBox.Show(mess)
            Exit Sub
        End If

        If (BFileDataList.Count = 0 AndAlso AFileDataList.Count = 0) Then
            MessageBox.Show("Tidak ada Data Faktur dengan Status Proses")
            Exit Sub
        End If

        If BFileDataList.Count > 0 Then
            ' create csfile txt, only for type B
            TransferChasisMasterProfile(BFileDataList)
            ' create fkfile txt for type B
            Transfer("B", BFileDataList, strMsg)
        End If

        If AFileDataList.Count > 0 Then
            ' create fkfile txt for type A
            Transfer("A", AFileDataList, strMsg)
        End If

        If strMsg = String.Empty Then
            MessageBox.Show("Retransfer data berhasil")
        End If

        If strMsg <> "" Then MessageBox.Show(strMsg)
    End Sub

    Private Sub btnTransfertoSAP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransfertoSAP.Click
        Dim alRFC As New ArrayList
        Dim strMsg As String = ""

        If Not IsNothing(sessHelp.GetSession("arRFC")) Then
            alRFC = CType(sessHelp.GetSession("arRFC"), ArrayList)
        End If

        If alRFC.Count > 0 Then TransferFakturs(alRFC, strMsg) 'paid via RFC

        If strMsg <> "" Then MessageBox.Show(strMsg)
    End Sub

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Dim arrData As New ArrayList
        Dim crits As CriteriaComposite
        If dgInvoiceRevisionList.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        If Not IsNothing(sessHelp.GetSession("criteriadownload")) Then
            crits = CType(sessHelp.GetSession("criteriadownload"), CriteriaComposite)

            Dim sortColl As SortCollection = New SortCollection

            sortColl.Add(New Sort(GetType(RevisionFaktur), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))  '-- Nomor chassis
            arrData = New RevisionFakturFacade(User).RetrieveByCriteria(crits, sortColl)
        End If

        If arrData.Count > 0 Then
            DoDownload(arrData)
        End If
    End Sub

    Private Sub btnSaveRemark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveRemark.Click
        Dim InvoiceList As New ArrayList  '-- List of revision faktur selected
        For Each item As DataGridItem In dgInvoiceRevisionList.Items
            If CType(item.FindControl("chkSelect"), CheckBox).Checked Then
                Dim id As Integer = item.Cells(0).Text  '-- ID column
                Dim revFaktur As RevisionFaktur = New RevisionFakturFacade(User).Retrieve(id)  '-- Retrieve this revision faktur based on ID
                If revFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Validasi Then
                    revFaktur.Remark = User.Identity.Name & Chr(1) & DateTime.Now.ToString() & Chr(1) & txtRemark.Text.Trim
                    InvoiceList.Add(revFaktur)  '-- Add to list of revision faktur
                End If
            End If
        Next
        If InvoiceList.Count > 0 Then
            Dim refFakturFac As New RevisionFakturFacade(User)
            refFakturFac.UpdateTransaction(InvoiceList)  '-- Update list of revision faktur selected
            ReadData()  '-- Re-read all data to refresh changes
            BindPage(dgInvoiceRevisionList.CurrentPageIndex) '-- Re-bind current page
            MessageBox.Show("Ubah Remark berhasil")
        Else
            MessageBox.Show("Remark hanya boleh di lakukan untuk revisi status validasi.")
        End If
    End Sub
#End Region

#Region "Custom Method"
    Private Sub EnablingRemark()
        btnSaveRemark.Visible = True
        lblRemark.Visible = True
        txtRemark.Visible = True
        pnlRemark.Visible = True
    End Sub

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.RevisiFakturDaftarLihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=REVISI FAKTUR - Daftar Revisi Faktur")
        End If

        Dim objDealer As Dealer = CType(sessHelp.GetSession("DEALER"), Dealer)

        If (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            Me.ddlKategoriPembayaran.Visible = SecurityProvider.Authorize(Context.User, SR.RevisiFakturDaftarKonfirmasi_Privilege)
            Me.btnProcess.Visible = SecurityProvider.Authorize(Context.User, SR.RevisiFakturDaftarKonfirmasi_Privilege)
            Me.btnConfirm.Visible = SecurityProvider.Authorize(Context.User, SR.RevisiFakturDaftarKonfirmasi_Privilege)

            Me.btnTransfer.Visible = SecurityProvider.Authorize(Context.User, SR.RevisiFakturDaftarTransfer_Privilege)
            Me.btnRetransfer.Visible = SecurityProvider.Authorize(Context.User, SR.RevisiFakturDaftarTransfer_Privilege)
            EnablingRemark()
        Else
            Me.ddlKategoriPembayaran.Visible = False
            Me.btnProcess.Visible = False
            Me.btnConfirm.Visible = False
            Me.btnTransfer.Visible = False
            Me.btnRetransfer.Visible = False
            Me.txtKodeDealer.Visible = False
            Me.lblSearchDealer.Visible = False
            lblDealerCode.Visible = True
            lblDealerCode.Text = objDealer.DealerCode & " / " & objDealer.SearchTerm1
        End If

        _PCAccessAllowed = SecurityProvider.Authorize(Context.User, SR.PKCategoryPC_Privilege)
        '_CVAccessAllowed = SecurityProvider.Authorize(Context.User, SR.PKCategoryCV_Privilege)
        _LCVAccessAllowed = SecurityProvider.Authorize(Context.User, SR.PKCategoryLCV_Privilege)

        If (Not _PCAccessAllowed) And (Not _LCVAccessAllowed) Then
            Me.btnSearch.Visible = False
            Me.ddlCategory.Visible = False
        End If

        _allowEdit = SecurityProvider.Authorize(Context.User, SR.RevisiFakturDaftarEdit_Privilege)
    End Sub

    Private Sub BindDropdownList()
        '-- Category criteria & sort
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim cat As String = ""
        If _PCAccessAllowed Then
            cat = cat & "'PC',"
        End If
        'If _CVAccessAllowed Then
        '    cat = cat & "'CV',"
        'End If
        If _LCVAccessAllowed Then
            cat = cat & "'LCV',"
        End If
        If cat <> "" Then
            cat = "(" & cat.Substring(0, cat.Length - 1) & ")"
            criterias.opAnd(New Criteria(GetType(Category), "CategoryCode", MatchType.InSet, cat))
        End If

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code

        '-- Bind Category dropdownlist
        ddlCategory.DataSource = New CategoryFacade(User).RetrieveByCriteria(criterias, sortColl)
        ddlCategory.DataTextField = "CategoryCode"
        ddlCategory.DataValueField = "CategoryCode"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item

        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)


    End Sub

    Private Sub BindListBoxList()
        lboxStatus.DataSource = New EnumDNET().RetrieveStatusFakturKendaraanRev
        lboxStatus.DataTextField = "NameType"
        lboxStatus.DataValueField = "ValType"
        lboxStatus.DataBind()

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            lboxStatus.Items.Remove(lboxStatus.Items.FindByText("Baru"))
        End If
    End Sub

    Private Sub BindRevisionTypeDropDownList(ByRef objDropDownList As DropDownList)

        Dim objRevisionTypeFacade As RevisionTypeFacade = New RevisionTypeFacade(User)

        objDropDownList.DataSource = objRevisionTypeFacade.RetrieveActiveList()
        objDropDownList.DataTextField = "Description"
        objDropDownList.DataValueField = "ID"
        objDropDownList.DataBind()
        objDropDownList.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Private Sub BindPaymentStatusDropDownList(ByRef objDropDownList As DropDownList)
        objDropDownList.DataSource = EnumStatusRevisionPayment.ArrayListStatus()
        objDropDownList.DataTextField = "text"
        objDropDownList.DataValueField = "value"
        objDropDownList.DataBind()
        objDropDownList.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Private Sub ReadData()
        '-- Read all data selected
        '-- Revision Request date
        If chkRequest.Checked Then
            If icRequestStart.Value > icRequestStart.Value Then
                '-- It must be Start date <= End date
                MessageBox.Show("Interval tanggal pengajuan revisi tidak valid")
                Exit Sub  '-- Directly exits
            Else
                If icRequestEnd.Value.Subtract(icRequestStart.Value).Days > 65 Then
                    '-- The difference must be lesser or equal to 65 days
                    MessageBox.Show("Selisih tanggal pengajuan revisi harus <= 65 hari")
                    Exit Sub  '-- Directly exits
                End If
            End If
        End If


        '-- Validation date
        If chkValidPeriod.Checked Then
            If icStartValid.Value > icEndValid.Value Then
                '-- It must be Start date <= End date
                MessageBox.Show("Interval tanggal validasi tidak valid")
                Exit Sub  '-- Directly exits
            Else
                If icEndValid.Value.Subtract(icStartValid.Value).Days > 65 Then
                    '-- The difference must be lesser or equal to 65 days
                    MessageBox.Show("Selisih tanggal validasi harus <= 65 hari")
                    Exit Sub  '-- Directly exits
                End If
            End If
        End If

        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(RevisionFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '-- RegNumber
        If txtNoRequest.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), "RegNumber", MatchType.[Partial], txtNoRequest.Text.Trim()))
        End If

        '-- Nomor Revisi faktur
        If txtRevisionFakturNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), "EndCustomer.FakturNumber", MatchType.[Partial], txtRevisionFakturNo.Text.Trim()))
        End If

        '-- Revisi Type
        If ddlRevisionType.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), "RevisionType.ID", MatchType.Exact, ddlRevisionType.SelectedValue))
        End If

        '-- Revision Status
        If lboxStatus.SelectedIndex <> -1 Then
            Dim SelectedStatus As String = GetSelectedItem(lboxStatus)
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), "RevisionStatus", MatchType.InSet, "(" & SelectedStatus & ")"))
        Else
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), "RevisionStatus", MatchType.InSet, "('0','1','2','3','4')"))
        End If

        '-- Revisi Pembayaran
        If ddlPembayaran.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), "IsPay", MatchType.Exact, ddlPembayaran.SelectedValue))
        End If

        '-- is temporary
        If ddlIsTemporary.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), "EndCustomer.IsTemporary", MatchType.Exact, ddlIsTemporary.SelectedValue))
        End If

        '-- payment status
        If ddlPaymentStatus.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), "RevisionPaymentDetail.RevisionPaymentHeader.Status", MatchType.Exact, ddlPaymentStatus.SelectedValue))
        End If

        If chkRequest.Checked Then
            '-- Periode Pengajuan Revisi
            Dim StartOpenFaktur As New DateTime(CInt(icRequestStart.Value.Year), CInt(icRequestStart.Value.Month), CInt(icRequestStart.Value.Day), 0, 0, 0)
            Dim EndOpenFaktur As New DateTime(CInt(icRequestEnd.Value.Year), CInt(icRequestEnd.Value.Month), CInt(icRequestEnd.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), "CreatedTime", MatchType.GreaterOrEqual, Format(StartOpenFaktur, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), "CreatedTime", MatchType.LesserOrEqual, Format(EndOpenFaktur, "yyyy-MM-dd HH:mm:ss")))

            Dim Time As TimeSpan = EndOpenFaktur.Subtract(StartOpenFaktur)
            If Time.Days > 65 Then
                MessageBox.Show("Periode Pencarian Melebihi 65 Hari")
                Exit Sub
            End If

        End If

        If chkValidPeriod.Checked Then
            '-- Periode Validasi
            Dim StartValid As New DateTime(CInt(icStartValid.Value.Year), CInt(icStartValid.Value.Month), CInt(icStartValid.Value.Day), 0, 0, 0)
            Dim EndValid As New DateTime(CInt(icEndValid.Value.Year), CInt(icEndValid.Value.Month), CInt(icEndValid.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), "NewValidationDate", MatchType.GreaterOrEqual, Format(StartValid, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), "NewValidationDate", MatchType.LesserOrEqual, Format(EndValid, "yyyy-MM-dd HH:mm:ss")))
            Dim Time As TimeSpan = EndValid.Subtract(StartValid)
            If Time.Days > 65 Then
                MessageBox.Show("Periode Pencarian Melebihi 65 Hari")
                Exit Sub
            End If
        End If

        '-- Dealer code
        If txtKodeDealer.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), "ChassisMaster.Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        End If

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), "RevisionStatus", MatchType.No, CType(EnumDNET.enumFakturKendaraanRev.Baru, Integer)))
        ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), "ChassisMaster.Dealer.ID", MatchType.Exact, objDealer.ID))
        End If

        '-- Nomor chassis
        If txtChassisNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), "ChassisMaster.ChassisNumber", MatchType.[Partial], txtChassisNo.Text.Trim()))
        End If

        '-- Category
        If ddlCategory.SelectedValue <> "" Then
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), "ChassisMaster.Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))
        End If

        If ddlCategory.SelectedValue <> "" And ddlSubCategory.SelectedValue <> "-1" Then
            'Dim Sql As String = ""
            'Dim oEVSC As EnumVehicleSubCategory = New EnumVehicleSubCategory
            'Dim sVals As String = oEVSC.GetSQLValue(ddlCategory.SelectedItem.Text, ddlSubCategory.SelectedValue)

            'Sql &= "  SELECT  vc.[ID] FROM    dbo.VechileType vt INNER JOIN dbo.[VechileColor] vc ON [vc].[VechileTypeID] = [vt].[ID] WHERE   vt.[RowStatus] = 0 AND vc.[RowStatus] = 0 "
            'Dim i As Integer
            'For i = 0 To sVals.Split(";").Length - 1
            '    If i = 0 Then
            '        Sql &= " and (vt.Description like '" & sVals.Split(";")(i) & "' "
            '        If sVals.Split(";").Length = 1 Then Sql &= ")"
            '    ElseIf i = sVals.Split(";").Length - 1 Then
            '        Sql &= " or vt.Description like '" & sVals.Split(";")(i) & "') "
            '    Else
            '        Sql &= " or vt.Description like '" & sVals.Split(";")(i) & "'"
            '    End If
            'Next
            'criterias.opAnd(New Criteria(GetType(RevisionFaktur), "ChassisMaster.VechileColor.ID", MatchType.InSet, "(" & Sql & ")"))

            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlSubCategory.SelectedValue
            Dim strSql2 As String = "select distinct a.ID from VechileColor a join VechileType b on a.VechileTypeID = b.ID and b.RowStatus = 0 "
            strSql2 += " join VechileModel c on b.ModelID = c.ID and c.RowStatus = 0 where a.RowStatus = 0 and c.ID in (" & strSql & ")"
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), "ChassisMaster.VechileColor.ID", MatchType.InSet, "(" & strSql2 & ")"))
        End If

        If Not chkRequest.Checked AndAlso Not chkValidPeriod.Checked Then

            If (txtNoRequest.Text = "" AndAlso txtChassisNo.Text = "" AndAlso txtRevisionFakturNo.Text = "") Then
                MessageBox.Show("Silahkan Masukan Periode pencarian dibawah 65 Hari")
                Exit Sub
                '' max -65 days
                'criterias.opAnd(New Criteria(GetType(RevisionFaktur), "CreatedTime", MatchType.GreaterOrEqual, Now.AddDays(-65)))

            End If

        End If

        If ddlMatching.SelectedValue <> "" Then



            If ddlMatching.SelectedValue = "1" Then
                criterias.opAnd(New Criteria(GetType(RevisionFaktur), "ID", MatchType.InSet, "(SELECT ID FROM [dbo].[fn_RevisionMatching]() )"))
            Else
                criterias.opAnd(New Criteria(GetType(RevisionFaktur), "ID", MatchType.NotInSet, "(SELECT ID FROM [dbo].[fn_RevisionMatching]() )"))
            End If
        End If


        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        'oCM.EndCustomerRev.ValidateBy 
        sortColl.Add(New Sort(GetType(RevisionFaktur), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))  '-- Nomor chassis

        '-- Retrieve recordset
        Dim InvoiceRevisionList As ArrayList = New RevisionFakturFacade(User).RetrieveByCriteria(criterias, sortColl)

        sessHelp.SetSession("InvoiceRevisionList", InvoiceRevisionList)
        sessHelp.SetSession("criteriadownload", criterias)

        If InvoiceRevisionList.Count > 0 Then
            '-- Enable all buttons if any record exists
            btnProcess.Enabled = True
            btnTransfer.Enabled = True
            btnConfirm.Enabled = True
            btnRetransfer.Enabled = True
        Else
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
            btnProcess.Enabled = False
            btnConfirm.Enabled = False
            btnTransfer.Enabled = False
            btnRetransfer.Enabled = False
        End If
    End Sub

    Private Function GetSelectedItem(ByVal listboxStatus As ListBox) As String
        '-- Items selected in listbox

        Dim _strStatus As String = String.Empty
        For Each item As ListItem In listboxStatus.Items
            If item.Selected Then
                If _strStatus = String.Empty Then
                    _strStatus = item.Value
                Else
                    _strStatus = _strStatus & "," & item.Value
                End If
            End If
        Next
        Return _strStatus
    End Function

    Private Sub BindPage(ByVal pageIndex As Integer)
        Dim InvoiceRevisionList As ArrayList = CType(sessHelp.GetSession("InvoiceRevisionList"), ArrayList)
        If InvoiceRevisionList.Count <> 0 Then
            Dim PagedList As ArrayList = ArrayListPager.DoPage(InvoiceRevisionList, pageIndex, dgInvoiceRevisionList.PageSize)
            dgInvoiceRevisionList.DataSource = PagedList
            dgInvoiceRevisionList.VirtualItemCount = InvoiceRevisionList.Count()
            dgInvoiceRevisionList.DataBind()
        Else
            dgInvoiceRevisionList.DataSource = New ArrayList
            dgInvoiceRevisionList.VirtualItemCount = 0
            dgInvoiceRevisionList.CurrentPageIndex = 0
            dgInvoiceRevisionList.DataBind()
        End If
        If dgInvoiceRevisionList.VirtualItemCount > 0 Then
            lblJumRecord.Text = "Jumlah record : " & dgInvoiceRevisionList.VirtualItemCount
        End If
    End Sub

    Public Function GetRevisionStatusName(ByVal intID As Integer) As String
        If intID > -1 Then
            Return ([Enum].Parse(GetType(EnumDNET.enumFakturKendaraanRev), intID, True)).ToString
        Else
            Return String.Empty
        End If

    End Function

    Public Function GetPaymentRevisionStatusName(ByVal intID As Integer) As String
        If intID > -1 Then
            Return ([Enum].Parse(GetType(EnumStatusRevisionPayment.Status), intID, True)).ToString
        Else
            Return String.Empty
        End If

    End Function

    Public Function GetRevisionOpsiPayment(ByVal intID As Integer) As String
        If intID > -1 Then
            Return ([Enum].Parse(GetType(EnumDNET.enumPaymentOption), intID, True)).ToString
        Else
            Return String.Empty
        End If
    End Function

    Private Sub storeCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("Dealer", txtKodeDealer.Text)
        crit.Add("RegNumber", txtNoRequest.Text)
        crit.Add("RevisionFakturNo", txtRevisionFakturNo.Text)
        crit.Add("ChassisNumber", txtChassisNo.Text)
        crit.Add("Validasi", lboxStatus.Items(0).Selected)
        crit.Add("Konfirmasi", lboxStatus.Items(1).Selected)
        crit.Add("Proses", lboxStatus.Items(2).Selected)
        crit.Add("Selesai", lboxStatus.Items(3).Selected)
        crit.Add("Category", ddlCategory.SelectedValue)
        crit.Add("ddlSubCategory", ddlSubCategory.SelectedValue)
        crit.Add("Type", ddlRevisionType.SelectedValue)
        crit.Add("IsPay", ddlPembayaran.SelectedValue)
        crit.Add("IsTemporary", ddlIsTemporary.SelectedValue)
        crit.Add("PaymentStatus", ddlPaymentStatus.SelectedValue)
        crit.Add("chkRequest", chkRequest.Checked)
        crit.Add("icRequestStart", icRequestStart.Value)
        crit.Add("icRequestEnd", icRequestEnd.Value)
        crit.Add("chkValidPeriod", chkValidPeriod.Checked)
        crit.Add("StartValid", icStartValid.Value)
        crit.Add("EndValid", icEndValid.Value)
        crit.Add("PageIndex", dgInvoiceRevisionList.CurrentPageIndex)
        crit.Add("ddlMatching", ddlMatching.SelectedValue)
        sessHelp.SetSession("CriteriaFormInvoiceRevisionList", crit)
    End Sub

    Private Function TransferFakturs(al As ArrayList, ByRef strMsg As String)
        If al.Count > 0 Then
            If Not IsNothing(al) AndAlso al.Count > 0 Then
                sessHelp.SetSession("arRFC", al)
            End If

            If Me.txtPass.Text = String.Empty Then
                RegisterStartupScript("OpenWindow", "<script>InputPasswordPlease();</script>")
                Return False
            End If

            If Not IsNothing(sessHelp.GetSession("arRFC")) AndAlso (IsNothing(al) OrElse al.Count = 0) Then
                al = CType(sessHelp.GetSession("arRFC"), ArrayList)
            End If
            sessHelp.SetSession("arRFC", Nothing)

            Dim UserName As String
            Dim Password As String
            Dim sapConStr As String = KTB.DNet.Lib.WebConfig.GetValue("SAPConnectionStringEmpty") ' User "SAPConnectionString" and prompt user to enter password first
            Dim oSAPDnet As SAPDNet
            Dim SONumber As String = "", SAPStatus As String = "", Msg As String = ""
            Dim aErrors As New ArrayList
            Dim oUI As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
            Dim objFakturRev As New RevisionFaktur
            Dim objRevSAPDoc As New RevisionSAPDoc

            Try
                UserName = Me.txtUser.Text
                Password = Me.txtPass.Text
                oSAPDnet = New SAPDNet(sapConStr, UserName, Password)
                For i As Integer = 0 To al.Count - 1
                    objFakturRev = CType(al(i), RevisionFaktur)

                    SONumber = ""
                    Msg = ""

                    oSAPDnet.SendFakturRevViaRFC(objFakturRev, SONumber, Msg, objRevSAPDoc)
                    If SONumber.Trim = String.Empty Then
                        aErrors.Add("Error PO : " & objFakturRev.RegNumber & ". " & Msg)
                    Else
                        If Not IsNothing(objRevSAPDoc) Then
                            Dim objFac As RevisionSAPDocFacade = New RevisionSAPDocFacade(User)
                            objRevSAPDoc.RowStatus = 0
                            objRevSAPDoc.CreatedBy = User.Identity.Name

                            Dim existingRevSAPDoc As RevisionSAPDoc = Nothing
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionSAPDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(RevisionSAPDoc), "RevisionFaktur.ID", MatchType.Exact, objFakturRev.ID))

                            Dim resultDataSAPDocs As ArrayList = objFac.Retrieve(criterias)
                            If Not IsNothing(resultDataSAPDocs) AndAlso resultDataSAPDocs.Count > 0 Then
                                existingRevSAPDoc = CType(resultDataSAPDocs(0), RevisionSAPDoc)
                            End If

                            If Not IsNothing(existingRevSAPDoc) Then
                                existingRevSAPDoc.DCAmount = objRevSAPDoc.DCAmount
                                existingRevSAPDoc.DebitChargeNo = objRevSAPDoc.DebitChargeNo
                                existingRevSAPDoc.DebitMemoNo = objRevSAPDoc.DebitMemoNo
                                existingRevSAPDoc.DMAmount = objRevSAPDoc.DMAmount
                                existingRevSAPDoc.LastUpdateTime = Now
                                existingRevSAPDoc.LastUpdateBy = User.Identity.Name

                                objFac.Update(existingRevSAPDoc)
                            Else
                                objFac.Insert(objRevSAPDoc)
                            End If


                            strMsg = strMsg & "Transfer Faktur " & objFakturRev.RegNumber & " Berhasil.\n"
                        End If
                    End If
                Next
                If aErrors.Count > 0 Then
                    Msg = ""
                    For Each erm As String In aErrors
                        Msg = Msg & erm & ";"
                    Next
                    MessageBox.Show("Transfer Gagal. " & Msg)
                    Return False
                Else
                    MessageBox.Show("Transfer Berhasil.")
                    Return True
                End If
            Catch ex As Exception
                MessageBox.Show("Transfer Gagal. " & ex.Message)
                Return False
            End Try
            'Else
            '    Dim _fileHelper As New FileHelper
            '    Dim str As FileInfo
            '    Try
            '        str = _fileHelper.TransferPOtoSAP(al)
            '        strMsg = strMsg & SR.UploadSucces(str.Name)
            '        Return True
            '    Catch ex As Exception
            '        strMsg = strMsg & SR.UploadFail(str.Name)
            '        Return False
            '    End Try

        End If

        If strMsg <> String.Empty Then
            MessageBox.Show(strMsg)
        End If
    End Function

    Private Sub TransferChasisMasterProfile(ByVal listFaktur As ArrayList)
        Dim filename = String.Format("{0}{1}{2}", "csprof", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")
        Dim oUI As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\FinishUnit\IR\" & oUI.UserName & "\" & filename   '-- Destination file

        Dim success As Boolean = False
        Dim sw As StreamWriter
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim finfo As New FileInfo(DestFile)
        Try
            success = imp.Start()
            If success Then
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If
                If finfo.Exists Then
                    finfo.Delete()
                End If
                Dim fs As FileStream = New FileStream(DestFile, FileMode.CreateNew)
                sw = New StreamWriter(fs)
                DnLoadInvoiceDataChasisProfile(sw, listFaktur)
                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            sw.Close()
            Throw ex
        End Try
    End Sub

    Private Sub DnLoadInvoiceDataChasisProfile(ByRef sw As StreamWriter, ByVal InvoiceResList As ArrayList)
        Dim tab As Char  '-- Separator character <Tab>
        Dim InvoiceLine As StringBuilder = New StringBuilder  '-- Invoice line in text file
        Dim temp As String = String.Empty
        tab = Chr(9)
        For Each objInvoice As RevisionFaktur In InvoiceResList
            Dim revisionChasisMasterProfileList As ArrayList = New RevisionChassisMasterProfileFacade(User).GetRevisionChassisMasterProfileByChassisEndCustomer(objInvoice.ChassisMaster.ID, objInvoice.EndCustomer.ID)

            Dim profiles As ArrayList
            Dim groupId As ArrayList

            groupId = New System.Collections.ArrayList((From item As RevisionChassisMasterProfile In revisionChasisMasterProfileList.OfType(Of RevisionChassisMasterProfile)()
                                Select item.ProfileGroup.ID Distinct).ToList())

            If (groupId.Count > 1) Then
                profiles = New System.Collections.ArrayList((From item As RevisionChassisMasterProfile In revisionChasisMasterProfileList.OfType(Of RevisionChassisMasterProfile)()
                                Where item.ProfileGroup.Code = "cust_prf_" + item.ChassisMaster.Category.CategoryCode.ToLower()
                                Select item).ToList())
            Else
                profiles = revisionChasisMasterProfileList
            End If

            For Each objChassisMasterProfileRev As RevisionChassisMasterProfile In profiles
                If objChassisMasterProfileRev.ProfileHeader.Status = CInt(EnumStatusProfile.StatusMode.Active) AndAlso objChassisMasterProfileRev.ProfileValue.Trim <> "" AndAlso (Not IsDBNull(objChassisMasterProfileRev.ProfileValue)) Then
                    InvoiceLine.Append(objInvoice.ChassisMaster.ChassisNumber + tab)
                    InvoiceLine.Append(objChassisMasterProfileRev.ProfileHeader.Code + tab)
                    temp = objChassisMasterProfileRev.ProfileValue.Trim
                    InvoiceLine.Append(temp.Trim)
                    InvoiceLine.Append(vbNewLine)
                    temp = String.Empty
                End If
            Next
        Next
        sw.WriteLine(InvoiceLine.ToString())
    End Sub

    Private Sub Transfer(ByVal FileType As String, al As ArrayList, ByRef strMsg As String)
        Dim dt As DateTime = DateTime.Now
        Dim sSuffix As String = CType(dt.Year, String) & CType(dt.Month, String) & _
                                CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & _
                                CType(dt.Second, String) & CType(dt.Millisecond, String)

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Try
            If imp.Start() Then

                If FileType = "B" Then
                    For i As Integer = 1 To 3
                        Dim typeID As Integer
                        If i = 3 Then
                            typeID = i + 1
                        Else
                            typeID = i
                        End If

                        Dim dataList As ArrayList = New ArrayList
                        dataList = New System.Collections.ArrayList((From item As RevisionFaktur In al.OfType(Of RevisionFaktur)()
                                    Where item.RevisionType.ID = typeID
                                    Select item).ToList())
                        If dataList.Count > 0 Then
                            WriteInvoiceDataB(sSuffix, dataList)
                        End If
                    Next
                ElseIf FileType = "A" Then
                    Dim strFileName As String
                    strFileName = "fkrev" & CType(al(0), RevisionFaktur).RevisionType.RevisionCode.ToLower

                    Dim InvoiceData As String = Server.MapPath("") & "\..\DataTemp\" & strFileName & sSuffix & ".txt"

                    Dim finfo As FileInfo = New FileInfo(InvoiceData)
                    If finfo.Exists Then
                        finfo.Delete()  '-- Delete temp file if exists
                    End If
                    Dim fs As FileStream = New FileStream(InvoiceData, FileMode.CreateNew)
                    Dim sw As StreamWriter = New StreamWriter(fs)

                    WriteInvoiceDataA(sw, al)

                    sw.Close()
                    fs.Close()

                    Dim oUI As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
                    Dim DestFolder As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\FinishUnit\IR\" & oUI.UserName
                    If Not IO.Directory.Exists(DestFolder) Then
                        IO.Directory.CreateDirectory(DestFolder)
                    End If
                    Dim DestFile As String = DestFolder & "\" & strFileName & sSuffix & ".txt"
                    Dim finfo2 As FileInfo = New FileInfo(InvoiceData)
                    finfo2.CopyTo(DestFile, True)
                End If

                imp.StopImpersonate()
                imp = Nothing
            End If
            ReadData()  '-- Read all data matching criteria
            BindPage(dgInvoiceRevisionList.CurrentPageIndex) '-- Re-bind current page

        Catch ex As Exception
            strMsg = strMsg & "Transfer Faktur Tidak Berbayar gagal\n"
        End Try

    End Sub

    Private Sub WriteInvoiceDataA(ByRef sw As StreamWriter, ByVal InvList As ArrayList)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim InvoiceLine As StringBuilder = New StringBuilder  '-- Invoice line in text file

        For Each objInvoice As RevisionFaktur In InvList

            InvoiceLine.Remove(0, InvoiceLine.Length)  '-- Empty Invoice line
            InvoiceLine.Append(objInvoice.ChassisMaster.ChassisNumber.Replace(tab, " ") & tab) '-- Chassis number
            If Not IsNothing(objInvoice.EndCustomer) Then
                InvoiceLine.Append(Format(objInvoice.EndCustomer.FakturDate, "ddMMyyyy") & tab)  '-- Faktur date

                InvoiceLine.Append(objInvoice.RegNumber & tab)

                InvoiceLine.Append(objInvoice.RevisionType.RevisionCode & tab)

                If objInvoice.IsPay = EnumDNET.enumPaymentOption.Bayar Then
                    Dim revPaymentDetail As RevisionPaymentDetail = CType(objInvoice.RevisionPaymentDetails(0), RevisionPaymentDetail)

                    Select Case revPaymentDetail.RevisionPaymentHeader.PaymentType
                        Case enumPaymentTypeRevision.PaymentType.Transfer
                            InvoiceLine.Append("T" & tab)
                        Case enumPaymentTypeRevision.PaymentType.Gyro
                            InvoiceLine.Append("G" & tab)
                        Case enumPaymentTypeRevision.PaymentType.Virtual_Account
                            InvoiceLine.Append("V" & tab)
                    End Select

                    InvoiceLine.Append(Math.Round(revPaymentDetail.RevisionPaymentHeader.TotalAmount, MidpointRounding.AwayFromZero) & tab) '--  amount

                    Dim slipNumber As String = revPaymentDetail.RevisionPaymentHeader.SlipNumber
                    Dim bankCode As String = slipNumber.Split(" ")(0)
                    If revPaymentDetail.RevisionPaymentHeader.PaymentType = enumPaymentTypeRevision.PaymentType.Transfer Then
                        InvoiceLine.Append(bankCode & tab) '--  bank name
                        InvoiceLine.Append("" & tab)  '--  giro#
                    ElseIf revPaymentDetail.RevisionPaymentHeader.PaymentType = enumPaymentTypeRevision.PaymentType.Gyro Then
                        Dim gyro As String = slipNumber.Substring(slipNumber.IndexOf(" ") + 1)
                        InvoiceLine.Append(bankCode & tab)  '--  bank name
                        InvoiceLine.Append(gyro & tab)  '--  giro#
                    ElseIf revPaymentDetail.RevisionPaymentHeader.PaymentType = enumPaymentTypeRevision.PaymentType.Virtual_Account Then
                        InvoiceLine.Append("" & tab)  '--  bank name
                        InvoiceLine.Append("" & tab)  '--  giro#
                    End If

                    InvoiceLine.Append(revPaymentDetail.RevisionPaymentHeader.AccDocNumber & tab) '--  TR No#
                    InvoiceLine.Append(revPaymentDetail.RevisionPaymentHeader.RegNumber & tab) '--  Payment RegNumber
                Else
                    InvoiceLine.Append("" & tab)
                    InvoiceLine.Append(0 & tab) '--  amount
                    InvoiceLine.Append("" & tab)  '--  bank name
                    InvoiceLine.Append("" & tab)  '--  giro#
                    InvoiceLine.Append("" & tab) '--  TR No#
                    InvoiceLine.Append("" & tab) '--  Payment RegNumber
                End If

            End If
            sw.WriteLine(InvoiceLine.ToString())  '-- Write Invoice line
        Next
    End Sub

    Private Sub WriteInvoiceDataB(ByRef sSuffix As String, ByVal InvoiceList As ArrayList)
        Dim strFileName As String
        strFileName = "fkrev" & CType(InvoiceList(0), RevisionFaktur).RevisionType.RevisionCode.ToLower

        Dim InvoiceData As String = Server.MapPath("") & "\..\DataTemp\" & strFileName & sSuffix & ".txt"

        Dim finfo As FileInfo = New FileInfo(InvoiceData)
        If finfo.Exists Then
            finfo.Delete()  '-- Delete temp file if exists
        End If
        Dim fs As FileStream = New FileStream(InvoiceData, FileMode.CreateNew)
        Dim sw As StreamWriter = New StreamWriter(fs)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim InvoiceLine As StringBuilder = New StringBuilder  '-- Invoice line in text file
        For Each objInvoice As RevisionFaktur In InvoiceList

            InvoiceLine.Remove(0, InvoiceLine.Length)  '-- Empty Invoice line
            InvoiceLine.Append(objInvoice.ChassisMaster.ChassisNumber.Replace(tab, " ") & tab) '-- Chassis number
            If Not IsNothing(objInvoice.EndCustomer) Then
                InvoiceLine.Append(Format(objInvoice.EndCustomer.FakturDate, "ddMMyyyy") & tab)  '-- Faktur date
                Dim objRefChassisMaster As ChassisMaster
                objRefChassisMaster = New ChassisMasterFacade(User).Retrieve(objInvoice.EndCustomer.RefChassisNumberID)
                If objRefChassisMaster Is Nothing Then
                    InvoiceLine.Append(tab)  '-- Empty column
                Else
                    InvoiceLine.Append(objRefChassisMaster.ChassisNumber.Replace(tab, " ") & tab)  '-- Ref chassis number
                End If
                ''Change Status
                'InvoiceLine.Append(" " & tab)   '-- Code

                InvoiceLine.Append(objInvoice.EndCustomer.Customer.Code.Replace(tab, " ") & tab)   '-- Code
                If UCase(objInvoice.EndCustomer.AreaViolationFlag) = "X" Then
                    Dim objAreaVioPayMethFacade As PaymentMethodFacade = New PaymentMethodFacade(User)
                    Dim objAreaVioPatMeth As PaymentMethod = objAreaVioPayMethFacade.Retrieve(objInvoice.EndCustomer.AreaViolationPaymentMethodID)
                    InvoiceLine.Append(objAreaVioPatMeth.Code.Replace(tab, " ") & tab)   '-- Wilayah TOP
                    InvoiceLine.Append(Format(objInvoice.EndCustomer.AreaViolationyAmount, "0") & tab)  '-- Wilayah amount
                    InvoiceLine.Append(objInvoice.EndCustomer.AreaViolationBankName.Replace(tab, " ") & tab)  '-- Wilayah bank name
                    InvoiceLine.Append(objInvoice.EndCustomer.AreaViolationGyroNumber.Replace(tab, " ") & tab)  '-- Wilayah giro#
                Else
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                End If
                If UCase(objInvoice.EndCustomer.PenaltyFlag) = "X" Then
                    Dim objPenaltyPayMethFacade As PaymentMethodFacade = New PaymentMethodFacade(User)
                    Dim objPenaltyPatMeth As PaymentMethod = objPenaltyPayMethFacade.Retrieve(objInvoice.EndCustomer.PenaltyPaymentMethodID)
                    InvoiceLine.Append(objPenaltyPatMeth.Code.Replace(tab, " ") & tab)  '-- Disc TOP
                    InvoiceLine.Append(Format(objInvoice.EndCustomer.PenaltyAmount, "0") & tab)  '-- Disc amount
                    InvoiceLine.Append(objInvoice.EndCustomer.PenaltyBankName.Replace(tab, " ") & tab)  '-- Disc bank name
                    InvoiceLine.Append(objInvoice.EndCustomer.PenaltyGyroNumber.Replace(tab, " ") & tab)  '-- Disc giro#
                Else
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                End If

                If UCase(objInvoice.EndCustomer.ReferenceLetterFlag) = "X" Then
                    InvoiceLine.Append(objInvoice.EndCustomer.ReferenceLetter.Replace(tab, " ") & tab)  '-- Letter
                Else
                    InvoiceLine.Append(tab)  '-- Empty column
                End If
                InvoiceLine.Append(IIf(objInvoice.EndCustomer.SaveBy <> "", UserInfo.Convert(objInvoice.EndCustomer.SaveBy.Replace(tab, " ")), "") & tab)  '-- Dibuat oleh
                InvoiceLine.Append(Format(objInvoice.EndCustomer.SaveTime, "ddMMyyyy") & tab)  '-- Tgl dibuat
                InvoiceLine.Append(IIf(objInvoice.EndCustomer.ValidateBy <> "", UserInfo.Convert(objInvoice.EndCustomer.ValidateBy.Replace(tab, " ")), "") & tab)  '-- Divalidasi oleh
                InvoiceLine.Append(Format(objInvoice.EndCustomer.ValidateTime, "ddMMyyyy") & tab)

                If Not IsNothing(objInvoice.EndCustomer.Customer) Then
                    InvoiceLine.Append(objInvoice.EndCustomer.Customer.PrintRegion & tab)
                Else
                    InvoiceLine.Append("" & tab)
                End If

                'Start  :Add MCP Number 
                If Not IsNothing(objInvoice.EndCustomer.LKPPHeader) Then
                    'If Not IsNothing(objInvoice.EndCustomer.MCPHeader) Then
                    'InvoiceLine.Append(objInvoice.EndCustomer.MCPHeader.ReferenceNumber & tab)
                    'InvoiceLine.Append(objInvoice.EndCustomer.MCPHeader.LetterDate.ToString("ddMMyyyy") & tab)
                    InvoiceLine.Append(objInvoice.EndCustomer.LKPPHeader.ReferenceNumber & tab)
                    InvoiceLine.Append(objInvoice.EndCustomer.LKPPHeader.LetterDate.ToString("ddMMyyyy") & tab)
                Else
                    InvoiceLine.Append("" & tab)
                    InvoiceLine.Append("" & tab)
                End If

                '--Inv Rev number
                InvoiceLine.Append(objInvoice.RegNumber & tab)

                If objInvoice.RevisionType.ID = EnumDNET.enumRevType.RR Then
                    InvoiceLine.Append(objInvoice.RevisionType.RevisionCode & tab)
                Else
                    InvoiceLine.Append(objInvoice.RevisionType.RevisionCode & "-" & objInvoice.ChassisMaster.Category.CategoryCode & tab)
                End If

                If objInvoice.IsPay = EnumDNET.enumPaymentOption.Bayar Then
                    Dim revPaymentDetail As RevisionPaymentDetail = CType(objInvoice.RevisionPaymentDetails(0), RevisionPaymentDetail)

                    Select Case revPaymentDetail.RevisionPaymentHeader.PaymentType
                        Case enumPaymentTypeRevision.PaymentType.Transfer
                            InvoiceLine.Append("T" & tab)
                        Case enumPaymentTypeRevision.PaymentType.Gyro
                            InvoiceLine.Append("G" & tab)
                        Case enumPaymentTypeRevision.PaymentType.Virtual_Account
                            InvoiceLine.Append("V" & tab)
                    End Select

                    InvoiceLine.Append(Math.Round(revPaymentDetail.RevisionPaymentHeader.TotalAmount, MidpointRounding.AwayFromZero) & tab) '--  amount

                    Dim slipNumber As String = revPaymentDetail.RevisionPaymentHeader.SlipNumber
                    Dim bankCode As String = slipNumber.Split(" ")(0)
                    If revPaymentDetail.RevisionPaymentHeader.PaymentType = enumPaymentTypeRevision.PaymentType.Transfer Then
                        InvoiceLine.Append(bankCode & tab) '--  bank name
                        InvoiceLine.Append("" & tab)  '--  giro#
                    ElseIf revPaymentDetail.RevisionPaymentHeader.PaymentType = enumPaymentTypeRevision.PaymentType.Gyro Then
                        Dim gyro As String = slipNumber.Substring(slipNumber.IndexOf(" ") + 1)
                        InvoiceLine.Append(bankCode & tab)  '--  bank name
                        InvoiceLine.Append(gyro & tab)  '--  giro#
                    ElseIf revPaymentDetail.RevisionPaymentHeader.PaymentType = enumPaymentTypeRevision.PaymentType.Virtual_Account Then
                        InvoiceLine.Append("" & tab)  '--  bank name
                        InvoiceLine.Append("" & tab)  '--  giro#
                    End If

                    InvoiceLine.Append(revPaymentDetail.RevisionPaymentHeader.AccDocNumber & tab) '--  TR No#
                    InvoiceLine.Append(revPaymentDetail.RevisionPaymentHeader.RegNumber & tab) '--  Payment RegNumber

                Else
                    InvoiceLine.Append("" & tab)
                    InvoiceLine.Append(0 & tab) '--  amount
                    InvoiceLine.Append("" & tab)  '--  bank name
                    InvoiceLine.Append("" & tab)  '--  giro#
                    InvoiceLine.Append("" & tab) '--  TR No#
                    InvoiceLine.Append("" & tab) '--  RegNumber
                End If

                InvoiceLine.Append(Format(objInvoice.CreatedTime, "ddMMyyyy") & tab)
                InvoiceLine.Append(Format(objInvoice.NewConfirmationDate, "ddMMyyyy"))
            End If
            sw.WriteLine(InvoiceLine.ToString())  '-- Write Invoice line
        Next

        sw.Close()
        fs.Close()

        Dim oUI As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
        Dim DestFolder As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\FinishUnit\IR\" & oUI.UserName
        If Not IO.Directory.Exists(DestFolder) Then
            IO.Directory.CreateDirectory(DestFolder)
        End If
        Dim DestFile As String = DestFolder & "\" & strFileName & sSuffix & ".txt"
        Dim finfo2 As FileInfo = New FileInfo(InvoiceData)
        finfo2.CopyTo(DestFile, True)
    End Sub

    Private Function IsPaid(ByVal revFaktur As RevisionFaktur) As Boolean
        Dim result As Boolean = False

        If Not IsNothing(revFaktur.RevisionPaymentDetails) Then
            If revFaktur.RevisionPaymentDetails.Count > 0 Then
                Dim paymentDetail As RevisionPaymentDetail = New RevisionPaymentDetail
                paymentDetail = CType(revFaktur.RevisionPaymentDetails(0), RevisionPaymentDetail)
                If paymentDetail.RevisionPaymentHeader.Status = EnumStatusRevisionPayment.Status.Selesai Then
                    result = True
                End If
            End If
        End If

        Return result
    End Function

    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        Dim strFileNm As String = "Daftar Revisi Faktur"
        Dim strFileNmHeader As String = "Daftar Revisi Faktur"

        sFileName = strFileNm & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond
        Dim ListData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        Try
            Dim finfo As FileInfo = New FileInfo(ListData)
            If finfo.Exists Then
                finfo.Delete()
            End If

            Dim fs As FileStream = New FileStream(ListData, FileMode.CreateNew)
            Dim sw As StreamWriter = New StreamWriter(fs)

            WriteListData(sw, data, strFileNmHeader)

            sw.Close()
            fs.Close()

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")
        Catch ex As Exception
            Dim dummy As Integer = 0
            MessageBox.Show("Download data gagal. " & ex.Message)
        End Try
    End Sub

    Private Sub WriteListData(ByVal sw As StreamWriter, ByVal data As ArrayList, ByVal fileName As String)
        Dim tab As Char = Chr(9)
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)

        Dim itemLine As StringBuilder = New StringBuilder
        Try
            If Not IsNothing(data) Then
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(fileName)
                sw.WriteLine(itemLine)
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(" " & tab & tab)
                sw.WriteLine(itemLine)
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append("No" & tab)
                itemLine.Append("Kode Dealer" & tab)
                itemLine.Append("Status Revisi Faktur" & tab)
                itemLine.Append("Opsi Pembayaran" & tab)
                itemLine.Append("No Pengajuan Revisi" & tab)
                itemLine.Append("Tipe Revisi" & tab)
                itemLine.Append("Nomor Rangka" & tab)
                itemLine.Append("Tipe/Warna" & tab)
                itemLine.Append("Nomor Faktur" & tab)
                itemLine.Append("Tgl Pengajuan Revisi" & tab)
                itemLine.Append("Tgl Validasi Revisi" & tab)
                itemLine.Append("Tgl Selesai" & tab)
                itemLine.Append("Nama Customer" & tab)
                itemLine.Append("Tgl Faktur" & tab)
                itemLine.Append("Debit Charge" & tab)
                itemLine.Append("Amount" & tab)
                itemLine.Append("Status Pembayaran" & tab)
                itemLine.Append("No Reg Pembayaran" & tab)
                itemLine.Append("Tgl Actual Bayar" & tab)

                sw.WriteLine(itemLine.ToString())

                Dim i As Integer = 1
                For Each item As RevisionFaktur In data
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(i.ToString & tab)
                    If Not item.ChassisMaster.Dealer Is Nothing Then
                        itemLine.Append(item.ChassisMaster.Dealer.DealerCode & tab)
                    Else
                        itemLine.Append(String.Empty & tab)
                    End If
                    itemLine.Append(GetRevisionStatusName(item.RevisionStatus) & tab)
                    itemLine.Append(GetRevisionOpsiPayment(item.IsPay) & tab)
                    itemLine.Append(item.RegNumber & tab)
                    itemLine.Append(item.RevisionType.Description & tab)
                    itemLine.Append(item.ChassisMaster.ChassisNumber & tab)
                    itemLine.Append(item.ChassisMaster.VechileColor.MaterialNumber & tab)
                    itemLine.Append(item.EndCustomer.FakturNumber & tab)
                    itemLine.Append(item.CreatedTime.ToString("dd/MM/yyyy") & tab)
                    itemLine.Append(item.NewConfirmationDate.ToString("dd/MM/yyyy") & tab)
                    itemLine.Append(item.EndCustomer.PrintedTime.ToString("dd/MM/yyyy") & tab)
                    itemLine.Append(item.EndCustomer.Customer.Name1 & tab)
                    itemLine.Append(String.Format(item.EndCustomer.FakturDate, "dd/MM/yyyy") & tab)
                    If Not IsNothing(item.RevisionSAPDoc) Then
                        itemLine.Append(item.RevisionSAPDoc.DebitChargeNo & tab)
                        itemLine.Append(Convert.ToInt32(item.RevisionSAPDoc.DCAmount).ToString() & tab)
                    Else
                        itemLine.Append(String.Empty & tab)
                        itemLine.Append(String.Empty & tab)
                    End If
                    If item.RevisionPaymentDetails.Count > 0 Then
                        itemLine.Append(GetPaymentRevisionStatusName(item.RevisionPaymentDetails(0).RevisionPaymentHeader.Status) & tab)
                        itemLine.Append(item.RevisionPaymentDetails(0).RevisionPaymentHeader.RegNumber.ToString() & tab)
                        itemLine.Append(String.Format(item.RevisionPaymentDetails(0).RevisionPaymentHeader.ActualPaymentDate, "dd/MM/yyyy") & tab)
                    Else
                        itemLine.Append(String.Empty & tab)
                        itemLine.Append(String.Empty & tab)
                        itemLine.Append(String.Empty & tab)
                    End If

                    sw.WriteLine(itemLine.ToString())
                    i = i + 1
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("Download gagal.")
        End Try

    End Sub
#End Region

    Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
    End Sub

End Class