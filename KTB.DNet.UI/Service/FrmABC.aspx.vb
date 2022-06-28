Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Domain.Search

Public Class FrmABC
    Inherits System.Web.UI.Page
    Private m_bFormPrivilege As Boolean = False
    Private sessHelper As New SessionHelper

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents dtgArea As System.Web.UI.WebControls.DataGrid
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtTipeABC As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeABC As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPosisiABC As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Method"
    Private Sub InitiatePage()
        ClearData()
        ActivateUserPrivilege()
        ViewState("CurrentSortColumn") = "CategoryCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

   

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.ENHABCEdit_Privilege)
        If Not SecurityProvider.Authorize(Context.User, SR.ENHABCView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=WSC - Kode Failure")
        End If
    End Sub
    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)
        If txtTipeABC.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(KodePostionWSC), "CategoryCode", MatchType.Exact, txtTipeABC.Text.Trim.ToUpper))
        End If
        If txtKodeABC.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(KodePostionWSC), "PostionCode", MatchType.Exact, txtKodeABC.Text.Trim.ToUpper))
        End If
        If txtPosisiABC.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(KodePostionWSC), "Code", MatchType.Exact, txtPosisiABC.Text.Trim.ToUpper))
        End If
    End Sub
    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            dtgArea.DataSource = New KodePostionWSCFacade(User).RetrieveActiveList(CType(sessHelper.GetSession("CRITERIAS"), CriteriaComposite), indexPage + 1, dtgArea.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            dtgArea.VirtualItemCount = totalRow
            dtgArea.DataBind()
        End If
    End Sub

    Private Sub ClearData()
        txtTipeABC.Text = String.Empty
        txtKodeABC.Text = String.Empty
        txtPosisiABC.Text = String.Empty

        dtgArea.SelectedIndex = -1
    End Sub






    Private Sub DeleteABC(ByVal nID As Integer)
        Dim nResult As Integer = -1
        Try
            Dim objABC As KodePostionWSC = New KodePostionWSCFacade(User).Retrieve(nID)
            Dim facade As KodePostionWSCFacade = New KodePostionWSCFacade(User)
            nResult = facade.DeleteFromDB(objABC)
            MessageBox.Show(SR.DeleteSucces)
            dtgArea.CurrentPageIndex = 0
            BindDatagrid(dtgArea.CurrentPageIndex)
        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
            dtgArea.SelectedIndex = -1
            ClearData()
        End Try

    End Sub


#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ActivateUserPrivilege()
        If Not IsPostBack Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KodePostionWSC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            CreateCriteria(criterias)
            sessHelper.SetSession("CRITERIAS", criterias)
            BindDatagrid(0)
            InitiatePage()
        End If
    End Sub


    Private Function IsRecordExist(ByVal catCode As String, ByVal posCode As String, ByVal code As String) As Boolean
        Dim objKodePosisiFacade As KodePostionWSCFacade = New KodePostionWSCFacade(User)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KodePostionWSC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KodePostionWSC), "CategoryCode", MatchType.Exact, catCode))
        criterias.opAnd(New Criteria(GetType(KodePostionWSC), "PostionCode", MatchType.Exact, posCode))
        criterias.opAnd(New Criteria(GetType(KodePostionWSC), "Code", MatchType.Exact, code))
        Dim oldObjList As ArrayList = objKodePosisiFacade.Retrieve(criterias)
        If oldObjList.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub InsertABC()
        Dim objABC As KodePostionWSC = New KodePostionWSC
        Dim facade As KodePostionWSCFacade = New KodePostionWSCFacade(User)
        Dim nResult As Integer = -1
        If (txtTipeABC.Text <> String.Empty) And (txtKodeABC.Text <> String.Empty) And (txtPosisiABC.Text <> String.Empty) Then
            If Not IsRecordExist(txtTipeABC.Text.Trim, txtKodeABC.Text.Trim, txtPosisiABC.Text.Trim) Then
                objABC.CategoryCode = txtTipeABC.Text.Trim.ToUpper
                objABC.PostionCode = txtKodeABC.Text.Trim.ToUpper
                objABC.Code = txtPosisiABC.Text.Trim.ToUpper
                facade.Insert(objABC)
                MessageBox.Show(SR.SaveSuccess)
            Else
                MessageBox.Show("Data Sudah Ada.")
            End If
        Else
            MessageBox.Show(SR.SaveFail)
        End If
    End Sub







    Private Sub dtgArea_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgArea.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgArea.CurrentPageIndex * dtgArea.PageSize)
        End If
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = m_bFormPrivilege
        End If
    End Sub

    Private Sub dtgArea_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgArea.ItemCommand

        If e.CommandName = "Delete" Then
            DeleteABC(e.Item.Cells(0).Text)
            ClearData()
        End If
    End Sub

    Private Sub dtgArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgArea.SelectedIndexChanged

    End Sub

    Private Sub dtgArea_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgArea.SortCommand
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

        dtgArea.SelectedIndex = -1
        dtgArea.CurrentPageIndex = 0
        BindDatagrid(dtgArea.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgArea_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgArea.PageIndexChanged
        dtgArea.SelectedIndex = -1
        dtgArea.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgArea.CurrentPageIndex)
        ClearData()
    End Sub
#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        InsertABC()
        dtgArea.CurrentPageIndex = 0
        ClearData()
        BindDatagrid(dtgArea.CurrentPageIndex)

    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KodePostionWSC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(criterias)
        sessHelper.SetSession("CRITERIAS", criterias)
        dtgArea.CurrentPageIndex = 0
        BindDatagrid(dtgArea.CurrentPageIndex)

    End Sub
End Class



