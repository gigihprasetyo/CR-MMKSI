Imports System
Imports System.Web
Imports System.Drawing.Imaging
Imports System.Drawing
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI

Public Class GetPhoto
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

    Private Const MAX_WIDTH As Integer = 200
    Private Const MAX_HEIGHT As Integer = 200
    Private sHTrainee As SessionHelper = New SessionHelper

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim allow As Boolean = False
        If Not IsNothing(sHTrainee.GetSession("DEALER")) Then
            allow = True
        End If

        Try
            Dim Parm As Integer = Integer.Parse(Request("id").ToString)
            Dim ParmTipe As String = ""
            If Not Request("Type") Is Nothing Then
                ParmTipe = Request("Type").ToString
            End If

            If ParmTipe = "1" Then
                DrawBlank()
            Else
                If allow Then
                    DrawPhoto(Parm)
                Else
                    DrawBlank()
                End If
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

        objGraphics.FillRectangle(WhiteBrush, 0, 0, 200, 200)
        objGraphics.DrawRectangle(New Pen(Color.Black, 2), 0, 0, 200, 200)
        objGraphics.FillEllipse(GrayBrush, New Rectangle(50, 30, 100, 100))
        objGraphics.FillEllipse(GrayBrush, New Rectangle(10, 100, 180, 300))
        objGraphics.DrawString("Gambarku", _
        New Font(FontFamily.GenericSansSerif, 16, FontStyle.Bold), _
        BlackBrush, _
        80, _
        10)

        objBitmap.Save(Response.OutputStream, ImageFormat.Gif)

        objBitmap.Dispose()
        objGraphics.Dispose()
    End Sub

    Private Sub DrawPhoto(ByVal nID As Integer)
        Dim objTrainee As TrTrainee = New TrTraineeFacade(User).Retrieve(nID)
        If Not IsNothing(objTrainee) AndAlso Not IsNothing(objTrainee.Photo) Then
            Dim objStream As System.IO.Stream = New System.IO.MemoryStream
            objStream.Write(objTrainee.Photo, 0, objTrainee.Photo.Length)
            Dim objBitmap As Bitmap = New Bitmap(objStream)
            Try
                Response.ContentType = "Image/Jpeg"
                ResizePhoto(objBitmap).Save(Response.OutputStream, ImageFormat.Jpeg)
            Finally
                objBitmap.Dispose()
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
            w = 200 * bmp.Width / bmp.Height
            h = 200 * bmp.Height / bmp.Height
        Else
            w = 200 * bmp.Width / bmp.Width
            h = 200 * bmp.Height / bmp.Width
        End If
        Return bmp.GetThumbnailImage(w, h, New Image.GetThumbnailImageAbort(AddressOf Abort), IntPtr.Zero)
    End Function

    Private Function Abort() As Boolean
        Return False
    End Function
#End Region

End Class
