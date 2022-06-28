 

Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Runtime.InteropServices
Namespace KTB.DNet.Utility


Public Class ExcelParser
    Private ExcelObj As Excel.Application = Nothing
    Public Sub New()

    End Sub

    Public Function GetDataFromExcel(ByVal FileName As String, _
        ByVal RangeName As String) As System.Data.DataSet
        ' Returns a DataSet containing information from a named range
        ' in the passed Excel worksheet  
        Try
                Dim strConn As String = "Driver={Microsoft Excel Driver (*.xls)};DBQ=" & FileName

                Dim objConn _
                    As New System.Data.Odbc.OdbcConnection(strConn)
            objConn.Open()
            ' Create objects ready to grab data
                Dim objCmd As New System.Data.Odbc.OdbcCommand( _
                "SELECT * FROM " & RangeName, objConn)
                Dim objDA As New System.Data.Odbc.OdbcDataAdapter
            objDA.SelectCommand = objCmd
            ' Fill DataSet
            Dim objDS As New System.Data.DataSet
            objDA.Fill(objDS)
            ' Clean up and return DataSet
            objConn.Close()
            Return objDS
        Catch ex As Exception
            Dim str As String = ex.Message
            ' Possible errors include Excel file already open and
            ' locked, et al.
            Return Nothing
        End Try
    End Function

    Public Function ReadDataFromExcel(ByVal FileName As String, _
       ByVal RangeName As String) As ArrayList
        ' Returns a DataSet containing information from a named range
        ' in the passed Excel worksheet  
        Try
            Dim strConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & FileName & ";Extended Properties=Excel 8.0;"
            Dim al As New ArrayList
            Dim objConn _
                As New System.Data.OleDb.OleDbConnection(strConn)
            objConn.Open()
            ' Create objects ready to grab data
            Dim objCmd As New System.Data.OleDb.OleDbCommand( _
                "SELECT * FROM " & RangeName, objConn)
            objCmd.CommandType = CommandType.Text

            Dim objReader As System.Data.OleDb.OleDbDataReader
            objReader = objCmd.ExecuteReader()
            ' Fill DataSet
            While objReader.Read()

                'Dim _data As New Data
                '_data.NAME = objReader(1)
                '_data.ID = objReader(0)
                '_data.KOTA = objReader(2)
                'al.Add(_data)
            End While
            Return al
        Catch ex As Exception
            Dim str As String = ex.Message
            ' Possible errors include Excel file already open and
            ' locked, et al.
            Return Nothing
        End Try
    End Function

    Public Function ReadDataFromExcelWorkBook(ByVal fileName As String) As ArrayList

        Dim theWorkbook As Excel.Workbook
        Try
            ExcelObj = New Excel.Application
            ExcelObj.Visible = False
            theWorkbook = ExcelObj.Workbooks.Open(fileName, 0, True, 5, "", "", True, Excel.XlPlatform.xlWindows, "\t", False, False, 0, True, True, False)

        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
        'get the collection of sheets in the workbook
        Dim sheets As Excel.Sheets = theWorkbook.Worksheets

        ' get the first and only worksheet from the collection 
        ' of worksheets
        Dim worksheet As Excel.Worksheet = CType(sheets, Excel.Worksheet).get_Item(1)

        Dim al As New ArrayList
        For i As Integer = 1 To 10
            Dim str As String = "A" + i.ToString()
            Dim range As Excel.Range = worksheet.get_Range("A" + i.ToString(), "J" + i.ToString())
            Dim myvalues As Array = CType(range.Cells.Value2, Array)
            al.Add(myvalues)
        Next

        theWorkbook.Close(False, fileName, False)
        Return al
    End Function



End Class

End Namespace