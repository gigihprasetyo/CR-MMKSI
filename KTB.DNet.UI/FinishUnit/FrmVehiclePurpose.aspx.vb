#Region "Imports Custom NameSpace"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmVehiclePurpose
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Private _sessHelper As SessionHelper = New SessionHelper
    Protected WithEvents txtCodeVehiclePurpose As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDeskripsiVehiclePurpose As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgVehiclePurpose As System.Web.UI.WebControls.DataGrid
    Protected WithEvents chkStatusVehiclePurpose As System.Web.UI.WebControls.CheckBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Triggers"

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.FakturKendaraanViewKendaraanSebagai_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FAKTUR KENDARAAN - Kendaraan Sebagai")
        End If
        If Not SecurityProvider.Authorize(context.User, SR.FakturKendaraanUpdateKendaraanSebagai_Privilege) Then
            Me.btnSimpan.Visible = False
            Me.btnBatal.Visible = False
            Me.dtgVehiclePurpose.Columns(6).Visible = False
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        If Not IsPostBack Then
            'Put user code to initialize the page here
            BindDtgVehiclePurpose(0)
            initiatePage()
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim objVehiclePurpose As VehiclePurpose = New VehiclePurpose
        Dim objVehiclePurposeFacade As VehiclePurposeFacade = New VehiclePurposeFacade(User)
        'Dim objCityFacade As CityFacade = New CityFacade(User)

        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not txtCodeVehiclePurpose.Text = String.Empty And Not txtDeskripsiVehiclePurpose.Text = String.Empty Then
                If objVehiclePurposeFacade.ValidateCode(txtCodeVehiclePurpose.Text) = 0 Then
                    'masukin data ke objek
                    objVehiclePurpose.Code = txtCodeVehiclePurpose.Text
                    objVehiclePurpose.Description = txtDeskripsiVehiclePurpose.Text
                    If chkStatusVehiclePurpose.Checked = True Then
                        objVehiclePurpose.Status = "A"
                    Else
                        objVehiclePurpose.Status = "X"
                    End If
                    'masukin data ke objek selesai

                    nResult = New VehiclePurposeFacade(User).Insert(objVehiclePurpose)

                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        ClearData()
                    End If

                Else
                    MessageBox.Show(SR.DataIsExist("Kode Vehicle Purpose"))
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Kode dan Deskripsi Vehicle Purpose"))
            End If
        Else
            nResult = UpdateVehiclePurpose()
            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
                ClearData()
            End If
        End If

        dtgVehiclePurpose.CurrentPageIndex = 0
        BindDtgVehiclePurpose(dtgVehiclePurpose.CurrentPageIndex)
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub dtgVehiclePurpose_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgVehiclePurpose.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            txtCodeVehiclePurpose.ReadOnly = True
            txtDeskripsiVehiclePurpose.ReadOnly = True
            chkStatusVehiclePurpose.Enabled = False
            ViewVehiclePurpose(e.Item.Cells(0).Text, False)
            dtgVehiclePurpose.SelectedIndex = -1
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewVehiclePurpose(e.Item.Cells(0).Text, True)
            dtgVehiclePurpose.SelectedIndex = e.Item.ItemIndex
            txtCodeVehiclePurpose.ReadOnly = True
            txtDeskripsiVehiclePurpose.ReadOnly = False
            chkStatusVehiclePurpose.Enabled = True
        ElseIf e.CommandName = "Delete" Then
            Try
                DeleteVehiclePurpose(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearData()
        End If
    End Sub

    Private Sub dtgVehiclePurpose_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgVehiclePurpose.ItemDataBound
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgVehiclePurpose.CurrentPageIndex * dtgVehiclePurpose.PageSize)
        End If

        If e.Item.Cells(4).Text = "A" Then
            e.Item.Cells(5).Text = "Aktif"
        ElseIf e.Item.Cells(4).Text.Trim = "X" Or e.Item.Cells(4).Text = " " Or e.Item.Cells(4).Text = "&nbsp;" Then
            e.Item.Cells(5).Text = "Tidak Aktif"
        End If
    End Sub

    Private Sub dtgVehiclePurpose_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgVehiclePurpose.PageIndexChanged
        dtgVehiclePurpose.SelectedIndex = -1
        dtgVehiclePurpose.CurrentPageIndex = e.NewPageIndex
        BindDtgVehiclePurpose(dtgVehiclePurpose.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgVehiclePurpose_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgVehiclePurpose.SortCommand
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

        dtgVehiclePurpose.SelectedIndex = -1
        dtgVehiclePurpose.CurrentPageIndex = 0
        BindDtgVehiclePurpose(dtgVehiclePurpose.CurrentPageIndex)
        ClearData()
    End Sub
#End Region

#Region "Private Method/Function"

    Private Sub BindDtgVehiclePurpose(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dtgVehiclePurpose.DataSource = New VehiclePurposeFacade(User).RetrieveActiveList(indexPage + 1, dtgVehiclePurpose.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgVehiclePurpose.VirtualItemCount = totalRow
            dtgVehiclePurpose.DataBind()
        End If

    End Sub

    Private Sub initiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = ""
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub ClearData()

        txtCodeVehiclePurpose.Text = String.Empty
        txtDeskripsiVehiclePurpose.Text = String.Empty
        chkStatusVehiclePurpose.Checked = True
        btnSimpan.Enabled = True
        txtCodeVehiclePurpose.ReadOnly = False
        txtDeskripsiVehiclePurpose.ReadOnly = False
        chkStatusVehiclePurpose.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        dtgVehiclePurpose.SelectedIndex = -1

    End Sub

    Private Function UpdateVehiclePurpose() As Integer

        Dim nResult As Integer
        Dim countRecord As Integer
        Dim statusVehiclePurpose As String

        Dim objVehiclePurpose As VehiclePurpose = CType(Session.Item("vsVehiclePurpose"), VehiclePurpose)

        If Not txtDeskripsiVehiclePurpose.Text = String.Empty Then
            objVehiclePurpose.Description = txtDeskripsiVehiclePurpose.Text
            If chkStatusVehiclePurpose.Checked = True Then
                objVehiclePurpose.Status = "A"
            Else
                objVehiclePurpose.Status = "X"
            End If
            nResult = New VehiclePurposeFacade(User).Update(objVehiclePurpose)
        End If
        Return nResult

    End Function

    Private Sub ViewVehiclePurpose(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objVehiclePurpose As VehiclePurpose = New VehiclePurposeFacade(User).Retrieve(nID)
        _sessHelper.SetSession("vsVehiclePurpose", objVehiclePurpose)

        If IsNothing(objVehiclePurpose) Then
            txtCodeVehiclePurpose.Text = ""
            txtDeskripsiVehiclePurpose.Text = ""
            chkStatusVehiclePurpose.Checked = False
            MessageBox.Show(SR.ViewFail)
        Else
            txtCodeVehiclePurpose.Text = objVehiclePurpose.Code
            txtDeskripsiVehiclePurpose.Text = objVehiclePurpose.Description
            If objVehiclePurpose.Status = "A" Then
                chkStatusVehiclePurpose.Checked = True
            ElseIf objVehiclePurpose.Status = "X" Then
                chkStatusVehiclePurpose.Checked = False
            End If
        End If
        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub DeleteVehiclePurpose(ByVal nID As Integer)

        If New HelperFacade(User, GetType(EndCustomer)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(EndCustomer), nID), _
            CreateAggreateForCheckRecord(GetType(EndCustomer))) Then
            MessageBox.Show(SR.CannotDelete)
        Else

            Dim objVehiclePurpose As VehiclePurpose = New VehiclePurposeFacade(User).Retrieve(nID)
            Dim nResult = New VehiclePurposeFacade(User).DeleteFromDB(objVehiclePurpose)

            dtgVehiclePurpose.CurrentPageIndex = 0
            BindDtgVehiclePurpose(dtgVehiclePurpose.CurrentPageIndex)
        End If

    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal VehiclePurposeID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "VehiclePurpose", MatchType.Exact, VehiclePurposeID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

#End Region

End Class
