Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Public Class FrmSalesmanTrainingType
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents txtTrainingType As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents dgType As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private sHelper As SessionHelper = New SessionHelper
    Private arlType As ArrayList = New ArrayList
    Dim objSalesmanTrainingType As SalesmanTrainingType = New SalesmanTrainingType
    Dim objSalesmanTrainingTypeFacade As SalesmanTrainingTypeFacade = New SalesmanTrainingTypeFacade(User)
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            ClearData()
            sHelper.SetSession("SortCol", "TrainingType")
            sHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
            sHelper.SetSession("SessArlType", arlType)
            If Request.QueryString("id") <> String.Empty Then 'Edit
                DisplayData(CInt(Request.QueryString("id")))
            End If
            BindDataGridType(0)
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim result As Boolean
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            result = InsertSalesmanTrainingType()
        ElseIf CType(ViewState("vsProcess"), String) = "Edit" Then
            result = UpdateSalesmanTrainingType()
        End If

        dgType.CurrentPageIndex = 0
        BindDataGridType(dgType.CurrentPageIndex)
        If result = True Then
            ClearData()
        End If

    End Sub
    Private Sub DisplayData(ByVal ID As Integer)
        'Dim objTraining As SalesmanTrainingType = New SalesmanTrainingTypeFacade(User).Retrieve(ID)
        'txtTrainingType.Text = objTraining.TrainingType

        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanTrainingType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(SalesmanTrainingType), "ID", MatchType.Exact, ID))

        'arlType = New SalesmanTrainingTypeFacade(User).RetrieveByCriteria(criterias, 1, 100, 100)
        'sHelper.SetSession("SessArlType", arlType)
        ''BindDataGridType(0)
    End Sub
    Private Sub BindDataGridType(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanTrainingType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(SalesmanTrainingType), "ID", MatchType.Exact, Request.QueryString("id")))

            arlType = New SalesmanTrainingTypeFacade(User).RetrieveActiveList(indexPage, dgType.PageSize, totalRow, sHelper.GetSession("SortCol"), sHelper.GetSession("SortDirection"), criterias)
            dgType.DataSource = arlType
            dgType.VirtualItemCount = totalRow
            dgType.DataBind()
            sHelper.SetSession("SessArlType", arlType)
        End If
    End Sub
    Private Function InsertSalesmanTrainingType() As Boolean

        Dim nResult As Integer

        If objSalesmanTrainingTypeFacade.ValidateCode(txtTrainingType.Text) = 0 Then
            objSalesmanTrainingType.TrainingType = txtTrainingType.Text

            nResult = New SalesmanTrainingTypeFacade(User).Insert(objSalesmanTrainingType)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
                Return False
            Else
                MessageBox.Show(SR.SaveSuccess)
                Return True
            End If
        Else
            MessageBox.Show(SR.DataIsExist("Jenis Training"))
            Return False
        End If

    End Function
    Private Function UpdateSalesmanTrainingType() As Boolean
        Dim nResult As Integer
        Dim Idedit As Integer = CType(sHelper.GetSession("objedit"), SalesmanTrainingType).ID
        If objSalesmanTrainingTypeFacade.ValidateCode(txtTrainingType.Text) = 0 Then
            objSalesmanTrainingType.ID = Idedit
            objSalesmanTrainingType.TrainingType = txtTrainingType.Text
            nResult = New SalesmanTrainingTypeFacade(User).Update(objSalesmanTrainingType)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
                Return False
            Else
                MessageBox.Show(SR.SaveSuccess)
                Return True
            End If
        Else
            MessageBox.Show(SR.DataIsExist("Jenis Training"))
            Return False
        End If

    End Function
    Public Sub ClearData()
        txtTrainingType.Text = String.Empty
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub dgType_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgType.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            objSalesmanTrainingType = arlType(e.Item.ItemIndex)
            Dim _lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            _lblNo.Text = e.Item.ItemIndex + 1 + (dgType.CurrentPageIndex * dgType.PageSize)

            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            lbtnDelete.CommandArgument = objSalesmanTrainingType.ID

            'lbtnEdit
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lbtnEdit.CommandArgument = objSalesmanTrainingType.ID
        End If
    End Sub

    Private Sub dgType_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgType.ItemCommand
        If e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            dgType.SelectedIndex = e.Item.ItemIndex
            Dim objSalesmanTrainingType As SalesmanTrainingType = New SalesmanTrainingTypeFacade(User).Retrieve(CInt(e.CommandArgument))
            sHelper.SetSession("objedit", objSalesmanTrainingType)
            ViewType(e.Item.Cells(0).Text, True)


        ElseIf e.CommandName = "Delete" Then
            DeleteForumCategory(e.Item.Cells(0).Text)
            ClearData()
        End If
    End Sub
    Private Sub DeleteForumCategory(ByVal nID As Integer)
        Dim iRecordCount As Integer = 0
        Dim objSalesmanTrainingType As SalesmanTrainingType = New SalesmanTrainingTypeFacade(User).Retrieve(nID)

        Try
            objSalesmanTrainingTypeFacade.DeleteFromDB(objSalesmanTrainingType)
        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
        End Try

        BindDataGridType(dgType.CurrentPageIndex)
    End Sub

    Private Sub ViewType(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objSalesmanTrainingType As SalesmanTrainingType = New SalesmanTrainingTypeFacade(User).Retrieve(nID)
        txtTrainingType.Text = objSalesmanTrainingType.TrainingType
        btnSave.Enabled = EditStatus
    End Sub

    Private Sub dgType_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgType.SortCommand
        If e.SortExpression = sHelper.GetSession("SortCol") Then
            If sHelper.GetSession("SortDirection") = Sort.SortDirection.ASC Then
                sHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
            Else
                sHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        sHelper.SetSession("SortCol", e.SortExpression)
        dgType.SelectedIndex = -1
        dgType.CurrentPageIndex = 0
        BindDataGridType(0)
    End Sub

    Private Sub dgType_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgType.PageIndexChanged
        dgType.CurrentPageIndex = e.NewPageIndex
        BindDataGridType(dgType.CurrentPageIndex)
    End Sub
End Class
