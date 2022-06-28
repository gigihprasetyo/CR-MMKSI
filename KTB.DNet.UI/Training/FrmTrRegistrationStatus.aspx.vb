Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports KTB.DNet.UI

Public Class FrmTrRegistrationStatus
    Inherits System.Web.UI.Page

    Dim sHStatus As SessionHelper = New SessionHelper
    Protected WithEvents dtgClassRegistration As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
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
        If Not IsPostBack Then           
            InitiatePage()
            BindDataGrid()
        End If
    End Sub

    'Private Sub ActivateUserPrivilege()
    '    If Not SecurityProvider.Authorize(Context.User, SR.TrainingStatusPendaftaran_Privilege) Then
    '        Server.Transfer("../FrmAccessDenied.aspx?modulName=Training - Status Pendaftaran")
    '    End If
    'End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

        'Response.Write( _
        '                New System.Text.StringBuilder("<script language='javascript'>"). _
        '                    Append("function downloadRegStatus()"). _
        '                    Append("{	window.open('./FrmDownloadTrRegistrationStatus.aspx?mode=download','_blank','fullscreen=no,menubar=yes,status=yes,titlebar=yes,toolbar=no,height=480,width=640,resizable=yes');	}"). _
        '                    Append("</script>").ToString _
        '                )
    End Sub

    Private Sub BindDataGrid()
        Try
            Dim arlClassReg As ArrayList = sHStatus.GetSession("objTrClassRegistration")
            'If IsNothing(arlClassReg) Then
            Dim critCol As ICriteria = CType(sHStatus.GetSession("classRegCriteria"), CriteriaComposite)
            Dim sortCol As ICollection = CType(sHStatus.GetSession("classRegSort"), SortCollection)
            arlClassReg = New TrClassRegistrationFacade(User).Retrieve(critCol, sortCol)
            sHStatus.SetSession("objTrClassRegistration", arlClassReg)
            'End If
            dtgClassRegistration.DataSource = arlClassReg
            dtgClassRegistration.DataBind()
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
                lblStartDate.Text = RowValue.TrClass.StartDate.Day & "-" & RowValue.TrClass.StartDate.Month & "-" & RowValue.TrClass.StartDate.Year
                lblEndDate.Text = RowValue.TrClass.FinishDate.Day & "-" & RowValue.TrClass.FinishDate.Month & "-" & RowValue.TrClass.FinishDate.Year
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

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Response.Redirect("./FrmDownloadTrRegistrationStatus.aspx")
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("./FrmViewTrClassRegistration1.aspx")
    End Sub
End Class
