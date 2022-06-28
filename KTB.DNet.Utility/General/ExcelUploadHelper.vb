Imports System.IO
Imports System.Text
Imports System.Collections.Generic
Imports System.Linq
Imports System.Security.Principal
Imports System.Runtime.CompilerServices
Imports OfficeOpenXml
Imports OfficeOpenXml.DataValidation
Imports System.Web.UI.Page
Imports KTB.DNet.Utility

Public Class ExcelField

    Public Sub New()
    End Sub

    Public Sub New(ByVal name As String, ByVal value As String, ByVal row As Integer, _
                   ByVal column As Integer, ByVal validation As String, Optional ByVal length As Integer = 0)
        Me.Name = name
        Me.Value = value
        Me.Row = row
        Me.Column = column
        Me.Validation = validation
        Me.Length = length
    End Sub

    Private _row As Integer
    Public Property Row() As Integer
        Get
            Return _row
        End Get
        Set(ByVal value As Integer)
            _row = value
        End Set
    End Property

    Private _column As Integer
    Public Property Column() As Integer
        Get
            Return _column
        End Get
        Set(ByVal value As Integer)
            _column = value
        End Set
    End Property

    Private _length As Integer
    Public Property Length() As Integer
        Get
            Return _length
        End Get
        Set(ByVal value As Integer)
            _length = value
        End Set
    End Property

    Private _value As String
    Public Property Value() As String
        Get
            Return _value
        End Get
        Set(ByVal value As String)
            _value = value
        End Set
    End Property

    Private _name As String
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Private _validation As String
    Public Property Validation() As String
        Get
            Return _validation
        End Get
        Set(ByVal value As String)
            _validation = value
        End Set
    End Property

End Class

Public Class ErrorExcelUpload
    Public Sub New(ByVal cell As String, ByVal value As String, ByVal message As String)
        Me.Cell = cell
        Me.Value = value
        Me.Message = message
    End Sub

    Private _cell As String
    Public Property Cell() As String
        Get
            Return _cell
        End Get
        Set(ByVal value As String)
            _cell = value
        End Set
    End Property

    Private _value As String
    Public Property Value() As String
        Get
            Return _value
        End Get
        Set(ByVal value As String)
            _value = value
        End Set
    End Property

    Private _message As String
    Public Property Message() As String
        Get
            Return _message
        End Get
        Set(ByVal value As String)
            _message = value
        End Set
    End Property

End Class

Public Class ExcelValidation
    Implements ICollection(Of ExcelField)

    Private listExcelField As List(Of ExcelField)
    Private WorkSheet As ExcelWorksheet

    Public Sub New()
        listExcelField = New List(Of ExcelField)
    End Sub

    Public Sub New(ByVal ws As ExcelWorksheet)
        Me.WorkSheet = ws
        listExcelField = New List(Of ExcelField)
    End Sub

    Public ReadOnly Property Count() As Integer Implements ICollection(Of ExcelField).Count
        Get
            Return listExcelField.Count
        End Get
    End Property

    Public ReadOnly Property IsReadOnly() As Boolean Implements ICollection(Of ExcelField).IsReadOnly
        Get
            Return True
        End Get
    End Property

    Public Sub Clear() _
        Implements ICollection(Of ExcelField).Clear
        listExcelField.Clear()
    End Sub

    Public Sub Add(ByVal excField As ExcelField) Implements ICollection(Of ExcelField).Add
        listExcelField.Add(excField)
    End Sub

    Public Function Create(ByVal name As String, ByVal row As Integer, _
                   ByVal column As Integer, ByVal validation As String, Optional ByVal length As Integer = 0) As ExcelField
        Dim rest As ExcelField = New ExcelField(name, WorkSheet.GetCellValue(row, column), row, column, validation, length)
        listExcelField.Add(rest)
        Return rest
    End Function

    Public Sub RemoveAt(ByVal index As Integer)
        listExcelField.RemoveAt(index)
    End Sub

    Public Sub CopyTo(ByVal arrexcField() As ExcelField, ByVal index As Integer) Implements ICollection(Of ExcelField).CopyTo
        listExcelField.CopyTo(arrexcField, index)
    End Sub

    Public Function Remove(ByVal excField As ExcelField) As Boolean Implements ICollection(Of ExcelField).Remove
        Return listExcelField.Remove(excField)
    End Function

    Public Function Contrains(ByVal excField As ExcelField) As Boolean _
        Implements ICollection(Of ExcelField).Contains
        Return listExcelField.Contains(excField)
    End Function

    Public Function Item(ByVal index As Integer) As ExcelField
        Return listExcelField.Item(index)
    End Function

    Public Function GetEnumerator() As IEnumerator(Of ExcelField) Implements IEnumerable(Of ExcelField).GetEnumerator
        Return listExcelField
    End Function

    Public Function GetEnumerator1() As IEnumerator Implements IEnumerable(Of ExcelField).GetEnumerator
        Return listExcelField
    End Function

    Public Function CreateCustomError(ByVal field As ExcelField, ByVal message As String, Optional ByVal isName As Boolean = True) As ErrorExcelUpload
        If isName Then
            Return New ErrorExcelUpload(GetExcelColumnName(field.Column) & field.Row.ToString(), field.Value, field.Name & " " & message)
        End If

        Return New ErrorExcelUpload(GetExcelColumnName(field.Column) & field.Row.ToString(), field.Value, message)
    End Function

    Public Function Validate() As List(Of ErrorExcelUpload)
        Dim listError As List(Of ErrorExcelUpload) = New List(Of ErrorExcelUpload)
        For Each field As ExcelField In listExcelField
            If String.IsNullorEmpty(field.Validation) Then
                Continue For
            End If

            Dim arrTipeVal() As String = field.Validation.Split(New Char() {","}, _
                                          System.StringSplitOptions.RemoveEmptyEntries)
            For Each tipeVal As String In arrTipeVal
                Select Case tipeVal.ToLower()
                    Case "required"
                        If String.IsNullorEmpty(field.Value) Then
                            listError.Add(New ErrorExcelUpload(GetExcelColumnName(field.Column) & field.Row.ToString(), _
                                         field.Value, _
                                         field.Name + " harus diisi"))
                        End If
                    Case "numeric"
                        If Not String.IsNullorEmpty(field.Value) Then
                            Try
                                Dim xValue As Double = Double.Parse(field.Value)
                            Catch ex As Exception
                                listError.Add(New ErrorExcelUpload(GetExcelColumnName(field.Column) & field.Row.ToString(), _
                                         field.Value, _
                                         field.Name + " harus berisi angka"))
                            End Try
                        End If
                    Case "email"
                        If Not String.IsNullorEmpty(field.Value) Then
                            Try
                                Dim mailAdd As System.Net.Mail.MailAddress = New Net.Mail.MailAddress(field.Value)
                            Catch
                                listError.Add(New ErrorExcelUpload(GetExcelColumnName(field.Column) & field.Row.ToString(), _
                                         field.Value, _
                                         "format email tidak benar"))
                            End Try
                        End If
                    Case "max"
                        If Not String.IsNullorEmpty(field.Value) Then
                            If field.Value.Trim.Length > field.Length Then
                                listError.Add(New ErrorExcelUpload(GetExcelColumnName(field.Column) & field.Row.ToString(), _
                                         field.Value, _
                                         field.Name + " maksimal {1} karakter"))
                            End If
                        End If
                    Case "date"
                        If Not String.IsNullorEmpty(field.Value) Then
                            Try
                                Dim arrDate() As String = field.Value.Split("-")
                                Dim isDate As DateTime = New DateTime(Integer.Parse(arrDate(2)), _
                                                                    Integer.Parse(arrDate(1)), _
                                                                    Integer.Parse(arrDate(0)))
                            Catch ex As Exception
                                listError.Add(New ErrorExcelUpload(GetExcelColumnName(field.Column) & field.Row.ToString(), _
                                         field.Value, _
                                         field.Name + " format tanggal harus (DD-MM-YYYY)"))
                            End Try
                        End If
                End Select
            Next
        Next
        Return listError
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="columnNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetExcelColumnName(columnNumber As Integer) As String
        Dim dividend As Integer = columnNumber
        Dim columnName As String = String.Empty
        Dim modulo As Integer

        While dividend > 0
            modulo = (dividend - 1) Mod 26
            columnName = Convert.ToChar(65 + modulo).ToString() & columnName
            dividend = CInt((dividend - modulo) / 26)
        End While

        Return columnName
    End Function
End Class

Public Class ExcelTemplate

    Private _Page As System.Web.UI.Page
    Private _Judul As String
    Private _TimeOut As Integer
    Private _SheetName As String
    Private _FileName As String
    Private ListColumn As List(Of ExcelTemplateColumn)
    Private ListValue As New Dictionary(Of Integer, List(Of ExcelTemplateColumn))
    Private listSheet As List(Of ExcelSheetData)
    Private excelPackage As ExcelPackage
    Private ListRow As ArrayList

    Public Sub New(ByVal page As System.Web.UI.Page)
        Me._Page = page
        Me._Judul = String.Empty
        Me._SheetName = String.Empty
        Me._FileName = String.Empty
        Me.ListColumn = New List(Of ExcelTemplateColumn)
        Me.ListValue = New Dictionary(Of Integer, List(Of ExcelTemplateColumn))
        Me.listSheet = New List(Of ExcelSheetData)
        Me.TimeOut = 120
        Me.ListRow = New ArrayList
    End Sub

    Public ReadOnly Property IsSheetData() As Boolean
        Get
            Return Not Me.listSheet.Count.Equals(0)
        End Get
    End Property

    Public Property FileName() As String
        Get
            Return _FileName
        End Get
        Set(ByVal value As String)
            _FileName = value
        End Set
    End Property

    Public Property SheetName() As String
        Get
            Return _SheetName
        End Get
        Set(ByVal value As String)
            _SheetName = value
        End Set
    End Property

    Public Property Judul() As String
        Get
            Return _Judul
        End Get
        Set(ByVal value As String)
            _Judul = value
        End Set
    End Property

    ''' <summary>
    ''' Default 120 second 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TimeOut() As Integer
        Get
            Return _TimeOut
        End Get
        Set(ByVal value As Integer)
            _TimeOut = value
        End Set
    End Property

    Public Sub AddRow(ByVal arrValue As ArrayList)
        Me.ListRow.Add(arrValue)
    End Sub

    Public Sub AddField(ByVal dataColumn As ExcelTemplateColumn)
        Me.ListColumn.Add(dataColumn)
    End Sub

    Public Sub AddField(ByVal index As Integer, ByVal value As String)
        Me.ListColumn.Add(New ExcelTemplateColumn(index, value))
    End Sub

    Public Sub AddField(ByVal index As Integer, ByVal value As String, ByVal type As EnumTypeCell)
        Me.ListColumn.Add(New ExcelTemplateColumn(index, value, type))
    End Sub

    Public Sub AddValue(ByVal index As Integer, ByVal dataValue As List(Of ExcelTemplateColumn))
        Me.ListValue.Add(index, dataValue)
    End Sub

    Public Sub AddSheet(ByVal sheet As ExcelSheetData)
        Me.listSheet.Add(sheet)
    End Sub

    Public Sub DownLoad()
        Try
            If String.IsNullorEmpty(Me.FileName) Or String.IsNullorEmpty(Me.SheetName) Then
                Throw New Exception("Filename dan sheetname harus di isi")
            Else
                If Not (Me.FileName.IndexOf("xls") > -1 Or Me.FileName.IndexOf("xlsx") > -1) Then
                    Me.FileName = Me.FileName + ".xlsx"
                End If
            End If

            Using excelPackage As New ExcelPackage()
                'excelPackage = New ExcelPackage()
                Dim ws As ExcelWorksheet = excelPackage.Workbook.Worksheets.Add(Me.SheetName)
                ws.View.ShowGridLines = False
                ws.Cells("A1").Value = Me.Judul
                ws.Cells("A1").Style.Font.Bold = True

                If IsSheetData Then
                    For Each dataSheet As ExcelSheetData In listSheet
                        Dim wsData As ExcelWorksheet = excelPackage.Workbook.Worksheets.Add(dataSheet.SheetName)
                        wsData.View.ShowGridLines = True
                        wsData.Cells("A1").Value = dataSheet.Judul
                        wsData.Cells("A1").Style.Font.Bold = True
                        wsData.Cells("A3").Value = dataSheet.ColumnCode
                        wsData.Cells("B3").Value = dataSheet.ColumnName
                        wsData.Cells("A3").Style.Font.Bold = True
                        wsData.Cells("B3").Style.Font.Bold = True

                        Dim idx As Integer = 4
                        For Each item As KeyValuePair(Of String, String) In dataSheet.listData
                            wsData.Cells("A" & idx.ToString()).Value = item.Key
                            wsData.Cells("B" & idx.ToString()).Value = item.Value

                            idx = idx + 1
                        Next
                    Next
                End If

                Dim idxValue As Integer = 3
                For Each itemValue As KeyValuePair(Of Integer, List(Of ExcelTemplateColumn)) In ListValue.OrderBy(Function(x) x.Key)
                    For Each dataItem As ExcelTemplateColumn In itemValue.Value
                        ws.Cells(GetExcelColumnName(dataItem.Index) & (idxValue + itemValue.Key).ToString()).Value = dataItem.Value
                    Next
                Next

                For Each item As ExcelTemplateColumn In ListColumn.OrderBy(Function(x) x.Index)
                    If item.Type.Equals(EnumTypeCell.Dropdownlist) Then
                        Dim address As String = String.Format("{0}4:{0}9999", GetExcelColumnName(item.Index))
                        Dim vals As ExcelDataValidationList = ws.DataValidations.AddListValidation(address)
                        For Each idata As String In item.DataValidation
                            vals.Formula.Values.Add(idata)
                        Next
                    ElseIf item.Type.Equals(EnumTypeCell.FormatDate) Then
                        Dim address As String = String.Format("{0}4:{0}9999", GetExcelColumnName(item.Index))
                        ws.Cells(address).Style.Numberformat.Format = "dd-mm-yyyy"
                    End If
                    ws.Cells(GetExcelColumnName(item.Index) & "3").SetHeaderValue(item.Value, 1)
                    ws.Column(item.Index).AutoFit()
                Next

                Dim idxStartRow As Integer = 4
                For Each arrData As ArrayList In ListRow
                    For i As Integer = 0 To arrData.Count - 1
                        ws.Cells(idxStartRow, i + 1).Value = arrData(i).ToString()
                    Next
                Next

                Dim fileBytes = excelPackage.GetAsByteArray()
                '_Page.Response.Clear()
                '_Page.Server.ScriptTimeout = TimeOut
                '_Page.Response.AppendHeader("Content-Length", fileBytes.Length.ToString())
                '_Page.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" 'xls
                '_Page.Response.AddHeader("content-disposition", String.Format(" filename={0}", Me.FileName))
                '_Page.Response.BinaryWrite(fileBytes)
                '_Page.Response.Flush()
                '_Page.Response.[End]()


                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

                Dim success As Boolean = False

                Try
                    success = imp.Start()
                    If success Then
                        File.WriteAllBytes(_Page.Server.MapPath("~/DataTemp/" & FileName), fileBytes)
                        imp.StopImpersonate()
                    End If

                Catch ex As Exception
                    Exit Sub

                End Try

                _Page.Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & FileName)

            End Using
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="columnNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetExcelColumnName(columnNumber As Integer) As String
        Dim dividend As Integer = columnNumber
        Dim columnName As String = String.Empty
        Dim modulo As Integer

        While dividend > 0
            modulo = (dividend - 1) Mod 26
            columnName = Convert.ToChar(65 + modulo).ToString() & columnName
            dividend = CInt((dividend - modulo) / 26)
        End While

        Return columnName
    End Function

End Class

Public Class ExcelSheetData
    Public Sub New()
        Me.listData = New Dictionary(Of String, String)
        Me.SheetName = String.Empty
    End Sub

    Public listData As Dictionary(Of String, String)
    Private _SheetName As String
    Private _Judul As String

    Public Property SheetName() As String
        Get
            Return _SheetName
        End Get
        Set(ByVal value As String)
            _SheetName = value
        End Set
    End Property

    Public Property Judul() As String
        Get
            Return _Judul
        End Get
        Set(ByVal value As String)
            _Judul = value
        End Set
    End Property

    Private _ColumnCode As String
    Public Property ColumnCode() As String
        Get
            Return _ColumnCode
        End Get
        Set(ByVal value As String)
            _ColumnCode = value
        End Set
    End Property

    Private _ColumnName As String
    Public Property ColumnName() As String
        Get
            Return _ColumnName
        End Get
        Set(ByVal value As String)
            _ColumnName = value
        End Set
    End Property

    Public Sub AddData(ByVal code As Integer, ByVal value As String)
        Me.listData.Add(code, value)
    End Sub

    Public Sub AddData(ByVal data As Dictionary(Of String, String))
        For Each item As KeyValuePair(Of String, String) In data
            Me.listData.Add(item.Key, item.Value)
        Next
    End Sub
End Class

Public Class ExcelTemplateColumn
    Public Sub New()
        Me.Type = EnumTypeCell.Text
    End Sub
    Public Sub New(ByVal idx As Integer, values As String, Optional type As EnumTypeCell = EnumTypeCell.Text)
        Me.Type = type
        Me.Index = idx
        Me.Value = values
    End Sub

    Private _index As Integer
    Public Property Index() As Integer
        Get
            Return _index
        End Get
        Set(ByVal value As Integer)
            _index = value
        End Set
    End Property

    Private _value As String
    Public Property Value() As String
        Get
            Return _value
        End Get
        Set(ByVal value As String)
            _value = value
        End Set
    End Property

    Private _type As EnumTypeCell
    Public Property Type() As EnumTypeCell
        Get
            Return _type
        End Get
        Set(ByVal value As EnumTypeCell)
            _type = value
        End Set
    End Property

    Private datas As IEnumerable(Of String)
    Public Property DataValidation() As IEnumerable(Of String)
        Get
            Return datas
        End Get
        Set(ByVal value As IEnumerable(Of String))
            datas = value
        End Set
    End Property


End Class

Public Enum EnumTypeCell
    Text = 1
    Dropdownlist = 2
    FormatDate = 3
End Enum

