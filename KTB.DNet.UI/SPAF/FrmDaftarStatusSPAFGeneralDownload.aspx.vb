Imports KTB.DNet.Domain

Public Class FrmDaftarStatusSPAFGeneralDownload
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblLeasing As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriodePersetujuan As System.Web.UI.WebControls.Label
    Protected WithEvents dtgSPAF As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblPeriodeKirim As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
#Region " Class "
    Private Class BindClass
        Private _VehicleType As String
        Public Property VehicleType() As String
            Get
                Return _VehicleType
            End Get
            Set(ByVal Value As String)
                _VehicleType = Value
            End Set
        End Property
        Private _VehicleDescription As String
        Public Property VehicleDescription() As String
            Get
                Return _VehicleDescription
            End Get
            Set(ByVal Value As String)
                _VehicleDescription = Value
            End Set
        End Property
        Private _Harga As Int64
        Private _HargaHistory As New ArrayList
        Public Property Harga() As Int64
            Get
                Return _Harga
            End Get
            Set(ByVal Value As Int64)
                _Harga = Value
                _HargaHistory.Add(Value)
            End Set
        End Property
        Public ReadOnly Property HargaAverage() As Int64
            Get
                Dim summy As Int64 = 0
                For Each hargas As Int64 In _HargaHistory
                    summy = summy + hargas
                Next
                Return summy / _HargaHistory.Count
            End Get
        End Property
        Private _UnitAvalis As Integer
        Public Property UnitAvalis() As Integer
            Get
                Return _UnitAvalis
            End Get
            Set(ByVal Value As Integer)
                _UnitAvalis = _UnitAvalis + Value
            End Set
        End Property
        Private _AmountAvalist As Int64
        Public Property AmountAvalist() As Int64
            Get
                Return _AmountAvalist
            End Get
            Set(ByVal Value As Int64)
                _AmountAvalist = _AmountAvalist + Value
            End Set
        End Property
        Private _NonAvalUnit As Integer
        Public Property NonAvalUnit() As Integer
            Get
                Return _NonAvalUnit
            End Get
            Set(ByVal Value As Integer)
                _NonAvalUnit = _NonAvalUnit + Value
            End Set
        End Property
        Private _NonAvalAmount As Int64
        Public Property NonAvalAmount() As Int64
            Get
                Return _NonAvalAmount
            End Get
            Set(ByVal Value As Int64)
                _NonAvalAmount = _NonAvalAmount + Value
            End Set
        End Property
        Private _PercenSPAF As Decimal
        Private _PercentSPAFHistory As New ArrayList
        Public Property PercenSPAF() As Decimal
            Get
                Return _PercenSPAF
            End Get
            Set(ByVal Value As Decimal)
                _PercenSPAF = Value
                _PercentSPAFHistory.Add(Value)
            End Set
        End Property
        Public ReadOnly Property PercentSPAFAverage() As Decimal
            Get
                Dim summy As Decimal = 0
                For Each percent As Decimal In _PercentSPAFHistory
                    summy = summy + percent
                Next
                Return summy / _PercentSPAFHistory.Count
            End Get
        End Property
        Public ReadOnly Property TotalUnitSPAF() As Integer
            Get
                Return UnitAvalis + NonAvalUnit
            End Get
        End Property
        Public ReadOnly Property TotalUnitAmount() As Int64
            Get
                Return AmountAvalist + NonAvalAmount
            End Get
        End Property
    End Class
#End Region

    Private _data As New Hashtable
    Private ReadOnly Property GetIdIn() As String()
        Get
            Return Request.QueryString("idin").Split(",")
        End Get
    End Property
    Private ReadOnly Property GetDataSession() As ArrayList
        Get
            Dim s As ArrayList = Session.Item("SPAFDOC")
            Dim newList As New ArrayList
            For Each sp As V_LeasingDaftarDokumen In s
                For Each id As String In GetIdIn
                    If sp.ID = id Then
                        newList.Add(sp)
                        Exit For
                    End If
                Next
            Next
            Return newList
        End Get
    End Property

    Private Sub SetTitle()
        If Request.QueryString("DocType") = EnumSPAFSubsidy.DocumentType.SPAF Then
            lblTitle.Text = EnumSPAFSubsidy.DocumentType.SPAF.ToString
            dtgSPAF.Columns(8).HeaderText = "%SPAF"
            dtgSPAF.Columns(9).HeaderText = "Total Unit SPAF"
        Else
            lblTitle.Text = EnumSPAFSubsidy.DocumentType.Subsidi.ToString
            dtgSPAF.Columns(8).HeaderText = "%Subsidi"
            dtgSPAF.Columns(9).HeaderText = "Total Unit Subsidi"
        End If
        lblPeriodeKirim.Text = Request.QueryString("Kirim")
        lblPeriodePersetujuan.Text = Request.QueryString("Periode")
    End Sub

    Private Sub ProcessData()
        For Each item As V_LeasingDaftarDokumen In GetDataSession
            Dim vehicleTypeCode As String = item.ChassisMaster.VechileColor.VechileType.VechileTypeCode
            Dim vehicleDescription As String = item.ChassisMaster.VechileColor.VechileType.Description
            Dim harga As Int64 = item.RetailPrice
            Dim sellingType As EnumSellingType.SellingType = item.SellingType
            Dim counter As Integer = 0
            Dim summy As Int64 = 0
            Dim procData As BindClass
            If Not _data.ContainsKey(vehicleTypeCode) Then
                procData = New BindClass
                procData.VehicleType = vehicleTypeCode
                procData.VehicleDescription = vehicleDescription
                _data.Add(vehicleTypeCode, procData)
            Else
                procData = _data(vehicleTypeCode)
            End If
            procData.Harga = harga
            If sellingType = EnumSellingType.SellingType.Avalis Then
                procData.UnitAvalis = 1
                If item.DocType = EnumSPAFSubsidy.DocumentType.SPAF Then
                    procData.AmountAvalist = item.SPAF
                    procData.PercenSPAF = (item.SPAF / item.RetailPrice) * 100
                Else
                    procData.AmountAvalist = item.Subsidi
                    procData.PercenSPAF = (item.Subsidi / item.RetailPrice) * 100
                End If
            Else
                procData.NonAvalUnit = 1
                If item.DocType = EnumSPAFSubsidy.DocumentType.SPAF Then
                    procData.NonAvalAmount = item.SPAF
                    procData.PercenSPAF = (item.SPAF / item.RetailPrice) * 100
                Else
                    procData.NonAvalAmount = item.Subsidi
                    procData.PercenSPAF = (item.Subsidi / item.RetailPrice) * 100
                End If
            End If
        Next
    End Sub

    Private Sub BindGrid()
        dtgSPAF.DataSource = _data.Values
        dtgSPAF.DataBind()
    End Sub

    Private Sub InitiatePage()
        Response.ContentType = "application/x-download"
        Dim uploadFilename As String = New System.Text.StringBuilder("""").Append("SPAF.xls").Append("""").ToString()
        Response.AddHeader("Content-Disposition", _
            New System.Text.StringBuilder("attachment;filename=").Append(uploadFilename).ToString)
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetTitle()
        ProcessData()
        BindGrid()
        InitiatePage()
    End Sub

    Private Sub dtgSPAF_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSPAF.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            e.Item.Cells(0).Text = (e.Item.ItemIndex + 1 + (dtgSPAF.PageSize * dtgSPAF.CurrentPageIndex)).ToString
        End If
        If e.Item.ItemType = ListItemType.Footer Then
            Dim summyHarga As Int64 = 0
            Dim summySPAF As Decimal = 0
            Dim counter As Int32 = 0
            For Each item As BindClass In _data.Values
                summyHarga = summyHarga + item.Harga
                If IsNumeric(e.Item.Cells(4).Text) Then
                    e.Item.Cells(4).Text = e.Item.Cells(4).Text + item.UnitAvalis
                Else
                    e.Item.Cells(4).Text = item.UnitAvalis
                End If
                If IsNumeric(e.Item.Cells(5).Text) Then
                    e.Item.Cells(5).Text = e.Item.Cells(5).Text + item.AmountAvalist
                Else
                    e.Item.Cells(5).Text = item.AmountAvalist
                End If
                If IsNumeric(e.Item.Cells(6).Text) Then
                    e.Item.Cells(6).Text = e.Item.Cells(6).Text + item.NonAvalUnit
                Else
                    e.Item.Cells(6).Text = item.NonAvalUnit
                End If
                If IsNumeric(e.Item.Cells(7).Text) Then
                    e.Item.Cells(7).Text = e.Item.Cells(7).Text + item.NonAvalAmount
                Else
                    e.Item.Cells(7).Text = item.NonAvalAmount
                End If
                summySPAF = summySPAF + item.PercentSPAFAverage
                If IsNumeric(e.Item.Cells(9).Text) Then
                    e.Item.Cells(9).Text = e.Item.Cells(9).Text + item.TotalUnitSPAF
                Else
                    e.Item.Cells(9).Text = item.TotalUnitSPAF
                End If
                If IsNumeric(e.Item.Cells(10).Text) Then
                    e.Item.Cells(10).Text = e.Item.Cells(10).Text + item.TotalUnitAmount
                Else
                    e.Item.Cells(10).Text = item.TotalUnitAmount
                End If
                counter = counter + 1
            Next
            e.Item.Cells(5).Text = Convert.ToInt64(e.Item.Cells(5).Text).ToString("#,##0.00")
            e.Item.Cells(7).Text = Convert.ToInt64(e.Item.Cells(7).Text).ToString("#,##0.00")
            e.Item.Cells(3).Text = (summyHarga / counter).ToString("#,##0.00")
            e.Item.Cells(8).Text = String.Format("=AVERAGE(I9:I{0})", counter + 8)
            e.Item.Cells(10).Text = Convert.ToInt64(e.Item.Cells(10).Text).ToString("#,##0.00")
        End If
    End Sub
End Class