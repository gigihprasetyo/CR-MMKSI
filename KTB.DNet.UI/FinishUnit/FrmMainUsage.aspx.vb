#Region "Imports Custom NameSpace"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmMainUsage
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtCodeMainUsage As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgMainUsage As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtDeskripsiMainUsage As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkStatusMainUsage As System.Web.UI.WebControls.CheckBox
    Private _sessHelper As SessionHelper = New SessionHelper
    Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Trigger"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitPrivilage()
        If Not IsPostBack Then
            'Put user code to initialize the page here
            BindDtgMainUsage(0)
            initiatePage()
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim objMainUsage As MainUsage = New MainUsage
        Dim objMainUsageFacade As MainUsageFacade = New MainUsageFacade(User)
        'Dim objCityFacade As CityFacade = New CityFacade(User)

        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not txtCodeMainUsage.Text = String.Empty And Not txtDeskripsiMainUsage.Text = String.Empty Then
                If objMainUsageFacade.ValidateCode(txtCodeMainUsage.Text) = 0 Then
                    'masukin data ke objek
                    objMainUsage.Code = txtCodeMainUsage.Text
                    objMainUsage.Description = txtDeskripsiMainUsage.Text
                    If chkStatusMainUsage.Checked = True Then
                        objMainUsage.Status = "A"
                    Else
                        objMainUsage.Status = "X"
                    End If
                    'masukin data ke objek selesai

                    nResult = New MainUsageFacade(User).Insert(objMainUsage)

                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        ClearData()
                    End If

                Else
                    MessageBox.Show(SR.DataIsExist("Kode Main Usage"))
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Kode dan Deskripsi Main Usage"))
            End If
        Else
            nResult = UpdateMainUSage()
            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
                ClearData()
            End If
        End If

        dtgMainUsage.CurrentPageIndex = 0
        BindDtgMainUsage(dtgMainUsage.CurrentPageIndex)
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub dtgMainUsage_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMainUsage.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            txtCodeMainUsage.ReadOnly = True
            txtDeskripsiMainUsage.ReadOnly = True
            chkStatusMainUsage.Enabled = False
            ViewMainUsage(e.Item.Cells(0).Text, False)
            dtgMainUsage.SelectedIndex = -1
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewMainUsage(e.Item.Cells(0).Text, True)
            dtgMainUsage.SelectedIndex = e.Item.ItemIndex
            txtCodeMainUsage.ReadOnly = True
            txtDeskripsiMainUsage.ReadOnly = False
            chkStatusMainUsage.Enabled = True
        ElseIf e.CommandName = "Delete" Then
            Try
                DeleteMainUsage(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearData()
        End If
    End Sub

    Private Sub dtgMainUsage_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMainUsage.ItemDataBound
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgMainUsage.CurrentPageIndex * dtgMainUsage.PageSize)
        End If

        If e.Item.Cells(4).Text = "A" Then
            e.Item.Cells(5).Text = "Aktif"
        ElseIf e.Item.Cells(4).Text.Trim = "X" Or e.Item.Cells(4).Text = " " Or e.Item.Cells(4).Text = "&nbsp;" Then
            e.Item.Cells(5).Text = "Tidak Aktif"
        End If


    End Sub

    Private Sub dtgMainUsage_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgMainUsage.PageIndexChanged
        dtgMainUsage.SelectedIndex = -1
        dtgMainUsage.CurrentPageIndex = e.NewPageIndex
        BindDtgMainUsage(dtgMainUsage.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgMainUsage_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgMainUsage.SortCommand
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

        dtgMainUsage.SelectedIndex = -1
        dtgMainUsage.CurrentPageIndex = 0
        BindDtgMainUsage(dtgMainUsage.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

#Region "Private Method/Function"


    Private Sub InitPrivilage()

        If Not SecurityProvider.Authorize(context.User, SR.FakturKendaraanViewPenggunaanUtama_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Faktur Kendaraan - Penggunaan Utama")
        End If

        If Not SecurityProvider.Authorize(context.User, SR.FakturKendaraanUbahPenggunaanUtama_Privilege) Then
            Me.btnSimpan.Visible = False
            Me.btnBatal.Visible = False
            Me.dtgMainUsage.Columns(6).Visible = False
        End If
    End Sub


    Private Sub BindDtgMainUsage(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dtgMainUsage.DataSource = New MainUsageFacade(User).RetrieveActiveList(indexPage + 1, dtgMainUsage.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgMainUsage.VirtualItemCount = totalRow
            dtgMainUsage.DataBind()
        End If

    End Sub

    Private Sub initiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = ""
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub ClearData()

        txtCodeMainUsage.Text = String.Empty
        txtDeskripsiMainUsage.Text = String.Empty
        chkStatusMainUsage.Checked = True
        btnSimpan.Enabled = True
        txtCodeMainUsage.ReadOnly = False
        txtDeskripsiMainUsage.ReadOnly = False
        chkStatusMainUsage.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        dtgMainUsage.SelectedIndex = -1

    End Sub

    Private Function UpdateMainUSage() As Integer

        Dim nResult As Integer
        Dim countRecord As Integer
        Dim statusMainUsage As String

        Dim objMainUsage As MainUsage = CType(Session.Item("vsMainUSage"), MainUsage)

        If Not txtDeskripsiMainUsage.Text = String.Empty Then
            objMainUsage.Description = txtDeskripsiMainUsage.Text
            If chkStatusMainUsage.Checked = True Then
                objMainUsage.Status = "A"
            Else
                objMainUsage.Status = "X"
            End If
            nResult = New MainUsageFacade(User).Update(objMainUsage)
        End If
        Return nResult

    End Function

    Private Sub ViewMainUsage(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objMainUsage As MainUsage = New MainUsageFacade(User).Retrieve(nID)
        _sessHelper.SetSession("vsMainUsage", objMainUsage)
        'Session.Add("vsMainUsage", objMainUsage)

        If IsNothing(objMainUsage) Then
            txtCodeMainUsage.Text = ""
            txtDeskripsiMainUsage.Text = ""
            chkStatusMainUsage.Checked = False
            Me.btnSimpan.Enabled = EditStatus
            MessageBox.Show(SR.ViewFail)
        Else
            txtCodeMainUsage.Text = objMainUsage.Code
            txtDeskripsiMainUsage.Text = objMainUsage.Description
            If objMainUsage.Status = "A" Then
                chkStatusMainUsage.Checked = True
            ElseIf objMainUsage.Status = "X" Then
                chkStatusMainUsage.Checked = False
            End If
            Me.btnSimpan.Enabled = EditStatus
        End If

        
    End Sub

    Private Sub DeleteMainUsage(ByVal nID As Integer)

        If New HelperFacade(User, GetType(EndCustomer)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(EndCustomer), nID), _
            CreateAggreateForCheckRecord(GetType(EndCustomer))) Then
            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objMainUsage As MainUsage = New MainUsageFacade(User).Retrieve(nID)
            Dim nResult = New MainUsageFacade(User).DeleteFromDB(objMainUsage)

            dtgMainUsage.CurrentPageIndex = 0
            BindDtgMainUsage(dtgMainUsage.CurrentPageIndex)
        End If
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal MainUsageID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "MainUsage", MatchType.Exact, MainUsageID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

#End Region

End Class
