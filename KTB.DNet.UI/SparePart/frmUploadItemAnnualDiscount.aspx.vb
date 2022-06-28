Imports Ktb.DNet.Parser
Imports KTB.DNet.Domain
Imports Ktb.DNet.BusinessFacade.SparePart
Imports Ktb.DNet.BusinessFacade
Imports KTB.DNet.Utility
Imports Ktb.DNet.Domain.Search
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security

Imports System.IO

Public Class frmUploadItemAnnualDiscount
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPilihLokasiFile As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents DataFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents dtgItemAnnualDiscount As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents icPeriodeAwal As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPeriodeAkhir As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnClear As System.Web.UI.WebControls.Button
    Protected WithEvents btnHapus As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private ArrListAnnualDiscountItem As ArrayList
    Private sessionHelper As New SessionHelper

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        CheckUserPrivilege()
        If Not IsPostBack Then

        End If
    End Sub

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.ViewAnnualDiscountUploadItem_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Pengelolaan File Annual Discount ")
        End If
        DataFile.Visible = SecurityProvider.Authorize(Context.User, SR.BrowseAnnualDiscountUploadItem_Privilege)
        btnUpload.Visible = SecurityProvider.Authorize(Context.User, SR.UploadAnnualDiscountUploadItem_Privilege)
        btnSimpan.Visible = SecurityProvider.Authorize(Context.User, SR.SaveAnnualDiscountUploadItem_Privilege)
        btnClear.Visible = SecurityProvider.Authorize(Context.User, SR.NewAnnualDiscountUploadItem_Privilege)
        btnHapus.Visible = SecurityProvider.Authorize(Context.User, SR.DeleteAnnualDiscountUploadAll_Privilege)
    End Sub

    Sub paging_grid(ByVal s As Object, ByVal e As DataGridPageChangedEventArgs)
        dtgItemAnnualDiscount.CurrentPageIndex = e.NewPageIndex
        BindDataToGrid()
    End Sub

    Sub dtgAnnualDiscount_ItemDataBound(ByVal sender As System.Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemIndex >= 0 Then
            If ArrListAnnualDiscountItem(e.Item.ItemIndex).ErrorMessage <> String.Empty Then
                btnSimpan.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        If IsValidPeriode() Then
            If UploadFile() Then
                dtgItemAnnualDiscount.Columns(7).Visible = True
                btnSimpan.Enabled = True
                btnClear.Enabled = True
                icPeriodeAkhir.Enabled = False
                icPeriodeAwal.Enabled = False
                BindDataToGrid()
            End If
        End If
    End Sub

    Private Function UploadFile() As Boolean
        If (Not DataFile.PostedFile Is Nothing) AndAlso (DataFile.PostedFile.ContentLength > 0) AndAlso DataFile.PostedFile.FileName.Substring(DataFile.PostedFile.FileName.Length - 4).ToUpper = ".XLS" Then
            'cek maxFileSize first
            Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

            If DataFile.PostedFile.ContentLength > maxFileSize Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                Exit Function
            End If

            Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName)  '-- Source file name
            Dim DestFile As String = Server.MapPath("") & "\..\DataTemp\" & SrcFile '-- Destination file
            Dim fileInfoDestination As New FileInfo(DestFile)
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            Dim success As Boolean = False
            Try
                success = imp.Start()
                If success Then
                    If Not fileInfoDestination.Exists Then
                        fileInfoDestination.Directory.Create()
                    End If
                    If System.IO.File.Exists(DestFile) Then
                        System.IO.File.Delete(DestFile)
                    End If
                    DataFile.PostedFile.SaveAs(DestFile)
                    imp.StopImpersonate()
                    imp = Nothing
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Dim validFrom As New DateTime(icPeriodeAwal.Value.Year, icPeriodeAwal.Value.Month, icPeriodeAwal.Value.Day, 0, 0, 0)
            Dim validTo As New DateTime(icPeriodeAkhir.Value.Year, icPeriodeAkhir.Value.Month, icPeriodeAkhir.Value.Day, 0, 0, 0)
            Try
                Dim parser As IExcelParser = New UploadAnnualDiscountParser
                ArrListAnnualDiscountItem = parser.ParseExcelNoTransactionWithValidDate(DestFile, "[Sheet1$]", User.Identity.Name, validFrom, validTo)
                sessionHelper.SetSession("AnnualDiscount", ArrListAnnualDiscountItem)
                Return True
            Catch ex As Exception
                MessageBox.Show("Upload Gagal")
                Return False
            End Try
        Else
            MessageBox.Show("file tidak ada, atau format file salah")
            Return False
        End If
    End Function

    Private Sub BindDataToGrid()
        ArrListAnnualDiscountItem = sessionHelper.GetSession("AnnualDiscount")
        dtgItemAnnualDiscount.DataSource = ArrListAnnualDiscountItem
        dtgItemAnnualDiscount.DataBind()
    End Sub

    Private Function IsValidPeriode() As Boolean
        If icPeriodeAwal.Value.Year <> icPeriodeAkhir.Value.Year Then
            MessageBox.Show("Periode harus berada dalam 1 tahun")
            Return False
        Else
            If icPeriodeAkhir.Value <= icPeriodeAwal.Value Then
                MessageBox.Show("Periode Awal harus lebih kecil dari periode akhir")
                Return False
            End If
        End If
        Return True
    End Function

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        dtgItemAnnualDiscount.DataSource = Nothing
        dtgItemAnnualDiscount.DataBind()
        icPeriodeAkhir.Enabled = True
        icPeriodeAwal.Enabled = True
        sessionHelper.RemoveSession("AnnualDiscount")
        btnHapus.Enabled = False
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        ArrListAnnualDiscountItem = sessionHelper.GetSession("AnnualDiscount")
        For Each item As AnnualDiscount In ArrListAnnualDiscountItem
            Dim annualDiscountFacade As annualDiscountFacade = New annualDiscountFacade(User)
            annualDiscountFacade.Insert(item)
        Next
        MessageBox.Show(SR.SaveSuccess)
        btnHapus.Enabled = True
        sessionHelper.RemoveSession("AnnualDiscount")
        btnSimpan.Enabled = False
        btnCari_Click(Nothing, Nothing)
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgItemAnnualDiscount.Columns(7).Visible = False
        BindData()
    End Sub

    Private Sub BindData()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim tglawal As New DateTime(icPeriodeAwal.Value.Year, icPeriodeAwal.Value.Month, icPeriodeAwal.Value.Day, 0, 0, 0)
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscount), "ValidateDateFrom", MatchType.Exact, tglawal))
        Dim tglakhir As New DateTime(icPeriodeAkhir.Value.Year, icPeriodeAkhir.Value.Month, icPeriodeAkhir.Value.Day, 0, 0, 0)
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscount), "ValidateDateTo", MatchType.Exact, tglakhir))

        ArrListAnnualDiscountItem = New AnnualDiscountFacade(User).Retrieve(criterias)
        If ArrListAnnualDiscountItem.Count > 0 Then
            btnHapus.Enabled = True
        End If
        sessionHelper.SetSession("AnnualDiscount", ArrListAnnualDiscountItem)
        BindDataToGrid()
    End Sub

    Private Sub btnHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHapus.Click
        ArrListAnnualDiscountItem = sessionHelper.GetSession("AnnualDiscount")
        Dim objAnnualDiscountFacade As New AnnualDiscountFacade(User)
        For Each item As AnnualDiscount In ArrListAnnualDiscountItem
            objAnnualDiscountFacade.Delete(item)
        Next
        sessionHelper.RemoveSession("AnnualDiscount")
        BindDataToGrid()
        btnHapus.Enabled = False
    End Sub

    Private Sub dtgItemAnnualDiscount_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgItemAnnualDiscount.ItemDataBound
        If e.Item.ItemIndex > -1 Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgItemAnnualDiscount.CurrentPageIndex * dtgItemAnnualDiscount.PageSize)
        End If
    End Sub
End Class