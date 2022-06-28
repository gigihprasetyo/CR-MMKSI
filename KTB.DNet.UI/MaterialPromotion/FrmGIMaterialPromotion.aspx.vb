#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.MaterialPromotion
Imports Ktb.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
#End Region

Public Class FrmGIMaterialPromotion
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlPeriod As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgAlokasi As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents txtDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealers As System.Web.UI.WebControls.Label
    Protected WithEvents txtRequestNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeBarang As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblKodeBarang As System.Web.UI.WebControls.Label
    Protected WithEvents lblRequest As System.Web.UI.WebControls.Label
    Protected WithEvents lblRequestNo As System.Web.UI.WebControls.Label
    Protected WithEvents btnCariGoodIssue As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private sHelper As SessionHelper = New SessionHelper
    Private totalRow As Integer = 0
    Private arlToDisplay As ArrayList = New ArrayList

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If Not IsPostBack Then
            BindDDl()
            ViewState("SortColumn") = "Dealer.DealerCode"
            ViewState("SortDirection") = Sort.SortDirection.ASC
            lblDealers.Attributes("onclick") = "ShowPPDealerSelection()"
            lblRequestNo.Attributes("onclick") = "ShowPPReqNo();"
            lblKodeBarang.Attributes("onclick") = "ShowMaterialPromotionSelection();"
            dgAlokasi.Columns(10).Visible = False
        End If

    End Sub

    Private Sub BindDDl()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(MaterialPromotionPeriod), "StartDate", Sort.SortDirection.ASC))
        'sortColl.Add(New Sort(GetType(MaterialPromotionPeriod), "StartMonth", Sort.SortDirection.ASC))
        'sortColl.Add(New Sort(GetType(MaterialPromotionPeriod), "EndMonth", Sort.SortDirection.ASC))

        Dim arlPeriod As ArrayList = New MaterialPromotionPeriodFacade(User).Retrieve(criterias, sortColl)

        Dim x As Integer = 0
        For Each item As MaterialPromotionPeriod In arlPeriod
            Dim ddlitem As DropDownList
            ddlPeriod.Items.Add("")
            ddlPeriod.Items(x).Text = item.PeriodName 'item.StartDate.ToString("dd/MM/yyyy") & " - " & item.EndDate.ToString("dd/MM/yyyy")
            ddlPeriod.Items(x).Value = item.ID.ToString
            x = x + 1
        Next
        ddlPeriod.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))

        If ddlPeriod.Items.Count > 0 Then
            ddlPeriod.SelectedIndex = 0
        End If


    End Sub
    Private _IsCari As Boolean = False
    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        If txtDealer.Text = "" And txtRequestNo.Text = "" Then
            MessageBox.Show("Masukkan kode dealer")
            Exit Sub
        End If

        _IsCari = True
        btnSimpan.Enabled = False
        btnCariGoodIssue_Click(Nothing, Nothing)
    End Sub

    Private Sub BindDatagrid(ByVal idxpage As Integer)

        If (idxpage >= 0) Then

            dgAlokasi.DataSource = CommonFunction.PageAndSortArraylist(CType(sHelper.GetSession("arlToDisplay"), ArrayList), idxpage, dgAlokasi.PageSize, GetType(MaterialPromotionAllocation), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))
            dgAlokasi.VirtualItemCount = CType(sHelper.GetSession("arlToDisplay"), ArrayList).Count
            dgAlokasi.DataBind()

            'If dgAlokasi.Items.Count = "0" Then
            '    MessageBox.Show(SR.DataNotFound(""))
            'End If

        End If

    End Sub

    Private Function GetArlToDisplay(ByVal idxPage As Integer) As ArrayList

        Dim arlBarang() As String = CType(sHelper.GetSession("KodeBarang"), String).Split(";".ToCharArray(), 100)


        If txtRequestNo.Text = "" Then
            Dim arlDealer() As String = txtDealer.Text.Split(";".ToCharArray(), 100)

            Dim objPeriod As MaterialPromotionPeriod = New MaterialPromotionPeriodFacade(User).Retrieve(CInt(ddlPeriod.SelectedValue))

            'For Each itemDealer As String In arlDealer
            Dim objdealer As Dealer = New DealerFacade(User).Retrieve(txtDealer.Text)

            If objdealer.ID = 0 Then
                MessageBox.Show("Dealer " & txtDealer.Text & " Tidak valid")
                sHelper.SetSession("ArlToDisplay", New ArrayList)
                Return New ArrayList
            End If

            For Each itemBarang As String In arlBarang

                Dim objBarang As MaterialPromotion = New MaterialPromotionFacade(User).Retrieve(itemBarang)
                If objBarang.ID = 0 Then
                    MessageBox.Show("Kode Barang " & itemBarang & " Tidak Ada")
                    sHelper.SetSession("ArlToDisplay", New ArrayList)
                    Return New ArrayList
                End If
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "Dealer.DealerCode", MatchType.Exact, txtDealer.Text))
                criterias.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "MaterialPromotion.GoodNo", MatchType.Exact, itemBarang))
                If Not _IsCari Then
                    criterias.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "MaterialPromotionPeriod.ID", MatchType.Exact, objPeriod.ID.ToString))
                End If


                Dim arlAlokasi As ArrayList = New MaterialPromotionAllocationFacade(User).Retrieve(criterias)
                Dim objAlokasi As MaterialPromotionAllocation
                If arlAlokasi.Count = 0 Then
                    objAlokasi = New MaterialPromotionAllocation
                    objAlokasi.Dealer = objdealer
                    objAlokasi.MaterialPromotion = objBarang
                    objAlokasi.MaterialPromotionPeriod = objPeriod
                Else
                    objAlokasi = arlAlokasi(0)
                End If

                'Alokasi >0
                'If chkAlokasi.Checked Then
                '    If objAlokasi.Qty > 0 Then
                '        arlToDisplay.Add(objAlokasi)
                '    End If
                'Else
                arlToDisplay.Add(objAlokasi)
                'End If

            Next
            'Next



        Else

            Dim criterias As New CriteriaComposite(New Criteria(GetType(MaterialPromotionRequestDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            criterias.opAnd(New Criteria(GetType(MaterialPromotionRequestDetail), "MaterialPromotionRequest.Status", MatchType.GreaterOrEqual, CInt(EnumMaterialPromotion.MaterialPromotionStatus.Validasi)))

            criterias.opAnd(New Criteria(GetType(MaterialPromotionRequestDetail), "MaterialPromotionRequest.RequestNo", MatchType.InSet, "('" & txtRequestNo.Text.Replace(";", "','") & "')"))

            If txtDealer.Text <> "" Then
                Dim criteriaDealer As New CriteriaComposite(New Criteria(GetType(MaterialPromotionRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriaDealer.opAnd(New Criteria(GetType(MaterialPromotionRequest), "Dealer.DealerCode", MatchType.Exact, txtDealer.Text.Trim()))
                Dim arlIncluded As ArrayList = New MaterialPromotionRequestFacade(User).Retrieve(criteriaDealer)
                Dim IDIncluded As String = ""
                For Each item As MaterialPromotionRequest In arlIncluded
                    IDIncluded = IDIncluded & item.ID.ToString & ","
                Next

                If IDIncluded <> "" Then
                    IDIncluded = Left(IDIncluded, IDIncluded.Length - 1)
                    criterias.opAnd(New Criteria(GetType(MaterialPromotionRequestDetail), "MaterialPromotionRequest.ID", MatchType.InSet, "(" & IDIncluded & ")"))
                Else
                    Return New ArrayList
                End If
            End If

            If txtKodeBarang.Text <> "" Then
                criterias.opAnd(New Criteria(GetType(MaterialPromotionRequestDetail), "MaterialPromotion.GoodNo", MatchType.InSet, "('" & txtKodeBarang.Text.Replace(";", "','") & "')"))
            End If

            Dim arlRequest As ArrayList = New MaterialPromotionRequestDetailFacade(User).Retrieve(criterias)

            For Each itemRequest As MaterialPromotionRequestDetail In arlRequest
                Dim criteriaAlokasi As New CriteriaComposite(New Criteria(GetType(MaterialPromotionAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriaAlokasi.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "MaterialPromotion.ID", MatchType.Exact, itemRequest.MaterialPromotion.ID))
                criteriaAlokasi.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "Dealer.ID", MatchType.Exact, itemRequest.MaterialPromotionRequest.Dealer.ID))
                If Not _IsCari Then
                    criteriaAlokasi.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "MaterialPromotionPeriod.ID", MatchType.Exact, ddlPeriod.SelectedValue))
                End If

                Dim arlalokasi As ArrayList = New MaterialPromotionAllocationFacade(User).Retrieve(criteriaAlokasi)
                Dim objAlokasi As MaterialPromotionAllocation
                If arlalokasi.Count > 0 Then
                    objAlokasi = arlalokasi(0)
                Else
                    objalokasi = New MaterialPromotionAllocation
                    objalokasi.MaterialPromotion = itemRequest.MaterialPromotion
                    objalokasi.MaterialPromotionPeriod = New MaterialPromotionPeriodFacade(User).Retrieve(CInt(ddlPeriod.SelectedValue))
                    objalokasi.Dealer = itemRequest.MaterialPromotionRequest.Dealer
                End If
                objalokasi.TempRequestNo = itemRequest.MaterialPromotionRequest.RequestNo
                objalokasi.TempRequestQty = itemRequest.Qty

                arlToDisplay.Add(objalokasi)
            Next


        End If



        totalRow = arlToDisplay.Count
        sHelper.SetSession("ArlToDisplay", arlToDisplay)

        'arlToDisplay = PagingArraylist(arlToDisplay, idxPage, dgAlokasi.PageSize, ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))
        arlToDisplay = CommonFunction.PageAndSortArraylist(arlToDisplay, idxPage, dgAlokasi.PageSize, GetType(MaterialPromotionAllocation), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))

        Return arlToDisplay



    End Function

    'Private Function PagingArraylist(ByVal ArlToPage As ArrayList, ByVal IdxPage As Integer, ByVal PageSize As Integer, ByVal SortColumn As String, ByVal SortDirection As Sort.SortDirection) As ArrayList


    '    If SortColumn = "Dealer.DealerCode" Then
    '        CommonFunction.SortArraylist(ArlToPage, GetType(MaterialPromotionAllocation), "MaterialPromotion.GoodNo", SortDirection.ASC)

    '    ElseIf SortColumn = "MaterialPromotion.GoodNo" Or SortColumn = "MaterialPromotion.Name" Then
    '        CommonFunction.SortArraylist(ArlToPage, GetType(MaterialPromotionAllocation), "Dealer.DealerCode", SortDirection.ASC)
    '    End If

    '    ArlToPage = CommonFunction.SortArraylist(ArlToPage, GetType(MaterialPromotionAllocation), SortColumn, SortDirection)

    '    ArlToPage = CommonFunction.PageArraylist(ArlToPage, IdxPage, dgAlokasi.PageSize)

    '    Return ArlToPage

    'End Function

    Private Sub bindtogrid(ByVal idxPage As Integer)


    End Sub

    Private Sub txtDealer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDealer.TextChanged

    End Sub



    Private Sub dgAlokasi_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgAlokasi.SortCommand
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
        dgAlokasi.SelectedIndex = -1
        dgAlokasi.CurrentPageIndex = 0
        BindDatagrid(dgAlokasi.CurrentPageIndex)

    End Sub

    Private Sub dgAlokasi_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgAlokasi.PageIndexChanged
        dgAlokasi.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dgAlokasi.CurrentPageIndex)

    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        'Cek Qty
        'Dim arlBarang(dgAlokasi.Items.Count, 3) As String
        'For Each item As DataGridItem In dgAlokasi.Items
        'TODO : CHECK MULTIPLE BARANG IN ONE DEALER, QTYSISA REFRESH
        Dim counter As Integer = 0
        For Each item As DataGridItem In dgAlokasi.Items
            Dim chkPO As CheckBox = item.FindControl("chkPO")
            If chkPO.Checked Then
                counter += 1
                Dim lblDealer As Label = item.FindControl("lblDealer")
                Dim lblRequest As Label = item.FindControl("lblRequest")
                Dim lblGoodsNo As Label = item.FindControl("lblGoodsNo")
                Dim lblQty As Label = item.FindControl("lblQty")
                Dim txtRequestQty As TextBox = item.FindControl("txtRequestQty")
                Dim lblSisa As Label = item.FindControl("lblSisa")

                If Val(txtRequestQty.Text) > Val(lblSisa.Text) Then
                    MessageBox.Show("Qty Good Issue tidak boleh lebih besar dari qty sisa ( KodeBarang " & lblGoodsNo.Text & " )")
                    Exit Sub
                End If
                If Val(txtRequestQty.Text) = 0 Then
                    MessageBox.Show("Masukkan Qty GI terlebih dulu ( KodeBarang " & lblGoodsNo.Text & " )")
                    Exit Sub
                End If
            End If

        Next

        If counter = 0 Then
            MessageBox.Show("Tidak ada data yang dipilih")
            Exit Sub
        End If

        Dim arlGI As ArrayList = New ArrayList
        Dim objPeriod As MaterialPromotionPeriod = New MaterialPromotionPeriodFacade(User).Retrieve(CInt(ddlPeriod.SelectedValue))
        For Each item As DataGridItem In dgAlokasi.Items
            Dim chkPO As CheckBox = item.FindControl("chkPO")

            If chkPO.Checked Then
                Dim lblDealer As Label = item.FindControl("lblDealer")
                Dim lblRequest As Label = item.FindControl("lblRequest")
                Dim lblGoodsNo As Label = item.FindControl("lblGoodsNo")
                Dim lblQty As Label = item.FindControl("lblQty")
                Dim txtRequestQty As TextBox = item.FindControl("txtRequestQty")
                Dim lblSisa As Label = item.FindControl("lblSisa")
                Dim lblKeterangan As Label = item.FindControl("lblKeterangan")
                Dim lblStatusGI As Label = item.FindControl("lblStatusGI")

                Dim objGI As MaterialPromotionGIGR = New MaterialPromotionGIGR
                objGI.Dealer = New DealerFacade(User).Retrieve(lblDealer.Text)
                objGI.MaterialPromotion = New MaterialPromotionFacade(User).Retrieve(lblGoodsNo.Text)
                objGI.MaterialPromotionPeriod = objPeriod
                objGI.Qty = Val(txtRequestQty.Text)
                objGI.RequestNo = lblRequest.Text

                Dim result As Integer = New MaterialPromotionGIGRFacade(User).InsertTransaction(objGI)

                If lblRequest.Text <> "" Then
                    Dim criteriaRequest As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionRequestDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criteriaRequest.opAnd(New Criteria(GetType(MaterialPromotionRequestDetail), "MaterialPromotionRequest.RequestNo", MatchType.Exact, objGI.RequestNo))
                    criteriaRequest.opAnd(New Criteria(GetType(MaterialPromotionRequestDetail), "MaterialPromotion.ID", MatchType.Exact, objGi.MaterialPromotion.ID))

                    Dim facade As MaterialPromotionRequestDetailFacade = New MaterialPromotionRequestDetailFacade(User)
                    Dim objRequestDetail As MaterialPromotionRequestDetail = facade.Retrieve(criteriaRequest)(0)

                    If objRequestDetail.GIQty = 0 Then
                        objRequestDetail.StatusGI = EnumGIStatusMatPromotion.GIStatus.Belum_Good_Issue
                    ElseIf objRequestDetail.GIQty < objRequestDetail.Qty Then
                        objRequestDetail.StatusGI = EnumGIStatusMatPromotion.GIStatus.Partial_Good_Issue
                    ElseIf objRequestDetail.GIQty >= objRequestDetail.Qty Then
                        objRequestDetail.StatusGI = EnumGIStatusMatPromotion.GIStatus.Good_Issue
                    End If


                    Dim resultx As Integer = facade.Update(objRequestDetail)

                    Select Case objRequestDetail.StatusGI
                        Case EnumGIStatusMatPromotion.GIStatus.Belum_Good_Issue
                            lblStatusGI.Text = EnumGIStatusMatPromotion.GIStatus.Belum_Good_Issue.ToString
                        Case EnumGIStatusMatPromotion.GIStatus.Good_Issue
                            lblStatusGI.Text = EnumGIStatusMatPromotion.GIStatus.Good_Issue.ToString
                        Case EnumGIStatusMatPromotion.GIStatus.Partial_Good_Issue
                            lblStatusGI.Text = EnumGIStatusMatPromotion.GIStatus.Partial_Good_Issue.ToString

                    End Select

                    Dim objRequest As MaterialPromotionRequest = objRequestDetail.MaterialPromotionRequest

                    Dim StatusGI As Integer = EnumGIStatusMatPromotion.GIStatus.Belum_Good_Issue
                    Dim counterx As Integer = 0
                    For Each itemDetail As MaterialPromotionRequestDetail In objRequest.MaterialPromotionRequestDetails
                        If itemDetail.Qty > 0 And itemDetail.Qty > itemDetail.GIQty Then
                            StatusGI = EnumGIStatusMatPromotion.GIStatus.Partial_Good_Issue
                            Exit For
                        End If

                        If counterx = objRequest.MaterialPromotionRequestDetails.Count - 1 And itemDetail.Qty = itemDetail.GIQty Then
                            StatusGI = EnumGIStatusMatPromotion.GIStatus.Good_Issue
                            Exit For
                        End If

                        counterx += 1
                    Next
                    objRequest.GIStatus = StatusGI
                    resultx = New MaterialPromotionRequestFacade(User).Update(objRequest)

                End If

                If result > 0 Then
                    Dim objGIInserted As MaterialPromotionGIGR = New MaterialPromotionGIGRFacade(User).Retrieve(result)
                    lblKeterangan.Text = objGIInserted.NoGI

                    Dim criteriaAlokasi As New CriteriaComposite(New Criteria(GetType(MaterialPromotionAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criteriaAlokasi.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "MaterialPromotion.ID", MatchType.Exact, objGIInserted.MaterialPromotion.ID))
                    criteriaAlokasi.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "MaterialPromotionPeriod.ID", MatchType.Exact, objGIInserted.MaterialPromotionPeriod.ID))
                    criteriaAlokasi.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "Dealer.ID", MatchType.Exact, objGIInserted.Dealer.ID))

                    Dim objAlokasi As MaterialPromotionAllocation = New MaterialPromotionAllocationFacade(User).Retrieve(criteriaAlokasi)(0)

                    lblSisa.Text = objAlokasi.QtySisa.ToString


                    chkpo.Checked = False
                    chkpo.Visible = False
                End If

            End If

        Next

        MessageBox.Show(SR.SaveSuccess)


    End Sub

    Private Sub dgAlokasi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgAlokasi.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealer"), Label)
            Dim lblDealerID As Label = CType(e.Item.FindControl("lblDealerID"), Label)
            Dim lblStatusGI As Label = CType(e.Item.FindControl("lblStatusGI"), Label)
            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(lblDealerID.Text)
            Dim lblRequest As Label = CType(e.Item.FindControl("lblRequest"), Label)
            lblDealerCode.ToolTip = objDealer.DealerName


            'If lblRequest.Text <> "" Then

            '    Dim objAlokasi As MaterialPromotionAllocation = e.Item.DataItem
            '    Dim lblRequestQty As Label = CType(e.Item.FindControl("lblRequestQty"), Label)
            '    Dim txtRequestQty As TextBox = CType(e.Item.FindControl("txtRequestQty"), TextBox)

            '    lblRequestQty.Visible = _IsCari
            '    txtRequestQty.Visible = Not _IsCari

            '    Dim criteriaRequest As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionRequestDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '    criteriaRequest.opAnd(New Criteria(GetType(MaterialPromotionRequestDetail), "MaterialPromotionRequest.RequestNo", MatchType.Exact, lblRequest.Text))
            '    criteriaRequest.opAnd(New Criteria(GetType(MaterialPromotionRequestDetail), "MaterialPromotion.ID", MatchType.Exact, objAlokasi.MaterialPromotion.ID))

            '    Dim facade As MaterialPromotionRequestDetailFacade = New MaterialPromotionRequestDetailFacade(User)
            '    Dim objRequestDetail As MaterialPromotionRequestDetail = facade.Retrieve(criteriaRequest)(0)

            '    Select Case objRequestDetail.StatusGI
            '        Case EnumGIStatusMatPromotion.GIStatus.Belum_Good_Issue
            '            lblStatusGI.Text = EnumGIStatusMatPromotion.GIStatus.Belum_Good_Issue.ToString
            '        Case EnumGIStatusMatPromotion.GIStatus.Good_Issue
            '            lblStatusGI.Text = EnumGIStatusMatPromotion.GIStatus.Good_Issue.ToString
            '        Case EnumGIStatusMatPromotion.GIStatus.Partial_Good_Issue
            '            lblStatusGI.Text = EnumGIStatusMatPromotion.GIStatus.Partial_Good_Issue.ToString

            '    End Select


            'End If

        End If
    End Sub

    Private Sub btnCariGoodIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCariGoodIssue.Click
        ViewState("CurrentSortColumn") = "Dealer.DealerCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

        sHelper.SetSession("KodeBarang", txtKodeBarang.Text)
        If txtDealer.Text = "" And txtRequestNo.Text = "" Then
            MessageBox.Show("Masukkan kode dealer")
            Exit Sub
        Else            
            If Not _IsCari Then
                If ddlPeriod.SelectedValue = "-1" Then
                    MessageBox.Show("Periode harus dipilih")
                    Return
                End If

                btnSimpan.Enabled = True
            End If


            arlToDisplay = GetArlToDisplay(0)
            'If arlToDisplay.Count = 0 Then
            '    'MessageBox.Show("Tidak Ada Data yang ditemukan")
            '    'Exit Sub
            'End If
        End If

        BindDatagrid(0)
    End Sub
End Class
