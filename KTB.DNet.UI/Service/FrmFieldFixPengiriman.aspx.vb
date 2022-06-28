Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.Helper

Imports System
Imports System.Net
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Data
Imports System.Linq
Imports System.Security
Imports System.Collections.Generic
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Excel
Imports System.Reflection

Imports PdfSharp
Imports PdfSharp.Pdf
Imports PdfSharp.Pdf.IO
Imports PdfSharp.Drawing
Imports SpireDoc = Spire.Doc
Imports Document = Spire.Doc.Document
Imports SpireDoc.Documents
Imports OfficeOpenXml
Imports Spire.Doc.Fields
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging

Public Class FrmFieldFixPengiriman
    Inherits System.Web.UI.Page

    Private sessionHelper As New SessionHelper
    Private objDealer As Dealer
    Private sesHelper As New SessionHelper
    Private TargetDirectory As String
    Dim arrChkSelectionID As ArrayList = New ArrayList

    Private filePdf As String = String.Empty

    Private arlDiscountProposalDtlApprovalToSPL As ArrayList = New ArrayList


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objDealer = Session("DEALER")

        If Not IsPostBack Then

            ViewState("CurrentSortColumn") = "RequestDate"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

            sesHelper.SetSession("ArrForecastPart", arrChkSelectionID)

            ClearAll()
            bindGrid(0)
        End If
    End Sub

    Private Sub bindGrid(ByVal currentPageIndex As Integer)
        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SparePartForecastDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(criterias)
        Dim totalRow As Integer = 0
        Dim arrSPFCO As ArrayList = New SparePartForecastDetailFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dtgSentPart.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        If arrSPFCO.Count = 0 Then
            dtgSentPart.DataSource = New ArrayList
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Field Fix"))
            End If
        Else
            dtgSentPart.DataSource = arrSPFCO
        End If

        dtgSentPart.VirtualItemCount = totalRow
        dtgSentPart.DataBind()
        sesHelper.SetSession("ArrForecastPart", New ArrayList)
    End Sub

    Private Sub CreateCriteria(ByRef criterias As CriteriaComposite)
        If txtKdDealer.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "SparePartForecastHeader.Dealer.DealerCode", MatchType.InSet, "('" & txtKdDealer.Text.Trim().Replace(";", "','") & "')"))
        End If
        If txtNoPO.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "SparePartForecastHeader.PoNumber", MatchType.Partial, txtNoPO.Text.Trim()))
        End If
        If txtPartNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "SparePartForecastMaster.SparePartMaster.PartNumber", MatchType.Exact, txtPartNo.Text.Trim()))
        End If
        If chkTglReq.Checked Then
            Dim StartProses As New DateTime(CInt(icStartDateReq.Value.Year), CInt(icStartDateReq.Value.Month), CInt(icStartDateReq.Value.Day), 0, 0, 0)
            Dim EndProses As New DateTime(CInt(icEndDateReq.Value.Year), CInt(icEndDateReq.Value.Month), CInt(icEndDateReq.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "RequestDate", MatchType.GreaterOrEqual, Format(StartProses, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "RequestDate", MatchType.LesserOrEqual, Format(EndProses, "yyyy-MM-dd HH:mm:ss")))
        End If

        Dim SelectedValue As Integer = EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Proses
        criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "Status", MatchType.Exact, SelectedValue))

        sesHelper.SetSession("CRITERIASspfDetail", criterias)

    End Sub

    Private Sub ClearAll()
        ViewState("EditID") = Nothing
        txtKdDealer.Text = ""
        txtPartNo.Text = ""
        icStartDateReq.Value = Date.Now.Date
        icEndDateReq.Value = Date.Now.Date
        'btnPrintPDF.Enabled = True
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If chkTglReq.Checked = False Then
            MessageBox.Show("Tanggal permintaan harus dipilih")
            Return
        Else
            bindGrid(0)
            btnPrintPDF.Enabled = False
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearAll()
        dtgSentPart.DataSource = New ArrayList
        dtgSentPart.DataBind()
    End Sub

    Private Sub dtgSentPart_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgSentPart.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            arrChkSelectionID = CType(sesHelper.GetSession("ArrForecastPart"), ArrayList)
            Dim rowValue As SparePartForecastDetail = CType(e.Item.DataItem, SparePartForecastDetail)
            CType(e.Item.FindControl("lblNo"), Label).Text = (dtgSentPart.CurrentPageIndex * dtgSentPart.PageSize + e.Item.ItemIndex + 1).ToString()
            CType(e.Item.FindControl("lblID"), Label).Text = rowValue.ID
            CType(e.Item.FindControl("lbliPoNo"), Label).Text = rowValue.SparePartForecastHeader.PoNumber
            CType(e.Item.FindControl("lbliKdDealer"), Label).Text = rowValue.SparePartForecastHeader.Dealer.DealerCode
            CType(e.Item.FindControl("lbliPartNo"), Label).Text = rowValue.SparePartForecastMaster.SparePartMaster.PartNumber
            CType(e.Item.FindControl("lbliPartName"), Label).Text = rowValue.SparePartForecastMaster.SparePartMaster.PartName
            CType(e.Item.FindControl("lbliNoBulletin"), Label).Text = rowValue.SparePartForecastMaster.NoBulletinService
            CType(e.Item.FindControl("lbliRequestDate"), Label).Text = rowValue.RequestDate.ToString("dd/MM/yyyy")
            CType(e.Item.FindControl("lbliRequestQty"), Label).Text = rowValue.RequestQty.ToString("N0")
            CType(e.Item.FindControl("lbliApprovedQty"), Label).Text = rowValue.ApprovedQty.ToString("N0")
            CType(e.Item.FindControl("lbliStatus"), Label).Text = EnumFieldFixPartOrderStatus.GetStringValue(rowValue.Status)

        End If
    End Sub

    Protected Sub chkSelection_CheckedChanged(sender As Object, e As EventArgs)
        Dim chk As CheckBox = CType(sender, CheckBox)
        Dim dgItem As DataGridItem = CType(chk.NamingContainer, DataGridItem)
        Dim lblID As Label = CType(dgItem.FindControl("lblID"), Label)
        Dim strID As String = lblID.Text

        arrChkSelectionID = CType(sesHelper.GetSession("ArrForecastPart"), ArrayList)
        If chk.Checked = True Then
            arrChkSelectionID.Add(strID)
        Else
            arrChkSelectionID.Remove(strID)
        End If
        sesHelper.SetSession("ArrForecastPart", arrChkSelectionID)

        If arrChkSelectionID.Count > 0 Then
            btnPrintPDF.Enabled = True
        Else
            btnPrintPDF.Enabled = False
        End If
    End Sub

    Private Sub btnPrintPDF_Click(sender As Object, e As EventArgs) Handles btnPrintPDF.Click
        lblLoading.Text = "Proses download. Mohon tunggu sebentar..."
        btnPrintPDF.Enabled = False
        SetPrint()
        Try
            Dim fInfo As FileInfo = New FileInfo(filePdf)
            Response.Redirect("../Download.aspx?file=" & fInfo.FullName, False)
        Catch ex As Exception
            Throw ex
        End Try

        MessageBox.Show("Add pdf berhasil")
        lblLoading.Text = ""
        bindGrid(0)
        btnPrintPDF.Enabled = False
    End Sub


    Private Sub SetPrint()
        Dim spfHeader As SparePartForecastHeader
        Dim arrDataH As ArrayList = New ArrayList
        Dim arrDataD As ArrayList
        Dim strSPFH_id As String = String.Empty
        Dim strCH_id As String = String.Empty
        If dtgSentPart.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di Print")
            Return
        Else
            arrChkSelectionID = CType(sesHelper.GetSession("ArrForecastPart"), ArrayList)
            For Each strIDNew As String In arrChkSelectionID
                If strCH_id = "" Then
                    strCH_id = strIDNew
                Else
                    strCH_id = strCH_id & ";" & strIDNew
                End If
            Next
        End If
        'collect data detail 

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartForecastDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "ID", MatchType.InSet, CommonFunction.GetStrValue(strCH_id, ";", ",")))
        arrDataD = New SparePartForecastDetailFacade(User).Retrieve(criterias)
        If arrDataD.Count > 0 Then
            Dim bCheck As Boolean = True
            For Each dt As SparePartForecastDetail In arrDataD
                If arrDataH.Count <> 0 Then
                    For i As Integer = 0 To arrDataH.Count - 1
                        If arrDataH(i) <> dt.SparePartForecastHeader.Dealer.ID.ToString Then
                            bCheck = True
                        Else
                            bCheck = False
                            Exit For
                        End If
                    Next
                    If bCheck Then
                        arrDataH.Add(dt.SparePartForecastHeader.Dealer.ID)
                    End If
                Else
                    arrDataH.Add(dt.SparePartForecastHeader.Dealer.ID)
                End If

                'If x <> dt.SparePartForecastHeader.Dealer.ID.ToString Then
                '    arrDataH.Add(dt.SparePartForecastHeader.Dealer.ID)
                '    x = dt.SparePartForecastHeader.Dealer.ID
                'End If
            Next

            Dim strFileName As String, strDestFile As String
            strFileName = ""
            strDestFile = ""
            Dim strResult As String = GeneratePDFtoGroupware(arrDataH, arrDataD, strFileName, strDestFile)

            If strResult.Trim <> "" Then
                filePdf = strResult
                bindGrid(0)
            End If
        End If
    End Sub

    Private Function GeneratePDFtoGroupware(ByVal arrDataH As ArrayList, ByVal arrDataD As ArrayList, ByRef strFileName As String, ByRef strDestFile As String) As String
        Dim result As String = String.Empty
        Try
            Dim dtHeader = arrDataH
            Dim dataD = arrDataD
            Dim newLine As String = Environment.NewLine

            Dim filePath As String = Server.MapPath("~\DataFile\Recall\NewDaftar_Pengiriman_SparepartForecast_Template.docx")
            Dim directoryTemp As String = Server.MapPath("~\DataFile\Recall\")
            Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(directoryTemp)
            If Not directoryInfo.Exists Then
                directoryInfo.Create()
            End If
            Dim finfo As FileInfo = New FileInfo(filePath)
            If Not finfo.Exists Then
                result = "File template Forecast Detail tidak ditemukan"
                Return result
            End If

            Dim filebytes As Byte() = File.ReadAllBytes(filePath)
            'add data header

            'Using Stream As MemoryStream = New MemoryStream()
            'Stream.Write(filebytes, 0, filebytes.Length)
            'Dim bytes As Byte() = Stream.ToArray()
            'Dim tempPath As String = Server.MapPath("~\DataTemp\PDF\") + Guid.NewGuid().ToString().Substring(0, 5) + ".docx"
            Dim tempPath As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") + "DataTemp\SparepartsForecastDetail\" + Guid.NewGuid().ToString().Substring(0, 5) + ".docx"
            strFileName = "SPForecast_" & Now.Year.ToString & Now.Month.ToString("d2") & Now.Day.ToString("d2") & "_" & Now.Hour.ToString("d2") & Now.Minute.ToString("d2") & Now.Second.ToString("d2") & ".pdf"
            UploadDocXFile(filebytes, tempPath)
            Dim tempPath2 As String = ""
            CreateTableinWord(tempPath, tempPath2, dtHeader, arrDataD)
            DownloadPDFFile(tempPath2, strFileName, strDestFile)
            'End Using
            result = strDestFile


        Catch ex As Exception
            result = ex.Message
        End Try
        Return result
    End Function

    Private Sub UploadDocXFile(ByVal bytes As Byte(), ByVal tempPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim success As Boolean = False
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            success = imp.Start()
            If imp.Start() Then
                If Not System.IO.Directory.Exists(tempPath) Then
                    System.IO.Directory.CreateDirectory(Path.GetDirectoryName(tempPath))
                End If

                If System.IO.File.Exists(tempPath) Then
                    System.IO.File.Delete(Path.GetDirectoryName(tempPath))
                End If

                Try
                    Dim wFile As System.IO.FileStream
                    wFile = New FileStream(tempPath, FileMode.Append)
                    wFile.Write(bytes, 0, bytes.Length)
                    wFile.Close()
                Catch ex As IOException
                    MsgBox(ex.ToString)
                End Try

                imp.StopImpersonate()
                imp = Nothing
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CreateTableinWord(ByVal tempPath As String, ByRef tempPath2 As String, ByVal hdArray As ArrayList, ByVal dtArray As ArrayList)
        'Load Document       

        Dim fNm As String = String.Empty
        Dim lstNm As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") + "DataTemp\SparepartsForecastDetail\" + Guid.NewGuid().ToString().Substring(0, 5) + ".docx"
        For rx As Integer = 0 To hdArray.Count - 1

            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(hdArray(rx))
            DataDealerHead(tempPath, objDealer)

            Dim doc As New Document()
            Dim filePath As String = tempPath
            doc.LoadFromFile(filePath)

            'Set Margins
            doc.Sections(0).PageSetup.Margins.Top = 17.9F
            doc.Sections(0).PageSetup.Margins.Bottom = 17.9F

            Dim dt As Integer = hdArray(rx)
            CreateTableData_SparepartForecast(doc, 0, dt, dtArray)
            tempPath2 = KTB.DNet.Lib.WebConfig.GetValue("SAN") + "DataTemp\SparepartsForecastDetail\" + Guid.NewGuid().ToString().Substring(0, 5) + ".docx"
            doc.SaveToFile(tempPath2, Spire.Doc.FileFormat.Docx)

            If rx = 0 Then
                fNm = tempPath2
            Else
                doc.LoadFromFile(fNm)
                doc.InsertTextFromFile(tempPath2, Spire.Doc.FileFormat.Docx)
                doc.SaveToFile(fNm, Spire.Doc.FileFormat.Docx)
            End If
        Next
        tempPath2 = fNm
    End Sub

    Function CreateTableData_SparepartForecast(ByVal doc As Document, ByVal j As Integer, ByVal arlObjHeader As Integer, ByVal arlObj As ArrayList)
        Dim section As SpireDoc.Section = doc.Sections(j)
        Dim selection As SpireDoc.Documents.TextSelection = doc.FindString("gridData_SparepartForecast", True, True)
        Dim range As TextRange = selection.GetAsOneRange()
        Dim paragraph As SpireDoc.Documents.Paragraph = range.OwnerParagraph
        Dim body As SpireDoc.Body = paragraph.OwnerTextBody
        Dim index As Integer = body.ChildObjects.IndexOf(paragraph)

        Dim cntTable As Integer = 1

        Dim table(cntTable) As SpireDoc.Table
        Dim objDealer As Dealer = New DealerFacade(User).Retrieve(arlObjHeader)
        Dim idx As Integer = 0

        Dim Header() As String = {"Nomor PO", "Nomor Part", "Nama Part", "No Service Bulletin", "Jumlah"}

        table(idx + 1) = section.AddTable(True)
        table(idx + 1).ResetCells(arlObj.Count + 1, Header.Length)
        table(idx + 1).TableFormat.WrapTextAround = True
        table(idx + 1).TableFormat.Positioning.HorizPositionAbs = SpireDoc.HorizontalPosition.Outside
        table(idx + 1).TableFormat.Positioning.VertRelationTo = SpireDoc.VerticalRelation.Margin
        table(idx + 1).TableFormat.Positioning.VertPosition = 10

        Dim width As SpireDoc.PreferredWidth = New SpireDoc.PreferredWidth(SpireDoc.WidthType.Percentage, 100)
        table(idx + 1).PreferredWidth = width
        'Header Row
        Dim FRow As SpireDoc.TableRow = table(idx + 1).Rows(0)
        FRow.IsHeader = True
        'Row Height
        FRow.Height = 15
        'Header Format
        FRow.RowFormat.BackColor = Color.PaleTurquoise
        For i As Integer = 0 To Header.Length - 1
            'Cell Alignment
            Dim p As SpireDoc.Documents.Paragraph = FRow.Cells(i).AddParagraph()
            FRow.Cells(i).CellFormat.VerticalAlignment = SpireDoc.Documents.VerticalAlignment.Middle
            p.Format.HorizontalAlignment = SpireDoc.Documents.HorizontalAlignment.Center
            'Data Format
            Dim TR As TextRange = p.AppendText(Header(i))
            TR.CharacterFormat.FontName = "mmsi"
            TR.CharacterFormat.FontSize = 9
            TR.CharacterFormat.TextColor = Color.Black
            TR.CharacterFormat.Bold = True
            If i = 0 Then
                FRow.Cells(i).Width = 50
            End If
            If i = 1 Then
                FRow.Cells(i).Width = 30
            End If
            If i = 2 Or i = 3 Then
                FRow.Cells(i).Width = 50
            End If
            If i = Header.Length - 1 Then
                FRow.Cells(i).Width = 20
            End If
        Next i

        'Data Row
        Dim y As Integer = 0
        For r As Integer = 0 To arlObj.Count - 1
            Dim objDetail As SparePartForecastDetail = CType(arlObj(r), SparePartForecastDetail)
            If objDealer.ID = objDetail.SparePartForecastHeader.Dealer.ID Then
                y = y + 1
                'Row Height
                Dim DataRows As SpireDoc.TableRow = table(idx + 1).Rows(y)
                DataRows.Height = 15
                Dim cols(Header.Length) As String
                cols(0) = objDetail.SparePartForecastHeader.PoNumber
                cols(1) = objDetail.SparePartForecastMaster.SparePartMaster.PartNumber
                cols(2) = objDetail.SparePartForecastMaster.SparePartMaster.PartName
                cols(3) = objDetail.SparePartForecastMaster.NoBulletinService
                cols(4) = objDetail.ApprovedQty

                'C Represents Column.
                For c As Integer = 0 To Header.Length - 1
                    'Cell Alignment
                    DataRows.Cells(c).CellFormat.VerticalAlignment = SpireDoc.Documents.VerticalAlignment.Middle
                    'Fill Data in Rows
                    Dim p2 As SpireDoc.Documents.Paragraph = DataRows.Cells(c).AddParagraph()
                    Dim TR2 As TextRange = p2.AppendText(cols(c))
                    'Format Cells
                    p2.Format.HorizontalAlignment = SpireDoc.Documents.HorizontalAlignment.Center
                    TR2.CharacterFormat.FontName = "mmsi"
                    TR2.CharacterFormat.FontSize = 9
                    TR2.CharacterFormat.TextColor = Color.Black
                Next c
            End If
        Next r
        If y <> arlObj.Count Then
            For i As Integer = table(idx + 1).Rows.Count - 1 To y + 1 Step -1
                table(idx + 1).Rows.RemoveAt(i)
            Next
        End If

        Dim cTbl As Integer = cntTable - 1
        body.ChildObjects.Remove(paragraph)
        body.ChildObjects.Insert(index, table(1))
        'For zz As Integer = cntTable - 2 To 0 Step -1
        '    body.ChildObjects.Insert(index, table(1))
        'Next

    End Function

    Private Sub DownloadPDFFile(ByVal tempPath As String, ByRef strFileName As String, ByRef strDestFile As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim success As Boolean = False
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            success = imp.Start()
            If success Then
                strFileName = "SPForecast_" & Now.Year.ToString & Now.Month.ToString("d2") & Now.Day.ToString("d2") & "_" & Now.Hour.ToString("d2") & Now.Minute.ToString("d2") & Now.Second.ToString("d2") & ".docx"
                'strDestFile = Server.MapPath("~\DataTemp\PDF\") + strFileName
                strDestFile = KTB.DNet.Lib.WebConfig.GetValue("SAN") + "FieldFix\SPForecast\" & objDealer.DealerCode & "\" & strFileName
                Dim fileInfo As FileInfo = New FileInfo(strDestFile)
                If Not fileInfo.Directory.Exists Then
                    fileInfo.Directory.Create()
                End If
                Dim document As Document = New Document()
                document.LoadFromFile(tempPath)
                document.SaveToFile(strDestFile, Spire.Doc.FileFormat.Docx)
                imp.StopImpersonate()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DataDealerHead(ByVal tempPath As String, ByVal objDealer As Dealer)
        Dim filePath As String = Server.MapPath("~\DataFile\Recall\NewDaftar_Pengiriman_SparepartForecast_Template.docx")
        Dim data = objDealer
        Dim filebytes As Byte() = File.ReadAllBytes(filePath)
        Using Stream As MemoryStream = New MemoryStream()
            Stream.Write(filebytes, 0, filebytes.Length)
            Using doc As WordprocessingDocument = WordprocessingDocument.Open(Stream, True)
                Dim body As DocumentFormat.OpenXml.Wordprocessing.Body = doc.MainDocumentPart.Document.Body
                Dim tables As List(Of DocumentFormat.OpenXml.Wordprocessing.Table) = body.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Table)().ToList()
                For Each table As DocumentFormat.OpenXml.Wordprocessing.Table In tables
                    Dim rows As List(Of DocumentFormat.OpenXml.Wordprocessing.TableRow) = table.Elements(Of DocumentFormat.OpenXml.Wordprocessing.TableRow)().ToList()
                    For Each row As DocumentFormat.OpenXml.Wordprocessing.TableRow In rows
                        For Each cell As DocumentFormat.OpenXml.Wordprocessing.TableCell In row.Descendants(Of DocumentFormat.OpenXml.Wordprocessing.TableCell)().Where(Function(x) x.InnerText.Contains("Var"))
                            Dim texts As List(Of DocumentFormat.OpenXml.Wordprocessing.Text) = cell.SelectMany(Function(p) p.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Run)()).SelectMany(Function(r) r.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Text)()).ToList()
                            Dim word As DocumentFormat.OpenXml.Wordprocessing.Text = New DocumentFormat.OpenXml.Wordprocessing.Text
                            Dim wordGabungan As String = ""
                            Dim i% = 0
                            For Each word2 As DocumentFormat.OpenXml.Wordprocessing.Text In texts
                                wordGabungan += word2.InnerText
                                If texts.Count > 1 Then
                                    If i = 0 Then
                                        word2.Text = ""
                                    End If
                                End If
                                word = word2
                                i += 1
                            Next
                            word.Text = wordGabungan
                            If word.Text.ToLower.Contains("var") Then
                                Select Case word.Text.ToLower
                                    Case "var0" 'Dealer COde
                                        word.Text = objDealer.DealerCode
                                    Case "var1"
                                        word.Text = objDealer.DealerName
                                    Case "var2"
                                        word.Text = objDealer.Address
                                    Case "var3"
                                        word.Text = objDealer.City.CityName
                                    Case "var4"
                                        word.Text = objDealer.Province.ProvinceName
                                    Case "var5"
                                        word.Text = objDealer.ZipCode
                                    Case "var6"
                                        word.Text = objDealer.Phone
                                End Select
                            End If
                        Next
                    Next
                Next
            End Using

            Dim bytes As Byte() = Stream.ToArray()
            If Not System.IO.Directory.Exists(Path.GetDirectoryName(tempPath)) Then
                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(tempPath))
            End If

            If System.IO.File.Exists(tempPath) Then
                System.IO.File.Delete(tempPath)
            End If
            Try
                Dim wFile As System.IO.FileStream
                wFile = New FileStream(tempPath, FileMode.Append)
                wFile.Write(bytes, 0, bytes.Length)
                wFile.Close()
            Catch ex As IOException
                Dim debugs = ""
            End Try

        End Using

    End Sub

    Protected Sub dtgSentPart_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgSentPart.PageIndexChanged
        dtgSentPart.CurrentPageIndex = e.NewPageIndex
        bindGrid(dtgSentPart.CurrentPageIndex)
    End Sub
End Class