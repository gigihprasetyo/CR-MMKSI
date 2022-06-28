#Region "Custom Namespace Imports"
Imports KTB.DNET.Domain
Imports KTB.DNET.BusinessFacade
Imports KTB.DNET.BusinessFacade.FinishUnit
Imports KTB.DNET.BusinessFacade.Helper
Imports KTB.DNET.BusinessFacade.Service
Imports KTB.DNET.BusinessFacade.Salesman
Imports KTB.DNET.BusinessFacade.DealerReport
Imports KTB.DNET.BusinessFacade.General
Imports KTB.DNET.BusinessFacade.LKPP
Imports KTB.DNET.Utility
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.Security
Imports KTB.DNET.WebCC
Imports KTB.DNET.BusinessFacade.Profile
Imports System.Linq
Imports System.Collections.Generic

#End Region

Public Class FrmEntryInvoiceRevisionFaktur
    Inherits System.Web.UI.Page

#Region "Private Variables"
    Private ssHelper As SessionHelper = New SessionHelper
    Private arl As ArrayList = New ArrayList
    Private arlDEL As ArrayList = New ArrayList
    Dim _arrDetail As New ArrayList
    Dim _arrDetailDel As New ArrayList
    Private blockFaktur As String = "Block_Faktur_"
    Private unBlockFaktur As String = "unBlokFaktur_"

    Private _objChassisMaster As ChassisMaster
    Private _objRevisionFaktur As RevisionFaktur
    Private _objEndCustomer As EndCustomer
    Private _formMode As Integer
    Dim isDealerDMS As Boolean = False

#End Region

#Region "Event Handlers"
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        initForm()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
            lblsearchLKPP.Visible = False
            Me.hdnIsLKPP.Value = "-1"
            Me.hdnLKPPConfirmation.Value = "-1"
            Me.hdnVerifyLKPP.Value = "-1"
            ActivateUserPrivilege()
            InitData()
            BindDataGrid()
            txtSalesmanCode.Attributes.Add("readonly", "readonly")
            txtSPKNumber.Attributes.Add("readonly", "readonly")
            txtLKPPNumber.Attributes.Add("readonly", "readonly")
            txtCustomerCode.Attributes.Add("readonly", "readonly")
            'Dim dealerSystems As DealerSystems = New DealerSystemsFacade(User).RetrieveByDealerCode(objDealer.DealerCode)

            'If Not IsNothing(dealerSystems) AndAlso dealerSystems.SystemID <> 1 Then
            '    isDealerDMS = True
            'End If
            'ssHelper.SetSession("isDealerDMS", isDealerDMS)
        Else
            If Request("__EVENTTARGET") <> "txtSPKOther" Then
                If hdnValid.Value.ToString() = "-1" Then
                    Session("Customer") = Nothing
                End If
                If Me.hdnIsLKPP.Value = "1" Then
                    If Me.hdnLKPPConfirmation.Value = "-1" Then
                        btnSave_Click(Nothing, Nothing)
                    ElseIf Me.hdnLKPPConfirmation.Value = "1" Then
                        If Me.hdnVerifyLKPP.Value = "-1" Then
                            btnSave_Click(Nothing, Nothing)
                        ElseIf Me.hdnVerifyLKPP.Value = "1" Then
                            btnSave_Click(Nothing, Nothing)
                        End If
                    End If
                End If

                InitDataFromSession()
            End If
        End If
        lblPopUp.Attributes("onClick") = "ShowPPTujuanSelection();"
        lblShowSalesman.Attributes("onClick") = "ShowSalesmanSelection();"
        lblSPKNumber.Attributes("onClick") = "ShowSPKSelection();"
        lblsearchLKPP.Attributes("onClick") = "ShowLKPPSelection();"
        'If Not IsNothing(ssHelper.GetSession("isDealerDMS")) Then
        '    isDealerDMS = CType(ssHelper.GetSession("isDealerDMS"), Boolean)
        '    If isDealerDMS Then
        '        lblPopUp.Visible = False
        '    End If
        'End If
    End Sub

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        ssHelper.SetSession("Customer", Nothing)
        ssHelper.SetSession("RevisionFaktur", Nothing)

        Dim url As String = CType(Session("FrmEntryInvoiceRevision_CalledBy"), String)
        If Not url Is Nothing Then
            Server.Transfer(url)
        End If
    End Sub

    Private Sub dtgPengajuanFaktur_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgPengajuanFaktur.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim CM As ChassisMaster = CType(e.Item.DataItem, ChassisMaster)
            Dim objEndCust As EndCustomer = CM.EndCustomer
            Dim CM2 As ChassisMaster = New ChassisMaster

            If (e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem) Then
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1

                Dim lblVehicleKind As Label = CType(e.Item.FindControl("lblVehicleKind"), Label)
                If Not IsNothing(CM) AndAlso CM.ID > 0 Then
                    If Not IsNothing(CM.VehicleKind) Then
                        lblVehicleKind.Text = CM.VehicleKind.VehicleKindGroup.Description
                    End If
                End If

                Dim lblVehicleModel As Label = CType(e.Item.FindControl("lblVehicleModel"), Label)
                If Not IsNothing(CM) AndAlso CM.ID > 0 Then
                    If Not IsNothing(CM.VehicleKind) Then
                        lblVehicleModel.Text = CM.VehicleKind.Description
                    End If
                End If

                CM2 = New FinishUnit.ChassisMasterFacade(User).Retrieve(objEndCust.RefChassisNumberID)
                Dim lblNoRangkaPengganti As Label = CType(e.Item.FindControl("lblNoRangkaPengganti"), Label)
                If (Not CM2 Is Nothing) Then
                    lblNoRangkaPengganti.Text = CM2.ChassisNumber
                End If

                Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                lbtnDelete.Attributes.Add("onclick", "return confirm('Hapus data?');")
                Dim lbtnEdit As LinkButton = e.Item.FindControl("lbtnEdit")

                'Dim oCM As ChassisMaster = New ChassisMasterFacade(User).Retrieve(CM.ID)

                If _formMode = EnumDNET.enumFormMode.View Then
                    lbtnEdit.Visible = False
                    lbtnDelete.Visible = False

                    Dim chkSelect As CheckBox = CType(e.Item.FindControl("chkSelect"), CheckBox)
                    chkSelect.Visible = False
                Else
                    If IsNothing(_objRevisionFaktur) Then
                        lbtnEdit.Visible = True
                        lbtnDelete.Visible = True

                        If Not IsNothing(CM) Then
                            If Not IsNothing(CM.VehicleKind) Then
                                lbtnEdit.Visible = True
                            Else
                                lbtnEdit.Visible = False
                            End If
                        Else
                            lbtnEdit.Visible = False
                        End If
                    Else
                        lbtnEdit.Visible = False
                        lbtnDelete.Visible = False

                        If _objRevisionFaktur.RevisionStatus > 0 Then
                            Dim chkSelect As CheckBox = CType(e.Item.FindControl("chkSelect"), CheckBox)
                            chkSelect.Visible = False
                        End If
                    End If

                End If
            ElseIf e.Item.ItemType = ListItemType.EditItem Then
                Dim icEditMaxDate As IntiCalendar = CType(e.Item.FindControl("icEditMaxDate"), IntiCalendar)
                icEditMaxDate.Value = CM.EndCustomer.FakturDate

                Dim txtEditNoRangkaPengganti As TextBox = CType(e.Item.FindControl("txtEditNoRangkaPengganti"), TextBox)
                CM2 = New FinishUnit.ChassisMasterFacade(User).Retrieve(objEndCust.RefChassisNumberID)
                If (Not CM2 Is Nothing) Then
                    txtEditNoRangkaPengganti.Text = CM2.ChassisNumber
                End If
            End If
        End If

        If e.Item.ItemType = ListItemType.Footer Then
            Dim chassisMasterID As Integer = CType(Request.QueryString("ChassisMasterID"), Integer)
            If chassisMasterID <> 0 Then
                _objChassisMaster = New FinishUnit.ChassisMasterFacade(User).Retrieve(chassisMasterID)
                Dim lblNomorRangkaFooter As Label = e.Item.FindControl("lblNomorRangkaFooter")
                Dim ddlVehicleKindF As DropDownList = e.Item.FindControl("ddlVehicleKindF")
                Dim ddlVehicleModelF As DropDownList = e.Item.FindControl("ddlVehicleModelF")
                lblNomorRangkaFooter.Text = _objChassisMaster.ChassisNumber

                RebindVehicleKind(ddlVehicleKindF, ddlVehicleModelF, lblNomorRangkaFooter)
            End If

            Dim icMaxDate As IntiCalendar = CType(e.Item.FindControl("icMaxDate"), IntiCalendar)
            If ssHelper.GetSession("DEFAULTDATE") Is Nothing Then
                icMaxDate.Value = Today.AddDays(1)
            Else
                icMaxDate.Value = ssHelper.GetSession("DEFAULTDATE")
            End If

        End If
    End Sub

    Private Sub dtgPengajuanFaktur_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPengajuanFaktur.ItemCommand
        arl = CType(ssHelper.GetSession("sessCM"), ArrayList)
        _arrDetail = CType(ssHelper.GetSession("DeliveryCustomerDetail"), ArrayList)
        Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
        Dim objCM As New ChassisMaster
        Select Case e.CommandName
            Case "Add"
                addCommand(e)
            Case "delete"
                Try
                    objCM = CType(arl(e.Item.ItemIndex), ChassisMaster)
                    Dim indek As Integer = getIndexDelivery(_arrDetail, objCM.ChassisNumber)
                    If indek <> -1 Then
                        Dim deletedDetail As DeliveryCustomerDetail = CType(_arrDetail(indek), DeliveryCustomerDetail)
                        If deletedDetail.ID > 0 Then
                            Dim deletedArrLst As ArrayList
                            _arrDetailDel = CType(ssHelper.GetSession("DelDetail"), ArrayList)
                            _arrDetailDel.Add(deletedDetail)
                        End If

                        _arrDetail.RemoveAt(indek)
                    End If
                    arlDEL.Add(arl(e.Item.ItemIndex))
                    arl.RemoveAt(e.Item.ItemIndex)
                Catch ex As Exception
                End Try
                BindDataGrid()
                If arl.Count >= 1 Then
                    dtgPengajuanFaktur.ShowFooter = False
                Else
                    dtgPengajuanFaktur.ShowFooter = True
                End If
                txtSPKNumber.Enabled = True
                lblSPKNumber.Visible = True
                txtSPKOther.Enabled = True
            Case "edit"
                ssHelper.SetSession("ADD", Nothing)
                objCM = CType(arl(e.Item.ItemIndex), ChassisMaster)
                Dim cat As String = objCM.Category.CategoryCode
                Dim strID As String = objCM.ID.ToString
                ssHelper.SetSession("NoSPK", txtSPKNumber.Text.Trim)
                ssHelper.SetSession("PREVPAGE", "FrmEntryInvoiceRevisionFaktur.aspx?ChassisMasterID=" & _objChassisMaster.ID & "&mode=" & _formMode)
                Response.Redirect("FrmMasterProfiles.aspx?iseditsingle=1&Cat=" & cat & "&adsfadfadfw32342412412412424=1&kjhadshkf9784323247832493ihdufiue=17&hjkadsdhhdhkjdafkhdafklhdfahdsajklsdfjdsjjkldsaf9874553983423=" & strID)

            Case "cancel"
                dtgPengajuanFaktur.EditItemIndex = -1
                dtgPengajuanFaktur.ShowFooter = True
                BindDataGrid()

            Case "RebindVehicleKind"
                RebindVehicleKind(e)

            Case "RebindVehicleModel"
                BindVehicleModel(e)
        End Select
        ssHelper.SetSession("DelCM", arlDEL)
        ssHelper.SetSession("DelDetail", _arrDetailDel)
        ssHelper.SetSession("sessCM", arl)
        ssHelper.SetSession("DeliveryCustomerDetail", _arrDetail)

    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click, btnValidate.Click
        If lblError.Text.Trim <> String.Empty Then
            MessageBox.Show(lblError.Text.Trim)
            Exit Sub
        End If
        Dim n As Integer
        If Me.isValidCM() = False Then Exit Sub


        Dim aCMs As ArrayList = CType(Session("sessCM"), ArrayList)
        Dim mess As String = String.Empty

        For Each _objChassisMaster As ChassisMaster In aCMs
            If Not CommonFunction.RevFakturIsValidData(_objChassisMaster, mess) Then
                MessageBox.Show("Proses Gagal\n" & mess)
                Exit Sub
            End If
        Next

        Dim oCust As Customer
        If (Not Session("Customer") Is Nothing) Then
            oCust = CType(Session("Customer"), Customer)
        Else
            Dim objCustomerFacade As CustomerFacade = New CustomerFacade(User)
            oCust = objCustomerFacade.Retrieve(txtCustomerCode.Text.Trim)
        End If

        If Not IsNothing(oCust) Then
            If oCust.CreatedTime < New Date(2011, 6, 1) Then
                MessageBox.Show("Data konsumen tidak dpt digunakan, silakan ajukan ulang.")
                Exit Sub
            End If

        End If

        If dtgPengajuanFaktur.Items.Count <> 1 Then
            MessageBox.Show("Pastikan terisi satu data detail rangka sebelum melanjutkan proses")
            Exit Sub
        End If

        If Not IsNothing(oCust) Then
            Dim LKPPStatus As Integer = EnumLKPPStatus.LKPPStatus.NonLKPP
            Dim oLKPP As LKPPHeader

            arl = CType(Session("sessCM"), ArrayList)
            'For Each oCM As ChassisMaster In arl
            '    Dim arrList As ArrayList = New SPKFakturFacade(User).CountSPKDetailCustomer(txtSPKNumber.Text, txtCustomerCode.Text, oCM.VechileColor.ID,
            '                                                                                oCM.VehicleKind.ID, oCM.VechileType)

            '    If arrList.Count > 0 Then
            '        Dim SumQty As Integer = 0
            '        Dim cr As New CriteriaComposite(New Criteria(GetType(SPKDetailCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '        cr.opAnd(New Criteria(GetType(SPKDetailCustomer), "Code", MatchType.Exact, txtCustomerCode.Text))
            '        cr.opAnd(New Criteria(GetType(SPKDetailCustomer), "SPKDetail.VechileColor.ID", MatchType.Exact, oCM.VechileColor.ID))
            '        cr.opAnd(New Criteria(GetType(SPKDetailCustomer), "SPKDetail.VehicleKind.ID", MatchType.Exact, oCM.VehicleKind.ID))
            '        cr.opAnd(New Criteria(GetType(SPKDetailCustomer), "SPKDetail.SPKHeader.SPKNumber", MatchType.Exact, txtSPKNumber.Text))
            '        cr.opAnd(New Criteria(GetType(SPKDetailCustomer), "SPKDetail.VehicleTypeCode", MatchType.Exact, oCM.VechileType))

            '        Dim arrSDCList As ArrayList = New SPKDetailCustomerFacade(User).Retrieve(cr)
            '        If arrSDCList.Count > 0 Then
            '            For Each oSDC As SPKDetailCustomer In arrSDCList
            '                If oSDC.SPKDetail.VechileColor.ID = oCM.VechileColor.ID AndAlso oSDC.SPKDetail.VehicleKind.ID = oCM.VehicleKind.ID Then
            '                    SumQty += oSDC.Quantity
            '                End If
            '            Next
            '        End If

            '        If arrList.Count >= SumQty Then
            '            MessageBox.Show("Pengajuan atas kendaraan dengan Model Tipe Warna untuk Customer yang dipilih sudah melebihi batas.")
            '            Exit Sub
            '        End If
            '    End If
            'Next

            If Not IsNothing(oCust.MyCustomerRequest) AndAlso oCust.MyCustomerRequest.ID > 0 Then
                Dim isMMC As Boolean = False
                arl = CType(Session("sessCM"), ArrayList)
                For Each oCM As ChassisMaster In arl
                    If oCM.VechileColor.VechileType.Category.CategoryCode = "PC" Or oCM.VechileColor.VechileType.Category.CategoryCode = "LCV" Then
                        isMMC = True
                        Exit For
                    End If
                Next

                'LKPP
                If oCust.MyCustomerRequest.Status1 = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah AndAlso isMMC Then

                    If LKPPCheckByVehicleType(txtLKPPNumber.Text.Trim, arl) = False Then Exit Sub

                    MessageBox.Show("MMKSI akan melakukan verifikasi terhadap pengajuan ini.")
                    LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP
                    oCust.MyCustomerRequest.LKPPStatus = LKPPStatus

                    'related to LKPP insert LKPPheaderid on endcustomer 
                    '--------------------------------------------
                    Dim critLKPP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LKPPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    critLKPP.opAnd(New Criteria(GetType(LKPPHeader), "ReferenceNumber", MatchType.Exact, txtLKPPNumber.Text.Trim))
                    oLKPP = New LKPPHeaderFacade(User).Retrieve(critLKPP)(0)
                    If Not IsNothing(oLKPP) Then
                        For Each oCM As ChassisMaster In arl
                            oCM.EndCustomer.LKPPHeader = oLKPP
                            oCM.EndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP
                        Next
                    End If
                    '--------------------------------------------
                Else

                    Dim isGovIndicated As Boolean = False

                    If Me.IsGovernmentInstitution(oCust.MyCustomerRequest.Name1) OrElse Me.IsGovernmentInstitution(oCust.MyCustomerRequest.Name2) Then isGovIndicated = True
                    If isGovIndicated Then
                        For Each oCM As ChassisMaster In arl
                            If isMMC Then
                                oCM.EndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.IndicatedLKPP
                            Else
                                oCM.EndCustomer.MCPStatus = 1       '--"1 = Terindikasi MCP; 0 = Bukan"
                            End If

                        Next
                    End If
                    ViewState("IsGovernmentInstitution") = isGovIndicated

                    'MMC
                    If isMMC Then
                        ' add validasi to profile
                        '--------------------------------------------
                        ViewState("IsGovernmentOwnerShip") = False

                        If Not ssHelper.GetSession("IsSucceedProfileFaktur") Is Nothing Then
                            If ssHelper.GetSession("IsSucceedProfileFaktur") = 0 Then

                                If LKPPCheckByVehicleType(txtLKPPNumber.Text.Trim, arl) = False Then Exit Sub

                                Dim critLKPP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LKPPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                critLKPP.opAnd(New Criteria(GetType(LKPPHeader), "ReferenceNumber", MatchType.Exact, txtLKPPNumber.Text.Trim))
                                oLKPP = New LKPPHeaderFacade(User).Retrieve(critLKPP)(0)
                                If Not IsNothing(oLKPP) Then
                                    For Each objCM As ChassisMaster In arl
                                        objCM.EndCustomer.LKPPHeader = oLKPP
                                    Next
                                End If
                            End If
                        Else
                            Dim _spkDetail As SPKDetail = CType(ssHelper.GetSession("SPKDETAILFAKTUR"), SPKDetail)
                            If Not IsNothing(_spkDetail) Then

                                Dim isProfileAllowed As Boolean = True
                                Dim arlProfile As ArrayList
                                Dim oSPKProfileFac As New SPKProfileFacade(User)
                                Dim cSPKProfile As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                cSPKProfile.opAnd(New Criteria(GetType(SPKProfile), "SPKDetail.ID", MatchType.Exact, _spkDetail.ID))

                                arlProfile = oSPKProfileFac.Retrieve(cSPKProfile)
                                Dim varGroupID As Integer = 0

                                For Each objCM As ChassisMaster In arl
                                    varGroupID = IIf(objCM.Category.CategoryCode.ToUpper() = "PC", 7, 6)
                                Next

                                For Each item As SPKProfile In arlProfile
                                    If (item.ProfileGroup.ID = varGroupID And item.ProfileHeader.ID = 5) AndAlso (item.ProfileValue = "IP" Or item.ProfileValue = "BN") Then
                                        ViewState("IsGovernmentOwnerShip") = True
                                        isProfileAllowed = False

                                        If LKPPCheckByVehicleType(txtLKPPNumber.Text.Trim, arl) = False Then Exit Sub

                                        MessageBox.Show("MMKSI akan melakukan verifikasi terhadap pengajuan ini.")
                                        LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP
                                        oCust.MyCustomerRequest.LKPPStatus = LKPPStatus

                                        Dim critLKPP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LKPPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                        critLKPP.opAnd(New Criteria(GetType(LKPPHeader), "ReferenceNumber", MatchType.Exact, txtLKPPNumber.Text.Trim))
                                        oLKPP = New LKPPHeaderFacade(User).Retrieve(critLKPP)(0)
                                        If Not IsNothing(oLKPP) Then
                                            For Each objCM As ChassisMaster In arl
                                                objCM.EndCustomer.LKPPHeader = oLKPP
                                                objCM.EndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP
                                            Next
                                            isProfileAllowed = True
                                        End If
                                    End If
                                Next

                                If Not isProfileAllowed Then
                                    For Each oCM As ChassisMaster In arl
                                        oCM.EndCustomer.LKPPStatus = 1 ' 1 '--"1 = Terindikasi LKPP; 0 = Bukan"
                                    Next
                                End If
                            End If
                        End If


                        ''CheckPoin3
                        If oCust.MyCustomerRequest.Status1 <> EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah AndAlso (CBool(ViewState("IsGovernmentInstitution"))) AndAlso Not (CBool(ViewState("IsGovernmentOwnerShip"))) Then

                            LKPPStatus = EnumLKPPStatus.LKPPStatus.NonLKPP
                            oCust.MyCustomerRequest.LKPPStatus = LKPPStatus

                            For Each objCM As ChassisMaster In arl
                                objCM.EndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.IndicatedLKPP
                            Next

                            If (txtLKPPNumber.Text.Trim() <> "") Then
                                Dim critLKPP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LKPPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                critLKPP.opAnd(New Criteria(GetType(LKPPHeader), "ReferenceNumber", MatchType.Exact, txtLKPPNumber.Text.Trim))
                                Dim xOLKPP As ArrayList = New LKPPHeaderFacade(User).Retrieve(critLKPP)
                                If xOLKPP.Count > 0 Then
                                    oLKPP = CType(xOLKPP(0), LKPPHeader)
                                    If Not IsNothing(oLKPP) Then
                                        For Each objCM As ChassisMaster In arl
                                            objCM.EndCustomer.LKPPHeader = oLKPP
                                        Next
                                    End If
                                End If
                            End If
                        End If

                        'Check Point 4
                        If (isMMC = True) AndAlso (Not CBool(ViewState("IsGovernmentInstitution"))) AndAlso (oCust.MyCustomerRequest.Status1 <> EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah) AndAlso txtLKPPNumber.Text.Trim() <> "" AndAlso Not (CBool(ViewState("IsGovernmentOwnerShip"))) Then
                            MessageBox.Show("LKPP Hanya diperuntukan untuk tipe pelanggan BUMN dan pemerintah")
                            Exit Sub
                        ElseIf (isMMC = True) AndAlso (Not CBool(ViewState("IsGovernmentInstitution"))) AndAlso (oCust.MyCustomerRequest.Status1 <> EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah) AndAlso txtLKPPNumber.Text.Trim() = "" AndAlso Not (CBool(ViewState("IsGovernmentOwnerShip"))) Then
                            LKPPStatus = EnumLKPPStatus.LKPPStatus.NonLKPP
                            oCust.MyCustomerRequest.LKPPStatus = LKPPStatus

                            For Each objCM As ChassisMaster In arl
                                objCM.EndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.NonLKPP
                            Next
                        End If

                        ''CheckPoin2 & 5
                        If oCust.MyCustomerRequest.Status1 <> EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah AndAlso (CBool(ViewState("IsGovernmentOwnerShip"))) Then
                            '
                            'Chek isi dan vechile kind
                            If LKPPCheckByVehicleType(txtLKPPNumber.Text.Trim, arl) = False Then Exit Sub

                            LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP
                            oCust.MyCustomerRequest.LKPPStatus = LKPPStatus

                            Dim critLKPP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LKPPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            critLKPP.opAnd(New Criteria(GetType(LKPPHeader), "ReferenceNumber", MatchType.Exact, txtLKPPNumber.Text.Trim))
                            oLKPP = New LKPPHeaderFacade(User).Retrieve(critLKPP)(0)
                            If Not IsNothing(oLKPP) Then
                                For Each objCM As ChassisMaster In arl
                                    objCM.EndCustomer.LKPPHeader = oLKPP
                                    objCM.EndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP
                                Next
                            End If
                        End If
                    End If
                End If

            End If

            If Session("MODE") = "insert" Then
                arl = CType(Session("sessCM"), ArrayList)
                If (arl.Count > 0) Then

                    '-------------------------
                    Dim oCustRequest As CustomerRequest = New CustomerRequestFacade(User).RetrieveCodeDesc(oCust.Code)
                    If Not IsNothing(oCustRequest) AndAlso oCustRequest.ID > 0 Then
                        Dim oCustRequestProfile As CustomerRequestProfile = oCustRequest.GetCustomerRequestProfile("NOKTP")
                        If Not IsNothing(oCustRequestProfile) AndAlso oCustRequestProfile.ID > 0 Then
                            If Not IsNothing(oCustRequestProfile.ProfileValue) Then
                                If oCustRequest.Status1 <> CType(EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah, Short) Then
                                    If oCustRequestProfile.ProfileValue.Trim.Length <= 5 Then
                                        MessageBox.Show("Faktur tidak bisa disimpan karena konsumen tdk punya data KTP/TDP. Silakan ajukan ulang konsumen tsb.")
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If oCustRequest.Status1 <> CType(EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah, Short) Then
                                    MessageBox.Show("Faktur tidak bisa disimpan karena konsumen tdk punya data KTP/TDP. Silakan ajukan ulang konsumen tsb.")
                                    Exit Sub
                                End If
                            End If
                        Else
                            If oCustRequest.Status1 <> CType(EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah, Short) Then
                                MessageBox.Show("Faktur tidak bisa disimpan karena konsumen tdk punya data KTP/TDP. Silakan ajukan ulang konsumen tsb.")
                                Exit Sub
                            End If
                        End If
                    Else

                    End If

                    '-------------------------

                    'Related to SPK on 200110330
                    '------------------------
                    Dim SPKMandatory As String = KTB.DNet.Lib.WebConfig.GetValue("SPKMandatory")
                    If Date.Now < New Date(2011, 5, 1) Then
                        SPKMandatory = "0"
                    End If

                    Dim noSPK As String = CType(ssHelper.GetSession("NoSPK"), String)

                    If SPKMandatory = "1" And noSPK = String.Empty Then
                        MessageBox.Show("No. Registrasi SPK harus diisi")
                        Exit Sub
                    End If

                    If noSPK <> String.Empty Then
                        Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(SPKHeader), "SPKNumber", MatchType.Exact, noSPK))
                        Dim objSPKHeaderList As ArrayList = New FinishUnit.SPKHeaderFacade(User).Retrieve(criterias)
                        If objSPKHeaderList.Count > 0 Then
                            Dim objSPKHeader As SPKHeader = objSPKHeaderList(0)

                            Dim arrevfak As New ArrayList
                            Dim cri As New CriteriaComposite(New Criteria(GetType(RevisionFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            cri.opAnd(New Criteria(GetType(RevisionFaktur), "ChassisMaster.ID", MatchType.Exact, CType(arl.Item(0), ChassisMaster).ID))
                            cri.opAnd(New Criteria(GetType(RevisionFaktur), "RevisionStatus", MatchType.No, 4))

                            arrevfak = New RevisionFakturFacade(User).Retrieve(cri)

                            If arrevfak.Count = 0 Then
                                n = New EndCustomerFacade(User).InsertTransactionPengajuanRevisiFaktur(oCust, arl, objSPKHeader)
                            Else
                                MessageBox.Show("Nomor Rangka Ini Sudah Dibuatkan Revisi Faktur")
                                Return
                            End If
                            'Use for Update status if revision finish
                            'CopyCustomerDealerToDealerSPK(oCust)

                            If txtSalesmanCode.Text.Trim <> String.Empty Then
                                If txtSalesmanCode.Text.Trim <> objSPKHeader.SalesmanHeader.SalesmanCode.Trim Then
                                    Dim criteriasSPKHeader As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                    criteriasSPKHeader.opAnd(New Criteria(GetType(SalesmanHeader), "SalesmanCode", MatchType.Exact, txtSalesmanCode.Text.Trim))
                                    Dim objSalesmanHeaderList As ArrayList = New SalesmanHeaderFacade(User).Retrieve(criteriasSPKHeader)
                                    If objSalesmanHeaderList.Count > 0 Then
                                        Dim objSalesmanHeader As SalesmanHeader = objSalesmanHeaderList(0)
                                        objSPKHeader.SalesmanHeader = objSalesmanHeader
                                        Dim oSPKHeaderFacade As SPKHeaderFacade = New SPKHeaderFacade(User)
                                        oSPKHeaderFacade.Update(objSPKHeader)
                                    End If
                                End If
                            End If
                            'end modified
                        Else
                            MessageBox.Show("No. SPK tidak ada")
                            Exit Sub
                        End If
                    Else
                        n = New EndCustomerFacade(User).InsertTransactionPengajuanRevisiFaktur(oCust, arl)

                        'Use for Update status if revision finish
                        'CopyCustomerDealerToDealerSPK(oCust)
                    End If
                    '-------------------------

                    If (n <> -1) Then
                        SaveChassisMasterProfile()
                        ' Use for Update status if revision finish
                        'If (Not Session("Salesman") Is Nothing) Then
                        '    GroupingDeliveryCustomer(CType(Session("Salesman"), SalesmanHeader), CType(Session("Customer"), Customer), CType(ssHelper.GetSession("DEALER"), Dealer), arl)
                        'Else
                        '    GroupingDeliveryCustomer(Nothing, CType(Session("Customer"), Customer), CType(ssHelper.GetSession("DEALER"), Dealer), arl)
                        'End If

                        'no need for revision, Use for Update status if revision finish
                        'For Each model As ChassisMaster In arl
                        'UpdatePendingDescAndLastUpdateProfile(model.ChassisNumber)
                        'Next

                        MessageBox.Show("Pengajuan revisi faktur sukses")
                        Session("MODE") = "update"
                        btnUpdateProfil.Enabled = True
                        btnSave.Enabled = False

                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(RevisionFaktur), "ChassisMaster.ID", MatchType.Exact, _objChassisMaster.ID))
                        criterias.opAnd(New Criteria(GetType(RevisionFaktur), "OldEndCustomer.ID", MatchType.Exact, _objChassisMaster.EndCustomerID))
                        Dim objListRevisionFaktur As ArrayList = New RevisionFakturFacade(User).Retrieve(criterias)
                        If objListRevisionFaktur.Count > 0 Then
                            _objRevisionFaktur = CType(objListRevisionFaktur(0), RevisionFaktur)
                            ssHelper.SetSession("RevisionFaktur", _objRevisionFaktur)
                        End If
                    Else
                        MessageBox.Show("Pengajuan revisi faktur gagal")
                    End If
                Else
                    MessageBox.Show("Tidak ada data dalam list")
                    Exit Sub
                End If
            ElseIf Session("MODE") = "update" Then
                arl = CType(Session("sessCM"), ArrayList)
                If (arl.Count > 0) Then
                    'SaveChassisMasterProfile()
                    If hdnValidationState.Value.ToString = "1" Then
                        Dim objRevisionFakturFac As RevisionFakturFacade = New RevisionFakturFacade(User)
                        n = New EndCustomerFacade(User).UpdateTransactionPengajuanRevisiFaktur(oCust, arl)
                        If n <> -1 Then
                            _objRevisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Validasi ' "1"
                            _objRevisionFaktur.NewValidationDate = Now
                            _objRevisionFaktur.NewValidationBy = User.Identity.Name
                            n = New RevisionFakturFacade(User).Update(_objRevisionFaktur)
                            If n <> -1 Then
                                MessageBox.Show(SR.ValidateSucces())

                                _objRevisionFaktur = New RevisionFakturFacade(User).Retrieve(_objRevisionFaktur.ID)
                                ssHelper.SetSession("RevisionFaktur", _objRevisionFaktur)
                                ssHelper.SetSession("ChassisMaster", _objRevisionFaktur.ChassisMaster)
                                Server.Transfer("FrmEntryInvoiceRevisionFaktur.aspx?ChassisMasterID=" & _objChassisMaster.ID & "&mode=" & EnumDNET.enumFormMode.Edit)
                            End If
                        End If

                        If n = -1 Then
                            MessageBox.Show(SR.ValidateFail)
                        End If

                    Else
                        n = New EndCustomerFacade(User).UpdateTransactionPengajuanRevisiFaktur(oCust, arl)

                        If (n <> -1) Then
                            MessageBox.Show("Pengajuan faktur sukses")
                            ssHelper.SetSession("MODE", "update")

                            If _formMode = EnumDNET.enumFormMode.Add Then
                                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                criterias.opAnd(New Criteria(GetType(RevisionFaktur), "ChassisMaster.ID", MatchType.Exact, _objChassisMaster.ID))
                                criterias.opAnd(New Criteria(GetType(RevisionFaktur), "EndCustomer.ID", MatchType.Exact, _objEndCustomer.ID))
                                Dim objListRevisionFaktur As ArrayList = New RevisionFakturFacade(User).Retrieve(criterias)
                                If objListRevisionFaktur.Count > 0 Then
                                    _objRevisionFaktur = CType(objListRevisionFaktur(0), RevisionFaktur)
                                    ssHelper.SetSession("RevisionFaktur", _objRevisionFaktur)
                                    ssHelper.SetSession("ChassisMaster", _objRevisionFaktur.ChassisMaster)
                                    Server.Transfer("FrmEntryInvoiceRevisionFaktur.aspx?ChassisMasterID=" & _objChassisMaster.ID & "&mode=" & EnumDNET.enumFormMode.Edit)
                                End If
                            Else
                                _objRevisionFaktur = New RevisionFakturFacade(User).Retrieve(_objRevisionFaktur.ID)
                                ssHelper.SetSession("RevisionFaktur", _objRevisionFaktur)
                                ssHelper.SetSession("ChassisMaster", _objRevisionFaktur.ChassisMaster)
                                Server.Transfer("FrmEntryInvoiceRevisionFaktur.aspx?ChassisMasterID=" & _objChassisMaster.ID & "&mode=" & EnumDNET.enumFormMode.Edit)
                            End If
                        Else
                            MessageBox.Show("Pengajuan faktur gagal")
                        End If
                    End If

                    ' New EndCustomerFacade(User).UpdateTransaction(oCust, arl, CType(Session("DelCM"), ArrayList))
                    'If (n <> -1) Then
                    '    If txtSalesmanCode.Text.Trim <> String.Empty Then
                    '        If (Not Session("Salesman") Is Nothing) Then
                    '            GroupingDeliveryCustomer(CType(Session("Salesman"), SalesmanHeader), CType(Session("Customer"), Customer), CType(ssHelper.GetSession("DEALER"), Dealer), arl)
                    '        Else
                    '            MessageBox.Show("Data sales tidak ditemukan")
                    '        End If
                    '    End If

                    '    MessageBox.Show("Pengajuan faktur sukses")
                    '    ssHelper.SetSession("MODE", "update")
                    '    'ssHelper.SetSession("DelCM", New ArrayList)
                    '    btnUpdateProfil.Enabled = True
                    'Else
                    '    MessageBox.Show("Pengajuan faktur gagal")
                    'End If
                Else
                    MessageBox.Show("Tidak ada data dalam list")
                    Exit Sub
                End If
            End If
        Else
            MessageBox.Show("Data konsumen tidak ditemukan")
            Exit Sub
        End If

    End Sub

    Private Sub btnUpdateProfil_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateProfil.Click
        Dim bcheck As Boolean = False
        Dim success As Boolean = False

        dtgPengajuanFaktur.DataSource = CType(Session("sessCM"), ArrayList)
        For Each dtgItem As DataGridItem In dtgPengajuanFaktur.Items
            If CType(dtgItem.Cells(0).FindControl("chkSelect"), CheckBox).Checked Then
                bcheck = True
                Exit For
            End If
        Next

        If bcheck Then
            Dim CheckedItemColl As ArrayList = New ArrayList
            CheckedItemColl = GetCheckedItem()

            Dim objPMColl As ArrayList = New ArrayList
            If CheckedItemColl.Count > 0 Then
                Dim strID As String = ""
                Dim cat As String = ""
                For Each ObjCM As ChassisMaster In CheckedItemColl
                    'strID = strID + ObjCM.ID.ToString + "-"
                    strID = ObjCM.ID.ToString
                    cat = ObjCM.Category.CategoryCode
                Next
                ssHelper.SetSession("PREVPAGE", "FrmEntryInvoiceRevisionFaktur.aspx?ChassisMasterID=" & _objChassisMaster.ID & "&mode=" & EnumDNET.enumFormMode.Edit)
                ssHelper.SetSession("UpdateProfile", True)
                ssHelper.SetSession("ADD", Nothing)

                If txtLKPPNumber.Text.Trim <> String.Empty Then
                    ssHelper.SetSession("LKPPNUMBER", txtLKPPNumber.Text.Trim)
                End If
                ssHelper.SetSession("FrmMasterProfiles_CalledBy", "FrmEntryInvoiceRevisionFaktur.aspx")
                Response.Redirect("FrmMasterProfiles.aspx?Cat=" & cat & "&adsfadfadfw32342412412412424=1&kjhadshkf9784323247832493ihdufiue=17&hjkadsdhhdhkjdafkhdafklhdfahdsajklsdfjdsjjkldsaf9874553983423=" & strID)
            End If
        Else
            MessageBox.Show("Pilih chassis yang ingin di update")
        End If
    End Sub

    Private Sub btnCancelValidate_Click(sender As Object, e As EventArgs) Handles btnCancelValidate.Click
        If IsCancelValidateValid(_objRevisionFaktur) Then
            _objRevisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Baru
            _objRevisionFaktur.NewValidationDate = Date.Parse("1/1/1753 12:00:00 AM")
            _objRevisionFaktur.NewValidationBy = String.Empty

            Dim iUpdated As Integer = New RevisionFakturFacade(User).Update(_objRevisionFaktur)
            If iUpdated <> -1 Then
                MessageBox.Show(SR.UpdateSucces)

                _objRevisionFaktur = New RevisionFakturFacade(User).Retrieve(_objRevisionFaktur.ID)
                ssHelper.SetSession("RevisionFaktur", _objRevisionFaktur)
                ssHelper.SetSession("ChassisMaster", _objRevisionFaktur.ChassisMaster)
                Server.Transfer("FrmEntryInvoiceRevisionFaktur.aspx?ChassisMasterID=" & _objChassisMaster.ID & "&mode=" & EnumDNET.enumFormMode.Edit)
            Else
                MessageBox.Show(SR.UpdateFail)
            End If
        Else
            MessageBox.Show("Proses batal tidak berhasil, karena data sudah di proses")
        End If
    End Sub

    Private Sub txtSPKOther_TextChanged(sender As Object, e As EventArgs) Handles txtSPKOther.TextChanged
        If txtSPKOther.Text.Trim <> String.Empty Then
            If txtSPKNumber.Text.Trim <> String.Empty Then
                MessageBox.Show("Sebelum mengisi SPK Other, delete value SPK Number terlebih dahulu!")
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPKHeader), "SPKNumber", MatchType.Exact, txtSPKOther.Text.Trim))
            Dim objNewSPKHeaderList As ArrayList = New ArrayList
            objNewSPKHeaderList = New FinishUnit.SPKHeaderFacade(User).Retrieve(criterias)
            Dim newSPKHeader As SPKHeader = New SPKHeader
            If objNewSPKHeaderList.Count > 0 Then
                newSPKHeader = CType(objNewSPKHeaderList(0), SPKHeader)
                If newSPKHeader.SPKReferenceNumber <> _objEndCustomer.SPKFaktur.SPKHeader.SPKNumber Then
                    MessageBox.Show("SPK Reference Number pada SPK Other tidak sesuai.")
                    txtSPKNumber.Text = String.Empty
                    Exit Sub
                End If
                ClearCustomer()

                txtSalesmanCode.Text = newSPKHeader.SalesmanHeader.SalesmanCode
                lblNamaSales.Text = newSPKHeader.SalesmanHeader.Name
                lblLevel.Text = newSPKHeader.SalesmanHeader.SalesmanLevel.Description
                lblPosisi.Text = newSPKHeader.SalesmanHeader.JobPosition.Description
                If Not IsNothing(newSPKHeader.SalesmanHeader.DealerBranch) Then
                    lblDealerBranch.Text = newSPKHeader.SalesmanHeader.DealerBranch.DealerBranchCode
                End If
                If Not IsNothing(newSPKHeader.CustomerRequest) Then
                    txtCustomerCode.Text = newSPKHeader.CustomerRequest.CustomerCode
                    lbltxtCustomerCode.Text = newSPKHeader.CustomerRequest.CustomerCode
                Else
                    MessageBox.Show("SPK belum menjadi Konsumen.")
                End If

                If Me.txtCustomerCode.Text.Trim <> String.Empty Then
                    If panelSPKOther.Visible And txtSPKOther.Text <> String.Empty Then
                        GetCustomerInfoForSPKMatch(txtCustomerCode.Text)
                    Else
                        GetCustomerInfo(Me.txtCustomerCode.Text.Trim)
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub btntxtCustomerCode_Click(sender As Object, e As EventArgs) Handles btntxtCustomerCode.Click
        Dim oSPKDetailCust As SPKDetailCustomer = New SPKDetailCustomerFacade(User).Retrieve(CInt(hfSPKDetailCustomerID.Value))
        If Not IsNothing(oSPKDetailCust.SPKDetail) Then
            If Not IsNothing(oSPKDetailCust.SPKDetail.VechileColor) Then
                If Not IsNothing(oSPKDetailCust.SPKDetail.VechileColor.VechileType) Then
                    Dim oLKPPDetail As LKPPDetail = New LKPPDetailFacade(User).RetrieveByHeaderAndVtype(oSPKDetailCust.LKPPReference, oSPKDetailCust.SPKDetail.VechileColor.VechileType.ID)
                    If oLKPPDetail.ID <> 0 AndAlso oLKPPDetail.UnitRemain > 0 Then
                        txtLKPPNumber.Text = oSPKDetailCust.LKPPReference
                    End If
                    'If oSPKDetailCust.LKPPReference.Trim.Length > 0 Then
                    '    txtLKPPNumber.Text = oSPKDetailCust.LKPPReference
                    'End If
                End If
                hdnVC.Value = oSPKDetailCust.SPKDetail.VechileColor.ID
                lblsearchLKPP.Visible = True
            End If
        End If
        lblsearchLKPP.Visible = True
    End Sub
#End Region

#Region "Custome Methods"
    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.RevisiFakturDaftarLihat_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Pengajuan Faktur")
        End If

        Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If _formMode = EnumDNET.enumFormMode.Add Then
                Me.dtgPengajuanFaktur.Columns(10).Visible = SecurityProvider.Authorize(Context.User, SR.RevisiFakturInput_Privilege)
            ElseIf _formMode = EnumDNET.enumFormMode.Edit Then
                Me.dtgPengajuanFaktur.Columns(10).Visible = SecurityProvider.Authorize(Context.User, SR.RevisiFakturDaftarEdit_Privilege)
            End If
        End If
    End Sub

    Private Sub initForm()
        _formMode = CType(Request.QueryString("mode"), Integer)
        _objChassisMaster = CType(ssHelper.GetSession("ChassisMaster"), ChassisMaster)
        _objRevisionFaktur = CType(ssHelper.GetSession("RevisionFaktur"), RevisionFaktur)

        If Not _objRevisionFaktur Is Nothing Then
            _objEndCustomer = New EndCustomer
            _objEndCustomer = _objRevisionFaktur.EndCustomer
        Else
            _objEndCustomer = New EndCustomer
            _objEndCustomer = _objChassisMaster.EndCustomer
        End If
    End Sub

    Private Sub InitData()

        Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
        Dim objUser As UserInfo = CType(ssHelper.GetSession("LOGINUSERINFO"), UserInfo)

        If Not objDealer Is Nothing Then
            ' Dealer Detail
            lblRevisionType.Text = New RevisionTypeFacade(User).Retrieve(2).Description
            lblKodeDealer.Text = objDealer.DealerCode
            lblNamaDealer.Text = objDealer.DealerName
            If Not IsNothing(_objRevisionFaktur) Then
                Me.lblStatus.Text = CType(_objRevisionFaktur.RevisionStatus, EnumDNET.enumFakturKendaraanRev).ToString()
            End If

            Select Case _formMode
                Case EnumDNET.enumFormMode.View
                    txtSPKNumber.Text = _objEndCustomer.RevisionSPKFaktur.SPKHeader.SPKNumber

                    arl = New ArrayList
                    arl.Add(_objChassisMaster)
                    ssHelper.SetSession("sessCM", arl)

                    'GetCustomerInfo(_objEndCustomer.RevisionSPKFaktur.SPKHeader.CustomerRequest.CustomerCode)
                    If Not IsNothing(ssHelper.GetSession("Customer")) Then
                        GetCustomerInfo(CType(ssHelper.GetSession("Customer"), Customer).Code)
                    End If


                    txtSalesmanCode.Text = _objEndCustomer.RevisionSPKFaktur.SPKHeader.SalesmanHeader.SalesmanCode
                    BindSales(_objEndCustomer.RevisionSPKFaktur.SPKHeader.SalesmanHeader)

                    If Not IsNothing(_objEndCustomer.LKPPHeader) Then
                        txtLKPPNumber.Text = _objEndCustomer.LKPPHeader.ReferenceNumber
                        lblinstitutionName2.Text = _objEndCustomer.LKPPHeader.GovInstName
                    End If

                    DisableFrmControl()

                Case EnumDNET.enumFormMode.Edit
                    txtSPKNumber.Text = _objEndCustomer.RevisionSPKFaktur.SPKHeader.SPKNumber

                    'ssHelper.SetSession("SPKDETAILFAKTUR", _objEndCustomer.RevisionSPKFaktur.SPKHeader.SPKDetails)

                    arl = New ArrayList
                    arl.Add(_objChassisMaster)
                    ssHelper.SetSession("sessCM", arl)

                    'GetCustomerInfo(_objEndCustomer.RevisionSPKFaktur.SPKHeader.CustomerRequest.CustomerCode)
                    If Not IsNothing(ssHelper.GetSession("Customer")) Then
                        txtCustomerCode.Text = CType(ssHelper.GetSession("Customer"), Customer).Code
                        GetCustomerInfo(CType(ssHelper.GetSession("Customer"), Customer).Code)
                    End If

                    txtSalesmanCode.Text = _objEndCustomer.RevisionSPKFaktur.SPKHeader.SalesmanHeader.SalesmanCode
                    BindSales(_objEndCustomer.RevisionSPKFaktur.SPKHeader.SalesmanHeader)
                    ssHelper.SetSession("Salesman", _objEndCustomer.RevisionSPKFaktur.SPKHeader.SalesmanHeader)

                    If Not IsNothing(_objEndCustomer.LKPPHeader) Then
                        txtLKPPNumber.Text = _objEndCustomer.LKPPHeader.ReferenceNumber
                        lblinstitutionName2.Text = _objEndCustomer.LKPPHeader.GovInstName
                        ssHelper.SetSession("LKPPNUMBER", txtLKPPNumber.Text.Trim)
                    End If

                    If _objRevisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Baru Then
                        btnUpdateProfil.Enabled = True
                        btnValidate.Enabled = True
                    Else
                        DisableFrmControl()
                        If _objRevisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Validasi Then
                            btnCancelValidate.Enabled = True
                        End If
                    End If
                    ssHelper.SetSession("MODE", "update")
                Case EnumDNET.enumFormMode.Add
                    Dim dealerSystems As DealerSystems = New DealerSystemsFacade(User).Retrieve(objDealer.ID)
                    'Dim dealerSystems As DealerSystems = New DealerSystemsFacade(User).Retrieve(_objEndCustomer.SPKFaktur.SPKHeader.Dealer.ID)

                    If Not IsNothing(dealerSystems) Then
                        'If dealerSystems.isSPKMatchFaktur Then
                        panelSPKOther.Visible = True
                        'End If
                    End If

                    If Not Session("PrevPage") Is Nothing Then
                        'If Not ssHelper.GetSession("NoSPK") Is Nothing Then
                        '    txtSPKNumber.Text = CType(ssHelper.GetSession("NoSPK"), String)
                        '    txtSPKNumber.Enabled = False
                        '    lblSPKNumber.Visible = False
                        'End If

                        arl = CType(ssHelper.GetSession("sessCM"), ArrayList)
                        ssHelper.SetSession("sessCM", arl)
                        txtCustomerCode.Text = CType(ssHelper.GetSession("Customer"), Customer).Code
                        If Not ssHelper.GetSession("Salesman") Is Nothing Then
                            txtSalesmanCode.Text = CType(ssHelper.GetSession("Salesman"), SalesmanHeader).SalesmanCode
                            GetSalesInfo(CType(ssHelper.GetSession("Salesman"), SalesmanHeader).SalesmanCode)
                        End If
                        If Not IsNothing(CType(Session("Customer"), Customer)) Then
                            BindCustomer(CType(Session("Customer"), Customer))
                        Else
                            BindCustomer(New Customer)
                        End If

                        If Not ssHelper.GetSession("UpdateProfile") Is Nothing Then
                            btnSave.Enabled = False
                            btnUpdateProfil.Enabled = True
                            If Not ssHelper.GetSession("IsSucceedProfileFaktur") Is Nothing Then
                                If ssHelper.GetSession("IsSucceedProfileFaktur") = 0 Then
                                    ssHelper.SetSession("MODE", "update")
                                    btnSave.Enabled = True
                                End If
                            End If
                        End If

                        If Not ssHelper.GetSession("LKPPNUMBER") Is Nothing Then
                            txtLKPPNumber.Text = CType(ssHelper.GetSession("LKPPNUMBER"), String)
                        End If
                    Else
                        ssHelper.SetSession("sessCM", arl)
                        ssHelper.SetSession("DeliveryCustomerDetail", _arrDetail)

                        Dim strCustomerRequestId As String
                        Dim CustomerRequestId As String
                        Dim CustomerCode As String
                        If Not Request.QueryString("qxctrvvyuotrpn") Is Nothing Then
                            strCustomerRequestId = Request.QueryString("qxctrvvyuotrpn")
                            CustomerRequestId = strCustomerRequestId.Split(";")(0)
                            CustomerCode = strCustomerRequestId.Split(";")(1)
                            txtCustomerCode.Text = CustomerCode
                            If Me.txtCustomerCode.Text.Trim <> String.Empty Then
                                GetCustomerInfo(txtCustomerCode.Text)
                            End If

                        End If
                        ssHelper.SetSession("MODE", "insert")
                    End If
            End Select
        End If
    End Sub

    Private Function GetSalesInfo(ByVal code As String)
        Dim bcheck As Boolean = True
        Dim objSalesFacade As SalesmanHeaderFacade = New SalesmanHeaderFacade(User)
        Dim objSales As SalesmanHeader = objSalesFacade.Retrieve(code)
        Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
        If objSales.ID > 0 Then
            'CR
            ssHelper.SetSession("Salesman", objSales)
            BindSales(objSales)
            bcheck = True
        Else
            MessageBox.Show("Kode Salesman Tidak Terdaftar")
            bcheck = False
        End If
        Return bcheck
    End Function

    Private Sub BindSales(ByVal objSales As SalesmanHeader)
        lblPosisi.Text = objSales.JobPosition.Description
        lblLevel.Text = objSales.SalesmanLevel.Description
        lblNamaSales.Text = objSales.Name

        If Not IsNothing(objSales.DealerBranch) Then
            lblDealerBranch.Text = objSales.DealerBranch.DealerBranchCode & " / " & objSales.DealerBranch.Term1
        End If
    End Sub

    Private Function GetCustomerInfo(ByVal code As String)
        Dim bcheck As Boolean = True
        Dim objCustomerFacade As CustomerFacade = New CustomerFacade(User)
        Dim objCust As Customer = objCustomerFacade.Retrieve(code)
        Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
        If objCust.ID > 0 Then
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                ssHelper.SetSession("Customer", objCust)
                bcheck = True
                BindCustomer(objCust)
                If objCust.CreatedTime < New Date(2011, 6, 1) Then
                    MessageBox.Show("Data konsumen tidak dpt digunakan, silakan ajukan ulang.")
                    bcheck = False
                End If

            ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If IsCustomerAvailaibleForLoginDealer(objCust, objDealer) Then
                    ssHelper.SetSession("Customer", objCust)
                    bcheck = True
                    BindCustomer(objCust)
                    If objCust.CreatedTime < New Date(2011, 6, 1) Then
                        MessageBox.Show("Data konsumen tidak dpt digunakan, silakan ajukan ulang.")
                        bcheck = False
                    End If
                Else
                    MessageBox.Show("Customer tidak terdaftar di dealer anda.")
                    bcheck = False
                End If
            End If
        Else
            MessageBox.Show("Customer tidak ditemukan")
            bcheck = False
        End If
        Return bcheck
    End Function

    Private Function GetCustomerInfoForSPKMatch(ByVal code As String) As Boolean
        Dim bcheck As Boolean = True
        Dim objCustomerFacade As CustomerFacade = New CustomerFacade(User)
        Dim objCust As Customer = objCustomerFacade.Retrieve(code)
        If objCust.ID > 0 Then
            ssHelper.SetSession("Customer", objCust)
            bcheck = True
            BindCustomer(objCust)
            If objCust.CreatedTime < New Date(2011, 6, 1) Then
                MessageBox.Show("Data konsumen tidak dpt digunakan, silakan ajukan ulang.")
                bcheck = False
            End If
        Else
            MessageBox.Show("Customer tidak ditemukan")
            bcheck = False
        End If
        Return bcheck
    End Function

    Private Sub BindCustomer(ByVal objCust As Customer)
        If Len(objCust.Code) > 1 Then
            lbltxtCustomerCode.Text = objCust.Code
            lblName.Text = objCust.Name1
            lblName2.Text = objCust.Name2
            lblGedung.Text = objCust.Name3
            lblAlamat.Text = objCust.Alamat
            lblKelurahan.Text = objCust.Kelurahan
            lblKecamatan.Text = objCust.Kecamatan
            lblKodePos.Text = objCust.PostalCode
            If Not IsNothing(objCust.City) Then
                lblPropinsi.Text = objCust.City.Province.ProvinceName
            End If

            If Not IsNothing(objCust.MyCustomerRequest) Then
                If Not IsNothing(objCust.MyCustomerRequest.GetCustomerRequestProfile("NOKTP")) Then
                    lblNoKTP.Text = objCust.MyCustomerRequest.GetCustomerRequestProfile("NOKTP").ProfileValue
                End If
            Else
                lblNoKTP.Text = String.Empty
            End If
            If objCust.PreArea <> String.Empty Then
                lblKodya.Text = objCust.PreArea & " " & objCust.City.CityName
            Else
                lblKodya.Text = objCust.City.CityName
            End If

            lblEmail.Text = objCust.Email
            lblPhone.Text = objCust.PhoneNo
            btnSave.Enabled = True

        Else

            lbltxtCustomerCode.Text = String.Empty
            lblName.Text = String.Empty
            lblName2.Text = String.Empty
            lblGedung.Text = String.Empty
            lblAlamat.Text = String.Empty
            lblKelurahan.Text = String.Empty
            lblKecamatan.Text = String.Empty
            lblKodePos.Text = String.Empty
            lblPropinsi.Text = String.Empty
            lblNoKTP.Text = String.Empty
            lblKodya.Text = String.Empty
            lblEmail.Text = String.Empty
            lblPhone.Text = String.Empty
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub BindDataGrid()
        arl = CType(Session("sessCM"), ArrayList)
        dtgPengajuanFaktur.DataSource = arl
        dtgPengajuanFaktur.DataBind()
        If arl.Count >= 1 Then
            dtgPengajuanFaktur.ShowFooter = False
        End If
    End Sub

    Public Sub RebindVehicleKind(ByRef e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim ddlVehicleKindF As DropDownList = e.Item.FindControl("ddlVehicleKindF")
        Dim ddlVehicleModelF As DropDownList = e.Item.FindControl("ddlVehicleModelF")
        Dim lblNomorRangkaFooter As Label = e.Item.FindControl("lblNomorRangkaFooter")

        RebindVehicleKind(ddlVehicleKindF, ddlVehicleModelF, lblNomorRangkaFooter)
    End Sub

    Public Sub RebindVehicleKind(ddlVehicleKindF As DropDownList, ddlVehicleModelF As DropDownList, lblNomorRangkaFooter As Label)
        Dim companyCode As String = KTB.DNET.Lib.WebConfig.GetValue("CompanyCode")
        Dim oCM As ChassisMaster

        If IsNothing(_objChassisMaster) Then
            Dim oCMFac As New ChassisMasterFacade(User)
            oCM = oCMFac.Retrieve(lblNomorRangkaFooter.Text)
        Else
            oCM = _objChassisMaster
        End If

        If IsNothing(oCM) OrElse oCM.ID < 1 Then
            MessageBox.Show("Nomor Rangka Tidak Terdaftar")
            Exit Sub
        End If

        hdnVC.Value = oCM.VechileColor.ID
        lblsearchLKPP.Visible = True

        If oCM.Category.ProductCategory.Code.Trim <> companyCode Then
            MessageBox.Show("Nomor Rangka tidak terdaftar di PT MMKSI")
            Exit Sub
        End If

        Dim oVKFac As New VehicleKindGroupFacade(User)
        Dim cVK As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKindGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim nKind As Integer = 0, nKindUsed As Integer = 0
        Dim aVK As ArrayList

        cVK.opAnd(New Criteria(GetType(VehicleKindGroup), "Code", MatchType.Exact, "LAIN"), "(", True)
        If oCM.VechileColor.VechileType.IsVehicleKind1 = 1 Then nKind += 1
        If oCM.VechileColor.VechileType.IsVehicleKind2 = 1 Then nKind += 1
        If oCM.VechileColor.VechileType.IsVehicleKind3 = 1 Then nKind += 1
        If oCM.VechileColor.VechileType.IsVehicleKind4 = 1 Then nKind += 1
        If (IsNothing(oCM) OrElse oCM.ID < 1) OrElse nKind < 1 Then
            cVK.opOr(New Criteria(GetType(VehicleKindGroup), "Code", MatchType.Exact, "LAIN"), ")", False)
        Else
            If oCM.VechileColor.VechileType.IsVehicleKind1 = 1 Then
                nKindUsed += 1
                If nKindUsed = nKind Then
                    cVK.opOr(New Criteria(GetType(VehicleKindGroup), "Code", MatchType.Exact, "MPG"), ")", False)
                Else
                    cVK.opOr(New Criteria(GetType(VehicleKindGroup), "Code", MatchType.Exact, "MPG"))
                End If
            End If
            If oCM.VechileColor.VechileType.IsVehicleKind2 = 1 Then
                nKindUsed += 1
                If nKindUsed = nKind Then
                    cVK.opOr(New Criteria(GetType(VehicleKindGroup), "Code", MatchType.Exact, "MBS"), ")", False)
                Else
                    cVK.opOr(New Criteria(GetType(VehicleKindGroup), "Code", MatchType.Exact, "MBS"))
                End If
            End If
            If oCM.VechileColor.VechileType.IsVehicleKind3 = 1 Then
                nKindUsed += 1
                If nKindUsed = nKind Then
                    cVK.opOr(New Criteria(GetType(VehicleKindGroup), "Code", MatchType.Exact, "MBG"), ")", False)
                Else
                    cVK.opOr(New Criteria(GetType(VehicleKindGroup), "Code", MatchType.Exact, "MBG"))
                End If
            End If
            If oCM.VechileColor.VechileType.IsVehicleKind4 = 1 Then
                nKindUsed += 1
                If nKindUsed = nKind Then
                    cVK.opOr(New Criteria(GetType(VehicleKindGroup), "Code", MatchType.Exact, "KKH"), ")", False)
                Else
                    cVK.opOr(New Criteria(GetType(VehicleKindGroup), "Code", MatchType.Exact, "KKH"))
                End If
            End If
        End If
        aVK = oVKFac.Retrieve(cVK)
        ddlVehicleKindF.Items.Clear()
        For Each oVK As VehicleKindGroup In aVK
            ddlVehicleKindF.Items.Add(New ListItem(oVK.Description, oVK.ID))
        Next
        BindVehicleModel(ddlVehicleKindF, ddlVehicleModelF)
        If Me.txtCustomerCode.Text.Trim <> String.Empty Then
            If panelSPKOther.Visible And txtSPKOther.Text <> String.Empty Then
                GetCustomerInfoForSPKMatch(txtCustomerCode.Text)
            Else
                GetCustomerInfo(Me.txtCustomerCode.Text.Trim)
            End If
        End If

    End Sub

    Private Sub InitDataFromSession()
        If Not ssHelper.GetSession("NoSPK") Is Nothing Then
            If panelSPKOther.Visible Then
                If txtSPKOther.Text = "" Then
                    txtSPKOther.Text = CType(ssHelper.GetSession("NoSPK"), String)
                    ssHelper.SetSession("NoSPK", txtSPKOther.Text)
                End If

            Else
                If txtSPKNumber.Text = "" Then
                    txtSPKNumber.Text = CType(ssHelper.GetSession("NoSPK"), String)
                    ssHelper.SetSession("NoSPK", txtSPKNumber.Text)
                End If

            End If
        End If
        If Not (CType(ssHelper.GetSession("Salesman"), SalesmanHeader)) Is Nothing Then
            If txtSalesmanCode.Text <> CType(ssHelper.GetSession("Salesman"), SalesmanHeader).SalesmanCode Then
                GetSalesInfo(txtSalesmanCode.Text)
            End If
        Else
            If txtSalesmanCode.Text.Trim <> String.Empty Then
                GetSalesInfo(txtSalesmanCode.Text.Trim)
            End If
        End If

        If Not (CType(Session("Customer"), Customer)) Is Nothing Then
            BindCustomer(CType(Session("Customer"), Customer))
        Else
            If Me.txtCustomerCode.Text.Trim <> String.Empty Then
                GetCustomerInfo(txtCustomerCode.Text)
            End If

            If Me.txtCustomerCode.Text.Trim() = "" AndAlso hdnValid.Value.ToString() = "-1" Then
                Dim objX As New Customer

                BindCustomer(objX)
            End If
        End If
    End Sub

    Public Sub BindVehicleModel(e As DataGridCommandEventArgs)
        Dim ddlVehicleKindF As DropDownList = e.Item.FindControl("ddlVehicleKindF")
        Dim ddlVehicleModelF As DropDownList = e.Item.FindControl("ddlVehicleModelF")

        BindVehicleModel(ddlVehicleKindF, ddlVehicleModelF)
    End Sub

    Public Sub BindVehicleModel(ddlVehicleKindF As DropDownList, ddlVehicleModelF As DropDownList)
        Dim aVK As ArrayList
        Dim oVKFac As New VehicleKindFacade(User)
        Dim cVK As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not IsNothing(ddlVehicleKindF.SelectedValue) And ddlVehicleKindF.SelectedValue <> "" Then
            cVK.opAnd(New Criteria(GetType(VehicleKind), "VehicleKindGroupID", MatchType.Exact, ddlVehicleKindF.SelectedValue.ToString))
        Else
            cVK.opAnd(New Criteria(GetType(VehicleKind), "VehicleKindGroupID", MatchType.Exact, 0))
            MessageBox.Show("Jenis Kendaraan belum di maintain.Harap hubungi MMKSI")
        End If

        aVK = oVKFac.Retrieve(cVK)
        ddlVehicleModelF.Items.Clear()
        For Each oVK As VehicleKind In aVK
            ddlVehicleModelF.Items.Add(New ListItem(oVK.Description, oVK.ID))
        Next
    End Sub

    Private Sub addCommand(ByVal e As DataGridCommandEventArgs)
        lblError.Text = String.Empty
        Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
        Dim objCM As New ChassisMaster
        Dim lblNomorRangkaFooter As Label = CType(e.Item.FindControl("lblNomorRangkaFooter"), Label)
        Dim txtFooterNoRangkaPengganti As TextBox = CType(e.Item.FindControl("txtFooterNoRangkaPengganti"), TextBox)
        Dim icMaxDate As IntiCalendar = CType(e.Item.FindControl("icMaxDate"), IntiCalendar)
        Dim mode As String = "add"
        Dim ddlVehicleKindF As DropDownList = CType(e.Item.FindControl("ddlVehicleKindF"), DropDownList)
        Dim ddlVehicleModelF As DropDownList = CType(e.Item.FindControl("ddlVehicleModelF"), DropDownList)

        If Not IsNothing(ddlVehicleKindF) Then
            If ddlVehicleKindF.Items.Count < 1 Then
                MessageBox.Show("Jenis Kendaraan Tidak Valid")
                RebindVehicleKind(e)
                Exit Sub
            Else
                Dim companyCode As String = KTB.DNET.Lib.WebConfig.GetValue("CompanyCode")
                Dim oCM As ChassisMaster
                Dim oCMFac As New ChassisMasterFacade(User)

                oCM = oCMFac.Retrieve(lblNomorRangkaFooter.Text)

                If oCM.Category.ProductCategory.Code.Trim <> companyCode Then
                    MessageBox.Show("Nomor Rangka tidak terdaftar")
                    RebindVehicleKind(e)
                    Exit Sub
                End If

                If txtFooterNoRangkaPengganti.Text.Trim() <> "" Then
                    Dim oCMPengganti As ChassisMaster
                    Dim oCMPenggantiFac As New ChassisMasterFacade(User)
                    oCMPengganti = oCMPenggantiFac.Retrieve(txtFooterNoRangkaPengganti.Text)
                    If (Not IsNothing(oCMPengganti) AndAlso Not IsNothing(oCMPengganti.Category)) Then
                        If oCMPengganti.Category.ProductCategory.Code.Trim <> companyCode Then
                            MessageBox.Show("Nomor Rangka Pengganti tidak terdaftar")
                            RebindVehicleKind(e)
                            Exit Sub
                        End If
                    End If
                End If

                Dim oVKFac As New VehicleKindGroupFacade(User)
                Dim cVK As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKindGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim nKind As Integer = 0, nKindUsed As Integer = 0
                Dim aVK As ArrayList

                cVK.opAnd(New Criteria(GetType(VehicleKindGroup), "Code", MatchType.Exact, "LAIN"), "(", True)
                If oCM.VechileColor.VechileType.IsVehicleKind1 = 1 Then nKind += 1
                If oCM.VechileColor.VechileType.IsVehicleKind2 = 1 Then nKind += 1
                If oCM.VechileColor.VechileType.IsVehicleKind3 = 1 Then nKind += 1
                If oCM.VechileColor.VechileType.IsVehicleKind4 = 1 Then nKind += 1
                If (IsNothing(oCM) OrElse oCM.ID < 1) OrElse nKind < 1 Then
                    cVK.opOr(New Criteria(GetType(VehicleKindGroup), "Code", MatchType.Exact, "LAIN"), ")", False)
                Else
                    If oCM.VechileColor.VechileType.IsVehicleKind1 = 1 Then
                        nKindUsed += 1
                        If nKindUsed = nKind Then
                            cVK.opOr(New Criteria(GetType(VehicleKindGroup), "Code", MatchType.Exact, "MPG"), ")", False)
                        Else
                            cVK.opOr(New Criteria(GetType(VehicleKindGroup), "Code", MatchType.Exact, "MPG"))
                        End If
                    End If
                    If oCM.VechileColor.VechileType.IsVehicleKind2 = 1 Then
                        nKindUsed += 1
                        If nKindUsed = nKind Then
                            cVK.opOr(New Criteria(GetType(VehicleKindGroup), "Code", MatchType.Exact, "MBS"), ")", False)
                        Else
                            cVK.opOr(New Criteria(GetType(VehicleKindGroup), "Code", MatchType.Exact, "MBS"))
                        End If
                    End If
                    If oCM.VechileColor.VechileType.IsVehicleKind3 = 1 Then
                        nKindUsed += 1
                        If nKindUsed = nKind Then
                            cVK.opOr(New Criteria(GetType(VehicleKindGroup), "Code", MatchType.Exact, "MBG"), ")", False)
                        Else
                            cVK.opOr(New Criteria(GetType(VehicleKindGroup), "Code", MatchType.Exact, "MBG"))
                        End If
                    End If
                    If oCM.VechileColor.VechileType.IsVehicleKind4 = 1 Then
                        nKindUsed += 1
                        If nKindUsed = nKind Then
                            cVK.opOr(New Criteria(GetType(VehicleKindGroup), "Code", MatchType.Exact, "KKH"), ")", False)
                        Else
                            cVK.opOr(New Criteria(GetType(VehicleKindGroup), "Code", MatchType.Exact, "KKH"))
                        End If
                    End If
                End If
                aVK = oVKFac.Retrieve(cVK)
                Dim isVKValid As Boolean = False
                For Each oVK As VehicleKindGroup In aVK
                    If oVK.ID = CType(ddlVehicleKindF.SelectedValue, Integer) Then
                        isVKValid = True
                    End If
                Next
                If Not isVKValid Then
                    MessageBox.Show("Jenis Kendaraan Tidak Valid")
                    RebindVehicleKind(e)
                    Exit Sub
                End If
                'model
                Dim aVK2 As ArrayList
                Dim oVKFac2 As New VehicleKindFacade(User)
                Dim cVK2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                If Not IsNothing(ddlVehicleKindF.SelectedValue) And ddlVehicleKindF.SelectedValue <> "" Then
                    cVK2.opAnd(New Criteria(GetType(VehicleKind), "VehicleKindGroupID", MatchType.Exact, ddlVehicleKindF.SelectedValue.ToString))
                Else
                    cVK2.opAnd(New Criteria(GetType(VehicleKind), "VehicleKindGroupID", MatchType.Exact, 0))
                    MessageBox.Show("Jenis Kendaraan belum di maintain.Harap hubungi MMKSI")
                End If

                aVK2 = oVKFac2.Retrieve(cVK2)
                Dim isVKValid2 As Boolean = False
                For Each oVK As VehicleKind In aVK2
                    If oVK.ID = CType(ddlVehicleModelF.SelectedValue, Integer) Then
                        isVKValid2 = True
                    End If
                Next
                If Not isVKValid2 Then
                    MessageBox.Show("Model Kendaraan Tidak Valid")
                    BindVehicleModel(e)
                    Exit Sub
                End If

                If Not IsNothing(ddlVehicleModelF) Then
                    If ddlVehicleModelF.Items.Count < 1 Then
                        MessageBox.Show("Model Kendaraan Tidak Valid")
                        BindVehicleModel(e)
                        Exit Sub
                    Else

                    End If
                End If
            End If
        End If

        objCM = CType(e.Item.DataItem, ChassisMaster)
        arl = CType(Session("sessCM"), ArrayList)
        _arrDetail = CType(Session("DeliveryCustomerDetail"), ArrayList)

        If Not IsChassisMasterAllowedToTransact(lblNomorRangkaFooter.Text.Trim()) Then Exit Sub
        If txtFooterNoRangkaPengganti.Text.Trim() <> "" Then
            If Not IsChassisMasterAllowedToTransact(txtFooterNoRangkaPengganti.Text.Trim()) Then Exit Sub
        End If

        If (CommonFunction.IsExistChassisNumberInLoginDealer(lblNomorRangkaFooter.Text.Trim(), objDealer.ID, User) = False) And (IsExistChassisNumberInStockDealer(lblNomorRangkaFooter.Text.Trim(), objDealer.ID, User) = False) Then
            MessageBox.Show("Nomor rangka tidak terdaftar di dealer anda")
            Exit Sub
        End If
        If txtFooterNoRangkaPengganti.Text.Trim <> String.Empty Then
            If (CommonFunction.IsExistChassisNumberInLoginDealer(txtFooterNoRangkaPengganti.Text.Trim(), objDealer.ID, User) = False) And (IsExistChassisNumberInStockDealer(txtFooterNoRangkaPengganti.Text.Trim(), objDealer.ID, User) = False) Then
                MessageBox.Show("Nomor rangka pengganti tidak terdaftar di dealer anda")
                Exit Sub
            End If
        End If
        If txtSalesmanCode.Text.Trim <> String.Empty Then
            If GetSalesInfo(txtSalesmanCode.Text.Trim) = False Then
                Exit Sub
            End If
        End If
        If txtCustomerCode.Text.Trim <> String.Empty Then
            If txtSPKOther.Text <> String.Empty And panelSPKOther.Visible Then
                ' if spk match enable to cross cutomer dealer
                If GetCustomerInfoForSPKMatch(txtCustomerCode.Text) = False Then
                    Exit Sub
                End If
            Else
                If GetCustomerInfo(txtCustomerCode.Text) = False Then
                    Exit Sub
                End If
            End If
        Else
            MessageBox.Show("Masukkan kode konsumen terlebih dahulu")
            Exit Sub
        End If

        ' To allow user to make Invoice in 29 and 30 sept 2008
        ' --------------------------------------------------------------------------
        If Not IsValidTglFaktur(icMaxDate.Value) Then
            If Format(icMaxDate.Value, "dd/mm/yyyy") = "29/09/2008" Or Format(icMaxDate.Value, "dd/mm/yyyy") = "30/09/2008" Then
                lblError.Text = "Tanggal pengajuan tidak valid/hari libur."
                Exit Sub
            End If
        Else
            lblError.Text = ""
        End If
        ' --------------------------------------------------------------------------

        'Related to SPK on 20110330
        '--------------------------------------------

        If txtSPKNumber.Text.Trim <> String.Empty Or txtSPKOther.Text.Trim <> String.Empty Then
            Dim errSPKMessage As String = IsSPKAllowed(lblNomorRangkaFooter.Text.Trim)
            If errSPKMessage <> String.Empty Then
                lblError.Text = errSPKMessage
                Exit Sub
            End If
        End If
        '--------------------------------------------

        If CheckData(lblNomorRangkaFooter.Text.Trim, txtFooterNoRangkaPengganti.Text.Trim, icMaxDate.Value, e.Item.ItemIndex) Then
            AddToArrayList(lblNomorRangkaFooter.Text, txtFooterNoRangkaPengganti.Text, icMaxDate.Value, mode, 0, CType(ddlVehicleModelF.SelectedValue, Integer))
            If IsValidChassisMasterII(lblNomorRangkaFooter.Text.Trim) <> String.Empty Then
                ssHelper.SetSession("PREVPAGE", "..\FinishUnit\FrmEntryInvoiceRevisionFaktur.aspx")
                ssHelper.SetSession("NoRangka", lblNomorRangkaFooter.Text)
                ssHelper.SetSession("NoRangkaPengganti", txtFooterNoRangkaPengganti.Text)
                ssHelper.SetSession("DateFaktur", icMaxDate.Value)
                Response.Redirect("..\PopUp\PopUpConfirmationPengajuanFaktur.aspx")
            End If

        End If
        BindDataGrid()
        txtSPKNumber.Enabled = False
        lblSPKNumber.Visible = False
        txtSPKOther.Enabled = False
    End Sub

    Private Function getIndexDelivery(ByVal list As ArrayList, ByVal NoRangka As String) As Integer
        Dim i As Integer = 0
        Try
            For Each item As DeliveryCustomerDetail In list
                If item.ChassisMaster.ChassisNumber = NoRangka Then
                    Return i
                    Exit Function
                End If
                i = i + 1
            Next
            Return -1
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Private Function RemoveCharacterBlock(ByVal pendingDesc As String, ByVal block As String) As String
        Dim strValue As String
        If Not pendingDesc = "" Then
            strValue = pendingDesc.Replace(block, "")
        End If
        Return strValue
    End Function

    Private Function AddToArrayList(ByVal NoRangka As String, ByVal NoRangkaPengganti As String, ByVal FakturDate As DateTime, ByVal mode As String, ByVal index As Integer, Optional ByVal VehicleKindID As Integer = 1) As ArrayList
        Dim CM As New ChassisMaster
        Dim CM2 As New ChassisMaster
        CM = New FinishUnit.ChassisMasterFacade(User).Retrieve(NoRangka)
        CM2 = New FinishUnit.ChassisMasterFacade(User).Retrieve(NoRangkaPengganti)
        Dim objEndCustomer As EndCustomer
        If getIndex(arl, NoRangka) <> -1 Then
            objEndCustomer = New EndCustomerFacade(User).Retrieve(CType(arl(getIndex(arl, NoRangka)), ChassisMaster).EndCustomerID)
        Else
            If _formMode = EnumDNET.enumFormMode.Add Then
                objEndCustomer = New EndCustomerFacade(User).Retrieve(CM.EndCustomer.ID)
            Else
                objEndCustomer = New EndCustomer
            End If
        End If
        objEndCustomer.RefChassisNumberID = CM2.ID
        objEndCustomer.FakturDate = FakturDate
        ssHelper.SetSession("DEFAULTDATE", FakturDate)
        CM.EndCustomer = objEndCustomer
        objEndCustomer.MarkLoaded()
        If mode = "add" Then
            CM.VehicleKind = New VehicleKind(VehicleKindID)
            arl.Add(CM)
            If CM.StockStatus <> "X" Then
                Dim oDeliveryCustomerDetil As New DeliveryCustomerDetail
                oDeliveryCustomerDetil.ChassisMaster = CM
                _arrDetail.Add(oDeliveryCustomerDetil)
            End If
        ElseIf mode = "update" Then
            Dim objCM As ChassisMaster = CType(arl(index), ChassisMaster)
            Dim indek As Integer = getIndexDelivery(_arrDetail, objCM.ChassisNumber)
            If indek <> -1 Then
                _arrDetail.RemoveAt(indek)
            End If
            arl.RemoveAt(index)
            arl.Insert(index, CM)
            If CM.StockStatus <> "X" Then
                Dim oDeliveryCustomerDetil As New DeliveryCustomerDetail
                oDeliveryCustomerDetil.ChassisMaster = CM
                _arrDetail.Insert(indek, oDeliveryCustomerDetil)
            End If
        End If
    End Function

    Private Function getIndex(ByVal list As ArrayList, ByVal NoRangka As String) As Integer
        Dim i As Integer = 0
        For Each item As ChassisMaster In list
            If item.ChassisNumber = NoRangka Then
                Return i
                Exit Function
            End If
            i = i + 1
        Next
        Return -1
    End Function

    Private Function GetCMProfile(ByRef oCM As ChassisMaster, ByRef oPG As ProfileGroup, ByRef oPH As ProfileHeader) As RevisionChassisMasterProfile
        Dim oCMPFac As New RevisionChassisMasterProfileFacade(User)
        Dim cCMP As New CriteriaComposite(New Criteria(GetType(RevisionChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim aCMP As ArrayList

        cCMP.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "ChassisMaster.ID", MatchType.Exact, oCM.ID))
        cCMP.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "EndCustomer.ID", MatchType.Exact, oCM.EndCustomer.ID))
        cCMP.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "ProfileGroup.ID", MatchType.Exact, oPG.ID))
        cCMP.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "ProfileHeader.ID", MatchType.Exact, oPH.ID))
        aCMP = oCMPFac.Retrieve(cCMP)
        If aCMP.Count > 0 Then
            Return CType(aCMP(0), RevisionChassisMasterProfile)
        Else
            Return New RevisionChassisMasterProfile
        End If
    End Function

    Private Function CopyCustomerDealerToDealerSPK(ByVal oCust As Customer) As Boolean
        Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
        If Not IsNothing(oCust) AndAlso oCust.ID > 1 Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CustomerDealer), "Customer.ID", MatchType.Exact, oCust.ID))
            Dim objCustomerDealerFacade As CustomerDealerFacade = New CustomerDealerFacade(User)
            Dim objList As ArrayList = objCustomerDealerFacade.Retrieve(criterias)
            If objList.Count > 0 Then
                Dim isExist As Boolean = False
                For Each item As CustomerDealer In objList
                    If item.Dealer.ID = objDealer.ID Then
                        isExist = True
                    End If
                Next
                If isExist = False Then
                    Dim newCustDealer As CustomerDealer = CType(objList(0), CustomerDealer)
                    newCustDealer.Dealer = objDealer
                    objCustomerDealerFacade.Insert(newCustDealer)
                End If
            End If
        End If
    End Function

    Private Function GetCheckedItem() As ArrayList
        dtgPengajuanFaktur.DataSource = CType(Session("sessCM"), ArrayList)
        Dim arlCheckedItem As ArrayList = New ArrayList
        Dim nIndeks As Integer
        For Each dtgItem As DataGridItem In dtgPengajuanFaktur.Items
            nIndeks = dtgItem.ItemIndex
            Dim objCM As ChassisMaster = CType(CType(dtgPengajuanFaktur.DataSource, ArrayList)(nIndeks), ChassisMaster)
            If CType(dtgItem.Cells(2).FindControl("chkSelect"), CheckBox).Checked Then
                arlCheckedItem.Add(objCM)

            End If
        Next
        Return arlCheckedItem
    End Function

    Private Sub DisableFrmControl()

        txtSPKNumber.Enabled = False
        lblSPKNumber.Visible = False
        txtSalesmanCode.Enabled = False
        lblShowSalesman.Visible = False
        txtLKPPNumber.Visible = False
        lblsearchLKPP.Visible = False

        btnSave.Enabled = False
    End Sub

    Private Sub ClearCustomer()
        txtSalesmanCode.Text = String.Empty
        lblNamaSales.Text = String.Empty
        lblLevel.Text = String.Empty
        lblPosisi.Text = String.Empty
        lblDealerBranch.Text = String.Empty
        txtCustomerCode.Text = String.Empty
        lbltxtCustomerCode.Text = String.Empty

        lbltxtCustomerCode.Text = String.Empty
        lblName.Text = String.Empty
        lblName2.Text = String.Empty
        lblGedung.Text = String.Empty
        lblAlamat.Text = String.Empty
        lblKelurahan.Text = String.Empty
        lblKecamatan.Text = String.Empty
        lblKodePos.Text = String.Empty
        lblPropinsi.Text = String.Empty
        lblNoKTP.Text = String.Empty
        lblKodya.Text = String.Empty
        lblEmail.Text = String.Empty
        lblPhone.Text = String.Empty
    End Sub
#End Region

#Region "Validation"
    Private Function IsCustomerAvailaibleForLoginDealer(ByVal objCust As Customer, ByVal loginDealer As Dealer) As Boolean
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(CustomerDealer), "Customer.ID", MatchType.Exact, objCust.ID))
        Dim objCustomerDealerFacade As CustomerDealerFacade = New CustomerDealerFacade(User)
        Dim objList As ArrayList = objCustomerDealerFacade.Retrieve(criterias)
        If Not IsNothing(objList) AndAlso objList.Count > 0 Then
            For Each item As CustomerDealer In objList
                If item.Dealer.DealerGroup.ID = loginDealer.DealerGroup.ID Then
                    If item.Customer.RowStatus <> DBRowStatus.Deleted Then
                        Return True
                    End If
                End If
            Next
        Else
            Return False
        End If
        Return False
    End Function

    Private Function isValidCM() As Boolean
        Dim aCMs As ArrayList = CType(Session("sessCM"), ArrayList)
        Dim sError As String = String.Empty

        For Each oCM As ChassisMaster In aCMs
            If oCM.isValidToCreateFaktur() = False Then
                sError &= IIf(sError = String.Empty, "", ", ") & oCM.ChassisNumber
            End If
        Next
        If sError <> String.Empty Then
            MessageBox.Show("Nomor Rangka " & sError & " Sudah Diretur")
        End If
        Return (sError = String.Empty)
    End Function

    Private Function IsChassisMasterAllowedToTransact(ByVal ChassisNumber As String) As Boolean
        'Start  : not implemented yet
        Return True
        'End    : not implemented yet
        Dim objCM As ChassisMaster = New ChassisMasterFacade(User).Retrieve(ChassisNumber)
        If objCM.ID > 0 Then
            If objCM.DONumber = "1256000064" Then
                MessageBox.Show("Chassis " & ChassisNumber & " tidak bisa dibuat faktur karena sudah diretur")
                Return False
            End If
        End If
        Return True
    End Function

    Private Function IsExistChassisNumberInStockDealer(ByVal ChassisNumber As String, ByVal DealerID As String, ByVal user As System.Security.Principal.IPrincipal)
        Dim arlCM As New ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, ChassisNumber))
        criterias.opAnd(New Criteria(GetType(ChassisMaster), "StockDealer", MatchType.Exact, DealerID))
        arlCM = New FinishUnit.ChassisMasterFacade(user).Retrieve(criterias)
        If (arlCM.Count > 0) Then
            Return True
        End If
        Return False
    End Function

    Private Function IsValidTglFaktur(ByVal FakturDate As Date) As Boolean
        If Date.Now.ToString("ddMMyyyy") = "22042011" Then
            Return True
        Else
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.NationalHoliday), "HolidayDate", MatchType.Exact, FakturDate.Day))
            criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.NationalHoliday), "HolidayMonth", MatchType.Exact, FakturDate.Month))
            criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.NationalHoliday), "HolidayYear", MatchType.Exact, FakturDate.Year))
            Dim arlNationalHoliday As ArrayList = New NationalHolidayFacade(User).RetrieveByCriteria(criterias)
            If arlNationalHoliday.Count > 0 Then
                Dim objTimeSpan As TimeSpan = FakturDate.Subtract(DateTime.Now)
                If objTimeSpan.Days >= 1 Then
                    For i As Integer = 1 To objTimeSpan.Days
                        Dim criterias1 As New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias1.opAnd(New Criteria(GetType(KTB.DNET.Domain.NationalHoliday), "HolidayDate", MatchType.Exact, FakturDate.AddDays(i * -1).Day))
                        criterias1.opAnd(New Criteria(GetType(KTB.DNET.Domain.NationalHoliday), "HolidayMonth", MatchType.Exact, FakturDate.AddDays(i * -1).Month))
                        criterias1.opAnd(New Criteria(GetType(KTB.DNET.Domain.NationalHoliday), "HolidayYear", MatchType.Exact, FakturDate.AddDays(i * -1).Year))
                        Dim arlNationalHoliday1 As ArrayList = New NationalHolidayFacade(User).RetrieveByCriteria(criterias1)
                        If arlNationalHoliday1.Count = 0 Then
                            Return True
                        End If
                    Next
                    Return False
                Else
                    Return False
                End If
            Else
                Return True
            End If
        End If

    End Function

    Private Function IsSPKAllowed(ByVal chassisNumber As String)
        Dim isAllowed As Boolean = False
        Dim msgReturn As String = String.Empty
        Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
        Dim spkNo As String = String.Empty

        If txtSPKOther.Text.Trim <> String.Empty And panelSPKOther.Visible Then
            spkNo = txtSPKOther.Text.Trim
        Else
            spkNo = txtSPKNumber.Text.Trim
        End If

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SPKHeader), "SPKNumber", MatchType.Exact, spkNo))
        Dim objSPKHeaderList As ArrayList = New FinishUnit.SPKHeaderFacade(User).Retrieve(criterias)
        If objSPKHeaderList.Count > 0 Then
            Dim objChassisMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(chassisNumber)
            Dim objSPKHeader As SPKHeader
            For Each oHeader As SPKHeader In objSPKHeaderList
                If oHeader.Dealer.DealerGroup.ID = objDealer.DealerGroup.ID Then
                    objSPKHeader = oHeader
                End If
            Next
            If IsNothing(objSPKHeader) Then
                objSPKHeader = objSPKHeaderList(0)
            End If

            Dim IsColorExist As Boolean = False

            For Each objSPKDetail As SPKDetail In objSPKHeader.SPKDetails
                If Not IsNothing(objSPKDetail.VechileColor) Then
                    If objChassisMaster.VechileColor.ColorCode.ToUpper() = objSPKDetail.VechileColor.ColorCode.ToUpper() Then
                        IsColorExist = True
                    End If
                End If
            Next

            Dim dataG = From spkd As SPKDetail In objSPKHeader.SPKDetails.Cast(Of SPKDetail).ToList()
                    Where Not IsNothing(spkd.VechileColor) AndAlso Not IsNothing(spkd.VechileColor.VechileType)
                    Group By spkd.VechileColor Into Group
                    Select VechileColor

            '    For Each objSPKDetail As SPKDetail In objSPKHeader.SPKDetails

            Dim spkL As New List(Of SPKDetail)()

            For Each dtColor As VechileColor In dataG
                Dim dr As New SPKDetail()
                dr.VechileColor = dtColor

                Dim dataQ = (From spkd As SPKDetail In objSPKHeader.SPKDetails.Cast(Of SPKDetail).ToList()
                        Where Not IsNothing(spkd.VechileColor) AndAlso Not IsNothing(spkd.VechileColor.VechileType) AndAlso spkd.VechileColor.ID = dtColor.ID
                        Select spkd.Quantity).Sum()
                dr.Quantity = CInt(dataQ)
                spkL.Add(dr)

            Next

            'For Each objSPKDetail As SPKDetail In objSPKHeader.SPKDetails
            For Each objSPKDetail As SPKDetail In spkL
                If Not IsNothing(objSPKDetail.VechileColor.VechileType) Then
                    If objChassisMaster.VechileColor.VechileType.ID = objSPKDetail.VechileColor.VechileType.ID Then
                        Dim i As Integer = 0
                        If Not IsNothing(objSPKHeader.SPKFakturs) Then
                            For Each objSPKFaktur As SPKFaktur In objSPKHeader.SPKFakturs
                                If objSPKFaktur.RowStatus = CType(DBRowStatus.Active, Short) Then
                                    If Not objSPKFaktur.EndCustomer.ChassisMaster Is Nothing Then
                                        If objSPKFaktur.EndCustomer.ChassisMaster.VechileColor.VechileType.ID = objChassisMaster.VechileColor.VechileType.ID _
                                        And objSPKFaktur.EndCustomer.ChassisMaster.VechileColor.ColorCode = objChassisMaster.VechileColor.ColorCode Then
                                            i += 1
                                        End If
                                    End If
                                End If
                            Next
                        End If

                        If i < objSPKDetail.Quantity Then
                            isAllowed = True
                            ssHelper.SetSession("SPKDETAILFAKTUR", objSPKDetail)
                            msgReturn = String.Empty
                            Exit For
                        Else
                            isAllowed = False
                            msgReturn = "Kuota SPK untuk tipe tersebut (" & objSPKDetail.Quantity & ") sudah habis."
                        End If
                    End If
                Else
                    isAllowed = True
                End If
            Next

            If isAllowed = False AndAlso msgReturn = String.Empty Then
                msgReturn = "Tipe kendaraan tidak terdaftar pada SPK"
            End If

            If IsColorExist = False Then
                isAllowed = False
                msgReturn = "Warna kendaraan tidak terdaftar pada SPK"
            End If

            'ini
            If CheckChassisNumberAndPendingDesc(chassisNumber) Then
                Dim sessPendingDesc = ssHelper.GetSession("sesPENDING_DESC")
                msgReturn = sessPendingDesc
                isAllowed = False
            End If

            ' if all validation above passed than validate SPK Matching
            If isAllowed And msgReturn = String.Empty Then
                If ValidateSPKMatching(msgReturn, objChassisMaster, objSPKHeader) Then
                    isAllowed = True
                End If
            End If
        Else
            msgReturn = "Nomor Registrasi SPK tidak terdaftar"
        End If
        ssHelper.SetSession("NoSPK", spkNo)
        Return msgReturn
    End Function

    Private Function CheckChassisNumberAndPendingDesc(ByVal chassisNumber As String) As Boolean
        Dim isBlock As Boolean = False
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, chassisNumber))
        criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.ChassisMaster), "RowStatus", MatchType.Exact, 0))
        Dim dataResult = New ChassisMasterFacade(User).Retrieve(criterias)

        If dataResult.count >= 1 Then
            For Each model As ChassisMaster In dataResult
                If model.PendingDesc <> String.Empty Then
                    If model.PendingDesc.Contains(blockFaktur) Then
                        isBlock = True
                        ssHelper.SetSession("sesPENDING_DESC", RemoveCharacterBlock(model.PendingDesc, blockFaktur))
                    Else
                        If model.PendingDesc.Contains(unBlockFaktur) And model.LastUpdateProfile >= DateTime.Now Then
                            isBlock = False
                        Else
                            isBlock = True
                            ssHelper.SetSession("sesPENDING_DESC", RemoveCharacterBlock(model.PendingDesc, unBlockFaktur))
                        End If
                    End If
                End If
            Next
        End If
        Return isBlock
    End Function

    Private Function CheckData(ByVal NoRangka As String, ByVal NoRangkaPengganti As String, ByVal icMax As Date, ByVal index As Integer) As Boolean
        'VALIDASI TANGGAL RANGKA
        If Not (IsDate(icMax)) Then
            MessageBox.Show("Tanggal faktur tidak valid.")
            Return False
            Exit Function
        End If

        If (icMax < Today.AddDays(1)) Then
            MessageBox.Show("Tanggal faktur harus lebih besar atau sama dengan besok")
            Return False
            Exit Function
        End If
        'VALIDASI NOMOR RANGKA
        If (NoRangka <> String.Empty) Then
            If (Not IsValidChassisNumber(NoRangka)) Then
                MessageBox.Show("Nomor rangka tidak terdaftar")
                Return False
                Exit Function
            End If
        Else
            MessageBox.Show("Nomor rangka tidak boleh kosong")
            Return False
        End If

        If (NoRangka = NoRangkaPengganti) Then
            MessageBox.Show("Nomor rangka dan nomor rangka pengganti tidak boleh sama")
            Return False
            Exit Function
        End If

        If (IsExistChassisNumberInArl(arl, NoRangka, index)) Then
            MessageBox.Show("Duplikasi Nomor Rangka")
            Return False
            Exit Function
        End If

        'VALIDASI NOMOR PENGGANTI RANGKA
        If (NoRangkaPengganti <> String.Empty) Then
            If (Not IsValidChassisNumber(NoRangkaPengganti)) Then
                MessageBox.Show("Nomor rangka pengganti tidak terdaftar")
                Return False
                Exit Function
            End If

            If (IsExistRefChassisNumberInArl(arl, NoRangkaPengganti, index)) Then
                MessageBox.Show("Nomor rangka penganti " + NoRangkaPengganti + " sudah ada")
                Return False
                Exit Function
            End If

            If (IsExistRefChassisNumberInEndCustomer(NoRangkaPengganti)) Then
                MessageBox.Show("Nomor rangka penganti " + NoRangkaPengganti + " sudah pernah digunakan")
                Return False
                Exit Function
            End If
        End If
        Return True
    End Function

    Private Function IsValidChassisMasterII(ByVal ChassisNumber As String) As String
        Dim CM As New ChassisMaster
        Dim CMs As ArrayList = New ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ChassisMaster), "FakturStatus", MatchType.Exact, CType(EnumChassisMaster.FakturStatus.Baru, Short)))
        criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, ChassisNumber))
        CMs = New FinishUnit.ChassisMasterFacade(User).Retrieve(criterias)

        If (CMs.Count > 0) Then
            CM = CMs(0)

            If CM.EndCustomer Is Nothing Then
                Return String.Empty
            Else
                If CM.EndCustomer.Customer Is Nothing Then
                    Return String.Empty
                Else
                    Return CM.EndCustomer.Customer.Code
                End If
            End If
        Else
            Return String.Empty
        End If

    End Function

    Private Function IsValidChassisNumber(ByVal ChassisNumber As String)
        Dim oCM As New ChassisMaster
        oCM = New ChassisMasterFacade(User).Retrieve(ChassisNumber)
        If (oCM.ChassisNumber.Trim() <> String.Empty) Then
            Return True
        End If
        Return False
    End Function

    Private Function IsExistRefChassisNumberInArl(ByVal arlList As ArrayList, ByVal ChassisNumber As String, ByVal index As Integer)
        Dim i As Integer = 0
        Dim _CM As ChassisMaster = New ChassisMasterFacade(User).Retrieve(ChassisNumber)
        For Each oCM As ChassisMaster In arlList
            If i <> index Then
                If (oCM.EndCustomer.RefChassisNumberID = _CM.ID) Then
                    Return True
                End If
            End If
            i = i + 1
        Next
        Return False
    End Function

    Private Function IsExistRefChassisNumberInEndCustomer(ByVal ChassisNumber As String)
        Dim CM As New ChassisMaster
        CM = New FinishUnit.ChassisMasterFacade(User).Retrieve(ChassisNumber)
        Dim EndCust As ArrayList = New ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(EndCustomer), "RefChassisNumberID", MatchType.Exact, CM.ID))
        EndCust = New FinishUnit.EndCustomerFacade(User).Retrieve(criterias)
        If (EndCust.Count > 0) Then
            Return True
        End If
        Return False
    End Function

    Private Function IsExistChassisNumberInArl(ByVal arlList As ArrayList, ByVal ChassisNumber As String, ByVal index As Integer)
        For Each oCM As ChassisMaster In arlList
            If index <> getIndex(arlList, ChassisNumber) Then
                If (oCM.ChassisNumber.Trim() = ChassisNumber) Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    Private Function LKPPCheckByVehicleType(ByVal lkppNumber As String, ByVal arlChassis As ArrayList) As Boolean
        Dim vReturn As Boolean = False
        If txtLKPPNumber.Text.Trim = "" Then
            MessageBox.Show("Konsumen terdeteksi LKPP, Nomor LKPP harap diisi.")
        Else
            Dim oLKPP As LKPPHeader = New LKPPHeaderFacade(User).Retrieve(lkppNumber)
            If Not IsNothing(oLKPP) Then
                For Each LKPPD As LKPPDetail In oLKPP.LKPPDetails
                    For Each objCM As ChassisMaster In arl
                        If objCM.VechileColor.VechileType.ID = LKPPD.VechileType.ID Then
                            vReturn = True
                        End If
                    Next
                Next
            End If
            If vReturn = False Then
                MessageBox.Show("Nomor LKPP tidak sesuai dengan tipe kendaraan yang dipilih.")
            End If
        End If

        Return vReturn

    End Function

    Private Function IsGovernmentInstitution(ByVal sName As String) As Boolean
        Dim sMCPList() As String = KTB.DNET.Lib.WebConfig.GetValue("ListOfMCPName").Split(";")
        Dim i As Integer
        Dim sTemp As String = ""
        If sName.Trim() = String.Empty Then
            Return False
        End If
        For i = 0 To sMCPList.Length - 1
            sTemp = sMCPList(i).Trim
            If sTemp.IndexOf("*") >= 0 Then
                If sTemp.Split("*").Length > 2 Then

                    Dim dtValue As DataTable = New DataTable
                    Dim Wc() As String = sTemp.Split("*")

                    Dim Filter As String = String.Empty

                    dtValue.Columns.Add(New DataColumn("Value"))
                    Dim dr As DataRow = dtValue.NewRow()
                    dr(0) = sName
                    dtValue.Rows.Add(dr)

                    For F As Integer = 0 To Wc.Length - 1
                        If Wc(F) <> "" Then
                            If F = 0 Then
                                Filter = Filter & "Value LIKE '%" & Wc(F) + "%' "
                            Else
                                If Filter <> "" Then
                                    Filter = Filter & " AND Value LIKE '%" & Wc(F) & "%' "
                                Else
                                    Filter = Filter & "Value LIKE '%" & Wc(F) + "%' "
                                End If


                            End If

                        End If
                    Next
                    Dim drs As DataRow() = dtValue.Select(Filter)
                    If Not IsNothing(drs) AndAlso drs.Length > 0 Then Return True

                ElseIf sTemp.StartsWith("*") Then
                    If sName.EndsWith(sTemp.Replace("*", "")) Then Return True
                ElseIf sTemp.EndsWith("*") Then
                    If sName.StartsWith(sTemp.Replace("*", "")) Then Return True
                Else
                    If sName.StartsWith(sTemp.Substring(0, sTemp.IndexOf("*"))) _
                        AndAlso sName.EndsWith(sTemp.Substring(sTemp.IndexOf("*") + 1)) Then
                        Return True
                    End If
                End If
            Else
                If sName = sTemp Then Return True
            End If
        Next

        Return False
    End Function

    Private Sub GroupingDeliveryCustomer(ByVal sales As SalesmanHeader, ByVal customer As Customer, ByVal loginDealer As Dealer, ByVal arr As ArrayList)
        Dim _listDetail As ArrayList = ssHelper.GetSession("DeliveryCustomerDetail")
        If _listDetail.Count > 0 Then
            Dim idStockDealers As String = String.Empty
            For Each item As DeliveryCustomerDetail In _listDetail
                If item.ChassisMaster.StockDealer <> 0 Then
                    idStockDealers = idStockDealers + item.ChassisMaster.StockDealer.ToString + ","
                End If
            Next
            idStockDealers = idStockDealers.Substring(0, idStockDealers.Length - 1)
            idStockDealers = "(" + idStockDealers + ")"
            Dim criteriasD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriasD.opAnd(New Criteria(GetType(Dealer), "ID", MatchType.InSet, idStockDealers))
            Dim _arrDetailBasedStockDealer As ArrayList = New DealerFacade(User).RetrieveByCriteria(criteriasD)
            Dim x As Integer = 0
            Dim _group As New ArrayList
            For Each item1 As Dealer In _arrDetailBasedStockDealer
                For Each item2 As DeliveryCustomerDetail In _listDetail
                    If item1.ID = item2.ChassisMaster.StockDealer Then
                        _group.Add(item2)
                    End If
                Next

                SalesDeliveryVehicle(sales, customer, item1, _group)
                _group.Clear()
            Next
        End If
    End Sub

    Private Function IsCancelValidateValid(ByVal objRevFaktur As RevisionFaktur) As Boolean
        Dim revisionFaktur As RevisionFaktur = New RevisionFakturFacade(User).Retrieve(objRevFaktur.ID)
        If revisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Validasi Then
            Return True
        End If
        Return False
    End Function

    Private Function ValidateSPKMatching(ByRef strMsg As String, ByVal objCM As ChassisMaster, ByVal objSPKHeader As SPKHeader) As Boolean
        Dim result As Boolean = False

        Dim dealerSystems As DealerSystems = New DealerSystemsFacade(User).RetrieveByDealerCode(objCM.Dealer.DealerCode)
        If IsNothing(dealerSystems) Then
            strMsg = strMsg & "Konfigurasi Dealer systems untuk Dealer" + objCM.Dealer.DealerName + "(" + objCM.Dealer.DealerCode + ") tidak ditemukan."
        Else
            'If dealerSystems.SystemID <> 1 Then
            '    If dealerSystems.isSPKMatchFaktur Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPKDetail), "SPKHeader.SPKNumber", MatchType.Exact, objSPKHeader.SPKNumber))
            criterias.opAnd(New Criteria(GetType(SPKDetail), "VechileColor.ID", MatchType.Exact, objCM.VechileColor.ID))
            Dim strQ As String = String.Empty
            strQ = String.Format("(SELECT b.ID FROM	dbo.SPKHeader a INNER JOIN dbo.SPKDetail b ON b.SPKHeaderID = a.ID AND b.RowStatus = a.RowStatus INNER JOIN dbo.SPKDetailCustomer	c ON b.ID = c.SPKDetailID AND c.RowStatus = a.RowStatus INNER JOIN dbo.CustomerRequest d ON c.CustomerRequestID = d.ID AND d.RowStatus = a.RowStatus WHERE	1 = 1 AND a.SPKNumber = '{0}' AND a.RowStatus = 0 AND b.VehicleColorID={1} AND d.CustomerCode='{2}')", objSPKHeader.SPKNumber, objCM.VechileColor.ID, txtCustomerCode.Text)
            criterias.opAnd(New Criteria(GetType(SPKDetail), "ID", MatchType.InSet, strQ))
            Dim spkDetailList As New ArrayList
            spkDetailList = New SPKDetailFacade(User).Retrieve(criterias)

            ''Additional
            'Is Temporary
            Dim _chm As ChassisMaster
            _chm = New ChassisMasterFacade(User).Retrieve(objCM.ChassisNumber)
            If Not IsNothing(_chm) AndAlso _chm.ID > 0 Then
                If _chm.EndCustomer.IsTemporary = 1 Then
                    Dim _1_SPKChassis As New SPKChassis
                    Dim _2_SPKChassis As New SPKChassis

                    If spkDetailList.Count > 0 Then
                        Dim ct As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKChassis), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        ct.opAnd(New Criteria(GetType(SPKChassis), "SPKDetail.ID", MatchType.Exact, CType(spkDetailList(0), SPKDetail).ID))
                        'ct.opAnd(New Criteria(GetType(SPKChassis), "MatchingType", MatchType.Exact, 1))
                        ct.opAnd(New Criteria(GetType(SPKChassis), "ChassisMaster.ID", MatchType.Exact, _chm.ID))

                        Dim sortColl As SortCollection = New SortCollection
                        sortColl.Add(New Sort(GetType(SPKChassis), "CreatedTime", Sort.SortDirection.DESC))
                        Dim obSPK As New ArrayList
                        obSPK = New SPKChassisFacade(User).Retrieve(ct, sortColl)
                        If (Not IsNothing(obSPK) AndAlso obSPK.Count > 0) Then
                            _1_SPKChassis = obSPK(0)
                            If _1_SPKChassis.MatchingType = 2 Then
                                strMsg = strMsg & vbNewLine & "Silahkan matching terlebih dahulu"
                                Return False
                            End If
                        Else
                            strMsg = strMsg & vbNewLine & "Jenis Kendaraan tidak sesuai dengan customer yang dipilih."
                            Return False
                        End If
                    End If

                    If 1 = 1 Then
                        Dim ct As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKChassis), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        ct.opAnd(New Criteria(GetType(SPKChassis), "ChassisMaster.ID", MatchType.Exact, _chm.ID))
                        'ct.opAnd(New Criteria(GetType(SPKChassis), "MatchingType", MatchType.Exact, 1))

                        Dim sortColl As SortCollection = New SortCollection
                        sortColl.Add(New Sort(GetType(SPKChassis), "CreatedTime", Sort.SortDirection.DESC))
                        Dim obSPK2 As New ArrayList
                        obSPK2 = New SPKChassisFacade(User).Retrieve(ct, sortColl)
                        If (Not IsNothing(obSPK2) AndAlso obSPK2.Count > 0) Then
                            _2_SPKChassis = obSPK2(0)
                            If _2_SPKChassis.MatchingType = 2 Then
                                strMsg = strMsg & vbNewLine & "Silahkan matching terlebih dahulu"
                                Return False
                            End If
                        Else
                            strMsg = strMsg & vbNewLine & "Jenis Kendaraan tidak sesuai dengan customer yang dipilih."
                            Return False
                        End If
                    End If

                    If _2_SPKChassis.ID > 0 Then
                        If Not (_1_SPKChassis.ID = _2_SPKChassis.ID) Then
                            strMsg = strMsg & vbNewLine & "SPK & Chassis tidak sesuai"
                            Return False

                        End If
                    End If

                    If _1_SPKChassis.ID = 0 OrElse _2_SPKChassis.ID = 0 Then
                        strMsg = strMsg & vbNewLine & "Silahkan melakukan matching terlebih Dahulu "
                        Return False
                    End If

                End If
            End If
            ''End Of Aditional

            If spkDetailList.Count = 0 Then
                Return True
            End If

            Dim idSPKDetailIDs As String = String.Empty
            For Each item As SPKDetail In spkDetailList
                If item.ID <> 0 Then
                    idSPKDetailIDs = idSPKDetailIDs + item.ID.ToString + ","
                End If
            Next
            idSPKDetailIDs = idSPKDetailIDs.Substring(0, idSPKDetailIDs.Length - 1)
            idSPKDetailIDs = "(" + idSPKDetailIDs + ")"

            Dim spkChassisList As ArrayList = New SPKChassisFacade(User).RetriveByListSPKDetailID(idSPKDetailIDs)

            If IsNothing(spkChassisList) Then
                result = True
            Else
                If spkChassisList.Count > 0 Then
                    Dim spkChassis As SPKChassis = New SPKChassis
                    'spkChassis = spkChassisList(0)
                    Dim spkDetail As SPKDetail = New SPKDetail
                    spkDetail = spkDetailList(0)
                    Dim spkChassisMatchingCount As Integer = 0
                    For Each iSPKChassis As SPKChassis In spkChassisList
                        If iSPKChassis.MatchingType = 1 Then
                            spkChassisMatchingCount += 1
                        ElseIf iSPKChassis.MatchingType = 2 Then
                            spkChassisMatchingCount -= 1
                        End If
                    Next
                    If spkDetail.Quantity >= spkChassisMatchingCount Then
                        result = True
                    Else
                        strMsg = strMsg & "Untuk Revisi Faktur No Chassis " & objCM.ChassisNumber & " tidak boleh di match dengan no spk yang sama " & objSPKHeader.SPKNumber
                    End If

                End If
                'End If
                '        Else
                '    ' for dealer non yana no need to check isspkmatchfaktur
                '    result = True
                'End If
            End If
        End If
        Return result
    End Function
#End Region

#Region "Transaction"
    Private Sub SaveChassisMasterProfile()
        Dim oCM As ChassisMaster
        Dim oCMPFac As New RevisionChassisMasterProfileFacade(User)
        Dim oCMP As RevisionChassisMasterProfile
        Dim oPHFac As New ProfileHeaderFacade(User)
        Dim oPHJenis As ProfileHeader = oPHFac.Retrieve("CBU_JENISKEND")
        Dim oPHModel As ProfileHeader = oPHFac.Retrieve("CBU_MODELKEND")
        Dim oPG As ProfileGroup
        Dim oPGFac As New ProfileGroupFacade(User)
        Dim GroupCode As String = ""
        Dim aCM As ArrayList = CType(Session("sessCM"), ArrayList)

        If Not IsNothing(oPHJenis) AndAlso oPHJenis.ID > 0 Then
            For Each di As DataGridItem In Me.dtgPengajuanFaktur.Items
                oCM = CType(aCM(di.ItemIndex), ChassisMaster)
                Dim _spkDetail As SPKDetail = CType(ssHelper.GetSession("SPKDETAILFAKTUR"), SPKDetail)
                If Not IsNothing(_spkDetail) Then
                    Dim arlProfile As ArrayList
                    Dim oSPKProfileFac As New SPKProfileFacade(User)
                    If hfSPKDetailCustomerID.Value = String.Empty Then
                        For Each oSPKDetailCust As SPKDetailCustomer In _spkDetail.SPKDetailCustomers
                            If txtCustomerCode.Text <> String.Empty Then
                                If oSPKDetailCust.CustomerRequest.CustomerCode = txtCustomerCode.Text Then
                                    hfSPKDetailCustomerID.Value = oSPKDetailCust.ID
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                    If _spkDetail.ID = 0 Then
                        _spkDetail = New SPKDetailCustomerFacade(User).Retrieve(CInt(hfSPKDetailCustomerID.Value)).SPKDetail
                    End If
                    Dim cSPKProfile As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    cSPKProfile.opAnd(New Criteria(GetType(SPKProfile), "SPKDetail.ID", MatchType.Exact, _spkDetail.ID))
                    cSPKProfile.opAnd(New Criteria(GetType(SPKProfile), "SPKDetailCustomer.ID", MatchType.Exact, hfSPKDetailCustomerID.Value.Trim()))

                    Dim vkGFacade As VehicleKindGroupFacade = New VehicleKindGroupFacade(User)
                    Dim vkFacade As VehicleKindFacade = New VehicleKindFacade(User)

                    arlProfile = oSPKProfileFac.Retrieve(cSPKProfile)
                    If arlProfile.Count = 0 Then
                        cSPKProfile = New CriteriaComposite(New Criteria(GetType(SPKProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        cSPKProfile.opAnd(New Criteria(GetType(SPKProfile), "SPKDetail.ID", MatchType.Exact, _spkDetail.ID))
                        cSPKProfile.opAnd(New Criteria(GetType(SPKProfile), "SPKDetailCustomerID", MatchType.IsNull))

                        arlProfile = oSPKProfileFac.Retrieve(cSPKProfile)
                    End If
                    For Each item As SPKProfile In arlProfile
                        oCMP = New RevisionChassisMasterProfile
                        oCMP = GetCMProfile(oCM, item.ProfileGroup, item.ProfileHeader)
                        oCMP.ChassisMaster = oCM
                        oCMP.EndCustomer = New EndCustomerFacade(User).Retrieve(oCM.EndCustomer.ID)
                        oCMP.ProfileGroup = item.ProfileGroup
                        oCMP.ProfileHeader = item.ProfileHeader
                        If item.ProfileHeader.Code.Equals("CBU_MODELKEND") Then
                            If Not String.IsNullorEmpty(item.ProfileValue) Then
                                oCMP.ProfileValue = vkFacade.Retrieve(CType(item.ProfileValue, Integer)).Code
                            Else
                                oCMP.ProfileValue = item.ProfileValue
                            End If
                        ElseIf item.ProfileHeader.Code.Equals("CBU_JENISKEND") Then
                            If Not String.IsNullorEmpty(item.ProfileValue) Then
                                oCMP.ProfileValue = vkGFacade.Retrieve(CType(item.ProfileValue, Integer)).Code
                            Else
                                oCMP.ProfileValue = item.ProfileValue
                            End If
                        Else
                            oCMP.ProfileValue = item.ProfileValue
                        End If
                        If oCMP.ID < 1 Then
                            oCMPFac.Insert(oCMP)
                        Else
                            oCMPFac.Update(oCMP)
                        End If
                    Next
                Else
                    GroupCode = "cust_prf_" & oCM.Category.CategoryCode.ToLower
                    oPG = oPGFac.Retrieve(GroupCode)

                    oCMP = GetCMProfile(oCM, oPG, oPHJenis)
                    oCMP.ChassisMaster = oCM
                    oCMP.EndCustomer = New EndCustomerFacade(User).Retrieve(oCM.EndCustomer.ID)
                    oCMP.ProfileGroup = oPG
                    oCMP.ProfileHeader = oPHJenis
                    oCMP.ProfileValue = oCM.VehicleKind.VehicleKindGroup.Code
                    If oCMP.ID < 1 Then
                        oCMPFac.Insert(oCMP)
                    Else
                        oCMPFac.Update(oCMP)
                    End If

                    oCMP = GetCMProfile(oCM, oPG, oPHModel)
                    oCMP.ChassisMaster = oCM
                    oCMP.EndCustomer = New EndCustomerFacade(User).Retrieve(oCM.EndCustomer.ID)
                    oCMP.ProfileGroup = oPG
                    oCMP.ProfileHeader = oPHModel
                    oCMP.ProfileValue = oCM.VehicleKind.Code
                    If oCMP.ID < 1 Then
                        oCMPFac.Insert(oCMP)
                    Else
                        oCMPFac.Update(oCMP)
                    End If

                End If
            Next
        End If
    End Sub

    Private Sub SalesDeliveryVehicle(ByVal sales As SalesmanHeader, ByVal customer As Customer, ByVal loginDealer As Dealer, ByVal arr As ArrayList)
        Dim oDeliveryCustomerHeaderFacade As New DeliveryCustomerHeaderFacade(User)
        Dim oDeliveryCustomerHeader As DeliveryCustomerHeader
        Dim result As Integer
        If Not ssHelper.GetSession("DeliveryCustomerHeader") Is Nothing Then
            oDeliveryCustomerHeader = CType(ssHelper.GetSession("DeliveryCustomerHeader"), DeliveryCustomerHeader)
        Else
            oDeliveryCustomerHeader = New DeliveryCustomerHeader
        End If
        oDeliveryCustomerHeader.Dealer = Nothing
        oDeliveryCustomerHeader.Customer = customer
        If sales Is Nothing Then
            oDeliveryCustomerHeader.SalesmanID = Nothing
        Else
            oDeliveryCustomerHeader.SalesmanID = sales.ID
        End If
        oDeliveryCustomerHeader.FromDealer = loginDealer.ID
        oDeliveryCustomerHeader.PostingDate = Date.Today
        oDeliveryCustomerHeader.RegDONumber = "Buat Faktur"

        If Session("MODE") = "insert" Then
            result = oDeliveryCustomerHeaderFacade.InsertTransaction(oDeliveryCustomerHeader, arr)
            ssHelper.SetSession("DeliveryCustomerHeader", oDeliveryCustomerHeader)
        ElseIf Session("MODE") = "update" Then
            _arrDetailDel = ssHelper.GetSession("DelDetail")
            result = oDeliveryCustomerHeaderFacade.UpdateTransaction(oDeliveryCustomerHeader, arr, _arrDetailDel)
        End If
    End Sub

    Private Sub UpdatePendingDescAndLastUpdateProfile(ByVal chassisNumber As String)
        Dim lastUpdateProfile = "1753-01-01 00:00:00.000"
        Dim facade As New ChassisMasterFacade(User)
        Dim result = facade.ExecuteSPChassisMasterProfile(chassisNumber, "", Convert.ToDateTime(lastUpdateProfile))
        If result = 0 Then
            MessageBox.Show(SR.SaveSuccess)
        Else
            MessageBox.Show(SR.SaveFail)
        End If
    End Sub
#End Region
End Class