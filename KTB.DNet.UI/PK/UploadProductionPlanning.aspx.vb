#Region "Custom Namespace Imports"
Imports KTB.DNet.Parser
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports System.Security.Principal
Imports OfficeOpenXml
Imports System.Runtime.CompilerServices
Imports System.Linq
Imports System.Collections.Generic
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports Excel
Imports System.Reflection

#End Region

Public Class UploadProductionPlanning
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPilihLokasiFile As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents dtgProductionPlan As System.Web.UI.WebControls.DataGrid
    Protected WithEvents DataFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents ddlPeriode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownLoad As System.Web.UI.WebControls.Button
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lboxStatus As System.Web.UI.WebControls.ListBox
    Protected WithEvents lBoxType As System.Web.UI.WebControls.ListBox
    Protected WithEvents ddlSubCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTipe As System.Web.UI.WebControls.DropDownList

    Protected WithEvents dtgProductionPlan2 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lbtnDownloadExcel As System.Web.UI.WebControls.LinkButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private ProductionPlanArrayList As ArrayList
    Private sessionHelper As New SessionHelper

    Private Const varSessUpload = "PKProductionPlan"
    Private Const varSessDownload = "dataForDownload"
    Private Const varSessFileForDownload = "FileForDownload"

    Private totalRencanaProduksi As Integer = 0
    Private totalAlokasi As Integer = 0
    Private totalAllocationQty As Integer = 0
    Private totalReserveQty As Integer = 0
    Private totalRencanaJual As Integer = 0
    Private totalStatusBaruValidasi As Integer = 0
    Private totalStatusKonfirmasiTungguDiskon As Integer = 0
    Private totalRilis As Integer = 0
    Private totalTolak As Integer = 0
    Private totalDO As Integer = 0
    Private totalAll As Integer = 0
    Private totalsisaStock As Integer = 0
    Private totalsisaStockDNet As Integer = 0
    Private totalSelesai As Integer = 0
#End Region

#Region "Custom Method"

    Private Sub RetrieveData()
        '--DropDownList Rencana Penebusan
        For Each item As ListItem In LookUp.ArraylistMonth(True, 6, 1, DateTime.Now)
            ddlPeriode.Items.Add(item)
        Next
        ddlPeriode.SelectedValue = Format(DateTime.Now, "MMM yyyy").ToString

    End Sub

    Private Sub Upload()
        Dim sb As StringBuilder = New StringBuilder

        If (Not DataFile.PostedFile Is Nothing) AndAlso (DataFile.PostedFile.ContentLength > 0) Then

            Dim fileExt As String = Path.GetExtension(DataFile.PostedFile.FileName)
            If Not (fileExt.ToLower() = ".xls" OrElse fileExt.ToLower() = ".xlsx") Then
                MessageBox.Show("Hanya Menerima File Excell")
                Return
            End If

            sessionHelper.SetSession(varSessUpload, New ArrayList)
            dtgProductionPlan.DataSource = New ArrayList
            dtgProductionPlan.DataBind()
            Me.btnSimpan.Enabled = False

            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate

            Try
                Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName)
                Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & DateTime.Now.ToString("dd/MM/yyy HHmmss") & "_" & SrcFile

                imp = New SAPImpersonate(_user, _password, _webServer)

                If imp.Start() Then
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(DataFile.PostedFile.InputStream, targetFile)

                    Dim i As Integer = 0
                    Dim objReader As IExcelDataReader = Nothing
                    Dim ArrUpload As New ArrayList
                    Dim ArrUploadOK As New ArrayList
                    Dim isOK As Boolean = True

                    Using stream As FileStream = File.Open(targetFile, FileMode.Open, FileAccess.Read)
                        If fileExt.ToLower.Contains("xlsx") Then
                            objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                        Else
                            objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                        End If

                        If (Not IsNothing(objReader)) Then
                            While objReader.Read()

                                If i >= 1 Then
                                    Dim ObjPPP As New PKProductionPlan
                                    Dim strMaterialNumber As String = ""
                                    Dim intPeriodMonth As Integer = 0
                                    Dim intPeriodYear As Integer = 0
                                    Dim strProductionYear As String = ""
                                    Dim intPlanQty As Decimal = 0
                                    Dim intReserveQty As Decimal = 0
                                    Dim intAllocationQty As Decimal = 0

                                    If Not IsNothing(objReader.GetString(1)) Then
                                        Try
                                            intPeriodMonth = Convert.ToInt32(objReader.GetString(1).Trim())
                                            ObjPPP.PeriodMonth = intPeriodMonth
                                            If intPeriodMonth < 0 OrElse intPeriodMonth > 13 Then
                                                sb.Append("Bulan Periode Tidak Valid;")
                                            End If

                                        Catch ex As Exception
                                            sb.Append("Bulan Periode Tidak Valid;")
                                        End Try
                                    Else
                                        Continue While
                                    End If

                                    If Not IsNothing(objReader.GetString(2)) Then
                                        Try
                                            intPeriodYear = Convert.ToInt32(objReader.GetString(2).Trim())
                                            ObjPPP.PeriodYear = intPeriodYear
                                            If intPeriodYear.ToString.Length = 4 Then
                                                If ObjPPP.PeriodYear < DateTime.Now.Year Then
                                                    sb.Append("Tahun Periode Alokasi Sudah Kadaluarsa;")

                                                ElseIf ObjPPP.PeriodYear = DateTime.Now.Year Then
                                                    If ObjPPP.PeriodMonth < DateTime.Now.Month Then
                                                        sb.Append("Periode Alokasi Sudah Kadaluarsa;")
                                                    End If
                                                End If
                                            Else
                                                sb.Append("Tahun Periode Tidak Valid;")
                                            End If
                                        Catch ex As Exception
                                            sb.Append("Tahun Periode Tidak Valid;")
                                        End Try
                                    Else
                                        Continue While
                                    End If

                                    If Not IsNothing(objReader.GetString(3)) Then
                                        Try
                                            strMaterialNumber = objReader.GetString(3).Trim()
                                            ObjPPP.MaterialNumber = strMaterialNumber
                                            If strMaterialNumber.Length = 8 Then
                                                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "MaterialNumber", MatchType.Exact, strMaterialNumber))
                                                Dim ArrList = New VechileColorFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                                                If ArrList.Count > 0 Then
                                                    Dim vc As VechileColor
                                                    vc = CType(ArrList(0), VechileColor)
                                                    Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")

                                                    If vc.VechileType.ProductCategory.Code.Trim <> companyCode Then
                                                        sb.Append("Tipe tidak terdapat pada Kategori Produk " & companyCode & ";")
                                                    Else
                                                        ObjPPP.VechileColor = ArrList(0)
                                                    End If
                                                Else
                                                    sb.Append("Kode Kendaraan tidak ada (" & strMaterialNumber & ");")
                                                End If
                                            Else
                                                sb.Append("Kode Kendaraan tidak ada (" & strMaterialNumber & ");")
                                            End If

                                        Catch exx As Exception
                                            sb.Append("Kode Kendaraan Tidak Valid;")
                                        End Try
                                    Else
                                        Continue While
                                    End If

                                    If Not IsNothing(objReader.GetString(4)) Then
                                        Try
                                            strProductionYear = objReader.GetString(4).Trim()
                                            ObjPPP.ProductionYear = strProductionYear
                                            If strProductionYear.Length <> 4 Then
                                                sb.Append("Tahun Produksi tidak valid;")
                                            End If
                                        Catch exx As Exception
                                            sb.Append("Tahun Produksi Tidak Valid;")
                                        End Try
                                    Else
                                        Continue While
                                    End If

                                    Try
                                        If Not IsNothing(objReader.GetString(5)) Then
                                            intPlanQty = CDec(objReader.GetString(5).Trim())
                                            ObjPPP.PlanQty = intPlanQty
                                            If intPlanQty < 0 Then
                                                sb.Append("Quantity Plan < 0;")
                                            End If
                                        Else
                                            sb.Append("Plan Qty Tidak Valid;")
                                        End If
                                    Catch ex As Exception
                                        sb.Append("Plan Qty Tidak Valid;")
                                    End Try

                                    Try
                                        If Not IsNothing(objReader.GetString(6)) Then
                                            intAllocationQty = CInt(objReader.GetString(6).Trim())
                                            If intAllocationQty >= 0 Then
                                                ObjPPP.AllocationQty = intAllocationQty
                                            Else
                                                ObjPPP.AllocationQty = intAllocationQty
                                                sb.Append("Alokasi Qty < 0;")
                                            End If
                                        Else
                                            sb.Append("Alokasi Qty Tidak Valid;")
                                        End If
                                    Catch exx As Exception
                                        sb.Append("Alokasi Qty Tidak Valid;")
                                    End Try

                                    Try
                                        If Not IsNothing(objReader.GetString(7)) Then
                                            intReserveQty = CDec(objReader.GetString(7).Trim())
                                            ObjPPP.ReserveQty = intReserveQty
                                            If intReserveQty < 0 Then
                                                ObjPPP.ReserveQty = intReserveQty
                                                sb.Append("Reserve Qty < 0;")
                                            End If
                                        Else
                                            sb.Append("Reserve Qty Tidak Valid;")
                                        End If
                                    Catch exx As Exception
                                        sb.Append("Reserve Qty Tidak Valid;")
                                    End Try

                                    If Not ObjPPP.VechileColor Is Nothing Then
                                        For Each item As PKProductionPlan In ArrUpload
                                            If Not item.VechileColor Is Nothing Then
                                                If item.PeriodMonth = ObjPPP.PeriodMonth Then
                                                    If item.PeriodYear = ObjPPP.PeriodYear Then
                                                        If item.VechileColor.ID = ObjPPP.VechileColor.ID Then
                                                            If item.ProductionYear = ObjPPP.ProductionYear Then
                                                                item.ErrorMessage += ";Duplikasi Perencanaan Produksi"
                                                                sb.Append("Duplikasi Perencanaan Produksi;")
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        Next
                                    End If

                                    If sb.ToString().Trim <> "" Then
                                        ObjPPP.ErrorMessage = sb.ToString()
                                        isOK = False
                                    End If

                                    ArrUpload.Add(ObjPPP)
                                End If
                                i = i + 1

                            End While
                        End If
                    End Using

                    sessionHelper.SetSession(varSessUpload, ArrUpload)
                    If isOK = False Then
                        btnSimpan.Enabled = False
                    Else
                        btnSimpan.Enabled = True
                    End If
                End If

            Catch ex As Exception
                MessageBox.Show("Fail To Process")
            Finally

                imp.StopImpersonate()
                imp = Nothing
            End Try

        Else
            MessageBox.Show("Berkas Upload tidak boleh Kosong")
        End If
    End Sub

    'Private Sub ParseFile()
    '    If (Not DataFile.PostedFile Is Nothing) And (DataFile.PostedFile.ContentLength > 0) Then
    '        'cek maxFileFirst
    '        Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

    '        If DataFile.PostedFile.ContentLength > maxFileSize Then
    '            MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
    '            Exit Sub
    '        Else
    '            Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName)  '-- Source file name
    '            Dim DestFile As String = Server.MapPath("") & "\..\DataFile\" & SrcFile  '-- Destination file
    '            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
    '            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
    '            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
    '            'Dim _webServer As String = "172.17.104.90"
    '            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
    '            Dim success As Boolean = False
    '            Try
    '                success = imp.Start()
    '                If success Then
    '                    DataFile.PostedFile.SaveAs(DestFile)
    '                    imp.StopImpersonate()
    '                    imp = Nothing
    '                End If
    '            Catch ex As Exception
    '                'MessageBox.Show(SR.DownloadFail(LinkButton.Text))
    '            End Try
    'Dim parser As IParser = New ProductionPlanParser
    '            ProductionPlanArrayList = CType(parser.ParseNoTransaction(DestFile, User.Identity.Name.ToString), ArrayList)
    '            sessionHelper.SetSession(varSessUpload, ProductionPlanArrayList)
    '        End If
    '    Else
    '        MessageBox.Show("Pilih Lokasi File")
    '    End If
    'End Sub

    Private Sub BindToGrid()
        ProductionPlanArrayList = sessionHelper.GetSession(varSessUpload)
        If Not ((ProductionPlanArrayList Is Nothing) OrElse (ProductionPlanArrayList.Count <= 0)) Then
            dtgProductionPlan.DataSource = ProductionPlanArrayList
            dtgProductionPlan.DataBind()
            For Each item As PKProductionPlan In ProductionPlanArrayList
                If item.ErrorMessage <> String.Empty Then
                    btnSimpan.Enabled = False
                    Exit Sub
                End If
            Next
            If dtgProductionPlan.Columns(11).Visible Then
                btnSimpan.Enabled = True
            End If
        Else
            btnSimpan.Enabled = False
        End If
    End Sub

#End Region

#Region "EventHandler"
    Protected Sub lbtnDownloadExcel_Click(sender As Object, e As EventArgs) Handles lbtnDownloadExcel.Click
        Dim strName As String = "Template_Rencana_Produksi.xlsx"
        Response.Redirect("../downloadlocal.aspx?file=DataFile\PK\" & strName)
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ActivateUserPrivilege()
        If Not IsPostBack Then
            RetrieveData()
            InitiatePage()
            BindddlCategory()
            CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlKategori.SelectedItem.Text)

            If ddlKategori.Items.Count <> 0 Then
                BindddlTipe(ddlKategori.SelectedItem.ToString)
            End If
        End If

    End Sub

    Private Sub BindddlCategory()
        Dim arrayListCategory As ArrayList = New PKHeaderFacade(User).RetrieveListCategory

        If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
            Dim listitemBlank As New ListItem("Silahkan Pilih", -1)
            ddlKategori.Items.Add(listitemBlank)
        End If

        For Each item As Category In arrayListCategory
            Dim listItem As New ListItem(item.CategoryCode, item.ID)
            If item.CategoryCode = "PC" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryPC_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                    ddlKategori.Items.Add(listItem)
                End If
            ElseIf item.CategoryCode = "LCV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryLCV_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                    ddlKategori.Items.Add(listItem)
                End If
            ElseIf item.CategoryCode = "CV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryCV_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                    ddlKategori.Items.Add(listItem)
                End If
            End If
        Next
    End Sub

    Private Sub BindddlTipe(ByVal Category As String)
        ddlTipe.Items.Clear()
        If ddlKategori.SelectedValue <> -1 Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "Category.CategoryCode", MatchType.Exact, Category))
            If ddlSubCategory.SelectedValue <> "-1" Then
                'CommonFunction.SetVehicleSubCategoryCriterias(ddlSubCategory, ddlKategori.SelectedItem.Text, criterias, "VechileType")
                Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlSubCategory.SelectedValue
                criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))
            End If
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "Status", MatchType.Exact, "A"))

            'Dim year As Integer = CInt(ddlPeriode.SelectedValue.Split(" ")(1))
            '            Dim strSql2 As String = String.Format(
            '                String.Join(
            '                Environment.NewLine,
            '"SELECT VechileTypeID FROM VechileColorIsActiveOnPK WITH (NOLOCK) ",
            '"INNER JOIN VechileColor on VechileColorIsActiveOnPK.VehicleColorID=VechileColor.id",
            '"WHERE (VechileColorIsActiveOnPK.RowStatus = 0",
            '"AND VechileColorIsActiveOnPK.Status = 1",
            '"AND VechileColorIsActiveOnPK.ProductionYear = {0}",
            '"AND VechileColor.Status <> 'X')"
            '), year)

            Dim strSql2 As String = String.Format(
                           String.Join(
                           Environment.NewLine,
           "SELECT VechileTypeID FROM VechileColorIsActiveOnPK WITH (NOLOCK) ",
           "INNER JOIN VechileColor on VechileColorIsActiveOnPK.VehicleColorID=VechileColor.id",
           "WHERE (VechileColorIsActiveOnPK.RowStatus = 0",
           "AND VechileColorIsActiveOnPK.Status = 1",
           "AND VechileColor.Status <> 'X')"
           ))

            criterias.opAnd(New Criteria(GetType(VechileType), "ID", MatchType.InSet, "(" & strSql2 & ")"))

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("VechileTypeCode")) Then
                sortColl.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            ddlTipe.DataSource = New VechileTypeFacade(User).Retrieve(criterias, sortColl)
            ddlTipe.DataTextField = "VechileTypeCode"
            ddlTipe.DataValueField = "id"
            ddlTipe.DataBind()
        End If
        ddlTipe.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlTipe.SelectedIndex = 0
    End Sub

    Private Sub ddlKategori_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlKategori.SelectedIndexChanged
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlKategori.SelectedItem.Text)
        'BindVehicleType(True)
        BindddlTipe(ddlKategori.SelectedItem.ToString)
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.RencanaProduksiView_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Rencana Produksi")
        End If
        btnUpload.Visible = SecurityProvider.Authorize(Context.User, SR.RencanaProduksiUpload_Privilege)
        DataFile.Disabled = Not SecurityProvider.Authorize(Context.User, SR.RencanaProduksiUpload_Privilege)
        btnDownLoad.Visible = SecurityProvider.Authorize(Context.User, SR.RencanaProduksiDownload_Privilege)
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "ProductionYear"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
    End Sub

    Private Sub dtgProductionPlan_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgProductionPlan.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If

        BindData()
    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        dtgProductionPlan.EditItemIndex = -1
        'btnSimpan.Enabled = True
        btnDownLoad.Enabled = False
        dtgProductionPlan.Columns(11).Visible = True
        dtgProductionPlan.Columns(12).Visible = False
        dtgProductionPlan.Columns(13).Visible = False
        dtgProductionPlan.Columns(14).Visible = False
        dtgProductionPlan.Columns(15).Visible = False
        dtgProductionPlan.Columns(16).Visible = False
        dtgProductionPlan.Columns(17).Visible = False
        dtgProductionPlan.Columns(18).Visible = False
        dtgProductionPlan.Columns(19).Visible = False
        dtgProductionPlan.Columns(20).Visible = False
        dtgProductionPlan.AllowSorting = False
        'ParseFile()
        Upload()
        BindToGrid()
    End Sub

    Private Class GridProdPlan
        Private _No As Integer
        Private _KodeKendaraan As String
        Private _BulanPeriode As Integer
        Private _TahunPeriode As Integer
        Private _TahunPerakitan As Integer
        Private _StokAwalBulan As Integer
        Private _Alokasi As Integer
        Private _Reserve As Integer
        Private _SisaStokSetelahAlokasi As Integer
        Private _BaruDanValidasi As Integer
        Private _KonfirmasiDanTungguDiskon As Integer
        Private _Rilis As Integer
        Private _Ditolak As Integer
        Private _SelesaiOC As Integer
        Private _DeliveryOrder As Integer
        Private _Total As Integer
        Private _SisaStokDNet As Integer

        Public Property No As Integer
            Get
                Return _No
            End Get
            Set(ByVal value As Integer)
                _No = value
            End Set
        End Property
        Public Property KodeKendaraan As String
            Get
                Return _KodeKendaraan
            End Get
            Set(ByVal value As String)
                _KodeKendaraan = value
            End Set
        End Property
        Public Property BulanPeriode As Integer
            Get
                Return _BulanPeriode
            End Get
            Set(ByVal value As Integer)
                _BulanPeriode = value
            End Set
        End Property
        Public Property TahunPeriode As Integer
            Get
                Return _TahunPeriode
            End Get
            Set(ByVal value As Integer)
                _TahunPeriode = value
            End Set
        End Property
        Public Property TahunPerakitan As Integer
            Get
                Return _TahunPerakitan
            End Get
            Set(ByVal value As Integer)
                _TahunPerakitan = value
            End Set
        End Property
        Public Property StokAwalBulan As Integer
            Get
                Return _StokAwalBulan
            End Get
            Set(ByVal value As Integer)
                _StokAwalBulan = value
            End Set
        End Property
        Public Property Alokasi As Integer
            Get
                Return _Alokasi
            End Get
            Set(ByVal value As Integer)
                _Alokasi = value
            End Set
        End Property
        Public Property Reserve As Integer
            Get
                Return _Reserve
            End Get
            Set(ByVal value As Integer)
                _Reserve = value
            End Set
        End Property
        Public Property SisaStokSetelahAlokasi As Integer
            Get
                Return _SisaStokSetelahAlokasi
            End Get
            Set(ByVal value As Integer)
                _SisaStokSetelahAlokasi = value
            End Set
        End Property
        Public Property BaruDanValidasi As Integer
            Get
                Return _BaruDanValidasi
            End Get
            Set(ByVal value As Integer)
                _BaruDanValidasi = value
            End Set
        End Property
        Public Property KonfirmasiDanTungguDiskon As Integer
            Get
                Return _KonfirmasiDanTungguDiskon
            End Get
            Set(ByVal value As Integer)
                _KonfirmasiDanTungguDiskon = value
            End Set
        End Property
        Public Property Rilis As Integer
            Get
                Return _Rilis
            End Get
            Set(ByVal value As Integer)
                _Rilis = value
            End Set
        End Property
        Public Property Ditolak As Integer
            Get
                Return _Ditolak
            End Get
            Set(ByVal value As Integer)
                _Ditolak = value
            End Set
        End Property
        Public Property SelesaiOC As Integer
            Get
                Return _SelesaiOC
            End Get
            Set(ByVal value As Integer)
                _SelesaiOC = value
            End Set
        End Property
        Public Property DeliveryOrder As Integer
            Get
                Return _DeliveryOrder
            End Get
            Set(ByVal value As Integer)
                _DeliveryOrder = value
            End Set
        End Property
        Public Property Total As Integer
            Get
                Return _Total
            End Get
            Set(ByVal value As Integer)
                _Total = value
            End Set
        End Property
        Public Property SisaStokDNet As Integer
            Get
                Return _SisaStokDNet
            End Get
            Set(ByVal value As Integer)
                _SisaStokDNet = value
            End Set
        End Property
    End Class

    Sub dtgProductionPlan_ItemDataBound(ByVal Sender As Object, ByVal E As DataGridItemEventArgs) Handles dtgProductionPlan.ItemDataBound
        ProductionPlanArrayList = sessionHelper.GetSession(varSessUpload)
        If E.Item.ItemType = ListItemType.Item OrElse E.Item.ItemType = ListItemType.AlternatingItem Then
            Dim dataForDownload As ArrayList = sessionHelper.GetSession(varSessDownload)
            'If E.Item.ItemIndex = 0 Then
            '    dataForDownload.Clear()
            'End If
            If IsNothing(dataForDownload) Then dataForDownload = New ArrayList
            Dim lineProdPlan As GridProdPlan = New GridProdPlan
            Dim objPKProductionPlan As PKProductionPlan = CType(E.Item.DataItem, PKProductionPlan)
            '-- Nomor ---
            'Dim lbLineNo As Label = CType(E.Item.FindControl("lbLineNo"), Label)
            objPKProductionPlan.LineNo = (E.Item.ItemIndex + 1 + (dtgProductionPlan.PageSize * dtgProductionPlan.CurrentPageIndex)).ToString
            'lbLineNo.Text = objPKProductionPlan.LineNo
            lineProdPlan.No = objPKProductionPlan.LineNo
            '-----------
            Dim lblMaterialNumber As Label = CType(E.Item.FindControl("lblMaterialNumber"), Label)
            Dim lblMaterialDescription As Label = CType(E.Item.FindControl("lblMaterialDescription"), Label)
            If Not (objPKProductionPlan.VechileColor Is Nothing) Then
                lblMaterialNumber.Text = objPKProductionPlan.VechileColor.MaterialNumber
                lblMaterialDescription.Text = objPKProductionPlan.VechileColor.MaterialDescription
            Else
                lblMaterialNumber.Text = objPKProductionPlan.MaterialNumber
                lblMaterialDescription.Text = objPKProductionPlan.MaterialDescription
            End If
            lineProdPlan.KodeKendaraan = lblMaterialNumber.Text

            Dim lblProductionYear As Label = CType(E.Item.FindControl("lblProductionYear"), Label)
            lblProductionYear.Text = objPKProductionPlan.ProductionYear
            lineProdPlan.BulanPeriode = CType(objPKProductionPlan.PeriodMonth, Integer)
            lineProdPlan.TahunPeriode = CType(objPKProductionPlan.PeriodYear, Integer)
            lineProdPlan.TahunPerakitan = CType(lblProductionYear.Text, Integer)

            Dim lblPlanQty As Label = CType(E.Item.FindControl("lblPlanQty"), Label)
            lblPlanQty.Text = FormatNumber(objPKProductionPlan.PlanQty, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            totalRencanaJual += CInt(lblPlanQty.Text)
            lineProdPlan.StokAwalBulan = CType(lblPlanQty.Text, Integer)

            Dim lblAllocationQty As Label = CType(E.Item.FindControl("lblAllocationQty"), Label)
            lblAllocationQty.Text = FormatNumber(objPKProductionPlan.AllocationQty, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            totalAllocationQty += CInt(lblAllocationQty.Text)
            lineProdPlan.Alokasi = CType(lblAllocationQty.Text, Integer)

            Dim lblReserveQty As Label = CType(E.Item.FindControl("lblReserveQty"), Label)
            lblReserveQty.Text = FormatNumber(objPKProductionPlan.ReserveQty, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            totalReserveQty += CInt(lblReserveQty.Text)
            lineProdPlan.Reserve = CType(lblReserveQty.Text, Integer)

            Dim lblSisaStok As Label = CType(E.Item.FindControl("lblSisaStok"), Label)
            Dim _PlanQty As Integer = objPKProductionPlan.PlanQty
            Dim _AllocationQty As Integer = objPKProductionPlan.AllocationQty
            Dim _ReserveQty As Integer = objPKProductionPlan.ReserveQty
            objPKProductionPlan.StokSetelahAlokasi = FormatNumber((_PlanQty - _AllocationQty - _ReserveQty), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblSisaStok.Text = objPKProductionPlan.StokSetelahAlokasi
            E.Item.Cells(10).BackColor = IIf(objPKProductionPlan.StokSetelahAlokasi < 5, Color.Yellow, Color.Empty)
            totalsisaStock += CInt(lblSisaStok.Text)
            lineProdPlan.SisaStokSetelahAlokasi = CType(lblSisaStok.Text, Integer)

            If Not objPKProductionPlan Is Nothing Then
                If Not objPKProductionPlan.ErrorMessage Is Nothing Then
                    Dim lblPesanError As Label = CType(E.Item.FindControl("lblPesanError"), Label)
                    lblPesanError.Text = objPKProductionPlan.ErrorMessage & "(" & E.Item.ItemIndex + 1 & ")"
                End If
            End If
            'If lboxStatus.SelectedIndex = -1 Then
            '    objPKProductionPlan.CriteriaStatus = New Criteria(GetType(PKDetail), "PKHeader.PKStatus", MatchType.InSet, "(" & CType(enumStatusPK.Status.Baru, Integer) & "," & CType(enumStatusPK.Status.Validasi, Integer) & "," & CType(enumStatusPK.Status.Tunggu_Diskon, Integer) & "," & CType(enumStatusPK.Status.Konfirmasi, Integer) & "," & CType(enumStatusPK.Status.Rilis, Integer) & "," & CType(enumStatusPK.Status.Selesai, Integer) & "," & CType(enumStatusPK.Status.Setuju, Integer) & ")")
            'Else
            '    Dim filter As String = "("
            '    For Each item As ListItem In lboxStatus.Items
            '        If item.Selected = True Then
            '            filter += item.Value & ","
            '        End If
            '    Next
            '    filter = filter.Trim(",")
            '    filter += ")"
            '    objPKProductionPlan.CriteriaStatus = New Criteria(GetType(PKDetail), "PKHeader.PKStatus", MatchType.InSet, filter)
            'End If

            If dtgProductionPlan.Columns(12).Visible = True OrElse
                dtgProductionPlan.Columns(13).Visible = True OrElse
                dtgProductionPlan.Columns(14).Visible = True OrElse
                dtgProductionPlan.Columns(15).Visible = True OrElse
                dtgProductionPlan.Columns(16).Visible = True OrElse
                dtgProductionPlan.Columns(17).Visible = True OrElse
                dtgProductionPlan.Columns(18).Visible = True OrElse
                dtgProductionPlan.Columns(19).Visible = True Then

                Dim lblCountStatusBaruValidasi As Label = CType(E.Item.FindControl("lblCountStatusBaruValidasi"), Label)
                If Not IsNothing(lblCountStatusBaruValidasi) Then
                    lblCountStatusBaruValidasi.Text = FormatNumber(objPKProductionPlan.TotalBaruAndValidasi, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    lineProdPlan.BaruDanValidasi = CType(lblCountStatusBaruValidasi.Text, Integer)
                    totalStatusBaruValidasi += CInt(lblCountStatusBaruValidasi.Text)
                End If

                Dim lblCountStatusKonfirmasiTungguDiskon As Label = CType(E.Item.FindControl("lblCountStatusKonfirmasiTungguDiskon"), Label)
                If Not IsNothing(lblCountStatusKonfirmasiTungguDiskon) Then
                    lblCountStatusKonfirmasiTungguDiskon.Text = FormatNumber(objPKProductionPlan.TotalKonfirmasiAndTungguDiskon, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    lineProdPlan.KonfirmasiDanTungguDiskon = CType(lblCountStatusKonfirmasiTungguDiskon.Text, Integer)
                    totalStatusKonfirmasiTungguDiskon += CInt(lblCountStatusKonfirmasiTungguDiskon.Text)
                End If

                Dim lblCountStatusRilis As Label = CType(E.Item.FindControl("lblCountStatusRilis"), Label)
                If Not IsNothing(lblCountStatusRilis) Then
                    lblCountStatusRilis.Text = FormatNumber(objPKProductionPlan.TotalReleaseAndAgree, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    lineProdPlan.Rilis = CType(lblCountStatusRilis.Text, Integer)
                    totalRilis += CInt(lblCountStatusRilis.Text)
                End If

                Dim lblCountStatusTolak As Label = CType(E.Item.FindControl("lblCountStatusTolak"), Label)
                If Not IsNothing(lblCountStatusTolak) Then
                    lblCountStatusTolak.Text = FormatNumber(objPKProductionPlan.TotalTolak, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    lineProdPlan.Ditolak = CType(lblCountStatusTolak.Text, Integer)
                    totalTolak += CInt(lblCountStatusTolak.Text)
                End If

                Dim lblCountStatusSelesai As Label = CType(E.Item.FindControl("lblCountStatusSelesai"), Label)
                If Not IsNothing(lblCountStatusSelesai) Then
                    lblCountStatusSelesai.Text = FormatNumber(objPKProductionPlan.TotalSelesai, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    lineProdPlan.SelesaiOC = CType(lblCountStatusSelesai.Text, Integer)
                    totalSelesai += CInt(lblCountStatusSelesai.Text)
                End If

                Dim lblCountDO As Label = CType(E.Item.FindControl("lblCountDO"), Label)
                If Not IsNothing(lblCountDO) Then
                    'Dim dblTotalDO As Double = 0
                    'Dim dsTotalDO As DataSet = New PKProductionPlanFacade(User).DoRetrieveDataSet(objPKProductionPlan.VechileColor.ID, objPKProductionPlan.PeriodMonth, objPKProductionPlan.PeriodYear, objPKProductionPlan.ProductionYear)
                    'Try
                    '    dblTotalDO = dsTotalDO.Tables(0).Rows(0)("TotalDO")
                    'Catch
                    'End Try
                    'objPKProductionPlan.TotalDO = FormatNumber(dblTotalDO, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    lblCountDO.Text = objPKProductionPlan.TotalDO
                    lineProdPlan.DeliveryOrder = CType(lblCountDO.Text, Integer)
                    totalDO += CInt(lblCountDO.Text)
                End If

                Dim lblTotal As Label = CType(E.Item.FindControl("lblTotal"), Label)
                If Not IsNothing(lblTotal) Then
                    objPKProductionPlan.Total = FormatNumber(objPKProductionPlan.TotalBaruAndValidasi + objPKProductionPlan.TotalKonfirmasiAndTungguDiskon + objPKProductionPlan.TotalReleaseAndAgree + objPKProductionPlan.TotalTolak + objPKProductionPlan.TotalSelesai, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    lblTotal.Text = objPKProductionPlan.Total
                    lineProdPlan.Total = CType(lblTotal.Text, Integer)
                    totalAll += CInt(lblTotal.Text)
                End If

                Dim lblSisaStokDNet As Label = CType(E.Item.FindControl("lblSisaStokDNet"), Label)
                If Not IsNothing(lblSisaStokDNet) Then
                    objPKProductionPlan.SisaStokDNet = FormatNumber(CInt(objPKProductionPlan.PlanQty) - CInt(objPKProductionPlan.TotalSelesai) - lineProdPlan.Rilis, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    lblSisaStokDNet.Text = objPKProductionPlan.SisaStokDNet
                    lineProdPlan.SisaStokDNet = CType(lblSisaStokDNet.Text, Integer)
                    totalsisaStockDNet += CInt(lblSisaStokDNet.Text)
                End If
            End If
            dataForDownload.Add(lineProdPlan)

        ElseIf E.Item.ItemType = ListItemType.Footer Then
            E.Item.Cells(7).Text = FormatNumber(totalRencanaJual, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            E.Item.Cells(8).Text = FormatNumber(totalAllocationQty, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            E.Item.Cells(9).Text = FormatNumber(totalReserveQty, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            E.Item.Cells(10).Text = FormatNumber(totalsisaStock, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            E.Item.Cells(12).Text = FormatNumber(totalStatusBaruValidasi, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            E.Item.Cells(13).Text = FormatNumber(totalStatusKonfirmasiTungguDiskon, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            E.Item.Cells(14).Text = FormatNumber(totalRilis, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            E.Item.Cells(15).Text = FormatNumber(totalTolak, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            E.Item.Cells(16).Text = FormatNumber(totalSelesai, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            E.Item.Cells(17).Text = FormatNumber(totalDO, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            E.Item.Cells(18).Text = FormatNumber(totalAll, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            E.Item.Cells(19).Text = FormatNumber(totalsisaStockDNet, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim errorList As StringBuilder = New StringBuilder
        ProductionPlanArrayList = sessionHelper.GetSession(varSessUpload)
        For Each item As PKProductionPlan In ProductionPlanArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKProductionPlan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKProductionPlan), "PeriodMonth", MatchType.Exact, item.PeriodMonth))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKProductionPlan), "PeriodYear", MatchType.Exact, item.PeriodYear))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKProductionPlan), "VechileColor.MaterialNumber", MatchType.Exact, item.VechileColor.MaterialNumber))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKProductionPlan), "ProductionYear", MatchType.Exact, item.ProductionYear))
            Dim objArrList As ArrayList = New PKProductionPlanFacade(User).Retrieve(criterias)
            If (objArrList Is Nothing) OrElse (objArrList.Count <= 0) Then
                Dim objProductionPlanFacade As PKProductionPlanFacade = New PKProductionPlanFacade(User)
                Try
                    objProductionPlanFacade.Insert(item)
                Catch ex As Exception
                    errorList.Append("#" & item.VechileColor.ColorCode & "#")
                End Try
            Else
                item.id = CType(objArrList(0), PKProductionPlan).id
                item.CreatedBy = CType(objArrList(0), PKProductionPlan).CreatedBy
                Dim objProductionPlanFacade As PKProductionPlanFacade = New PKProductionPlanFacade(User)
                Try
                    objProductionPlanFacade.Update(item)
                Catch ex As Exception
                    errorList.Append("#" & item.VechileColor.ColorCode & "#")
                End Try
            End If
        Next
        If errorList.Length > 0 Then
            MessageBox.Show(SR.SaveFail & " : " & errorList.ToString)
        Else
            MessageBox.Show(SR.SaveSuccess)
        End If
        sessionHelper.RemoveSession(varSessUpload)
        btnSimpan.Enabled = False
        btnCari_Click(Nothing, Nothing)
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        DoInquiry()
    End Sub

    Private Sub DoInquiry()
        'btnSimpan.Enabled = False
        Dim dataForDownload As ArrayList = New ArrayList
        sessionHelper.SetSession(varSessDownload, dataForDownload)
        dtgProductionPlan.Columns(11).Visible = False
        dtgProductionPlan.Columns(12).Visible = True
        dtgProductionPlan.Columns(13).Visible = True
        dtgProductionPlan.Columns(14).Visible = True
        dtgProductionPlan.Columns(15).Visible = True
        dtgProductionPlan.Columns(16).Visible = True
        dtgProductionPlan.Columns(17).Visible = True
        dtgProductionPlan.Columns(18).Visible = True
        dtgProductionPlan.Columns(19).Visible = True
        dtgProductionPlan.Columns(20).Visible = True
        dtgProductionPlan.AllowSorting = True

        BindData()
        If dtgProductionPlan.Items.Count > 0 Then
            Try
                'DownLoadExcel()
                btnDownLoad.Enabled = True
            Catch ex As Exception
            End Try
        Else
            btnDownLoad.Enabled = False
        End If
    End Sub

    Private Sub InitFileToDownload()
        Dim list As ArrayList = New ArrayList
        Dim templateFile As String = Server.MapPath("~\DataFile\PK\TemplateDownload_RencanaProduksi.xlsx")
        Dim fileExtention As String = System.IO.Path.GetExtension(templateFile)
        Dim NewFileName = String.Format("RENCANA_PRODUKSI{0}{1}", Now.TimeOfDay.TotalSeconds.ToString, fileExtention)
        Dim CopyToFolder As String = "DataTemp"
        Dim NewCopy = Server.MapPath(String.Format("~\{0}\{1}", CopyToFolder, NewFileName))

        
        If System.IO.File.Exists(templateFile) = True Then
            System.IO.File.Copy(templateFile, NewCopy)
            Dim fileInfo As FileInfo = New FileInfo(NewCopy)
            Dim excelFile As ExcelPackage = New ExcelPackage(fileInfo)
            Dim ws As ExcelWorksheet = excelFile.Workbook.Worksheets(1)
            Dim data As ArrayList = sessionHelper.GetSession(varSessDownload)
            Try
                ws.Cells("A2").LoadFromDataTable(ArrayListToDataTable(data), False)
            Catch ex As Exception
                Dim a = ex.Message
            End Try

            excelFile.Save()
            sessionHelper.SetSession(varSessFileForDownload, String.Format("{0}\{1}", CopyToFolder, NewFileName))
        End If
    End Sub

    Private Sub DownLoadExcel()
        Dim list As ArrayList = New ArrayList
        Dim templateFile As String = Server.MapPath("~\DataFile\PK\TemplateDownload_RencanaProduksi.xlsx")
        Dim fileExtention As String = System.IO.Path.GetExtension(templateFile)
        'Dim NewFileName = String.Format("RENCANA_PRODUKSI{0}{1}", Now.TimeOfDay.TotalSeconds.ToString, fileExtention)
        Dim NewFileName = "Rencana_Produksi" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & fileExtention

        If System.IO.File.Exists(templateFile) = True Then
            Dim fileInfo As FileInfo = New FileInfo(templateFile)
            Dim excelFile As ExcelPackage = New ExcelPackage(fileInfo)
            Dim ws As ExcelWorksheet = excelFile.Workbook.Worksheets(1)
            Dim data As ArrayList = sessionHelper.GetSession(varSessDownload)
            Try
                ws.Cells("A2").LoadFromDataTable(ArrayListToDataTable(data), False)
            Catch ex As Exception
                Dim a = ex.Message
            End Try

            Dim fileBytes = excelFile.GetAsByteArray()


            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

            Dim success As Boolean = False

            Try
                success = imp.Start()
                If success Then
                    File.WriteAllBytes(Server.MapPath("~/DataTemp/" & NewFileName), fileBytes)
                    imp.StopImpersonate()
                End If

            Catch ex As Exception
                Exit Sub

            End Try
            sessionHelper.SetSession(varSessFileForDownload, NewFileName)
        End If
            

    End Sub

   


    Private Sub BindData()
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKProductionPlan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim tgl As DateTime = System.Convert.ToDateTime(ddlPeriode.SelectedValue)
        Dim strSql As String = String.Empty
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKProductionPlan), "PeriodMonth", MatchType.Exact, tgl.Month))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKProductionPlan), "PeriodYear", MatchType.Exact, tgl.Year))
        'If ddlKategori.SelectedIndex <> 0 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKProductionPlan), "VechileColor.VechileType.Category.ID", MatchType.Exact, ddlKategori.SelectedValue))
        'End If
        If ddlSubCategory.SelectedValue <> "-1" Then
            'CommonFunction.SetVehicleSubCategoryCriterias(ddlSubCategory, ddlKategori.SelectedItem.Text, criterias, "PKProductionPlan")
            strSql = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlSubCategory.SelectedValue
        Else
            strSql = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 "
            'criterias.opAnd(New Criteria(GetType(PKProductionPlan), "VechileColor.VechileType.VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))
        End If
        'If ddlTipe.SelectedIndex <> 0 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKProductionPlan), "VechileColor.VechileType.ID", MatchType.Exact, ddlTipe.SelectedValue))
        'End If


        'If ddlTipe.SelectedIndex <> 0 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKProductionPlan), "VechileColor.VechileType.ID", MatchType.Exact, ddlTipe.SelectedValue))
        'End If

        'If lBoxType.SelectedIndex <> -1 Then
        '    Dim filter As String = "("
        '    For Each item As ListItem In lBoxType.Items
        '        If item.Selected = True Then
        '            filter += item.Value & ","
        '        End If
        '    Next
        '    filter = filter.Trim(",")
        '    filter += ")"
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKProductionPlan), "VechileColor.VechileType.ID", MatchType.InSet, filter))
        'End If

        'ProductionPlanArrayList = New PKProductionPlanFacade(User).Retrieve(criterias, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        ProductionPlanArrayList = New PKProductionPlanFacade(User).RetrieveRencanaProduksi(CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection),
                                                                                           tgl.Month, tgl.Year, ddlKategori.SelectedValue, strSql,
                                                                                           ddlTipe.SelectedValue)
        sessionHelper.SetSession(varSessUpload, ProductionPlanArrayList)
        If ProductionPlanArrayList.Count > 0 Then
            BindToGrid()
        Else
            dtgProductionPlan.DataSource = ProductionPlanArrayList
            dtgProductionPlan.DataBind()
            MessageBox.Show("Data Produksi Tidak Ditemukan")
        End If

    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownLoad.Click
        DoDownload()

        'Dim _fileHelper As New FileHelper
        'Dim fileInfo1 As New FileInfo(Server.MapPath(""))
        'Try
        '    Dim str As FileInfo = _fileHelper.TransferURPtoText(UploadProductionPlanTransferData, fileInfo1)
        '    Response.Redirect("../Downloadlocal.aspx?file=" & KTB.DNet.Lib.WebConfig.GetValue("URPDestFileDirectory").ToString & "\" & str.Name)
        'Catch ex As Exception
        '    MessageBox.Show("Gagal Download File.")
        'End Try
    End Sub

    Private Sub DoDownload()
        DownLoadExcel()
        Dim filePath As String = sessionHelper.GetSession(varSessFileForDownload)
        'Response.Redirect("../downloadlocal.aspx?file=" & filePath)
        Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & filePath, True)
        'Dim fileInfo As New FileInfo(Server.MapPath("~\DataFile\PK\TemplateDownload_RencanaProduksi.xlsx"))
        'Try)
        '    Dim excelFile As ExcelPackage = New ExcelPackage(fileInfo)
        '    Dim ws As ExcelWorksheet = excelFile.Workbook.Worksheets(1)
        '    Dim data As ArrayList = sessionHelper.GetSession(varSessDownload)
        '    ws.Cells("A2").LoadFromDataTable(ArrayListToDataTable(data), False)
        '    excelFile.Save()
        '    Response.Redirect("../downloadlocal.aspx?file=" & fileInfo.Name)
        'Catch ex As Exception
        '    MessageBox.Show("Gagal Download File.")
        'End Try
    End Sub

    Private Function ArrayListToDataTable(ByVal arraylist As ArrayList) As System.Data.DataTable
        'Dim dt As DataTable = New DataTable()

        'If arraylist.Count <= 0 Then
        '    Return dt
        'End If

        'Dim propertiesinfo As PropertyInfo() = arraylist(0).GetType().GetProperties()

        'For Each pf As PropertyInfo In propertiesinfo
        '    Dim dc As DataColumn = New DataColumn(pf.Name)
        '    dc.DataType = pf.PropertyType
        '    dt.Columns.Add(dc)
        'Next

        'For Each ar As Object In arraylist
        '    Dim dr As DataRow = dt.NewRow
        '    Dim pf As PropertyInfo() = ar.GetType().GetProperties()

        '    For Each prop As PropertyInfo In pf
        '        dr(prop.Name) = prop.GetValue(ar, Nothing)
        '    Next
        '    dt.Rows.Add(dr)
        'Next
        'Return dt

        Dim dt As New System.Data.DataTable()

        For i As Integer = 0 To arraylist.Count - 1
            Dim GenericObject As Object = arraylist.Item(i)
            For Each item As PropertyInfo In GenericObject.GetType().GetProperties()
                Try
                    Dim column = New DataColumn()
                    Dim ColName As String = item.Name.ToString

                    column.ColumnName = ColName
                    dt.Columns.Add(column)

                Catch ex As Exception
                    Dim a As String = ex.Message
                End Try
            Next

            Dim row As DataRow = dt.NewRow()
            Try
                Dim j As Integer = 0
                For Each item As PropertyInfo In GenericObject.GetType().GetProperties()
                    row(j) = item.GetValue(GenericObject, Nothing)
                    j += 1
                Next

                dt.Rows.Add(row)
            Catch ex As Exception
                Dim b As String = ex.Message
            End Try
            

        Next
        Return dt

    End Function

    Private Function UploadProductionPlanTransferData() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim oExArgs As New System.Collections.ArrayList
        Dim objUploadProuctionPlan As New PKProductionPlanFacade(User)

        For Each oDataGridItem In dtgProductionPlan.Items
            Dim _Plan As New KTB.DNet.Domain.PKProductionPlan
            _Plan.id = CType(oDataGridItem.FindControl("lblID"), Label).Text
            _Plan = objUploadProuctionPlan.Retrieve(_Plan.id)
            oExArgs.Add(_Plan)
        Next
        Return oExArgs
    End Function

    Private Sub ddlSubCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubCategory.SelectedIndexChanged
        'BindVehicleType(True)
        BindddlTipe(ddlKategori.SelectedItem.ToString)
    End Sub
#End Region

    Protected Sub dtgProductionPlan_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs) Handles dtgProductionPlan.ItemCommand
        Select Case (e.CommandName)
            Case "Edit"
                dtgProductionPlan_EditCommand(e)
            Case "Update"
                dtgProductionPlan_Update(e)
            Case "Cancel"
                dtgProductionPlan_CancelCommand(e)
        End Select
    End Sub
    Private Sub dtgProductionPlan_EditCommand(ByVal e As DataGridCommandEventArgs)
        dtgProductionPlan.EditItemIndex = CInt(e.Item.ItemIndex)
        BindToGrid()
    End Sub
    Private Sub dtgProductionPlan_CancelCommand(ByVal e As DataGridCommandEventArgs)
        dtgProductionPlan.EditItemIndex = -1
        BindData()
    End Sub
    Private Sub dtgProductionPlan_Update(ByVal e As DataGridCommandEventArgs)
        UpdateCommand(e)
    End Sub
    Private Sub UpdateCommand(ByVal e As DataGridCommandEventArgs)
        Dim id As Integer = CType(CType(e.Item.FindControl("lblID"), Label).Text, Integer)
        Dim txtEditReserve As TextBox = CType(e.Item.FindControl("txtEditReserve"), TextBox)

        Dim kodekendaraan As String = CType(e.Item.FindControl("lblMaterialNumber"), Label).Text
        Dim periodebulan As Integer = CInt(CType(e.Item.FindControl("lblPeriodMonth"), Label).Text)
        Dim periodetahun As Integer = CInt(CType(e.Item.FindControl("lblPeriodYear"), Label).Text)
        Dim tahunproduksi As Integer = CInt(CType(e.Item.FindControl("lblProductionYear"), Label).Text)

        Dim reserveQty As Integer = CType(If(txtEditReserve.Text = "", 0, txtEditReserve.Text), Integer)
        Dim objFacade As PKProductionPlanFacade = New PKProductionPlanFacade(User)
        Dim obj As PKProductionPlan = objFacade.Retrieve(id)
        obj.ReserveQty = reserveQty
        objFacade.Update(obj)
        dtgProductionPlan.EditItemIndex = -1
        DoInquiry()
        dtgProductionPlan.ShowFooter = True
        'UpdateDataForDownload(kodekendaraan, periodebulan, periodetahun, tahunproduksi, reserveQty)
    End Sub
    Private Sub UpdateDataForDownload(ByVal kodekendaraan As String, ByVal periodebulan As Integer, ByVal periodetahun As Integer, ByVal tahunproduksi As Integer, ByVal reserveqty As Integer)
        Dim arr As ArrayList = sessionHelper.GetSession(varSessDownload)
        Dim list As List(Of GridProdPlan) = arr.Cast(Of GridProdPlan)().ToList()
        list.Where(Function(i) i.KodeKendaraan = kodekendaraan AndAlso i.BulanPeriode = periodebulan AndAlso i.TahunPeriode = periodetahun AndAlso i.TahunPerakitan = tahunproduksi).FirstOrDefault().Reserve = reserveqty
        arr.Clear()
        arr.AddRange(list)
        sessionHelper.SetSession(varSessDownload, arr)
    End Sub
End Class