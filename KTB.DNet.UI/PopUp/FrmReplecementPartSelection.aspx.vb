Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement

Public Class FrmReplecementPartSelection
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Dim _sessHelper As New SessionHelper
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents dtgPartSelection As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSimpan2 As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        BindSearch()

        If Not Integer.Parse(Request.Params("ID")) = 0 Then
            Dim ID As Integer = Integer.Parse(Request.Params("ID"))
            BindPart(ID)
            btnChoose.Visible = False
            btnSimpan2.Visible = True

        Else
            If dtgPartSelection.Items.Count > 0 Then
                btnChoose.Disabled = False
            Else
                btnChoose.Disabled = True
            End If
        End If
    End Sub
    Public Sub BindSearch()
        dtgPartSelection.DataSource = New ReplecementPartMasterFacade(User).RetrieveList("Code", Sort.SortDirection.ASC)
        _sessHelper.SetSession("RP", dtgPartSelection.DataSource)
        dtgPartSelection.DataBind()
    End Sub
    Public Sub BindPart(ByVal id As Integer)
        Dim objPMHeader As New PMHeader
        Dim _arrPM As New ArrayList
        objPMHeader = New PMHeaderFacade(User).Retrieve(id)

        For Each item As PMDetail In objPMHeader.PMDetails
            _arrPM.Add(item)
        Next

        Dim nIndeks As Integer
        For Each dtgItem As DataGridItem In dtgPartSelection.Items
            nIndeks = dtgItem.ItemIndex
            Dim objPM As ReplecementPartMaster = CType(CType(dtgPartSelection.DataSource, ArrayList)(nIndeks), ReplecementPartMaster)

            For Each _itemPM As PMDetail In _arrPM
                If _itemPM.ReplecementPartMaster.ID = objPM.ID Then
                    If Not CType(dtgItem.Cells(0).FindControl("chkItemChecked"), CheckBox) Is Nothing Then
                        CType(dtgItem.Cells(0).FindControl("chkItemChecked"), CheckBox).Checked = True

                    End If
                End If
            Next

        Next
    End Sub

    Private Sub btnSimpan2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan2.Click
        Dim objPMHeader As New PMHeader
        Dim _OLDPM As New ArrayList
        Dim _NEWPM As New ArrayList
        Dim nResult As Integer = 0

        objPMHeader = New PMHeaderFacade(User).Retrieve(Integer.Parse(Request.Params("ID")))
        For Each item As PMDetail In objPMHeader.PMDetails
            _OLDPM.Add(item)
        Next
        _NEWPM = GetCheckedPMItem()

        nResult = New PMHeaderFacade(User).UpdateTransaction(objPMHeader, _OLDPM, _NEWPM)
        If nResult = -1 Then
            MessageBox.Show("Update Gagal")
        Else
            MessageBox.Show("Update Sukses")
            Response.Write("<script language='javascript'>window.close();</script>")
        End If
    End Sub
    Private Function GetCheckedPMItem() As ArrayList
        dtgPartSelection.DataSource = CType(_sessHelper.GetSession("RP"), ArrayList)

        Dim arlCheckedPMItem As ArrayList = New ArrayList
        Dim nIndeks As Integer
        For Each dtgItem As DataGridItem In dtgPartSelection.Items
            nIndeks = dtgItem.ItemIndex
            Dim objPM As ReplecementPartMaster = CType(CType(dtgPartSelection.DataSource, ArrayList)(nIndeks), ReplecementPartMaster)
            If CType(dtgItem.Cells(0).FindControl("chkItemChecked"), CheckBox).Checked Then
                Dim objPMDetails As New PMDetail
                objPMDetails.ReplecementPartMaster = objPM
                arlCheckedPMItem.Add(objPMDetails)
            End If
        Next
        Return arlCheckedPMItem
    End Function
End Class
