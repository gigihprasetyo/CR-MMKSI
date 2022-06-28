#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Parser
Imports KTB.DNet.UI.Helper
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Configuration
#End Region

Public Class frmUploadPencairanDepositATahunan
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblFile As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents dgUpload As System.Web.UI.WebControls.DataGrid
    Protected WithEvents fileUpload As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents FromDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ToDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents ddlProductCategory As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Private Variables"
    Private sHelper As SessionHelper = New SessionHelper
    Private totalRow As Integer = 0
    Private arlToDisplay As ArrayList = New ArrayList
    Private srcfile As String
    Private Const VALID_FILE_TYPE = "EXCEL"
    Private Const VALID_FILE_TYPE2 = "OCTET-STREAM"

#End Region


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            Dim companyCode As String = KTB.DNET.Lib.WebConfig.GetValue("CompanyCode")
            sHelper.SetSession("ItemError", 0)
            GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True, companyCode)
        End If
        'If CheckBtnPriv() Then
        '    fileUpload.Visible = True
        '    btnSave.Visible = True
        '    btnUpload.Visible = True
        '    btnCancel.Visible = True
        'Else
        '    fileUpload.Visible = False
        '    btnSave.Visible = False
        '    btnUpload.Visible = False
        '    btnCancel.Visible = False
        'End If
    End Sub

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.DepositA_upload_pencairan_depositA_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Deposit A - Upload Pencairan Deposit A Tahunan")            
        End If
    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        sHelper.SetSession("ItemError", 0)
        Dim NamaFile As String = Path.GetFileName(fileUpload.PostedFile.FileName)
        If (fileUpload.PostedFile Is Nothing) OrElse fileUpload.PostedFile.ContentLength <= 0 Then
            MessageBox.Show("Pilih file yang akan di-upload.")
            Return
        Else
            If fileUpload.PostedFile.ContentType.ToString <> "application/vnd.ms-excel" And _
            fileUpload.PostedFile.ContentType.ToString <> "application/octet-stream" Then

                MessageBox.Show("Bukan file EXCEL. File anda " & fileUpload.PostedFile.ContentType)
                Return

            Else
                'cek maxFileSize First
                Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

                If fileUpload.PostedFile.ContentLength > maxFileSize Then
                    MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                    Exit Sub
                Else
                    Dim filename As String = System.IO.Path.GetFileName(fileUpload.PostedFile.FileName)
                    Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & filename
                    Dim arlParseResult As New ArrayList

                    If IsFileExist(targetFile) Then
                        MessageBox.Show(SR.UploadFail("PencairanDepositATahunan"))
                        Return
                    End If

                    Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                    Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                    Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

                    Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

                    sapImp.Start()

                    Try
                        SavingToFolder(targetFile, fileUpload.PostedFile)

                        Dim objUploadDepositParser As UploadPencairanDepositATahunanParser = New UploadPencairanDepositATahunanParser
                        Dim arSheet As ArrayList
                        arSheet = objUploadDepositParser.GetSheet(targetFile)
                        arlParseResult = objUploadDepositParser.ParsingExcel(targetFile, "[" & CType(arSheet(0), String) & "]", "User")
                        dgUpload.Columns(5).Visible = True
                        dgUpload.Columns(3).Visible = False
                        dgUpload.DataSource = arlParseResult
                        dgUpload.DataBind()
                    Catch
                        Throw

                    Finally
                        sapImp.StopImpersonate()
                    End Try
                End If
            End If
        End If

    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If CInt(sHelper.GetSession("ItemError")) > 0 Then
            MessageBox.Show("Masih Ada Kesalahan Data")
            Exit Sub
        End If

        If Date.Compare(FromDate.Value, ToDate.Value) < 0 Then
            If DateDiff(DateInterval.Month, FromDate.Value, ToDate.Value) < 6 Then
                MessageBox.Show("Range Periode kurang dari 6 bulan")
                Exit Sub
            End If
        Else
            MessageBox.Show("Tanggal akhir harus lebih besar dari tanggal awal")
            Exit Sub
        End If

        Dim objUserInfo As UserInfo = CType(sHelper.GetSession("LOGINUSERINFO"), UserInfo)
        Dim objAnnualDeposit As AnnualDepositAHeader

        Dim objAnnualDepositAHeaderFacade As New AnnualDepositAHeaderFacade(User)

        If dgUpload.Items.Count > 0 Then
            Dim ErrCount As Integer = 0

            For Each itemAlokasi As DataGridItem In dgUpload.Items
                Dim lblDealerCode As Label = itemAlokasi.FindControl("lblDealerCode")
                Dim lblJumlah As Label = itemAlokasi.FindControl("lblJumlah")
                Dim lblProductCategoryCode As Label = itemAlokasi.FindControl("lblProductCategoryCode")

                Dim objDealer As Dealer
                objDealer = New DealerFacade(User).Retrieve(lblDealerCode.Text)

                Dim objProducCategory As ProductCategory
                objProducCategory = New ProductCategoryFacade(User).Retrieve(lblProductCategoryCode.Text)

                If objDealer.ID > 0 Then
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "FromDate", MatchType.Exact, FromDate.Value))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "ToDate", MatchType.Exact, ToDate.Value))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "Dealer.ID", MatchType.Exact, objDealer.ID))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "ProductCategory.ID", MatchType.Exact, objProducCategory.ID))

                    Dim arlResult As ArrayList
                    arlResult = objAnnualDepositAHeaderFacade.Retrieve(criterias)

                    If arlResult.Count = 0 Then
                        objAnnualDeposit = New AnnualDepositAHeader
                    Else
                        objAnnualDeposit = arlResult(0)
                    End If

                    objAnnualDeposit.Dealer = objDealer
                    objAnnualDeposit.NettoAmount = CDec(lblJumlah.Text)
                    objAnnualDeposit.Status = 0
                    objAnnualDeposit.FromDate = FromDate.Value
                    objAnnualDeposit.ToDate = ToDate.Value
                    objAnnualDeposit.ProductCategory = objProducCategory
                    Dim iResult As Integer
                    If objAnnualDeposit.ID = 0 Then
                        iResult = objAnnualDepositAHeaderFacade.Insert(objAnnualDeposit)
                    Else
                        iResult = objAnnualDepositAHeaderFacade.Update(objAnnualDeposit)
                    End If
                    If iResult < 0 Then
                        MessageBox.Show(SR.SaveFail)
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("Dealer " & objDealer.DealerCode & " Tidak ada")
                    Exit Sub
                End If
            Next

            MessageBox.Show(SR.SaveSuccess)
        Else
            MessageBox.Show("Upload data terlebih dahulu")
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        btnSave.Enabled = False
        dgUpload.DataSource = Nothing
        dgUpload.DataBind()
    End Sub

    Private Sub dgUpload_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgUpload.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim objUpload As AnnualDepositAHeader = e.Item.DataItem
            If objUpload.ErrorMessage <> "" Then
                e.Item.BackColor = System.Drawing.Color.Red
                sHelper.SetSession("ItemError", CInt(sHelper.GetSession("ItemError")) + 1)                
            End If

            Dim lblJumlah As Label = e.Item.FindControl("lblJumlah")
            lblJumlah.Text = FormatNumber(objUpload.NettoAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub


#Region "Custom Method"

    Private Function IsFileExist(ByVal filename As String) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

        Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        sapImp.Start()

        Try
            '---Mode using sapImpersonate---
            Dim fInfo As System.IO.FileInfo = New System.IO.FileInfo(filename)

            'Return fInfo.Exists
            If fInfo.Exists Then
                fInfo.Delete()
                Return False
            End If

        Catch ex As IO.IOException
            Throw
        Catch
            Throw New Exception(SR.UploadFail(System.IO.Path.GetFileName(filename)))
        Finally
            sapImp.StopImpersonate()
        End Try
    End Function

    Private Sub SavingToFolder(ByVal targetFile As String, ByVal postedFile As HttpPostedFile)

        Try
            postedFile.SaveAs(targetFile)
            'fInfo.MoveTo(targetFile)

            Dim trgInfo As System.IO.FileInfo = New System.IO.FileInfo(targetFile)
            If Not trgInfo.Exists Then
                Throw New IO.IOException("File gagal disimpan di Server. Harap hubungi Administrator")
            End If
        Catch ex As IO.IOException
            Throw
        Catch
            Throw New Exception(SR.UploadFail(System.IO.Path.GetFileName(targetFile)))
        End Try
    End Sub


    Private Function uploadToServer() As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Try
            success = imp.Start()
            If success Then
                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("UploadAllocMatPromotion") & "\Upload" & srcfile   '-- Destination file
                Dim finfo As FileInfo = New FileInfo(DestFile)
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If
                fileUpload.PostedFile.SaveAs(DestFile)
            End If
            imp.StopImpersonate()
            imp = Nothing
            Return True
        Catch ex As Exception
            lblError.Text = "Gagal Impersonate"
            lblError.Visible = True
            Return False
        End Try
    End Function

    'Private Sub GetGeneratedNumber(ByVal id As Integer)
    '    Dim objStockMP As MaterialPromotionStockAdjustment = New MaterialPromotionStockAdjustmentFacade(User).Retrieve(id)
    '    If Not objStockMP Is Nothing Then
    '        txtKeterangan.Text = objStockMP.Keterangan
    '    End If
    'End Sub


#End Region

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        BindGrid()
    End Sub

    Private Sub BindGrid()
        Dim arlAnnual As New ArrayList

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If CType(FromDate.Value, Date) <= CType(ToDate.Value, Date) Then
            Dim TanggalAwal As New DateTime(CInt(FromDate.Value.Year), CInt(FromDate.Value.Month), CInt(FromDate.Value.Day), 0, 0, 0)
            Dim TanggalAkhir As New DateTime(CInt(ToDate.Value.Year), CInt(ToDate.Value.Month), CInt(ToDate.Value.Day), 0, 0, 0)

            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "FromDate", MatchType.GreaterOrEqual, Format(TanggalAwal, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "ToDate", MatchType.LesserOrEqual, Format(TanggalAkhir, "yyyy-MM-dd HH:mm:ss")))

            If CInt(ddlProductCategory.SelectedValue) > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "ProductCategory.ID", MatchType.Exact, ddlProductCategory.SelectedValue))
            End If


            arlAnnual = New AnnualDepositAHeaderFacade(User).RetrieveActiveList(criterias, 1, dgUpload.PageSize, _
                0, CType(ViewState("CurrentSortColumn"), String), _
                CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dgUpload.Columns(4).Visible = True
            dgUpload.Columns(5).Visible = False
            dgUpload.DataSource = arlAnnual
            dgUpload.DataBind()
        Else
            MessageBox.Show(SR.InvalidRangeDate)
        End If
    End Sub

    Private Sub dgUpload_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgUpload.SortCommand
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

        dgUpload.SelectedIndex = -1
        dgUpload.CurrentPageIndex = 0
        BindGrid()
    End Sub
End Class
