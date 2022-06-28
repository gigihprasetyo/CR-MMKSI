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

Public Class PopUpInfoKendaraanBB
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents lblMaterial As System.Web.UI.WebControls.Label
    'Protected WithEvents lblNoSerial As System.Web.UI.WebControls.Label
    'Protected WithEvents lblNoChassis As System.Web.UI.WebControls.Label
    'Protected WithEvents lblFakturOpenDate As System.Web.UI.WebControls.Label
    'Protected WithEvents lblNoEngine As System.Web.UI.WebControls.Label
    'Protected WithEvents lblDOPrintDate As System.Web.UI.WebControls.Label
    'Protected WithEvents lblDealerSold As System.Web.UI.WebControls.Label
    'Protected WithEvents lblStockOutDate As System.Web.UI.WebControls.Label
    'Protected WithEvents lblDealerPDI As System.Web.UI.WebControls.Label
    'Protected WithEvents lblTglPDI As System.Web.UI.WebControls.Label
    'Protected WithEvents lblPDIIndicator As System.Web.UI.WebControls.Label
    'Protected WithEvents dtgServiceData As System.Web.UI.WebControls.DataGrid
    'Protected WithEvents dgPMStatus As System.Web.UI.WebControls.DataGrid
    'Protected WithEvents lblChassisNumber As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Dim arlist, arlist2 As ArrayList
    Dim sparepartColl, arChassisMaster As ArrayList
    Dim ObjChassisMaster As ChassisMasterBB
    Dim objEndCustomer As EndCustomer
    Dim sHVeh As SessionHelper = New SessionHelper
    Dim sHECus As SessionHelper = New SessionHelper
    Dim isDeleteAuth As Boolean = False

#End Region

#Region "Custom Method"

    Private Sub bindFreeService()
        Dim criterias2 As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "ChassisNumber", MatchType.Exact, Me.lblChassisNumber.Text))
        arChassisMaster = New ChassisMasterBBFacade(User).Retrieve(criterias)
        Dim ObjChassisMaster As ChassisMasterBB
        For Each ObjChassisMaster In arChassisMaster
            criterias2 = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "ChassisMasterBB.ID", MatchType.Exact, ObjChassisMaster.ID))

        Next
        sHVeh.SetSession("infoSort", criterias2)
        'dtgServiceData.DataSource() = New FreeServiceFacade(User).Retrieve(criterias2)
        Dim indexPage As Integer = 0
        Dim totalRow As Integer = 0
        dtgServiceData.DataSource() = New FreeServiceBBFacade(User).RetrieveActiveList(CType(sHVeh.GetSession("infoSort"), CriteriaComposite), indexPage + 1, dtgServiceData.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
        dtgServiceData.DataBind()
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
    'Private Sub searchEndCustomer()
    '    Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.EndCustomer), "ChassisMaster.ID", MatchType.Exact, ObjChassisMaster.ID))
    '    'New Criteria(GetType(ChassisMasterBB), "EndCustomer", Me.ID)
    '    Dim EndCustomerColl As ArrayList = New EndCustomerFacade(User).Retrieve(criterias2)

    '    If EndCustomerColl.Count > 0 Then
    '        objEndCustomer = CType(EndCustomerColl(0), EndCustomer)

    '        If Not IsNothing(objEndCustomer.OpenFakturDate) Then
    '            If objEndCustomer.OpenFakturDate <= "1/1/1900" Then
    '                lblFakturOpenDate.Text = ""
    '            Else
    '                lblFakturOpenDate.Text = objEndCustomer.OpenFakturDate.ToString("dd/MM/yyyy")
    '            End If
    '        End If

    '    End If
    'End Sub
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
        ObjChassisMaster = CType(sparepartColl(0), ChassisMasterBB)
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
        ObjChassisMaster = CType(sparepartColl(0), ChassisMasterBB)
        If ObjChassisMaster.ChassisNumber <> "" Then
            lblNoChassis.Text = ObjChassisMaster.ChassisNumber
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
        Else
            lblNoChassis.Text = ""
        End If
        'searchEndCustomer()
        searchMaterialNumber()
        'SearchPMData()
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
                        lblNoEngine.Visible = True
                        lblNoSerial.Visible = True
                        lblFakturOpenDate.Visible = True
                        lblDealerSold.Visible = False
                        lblDealerPDI.Visible = False
                        lblDOPrintDate.Visible = False
                        lblStockOutDate.Visible = False
                        lblTglPDI.Visible = False
                        lblPDIIndicator.Visible = False
                    ElseIf objDealer.DealerCode <> _DealerCode AndAlso lblFakturOpenDate.Text = "" Then
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
                        lblChassisNumber.Text = ""
                        MessageBox.Show("Chasis tidak ditemukan.")
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

            End If
        End If

    End Sub
    Public Sub searchVehicleInformation()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "ChassisNumber", MatchType.Exact, Me.lblChassisNumber.Text))
        sparepartColl = New ChassisMasterBBFacade(User).Retrieve(criterias)
        sHVeh.SetSession("SPCOLL", sparepartColl)
        Dim ObjChassisMaster As ChassisMasterBB
        If sparepartColl.Count > 0 Then
            searchChassisMaster()
            dtgServiceData.Visible = True
            dgPMStatus.Visible = False
            bindFreeService()
            'SearchPMData()
            'CheckAuthorityForDisplay()
        Else
            dtgServiceData.Visible = False
            dgPMStatus.Visible = False
            MessageBox.Show(SR.DataNotFound("Data"))
        End If
    End Sub
    Private Sub SearchPMDataShort(ByVal currPageindex As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ChassisMasterBB.ChassisNumber", MatchType.Exact, lblChassisNumber.Text))
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
    Private Sub ItemTypeDataBound(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim RowValue As PMHeader = CType(e.Item.DataItem, PMHeader)

        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblStatus As Label = CType(e.Item.FindControl("lblStatusPM"), Label)
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
            FooterTypeDataBound(e)
        End If

    End Sub

    ' Not used Method in this page
    Private Sub ClearLabel()
        lblDealerPDI.Text = ""
        lblDealerSold.Text = ""
        lblDOPrintDate.Text = ""
        lblFakturOpenDate.Text = ""
        lblMaterial.Text = ""
        lblNoChassis.Text = ""
        lblNoEngine.Text = ""
        lblNoSerial.Text = ""
        lblStockOutDate.Text = ""
        lblTglPDI.Text = ""
    End Sub
#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitialPage()
            lblChassisNumber.Text = Request.QueryString("cn")
            ClearLabel()
            searchVehicleInformation()
        End If
    End Sub

    Private Sub InitialPage()
        ViewState("currentSortColumn") = "FSKind.KindCode"
        ViewState("currentSortDirection") = Sort.SortDirection.ASC
        viewstate("CurrentSortColumnX") = "ID"
        viewstate("CurrentSortDirectX") = Sort.SortDirection.ASC
    End Sub
    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.InformasiKendaraanView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Finish Unit - Form Informasi Kendaraan")
        End If
        isDeleteAuth = SecurityProvider.Authorize(Context.User, SR.ENHDataServiceHapus_Privilege)
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

    Private Sub dgPMStatus_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPMStatus.ItemDataBound
        BindItemdgChassisNumber(e)
    End Sub
    Private Sub dgPMStatus_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPMStatus.PageIndexChanged
        dgPMStatus.CurrentPageIndex = e.NewPageIndex
        dgPMStatus.DataBind()

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

    'Not Used Event for this Page
    Private Sub dtgServiceData_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgServiceData.ItemCommand
        If e.CommandName = "Delete" Then
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim id As Integer = lblID.Text
            Dim objFreeService As FreeServiceBB = New FreeServiceBBFacade(User).Retrieve(id)
            If Not objFreeService Is Nothing Then
                If objFreeService.ID > 0 Then
                    objFreeService.RowStatus = DBRowStatus.Deleted
                    Dim objFSFacade As FreeServiceBBFacade = New FreeServiceBBFacade(User)
                    Dim i As Integer = objFSFacade.Update(objFreeService)
                    If i = -1 Then
                        MessageBox.Show(SR.DeleteFail)
                    Else
                        If lblChassisNumber.Text <> "" Then
                            ClearLabel()
                            searchVehicleInformation()
                        End If
                        MessageBox.Show(SR.DeleteSucces)
                    End If
                End If
            End If

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
                        If lblChassisNumber.Text <> "" Then
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
#End Region


End Class