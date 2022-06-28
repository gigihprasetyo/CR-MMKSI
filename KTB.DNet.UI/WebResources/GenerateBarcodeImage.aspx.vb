Imports System.IO

Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Drawing.Imaging

Public Class GenerateBarcodeImage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs)
        If Request.QueryString("d") IsNot Nothing Then
            'Read in the parameters
            Dim strData As String = Server.UrlDecode(Request.QueryString("d"))
            Dim imageHeight As Integer = Convert.ToInt32(Request.QueryString("h"))
            Dim imageWidth As Integer = Convert.ToInt32(Request.QueryString("w"))
            Dim Forecolor As String = "000000"
            Dim Backcolor As String = "FFFFFF"
            Dim bIncludeLabel As Boolean = "true"
            Dim strImageFormat As String = "PNG"

            Dim type As BarcodeLib.TYPE = BarcodeLib.TYPE.UNSPECIFIED
            type = BarcodeLib.TYPE.CODE128

            'switch
            Dim barcodeImage As System.Drawing.Image = Nothing
            Try
                Dim b As New BarcodeLib.Barcode()
                If type <> BarcodeLib.TYPE.UNSPECIFIED Then
                    b.IncludeLabel = bIncludeLabel
                    b.Alignment = BarcodeLib.AlignmentPositions.CENTER
                    'alignment

                    'switch


                    '===== Encoding performed here =====
                    barcodeImage = b.Encode(type, strData.Trim(), System.Drawing.ColorTranslator.FromHtml("#" & Forecolor), System.Drawing.ColorTranslator.FromHtml("#" & Backcolor), imageWidth, imageHeight)
                    '===================================

                    '===== Static Encoding performed here =====
                    'barcodeImage = BarcodeLib.Barcode.DoEncode(type, this.txtData.Text.Trim(), this.chkGenerateLabel.Checked, this.btnForeColor.BackColor, this.btnBackColor.BackColor);
                    '==========================================

                    Response.ContentType = "../images/" & strImageFormat
                    Dim MemStream As New System.IO.MemoryStream()
                    barcodeImage.Save(MemStream, ImageFormat.Png)

                    'switch
                    MemStream.WriteTo(Response.OutputStream)
                    'if
                End If
                'try
                'TODO: find a way to return this to display the encoding error message
            Catch ex As Exception
            Finally
                'catch
                If barcodeImage IsNot Nothing Then
                    'Clean up / Dispose...
                    barcodeImage.Dispose()
                End If
                'finally
            End Try
        End If
        'if
    End Sub
End Class

#Region "Mark"
'Protected Sub Page_Load(sender As Object, e As EventArgs)
'    If Request.QueryString("d") IsNot Nothing Then
'        'Read in the parameters
'        Dim strData As String = Request.QueryString("d")
'        Dim imageHeight As Integer = Convert.ToInt32(Request.QueryString("h"))
'        Dim imageWidth As Integer = Convert.ToInt32(Request.QueryString("w"))
'        Dim Forecolor As String = Request.QueryString("fc")
'        Dim Backcolor As String = Request.QueryString("bc")
'        Dim bIncludeLabel As Boolean = Request.QueryString("il").ToLower().Trim() = "true"
'        Dim strImageFormat As String = Request.QueryString("if").ToLower().Trim()
'        Dim strAlignment As String = Request.QueryString("align").ToLower().Trim()

'        Dim type As BarcodeLib.TYPE = BarcodeLib.TYPE.UNSPECIFIED
'        Select Case Request.QueryString("t").Trim()
'            Case "UPC-A"
'                type = BarcodeLib.TYPE.UPCA
'                Exit Select
'            Case "UPC-E"
'                type = BarcodeLib.TYPE.UPCE
'                Exit Select
'            Case "UPC 2 Digit Ext"
'                type = BarcodeLib.TYPE.UPC_SUPPLEMENTAL_2DIGIT
'                Exit Select
'            Case "UPC 5 Digit Ext"
'                type = BarcodeLib.TYPE.UPC_SUPPLEMENTAL_5DIGIT
'                Exit Select
'            Case "EAN-13"
'                type = BarcodeLib.TYPE.EAN13
'                Exit Select
'            Case "JAN-13"
'                type = BarcodeLib.TYPE.JAN13
'                Exit Select
'            Case "EAN-8"
'                type = BarcodeLib.TYPE.EAN8
'                Exit Select
'            Case "ITF-14"
'                type = BarcodeLib.TYPE.ITF14
'                Exit Select
'            Case "Codabar"
'                type = BarcodeLib.TYPE.Codabar
'                Exit Select
'            Case "PostNet"
'                type = BarcodeLib.TYPE.PostNet
'                Exit Select
'            Case "Bookland-ISBN"
'                type = BarcodeLib.TYPE.BOOKLAND
'                Exit Select
'            Case "Code 11"
'                type = BarcodeLib.TYPE.CODE11
'                Exit Select
'            Case "Code 39"
'                type = BarcodeLib.TYPE.CODE39
'                Exit Select
'            Case "Code 39 Extended"
'                type = BarcodeLib.TYPE.CODE39Extended
'                Exit Select
'            Case "Code 93"
'                type = BarcodeLib.TYPE.CODE93
'                Exit Select
'            Case "LOGMARS"
'                type = BarcodeLib.TYPE.LOGMARS
'                Exit Select
'            Case "MSI"
'                type = BarcodeLib.TYPE.MSI_Mod10
'                Exit Select
'            Case "Interleaved 2 of 5"
'                type = BarcodeLib.TYPE.Interleaved2of5
'                Exit Select
'            Case "Standard 2 of 5"
'                type = BarcodeLib.TYPE.Standard2of5
'                Exit Select
'            Case "Code 128"
'                type = BarcodeLib.TYPE.CODE128
'                Exit Select
'            Case "Code 128-A"
'                type = BarcodeLib.TYPE.CODE128A
'                Exit Select
'            Case "Code 128-B"
'                type = BarcodeLib.TYPE.CODE128B
'                Exit Select
'            Case "Code 128-C"
'                type = BarcodeLib.TYPE.CODE128C
'                Exit Select
'            Case "Telepen"
'                type = BarcodeLib.TYPE.TELEPEN
'                Exit Select
'            Case "FIM (Facing Identification Mark)"
'                type = BarcodeLib.TYPE.FIM
'                Exit Select
'            Case "Pharmacode"
'                type = BarcodeLib.TYPE.PHARMACODE
'                Exit Select
'            Case Else
'                Exit Select
'        End Select
'        'switch
'        Dim barcodeImage As System.Drawing.Image = Nothing
'        Try
'            Dim b As New BarcodeLib.Barcode()
'            If type <> BarcodeLib.TYPE.UNSPECIFIED Then
'                b.IncludeLabel = bIncludeLabel

'                'alignment
'                Select Case strAlignment.ToLower().Trim()
'                    Case "c"
'                        b.Alignment = BarcodeLib.AlignmentPositions.CENTER
'                        Exit Select
'                    Case "r"
'                        b.Alignment = BarcodeLib.AlignmentPositions.RIGHT
'                        Exit Select
'                    Case "l"
'                        b.Alignment = BarcodeLib.AlignmentPositions.LEFT
'                        Exit Select
'                    Case Else
'                        b.Alignment = BarcodeLib.AlignmentPositions.CENTER
'                        Exit Select
'                End Select
'                'switch
'                If Forecolor.Trim() = "" AndAlso Forecolor.Trim().Length <> 6 Then
'                    Forecolor = "000000"
'                End If
'                If Backcolor.Trim() = "" AndAlso Backcolor.Trim().Length <> 6 Then
'                    Backcolor = "FFFFFF"
'                End If

'                '===== Encoding performed here =====
'                barcodeImage = b.Encode(type, strData.Trim(), System.Drawing.ColorTranslator.FromHtml("#" & Forecolor), System.Drawing.ColorTranslator.FromHtml("#" & Backcolor), imageWidth, imageHeight)
'                '===================================

'                '===== Static Encoding performed here =====
'                'barcodeImage = BarcodeLib.Barcode.DoEncode(type, this.txtData.Text.Trim(), this.chkGenerateLabel.Checked, this.btnForeColor.BackColor, this.btnBackColor.BackColor);
'                '==========================================

'                Response.ContentType = "image/" & strImageFormat
'                Dim MemStream As New System.IO.MemoryStream()

'                Select Case strImageFormat
'                    Case "gif"
'                        barcodeImage.Save(MemStream, ImageFormat.Gif)
'                        Exit Select
'                    Case "jpeg"
'                        barcodeImage.Save(MemStream, ImageFormat.Jpeg)
'                        Exit Select
'                    Case "png"
'                        barcodeImage.Save(MemStream, ImageFormat.Png)
'                        Exit Select
'                    Case "bmp"
'                        barcodeImage.Save(MemStream, ImageFormat.Bmp)
'                        Exit Select
'                    Case "tiff"
'                        barcodeImage.Save(MemStream, ImageFormat.Tiff)
'                        Exit Select
'                    Case Else
'                        Exit Select
'                End Select
'                'switch
'                MemStream.WriteTo(Response.OutputStream)
'                'if
'            End If
'            'try
'            'TODO: find a way to return this to display the encoding error message
'        Catch ex As Exception
'        Finally
'            'catch
'            If barcodeImage IsNot Nothing Then
'                'Clean up / Dispose...
'                barcodeImage.Dispose()
'            End If
'            'finally
'        End Try
'    End If
'    'if
'End Sub
#End Region
