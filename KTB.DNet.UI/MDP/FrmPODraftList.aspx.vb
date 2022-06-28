#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.MDP
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports System.Text
Imports KTB.DNet.SAP
Imports KTB.DNet.BusinessFacade.Transfer
Imports KTB.DNet.BusinessFacade

Imports System.Collections.Generic
Imports System.Linq
#End Region

Public Class FrmPODraftList
    Inherits System.Web.UI.Page

#Region "Custom Variable Declaration"
    Private _ListPO As ArrayList
    Private _ListPODraftHeader As ArrayList
    Private objDealer As Dealer
    Private sessionHelper As New SessionHelper
    Private POSavePrivilege As Boolean
    Private POViewPrivilege As Boolean
    Dim arrPODraftDetail As ArrayList
    Dim arrPODraftHeader As ArrayList
#End Region

#Region "Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not SecurityProvider.Authorize(Context.User, SR.MDP_Daftar_PO_Draft_Display_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar PO Draft")
        End If

        If Not IsPostBack Then
            objDealer = CType(Session("DEALER"), Dealer)
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                txtKodeDealer.Enabled = False
                lblSearchDealer.Visible = False
                txtKodeDealer.Text = objDealer.DealerCode
            Else
                txtKodeDealer.Enabled = True
                lblSearchDealer.Visible = True
            End If

            BindddlStatus()
            BindddlOrderType()
            BindDdlFreePPh()
            BindFactoring()
            BindDdlPaymentType()
            BindDdlPeriode()
            BindToddlCategory()

            btnSubmitPO.Visible = SecurityProvider.Authorize(Context.User, SR.MDP_Daftar_PO_Draft_Edit_Privilege)
            btnSubmitPO.Enabled = False

            If GetSessionCriteria() Then
                BinddtgPO(dtgPO.CurrentPageIndex)
                GetTotalQuantity()
            End If
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
    End Sub

    Protected Sub dtgPO_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgPO.ItemCommand
        Select Case (e.CommandName)
            Case "Detail"
                sessionHelper.SetSession("PrevPage", Request.Url.ToString())
                SetSessionCriteria()
                Response.Redirect("../MDP/CreatePODraft.aspx?id=" & e.Item.Cells(0).Text & "&mode=VIEW")
            Case "Edit"
                sessionHelper.SetSession("PrevPage", Request.Url.ToString())
                SetSessionCriteria()
                Response.Redirect("../MDP/CreatePODraft.aspx?id=" & e.Item.Cells(0).Text & "&mode=EDIT")
        End Select
    End Sub

    Protected Sub dtgPO_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgPO.PageIndexChanged
        dtgPO.CurrentPageIndex = e.NewPageIndex
        BinddtgPO(dtgPO.CurrentPageIndex)
    End Sub

    Protected Sub dtgPO_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgPO.SortCommand
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

        dtgPO.SelectedIndex = -1
        dtgPO.CurrentPageIndex = 0
        BinddtgPO(dtgPO.CurrentPageIndex)
    End Sub

    Protected Sub dtgPO_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgPO.ItemDataBound
        objDealer = CType(Session("DEALER"), Dealer)

        If (e.Item.ItemIndex <> -1) Then
            If e.Item.ItemIndex = 0 Then
                POSavePrivilege = SecurityProvider.Authorize(Context.User, SR.MDP_Daftar_PO_Draft_Edit_Privilege)
                POViewPrivilege = SecurityProvider.Authorize(Context.User, SR.MDP_Daftar_PO_Draft_Display_Privilege)
            End If
            Dim objPODraftHeader As PODraftHeader
            objPODraftHeader = CType(_ListPO(e.Item.ItemIndex), PODraftHeader)
            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dtgPO.PageSize * dtgPO.CurrentPageIndex)).ToString
            e.Item.Cells(2).Text = CType(objPODraftHeader.Status, enumStatusPO.StatusDraftPO).ToString
            Dim regdate As New DateTime(objPODraftHeader.ReqAllocationYear, objPODraftHeader.ReqAllocationMonth, objPODraftHeader.ReqAllocationDate, 0, 0, 0)
            Dim lblKodeDealer As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)
            lblKodeDealer.Text = objPODraftHeader.ContractHeader.Dealer.DealerCode
            lblKodeDealer.ToolTip = objPODraftHeader.ContractHeader.Dealer.SearchTerm1
            e.Item.Cells(4).Text = objPODraftHeader.DraftPONumber
            e.Item.Cells(5).Text = Format(objPODraftHeader.CreatedTime, "dd/MM/yyyy")
            e.Item.Cells(6).Text = Format(objPODraftHeader.ReqAllocationDateTime, "dd/MM/yyyy")
            e.Item.Cells(7).Text = IIf(objPODraftHeader.SubmitPODate = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, Date), "", Format(objPODraftHeader.SubmitPODate, "dd/MM/yyyy"))
            e.Item.Cells(8).Text = objPODraftHeader.ContractHeader.Category.CategoryCode
            e.Item.Cells(9).Text = CType(objPODraftHeader.POType, Lookup.EnumJenisOrder).ToString
            e.Item.Cells(10).Text = objPODraftHeader.TermOfPayment.Description
            'Start  :DailyPO - add info about PPh Status
            If objPODraftHeader.FreePPh22Indicator = "0" Then
                e.Item.Cells(11).Text = "Ya"
            Else
                e.Item.Cells(11).Text = "Tidak"
            End If
            'End    :DailyPO - add info about PPh Status

            e.Item.Cells(12).Text = FormatNumber(objPODraftHeader.TotalHarga, 0, , , TriState.UseDefault)
            e.Item.Cells(13).Text = FormatNumber(objPODraftHeader.TotalHargaPP, 0, , , TriState.UseDefault)
            If objPODraftHeader.Status = CType(enumStatusPO.StatusDraftPO.Batal, Integer) Then
                e.Item.Cells(14).Text = FormatNumber(0, 0, , , TriState.UseDefault)
            Else ': objPODraftHeader.TotalGuarantee()
                e.Item.Cells(14).Text = FormatNumber(objPODraftHeader.TotalHargaIT, 0, , , TriState.UseDefault)
            End If

            e.Item.Cells(15).Text = FormatNumber(objPODraftHeader.TotalHargaLC, 0, , , TriState.UseDefault) '<--tambahan SLA

            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnOperator"), LinkButton)
            If (objPODraftHeader.Status = CType(enumStatusPO.StatusDraftPO.Baru, Integer)) AndAlso (Not objDealer Is Nothing) AndAlso (objPODraftHeader.ContractHeader.Dealer.ID = objDealer.ID) Then
                lbtnEdit.Text = "<img src=""../images/edit.gif"" alt=""Ubah"" border=""0"" style=""cursor:hand"">"
                lbtnEdit.CommandName = "Edit"
                lbtnEdit.Visible = POSavePrivilege
            Else
                lbtnEdit.Visible = POViewPrivilege
            End If

        End If
    End Sub

    Protected Sub ddlPeriodeMDP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPeriodeMDP.SelectedIndexChanged
        BindddlPeriodeTanggalKirim()
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        BinddtgPO(dtgPO.CurrentPageIndex)
        GetTotalQuantity()
    End Sub

    Protected Sub btnBatalCari_Click(sender As Object, e As EventArgs) Handles btnBatalCari.Click
        ClearControl()
    End Sub

    Protected Sub btnSubmitPO_Click(sender As Object, e As EventArgs) Handles btnSubmitPO.Click
        If dtgPO.Items.Count <= 0 Then
            MessageBox.Show("Mohon Search Data Terlebih Dahulu")
            Exit Sub
        End If
        Dim TotalStock As Integer = 0
        Dim TotalPengajuanStock As Integer = 0
        Dim TotalSubmitedStock As Integer = 0

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ContractHeader.Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If txtKodeDealer.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ContractHeader.Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        End If

        'If ddlStatus.SelectedValue <> -1 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "Status", MatchType.Exact, ddlStatus.SelectedItem.Value))
        'End If

        Dim tglDari As DateTime = DateTime.Now
        Dim tglSampai As DateTime = DateTime.Now

        Dim arrTglKirim As Array = ddlPeriodeTanggalKirim.SelectedItem.Value.Split(";")

        tglDari = Convert.ToDateTime(arrTglKirim(0))
        tglSampai = Convert.ToDateTime(arrTglKirim(1))


        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationYear", MatchType.Greater, CType(tglDari.Year.ToString, Short)), "(", True)
        criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationYear", MatchType.Exact, CType(tglDari.Year.ToString, Short)), "(", True)
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationMonth", MatchType.Greater, CType(tglDari.Month.ToString, Short)), ")", False)
        criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationYear", MatchType.Exact, CType(tglDari.Year.ToString, Short)), "(", True)
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationMonth", MatchType.Exact, CType(tglDari.Month.ToString, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationDate", MatchType.GreaterOrEqual, CType(tglDari.Day.ToString, Short)), "))", False)

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationYear", MatchType.Lesser, CType(tglSampai.Year.ToString, Short)), "(", True)
        criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationYear", MatchType.Exact, CType(tglSampai.Year.ToString, Short)), "(", True)
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationMonth", MatchType.Lesser, CType(tglSampai.Month.ToString, Short)), ")", False)
        criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationYear", MatchType.Exact, CType(tglSampai.Year.ToString, Short)), "(", True)
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationMonth", MatchType.Exact, CType(tglSampai.Month.ToString, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationDate", MatchType.LesserOrEqual, CType(tglSampai.Day.ToString, Short)), "))", False)

        Dim _SubmitPODraftHeader As ArrayList = New PODraftHeaderFacade(User).Retrieve(criterias)
        'TotalPengajuanStock = GetTotalQuantity()
        'If IsNothing(_ListPODraftHeader) Then
        '    _ListPODraftHeader = sessionHelper.GetSession("LISTPODRAFT")
        'End If

        If Not IsNothing(_SubmitPODraftHeader) Then
            For Each PODraftHeaderData As PODraftHeader In _SubmitPODraftHeader
                If IsEnableCeilingFilter(PODraftHeaderData) AndAlso Not IsLesserThanAvailableCeiling(PODraftHeaderData) Then
                    Exit Sub
                End If
            Next
        End If

        For Each ListPODraft As PODraftHeader In _SubmitPODraftHeader
            For Each ListPODetail As PODraftDetail In ListPODraft.PODraftDetail
                If ListPODraft.Status = enumStatusPO.StatusDraftPO.Baru Then
                    TotalPengajuanStock += ListPODetail.ReqQty
                ElseIf ListPODraft.Status = enumStatusPO.StatusDraftPO.SubmitPO Then
                    TotalSubmitedStock += ListPODetail.ReqQty
                End If
            Next
        Next



        Dim MDPDailyStockCriteria As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MDPDealerDailyStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        objDealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            MDPDailyStockCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.MDPDealerDailyStock), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If
        If Not String.IsNullOrEmpty(txtKodeDealer.Text) Then
            MDPDailyStockCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.MDPDealerDailyStock), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text.Trim()))
        End If
        'MDPDailyStockCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.MDPDealerDailyStock), "MDPDealerMonthlyStock.VechileColor.ID", MatchType.Exact, objContractDetail.VechileColor.ID))
        MDPDailyStockCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.MDPDealerDailyStock), "PeriodMonth", MatchType.Exact, tglDari.Month))
        MDPDailyStockCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.MDPDealerDailyStock), "PeriodYear", MatchType.Exact, tglDari.Year))
        MDPDailyStockCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.MDPDealerDailyStock), "PeriodeDate", MatchType.GreaterOrEqual, tglDari.Day))
        MDPDailyStockCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.MDPDealerDailyStock), "PeriodeDate", MatchType.LesserOrEqual, tglSampai.Day))

        Dim arrMDPDailyStock As ArrayList = New MDPDealerDailyStockFacade(User).Retrieve(MDPDailyStockCriteria)
        Dim arrPeriodeQuantityStock As New List(Of PeriodAndQuantity)
        arrPeriodeQuantityStock = (From model As MDPDealerDailyStock In arrMDPDailyStock
                      Group By model.PeriodeDate Into Group
                      Select New PeriodAndQuantity With
                      {.PeriodDate = PeriodeDate, .AllocationQuantity = Group.Sum(Function(r) r.AllocationQuantity)}).ToList()

        For Each objPeridodeQtyStock As PeriodAndQuantity In arrPeriodeQuantityStock
            TotalStock += objPeridodeQtyStock.AllocationQuantity
        Next

        If TotalPengajuanStock = 0 Then
            MessageBox.Show("tidak ada PO draft yang disubmit, proses submit PO hanya berlaku untuk PO draft dengan status 'Baru'")
            Return
        Else
            TotalStock = TotalStock - TotalSubmitedStock
        End If

        If TotalStock > TotalPengajuanStock Then
            Dim PODraftDetailCriteria As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                PODraftDetailCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "PODraftHeader.Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
            End If
            PODraftDetailCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "PODraftHeader.ReqAllocationMonth", MatchType.Exact, tglDari.Month))
            PODraftDetailCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "PODraftHeader.ReqAllocationYear", MatchType.Exact, tglDari.Year))
            PODraftDetailCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "PODraftHeader.ReqAllocationDate", MatchType.GreaterOrEqual, tglDari.Day))
            PODraftDetailCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "PODraftHeader.ReqAllocationDate", MatchType.LesserOrEqual, tglSampai.Day))
            PODraftDetailCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "PODraftHeader.Status", MatchType.No, CType(enumStatusPO.StatusDraftPO.Batal, Integer)))

            arrPODraftDetail = New PODraftDetailFacade(User).Retrieve(PODraftDetailCriteria)

            Dim arrPeriodeQuantityPengajuan As New List(Of PeriodAndQuantity)
            arrPeriodeQuantityPengajuan = (From model As PODraftDetail In arrPODraftDetail
                          Group By model.PODraftHeader.ReqAllocationDate Into Group
                          Select New PeriodAndQuantity With
                          {.PeriodDate = ReqAllocationDate, .AllocationQuantity = Group.Sum(Function(r) r.ReqQty)}).ToList()

            Dim arrSisaStock As ArrayList = New ArrayList
            For Each objPeriodQtyStock As PeriodAndQuantity In arrPeriodeQuantityStock
                Dim Sisa As Integer = 0
                Dim str As String = String.Empty
                For Each objPeriodQtyPengajuan As PeriodAndQuantity In arrPeriodeQuantityPengajuan
                    If objPeriodQtyStock.PeriodDate = objPeriodQtyPengajuan.PeriodDate Then
                        Sisa = objPeriodQtyStock.AllocationQuantity - objPeriodQtyPengajuan.AllocationQuantity
                        Exit For
                    Else
                        Sisa = objPeriodQtyStock.AllocationQuantity
                    End If
                Next
                If Sisa > 0 Then
                    str = objPeriodQtyStock.PeriodDate.ToString() + " = " + Sisa.ToString() + " unit"
                    arrSisaStock.Add(str)
                End If
            Next

            Dim MsgSisaUnit As String = String.Empty
            For Each SisaUnit As String In arrSisaStock
                MsgSisaUnit += "tgl " + SisaUnit + "\n"
            Next

            Dim ValidationMsg As String = String.Empty
            ValidationMsg = "Masih terdapat stock MDP yang belum diajukan Draft PO di bulan " + _
                CType(tglDari.Month, Lookup.EnumBulan).ToString() + " :" + "\n" + MsgSisaUnit
            MessageBox.Show(ValidationMsg)
        Else
            Dim POHeaderID As Integer
            Try
                For Each PODraftHeaderData As PODraftHeader In _SubmitPODraftHeader
                    If PODraftHeaderData.Status = CType(enumStatusPO.StatusDraftPO.Baru, Integer) Then
                        'sums = 0
                        Dim objPOHeader As POHeader = New POHeader
                        objPOHeader.Dealer = PODraftHeaderData.Dealer
                        objPOHeader.Status = enumStatusPO.Status.Baru
                        objPOHeader.ContractHeader = PODraftHeaderData.ContractHeader
                        objPOHeader.ReqAllocationDate = PODraftHeaderData.ReqAllocationDate
                        objPOHeader.ReqAllocationMonth = PODraftHeaderData.ReqAllocationMonth
                        objPOHeader.ReqAllocationYear = PODraftHeaderData.ReqAllocationYear
                        objPOHeader.ReqAllocationDateTime = PODraftHeaderData.ReqAllocationDateTime
                        objPOHeader.EffectiveDate = PODraftHeaderData.EffectiveDate
                        objPOHeader.DealerPONumber = PODraftHeaderData.DealerPONumber
                        objPOHeader.TermOfPayment = PODraftHeaderData.TermOfPayment
                        objPOHeader.POType = PODraftHeaderData.POType
                        objPOHeader.FreePPh22Indicator = PODraftHeaderData.FreePPh22Indicator
                        objPOHeader.PassTOP = PODraftHeaderData.PassTOP
                        objPOHeader.IsFactoring = PODraftHeaderData.IsFactoring
                        objPOHeader.SPL = PODraftHeaderData.SPL
                        objPOHeader.IsTransfer = PODraftHeaderData.IsTransfer
                        objPOHeader.PODestination = PODraftHeaderData.PODestination

                        For Each PODetailData As PODraftDetail In PODraftHeaderData.PODraftDetail
                            Dim objPODetail As PODetail = New PODetail
                            objPODetail.ContractDetail = PODetailData.ContractDetail
                            objPODetail.Discount = PODetailData.Discount
                            objPODetail.LineItem = PODetailData.LineItem
                            objPODetail.POHeader = objPOHeader
                            objPODetail.Price = PODetailData.Price
                            objPODetail.Interest = PODetailData.Interest
                            objPODetail.AmountRewardDepA = PODetailData.AmountRewardDepA
                            objPODetail.DiscountReward = PODetailData.DiscountReward
                            objPODetail.AmountReward = PODetailData.AmountReward
                            objPODetail.PPh22 = PODetailData.PPh22
                            objPODetail.ReqQty = PODetailData.ReqQty
                            objPODetail.LogisticCost = PODetailData.LogisticCost
                            objPOHeader.PODetails.Add(objPODetail)
                            'sums += PODetailData.ReqQty
                        Next
                        POHeaderID = New POHeaderFacade(User).Insert(objPOHeader)
                        Dim objPODraftHeader As PODraftHeader = New PODraftHeaderFacade(User).Retrieve(PODraftHeaderData.ID)
                        objPODraftHeader.POHeader = objPOHeader
                        objPODraftHeader.Status = CType(enumStatusPO.StatusDraftPO.SubmitPO, Integer)
                        objPODraftHeader.SubmitPODate = DateTime.Now
                        Dim PODraftHeaderID As Integer = New PODraftHeaderFacade(User).Update(objPODraftHeader)
                        If objPOHeader.IsFactoring = 0 Then
                            SetFreeDays(objPOHeader)
                        End If
                    End If
                Next
                MessageBox.Show("Submit PO Sukses.")
                BinddtgPO(dtgPO.CurrentPageIndex)
                GetTotalQuantity()
            Catch ex As Exception
                Dim POHFac As POHeaderFacade = New POHeaderFacade(User)
                Dim objPOHeaderRollback As POHeader = POHFac.Retrieve(POHeaderID)
                If Not IsNothing(objPOHeaderRollback) Then
                    POHFac.DeleteFromDB(objPOHeaderRollback)
                End If
                MessageBox.Show("Submit PO Gagal, ulangi beberapa saat lagi.")
                Return
            End Try

        End If

    End Sub

    Protected Sub txtKodeDealer_TextChanged(sender As Object, e As EventArgs) Handles txtKodeDealer.TextChanged
        BindddlPeriodeTanggalKirim()
    End Sub

    Protected Sub HFKodeDealer_ValueChanged(sender As Object, e As EventArgs) Handles HFKodeDealer.ValueChanged
        BindddlPeriodeTanggalKirim()
    End Sub
#End Region

#Region "Custom Method"
    Private Sub BindddlStatus()
        ddlStatus.Items.Add(New ListItem("Silahkan pilih", -1))
        For Each item As ListItem In LookUp.StatusDraftPO
            ddlStatus.Items.Add(item)
        Next
    End Sub

    Private Sub BindddlOrderType()
        ddlOrderType.Items.Clear()
        ddlOrderType.Items.Add(New ListItem("Silahkan pilih", -1))
        For Each item As ListItem In LookUp.ArrayJenisPO
            If item.Text = "Harian" Then
                If SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Harian) OrElse SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Semua) Then
                    ddlOrderType.Items.Add(item)
                End If
            ElseIf item.Text = "Tambahan" Then
                If (SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Tambahan) OrElse SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Semua)) Then
                    ddlOrderType.Items.Add(item)
                End If
            End If
        Next
    End Sub

    Private Sub BindDdlFreePPh()
        ddlFreePPh.Items.Clear()
        ddlFreePPh.Items.Add(New ListItem("Silahkan Pilih", -1))
        ddlFreePPh.Items.Add(New ListItem("Ya", 0))
        ddlFreePPh.Items.Add(New ListItem("Tidak", 1))
    End Sub

    Private Sub BindFactoring()
        Me.ddlFactoring.Items.Clear()
        Me.ddlFactoring.Items.Add(New ListItem("Silahkan Pilih", 2))
        Me.ddlFactoring.Items.Add(New ListItem("Factoring", 1))
        Me.ddlFactoring.Items.Add(New ListItem("Non Factoring", 0))

        Dim IsDSF As Boolean = (CType(Session.Item("DEALER"), Dealer).Title = CType(EnumDealerTittle.DealerTittle.LEASING, String)) ' (CType(Session.Item("DEALER"), Dealer).DealerCode.Trim.ToUpper = "DSF")
        If IsDSF Then
            Me.ddlFactoring.SelectedValue = 1
            Me.ddlFactoring.Enabled = False
        Else
            Me.ddlFactoring.SelectedValue = 2
            Me.ddlFactoring.Enabled = True
        End If
    End Sub

    Private Sub BindDdlPaymentType()
        ddlPaymentType.Items.Clear()
        ddlPaymentType.Items.Add(New ListItem("Silahkan Pilih", 0))
        For Each li As ListItem In enumPaymentType.GetList
            ddlPaymentType.Items.Add(li)
        Next
    End Sub

    Private Sub BindDdlPeriode()
        For Each item As ListItem In LookUp.ArraylistMonth(True, 6, 1, DateTime.Now)
            ddlPeriodeMDP.Items.Add(item)
        Next
        ddlPeriodeMDP.SelectedValue = Format(DateTime.Now, "MMM yyyy").ToString
        BindddlPeriodeTanggalKirim()
    End Sub

    Private Sub BindddlPeriodeTanggalKirim()
        ddlPeriodeTanggalKirim.Items.Clear()
        Dim tgl As DateTime = System.Convert.ToDateTime(ddlPeriodeMDP.SelectedValue)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MDPDealerDailyStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MDPDealerDailyStock), "PeriodMonth", MatchType.Exact, tgl.Month))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MDPDealerDailyStock), "PeriodYear", MatchType.Exact, tgl.Year))
        If txtKodeDealer.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MDPDealerDailyStock), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text))
        End If
        Dim arrListPeriod As ArrayList = New MDPDealerDailyStockFacade(User).Retrieve(criterias)
        Dim arrPeriod As ArrayList = New ArrayList

        For Each objMDPDealerDailyStock As MDPDealerDailyStock In arrListPeriod
            If arrPeriod.IndexOf(objMDPDealerDailyStock.PeriodStartDate.ToString()) = -1 Then
                arrPeriod.Add(objMDPDealerDailyStock.PeriodStartDate.ToString())
                Dim li As New ListItem
                li.Text = objMDPDealerDailyStock.PeriodStartDate.Day.ToString() + " - " + objMDPDealerDailyStock.PeriodEndDate.Day.ToString()
                li.Value = objMDPDealerDailyStock.PeriodStartDate.ToString() + ";" + objMDPDealerDailyStock.PeriodEndDate.ToString()
                ddlPeriodeTanggalKirim.Items.Add(li)
            End If
        Next
    End Sub

    Private Sub BindToddlCategory()
        Dim arrayListCategory As ArrayList = New PKHeaderFacade(User).RetrieveListCategory
        ddlCategory.Items.Clear()
        If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
            Dim listitemBlank As New ListItem("Silahkan Pilih", -1)
            ddlCategory.Items.Add(listitemBlank)
        End If

        Dim PCID As Short = GetProductCategoryID()
        For Each item As Category In arrayListCategory
            Dim listItem As New ListItem(item.CategoryCode, item.ID)
            If item.CategoryCode = "PC" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryPC_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                    If PCID = 0 OrElse PCID = item.ProductCategory.ID Then
                        ddlCategory.Items.Add(listItem)
                    End If
                End If
            ElseIf item.CategoryCode = "LCV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryLCV_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                    If PCID = 0 OrElse PCID = item.ProductCategory.ID Then
                        ddlCategory.Items.Add(listItem)
                    End If
                End If
            ElseIf item.CategoryCode = "CV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryCV_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                    If PCID = 0 OrElse PCID = item.ProductCategory.ID Then
                        ddlCategory.Items.Add(listItem)
                    End If
                End If
            End If
        Next
    End Sub

    Private Function GetProductCategoryID() As Short
        '1 = id dari product category mmc
        Dim PCID As Short = CType(1, Short)
        If PCID < 1 Then PCID = 0

        Return PCID
    End Function

    Private Sub BinddtgPO(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ContractHeader.Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If txtKodeDealer.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ContractHeader.Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        End If

        If ddlOrderType.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "POType", MatchType.Exact, ddlOrderType.SelectedValue))
        If ddlCategory.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ContractHeader.Category.ID", MatchType.Exact, ddlCategory.SelectedValue))

        If txtNoDraftPO.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "DraftPONumber", MatchType.Exact, txtNoDraftPO.Text))
        End If

        If ddlStatus.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "Status", MatchType.Exact, ddlStatus.SelectedItem.Value))
        End If

        'Start  :DailyPO - add info about PPh Status
        If ddlFreePPh.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "FreePPh22Indicator", MatchType.Exact, ddlFreePPh.SelectedValue))
        End If
        'End    :DailyPO - add info about PPh Status
        If CType(ddlFactoring.SelectedValue, Short) <> 2 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "IsFactoring", MatchType.Exact, CType(ddlFactoring.SelectedValue, Short)))
        End If
        'Add RTGS
        If ddlPaymentType.SelectedValue > 0 Then
            If ddlPaymentType.SelectedItem.Text.Trim.ToUpper.EndsWith("TOP") Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "TermOfPayment.PaymentType", MatchType.Exact, CType(enumPaymentType.PaymentType.TOP, Short)))
            ElseIf ddlPaymentType.SelectedItem.Text.Trim.ToUpper = "COD" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "TermOfPayment.PaymentType", MatchType.Exact, CType(enumPaymentType.PaymentType.COD, Short)))
            ElseIf ddlPaymentType.SelectedItem.Text.Trim.ToUpper = "RTGS" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "TermOfPayment.PaymentType", MatchType.Exact, CType(enumPaymentType.PaymentType.RTGS, Short)))
            End If
        End If
        'end Add RTGS
        Dim X As PODraftHeader

        Dim PCID As Short = Me.GetProductCategoryID()
        If PCID > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ContractHeader.Category.ProductCategory.ID", MatchType.Exact, PCID))
        End If

        Dim tglDari As DateTime = DateTime.Now
        Dim tglSampai As DateTime = DateTime.Now

        If ddlPeriodeTanggalKirim.SelectedValue <> "" Then
            Dim arrTglKirim As Array = ddlPeriodeTanggalKirim.SelectedItem.Value.Split(";")
            tglDari = Convert.ToDateTime(arrTglKirim(0))
            tglSampai = Convert.ToDateTime(arrTglKirim(1))
        End If

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationYear", MatchType.Greater, CType(tglDari.Year.ToString, Short)), "(", True)
        criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationYear", MatchType.Exact, CType(tglDari.Year.ToString, Short)), "(", True)
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationMonth", MatchType.Greater, CType(tglDari.Month.ToString, Short)), ")", False)
        criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationYear", MatchType.Exact, CType(tglDari.Year.ToString, Short)), "(", True)
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationMonth", MatchType.Exact, CType(tglDari.Month.ToString, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationDate", MatchType.GreaterOrEqual, CType(tglDari.Day.ToString, Short)), "))", False)

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationYear", MatchType.Lesser, CType(tglSampai.Year.ToString, Short)), "(", True)
        criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationYear", MatchType.Exact, CType(tglSampai.Year.ToString, Short)), "(", True)
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationMonth", MatchType.Lesser, CType(tglSampai.Month.ToString, Short)), ")", False)
        criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationYear", MatchType.Exact, CType(tglSampai.Year.ToString, Short)), "(", True)
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationMonth", MatchType.Exact, CType(tglSampai.Month.ToString, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "ReqAllocationDate", MatchType.LesserOrEqual, CType(tglSampai.Day.ToString, Short)), "))", False)

        sessionHelper.SetSession("Criterias", criterias)

        _ListPO = New PODraftHeaderFacade(User).RetrieveActiveList(currentPageIndex + 1, dtgPO.PageSize, _
                       total, CType(ViewState("CurrentSortColumn"), String), _
                       CType(ViewState("CurrentSortDirect"), Sort.SortDirection), criterias)

        _ListPODraftHeader = New PODraftHeaderFacade(User).Retrieve(criterias)

        sessionHelper.SetSession("LISTPODRAFT", _ListPODraftHeader)
        dtgPO.DataSource = _ListPO

        dtgPO.VirtualItemCount = total

        If _ListPO.Count > 0 Then
            dtgPO.DataBind()
            btnSubmitPO.Enabled = True
        Else
            dtgPO.DataBind()
            btnSubmitPO.Enabled = False
            MessageBox.Show("Data Tidak Ditemukan")
        End If

    End Sub

    Private Sub ClearControl()
        txtKodeDealer.Text = String.Empty
        If ddlStatus.Items.Count > 0 Then
            ddlStatus.SelectedIndex = 0
        End If
        If ddlCategory.Items.Count > 0 Then
            ddlCategory.SelectedIndex = 0
        End If
        If ddlPeriodeMDP.Items.Count > 0 Then
            ddlPeriodeMDP.SelectedIndex = 0
        End If
        If ddlOrderType.Items.Count > 0 Then
            ddlOrderType.SelectedIndex = 0
        End If
        If ddlPeriodeTanggalKirim.Items.Count > 0 Then
            ddlPeriodeTanggalKirim.SelectedIndex = 0
        End If
        If ddlPaymentType.Items.Count > 0 Then
            ddlPaymentType.SelectedIndex = 0
        End If
        If ddlFreePPh.Items.Count > 0 Then
            ddlFreePPh.SelectedIndex = 0
        End If
        If ddlFactoring.Items.Count > 0 Then
            ddlFactoring.SelectedIndex = 0
        End If
        txtNoDraftPO.Text = String.Empty
    End Sub

    Private Function GetTotalQuantity() As Integer
        Dim strID As String = ""
        Dim totQty As Integer = 0
        If IsNothing(_ListPODraftHeader) Then
            _ListPODraftHeader = sessionHelper.GetSession("LISTPODRAFT")
        End If

        For Each item As PODraftHeader In _ListPODraftHeader
            strID &= item.ID.ToString & ","
        Next

        If strID <> "" Then
            strID = Left(strID, strID.Length - 1)
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.V_RekapDraftPO), "ID", MatchType.InSet, "(" & strID & ")"))
            Dim arl As ArrayList = New v_RekapDraftPOFacade(User).Retrieve(criterias)

            For Each obj As V_RekapDraftPO In arl
                totQty += obj.TotalQuantity
            Next
        End If

        'End Modified by firman 17 Feb 15 (Reduce Roundtrip)
        lblQuantity.Text = FormatNumber(totQty, 0, , , TriState.UseDefault) & " Unit"
        Return totQty
    End Function

    Private Function IsEnableCeilingFilter(ByVal objPODraftHeader As PODraftHeader) As Boolean
        'If chkFactoring.Checked Then Return True
        If objPODraftHeader.IsFactoring = 1 Then Return True
        Dim oD As Dealer = sessionHelper.GetSession("DEALER")
        Dim oTC As TransactionControl

        'If Me.GetProductCategory.Code.Trim.ToUpper() = "MFTBC" Then
        If objPODraftHeader.ContractHeader.Category.CategoryCode.Trim.ToUpper() = "MFTBC" Then
            oTC = New DealerFacade(User).RetrieveTransactionControl(oD.ID, EnumDealerTransType.DealerTransKind.FilterPengajuanPO)
        Else
            oTC = New DealerFacade(User).RetrieveTransactionControl(oD.ID, EnumDealerTransType.DealerTransKind.FilterPengajuanPOMMC)
        End If
        If Not IsNothing(oTC) AndAlso oTC.ID > 0 Then
            If oTC.Status = 1 Then
                Return True
            Else
                Return False
            End If
        Else
            Return True
        End If

    End Function

    Public Function IsLesserThanAvailableCeiling(ByVal objPODraftHeader As PODraftHeader, Optional ByVal IsAfterSaving As Boolean = False) As Boolean
        Dim objD As Dealer = Session("DEALER")
        Dim TotalPO As Decimal = Me.GetTotalPOInUI(objPODraftHeader) ' CType(viewstate.Item("SubTotalHarga"), Decimal)
        Dim oTEOP As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(objPODraftHeader.TermOfPayment.ID)
        Dim IsLesser As Boolean = False

        For Each PODraftDetailData As PODraftDetail In objPODraftHeader.PODraftDetail
            If oTEOP.PaymentType = CType(enumPaymentType.PaymentType.RTGS, Short) Then
                IsLesser = True
            Else
                Dim Ceiling As Decimal = 0
                Dim Proposed As Decimal = 0
                Dim Liquified As Decimal = 0
                Dim Outstanding As Decimal = 0
                Dim TodaysAvCeiling As Decimal = 0
                Dim TomorrowAvCeiling As Decimal = 0
                Dim AvCeiling As Decimal = 0

                If objPODraftHeader.IsFactoring = 1 Then
                    Dim AvFactCeiling As Decimal = 0
                    Dim oFM As FactoringMaster = New FactoringMasterFacade(User).Retrieve(objPODraftHeader.ContractHeader.Category.ProductCategory, objD.CreditAccount)

                    If oTEOP.PaymentType = enumPaymentType.PaymentType.TOP Then
                        Dim dtJatuhTempo As Date = DateAdd(DateInterval.Day, oTEOP.TermOfPaymentValue, objPODraftHeader.ReqAllocationDateTime)
                        If dtJatuhTempo > oFM.MaxTOPDate Then
                            MessageBox.Show("Jatuh Tempo PO Melebihi Tanggal Validitas Ceiling")
                            Return False
                        End If
                    End If
                    IsLesser = CommonFunction.IsEnoughForFactoring(objPODraftHeader.ContractHeader.Category.ProductCategory, CType(PODraftDetailData.ReqQty, Integer), TotalPO, CType(Session("DEALER"), Dealer).CreditAccount, IsAfterSaving, AvFactCeiling _
                    , Ceiling, Proposed, Liquified, Outstanding)

                Else
                    'Credit Ceiling
                    Dim paymentScheme As Short = Me.GetCurrentPaymentMethod(objPODraftHeader.ContractHeader.PKHeader, objPODraftHeader)

                    If paymentScheme = TransferControl.EnumPaymentScheme.Gyro Then

                        If oTEOP.PaymentType = CType(enumPaymentType.PaymentType.TOP, Short) Then
                            Dim objSCM As sp_CreditMaster = GetCeilingCredit(objPODraftHeader.ContractHeader.Category.ProductCategory, objD.CreditAccount, oTEOP.PaymentType, objPODraftHeader.ReqAllocationDateTime)
                            If objSCM Is Nothing Then
                                MessageBox.Show("Total PO melebihi Ceiling yang tersedia")
                                Return False
                            Else
                                If objSCM.ID < 1 Then
                                    MessageBox.Show("Total PO melebihi Ceiling yang tersedia")
                                    Return False
                                End If
                            End If
                            Dim dtJatuhTempo As Date = DateAdd(DateInterval.Day, oTEOP.TermOfPaymentValue, objPODraftHeader.ReqAllocationDateTime)
                            If dtJatuhTempo > objSCM.MaxTOPDate Then
                                MessageBox.Show("Jatuh Tempo PO Melebihi Tanggal Validitas Ceiling")
                                Return False
                            End If
                        End If

                        IsLesser = CommonFunction.IsCeilingEnoughSimulationTOP(objPODraftHeader.ContractHeader.Category.ProductCategory, CType(PODraftDetailData.ReqQty, Integer), objPODraftHeader.ReqAllocationDateTime, TotalPO, objD.CreditAccount, oTEOP.PaymentType, IsAfterSaving, Ceiling, Proposed, Liquified, Outstanding, TodaysAvCeiling, TomorrowAvCeiling, AvCeiling)
                    Else 'paymentSchema = TRANSFER
                        Dim i As Integer = objPODraftHeader.ID
                        Dim oTCFac As New TransferCeilingFacade(User)
                        Dim oTC As New TransferCeiling()
                        Dim IsEnough As Boolean = False
                        Dim sMsg As String = ""
                        Dim NewAvCeiling As Decimal = 0

                        IsEnough = oTCFac.IsEnoughCeiling(objPODraftHeader, TotalPO, oTC, NewAvCeiling, sMsg)

                        If Not IsEnough Then MessageBox.Show(sMsg)

                        Return IsEnough

                    End If
                End If
            End If
        Next

        If IsLesser = False Then
            MessageBox.Show("Total PO melebihi Ceiling yang tersedia")
        End If
        Return IsLesser
    End Function

    Private Function GetTotalPOInUI(ByVal objPODraftHeader As PODraftHeader) As Decimal
        Dim TotalPO As Decimal = 0

        For Each di As PODraftDetail In objPODraftHeader.PODraftDetail
            'Dim txtQty As TextBox = di.FindControl("TextBox1")
            'Dim ID As Integer = CType(di.Cells(0).Text, Integer)
            Dim Qty As String = di.ReqQty.ToString()
            Dim ID As Integer = di.ContractDetail.ID
            Dim oCD As ContractDetail = New ContractDetailFacade(User).Retrieve(ID)
            Dim n As Integer

            If Not IsNothing(oCD) AndAlso oCD.ID > 0 Then
                Try
                    n = CType(Qty, Double)
                Catch ex As Exception
                    n = 0
                End Try
                TotalPO += n * oCD.Amount
            End If
        Next
        Return TotalPO
    End Function

    Private Function GetCurrentPaymentMethod(ByRef objPKHeader As PKHeader, ByRef objPODraftHeader As PODraftHeader) As Short
        'start : add payment scheme information (Gyro or Transfer) on 20160815
        Dim cTC As New CriteriaComposite(New Criteria(GetType(TransferControl), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim curPeriod As Date = DateSerial(objPODraftHeader.ReqAllocationDateTime.Year, objPODraftHeader.ReqAllocationDateTime.Month, 1)
        Dim sTC As New SortCollection()
        Dim oTCFac As New TransferControlFacade(User)
        Dim aTCs As ArrayList
        Dim oTC As TransferControl
        Dim state As Short

        cTC.opAnd(New Criteria(GetType(TransferControl), "CreditAccount", MatchType.Exact, objPKHeader.Dealer.CreditAccount))
        cTC.opAnd(New Criteria(GetType(TransferControl), "ValidFrom", MatchType.LesserOrEqual, curPeriod))
        cTC.opAnd(New Criteria(GetType(TransferControl), "ProductCategory.ID", MatchType.Exact, objPKHeader.Category.ProductCategory.ID))
        cTC.opAnd(New Criteria(GetType(TransferControl), "PaymentType", MatchType.Exact, objPODraftHeader.TermOfPayment.PaymentType))

        sTC.Add(New Sort(GetType(TransferControl), "ValidFrom", Sort.SortDirection.DESC))
        aTCs = oTCFac.Retrieve(cTC, sTC)
        If (aTCs.Count > 0) Then
            oTC = aTCs(0)
            state = oTC.Status
        Else
            state = TransferControl.EnumPaymentScheme.Gyro
        End If
        'end : add payment scheme information (Gyro or Transfer) on 20160815
        Return state
    End Function

    Private Function GetCeilingCredit(ByVal PC As ProductCategory, ByVal CreditAccount As String, ByVal PaymentType As Short, ByVal PermintaanKirim As DateTime) As sp_CreditMaster
        Dim objSCMFac As sp_CreditMasterFacade = New sp_CreditMasterFacade(User)
        Dim arlSCM As ArrayList
        Dim objSCM As sp_CreditMaster
        Dim crtSCM As CriteriaComposite
        Dim ReportDate As Date = DateSerial(Now.Year, Now.Month, Now.Day)
        Dim ReqDelDate As Date = PermintaanKirim

        crtSCM = New CriteriaComposite(New Criteria(GetType(sp_CreditMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtSCM.opAnd(New Criteria(GetType(sp_CreditMaster), "CreditAccount", MatchType.Exact, CreditAccount))
        crtSCM.opAnd(New Criteria(GetType(sp_CreditMaster), "PaymentType", MatchType.Exact, PaymentType))
        arlSCM = objSCMFac.RetrieveFromSP(PC, ReportDate, ReqDelDate, crtSCM)
        If arlSCM.Count > 0 Then
            Return CType(arlSCM(0), sp_CreditMaster)
        Else
            Return Nothing
        End If
    End Function

    Private Sub SetSessionCriteria()
        Dim objSCPODraft As ArrayList = New ArrayList
        objSCPODraft.Add(txtKodeDealer.Text)
        objSCPODraft.Add(ddlStatus.SelectedIndex)
        objSCPODraft.Add(ddlPeriodeMDP.SelectedIndex)
        objSCPODraft.Add(ddlPeriodeTanggalKirim.SelectedIndex)

        objSCPODraft.Add(ddlCategory.SelectedIndex)
        objSCPODraft.Add(ddlOrderType.SelectedIndex)
        objSCPODraft.Add(txtNoDraftPO.Text)
        objSCPODraft.Add(ddlPaymentType.SelectedValue)
        objSCPODraft.Add(ddlFreePPh.SelectedValue)
        objSCPODraft.Add(ddlFactoring.SelectedValue)

        objSCPODraft.Add(dtgPO.CurrentPageIndex)
        objSCPODraft.Add(CType(ViewState("CurrentSortColumn"), String))
        objSCPODraft.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        sessionHelper.SetSession("SESPODRAFTCRITERIA", objSCPODraft)
    End Sub

    Private Function GetSessionCriteria() As Boolean
        Dim objSCPODraft As ArrayList = sessionHelper.GetSession("SESPODRAFTCRITERIA")
        If Not objSCPODraft Is Nothing Then
            txtKodeDealer.Text = objSCPODraft.Item(0)
            ddlStatus.SelectedIndex = objSCPODraft.Item(1)
            ddlPeriodeMDP.SelectedIndex = objSCPODraft.Item(2)
            ddlPeriodeMDP_SelectedIndexChanged(Nothing, Nothing)
            ddlPeriodeTanggalKirim.SelectedIndex = objSCPODraft.Item(3)

            ddlCategory.SelectedIndex = objSCPODraft.Item(4)
            ddlOrderType.SelectedIndex = objSCPODraft.Item(5)
            txtNoDraftPO.Text = objSCPODraft.Item(6)
            ddlPaymentType.SelectedValue = objSCPODraft.Item(7)
            ddlFreePPh.SelectedValue = objSCPODraft.Item(8)
            ddlFactoring.SelectedValue = objSCPODraft.Item(9)

            dtgPO.CurrentPageIndex = objSCPODraft.Item(10)
            ViewState("CurrentSortColumn") = objSCPODraft.Item(11)
            ViewState("CurrentSortDirect") = objSCPODraft.Item(12)

            Return True
        End If
        Return False
    End Function

    Private Function GetItemDeposit(ByVal oCD As ContractDetail, ByVal objPOHeader As POHeader) As Double
        Return oCD.GuaranteeAmount
        Exit Function

        Dim i As Integer
        i = 0
        Dim dt As Date = DateSerial(objPOHeader.ReqAllocationYear, objPOHeader.ReqAllocationMonth, objPOHeader.ReqAllocationDate)
        If oCD.ContractHeader.PKHeader.JaminanID = 0 Then
            Dim oJFac As JaminanFacade = New JaminanFacade(User)
            Dim crtJ As CriteriaComposite
            Dim arlJ As New ArrayList

            'Dim oJ As Jaminan

            crtJ = New CriteriaComposite(New Criteria(GetType(Jaminan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crtJ.opAnd(New Criteria(GetType(Jaminan), "ValidFrom", MatchType.LesserOrEqual, dt))
            crtJ.opAnd(New Criteria(GetType(Jaminan), "ValidTo", MatchType.GreaterOrEqual, dt))
            crtJ.opAnd(New Criteria(GetType(Jaminan), "Status", MatchType.Exact, CType(EnumStatusSPL.StatusSPL.Aktif, Short)))
            'crtJ.opAnd(New Criteria(GetType(Jaminan), "DealerCode", MatchType.Partial, lblDealerCode.Text))
            arlJ = oJFac.Retrieve(crtJ)
            If arlJ.Count > 0 Then
                For Each oJ As Jaminan In arlJ
                    If (" " & oJ.DealerCode).IndexOf(objPOHeader.Dealer.DealerCode) > 0 Then
                        For Each oJD As JaminanDetail In oJ.JaminanDetails
                            If oJD.VehicleTypeCode = oCD.VechileColor.VechileType.VechileTypeCode AndAlso (dt >= oJD.Jaminan.ValidFrom And dt <= oJD.Jaminan.ValidTo) And IIf(oJD.Purpose = LookUp.enumPurpose.Semua, True, oJD.Purpose = oCD.ContractHeader.Purpose) Then
                                Return oJD.Amount
                            End If
                        Next
                    End If
                Next
            Else
                Return 0
            End If
        Else
            Dim oJFac As JaminanFacade = New JaminanFacade(User)
            Dim oJ As Jaminan
            oJ = oJFac.Retrieve(oCD.ContractHeader.PKHeader.JaminanID)
            For Each oJD As JaminanDetail In oJ.JaminanDetails
                If oJD.VehicleTypeCode = oCD.VechileColor.VechileType.VechileTypeCode AndAlso (dt >= oJD.Jaminan.ValidFrom And dt <= oJD.Jaminan.ValidTo) And IIf(oJD.Purpose = LookUp.enumPurpose.Semua, True, oJD.Purpose = oCD.ContractHeader.Purpose) Then
                    Return oJD.Amount
                End If
            Next
        End If
        Return 0
    End Function

    'Dim sums As Integer = 0
    Private Sub SetFreeDays(poHeader As POHeader)
        Dim dt As Date = DateSerial(poHeader.ReqAllocationYear, poHeader.ReqAllocationMonth, poHeader.ReqAllocationDate)
        Dim warning As String = ""
        Dim MaxTop As Integer = 0
        Dim dMod As New Dictionary(Of Integer, Integer) 'Record row / Model ID
        For Each pod As PODetail In poHeader.PODetails
            Dim MID As Integer = pod.ContractDetail.VechileColor.VechileType.VechileModel.ID
            If Not dMod.ContainsKey(MID) Then
                dMod.Add(MID, 1)
            Else
                dMod(MID) += 1
            End If
        Next
        For Each i As Integer In dMod.Keys
            Dim pd As New ArrayList
            For Each j As PODetail In poHeader.PODetails
                If j.ContractDetail.VechileColor.VechileType.VechileModel.ID = i Then
                    j.FreeDays = 0
                    j.MaxTOPDay = 0
                    pd.Add(j)
                End If
            Next
            Dim getFreeDays As Integer = 0
            Dim getMaxTopDays As Integer = 0
            sessionHelper.SetSession("Itung", True)
            getFreeDays = CalculateFreeDays(poHeader, objDealer, pd, dt, dt, dt, MaxTop, warning)

            If getFreeDays = -1 AndAlso getMaxTopDays = -1 Then
                ViewState("warning") = sessionHelper.GetSession("Warning")
                ViewState("pops") = True
                Exit Sub
            End If

            For Each pod As PODetail In pd
                pod.FreeDays = getFreeDays
                pod.MaxTOPDay = getMaxTopDays
            Next

            For Each _Detail As PODetail In poHeader.PODetails
                For Each objPD As PODetail In pd
                    If _Detail.ContractDetail.VechileColor.VechileType.VechileModel.ID = objPD.ContractDetail.VechileColor.VechileType.VechileModel.ID Then
                        _Detail.FreeDays = getFreeDays
                        _Detail.MaxTOPDay = MaxTop
                        _Detail.Interest = CalculateInterestNonFactoring(poHeader, _Detail)
                        Dim PDFacade As New PODetailFacade(User)
                        PDFacade.Update(_Detail)
                    End If
                Next
            Next
        Next

    End Sub

    Public Function CalculateFreeDays(poHeader As POHeader, Dealer As Dealer, PoDetails As ArrayList, recAllocDateTime As Date, ValidFrom As Date, ValidTo As Date, ByRef VarMaxTOP As Integer, ByRef LastPeriodeRemain As String) As Integer
        Try
            If poHeader.IsFactoring = 1 Then
                VarMaxTOP = 0
                LastPeriodeRemain = ""
                Return 0
            End If
        Catch
        End Try
        Dim sessHelp As SessionHelper = New SessionHelper
        Dim POTargetFac As New DealerPOTargetFacade(User)
        Dim modelID As String = ""
        Dim PODetailID As String = ""
        Dim detaiD As New ArrayList
        Dim _return As Integer = 0
        For Each podetail As PODetail In PoDetails
            If modelID.Length = 0 Then
                modelID = podetail.ContractDetail.VechileColor.VechileType.VechileModel.ID
                PODetailID = podetail.ID
            Else
                modelID = modelID & "," & podetail.ContractDetail.VechileColor.VechileType.VechileModel.ID
                PODetailID = PODetailID & "," & podetail.ID
            End If
            recAllocDateTime = ValidFrom
            detaiD.Add(podetail.ID)
        Next

        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerPOTarget), "RowStatus", MatchType.Exact, (CType(DBRowStatus.Active, Short))))
        criteria.opAnd(New Criteria(GetType(DealerPOTarget), "Dealer.ID", MatchType.Exact, Dealer.ID))
        criteria.opAnd(New Criteria(GetType(DealerPOTarget), "ValidFrom", MatchType.GreaterOrEqual, ValidFrom.Year & "-" & ValidFrom.Month & "-01 00:00:00.000"))
        criteria.opAnd(New Criteria(GetType(DealerPOTarget), "ValidTo", MatchType.LesserOrEqual, ValidTo.Year & "-" & ValidTo.Month & "-" & DateTime.DaysInMonth(ValidFrom.Year, ValidFrom.Month) & " 00:00:00.000"))
        Dim arlModel As ArrayList = POTargetFac.Retrieve(criteria)
        criteria.opAnd(New Criteria(GetType(DealerPOTarget), "VechileModel.ID", MatchType.InSet, "(" & modelID & ")"))
        'criteria.opAnd(New Criteria(GetType(DealerPOTarget), "VechileModel.ID", MatchType.Exact, PODetail.ContractDetail.VechileColor.VechileType.VechileModel.ID))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(DealerPOTarget), "ValidTo", Sort.SortDirection.ASC))  '-- Sort by Vechile type code
        Dim arlPOTarget As ArrayList = POTargetFac.Retrieve(criteria, sortColl)

        Dim mods = From arl As DealerPOTarget In arlPOTarget
                   Select arl.VechileModel.ID Distinct

        Dim mods2 = From arl As DealerPOTarget In arlModel
        Select arl.VechileModel.ID Distinct

        Dim modsArr As New ArrayList
        For Each a As Short In mods
            modsArr.Add(a)
        Next

        Dim modsArr2 As New ArrayList
        For Each a As Short In mods2
            modsArr2.Add(a)
        Next
        Dim ada As Boolean = False
        Dim gaada As Boolean = False
        For Each st As String In modelID.Split(",")
            If modsArr2.Contains(CType(st, Short)) Then
                ada = True
            Else
                gaada = True
            End If
        Next

        If ada AndAlso gaada Then
            _return = -1
            VarMaxTOP = -1
            LastPeriodeRemain = "Model kendaraan Program TOP Khusus tidak dapat digabungkan dengan Model Kendaraan lain"
            ViewState("warning") = LastPeriodeRemain
            ViewState("pops") = True
            Return _return
        ElseIf Not ada AndAlso gaada Then
            _return = 0
            VarMaxTOP = 0
            Return _return
        End If

        Dim arlPoDetail As New ArrayList
        'If Not sessHelp.GetSession("Itung") Then
        '    arlPoDetail = objPODraftHeader.PODraftDetail
        'Else
        Dim PDetailFac As New PODetailFacade(User)
        Dim criteriaPD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, (CType(DBRowStatus.Active, Short))))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "ID", MatchType.NotInSet, PODetailID))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.Dealer.ID", MatchType.Exact, Dealer.ID))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.VechileType.VechileModel.ID", MatchType.InSet, "(" & modelID & ")"))
        'criteriaPD.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.VechileType.VechileModel.ID", MatchType.Exact, PODetail.ContractDetail.VechileColor.VechileType.VechileModel.ID))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationDateTime", MatchType.GreaterOrEqual, ValidFrom.Year & "-" & ValidFrom.Month & "-01 00:00:00.000"))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationDateTime", MatchType.LesserOrEqual, ValidTo.Year & "-" & ValidTo.Month & "-" & DateTime.DaysInMonth(ValidFrom.Year, ValidFrom.Month) & " 00:00:00.000"))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.Status", MatchType.InSet, ("(0, 2, 4, 6, 8)")))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.IsFactoring", MatchType.Exact, 0))
        arlPoDetail = PDetailFac.Retrieve(criteriaPD)
        'End If

        Dim AllocRemain As Integer = 0
        Dim ExpiredPeriode As Boolean = False
        Dim OverQuantity As Boolean = False
        Dim CurrentQuantity As Integer = 0
        Dim arlPeriodeRemain As New ArrayList
        Dim dFDays As New Dictionary(Of Integer, Integer)
        Dim dFDaysTarget As New Dictionary(Of Integer, Integer)

        For Each pDetail As PODetail In arlPoDetail
            If Not IsNothing(pDetail.POHeader) Then
                If pDetail.POHeader.IsFactoring <> 0 Then
                    Continue For
                End If
            End If

            If Not IsNothing(sessHelp.GetSession("EditPO")) OrElse sessHelp.GetSession("EAlloc") Then
                If detaiD.Contains(pDetail.ID) Then
                    pDetail.FreeDays = 0
                    recAllocDateTime = ValidFrom
                    If sessHelp.GetSession("EAlloc") AndAlso sessHelp.GetSession("Itung") Then
                        For Each _d As PODetail In PoDetails
                            If pDetail.AllocQty <> _d.ReqQty AndAlso pDetail.ID = _d.ID Then
                                pDetail.AllocQty = _d.ReqQty
                            End If
                        Next
                    End If
                End If
            End If

            If Not dFDays.ContainsKey(pDetail.FreeDays) Then
                dFDays.Add(pDetail.FreeDays, 0)
            End If
            If sessHelp.GetSession("Itung") OrElse sessHelp.GetSession("EAlloc") Then
                If Not IsNothing(pDetail.POHeader) Then
                    Select Case pDetail.POHeader.Status
                        Case 0
                            dFDays(pDetail.FreeDays) += pDetail.ReqQty
                        Case 2
                            If pDetail.AllocQty = 0 Then
                                dFDays(pDetail.FreeDays) += pDetail.ReqQty
                            ElseIf pDetail.AllocQty > 0 Then
                                dFDays(pDetail.FreeDays) += pDetail.AllocQty
                            End If
                        Case 4, 6, 8
                            dFDays(pDetail.FreeDays) += pDetail.AllocQty
                    End Select
                Else
                    'buat yg mdp
                    dFDays(pDetail.FreeDays) += pDetail.ReqQty
                End If
            Else
                dFDays(pDetail.FreeDays) += pDetail.ReqQty
            End If
        Next

        If sessHelp.GetSession("Itung") AndAlso Not sessHelp.GetSession("EAlloc") Then
            If Not dFDays.ContainsKey(0) Then
                dFDays.Add(0, 0)
            End If
            For Each PoDe As PODetail In PoDetails
                dFDays(0) += PoDe.ReqQty
            Next
        End If

        If Not IsNothing(sessHelp.GetSession("EditPO")) Then
            dFDays(0) = CType(sessHelp.GetSession("EditPO"), Integer)
        End If

        Try
            Dim freeDays As ArrayList = POTargetFac.RetrieveDefaultFreeDays(Dealer, modelID)
            If freeDays.Count > 0 Then
                _return = CType(freeDays(0), DealerPOTarget).FreeDays
                VarMaxTOP = CType(freeDays(0), DealerPOTarget).MaxTOPDay
            End If
        Catch ex As Exception
        End Try
        sessHelp.RemoveSession("Warning")
        Dim carryOver As Integer = 0
        For Each dPOT As DealerPOTarget In arlPOTarget
            If Not dFDaysTarget.ContainsKey(dPOT.FreeDays) Then
                dFDaysTarget.Add(dPOT.FreeDays, dPOT.MaxQuantity)
            End If

            If carryOver > 0 Then
                dFDaysTarget(dPOT.FreeDays) += carryOver
            End If

            If dFDays.ContainsKey(dPOT.FreeDays) Then
                dFDaysTarget(dPOT.FreeDays) -= dFDays(dPOT.FreeDays)
                dFDays.Remove(dPOT.FreeDays)
                AllocRemain += dFDaysTarget(dPOT.FreeDays)
            End If
            carryOver = 0
            If recAllocDateTime <= dPOT.ValidTo Then
                ExpiredPeriode = False
            ElseIf recAllocDateTime > dPOT.ValidTo Then
                ExpiredPeriode = True
                If Date.Now.Date > dPOT.ValidTo Then
                    carryOver += dFDaysTarget(dPOT.FreeDays)
                    dFDaysTarget.Remove(dPOT.FreeDays)
                End If
            End If

            If dFDays.ContainsKey(0) Then
                If ExpiredPeriode Then
                    Continue For
                End If

                If AllocRemain >= 0 Then
                    'If dFDaysTarget(dPOT.FreeDays) = 0 Then
                    '    dFDaysTarget.Remove(dPOT.FreeDays)
                    '    Continue For
                    'ElseIf OverQuantity AndAlso (dFDaysTarget(dPOT.FreeDays) - dFDays(0)) >= 0 Then
                    If OverQuantity AndAlso (dFDaysTarget(dPOT.FreeDays) - dFDays(0)) >= 0 Then
                        _return = dPOT.FreeDays
                        VarMaxTOP = dPOT.MaxTOPDay
                        dFDaysTarget(dPOT.FreeDays) -= dFDays(0)
                        dFDays.Remove(0)
                        Continue For
                    ElseIf (dFDaysTarget(dPOT.FreeDays) - dFDays(0)) >= 0 Then
                        _return = dPOT.FreeDays
                        VarMaxTOP = dPOT.MaxTOPDay
                        dFDaysTarget(dPOT.FreeDays) -= dFDays(0)
                        dFDays.Remove(0)
                        OverQuantity = False
                    ElseIf (dFDaysTarget(dPOT.FreeDays) - dFDays(0)) < 0 Then
                        OverQuantity = True
                        Continue For

                        If LastPeriodeRemain.Length = 0 Then
                            LastPeriodeRemain = "Sisa freedays pada periode " & dPOT.Sequence & " untuk kendaraan " & dPOT.VechileModel.IndDescription & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & " Unit \n"
                        Else
                            LastPeriodeRemain = LastPeriodeRemain & "Sisa freedays pada periode " & dPOT.Sequence & " untuk kendaraan " & dPOT.VechileModel.IndDescription & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & " Unit \n"
                        End If
                        sessHelp.SetSession("Warning", LastPeriodeRemain)
                        dFDaysTarget.Remove(dPOT.FreeDays)
                        VarMaxTOP = -1
                        Return -1
                    Else
                        Continue For
                    End If
                Else
                    OverQuantity = True
                    dFDaysTarget.Remove(dPOT.FreeDays)
                    Continue For

                    'If LastPeriodeRemain.Length = 0 Then
                    '    LastPeriodeRemain = "Sisa freedays pada periode " & dPOT.Sequence & " untuk kendaraan " & dPOT.VechileModel.IndDescription & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & " Unit \n"
                    'Else
                    '    LastPeriodeRemain = LastPeriodeRemain & "Sisa freedays pada periode " & dPOT.Sequence & " untuk kendaraan " & dPOT.VechileModel.IndDescription & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & " Unit \n"
                    'End If
                    'sessHelp.SetSession("Warning", LastPeriodeRemain)
                    'dFDaysTarget.Remove(dPOT.FreeDays)
                    'VarMaxTOP = -1
                    'Return -1
                End If
            End If
        Next

        Return _return
    End Function

    Private Function CalculateInterestNonFactoring(ByVal objPOHeader As POHeader, ByVal objPODetail As PODetail)
        Dim rInterest As Decimal = 0
        Dim nTOP As Integer = 0
        Dim nMonth As Integer = 0
        Dim objPriceArrayList As ArrayList = New PriceFacade(User).Retrieve(objPODetail.ContractDetail)
        Dim ItemDeposit As Double = GetItemDeposit(objPODetail.ContractDetail, objPOHeader)
        If objPriceArrayList.Count > 0 Then
            Dim objPrice As Price
            For Each item As Price In objPriceArrayList
                nTOP = objPOHeader.TermOfPayment.TermOfPaymentValue
                nMonth = DateTime.DaysInMonth(objPODetail.ContractDetail.ContractHeader.ContractPeriodYear, objPODetail.ContractDetail.ContractHeader.ContractPeriodMonth)

                If item.ValidFrom <= New DateTime(objPODetail.ContractDetail.ContractHeader.PricePeriodYear,
                                                  objPODetail.ContractDetail.ContractHeader.PricePeriodMonth,
                                                  objPODetail.ContractDetail.ContractHeader.PricePeriodDay) Then
                    objPrice = item
                    Exit For
                End If
            Next
            rInterest = objPODetail.ReqQty * objPODetail.ContractDetail.ContractHeader.PKHeader.FreeIntIndicator *
                                    Calculation.CountInterest(objPODetail.FreeDays, nTOP, nMonth, objPrice.Interest,
                                    objPODetail.ContractDetail.Amount - ItemDeposit, objPrice.PPh23)

        End If
        Return rInterest
    End Function

#End Region

End Class

Public Class PeriodAndQuantity
    Private _PeriodDate As Integer
    Private _AllocationQuantity As Integer

    Public Property PeriodDate As Integer
        Get
            Return _PeriodDate
        End Get
        Set(ByVal value As Integer)
            _PeriodDate = value
        End Set
    End Property

    Public Property AllocationQuantity As Integer
        Get
            Return _AllocationQuantity
        End Get
        Set(ByVal value As Integer)
            _AllocationQuantity = value
        End Set
    End Property

End Class