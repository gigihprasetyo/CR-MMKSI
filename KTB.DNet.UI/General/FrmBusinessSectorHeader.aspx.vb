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



Public Class FrmBusinessSectorHeader
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
    Protected WithEvents dgIndustri As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtIndustri As System.Web.UI.WebControls.TextBox

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
    Private _BusinessSectorHeaderFacade As New BusinessSectorHeaderFacade(User)
    Private _editPriv As Boolean = False
    Private sessHelper As New SessionHelper
    Private objBusinessSectorHeader As BusinessSectorHeader
    Private hasSubmitPrivilege As Boolean
    Private hasDeletePrivilege As Boolean
    Private hasCancelPrivilege As Boolean
#End Region

#Region "PrivateCustomMethods"
    ' penambahan untuk initialize data
    Private Sub ClearData()
        txtIndustri.Text = String.Empty
        If dgIndustri.Items.Count > 0 Then
            dgIndustri.SelectedIndex = -1
        End If
        ViewState.Add("vsProcess", "Insert")
    End Sub
    Private Sub Initialize()
        ClearData()
        'ViewState("CurrentSortColumn") = "BusinessSectorName"
        'ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub
    Private Sub BindDataGrid(ByVal idxPage As Integer)
        sessHelper.SetSession("idxPage", idxPage)
        'sessHelper.SetSession("SortColumn", ViewState("CurrentSortColumn"))
        'sessHelper.SetSession("SortDirect", ViewState("CurrentSortDirect"))

        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BusinessSectorHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtIndustri.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(BusinessSectorHeader), "BusinessSectorName", MatchType.Partial, txtIndustri.Text))
        End If

        arrList = _BusinessSectorHeaderFacade.RetrieveByCriteria(criterias, idxPage + 1, dgIndustri.PageSize, totalRow)
        dgIndustri.DataSource = arrList
        dgIndustri.VirtualItemCount = totalRow
        Try
            dgIndustri.DataBind()
        Catch ex As Exception
            dgIndustri.CurrentPageIndex = 0
            dgIndustri.DataBind()
        End Try
    End Sub

    Private Sub BindControlsAttribute()

    End Sub
    ' untuk menampung kriteria yg sebelumnya
    Private Sub SaveCriteria()
        Dim crits As Hashtable = New Hashtable
        crits.Add("BusinessSectorName", txtIndustri.Text)
        sessHelper.SetSession("FrmBusinessSectorHeader", crits)
    End Sub
    ' untuk menampilkan data kriteria sebelumnya
    Private Sub ReadCriteria()
        Dim crits As Hashtable
        crits = CType(sessHelper.GetSession("FrmBusinessSectorHeader"), Hashtable)
        If Not IsNothing(crits) Then
            txtIndustri.Text = CStr(crits.Item("BusinessSectorName"))

        End If
    End Sub

    Private Function UpdateBusinessSectorHeader(ByVal BusinessSectorHeaderName As String) As Integer
        Dim nResult = -1
        Dim objBusinessSectorHeader As BusinessSectorHeader = CType(sessHelper.GetSession("vsBusinessSectorHeader"), BusinessSectorHeader)
        objBusinessSectorHeader.BusinessSectorName = Me.txtIndustri.Text
        Try
            nResult = New BusinessSectorHeaderFacade(User).Update(objBusinessSectorHeader)
        Catch ex As Exception
            MessageBox.Show(SR.UpdateFail)
        End Try

        Return nResult
    End Function
    Private Sub ViewBusinessSectorHeader(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objBusinessSectorHeader As BusinessSectorHeader = New BusinessSectorHeaderFacade(User).Retrieve(nID)
        If Not objBusinessSectorHeader Is Nothing Then
            sessHelper.SetSession("vsBusinessSectorHeader", objBusinessSectorHeader)
            'ViewState.Add("vsProvince", objProvince)
            txtIndustri.Text = objBusinessSectorHeader.BusinessSectorName

            Me.btnSimpan.Enabled = EditStatus
        Else
            MessageBox.Show(SR.ViewFail)
        End If

    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal BusinessSectorHeaderID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BusinessSectorDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BusinessSectorDetail), "BusinessSectorHeader.ID", MatchType.Exact, BusinessSectorHeaderID))

        Return criterias
    End Function

    Private Function CreateAggregateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function


    Private Sub DeleteBusinessSectorHeader(ByVal nID As Integer)
        'Dim ArryList As ArrayList = New ArrayList
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "Province.ID", MatchType.Exact, nID))
        'ArryList = New DealerFacade(User).Retrieve(criterias)

        Dim iRecordCount As Integer = 0

        If New HelperFacade(User, GetType(BusinessSectorDetail)).IsRecordExist(CreateCriteriaForCheckRecord(nID), _
            CreateAggregateForCheckRecord(GetType(BusinessSectorDetail))) Then
            iRecordCount = iRecordCount + 1
        End If

        If iRecordCount > 0 Then
            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objBusinessSectorHeader As BusinessSectorHeader = New BusinessSectorHeaderFacade(User).Retrieve(nID)
            'objProvince.RowStatus = DBRowStatus.Deleted
            'Dim nResult = New ProvinceFacade(User).Update(objProvince)
            Dim facade As BusinessSectorHeaderFacade = New BusinessSectorHeaderFacade(User)
            objBusinessSectorHeader.RowStatus = -1
            facade.Update(objBusinessSectorHeader)
            MessageBox.Show(SR.DeleteSucces)
            dgIndustri.CurrentPageIndex = 0
            BindDataGrid(dgIndustri.CurrentPageIndex)

        End If
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
            BindControlsAttribute()
            SetControlPrivilege()
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

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim objBusinessSectorHeader As BusinessSectorHeader = New BusinessSectorHeader
        Dim businessSectorHeaderFacade As BusinessSectorHeaderFacade = New BusinessSectorHeaderFacade(User)
        Dim nResult As Integer = -1
        txtIndustri.ReadOnly = False

        Dim businessSectorHeaderCriteria As New CriteriaComposite(New Criteria(GetType(BusinessSectorHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        businessSectorHeaderCriteria.opAnd(New Criteria(GetType(BusinessSectorHeader), "BusinessSectorName", MatchType.Exact, txtIndustri.Text))

        Dim businessSectorHeaderList As ArrayList = businessSectorHeaderFacade.Retrieve(businessSectorHeaderCriteria)
        If businessSectorHeaderList.Count > 0 Then
            MessageBox.Show("Terdapat lebih dari 1 row untuk nama Industri ini")
        Else
            If CType(ViewState("vsProcess"), String) = "Insert" Then
                If txtIndustri.Text <> String.Empty Then
                    objBusinessSectorHeader.BusinessSectorName = txtIndustri.Text
                    nResult = businessSectorHeaderFacade.Insert(objBusinessSectorHeader)
                    If nResult < 0 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                    End If
                Else
                    MessageBox.Show("Mohon masukkan nama Industri")
                End If
            Else

                If UpdateBusinessSectorHeader(txtIndustri.Text) < 0 Then
                    MessageBox.Show(SR.UpdateFail)
                Else
                    MessageBox.Show(SR.UpdateSucces)
                End If

            End If
        End If


        ClearData()
        dgIndustri.CurrentPageIndex = 0
        BindDataGrid(dgIndustri.CurrentPageIndex)
    End Sub
    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        BindDataGrid(0)
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ViewState("CurrentSortColumn") = "BusinessSectorName"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        SaveCriteria()
        dgIndustri.CurrentPageIndex = 0
        BindDataGrid(0)
    End Sub
    Private Sub dgIndustri_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgIndustri.SortCommand
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
        dgIndustri.SelectedIndex = -1
        dgIndustri.CurrentPageIndex = 0
        BindDataGrid(dgIndustri.CurrentPageIndex)
    End Sub
    Private Sub dgIndustri_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgIndustri.PageIndexChanged
        dgIndustri.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgIndustri.CurrentPageIndex)
    End Sub
    Private Sub dgIndustri_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgIndustri.ItemCommand

        If e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewBusinessSectorHeader(e.Item.Cells(0).Text, True)
            dgIndustri.SelectedIndex = e.Item.ItemIndex
            txtIndustri.ReadOnly = False

        ElseIf e.CommandName = "Delete" Then
            Try
                DeleteBusinessSectorHeader(e.Item.Cells(0).Text)

            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearData()
        End If

    End Sub
    Private Sub dgIndustri_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgIndustri.ItemDataBound
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = hasDeletePrivilege
        End If

        If Not e.Item.DataItem Is Nothing Then
            Dim objBusinessSectorHeader As BusinessSectorHeader = e.Item.DataItem

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgIndustri.CurrentPageIndex * dgIndustri.PageSize)

            Dim lblBusinessSectorName As Label = CType(e.Item.FindControl("lblBusinessSectorName"), Label)
            lblBusinessSectorName.Text = objBusinessSectorHeader.BusinessSectorName

            Dim lbtnEditNew As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lbtnEditNew.CommandArgument = CType(objBusinessSectorHeader.ID, String)
            lbtnEditNew.Visible = hasSubmitPrivilege
        End If

    End Sub
#End Region


#Region "Privilege"

    Private Sub CheckPrivilege()
        hasSubmitPrivilege = SecurityProvider.Authorize(Context.User, SR.simpan_industri_Privilege)
        hasCancelPrivilege = SecurityProvider.Authorize(Context.User, SR.batal_industri_Privilege)
        hasDeletePrivilege = SecurityProvider.Authorize(Context.User, SR.delete_industri_Privilege)

        If Not hasSubmitPrivilege AndAlso Not hasCancelPrivilege AndAlso Not hasDeletePrivilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Umum - Industri")
        End If
    End Sub

    'Private Sub CheckPrivDealerOnly()
    '    If Not SecurityProvider.Authorize(context.User, SR.ProfileListCreate_Privilege) Then
    '        Server.Transfer("../FrmAccessDenied.aspx?modulName=Dealer - Daftar Profile")
    '    End If
    'End Sub

#End Region

End Class
