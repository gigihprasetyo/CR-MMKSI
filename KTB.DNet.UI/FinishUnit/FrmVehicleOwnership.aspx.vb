#Region "Imports Custom NameSpace"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmVehicleOwnership
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgVehicleOwnership As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents chkStatusVehicleOwnership As System.Web.UI.WebControls.CheckBox
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtDeskripsiVehicleOwnership As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtCodeVehicleOwnership As System.Web.UI.WebControls.TextBox
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitPrivilage()
        If Not IsPostBack Then
            'Put user code to initialize the page here
            BindDtgVehicleOwnership(0)
            initiatePage()
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim objVehicleOwnership As VehicleOwnership = New VehicleOwnership
        Dim objVehicleOwnershipFacade As VehicleOwnershipFacade = New VehicleOwnershipFacade(User)
        'Dim objCityFacade As CityFacade = New CityFacade(User)

        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not txtCodeVehicleOwnership.Text = String.Empty And Not txtDeskripsiVehicleOwnership.Text = String.Empty Then
                If objVehicleOwnershipFacade.ValidateCode(txtCodeVehicleOwnership.Text) = 0 Then
                    'masukin data ke objek
                    objVehicleOwnership.Code = txtCodeVehicleOwnership.Text
                    objVehicleOwnership.Description = txtDeskripsiVehicleOwnership.Text
                    If chkStatusVehicleOwnership.Checked = True Then
                        objVehicleOwnership.Status = "A"
                    Else
                        objVehicleOwnership.Status = "X"
                    End If
                    'masukin data ke objek selesai

                    nResult = New VehicleOwnershipFacade(User).Insert(objVehicleOwnership)

                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        ClearData()
                    End If

                Else
                    MessageBox.Show(SR.DataIsExist("Kode Vehicle Ownership"))
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Kode dan Deskripsi Vehicle Ownership"))
            End If
        Else
            nResult = UpdateVehicleOwnership()
            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
                ClearData()
            End If
        End If

        dtgVehicleOwnership.CurrentPageIndex = 0
        BindDtgVehicleOwnership(dtgVehicleOwnership.CurrentPageIndex)
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub dtgVehicleOwnership_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgVehicleOwnership.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            txtCodeVehicleOwnership.ReadOnly = True
            txtDeskripsiVehicleOwnership.ReadOnly = True
            chkStatusVehicleOwnership.Enabled = False
            ViewVehicleOwnership(e.Item.Cells(0).Text, False)
            dtgVehicleOwnership.SelectedIndex = -1
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewVehicleOwnership(e.Item.Cells(0).Text, True)
            dtgVehicleOwnership.SelectedIndex = e.Item.ItemIndex
            txtCodeVehicleOwnership.ReadOnly = True
            txtDeskripsiVehicleOwnership.ReadOnly = False
            chkStatusVehicleOwnership.Enabled = True
        ElseIf e.CommandName = "Delete" Then
            Try
                DeleteVehicleOwnership(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearData()
        End If
    End Sub

    Private Sub dtgVehicleOwnership_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgVehicleOwnership.ItemDataBound
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgVehicleOwnership.CurrentPageIndex * dtgVehicleOwnership.PageSize)
        End If

        If e.Item.Cells(4).Text = "A" Then
            e.Item.Cells(5).Text = "Aktif"
        ElseIf e.Item.Cells(4).Text.Trim = "X" Or e.Item.Cells(4).Text = " " Or e.Item.Cells(4).Text = "&nbsp;" Then
            e.Item.Cells(5).Text = "Tidak Aktif"
        End If
    End Sub

    Private Sub dtgVehicleOwnership_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgVehicleOwnership.PageIndexChanged
        dtgVehicleOwnership.SelectedIndex = -1
        dtgVehicleOwnership.CurrentPageIndex = e.NewPageIndex
        BindDtgVehicleOwnership(dtgVehicleOwnership.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgVehicleOwnership_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgVehicleOwnership.SortCommand
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

        dtgVehicleOwnership.SelectedIndex = -1
        dtgVehicleOwnership.CurrentPageIndex = 0
        BindDtgVehicleOwnership(dtgVehicleOwnership.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

#Region "Private Method/Function"

    Private Sub InitPrivilage()

        If Not SecurityProvider.Authorize(context.User, SR.FakturKendaraanViewKepemilikanKendaraan_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Kepemilikan Kendaraan")
        End If

        If Not SecurityProvider.Authorize(context.User, SR.FakturKendaraanUbahKepemilikanKendaraan_Privilege) Then
            Me.btnSimpan.Visible = False
            Me.btnBatal.Visible = False
            Me.dtgVehicleOwnership.Columns(6).Visible = False
        End If

    End Sub


    Private Sub BindDtgVehicleOwnership(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dtgVehicleOwnership.DataSource = New VehicleOwnershipFacade(User).RetrieveActiveList(indexPage + 1, dtgVehicleOwnership.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgVehicleOwnership.VirtualItemCount = totalRow
            dtgVehicleOwnership.DataBind()
        End If

    End Sub

    Private Sub initiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = ""
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub ClearData()

        txtCodeVehicleOwnership.Text = String.Empty
        txtDeskripsiVehicleOwnership.Text = String.Empty
        chkStatusVehicleOwnership.Checked = True
        btnSimpan.Enabled = True
        txtCodeVehicleOwnership.ReadOnly = False
        txtDeskripsiVehicleOwnership.ReadOnly = False
        chkStatusVehicleOwnership.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        dtgVehicleOwnership.SelectedIndex = -1

    End Sub

    Private Function UpdateVehicleOwnership() As Integer

        Dim nResult As Integer
        Dim countRecord As Integer
        Dim statusVehicleOwnership As String

        Dim objVehicleOwnership As VehicleOwnership = CType(Session.Item("vsVehicleOwnership"), VehicleOwnership)

        If Not txtDeskripsiVehicleOwnership.Text = String.Empty Then
            objVehicleOwnership.Description = txtDeskripsiVehicleOwnership.Text
            If chkStatusVehicleOwnership.Checked = True Then
                objVehicleOwnership.Status = "A"
            Else
                objVehicleOwnership.Status = "X"
            End If
            nResult = New VehicleOwnershipFacade(User).Update(objVehicleOwnership)
        End If
        Return nResult

    End Function

    Private Sub ViewVehicleOwnership(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objVehicleOwnership As VehicleOwnership = New VehicleOwnershipFacade(User).Retrieve(nID)
        _sessHelper.SetSession("vsVehicleOwnership", objVehicleOwnership)

        If IsNothing(objVehicleOwnership) Then
            txtCodeVehicleOwnership.Text = ""
            txtDeskripsiVehicleOwnership.Text = ""
            chkStatusVehicleOwnership.Checked = False
            MessageBox.Show(SR.ViewFail)
        Else
            txtCodeVehicleOwnership.Text = objVehicleOwnership.Code
            txtDeskripsiVehicleOwnership.Text = objVehicleOwnership.Description
            If objVehicleOwnership.Status = "A" Then
                chkStatusVehicleOwnership.Checked = True
            ElseIf objVehicleOwnership.Status = "X" Then
                chkStatusVehicleOwnership.Checked = False
            End If
        End If
        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub DeleteVehicleOwnership(ByVal nID As Integer)

        If New HelperFacade(User, GetType(EndCustomer)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(EndCustomer), nID), _
            CreateAggreateForCheckRecord(GetType(EndCustomer))) Then
            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objVehicleOwnership As VehicleOwnership = New VehicleOwnershipFacade(User).Retrieve(nID)
            Dim nResult = New VehicleOwnershipFacade(User).DeleteFromDB(objVehicleOwnership)

            dtgVehicleOwnership.CurrentPageIndex = 0
            BindDtgVehicleOwnership(dtgVehicleOwnership.CurrentPageIndex)
        End If
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal VehicleOwnershipID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "VehicleOwnership", MatchType.Exact, VehicleOwnershipID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

#End Region

End Class
