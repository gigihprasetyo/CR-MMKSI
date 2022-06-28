 

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
Public Class GetPCImage
    Inherits System.Web.UI.Page
    Private Const MAX_WIDTH As Integer = 70
    Private Const MAX_HEIGHT As Integer = 70
    Private sHelper As SessionHelper = New SessionHelper
    Private cooValue As String
    Private clientCookies As String
    Private CookieName = KTB.DNet.Lib.WebConfig.GetValue("DNETPhisingGuard")

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

    Private Function RetrievePCPhisingGuard(ByVal CookiesValue As String) As PCPhisingGuard
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PCPhisingGuard), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PCPhisingGuard), "CookiesValue", MatchType.Exact, CookiesValue))
        Dim ArlPCPhisingGuard As ArrayList = New PCPhisingGuardFacade(Nothing).Retrieve(criterias)
        If ArlPCPhisingGuard.Count > 0 Then
            Return CType(ArlPCPhisingGuard(0), PCPhisingGuard)
        Else
            Return Nothing
        End If
    End Function

    Private Function RetrieveImage() As PCPhisingGuard
        Dim loop1 As Integer
        Dim arr1() As String
        Dim MyCookieColl As HttpCookieCollection
        Dim MyCookie As HttpCookie

        MyCookieColl = Request.Cookies
        arr1 = MyCookieColl.AllKeys
        ' Grab individual cookie objects by cookie name     
        For loop1 = 0 To arr1.GetUpperBound(0)
            MyCookie = MyCookieColl(arr1(loop1))
            If MyCookie.Name = CookieName Then
                clientCookies = MyCookie.Value.ToString
            End If
        Next
        Dim objPCPHisingGuard As PCPhisingGuard = RetrievePCPhisingGuard(clientCookies)
        Return objPCPHisingGuard
    End Function

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim objPC As PCPhisingGuard = RetrieveImage()
        Try
            If Not objPC Is Nothing Then
                DrawImage(objPC.Image, objPC.Description)
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
        Dim bgCol As Color = Color.FromArgb(204, 204, 204)
        Dim BGBrush = New SolidBrush(bgCol)
        objBitmap = New Bitmap(200, 200)
        objGraphics = Graphics.FromImage(objBitmap)
        Dim minute As String = Now.Minute
        If minute.Length = 1 Then
            minute = "0" & minute
        End If
        objGraphics.FillRectangle(BGBrush, 0, 0, 200, 200)
        objGraphics.DrawString(Now.Date.ToString("dd MMM").ToUpper, New Font(FontFamily.GenericSansSerif, 32, FontStyle.Bold), BlackBrush, 23, 50)
        objGraphics.DrawString(Now.Hour & " : " & minute & " WIB", New Font(FontFamily.GenericSansSerif, 22, FontStyle.Regular), BlackBrush, 18, 132)
        objBitmap.Save(Response.OutputStream, ImageFormat.Jpeg)
        objBitmap.Dispose()
        objGraphics.Dispose()
    End Sub

    Private Function ManipulteString(ByVal str As String) As String
        If str.Length > 8 Then
            Return str.Substring(0, 8).ToUpper.Trim
        End If
        If str.Length = 8 Then
            Return str.ToUpper.Trim
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

    Private Sub DrawImage(ByVal bites As Byte(), ByVal desc As String)
        Dim objGraphics As Graphics
        Dim GrayBrush = New SolidBrush(Color.Gray)
        Dim BlackBrush = New SolidBrush(Color.Black)
        Dim WhiteBrush = New SolidBrush(Color.White)
        Dim drawBmp As Bitmap = New Bitmap(70, 100)
        Dim objStream As System.IO.Stream = New System.IO.MemoryStream
        objStream.Write(bites, 0, bites.Length)
        Dim objBitmap As Bitmap = New Bitmap(objStream)
        Try
            Response.ContentType = "Image/Jpeg"
            objGraphics = Graphics.FromImage(drawBmp)
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

