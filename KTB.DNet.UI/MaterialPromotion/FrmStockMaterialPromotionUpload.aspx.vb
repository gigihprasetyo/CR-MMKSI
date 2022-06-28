#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.MaterialPromotion
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

Public Class FrmStockMaterialPromotionUpload
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblFile As System.Web.UI.WebControls.Label
    Protected WithEvents fileUpload As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents dgUpload As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label

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

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            sHelper.SetSession("ItemError", 0)
        End If
        If CheckBtnPriv() Then
            fileUpload.Visible = True
            btnSave.Visible = True
            btnUpload.Visible = True
            btnCancel.Visible = True
        Else
            fileUpload.Visible = False
            btnSave.Visible = False
            btnUpload.Visible = False
            btnCancel.Visible = False
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
                        MessageBox.Show(SR.UploadFail("StockMaterialPromotion"))
                        Return
                    End If

                    Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                    Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                    Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

                    Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

                    sapImp.Start()

                    Try
                        SavingToFolder(targetFile, fileUpload.PostedFile)

                        Dim objUploadSMPParser As UploadStockMaterialPromotionParser = New UploadStockMaterialPromotionParser
                        Dim arSheet As ArrayList
                        arSheet = objUploadSMPParser.GetSheet(targetFile)
                        arlParseResult = objUploadSMPParser.ParsingExcel(targetFile, "[" & CType(arSheet(0), String) & "]", "User")

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

        Dim objUserInfo As UserInfo = CType(sHelper.GetSession("LOGINUSERINFO"), UserInfo)

        If dgUpload.Items.Count > 0 Then
            Dim ErrCount As Integer = 0

            For Each itemAlokasi As DataGridItem In dgUpload.Items
                Dim lblGoodsNo As Label = itemAlokasi.FindControl("lblGoodsNo")
                Dim lblDescription As Label = itemAlokasi.FindControl("lblDescription")
                Dim lblUnit As Label = itemAlokasi.FindControl("lblUnit")
                Dim lblStock As Label = itemAlokasi.FindControl("lblStock")

                Dim adjType As Short
                Dim goodno As String = lblGoodsNo.Text
                Dim nmBarang As String = lblDescription.Text
                Dim unit As String = lblUnit.Text
                Dim qty As Integer = CInt(lblStock.Text)
                If qty > 0 Then
                    adjType = 1
                Else
                    adjType = 2
                    qty = qty * -1
                End If
                Dim desc As String = "Adjustment dari upload stock"

                Dim objMPMaster As KTB.DNet.Domain.MaterialPromotion = New KTB.DNet.Domain.MaterialPromotion
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MaterialPromotion), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MaterialPromotion), "GoodNo", MatchType.Exact, goodno))
                Dim arlMPMaster As ArrayList = New MaterialPromotionFacade(User).Retrieve(criterias)
                If Not arlMPMaster Is Nothing Then
                    If arlMPMaster.Count > 0 Then
                        objMPMaster = arlMPMaster(0)
                    End If
                End If

                objMPMaster.GoodNo = goodno
                objMPMaster.Name = nmBarang

                'cek apakah diobject yang diambil sudah ada stoknya
                If objMPMaster.Stock = 0 Then
                    If adjType = 1 Then
                        Dim stok As Integer = objMPMaster.Stock
                        objMPMaster.Stock = objMPMaster.Stock + qty

                        Dim objStockMP As New KTB.DNet.Domain.MaterialPromotionStockAdjustment
                        objStockMP.Dealer = objUserInfo.Dealer
                        objStockMP.StockAwal = stok
                        objStockMP.AdjustType = adjType
                        objStockMP.Description = desc
                        objStockMP.Qty = qty

                        objMPMaster.MaterialPromotionStockAdjustments.Insert(0, objStockMP)

                        Dim iResult As Integer = New MaterialPromotionFacade(User).InsertTransactionStock(objMPMaster)

                        If iResult = -1 Then
                            MessageBox.Show(objMPMaster.GoodNo & "tidak berhasil disimpan")
                            ErrCount = 1
                        End If
                    ElseIf adjType = 2 Then
                        MessageBox.Show("Tidak bisa melakukan insert data Adjustment Out, karena Stok anda: " & objMPMaster.Stock)
                    Else
                        MessageBox.Show("Silahkan pilih Adjustment yang diinginkan")
                    End If
                Else
                    If adjType = 1 Then
                        Dim stok As Integer = objMPMaster.Stock
                        objMPMaster.Stock = objMPMaster.Stock + qty

                        Dim objStockMP As New KTB.DNet.Domain.MaterialPromotionStockAdjustment
                        objStockMP.Dealer = objUserInfo.Dealer
                        objStockMP.StockAwal = stok
                        objStockMP.AdjustType = adjType
                        objStockMP.Description = desc
                        objStockMP.Qty = qty

                        objMPMaster.MaterialPromotionStockAdjustments.Insert(0, objStockMP)

                        Dim iResult As Integer = New MaterialPromotionFacade(User).InsertTransactionStock(objMPMaster)

                        If iResult = -1 Then
                            MessageBox.Show(objMPMaster.GoodNo & "tidak berhasil disimpan")
                            ErrCount = 1
                        End If
                    ElseIf adjType = 2 Then
                        Dim stok As Integer = objMPMaster.Stock
                        objMPMaster.Stock = objMPMaster.Stock - qty

                        If objMPMaster.Stock < 0 Then
                            MessageBox.Show("Adjustment Out tidak bisa dilakukan. Stok kurang dari 1")
                        Else
                            Dim objStockMP As New KTB.DNet.Domain.MaterialPromotionStockAdjustment
                            objStockMP.Dealer = objUserInfo.Dealer
                            objStockMP.StockAwal = stok
                            objStockMP.AdjustType = adjType
                            objStockMP.Description = desc
                            objStockMP.Qty = qty

                            objMPMaster.MaterialPromotionStockAdjustments.Insert(0, objStockMP)

                            Dim iResult As Integer = New MaterialPromotionFacade(User).InsertTransactionStock(objMPMaster)

                            If iResult = -1 Then
                                MessageBox.Show(objMPMaster.GoodNo & "tidak berhasil disimpan")
                                ErrCount = 1
                            End If
                        End If
                    Else
                        MessageBox.Show("Silahkan pilih Adjustment yang diinginkan")
                    End If
                End If
            Next

            If errCount = 0 Then
                MessageBox.Show("Data berhasil disimpan")
            End If
        Else
            MessageBox.Show("Upload data terlebih dahulu")
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        'fileUpload.Value = String.Empty
        btnSave.Enabled = False
        dgUpload.DataSource = Nothing
        dgUpload.DataBind()
    End Sub

    Private Sub dgUpload_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgUpload.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim objUpload As KTB.DNet.Parser.StockMaterialPromotionUpload = e.Item.DataItem
            If objUpload.ErrorMessage <> "" Then
                e.Item.BackColor = System.Drawing.Color.Red
                sHelper.SetSession("ItemError", CInt(sHelper.GetSession("ItemError")) + 1)
            End If
        End If
    End Sub

#End Region

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

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionAllocationViewUpload_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Material Promosi - Upload Alokasi Material Promosi")
        End If
    End Sub

    Private Function CheckBtnPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionAllocationUploadFile_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region

End Class
