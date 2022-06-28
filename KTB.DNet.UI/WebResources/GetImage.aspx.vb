Imports System
Imports System.Web
Imports System.Drawing.Imaging
Imports System.Drawing
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Lib
Imports System.Math
Public Class GetImage
    Inherits System.Web.UI.Page
    Private Const MAX_WIDTH As Integer = 70
    Private Const MAX_HEIGHT As Integer = 70
    Private sHelper As SessionHelper = New SessionHelper

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim allow As Boolean = False
        Dim Parm As Integer = Integer.Parse(Request("id").ToString)
        Dim desc As String
        Try
            desc = Request("title").ToString
        Catch ex As Exception
            desc = ""
        End Try

        If Parm > 0 Then
            allow = True
        End If

        Try
            If allow Then
                If desc = "" Then
                    DrawPhoto(Parm)
                Else
                    DrawImage(Parm, desc)
                End If

            Else
                DrawBlank()
            End If
        Catch ex As Exception
            DrawBlank()
        End Try
    End Sub

#Region "Private"
    Private Sub DrawBlank()
        Dim objBitmap As Bitmap
        Dim objGraphics As Graphics
        Dim GrayBrush = New SolidBrush(Color.Gray)
        Dim BlackBrush = New SolidBrush(Color.Black)
        Dim WhiteBrush = New SolidBrush(Color.White)
        objBitmap = New Bitmap(200, 200)
        objGraphics = Graphics.FromImage(objBitmap)
        objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
        objGraphics.FillRectangle(WhiteBrush, 0, 0, 200, 200)
        objGraphics.DrawRectangle(New Pen(Color.Black, 2), 0, 0, 200, 200)
        objGraphics.FillEllipse(GrayBrush, New Rectangle(50, 30, 100, 100))
        objGraphics.FillEllipse(GrayBrush, New Rectangle(10, 100, 180, 300))
        objBitmap.Save(Response.OutputStream, ImageFormat.Gif)
        objBitmap.Dispose()
        objGraphics.Dispose()
    End Sub

    Private Function ManipulteString(ByVal str As String) As String
        If str.Length > 8 Then
            Return str.Substring(0, 8).ToUpper
        End If
        If str.Length = 8 Then
            Return str.ToUpper 
        End If
        If str.Length < 8 Then
            Dim legthStr As Integer = str.Length
            Dim sisa As Integer = 8 - legthStr
            If sisa > 1 Then
                sisa = Round(sisa / 2)
                Dim blankSpace As String
                For i As Integer = 0 To sisa - 1
                    blankSpace += "  "
                Next
                Return blankSpace & str.ToUpper & blankSpace
            Else
                Return str.ToUpper
            End If
        End If
    End Function

    Private Sub DrawImage(ByVal nID As Integer, ByVal desc As String)
        Dim objGraphics As Graphics
        Dim GrayBrush = New SolidBrush(Color.Gray)
        Dim BlackBrush = New SolidBrush(Color.Black)
        Dim WhiteBrush = New SolidBrush(Color.White)
        Dim drawBmp As Bitmap = New Bitmap(70, 100)
        Dim objPhisngGuard As PhisingGuardImage = New PhisingGuardImageFacade(User).Retrieve(nID)
        If Not IsNothing(objPhisngGuard) AndAlso Not IsNothing(objPhisngGuard.Image) Then
            Dim objStream As System.IO.Stream = New System.IO.MemoryStream
            objStream.Write(objPhisngGuard.Image, 0, objPhisngGuard.Image.Length)
            Dim objBitmap As Bitmap = New Bitmap(objStream)
            Try
                Response.ContentType = "Image/Jpeg"
                objGraphics = Graphics.FromImage(drawBmp)
                objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
                objGraphics.FillRectangle(WhiteBrush, 0, 0, 80, 100)
                objGraphics.DrawImage(objBitmap, 0, 0, 70, 80)
                objGraphics.DrawString(ManipulteString(desc), _
                                   New Font(FontFamily.GenericSerif, 10, FontStyle.Bold), _
                                   BlackBrush, 1, 85)
                ResizePhoto(drawBmp).Save(Response.OutputStream, ImageFormat.Jpeg)
            Finally
                objBitmap.Dispose()
                drawBmp.Dispose()
            End Try
        Else
            DrawBlank()
        End If
    End Sub

    Private Sub DrawPhoto(ByVal nID As Integer)
        Dim objGraphics As Graphics
        Dim GrayBrush = New SolidBrush(Color.Gray)
        Dim BlackBrush = New SolidBrush(Color.Black)
        Dim WhiteBrush = New SolidBrush(Color.White)
        Dim drawBmp As Bitmap = New Bitmap(70, 70)
        Dim objPhisngGuard As PhisingGuardImage = New PhisingGuardImageFacade(User).Retrieve(nID)
        If Not IsNothing(objPhisngGuard) AndAlso Not IsNothing(objPhisngGuard.Image) Then
            Dim objStream As System.IO.Stream = New System.IO.MemoryStream
            objStream.Write(objPhisngGuard.Image, 0, objPhisngGuard.Image.Length)
            Dim objBitmap As Bitmap = New Bitmap(objStream)
            Try
                Response.ContentType = "Image/Jpeg"
                objGraphics = Graphics.FromImage(drawBmp)
                objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
                objGraphics.FillRectangle(WhiteBrush, 0, 0, 70, 70)
                objGraphics.DrawImage(objBitmap, 0, 0, 70, 70)
                ResizePhoto(drawBmp).Save(Response.OutputStream, ImageFormat.Jpeg)
            Finally
                objBitmap.Dispose()
                drawBmp.Dispose()
            End Try
        Else
            DrawBlank()
        End If
    End Sub

    Private Function ResizePhoto(ByVal bmp As Bitmap) As Bitmap
        If (bmp.Width <= MAX_WIDTH) AndAlso (bmp.Height <= MAX_HEIGHT) Then
            Return bmp
        End If

        Dim w, h As Integer
        If bmp.Height > bmp.Width Then
            w = MAX_WIDTH * bmp.Width / bmp.Height
            h = MAX_HEIGHT * bmp.Height / bmp.Height
        Else
            w = MAX_WIDTH * bmp.Width / bmp.Width
            h = MAX_HEIGHT * bmp.Height / bmp.Width
        End If
        Return bmp.GetThumbnailImage(w, h, New Image.GetThumbnailImageAbort(AddressOf Abort), IntPtr.Zero)
    End Function

    Private Function Abort() As Boolean
        Return False
    End Function
#End Region

End Class
