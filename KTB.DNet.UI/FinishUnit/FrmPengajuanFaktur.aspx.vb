#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.LKPP
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.WebCC
Imports KTB.DNet.BusinessFacade.Profile
Imports System.Linq
Imports System.Collections
Imports System.Collections.Generic
Imports KTB.DNet.BusinessValidation
#End Region

Public Class FrmPengajuanFaktur
    Inherits System.Web.UI.Page


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtCustomerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents lblGedung As System.Web.UI.WebControls.Label
    Protected WithEvents lblAlamat As System.Web.UI.WebControls.Label
    Protected WithEvents lblKecamatan As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodePos As System.Web.UI.WebControls.Label
    Protected WithEvents lblPropinsi As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoKTP As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodya As System.Web.UI.WebControls.Label
    Protected WithEvents lblEmail As System.Web.UI.WebControls.Label
    Protected WithEvents lblPhone As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoRabgka As System.Web.UI.WebControls.Label
    Protected WithEvents dtgPengajuanFaktur As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblNamaDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Phone As System.Web.UI.WebControls.Panel
    Protected WithEvents btnUpdateProfil As System.Web.UI.WebControls.Button
    Protected WithEvents temp As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSalesmanCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblLevel As System.Web.UI.WebControls.Label
    Protected WithEvents lblPosisi As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaSales As System.Web.UI.WebControls.Label
    Protected WithEvents lblShowSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents lblKelurahan As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopUp As System.Web.UI.WebControls.Label
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    'Protected WithEvents lblError1 As System.Web.UI.WebControls.Label
    'Protected WithEvents lblError2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblError3 As System.Web.UI.WebControls.Label
    Protected WithEvents bltError As System.Web.UI.WebControls.BulletedList
    Protected WithEvents tblError As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents trHeader As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trDetail As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents lnkShow As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblName2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtSPKNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoRangka As System.Web.UI.WebControls.HiddenField
    Protected WithEvents lblSPKNumber As System.Web.UI.WebControls.Label
    Protected WithEvents hdnIsMCP As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnMCPConfirmation As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnVerifyMCP As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents imgDealer As System.Web.UI.WebControls.ImageButton
    Protected WithEvents txtMCPNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblMCPNumber As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoFleetReq As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblNoFleetReq As System.Web.UI.WebControls.Label
    Protected WithEvents lblIntitutionName As System.Web.UI.WebControls.Label
    Protected WithEvents lblsearchLKPP As System.Web.UI.WebControls.Label
    Protected WithEvents lblinstitutionName2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtLKPPNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents hdnIsLKPP As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnLKPPConfirmation As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnVerifyLKPP As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lbltxtCustomerCode As System.Web.UI.WebControls.Label
    Protected WithEvents btntxtCustomerCode As System.Web.UI.WebControls.Button
    Protected WithEvents hdnValid As System.Web.UI.WebControls.HiddenField
    Protected WithEvents chkIsTemporary As CheckBox
    Protected WithEvents hfSPKDetailCustomerID As System.Web.UI.WebControls.HiddenField
    Protected WithEvents hdnVC As System.Web.UI.WebControls.HiddenField

    Protected WithEvents fleetTD1 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents fleetTD2 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents fleetTD3 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents MCP_TD1 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents MCP_TD2 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents MCP_TD3 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents MCP_TD4 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents MCP_TD5 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents MCP_TD6 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents Pengadaan_TD1 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents Pengadaan_TD2 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents Pengadaan_TD3 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents Pengadaan_TD4 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents Pengadaan_TD5 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents Pengadaan_TD6 As System.Web.UI.HtmlControls.HtmlTableCell


    Protected WithEvents lblDealerBranch As Label
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variables"
    Private ssHelper As SessionHelper = New SessionHelper
    Private arl As ArrayList = New ArrayList
    Private arlDEL As ArrayList = New ArrayList
    Dim _arrDetail As New ArrayList
    Dim _arrDetailDel As New ArrayList
    Private blockFaktur As String = "Block_Faktur_"
    Private unBlockFaktur As String = "unBlokFaktur_"
    Dim isDealerDMS As Boolean = False
    Dim NoRangka As String
    Private isDealerPiloting As Boolean = False

#End Region

#Region "Custome Methods"

    Private Sub InitData()
        'If Session("sessCM") Is Nothing Then
        Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
        Dim objUser As UserInfo = CType(ssHelper.GetSession("LOGINUSERINFO"), UserInfo)
        Dim mode As String = "add"
        If Not objDealer Is Nothing Then
            lblKodeDealer.Text = objDealer.DealerCode
            lblNamaDealer.Text = objDealer.DealerName

            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                'chkIsTemporary.Visible = True
                'Dim isDealerYana = False
                'Dim crtDS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerSystems), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'crtDS.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerSystems), "Dealer.ID", MatchType.Exact, objDealer.ID))
                'Dim arrDealerSystem As ArrayList = New DealerSystemsFacade(User).Retrieve(crtDS)

                'If arrDealerSystem.Count > 0 Then
                '    Dim objDealerSystem As DealerSystems = arrDealerSystem(0)
                '    If objDealerSystem.SystemID = 2 Then
                '        isDealerYana = True
                '    End If
                'End If

                'If isDealerYana Then
                '    chkIsTemporary.Enabled = False
                'Else
                Dim crtTC As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TransactionControl), "Dealer.ID", MatchType.Exact, objDealer.ID))
                crtTC.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransactionControl), "Kind", MatchType.Exact, CType(EnumDealerTransType.DealerTransKind.TemporaryFaktur, Short)))
                Dim ObjTransControl As TransactionControl = New DealerFacade(User).RetrieveTransactionControlByCriteria(crtTC)

                If Not IsNothing(ObjTransControl) Then
                    If ObjTransControl.Status <> CType(EnumDealerStatus.DealerStatus.Aktive, Short) Then
                        'chkIsTemporary.Enabled = False
                    End If
                End If
                'End If
            End If

            If Not Session("PrevPage") Is Nothing Then
                If Not ssHelper.GetSession("NoSPK") Is Nothing Then
                    txtSPKNumber.Text = CType(ssHelper.GetSession("NoSPK"), String)
                    txtSPKNumber.Enabled = False
                    lblSPKNumber.Visible = False
                End If

                arl = CType(ssHelper.GetSession("sessCM"), ArrayList)
                _arrDetail = CType(ssHelper.GetSession("DeliveryCustomerDetail"), ArrayList)
                ssHelper.SetSession("sessCM", arl)
                txtCustomerCode.Text = CType(ssHelper.GetSession("Customer"), Customer).Code
                If Not ssHelper.GetSession("Salesman") Is Nothing Then
                    txtSalesmanCode.Text = CType(ssHelper.GetSession("Salesman"), SalesmanHeader).SalesmanCode
                    GetSalesInfo(CType(ssHelper.GetSession("Salesman"), SalesmanHeader).SalesmanCode)
                End If
                GetCustomerInfo(CType(Session("Customer"), Customer).Code)
                'If Not ssHelper.GetSession("ADD") Is Nothing Then
                '    AddToArrayList(CType(Session("NoRangka"), String), CType(Session("NoRangkaPengganti"), String), CType(Session("DateFaktur"), String), mode, 0)
                'End If
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
                If Not ssHelper.GetSession("MCPNUMBER") Is Nothing Then
                    txtMCPNumber.Text = CType(ssHelper.GetSession("MCPNUMBER"), String)
                End If

                If Not ssHelper.GetSession("FrmPengajuanFaktur_NoFleetReq") Is Nothing Then
                    txtNoFleetReq.Text = CType(ssHelper.GetSession("FrmPengajuanFaktur_NoFleetReq"), String)
                End If

                If Not ssHelper.GetSession("LKPPNUMBER") Is Nothing Then
                    txtLKPPNumber.Text = CType(ssHelper.GetSession("LKPPNUMBER"), String)
                End If
            Else
                ssHelper.SetSession("sessCM", arl)
                ssHelper.SetSession("DeliveryCustomerDetail", _arrDetail)
                'Dim plgnCode As String = Request.QueryString("qxctrvvyuotrpn")
                'If plgnCode <> String.Empty Then
                '    txtCustomerCode.Text = Request.QueryString("qxctrvvyuotrpn")
                '    GetCustomerInfo(txtCustomerCode.Text)
                'End If


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

        End If
    End Sub

    Private Function getChassisMaster(ByVal objEndCust As EndCustomer) As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.ID", MatchType.Exact, objEndCust.ID))
        Return New ChassisMasterFacade(User).RetrieveByCriteria(criterias)
    End Function

    Private Sub BindDataGrid()
        arl = CType(Session("sessCM"), ArrayList)
        dtgPengajuanFaktur.DataSource = arl
        dtgPengajuanFaktur.DataBind()
        If arl.Count >= 1 Then
            dtgPengajuanFaktur.ShowFooter = False
        End If
    End Sub

    Private Sub ClearCustomerInfo()
        lblName.Text = ""
        lblName2.Text = ""
        lblGedung.Text = ""
        lblAlamat.Text = ""
        lblKelurahan.Text = ""
        lblKecamatan.Text = ""
        lblKodePos.Text = ""
        lblPropinsi.Text = ""
        lblNoKTP.Text = ""
        lblKodya.Text = ""
        lblEmail.Text = ""
        lblPhone.Text = ""
    End Sub

    Private Sub BindCustomer(ByVal objCust As Customer)
        If Len(objCust.Code) > 1 Then
            lbltxtCustomerCode.Text = objCust.Code
            txtCustomerCode.Text = objCust.Code
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
            txtCustomerCode.Text = String.Empty
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

    Private Sub BindSales(ByVal objSales As SalesmanHeader)
        lblPosisi.Text = objSales.JobPosition.Description
        lblLevel.Text = objSales.SalesmanLevel.Description
        lblNamaSales.Text = objSales.Name

        If Not IsNothing(objSales.DealerBranch) Then
            lblDealerBranch.Text = objSales.DealerBranch.DealerBranchCode & " / " & objSales.DealerBranch.Term1
        End If
    End Sub

    Private Function GetCustomerInfo(ByVal code As String) As Boolean
        Dim bcheck As Boolean = True
        Dim objCustomerFacade As CustomerFacade = New CustomerFacade(User)
        Dim objCust As Customer = objCustomerFacade.Retrieve(code)
        Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
        If objCust.ID > 0 Then
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

    Private Function GetSalesInfo(ByVal code As String)
        Dim bcheck As Boolean = True
        Dim objSalesFacade As SalesmanHeaderFacade = New SalesmanHeaderFacade(User)
        Dim objSales As SalesmanHeader = objSalesFacade.Retrieve(code)
        Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
        If objSales.ID > 0 Then
            'CR
            'If IsSalesAvailaibleForLoginDealer(objSales, objDealer) Then
            ssHelper.SetSession("Salesman", objSales)
            BindSales(objSales)
            bcheck = True
            'Else
            '    MessageBox.Show("Kode Salesman Tidak Valid")
            '    bcheck = False
            'End If
        Else
            MessageBox.Show("Kode Salesman Tidak Terdaftar")
            bcheck = False
        End If
        Return bcheck
    End Function

    Private Function IsCustomerAvailaibleForLoginDealer(ByVal objCust As Customer, ByVal loginDealer As Dealer) As Boolean
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(CustomerDealer), "Customer.ID", MatchType.Exact, objCust.ID))
        Dim objCustomerDealerFacade As CustomerDealerFacade = New CustomerDealerFacade(User)
        Dim objList As ArrayList = objCustomerDealerFacade.Retrieve(criterias)
        If Not IsNothing(objList) AndAlso objList.Count > 0 Then
            For Each item As CustomerDealer In objList
                'remakrs by anh req by miyuki 20171013
                'If item.Dealer.ID = loginDealer.ID Then
                '    If item.Customer.RowStatus <> DBRowStatus.Deleted Then
                '        Return True
                '    End If
                'End If
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


    Private Function IsSalesAvailaibleForLoginDealer(ByVal objSales As SalesmanHeader, ByVal loginDealer As Dealer) As Boolean
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "ID", MatchType.Exact, objSales.ID))
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.ID", MatchType.Exact, loginDealer.ID))
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, CType(EnumSalesmanUnit.SalesmanUnit.Unit, Byte)))
        Dim objSalesman As SalesmanHeader = New SalesmanHeaderFacade(User).RetrieveByCriteria(criterias)
        If objSalesman.ID <> 0 Then
            Return True
        Else
            Return False
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

    Private Function isValidFakturDate(ByVal FakturDate As String)
        Try
            Convert.ToDateTime(FakturDate)
        Catch ex As Exception
            Return False
        End Try
        Return True
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

    Private Function IsValidChassisMasterI(ByVal ChassisNumber As String)
        Dim CM As New ChassisMaster
        CM = New FinishUnit.ChassisMasterFacade(User).Retrieve(ChassisNumber)
        Dim CMs As ArrayList = New ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ChassisMaster), "FakturStatus", MatchType.No, CType(EnumChassisMaster.FakturStatus.Baru, Short)))
        criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, ChassisNumber))
        CMs = New FinishUnit.ChassisMasterFacade(User).Retrieve(criterias)
        If (CMs.Count > 0) Then
            Return False
        End If
        Return True
    End Function

    Private Function IsValidChassisMasterII(ByVal ChassisNumber As String) As String
        Dim CM As New ChassisMaster
        'CM = New FinishUnit.ChassisMasterFacade(User).Retrieve(ChassisNumber)
        Dim CMs As ArrayList = New ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ChassisMaster), "FakturStatus", MatchType.Exact, CType(EnumChassisMaster.FakturStatus.Baru, Short)))
        criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, ChassisNumber))
        CMs = New FinishUnit.ChassisMasterFacade(User).Retrieve(criterias)

        'Dim aVK As ArrayList
        'Dim oVK As New VehicleKind
        'Dim oVKFac As New VehicleKindFacade(User)
        'Dim cVK As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'cVK.opAnd(New Criteria(GetType(VehicleKind), "ID", MatchType.Exact, CType(lblVehicleKindID.Text.Trim, Integer)))

        'aVK = oVKFac.Retrieve(cVK)

        If (CMs.Count > 0) Then
            CM = CMs(0)
            'If aVK.Count > 0 Then
            '    CM.VehicleKind = aVK(0)
            'End If

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

    Private Function AddToArrayList(ByVal NoRangka As String, ByVal NoRangkaPengganti As String, ByVal FakturDate As DateTime, ByVal mode As String, ByVal index As Integer, Optional ByVal VehicleKindID As Integer = 1) As ArrayList
        Dim CM As New ChassisMaster
        Dim CM2 As New ChassisMaster
        CM = New FinishUnit.ChassisMasterFacade(User).Retrieve(NoRangka)
        CM2 = New FinishUnit.ChassisMasterFacade(User).Retrieve(NoRangkaPengganti)
        Dim objEndCustomer As EndCustomer
        If getIndex(arl, NoRangka) <> -1 Then
            objEndCustomer = CType(arl(getIndex(arl, NoRangka)), ChassisMaster).EndCustomer
        Else
            objEndCustomer = New EndCustomer
        End If
        objEndCustomer.RefChassisNumberID = CM2.ID
        objEndCustomer.FakturDate = FakturDate

        'If chkIsTemporary.Visible = True Then
        '    objEndCustomer.IsTemporary = CType(CInt(Int(chkIsTemporary.Checked)), Short)
        'ElseIf chkIsTemporary.Visible = False Then
        '    objEndCustomer.IsTemporary = -1
        'End If
        objEndCustomer.IsTemporary = 0

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

    Private Function UpdateArrayList(ByVal NoRangka As String, ByVal NoRangkaPengganti As String, ByVal FakturDate As DateTime) As ArrayList
        Dim CM As New ChassisMaster
        Dim CM2 As New ChassisMaster
        CM = New FinishUnit.ChassisMasterFacade(User).Retrieve(NoRangka)
        CM2 = New FinishUnit.ChassisMasterFacade(User).Retrieve(NoRangkaPengganti)
        Dim objEndCustomer As New EndCustomer
        objEndCustomer.RefChassisNumberID = CM2.ID
        objEndCustomer.FakturDate = FakturDate
        ssHelper.SetSession("DEFAULTDATE", FakturDate)
        CM.EndCustomer = objEndCustomer
        objEndCustomer.MarkLoaded()
        arl.Add(CM)
        If CM.StockStatus <> "X" Then
            Dim oDeliveryCustomerDetil As New DeliveryCustomerDetail
            oDeliveryCustomerDetil.ChassisMaster = CM
            _arrDetail.Add(oDeliveryCustomerDetil)
        End If
    End Function

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

    Private Function IsValidTglFaktur(ByVal FakturDate As Date) As Boolean
        If Date.Now.ToString("ddMMyyyy") = "22042011" Then
            Return True
        Else
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayDate", MatchType.Exact, FakturDate.Day))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayMonth", MatchType.Exact, FakturDate.Month))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayYear", MatchType.Exact, FakturDate.Year))
            Dim arlNationalHoliday As ArrayList = New NationalHolidayFacade(User).RetrieveByCriteria(criterias)
            If arlNationalHoliday.Count > 0 Then
                Dim objTimeSpan As TimeSpan = FakturDate.Subtract(DateTime.Now)
                If objTimeSpan.Days >= 1 Then
                    For i As Integer = 1 To objTimeSpan.Days
                        Dim criterias1 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayDate", MatchType.Exact, FakturDate.AddDays(i * -1).Day))
                        criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayMonth", MatchType.Exact, FakturDate.AddDays(i * -1).Month))
                        criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayYear", MatchType.Exact, FakturDate.AddDays(i * -1).Year))
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

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(SPKHeader), "Dealer.ID", MatchType.Exact, objDealer.ID))
        criterias.opAnd(New Criteria(GetType(SPKHeader), "SPKNumber", MatchType.Exact, txtSPKNumber.Text.Trim))
        Dim objSPKHeaderList As ArrayList = New FinishUnit.SPKHeaderFacade(User).Retrieve(criterias)
        If objSPKHeaderList.Count > 0 Then
            Dim objChassisMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(chassisNumber)
            Dim objSPKHeader As SPKHeader '= objSPKHeaderList(0)
            For Each oHeader As SPKHeader In objSPKHeaderList
                If oHeader.Dealer.DealerGroup.ID = objDealer.DealerGroup.ID Then
                    objSPKHeader = oHeader
                End If
            Next
            If IsNothing(objSPKHeader) Then
                objSPKHeader = objSPKHeaderList(0)
            End If

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
            'ini
            If CheckChassisNumberAndPendingDesc(chassisNumber) Then
                Dim sessPendingDesc = ssHelper.GetSession("sesPENDING_DESC")
                If Not IsNothing(sessPendingDesc) Then
                    msgReturn = msgReturn + vbCrLf + sessPendingDesc.ToString()
                    isAllowed = False
                End If

            End If

        Else
            msgReturn = "Nomor Registrasi SPK tidak terdaftar"
        End If
        ssHelper.SetSession("NoSPK", txtSPKNumber.Text.Trim)
        Return msgReturn
    End Function

    'Private Function IsTemporary(ByVal spkCustomer As SPKCustomer)
    '    If Not IsNothing(spkCustomer.SAPCustomer) AndAlso spkCustomer.SAPCustomer.ID > 0 Then
    '        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '        criterias.opAnd(New Criteria(GetType(SPKCustomer), "SAPCustomer.ID", MatchType.Exact, spkCustomer.SAPCustomer.ID))

    '        Dim listSpkCust As ArrayList = New SPKCustomerFacade(User).Retrieve(criterias)

    '        If listSpkCust.Count > 1 Then
    '            chkIsTemporary.Checked = True
    '        End If
    '    End If
    'End Function

    Public Sub RebindVehicleKind(ByRef e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim txtFooterNomorRangka As TextBox = e.Item.FindControl("txtFooterNomorRangka")
        Dim ddlVehicleKindF As DropDownList = e.Item.FindControl("ddlVehicleKindF")
        Dim ddlVehicleModelF As DropDownList = e.Item.FindControl("ddlVehicleModelF")
        Dim oCM As ChassisMaster
        Dim oCMFac As New ChassisMasterFacade(User)

        txtNoRangka.Value = txtFooterNomorRangka.Text

        oCM = oCMFac.Retrieve(txtFooterNomorRangka.Text)
        If IsNothing(oCM) OrElse oCM.ID < 1 Then
            MessageBox.Show("Nomor Rangka Tidak Terdaftar")
            ClearChassisForm()
            Exit Sub
        ElseIf oCM.FakturStatus <> 0 Then
            MessageBox.Show("Faktur Status tidak sesuai")
            ClearChassisForm()
            Exit Sub
        End If

        If oCM.Category.ProductCategory.Code.Trim <> companyCode Then
            MessageBox.Show("Nomor Rangka tidak terdaftar di PT MMKSI")
            Exit Sub
        End If

        Dim listSpkChassis As ArrayList = New ArrayList
        Dim dealerSystems As DealerSystems = New DealerSystemsFacade(User).RetrieveByDealerCode(objDealer.DealerCode)
        'Dim isDealerDMS As Boolean = False

        If Not IsNothing(dealerSystems) AndAlso dealerSystems.SystemID <> 1 Then
            isDealerDMS = True
        End If
        isDealerPiloting = TCHelper.GetActiveTCResult(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.PilotingSPKMatching))
        If 1 = 1 Then



            If (isDealerPiloting OrElse isDealerDMS) Then
                Dim crSpkChassis As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKChassis), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crSpkChassis.opAnd(New Criteria(GetType(SPKChassis), "MatchingType", MatchType.Exact, 1), "(", True)
                crSpkChassis.opOr(New Criteria(GetType(SPKChassis), "MatchingType", MatchType.Exact, 3), ")", False)
                crSpkChassis.opAnd(New Criteria(GetType(SPKChassis), "ChassisMaster.ChassisNumber", MatchType.Exact, oCM.ChassisNumber))

                listSpkChassis = New SPKChassisFacade(User).Retrieve(crSpkChassis)
                If listSpkChassis.Count = 0 Then
                    MessageBox.Show("Nomor Rangka tidak match dengan SPK")
                    ' is spkmatchfaktur spkchassis shouldbe match(1) or rematch(3)
                    If dealerSystems.isSPKMatchFaktur Then
                        MessageBox.Show("Nomor Rangka tidak match dengan SPK")
                    Else
                        MessageBox.Show("Jenis Kendaraan tidak sesuai dengan customer yang dipilih.")
                    End If
                    'Else
                    '    Dim spkChassis As SPKChassis = CType(listSpkChassis(0), SPKChassis)
                    '    ' if chassis have SPK match, rebind data SPK
                    '    spkHeader = spkChassis.SPKDetail.SPKHeader

                    'If Not IsNothing(spkHeader.SPKCustomer) Then
                    '    IsTemporary(spkHeader.SPKCustomer)
                    'End If

                    '    Dim oSPKDetailCust As SPKDetailCustomer = New SPKDetailCustomerFacade(User).Retrieve(CInt(hfSPKDetailCustomerID.Value))
                    '    If Not IsNothing(oSPKDetailCust.CustomerRequest) AndAlso oSPKDetailCust.CustomerRequest.CustomerCode <> String.Empty Then
                    '        Dim cusCode As String = spkHeader.CustomerRequest.CustomerCode
                    '        ' if spk match enable to cross cutomer dealer
                    '        Dim isCustomerValid As Boolean = GetCustomerInfoForSPKMatch(cusCode)
                    '        If isCustomerValid Then
                    '            Dim objCustomerFacade As CustomerFacade = New CustomerFacade(User)
                    '            Dim objCust As Customer = objCustomerFacade.Retrieve(cusCode)
                    '            BindCustomer(objCust)
                    '            Dim sales As SalesmanHeader = spkHeader.SalesmanHeader
                    '            BindSales(sales)

                    '            txtSPKNumber.Text = spkHeader.SPKNumber
                    '            txtSalesmanCode.Text = sales.SalesmanCode
                    '            If Not IsNothing(sales.DealerBranch) Then
                    '                lblDealerBranch.Text = sales.DealerBranch.Name
                    '            End If
                    '        Else
                    '            ClearCustomerInfo()
                    '        End If
                    '    Else
                    '        MessageBox.Show("Faktur tidak bisa dibuat karena data customer belum tersedia.")
                    '    End If
                    'End If

                    '--add by wdi 20161007              
                    'Else
                    '    MessageBox.Show("Konfigurasi Dealer systems untuk Dealer" + objDealer.DealerName + "(" + objDealer.DealerCode + ") tidak ditemukan")
                End If
            End If

        End If

        If (Not isDealerDMS) And (Not isDealerPiloting) Then
            If hfSPKDetailCustomerID.Value = String.Empty Then
                MessageBox.Show("Silahkan pilih SPK dan Customer terlebih dahulu.")
                Exit Sub
            End If
        End If

        'added 15/1/2021
        Dim oSPKDetailCust As SPKDetailCustomer = New SPKDetailCustomer
        Dim spkHeader As SPKHeader = New SPKHeader
        If dealerSystems.SystemID <> 1 Or isDealerPiloting Then
            If listSpkChassis.Count > 0 Then
                Dim oSPKChassis As SPKChassis = CType(listSpkChassis(0), SPKChassis)
                If oSPKChassis.SPKDetail.SPKDetailCustomers.Count = 0 Then
                    MessageBox.Show("Faktur tidak bisa dibuat karena data customer belum tersedia.")
                    Exit Sub
                End If
                oSPKDetailCust = oSPKChassis.SPKDetail.SPKDetailCustomers(0)
                spkHeader = oSPKChassis.SPKDetail.SPKHeader
                hdnVC.Value = oSPKDetailCust.SPKDetail.VechileColor.ID
                If UnitRemain(oSPKDetailCust.LKPPReference, oSPKDetailCust.SPKDetail.VechileColor.VechileType.ID) > 0 Then
                    txtLKPPNumber.Text = oSPKDetailCust.LKPPReference
                End If
                lblsearchLKPP.Visible = True
            End If
        Else
            oSPKDetailCust = New SPKDetailCustomerFacade(User).Retrieve(CInt(hfSPKDetailCustomerID.Value))
            spkHeader = oSPKDetailCust.SPKDetail.SPKHeader
            hdnVC.Value = oSPKDetailCust.SPKDetail.VechileColor.ID
            If UnitRemain(oSPKDetailCust.LKPPReference, oSPKDetailCust.SPKDetail.VechileColor.VechileType.ID) > 0 Then
                txtLKPPNumber.Text = oSPKDetailCust.LKPPReference
            End If
            lblsearchLKPP.Visible = True
        End If
        'end

        'added by Gerry 11/1/2021
        If dealerSystems.SystemID <> 1 Then
            If Not IsNothing(oSPKDetailCust.CustomerRequest) Then
                If oSPKDetailCust.CustomerRequest.CustomerCode <> String.Empty Then
                    Dim cusCode As String = oSPKDetailCust.CustomerRequest.CustomerCode
                    ' if spk match enable to cross cutomer dealer
                    Dim isCustomerValid As Boolean = GetCustomerInfoForSPKMatch(cusCode)
                    If isCustomerValid Then
                        Dim objCustomerFacade As CustomerFacade = New CustomerFacade(User)
                        Dim objCust As Customer = objCustomerFacade.Retrieve(cusCode)
                        BindCustomer(objCust)
                        Dim sales As SalesmanHeader = spkHeader.SalesmanHeader
                        BindSales(sales)

                        txtSPKNumber.Text = spkHeader.SPKNumber
                        txtSalesmanCode.Text = sales.SalesmanCode
                        If Not IsNothing(sales.DealerBranch) Then
                            lblDealerBranch.Text = sales.DealerBranch.Name
                        End If
                    Else
                        ClearCustomerInfo()
                    End If
                Else
                    MessageBox.Show("Faktur tidak bisa dibuat karena data customer belum tersedia.")
                End If
            Else
                MessageBox.Show("Faktur tidak bisa dibuat karena data customer belum tersedia.")
            End If
        Else
            isDealerPiloting = TCHelper.GetActiveTCResult(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.PilotingSPKMatching))

            If (isDealerPiloting = True) Then
                Dim crSpkChassis As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKChassis), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crSpkChassis.opAnd(New Criteria(GetType(SPKChassis), "MatchingType", MatchType.Exact, 1), "(", True)
                crSpkChassis.opOr(New Criteria(GetType(SPKChassis), "MatchingType", MatchType.Exact, 3), ")", False)
                crSpkChassis.opAnd(New Criteria(GetType(SPKChassis), "ChassisMaster.ChassisNumber", MatchType.Exact, oCM.ChassisNumber))

                listSpkChassis = New SPKChassisFacade(User).Retrieve(crSpkChassis)
                If listSpkChassis.Count = 0 Then
                    'MessageBox.Show("Nomor Rangka tidak match dengan SPK")
                Else
                    Dim oSPKChassis As SPKChassis = CType(listSpkChassis(0), SPKChassis)
                    'oSPKDetailCust = oSPKChassis.SPKDetail.SPKDetailCustomers(0)
                    spkHeader = oSPKChassis.SPKDetail.SPKHeader
                    If Not IsNothing(spkHeader.SalesmanHeader) Then
                        Dim sales As SalesmanHeader = spkHeader.SalesmanHeader
                        BindSales(sales)
                        txtSalesmanCode.Text = sales.SalesmanCode
                        If Not IsNothing(sales.DealerBranch) Then
                            lblDealerBranch.Text = sales.DealerBranch.Name
                        End If
                    End If

                    txtSPKNumber.Text = spkHeader.SPKNumber

                End If
            End If

        End If

        If oCM.Category.CategoryCode.ToUpper() = "CV" Then
            lblNoFleetReq.Visible = True
            'txtNoFleetReq.Enabled = False
        Else '--PC or LCV then disable fleet
            lblNoFleetReq.Visible = False
            txtNoFleetReq.Text = ""
            'txtNoFleetReq.Enabled = False
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

        For Each item As ChassisMasterProfile In oCM.ChassisMasterProfiles
            If item.ProfileHeader.ID = 43 Then
                Dim oVKs As VehicleKindGroup = New VehicleKindGroupFacade(User).RetrieveByCode(item.ProfileValue)
                Try
                    ddlVehicleKindF.SelectedValue = oVKs.ID
                Catch ex As Exception
                    ddlVehicleKindF.SelectedIndex = 0
                End Try
                Exit For
            End If
        Next

        BindVehicleModel(e)
        If Me.txtCustomerCode.Text.Trim <> String.Empty Then
            If listSpkChassis.Count > 0 Then
                GetCustomerInfoForSPKMatch(txtCustomerCode.Text)
            Else
                GetCustomerInfo(Me.txtCustomerCode.Text.Trim) 'rebind, after postbock, it has been cleared
            End If
        End If

    End Sub

    Private Function UnitRemain(ByVal LKPPNumber As String, ByVal vehicleTypeID As Integer) As Integer
        Dim _lkppDetail As LKPPDetail = New LKPPDetailFacade(User).RetrieveByHeaderAndVtype(LKPPNumber, vehicleTypeID)
        If _lkppDetail.ID = 0 Then
            Return 0
        Else
            Return _lkppDetail.UnitRemain
        End If
    End Function

    Public Sub ClearChassisForm()
        txtSPKNumber.Text = ""
        lbltxtCustomerCode.Text = ""
        txtCustomerCode.Text = ""
        txtSalesmanCode.Text = ""
        lblNamaSales.Text = ""
        lblLevel.Text = ""
        lblPosisi.Text = ""

    End Sub
    Public Sub BindVehicleModel(ByRef e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim ddlVehicleKindF As DropDownList = e.Item.FindControl("ddlVehicleKindF")
        Dim ddlVehicleModelF As DropDownList = e.Item.FindControl("ddlVehicleModelF")

        Dim aVK As ArrayList
        Dim oVKFac As New VehicleKindFacade(User)
        Dim cVK As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not IsNothing(ddlVehicleKindF.SelectedValue) And ddlVehicleKindF.SelectedValue <> "" Then
            cVK.opAnd(New Criteria(GetType(VehicleKind), "VehicleKindGroupID", MatchType.Exact, ddlVehicleKindF.SelectedValue.ToString))
        Else
            cVK.opAnd(New Criteria(GetType(VehicleKind), "VehicleKindGroupID", MatchType.Exact, 0))
            MessageBox.Show("Jenis Kendaraan belum di maintain.Harap hubungi MMKSI")
        End If


        Dim txtFooterNomorRangka As TextBox = e.Item.FindControl("txtFooterNomorRangka")
        Dim oCM As ChassisMaster
        Dim oCMFac As New ChassisMasterFacade(User)

        oCM = oCMFac.Retrieve(txtFooterNomorRangka.Text)

        aVK = oVKFac.Retrieve(cVK)
        ddlVehicleModelF.Items.Clear()
        For Each oVK As VehicleKind In aVK
            ddlVehicleModelF.Items.Add(New ListItem(oVK.Description, oVK.ID))
        Next

        For Each item As ChassisMasterProfile In oCM.ChassisMasterProfiles
            If item.ProfileHeader.ID = 44 Then
                Dim oVKs As VehicleKind = oVKFac.RetrieveByCode(item.ProfileValue)
                Try
                    ddlVehicleModelF.SelectedValue = oVKs.ID
                Catch ex As Exception
                    ddlVehicleModelF.SelectedIndex = 1
                End Try
                Exit For
            End If
        Next
    End Sub

    Private Sub SaveChassisMasterProfile()
        Dim oCM As ChassisMaster
        Dim oCMPFac As New ChassisMasterProfileFacade(User)
        Dim oCMP As ChassisMasterProfile
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
                        oCMP = New ChassisMasterProfile
                        oCMP = GetCMProfile(oCM, item.ProfileGroup, item.ProfileHeader)
                        oCMP.ChassisMaster = oCM
                        oCMP.ProfileGroup = item.ProfileGroup
                        oCMP.ProfileHeader = item.ProfileHeader
                        If item.ProfileHeader.Code.Equals("CBU_MODELKEND") Then
                            'If Not String.IsNullorEmpty(item.ProfileValue) Then
                            '    oCMP.ProfileValue = vkFacade.Retrieve(CType(item.ProfileValue, Integer)).Code
                            'Else
                            '    oCMP.ProfileValue = item.ProfileValue
                            'End If
                            oCMP.ProfileValue = oCM.VehicleKind.Code
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

    Private Function GetCMProfile(ByRef oCM As ChassisMaster, ByRef oPG As ProfileGroup, ByRef oPH As ProfileHeader) As ChassisMasterProfile
        Dim oCMPFac As New ChassisMasterProfileFacade(User)
        Dim cCMP As New CriteriaComposite(New Criteria(GetType(ChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim aCMP As ArrayList


        cCMP.opAnd(New Criteria(GetType(ChassisMasterProfile), "ChassisMaster.ID", MatchType.Exact, oCM.ID))
        cCMP.opAnd(New Criteria(GetType(ChassisMasterProfile), "ProfileGroup.ID", MatchType.Exact, oPG.ID))
        cCMP.opAnd(New Criteria(GetType(ChassisMasterProfile), "ProfileHeader.ID", MatchType.Exact, oPH.ID))
        aCMP = oCMPFac.Retrieve(cCMP)
        If aCMP.Count > 0 Then
            Return CType(aCMP(0), ChassisMasterProfile)
        Else
            Return New ChassisMasterProfile
        End If
    End Function

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
        If Not Page.IsPostBack Then
            Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
            lblsearchLKPP.Visible = False
            Me.hdnIsMCP.Value = "-1"
            Me.hdnMCPConfirmation.Value = "-1"
            Me.hdnVerifyMCP.Value = "-1"
            Me.hdnIsLKPP.Value = "-1"
            Me.hdnLKPPConfirmation.Value = "-1"
            Me.hdnVerifyLKPP.Value = "-1"
            ActivateUserPrivilege()
            InitData()
            BindDataGrid()
            txtSalesmanCode.Attributes.Add("readonly", "readonly")
            txtSPKNumber.Attributes.Add("readonly", "readonly")
            txtMCPNumber.Attributes.Add("readonly", "readonly")
            txtLKPPNumber.Attributes.Add("readonly", "readonly")
            txtNoFleetReq.Attributes.Add("readonly", "readonly")
            txtCustomerCode.Attributes.Add("readonly", "readonly")

            If companyCode = "MMC" Then
                fleetTD1.Attributes.Add("style", "display:none;")
                fleetTD2.Attributes.Add("style", "display:none;")
                fleetTD3.Attributes.Add("style", "display:none;")

                MCP_TD1.Attributes.Add("style", "display:none;")
                MCP_TD2.Attributes.Add("style", "display:none;")
                MCP_TD3.Attributes.Add("style", "display:none;")
                MCP_TD4.Attributes.Add("style", "display:none;")
                MCP_TD5.Attributes.Add("style", "display:none;")
                MCP_TD6.Attributes.Add("style", "display:none;")

            ElseIf companyCode = "MFTBC" Then

                Pengadaan_TD1.Attributes.Add("style", "display:none;")
                Pengadaan_TD2.Attributes.Add("style", "display:none;")
                Pengadaan_TD3.Attributes.Add("style", "display:none;")
                Pengadaan_TD4.Attributes.Add("style", "display:none;")
                Pengadaan_TD5.Attributes.Add("style", "display:none;")
                Pengadaan_TD6.Attributes.Add("style", "display:none;")
            End If

            Dim dealerSystems As DealerSystems = New DealerSystemsFacade(User).RetrieveByDealerCode(objDealer.DealerCode)
            If Not IsNothing(dealerSystems) AndAlso dealerSystems.SystemID <> 1 Then
                isDealerDMS = True
            End If

            isDealerPiloting = TCHelper.GetActiveTCResult(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.PilotingSPKMatching))
            If (isDealerPiloting OrElse isDealerDMS) Then
                lblSPKNumber.Visible = False
            End If

            'If Not IsNothing(dealerSystems) AndAlso dealerSystems.SystemID <> 1 Then
            'If 1 = 1 Then ' cek y, knp jadi dealer dms
            '    isDealerDMS = True
            'End If
            ssHelper.SetSession("isDealerDMS", isDealerDMS)
        Else
            If hdnValid.Value.ToString() = "-1" Then
                Session("Customer") = Nothing
            End If
            If Me.hdnIsMCP.Value = "1" Then
                If Me.hdnMCPConfirmation.Value = "-1" Then
                    btnSave_Click(Nothing, Nothing)
                ElseIf Me.hdnMCPConfirmation.Value = "1" Then
                    If Me.hdnVerifyMCP.Value = "-1" Then
                        btnSave_Click(Nothing, Nothing)
                    ElseIf Me.hdnVerifyMCP.Value = "1" Then
                        btnSave_Click(Nothing, Nothing)
                    End If
                End If
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
        'If (txtSPKNumber.Text <> '' && txtfoo)
        'lblNoRabgka.Text = NoRangka
        ''If Not IsNothing(dtgPengajuanFaktur.DataSource) Then
        'txtNoRangka.Value = NoRangka
        'ini Di cek


        isDealerPiloting = TCHelper.GetActiveTCResult(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.PilotingSPKMatching))
        If (isDealerPiloting OrElse isDealerDMS) Then
            lblSPKNumber.Visible = False
            lblPopUp.Attributes("onClick") = "ShowPPTujuanSelection();"
        Else
            lblPopUp.Attributes("onClick") = "ShowPPTujuanSelection2();"
        End If
        'End If
        'imgDealer.Attributes("onclick") = "ShowPPTujuanSelection();"
        lblShowSalesman.Attributes("onClick") = "ShowSalesmanSelection();"
        lblSPKNumber.Attributes("onClick") = "ShowSPKSelection();"
        lblMCPNumber.Attributes("onClick") = "ShowMCPSelection();"
        lblsearchLKPP.Attributes("onClick") = "ShowLKPPSelection();"
        lblNoFleetReq.Attributes("onClick") = "ShowFleetReqSelection();"
        If Not IsNothing(ssHelper.GetSession("isDealerDMS")) Then
            isDealerDMS = CType(ssHelper.GetSession("isDealerDMS"), Boolean)
            If isDealerDMS Then
                lblPopUp.Visible = False
            End If
            Dim dealerSystems As DealerSystems = New DealerSystemsFacade(User).RetrieveByDealerCode(objDealer.DealerCode)
            If dealerSystems.SystemID = 1 Then
                lblPopUp.Visible = True
            End If
        End If

    End Sub

    Private Sub InitDataFromSession()
        If Not ssHelper.GetSession("NoSPK") Is Nothing Then
            txtSPKNumber.Text = CType(ssHelper.GetSession("NoSPK"), String)
        End If
        If Not (CType(ssHelper.GetSession("Salesman"), SalesmanHeader)) Is Nothing Then
            GetSalesInfo(CType(ssHelper.GetSession("Salesman"), SalesmanHeader).SalesmanCode)
        Else
            If txtSalesmanCode.Text.Trim <> String.Empty Then
                GetSalesInfo(txtSalesmanCode.Text.Trim)
            End If
        End If

        If Not (CType(Session("Customer"), Customer)) Is Nothing Then
            'updated by CR PDI-PKT on 1 Feb 2021 
            'BindCustomer(CType(Session("Customer"), Customer))
            If Me.txtCustomerCode.Text.Trim <> String.Empty Then
                GetCustomerInfo(txtCustomerCode.Text)
            End If
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

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.FakturKendaraanPangajuanBuat_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Pengajuan Faktur")
        End If
    End Sub

    Private Sub dtgPengajuanFaktur_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPengajuanFaktur.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim CM As ChassisMaster = CType(e.Item.DataItem, ChassisMaster)
            Dim objEndCust As EndCustomer = CM.EndCustomer
            Dim CM2 As ChassisMaster = New ChassisMaster
            CM2 = New FinishUnit.ChassisMasterFacade(User).Retrieve(objEndCust.RefChassisNumberID)
            If (e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem) Then
                Dim lblNoRangkaPengganti As Label = CType(e.Item.FindControl("lblNoRangkaPengganti"), Label)
                If (Not CM2 Is Nothing) Then
                    lblNoRangkaPengganti.Text = CM2.ChassisNumber
                End If
                Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                lbtnDelete.Attributes.Add("onclick", "return confirm('Hapus data?');")
                Dim lbtnEdit As LinkButton = e.Item.FindControl("lbtnEdit")
                If CM.FakturStatus = CInt(EnumChassisMaster.FakturStatus.Baru) Then
                    'update by anh 2012
                    Dim oCM As ChassisMaster = New ChassisMasterFacade(User).Retrieve(CM.ID)
                    If Not IsNothing(oCM) Then
                        If Not IsNothing(oCM.VehicleKind) Then
                            lbtnEdit.Visible = True
                        Else
                            lbtnEdit.Visible = False
                        End If
                    Else
                        lbtnEdit.Visible = False
                    End If
                    'end update by anh
                Else
                    lbtnEdit.Visible = False
                End If
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1
                Dim lblVehicleKind As Label = CType(e.Item.FindControl("lblVehicleKind"), Label)
                If Not IsNothing(CM) AndAlso CM.ID > 0 Then
                    If Not IsNothing(CM.VehicleKind) Then
                        'If Not Session("PrevPage") Is Nothing Then
                        '    lblVehicleKind.Text = CType(ssHelper.GetSession("JenisKendaraan"), String)
                        'Else
                        lblVehicleKind.Text = CM.VehicleKind.VehicleKindGroup.Description
                        'End If
                    End If
                End If
                Dim lblVehicleModel As Label = CType(e.Item.FindControl("lblVehicleModel"), Label)
                If Not IsNothing(CM) AndAlso CM.ID > 0 Then
                    If Not IsNothing(CM.VehicleKind) Then
                        'If Not Session("PrevPage") Is Nothing Then
                        '    lblVehicleKind.Text = CType(ssHelper.GetSession("ModelKendaraan"), String)
                        'Else
                        lblVehicleModel.Text = CM.VehicleKind.Description
                        'End If
                    End If
                End If
            ElseIf e.Item.ItemType = ListItemType.EditItem Then
                Dim txtEditNoRangkaPengganti As TextBox = CType(e.Item.FindControl("txtEditNoRangkaPengganti"), TextBox)
                If (Not CM2 Is Nothing) Then
                    txtEditNoRangkaPengganti.Text = CM2.ChassisNumber
                End If
                Dim icEditMaxDate As IntiCalendar = CType(e.Item.FindControl("icEditMaxDate"), IntiCalendar)
                icEditMaxDate.Value = CM.EndCustomer.FakturDate
            End If
        End If

        If e.Item.ItemType = ListItemType.Footer Then
            Dim icMaxDate As IntiCalendar = CType(e.Item.FindControl("icMaxDate"), IntiCalendar)
            If ssHelper.GetSession("DEFAULTDATE") Is Nothing Then
                icMaxDate.Value = Today.AddDays(1)
            Else
                icMaxDate.Value = ssHelper.GetSession("DEFAULTDATE")
            End If

        End If
    End Sub

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

        If IsValidChassisMasterI(NoRangka) = False Then
            MessageBox.Show("Nomor Rangka  " & NoRangka & " tidak dapat digunakan")
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

    Private Sub addCommand(ByVal e As DataGridCommandEventArgs)
        lnkShow.Visible = False
        lblError.Text = String.Empty
        Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
        Dim objCM As New ChassisMaster
        Dim txtFooterNomorRangka As TextBox = CType(e.Item.FindControl("txtFooterNomorRangka"), TextBox)
        Dim txtFooterNoRangkaPengganti As TextBox = CType(e.Item.FindControl("txtFooterNoRangkaPengganti"), TextBox)
        Dim icMaxDate As IntiCalendar = CType(e.Item.FindControl("icMaxDate"), IntiCalendar)
        Dim mode As String = "add"
        Dim ddlVehicleKindF As DropDownList = CType(e.Item.FindControl("ddlVehicleKindF"), DropDownList)
        Dim ddlVehicleModelF As DropDownList = CType(e.Item.FindControl("ddlVehicleModelF"), DropDownList)
        Dim listSpkChassis As ArrayList = New ArrayList



        If Not IsNothing(ddlVehicleKindF) Then
            If ddlVehicleKindF.Items.Count < 1 Then
                MessageBox.Show("Jenis Kendaraan Tidak Valid")
                RebindVehicleKind(e)
                Exit Sub
            Else
                Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
                Dim oCM As ChassisMaster
                Dim oCMFac As New ChassisMasterFacade(User)

                oCM = oCMFac.Retrieve(txtFooterNomorRangka.Text)

                If oCM.Category.ProductCategory.Code.Trim <> companyCode Then
                    MessageBox.Show("Nomor Rangka tidak terdaftar")
                    RebindVehicleKind(e)
                    Exit Sub
                End If

                Dim dealerSystems As DealerSystems = New DealerSystemsFacade(User).RetrieveByDealerCode(objDealer.DealerCode)
                If IsNothing(dealerSystems) Then
                    MessageBox.Show("Konfigurasi Dealer systems untuk Dealer" + objDealer.DealerName + "(" + objDealer.DealerCode + ") tidak ditemukan")
                    Exit Sub
                End If

                'If dealerSystems.SystemID <> 1 Then
                isDealerPiloting = TCHelper.GetActiveTCResult(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.PilotingSPKMatching))
                If (isDealerPiloting = False) Then
                    Dim crSpkChassis As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKChassis), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crSpkChassis.opAnd(New Criteria(GetType(SPKChassis), "MatchingType", MatchType.Exact, 1), "(", True)
                    crSpkChassis.opOr(New Criteria(GetType(SPKChassis), "MatchingType", MatchType.Exact, 3), ")", False)
                    crSpkChassis.opAnd(New Criteria(GetType(SPKChassis), "SPKDetail.SPKHeader.SPKNumber", MatchType.Exact, txtSPKNumber.Text.Trim))
                    crSpkChassis.opAnd(New Criteria(GetType(SPKChassis), "ChassisMaster.ChassisNumber", MatchType.Exact, txtFooterNomorRangka.Text.Trim))

                    listSpkChassis = New SPKChassisFacade(User).Retrieve(crSpkChassis)
                    If listSpkChassis.Count = 0 Then
                        ' if delersystem is spkmatch faktur spkchassis should be match(1) or rematch(3)
                        If dealerSystems.isSPKMatchFaktur Then
                            MessageBox.Show("Nomor Rangka tidak match dengan SPK")
                            RebindVehicleKind(e)
                            Exit Sub
                            'Else
                            '    MessageBox.Show("Jenis Kendaraan tidak sesuai dengan customer yang dipilih.")
                            '    RebindVehicleKind(e)
                            '    Exit Sub
                        End If
                    End If
                Else
                    Dim crSpkChassis As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKChassis), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crSpkChassis.opAnd(New Criteria(GetType(SPKChassis), "MatchingType", MatchType.Exact, 1), "(", True)
                    crSpkChassis.opOr(New Criteria(GetType(SPKChassis), "MatchingType", MatchType.Exact, 3), ")", False)
                    crSpkChassis.opAnd(New Criteria(GetType(SPKChassis), "SPKDetail.SPKHeader.SPKNumber", MatchType.Exact, txtSPKNumber.Text.Trim))
                    crSpkChassis.opAnd(New Criteria(GetType(SPKChassis), "ChassisMaster.ChassisNumber", MatchType.Exact, txtFooterNomorRangka.Text.Trim))

                    listSpkChassis = New SPKChassisFacade(User).Retrieve(crSpkChassis)
                    If listSpkChassis.Count = 0 Then
                        ' if delersystem is spkmatch faktur spkchassis should be match(1) or rematch(3)
                        If dealerSystems.isSPKMatchFaktur Then
                            MessageBox.Show("Nomor Rangka tidak match dengan SPK")
                            RebindVehicleKind(e)
                            Exit Sub
                        Else
                            MessageBox.Show("Jenis Kendaraan tidak sesuai dengan customer yang dipilih.")
                            RebindVehicleKind(e)
                            Exit Sub
                        End If
                    End If
                End If


                If txtFooterNoRangkaPengganti.Text <> "" Then
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
                'didieu
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

        If Not IsChassisMasterAllowedToTransact(txtFooterNomorRangka.Text.Trim()) Then Exit Sub
        If txtFooterNoRangkaPengganti.Text.Trim() <> "" Then
            If Not IsChassisMasterAllowedToTransact(txtFooterNoRangkaPengganti.Text.Trim()) Then Exit Sub
        End If


        If (CommonFunction.IsExistChassisNumberInLoginDealer(txtFooterNomorRangka.Text.Trim(), objDealer.ID, User) = False) And (IsExistChassisNumberInStockDealer(txtFooterNomorRangka.Text.Trim(), objDealer.ID, User) = False) Then
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
            If listSpkChassis.Count > 0 Then
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

        ' Modified by Ikhsan, 20080919
        ' Requested by Rina
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

        'Add by Nana related to SPK on 20110330
        '--------------------------------------------

        If txtSPKNumber.Text.Trim <> String.Empty Then
            Dim errSPKMessage As String = IsSPKAllowed(txtFooterNomorRangka.Text.Trim)
            If errSPKMessage <> String.Empty Then
                lblError.Text = errSPKMessage
                Exit Sub
            End If
        End If
        '--------------------------------------------


        Dim func As New ChassisMasterFacade(Me.User)
        Dim funcD As New DealerFacade(Me.User)
        Dim valFaktur As Dictionary(Of String, String) = func.ValidasiPengajuanFaktur(objDealer.ID, txtFooterNomorRangka.Text)

        lblError3.Text = String.Empty

        Dim errMsg As String = String.Empty
        Dim errMsg2 As String = String.Empty
        'Dim idx As Integer = 1
        bltError.Items.Clear()
        For Each item As KeyValuePair(Of String, String) In valFaktur
            bltError.Items.Add(item.Key + " - " + funcD.Retrieve(item.Value).DealerName + "  " + item.Value)

        Next
        If valFaktur.Count > 0 Then
            lnkShow.Visible = True
            lblError3.Text = "Masih ada no rangka dummy yang belum diajukan : <br/>"

            trDetail.Visible = False
            lnkShow.Text = "Show Detail"
        End If


        If CheckData(txtFooterNomorRangka.Text.Trim, txtFooterNoRangkaPengganti.Text.Trim, icMaxDate.Value, e.Item.ItemIndex) Then
            AddToArrayList(txtFooterNomorRangka.Text, txtFooterNoRangkaPengganti.Text, icMaxDate.Value, mode, 0, CType(ddlVehicleModelF.SelectedValue, Integer))
            If IsValidChassisMasterII(txtFooterNomorRangka.Text.Trim) <> String.Empty Then
                ssHelper.SetSession("PREVPAGE", "..\FinishUnit\FrmPengajuanFaktur.aspx")
                ssHelper.SetSession("NoRangka", txtFooterNomorRangka.Text)
                ssHelper.SetSession("NoRangkaPengganti", txtFooterNoRangkaPengganti.Text)
                ssHelper.SetSession("DateFaktur", icMaxDate.Value)
                ssHelper.SetSession("FrmPengajuanFaktur_NoFleetReq", txtNoFleetReq.Text)
                'ssHelper.SetSession("JenisKendaraan", ddlVehicleKindF.SelectedItem.Text)
                'ssHelper.SetSession("ModelKendaraan", ddlVehicleModelF.SelectedItem.Text)
                Response.Redirect("..\PopUp\PopUpConfirmationPengajuanFaktur.aspx")
            End If

        End If
        BindDataGrid()
        txtSPKNumber.Enabled = False
        lblSPKNumber.Visible = False

    End Sub

    Private Sub updateCommand(ByVal e As DataGridCommandEventArgs)
        Dim txtFooterNomorRangka As TextBox = CType(e.Item.FindControl("txtFooterNomorRangka"), TextBox)
        Dim txtFooterNoRangkaPengganti As TextBox = CType(e.Item.FindControl("txtFooterNoRangkaPengganti"), TextBox)
        Dim lblNomorRangka As Label = CType(e.Item.FindControl("lblNomorRangka"), Label)
        Dim txtEditNoRangkaPengganti As TextBox = CType(e.Item.FindControl("txtEditNoRangkaPengganti"), TextBox)
        Dim icEditMaxDate As IntiCalendar = CType(e.Item.FindControl("icEditMaxDate"), IntiCalendar)
        Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
        Dim mode As String = "update"
        Dim ddlVehicleKindF As DropDownList = CType(e.Item.FindControl("ddlVehicleKindF"), DropDownList)
        Dim ddlVehicleModelF As DropDownList = CType(e.Item.FindControl("ddlVehicleModelF"), DropDownList)

        If Not IsChassisMasterAllowedToTransact(txtFooterNomorRangka.Text.Trim()) Then Exit Sub
        If Not IsChassisMasterAllowedToTransact(txtFooterNoRangkaPengganti.Text.Trim()) Then Exit Sub


        If (CommonFunction.IsExistChassisNumberInLoginDealer(lblNomorRangka.Text, objDealer.ID, User) = False) And (IsExistChassisNumberInStockDealer(lblNomorRangka.Text, objDealer.ID, User) = False) Then
            MessageBox.Show("Nomor rangka tidak terdaftar di dealer anda")
            Exit Sub
        End If
        If txtEditNoRangkaPengganti.Text.Trim <> String.Empty Then
            If (CommonFunction.IsExistChassisNumberInLoginDealer(txtEditNoRangkaPengganti.Text.Trim(), objDealer.ID, User) = False) And (IsExistChassisNumberInStockDealer(txtEditNoRangkaPengganti.Text.Trim(), objDealer.ID, User) = False) Then
                MessageBox.Show("Nomor rangka pengganti tidak terdaftar di dealer anda")
                Exit Sub
            End If
        End If

        'add vlidation by anh 20120124
        If Not IsNothing(ddlVehicleKindF) Then
            If ddlVehicleKindF.Items.Count < 1 Then
                MessageBox.Show("Jenis Kendaraan Tidak Valid")
                RebindVehicleKind(e)
                Exit Sub
            Else
                Dim oCM As ChassisMaster
                Dim oCMFac As New ChassisMasterFacade(User)

                oCM = oCMFac.Retrieve(txtFooterNomorRangka.Text)
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
        'end added by anh 20120124


        If txtSalesmanCode.Text.Trim <> String.Empty Then
            If GetSalesInfo(txtSalesmanCode.Text.Trim) = False Then
                Exit Sub
            End If
        End If
        If txtCustomerCode.Text.Trim <> String.Empty Then
            If GetCustomerInfo(txtCustomerCode.Text) = False Then
                Exit Sub
            End If
        Else
            MessageBox.Show("Masukkan kode konsumen terlebih dahulu")
            Exit Sub
        End If
        If Not IsValidTglFaktur(icEditMaxDate.Value) Then
            lblError.Text = "Tanggal pengajuan tidak valid/hari libur."
            Exit Sub
        Else
            lblError.Text = ""
        End If

        'Add by Nana related to SPK
        '--------------------------------------------
        If txtSPKNumber.Text.Trim <> String.Empty Then
            Dim errSPKMessage As String = IsSPKAllowed(txtFooterNomorRangka.Text.Trim)
            If errSPKMessage <> String.Empty Then
                lblError.Text = errSPKMessage
                Exit Sub
            End If
        End If
        '--------------------------------------------

        If CheckData(lblNomorRangka.Text.Trim, txtEditNoRangkaPengganti.Text.Trim, icEditMaxDate.Value, e.Item.ItemIndex) Then
            AddToArrayList(lblNomorRangka.Text, txtEditNoRangkaPengganti.Text, icEditMaxDate.Value, mode, e.Item.ItemIndex)
            dtgPengajuanFaktur.EditItemIndex = -1
            dtgPengajuanFaktur.ShowFooter = True
        End If
        BindDataGrid()
    End Sub

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
            Case "edit"
                ssHelper.SetSession("ADD", Nothing)
                objCM = CType(arl(e.Item.ItemIndex), ChassisMaster)
                Dim cat As String = objCM.Category.CategoryCode
                Dim strID As String = objCM.ID.ToString
                ssHelper.SetSession("NoSPK", txtSPKNumber.Text.Trim)
                ssHelper.SetSession("PREVPAGE", Request.Url.ToString())
                Response.Redirect("FrmMasterProfiles.aspx?iseditsingle=1&Cat=" & cat & "&adsfadfadfw32342412412412424=1&kjhadshkf9784323247832493ihdufiue=17&hjkadsdhhdhkjdafkhdafklhdfahdsajklsdfjdsjjkldsaf9874553983423=" & strID)
                'Try
                '    dtgPengajuanFaktur.ShowFooter = False
                '    dtgPengajuanFaktur.EditItemIndex = e.Item.ItemIndex
                'Catch ex As Exception

                'End Try
            Case "update"
                Try
                    updateCommand(e)
                Catch ex As Exception

                End Try
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

    Private Function RemoveCharacterBlock(ByVal pendingDesc As String, ByVal block As String) As String
        Dim strValue As String
        If Not pendingDesc = "" Then
            strValue = pendingDesc.Replace(block, "")
        End If
        Return strValue
    End Function

    Private Function CheckChassisNumberAndPendingDesc(ByVal chassisNumber As String) As Boolean
        Dim isBlock As Boolean = False
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, chassisNumber))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, 0))
        Dim dataResult = New ChassisMasterFacade(User).Retrieve(criterias)

        If dataResult.count >= 1 Then
            For Each model As ChassisMaster In dataResult
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
            Next
        End If
        Return isBlock
    End Function

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If lblError.Text.Trim <> String.Empty Then
            MessageBox.Show(lblError.Text.Trim)
            Exit Sub
        End If
        Dim n As Integer
        If Me.isValidCM() = False Then Exit Sub

        Dim oCust As Customer
        If (Not Session("Customer") Is Nothing) Then
            oCust = CType(Session("Customer"), Customer)
        Else
            Dim objCustomerFacade As CustomerFacade = New CustomerFacade(User)
            oCust = objCustomerFacade.Retrieve(txtCustomerCode.Text.Trim)
        End If

        Dim TipeKendaraanSPK As String = String.Empty
        Dim WarnaKendaraanSPK As String = String.Empty
        If Not IsNothing(oCust) Then
            If oCust.CreatedTime < New Date(2011, 6, 1) Then
                MessageBox.Show("Data konsumen tidak dpt digunakan, silakan ajukan ulang.")
                Exit Sub
            End If
            arl = CType(Session("sessCM"), ArrayList)

            For Each oCM As ChassisMaster In arl
                If hfSPKDetailCustomerID.Value = "" Then
                    Dim isSameColor As Boolean = False
                    Dim cr As New CriteriaComposite(New Criteria(GetType(SPKDetailCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    cr.opAnd(New Criteria(GetType(SPKDetailCustomer), "CustomerRequest.CustomerCode", MatchType.Exact, txtCustomerCode.Text))
                    cr.opAnd(New Criteria(GetType(SPKDetailCustomer), "SPKDetail.SPKHeader.SPKNumber", MatchType.Exact, txtSPKNumber.Text))
                    'cr.opAnd(New Criteria(GetType(SPKDetailCustomer), "SPKDetail.VechileColor.ID", MatchType.Exact, oCM.VechileColor.ID))
                    Dim arrSDCList As ArrayList = New SPKDetailCustomerFacade(User).Retrieve(cr)
                    If Not IsNothing(arrSDCList) AndAlso arrSDCList.Count > 0 Then
                        For Each oSDC As SPKDetailCustomer In arrSDCList
                            If oSDC.SPKDetail.VechileColor.ID = oCM.VechileColor.ID Then
                                hfSPKDetailCustomerID.Value = oSDC.ID.ToString()
                                isSameColor = True
                                'WarnaKendaraanSPK = oSDC.SPKDetail.VechileColor.ColorCode
                                TipeKendaraanSPK = oSDC.SPKDetail.VechileColor.MaterialNumber
                                Exit For
                            End If
                        Next
                        If Not isSameColor Then
                            lblError.Text = "Tipe/Warna Kendaraan pada Chassis " & oCM.VechileColor.MaterialNumber & _
                                " tidak sesuai dengan Tipe/Warna Kendaraan di SPK " & TipeKendaraanSPK '& "/" & WarnaKendaraanSPK
                            MessageBox.Show(lblError.Text.Trim)
                            Exit Sub
                        End If
                    Else
                        lblError.Text = "Konsumen Faktur Tidak Terdaftar"
                        MessageBox.Show(lblError.Text.Trim)
                        Exit Sub
                    End If
                End If

                Dim _spkDetail As SPKDetail = CType(ssHelper.GetSession("SPKDETAILFAKTUR"), SPKDetail)
                If _spkDetail.ID = 0 Then
                    _spkDetail = New SPKDetailCustomerFacade(User).Retrieve(CInt(hfSPKDetailCustomerID.Value)).SPKDetail
                    ssHelper.SetSession("SPKDETAILFAKTUR", _spkDetail)
                End If
            Next



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

        End If

        If dtgPengajuanFaktur.Items.Count <> 1 Then
            MessageBox.Show("Pastikan terisi satu data detail rangka sebelum melanjutkan proses")
            Exit Sub
        End If

        If Not IsNothing(oCust) Then
            Dim MCPStatus As Integer = EnumMCPStatus.MCPStatus.NonMCP
            Dim LKPPStatus As Integer = EnumLKPPStatus.LKPPStatus.NonLKPP
            Dim oMCP As MCPHeader
            Dim oLKPP As LKPPHeader
            'Start  :MCP Confirmation;by:dna;on:20110622;for:rina
            If Not IsNothing(oCust.MyCustomerRequest) AndAlso oCust.MyCustomerRequest.ID > 0 Then
                Dim IsCV As Boolean = False
                Dim isMMC As Boolean = False
                arl = CType(Session("sessCM"), ArrayList)
                For Each oCM As ChassisMaster In arl
                    If oCM.VechileColor.VechileType.Category.CategoryCode = "CV" Then
                        IsCV = True
                        Exit For
                    End If
                    If oCM.VechileColor.VechileType.Category.CategoryCode = "PC" Or oCM.VechileColor.VechileType.Category.CategoryCode = "LCV" Then
                        isMMC = True
                        Exit For
                    End If
                Next

                'MCP
                If oCust.MyCustomerRequest.Status1 = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah AndAlso IsCV Then
                    'Add by anh 20150624 for yurike, related to MCP 
                    '--------------------------------------------
                    If MCPCheckByVehicleType(txtMCPNumber.Text.Trim, arl) = False Then Exit Sub
                    '--------------------------------------------
                    If hdnIsMCP.Value = "-1" Then
                        MessageBox.Confirm("Apakah dealer telah mengirimkan MCP?", "hdnIsMCP")
                        Exit Sub
                    ElseIf hdnIsMCP.Value = "1" Then
                        If Me.hdnMCPConfirmation.Value = "-1" Then
                            MessageBox.Confirm("Konsumen terdeteksi MCP, Apakah proses pengajuan tetap dilanjutkan?", "hdnMCPConfirmation")
                            Exit Sub
                        ElseIf Me.hdnMCPConfirmation.Value = "1" Then
                            MessageBox.Show("MMKSI akan melakukan verifikasi terhadap pengajuan ini.")
                            MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP

                            oCust.MyCustomerRequest.MCPStatus = MCPStatus
                            oCust.MyCustomerRequest.LKPPStatus = Nothing
                            'Add by anh 20150624 for yurike, related to MCP insert mcpheaderid on endcustomer 
                            '--------------------------------------------
                            Dim critMCP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MCPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            critMCP.opAnd(New Criteria(GetType(MCPHeader), "ReferenceNumber", MatchType.Exact, txtMCPNumber.Text.Trim))
                            oMCP = New MCPHeaderFacade(User).Retrieve(critMCP)(0)
                            If Not IsNothing(oMCP) Then
                                For Each oCM As ChassisMaster In arl
                                    oCM.EndCustomer.MCPHeader = oMCP
                                Next
                            End If
                            '--------------------------------------------
                        End If
                    End If

                    'LKPP
                ElseIf oCust.MyCustomerRequest.Status1 = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah AndAlso isMMC Then
                    If LKPPCheckByVehicleType(txtLKPPNumber.Text.Trim, arl) = False Then Exit Sub
                    If 1 = 1 Then
                        MessageBox.Show("MMKSI akan melakukan verifikasi terhadap pengajuan ini.")
                        LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP
                        oCust.MyCustomerRequest.LKPPStatus = LKPPStatus
                        'oCust.MyCustomerRequest.MCPStatus = Nothing
                        'Add by anh 20150624 for yurike, related to LKPP insert LKPPheaderid on endcustomer 
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
                    End If
                    'End If
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
                    'CV
                    If IsCV Then
                        'added by anh 2015-08-24 ' add validasi to profile
                        '--------------------------------------------
                        If Not ssHelper.GetSession("IsSucceedProfileFaktur") Is Nothing Then
                            If ssHelper.GetSession("IsSucceedProfileFaktur") = 0 Then
                                If MCPCheckByVehicleType(txtMCPNumber.Text.Trim, arl) = False Then Exit Sub

                                Dim critMCP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MCPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                critMCP.opAnd(New Criteria(GetType(MCPHeader), "ReferenceNumber", MatchType.Exact, txtMCPNumber.Text.Trim))
                                oMCP = New MCPHeaderFacade(User).Retrieve(critMCP)(0)
                                If Not IsNothing(oMCP) Then
                                    For Each objCM As ChassisMaster In arl
                                        objCM.EndCustomer.MCPHeader = oMCP
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
                                If hfSPKDetailCustomerID.Value <> "" Then
                                    cSPKProfile.opAnd(New Criteria(GetType(SPKProfile), "SPKDetailCustomer.ID", MatchType.Exact, CInt(hfSPKDetailCustomerID.Value)))
                                End If
                                arlProfile = oSPKProfileFac.Retrieve(cSPKProfile)
                                For Each item As SPKProfile In arlProfile
                                    If (item.ProfileGroup.ID = 5 And item.ProfileHeader.ID = 5) AndAlso (item.ProfileValue = "IP" Or item.ProfileValue = "BN") Then
                                        isProfileAllowed = False

                                        If MCPCheckByVehicleType(txtMCPNumber.Text.Trim, arl) = False Then Exit Sub

                                        If hdnIsMCP.Value = "-1" Then
                                            MessageBox.Confirm("Apakah dealer telah mengirimkan MCP?", "hdnIsMCP")
                                            Exit Sub
                                        ElseIf hdnIsMCP.Value = "1" Then
                                            If Me.hdnMCPConfirmation.Value = "-1" Then
                                                MessageBox.Confirm("Konsumen terdeteksi MCP, Apakah proses pengajuan tetap dilanjutkan?", "hdnMCPConfirmation")
                                                Exit Sub
                                            ElseIf Me.hdnMCPConfirmation.Value = "1" Then
                                                MessageBox.Show("MMKSI akan melakukan verifikasi terhadap pengajuan ini.")
                                                MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP
                                                oCust.MyCustomerRequest.MCPStatus = MCPStatus

                                                Dim critMCP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MCPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                                critMCP.opAnd(New Criteria(GetType(MCPHeader), "ReferenceNumber", MatchType.Exact, txtMCPNumber.Text.Trim))
                                                oMCP = New MCPHeaderFacade(User).Retrieve(critMCP)(0)
                                                If Not IsNothing(oMCP) Then
                                                    For Each objCM As ChassisMaster In arl
                                                        objCM.EndCustomer.MCPHeader = oMCP
                                                    Next
                                                    isProfileAllowed = True
                                                End If
                                            End If
                                        End If
                                    End If
                                Next
                                If Not isProfileAllowed Then
                                    For Each oCM As ChassisMaster In arl
                                        oCM.EndCustomer.MCPStatus = 1 '--"1 = Terindikasi MCP; 0 = Bukan"
                                    Next
                                End If
                            End If
                        End If
                        'end added by anh 2015-08-24
                    End If

                    'MMC
                    If isMMC Then
                        'added by anh 2015-08-24 ' add validasi to profile
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
                                If hfSPKDetailCustomerID.Value <> "" Then
                                    cSPKProfile.opAnd(New Criteria(GetType(SPKProfile), "SPKDetailCustomer.ID", MatchType.Exact, CInt(hfSPKDetailCustomerID.Value)))
                                End If
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

                                        If 1 = 1 Then
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
                                        'End If
                                    End If
                                Next

                                If Not isProfileAllowed Then
                                    For Each oCM As ChassisMaster In arl
                                        oCM.EndCustomer.LKPPStatus = 1 ' 1 '--"1 = Terindikasi LKPP; 0 = Bukan"
                                    Next
                                End If
                            End If
                        End If
                        'end added by adi 2015-12-16

                        ''CheckPoin3
                        If oCust.MyCustomerRequest.Status1 <> EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah AndAlso (CBool(ViewState("IsGovernmentInstitution"))) AndAlso Not (CBool(ViewState("IsGovernmentOwnerShip"))) Then
                            'Chek isi dan vechile kind
                            '  If LKPPCheckByVehicleType(txtLKPPNumber.Text.Trim, arl) = False Then Exit Sub
                            '  MessageBox.Show("KTB akan melakukan verifikasi terhadap pengajuan ini.")
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

                            '  MessageBox.Show("KTB akan melakukan verifikasi terhadap pengajuan ini.")
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
            'End    :MCP Confirmation;by:dna;on:20110622;for:rina
            'If (Not Session("Salesman") Is Nothing) Then
            If Session("MODE") = "insert" Then
                arl = CType(Session("sessCM"), ArrayList)
                If (arl.Count > 0) Then
                    SaveChassisMasterProfile()
                    'Dim oCust As Customer = CType(Session("Customer"), Customer)
                    'Add by Anh Req by Rina, 20110331
                    '-------------------------

                    'Dim isTmp As Short = Convert.ToInt16(chkIsTemporary.Checked)
                    Dim isTmp As Short = 0
                    For i As Integer = 0 To arl.Count - 1
                        Dim chassis As ChassisMaster = arl(i)
                        If chassis.EndCustomer.IsTemporary <> isTmp Then
                            chassis.EndCustomer.IsTemporary = isTmp
                            arl(i) = chassis
                        End If
                    Next

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
                        'If ocust.CreatedBy.ToUpper.Trim <> "" Then
                        '    MessageBox.Show("Faktur tidak bisa disimpan karena konsumen tdk punya data KTP/TDP. Silakan ajukan ulang konsumen tsb.")
                        '    Exit Sub
                        'End If
                    End If
                    '-------------------------
                    'Add by Anh related to SPK on 200110330
                    '------------------------
                    Dim SPKMandatory As String = KTB.DNet.Lib.WebConfig.GetValue("SPKMandatory")
                    If Date.Now < New Date(2011, 5, 1) Then
                        SPKMandatory = "0"
                    End If

                    If SPKMandatory = "1" And txtSPKNumber.Text.Trim = "" Then
                        MessageBox.Show("No. Registrasi SPK harus diisi")
                        Exit Sub
                    End If

                    'If CInt(SPKMandatory) = 1 Then
                    If txtSPKNumber.Text.Trim <> String.Empty Then
                        Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        'criterias.opAnd(New Criteria(GetType(SPKHeader), "Dealer.ID", MatchType.Exact, objDealer.ID))
                        criterias.opAnd(New Criteria(GetType(SPKHeader), "SPKNumber", MatchType.Exact, txtSPKNumber.Text.Trim))
                        Dim objSPKHeaderList As ArrayList = New FinishUnit.SPKHeaderFacade(User).Retrieve(criterias)
                        If objSPKHeaderList.Count > 0 Then
                            Dim objSPKHeader As SPKHeader = objSPKHeaderList(0)

                            DeactiveSpkFakturIfChassisMasterHaveEndCustomer(arl)
                            Dim WarnaKen As String = String.Empty
                            Dim TipeKen As String = String.Empty
                            If (checkVechileColor(arl, objSPKHeader, TipeKen)) Then
                                n = New EndCustomerFacade(User).InsertTransactionPengajuanFaktur(oCust, arl, objSPKHeader)
                            Else
                                Dim _spkDetail As SPKDetail = CType(ssHelper.GetSession("SPKDETAILFAKTUR"), SPKDetail)
                                If Not IsNothing(_spkDetail) Then
                                    TipeKendaraanSPK = _spkDetail.VechileColor.MaterialNumber
                                    'WarnaKendaraanSPK = _spkDetail.VechileColor.ColorCode
                                    Dim err As String = "Tipe/Warna Kendaraan Chassis " & TipeKen & "/" & WarnaKen & _
                                    " tidak sesuai dengan Tipe/Warna Kendaraan di SPK " & TipeKendaraanSPK '& "/" & WarnaKendaraanSPK
                                    MessageBox.Show(err)
                                    Return
                                Else
                                    MessageBox.Show("Warna Kendaraan Tidak Sama Antara Chassis dan SPK")
                                    Return
                                End If
                            End If
                            'n = New EndCustomerFacade(User).InsertTransactionPengajuanFaktur(oCust, arl, objSPKHeader)
                            'start by anh 20171013
                            CopyCustomerDealerToDealerSPK(oCust)
                            'end by anh 20171013

                            'start Remark by rudi, move to frmEntryInvoice on button validasi
                            'start indent system ' anh 201707
                            'If n <> -1 Then
                            '    Dim objUpdate As FinishUnit.SPKHeaderFacade = New FinishUnit.SPKHeaderFacade(User)
                            '    Dim objSPKHeaderUpdated As SPKHeader = objUpdate.Retrieve(objSPKHeader.ID)
                            '    If Not IsNothing(objSPKHeaderUpdated) Then
                            '        If objSPKHeader.SPKFakturs.Count > 0 Then
                            '            Dim qty As Integer = 0
                            '            For Each det As SPKDetail In objSPKHeader.SPKDetails
                            '                qty = qty + det.Quantity
                            '            Next
                            '            If (objSPKHeader.SPKFakturs.Count = qty) Then
                            '                Dim oldStatus As Integer = objSPKHeader.Status

                            '                objSPKHeader.Status = EnumStatusSPK.Status.Selesai
                            '                objUpdate = New FinishUnit.SPKHeaderFacade(User)
                            '                objUpdate.Update(objSPKHeader)


                            '                'Insert StatusChangeHistory
                            '                Dim objNewStatus As New StatusChangeHistory
                            '                objNewStatus.DocumentType = 6
                            '                objNewStatus.DocumentRegNumber = objSPKHeader.SPKNumber
                            '                objNewStatus.OldStatus = oldStatus
                            '                objNewStatus.NewStatus = CInt(EnumStatusSPK.Status.Selesai)
                            '                objNewStatus.RowStatus = 0

                            '                Dim iReturn As Integer = New StatusChangeHistoryFacade(User).Insert(objNewStatus)
                            '            End If
                            '        End If
                            '    End If
                            'End If
                            'end indent system ' anh 201707
                            'end Remark by rudi

                            'Update sales faktur ke sales SPK on SPKHeader table
                            'Modified by ANH 20120411

                            '--insert fleet request number, add by wdi 20161003
                            If n > -1 And txtNoFleetReq.Text.Trim <> "" Then
                                Dim critFleetRequest As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                critFleetRequest.opAnd(New Criteria(GetType(FleetRequest), "NoRegRequest", MatchType.Exact, txtNoFleetReq.Text.Trim))
                                Dim arrFleetRequest As ArrayList = New FleetRequestFacade(User).Retrieve(critFleetRequest)

                                Dim oFleetRequest As FleetRequest
                                If arrFleetRequest.Count > 0 Then
                                    oFleetRequest = arrFleetRequest(0)
                                End If

                                If Not IsNothing(oFleetRequest) Then
                                    Dim objFleetFaktur As FleetFaktur = New FleetFaktur
                                    objFleetFaktur.ChassisMaster = CType(arl(0), ChassisMaster)
                                    objFleetFaktur.FleetRequest = oFleetRequest
                                    Dim oFleetFakturFacade As FleetFakturFacade = New FleetFakturFacade(User)
                                    Dim intResult As Integer = oFleetFakturFacade.Insert(objFleetFaktur)
                                End If
                            End If

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
                        n = New EndCustomerFacade(User).InsertTransactionPengajuanFaktur(oCust, arl)

                        'start by anh 20171013
                        CopyCustomerDealerToDealerSPK(oCust)
                        'end by anh 20171013

                        '--insert fleet request number, add by wdi 20161003
                        If n > -1 And txtNoFleetReq.Text.Trim <> "" Then
                            Dim critFleetRequest As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            critFleetRequest.opAnd(New Criteria(GetType(FleetRequest), "NoRegRequest", MatchType.Exact, txtNoFleetReq.Text.Trim))
                            Dim arrFleetRequest As ArrayList = New FleetRequestFacade(User).Retrieve(critFleetRequest)

                            Dim oFleetRequest As FleetRequest
                            If arrFleetRequest.Count > 0 Then
                                oFleetRequest = arrFleetRequest(0)
                            End If

                            If Not IsNothing(oFleetRequest) Then
                                Dim objFleetFaktur As FleetFaktur = New FleetFaktur
                                objFleetFaktur.ChassisMaster = CType(arl(0), ChassisMaster)
                                objFleetFaktur.FleetRequest = oFleetRequest
                                Dim oFleetFakturFacade As FleetFakturFacade = New FleetFakturFacade(User)
                                Dim intResult As Integer = oFleetFakturFacade.Insert(objFleetFaktur)
                            End If
                        End If

                    End If

                    'Else
                    '    n = New EndCustomerFacade(User).InsertTransactionPengajuanFaktur(oCust, arl)
                    'End If
                    '-------------------------
                    If (n <> -1) Then
                        If (Not Session("Salesman") Is Nothing) Then
                            GroupingDeliveryCustomer(CType(Session("Salesman"), SalesmanHeader), CType(Session("Customer"), Customer), CType(ssHelper.GetSession("DEALER"), Dealer), arl)
                        Else
                            GroupingDeliveryCustomer(Nothing, CType(Session("Customer"), Customer), CType(ssHelper.GetSession("DEALER"), Dealer), arl)
                        End If

                        'Update MCPDeatail ; add by anh 20150624 for yurike related to mcp
                        'Start
                        If Len(txtMCPNumber.Text.Trim) > 0 Then
                            'UpdateMCPDetail(oCust)
                            For Each cm As ChassisMaster In arl
                                UpdateMCPDetail(cm)
                            Next
                        End If
                        'end

                        For Each model As ChassisMaster In arl
                            UpdatePendingDescAndLastUpdateProfile(model.ChassisNumber)
                        Next

                        MessageBox.Show("Pengajuan faktur sukses")
                        'Todo session
                        Session("MODE") = "update"
                        btnUpdateProfil.Enabled = True
                        btnSave.Enabled = False
                    Else
                        MessageBox.Show("Pengajuan faktur gagal")
                    End If
                Else
                    MessageBox.Show("Tidak ada data dalam list")
                    Exit Sub
                End If
            ElseIf Session("MODE") = "update" Then
                arl = CType(Session("sessCM"), ArrayList)
                If (arl.Count > 0) Then
                    SaveChassisMasterProfile()
                    'Dim oCust As Customer = CType(Session("Customer"), Customer)
                    n = New EndCustomerFacade(User).UpdateTransaction(oCust, arl, CType(Session("DelCM"), ArrayList))
                    If (n <> -1) Then
                        If txtSalesmanCode.Text.Trim <> String.Empty Then
                            If (Not Session("Salesman") Is Nothing) Then
                                GroupingDeliveryCustomer(CType(Session("Salesman"), SalesmanHeader), CType(Session("Customer"), Customer), CType(ssHelper.GetSession("DEALER"), Dealer), arl)
                            Else
                                MessageBox.Show("Data sales tidak ditemukan")
                            End If
                        End If

                        '--insert fleet request number, add by wdi 20161003
                        If n > -1 And txtNoFleetReq.Text.Trim <> "" Then
                            Dim critFleetRequest As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            critFleetRequest.opAnd(New Criteria(GetType(FleetRequest), "NoRegRequest", MatchType.Exact, txtNoFleetReq.Text.Trim))
                            Dim arrFleetRequest As ArrayList = New FleetRequestFacade(User).Retrieve(critFleetRequest)

                            Dim oFleetRequest As FleetRequest
                            If arrFleetRequest.Count > 0 Then
                                oFleetRequest = arrFleetRequest(0)
                            End If

                            Dim intResult As Integer = 0
                            If Not IsNothing(oFleetRequest) Then
                                Dim critFleetFaktur As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                critFleetFaktur.opAnd(New Criteria(GetType(FleetFaktur), "ChassisMaster.ID", MatchType.Exact, arl(0).ID))
                                Dim objFleetFaktur As FleetFaktur = New FleetFakturFacade(User).Retrieve(critFleetFaktur)(0)

                                If IsNothing(objFleetFaktur) Then               '-- insert
                                    objFleetFaktur = New FleetFaktur
                                    objFleetFaktur.ChassisMaster = CType(arl(0), ChassisMaster)
                                    objFleetFaktur.FleetRequest = oFleetRequest
                                    Dim oFleetFakturFacade As FleetFakturFacade = New FleetFakturFacade(User)
                                    intResult = oFleetFakturFacade.Insert(objFleetFaktur)
                                Else                                            '--update
                                    objFleetFaktur.FleetRequest = oFleetRequest
                                    Dim oFleetFakturFacade As FleetFakturFacade = New FleetFakturFacade(User)
                                    intResult = oFleetFakturFacade.Update(objFleetFaktur)
                                End If
                            Else
                                Dim critFleetFaktur As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                critFleetFaktur.opAnd(New Criteria(GetType(FleetFaktur), "ChassisMaster.ID", MatchType.Exact, arl(0).ID))
                                Dim objFleetFaktur As FleetFaktur = New FleetFakturFacade(User).Retrieve(critFleetFaktur)(0)

                                If Not IsNothing(objFleetFaktur) Then           '--if exist then deleted
                                    objFleetFaktur.FleetRequest = oFleetRequest
                                    objFleetFaktur.RowStatus = -1
                                    Dim oFleetFakturFacade As FleetFakturFacade = New FleetFakturFacade(User)
                                    intResult = oFleetFakturFacade.Insert(objFleetFaktur)
                                End If
                            End If
                        End If

                        MessageBox.Show("Pengajuan faktur sukses")
                        ssHelper.SetSession("MODE", "update")
                        ssHelper.SetSession("DelCM", New ArrayList)
                        btnUpdateProfil.Enabled = True
                    Else
                        MessageBox.Show("Pengajuan faktur gagal")
                    End If
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

    Private Function checkVechileColor(arl As ArrayList, objspk As SPKHeader, ByRef Tipe As String) As Boolean
        Dim ret As Boolean = False
        For Each row As ChassisMaster In arl
            For Each row1 As SPKDetail In objspk.SPKDetails
                If row.VechileColor.ColorCode = row1.VechileColor.ColorCode Then
                    ret = True
                    Exit For
                End If
            Next
            'Warna = row.VechileColor.ColorCode
            Tipe = row.VechileColor.MaterialNumber
        Next
        Return ret
    End Function

    Private Sub DeactiveSpkFakturIfChassisMasterHaveEndCustomer(ByVal arlChassisMaster As ArrayList)
        Dim objSpkFaktur As SPKFakturFacade = New SPKFakturFacade(User)
        For Each item As ChassisMaster In arlChassisMaster
            If item.EndCustomerID <> 0 Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SPKFaktur), "EndCustomer.ID", MatchType.Exact, item.EndCustomerID))
                Dim arlSpkFaktur As ArrayList = objSpkFaktur.Retrieve(criterias)

                For Each spkFakturData As SPKFaktur In arlSpkFaktur
                    spkFakturData.RowStatus = CType(DBRowStatus.Deleted, Short)
                    objSpkFaktur.Update(spkFakturData)
                Next

            End If
        Next
    End Sub

    Private Function MCPCheckByVehicleType(ByVal mcpNumber As String, ByVal arlChassis As ArrayList) As Boolean
        Dim vReturn As Boolean = False

        If txtMCPNumber.Text.Trim = "" Then
            MessageBox.Show("Konsumen terdeteksi MCP, Nomor MCP harap diisi.")
        Else
            Dim oMCP As MCPHeader = New MCPHeaderFacade(User).Retrieve(mcpNumber)
            If Not IsNothing(oMCP) Then
                For Each mcpD As MCPDetail In oMCP.MCPDetails
                    For Each objCM As ChassisMaster In arl
                        If objCM.VechileColor.VechileType.ID = mcpD.VechileType.ID Then
                            vReturn = True
                        End If
                    Next
                Next
            End If
            If vReturn = False Then
                MessageBox.Show("Nomor MCP tidak sesuai dengan tipe kendaraan yang dipilih.")
            End If
        End If

        Return vReturn

    End Function

    Private Sub UpdateMCPDetail(ByVal oCust As Customer)
        'Dim objCM As EndCustomer = New EndCustomerFacade(User).Retrieve(endCustomerID)
        Dim objCM As EndCustomer = CType(oCust.EndCustomers(0), EndCustomer)

        For Each ec As EndCustomer In oCust.EndCustomers
            If Not IsNothing(ec.MCPHeader) Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MCPDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(MCPDetail), "MCPHeader.ID", MatchType.Exact, objCM.MCPHeader.ID))
                criterias.opAnd(New Criteria(GetType(MCPDetail), "VechileType.ID", MatchType.Exact, objCM.ChassisMaster.VechileColor.VechileType.ID))
                Dim objMCPDetail As MCPDetail = New MCPDetailFacade(User).Retrieve(criterias)(0)
                If Not IsNothing(objMCPDetail) Then
                    'updated 20150722, block minus.
                    If objMCPDetail.UnitRemain > 0 Then
                        objMCPDetail.UnitRemain = objMCPDetail.UnitRemain - 1
                        Dim i As Integer = New MCPDetailFacade(User).Update(objMCPDetail)
                    End If
                End If
                Exit For
            End If
        Next

    End Sub

    Private Sub UpdateMCPDetail(ByVal oCust As ChassisMaster)
        If Not IsNothing(oCust.EndCustomer.MCPHeader) Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MCPDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MCPDetail), "MCPHeader.ID", MatchType.Exact, oCust.EndCustomer.MCPHeader.ID))
            criterias.opAnd(New Criteria(GetType(MCPDetail), "VechileType.ID", MatchType.Exact, oCust.EndCustomer.ChassisMaster.VechileColor.VechileType.ID))
            Dim objMCPDetail As MCPDetail = New MCPDetailFacade(User).Retrieve(criterias)(0)
            If Not IsNothing(objMCPDetail) Then
                If objMCPDetail.UnitRemain > 0 Then
                    objMCPDetail.UnitRemain = objMCPDetail.UnitRemain - 1
                    Dim i As Integer = New MCPDetailFacade(User).Update(objMCPDetail)
                End If
            End If
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

        'Try

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
                ssHelper.SetSession("PREVPAGE", "FrmPengajuanFaktur.aspx")
                ssHelper.SetSession("UpdateProfile", True)
                ssHelper.SetSession("ADD", Nothing)
                If txtMCPNumber.Text.Trim <> String.Empty Then
                    ssHelper.SetSession("MCPNUMBER", txtMCPNumber.Text.Trim)
                End If

                If txtLKPPNumber.Text.Trim <> String.Empty Then
                    ssHelper.SetSession("LKPPNUMBER", txtLKPPNumber.Text.Trim)
                End If
                Response.Redirect("FrmMasterProfiles.aspx?Cat=" & cat & "&adsfadfadfw32342412412412424=1&kjhadshkf9784323247832493ihdufiue=17&hjkadsdhhdhkjdafkhdafklhdfahdsajklsdfjdsjjkldsaf9874553983423=" & strID)
            End If
        Else
            MessageBox.Show("Pilih chassis yang ingin di update")
        End If
    End Sub

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

    'Private Function IsMCPValid(ByVal IsAfterConfirmation As Boolean) As Boolean
    '    hdnVerifyMCP.Value = EnumMCPStatus.MCPStatus.NonMCP
    '    If CType(Me.ddlTipe.SelectedValue, Integer) = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah Then
    '        If IsAfterConfirmation = False Then
    '            If (IsGovernmentInstitution(Me.txtNama.Text) OrElse IsGovernmentInstitution(Me.txtNama2.Text)) Then
    '                MessageBox.Confirm("Apakah proses pengajuan tetap dilanjutkan?", "hdnMCPConfirmation")
    '                Return False
    '            Else
    '                Return True
    '            End If
    '        Else
    '            If (IsGovernmentInstitution(Me.txtNama.Text) OrElse IsGovernmentInstitution(Me.txtNama2.Text)) Then
    '                If Me.hdnMCPConfirmation.Value <> "1" Then 'User click No in confirmation box
    '                    Return False
    '                Else
    '                    hdnVerifyMCP.Value = EnumMCPStatus.MCPStatus.NotVerifiedMCP
    '                    Return True 'user click ok in confirmation box
    '                End If
    '            Else
    '                Return True 'never
    '            End If
    '        End If
    '    Else
    '        Return True
    '    End If

    'End Function

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
#End Region

#Region "Custom"

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

    'Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
    '    If lblError.Text.Trim <> String.Empty Then
    '        MessageBox.Show(lblError.Text.Trim)
    '        Exit Sub
    '    End If
    '    Dim n As Integer
    '    If Me.isValidCM() = False Then Exit Sub

    '    Dim oCust As Customer
    '    If (Not Session("Customer") Is Nothing) Then
    '        oCust = CType(Session("Customer"), Customer)
    '    Else
    '        Dim objCustomerFacade As CustomerFacade = New CustomerFacade(User)
    '        oCust = objCustomerFacade.Retrieve(txtCustomerCode.Text.Trim)
    '    End If

    '    If Not IsNothing(oCust) Then
    '        If oCust.CreatedTime < New Date(2011, 6, 1) Then
    '            MessageBox.Show("Data konsumen tidak dpt digunakan, silakan ajukan ulang.")
    '            Exit Sub
    '        End If
    '    End If

    '    If Not IsNothing(oCust) Then
    '        Dim MCPStatus As Integer = EnumMCPStatus.MCPStatus.NonMCP
    '        Dim oMCP As MCPHeader
    '        'Start  :MCP Confirmation;by:dna;on:20110622;for:rina
    '        If Not IsNothing(oCust.MyCustomerRequest) AndAlso oCust.MyCustomerRequest.ID > 0 Then
    '            Dim IsCV As Boolean = False
    '            arl = CType(Session("sessCM"), ArrayList)
    '            For Each oCM As ChassisMaster In arl
    '                If oCM.VechileColor.VechileType.Category.CategoryCode = "CV" Then
    '                    IsCV = True
    '                    Exit For
    '                End If
    '            Next
    '            If oCust.MyCustomerRequest.Status1 = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah AndAlso IsCV Then
    '                'Add by anh 20150624 for yurike, related to MCP 
    '                '--------------------------------------------

    '                If MCPCheckByVehicleType(txtMCPNumber.Text.Trim, arl) = False Then Exit Sub

    '                '--------------------------------------------
    '                If hdnIsMCP.Value = "-1" Then
    '                    MessageBox.Confirm("Apakah dealer telah mengirimkan MCP?", "hdnIsMCP")
    '                    Exit Sub
    '                ElseIf hdnIsMCP.Value = "1" Then
    '                    If Me.hdnMCPConfirmation.Value = "-1" Then
    '                        MessageBox.Confirm("Konsumen terdeteksi MCP, Apakah proses pengajuan tetap dilanjutkan?", "hdnMCPConfirmation")
    '                        Exit Sub
    '                    ElseIf Me.hdnMCPConfirmation.Value = "1" Then
    '                        MessageBox.Show("MMKSI akan melakukan verifikasi terhadap pengajuan ini.")
    '                        MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP
    '                        oCust.MyCustomerRequest.MCPStatus = MCPStatus

    '                        'Add by anh 20150624 for yurike, related to MCP insert mcpheaderid on endcustomer 
    '                        '--------------------------------------------
    '                        Dim critMCP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MCPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '                        critMCP.opAnd(New Criteria(GetType(MCPHeader), "ReferenceNumber", MatchType.Exact, txtMCPNumber.Text.Trim))
    '                        oMCP = New MCPHeaderFacade(User).Retrieve(critMCP)(0)
    '                        If Not IsNothing(oMCP) Then
    '                            For Each oCM As ChassisMaster In arl
    '                                oCM.EndCustomer.MCPHeader = oMCP
    '                            Next
    '                        End If
    '                        '--------------------------------------------

    '                    End If
    '                End If

    '            Else
    '                Dim isGovIndicated As Boolean = True
    '                If Not Me.IsGovernmentInstitution(oCust.MyCustomerRequest.Name1) OrElse Not Me.IsGovernmentInstitution(oCust.MyCustomerRequest.Name2) Then isGovIndicated = False
    '                If isGovIndicated Then
    '                    For Each oCM As ChassisMaster In arl
    '                        oCM.EndCustomer.MCPStatus = 1 '--"1 = Terindikasi MCP; 0 = Bukan"
    '                    Next
    '                End If
    '                If IsCV Then
    '                    'added by anh 2015-08-24 ' add validasi to profile
    '                    '--------------------------------------------

    '                    If Not ssHelper.GetSession("IsSucceedProfileFaktur") Is Nothing Then
    '                        If ssHelper.GetSession("IsSucceedProfileFaktur") = 0 Then

    '                            If MCPCheckByVehicleType(txtMCPNumber.Text.Trim, arl) = False Then Exit Sub

    '                            Dim critMCP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MCPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '                            critMCP.opAnd(New Criteria(GetType(MCPHeader), "ReferenceNumber", MatchType.Exact, txtMCPNumber.Text.Trim))
    '                            oMCP = New MCPHeaderFacade(User).Retrieve(critMCP)(0)
    '                            If Not IsNothing(oMCP) Then
    '                                For Each objCM As ChassisMaster In arl
    '                                    objCM.EndCustomer.MCPHeader = oMCP
    '                                Next
    '                            End If
    '                        End If
    '                    Else
    '                        Dim _spkDetail As SPKDetail = CType(ssHelper.GetSession("SPKDETAILFAKTUR"), SPKDetail)
    '                        If Not IsNothing(_spkDetail) Then

    '                            Dim isProfileAllowed As Boolean = True
    '                            Dim arlProfile As ArrayList
    '                            Dim oSPKProfileFac As New SPKProfileFacade(User)
    '                            Dim cSPKProfile As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '                            cSPKProfile.opAnd(New Criteria(GetType(SPKProfile), "SPKDetail.ID", MatchType.Exact, _spkDetail.ID))

    '                            arlProfile = oSPKProfileFac.Retrieve(cSPKProfile)
    '                            For Each item As SPKProfile In arlProfile
    '                                If (item.ProfileGroup.ID = 5 And item.ProfileHeader.ID = 5) AndAlso (item.ProfileValue = "IP" Or item.ProfileValue = "BN") Then
    '                                    isProfileAllowed = False

    '                                    If MCPCheckByVehicleType(txtMCPNumber.Text.Trim, arl) = False Then Exit Sub

    '                                    If hdnIsMCP.Value = "-1" Then
    '                                        MessageBox.Confirm("Apakah dealer telah mengirimkan MCP?", "hdnIsMCP")
    '                                        Exit Sub
    '                                    ElseIf hdnIsMCP.Value = "1" Then
    '                                        If Me.hdnMCPConfirmation.Value = "-1" Then
    '                                            MessageBox.Confirm("Konsumen terdeteksi MCP, Apakah proses pengajuan tetap dilanjutkan?", "hdnMCPConfirmation")
    '                                            Exit Sub
    '                                        ElseIf Me.hdnMCPConfirmation.Value = "1" Then
    '                                            MessageBox.Show("KTB akan melakukan verifikasi terhadap pengajuan ini.")
    '                                            MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP
    '                                            oCust.MyCustomerRequest.MCPStatus = MCPStatus

    '                                            Dim critMCP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MCPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '                                            critMCP.opAnd(New Criteria(GetType(MCPHeader), "ReferenceNumber", MatchType.Exact, txtMCPNumber.Text.Trim))
    '                                            oMCP = New MCPHeaderFacade(User).Retrieve(critMCP)(0)
    '                                            If Not IsNothing(oMCP) Then
    '                                                For Each objCM As ChassisMaster In arl
    '                                                    objCM.EndCustomer.MCPHeader = oMCP
    '                                                Next
    '                                                isProfileAllowed = True
    '                                            End If
    '                                        End If
    '                                    End If
    '                                End If
    '                            Next
    '                            If Not isProfileAllowed Then
    '                                For Each oCM As ChassisMaster In arl
    '                                    oCM.EndCustomer.MCPStatus = 1 '--"1 = Terindikasi MCP; 0 = Bukan"
    '                                Next
    '                            End If
    '                        End If
    '                    End If

    '                    'end added by anh 2015-08-24
    '                End If
    '            End If

    '        End If
    '        'End    :MCP Confirmation;by:dna;on:20110622;for:rina
    '        'If (Not Session("Salesman") Is Nothing) Then
    '        If Session("MODE") = "insert" Then
    '            arl = CType(Session("sessCM"), ArrayList)
    '            If (arl.Count > 0) Then
    '                SaveChassisMasterProfile()
    '                'Dim oCust As Customer = CType(Session("Customer"), Customer)
    '                'Add by Anh Req by Rina, 20110331
    '                '-------------------------

    '                Dim oCustRequest As CustomerRequest = New CustomerRequestFacade(User).RetrieveCodeDesc(oCust.Code)
    '                If Not IsNothing(oCustRequest) AndAlso oCustRequest.ID > 0 Then
    '                    Dim oCustRequestProfile As CustomerRequestProfile = oCustRequest.GetCustomerRequestProfile("NOKTP")
    '                    If Not IsNothing(oCustRequestProfile) AndAlso oCustRequestProfile.ID > 0 Then
    '                        If Not IsNothing(oCustRequestProfile.ProfileValue) Then
    '                            If oCustRequest.Status1 <> CType(EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah, Short) Then
    '                                If oCustRequestProfile.ProfileValue.Trim.Length <= 5 Then
    '                                    MessageBox.Show("Faktur tidak bisa disimpan karena konsumen tdk punya data KTP/TDP. Silakan ajukan ulang konsumen tsb.")
    '                                    Exit Sub
    '                                End If
    '                            End If
    '                        Else
    '                            If oCustRequest.Status1 <> CType(EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah, Short) Then
    '                                MessageBox.Show("Faktur tidak bisa disimpan karena konsumen tdk punya data KTP/TDP. Silakan ajukan ulang konsumen tsb.")
    '                                Exit Sub
    '                            End If
    '                        End If
    '                    Else
    '                        If oCustRequest.Status1 <> CType(EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah, Short) Then
    '                            MessageBox.Show("Faktur tidak bisa disimpan karena konsumen tdk punya data KTP/TDP. Silakan ajukan ulang konsumen tsb.")
    '                            Exit Sub
    '                        End If



    '                    End If
    '                Else
    '                    'If ocust.CreatedBy.ToUpper.Trim <> "" Then
    '                    '    MessageBox.Show("Faktur tidak bisa disimpan karena konsumen tdk punya data KTP/TDP. Silakan ajukan ulang konsumen tsb.")
    '                    '    Exit Sub
    '                    'End If
    '                End If

    '                '-------------------------

    '                'Add by Anh related to SPK on 200110330
    '                '------------------------
    '                Dim SPKMandatory As String = KTB.DNet.Lib.WebConfig.GetValue("SPKMandatory")
    '                If Date.Now < New Date(2011, 5, 1) Then
    '                    SPKMandatory = "0"
    '                End If

    '                If SPKMandatory = "1" And txtSPKNumber.Text.Trim = "" Then
    '                    MessageBox.Show("No. Registrasi SPK harus diisi")
    '                    Exit Sub
    '                End If

    '                'If CInt(SPKMandatory) = 1 Then
    '                If txtSPKNumber.Text.Trim <> String.Empty Then
    '                    Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
    '                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '                    'criterias.opAnd(New Criteria(GetType(SPKHeader), "Dealer.ID", MatchType.Exact, objDealer.ID))
    '                    criterias.opAnd(New Criteria(GetType(SPKHeader), "SPKNumber", MatchType.Exact, txtSPKNumber.Text.Trim))
    '                    Dim objSPKHeaderList As ArrayList = New FinishUnit.SPKHeaderFacade(User).Retrieve(criterias)
    '                    If objSPKHeaderList.Count > 0 Then
    '                        Dim objSPKHeader As SPKHeader = objSPKHeaderList(0)
    '                        n = New EndCustomerFacade(User).InsertTransactionPengajuanFaktur(oCust, arl, objSPKHeader)
    '                        'Update sales faktur ke sales SPK on SPKHeader table
    '                        'Modified by ANH 20120411
    '                        If txtSalesmanCode.Text.Trim <> String.Empty Then
    '                            If txtSalesmanCode.Text.Trim <> objSPKHeader.SalesmanHeader.SalesmanCode.Trim Then
    '                                Dim criteriasSPKHeader As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '                                criteriasSPKHeader.opAnd(New Criteria(GetType(SalesmanHeader), "SalesmanCode", MatchType.Exact, txtSalesmanCode.Text.Trim))
    '                                Dim objSalesmanHeaderList As ArrayList = New SalesmanHeaderFacade(User).Retrieve(criteriasSPKHeader)
    '                                If objSalesmanHeaderList.Count > 0 Then
    '                                    Dim objSalesmanHeader As SalesmanHeader = objSalesmanHeaderList(0)
    '                                    objSPKHeader.SalesmanHeader = objSalesmanHeader
    '                                    Dim oSPKHeaderFacade As SPKHeaderFacade = New SPKHeaderFacade(User)
    '                                    oSPKHeaderFacade.Update(objSPKHeader)
    '                                End If
    '                            End If
    '                        End If
    '                        'end modified
    '                    Else
    '                        MessageBox.Show("No. SPK tidak ada")
    '                        Exit Sub
    '                    End If
    '                Else
    '                    n = New EndCustomerFacade(User).InsertTransactionPengajuanFaktur(oCust, arl)
    '                End If



    '                'Else
    '                '    n = New EndCustomerFacade(User).InsertTransactionPengajuanFaktur(oCust, arl)
    '                'End If
    '                '-------------------------
    '                If (n <> -1) Then
    '                    If (Not Session("Salesman") Is Nothing) Then
    '                        GroupingDeliveryCustomer(CType(Session("Salesman"), SalesmanHeader), CType(Session("Customer"), Customer), CType(ssHelper.GetSession("DEALER"), Dealer), arl)
    '                    Else
    '                        GroupingDeliveryCustomer(Nothing, CType(Session("Customer"), Customer), CType(ssHelper.GetSession("DEALER"), Dealer), arl)
    '                    End If

    '                    'Update MCPDeatail ; add by anh 20150624 for yurike related to mcp
    '                    'Start
    '                    If Len(txtMCPNumber.Text.Trim) > 0 Then
    '                        'UpdateMCPDetail(oCust)
    '                        For Each cm As ChassisMaster In arl
    '                            UpdateMCPDetail(cm)
    '                        Next
    '                    End If
    '                    'end

    '                    MessageBox.Show("Pengajuan faktur sukses")
    '                    'Todo session
    '                    Session("MODE") = "update"
    '                    btnUpdateProfil.Enabled = True
    '                    btnSave.Enabled = False
    '                Else
    '                    MessageBox.Show("Pengajuan faktur gagal")
    '                End If
    '            Else
    '                MessageBox.Show("Tidak ada data dalam list")
    '                Exit Sub
    '            End If
    '        ElseIf Session("MODE") = "update" Then
    '            arl = CType(Session("sessCM"), ArrayList)
    '            If (arl.Count > 0) Then
    '                SaveChassisMasterProfile()
    '                'Dim oCust As Customer = CType(Session("Customer"), Customer)
    '                n = New EndCustomerFacade(User).UpdateTransaction(oCust, arl, CType(Session("DelCM"), ArrayList))
    '                If (n <> -1) Then
    '                    If txtSalesmanCode.Text.Trim <> String.Empty Then
    '                        If (Not Session("Salesman") Is Nothing) Then
    '                            GroupingDeliveryCustomer(CType(Session("Salesman"), SalesmanHeader), CType(Session("Customer"), Customer), CType(ssHelper.GetSession("DEALER"), Dealer), arl)
    '                        Else
    '                            MessageBox.Show("Data sales tidak ditemukan")
    '                        End If
    '                    End If
    '                    MessageBox.Show("Pengajuan faktur sukses")
    '                    ssHelper.SetSession("MODE", "update")
    '                    ssHelper.SetSession("DelCM", New ArrayList)
    '                    btnUpdateProfil.Enabled = True
    '                Else
    '                    MessageBox.Show("Pengajuan faktur gagal")
    '                End If
    '            Else
    '                MessageBox.Show("Tidak ada data dalam list")
    '                Exit Sub
    '            End If
    '        End If
    '    Else
    '        MessageBox.Show("Data konsumen tidak ditemukan")
    '        Exit Sub
    '    End If

    'End Sub

#End Region

    Protected Sub btntxtCustomerCode_Click(sender As Object, e As EventArgs) Handles btntxtCustomerCode.Click
        Dim oSPKDetailCust As SPKDetailCustomer = New SPKDetailCustomerFacade(User).Retrieve(CInt(hfSPKDetailCustomerID.Value))
        If Not IsNothing(oSPKDetailCust.SPKDetail) Then
            If Not IsNothing(oSPKDetailCust.SPKDetail.VechileColor) Then
                If Not IsNothing(oSPKDetailCust.SPKDetail.VechileColor.VechileType) Then
                    If UnitRemain(oSPKDetailCust.LKPPReference, oSPKDetailCust.SPKDetail.VechileColor.VechileType.ID) > 0 Then
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

    Private Sub lnkShow_Click(sender As Object, e As EventArgs) Handles lnkShow.Click
        If lnkShow.Text = "Show Detail" Then
            trDetail.Visible = True
            lnkShow.Text = "Sembunyikan"
        Else
            trDetail.Visible = False
            lnkShow.Text = "Show Detail"
        End If

    End Sub
End Class
