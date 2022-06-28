Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser.ExcelParser
Imports KTB.DNet.Parser.Domain
Imports KTB.DNet.Security

Namespace KTB.DNet.UI.SparePart

    Public Class FrmUploadPRP
        Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents lblUploadBy As System.Web.UI.WebControls.Label
        Protected WithEvents ddlMonth As System.Web.UI.WebControls.DropDownList
        Protected WithEvents txtYear As System.Web.UI.WebControls.TextBox
        Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
        Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
        Protected WithEvents inFileLocation As System.Web.UI.HtmlControls.HtmlInputFile
        Protected WithEvents rfvMonth As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents reTahun As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents btnUpload As System.Web.UI.HtmlControls.HtmlInputButton
        Protected WithEvents Requiredfieldvalidator3 As System.Web.UI.WebControls.RequiredFieldValidator

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object
        Private bPrivilegeUploadPRP As Boolean = False

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Const VALID_FILE_TYPE = "EXCEL"
        Private Const VALID_FILE_TYPE2 = "OCTET-STREAM"
        Dim sHPrp As SessionHelper = New SessionHelper
        Private RootDestDir As String = KTB.DNet.Lib.WebConfig.GetValue("SPFileDirectory")
        Private usrInfo As Dealer

#Region "Event Method"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'for testing purpose only
            'sHPrp.SetSession("DEALER", New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(47))
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            usrInfo = CType(sHPrp.GetSession("DEALER"), Dealer)
            ActivateUserPrivilege()
            If Not IsPostBack Then
                InitiatePage()
            End If
        End Sub
        Private Sub SetControlPrivilege()
            btnUpload.Visible = bPrivilegeUploadPRP
        End Sub

        Private Sub ActivateUserPrivilege()
            bPrivilegeUploadPRP = SecurityProvider.Authorize(Context.User, SR.SaveUploadPRP_Privilege)
            
            If Not SecurityProvider.Authorize(Context.User, SR.ViewUploadPRP_Privilege) Or Not SecurityProvider.Authorize(Context.User, SR.SaveUploadPRP_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=PARTSHOP REWARD PROGRAM - Upload PRP")
            End If
        End Sub

        Private Sub InitiatePage()
            lblUploadBy.Text = UserInfo.Convert(User.Identity.Name)
            BindMonth()
            BindCategory()
            SetControlPrivilege()
            'ClearPage()
        End Sub

        Private Sub ClearPage()
            'ddlMonth.SelectedIndex = 0
            txtYear.Text = ""
            txtDescription.Text = ""
            'ddlCategory.SelectedIndex = 0
        End Sub

        Private Sub BindCategory()
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PRPCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(PRPCategory), "Status", MatchType.Exact, 0))
            Dim prpCategories As ArrayList = New PRPCategoryFacade(User).Retrieve(crit)
            Dim lItem As ListItem
            For Each prpCat As PRPCategory In prpCategories
                lItem = New ListItem(prpCat.CategoryName, prpCat.ID)
                ddlCategory.Items.Add(lItem)
            Next
        End Sub

        Private Sub BindMonth()
            Dim lItem As ListItem

            For numMonth As Integer = 1 To 12
                lItem = New ListItem(MonthName(numMonth), CStr(numMonth))
                ddlMonth.Items.Add(lItem)
            Next

        End Sub

        Private Function isPageValid() As Boolean
            If inFileLocation.PostedFile.ContentType.ToUpper.IndexOf(VALID_FILE_TYPE) < 0 _
                And inFileLocation.PostedFile.ContentType.ToUpper.IndexOf(VALID_FILE_TYPE2) < 0 Then
                Throw New Exception("Bukan file EXCEL. File anda " & inFileLocation.PostedFile.ContentType)
                Return False
            End If
            If txtYear.Text = "" Or ddlMonth.SelectedIndex < 0 Then
                Throw New Exception(SR.InvalidSendDate)
                Return False
            ElseIf txtYear.Text <> "" Then
                If CInt(txtYear.Text) < 1900 Then
                    Throw New Exception("Tanggal harus >= 1900")
                    Return False
                End If
            End If

            Return True
        End Function

        Private Function IsUnhack() As Boolean
            If txtYear.Text.IndexOf("<") >= 0 Or txtYear.Text.IndexOf(">") >= 0 Or txtYear.Text.IndexOf("'") >= 0 Then
                Return False
            End If

            If txtDescription.Text.IndexOf("<") >= 0 Or txtDescription.Text.IndexOf(">") >= 0 Or txtDescription.Text.IndexOf("'") >= 0 Then
                Return False
            End If

            Return True
        End Function

        Private Sub btnUpload_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.ServerClick
            SyncLock GetType(FrmUploadPRP)
                Try
                    InsertData()
                    ClearPage()
                    Throw New Exception(SR.SaveSuccess)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                Finally
                    Application.UnLock()
                End Try
            End SyncLock
        End Sub

#End Region

#Region "Insert Data and Upload"
        Private Sub InsertData()
            If isPageValid() Then
                If Not IsUnhack() Then
                    Throw New DataException("< dan > bukan karakter valid")
                End If
                Dim objPRPCategory As PRPCategory = New PRPCategoryFacade(User).Retrieve(CInt(ddlCategory.SelectedValue))
                If IsNothing(objPRPCategory) Then
                    Throw New DataException(SR.DataNotFound("PRPCategory"))
                End If
                If inFileLocation.PostedFile.ContentLength <> inFileLocation.PostedFile.InputStream.Length Then
                    Throw New DataException(SR.InvalidData(inFileLocation.PostedFile.FileName))
                End If

                'cek maxFileSize first
                Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

                If inFileLocation.PostedFile.ContentLength > maxFileSize Then
                    MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                    Exit Sub
                End If

                Dim filename As String = System.IO.Path.GetFileName(inFileLocation.PostedFile.FileName)
                Dim targetFile As String = New System.Text.StringBuilder(RootDestDir). _
                    Append("\").Append(filename).ToString

                If IsFileExist(RootDestDir + "\" + filename) Then
                    Throw New DataException(SR.FileIsExist(filename))
                End If

                Dim objPRPFile As PRPFile = New PRPFile
                Dim period As Date = New Date(CInt(txtYear.Text), ddlMonth.SelectedValue, 1)
                objPRPFile.Period = period
                objPRPFile.PRPCategory = objPRPCategory
                objPRPFile.Filename = filename
                objPRPFile.Description = txtDescription.Text
                'Created by is inputed manually
                objPRPFile.CreatedBy = User.Identity.Name

                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

                Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

                sapImp.Start()

                Try
                    SavingToFolder(targetFile, inFileLocation.PostedFile)
                    Dim excelStatus As PRPExcelParser.ValidStatus
                    Dim objPRPExcelParser As PRPExcelParser = New PRPExcelParser

                    Try
                        excelStatus = objPRPExcelParser.IsExcelValid(targetFile.ToString)
                    Catch
                        Throw
                    End Try

                    If excelStatus = objPRPExcelParser.ValidStatus.ErrorDealer Then
                        Throw New DataException("Error sheet PERDLR")
                    ElseIf excelStatus = objPRPExcelParser.ValidStatus.ErrorToko Then
                        Throw New DataException("Error sheet PERTOKO")
                    ElseIf excelStatus = objPRPExcelParser.ValidStatus.InValidFile Then
                        Throw New DataException("File Excel Salah")
                    ElseIf excelStatus = objPRPExcelParser.ValidStatus.ErrorDataInSheetToko Then
                        Throw New DataException("Data pada sheet PERTOKO tidak valid")
                    ElseIf excelStatus = objPRPExcelParser.ValidStatus.ErrorDataInSheetDealer Then
                        Throw New DataException("Data pada sheet PERDLR tidak valid")
                    End If

                    If excelStatus = PRPExcelParser.ValidStatus.Valid Then
                        Dim nResult As Integer = New PRPFileFacade(User).Insert(objPRPFile)
                        If nResult = -1 Then
                            Throw New Exception(SR.SaveFail)
                        End If
                    End If
                Catch
                    CleanTemporaryFile(targetFile.ToString)
                    Throw
                Finally
                    sapImp.StopImpersonate()
                End Try
            Else
                Throw New Exception("Data tidak valid")
            End If
        End Sub

        Private Sub SavingToFolder(ByVal targetFile As String, ByVal postedFile As HttpPostedFile)

            Try
                Dim directoryFile As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(RootDestDir)
                If Not directoryFile.Exists Then
                    directoryFile.Create()
                End If

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

        Private Function IsFileExist(ByVal filename As String) As Boolean
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

            Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

            sapImp.Start()

            Try
                '---Mode using transfer file---
                'Dim objUpload As TransferFile = New TransferFile(_user, _password, _sapServer)
                'objUpload.copyFile(filename, newFolder, False)

                '---Mode using sapImpersonate---
                Dim fInfo As System.IO.FileInfo = New System.IO.FileInfo(filename)

                Return fInfo.Exists

            Catch ex As IO.IOException
                Throw
            Catch
                Throw New Exception(SR.UploadFail(System.IO.Path.GetFileName(filename)))
            Finally
                sapImp.StopImpersonate()
            End Try
        End Function

        Private Function CleanTemporaryFile(ByVal tempFilename As String) As Boolean
            Try
                System.IO.File.Delete(tempFilename)
            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function

#End Region

    End Class
End Namespace
