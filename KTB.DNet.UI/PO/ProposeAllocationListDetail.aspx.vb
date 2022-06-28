#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System.IO
Imports System.Text
#End Region

Public Class ProposeAllocationListDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents dgAllocationDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTanggalAlokasi As System.Web.UI.WebControls.Label
    Protected WithEvents lblTipeWarna As System.Web.UI.WebControls.Label
    Protected WithEvents lblModelTipeWarna As System.Web.UI.WebControls.Label
    Protected WithEvents lblTahunPerakitan As System.Web.UI.WebControls.Label
    Protected WithEvents lblStokAtp As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Method"

    Private Sub BindDataGrid(ByVal MaterialNumber As String, ByVal tgl As Date, ByVal modeltipewarna As String, ByVal tahunperakitan As String, ByVal stokATP As String)
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PPQty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(PPQty), "MaterialNumber", MatchType.Exact, MaterialNumber))
        'criterias.opAnd(New Criteria(GetType(PPQty), "PeriodeDate", MatchType.Exact, tgl.Day))
        'criterias.opAnd(New Criteria(GetType(PPQty), "PeriodeMonth", MatchType.Exact, tgl.Month))
        'criterias.opAnd(New Criteria(GetType(PPQty), "PeriodeYear", MatchType.Exact, tgl.Year))
        'criterias.opAnd(New Criteria(GetType(PPQty), "DealerCode", MatchType.Exact, code))
        lblTanggalAlokasi.Text = tgl
        lblTipeWarna.Text = MaterialNumber
        lblModelTipeWarna.Text = modeltipewarna
        lblTahunPerakitan.Text = tahunperakitan
        lblStokAtp.Text = stokATP

        'tgl = tgl.AddDays(1)'replaced for DealerOrderCR

        Dim nextdate As Date = New NationalHolidayFacade(User).RetrieveNextDay(tgl)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.MaterialNumber", MatchType.Exact, MaterialNumber))
        'criterias.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationDate", MatchType.Exact, nextdate.Day))
        'criterias.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationMonth", MatchType.Exact, nextdate.Month))
        'criterias.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationYear", MatchType.Exact, nextdate.Year))

        'criterias.opAnd(New Criteria(GetType(PODetail), "AllocationDateTime", MatchType.Exact, tgl.ToString("yyyy/MM/dd 00:00:00")))
        criterias.opAnd(New Criteria(GetType(PODetail), "AllocationDateTime", MatchType.GreaterOrEqual, tgl.ToString("yyyy/MM/dd 00:00:00")))
        criterias.opAnd(New Criteria(GetType(PODetail), "AllocationDateTime", MatchType.Lesser, tgl.AddDays(1).ToString("yyyy/MM/dd 00:00:00")))

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.Status", MatchType.InSet, "(" & CType(enumStatusPO.Status.Baru, Integer) & "," & CType(enumStatusPO.Status.Konfirmasi, Integer) & "," & CType(enumStatusPO.Status.Ditolak, Integer) & "," & CType(enumStatusPO.Status.Rilis, Integer) & "," & CType(enumStatusPO.Status.DiBlok, Integer) & "," & CType(enumStatusPO.Status.Setuju, Integer) & "," & CType(enumStatusPO.Status.Tidak_Setuju, Integer) & "," & CType(enumStatusPO.Status.Selesai, Integer) & ")"))
        Dim sPOD As New SortCollection
        sPOD.Add(New Sort(GetType(PODetail), "AllocationDateTime", Sort.SortDirection.ASC))
        Dim aPODs As ArrayList = New PODetailFacade(User).Retrieve(criterias, sPOD)
        Me.sessionHelper.SetSession(Me._arlPODetails, aPODs)
        dgAllocationDetail.DataSource = aPODs
        dgAllocationDetail.DataBind()
    End Sub


    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "Dealer Order " & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls

        Dim TraineeData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(TraineeData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(TraineeData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteData(sw, data)

                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If



            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub


    Private Sub WriteData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder
        Dim i As Integer = 1
        Dim nDays As Integer = CType(viewstate.Item("nDays"), Integer)

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Dealer Order")
            sw.WriteLine(itemLine)

            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)

            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Tanggal Alokasi" & tab & Me.lblTanggalAlokasi.Text & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Tipe/Warna" & tab & Me.lblTipeWarna.Text & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Model/Tipe/Warna" & tab & Me.lblModelTipeWarna.Text & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Tahun Perakitan/Impor" & tab & Me.lblTahunPerakitan.Text & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Stok ATP" & tab & Me.lblStokAtp.Text & tab)
            sw.WriteLine(itemLine)


            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)

            itemLine.Remove(0, itemLine.Length)

            itemLine.Append("No" & tab)
            itemLine.Append("Dealer Code" & tab)
            itemLine.Append("Nama Dealer" & tab)
            itemLine.Append("Tgl. Permintaan Kirim" & tab)
            itemLine.Append("Tgl. Alokasi PO" & tab)
            itemLine.Append("Nomor OC" & tab)
            itemLine.Append("Nomor PO" & tab)
            itemLine.Append("Nomor PO Dealer" & tab)
            itemLine.Append("Header Note 1 / Keterangan" & tab)
            itemLine.Append("Order (Unit)" & tab)
            itemLine.Append("Alokasi (Unit)" & tab)
            itemLine.Append("Sisa (Unit)" & tab)
            itemLine.Append("Sisa Sebelum(Unit)" & tab)
            itemLine.Append("Sisa Sesudah(Unit)" & tab)
            itemLine.Append("Status" & tab)
            sw.WriteLine(itemLine.ToString())
            Dim No As Integer = 1
            For Each RowValue As PODetail In data
                itemLine.Remove(0, itemLine.Length)

                itemLine.Append(No.ToString & tab)
                itemLine.Append(RowValue.POHeader.Dealer.DealerCode & tab)
                itemLine.Append(RowValue.POHeader.Dealer.DealerName & tab)
                itemLine.Append(RowValue.POHeader.ReqAllocationDateTime.ToString("dd/MMM/yyyy") & tab)
                itemLine.Append(RowValue.AllocationDateTime.ToString("dd/MMM/yy") & tab)
                If Not IsNothing(RowValue.POHeader.ContractHeader) Then
                    itemLine.Append(RowValue.POHeader.ContractHeader.ContractNumber & tab)
                Else
                    itemLine.Append(String.Empty & tab)
                End If
                itemLine.Append(RowValue.POHeader.PONumber & tab)
                itemLine.Append(RowValue.POHeader.DealerPONumber & tab)
                If Not IsNothing(RowValue.POHeader.ContractHeader) Then
                    itemLine.Append(RowValue.POHeader.ContractHeader.ProjectName & tab)
                Else
                    itemLine.Append(String.Empty & tab)
                End If
                itemLine.Append(RowValue.ReqQty & tab)
                itemLine.Append(RowValue.AllocQty & tab)
                itemLine.Append(RowValue.ReqQty - RowValue.AllocQty & tab)
                If Not IsNothing(RowValue.v_DealerOrder) AndAlso RowValue.v_DealerOrder.ID > 0 Then
                    itemLine.Append(RowValue.v_DealerOrder.StokSebelum & tab)
                    itemLine.Append(RowValue.v_DealerOrder.StokSesudah & tab)
                Else
                    itemLine.Append("0" & tab)
                    itemLine.Append("0" & tab)
                End If
                Dim oStatus As enumStatusPO.Status = RowValue.POHeader.Status
                itemLine.Append(oStatus.ToString.Replace("_", " ") & tab)

                sw.WriteLine(itemLine.ToString())
                No += 1
            Next
        End If
    End Sub


#End Region

    Private sessionHelper As New sessionHelper
    Private _arlPODetails As String = "_arlPODetails"

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            Dim id As String = Request.QueryString("master")
            Dim tanggal As Date = Request.QueryString("date")
            Dim modeltipewarna As String = Request.QueryString("modeltipewarna")
            Dim tahunperakitan As String = Request.QueryString("tahunperakitan")
            Dim stokATP As String = Request.QueryString("stokATP")

            BindDataGrid(id, tanggal, modeltipewarna, tahunperakitan, stokATP)

            If Not SecurityProvider.Authorize(Context.User, SR.UnitUsulanSAPAlocationView_Privilege) Then
                Response.Redirect("../frmAccessDenied.aspx?modulName=Unit Usulan SAP Detail")
            End If
        End If
        'btnBack.Attributes.Add("onclick", "window.history.go(-1)")
    End Sub

    Private Sub dgAllocationDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgAllocationDetail.ItemDataBound
        Dim _dealer As Dealer

        If (e.Item.ItemIndex <> -1) Then
            Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

            If Not e.Item.DataItem Is Nothing Then
                e.Item.DataItem.GetType().ToString()
                Dim RowValue As PODetail = CType(e.Item.DataItem, PODetail)

                If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                    Try
                        e.Item.Cells(0).Text = (e.Item.ItemIndex + 1 + (dgAllocationDetail.PageSize * dgAllocationDetail.CurrentPageIndex)).ToString
                        e.Item.Cells(1).Text = RowValue.POHeader.Dealer.DealerCode
                        e.Item.Cells(2).Text = RowValue.POHeader.Dealer.DealerName
                        e.Item.Cells(3).Text = RowValue.POHeader.ReqAllocationDateTime
                        e.Item.Cells(4).Text = RowValue.AllocationDateTime.ToString("dd/MMM/yy")
                        e.Item.Cells(5).Text = RowValue.POHeader.PONumber
                        e.Item.Cells(6).Text = RowValue.POHeader.DealerPONumber
                        e.Item.Cells(7).Text = RowValue.ReqQty
                        e.Item.Cells(8).Text = RowValue.AllocQty
                        e.Item.Cells(9).Text = RowValue.ReqQty - RowValue.AllocQty
                        Dim oStatus As enumStatusPO.Status = RowValue.POHeader.Status

                        If Not IsNothing(RowValue.v_DealerOrder) AndAlso RowValue.v_DealerOrder.ID > 0 Then
                            e.Item.Cells(10).Text = RowValue.v_DealerOrder.StokSebelum
                            e.Item.Cells(11).Text = RowValue.v_DealerOrder.StokSesudah
                        Else
                            e.Item.Cells(10).Text = "-"
                            e.Item.Cells(11).Text = "-"
                        End If

                        e.Item.Cells(12).Text = oStatus.ToString.Replace("_", " ") '  RowValue.POHeader.Status.ToString.Replace("_", " ")
                    Catch ex As Exception
                    End Try

                End If

            End If

        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If Not sessionHelper.GetSession("PrevPage") Is Nothing AndAlso Not sessionHelper.GetSession("PrevPage") = String.Empty Then
            Response.Redirect(sessionHelper.GetSession("PrevPage").ToString())
        Else
            Response.Redirect("../login.aspx")
        End If
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim aPODs As ArrayList = Me.sessionHelper.GetSession(Me._arlPODetails)
        Me.DoDownload(aPODs)
    End Sub

#End Region
End Class