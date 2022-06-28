Imports System.IO
Imports System.Drawing.Imaging
Imports System.Drawing
Imports KTB.DNet.Utility
Imports System.Text

Public Class frmPresentationSlider
    Inherits System.Web.UI.Page

    Private ReadOnly varUpload As String = "Presentation\"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            If Request.QueryString("key") <> "" AndAlso Request.QueryString("number") <> "" AndAlso Not IsNothing(Session("id_ppt")) Then
                Dim objH As Hashtable
                objH = CType(Session("id_ppt"), Hashtable)
                If (objH("key").ToString() = Request.QueryString("key")) Then
                    hdnNumber.Value = Request.QueryString("number")
                    hdnKey.Value = Request.QueryString("key")
                    Dim _p As Integer = 1

                    For _p = 1 To CInt(hdnNumber.Value)
                        ddPage.Items.Add(New ListItem("Page  " & _p.ToString(), _p.ToString()))
                    Next

                    If Not IsNothing(Request.QueryString("token")) AndAlso Request.QueryString("token") <> "" Then
                        If Request.QueryString("token").ToString() = objH("token").ToString() Then
                            objH("token") = ""
                            Session("id_ppt") = objH
                        Else
                            Server.Transfer("../FrmAccessDenied.aspx?modulName=Marketing - Presentation")
                        End If

                    Else
                        Server.Transfer("../FrmAccessDenied.aspx?modulName=Marketing - Presentation")
                    End If

                End If
            End If
        End If

        If Request.QueryString("key") <> "" AndAlso Request.QueryString("page") <> "" Then
            If Not IsNothing(Session("id_ppt")) Then
                Dim objH As Hashtable
                objH = CType(Session("id_ppt"), Hashtable)
                If (objH("key").ToString() = Request.QueryString("key")) Then
                    DrawBlank(Request.QueryString("page").ToString(), objH("id").ToString())
                End If

            End If


        End If

    End Sub

    Private Sub DrawBlank_cancel(ByVal pageNamuber As String)
        Dim path As String = Server.MapPath("../DataFile/PPT/NotFound.png") '& "../DataFile/PPT/NotFound.png"
        Dim DirPath As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & varUpload & Request.QueryString("id").ToString() 'ViewState("id_ppt").ToString()
        Dim objBitmap As Bitmap
        Dim oImg As System.Drawing.Image
        Dim filePath As String

        If Not IsNothing(Session("id_ppt")) Then
            If Session("id_ppt").ToString() <> Request.QueryString("id").ToString() Then
                oImg = System.Drawing.Bitmap.FromFile(path)
                Response.ContentType = "image/png"
                oImg.Save(Response.OutputStream, ImageFormat.Png)
                oImg.Dispose()
                Return
            End If
        Else
            oImg = System.Drawing.Bitmap.FromFile(path)
            Response.ContentType = "image/png"
            oImg.Save(Response.OutputStream, ImageFormat.Png)
            oImg.Dispose()
            Return
        End If

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                If Not Directory.Exists(DirPath) Then
                    Throw New Exception("Directory Not Found")
                End If
                filePath = DirPath & "\Slide" & pageNamuber.ToString() & ".png"

                If Not File.Exists(filePath) Then
                    Throw New Exception("File Not Found")
                End If


                Dim Value As String = "data:image/gif;base64," + Convert.ToBase64String(File.ReadAllBytes(filePath))
                Response.Clear()
                'Response.ContentType = "text/plain"
                Response.Write(Value)
                Response.Flush()
                Response.End()
                imp.StopImpersonate()
                imp = Nothing
                'oImg = System.Drawing.Bitmap.FromFile(filePath)

                'Response.ContentType = "image/png"

                'Response.Cache.SetAllowResponseInBrowserHistory(False)
                'Response.Cache.SetCacheability(HttpCacheability.NoCache)

                'oImg.Save(Response.OutputStream, ImageFormat.Png)
                'oImg.Dispose()

            Else
                oImg = System.Drawing.Bitmap.FromFile(path)
                Response.ContentType = "image/png"
                oImg.Save(Response.OutputStream, ImageFormat.Png)
                oImg.Dispose()
                imp = Nothing
            End If
        Catch ex As Exception
            oImg = System.Drawing.Bitmap.FromFile(path)
            Response.ContentType = "image/png"
            oImg.Save(Response.OutputStream, ImageFormat.Png)
            oImg.Dispose()
            imp.StopImpersonate()
            imp = Nothing
        End Try


        'Response.ContentType = "image/png"
        'oImg.Save(Response.OutputStream, ImageFormat.Png)
        'oImg.Dispose()
        'oImg = Nothing

    End Sub

    Private Function GetBase64(ByVal id As String) As String

        Dim base64String As String = String.Empty
        Dim path As String = Server.MapPath("") & "../DataTemp/" & ViewState("id_ppt").ToString() & "/Slide" & id & ".PNG"

        ' Convert Image to Base64
        Using img = System.Drawing.Image.FromFile(path)
            ' Image Path from File Upload Controller
            Using memStream = New MemoryStream()
                img.Save(memStream, img.RawFormat)
                Dim imageBytes As Byte() = memStream.ToArray()

                ' Convert byte[] to Base64 String
                base64String = Convert.ToBase64String(imageBytes)

                '   return base64String;

            End Using
        End Using
        Return base64String
    End Function

    'Protected Sub BtnTest_Click(sender As Object, e As EventArgs) Handles BtnTest.Click
    '    Dim str As String = "../DataTemp/" & ViewState("id_ppt").ToString() & "/Slide3" & "" & ".PNG"
    '    slide.Src = "data:image/png;base64," + Convert.ToBase64String(File.ReadAllBytes(Server.MapPath(str)))
    '    ' @"data:image/gif;base64," + Convert.ToBase64String(File.ReadAllBytes(Server.MapPath(@"/images/your_image.gif")))
    'End Sub




    Private Sub DrawBlank(ByVal pageNamuber As String, ByVal id As String)
        Dim path As String = Server.MapPath("../DataFile/PPT/NotFound.png") '& "../DataFile/PPT/NotFound.png"
        Dim DirPath As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & varUpload & id 'ViewState("id_ppt").ToString()
        Dim objBitmap As Bitmap
        Dim oImg As System.Drawing.Image
        Dim filePath As String

       

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                If Not Directory.Exists(DirPath) Then
                    Throw New Exception("Directory Not Found")
                End If
                filePath = DirPath & "\Slide" & pageNamuber.ToString() & ".png"

                If Not File.Exists(filePath) Then
                    Throw New Exception("File Not Found")
                End If

                oImg = System.Drawing.Bitmap.FromFile(filePath)

                Response.ContentType = "image/png"

                Response.Cache.SetAllowResponseInBrowserHistory(False)
                Response.Cache.SetCacheability(HttpCacheability.NoCache) 
                Response.Cache.AppendCacheExtension("no-store, must-revalidate")
                Response.AppendHeader("Pragma", "no-cache")
                Response.AppendHeader("Expires", "0")

                oImg.Save(Response.OutputStream, ImageFormat.Png)
                Response.Flush()
                Response.End()
                oImg.Dispose()
                imp.StopImpersonate()
                imp = Nothing
            Else
                oImg = System.Drawing.Bitmap.FromFile(path)
                Response.ContentType = "image/png"
                oImg.Save(Response.OutputStream, ImageFormat.Png)
                oImg.Dispose()
                imp = Nothing
            End If
        Catch ex As Exception
            oImg = System.Drawing.Bitmap.FromFile(path)
            Response.ContentType = "image/png"
            oImg.Save(Response.OutputStream, ImageFormat.Png)
            oImg.Dispose()
            imp.StopImpersonate()
            imp = Nothing
        End Try


        'Response.ContentType = "image/png"
        'oImg.Save(Response.OutputStream, ImageFormat.Png)
        'oImg.Dispose()
        'oImg = Nothing

    End Sub
End Class
