#Region "Imports Custom NameSpace"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmOwnerAge
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtDeskripsiOwnerAge As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodeOwnerAge As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgOwnerAge As System.Web.UI.WebControls.DataGrid
    Protected WithEvents chkStatusOwnerAge As System.Web.UI.WebControls.CheckBox
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

#Region "Trigger"

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.FakturKendaraanViewUsiaPemilik_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FAKTUR KENDARAAN - Usia Pemilik")
        End If
        If Not SecurityProvider.Authorize(context.User, SR.FakturKendaraanUbahUsiaPemilik_Privilege) Then
            Me.btnSimpan.Visible = False
            Me.btnBatal.Visible = False
            Me.dtgOwnerAge.Columns(6).Visible = False
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        If Not IsPostBack Then
            'Put user code to initialize the page here
            BindDtgOwnerAge(0)
            initiatePage()
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim objOwnerAge As OwnerAge = New OwnerAge
        Dim objOwnerAgeFacade As OwnerAgeFacade = New OwnerAgeFacade(User)
        'Dim objCityFacade As CityFacade = New CityFacade(User)

        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not txtCodeOwnerAge.Text = String.Empty And Not txtDeskripsiOwnerAge.Text = String.Empty Then
                If objOwnerAgeFacade.ValidateCode(txtCodeOwnerAge.Text) = 0 Then
                    'masukin data ke objek
                    objOwnerAge.Code = txtCodeOwnerAge.Text
                    objOwnerAge.Description = txtDeskripsiOwnerAge.Text
                    If chkStatusOwnerAge.Checked = True Then
                        objOwnerAge.Status = "A"
                    Else
                        objOwnerAge.Status = "X"
                    End If
                    'masukin data ke objek selesai

                    nResult = New OwnerAgeFacade(User).Insert(objOwnerAge)

                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        ClearData()
                    End If

                Else
                    MessageBox.Show(SR.DataIsExist("Kode Owner Age"))
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Kode dan Deskripsi Owner Age"))
            End If
        Else
            nResult = UpdateOwnerAge()
            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
                ClearData()
            End If
        End If

        dtgOwnerAge.CurrentPageIndex = 0
        BindDtgOwnerAge(dtgOwnerAge.CurrentPageIndex)
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub dtgOwnerAge_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgOwnerAge.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            txtCodeOwnerAge.ReadOnly = True
            txtDeskripsiOwnerAge.ReadOnly = True
            chkStatusOwnerAge.Enabled = False
            ViewOwnerAge(e.Item.Cells(0).Text, False)
            dtgOwnerAge.SelectedIndex = -1
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewOwnerAge(e.Item.Cells(0).Text, True)
            dtgOwnerAge.SelectedIndex = e.Item.ItemIndex
            txtCodeOwnerAge.ReadOnly = True
            txtDeskripsiOwnerAge.ReadOnly = False
            chkStatusOwnerAge.Enabled = True
        ElseIf e.CommandName = "Delete" Then
            Try
                DeleteOwnerAge(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearData()
        End If
    End Sub

    Private Sub dtgOwnerAge_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgOwnerAge.ItemDataBound
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgOwnerAge.CurrentPageIndex * dtgOwnerAge.PageSize)
        End If

        If e.Item.Cells(4).Text = "A" Then
            e.Item.Cells(5).Text = "Aktif"
        ElseIf e.Item.Cells(4).Text.Trim = "X" Or e.Item.Cells(4).Text = " " Or e.Item.Cells(4).Text = "&nbsp;" Then
            e.Item.Cells(5).Text = "Tidak Aktif"
        End If
    End Sub

    Private Sub dtgOwnerAge_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgOwnerAge.PageIndexChanged
        dtgOwnerAge.SelectedIndex = -1
        dtgOwnerAge.CurrentPageIndex = e.NewPageIndex
        BindDtgOwnerAge(dtgOwnerAge.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgOwnerAge_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgOwnerAge.SortCommand
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

        dtgOwnerAge.SelectedIndex = -1
        dtgOwnerAge.CurrentPageIndex = 0
        BindDtgOwnerAge(dtgOwnerAge.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

#Region "Private Method/Function"
    Private Sub BindDtgOwnerAge(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dtgOwnerAge.DataSource = New OwnerAgeFacade(User).RetrieveActiveList(indexPage + 1, dtgOwnerAge.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgOwnerAge.VirtualItemCount = totalRow
            dtgOwnerAge.DataBind()
        End If

    End Sub

    Private Sub initiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = ""
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub ClearData()

        txtCodeOwnerAge.Text = String.Empty
        txtDeskripsiOwnerAge.Text = String.Empty
        chkStatusOwnerAge.Checked = True
        btnSimpan.Enabled = True
        txtCodeOwnerAge.ReadOnly = False
        txtDeskripsiOwnerAge.ReadOnly = False
        chkStatusOwnerAge.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        dtgOwnerAge.SelectedIndex = -1

    End Sub

    Private Function UpdateOwnerAge() As Integer

        Dim nResult As Integer
        Dim countRecord As Integer
        Dim statusOwnerAge As String

        Dim objOwnerAge As OwnerAge = CType(Session.Item("vsOwnerAge"), OwnerAge)

        If Not txtDeskripsiOwnerAge.Text = String.Empty Then
            objOwnerAge.Description = txtDeskripsiOwnerAge.Text
            If chkStatusOwnerAge.Checked = True Then
                objOwnerAge.Status = "A"
            Else
                objOwnerAge.Status = "X"
            End If
            nResult = New OwnerAgeFacade(User).Update(objOwnerAge)
        End If
        Return nResult

    End Function

    Private Sub ViewOwnerAge(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objOwnerAge As OwnerAge = New OwnerAgeFacade(User).Retrieve(nID)
        _sessHelper.SetSession("vsOwnerAge", objOwnerAge)

        If IsNothing(objOwnerAge) Then
            txtCodeOwnerAge.Text = ""
            txtDeskripsiOwnerAge.Text = ""
            chkStatusOwnerAge.Checked = False
            Me.btnSimpan.Enabled = EditStatus
            MessageBox.Show(SR.ViewFail)
        Else
            txtCodeOwnerAge.Text = objOwnerAge.Code
            txtDeskripsiOwnerAge.Text = objOwnerAge.Description
            If objOwnerAge.Status = "A" Then
                chkStatusOwnerAge.Checked = True
            ElseIf objOwnerAge.Status = "X" Then
                chkStatusOwnerAge.Checked = False
            End If
            Me.btnSimpan.Enabled = EditStatus
        End If
        
    End Sub

    Private Sub DeleteOwnerAge(ByVal nID As Integer)

        If New HelperFacade(User, GetType(EndCustomer)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(EndCustomer), nID), _
            CreateAggreateForCheckRecord(GetType(EndCustomer))) Then
            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objOwnerAge As OwnerAge = New OwnerAgeFacade(User).Retrieve(nID)
            Dim nResult = New OwnerAgeFacade(User).DeleteFromDB(objOwnerAge)

            dtgOwnerAge.CurrentPageIndex = 0
            BindDtgOwnerAge(dtgOwnerAge.CurrentPageIndex)
        End If

    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal OwnerAgeID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "OwnerAge", MatchType.Exact, OwnerAgeID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

#End Region

End Class
