#Region "DNet Namespace Imports"
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.CallCenter
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class FrmPreviewCustomer
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlKategoriKonsumen As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlJenisKendaraan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents valDealer As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlKotaDealer As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAreaDealer As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtKonsumen As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnShow As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents dgReports As System.Web.UI.WebControls.DataGrid
    'Protected WithEvents fraDownload As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents fraDownload As System.Web.UI.HtmlControls.HtmlIframe
    Protected WithEvents hdnValSave As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents ddlGroupDealer As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblASS As System.Web.UI.WebControls.Label
    Protected WithEvents lblMessage As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Declaration"
    Private sessHelper As New SessionHelper
#End Region

#Region "Event"

#End Region
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        lblMessage.Text = ""
        If Not IsPostBack Then
            BindPeriod()
            BindDealer()
            BindKategoriKonsumen()
            BindJenisKendaraan()
        End If

    End Sub

    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        dgReports.CurrentPageIndex = 0
        BindDataReport(dgReports.CurrentPageIndex)
    End Sub

    Private Sub dgReports_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgReports.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (dgReports.CurrentPageIndex * dgReports.PageSize + e.Item.ItemIndex + 1).ToString()
        End If
    End Sub

#Region "Custom"

    Private Sub BindPeriod()
        Try
            ddlMonth.Items.Clear()
            ddlMonth.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
            For Each item As ListItem In CType(LookUp.ArrayMonth(), IEnumerable)
                item.Selected = False
                ddlMonth.Items.Add(item)
            Next
            ddlMonth.ClearSelection()
            ddlMonth.SelectedIndex = CType(Format(DateTime.Now, "MM").ToString, Integer)
        Catch ex As Exception
            MessageBox.Show("Error binding data bulan, silahkan kirim error ini ke dnet admin")
        End Try

        '--DropDownList Faktur Year
        Try
            ddlYear.Items.Clear()
            ddlYear.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
            For Each item As ListItem In CType(LookUp.ArrayYear(True, 2, 8, Date.Now.Year.ToString), IEnumerable)
                item.Selected = False
                ddlYear.Items.Add(item)
            Next
            ddlYear.ClearSelection()
            ddlYear.SelectedValue = Format(DateTime.Now, "yyyy").ToString
        Catch ex As Exception
            MessageBox.Show("Error binding data tahun, silahkan kirim error ini ke dnet admin")
        End Try

    End Sub

    Private Sub BindDealer()
        Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If Not IsNothing(objuser) Then
            lblDealer.Text = objuser.Dealer.DealerCode & " - " & objuser.Dealer.DealerName
        End If
    End Sub

    Private Sub BindKategoriKonsumen()
        ddlKategoriKonsumen.Items.Clear()

        Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcCustomerCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critCol.opAnd(New Criteria(GetType(CcCustomerCategory), "Code", MatchType.Exact, "ASS"))
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(CcCustomerCategory), "ID", Sort.SortDirection.ASC))
        Dim objReportList As ArrayList = New CcCustomerCategoryFacade(User).Retrieve(critCol, sortCol)

        Dim li As ListItem
        For Each objReport As CcCustomerCategory In objReportList
            li = New ListItem(objReport.Description, objReport.ID.ToString)
            ddlKategoriKonsumen.Items.Add(li)
        Next

    End Sub

    Private Sub BindJenisKendaraan()
        ddlJenisKendaraan.Items.Clear()

        Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcVehicleCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(CcVehicleCategory), "ID", Sort.SortDirection.ASC))
        Dim objReportList As ArrayList = New CcVehicleCategoryFacade(User).Retrieve(critCol, sortCol)

        Dim li As ListItem
        For Each objReport As CcVehicleCategory In objReportList
            li = New ListItem(objReport.Description, objReport.ID.ToString)
            ddlJenisKendaraan.Items.Add(li)
        Next

        li = New ListItem("Semua", "0")
        ddlJenisKendaraan.Items.Insert(0, li)

    End Sub

    Private Sub BindDataReport(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arlReportDealer As New ArrayList
        Dim strPeriod As String = CType(ddlYear.SelectedValue.ToString & IIf(ddlMonth.SelectedValue.ToString.Length = 1, "0" & ddlMonth.SelectedValue.ToString, ddlMonth.SelectedValue.ToString), String)
        Dim objCcPeriod As CcPeriod = New CcPeriodFacade(User).Retrieve(strPeriod)
        If Not IsNothing(objCcPeriod) Then
            If (idxPage >= 0) Then
                Dim criterias As New CriteriaComposite(New Criteria(GetType(V_CcGridContact), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
                If Not IsNothing(objDealer) Then
                    criterias.opAnd(New Criteria(GetType(V_CcGridContact), "DealerID", MatchType.Exact, objDealer.ID))
                End If

                criterias.opAnd(New Criteria(GetType(V_CcGridContact), "CcCustomerCategoryID", MatchType.Exact, CInt(ddlKategoriKonsumen.SelectedValue)))
                If ddlJenisKendaraan.SelectedIndex > 0 Then
                    criterias.opAnd(New Criteria(GetType(V_CcGridContact), "CcVehicleCategoryID", MatchType.Exact, CInt(ddlJenisKendaraan.SelectedValue)))
                End If
                criterias.opAnd(New Criteria(GetType(V_CcGridContact), "CcPeriodID", MatchType.Exact, objCcPeriod.ID))

                If txtKonsumen.Text <> String.Empty Then
                    criterias.opAnd(New Criteria(GetType(V_CcGridContact), "ConsumerName", MatchType.[Partial], txtKonsumen.Text))
                End If

                arlReportDealer = New V_CcGridContactFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dgReports.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            End If
        Else

        End If

        dgReports.VirtualItemCount = totalRow
        dgReports.DataSource = arlReportDealer
        dgReports.DataBind()

        lblMessage.Text = "Jumlah data : " & FormatNumber(totalRow, 0)
    End Sub

    Private Sub dgReports_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgReports.SortCommand
        If CType(viewstate("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(viewstate("CurrentSortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    viewstate("CurrentSortDirect") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    viewstate("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("CurrentSortColumn") = e.SortExpression
            viewstate("CurrentSortDirect") = Sort.SortDirection.DESC
        End If
        BindDataReport(dgReports.CurrentPageIndex)
    End Sub

    Private Sub dgReports_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgReports.PageIndexChanged
        dgReports.CurrentPageIndex = e.NewPageIndex
        BindDataReport(dgReports.CurrentPageIndex)
    End Sub

#End Region

    
End Class
