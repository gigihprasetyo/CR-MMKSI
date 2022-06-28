Imports KTB.DNet.BusinessFacade.SPAF
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain

Public Class FrmDownloadDoc
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgDownload As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
#Region " Class Declaration "
    Public Class BindClass
        Private _kodeDealer As String
        Public Property KodeDealer() As String
            Get
                Return _kodeDealer
            End Get
            Set(ByVal Value As String)
                _kodeDealer = Value
            End Set
        End Property
        Private _namaDealer As String
        Public Property NamaDealer() As String
            Get
                Return _namaDealer
            End Get
            Set(ByVal Value As String)
                _namaDealer = Value
            End Set
        End Property
        Private _family As String
        Public Property Family() As String
            Get
                Return _family
            End Get
            Set(ByVal Value As String)
                _family = Value
            End Set
        End Property
        Private _variants As String
        Public Property Variants() As String
            Get
                Return _variants
            End Get
            Set(ByVal Value As String)
                _variants = Value
            End Set
        End Property
        Private _Unit As Int32
        Public Property Unit() As Int32
            Get
                Return _Unit
            End Get
            Set(ByVal Value As Int32)
                _Unit = Value
            End Set
        End Property
        Private _jumlahDpp As Double = 0
        Public Property JumlahDpp() As Double
            Get
                Return _jumlahDpp
            End Get
            Set(ByVal Value As Double)
                _jumlahDpp = Value
            End Set
        End Property
        Private _jumlahPpn As Double
        Public Property JumlahPpn() As Double
            Get
                Return _jumlahPpn
            End Get
            Set(ByVal Value As Double)
                _jumlahPpn = Value
            End Set
        End Property

        Private _jumlahDppPpn As Double
        Public Property JumlahDppPpn() As Double
            Get
                Return _jumlahDppPpn
            End Get
            Set(ByVal Value As Double)
                _jumlahDppPpn = Value
            End Set
        End Property
    End Class
#End Region

    Private ReadOnly Property GetIdIn() As String
        Get
            Return String.format("({0})", Request.QueryString("idin"))
        End Get
    End Property

    Private Sub SetSPAFOrSubsidi()
        If Request.QueryString("DocType") = EnumSPAFSubsidy.DocumentType.SPAF Then
            lblTitle.Text = String.Format("Dokumen : {0}", EnumSPAFSubsidy.DocumentType.SPAF.ToString)
        Else
            lblTitle.Text = String.Format("Dokumen : {0}", EnumSPAFSubsidy.DocumentType.Subsidi.ToString)
        End If
    End Sub

    Private Function ProcessData(ByVal datas As ArrayList) As Hashtable
        Dim groupedData As New Hashtable
        For Each item As V_LeasingDaftarDokumen In datas
            Dim vehicleTypeCode As String = item.ChassisMaster.VechileColor.VechileType.VechileTypeCode
            Dim vehicleDescription As String = item.ChassisMaster.VechileColor.VechileType.Description
            Dim procData As BindClass
            If groupedData.ContainsKey(vehicleTypeCode) Then
                procData = groupedData(vehicleTypeCode)
                procData.Unit = procData.Unit + 1
            Else
                procData = New BindClass
                procData.KodeDealer = item.Dealer.DealerCode
                procData.NamaDealer = item.Dealer.DealerName
                procData.Family = vehicleTypeCode
                procData.Variants = vehicleDescription
                procData.Unit = 1
                groupedData.Add(vehicleTypeCode, procData)
            End If
            procData.JumlahDpp = item.RetailPrice + procData.JumlahDpp
            procData.JumlahPpn = item.PPn + procData.JumlahPpn
            procData.JumlahDppPpn = procData.JumlahDpp + procData.JumlahPpn
        Next
        Return groupedData
    End Function

    Private Sub BindGrid()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(V_LeasingDaftarDokumen), "RowStatus", _
            CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(V_LeasingDaftarDokumen), "ID", MatchType.InSet, GetIdIn))
        Dim fac As New V_LeasingDaftarDokumenFacade(User)
        dtgDownload.DataSource = ProcessData(fac.Retrieve(criterias)).Values
        dtgDownload.DataBind()
    End Sub

    Private Sub DownloadPage()

        Response.Clear()
        'Response.Buffer = True
        Response.Charset = ""
        Me.EnableViewState = False
        Dim oStringWriter As System.IO.StringWriter = New System.IO.StringWriter
        Dim oHtmlTextWriter As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(oStringWriter)
        Me.Controls.Clear() 'ClearControls(dgUploadData)
        dtgDownload.RenderControl(oHtmlTextWriter)
        Response.Write(oStringWriter.ToString())


        Response.ContentType = "application/x-download"
        Dim uploadFilename As String = New System.Text.StringBuilder("""").Append("SPAF.xls").Append("""").ToString()
        Response.AddHeader("Content-Disposition", _
            New System.Text.StringBuilder("attachment;filename=").Append(uploadFilename).ToString)
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetSPAFOrSubsidi()
        BindGrid()
        DownloadPage()
    End Sub

    Private Sub dtgDownload_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDownload.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(0).Text = (e.Item.ItemIndex + 1 + (dtgDownload.PageSize * dtgDownload.CurrentPageIndex)).ToString
        End If
       
        If e.Item.ItemType = ListItemType.Footer Then
            Dim TotalDpp, TotalPpn, TotalDppPpn As Double
            TotalDpp = 0
            TotalPpn = 0
            TotalDppPpn = 0
            Dim i As Integer
            For i = 0 To Me.dtgDownload.Items.Count - 1
                TotalDpp = TotalDpp + CType(dtgDownload.Items(i).Cells(12).Text(), Double)
                TotalPpn = TotalPpn + CType(dtgDownload.Items(i).Cells(13).Text(), Double)
                TotalDppPpn = TotalDpp + TotalPpn
            Next
            e.Item.Cells(11).Text = "Total Harga: "
            e.Item.Cells(12).Text = TotalDpp.ToString("#,##0.00")
            e.Item.Cells(13).Text = TotalPpn.ToString("#,##0.00")
            e.Item.Cells(14).Text = TotalDppPpn.ToString("#,##0.00")
        End If
    End Sub
End Class