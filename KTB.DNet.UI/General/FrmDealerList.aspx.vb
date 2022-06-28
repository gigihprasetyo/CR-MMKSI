#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.General

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports KTB.DNet.UI.Helper
#End Region



Public Class FrmDealerList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlArea As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgDealer As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button

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
    Private _SalesmanAreaFacade As New SalesmanAreaFacade(User)
    Private _DealerFacade As New DealerFacade(User)
    Private _editPriv As Boolean = False
    Private sessHelper As New SessionHelper
    Private objDealer As Dealer

#End Region

#Region "PrivateCustomMethods"
    ' penambahan untuk initialize data
    Private Sub ClearData()

        txtDealerCode.Text = String.Empty
        ddlArea.SelectedIndex = -1

        If dgDealer.Items.Count > 0 Then
            dgDealer.SelectedIndex = -1
        End If

    End Sub
    Private Sub Initialize()
        ClearData()
        ViewState("CurrentSortColumn") = "DealerCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub
    Private Sub BindDataGrid(ByVal idxPage As Integer)
        sessHelper.SetSession("idxPage", idxPage)
        sessHelper.SetSession("SortColumn", ViewState("CurrentSortColumn"))
        sessHelper.SetSession("SortDirect", ViewState("CurrentSortDirect"))

        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim sign As Integer = 0
        objDealer = Session("DEALER")

        'If txtDealerCode.Text.Length > 0 Then
        '    criterias.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.InSet, CommonFunction.GetStrValue(txtDealerCode.Text, ";", ",")))
        'End If

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If (txtDealerCode.Text.Trim <> String.Empty) Then
                criterias.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
            End If
        Else
            If (txtDealerCode.Text.Trim <> String.Empty) Then
                If New DataOwner().IsdealerExistInGroup(txtDealerCode.Text.Trim, objDealer) Then
                    criterias.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
                Else
                    sign = -1
                End If
            Else
                Dim strCrit As String = New DataOwner().GenerateDealerCodeSelection(objDealer, User)
                criterias.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.InSet, strCrit))
            End If
            ' mengambil data berdasarkan group dealernya
            Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
            criterias.opAnd(New Criteria(GetType(Dealer), "DealerGroup.ID", MatchType.Exact, objuser.Dealer.DealerGroup.ID))
        End If


        If ddlArea.SelectedItem.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(Dealer), "Area1.ID", MatchType.Exact, ddlArea.SelectedValue))
        End If

        arrList = _DealerFacade.RetrieveByCriteria(criterias, idxPage + 1, dgDealer.PageSize, totalRow, _
        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        If sign = -1 Then
            dgDealer.DataSource = Nothing
            dgDealer.DataBind()
            MessageBox.Show("Kode dealer tidak valid.")
        Else
            dgDealer.DataSource = arrList
            dgDealer.VirtualItemCount = totalRow
            Try
                dgDealer.DataBind()
            Catch ex As Exception
                dgDealer.CurrentPageIndex = 0
                dgDealer.DataBind()
            End Try
        End If

    End Sub
    ' Untuk bind data yg bersangkutan - related
    Private Sub BindDropDownLists()
        CommonFunction.BindArea2Code(ddlArea, Me.User, True)
    End Sub
    Private Sub BindControlsAttribute()
        lblPopUpDealer.Attributes("onClick") = "showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);"
    End Sub
    ' untuk menampung kriteria yg sebelumnya
    Private Sub SaveCriteria()
        Dim crits As Hashtable = New Hashtable
        crits.Add("DealerCode", txtDealerCode.Text)
        crits.Add("Area1Id", ddlArea.SelectedValue)
        sessHelper.SetSession("FrmDealerList", crits)
    End Sub
    ' untuk menampilkan data kriteria sebelumnya
    Private Sub ReadCriteria()
        Dim crits As Hashtable
        crits = CType(sessHelper.GetSession("FrmDealerList"), Hashtable)
        If Not IsNothing(crits) Then
            txtDealerCode.Text = CStr(crits.Item("DealerCode"))
            ddlArea.SelectedValue = CStr(crits.Item("Area1Id"))
        End If
    End Sub
    '' penambahan untuk view data
    'Private Sub ViewArea(ByVal nID As Integer, ByVal EditStatus As Boolean)
    '    Dim objSalesmanArea As SalesmanArea = New SalesmanAreaFacade(User).Retrieve(nID)
    '    Session.Add("vsSalesmanArea", objSalesmanArea)
    '    txtAreaCode.Text = objSalesmanArea.AreaCode
    '    txtAreaDesc.Text = objSalesmanArea.AreaDesc
    '    txtCity.Text = objSalesmanArea.City
    '    Me.btnSimpan.Enabled = EditStatus
    'End Sub

    ' ini perlu set security

#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        If Not IsNothing(sessHelper.GetSession("LOGINUSERINFO")) Then
            Dim objUserInfo As UserInfo = New UserInfo
            objUserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
            'If objUserInfo.Dealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            '    CheckPrivDealerOnly()
            'End If
        End If
        If Not IsPostBack Then
            Initialize()
            BindControlsAttribute()
            BindDropDownLists()
            ReadCriteria()
            If Request.QueryString("isback") <> String.Empty AndAlso Request.QueryString("isback") = "1" Then
                ViewState("CurrentSortColumn") = sessHelper.GetSession("SortColumn")
                ViewState("CurrentSortDirect") = sessHelper.GetSession("SortDirect")
                BindDataGrid(sessHelper.GetSession("idxPage"))
            Else
                BindDataGrid(0)
            End If

        End If
    End Sub
    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        BindDataGrid(0)
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ViewState("CurrentSortColumn") = "DealerCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        SaveCriteria()
        dgDealer.CurrentPageIndex = 0
        BindDataGrid(0)
    End Sub
    Private Sub dgDealer_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDealer.SortCommand
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
        dgDealer.SelectedIndex = -1
        dgDealer.CurrentPageIndex = 0
        BindDataGrid(dgDealer.CurrentPageIndex)
    End Sub
    Private Sub dgDealer_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDealer.PageIndexChanged
        dgDealer.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgDealer.CurrentPageIndex)
    End Sub
    Private Sub dgDealer_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDealer.ItemCommand
        Dim strTmp As String() = CType(e.CommandArgument, String).Split(";")

        Dim strDealerId As String = strTmp(0)
        Dim strDealerCode As String
        If CType(e.CommandArgument, String).IndexOf(";") > 0 Then
            strDealerCode = strTmp(1)
        End If

        Select Case e.CommandName
            Case "View"
                Response.Redirect("../General/FrmDealerProfileList.aspx?DealerID=" & strDealerId)
            Case "Edit"
                If strDealerCode <> "" Then
                    Response.Redirect("../General/FrmDealerProfile.aspx?DealerCode=" & strDealerCode)
                End If
        End Select

    End Sub
    Private Sub dgDealer_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDealer.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objDealer As Dealer = e.Item.DataItem

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgDealer.CurrentPageIndex * dgDealer.PageSize)

            Dim lblDealerCodeNew As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            lblDealerCodeNew.Text = objDealer.DealerCode

            Dim lblDealerNameNew As Label = CType(e.Item.FindControl("lblDealerName"), Label)
            lblDealerNameNew.Text = objDealer.DealerName

            Dim lblCityNameNew As Label = CType(e.Item.FindControl("lblCityName"), Label)
            lblCityNameNew.Text = objDealer.City.CityName

            Dim lbtnViewNew As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            lbtnViewNew.CommandArgument = CType(objDealer.ID, String) & ";" & CType(objDealer.DealerCode, String)

            Dim lbtnEditNew As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lbtnEditNew.CommandArgument = CType(objDealer.ID, String) & ";" & CType(objDealer.DealerCode, String)
        End If

    End Sub
#End Region


#Region "Privilege"

    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.ProfileListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Dealer - Daftar Profile")
        End If
    End Sub

    'Private Sub CheckPrivDealerOnly()
    '    If Not SecurityProvider.Authorize(context.User, SR.ProfileListCreate_Privilege) Then
    '        Server.Transfer("../FrmAccessDenied.aspx?modulName=Dealer - Daftar Profile")
    '    End If
    'End Sub

#End Region

End Class
