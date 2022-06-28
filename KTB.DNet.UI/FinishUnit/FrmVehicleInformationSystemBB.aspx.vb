#Region ".NET Base Class Namespace Imports"
Imports System.IO
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
#End Region

Public Class FrmVehicleInformationSystemBB
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtChassisNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgServiceData As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblMaterial As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoChassis As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoEngine As System.Web.UI.WebControls.Label
    Protected WithEvents lblStockOutDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerSold As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoSerial As System.Web.UI.WebControls.Label
    Protected WithEvents lblDOPrintDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblFakturOpenDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerPDI As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglPDI As System.Web.UI.WebControls.Label

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
    Private bPrivilegeVehicleInfo As Boolean = False

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Dim arlist, arlist2 As ArrayList
    Dim sparepartColl, arChassisMasterBB As ArrayList
    Dim ObjChassisMasterBB As ChassisMasterBB
    Dim objEndCustomer As EndCustomer
    Dim sHVeh As SessionHelper = New SessionHelper
    Dim sHECus As SessionHelper = New SessionHelper
    Dim isDeleteAuth As Boolean = False
    Private _showForWSCSpecial As Boolean = False

#End Region

#Region "Custom Method"

    Private Sub bindFreeServiceBB()
        Dim criterias2 As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "ChassisNumber", MatchType.Exact, Me.txtChassisNumber.Text))
        arChassisMasterBB = New ChassisMasterBBFacade(User).Retrieve(criterias)
        Dim ObjChassisMasterBB As ChassisMasterBB
        For Each ObjChassisMasterBB In arChassisMasterBB
            criterias2 = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "ChassisMasterBB.ID", MatchType.Exact, ObjChassisMasterBB.ID))

        Next
        sHVeh.SetSession("infoSort", criterias2)
        'dtgServiceData.DataSource() = New FreeServiceBBFacade(User).Retrieve(criterias2)
        Dim indexPage As Integer = 0
        Dim totalRow As Integer = 0
        dtgServiceData.DataSource() = New FreeServiceBBFacade(User).RetrieveActiveList(CType(sHVeh.GetSession("infoSort"), CriteriaComposite), indexPage + 1, dtgServiceData.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
        dtgServiceData.DataBind()
    End Sub



    Private Sub getChassisData()
        Dim bError As Boolean = False

        For Each VInformationSystem As ChassisMasterBB In arlist
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

    Private Sub saveChassisMasterBB()

        Dim arList As ArrayList = CType(sHVeh.GetSession("VehInformationSystem"), ArrayList)

        For Each VehInformationSystem As ChassisMasterBB In arList
            If Not IsExistCode(VehInformationSystem.ChassisNumber) Then
                InsertChassisMasterBB(VehInformationSystem)  '-- Insert 
            Else
                UpdateChassisMasterBB(VehInformationSystem)  '-- Update 
            End If
        Next
    End Sub

    Private Sub saveEndCustomer()
        If Me._showForWSCSpecial Then
            'Dim arList2 As ArrayList = CType(sHECus.GetSession("ECustomer"), ArrayList)
            'For Each ECustomer As EndCustomer In arList2
            '    If Not IsExistCode2(ECustomer.ChassisMasterBB.ID) Then
            '        InsertEndCustomer(ECustomer)  '-- Insert 
            '    Else
            '        UpdateEndCustomer(ECustomer)  '-- Update
            '    End If
            'Next
        End If
    End Sub

    Private Function IsExistCode(ByVal sCode As String) As Boolean
        Dim objChassis As ChassisMasterBBFacade = New ChassisMasterBBFacade(User)
        Return objChassis.ValidateCode(sCode) > 0
    End Function

    Private Function IsExistCode2(ByVal ChassisId As Integer) As Boolean
        Dim objEndCustomerFacade As EndCustomerFacade = New EndCustomerFacade(User)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EndCustomer), "ChassisMasterBB.ID", MatchType.Exact, ChassisId))
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

    Private Sub InsertChassisMasterBB(ByVal VehInformationSystem As ChassisMasterBB)
        Dim objChassisMasterBBFacade As ChassisMasterBBFacade = New ChassisMasterBBFacade(User)
        objChassisMasterBBFacade.Insert(VehInformationSystem)
    End Sub

    Private Sub UpdateChassisMasterBB(ByVal VehInformationSystem As ChassisMasterBB)
        Dim criterias As CriteriaComposite
        Dim objChassisMasterBB2 As ChassisMasterBB
        Dim objChassisMasterBBRet As ArrayList
        Dim objChassisMasterBBFacade As ChassisMasterBBFacade = New ChassisMasterBBFacade(User)
        criterias = New CriteriaComposite(New Criteria(GetType(ChassisMasterBB), "ChassisNumber", MatchType.Exact, VehInformationSystem.ChassisNumber))
        objChassisMasterBBRet = New ChassisMasterBBFacade(User).Retrieve(criterias)
        If objChassisMasterBBRet.Count > 0 Then
            objChassisMasterBB2 = objChassisMasterBBRet(0)
        End If
        VehInformationSystem.ID = objChassisMasterBB2.ID
        objChassisMasterBBFacade.Update(VehInformationSystem)

    End Sub

    Private Sub UpdateEndCustomer(ByVal ECustomer As EndCustomer)
        If Me._showForWSCSpecial Then
            'Dim criterias As CriteriaComposite
            'Dim objEndCustomer2 As EndCustomer
            'Dim objEndCustomerRet As ArrayList
            'Dim objEndCustumerFacade As EndCustomerFacade = New EndCustomerFacade(User)
            'criterias = New CriteriaComposite(New Criteria(GetType(EndCustomer), "ChassisMasterBB.ID", MatchType.Exact, ECustomer.ChassisMasterBB.ID))
            'objEndCustomerRet = New EndCustomerFacade(User).Retrieve(criterias)
            'If objEndCustomerRet.Count > 0 Then
            '    objEndCustomer2 = objEndCustomerRet(0)
            'End If
            'ECustomer.ID = objEndCustomer2.ID
            'objEndCustumerFacade.Update(ECustomer)
        End If
    End Sub

    Private Function searchSoldDealer() As Dealer
        Dim criteriasD As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriasD.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ID", MatchType.Exact, ObjChassisMasterBB.Dealer.ID))
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
        If Me._showForWSCSpecial Then
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.EndCustomer), "ChassisMasterBB.ID", MatchType.Exact, ObjChassisMasterBB.ID))
            'New Criteria(GetType(ChassisMasterBB), "EndCustomer", Me.ID)
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

            End If
        End If
    End Sub

    Private Sub searchMaterialNumber()
        Dim criterias3 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias3.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "ID", MatchType.Exact, ObjChassisMasterBB.VechileColor.ID))
        Dim VichileColl As ArrayList = New VechileColorFacade(User).Retrieve(criterias3)
        Dim objVichileColor As VechileColor
        If VichileColl.Count > 0 Then
            objVichileColor = CType(VichileColl(0), VechileColor)
            lblMaterial.Text = objVichileColor.MaterialNumber & " - " & objVichileColor.MaterialDescription
        End If
    End Sub

    Private Sub SearchPMData()
        If Me._showForWSCSpecial Then
            ObjChassisMasterBB = CType(sparepartColl(0), ChassisMasterBB)
            Dim PMList As ArrayList = New ArrayList
            If Not ObjChassisMasterBB Is Nothing Then
                PMList = ObjChassisMasterBB.PMHeaders
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

        End If

    End Sub

    Private Sub searchChassisMasterBB()
        ObjChassisMasterBB = CType(sparepartColl(0), ChassisMasterBB)
        If ObjChassisMasterBB.ChassisNumber <> "" Then
            lblNoChassis.Text = ObjChassisMasterBB.ChassisNumber
            If Not IsNothing(Request.Item("IsLeasing")) AndAlso Integer.Parse(Request.Item("IsLeasing")) = 1 Then
                Me.txtNoEngine.Text = ObjChassisMasterBB.EngineNumber
            End If
            lblNoEngine.Text = ObjChassisMasterBB.EngineNumber
            If ObjChassisMasterBB.DODate <= "1/1/1900" Then
                lblDOPrintDate.Text = ""
            Else
                lblDOPrintDate.Text = ObjChassisMasterBB.DODate.ToString("dd/MM/yyyy")
            End If
            lblNoSerial.Text = ObjChassisMasterBB.SerialNumber
            If ObjChassisMasterBB.GIDate <= "1/1/1900" Then
                lblStockOutDate.Text = ""
            Else
                lblStockOutDate.Text = ObjChassisMasterBB.GIDate.ToString("dd/MM/yyyy")
            End If
            If Me._showForWSCSpecial Then
                Dim criterias3 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias3.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "ChassisMasterBB.ID", MatchType.Exact, ObjChassisMasterBB.ID))
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
            End If
            searchSoldDealer()
        Else
            lblNoChassis.Text = ""
        End If
        searchEndCustomer()
        searchMaterialNumber()
        SearchPMData()
    End Sub
    Public Sub CheckAuthorityForDisplay()
        If Me._showForWSCSpecial = False Then Exit Sub
        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
        If Not IsNothing(objDealer) Then
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If lblDealerSold.Text <> String.Empty And lblDealerSold.Text.Length >= 6 Then
                    Dim _DealerCode As String = Left(lblDealerSold.Text, 6)
                    If objDealer.DealerCode <> _DealerCode AndAlso lblFakturOpenDate.Text <> "" Then
                        lblMaterial.Visible = True
                        lblNoChassis.Visible = True
                        lblNoEngine.Visible = True
                        lblNoSerial.Visible = True
                        lblFakturOpenDate.Visible = True
                        lblDealerSold.Visible = False
                        lblDealerPDI.Visible = False
                        lblDOPrintDate.Visible = False
                        lblStockOutDate.Visible = False
                        lblTglPDI.Visible = False
                        lblPDIIndicator.Visible = False
                        BtnDeletePDI.Visible = False
                    ElseIf objDealer.DealerCode <> _DealerCode AndAlso lblFakturOpenDate.Text = "" Then
                        BtnDeletePDI.Visible = False
                        lblMaterial.Visible = False
                        lblNoChassis.Visible = False
                        lblNoEngine.Visible = False
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
                        dgPMStatus.DataSource = Nothing
                        dgPMStatus.DataBind()
                        MessageBox.Show("Chasis Tidak Dapat Dilihat")
                    Else
                        lblMaterial.Visible = True
                        lblNoChassis.Visible = True
                        lblNoEngine.Visible = True
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
                lblNoEngine.Visible = True
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
    End Sub

    Public Sub searchVehicleInformation()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "ChassisNumber", MatchType.Exact, Me.txtChassisNumber.Text))
        sparepartColl = New ChassisMasterBBFacade(User).Retrieve(criterias)
        sHVeh.SetSession("SPCOLL", sparepartColl)
        Dim ObjChassisMasterBB As ChassisMasterBB
        If sparepartColl.Count > 0 Then 'o2n
            searchChassisMasterBB()
            dtgServiceData.Visible = True
            dgPMStatus.Visible = True
            bindFreeServiceBB()
            SearchPMData()
            CheckAuthorityForDisplay()
        Else
            dtgServiceData.Visible = False
            dgPMStatus.Visible = False
            MessageBox.Show(SR.DataNotFound("Data"))
        End If
        'Start WSC Special
        dgPMStatus.DataSource = New ArrayList
        dgPMStatus.DataBind()

        'End WSC Special
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitialPage()
        End If
    End Sub

    Private Sub InitialPage()
        txtChassisNumber.Attributes.Add("onkeydown", "enter(document.all.btnSearch)")
        ViewState("currentSortColumn") = "FSKind.KindCode"
        ViewState("currentSortDirection") = Sort.SortDirection.ASC
        viewstate("CurrentSortColumnX") = "ID"
        viewstate("CurrentSortDirectX") = Sort.SortDirection.ASC
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
            Dim RowValue As ChassisMasterBB = CType(e.Item.DataItem, ChassisMasterBB)

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
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        SearchData()

        Dim _sesshelper As New SessionHelper
        Dim oDealer As Dealer = CType(_sesshelper.GetSession("DEALER"), Dealer)
        If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            lbtnDeletePDI.Visible = False
        Else
            If lblTglPDI.Text = "" Then
                lbtnDeletePDI.Visible = False
            Else
                lbtnDeletePDI.Visible = True
            End If
        End If


    End Sub

    Private Sub dtgServiceData_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgServiceData.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgServiceData.CurrentPageIndex * dtgServiceData.PageSize)
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As FreeServiceBB = CType(e.Item.DataItem, FreeServiceBB)
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
        If CType(viewstate("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(viewstate("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    viewstate("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    viewstate("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("currentSortColumn") = e.SortExpression
            viewstate("currentSortDirection") = Sort.SortDirection.ASC
        End If

        dtgServiceData.SelectedIndex = -1
        dtgServiceData.CurrentPageIndex = 0
        bindGridSorting(dtgServiceData.CurrentPageIndex)
    End Sub
    Private Sub dgPMStatus_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPMStatus.SortCommand
        If CType(viewstate("CurrentSortColumnX"), String) = e.SortExpression Then
            Select Case CType(viewstate("CurrentSortDirectX"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    viewstate("CurrentSortDirectX") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    viewstate("CurrentSortDirectX") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("CurrentSortColumnX") = e.SortExpression
            viewstate("CurrentSortDirectX") = Sort.SortDirection.ASC
        End If
        dgPMStatus.SelectedIndex = -1
        dgPMStatus.CurrentPageIndex = 0
        SearchPMDataShort(dgPMStatus.CurrentPageIndex)
    End Sub
    Private Sub SearchPMDataShort(ByVal currPageindex As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ChassisMasterBB.ChassisNumber", MatchType.Exact, txtChassisNumber.Text))
        If (currPageindex >= 0) Then
            dgPMStatus.DataSource = New PMHeaderFacade(User).RetrieveActiveList(criterias, currPageindex + 1, dgPMStatus.PageSize, totalRow, CType(ViewState("CurrentSortColumnX"), String), CType(ViewState("CurrentSortDirectX"), Sort.SortDirection))
            dgPMStatus.VirtualItemCount = totalRow
            dgPMStatus.DataBind()
        End If
    End Sub
    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dtgServiceData.DataSource = New FreeServiceBBFacade(User).RetrieveActiveList(CType(sHVeh.GetSession("infoSort"), CriteriaComposite), indexPage + 1, dtgServiceData.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
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
            Dim objFreeServiceBB As FreeServiceBB = New FreeServiceBBFacade(User).Retrieve(id)
            If Not objFreeServiceBB Is Nothing Then
                If objFreeServiceBB.ID > 0 Then
                    objFreeServiceBB.RowStatus = DBRowStatus.Deleted
                    Dim objFSFacade As FreeServiceBBFacade = New FreeServiceBBFacade(User)
                    Dim i As Integer = objFSFacade.Update(objFreeServiceBB)
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
            ElseIf RowValue.PMStatus = EnumPMStatus.PMStatus.Selesai Then
                lblStatus.Text = "<img src=""../images/green.gif"" border=""0"" alt=""Selesai"">"
            End If
        Else
            lblStatus.Text = ""
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
            If Me._showForWSCSpecial Then
                FooterTypeDataBound(e)
            End If
        End If

    End Sub
    Private Sub dgPMStatus_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPMStatus.ItemDataBound
        If Me._showForWSCSpecial Then
            BindItemdgChassisNumber(e)
        End If
    End Sub
    Private Sub dgPMStatus_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPMStatus.ItemCommand
        If e.CommandName = "DeletePM" Then
            Dim lblID As Label = CType(e.Item.FindControl("lblPMID"), Label)
            Dim id As Integer = lblID.Text
            Dim objPM As PMHeader = New PMHeaderFacade(User).Retrieve(id)
            If Not objPM Is Nothing Then
                If objPM.ID > 0 Then

                    If objPM.PMStatus = EnumPMStatus.PMStatus.Selesai Then
                        MessageBox.Show("Data Tidak Dapat Dihapus")
                        Exit Sub
                    End If

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
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "ChassisNumber", MatchType.Exact, Me.txtChassisNumber.Text))
        sparepartColl = New ChassisMasterBBFacade(User).Retrieve(criterias)

        Dim oCMFac As ChassisMasterBBFacade = New ChassisMasterBBFacade(User)
        Dim cCM As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim aCM As New ArrayList

        cCM.opAnd(New Criteria(GetType(ChassisMasterBB), "EngineNumber", MatchType.Exact, txtNoEngine.Text.Trim))
        aCM = oCMFac.Retrieve(cCM)
        If aCM.Count > 0 Then
            Me.txtChassisNumber.Text = CType(aCM(0), ChassisMasterBB).ChassisNumber
            btnSearch_Click(sender, e)
        End If
        'End    :by:dna;for:doniset;on:20100623;remark:For Leasing Purpose Only
    End Sub
End Class