#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.General

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
#End Region



Public Class FrmBusinessSectorDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dgDomainBisnis As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlIndustri As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDomainBisnis As System.Web.UI.WebControls.TextBox

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
    Private _BusinessSectorHeaderDetailFacade As New BusinessSectorDetailFacade(User)

    Private sessHelper As New SessionHelper
    Private objBusinessDetailHeader As BusinessSectorDetail
    Private hasSubmitPrivilege As Boolean
    Private hasDeletePrivilege As Boolean
    Private hasCancelPrivilege As Boolean
#End Region

#Region "PrivateCustomMethods"
    ' penambahan untuk initialize data
    Private Sub ClearData()

        ddlIndustri.SelectedIndex = -1
        txtDomainBisnis.Text = String.Empty

        If dgDomainBisnis.Items.Count > 0 Then
            dgDomainBisnis.SelectedIndex = -1
        End If

        ViewState.Add("vsProcess", "Insert")
    End Sub
    Private Sub Initialize()
        ClearData()
        ViewState("CurrentSortColumn") = "BusinessSectorHeader.BusinessSectorName"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub
    Private Sub BindDataGrid(ByVal idxPage As Integer)
        sessHelper.SetSession("idxPage", idxPage)
        sessHelper.SetSession("SortColumn", ViewState("CurrentSortColumn"))
        sessHelper.SetSession("SortDirect", ViewState("CurrentSortDirect"))

        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BusinessSectorDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If ddlIndustri.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(BusinessSectorDetail), "BusinessSectorHeader.ID", MatchType.Exact, ddlIndustri.SelectedValue))
        End If

        If txtDomainBisnis.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(BusinessSectorDetail), "BusinessDomain", MatchType.Partial, txtDomainBisnis.Text))
        End If
        arrList = _BusinessSectorHeaderDetailFacade.RetrieveByCriteria(criterias, idxPage + 1, dgDomainBisnis.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgDomainBisnis.DataSource = arrList
        dgDomainBisnis.VirtualItemCount = totalRow
        Try
            dgDomainBisnis.DataBind()
        Catch ex As Exception
            dgDomainBisnis.CurrentPageIndex = 0
            dgDomainBisnis.DataBind()
        End Try
    End Sub

    Private Sub BindControlsAttribute()

    End Sub
    ' untuk menampung kriteria yg sebelumnya
    Private Sub SaveCriteria()
        Dim crits As Hashtable = New Hashtable
        crits.Add("BusinessSectorHeader", ddlIndustri.SelectedValue)
        crits.Add("DomainBisnis", txtDomainBisnis.Text)
        sessHelper.SetSession("FrmBusinessSectorDetail", crits)
    End Sub
    ' untuk menampilkan data kriteria sebelumnya
    Private Sub ReadCriteria()
        Dim crits As Hashtable
        crits = CType(sessHelper.GetSession("FrmBusinessSectorDetail"), Hashtable)
        If Not IsNothing(crits) Then
            ddlIndustri.SelectedIndex = ddlIndustri.Items.IndexOf(ddlIndustri.Items.FindByValue(CInt(crits.Item("BusinessSectorHeader"))))
            txtDomainBisnis.Text = CStr(crits.Item("DomainBisnis"))
        End If
    End Sub

    Private Sub BindBusinessSectorHeader()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BusinessSectorHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        ddlIndustri.DataSource = New BusinessSectorHeaderFacade(User).Retrieve(criterias)
        ddlIndustri.DataTextField = "BusinessSectorName"
        ddlIndustri.DataValueField = "ID"
        ddlIndustri.DataBind()
        ddlIndustri.Items.Insert(0, New ListItem("", ""))
    End Sub

    Private Sub ViewBusinessSectorDetail(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objBusinessSectorDetail As BusinessSectorDetail = New BusinessSectorDetailFacade(User).Retrieve(nID)
        If Not objBusinessSectorDetail Is Nothing Then
            sessHelper.SetSession("vsBusinessSectorDetail", objBusinessSectorDetail)
            'ViewState.Add("vsProvince", objProvince)
            ddlIndustri.SelectedIndex = ddlIndustri.Items.IndexOf(ddlIndustri.Items.FindByValue(objBusinessSectorDetail.BusinessSectorHeader.ID))
            txtDomainBisnis.Text = objBusinessSectorDetail.BusinessDomain

            Me.btnSimpan.Enabled = EditStatus
        Else
            MessageBox.Show(SR.ViewFail)
        End If

    End Sub
    Private Function UpdateBusinessSectorDetail(ByVal businessSectorHeader As BusinessSectorHeader, ByVal businessSectorDetailName As String) As Integer
        Dim nResult = -1
        Dim objBusinessSectorDetail As BusinessSectorDetail = CType(sessHelper.GetSession("vsBusinessSectorDetail"), BusinessSectorDetail)
        objBusinessSectorDetail.BusinessSectorHeader = businessSectorHeader
        objBusinessSectorDetail.BusinessDomain = businessSectorDetailName
        Try
            nResult = New BusinessSectorDetailFacade(User).Update(objBusinessSectorDetail)
        Catch ex As Exception
            MessageBox.Show(SR.UpdateFail)
        End Try

        Return nResult
    End Function

    Private Sub DeleteBusinessSectorDetail(ByVal nID As Integer)
        'Dim ArryList As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SPKCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SPKCustomer), "BusinessSectorDetail.ID", MatchType.Exact, nID))
        Dim spkCustomerList As ArrayList = New SPKCustomerFacade(User).Retrieve(criterias)
        If spkCustomerList.Count > 0 Then
            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objBusinessSectorDetail As BusinessSectorDetail = New BusinessSectorDetailFacade(User).Retrieve(nID)
            'objProvince.RowStatus = DBRowStatus.Deleted
            'Dim nResult = New ProvinceFacade(User).Update(objProvince)
            Dim facade As BusinessSectorDetailFacade = New BusinessSectorDetailFacade(User)
            objBusinessSectorDetail.RowStatus = -1
            facade.Update(objBusinessSectorDetail)
            MessageBox.Show(SR.DeleteSucces)
            
        End If
        dgDomainBisnis.CurrentPageIndex = 0
        BindDataGrid(dgDomainBisnis.CurrentPageIndex)

    End Sub

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = hasSubmitPrivilege
        btnBatal.Visible = hasCancelPrivilege
    End Sub
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
            SetControlPrivilege()
            BindControlsAttribute()
            BindBusinessSectorHeader()
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
        ViewState("CurrentSortColumn") = "BusinessSectorHeader.BusinessSectorName"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        SaveCriteria()
        dgDomainBisnis.CurrentPageIndex = 0
        BindDataGrid(0)
    End Sub
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim objBusinessSectorDetail As BusinessSectorDetail = New BusinessSectorDetail
        Dim businessSectorDetailFacade As BusinessSectorDetailFacade = New BusinessSectorDetailFacade(User)

        Dim objBusinessSectorHeader As BusinessSectorHeader = New BusinessSectorHeader
        Dim businessSectorHeaderFacade As BusinessSectorHeaderFacade = New BusinessSectorHeaderFacade(User)

        If ddlIndustri.SelectedIndex < 1 Then
            MessageBox.Show("Mohon masukkan nama Industri")

        ElseIf txtDomainBisnis.Text = "" Then
            MessageBox.Show("Mohon masukkan nama Domain Bisnis")
        Else

            Dim criterias As New CriteriaComposite(New Criteria(GetType(BusinessSectorHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BusinessSectorHeader), "ID", MatchType.Exact, ddlIndustri.SelectedValue))

            Dim arrList = New ArrayList()
            arrList = businessSectorHeaderFacade.Retrieve(criterias)
            objBusinessSectorHeader = arrList(0)

            Dim businessSectorDetailCriteria As New CriteriaComposite(New Criteria(GetType(BusinessSectorDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            businessSectorDetailCriteria.opAnd(New Criteria(GetType(BusinessSectorDetail), "BusinessDomain", MatchType.Exact, txtDomainBisnis.Text))
            businessSectorDetailCriteria.opAnd(New Criteria(GetType(BusinessSectorDetail), "BusinessSectorHeader.ID", MatchType.Exact, ddlIndustri.SelectedValue))

            Dim businessSectorDetailList As ArrayList = businessSectorDetailFacade.Retrieve(businessSectorDetailCriteria)

            If businessSectorDetailList.Count > 0 Then
                MessageBox.Show("Terdapat lebih dari 1 row untuk nama domain bisnis ini")
            Else
                Dim nResult As Integer = -1
                If CType(ViewState("vsProcess"), String) = "Insert" Then

                    If businessSectorDetailList.Count > 0 Then
                        MessageBox.Show("Terdapat lebih dari 1 row untuk nama domain bisnis ini")
                    Else
                        objBusinessSectorDetail.BusinessSectorHeader = objBusinessSectorHeader
                        objBusinessSectorDetail.BusinessDomain = txtDomainBisnis.Text
                        nResult = businessSectorDetailFacade.Insert(objBusinessSectorDetail)
                        If nResult < 0 Then
                            MessageBox.Show(SR.SaveFail)
                        Else
                            MessageBox.Show(SR.SaveSuccess)
                        End If
                    End If


                Else

                    If UpdateBusinessSectorDetail(objBusinessSectorHeader, txtDomainBisnis.Text) < 0 Then
                        MessageBox.Show(SR.UpdateFail)
                    Else
                        MessageBox.Show(SR.UpdateSucces)
                    End If

                End If
            End If
        End If


        ClearData()
        dgDomainBisnis.CurrentPageIndex = 0
        BindDataGrid(dgDomainBisnis.CurrentPageIndex)
    End Sub
    Private Sub dgDomainBisnis_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDomainBisnis.SortCommand
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
        dgDomainBisnis.SelectedIndex = -1
        dgDomainBisnis.CurrentPageIndex = 0
        BindDataGrid(dgDomainBisnis.CurrentPageIndex)
    End Sub
    Private Sub dgDomainBisnis_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDomainBisnis.PageIndexChanged
        dgDomainBisnis.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgDomainBisnis.CurrentPageIndex)
    End Sub
    Private Sub dgDomainBisnis_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDomainBisnis.ItemCommand
        Dim strTmp As String() = CType(e.CommandArgument, String).Split(";")

        If e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewBusinessSectorDetail(e.Item.Cells(0).Text, True)
            dgDomainBisnis.SelectedIndex = e.Item.ItemIndex
            txtDomainBisnis.ReadOnly = False

        ElseIf e.CommandName = "Delete" Then
            Try
                DeleteBusinessSectorDetail(e.Item.Cells(0).Text)

            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearData()
        End If

    End Sub
    Private Sub dgDomainBisnis_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDomainBisnis.ItemDataBound
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = hasDeletePrivilege
        End If

        If Not e.Item.DataItem Is Nothing Then
            Dim objBusinessSectorDetail As BusinessSectorDetail = e.Item.DataItem

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgDomainBisnis.CurrentPageIndex * dgDomainBisnis.PageSize)

            Dim lblBusinessSectorName As Label = CType(e.Item.FindControl("lblBusinessSectorName"), Label)
            lblBusinessSectorName.Text = objBusinessSectorDetail.BusinessSectorHeader.BusinessSectorName

            Dim lblBusinessSectorDetailName As Label = CType(e.Item.FindControl("lblBusinessSectorDetailName"), Label)
            lblBusinessSectorDetailName.Text = objBusinessSectorDetail.BusinessDomain

            Dim lbtnEditNew As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lbtnEditNew.CommandArgument = CType(objBusinessSectorDetail.ID, String)
            lbtnEditNew.Visible = hasSubmitPrivilege
        End If

    End Sub
#End Region


#Region "Privilege"

    Private Sub CheckPrivilege()
        hasSubmitPrivilege = SecurityProvider.Authorize(Context.User, SR.simpan_Domain_bisnis_Privilege)
        hasCancelPrivilege = SecurityProvider.Authorize(Context.User, SR.batal_Domain_bisnis_Privilege)
        hasDeletePrivilege = SecurityProvider.Authorize(Context.User, SR.delete_Domain_bisnis_Privilege)
        If Not hasSubmitPrivilege AndAlso Not hasCancelPrivilege AndAlso Not hasDeletePrivilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Umum - Domain bisnis")
        End If
    End Sub

    'Private Sub CheckPrivDealerOnly()
    '    If Not SecurityProvider.Authorize(context.User, SR.ProfileListCreate_Privilege) Then
    '        Server.Transfer("../FrmAccessDenied.aspx?modulName=Dealer - Daftar Profile")
    '    End If
    'End Sub

#End Region

End Class
