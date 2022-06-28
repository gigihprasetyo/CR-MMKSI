#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Event
Imports KTB.DNet.BusinessFacade.FinishUnit
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Configuration
#End Region

Public Class FrmRealizationEvent
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents icStartDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEndDate As KTB.DNet.WebCC.IntiCalendar
    'Protected WithEvents dgAlokasi As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents txtID As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRealNumOfInvitation As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRealNumOfParticipants As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRealTotalCost As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRealComment As System.Web.UI.WebControls.TextBox
    Protected WithEvents fileRealCostDetail As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents fileRealVideo As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents fileRealMatPromo As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents lblDealerID As System.Web.UI.WebControls.Label
    Protected WithEvents btnUploadRealCostDetail As System.Web.UI.WebControls.Button
    Protected WithEvents btnUploadRealVideo As System.Web.UI.WebControls.Button
    Protected WithEvents btnUploadRealMatPromo As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents txtCostDetail As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtVideo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMatPromo As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgEventSales As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents txtTempatAcara As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRealApprovalCost As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private arlRealEvent As ArrayList
    Private arlDetail As ArrayList = New ArrayList
    Private objRealEvent As EventInfo
    Private sHelper As SessionHelper = New SessionHelper
    Private objMatPromo As String
    Private objVideo As String
    Private objBiaya As String
#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ViewState("CurrentSortColumn") = "VechileType.Description"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            If Not IsNothing(Request.QueryString("id")) Then
                objRealEvent = New EventInfoFacade(User).Retrieve(CInt(Request.QueryString("id")))
                txtID.Text = objRealEvent.EventRequestNo
                lblDealerID.Text = objRealEvent.Dealer.ID
                lblDealer.Text = objRealEvent.Dealer.DealerCode
                lblDealerName.Text = objRealEvent.Dealer.DealerName
                displaydata()
                txtID.ReadOnly = True
                btnBack.Visible = True
                btnCancel.Visible = False
                sHelper.SetSession("EventSalesItem", objRealEvent.EventSaless)
                BindtoGrid()
                If (Request.QueryString("Mode") = "Edit") Then
                    datareadOnly(False)
                Else
                    datareadOnly(True)
                    btnSimpan.Visible = False
                    fileRealCostDetail.Visible = False
                    fileRealMatPromo.Visible = False
                    fileRealVideo.Visible = False
                    btnUploadRealCostDetail.Visible = False
                    btnUploadRealVideo.Visible = False
                    btnUploadRealMatPromo.Visible = False
                    dtgEventSales.Columns(4).Visible = False
                    dtgEventSales.ShowFooter = False
                End If
                btnCari.Visible = False
            Else
                If InitialPageSession() Then
                    ClearData()
                    btnCari.Attributes("OnClick") = "ShowPopUpEventInfo();"
                    datareadOnly(True)
                    sHelper.SetSession("EventSalesItem", New ArrayList)
                    BindtoGrid()
                End If
            End If

        End If

    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If validRealDate() Then
            Dim arrEvent As ArrayList = New ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EventInfo), "EventRequestNo", MatchType.Exact, txtID.Text))
            criterias.opAnd(New Criteria(GetType(EventInfo), "Dealer.ID", MatchType.Exact, lblDealerID.Text))
            criterias.opAnd(New Criteria(GetType(EventInfo), "IsConfirmed", MatchType.Exact, "1"))
            arrEvent = New EventInfoFacade(User).Retrieve(criterias)
            If arrEvent.Count > 0 Then
                objRealEvent = arrEvent(0)
                If updateEventInfo() Then
                    If updateEventSales() Then
                        MessageBox.Show(SR.UpdateSucces)
                    Else
                        Return
                    End If
                Else
                    Return
                End If
            Else
                MessageBox.Show(SR.UpdateFail)
            End If
        Else
            MessageBox.Show("Tanggal Mulai Pelaksanaan Tidak Boleh Lebih Besar dari Tanggal Akhir Pelaksanaan")
            Return
        End If

    End Sub

    Private Sub btnUploadRealCostDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUploadRealCostDetail.Click
        If Not (fileRealCostDetail.PostedFile Is Nothing) And (fileRealCostDetail.PostedFile.ContentLength > 0) Then
            If uploadData(fileRealCostDetail) Then

            Else

            End If
        Else
            MessageBox.Show("Pilih file yang akan di-upload.")
        End If
    End Sub

    Private Sub btnUploadRealMatPromo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadRealMatPromo.Click
        If Not (fileRealMatPromo.PostedFile Is Nothing) And (fileRealMatPromo.PostedFile.ContentLength > 0) Then
            If uploadData(fileRealMatPromo) Then

            Else

            End If
        Else
            MessageBox.Show("Pilih file yang akan di-upload.")
        End If
    End Sub

    Private Sub btnUploadRealVideo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadRealVideo.Click
        If Not (fileRealVideo.PostedFile Is Nothing) And (fileRealVideo.PostedFile.ContentLength > 0) Then
            If uploadData(fileRealVideo) Then

            Else

            End If
        Else
            MessageBox.Show("Pilih file yang akan di-upload.")
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        If txtID.Text = String.Empty Then
            ClearData()
            datareadOnly(True)
            Return
        End If
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(EventInfo), "Dealer.ID", MatchType.Exact, lblDealerID.Text))
        criterias.opAnd(New Criteria(GetType(EventInfo), "EventRequestNo", MatchType.Exact, txtID.Text))
        criterias.opAnd(New Criteria(GetType(EventInfo), "IsConfirmed", MatchType.Exact, "1"))

        arlRealEvent = New ArrayList
        arlRealEvent = New EventInfoFacade(User).Retrieve(criterias)
        If arlRealEvent.Count > 0 Then
            objRealEvent = arlRealEvent(0)
            sHelper.SetSession("EventInfoItem", objRealEvent)
            arlDetail = objRealEvent.EventSaless
            sHelper.SetSession("EventSalesItem", arlDetail)
            datareadOnly(False)
            displaydata()
            BindtoGrid()
            btnSimpan.Enabled = True
            dtgEventSales.ShowFooter = True
        Else
            ClearData()
            datareadOnly(True)
            btnSimpan.Enabled = False
            arlDetail = New ArrayList
            BindtoGrid()
            sHelper.SetSession("EventInfoItem", Nothing)
            sHelper.SetSession("EventSalesItem", Nothing)
            MessageBox.Show("Data Not Valid")

        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClearData()
        datareadOnly(True)
        arlDetail = New ArrayList
        BindtoGrid()
    End Sub

    Private Sub dtgEventSales_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEventSales.ItemDataBound
        If e.Item.ItemType = ListItemType.Footer Then
            BindDDL(e, "ddlIVehicleDesc")
        ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", _
                    New CommonFunction().PreventDoubleClickAtGrid(CType(e.Item.FindControl("lbtnDelete"), LinkButton), "Yakin Data ini akan dihapus?"))
            End If

        ElseIf e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            BindDDL(e, "ddlEVehicleDesc")
        End If
    End Sub

    Private Sub dtgEventSales_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEventSales.ItemCommand
        arlDetail = CType(Session("EventSalesItem"), ArrayList)
        Select Case e.CommandName
            Case "add" 'Insert New item to datagrid
                Dim valVehicle As DropDownList = CType(e.Item.FindControl("ddlIVehicleDesc"), DropDownList)
                Dim valQty As TextBox = CType(e.Item.FindControl("txtFQty"), TextBox)
                Dim valType As Label = CType(e.Item.FindControl("lblFType"), Label)

                Dim objVehicle As VechileType

                If valqty.Text = String.Empty Then
                    MessageBox.Show("Silakan masukan hasil penjualan.")
                    Return
                Else
                    If Not IsNumeric(valqty.Text) Then
                        MessageBox.Show("Hasil penjualan harus numeric")
                        Return
                    End If
                End If

                If valVehicle.SelectedIndex > 0 Then
                    If (valQty.Text = String.Empty) OrElse (CInt(valQty.Text) < 1) Then
                        MessageBox.Show("Hasil Penjualan Harus Lebih Besar dari 0 (NOL)")
                        Return
                    Else
                        objVehicle = New VechileTypeFacade(User).Retrieve(CInt(valVehicle.SelectedValue))
                        If Not IsNothing(objVehicle) Then
                            valType.Text = objVehicle.Category.CategoryCode
                        End If
                        If VehicleIsExist(valVehicle.SelectedValue, arlDetail, e.Item.ItemIndex) Then
                            MessageBox.Show(SR.DataIsExist("Jenis Kendaraan "))
                            Return
                        Else
                            Dim objEventSalesItem As EventSales = New EventSales
                            objEventSalesItem.VechileType = objVehicle
                            Try
                                objEventSalesItem.Qty = CInt(valQty.Text)
                            Catch ex As Exception
                                MessageBox.Show("Quantity tidak valid")
                                Return
                            End Try
                            objEventSalesItem.EventInfo = CType(Session("EventInfoItem"), EventInfo)
                            arlDetail.Add(objEventSalesItem)
                        End If
                    End If
                Else
                    MessageBox.Show("Pilih Type Kendaraan")
                    Return
                End If
            Case "edit" 'Edit mode activated
                btnSimpan.Enabled = False
                dtgEventSales.ShowFooter = False
                dtgEventSales.EditItemIndex = e.Item.ItemIndex
            Case "delete" 'Delete this datagrid item 
                Try
                    arlDetail.RemoveAt(e.Item.ItemIndex)
                Catch ex As Exception

                End Try
            Case "save" 'Update this datagrid item 
                Dim valVehicle As DropDownList = CType(e.Item.FindControl("ddlEVehicleDesc"), DropDownList)
                Dim valQty As TextBox = CType(e.Item.FindControl("txtEQty"), TextBox)
                Dim valType As Label = CType(e.Item.FindControl("lblEType"), Label)

                Dim objVehicle As VechileType

                If valVehicle.SelectedIndex > 0 Then
                    If (valQty.Text = String.Empty) OrElse (CInt(valQty.Text) < 1) Then
                        MessageBox.Show("Hasil Penjualan Harus Lebih Besar dari 0 (NOL)")
                        Return
                    Else
                        objVehicle = New VechileTypeFacade(User).Retrieve(CInt(valVehicle.SelectedValue))
                        If Not IsNothing(objVehicle) Then
                            valType.Text = objVehicle.Category.CategoryCode
                        End If
                        If VehicleIsExist(valVehicle.SelectedValue, arlDetail, e.Item.ItemIndex) Then
                            MessageBox.Show(SR.DataIsExist("Jenis Kendaraan "))
                            Return
                        Else
                            Dim objEventSalesItem As EventSales = CType(arlDetail(e.Item.ItemIndex), EventSales)
                            objEventSalesItem.VechileType = objVehicle
                            objEventSalesItem.Qty = CInt(valQty.Text)
                            objEventSalesItem.EventInfo = CType(Session("EventInfoItem"), EventInfo)
                            If (New EventSalesFacade(User).Update(objEventSalesItem) <> -1) Then
                                arlDetail(e.Item.ItemIndex) = objEventSalesItem
                                dtgEventSales.EditItemIndex = -1
                                btnSimpan.Enabled = True
                                dtgEventSales.ShowFooter = True
                            End If
                        End If
                    End If
                Else
                    MessageBox.Show("Pilih Type Kendaraan")
                    Return
                End If
            Case "cancel" 'Cancel Update this datagrid item 
                dtgEventSales.EditItemIndex = -1
                dtgEventSales.ShowFooter = True
                btnSimpan.Enabled = True
        End Select
        sHelper.SetSession("EventSalesItem", arlDetail)
        BindtoGrid()
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("../Event/FrmListRealizationEvent.aspx?, false")
    End Sub

    Private Sub dtgEventSales_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEventSales.SortCommand
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
        BindtoGrid()
    End Sub
#End Region

#Region "Custom Method"

    Private Function InitialPageSession() As Boolean
        If Not IsNothing(Session("DEALER")) Then
            lblDealerID.Text = CType(Session("DEALER"), Dealer).ID
            lblDealer.Text = CType(Session("DEALER"), Dealer).DealerCode
            lblDealerName.Text = CType(Session("DEALER"), Dealer).DealerName
            Return True
        End If
        Return False
    End Function

    Private Sub ClearData()
        txtTempatAcara.Text = String.Empty
        txtID.Text = String.Empty
        txtRealNumOfInvitation.Text = String.Empty
        txtRealNumOfParticipants.Text = String.Empty
        txtRealTotalCost.Text = String.Empty
        txtRealApprovalCost.Text = String.Empty
        txtRealComment.Text = String.Empty
        icStartDate.Value = Now
        icEndDate.Value = Now
        btnSimpan.Enabled = False
        txtCostDetail.Text = String.Empty
        txtVideo.Text = String.Empty
        txtMatPromo.Text = String.Empty
        dtgEventSales.ShowFooter = False
    End Sub

    Private Function validRealDate() As Boolean
        If icStartDate.Value <= icEndDate.Value Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function checkInteger(ByVal txtName As TextBox) As Integer
        If (txtName.Text <> "") Then
            Return CInt(txtName.Text)
        Else
            Return 0
        End If
    End Function

    Private Function updateEventInfo() As Boolean
        objRealEvent.RealDateStart = icStartDate.Value.ToShortDateString
        objRealEvent.RealDateEnd = icEndDate.Value.ToShortDateString
        objRealEvent.RealLocation = txtTempatAcara.Text '-->tambahan Tempat Acara
        objRealEvent.RealComment = txtRealComment.Text
        objRealEvent.RealMatPromoFile = fileRealMatPromo.PostedFile.FileName
        objRealEvent.RealVideoFile = fileRealVideo.PostedFile.FileName
        objRealEvent.RealCostDetailFile = fileRealCostDetail.PostedFile.FileName
        objRealEvent.RealNumOfInvitation = checkInteger(txtRealNumOfInvitation)
        objRealEvent.RealNumOfParticipants = checkInteger(txtRealNumOfParticipants)
        objRealEvent.RealTotalCost = checkInteger(txtRealTotalCost)
        objRealEvent.RealApprovalCost = checkInteger(txtRealApprovalCost)
        objRealEvent.RealCostDetailFile = txtCostDetail.Text.Trim
        objRealEvent.RealMatPromoFile = txtMatPromo.Text.Trim
        objRealEvent.RealVideoFile = txtVideo.Text.Trim
        objRealEvent.IsRealization = 1
        If objRealEvent.IsConfirmed = 1 Then
            If (objRealEvent.EventRealizationNo = String.Empty) OrElse (IsNothing(objRealEvent.EventRealizationNo)) Then
                objRealEvent.EventRealizationNo = "request"
            End If
        End If
        Dim hasil As Integer
        Try
            hasil = New EventInfoFacade(User).Update(objRealEvent)
            If hasil <> -1 Then
                Return True
            Else
                MessageBox.Show(SR.UploadFail("Event Info"))
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show(SR.UploadFail("Event Info"))
            Return False
        End Try

    End Function

    Private Function updateEventSales() As Boolean
        Dim objEventSalesToUpdate As EventSales
        Dim objEventSalesdata As EventSales
        Dim arlDetailToUpdate As ArrayList = New ArrayList
        Dim nresult As Integer
        arlDetail = CType(sHelper.GetSession("EventSalesItem"), ArrayList)
        If objRealEvent.EventSaless.Count > 0 Then
            arlDetailToUpdate = objRealEvent.EventSaless
            For Each item As EventSales In arlDetailToUpdate
                item.RowStatus = DBRowStatus.Deleted
                Try
                    nresult = New EventSalesFacade(User).Update(item)
                    If nresult <> -1 Then

                    Else
                        MessageBox.Show("Gagal Update EventSales")

                    End If

                Catch ex As Exception

                End Try
            Next
        End If

        If arlDetail.Count > 0 Then
            For Each updateItem As EventSales In arlDetail
                If updateItem.ID = 0 Then
                    Try
                        updateItem.EventInfo = objRealEvent
                        nresult = New EventSalesFacade(User).Insert(updateItem)
                        If nresult = -1 Then
                            MessageBox.Show("Gagal Insert")
                            Return False
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Gagal Insert")
                        Return False
                    End Try
                Else
                    For Each item As EventSales In arlDetailToUpdate
                        If item.ID = updateItem.ID Then
                            item.Qty = updateItem.Qty
                            item.VechileType = updateItem.VechileType
                            item.RowStatus = DBRowStatus.Active
                            Try
                                nresult = New EventSalesFacade(User).Update(item)
                                If nresult = -1 Then
                                    MessageBox.Show("Gagal Update")
                                    Return False
                                End If
                            Catch ex As Exception
                                MessageBox.Show("Gagal Update")
                                Return False
                            End Try
                        End If
                    Next
                End If
            Next
        End If
        Return True
    End Function

    Private Function uploadData(ByVal _Filedata As HtmlInputFile) As Boolean
        Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))
        If _Filedata.PostedFile.ContentLength > maxFileSize Then
            MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
            Exit Function
        Else
            Dim SrcFile As String
            Dim DestFile As String
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            Dim success As Boolean = False

            SrcFile = Path.GetFileName(_Filedata.PostedFile.FileName)
            DestFile = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("EventDir") & "\"
            Dim filedir As String

            If Not IsNothing(Session("EventInfoItem")) Then
                filedir = CType(Session("EventInfoItem"), EventInfo).EventRequestNo
                filedir = filedir.Replace("/", "_").ToString
                DestFile = DestFile & filedir
            End If
            If _Filedata.ID = fileRealCostDetail.ID Then
                DestFile = DestFile & "\Detail_Biaya" & "\" & SrcFile
                objBiaya = DestFile
                txtCostDetail.Text = filedir & "\Detail_Biaya" & "\" & SrcFile
            ElseIf _Filedata.ID = fileRealVideo.ID Then
                DestFile = DestFile & "\Video" & "\" & SrcFile
                objVideo = DestFile
                txtVideo.Text = filedir & "\Video" & "\" & SrcFile
            ElseIf _Filedata.ID = fileRealMatPromo.ID Then
                DestFile = DestFile & "\MatPromo" & "\" & SrcFile
                objMatPromo = DestFile
                txtMatPromo.Text = filedir & "\MatPromo" & "\" & SrcFile
            End If

            Dim finfo As New FileInfo(DestFile)
            Try
                success = imp.Start()
                If success Then
                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If
                    _Filedata.PostedFile.SaveAs(DestFile)
                    imp.StopImpersonate()
                    imp = Nothing
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End If
    End Function

    Private Sub displaydata()
        If (objRealEvent.IsRealization = 1) Then
            icStartDate.Value = objRealEvent.RealDateStart
            txtTempatAcara.Text = objRealEvent.RealLocation '-->tambahan Tempat Acara
            icEndDate.Value = objRealEvent.RealDateEnd
            txtRealNumOfInvitation.Text = objRealEvent.RealNumOfInvitation.ToString("#,###")
            txtRealNumOfParticipants.Text = objRealEvent.RealNumOfParticipants.ToString("#,###")
            txtRealTotalCost.Text = objRealEvent.RealTotalCost.ToString("#,###")
            txtRealApprovalCost.Text = objRealEvent.RealApprovalCost.ToString("#,###")
            txtRealComment.Text = objRealEvent.RealComment
            txtCostDetail.Text = objRealEvent.RealCostDetailFile
            txtVideo.Text = objRealEvent.RealVideoFile
            txtMatPromo.Text = objRealEvent.RealMatPromoFile
        ElseIf (objRealEvent.IsConfirmed = 1) Then
            icStartDate.Value = objRealEvent.ConfirmedDateStart
            icEndDate.Value = objRealEvent.ConfirmedDateEnd
            txtRealNumOfInvitation.Text = objRealEvent.ConfirmedNumOfInvitation.ToString
            txtRealNumOfParticipants.Text = "0"
            txtRealTotalCost.Text = String.Empty
            txtRealApprovalCost.Text = String.Empty
            txtRealComment.Text = String.Empty
            txtTempatAcara.Text = String.Empty '-->tambahan Tempat Acara
        End If
    End Sub

    Private Sub datareadOnly(ByVal bval As Boolean)
        icStartDate.Enabled = Not bval
        icEndDate.Enabled = Not bval
        txtRealNumOfInvitation.ReadOnly = bval
        txtRealNumOfParticipants.ReadOnly = bval
        txtRealTotalCost.ReadOnly = bval
        txtRealApprovalCost.ReadOnly = bval
        txtRealComment.ReadOnly = bval
        btnUploadRealCostDetail.Enabled = Not bval
        btnUploadRealVideo.Enabled = Not bval
        btnUploadRealMatPromo.Enabled = Not bval
        txtTempatAcara.Enabled = Not bval '-->Tambahan Tempat Acara

    End Sub

    Private Sub BindtoGrid()
        If Not IsNothing(sHelper.GetSession("EventSalesItem")) Then
            arlDetail = CType(sHelper.GetSession("EventSalesItem"), ArrayList)
            arlDetail = CommonFunction.SortArraylist(arlDetail, GetType(EventSales), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))
            dtgEventSales.DataSource = arlDetail
            dtgEventSales.DataBind()
            sHelper.SetSession("EventSalesItem", arlDetail)
        End If
    End Sub

    Private Sub BindDDL(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs, ByVal ddlId As String)
        Dim ddlVehicle As DropDownList = CType(e.Item.FindControl(ddlId), DropDownList)
        Dim arlVehicle As ArrayList = New VechileTypeFacade(User).RetrieveList
        If arlVehicle.Count > 0 Then
            ddlVehicle.DataTextField = "Description"
            ddlVehicle.DataValueField = "ID"
            ddlVehicle.DataSource = arlVehicle
            ddlVehicle.DataBind()
            ddlVehicle.Items.Insert(0, New ListItem("Silahkan Pilih", "0"))
        End If
        If e.Item.ItemType = ListItemType.EditItem Then
            ddlVehicle.SelectedValue = CType(arlDetail(e.Item.ItemIndex), EventSales).VechileType.ID
        End If
    End Sub

    Private Function VehicleIsExist(ByVal VehicleID As String, ByVal arlVehicle As ArrayList, ByVal nIndeks As Integer) As Boolean
        Dim bResult As Boolean = False
        Dim i As Integer
        For i = 0 To arlVehicle.Count - 1
            If CType(arlVehicle(i), EventSales).VechileType.ID.ToString = VehicleID AndAlso nIndeks <> i Then
                bResult = True
                Exit For
            End If
        Next
        Return bResult
    End Function

#End Region


   
End Class
