Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.UI

Public Class ReferenceHelper
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dtgReferences As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private TypeInDb As String() = {"TR", ""}
    Private TypeInName As String() = {"Training", ""}
    Private CodeInDb As String() = {"EHT", "BCCODE", "RMDR", "MNGR", "RPTR", "TMPTR", "RINF1", "RINF2", "RINF3"}
    Private CodeInName As String() = {"Evaluasi Hasil Training", _
        "Kode Kategori Dasar", _
        "Reminder", _
        "EHT Mengetahui", _
        "EHT Dilaporkan", _
        "Tempat Cetak EHT", _
        "Info1 Cetak Reminder", _
        "Info2 Cetak Reminder", _
        "Penandatangan Cetak Reminder"}

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ActivatePrivilege()
        If Not IsPostBack Then
            InitiatePage()
            DataBindGrid()
        End If
    End Sub

    Private Sub ActivatePrivilege()
        'SR.TrainingReferensi_Privilege
        Dim objDealer As Dealer = CType(New SessionHelper().GetSession("DEALER"), Dealer)
        If Not IsNothing(objDealer) Then
            If Not SecurityProvider.Authorize(Context.User, SR.TrainingReferensi_Privilege) Or objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Referensi")
            End If
        Else
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Referensi")
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
        Dim nResult As ArrayList = New ReferenceFacade(User).RetrieveList()
        Return nResult
    End Function

    Private Sub dtgReferences_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgReferences.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As Reference = CType(e.Item.DataItem, Reference)

            Dim lblType As Label = CType(e.Item.FindControl("lblType"), Label)
            Dim lblCode As Label = CType(e.Item.FindControl("lblCode"), Label)
            Dim txtDescription As TextBox = CType(e.Item.FindControl("txtDescription"), TextBox)

            If Not IsNothing(RowValue.Type) Then
                lblType.Text = GetTypeInName(RowValue.Type)
            End If

            If Not IsNothing(RowValue.Code) Then
                lblCode.Text = GetCodeInName(RowValue.Code)
            End If

            If Not IsNothing(RowValue.Description) Then
                txtDescription.Text = RowValue.Description
            End If
        End If
    End Sub

    Private Function GetTypeInName(ByVal inDB As String) As String
        Dim num As Integer = Array.IndexOf(TypeInDb, inDB)
        If num < 0 Then Return ""
        Return TypeInName(num)
    End Function

    Private Function GetCodeInName(ByVal inCode As String) As String
        Dim num As Integer = Array.IndexOf(CodeInDb, inCode)
        If num < 0 Then Return ""
        Return CodeInName(num)
    End Function

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        DataBindGrid()
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
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
        Next
        Return nResult
    End Function
End Class
