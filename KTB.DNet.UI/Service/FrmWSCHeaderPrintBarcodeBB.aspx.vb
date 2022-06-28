Imports KTB.DNET.Domain
Imports KTB.DNET.BusinessFacade
Imports KTB.DNET.BusinessFacade.FinishUnit
Imports KTB.DNET.BusinessFacade.Service
Imports KTB.DNET.Utility
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.Security
Imports System.Globalization

Public Class FrmWSCHeaderPrintBarcodeBB
    Inherits System.Web.UI.Page

#Region "Custom Variable"
    Dim sHelper As New SessionHelper
    Dim oDealer As New Dealer
    Dim dblTotalHarga As Double
    Dim TotalRowPerPages As Integer = 12
    Dim TotalPages As Double = 0
    Dim arrBarcode As ArrayList
    Dim strIdTmp As String()
    Dim strIdAwal As String = ""
    Dim strKodeBarcode As String = ""
    Dim strClaimNumber As String = ""
    Dim objParts As WSCDetailBB
    Dim startIndexRow As Integer = 0
    Dim index As Integer = 0
    Dim currentPage As Integer = 1
    Dim intViewStateMode As String
    Dim screenFrom As String
    Dim pqrId As String
    Dim wscId As String

#End Region

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        GetRequestString()
        Server.Transfer("../Service/FrmWSCHeaderBB.aspx?viewStateMode=" & intViewStateMode & "&screenFrom=" & screenFrom & "&PQRId=" & pqrId & "&WSCId=" & wscId)
    End Sub

    Private Sub GetRequestString()
        Try
            intViewStateMode = Request.QueryString("viewStateMode")
        Catch
        End Try
        Try
            screenFrom = Request.QueryString("screenFrom")
        Catch
        End Try
        Try
            pqrId = CType(Request.QueryString("PQRId").ToString, Integer)
        Catch
        End Try
        Try
            wscId = CType(Request.QueryString("WSCId").ToString, Integer)
        Catch
        End Try
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        GetRequestString()

        If Not IsPostBack Then
            btnPrint.Attributes("onclick") = "PrintDocument();"

            If Not IsNothing(Request.QueryString("id")) Then
                strIdTmp = Request.QueryString("id").Split(";")
                lblCurrentPage.Text = "1"

                If strIdTmp.Length > 0 Then
                    TotalPages = strIdTmp.Length / TotalRowPerPages
                    Dim number As String = TotalPages.ToString()
                    Dim output As Integer
                    If Not (Integer.TryParse(number, output)) Then
                        TotalPages = CType(TotalPages, Integer) + 1
                    End If
                    lblTotalPages.Text = TotalPages
                Else
                    lblTotalPages.Text = "1"
                End If
                If CType(lblTotalPages.Text, Integer) = 1 Then
                    lnkbtnNext.Visible = False
                    lnkbtnPrev.Visible = False
                ElseIf CType(lblTotalPages.Text, Integer) > 1 Then
                    lnkbtnPrev.Visible = True
                    lnkbtnNext.Visible = True
                Else
                    lnkbtnNext.Visible = False
                    lnkbtnPrev.Visible = False
                End If

                arrBarcode = New ArrayList
                startIndexRow = 0
                RetriveArrayBarcode(startIndexRow, arrBarcode)

                If startIndexRow = 0 Then
                    lnkbtnPrev.Visible = False
                ElseIf strIdTmp.Length >= 12 Then
                    lnkbtnNext.Visible = False
                End If

                ' set data
                If arrBarcode.Count > 0 Then
                    MapToScreen(arrBarcode)
                End If
            Else
                Return
            End If
        End If
    End Sub

    Private Sub MapToScreen(ByVal arrBarcode As ArrayList)
        Dim cultureInfo As New CultureInfo("id-ID")
        btnPrint.Attributes("onclick") = "window.print();"

        BarcodeImage1.Attributes("style") = "display:none"
        BarcodeImage2.Attributes("style") = "display:none"
        BarcodeImage3.Attributes("style") = "display:none"
        BarcodeImage4.Attributes("style") = "display:none"
        BarcodeImage5.Attributes("style") = "display:none"
        BarcodeImage6.Attributes("style") = "display:none"
        BarcodeImage7.Attributes("style") = "display:none"
        BarcodeImage8.Attributes("style") = "display:none"
        BarcodeImage9.Attributes("style") = "display:none"
        BarcodeImage10.Attributes("style") = "display:none"
        BarcodeImage11.Attributes("style") = "display:none"
        BarcodeImage12.Attributes("style") = "display:none"

        BarcodeImage1.ImageUrl = ""
        BarcodeImage2.ImageUrl = ""
        BarcodeImage3.ImageUrl = ""
        BarcodeImage4.ImageUrl = ""
        BarcodeImage5.ImageUrl = ""
        BarcodeImage6.ImageUrl = ""
        BarcodeImage7.ImageUrl = ""
        BarcodeImage8.ImageUrl = ""
        BarcodeImage9.ImageUrl = ""
        BarcodeImage10.ImageUrl = ""
        BarcodeImage11.ImageUrl = ""
        BarcodeImage12.ImageUrl = ""

        Dim indexBarcodeImage As Integer = 1
        For Each KodeBarcode As String In arrBarcode
            Select Case indexBarcodeImage
                Case 1
                    BarcodeImage1.ImageUrl = CreateBarcode(KodeBarcode)
                    BarcodeImage1.Attributes("style") = "display:table-row"
                Case 2
                    BarcodeImage2.ImageUrl = CreateBarcode(KodeBarcode)
                    BarcodeImage2.Attributes("style") = "display:table-row"
                Case 3
                    BarcodeImage3.ImageUrl = CreateBarcode(KodeBarcode)
                    BarcodeImage3.Attributes("style") = "display:table-row"
                Case 4
                    BarcodeImage4.ImageUrl = CreateBarcode(KodeBarcode)
                    BarcodeImage4.Attributes("style") = "display:table-row"
                Case 5
                    BarcodeImage5.ImageUrl = CreateBarcode(KodeBarcode)
                    BarcodeImage5.Attributes("style") = "display:table-row"
                Case 6
                    BarcodeImage6.ImageUrl = CreateBarcode(KodeBarcode)
                    BarcodeImage6.Attributes("style") = "display:table-row"
                Case 7
                    BarcodeImage7.ImageUrl = CreateBarcode(KodeBarcode)
                    BarcodeImage7.Attributes("style") = "display:table-row"
                Case 8
                    BarcodeImage8.ImageUrl = CreateBarcode(KodeBarcode)
                    BarcodeImage8.Attributes("style") = "display:table-row"
                Case 9
                    BarcodeImage9.ImageUrl = CreateBarcode(KodeBarcode)
                    BarcodeImage9.Attributes("style") = "display:table-row"
                Case 10
                    BarcodeImage10.ImageUrl = CreateBarcode(KodeBarcode)
                    BarcodeImage10.Attributes("style") = "display:table-row"
                Case 11
                    BarcodeImage11.ImageUrl = CreateBarcode(KodeBarcode)
                    BarcodeImage11.Attributes("style") = "display:table-row"
                Case 12
                    BarcodeImage12.ImageUrl = CreateBarcode(KodeBarcode)
                    BarcodeImage12.Attributes("style") = "display:table-row"
                Case Else
            End Select

            indexBarcodeImage += 1
        Next
    End Sub

    Private Function CreateBarcode(ByVal KodeBarcode As String) As String
        Dim strImageURL As String = "../WebResources/GenerateBarcodeImage.aspx?" & _
            "d=" & Server.UrlEncode(KodeBarcode) & "&h=150" & "&w=300"

        Return strImageURL
    End Function

    Private Function RetriveArrayBarcode(ByVal startIndexRow As Integer, ByRef _arrBarcode As ArrayList)
        oDealer = CType(sHelper.GetSession("DEALER"), Dealer)
        Try
            strClaimNumber = Request.QueryString("claimNumber")
        Catch
        End Try
        Try
            strIdTmp = Request.QueryString("id").Split(";")
        Catch
        End Try

        For index = startIndexRow To strIdTmp.Length - 1
            strIdAwal = strIdTmp(index)
            If strIdAwal <> "" Then
                objParts = New WSCDetailBBFacade(User).Retrieve(CType(strIdAwal, Integer))
                strKodeBarcode = oDealer.DealerCode & strClaimNumber & Replace(objParts.SparePartMaster.PartNumber, "-", " ")
                arrBarcode.Add(strKodeBarcode)
            End If
        Next
    End Function

    Private Sub lnkbtnNext_Click(sender As Object, e As EventArgs) Handles lnkbtnNext.Click
        index = 0
        Dim TotalPages As Integer = CType(lblTotalPages.Text, Integer)
        currentPage = lblCurrentPage.Text

        arrBarcode = New ArrayList
        startIndexRow = (currentPage * TotalRowPerPages)
        RetriveArrayBarcode(startIndexRow, arrBarcode)

        lblCurrentPage.Text = currentPage + 1
        currentPage = lblCurrentPage.Text
        Dim TotalRowPages As Integer = (currentPage * TotalRowPerPages)
        If startIndexRow > 0 Then
            lnkbtnPrev.Visible = True
        End If
        If startIndexRow = ((TotalPages * TotalRowPerPages) - TotalRowPerPages) Then
            lnkbtnNext.Visible = False
        Else
            lnkbtnNext.Visible = True
        End If

        ' set data
        If arrBarcode.Count > 0 Then
            MapToScreen(arrBarcode)
        End If
    End Sub

    Private Sub lnkbtnPrev_Click(sender As Object, e As EventArgs) Handles lnkbtnPrev.Click
        Dim startIndexRow As Integer
        Dim currentPage As Integer = lblCurrentPage.Text
        Dim indexPrevPage As Integer = currentPage - 1

        Dim TotalRowPages As Integer
        Dim TotalRowPrevPages As Integer
        Dim TotalRowPrev As Integer = (TotalRowPerPages * 2)
        Dim TotalPages As Integer = CType(lblTotalPages.Text, Integer)

        TotalRowPages = currentPage * TotalRowPerPages
        TotalRowPrevPages = indexPrevPage * TotalRowPerPages

        index = 0
        arrBarcode = New ArrayList
        startIndexRow = TotalRowPages - TotalRowPrev
        RetriveArrayBarcode(startIndexRow, arrBarcode)

        lblCurrentPage.Text = currentPage - 1
        If startIndexRow <= 0 Then
            lnkbtnPrev.Visible = False
        Else
            lnkbtnPrev.Visible = True
        End If
        If startIndexRow = ((TotalPages * TotalRowPerPages) - TotalRowPerPages) Then
            lnkbtnNext.Visible = False
        Else
            lnkbtnNext.Visible = True
        End If

        ' set data
        If arrBarcode.Count > 0 Then
            MapToScreen(arrBarcode)
        End If
    End Sub
End Class
