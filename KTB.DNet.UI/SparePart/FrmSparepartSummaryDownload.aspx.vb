
#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports Ktb.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region

Public Class FrmSparepartSummaryDownload
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgSPPO As System.Web.UI.WebControls.DataGrid

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
        Me.EnableViewState = False
        If Not IsPostBack Then
            Dim sh As SessionHelper = New SessionHelper
            Dim crit As CriteriaComposite = CType(sh.GetSession("MONITORINGPOTODOWNLOAD"), CriteriaComposite)
            SetDownload()
            BindTodtgSPPO(crit)
        End If
    End Sub

    
    Private Sub SetDownload()
        Response.ContentType = "application/x-download"
        Dim uploadFilename As String = New System.Text.StringBuilder("""").Append("PEMESANAN - Monitoring Sparepart.xls").Append("""").ToString()

        Response.AddHeader("Content-Disposition", _
            New System.Text.StringBuilder("attachment;filename=").Append(uploadFilename).ToString)
    End Sub


    Private Sub BindTodtgSPPO(ByVal criterias As CriteriaComposite)
        Dim ListSPPO As ArrayList = New SparePartPOFacade(User).RetrieveSummary(criterias)

        dtgSPPO.DataSource = ListSPPO

        dtgSPPO.DataBind()
    End Sub

   
    Private Sub dtgSPPO_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSPPO.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1).ToString
            Dim sp As V_SparePartPOSummary = CType(e.Item.DataItem, V_SparePartPOSummary)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            lblDealerCode.Text = sp.DealerCode
            Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
            lblDealerName.Text = sp.DealerName
            Dim lblPONumber As Label = CType(e.Item.FindControl("lblPONumber"), Label)
            lblPONumber.Text = sp.PONumber
            Dim lblTanggalPesanan As Label = CType(e.Item.FindControl("lblTanggalPesanan"), Label)
            lblTanggalPesanan.Text = sp.PODate
            Dim lblJenisPesanan As Label = CType(e.Item.FindControl("lblJenisPesanan"), Label)
            If sp.OrderType = "E" Then
                lblJenisPesanan.Text = "Emergency"
            Else
                If sp.OrderType = "R" Then
                    lblJenisPesanan.Text = "Reguler"
                Else
                    If sp.OrderType = "K" Then
                        lblJenisPesanan.Text = "P.Khusus"
                    Else
                        If sp.OrderType = "I" Then
                            lblJenisPesanan.Text = "Indent"
                        End If
                    End If
                End If


            End If

            Dim lblProcessCode As Label = CType(e.Item.FindControl("lblProcessCode"), Label)


            Dim listItem0 As New ListItem("Baru", "")
            Dim listItem1 As New ListItem("Batal", "C")
            Dim listItem2 As New ListItem("Telah Dikirim", "S")
            Dim listItem3 As New ListItem("Telah Diproses", "P")
            Dim listItem4 As New ListItem("Batal MMKSI", "X")


            Dim listItem5 As New ListItem("Tolak MMKSI", "T")

            If sp.ProcessCode = "" Then

            End If

            Select Case sp.ProcessCode
                Case ""
                    lblProcessCode.Text = "Baru"
                Case "C"
                    lblProcessCode.Text = "Batal"
                Case "S"
                    lblProcessCode.Text = "Telah Dikirim"
                Case "P"
                    lblProcessCode.Text = "Telah Diproses"
                Case "X"
                    lblProcessCode.Text = "Batal MMKSI"
                Case "T"
                    lblProcessCode.Text = "Tolak MMKSI"
            End Select
            Dim lblTotalQuantity As Label = CType(e.Item.FindControl("lblTotalQuantity"), Label)
            Dim lblTotalAmount As Label = CType(e.Item.FindControl("lblTotalAmount"), Label)
            'lblTotalQuantity.Text = FormatNumber(sp.ItemCount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            ''lblTotalAmount.Text = FormatNumber(sp.ItemAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'lblTotalAmount.Text = sp.ItemAmount
            'lblTotalAmount.Text = FormatNumber(sp.ItemAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'lblTotalQuantity.Text = FormatNumber(sp.ItemCount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Dim ItemAmount As Int64 = sp.ItemAmount
            lblTotalAmount.Text = ItemAmount
            lblTotalQuantity.Text = sp.ItemCount
        End If
    End Sub
End Class
