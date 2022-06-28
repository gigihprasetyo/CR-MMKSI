Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Public Class frmDownloadPartIncidentalPOheader
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgPartIncidentalDetail As System.Web.UI.WebControls.DataGrid

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
        If Not IsPostBack Then
            BindDataGrid()
        End If
    End Sub

    Private Function SetDownload()
        Response.ContentType = "application/x-download"
        Dim uploadFilename As String = New System.Text.StringBuilder("""").Append("Permintaan Khusus-DaftarPOPermintaanKhusus.xls").Append("""").ToString
        Response.AddHeader("Content-Disposition", _
            New System.Text.StringBuilder("attachment;filename=").Append(uploadFilename).ToString)
    End Function

    Private Sub BindDataGrid()
        Dim arl As ArrayList = New SessionHelper().GetSession("arlDownloadable")
        dgPartIncidentalDetail.DataSource = arl
        dgPartIncidentalDetail.DataBind()
    End Sub

    Private Sub dgPartIncidentalDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPartIncidentalDetail.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objPartIncidentalDetail As PartIncidentalDetail = e.Item.DataItem
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgPartIncidentalDetail.CurrentPageIndex * dgPartIncidentalDetail.PageSize)
            Dim dealerCode As String = objPartIncidentalDetail.PartIncidentalHeader.Dealer.DealerCode
            Dim reqNumber As String = objPartIncidentalDetail.PartIncidentalHeader.RequestNumber
            Dim tglPesanan As Date = objPartIncidentalDetail.PartIncidentalHeader.IncidentalDate
            Dim tglProses As Date
            Dim coltglProses As String = String.Empty


            e.Item.Cells(2).Text = dealerCode
            e.Item.Cells(3).Text = reqNumber
            Dim strPO As String = String.Empty
            For Each item As PartIncidentalPO In objPartIncidentalDetail.PartIncidentalPOs
                If strPO = String.Empty Then
                    strPO = item.PONumber
                    'tglProses = item.ProcessDate
                    coltglProses = item.ProcessDate.ToString("dd/MM/yyyy")
                Else
                    strPO += "<br>" & item.PONumber
                    coltglProses += "<br>" + item.ProcessDate.ToString("dd/MM/yyyy")
                    'tglProses += "<br>" & item.ProcessDate
                End If
            Next

            e.Item.Cells(4).Text = strPO
            e.Item.Cells(5).Text = tglPesanan
            e.Item.Cells(6).Text = coltglProses

            e.Item.Cells(8).Text = CType(e.Item.DataItem, PartIncidentalDetail).SparePartMaster.PartNumber
            e.Item.Cells(9).Text = CType(e.Item.DataItem, PartIncidentalDetail).SparePartMaster.PartName
            e.Item.Cells(10).Text = CType(e.Item.DataItem, PartIncidentalDetail).AlocatedQuantity
        End If
    End Sub
End Class
