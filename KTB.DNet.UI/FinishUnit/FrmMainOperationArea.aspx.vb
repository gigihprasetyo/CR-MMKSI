#Region "Imports Custom NameSpace"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmMainOperationArea
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents chkStatusMainOperationArea As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtDeskripsiMainOperationArea As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodeMainOperationArea As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgMainOperationArea As System.Web.UI.WebControls.DataGrid
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
        If Not SecurityProvider.Authorize(context.User, SR.FakturKendaraanViewDaerahOperasi_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FAKTUR KENDARAAN - Daerah Utama Operasi")
        End If
        If Not SecurityProvider.Authorize(context.User, SR.FakturKendaraanUbahDaerahOperasi_Privilege) Then
            Me.btnSimpan.Visible = False
            Me.btnBatal.Visible = False
            Me.dtgMainOperationArea.Columns(6).Visible = False
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        If Not IsPostBack Then
            'Put user code to initialize the page here
            BindDtgMainOperationArea(0)
            initiatePage()
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim objMainOperationArea As MainOperationArea = New MainOperationArea
        Dim objMainOperationAreaFacade As MainOperationAreaFacade = New MainOperationAreaFacade(User)
        'Dim objCityFacade As CityFacade = New CityFacade(User)

        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not txtCodeMainOperationArea.Text = String.Empty And Not txtDeskripsiMainOperationArea.Text = String.Empty Then
                If objMainOperationAreaFacade.ValidateCode(txtCodeMainOperationArea.Text) = 0 Then
                    'masukin data ke objek
                    objMainOperationArea.Code = txtCodeMainOperationArea.Text
                    objMainOperationArea.Description = txtDeskripsiMainOperationArea.Text
                    If chkStatusMainOperationArea.Checked = True Then
                        objMainOperationArea.Status = "A"
                    Else
                        objMainOperationArea.Status = "X"
                    End If
                    'masukin data ke objek selesai

                    nResult = New MainOperationAreaFacade(User).Insert(objMainOperationArea)

                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        ClearData()
                    End If

                Else
                    MessageBox.Show(SR.DataIsExist("Kode Main Operation Area"))
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Kode dan Deskripsi Main Operation Area"))
            End If
        Else
            nResult = UpdateMainOperationArea()
            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
                ClearData()
            End If
        End If

        dtgMainOperationArea.CurrentPageIndex = 0
        BindDtgMainOperationArea(dtgMainOperationArea.CurrentPageIndex)
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub dtgMainOperationArea_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMainOperationArea.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            txtCodeMainOperationArea.ReadOnly = True
            txtDeskripsiMainOperationArea.ReadOnly = True
            chkStatusMainOperationArea.Enabled = False
            ViewMainOperationArea(e.Item.Cells(0).Text, False)
            dtgMainOperationArea.SelectedIndex = -1
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewMainOperationArea(e.Item.Cells(0).Text, True)
            dtgMainOperationArea.SelectedIndex = e.Item.ItemIndex
            txtCodeMainOperationArea.ReadOnly = True
            txtDeskripsiMainOperationArea.ReadOnly = False
            chkStatusMainOperationArea.Enabled = True
        ElseIf e.CommandName = "Delete" Then
            Try
                DeleteMainOperationArea(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearData()
        End If
    End Sub

    Private Sub dtgMainOperationArea_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMainOperationArea.ItemDataBound
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgMainOperationArea.CurrentPageIndex * dtgMainOperationArea.PageSize)
        End If

        If e.Item.Cells(4).Text = "A" Then
            e.Item.Cells(5).Text = "Aktif"
        ElseIf e.Item.Cells(4).Text.Trim = "X" Or e.Item.Cells(4).Text = " " Or e.Item.Cells(4).Text = "&nbsp;" Then
            e.Item.Cells(5).Text = "Tidak Aktif"
        End If
    End Sub

    Private Sub dtgMainOperationArea_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgMainOperationArea.PageIndexChanged
        dtgMainOperationArea.SelectedIndex = -1
        dtgMainOperationArea.CurrentPageIndex = e.NewPageIndex
        BindDtgMainOperationArea(dtgMainOperationArea.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgMainOperationArea_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgMainOperationArea.SortCommand
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

        dtgMainOperationArea.SelectedIndex = -1
        dtgMainOperationArea.CurrentPageIndex = 0
        BindDtgMainOperationArea(dtgMainOperationArea.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

#Region "Private Method/Function"
    Private Sub BindDtgMainOperationArea(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dtgMainOperationArea.DataSource = New MainOperationAreaFacade(User).RetrieveActiveList(indexPage + 1, dtgMainOperationArea.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgMainOperationArea.VirtualItemCount = totalRow
            dtgMainOperationArea.DataBind()
        End If

    End Sub

    Private Sub initiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = ""
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub ClearData()

        txtCodeMainOperationArea.Text = String.Empty
        txtDeskripsiMainOperationArea.Text = String.Empty
        chkStatusMainOperationArea.Checked = True
        btnSimpan.Enabled = True
        txtCodeMainOperationArea.ReadOnly = False
        txtDeskripsiMainOperationArea.ReadOnly = False
        chkStatusMainOperationArea.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        dtgMainOperationArea.SelectedIndex = -1

    End Sub

    Private Function UpdateMainOperationArea() As Integer

        Dim nResult As Integer
        Dim countRecord As Integer
        Dim statusMainOperationArea As String

        Dim objMainOperationArea As MainOperationArea = CType(Session.Item("vsMainOperationArea"), MainOperationArea)

        If Not txtDeskripsiMainOperationArea.Text = String.Empty Then
            objMainOperationArea.Description = txtDeskripsiMainOperationArea.Text
            If chkStatusMainOperationArea.Checked = True Then
                objMainOperationArea.Status = "A"
            Else
                objMainOperationArea.Status = "X"
            End If
            nResult = New MainOperationAreaFacade(User).Update(objMainOperationArea)
        End If
        Return nResult

    End Function

    Private Sub ViewMainOperationArea(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objMainOperationArea As MainOperationArea = New MainOperationAreaFacade(User).Retrieve(nID)
        _sessHelper.SetSession("vsMainOperationArea", objMainOperationArea)

        If IsNothing(objMainOperationArea) Then
            txtCodeMainOperationArea.Text = ""
            txtDeskripsiMainOperationArea.Text = ""
            chkStatusMainOperationArea.Checked = False
            Me.btnSimpan.Enabled = EditStatus
            MessageBox.Show(SR.ViewFail)
        Else
            txtCodeMainOperationArea.Text = objMainOperationArea.Code
            txtDeskripsiMainOperationArea.Text = objMainOperationArea.Description
            If objMainOperationArea.Status = "A" Then
                chkStatusMainOperationArea.Checked = True
            ElseIf objMainOperationArea.Status = "X" Then
                chkStatusMainOperationArea.Checked = False
            End If
            Me.btnSimpan.Enabled = EditStatus
        End If

        
    End Sub

    Private Sub DeleteMainOperationArea(ByVal nID As Integer)

        If New HelperFacade(User, GetType(EndCustomer)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(EndCustomer), nID), _
            CreateAggreateForCheckRecord(GetType(EndCustomer))) Then
            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objMainOperationArea As MainOperationArea = New MainOperationAreaFacade(User).Retrieve(nID)
            Dim nResult = New MainOperationAreaFacade(User).DeleteFromDB(objMainOperationArea)

            dtgMainOperationArea.CurrentPageIndex = 0
            BindDtgMainOperationArea(dtgMainOperationArea.CurrentPageIndex)
        End If
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal MainOperationAreaID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "MainOperationArea", MatchType.Exact, MainOperationAreaID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

#End Region

End Class
