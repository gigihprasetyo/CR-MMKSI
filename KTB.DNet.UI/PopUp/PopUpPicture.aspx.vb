Imports System.IO
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.BabitSalesComm
Imports KTB.DNet.Utility


Public Class PopUpPicture
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents imgDisplay As System.Web.UI.WebControls.Image
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents lbtnDownload As System.Web.UI.WebControls.LinkButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private sessHelp As SessionHelper = New SessionHelper
    Private _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
    Private _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
    Private _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
    Private SourceFile As String = ""
    Private DestFile As String = ""

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Dim szId As String = Request.QueryString("id")
        Dim szType As String = Request.QueryString("type")

        Dim szfile As String = ""

        Dim obj As PengajuanDesignIklan = New PengajuanDesignIklanFacade(User).Retrieve(CInt(szId))
        If (szType = "1") Then
            szfile = KTB.DNet.Lib.WebConfig.GetValue("IklanBABIT") & "\" & obj.UploadeIklan
        Else
            Dim objSampleIklan As SampleIklan = New SampleIklanFacade(User).Retrieve(obj.NamaIklanKTB)
            szfile = objSampleIklan.UploadedIklan
        End If

        imgDisplay.ImageUrl = String.Format("~/Event/EventImageHandler.aspx?file={0}", szfile)
        imgDisplay.Visible = True

        'Put user code to initialize the page here
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)

        'If Not IsPostBack Then
        '    Dim JenisPicture As Integer = Request.QueryString("Jenis")
        '    Dim pathPicture As String

        '    If JenisPicture = 1 Then 'BabitIklanKTB
        '        pathPicture = GetPathBabitIklanKTB()
        '    ElseIf JenisPicture = 2 Then 'BabitIklanDealer
        '        pathPicture = GetPathBabitIklanDealer()
        '    ElseIf JenisPicture = 3 Then 'AuditReport
        '        pathPicture = GetPathAuditReport()
        '    ElseIf JenisPicture = 4 Then 'AuditReportRep
        '        pathPicture = GetPathAuditReportRep()
        '    End If


        '    If pathPicture = "" Then
        '        lblError.Text = "File Not Found !"
        '        lbtnDownload.Visible = False
        '    Else
        '        imgDisplay.ImageUrl = pathPicture
        '        lbtnDownload.CommandArgument = SourceFile
        '    End If

    End Sub

    Private Sub lbtnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnDownload.Click
        Response.Redirect("../Download.aspx?file=" & lbtnDownload.CommandArgument)
        'Response.Redirect("../Download.aspx?file=" & "\\172.17.104.203\ZDNet\Repository\BSI-Net\BABIT\Iklan\021107094046\Winter.jpg")
    End Sub


    Private Function GetPathAuditReport() As String
        imgDisplay.Width = Unit.Empty
        imgDisplay.Height = Unit.Empty
        imgDisplay.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & Request.QueryString("ID") & "&type=" & "AuditScheduleDealerBig"
        Return imgDisplay.ImageUrl
        SourceFile = imgDisplay.ImageUrl

    End Function

    Private Function GetPathAuditReportRep() As String
        imgDisplay.Width = Unit.Empty
        imgDisplay.Height = Unit.Empty

        imgDisplay.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & Request.QueryString("ID") & "&type=" & "FotoPerbaikanAuditScheduleDealerBig"
        Return imgDisplay.ImageUrl
        SourceFile = imgDisplay.ImageUrl

    End Function

    Private Function GetPathBabitIklanKTB() As String
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        If imp.Start Then

            Dim NoIklan As String = Request.QueryString("NoIklan")
            Dim objUser As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
            Dim objSampleIklan As SampleIklan = New SampleIklanFacade(User).Retrieve(NoIklan)

            SourceFile = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "\" & objSampleIklan.UploadedIklan  '-- Destination file
            DestFile = KTB.DNet.Lib.WebConfig.GetValue("EULAPATH") & "DataTemp\" & objUser.UserName & objUser.Dealer.DealerCode & "\" & objSampleIklan.UploadedIklan '-- Destination file

            Dim f As FileInfo = New FileInfo(DestFile)
            Dim DirLokal As String = f.Directory.ToString

            If Directory.Exists(DirLokal) Then
                Directory.Delete(DirLokal, True)
            End If
            Directory.CreateDirectory(DirLokal)


            If File.Exists(SourceFile) Then
                If Not (File.Exists(DestFile)) Then
                    File.Copy(SourceFile, DestFile)
                End If
            Else
                Return ""
            End If
            imp.StopImpersonate()
            imp = Nothing
            Return "..\DataTemp\" & objUser.UserName & objUser.Dealer.DealerCode & "\" & objSampleIklan.UploadedIklan

        Else
            Return ""
        End If


    End Function

    Private Function GetPathBabitIklanDealer() As String
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        If imp.Start Then

            Dim IDPengajuan As String = Request.QueryString("IDPengajuan")
            Dim objUser As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
            Dim objPengajuanIklan As PengajuanDesignIklan = New PengajuanDesignIklanFacade(User).Retrieve(CInt(IDPengajuan))


            SourceFile = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("IklanBABIT") & "\" & objPengajuanIklan.UploadeIklan  '-- Destination file
            DestFile = KTB.DNet.Lib.WebConfig.GetValue("EULAPATH") & "DataTemp\" & objUser.UserName & objUser.Dealer.DealerCode & "\" & New FileInfo(objPengajuanIklan.UploadeIklan).Name '-- Destination file

            Dim f As FileInfo = New FileInfo(DestFile)
            Dim DirLokal As String = f.Directory.ToString

            If Directory.Exists(DirLokal) Then
                Directory.Delete(DirLokal, True)
            End If
            Directory.CreateDirectory(DirLokal)


            If File.Exists(SourceFile) Then
                File.Copy(SourceFile, DestFile, True)
            Else
                Return ""
            End If
            imp.StopImpersonate()
            imp = Nothing
            Return "..\DataTemp\" & objUser.UserName & objUser.Dealer.DealerCode & "\" & New FileInfo(objPengajuanIklan.UploadeIklan).Name

        Else
            Return ""
        End If


    End Function


End Class
