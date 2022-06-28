#Region "Imports Custom NameSpace"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmVehicleBodyShape
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents chkStatusVehicleBodyShape As System.Web.UI.WebControls.CheckBox
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtDeskripsiVehicleBodyShape As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtCodeVehicleBodyShape As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dtgVehicleBodyShape As System.Web.UI.WebControls.DataGrid
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

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.FakturKendaraanViewBentukBody_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FAKTUR KENDARAAN - Bentuk Body Kendaraan")
        End If
        If Not SecurityProvider.Authorize(context.User, SR.FakturKendaraanUpdateBentukBody_Privilege) Then
            Me.btnSimpan.Visible = False
            Me.btnBatal.Visible = False
            Me.dtgVehicleBodyShape.Columns(6).Visible = False
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        If Not IsPostBack Then
            'Put user code to initialize the page here
            BindDdlCategory()
            BindDtgVehicleBodyShape(0)
            initiatePage()
        End If

    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim objVehicleBodyShape As VehicleBodyShape = New VehicleBodyShape
        Dim objVehicleBodyShapeFacade As VehicleBodyShapeFacade = New VehicleBodyShapeFacade(User)
        'Dim objCityFacade As CityFacade = New CityFacade(User)

        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not txtCodeVehicleBodyShape.Text = String.Empty And Not txtDeskripsiVehicleBodyShape.Text = String.Empty Then
                If objVehicleBodyShapeFacade.ValidateCode(CType(ddlCategory.SelectedValue, Integer), txtCodeVehicleBodyShape.Text) = 0 Then

                    'masukin data ke objek
                    objVehicleBodyShape.Code = txtCodeVehicleBodyShape.Text
                    objVehicleBodyShape.Description = txtDeskripsiVehicleBodyShape.Text
                    objVehicleBodyShape.Category = New CategoryFacade(User).Retrieve(CType(ddlCategory.SelectedValue, Integer))
                    If chkStatusVehicleBodyShape.Checked = True Then
                        objVehicleBodyShape.Status = "A"
                    Else
                        objVehicleBodyShape.Status = "X"
                    End If
                    'masukin data ke objek selesai

                    nResult = New VehicleBodyShapeFacade(User).Insert(objVehicleBodyShape)

                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        ClearData()
                    End If

                Else
                    MessageBox.Show(SR.DataIsExist("Kode Vehicle Body Shape"))
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Kode dan Deskripsi Vehicle Body Shape"))
            End If
        Else
            nResult = UpdateVehicleBodyShape()
            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
                ClearData()
            End If
        End If

        dtgVehicleBodyShape.CurrentPageIndex = 0
        BindDtgVehicleBodyShape(dtgVehicleBodyShape.CurrentPageIndex)
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub dtgVehicleBodyShape_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgVehicleBodyShape.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            txtCodeVehicleBodyShape.ReadOnly = True
            ddlCategory.Enabled = False
            txtDeskripsiVehicleBodyShape.ReadOnly = True
            chkStatusVehicleBodyShape.Enabled = False
            ViewVehicleBodyShape(e.Item.Cells(0).Text, False)
            dtgVehicleBodyShape.SelectedIndex = -1
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewVehicleBodyShape(e.Item.Cells(0).Text, True)
            dtgVehicleBodyShape.SelectedIndex = e.Item.ItemIndex
            txtCodeVehicleBodyShape.ReadOnly = True
            ddlCategory.Enabled = True
            txtDeskripsiVehicleBodyShape.ReadOnly = False
            chkStatusVehicleBodyShape.Enabled = True
        ElseIf e.CommandName = "Delete" Then
            Try
                DeleteVehicleBodyShape(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearData()
        End If
    End Sub

    Private Sub dtgVehicleBodyShape_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgVehicleBodyShape.ItemDataBound
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgVehicleBodyShape.CurrentPageIndex * dtgVehicleBodyShape.PageSize)
        End If

        If e.Item.Cells(5).Text = "A" Then
            e.Item.Cells(6).Text = "Aktif"
        ElseIf e.Item.Cells(5).Text.Trim = "X" Or e.Item.Cells(5).Text = " " Or e.Item.Cells(5).Text = "&nbsp;" Then
            e.Item.Cells(6).Text = "Tidak Aktif"
        End If


    End Sub

    Private Sub dtgVehicleBodyShape_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgVehicleBodyShape.PageIndexChanged
        dtgVehicleBodyShape.SelectedIndex = -1
        dtgVehicleBodyShape.CurrentPageIndex = e.NewPageIndex
        BindDtgVehicleBodyShape(dtgVehicleBodyShape.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgVehicleBodyShape_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgVehicleBodyShape.SortCommand
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

        dtgVehicleBodyShape.SelectedIndex = -1
        dtgVehicleBodyShape.CurrentPageIndex = 0
        BindDtgVehicleBodyShape(dtgVehicleBodyShape.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub BindDdlCategory()
        ddlCategory.DataSource = New CategoryFacade(User).RetrieveList("CategoryCode", Sort.SortDirection.ASC)
        ddlCategory.DataTextField = "CategoryCode"
        ddlCategory.DataValueField = "ID"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("", ""))
    End Sub

    Private Sub BindDtgVehicleBodyShape(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            dtgVehicleBodyShape.DataSource = New VehicleBodyShapeFacade(User).RetrieveActiveList(indexPage + 1, dtgVehicleBodyShape.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgVehicleBodyShape.VirtualItemCount = totalRow
            dtgVehicleBodyShape.DataBind()
        End If

    End Sub

    Private Sub ClearData()

        txtCodeVehicleBodyShape.Text = String.Empty
        txtDeskripsiVehicleBodyShape.Text = String.Empty
        btnSimpan.Enabled = True
        ddlCategory.Enabled = True
        ddlCategory.SelectedValue = ""
        chkStatusVehicleBodyShape.Checked = True
        ViewState.Add("vsProcess", "Insert")
        dtgVehicleBodyShape.SelectedIndex = -1
        txtCodeVehicleBodyShape.ReadOnly = False
        txtDeskripsiVehicleBodyShape.ReadOnly = False

    End Sub

    Private Sub initiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = ""
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub



    Private Function UpdateVehicleBodyShape() As Integer

        Dim nResult As Integer
        Dim countRecord As Integer
        Dim statusVehicleBodyShape As String

        Dim objVehicleBodyShape As VehicleBodyShape = CType(Session.Item("vsVehicleBodyShape"), VehicleBodyShape)

        If Not txtDeskripsiVehicleBodyShape.Text = String.Empty Then
            objVehicleBodyShape.Description = txtDeskripsiVehicleBodyShape.Text
            objVehicleBodyShape.Category = New CategoryFacade(User).Retrieve(CType(ddlCategory.SelectedValue, Integer))
            If chkStatusVehicleBodyShape.Checked = True Then
                objVehicleBodyShape.Status = "A"
            Else
                objVehicleBodyShape.Status = "X"
            End If
            nResult = New VehicleBodyShapeFacade(User).Update(objVehicleBodyShape)
        End If
        Return nResult

    End Function

    Private Sub ViewVehicleBodyShape(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objVehicleBodyShape As VehicleBodyShape = New VehicleBodyShapeFacade(User).Retrieve(nID)
        _sessHelper.SetSession("vsVehicleBodyShape", objVehicleBodyShape)

        If IsNothing(objVehicleBodyShape.Category) Then
            ddlCategory.SelectedValue = ""
            txtCodeVehicleBodyShape.Text = ""
            txtDeskripsiVehicleBodyShape.Text = ""
            chkStatusVehicleBodyShape.Checked = False
            MessageBox.Show(SR.ViewFail)
        Else
            ddlCategory.SelectedValue = CType(objVehicleBodyShape.Category.ID, String)
            txtCodeVehicleBodyShape.Text = objVehicleBodyShape.Code
            txtDeskripsiVehicleBodyShape.Text = objVehicleBodyShape.Description
            If objVehicleBodyShape.Status = "A" Then
                chkStatusVehicleBodyShape.Checked = True
            ElseIf objVehicleBodyShape.Status = "X" Then
                chkStatusVehicleBodyShape.Checked = False
            End If
        End If
        Me.btnSimpan.Enabled = EditStatus
        
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal VehicleBodyShapeID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "VehicleBodyShape", MatchType.Exact, VehicleBodyShapeID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Sub DeleteVehicleBodyShape(ByVal nID As Integer)

        'Dim objVehicleBodyShape As VehicleBodyShape = New VehicleBodyShapeFacade(User).Retrieve(nID)

        'Dim nResult = New VehicleBodyShapeFacade(User).DeleteFromDB(objVehicleBodyShape)

        'dtgVehicleBodyShape.CurrentPageIndex = 0
        'BindDtgVehicleBodyShape(dtgVehicleBodyShape.CurrentPageIndex)

        If New HelperFacade(User, GetType(EndCustomer)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(EndCustomer), nID), _
            CreateAggreateForCheckRecord(GetType(EndCustomer))) Then
            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objVehicleBodyShape As VehicleBodyShape = New VehicleBodyShapeFacade(User).Retrieve(nID)
            Dim nResult = New VehicleBodyShapeFacade(User).DeleteFromDB(objVehicleBodyShape)

            dtgVehicleBodyShape.CurrentPageIndex = 0
            BindDtgVehicleBodyShape(dtgVehicleBodyShape.CurrentPageIndex)
        End If

    End Sub

End Class
