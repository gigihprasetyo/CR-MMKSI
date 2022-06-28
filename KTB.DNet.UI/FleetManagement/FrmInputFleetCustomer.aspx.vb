#Region "Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Data
Imports System.Collections.Generic
Imports System.Linq
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SAP
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.Security
#End Region


Public Class FrmInputFleetCustomer
    Inherits System.Web.UI.Page

#Region "Variables"
    Private ReadOnly varSessFCContact As String = "sessInputFleetCustomerContact"
    Private ReadOnly varSessKepemilikanKendaraan As String = "sessKepemilikanKendaraan"
    Private ReadOnly varSessDealer As String = "sessDealer"
    Private ReadOnly varSessFleetCustomerID As String = "FleetCustomerID"
    Private ReadOnly varSessStatusUpdate As String = "StatusUpdate"
    Dim sessHelper As KTB.DNet.Utility.SessionHelper = New KTB.DNet.Utility.SessionHelper
    Private objLoginDealer As Dealer
    Private objUserInfo As UserInfo
    Private mode As enumMode.Mode
    Private sesHelper As New SessionHelper
    Dim _arlFCContact As ArrayList
    Dim _arlDealer As ArrayList
    Private objFleetCustomer As FleetCustomer
    Private objFleetCustomerContact As FleetCustomerContact
    'Private objFleetHasilSurveyHeader As FleetHasilSurveyHeader
    'Private objFleetHasilSurveyDetail As FleetHasilSurveyDetail
    Private objFleetCustomerToDealer As FleetCustomerToDealer

    Private facFleetCustomer As New FleetCustomerFacade(User)
    Private facFleetCustomerContact As New FleetCustomerContactFacade(User)
    'Private facFleetHasilSurveyHeader As New FleetHasilSurveyHeaderFacade(User)
    'Private facFleetHasilSurveyDetail As New FleetHasilSurveyDetailFacade(User)
    Private facFleetCustomerToDealer As New FleetCustomerToDealerFacade(User)
#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    ' datagrid
    Protected WithEvents dtgMain As Global.System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgKepemilikanKendaraan As Global.System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgDealerSelection As System.Web.UI.WebControls.DataGrid
    ' hidden field
    Protected WithEvents txtDealerCodeList As System.Web.UI.WebControls.HiddenField
    Protected WithEvents hdnFleetCustomerID As System.Web.UI.WebControls.HiddenField
    Protected WithEvents hdnGroupName As System.Web.UI.WebControls.HiddenField
    ' textbox
    Protected WithEvents txtGroupName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtGedung As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAlamat As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKecamatan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKelurahan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodepos As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNotlp As System.Web.UI.WebControls.TextBox
    'Protected WithEvents txtNoNPWP As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtIdentityNumber As System.Web.UI.WebControls.TextBox

    ' panel
    Protected WithEvents pnlKepemilikanKendaraan As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlOtorisasiDealer As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlFleetCustomer As System.Web.UI.WebControls.Panel

    ' dropdownlist 
    Protected WithEvents ddlPreArea As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPropinsi As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlKota As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTipePerusahaan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlIdentityType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlClassification As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlBusinessSector As System.Web.UI.WebControls.DropDownList
    Protected WithEvents inFileLocation As Global.System.Web.UI.HtmlControls.HtmlInputFile

    Protected WithEvents hdnMCPConfirmation As System.Web.UI.HtmlControls.HtmlInputHidden
    ' button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnDealerHelper As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    'link button
    Protected WithEvents lbtnDownload As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnDelete As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lnkbtnDeleteAttachment As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lnkReloadCustomerGroup As System.Web.UI.WebControls.LinkButton
    ' label
    'Protected WithEvents lblTypePerusahaanSelect As System.Web.UI.WebControls.Label
    'Protected WithEvents lblCustBusinessSelect As System.Web.UI.WebControls.Label
    'Protected WithEvents lblGroupNameSelect As System.Web.UI.WebControls.Label
    Protected WithEvents lblAttachment As System.Web.UI.WebControls.Label
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblPencarianDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    ' tabel
    Protected WithEvents tblFleetCustomer As System.Web.UI.WebControls.Table
    ' privilege
    Private _input As Boolean = False
    Private _edit As Boolean = False
    Private _view As Boolean = False

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        ' untuk render dinamic datagrid kepemilikan kendaraan
        'InitGridKepemilikanKendaraan()
    End Sub
#End Region

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'txtGroupName.Enabled = False
        txtCode.Enabled = False
        CheckPrivilege()
        If Not IsPostBack() Then
            FillForm()
        End If
        txtGroupName.Attributes.Add("readonly", "1")
    End Sub

    Private Sub CheckPrivilege()
        '_view = SecurityProvider.Authorize(Context.User, SR.Input_Pengajuan_Fleet_Privilege)
        'If Not _view Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=FLEET MANAGEMENT - Input Fleet Customer")
        'End If

        '_input = SecurityProvider.Authorize(Context.User, SR.Input_Pengajuan_Fleet_Privilege)
        '_edit = SecurityProvider.Authorize(Context.User, SR.Proses_Pengajuan_Fleet_Privilege)

        'btnSave.Visible = _input

        _view = SecurityProvider.Authorize(Context.User, SR.FleetCustomer_List_Privilege)
        If Not _view Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FLEET MANAGEMENT - Input Fleet Customer")
        End If

        _input = SecurityProvider.Authorize(Context.User, SR.FleetCustomer_Input_Privilege)
        _edit = SecurityProvider.Authorize(Context.User, SR.FleetCustomer_Edit_Privilege)

        btnSave.Visible = _input

    End Sub

#Region "Reload script"
    Private Sub lnkReloadCustomerGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkReloadCustomerGroup.Click
        If txtGroupName.Text.Trim <> String.Empty Then

            Dim objCustomerGroup As CustomerGroup = New CustomerGroupFacade(User).RetrieveByCode(txtGroupName.Text)
            If objCustomerGroup.ID = 0 Then
                MessageBox.Show("Kode tidak terdaftar")
            End If
        Else
            MessageBox.Show("Kode kosong")
        End If

    End Sub
#End Region

#Region "form action"

    Private Sub ddlPropinsi_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlPropinsi.SelectedIndexChanged
        ddlKota.Items.Clear()
        If ddlPropinsi.SelectedIndex <> 0 Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(City), "Province.ID", MatchType.Exact, ddlPropinsi.SelectedValue))
            criterias.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))

            ddlKota.DataSource = New CityFacade(User).RetrieveActiveList(criterias, "CityName", Sort.SortDirection.ASC)
            ddlKota.DataTextField = "CityName".ToUpper
            ddlKota.DataValueField = "ID"
            ddlKota.DataBind()
        End If
        ddlKota.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlKota.SelectedIndex = 0

        If Not String.IsNullOrEmpty(hdnGroupName.Value) Then
            'lblGroupNameSelect.Text = hdnGroupName.Value
            txtGroupName.Text = hdnGroupName.Value
        End If

    End Sub

    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        ddlTipePerusahaan.Enabled = False
        ddlClassification.Enabled = False
        ddlTipePerusahaan.Items.Clear()
        ddlClassification.Items.Clear()

        Dim listOfIdentityType As ArrayList = New ArrayList

        'EnumTipePelanggan 0 => Perorangan
        'EnumTipePelanggan 1 => Perusahaan
        'EnumTipePelanggan 2 => BUMN

        If ddlCategory.SelectedValue = "0" Then

            'Add NPWP to listOfStandardCode
            listOfIdentityType.Add(New StandardCodeFacade(User).GetByCategoryValue("EnumIdentityType", "1"))

        ElseIf ddlCategory.SelectedValue = "1" Then
            ddlTipePerusahaan.Enabled = True
            ddlTipePerusahaan.Items.Clear()
            ddlTipePerusahaan.DataSource = New StandardCodeFacade(User).RetrieveByCategory("EnumTipePerusahaan")
            ddlTipePerusahaan.DataTextField = "ValueDesc"
            ddlTipePerusahaan.DataValueField = "ValueId"
            ddlTipePerusahaan.DataBind()
            ddlTipePerusahaan.Items.Insert(0, New ListItem("Silakan Piih", -1))
            ddlTipePerusahaan.SelectedIndex = 0

            'Add NPWP to listOfStandardCode
            listOfIdentityType.Add(New StandardCodeFacade(User).GetByCategoryValue("EnumIdentityType", "2"))

            'Add SIUP to listOfStandardCode
            listOfIdentityType.Add(New StandardCodeFacade(User).GetByCategoryValue("EnumIdentityType", "3"))

        ElseIf ddlCategory.SelectedValue = "2" Then
            ' dropdown klasifikasi
            ddlClassification.Enabled = True
            ddlClassification.Items.Clear()
            ddlClassification.DataSource = EnumMCPClassification.RetrieveClassification
            ddlClassification.DataValueField = "ValStatus"
            ddlClassification.DataTextField = "NameStatus"
            ddlClassification.DataBind()
            ddlClassification.Items.Insert(0, New ListItem("Silakan Piih", -1))
            ddlClassification.SelectedIndex = 0

            'Add E-CATALOGUE to listOfStandardCode
            listOfIdentityType.Add(New StandardCodeFacade(User).GetByCategoryValue("EnumIdentityType", "4"))

        Else
            listOfIdentityType = New StandardCodeFacade(User).RetrieveByCategory("EnumIdentityType")
        End If

        ddlIdentityType.DataSource = listOfIdentityType
        ddlIdentityType.DataTextField = "ValueDesc"
        ddlIdentityType.DataValueField = "ValueId"
        ddlIdentityType.DataBind()
        ddlIdentityType.SelectedIndex = 0


        'lblTypePerusahaanSelect.Text = "[Tipe]"
        'If (ddlCategory.SelectedValue = 1) Then
        '    lblTypePerusahaanSelect.Text = "CP"
        'ElseIf ddlCategory.SelectedValue = 2 Then
        '    lblTypePerusahaanSelect.Text = "GV"
        'End If

        If Not String.IsNullOrEmpty(hdnGroupName.Value) Then
            'lblGroupNameSelect.Text = hdnGroupName.Value
            txtGroupName.Text = hdnGroupName.Value
        End If

    End Sub
#End Region

#Region "generate form component"
    Private Sub BindDropDown()

        ddlCategory.Items.Clear()

        Dim criteriasTipePelanggan As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriasTipePelanggan.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumTipePelanggan"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(StandardCode), "Sequence", Sort.SortDirection.ASC))
        ddlCategory.DataSource = New StandardCodeFacade(User).Retrieve(criteriasTipePelanggan, sortColl)
        ddlCategory.DataTextField = "ValueDesc"
        ddlCategory.DataValueField = "ValueId"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Silakan Piih", -1))
        ddlCategory.SelectedIndex = 0
        ddlCategory_SelectedIndexChanged(Me, System.EventArgs.Empty)

        ' dropdown profile detail yang profile header cod = CBU_PURPOSE_FLEET 
        Dim listOfBusinessSector As ArrayList = New VWI_BusinessSectorFacade(User).RetrieveList()
        ddlBusinessSector.Items.Clear()
        ddlBusinessSector.DataSource = listOfBusinessSector
        ddlBusinessSector.DataTextField = "BusinessName"
        ddlBusinessSector.DataValueField = "ID"
        ddlBusinessSector.DataBind()
        ddlBusinessSector.Items.Insert(0, New ListItem("Silakan Piih", -1))
        ddlBusinessSector.SelectedIndex = 0

        'dropdown propinsi
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim list As ArrayList = New ProvinceFacade(User).RetrieveActiveList(criterias, "ProvinceName", Sort.SortDirection.ASC)

        ddlPropinsi.Items.Clear()
        ddlPropinsi.DataSource = list
        ddlPropinsi.DataTextField = "ProvinceName"
        ddlPropinsi.DataValueField = "ID"
        ddlPropinsi.DataBind()
        ddlPropinsi.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlPropinsi.SelectedIndex = 0
        ddlPropinsi_SelectedIndexChanged(Me, System.EventArgs.Empty)

    End Sub
#End Region

#Region "Custom"

    Private Sub AddData(ByRef _ObjArray As ArrayList, ByVal e As DataGridCommandEventArgs)
        Dim txtName As TextBox = CType(e.Item.FindControl("txtName"), TextBox)
        Dim txtPosition As TextBox = CType(e.Item.FindControl("txtPosition"), TextBox)
        Dim txtPhone As TextBox = CType(e.Item.FindControl("txtPhone"), TextBox)
        Dim txtHandphone As TextBox = CType(e.Item.FindControl("txtHandphone"), TextBox)
        Dim txtEmail As TextBox = CType(e.Item.FindControl("txtEmail"), TextBox)

        If txtName.Text.Trim() = "" OrElse txtPosition.Text.Trim() = "" OrElse txtPhone.Text.Trim = "" OrElse txtEmail.Text.Trim = String.Empty Then
            dtgMain.DataSource = _arlFCContact
            dtgMain.DataBind()
            MessageBox.Show("Data yang anda input tidak lengkap")
            Return
        End If

        Dim strMessage As String = String.Empty

        If txtEmail.Text.Trim.Length > 0 Then
            If Not EmailAddressCheck(txtEmail.Text.Trim) Then
                strMessage &= "Format email salah. \n"
            End If
        End If

        If txtHandphone.Text.Trim <> String.Empty And IsHandphoneValid(txtHandphone.Text.Trim) = False Then
            strMessage &= "No. Handphone harus diawali dengan '08' (nol)."
        End If

        If strMessage <> String.Empty Then
            dtgMain.DataSource = _arlFCContact
            dtgMain.DataBind()
            MessageBox.Show(strMessage)
            Return
        End If

        Dim objFCContact As New FleetCustomerContact
        objFCContact.ID = "0"
        objFCContact.Name = txtName.Text
        objFCContact.Position = txtPosition.Text
        objFCContact.PhoneNo = txtPhone.Text
        objFCContact.Handphone = txtHandphone.Text.Trim
        objFCContact.Email = txtEmail.Text

        _ObjArray.Add(objFCContact)
        dtgMain.DataSource = _ObjArray
        dtgMain.DataBind()

    End Sub

    Private Function IsHandphoneValid(ByVal handphoneNo As String) As Boolean
        Dim status As Boolean = True

        If Len(handphoneNo) < 6 AndAlso Left(handphoneNo, 2) <> "08" Then
            status = False
        End If

        Return status
    End Function

    Private Function EmailAddressCheck(ByVal emailAddress As String) As Boolean
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(emailAddress, pattern)
        If emailAddressMatch.Success Then
            EmailAddressCheck = True
        Else
            EmailAddressCheck = False
        End If
    End Function

#End Region

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim nResult As Integer = 0
        Dim fleetCustomerID As Integer
        Dim sessAssignToDealer As String = sessHelper.GetSession(varSessStatusUpdate)
        Dim strValidateCode As String = String.Empty

        If ValidateSave() Then
            GetFleetCustomer()
            If sessAssignToDealer <> "UpdateDealer" Then
                If UploadFile(objFleetCustomer) <> 1 Then
                    MessageBox.Show("File gagal disimpan di Server. Harap hubungi Administrator")
                    Return
                End If
            End If

            If Convert.ToString(sessHelper.GetSession("Status")) = "Insert" Then
                ' validate  code
                strValidateCode = facFleetCustomer.ValidateCode(objFleetCustomer.Code)
                If strValidateCode = String.Empty Then

                    ' insert fleet customer
                    fleetCustomerID = facFleetCustomer.Insert(objFleetCustomer)
                    If (fleetCustomerID <> -1) Then
                        Dim fleetcust As FleetCustomer = facFleetCustomer.Retrieve(fleetCustomerID)
                        fleetcust.Code = String.Format("F{0}", fleetCustomerID.ToString().PadLeft(5, "0"))
                        facFleetCustomer.Update(fleetcust)
                        txtCode.Text = fleetcust.Code

                        Dim listFCContact = GetFleetCustomerContact(fleetCustomerID)
                        ' insert fleet customer contact
                        For Each itemFCContact As FleetCustomerContact In listFCContact
                            If (facFleetCustomerContact.Insert(itemFCContact) = -1) Then
                                If (nResult <> -1) Then
                                    nResult = -1
                                End If
                            End If
                        Next

                        'GetFleetHasilSurverHeader(fleetCustomerID)
                        ' insert fleet hasil surver header
                        'Dim fleetHasilSurveyHeaderID = facFleetHasilSurveyHeader.Insert(objFleetHasilSurveyHeader)
                        'If (fleetHasilSurveyHeaderID <> -1) Then
                        '    Dim listFHSDetail = GetFleetHasilSurveyDetail(fleetHasilSurveyHeaderID)
                        '    ' insert list fleet hasil survey detail
                        '    For Each objDetail As FleetHasilSurveyDetail In listFHSDetail
                        '        If (facFleetHasilSurveyDetail.Insert(objDetail) = -1) Then
                        '            If (nResult <> -1) Then
                        '                nResult = -1
                        '            End If
                        '        End If
                        '    Next

                        'Else
                        '    If (nResult <> -1) Then
                        '        nResult = -1
                        '    End If
                        'End If

                        Dim listFCtoDealer = GetFleetCustomertoDealer(fleetCustomerID)
                        For Each objFCtoDealer As FleetCustomerToDealer In listFCtoDealer
                            ' insert list fleet customer to dealer
                            If (facFleetCustomerToDealer.Insert(objFCtoDealer) = -1) Then
                                If (nResult <> -1) Then
                                    nResult = -1
                                End If
                            End If
                        Next

                    Else
                        nResult = -1
                    End If
                Else
                    nResult = -1
                End If

            ElseIf Convert.ToString(sessHelper.GetSession("Status")) = "Update" Then
                fleetCustomerID = sessHelper.GetSession(varSessFleetCustomerID)
                ' validate code
                strValidateCode = facFleetCustomer.ValidateCode(objFleetCustomer.Code, fleetCustomerID)
                If strValidateCode = String.Empty Then
                    objFleetCustomer.ID = fleetCustomerID
                    If (sessAssignToDealer <> "UpdateDealer") Then
                        facFleetCustomer.Update(objFleetCustomer)
                        Dim listOldFCContact As ArrayList = facFleetCustomerContact.RetrieveWithOneCriteria(1, 1000, 1000, "FleetCustomerID", MatchType.Exact, fleetCustomerID)
                        Dim listNewFCContact As List(Of FleetCustomerContact) = GetFleetCustomerContact(fleetCustomerID)
                        For Each itemNew As FleetCustomerContact In listNewFCContact
                            If itemNew.ID = 0 Then
                                ' insert jika id = 0
                                nResult = facFleetCustomerContact.Insert(itemNew)
                            Else
                                ' update jika id tidak = 0
                                nResult = facFleetCustomerContact.Update(itemNew)

                                ' mencari index data yang terupdate
                                Dim index As Integer = -1
                                If listOldFCContact.Count > 0 Then
                                    For i As Integer = 0 To listOldFCContact.Count - 1
                                        Dim itemOldFCContact As FleetCustomerContact = listOldFCContact(i)
                                        If itemOldFCContact.ID = itemNew.ID Then
                                            index = i
                                        End If
                                    Next

                                End If

                                ' remove at index di list data lama
                                If index > -1 Then
                                    listOldFCContact.RemoveAt(index)
                                End If
                            End If
                        Next

                        ' hapus data lama
                        If listOldFCContact.Count > 0 Then
                            For Each itemOld As FleetCustomerContact In listOldFCContact
                                itemOld.RowStatus = DBRowStatus.Deleted
                                facFleetCustomerContact.Update(itemOld)
                            Next
                        End If
                    Else
                        ' update at assign to dealer
                        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomerToDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crt.opAnd(New Criteria(GetType(FleetCustomerToDealer), "FleetCustomerID", MatchType.Exact, fleetCustomerID))
                        Dim listOldFCDealer As ArrayList = facFleetCustomerToDealer.Retrieve(crt)
                        Dim listNewFCDealer As List(Of FleetCustomerToDealer) = GetFleetCustomertoDealer(fleetCustomerID)

                        For Each itemNew As FleetCustomerToDealer In listNewFCDealer
                            If itemNew.ID = 0 Then
                                ' insert jika id = 0
                                facFleetCustomerToDealer.Insert(itemNew)
                            Else
                                facFleetCustomerToDealer.Update(itemNew)

                                ' update jika id tidak sama dengan 0
                                Dim index As Integer = -1
                                If listOldFCDealer.Count > 0 Then
                                    For i As Integer = 0 To listOldFCDealer.Count - 1
                                        Dim itemOldFCDealer As FleetCustomerToDealer = listOldFCDealer(i)
                                        If itemOldFCDealer.ID = itemNew.ID Then
                                            index = i
                                        End If
                                    Next
                                End If

                                ' remove at index di list data lama
                                If index > -1 Then
                                    listOldFCDealer.RemoveAt(index)
                                End If
                            End If
                        Next

                        ' hapus data lama
                        If listOldFCDealer.Count > 0 Then
                            For Each itemOld As FleetCustomerToDealer In listOldFCDealer
                                itemOld.RowStatus = DBRowStatus.Deleted
                                facFleetCustomerToDealer.Update(itemOld)
                            Next
                        End If

                    End If
                Else
                    nResult = -1
                End If

            End If
        Else
            nResult = -2
        End If

        If Not String.IsNullOrEmpty(hdnGroupName.Value) Then
            txtGroupName.Text = hdnGroupName.Value
        End If

        If (nResult = 0) Then
            If strValidateCode = String.Empty Then
                RemoveALLSession()
                Dim strSesStatusUpdate As String = sessHelper.GetSession(varSessStatusUpdate)
                sessHelper.SetSession("Status", "Update")
                sessHelper.SetSession(varSessFleetCustomerID, fleetCustomerID)
                'sessHelper.SetSession(varSessStatusUpdate, "UpdateFleet")
                sessHelper.SetSession(varSessStatusUpdate, strSesStatusUpdate)
                pnlOtorisasiDealer.Visible = False
                pnlKepemilikanKendaraan.Visible = False

                If (sessAssignToDealer = "UpdateDealer") Then
                    pnlOtorisasiDealer.Visible = True
                End If
                lblAttachment.Text = objFleetCustomer.Attachment
                ' dtg fleet customer contact
                Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomerContact), "FleetCustomerID", MatchType.Exact, fleetCustomerID))
                crt.opAnd(New Criteria(GetType(FleetCustomerContact), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                _arlFCContact = facFleetCustomerContact.Retrieve(crt)
                dtgMain.DataSource = _arlFCContact
                dtgMain.DataBind()
                sessHelper.SetSession(varSessFCContact, _arlFCContact)

                MessageBox.Show("Simpan Berhasil")
            Else
                MessageBox.Show(strValidateCode)
            End If

        ElseIf nResult = -1 Then
            If strValidateCode <> String.Empty Then
                MessageBox.Show(strValidateCode)
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        ElseIf nResult = -2 Then
        End If

    End Sub

    Private Function UploadFile(ByRef objFleetCustomer As FleetCustomer) As Integer
        Dim retValue As Integer = 0
        Dim sessStatus As String = sessHelper.GetSession("Status")

        If inFileLocation.PostedFile.FileName.Length > 0 Then
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

            Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            sapImp.Start()
            Try
                If inFileLocation.PostedFile.ContentLength <> inFileLocation.PostedFile.InputStream.Length Then
                    retValue = 0
                    Throw New Exception("File Tidak Sama")
                End If

                Dim directory As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("FleetCustomerDir")
                Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(directory)
                If Not directoryInfo.Exists Then
                    directoryInfo.Create()
                End If

                Dim ext As String = System.IO.Path.GetExtension(inFileLocation.PostedFile.FileName)
                Dim filename As String = System.IO.Path.GetFileName(inFileLocation.PostedFile.FileName)
                Dim targetFile As String = Path.Combine(directory, filename).ToString

                Dim fInfo As System.IO.FileInfo = New System.IO.FileInfo(targetFile)
                If fInfo.Exists Then
                    fInfo.Delete()
                End If

                inFileLocation.PostedFile.SaveAs(targetFile)
                Dim trgInfo As System.IO.FileInfo = New System.IO.FileInfo(targetFile)
                If Not trgInfo.Exists Then
                    retValue = 0
                End If
                Dim strFileSave As String = KTB.DNet.Lib.WebConfig.GetValue("FleetCustomerDir") & "\" & filename
                objFleetCustomer.Attachment = strFileSave
                retValue = 1
            Catch ex As Exception
                retValue = 0
            Finally
                sapImp.StopImpersonate()
            End Try
            Return retValue
        Else
            If (sessStatus <> "UpdateDealer") Then
                retValue = 1
            End If
            Return retValue
        End If
    End Function

    Private Function ValidateSave() As Boolean
        Dim str As String = String.Empty
        Dim result As Boolean = True
        Dim objFleetCustomer As New FleetCustomer

        If ddlCategory.SelectedIndex = 0 Then
            str = "Silakan Pilih Kategori !"
            result = False
        End If

        If (ddlCategory.SelectedValue = 1) Then
            If ddlTipePerusahaan.SelectedIndex = 0 Then
                If String.IsNullOrEmpty(str) Then
                    str = "Silakan Pilih Tipe Perusahaan !"
                Else
                    str += "\nSilakan Pilih Tipe Perusahaan !"
                End If
                result = False
            End If
        ElseIf ddlCategory.SelectedValue = 2 Then
            If ddlClassification.SelectedIndex = 0 Then
                If String.IsNullOrEmpty(str) Then
                    str = "Silakan Pilih Klasifikasi !"
                Else
                    str += "\nSilakan Pilih Klasifikasi !"
                End If
                result = False
            End If
        End If

        If ddlBusinessSector.SelectedIndex = 0 Then
            If String.IsNullOrEmpty(str) Then
                str = "Silakan Pilih Profil Bisnis !"
            Else
                str += "\nSilakan Profil Bisnis !"
            End If
            result = False
        End If

        If String.IsNullOrEmpty(txtName.Text) Then
            If String.IsNullOrEmpty(str) Then
                str = "Data Nama tidak boleh kosong !"
            Else
                str += "\nData Nama tidak boleh kosong !"
            End If
            result = False
        End If

        If String.IsNullOrEmpty(txtAlamat.Text) Then
            If String.IsNullOrEmpty(str) Then
                str = "Data Alamat tidak boleh kosong !"
            Else
                str += "\nData Alamat tidak boleh kosong !"
            End If
            result = False
        End If

        If ddlPropinsi.SelectedIndex = 0 Then
            If String.IsNullOrEmpty(str) Then
                str = "Silakan Pilih Propinsi !"
            Else
                str += "\nSilakan Pilih Propinsi !"
            End If
            result = False
        End If

        If ddlKota.SelectedIndex = 0 Then
            If String.IsNullOrEmpty(str) Then
                str = "Silakan Pilih Kota !"
            Else
                str += "\nSilakan Pilih Kota !"
            End If
            result = False
        End If

        Dim city As KTB.DNet.Domain.City = New CityFacade(User).Retrieve(CType(ddlKota.SelectedValue, Integer))
        Dim area As String = String.Empty
        area = ddlPreArea.SelectedValue.ToUpper
        If Not IsNothing(city) Then
            Select Case Len(city.CityName)
                Case Is > 31
                    If ddlPreArea.SelectedIndex > 0 Then
                        If String.IsNullOrEmpty(str) Then
                            str = "Kosongkan (Kota / Kabupaten)"
                        Else
                            str += "\nKosongkan (Kota / Kabupaten)"
                        End If
                        result = False
                    End If
                Case Is = 30
                    If ddlPreArea.SelectedIndex > 0 AndAlso Len(area) > 4 Then
                        If String.IsNullOrEmpty(str) Then
                            str = "Gunakan KAB / KOTA atau kosongkan (Kota / Kabupaten)"
                        Else
                            str += "\nGunakan KAB / KOTA atau kosongkan (Kota / Kabupaten)"
                        End If
                        result = False
                    End If
                Case Is = 31
                    If ddlPreArea.SelectedIndex > 0 AndAlso Len(area) > 3 Then
                        If String.IsNullOrEmpty(str) Then
                            str = "Gunakan KAB atau kosongkan (Kota / Kabupaten)"
                        Else
                            str += "\nGunakan KAB atau kosongkan (Kota / Kabupaten)"
                        End If
                        result = False
                    End If
                Case Is < 30
                    If ddlPreArea.SelectedIndex > 0 AndAlso Len(area) > 5 Then
                        If Len(city.CityName) > 25 Then
                            If String.IsNullOrEmpty(str) Then
                                str = "Gunakan KAB / KOTA / KODYA"
                            Else
                                str += "\nGunakan KAB / KOTA / KODYA"
                            End If
                            result = False
                        End If
                    End If
            End Select
        End If

        If result Then
            Return result
        Else
            MessageBox.Show(str)
            Return result
        End If

    End Function

    Private Function GetFleetCustomer()
        objFleetCustomer = New FleetCustomer()
        objFleetCustomer.CategoryIndex = ddlCategory.SelectedValue
        objFleetCustomer.TipeCustomer = ddlCategory.SelectedValue
        objFleetCustomer.IdentityType = ddlIdentityType.SelectedValue

        If objFleetCustomer.CategoryIndex = 1 Then
            objFleetCustomer.TypeIndex = CType(ddlTipePerusahaan.SelectedValue, Integer)
            objFleetCustomer.ClassificationIndex = -1
        ElseIf objFleetCustomer.CategoryIndex = 2 Then
            objFleetCustomer.ClassificationIndex = CType(ddlClassification.SelectedValue, Integer)
            objFleetCustomer.TypeIndex = -1
        End If

        Dim vwi_businessSectorFacade As New VWI_BusinessSectorFacade(User)
        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VWI_BusinessSector), "ID", MatchType.Exact, ddlBusinessSector.SelectedValue))

        Dim businessSectorArrayList = vwi_businessSectorFacade.Retrieve(crt)
        Dim businessSector As VWI_BusinessSector = CType(businessSectorArrayList(0), VWI_BusinessSector)
        objFleetCustomer.BusinessSectorDetailId = businessSector.ID

        Dim facCustomerGroup As New CustomerGroupFacade(User)
        Dim customerGroup As CustomerGroup = facCustomerGroup.RetrieveByCode(txtGroupName.Text)
        If customerGroup.ID = 0 Then
            objFleetCustomer.CustomerGroup = New CustomerGroup(ID:=1)
        Else
            objFleetCustomer.CustomerGroup = customerGroup
        End If

        'Dim code As String = lblTypePerusahaanSelect.Text & lblCustBusinessSelect.Text & objFleetCustomer.CustomerGroup.Code & txtCode.Text.ToString().PadLeft(3, "0")
        'Dim code As String = txtCode.Text.ToString().PadLeft(5, "0")
        objFleetCustomer.Code = txtCode.Text

        objFleetCustomer.IdentityNumber = txtIdentityNumber.Text

        objFleetCustomer.Name = txtName.Text
        objFleetCustomer.Gedung = txtGedung.Text
        objFleetCustomer.Alamat = txtAlamat.Text
        objFleetCustomer.ProvinceID = CType(ddlPropinsi.SelectedValue, Integer)
        objFleetCustomer.PreArea = ddlPreArea.SelectedValue

        objFleetCustomer.City = New City(ddlKota.SelectedValue)
        objFleetCustomer.Kecamatan = txtKecamatan.Text
        objFleetCustomer.Kelurahan = txtKelurahan.Text
        objFleetCustomer.Email = txtEmail.Text
        objFleetCustomer.PhoneNo = txtNotlp.Text
        'objFleetCustomer.NPWP = txtNoNPWP.Text
        objFleetCustomer.IdentityNumber = txtIdentityNumber.Text
        objFleetCustomer.PostalCode = txtKodepos.Text

    End Function

    Private Function GetFleetCustomerContact(ByVal fleetCustomerID As Integer) As List(Of FleetCustomerContact)
        Dim list As New List(Of FleetCustomerContact)

        For Each dt As DataGridItem In dtgMain.Items
            objFleetCustomerContact = New FleetCustomerContact()

            Dim lblID As Label = dt.FindControl("lblID")
            Dim lblName As Label = dt.FindControl("lblName")
            Dim lblPosition As Label = dt.FindControl("lblPosition")
            Dim lblPhone As Label = dt.FindControl("lblPhone")
            Dim lblHandphone As Label = dt.FindControl("lblHandphone")
            Dim lblEmail As Label = dt.FindControl("lblEmail")

            Dim dtr As DataRowView = CType(dt.DataItem, DataRowView)

            objFleetCustomerContact.ID = lblID.Text
            objFleetCustomerContact.FleetCustomerID = fleetCustomerID
            objFleetCustomerContact.Name = lblName.Text
            objFleetCustomerContact.Position = lblPosition.Text
            objFleetCustomerContact.PhoneNo = lblPhone.Text
            objFleetCustomerContact.Handphone = lblHandphone.Text
            objFleetCustomerContact.Email = lblEmail.Text

            list.Add(objFleetCustomerContact)
        Next

        Return list
    End Function

    'Private Function GetFleetHasilSurverHeader(ByVal fleetCustomerID As Integer)
    '    objFleetHasilSurveyHeader = New FleetHasilSurveyHeader()
    '    objFleetHasilSurveyHeader.FleetCustomerID = fleetCustomerID

    'End Function

    'Private Function GetFleetHasilSurveyDetail(ByVal fleetHasilSurveyHeaderID As Integer) As List(Of FleetHasilSurveyDetail)

    '    Dim facCompetitor As New CompetitorFacade(User)
    '    Dim listCol As New List(Of String)
    '    Dim list As New List(Of FleetHasilSurveyDetail)

    '    For Each dtCol As DataGridColumn In dtgKepemilikanKendaraan.Columns
    '        listCol.Add(dtCol.HeaderText)
    '    Next

    '    For Each dt As DataGridItem In dtgKepemilikanKendaraan.Items

    '        Dim lblVehicleClassID As Label = dt.FindControl("lblVehicleClassID")

    '        For i As Integer = 3 To listCol.Count - 1
    '            objFleetHasilSurveyDetail = New FleetHasilSurveyDetail()
    '            objFleetHasilSurveyDetail.FleetHasilSurveyHeaderID = fleetHasilSurveyHeaderID
    '            If (listCol(i) = "Mitsubishi") Then
    '                objFleetHasilSurveyDetail.CompetitorID = 1
    '            Else
    '                Dim getCompetitor As Competitor = facCompetitor.GetCompetitorByName(listCol(i))
    '                objFleetHasilSurveyDetail.CompetitorID = getCompetitor.ID
    '            End If

    '            Dim txtAmount As TextBox = dt.FindControl("txt" & listCol(i))
    '            objFleetHasilSurveyDetail.VehicleClassID = lblVehicleClassID.Text
    '            objFleetHasilSurveyDetail.SelisihHasilSurvey = txtAmount.Text

    '            objFleetHasilSurveyDetail.Amount = txtAmount.Text

    '            list.Add(objFleetHasilSurveyDetail)
    '        Next

    '    Next

    '    Return list
    'End Function

    Private Function GetFleetCustomertoDealer(ByVal fleetCustomerID As Integer) As List(Of FleetCustomerToDealer)
        Dim list As New List(Of FleetCustomerToDealer)

        For Each dt As DataGridItem In dtgDealerSelection.Items
            Dim lblDealerID As Label = dt.FindControl("lblDealerID")
            objFleetCustomerToDealer = New FleetCustomerToDealer()
            objFleetCustomerToDealer.FleetCustomerID = fleetCustomerID
            objFleetCustomerToDealer.DealerID = lblDealerID.Text

            list.Add(objFleetCustomerToDealer)
        Next

        Return list
    End Function

    Private Sub dtgMain_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgMain.ItemCommand
        _arlFCContact = CType(sesHelper.GetSession(varSessFCContact), ArrayList)
        Select Case e.CommandName.ToLower()
            Case "add" 'Insert New item to datagrid
                AddData(_arlFCContact, e)
                sesHelper.SetSession(varSessFCContact, _arlFCContact)

            Case "delete" 'Delete this datagrid item 
                _arlFCContact.RemoveAt(e.Item.ItemIndex)
                dtgMain.DataSource = _arlFCContact
                dtgMain.DataBind()
                sesHelper.SetSession(varSessFCContact, _arlFCContact)

        End Select
    End Sub

    Protected Sub dtgMain_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        If Not e.Item.ItemIndex = -1 Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.EditItem Then
            SetDtgFCContactItem(e)
        End If

    End Sub

    Private Sub SetDtgFCContactItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", _
                New CommonFunction().PreventDoubleClickAtGrid(CType(e.Item.FindControl("lbtnDelete"), LinkButton), "Yakin Data ini akan dihapus?"))
        End If
    End Sub

    Private Sub BindGridFCContact()
        Dim arrL As ArrayList
        arrL = sesHelper.GetSession(Me.varSessFCContact)

        Me.dtgMain.DataSource = arrL
        Me.dtgMain.DataBind()

    End Sub

    Private Sub BindGridKepemilikanKendaraan()
        Dim dt As DataTable
        dt = sesHelper.GetSession(Me.varSessKepemilikanKendaraan)

        Me.dtgKepemilikanKendaraan.DataSource = dt
        Me.dtgKepemilikanKendaraan.DataBind()

    End Sub


    'Private Sub GenerateDtGridKepemilikanKendaraan()

    '    ' get list competitor
    '    Dim fCompetitor As New CompetitorFacade(User)
    '    Dim dtcompetitor As ArrayList = fCompetitor.RetrieveActiveList()

    '    Dim dt As New DataTable()
    '    dt.Columns.Add("Id", GetType(Integer))
    '    dt.Columns.Add("VehicleClassID", GetType(Integer))
    '    dt.Columns.Add("VehicleClass", GetType(String))
    '    dt.Columns.Add("Mistubishi", GetType(String))
    '    For Each item As Competitor In dtcompetitor
    '        dt.Columns.Add(item.Name)
    '    Next

    '    ' get list vehicle class
    '    Dim fVehicleClass As New VehicleClassFacade(User)
    '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    criterias.opAnd(New Criteria(GetType(VehicleClass), "Status", MatchType.Exact, "0"))
    '    Dim dtVehicleClass As ArrayList = fVehicleClass.RetrieveByCriteria(criterias)

    '    Dim i As Integer = 1
    '    For Each itemClass As VehicleClass In dtVehicleClass
    '        Dim row As DataRow = dt.NewRow
    '        row("Id") = i
    '        row("VehicleClassID") = itemClass.ID
    '        row("VehicleClass") = itemClass.Description
    '        row("Mistubishi") = "0"
    '        For Each item As Competitor In dtcompetitor
    '            row(item.Name) = "0"
    '        Next

    '        dt.Rows.Add(row)
    '        i = i + 1
    '    Next

    '    Me.sesHelper.SetSession(varSessKepemilikanKendaraan, dt)
    '    BindGridKepemilikanKendaraan()

    'End Sub

#Region "dynamic datagrid"
    'Private Sub InitGridKepemilikanKendaraan()

    '    ' get list competitor
    '    Dim fCompetitor As New CompetitorFacade(User)
    '    Dim dtcompetitor As ArrayList = fCompetitor.RetrieveActiveList()

    '    Dim templateColumn As New TemplateColumn()
    '    templateColumn.HeaderText = "No"
    '    templateColumn.HeaderStyle.CssClass = "titleTableSales"
    '    templateColumn.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "No")
    '    templateColumn.ItemTemplate = New DataGridTemplate(ListItemType.Item, "No", True)
    '    templateColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center
    '    templateColumn.ItemStyle.Width = Unit.Percentage(3)
    '    dtgKepemilikanKendaraan.Columns.Add(templateColumn)

    '    templateColumn = New TemplateColumn()
    '    templateColumn.Visible = False
    '    templateColumn.HeaderText = "VehicleClassID"
    '    templateColumn.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "Vehicle Class Id")
    '    templateColumn.ItemTemplate = New DataGridTemplate(ListItemType.Item, "VehicleClassID", True)
    '    dtgKepemilikanKendaraan.Columns.Add(templateColumn)

    '    templateColumn = New TemplateColumn()
    '    templateColumn.HeaderText = "VehicleClass"
    '    templateColumn.HeaderStyle.CssClass = "titleTableSales"
    '    templateColumn.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "Kelas Kendaraan")
    '    templateColumn.ItemTemplate = New DataGridTemplate(ListItemType.Item, "VehicleClass", True)
    '    dtgKepemilikanKendaraan.Columns.Add(templateColumn)

    '    templateColumn = New TemplateColumn()
    '    templateColumn.HeaderText = "Mitsubishi"
    '    templateColumn.HeaderStyle.CssClass = "titleTableSales"
    '    templateColumn.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "Mitsubishi")
    '    If (sessHelper.GetSession("Status") = "View") Then
    '        templateColumn.ItemTemplate = New DataGridTemplate(ListItemType.Item, "Mitsubishi", False, True)
    '    Else
    '        templateColumn.ItemTemplate = New DataGridTemplate(ListItemType.Item, "Mitsubishi")
    '    End If
    '    dtgKepemilikanKendaraan.Columns.Add(templateColumn)

    '    For Each itemCompetitor As Competitor In dtcompetitor
    '        templateColumn = New TemplateColumn()
    '        templateColumn.HeaderText = itemCompetitor.Name
    '        templateColumn.HeaderStyle.CssClass = "titleTableSales"
    '        templateColumn.HeaderTemplate = New DataGridTemplate(ListItemType.Header, itemCompetitor.Name)
    '        If (sessHelper.GetSession("Status") = "View") Then
    '            templateColumn.ItemTemplate = New DataGridTemplate(ListItemType.Item, itemCompetitor.Name, False, True)
    '        Else
    '            templateColumn.ItemTemplate = New DataGridTemplate(ListItemType.Item, itemCompetitor.Name)
    '        End If
    '        dtgKepemilikanKendaraan.Columns.Add(templateColumn)
    '    Next

    'End Sub

    Private Class DataGridTemplate
        Implements ITemplate
        Dim templateType As ListItemType
        Dim columnName As String
        Dim isLabelOnly As Boolean
        Dim isDisable As Boolean

        Sub New(ByVal type As ListItemType, ByVal ColName As String, Optional ByVal isLblOnly As Boolean = False, Optional ByVal isDisableTxt As Boolean = False)
            templateType = type
            columnName = ColName
            isLabelOnly = isLblOnly
            isDisable = isDisableTxt
        End Sub

        Sub InstantiateIn(ByVal container As Control) _
           Implements ITemplate.InstantiateIn
            Dim literal As New Literal()
            Select Case templateType
                Case ListItemType.Header
                    literal.Text = "<B>" & columnName & "</B>"
                    container.Controls.Add(literal)
                Case ListItemType.Item
                    If isLabelOnly Then
                        Dim lbl As New Label
                        lbl.ID = "lbl" & columnName
                        container.Controls.Add(lbl)
                    Else
                        Dim txt1 As New TextBox
                        If isDisable Then
                            txt1.Enabled = False
                        End If
                        txt1.ID = "txt" & columnName
                        txt1.Width = Unit.Percentage(100)
                        txt1.Attributes.Add("onblur", "NumOnlyBlurWithOnGridTxt(this,'0');")
                        txt1.Attributes.Add("onkeypress", "return numericOnlyUniv(event)")

                        txt1.Text = "0"
                        txt1.CssClass = "textRight"
                        container.Controls.Add(txt1)
                    End If

                Case ListItemType.EditItem
                    Dim txt2 As New TextBox()
                    txt2.Text = ""
                    container.Controls.Add(txt2)
                Case ListItemType.Footer
                    literal.Text = "<I>Footer</I>"
                    container.Controls.Add(literal)
            End Select
        End Sub
    End Class

#End Region

    Private Sub dtgKepemilikanKendaraan_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgKepemilikanKendaraan.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.EditItem Then

            If Not IsNothing(e.Item.DataItem) Then
                Try
                    Dim dtRow As DataRowView = CType(e.Item.DataItem, DataRowView)
                    Dim dtr As DataRow = dtRow.Row
                    Dim lblNo As Label = e.Item.FindControl("lblNo")
                    lblNo.Text = (e.Item.ItemIndex + 1).ToString()
                    Dim lblVehicleClass As Label = e.Item.FindControl("lblVehicleClass")
                    lblVehicleClass.Text = dtr("VehicleClass").ToString()
                    Dim lblVehicleClassID As Label = e.Item.FindControl("lblVehicleClassID")
                    lblVehicleClassID.Text = dtr("VehicleClassID").ToString()

                    For i As Integer = 3 To dtgKepemilikanKendaraan.Columns.Count - 1
                        Dim columnName As String = dtgKepemilikanKendaraan.Columns(i).HeaderText
                        Dim amount As String = dtr(columnName).ToString
                        If String.IsNullOrEmpty(amount) Then
                            amount = 0
                        End If

                        Dim txtColumnName As TextBox = e.Item.FindControl("txt" & columnName)
                        If Not IsNothing(txtColumnName) Then
                            txtColumnName.Text = amount
                        End If
                    Next

                Catch ex As Exception

                End Try

            End If
        End If
    End Sub

    Protected Sub dtgDealerSelection_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgDealerSelection.ItemCommand
        _arlDealer = CType(sesHelper.GetSession(varSessDealer), ArrayList)
        Select Case e.CommandName.ToLower()

            Case "delete" 'Delete this datagrid item 
                _arlDealer.RemoveAt(e.Item.ItemIndex)
                dtgDealerSelection.DataSource = _arlDealer
                dtgDealerSelection.DataBind()
                sesHelper.SetSession(varSessDealer, _arlDealer)

        End Select
    End Sub

    Protected Sub btnDealerHelper_Click(sender As Object, e As EventArgs) Handles btnDealerHelper.Click
        'get dealer code from textbox hidden (xxx;xxx;xxx;)
        Dim dealerCodeList As String = txtDealerCodeList.Value
        If Not String.IsNullOrEmpty(dealerCodeList) Then
            Dim arl As ArrayList = New ArrayList()

            ' split dealer code text
            Dim stringSeparators() As String = {";"}
            Dim splitDealerCode() As String = dealerCodeList.Split(stringSeparators, StringSplitOptions.None)
            Dim strDealer As String = String.Empty
            For Each Str As String In splitDealerCode
                If String.IsNullOrEmpty(strDealer) Then
                    strDealer = "('" & Str & "'"
                Else
                    strDealer += "," & "'" & Str & "'"
                End If
            Next
            strDealer += ")"

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.InSet, strDealer))
            Dim obj As ArrayList = New DealerFacade(User).RetrieveByCriteria(criterias)

            ' get session array list selected dealer
            arl = sesHelper.GetSession(varSessDealer)
            ' add new selected dealer to session array
            If Not arl Is Nothing Then
                For Each item As Dealer In obj
                    arl.Add(item)
                Next
            Else
                arl = obj
            End If
            dtgDealerSelection.DataSource = arl
            dtgDealerSelection.DataBind()
            sesHelper.SetSession(varSessDealer, arl)
        End If
    End Sub

    Protected Sub dtgDealerSelection_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgDealerSelection.ItemDataBound
        If Not e.Item.ItemIndex = -1 Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1
        End If

        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As Dealer = CType(e.Item.DataItem, Dealer)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblGroup As Label = CType(e.Item.FindControl("lblGroup"), Label)
                If Not IsNothing(RowValue.DealerGroup) Then
                    lblGroup.Text = RowValue.DealerGroup.GroupName
                End If
                Dim lblCity As Label = CType(e.Item.FindControl("lblCity"), Label)
                If Not IsNothing(RowValue.City) Then
                    lblCity.Text = RowValue.City.CityName
                End If
            End If
        End If
    End Sub

    Protected Sub lbtnDownload_Click(sender As Object, e As EventArgs) Handles lbtnDownload.Click
        Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("FleetCustomerDir") & "\" & CType(sessHelper.GetSession("FleetCustomer"), FleetCustomer).Attachment
        Response.Redirect("../Download.aspx?file=" & PathFile)
    End Sub

    Protected Sub lnkbtnDeleteAttachment_Click(sender As Object, e As EventArgs) Handles lnkbtnDeleteAttachment.Click
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Try
            success = imp.Start()
            If success Then
                objFleetCustomer = CType(sessHelper.GetSession("FleetCustomer"), FleetCustomer)
                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("FleetCustomerDir") & "\" & objFleetCustomer.Attachment

                Dim finfo As New FileInfo(DestFile)

                If finfo.Exists Then
                    finfo.Delete()

                    objFleetCustomer.Attachment = String.Empty
                    Dim nResult As Integer = New FleetCustomerFacade(User).Update(objFleetCustomer)
                    sessHelper.SetSession("FleetCustomer", New FleetCustomerFacade(User).Retrieve(nResult))
                    objFleetCustomer = CType(sessHelper.GetSession("FleetCustomer"), FleetCustomer)
                    ViewState("Mode") = enumMode.Mode.EditMode
                    FillForm()
                End If

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FillForm()
        BindDropDown()
        dtgMain.DataSource = New ArrayList
        dtgMain.DataBind()

        Dim idFleetCustomer = sessHelper.GetSession(varSessFleetCustomerID)
        Me.sesHelper.SetSession(varSessFCContact, New ArrayList)

        If Convert.ToString(sessHelper.GetSession("Status")) = "Update" Then
            GetValueFromDb(idFleetCustomer)
            If (Convert.ToString(sessHelper.GetSession(varSessStatusUpdate)) = "UpdateFleet") Then
                pnlKepemilikanKendaraan.Visible = False
                pnlOtorisasiDealer.Visible = False
                lblTitle.Text = "FLEET MANAGEMENT - Update Fleet Customer"
            ElseIf Convert.ToString(sessHelper.GetSession(varSessStatusUpdate)) = "UpdateDealer" Then
                pnlFleetCustomer.Visible = False
                pnlKepemilikanKendaraan.Visible = False
                lblTitle.Text = "FLEET MANAGEMENT - Assign to Dealer"
            End If

        ElseIf Convert.ToString(sessHelper.GetSession("Status")) = "View" Then

            GetValueFromDb(idFleetCustomer)

            btnSave.Visible = False
            lblPencarianDealer.Visible = False
            lblPopUpDealer.Visible = False
            lblTitle.Text = "FLEET MANAGEMENT - Fleet Customer Detail"
            SetForm(False)

        Else
            'GenerateDtGridKepemilikanKendaraan()
            sessHelper.SetSession("Status", "Insert")

        End If

        'If lblGroupNameSelect.Text = String.Empty Then
        '    lblGroupNameSelect.Text = New CustomerGroupFacade(User).Retrieve(1).Code
        'End If
    End Sub

    Private Sub SetForm(ByVal bool As Boolean)
        ddlCategory.Enabled = bool
        ddlTipePerusahaan.Enabled = bool
        ddlIdentityType.Enabled = bool
        ddlClassification.Enabled = bool
        ddlBusinessSector.Enabled = bool
        'txtGroupName.Enabled = bool
        txtCode.Enabled = False
        txtName.Enabled = bool
        txtGedung.Enabled = bool
        txtAlamat.Enabled = bool
        ddlPropinsi.Enabled = bool
        ddlPreArea.Enabled = bool
        ddlKota.Enabled = bool
        txtKecamatan.Enabled = bool
        txtKelurahan.Enabled = bool
        txtKodepos.Enabled = bool
        txtEmail.Enabled = bool
        txtNotlp.Enabled = bool
        'txtNoNPWP.Enabled = bool
        txtIdentityNumber.Enabled = bool

        dtgMain.ShowFooter = bool
        dtgKepemilikanKendaraan.ShowFooter = bool
        dtgDealerSelection.ShowFooter = bool
        dtgMain.Columns.Item(dtgMain.Columns.Count - 1).Visible = bool
        dtgDealerSelection.Columns.Item(dtgDealerSelection.Columns.Count - 1).Visible = bool

    End Sub

    Private Sub GetValueFromDb(ByVal id As Integer)
        Dim fleetCustomer As FleetCustomer
        Dim fleetCustomertoDealers As ArrayList

        If Not IsNothing(sessHelper.GetSession("FleetCustomer")) Then
            fleetCustomer = sessHelper.GetSession("FleetCustomer")
            fleetCustomer.ID = 0
            If Not IsNothing(sesHelper.GetSession("FleetCustomerToDealer")) Then
                fleetCustomertoDealers = sesHelper.GetSession("FleetCustomerToDealer")
            End If
        Else
            fleetCustomer = facFleetCustomer.Retrieve(id)
            fleetCustomertoDealers = facFleetCustomerToDealer.RetrieveWithOneCriteria(1, 100, 1000, "FleetCustomerID", MatchType.Exact, id.ToString())
        End If

        Dim fleetCustomerContacts As ArrayList = facFleetCustomerContact.RetrieveWithOneCriteria(1, 100, 1000, "FleetCustomerID", MatchType.Exact, id.ToString())
        Dim businessSector As VWI_BusinessSector = New VWI_BusinessSectorFacade(User).Retrieve(fleetCustomer.BusinessSectorDetailId)

        ddlCategory.SelectedValue = fleetCustomer.CategoryIndex
        If fleetCustomer.CategoryIndex <> -1 Then
            ddlCategory_SelectedIndexChanged(Me, System.EventArgs.Empty)
        End If

        hdnFleetCustomerID.Value = fleetCustomer.ID
        ddlTipePerusahaan.SelectedValue = fleetCustomer.TypeIndex
        If fleetCustomer.TypeIndex <> -1 Then
            ddlTipePerusahaan_TextChanged(Me, System.EventArgs.Empty)
        End If

        ddlIdentityType.SelectedValue = fleetCustomer.IdentityType

        ddlClassification.SelectedValue = fleetCustomer.ClassificationIndex
        If fleetCustomer.ClassificationIndex <> -1 Then
            ddlClassification_SelectedIndexChanged(Me, System.EventArgs.Empty)
        End If

        ddlBusinessSector.SelectedValue = businessSector.ID
        If fleetCustomer.BusinessSectorDetailId <> -1 Then
            ddlBusinessSector_SelectedIndexChanged(Me, EventArgs.Empty)
        End If

        If Not IsNothing(fleetCustomer.CustomerGroup) Then
            txtGroupName.Text = fleetCustomer.CustomerGroup.Code
        End If

        'If fleetCustomer.CategoryIndex <> -1 Then
        '    Dim strCategory = String.Empty
        '    If fleetCustomer.CategoryIndex = 1 Then
        '        strCategory = "CP"
        '    ElseIf fleetCustomer.CategoryIndex = 2 Then
        '        strCategory = "GV"
        '    End If
        '    lblTypePerusahaanSelect.Text = strCategory
        'Else
        '    lblTypePerusahaanSelect.Text = "[Type]"
        'End If

        'lblGroupNameSelect.Text = fleetCustomer.CustomerGroup.Code
        'lblCustBusinessSelect.Text = businessSector.Code
        txtName.Text = fleetCustomer.Name
        txtGedung.Text = fleetCustomer.Gedung
        txtAlamat.Text = fleetCustomer.Alamat
        ddlPropinsi.SelectedValue = fleetCustomer.ProvinceID
        ddlPropinsi_SelectedIndexChanged(Me, System.EventArgs.Empty)

        ddlPreArea.SelectedValue = fleetCustomer.PreArea
        ddlKota.SelectedValue = fleetCustomer.City.ID
        txtKecamatan.Text = fleetCustomer.Kecamatan
        txtKelurahan.Text = fleetCustomer.Kelurahan
        txtKodepos.Text = fleetCustomer.PostalCode
        txtEmail.Text = fleetCustomer.Email
        txtNotlp.Text = fleetCustomer.PhoneNo
        'txtNoNPWP.Text = fleetCustomer.NPWP
        txtIdentityNumber.Text = fleetCustomer.IdentityNumber
        lblAttachment.Text = fleetCustomer.Attachment

        'Dim intCodeLength As Integer = fleetCustomer.Code.Length
        'If (intCodeLength <> 0) Then
        '    txtCode.Text = fleetCustomer.Code.Substring(intCodeLength - 3, 3)
        'End If

        txtCode.Text = fleetCustomer.Code

        ' fill data grid contact
        Dim arrList As New ArrayList()
        If (fleetCustomerContacts.Count > 0) Then
            For Each itemFCContact As FleetCustomerContact In fleetCustomerContacts
                arrList.Add(itemFCContact)
            Next
        End If
        dtgMain.DataSource = arrList
        dtgMain.DataBind()
        sessHelper.SetSession(varSessFCContact, arrList)

        'If fleetCustomer.ID <> 0 Then
        '    Dim arrListHasilSurvey As ArrayList = New ArrayList
        '    Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetHasilSurveyHeader), "FleetCustomerID", MatchType.Exact, fleetCustomer.ID))
        '    arrListHasilSurvey = facFleetHasilSurveyHeader.Retrieve(crt)

        '    If arrListHasilSurvey.Count > 0 Then
        '        ' fill data grid kepemilikan kendraan
        '        Dim dtSetKepemilikanKendaraan As DataSet = facFleetCustomer.RetrieveSp("exec sp_FCD_getVehicleOwnership " & fleetCustomer.ID)
        '        dtgKepemilikanKendaraan.DataSource = dtSetKepemilikanKendaraan.Tables(0)
        '        dtgKepemilikanKendaraan.DataBind()
        '        sessHelper.SetSession(varSessKepemilikanKendaraan, dtSetKepemilikanKendaraan.Tables(0))
        '    Else
        '        'GenerateDtGridKepemilikanKendaraan()
        '    End If

        'End If

        ' fill data grid dealer

        arrList = New ArrayList()
        If (fleetCustomertoDealers.Count > 0) Then
            For Each itemDealer As FleetCustomerToDealer In fleetCustomertoDealers
                Dim dealer As Dealer = New DealerFacade(User).Retrieve(itemDealer.DealerID)
                arrList.Add(dealer)
            Next
        End If
        dtgDealerSelection.DataSource = arrList
        dtgDealerSelection.DataBind()
        sesHelper.SetSession(varSessDealer, arrList)

    End Sub

    Protected Sub ddlTipePerusahaan_TextChanged(sender As Object, e As EventArgs) Handles ddlTipePerusahaan.SelectedIndexChanged

        If Not String.IsNullOrEmpty(hdnGroupName.Value) Then
            'lblGroupNameSelect.Text = hdnGroupName.Value
            txtGroupName.Text = hdnGroupName.Value
        End If

    End Sub


    Protected Sub ddlBusinessSector_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBusinessSector.SelectedIndexChanged
        'lblCustBusinessSelect.Text = "[Profil]"
        'If ddlBusinessSector.SelectedIndex <> 0 Then
        '    lblCustBusinessSelect.Text = ddlBusinessSector.SelectedValue
        'End If

        If Not String.IsNullOrEmpty(hdnGroupName.Value) Then
            'lblGroupNameSelect.Text = hdnGroupName.Value
            txtGroupName.Text = hdnGroupName.Value
        End If
    End Sub

    Protected Sub txtGroupName_TextChanged(sender As Object, e As EventArgs) Handles txtGroupName.TextChanged
        'lblGroupNameSelect.Text = "[Grup]"
        'If txtGroupName.Text <> String.Empty Then
        '    lblGroupNameSelect.Text = txtGroupName.Text
        'End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        RemoveALLSession()
        Response.Redirect("FrmFleetCustomerList.aspx")
    End Sub

    Private Sub RemoveALLSession()
        sessHelper.RemoveSession(varSessFCContact)
        sessHelper.RemoveSession(varSessKepemilikanKendaraan)
        sessHelper.RemoveSession(varSessDealer)
    End Sub


    Protected Sub ddlClassification_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClassification.SelectedIndexChanged
        If Not String.IsNullOrEmpty(hdnGroupName.Value) Then
            'lblGroupNameSelect.Text = hdnGroupName.Value
            txtGroupName.Text = hdnGroupName.Value
        End If
    End Sub
End Class