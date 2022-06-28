#Region "Custom Namespace Imports"
Imports Ktb.DNet.Domain
Imports Ktb.DNet.Domain.Search
Imports Ktb.DNet.BusinessFacade
Imports Ktb.DNet.BusinessFacade.SparePart
Imports Ktb.DNet.BusinessFacade.General
Imports Ktb.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region

Public Class FrmRegularOrderConfirmation
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtReason As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dgRegulerOrderConfirmation As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblJudulDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblJudulReason As System.Web.UI.WebControls.Label
    Protected WithEvents reqReason As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblDealerTanda As System.Web.UI.WebControls.Label
    Protected WithEvents lblTanggalTanda As System.Web.UI.WebControls.Label
    Protected WithEvents lblReasonTanda As System.Web.UI.WebControls.Label
    Protected WithEvents lblButtonTanda As System.Web.UI.WebControls.Label
    Protected WithEvents icConfirDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icSampai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblSampai As System.Web.UI.WebControls.Label

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
    Dim sesshelper As SessionHelper = New SessionHelper
    Dim ROCFacade As RegularOrderConfirmationFacade = New RegularOrderConfirmationFacade(User)
    Private _edit As Boolean
    Private _view As Boolean
#End Region

#Region "PrivateCustomMethods"
    Private Function ROCByDealerCode(ByVal sDealerCode As String) As ArrayList
        Dim arrList As New ArrayList
        Dim startDate As DateTime = New DateTime(icConfirDate.Value.Year, icConfirDate.Value.Month, icConfirDate.Value.Day, 0, 0, 0)
        Dim endDate As DateTime = New DateTime(icConfirDate.Value.Year, icConfirDate.Value.Month, icConfirDate.Value.Day, 23, 59, 59)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(RegularOrderConfirmation), "RowStatus", CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(RegularOrderConfirmation), "Dealer.DealerCode", sDealerCode))
        criterias.opAnd(New Criteria(GetType(RegularOrderConfirmation), "ConfirmationDate", MatchType.GreaterOrEqual, startDate))
        criterias.opAnd(New Criteria(GetType(RegularOrderConfirmation), "ConfirmationDate", MatchType.LesserOrEqual, endDate))
        arrList = ROCFacade.Retrieve(criterias)
        Return arrList
    End Function
    Private Sub EnableControl(ByVal isEnable As Boolean)
        txtDealerCode.ReadOnly = Not isEnable
        txtReason.ReadOnly = Not isEnable
        icConfirDate.Enabled = isEnable
        If isEnable Then
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection()"
        Else
            lblSearchDealer.Attributes("onclick") = ""
        End If
    End Sub
    Private Sub ViewROC(ByVal id As Integer)
        Dim ObjROC As RegularOrderConfirmation = ROCFacade.Retrieve(id)
        txtDealerCode.Text = ObjROC.Dealer.DealerCode
        icConfirDate.Value = ObjROC.ConfirmationDate
        txtReason.Text = ObjROC.Reason
    End Sub
    Private Function Update(ByVal ObjROC As RegularOrderConfirmation) As Integer
        Dim result As Integer

        ObjROC.Reason = txtReason.Text.Trim()
        ObjROC.ConfirmationDate = New DateTime(icConfirDate.Value.Year, icConfirDate.Value.Month, icConfirDate.Value.Day, 0, 0, 0)
        result = ROCFacade.Update(ObjROC)
        Return result
    End Function
    Private Function Insert(ByVal sDealerCode As String) As Integer
        Dim result As Integer
        Dim ObjROC As RegularOrderConfirmation = New RegularOrderConfirmation
        Dim _DealerFacade As DealerFacade = New DealerFacade(User)
        Dim ObjDealer As Dealer = _DealerFacade.Retrieve(sDealerCode)
        ObjROC.CreatedTime = Now
        ObjROC.LastUpdateTime = Now
        ObjROC.Dealer = ObjDealer
        ObjROC.Reason = txtReason.Text.Trim()
        ObjROC.ConfirmationDate = New DateTime(icConfirDate.Value.Year, icConfirDate.Value.Month, icConfirDate.Value.Day, 0, 0, 0)

        result = ROCFacade.Insert(ObjROC)
        Return result
    End Function
    Private Sub BindDataGrid()
        Dim totalRow As Integer = 0
        Dim crit As New CriteriaComposite(New Criteria(GetType(RegularOrderConfirmation), "RowStatus", CType(DBRowStatus.Active, Short)))
        If lblDealerCode.Visible Then
            crit.opAnd(New Criteria(GetType(RegularOrderConfirmation), "Dealer.DealerCode", lblDealerCode.Text.Split(" - ")(0).Trim))
            dgRegulerOrderConfirmation.DataSource = ROCFacade.RetrieveActiveList(crit, 1, dgRegulerOrderConfirmation.PageSize, totalRow, "Dealer.DealerCode", Sort.SortDirection.ASC)
        Else
            dgRegulerOrderConfirmation.DataSource = ROCFacade.RetrieveActiveList(1, dgRegulerOrderConfirmation.PageSize, totalRow, "Dealer.DealerCode", Sort.SortDirection.ASC)
        End If

        dgRegulerOrderConfirmation.VirtualItemCount = totalRow
        dgRegulerOrderConfirmation.DataBind()
    End Sub
    Private Sub BindDataGrid(ByVal indexPage As Integer)
        If indexPage >= 0 Then
            Dim totalRow As Integer = 0
            Dim arrList As ArrayList = New ArrayList
            Dim startDate As DateTime = New DateTime(icConfirDate.Value.Year, icConfirDate.Value.Month, icConfirDate.Value.Day, 0, 0, 0)
            Dim endDate As DateTime = New DateTime(icConfirDate.Value.Year, icConfirDate.Value.Month, icConfirDate.Value.Day, 23, 59, 59)

            Dim criterias As New CriteriaComposite(New Criteria(GetType(RegularOrderConfirmation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            If lblDealerCode.Visible Then
                criterias.opAnd(New Criteria(GetType(RegularOrderConfirmation), "Dealer.DealerCode", MatchType.Exact, lblDealerCode.Text.Split("-")(0).Trim()))
                startDate = New DateTime(icConfirDate.Value.Year, icConfirDate.Value.Month, icConfirDate.Value.Day, 0, 0, 0)
                endDate = New DateTime(icConfirDate.Value.Year, icConfirDate.Value.Month, icConfirDate.Value.Day, 23, 59, 59)
                criterias.opAnd(New Criteria(GetType(RegularOrderConfirmation), "ConfirmationDate", MatchType.GreaterOrEqual, startDate))
                criterias.opAnd(New Criteria(GetType(RegularOrderConfirmation), "ConfirmationDate", MatchType.LesserOrEqual, endDate))
            Else
                If txtDealerCode.Text.Length > 0 Then
                    criterias.opAnd(New Criteria(GetType(RegularOrderConfirmation), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
                End If
                startDate = New DateTime(icConfirDate.Value.Year, icConfirDate.Value.Month, icConfirDate.Value.Day, 0, 0, 0)
                endDate = New DateTime(icSampai.Value.Year, icSampai.Value.Month, icSampai.Value.Day, 23, 59, 59)
                criterias.opAnd(New Criteria(GetType(RegularOrderConfirmation), "ConfirmationDate", MatchType.GreaterOrEqual, startDate))
                criterias.opAnd(New Criteria(GetType(RegularOrderConfirmation), "ConfirmationDate", MatchType.LesserOrEqual, endDate))
            End If

            If txtReason.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(RegularOrderConfirmation), "Reason", MatchType.Exact, txtReason.Text.Trim()))
            End If

            arrList = ROCFacade.RetrieveActiveList(criterias, indexPage + 1, dgRegulerOrderConfirmation.PageSize, _
                totalRow, CType(sesshelper.GetSession("CurrentSortColumn"), String), CType(sesshelper.GetSession("CurrentSortDirect"), Sort.SortDirection))
            If arrList.Count > 0 Then
                dgRegulerOrderConfirmation.DataSource = arrList
                dgRegulerOrderConfirmation.VirtualItemCount = totalRow
                dgRegulerOrderConfirmation.DataBind()
            Else
                dgRegulerOrderConfirmation.DataSource = New ArrayList
                dgRegulerOrderConfirmation.VirtualItemCount = 0
                dgRegulerOrderConfirmation.DataBind()
                MessageBox.Show(SR.DataNotFound("Pemesanan"))
            End If
        End If
    End Sub
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.ENHKonfirmasiOrderView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PEMESANAN - Konfirmasi Pemesanan PO")
        End If
        _edit = SecurityProvider.Authorize(context.User, SR.ENHKonfirmasiOrderEdit_Privilege)
        _view = SecurityProvider.Authorize(context.User, SR.ENHKonfirmasiOrderView_Privilege)

        Dim ObjOrg As Dealer = CType(Session("DEALER"), Dealer)
        If ObjOrg.Title = EnumDealerTittle.DealerTittle.DEALER Then
            btnSave.Visible = _edit
        End If

    End Sub
    Private Sub Initialize()
        Dim ObjOrg As Dealer = CType(Session("DEALER"), Dealer)
        If ObjOrg.Title = EnumDealerTittle.DealerTittle.DEALER Then
            lblDealerCode.Visible = True
            lblDealerCode.Text = ObjOrg.DealerCode & " - " & ObjOrg.DealerName

            txtDealerCode.Visible = False
            lblSearchDealer.Visible = False

            lblJudulDealer.Visible = True
            lblSampai.Visible = False
            icSampai.Visible = False

            btnCancel.Visible = True

            lblJudulReason.Visible = True
            txtReason.Visible = True
            reqReason.Visible = True
        Else
            lblJudulDealer.Visible = True
            lblDealerTanda.Visible = True
            lblDealerCode.Visible = False
            txtDealerCode.Visible = True
            lblSearchDealer.Visible = True

            lblSampai.Visible = True
            icSampai.Visible = True

            btnSave.Visible = False
            btnCancel.Visible = False

            lblReasonTanda.Visible = False
            lblJudulReason.Visible = False
            txtReason.Visible = False
            reqReason.Visible = False
        End If
        txtDealerCode.Text = ""
        icConfirDate.Value = Date.Now
        icSampai.Value = Date.Now
        txtReason.Text = ""
        sesshelper.SetSession("CurrentSortColumn", "Dealer.DealerCode")
        sesshelper.SetSession("CurrentSortDirect", Sort.SortDirection.ASC)
        sesshelper.SetSession("Status", "Insert")
        EnableControl(True)
        btnSave.Enabled = True
        btnCancel.Enabled = True
        btnSearch.Enabled = True
    End Sub
#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        If Not IsPostBack Then
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection()"
            Initialize()
            dgRegulerOrderConfirmation.DataSource = New ArrayList
            dgRegulerOrderConfirmation.DataBind()
        End If
    End Sub
    Private Sub dgRegulerOrderConfirmation_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgRegulerOrderConfirmation.ItemDataBound
        Dim ObjOrg As Dealer = CType(Session("DEALER"), Dealer)
        If ObjOrg.Title = EnumDealerTittle.DealerTittle.DEALER Then
            e.Item.Cells(2).Visible = False
        Else
            e.Item.Cells(2).Visible = True
        End If
        If Not e.Item.ItemIndex = -1 Then

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgRegulerOrderConfirmation.CurrentPageIndex * dgRegulerOrderConfirmation.PageSize)
            e.Item.Cells(2).Text = CType(e.Item.DataItem, RegularOrderConfirmation).Dealer.DealerCode
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
            
            If ObjOrg.Title = EnumDealerTittle.DealerTittle.DEALER Then
                e.Item.Cells(2).Visible = False
                CType(e.Item.FindControl("lbtnView"), LinkButton).Visible = _view
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = _edit
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = _edit
            Else
                e.Item.Cells(2).Visible = True
                CType(e.Item.FindControl("lbtnView"), LinkButton).Visible = False
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = True
            End If
        End If
    End Sub
    Private Sub dgRegulerOrderConfirmation_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgRegulerOrderConfirmation.PageIndexChanged
        dgRegulerOrderConfirmation.SelectedIndex = -1
        dgRegulerOrderConfirmation.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgRegulerOrderConfirmation.CurrentPageIndex)
    End Sub
    Private Sub dgRegulerOrderConfirmation_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgRegulerOrderConfirmation.SortCommand
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

        dgRegulerOrderConfirmation.SelectedIndex = -1
        dgRegulerOrderConfirmation.CurrentPageIndex = 0
        BindDataGrid(dgRegulerOrderConfirmation.CurrentPageIndex)
    End Sub
    Private Sub dgRegulerOrderConfirmation_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgRegulerOrderConfirmation.ItemCommand
        If (e.CommandName = "View") Then
            EnableControl(False)
            ViewROC(CType(e.Item.Cells(0).Text, Integer))
            btnSave.Enabled = False
            btnSearch.Enabled = False
        ElseIf e.CommandName = "Edit" Then
            EnableControl(True)
            ViewROC(CType(e.Item.Cells(0).Text, Integer))
            btnSave.Enabled = True
            btnSearch.Enabled = False
            sesshelper.SetSession("IDROC", CType(e.Item.Cells(0).Text, Integer))
            sesshelper.SetSession("Status", "Update")
        ElseIf e.CommandName = "Delete" Then
            Try
                Dim objROC As RegularOrderConfirmation = ROCFacade.Retrieve(CType(e.Item.Cells(0).Text, Integer))
                ROCFacade.Delete(objROC)
                MessageBox.Show(SR.DeleteSucces)
                Initialize()
                BindDataGrid()
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If CType(sesshelper.GetSession("Status"), String) = "Insert" Then
            Dim sDealerCode As String
            Dim arrList As ArrayList
            If lblDealerCode.Visible Then
                sDealerCode = lblDealerCode.Text.Trim().Split(" - ")(0).Trim()
                arrList = ROCByDealerCode(sDealerCode)
                If arrList.Count > 0 Then
                    If Not Update(CType(arrList(0), RegularOrderConfirmation)) = -2 Then
                        'Initialize()
                        'BindDataGrid()
                        MessageBox.Show(SR.DataIsExist("Regular Order Konfirmasi"))
                    Else
                        MessageBox.Show(SR.SaveFail)
                    End If
                Else
                    If Insert(sDealerCode) = -2 Then
                        Initialize()
                        BindDataGrid()
                        MessageBox.Show(SR.SaveSuccess)
                    Else
                        MessageBox.Show(SR.SaveFail)
                    End If
                End If
            Else
                Dim asDealerCode() As String = txtDealerCode.Text.Split(";")
                Try
                    For i As Integer = 0 To asDealerCode.Length - 1
                        sDealerCode = asDealerCode(i)
                        arrList = ROCByDealerCode(sDealerCode)
                        If arrList.Count > 0 Then
                            Update(CType(arrList(0), RegularOrderConfirmation))
                        Else
                            Insert(sDealerCode)
                        End If
                    Next i
                    Initialize()
                    BindDataGrid()
                    MessageBox.Show(SR.SaveSuccess)
                Catch ex As Exception
                    MessageBox.Show(SR.SaveFail)
                End Try
            End If
        ElseIf CType(sesshelper.GetSession("Status"), String) = "Update" Then
            Dim ObjROC As RegularOrderConfirmation = ROCFacade.Retrieve(CType(sesshelper.GetSession("IDROC"), Integer))
            If Not Update(ObjROC) = -2 Then
                Initialize()
                BindDataGrid()
                MessageBox.Show(SR.SaveSuccess)
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        End If
    End Sub
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Initialize()
        'BindDataGrid()
    End Sub
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgRegulerOrderConfirmation.CurrentPageIndex = 0
        BindDataGrid(0)
    End Sub
#End Region

End Class
