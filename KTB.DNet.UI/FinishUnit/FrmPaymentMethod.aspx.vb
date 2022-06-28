#Region "Imports Custom NameSpace"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmPaymentMethod
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgPaymentMethod As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents chkStatusPaymentMethod As System.Web.UI.WebControls.CheckBox
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtDeskripsiPaymentMethod As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtCodePaymentMethod As System.Web.UI.WebControls.TextBox
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
        If Not SecurityProvider.Authorize(context.User, SR.FakturKendaraanViewTipePembayaran_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FAKTUR KENDARAAN - Tipe Pembayaran")
        End If
        If Not SecurityProvider.Authorize(context.User, SR.FakturKendaraanUpdateTipePembayaran_Privilege) Then
            Me.btnSimpan.Visible = False
            Me.btnBatal.Visible = False
            Me.dtgPaymentMethod.Columns(6).Visible = False
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        If Not IsPostBack Then
            'Put user code to initialize the page here
            BindDtgPaymentMethod(0)
            initiatePage()
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim objPaymentMethod As PaymentMethod = New PaymentMethod
        Dim objPaymentMethodFacade As PaymentMethodFacade = New PaymentMethodFacade(User)
        'Dim objCityFacade As CityFacade = New CityFacade(User)

        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not txtCodePaymentMethod.Text = String.Empty And Not txtDeskripsiPaymentMethod.Text = String.Empty Then
                If objPaymentMethodFacade.ValidateCode(txtCodePaymentMethod.Text) = 0 Then
                    'masukin data ke objek
                    objPaymentMethod.Code = txtCodePaymentMethod.Text
                    objPaymentMethod.Description = txtDeskripsiPaymentMethod.Text
                    If chkStatusPaymentMethod.Checked = True Then
                        objPaymentMethod.Status = "A"
                    Else
                        objPaymentMethod.Status = "X"
                    End If
                    'masukin data ke objek selesai

                    nResult = New PaymentMethodFacade(User).Insert(objPaymentMethod)

                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        ClearData()
                    End If

                Else
                    MessageBox.Show(SR.DataIsExist("Kode Payment Method"))
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Kode dan Deskripsi Payment Method"))
            End If
        Else
            nResult = UpdatePaymentMethod()
            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
                ClearData()
            End If
        End If

        dtgPaymentMethod.CurrentPageIndex = 0
        BindDtgPaymentMethod(dtgPaymentMethod.CurrentPageIndex)
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub dtgPaymentMethod_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPaymentMethod.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            txtCodePaymentMethod.ReadOnly = True
            txtDeskripsiPaymentMethod.ReadOnly = True
            chkStatusPaymentMethod.Enabled = False
            ViewPaymentMethod(e.Item.Cells(0).Text, False)
            dtgPaymentMethod.SelectedIndex = -1
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewPaymentMethod(e.Item.Cells(0).Text, True)
            dtgPaymentMethod.SelectedIndex = e.Item.ItemIndex
            txtCodePaymentMethod.ReadOnly = True
            txtDeskripsiPaymentMethod.ReadOnly = False
            chkStatusPaymentMethod.Enabled = True
        ElseIf e.CommandName = "Delete" Then
            Try
                DeletePaymentMethod(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearData()
        End If
    End Sub

    Private Sub dtgPaymentMethod_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPaymentMethod.ItemDataBound
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgPaymentMethod.CurrentPageIndex * dtgPaymentMethod.PageSize)
        End If

        If e.Item.Cells(4).Text = "A" Then
            e.Item.Cells(5).Text = "Aktif"
        ElseIf e.Item.Cells(4).Text.Trim = "X" Or e.Item.Cells(4).Text = " " Or e.Item.Cells(4).Text = "&nbsp;" Then
            e.Item.Cells(5).Text = "Tidak Aktif"
        End If
    End Sub

    Private Sub dtgPaymentMethod_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPaymentMethod.PageIndexChanged
        dtgPaymentMethod.SelectedIndex = -1
        dtgPaymentMethod.CurrentPageIndex = e.NewPageIndex
        BindDtgPaymentMethod(dtgPaymentMethod.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgPaymentMethod_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPaymentMethod.SortCommand
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

        dtgPaymentMethod.SelectedIndex = -1
        dtgPaymentMethod.CurrentPageIndex = 0
        BindDtgPaymentMethod(dtgPaymentMethod.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

#Region "Private Method/Function"
    Private Sub BindDtgPaymentMethod(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dtgPaymentMethod.DataSource = New PaymentMethodFacade(User).RetrieveActiveList(indexPage + 1, dtgPaymentMethod.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgPaymentMethod.VirtualItemCount = totalRow
            dtgPaymentMethod.DataBind()
        End If

    End Sub

    Private Sub initiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = ""
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub ClearData()

        txtCodePaymentMethod.Text = String.Empty
        txtDeskripsiPaymentMethod.Text = String.Empty
        chkStatusPaymentMethod.Checked = True
        btnSimpan.Enabled = True
        txtCodePaymentMethod.ReadOnly = False
        txtDeskripsiPaymentMethod.ReadOnly = False
        chkStatusPaymentMethod.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        dtgPaymentMethod.SelectedIndex = -1

    End Sub

    Private Function UpdatePaymentMethod() As Integer

        Dim nResult As Integer
        Dim countRecord As Integer
        Dim statusPaymentMethod As String

        Dim objPaymentMethod As PaymentMethod = CType(Session.Item("vsPaymentMethod"), PaymentMethod)

        If Not txtDeskripsiPaymentMethod.Text = String.Empty Then
            objPaymentMethod.Description = txtDeskripsiPaymentMethod.Text
            If chkStatusPaymentMethod.Checked = True Then
                objPaymentMethod.Status = "A"
            Else
                objPaymentMethod.Status = "X"
            End If
            nResult = New PaymentMethodFacade(User).Update(objPaymentMethod)
        End If
        Return nResult

    End Function

    Private Sub ViewPaymentMethod(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objPaymentMethod As PaymentMethod = New PaymentMethodFacade(User).Retrieve(nID)
        _sessHelper.SetSession("vsPaymentMethod", objPaymentMethod)

        If IsNothing(objPaymentMethod) Then
            txtCodePaymentMethod.Text = ""
            txtDeskripsiPaymentMethod.Text = ""
            chkStatusPaymentMethod.Checked = False
            Me.btnSimpan.Enabled = EditStatus
            MessageBox.Show(SR.ViewFail)
        Else
            txtCodePaymentMethod.Text = objPaymentMethod.Code
            txtDeskripsiPaymentMethod.Text = objPaymentMethod.Description
            If objPaymentMethod.Status = "A" Then
                chkStatusPaymentMethod.Checked = True
            ElseIf objPaymentMethod.Status = "X" Then
                chkStatusPaymentMethod.Checked = False
            End If
            Me.btnSimpan.Enabled = EditStatus

        End If

        
    End Sub

    Private Sub DeletePaymentMethod(ByVal nID As Integer)

        Dim arrRecordExist As ArrayList = New ArrayList
        'Dim arrAreaViolation As ArrayList = New ArrayList
        'Dim arrPenalty As ArrayList = New ArrayList
        Dim objEndCustomerFacade As EndCustomerFacade = New EndCustomerFacade(User)
        Dim crit As New CriteriaComposite(New Criteria(GetType(EndCustomer), "PenaltyPaymentMethodID", MatchType.Exact, nID))
        crit.opOr(New Criteria(GetType(EndCustomer), "AreaViolationPaymentMethodID", MatchType.Exact, nID))

        arrRecordExist = objEndCustomerFacade.Retrieve(crit)
        'arrAreaViolation = objEndCustomerFacade.Retrieve(iteriaComposite(New Criteria(GetType(EndCustomer), "AreaViolationPaymentMethodID", MatchType.Exact, nID)))
        'arrPenalty = objEndCustomerFacade.Retrieve(New CriteriaComposite(New Criteria(GetType(EndCustomer), "PenaltyPaymentMethodID", MatchType.Exact, nID)))

        'If arrAreaViolation.Count > 0 Or arrPenalty.Count > 0 Then
        If arrRecordExist.Count > 0 Then
            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objPaymentMethod As PaymentMethod = New PaymentMethodFacade(User).Retrieve(nID)
            Dim nResult = New PaymentMethodFacade(User).DeleteFromDB(objPaymentMethod)

            dtgPaymentMethod.CurrentPageIndex = 0
            BindDtgPaymentMethod(dtgPaymentMethod.CurrentPageIndex)
        End If
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal PaymentMethodID As Integer) As CriteriaComposite
        'Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EndCustomer), "VechileColor.ID", MatchType.Exact, nTypeID))

        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New CriteriaComposite(New Criteria(DomainType, "AreaViolationPaymentMethodID", MatchType.Exact, PaymentMethodID)))
        criterias.opAnd(New Criteria(DomainType, "PenaltyPaymentMethodID", MatchType.Exact, PaymentMethodID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

#End Region

End Class
