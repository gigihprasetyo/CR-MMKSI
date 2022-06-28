#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security

#End Region

Public Class FrmPositionWSC
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents ValidCode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ValidDescription As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ValidCategory As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents dgPosWSC As System.Web.UI.WebControls.DataGrid
    Protected WithEvents CustomValidator1 As System.Web.UI.WebControls.CustomValidator

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private sesHelper As SessionHelper = New SessionHelper
    Private _isDeleteAllowed As Boolean = False
    Private _isEditAllowed As Boolean = False
    Private _isViewAllowed As Boolean = False
#End Region

#Region "Custom Method"

    Private Sub BindData()
        GetDdlPosCategory()
        BindDG(0)
    End Sub

    Private Sub GetDdlPosCategory()
        ddlCategory.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For Each liOrderType As ListItem In LookUp.ArrayPosCategory
            ddlCategory.Items.Add(New ListItem(liOrderType.Text, liOrderType.Value))
        Next
        ddlCategory.SelectedValue = "-1"
        'ddlCategory.DataBind()
    End Sub

    Private Sub BindDG(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            dgPosWSC.DataSource = New PositionWSCFacade(User).RetrieveActiveList(indexPage + 1, dgPosWSC.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dgPosWSC.VirtualItemCount = totalRow
            dgPosWSC.DataBind()
        End If
    End Sub

    Private Sub InitiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = "PositionCategory"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub ClearData()
        txtCode.Text = String.Empty
        txtDescription.Text = String.Empty
        btnSave.Enabled = True
        ddlCategory.SelectedValue = "-1"
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub InsertPosWSC()
        Dim objPosWSCFacade As PositionWSCFacade = New PositionWSCFacade(User)
        Dim objPosWSC As PositionWSC = New PositionWSC
        Dim nResult As Integer
        If ddlCategory.SelectedValue <> "-1" Then
            nResult = objPosWSCFacade.ValidateCode(ddlCategory.SelectedValue, txtCode.Text)
            If nResult = 0 Then
                objPosWSC.PositionCode = txtCode.Text
                objPosWSC.Description = txtDescription.Text
                objPosWSC.PositionCategory = ddlCategory.SelectedValue
                nResult = New PositionWSCFacade(User).Insert(objPosWSC)
                If nResult = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show(SR.SaveSuccess)
                End If
            ElseIf nResult = 0 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.DataIsExist("Posisi WSC"))
            End If
        Else
            MessageBox.Show("Kategori belum terdefinisi")
        End If

    End Sub

    Private Function ViewPosWSC(ByVal nID As Integer, ByVal EditStatus As Boolean) As Boolean
        Dim objPosWSC As PositionWSC
        objPosWSC = New PositionWSCFacade(User).Retrieve(nID)
        If Not objPosWSC Is Nothing Then
            sesHelper.SetSession("vsPosWSC", objPosWSC)
            txtCode.Text = objPosWSC.PositionCode
            txtDescription.Text = objPosWSC.Description
            ddlCategory.SelectedValue = objPosWSC.PositionCategory
            Me.btnSave.Enabled = EditStatus
            Return True
        Else
            Return False
        End If
    End Function

    Private Function UpdatePosWSC() As Integer
        Dim objPosWSC As PositionWSC = CType(Session("vsPosWSC"), PositionWSC)
        Dim nResult = 0
        objPosWSC.PositionCategory = ddlCategory.SelectedValue
        objPosWSC.Description = txtDescription.Text

        nResult = New PositionWSCFacade(User).Update(objPosWSC)
        Return nResult
    End Function

    Private Function IsNotExistOnRelatedEntity(ByVal nID As Integer) As String
        Dim returnValue As String = ""
        Dim fac As WSCHeaderFacade = New WSCHeaderFacade(User)
        Dim posFac As PositionWSCFacade = New PositionWSCFacade(User)
        Dim posWSC As PositionWSC
        posWSC = posFac.Retrieve(nID)
        If Not posWSC Is Nothing Then
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(WSCHeader), "Code" & posWSC.PositionCategory, MatchType.Exact, posWSC.PositionCode))

            Dim aggr As Aggregate = New Aggregate(GetType(WSCHeader), "ID", AggregateType.Count)

            Dim count As Integer
            count = fac.RetrieveScalar(aggr, crit)
            If (count > 0) Then
                returnValue = "Data masih digunakan"
            ElseIf count = -1 Then
                returnValue = SR.DeleteFail
            Else
                returnValue = ""
            End If
        Else
            returnValue = SR.DataNotFound("Kode")
        End If
        Return returnValue
    End Function

    Private Sub DeletePosWSC(ByVal nID As Integer)
        Dim msg As String
        msg = IsNotExistOnRelatedEntity(nID)
        If msg = "" Then
            Dim objPosWSC As PositionWSC
            objPosWSC = New PositionWSCFacade(User).Retrieve(nID)
            If Not objPosWSC Is Nothing Then
                Dim PosWSCFacade As PositionWSCFacade = New PositionWSCFacade(User)

                If PosWSCFacade.DeleteFromDB(objPosWSC) > 0 Then
                    dgPosWSC.CurrentPageIndex = 0
                    BindDG(dgPosWSC.CurrentPageIndex)
                    MessageBox.Show(SR.DeleteSucces)
                Else
                    MessageBox.Show(SR.DeleteFail)
                End If
            Else
                MessageBox.Show(SR.DeleteFail)
            End If
        Else
            MessageBox.Show(msg)
        End If
    End Sub

#End Region

#Region "EventHandler"

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ServiceKodeABCView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=WSC - Daftar Kode ABC")
        End If
        If Not SecurityProvider.Authorize(context.User, SR.ServiceKodeABCUpdate_Privilege) Then
            Me.btnSave.Visible = False
            Me.btnCancel.Visible = False
            Me.dgPosWSC.Columns(5).Visible = False
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        If Not IsPostBack Then
            ViewState("CurrentSortColumn") = "PositionCategory"
            ViewState("CurrentSortTable") = GetType(PositionWSC)
            ViewState("CurrentSortDirection") = Sort.SortDirection.ASC
            BindData()
            InitiatePage()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ddlCategory.Enabled = True
        txtCode.ReadOnly = False
        txtDescription.ReadOnly = False
        ddlCategory.SelectedValue = "-1"
        ClearData()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        ddlCategory.Enabled = True
        txtCode.ReadOnly = False
        txtDescription.ReadOnly = False
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            InsertPosWSC()
        Else
            If UpdatePosWSC() > 0 Then
                MessageBox.Show(SR.UpdateSucces)
            Else
                MessageBox.Show(SR.UpdateFail)
            End If
        End If
        ClearData()
        dgPosWSC.CurrentPageIndex = 0
        BindDG(dgPosWSC.CurrentPageIndex)
    End Sub

    Private Sub dgPosWSC_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPosWSC.ItemCommand
        Select Case e.CommandName
            Case "View"
                If ViewPosWSC(e.Item.Cells(0).Text, False) Then
                    ddlCategory.Enabled = False
                    txtCode.ReadOnly = True
                    txtDescription.ReadOnly = True
                    ViewState.Add("vsProcess", "View")
                    dgPosWSC.SelectedIndex = e.Item.ItemIndex
                Else
                    MessageBox.Show(SR.ViewFail)
                End If
            Case "Edit"
                If ViewPosWSC(e.Item.Cells(0).Text, True) Then
                    ddlCategory.Enabled = False
                    txtCode.ReadOnly = True
                    txtDescription.ReadOnly = False
                    ViewState.Add("vsProcess", "Edit")
                    dgPosWSC.SelectedIndex = e.Item.ItemIndex
                Else
                    MessageBox.Show(SR.ViewFail)
                End If
            Case "Delete"
                DeletePosWSC(e.Item.Cells(0).Text)
                ClearData()
        End Select
    End Sub

    Private Sub dgPosWSC_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPosWSC.ItemDataBound
        If Not e.Item.FindControl("lnkDelete") Is Nothing Then
            CType(e.Item.FindControl("lnkDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dgPosWSC.CurrentPageIndex * dgPosWSC.PageSize)
        End If
    End Sub

    Private Sub dgPosWSC_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPosWSC.PageIndexChanged
        dgPosWSC.CurrentPageIndex = e.NewPageIndex
        BindDG(dgPosWSC.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dgPosWSC_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPosWSC.SortCommand
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

        dgPosWSC.CurrentPageIndex = 0
        BindDG(dgPosWSC.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

End Class