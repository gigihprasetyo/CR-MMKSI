Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security

Public Class FrmReference
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgReferences As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Const REF_TYPE As String = "TR"
    Private CodeInDb As String() = {"EHT1", "EHT2", "BCCODE", "RMDR"}
    Private CodeInName As String() = {"Evaluasi Hasil Training 1", _
        "Evaluasi Hasil Training 2", _
        "Kode Kategori Dasar", _
        "Reminder"}

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            DataBindGrid()
        End If
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.TrainingReferensi_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Training - Referensi")
        End If
    End Sub

    Private Sub InitiatePage()

    End Sub

    Private Sub DataBindGrid()
        dtgReferences.DataSource = RetrieveReferenceData()
        dtgReferences.DataBind()
    End Sub

    Private Function RetrieveReferenceData() As ArrayList
        Dim totalRow As Integer
        Dim nResult As ArrayList = New ReferenceFacade(User).RetrieveActiveList(REF_TYPE)
        Return nResult
    End Function

    Private Sub dtgReferences_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgReferences.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As Reference = CType(e.Item.DataItem, Reference)

            Dim lblCode As Label = CType(e.Item.FindControl("lblCode"), Label)
            Dim txtDescription As TextBox = CType(e.Item.FindControl("txtDescription"), TextBox)

            If Not IsNothing(RowValue.Code) Then
                lblCode.Text = GetCodeInName(RowValue.Code)
            End If

            If Not IsNothing(RowValue.Description) Then
                txtDescription.Text = RowValue.Description
            End If
        End If
    End Sub

    Private Function GetCodeInName(ByVal inCode As String) As String
        Return CodeInName(Array.IndexOf(CodeInDb, inCode))
    End Function

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        DataBindGrid()
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not IsUnhack() Then
            MessageBox.Show("< dan > bukan karakter valid")
            Return
        End If
        Dim totalUpdate As Integer = SaveAll()
        If totalUpdate >= 0 Then
            MessageBox.Show(SR.SaveSuccess + " " + totalUpdate.ToString + " diubah")
        Else
            MessageBox.Show(SR.SaveFail + " data ke-" + (-totalUpdate).ToString + " gagal diubah")
        End If
        DataBindGrid()
    End Sub

    Private Function SaveAll() As Integer
        Dim nResult As Integer = 0
        Dim objRefFacade As ReferenceFacade = New ReferenceFacade(User)
        For rowIndex As Integer = 0 To dtgReferences.Items.Count - 1
            If dtgReferences.Items(rowIndex).ItemType = ListItemType.AlternatingItem Or dtgReferences.Items(rowIndex).ItemType = ListItemType.Item Then
                Try
                    Dim id As Integer = Integer.Parse(dtgReferences.Items(rowIndex).Cells(0).Text)
                    Dim txtDescription As TextBox = CType(dtgReferences.Items(rowIndex).FindControl("txtDescription"), TextBox)
                    Dim objReferences As Reference = objRefFacade.Retrieve(id)
                    If objReferences.Description <> txtDescription.Text Then
                        objReferences.Description = txtDescription.Text
                        If objRefFacade.Update(objReferences) <> -1 Then
                            nResult += 1
                        End If
                    End If
                Catch
                    Return -rowIndex
                End Try
            End If
        Next
        Return nResult
    End Function

    Private Function IsUnhack() As Boolean
        For rowIndex As Integer = 0 To dtgReferences.Items.Count - 1
            If dtgReferences.Items(rowIndex).ItemType = ListItemType.AlternatingItem Or dtgReferences.Items(rowIndex).ItemType = ListItemType.Item Then
                Try
                    Dim id As Integer = Integer.Parse(dtgReferences.Items(rowIndex).Cells(0).Text)
                    Dim txtDescription As TextBox = CType(dtgReferences.Items(rowIndex).FindControl("txtDescription"), TextBox)
                    If txtDescription.Text.IndexOf("<") >= 0 Or txtDescription.Text.IndexOf(">") >= 0 Or txtDescription.Text.IndexOf("'") >= 0 Then
                        Return False
                    End If
                Catch
                    Return -rowIndex
                End Try
            End If
        Next
        Return True
    End Function
End Class
