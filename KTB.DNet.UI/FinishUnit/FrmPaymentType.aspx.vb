#Region "Imports Custom NameSpace"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmPaymentType
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgPaymentType As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents chkStatusPaymentType As System.Web.UI.WebControls.CheckBox
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtDeskripsiPaymentType As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtCodePaymentType As System.Web.UI.WebControls.TextBox
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
            BindDtgPaymentType(0)
            initiatePage()
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim objPaymentType As PaymentType = New PaymentType
        Dim objPaymentTypeFacade As PaymentTypeFacade = New PaymentTypeFacade(User)
        'Dim objCityFacade As CityFacade = New CityFacade(User)

        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not txtCodePaymentType.Text = String.Empty And Not txtDeskripsiPaymentType.Text = String.Empty Then
                If objPaymentTypeFacade.ValidateCode(txtCodePaymentType.Text) = 0 Then
                    'masukin data ke objek
                    objPaymentType.Code = txtCodePaymentType.Text
                    objPaymentType.Description = txtDeskripsiPaymentType.Text
                    If chkStatusPaymentType.Checked = True Then
                        objPaymentType.Status = "A"
                    Else
                        objPaymentType.Status = "X"
                    End If
                    'masukin data ke objek selesai

                    nResult = New PaymentTypeFacade(User).Insert(objPaymentType)

                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        ClearData()
                    End If

                Else
                    MessageBox.Show(SR.DataIsExist("Kode Payment Type"))
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Kode dan Deskripsi Payment Type"))
            End If
        Else
            nResult = UpdatePaymentType()
            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
                ClearData()
            End If
        End If

        dtgPaymentType.CurrentPageIndex = 0
        BindDtgPaymentType(dtgPaymentType.CurrentPageIndex)
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub dtgPaymentType_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPaymentType.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            txtCodePaymentType.ReadOnly = True
            txtDeskripsiPaymentType.ReadOnly = True
            chkStatusPaymentType.Enabled = False
            ViewPaymentType(e.Item.Cells(0).Text, False)
            dtgPaymentType.SelectedIndex = -1
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewPaymentType(e.Item.Cells(0).Text, True)
            dtgPaymentType.SelectedIndex = e.Item.ItemIndex
            txtCodePaymentType.ReadOnly = True
            txtDeskripsiPaymentType.ReadOnly = False
            chkStatusPaymentType.Enabled = True
        ElseIf e.CommandName = "Delete" Then
            Try
                DeletePaymentType(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearData()
        End If
    End Sub

    Private Sub dtgPaymentType_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPaymentType.ItemDataBound
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgPaymentType.CurrentPageIndex * dtgPaymentType.PageSize)
        End If

        If e.Item.Cells(4).Text = "A" Then
            e.Item.Cells(5).Text = "Aktif"
        ElseIf e.Item.Cells(4).Text.Trim = "X" Or e.Item.Cells(4).Text = " " Or e.Item.Cells(4).Text = "&nbsp;" Then
            e.Item.Cells(5).Text = "Tidak Aktif"
        End If

    End Sub

    Private Sub dtgPaymentType_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPaymentType.PageIndexChanged
        dtgPaymentType.SelectedIndex = -1
        dtgPaymentType.CurrentPageIndex = e.NewPageIndex
        BindDtgPaymentType(dtgPaymentType.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgPaymentType_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPaymentType.SortCommand
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

        dtgPaymentType.SelectedIndex = -1
        dtgPaymentType.CurrentPageIndex = 0
        BindDtgPaymentType(dtgPaymentType.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

#Region "Private Method/Function"

    Private Sub InitPrivilage()

        If Not SecurityProvider.Authorize(context.User, SR.FakturKendaraanViewCaraPembayaran_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Faktur Kendaraan - Cara Pembayaran")
        End If

        If Not SecurityProvider.Authorize(context.User, SR.FakturKendaraanUpdateCaraPembayaran_Privilege) Then
            Me.btnSimpan.Visible = False
            Me.btnBatal.Visible = False
            Me.dtgPaymentType.Columns(6).Visible = False
        End If

    End Sub

    Private Sub BindDtgPaymentType(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dtgPaymentType.DataSource = New PaymentTypeFacade(User).RetrieveActiveList(indexPage + 1, dtgPaymentType.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgPaymentType.VirtualItemCount = totalRow
            dtgPaymentType.DataBind()
        End If

    End Sub

    Private Sub initiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = ""
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub ClearData()

        txtCodePaymentType.Text = String.Empty
        txtDeskripsiPaymentType.Text = String.Empty
        chkStatusPaymentType.Checked = True
        btnSimpan.Enabled = True
        txtCodePaymentType.ReadOnly = False
        txtDeskripsiPaymentType.ReadOnly = False
        chkStatusPaymentType.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        dtgPaymentType.SelectedIndex = -1

    End Sub

    Private Function UpdatePaymentType() As Integer

        Dim nResult As Integer
        Dim countRecord As Integer
        Dim statusPaymentType As String

        Dim objPaymentType As PaymentType = CType(Session.Item("vsPaymentType"), PaymentType)

        If Not txtDeskripsiPaymentType.Text = String.Empty Then
            objPaymentType.Description = txtDeskripsiPaymentType.Text
            If chkStatusPaymentType.Checked = True Then
                objPaymentType.Status = "A"
            Else
                objPaymentType.Status = "X"
            End If
            nResult = New PaymentTypeFacade(User).Update(objPaymentType)
        End If
        Return nResult

    End Function

    Private Sub ViewPaymentType(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objPaymentType As PaymentType = New PaymentTypeFacade(User).Retrieve(nID)
        _sessHelper.SetSession("vsPaymentType", objPaymentType)

        If IsNothing(objPaymentType) Then
            txtCodePaymentType.Text = ""
            txtDeskripsiPaymentType.Text = ""
            chkStatusPaymentType.Checked = False
            Me.btnSimpan.Enabled = EditStatus
            MessageBox.Show(SR.ViewFail)
        Else
            txtCodePaymentType.Text = objPaymentType.Code
            txtDeskripsiPaymentType.Text = objPaymentType.Description
            If objPaymentType.Status = "A" Then
                chkStatusPaymentType.Checked = True
            ElseIf objPaymentType.Status = "X" Then
                chkStatusPaymentType.Checked = False
            End If
            Me.btnSimpan.Enabled = EditStatus
        End If

        
    End Sub

    Private Sub DeletePaymentType(ByVal nID As Integer)

        If New HelperFacade(User, GetType(EndCustomer)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(EndCustomer), nID), _
            CreateAggreateForCheckRecord(GetType(EndCustomer))) Then
            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objPaymentType As PaymentType = New PaymentTypeFacade(User).Retrieve(nID)
            Dim nResult = New PaymentTypeFacade(User).DeleteFromDB(objPaymentType)

            dtgPaymentType.CurrentPageIndex = 0
            BindDtgPaymentType(dtgPaymentType.CurrentPageIndex)
        End If
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal PaymentTypeID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "PaymentType", MatchType.Exact, PaymentTypeID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

#End Region

End Class
