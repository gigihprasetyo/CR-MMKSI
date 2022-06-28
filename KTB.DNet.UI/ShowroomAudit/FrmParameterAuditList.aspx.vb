#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.ShowroomAudit
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmParameterAuditList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlYearPeriod As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgParamAuditList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnRilis As System.Web.UI.WebControls.Button
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblAuditSearch As System.Web.UI.WebControls.Label
    Protected WithEvents txtAuditCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents pnldealer As System.Web.UI.WebControls.Panel

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Deklarasi"
    Dim criterias As CriteriaComposite
    Dim sHelper As New SessionHelper
#End Region

#Region "Custom Method"
    Private Sub CreateCriteria()
        criterias = New CriteriaComposite(New Criteria(GetType(AuditParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtAuditCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(AuditParameter), "Code", MatchType.InSet, "('" & txtAuditCode.Text.Replace(";", "','") & "')"))
        End If
        criterias.opAnd(New Criteria(GetType(AuditParameter), "Period", MatchType.Exact, ddlYearPeriod.SelectedValue))
        sHelper.SetSession("CRITERIAS", criterias)
    End Sub

    Private Sub BindYear()
        Dim i As Integer
        Dim yearMax As Integer = Year(Date.Now) + 5
        For i = Year(Date.Now) - 5 To yearMax
            ddlYearPeriod.Items.Add(i.ToString)
        Next
        ddlYearPeriod.SelectedIndex = -1
    End Sub
    'Private Sub CreateCriteria()
    '    criterias = New CriteriaComposite(New Criteria(GetType(AuditParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

    '    If txtAuditCode.Text <> "" Then
    '        criterias.opAnd(New Criteria(GetType(AuditParameter), "Code", MatchType.InSet, "('" & txtAuditCode.Text.Replace(";", "','") & "')"))
    '    End If
    '    criterias.opAnd(New Criteria(GetType(AuditParameter), "Period", MatchType.Exact, ddlYearPeriod.SelectedValue))
    'End Sub
    Private Sub BindDataToGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer
        If indexPage >= 0 Then

            'refer bug 1094
            'Dim dealerCode As String = String.Empty
            'If IsLoginAsDealer() Then
            '    dealerCode = SesDealer.DealerCode
            'Else
            '    dealerCode = txtDealerCode.Text.Trim().Replace(";", "','")
            'End If
            'Dim auditParameterIDs As String = String.Empty
            'If (dealerCode.Length > 0) Then
            'Dim critsForAuditScheduleDealer As New CriteriaComposite(New Criteria(GetType(AuditScheduleDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            'critsForAuditScheduleDealer.opAnd(New Criteria(GetType(AuditScheduleDealer), "Dealer.DealerCode", MatchType.InSet, "('" & dealerCode & "')"))
            'Dim arlAuditScheduleDealer As ArrayList = New AuditScheduleDealerFacade(User).Retrieve(critsForAuditScheduleDealer)

            'For Each itemDealer As AuditScheduleDealer In arlAuditScheduleDealer
            '    If Not itemDealer.AuditSchedule Is Nothing _
            '        And Not itemDealer.AuditSchedule.AuditParameter Is Nothing Then
            '        If auditParameterIDs.Length > 0 Then
            '            auditParameterIDs += ","
            '        End If
            '        auditParameterIDs += itemDealer.AuditSchedule.AuditParameter.ID.ToString()
            '    End If
            'Next
            ''End If

            'If auditParameterIDs.Length > 0 Then
            '    auditParameterIDs = "(" + auditParameterIDs + ")"
            '    criterias.opAnd(New Criteria(GetType(AuditParameter), "ID", MatchType.InSet, auditParameterIDs))
            'End If

            Dim arlAllData As ArrayList = New AuditParameterFacade(User).RetrieveActiveList(CType(sHelper.GetSession("CRITERIAS"), CriteriaComposite), indexPage + 1, dtgParamAuditList.PageSize, totalRow, viewstate("SortColPL"), viewstate("SortDirectionPL"))
            dtgParamAuditList.VirtualItemCount = totalRow
            If arlAllData.Count > 0 Then
                dtgParamAuditList.DataSource = arlAllData
            Else
                dtgParamAuditList.DataSource = New ArrayList
            End If
            If indexPage = 0 Then
                dtgParamAuditList.CurrentPageIndex = 0
            End If

            sHelper.SetSession("arlAllData", arlAllData)
            dtgParamAuditList.DataBind()
        End If
    End Sub
    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function
#End Region
#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.JuklakListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SHOWROOM AUDIT - Daftar Petunjuk Pelaksanaan")
        End If
    End Sub

    Private Function CekJuklakListReleasePrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.JuklakListRelease_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekJuklakListEditPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.JuklakListEdit_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

    Private Property SesDealer() As Dealer
        Get
            Return CType(sHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)

            sHelper.SetSession("DEALER", Value)
        End Set
    End Property

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            BindYear()
            viewstate.Add("SortColPL", "Code")
            viewstate.Add("SortDirectionPL", Sort.SortDirection.ASC)
            CreateCriteria()
            BindDataToGrid(0)

            btnRilis.Visible = True
            'refer bug 1094
            '------------------------------------
            'If IsLoginAsDealer() Then
            '    txtDealerCode.Visible = False

            '    lblSearchDealer.Visible = False

            '    btnRilis.Visible = False

            '    lblKodeDealer.Visible = True
            '    lblKodeDealer.Text = SesDealer.DealerCode + " - " + SesDealer.DealerName
            'Else
            '    txtDealerCode.Visible = True

            '    lblSearchDealer.Visible = True
            '    lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"

            '    btnRilis.Visible = True
            '    btnRilis.Attributes.Add("onclick", "return GetSelectedItem();")

            '    lblKodeDealer.Visible = False
            'End If
            '---------------------------------------

            ' add security
            If Not CekJuklakListReleasePrivilege() Then
                btnRilis.Enabled = False
            End If

            If Not CekJuklakListEditPrivilege() Then
                dtgParamAuditList.Columns(6).Visible = False    ' kolom edit
            End If
        End If
    End Sub

    Private Sub dtgParamAuditList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgParamAuditList.PageIndexChanged
        dtgParamAuditList.CurrentPageIndex = e.NewPageIndex
        BindDataToGrid(dtgParamAuditList.CurrentPageIndex)
    End Sub

    Private Sub dtgParamAuditList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgParamAuditList.SortCommand
        If e.SortExpression = viewstate("SortColPL") Then
            If viewstate("SortDirectionPL") = Sort.SortDirection.ASC Then
                viewstate.Add("SortDirectionPL", Sort.SortDirection.DESC)
            Else
                viewstate.Add("SortDirectionPL", Sort.SortDirection.ASC)
            End If
        End If
        viewstate.Add("SortColPL", e.SortExpression)
        dtgParamAuditList.SelectedIndex = -1
        dtgParamAuditList.CurrentPageIndex = 0
        BindDataToGrid(0)
    End Sub

    Private Sub dtgParamAuditList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgParamAuditList.ItemDataBound
        Dim arlAllData As ArrayList = sHelper.GetSession("arlAllData")
        If e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.Item Then
            Dim objAuditParam As AuditParameter = arlAllData(e.Item.ItemIndex)
            Dim chk As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (e.Item.ItemIndex + 1 + (dtgParamAuditList.PageSize * dtgParamAuditList.CurrentPageIndex)).ToString

            If objAuditParam.IsRilis = 1 Then
                'chk.Checked = True
                lbtnDelete.Visible = False
            End If

            'linkbutton
            Dim lbtnJuklakFile As LinkButton = CType(e.Item.FindControl("lbtnJuklakFile"), LinkButton)
            Dim lbtnItemScore As LinkButton = CType(e.Item.FindControl("lbtnItemScore"), LinkButton)

            lbtnJuklakFile.Text = objAuditParam.JukLakFile.Substring(objAuditParam.JukLakFile.LastIndexOf("\") + 1)
            lbtnItemScore.Text = objAuditParam.AssessmentItem.Substring(objAuditParam.AssessmentItem.LastIndexOf("\") + 1)
        End If
    End Sub

    Private Sub dtgParamAuditList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgParamAuditList.ItemCommand
        If e.CommandName = "DownloadJukLak" Then
            Dim strLink As String = e.CommandArgument
            Response.Redirect("../Download.aspx?file=" & strLink)
        ElseIf e.CommandName = "ItemScore" Then
            Dim strLink2 As String = e.CommandArgument
            Response.Redirect("../Download.aspx?file=" & strLink2)
        ElseIf e.CommandName = "Edit" Then
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            sHelper.SetSession("IDEdit", lblid.Text)
            Response.Redirect("FrmParameterAudit.aspx")
        ElseIf e.CommandName = "Delete" Then
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim objAuditParameter As AuditParameter = New AuditParameterFacade(User).Retrieve(CInt(lblid.Text))

            If (New AuditParameterFacade(User).Delete(objAuditParameter) <> -1) Then
                MessageBox.Show("Data berhasil dihapus")
                BindDataToGrid(0)
            Else
                MessageBox.Show("Data tidak berhasil dihapus")
            End If
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgParamAuditList.CurrentPageIndex = 0
        CreateCriteria()
        BindDataToGrid(dtgParamAuditList.CurrentPageIndex)
    End Sub

    Private Sub btnRilis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRilis.Click
        'cek untuk melakukan rilis parameter
        Dim arlChecked As New ArrayList
        For Each item As DataGridItem In dtgParamAuditList.Items
            Dim objAuditParam As New AuditParameter
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            Dim lblID As Label = CType(item.FindControl("lblID"), Label)
            If chk.Checked Then
                Dim id As Integer = CInt(lblID.Text)
                objAuditParam = New AuditParameterFacade(User).Retrieve(id)

                arlChecked.Add(objAuditParam)
            End If
        Next

        If (arlChecked.Count <> 0) Then
            If (New AuditParameterFacade(User).UpdateTransaction(arlChecked) <> -1) Then
                MessageBox.Show("Data berhasil dirilis")
                BindDataToGrid(0)
            Else
                MessageBox.Show("Data gagal dirilis")
            End If
        Else
            MessageBox.Show("Belum ada data yang dipilih")
        End If
    End Sub
End Class
