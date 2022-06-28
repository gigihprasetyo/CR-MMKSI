#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.BabitSalesComm
Imports KTB.DNet.BusinessFacade.GLAccountSalesComm
Imports KTB.DNet.BusinessFacade.CostCenterSalesComm
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

Imports System.IO

Public Class FrmDownloadBabit
    Inherits System.Web.UI.Page


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlMonthPeriodeFrom As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMonthPeriodeTo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dgAlokasiBabit As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtPaymentNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents icPaymentDateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPaymentDateTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlActivityType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlTahun As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTahunTo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkTglPembayaran As System.Web.UI.WebControls.CheckBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Variables"
    Dim _sHelper As New SessionHelper
#End Region

#Region "Constants"
    Private Const VS_CurrentSortColumn As String = "VS_CurrentSortColumn"
    Private Const VS_CurrentSortDirect As String = "VS_CurrentSortDirect"
#End Region

#Region "Properties"
    Private Property CurrentSortColumn() As String
        Get
            Dim val As Object = ViewState(VS_CurrentSortColumn)
            If (val Is Nothing) Then
                val = "Dealer.DealerCode"
            End If
            Return val.ToString()
        End Get
        Set(ByVal Value As String)
            ViewState(VS_CurrentSortColumn) = Value
        End Set
    End Property

    Private Property CurrentSortDirection() As Sort.SortDirection
        Get
            Dim val As Object = ViewState(VS_CurrentSortDirect)
            If (val Is Nothing) Then
                val = Sort.SortDirection.ASC
            End If

            Return CType(val, Sort.SortDirection)
        End Get
        Set(ByVal Value As Sort.SortDirection)
            ViewState(VS_CurrentSortDirect) = Value
        End Set
    End Property
#End Region

#Region "Custom Methods"
    Private Sub SetEnabledButtons(ByVal IsEnable As Boolean)
        btnDownload.Enabled = IsEnable
        btnSearch.Enabled = IsEnable
        If IsEnable Then
            btnSimpan.Enabled = CekDownloadSavePrivilege()
        Else
            btnSimpan.Enabled = IsEnable
        End If

    End Sub

    Private Sub BindToGridTemp()
        dgAlokasiBabit.DataSource = SesDataSource
        dgAlokasiBabit.DataBind()
    End Sub

    Private Sub BindPeriode(ByRef _control As DropDownList)
        _control.Items.Clear()
        _control.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        _control.Items.Add(New ListItem("Januari", "1"))
        _control.Items.Add(New ListItem("Februari", "2"))
        _control.Items.Add(New ListItem("Maret", "3"))
        _control.Items.Add(New ListItem("April", "4"))
        _control.Items.Add(New ListItem("Mei", "5"))
        _control.Items.Add(New ListItem("Juni", "6"))
        _control.Items.Add(New ListItem("Juli", "7"))
        _control.Items.Add(New ListItem("Agustus", "8"))
        _control.Items.Add(New ListItem("September", "9"))
        _control.Items.Add(New ListItem("Oktober", "10"))
        _control.Items.Add(New ListItem("November", "11"))
        _control.Items.Add(New ListItem("Desember", "12"))
    End Sub

    Private Sub BindTahun()
        ddlTahun.Items.Clear()
        ddlTahun.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For i As Integer = DateTime.Today.Year - 5 To DateTime.Today.Year + 1
            ddlTahun.Items.Add(New ListItem(i.ToString, i))
        Next
        ddlTahunTo.Items.Clear()
        ddlTahunTo.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For i As Integer = DateTime.Today.Year - 5 To DateTime.Today.Year + 1
            ddlTahunTo.Items.Add(New ListItem(i.ToString, i))
        Next
    End Sub

    Private Sub BindControls()
        BindPeriode(ddlMonthPeriodeFrom)
        BindPeriode(ddlMonthPeriodeTo)

        Dim arlActivityType As ArrayList = New EnumBabit().BabitProposalTypeList
        ddlActivityType.Items.Clear()
        ddlActivityType.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For Each item As BabitItem In arlActivityType
            ddlActivityType.Items.Add(New ListItem(item.BabitValue, item.BabitCode))
        Next

    End Sub
#End Region

#Region "Deklarasi"
    Dim sHelper As New SessionHelper
#End Region

#Region "Sessions"
    Private Property SesDataSource() As ArrayList
        Get
            Dim arlDataSource As ArrayList = sHelper.GetSession("arlDataSource")
            If arlDataSource Is Nothing Then
                arlDataSource = New ArrayList
                sHelper.SetSession("arlDataSource", arlDataSource)
            End If

            Return arlDataSource
        End Get
        Set(ByVal Value As ArrayList)
            sHelper.SetSession("arlDataSource", Value)
        End Set
    End Property
#End Region
#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.BabitDownloadView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Download BABIT")
        End If
    End Sub

    Private Function CekDownloadEditPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.BabitDownloadEdit_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekDownloadSavePrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.BabitDownloadSave_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekDownloadPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.BabitDownload_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblPopUpDealer.Attributes("onClick") = "ShowPPDealerSelection()"
        InitiateAuthorization()
        If Not IsPostBack Then
            BindControls()
            BindTahun()
            CurrentSortColumn = "Dealer.DealerCode"
            CurrentSortDirection = Sort.SortDirection.ASC

            dgAlokasiBabit.CurrentPageIndex = 0
            'BindDataToGrid(dgAlokasiBabit.CurrentPageIndex)
        End If

        ' add security
        If Not CekDownloadEditPrivilege() Then
            dgAlokasiBabit.Columns(10).Visible = False   ' aksi grid
        End If
        If Not CekDownloadSavePrivilege() Then
            btnSimpan.Enabled = False
            'dgAlokasiBabit.Columns(0).Visible = False 'centang item
        End If
        If Not CekDownloadPrivilege() Then
            btnDownload.Enabled = False
        End If
    End Sub
    Private Function cleanStringForInsetOperator(ByVal str As String)
        str = str.Replace(";", "','").Replace("~", "").Replace("!", "").Replace("`", "").Replace(".", "")
        str = str.Replace("!", "").Replace("@", "").Replace("#", "").Replace("$", "").Replace("%", "")
        str = str.Replace("^", "").Replace("&", "").Replace("*", "").Replace("(", "").Replace(")", "")
        str = str.Replace("_", "").Replace("-", "").Replace("+", "").Replace("=", "").Replace("|", "")
        str = str.Replace("\", "").Replace(">", "").Replace("<", "").Replace("?", "").Replace("/", "")

        Return str
    End Function
    Private Function FindData(ByVal indexPage As Integer, ByRef totalRow As Integer) As ArrayList
        Dim coreType As Type = GetType(BabitPayment)
        Dim criteriaForBabitPayment As CriteriaComposite

        'Add payment date
        Dim dtPaymentDateFrom As Date = icPaymentDateFrom.Value
        Dim dtPaymentDateTo As Date = icPaymentDateTo.Value
        Dim strBabitProposalIDsInBabitPayment As String = String.Empty

        criteriaForBabitPayment = New CriteriaComposite(New Criteria(coreType, "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'Add dealer code criteria if supplied
        Dim dealerCode As String = txtDealerCode.Text.Trim()
        If (dealerCode.Length > 0) Then
            dealerCode = "('" + cleanStringForInsetOperator(dealerCode) + "')"

            criteriaForBabitPayment.opAnd(New Criteria(coreType, "Dealer.DealerCode", MatchType.InSet, dealerCode))
        End If

        If Not txtPaymentNo.Text = String.Empty Then
            criteriaForBabitPayment.opAnd(New Criteria(coreType, "NomorPembayaran", MatchType.[Partial], txtPaymentNo.Text.Trim))
        End If

        If (chkTglPembayaran.Checked) Then
            criteriaForBabitPayment.opAnd(New Criteria(coreType, "PaymentDate", _
                                            MatchType.GreaterOrEqual, dtPaymentDateFrom))
            criteriaForBabitPayment.opAnd(New Criteria(coreType, "PaymentDate", _
                                            MatchType.Lesser, dtPaymentDateTo.AddDays(1)))
        End If

        'Add Periode Month From, To and Year
        'First, we have to find all BabitAlloction that match the supplied criteria        
        Dim StartPeriod As Integer = CInt(ddlMonthPeriodeFrom.SelectedValue)
        Dim EndPeriod As Integer = CInt(ddlMonthPeriodeTo.SelectedValue)

        Dim strBabitAllocationIDs As String = String.Empty
        'If StartPeriod > -1 And EndPeriod > -1 And PeriodYear > -1 Then
        Dim criteriaForBabitAllocation As CriteriaComposite
        criteriaForBabitAllocation = New CriteriaComposite(New Criteria(GetType(BabitAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (StartPeriod <> -1) Then
            criteriaForBabitAllocation.opAnd(New Criteria(GetType(BabitAllocation), "Babit.StartPeriod", MatchType.LesserOrEqual, StartPeriod))
        End If

        'If (EndPeriod <> -1) Then
        '    criteriaForBabitAllocation.opAnd(New Criteria(GetType(BabitAllocation), "Babit.EndPeriod", MatchType.GreaterOrEqual, EndPeriod))
        'End If

        If ddlTahun.SelectedValue <> -1 Then
            criteriaForBabitAllocation.opAnd(New Criteria(GetType(BabitAllocation), "Babit.BabitYear", MatchType.LesserOrEqual, ddlTahun.SelectedValue))
        End If

        If ddlTahunTo.SelectedValue <> -1 Then
            criteriaForBabitAllocation.opAnd(New Criteria(GetType(BabitAllocation), "Babit.BabitYearEnd", MatchType.GreaterOrEqual, ddlTahunTo.SelectedValue))
        End If
        If EndPeriod <> -1 Then
            If (StartPeriod = -1) Or (ddlTahun.SelectedValue = "-1") Or (ddlTahunTo.SelectedValue = "-1") Then
                criteriaForBabitAllocation.opAnd(New Criteria(GetType(BabitAllocation), "Babit.EndPeriod", MatchType.GreaterOrEqual, EndPeriod))
            End If
        End If

        Dim arlBabitAllocation As ArrayList = New BabitFacade(User).RetrieveBabitAllocation(criteriaForBabitAllocation)

        'If no record exists, just return an empty array list
        If arlBabitAllocation.Count <= 0 Then
            Return New ArrayList
        End If

        Dim bfacade As New BabitSalesComm.BabitProposalFacade(User)
        If (ddlMonthPeriodeFrom.SelectedValue <> "-1" And ddlTahun.SelectedValue <> "-1" And ddlMonthPeriodeTo.SelectedValue <> "-1" And ddlTahunTo.SelectedValue <> "-1") Then
            Dim dtmFrom2 As DateTime = New DateTime(CInt(ddlTahun.SelectedValue), CInt(ddlMonthPeriodeFrom.SelectedValue), 1)
            Dim dtmTo2 As DateTime = New DateTime(CInt(ddlTahunTo.SelectedValue), CInt(ddlMonthPeriodeTo.SelectedValue), 1)
            Dim arl2 As ArrayList = bfacade.FilterBabitAllocationByPeriodeList(arlBabitAllocation, dtmFrom2, dtmTo2)
            arlBabitAllocation = New ArrayList
            arlBabitAllocation = arl2
            If IsNothing(arlBabitAllocation) Then
                arlBabitAllocation = New ArrayList
            End If
        End If

        'If found, then build a list of BabitAllocation id so we can use
        'InSet criteria to search for BabitProposal 
        Dim distinctIDs As New Hashtable
        For i As Integer = 0 To arlBabitAllocation.Count - 1
            If strBabitAllocationIDs.Length > 0 Then
                strBabitAllocationIDs += ","
            End If

            Dim item As BabitAllocation = CType(arlBabitAllocation(i), BabitAllocation)

            If distinctIDs(item.ID) Is Nothing Then
                strBabitAllocationIDs += item.ID.ToString()
                distinctIDs.Add(item.ID, item.ID)
            End If
        Next

        If strBabitAllocationIDs.Length > 0 Then
            strBabitAllocationIDs = "(" + strBabitAllocationIDs + ")"
        End If
        'End If

        Dim criteriaForBabitProposal As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitProposal), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If strBabitAllocationIDs.Length > 0 Then
            criteriaForBabitProposal.opAnd(New Criteria(GetType(BabitProposal), "BabitAllocation.ID", MatchType.InSet, strBabitAllocationIDs))
        End If

        'Add ActivityType
        Dim strActivityType As String = ddlActivityType.SelectedValue
        If strActivityType <> "-1" Then
            criteriaForBabitProposal.opAnd(New Criteria(GetType(BabitProposal), "ActivityType", MatchType.Exact, ddlActivityType.SelectedValue))
        End If

        'Search BabitProposal
        Dim arlBabitProposal As ArrayList = New BabitFacade(User).RetrieveBabitProposal(criteriaForBabitProposal)
        'If found, then build a list of BabitProposal's id
        Dim distinctBabitProposalIDs As New Hashtable
        Dim strBabitProposalIds As String = String.Empty
        For i As Integer = 0 To arlBabitProposal.Count - 1
            If strBabitProposalIds.Length > 0 Then
                strBabitProposalIds += ","
            End If

            Dim item As BabitProposal = CType(arlBabitProposal(i), BabitProposal)

            If distinctBabitProposalIDs(item.ID) Is Nothing Then
                strBabitProposalIds += item.ID.ToString()
                distinctBabitProposalIDs.Add(item.ID, item.ID)
            End If
        Next

        If strBabitProposalIds.Length > 0 Then
            strBabitProposalIds = "(" + strBabitProposalIds + ")"

            criteriaForBabitPayment.opAnd(New Criteria(coreType, "BabitProposal.ID", MatchType.InSet, strBabitProposalIds))
        End If

        Dim arlAllData As ArrayList = New BabitFacade(User).RetrieveBabitPayments(criteriaForBabitPayment, indexPage + 1, _
                                            dgAlokasiBabit.PageSize, totalRow, _
                                            CurrentSortColumn, CurrentSortDirection)

        Return arlAllData
    End Function

    Private Function IsOKToSearch() As Boolean
        'Validate Tgl. Pembayaran
        Dim dtPaymentDateStart As Date = icPaymentDateFrom.Value
        Dim dtPaymentDateEnd As Date = icPaymentDateTo.Value

        If dtPaymentDateStart.Subtract(dtPaymentDateEnd).TotalDays > 0 Then
            MessageBox.Show("Tgl. Pembayaran awal tidak boleh lebih besar dari Tgl. Pembayaran akhir")
            Return False
        End If


        'Validate Period
        'Dim strStartPeriod As String = ddlMonthPeriodeFrom.SelectedValue
        'Dim strEndPeriod As String = ddlMonthPeriodeTo.SelectedValue
        'Dim strPeriodYear As String = txtPeriodYear.Text.Trim

        'Dim bIsAllPeriodFilled = (strStartPeriod <> -1 Or strEndPeriod <> -1 Or strPeriodYear.Length > 0)

        'Validate Start Period
        'If strStartPeriod <> "-1" Then
        '    If strEndPeriod = "-1" Or strPeriodYear.Length <= 0 Then
        '        MessageBox.Show("Bila Periode di isi maka Tahun juga harus diisi")
        '        Return False
        '    End If
        'End If
        'Validate End Period
        'If strEndPeriod <> "-1" Then
        '    If strStartPeriod = "-1" Or strPeriodYear.Length <= 0 Then
        '        MessageBox.Show("Bila Periode di isi maka Tahun juga harus diisi")
        '        Return False
        '    End If
        'End If
        'Validate Year Period
        'If strPeriodYear.Length > 0 Then
        '    If strStartPeriod = "-1" Or strEndPeriod = "-1" Then
        '        MessageBox.Show("Bila Tahun di isi maka Periode juga harus diisi")
        '        Return False
        '    End If
        'End If

        'Validate StartPeriod and EndPeriod

        If (CInt(ddlTahun.SelectedValue) <> -1 And CInt(ddlTahunTo.SelectedValue) <> -1) Then
            If (CInt(ddlTahun.SelectedValue) = CInt(ddlTahunTo.SelectedValue)) Then
                If (CInt(ddlMonthPeriodeFrom.SelectedValue) <> -1 And CInt(ddlMonthPeriodeTo.SelectedValue) <> -1) Then
                    If CInt(ddlMonthPeriodeFrom.SelectedValue) > CInt(ddlMonthPeriodeTo.SelectedValue) Then
                        MessageBox.Show("Periode awal harus lebih kecil dari akhir periode")
                        Exit Function
                    End If
                End If
            ElseIf (CInt(ddlTahun.SelectedValue) > CInt(ddlTahunTo.SelectedValue)) Then
                MessageBox.Show("Periode awal harus lebih kecil dari akhir periode")
                Exit Function
            End If
        End If

        'Validate Year Period
        'If strPeriodYear.Length > 0 Then
        '    If Not IsNumeric(strPeriodYear) Then
        '        MessageBox.Show("Tahun harus di isi angka")
        '        Return False
        '    End If
        'End If

        Return True
    End Function

    Private Function SetGLAndCostCenter(ByVal list As ArrayList) As ArrayList
        Dim newList As ArrayList = New ArrayList
        For Each item As BabitPayment In list
            If item.GLAccount Is Nothing Then
                item.GLAccount = New GLAccountFacade(User).Retrieve("43021030")
            End If
            If item.CostCenter Is Nothing Then
                item.CostCenter = New CostCenterFacade(User).Retrieve("CC1000000")
            End If
            newList.Add(item)
        Next
        Return newList
    End Function

    Private Sub BindDataToGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer

        If indexPage >= 0 Then

            Dim arlAllData As ArrayList = New ArrayList
            If IsOKToSearch() Then
                arlAllData = FindData(indexPage, totalRow)
                If (IsNothing(arlAllData) Or arlAllData.Count <= 0) Then
                    MessageBox.Show("Data tidak ditemukan")
                End If
            End If
            arlAllData = SetGLAndCostCenter(arlAllData)
            SesDataSource = arlAllData
            dgAlokasiBabit.DataSource = arlAllData
            dgAlokasiBabit.VirtualItemCount = totalRow
            If indexPage = 0 Then
                dgAlokasiBabit.CurrentPageIndex = 0
            End If

            dgAlokasiBabit.DataBind()
        End If
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            dgAlokasiBabit.CurrentPageIndex = 0
            BindDataToGrid(dgAlokasiBabit.CurrentPageIndex)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dgAlokasiBabit_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgAlokasiBabit.EditCommand
        dgAlokasiBabit.EditItemIndex = e.Item.ItemIndex
        'dgAlokasiBabit.ShowFooter = False

        SetEnabledButtons(False)

        BindToGridTemp()
    End Sub

    Private Sub dgAlokasiBabit_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgAlokasiBabit.CancelCommand
        dgAlokasiBabit.EditItemIndex = -1
        'dgAlokasiBabit.ShowFooter = True

        SetEnabledButtons(True)

        BindToGridTemp()
    End Sub

    Private Sub dgAlokasiBabit_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgAlokasiBabit.UpdateCommand
        'dtgPhotoList.ShowFooter = True

        Dim arrDataUpd As ArrayList = SesDataSource

        Dim ddlGLAccount As DropDownList = e.Item.FindControl("ddlEditGLAccount")
        Dim ddlCostCenter As DropDownList = e.Item.FindControl("ddlEditShortText")
        Dim txtDescription As TextBox = e.Item.FindControl("txtEditKeterangan")

        Dim GLAccountID As Integer = CInt(ddlGLAccount.SelectedValue)
        Dim CostCenterID As Integer = CInt(ddlCostCenter.SelectedValue)

        If GLAccountID = -1 Then
            MessageBox.Show("GL Account harus dipilih")
            Return
        End If

        If CostCenterID = -1 Then
            MessageBox.Show("Cost Center harus dipilih")
            Return
        End If
        If txtDescription.Text.Trim = String.Empty Then
            MessageBox.Show("Keterangan harus diisi")
            Return
        Else
            If txtDescription.Text.Length > 40 Then
                MessageBox.Show("Keterangan Max 40 Karakter")
                Return
            End If
        End If
        Dim objDomain As BabitPayment = CType(arrDataUpd(e.Item.ItemIndex), BabitPayment)
        objDomain.GLAccount = New GLAccountSalesComm.GLAccountFacade(User).Retrieve(GLAccountID)
        objDomain.CostCenter = New CostCenterSalesComm.CostCenterFacade(User).Retrieve(CostCenterID)
        objDomain.Keterangan = txtDescription.Text.Trim()
        Dim objUpdateBabitPayFacade As BabitPaymentFacade = New BabitPaymentFacade(User)
        If objUpdateBabitPayFacade.Update(objDomain) <> -1 Then
            dgAlokasiBabit.EditItemIndex = -1
            SetEnabledButtons(True)
            BindToGridTemp()
        Else
            MessageBox.Show("Gagal update")
            Exit Sub
        End If
        'arrDataUpd(e.Item.ItemIndex) = objAuditfoto

        'SesPhotoList = arrDataUpd
        
    End Sub

    Private Sub dgAlokasiBabit_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgAlokasiBabit.ItemDataBound
        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then

            'Bind GL Account Dropdown
            Dim ddlGLAccount As DropDownList = e.Item.FindControl("ddlEditGLAccount")
            If Not ddlGLAccount Is Nothing Then
                Dim arlGLAccount As ArrayList = New GLAccountSalesComm.GLAccountFacade(User).RetrieveActiveList()

                ddlGLAccount.Items.Clear()
                ddlGLAccount.Items.Add(New ListItem("Silahkan Pilih", "-1"))
                For Each item As GLAccount In arlGLAccount
                    ddlGLAccount.Items.Add(New ListItem(item.AccountNumber + " - " + item.Description, item.ID.ToString()))
                Next
            End If
            Dim objDomain As BabitPayment = CType(SesDataSource(e.Item.ItemIndex), BabitPayment)
            If Not objDomain.GLAccount Is Nothing Then
                ddlGLAccount.SelectedValue = objDomain.GLAccount.ID.ToString
            End If

            'Bind Cost Center Dropdown
            Dim ddlCostCenter As DropDownList = e.Item.FindControl("ddlEditShortText")
            If Not ddlCostCenter Is Nothing Then
                Dim arlCostCenter As ArrayList = New CostCenterSalesComm.CostCenterFacade(User).RetrieveActiveList()

                ddlCostCenter.Items.Clear()
                ddlCostCenter.Items.Add(New ListItem("Silahkan Pilih", "-1"))
                For Each item As CostCenter In arlCostCenter
                    ddlCostCenter.Items.Add(New ListItem(item.CostCenterCode + " - " + item.ShortText, item.ID.ToString()))
                Next
            End If

            If Not objDomain.CostCenter Is Nothing Then
                ddlCostCenter.SelectedValue = objDomain.CostCenter.ID.ToString
            End If
        ElseIf e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim objDomain As BabitPayment = CType(SesDataSource(e.Item.ItemIndex), BabitPayment)
            Dim lblActivityType As Label = e.Item.FindControl("lblActivityType")
            If CType(objDomain.BabitProposal.ActivityType, KTB.DNet.Domain.EnumBabit.BabitProposalType).ToString() = "Even" Then
                lblActivityType.Text = "Event"
            Else
                lblActivityType.Text = CType(objDomain.BabitProposal.ActivityType, KTB.DNet.Domain.EnumBabit.BabitProposalType).ToString()
            End If
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim nresult As Integer = 0

        Dim arlDataSource As ArrayList = SesDataSource
        Dim counter As Integer = 1
        If arlDataSource.Count > 0 Then
            For Each item As BabitPayment In arlDataSource
                If item.GLAccount Is Nothing Then
                    MessageBox.Show("GL Account harus di isi untuk item No. " + counter.ToString())
                    Return
                End If
                counter += 1
            Next

            Dim facade As New BabitPaymentFacade(User)

            nresult = facade.UpdateTransaction(SesDataSource)

            If nresult <> -1 Then
                MessageBox.Show(SR.SaveSuccess)
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        Else
            MessageBox.Show("Data tidak ada yang dipilih.")
        End If

    End Sub
    Private Function WriteBabitData(ByRef sw As StreamWriter)
        Dim list As ArrayList = SesDataSource

        Dim lines As New System.Text.StringBuilder
        'Dim separator As String = "	" 'use tab as separator
        Dim separator As String = ";"

        Dim SelectedItemCount As Integer = 0
        For i As Integer = 0 To dgAlokasiBabit.Items.Count - 1
            If CType(dgAlokasiBabit.Items(i).FindControl("chkSelection"), CheckBox).Checked Then
                SelectedItemCount += 1
            End If
        Next

        For i As Integer = 0 To dgAlokasiBabit.Items.Count - 1
            Dim objDomain As BabitPayment = CType(list(i), BabitPayment)
            If CType(dgAlokasiBabit.Items(i).FindControl("chkSelection"), CheckBox).Checked Then
                '---modif by Ronny (bugs 794)

                '--order type
                lines.Append("ZE")
                lines.Append(separator)

                '--AccountAssignmentCat
                lines.Append("K")
                lines.Append(separator)

                '--AccountAssignmentCat
                lines.Append("D")
                lines.Append(separator)

                '--deliverydate
                lines.Append(Date.Today.ToString("ddMMyyyy"))
                lines.Append(separator)


                '--plant
                lines.Append("P100")
                lines.Append(separator)


                '--purchgroup
                lines.Append("083")
                lines.Append(separator)



                '--matgroup
                lines.Append("Others".ToUpper())
                lines.Append(separator)



                '--requistioner
                lines.Append("PROMOTION")
                lines.Append(separator)

                '--Shorttext
                Dim strKeterangan As String
                If objDomain.Keterangan.Length > 40 Then
                    strKeterangan = objDomain.Keterangan.Substring(0, 40)
                Else
                    strKeterangan = objDomain.Keterangan
                End If
                lines.Append(strKeterangan)
                lines.Append(separator)

                '--Qty
                lines.Append("1")
                lines.Append(separator)

                '--unit
                lines.Append("PC")
                lines.Append(separator)

                '--ValuationikoPrice
                lines.Append(objDomain.BabitProposal.KTBApprovalAmount.ToString("#0"))
                lines.Append(separator)

                '--PurchOrg
                lines.Append("PR14")
                lines.Append(separator)


                '--destinationvendor
                lines.Append(objDomain.Dealer.DealerCode)
                lines.Append(separator)

                Dim glAcc As String
                Dim cc As String
                'If objDomain.BabitProposal.BabitAllocation.Babit.AllocationType = EnumBabit.BabitAllocationType.Alokasi_Reguler Or objDomain.BabitProposal.BabitAllocation.Babit.AllocationType = EnumBabit.BabitAllocationType.Alokasi_Tambahan Then
                '    glAcc = "43021030"
                '    cc = "CC1000000"
                'Else
                '    glAcc = "43022040"
                '    cc = "OTHER"
                'End If

                If objDomain.CostCenter Is Nothing Then
                    cc = "CC1000000"
                Else
                    cc = objDomain.CostCenter.CostCenterCode
                End If


                If objDomain.GLAccount Is Nothing Then
                    glAcc = "43021030"
                Else
                    glAcc = objDomain.GLAccount.AccountNumber
                End If


                '--CostCenter
                lines.Append(cc)
                lines.Append(separator)


                '--G/LAccount
                lines.Append(glAcc)
                lines.Append(separator)


                '--Buss.Area
                lines.Append("ZC11")
                lines.Append(separator)

                '--No Persetujuan
                lines.Append(objDomain.BabitProposal.NoPersetujuan.Trim)

                If i < SelectedItemCount - 1 Then
                    lines.Append(vbNewLine)
                End If
            End If
        Next

        sw.WriteLine(lines.ToString())

    End Function
    Private Sub DoDownload()
        '-- Download data in datagrid to text file

        'Check for items validity
        Dim list As ArrayList = SesDataSource
        Dim bIsNoItemSelected As Boolean = False
        For i As Integer = 0 To dgAlokasiBabit.Items.Count - 1
            Dim objDomain As BabitPayment = CType(list(i), BabitPayment)
            If CType(dgAlokasiBabit.Items(i).FindControl("chkSelection"), CheckBox).Checked Then
                'If objDomain.GLAccount Is Nothing Then
                '    MessageBox.Show("GL Account untuk item " + (i + 1).ToString + " harus diisi")
                '    Return
                'End If

                'If objDomain.CostCenter Is Nothing Then
                '    MessageBox.Show("Cost Center untuk item " + (i + 1).ToString + " harus diisi")
                '    Return
                'End If

                If objDomain.Keterangan = String.Empty Then
                    MessageBox.Show("Field keterangan harus di isi")
                    Return
                End If

                bIsNoItemSelected = True
            End If
        Next
        If Not bIsNoItemSelected Then
            MessageBox.Show("Minimal 1 item harus dipilih")
            Return
        End If
        '-- Generate timestamp
        Dim dt As DateTime = DateTime.Now
        Dim sSuffix As String = CType(dt.Year, String) & CType(dt.Month, String) & _
                                CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & _
                                CType(dt.Second, String) & CType(dt.Millisecond, String)

        '-- Temp file must be a randomly named text file!
        'Dim BabitDataPath As String = Server.MapPath("") & "\..\DataTemp\BabitPaymentData" & sSuffix & ".txt"
        Dim BabitDataPath As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\SalesComm\BabitPaymentData" & sSuffix & ".txt"
        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim dirInfo As New DirectoryInfo(Path.GetDirectoryName(BabitDataPath))

                If Not dirInfo.Exists Then
                    dirInfo.Create()
                End If

                Dim finfo As FileInfo = New FileInfo(BabitDataPath)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(BabitDataPath, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteBabitData(sw)

                '-- Close stream & file
                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing
                MessageBox.Show("Data berhasil diupload ke SAP")

            End If

            '-- Download color data to client!
            'Response.Redirect("../downloadlocal.aspx?file=DataTemp\BabitPaymentData" & sSuffix & ".txt")

        Catch ex As Exception
            Dim errMess As String = ex.Message
        End Try
    End Sub
    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Try
            DoDownload()
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try
    End Sub

    Private Sub chkTglPembayaran_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTglPembayaran.CheckedChanged
        icPaymentDateFrom.Enabled = chkTglPembayaran.Checked
        icPaymentDateTo.Enabled = chkTglPembayaran.Checked
    End Sub
End Class
