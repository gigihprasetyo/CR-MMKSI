#Region "Imports Custom NameSpace"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmCustomerBusiness
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgCustomerBusiness As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents chkStatusCustomerBusiness As System.Web.UI.WebControls.CheckBox
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtDeskripsiCustomerBusiness As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtCodeCustomerBusiness As System.Web.UI.WebControls.TextBox
    Private _sessHelper As SessionHelper = New SessionHelper
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variables"
    Private _UbahPrivilage As Boolean = False
#End Region

#Region "Trigger"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitPrivilage()
        If Not IsPostBack Then
            'Put user code to initialize the page here
            BindDtgCustomerBusiness(0)
            initiatePage()
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim objCustomerBusiness As CustomerBusiness = New CustomerBusiness
        Dim objCustomerBusinessFacade As CustomerBusinessFacade = New CustomerBusinessFacade(User)
        'Dim objCityFacade As CityFacade = New CityFacade(User)

        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not txtCodeCustomerBusiness.Text = String.Empty And Not txtDeskripsiCustomerBusiness.Text = String.Empty Then
                If objCustomerBusinessFacade.ValidateCode(txtCodeCustomerBusiness.Text) = 0 Then
                    'masukin data ke objek
                    objCustomerBusiness.Code = txtCodeCustomerBusiness.Text
                    objCustomerBusiness.Description = txtDeskripsiCustomerBusiness.Text
                    If chkStatusCustomerBusiness.Checked = True Then
                        objCustomerBusiness.Status = "A"
                    Else
                        objCustomerBusiness.Status = "X"
                    End If
                    'masukin data ke objek selesai

                    nResult = New CustomerBusinessFacade(User).Insert(objCustomerBusiness)

                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        ClearData()
                    End If

                Else
                    MessageBox.Show(SR.DataIsExist("Kode Customer Business"))
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Kode dan Deskripsi Customer Business"))
            End If
        Else
            nResult = UpdateCustomerBusiness()
            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
                ClearData()
            End If
        End If

        dtgCustomerBusiness.CurrentPageIndex = 0
        BindDtgCustomerBusiness(dtgCustomerBusiness.CurrentPageIndex)
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub dtgCustomerBusiness_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgCustomerBusiness.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            txtCodeCustomerBusiness.ReadOnly = True
            txtDeskripsiCustomerBusiness.ReadOnly = True
            chkStatusCustomerBusiness.Enabled = False
            ViewCustomerBusiness(e.Item.Cells(0).Text, False)
            dtgCustomerBusiness.SelectedIndex = -1
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewCustomerBusiness(e.Item.Cells(0).Text, True)
            dtgCustomerBusiness.SelectedIndex = e.Item.ItemIndex
            txtCodeCustomerBusiness.ReadOnly = True
            txtDeskripsiCustomerBusiness.ReadOnly = False
            chkStatusCustomerBusiness.Enabled = True
        ElseIf e.CommandName = "Delete" Then
            Try
                DeleteCustomerBusiness(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearData()
        End If
    End Sub

    Private Sub dtgCustomerBusiness_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCustomerBusiness.ItemDataBound
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgCustomerBusiness.CurrentPageIndex * dtgCustomerBusiness.PageSize)
        End If

        If e.Item.Cells(4).Text = "A" Then
            e.Item.Cells(5).Text = "Aktif"
        ElseIf e.Item.Cells(4).Text.Trim = "X" Or e.Item.Cells(4).Text = " " Or e.Item.Cells(4).Text = "&nbsp;" Then
            e.Item.Cells(5).Text = "Tidak Aktif"
        End If


    End Sub

    Private Sub dtgCustomerBusiness_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCustomerBusiness.PageIndexChanged
        dtgCustomerBusiness.SelectedIndex = -1
        dtgCustomerBusiness.CurrentPageIndex = e.NewPageIndex
        BindDtgCustomerBusiness(dtgCustomerBusiness.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgCustomerBusiness_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCustomerBusiness.SortCommand
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

        dtgCustomerBusiness.SelectedIndex = -1
        dtgCustomerBusiness.CurrentPageIndex = 0
        BindDtgCustomerBusiness(dtgCustomerBusiness.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

#Region "Private Method/Function"

    Private Sub InitPrivilage()

        If Not SecurityProvider.Authorize(context.User, SR.FakturKendaraanViewUsahaKonsumen_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Faktur Kendaraan - Usaha Konsumen")
        End If

        If Not SecurityProvider.Authorize(context.User, SR.FakturKendaraanUpdateUsahaKonsumen_Privilege) Then
            Me.btnSimpan.Visible = False
            Me.btnBatal.Visible = False
            Me.dtgCustomerBusiness.Columns(6).Visible = False
        End If

    End Sub

    Private Sub BindDtgCustomerBusiness(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dtgCustomerBusiness.DataSource = New CustomerBusinessFacade(User).RetrieveActiveList(indexPage + 1, dtgCustomerBusiness.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgCustomerBusiness.VirtualItemCount = totalRow
            dtgCustomerBusiness.DataBind()
        End If

    End Sub

    Private Sub initiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = ""
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub ClearData()

        txtCodeCustomerBusiness.Text = String.Empty
        txtDeskripsiCustomerBusiness.Text = String.Empty
        chkStatusCustomerBusiness.Checked = True
        btnSimpan.Enabled = True
        txtCodeCustomerBusiness.ReadOnly = False
        txtDeskripsiCustomerBusiness.ReadOnly = False
        chkStatusCustomerBusiness.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        dtgCustomerBusiness.SelectedIndex = -1

    End Sub

    Private Function UpdateCustomerBusiness() As Integer

        Dim nResult As Integer
        Dim countRecord As Integer
        Dim statusCustomerBusiness As String

        Dim objCustomerBusiness As CustomerBusiness = CType(Session.Item("vsCustomerBusiness"), CustomerBusiness)

        If Not txtDeskripsiCustomerBusiness.Text = String.Empty Then
            objCustomerBusiness.Description = txtDeskripsiCustomerBusiness.Text
            If chkStatusCustomerBusiness.Checked = True Then
                objCustomerBusiness.Status = "A"
            Else
                objCustomerBusiness.Status = "X"
            End If
            nResult = New CustomerBusinessFacade(User).Update(objCustomerBusiness)
        End If
        Return nResult

    End Function

    Private Sub ViewCustomerBusiness(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objCustomerBusiness As CustomerBusiness = New CustomerBusinessFacade(User).Retrieve(nID)
        _sessHelper.SetSession("vsCustomerBusiness", objCustomerBusiness)

        If IsNothing(objCustomerBusiness) Then
            txtCodeCustomerBusiness.Text = ""
            txtDeskripsiCustomerBusiness.Text = ""
            chkStatusCustomerBusiness.Checked = False
            Me.btnSimpan.Enabled = EditStatus
            MessageBox.Show(SR.ViewFail)
        Else
            txtCodeCustomerBusiness.Text = objCustomerBusiness.Code
            txtDeskripsiCustomerBusiness.Text = objCustomerBusiness.Description
            If objCustomerBusiness.Status = "A" Then
                chkStatusCustomerBusiness.Checked = True
            ElseIf objCustomerBusiness.Status = "X" Then
                chkStatusCustomerBusiness.Checked = False
            End If
            Me.btnSimpan.Enabled = EditStatus
        End If

    End Sub

    Private Sub DeleteCustomerBusiness(ByVal nID As Integer)

        If New HelperFacade(User, GetType(EndCustomer)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(EndCustomer), nID), _
            CreateAggreateForCheckRecord(GetType(EndCustomer))) Then
            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objCustomerBusiness As CustomerBusiness = New CustomerBusinessFacade(User).Retrieve(nID)
            Dim nResult = New CustomerBusinessFacade(User).DeleteFromDB(objCustomerBusiness)

            dtgCustomerBusiness.CurrentPageIndex = 0
            BindDtgCustomerBusiness(dtgCustomerBusiness.CurrentPageIndex)
        End If
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal CustomerBusinessID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "CustomerBusiness", MatchType.Exact, CustomerBusinessID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

#End Region

End Class
