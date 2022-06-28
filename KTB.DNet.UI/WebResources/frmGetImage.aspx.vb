#Region "Summary"
'---------------------------------------------------'
'-- Program Code : frmGetImage.aspx               --'
'-- Program Name : Get Image                      --'
'-- Description  :                                --'
'---------------------------------------------------'
'-- Programmer   : Agus Pirnadi                   --'
'-- Start Date   : Jan 17 2007                    --'
'-- Update By    :                                --'
'-- Last Update  :                                --'
'---------------------------------------------------'
'-- Copyright © 2007 by Intimedia                 --'
'---------------------------------------------------'
#End Region

#Region "System Namespace"
Imports System
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
#End Region

Public Class frmGetImage
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
    Private oPhisingGuardImage As PhisingGuardImage = New PhisingGuardImage
    Private oPhisingGuardImageFacade As PhisingGuardImageFacade = New PhisingGuardImageFacade(User)
#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim id As Integer = CType(Request("id"), Integer)
        DrawImage(id)
    End Sub

#End Region

#Region "Custom Method"

    Private Sub DrawImage(ByVal nID As Integer)
        oPhisingGuardImage = oPhisingGuardImageFacade.Retrieve(nID)
        If Not IsNothing(oPhisingGuardImage) AndAlso Not IsNothing(oPhisingGuardImage.Image) Then
            Dim objStream As System.IO.Stream = New System.IO.MemoryStream
            objStream.Write(oPhisingGuardImage.Image, 0, oPhisingGuardImage.Image.Length)
            Dim objBitmap As Bitmap = New Bitmap(objStream)
            Try
                Response.ContentType = "Image/Jpeg"
                ResizePhoto(objBitmap).Save(Response.OutputStream, ImageFormat.Jpeg)
            Finally
                objBitmap.Dispose()
            End Try
        End If
    End Sub

    Private Function ResizePhoto(ByVal bmp As Bitmap) As Bitmap
        If (bmp.Width <= MAX_WIDTH) AndAlso (bmp.Height <= MAX_HEIGHT) Then
            Return bmp
        End If

        Dim w, h As Integer
        If bmp.Height > bmp.Width Then
            w = 70 * bmp.Width / bmp.Height
            h = 70 * bmp.Height / bmp.Height
        Else
            w = 70 * bmp.Width / bmp.Width
            h = 70 * bmp.Height / bmp.Width
        End If
        Return bmp.GetThumbnailImage(w, h, New Image.GetThumbnailImageAbort(AddressOf Abort), IntPtr.Zero)
    End Function

    Private Function Abort() As Boolean
        Return False
    End Function
#End Region

End Class
