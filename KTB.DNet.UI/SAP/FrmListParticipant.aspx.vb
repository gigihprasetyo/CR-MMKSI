#Region "Custom Namespace"
Imports ktb.DNet.UI.Helper
Imports ktb.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SAP
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
#End Region

Public Class FrmListParticipant
    Inherits System.Web.UI.Page

#Region "Deklarasi"
    Private sHelper As New SessionHelper
    Private criterias As CriteriaComposite
    Protected WithEvents hdnFieldTemp As System.Web.UI.HtmlControls.HtmlInputHidden
#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtSAPNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSAPNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblStartPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents lblEndPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents dtgSAPList As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Method"
    Private Sub BindToGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer

        If indexPage >= 0 Then

            CreateCriteria()
            Dim arlSAPPart As ArrayList = New SAPRegisterFacade(User).RetrieveActiveList(indexPage + 1, dtgSAPList.PageSize, totalRow, viewstate("currSortColLP"), viewstate("currSortDirLP"), criterias)
            If arlSAPPart.Count > 0 Then
                dtgSAPList.DataSource = arlSAPPart
                dtgSAPList.VirtualItemCount = totalRow
            Else
                dtgSAPList.DataSource = arlSAPPart
                MessageBox.Show("Data Tidak ditermukan")
            End If

            If indexPage = 0 Then
                dtgSAPList.CurrentPageIndex = 0
            End If

            dtgSAPList.DataBind()
        End If
    End Sub

    Private Sub CreateCriteria()
        criterias = New CriteriaComposite(New Criteria(GetType(SAPRegister), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SAPRegister), "SAPPeriod.SAPNumber", MatchType.Exact, txtSAPNo.Text.Trim))

        'Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'If txtDealerCode.Text <> String.Empty Then
        '    crits.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Trim))
        'End If

        'If ddlKategori.SelectedIndex <> 0 Then
        '    crits.opAnd(New Criteria(GetType(SalesmanHeader), "JobPosition.ID", MatchType.Exact, ddlKategori.SelectedValue))
        'End If

        'Dim arlSalesHeader As ArrayList = New SalesmanHeaderFacade(User).Retrieve(crits)
        'Dim strSalesHeaderId As String = ""
        'For Each item As SalesmanHeader In arlSalesHeader
        '    strSalesHeaderId &= item.ID & ","
        'Next

        'If strSalesHeaderId <> "" Then
        '    strSalesHeaderId = Left(strSalesHeaderId, strSalesHeaderId.Length - 1)
        'Else
        '    strSalesHeaderId = "0"
        'End If

        'Todo Inset
        'criterias.opAnd(New Criteria(GetType(SAPRegister), "SalesmanHeader.ID", MatchType.InSet, "(" & strSalesHeaderId & ")"))
        If txtDealerCode.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SAPRegister), "SalesmanHeader.Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Trim))
            'crits.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Trim))
        End If

        If ddlKategori.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(SAPRegister), "SalesmanHeader.JobPosition.ID", MatchType.Exact, ddlKategori.SelectedValue))
            'crits.opAnd(New Criteria(GetType(SalesmanHeader), "JobPosition.ID", MatchType.Exact, ddlKategori.SelectedValue))
        End If

    End Sub
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ParticipantListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Participant")
        End If
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("Menu")) Then
                Dim iMenu As Integer = CType(Request.QueryString("Menu"), Integer)
                If iMenu > 0 Then
                    CommonFunction.BindJobPositionByMenuAssigned(ddlKategori, User, True, iMenu)
                End If
            Else
                CommonFunction.BindJobPosition(ddlKategori, User, True, False)
            End If

            ViewState("currSortColLP") = "ID"
            ViewState("currSortDirLP") = Sort.SortDirection.ASC

            Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
            If objUserInfo.Dealer.Title <> 1 Then
                lblDealerCode.Attributes.Add("onclick", "")
                lblDealerCode.Visible = False
                lblSAPNo.Attributes.Add("onclick", "ShowSAPSelection();")
                txtDealerCode.Text = objUserInfo.Dealer.DealerCode
                txtDealerCode.Enabled = False
            Else
                lblDealerCode.Visible = True
                lblDealerCode.Attributes.Add("onclick", "ShowPPDealerSelection();")
                lblSAPNo.Attributes.Add("onclick", "ShowSAPSelection();")
                txtDealerCode.Enabled = True
            End If
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        If txtSAPNo.Text = String.Empty Then
            MessageBox.Show("Masukkan No SAP terlebih dahulu")
        Else
            If hdnFieldTemp.Value <> "" Then
                Dim strData() As String = hdnFieldTemp.Value.Split(";")
                Dim strStartPeriod As String = strData(0)
                Dim strEndPeriod As String = strData(1)

                lblStartPeriod.Text = strStartPeriod
                lblEndPeriod.Text = strEndPeriod

                dtgSAPList.CurrentPageIndex = 0
                BindToGrid(dtgSAPList.CurrentPageIndex)
            Else
                Dim sapNo As String = txtSAPNo.Text.Trim
                Dim objSAPPeriod As SAPPeriod = New SAPPeriodFacade(User).Retrieve(sapNo)
                If Not objSAPPeriod Is Nothing Then
                    lblStartPeriod.Text = objSAPPeriod.StartDate
                    lblEndPeriod.Text = objSAPPeriod.EndDate

                    dtgSAPList.CurrentPageIndex = 0
                    BindToGrid(dtgSAPList.CurrentPageIndex)
                Else
                    MessageBox.Show("No SAP " & sapNo & " tidak ditemukan")
                End If
            End If
        End If
    End Sub

    Private Sub dtgSAPList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSAPList.PageIndexChanged
        dtgSAPList.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgSAPList.CurrentPageIndex)
    End Sub

    Private Sub dtgSAPList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSAPList.SortCommand
        If CType(ViewState("currSortColLP"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirLP"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirLP") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currSortDirLP") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColLP") = e.SortExpression
            ViewState("currSortDirLP") = Sort.SortDirection.ASC
        End If
        BindToGrid(dtgSAPList.CurrentPageIndex)
    End Sub

    Private Sub dtgSAPList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSAPList.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dtgSAPList.PageSize * dtgSAPList.CurrentPageIndex)
        End If
    End Sub
End Class
