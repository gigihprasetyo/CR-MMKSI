Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement

Public Class PopReplecementPartSelection
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
        If Not IsPostBack() Then
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
        Dim bcheck As Boolean = False
        Dim nResult As Integer = 0

        dtgPartSelection.DataSource = CType(Session("RP"), ArrayList)
        For Each dtgItem As DataGridItem In dtgPartSelection.Items
            If CType(dtgItem.Cells(0).FindControl("chkItemChecked"), CheckBox).Checked Then
                bcheck = True
                Exit For
            End If
        Next
        Try
            If bcheck Then
                objPMHeader = New PMHeaderFacade(User).Retrieve(Integer.Parse(Request.Params("ID")))
                For Each item As PMDetail In objPMHeader.PMDetails
                    _OLDPM.Add(item)
                Next

                Dim _NEWPM As New ArrayList
                _NEWPM = GetCheckedPMItem()
                If _NEWPM.Count > 0 Then
                    nResult = New PMHeaderFacade(User).UpdateTransaction(objPMHeader, _OLDPM, _NEWPM)
                    If nResult = -1 Then
                        MessageBox.Show("Update Gagal")
                    Else
                        Response.Write("<script language='javascript'>alert('Update Sukses');window.close();</script>")
                    End If
                Else
                    MessageBox.Show("Part belum dipilih")
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Update Gagal")
        End Try

    End Sub

    Private Function GetCheckedPMItem() As ArrayList
        Dim arlCheckedPMItem As New ArrayList
        Dim nIndeks As Integer
        dtgPartSelection.DataSource = CType(Session("RP"), ArrayList)

        For Each dtgItem As DataGridItem In dtgPartSelection.Items
            nIndeks = dtgItem.ItemIndex
            Dim objPM As ReplecementPartMaster = CType(CType(dtgPartSelection.DataSource, ArrayList)(nIndeks), ReplecementPartMaster)
            If CType(dtgItem.Cells(0).FindControl("chkItemChecked"), CheckBox).Checked Then
                If Not IsNothing(objPM) Then
                    arlCheckedPMItem.Add(objPM)
                End If
            End If
        Next
        Return arlCheckedPMItem
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Response.Redirect("PopReplecementPart.aspx?ID=" & Request.Params("ID"))
    End Sub
End Class
