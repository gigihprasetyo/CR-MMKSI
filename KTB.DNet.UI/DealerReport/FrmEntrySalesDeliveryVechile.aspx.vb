Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.BusinessFacade.Salesman

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security


Public Class FrmEntrySalesDeliveryVechile
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents txtTujuan As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUp As System.Web.UI.WebControls.Label
    Protected WithEvents dgSalesDelieveryVechile As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoRegDelivery As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents optDealer As System.Web.UI.WebControls.RadioButton
    Protected WithEvents optCustomer As System.Web.UI.WebControls.RadioButton
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNama As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents lblAlamat As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKota As System.Web.UI.WebControls.Label
    Protected WithEvents icTglDelivery As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents txtRefDO As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents txtSalesman As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblShowSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents lblTipe As System.Web.UI.WebControls.Label
    Protected WithEvents lblTujuan As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglPengiriman As System.Web.UI.WebControls.Label
    Protected WithEvents lblRefDO As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaSales As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents lblLevel As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents lblJabatan As System.Web.UI.WebControls.Label
    Protected WithEvents IsAbort As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnAbort As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"

    Private sessHelper As New SessionHelper
    Private oDealer As New Dealer
    Private oLoginUser As New UserInfo

    Private oDeliveryCustomerHeader As DeliveryCustomerHeader
    Private oDeliveryCustomerHeaderFacade As New DeliveryCustomerHeaderFacade(User)
    Private oDeliveryCustomerDetailFacade As New DeliveryCustomerDetailFacade(User)

    Private Mode As enumMode.Mode

    Private bEditPriv As Boolean


#End Region

#Region "PrivateCustomMethods"
#Region "Check Privilage"

    Private Sub InitiateAuthorization()
        Dim isAccesablelevelOne As Boolean = SecurityProvider.Authorize(context.User, SR.KirimKendaraanDetailView_Privilege)
        Dim isAccesablelevelTwo As Boolean = SecurityProvider.Authorize(context.User, SR.KirimKendaraanListView_Privilege)

        If Not (isAccesablelevelOne Or isAccesablelevelTwo) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Dealer Report - Pengiriman Kendaraan")
        End If

        bEditPriv = SecurityProvider.Authorize(context.User, SR.KirimKendaraanDetailEdit_Privilege)
    End Sub

#End Region

    Private Sub ClearData()
        lblNoRegDelivery.Text = ""
        optCustomer.Checked = False
        optDealer.Checked = True
        txtTujuan.Text = ""
        lblNama.Text = ""
        lblAlamat.Text = ""
        lblKota.Text = ""
        icTglDelivery.Value = DateTime.Today
        txtRefDO.Text = ""
    End Sub
    Private Sub LoadForm()
        lblNoRegDelivery.Text = oDeliveryCustomerHeader.RegDONumber
        If Not (oDeliveryCustomerHeader.Dealer Is Nothing OrElse oDeliveryCustomerHeader.Dealer.ID = 0) Then
            lblTipe.Text = "Dealer"
            optDealer.Checked = True
            optCustomer.Checked = False
            txtTujuan.Text = oDeliveryCustomerHeader.Dealer.DealerCode
            lblTujuan.Text = oDeliveryCustomerHeader.Dealer.DealerCode
            lblNama.Text = oDeliveryCustomerHeader.Dealer.DealerName
            lblAlamat.Text = oDeliveryCustomerHeader.Dealer.Address
            lblKota.Text = oDeliveryCustomerHeader.Dealer.City.CityName
        ElseIf Not (oDeliveryCustomerHeader.Customer Is Nothing OrElse oDeliveryCustomerHeader.Customer.ID = 0) Then
            lblTipe.Text = "Customer"
            optDealer.Checked = False
            optCustomer.Checked = True
            txtTujuan.Text = oDeliveryCustomerHeader.Customer.Code
            lblTujuan.Text = oDeliveryCustomerHeader.Customer.Code
            lblNama.Text = oDeliveryCustomerHeader.Customer.Name1
            lblAlamat.Text = oDeliveryCustomerHeader.Customer.Alamat
            lblKota.Text = oDeliveryCustomerHeader.Customer.City.CityName
        End If
        icTglDelivery.Value = oDeliveryCustomerHeader.PostingDate
        lblTglPengiriman.Text = oDeliveryCustomerHeader.PostingDate.ToString("dd/MM/yyyy")
        txtRefDO.Text = oDeliveryCustomerHeader.ReffDONumber
        lblRefDO.Text = oDeliveryCustomerHeader.ReffDONumber
        If oDeliveryCustomerHeader.SalesmanID <> 0 Then
            Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(oDeliveryCustomerHeader.SalesmanID)
            txtSalesman.Text = objSalesmanHeader.SalesmanCode
            lblSalesman.Text = objSalesmanHeader.SalesmanCode
            lblNamaSales.Text = objSalesmanHeader.Name
            lblLevel.Text = objSalesmanHeader.SalesmanLevel.Description
            lblJabatan.Text = objSalesmanHeader.JobPosition.Description
        End If
    End Sub
    Private Sub fillForm()
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        lblDealer.Text = oDealer.DealerCode & "/" & oDealer.DealerName

        If Mode = enumMode.Mode.NewItemMode Then
            ClearData()
        ElseIf Mode = enumMode.Mode.EditMode OrElse Mode = enumMode.Mode.ViewMode Then
            LoadForm()
        End If
        BindDetail()
        setFormView()
    End Sub
    Private Sub BindDetail()
        dgSalesDelieveryVechile.DataSource = CType(sessHelper.GetSession("DeliveryCustomerDetail"), ArrayList)
        dgSalesDelieveryVechile.DataBind()
    End Sub
    Private Function ChassisNumberIsExist(ByVal ChassisMasterID As Integer, ByVal DetailCollection As ArrayList) As Boolean
        Dim i As Integer
        Dim bResult As Boolean = False

        If DetailCollection.Count > 0 Then
            For Each _detail As DeliveryCustomerDetail In DetailCollection
                If _detail.ChassisMaster.ID = ChassisMasterID Then
                    bResult = True
                    Exit For
                End If
            Next
        End If
        Return bResult
    End Function
    Private Function ReffDOIsExist(ByVal NoReffDO As String) As Boolean
        Dim bResult As Boolean = False

        Dim criterias As New CriteriaComposite(New Criteria(GetType(DeliveryCustomerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(DeliveryCustomerHeader), "ReffDONumber", MatchType.Exact, NoReffDO.Trim))

        Dim tempArr As ArrayList = oDeliveryCustomerHeaderFacade.Retrieve(criterias)

        If tempArr.Count = 0 Then
            Return False
        End If

        Return bResult
    End Function

    Private Function ValidateSave() As Boolean
        If optCustomer.Checked = False And optDealer.Checked = False Then
            MessageBox.Show("Silakan pilih salah satu Tipe")
            Return False
        Else
            If txtTujuan.Text <> String.Empty Then
                ' data session must be valid
                RetrieveTujuanInfo(txtTujuan.Text)
                If IsNothing(sessHelper.GetSession("InfoTujuan")) Then
                    If optDealer.Checked = True Then
                        MessageBox.Show("Masukan tujuan dealer yang valid")
                    End If

                    If optCustomer.Checked = True Then
                        MessageBox.Show("Masukan tujuan customer yang valid")
                    End If
                    Return False
                End If
            End If
        End If

        If txtTujuan.Text = String.Empty Then
            MessageBox.Show("Silakan pilih tujuan.")
            Return False
        End If

        If ReffDOIsExist(txtRefDO.Text) Then
            MessageBox.Show("No Reff DO telah pernah di gunakan.")
            Return False
        End If

        If Not (icTglDelivery.Value.Month = DateTime.Today.Month And icTglDelivery.Value.Year = DateTime.Today.Year) Then
            MessageBox.Show("Periode pengiriman telah di tutup")
            Return False
        End If

        If txtSalesman.Text.Trim <> String.Empty Then
            Dim objSalesman As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(txtSalesman.Text.Trim)
            If objSalesman Is Nothing Or objSalesman.ID = 0 Then
                MessageBox.Show("Salesman tidak valid")
                Return False
            End If

            Dim objDealer As Dealer = New SessionHelper().GetSession("DEALER")
            If objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                If objSalesman.Dealer.ID <> CType(New SessionHelper().GetSession("DEALER"), Dealer).ID Then
                    MessageBox.Show("Salesman tidak valid")
                    Return False
                End If
            End If

            If objSalesman.SalesIndicator <> EnumSalesmanUnit.SalesmanUnit.Unit Then
                MessageBox.Show("Unit salesman tidak valid")
                Return False
            End If

        End If

        If optDealer.Checked = True Then
            Dim objDealer As Dealer = sessHelper.GetSession("DEALER")
            If objDealer.DealerCode = txtTujuan.Text Then
                MessageBox.Show("Dealer tujuan sama dengan Dealer asal.")
                Return False
            End If
        End If

        If CType(sessHelper.GetSession("DeliveryCustomerDetail"), ArrayList).Count < 1 Then
            MessageBox.Show("Belum ada No Rangka yang diisi")
            Return False
        End If

        Return True

    End Function

    Private Sub LoadTujuanInfo()
        If optDealer.Checked = True Then
            If Not IsNothing(sessHelper.GetSession("InfoTujuan")) Then
                Dim _dealer As Dealer = CType(sessHelper.GetSession("InfoTujuan"), Dealer)
                lblNama.Text = _dealer.DealerName
                lblAlamat.Text = _dealer.Address
                lblKota.Text = _dealer.City.CityName
            End If
        ElseIf optCustomer.Checked = True Then
            If Not IsNothing(sessHelper.GetSession("InfoTujuan")) Then
                Dim _customer As Customer = CType(sessHelper.GetSession("InfoTujuan"), Customer)
                lblNama.Text = _customer.Name1
                lblAlamat.Text = _customer.Alamat
                lblKota.Text = _customer.City.CityName
            End If
        End If
    End Sub

    Private Function RetrieveTujuanInfo(ByVal code As String)
        Dim result As Object
        Dim arrTmp As ArrayList
        If optDealer.Checked = True Then
            Dim _dealer As Dealer = New DealerFacade(User).Retrieve(txtTujuan.Text.Trim)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, txtTujuan.Text.Trim))

            arrTmp = New DealerFacade(User).Retrieve(criterias)
            If Not IsNothing(arrTmp) Then
                If arrTmp.Count > 0 Then
                    result = _dealer
                Else
                    result = Nothing
                End If
            End If
        ElseIf optCustomer.Checked = True Then
            Dim _customer As Customer = New CustomerFacade(User).Retrieve(txtTujuan.Text.Trim)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Customer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Customer), "Code", MatchType.Exact, txtTujuan.Text.Trim))
            arrTmp = New CustomerFacade(User).Retrieve(criterias)
            If Not IsNothing(arrTmp) Then
                If arrTmp.Count > 0 Then
                    result = _customer
                Else
                    result = Nothing
                End If
            End If
        End If

        sessHelper.SetSession("InfoTujuan", result)
    End Function
    Private Sub setFormView()
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        oDeliveryCustomerHeader = CType(sessHelper.GetSession("DeliveryCustomerHeader"), DeliveryCustomerHeader)
        If Mode = enumMode.Mode.NewItemMode OrElse Mode = enumMode.Mode.EditMode Then
            optCustomer.Visible = True
            optDealer.Visible = True
            txtTujuan.Visible = True
            lblPopUp.Visible = True
            txtSalesman.Visible = True
            lblShowSalesman.Visible = True
            icTglDelivery.Visible = True
            txtRefDO.Visible = True


            lblTipe.Visible = False
            lblTujuan.Visible = False
            lblSalesman.Visible = False
            lblTglPengiriman.Visible = False
            lblRefDO.Visible = False

            dgSalesDelieveryVechile.ShowFooter = True
            dgSalesDelieveryVechile.Columns(dgSalesDelieveryVechile.Columns.Count - 1).Visible = True


            If bEditPriv Then
                btnSimpan.Visible = True
            Else
                btnSimpan.Visible = bEditPriv
            End If


            If Mode = enumMode.Mode.NewItemMode Then
                btnCancel.Visible = False
            ElseIf Mode = enumMode.Mode.EditMode Then
                btnCancel.Visible = True
            End If

        ElseIf Mode = enumMode.Mode.ViewMode Then
            optCustomer.Visible = False
            optDealer.Visible = False
            txtTujuan.Visible = False
            lblPopUp.Visible = False
            txtSalesman.Visible = False
            lblShowSalesman.Visible = False
            icTglDelivery.Visible = False
            txtRefDO.Visible = False


            lblTipe.Visible = True
            lblTujuan.Visible = True
            lblSalesman.Visible = True
            lblTglPengiriman.Visible = True
            lblRefDO.Visible = True

            dgSalesDelieveryVechile.ShowFooter = False
            dgSalesDelieveryVechile.Columns(dgSalesDelieveryVechile.Columns.Count - 1).Visible = False

            If bEditPriv Then
                btnSimpan.Visible = False
            Else
                btnSimpan.Visible = bEditPriv
            End If
            btnCancel.Visible = True

        End If

    End Sub


#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not txtTujuan.Text = String.Empty Then
            RetrieveTujuanInfo(txtTujuan.Text)
            'Dim tujuan As Object = sessHelper.GetSession("InfoTujuan")
            'If tujuan Is Nothing Then
            'End If
            LoadTujuanInfo()
        End If

        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)

        If Not IsPostBack Then
            If Request.Params("Mode").ToString() = "New" Then
                ViewState("Mode") = enumMode.Mode.NewItemMode
                oDeliveryCustomerHeader = New DeliveryCustomerHeader
                sessHelper.SetSession("DeliveryCustomerHeader", oDeliveryCustomerHeader)
                sessHelper.SetSession("DeliveryCustomerDetail", New ArrayList)
                sessHelper.SetSession("DeletedDeliveryCustomerDetail", New ArrayList)
            ElseIf Request.Params("Mode").ToString() = "Edit" Then
                ViewState("Mode") = enumMode.Mode.EditMode
                oDeliveryCustomerHeader = oDeliveryCustomerHeaderFacade.Retrieve(CInt(Request.Params("ID")))
                sessHelper.SetSession("DeliveryCustomerHeader", oDeliveryCustomerHeader)
                sessHelper.SetSession("DeliveryCustomerDetail", oDeliveryCustomerHeader.DeliveryCustomerDetails)
                sessHelper.SetSession("DeletedDeliveryCustomerDetail", New ArrayList)
            ElseIf Request.Params("Mode").ToString() = "View" Then
                ViewState("Mode") = enumMode.Mode.ViewMode
                oDeliveryCustomerHeader = oDeliveryCustomerHeaderFacade.Retrieve(CInt(Request.Params("ID")))
                sessHelper.SetSession("DeliveryCustomerHeader", oDeliveryCustomerHeader)
                sessHelper.SetSession("DeliveryCustomerDetail", oDeliveryCustomerHeader.DeliveryCustomerDetails)
                sessHelper.SetSession("DeletedDeliveryCustomerDetail", New ArrayList)
            End If
            fillForm()
            lblPopUp.Attributes("onClick") = "ShowPPTujuanSelection();"
            lblShowSalesman.Attributes("onClick") = "ShowSalesmanSelection();"
        End If
        CheckIsAbort()

    End Sub

    Private Sub CheckIsAbort()
        btnAbort.Visible = False
        oDeliveryCustomerHeader = CType(sessHelper.GetSession("DeliveryCustomerHeader"), DeliveryCustomerHeader)
        If CType(sessHelper.GetSession("DeliveryCustomerDetail"), ArrayList).Count < 1 Then
            If oDeliveryCustomerHeader.ID <> 0 Then
                btnAbort.Visible = True
            End If
        End If
    End Sub
    Private Sub dgSalesDelieveryVechile_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSalesDelieveryVechile.ItemDataBound
        If e.Item.ItemType = ListItemType.Footer Then
            Dim lblPopUp As Label = CType(e.Item.Cells(1).FindControl("lblSearchChassisFooter"), Label)
            lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpChassisMasterSelection.aspx", "", 710, 700, "GetSelectedChassisCode")
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.EditItem Then
            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            e.Item.Cells(0).Controls.Add(lNum)
        End If
    End Sub

    Private Sub displaySalesman()
        If txtSalesman.Text = "" Then
            Exit Sub
        End If
        Dim objSalesman As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(txtSalesman.Text.Trim)
        If objSalesman.ID = 0 Then
            Exit Sub
        End If

        lblNamaSales.Text = objSalesman.Name
        lblLevel.Text = objSalesman.SalesmanLevel.Description
        lblJabatan.Text = objSalesman.JobPosition.Description

    End Sub

    Private Sub dgSalesDelieveryVechile_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesDelieveryVechile.ItemCommand
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        Dim _arrDetail As ArrayList = CType(sessHelper.GetSession("DeliveryCustomerDetail"), ArrayList)

        Select Case e.CommandName
            Case "Add" 'Add datagrid item 

                displaySalesman()

                Dim txtChassisMasterNumber As TextBox = CType(e.Item.FindControl("txtChassisNumberFooter"), TextBox)
                Dim objChassisMaster As ChassisMaster

                If IsNothing(txtChassisMasterNumber) OrElse txtChassisMasterNumber.Text = String.Empty Then
                    MessageBox.Show("No Rangka tidak boleh kosong")
                    Return
                End If

                'If Not CommonFunction.IsExistChassisNumberInLoginDealer(txtChassisMasterNumber.Text, oDealer.ID, User) Then
                '    If Not CommonFunction.IsExistChassisNumberInStockMovement(txtChassisMasterNumber.Text, oDealer.ID, User) Then
                '        MessageBox.Show("No Rangka tidak terdaftar dalam dealer anda")
                '        Return
                '    End If
                'End If
                Dim objDealer As Dealer = sessHelper.GetSession("DEALER")
                objChassisMaster = New ChassisMasterFacade(User).Retrieve(txtChassisMasterNumber.Text.Trim())

                If Not (IsNothing(objChassisMaster) OrElse objChassisMaster.ID = 0) Then
                    If Not ChassisNumberIsExist(objChassisMaster.ID, _arrDetail) Then
                        Dim _DeliveryCustomerDetail As DeliveryCustomerDetail = New DeliveryCustomerDetail
                        _DeliveryCustomerDetail.ChassisMaster = objChassisMaster
                        If objChassisMaster.FakturStatus = 0 Then
                            If objChassisMaster.StockStatus <> "X" Then
                                If objDealer.ID = objChassisMaster.StockDealer Then
                                    _arrDetail.Add(_DeliveryCustomerDetail)
                                    sessHelper.SetSession("DeliveryCustomerDetail", _arrDetail)
                                Else
                                    MessageBox.Show("'No Rangka Bukan Stok dealer " & objDealer.SearchTerm1)
                                End If
                            Else
                                MessageBox.Show("No Rangka bukan Stock")
                            End If
                        Else
                            MessageBox.Show("No Rangka sudah di proses.")
                        End If
                    Else
                        MessageBox.Show(SR.DataIsExist("Chassis Number"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Chassis Number"))
                End If
            Case "Delete" 'Delete this datagrid item 
                If Mode = enumMode.Mode.NewItemMode Then
                    _arrDetail.RemoveAt(e.Item.ItemIndex)
                ElseIf Mode = enumMode.Mode.EditMode Then
                    oDeliveryCustomerHeader = CType(sessHelper.GetSession("DeliveryCustomerHeader"), DeliveryCustomerHeader)
                    Dim deletedDetail As DeliveryCustomerDetail = CType(_arrDetail(e.Item.ItemIndex), DeliveryCustomerDetail)

                    If deletedDetail.ChassisMaster.FakturStatus <> 0 Then
                        MessageBox.Show("No Rangka telah di kirim. Proses hapus di batalkan")
                        Return
                    End If


                    If Not IsNothing(oDeliveryCustomerHeader.Dealer) Then
                        If deletedDetail.ChassisMaster.StockDealer <> oDeliveryCustomerHeader.Dealer.ID Then
                            MessageBox.Show("No Rangka telah berubah posisi. Proses hapus di batalkan")
                            Return
                        End If

                    End If

                    If deletedDetail.ID > 0 Then
                        Dim deletedArrLst As ArrayList
                        deletedArrLst = CType(sessHelper.GetSession("DeletedDeliveryCustomerDetail"), ArrayList)
                        If deletedArrLst Is Nothing Then
                            deletedArrLst = New ArrayList
                        End If
                        deletedArrLst.Add(deletedDetail)
                        sessHelper.SetSession("DeletedDeliveryCustomerDetail", deletedArrLst)
                    End If
                    _arrDetail.RemoveAt(e.Item.ItemIndex)
                    sessHelper.SetSession("DeliveryCustomerDetail", _arrDetail)
                End If
        End Select
        BindDetail()
        CheckIsAbort()
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Server.Transfer("~/DealerReport/FrmListSalesDeliveryVechile.aspx")
    End Sub
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim result As Integer

        If ValidateSave() Then

            Mode = CType(ViewState("Mode"), enumMode.Mode)
            oDeliveryCustomerHeader = CType(sessHelper.GetSession("DeliveryCustomerHeader"), DeliveryCustomerHeader)

            oDeliveryCustomerHeader.FromDealer = oDealer.ID

            If optDealer.Checked = True Then
                Dim _dealer As Dealer = New DealerFacade(User).Retrieve(txtTujuan.Text.Trim)
                If Not IsNothing(sessHelper.GetSession("InfoTujuan")) Then
                    oDeliveryCustomerHeader.Dealer = _dealer
                    oDeliveryCustomerHeader.Customer = Nothing
                End If
            ElseIf optCustomer.Checked = True Then
                Dim _customer As Customer = New CustomerFacade(User).Retrieve(txtTujuan.Text.Trim)
                If Not IsNothing(sessHelper.GetSession("InfoTujuan")) Then
                    oDeliveryCustomerHeader.Dealer = Nothing
                    oDeliveryCustomerHeader.Customer = _customer
                End If
            End If

            oDeliveryCustomerHeader.PostingDate = icTglDelivery.Value
            oDeliveryCustomerHeader.ReffDONumber = txtRefDO.Text

            If txtSalesman.Text.Trim <> String.Empty Then
                oDeliveryCustomerHeader.SalesmanID = New SalesmanHeaderFacade(User).Retrieve(txtSalesman.Text.Trim).ID
            Else
                oDeliveryCustomerHeader.SalesmanID = Nothing
            End If

            'If oDeliveryCustomerHeader.DeliveryCustomerDetails.Count < 1 Then
            '    MessageBox.Show("Tidak ada detail yang disimpan")
            '    Return
            'End If

            If Mode = enumMode.Mode.NewItemMode Then
                oDeliveryCustomerHeader.RegDONumber = oDealer.DealerCode & "/"
                result = oDeliveryCustomerHeaderFacade.InsertTransaction(oDeliveryCustomerHeader, CType(sessHelper.GetSession("DeliveryCustomerDetail"), ArrayList))
            ElseIf Mode = enumMode.Mode.EditMode Then
                oDeliveryCustomerHeader.RegDONumber = lblNoRegDelivery.Text
                result = oDeliveryCustomerHeaderFacade.UpdateTransaction(oDeliveryCustomerHeader, CType(sessHelper.GetSession("DeliveryCustomerDetail"), ArrayList), CType(sessHelper.GetSession("DeletedDeliveryCustomerDetail"), ArrayList))
            End If

            If result > 0 Then

                sessHelper.SetSession("DeliveryCustomerHeader", oDeliveryCustomerHeaderFacade.Retrieve(result))
                oDeliveryCustomerHeader = CType(sessHelper.GetSession("DeliveryCustomerHeader"), DeliveryCustomerHeader)

                sessHelper.SetSession("DeliveryCustomerDetail", oDeliveryCustomerHeader.DeliveryCustomerDetails)
                sessHelper.SetSession("DeliveryCustomerDetailDeleted", New ArrayList)

                ViewState("Mode") = enumMode.Mode.EditMode
                fillForm()

                MessageBox.Show("Data Berhasil Disimpan")
                'Server.Transfer("~/DealerReport/FrmListSalesDeliveryVechile.aspx")
            Else
                MessageBox.Show("Save Fail!")
            End If

        Else
            'MessageBox.Show("Data tidak lengkap. Silakan diperiksa lagi ")
        End If

    End Sub

    Private Sub btnAbort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAbort.Click

        Dim result As Integer
        Dim ErrMessage As String = String.Empty

        oDeliveryCustomerHeader = CType(sessHelper.GetSession("DeliveryCustomerHeader"), DeliveryCustomerHeader)
        result = oDeliveryCustomerHeaderFacade.BatalSalesDeliveryVechile(oDeliveryCustomerHeader, CType(sessHelper.GetSession("DeletedDeliveryCustomerDetail"), ArrayList))
        If (result = -1) Then
            If ErrMessage = String.Empty Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(ErrMessage)
            End If
        Else
            MessageBox.Show(SR.SaveSuccess)
            Server.Transfer("~/DealerReport/FrmListSalesDeliveryVechile.aspx")
        End If

    End Sub

#End Region

End Class
