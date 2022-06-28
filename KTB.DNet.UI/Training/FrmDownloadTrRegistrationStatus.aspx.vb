Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports KTB.DNet.UI

Public Class FrmDownloadTrRegistrationStatus
    Inherits System.Web.UI.Page

    Dim sHStatus As SessionHelper = New SessionHelper
    Protected WithEvents dtgClassRegistration As System.Web.UI.WebControls.DataGrid
    Dim objDealer As Dealer

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

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
        objDealer = CType(sHStatus.GetSession("DEALER"), Dealer)
        'ActivateUserPrivilege()
        Me.EnableViewState = False
        'If Not IsPostBack Then
        InitiatePage()
        SetDownload()
        BindDataGrid()
        'End If
    End Sub

    'Private Sub ActivateUserPrivilege()
    '    If Not SecurityProvider.Authorize(Context.User, SR.TrainingStatusPendaftaran_Privilege) Then
    '        Server.Transfer("../FrmAccessDenied.aspx?modulName=Training - Status Pendaftaran")
    '    End If
    'End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindDataGrid()

        Try
            Dim arlClassReg As ArrayList = sHStatus.GetSession("objTrClassRegistration")

            If IsNothing(arlClassReg) Then
                Response.Write("<script>alert('Tidak ada data');window.close();</script>")
                Return
            End If

            'Dim tempAL As ArrayList = New ArrayList
            ''10 - 50
            'For i As Integer = 10 To 45
            'tempAL.Add(arlClassReg.Item(i))
            'Next

            dtgClassRegistration.DataSource = arlClassReg
            'dtgClassRegistration.DataSource = tempAL
            dtgClassRegistration.DataBind()

            'Dim stream As System.IO.MemoryStream = New System.IO.MemoryStream
            'Dim writer As System.IO.StreamWriter = New System.IO.StreamWriter(stream)
            'Dim htmlWriter As Html32TextWriter = New Html32TextWriter(writer)
            'dtgClassRegistration.RenderControl(htmlWriter)
            'htmlWriter.Flush()

            'Response.Write(htmlWriter)

            'htmlWriter.Close()
            'writer.Close()
            'stream.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dtgClassRegistration_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClassRegistration.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            SetTextColumnNo(e)
            SetTextColumnStatus(e)
            Dim RowValue As TrClassRegistration = CType(e.Item.DataItem, TrClassRegistration)
            If Not IsNothing(RowValue.TrClass) Then
                Dim lblStartDate As Label = CType(e.Item.FindControl("lblStartDate"), Label)
                Dim lblEndDate As Label = CType(e.Item.FindControl("lblEndDate"), Label)
                'lblStartDate.Text = RowValue.TrClass.StartDate.Day & "-" & RowValue.TrClass.StartDate.Month & "-" & RowValue.TrClass.StartDate.Year
                'lblEndDate.Text = RowValue.TrClass.FinishDate.Day & "-" & RowValue.TrClass.FinishDate.Month & "-" & RowValue.TrClass.FinishDate.Year
                lblStartDate.Text = RowValue.TrClass.StartDate.ToShortDateString
                lblEndDate.Text = RowValue.TrClass.FinishDate.ToShortDateString

            End If
        End If
    End Sub

    Private Sub SetTextColumnNo(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgClassRegistration.CurrentPageIndex * dtgClassRegistration.PageSize)
    End Sub

    Private Sub SetTextColumnStatus(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
        Select Case CType(DataBinder.Eval(e.Item.DataItem, "Status"), String)
            Case "0"
                lblStatus.Text = GetEnumClassRegText(EnumTrClassRegistration.DataStatusType.Register)
            Case "1"
                lblStatus.Text = GetEnumClassRegText(EnumTrClassRegistration.DataStatusType.Pass)
            Case "2"
                lblStatus.Text = GetEnumClassRegText(EnumTrClassRegistration.DataStatusType.Fail)
            Case "3"
                lblStatus.Text = GetEnumClassRegText(EnumTrClassRegistration.DataStatusType.Reject)
            Case Else
                lblStatus.Text = ""
        End Select
    End Sub

    Private Function GetEnumClassRegText(ByVal index As Integer) As String
        Dim obj As New EnumTrClassRegistration
        Return obj.StatusByIndex(index)
    End Function

    Private Function SetDownload()
        Response.ContentType = "application/x-download"
        Dim uploadFilename As String = New System.Text.StringBuilder("""").Append("Status Pendaftaran.xls").Append("""").ToString

        Response.AddHeader("Content-Disposition", _
            New System.Text.StringBuilder("attachment;filename=").Append(uploadFilename).ToString)

    End Function

End Class
