Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.Claim
Imports KTB.DNet.Domain.Search
Imports System.IO

Public Class FrmClaimDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ltrDealerCode As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrDealerName As System.Web.UI.WebControls.Literal
    Protected WithEvents lblTglFaktur As System.Web.UI.WebControls.Label
    Protected WithEvents txtComment As System.Web.UI.WebControls.TextBox
    Protected WithEvents ltrStatus As System.Web.UI.WebControls.Literal
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents fuEvidence As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents ltrNoAndDate As System.Web.UI.WebControls.Literal
    Protected WithEvents dtgEntryClaimEdit As System.Web.UI.WebControls.DataGrid
    Protected WithEvents pnlEdit As System.Web.UI.WebControls.Panel
    Protected WithEvents dgView As System.Web.UI.WebControls.DataGrid
    Protected WithEvents pnlView As System.Web.UI.WebControls.Panel
    Protected WithEvents ltrPenjelasan As System.Web.UI.WebControls.Literal
    Protected WithEvents ddlClaimReasonHeader As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents lblFilename As System.Web.UI.WebControls.Label
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents ltrAlasanClain As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrNoFaktur As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoSO As System.Web.UI.WebControls.Label

    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents FailureText As System.Web.UI.WebControls.Literal
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object



    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()

        Dim objUser As UserInfo = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If objUser.Dealer.Title = "1" Then
            IsKTB = True

        Else
            IsKTB = False
        End If

    End Sub

#End Region

#Region "Custom Variable Declaration"

    Private _arlClaimDetailsAdd As ArrayList = New ArrayList
    Private _arlClaimDetailsUpdate As ArrayList = New ArrayList
    Private _arlClaimDetailsDelete As ArrayList = New ArrayList
    Private IsKTB As Boolean
    Private KTBEdit As Boolean
    Dim sesHelper As SessionHelper = New SessionHelper
    Dim oDealer As Dealer
    Dim SPPo As SparePartPOStatus = New SparePartPOStatus
    Dim SPPoDetails As ArrayList = New ArrayList
    Dim CD As ArrayList = New ArrayList
    Dim ClaimHeaderID As Integer
    Dim View As Boolean
    Dim ViewFromKTB As Boolean

    Dim sum_qtyview As Integer = 0
    Dim sum_qtyapprovedview As Integer = 0
    Dim sum_pricetotalview As Integer = 0

    Private objClaimHeader As ClaimHeader
    Private objClaimDetail As ClaimDetail
    Private Mode As enumMode.Mode
#End Region

#Region "Custom Method"
    Private Sub SetClaimHeaderByInputedData()
        'Dim objCH As ClaimHeader = New ClaimHeader
        'Dim objSparePartPO As SparePartPOStatus = New SparePart.SparePartPOStatusFacade(User).Retrieve(ltrNoFaktur.Text.Trim())
        Dim objSparePartPO As SparePartPOStatus = New SparePart.SparePartPOStatusFacade(User).RetrievePO(ltrNoFaktur.Text.Trim(), lblNoSO.Text)
        'remark by willy masalah claim
        'objClaimHeader.ClaimDate = Convert.ToDateTime(lblTglFaktur.Text)
        objClaimHeader.SparePartPOStatus = objSparePartPO
        objClaimHeader.Description = Server.HtmlEncode(txtComment.Text.Trim())
        objClaimHeader.Status = EnumClaimStatus.ClaimStatus.Baru
        Dim CR As New ClaimReason
        CR = New Claim.ClaimReasonFacade(User).Retrieve(Convert.ToInt32(ddlClaimReasonHeader.SelectedValue))
        objClaimHeader.ClaimReason = CR
        objClaimHeader.Dealer = oDealer

        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimProgress), "Progress", MatchType.Exact, ""))
        objClaimHeader.ClaimProgress = New ClaimProgressFacade(User).Retrieve(crits)(0)
        objClaimHeader.UploadFileName = lblFilename.Text

    End Sub
    Private Sub BindDataToPage()
        If IsNothing(sesHelper.GetSession("PartEntryClaimEdit")) Then
            Dim objClaimHeaderFacade As ClaimHeaderFacade = New ClaimHeaderFacade(User)
            objClaimHeader = objClaimHeaderFacade.Retrieve(Convert.ToInt32(Request.QueryString("ClaimHeaderID")))
            sesHelper.SetSession("PartEntryClaimEdit", objClaimHeader)
        Else
            objClaimHeader = sesHelper.GetSession("PartEntryClaimEdit")
            Mode = ViewState("Mode")
            If (Mode = enumMode.Mode.EditMode) Then
                BindHeaderToForm()
            End If
        End If
        BindDetailToGrid()
    End Sub

    Private Sub BindDetailToGrid()
        objClaimHeader = sesHelper.GetSession("PartEntryClaimEdit")
        dtgEntryClaimEdit.DataSource = objClaimHeader.ClaimDetails
        dtgEntryClaimEdit.DataBind()
    End Sub

    Private Sub BindHeaderToForm()
        objClaimHeader = sesHelper.GetSession("PartEntryClaimEdit")
        ltrNoAndDate.Text = objClaimHeader.ClaimNo
        txtComment.Text = objClaimHeader.Description
        ltrNoFaktur.Text = sesHelper.GetSession("fakturcari")
        lblNoSO.Text = sesHelper.GetSession("fakturSOcari")
    End Sub

    Private Sub SetButtonNewMode()
        btnSave.Enabled = True
    End Sub

    Private Function ValidateItem(ByVal kodeBarang As String, ByVal quantity As String, ByVal keterangan As String, ByVal qtyApproved As String) As Boolean
        Dim objReason As ClaimReason = New ClaimReasonFacade(User).Retrieve(CInt(ddlClaimReasonHeader.SelectedValue))

        If ltrNoFaktur.Text = String.Empty Then
            MessageBox.Show("Error : Silahkan isi nomor faktur terlebih dahulu")
            Return False
            'ElseIf ddlClaimReasonHeader.SelectedIndex = 0 Then
            '    MessageBox.Show("Error : Silahkan pilih alasan terlebih dahulu")
            '    Return False
        ElseIf (Val(quantity) < Val(qtyApproved)) Then
            MessageBox.Show("Quantity Approved tidak boleh lebih besar dari Quantity Claim")
            Return False
        ElseIf objReason.Status = EnumCategoryStatus.CategoryStatus.TidakAktif Then
            MessageBox.Show("Alasan " & ddlClaimReasonHeader.SelectedItem.Text & " tidak aktif. Silahkan pilih alasan yang lain.")
            Return False
        ElseIf (kodeBarang = String.Empty Or quantity = String.Empty Or keterangan = String.Empty) Then
            MessageBox.Show("Error : Nomor Barang, jumlah dan keterangan Tidak boleh Kosong")
            Return False
        Else
            If Not IsKTB Then
                Dim ObjSparePartMaster As SparePartMaster = New SparePartMasterFacade(User).Retrieve(kodeBarang.Trim)
                Dim oSPDetails As ArrayList = GetCurrentSparePartPOStatusDetailBySparePartCode(kodeBarang)

                If (ObjSparePartMaster.ID = 0) Then
                    MessageBox.Show("Error : SparePart Tidak Ditemukan")
                    Return False
                ElseIf oSPDetails.Count = 0 Then
                    MessageBox.Show("Error : SparePart Tidak terdapat di Faktur")
                    Return False
                    'ElseIf ObjSparePartMaster.TypeCode = "I" Or ObjSparePartMaster.TypeCode = "E" Or ObjSparePartMaster.TypeCode = "A" Then
                    '    MessageBox.Show("Error : Sparepart dengan stop mark I, E, A tidak bisa dipesan")
                    '    Return False
                ElseIf Convert.ToInt32(ddlClaimReasonHeader.SelectedValue) <> 3 Then
                    Dim objSparePartPOStatusDetailTmp As SparePartPOStatusDetail = oSPDetails(0)
                    'If (Convert.ToInt32(quantity) > (objSparePartPOStatusDetailTmp.BillingQuantity - objSparePartPOStatusDetailTmp.ClaimedQty)) Then
                    '    MessageBox.Show("Item " & objSparePartPOStatusDetailTmp.SparePartMaster.PartNumber & " : Quantity Claim telah melampaui batas yang diperbolehkan (" & (objSparePartPOStatusDetailTmp.BillingQuantity - objSparePartPOStatusDetailTmp.ClaimedQty) & ")")
                    '    Return False
                    'End If
                End If
            End If
        End If

        'End If
        Return True
    End Function

    Private Function ValidateDuplication(ByVal kodeBarang As String, ByVal Mode As String, ByVal Rowindex As Integer) As Boolean
        Try
            If (Mode = "Add") Then
                For Each item As ClaimDetail In objClaimHeader.ClaimDetails
                    If (item.SparePartPOStatusDetail.SparePartMaster.PartNumber = kodeBarang.Trim) Then
                        MessageBox.Show("Error : Duplikasi Kode Barang")
                        Return False
                    End If
                Next
            Else
                Dim i As Integer = 0
                For Each item As ClaimDetail In objClaimHeader.ClaimDetails
                    If (item.SparePartPOStatusDetail.SparePartMaster.PartNumber = kodeBarang.Trim) Then
                        If i <> Rowindex Then
                            MessageBox.Show("Error : Duplikasi Kode Barang")
                            Return False
                        End If
                    End If
                    i = i + 1
                Next
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show("Error : Part Incedental tidak ditemukan.")
            Return False
        End Try
    End Function

    Private Sub SetDtgClaimItemFooter(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblFooterNomorBarang As Label = CType(e.Item.FindControl("lblFooterNomorBarang"), Label)
        lblFooterNomorBarang.Attributes("onclick") = "ShowPPKodeBarangSelection();"

        Dim drpCondition As DropDownList = CType(e.Item.FindControl("drpConditionFooter"), DropDownList)
        BindCondition(drpCondition)
    End Sub

    Private Sub SetDtgClaimItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblEditNomorBarang As Label = CType(e.Item.FindControl("lblEditNomorBarang"), Label)
        lblEditNomorBarang.Attributes("onclick") = "ShowPPKodeBarangSelection();"

        Dim lblConditionEditID As Label = CType(e.Item.FindControl("lblConditionEditID"), Label)
        Dim ddlCondition As DropDownList = CType(e.Item.FindControl("drpConditionEdit"), DropDownList)
        BindCondition(ddlCondition)
        ddlCondition.SelectedValue = lblConditionEditID.Text

        If IsKTB Then
            Dim txtNoBarang As TextBox = CType(e.Item.FindControl("txtNomorBarangEdit"), TextBox)
            Dim txtQtyClaim As TextBox = CType(e.Item.FindControl("txtQtyClaimEntryEdit"), TextBox)
            Dim txtKeterangan As TextBox = e.Item.FindControl("txtKeteranganEntryEdit")
            Dim drpConditionEdit As DropDownList = e.Item.FindControl("drpConditionEdit")

            txtNoBarang.Enabled = False
            lblEditNomorBarang.Visible = False
            txtQtyClaim.Enabled = False
            txtKeterangan.Enabled = False
            'drpConditionEdit.Enabled = False

            Dim lblStatusEditID As Label = CType(e.Item.FindControl("lblStatusEditID"), Label)
            Dim ddlStatus As DropDownList = CType(e.Item.FindControl("ddlStatus"), DropDownList)
            BindStatusDetail(ddlStatus)
            ddlStatus.SelectedValue = lblStatusEditID.Text
        Else
            ddlCondition.Enabled = False
            Dim txtQtyApproved As TextBox = CType(e.Item.FindControl("txtQtyApprovedEdit"), TextBox)
            txtQtyApproved.Enabled = False
        End If
    End Sub

    Private Function CreateClaimDetail(ByVal kodeBarang As String, ByVal quantity As String, ByVal keterangan As String, ByVal qtyApproved As String, ByRef condition As DropDownList, ByRef status As DropDownList) As ClaimDetail
        Dim objClaimDetailResult As ClaimDetail = New ClaimDetail
        Dim oSPDetails As ArrayList = GetCurrentSparePartPOStatusDetailBySparePartCode(kodeBarang)
        Dim oClaimGood As ClaimGoodCondition = New Claim.ClaimGoodConditionFacade(User).Retrieve(Convert.ToInt32(condition.SelectedValue))
        objClaimDetailResult.ClaimGoodCondition = oClaimGood
        objClaimDetailResult.Keterangan = keterangan
        objClaimDetailResult.Qty = quantity
        objClaimDetailResult.SparePartPOStatusDetail = CType(oSPDetails(0), SparePartPOStatusDetail)
        objClaimDetailResult.ApprovedQty = Convert.ToInt32(qtyApproved)

        If Not status Is Nothing Then
            objClaimDetailResult.StatusDetailKTB = status.SelectedValue
            Select Case objClaimDetailResult.StatusDetailKTB
                Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Baru
                    objClaimDetailResult.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetail.Baru
                Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Ditagih
                    objClaimDetailResult.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetail.Ditagih
                Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Ditolak
                    objClaimDetailResult.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetail.Ditolak
                Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Retur_Asuransi
                    objClaimDetailResult.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetail.Retur
                Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Retur_NonAsuransi
                    objClaimDetailResult.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetail.Retur
                Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Dipenuhi
                    objClaimDetailResult.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetail.Dipenuhi
            End Select
        End If

        Return objClaimDetailResult
    End Function

    Private Function GetCurrentSparePartPOStatusDetailBySparePartCode(ByVal kodeBarang As String) As ArrayList
        Dim oSPDetails As ArrayList
        Dim objUser As UserInfo = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)

        '  SPPo = New SparePartPOStatusFacade(User).Retrieve(ltrNoFaktur.Text)
        SPPo = New SparePartPOStatusFacade(User).RetrievePO(ltrNoFaktur.Text, lblNoSO.Text)
        lblTglFaktur.Text = Format(SPPo.BillingDate, "dd/MM/yyyy")
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "SparePartPOStatus.ID", MatchType.Exact, SPPo.ID.ToString()))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "SparePartPOStatus.SparePartPO.Dealer.ID", MatchType.Exact, objUser.Dealer.ID.ToString()))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "SparePartMaster.PartNumber", MatchType.Exact, kodeBarang))
        oSPDetails = New SparePart.SparePartPOStatusDetailFacade(User).RetrieveActiveList(criterias, "ID", Sort.SortDirection.ASC)
        Return oSPDetails
    End Function

    Sub fillDataDealer(ByVal ID As Integer, ByVal Mode As Boolean)
        Dim CH As ClaimHeader = New ClaimHeader
        CH = New Claim.ClaimHeaderFacade(User).Retrieve(ID)
        ltrDealerCode.Text = String.Format("{0} / {1}", CH.Dealer.DealerCode.ToString(), CH.Dealer.Province.ProvinceName)
        ltrDealerName.Text = CH.Dealer.DealerName
        ltrNoAndDate.Text = String.Format("{0} / {1}", CH.ClaimNo, CH.ClaimDate.ToString("dd/MM/yyyy"))
        ltrNoFaktur.Text = CH.SparePartPOStatus.BillingNumber
        lblNoSO.Text = CH.SparePartPOStatus.SONumber
        lblTglFaktur.Text = CH.SparePartPOStatus.BillingDate.ToString("dd/MM/yyyy")
        ltrPenjelasan.Text = CH.Description
        txtComment.Text = CH.Description
        ddlClaimReasonHeader.SelectedValue = CH.ClaimReason.ID
        ltrAlasanClain.Text = ddlClaimReasonHeader.SelectedItem.Text
        If (CH.Status = EnumClaimStatus.ClaimStatus.Baru) Then
            ltrStatus.Text = "Baru"
        ElseIf CH.Status = EnumClaimStatus.ClaimStatus.Batal Then
            ltrStatus.Text = "Batal"
        ElseIf CH.Status = EnumClaimStatus.ClaimStatus.Dikirim Then
            ltrStatus.Text = "Dikirim"
            'ElseIf CH.Status = EnumClaimStatus.ClaimStatus.Ditolak Then
            '    ltrStatus.Text = "Ditolak"
        ElseIf CH.Status = EnumClaimStatus.ClaimStatus.Proses Then
            ltrStatus.Text = "Proses"
        ElseIf CH.Status = EnumClaimStatus.ClaimStatus.Selesai Then
            ltrStatus.Text = "Selesai"
        ElseIf CH.Status = EnumClaimStatus.ClaimStatus.Complete_Selesai Then
            ltrStatus.Text = "Complete Selesai"
        End If
    End Sub

    Sub FillClaimDetails(ByVal ID As Integer)
        Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.ID", MatchType.Exact, ID.ToString()))
        CD = New Claim.ClaimDetailFacade(User).RetrieveActiveList(criterias2, CType(ViewState("CurrentSortColumnView"), String), CType(ViewState("CurrentSortDirectView"), Sort.SortDirection))
    End Sub

    Sub BindToGrid(ByVal ID As Integer, ByVal Mode As Boolean)
        Dim total As Integer = 0
        FillClaimDetails(ID)
        If IsKTB Then
            dtgEntryClaimEdit.ShowFooter = False
        End If
        If (Mode = False) Then
            btnSave.Visible = True
            btnCancel.Visible = True
            pnlView.Visible = False
            pnlEdit.Visible = True
            ltrPenjelasan.Visible = False
            txtComment.Visible = True
            fuEvidence.Disabled = False
            'SPPo = New SparePart.SparePartPOStatusFacade(User).Retrieve(ltrNOFaktur.Text)

            'Dim criteriax As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criteriax.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.ID", MatchType.Exact, ID.ToString()))

            'Dim arlClaimDetail As ArrayList = New KTB.DNet.BusinessFacade.Claim.ClaimDetailFacade(User).Retrieve(criteriax)


            'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "SparePartPOStatus.ID", MatchType.Exact, SPPo.ID.ToString()))

            'Dim objUser As UserInfo = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)

            'If objUser.Dealer.Title = "1" Then

            '    Dim strToInclude As String = ""
            '    If arlClaimDetail.Count > 0 Then
            '        For Each item As ClaimDetail In arlClaimDetail
            '            strToInclude = strToInclude & item.SparePartPOStatusDetail.ID & ","
            '        Next
            '        strToInclude = Left(strToInclude, strToInclude.Length - 1)
            '    Else
            '        strToInclude = "0"
            '    End If

            '    'Todo Inset
            '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "ID", MatchType.InSet, "(" & strToInclude & ")"))

            'End If
            'SPPoDetails = New SparePart.SparePartPOStatusDetailFacade(User).RetrieveActiveList(criterias, CType(ViewState("CurrentSortColumnEdit"), String), CType(ViewState("CurrentSortDirectEdit"), Sort.SortDirection))
            'dtgEntryClaimEdit.DataSource = SPPoDetails
            'dtgEntryClaimEdit.DataBind()

            BindDataToPage()
        Else
            pnlView.Visible = True
            pnlEdit.Visible = False
            ltrPenjelasan.Visible = True
            'remark by willy 14052008
            'ddlClaimReasonHeader.Enabled = False
            ltrAlasanClain.Visible = True
            If Not ddlClaimReasonHeader.SelectedItem Is Nothing Then
                ltrAlasanClain.Text = ddlClaimReasonHeader.SelectedItem.Text
            End If

            ddlClaimReasonHeader.Visible = False

            txtComment.Visible = False
            'remark by willy 14 05 2008
            'fuEvidence.Disabled = True
            fuEvidence.Visible = False
            btnUpload.Visible = False

            btnSave.Visible = False
            btnCancel.Visible = True
            dgView.DataSource = CD
            dgView.DataBind()
        End If
    End Sub

    Sub BindCondition(ByVal ddl As DropDownList)
        Dim oClaimCondition As ArrayList
        oClaimCondition = New Claim.ClaimGoodConditionFacade(User).RetrieveActiveList()
        ddl.DataTextField = "Condition"
        ddl.DataValueField = "ID"
        ddl.DataSource = oClaimCondition
        ddl.DataBind()
        Dim objConditionBlank As ClaimGoodCondition = New ClaimGoodConditionFacade(User).Retrieve("")
        If objConditionBlank.ID > 0 Then
            ddl.SelectedValue = objConditionBlank.ID
        End If
    End Sub

    Sub BindStatusDetail(ByVal ddl As DropDownList)

        Dim objUser As UserInfo = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)

        Dim oClaimDetailStatus As ArrayList
        If IsKTB Then
            oClaimDetailStatus = New EnumClaimStatusDetail().RetrieveStatusKTB
        Else
            oClaimDetailStatus = New EnumClaimStatusDetail().RetrieveStatus
        End If

        ddl.DataTextField = "NameStatus"
        ddl.DataValueField = "ValStatus"
        ddl.DataSource = oClaimDetailStatus
        ddl.DataBind()
    End Sub

    Sub BindReasonHeader(ByVal ddl As DropDownList)
        Dim oClaimReason As ArrayList
        oClaimReason = New Claim.ClaimReasonFacade(User).RetrieveActiveListHeader()
        ddl.DataTextField = "Reason"
        ddl.DataValueField = "ID"
        ddl.DataSource = oClaimReason
        ddl.DataBind()
    End Sub

    Sub BindReasonDetails(ByVal ddl As DropDownList)
        Dim oClaimReasons As ClaimReason
        Dim oClaimReasonAdd As ArrayList = New ArrayList
        Dim oClaimReason As ArrayList
        oClaimReason = New Claim.ClaimReasonFacade(User).RetrieveActiveListDetail()
        For Each CR As ClaimReason In oClaimReason
            oClaimReasons = New ClaimReason
            oClaimReasons.ID = CR.ID
            oClaimReasons.Reason = CR.Reason & " - " & CR.Prerequisite
            oClaimReasonAdd.Add(oClaimReasons)
        Next
        ddl.DataTextField = "Reason"
        ddl.DataValueField = "ID"
        ddl.DataSource = oClaimReasonAdd
        ddl.DataBind()
    End Sub

    Private Function UpdateClaimHeader() As ClaimHeader
        Dim CH As ClaimHeader = New ClaimHeader
        CH = New Claim.ClaimHeaderFacade(User).Retrieve(ClaimHeaderID)
        CH.Description = Server.HtmlEncode(txtComment.Text.Trim())
        Dim CR As New ClaimReason
        CR = New Claim.ClaimReasonFacade(User).Retrieve(Convert.ToInt32(ddlClaimReasonHeader.SelectedValue))
        CH.ClaimReason = CR
        If sesHelper.GetSession("UploadFile") <> Nothing Then
            Try

                File.Delete(KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("ClaimEvidenceDir") & "\" & CH.UploadFileName.Replace("/", "\"))
                CH.UploadFileName = lblFilename.Text
            Catch ex As Exception
            End Try

            Try
                CH.UploadFileName = lblFilename.Text
            Catch ex As Exception

            End Try
        End If
        Return CH
    End Function

    Private Function SaveToArrayList() As Boolean
        Dim retVal As Boolean = True
        FillClaimDetails(ClaimHeaderID)
        Dim i As Integer = 0

        If IsNothing(sesHelper.GetSession("_arlClaimDetailsDelete")) Then
            _arlClaimDetailsDelete = New ArrayList
        Else
            _arlClaimDetailsDelete = sesHelper.GetSession("_arlClaimDetailsDelete")
        End If

        For Each item As DataGridItem In dtgEntryClaimEdit.Items
            Dim isAdd As Boolean = True
            Dim NoBarang As Label = CType(item.FindControl("lblNoBarang"), Label)
            'Dim drpCondition As DropDownList = CType(item.FindControl("drpCondition"), DropDownList)
            'Dim txtQtyClaim As TextBox = CType(item.FindControl("txtQtyClaim"), TextBox)

            'UnRemark by Ikhsan
            '26 Juni 2008
            'Agar KTB bisa melakukan Edit tanpa EditCommandColumn
            '---------------------------------------------------------------------------------
            Dim txtQtyApproved As TextBox = CType(item.FindControl("txtQtyApproved"), TextBox)
            'txtQtyApproved.ReadOnly = False
            '---------------------------------------------------------------------------------
            Dim drpCondition As Label = CType(item.FindControl("lblConditionID"), Label)

            'Start  :bug;by:dna;on:20100825;for:yurike;remark:error->melibihi 
            Dim txtQtyClaim As Label = CType(item.FindControl("lblQtyClaim"), Label)
            'Dim txtQtyClaim As TextBox = CType(item.FindControl("TxtQtyApproved"), TextBox)
            'End    :bug;by:dna;on:20100825;for:yurike;remark:error->melibihi

            'Remark by Ikhsan
            '26 Juni 2008
            'Agar KTB bisa melakukan Edit tanpa EditCommandColumn
            '------------------------------------------------------------------------------
            'Dim txtQtyApproved As Label = CType(item.FindControl("lblQtyApproved"), Label)
            '------------------------------------------------------------------------------


            Dim txtKeterangan As Label = CType(item.FindControl("lblKeterangan"), Label)

            'ddlStatusEditDtg
            'Dim ddlStatus As DropDownList = CType(item.FindControl("ddlStatus"), DropDownList)
            Dim ddlStatus As DropDownList = CType(item.FindControl("ddlStatusEditDtg"), DropDownList)

            txtQtyClaim.Text = txtQtyClaim.Text.Replace("-", "")
            txtQtyApproved.Text = txtQtyApproved.Text.Replace("-", "")

            If (Val(txtQtyClaim.Text) < Val(txtQtyApproved.Text)) Then
                MessageBox.Show("Item " & i + 1 & " : Quantity Approved tidak boleh lebih besar dari Quantity Claim")
                retVal = False
                Return retVal
            End If

            If (Val(txtQtyClaim.Text) > 0) Then
                Dim objClaimDetails As ClaimDetail = New ClaimDetail
                Dim objSparePartPO As SparePartPOStatus
                Dim objSpMaster As ArrayList

                Dim oClaimGood As ClaimGoodCondition = New Claim.ClaimGoodConditionFacade(User).Retrieve(Convert.ToInt32(drpCondition.Text))

                For Each item2 As ClaimDetail In CD
                    If (item2.SparePartPOStatusDetail.SparePartMaster.PartNumber = NoBarang.Text) Then
                        isAdd = False
                        Exit For
                    End If
                Next

                If (isAdd = True) Then
                    ' objSparePartPO = New SparePart.SparePartPOStatusFacade(User).Retrieve(ltrNoFaktur.Text.Trim())
                    objSparePartPO = New SparePart.SparePartPOStatusFacade(User).RetrievePO(ltrNoFaktur.Text.Trim(), lblNoSO.Text)

                    Dim criteriasMaster As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartMaster), "PartNumber", MatchType.Exact, NoBarang.Text))
                    objSpMaster = New SparePart.SparePartMasterFacade(User).Retrieve(criteriasMaster)

                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "SparePartPOStatus.ID", MatchType.Exact, objSparePartPO.ID))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "SparePartMaster.ID", MatchType.Exact, CType(objSpMaster(0), SparePartMaster).ID))

                    Dim oSPDetails As ArrayList = New SparePart.SparePartPOStatusDetailFacade(User).RetrieveList(criterias)

                    objClaimDetails.Qty = 0
                    objClaimDetails.SparePartPOStatusDetail = oSPDetails(0)
                    objClaimDetails.ClaimGoodCondition = oClaimGood


                    'remark by willy 10 06 2008
                    'If (txtQtyClaim.Text.Trim() <> String.Empty) Then
                    '    If (Convert.ToInt32(txtQtyClaim.Text) > (objClaimDetails.SparePartPOStatusDetail.BillingQuantity - objClaimDetails.SparePartPOStatusDetail.ClaimedQty)) Then
                    '        'MessageBox.Show("Jumlah claim pada record " & i + 1 & " tidak boleh lebih besar dari jumlah quantity sisa " & (objClaimDetails.SparePartPOStatusDetail.BillingQuantity - objClaimDetails.SparePartPOStatusDetail.ClaimedQty) & "")
                    '        MessageBox.Show("Item " & i + 1 & " : Quantity Claim telah melampaui batas yang diperbolehkan")
                    '        retVal = False
                    '        Return retVal
                    '    End If
                    'Else
                    '    MessageBox.Show("Jumlah claim harus diisi")
                    '    retVal = False
                    '    Return retVal
                    'End If

                    objClaimDetails.Qty = txtQtyClaim.Text
                    objClaimDetails.ApprovedQty = Val(txtQtyApproved.Text)
                    objClaimDetails.Keterangan = txtKeterangan.Text

                    If IsKTB Then
                        Dim lblStatusID As Label = CType(item.FindControl("lblStatusID"), Label)
                        'Changed By Ikhsan 26 June 2008
                        objClaimDetails.StatusDetailKTB = lblStatusID.Text
                        Select Case objClaimDetails.StatusDetailKTB
                            Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Baru
                                objClaimDetails.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetail.Baru
                            Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Ditagih
                                objClaimDetails.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetail.Ditagih
                            Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Ditolak
                                objClaimDetails.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetail.Ditolak
                            Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Retur_Asuransi
                                objClaimDetails.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetail.Retur
                            Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Retur_NonAsuransi
                                objClaimDetails.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetail.Retur
                            Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Dipenuhi
                                objClaimDetails.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetail.Dipenuhi
                        End Select
                    Else
                        'Dealer can't change status detail
                        'objClaimDetails.StatusDetail = ddlStatus.SelectedValue
                        'Select Case objClaimDetails.StatusDetail
                        '    Case EnumClaimStatusDetail.ClaimStatusDetail.Baru
                        '        objClaimDetails.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetailKTB.Baru
                        '    Case EnumClaimStatusDetail.ClaimStatusDetail.Ditagih
                        '        objClaimDetails.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetailKTB.Ditagih
                        '    Case EnumClaimStatusDetail.ClaimStatusDetail.Ditolak
                        '        objClaimDetails.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetailKTB.Ditolak
                        '    Case EnumClaimStatusDetail.ClaimStatusDetail.Retur
                        '        objClaimDetails.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetailKTB.Retur_NonAsuransi
                        '        
                        'End Select
                    End If

                    If objClaimDetails.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetail.Ditolak Then
                        objClaimDetails.ApprovedQty = 0
                    End If

                    _arlClaimDetailsAdd.Add(objClaimDetails)
                Else
                    Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ID", MatchType.Exact, Convert.ToInt32(dtgEntryClaimEdit.DataKeys().Item(i))))
                    criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.ID", MatchType.Exact, ClaimHeaderID))

                    Dim oCDetails As ArrayList = New Claim.ClaimDetailFacade(User).Retrieve(criterias2)
                    Dim _CD As ClaimDetail = oCDetails(0)
                    If (txtQtyApproved.Text.Trim() <> String.Empty) Then ' If (txtQtyClaim.Text.Trim() <> String.Empty) Then 
                        If (Convert.ToInt32(txtQtyApproved.Text) > (_CD.SparePartPOStatusDetail.BillingQuantity - _CD.SparePartPOStatusDetail.GetApprovedQty(_CD.ID))) Then 'If (Convert.ToInt32(txtQtyClaim.Text) > (_CD.SparePartPOStatusDetail.BillingQuantity - _CD.SparePartPOStatusDetail.ClaimedQty + _CD.Qty)) Then
                            MessageBox.Show("Jumlah claim pada record " & i + 1 & " tidak boleh lebih besar dari jumlah sisa quantity ")
                            retVal = False
                            Return retVal
                        End If

                        If (Convert.ToInt32(txtQtyApproved.Text) > (_CD.Qty)) Then 'If (Convert.ToInt32(txtQtyClaim.Text) > (_CD.SparePartPOStatusDetail.BillingQuantity - _CD.SparePartPOStatusDetail.ClaimedQty + _CD.Qty)) Then
                            MessageBox.Show("Jumlah claim pada record " & i + 1 & " tidak boleh lebih besar dari pengajuan ")
                            retVal = False
                            Return retVal
                        End If

                    Else
                        MessageBox.Show("Jumlah claim harus diisi")
                        retVal = False
                        Return retVal
                    End If
                    _CD.ApprovedQty = Val(txtQtyApproved.Text)
                    _CD.Qty = txtQtyClaim.Text
                    _CD.ClaimGoodCondition = oClaimGood
                    _CD.Keterangan = txtKeterangan.Text


                    If IsKTB Then
                        Dim lblStatusID As Label = CType(item.FindControl("lblStatusID"), Label)
                        'Changed by Ikhsan, 26 June 2008
                        '_CD.StatusDetailKTB = lblStatusID.Text
                        _CD.StatusDetailKTB = ddlStatus.SelectedItem.Value

                        Dim drpConditionKTB As DropDownList = CType(item.FindControl("drpConditionKTB"), DropDownList)
                        Dim objGoodCondition As ClaimGoodCondition = New ClaimGoodConditionFacade(User).Retrieve(CInt(drpConditionKTB.SelectedValue))
                        _CD.ClaimGoodCondition = objGoodCondition

                        Select Case _CD.StatusDetailKTB
                            Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Baru
                                _CD.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetail.Baru
                            Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Ditagih
                                _CD.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetail.Ditagih
                            Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Ditolak
                                _CD.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetail.Ditolak
                            Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Retur_Asuransi
                                _CD.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetail.Retur
                            Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Retur_NonAsuransi
                                _CD.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetail.Retur
                            Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Dipenuhi
                                _CD.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetail.Dipenuhi
                        End Select
                    Else
                        'Dealer can't change status detail
                        'objClaimDetails.StatusDetail = ddlStatus.SelectedValue
                        'Select Case objClaimDetails.StatusDetail
                        '    Case EnumClaimStatusDetail.ClaimStatusDetail.Baru
                        '        objClaimDetails.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetailKTB.Baru
                        '    Case EnumClaimStatusDetail.ClaimStatusDetail.Ditagih
                        '        objClaimDetails.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetailKTB.Ditagih
                        '    Case EnumClaimStatusDetail.ClaimStatusDetail.Ditolak
                        '        objClaimDetails.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetailKTB.Ditolak
                        '    Case EnumClaimStatusDetail.ClaimStatusDetail.Retur
                        '        objClaimDetails.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetailKTB.Retur_NonAsuransi
                        '        
                        'End Select


                    End If

                    If _CD.StatusDetail = EnumClaimStatusDetail.ClaimStatusDetail.Ditolak Then
                        _CD.ApprovedQty = 0
                    End If

                    _arlClaimDetailsUpdate.Add(_CD)
                End If
            Else
                'For Each item2 As ClaimDetail In CD
                '    If (item2.SparePartPOStatusDetail.SparePartMaster.PartNumber = NoBarang.Text) Then
                '        Dim criterias3 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "SparePartPOStatusDetail.ID", MatchType.Exact, Convert.ToInt32(dtgEntryClaimEdit.DataKeys().Item(i))))
                '        criterias3.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.ID", MatchType.Exact, ClaimHeaderID))

                '        Dim oCDetails As ArrayList = New Claim.ClaimDetailFacade(User).Retrieve(criterias3)
                '        Dim _CDDel As ClaimDetail = oCDetails(0)
                '        _arlClaimDetailsDelete.Add(_CDDel)
                '        Exit For
                '    End If
                'Next
            End If
            i = i + 1
        Next

        Dim ISDealer As Boolean = (CType(Session.Item("DEALER"), Dealer).Title = CType(EnumDealerTittle.DealerTittle.DEALER, String))

        If ISDealer AndAlso Convert.ToInt32(ddlClaimReasonHeader.SelectedValue) <> 3 Then
            Dim OBjHeader As ClaimHeader = New ClaimHeaderFacade(User).Retrieve(ClaimHeaderID)

            Dim strHtml As String = String.Empty
            Dim arrCombine As New ArrayList

            For Each cld As ClaimDetail In _arlClaimDetailsAdd
                cld.ClaimHeader = OBjHeader
                arrCombine.Add(cld)
            Next

            For Each cld As ClaimDetail In _arlClaimDetailsUpdate
                cld.ClaimHeader = OBjHeader
                arrCombine.Add(cld)
            Next

            If Not isDetailOk(arrCombine, strHtml) Then
                ErrorMessage.Visible = True
                FailureText.Text = strHtml
                Return False
            Else
                ErrorMessage.Visible = False
            End If
        End If

        Return retVal
    End Function

    Private Sub GetFileClaimHeader(ByVal status As Boolean)
        Dim ext As String = ""
        Dim Rnd As Random = New Random
        Dim RndVal As String = Rnd.Next()
        Dim fileName As String = String.Empty
        fileName = DateTime.Now.ToString("ddMMyyyyHHmmss") & RndVal & "\" & Path.GetFileName(fuEvidence.PostedFile.FileName)
        fileName = String.Format("{0}{1}{2}", fileName, "", ext)


        If fileName <> "" OrElse fileName <> Nothing Then
            'cek filesize first
            Dim maxFileSize As Integer = CDbl(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))
            If fuEvidence.PostedFile.ContentLength > maxFileSize Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                Exit Sub
            Else
                'Dim SrcFile As String = Path.GetFileName(fuEvidence.PostedFile.FileName)  '-- Source file name
                Dim DestFile As String
                If status Then
                    DestFile = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("ClaimEvidenceDir") & "\" & lblFilename.Text   '-- Destination file
                Else
                    DestFile = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "DataTemp\CE" & "\" & fileName   '-- Destination file
                End If
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False
                Dim finfo As New FileInfo(DestFile)
                Try
                    success = imp.Start()
                    If success Then
                        If Not finfo.Directory.Exists Then
                            Directory.CreateDirectory(finfo.DirectoryName)
                        End If
                        If sesHelper.GetSession("UploadFile") Is Nothing Then
                            fuEvidence.PostedFile.SaveAs(DestFile)
                            sesHelper.SetSession("UploadFile", DestFile)
                            lblFilename.Text = fileName
                        Else
                            Dim old As String = CType(sesHelper.GetSession("UploadFile"), String)
                            If status Then
                                File.Copy(old, DestFile)
                                File.Delete(CType(sesHelper.GetSession("UploadFile"), String))
                                Directory.Delete(Path.GetDirectoryName(old))
                            Else
                                fuEvidence.PostedFile.SaveAs(DestFile)
                                File.Delete(CType(sesHelper.GetSession("UploadFile"), String))
                                Directory.Delete(Path.GetDirectoryName(old))
                                sesHelper.SetSession("UploadFile", DestFile)
                                lblFilename.Text = fileName
                            End If

                        End If

                        imp.StopImpersonate()
                        imp = Nothing
                    End If
                Catch ex As Exception
                    Throw ex
                    Exit Sub
                End Try
            End If
        End If
    End Sub

    Dim sumQtyClaim As Integer = 0
    Dim sumTotal As Decimal = 0D
    Dim sumQtyApproved As Integer = 0



    Private Function isDetailOk(ByVal arrDetail As ArrayList, ByRef strHTML As String) As Boolean

        Dim isValid As Boolean = True
        Dim strTempalte As String = "<ul>{0}</ul>"
        strHTML = ""
        For Each Cd As ClaimDetail In arrDetail
            Dim qtyBill As Integer = Cd.SparePartPOStatusDetail.BillingQuantity
            If Cd.Qty > qtyBill Then

                strHTML = strHTML & "<li> " & Cd.SparePartPOStatusDetail.SparePartMaster.PartNumber & " melebihi SO  </li>"

                isValid = False
                Continue For
            End If
            Dim intClaim As Integer = Cd.SparePartPOStatusDetail.GetClaimedQty(Cd.ClaimHeader.ID)
            If (qtyBill - intClaim) < Cd.Qty Then

                Dim arr As New ArrayList
                arr = Cd.SparePartPOStatusDetail.GetClaimedDetail(Cd.ClaimHeader.ID)
                strHTML = strHTML & "<li> Sisa Qty : " & (qtyBill - intClaim).ToString & " & " & Cd.SparePartPOStatusDetail.SparePartMaster.PartNumber & " sudah diajukan di {0}  </li>"
                Dim strClaim As String = "<ul>"

                For Each cdd As ClaimDetail In arr
                    strClaim = strClaim & "<li>" & cdd.ClaimHeader.ClaimNo & " </li> "
                Next
                strClaim = strClaim & "</ul>"
                strHTML = String.Format(strHTML, strClaim)
                isValid = False
                Continue For
            End If
        Next

        If Not isValid Then
            strHTML = String.Format(strTempalte, strHTML)
        End If

        Return isValid
    End Function
#End Region

#Region "Event Handlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ClaimHeaderID = Request.QueryString("ClaimHeaderID")
        ViewFromKTB = CType(Request.QueryString("ViewKTB"), Boolean)
        View = CType(Request.QueryString("View"), Boolean)
        If Not IsPostBack Then
            sesHelper.RemoveSession("PartEntryClaimEdit")
            If (View = True) Then
                ViewState("currSortColumnView") = "SparePartPOStatusDetail.SparePartMaster.PartNumber"
                ViewState("currSortDirectionView") = Sort.SortDirection.ASC
            Else
                ViewState("currSortColumnEdit") = "SparePartMaster.PartNumber"
                ViewState("currSortDirectionEdit") = Sort.SortDirection.ASC
            End If
            BindReasonHeader(ddlClaimReasonHeader)
            fillDataDealer(ClaimHeaderID, View)
            BindToGrid(ClaimHeaderID, View)

            If IsKTB Then
                dtgEntryClaimEdit.Columns(7).Visible = False
                dgView.Columns(8).Visible = False
            End If

            If ViewFromKTB Then
                ltrAlasanClain.Visible = True
                If Not ddlClaimReasonHeader.SelectedItem Is Nothing Then
                    ltrAlasanClain.Text = ddlClaimReasonHeader.SelectedItem.Text
                End If
                ddlClaimReasonHeader.Visible = False
                txtComment.ReadOnly = True
                fuEvidence.Visible = False
                btnUpload.Visible = False

            Else
                Dim objHeader As ClaimHeader = New Claim.ClaimHeaderFacade(User).Retrieve(CInt(ClaimHeaderID))
                If objHeader.Status = EnumClaimStatus.ClaimStatus.Selesai Or objHeader.Status = EnumClaimStatus.ClaimStatus.Complete_Selesai Then
                    dgView.Columns(9).Visible = True
                    dtgEntryClaimEdit.Columns(9).Visible = True
                Else
                    If (sesHelper.GetSession("arrival") = "true") Then
                        txtComment.ReadOnly = True
                        fuEvidence.Visible = False
                        btnUpload.Visible = False
                        ltrAlasanClain.Visible = True
                        If Not ddlClaimReasonHeader.SelectedItem Is Nothing Then
                            ltrAlasanClain.Text = ddlClaimReasonHeader.SelectedItem.Text
                        End If
                        ddlClaimReasonHeader.Visible = False
                    End If
                    dgView.Columns(9).Visible = False
                    dtgEntryClaimEdit.Columns(9).Visible = False
                End If
            End If

            If Request.QueryString("isTerima") <> String.Empty Then
                If Request.QueryString("isTerima") = "1" Then
                    ltrAlasanClain.Visible = True
                    If Not ddlClaimReasonHeader.SelectedItem Is Nothing Then
                        ltrAlasanClain.Text = ddlClaimReasonHeader.SelectedItem.Text
                    End If
                    ddlClaimReasonHeader.Visible = False
                    txtComment.ReadOnly = True
                    btnUpload.Visible = False
                    fuEvidence.Visible = False
                End If
            End If
        End If
    End Sub

    Private Sub dtgEntryClaimEdit_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEntryClaimEdit.ItemDataBound
        If e.Item.ItemType = ListItemType.Footer Then
            SetDtgClaimItemFooter(e)
        ElseIf e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            SetDtgClaimItemEdit(e)
        Else
            If (e.Item.ItemIndex >= 0) Then

                Dim drpConditionKTB As DropDownList = CType(e.Item.FindControl("drpConditionKTB"), DropDownList)
                drpConditionKTB.Enabled = False
                If IsKTB Then

                    Dim claimDetailRow As ClaimDetail = CType(e.Item.DataItem, ClaimDetail)
                    Dim lblStatusID As Label = CType(e.Item.FindControl("lblStatusID"), Label)
                    'Change By Ikhsan 26 June 2008
                    Dim ddlStatusEditDtg As DropDownList = CType(e.Item.FindControl("ddlStatusEditDtg"), DropDownList)
                    'Dim ddlStatus As DropDownList = CType(e.Item.FindControl("ddlStatusEditDtg"), DropDownList)
                    ' Ditambahkan button event method untuk delete, sesuai dengan permintaan Rina
                    ' Agar yang bisa melakukan delete dibatasi hanya user dealer
                    ' 24 Juni 2008
                    '---------------------------------------------------------------------------------
                    Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                    '---------------------------------------------------------------------------------

                    BindCondition(drpConditionKTB)
                    drpConditionKTB.SelectedValue = claimDetailRow.ClaimGoodCondition.ID.ToString

                    Dim txtQtyApproved As TextBox = CType(e.Item.FindControl("txtQtyApproved"), TextBox)
                    Dim txtClaimDtlID As Label = CType(e.Item.FindControl("lblClaimDtlID"), Label)

                    txtQtyApproved.ReadOnly = False

                    'Ikhsan 30 Juni 2008, Untuk memastikan bahwa setelah di save oleh User KTb, Grid dapat di refresh dengan data terbaru
                    '--------------------------------------------------------------------------------------------------------------------
                    If KTBEdit Then
                        objClaimHeader = sesHelper.GetSession("PartEntryClaimEdit")
                        'lblStatusID.Text = objClaimHeader.StatusKTB.ToString
                        objClaimDetail = New ClaimDetailFacade(User).Retrieve(CInt(txtClaimDtlID.Text.ToString))
                        'Dim txtQtyApproved As TextBox = CType(dtgEntryClaimEdit.FindControl("txtQtyApproved"), TextBox)
                        txtQtyApproved.Text = objClaimDetail.ApprovedQty.ToString
                    End If
                    '--------------------------------------------------------------------------------------------------------------------


                    'lblStatusID.Text = objClaimHeader.StatusKTB.ToString
                    lblStatusID.Text = claimDetailRow.StatusDetailKTB.ToString
                    BindStatusDetail(ddlStatusEditDtg)
                    ddlStatusEditDtg.SelectedValue = lblStatusID.Text

                    'UnRemark by Ikhsan
                    '26 Juni 2008
                    'Agar KTB bisa melakukan Edit tanpa EditCommandColumn
                    '---------------------------------------------------------------------------------

                    'If IsNothing(objClaimDetail) Then
                    '    objClaimDetail = New ClaimDetailFacade(User).Retrieve(CInt(ClaimHeaderID))
                    'End If

                    ' Dipindahkan ke atas sebelum IF KTBEdit, by Ikhsan 30 Juni 2008
                    '----------------------------------------------------------------------------------
                    'Dim txtQtyApproved As TextBox = CType(e.Item.FindControl("txtQtyApproved"), TextBox)
                    'txtQtyApproved.ReadOnly = False
                    'txtQtyApproved.Text = objClaimDetail.ApprovedQty.ToString
                    '---------------------------------------------------------------------------------




                    ' rubah statenya ke true untuk KTB biar mudah untuk editnya
                    ' 24 Juni 2008
                    'ddlStatusEditDtg.Enabled = False
                    '---------------------------------------------------------------------------------
                    ddlStatusEditDtg.Enabled = True
                    drpConditionKTB.Enabled = True
                    '---------------------------------------------------------------------------------

                    ' Ditambahkan button event method untuk delete, sesuai dengan permintaan Rina
                    ' Agar yang bisa melakukan delete dibatasi hanya user dealer
                    ' 24 Juni 2008
                    '---------------------------------------------------------------------------------
                    lbtnDelete.Visible = False
                    '---------------------------------------------------------------------------------

                    'Ditambahkan biar user KTB bisa melakukan Editing tanpa adanya EditCommandColumn
                    'Dan menyembunyikan control2 tersebut.
                    '---------------------------------------------------------------------------------
                    dtgEntryClaimEdit.Columns(10).Visible = False
                    dtgEntryClaimEdit.Columns(11).Visible = False
                    dtgEntryClaimEdit.Columns(12).Visible = False
                    'txtQtyApproved.Enabled = True
                    '---------------------------------------------------------------------------------


                End If



            End If
        End If


        If (e.Item.ItemIndex >= 0) Then
            'Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            'e.Item.Cells(0).Controls.Add(lNum)
            'Dim Sp As SparePartPOStatusDetail = CType(e.Item.DataItem, SparePartPOStatusDetail)
            Dim txtQtyClaim As TextBox = CType(e.Item.FindControl("txtQtyClaim"), TextBox)
            Dim lblHargaSatuan As Label = CType(e.Item.FindControl("lblHargaSatuan"), Label)
            Dim lblTotal As Label = CType(e.Item.FindControl("lblTotal"), Label)
            'Dikomen oleh Ikhsan 30 Juni 2008, Untuk Menyimpan ClaimDetailID.
            Dim lblClaimDtlID As Label = CType(e.Item.FindControl("lblClaimDtlID"), Label)
            'dikomen oleh Ikhsan, 30 Juni 2008
            'Biar tidak terlalu banyak deklarasi variable dengan nama dan object yang sama
            '------------------------------------------------------------------------------------
            Dim txtQtyApproved As TextBox = CType(e.Item.FindControl("txtQtyApproved"), TextBox)
            '------------------------------------------------------------------------------------
            Dim txtKeterangan As TextBox = e.Item.FindControl("txtKeterangan")

            Dim objUser As UserInfo = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)

            If objUser.Dealer.Title = "1" Then
                'txtKeterangan.Enabled = False

                If View Then
                    'drpCondition.Enabled = False
                    'ddlStatus.Enabled = False
                Else
                    If Request.QueryString("isTerima") <> String.Empty Then
                        If Request.QueryString("isTerima") = "1" Then
                            'drpCondition.Enabled = True
                            'ddlStatus.Enabled = False
                        Else
                            'drpCondition.Enabled = False
                            'ddlStatus.Enabled = True
                        End If
                    Else
                        'drpCondition.Enabled = False
                        'ddlStatus.Enabled = True
                    End If

                End If
            Else
                'ddlStatus.Enabled = False
            End If

            'Bug 707    
            'If objUser.Dealer.Title <> "1" And drpCondition.SelectedItem.Text = "" And drpCondition.Enabled = False Then
            '    drpCondition.Visible = False
            'End If
        End If

        'If e.Item.ItemType = ListItemType.Footer Then
        '    Dim lblSumQtyClaim As Label = CType(e.Item.FindControl("lblSumQtyClaim"), Label)
        '    Dim lblSumTotal As Label = CType(e.Item.FindControl("lblSumTotal"), Label)
        '    Dim lblSumQtyApproved As Label = CType(e.Item.FindControl("lblSumQtyApproved"), Label)
        '    lblSumQtyClaim.Text = sumQtyClaim
        '    lblSumTotal.Text = sumTotal.ToString("#,###")
        '    lblSumQtyApproved.Text = sumQtyApproved
        'End If
    End Sub

    Private Sub dtgEntryClaimEdit_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEntryClaimEdit.SortCommand
        If CType(ViewState("CurrentSortColumnEdit"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirectEdit"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirectEdit") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirectEdit") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumnEdit") = e.SortExpression
            ViewState("CurrentSortDirectEdit") = Sort.SortDirection.ASC
        End If
        BindToGrid(ClaimHeaderID, View)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        KTBEdit = True
        If (SaveToArrayList() = True) Then

            Dim nResult As Integer
            Try
                If sesHelper.GetSession("UploadFile") <> Nothing Then
                    GetFileClaimHeader(True)
                End If
                Dim ObjClaimHeader As ClaimHeader = UpdateClaimHeader()
                If ObjClaimHeader Is Nothing Then
                    MessageBox.Show("Gagal simpan claim")
                    Return
                Else
                    Dim objReason As ClaimReason = New ClaimReasonFacade(User).Retrieve(CInt(ddlClaimReasonHeader.SelectedValue))

                    If objReason.IsMandatoryUpload = 1 And (fuEvidence.Value = "" Or fuEvidence.Value = Nothing) And ObjClaimHeader.UploadFileName = "" Then
                        MessageBox.Show("Alasan " & ddlClaimReasonHeader.SelectedItem.Text & " harus menyertakan upload file evidence")
                        Exit Sub
                    End If

                    If ObjClaimHeader.StatusKTB <> EnumClaimProgress.ClaimProgressKTB.Complete_Selesai Then
                        Dim IsCompleteSelesaiOK As Boolean = IIf(ObjClaimHeader.FakturRetur.Trim <> "" AndAlso ObjClaimHeader.StatusKTB = EnumClaimProgress.ClaimProgressKTB.Selesai, True, False) ' IsCompleteSelesai(_arlClaimDetailsAdd, _arlClaimDetailsUpdate)
                        If IsCompleteSelesaiOK Then
                            ObjClaimHeader.StatusKTB = EnumClaimProgress.ClaimProgressKTB.Complete_Selesai
                            ObjClaimHeader.Status = EnumClaimStatus.ClaimStatus.Complete_Selesai
                        End If
                    End If

                    nResult = New Claim.ClaimDetailFacade(User).InsertUpdateDeleteClaimHeaderDetail(ObjClaimHeader, _arlClaimDetailsAdd, _arlClaimDetailsUpdate, _arlClaimDetailsDelete)
                    sesHelper.RemoveSession("PartEntryClaimEdit")
                End If
            Catch ex As Exception
                MessageBox.Show("Gagal simpan claim " & ex.Message)
                Return
            End Try
            If nResult <> -1 Then
                MessageBox.Show(SR.UpdateSucces)
                BindToGrid(ClaimHeaderID, View)
                sesHelper.SetSession("UploadFile", Nothing)
                lblFilename.Text = ""
                'If IsKTB Then
                '    ObjClaimHeader = sesHelper.GetSession("PartEntryClaimEdit")
                '    Dim ddlStatusEditDtg As DropDownList = CType(dtgEntryClaimEdit.FindControl("ddlStatusEditDtg"), DropDownList)
                '    'ddlStatusEditDtg.SelectedValue = ObjClaimHeader.StatusKTB.ToString
                '    'Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                '    'crits.opAnd(New Criteria(GetType(ClaimDetail), "ClaimHeaderID", MatchType.Exact, ClaimHeaderID))
                '    objClaimDetail = New ClaimDetailFacade(User).Retrieve(CInt(ClaimHeaderID))
                '    Dim txtQtyApproved As TextBox = CType(dtgEntryClaimEdit.FindControl("txtQtyApproved"), TextBox)
                '    txtQtyApproved.Text = objClaimDetail.ApprovedQty.ToString
                'End If
            Else
                MessageBox.Show(SR.UpdateFail)
            End If
        End If
        KTBEdit = False

    End Sub

    Private Function IsCompleteSelesai(ByVal _arlClaimDetailsAdd As ArrayList, ByVal _arlClaimDetailsUpdate As ArrayList) As Boolean
        Dim Result As Boolean = True
        If IsKTB Then
            For Each item As ClaimDetail In _arlClaimDetailsAdd
                If item.StatusDetailKTB <> EnumClaimStatusDetail.ClaimStatusDetailKTB.Dipenuhi _
                    And item.StatusDetailKTB <> EnumClaimStatusDetail.ClaimStatusDetailKTB.Ditolak Then
                    Result = False
                    Exit For
                End If
            Next

            For Each item As ClaimDetail In _arlClaimDetailsUpdate
                If item.StatusDetailKTB <> EnumClaimStatusDetail.ClaimStatusDetailKTB.Dipenuhi _
                    And item.StatusDetailKTB <> EnumClaimStatusDetail.ClaimStatusDetailKTB.Ditolak Then
                    Result = False
                    Exit For
                End If
            Next
        End If

        Return Result

    End Function
    Private Sub dgView_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgView.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            e.Item.Cells(0).Controls.Add(lNum)
            Dim lblStatus As Label = e.Item.FindControl("lblStatus")
            Dim objDetail As ClaimDetail = e.Item.DataItem

            If IsKTB Then
                Select Case objDetail.StatusDetailKTB
                    Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Baru
                        lblStatus.Text = EnumClaimStatusDetail.ClaimStatusDetailKTB.Baru.ToString
                    Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Ditolak
                        lblStatus.Text = EnumClaimStatusDetail.ClaimStatusDetailKTB.Ditolak.ToString
                    Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Retur_Asuransi
                        lblStatus.Text = EnumClaimStatusDetail.ClaimStatusDetailKTB.Retur_Asuransi.ToString
                    Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Retur_NonAsuransi
                        lblStatus.Text = EnumClaimStatusDetail.ClaimStatusDetailKTB.Retur_NonAsuransi.ToString
                    Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Ditagih
                        lblStatus.Text = EnumClaimStatusDetail.ClaimStatusDetailKTB.Ditagih.ToString
                    Case EnumClaimStatusDetail.ClaimStatusDetailKTB.Dipenuhi
                        lblStatus.Text = EnumClaimStatusDetail.ClaimStatusDetailKTB.Dipenuhi.ToString
                End Select

            Else
                Select Case objDetail.StatusDetail
                    Case EnumClaimStatusDetail.ClaimStatusDetail.Baru
                        lblStatus.Text = EnumClaimStatusDetail.ClaimStatusDetail.Baru.ToString
                    Case EnumClaimStatusDetail.ClaimStatusDetail.Ditolak
                        lblStatus.Text = EnumClaimStatusDetail.ClaimStatusDetail.Ditolak.ToString
                    Case EnumClaimStatusDetail.ClaimStatusDetail.Retur
                        lblStatus.Text = EnumClaimStatusDetail.ClaimStatusDetail.Retur.ToString
                    Case EnumClaimStatusDetail.ClaimStatusDetail.Ditagih
                        lblStatus.Text = EnumClaimStatusDetail.ClaimStatusDetail.Ditagih.ToString
                    Case EnumClaimStatusDetail.ClaimStatusDetail.Dipenuhi
                        lblStatus.Text = EnumClaimStatusDetail.ClaimStatusDetail.Dipenuhi.ToString
                End Select

            End If


            'If objDetail.ClaimHeader.Status = EnumClaimStatus.ClaimStatus.Selesai Then
            '    lblStatus.Visible = True
            'Else
            '    lblStatus.Visible = False
            'End If

            sum_qtyview += objDetail.Qty
            sum_qtyapprovedview += objDetail.ApprovedQty

            If objDetail.StatusDetail <> EnumClaimStatusDetail.ClaimStatusDetail.Ditolak Then
                sum_pricetotalview += (objDetail.ApprovedQty * objDetail.SparePartPOStatusDetail.ClaimPriceUnit)
            End If

        End If

        If e.Item.ItemType = ListItemType.Footer Then
            Dim lblSumQtyView As Label = e.Item.FindControl("lblSumQtyView")
            Dim lblSumTotalView As Label = e.Item.FindControl("lblSumTotalView")
            Dim lblSumQtyApprovedView As Label = e.Item.FindControl("lblSumQtyApprovedView")
            lblSumQtyView.Text = sum_qtyview.ToString
            lblSumTotalView.Text = sum_pricetotalview.ToString("#,##0")
            lblSumQtyApprovedView.Text = sum_qtyapprovedview.ToString
        End If

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If ViewFromKTB = False Then
            If (sesHelper.GetSession("arrival") = "true") Then
                sesHelper.SetSession("arrival", "false")
                Response.Redirect("../Claim/FrmClaimArrival.aspx")
            End If
            Response.Redirect("../Claim/FrmListClaim.aspx", True)
        Else
            sesHelper.SetSession("RefreshData", "Yes")
            Response.Redirect("../Claim/FrmClaimList.aspx?isBack=1", True)
        End If
    End Sub

    Private Sub dgView_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgView.SortCommand
        If CType(ViewState("CurrentSortColumnView"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirectView"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirectView") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirectView") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumnView") = e.SortExpression
            ViewState("CurrentSortDirectView") = Sort.SortDirection.ASC
        End If
        BindToGrid(ClaimHeaderID, View)
    End Sub

    Private Sub dtgEntryClaim_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEntryClaimEdit.ItemCommand
        objClaimHeader = sesHelper.GetSession("PartEntryClaimEdit")
        Dim objClaimDetailFacade As ClaimDetailFacade
        Select Case (e.CommandName)
            Case "Delete"
                Dim lbl1 As Label = e.Item.Cells(0).FindControl("lblNo")
                Mode = ViewState("Mode")
                If (Mode = enumMode.Mode.EditMode) Then
                    'If (CType(objClaimHeader.StatusKTB, Short) = CType(PartIncidentalStatus.PartIncidentalKTBStatusEnum.Baru, Short)) Then
                    If (objClaimHeader.ClaimDetails.Count <> 1) Then
                        objClaimDetailFacade = New ClaimDetailFacade(User)
                        objClaimDetailFacade.DeleteFromDB(objClaimHeader.ClaimDetails.Item(CType(lbl1.Text, Integer) - 1))
                        'Else
                        '    MessageBox.Show("Permintaan Khusus Harus memiliki minimal 1 Detail")
                        '    Exit Sub
                        'End If
                    Else
                        MessageBox.Show("Status Permintaan Bukan Baru")
                        Exit Sub
                    End If
                End If

                If objClaimHeader.ClaimDetails.Count() <= 1 Then
                    MessageBox.Show("Pengajuan claim minimal memiliki 1 claim data")
                    Exit Sub
                End If

                'add by willy
                If IsNothing(sesHelper.GetSession("_arlClaimDetailsDelete")) Then
                    _arlClaimDetailsDelete = New ArrayList
                Else
                    _arlClaimDetailsDelete = sesHelper.GetSession("_arlClaimDetailsDelete")
                End If
                _arlClaimDetailsDelete.Add(objClaimHeader.ClaimDetails.Item(CType(lbl1.Text, Integer) - 1))
                sesHelper.SetSession("_arlClaimDetailsDelete", _arlClaimDetailsDelete)

                objClaimHeader.ClaimDetails.Remove(objClaimHeader.ClaimDetails.Item(CType(lbl1.Text, Integer) - 1))
                sesHelper.SetSession("PartEntryClaimEdit", objClaimHeader)
                BindDataToPage()
                Mode = ViewState("Mode")
                If objClaimHeader.ClaimDetails.Count = 0 And Mode = enumMode.Mode.NewItemMode Then
                    SetButtonNewMode()
                End If
                If IsKTB Then
                    dtgEntryClaimEdit.ShowFooter = False
                Else
                    dtgEntryClaimEdit.ShowFooter = True
                End If

            Case "Add"
                If Not Page.IsValid Then
                    Return
                End If
                Dim txt1 As TextBox = e.Item.FindControl("txtFooterNomorBarang")
                Dim txt2 As TextBox = e.Item.FindControl("txtQtyClaimEntry")
                Dim txt3 As TextBox = e.Item.FindControl("txtKeteranganEntry")
                'Dim txtQtyApproved As TextBox = e.Item.FindControl("txtQtyApproved")
                Dim ddlCondition As DropDownList = e.Item.FindControl("drpConditionFooter")
                Dim ddlStatus As DropDownList = e.Item.FindControl("ddlStatus")
                If (ValidateDuplication(txt1.Text.ToUpper, "Add", -1) AndAlso ValidateItem(txt1.Text, txt2.Text, txt3.Text, 0)) Then
                    objClaimDetail = CreateClaimDetail(txt1.Text, txt2.Text, txt3.Text, 0, ddlCondition, ddlStatus)
                    Mode = ViewState("Mode")
                    If (Mode = enumMode.Mode.EditMode) Then
                        objClaimDetailFacade = New ClaimDetailFacade(User)
                        objClaimDetail.ClaimHeader = objClaimHeader
                        objClaimDetailFacade.Insert(objClaimDetail)
                    End If
                Else
                    Exit Sub
                End If
                'add by willy, ga tau enaknya taro dmana
                ddlClaimReasonHeader.Enabled = False

                objClaimHeader.ClaimDetails.Add(objClaimDetail)
                BindDataToPage()
        End Select
    End Sub

    Private Sub dtgEntryClaim_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEntryClaimEdit.EditCommand
        dtgEntryClaimEdit.EditItemIndex = CInt(e.Item.ItemIndex)
        dtgEntryClaimEdit.ShowFooter = False
        btnSave.Enabled = False
        BindDetailToGrid()
    End Sub

    Private Sub dtgEntryClaim_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEntryClaimEdit.CancelCommand
        dtgEntryClaimEdit.EditItemIndex = -1
        BindDetailToGrid()
        btnSave.Enabled = True
        If Not IsKTB Then
            dtgEntryClaimEdit.ShowFooter = True
        End If
    End Sub

    Private Sub dtgEntryClaim_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEntryClaimEdit.UpdateCommand
        objClaimHeader = sesHelper.GetSession("PartEntryClaimEdit")
        Dim txt1 As TextBox = e.Item.FindControl("txtNomorBarangEdit")
        Dim txt2 As TextBox = e.Item.FindControl("txtQtyClaimEntryEdit")
        Dim txt3 As TextBox = e.Item.FindControl("txtKeteranganEntryEdit")
        Dim txtQtyApproved As TextBox = e.Item.FindControl("txtQtyApprovedEdit")
        Dim ddlCondition As DropDownList = e.Item.FindControl("drpConditionEdit")

        If (ValidateDuplication(txt1.Text.ToUpper, "Edit", e.Item.ItemIndex) AndAlso ValidateItem(txt1.Text, txt2.Text, txt3.Text, txtQtyApproved.Text)) Then
            objClaimDetail = objClaimHeader.ClaimDetails(e.Item.ItemIndex)


            Dim oClaimGood As ClaimGoodCondition = New Claim.ClaimGoodConditionFacade(User).Retrieve(Convert.ToInt32(ddlCondition.SelectedValue))
            objClaimDetail.Keterangan = txt3.Text
            objClaimDetail.Qty = Convert.ToInt32(txt2.Text)
            objClaimDetail.ApprovedQty = Convert.ToInt32(txtQtyApproved.Text)

            'di remark kalo ktb uda ga perlu
            If Not IsKTB Then
                Dim oSPDetails As ArrayList = GetCurrentSparePartPOStatusDetailBySparePartCode(txt1.Text)
                objClaimDetail.SparePartPOStatusDetail = CType(oSPDetails(0), SparePartPOStatusDetail)
            Else
                Dim ddlStatus As DropDownList = e.Item.FindControl("ddlStatus")
                objClaimDetail.StatusDetailKTB = ddlStatus.SelectedValue
            End If
            objClaimDetail.ClaimGoodCondition = oClaimGood

            sesHelper.SetSession("PartEntryClaimEdit", objClaimHeader)
            dtgEntryClaimEdit.EditItemIndex = -1
            Mode = ViewState("Mode")
            If (Mode = enumMode.Mode.EditMode) Then
                Dim objClaimDetailFacade As New ClaimDetailFacade(User)
                objClaimDetail.ClaimHeader = objClaimHeader
                objClaimDetailFacade.Update(objClaimDetail)
            End If
            dtgEntryClaimEdit.EditItemIndex = -1
            If Not IsKTB Then
                dtgEntryClaimEdit.ShowFooter = True
            End If
            BindDetailToGrid()
        End If

        btnSave.Enabled = True
    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        If fuEvidence.Value <> "" OrElse fuEvidence.Value <> Nothing Then
            GetFileClaimHeader(False)
        Else
            MessageBox.Show("File masih kosong")
        End If
    End Sub
#End Region
End Class
