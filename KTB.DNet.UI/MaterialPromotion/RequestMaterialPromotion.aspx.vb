#Region "Custom Namespace Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.MaterialPromotion
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports System.IO
#End Region

Public Class RequestMaterialPromotion
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnNew As System.Web.UI.WebControls.Button
    Protected WithEvents btnValidasi As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents lblRequestDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents lblRequestNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents dtgMaterialPromotion As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblSearchRequestNo As System.Web.UI.WebControls.Label
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents btnCetak As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Private Variables"
    Dim sessHelp As KTB.DNet.Utility.SessionHelper = New KTB.DNet.Utility.SessionHelper
    Private objProfileDetail As MaterialPromotionRequestFacade
    Private objMaterialPromotionRequestFacade As MaterialPromotionRequestFacade
    Private sessionHelper As New sessionHelper
    Private Detail As MaterialPromotionRequestDetail
    Private Header As MaterialPromotionRequest
    Private mode As Integer
    Private objDealer As Dealer
    Private DealerUser As Dealer
    Dim MatProID As Integer
    Dim strID As String
    Private _editDetailPriv As Boolean = False
    Private _btnPriv As Boolean = False
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionAllocationView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Material Promosi - Permintaan Material Promosi")
        End If
    End Sub

    Private Function CheckBtnPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionCreatePermintaan_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CheckValidasiPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PermintaanMaterialPromotionListValidateDetail_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function ChecKEditDetailPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionListPermintaanEditDetail_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CheckDownloadButtonPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.MatPromPermintaanDownload_privilege) Then
            Return False
        Else
            Return True
        End If
    End Function


    Dim bCekDeletePriv As Boolean = SecurityProvider.Authorize(context.User, SR.PermintaanMaterialPromotionListDeleteDetail_Privilege)

    'Private Function CheckDeletePriv() As Boolean
    '    If Not SecurityProvider.Authorize(context.User, SR.PermintaanMaterialPromotionListDeleteDetail_Privilege) Then
    '        Return False
    '    Else
    '        Return True
    '    End If
    'End Function

#End Region

#Region "Custom Method"
    Private Sub BindRequestNumberSearching()
        lblSearchRequestNo.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpRequestMaterialPromotion.aspx", "", 510, 700, "RequestNo")
    End Sub
    Private Sub BindHeader()
        If (Request.QueryString("id") <> "") Then
            Header = New MaterialPromotionRequestFacade(User).Retrieve(Request.QueryString("id"))
            lblRequestNo.Text = Header.RequestNo
            lblDealerCode.Text = Header.Dealer.DealerCode
            lblDealerName.Text = Header.Dealer.DealerName
            lblCity.Text = Header.Dealer.City.CityName
            lblRequestDate.Text = Header.RequestDate.ToString("dd/MM/yyyy")
            lblStatus.Text = CType(Header.Status, EnumMaterialPromotion.MaterialPromotionStatus).ToString
            txtDescription.Text = Header.Description
            Dim arrTempData As ArrayList = Header.MaterialPromotionRequestDetails
            sessHelp.SetSession("AddData", arrTempData)
            DealerUser = sessionHelper.GetSession("Dealer")
            If DealerUser.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
                btnValidasi.Visible = CheckValidasiPriv()
            End If
        Else
            Dim objUserInfo As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
            lblDealerCode.Text = objUserInfo.Dealer.DealerCode
            lblDealerName.Text = objUserInfo.Dealer.DealerName
            lblCity.Text = objUserInfo.Dealer.City.CityName
            lblRequestDate.Text = DateTime.Now.ToString("dd/MM/yyyy")
            lblStatus.Text = CType(0, EnumMaterialPromotion.MaterialPromotionStatus).ToString
        End If
    End Sub
    Private Sub AddDataToGrid(ByVal e As DataGridCommandEventArgs)
        Dim txtGoodsNo As TextBox = e.Item.FindControl("txtFooterGoodsNo")
        Dim lblName As Label = e.Item.FindControl("lblFooterName")
        Dim txtQuantity As TextBox = e.Item.FindControl("txtFooterQuantity")
        Dim txtRequestQuantity As TextBox = e.Item.FindControl("txtFooterRequestQuantity")
        Dim txtDescription1 As TextBox = e.Item.FindControl("txtDescription1")
        If (txtGoodsNo.Text = String.Empty And txtQuantity.Text = String.Empty) Then
            MessageBox.Show("Kode barang dan Qty harus diisi")

            objDealer = Session("DEALER")
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                If CInt(Val(txtRequestQuantity.Text)) < CInt(Val(txtQuantity.Text)) Then
                    MessageBox.Show("Jumlah disetujui tidak boleh lebih besar dari pada Jumlah Permintaan")
                End If
            End If
        Else
            If (txtRequestQuantity.Text <> String.Empty) Then
                If (ValidateDuplicate(txtGoodsNo.Text.Trim) And IsValidCode(txtGoodsNo.Text.Trim)) Then
                    Detail = New MaterialPromotionRequestDetail
                    Detail.MaterialPromotion = New MaterialPromotionFacade(User).Retrieve(txtGoodsNo.Text)
                    Detail.Qty = txtQuantity.Text
                    Detail.RequestQty = txtRequestQuantity.Text
                    Detail.Description = txtDescription1.Text
                    Dim arrTempData As ArrayList = sessionHelper.GetSession("AddData")
                    If arrTempData Is Nothing Then
                        arrTempData = New ArrayList
                    End If
                    arrTempData.Add(Detail)
                    sessionHelper.SetSession("AddData", arrTempData)
                    BindDataToGrid()
                End If
            Else
                MessageBox.Show("Jumlah permintaan harus diisi")
            End If
        End If
    End Sub
    Private Function IsValidCode(ByVal code As String) As Boolean
        Dim objMaterialPromotion As KTB.DNet.Domain.MaterialPromotion
        objMaterialPromotion = New MaterialPromotionFacade(User).Retrieve(code)
        If objMaterialPromotion.ID <= 0 Then
            MessageBox.Show("Error: Kode invalid")
            Return False
        End If
        Return True
    End Function
    Private Function ValidateDuplicate(ByVal kode As String) As Boolean
        Dim arlCekdata As ArrayList = sessionHelper.GetSession("AddData")
        If Not arlCekdata Is Nothing Then
            For Each item As MaterialPromotionRequestDetail In arlCekdata
                If (item.MaterialPromotion.GoodNo.ToUpper = kode.ToUpper) Then
                    MessageBox.Show("Error: Duplikasi Kode")
                    Return False
                End If
            Next
        End If
        Return True
    End Function
    Private Sub BindDataToGrid()
        Dim arrTempData As ArrayList = sessionHelper.GetSession("AddData")
        If Not arrTempData Is Nothing Then
            dtgMaterialPromotion.DataSource = arrTempData
        Else
            dtgMaterialPromotion.DataSource = New ArrayList
        End If
        dtgMaterialPromotion.DataBind()
    End Sub
    Private Sub EnableControl()
        If (Not IsNothing(Request.QueryString("mode"))) Then
            If Request.QueryString("mode") = "View" Then
                dtgMaterialPromotion.ShowFooter = False
                btnNew.Enabled = False
                btnSimpan.Enabled = False
                btnValidasi.Enabled = False
                lblSearchRequestNo.Visible = False
                txtDescription.ReadOnly = True
            End If
        Else
            If CheckBtnPriv() Then
                btnNew.Enabled = True
                btnSimpan.Enabled = True
                btnValidasi.Enabled = True
                lblSearchRequestNo.Visible = True
            Else
                btnNew.Enabled = False
                btnSimpan.Enabled = False
                btnValidasi.Enabled = False
                lblSearchRequestNo.Visible = False
            End If
            txtDescription.ReadOnly = False
        End If
    End Sub
    Private Function PopulateMaterialPromotionRequestHeader() As MaterialPromotionRequest
        Header = New MaterialPromotionRequest
        Dim i As Integer = 0

        If (lblRequestNo.Text = "") Then
            Header.Dealer = New DealerFacade(User).Retrieve(lblDealerCode.Text)
            Header.RequestDate = DateTime.Now
            Header.Status = EnumMaterialPromotion.MaterialPromotionStatus.Baru
            Header.IsValidate = 1
            Header.RowStatus = DBRowStatus.Active
            Header.Description = txtDescription.Text
        Else
            Header = New MaterialPromotionRequestFacade(User).Retrieve(lblRequestNo.Text)
            Header.Description = txtDescription.Text
        End If

        i = New MaterialPromotionRequestFacade(User).Update(Header)
        Return Header
    End Function
    Private Function SaveProfileHeader() As Integer
        Dim nResult As Integer = 0
        Dim Detaillist As ArrayList = sessionHelper.GetSession("AddData")
        If (Not IsNothing(Detaillist)) And Detaillist.Count <> 0 Then
            Dim objHeader As MaterialPromotionRequest = PopulateMaterialPromotionRequestHeader()

            If (lblRequestNo.Text <> "") Then
                Dim objHeaderFacade As MaterialPromotionRequestFacade = New MaterialPromotionRequestFacade(User)
                Dim objDetailFacase As MaterialPromotionRequestDetailFacade = New MaterialPromotionRequestDetailFacade(User)
                Detaillist = CType(sessionHelper.GetSession("AddData"), ArrayList)
                Dim DetailFacade As MaterialPromotionRequestDetailFacade = New MaterialPromotionRequestDetailFacade(User)

                For Each item As MaterialPromotionRequestDetail In Detaillist
                    If item.ID <> 0 Then
                        'Update
                        nResult = DetailFacade.Update(item)
                    Else
                        'Insert
                        item.MaterialPromotionRequest = objHeaderFacade.Retrieve(lblRequestNo.Text)
                        nResult = objDetailFacase.Insert(item)
                    End If
                Next
                MessageAlert(nResult, 0)
            Else
                Dim objHeaderFacade As MaterialPromotionRequestFacade = New MaterialPromotionRequestFacade(User)

                nResult = objHeaderFacade.Insert(objHeader, Detaillist)
                objHeader = New MaterialPromotionRequestFacade(User).Retrieve(nResult)
                lblRequestNo.Text = objHeader.RequestNo
                MessageAlert(nResult, 1)
            End If
        Else
            nResult = 1
            MessageBox.Show("Silahkan masukan detail.")
        End If

        Return nResult
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
    Private Sub UpdateData(ByVal e As DataGridCommandEventArgs)
        Dim mode As Boolean = True

        Dim arrDataUpd As ArrayList = sessionHelper.GetSession("AddData")
        Dim lblNo As Label = e.Item.FindControl("lblNo")
        Dim txtGoodsNo As TextBox = e.Item.FindControl("txtEditGoodsNo")
        Dim txtQuantity As TextBox = e.Item.FindControl("txtEditQuantity")
        Dim txtRequestQuantity As TextBox = e.Item.FindControl("txtEditRequestQuantity")
        Dim Label3 As Label = e.Item.FindControl("Label3")
        Dim txtDescription2 As TextBox = e.Item.FindControl("txtDescription2")
        Dim lblID As Label = e.Item.FindControl("Label3")
        Detail = New MaterialPromotionRequestDetail

        If ((txtRequestQuantity.Text <> String.Empty) And (txtQuantity.Text <> String.Empty)) Then
            objDealer = Session("DEALER")
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                If CInt(txtRequestQuantity.Text) < CInt(txtQuantity.Text) Then
                    MessageBox.Show("Jumlah disetujui tidak boleh lebih besar dari pada Jumlah Permintaan")
                    Exit Sub
                End If
            End If
            If ((CType(txtRequestQuantity.Text, Integer) < 0) Or (CType(txtQuantity.Text, Integer) < 0)) Then
                MessageBox.Show("Check kembali Jumlah Permintaan atau Jumlah disetujui")
                mode = False
            End If
        Else
            mode = False
        End If

        If (mode) Then
            Detail.MaterialPromotion = New MaterialPromotionFacade(User).Retrieve(txtGoodsNo.Text)
            Detail.Qty = txtQuantity.Text
            Detail.RequestQty = txtRequestQuantity.Text
            Detail.Description = txtDescription2.Text

            'Dim id As String = e.Item.Cells(0).Text
            Dim id As String = lblID.Text
            If (id <> "") Then
                Detail.ID = id
            End If
            If (lblRequestNo.Text <> "") Then
                Dim HeaderFacade As MaterialPromotionRequestFacade = New MaterialPromotionRequestFacade(User)
                Detail.MaterialPromotionRequest = HeaderFacade.Retrieve(lblRequestNo.Text)
            End If
            arrDataUpd.RemoveAt(CType(lblNo.Text, Integer) - 1)
            arrDataUpd.Insert((CType(lblNo.Text, Integer) - 1), Detail)

            sessionHelper.SetSession("AddData", arrDataUpd)

            dtgMaterialPromotion.EditItemIndex = -1
            BindDataToGrid()
        Else

        End If

    End Sub
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DealerUser = sessionHelper.GetSession("Dealer")
        If DealerUser.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            InitiateAuthorization()
        End If
        _editDetailPriv = ChecKEditDetailPriv()
        _btnPriv = CheckBtnPriv()
        btnDownload.Visible = CheckDownloadButtonPriv()
        If Not IsPostBack Then
            DealerUser = sessionHelper.GetSession("Dealer")
            BindHeader()
            BindDataToGrid()
            BindRequestNumberSearching()
            EnableControl()
            If DealerUser.Title = EnumDealerTittle.DealerTittle.KTB Then
                btnNew.Visible = False
                btnValidasi.Visible = False
                dtgMaterialPromotion.ShowFooter = False
                dtgMaterialPromotion.Columns(9).Visible = False
            End If
        End If
        lblSearchRequestNo.Visible = _editDetailPriv
        btnSimpan.Visible = _editDetailPriv
    End Sub
    Private Sub dtgMaterialPromotion_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMaterialPromotion.UpdateCommand
        UpdateData(e)
        dtgMaterialPromotion.ShowFooter = True
        DealerUser = sessionHelper.GetSession("Dealer")
        If DealerUser.Title = EnumDealerTittle.DealerTittle.KTB Then
            dtgMaterialPromotion.ShowFooter = False
        End If

    End Sub
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If SaveProfileHeader() = 0 Then
            mode = EnumSaveMode.Mode.SaveDB
            sessionHelper.SetSession("SaveMode", mode)
            sessionHelper.SetSession("Header", Header)
        End If

        'MessageBox.Show("Simpan Header berhasil")
        'Response.Redirect("RequestMaterialPromotion.aspx")
    End Sub
    Private Sub dtgMaterialPromotion_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMaterialPromotion.EditCommand
        Dim lblno As Label = e.Item.FindControl("lblNo")
        dtgMaterialPromotion.EditItemIndex = CType(lblno.Text, Integer) - 1
        dtgMaterialPromotion.ShowFooter = False

        Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
        Dim objDealer As Dealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            lbtnDelete.Visible = bCekDeletePriv
        End If

        BindDataToGrid()
    End Sub
    Private Sub dtgMaterialPromotion_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMaterialPromotion.CancelCommand
        dtgMaterialPromotion.EditItemIndex = -1
        dtgMaterialPromotion.ShowFooter = True
        DealerUser = sessionHelper.GetSession("Dealer")
        If DealerUser.Title = EnumDealerTittle.DealerTittle.KTB Then
            dtgMaterialPromotion.ShowFooter = False
        End If
        e.Item.Cells(9).Visible = False
        BindDataToGrid()
    End Sub
    Private Sub dtgMaterialPromotion_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMaterialPromotion.DeleteCommand
        Dim arrData As ArrayList = sessionHelper.GetSession("AddData")
        Header = sessionHelper.GetSession("Header")
        Dim lbl1 As Label = e.Item.Cells(0).FindControl("lblNo")

        mode = CType(sessionHelper.GetSession("SaveMode"), Integer)
        If mode = EnumSaveMode.Mode.SaveArrayList Then
            arrData.Remove(arrData.Item(CType(lbl1.Text, Integer) - 1))
            sessionHelper.SetSession("AddData", arrData)
            Dim obj As MaterialPromotionRequestDetail
            obj = New MaterialPromotionRequestDetailFacade(User).Retrieve(CType(e.CommandArgument, Integer))
            If Not IsNothing(obj) Then
                Dim nResult As Integer = New MaterialPromotionRequestDetailFacade(User).DeleteFromDB(obj)
            End If
            BindDataToGrid()
        End If
    End Sub
    Private Sub dtgMaterialPromotion_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMaterialPromotion.ItemCommand
        If e.CommandName = "Add" Then
            AddDataToGrid(e)
        End If
    End Sub
    Private Sub dtgMaterialPromotion_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMaterialPromotion.ItemDataBound
        objDealer = Session("DEALER")
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim objHead As MaterialPromotionRequestDetail = CType(e.Item.DataItem, MaterialPromotionRequestDetail)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "Dealer.DealerCode", lblDealerCode.Text))
            criterias.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "MaterialPromotion.ID", objHead.MaterialPromotion.ID))
            Dim _arr As ArrayList = New MaterialPromotionAllocationFacade(User).Retrieve(criterias)
            Dim lblAlokasiQuantity As Label = CType(e.Item.FindControl("lblAlokasiQuantity"), Label)
            If _arr.Count > 0 Then
                lblAlokasiQuantity.Text = CType(_arr(0), MaterialPromotionAllocation).ValidateQty.ToString
            Else
                lblAlokasiQuantity.Text = "0"
            End If
            e.Item.Cells(8).Visible = _editDetailPriv
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            If DealerUser.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
                lbtnDelete.Visible = bCekDeletePriv
            End If

        End If
        If e.Item.ItemType = ListItemType.EditItem Then
            Dim lblID As Label = CType(e.Item.FindControl("Label1"), Label)
            Dim lbtnPopUpEdit As Label = CType(e.Item.FindControl("lbtnPopUpEdit"), Label)
            Dim txtEditQuantity As TextBox = CType(e.Item.FindControl("txtEditQuantity"), TextBox)
            Dim txtEditRequestQuantity As TextBox = CType(e.Item.FindControl("txtEditRequestQuantity"), TextBox)
            Dim txtDescription2 As TextBox = CType(e.Item.FindControl("txtDescription2"), TextBox)
            lbtnPopUpEdit.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpMaterialPromotionforRequest.aspx", "", 500, 760, "MatPro")
            Dim txtEditGoodsNo As TextBox = e.Item.FindControl("txtEditGoodsNo")

            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            If objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                'e.Item.Cells(5).Visible = False
                txtEditQuantity.Enabled = False
                lbtnDelete.Visible = bCekDeletePriv
            Else
                txtEditGoodsNo.Enabled = False
                lbtnPopUpEdit.Visible = _btnPriv
                txtEditRequestQuantity.Enabled = False
            End If
        End If

        If e.Item.ItemType = ListItemType.Footer Then
            Dim lbtnPopUpFooter As Label = CType(e.Item.FindControl("lbtnPopUpFooter"), Label)
            Dim txtFooterQuantity As TextBox = CType(e.Item.FindControl("txtFooterQuantity"), TextBox)
            Dim txtDescription1 As TextBox = CType(e.Item.FindControl("txtDescription1"), TextBox)
            lbtnPopUpFooter.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpMaterialPromotionforRequest.aspx", "", 500, 760, "MatPro")
            lbtnPopUpFooter.Visible = _btnPriv
            Dim lbtnAdd As LinkButton = CType(e.Item.FindControl("lbtnAdd"), LinkButton)
            If objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                'e.Item.Cells(5).Visible = False
                txtFooterQuantity.Enabled = False
                'Bugs 1024 - Dealer can't entry Approve Quantity
                txtFooterQuantity.Text = "0"
                lbtnAdd.Visible = _btnPriv
            End If
        End If

        If (Not IsNothing(Request.QueryString("mode"))) Then
            If Request.QueryString("mode") = "View" Then
                e.Item.Cells(8).Visible = False
                e.Item.Cells(9).Visible = False
            End If
        End If

    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        sessHelp.RemoveSession("AddData")
        Response.Redirect("RequestMaterialPromotion.aspx")
    End Sub
    Private Sub btnValidasi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidasi.Click
        Dim nResult As Integer
        Dim objHeaderFacade As MaterialPromotionRequestFacade = New MaterialPromotionRequestFacade(User)
        Header = objHeaderFacade.Retrieve(lblRequestNo.Text)
        Header.Status = EnumMaterialPromotion.MaterialPromotionStatus.Validasi
        nResult = objHeaderFacade.Update(Header)
        MessageAlert(nResult, 0)
        BindHeader()
    End Sub
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("ListMaterialPromotionRequest.aspx")
    End Sub
    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        ' Add the sub for btnDownload by Ikhsan, 23 September 2008
        ' Requested by Rina as part Of CR
        ' to enable download button       

        Dim i As Integer = 1
        Dim success As Boolean = False
        Dim Connect As Boolean = False
        Dim sw As StreamWriter
        Dim filename = String.Format("{0}{1}{2}", "MatPromRequest", Date.Now.ToString("ddMMyyyyHHmmss"), ".xls")
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("DownloadAllocMatPromotion") & "\" & filename  '-- Destination file

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate

        If (Connect = False) Then
            imp = New SAPImpersonate(_user, _password, _webServer)
            Dim finfo As New FileInfo(DestFile)
            Try
                success = imp.Start()
                If success Then
                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If
                    sw = New StreamWriter(DestFile)
                    Connect = True
                End If
            Catch ex As Exception
                Throw ex
                Exit Sub
            End Try
        End If

        Dim strTab As String = Chr(9)
        sw.WriteLine("PERMINTAAN MATERIAL PROMOSI")
        sw.WriteLine("Nomer Permintaan" + strTab + lblRequestNo.Text)
        sw.WriteLine("Kode Dealer" + strTab + lblDealerCode.Text)
        sw.WriteLine("Nama Dealer" + strTab + lblDealerName.Text)
        sw.WriteLine("Kota" + strTab + lblCity.Text)
        sw.WriteLine("Tanggal Permintaan" + strTab + lblRequestDate.Text)
        sw.WriteLine("Status" + strTab + lblStatus.Text)
        sw.WriteLine("Keterangan" + strTab + txtDescription.Text)

        sw.WriteLine("No" + strTab + "Kode Barang" + strTab + "Nama Barang" + strTab + "Jumlah Alokasi" + strTab + "Jumlah Permintaan" + strTab + "Jumlah Disetujui" + strTab + "Keterangan")

        Dim arrTempData As ArrayList = sessionHelper.GetSession("AddData")

        For Each item As MaterialPromotionRequestDetail In arrTempData
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "Dealer.DealerCode", lblDealerCode.Text))
            criterias.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "MaterialPromotion.ID", item.MaterialPromotion.ID))
            Dim _arr As ArrayList = New MaterialPromotionAllocationFacade(User).Retrieve(criterias)

            Dim ValidQty As String = "0"

            If _arr.Count > 0 Then
                ValidQty = CType(_arr(0), MaterialPromotionAllocation).ValidateQty.ToString
            End If

            sw.WriteLine(i.ToString + strTab + item.MaterialPromotion.GoodNo + strTab + item.MaterialPromotion.Name + strTab + ValidQty + strTab + item.RequestQty.ToString + strTab + item.Qty.ToString + strTab + item.Description.ToString)
            i = i + 1
        Next
        sw.WriteLine("")
        sw.WriteLine("Catatan :")
        sw.WriteLine("Pengambilan barang agar dapat dilakukan segera")
        sw.WriteLine("setelah mendapat persetujuan dari KTB.")
        

        sw.WriteLine("Jakarta ..............., " + Date.Now.Year.ToString)
        sw.WriteLine("")
        sw.WriteLine("Diketahui Oleh" + strTab + "Diterima Oleh                    Diserahkan Oleh")
        sw.WriteLine("")
        sw.WriteLine("")
        sw.WriteLine("")
        sw.WriteLine("")
        sw.WriteLine("Pimpinan Dealer" + strTab + "PIC Dealer                      Staff Gudang Promosi")

        
        If (success = True) Then
            sw.Close()
            Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("DownloadAllocMatPromotion") & "\" & filename
            imp.StopImpersonate()
            imp = Nothing
            Response.Redirect("../Download.aspx?file=" & PathFile)
        Else
            MessageBox.Show("Download file Material Promotion Allocation gagal")
        End If
    End Sub
    Private Sub btnCetak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCetak.Click
        Response.Redirect("RequestMaterialPromotionPrintPreview.aspx?id=" & Request.QueryString("id"))
    End Sub
#End Region

    
End Class
