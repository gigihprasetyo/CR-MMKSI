Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Security
Imports KTB.DNet.Utility

Public Class FrmDownloadPRP
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgReportPRP As System.Web.UI.WebControls.DataGrid

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
        'Put user code to initialize the page here
        Me.EnableViewState = False
        SetDownload()
        If Not Page.IsPostBack Then
            Dim sh As SessionHelper = New SessionHelper
            Dim criteria As CriteriaComposite = CType(sh.GetSession("CRITERIAPRP"), CriteriaComposite)
            'Dim sortDirection As Integer = 0
            'Dim sortColumn As String = String.Empty
            'sortDirection = CType(sh.GetSession("CRITERIAPRPSORTDIRECTION"), Integer)
            'sortColumn = CType(sh.GetSession("CRITERIAPRPSORTCOLOUMN"), String)
            If Not criteria Is Nothing Then
                Dim objFacade As PRPSenderInfoFacade = New PRPSenderInfoFacade(User)
                Dim objPRPColl As ArrayList = objFacade.Retrieve(criteria)
                dtgReportPRP.DataSource = objPRPColl
                dtgReportPRP.DataBind()
            End If
        End If
    End Sub

    Private Function SetDownload()
        Response.ContentType = "application/x-download"
        Dim uploadFilename As String = New System.Text.StringBuilder("""").Append("PRP - Daftar Pengiriman Laporan PRP .xls").Append("""").ToString

        Response.AddHeader("Content-Disposition", _
            New System.Text.StringBuilder("attachment;filename=").Append(uploadFilename).ToString)

    End Function


    Private Sub dtgReportPRP_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgReportPRP.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim data As PRPSenderInfo = CType(e.Item.DataItem, PRPSenderInfo)
            Dim lblOrganization As Label = CType(e.Item.FindControl("lblOrganization"), Label)
            If data.CreatedBy.Length >= 6 Then
                Dim objOrg As Dealer = New DealerFacade(User).Retrieve(CInt(data.CreatedBy().Substring(0, 6)))
                If Not IsNothing(objOrg) Then
                    lblOrganization.Text = objOrg.DealerCode
                End If
            End If

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Select Case data.Status
                Case PRPSenderInfo.EnumSendStatus.Baru
                    lblStatus.Text = "Baru"
                Case PRPSenderInfo.EnumSendStatus.Sukses
                    lblStatus.Text = "Sukses"
                Case PRPSenderInfo.EnumSendStatus.Gagal
                    lblStatus.Text = "Gagal"
            End Select

            Dim lblCreatedBy As Label = CType(e.Item.FindControl("lblCreatedBy"), Label)
            Dim createdBy As String = data.CreatedBy.Remove(0, 6)
            Dim objUser As UserInfo = New UserInfoFacade(User).Retrieve(createdBy.Trim)
            lblCreatedBy.Text = objUser.FirstName & " " & objUser.LastName & " - " & objUser.Email

            Dim lblDataStatus As Label = CType(e.Item.FindControl("lblDataStatus"), Label)
            If data.RowStatus = 0 Then
                lblDataStatus.Text = "Aktive"
            Else
                lblDataStatus.Text = "Tidak Aktive"

            End If


        End If
    End Sub
End Class
