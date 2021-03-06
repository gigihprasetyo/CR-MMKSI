#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Security
#End Region
#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Configuration
#End Region

Public Class FrmCustomerList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtKodePelanggan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtNama As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlKota As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtAlamat As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgCustomerRequest As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button
    Protected WithEvents ddlPropinsi As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents ddlStatusCustDealer As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ICSampai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlCompany As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chktglVer As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ICHingga As KTB.DNet.WebCC.IntiCalendar

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
    ' Dim sessHelp As SessionHelper = New SessionHelper
    Dim criterias As CriteriaComposite
    Dim criterias2 As CriteriaComposite
    Dim arlCustomer As New ArrayList
    Dim objCust As New CustomerRequest
    Dim objCustTemp As New Customer
    Private objDealer As Dealer
    Private sessHelper As New SessionHelper
    Private mAktivsi As Boolean = False
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.CustomerListSAP_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=KONSUMEN - Daftar Konsumen")
        End If
        mAktivsi = SecurityProvider.Authorize(context.User, SR.CustomerActivation_Privilege)
    End Sub

    Private Sub CheckBtnPriv()
        Me.btnDownload.Enabled = SecurityProvider.Authorize(context.User, SR.CustomerListSAPDonwload_Privilege)
    End Sub

    Private Function CheckBtnAtGridPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.CustomerListViewDetail_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'Put user code to initialize the page here
        InitiateAuthorization()

        If Not IsPostBack Then
            viewstate.Add("IsCustomerRequest", True)
            If Not IsNothing(sessHelper.GetSession("LOGINUSERINFO")) Then
                Dim objUserInfo As UserInfo = sessHelper.GetSession("LOGINUSERINFO")

                If objUserInfo.Dealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                    'txtDealer.ReadOnly = True
                    txtDealer.Attributes.Add("readonly", "readonly")
                    txtDealer.Text = objUserInfo.Dealer.DealerCode
                    lblSearchDealer.Visible = False
                Else
                    txtDealer.ReadOnly = False
                    lblSearchDealer.Visible = True
                End If
                lblSearchDealer.Attributes("onClick") = "ShowPPDealerSelection();"
            End If

            BindDropDown()
            ClearForm()
            GetSessionCriteria()
            CreateCriteriaALL()
            BindDatagrid(dtgCustomerRequest.CurrentPageIndex)

            'If dtgCustomerRequest.Items.Count > 0 Then
            '    btnDownload.Enabled = True
            'Else
            '    btnDownload.Enabled = False
            'End If
        End If
        CheckBtnPriv()
    End Sub
    Private Sub BindDropDown()
        'ddlTipePengajuan.DataSource = New EnumTipePengajuanCustomerRequest().RetrieveType()
        'ddlTipePengajuan.DataTextField = "NameTipe"
        'ddlTipePengajuan.DataValueField = "ValTipe"
        'ddlTipePengajuan.DataBind()
        'ddlTipePengajuan.Items.Insert(0, New ListItem("----", ""))
        'ddlTipePengajuan.SelectedIndex = 0

        'ddlStatus.DataSource = New EnumStatusCustomerRequest().RetrieveType
        'ddlStatus.DataTextField = "NameTipe"
        'ddlStatus.DataValueField = "ValTipe"
        'ddlStatus.DataBind()
        'ddlStatus.Items.Insert(0, New ListItem("----", ""))
        'ddlStatus.SelectedIndex = 0
        Dim Provice_criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        ddlPropinsi.Items.Clear()
        ddlPropinsi.DataSource = New ProvinceFacade(User).RetrieveActiveList(Provice_criteria, "ProvinceName", Sort.SortDirection.ASC)
        ddlPropinsi.DataTextField = "ProvinceName"
        ddlPropinsi.DataValueField = "ID"
        ddlPropinsi.DataBind()
        ddlPropinsi.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlPropinsi.SelectedIndex = 0
        ddlPropinsi_SelectedIndexChanged(Me, System.EventArgs.Empty)

    End Sub

    Private Function isDealerAssignValid(ByVal CurrentDealer As Dealer, ByVal SearchDealerCode As String) As Boolean
        Dim result As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "Status", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(CurrentDealer, User)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, SearchDealerCode))

        result = New DealerFacade(User).RetrieveByCriteria(criterias)

        If result.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function isDealerValid(ByVal DealerCode As String) As Boolean
        Dim _dealer As New Dealer
        _dealer = New DealerFacade(User).Retrieve(DealerCode)

        If _dealer.ID > 0 Then
            objDealer = Session("DEALER")
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If isDealerAssignValid(objDealer, DealerCode) Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return True
            End If
        Else
            Return False
        End If


    End Function
    Private Function CreateCriteriaALL() As Integer
        Dim result As Integer = 1
        Dim strDealerCode As String
        Dim statusrecord As Integer = CInt(ddlStatusCustDealer.SelectedValue)
        objDealer = Session("DEALER")
        If txtDealer.Text <> String.Empty Then
            If isDealerValid(txtDealer.Text) Then
                strDealerCode = txtDealer.Text
            Else
                result = 0
            End If


            Dim ctr As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerDealer), "Dealer.DealerCode", MatchType.Exact, strDealerCode))
            If statusrecord <> -10 Then
                ctr.opAnd(New Criteria(GetType(CustomerDealer), "RowStatus", MatchType.Exact, statusrecord))
            End If

            Dim arrCustomerDealer As ArrayList = New ArrayList
            arrCustomerDealer = New CustomerDealerFacade(User).Retrieve(ctr)
            If arrCustomerDealer.Count > 0 Then
                If (ICSampai.Value > ICHingga.Value) Then
                    MessageBox.Show("Tanggal Hingga Harus Lebih Dari Tanggal Dari")
                    result = 0

                Else
                    CreateCriteria(strDealerCode, statusrecord)
                    CreateCriteriaCustomer(strDealerCode, statusrecord)
                End If
            Else
                result = 0
            End If

        Else
            If (ICSampai.Value > ICHingga.Value) Then
                MessageBox.Show("Tanggal Hingga Harus Lebih Dari Tanggal Dari")
                result = 0
            Else
                objDealer = Session("DEALER")
                If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    Dim _temp As String = objDealer.DealerCode
                    CreateCriteria(_temp, statusrecord)
                    CreateCriteriaCustomer(_temp, statusrecord)
                Else
                    CreateCriteria("", statusrecord)
                    CreateCriteriaCustomer("", statusrecord)
                End If
            End If

        End If
        Return result
    End Function

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim strDealerCode As String
        'objDealer = Session("DEALER")

        'If txtDealer.Text <> String.Empty Then

        '    If Not isDealerValid(txtDealer.Text) Then
        '        MessageBox.Show("Dealer yg di masukan tidak valid")
        '        arlCustomer = New ArrayList
        '        dtgCustomerRequest.DataSource = arlCustomer
        '        dtgCustomerRequest.DataBind()
        '        btnDownload.Enabled = False
        '        Return
        '    End If


        '    Dim ctr As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '    '    ' code ini utk search dealer yg multiple
        '    '    'strDealerCode = txtDealer.Text.Replace(";", "','")
        '    '    'ctr.opAnd(New Criteria(GetType(CustomerDealer), "Dealer.DealerCode", MatchType.InSet, "('" & strDealerCode & "')"))

        '    ctr.opAnd(New Criteria(GetType(CustomerDealer), "Dealer.DealerCode", MatchType.Exact, strDealerCode))
        '    Dim arrCustomerDealer As ArrayList = New ArrayList
        '    arrCustomerDealer = New CustomerDealerFacade(User).Retrieve(ctr)
        '    If arrCustomerDealer.Count <= 0 Then
        '        '        strDealerCode = String.Empty
        '        '        For Each objCustDealer As CustomerDealer In arrCustomerDealer
        '        '            strDealerCode = strDealerCode & objCustDealer.Customer.ID & ","
        '        '        Next
        '        '        strDealerCode = strDealerCode.Substring(0, strDealerCode.Length - 1)
        '        '        'strDealerCode = CreateCriteriaII(strDealerCode)
        '        '        CreateCriteria(strDealerCode)
        '        '        If (indexPage >= 0) Then
        '        '            arlCustomer = New CustomerFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dtgCustomerRequest.PageSize, totalRow, viewstate.Item("SortCol"), viewstate.Item("SortDirection"))
        '        '            dtgCustomerRequest.DataSource = arlCustomer
        '        '            dtgCustomerRequest.VirtualItemCount = totalRow
        '        '            dtgCustomerRequest.DataBind()
        '        '        End If
        '        '    Else
        '        MessageBox.Show(SR.DataNotFound("Customer"))
        '        arlCustomer = New ArrayList
        '        dtgCustomerRequest.DataSource = arlCustomer
        '        dtgCustomerRequest.DataBind()
        '        btnDownload.Enabled = False
        '        Return
        '    End If
        'End If
        'Else
        '    objDealer = Session("DEALER")
        '    If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        '        'Dim _temp As String = DealerFacade.GenerateDealerCodeSelection(objDealer, User).Remove(0, 2)
        '        Dim _temp As String = objDealer.DealerCode
        '        '_temp = _temp.Remove(_temp.Length - 2, 2)
        '        _temp = CreateCriteriaII(_temp)
        '        CreateCriteria(_temp)
        '    Else
        '        CreateCriteria("")
        '    End If
        If (indexPage >= 0) Then
            'CreateCriteriaALL()
            viewstate.Item("IsCustomerRequest") = True
            If IsNothing(CType(sessHelper.GetSession("SEARCHCRITERIA_REQUEST"), CriteriaComposite)) Then
                criterias = New CriteriaComposite(New Criteria(GetType(CustomerRequest), "RowStatus", MatchType.Exact, -100))
                sessHelper.SetSession("SEARCHCRITERIA_REQUEST", criterias)
            End If

            arlCustomer = New CustomerRequestFacade(User).RetrieveByCriteria(CType(sessHelper.GetSession("SEARCHCRITERIA_REQUEST"), CriteriaComposite), indexPage + 1, dtgCustomerRequest.PageSize, totalRow, viewstate.Item("SortCol"), viewstate.Item("SortDirection"))

            If Not (arlCustomer.Count > 0) Then
                If IsNothing(CType(sessHelper.GetSession("SEARCHCRITERIA"), CriteriaComposite)) Then
                    criterias = New CriteriaComposite(New Criteria(GetType(Customer), "RowStatus", MatchType.Exact, -100))
                    sessHelper.SetSession("SEARCHCRITERIA", criterias)
                End If
                viewstate.Item("IsCustomerRequest") = False
                arlCustomer = New CustomerFacade(User).RetrieveByCriteria(CType(sessHelper.GetSession("SEARCHCRITERIA"), CriteriaComposite), indexPage + 1, dtgCustomerRequest.PageSize, totalRow, viewstate.Item("SortCol"), viewstate.Item("SortDirection"))
            End If

            dtgCustomerRequest.DataSource = arlCustomer
            dtgCustomerRequest.VirtualItemCount = totalRow
            dtgCustomerRequest.DataBind()
        End If

        'End If

        btnDownload.Enabled = SecurityProvider.Authorize(context.User, SR.CustomerListSAPDonwload_Privilege)
        ' sessHelper.SetSession("ListCustomer", arlCustomer)
        sessHelper.SetSession("FilterCustomer", criterias)

        'criterias = New CriteriaComposite(New Criteria(GetType(Customer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgCustomerRequest.CurrentPageIndex = 0
        If txtDealer.Text.Trim = "" And ddlStatusCustDealer.SelectedValue <> -10 Then
            MessageBox.Show("Kode Dealer tidak boleh kosong")
            Return
        End If
        If CreateCriteriaALL() <> 0 Then
            BindDatagrid(dtgCustomerRequest.CurrentPageIndex)
        Else
            dtgCustomerRequest.DataSource = New ArrayList
            dtgCustomerRequest.VirtualItemCount = 0
            dtgCustomerRequest.DataBind()
            btnDownload.Enabled = False
        End If

        If dtgCustomerRequest.Items.Count > 0 Then
            btnDownload.Enabled = SecurityProvider.Authorize(context.User, SR.CustomerListSAPDonwload_Privilege)
        Else
            btnDownload.Enabled = False
        End If
    End Sub

    Private Function CreateCriteriaII(ByVal _codes As String) As String

        criterias2 = New CriteriaComposite(New Criteria(GetType(CustomerDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(CustomerDealer), "Dealer.DealerCode", MatchType.InSet, "('" + _codes + "')"))
        Dim _arr As ArrayList = New CustomerDealerFacade(User).RetrieveByCriteria(criterias2)
        Dim _tem As String = ""
        For Each item As CustomerDealer In _arr
            _tem = _tem + item.Customer.ID.ToString + ","
        Next
        If _tem.Length > 0 Then
            _tem = _tem.Substring(0, _tem.Length - 1)
        End If
        Return _tem
    End Function

    Private Sub CreateCriteriaCustomer(ByVal lstCust As String, ByVal statusrecord As String)
        criterias = New CriteriaComposite(New Criteria(GetType(Customer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (txtKodePelanggan.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(Customer), "Code", MatchType.[Partial], txtKodePelanggan.Text.Trim))
        End If

        If txtNama.Text.Trim <> String.Empty Then
            If txtNama.Text.Split(";").Length > 1 Then
                Dim nama As String
                Dim arrNama As ArrayList = New ArrayList

                For i As Integer = 0 To txtNama.Text.Split(";").Length - 1
                    nama = txtNama.Text.Trim.Split(";")(i)
                    If nama <> String.Empty Then
                        arrNama.Add(nama)
                    End If
                Next i

                If arrNama.Count > 0 Then
                    If arrNama.Count = 1 Then
                        nama = arrNama(0)
                        criterias.opAnd(New Criteria(GetType(Customer), "Name1", MatchType.[Partial], nama), "(", True)
                        criterias.opOr(New Criteria(GetType(Customer), "Name2", MatchType.[Partial], nama), ")", False)
                    Else
                        For idx As Integer = 0 To arrNama.Count - 1
                            nama = arrNama(idx)
                            If idx = 0 Then
                                criterias.opAnd(New Criteria(GetType(Customer), "Name1", MatchType.[Partial], nama), "(", True)
                                criterias.opOr(New Criteria(GetType(Customer), "Name2", MatchType.[Partial], nama))
                            ElseIf idx = arrNama.Count - 1 Then
                                criterias.opOr(New Criteria(GetType(Customer), "Name1", MatchType.[Partial], nama))
                                criterias.opOr(New Criteria(GetType(Customer), "Name2", MatchType.[Partial], nama), ")", False)
                            Else
                                criterias.opOr(New Criteria(GetType(Customer), "Name1", MatchType.[Partial], nama))
                                criterias.opOr(New Criteria(GetType(Customer), "Name2", MatchType.[Partial], nama))
                            End If
                        Next
                    End If
                End If
            Else
                If txtNama.Text.Replace(";", "").Trim.Length > 0 Then
                    criterias.opAnd(New Criteria(GetType(Customer), "Name1", MatchType.[Partial], txtNama.Text), "(", True)
                    criterias.opOr(New Criteria(GetType(Customer), "Name2", MatchType.[Partial], txtNama.Text), ")", False)
                End If
            End If
        End If


        If lstCust <> String.Empty Then
            Dim strStatus As String = ""
            If statusrecord <> "" And statusrecord <> "-10" Then
                strStatus = " and cd.rowstatus=" & statusrecord

            End If
            criterias.opAnd(New Criteria(GetType(Customer), "ID", MatchType.InSet, "(" & "select Newcustomerid from customerDealer cd join dealer d on(cd.dealerid=d.id) where dealercode='" & lstCust & "' " & strStatus & ")"))
        End If

        If (txtAlamat.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(Customer), "Alamat", MatchType.[Partial], txtAlamat.Text.Trim))
        End If

        If ddlPropinsi.SelectedValue <> 0 Then
            If ddlKota.SelectedValue <> 0 Then
                criterias.opAnd(New Criteria(GetType(Customer), "City.ID", MatchType.Exact, ddlKota.SelectedValue))
            Else
                criterias.opAnd(New Criteria(GetType(Customer), "City.ID", MatchType.InSet, "(" & CriteriaCityColl(ddlPropinsi.SelectedValue) & ")"))
            End If
        End If

        'If DateDiff(DateInterval.Day, icTglPengajuan1.Value, icTglPengajuan2.Value, Microsoft.VisualBasic.FirstDayOfWeek.Monday, FirstWeekOfYear.Jan1) > 0 Then
        '    criterias.opAnd(New Criteria(GetType(Customer), "CreatedTime", MatchType.GreaterOrEqual, icTglPengajuan1.Value))
        '    criterias.opAnd(New Criteria(GetType(Customer), "CreatedTime", MatchType.LesserOrEqual, icTglPengajuan2.Value.AddDays(1)))
        'End If

        If chktglVer.Checked Then
            'If (GetVerification.Length > 0) Then
            '    criterias.opAnd(New Criteria(GetType(Customer), "Code", MatchType.InSet, "(" & GetVerification() & ")"))
            'End If
            criterias.opAnd(New Criteria(GetType(Customer), "Code", MatchType.InSet, "(" & GetVerificationCustomer() & ")"))
        End If

        sessHelper.SetSession("SEARCHCRITERIA", criterias)

    End Sub


    Private Sub CreateCriteria(ByVal lstCust As String, ByVal statusrecord As String)
        criterias = New CriteriaComposite(New Criteria(GetType(CustomerRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (txtKodePelanggan.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(CustomerRequest), "CustomerCode", MatchType.[Partial], txtKodePelanggan.Text.Trim))
        End If

        If txtNama.Text.Trim <> String.Empty Then
            If txtNama.Text.Split(";").Length > 1 Then
                Dim nama As String
                Dim arrNama As ArrayList = New ArrayList

                For i As Integer = 0 To txtNama.Text.Split(";").Length - 1
                    nama = txtNama.Text.Trim.Split(";")(i)
                    If nama <> String.Empty Then
                        arrNama.Add(nama)
                    End If
                Next i

                If arrNama.Count > 0 Then
                    If arrNama.Count = 1 Then
                        nama = arrNama(0)
                        criterias.opAnd(New Criteria(GetType(CustomerRequest), "Name1", MatchType.[Partial], nama), "(", True)
                        criterias.opOr(New Criteria(GetType(CustomerRequest), "Name2", MatchType.[Partial], nama), ")", False)
                    Else
                        For idx As Integer = 0 To arrNama.Count - 1
                            nama = arrNama(idx)
                            If idx = 0 Then
                                criterias.opAnd(New Criteria(GetType(CustomerRequest), "Name1", MatchType.[Partial], nama), "(", True)
                                criterias.opOr(New Criteria(GetType(CustomerRequest), "Name2", MatchType.[Partial], nama))
                            ElseIf idx = arrNama.Count - 1 Then
                                criterias.opOr(New Criteria(GetType(CustomerRequest), "Name1", MatchType.[Partial], nama))
                                criterias.opOr(New Criteria(GetType(CustomerRequest), "Name2", MatchType.[Partial], nama), ")", False)
                            Else
                                criterias.opOr(New Criteria(GetType(CustomerRequest), "Name1", MatchType.[Partial], nama))
                                criterias.opOr(New Criteria(GetType(CustomerRequest), "Name2", MatchType.[Partial], nama))
                            End If
                        Next
                    End If
                End If
            Else
                If txtNama.Text.Replace(";", "").Trim.Length > 0 Then
                    criterias.opAnd(New Criteria(GetType(CustomerRequest), "Name1", MatchType.[Partial], txtNama.Text), "(", True)
                    criterias.opOr(New Criteria(GetType(CustomerRequest), "Name2", MatchType.[Partial], txtNama.Text), ")", False)
                End If
            End If
        End If


        If lstCust <> String.Empty Then
            Dim strStatus As String = ""
            If statusrecord <> "" And statusrecord <> "-10" Then
                strStatus = " and cd.rowstatus=" & statusrecord

            End If
            'criterias.opAnd(New Criteria(GetType(CustomerRequest), "ID", MatchType.InSet, "(" & "select Newcustomerid from customerDealer cd join dealer d on(cd.dealerid=d.id) where dealercode='" & lstCust & "' " & strStatus & ")"))
            criterias.opAnd(New Criteria(GetType(CustomerRequest), "Dealer.ID", MatchType.InSet, "(" & "select DealerID from customerDealer cd join dealer d on(cd.dealerid=d.id) where dealercode='" & lstCust & "' " & strStatus & ")"))

        End If

        If (txtAlamat.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(CustomerRequest), "Alamat", MatchType.[Partial], txtAlamat.Text.Trim))
        End If

        If ddlPropinsi.SelectedValue <> 0 Then
            If ddlKota.SelectedValue <> 0 Then
                criterias.opAnd(New Criteria(GetType(CustomerRequest), "CityID", MatchType.Exact, ddlKota.SelectedValue))
            Else
                criterias.opAnd(New Criteria(GetType(CustomerRequest), "CityID", MatchType.InSet, "(" & CriteriaCityColl(ddlPropinsi.SelectedValue) & ")"))
            End If
        End If

        'If DateDiff(DateInterval.Day, icTglPengajuan1.Value, icTglPengajuan2.Value, Microsoft.VisualBasic.FirstDayOfWeek.Monday, FirstWeekOfYear.Jan1) > 0 Then
        '    criterias.opAnd(New Criteria(GetType(CustomerRequest), "CreatedTime", MatchType.GreaterOrEqual, icTglPengajuan1.Value))
        '    criterias.opAnd(New Criteria(GetType(CustomerRequest), "CreatedTime", MatchType.LesserOrEqual, icTglPengajuan2.Value.AddDays(1)))
        'End If

        If chktglVer.Checked Then
            'If (GetVerification.Length > 0) Then
            '    criterias.opAnd(New Criteria(GetType(CustomerRequest), "CustomerCode", MatchType.InSet, "(" & GetVerification() & ")"))
            'End If
            criterias.opAnd(New Criteria(GetType(CustomerRequest), "CustomerCode", MatchType.InSet, "(" & GetVerificationCustomer() & ")"))
        End If

        sessHelper.SetSession("SEARCHCRITERIA_REQUEST", criterias)

    End Sub

    Private Function GetVerificationCustomer() As String
        Dim sql As String = String.Empty
        sql = "select cr.CustomerCode from CustomerStatusHistory as csh " & _
            "left join CustomerRequest as cr on csh.CustomerRequestID = cr.ID " & _
            "where(csh.NewStatus = 1) " & _
            "and csh.CreatedTime between '" & ICSampai.Value.ToString("MM/dd/yyyy") & "' and '" & ICHingga.Value.ToString("MM/dd/yyyy") & "'"
        Return sql
    End Function

    Private Function CriteriaCityColl(ByVal ProvinceId As Integer) As String

        Dim l_criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        l_criterias.opAnd(New Criteria(GetType(City), "Province.ID", MatchType.Exact, ProvinceId))
        Dim _arr As ArrayList = New CityFacade(User).Retrieve(l_criterias)
        Dim _tem As String = ""
        For Each item As City In _arr
            _tem = _tem + item.ID.ToString + ","
        Next
        If _tem.Length > 0 Then
            _tem = _tem.Substring(0, _tem.Length - 1)
        End If
        Return _tem


    End Function

    Private Function IsCustomerDealerDelete(ByVal objCust As Customer) As Boolean
        Dim l_criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerDealer), "Customer.ID", MatchType.Exact, objCust.ID))
        Dim list As ArrayList = New CustomerDealerFacade(User).Retrieve(l_criterias)
        If list.Count > 0 Then
            For Each item As CustomerDealer In list
                If item.RowStatus = DBRowStatus.Deleted Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Private Function IsCustomerDealerDelete(ByVal objCust As Customer, ByVal objDealer As Dealer) As Boolean
        Dim l_criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerDealer), "Customer.ID", MatchType.Exact, objCust.ID))
        l_criterias.opAnd(New Criteria(GetType(CustomerDealer), "Dealer.ID", MatchType.Exact, objDealer.ID))
        Dim list As ArrayList = New CustomerDealerFacade(User).Retrieve(l_criterias)
        If list.Count > 0 Then
            For Each item As CustomerDealer In list
                If item.RowStatus = DBRowStatus.Deleted Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Private Sub BindWithCustomer(ByRef sender As Object, ByRef e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objCust As Customer

        If e.Item.ItemIndex <> -1 Then
            objCust = arlCustomer(e.Item.ItemIndex)


            Dim _lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            _lblNo.Text = e.Item.ItemIndex + 1 + (dtgCustomerRequest.CurrentPageIndex * dtgCustomerRequest.PageSize)

            Dim _lblKota As Label = CType(e.Item.FindControl("lblKota"), Label)
            _lblKota.Text = objCust.City.CityName ' New CityFacade(User).Retrieve(CInt(objCust.CityID)).CityName

            Dim _lblAlamat As Label = CType(e.Item.FindControl("lblAlamat"), Label)
            _lblAlamat.Text = objCust.Alamat

            Dim _lblNama As Label = CType(e.Item.FindControl("lblNama"), Label)
            'If objCust.Name2.Length > 10 Then
            _lblNama.Text = objCust.Name1 & " " & objCust.Name2
            'Else
            '    _lblNama.Text = objCust.Name1 & " " & objCust.Name2
            'End If

            Dim _lblCustomerCode As Label = CType(e.Item.FindControl("lblCustomerCode"), Label)
            _lblCustomerCode.Text = objCust.Code

            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim objUser As UserInfo = CType(Session("LOGINUSERINFO"), UserInfo)
            lbtnEdit.CommandArgument = objCust.ID.ToString & ";" & objCust.Code

            Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            If CheckBtnAtGridPriv() = False Then
                lbtnView.Visible = False
                lbtnEdit.Visible = False
            Else
                lbtnView.Visible = True
                lbtnEdit.Visible = True
                If objUser.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    lbtnEdit.Visible = False
                Else
                    lbtnView.Visible = False
                End If
            End If
            'request yuki from anton
            'blocking
            lbtnEdit.Visible = False

            Dim lbtnActivation As LinkButton = CType(e.Item.FindControl("lbtnActivation"), LinkButton)
            'lbtnActivation.Text = objCust.Code
            lbtnActivation.CommandArgument = objCust.Code
            lbtnActivation.Visible = mAktivsi
            If objUser.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                objCustTemp = New CustomerFacade(User).RetrieveByCode(objCust.Code)
                'Change By Ags to retrieve CustRequest by Code
                If IsCustomerDealerDelete(objCustTemp, objUser.Dealer) Then

                    e.Item.BackColor = Color.Yellow
                End If
            Else
                If IsCustomerDealerDelete(objCustTemp) Then
                    e.Item.BackColor = Color.Yellow
                End If
            End If

            'lbtnEdit   OK
            lbtnView.Visible = False
            'lbtnActivation.Visible = False
            CType(e.Item.FindControl("lbtnHistoryStatus"), LinkButton).Visible = False
        End If
    End Sub

    Private Sub dtgCustomerRequest_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCustomerRequest.ItemDataBound
        'IsCustomerRequest
        If CType(viewstate.Item("IsCustomerRequest"), Boolean) = False Then
            BindWithCustomer(sender, e)
            Exit Sub
        End If
        If e.Item.ItemIndex <> -1 Then
            objCust = arlCustomer(e.Item.ItemIndex)


            Dim _lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            _lblNo.Text = e.Item.ItemIndex + 1 + (dtgCustomerRequest.CurrentPageIndex * dtgCustomerRequest.PageSize)

            Dim _lblKota As Label = CType(e.Item.FindControl("lblKota"), Label)
            _lblKota.Text = New CityFacade(User).Retrieve(CInt(objCust.CityID)).CityName

            Dim _lblAlamat As Label = CType(e.Item.FindControl("lblAlamat"), Label)
            _lblAlamat.Text = objCust.Alamat

            Dim _lblNama As Label = CType(e.Item.FindControl("lblNama"), Label)
            'If objCust.Name2.Length > 10 Then
            _lblNama.Text = objCust.Name1 & " " & objCust.Name2
            'Else
            '    _lblNama.Text = objCust.Name1 & objCust.Name2
            'End If

            Dim _lblCustomerCode As Label = CType(e.Item.FindControl("lblCustomerCode"), Label)
            _lblCustomerCode.Text = objCust.CustomerCode
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim objUser As UserInfo = CType(Session("LOGINUSERINFO"), UserInfo)

            Try
                lbtnEdit.CommandArgument = objCust.ID.ToString & ";" & objCust.CustomerCode
            Catch ex As Exception
                lbtnEdit.CommandArgument = objCust.ID.ToString & ";" & CType(arlCustomer(e.Item.ItemIndex), Customer).Code
            End Try
            Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            If CheckBtnAtGridPriv() = False Then
                lbtnView.Visible = False
                lbtnEdit.Visible = False
            Else
                lbtnView.Visible = True
                lbtnEdit.Visible = True
                If objUser.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    lbtnEdit.Visible = False
                Else
                    lbtnView.Visible = False
                End If
            End If
            lbtnEdit.Visible = False
            Dim lbtnActivation As LinkButton = CType(e.Item.FindControl("lbtnActivation"), LinkButton)
            'lbtnActivation.Text = objCust.CustomerCode

            lbtnActivation.Visible = mAktivsi
            lbtnActivation.CommandArgument = objCust.CustomerCode
            Dim IsAlreadyChanged As Boolean = False
            If objUser.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                objCustTemp = New CustomerFacade(User).RetrieveByCode(objCust.CustomerCode)
                'Change By Ags to retrieve CustRequest by Code
                If IsCustomerDealerDelete(objCustTemp, objUser.Dealer) Then
                    IsAlreadyChanged = True
                    e.Item.BackColor = Color.Yellow
                End If
            Else
                If IsCustomerDealerDelete(objCustTemp) Then
                    IsAlreadyChanged = True
                    e.Item.BackColor = Color.Yellow
                End If
            End If
            'If IsAlreadyChanged = False AndAlso objCust.MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP Then
            '    e.Item.BackColor = System.Drawing.Color.Gainsboro
            'End If
        End If

    End Sub

    Private Sub dtgCustomerRequest_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCustomerRequest.SortCommand
        If e.SortExpression = viewstate.Item("SortCol") Then
            If viewstate.Item("SortDirection") = Sort.SortDirection.ASC Then
                viewstate.Item("SortDirection") = Sort.SortDirection.DESC
            Else
                viewstate.Item("SortDirection") = Sort.SortDirection.ASC
            End If
        End If
        viewstate.Item("SortCol") = e.SortExpression
        dtgCustomerRequest.SelectedIndex = -1
        dtgCustomerRequest.CurrentPageIndex = 0
        BindDatagrid(0)
    End Sub

    Private Sub dtgCustomerRequest_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCustomerRequest.PageIndexChanged
        dtgCustomerRequest.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgCustomerRequest.CurrentPageIndex)
    End Sub

    'Private Sub BtnRevisi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRevisi.Click
    '    Dim list As ArrayList = PopulateCustomer()
    '    Dim objFacade As CustomerFacade = New CustomerFacade(User)
    '    Try
    '        objFacade.UpdateList(list)
    '        MessageBox.Show("Update berhasil")
    '    Catch ex As Exception
    '        MessageBox.Show("Update tidak berhasil")
    '    End Try
    'End Sub

    Private Function PopulateCustomer() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        For Each oDataGridItem In dtgCustomerRequest.Items
            chkExport = oDataGridItem.FindControl("cbCheck")
            If chkExport.Checked Then
                Dim _customer As Customer = New CustomerFacade(User).Retrieve(CInt(CType(oDataGridItem.FindControl("lblID"), Label).Text))
                _customer.Status = EnumStatusCustomer.ReadyForUpdate.Yes
                oExArgs.Add(_customer)
            End If
        Next
        Return oExArgs
    End Function

    'Private Sub BtnRev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim list As ArrayList = PopulateCustomer()
    '    If list.Count > 0 Then
    '        Dim objFacade As CustomerFacade = New CustomerFacade(User)
    '        Try
    '            objFacade.UpdateList(list)
    '            MessageBox.Show("Update berhasil")
    '        Catch ex As Exception
    '            MessageBox.Show("Update tidak berhasil")
    '        End Try
    '    Else
    '        MessageBox.Show("Tidak ada data yang di pilih.")
    '    End If
    'End Sub

    Private Sub dtgCustomerRequest_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgCustomerRequest.ItemCommand
        If e.CommandName = "AJUKANFAKTUR" Then
            SetSessionCriteria()
            Session("PlgCode") = e.Item().Cells(2).Text
            Response.Redirect("../FinishUnit/FrmPengajuanFaktur.aspx?qxctrvvyuotrpn=" & e.CommandArgument & "&qxctrvvyuotrplgcd=" & e.Item().Cells(2).Text)
        ElseIf e.CommandName = "VIEW" Then
            SetSessionCriteria()
            Response.Redirect("FrmCustomerDetail.aspx?qxctrvvyuotrpn=" & e.CommandArgument & "&qxctrvvyuotrplgcd=" & e.Item().Cells(2).Text)
        ElseIf e.CommandName = "AKTIF" Then
            SetSessionCriteria() 'o2n1
            Response.Redirect("FrmCustomerDealerList.aspx?qxctrvvyuotrpn=" & e.CommandArgument)
        ElseIf e.CommandName = "HISTORY" Then
            SetSessionCriteria()
            Response.Redirect("FrmDaftarStatusPerCustomer.aspx?qxctrvvyuotrpn=" & e.CommandArgument)


        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        Dim list As ArrayList = PopulateCustomer()
        If list.Count > 0 Then
            Dim objFacade As CustomerFacade = New CustomerFacade(User)
            Try
                objFacade.UpdateList(list)
                MessageBox.Show("Update berhasil")
            Catch ex As Exception
                MessageBox.Show("Update tidak berhasil")
            End Try
        Else
            MessageBox.Show("Tidak ada data yang di pilih.")
        End If
    End Sub

    Private Sub ddlPropinsi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPropinsi.SelectedIndexChanged
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
    End Sub

    Private Sub SetSessionCriteria()
        Dim arrLastState As ArrayList = New ArrayList
        arrLastState.Add(txtKodePelanggan.Text)
        arrLastState.Add(txtNama.Text)
        arrLastState.Add(txtAlamat.Text)
        arrLastState.Add(txtDealer.Text)

        arrLastState.Add(ddlPropinsi.SelectedIndex)
        arrLastState.Add(ddlKota.SelectedIndex)
        arrLastState.Add(dtgCustomerRequest.CurrentPageIndex)
        arrLastState.Add(CType(ViewState("SortCol"), String))
        arrLastState.Add(CType(ViewState("SortDirection"), Sort.SortDirection))
        sessHelper.SetSession("SESSIONLASTSTATE", arrLastState)
    End Sub

    Private Sub GetSessionCriteria()
        Dim arrLastState As ArrayList = sessHelper.GetSession("SESSIONLASTSTATE")
        If Not arrLastState Is Nothing Then
            txtKodePelanggan.Text = arrLastState.Item(0)
            txtNama.Text = arrLastState.Item(1)
            txtAlamat.Text = arrLastState.Item(2)
            txtDealer.Text = arrLastState.Item(3)
            ddlPropinsi.SelectedIndex = arrLastState.Item(4)
            ddlPropinsi_SelectedIndexChanged(Me, System.EventArgs.Empty)
            ddlKota.SelectedIndex = arrLastState.Item(5)

            dtgCustomerRequest.CurrentPageIndex = arrLastState.Item(6)
            ViewState("SortCol") = arrLastState.Item(7)
            ViewState("SortDirection") = arrLastState.Item(8)
            'Else
            '    dtgCustomerRequest.CurrentPageIndex = 0
            '    ViewState("SortCol") = "Code"
            '    ViewState("SortDirection") = Sort.SortDirection.ASC
        End If
    End Sub

    Private Sub ClearForm()
        txtKodePelanggan.Text = ""
        txtNama.Text = ""
        txtAlamat.Text = ""
        'txtDealer.Text = ""
        ddlPropinsi.SelectedIndex = 0
        ddlKota.SelectedIndex = 0
    End Sub



    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        DownloadData()
    End Sub

    Private Sub DownloadData()
        Dim strText As StringBuilder
        Dim _str As String = String.Empty
        Dim arrToDownload As New ArrayList
        Dim delimiter As String = Chr(9)
        Dim cuPage As Integer = dtgCustomerRequest.CurrentPageIndex
        Dim i As Integer = 0

        Dim arltoDownload As ArrayList = New ArrayList
        If Not IsNothing(sessHelper.GetSession("FilterCustomer")) Then
            Dim crite As CriteriaComposite
            crite = CType(sessHelper.GetSession("FilterCustomer"), CriteriaComposite)

            arltoDownload = New CustomerFacade(User).Retrieve(crite)
            If arltoDownload.Count > 0 Then
                strText = New StringBuilder
                '-- set Header
                HeaderDownload(strText, arltoDownload.Count.ToString.Length)
                '-- detail value
                For Each objCust As Customer In arltoDownload
                    '-- No
                    strText.Append((CStr(cuPage * dtgCustomerRequest.PageSize + i + 1) & ".").PadRight(arltoDownload.Count.ToString.Length, ""))
                    strText.Append(delimiter)

                    '---- Kode (Kode Pelanggan)
                    strText.Append(objCust.Code.Trim)
                    strText.Append(delimiter)

                    '---- Nama (Nama1&Nama2 -->gabung tanpa spasi)
                    _str = objCust.Name1.Trim & objCust.Name2.Trim
                    strText.Append(_str.Trim)
                    strText.Append(delimiter)

                    '---- Gedung (Nama3)
                    strText.Append(objCust.Name3.Trim)
                    strText.Append(delimiter)

                    '---- Alamat 
                    strText.Append(objCust.Alamat.Trim)
                    strText.Append(delimiter)

                    '---- Kelurahan 
                    strText.Append(objCust.Kelurahan.Trim)
                    strText.Append(delimiter)

                    '---- Kecamatan 
                    strText.Append(objCust.Kecamatan.Trim)
                    strText.Append(delimiter)

                    '---- Kodepos 
                    strText.Append(objCust.PostalCode.Trim)
                    strText.Append(delimiter)

                    '---- Kota 
                    _str = objCust.PreArea.Trim & " " & objCust.City.CityName.Trim
                    strText.Append(_str.Trim)
                    strText.Append(delimiter)

                    '---- Propinsi 
                    strText.Append(objCust.City.Province.ProvinceName.Trim)
                    strText.Append(delimiter)

                    '----new line
                    strText.Append(vbNewLine)
                    i = i + 1
                Next

                Try
                    saveToTextFile(strText.ToString())
                Catch
                    MessageBox.Show("Proses Download gagal")
                    Return
                End Try

                Response.Redirect("../downloadlocal.aspx?file=DataTemp\DaftarKonsumen.txt")

            End If
            'arltoDownload = CType(sessHelper.GetSession("ListCustomer"), ArrayList)


        End If
    End Sub

    Private Sub HeaderDownload(ByRef HeaderTxt As StringBuilder, ByVal RecCount As Integer)
        Dim delimiter As String = Chr(9)
        '--- No
        HeaderTxt.Append("No.")
        HeaderTxt.Append(delimiter)

        '---- Kode (Kode Pelanggan)
        HeaderTxt.Append("Kode.")
        HeaderTxt.Append(delimiter)

        '---- Nama (Nama1&Nama2 -->gabung tanpa spasi)
        HeaderTxt.Append("Nama.")
        HeaderTxt.Append(delimiter)

        '---- Gedung (Nama3)
        HeaderTxt.Append("Gedung.")
        HeaderTxt.Append(delimiter)

        '---- Alamat 
        HeaderTxt.Append("Alamat.")
        HeaderTxt.Append(delimiter)

        '---- Kelurahan 
        HeaderTxt.Append("Kelurahan.")
        HeaderTxt.Append(delimiter)

        '---- Kecamatan 
        HeaderTxt.Append("Kecamatan.")
        HeaderTxt.Append(delimiter)

        '---- Kodepos 
        HeaderTxt.Append("Kode Pos.")
        HeaderTxt.Append(delimiter)

        '---- Kota 
        HeaderTxt.Append("Kota.")
        HeaderTxt.Append(delimiter)

        '---- Propinsi 
        HeaderTxt.Append("Propinsi.")
        HeaderTxt.Append(delimiter)

        '----new line
        HeaderTxt.Append(vbNewLine)

    End Sub

    Private Sub saveToTextFile(ByVal str As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Try
            If imp.Start() Then

                Dim objFileStream As New FileStream(Me.Server.MapPath("") & "\..\DataTemp\DaftarKonsumen.txt", FileMode.Create, FileAccess.Write)
                Dim objStreamWriter As New StreamWriter(objFileStream)

                objStreamWriter.WriteLine(str)
                objStreamWriter.Close()

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Function GetVerification() As String
        'Check CustomerHistory With Verification Date in UI
        '

        Dim _facadeHistory As New CustomerStatusHistoryFacade(User)
        Dim _arrayListHistory As New ArrayList
        Dim _str As New StringBuilder
        Dim criterias As CriteriaComposite
        criterias = New CriteriaComposite(New Criteria(GetType(CustomerStatusHistory), "NewStatus", MatchType.Exact, 1)) 'Validasi
        criterias.opAnd(New Criteria(GetType(CustomerStatusHistory), "CreatedTime", MatchType.GreaterOrEqual, ICSampai.Value))
        criterias.opAnd(New Criteria(GetType(CustomerStatusHistory), "CreatedTime", MatchType.LesserOrEqual, ICHingga.Value))

        _arrayListHistory = _facadeHistory.Retrieve(criterias)

        For Each _history As CustomerStatusHistory In _arrayListHistory
            If (_history.CustomerRequest.CustomerCode.Length > 0) Then
                _str.Append(_history.CustomerRequest.CustomerCode)
                _str.Append(",")
            End If

        Next
        Dim _tstString = _str.ToString()
        If (_str.Length > 0) Then
            _str.Remove(_str.Length - 1, 1)

        End If

        Return _str.ToString()
    End Function

    Function CheckOldBusinessType(ByVal _strCustomerCode As String) As Boolean
        Dim _retval As Boolean = True


        Return _retval
    End Function
End Class
