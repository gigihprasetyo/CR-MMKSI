#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.LKPP
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.WebCC
Imports KTB.DNet.Security
Imports System.Collections.Generic
Imports System.Linq
#End Region

<System.Runtime.InteropServices.GuidAttribute("8D45D5F8-CA7E-468F-BB74-CAFE8017708A")> Public Class FrmEntryInvoiceRevision
    Inherits System.Web.UI.Page

#Region " Custom Variable Declaration "
    Private _sesshelper As SessionHelper = New SessionHelper
    Private _objEndCustomer As EndCustomer
    Private _objChassisMaster As ChassisMaster
    Private _objRevisionFaktur As RevisionFaktur
    Private _objDealer As Dealer
    Private _sErrorMessage As String = ""
    Private _sSuccessMessage As String = ""
    Private _bIsUpdateNeeded As Boolean = False
    Private _bIsValidating As Boolean = False
    Private _ctlJenisKendaraan As String = ""
    Private _ctlModelKendaraan As String = ""
    Private _ctlLeasing As String = ""
    Private _ctlKaroseri As String = ""
    Private _ctlPurcStat As String = ""
    Private _formMode As Integer
    Dim alGroup As ArrayList = New ArrayList
    Private CVGroup As String = "cust_prf_cv"
    Private PCGroup As String = "cust_prf_pc"
    Private LCVGroup As String = "cust_prf_lcv"
#End Region

#Region " Event Handler "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        initForm()
    End Sub

    Private Sub InitiateAuthorization()

        If Not SecurityProvider.Authorize(Context.User, SR.RevisiFakturInput_Privilege) And _formMode = EnumDNET.enumFormMode.Add Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=REVISI FAKTUR - Permohonan Revisi Faktur Kendaraan")
        End If

        If Not SecurityProvider.Authorize(Context.User, SR.RevisiFakturDaftarEdit_Privilege) And _formMode = EnumDNET.enumFormMode.Edit Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=REVISI FAKTUR - Permohonan Revisi Faktur Kendaraan")
        End If

        If Not SecurityProvider.Authorize(Context.User, SR.RevisiFakturDaftarLihat_Privilege) And _formMode = EnumDNET.enumFormMode.View Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=REVISI FAKTUR - Permohonan Revisi Faktur Kendaraan")
        End If

        Dim objDealer As Dealer = CType(_sesshelper.GetSession("DEALER"), Dealer)

        If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            Me.btnCancelValidate.Visible = SecurityProvider.Authorize(Context.User, SR.RevisiFakturDaftarEdit_Privilege)
            Me.btnValidate.Visible = SecurityProvider.Authorize(Context.User, SR.RevisiFakturDaftarEdit_Privilege)
            Me.btnSave.Visible = SecurityProvider.Authorize(Context.User, SR.RevisiFakturInput_Privilege)
        Else
            Me.btnCancelValidate.Visible = False
            Me.btnValidate.Visible = False
            Me.btnSave.Visible = False
        End If


        If Not IsNothing(_sesshelper.GetSession("disabledSave")) Then
            If btnSave.Visible Then
                If CType(_sesshelper.GetSession("disabledSave"), Boolean) Then
                    btnSave.Enabled = False
                End If
            End If

            If btnValidate.Visible Then
                If CType(_sesshelper.GetSession("disabledSave"), Boolean) Then
                    btnValidate.Enabled = False
                End If
            End If
        End If
    End Sub

    Private Sub initForm()
        _objChassisMaster = CType(GetFromSession("ChassisMaster"), ChassisMaster)
        _formMode = CType(Request.QueryString("mode"), Integer)
        _objRevisionFaktur = CType(GetFromSession("RevisionFaktur"), RevisionFaktur)

        If Not _objRevisionFaktur Is Nothing Then
            _objEndCustomer = New EndCustomer
            _objEndCustomer = _objRevisionFaktur.EndCustomer
        Else
            _objEndCustomer = New EndCustomer
            _objEndCustomer = _objChassisMaster.EndCustomer
        End If
        BindEndCustomerProfileToControl()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        If Not IsPostBack Then
            LoadPage()

            If Request.QueryString!t <> "" Then
                hidRevisionType.Value = Request.QueryString!t

                Dim revisionType As RevisionType = New RevisionType
                revisionType = GetRevisionType(hidRevisionType.Value)
                pnlRevType.Visible = True
                lblRevisionType.Text = revisionType.Description

                Select Case revisionType.ID
                    Case 1 'Perubahan KTP Sama
                        OpenRevisionSameTDPForm()

                    Case 2 'Perubahan KTP Beda
                        If _formMode = EnumDNET.enumFormMode.Add Then
                            Response.Redirect("FrmEntryInvoiceRevisionFaktur.aspx?ChassisMasterID=" & _objChassisMaster.ID & "&mode=" & _formMode)
                        Else
                            OpenRevisionSameTDPForm()
                        End If

                    Case 3 'Perubahan Tanggal faktur
                        OpenRevisionFakturDateForm()

                    Case 4 'Perubahan Data No Rangka
                        OpenRevisionChassisDataForm()

                End Select
            End If

            GetRenderControl()
            ManageDDLControl()


        End If

        Me.btnSave.Attributes.Add("onclick", "ToggleDisclaimerAgreement();")
        Me.btnKembali.Attributes.Add("onclick", "DisclaimerAgreed = true;")
        Me.btnBindModel.Attributes.Add("onclick", "DisclaimerAgreed = true;")
        Me.btnValidate.Attributes.Add("onclick", "ToggleDisclaimerAgreement();")
        Me.btnCancelValidate.Attributes.Add("onclick", "DisclaimerAgreed = true;")
        Me.chkPelanggaranWilayah.Attributes.Add("onclick", "ToggleAdditionalInformation('chkPelanggaranWilayah');")
        Me.chkPembayaranPenalti.Attributes.Add("onclick", "ToggleAdditionalInformation('chkPembayaranPenalti');")
        Me.chkSuratReferensi.Attributes.Add("onclick", "ToggleAdditionalInformation('chkSuratReferensi');")
        Me.imgAddInfo.Attributes.Add("onclick", "MinimizeAddtionalInformation();")
        Me.chkDisclaimerAgreement.Attributes.Add("onclick", "ToggleDisclaimerAgreement();")
        Me.btnCancel.Attributes.Add("onclick", "DisclaimerAgreed = true; return confirm('" & SR.CancelConfirmation & "');")

        'LKPP
        Me.chkLKPP.Attributes.Add("onclick", "ToggleAdditionalInformation('chkLKPP');")
        Me.lblLKPPNumber.Attributes.Add("onclick", "ShowLKPPSelection();")
        Me.txtNoLKPP.Attributes.Add("readonly", "readonly")
        'LKPP

        'Fleet
        Me.chkFleet.Attributes.Add("onclick", "ToggleAdditionalInformation('chkFleet');")
        Me.lblNoRegRequest.Attributes.Add("onclick", "ShowFleetSelection();")
        Me.txtNoFleetReq.Attributes.Add("readonly", "readonly")
        'Fleet
    End Sub

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        _sesshelper.SetSession("Customer", Nothing)
        _sesshelper.SetSession("RevisionFaktur", Nothing)

        If _formMode = EnumDNET.enumFormMode.Add Then
            Response.Redirect("FrmInvoiceRevision.aspx")
        Else
            Dim url As String = CType(Session("FrmEntryInvoiceRevision_CalledBy"), String)
            If Not url Is Nothing Then
                Server.Transfer(url)
            End If
        End If
    End Sub

    Private Sub txtCustomerID_TextChanged(sender As Object, e As EventArgs) Handles txtCustomerID.TextChanged

        If txtCustomerID.Text.Trim.Length > 6 Then

            Dim objCustomerFacade As CustomerFacade = New CustomerFacade(User)
            Dim newCustomer As Customer = objCustomerFacade.Retrieve(txtCustomerID.Text.Trim)

            If newCustomer.ID > 0 Then
                If Not IsNothing(_objChassisMaster.EndCustomer.Customer.MyCustomerRequest) And Not IsNothing(newCustomer.MyCustomerRequest) Then
                    Dim oldCRP As CustomerRequestProfile = _objChassisMaster.EndCustomer.Customer.MyCustomerRequest.GetCustomerRequestProfile("NOKTP")
                    Dim newCRP As CustomerRequestProfile = newCustomer.MyCustomerRequest.GetCustomerRequestProfile("NOKTP")
                    If Not IsNothing(oldCRP) And Not IsNothing(newCRP) Then
                        If newCRP.ProfileValue.ToString <> oldCRP.ProfileValue.ToString Then
                            btnSave.Enabled = False
                            MessageBox.Show("No KTP tidak sama dengan kode customer sebelumnya, silahkan buat pengajuan tipe KTP beda untuk melanjutkan")
                        Else
                            BindCustomer(newCustomer)
                            btnSave.Enabled = True
                            _sesshelper.SetSession("newCustomer", newCustomer)
                        End If
                    Else
                        txtCustomerID.Text = lblCustCode.Text
                        MessageBox.Show("Kode customer tidak punya No KTP")
                    End If
                Else
                    txtCustomerID.Text = lblCustCode.Text
                    MessageBox.Show("Kode customer tidak punya data customer request")
                End If
            End If
        End If


    End Sub

    Private Sub btnDisableLeasing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisableLeasing.Click
        Dim ddlPayment As DropDownList = GetDDLProfile("CBU_WAYPAID1")

        Dim ddlLeasing As DropDownList = pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlLeasing"), String))

        If ddlPayment.SelectedItem.Text.Trim = "TUNAI" Then
            ddlLeasing.SelectedIndex = 0
            ddlLeasing.Enabled = False
        ElseIf ddlPayment.SelectedItem.Text = "KREDIT" Then
            ddlLeasing.Enabled = True
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not Page.IsValid Then
            MessageBox.Show("Input data belum lengkap")
            Exit Sub
        End If

        If Not ValidateNomorReferensi(txtSRNomorSurat.Text.Trim) Then
            If Not IsNothing(_objRevisionFaktur) AndAlso (_objRevisionFaktur.RevisionType.ID = EnumDNET.enumRevType.RN OrElse _objRevisionFaktur.RevisionType.ID = EnumDNET.enumRevType.RS) Then
                MessageBox.Show("Nomor Referensi tidak valid.")
                Exit Sub
            End If
        End If
        Dim ddlJenis As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlJenis"), String))
        Dim ddlModel As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlModel"), String))
        Dim ddlLeasing As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlLeasing"), String))

        If ddlLeasing.SelectedIndex = 0 Then
            Dim oPHFac As New ProfileHeaderFacade(User)
            Dim oPHPayment As ProfileHeader = oPHFac.Retrieve("CBU_WAYPAID1")

            Dim oPG As ProfileGroup
            Dim oPGFac As New ProfileGroupFacade(User)
            oPG = oPGFac.Retrieve("cust_prf_" & _objChassisMaster.Category.CategoryCode.ToLower)
            Dim oPHTGFac As New ProfileHeaderToGroupFacade(User)
            Dim cCMP As New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cCMP.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, oPG.ID))
            Dim oPHTGList As ArrayList = oPHTGFac.RetrieveByCriteria(cCMP)

            Dim _ctlPayment As String = ""

            For Each phtg As ProfileHeaderToGroup In oPHTGList
                If phtg.ProfileHeader.ID = oPHPayment.ID Then
                    _ctlPayment = "DDLIST" & phtg.ID.ToString & "_" & phtg.ProfileGroup.ID.ToString
                End If
            Next

            Dim ddlPayment As DropDownList = Me.pnlInformasion.FindControl(_ctlPayment)
        End If

        Dim oVK As VehicleKind = New VehicleKindFacade(User).Retrieve(CType(ddlModel.SelectedValue, Integer))
        If oVK.VehicleKindGroupID <> ddlJenis.SelectedValue Then
            MessageBox.Show("Simpan gagal.\n Model kendaraan tidak sesuai dengan jenis kendaraan")
            Exit Sub
        End If

        SaveInvoiceRevision()
        If (_sErrorMessage <> "") Then
            MessageBox.Show(_sErrorMessage)
        Else
            MessageBox.Show(_sSuccessMessage)

            If _formMode = EnumDNET.enumFormMode.Add Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(RevisionFaktur), "ChassisMaster.ID", MatchType.Exact, _objChassisMaster.ID))
                criterias.opAnd(New Criteria(GetType(RevisionFaktur), "RevisionStatus", MatchType.Exact, CType(EnumDNET.enumFakturKendaraanRev.Baru, Short)))
                Dim objListRevisionFaktur As ArrayList = New RevisionFakturFacade(User).Retrieve(criterias)
                If objListRevisionFaktur.Count > 0 Then
                    _objRevisionFaktur = CType(objListRevisionFaktur(0), RevisionFaktur)
                    _sesshelper.SetSession("RevisionFaktur", _objRevisionFaktur)
                    _sesshelper.SetSession("ChassisMaster", _objRevisionFaktur.ChassisMaster)
                    Server.Transfer("FrmEntryInvoiceRevision.aspx?t=" & _objRevisionFaktur.RevisionTypeID & "&mode=" & EnumDNET.enumFormMode.Edit)
                End If
            Else
                _objRevisionFaktur = New RevisionFakturFacade(User).Retrieve(_objRevisionFaktur.ID)
                _sesshelper.SetSession("RevisionFaktur", _objRevisionFaktur)
                _sesshelper.SetSession("ChassisMaster", _objRevisionFaktur.ChassisMaster)
                Server.Transfer("FrmEntryInvoiceRevision.aspx?t=" & _objRevisionFaktur.RevisionTypeID & "&mode=" & EnumDNET.enumFormMode.Edit)
            End If

        End If

    End Sub

    Private Sub btnValidate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidate.Click



        If Not IsNothing(sender) AndAlso Not Page.IsValid Then
            MessageBox.Show("Input data belum lengkap")
            Exit Sub
        End If

        Dim mess As String = String.Empty

        If _objRevisionFaktur.RevisionType.ID = EnumDNET.enumRevType.RS AndAlso Not CommonFunction.RevFakturIsValidData(_objChassisMaster, mess) Then
            Dim rFacade As Integer = New RevisionFakturFacade(User).UpdateRemark(_objRevisionFaktur, mess)
            MessageBox.Show("Proses Gagal\n" & mess)
            Exit Sub
        End If

        If lblNamaPesananKhusus.Text <> String.Empty Then
            If _objRevisionFaktur.RevisionType.ID = EnumDNET.enumRevType.RN Or _objRevisionFaktur.RevisionType.ID = EnumDNET.enumRevType.RS Then
                If txtSRNomorSurat.Text = String.Empty Then
                    MessageBox.Show("Pesanan Khusus : Surat Referensi harus diisi")
                    Exit Sub
                End If
            End If
        End If

        _bIsValidating = True

        Dim ddlJenis As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlJenis"), String))
        Dim ddlModel As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlModel"), String))
        Dim ddlLeasing As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlLeasing"), String))

        If ddlLeasing.SelectedIndex = 0 Then
            Dim oPHFac As New ProfileHeaderFacade(User)
            Dim oPHPayment As ProfileHeader = oPHFac.Retrieve("CBU_WAYPAID1")

            Dim oPG As ProfileGroup
            Dim oPGFac As New ProfileGroupFacade(User)
            oPG = oPGFac.Retrieve("cust_prf_" & _objChassisMaster.Category.CategoryCode.ToLower)
            Dim oPHTGFac As New ProfileHeaderToGroupFacade(User)
            Dim cCMP As New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cCMP.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, oPG.ID))
            Dim oPHTGList As ArrayList = oPHTGFac.RetrieveByCriteria(cCMP)

            Dim _ctlPayment As String = ""

            For Each phtg As ProfileHeaderToGroup In oPHTGList
                If phtg.ProfileHeader.ID = oPHPayment.ID Then
                    _ctlPayment = "DDLIST" & phtg.ID.ToString & "_" & phtg.ProfileGroup.ID.ToString
                End If
            Next

            Dim ddlPayment As DropDownList = Me.pnlInformasion.FindControl(_ctlPayment)
        End If

        Dim oVK As VehicleKind = New VehicleKindFacade(User).Retrieve(CType(ddlModel.SelectedValue, Integer))
        If oVK.VehicleKindGroupID <> ddlJenis.SelectedValue Then
            MessageBox.Show("Simpan gagal.\n Model kendaraan tidak sesuai dengan jenis kendaraan")
            Exit Sub
        End If

        SaveInvoiceRevision()

        Dim isValid As Boolean = True

        If _sErrorMessage = "" Then
            If _objEndCustomer.Customer Is Nothing Then
                MessageBox.Show("Belum bisa divalidasi karena data konsumen masih kosong")
            Else
                Dim objChassisMasterFac As ChassisMasterFacade = New ChassisMasterFacade(User)

                If Not objChassisMasterFac.IsAreaViolationFree(_objChassisMaster) Then
                    If _objEndCustomer.AreaViolationFlag = "X" Or _objEndCustomer.ReferenceLetterFlag = "X" Then
                        isValid = True
                    Else
                        If _objRevisionFaktur.RevisionType.ID = EnumDNET.enumRevType.RT Or _objRevisionFaktur.RevisionType.ID = EnumDNET.enumRevType.RR Then
                            isValid = True
                        Else
                            isValid = False
                            _sErrorMessage = _sErrorMessage & "Informasi Tambahan - Pelanggaran Wilayah atau Surat Referensi wajib diisi\n"
                        End If
                    End If
                End If

                If _objChassisMaster.DiscountAmount > 0 Then
                    If _objEndCustomer.PenaltyFlag = "X" Or _objEndCustomer.ReferenceLetterFlag = "X" Then
                        isValid = True
                    Else
                        If _objRevisionFaktur.RevisionType.ID = EnumDNET.enumRevType.RT Or _objRevisionFaktur.RevisionType.ID = EnumDNET.enumRevType.RR Then
                            isValid = True
                        Else
                            isValid = False
                            _sErrorMessage = _sErrorMessage & "Informasi Tambahan - Pembayaran Penalti atau Surat Referensi wajib diisi\n"
                        End If
                    End If
                End If

                If isValid Then
                    _objRevisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Validasi ' "1"
                    _objRevisionFaktur.NewValidationDate = Now
                    _objRevisionFaktur.NewValidationBy = User.Identity.Name

                    Dim objRevisionFakturFac As RevisionFakturFacade = New RevisionFakturFacade(User)
                    Dim iUpdated As Integer = objRevisionFakturFac.Update(_objRevisionFaktur)
                    If iUpdated <> -1 Then
                        ToggleButton()
                        Me.lblValidatedBy.Text = "<b>" & UserInfo.Convert(User.Identity.Name) & "</b> pada tanggal <b>" & Date.Now.ToString("dd/MM/yyyy HH:mm:ss") & "</b>"
                        Me.lblStatus.Text = CType(_objRevisionFaktur.RevisionStatus, EnumDNET.enumFakturKendaraanRev).ToString()

                        MessageBox.Show(SR.ValidateSucces())
                        UpdateSpkHeader(True)

                        _objRevisionFaktur = New RevisionFakturFacade(User).Retrieve(_objRevisionFaktur.ID)
                        _sesshelper.SetSession("RevisionFaktur", _objRevisionFaktur)
                        _sesshelper.SetSession("ChassisMaster", _objRevisionFaktur.ChassisMaster)
                        Dim msgMatch As String = ""


                        Server.Transfer("FrmEntryInvoiceRevision.aspx?t=" & _objRevisionFaktur.RevisionTypeID & "&mode=" & EnumDNET.enumFormMode.Edit & "&Msg=" & Server.UrlEncode(msgMatch))
                    Else
                        MessageBox.Show(SR.ValidateFail)
                    End If
                Else
                    MessageBox.Show(_sErrorMessage)
                End If
            End If
        Else
            MessageBox.Show(_sErrorMessage)
        End If
    End Sub

    Private Sub UpdateSpkHeader(ByVal bValidate As Boolean)
        Try

            Dim spkHeaderData As SPKHeader = _objRevisionFaktur.EndCustomer.RevisionSPKFaktur.SPKHeader
            Dim endCustomerID As Integer = _objRevisionFaktur.EndCustomer.ID

            If Not IsSpkHeaderNeedToUpdate(spkHeaderData, endCustomerID, bValidate) Then
                Exit Sub
            Else
                UpdateSpkHeaderAndInsertStatusChangeHistory(spkHeaderData, bValidate)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub UpdateSpkHeaderAndInsertStatusChangeHistory(ByVal spkHeaderData As SPKHeader, ByVal bValidate As Boolean)
        Dim newStatus As String = String.Empty
        Dim oldStatus As String = spkHeaderData.Status

        If bValidate Then
            newStatus = EnumStatusSPK.Status.Selesai
        Else
            newStatus = GetPreviousStatus(spkHeaderData.SPKNumber, newStatus)
        End If

        JustUpdateSpkHeader(spkHeaderData, newStatus)
        ' InsertStatusChangeHistory(spkHeaderData.SPKNumber, newStatus, oldStatus) 'sudah jalan saat execute updatespkheader
    End Sub

    Private Function IsSpkHeaderNeedToUpdate(ByVal spkHeaderData As SPKHeader, ByVal endCustomerID As Integer, ByVal bValidate As Boolean) As Boolean

        If _objRevisionFaktur.RevisionTypeID <> EnumDNET.enumRevType.RS Then
            Return False
        End If

        If bValidate = False And spkHeaderData.Status <> EnumStatusSPK.Status.Selesai Then
            Return False
        End If

        If bValidate = True And spkHeaderData.Status = EnumStatusSPK.Status.Selesai Then
            Return False
        End If

        If Not IsSpkQuantityValid(spkHeaderData, bValidate) Then
            Return False
        End If

        Return True
    End Function

    Private Function IsSpkQuantityValid(ByVal spkheaderData As SPKHeader, ByVal bValidate As Boolean) As Boolean
        Try
            Dim iQtySpk As Integer = GetQtySpk(spkheaderData)
            Dim iQtyFakturNormal As Integer = GetQtyFakturNormal(spkheaderData)
            Dim iQtyFakturRevisi As Integer = GetQtyFakturRevisi(spkheaderData)

            If bValidate = False Then
                If iQtySpk > (iQtyFakturNormal + iQtyFakturRevisi) Then
                    Return True
                Else
                    Return False
                End If
            Else
                If iQtySpk = (iQtyFakturNormal + iQtyFakturRevisi) Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Throw New Exception("Error dalam validasi quantity SPK")
        End Try


    End Function

    Private Function GetQtySpk(ByVal spkheaderData As SPKHeader) As Integer
        Dim result As Integer = 0

        For Each spkDetailData As SPKDetail In spkheaderData.SPKDetails
            result += spkDetailData.Quantity
        Next

        Return result
    End Function


    Private Function GetQtyFakturNormal(ByVal spkheaderData As SPKHeader) As Integer
        Dim result As Integer = 0

        For Each spkFakturData As SPKFaktur In spkheaderData.SPKFakturs
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.ID", MatchType.Exact, spkFakturData.EndCustomer.ID))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "FakturStatus", MatchType.No, 0)) ' <> baru
            Dim lstChassisMaster As ArrayList = New FinishUnit.ChassisMasterFacade(User).Retrieve(criterias)

            result += lstChassisMaster.Count
        Next

        Return result
    End Function

    Private Function GetQtyFakturRevisi(ByVal spkheaderData As SPKHeader) As Integer
        Dim result As Integer = 0

        ' For Each spkFakturData As SPKFaktur In spkheaderData.SPKFakturs
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionSPKFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(RevisionSPKFaktur), "SPKHeader.ID", MatchType.Exact, spkheaderData.ID))
        Dim lstRevisionSPKFaktur As ArrayList = New RevisionSPKFakturFacade(User).Retrieve(criterias)

        For Each revisionSPKFakturData As RevisionSPKFaktur In lstRevisionSPKFaktur
            Dim criteriasRevision As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriasRevision.opAnd(New Criteria(GetType(RevisionFaktur), "EndCustomer.ID", MatchType.Exact, revisionSPKFakturData.EndCustomer.ID))
            criteriasRevision.opAnd(New Criteria(GetType(RevisionFaktur), "RevisionStatus", MatchType.No, 0)) ' <> baru
            Dim lstRevisionFaktur As ArrayList = New RevisionFakturFacade(User).Retrieve(criteriasRevision)
            result += lstRevisionFaktur.Count
        Next
        ' Next
        Return result
    End Function

    Private Sub JustUpdateSpkHeader(ByVal spkHeaderData As SPKHeader, ByVal newStatus As String)
        Try
            spkHeaderData.Status = CInt(newStatus)

            Dim objSPKHeaderFac As SPKHeaderFacade = New SPKHeaderFacade(User)
            objSPKHeaderFac.Update(spkHeaderData)
        Catch ex As Exception
            Throw New Exception("Gagal dalam mengupdate status SPK")
        End Try

    End Sub

    Private Function GetPreviousStatus(ByVal spkNumber As String, ByRef newStatus As String) As String
        Dim result As String = String.Empty
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, spkNumber))

        Dim arraylistSpkStatusHistory As ArrayList = New StatusChangeHistoryFacade(User).Retrieve(criterias)

        If arraylistSpkStatusHistory.Count > 0 Then
            Dim listSpkStatusHistory As List(Of StatusChangeHistory) = arraylistSpkStatusHistory.Cast(Of StatusChangeHistory)().ToList()
            Dim lastSpkStatusHistory As StatusChangeHistory = listSpkStatusHistory.OrderByDescending(Function(x) x.id)(0)
            result = lastSpkStatusHistory.OldStatus.ToString()

        End If
        newStatus = result
        Return result
    End Function

    Private Sub InsertStatusChangeHistory(ByVal spkNumber As String, ByVal newStatus As String, ByVal oldStatus As String)
        Dim statusChangeHistoryData As New StatusChangeHistory
        statusChangeHistoryData.DocumentType = 6
        statusChangeHistoryData.DocumentRegNumber = spkNumber
        statusChangeHistoryData.OldStatus = CInt(oldStatus)
        statusChangeHistoryData.NewStatus = CInt(newStatus)
        statusChangeHistoryData.RowStatus = 0

        Dim iReturn As Integer = New StatusChangeHistoryFacade(User).Insert(statusChangeHistoryData)
    End Sub

    Private Sub btnCancelValidate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelValidate.Click
        If IsCancelValidateValid(_objRevisionFaktur) Then
            _objRevisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Baru
            _objRevisionFaktur.NewValidationDate = Date.Parse("1/1/1753 12:00:00 AM")
            _objRevisionFaktur.NewValidationBy = String.Empty

            Dim iUpdated As Integer = New RevisionFakturFacade(User).Update(_objRevisionFaktur)
            If iUpdated <> -1 Then
                MessageBox.Show(SR.UpdateSucces)

                UpdateSpkHeader(False)
                _objRevisionFaktur = New RevisionFakturFacade(User).Retrieve(_objRevisionFaktur.ID)
                _sesshelper.SetSession("RevisionFaktur", _objRevisionFaktur)
                _sesshelper.SetSession("ChassisMaster", _objRevisionFaktur.ChassisMaster)
                Server.Transfer("FrmEntryInvoiceRevision.aspx?t=" & _objRevisionFaktur.RevisionTypeID & "&mode=" & EnumDNET.enumFormMode.Edit)
            Else
                MessageBox.Show(SR.UpdateFail)
            End If
        Else
            MessageBox.Show("Proses batal tidak berhasil, karena data sudah di proses")
        End If

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        _objRevisionFaktur = CType(GetFromSession("RevisionFaktur"), RevisionFaktur)

        If Not IsNothing(_objRevisionFaktur) Then

            Dim oRevisionFakturFacade As RevisionFakturFacade = New RevisionFakturFacade(User)
            Dim ProfileList As ArrayList = New RevisionChassisMasterProfileFacade(User).GetRevisionChassisMasterProfileByChassisEndCustomer(_objRevisionFaktur.ChassisMaster.ID, _objRevisionFaktur.EndCustomer.ID)

            Dim result As Integer = oRevisionFakturFacade.CancelFaktur(_objRevisionFaktur, ProfileList)

            If result <> -1 Then
                MessageBox.Show("Data berhasil dibatalkan")

                _sesshelper.SetSession("Customer", Nothing)
                _sesshelper.SetSession("RevisionFaktur", Nothing)

                If _formMode = EnumDNET.enumFormMode.Add Then
                    Response.Redirect("FrmInvoiceRevision.aspx")
                Else
                    Dim url As String = CType(Session("FrmEntryInvoiceRevision_CalledBy"), String)
                    If Not url Is Nothing Then
                        Server.Transfer(url)
                    End If
                End If
            Else
                MessageBox.Show("Data gagal dibatalkan ")
            End If

        End If
    End Sub
#End Region

#Region " Custom Method "
    Private Sub BindEndCustomerProfileToControl()
        Dim profileGroup As ProfileGroup = New ProfileGroup

        Select Case _objChassisMaster.Category.CategoryCode
            Case "PC"
                profileGroup = New ProfileGroupFacade(User).Retrieve(PCGroup)

            Case "CV"
                profileGroup = New ProfileGroupFacade(User).Retrieve(CVGroup)

            Case "LCV"
                profileGroup = New ProfileGroupFacade(User).Retrieve(LCVGroup)
        End Select

        If IsOpenRevision Then
            RenderProfilePanel(_objChassisMaster, profileGroup, EnumProfileType.ProfileType.CHASSISMASTER, pnlInformasion, False)
        Else
            RenderProfilePanel(_objChassisMaster, profileGroup, EnumProfileType.ProfileType.CHASSISMASTER, pnlInformasion, True)
        End If
    End Sub

    Private Function GetFromSession(ByVal sObject As String) As Object
        If Session("DEALER") Is Nothing Then
            Response.Redirect("../SessionExpired.htm")
        Else
            'Todo session
            Return Session(sObject)
        End If
    End Function

    Private Sub RenderProfilePanel(ByVal objCM As ChassisMaster, ByVal objGroup As ProfileGroup, ByVal profileType As Short, ByVal objPanel As Panel, ByVal IsReadOnly As Boolean)
        Dim objRenderPanel As RenderingProfile = New RenderingProfile(IsReadOnly)

        If Not objCM Is Nothing Then
            If _formMode = EnumDNET.enumFormMode.Add Then
                objRenderPanel.GeneratePanel(objCM.ID, objPanel, objGroup, profileType, User)

            ElseIf _formMode = EnumDNET.enumFormMode.Edit Or _formMode = EnumDNET.enumFormMode.View Then
                Dim listProfile As ArrayList = objGroup.ProfileHeaderToGroups
                Dim objListProfileHeader As ArrayList = New ArrayList

                If Not listProfile Is Nothing Then
                    If listProfile.Count > 0 Then
                        For Each item As ProfileHeaderToGroup In listProfile
                            Dim objFacade As RevisionChassisMasterProfileFacade = New RevisionChassisMasterProfileFacade(System.Threading.Thread.CurrentPrincipal)
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "ChassisMaster.ID", MatchType.Exact, _objChassisMaster.ID))
                            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "EndCustomer.ID", MatchType.Exact, _objEndCustomer.ID))
                            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
                            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "ProfileHeader.ID", MatchType.Exact, item.ProfileHeader.ID))
                            Dim objListChassisMasterProfile As ArrayList = objFacade.Retrieve(criterias)
                            If objListChassisMasterProfile.Count > 0 Then
                                item.ProfileHeader.ProfileHeaderValue = CType(objListChassisMasterProfile(0), RevisionChassisMasterProfile).ProfileValue
                            End If
                            objListProfileHeader.Add(item.ProfileHeader)
                        Next

                        objRenderPanel.GeneratePanel(objListProfileHeader, objPanel, objGroup, profileType, User)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub LoadPage()
        _objDealer = CType(GetFromSession("DEALER"), Dealer)

        If Not _objChassisMaster Is Nothing Then
            If IsNothing(_objEndCustomer) Then
                _objEndCustomer = _objChassisMaster.EndCustomer
            End If
        Else
            Me.btnKembali.Attributes.Add("onclick", "DisclaimerAgreed = true;")
            Me.btnBindModel.Attributes.Add("onclick", "DisclaimerAgreed = true;")
            Me.btnCancel.Enabled = False
            Me.btnValidate.Enabled = False
            Me.btnSave.Enabled = False
            Me.btnCancelValidate.Enabled = False
            MessageBox.Show(SR.DataNotFound("Sasis"))
            Exit Sub
        End If

        AdditionalInformationToControl()
        ChassisMasterToControl()
        EndCustomerToControl()
        If Not IsOpenRevision Then
            DisableAll()
        End If

        ToggleButton()
        chkPrintProvinceOnInvoice.Enabled = False
        'If Not _objEndCustomer Is Nothing Then
        'Dim objCustomer As Customer = _objEndCustomer.Customer
        'If Not objCustomer Is Nothing Then
        If Not IsNothing(_objRevisionFaktur) Then
            If _objRevisionFaktur.RevisionStatus = "0" Then
                'If ((objCustomer.DeletionMark = "0" Or objCustomer.DeletionMark = 0) And _objRevisionFaktur.RevisionStatus = "0") Then
                chkPrintProvinceOnInvoice.Enabled = True
            End If
        End If
        'End If
        'End If


    End Sub


    Private Sub AdditionalInformationToControl()
        If IsOpenRevision Then
            ToggleAdditionalInformationControl(False)
            Dim objPaymentMethodFacade As PaymentMethodFacade = New PaymentMethodFacade(User)
            bindArrayListToDropDownList(Me.ddlPWPaymentMethod, objPaymentMethodFacade.RetrievePaymentMethodList)
            bindArrayListToDropDownList(Me.ddlPPPaymentMethod, objPaymentMethodFacade.RetrievePaymentMethodList)
        Else
            ToggleAdditionalInformationControl(True)
        End If
    End Sub

    Private Sub ToggleAdditionalInformationControl(ByVal value As Boolean)
        Me.lblPWAmount.Visible = value
        Me.lblPWBank.Visible = value
        Me.lblPWNomorGiro.Visible = value
        Me.lblPWPaymentMethod.Visible = value
        Me.lblPPAmount.Visible = value
        Me.lblPPBank.Visible = value
        Me.lblPPNomorGiro.Visible = value
        Me.lblPPPaymentMethod.Visible = value
        Me.lblSRNomorSurat.Visible = value
        Me.lblNoLKPP.Visible = value

        Me.ddlPWPaymentMethod.Visible = Not value
        Me.txtPWAmount.Visible = Not value
        Me.txtPWBank.Visible = Not value
        Me.txtPWNomorGiro.Visible = Not value
        Me.ddlPPPaymentMethod.Visible = Not value
        Me.txtPPAmount.Visible = Not value
        Me.txtPPBank.Visible = Not value
        Me.txtPPNomorGiro.Visible = Not value
        Me.txtSRNomorSurat.Visible = Not value
        Me.txtNoLKPP.Visible = Not value
        Me.lblLKPPNumber.Visible = Not value
    End Sub

    Private Sub bindArrayListToDropDownList(ByRef objDropDownList As DropDownList, ByVal objArrayList As ArrayList)
        objDropDownList.DataSource = objArrayList
        objDropDownList.DataTextField = "Description"
        objDropDownList.DataValueField = "ID"
        objDropDownList.DataBind()
        objDropDownList.Items.Insert(0, "Silahkan Pilih")
    End Sub


    Private Sub ChassisMasterToControl()
        If Not _objChassisMaster Is Nothing Then

            If Not IsNothing(_objEndCustomer) Then
                If Not IsNothing(_objEndCustomer.SPKFaktur) Then
                    If Not IsNothing(_objEndCustomer.SPKFaktur.SPKHeader) Then

                        If Not IsNothing(_objEndCustomer.SPKFaktur.SPKHeader.SalesmanHeader) Then
                            If Not IsNothing(_objEndCustomer.SPKFaktur.SPKHeader.SalesmanHeader.DealerBranch) Then
                                lblDealerBranch.Text = _objEndCustomer.SPKFaktur.SPKHeader.SalesmanHeader.DealerBranch.DealerBranchCode & " / " & _objChassisMaster.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader.DealerBranch.Term1
                            End If
                        End If
                    End If

                End If

            End If

            'LKPP
            If Not IsNothing(_objEndCustomer) AndAlso Not IsNothing(_objEndCustomer.Customer) AndAlso Not IsNothing(_objEndCustomer.Customer.MyCustomerRequest) AndAlso _
           _objEndCustomer.Customer.MyCustomerRequest.Status1 = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah And (_objChassisMaster.Category.CategoryCode.ToUpper() = "PC" OrElse _objChassisMaster.Category.CategoryCode.ToUpper() = "LCV") Then
                Me.txtLKPPConfirmation.Text = "-1"
            Else
                Me.txtLKPPConfirmation.Text = "1"
            End If
            'EndOfLKPP
            Me.lblChassisNumber.Text = _objChassisMaster.ChassisNumber
            Me.lblEngineNumber.Text = _objChassisMaster.EngineNumber
            Me.lblModelTypeColor.Text = _objChassisMaster.VechileColor.VechileType.VechileModel.VechileModelCode & " - " & _
                    _objChassisMaster.VechileColor.VechileType.Description & " " & _
                    _objChassisMaster.VechileColor.ColorIndName
            If Not IsNothing(_objRevisionFaktur) Then
                Me.lblStatus.Text = CType(_objRevisionFaktur.RevisionStatus, EnumDNET.enumFakturKendaraanRev).ToString()
            End If

            If Not _objEndCustomer Is Nothing Then
                Me.lblNomorFaktur.Text = _objEndCustomer.FakturNumber
            Else
                Me.lblNomorFaktur.Text = String.Empty
            End If
            Me.lblDODate.Text = Format(_objChassisMaster.DODate, "dd/MM/yyyy")
            Me.lblDiscountAmount.Text = String.Format("{0:#,###}", _objChassisMaster.DiscountAmount)
            Me.lblTOPayment.Text = _objChassisMaster.TermOfPayment.Description

            Me.lblDealerName.Text = _objChassisMaster.Dealer.DealerName
            Me.lblDealerCode.Text = _objChassisMaster.Dealer.DealerCode & " / " & _objChassisMaster.Dealer.SearchTerm1

            If _objChassisMaster.SONumber.Trim <> "" Then
                Dim objPOHeader As POHeader
                Dim objPOHeaderFac As POHeaderFacade = New POHeaderFacade(User)
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(POHeader), "SONumber", MatchType.Exact, _objChassisMaster.SONumber))
                Dim list As ArrayList
                list = objPOHeaderFac.Retrieve(crit)
                If (Not IsNothing(list) AndAlso list.Count > 0) Then
                    objPOHeader = CType(list.Item(0), POHeader)
                    If objPOHeader.ID > 0 Then
                        Dim objCont As ContractHeader
                        objCont = objPOHeader.ContractHeader
                        If (Not IsNothing(objCont)) AndAlso objCont.ID > 0 Then
                            Me.lblNamaPesananKhusus.Text = objCont.ProjectName
                        End If
                    End If
                End If
            End If

            ' Adding block vehicle code from web config
            If IsVehicleTypeBlocked(_objChassisMaster.VechileColor.VechileType.VechileTypeCode) Then
                MessageBox.Show("Pengajuan faktur untuk tipe " + _objChassisMaster.VechileColor.VechileType.VechileTypeCode _
                    + " sementara tidak dapat dilakukan.")
                'ViewState.Add("disabledSave", True)
                _sesshelper.SetSession("disabledSave", True)
            End If
        End If
    End Sub

    Private Function IsVehicleTypeBlocked(ByVal vehicleCode As String) As Boolean
        Dim Retval As Boolean = False

        Dim strBlock As String = KTB.DNet.Lib.WebConfig.GetValue("BLOCK_VEHICLE_CODE").ToString().Trim()
        If Not strBlock.Equals("") Then
            Dim sep As Char = ","
            Dim arrBlock As String() = strBlock.Split(sep)
            If arrBlock.Length > 0 Then
                For Each sBlock As String In arrBlock
                    If vehicleCode = sBlock.Trim() Then
                        Retval = True
                        Exit For
                    End If
                Next
            End If
        End If
        Return Retval
    End Function


    Private Sub EndCustomerToControl()
        If Not _objEndCustomer.Customer Is Nothing Then
            If (_objChassisMaster.FakturStatus = EnumChassisMaster.FakturStatus.Baru) Or (Not _objRevisionFaktur Is Nothing AndAlso _objRevisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Baru) Then
                pnlCustomerCode.Visible = True
                HideAllEndCustomerInputControl()
                ShowAllEndCustomerLabel()
                BindCustomer(_objEndCustomer.Customer)
                Me.icInvoiceDate.Value = _objEndCustomer.FakturDate
                Me.icInvoiceDate.Visible = True
                Me.lblValidatedBy.Text = ""
                Me.lblCustCode.Text = _objEndCustomer.Customer.Code
                Me.lblCustCode.Visible = True
                Dim objRefChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
                Dim ObjRefChassisMaster As ChassisMaster
                ObjRefChassisMaster = objRefChassisMasterFacade.Retrieve(_objEndCustomer.RefChassisNumberID)
                If Not ObjRefChassisMaster Is Nothing Then
                    Me.txtRefChassisNumber.Text = ObjRefChassisMaster.ChassisNumber
                End If

                If _objEndCustomer.Customer.PrintRegion = "X" Then
                    Me.chkPrintProvinceOnInvoice.Checked = True
                    chkPrintProvinceOnInvoice.Checked = True
                Else
                    Me.chkPrintProvinceOnInvoice.Checked = False
                    chkPrintProvinceOnInvoice.Checked = False
                End If

                Me.lblCreatedBy.Text = "<b>" & UserInfo.Convert(_objEndCustomer.SaveBy) & "</b> pada tanggal <b>" & _objEndCustomer.SaveTime.ToString("dd/MM/yyyy HH:mm:ss") & "</b>"
                Me.lblInvoiceDate.Visible = False
                Me.lblRefChassisNo.Visible = False
            Else
                HideAllEndCustomerInputControl()
                ShowAllEndCustomerLabel()
                BindCustomer(_objEndCustomer.Customer)
                pnlCustomerCode.Visible = True
                Me.lblCustCode.Visible = True
                Me.icInvoiceDate.Visible = False
                Me.lblCustCode.Text = _objEndCustomer.Customer.Code
                Me.lblInvoiceDate.Text = _objEndCustomer.FakturDate.ToString("dd/MM/yyyy")
                Dim objRefChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
                Dim ObjRefChassisMaster As ChassisMaster
                ObjRefChassisMaster = objRefChassisMasterFacade.Retrieve(_objEndCustomer.RefChassisNumberID)
                If Not ObjRefChassisMaster Is Nothing Then
                    Me.txtRefChassisNumber.Text = ObjRefChassisMaster.ChassisNumber
                End If
                Me.lblCreatedBy.Text = "<b>" & UserInfo.Convert(_objEndCustomer.SaveBy) & "</b> pada tanggal <b>" & _objEndCustomer.SaveTime.ToString("dd/MM/yyyy HH:mm:ss") & "</b>"
                Me.lblValidatedBy.Text = IIf(_objEndCustomer.ValidateBy.Trim = "", "", "<b>" & UserInfo.Convert(_objEndCustomer.ValidateBy) & "</b> pada tanggal <b>" & _objEndCustomer.ValidateTime.ToString("dd/MM/yyyy HH:mm:ss") & "</b>")
            End If
        Else
            Dim objRefChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
            Dim ObjRefChassisMaster As ChassisMaster
            ObjRefChassisMaster = objRefChassisMasterFacade.Retrieve(_objEndCustomer.RefChassisNumberID)
            If Not ObjRefChassisMaster Is Nothing Then
                Me.txtRefChassisNumber.Text = ObjRefChassisMaster.ChassisNumber
            End If
            chkPrintProvinceOnInvoice.Visible = False
            ShowAllEndCustomerLabel()
            HideAllEndCustomerInputControl()
            Me.icInvoiceDate.Value = _objEndCustomer.FakturDate
            Me.icInvoiceDate.Visible = True
            Me.lblValidatedBy.Text = ""
            Me.lblTitleValidateBy.Text = ""
        End If
        ' Additional Information
        BindEndCustomerAdditionalInformationToControl()
    End Sub

    Private Sub HideAllEndCustomerInputControl()
        Me.icInvoiceDate.Visible = False
        Me.txtRefChassisNumber.Visible = False
        Me.txtCustomerID.Visible = False
        lbCustomerID.Visible = False
        lnkReloadCust.Visible = False
    End Sub

    Private Sub ShowAllEndCustomerLabel()
        Me.lblName1.Visible = True
        Me.lblName2.Visible = True
        Me.lblName3.Visible = True
        Me.lblAddress.Visible = True
        Me.lblKel.Visible = True
        Me.lblKec.Visible = True
        Me.lblPOSCode.Visible = True
        Me.lblProvince.Visible = True
        Me.lblKTP.Visible = True
        Me.lblCity.Visible = True
        Me.lblEmail.Visible = True
        Me.lblPhone.Visible = True
        Me.lblCetak.Visible = True
        Me.lblCapCetak.Visible = True
        Me.lblInvoiceDate.Visible = True
        Me.lblRefChassisNo.Visible = True
    End Sub

    Private Sub BindCustomer(ByVal objCust As Customer)
        Me.lblCustCode.Text = objCust.Code
        Me.lblName1.Text = objCust.Name1
        Me.lblName2.Text = objCust.Name2
        Me.lblName3.Text = objCust.Name3
        Me.lblAddress.Text = objCust.Alamat
        Me.lblKel.Text = objCust.Kelurahan
        Me.lblKec.Text = objCust.Kecamatan
        Me.lblPOSCode.Text = objCust.PostalCode
        If objCust.PrintRegion = "X" Then
            lblCetak.Text = "Ya"
            chkPrintProvinceOnInvoice.Checked = True
        Else
            lblCetak.Text = "Tidak"
            chkPrintProvinceOnInvoice.Checked = False
        End If
        Me.lblProvince.Text = objCust.City.Province.ProvinceName
        If Not IsNothing(objCust.MyCustomerRequest) Then
            Dim oCRP As CustomerRequestProfile = objCust.MyCustomerRequest.GetCustomerRequestProfile("NOKTP")
            If Not IsNothing(oCRP) Then
                Me.lblKTP.Text = oCRP.ProfileValue.ToString
            Else
                Me.lblKTP.Text = String.Empty
            End If
        Else
            Me.lblKTP.Text = String.Empty
        End If


        Me.lblCity.Text = objCust.PreArea + " " + objCust.City.CityName
        Me.lblEmail.Text = objCust.Email
        Me.lblPhone.Text = objCust.PhoneNo

    End Sub

    Private Sub BindEndCustomerAdditionalInformationToControl()
        If _objEndCustomer.AreaViolationFlag = "X" Then
            Me.chkPelanggaranWilayah.Checked = True

            If IsOpenRevision Then
                If _objEndCustomer.AreaViolationPaymentMethodID = 0 Then
                    Me.ddlPWPaymentMethod.SelectedIndex = 0
                Else
                    Me.ddlPWPaymentMethod.SelectedValue = _objEndCustomer.AreaViolationPaymentMethodID.ToString
                End If

                Me.txtPWAmount.Text = FormatNumber(_objEndCustomer.AreaViolationyAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Me.txtPWBank.Text = _objEndCustomer.AreaViolationBankName
                Me.txtPWNomorGiro.Text = _objEndCustomer.AreaViolationGyroNumber
            Else
                Dim objPaymentMethod As PaymentMethod = New PaymentMethodFacade(User).Retrieve(_objEndCustomer.AreaViolationPaymentMethodID)
                If Not objPaymentMethod Is Nothing Then
                    Me.lblPWPaymentMethod.Text = objPaymentMethod.Description
                Else
                    Me.lblPWPaymentMethod.Text = String.Empty
                End If

                Me.lblPWAmount.Text = FormatNumber(_objEndCustomer.AreaViolationyAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Me.lblPWBank.Text = _objEndCustomer.AreaViolationBankName
                Me.lblPWNomorGiro.Text = _objEndCustomer.AreaViolationGyroNumber
            End If

        End If
        If _objEndCustomer.PenaltyFlag = "X" Then
            Me.chkPembayaranPenalti.Checked = True
            If IsOpenRevision Then
                If _objEndCustomer.PenaltyPaymentMethodID = 0 Then
                    Me.ddlPPPaymentMethod.SelectedIndex = 0
                Else
                    Me.ddlPPPaymentMethod.SelectedValue = _objEndCustomer.PenaltyPaymentMethodID.ToString
                End If

                Me.txtPPAmount.Text = FormatNumber(_objEndCustomer.PenaltyAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Me.txtPPBank.Text = _objEndCustomer.PenaltyBankName
                Me.txtPPNomorGiro.Text = _objEndCustomer.PenaltyGyroNumber
            Else
                Dim objPaymentMethod As PaymentMethod = New PaymentMethodFacade(User).Retrieve(_objEndCustomer.PenaltyPaymentMethodID)
                If Not objPaymentMethod Is Nothing Then
                    Me.lblPPPaymentMethod.Text = objPaymentMethod.Description
                Else
                    Me.lblPPPaymentMethod.Text = String.Empty
                End If

                Me.lblPPAmount.Text = FormatNumber(_objEndCustomer.PenaltyAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Me.lblPPBank.Text = _objEndCustomer.PenaltyBankName
                Me.lblPPNomorGiro.Text = _objEndCustomer.PenaltyGyroNumber
            End If

        End If
        If _objEndCustomer.ReferenceLetterFlag = "X" Then
            Me.chkSuratReferensi.Checked = True
            If IsOpenRevision Then
                Me.txtSRNomorSurat.Text = _objEndCustomer.ReferenceLetter
            Else
                Me.lblSRNomorSurat.Text = _objEndCustomer.ReferenceLetter
            End If

        End If

        'LKPP
        If Not _objChassisMaster.EndCustomer.LKPPHeader Is Nothing Then
            If _objChassisMaster.EndCustomer.LKPPHeader.ID > 0 Then
                Me.chkLKPP.Checked = True
                Me.lblNoLKPP.Text = _objChassisMaster.EndCustomer.LKPPHeader.ReferenceNumber
                Me.txtNoLKPP.Text = _objChassisMaster.EndCustomer.LKPPHeader.ReferenceNumber
            End If
        Else
            Me.txtNoLKPP.Text = String.Empty
        End If

        If Not _objEndCustomer.LKPPHeader Is Nothing Then
            If _objEndCustomer.LKPPHeader.ReferenceNumber.Length > 0 Then
                lblNoLKPP.Text = _objEndCustomer.LKPPHeader.ReferenceNumber
                txtNoLKPP.Text = _objEndCustomer.LKPPHeader.ReferenceNumber
            Else
                MessageBox.Show("No LKPP kosong")
            End If

        End If
        'LKPP

        'Fleet
        Dim critFleetFaktur As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critFleetFaktur.opAnd(New Criteria(GetType(FleetFaktur), "ChassisMaster.ChassisNumber", MatchType.Exact, lblChassisNumber.Text))
        Dim arrFleetFaktur As ArrayList = New FleetFakturFacade(User).Retrieve(critFleetFaktur)

        Dim objFleetFaktur As FleetFaktur
        If arrFleetFaktur.Count > 0 Then
            objFleetFaktur = arrFleetFaktur(0)
        End If

        If Not IsNothing(objFleetFaktur) Then
            Me.chkFleet.Checked = True
            Me.lblNoFleet.Text = objFleetFaktur.FleetRequest.NoRegRequest
            Me.txtNoFleetReq.Text = objFleetFaktur.FleetRequest.NoRegRequest
        End If
        'Fleet
    End Sub

    Private Sub BindViewOnlyEndCustomerAdditionalInformation()
        If _objEndCustomer.AreaViolationFlag = "X" Then
            Me.chkPelanggaranWilayah.Checked = True

            Dim objPaymentMethod As PaymentMethod = New PaymentMethodFacade(User).Retrieve(_objEndCustomer.AreaViolationPaymentMethodID)
            If Not objPaymentMethod Is Nothing Then
                Me.lblPWPaymentMethod.Text = objPaymentMethod.Description
            Else
                Me.lblPWPaymentMethod.Text = String.Empty
            End If

            Me.lblPWAmount.Text = FormatNumber(_objEndCustomer.AreaViolationyAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Me.lblPWBank.Text = _objEndCustomer.AreaViolationBankName
            Me.lblPWNomorGiro.Text = _objEndCustomer.AreaViolationGyroNumber
        End If
        If _objEndCustomer.PenaltyFlag = "X" Then
            Me.chkPembayaranPenalti.Checked = True

            Dim objPaymentMethod As PaymentMethod = New PaymentMethodFacade(User).Retrieve(_objEndCustomer.PenaltyPaymentMethodID)
            If Not objPaymentMethod Is Nothing Then
                Me.lblPPPaymentMethod.Text = objPaymentMethod.Description
            Else
                Me.lblPPPaymentMethod.Text = String.Empty
            End If

            Me.lblPPAmount.Text = FormatNumber(_objEndCustomer.PenaltyAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Me.lblPPBank.Text = _objEndCustomer.PenaltyBankName
            Me.lblPPNomorGiro.Text = _objEndCustomer.PenaltyGyroNumber

        End If
        If _objEndCustomer.ReferenceLetterFlag = "X" Then
            Me.chkSuratReferensi.Checked = True
            Me.lblSRNomorSurat.Text = _objEndCustomer.ReferenceLetter
        End If

        'LKPP
        If Not _objEndCustomer.LKPPHeader Is Nothing Then
            If _objEndCustomer.LKPPHeader.ID > 0 Then
                Me.chkLKPP.Checked = True
                Me.lblNoLKPP.Text = _objEndCustomer.LKPPHeader.ReferenceNumber
                Me.txtNoLKPP.Text = _objEndCustomer.LKPPHeader.ReferenceNumber
            End If
        Else
            Me.txtNoLKPP.Text = String.Empty
        End If

        If Not _objEndCustomer.LKPPHeader Is Nothing Then
            If _objEndCustomer.LKPPHeader.ReferenceNumber.Length > 0 Then
                txtNoLKPP.Text = _objEndCustomer.LKPPHeader.ReferenceNumber
                lblNoLKPP.Text = _objEndCustomer.LKPPHeader.ReferenceNumber
            Else
                MessageBox.Show("No LKPP kosong")
            End If

        End If
        'LKPP

        'Fleet
        Dim critFleetFaktur As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critFleetFaktur.opAnd(New Criteria(GetType(FleetFaktur), "ChassisMaster.ChassisNumber", MatchType.Exact, lblChassisNumber.Text))
        Dim arrFleetFaktur As ArrayList = New FleetFakturFacade(User).Retrieve(critFleetFaktur)

        Dim objFleetFaktur As FleetFaktur
        If arrFleetFaktur.Count > 0 Then
            objFleetFaktur = arrFleetFaktur(0)
        End If

        If Not IsNothing(objFleetFaktur) Then
            Me.chkFleet.Checked = True
            Me.lblNoFleet.Text = objFleetFaktur.FleetRequest.NoRegRequest
            Me.txtNoFleetReq.Text = objFleetFaktur.FleetRequest.NoRegRequest
        End If
        'Fleet
    End Sub

    Private Sub EnableAll()
        icInvoiceDate.Visible = True
        txtRefChassisNumber.Visible = True
        'chkPrintProvinceOnInvoice.Enabled = True
        chkPrintProvinceOnInvoice.Visible = True

        chkPelanggaranWilayah.Enabled = True
        ddlPWPaymentMethod.Enabled = True
        txtPWAmount.ReadOnly = False
        txtPWBank.ReadOnly = False
        txtPWNomorGiro.ReadOnly = False

        chkPembayaranPenalti.Enabled = True
        ddlPPPaymentMethod.Enabled = True
        txtPPAmount.ReadOnly = False
        txtPPBank.ReadOnly = False
        txtPPNomorGiro.ReadOnly = False

        chkSuratReferensi.Enabled = True
        txtSRNomorSurat.ReadOnly = False

        chkLKPP.Enabled = True
        chkFleet.Enabled = True
    End Sub

    Private Sub DisableAll()
        icInvoiceDate.Enabled = False

        chkPelanggaranWilayah.Enabled = False
        ddlPWPaymentMethod.Enabled = False
        txtPWAmount.ReadOnly = True
        txtPWBank.ReadOnly = True
        txtPWNomorGiro.ReadOnly = True

        chkPembayaranPenalti.Enabled = False
        ddlPPPaymentMethod.Enabled = False
        txtPPAmount.ReadOnly = True
        txtPPBank.ReadOnly = True
        txtPPNomorGiro.ReadOnly = True

        chkSuratReferensi.Enabled = False
        txtSRNomorSurat.ReadOnly = True

        'LKPP
        chkLKPP.Enabled = False
        txtNoLKPP.ReadOnly = True
        'LKPP

        'Fleet
        chkFleet.Enabled = False
        txtNoFleetReq.ReadOnly = True
        'Fleet
    End Sub


    Public Sub ToggleButton()
        If IsNothing(_objRevisionFaktur) Then
            If (CType(_sesshelper.GetSession("disabledSave"), Boolean)) Then
                Me.btnSave.Enabled = False
            Else
                Me.btnSave.Enabled = True
            End If
            Me.btnValidate.Enabled = False
            Me.btnCancelValidate.Enabled = False
        ElseIf _objRevisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Baru And _formMode > 0 Then
            If (CType(_sesshelper.GetSession("disabledSave"), Boolean)) Then
                Me.btnSave.Enabled = False
            Else
                Me.btnSave.Enabled = True
            End If
            If _objRevisionFaktur.ChassisMaster.EndCustomer Is Nothing Then
                Me.btnValidate.Enabled = False
            Else
                If (CType(_sesshelper.GetSession("disabledSave"), Boolean)) Then
                    Me.btnValidate.Enabled = False
                Else
                    Me.btnValidate.Enabled = True
                End If
            End If
            Me.btnCancelValidate.Enabled = False

        ElseIf _objRevisionFaktur.RevisionStatus = "1" And _formMode > 0 Then
            Me.btnSave.Enabled = False
            Me.btnValidate.Enabled = False
            Me.btnCancelValidate.Enabled = True
        Else
            Me.btnSave.Enabled = False
            Me.btnValidate.Enabled = False
            Me.btnCancelValidate.Enabled = False
        End If
    End Sub


    Private Sub GetRenderControl()
        Dim oPHFac As New ProfileHeaderFacade(User)
        Dim oPHJenis As ProfileHeader = oPHFac.Retrieve("CBU_JENISKEND")
        Dim oPHModel As ProfileHeader = oPHFac.Retrieve("CBU_MODELKEND")
        Dim oPHLeasing As ProfileHeader = oPHFac.Retrieve("CBU_LEASING")
        Dim oPHKaroseri As ProfileHeader = oPHFac.Retrieve("CBU_CARROSSERIE")
        Dim opHPurcStatus As ProfileHeader = oPHFac.Retrieve("CBU_PURCSTAT")

        Dim oPG As ProfileGroup
        Dim oPGFac As New ProfileGroupFacade(User)
        oPG = oPGFac.Retrieve("cust_prf_" & _objChassisMaster.Category.CategoryCode.ToLower)
        Dim oPHTGFac As New ProfileHeaderToGroupFacade(User)
        Dim cCMP As New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        cCMP.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, oPG.ID))
        Dim oPHTGList As ArrayList = oPHTGFac.RetrieveByCriteria(cCMP)

        For Each phtg As ProfileHeaderToGroup In oPHTGList
            If phtg.ProfileHeader.ID = oPHJenis.ID Then
                _ctlJenisKendaraan = "DDLIST" & phtg.ID.ToString & "_" & phtg.ProfileGroup.ID.ToString
                _sesshelper.SetSession("ddlJenis", _ctlJenisKendaraan)
            ElseIf phtg.ProfileHeader.ID = oPHModel.ID Then
                _ctlModelKendaraan = "DDLIST" & phtg.ID.ToString & "_" & phtg.ProfileGroup.ID.ToString
                _sesshelper.SetSession("ddlModel", _ctlModelKendaraan)
            ElseIf phtg.ProfileHeader.ID = oPHLeasing.ID Then
                _ctlLeasing = "DDLIST" & phtg.ID.ToString & "_" & phtg.ProfileGroup.ID.ToString
                _sesshelper.SetSession("ddlLeasing", _ctlLeasing)
            ElseIf phtg.ProfileHeader.ID = oPHKaroseri.ID Then
                _ctlKaroseri = "DDLIST" & phtg.ID.ToString & "_" & phtg.ProfileGroup.ID.ToString
                _sesshelper.SetSession("ddlKaroseri", _ctlKaroseri)
            ElseIf phtg.ProfileHeader.ID = opHPurcStatus.ID Then
                _ctlPurcStat = "DDLIST" & phtg.ID.ToString & "_" & phtg.ProfileGroup.ID.ToString
                _sesshelper.SetSession("ddlPurcStat", _ctlPurcStat)
            End If
        Next
    End Sub


    Private Sub DisableAllRenderedControl()
        Dim oPG As ProfileGroup
        Dim oPGFac As New ProfileGroupFacade(User)
        oPG = oPGFac.Retrieve("cust_prf_" & _objChassisMaster.Category.CategoryCode.ToLower)
        Dim oPHTGFac As New ProfileHeaderToGroupFacade(User)
        Dim cCMP As New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        cCMP.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, oPG.ID))
        Dim oPHTGList As ArrayList = oPHTGFac.RetrieveByCriteria(cCMP)

        For Each phtg As ProfileHeaderToGroup In oPHTGList
            Dim ddlControl As DropDownList = pnlInformasion.FindControl("DDLIST" & phtg.ID.ToString & "_" & phtg.ProfileGroup.ID.ToString)
            ddlControl.Enabled = False
        Next
    End Sub


    Private Sub ManageDDLControl()
        Dim vKind As VehicleKind

        If _formMode = EnumDNET.enumFormMode.Add Then
            vKind = _objChassisMaster.VehicleKind
        Else
            Dim oPHFac As New ProfileHeaderFacade(User)
            Dim oModel As ProfileHeader = oPHFac.Retrieve("CBU_MODELKEND")
            Dim val As String = GetProfileValue(oModel)

            Dim oJenis As ProfileHeader = oPHFac.Retrieve("CBU_JENISKEND")
            Dim valJenis As String = GetProfileValue(oJenis)

            Dim oVKFac As VehicleKindFacade = New VehicleKindFacade(User)
            If Not String.IsNullorEmpty(val) Then
                Dim intVal As Integer
                Dim intValJenis As Integer
                If Integer.TryParse(val, intVal) Then
                    vKind = oVKFac.Retrieve(intVal)
                Else
                    If Integer.TryParse(valJenis, intValJenis) Then
                        vKind = oVKFac.RetrieveByCode(val, intValJenis)
                    Else
                        vKind = oVKFac.RetrieveByCode(val, valJenis)
                    End If
                End If
            Else
                ' if no profile then get kind from chassismaster
                vKind = _objChassisMaster.VehicleKind
            End If
        End If

        Dim ddlJenis As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlJenis"), String))
        Dim ddlModel As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlModel"), String))

        Dim ddlLeasing As DropDownList
        Dim ddlKaroseri As DropDownList
        If _objChassisMaster.Category.CategoryCode = "PC" Or _objChassisMaster.Category.CategoryCode = "LCV" Then 'antisipasi data lama
            ddlLeasing = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlLeasing"), String))
            ddlKaroseri = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlKaroseri"), String))
        End If

        Dim oVKGFac As VehicleKindGroupFacade = New VehicleKindGroupFacade(User)

        Dim cVKG As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKindGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim strGroup As String = ""
        If _objChassisMaster.VechileColor.VechileType.IsVehicleKind1 = 1 Then
            strGroup &= "2,"
        End If
        If _objChassisMaster.VechileColor.VechileType.IsVehicleKind2 = 1 Then
            strGroup &= "3,"
        End If
        If _objChassisMaster.VechileColor.VechileType.IsVehicleKind3 = 1 Then
            strGroup &= "4,"
        End If
        If _objChassisMaster.VechileColor.VechileType.IsVehicleKind4 = 1 Then
            strGroup &= "5,"
        End If

        Try
            If Not IsNothing(vKind) AndAlso vKind.ID = 1 Then
                strGroup &= "1,"
            End If
        Catch ex As Exception

        End Try


        strGroup = Left(strGroup, strGroup.Length - 1)
        cVKG.opAnd(New Criteria(GetType(VehicleKindGroup), "ID", MatchType.InSet, "(" & strGroup & ")"))

        Dim aVKG As ArrayList
        Dim oVKofCM As VehicleKind
        If Not IsNothing(vKind) AndAlso vKind.ID = 1 Then
            aVKG = oVKGFac.RetrieveList()
        Else
            aVKG = oVKGFac.Retrieve(cVKG)
        End If

        If Not IsNothing(ddlJenis) Then
            With ddlJenis.Items
                .Clear()
                For Each oVKG As VehicleKindGroup In aVKG
                    .Add(New ListItem(oVKG.Description, oVKG.ID))
                Next
            End With
        End If
        If Not IsNothing(vKind) AndAlso vKind.ID > 0 Then
            If Not IsNothing(ddlJenis) Then
                Dim isTypeValid As Boolean = False
                For i As Integer = 0 To ddlJenis.Items.Count - 1
                    If ddlJenis.Items(i).Value = vKind.VehicleKindGroup.ID Then
                        isTypeValid = True
                        Exit For
                    End If
                Next
                If isTypeValid = True Then
                    ddlJenis.SelectedValue = vKind.VehicleKindGroup.ID
                    Me.txtNewKindID.Text = ddlJenis.SelectedValue
                Else
                    MessageBox.Show("Jenis dan Model yang ada pilih sebelumnya tidak valid. Silahkan pilih Jenis dan Model yang sesuai")
                End If
            End If
            BindDDLModel(True)
        End If
        If Not IsNothing(ddlJenis) And Not IsNothing(ddlModel) Then
            ddlJenis.Attributes.Add("OnChange", "RebindModel()")
            ddlModel.Attributes.Add("OnChange", "ChoosenModel()")
        End If

        If Not IsNothing(ddlLeasing) Then
            If _formMode > 0 Then
                Dim ddlPayment As DropDownList = GetDDLProfile("CBU_WAYPAID1")
                ddlPayment.Attributes.Add("OnChange", "DisableLeasing()")

                If ddlPayment.SelectedItem.Text.Trim = "TUNAI" Then
                    ddlLeasing.SelectedIndex = 0
                    ddlLeasing.Enabled = False
                ElseIf ddlPayment.SelectedItem.Text = "KREDIT" Then
                    If ddlPayment.Enabled Then 'if ddlPayment disabled then ddlLeasing keep disabled also
                        ddlLeasing.Enabled = True
                    End If
                End If
            End If
        End If

        '-- Start Invoice Revision, 02042018
        If Not IsNothing(ddlLeasing) Then
            Dim oPHFac As New ProfileHeaderFacade(User)
            Dim oPHLeasing As ProfileHeader = oPHFac.Retrieve("CBU_LEASING")
            Dim oLeasingFac As LeasingFacade = New LeasingFacade(User)
            Dim cLeasing As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Leasing), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cLeasing.opAnd(New Criteria(GetType(Leasing), "Status", MatchType.Exact, CType(1, Byte)))

            Dim aLeasing As ArrayList
            aLeasing = oLeasingFac.Retrieve(cLeasing)

            Dim strProfileValue As String = GetProfileValue(oPHLeasing)
            Dim intProfileID As Integer = 0

            With ddlLeasing.Items
                .Clear()
                For Each oLeasing As Leasing In aLeasing
                    .Add(New ListItem(oLeasing.LeasingName, oLeasing.LeasingCode))
                Next
            End With
            Dim listSilahkanPilih As ListItem = New ListItem("Silahkan Pilih", "")
            ddlLeasing.Items.Insert(0, listSilahkanPilih)

            Try
                ddlLeasing.SelectedValue = strProfileValue
            Catch ex As Exception

            End Try

        End If

        If Not IsNothing(ddlKaroseri) Then
            Dim oPHFac As New ProfileHeaderFacade(User)
            Dim oPHKaroseri As ProfileHeader = oPHFac.Retrieve("CBU_CARROSSERIE")
            Dim oKaroseriFac As KaroseriFacade = New KaroseriFacade(User)
            Dim cKaroseri As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Karoseri), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cKaroseri.opAnd(New Criteria(GetType(Karoseri), "Status", MatchType.Exact, CType(1, Byte)))

            Dim aKaroseri As ArrayList
            aKaroseri = oKaroseriFac.Retrieve(cKaroseri)

            Dim strProfileValue As String = GetProfileValue(oPHKaroseri)
            Dim intProfileID As Integer = 0

            With ddlKaroseri.Items
                .Clear()
                For Each oKaroseri As Karoseri In aKaroseri
                    .Add(New ListItem(oKaroseri.Name, oKaroseri.Code))
                    'If oKaroseri.ID.ToString() = strProfileValue Then intProfileID = oKaroseri.ID
                Next
            End With
            Dim listSilahkanPilih As ListItem = New ListItem("Silahkan Pilih", "")
            ddlKaroseri.Items.Insert(0, listSilahkanPilih)

            Try
                ddlKaroseri.SelectedValue = strProfileValue
            Catch ex As Exception

            End Try

        End If

        Dim ddlPurcStat As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlPurcStat"), String))

        If Not IsNothing(ddlKaroseri) AndAlso _formMode = EnumDNET.enumFormMode.View Then
            ddlPurcStat.Enabled = False
        End If
    End Sub

    Private Sub BindDDLModel(Optional ByVal IsNotFromClient As Boolean = False)
        Dim ddlJenis As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlJenis"), String))
        Dim ddlModel As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlModel"), String))
        Dim oVKFac As New VehicleKindFacade(User)
        Dim aVK As ArrayList

        If Not IsNothing(ddlModel) Then
            Dim vKind As VehicleKind

            If _formMode = EnumDNET.enumFormMode.Add Then
                vKind = _objChassisMaster.VehicleKind
            Else
                Dim oPHFac As New ProfileHeaderFacade(User)
                Dim oModel As ProfileHeader = oPHFac.Retrieve("CBU_MODELKEND")
                Dim val As String = GetProfileValue(oModel)

                Dim oJenis As ProfileHeader = oPHFac.Retrieve("CBU_JENISKEND")
                Dim valJenis As String = GetProfileValue(oJenis)

                If Not String.IsNullorEmpty(val) Then
                    Dim intVal As Integer
                    Dim intValJenis As Integer
                    If Integer.TryParse(val, intVal) Then
                        vKind = oVKFac.Retrieve(intVal)
                    Else
                        If Integer.TryParse(valJenis, intValJenis) Then
                            vKind = oVKFac.RetrieveByCode(val, intValJenis)
                        Else
                            vKind = oVKFac.RetrieveByCode(val, valJenis)
                        End If
                    End If
                End If
            End If

            With ddlModel.Items
                .Clear()
                If CType(ddlJenis.SelectedValue, Integer) > 0 Then
                    Dim strGroup As String = ""
                    If _objChassisMaster.VechileColor.VechileType.IsVehicleKind1 = 1 Then
                        strGroup &= "2,"
                    End If
                    If _objChassisMaster.VechileColor.VechileType.IsVehicleKind2 = 1 Then
                        strGroup &= "3,"
                    End If
                    If _objChassisMaster.VechileColor.VechileType.IsVehicleKind3 = 1 Then
                        strGroup &= "4,"
                    End If
                    If _objChassisMaster.VechileColor.VechileType.IsVehicleKind4 = 1 Then
                        strGroup &= "5,"
                    End If

                    If Not IsNothing(vKind) AndAlso vKind.ID = 1 Then
                        strGroup &= "1,"
                    End If

                    strGroup = Left(strGroup, strGroup.Length - 1)

                    Dim cVK As New CriteriaComposite(New Criteria(GetType(VehicleKind), "VehicleKindGroup.ID", MatchType.InSet, "(" & strGroup & ")"))


                    aVK = oVKFac.Retrieve(cVK)

                    For Each oVK As VehicleKind In aVK
                        .Add(New ListItem(oVK.Description, oVK.ID))
                    Next

                    If IsNotFromClient AndAlso Not IsNothing(_objChassisMaster) AndAlso Not IsNothing(vKind) AndAlso vKind.ID > 0 Then
                        Dim isTypeValid As Boolean = False
                        For i As Integer = 0 To ddlModel.Items.Count - 1
                            If ddlModel.Items(i).Value = vKind.ID Then
                                isTypeValid = True
                                Exit For
                            End If
                        Next
                        If isTypeValid = True Then
                            ddlModel.SelectedValue = vKind.ID.ToString
                        Else
                            MessageBox.Show("Jenis dan Model yang ada pilih sebelumnya tidak valid. Silahkan pilih Jenis dan Model yang sesuai")
                        End If

                    End If
                    Me.txtNewModelID.Text = ddlModel.SelectedValue
                End If
            End With

        End If

    End Sub

    Private Function GetDDLProfile(ByVal strPHCode As String) As DropDownList
        Dim oPHFac As New ProfileHeaderFacade(User)
        Dim oPHPayment As ProfileHeader = oPHFac.Retrieve("CBU_WAYPAID1")

        Dim oPG As ProfileGroup
        Dim oPGFac As New ProfileGroupFacade(User)
        oPG = oPGFac.Retrieve("cust_prf_" & _objChassisMaster.Category.CategoryCode.ToLower)
        Dim oPHTGFac As New ProfileHeaderToGroupFacade(User)
        Dim cCMP As New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        cCMP.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, oPG.ID))
        Dim oPHTGList As ArrayList = oPHTGFac.RetrieveByCriteria(cCMP)

        Dim _ctlPayment As String = ""

        For Each phtg As ProfileHeaderToGroup In oPHTGList
            If phtg.ProfileHeader.ID = oPHPayment.ID Then
                _ctlPayment = "DDLIST" & phtg.ID.ToString & "_" & phtg.ProfileGroup.ID.ToString
            End If
        Next

        Return pnlInformasion.FindControl(_ctlPayment)
    End Function

    Private Function GetProfileValue(ByVal oPH As ProfileHeader) As String
        Dim objGroup As ProfileGroup
        Dim strProfileValue As String = ""

        Select Case _objChassisMaster.Category.CategoryCode
            Case "PC"
                objGroup = New ProfileGroupFacade(User).Retrieve(PCGroup)
            Case "CV"
                objGroup = New ProfileGroupFacade(User).Retrieve(CVGroup)
            Case "LCV"
                objGroup = New ProfileGroupFacade(User).Retrieve(LCVGroup)
        End Select

        If _formMode = EnumDNET.enumFormMode.Add Then
            Dim objFacade As ChassisMasterProfileFacade = New ChassisMasterProfileFacade(System.Threading.Thread.CurrentPrincipal)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ChassisMaster.ID", MatchType.Exact, _objChassisMaster.ID))
            criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
            criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ProfileHeader.ID", MatchType.Exact, oPH.ID))
            Dim objListChassisMasterProfile As ArrayList = objFacade.Retrieve(criterias)
            If objListChassisMasterProfile.Count > 0 Then
                strProfileValue = CType(objListChassisMasterProfile(0), ChassisMasterProfile).ProfileValue
            End If
        Else
            Dim objFacade As RevisionChassisMasterProfileFacade = New RevisionChassisMasterProfileFacade(System.Threading.Thread.CurrentPrincipal)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "ChassisMaster.ID", MatchType.Exact, _objChassisMaster.ID))
            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "EndCustomer.ID", MatchType.Exact, _objRevisionFaktur.EndCustomer.ID))
            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "ProfileHeader.ID", MatchType.Exact, oPH.ID))
            Dim objListRevisionChassisMasterProfile As ArrayList = objFacade.Retrieve(criterias)
            If objListRevisionChassisMasterProfile.Count > 0 Then
                strProfileValue = CType(objListRevisionChassisMasterProfile(0), RevisionChassisMasterProfile).ProfileValue
            End If
        End If
        Return strProfileValue
    End Function


    Public Function GetRevisionType(ByVal intID As Integer) As RevisionType
        If intID > 0 Then
            Dim objRevisionType As RevisionType = New RevisionTypeFacade(User).Retrieve(intID)
            Return objRevisionType
        Else
            Return Nothing
        End If

    End Function


    Private Sub OpenRevisionSameTDPForm()
        If Not _objEndCustomer.Customer Is Nothing Then

            If (_objChassisMaster.FakturStatus = EnumChassisMaster.FakturStatus.Selesai Or (_objChassisMaster.FakturStatus = EnumChassisMaster.FakturStatus.Proses And _objChassisMaster.EndCustomer.IsTemporary = 1)) And _formMode > 0 And
               ((_objChassisMaster.EndCustomer.RevisionFaktur Is Nothing) Or
                (Not _objRevisionFaktur Is Nothing AndAlso _objRevisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Baru) Or
                (Not _objChassisMaster.EndCustomer.RevisionFaktur Is Nothing AndAlso _objChassisMaster.EndCustomer.RevisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Selesai)) Then
                'EnableAllProfileControl(True)
                EnableAll()
                pnlCustomerCode.Visible = True
                HideAllEndCustomerInputControl()
                ShowAllEndCustomerLabel()
                BindCustomer(_objEndCustomer.Customer)
                If _formMode = EnumDNET.enumFormMode.Add Then
                    Me.icInvoiceDate.Value = GetDefaultInvoiceDate()
                Else
                    Me.icInvoiceDate.Value = _objEndCustomer.FakturDate
                End If
                Me.icInvoiceDate.Visible = True
                Me.icInvoiceDate.Enabled = True
                Me.lblValidatedBy.Text = IIf(_objEndCustomer.ValidateBy.Trim = "", "", "<b>" & UserInfo.Convert(_objEndCustomer.ValidateBy) & "</b> pada tanggal <b>" & _objEndCustomer.ValidateTime.ToString("dd/MM/yyyy HH:mm:ss") & "</b>")

                Me.lblCustCode.Text = _objEndCustomer.Customer.Code
                Me.lblCustCode.Visible = False
                txtCustomerID.Text = _objEndCustomer.Customer.Code
                txtCustomerID.Visible = True
                lbCustomerID.Visible = True
                lnkReloadCust.Visible = True

                Dim objRefChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
                Dim ObjRefChassisMaster As ChassisMaster
                ObjRefChassisMaster = objRefChassisMasterFacade.Retrieve(_objEndCustomer.RefChassisNumberID)
                If Not ObjRefChassisMaster Is Nothing Then
                    Me.txtRefChassisNumber.Text = ObjRefChassisMaster.ChassisNumber
                End If

                Me.lblCreatedBy.Text = "<b>" & UserInfo.Convert(_objEndCustomer.SaveBy) & "</b> pada tanggal <b>" & _objEndCustomer.SaveTime.ToString("dd/MM/yyyy HH:mm:ss") & "</b>"
                Me.lblInvoiceDate.Visible = False
                Me.lblRefChassisNo.Visible = True

                ' set btn cancel
                If _formMode = EnumDNET.enumFormMode.Edit Then
                    Me.btnCancel.Visible = True
                End If
            Else
                DisableAll()
                DisableAllRenderedControl()
                HideAllEndCustomerInputControl()
                ShowAllEndCustomerLabel()
                BindCustomer(_objEndCustomer.Customer)
                pnlCustomerCode.Visible = True
                Me.lblCustCode.Visible = True
                Me.icInvoiceDate.Visible = False
                Me.lblCustCode.Text = _objEndCustomer.Customer.Code
                Me.lblInvoiceDate.Text = _objEndCustomer.FakturDate.ToString("dd/MM/yyyy")
                Dim objRefChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
                Dim ObjRefChassisMaster As ChassisMaster
                ObjRefChassisMaster = objRefChassisMasterFacade.Retrieve(_objEndCustomer.RefChassisNumberID)
                If Not ObjRefChassisMaster Is Nothing Then
                    Me.lblRefChassisNo.Text = ObjRefChassisMaster.ChassisNumber
                End If
                Me.lblCreatedBy.Text = "<b>" & UserInfo.Convert(_objEndCustomer.SaveBy) & "</b> pada tanggal <b>" & _objEndCustomer.SaveTime.ToString("dd/MM/yyyy HH:mm:ss") & "</b>"
                Me.lblValidatedBy.Text = IIf(_objEndCustomer.ValidateBy.Trim = "", "", "<b>" & UserInfo.Convert(_objEndCustomer.ValidateBy) & "</b> pada tanggal <b>" & _objEndCustomer.ValidateTime.ToString("dd/MM/yyyy HH:mm:ss") & "</b>")
            End If
        Else
            DisableAll()
            DisableAllRenderedControl()
            Dim objRefChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
            Dim ObjRefChassisMaster As ChassisMaster
            ObjRefChassisMaster = objRefChassisMasterFacade.Retrieve(_objEndCustomer.RefChassisNumberID)
            If Not ObjRefChassisMaster Is Nothing Then
                Me.txtRefChassisNumber.Text = ObjRefChassisMaster.ChassisNumber
            End If
            chkPrintProvinceOnInvoice.Visible = False
            ShowAllEndCustomerLabel()
            HideAllEndCustomerInputControl()
            Me.icInvoiceDate.Value = _objEndCustomer.FakturDate
            Me.icInvoiceDate.Visible = True
            'Me.txtRefChassisNumber.Visible = True
            Me.lblValidatedBy.Text = IIf(_objEndCustomer.ValidateBy.Trim = "", "", "<b>" & UserInfo.Convert(_objEndCustomer.ValidateBy) & "</b> pada tanggal <b>" & _objEndCustomer.ValidateTime.ToString("dd/MM/yyyy HH:mm:ss") & "</b>")
            Me.lblTitleValidateBy.Text = ""
        End If
        ' Additional Information
        BindEndCustomerAdditionalInformationToControl()
    End Sub

    Private Sub EnableAllProfileControl(ByVal bolEnable As Boolean)

        Dim objGroup As ProfileGroup
        Select Case _objChassisMaster.Category.CategoryCode
            Case "PC"
                objGroup = New ProfileGroupFacade(User).Retrieve("cust_prf_pc")
            Case "CV"
                objGroup = New ProfileGroupFacade(User).Retrieve("cust_prf_cv")
            Case "LCV"
                objGroup = New ProfileGroupFacade(User).Retrieve("cust_prf_lcv")
        End Select

        Dim hstPassing As Hashtable = CType(HttpContext.Current.Session.Item("PROFILE" & "_" & objGroup.ID), Hashtable)
        If Not hstPassing Is Nothing Then
            If hstPassing.Count > 0 Then
                For Each key As String In hstPassing.Keys
                    Select Case key.Split("-")(0)
                        Case EnumControlType.ControlType.Text
                            Dim ctr As TextBox = CType(Me.FindControl(key.Split("-")(1)), TextBox)
                            ctr.Enabled = bolEnable
                            ctr.Text = ""
                        Case EnumControlType.ControlType.Calendar
                            Dim ctr As KTB.DNet.WebCC.IntiCalendar = CType(Me.FindControl(key.Split("-")(1)), KTB.DNet.WebCC.IntiCalendar)
                            ctr.Enabled = bolEnable
                            ctr.Value = Date.Today
                        Case EnumControlType.ControlType.CheckListBox
                            Dim ctr As CheckBoxList = CType(Me.FindControl(key.Split("-")(1)), CheckBoxList)
                            ctr.Enabled = bolEnable
                            ctr.ClearSelection()
                        Case EnumControlType.ControlType.List
                            Dim ctr As DropDownList = CType(Me.FindControl(key.Split("-")(1)), DropDownList)
                            ctr.Enabled = bolEnable
                            ctr.SelectedIndex = 0
                    End Select
                Next
            End If
        End If
    End Sub


    Private Sub OpenRevisionFakturDateForm()

        GetRenderControl()
        Dim objRefChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
        Dim ObjRefChassisMaster As ChassisMaster
        ObjRefChassisMaster = objRefChassisMasterFacade.Retrieve(_objEndCustomer.RefChassisNumberID)
        If Not ObjRefChassisMaster Is Nothing Then
            Me.lblRefChassisNo.Text = ObjRefChassisMaster.ChassisNumber
        End If
        HideAllEndCustomerInputControl()
        ShowAllEndCustomerLabel()
        DisableAll()

        If Not _objEndCustomer.Customer Is Nothing Then

            If (_objChassisMaster.FakturStatus = EnumChassisMaster.FakturStatus.Selesai Or (_objChassisMaster.FakturStatus = EnumChassisMaster.FakturStatus.Proses And _objChassisMaster.EndCustomer.IsTemporary = 1)) And _formMode > 0 And
               ((_objChassisMaster.EndCustomer.RevisionFaktur Is Nothing) Or
                (Not _objRevisionFaktur Is Nothing AndAlso _objRevisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Baru) Or
                (Not _objChassisMaster.EndCustomer.RevisionFaktur Is Nothing AndAlso _objChassisMaster.EndCustomer.RevisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Selesai)) Then

                If _formMode = EnumDNET.enumFormMode.Add Then
                    Me.icInvoiceDate.Value = GetDefaultInvoiceDate()
                Else
                    Me.icInvoiceDate.Value = _objEndCustomer.FakturDate
                End If

                Me.icInvoiceDate.Visible = True
                Me.icInvoiceDate.Enabled = True
                Me.lblInvoiceDate.Visible = False

                ' set btn cancel
                If _formMode = EnumDNET.enumFormMode.Edit Then
                    Me.btnCancel.Visible = True
                End If
            Else
                Me.lblInvoiceDate.Text = _objEndCustomer.FakturDate.ToString("dd/MM/yyyy")
            End If

            BindCustomer(_objEndCustomer.Customer)

            Me.lblCustCode.Text = _objEndCustomer.Customer.Code
            Me.lblCreatedBy.Text = "<b>" & UserInfo.Convert(_objEndCustomer.SaveBy) & "</b> pada tanggal <b>" & _objEndCustomer.SaveTime.ToString("dd/MM/yyyy HH:mm:ss") & "</b>"
            Me.lblValidatedBy.Text = IIf(_objEndCustomer.ValidateBy.Trim = "", "", "<b>" & UserInfo.Convert(_objEndCustomer.ValidateBy) & "</b> pada tanggal <b>" & _objEndCustomer.ValidateTime.ToString("dd/MM/yyyy HH:mm:ss") & "</b>")

            pnlCustomerCode.Visible = True
            Me.lblCustCode.Visible = True

        Else
            chkPrintProvinceOnInvoice.Visible = False
            Me.lblInvoiceDate.Text = _objEndCustomer.FakturDate.ToString("dd/MM/yyyy")
            Me.lblValidatedBy.Text = IIf(_objEndCustomer.ValidateBy.Trim = "", "", "<b>" & UserInfo.Convert(_objEndCustomer.ValidateBy) & "</b> pada tanggal <b>" & _objEndCustomer.ValidateTime.ToString("dd/MM/yyyy HH:mm:ss") & "</b>")
            Me.lblTitleValidateBy.Text = ""
        End If

        DisableAllRenderedControl()
        ' Additional Information
        BindViewOnlyEndCustomerAdditionalInformation()
        ToggleAdditionalInformationControl(True)
    End Sub


    Private Sub OpenRevisionChassisDataForm()
        Dim objRefChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
        Dim ObjRefChassisMaster As ChassisMaster
        ObjRefChassisMaster = objRefChassisMasterFacade.Retrieve(_objEndCustomer.RefChassisNumberID)
        If Not ObjRefChassisMaster Is Nothing Then
            Me.lblRefChassisNo.Text = ObjRefChassisMaster.ChassisNumber
        End If
        HideAllEndCustomerInputControl()
        ShowAllEndCustomerLabel()
        DisableAll()
        If _formMode = EnumDNET.enumFormMode.Add Then
            Me.icInvoiceDate.Value = GetDefaultInvoiceDate()
        Else
            Me.icInvoiceDate.Value = _objEndCustomer.FakturDate
        End If
        Me.icInvoiceDate.Visible = True
        Me.icInvoiceDate.Enabled = True
        Me.lblInvoiceDate.Visible = False

        If Not _objEndCustomer.Customer Is Nothing Then

            If Not IsOpenRevision Or
                _formMode = EnumDNET.enumFormMode.View Then

                DisableAllRenderedControl()
                Me.lblInvoiceDate.Visible = True
                Me.icInvoiceDate.Visible = False
                Me.lblInvoiceDate.Text = _objEndCustomer.FakturDate.ToString("dd/MM/yyyy")
            End If

            BindCustomer(_objEndCustomer.Customer)

            Me.lblCustCode.Text = _objEndCustomer.Customer.Code
            Me.lblCreatedBy.Text = "<b>" & UserInfo.Convert(_objEndCustomer.SaveBy) & "</b> pada tanggal <b>" & _objEndCustomer.SaveTime.ToString("dd/MM/yyyy HH:mm:ss") & "</b>"
            Me.lblValidatedBy.Text = IIf(_objEndCustomer.ValidateBy.Trim = "", "", "<b>" & UserInfo.Convert(_objEndCustomer.ValidateBy) & "</b> pada tanggal <b>" & _objEndCustomer.ValidateTime.ToString("dd/MM/yyyy HH:mm:ss") & "</b>")

            pnlCustomerCode.Visible = True
            Me.lblCustCode.Visible = True

        Else
            chkPrintProvinceOnInvoice.Visible = False
            Me.lblValidatedBy.Text = IIf(_objEndCustomer.ValidateBy.Trim = "", "", "<b>" & UserInfo.Convert(_objEndCustomer.ValidateBy) & "</b> pada tanggal <b>" & _objEndCustomer.ValidateTime.ToString("dd/MM/yyyy HH:mm:ss") & "</b>")
            Me.lblTitleValidateBy.Text = ""
        End If

        ' Additional Information
        BindViewOnlyEndCustomerAdditionalInformation()
        ToggleAdditionalInformationControl(True)

        If IsOpenRevision Then
            txtNoLKPP.Enabled = True
            txtNoLKPP.Visible = True
            chkLKPP.Enabled = True
            lblLKPPNumber.Visible = True

            ' set btn cancel
            If _formMode = EnumDNET.enumFormMode.Edit Then
                Me.btnCancel.Visible = True
                Me.lblNoLKPP.Visible = False
            End If
        End If
    End Sub


    Private Sub ControlToEndCustomer()
        Dim endCustomer As EndCustomer
        Dim oldEndCustomerID As Integer
        oldEndCustomerID = _objChassisMaster.EndCustomer.ID
        _sesshelper.SetSession("oldEndCustomerID", oldEndCustomerID)
        endCustomer = _objEndCustomer

        Dim _objNewCustomer As Customer = CType(GetFromSession("newCustomer"), Customer)
        If Not _objNewCustomer Is Nothing Then
            endCustomer.Customer = _objNewCustomer
            endCustomer.Name1 = _objNewCustomer.Name1
        End If

        If Not endCustomer.Customer Is Nothing Then

            If Me.chkPrintProvinceOnInvoice.Checked Then
                endCustomer.Customer.PrintRegion = "X"
            Else
                endCustomer.Customer.PrintRegion = ""
            End If
        Else
            If (Session("Customer") Is Nothing) Then
                _sErrorMessage = "Data customer belum ada"
                Exit Sub
            Else
                endCustomer.Customer = CType(Session("Customer"), Customer)
            End If
        End If

        endCustomer.FakturDate = Me.icInvoiceDate.Value


        ' Additional Information
        If Me.chkPelanggaranWilayah.Checked Then
            endCustomer.AreaViolationFlag = "X"
            endCustomer.AreaViolationBankName = Me.txtPWBank.Text.ToUpper
            endCustomer.AreaViolationGyroNumber = Me.txtPWNomorGiro.Text
            If Me.ddlPWPaymentMethod.SelectedIndex = 0 Then
                _sErrorMessage = _sErrorMessage & "Belum memilih Cara Pembayaran"
                endCustomer.AreaViolationPaymentMethodID = CType(Me.ddlPWPaymentMethod.SelectedIndex, Integer)
            Else
                endCustomer.AreaViolationPaymentMethodID = CType(Me.ddlPWPaymentMethod.SelectedValue, Integer)
            End If
            If Me.txtPWAmount.Text.Trim = String.Empty Then
                _sErrorMessage = "Biaya pelanggaran wilayah tidak boleh kosong"
                Exit Sub
            End If

            endCustomer.AreaViolationyAmount = CType(IIf(Me.txtPWAmount.Text.Trim = String.Empty, "0", Me.txtPWAmount.Text.Trim), Decimal)
        Else
            endCustomer.AreaViolationFlag = Nothing
            endCustomer.AreaViolationBankName = Nothing
            endCustomer.AreaViolationGyroNumber = Nothing
            endCustomer.AreaViolationPaymentMethodID = Nothing
            endCustomer.AreaViolationyAmount = Nothing
        End If

        If Me.chkPembayaranPenalti.Checked Then
            endCustomer.PenaltyFlag = "X"
            endCustomer.PenaltyBankName = Me.txtPPBank.Text.ToUpper
            endCustomer.PenaltyGyroNumber = Me.txtPPNomorGiro.Text
            If Me.ddlPPPaymentMethod.SelectedIndex = 0 Then
                _sErrorMessage = _sErrorMessage & "Belum memilih Cara Pembayaran"
                endCustomer.PenaltyPaymentMethodID = CType(Me.ddlPPPaymentMethod.SelectedIndex, Integer)
            Else
                endCustomer.PenaltyPaymentMethodID = CType(Me.ddlPPPaymentMethod.SelectedValue, Integer)
            End If
            If Me.txtPPAmount.Text.Trim = String.Empty Then
                _sErrorMessage = "Biaya pembayaran penalti tidak boleh kosong"
                Exit Sub
            End If
            endCustomer.PenaltyAmount = CType(IIf(Me.txtPPAmount.Text.Trim = String.Empty, "0", Me.txtPPAmount.Text.Trim), Decimal)
        Else
            endCustomer.PenaltyFlag = Nothing
            endCustomer.PenaltyBankName = Nothing
            endCustomer.PenaltyGyroNumber = Nothing
            endCustomer.PenaltyPaymentMethodID = Nothing
            endCustomer.PenaltyAmount = Nothing
        End If

        If Me.chkSuratReferensi.Checked Then
            endCustomer.ReferenceLetterFlag = "X"
            endCustomer.ReferenceLetter = Me.txtSRNomorSurat.Text
        Else
            endCustomer.ReferenceLetterFlag = Nothing
            endCustomer.ReferenceLetter = Nothing
        End If

        'LKPP
        If Me.chkLKPP.Checked Then
            Dim ValExist As Boolean = False

            ' Check Vehicle Type
            Try
                endCustomer.LKPPHeader = New LKPPHeaderFacade(User).Retrieve(txtNoLKPP.Text.Trim)
                For Each ObjLkppDetail As LKPPDetail In endCustomer.LKPPHeader.LKPPDetails
                    If _objChassisMaster.VechileColor.VechileType.ID = ObjLkppDetail.VechileType.ID Then
                        ValExist = True
                        endCustomer.MCPHeader = Nothing
                        endCustomer.MCPStatus = Nothing
                        Exit For
                    End If
                Next
            Catch ex As Exception

            End Try



            If Not ValExist Then
                _sErrorMessage = "Tipe Kendaraan tidak terdaftar di Pengadaan LKPP"
            End If

            If txtNoLKPP.Text.Trim() = "" Then
                _sErrorMessage = "Nomor LKPP Belum diisi"
            End If
        Else
            endCustomer.LKPPHeader = Nothing
        End If
        'LKPP

        'Customer Profile
        Select Case _objChassisMaster.Category.CategoryCode
            Case "PC"
                SetChassisMasterProfile(PCGroup)
            Case "LCV"
                SetChassisMasterProfile(LCVGroup)
        End Select

        If Not _bIsValidating Then
            endCustomer.SaveBy = User.Identity.Name
            endCustomer.SaveTime = Date.Now
        End If

        _objChassisMaster.EndCustomer = endCustomer
    End Sub

    Private Sub SetChassisMasterProfile(ByVal GroupCode As String)
        Dim objRenderPanel As RenderingProfile = New RenderingProfile
        alGroup = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(GroupCode), CType(EnumProfileType.ProfileType.CHASSISMASTER, Short), User)

        If _formMode = EnumDNET.enumFormMode.Edit Then
            Dim revChassisMasterProfile As ArrayList = New ArrayList

            For Each item As ChassisMasterProfile In alGroup
                Dim revProfile As RevisionChassisMasterProfile = New RevisionChassisMasterProfile
                revProfile.ProfileGroup = item.ProfileGroup
                revProfile.ProfileHeader = item.ProfileHeader
                revProfile.ProfileValue = item.ProfileValue
                revChassisMasterProfile.Add(revProfile)
            Next

            alGroup = New ArrayList
            alGroup = revChassisMasterProfile
        End If
    End Sub

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

#End Region

#Region " Transaction"
    Private Sub SaveInvoiceRevision()



        If Format(Me.icInvoiceDate.Value, "yyyy/MM/dd") >= Format(Date.Now, "yyyy/MM/dd") Then
            ' To allow Invoice creation in 29/30 Sept 2008
            ' Start --------------------------------------------------------
            If Format(Me.icInvoiceDate.Value, "yyyy/MM/dd") <> "2008/09/29" And Format(Me.icInvoiceDate.Value, "yyyy/MM/dd") <> "2008/09/30" Then
                If IsValidTglFaktur() Then
                    ControlToEndCustomer()
                Else
                    _sErrorMessage = "Besok Hari Libur"
                End If
            End If
            ' End --------------------------------------------------------
        Else
            _sErrorMessage = "Tanggal faktur seharusnya >= tanggal hari ini"
        End If
        Dim isMCP_Allowed As Boolean = True
        Dim _isGovermentType As Boolean = False
        Dim _isGovermentOwnerShip As Boolean = False
        Dim _isGovermentName As Boolean = False

        If (_sErrorMessage = "") Then
            ' Do Update
            'Check isGovermentType
            _isGovermentType = isGovermentType(_objChassisMaster.EndCustomer)
            _isGovermentName = isGovermentName(_objChassisMaster.EndCustomer)
            Dim nUpdatedRow As Integer = -1
            Dim prfGroup As ProfileGroup

            Select Case _objChassisMaster.Category.CategoryCode
                Case "PC"
                    prfGroup = New ProfileGroupFacade(User).Retrieve(PCGroup)
                    Dim PCList As ArrayList = New ArrayList
                    PCList = alGroup

                    For Each item As Object In PCList
                        If _formMode = EnumDNET.enumFormMode.Add Then
                            item = CType(item, ChassisMasterProfile)
                        ElseIf _formMode = EnumDNET.enumFormMode.Edit Then
                            item = CType(item, RevisionChassisMasterProfile)
                        End If
                        If (item.ProfileHeader.ID = 5 And item.ProfileGroup.ID = 7) AndAlso (item.ProfileValue = "IP" Or item.ProfileValue = "BN") Then
                            _isGovermentOwnerShip = True

                            If txtNoLKPP.Text.Trim <> "" AndAlso chkLKPP.Checked Then
                                Dim isExistOnLKPP As Boolean = False
                                Dim lkppH As LKPPHeader = New LKPPHeaderFacade(User).Retrieve(txtNoLKPP.Text.Trim)
                                If Not IsNothing(lkppH) Then
                                    For Each lkppD As LKPPDetail In lkppH.LKPPDetails
                                        If _objChassisMaster.VechileColor.VechileType.ID = lkppD.VechileType.ID Then
                                            isExistOnLKPP = True
                                            _objEndCustomer.LKPPHeader = lkppH
                                            _objEndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP
                                            If Not IsNothing(_objEndCustomer.Customer) AndAlso Not IsNothing(_objEndCustomer.Customer.MyCustomerRequest) Then
                                                _objEndCustomer.Customer.MyCustomerRequest.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP
                                            End If

                                        End If
                                    Next
                                End If
                                If Not IsNothing(_objEndCustomer.Customer) AndAlso Not IsNothing(_objEndCustomer.Customer.MyCustomerRequest) Then
                                    _objEndCustomer.Customer.MyCustomerRequest.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP
                                End If

                                If isExistOnLKPP = False Then
                                    _sErrorMessage = "Nomor LKPP tidak sesuai dengan tipe kendaraan yang dipilih."
                                End If
                            Else
                                isMCP_Allowed = False
                                _sErrorMessage = "Customer terdeteksi LKPP. Silahkan masukkan nomor LKPP"
                            End If

                        ElseIf (item.ProfileHeader.ID = 5 And item.ProfileGroup.ID = 7) AndAlso Not (item.ProfileValue = "IP" Or item.ProfileValue = "BN") Then
                            If Not IsNothing(_objEndCustomer.Customer) AndAlso Not IsNothing(_objEndCustomer.Customer.MyCustomerRequest) Then
                                _objEndCustomer.Customer.MyCustomerRequest.LKPPStatus = EnumLKPPStatus.LKPPStatus.NonLKPP
                            End If

                        End If
                    Next

                    'Checking Scenario LKPP
                    'C1 , C6
                    If _isGovermentType OrElse _isGovermentOwnerShip Then
                        If IsNothing(_objEndCustomer.LKPPHeader) Then
                            isMCP_Allowed = False
                            _sErrorMessage = "Customer terdeteksi LKPP. Silahkan masukkan nomor LKPP"
                        Else
                            _objEndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP
                        End If

                    ElseIf _isGovermentName Then
                        _objEndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.IndicatedLKPP

                    ElseIf _isGovermentName = False AndAlso _isGovermentOwnerShip = False AndAlso _isGovermentType = False Then
                        If Not IsNothing(_objEndCustomer.LKPPHeader) Then
                            isMCP_Allowed = False
                            _sErrorMessage = "LKPP Hanya diperuntukan untuk tipe pelanggan BUMN dan pemerintah"
                        Else
                            _objEndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.NonLKPP
                        End If
                    End If

                    If isMCP_Allowed Then
                        If _formMode = EnumDNET.enumFormMode.Add Then
                            Dim revisionFaktur As RevisionFaktur = New RevisionFaktur
                            revisionFaktur.RevisionTypeID = hidRevisionType.Value
                            revisionFaktur.ChassisMaster = _objChassisMaster
                            revisionFaktur.EndCustomer = _objEndCustomer
                            Dim oldEndCUstomerID As Integer = CType(GetFromSession("oldEndCustomerID"), Integer)
                            Dim oldEndCustomer As EndCustomer = New EndCustomerFacade(User).Retrieve(oldEndCUstomerID)
                            revisionFaktur.OldEndCustomer = oldEndCustomer

                            nUpdatedRow = New RevisionFakturFacade(User).Insert(revisionFaktur, PCList, prfGroup)


                        ElseIf _formMode = EnumDNET.enumFormMode.Edit Then
                            _objRevisionFaktur.EndCustomer = New EndCustomer
                            _objRevisionFaktur.EndCustomer = _objEndCustomer
                            nUpdatedRow = New RevisionFakturFacade(User).Update(_objRevisionFaktur, PCList, prfGroup)
                        End If
                    End If

                Case "LCV"
                    prfGroup = New ProfileGroupFacade(User).Retrieve(LCVGroup)
                    Dim LCVList As ArrayList = alGroup
                    For Each item As Object In LCVList
                        If _formMode = EnumDNET.enumFormMode.Add Then
                            item = CType(item, ChassisMasterProfile)
                        ElseIf _formMode = EnumDNET.enumFormMode.Edit Then
                            item = CType(item, RevisionChassisMasterProfile)
                        End If
                        If (item.ProfileHeader.ID = 5 And item.ProfileGroup.ID = 6) AndAlso (item.ProfileValue = "IP" Or item.ProfileValue = "BN") Then
                            _isGovermentOwnerShip = True

                            If txtNoLKPP.Text.Trim <> "" AndAlso chkLKPP.Checked Then
                                Dim isExistOnLKPP As Boolean = False
                                Dim lkppH As LKPPHeader = New LKPPHeaderFacade(User).Retrieve(txtNoLKPP.Text.Trim)
                                If Not IsNothing(lkppH) Then
                                    For Each lkppD As LKPPDetail In lkppH.LKPPDetails
                                        If _objChassisMaster.VechileColor.VechileType.ID = lkppD.VechileType.ID Then
                                            isExistOnLKPP = True
                                            _objEndCustomer.LKPPHeader = lkppH
                                            If Not IsNothing(_objEndCustomer.Customer) AndAlso Not IsNothing(_objEndCustomer.Customer.MyCustomerRequest) Then
                                                _objEndCustomer.Customer.MyCustomerRequest.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP
                                            End If
                                        End If
                                    Next
                                End If
                                If Not IsNothing(_objEndCustomer.Customer) AndAlso Not IsNothing(_objEndCustomer.Customer.MyCustomerRequest) Then
                                    _objEndCustomer.Customer.MyCustomerRequest.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP
                                End If

                                If isExistOnLKPP = False Then
                                    _sErrorMessage = "Nomor LKPP tidak sesuai dengan tipe kendaraan yang dipilih."
                                End If
                            Else
                                isMCP_Allowed = False
                                _sErrorMessage = "Customer terdeteksi LKPP. Silahkan masukkan nomor LKPP"
                            End If

                        ElseIf (item.ProfileHeader.ID = 5 And item.ProfileGroup.ID = 6) AndAlso Not (item.ProfileValue = "IP" Or item.ProfileValue = "BN") Then
                            If Not IsNothing(_objEndCustomer.Customer) AndAlso Not IsNothing(_objEndCustomer.Customer.MyCustomerRequest) Then
                                _objEndCustomer.Customer.MyCustomerRequest.LKPPStatus = EnumLKPPStatus.LKPPStatus.NonLKPP
                            End If

                        End If
                    Next

                    'Checking Scenario LKPP
                    'C1 , C6
                    If _isGovermentType OrElse _isGovermentOwnerShip Then
                        If IsNothing(_objEndCustomer.LKPPHeader) Then
                            isMCP_Allowed = False
                            _sErrorMessage = "Customer terdeteksi LKPP. Silahkan masukkan nomor LKPP"
                        Else
                            _objEndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP
                        End If

                    ElseIf _isGovermentName Then
                        _objEndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.IndicatedLKPP

                    ElseIf _isGovermentName = False AndAlso _isGovermentOwnerShip = False AndAlso _isGovermentType = False Then
                        If Not IsNothing(_objEndCustomer.LKPPHeader) Then
                            isMCP_Allowed = False
                            _sErrorMessage = "LKPP Hanya diperuntukan untuk tipe pelanggan BUMN dan pemerintah"
                        Else
                            _objEndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.NonLKPP
                        End If
                    End If

                    If isMCP_Allowed Then
                        If _formMode = EnumDNET.enumFormMode.Add Then
                            Dim revisionFaktur As RevisionFaktur = New RevisionFaktur
                            revisionFaktur.RevisionTypeID = hidRevisionType.Value
                            revisionFaktur.ChassisMaster = _objChassisMaster
                            revisionFaktur.EndCustomer = _objChassisMaster.EndCustomer
                            Dim oldEndCUstomerID As Integer = CType(GetFromSession("oldEndCustomerID"), Integer)
                            Dim oldEndCustomer As EndCustomer = New EndCustomerFacade(User).Retrieve(oldEndCUstomerID)
                            revisionFaktur.OldEndCustomer = oldEndCustomer

                            nUpdatedRow = New RevisionFakturFacade(User).Insert(revisionFaktur, LCVList, prfGroup)

                        ElseIf _formMode = EnumDNET.enumFormMode.Edit Then
                            _objRevisionFaktur.EndCustomer = New EndCustomer
                            _objRevisionFaktur.EndCustomer = _objEndCustomer
                            nUpdatedRow = New RevisionFakturFacade(User).Update(_objRevisionFaktur, LCVList, prfGroup)
                        End If

                    End If
            End Select

            If isMCP_Allowed Then
                SaveVehicleKind()
            End If

            If nUpdatedRow > -1 Then
                _sSuccessMessage = SR.UpdateSucces()
            Else
                'add by anh - validasi mcp - 20150908
                If _sErrorMessage = "" Then
                    _sErrorMessage = SR.UpdateFail
                End If
                'end added
            End If
        End If
    End Sub

    Private Function IsChassisMatchSPK(ByVal revisionFaktur As RevisionFaktur) As Boolean
        Dim ret As Boolean = False
        Dim objRevisionSPKFaktur As RevisionSPKFaktur
        Dim arlRevisionSPKFaktur As ArrayList
        Dim cri As New CriteriaComposite(New Criteria(GetType(RevisionSPKFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        cri.opAnd(New Criteria(GetType(RevisionSPKFaktur), "EndCustomer.ID", MatchType.Exact, revisionFaktur.EndCustomer.ID))

        arlRevisionSPKFaktur = New RevisionSPKFakturFacade(User).Retrieve(cri)

        For Each row As RevisionSPKFaktur In arlRevisionSPKFaktur
            For Each rowspk As SPKDetail In row.SPKHeader.SPKDetails
                If revisionFaktur.ChassisMaster.VechileColor.ID = rowspk.VechileColor.ID Then
                    ret = True
                    Exit For
                End If
            Next
        Next

        Return ret
    End Function

    Private Function SaveVehicleKind() As Integer
        Dim ddlModel As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlModel"), String))
        Dim oCMFac As New ChassisMasterFacade(User)
        Dim sOK As Integer = -1

        _objChassisMaster.VehicleKind = New VehicleKind(CType(ddlModel.SelectedValue, Integer))
        'If oCMFac.Update(_objChassisMaster) > 0 Then
        sOK = 0
        SaveRevisionChassisMasterProfile()
        'End If
        Return sOK
    End Function

    Private Sub SaveRevisionChassisMasterProfile()
        Dim ddlJenis As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlJenis"), String))
        Dim ddlModel As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlModel"), String))
        Dim ddlLeasing As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlLeasing"), String))
        Dim ddlKaroseri As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlKaroseri"), String))

        Dim oCM As ChassisMaster
        Dim oEndCust As EndCustomer
        Dim oCMPFac As New RevisionChassisMasterProfileFacade(User)
        Dim oCMP As RevisionChassisMasterProfile
        Dim oPHFac As New ProfileHeaderFacade(User)
        Dim oPHJenis As ProfileHeader = oPHFac.Retrieve("CBU_JENISKEND")
        Dim oPHModel As ProfileHeader = oPHFac.Retrieve("CBU_MODELKEND")
        Dim oPHLeasing As ProfileHeader = oPHFac.Retrieve("CBU_LEASING")
        Dim oPHKaroseri As ProfileHeader = oPHFac.Retrieve("CBU_CARROSSERIE")
        Dim oPG As ProfileGroup
        Dim oPGFac As New ProfileGroupFacade(User)
        Dim GroupCode As String = ""
        Dim aCM As ArrayList = CType(Session("sessCM"), ArrayList)
        Dim oVK As VehicleKind = New VehicleKindFacade(User).Retrieve(CType(ddlModel.SelectedValue, Integer))
        Dim oLeasing As Leasing = New LeasingFacade(User).GetLeasing(ddlLeasing.SelectedValue)
        Dim oKaroseri As Karoseri = New KaroseriFacade(User).GetKaroseri(ddlKaroseri.SelectedValue)

        oCM = _objChassisMaster
        oEndCust = _objEndCustomer

        GroupCode = "cust_prf_" & oCM.Category.CategoryCode.ToLower
        oPG = oPGFac.Retrieve(GroupCode)

        oCMP = GetCMProfile(oCM, oPG, oPHJenis)

        oCMP.ChassisMaster = oCM
        oCMP.ProfileGroup = oPG
        oCMP.ProfileHeader = oPHJenis
        oCMP.ProfileValue = oVK.VehicleKindGroup.Code
        If oCMP.ID < 1 Then
            oCMP.EndCustomer = oEndCust
            oCMPFac.Insert(oCMP)
        Else
            oCMPFac.Update(oCMP)
        End If

        oCMP = GetCMProfile(oCM, oPG, oPHModel)
        oCMP.ChassisMaster = oCM
        oCMP.ProfileGroup = oPG
        oCMP.ProfileHeader = oPHModel
        oCMP.ProfileValue = oVK.Code
        If oCMP.ID < 1 Then
            oCMP.EndCustomer = oEndCust
            oCMPFac.Insert(oCMP)
        Else
            oCMPFac.Update(oCMP)
        End If

        If Not IsNothing(oLeasing) Then
            oCMP = GetCMProfile(oCM, oPG, oPHLeasing)
            oCMP.ChassisMaster = oCM
            oCMP.ProfileGroup = oPG
            oCMP.ProfileHeader = oPHLeasing
            oCMP.ProfileValue = oLeasing.LeasingCode
            If oCMP.ID < 1 Then
                oCMP.EndCustomer = oEndCust
                oCMPFac.Insert(oCMP)
            Else
                oCMPFac.Update(oCMP)
            End If
        End If

        If Not IsNothing(oKaroseri) Then
            oCMP = GetCMProfile(oCM, oPG, oPHKaroseri)
            oCMP.ChassisMaster = oCM
            oCMP.ProfileGroup = oPG
            oCMP.ProfileHeader = oPHKaroseri
            oCMP.ProfileValue = oKaroseri.Code
            If oCMP.ID < 1 Then
                oCMP.EndCustomer = oEndCust
                oCMPFac.Insert(oCMP)
            Else
                oCMPFac.Update(oCMP)
            End If
        End If
    End Sub
#End Region

#Region " Validation "
    Private Function ValidateNomorReferensi(ByVal nomor As String) As Boolean
        Dim _invalidString As String = KTB.DNet.Lib.WebConfig.GetValue("InvalidCharacter")
        If nomor.Trim <> String.Empty Then
            Dim _firstChar As String = nomor.Substring(0, 1)
            For Each item As String In _invalidString
                If _firstChar = item Then
                    Return False
                End If
            Next
            Return True
        Else
            Return True
        End If
    End Function

    Private Function IsValidTglFaktur() As Boolean
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayDate", MatchType.Exact, icInvoiceDate.Value.Day))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayMonth", MatchType.Exact, icInvoiceDate.Value.Month))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayYear", MatchType.Exact, icInvoiceDate.Value.Year))
        Dim arlNationalHoliday As ArrayList = New NationalHolidayFacade(User).RetrieveByCriteria(criterias)

        If arlNationalHoliday.Count > 0 Then
            Dim objTimeSpan As TimeSpan = icInvoiceDate.Value.Subtract(DateTime.Now)
            If objTimeSpan.Days >= 1 Then
                For i As Integer = 1 To objTimeSpan.Days
                    Dim criterias1 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayDate", MatchType.Exact, icInvoiceDate.Value.AddDays(i * -1).Day))
                    criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayMonth", MatchType.Exact, icInvoiceDate.Value.AddDays(i * -1).Month))
                    criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayYear", MatchType.Exact, icInvoiceDate.Value.AddDays(i * -1).Year))
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
    End Function

    Private Function GetDefaultInvoiceDate() As Date
        Dim invoiceDate As Date = Date.Now.AddDays(1)
        While IsHoliday(invoiceDate)
            invoiceDate = invoiceDate.AddDays(1)
        End While
        Return invoiceDate
    End Function

    Private Function IsHoliday(paramDate As Date) As Boolean
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayDate", MatchType.Exact, paramDate.Day))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayMonth", MatchType.Exact, paramDate.Month))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayYear", MatchType.Exact, paramDate.Year))
        Dim arlNationalHoliday As ArrayList = New NationalHolidayFacade(User).RetrieveByCriteria(criterias)

        If arlNationalHoliday.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function isGovermentType(ByVal parEndCustomer As EndCustomer) As Boolean
        Try
            If Not IsNothing(parEndCustomer.Customer) AndAlso Not IsNothing(parEndCustomer.Customer.MyCustomerRequest) Then
                Return parEndCustomer.Customer.MyCustomerRequest.Status1 = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah
            Else
                Dim objGG = parEndCustomer.SPKFaktur.SPKHeader.SPKCustomer.TipeCustomer = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah
            End If
        Catch ex As Exception

        End Try


        Return False
    End Function

    Private Function isGovermentName(ByVal parEndCustomer As EndCustomer) As Boolean

        Try
            If Not IsNothing(parEndCustomer.Customer) AndAlso Not IsNothing(parEndCustomer.Customer.MyCustomerRequest) Then
                If Me.IsGovernmentInstitution(parEndCustomer.Customer.MyCustomerRequest.Name1) OrElse Me.IsGovernmentInstitution(parEndCustomer.Customer.MyCustomerRequest.Name2) Then
                    Return True
                End If
            Else
                Dim ObjName1 As String = parEndCustomer.SPKFaktur.SPKHeader.SPKCustomer.Name1
                Dim ObjName2 As String = parEndCustomer.SPKFaktur.SPKHeader.SPKCustomer.Name2

                If Me.IsGovernmentInstitution(ObjName1) OrElse Me.IsGovernmentInstitution(ObjName2) Then
                    Return True
                End If
            End If
        Catch ex As Exception

        End Try


        Return False
    End Function

    Private Function IsGovernmentInstitution(ByVal sName As String) As Boolean
        Dim sMCPList() As String = KTB.DNet.Lib.WebConfig.GetValue("ListOfMCPName").Split(";")
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

    Private Function IsCancelValidateValid(ByVal objRevFaktur As RevisionFaktur) As Boolean
        Dim revisionFaktur As RevisionFaktur = New RevisionFakturFacade(User).Retrieve(objRevFaktur.ID)
        If revisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Validasi Then
            Return True
        End If
        Return False
    End Function

    Private ReadOnly Property IsOpenRevision() As Boolean
        Get
            _objRevisionFaktur = CType(GetFromSession("RevisionFaktur"), RevisionFaktur)
            If IsNothing(_objRevisionFaktur) Then
                Return True
            ElseIf Not IsNothing(_objRevisionFaktur) Then
                Return _objRevisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Baru And _formMode > 0
            End If
        End Get
    End Property
#End Region

    Private Sub hdnNoLKPP_ValueChanged(sender As Object, e As EventArgs) Handles hdnNoLKPP.ValueChanged
        txtNoLKPP.Text = hdnNoLKPP.Value.Trim
    End Sub
End Class