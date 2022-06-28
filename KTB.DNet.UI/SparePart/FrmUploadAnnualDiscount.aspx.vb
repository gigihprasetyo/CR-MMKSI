#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Configuration
#End Region

Public Class FrmUploadAnnualDiscount
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents TRDealer As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents ddlProgramName As System.Web.UI.WebControls.DropDownList
    Protected WithEvents FileEnum As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents lblFileName As System.Web.UI.WebControls.Label
    Protected WithEvents intiFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents intiTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnShowAllProgram As System.Web.UI.WebControls.Button
    Protected WithEvents ddlGroup As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTipe As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblGroup As System.Web.UI.WebControls.Label

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
    Private srcfile As String
    Private _tipeFile As Int16
    Private objDealer As Dealer
#End Region

#Region "Custom Method"

    Private Sub bindToddlEnum()
        ddlProgramName.Items.Clear()
        Dim arrAnnual As ArrayList
        Dim listitemBlank As ListItem = New ListItem("Silahkan Pilih", -1)

        'arrAnnual = New FileUploadAnnualDiscountFacade(User).RetrieveActiveList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FileUploadAnnualDiscount), "RowStatus", MatchType.No, CType(DBRowStatus.Deleted, Short)))
        If ddlTipe.SelectedIndex <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FileUploadAnnualDiscount), "Tipe", MatchType.Exact, ddlTipe.SelectedIndex))
            If ddlTipe.SelectedIndex = 2 Then
                lblGroup.Visible = True
                ddlGroup.Visible = True
            Else
                lblGroup.Visible = False
                ddlGroup.Visible = False
            End If
        End If


        Dim sortColl As SortCollection = New SortCollection
        If (Not IsNothing("ProgramName")) Then
            sortColl.Add(New Sort(GetType(FileUploadAnnualDiscount), "ProgramName", Sort.SortDirection.ASC))
        Else
            sortColl = Nothing
        End If

        If CType(intiFrom.Value, Date) <= CType(intiTo.Value, Date) Then '--Create New Calendar
            Dim tglFrom As New Date(intiFrom.Value.Year, intiFrom.Value.Month, intiFrom.Value.Day, 0, 0, 0)
            Dim tglTo As New Date(intiTo.Value.Year, intiTo.Value.Month, intiTo.Value.Day, 23, 59, 59)
            '--Get Criterias From Selected Calendar
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FileUploadAnnualDiscount), "CreatedTime", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FileUploadAnnualDiscount), "CreatedTime", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")))
        Else
            ddlProgramName.DataBind()
            MessageBox.Show("Tanggal Awal Lebih Besar Dari Tanggal Akhir")
            Return
        End If

        arrAnnual = New FileUploadAnnualDiscountFacade(User).Retrieve(criterias, sortColl)

        ddlProgramName.Items.Add(listitemBlank)
        For Each item As FileUploadAnnualDiscount In arrAnnual
            If item.ProgramName.Length > 8 Then
                Dim _Uploaded As String = Right(item.ProgramName, 8)
                If _Uploaded <> "Uploaded" Then
                    ddlProgramName.Items.Add(New ListItem(item.ProgramName, item.ID))
                End If
            Else
                ddlProgramName.Items.Add(New ListItem(item.ProgramName, item.ID))
            End If
        Next

        'ddlProgramName.DataSource = arrAnnual
        'ddlProgramName.DataTextField = "ProgramName"
        'ddlProgramName.DataValueField = "ID"
        'ddlProgramName.Items.Add(listitemBlank)
        'ddlProgramName.DataBind()
    End Sub

    Private Sub BindToDDLGroup()
        ddlGroup.DataSource = New DealerGroupFacade(User).RetrieveList
        ddlGroup.DataTextField = "GroupName"
        ddlGroup.DataValueField = "ID"
        ddlGroup.DataBind()
    End Sub

    Private Sub copyfiletowebserver(ByRef _SrcFile As String, Optional ByVal dealername As String = Nothing, Optional ByVal groupname As String = Nothing)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        If FileEnum.Value <> "" OrElse FileEnum.Value <> Nothing Then
            'cek maxFileSize first
            Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

            If FileEnum.PostedFile.ContentLength > maxFileSize Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                Exit Sub
            End If

            _SrcFile = Path.GetFileName(FileEnum.PostedFile.FileName)  '-- Source file name
            'If (_SrcFile.ToString.ToUpper = PROGRAM_ANNUAL_DISCOUNT.ToUpper) OrElse (_SrcFile.ToString.ToUpper = PETUNJUK_PELAKSANAAN.ToUpper) OrElse (_SrcFile.ToString.ToUpper = HASIL_ANNUAL_DISCOUNT.ToUpper) Then
            If (_SrcFile.ToString.ToUpper = lblFileName.Text.ToString.ToUpper) Then
                Dim DestFile As String
                Dim finfo As FileInfo
                Try
                    success = imp.Start()
                    If success Then
                        If CType(ViewState("_TipeFile"), Integer) = 1 Then
                            'DestFile = Server.MapPath("") & "\..\DataFile\AnnualDiscount\" & groupname & "\" & dealername & "\" & _SrcFile  '-- Destination file
                            DestFile = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "\..\DataFile\AnnualDiscount\" & groupname & "\" & dealername & "\" & _SrcFile  '-- Destination file
                            finfo = New FileInfo(DestFile)
                            If Not finfo.Directory.Exists Then
                                Directory.CreateDirectory(finfo.DirectoryName)
                            End If
                        ElseIf CType(ViewState("_TipeFile"), Integer) = 2 Then
                            'DestFile = Server.MapPath("") & "\..\DataFile\AnnualDiscount\" & groupname & "\" & _SrcFile  '-- Destination file
                            DestFile = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "\..\DataFile\AnnualDiscount\" & groupname & "\" & _SrcFile  '-- Destination file
                            finfo = New FileInfo(DestFile)
                            If Not finfo.Directory.Exists Then
                                Directory.CreateDirectory(finfo.DirectoryName)
                            End If
                        Else
                            'DestFile = Server.MapPath("") & "\..\DataFile\AnnualDiscount\General" & "\" & _SrcFile  '-- Destination file
                            DestFile = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "\..\DataFile\AnnualDiscount\General" & "\" & _SrcFile  '-- Destination file
                            finfo = New FileInfo(DestFile)

                            If Not finfo.Directory.Exists Then
                                Directory.CreateDirectory(finfo.DirectoryName)
                            End If
                        End If

                        FileEnum.PostedFile.SaveAs(DestFile)
                        imp.StopImpersonate()
                        imp = Nothing
                    End If
                Catch ex As Exception
                    MessageBox.Show("Gagal Impersonate")
                End Try

                If CType(ViewState("_TipeFile"), Integer) = 1 Then
                    'CopytoAnOtherWebServer(finfo, groupname, dealername)
                ElseIf CType(ViewState("_TipeFile"), Integer) = 2 Then
                    'CopytoAnOtherWebServer(finfo, groupname)
                Else
                    'CopytoAnOtherWebServer(finfo)
                End If

            Else
                _SrcFile = Nothing
            End If
        Else
            _SrcFile = Nothing
        End If
    End Sub

    Private Sub CopytoAnOtherWebServer(ByVal finfo As FileInfo, Optional ByVal groupName As String = "", Optional ByVal dealerCode As String = "")
        Dim helper As FileHelper = New FileHelper
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim otherFolder As String

        If groupName = "" Then
            otherFolder = "General"
        ElseIf dealerCode = "" Then
            otherFolder = groupName
        Else
            otherFolder = groupName & "\" & dealerCode
        End If

        Try
            success = imp.Start()
            If success Then
                helper.TransferToListWebServer(finfo, otherFolder, True, "AnnualDiscountDirectory")
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Proses Upload Gagal")
        End Try
    End Sub


    Private Sub UpdateRowStatus(ByVal _filename As String)
        Dim objFileUploadAnnualDiscount As FileUploadAnnualDiscount
        Dim objFileUploadAnnualDiscountFacade As New FileUploadAnnualDiscountFacade(User)
        objFileUploadAnnualDiscount = objFileUploadAnnualDiscountFacade.RetrieveByFileName(_filename)
        objFileUploadAnnualDiscount.ProgramName = objFileUploadAnnualDiscount.ProgramName & "-" & "Uploaded"
        objFileUploadAnnualDiscount.RowStatus = DBRowStatus.Active
        objFileUploadAnnualDiscountFacade.Update(objFileUploadAnnualDiscount)
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        CheckUserPrivilege()
        If Not IsPostBack Then

            'bindToddlEnum()
            Dim listitemBlank As ListItem = New ListItem("Silahkan Pilih", -1)
            ddlProgramName.Items.Add(listitemBlank)
            BindToDDLGroup()
        End If
    End Sub

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.ViewAnnualDiscountUpload_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Upload File Annual Discount")
        End If

        FileEnum.Visible = SecurityProvider.Authorize(Context.User, SR.BrowseAnnualDiscountUpload_Privilege)
        btnUpload.Visible = SecurityProvider.Authorize(Context.User, SR.UploadAnnualDiscountUpload_Privilege)
    End Sub

    Private Function CheckFileExist(ByVal finfo As FileInfo) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Try
            If imp.Start() Then
                If finfo.Exists Then
                    Return True
                Else
                    Return False
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Dim NamaFile As String = Path.GetFileName(FileEnum.PostedFile.FileName)

        If CType(ViewState("_TipeFile"), Integer) = 1 Then
            If (NamaFile.ToString.ToUpper <> lblFileName.Text.ToString.ToUpper) Then
                MessageBox.Show("Nama Program dan Nama File yang dipilih tidak sama")
            Else
                Dim objDealer As New Dealer
                objDealer = New DealerFacade(User).Retrieve(viewstate("_DealerCode"))
                If Not IsNothing(objDealer) Then
                    If objDealer.DealerGroup Is Nothing Then
                        copyfiletowebserver(srcfile, viewstate("_DealerCode"), "SingleGroup") 'objDealer.DealerCode)
                    Else
                        copyfiletowebserver(srcfile, viewstate("_DealerCode"), objDealer.DealerGroup.GroupName) 'objDealer.DealerCode)
                    End If
                    If (srcfile <> Nothing) Then
                        ddlProgramName.ClearSelection()
                        MessageBox.Show("Data Berhasil Di Upload Untuk dealer " & objDealer.SearchTerm2)
                    Else
                        MessageBox.Show("Pilih Lokasi File")
                    End If
                End If
            End If
        ElseIf CType(ViewState("_TipeFile"), Integer) = 2 Then
            If (NamaFile.ToString.ToUpper <> lblFileName.Text.ToString.ToUpper) Then
                MessageBox.Show("Nama Program dan Nama File yang dipilih tidak sama")
            Else
                'Dim objDealer As New Dealer
                'objDealer = New DealerFacade(User).Retrieve(viewstate("_DealerCode"))
                copyfiletowebserver(srcfile, , ddlGroup.SelectedItem.Text.ToString) 'objDealer.DealerCode)
                If (srcfile <> Nothing) Then
                    ddlProgramName.ClearSelection()
                    MessageBox.Show("Data Berhasil Di Upload Untuk Group " & ddlGroup.SelectedItem.Text.ToString)
                Else
                    MessageBox.Show("Pilih Lokasi File")
                End If
            End If
        Else
            If (NamaFile.ToString.ToUpper <> lblFileName.Text.ToString.ToUpper) Then
                MessageBox.Show("Nama Program dan Nama File yang dipilih tidak sama")
            Else
                'Dim objDealer As New Dealer
                'objDealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text)
                copyfiletowebserver(srcfile)
                If (srcfile <> Nothing) Then
                    ddlProgramName.ClearSelection()
                    MessageBox.Show("Data General Berhasil Di Upload")
                Else
                    MessageBox.Show("Pilih Lokasi File")
                End If
            End If
        End If

        UpdateRowStatus(NamaFile)
        bindToddlEnum()
    End Sub

    Private Sub ddlProgramName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlProgramName.SelectedIndexChanged
        If ddlProgramName.SelectedValue = -1 Then
            lblFileName.Text = String.Empty
        Else
            Dim _objFileUploadAnnualDiscount As New FileUploadAnnualDiscount
            _objFileUploadAnnualDiscount = New FileUploadAnnualDiscountFacade(User).Retrieve(CInt(ddlProgramName.SelectedValue))

            lblFileName.Text = _objFileUploadAnnualDiscount.FileName.ToString
            ViewState.Add("_TipeFile", _objFileUploadAnnualDiscount.Tipe)
            'ViewState.Add("_GroupName", _objFileUploadAnnualDiscount)
            If _objFileUploadAnnualDiscount.Tipe = 2 Then
                '  TRDealer.Visible = True
            ElseIf _objFileUploadAnnualDiscount.Tipe = 1 Then
                ViewState.Add("_DealerCode", lblFileName.Text.Substring(0, 6))
                '  TRDealer.Visible = False
            Else
                ' TRDealer.Visible = False
            End If
        End If
    End Sub

    Private Sub btnShowAllProgram_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowAllProgram.Click
        bindToddlEnum()
    End Sub

#End Region

End Class