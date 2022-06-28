#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
#End Region

Public Class FrmSalesmanEvaluasi
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon4 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblChassisNo As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents chkValidPeriod As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents icStartValid As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEndValid As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents icEndConfirm As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents lblJumRecord As System.Web.UI.WebControls.Label
    Protected WithEvents dgInvoiceList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtDownload As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents txtSalesman As System.Web.UI.WebControls.TextBox
    Protected WithEvents lboxStatus As System.Web.UI.WebControls.ListBox
    Protected WithEvents lblInvoiceNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents lblCategoryTeam As System.Web.UI.WebControls.Label
    Protected WithEvents lblCategory As System.Web.UI.WebControls.Label
    Protected WithEvents chkValidation As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents icStartValidation As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEndValidation As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents icStartOpen As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEndOpen As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEndConfirmTime As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents chkConfirm As System.Web.UI.WebControls.CheckBox
    Protected WithEvents icStartConfirm As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents chkOpenFaktur As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents icStartConfirmTime As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents chkSPK As System.Web.UI.WebControls.CheckBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


#Region " Private fields "
    Dim sessHelp As SessionHelper = New SessionHelper
    Dim InvoiceList As New ArrayList  '-- List of invoice
    Dim _PCAccessAllowed As Boolean = False
    Dim _CVAccessAllowed As Boolean = False
    Dim _LCVAccessAllowed As Boolean = False
    Private objDealer As Dealer
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Page.Server.ScriptTimeout = 600
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        If Not IsPostBack Then
            BindDropdownList()  '-- Bind dropdownlist
            BindListBoxList()
            ViewState("currSortColumn") = "DealerCode"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            BindPage(dgInvoiceList.CurrentPageIndex)  '-- Bind page-1
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        lblSalesman.Attributes("onClick") = "ShowSalesmanSelection();"
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgInvoiceList.CurrentPageIndex = 0
        BindPage(dgInvoiceList.CurrentPageIndex)  '-- Bind page-1
    End Sub

    Private Sub dgInvoiceList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgInvoiceList.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSPK As V_SPK_Salesman = e.Item.DataItem
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgInvoiceList.CurrentPageIndex * dgInvoiceList.PageSize)
            e.Item.Cells(2).ToolTip = objSPK.DealerName
            e.Item.Cells(6).Text = objSPK.CategoryTeam
            e.Item.Cells(8).Text = objSPK.FakturDate.ToString("dd-MM-yyyy")
            Dim EnumStatus As EnumChassisMaster.FakturStatus = objSPK.FakturStatus
            e.Item.Cells(9).Text = EnumStatus.ToString
            e.Item.Cells(11).Text = objSPK.SPKDate.ToString("dd-MM-yyyy")

        End If
    End Sub

    Private Sub dgInvoiceList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgInvoiceList.PageIndexChanged
        dgInvoiceList.CurrentPageIndex = e.NewPageIndex
        BindPage(dgInvoiceList.CurrentPageIndex)
    End Sub

    Private Sub dgInvoiceList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgInvoiceList.SortCommand
        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If
        dgInvoiceList.SelectedIndex = -1
        BindPage(dgInvoiceList.CurrentPageIndex)
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim arlSPKSalesman As New ArrayList
        If Not IsNothing(sessHelp.GetSession("SalesmanEvaluationList")) Then
            'arlSPKSalesman = CType(sessHelp.GetSession("SalesmanEvaluationList"), ArrayList)
            'arlSPKSalesman = Me.getDataToDownload()
        End If
        DoDownload(Me.getDataToDownload())
    End Sub
#End Region

#Region "Custom"

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.IDTPViewCreate_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=TENAGA PENJUAL - Evaluasi Tenaga Penjual")
        End If
        _PCAccessAllowed = True 'SecurityProvider.Authorize(context.User, SR.PKCategoryPC_Privilege)
        _CVAccessAllowed = True 'SecurityProvider.Authorize(context.User, SR.PKCategoryCV_Privilege)
        _LCVAccessAllowed = True 'SecurityProvider.Authorize(context.User, SR.PKCategoryLCV_Privilege)
        If (Not _PCAccessAllowed) And (Not _CVAccessAllowed) And (Not _LCVAccessAllowed) Then
            Me.btnSearch.Visible = False
            Me.ddlCategory.Visible = False
        End If
    End Sub

    Private Sub BindListBoxList()
        lboxStatus.DataSource = New EnumDNET().RetrieveStatusFakturKendaraan
        lboxStatus.DataTextField = "NameType"
        lboxStatus.DataValueField = "ValType"
        lboxStatus.DataBind()
    End Sub

    Private Sub BindDropdownList()

        '-- Category criteria & sort
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim cat As String = ""
        If _PCAccessAllowed Then
            cat = cat & "'PC',"
        End If
        If _CVAccessAllowed Then
            cat = cat & "'CV',"
        End If
        If _LCVAccessAllowed Then
            cat = cat & "'LCV',"
        End If
        If cat <> "" Then
            cat = "(" & cat.Substring(0, cat.Length - 1) & ")"
            criterias.opAnd(New Criteria(GetType(Category), "CategoryCode", MatchType.InSet, cat))
        End If

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code

        '-- Bind Category dropdownlist
        ddlCategory.DataSource = New CategoryFacade(User).RetrieveByCriteria(criterias, sortColl)
        ddlCategory.DataTextField = "CategoryCode"
        ddlCategory.DataValueField = "ID"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item

        lboxStatus.SelectedIndex = -1  '-- Clear status selection

    End Sub

    Private Function GetSelectedItem(ByVal listboxStatus As ListBox) As String
        '-- Items selected in listbox

        Dim _strStatus As String = String.Empty
        For Each item As ListItem In listboxStatus.Items
            If item.Selected Then
                If _strStatus = String.Empty Then
                    _strStatus = item.Value
                Else
                    _strStatus = _strStatus & "," & item.Value
                End If
            End If
        Next
        Return _strStatus
    End Function

    Private Function DateValidation() As Boolean
        '-- Date validation
        If Not chkValidPeriod.Checked And Not chkSPK.Checked _
            And Not chkValidation.Checked And Not chkOpenFaktur.Checked And Not chkConfirm.Checked Then
            '-- At least one date range is set: Periode Validasi or Periode Konfirmasi
            MessageBox.Show("Salah satu tanggal harus dipilih.")
            Return False
        End If

        '-- Tanggal Faktur
        If chkValidPeriod.Checked Then
            If icStartValid.Value > icEndValid.Value Then
                '-- It must be Start date <= End date
                MessageBox.Show("Interval tanggal validasi tidak valid")
                Return False
            Else
                If icEndValid.Value.Subtract(icStartValid.Value).Days > 65 Then
                    '-- The difference must be lesser or equal to 65 days
                    MessageBox.Show("Selisih tanggal validasi harus <= 65 hari")
                    Return False
                End If
            End If
        End If

        '-- Tanggal SPK
        If chkSPK.Checked Then
            If icStartConfirm.Value > icEndConfirm.Value Then
                '-- It must be Start date <= End date
                MessageBox.Show("Interval tanggal konfirmasi tidak valid")
                Return False
            Else
                If icEndConfirm.Value.Subtract(icStartConfirm.Value).Days > 65 Then
                    '-- The difference must be lesser or equal to 65 days
                    MessageBox.Show("Selisih tanggal konfirmasi harus <= 65 hari")
                    Exit Function '-- Directly exits
                End If
            End If
        End If

        '-- Tanggal Validasi Faktur
        If chkValidation.Checked Then
            If icStartValidation.Value > icEndValidation.Value Then
                '-- It must be Start date <= End date
                MessageBox.Show("Interval tanggal validasi faktur tidak valid")
                Return False
            Else
                If icEndValidation.Value.Subtract(icStartValidation.Value).Days > 65 Then
                    '-- The difference must be lesser or equal to 65 days
                    MessageBox.Show("Selisih tanggal validasi faktur harus <= 65 hari")
                    Return False
                End If
            End If
        End If

        '-- Tanggal Buka Faktur
        If chkOpenFaktur.Checked Then
            If icStartOpen.Value > icEndOpen.Value Then
                '-- It must be Start date <= End date
                MessageBox.Show("Interval tanggal buka faktur tidak valid")
                Return False
            Else
                If icEndOpen.Value.Subtract(icStartOpen.Value).Days > 65 Then
                    '-- The difference must be lesser or equal to 65 days
                    MessageBox.Show("Selisih tanggal buka faktur harus <= 65 hari")
                    Return False
                End If
            End If
        End If

        '-- Tanggal Konfirmasi Faktur
        If chkConfirm.Checked Then
            If icStartConfirmTime.Value > icEndConfirmTime.Value Then
                '-- It must be Start date <= End date
                MessageBox.Show("Interval tanggal konfirmasi faktur tidak valid")
                Return False
            Else
                If icEndConfirmTime.Value.Subtract(icStartConfirmTime.Value).Days > 65 Then
                    '-- The difference must be lesser or equal to 65 days
                    MessageBox.Show("Selisih tanggal konfirmasi faktur harus <= 65 hari")
                    Return False
                End If
            End If
        End If
        Return True
    End Function

    Private Sub BindPage(ByVal pageIndex As Integer)
        '-- Read all data selected
        Dim total As Integer = 0

        If Not DateValidation() Then
            Exit Sub
        End If

        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(V_SPK_Salesman), "SPKFakturID", MatchType.Greater, CType(DBRowStatus.Active, Short)))

        '-- Category
        If ddlCategory.SelectedValue <> "" Then
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman), "CategoryID", MatchType.Exact, ddlCategory.SelectedValue))
        End If

        '-- Status
        If lboxStatus.SelectedIndex <> -1 Then
            Dim SelectedStatus As String = GetSelectedItem(lboxStatus)
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman), "FakturStatus", MatchType.InSet, "(" & SelectedStatus & ")"))
        Else
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman), "FakturStatus", MatchType.InSet, "('1','2','3','4')"))
        End If

        If chkValidPeriod.Checked Then
            '-- Periode Validasi
            Dim StartValid As New DateTime(CInt(icStartValid.Value.Year), CInt(icStartValid.Value.Month), CInt(icStartValid.Value.Day), 0, 0, 0)
            Dim EndValid As New DateTime(CInt(icEndValid.Value.Year), CInt(icEndValid.Value.Month), CInt(icEndValid.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman), "FakturDate", MatchType.GreaterOrEqual, Format(StartValid, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman), "FakturDate", MatchType.LesserOrEqual, Format(EndValid, "yyyy-MM-dd HH:mm:ss")))
        End If

        If chkSPK.Checked Then
            '-- Periode Konfirmasi
            Dim StartConfirm As New DateTime(CInt(icStartConfirm.Value.Year), CInt(icStartConfirm.Value.Month), CInt(icStartConfirm.Value.Day), 0, 0, 0)
            Dim EndConfirm As New DateTime(CInt(icEndConfirm.Value.Year), CInt(icEndConfirm.Value.Month), CInt(icEndConfirm.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman), "SPKDate", MatchType.GreaterOrEqual, Format(StartConfirm, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman), "SPKDate", MatchType.LesserOrEqual, Format(EndConfirm, "yyyy-MM-dd HH:mm:ss")))
        End If

        If chkValidation.Checked Then
            '-- Periode Konfirmasi
            Dim StartValidation As New DateTime(CInt(icStartValidation.Value.Year), CInt(icStartValidation.Value.Month), CInt(icStartValidation.Value.Day), 0, 0, 0)
            Dim EndValidation As New DateTime(CInt(icEndValidation.Value.Year), CInt(icEndValidation.Value.Month), CInt(icEndValidation.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman), "FakturValidateTime", MatchType.GreaterOrEqual, Format(StartValidation, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman), "FakturValidateTime", MatchType.LesserOrEqual, Format(EndValidation, "yyyy-MM-dd HH:mm:ss")))
        End If

        If chkOpenFaktur.Checked Then
            '-- Periode Konfirmasi
            Dim StartOpenFaktur As New DateTime(CInt(icStartOpen.Value.Year), CInt(icStartOpen.Value.Month), CInt(icStartOpen.Value.Day), 0, 0, 0)
            Dim EndOpenFaktur As New DateTime(CInt(icEndOpen.Value.Year), CInt(icEndOpen.Value.Month), CInt(icEndOpen.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman), "InvoiceOpen", MatchType.GreaterOrEqual, Format(StartOpenFaktur, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman), "InvoiceOpen", MatchType.LesserOrEqual, Format(EndOpenFaktur, "yyyy-MM-dd HH:mm:ss")))
        End If

        '--Faktur Confirmation date
        If chkConfirm.Checked Then
            '-- Periode Konfirmasi
            Dim StartKonfirmasiFaktur As New DateTime(CInt(icStartConfirmTime.Value.Year), CInt(icStartConfirmTime.Value.Month), CInt(icStartConfirmTime.Value.Day), 0, 0, 0)
            Dim EndKonfirmasiFaktur As New DateTime(CInt(icEndConfirmTime.Value.Year), CInt(icEndConfirmTime.Value.Month), CInt(icEndConfirmTime.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman), "ConfirmTime", MatchType.GreaterOrEqual, Format(StartKonfirmasiFaktur, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman), "ConfirmTime", MatchType.LesserOrEqual, Format(EndKonfirmasiFaktur, "yyyy-MM-dd HH:mm:ss")))
        End If
        'objDealer = Session("DEALER")
        'If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        '    criterias.opAnd(New Criteria(GetType(V_SPK_Salesman), "DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        'End If

        '-- Dealer code
        If txtKodeDealer.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman), "DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        End If

        '-- Salesman
        If txtSalesman.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman), "SalesmanCode", MatchType.Exact, txtSalesman.Text.Trim()))
        End If

        '-- Retrieve recordset
        Dim SPKSalesmanResultList As ArrayList '= New V_SPK_SalesmanFacade(User).RetrieveByCriteria(, sortColl)
        SPKSalesmanResultList = New V_SPK_SalesmanFacade(User).RetrieveActiveList(dgInvoiceList.CurrentPageIndex + 1, dgInvoiceList.PageSize, _
                total, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection), criterias)
        '-- Store recordset into session for later use
        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(V_SPK_Salesman), ViewState("currSortColumn"), ViewState("currSortDirection")))

        'Dim SPKSalesmanDownloadList As ArrayList = New V_SPK_SalesmanFacade(User).Retrieve(criterias, sortColl)
        'replace with getDataToDownload
        Dim SPKSalesmanDownloadList As ArrayList = New ArrayList
        sessHelp.SetSession("SalesmanEvaluationList", SPKSalesmanDownloadList)

        If SPKSalesmanResultList.Count > 0 Then
            '-- Enable all buttons if any record exists
            btnDownload.Enabled = True
        Else
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
            btnDownload.Enabled = False
        End If

        If SPKSalesmanResultList.Count <> 0 Then
            'Dim PagedList As ArrayList = ArrayListPager.DoPage(SPKSalesmanResultList, pageIndex, dgInvoiceList.PageSize)
            dgInvoiceList.DataSource = SPKSalesmanResultList
            dgInvoiceList.VirtualItemCount = total 'SPKSalesmanResultList.Count()
            dgInvoiceList.DataBind()
        Else
            dgInvoiceList.DataSource = New ArrayList
            dgInvoiceList.VirtualItemCount = 0
            dgInvoiceList.CurrentPageIndex = 0
            dgInvoiceList.DataBind()
        End If
        If dgInvoiceList.VirtualItemCount > 0 Then
            lblJumRecord.Text = "Jumlah record : " & dgInvoiceList.VirtualItemCount
        End If
    End Sub

    Private Function getDataToDownload() As ArrayList
        '-- Read all data selected
        Dim total As Integer = 0
        '-- Date validation
        If Not DateValidation() Then
            Exit Function
        End If

        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(V_SPK_Salesman_Download), "SPKFakturID", MatchType.Greater, CType(DBRowStatus.Active, Short)))

        '-- Category
        If ddlCategory.SelectedValue <> "" Then
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman_Download), "CategoryID", MatchType.Exact, ddlCategory.SelectedValue))
        End If

        '-- Status
        If lboxStatus.SelectedIndex <> -1 Then
            Dim SelectedStatus As String = GetSelectedItem(lboxStatus)
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman_Download), "FakturStatus", MatchType.InSet, "(" & SelectedStatus & ")"))
        Else
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman_Download), "FakturStatus", MatchType.InSet, "('1','2','3','4')"))
        End If

        If chkValidPeriod.Checked Then
            '-- Periode Validasi
            Dim StartValid As New DateTime(CInt(icStartValid.Value.Year), CInt(icStartValid.Value.Month), CInt(icStartValid.Value.Day), 0, 0, 0)
            Dim EndValid As New DateTime(CInt(icEndValid.Value.Year), CInt(icEndValid.Value.Month), CInt(icEndValid.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman_Download), "FakturDate", MatchType.GreaterOrEqual, Format(StartValid, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman_Download), "FakturDate", MatchType.LesserOrEqual, Format(EndValid, "yyyy-MM-dd HH:mm:ss")))
        End If

        If chkSPK.Checked Then
            '-- Periode Konfirmasi
            Dim StartConfirm As New DateTime(CInt(icStartConfirm.Value.Year), CInt(icStartConfirm.Value.Month), CInt(icStartConfirm.Value.Day), 0, 0, 0)
            Dim EndConfirm As New DateTime(CInt(icEndConfirm.Value.Year), CInt(icEndConfirm.Value.Month), CInt(icEndConfirm.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman_Download), "SPKDate", MatchType.GreaterOrEqual, Format(StartConfirm, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman_Download), "SPKDate", MatchType.LesserOrEqual, Format(EndConfirm, "yyyy-MM-dd HH:mm:ss")))
        End If

        If chkValidation.Checked Then
            '-- Periode Konfirmasi
            Dim StartValidation As New DateTime(CInt(icStartValidation.Value.Year), CInt(icStartValidation.Value.Month), CInt(icStartValidation.Value.Day), 0, 0, 0)
            Dim EndValidation As New DateTime(CInt(icEndValidation.Value.Year), CInt(icEndValidation.Value.Month), CInt(icEndValidation.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman_Download), "FakturValidateTime", MatchType.GreaterOrEqual, Format(StartValidation, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman_Download), "FakturValidateTime", MatchType.LesserOrEqual, Format(EndValidation, "yyyy-MM-dd HH:mm:ss")))
        End If

        If chkOpenFaktur.Checked Then
            '-- Periode Konfirmasi
            Dim StartOpenFaktur As New DateTime(CInt(icStartOpen.Value.Year), CInt(icStartOpen.Value.Month), CInt(icStartOpen.Value.Day), 0, 0, 0)
            Dim EndOpenFaktur As New DateTime(CInt(icEndOpen.Value.Year), CInt(icEndOpen.Value.Month), CInt(icEndOpen.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman_Download), "InvoiceOpen", MatchType.GreaterOrEqual, Format(StartOpenFaktur, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman_Download), "InvoiceOpen", MatchType.LesserOrEqual, Format(EndOpenFaktur, "yyyy-MM-dd HH:mm:ss")))
        End If

        '--Faktur Confirmation date
        If chkConfirm.Checked Then
            '-- Periode Konfirmasi
            Dim StartKonfirmasiFaktur As New DateTime(CInt(icStartConfirmTime.Value.Year), CInt(icStartConfirmTime.Value.Month), CInt(icStartConfirmTime.Value.Day), 0, 0, 0)
            Dim EndKonfirmasiFaktur As New DateTime(CInt(icEndConfirmTime.Value.Year), CInt(icEndConfirmTime.Value.Month), CInt(icEndConfirmTime.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman_Download), "ConfirmTime", MatchType.GreaterOrEqual, Format(StartKonfirmasiFaktur, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman_Download), "ConfirmTime", MatchType.LesserOrEqual, Format(EndKonfirmasiFaktur, "yyyy-MM-dd HH:mm:ss")))
        End If

        'objDealer = Session("DEALER")
        'If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        '    criterias.opAnd(New Criteria(GetType(V_SPK_Salesman_Download), "DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        'End If

        '-- Dealer code
        If txtKodeDealer.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman_Download), "DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        End If

        '-- Salesman
        If txtSalesman.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman_Download), "SalesmanCode", MatchType.Exact, txtSalesman.Text.Trim()))
        End If

        '-- Retrieve recordset
        'Dim SPKSalesmanResultList As ArrayList '= New V_SPK_Salesman_DownloadFacade(User).RetrieveByCriteria(, sortColl)
        'SPKSalesmanResultList = New V_SPK_Salesman_DownloadFacade(User).RetrieveActiveList(dgInvoiceList.CurrentPageIndex + 1, dgInvoiceList.PageSize, _
        '        total, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection), criterias)

        '-- Store recordset into session for later use
        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(V_SPK_Salesman_Download), ViewState("currSortColumn"), ViewState("currSortDirection")))

        Dim SPKSalesmanDownloadList As ArrayList = New V_SPK_Salesman_DownloadFacade(User).Retrieve(criterias, sortColl)
        sessHelp.SetSession("SalesmanEvaluationList", SPKSalesmanDownloadList)

        Return SPKSalesmanDownloadList
    End Function


    Private Sub DoDownload(ByVal dataSPK As ArrayList)
        Dim sFileName As String
        sFileName = "Evaluasi Salesman [" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & "]"

        '-- Temp file must be a randomly named file!
        Dim SPKData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(SPKData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(SPKData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteSPKSalesman(sw, dataSPK)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls", True)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub WriteSPKSalesman(ByVal sw As StreamWriter, ByVal dataSPK As ArrayList)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        itemLine.Remove(0, itemLine.Length)  '-- Empty line
        itemLine.Append("EVALUASI SALESMAN")
        sw.WriteLine(itemLine)
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)
        sw.WriteLine(" ")


        '======SPK DETAIL=======
        If (dataSPK.Count > 0) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("NO" & tab)
            itemLine.Append("KODE DEALER" & tab)
            itemLine.Append("NAMA DEALER" & tab)
            itemLine.Append("ALAMAT DEALER" & tab)
            itemLine.Append("KOTA DEALER" & tab)
            '---------start 20130429 added by anh req by angga
            itemLine.Append("GROUP DEALER" & tab)
            itemLine.Append("AREA DEALER" & tab)
            '---------end 20130429 added by anh req by angga
            itemLine.Append("CABANG DEALER" & tab) 'added by anh 20130503
            itemLine.Append("KATEGORI SF TIM" & tab)
            itemLine.Append("KODE SALESMAN" & tab)
            itemLine.Append("NAMA SALESMAN" & tab)
            itemLine.Append("LEVEL SALESMAN" & tab)
            itemLine.Append("TANGGAL MASUK" & tab)
            '---------start 20130429 added by anh req by angga
            itemLine.Append("JOB POSITION" & tab)
            '---------end 20130429 added by anh req by angga
            itemLine.Append("SUPERVISOR" & tab)
            itemLine.Append("TANGGAL FAKTUR (DD/MM/YYYY)" & tab)
            itemLine.Append("TANGGAL VALIDASI FAKTUR (DD/MM/YYYY)" & tab)
            itemLine.Append("TANGGAL BUKA FAKTUR (DD/MM/YYYY)" & tab)
            itemLine.Append("TANGGAL KONFIRMASI FAKTUR (DD/MM/YYYY)" & tab)
            itemLine.Append("NO. FAKTUR" & tab)
            itemLine.Append("TANGGAL SPK (DD/MM/YYYY)" & tab)
            itemLine.Append("STATUS FAKTUR" & tab)
            itemLine.Append("NOMOR SPK" & tab)
            itemLine.Append("NOMOR SPK DEALER" & tab) 'added by anh 20130516
            itemLine.Append("NO. RANGKA" & tab)
            itemLine.Append("KATEGORI" & tab)
            itemLine.Append("NAMA KENDARAAN" & tab)
            itemLine.Append("NAMA CUSTOMER" & tab)
            itemLine.Append("NO TELP" & tab)
            itemLine.Append("ALAMAT" & tab)
            itemLine.Append("KELURAHAN" & tab)
            itemLine.Append("KECAMATAN" & tab)
            itemLine.Append("KOTA" & tab)

            sw.WriteLine(itemLine.ToString())
            Dim i As Integer = 1
            Try
                For Each item As V_SPK_Salesman_Download In dataSPK
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(i.ToString & tab)
                    itemLine.Append(item.DealerCode.ToString & tab)
                    itemLine.Append(item.DealerName & tab)
                    itemLine.Append(item.Address & tab)
                    itemLine.Append(item.CityName & tab)
                    itemLine.Append(item.GroupName & tab) '--GROUP DEALER
                    itemLine.Append(item.DealerArea & tab) '--AREA DEALER
                    itemLine.Append(item.DealerBranchName & tab) 'added by anh 20130503
                    itemLine.Append(item.CategoryTeam & tab)
                    itemLine.Append(item.SalesmanCode & tab)
                    itemLine.Append(item.SalesmanName & tab)
                    itemLine.Append(item.Level & tab)
                    itemLine.Append(item.HireDate.ToString("dd/MM/yyyy") & tab)
                    itemLine.Append(item.JobPosition & tab) '--JOB POSITION
                    itemLine.Append(item.LeaderName & tab)
                    itemLine.Append(item.FakturDate.ToString("dd/MM/yyyy") & tab)
                    itemLine.Append(item.FakturValidateTime.ToString("dd/MM/yyyy") & tab)
                    itemLine.Append(item.InvoiceOpen.ToString("dd/MM/yyyy") & tab)
                    itemLine.Append(item.ConfirmTime.ToString("dd/MM/yyyy") & tab) 'TGL Konfirmasi
                    itemLine.Append(item.FakturNumber & tab)
                    itemLine.Append(item.SPKDate.ToString("dd/MM/yyyy") & tab)
                    itemLine.Append(CType(CInt(item.FakturStatus), EnumChassisMaster.FakturStatus).ToString & tab)
                    itemLine.Append(item.SPKNumber & tab)
                    itemLine.Append(item.DealerSPKNumber & tab) 'added by anh 20130516
                    itemLine.Append(item.ChassisNumber & tab)
                    itemLine.Append(item.CategoryCode & tab)
                    itemLine.Append(item.MaterialDescription & tab)
                    itemLine.Append(item.CustomerName & tab)
                    itemLine.Append(item.PhoneNo & tab)
                    itemLine.Append(item.Alamat & tab)
                    itemLine.Append(item.Kelurahan & tab)
                    itemLine.Append(item.Kecamatan & tab)
                    itemLine.Append(item.Kota & tab)

                    sw.WriteLine(itemLine.ToString())
                    i = i + 1
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If

        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)

    End Sub
#End Region




End Class
