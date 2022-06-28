Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNET.Security
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net.Mail
Imports System.Text
Imports KTB.DNet.BusinessValidation.Helpers

Public Class FrmMSPRegistration
    Inherits System.Web.UI.Page
    Private _view As Boolean = False
    Private _input As Boolean = False
    Private _edit As Boolean = False
    Private _sessHelper As New SessionHelper
    Private _strSessSearch As String = "CRITERIAS"
    Private _strSessMSPRegistrationHistoryID As String = "MSPRegistrationHistoryID"
    Private _strSessStatusInput As String = "StatusInput"
    Private objLoginDealer As Dealer
    Private objUserInfo As UserInfo
    Private objMSPRegistration As MSPRegistration
    Private objMSPRegistrationHistory As MSPRegistrationHistory
    Private objMSPCustomer As MSPCustomer
    Private mailAddress As MailAddress
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)
    Dim crt As CriteriaComposite
    Dim arr As ArrayList
    Dim sorts As SortCollection
    Dim strMsg As String = String.Empty
    Dim strMSPMasterID As String = String.Empty
    Dim oldStatusHistory As Integer = -1

    Private Sub CheckPrivilege()
        _view = SecurityProvider.Authorize(Context.User, SR.MSPRegistration_view_privilege)
        _input = SecurityProvider.Authorize(Context.User, SR.MSPRegistration_input_dealer_Privilege)
        _edit = SecurityProvider.Authorize(Context.User, SR.MSPRegistration_edit_privilege)
        If Not _view Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=MSP - Registrasi MSP")
        End If
        ' dealer bisa input dan edit, mks hanya bisa edit
        btnSave.Visible = _input
        btnSave.Visible = _edit
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        objLoginDealer = CType(_sessHelper.GetSession("DEALER"), Dealer)
        objUserInfo = CType(_sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If Not IsPostBack Then
            FillForm()
        End If
    End Sub

    Private Sub SetHeaderData()
        lblDealer.Text = objLoginDealer.DealerCode & "/" & objLoginDealer.SearchTerm1
        lblSubmitBy.Text = objLoginDealer.DealerCode & "-" & objUserInfo.UserName
        lblCurrentDate.Text = Date.Now.ToString("dd/MM/yyyy")
        lblStatus.Text = EnumStatusMSP.Status.Baru.ToString()
    End Sub

    Private Sub FillForm()
        BindDropDown()
        SetHeaderData()
        Dim mspRegistrationHistoryID As Integer = _sessHelper.GetSession(_strSessMSPRegistrationHistoryID)
        ' get edit atau update
        If Convert.ToString(_sessHelper.GetSession(_strSessStatusInput)).ToUpper = "UPDATE" Then
            SetDataToForm(mspRegistrationHistoryID, False)
            MessageBox.Show("Perubahan Type dan Durasi MSP akan mengikuti harga terbaru.")
        ElseIf Convert.ToString(_sessHelper.GetSession(_strSessStatusInput)) = "VIEW" Then
            SetDataToForm(mspRegistrationHistoryID)
            If objLoginDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                'btnNew.Visible = False
            End If
            btnSave.Visible = False
            lbtnRefCustomerCode.Visible = False
            lnkReloadCustomer.Visible = False
            'ali
            ' _sessHelper.RemoveSession(_strSessMSPRegistrationHistoryID)
        End If
    End Sub

    Private Sub SetDataToForm(ByVal MSPRegistrationHistoryID As Integer, Optional ByVal isView As Boolean = True)
        objMSPRegistrationHistory = New MSPRegistrationHistory
        objMSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(MSPRegistrationHistoryID)

        If Not IsNothing(objMSPRegistrationHistory) Then
            If Not IsNothing(objMSPRegistrationHistory.MSPRegistration) Then
                If Not IsNothing(objMSPRegistrationHistory.MSPRegistration.MSPCustomer) Then
                    objMSPRegistration = objMSPRegistrationHistory.MSPRegistration
                    objMSPCustomer = objMSPRegistration.MSPCustomer

                    'set header data
                    lblDealer.Text = objMSPRegistrationHistory.MSPRegistration.Dealer.DealerCode & "/" & objMSPRegistrationHistory.MSPRegistration.Dealer.SearchTerm1
                    lblSubmitBy.Text = objMSPRegistrationHistory.MSPRegistration.Dealer.DealerCode & "-" & objMSPRegistrationHistory.MSPRegistration.CreatedBy
                    lblCurrentDate.Text = objMSPRegistrationHistory.RegistrationDate.ToString("dd/MM/yyyy")
                    lblStatus.Text = CType(objMSPRegistrationHistory.Status, EnumStatusMSP.Status).ToString
                    lblMSPNo.Text = objMSPRegistration.MSPCode

                    If objLoginDealer.Title = EnumDealerTittle.DealerTittle.DEALER And objLoginDealer.ID = objMSPRegistration.Dealer.ID Then
                        If lblStatus.Text = "Baru" Or lblStatus.Text = "Batal_Validasi" Then
                            btnValidasi.Visible = True
                        End If
                    Else
                        If lblStatus.Text = "Validasi" Then
                            btnConfirm.Visible = True
                        End If
                    End If

                    Dim ppnVal As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(objMSPRegistrationHistory.RegistrationDate.Date, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

                    If isView Then
                        SetVisibleView()

                        lblOldMSPNo.Text = objMSPRegistration.OldMSPCode
                        lblKTPNo.Text = objMSPCustomer.KTPNo
                        lblName.Text = objMSPCustomer.Name1
                        lblAge.Text = objMSPCustomer.Age
                        lblAddress.Text = objMSPCustomer.Alamat
                        lblKelurahan.Text = objMSPCustomer.Kelurahan
                        lblKecamatan.Text = objMSPCustomer.Kecamatan
                        If Not IsNothing(objMSPCustomer.Province) AndAlso objMSPCustomer.Province.ID > 0 Then
                            lblPropinsi.Text = objMSPCustomer.Province.ProvinceName
                        End If

                        If Not IsNothing(objMSPCustomer.City) AndAlso objMSPCustomer.City.ID > 0 Then
                            lblKota.Text = objMSPCustomer.City.CityName
                        End If

                        lblPreArea.Text = objMSPCustomer.PreArea

                        lblNotlp.Text = objMSPCustomer.PhoneNo
                        lblEmail.Text = objMSPCustomer.Email


                        If Not IsNothing(objMSPRegistration.ChassisMaster) AndAlso Not IsNothing(objMSPRegistration.ChassisMaster.EndCustomer) Then
                            lblChassisNumber.Text = objMSPRegistration.ChassisMaster.ChassisNumber

                            lblVehicleType.Text = objMSPRegistration.ChassisMaster.VechileType
                            lblTglPKT.Text = objMSPRegistration.ChassisMaster.EndCustomer.OpenFakturDate.ToString("dd/MM/yyyy")
                            lblValidUntil.Text = objMSPRegistration.ChassisMaster.EndCustomer.OpenFakturDate.AddYears(objMSPRegistrationHistory.MSPMaster.Duration).ToString("dd/MM/yyyy")

                            'If Not IsNothing(objMSPRegistration.ChassisMaster.ChassisMasterPKT) Then
                            '    lblTglPKT.Text = objMSPRegistration.ChassisMaster.ChassisMasterPKT.PKTDate

                            '    lblValidUntil.Text = objMSPRegistration.ChassisMaster.ChassisMasterPKT.PKTDate.AddYears(objMSPRegistrationHistory.MSPMaster.Duration).ToString("dd/MM/yyyy")

                            '    'objMSPRegistration.ChassisMaster.EndCustomer.HandoverDate.AddYears(objMSPRegistrationHistory.MSPMaster.Duration).ToString("dd/MM/yyyy")
                            'End If

                            lblEngineNumber.Text = objMSPRegistration.ChassisMaster.EngineNumber

                        End If
                        lblRefCustomerCode.Text = If(IsNothing(objMSPCustomer.RefCustomer), "", objMSPCustomer.RefCustomer.Code)
                        lblMSPType.Text = objMSPRegistrationHistory.MSPMaster.MSPType.Description
                        lblDuration.Text = objMSPRegistrationHistory.MSPMaster.Duration.ToString & "Thn - " & (String.Format("{0:#,##0}", CDec(objMSPRegistrationHistory.MSPMaster.MSPKm))).ToString & " KM"
                        'lblAmountMSP.Text = If(Not IsNothing(objMSPRegistrationHistory.MSPMaster), (objMSPRegistrationHistory.MSPMaster.Amount * 1.1).ToString("C"), String.Empty)
                        lblAmountMSP.Text = If(Not IsNothing(objMSPRegistrationHistory.MSPMaster), (objMSPRegistrationHistory.MSPMaster.Amount + CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnVal, dpp:=objMSPRegistrationHistory.MSPMaster.Amount)).ToString("C"), String.Empty)

                        lblSoldBy.Text = objMSPRegistrationHistory.SoldBy
                    Else
                        SetVisibleView(False)

                        txtOldMSPNo.Text = objMSPRegistration.OldMSPCode
                        txtKTPNo.Text = objMSPCustomer.KTPNo
                        txtName.Text = objMSPCustomer.Name1
                        txtAge.Text = objMSPCustomer.Age
                        txtAddress.Text = objMSPCustomer.Alamat
                        txtKelurahan.Text = objMSPCustomer.Kelurahan
                        txtKecamatan.Text = objMSPCustomer.Kecamatan
                        If Not IsNothing(objMSPCustomer.Province) Then
                            ddlPropinsi.SelectedValue = objMSPCustomer.Province.ID
                        End If

                        ddlPropinsi_SelectedIndexChanged(Me, System.EventArgs.Empty)
                        Try
                            ddlPreArea.SelectedValue = objMSPCustomer.PreArea
                        Catch ex As Exception

                        End Try
                        Try
                            ddlKota.SelectedValue = objMSPCustomer.City.ID
                        Catch ex As Exception

                        End Try

                        txtNotlp.Text = objMSPCustomer.PhoneNo
                        txtEmail.Text = objMSPCustomer.Email
                        txtRefCustomerCode.Text = If(IsNothing(objMSPCustomer.RefCustomer), "", objMSPCustomer.RefCustomer.Code)
                        ddlSoldBy.SelectedValue = objMSPRegistrationHistory.SoldBy
                        'lblAmountMSP.Text = (objMSPRegistrationHistory.MSPMaster.Amount * 1.1).ToString("C")
                        lblAmountMSP.Text = (objMSPRegistrationHistory.MSPMaster.Amount + CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnVal, dpp:=objMSPRegistrationHistory.MSPMaster.Amount)).ToString("C")
                        If Not IsNothing(objMSPRegistration.ChassisMaster) AndAlso Not IsNothing(objMSPRegistration.ChassisMaster.EndCustomer) Then
                            txtChassisNumber.Text = objMSPRegistration.ChassisMaster.ChassisNumber

                            lblVehicleType.Text = objMSPRegistration.ChassisMaster.VechileType

                            lblTglPKT.Text = objMSPRegistration.ChassisMaster.EndCustomer.OpenFakturDate.ToString("dd/MM/yyyy")
                            lblValidUntil.Text = objMSPRegistration.ChassisMaster.EndCustomer.OpenFakturDate.AddYears(objMSPRegistrationHistory.MSPMaster.Duration).ToString("dd/MM/yyyy")


                            'If Not IsNothing(objMSPRegistration.ChassisMaster.ChassisMasterPKT) Then
                            '    lblTglPKT.Text = objMSPRegistration.ChassisMaster.ChassisMasterPKT.PKTDate.ToString("dd/MM/yyyy")
                            '    '  lblTglPKT.Text =  objMSPRegistration.ChassisMaster.EndCustomer.HandoverDate.ToString("dd/MM/yyyy")
                            '    lblValidUntil.Text = objMSPRegistration.ChassisMaster.ChassisMasterPKT.PKTDate.AddYears(objMSPRegistrationHistory.MSPMaster.Duration).ToString("dd/MM/yyyy")

                            '    'lblValidUntil.Text = objMSPRegistration.ChassisMaster.EndCustomer.HandoverDate.AddYears(objMSPRegistrationHistory.MSPMaster.Duration).ToString("dd/MM/yyyy")

                            'End If

                            lblEngineNumber.Text = objMSPRegistration.ChassisMaster.EngineNumber

                        End If

                        If objLoginDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                            txtOldMSPNo.ReadOnly = True
                            txtKTPNo.ReadOnly = True
                            txtName.ReadOnly = True
                            txtAge.ReadOnly = True
                            txtAddress.ReadOnly = True
                            txtKelurahan.ReadOnly = True
                            txtKecamatan.ReadOnly = True
                            txtNotlp.ReadOnly = True
                            txtEmail.ReadOnly = True
                            txtRefCustomerCode.ReadOnly = True

                            ddlPropinsi.Visible = False
                            lblPropinsi.Visible = True
                            If Not IsNothing(objMSPCustomer.Province) Then
                                lblPropinsi.Text = objMSPCustomer.Province.ProvinceName
                            End If


                            ddlPreArea.Visible = False
                            lblPreArea.Visible = True
                            lblPreArea.Text = objMSPCustomer.PreArea

                            ddlKota.Visible = False
                            lblKota.Visible = True
                            If Not IsNothing(objMSPCustomer.City) Then
                                lblKota.Text = objMSPCustomer.City.CityName
                            End If


                            ddlSoldBy.Visible = False
                            lblSoldBy.Visible = True
                            lblSoldBy.Text = objMSPRegistrationHistory.SoldBy
                            'Else
                            '    txtChassisNumber_TextChanged(Me, EventArgs.Empty)
                            '    ddlMSPType.SelectedValue = objMSPRegistrationHistory.MSPMaster.MSPType.ID.ToString
                            '    ddlMSPType_SelectedIndexChanged(Me, EventArgs.Empty)

                            '    ddlDuration.SelectedValue = objMSPRegistrationHistory.MSPMaster.ID.ToString
                            '    ddlDuration_SelectedIndexChanged(Me, EventArgs.Empty)
                        End If

                        lblMSPType.Visible = True
                        ddlMSPType.Visible = False
                        lblDuration.Visible = True
                        ddlDuration.Visible = False
                        lblMSPType.Text = If(Not IsNothing(objMSPRegistrationHistory.MSPMaster), objMSPRegistrationHistory.MSPMaster.MSPType.Description, String.Empty)
                        lblDuration.Text = If(Not IsNothing(objMSPRegistrationHistory.MSPMaster), objMSPRegistrationHistory.MSPMaster.Duration.ToString & " Thn - " & (String.Format("{0:#,##0}", CDec(objMSPRegistrationHistory.MSPMaster.MSPKm))).ToString & " KM", String.Empty)

                        If objLoginDealer.ID <> objMSPRegistrationHistory.MSPRegistration.Dealer.ID Then
                            If objLoginDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                                btnSave.Enabled = False
                            End If
                            btnValidasi.Enabled = False
                        End If

                    End If
                Else
                    strMsg += "\n" & "Data registrasi tidak memiliki data konsumen."
                End If
            Else
                strMsg += "\n" & "Histori tidak memiliki parent."
            End If
        End If


    End Sub

    Private Sub SetVisibleView(Optional ByVal isView As Boolean = True)
        Dim isInputOrEdit As Boolean = Not isView

        lblOldMSPNo.Visible = isView
        txtOldMSPNo.Visible = isInputOrEdit

        lblKTPNo.Visible = isView
        txtKTPNo.Visible = isInputOrEdit

        lblName.Visible = isView
        txtName.Visible = isInputOrEdit

        lblAge.Visible = isView
        txtAge.Visible = isInputOrEdit

        lblAddress.Visible = isView
        txtAddress.Visible = isInputOrEdit

        lblKelurahan.Visible = isView
        txtKelurahan.Visible = isInputOrEdit

        lblKecamatan.Visible = isView
        txtKecamatan.Visible = isInputOrEdit

        lblPropinsi.Visible = isView
        ddlPropinsi.Visible = isInputOrEdit

        lblPreArea.Visible = isView
        ddlPreArea.Visible = isInputOrEdit

        lblKota.Visible = isView
        ddlKota.Visible = isInputOrEdit

        lblNotlp.Visible = isView
        txtNotlp.Visible = isInputOrEdit

        lblEmail.Visible = isView
        txtEmail.Visible = isInputOrEdit

        lblChassisNumber.Visible = isView
        txtChassisNumber.Visible = isInputOrEdit

        lblRefCustomerCode.Visible = isView
        txtRefCustomerCode.Visible = isInputOrEdit

        lblMSPType.Visible = isView
        ddlMSPType.Visible = isInputOrEdit

        lblDuration.Visible = isView
        ddlDuration.Visible = isInputOrEdit

        lblSoldBy.Visible = isView
        ddlSoldBy.Visible = isInputOrEdit
    End Sub

    Private Sub BindDropDown()
        'dropdown propinsi
        crt = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        arr = New ProvinceFacade(User).RetrieveActiveList(crt, "ProvinceName", Sort.SortDirection.ASC)

        ddlPropinsi.Items.Clear()
        ddlPropinsi.DataSource = arr
        ddlPropinsi.DataTextField = "ProvinceName"
        ddlPropinsi.DataValueField = "ID"
        ddlPropinsi.DataBind()
        ddlPropinsi.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlPropinsi.SelectedIndex = 0
        ddlPropinsi_SelectedIndexChanged(Me, System.EventArgs.Empty)

    End Sub

    Private Sub ddlPropinsi_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlPropinsi.SelectedIndexChanged
        ddlKota.Items.Clear()
        If ddlPropinsi.SelectedIndex <> 0 Then
            crt = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
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

    Protected Sub txtChassisNumber_TextChanged(sender As Object, e As EventArgs) Handles txtChassisNumber.TextChanged
        lblMSPType.Visible = False
        ddlMSPType.Visible = True
        ddlDuration.Visible = True
        lblDuration.Visible = False
        Dim strValidateChassis As String = ValidateChassisNumber(txtChassisNumber.Text)
        If strValidateChassis = String.Empty Then
            Dim dtSet As DataSet = New MSPRegistrationFacade(User).RetrieveSp("EXEC sp_MSP_GetMSPMasterList '" + txtChassisNumber.Text + "'")
            Dim dtTbl As DataTable = dtSet.Tables(0)
            If dtTbl.Rows.Count > 0 Then

                If (Convert.ToDateTime(dtTbl.Rows(0)("PKTDate")) = CType("1753-01-01 00:00:00.000", DateTime)) Then
                    MessageBox.Show("Chassis Number belum Buka Faktur. Silahkan melakukan pengajuan di Faktur kendaraan")
                    txtChassisNumber.Text = String.Empty
                    Return
                End If

                For Each row As DataRow In dtTbl.Rows
                    strMSPMasterID += "," & row("MSPMasterID").ToString
                Next
                ' set type kendaraan
                lblVehicleType.Text = dtTbl.Rows(0)("VehicleTypeCode").ToString
                ' set engine number
                lblEngineNumber.Text = dtTbl.Rows(0)("EngineNumber").ToString
                ' set tanggal PKT
                lblTglPKT.Text = Convert.ToDateTime(dtTbl.Rows(0)("PKTDate")).ToString("dd/MM/yyyy")
                ' set dropdown Type MSP
                crt = New CriteriaComposite(New Criteria(GetType(MSPMaster), "ID", MatchType.InSet, "(" + strMSPMasterID.Substring(1, strMSPMasterID.Length - 1) + ")"))
                crt.opAnd(New Criteria(GetType(MSPMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                arr = New MSPMasterFacade(User).Retrieve(crt)

                Dim newArrObjMSPMaster = From a As MSPMaster In arr
                                         Group By a.MSPType.ID, a.MSPType.Description Into Group
                                    Select ID, Description

                ddlMSPType.Items.Clear()
                ddlMSPType.DataSource = newArrObjMSPMaster
                ddlMSPType.DataTextField = "Description"
                ddlMSPType.DataValueField = "ID"
                ddlMSPType.DataBind()
                ddlMSPType.Items.Insert(0, New ListItem("Silahkan Pilih", 0))
                ddlMSPType.SelectedIndex = 0
                ddlMSPType_SelectedIndexChanged(Me, System.EventArgs.Empty)
            Else
                lblVehicleType.Text = String.Empty
                lblEngineNumber.Text = String.Empty
                lblTglPKT.Text = String.Empty
                txtChassisNumber.Text = String.Empty
                ddlMSPType.Items.Clear()
                ddlMSPType_SelectedIndexChanged(Me, System.EventArgs.Empty)
                MessageBox.Show("Tidak ada MSP Master yang terhubung dengan Chassis Number " + txtChassisNumber.Text)
            End If
        Else
            MessageBox.Show(strValidateChassis.Substring(2, strValidateChassis.Length - 2))
        End If

    End Sub

    Private Sub ddlMSPType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMSPType.SelectedIndexChanged
        ddlDuration.Items.Clear()
        ddlDuration_SelectedIndexChanged(Me, System.EventArgs.Empty)
        If ddlMSPType.SelectedIndex > 0 Then
            Dim dtSet As DataSet = New MSPRegistrationFacade(User).RetrieveSp("EXEC sp_MSP_GetMSPMasterList '" + txtChassisNumber.Text + "'")
            Dim dtTbl As DataTable = dtSet.Tables(0)
            If dtTbl.Rows.Count > 0 Then
                For Each row As DataRow In dtTbl.Rows
                    strMSPMasterID += "," & row("MSPMasterID").ToString
                Next

                crt = New CriteriaComposite(New Criteria(GetType(MSPMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crt.opAnd(New Criteria(GetType(MSPMaster), "MSPType", MatchType.Exact, ddlMSPType.SelectedValue))
                crt.opAnd(New Criteria(GetType(MSPMaster), "ID", MatchType.InSet, "(" + strMSPMasterID.Substring(1, strMSPMasterID.Length - 1) + ")"))
                sorts = New SortCollection
                sorts.Add(New Sort(GetType(MSPMaster), "Duration", Sort.SortDirection.ASC))
                sorts.Add(New Sort(GetType(MSPMaster), "Amount", Sort.SortDirection.ASC))
                arr = New MSPMasterFacade(User).Retrieve(crt, sorts)
                Dim newArr = From a As MSPMaster In arr
                                          Select New With {.ID = a.ID, .DurationAmount = a.Duration.ToString + " Thn - " + String.Format("{0:#,##0}", Convert.ToDouble(a.MSPKm)) + " KM"}
                ddlDuration.Items.Clear()
                ddlDuration.DataSource = newArr
                ddlDuration.DataTextField = "DurationAmount"
                ddlDuration.DataValueField = "ID"
                ddlDuration.DataBind()
                ddlDuration.Items.Insert(0, New ListItem("Silahkan Pilih", 0))
                ddlDuration.SelectedIndex = 0
                ddlDuration_SelectedIndexChanged(Me, System.EventArgs.Empty)

            End If

        End If
    End Sub

    Private Sub ddlDuration_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDuration.SelectedIndexChanged
        lblAmountMSP.Text = String.Empty
        lblValidUntil.Text = String.Empty
        If ddlDuration.SelectedIndex > 0 Then
            Dim objMSPMaster As MSPMaster = New MSPMasterFacade(User).Retrieve(CInt(ddlDuration.SelectedValue))
            Dim validUntil As Date = CDate(lblTglPKT.Text).AddYears(objMSPMaster.Duration)
            If validUntil <= Now.AddYears(1) Then
                MessageBox.Show("Nomor rangka ini sudah melebihi batas pendaftaran pada durasi " & objMSPMaster.Duration.ToString & " tahun.")
                btnSave.Enabled = False
                ddlDuration.SelectedIndex = 0
            Else
                Dim ppnVal As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(DateTime.Now.Date, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
                'lblAmountMSP.Text = (objMSPMaster.Amount * 1.1).ToString("C")
                lblAmountMSP.Text = (objMSPMaster.Amount + CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnVal, dpp:=objMSPMaster.Amount)).ToString("C")

                lblValidUntil.Text = validUntil.ToString("dd/MM/yyyy")
                btnSave.Enabled = True
            End If
            
        End If
    End Sub

    'Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
    '    lblStatus.Text = EnumStatusMSP.Status.Baru.ToString
    '    'btnNew.Visible = False
    '    RemoveSession()
    '    ClearForm()
    'End Sub

    Private Function ClearForm()
        txtOldMSPNo.Text = String.Empty
        txtKTPNo.Text = String.Empty
        txtName.Text = String.Empty
        txtAge.Text = String.Empty
        txtAddress.Text = String.Empty
        txtKelurahan.Text = String.Empty
        txtKecamatan.Text = String.Empty
        ddlPropinsi.SelectedIndex = 0
        ddlPropinsi_SelectedIndexChanged(Me, System.EventArgs.Empty)
        ddlPreArea.SelectedIndex = 0
        txtNotlp.Text = String.Empty
        txtEmail.Text = String.Empty
        txtChassisNumber.Text = String.Empty
        lblVehicleType.Text = String.Empty
        lblTglPKT.Text = String.Empty
        lblEngineNumber.Text = String.Empty
        ddlMSPType.Items.Clear()
        ddlDuration.Items.Clear()
        txtRefCustomerCode.Text = String.Empty
        lblAmountMSP.Text = String.Empty
        lblValidUntil.Text = String.Empty
        ddlSoldBy.SelectedIndex = 0
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim nResult As Integer
        Dim mSPRegistrationHistoryID As Integer
        Dim strValidate As String = ValidateForm()
        If strValidate <> String.Empty Then
            MessageBox.Show(strValidate.Substring(2, strValidate.Length - 2))
        Else
            BindDatatoObject()

            If IsNothing(_sessHelper.GetSession(_strSessMSPRegistrationHistoryID)) Then
                ' insert
                ' set request type = Baru
                objMSPRegistrationHistory.RequestType = EnumStatusMSP.StatusType.Baru
                objMSPRegistrationHistory.Sequence = 1
                If Not IsNothing(objMSPRegistrationHistory.MSPMaster) Then
                    objMSPRegistrationHistory.SelisihAmount = objMSPRegistrationHistory.MSPMaster.Amount
                Else
                    objMSPRegistrationHistory.SelisihAmount = 0
                End If

                If String.IsNullorEmpty(txtName.Text) Then
                    MessageBox.Show("Nama tidak boleh kosong")
                    Return
                End If

                Dim facMSPRegistration As New MSPRegistrationFacade(User)
                Dim int As Integer = facMSPRegistration.Insert(objMSPRegistration, objMSPRegistrationHistory, objMSPCustomer)
                If int = -1 Then
                    nResult = 1

                    ' add to history status
                    Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                    objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.MSP_Registration), mSPRegistrationHistoryID, oldStatusHistory, CType(New MSPRegistrationHistoryFacade(User).Retrieve(CInt(mSPRegistrationHistoryID)), MSPRegistrationHistory).Status)
                Else
                    objMSPRegistration = New MSPRegistrationFacade(User).Retrieve(int)
                    mSPRegistrationHistoryID = CType(objMSPRegistration.MSPRegistrationHistorys(objMSPRegistration.MSPRegistrationHistorys.Count - 1), MSPRegistrationHistory).ID

                    'add popup info status payment belum selesai
                    Dim dtRegNo As New StringBuilder
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPTransferPayment), "Dealer.DealerCode", MatchType.Exact, objLoginDealer.DealerCode))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPTransferPayment), "Status", MatchType.No, "6"))
                    Dim arrMSPTPayment As ArrayList = New MSPTransferPaymentFacade(User).Retrieve(criterias)
                    If arrMSPTPayment.Count > 0 Then
                        dtRegNo = New StringBuilder
                        For Each regNo As MSPTransferPayment In arrMSPTPayment
                            If Not String.IsNullorEmpty(regNo.RegNumber) Then
                                dtRegNo.Append(regNo.RegNumber)
                                dtRegNo.Append(";")
                            End If
                        Next
                        MessageBox.Show("Anda belum melakukan pembayaran dengan no registration: " & dtRegNo.ToString & " silahkan lakukan pembayaran")
                    End If

                End If
            Else
                'update
                Dim oldObjMSPRegistrattionHistory As MSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(CInt(_sessHelper.GetSession(_strSessMSPRegistrationHistoryID)))
                oldObjMSPRegistrattionHistory.BenefitMasterHeaderID = objMSPRegistrationHistory.BenefitMasterHeaderID
                If (ddlDuration.Visible) Then
                    oldObjMSPRegistrattionHistory.MSPMaster = objMSPRegistrationHistory.MSPMaster
                End If
                oldObjMSPRegistrattionHistory.SFDate = objMSPRegistrationHistory.SFDate
                oldObjMSPRegistrattionHistory.SoldBy = objMSPRegistrationHistory.SoldBy

                Dim oldObjMSPRegistration As MSPRegistration = oldObjMSPRegistrattionHistory.MSPRegistration
                oldObjMSPRegistration.ChassisMaster = objMSPRegistration.ChassisMaster
                oldObjMSPRegistration.OldMSPCode = objMSPRegistration.OldMSPCode

                Dim oldObjMSPCustomer As MSPCustomer = oldObjMSPRegistration.MSPCustomer
                oldObjMSPCustomer.Age = objMSPCustomer.Age
                oldObjMSPCustomer.Alamat = objMSPCustomer.Alamat
                oldObjMSPCustomer.Attachment = objMSPCustomer.Attachment
                oldObjMSPCustomer.City = objMSPCustomer.City
                oldObjMSPCustomer.CompleteName = objMSPCustomer.CompleteName
                oldObjMSPCustomer.Email = objMSPCustomer.Email
                oldObjMSPCustomer.Kecamatan = objMSPCustomer.Kecamatan
                oldObjMSPCustomer.Kelurahan = objMSPCustomer.Kelurahan
                oldObjMSPCustomer.KTPNo = objMSPCustomer.KTPNo
                oldObjMSPCustomer.Name1 = objMSPCustomer.Name1
                oldObjMSPCustomer.Name2 = objMSPCustomer.Name2
                oldObjMSPCustomer.Name3 = objMSPCustomer.Name3
                oldObjMSPCustomer.PhoneNo = objMSPCustomer.PhoneNo
                oldObjMSPCustomer.PostalCode = objMSPCustomer.PostalCode
                oldObjMSPCustomer.PreArea = objMSPCustomer.PreArea
                oldObjMSPCustomer.Province = objMSPCustomer.Province
                oldObjMSPCustomer.RefCustomer = objMSPCustomer.RefCustomer

                If oldObjMSPRegistrattionHistory.RequestType = CInt(EnumStatusMSP.StatusType.Baru) Then
                    If Not IsNothing(oldObjMSPRegistrattionHistory.MSPMaster) Then
                        oldObjMSPRegistrattionHistory.SelisihAmount = objMSPRegistrationHistory.MSPMaster.Amount
                    Else
                        oldObjMSPRegistrattionHistory.SelisihAmount = 0
                    End If
                Else
                    Dim crtUpgrade As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPRegistrationHistory), "Sequence", MatchType.Exact, oldObjMSPRegistrattionHistory.Sequence - 1))
                    crtUpgrade.opAnd(New Criteria(GetType(MSPRegistrationHistory), "MSPRegistration.ID", MatchType.Exact, oldObjMSPRegistrattionHistory.MSPRegistration.ID))
                    Dim prefObjMSPRegHistory As MSPRegistrationHistory = CType(New MSPRegistrationHistoryFacade(User).Retrieve(crtUpgrade)(0), MSPRegistrationHistory)

                    If Not IsNothing(oldObjMSPRegistrattionHistory.MSPMaster) Then
                        Dim _MSPHelper As New MSPHelper()
                        Dim Str As String = _MSPHelper.ValidateUpgradeMSP(prefObjMSPRegHistory, oldObjMSPRegistrattionHistory)
                        If Str <> String.Empty Then
                            MessageBox.Show(Str.Substring(2, Str.Length - 2))
                            Exit Sub
                        End If

                        oldObjMSPRegistrattionHistory.SelisihAmount = objMSPRegistrationHistory.MSPMaster.Amount - prefObjMSPRegHistory.MSPMaster.Amount
                    Else
                        oldObjMSPRegistrattionHistory.SelisihAmount = 0
                    End If
                End If

                Dim facMSPRegistration As New MSPRegistrationFacade(User)
                Dim int As Integer = facMSPRegistration.Update(oldObjMSPRegistration, oldObjMSPRegistrattionHistory, oldObjMSPCustomer)
                If int = -1 Then
                    nResult = 1
                Else
                    objMSPRegistration = New MSPRegistrationFacade(User).Retrieve(int)
                    mSPRegistrationHistoryID = CType(objMSPRegistration.MSPRegistrationHistorys(objMSPRegistration.MSPRegistrationHistorys.Count - 1), MSPRegistrationHistory).ID
                End If
            End If
            If nResult <> 1 Then
                _sessHelper.SetSession(_strSessMSPRegistrationHistoryID, mSPRegistrationHistoryID)
                MessageBox.Show("Data registrasi konsumen berhasil tersimpan.")
                If objLoginDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                    btnValidasi.Visible = True
                End If

            Else
                MessageBox.Show("Data registrasi konsumen gagal tersimpan.")
            End If
        End If
    End Sub

    Private Function BindDatatoObject()
        BindMSPCustomerObject()
        BindMSPRegistrationObject()
        BindMSPRegistrationHistoryObject()
    End Function

    Private Function ValidateForm() As String
        Dim str As String = String.Empty
        If lblTglPKT.Text = String.Empty Then
            str += "\n" + "Tanggal PKT tidak boleh kosong."
        End If

        If txtKTPNo.Text.Length < 16 AndAlso txtKTPNo.Text.Length > 0 Then
            str += "\n" + "No KTP tidak boleh kurang dari 16 digit."
        End If

        'If ddlPropinsi.SelectedIndex = 0 Then
        '    str += "\n" + "Provinsi tidak boleh kosong."
        'End If

        'If ddlPreArea.SelectedIndex = 0 Then
        '    str += "\n" + "PreArea tidak boleh kosong."
        'End If

        'If ddlKota.SelectedIndex = 0 Then
        '    str += "\n" + "Kota tidak boleh kosong."
        'End If

        If ddlMSPType.SelectedIndex = 0 Then
            str += "\n" + "Tipe MSP tidak boleh kosong."
        End If

        If (ddlDuration.Visible) Then
            If ddlDuration.SelectedIndex = 0 Then
                str += "\n" + "Durasi/KM Package tidak boleh kosong."
            End If
        End If

        If txtChassisNumber.Text.Trim = String.Empty Then
            str += "\n" + "Chassis number tidak boleh kosong."
        Else
            str += ValidateChassisNumber(txtChassisNumber.Text)
        End If

        If txtRefCustomerCode.Text <> String.Empty Then
            Dim objCustomer As Customer = New CustomerFacade(User).RetrieveByCode(txtRefCustomerCode.Text)
            If objCustomer.ID = 0 Then
                str += "\n" & "Ref kode pelanggan tidak terdaftar."
            End If
        End If

        If txtEmail.Text <> String.Empty Then
            Dim bool As Boolean = New KTB.DNet.UI.Helper.MSPHelper().EmailAddressCheck(txtEmail.Text)
            If bool = False Then
                str += "\n" & "Alamat email tidak valid."
            End If
        End If

        If ddlSoldBy.SelectedIndex = 0 Then
            str += "\n" + "Dijual oleh tidak boleh kosong."
        End If

        Return str
    End Function

    Public Function ValidateChassisNumber(ByVal chassisNumber As String) As String
        ' chassis number yg teregistrasi tidak dapat dibuat lagi
        Dim str As String = String.Empty
        Dim mspRegistrationHistoryID As Integer = _sessHelper.GetSession(_strSessMSPRegistrationHistoryID)
        If IsNothing(mspRegistrationHistoryID) Then
            mspRegistrationHistoryID = 0
        End If
        objMSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(mspRegistrationHistoryID)

        crt = New CriteriaComposite(New Criteria(GetType(MSPRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisNumber))
        crt.opAnd(New Criteria(GetType(MSPRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not IsNothing(objMSPRegistrationHistory) Then
            crt.opAnd(New Criteria(GetType(MSPRegistration), "ID", MatchType.No, objMSPRegistrationHistory.MSPRegistration.ID))
        End If

        arr = New MSPRegistrationFacade(User).Retrieve(crt)
        If arr.Count > 0 Then
            str = "\n" + "Chassis number " + txtChassisNumber.Text + " sudah terdaftar sebelumnya."
            txtChassisNumber.Text = String.Empty
        End If

        Return str
    End Function

    Private Function BindMSPCustomerObject()
        objMSPCustomer = New MSPCustomer
        objMSPCustomer.KTPNo = txtKTPNo.Text
        objMSPCustomer.Name1 = txtName.Text
        objMSPCustomer.Age = If(txtAge.Text = String.Empty, 0, CInt(txtAge.Text))
        objMSPCustomer.Alamat = txtAddress.Text
        objMSPCustomer.Kecamatan = txtKecamatan.Text
        objMSPCustomer.Kelurahan = txtKelurahan.Text
        Try
            If ddlPropinsi.SelectedValue.ToString() <> "0" Then
                objMSPCustomer.Province = New Province(ID:=CInt(ddlPropinsi.SelectedValue))
            End If

        Catch ex As Exception

        End Try

        Try
            objMSPCustomer.PreArea = ddlPreArea.SelectedValue
        Catch ex As Exception

        End Try
        Try
            If ddlKota.SelectedValue.ToString() <> "0" Then
                objMSPCustomer.City = New City(ID:=CInt(ddlKota.SelectedValue))
            End If


        Catch ex As Exception

        End Try

        objMSPCustomer.PhoneNo = txtNotlp.Text
        objMSPCustomer.Email = txtEmail.Text

        If (txtRefCustomerCode.Text <> String.Empty) Then
            Dim objCustomer As Customer = New CustomerFacade(User).Retrieve(txtRefCustomerCode.Text)
            If Not IsNothing(objCustomer) Then
                objMSPCustomer.RefCustomer = objCustomer
            End If
        End If
       
    End Function

    Private Function BindMSPRegistrationObject()
        objMSPRegistration = New MSPRegistration
        Dim objChassisMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(txtChassisNumber.Text)
        If Not IsNothing(objChassisMaster) Then
            objMSPRegistration.ChassisMaster = objChassisMaster
        End If
        objMSPRegistration.Dealer = objLoginDealer
        objMSPRegistration.OldMSPCode = txtOldMSPNo.Text
    End Function

    Private Function BindMSPRegistrationHistoryObject()
        objMSPRegistrationHistory = New MSPRegistrationHistory

        If (ddlDuration.Visible) Then
            Dim newMSPMaster As MSPMaster = New MSPMaster(ID:=CInt(ddlDuration.SelectedValue))
            If Not IsNothing(newMSPMaster) Then
                objMSPRegistrationHistory.MSPMaster = newMSPMaster
            End If
        End If
       
        objMSPRegistrationHistory.RegistrationDate = CDate(lblCurrentDate.Text)
        objMSPRegistrationHistory.SoldBy = ddlSoldBy.SelectedValue

        objMSPRegistration.MSPRegistrationHistorys.Add(objMSPRegistrationHistory)
    End Function

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        RemoveSession()
        Response.Redirect("FrmMSPRegistrationList.aspx")
    End Sub

    Private Function RemoveSession()
        _sessHelper.RemoveSession(_strSessMSPRegistrationHistoryID)
        _sessHelper.RemoveSession(_strSessStatusInput)
    End Function

    Protected Sub btnValidasi_Click(sender As Object, e As EventArgs) Handles btnValidasi.Click
        Dim idMSPRegistrationHistory As Integer = CInt(_sessHelper.GetSession(_strSessMSPRegistrationHistoryID))
        objMSPRegistrationHistory = New MSPRegistrationHistory
        objMSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(idMSPRegistrationHistory)
        If Not IsNothing(objMSPRegistrationHistory) Then
            If (objMSPRegistrationHistory.Status = EnumStatusMSP.Status.Baru Or objMSPRegistrationHistory.Status = EnumStatusMSP.Status.Batal_Validasi) Then
                ' set old status to history status
                oldStatusHistory = objMSPRegistrationHistory.Status
                objMSPRegistrationHistory.Status = EnumStatusMSP.Status.Validasi
                ' set MSPCode
                objMSPRegistrationHistory.MSPRegistration.MSPCode = "MSP" & objMSPRegistrationHistory.MSPRegistration.ID.ToString.PadLeft(7, "0")

                If (New MSPRegistrationFacade(User).Update(objMSPRegistrationHistory.MSPRegistration, objMSPRegistrationHistory, objMSPRegistrationHistory.MSPRegistration.MSPCustomer)) = -1 Then
                    MessageBox.Show("Gagal validasi data registrasi MSP.")
                    Return
                End If

                ' add to history status
                Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.MSP_Registration), objMSPRegistrationHistory.ID, oldStatusHistory, objMSPRegistrationHistory.Status)

                MessageBox.Show("Sukses validasi data registrasi MSP.")
                _sessHelper.SetSession(_strSessStatusInput, "VIEW")
                _sessHelper.SetSession(_strSessMSPRegistrationHistoryID, objMSPRegistrationHistory.ID)
                FillForm()
                btnValidasi.Visible = False
                btnSave.Visible = False
            End If
        End If
    End Sub

    Protected Sub lnkReloadCustomer_Click(sender As Object, e As EventArgs) Handles lnkReloadCustomer.Click
        If txtRefCustomerCode.Text = String.Empty Then
            MessageBox.Show("Ref kode pelanggan kosong.")
            Return
        End If

        ' clean up form ref customer
        txtName.Text = String.Empty
        txtAddress.Text = String.Empty
        txtKelurahan.Text = String.Empty
        txtKecamatan.Text = String.Empty
        ddlPropinsi.SelectedIndex = 0
        ddlPropinsi_SelectedIndexChanged(Me, EventArgs.Empty)
        ddlPreArea.SelectedIndex = 0
        txtNotlp.Text = String.Empty
        txtEmail.Text = String.Empty

        Dim objCustomer As Customer = New CustomerFacade(User).RetrieveByCode(txtRefCustomerCode.Text)
        If objCustomer.ID > 0 Then
            txtName.Text = objCustomer.Name1 & " " & objCustomer.Name2 & " " & objCustomer.Name3
            txtAddress.Text = objCustomer.Alamat
            txtKelurahan.Text = objCustomer.Kelurahan
            txtKecamatan.Text = objCustomer.Kecamatan
            If Not IsNothing(objCustomer.City) Then
                ddlPropinsi.SelectedValue = objCustomer.City.Province.ID
                ddlPropinsi_SelectedIndexChanged(Me, EventArgs.Empty)
                ddlKota.SelectedValue = objCustomer.City.ID
            End If
            ddlPreArea.SelectedValue = objCustomer.PreArea
            txtNotlp.Text = objCustomer.PhoneNo
            txtEmail.Text = objCustomer.Email

        Else
            MessageBox.Show("Ref kode pelanggan tidak terdaftar.")
        End If
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Dim idMSPRegistrationHistory As Integer = CInt(_sessHelper.GetSession(_strSessMSPRegistrationHistoryID))
        objMSPRegistrationHistory = New MSPRegistrationHistory
        objMSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(idMSPRegistrationHistory)
        If Not IsNothing(objMSPRegistrationHistory) Then
            If objMSPRegistrationHistory.Status = EnumStatusMSP.Status.Validasi Then
                ' set old history status msp
                oldStatusHistory = objMSPRegistrationHistory.Status
                objMSPRegistrationHistory.Status = EnumStatusMSP.Status.Konfirmasi

                If (New MSPRegistrationFacade(User).Update(objMSPRegistrationHistory.MSPRegistration, objMSPRegistrationHistory, objMSPRegistrationHistory.MSPRegistration.MSPCustomer)) = -1 Then
                    MessageBox.Show("Gagal konfirmasi data registrasi MSP.")
                    Return
                End If

                ' add to history status
                Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.MSP_Registration), objMSPRegistrationHistory.ID, oldStatusHistory, objMSPRegistrationHistory.Status)

                MessageBox.Show("Sukses konfirmasi data registrasi MSP.")
                _sessHelper.SetSession(_strSessStatusInput, "VIEW")
                Response.Redirect("FrmMSPRegistration.aspx")
            End If
        End If
    End Sub
End Class