Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.Utility

Public Class FrmAssignAccessibility
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents lblType As System.Web.UI.WebControls.Label
    Protected WithEvents lblCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearch1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearch2 As System.Web.UI.WebControls.Label
    Protected WithEvents dtgPrivilege As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private sHOrganization As SessionHelper = New SessionHelper
    Private objDealer As Dealer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objDealer = sHOrganization.GetSession("SessObjDealer")
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            'BindDataGrid()
        Else
            ListingCheckedData()
        End If
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not IsNothing(objDealer) Then
            If Not SecurityProvider.Authorize(Context.User, SR.AdminAssigHakAccessOrganization_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - Hak Akses Organisasi")
            End If
        End If
    End Sub

    Private Sub InitiatePage()
        lblType.Text = objDealer.TitleDealer
        lblCode.Text = objDealer.DealerCode
        lblName.Text = objDealer.DealerName
        lblCity.Text = objDealer.City.CityName
        lblSearch1.Text = objDealer.SearchTerm1
        lblSearch2.Text = objDealer.SearchTerm2
        ViewState("vsSortColumn") = "ID"
        ViewState("vsSortDirect") = Sort.SortDirection.ASC
        CreateCheckedData()
        sHOrganization.RemoveSession("objDataGrid")
    End Sub

    Private Sub BindDataGrid()
        Dim ArrPrivilege As ArrayList = sHOrganization.GetSession("objDataGrid")

        btnSave.Enabled = False
        If IsNothing(ArrPrivilege) Then
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
            If (Not IsNothing(ViewState("vsSortColumn"))) And (Not IsNothing(ViewState("vsSortDirect"))) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(Privilege), ViewState("vsSortColumn"), ViewState("vsSortDirect")))
            Else
                sortColl = Nothing
            End If

            'sts = New EnumTitle(0, "DEALER")
            'sts = New EnumTitle(1, "KTB")
            'sts = New EnumTitle(2, "KTB_DEALER")
            'sts = New EnumTitle(3, "LEASING")
            'sts = New EnumTitle(4, "KTB_LEASING")
            'sts = New EnumTitle(5, "DEALER_LEASING")
            'sts = New EnumTitle(6, "KTB_DEALER_LEASING")
           
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Privilege), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim title As String = String.Empty
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                title = "('0','2','5','6')"
            End If
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                title = "('1','2','4','6')"
            End If
            If objDealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
                title = "('3','4','5','6')"
            End If

            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Privilege), "Title", MatchType.InSet, title))
            If txtDescription.Text <> "" Then
                Dim strDescrip() As String = txtDescription.Text.Split(";")
                For i As Integer = 0 To strDescrip.Length - 1
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Privilege), "Description", MatchType.[Partial], strDescrip(i)))
                Next
            End If

            Try
                ArrPrivilege = New PrivilegeFacade(User).Retrieve(criterias, sortColl)
                sHOrganization.SetSession("objDataGrid", ArrPrivilege)
            Catch
                MessageBox.Show("Harap periksa kembali kategori pencarian anda")
                ArrPrivilege = New ArrayList
            End Try
        End If

        If ArrPrivilege.Count > 0 Then
            dtgPrivilege.DataSource = ArrPrivilege
            btnSave.Enabled = True
        Else
            dtgPrivilege.DataSource = Nothing
            MessageBox.Show("Data tidak ditemukan ")
        End If

        dtgPrivilege.DataBind()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            SaveData()
            MessageBox.Show(SR.SaveSuccess)
            CreateCheckedData()
            dtgPrivilege.CurrentPageIndex = 0
            sHOrganization.RemoveSession("objDataGrid")
            BindDataGrid()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub SaveData()
        Dim objDomain As OrganizationPrivilege
        Dim objFacade As OrganizationPrivilegeFacade = New OrganizationPrivilegeFacade(User)
        Dim checkedList As ArrayList = CType(sHOrganization.GetSession("checkedData"), ArrayList)

        Try
            objFacade.PerformTransaction(objDealer, checkedList)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub dtgPrivilege_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPrivilege.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgPrivilege.CurrentPageIndex * dtgPrivilege.PageSize)
            Dim cbItem As CheckBox = e.Item.FindControl("cbItem")
            Dim id As String = e.Item.Cells(2).Text
            Dim checkedData As ArrayList = CType(sHOrganization.GetSession("checkedData"), ArrayList)
            cbItem.Checked = False
            If Not IsNothing(checkedData) Then
                If checkedData.Contains(id) Then
                    cbItem.Checked = True
                End If
            End If

        End If
    End Sub

    Private Sub dtgPrivilege_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPrivilege.PageIndexChanged
        dtgPrivilege.CurrentPageIndex = e.NewPageIndex
        BindDataGrid()
    End Sub

    Private Sub dtgPrivilege_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPrivilege.SortCommand

        If CType(ViewState("vsSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("vsSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("vsSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("vsSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("vsSortColumn") = e.SortExpression
            ViewState("vsSortDirect") = Sort.SortDirection.ASC
        End If

        sHOrganization.RemoveSession("objDataGrid")
        dtgPrivilege.CurrentPageIndex = 0
        BindDataGrid()
    End Sub

    Private Sub ListingCheckedData()
        Dim listOfCheckedData As ArrayList = CType(sHOrganization.GetSession("checkedData"), ArrayList)
        If IsNothing(listOfCheckedData) Then
            listOfCheckedData = New ArrayList
        End If

        For Each item As DataGridItem In dtgPrivilege.Items
            If item.ItemType = ListItemType.Item Or item.ItemType = ListItemType.AlternatingItem Then
                Dim cbItem As CheckBox = CType(item.FindControl("cbItem"), CheckBox)
                If cbItem.Checked Then
                    If Not listOfCheckedData.Contains(item.Cells(2).Text) Then
                        listOfCheckedData.Add(item.Cells(2).Text)
                    End If
                Else
                    If listOfCheckedData.Contains(item.Cells(2).Text) Then
                        listOfCheckedData.Remove(item.Cells(2).Text)
                    End If
                End If
            End If
        Next

        sHOrganization.SetSession("checkedData", listOfCheckedData)

    End Sub

    Public Sub CreateCheckedData()
        Dim CritComp As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OrganizationPrivilege), "RowStatus", MatchType.Exact, CInt(DBRowStatus.Active)))
        CritComp.opAnd(New Criteria(GetType(OrganizationPrivilege), "Dealer.ID", MatchType.Exact, objDealer.ID))
        Dim objOrganizationPrivileges As ArrayList = New OrganizationPrivilegeFacade(User).Retrieve(CritComp)
        Dim checkedList As ArrayList = New ArrayList
        For Each item As OrganizationPrivilege In objOrganizationPrivileges
            checkedList.Add(CStr(item.Privilege.ID))
        Next
        sHOrganization.SetSession("checkedData", checkedList)
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("../General/FrmListDealerMantenance.aspx")
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dtgPrivilege.CurrentPageIndex = 0
        sHOrganization.RemoveSession("objDataGrid")
        BindDataGrid()
    End Sub
End Class
