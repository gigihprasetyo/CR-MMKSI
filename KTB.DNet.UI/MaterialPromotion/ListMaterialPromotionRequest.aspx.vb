Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.MaterialPromotion
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports KTB.DNet.UI.Helper
Public Class ListMaterialPromotionRequest
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSeacrh As System.Web.UI.WebControls.Button
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRequestNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lstStatus As System.Web.UI.WebControls.ListBox
    Protected WithEvents dtgMaterialPromotionList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnProses As System.Web.UI.WebControls.Button
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblChangeStatus As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatusGI As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Const SES_objAuditScheduleDealer As String = "SES_objAuditScheduleDealer"
#End Region

#Region " Private Variables"
    Dim sessHelp As SessionHelper = New SessionHelper
    Dim criterias As CriteriaComposite
    Dim arlMaterialPromotionRequest As ArrayList
    Dim objMaterialPromotionRequest As MaterialPromotionRequest
    Dim strStatus As String = ""
    Private objDealer As Dealer
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionViewPermintaanList_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Material Promosi - Daftar Permintaan Material Promosi")
        End If
    End Sub

    Private Function CheckDealerStatusProcess(ByVal _str As String) As Boolean
        If _str.ToUpper = "BATAL" Then
            If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionListPermintaanCancel_Privilege) Then
                Return False
            Else
                Return True
            End If
        ElseIf _str.ToUpper = "VALIDASI" Then
            If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionListValidationPermintaan_Privilege) Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If
    End Function

    Private Function ChecKTBStatusProcess(ByVal _str As String) As Boolean
        If _str.ToUpper = "DISETUJUI" Then
            If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionListPermintaanDisetujui_Privilege) Then
                Return False
            Else
                Return True
            End If
        ElseIf _str.ToUpper = "DITOLAK" Then
            If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionListPermintaanReject_Privilege) Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If
    End Function

    Dim checkVal As Boolean = SecurityProvider.Authorize(context.User, SR.MaterialPromotionListPermintaanEdit_Privilege)
    'Private Function ChecKEditIcon() As Boolean
    '    If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionListPermintaanEdit_Privilege) Then
    '        Return False
    '    Else
    '        Return True
    '    End If
    'End Function

    Dim bChecKViewDetail As Boolean = SecurityProvider.Authorize(context.User, SR.MaterialPromotionListPermintaanViewDetail_Privilege)

    'Private Function ChecKViewDetail() As Boolean
    '    If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionListPermintaanViewDetail_Privilege) Then
    '        Return False
    '    Else
    '        Return True
    '    End If
    'End Function

#End Region

#Region "Custom Method"
    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function
    Private Property SesDealer() As Dealer
        Get
            Return CType(sessHelp.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sessHelp.SetSession("DEALER", Value)
        End Set
    End Property
    Private Sub BindStatus()

        ddlStatusGI.DataSource = New EnumGIStatusMatPromotion().RetrieveStatus
        ddlStatusGI.DataTextField = "NameStatus"
        ddlStatusGI.DataValueField = "ValStatus"
        ddlStatusGI.DataBind()
        ddlStatusGI.Items.Insert(0, "Pilih Status")
        ddlStatusGI.SelectedIndex = 0

        'refer bug 1343
        Dim arlStatus As ArrayList
        If IsLoginAsDealer() Then
            arlStatus = New EnumMaterialPromotion().RetrieveMaterialPromotionStatusDealer
        Else
            arlStatus = New EnumMaterialPromotion().RetrieveMaterialPromotionStatusKTB
        End If

        lstStatus.DataSource = arlStatus
        lstStatus.DataTextField = "NameStatus"
        lstStatus.DataValueField = "ValStatus"
        lstStatus.DataBind()

        Dim arlStatusProcess As New ArrayList
        Dim arlTemp As New ArrayList
        If IsLoginAsDealer() Then
            arlTemp = EnumMaterialPromotion.RetrieveMaterialPromotionStatusUpdateDealer()
            For Each itemTmp As EnumMaterialPromotionStatus In arlTemp
                If CheckDealerStatusProcess(itemTmp.NameStatus) Then
                    arlStatusProcess.Add(itemTmp)
                End If
            Next
            ddlStatus.DataSource = arlStatusProcess
        Else
            arlTemp = EnumMaterialPromotion.RetrieveMaterialPromotionStatusForKTB()
            For Each itemTmp As EnumMaterialPromotionStatus In arlTemp
                If ChecKTBStatusProcess(itemTmp.NameStatus) Then
                    arlStatusProcess.Add(itemTmp)
                End If
            Next
            ddlStatus.DataSource = arlStatusProcess
        End If

        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataBind()
    End Sub
    Private Sub BindDealerSearching()
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
    End Sub
    Private Sub ReadCriteria()
        Dim crits As Hashtable
        crits = CType(sessHelp.GetSession("criterias"), Hashtable)

        If Not IsNothing(crits) Then
            txtDealerCode.Text = CStr(crits.Item("DealerCode"))
            txtRequestNo.Text = CStr(crits.Item("RequestNo"))
            'ddlOrderType.SelectedValue = CStr(crits.Item(""))

            If Not IsNothing(crits.Item("Status")) Then
                For Each item As ListItem In lstStatus.Items
                    For Each i As String In CStr(crits.Item("Status")).Split(",")
                        If item.Value = i Then
                            item.Selected = True
                        End If
                    Next
                Next
            End If
        End If
    End Sub
    Private Sub SaveCriteria()
        Dim crits As Hashtable = New Hashtable
        crits.Add("DealerCode", txtDealerCode.Text)
        crits.Add("RequestNo", txtRequestNo.Text)
        'crits.Add("", ddlOrderType.SelectedValue)
        For Each item As ListItem In lstStatus.Items
            If item.Selected = True Then
                strStatus = strStatus & item.Value & ","
            End If
        Next

        If (strStatus <> "") Then
            strStatus = strStatus.Remove(strStatus.Length - 1, 1)
            crits.Add("Status", strStatus)
        End If

        sessHelp.SetSession("criterias", crits)
    End Sub
    Private Function CreateCriteria() As Boolean
        Dim objMaterialPromotionRequest As MaterialPromotionRequest
        criterias = New CriteriaComposite(New Criteria(GetType(MaterialPromotionRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        objDealer = Session("DEALER")

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            criterias.opAnd(New Criteria(GetType(MaterialPromotionRequest), "Status", MatchType.GreaterOrEqual, CInt(EnumMaterialPromotion.MaterialPromotionStatus.Validasi)))
            If (txtDealerCode.Text.Trim <> String.Empty) Then
                criterias.opAnd(New Criteria(GetType(MaterialPromotionRequest), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
            End If
        Else
            If (txtDealerCode.Text.Trim <> String.Empty) Then
                If New DataOwner().IsdealerExistInGroup(txtDealerCode.Text.Trim, objDealer) Then
                    criterias.opAnd(New Criteria(GetType(MaterialPromotionRequest), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
                Else
                    Return False
                End If
            Else
                Dim strCrit As String = New DataOwner().GenerateDealerCodeSelection(objDealer, User)
                criterias.opAnd(New Criteria(GetType(MaterialPromotionRequest), "Dealer.DealerCode", MatchType.InSet, strCrit))
            End If
        End If

        If (txtRequestNo.Text.Trim <> "") Then
            criterias.opAnd(New Criteria(GetType(MaterialPromotionRequest), "RequestNo", MatchType.[Partial], txtRequestNo.Text.Trim))
        End If

        If ddlStatusGI.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(MaterialPromotionRequest), "GIStatus", MatchType.Exact, ddlStatusGI.SelectedValue))
        End If

        'If ddlOrderType.SelectedValue <> "" Then
        '    criterias.opAnd(New Criteria(GetType(MaterialPromotionRequest), "", MatchType.Exact, ddlOrderType.SelectedValue))
        'End If

        Dim selectedStatus As String = ""
        For Each item As ListItem In lstStatus.Items
            If item.Selected = True Then
                selectedStatus = selectedStatus & item.Value & ","
            End If
        Next

        If (selectedStatus <> "") Then
            selectedStatus = selectedStatus.Remove(selectedStatus.Length - 1, 1)
            criterias.opAnd(New Criteria(GetType(MaterialPromotionRequest), "Status", MatchType.InSet, "(" + selectedStatus + ")"))
        End If
        Return True
    End Function
    Private Sub MessageAlert(ByVal nResult As Integer, ByVal sign As Integer)
        If nResult = -1 Then
            If (sign = 1) Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.UpdateFail)
            End If
        Else
            If (sign = 1) Then
                MessageBox.Show(SR.SaveSuccess)
            Else
                MessageBox.Show(SR.UpdateSucces)
            End If
        End If
    End Sub
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        InitiateAuthorization()

        If Not IsPostBack Then
            BindStatus()
            BindDealerSearching()
            ReadCriteria()
            BindDatagrid(0)
        End If
    End Sub
    Private Sub btnSeacrh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeacrh.Click
        viewstate.Item("SortCol") = "RequestNo"
        viewstate.Item("SortDirection") = Sort.SortDirection.ASC
        dtgMaterialPromotionList.CurrentPageIndex = 0
        SaveCriteria()
        BindDatagrid(0)
    End Sub
    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (CreateCriteria()) Then
            If (indexPage >= 0) Then
                arlMaterialPromotionRequest = New MaterialPromotionRequestFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dtgMaterialPromotionList.PageSize, totalRow, viewstate.Item("SortCol"), viewstate.Item("SortDirection"))
                dtgMaterialPromotionList.DataSource = arlMaterialPromotionRequest
                dtgMaterialPromotionList.VirtualItemCount = totalRow
                dtgMaterialPromotionList.DataBind()
            End If

            If totalRow > 0 Then
                lblChangeStatus.Visible = True
                ddlStatus.Visible = True
                btnProses.Visible = True
            End If
        Else
            dtgMaterialPromotionList.DataSource = Nothing
            dtgMaterialPromotionList.DataBind()
            MessageBox.Show("Kode dealer tidak valid.")
        End If

    End Sub
    Private Sub dtgMaterialPromotionList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMaterialPromotionList.ItemCommand
        If e.CommandName = "View" Then
            Response.Redirect("RequestMaterialPromotion.aspx?id=" + e.CommandArgument + "&mode=View")
        ElseIf e.CommandName = "Edit" Then
            Response.Redirect("RequestMaterialPromotion.aspx?id=" + e.CommandArgument + "&mode=Edit")
        End If
    End Sub
    Private Sub dtgMaterialPromotionList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMaterialPromotionList.ItemDataBound
        If e.Item.ItemIndex <> -1 Then

            objMaterialPromotionRequest = arlMaterialPromotionRequest(e.Item.ItemIndex)
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgMaterialPromotionList.CurrentPageIndex * dtgMaterialPromotionList.PageSize)

            Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            lbtnView.CommandArgument = objMaterialPromotionRequest.RequestNo
            lbtnView.Visible = bChecKViewDetail

            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lbtnEdit.CommandArgument = objMaterialPromotionRequest.RequestNo

            'Dim checkVal As Boolean = False
            'checkVal = ChecKEditIcon()

            If IsLoginAsDealer() Then
                If objMaterialPromotionRequest.Status = CType(EnumMaterialPromotion.MaterialPromotionStatus.Baru, Integer) Then
                    lbtnEdit.Visible = checkVal
                Else
                    lbtnEdit.Visible = False
                End If
            Else
                If objMaterialPromotionRequest.Status = CType(EnumMaterialPromotion.MaterialPromotionStatus.Validasi, Integer) Then
                    lbtnEdit.Visible = checkVal
                Else
                    lbtnEdit.Visible = False
                End If
            End If

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            lblStatus.Text = CType(objMaterialPromotionRequest.Status, EnumMaterialPromotion.MaterialPromotionStatus).ToString
        End If
    End Sub
    Private Sub btnProses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProses.Click

        Dim nResult As Integer
        Dim checkCounter As Integer = 0

        If dtgMaterialPromotionList.Items.Count > 0 Then

            'Check Status Change (Bug 497)
            For Each item As DataGridItem In dtgMaterialPromotionList.Items
                Dim chckbox As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
                Dim lbtnView As LinkButton = CType(item.FindControl("lbtnView"), LinkButton)

                objMaterialPromotionRequest = New MaterialPromotionRequestFacade(User).Retrieve(lbtnView.CommandArgument)
                If chckbox.Checked Then
                    If objMaterialPromotionRequest.Status = EnumMaterialPromotion.MaterialPromotionStatus.Baru And Not (CByte(ddlStatus.SelectedValue) = EnumMaterialPromotion.MaterialPromotionStatus.Batal Or CByte(ddlStatus.SelectedValue) = EnumMaterialPromotion.MaterialPromotionStatus.Validasi) Then
                        MessageBox.Show("Item " & item.ItemIndex + 1 & " : Status baru hanya bisa diubah menjadi status batal atau validasi")
                        Return
                    End If

                    If objMaterialPromotionRequest.Status = EnumMaterialPromotion.MaterialPromotionStatus.Batal Then
                        MessageBox.Show("Item " & item.ItemIndex + 1 & " : Status batal tidak dapat diubah")
                        Return
                    End If

                    If objMaterialPromotionRequest.Status = EnumMaterialPromotion.MaterialPromotionStatus.Validasi And Not (CByte(ddlStatus.SelectedValue) = EnumMaterialPromotion.MaterialPromotionStatus.Disetujui Or CByte(ddlStatus.SelectedValue) = EnumMaterialPromotion.MaterialPromotionStatus.Ditolak) Then
                        MessageBox.Show("Item " & item.ItemIndex + 1 & " : Status validasi hanya bisa diubah menjadi status disetujui atau ditolak")
                        Return
                    End If

                    If objMaterialPromotionRequest.Status = EnumMaterialPromotion.MaterialPromotionStatus.Disetujui Then
                        MessageBox.Show("Item " & item.ItemIndex + 1 & " : Status disetujui tidak dapat diubah lagi")
                        Return
                    End If

                    If objMaterialPromotionRequest.Status = EnumMaterialPromotion.MaterialPromotionStatus.Ditolak Then
                        MessageBox.Show("Item " & item.ItemIndex + 1 & " : Status ditolak tidak dapat diubah")
                        Return
                    End If

                End If
            Next

            For Each item As DataGridItem In dtgMaterialPromotionList.Items
                Dim chckbox As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
                Dim lbtnView As LinkButton = CType(item.FindControl("lbtnView"), LinkButton)

                objMaterialPromotionRequest = New MaterialPromotionRequestFacade(User).Retrieve(lbtnView.CommandArgument)
                If chckbox.Checked Then
                    objMaterialPromotionRequest.Status = ddlStatus.SelectedValue
                    nResult = New MaterialPromotionRequestFacade(User).Update(objMaterialPromotionRequest)

                    For Each itemDetail As MaterialPromotionRequestDetail In objMaterialPromotionRequest.MaterialPromotionRequestDetails
                        If itemDetail.Qty > 0 Then
                            Dim objMaterial As KTB.DNet.Domain.MaterialPromotion = itemDetail.MaterialPromotion
                            Dim objAdjustment As MaterialPromotionStockAdjustment = New MaterialPromotionStockAdjustment
                            objAdjustment.Dealer = itemDetail.MaterialPromotionRequest.Dealer
                            objAdjustment.AdjustType = 2
                            objAdjustment.Qty = itemDetail.Qty
                            objAdjustment.StockAwal = objMaterial.Stock
                            objAdjustment.MaterialPromotion = itemDetail.MaterialPromotion
                            objAdjustment.Description = itemDetail.Description
                            nResult = New MaterialPromotionStockAdjustmentFacade(User).Insert(objAdjustment)
                            objMaterial.Stock = objMaterial.Stock - itemDetail.Qty
                            nResult = New MaterialPromotionFacade(User).Update(objMaterial)

                        End If


                    Next

                    checkCounter = checkCounter + 1
                End If
            Next
        Else
        End If
        If (checkCounter <> 0) Then
            MessageAlert(nResult, 0)
        Else
            MessageBox.Show("Silahkan pilih status")
        End If

        BindDatagrid(0)
    End Sub
    Private Sub dtgMaterialPromotionList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgMaterialPromotionList.SortCommand
        If e.SortExpression = viewstate.Item("SortCol") Then
            If viewstate.Item("SortDirection") = Sort.SortDirection.ASC Then
                viewstate.Item("SortDirection") = Sort.SortDirection.DESC
            Else
                viewstate.Item("SortDirection") = Sort.SortDirection.ASC
            End If
        End If
        viewstate.Item("SortCol") = e.SortExpression

        dtgMaterialPromotionList.SelectedIndex = -1
        dtgMaterialPromotionList.CurrentPageIndex = 0
        BindDatagrid(dtgMaterialPromotionList.CurrentPageIndex)
    End Sub
    Private Sub dtgMaterialPromotionList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgMaterialPromotionList.PageIndexChanged
        dtgMaterialPromotionList.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgMaterialPromotionList.CurrentPageIndex)
    End Sub
    Private Sub dtgMaterialPromotionList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgMaterialPromotionList.SelectedIndexChanged

    End Sub

#End Region







End Class
