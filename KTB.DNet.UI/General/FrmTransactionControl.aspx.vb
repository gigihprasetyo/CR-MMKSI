Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports System.Drawing.Color
Imports System
Imports System.Text
Imports KTB.DNet.Security


Public Class FrmTransactionControl
    Inherits System.Web.UI.Page
    Private arlDummy As ArrayList = New ArrayList
    Private m_bChangePK_TrC_Privilege As Boolean = False
    Private m_bChangePO_TrC_Privilege As Boolean = False
    Private m_bChangePO_SP_TrC_Privilege As Boolean = False
    Private m_ubah_daftar_training_transcontrol_Privilege As Boolean = False
    Private m_ubah_credit_control_transcontrol_Privilege As Boolean = False
    Private m_ubah_vehicle_history_transcontrol_Privilege As Boolean = False
    Private m_ubah_factoring_transcontrol_privilege As Boolean = False
    Private m_ubah_input_gyro_transcontrol_privilege As Boolean = False
    Private m_ubah_redemption_transcontrol_privilege As Boolean = False


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgDealerList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnActivate As System.Web.UI.WebControls.Button
    Protected WithEvents ddlTransactionType As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents btnNoActivate As System.Web.UI.WebControls.Button
    Private _sessHelper As SessionHelper = New SessionHelper
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlstatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub
#End Region

    Private objDealer As Dealer
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            BindDdlStatus()
            BindDdlTransType()
            InitiatePage()
        End If
    End Sub

    Private Sub SetControlPrivilege()
        If m_bChangePK_TrC_Privilege Or m_bChangePO_TrC_Privilege Then
            btnActivate.Visible = True
            btnNoActivate.Visible = True
        Else
            btnActivate.Visible = False
            btnNoActivate.Visible = False
        End If
    End Sub

    Private Sub ActivateUserPrivilege()

        'ChangePK_TrC_Privilege
        m_bChangePK_TrC_Privilege = SecurityProvider.Authorize(Context.User, SR.ChangePK_TransControlList_Privilege)

        'ChangePO_TrC_Privilege
        m_bChangePO_TrC_Privilege = SecurityProvider.Authorize(Context.User, SR.ChangePO_TransControlList_Privilege)

        'UpdateSparepartPOTransactionControl_Privilege
        m_bChangePO_SP_TrC_Privilege = SecurityProvider.Authorize(Context.User, SR.UpdateSparepartPOTransactionControl_Privilege)

        'View_TrC_Privilege
        If Not SecurityProvider.Authorize(Context.User, SR.ViewTransControlList_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=GENERAL - Transaction Control")
        End If
        'Daftar Training
        m_ubah_daftar_training_transcontrol_Privilege = SecurityProvider.Authorize(Context.User, SR.TrainingTransactionControlListEdit_Privilege)
        'Credit Control
        m_ubah_credit_control_transcontrol_Privilege = SecurityProvider.Authorize(Context.User, SR.CreditControlTransactionControlEdit_Privilege)
        'Vehicle History
        m_ubah_vehicle_history_transcontrol_Privilege = SecurityProvider.Authorize(Context.User, SR.VehicleHistoryTransactionControlEdit_Privilege)
        m_ubah_factoring_transcontrol_privilege = SecurityProvider.Authorize(Context.User, SR.ubah_factoring_transcontrol_privilege)
        m_ubah_input_gyro_transcontrol_privilege = SecurityProvider.Authorize(Context.User, SR.ubah_input_gyro_transcontrol_privilege)
        m_ubah_redemption_transcontrol_privilege = SecurityProvider.Authorize(Context.User, SR.ubah_redemption_transcontrol_privilege)
    End Sub

    Private Sub InitiatePage()
        SetControlPrivilege()
        AssignAttributeControl()
        ViewState("CurrentSortColumn") = "DealerCode"
        ViewState("CurrentSortDirect") = "ASC" 'Sort.SortDirection.ASC
    End Sub

    Private Sub AssignAttributeControl()
        Dim lblPopUp As Label = CType(Page.FindControl("lblPopUpDealer"), Label)
        lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpDealerSelection.aspx", "", 500, 760, "DealerSelection")
    End Sub

    Private Function ConvertKodeDealer(ByVal sKodeDealerColl As String)
        Dim sKodeDealerTemp() As String = sKodeDealerColl.Split(New Char() {";"})
        Dim sKodeDealer As String = ""
        For i As Integer = 0 To sKodeDealerTemp.Length - 1
            sKodeDealer = sKodeDealer & "'" & sKodeDealerTemp(i).Trim() & "'"

            If Not (i = sKodeDealerTemp.Length - 1) Then
                sKodeDealer = sKodeDealer & ","
            End If
        Next
        sKodeDealer = "(" & sKodeDealer & ")"
        Return sKodeDealer
    End Function

    Private Sub CreateCriteriaSearch()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "Status", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If
        If Not (txtKodeDealer.Text.Trim() = "") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.InSet, ConvertKodeDealer(txtKodeDealer.Text.Trim())))
        End If
        _sessHelper.SetSession("sessCriteria", criterias)
    End Sub

    Private Function BindDatagrid(ByVal Criterias As CriteriaComposite) As ArrayList
        Dim strSortDirection As String = CType(ViewState("CurrentSortDirect"), String)
        Dim objSortDirection As Sort.SortDirection = Sort.SortDirection.ASC

        If strSortDirection = "ASC" Then
            objSortDirection = Sort.SortDirection.ASC
        ElseIf strSortDirection = "DESC" Then
            objSortDirection = Sort.SortDirection.DESC
        End If

        Return New DealerFacade(User).RetrieveActiveList(Criterias, CType(ViewState("CurrentSortColumn"), String), objSortDirection)
    End Function

    Private Sub BindDdlStatus()
        Dim listStatus As New EnumDealerStatus
        Dim al As ArrayList = listStatus.RetrieveStatus
        For Each item As EnumDealer In al
            ddlstatus.Items.Insert(0, New ListItem(item.NameStatus, item.ValStatus))
        Next
        ddlstatus.Items.Insert(0, New ListItem("Pilih Status", ""))
    End Sub

    Private Function CheckNotAktiveTrC(ByVal arrDealerAll As ArrayList) As ArrayList
        '=== Jika record di tabel TC kosong maka dianggap sebagai 
        '=== Dealer dg. transaksi kontrol yang tidak aktif
        Dim listStatus As New EnumDealerStatus
        Dim ArrDealerTmp As ArrayList = New ArrayList
        If Not IsNothing(arrDealerAll) Then
            For Each InstObjDealer As Dealer In arrDealerAll
                Dim crtTC As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TransactionControl), "Dealer.ID", MatchType.Exact, InstObjDealer.ID))
                crtTC.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransactionControl), "Kind", MatchType.Exact, ddlTransactionType.SelectedValue))
                crtTC.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransactionControl), "Status", MatchType.Exact, CType(listStatus.DealerStatus.Aktive, Short)))
                Dim TestObjTransControl As TransactionControl = New DealerFacade(User).RetrieveTransactionControlByCriteria(crtTC)
                If ddlstatus.SelectedValue = CType(listStatus.DealerStatus.Aktive, String) Then
                    If Not IsNothing(TestObjTransControl) Then
                        InstObjDealer.TransactionControlStatus = "Aktif"
                        ArrDealerTmp.Add(InstObjDealer)
                    End If
                ElseIf ddlstatus.SelectedValue = CType(listStatus.DealerStatus.NonAktive, String) Then
                    If IsNothing(TestObjTransControl) Then
                        InstObjDealer.TransactionControlStatus = "Tidak Aktif"
                        ArrDealerTmp.Add(InstObjDealer)
                    End If
                Else
                    If Not IsNothing(TestObjTransControl) Then
                        InstObjDealer.TransactionControlStatus = "Aktif"
                    Else
                        InstObjDealer.TransactionControlStatus = "Tidak Aktif"
                    End If
                    ArrDealerTmp.Add(InstObjDealer)
                End If
            Next
            Return ArrDealerTmp
        End If
    End Function

     Private Function CheckAktiveTrC(ByVal arrDealerAll As ArrayList) As ArrayList

        '=== Jika record di tabel TC kosong maka dianggap sebagai 
        '=== Dealer dg. transaksi kontrol yang aktif

        Dim listStatus As New EnumDealerStatus
        Dim ArrDealerTmp As ArrayList = New ArrayList
        If Not IsNothing(arrDealerAll) Then
            For Each InstObjDealer As Dealer In arrDealerAll
                Dim crtTC As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TransactionControl), "Dealer.ID", MatchType.Exact, InstObjDealer.ID))
                crtTC.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransactionControl), "Kind", MatchType.Exact, ddlTransactionType.SelectedValue))
                Dim TestObjTransControl As TransactionControl = New DealerFacade(User).RetrieveTransactionControlByCriteria(crtTC)
                If ddlstatus.SelectedValue = CType(listStatus.DealerStatus.Aktive, String) Then
                    'pilihannya status = aktif saja
                    If Not IsNothing(TestObjTransControl) Then
                        If TestObjTransControl.Status = CType(listStatus.DealerStatus.Aktive, Short) Then
                            InstObjDealer.TransactionControlStatus = "Aktif"
                            InstObjDealer.TrCLastUpdate = TestObjTransControl.LastUpdateTime
                            InstObjDealer.TrCLastUpdateBy = TestObjTransControl.LastUpdateBy
                            ArrDealerTmp.Add(InstObjDealer)
                        End If
                    End If
                ElseIf ddlstatus.SelectedValue = CType(listStatus.DealerStatus.NonAktive, String) Then
                    'pilihannya status = tidak aktif saja
                    If Not IsNothing(TestObjTransControl) Then
                        If TestObjTransControl.Status = CType(listStatus.DealerStatus.NonAktive, Short) Then
                            InstObjDealer.TransactionControlStatus = "Tidak Aktif"
                            InstObjDealer.TrCLastUpdate = TestObjTransControl.LastUpdateTime
                            InstObjDealer.TrCLastUpdateBy = TestObjTransControl.LastUpdateBy
                            ArrDealerTmp.Add(InstObjDealer)
                        End If
                    End If
                Else 'pilihannya status = aktif atau tidak aktif
                    If Not IsNothing(TestObjTransControl) Then
                        If TestObjTransControl.Status = CType(listStatus.DealerStatus.Aktive, Short) Then
                            InstObjDealer.TransactionControlStatus = "Aktif"
                        Else
                            InstObjDealer.TransactionControlStatus = "Tidak Aktif"
                        End If
                        InstObjDealer.TrCLastUpdate = TestObjTransControl.LastUpdateTime
                        InstObjDealer.TrCLastUpdateBy = TestObjTransControl.LastUpdateBy
                    Else
                        InstObjDealer.TransactionControlStatus = "-"
                        If Not IsNothing(InstObjDealer.CreatedTime) Then
                            InstObjDealer.TrCLastUpdate = InstObjDealer.CreatedTime
                        End If
                    End If
                    ArrDealerTmp.Add(InstObjDealer)
                End If
            Next
            Return ArrDealerTmp
        End If
    End Function

    Private Sub SetEnableButton(ByVal ArrDealerTmp As ArrayList)
        Dim listStatus As New EnumDealerStatus
        If ArrDealerTmp.Count > 0 Then
            If ddlstatus.SelectedValue = CType(listStatus.DealerStatus.Aktive, String) Then
                btnActivate.Enabled = False
                btnNoActivate.Enabled = True
            ElseIf ddlstatus.SelectedValue = CType(listStatus.DealerStatus.NonAktive, String) Then
                btnActivate.Enabled = True
                btnNoActivate.Enabled = False
            Else
                Dim bFalseCheck As Boolean = False
                Dim bTrueCheck As Boolean = False
                Dim bAlternating As Boolean = False

                For Each objDealer As Dealer In ArrDealerTmp
                    If objDealer.TransactionControlStatus = "Aktif" Then
                        bTrueCheck = True
                    Else
                        bFalseCheck = True
                    End If

                    If bTrueCheck And bFalseCheck Then
                        bAlternating = True
                        Exit For
                    End If
                Next

                If bAlternating Then
                    btnActivate.Enabled = True
                    btnNoActivate.Enabled = True
                Else
                    If bTrueCheck Then
                        btnActivate.Enabled = False
                        btnNoActivate.Enabled = True
                    Else
                        btnActivate.Enabled = True
                        btnNoActivate.Enabled = False
                    End If
                End If
            End If
        Else 'ArrDealerTmp.Count <= 0
            btnActivate.Enabled = False
            btnNoActivate.Enabled = False
        End If

    End Sub

    Private Sub SortListControl(ByRef ListControl As ArrayList, ByVal SortColumn As String, _
            ByVal SortDirection As String)

        Dim IsAsc As Boolean = True
        If SortDirection = "ASC" Then
            IsAsc = True
        ElseIf SortDirection = "DESC" Then
            IsAsc = False
        End If

        If SortColumn = "City.CityName" Then
            Dim objCompareCityName As IComparer = New CompareCityName(IsAsc)
            ListControl.Sort(objCompareCityName)
        Else
            Dim objListComparer As IComparer = New ListComparer(IsAsc, SortColumn)
            ListControl.Sort(objListComparer)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        CreateCriteriaSearch()
        Dim totRow As Integer = 0
        dtgDealerList.CurrentPageIndex = 0
        Dim arrDealerAll As ArrayList = BindDatagrid(CType(_sessHelper.GetSession("sessCriteria"), CriteriaComposite))
        Dim ArrDealerTmp As ArrayList = New ArrayList
        ArrDealerTmp = CheckAktiveTrC(arrDealerAll)
        SetEnableButton(ArrDealerTmp)

        SortListControl(ArrDealerTmp, CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), String))
        _sessHelper.SetSession("sessDealerList", ArrDealerTmp)
        dtgDealerList.DataSource = ArrDealerTmp
        dtgDealerList.VirtualItemCount = ArrDealerTmp.Count
        dtgDealerList.DataBind()
    End Sub

    Private Sub dtgReason_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDealerList.SortCommand
        Dim totRow As Integer = 0
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), String) 'Sort.SortDirection)
                Case "ASC" 'Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = "DESC" 'Sort.SortDirection.DESC
                Case "DESC" 'Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = "ASC" 'Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = "ASC" 'Sort.SortDirection.ASC
        End If

        'Dim arrDealerAll As ArrayList = BindDatagrid(CType(_sessHelper.GetSession("sessCriteria"), CriteriaComposite))
        'Dim ArrDealerTmp As ArrayList = New ArrayList
        'ArrDealerTmp = CheckAktiveTrC(arrDealerAll)
        '_sessHelper.SetSession("sessDealerList", ArrDealerTmp)

        Dim ArrDealerTmp As ArrayList = CType(Session.Item("sessDealerList"), ArrayList)
        SortListControl(ArrDealerTmp, CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), String))

        dtgDealerList.SelectedIndex = -1
        dtgDealerList.CurrentPageIndex = 0

        dtgDealerList.DataSource = GetCurrentColl(ArrDealerTmp, dtgDealerList.CurrentPageIndex)
        dtgDealerList.DataBind()
    End Sub

    Private Function GetCurrentColl(ByVal DealerColl As ArrayList, ByVal CurrentPage As Integer) As ArrayList
        'SetEnableButton(DealerColl)
        dtgDealerList.VirtualItemCount = DealerColl.Count
        Dim ArlCurrentColl As ArrayList = New ArrayList
        Dim nStart As Integer = (dtgDealerList.PageSize * CurrentPage)
        Dim i As Integer
        For i = 0 To (dtgDealerList.VirtualItemCount - (dtgDealerList.PageSize * CurrentPage) - 1)
            ArlCurrentColl.Add(DealerColl(nStart + i))
        Next
        '_sessHelper.SetSession("sessDealerList", ArlCurrentColl)
        Return ArlCurrentColl
    End Function

    Private Sub dtgReason_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDealerList.PageIndexChanged
        Dim totRow As Integer = 0

        'Dim arrDealerAll As ArrayList = CType(_sessHelper.GetSession("sessDealerList"), ArrayList)
        'Dim ArrDealerTmp As ArrayList = New ArrayList
        'ArrDealerTmp = CheckAktiveTrC(arrDealerAll)

        Dim ArrDealerTmp As ArrayList = CType(Session.Item("sessDealerList"), ArrayList)

        dtgDealerList.SelectedIndex = -1
        dtgDealerList.CurrentPageIndex = e.NewPageIndex
        dtgDealerList.DataSource = GetCurrentColl(ArrDealerTmp, e.NewPageIndex)
        dtgDealerList.DataBind()
        '_sessHelper.SetSession("sessDealerList", ArrDealerTmp)
    End Sub

    Private Sub dtgDealerList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDealerList.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            BoundRowItems(e)
        End If
    End Sub

    Private Sub BoundRowItems(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objDealer As Dealer = CType(CType(dtgDealerList.DataSource, ArrayList)(e.Item.ItemIndex), Dealer)
        If Not IsNothing(objDealer) Then
            Dim _cb As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lbltglUpdate As Label = CType(e.Item.FindControl("lbltglUpdate"), Label)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblUbahOleh As Label = CType(e.Item.FindControl("lblUbahOleh"), Label)
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                CType(e.Item.FindControl("lblNo"), Label).Text = CType(e.Item.ItemIndex + 1 + (dtgDealerList.CurrentPageIndex * dtgDealerList.PageSize), String)
            End If
            If objDealer.TransactionControlStatus = "Aktif" Then
                lblStatus.Text = "Aktif"
                lblStatus.ForeColor = Black
            Else
                lblStatus.Text = objDealer.TransactionControlStatus
                lblStatus.ForeColor = Red
                e.Item.BackColor = Color.LightSalmon
            End If
            lbltglUpdate.Text = Format(objDealer.TrCLastUpdate, "dd/MM/yyyy")
            If objDealer.TrCLastUpdateBy <> Nothing Then
                lblUbahOleh.Text = UserInfo.Convert(objDealer.TrCLastUpdateBy)
            End If
            Dim lblHistoryStatus As Label = CType(e.Item.FindControl("lblHistoryStatus"), Label)
            lblHistoryStatus.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpChangeTransactionControl.aspx?id=" & e.Item.Cells(2).Text & "&TransType=" & ddlTransactionType.SelectedValue, "", 400, 400, "Dummy")
            
        End If
    End Sub

    Private Sub BindDdlTransType()
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim al As ArrayList = New EnumDealerTransType().RetrieveTransType(companyCode)
        For i As Integer = 0 To al.Count - 1
            ddlTransactionType.Items.Insert(i, New ListItem(al(i).NameTransType, al(i).ValTransType))
        Next
        ActivateUserPrivilege()
        If Not m_bChangePK_TrC_Privilege Then
            SetDdlTransTypePrivilege(CType(EnumDealerTransType.DealerTransKind.PKBulanan, String))
            SetDdlTransTypePrivilege(CType(EnumDealerTransType.DealerTransKind.PKTambahan, String))
        End If
        If Not m_bChangePO_TrC_Privilege Then
            SetDdlTransTypePrivilege(CType(EnumDealerTransType.DealerTransKind.POBulanan, String))
            SetDdlTransTypePrivilege(CType(EnumDealerTransType.DealerTransKind.POTambahan, String))
        End If

        If Not m_bChangePO_SP_TrC_Privilege Then
            SetDdlTransTypePrivilege(CType(EnumDealerTransType.DealerTransKind.POSparePart, String))
        End If

        If Not m_ubah_credit_control_transcontrol_Privilege Then
            SetDdlTransTypePrivilege(CType(EnumDealerTransType.DealerTransKind.CreditControl, String))
        End If
        If Not m_ubah_credit_control_transcontrol_Privilege Then
            SetDdlTransTypePrivilege(CType(EnumDealerTransType.DealerTransKind.FilterPengajuanPO, String))
            SetDdlTransTypePrivilege(CType(EnumDealerTransType.DealerTransKind.FilterPengajuanPOMMC, String))
            SetDdlTransTypePrivilege(CType(EnumDealerTransType.DealerTransKind.FilterAlokasi, String))
            SetDdlTransTypePrivilege(CType(EnumDealerTransType.DealerTransKind.FilterAlokasiMMC, String))
        End If
        If Not m_ubah_daftar_training_transcontrol_Privilege Then
            SetDdlTransTypePrivilege(CType(EnumDealerTransType.DealerTransKind.DaftarTraining, String))
        End If
        If Not m_ubah_vehicle_history_transcontrol_Privilege Then
            SetDdlTransTypePrivilege(CType(EnumDealerTransType.DealerTransKind.DataKendaraanLama, String))
        End If
        If Not m_ubah_factoring_transcontrol_privilege Then
            SetDdlTransTypePrivilege(CType(EnumDealerTransType.DealerTransKind.Factoring, String))
            SetDdlTransTypePrivilege(CType(EnumDealerTransType.DealerTransKind.FactoringMMC, String))
        End If
        If Not m_ubah_input_gyro_transcontrol_privilege Then
            SetDdlTransTypePrivilege(CType(EnumDealerTransType.DealerTransKind.InputGyro, String))
        End If
        If Not m_ubah_redemption_transcontrol_privilege Then
            SetDdlTransTypePrivilege(CType(EnumDealerTransType.DealerTransKind.RedemptionPlan, String))
        End If

        If ddlTransactionType.Items.Count = 0 Then
            btnSearch.Enabled = False
            ddlTransactionType.Enabled = False
        Else
            btnSearch.Enabled = True
            ddlTransactionType.Enabled = True
        End If
    End Sub


    Private Sub SetDdlTransTypePrivilege(ByVal TransTypeValue As String)
        Dim trap As Integer = -1
        For i As Integer = 0 To ddlTransactionType.Items.Count - 1
            If ddlTransactionType.Items(i).Value = TransTypeValue Then
                trap = i
            End If
        Next
        If trap <> -1 Then
            ddlTransactionType.Items.RemoveAt(trap)
        End If
    End Sub

    Private Function GetCheckedDealerItem(ByVal nStatus As Short) As ArrayList
        'dtgDealerList.DataSource = CType(Session("sessDealerList"), ArrayList)
        Dim ArrDealerTmp As ArrayList = CType(Session.Item("sessDealerList"), ArrayList)

        Dim arlCheckedDealerItem As ArrayList = New ArrayList
        Dim nIndeks As Integer
        For Each dtgItem As DataGridItem In dtgDealerList.Items
            nIndeks = dtgItem.ItemIndex + (dtgDealerList.CurrentPageIndex * dtgDealerList.PageSize)
            'Dim objDealer As Dealer = CType(CType(dtgDealerList.DataSource, ArrayList)(nIndeks), Dealer)
            Dim objDealer As Dealer = CType(ArrDealerTmp(nIndeks), Dealer)
            If CType(dtgItem.Cells(0).FindControl("chkItemChecked"), CheckBox).Checked Then
                Dim objTransControl As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(objDealer.ID, ddlTransactionType.SelectedValue)
                If Not IsNothing(objTransControl) Then
                    'objTransControl.RowStatus = 1 
                    If objTransControl.Status = nStatus Then
                        objTransControl.UpdateTransHistory = False
                    End If
                    objTransControl.Status = nStatus
                    'objTransControl.LastUpdateTime = Today.Date
                    'objTransControl.LastUpdateBy = User.Identity.Name

                    arlCheckedDealerItem.Add(objTransControl)
                Else
                    objTransControl = New TransactionControl
                    objTransControl.Kind = ddlTransactionType.SelectedValue
                    objTransControl.Dealer = objDealer
                    objTransControl.DateControl = 25
                    objTransControl.Status = nStatus
                    If nStatus = EnumDealerStatus.DealerStatus.Aktive Then
                        objTransControl.UpdateTransHistory = False
                    End If
                    objTransControl.RowStatus = 0
                    'objTransControl.CreatedTime = Today.Date
                    'objTransControl.CreatedBy = User.Identity.Name
                    objTransControl.LastUpdateBy = User.Identity.Name
                    arlCheckedDealerItem.Add(objTransControl)
                End If
            End If
        Next
        Return arlCheckedDealerItem
    End Function

    Private Sub btnNoActivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNoActivate.Click
        Dim bcheck As Boolean = False
        'dtgDealerList.DataSource = CType(Session("sessDealerList"), ArrayList)
        For Each dtgItem As DataGridItem In dtgDealerList.Items
            If CType(dtgItem.Cells(0).FindControl("chkItemChecked"), CheckBox).Checked Then
                bcheck = True
                Exit For
            End If
        Next
        If bcheck Then
            Dim ArrDealerTmp As ArrayList = GetCheckedDealerItem(CType(EnumDealerStatus.DealerStatus.NonAktive, Short))
            Dim nTransResult = New DealerFacade(User).UpdateTransactionControl(ArrDealerTmp)
            If nTransResult = 0 Then
                MessageBox.Show("Update Non Aktivasi Sukses")
                _sessHelper.SetSession("sessDealerList", ArrDealerTmp)
            Else
                MessageBox.Show("Update Non Aktivasi gagal")
            End If
            btnSearch_Click(sender, e)
        Else
            MessageBox.Show("Dealer belum dipilih !")
        End If
    End Sub

    Private Sub btnActivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivate.Click
        Dim bcheck As Boolean = False
        'dtgDealerList.DataSource = CType(Session("sessDealerList"), ArrayList)
        For Each dtgItem As DataGridItem In dtgDealerList.Items
            If CType(dtgItem.Cells(0).FindControl("chkItemChecked"), CheckBox).Checked Then
                bcheck = True
                Exit For
            End If
        Next
        If bcheck Then
            Dim ArrDealerTmp As ArrayList = GetCheckedDealerItem(CType(EnumDealerStatus.DealerStatus.Aktive, Short))
            Dim nTransResult = New DealerFacade(User).UpdateTransactionControl(ArrDealerTmp)
            If nTransResult = 0 Then
                MessageBox.Show("Update Aktivasi Sukses")
                _sessHelper.SetSession("sessDealerList", ArrDealerTmp)
            Else
                MessageBox.Show("Update Aktivasi gagal")
            End If
            btnSearch_Click(sender, e)
        Else
            MessageBox.Show("Dealer belum dipilih !")
        End If
    End Sub
End Class





