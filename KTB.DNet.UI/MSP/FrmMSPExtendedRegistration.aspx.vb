Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.IO
Imports KTB.DNet.BusinessValidation.Helpers

Public Class FrmMSPExtendedRegistration
    Inherits System.Web.UI.Page

    Private _sessHelper As New SessionHelper
    Private _strSessSearch As String = "FrmMSPExtendedRegistration.Criteria"
    Private _sessData As String = "FrmMSPExtendedRegistration.Data"
    Private _vStateChassis As String = "RegisteredChassis"
    Private _vStateCustomer As String = "MSPCustomer"
    Private _vStateStatusID As String = "StatusID"
    Private objLoginDealer As Dealer
    Private objUserInfo As UserInfo
    Private objMSPRegistration As MSPExRegistration
    Private objMSPCustomer As MSPCustomer
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)
    Dim strMsg As String = String.Empty
    Dim strMSPMasterID As String = String.Empty
    Dim Mode As String = "New"
    Dim Load1st As Boolean = False

    Private Sub PrivInput()
        If Not SecurityProvider.Authorize(Context.User, SR.MSPExtended_Input) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=MSP Extended - Input Registrasi Konsumen")
        End If
    End Sub
    Private Sub PrivView()
        If Not SecurityProvider.Authorize(Context.User, SR.MSPExtended_View) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=MSP Extended - View Registrasi Konsumen")
        End If
    End Sub
    Private Sub PrivEdit()
        If Not SecurityProvider.Authorize(Context.User, SR.MSPExtended_Ubah) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=MSP Extended - Edit Registrasi Konsumen")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objLoginDealer = CType(_sessHelper.GetSession("DEALER"), Dealer)
        objUserInfo = CType(_sessHelper.GetSession("LOGINUSERINFO"), UserInfo)

        If Not IsNothing(Request.QueryString("MOD")) Then
            Mode = Request.QueryString("MOD")
            Load1st = True
        End If
        If Not IsPostBack Then
            FillForm()
            ViewState(_vStateCustomer) = False
            ViewState(_vStateChassis) = False
            hdConfirm.Value = "-1"
        End If
        lnkReloadChassis.Attributes("style") = "display:none"
        lnkReloadMSPLama.Attributes("style") = "display:none"

        If Mode = "View" Then
            DisableInput()
            PrivView()
        ElseIf Mode = "New" Then
            PrivInput()
            'add validasi untuk dealer 07012021 by irfan
            If objLoginDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If ProcessOverDueDealer() Then
                    Server.Transfer("../FrmAccessDenied.aspx?mess=Anda tidak bisa mengajukan registrasi <br/>dikarenakan ada registrasi yang masih berstatus 'Baru', <br/>silahkan lakukan validasi untuk registrasi yang masih 'Baru'")
                End If
            Else
                If ProcessOverDue() Then
                    Server.Transfer("../FrmAccessDenied.aspx?mess=Anda tidak bisa mengajukan registrasi <br/>dikarenakan ada registrasi yang masih berstatus 'Baru', <br/>silahkan lakukan validasi untuk registrasi yang masih 'Baru'")
                End If
            End If
        Else
            PrivEdit()
        End If

        If Not IsNothing(Request.QueryString("SIMPAN")) Then
            If Hidden1.Value = -1 Then
                MessageBox.Show("Simpan Berhasil")
                Hidden1.Value = 0
                Dim varPaymentRegnumber As String = ""
                Dim mPayment As ArrayList = New MSPExPaymentFacade(User).RetrieveUnComplete(objLoginDealer.DealerCode)
                If mPayment.Count > 0 Then

                    For Each oPayment As MSPExPayment In mPayment
                        Dim dtVal As DateTime = Date.Now.AddDays(-30)
                        If oPayment.CreatedTime < dtVal Then
                            If varPaymentRegnumber.Length = 0 Then
                                varPaymentRegnumber = oPayment.RegNumber
                            Else
                                varPaymentRegnumber = varPaymentRegnumber & "," & oPayment.RegNumber
                            End If

                        End If
                    Next
            End If
            If varPaymentRegnumber.Trim.Length > 0 Then
                MessageBox.Show("Anda belum melakukan pembayaran dengan no registration: " & varPaymentRegnumber & " silahkan lakukan pembayaran \n")
            End If
        End If
        End If
        Load1st = False
    End Sub

    Private Sub FillForm()
        BindDropDown()
        SetHeaderData()
    End Sub

    Private Sub BindDropDown()
        'dropdown propinsi
        Dim crt = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arr = New ProvinceFacade(User).RetrieveActiveList(crt, "ProvinceName", Sort.SortDirection.ASC)

        ddlPropinsi.Items.Clear()
        ddlPropinsi.DataSource = arr
        ddlPropinsi.DataTextField = "ProvinceName"
        ddlPropinsi.DataValueField = "ID"
        ddlPropinsi.DataBind()
        ddlPropinsi.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlPropinsi.SelectedIndex = 0
        ddlPropinsi_SelectedIndexChanged(Me, System.EventArgs.Empty)

        ddlTipeMSP.Items.Insert(0, New ListItem("Silahkan Pilih", 0))
        ddlTipeMSP.Enabled = False
    End Sub

    Private Sub SetHeaderData()
        lblDealer.Text = objLoginDealer.DealerCode & "/" & objLoginDealer.SearchTerm1
        lblSubmitBy.Text = objLoginDealer.DealerCode & "-" & objUserInfo.UserName
        lblCurrentDate.Text = Date.Now.ToString("dd/MM/yyyy")

        If Mode = "New" Then
            lblMSPNo.Text = "[Auto Generated]"
            lblStatus.Text = EnumMSPEx.MSPExStatus.Baru.ToString()
            btnBack.Visible = False
        Else
            If Not IsNothing(Request.QueryString("REGN")) Then
                hdnMSPLama.Value = Request.QueryString("REGN")
            End If

            Dim oMSPExRegistration As MSPExRegistration = New MSPExRegistrationFacade(User).RetrieveByRegNumber(hdnMSPLama.Value)
            lblMSPNo.Text = oMSPExRegistration.RegNumber
            ViewState(_vStateStatusID) = oMSPExRegistration.Status
            lblStatus.Text = EnumMSPEx.GetStringValue(oMSPExRegistration.Status)

            lblDealer.Text = oMSPExRegistration.Dealer.DealerCode & "/" & oMSPExRegistration.Dealer.SearchTerm1
            lblSubmitBy.Text = oMSPExRegistration.Dealer.DealerCode & "-" & oMSPExRegistration.CreatedBy.Remove(0, 6)
            lblCurrentDate.Text = oMSPExRegistration.CreatedTime.ToString("dd/MM/yyyy")

            btnBack.Visible = True
            LoadData(False)
            If Mode = "View" Then
                btnSave.Visible = False
                btnValidasi.Visible = False
            ElseIf Mode = "Edit" Then
                'btnSave.Visible = False
                btnValidasi.Visible = True
            End If
        End If
    End Sub

    Private Sub DisableInput()
        txtChassisNo.Enabled = False
        txtMSPLama.Enabled = False
        txtNoMesin.Enabled = False
        txtNoKTP.Enabled = False
        txtNama.Enabled = False
        txtUsia.Enabled = False
        txtAlamat.Enabled = False
        txtKelurahan.Enabled = False
        txtKecamatan.Enabled = False
        ddlPropinsi.Enabled = False
        ddlPreArea.Enabled = False
        ddlKota.Enabled = False
        txtNoTelp.Enabled = False
        txtEmail.Enabled = False
        txtOdo.Enabled = False
        ddlTipeMSP.Enabled = False
    End Sub

    Private Sub ddlPropinsi_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlPropinsi.SelectedIndexChanged
        ddlKota.Items.Clear()
        If ddlPropinsi.SelectedIndex <> 0 Then
            Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crt.opAnd(New Criteria(GetType(City), "Province.ID", MatchType.Exact, ddlPropinsi.SelectedValue))
            crt.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))

            ddlKota.DataSource = New CityFacade(User).RetrieveActiveList(crt, "CityName", Sort.SortDirection.ASC)
            ddlKota.DataTextField = "CityName".ToUpper
            ddlKota.DataValueField = "ID"
            ddlKota.DataBind()
        End If
        ddlKota.Items.Insert(0, New ListItem("Silahkan Pilih", 0))
        ddlKota.SelectedIndex = 0

    End Sub

    Protected Sub lnkReloadChassis_Click(sender As Object, e As EventArgs) Handles lnkReloadChassis.Click
        If txtChassisNo.Text.Trim.Length > 0 Then
            hdnChassisNo.Value = txtChassisNo.Text.Trim
            LoadData(True)
        Else
            MessageBox.Show("Nomor Chassis masih kosong")
        End If
    End Sub

    Protected Sub lnkReloadMSPLama_Click(sender As Object, e As EventArgs) Handles lnkReloadMSPLama.Click
        If txtMSPLama.Text.Trim.Length > 0 Then
            hdnMSPLama.Value = txtMSPLama.Text
            LoadData(False)
        Else
            MessageBox.Show("Nomor MSP Lama masih kosong")
        End If
    End Sub

    Private Sub ClearInput()
        'lblDealer.Text = String.Empty
        'lblMSPNo.Text = String.Empty
        'lblSubmitBy.Text = String.Empty
        'lblCurrentDate.Text = String.Empty
        'lblStatus.Text = String.Empty
        'hdnChassisNo.Value = String.Empty
        'txtChassisNo.Text = String.Empty
        'hdnMSPLama.Value = String.Empty
        'txtMSPLama.Text = String.Empty
        lblTipeKendaraan.Text = String.Empty
        lblTglBukaFaktur.Text = String.Empty
        txtNoMesin.Text = String.Empty
        txtNoKTP.Text = String.Empty
        txtNama.Text = String.Empty
        txtUsia.Text = String.Empty
        txtAlamat.Text = String.Empty
        txtKelurahan.Text = String.Empty
        txtKecamatan.Text = String.Empty
        ddlPropinsi.SelectedIndex = 0
        ddlPreArea.SelectedIndex = 0
        ddlKota.SelectedIndex = 0
        txtNoTelp.Text = String.Empty
        txtEmail.Text = String.Empty
        txtOdo.Text = String.Empty
        ddlTipeMSP.SelectedIndex = 0
        ddlTipeMSP.Enabled = False
        lblDurasi.Text = String.Empty
        lblValidSampaiTanggal.Text = String.Empty
        lblValidSampaiKM.Text = String.Empty
        lblHarga.Text = String.Empty
        lblPPN.Text = String.Empty
        lblTotalHarga.Text = String.Empty
    End Sub

    Protected Sub LoadData(ByVal byChassis As Boolean)
        If Mode = "New" Then
            btnSave.Enabled = True
            btnValidasi.Visible = False
        End If

        Dim oMSPExRegistration As MSPExRegistration
        If byChassis Then
            oMSPExRegistration = New MSPExRegistrationFacade(User).RetrieveByChassisNumber(hdnChassisNo.Value)
        Else
            oMSPExRegistration = New MSPExRegistrationFacade(User).RetrieveByRegNumber(hdnMSPLama.Value)
        End If
        ClearInput()
        If Not IsNothing(oMSPExRegistration) Then
            If oMSPExRegistration.ID > 0 Then
                _sessHelper.SetSession(_sessData, oMSPExRegistration)
                txtMSPLama.Text = oMSPExRegistration.RegNumber
                txtChassisNo.Text = oMSPExRegistration.ChassisMaster.ChassisNumber
                hdnMSPLama.Value = oMSPExRegistration.RegNumber
                hdnChassisNo.Value = oMSPExRegistration.ChassisMaster.ChassisNumber

                txtOdo.Text = oMSPExRegistration.MileAge
                lblTipeKendaraan.Text = oMSPExRegistration.ChassisMaster.VechileColor.VechileType.Description
                ViewState("VTypeID") = oMSPExRegistration.ChassisMaster.VechileColor.VechileType.ID
                If oMSPExRegistration.ChassisMaster.EndCustomer.OpenFakturDate.Year > 1900 Then
                    lblTglBukaFaktur.Text = oMSPExRegistration.ChassisMaster.EndCustomer.OpenFakturDate
                End If
                'txtNoMesin.Text = oMSPExRegistration.ChassisMaster.EngineNumber

                If Not IsNothing(oMSPExRegistration.MSPCustomer) Then
                    If oMSPExRegistration.MSPCustomer.ID > 0 Then
                        txtNoKTP.Text = oMSPExRegistration.MSPCustomer.KTPNo
                        txtNama.Text = oMSPExRegistration.MSPCustomer.Name1
                        txtUsia.Text = oMSPExRegistration.MSPCustomer.Age
                        txtAlamat.Text = oMSPExRegistration.MSPCustomer.Alamat
                        txtKelurahan.Text = oMSPExRegistration.MSPCustomer.Kelurahan
                        txtKecamatan.Text = oMSPExRegistration.MSPCustomer.Kecamatan
                        ddlPreArea.SelectedValue = oMSPExRegistration.MSPCustomer.PreArea
                        If Not IsNothing(oMSPExRegistration.MSPCustomer.Province) Then
                            ddlPropinsi.SelectedValue = oMSPExRegistration.MSPCustomer.Province.ID
                            ddlPropinsi_SelectedIndexChanged(Nothing, Nothing)
                        End If
                        If Not IsNothing(oMSPExRegistration.MSPCustomer.City) Then
                            ddlKota.SelectedValue = oMSPExRegistration.MSPCustomer.City.ID
                        End If
                        txtNoTelp.Text = oMSPExRegistration.MSPCustomer.PhoneNo
                        txtEmail.Text = oMSPExRegistration.MSPCustomer.Email
                    End If
                Else
                    ViewState(_vStateCustomer) = True
                End If

                If LoadMSPType(oMSPExRegistration.ChassisMaster.VechileColor.VechileType.ID) > 0 Then
                    If Not IsNothing(oMSPExRegistration.MSPExMaster) Then
                        If oMSPExRegistration.MSPExMaster.ID > 0 Then
                            Try
                                ddlTipeMSP.SelectedValue = oMSPExRegistration.MSPExMaster.MSPExType.ID
                                ddlTipeMSP_SelectedIndexChanged(Nothing, Nothing)
                            Catch ex As Exception
                                MessageBox.Show("Tipe " & oMSPExRegistration.MSPExMaster.MSPExType.Code & " Tidak cocok untuk tipe kendaraan tersebut")
                                ddlTipeMSP.Enabled = False
                            End Try
                        End If
                    End If
                End If

                lblTanggalPKT.Text = New ChassisMasterPKTFacade(User).RetrieveByChassiNumber(oMSPExRegistration.ChassisMaster.ID).PKTDate
            Else
                Dim cMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(hdnChassisNo.Value)
                'txtNoMesin.Text = cMaster.EngineNumber
                If Not IsNothing(cMaster.VechileColor) Then
                    If Not IsNothing(cMaster.VechileColor.VechileType) Then
                        lblTipeKendaraan.Text = cMaster.VechileColor.VechileType.Description
                        LoadMSPType(cMaster.VechileColor.VechileType.ID)
                    End If
                End If
                If Not IsNothing(cMaster.EndCustomer) Then
                    If cMaster.EndCustomer.ID > 0 Then
                        If cMaster.EndCustomer.OpenFakturDate.Year > 1900 Then
                            lblTglBukaFaktur.Text = cMaster.EndCustomer.OpenFakturDate
                        End If
                    End If
                End If
                lblTanggalPKT.Text = New ChassisMasterPKTFacade(User).RetrieveByChassiNumber(cMaster.ID).PKTDate
            End If
        Else
            ViewState(_vStateChassis) = True
        End If
    End Sub

    Private Function LoadMSPType(ByVal TypeID As Integer) As Integer
        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crt.opAnd(New Criteria(GetType(MSPExMaster), "VechileType.ID", MatchType.Exact, TypeID))
        crt.opAnd(New Criteria(GetType(MSPExMaster), "Status", MatchType.Exact, 1))
        ViewState("VTypeID") = TypeID

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(MSPExMaster), "ID", Sort.SortDirection.ASC))
        Dim arr As ArrayList = New MSPExMasterFacade(User).RetrieveByCriteria(crt, sortColl)
        If arr.Count = 0 Then
            MessageBox.Show("Tipe kendaraan tidak terdaftar pada Smart Program Extended")
            Return 0
        Else
            Dim i As Integer = 0
            For Each item As MSPExMaster In arr
                If item.EndDate < Date.Now Then
                    arr.RemoveAt(i)
                End If
                i = i + 1
            Next
            If arr.Count = 0 Then
                MessageBox.Show("Smart Program Extended untuk Tipe kendaraan tersebut sudah expired")
                Exit Function
            End If
        End If

        ddlTipeMSP.Enabled = True
        ddlTipeMSP.Items.Clear()
        For Each item As MSPExMaster In arr
            ddlTipeMSP.Items.Add(New ListItem(item.MSPExType.Description, item.MSPExType.ID))
        Next
        ddlTipeMSP.DataBind()
        ddlTipeMSP.Items.Insert(0, New ListItem("Silahkan Pilih", 0))
        ddlTipeMSP.SelectedIndex = 0
        Return arr.Count
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim mess As String = ""
        If Not InputValidation(mess) Then
            MessageBox.Show(mess)
            Exit Sub
        End If

        Dim result As Integer
        Dim oReg As New MSPExRegistration
        If Mode = "Edit" Then
            oReg = CType(_sessHelper.GetSession(_sessData), MSPExRegistration)
        End If
        oReg.Dealer = objLoginDealer
        Dim cMaster As ChassisMaster = New ChassisMasterFacade(User).RetrieveByChassisAndEngine(txtChassisNo.Text.Trim, txtNoMesin.Text.Trim)
        oReg.ChassisMaster = cMaster
        oReg.MileAge = txtOdo.Text
        Dim oMSPExMaster As MSPExMaster = New MSPExMasterFacade(User).RetrieveByMSPExType(CInt(ddlTipeMSP.SelectedValue), cMaster.VechileColor.VechileType.ID)
        oReg.MSPExMaster = oMSPExMaster
        Dim oPrefixMSPRegistration As PrefixMSPRegistration = New PrefixMSPRegistrationFacade(User).Retrieve(oMSPExMaster.MSPExType)
        oReg.Prefix = oPrefixMSPRegistration.Prefix
        oReg.ValidDateTo = lblValidSampaiTanggal.Text
        oReg.ValidKMTo = CInt(lblValidSampaiKM.Text)
        oReg.WarrantyValidDateTo = lblWarrantyValidSampaiDate.Text
        oReg.WarrantyValidKMTo = CInt(lblWarrantyValidSampaiKM.Text)
        oReg.IsTransfertoSAP = 0
        If Mode = "Edit" Then
            oReg.Status = CInt(ViewState(_vStateStatusID))
            result = New MSPExRegistrationFacade(User).UpdateTransaction(ConstructCustomerData(oReg.MSPCustomer), oReg)
        Else
            oReg.Status = 0
            result = New MSPExRegistrationFacade(User).InsertTransaction(ConstructCustomerData(), oReg)
        End If
        If result > -1 Then
            Dim oREgis As MSPExRegistration
            If Mode = "Edit" Then
                oREgis = CType(_sessHelper.GetSession(_sessData), MSPExRegistration)
            Else
                oREgis = New MSPExRegistrationFacade(User).Retrieve(result)
            End If
            InsertStatusChangeHistory(oREgis.RegNumber, EnumMSPEx.MSPExStatus.Baru, "-1")
            'SendWSMPart2(oREgis.ID)
            Response.Redirect("../MSP/FrmMSPExtendedRegistration.aspx?MOD=Edit&SIMPAN=1&REGN=" & oREgis.RegNumber)
        Else
            MessageBox.Show("Simpan Gagal")
        End If
    End Sub

    Private Function ConstructCustomerData(Optional ByVal mCustomer As MSPCustomer = Nothing) As MSPCustomer
        If IsNothing(mCustomer) Then
            mCustomer = New MSPCustomer
        End If

        mCustomer.KTPNo = txtNoKTP.Text.Trim
        mCustomer.Name1 = txtNama.Text.Trim
        If txtUsia.Text.Trim.Length > 0 Then
            mCustomer.Age = txtUsia.Text.Trim
        End If
        mCustomer.Alamat = txtAlamat.Text.Trim
        If txtKelurahan.Text.Trim.Length > 0 Then
            mCustomer.Kelurahan = txtKelurahan.Text.Trim
        End If
        If txtKecamatan.Text.Trim.Length > 0 Then
            mCustomer.Kecamatan = txtKecamatan.Text.Trim
        End If
        If ddlPropinsi.SelectedIndex > 0 Then
            mCustomer.Province = New ProvinceFacade(User).Retrieve(CInt(ddlPropinsi.SelectedValue))
        End If
        If ddlKota.SelectedIndex > 0 Then
            mCustomer.City = New CityFacade(User).Retrieve(CInt(ddlKota.SelectedValue))
        End If
        mCustomer.PhoneNo = txtNoTelp.Text.Trim
        mCustomer.Email = txtEmail.Text.Trim
        Return mCustomer
    End Function

    Private Function InputValidation(ByRef message As String) As Boolean
        If txtChassisNo.Text.Trim.Length = 0 Then
            message = message & "Nomor Chassis Tidak boleh kosong \n"
        End If

        If txtNoMesin.Text.Trim.Length = 0 Then
            message = message & "Nomor Mesin Tidak boleh kosong \n"
        End If

        If txtNama.Text.Trim.Length = 0 Then
            message = message & "Nama STNK Tidak boleh kosong \n"
        End If

        If txtAlamat.Text.Trim.Length = 0 Then
            message = message & "Alamat Customer Tidak boleh kosong \n"
        End If

        If txtNoTelp.Text.Trim.Length = 0 Then
            message = message & "No Telp Customer Tidak boleh kosong \n"
        End If

        If txtOdo.Text.Trim.Length = 0 Then
            message = message & "Odometer Tidak boleh kosong \n"
        End If

        If ddlTipeMSP.SelectedIndex = 0 Then
            message = message & "Pilih Tipe MSP Extended terlibih dahulu \n"
        End If

        If txtEmail.Text.Trim.Length <> 0 Then
            If Not txtEmail.Text.Contains("@") Then
                message = message & "Format Email salah \n"
            End If
        End If

        Dim cMaster As ChassisMaster = New ChassisMasterFacade(User).RetrieveByChassisAndEngine(txtChassisNo.Text.Trim, txtNoMesin.Text.Trim)
        If Not IsNothing(cMaster) Then
            If cMaster.ID = 0 Then
                message = message & "Chassis Number dengan Nomor Mesin tersebut tidak ditemukan \n"
            End If
        Else
            message = message & "Chassis Number dengan Nomor Mesin tersebut tidak ditemukan \n"
        End If

        If Not IsNothing(cMaster.EndCustomer) Then
            If cMaster.FakturStatus <> 4 Then
                message = message & "Chassis yang diinputkan harus berstatus faktur Selesai \n"
            End If
        Else
            message = message & "Chassis yang diinputkan harus sudah dilakukan input faktur \n"
        End If


        If New PrefixMSPRegistrationFacade(User).ValidateMSPExTypeID(ddlTipeMSP.SelectedValue) = 0 Then
            message = message & "Simpan Gagal, Kategori Program untuk Tipe Paket yang digunakan Belum Terdaftar, Silahkan Menghubungi SU.DNET_Ops@bsi.co.id \n"
        End If

        If Mode = "New" Then
            Dim oReg As MSPExRegistration = New MSPExRegistrationFacade(User).RetrieveByChassisNumber(txtChassisNo.Text)
            If oReg.ID > 0 Then
                Dim oPrefix As PrefixMSPRegistration = New PrefixMSPRegistrationFacade(User).Retrieve(oReg.MSPExMaster.MSPExType)
                Dim MaxPM As Integer = New MSPExRegistrationFacade(User).RetrieveCountMaxPM(oReg.MSPExMaster.MSPExType.ID)
                Dim HistoryService As Integer = New MSPExRegistrationFacade(User).RetrieveCountHistoryService(oReg.ChassisMaster.ID, oReg.CreatedTime)
                Dim CountFS As Integer = New MSPExRegistrationFacade(User).RetrieveCountFS(oReg.MSPExMaster.MSPExType.ID, oReg.ChassisMaster.ID, oReg.CreatedTime, oReg.ValidDateTo)
                If oPrefix.ProgramName = "MSPExtended" Then
                    If oReg.ValidDateTo.Date > Date.Now AndAlso MaxPM > HistoryService Then
                        message = message & "MSP Extended dengan Chassis " & txtChassisNo.Text & " masih berstatus Aktif, hanya boleh mengajukan ketika sudah berstatus Non Aktif \n"
                    End If
                Else
                    Dim arrPrefix As ArrayList = New PrefixMSPRegistrationFacade(User).RetrieveList(oPrefix.ProgramName)
                    Dim strArrMSPExTypeID As String = String.Empty
                    If arrPrefix.Count > 0 Then
                        For Each oPrefixMSPRegistration As PrefixMSPRegistration In arrPrefix
                            If strArrMSPExTypeID = String.Empty Then
                                strArrMSPExTypeID = oPrefixMSPRegistration.MSPExTypeID.ToString
                            Else
                                If Not strArrMSPExTypeID.Split(",").Contains(oPrefixMSPRegistration.MSPExTypeID.ToString) Then
                                    strArrMSPExTypeID = strArrMSPExTypeID & "," & oPrefixMSPRegistration.MSPExTypeID.ToString
                                End If
                            End If
                        Next
                    End If

                    If strArrMSPExTypeID.Split(",").Contains(ddlTipeMSP.SelectedValue) Then
                        message = message & "Chassis " & txtChassisNo.Text & " sudah pernah mengajukan Fleet Service Package \n"
                    Else
                        If oReg.ValidDateTo.Date > Date.Now Then
                            message = message & "MSP Extended Fleet dengan Chassis " & txtChassisNo.Text & " masih berstatus Aktif, hanya boleh mengajukan ketika sudah berstatus Non Aktif \n"
                        End If
                    End If
                End If
            End If
        End If

        'Dim oReg As MSPExRegistration = New MSPExRegistrationFacade(User).RetrieveByChassisNumber(txtChassisNo.Text)
        'If oReg.ValidDateTo.Date > Date.Now Then
        '    message = message & "MSP Extended dengan Chassis " & txtChassisNo.Text & " masih berstatus Aktif, hanya boleh mengajukan ketika sudah berstatus Non Aktif \n"
        'End If

        If message.Trim.Length > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Protected Sub btnValidasi_Click(sender As Object, e As EventArgs) Handles btnValidasi.Click
        If hdConfirm.Value = "-1" Then
            RegisterStartupScript("Confirm", "<script>ShowConfirm('Apakah yakin ingin menvalidasi?', 'btnValidasi');</script>")
            Return
        Else
            hdConfirm.Value = "-1"
        End If
        If Not IsNothing(_sessHelper.GetSession(_sessData)) Then
            Dim oMSPExRegistration As MSPExRegistration = CType(_sessHelper.GetSession(_sessData), MSPExRegistration)
            Dim oldStatus As Short = oMSPExRegistration.Status
            Dim newStatus As Short = 0

            If IsMspProgramFleet() Then
                newStatus = EnumMSPEx.MSPExStatus.Selesai
            Else
                newStatus = EnumMSPEx.MSPExStatus.Validasi
            End If
            oMSPExRegistration.Status = newStatus

            Dim result As Integer = New MSPExRegistrationFacade(User).Update(oMSPExRegistration)
            If result > -1 Then
                If oMSPExRegistration.MSPExMaster.MSPExType.SendToSAP = 1 Then
                    SendWSM(oMSPExRegistration.ID)
                End If
                InsertStatusChangeHistory(oMSPExRegistration.RegNumber, newStatus, oldStatus)
                Response.Redirect("../MSP/FrmMSPExtendedRegistration.aspx?MOD=View&SIMPAN=1&REGN=" & oMSPExRegistration.RegNumber)
            Else
                MessageBox.Show("Simpan Gagal")
            End If
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        RemoveSession()
        Response.Redirect("FrmMSPExtendedRegistrationList.aspx")
    End Sub

    Private Sub RemoveSession()
        _sessHelper.RemoveSession(_sessData)
        _sessHelper.RemoveSession(_strSessSearch)
    End Sub

    Protected Sub ddlTipeMSP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipeMSP.SelectedIndexChanged
        If ddlTipeMSP.SelectedIndex <> 0 Then
            Dim VTypeID As Integer = 0
            If Not IsNothing(ViewState("VTypeID")) Then
                VTypeID = CInt(ViewState("VTypeID"))
            End If
            Dim oMaster As MSPExMaster = New MSPExMasterFacade(User).RetrieveByMSPExType(CInt(ddlTipeMSP.SelectedValue), VTypeID)
            If Not IsNothing(oMaster) Then
                If oMaster.ID > 0 Then
                    lblDurasi.Text = oMaster.Duration & " Tahun/" & oMaster.MSPExKM & " KM"
                    Dim ValidDate As Date = Date.Now
                    Dim ValidKM As Integer = 0
                    GetValidDateKM(oMaster, ValidDate, ValidKM)

                    lblValidSampaiTanggal.Text = ValidDate.ToString("dd MMM yyyy")
                    lblValidSampaiKM.Text = ValidKM

                    Dim WarrantyValidDate As Date = Date.Now
                    Dim WarrantyValidKM As Integer = 0
                    getWarrantyData(oMaster, WarrantyValidDate, WarrantyValidKM)

                    lblWarrantyValidSampaiDate.Text = WarrantyValidDate.ToString("dd MMM yyyy")
                    lblWarrantyValidSampaiKM.Text = WarrantyValidKM

                    lblHarga.Text = oMaster.Amount.ToString("#,##0")
                    Dim oMSPExRegistration As MSPExRegistration = New MSPExRegistrationFacade(User).RetrieveByRegNumber(hdnMSPLama.Value)
                    Dim ppnVal As Decimal = 0

                    If Mode = "New" Then
                        ppnVal = CalcHelper.GetPPNMasterByTaxTypeId(DateTime.Now.Date, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
                    Else
                        ppnVal = CalcHelper.GetPPNMasterByTaxTypeId(oMSPExRegistration.CreatedTime, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
                    End If

                    'lblPPN.Text = (oMaster.Amount * 0.1).ToString("#,##0")
                    'lblTotalHarga.Text = (oMaster.Amount * 1.1).ToString("#,##0")
                    lblPPN.Text = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnVal, dpp:=oMaster.Amount).ToString("#,##0")
                    lblTotalHarga.Text = (oMaster.Amount + CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnVal, dpp:=oMaster.Amount)).ToString("#,##0")
                Else
                    MessageBox.Show("Tidak ada Master data Untuk Tipe ini")
                    Exit Sub
                End If
            Else
                MessageBox.Show("Tidak ada Master data Untuk Tipe ini")
                Exit Sub
            End If
        Else
            lblDurasi.Text = ""
            lblValidSampaiTanggal.Text = ""
            lblValidSampaiKM.Text = ""
            lblHarga.Text = ""
            lblPPN.Text = ""
            lblTotalHarga.Text = ""
        End If
    End Sub

    Private Function GetValidDateKM(ByVal oMSPExMaster As MSPExMaster, ByRef ValidDate As Date, ByRef ValidKM As Integer) As String
        Dim MSPEX As MSPExRegistration
        If Mode = "Edit" Then
            MSPEX = New MSPExRegistrationFacade(User).RetrieveByCNumberAndNotRNumber(txtChassisNo.Text.Trim, lblMSPNo.Text.Trim)
        Else
            MSPEX = New MSPExRegistrationFacade(User).RetrieveByChassisNumber(txtChassisNo.Text.Trim)
        End If

        Dim odometer As Integer = 0
        If txtOdo.Text.Trim <> "" Then
            odometer = CInt(txtOdo.Text)
        End If

        If Not IsMspProgramFleet() Then
            If MSPEX.ID = 0 Then
                Dim MSPHist As MSPRegistrationHistory = DataMSPLama()
                If MSPHist.ID = 0 Then
                    ValidDate = DateAdd(DateInterval.Year, oMSPExMaster.Duration, Date.Now)
                    ValidKM = odometer + oMSPExMaster.MSPExKM
                Else
                    Dim expiredDateMSPLama As Date = DateAdd(DateInterval.Year, MSPHist.MSPMaster.Duration, MSPHist.RegistrationDate)
                    If expiredDateMSPLama >= Date.Now AndAlso MSPHist.MSPMaster.MSPKm >= odometer Then
                        ValidDate = DateAdd(DateInterval.Year, oMSPExMaster.Duration, expiredDateMSPLama)
                        ValidKM = MSPHist.MSPMaster.MSPKm + oMSPExMaster.MSPExKM
                    Else
                        ValidDate = DateAdd(DateInterval.Year, oMSPExMaster.Duration, Date.Now)
                        ValidKM = odometer + oMSPExMaster.MSPExKM
                    End If
                End If
            Else
                'Dim PMCount As Integer = GetPMCount(MSPEX.MSPExMaster.MSPExType)
                Dim PMCount As Integer = getPMCount1(MSPEX.MSPExMaster.MSPExType)
                Dim MaxPM As Integer = GetMaxPM(MSPEX.MSPExMaster.MSPExType).Count
                If MSPEX.ValidDateTo.Date >= Date.Now _
                    AndAlso MSPEX.ValidKMTo >= odometer _
                    AndAlso PMCount < MaxPM Then

                    ValidDate = DateAdd(DateInterval.Year, oMSPExMaster.Duration, MSPEX.ValidDateTo)
                    ValidKM = MSPEX.ValidKMTo + oMSPExMaster.MSPExKM

                    If Mode = "New" Then
                        'Dim SvcCount = New MSPExRegistrationFacade(User).RetrieveCountHistoryService(txtChassisNo.Text.Trim, MSPEX.ID)
                        Dim SvcCount = New MSPExRegistrationFacade(User).RetrieveCountHistoryService(MSPEX.ChassisMaster.ID, MSPEX.CreatedTime)
                        If SvcCount = MaxPM And MSPEX.ValidDateTo.Date < Date.Now Then
                            ValidDate = Date.Now.AddDays(oMSPExMaster.Duration)
                            ValidKM = odometer + oMSPExMaster.MSPExKM
                        Else
                            MessageBox.Show("Chassis ini masih memiliki Program MSP Extended yang berstatus Aktif")
                            btnSave.Enabled = False
                            btnValidasi.Visible = False
                        End If
                    End If
                Else
                    ValidDate = DateAdd(DateInterval.Year, oMSPExMaster.Duration, Date.Now)
                    ValidKM = odometer + oMSPExMaster.MSPExKM
                End If
            End If
        Else
            ValidDate = DateAdd(DateInterval.Year, oMSPExMaster.Duration, Date.Now)
            ValidKM = oMSPExMaster.MSPExKM
        End If

        If Load1st AndAlso Mode = "View" Then
            ValidDate = MSPEX.ValidDateTo
            ValidKM = MSPEX.ValidKMTo
        End If
    End Function

    Private Function IsMspProgramFleet() As Boolean
        'Dim VTypeID As Integer = 0
        'If Not IsNothing(ViewState("VTypeID")) Then
        '    VTypeID = CInt(ViewState("VTypeID"))
        'End If

        Dim arrPrefix As ArrayList = New PrefixMSPRegistrationFacade(User).RetrieveList("FleetPackage")
        Dim strArrMSPExTypeID As String = String.Empty
        If arrPrefix.Count > 0 Then
            For Each oPrefixMSPRegistration As PrefixMSPRegistration In arrPrefix
                If strArrMSPExTypeID = String.Empty Then
                    strArrMSPExTypeID = oPrefixMSPRegistration.MSPExTypeID.ToString
                Else
                    If Not strArrMSPExTypeID.Split(",").Contains(oPrefixMSPRegistration.MSPExTypeID.ToString) Then
                        strArrMSPExTypeID = strArrMSPExTypeID & "," & oPrefixMSPRegistration.MSPExTypeID.ToString
                    End If
                End If
            Next
        End If

        If strArrMSPExTypeID.Split(",").Contains(ddlTipeMSP.SelectedValue) Then
            Return True
        Else
            Return False
        End If

        'Dim oMaster As MSPExMaster = New MSPExMasterFacade(User).RetrieveByMSPExType(CInt(ddlTipeMSP.SelectedValue), VTypeID)
        'Select Case oMaster.MSPExType.ID
        '    Case 1, 2, 3 'Non Fleet
        '        Return False
        '    Case Else
        '        Return True
        'End Select
    End Function

    Private Function DataMSPLama() As MSPRegistrationHistory
        Dim critMSP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPRegistrationHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critMSP.opAnd(New Criteria(GetType(MSPRegistrationHistory), "MSPRegistration.ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisNo.Text.Trim))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(MSPRegistrationHistory), "CreatedTime", Sort.SortDirection.DESC))

        Dim arlMSP As ArrayList = New MSPRegistrationHistoryFacade(User).Retrieve(critMSP, sortColl)
        If arlMSP.Count > 0 Then
            Return New MSPRegistrationHistoryFacade(User).Retrieve(critMSP, sortColl)(0)
        End If
        Return New MSPRegistrationHistory
    End Function

    Private Function GetPMCount(ByVal oMSPExType As MSPExType) As Integer
        Dim FSKindID As String = "0"
        For Each item As MSPExMappingtoFSKind In GetMaxPM(oMSPExType) 'Max PM dari MSPExMappingtoFSKind berdasarkan MSPExType
            If FSKindID.Length = 0 Then
                FSKindID = item.FSKind.ID
            Else
                FSKindID = FSKindID & "," & item.FSKind.ID 'Diloop untuk ambil ID FSKind dari MSPExMappingtoFSKind (ex: 1,2,3,4)
            End If
        Next
        Dim critFS As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critFS.opAnd(New Criteria(GetType(FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisNo.Text.Trim))
        critFS.opAnd(New Criteria(GetType(FreeService), "FSKind.ID", MatchType.InSet, "(" & FSKindID & ")")) 'Hasil dari loop dimasukin kesini
        Dim arlFS As ArrayList = New FreeServiceFacade(User).Retrieve(critFS)
        'Hasil dari critFS di atas jadi query berikut
        'SELECT * FROM dbo.FreeService
        'INNER JOIN dbo.ChassisMaster ON dbo.FreeService.ChassisMasterID = dbo.ChassisMaster.ID
        'INNER JOIN dbo.FSKind ON dbo.FreeService.FSKindID = dbo.FSKind.ID
        'WHERE dbo.ChassisMaster.ChassisNumber = 'txtChassisNo.Text.Trim'
        'AND dbo.FSKind.ID IN (sekian, sekian) -- Hasil Loop

        'Bagian ini untuk grouping berdasarkan chassismaster ID jadi hasilnya macem count(*) di sql server
        'comment by ridwan
        If arlFS.Count > 0 Then
            Dim groupedArr As Integer = (From a As FreeService In arlFS
                                         Group By a.ChassisMaster.ID Into Group
                                         Select Group).Count

            Return groupedArr
        End If
        Return 0
        'Return arlFS.Count
    End Function

    Private Function getPMCount1(ByVal oMSPExType As MSPExType) As Integer
        Dim FSKindID As String = "0"
        For Each item As MSPExMappingtoFSKind In GetMaxPM(oMSPExType) 'Max PM dari MSPExMappingtoFSKind berdasarkan MSPExType
            If FSKindID.Length = 0 Then
                FSKindID = item.FSKind.ID
            Else
                FSKindID = FSKindID & "," & item.FSKind.ID 'Diloop untuk ambil ID FSKind dari MSPExMappingtoFSKind (ex: 1,2,3,4)
            End If
        Next
        Dim critFS As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critFS.opAnd(New Criteria(GetType(FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisNo.Text.Trim))
        critFS.opAnd(New Criteria(GetType(FreeService), "FSKind.ID", MatchType.InSet, "(" & FSKindID & ")")) 'Hasil dari loop dimasukin kesini
        Dim arrFS As ArrayList = New FreeServiceFacade(User).Retrieve(critFS)

        Dim crit1 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExRegistration), "RowStatus", MatchType.Exact, 0))
        crit1.opAnd(New Criteria(GetType(MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisNo.Text.Trim))
        Dim arrMSPExReg As ArrayList = New MSPExRegistrationFacade(User).Retrieve(crit1)
        Dim lastMSP As MSPExRegistration
        If arrMSPExReg.Count > 0 Then
            lastMSP = CType(arrMSPExReg(arrMSPExReg.Count - 1), MSPExRegistration)
        End If

        Dim cnt As Integer = (From a As FreeService In arrFS Where a.CreatedTime >= lastMSP.CreatedTime Select a).Count
        Return cnt
    End Function

    Private Function GetMaxPM(ByVal oMSPExType As MSPExType) As ArrayList
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExMappingtoFSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(MSPExMappingtoFSKind), "MSPExType.ID", MatchType.Exact, oMSPExType.ID))
        Return New MSPExMappingtoFSKindFacade(User).Retrieve(crit)
    End Function

    Private Function sSuffix() As String
        Return DateTime.Now.ToString("yyyyMMddHHmmss")
    End Function

    Private Sub SendWSM(ByVal result As Integer)
        Dim oReg As MSPExRegistration = New MSPExRegistrationFacade(User).Retrieve(result)

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer) 'Just TEST

        Dim success As Boolean = False
        Dim sTimestamp As String = sSuffix()
        Dim FileNameSAP As String = KTB.DNet.Lib.WebConfig.GetValue("SAPSERVERFOLDER") & "\Service\MSPEXT\Registration\MSPExtRegistration" & sTimestamp & ".txt"
        Dim FileNameLocal As String = Server.MapPath("") & "\..\DataTemp\MSPExtRegistration" & sTimestamp & ".txt"

        Try
            success = imp.Start
            If success Then
                Dim CheckedWSCItemColl As ArrayList = New ArrayList

                Dim nSavedData As Integer = AppendText(oReg, FileNameLocal, FileNameSAP)
                If nSavedData < 1 Then
                    Dim sIndicator As String = ""
                    sIndicator = IIf(nSavedData = -1, ".", IIf(nSavedData = -1, ",", ""))
                    MessageBox.Show("Rilis data gagal" & sIndicator)
                End If

                oReg.Status = EnumMSPEx.MSPExStatus.Validasi
                oReg.IsTransfertoSAP = 1
                Dim nResult = New MSPExRegistrationFacade(User).Update(oReg)
                If nResult = 0 Then
                    MessageBox.Show("Send To SAP Sukses")
                Else
                    MessageBox.Show("Send To SAP gagal")
                End If

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Send To SAP gagal !")
        End Try
    End Sub

    Private Function AppendText(ByVal oReg As MSPExRegistration, ByVal FileNameLocal As String, ByVal filename As String) As Integer ' Number of data sent to SAP
        Dim strText As New StringBuilder
        Dim objAl As New ArrayList
        Dim nData As Integer = 1
        Dim delimiter As String = ";"
        Dim sMessage As String = ""
        'Format:
        'RegistrationNumber;RegistrationDate;ChassisNumber;CustomerName;Age;Address;PhoneNumber;IdentityNumber;Kelurahan;Kecamatan;Provinsi;Kabupaten;Email;DealerCode;MSPExTypeCode;Duration;MSPExKM;LastKM;ExpiredDate;Amount

        Try
            strText = New StringBuilder
            strText.Append(oReg.RegNumber)
            strText.Append(delimiter)
            strText.Append(Format(oReg.CreatedTime, "ddMMyyyy"))
            strText.Append(delimiter)
            strText.Append(oReg.ChassisMaster.ChassisNumber)
            strText.Append(delimiter)
            strText.Append(oReg.MSPCustomer.Name1)
            strText.Append(delimiter)
            strText.Append(oReg.MSPCustomer.Age)
            strText.Append(delimiter)
            strText.Append(oReg.MSPCustomer.Alamat)
            strText.Append(delimiter)
            strText.Append(oReg.MSPCustomer.PhoneNo)
            strText.Append(delimiter)
            strText.Append(oReg.MSPCustomer.KTPNo)
            strText.Append(delimiter)
            strText.Append(oReg.MSPCustomer.Kelurahan)
            strText.Append(delimiter)
            strText.Append(oReg.MSPCustomer.Kecamatan)
            strText.Append(delimiter)
            If IsNothing(oReg.MSPCustomer.Province) Then
                strText.Append("")
            Else
                strText.Append(oReg.MSPCustomer.Province.ProvinceName)
            End If
            strText.Append(delimiter)
            If IsNothing(oReg.MSPCustomer.City) Then
                strText.Append("")
            Else
                strText.Append(oReg.MSPCustomer.City.CityName)
            End If
            strText.Append(delimiter)
            strText.Append(oReg.MSPCustomer.Email)
            strText.Append(delimiter)
            strText.Append(oReg.Dealer.DealerCode)
            strText.Append(delimiter)
            strText.Append(oReg.MSPExMaster.MSPExType.Code)
            strText.Append(delimiter)
            strText.Append(oReg.MSPExMaster.Duration)
            strText.Append(delimiter)
            strText.Append(oReg.MSPExMaster.MSPExKM)
            strText.Append(delimiter)
            strText.Append(oReg.ValidKMTo)
            strText.Append(delimiter)
            strText.Append(Format(oReg.ValidDateTo, "ddMMyyyy"))
            strText.Append(delimiter)
            strText.Append(CDbl(oReg.MSPExMaster.Amount))
            strText.Append(delimiter)
            Dim oAppConfig As AppConfig = New AppConfigFacade(User).Retrieve("TermOfPaymentMSPEx")
            If Not IsNothing(oAppConfig) Then
                strText.Append(oAppConfig.Value)
            Else
                strText.Append("")
            End If
            strText.Append(vbNewLine)

            If Not SaveToSAP(FileNameLocal, filename, strText) Then
                nData = -2
            End If
        Catch ex As Exception
            nData = -1 ' -1 means error occured
        End Try

        Return nData
    End Function

    Private Sub SendWSMPart2(ByVal result As Integer)
        Dim oReg As MSPExRegistration = New MSPExRegistrationFacade(User).Retrieve(result)

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer) 'Just TEST

        Dim success As Boolean = False
        Dim sTimestamp As String = sSuffix()
        Dim FileNameSAP As String = KTB.DNet.Lib.WebConfig.GetValue("SAPSERVERFOLDER") & "\Service\MSPEXT\Registration\MSPExtRegistrationPart2" & sTimestamp & ".txt"
        Dim FileNameLocal As String = Server.MapPath("") & "\..\DataTemp\MSPExtRegistrationPart2" & sTimestamp & ".txt"

        Try
            success = imp.Start
            If success Then
                Dim CheckedWSCItemColl As ArrayList = New ArrayList

                Dim nSavedData As Integer = AppendTextPart2(oReg, FileNameLocal, FileNameSAP)
                If nSavedData < 1 Then
                    Dim sIndicator As String = ""
                    sIndicator = IIf(nSavedData = -1, ".", IIf(nSavedData = -1, ",", ""))
                    MessageBox.Show("Rilis data gagal" & sIndicator)
                End If

                'oReg.Status = EnumMSPEx.MSPExStatus.Validasi
                'oReg.IsTransfertoSAP = 1
                'Dim nResult = New MSPExRegistrationFacade(User).Update(oReg)
                'If nResult = 0 Then
                MessageBox.Show("Send To SAP Sukses")
                'Else
                '    MessageBox.Show("Send To SAP gagal")
                'End If

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Send To SAP gagal !")
        End Try
    End Sub

    Private Function AppendTextPart2(ByVal oReg As MSPExRegistration, ByVal FileNameLocal As String, ByVal filename As String) As Integer ' Number of data sent to SAP
        Dim strText As New StringBuilder
        Dim objAl As New ArrayList
        Dim nData As Integer = 1
        Dim delimiter As String = ";"
        Dim sMessage As String = ""
        'Format:
        'K;MSPEXRegistration_[TimeStamp]\nH;DealerID;MSPCustomerID;ChassisMasterID;MileAge;MSPExMasterID;RegNumber;ValidDateTo[ddmmyyyy];ValidKMTo;Status;IsTransfertoSAP;WarrantyValidDateTo[ddmmyyyy];WarrantyValidKMTo;RowStatus;'

        Try
            strText = New StringBuilder
            strText.Append(oReg.Dealer.ID)
            strText.Append(delimiter)
            strText.Append(oReg.MSPCustomer.ID)
            strText.Append(delimiter)
            strText.Append(oReg.ChassisMaster.ID)
            strText.Append(delimiter)
            strText.Append(oReg.MileAge)
            strText.Append(delimiter)
            strText.Append(oReg.MSPExMaster.ID)
            strText.Append(delimiter)
            strText.Append(oReg.RegNumber)
            strText.Append(delimiter)
            strText.Append(Format(oReg.ValidDateTo, "ddMMyyyy"))
            strText.Append(delimiter)
            strText.Append(oReg.ValidKMTo)
            strText.Append(delimiter)
            strText.Append(oReg.Status)
            strText.Append(delimiter)
            strText.Append(oReg.IsTransfertoSAP)
            strText.Append(delimiter)
            strText.Append(Format(oReg.WarrantyValidDateTo, "ddMMyyyy"))
            strText.Append(delimiter)
            strText.Append(oReg.WarrantyValidKMTo)
            strText.Append(delimiter)
            strText.Append(oReg.RowStatus)
            strText.Append(vbNewLine)

            If Not SaveToSAP(FileNameLocal, filename, strText) Then
                nData = -2
            End If
        Catch ex As Exception
            nData = -1 ' -1 means error occured
        End Try

        Return nData
    End Function

    Private Function SaveToSAP(ByVal DestFileLocal As String, ByVal DestFile As String, ByRef sb As StringBuilder) As Boolean
        Dim success As Boolean = False
        Dim sw As StreamWriter
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim fInfoLocal As New FileInfo(DestFile)
        Dim finfo As New FileInfo(DestFile)
        Try
            success = imp.Start()
            If success Then
                'Local
                If Not fInfoLocal.Directory.Exists Then Directory.CreateDirectory(fInfoLocal.DirectoryName)
                If fInfoLocal.Exists() Then fInfoLocal.Delete()
                Dim fs As FileStream = New FileStream(DestFileLocal, FileMode.CreateNew)
                sw = New StreamWriter(fs)
                sw.Write(sb.ToString)
                sw.Close()
                fs.Close()

                'Server
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If
                If finfo.Exists Then
                    finfo.Delete()
                End If
                System.IO.File.Copy(DestFileLocal, DestFile)
                'System.IO.File.Copy(DestFileLocal, DestFile & ".wts")
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            success = False
            sw.Close()
        End Try
        Return success
    End Function

    Private Sub InsertStatusChangeHistory(ByVal regNumber As String, ByVal newStatus As String, ByVal oldStatus As String)
        Try
            Dim objNewStatus As New StatusChangeHistory
            objNewStatus.DocumentType = 1
            objNewStatus.DocumentRegNumber = regNumber
            objNewStatus.OldStatus = CInt(oldStatus)
            objNewStatus.NewStatus = CInt(newStatus)
            objNewStatus.RowStatus = 0

            Dim iReturn As Integer = New StatusChangeHistoryFacade(User).Insert(objNewStatus)
        Catch ex As Exception
            Throw New Exception("Gagal dalam menginput history")
        End Try
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        ddlTipeMSP_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Function ProcessOverDue() As Boolean
        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(MSPExRegistration), "Dealer.DealerCode", MatchType.Exact, objLoginDealer.DealerCode))
        crits.opAnd(New Criteria(GetType(MSPExRegistration), "Status", MatchType.Exact, CType(EnumMSPEx.MSPExStatus.Baru, Short)))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(MSPExRegistration), "ID", Sort.SortDirection.DESC))
        Dim arrMSPEx As ArrayList = New MSPExRegistrationFacade(User).Retrieve(crits, sortColl)
        If arrMSPEx.Count > 0 Then
            Dim oMSPex As MSPExRegistration = CType(arrMSPEx(0), MSPExRegistration)
            If CInt(Date.Now.Subtract(oMSPex.CreatedTime.Date).Days) > 3 Then
                Return True
            End If
        End If
        Return False
    End Function
    'add 07012021 by irfan
    Private Function ProcessOverDueDealer() As Boolean
        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(MSPExRegistration), "Dealer.DealerCode", MatchType.Exact, objLoginDealer.DealerCode))
        crits.opAnd(New Criteria(GetType(MSPExRegistration), "Status", MatchType.Exact, CType(EnumMSPEx.MSPExStatus.Baru, Short)))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(MSPExRegistration), "ID", Sort.SortDirection.DESC))
        Dim arrMSPEx As ArrayList = New MSPExRegistrationFacade(User).Retrieve(crits, sortColl)
        If arrMSPEx.Count > 0 Then
            Dim oMSPex As MSPExRegistration = CType(arrMSPEx(0), MSPExRegistration)
            If CInt(Date.Now.Subtract(oMSPex.CreatedTime.Date).Days) > 1 Then
                Return True
            End If
        End If
        Return False
    End Function

    Private Sub getWarrantyData(ByVal oMSPExMaster As MSPExMaster, ByRef returnDate As Date, ByRef returnKM As Integer)
        Dim MSPHist As MSPRegistrationHistory = DataMSPLama()
        Dim MSPExWarr As MSPExWarrantyMaster = New MSPExWarrantyMasterFacade(User).Retrieve(oMSPExMaster.MSPExType, Date.Now.Date, Date.Now.Date)

        Dim MSPEX As MSPExRegistration
        If Mode = "Edit" Then
            MSPEX = New MSPExRegistrationFacade(User).RetrieveByCNumberAndNotRNumber(txtChassisNo.Text.Trim, lblMSPNo.Text.Trim)
        Else
            MSPEX = New MSPExRegistrationFacade(User).RetrieveByChassisNumber(txtChassisNo.Text.Trim)
        End If

        Dim odometer As Integer = 0
        If txtOdo.Text.Trim <> "" Then
            odometer = CInt(txtOdo.Text)
        End If

        If Not IsMspProgramFleet() Then
            If MSPEX.ID <> 0 Then
                Dim PMCount As Integer = GetPMCount(MSPEX.MSPExMaster.MSPExType)
                Dim MaxPM As Integer = GetMaxPM(MSPEX.MSPExMaster.MSPExType).Count
                'If Not MSPEX.ValidDateTo.Date >= Date.Now _
                '    AndAlso Not MSPEX.ValidKMTo >= odometer _
                '    AndAlso Not PMCount < MaxPM Then
                returnDate = DateAdd(DateInterval.Year, MSPExWarr.Duration, Date.Now.Date)
                returnKM = odometer + MSPExWarr.KM
                'End If
            Else
                If MSPHist.ID > 0 Then
                    Dim expiredDateMSPLama As Date = DateAdd(DateInterval.Year, MSPHist.MSPMaster.Duration, MSPHist.RegistrationDate)
                    If expiredDateMSPLama >= Date.Now AndAlso MSPHist.MSPMaster.MSPKm >= odometer Then
                        'kalau ad datanya dan masih aktif
                        'Maksimum tgl warranty = valid date to MSP lama + duration MSP Ex warranty
                        returnDate = DateAdd(DateInterval.Year, MSPExWarr.Duration, expiredDateMSPLama)
                        returnKM = MSPHist.MSPMaster.MSPKm + MSPExWarr.KM
                    Else
                        'kalau ada datanya dan tdk aktif
                        'Maksimum tgl warranty = tgl registrasi + duration MSP Ex warranty
                        returnDate = DateAdd(DateInterval.Year, MSPExWarr.Duration, Date.Now.Date)
                        returnKM = odometer + MSPExWarr.KM
                    End If
                Else
                    'kalau data tidak ditemukan
                    'Maksimum tgl warranty = tgl registrasi + duration MSP Ex warranty
                    returnDate = DateAdd(DateInterval.Year, MSPExWarr.Duration, Date.Now.Date)
                    returnKM = odometer + MSPExWarr.KM
                End If
            End If
        Else
            returnDate = DateAdd(DateInterval.Year, MSPExWarr.Duration, Date.Now)
            returnKM = MSPExWarr.KM
        End If

        If Load1st AndAlso Mode = "View" Then
            returnDate = MSPEX.WarrantyValidDateTo
            returnKM = MSPEX.WarrantyValidKMTo
        End If
    End Sub

End Class