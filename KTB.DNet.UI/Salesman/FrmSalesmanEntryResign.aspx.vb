#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

#End Region



Public Class FrmSalesmanEntryResign
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopupsubordinate As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents chkResign As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ddlSalesmanCode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents icResignDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlResignReason As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtResignReason As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSalesmanCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents dgSalesmanHeader As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents btnTriggerSalesman As System.Web.UI.WebControls.Button
    Protected WithEvents lblText As System.Web.UI.WebControls.Label
    Protected WithEvents lblsemicolon As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents lblPosition As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopUpSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents hdnSalesmanCode As System.Web.UI.WebControls.HiddenField
    Protected WithEvents hdnConfirm As System.Web.UI.WebControls.HiddenField
    Protected WithEvents trSubOrdinate As System.Web.UI.HtmlControls.HtmlTableRow

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    Private _SalesmanHeaderFacade As New SalesmanHeaderFacade(User)
    Private _viewPriv As Boolean
    Private _downloadPriv As Boolean
    Private sessHelper As New SessionHelper
    Private strDefDate As String = "1753/01/01"
    Private EnumResignReason_LainLain As Integer = 0

#End Region

#Region "PrivateCustomMethods"
    ' penambahan untuk initialize data
    Private Sub ClearData()
        ddlSalesmanCode.SelectedIndex = -1
        lblName.Text = String.Empty
        lblPosition.Text = String.Empty
        icResignDate.Value = Now
        ddlResignReason.SelectedIndex = -1
        txtResignReason.Visible = False
        txtResignReason.Text = String.Empty
        icResignDate.Enabled = False
        chkResign.Checked = False
        icResignDate.Value = Now
        txtSalesmanCode.Text = String.Empty
        txtSalesmanCode.ReadOnly = False
        lblPopUpSalesman.Visible = True
        trSubOrdinate.Visible = False
        'txtName.ReadOnly = False
        'txtPosition.ReadOnly = False
        btnSimpan.Enabled = True
        If dgSalesmanHeader.Items.Count > 0 Then
            dgSalesmanHeader.SelectedIndex = -1
        End If
        ViewState.Add("vsProcess", "Edit")
    End Sub
    ' untuk update data yg sdh ada sebelumnya
    Private Sub Update()
        If Not IsNothing(sessHelper.GetSession("vsSalesmanHeader")) Then
            Dim func As New SalesmanDSEFacade(Me.User)
            Dim objSalesmanHeader As SalesmanHeader = CType(sessHelper.GetSession("vsSalesmanHeader"), SalesmanHeader)
            objSalesmanHeader.ResignDate = icResignDate.Value
            objSalesmanHeader.ResignReasonType = ddlResignReason.SelectedValue

            If objSalesmanHeader.ResignReasonType = EnumResignReason_LainLain Then
                objSalesmanHeader.ResignReason = txtResignReason.Text
            Else
                Dim StandardCode = New StandardCodeFacade(User).GetByCategoryValue("EnumResignReason", objSalesmanHeader.ResignReasonType)
                If (StandardCode IsNot Nothing) Then
                    objSalesmanHeader.ResignReason = StandardCode.ValueDesc
                End If
            End If

            objSalesmanHeader.Status = CType(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif, String)

            Dim nResult = New SalesmanHeaderFacade(User).Update(objSalesmanHeader)
            If nResult < 0 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                Dim objDSE As SalesmanDSE = func.GetBySalesmanHeader(objSalesmanHeader.SalesmanCode)
                If objDSE.ID > 0 Then
                    objDSE.RowStatus = -1
                    func.NonAktif_Changes(objDSE)
                End If

                MessageBox.Show(SR.UpdateSucces)
                ViewState.Add("vsProcess", "Default")
            End If
        Else
            MessageBox.Show(SR.UpdateFail)
        End If
    End Sub
    Private Sub Initialize()
        ClearData()
        ViewState("CurrentSortColumn") = "SalesmanCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ViewState.Add("vsProcess", "Default")
    End Sub
    ' penambahan untuk delete data
    Private Sub Delete(ByVal nID As Integer)
        ' melakukan update, pembatalan resign
        Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(nID)
        If Not objSalesmanHeader Is Nothing Then
            objSalesmanHeader.ResignDate = Date.Parse(strDefDate)
            objSalesmanHeader.ResignReason = String.Empty
            Dim nResult = New SalesmanHeaderFacade(User).Update(objSalesmanHeader)
            If nResult < 0 Then
                MessageBox.Show("Status Pembatalan Resign Salesman gagal")
            Else
                MessageBox.Show("Status Pembatalan Resign Salesman telah dibatalkan")
            End If
        End If
        BindDataGrid(0)
    End Sub
    ' penambahan untuk view data
    Private Sub View(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(nID)
        sessHelper.SetSession("vsSalesmanHeader", objSalesmanHeader)
        ddlSalesmanCode.SelectedValue = objSalesmanHeader.ID
        txtSalesmanCode.Text = objSalesmanHeader.SalesmanCode
        hdnSalesmanCode.Value = objSalesmanHeader.SalesmanCode
        lblName.Text = objSalesmanHeader.Name
        lblPosition.Text = objSalesmanHeader.JobPosition.Code
        icResignDate.Value = objSalesmanHeader.ResignDate
        ddlResignReason.SelectedValue = objSalesmanHeader.ResignReasonType

        txtResignReason.Text = objSalesmanHeader.ResignReason
        txtResignReason.Visible = objSalesmanHeader.ResignReasonType = EnumResignReason_LainLain

        Me.btnSimpan.Enabled = EditStatus
    End Sub
    Private Sub SetPageTitle()
        If Not IsNothing(Request.QueryString("Mode")) Then
            Select Case Request.QueryString("Mode")
                Case "unit"
                    lblPageTitle.Text = "DATABASE SALESMAN - Pengunduran Diri"
                Case "part"
                    lblPageTitle.Text = "DATABASE SALESMAN PART - Pengunduran Diri"
                Case "servis"
                    lblPageTitle.Text = "DATABASE MECHANIC - Pengunduran Diri"
            End Select
        Else
            lblPageTitle.Text = "DATABASE SALESMAN - Pengunduran Diri ..."
        End If
    End Sub
    Private Sub EnableControl()
        If Not IsNothing(Request.QueryString("From")) Then
            btnSimpan.Visible = False
            btnBatal.Visible = False
        Else
            btnSimpan.Visible = True
            btnBatal.Visible = True
        End If
        Dim objDealer As Dealer = sessHelper.GetSession("DEALER")
        If objDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            btnBack.Visible = False
        Else
            ' dari KTB, lihat saja dari view data rekap turn over
            btnBack.Visible = True
        End If

        If Not IsNothing(sessHelper.GetSession("mode")) Then
            If sessHelper.GetSession("mode") = "View" Then
                ddlSalesmanCode.Enabled = False
                lblText.Visible = True
                lblsemicolon.Visible = True
                lblKodeDealer.Visible = True

                Dim crits As Hashtable
                crits = CType(sessHelper.GetSession("TO"), Hashtable)
                If Not IsNothing(crits) Then
                    lblKodeDealer.Text = CType(crits.Item("DealerCode"), String)
                End If

                'lblName.Enabled = False
                'lblPosition.Enabled = False
                txtResignReason.Enabled = False
                icResignDate.Enabled = False
                chkResign.Enabled = False
            End If
        Else
            lblText.Visible = False
            lblsemicolon.Visible = False
            lblKodeDealer.Visible = False
            ddlSalesmanCode.Enabled = True
            lblName.Enabled = True
            lblPosition.Enabled = True
            txtResignReason.Enabled = True
            icResignDate.Enabled = True
            chkResign.Enabled = True
        End If
    End Sub
    Private Function CheckValidation() As Boolean
        Dim blnValid As Boolean = True
        If dgSalesmanHeader.Items.Count <= 0 Then
            blnValid = False
            MessageBox.Show("Tidak ada data untuk diupdate dari kriteria yang bersangkutan")
            Return (blnValid)
        End If

        Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(hdnSalesmanCode.Value)

        If chkResign.Checked = False Then
            blnValid = False
            MessageBox.Show("Silahkan isi tanggal pengunduran diri yang bersangkutan")
            icResignDate.Enabled = False
            Return (blnValid)
        Else
            If icResignDate.Value = Date.Parse(strDefDate) Then
                blnValid = False
                MessageBox.Show("Silakan input tanggal resign yang valid")
                Return (blnValid)
            ElseIf icResignDate.Value < objSalesmanHeader.HireDate Then
                blnValid = False
                MessageBox.Show("Tanggal keluar tidak boleh lebih kecil dari tanggal masuk")
                Return (blnValid)
            End If
        End If

        If ddlResignReason.SelectedValue = -1 Then
            blnValid = False
            MessageBox.Show("Silahkan pilih alasan resign")
            Return (blnValid)
        End If

        If ddlResignReason.SelectedValue = EnumResignReason_LainLain AndAlso txtResignReason.Text.Trim = "" Then
            blnValid = False
            MessageBox.Show("Silahkan isi alasan resign")
            Return (blnValid)
        End If

        Return blnValid
    End Function
    Private Sub SettingDate(ByVal dtValue As Date)
        If dtValue <> Date.Parse(strDefDate) Then
            chkResign.Checked = True
            icResignDate.Enabled = True
        Else
            chkResign.Checked = False
            icResignDate.Enabled = False
        End If
    End Sub
    ' Untuk bind data yg bersangkutan - related
    Private Sub BindDropDownLists()
        Dim strDealer As String
        Dim objDealer As Dealer = sessHelper.GetSession("DEALER")
        If objDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            strDealer = objDealer.DealerCode
        Else
            ' untuk KTB bs akses semua data
            strDealer = ""
        End If

        If Not IsNothing(Request.QueryString("Mode")) Then
            Select Case Request.QueryString("Mode")
                Case "unit"
                    CommonFunction.BindSalesmanCode(ddlSalesmanCode, Me.User, True, False, CType(CType(EnumSalesmanUnit.SalesmanUnit.Unit, Byte), String), strDealer)
                Case "part"
                    CommonFunction.BindSalesmanCode(ddlSalesmanCode, Me.User, True, False, CType(CType(EnumSalesmanUnit.SalesmanUnit.Sparepart, Byte), String), strDealer)
                Case "servis"
                    CommonFunction.BindSalesmanCode(ddlSalesmanCode, Me.User, True, False, CType(CType(EnumSalesmanUnit.SalesmanUnit.Mekanik, Byte), String), strDealer)
                Case Else
                    CommonFunction.BindSalesmanCode(ddlSalesmanCode, Me.User, True, False, "")
            End Select
        End If
    End Sub
    Private Sub BindResignReason()
        Dim EnumResignReasons As ArrayList = New StandardCodeFacade(User).RetrieveByCategory("EnumResignReason")
        ddlResignReason.DataSource = EnumResignReasons
        ddlResignReason.DataValueField = "ValueId"
        ddlResignReason.DataTextField = "ValueDesc"
        ddlResignReason.DataBind()
        ddlResignReason.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub
    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "RegisterStatus", MatchType.Exact, CType(EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Sudah_Register, String)))

        If Not IsNothing(Request.QueryString("Mode")) Then
            Select Case Request.QueryString("Mode")
                Case "unit"
                    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, CType(EnumSalesmanUnit.SalesmanUnit.Unit, Byte)))
                Case "part"
                    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, CType(EnumSalesmanUnit.SalesmanUnit.Sparepart, Byte)))
                Case "servis"
                    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, CType(EnumSalesmanUnit.SalesmanUnit.Mekanik, Byte)))
            End Select
        End If

        Dim objDealer As Dealer = sessHelper.GetSession("DEALER")

        If objDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            ' ambil berdasarkan dealer yg login
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.Exact, objDealer.DealerCode))
        ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then

            'Bugs 100006
            Dim crits As Hashtable
            crits = CType(sessHelper.GetSession("TO"), Hashtable)
            If Not IsNothing(crits) Then

                Dim icStartDate, icEndDate As Date

                icStartDate = CType(crits.Item("CreatedTimeGreaterOrEqual"), Date)
                icEndDate = CType(crits.Item("CreatedTimeLesserOrEqual"), Date)

                criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.Exact, CType(crits.Item("DealerCode"), String)))
                criterias.opAnd(New Criteria(GetType(SalesmanHeader), "HireDate", MatchType.GreaterOrEqual, icStartDate))
                criterias.opAnd(New Criteria(GetType(SalesmanHeader), "HireDate", MatchType.LesserOrEqual, icEndDate))
                criterias.opAnd(New Criteria(GetType(SalesmanHeader), "ResignDate", MatchType.No, Date.Parse(strDefDate)))
            End If
        End If

        arrList = _SalesmanHeaderFacade.RetrieveByCriteria(criterias, idxPage + 1, dgSalesmanHeader.PageSize, totalRow, _
        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgSalesmanHeader.DataSource = arrList
        dgSalesmanHeader.VirtualItemCount = totalRow
        dgSalesmanHeader.DataBind()

    End Sub
#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        CheckPrivilege()
        _viewPriv = checkViewDetailPriv()
        _downloadPriv = checkDownloadPriv()
        If Not IsPostBack Then
            Initialize()
            BindDataGrid(0)
            BindDropDownLists()
            BindResignReason()
            SetPageTitle()
            EnableControl()
            lblPopUpSalesman.AddOnClick("ShowPopUpSalesmanbyDealer('');")
        End If
        If hdnConfirm.Value = "1" Then
            hdnConfirm.Value = ""
            Dim func As New SalesmanDSEFacade(Me.User)
            Dim objSalesmanHeader As SalesmanHeader = sessHelper.GetSession("salesResigne")
            objSalesmanHeader.ResignDate = icResignDate.Value
            objSalesmanHeader.ResignReason = txtResignReason.Text
            objSalesmanHeader.Status = CType(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif, String)

            Dim nResult = New SalesmanHeaderFacade(User).Update(objSalesmanHeader)
            If nResult < 0 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                Dim objDSE As SalesmanDSE = func.GetBySalesmanHeader(objSalesmanHeader.SalesmanCode)
                If objDSE.ID > 0 Then
                    objDSE.RowStatus = -1
                    func.NonAktif_Changes(objDSE)
                End If

                MessageBox.Show(SR.UpdateSucces)
                ViewState.Add("vsProcess", "Default")
            End If


            ClearData()
            dgSalesmanHeader.CurrentPageIndex = 0
            BindDataGrid(dgSalesmanHeader.CurrentPageIndex)
        End If

    End Sub
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeader
        Dim objSalesmanHeaderFacade As SalesmanHeaderFacade = New SalesmanHeaderFacade(User)
        Dim nResult As Integer = -1

        If CheckValidation() Then
            If CType(ViewState("vsProcess"), String) = "Edit" Then
                If Not hdnSalesmanCode.Value = String.Empty Then
                    If trSubOrdinate.Visible Then
                        Dim objSls As SalesmanHeader = CType(sessHelper.GetSession("vsSalesmanHeader"), SalesmanHeader)
                        sessHelper.SetSession("salesResigne", objSls)

                        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "LeaderId", MatchType.Exact, objSls.ID))
                        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.Exact, 2))
                        Dim arrSalesman As ArrayList = objSalesmanHeaderFacade.Retrieve(criterias)
                        'If arrSalesman.Count > 0 Then
                        '    MessageBox.Confirm("Masih terdapat sub ordinat yang aktif. Anda yakin ingin mendaftarkan pengunduran diri?", "hdnConfirm")
                        '    Return
                        'Else
                        '    MessageBox.Confirm("Anda yakin ingin mendaftarkan pengunduran diri?", "hdnConfirm")
                        '    Return
                        'End If
                        'CR Salesman
                        If arrSalesman.Count > 0 Then
                            MessageBox.Show("Pengunduran diri tidak dapat diproses, Mohon assign bawahan kepada atasan yang lain")
                            Exit Sub
                        Else
                            MessageBox.Show("Anda yakin ingin mendaftarkan pengunduran diri?")
                        End If
                        'end
                    End If
                    Update()
                Else
                    MessageBox.Show("Salesman harus teregister terlebih dahulu!")
                End If
            Else

            End If

            ClearData()
            dgSalesmanHeader.CurrentPageIndex = 0
            BindDataGrid(dgSalesmanHeader.CurrentPageIndex)
        End If
    End Sub
    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        txtResignReason.ReadOnly = False
        trSubOrdinate.Visible = False
        ddlSalesmanCode.Enabled = True
        chkResign.Enabled = True
    End Sub
    Private Sub dgSalesmanHeader_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSalesmanHeader.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If
        dgSalesmanHeader.SelectedIndex = -1
        dgSalesmanHeader.CurrentPageIndex = 0
        BindDataGrid(dgSalesmanHeader.CurrentPageIndex)
    End Sub
    Private Sub dgSalesmanHeader_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSalesmanHeader.PageIndexChanged
        dgSalesmanHeader.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgSalesmanHeader.CurrentPageIndex)

        Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(1)
        sessHelper.SetSession("SessionImage", objSalesmanHeader)
    End Sub
    Private Sub dgSalesmanHeader_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesmanHeader.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            View(e.Item.Cells(0).Text, False)
            ddlSalesmanCode.Enabled = False
            txtSalesmanCode.ReadOnly = True
            lblPopUpSalesman.Visible = False
            'lblName.ReadOnly = True
            'lblPosition.ReadOnly = True
            chkResign.Enabled = False
            icResignDate.Enabled = False
            txtResignReason.ReadOnly = True

        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            View(e.Item.Cells(0).Text, True)
            dgSalesmanHeader.SelectedIndex = e.Item.ItemIndex
            ddlSalesmanCode.Enabled = False
            chkResign.Enabled = True
            txtResignReason.ReadOnly = False
            txtSalesmanCode.ReadOnly = True
            lblPopUpSalesman.Visible = False
            SettingDate(icResignDate.Value)


        ElseIf e.CommandName = "Delete" Then
            Delete(e.Item.Cells(0).Text)
            ClearData()
        End If
    End Sub
    Private Sub dgSalesmanHeader_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSalesmanHeader.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSalesmanHeader As SalesmanHeader = e.Item.DataItem

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgSalesmanHeader.CurrentPageIndex * dgSalesmanHeader.PageSize)

            Dim lblPosisiNew As Label = CType(e.Item.FindControl("lblPosisi"), Label)
            lblPosisiNew.Text = objSalesmanHeader.JobPosition.Code

            Dim lblResignDateNew As Label = CType(e.Item.FindControl("lblResignDate"), Label)
            lblResignDateNew.Text = objSalesmanHeader.ResignDate.ToString("dd/MM/yyyy")

            ' yg aktif, dibuatkan disable labelnya - belum resign, related bug 695
            If objSalesmanHeader.ResignDate = Date.Parse(strDefDate) Then
                lblResignDateNew.Visible = False
            Else
                lblResignDateNew.Visible = True
            End If

            Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            If _viewPriv Then
                lbtnView.Visible = True
            Else
                lbtnView.Visible = False
            End If
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            If objSalesmanHeader.ResignDate < New Date(1900, 1, 1) Then
                lbtnEdit.Visible = False
            End If
        End If
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dibatalkan status resignnya?');")
        End If

        If Not IsNothing(Request.QueryString("From")) Then
            e.Item.Cells(6).Visible = False
        End If
    End Sub
    Private Sub ddlSalesmanCode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSalesmanCode.SelectedIndexChanged
        If ddlSalesmanCode.SelectedItem.Text <> "" Then
            ViewState.Add("vsProcess", "Edit")
            Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CType(ddlSalesmanCode.SelectedValue, Integer))

            sessHelper.SetSession("vsSalesmanHeader", objSalesmanHeader)
            If Not IsNothing(objSalesmanHeader) Then
                lblName.Text = objSalesmanHeader.Name
                lblPosition.Text = objSalesmanHeader.JobPosition.Code
                icResignDate.Value = objSalesmanHeader.ResignDate
                txtResignReason.Text = objSalesmanHeader.ResignReason
                SettingDate(objSalesmanHeader.ResignDate)
            End If
        Else
            ClearData()
        End If
    End Sub
    Private Sub chkResign_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkResign.CheckedChanged
        If chkResign.Checked Then
            icResignDate.Enabled = True
            icResignDate.Value = Now()
        Else
            icResignDate.Enabled = False
            icResignDate.Value = Date.Parse(strDefDate)
        End If
    End Sub
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("../Salesman/FrmSalesmanTurnOver.aspx")
    End Sub

    Private Sub ddlResignReason_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlResignReason.SelectedIndexChanged
        txtResignReason.Visible = ddlResignReason.SelectedValue = EnumResignReason_LainLain
    End Sub

    'Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) handles n
    '    sessHelper.SetSession("Status", "Insert")
    '    Response.Redirect("FrmSalesmanEntryResignDetail.aspx")
    'End Sub

#End Region




#Region "Privilege"

    Private Sub CheckPrivilege()

        Dim objDealer As Dealer = sessHelper.GetSession("DEALER")
        If objDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            If Not SecurityProvider.Authorize(Context.User, SR.ResignDataViewCreate_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Tenaga Penjual - Buat Data Pengunduran Diri")
            End If

        End If



    End Sub

    Private Function checkViewDetailPriv() As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.ResignDataViewDetailCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Function checkDownloadPriv() As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.ResignDataDownloadCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region



    Private Sub btnTriggerSalesman_Click(sender As Object, e As EventArgs) Handles btnTriggerSalesman.Click
        Dim funcSales As New SalesmanHeaderFacade(Me.User)
        Dim funcGrade As New SalesmanGradeFacade(Me.User)

        ViewState.Add("vsProcess", "Edit")

        Dim objSales As SalesmanHeader = funcSales.Retrieve(hdnSalesmanCode.Value)
        sessHelper.SetSession("vsSalesmanHeader", objSales)

        lblName.Text = objSales.Name
        lblPosition.Text = objSales.JobPosition.Description

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "LeaderId", MatchType.Exact, objSales.ID))
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.Exact, 2))
        Dim arrSalesman As ArrayList = funcSales.Retrieve(criterias)
        If arrSalesman.Count > 0 Then
            trSubOrdinate.Visible = True
            lblPopupsubordinate.AddOnClick("ShowPopUpSubordinate('" + objSales.ID.ToString() + "');")
        Else
            trSubOrdinate.Visible = False
        End If


    End Sub
End Class

