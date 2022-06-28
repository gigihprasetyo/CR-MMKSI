#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility

Imports System.IO
Imports System.Text
#End Region

Public Class FrmLaporanPenjualanAcc
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtEnumAccessories As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblNomorLaporan As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorPermintaan As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents txtRefNum As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblTanggalInput As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoRangka As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoRangka As System.Web.UI.WebControls.TextBox
    Protected WithEvents lnkbtnCheckChassis As System.Web.UI.WebControls.LinkButton
    Protected WithEvents dtgMain As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents calStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtReportNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents calEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents chkPenjualan As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkPembuatan As System.Web.UI.WebControls.CheckBox
    Protected WithEvents calCreatedStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents calCreatedEnd As KTB.DNet.WebCC.IntiCalendar

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


#Region "Variables"
    Private _sessHelper As New SessionHelper
    Private _sessASs As String = "FrmLaporanPenjualanAcc._sessASs"
    Private _sessCriterias As String = "FrmLaporanPenjualanAcc._sessCriterias"
#End Region

#Region "Methods"

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.Lihat_laporan_penjualan_accessories_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PENJUALAN ACCESSORIES - Kategori Input")
        End If
        Dim IsOk As Boolean = False
        IsOk = SecurityProvider.Authorize(context.User, SR.Download_laporan_penjualan_accessories_privilege)
        Me.btnDownload.Enabled = IsOk
    End Sub


    Private Sub Initialization()
        InitLibrary()
        BindData()
    End Sub

    Private Sub InitLibrary()
        Dim aACs As ArrayList
        Dim sAC As New SortCollection
        Dim cAC As New CriteriaComposite(New Criteria(GetType(AccessoriesCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim oDealer As Dealer = Session.Item("DEALER")
        Dim IsRedirected As Boolean = False

        If Not IsNothing(Request.Item("IsRedirected")) Then
            If Request.Item("IsRedirected") = "1" Then
                IsRedirected = True
            End If
        End If

        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"

        sAC.Add(New Sort(GetType(AccessoriesCategory), "Name", Sort.SortDirection.ASC))
        aACs = New AccessoriesCategoryFacade(User).Retrieve(cAC, sAC)
        Me.ddlKategori.Items.Clear()
        Me.ddlKategori.Items.Add(New ListItem("Silahkan Pilih", 0))
        For Each oAC As AccessoriesCategory In aACs
            Me.ddlKategori.Items.Add(New ListItem(oAC.Name, oAC.ID))
        Next
        Me.calStart.Value = DateSerial(Now.Year, Now.Month, 1)
        Me.calEnd.Value = DateSerial(Now.Year, Now.Month, 1).AddMonths(1).AddDays(-1)
        Me.calCreatedStart.Value = Me.calStart.Value
        Me.calCreatedEnd.Value = Me.calEnd.Value

        If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            Me.txtKodeDealer.Text = String.Empty
            Me.txtKodeDealer.ReadOnly = False
            Me.lblSearchDealer.Visible = True
            Me.dtgMain.Columns(Me.dtgMain.Columns.Count - 2).Visible = True 'View
            Me.dtgMain.Columns(Me.dtgMain.Columns.Count - 1).Visible = False 'Edit
        Else
            Me.txtKodeDealer.Text = oDealer.DealerCode
            'Me.txtKodeDealer.ReadOnly = True
            txtKodeDealer.Attributes.Add("readonly", "readonly")
            Me.lblSearchDealer.Visible = False
            Me.dtgMain.Columns(Me.dtgMain.Columns.Count - 2).Visible = False 'View
            Me.dtgMain.Columns(Me.dtgMain.Columns.Count - 1).Visible = True 'Edit
        End If

        If IsRedirected Then
            Dim arrCrits(5) As String
            arrCrits = Me._sessHelper.GetSession(Me._sessCriterias)

            Me.txtKodeDealer.Text = arrCrits(0)
            Me.ddlKategori.SelectedIndex = arrCrits(1)
            Me.txtReportNumber.Text = arrCrits(2)
            Me.txtRefNum.Text = arrCrits(3)
            Me.calStart.Value = arrCrits(4)
            Me.calEnd.Value = arrCrits(5)

        End If
    End Sub

    Private Sub BindData()
        Dim cAS As New CriteriaComposite(New Criteria(GetType(AccessoriesSale), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sAS As New SortCollection
        Dim oASFac As New AccessoriesSaleFacade(User)
        Dim aASs As ArrayList
        Dim sCodes As String
        Dim arrCrits(5) As String

        If Me.chkPenjualan.Checked = False AndAlso Me.chkPembuatan.Checked = False Then
            MessageBox.Show("Kriteria Tanggal Harus Dipilih")
            Exit Sub
        End If

        If Me.txtKodeDealer.Text.Trim <> String.Empty Then
            sCodes = "'" & Me.txtKodeDealer.Text.Replace(";", "','") & "'"
            cAS.opAnd(New Criteria(GetType(AccessoriesSale), "Dealer.DealerCode", MatchType.InSet, "(" & sCodes & ")"))
        End If
        If Me.ddlKategori.SelectedIndex > 0 Then
            cAS.opAnd(New Criteria(GetType(AccessoriesSale), "AccessoriesCategory.ID", MatchType.Exact, CType(Me.ddlKategori.SelectedValue, Integer)))
        End If
        If Me.txtReportNumber.Text.Trim <> String.Empty Then
            cAS.opAnd(New Criteria(GetType(AccessoriesSale), "ReportNumber", MatchType.Exact, Me.txtReportNumber.Text))
        End If
        If Me.txtRefNum.Text.Trim <> String.Empty Then
            cAS.opAnd(New Criteria(GetType(AccessoriesSale), "RefNumber", MatchType.Exact, Me.txtRefNum.Text))
        End If
        If Me.txtNoRangka.Text.Trim <> String.Empty Then
            cAS.opAnd(New Criteria(GetType(AccessoriesSale), "ChassisMaster.ChassisNumber", MatchType.Exact, Me.txtNoRangka.Text))
        End If
        If Me.chkPenjualan.Checked Then
            cAS.opAnd(New Criteria(GetType(AccessoriesSale), "SoldDate", MatchType.GreaterOrEqual, Me.calStart.Value.ToString("yyyy/MM/dd 00:00:00")))
            cAS.opAnd(New Criteria(GetType(AccessoriesSale), "SoldDate", MatchType.Lesser, Me.calEnd.Value.AddDays(1).ToString("yyyy/MM/dd 00:00:00")))
        End If
        If Me.chkPembuatan.Checked Then
            cAS.opAnd(New Criteria(GetType(AccessoriesSale), "CreatedTime", MatchType.GreaterOrEqual, Me.calCreatedStart.Value.ToString("yyyy/MM/dd 00:00:00")))
            cAS.opAnd(New Criteria(GetType(AccessoriesSale), "CreatedTime", MatchType.Lesser, Me.calCreatedEnd.Value.AddDays(1).ToString("yyyy/MM/dd 00:00:00")))
        End If

        arrCrits(0) = Me.txtKodeDealer.Text
        arrCrits(1) = Me.ddlKategori.SelectedIndex
        arrCrits(2) = Me.txtReportNumber.Text
        arrCrits(3) = Me.txtRefNum.Text
        arrCrits(4) = Me.calStart.Value.ToString("yyyy/MM/dd 00:00:00")
        arrCrits(5) = Me.calEnd.Value.ToString("yyyy/MM/dd 00:00:00")
        Me._sessHelper.SetSession(Me._sessCriterias, arrCrits)

        sAS.Add(New Sort(GetType(AccessoriesSale), "SoldDate", Sort.SortDirection.ASC))
        aASs = oASFac.Retrieve(cAS, sAS)
        Me._sessHelper.SetSession(Me._sessASs, aASs)
        Me.dtgMain.DataSource = aASs
        Me.dtgMain.DataBind()
    End Sub


    Private Sub DoDownload(ByRef arlData As ArrayList)
        Dim sFileName As String
        Dim fullFileName As String
        sFileName = "PenjualanAccessories" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond

        fullFileName = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(fullFileName)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(fullFileName, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteData(sw, arlData)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub WriteData(ByVal sw As StreamWriter, ByVal arlData As ArrayList)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        Dim oAS As AccessoriesSale
        Dim i As Integer = 1, j As Integer
        Dim sHeader As String

        If Not IsNothing(arlData) Then
            itemLine.Remove(0, itemLine.Length)

            itemLine.Append("No" & tab)
            itemLine.Append("Kategori Input" & tab)
            itemLine.Append("No. Laporan" & tab)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Nama Dealer" & tab)
            itemLine.Append("Kota" & tab)
            itemLine.Append("Tgl Pengajuan" & tab)
            itemLine.Append("Tgl Pembuatan" & tab) 'new 
            itemLine.Append("No. Referensi" & tab)
            itemLine.Append("No. Rangka" & tab)
            itemLine.Append("Nama Kendaraan" & tab) 'new 
            itemLine.Append("Tipe Kendaraan" & tab) 'new 
            itemLine.Append("Nama Customer" & tab)
            itemLine.Append("No. Telepon" & tab)
            itemLine.Append("Comment" & tab)
            itemLine.Append("Material" & tab) 'new 
            itemLine.Append("Nama Material" & tab) 'new 
            itemLine.Append("Quantity" & tab) 'new 

            sw.WriteLine(itemLine.ToString())
            i = 1
            For Each di As DataGridItem In Me.dtgMain.Items
                oAS = CType(arlData(di.ItemIndex), AccessoriesSale)

                'sHeader = String.Empty
                'itemLine.Remove(0, itemLine.Length)


                'sHeader = itemLine.ToString
                'sw.WriteLine(i.ToString & sHeader)
                j = 1
                For Each oASD As AccessoriesSaleDetail In oAS.AccessoriesSaleDetails
                    itemLine.Remove(0, itemLine.Length)

                    itemLine.Append(IIf(j = 1, i.ToString, "") & tab)
                    itemLine.Append(oAS.AccessoriesCategory.Name & tab)
                    itemLine.Append(oAS.ReportNumber & tab)
                    itemLine.Append(oAS.Dealer.DealerCode & tab)
                    itemLine.Append(oAS.Dealer.DealerName & tab)
                    itemLine.Append(oAS.Dealer.City.CityName & tab)
                    itemLine.Append(oAS.SoldDate.ToString("dd/MMM/yyyy") & tab)
                    itemLine.Append(oAS.CreatedTime.ToString("dd/MMM/yyyy") & tab)
                    itemLine.Append(oAS.RefNumber & tab)
                    itemLine.Append(oAS.ChassisMaster.ChassisNumber & tab)
                    itemLine.Append(oAS.ChassisMaster.VechileColor.VechileType.Description & tab)
                    itemLine.Append(oAS.ChassisMaster.VechileColor.VechileType.VechileTypeCode & tab)
                    itemLine.Append(oAS.CustomerName & tab)
                    itemLine.Append(oAS.CustomerPhone & tab)
                    itemLine.Append(Me.GetValidComment(oAS.Comment) & tab)
                    itemLine.Append(oASD.SparePartMaster.PartNumber & tab)
                    itemLine.Append(oASD.SparePartMaster.PartName & tab)
                    itemLine.Append(oASD.Jumlah & tab)

                    sw.WriteLine(itemLine.ToString)
                    j += 1
                Next
                i += 1
            Next
        End If
    End Sub

    Private Function GetValidComment(ByVal sComment As String) As String
        Dim sValid As String = String.Empty

        For Each c As Char In sComment
            If Asc(c) = 10 Then
                sValid &= " "  ' "<br style='mso-data-placement:same-cell;'>"
            ElseIf Asc(c) = 13 Then
                sValid &= " "
            Else
                sValid &= c.ToString()
            End If
        Next
        Return sValid
    End Function

#End Region

#Region "Events"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckUserPrivilege()
        If Not IsPostBack() Then
            Initialization()
        End If
    End Sub

    Private Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
        BindData()
    End Sub

    Private Sub dtgMain_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.ItemCommand
        If e.CommandName.Trim.ToUpper = "Edit".ToUpper Then
            Dim aASs As ArrayList = Me._sessHelper.GetSession(Me._sessASs)
            Dim sUrl As String = String.Empty
            Dim oAS As AccessoriesSale = aass(e.Item.ItemIndex)

            surl = "FrmInputPenjualanAcc.aspx?ID=" & oas.ID.ToString
            Response.Redirect(sUrl)
        ElseIf e.CommandName.Trim.ToUpper = "Detail".ToUpper Then
            Dim aASs As ArrayList = Me._sessHelper.GetSession(Me._sessASs)
            Dim sUrl As String = String.Empty
            Dim oAS As AccessoriesSale = aass(e.Item.ItemIndex)

            surl = "FrmInputPenjualanAcc.aspx?ID=" & oas.ID.ToString
            Response.Redirect(sUrl)
        End If
    End Sub

#End Region

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim aDP As ArrayList = Me._sessHelper.GetSession(Me._sessASs)

        If aDP.Count > 0 Then
            'Me.dtgMain.Columns(Me.dtgMain.Columns.Count - 1).Visible = False
            'CommonFunction.DownloadDTGToExcel(Me.Page, Me.dtgMain, "PenjualanAccessories" & Now.ToString("yyyyMMddHHmmss") & ".xls")
            'Me.dtgMain.Columns(Me.dtgMain.Columns.Count - 1).Visible = True
            DoDownload(aDP)
        Else
            MessageBox.Show("Tidak ada data yang didownload")
        End If
    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblComment As Label = e.Item.FindControl("lblComment")
            Dim aDPs As ArrayList = Me._sessHelper.GetSession(Me._sessASs)
            Dim str As String

            str = CType(aDPs(e.Item.ItemIndex), AccessoriesSale).Comment
            If str.Length > 20 Then
                str = str.Substring(0, 18) & "..."
            End If
            lblComment.Text = str
        End If
    End Sub
End Class
