#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.ShowroomAudit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
'Imports System.IO
'Imports System.Text
'Imports System.Configuration
#End Region

Public Class FrmListAuditSchedule
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtAuditNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblAuditSearch As System.Web.UI.WebControls.Label
    Protected WithEvents dtgAuditScheduleDealer As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlPeriode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents btnRilis As System.Web.UI.WebControls.Button
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtAuditCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtUser As System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Private Const VS_CurrentSortColumn As String = "VS_CurrentSortColumn"
    Private Const VS_CurrentSortDirect As String = "VS_CurrentSortDirect"
#End Region

#Region "Variables"
    Dim _sHelper As New SessionHelper
#End Region

#Region "Properties"
    Private Property CurrentSortColumn() As String
        Get
            Dim val As Object = ViewState(VS_CurrentSortColumn)
            If (val Is Nothing) Then
                val = "Dealer.ID"
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

    Private Property SesDealer() As Dealer
        Get
            Return CType(_sHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            _sHelper.SetSession("DEALER", Value)
        End Set
    End Property
#End Region

#Region "Custom Method"

    Private Sub BindDropdownPeriode()

        Dim i As Integer
        Dim yearMax As Integer = Year(Date.Now) + 1
        For i = Year(Date.Now) To yearMax
            ddlPeriode.Items.Add(i.ToString)
        Next
        ddlPeriode.SelectedIndex = -1

    End Sub

    Private Function cleanStringForInsetOperator(ByVal str As String)
        str = str.Replace(";", ",").Replace("~", "").Replace("!", "").Replace("`", "").Replace(".", "")
        str = str.Replace("!", "").Replace("@", "").Replace("#", "").Replace("$", "").Replace("%", "")
        str = str.Replace("^", "").Replace("&", "").Replace("*", "").Replace("(", "").Replace(")", "")
        str = str.Replace("_", "").Replace("-", "").Replace("+", "").Replace("=", "").Replace("|", "")
        str = str.Replace("\", "").Replace(">", "").Replace("<", "").Replace("?", "").Replace("/", "")

        Return str
    End Function

    Private Function FindData(ByVal indexPage As Integer) As ArrayList
        'First, we have to find all audit schedule that match the supplied criteria
        Dim criteriaForAuditSchedule As CriteriaComposite
        criteriaForAuditSchedule = New CriteriaComposite(New Criteria(GetType(AuditSchedule), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If ddlPeriode.SelectedIndex <> -1 Then
            criteriaForAuditSchedule.opAnd(New Criteria(GetType(AuditSchedule), "AuditParameter.Period", MatchType.Exact, ddlPeriode.SelectedValue))
        End If

        If txtAuditCode.Text <> "" Then
            criteriaForAuditSchedule.opAnd(New Criteria(GetType(AuditSchedule), "AuditParameter.Code", MatchType.InSet, "('" & txtAuditCode.Text.Replace(";", "','") & "')"))
        End If

        Dim arlAuditSchedule As ArrayList = New AuditScheduleFacade(User).Retrieve(criteriaForAuditSchedule)


        'If found, then build a list of audit schedule id so we can use
        'InSet criteria to search for AuditScheduleDealer
        Dim strAuditScheduleIds As String = String.Empty
        For i As Integer = 0 To arlAuditSchedule.Count - 1
            If strAuditScheduleIds.Length > 0 Then
                strAuditScheduleIds += ","
            End If

            Dim item As AuditSchedule = CType(arlAuditSchedule(i), AuditSchedule)

            strAuditScheduleIds += item.ID.ToString()
        Next

        'Prepare an empty array list, so if no audit schedule found
        'just bind the grid with this empty array list
        Dim arlAllData As ArrayList = New ArrayList
        If (strAuditScheduleIds.Trim().Length > 0) Then
            strAuditScheduleIds = "(" + strAuditScheduleIds + ")"

            Dim criteriaForAuditScheduleDealer As CriteriaComposite
            criteriaForAuditScheduleDealer = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriaForAuditScheduleDealer.opAnd(New Criteria(GetType(AuditScheduleDealer), "AuditSchedule.ID", MatchType.InSet, strAuditScheduleIds))

            'Add dealer code criteria if supplied
            '--modified by ronny
            Dim dealerCode As String = String.Empty
            If IsLoginAsDealer() Then
                dealerCode = SesDealer.DealerCode
            Else
                dealerCode = txtDealerCode.Text.Trim().Replace(";", "','")
            End If
            If (dealerCode.Length > 0) Then
                criteriaForAuditScheduleDealer.opAnd(New Criteria(GetType(AuditScheduleDealer), "Dealer.DealerCode", MatchType.InSet, "('" & dealerCode & "')"))
                '    dealerCode = "(" + cleanStringForInsetOperator(dealerCode) + ")"

                '    criteriaForAuditScheduleDealer.opAnd(New Criteria(GetType(AuditScheduleDealer), "Dealer.DealerCode", MatchType.InSet, dealerCode))
            End If
            '---end modified

            'Search AuditScheduleDealer
            arlAllData = New AuditScheduleDealerFacade(User).Retrieve(criteriaForAuditScheduleDealer, CurrentSortColumn, CurrentSortDirection)
            _sHelper.SetSession("ddlPeriodeValue", ddlPeriode.SelectedValue)
            _sHelper.SetSession("txtDealerCodeValue", txtDealerCode.Text)
            _sHelper.SetSession("CriteriasAudit", criteriaForAuditScheduleDealer)
            _sHelper.SetSession("GridPageIndexBack", indexPage)
            _sHelper.SetSession("CurrSortColBack", CurrentSortColumn)
            _sHelper.SetSession("currSortDirCLnBack", CurrentSortDirection)
        End If

        Return arlAllData
    End Function

    Private Sub BindDataToGrid(ByVal indexPage As Integer)
        If indexPage >= 0 Then
            Dim arlAllData As ArrayList = CType(_sHelper.GetSession("arlAllData"), ArrayList)
            dtgAuditScheduleDealer.DataSource = arlAllData
            dtgAuditScheduleDealer.VirtualItemCount = arlAllData.Count

            dtgAuditScheduleDealer.DataBind()

            If Not IsLoginAsDealer() Then
                btnRilis.Visible = arlAllData.Count > 0
            End If

        End If
    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Private Sub bindToGrid(ByVal idx As Integer, ByVal criterias As CriteriaComposite)
        Dim arlAlldata As ArrayList = New ArrayList
        Dim totalRow As Integer = 0

        arlAlldata = New AuditScheduleDealerFacade(User).RetrieveActiveList(criterias, idx + 1, dtgAuditScheduleDealer.PageSize, _
        totalRow, CurrentSortColumn, CurrentSortDirection)

        _sHelper.SetSession("CriteriasAudit", criterias)
        _sHelper.SetSession("GridPageIndexBack", idx)
        _sHelper.SetSession("CurrSortColBack", CurrentSortColumn)
        _sHelper.SetSession("currSortDirCLnBack", CurrentSortDirection)

        _sHelper.SetSession("arlAllData", arlAlldata)
        dtgAuditScheduleDealer.DataSource = arlAlldata
        dtgAuditScheduleDealer.VirtualItemCount = arlAlldata.Count
        dtgAuditScheduleDealer.CurrentPageIndex = idx
        dtgAuditScheduleDealer.DataBind()

        If Not IsLoginAsDealer() Then
            btnRilis.Visible = arlAlldata.Count > 0
        End If
    End Sub
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ScheduleListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SHOWROOM AUDIT- Daftar Jadwal")
        End If
    End Sub

    Private Function CekListInputAuditorPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.ScheduleListInputAuditor_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekListResultPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.ScheduleListResult_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekListDeletePrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.ScheduleListDelete_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

    Dim objDealer As Dealer
#Region "Control Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        objDealer = CType(Session("DEALER"), Dealer)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If (Not IsPostBack) Then
            BindDropdownPeriode()

            Dim qsIsBack As Integer = CInt(Request.QueryString("isBack"))
            If qsIsBack = 1 Then
                ddlPeriode.SelectedValue = _sHelper.GetSession("ddlPeriodeValue")
                txtDealerCode.Text = _sHelper.GetSession("txtDealerCodeValue")
                bindToGrid(CInt(_sHelper.GetSession("GridPageIndexBack")), _sHelper.GetSession("CriteriasAudit"))
                CurrentSortColumn = CType(_sHelper.GetSession("CurrSortColBack"), String)
                CurrentSortDirection = CType(_sHelper.GetSession("currSortDirCLnBack"), Sort.SortDirection)

            Else
                CurrentSortColumn = "ID"
                CurrentSortDirection = Sort.SortDirection.ASC
                dtgAuditScheduleDealer.CurrentPageIndex = 0
                Dim arlAllData As ArrayList = FindData(dtgAuditScheduleDealer.CurrentPageIndex)
                _sHelper.SetSession("arlAllData", arlAllData)
                BindDataToGrid(dtgAuditScheduleDealer.CurrentPageIndex)

            End If

            If IsLoginAsDealer() Then
                txtUser.Value = "Dealer"
                txtDealerCode.Visible = False

                lblSearchDealer.Visible = False

                btnRilis.Visible = False

                lblKodeDealer.Visible = True
                lblKodeDealer.Text = SesDealer.DealerCode + " - " + SesDealer.DealerName

                'Hide checkbox column
                dtgAuditScheduleDealer.Columns(0).Visible = False

                'Hide Kode Dealer column
                dtgAuditScheduleDealer.Columns(2).Visible = False
                'Hide Periode column
                dtgAuditScheduleDealer.Columns(4).Visible = False
            Else
                txtUser.Value = SesDealer.DealerCode '"KTB"
                'Hide Kode Audit column
                dtgAuditScheduleDealer.Columns(1).Visible = False
                'Hide File Pelaksana column
                dtgAuditScheduleDealer.Columns(5).Visible = False
                'Hide Item Penilaian column
                dtgAuditScheduleDealer.Columns(6).Visible = False

                txtDealerCode.Visible = True

                lblSearchDealer.Visible = True
                lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"

                btnRilis.Visible = True
                btnRilis.Attributes.Add("onclick", "return GetSelectedItem();")

                lblKodeDealer.Visible = False
            End If

            ' add security
            If Not CekListInputAuditorPrivilege() Then
                btnRilis.Enabled = False
            End If
            'If Not CekListResultPrivilege() Then
            '    btnRilis.Enabled = False
            'End If
        End If
    End Sub
    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        Try
            dtgAuditScheduleDealer.CurrentPageIndex = 0
            Dim arlAllData As ArrayList = FindData(dtgAuditScheduleDealer.CurrentPageIndex)
            _sHelper.SetSession("arlAllData", arlAllData)
            BindDataToGrid(dtgAuditScheduleDealer.CurrentPageIndex)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dtgAuditScheduleDealer_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgAuditScheduleDealer.PageIndexChanged
        dtgAuditScheduleDealer.CurrentPageIndex = e.NewPageIndex
        BindDataToGrid(dtgAuditScheduleDealer.CurrentPageIndex)
    End Sub

    Private Sub dtgAuditScheduleDealer_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgAuditScheduleDealer.SortCommand
        If e.SortExpression = CurrentSortColumn Then
            If CurrentSortDirection = Sort.SortDirection.ASC Then
                CurrentSortDirection = Sort.SortDirection.DESC
            Else
                CurrentSortDirection = Sort.SortDirection.ASC
            End If
        End If
        CurrentSortColumn = e.SortExpression
        BindDataToGrid(dtgAuditScheduleDealer.CurrentPageIndex)
    End Sub

    Private Sub btnRilis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRilis.Click

        'cek untuk melakukan rilis parameter
        Dim arlChecked As New ArrayList
        For Each item As DataGridItem In dtgAuditScheduleDealer.Items
            Dim objAuditScheduler As AuditScheduleDealer
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            Dim lblID As Label = CType(item.FindControl("lblID"), Label)
            If chk.Checked Then
                Dim id As Integer = CInt(lblID.Text)
                objAuditScheduler = New AuditScheduleDealerFacade(User).Retrieve(id)

                arlChecked.Add(objAuditScheduler)
            End If
        Next

        If (New AuditScheduleDealerFacade(User).UpdateTransaction(arlChecked) <> -1) Then
            MessageBox.Show("Data berhasil dirilis")
        Else
            MessageBox.Show("Data gagal dirilis")
        End If
    End Sub

    Private Sub dtgAuditScheduleDealer_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgAuditScheduleDealer.ItemCommand
        If e.CommandName.ToLower = "edit".ToLower Then
            If IsLoginAsDealer() Then
                Dim lblID As Label = CType(e.Item.FindControl("lblAuditParameterID"), Label)
                _sHelper.SetSession("IDEdit", lblid.Text)
                _sHelper.SetSession("isEditMode", "1")
                Response.Redirect("../ShowroomAudit/FrmParameterAudit.aspx", True)
            Else
                Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
                _sHelper.SetSession("IDEdit", lblid.Text)
                _sHelper.SetSession("isEditMode", "0")
                Response.Redirect("FrmAuditScheduleSingle.aspx")
            End If
        ElseIf e.CommandName.ToLower = "Delete".ToLower Then
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim objAuditScheduleDealer As AuditScheduleDealer = New AuditScheduleDealerFacade(User).Retrieve(CInt(lblid.Text))

            If (New AuditScheduleDealerFacade(User).Delete(objAuditScheduleDealer) <> -1) Then
                MessageBox.Show("Data berhasil dihapus")
                BindDataToGrid(dtgAuditScheduleDealer.CurrentPageIndex)
            Else
                MessageBox.Show("Data tidak berhasil dihapus")
            End If
        ElseIf e.CommandName.ToLower = "hasil".ToLower Then
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            _sHelper.SetSession("IDEdit", lblid.Text)
            Response.Redirect("FrmAuditAssesmentResult.aspx")
        End If
    End Sub

    Private Sub dtgAuditScheduleDealer_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgAuditScheduleDealer.ItemDataBound
        If e.Item.ItemIndex >= 0 Then
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnDetail As LinkButton = CType(e.Item.FindControl("lbtnDetail"), LinkButton)
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                lbtnEdit.Visible = False
                '12-Nov-2007    Deddy H     Fix bug 1431
                lbtnDelete.Visible = False
            End If

            'add security
            If Not CekListDeletePrivilege() Then
                lbtnDelete.Visible = False
            End If
            If Not CekListInputAuditorPrivilege() Then
                lbtnEdit.Visible = False
            End If

            If Not CekListResultPrivilege() Then
                lbtnDetail.Visible = False
            End If
        End If

    End Sub
#End Region


End Class
