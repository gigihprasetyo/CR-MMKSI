#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Linq
#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade
Imports System.Text
Imports KTB.DNet.BusinessValidation

#End Region

Public Class FrmVehicleInformationSystem
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtChassisNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btnEdit As System.Web.UI.WebControls.LinkButton
    Protected WithEvents dtgServiceData As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgServiceDataCampaign As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblMaterial As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoChassis As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoEngine As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoEngineTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoEngineColon As System.Web.UI.WebControls.Label
    Protected WithEvents lblStockOutDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerSold As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoSerial As System.Web.UI.WebControls.Label
    Protected WithEvents lblDOPrintDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblFakturOpenDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerPDI As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglPDI As System.Web.UI.WebControls.Label
    Protected WithEvents ICHandoverDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lnkbtnPopUpRefClaimNumber As LinkButton


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents lblPDIIndicator As System.Web.UI.WebControls.Label
    Protected WithEvents BtnDeletePDI As System.Web.UI.WebControls.Button
    Protected WithEvents dgPMStatus As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lbtnDeletePDI As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblFakturDate As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoEngine As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearchEngine As System.Web.UI.WebControls.Button
    Protected WithEvents lblNoRegRequest As System.Web.UI.WebControls.Label
    Protected WithEvents lblKeteranganFleet As System.Web.UI.WebControls.Label
    Protected WithEvents lblHandoverDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblFSType As Label
    Protected WithEvents lblRecallChassisMaster As Label
    '-- start add by rudi
    Protected WithEvents dgWarrantyServiceClaim As System.Web.UI.WebControls.DataGrid
    '-- end add by rudi

    Protected WithEvents trMSP As Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trGridMSP As Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents dgMSP As System.Web.UI.WebControls.DataGrid


    Private bPrivilegeVehicleInfo As Boolean = False

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

    Private _sessHelper As SessionHelper
    Private _objDealer As Dealer
    Private _endCustomer As EndCustomer
#End Region

#Region "Custom Variable Declaration"
    Dim arlist, arlist2 As ArrayList
    Dim sparepartColl, arChassisMaster As ArrayList
    Dim ObjChassisMaster As ChassisMaster
    Dim objEndCustomer As EndCustomer
    Dim sHVeh As SessionHelper = New SessionHelper
    Dim sHECus As SessionHelper = New SessionHelper
    Dim isDeleteAuth As Boolean = False
    Private sessionHelper As New SessionHelper
    Dim _chassisMasterId As Integer
    '-- start add by rudi
    Private ClaimStatusDAPP As String = "DAPP"
    Private ClaimStatusAPP As String = "APP"
    '-- end add by rudi

#End Region

#Region "Custom Method"

    Private Sub bindFreeService()
        Dim criterias2 As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, Me.txtChassisNumber.Text))
        arChassisMaster = New ChassisMasterFacade(User).Retrieve(criterias)
        Dim ObjChassisMaster As ChassisMaster
        For Each ObjChassisMaster In arChassisMaster
            criterias2 = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.ID", MatchType.Exact, ObjChassisMaster.ID))

        Next
        sHVeh.SetSession("infoSort", criterias2)
        'dtgServiceData.DataSource() = New FreeServiceFacade(User).Retrieve(criterias2)
        Dim indexPage As Integer = 0
        Dim totalRow As Integer = 0
        dtgServiceData.DataSource() = New FreeServiceFacade(User).RetrieveActiveList(CType(sHVeh.GetSession("infoSort"), CriteriaComposite), indexPage + 1, dtgServiceData.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
        dtgServiceData.DataBind()
    End Sub

    '-- add start rudi
    Private Sub bindWarrantyServiceClaim()
        Dim criterias2 As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, Me.txtChassisNumber.Text))
        arChassisMaster = New ChassisMasterFacade(User).Retrieve(criterias)
        Dim ObjChassisMaster As ChassisMaster
        For Each ObjChassisMaster In arChassisMaster
            criterias2 = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.WSCHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCHeader), "ChassisMaster.ID", MatchType.Exact, ObjChassisMaster.ID))
        Next
        sHVeh.SetSession("infoSort", criterias2)
        Dim indexPage As Integer = 0
        Dim totalRow As Integer = 0
        dgWarrantyServiceClaim.DataSource() = New WSCHeaderFacade(User).RetrieveActiveList(CType(sHVeh.GetSession("infoSort"), CriteriaComposite), indexPage + 1, dtgServiceData.PageSize, totalRow, CType(ViewState("currentSortColumnX"), String), CType(ViewState("currentSortDirectX"), Sort.SortDirection))
        dgWarrantyServiceClaim.DataBind()
    End Sub
    '-- add end rudi

    Private Sub bindFleet()
        Dim critFleetFaktur As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critFleetFaktur.opAnd(New Criteria(GetType(FleetFaktur), "ChassisMaster.ChassisNumber", MatchType.Exact, Me.txtChassisNumber.Text))
        Dim arrFleetFaktur As ArrayList = New FleetFakturFacade(User).Retrieve(critFleetFaktur)

        Dim objFleetFaktur As FleetFaktur
        If arrFleetFaktur.Count > 0 Then
            objFleetFaktur = arrFleetFaktur(0)
        End If

        If Not IsNothing(objFleetFaktur) Then
            lblNoRegRequest.Text = objFleetFaktur.FleetRequest.NoRegRequest
            lblKeteranganFleet.Text = objFleetFaktur.KeteranganFleet
        End If
    End Sub

    Private Sub getChassisData()
        Dim bError As Boolean = False

        For Each VInformationSystem As ChassisMaster In arlist
            If Not VInformationSystem.ErrorMessage = String.Empty Then
                bError = True
                Exit For
            End If
        Next
        If Not bError Then
            sHVeh.SetSession("VehInformationSystem", arlist)
        End If

    End Sub

    Private Sub getEndCustomerData()
        Dim bError2 As Boolean = False

        For Each VEndCustomer As EndCustomer In arlist2
            If Not VEndCustomer.ErrorMessage = String.Empty Then
                bError2 = True
                Exit For
            End If
        Next
        If Not bError2 Then
            sHECus.SetSession("ECustomer", arlist2)
        End If

    End Sub

    Private Sub saveChassisMaster()

        Dim arList As ArrayList = CType(sHVeh.GetSession("VehInformationSystem"), ArrayList)

        For Each VehInformationSystem As ChassisMaster In arList
            If Not IsExistCode(VehInformationSystem.ChassisNumber) Then
                InsertChassisMaster(VehInformationSystem)  '-- Insert 
            Else
                UpdateChassisMaster(VehInformationSystem)  '-- Update 
            End If
        Next
    End Sub

    Private Sub saveEndCustomer()
        Dim arList2 As ArrayList = CType(sHECus.GetSession("ECustomer"), ArrayList)
        For Each ECustomer As EndCustomer In arList2
            If Not IsExistCode2(ECustomer.ChassisMaster.ID) Then
                InsertEndCustomer(ECustomer)  '-- Insert 
            Else
                UpdateEndCustomer(ECustomer)  '-- Update
            End If
        Next
    End Sub

    Private Function IsExistCode(ByVal sCode As String) As Boolean
        Dim objChassis As ChassisMasterFacade = New ChassisMasterFacade(User)
        Return objChassis.ValidateCode(sCode) > 0
    End Function

    Private Function IsExistCode2(ByVal ChassisId As Integer) As Boolean
        Dim objEndCustomerFacade As EndCustomerFacade = New EndCustomerFacade(User)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EndCustomer), "ChassisMaster.ID", MatchType.Exact, ChassisId))
        Dim TExist As ArrayList = New EndCustomerFacade(User).Retrieve(criterias)
        If TExist.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub InsertEndCustomer(ByVal ECustomer As EndCustomer)
        Dim objEndCustomerFacade As EndCustomerFacade = New EndCustomerFacade(User)
        objEndCustomerFacade.Insert(ECustomer)
    End Sub

    Private Sub InsertChassisMaster(ByVal VehInformationSystem As ChassisMaster)
        Dim objChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
        objChassisMasterFacade.Insert(VehInformationSystem)
    End Sub

    Private Sub UpdateChassisMaster(ByVal VehInformationSystem As ChassisMaster)
        Dim criterias As CriteriaComposite
        Dim objChassisMaster2 As ChassisMaster
        Dim objChassisMasterRet As ArrayList
        Dim objChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
        criterias = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, VehInformationSystem.ChassisNumber))
        objChassisMasterRet = New ChassisMasterFacade(User).Retrieve(criterias)
        If objChassisMasterRet.Count > 0 Then
            objChassisMaster2 = objChassisMasterRet(0)
        End If
        VehInformationSystem.ID = objChassisMaster2.ID
        objChassisMasterFacade.Update(VehInformationSystem)

    End Sub

    Private Sub UpdateEndCustomer(ByVal ECustomer As EndCustomer)
        Dim criterias As CriteriaComposite
        Dim objEndCustomer2 As EndCustomer
        Dim objEndCustomerRet As ArrayList
        Dim objEndCustumerFacade As EndCustomerFacade = New EndCustomerFacade(User)
        criterias = New CriteriaComposite(New Criteria(GetType(EndCustomer), "ChassisMaster.ID", MatchType.Exact, ECustomer.ChassisMaster.ID))
        objEndCustomerRet = New EndCustomerFacade(User).Retrieve(criterias)
        If objEndCustomerRet.Count > 0 Then
            objEndCustomer2 = objEndCustomerRet(0)
        End If
        ECustomer.ID = objEndCustomer2.ID
        objEndCustumerFacade.Update(ECustomer)
    End Sub

    Private Function searchSoldDealer() As Dealer
        Dim criteriasD As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriasD.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ID", MatchType.Exact, ObjChassisMaster.Dealer.ID))
        Dim dealerD As ArrayList = New DealerFacade(User).Retrieve(criteriasD)
        Dim objDealerD As Dealer

        If dealerD.Count > 0 Then
            objDealerD = CType(dealerD(0), Dealer)
            If objDealerD.DealerCode <> "" Then
                lblDealerSold.Text = objDealerD.DealerCode & " - " & objDealerD.SearchTerm1

            Else
                lblNoChassis.Text = ""
            End If
        End If
        Return objDealerD
    End Function

    Private Sub searchEndCustomer()
        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.EndCustomer), "ChassisMaster.ID", MatchType.Exact, ObjChassisMaster.ID))
        'New Criteria(GetType(ChassisMaster), "EndCustomer", Me.ID)
        Dim EndCustomerColl As ArrayList = New EndCustomerFacade(User).Retrieve(criterias2)

        If EndCustomerColl.Count > 0 Then
            objEndCustomer = CType(EndCustomerColl(0), EndCustomer)

            If Not IsNothing(objEndCustomer.OpenFakturDate) Then
                If objEndCustomer.OpenFakturDate <= "1/1/1900" Then
                    lblFakturOpenDate.Text = ""
                Else
                    lblFakturOpenDate.Text = objEndCustomer.OpenFakturDate.ToString("dd/MM/yyyy")
                End If
            End If

            If Not IsNothing(objEndCustomer.FakturDate) Then
                If objEndCustomer.FakturDate <= "1/1/1900" Then
                    lblFakturDate.Text = ""
                Else
                    lblFakturDate.Text = objEndCustomer.FakturDate.ToString("dd/MM/yyyy")
                End If
            End If

            'ICHandoverDate.Visible = True
            'If (_objDealer.DealerCode.Equals("MKS")) Then
            '    ICHandoverDate.Enabled = True
            'Else
            '    ICHandoverDate.Enabled = False
            'End If

            If Not IsNothing(ObjChassisMaster.ChassisMasterPKTDates) AndAlso ObjChassisMaster.ChassisMasterPKTDates.Count > 0 Then
                Dim pkt As ChassisMasterPKT
                pkt = CType(ObjChassisMaster.ChassisMasterPKTDates(0), ChassisMasterPKT)

                If pkt.PKTDate.Year <= 1900 Then
                    'lblHandoverDate.Text = ""
                    'ICHandoverDate.Value = New DateTime(1900, 1, 1)
                Else
                    lblHandoverDate.Text = pkt.PKTDate.ToString("dd/MM/yyyy")
                    'ICHandoverDate.Value = objEndCustomer.HandoverDate
                End If
            Else
                lblHandoverDate.Text = ""
                ICHandoverDate.Value = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
            End If
        End If
    End Sub

    Private Sub searchMaterialNumber()
        Dim criterias3 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias3.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "ID", MatchType.Exact, ObjChassisMaster.VechileColor.ID))
        Dim VichileColl As ArrayList = New VechileColorFacade(User).Retrieve(criterias3)
        Dim objVichileColor As VechileColor
        If VichileColl.Count > 0 Then
            objVichileColor = CType(VichileColl(0), VechileColor)
            lblMaterial.Text = objVichileColor.MaterialNumber & " - " & objVichileColor.MaterialDescription
        End If
    End Sub

    Private Sub SearchPMData()
        ObjChassisMaster = CType(sparepartColl(0), ChassisMaster)
        Dim PMList As ArrayList = New ArrayList
        If Not ObjChassisMaster Is Nothing Then
            PMList = ObjChassisMaster.PMHeaders
            If PMList.Count > 0 Then
                CommonFunction.SortArraylist(PMList, GetType(PMHeader), "StandKM", Sort.SortDirection.ASC)
            End If
            sHVeh.SetSession("objPMStatus", PMList)
            If PMList.Count > 0 Then
                dgPMStatus.DataSource = PMList
                dgPMStatus.DataBind()
            Else
                dgPMStatus.DataSource = New ArrayList
                dgPMStatus.DataBind()
            End If
        End If

    End Sub

    Private Sub searchChassisMaster()
        ObjChassisMaster = CType(sparepartColl(0), ChassisMaster)
        _chassisMasterId = ObjChassisMaster.ID
        If ObjChassisMaster.ChassisNumber <> "" Then
            lblNoChassis.Text = ObjChassisMaster.ChassisNumber
            If Not IsNothing(Request.Item("IsLeasing")) AndAlso Integer.Parse(Request.Item("IsLeasing")) = 1 Then
                Me.txtNoEngine.Text = ObjChassisMaster.EngineNumber
            End If
            lblNoEngine.Text = ObjChassisMaster.EngineNumber
            If ObjChassisMaster.DODate <= "1/1/1900" Then
                lblDOPrintDate.Text = ""
            Else
                lblDOPrintDate.Text = ObjChassisMaster.DODate.ToString("dd/MM/yyyy")
            End If
            lblNoSerial.Text = ObjChassisMaster.SerialNumber
            If ObjChassisMaster.GIDate <= "1/1/1900" Then
                lblStockOutDate.Text = ""
            Else
                lblStockOutDate.Text = ObjChassisMaster.GIDate.ToString("dd/MM/yyyy")
            End If
            Dim criterias3 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias3.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "ChassisMaster.ID", MatchType.Exact, ObjChassisMaster.ID))
            Dim colPDI As ArrayList = New PDIFacade(User).Retrieve(criterias3)
            Dim objPDI As PDI
            lblPDIIndicator.Text = ""
            BtnDeletePDI.Visible = False
            If colPDI.Count > 0 Then
                objPDI = CType(colPDI(0), PDI)

                sHVeh.SetSession("PDI", objPDI)
                lblTglPDI.Text = objPDI.PDIDate.ToString("dd/MM/yyyy")
                If objPDI.PDIStatus = EnumFSStatus.FSStatus.Baru Then
                    lblPDIIndicator.Text = "<img src=""../images/red.gif"" border=""0"" alt=""Baru"">"
                ElseIf objPDI.PDIStatus = EnumFSStatus.FSStatus.Rilis Then
                    lblPDIIndicator.Text = "<img src=""../images/yellow.gif"" border=""0"" alt=""Rilis"">"
                ElseIf objPDI.PDIStatus = EnumFSStatus.FSStatus.Proses Then
                    lblPDIIndicator.Text = "<img src=""../images/yellow.gif"" border=""0"" alt=""Proses"">"
                ElseIf objPDI.PDIStatus = EnumFSStatus.FSStatus.Selesai Then
                    lblPDIIndicator.Text = "<img src=""../images/green.gif"" border=""0"" alt=""Selesai"">"
                Else
                    lblPDIIndicator.Text = ""
                End If
                Dim criterias4 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias4.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ID", MatchType.Exact, objPDI.Dealer.ID))
                Dim colDealerPDI As ArrayList = New DealerFacade(User).Retrieve(criterias4)
                Dim objDealerPDI As Dealer
                If colDealerPDI.Count > 0 Then
                    objDealerPDI = CType(colDealerPDI(0), Dealer)
                    lblDealerPDI.Text = objDealerPDI.DealerCode & " - " & objDealerPDI.SearchTerm1
                End If
            End If
            searchSoldDealer()

            '--add start by rudi
            CheckConditionPKT(ObjChassisMaster.ID)
            '--add end by rudi

        Else
            lblNoChassis.Text = ""
        End If
        searchEndCustomer()
        searchMaterialNumber()
        SearchPMData()
    End Sub

    Private Sub CheckConditionPKT(intCMID As Integer)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ID", MatchType.Exact, intCMID))
        Dim arrCM As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)
        Dim oChassisMaster As ChassisMaster
        If Not IsNothing(arrCM) And arrCM.Count > 0 Then
            oChassisMaster = CType(arrCM(0), ChassisMaster)
            Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                If Not IsNothing(oChassisMaster.ChassisMasterPKTDates) And oChassisMaster.ChassisMasterPKTDates.Count > 0 Then
                    Dim isHasPKTPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.InputPKTDate_Privilege)
                    'btnSimpan.Visible = True
                    'ICHandoverDate.Visible = True
                    'ICHandoverDate.Enabled = True
                    lblHandoverDate.Visible = True
                    'btnEdit.Visible = True
                    btnEdit.Visible = isHasPKTPriv
                    ICHandoverDate.Value = CType(oChassisMaster.ChassisMasterPKTDates(0), ChassisMasterPKT).PKTDate
                    lblHandoverDate.Text = CType(oChassisMaster.ChassisMasterPKTDates(0), ChassisMasterPKT).PKTDate
                Else
                    'btnSimpan.Visible = False
                    'ICHandoverDate.Visible = False
                    'ICHandoverDate.Enabled = False
                    lblHandoverDate.Visible = False
                    btnEdit.Visible = False
                    ICHandoverDate.Value = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
                    lblHandoverDate.Text = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
                End If

            Else  '-- dealer login
                If Not IsNothing(oChassisMaster.ChassisMasterPKTDates) And oChassisMaster.ChassisMasterPKTDates.Count > 0 AndAlso CType(oChassisMaster.ChassisMasterPKTDates(0), ChassisMasterPKT).PKTDate.Year > 1900 Then
                    'btnSimpan.Visible = False
                    'ICHandoverDate.Visible = True
                    'ICHandoverDate.Enabled = False
                    lblHandoverDate.Visible = True
                    btnEdit.Visible = False
                    ICHandoverDate.Value = CType(oChassisMaster.ChassisMasterPKTDates(0), ChassisMasterPKT).PKTDate
                    lblHandoverDate.Text = CType(oChassisMaster.ChassisMasterPKTDates(0), ChassisMasterPKT).PKTDate
                Else
                    'btnSimpan.Visible = True
                    'ICHandoverDate.Visible = True
                    'ICHandoverDate.Enabled = True
                    lblHandoverDate.Visible = False

                    Dim criteriasDealerSystems As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSystems), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criteriasDealerSystems.opAnd(New Criteria(GetType(DealerSystems), "Dealer.ID", MatchType.Exact, objDealer.ID))
                    Dim DealerSystemsFacade As DealerSystemsFacade = New DealerSystemsFacade(User)
                    Dim arlDealerSystem As ArrayList = New ArrayList
                    arlDealerSystem = DealerSystemsFacade.Retrieve(criteriasDealerSystems)
                    Dim IsDealerDnet As Boolean = True
                    Dim isHasPKTPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.InputPKTDate_Privilege)
                    If Not IsNothing(arlDealerSystem) AndAlso arlDealerSystem.Count > 0 Then
                        If CType(arlDealerSystem(0), DealerSystems).SystemID <> 1 Then
                            IsDealerDnet = False
                        End If
                    End If
                    Dim isPloting As Boolean = TCHelper.GetActiveTCResult(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.PilotingPKT))

                    If oChassisMaster.Dealer.ID = objDealer.ID AndAlso IsDealerDnet AndAlso isHasPKTPriv AndAlso Not isPloting Then
                        btnEdit.Visible = True
                    Else
                        btnEdit.Visible = False
                    End If


                    ICHandoverDate.Value = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

                    lblHandoverDate.Text = ""
                End If
            End If
        End If
    End Sub

    Public Sub CheckAuthorityForDisplay()
        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
        If Not IsNothing(objDealer) Then
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If lblDealerSold.Text <> String.Empty And lblDealerSold.Text.Length >= 6 Then
                    Dim _DealerCode As String = Left(lblDealerSold.Text, 6)
                    If objDealer.DealerCode <> _DealerCode AndAlso lblFakturOpenDate.Text <> "" Then
                        lblMaterial.Visible = True
                        lblNoChassis.Visible = True
                        'lblNoEngine.Visible = True
                        lblNoSerial.Visible = True
                        lblFakturOpenDate.Visible = True
                        lblDealerSold.Visible = False
                        lblDealerPDI.Visible = False
                        lblDOPrintDate.Visible = True
                        'lblDOPrintDate.Visible = False 'req 20121109
                        lblStockOutDate.Visible = True
                        'lblStockOutDate.Visible = False 'req 20121109
                        lblTglPDI.Visible = True
                        'lblTglPDI.Visible = False 'req 20121109
                        lblPDIIndicator.Visible = False
                        BtnDeletePDI.Visible = False
                    ElseIf objDealer.DealerCode <> _DealerCode AndAlso lblFakturOpenDate.Text = "" Then
                        BtnDeletePDI.Visible = False
                        lblMaterial.Visible = False
                        lblNoChassis.Visible = False
                        'lblNoEngine.Visible = False
                        lblNoSerial.Visible = False
                        lblFakturOpenDate.Visible = False
                        lblDealerSold.Visible = False
                        lblDealerPDI.Visible = False
                        lblDOPrintDate.Visible = False
                        lblStockOutDate.Visible = False
                        lblTglPDI.Visible = False
                        lblPDIIndicator.Visible = False
                        dtgServiceData.DataSource = Nothing
                        dtgServiceData.DataBind()
                        dgServiceDataCampaign.DataSource = Nothing
                        dgServiceDataCampaign.DataBind()
                        dgPMStatus.DataSource = Nothing
                        dgPMStatus.DataBind()

                        MessageBox.Show("Chasis Tidak Dapat Dilihat")
                    Else
                        lblMaterial.Visible = True
                        lblNoChassis.Visible = True
                        'lblNoEngine.Visible = True
                        lblNoSerial.Visible = True
                        lblFakturOpenDate.Visible = True
                        lblDealerSold.Visible = True
                        lblDealerPDI.Visible = True
                        lblDOPrintDate.Visible = True
                        lblStockOutDate.Visible = True
                        lblTglPDI.Visible = True
                        BtnDeletePDI.Visible = False
                        lblPDIIndicator.Visible = True
                    End If
                End If
            Else
                lblMaterial.Visible = True
                lblNoChassis.Visible = True
                'lblNoEngine.Visible = True
                lblNoSerial.Visible = True
                lblFakturOpenDate.Visible = True
                lblDealerSold.Visible = True
                lblDealerPDI.Visible = True
                lblDOPrintDate.Visible = True
                lblStockOutDate.Visible = True
                lblTglPDI.Visible = True
                BtnDeletePDI.Visible = False

            End If
        End If

    End Sub

    Private Sub ClearLabel()
        lblDealerPDI.Text = ""
        lblDealerSold.Text = ""
        lblDOPrintDate.Text = ""
        lblFakturOpenDate.Text = ""
        lblFakturDate.Text = ""
        lblMaterial.Text = ""
        lblNoChassis.Text = ""
        lblNoEngine.Text = ""
        lblNoSerial.Text = ""
        lblStockOutDate.Text = ""
        lblTglPDI.Text = ""
        lblNoRegRequest.Text = ""
        lblKeteranganFleet.Text = ""
    End Sub

    '--14-09-2018--
    Public Function searchResultFSType() As String
        Dim fsType As String = String.Empty
        Dim fsTypeRet As String = String.Empty
        Dim dataSetResultFSType As New DataSet
        Dim dataSetResultFSTypeMSP As New DataSet
        Dim cri As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        cri.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumFSKind"))

        Dim arrsc As ArrayList = New StandardCodeFacade(User).Retrieve(cri)

        dataSetResultFSTypeMSP = New FSKindOnVechileTypeFacade(User).RetrieveFromSP_MSP(Me.txtChassisNumber.Text)
        dataSetResultFSType = New FSKindOnVechileTypeFacade(User).RetrieveFromSP(Me.txtChassisNumber.Text)

        If Not IsNothing(dataSetResultFSType) AndAlso dataSetResultFSType.Tables.Count > 0 Then

            For Each dr As DataRow In dataSetResultFSType.Tables(0).Rows
                If Integer.Parse(dr.ItemArray.FirstOrDefault) < 4 Then ' lihat di standard code dengan category "EnumFSKIND"
                    If fsType = "" Then
                        fsType = CommonFunction.GetEnumDescription(Integer.Parse(dr.ItemArray.FirstOrDefault), "EnumFSKind")
                        'fsType = New EnumFSKind().TypeByIndex(Integer.Parse(dr.ItemArray.FirstOrDefault()))
                    Else
                        fsType += ", " + CommonFunction.GetEnumDescription(Integer.Parse(dr.ItemArray.FirstOrDefault), "EnumFSKind")
                        'fsType += ", " + New EnumFSKind().TypeByIndex(Integer.Parse(dr.ItemArray.FirstOrDefault()))
                    End If
                Else

                    If Not IsNothing(dataSetResultFSTypeMSP) AndAlso dataSetResultFSTypeMSP.Tables.Count > 0 Then
                        For Each drmsp As DataRow In dataSetResultFSTypeMSP.Tables(0).Rows
                            If fsType = "" Then
                                fsType = drmsp.ItemArray.FirstOrDefault
                            Else
                                fsType += ", Maintenance " + drmsp.ItemArray.FirstOrDefault
                            End If
                        Next
                    End If
                    Exit For
                End If
            Next
        Else
            If Not IsNothing(dataSetResultFSTypeMSP) AndAlso dataSetResultFSTypeMSP.Tables.Count > 0 Then
                For Each dr As DataRow In dataSetResultFSTypeMSP.Tables(0).Rows
                    If fsType = "" Then
                        fsType = dr.ItemArray.FirstOrDefault
                    Else
                        fsType += ", Maintenance " + dr.ItemArray.FirstOrDefault
                    End If
                Next
            End If
        End If

        Return fsType
    End Function

    Public Sub searchVehicleInformation()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, Me.txtChassisNumber.Text))
        sparepartColl = New ChassisMasterFacade(User).Retrieve(criterias)

        sHVeh.SetSession("SPCOLL", sparepartColl)
        Dim ObjChassisMaster As ChassisMaster
        If sparepartColl.Count > 0 Then
            searchChassisMaster()
            ShowServiceDataCampaign()
            dtgServiceData.Visible = True
            dgPMStatus.Visible = True
            bindFreeService()
            bindFleet()
            '-- start add by rudi
            bindWarrantyServiceClaim()
            '-- end add by rudi
            SearchPMData()
            BindMSP()

            CheckAuthorityForDisplay()
        Else
            dtgServiceData.Visible = False
            dgPMStatus.Visible = False
            MessageBox.Show(SR.DataNotFound("Data"))
        End If
    End Sub

    Private Sub ShowServiceDataCampaign()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Partial, Me.txtChassisNumber.Text.Trim()))

        Dim ObjChassis As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)

        Me.dgServiceDataCampaign.DataSource = New ArrayList
        Me.dgServiceDataCampaign.DataBind()


        If Not IsNothing(ObjChassis) AndAlso ObjChassis.Count > 0 AndAlso Not IsNothing(CType(ObjChassis(0), ChassisMaster).VechileColor) Then

            criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.RecallService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallService), "ChassisMaster.ID", MatchType.Exact, CType(ObjChassis(0), ChassisMaster).ID))
            Dim sparepartColl As ArrayList = New RecallServiceFacade(User).Retrieve(criterias)

            If Not IsNothing(sparepartColl) Then
                Me.dgServiceDataCampaign.DataSource = sparepartColl
                Me.dgServiceDataCampaign.DataBind()
            Else
                Me.dgServiceDataCampaign.DataSource = New ArrayList
                Me.dgServiceDataCampaign.DataBind()

            End If

        Else
            Me.dgServiceDataCampaign.DataSource = New ArrayList
            Me.dgServiceDataCampaign.DataBind()
        End If



        'criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.RecallChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallChassisMaster), "ChassisNo", MatchType.Exact, txtChassisNumber.Text.Trim()))
        'Dim CCC As ArrayList = New RecallChassisMasterFacade(User).Retrieve(criterias)

        'If (Not IsNothing(CCC) AndAlso CCC.Count > 0) Then

        'Else
        '    Me.dgServiceDataCampaign.DataSource = New ArrayList
        '    Me.dgServiceDataCampaign.DataBind()
        'End If

    End Sub

    Private Sub BindMSP()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPClaim), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPClaim), "ChassisMaster.ChassisNumber", MatchType.Exact, Me.txtChassisNumber.Text))


        Dim result As New ArrayList
        result = New MSPClaimFacade(User).Retrieve(criterias)
        dgMSP.DataSource = result
        dgMSP.DataBind()
    End Sub
#End Region


#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        _sessHelper = New SessionHelper
        _objDealer = CType(_sessHelper.GetSession("DEALER"), Dealer)

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitialPage()
        End If

        'Dim _sesshelper As New SessionHelper
        'Dim oDealer As Dealer = CType(_sesshelper.GetSession("DEALER"), Dealer)

        'If (_objDealer.DealerCode.Equals("MKS")) Then
        '    ICHandoverDate.Enabled = True
        'Else
        '    ICHandoverDate.Enabled = False
        'End If
    End Sub

    Private Sub InitialPage()
        If Not SecurityProvider.Authorize(Context.User, SR.PKTHistoryChange_Privilege) Then
            lnkbtnPopUpRefClaimNumber.Visible = False
        End If
        txtChassisNumber.Attributes.Add("onkeydown", "enter(document.all.btnSearch)")
        ViewState("currentSortColumn") = "FSKind.KindCode"
        ViewState("currentSortDirection") = Sort.SortDirection.ASC
        ViewState("CurrentSortColumnX") = "ID"
        ViewState("CurrentSortDirectX") = Sort.SortDirection.ASC

        If _objDealer.Title = 0 Then
            lblNoEngine.Visible = False
            lblNoEngineColon.Visible = False
            lblNoEngineTitle.Visible = False
        ElseIf _objDealer.Title = 1 Then
            lblNoEngine.Visible = True
            lblNoEngineColon.Visible = True
            lblNoEngineTitle.Visible = True
        End If
    End Sub

    Private Sub ActivateUserPrivilege()
        'Start  :by:dna;for:Rina;purpose:Add leasing menu;Time:20100528
        If Not IsNothing(Request.Item("IsLeasing")) AndAlso Integer.Parse(Request.Item("IsLeasing")) = 1 Then Exit Sub
        'End    :by:dna;for:Rina;purpose:Add leasing menu;Time:20100528
        If Not SecurityProvider.Authorize(Context.User, SR.InformasiKendaraanView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Finish Unit - Form Informasi Kendaraan")
        End If
        isDeleteAuth = SecurityProvider.Authorize(Context.User, SR.ENHDataServiceHapus_Privilege)
    End Sub

    Private Sub dgChassis_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As ChassisMaster = CType(e.Item.DataItem, ChassisMaster)

            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblDealerID As Label = CType(e.Item.FindControl("lblDealerID"), Label)
                If Not IsNothing(RowValue.Dealer) Then
                    lblDealerID.Text = RowValue.Dealer.ID
                End If
                Dim lblMatNumber As Label = CType(e.Item.FindControl("lblMatNumber"), Label)
                If Not IsNothing(RowValue.VechileColor) Then
                    lblMatNumber.Text = RowValue.VechileColor.ID
                End If

            End If

        End If

    End Sub

    Private Sub dgEndCustomer_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As EndCustomer = CType(e.Item.DataItem, EndCustomer)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblProvID As Label = CType(e.Item.FindControl("lblProvID"), Label)
                If Not IsNothing(RowValue.Customer.City.Province) Then
                    lblProvID.Text = RowValue.Customer.City.Province.ID
                End If
                Dim lblArea1 As Label = CType(e.Item.FindControl("lblArea1"), Label)
                If Not IsNothing(RowValue.Customer.City) Then
                    lblArea1.Text = RowValue.Customer.City.ID
                End If
            End If
        End If
    End Sub

    Private Sub SearchData()
        If txtChassisNumber.Text <> "" Then
            ClearLabel()
            searchVehicleInformation()
            Dim FSResult As String = searchResultFSType()
            If FSResult.Length > 0 Then
                FSResult = FSResult & "; " & MSPExtResult()
            Else
                FSResult = MSPExtResult()
            End If
            lblFSType.Text = FSResult
            lblRecallChassisMaster.Text = GetRecallCategoryDescription(txtChassisNumber.Text)
        End If
    End Sub

    Private Function GetRecallCategoryDescription(ByVal chassisNumber As String) As String
        Dim result As String
        Try

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.RecallChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallChassisMaster), "ChassisNo", MatchType.Exact, chassisNumber))

            Dim listRecallChassisMasterData As ArrayList = New RecallChassisMasterFacade(User).Retrieve(criterias)

            If listRecallChassisMasterData.Count > 0 Then

                For i As Integer = 0 To listRecallChassisMasterData.Count - 1
                    Dim data As RecallChassisMaster = CType(listRecallChassisMasterData(i), RecallChassisMaster)

                    result += data.RecallCategory.RecallRegNo + " - " + data.RecallCategory.Description

                    If i <> listRecallChassisMasterData.Count - 1 Then
                        result += " <br />"
                    End If
                Next
            Else
                result = String.Empty
            End If

        Catch ex As Exception
            result = String.Empty
        End Try
        Return result
    End Function

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        btnSimpan.Visible = False
        ICHandoverDate.Visible = False
        btnSimpan.Visible = False
        SearchData()

        lnkbtnPopUpRefClaimNumber.Attributes("onClick") = "ShowPPInfoWSCSelection()"

        If SecurityProvider.Authorize(Context.User, SR.PKTHistoryChange_Privilege) Then
            lnkbtnPopUpRefClaimNumber.Visible = True
        Else
            lnkbtnPopUpRefClaimNumber.Visible = False
        End If
        Dim _sesshelper As New SessionHelper
        Dim oDealer As Dealer = CType(_sesshelper.GetSession("DEALER"), Dealer)
        Dim oUserInfo As UserInfo = CType(_sesshelper.GetSession("LOGINUSERINFO"), UserInfo)
        If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            lbtnDeletePDI.Visible = False
        Else
            If lblTglPDI.Text = "" Then
                lbtnDeletePDI.Visible = False
            Else
                lbtnDeletePDI.Visible = True
            End If
        End If

        'ICHandoverDate.Visible = True
        'If (oDealer.DealerCode.Equals("MKS")) Then
        '    ICHandoverDate.Enabled = True
        'Else
        '    ICHandoverDate.Enabled = False
        'End If



    End Sub

    Private Sub dtgServiceData_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgServiceData.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgServiceData.CurrentPageIndex * dtgServiceData.PageSize)
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As FreeService = CType(e.Item.DataItem, FreeService)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)

                If RowValue.Status = EnumFSStatus.FSStatus.Baru Then
                    lblStatus.Text = "<img src=""../images/red.gif"" border=""0"" alt=""Baru"">"
                ElseIf RowValue.Status = EnumFSStatus.FSStatus.Rilis Then
                    lblStatus.Text = "<img src=""../images/yellow.gif"" border=""0"" alt=""Rilis"">"
                ElseIf RowValue.Status = EnumFSStatus.FSStatus.Proses Then
                    lblStatus.Text = "<img src=""../images/yellow.gif"" border=""0"" alt=""Proses"">"
                ElseIf RowValue.Status = EnumFSStatus.FSStatus.Selesai Then
                    lblStatus.Text = "<img src=""../images/green.gif"" border=""0"" alt=""Selesai"">"
                End If

                Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
                If Not RowValue Is Nothing Then
                    lblID.Text = RowValue.ID
                End If
                If Not IsNothing(RowValue.Dealer) Then
                    lblDealer.Text = RowValue.Dealer.DealerCode & " - " & RowValue.Dealer.SearchTerm1
                End If
                Dim lblkind As Label = CType(e.Item.FindControl("lblKind"), Label)
                If Not IsNothing(RowValue.FSKind) Then
                    lblkind.Text = RowValue.FSKind.KindDescription
                End If
                Dim lblFS As Label = CType(e.Item.FindControl("lblFS"), Label)
                If Not IsNothing(RowValue.ServiceDate) Then
                    If RowValue.ServiceDate <= "1/1/1900" Then
                        lblFS.Text = ""
                    Else
                        lblFS.Text = Format(RowValue.ServiceDate, "dd/MM/yyyy")
                    End If
                End If
                Dim lblTglPro As Label = CType(e.Item.FindControl("lblTglPro"), Label)
                If Not IsNothing(RowValue.CreatedTime) Then
                    If RowValue.ServiceDate <= "1/1/1900" Then
                        lblTglPro.Text = ""
                    Else
                        lblTglPro.Text = Format(RowValue.CreatedTime, "dd/MM/yyyy")
                    End If
                End If
                Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                lbtnDelete.Visible = isDeleteAuth
            End If
        End If
    End Sub

    Private Sub dtgServiceData_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgServiceData.PageIndexChanged
        dtgServiceData.CurrentPageIndex = e.NewPageIndex
        dtgServiceData.DataBind()
    End Sub

#End Region

    Private Sub dtgServiceData_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgServiceData.SortCommand
        If CType(ViewState("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currentSortColumn") = e.SortExpression
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
        End If

        dtgServiceData.SelectedIndex = -1
        dtgServiceData.CurrentPageIndex = 0
        bindGridSorting(dtgServiceData.CurrentPageIndex)
    End Sub
    Private Sub dgPMStatus_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPMStatus.SortCommand
        If CType(ViewState("CurrentSortColumnX"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirectX"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirectX") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirectX") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumnX") = e.SortExpression
            ViewState("CurrentSortDirectX") = Sort.SortDirection.ASC
        End If
        dgPMStatus.SelectedIndex = -1
        dgPMStatus.CurrentPageIndex = 0
        SearchPMDataShort(dgPMStatus.CurrentPageIndex)
    End Sub
    Private Sub SearchPMDataShort(ByVal currPageindex As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisNumber.Text))
        If (currPageindex >= 0) Then
            dgPMStatus.DataSource = New PMHeaderFacade(User).RetrieveActiveList(criterias, currPageindex + 1, dgPMStatus.PageSize, totalRow, CType(ViewState("CurrentSortColumnX"), String), CType(ViewState("CurrentSortDirectX"), Sort.SortDirection))
            dgPMStatus.VirtualItemCount = totalRow
            dgPMStatus.DataBind()
        End If
    End Sub
    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dtgServiceData.DataSource = New FreeServiceFacade(User).RetrieveActiveList(CType(sHVeh.GetSession("infoSort"), CriteriaComposite), indexPage + 1, dtgServiceData.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dtgServiceData.VirtualItemCount = totalRow
            dtgServiceData.DataBind()
        End If
    End Sub
    Private Sub dtgServiceData_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgServiceData.EditCommand

    End Sub
    Private Sub dtgServiceData_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgServiceData.ItemCommand
        If e.CommandName = "Delete" Then
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim id As Integer = lblID.Text
            Dim objFreeService As FreeService = New FreeServiceFacade(User).Retrieve(id)
            If Not objFreeService Is Nothing Then
                If objFreeService.ID > 0 Then
                    objFreeService.RowStatus = DBRowStatus.Deleted
                    Dim objFSFacade As FreeServiceFacade = New FreeServiceFacade(User)
                    Dim i As Integer = objFSFacade.Update(objFreeService)
                    If i = -1 Then
                        MessageBox.Show(SR.DeleteFail)
                    Else
                        If txtChassisNumber.Text <> "" Then
                            ClearLabel()
                            searchVehicleInformation()
                        End If
                        MessageBox.Show(SR.DeleteSucces)
                    End If
                End If
            End If

        End If
    End Sub
    Private Sub BtnDeletePDI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDeletePDI.Click
        Dim objPDI As PDI = sHVeh.GetSession("PDI")
        If Not objPDI Is Nothing Then
            Try
                Dim objPDIFacade As PDIFacade = New PDIFacade(User)
                objPDIFacade.Delete(objPDI)
                SearchData()
            Catch ex As Exception
                MessageBox.Show("Hapus Data tidak berhasil")
            End Try

        End If
    End Sub
    Private Sub ItemTypeDataBound(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim RowValue As PMHeader = CType(e.Item.DataItem, PMHeader)

        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
        Dim lblTglPM As Label = CType(e.Item.FindControl("lblTglPM"), Label)
        Dim lblTglRilis As Label = CType(e.Item.FindControl("lblTglRilis"), Label)
        Dim lblPopUp As Label = CType(e.Item.FindControl("lblPopUpDetail"), Label)
        lblPopUp.Visible = False
        Dim lblKodeDealer As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)
        Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

        lblKodeDealer.Text = RowValue.Dealer.DealerCode & " - " & RowValue.Dealer.SearchTerm1
        lblNo.Text = (e.Item.ItemIndex + 1) + (dgPMStatus.CurrentPageIndex * dgPMStatus.PageSize)

        If RowValue.ServiceDate <= "01/01/1900" Then
            lblTglPM.Text = ""
        Else
            lblTglPM.Text = RowValue.ServiceDate.ToString("dd/MM/yyyy")
        End If

        If RowValue.ReleaseDate <= "01/01/1900" Then
            lblTglRilis.Text = ""
        Else
            lblTglRilis.Text = RowValue.ReleaseDate.ToString("dd/MM/yyyy")
        End If
        If RowValue.PMStatus <> "" Then

            If RowValue.PMStatus = EnumPMStatus.PMStatus.Baru Then
                lblStatus.Text = "<img src=""../images/red.gif"" border=""0"" alt=""Baru"">"
            ElseIf RowValue.PMStatus = EnumPMStatus.PMStatus.Proses Then
                lblStatus.Text = "<img src=""../images/yellow.gif"" border=""0"" alt=""Proses"">"
                Dim objDealer As Dealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
                If (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
                    lbtnDelete.Visible = True
                Else
                    lbtnDelete.Visible = False
                End If

                'lbtnDelete.Visible = False            
            ElseIf RowValue.PMStatus = EnumPMStatus.PMStatus.Selesai Then
                lblStatus.Text = "<img src=""../images/green.gif"" border=""0"" alt=""Selesai"">"
                Dim objDealer As Dealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
                If (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
                    lbtnDelete.Visible = True
                Else
                    lbtnDelete.Visible = False
                End If
                'lbtnDelete.Visible = False
            End If
        Else
            lblStatus.Text = ""
        End If

        Dim lblMSPDescription As Label = CType(e.Item.FindControl("lblMSPDescription"), Label)
        If Not IsNothing(lblMSPDescription) Then
            lblMSPDescription.Text = RowValue.Remarks
        End If

        lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../Service/FrmPopUpPMDetail.aspx?id=" & RowValue.ID & "&index=-1", "", 310, 500, "ShowPopUp")

    End Sub
    Private Sub SortListControl(ByRef pCompletelist As ArrayList, ByVal SortColumn As String, ByVal SortDirection As Integer)

        Dim IsAsc As Boolean = True
        If SortDirection = Sort.SortDirection.ASC Then
            IsAsc = True
        ElseIf SortDirection = Sort.SortDirection.DESC Then
            IsAsc = False
        End If

        Dim objListComparer As IComparer = New ListComparer(IsAsc, SortColumn)
        If pCompletelist.Count > 0 Then
            pCompletelist.Sort(objListComparer)
        End If
    End Sub
    Private Sub FooterTypeDataBound(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objPMHeaderCol As ArrayList = CType(sHVeh.GetSession("objPMStatus"), ArrayList)
        Dim currObj As PMHeader
        Dim cmpObj As PMHeader
        Dim DealerCount As Integer = 0



        SortListControl(objPMHeaderCol, "Dealer.DealerCode", Sort.SortDirection.ASC)

        If objPMHeaderCol.Count > 0 Then
            For i As Integer = 0 To objPMHeaderCol.Count - 1
                If i = 0 Then
                    cmpObj = objPMHeaderCol(i)
                    DealerCount = DealerCount + 1
                Else
                    currObj = objPMHeaderCol(i)
                    If currObj.Dealer.DealerCode <> cmpObj.Dealer.DealerCode Then
                        cmpObj = objPMHeaderCol(i)
                        DealerCount = DealerCount + 1
                    End If
                End If

            Next
        End If


    End Sub
    Private Sub BindItemdgChassisNumber(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            ItemTypeDataBound(e)
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            FooterTypeDataBound(e)
        End If

    End Sub
    Private Sub dgPMStatus_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPMStatus.ItemDataBound
        BindItemdgChassisNumber(e)
    End Sub

    Private Sub dgPMStatus_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPMStatus.ItemCommand
        If e.CommandName = "DeletePM" Then
            Dim lblID As Label = CType(e.Item.FindControl("lblPMID"), Label)
            Dim id As Integer = lblID.Text
            Dim objPM As PMHeader = New PMHeaderFacade(User).Retrieve(id)
            If Not objPM Is Nothing Then
                If objPM.ID > 0 Then

                    'If objPM.PMStatus = EnumPMStatus.PMStatus.Selesai Then
                    'MessageBox.Show("Data Tidak Dapat Dihapus")
                    'Exit Sub
                    'End If

                    objPM.RowStatus = DBRowStatus.Deleted
                    Dim objPMFacade1 As PMHeaderFacade = New PMHeaderFacade(User)
                    Try
                        objPMFacade1.Delete(objPM)
                        If txtChassisNumber.Text <> "" Then
                            ClearLabel()
                            searchVehicleInformation()
                        End If
                        MessageBox.Show(SR.DeleteSucces)
                    Catch ex As Exception
                        MessageBox.Show(SR.DeleteFail)
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub lbtnDeletePDI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnDeletePDI.Click
        BtnDeletePDI_Click(Me, System.EventArgs.Empty)
    End Sub

    Private Sub btnSearchEngine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchEngine.Click
        'Start  :by:dna;for:doniset;on:20100623;remark:For Leasing Purpose Only
        If Me.txtNoEngine.Text.Trim = "" Then Exit Sub
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, Me.txtChassisNumber.Text))
        sparepartColl = New ChassisMasterFacade(User).Retrieve(criterias)

        Dim oCMFac As ChassisMasterFacade = New ChassisMasterFacade(User)
        Dim cCM As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim aCM As New ArrayList

        cCM.opAnd(New Criteria(GetType(ChassisMaster), "EngineNumber", MatchType.Exact, txtNoEngine.Text.Trim))
        aCM = oCMFac.Retrieve(cCM)
        If aCM.Count > 0 Then
            Me.txtChassisNumber.Text = CType(aCM(0), ChassisMaster).ChassisNumber
            btnSearch_Click(sender, e)
        End If
        'End    :by:dna;for:doniset;on:20100623;remark:For Leasing Purpose Only
        'ICHandoverDate.Visible = True

        'If (_objDealer.DealerCode.Equals("MKS")) Then
        '    ICHandoverDate.Enabled = True
        'Else
        '    ICHandoverDate.Enabled = False
        'End If
    End Sub

    '--add start by rudi
    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        '    Dim _pktDateFacade As New PKTDateFacade(User)
        '    Dim objPKTDate As New PKTDate()

        Dim sstop As Boolean = False


        If ICHandoverDate.Value.Year <= 1900 Then
            sstop = True

            MessageBox.Show("Tanggal PKT tidak valid")

            Return
        End If

        If sstop Then
            Exit Sub
        End If

        lblHandoverDate.Visible = True
        'btnEdit.Visible = True
        ICHandoverDate.Visible = False
        btnSimpan.Visible = False

        Dim objCMPKTFacade As New ChassisMasterPKTFacade(User)
        Dim objCMPKT As New ChassisMasterPKT()
        Dim objChassis As ChassisMaster = GetChassis()
        If IsNothing(objChassis) Then
            Return
        End If

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterPKT), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterPKT), "ChassisMaster.ID", MatchType.Exact, objChassis.ID))
        Dim arObjCMPKT As ArrayList
        arObjCMPKT = New ChassisMasterPKTFacade(User).Retrieve(criterias)

        If (arObjCMPKT.Count > 0) Then
            objCMPKT = arObjCMPKT(0)
        End If

        objCMPKT.ChassisMaster = objChassis
        objCMPKT.PKTDate = ICHandoverDate.Value
        Dim intResult As Integer = -1
        Dim str As String
        If (arObjCMPKT.Count > 0) Then
            intResult = objCMPKTFacade.Update(objCMPKT)
            str = "Update"
        Else
            intResult = objCMPKTFacade.Insert(objCMPKT)
            str = "Insert"
        End If
        insertPKTChange(objCMPKT.PKTDate)

        If intResult < 0 Then
            MessageBox.Show("Data gagal di" & str)
            Exit Sub
        End If

        '--add start by rudi
        CheckConditionPKT(objChassis.ID)
        '--add end by rudi

        'To SAP
        Dim PKTDate As String = ICHandoverDate.Value
        lblHandoverDate.Text = PKTDate
        SendToSap(objChassis.ChassisNumber, PKTDate)
        'end To SAP

        '    Dim objEndCustomer As EndCustomer = GetEndCustomer(objChassis.ID)
        '    If IsNothing(objEndCustomer) = False Then
        '        objEndCustomer.HandoverDate = ICHandoverDate.Value
        '        Dim endCustomerFacade As EndCustomerFacade = New EndCustomerFacade(User)
        '        endCustomerFacade.Update(objEndCustomer)
        '    End If

        MessageBox.Show("Simpan data berhasil.")
    End Sub
    '--add end by rudi

    Private Function GetChassis()
        Dim _chassisNo As String = Me.txtChassisNumber.Text
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, _chassisNo))
        arChassisMaster = New ChassisMasterFacade(User).Retrieve(criterias)
        If (arChassisMaster.Count > 0) Then
            Return CType(arChassisMaster(0), ChassisMaster)
        End If

        Return Nothing
    End Function

    Private Function GetEndCustomer(chassisId As Integer)
        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.EndCustomer), "ChassisMaster.ID", MatchType.Exact, chassisId))
        Dim EndCustomerColl As ArrayList = New EndCustomerFacade(User).Retrieve(criterias2)

        If EndCustomerColl.Count > 0 Then
            objEndCustomer = CType(EndCustomerColl(0), EndCustomer)
            Return objEndCustomer
        End If
        Return Nothing
    End Function

    Private Sub dgWarrantyServiceClaim_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgWarrantyServiceClaim.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim RowValue As New WSCHeader

            RowValue = e.Item.DataItem

            Dim lblClaimStatus As Label = CType(e.Item.FindControl("lblClaimStatus"), Label)

            If RowValue.Status = 0 Then
                lblClaimStatus.Text = "<img src=""../images/red.gif"" border=""0"" alt=""Baru"">"
            ElseIf RowValue.Status = 1 Then
                lblClaimStatus.Text = "<img src=""../images/yellow.gif"" border=""0"" alt=""Proses"">"
            ElseIf RowValue.Status = 2 Then
                lblClaimStatus.Text = "<img src=""../images/green.gif"" border=""0"" alt=""Selesai"">"
            End If
        End If
    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs)
        ICHandoverDate.Visible = True
        btnSimpan.Visible = True
        btnEdit.Visible = False
        lblHandoverDate.Visible = False
    End Sub

    Private Sub SendToSap(ByVal chassisNumber As String, ByVal PKTDate As String)

        Dim i As Integer = 0
        Dim arl As ArrayList = New ArrayList
        Dim success As Boolean = False

        Dim IsCheck As Boolean = False
        Dim sw As StreamWriter
        Dim sb As New StringBuilder
        Dim filename = String.Format("{0}{1}{2}", "PKTData", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt") 'PKTData[TimeStamp].txt
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("FinishUnitFileDirectory") & "\PKT\" & filename  '-- Destination file
        Dim LocalDest As String = Server.MapPath("") & "\..\DataTemp\" & filename
        Dim tmp As Integer = 0

        sb.Append(chassisNumber & ";" & CType(PKTDate, Date).ToString("ddMMyyyy")) 'EquipmentNo;PKTDate
        sb.Append(vbNewLine)

        If (sb.Length > 0) Then
            If Transfer(DestFile, LocalDest, sb.ToString()) Then         '>> Code utk upload data lsg ke SAP Folder
                MessageBox.Show("Send tanggal PKT ke SAP sukses")
            Else
                MessageBox.Show("Send tanggal PKT ke SAP gagal")
            End If
        End If
    End Sub

    Private Function Transfer(ByVal DestFile As String, ByVal DestFileLocal As String, ByVal Val As String) As Boolean
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
                sw.Write(Val.ToString)
                sw.Close()
                fs.Close()

                'Server
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If
                If finfo.Exists Then
                    finfo.Delete()
                End If
                File.Copy(DestFileLocal, DestFile)
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch
            success = False
            sw.Close()
        End Try
        Return success
    End Function

    Private Sub insertPKTChange(ByVal pktDate As Date)
        Dim objPKTChange As PKTChangeHistory = New PKTChangeHistory
        objPKTChange.DocType = CType(LookUp.DocumentType.PKT, Short)
        objPKTChange.DocNumber = txtChassisNumber.Text
        If lblHandoverDate.Text <> "" Then

            Try
                objPKTChange.OldDate = lblHandoverDate.Text
            Catch ex As Exception

            End Try
        End If

        objPKTChange.NewDate = pktDate
        objPKTChange.Description = "Tanggal PKT"

        Dim result As Integer = New PKTChangeHistoryFacade(User).Insert(objPKTChange)
        If result > 0 Then
            'MessageBox.Show("Simpan Change History ")
        Else
        End If

    End Sub

    Private Sub dgMSP_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgMSP.ItemDataBound


        If Not IsNothing(e.Item.DataItem) Then

            Dim objClaim As MSPClaim = e.Item.DataItem

            Dim lblNo As Label = e.Item.FindControl("lblNo")

            lblNo.Text = (e.Item.ItemIndex + 1) + (dgMSP.CurrentPageIndex * dgMSP.PageSize)

            Dim lblStatus As Label = e.Item.FindControl("lblStatus")

            If Not IsNothing(lblStatus) Then

                lblStatus.Text = CType(objClaim.Status, EnumStatusMSP.Status).ToString()
            End If
        End If
    End Sub

    Private Sub dgServiceDataCampaign_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgServiceDataCampaign.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgServiceDataCampaign.CurrentPageIndex * dgServiceDataCampaign.PageSize)
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As RecallService = CType(e.Item.DataItem, RecallService)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)

                If Not IsNothing(RowValue.Dealer) Then
                    lblDealer.Text = RowValue.Dealer.DealerCode & " - " & RowValue.Dealer.SearchTerm1
                End If

                Dim lblTglPro As Label = CType(e.Item.FindControl("lblTglPro"), Label)
                If Not IsNothing(RowValue.CreatedTime) Then
                    If RowValue.ServiceDate <= "1/1/1900" Then
                        lblTglPro.Text = ""
                    Else
                        lblTglPro.Text = Format(RowValue.CreatedTime, "dd/MM/yyyy")
                    End If
                End If

            End If
        End If
    End Sub

    Private Function MSPExtResult() As String
        Dim _return As String = String.Empty
        Dim arrReg As ArrayList = New MSPExRegistrationFacade(User).RetrieveArrChassisNumber(Me.txtChassisNumber.Text)
        If arrReg.Count > 0 Then
            For Each item As MSPExRegistration In arrReg
                If _return.Length > 0 Then
                    _return = _return & item.MSPExMaster.MSPExType.Description
                Else
                    _return = item.MSPExMaster.MSPExType.Description
                End If
            Next
        End If
        Return _return
    End Function

End Class