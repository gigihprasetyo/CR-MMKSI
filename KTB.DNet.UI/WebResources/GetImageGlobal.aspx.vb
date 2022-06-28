#Region "Summary"
'---------------------------------------------------'
'-- Program Code : frmGetImage.aspx               --'
'-- Program Name : Get Image                      --'
'-- Description  :                                --'
'---------------------------------------------------'
'-- Programmer   : Deddy H                        --'
'-- Start Date   : Jul 19 2007                    --'
'-- Update By    :                                --'
'-- Last Update  :                                --'
'---------------------------------------------------'
'-- Copyright © 2007 by Intimedia                 --'
'---------------------------------------------------'
#End Region

#Region "System Namespace"
Imports System
Imports System.IO
Imports System.Web
Imports System.Drawing.Imaging
Imports System.Drawing
#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.BusinessFacade.Salesman
#End Region

Public Class frmGetImageGlobal
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Fields"
    Private Const MAX_WIDTH As Integer = 70
    Private Const MAX_HEIGHT As Integer = 70
    Private sessHelp As SessionHelper = New SessionHelper
    Private isEncoded As Boolean = False
    'Private oPhisingGuardImage As PhisingGuardImage = New PhisingGuardImage
    'Private oPhisingGuardImageFacade As PhisingGuardImageFacade = New PhisingGuardImageFacade(User)
#End Region

#Region "EventHandler"

    Private Sub DrawImageFromFile(ByVal filePath As String, ByVal heigth As Integer, ByVal width As Integer)


        Dim objBitmap As Bitmap
        Try
            Dim fileInfo As New FileInfo(filePath)
        Catch ex As Exception
            Dim uriString As String = New Uri(filePath).LocalPath
            Dim fileInfo As New FileInfo(uriString)
        End Try

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then
                Dim oImg As System.Drawing.Image = System.Drawing.Bitmap.FromFile(filePath)
                'Dim oImg As System.Drawing.Image = System.Drawing.Bitmap.FromFile(KTB.DNet.Lib.WebConfig.GetValue("ImageTest"))
                objBitmap = New Bitmap(oImg)
                Response.ContentType = "Image/Jpeg"
                ResizePhoto(objBitmap, width, heigth).Save(Response.OutputStream, ImageFormat.Jpeg)
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Response.Write("File tidak ditemukan.")
            imp.StopImpersonate()
            imp = Nothing
        End Try

    End Sub
    Private Function CheckFileExist(ByVal fileinfo As FileInfo) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Try
            success = imp.Start()
            If success Then
                Return fileinfo.Exists
            End If
        Catch ex As Exception
            Return False
        Finally
            imp.StopImpersonate()
            imp = Nothing
        End Try

    End Function
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Dim id As Integer = CType(Request("id"), Integer)
        Dim type As String = CType(Request("type"), String)
        Dim filePath As String = CType(Request("file"), String)
        Dim heigth As Integer = CType(Request("hg"), Integer)
        Dim width As Integer = CType(Request("wd"), Integer)


        If Not IsNothing(Request("isEncoded")) Then
            If Request("isEncoded").ToString() = "1" Then
                isEncoded = True
            End If
        End If

        If type <> String.Empty Then
            Select Case type
                Case "ImageFile"
                    DrawImageFromFile(filePath, heigth, width)
                Case "SalesmanHeader"
                    DrawImageSalesmanHeader(id)
                Case "SalesmanUniform"
                    DrawImageSalesmanUniform(id)
                Case "SalesmanUniformBig"
                    DrawImageSalesmanUniformBig(id)

                Case "UserInfo"
                    DrawImageUserInfo(id)
                Case "DealerSlideShow"
                    DrawImageDealerSlideShow(id)
                Case "AuditScheduleDealer"
                    DrawImageAuditScheduleDealer(id)
                Case "FotoPerbaikanAuditScheduleDealer"
                    DrawImageFotoPerbaikanAuditScheduleDealer(id)
                Case "AuditScheduleDealerReport"
                    DrawImageAuditScheduleDealerReport(id)
                Case "AuditScheduleDealerBig"
                    DrawImageAuditScheduleDealerBig(id)
                Case "FotoPerbaikanAuditScheduleDealerBig"
                    DrawImageFotoPerbaikanAuditScheduleDealerBig(id)
                Case "SPKCustomer"
                    If isEncoded Then
                        DrawSPKKtp(Server.UrlDecode(CType(Request("file"), String)))
                    Else
                        DrawSPKKtp(CType(Request("file"), String))
                    End If
                Case "Chassis"
                    If isEncoded Then
                        DrawChassis(Server.UrlDecode(CType(Request("file"), String)))
                    Else
                        DrawChassis(CType(Request("file"), String))
                    End If
                Case "FakturEvidanceMonthlyDocument"
                    If isEncoded Then
                        DrawImageEvidanceFakturMonthlyDocument(CType(Request("file"), String))
                    Else
                        DrawImageEvidanceFakturMonthlyDocument(CType(Request("file"), String))
                    End If
            End Select
        End If
        'DrawImage(id)
    End Sub

#End Region

#Region "Custom Method"
    Dim imgForDealerSlideShow As Integer = 250
    Dim imgForSalesmanHeader As Integer = 100
    Dim imgForSalesmanUniform As Integer = 100
    Dim imgForSalesmanUniformBig As Integer = 500

    Dim imgForAuditScheduleDealer As Integer = 100
    Dim imgForAuditScheduleDealerBig As Integer = 500

    Private Sub DrawImageDealerSlideShow(ByVal nID As Integer)
        Dim objDomain As DealerProfilePhoto = New DealerProfilePhoto
        Dim objFacade As DealerProfilePhotoFacade = New DealerProfilePhotoFacade(User)
        objDomain = objFacade.Retrieve(nID)
        If Not IsNothing(objDomain) AndAlso Not IsNothing(objDomain.Image) Then
            Dim objStream As System.IO.Stream = New System.IO.MemoryStream
            objStream.Write(objDomain.Image, 0, objDomain.Image.Length)
            Dim objBitmap As Bitmap = New Bitmap(objStream)
            Try
                Response.ContentType = "Image/Jpeg"
                ResizePhoto(objBitmap, imgForDealerSlideShow, imgForDealerSlideShow).Save(Response.OutputStream, ImageFormat.Jpeg)
            Finally
                objBitmap.Dispose()
            End Try
        End If
    End Sub

    

    Private Sub DrawImageFotoPerbaikanAuditScheduleDealerBig(ByVal nID As Integer)
        Dim sHelper As New SessionHelper

        Dim objAuditScheduleDealer As AuditScheduleDealer = CType(sHelper.GetSession("SES_objAuditScheduleDealer"), AuditScheduleDealer)

        If (objAuditScheduleDealer.AuditScheduleDealerReports.Count > 0) Then
            Dim img As Byte() = CType(objAuditScheduleDealer.AuditScheduleDealerReports(nID), AuditScheduleDealerReport).ItemImageReparation

            If Not IsNothing(img) Then
                Dim objStream As System.IO.Stream = New System.IO.MemoryStream
                objStream.Write(img, 0, img.Length)
                Dim objBitmap As Bitmap = New Bitmap(objStream)
                Try
                    Response.ContentType = "Image/Jpeg"
                    ResizePhoto(objBitmap, imgForAuditScheduleDealerBig, imgForAuditScheduleDealerBig).Save(Response.OutputStream, ImageFormat.Jpeg)
                Finally
                    objBitmap.Dispose()
                End Try
            End If
        End If
    End Sub


    Private Sub DrawImageAuditScheduleDealerBig(ByVal nID As Integer)
        Dim sHelper As New SessionHelper

        'Dim objAuditScheduleDealer As AuditScheduleDealer = CType(sHelper.GetSession("SES_objAuditScheduleDealer"), AuditScheduleDealer)
        'Dim objAuditParameterPhoto As AuditParameterPhoto = CType(objAuditScheduleDealer.AuditSchedule.AuditParameter.AuditParameterPhotos(nID), AuditParameterPhoto)
        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealerReport), "AuditScheduleDealerID", MatchType.Exact, nID))
        'Dim arlreportx As ArrayList = New KTB.DNet.BusinessFacade.ShowroomAudit.AuditScheduleDealerReportFacade(User).Retrieve(criterias)
        Dim objAuditScheduleDealerReport As AuditScheduleDealerReport = New KTB.DNet.BusinessFacade.ShowroomAudit.AuditScheduleDealerReportFacade(User).Retrieve(nID)
        'If arlreportx.Count > 0 Then
        '    objAuditScheduleDealerReport = arlreportx(0)
        'Else
        '    objAuditScheduleDealerReport = New AuditScheduleDealerReport
        'End If

        If objAuditScheduleDealerReport.ID > 0 Then
            Dim img As Byte() = objAuditScheduleDealerReport.ItemImage

            If Not IsNothing(img) Then
                Dim objStream As System.IO.Stream = New System.IO.MemoryStream
                objStream.Write(img, 0, img.Length)
                Dim objBitmap As Bitmap = New Bitmap(objStream)
                Try
                    Response.ContentType = "Image/Jpeg"
                    ResizePhoto(objBitmap, imgForAuditScheduleDealerBig, imgForAuditScheduleDealerBig).Save(Response.OutputStream, ImageFormat.Jpeg)
                Finally
                    objBitmap.Dispose()
                End Try
            End If
        End If
    End Sub

    Private Sub DrawImageAuditScheduleDealer(ByVal nID As Integer)
        Dim sHelper As New SessionHelper

        'Dim objAuditScheduleDealer As AuditScheduleDealer = CType(sHelper.GetSession("SES_objAuditScheduleDealer"), AuditScheduleDealer)
        'Dim objAuditParameterPhoto As AuditParameterPhoto = CType(objAuditScheduleDealer.AuditSchedule.AuditParameter.AuditParameterPhotos(nID), AuditParameterPhoto)
        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealerReport), "AuditScheduleDealerID", MatchType.Exact, nID))
        'Dim arlreportx As ArrayList = New KTB.DNet.BusinessFacade.ShowroomAudit.AuditScheduleDealerReportFacade(User).Retrieve(criterias)
        Dim objAuditScheduleDealerReport As AuditScheduleDealerReport = New KTB.DNet.BusinessFacade.ShowroomAudit.AuditScheduleDealerReportFacade(User).Retrieve(nID)
        'If arlreportx.Count > 0 Then
        '    objAuditScheduleDealerReport = arlreportx(0)
        'Else
        '    objAuditScheduleDealerReport = New AuditScheduleDealerReport
        'End If

        If objAuditScheduleDealerReport.ID > 0 Then
            Dim img As Byte() = objAuditScheduleDealerReport.ItemImage

            If Not IsNothing(img) Then
                Dim objStream As System.IO.Stream = New System.IO.MemoryStream
                objStream.Write(img, 0, img.Length)
                Dim objBitmap As Bitmap = New Bitmap(objStream)
                Try
                    Response.ContentType = "Image/Jpeg"
                    ResizePhoto(objBitmap, imgForAuditScheduleDealer, imgForAuditScheduleDealer).Save(Response.OutputStream, ImageFormat.Jpeg)
                Finally
                    objBitmap.Dispose()
                End Try
            End If
        End If
    End Sub
    Private Sub DrawImageAuditScheduleDealerReport(ByVal nID As Integer)
        Dim sHelper As New SessionHelper

        Dim objAuditScheduleDealer As AuditScheduleDealer = CType(sHelper.GetSession("SES_objAuditScheduleDealer"), AuditScheduleDealer)

        If (objAuditScheduleDealer.AuditScheduleDealerReports.Count > 0) Then
            Dim img As Byte() = CType(objAuditScheduleDealer.AuditScheduleDealerReports(nID), AuditScheduleDealerReport).ItemImage

            If Not IsNothing(img) Then
                Dim objStream As System.IO.Stream = New System.IO.MemoryStream
                objStream.Write(img, 0, img.Length)
                Dim objBitmap As Bitmap = New Bitmap(objStream)
                Try
                    Response.ContentType = "Image/Jpeg"
                    ResizePhoto(objBitmap, imgForAuditScheduleDealer, imgForAuditScheduleDealer).Save(Response.OutputStream, ImageFormat.Jpeg)
                Finally
                    objBitmap.Dispose()
                End Try
            End If
        End If
    End Sub
    Private Sub DrawImageFotoPerbaikanAuditScheduleDealer(ByVal nID As Integer)
        Dim sHelper As New SessionHelper

        Dim objAuditScheduleDealer As AuditScheduleDealer = CType(sHelper.GetSession("SES_objAuditScheduleDealer"), AuditScheduleDealer)

        If (objAuditScheduleDealer.AuditScheduleDealerReports.Count > 0) Then
            Dim img As Byte() = CType(objAuditScheduleDealer.AuditScheduleDealerReports(nID), AuditScheduleDealerReport).ItemImageReparation

            If Not IsNothing(img) Then
                Dim objStream As System.IO.Stream = New System.IO.MemoryStream
                objStream.Write(img, 0, img.Length)
                Dim objBitmap As Bitmap = New Bitmap(objStream)
                Try
                    Response.ContentType = "Image/Jpeg"
                    ResizePhoto(objBitmap, imgForAuditScheduleDealer, imgForAuditScheduleDealer).Save(Response.OutputStream, ImageFormat.Jpeg)
                Finally
                    objBitmap.Dispose()
                End Try
            End If
        End If
    End Sub

    Private Sub DrawImageSalesmanHeader(ByVal nID As Integer)
        Dim objDomain As SalesmanHeader = New SalesmanHeader
        Dim objFacade As SalesmanHeaderFacade = New SalesmanHeaderFacade(User)
        objDomain = objFacade.Retrieve(nID)
        If Not IsNothing(objDomain) AndAlso Not IsNothing(objDomain.Image) Then
            Dim objStream As System.IO.Stream = New System.IO.MemoryStream
            objStream.Write(objDomain.Image, 0, objDomain.Image.Length)
            Dim objBitmap As Bitmap = New Bitmap(objStream)
            Try
                Response.ContentType = "Image/Jpeg"
                ResizePhotoSalesman(objBitmap, imgForSalesmanHeader, imgForSalesmanHeader).Save(Response.OutputStream, ImageFormat.Jpeg)
            Finally
                objBitmap.Dispose()
            End Try
        End If
    End Sub

    Private Sub DrawImageSalesmanUniform(ByVal nID As Integer)
        Dim objDomain As SalesmanUniform = New SalesmanUniform
        Dim objFacade As SalesmanUniformFacade = New SalesmanUniformFacade(User)
        objDomain = objFacade.Retrieve(nID)
        If Not IsNothing(objDomain) AndAlso Not IsNothing(objDomain.Image) Then
            Dim objStream As System.IO.Stream = New System.IO.MemoryStream
            objStream.Write(objDomain.Image, 0, objDomain.Image.Length)
            Dim objBitmap As Bitmap = New Bitmap(objStream)
            Try
                Response.ContentType = "Image/Jpeg"
                ResizePhoto(objBitmap, imgForSalesmanUniform, imgForSalesmanUniform).Save(Response.OutputStream, ImageFormat.Jpeg)
            Finally
                objBitmap.Dispose()
            End Try
        End If
    End Sub

    Private Sub DrawImageSalesmanUniformBig(ByVal nID As Integer)
        Dim objDomain As SalesmanUniform = New SalesmanUniform
        Dim objFacade As SalesmanUniformFacade = New SalesmanUniformFacade(User)
        objDomain = objFacade.Retrieve(nID)
        If Not IsNothing(objDomain) AndAlso Not IsNothing(objDomain.Image) Then
            Dim objStream As System.IO.Stream = New System.IO.MemoryStream
            objStream.Write(objDomain.Image, 0, objDomain.Image.Length)
            Dim objBitmap As Bitmap = New Bitmap(objStream)
            Try
                Response.ContentType = "Image/Jpeg"
                ResizePhoto(objBitmap, imgForSalesmanUniformBig, imgForSalesmanUniformBig).Save(Response.OutputStream, ImageFormat.Jpeg)
            Finally
                objBitmap.Dispose()
            End Try
        End If
    End Sub

    Private Sub DrawImageEvidanceFakturMonthlyDocument(ByVal nameImage As String)
        Dim varUpload As String = "\MonthlyDocEvidance\"
        Dim path As String = Server.MapPath("../DataFile/PPT/NotFound.png") '& "../DataFile/PPT/NotFound.png"
        Dim strSAP As String = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        Dim DirPath As String
        If Strings.Right(strSAP, 1).Equals("\") Then

            DirPath = KTB.DNet.Lib.WebConfig.GetValue("SAN") & varUpload & nameImage
        Else
            DirPath = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "\MonthlyDocEvidance\" & nameImage

        End If
        'ViewState("id_ppt").ToString()
        Dim objBitmap As Bitmap
        Dim oImg As System.Drawing.Image
        Dim filePath As String

        Dim ext As String = "png"

        Try
            ext = nameImage.Split(".")(1).ToString().ToLower()
        Catch ex As Exception

        End Try

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then



                oImg = System.Drawing.Bitmap.FromFile(DirPath)

                Response.ContentType = "image/" & ext

                Response.Cache.SetAllowResponseInBrowserHistory(False)
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.Cache.AppendCacheExtension("no-store, must-revalidate")
                Response.AppendHeader("Pragma", "no-cache")
                Response.AppendHeader("Expires", "0")

                Select Case ext
                    Case "png"
                        oImg.Save(Response.OutputStream, ImageFormat.Png)
                    Case "jpg", "jpeg"
                        oImg.Save(Response.OutputStream, ImageFormat.Jpeg)
                    Case "gif"
                        oImg.Save(Response.OutputStream, ImageFormat.Gif)
                    Case "bmp", "bitmap"
                        oImg.Save(Response.OutputStream, ImageFormat.Bmp)
                    Case Else
                        oImg.Save(Response.OutputStream, ImageFormat.Png)
                End Select

                Response.Flush()
                Response.End()
                oImg.Dispose()
                imp.StopImpersonate()
                imp = Nothing
            Else
                oImg = System.Drawing.Bitmap.FromFile(path)
                Response.ContentType = "image/" & ext
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
    End Sub

    Private Sub DrawSPKKtp(ByVal nameImage As String)
        Dim varUpload As String = "\OCR\"
        Dim path As String = Server.MapPath("../DataFile/PPT/NotFound.png") '& "../DataFile/PPT/NotFound.png"
        Dim strSAP As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder")
        Dim DirPath As String
        If Strings.Right(strSAP, 1).Equals("\") Then

            DirPath = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & varUpload & nameImage
        Else
            DirPath = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & "OCR\" & nameImage

        End If

        'ViewState("id_ppt").ToString()
        Dim objBitmap As Bitmap
        Dim oImg As System.Drawing.Image
        Dim filePath As String

        Dim ext As String = "png"

        Try
            ext = nameImage.Split(".")(1).ToString().ToLower()
        Catch ex As Exception

        End Try

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then



                oImg = System.Drawing.Bitmap.FromFile(DirPath)

                Response.ContentType = "image/" & ext

                Response.Cache.SetAllowResponseInBrowserHistory(False)
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.Cache.AppendCacheExtension("no-store, must-revalidate")
                Response.AppendHeader("Pragma", "no-cache")
                Response.AppendHeader("Expires", "0")

                Select Case ext
                    Case "png"
                        oImg.Save(Response.OutputStream, ImageFormat.Png)
                    Case "jpg", "jpeg"
                        oImg.Save(Response.OutputStream, ImageFormat.Jpeg)
                    Case "gif"
                        oImg.Save(Response.OutputStream, ImageFormat.Gif)
                    Case "bmp", "bitmap"
                        oImg.Save(Response.OutputStream, ImageFormat.Bmp)
                    Case Else
                        oImg.Save(Response.OutputStream, ImageFormat.Png)
                End Select

                Response.Flush()
                Response.End()
                oImg.Dispose()
                imp.StopImpersonate()
                imp = Nothing
            Else
                oImg = System.Drawing.Bitmap.FromFile(path)
                Response.ContentType = "image/" & ext
                oImg.Save(Response.OutputStream, ImageFormat.Png)
                oImg.Dispose()
                imp = Nothing
            End If
        Catch ex As Exception
            Try
                oImg = System.Drawing.Bitmap.FromFile(path)
                Response.ContentType = "image/png"
                oImg.Save(Response.OutputStream, ImageFormat.Png)
                oImg.Dispose()
                imp.StopImpersonate()
                imp = Nothing
            Catch esx As Exception

            End Try
            
        End Try




    End Sub

    Private Sub DrawChassis(ByVal nameImage As String)
        Dim varUpload As String = "\OCRChasiss\"
        Dim path As String = Server.MapPath("../DataFile/PPT/NotFound.png") '& "../DataFile/PPT/NotFound.png"
        Dim strSAP As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder")
        Dim DirPath As String
        If Strings.Right(strSAP, 1).Equals("\") Then

            DirPath = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & varUpload & nameImage
        Else
            DirPath = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & "OCRChasiss\" & nameImage

        End If

        'ViewState("id_ppt").ToString()
        Dim objBitmap As Bitmap
        Dim oImg As System.Drawing.Image
        Dim filePath As String

        Dim ext As String = "png"

        Try
            ext = nameImage.Split(".")(1).ToString().ToLower()
        Catch ex As Exception

        End Try

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then



                oImg = System.Drawing.Bitmap.FromFile(DirPath)

                Response.ContentType = "image/" & ext

                Response.Cache.SetAllowResponseInBrowserHistory(False)
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.Cache.AppendCacheExtension("no-store, must-revalidate")
                Response.AppendHeader("Pragma", "no-cache")
                Response.AppendHeader("Expires", "0")

                Select Case ext
                    Case "png"
                        oImg.Save(Response.OutputStream, ImageFormat.Png)
                    Case "jpg", "jpeg"
                        oImg.Save(Response.OutputStream, ImageFormat.Jpeg)
                    Case "gif"
                        oImg.Save(Response.OutputStream, ImageFormat.Gif)
                    Case "bmp", "bitmap"
                        oImg.Save(Response.OutputStream, ImageFormat.Bmp)
                    Case Else
                        oImg.Save(Response.OutputStream, ImageFormat.Png)
                End Select

                Response.Flush()
                Response.End()
                oImg.Dispose()
                imp.StopImpersonate()
                imp = Nothing
            Else
                oImg = System.Drawing.Bitmap.FromFile(path)
                Response.ContentType = "image/" & ext
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




    End Sub

    '07-Aug-2007    Deddy H     Untuk keperluan database salesmanUniform
    '04-Sep-2007    Diana     Untuk keperluan database UserInfo
    Private Sub DrawImageUserInfo(ByVal nID As Integer)
        Dim objDomain As UserInfo = New UserInfo
        Dim objFacade As UserInfoFacade = New UserInfoFacade(User)
        objDomain = objFacade.Retrieve(nID)
        If Not IsNothing(objDomain) AndAlso Not IsNothing(objDomain.Picture) Then
            Dim objStream As System.IO.Stream = New System.IO.MemoryStream
            objStream.Write(objDomain.Picture, 0, objDomain.Picture.Length)
            Dim objBitmap As Bitmap = New Bitmap(objStream)
            Try
                Response.ContentType = "Image/Jpeg"
                ResizePhoto(objBitmap, imgForSalesmanUniform, imgForSalesmanUniform).Save(Response.OutputStream, ImageFormat.Jpeg)
            Finally
                objBitmap.Dispose()
            End Try
        End If
    End Sub
    Private Function ResizePhoto(ByVal bmp As Bitmap, ByVal maxWidth As Integer, ByVal maxHeight As Integer) As Bitmap
        If (bmp.Width <= maxWidth) AndAlso (bmp.Height <= maxHeight) Then
            Return bmp
        End If

        Dim w, h As Integer
        If bmp.Height > bmp.Width Then
            w = maxWidth * bmp.Width / bmp.Height
            h = maxWidth * bmp.Height / bmp.Height
        Else
            w = maxWidth * bmp.Width / bmp.Width
            h = maxWidth * bmp.Height / bmp.Width
        End If
        Return bmp.GetThumbnailImage(w, h, New Image.GetThumbnailImageAbort(AddressOf Abort), IntPtr.Zero)
    End Function

    Private Function ResizePhotoSalesman(ByVal bmp As Bitmap, ByVal maxWidth As Integer, ByVal maxHeight As Integer) As Bitmap
        Return bmp
    End Function

    Private Function Abort() As Boolean
        Return False
    End Function
#End Region

End Class
