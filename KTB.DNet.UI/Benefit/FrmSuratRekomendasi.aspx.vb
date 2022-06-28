Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Benefit
Imports KTB.DNet.BusinessFacade.FinishUnit


Public Class FrmSuratRekomendasi
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblNoRegRecom As System.Web.UI.WebControls.Label
    Protected WithEvents LblKota1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTanggal As System.Web.UI.WebControls.Label

    Protected WithEvents lblLeasing As System.Web.UI.WebControls.Label
    Protected WithEvents lblKota2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealer As System.Web.UI.WebControls.Label

    Protected WithEvents lblModel As System.Web.UI.WebControls.Label
    'Protected WithEvents lblPembeli As System.Web.UI.WebControls.Label
    'Protected WithEvents lblNamaFaktur As System.Web.UI.WebControls.Label

    Protected WithEvents lblTipe As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoChassis As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoEngine As System.Web.UI.WebControls.Label
 

    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private sessHelper As New SessionHelper
    Private objChassisMaster As ChassisMaster = New ChassisMaster
    Private objBenefitMasterDetail As BenefitMasterDetail = New BenefitMasterDetail


#Region "Private Property"
    Private Property SesType() As EnumAlertManagement.AlertManagementType
        Get
            Return CType(sessHelper.GetSession("ListAlertMasterType"), EnumAlertManagement.AlertManagementType)
        End Get
        Set(ByVal Value As EnumAlertManagement.AlertManagementType)
            sessHelper.SetSession("ListAlertMasterType", Value)
        End Set
    End Property
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.AlertManagementListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=ALERT MANAGEMENT - Alert Managemen List")
        End If
    End Sub

    Dim bCekDetailPriv As Boolean = SecurityProvider.Authorize(context.User, SR.AlertManagementListView_Privilege)
#End Region


    Private Sub RetrieveDealer()
        Dim objSessionHelper As New SessionHelper
        Dim objDealer As Dealer = CType(objSessionHelper.GetSession("DEALER"), Dealer)
        If Not objDealer Is Nothing Then

            If Not objDealer.DealerGroup Is Nothing Then
                lblNamaDealer.Text = objDealer.DealerName
                lblKota2.Text = objDealer.City.CityName
                LblKota1.Text = lblKota2.Text
            Else
               
            End If

        Else

        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'InitiateAuthorization()
        If Not IsPostBack Then

            BindDataGrid()

            RetrieveDealer()
        End If
    End Sub

   
   

    Private Sub BindDataGrid()
        Dim idmasterdetil As String = Request.QueryString("idmasterdetil")
        Dim idchassis As String = Request.QueryString("idchassis")

        Dim bcid As String = ""

        If Not IsNothing(Request.QueryString("bcid")) Then
            bcid = Request.QueryString("bcid")
        End If

        Dim norecom As String = Request.QueryString("norecom")
        If Not idchassis Is Nothing Then
            objChassisMaster = New ChassisMasterFacade(User).Retrieve(CInt(idchassis))
            If Not objChassisMaster Is Nothing Then
                If Not objChassisMaster.EndCustomer Is Nothing Then
                    'lblNamaFaktur.Text = objChassisMaster.EndCustomer.Name1
                    'lblPembeli.Text = objChassisMaster.EndCustomer.Customer.Name1
                Else
                    'lblNamaFaktur.Text = ""
                    'lblPembeli.Text = ""
                End If

                lblNoChassis.Text = objChassisMaster.ChassisNumber
                lblNoEngine.Text = objChassisMaster.EngineNumber
                lblTipe.Text = objChassisMaster.VechileColor.VechileType.Description 'objChassisMaster.VechileColor.VechileType.VechileModel.Description & ", " _
                '& objChassisMaster.VechileColor.VechileType.Description
                lblModel.Text = objChassisMaster.VechileColor.VechileType.VechileModel.Description

            End If
        End If

        If Not idmasterdetil Is Nothing Then
            objBenefitMasterDetail = New BenefitMasterDetailFacade(User).Retrieve(CInt(idmasterdetil))
            If Not IsNothing(objBenefitMasterDetail) AndAlso 1 = 2 Then

                Dim objBenefitClaimRecom As BenefitClaimRecommendation = New BenefitClaimRecommendationFacade(User).RetrieveByClaimDetail(objBenefitMasterDetail.ID)
                Dim temp1 As String = ""
                Dim temp2 As String = ""
                Dim temp3 As String = ""
                If Not objBenefitMasterDetail Is Nothing Then
                    For Each el As BenefitMasterLeasing In objBenefitMasterDetail.BenefitMasterLeasings
                        temp1 += el.LeasingCompany.LeasingName
                    Next
                    For Each el As BenefitMasterDealer In objBenefitMasterDetail.BenefitMasterHeader.BenefitMasterDealers
                        temp2 += el.Dealer.DealerName
                        temp3 += el.Dealer.City.CityName
                    Next
                End If
                lblLeasing.Text = temp1
                lblNamaDealer.Text = temp2
                lblKota2.Text = temp3
                LblKota1.Text = temp3
            Else

                If Not IsNothing(objChassisMaster) Then
                    Dim ObjBCDetail As BenefitClaimDetails
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDetails), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(BenefitClaimDetails), "ChassisMaster.ID", MatchType.Exact, objChassisMaster.ID))
                    criterias.opAnd(New Criteria(GetType(BenefitClaimDetails), "ID", MatchType.Exact, CInt(bcid)))
                    Dim ObjArrDetail As New ArrayList
                    ObjArrDetail = New BenefitClaimDetailsFacade(User).Retrieve(criterias)
                    If Not IsNothing(ObjArrDetail) AndAlso ObjArrDetail.Count > 0 Then
                        ObjBCDetail = CType(ObjArrDetail(0), BenefitClaimDetails)
                        lblLeasing.Text = ObjBCDetail.BenefitClaimHeader.LeasingCompany.LeasingName

                    End If
                End If
                'lblNoRegRecom.Text = objBenefitClaimRecom.BenefitClaimRecReg
            End If

        Else
            RetrieveDealer()
        End If

        If Not norecom Is Nothing Then
            lblNoRegRecom.Text = norecom
        End If

        Dim StrDate As String = ""
        Dim ObjL As ListItem
        ObjL = CType(LookUp.ArrayBulan((DateTime.Now.Month - 1)), ListItem)
        StrDate = String.Format("{0} {1} {2}", DateTime.Now.Date.Day.ToString(), ObjL.Text, DateTime.Now.Year.ToString())
        lblTanggal.Text = StrDate 'DateTime.Now.ToString("dd/MM/yyyy")


    End Sub


    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim idmasterdetil As String = Request.QueryString("idmasterdetil")
        Dim idchassis As String = Request.QueryString("idchassis")
        Dim norecom As String = Request.QueryString("norecom")
        'Dim list As New ArrayList
        'Dim listcheck As New ArrayList

        'For Each item As String In arrayCheck.Value.Replace(" ", "").Split(";")
        '    If Not item Is Nothing And Not item = "" Then
        '        listcheck.Add(item)
        '    End If
        'Next

        'If listcheck.Count < 1 Then
        '    MessageBox.Show("Check list data minimal satu")
        '    Return
        'End If

        'If Not sessHelper.GetSession("ListSession") Is Nothing Then
        '    list = CType(sessHelper.GetSession("ListSession"), ArrayList)
        'End If



        'Dim n As Integer = New BenefitClaimHeaderFacade(User).UpdateStatus1(list, listcheck, ddlAccuered.SelectedValue, _
        '                                                            icDateBayar.Value, Nothing)
        'If n = -1 Then
        '    MessageBox.Show(SR.SaveFail)
        'Else
        '    MessageBox.Show(SR.SaveSuccess)
        '    BindDataGrid(dgTable.CurrentPageIndex)
        'End If

        If Not idmasterdetil = Nothing Then
            Dim objdetail As BenefitClaimDetails = New BenefitClaimDetailsFacade(User).Retrieve(CInt(idmasterdetil))
            If Not IsNothing(objdetail) Then
                Dim objBenefitClaimRecom As BenefitClaimRecommendation = New BenefitClaimRecommendationFacade(User).RetrieveByClaimDetail(objdetail.ID)

                objBenefitClaimRecom.BenefitClaimDetails = objdetail
                objBenefitClaimRecom.BenefitClaimRecReg = norecom


                Dim n As Integer = -1
                If Not objBenefitClaimRecom.ID = Nothing Then
                    n = New BenefitClaimRecommendationFacade(User).UpdateRecom(objBenefitClaimRecom)
                Else
                    n = New BenefitClaimRecommendationFacade(User).InsertRecom(objBenefitClaimRecom)
                End If

                If n = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show("Simpan Berhasil")

                    BindDataGrid()
                End If
            End If
            
        End If

    End Sub
  
End Class
