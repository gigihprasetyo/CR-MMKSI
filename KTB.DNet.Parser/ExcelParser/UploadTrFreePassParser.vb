Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports System.IO
Imports OfficeOpenXml
Imports System.Collections.Generic
Imports System.Linq
Imports System.Security
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports System.Security.Principal
Imports System.Text
Imports KTB.DNet.Domain.Search

Namespace KTB.DNet.Parser

    Public Class UploadTrFreePassParser
        Inherits AbstractExcelParser

        Private m_userPrincipal As IPrincipal = Nothing
        Private excelValidation As ExcelValidation = New ExcelValidation()
        Private m_page As System.Web.UI.Page

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
        End Sub

        Public Sub New(ByVal userPrincipal As IPrincipal, ByVal page As System.Web.UI.Page)
            Me.m_userPrincipal = userPrincipal
            Me.m_page = page
        End Sub


        Protected Overrides Function ParsingExcelNoTransaction(fileName As String, sheetName As String, user As String) As Object

            Dim arlResult As ArrayList = GetResultParse(fileName)
            Dim listError As List(Of ErrorExcelUpload) = New List(Of ErrorExcelUpload)
            Dim listFreePass As List(Of TrFreePass) = MappingParserResultToData(arlResult, listError)

            If listError.Count = 0 Then
                SaveData(listFreePass)
            End If

            Return listError
        End Function

        Private Function GetResultParse(ByVal fileName As String) As ArrayList
            Dim fileInfo As FileInfo = New FileInfo(fileName)
            Dim result As New ArrayList
            Using excelPkg As New ExcelPackage(fileInfo)
                Using ws As ExcelWorksheet = excelPkg.Workbook.Worksheets.Item(1)
                    Dim ColumnCount As Integer = ws.Dimension.End.Column
                    Dim RowCount As Integer = ws.Dimension.End.Row

                    For idx As Integer = 4 To RowCount
                        Dim arlString As List(Of String) = New List(Of String)
                        For i As Integer = 1 To ColumnCount
                            arlString.Add(ws.GetCellValue(idx, i))
                        Next

                        result.Add(arlString)
                    Next
                End Using
            End Using
            Return result
        End Function

        Private Function MappingParserResultToData(arlResult As ArrayList, ByRef listError As List(Of ErrorExcelUpload)) As List(Of TrFreePass)
            Dim result As New List(Of TrFreePass)
            Dim listTahunFiskal As List(Of String) = GetTahunFiskal()
            For i As Integer = 0 To arlResult.Count - 1
                Dim listErrorPerRow As New List(Of ErrorExcelUpload)
                Dim ErrorMessage As New StringBuilder
                Dim rowValue As List(Of String) = CType(arlResult(i), List(Of String))
                Dim item As New TrFreePass
                item.QtyUsed = 0
                item.Status = 1

                Dim dealerData As Dealer = New DealerFacade(m_userPrincipal).Retrieve(rowValue(0))

                If dealerData.ID = 0 Then
                    Dim err As ErrorExcelUpload = New ErrorExcelUpload(excelValidation.GetExcelColumnName(1) & (i + 4).ToString(), rowValue(0), "Kode Dealer tidak ditemukan")
                    listErrorPerRow.Add(err)
                    'ErrorMessage.Append("Kode Dealer " & rowValue(0) & " tidak ditemukan dalam database." & Chr(13) & Chr(10))
                Else
                    item.Dealer = dealerData
                End If

                If Not listTahunFiskal.Contains(rowValue(1)) Then
                    Dim err As ErrorExcelUpload = New ErrorExcelUpload(excelValidation.GetExcelColumnName(2) & (i + 4).ToString(), rowValue(1), "Tahun Fiskal tidak valid")
                    listErrorPerRow.Add(err)
                    ' ErrorMessage.Append("Tahun fiskal tidak valid (format yyyy/yyyy)." & Chr(13) & Chr(10))
                Else
                    item.FiscalYear = rowValue(1)
                End If

                If Not IsNumeric(rowValue(2)) Then
                    Dim err As ErrorExcelUpload = New ErrorExcelUpload(excelValidation.GetExcelColumnName(3) & (i + 4).ToString(), rowValue(2), "Qty tidak valid")
                    listErrorPerRow.Add(err)
                    ' ErrorMessage.Append("Qty tidak valid." & Chr(13) & Chr(10))
                Else
                    item.Qty = rowValue(2)
                End If

                If listErrorPerRow.Count = 0 Then
                    Dim existingData As TrFreePass = GetExistingFreePass(item.Dealer.DealerCode, item.FiscalYear)
                    If existingData.ID <> 0 Then
                        If item.Qty < existingData.QtyUsed Then
                            Dim err As ErrorExcelUpload = New ErrorExcelUpload(excelValidation.GetExcelColumnName(3) & (i + 4).ToString(), rowValue(2), "Free pass terpakai melebihi Qty")
                            listErrorPerRow.Add(err)
                        Else
                            item.QtyUsed = existingData.QtyUsed
                        End If
                    End If
                End If

                listError.AddRange(listErrorPerRow)
                result.Add(item)

            Next

            Return result
        End Function

        Private Function GetTahunFiskal() As List(Of String)
            Dim GetTahun As Integer = DateTime.Now.Year
            Dim result As List(Of String) = New List(Of String)

            'Before
            For x As Integer = 4 To 0 Step -1
                Dim value1 As String = (GetTahun - x).ToString()
                Dim value2 As String = (GetTahun - x - 1).ToString()
                Dim value As String = String.Format("{0}/{1}", value2, value1)
                result.Add(value)
            Next
            'After
            For x As Integer = 0 To 4
                Dim value1 As String = (GetTahun + x).ToString()
                Dim value2 As String = (GetTahun + x + 1).ToString()
                Dim value As String = String.Format("{0}/{1}", value1, value2)
                result.Add(value)
            Next
            Return result
        End Function

        Private Function GenerateListError(listFreePass As List(Of TrFreePass)) As List(Of ErrorExcelUpload)
            Dim result As New List(Of ErrorExcelUpload)
            For i As Integer = 0 To listFreePass.Count - 1

            Next

            Return result
        End Function

        Private Function GetExistingFreePass(ByVal dealerCode As String, ByVal fiscalYear As String) As TrFreePass
            Dim criterias As New CriteriaComposite(New Criteria(GetType(TrFreePass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrFreePass), "Dealer.DealerCode", MatchType.Exact, dealerCode))
            criterias.opAnd(New Criteria(GetType(TrFreePass), "FiscalYear", MatchType.Exact, fiscalYear))

            Dim result As New TrFreePass

            Dim arlResult As ArrayList = New TrFreePassFacade(m_userPrincipal).Retrieve(criterias)

            If arlResult.Count > 0 Then
                result = arlResult(0)
            End If


            Return result

        End Function

        Private Sub SaveData(listFreePass As List(Of TrFreePass))
            Dim facade As TrFreePassFacade = New TrFreePassFacade(m_userPrincipal)
            For Each data As TrFreePass In listFreePass
                Dim existingData As TrFreePass = GetExistingFreePass(data.Dealer.DealerCode, data.FiscalYear)
                If existingData.ID <> 0 Then
                    facade.Update(data)
                Else
                    facade.Insert(data)
                End If
            Next
        End Sub

    End Class



End Namespace
