#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper

#End Region

Public Class PopUpSPK
    Inherits System.Web.UI.Page

    Private sesHelper As New SessionHelper
    Private SessionGridSPK As String = "PopUpSPK.SPKHeaderList"

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents dtgNationalEventSelection As System.Web.UI.WebControls.DataGrid
    'Protected WithEvents txtEventName As System.Web.UI.WebControls.TextBox
    'Protected WithEvents ddlEventCategory As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents icEventDateFrom As KTB.DNet.WebCC.IntiCalendar
    'Protected WithEvents icEventDateTo As KTB.DNet.WebCC.IntiCalendar
    'Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    'Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    'Protected WithEvents chkTanggal As System.Web.UI.WebControls.CheckBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Property SesDealer() As Dealer
        Get
            Return CType(sesHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sesHelper.SetSession("DEALER", Value)
        End Set
    End Property

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " custom Declaration "
    Private objDealer As Dealer
#End Region

#Region " Custom Method "

    Private Sub ClearData()
        Me.txtName.Text = String.Empty
        Me.txtKTP.Text = String.Empty
        dtgSpkSelection.DataSource = New ArrayList
        dtgSpkSelection.DataBind()
    End Sub

#End Region

#Region " Event Hendler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsPostBack Then
            'Added by Ery for sorting
            ViewState("currSortColumn") = "CreatedTime"
            ViewState("currSortDirection") = Sort.SortDirection.DESC

            objDealer = Session("DEALER")
            ClearData()

            BindSearch()
            BindGrid(dtgSpkSelection.CurrentPageIndex)  '-- Bind page-1
            If dtgSpkSelection.Items.Count > 0 Then
                btnChoose.Disabled = False
            Else
                btnChoose.Disabled = True
            End If
        End If
    End Sub

    Public Sub BindSearch()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SPKHeader), "Dealer.ID", MatchType.Exact, SesDealer().ID))
        criterias.opAnd(New Criteria(GetType(SPKHeader), "SPKCustomer.SAPCustomer.ID", MatchType.Greater, 0))

        If Not txtName.Text.Trim = "" Then
            criterias.opAnd(New Criteria(GetType(SPKHeader), "SPKCustomer.Name1", MatchType.Exact, txtName.Text))
        End If

        Dim strQuery As String = ""
        If Not txtKTP.Text.Trim = "" Then
            strQuery = "SELECT SPKCustomerID FROM SPKCustomerProfile WHERE ProfileHeaderID=29 AND ProfileValue='" & txtKTP.Text.Trim & "'"
            criterias.opAnd(New Criteria(GetType(SPKHeader), "SPKCustomer.Name1", MatchType.InSet, "(" & strQuery & ")"))
        End If

        If Not IsNothing(Request.QueryString("EventCode")) AndAlso Not Request.QueryString("EventCode") = "" Then
            criterias.opAnd(New Criteria(GetType(SPKHeader), "CampaignName", MatchType.Exact, Request.QueryString("EventCode").ToString))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(SPKHeader), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        '-- Retrieve recordset
        Dim data As ArrayList = New SPKHeaderFacade(User).Retrieve(criterias, sortColl)
        sesHelper.SetSession(SessionGridSPK, data)
        If data.Count <= 0 Then
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        End If
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim arrSPKList As ArrayList = CType(sesHelper.GetSession(SessionGridSPK), ArrayList)
        If arrSPKList.Count <> 0 Then
            CommonFunction.SortListControl(arrSPKList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrSPKList, pageIndex, dtgSpkSelection.PageSize)
            dtgSpkSelection.DataSource = PagedList
            dtgSpkSelection.VirtualItemCount = arrSPKList.Count()
            dtgSpkSelection.DataBind()
        Else
            dtgSpkSelection.DataSource = New ArrayList
            dtgSpkSelection.VirtualItemCount = 0
            dtgSpkSelection.CurrentPageIndex = 0
            dtgSpkSelection.DataBind()
        End If
    End Sub

    Private Sub dtgSpkSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSpkSelection.ItemDataBound
        Dim lblSPKNumber As Label = CType(e.Item.FindControl("lblSPKNumber"), Label)
        Dim lblCustomerName As Label = CType(e.Item.FindControl("lblCustomerName"), Label)
        Dim lblProfileValue As Label = CType(e.Item.FindControl("lblProfileValue"), Label)
        Dim lblAddress As Label = CType(e.Item.FindControl("lblAddress"), Label)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As SPKHeader = CType(e.Item.DataItem, SPKHeader)
            lblSPKNumber.Text = oData.SPKNumber
            lblCustomerName.Text = oData.SPKCustomer.Name1

            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKCustomerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(SPKCustomerProfile), "SPKCustomer.ID", MatchType.Exact, oData.SPKCustomer.ID))
            crit.opAnd(New Criteria(GetType(SPKCustomerProfile), "ProfileHeader.ID", MatchType.Exact, 29))

            Dim arrSPKCustomerProfile As ArrayList = New KTB.DNet.BusinessFacade.Profile.SPKCustomerProfileFacade(User).Retrieve(crit)

            If arrSPKCustomerProfile.Count > 0 Then
                Dim objSPKCustomerProfile As SPKCustomerProfile = arrSPKCustomerProfile(0)
                lblProfileValue.Text = objSPKCustomerProfile.ProfileValue
            End If

            lblAddress.Text = oData.SPKCustomer.Alamat
        End If


        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As SPKHeader = CType(e.Item.DataItem, SPKHeader)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindSearch()
        BindGrid(dtgSpkSelection.CurrentPageIndex)
        If dtgSpkSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

#End Region

    Private Sub dtgSpkSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSpkSelection.SortCommand
        If e.SortExpression = ViewState("currSortColumn") Then
            If ViewState("currSortDirection") = Sort.SortDirection.ASC Then
                ViewState.Add("currSortDirection", Sort.SortDirection.ASC)
            Else
                ViewState.Add("currSortDirection", Sort.SortDirection.DESC)
            End If
        End If
        ViewState.Add("currSortColumn", e.SortExpression)
        dtgSpkSelection.CurrentPageIndex = 0
        BindSearch()
        BindGrid(dtgSpkSelection.CurrentPageIndex)
    End Sub

    Private Sub dtgSpkSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSpkSelection.PageIndexChanged
        dtgSpkSelection.CurrentPageIndex = e.NewPageIndex
        BindSearch()
        BindGrid(e.NewPageIndex)
    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

End Class