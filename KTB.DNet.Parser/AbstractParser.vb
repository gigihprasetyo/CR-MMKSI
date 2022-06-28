#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Data
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Text
Imports System.Data.Odbc
Imports System.Security.Principal.GenericIdentity
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Security.Principal
Imports Excel

#End Region

Namespace KTB.DNet.Parser

    Public MustInherit Class AbstractParser
        Implements IParser

        Private _delimited As String
        Private logFileName As String = AppDomain.CurrentDomain.BaseDirectory
        Private _mUser As IPrincipal
        Private _ObjSyslogParameter As WSMSyslogParameter
        Private _sourceName As String
        Private _blockName As String

        Protected Property Delimited() As String
            Get
                Return _delimited
            End Get
            Set(ByVal Value As String)
                _delimited = Value
            End Set
        End Property

        Public Property SourceName() As String
            Get
                Return _sourceName
            End Get
            Set(ByVal Value As String)
                _sourceName = Value
            End Set
        End Property

        Public Property BlockName() As String
            Get
                Return _blockName
            End Get
            Set(ByVal Value As String)
                _blockName = Value
            End Set
        End Property

        Private ReadOnly Property mUserPrincipal() As IPrincipal
            Get
                Return New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            End Get
        End Property

        Public ReadOnly Property SysLogParameter() As WSMSyslogParameter
            Get
                Return New WSMSyslogParameter(mUserPrincipal)
            End Get
        End Property

        Protected Overridable Function DoParse(ByVal fileName As String, ByVal user As String, ByRef msg As String) As Object

        End Function
        Protected MustOverride Function DoParse(ByVal fileName As String, ByVal user As String) As Object
        Protected Overridable Function DoRead(ByVal fileName As String, ByVal ValueStr As String) As Object

        End Function
        Protected Overridable Function DoTransaction(ByRef msg As String) As Integer

        End Function

        Protected MustOverride Function DoTransaction() As Integer
        Protected MustOverride Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        Protected Function NextLine(ByVal stream As StreamReader)
            Dim stemp As Integer = stream.Read
            Dim sReturn = ""
            While (Not (stemp = -1) And (Not stemp = 10)) 'char 10 = /n
                Dim str As String = stemp.ToString
                'If (stemp = 91) Then 'Asc("[")
                '    stemp = 34 'Asc(\")
                'End If
                'If (stemp = 93) Then 'Asc("]")
                '    stemp = 34 'Asc(\")
                'End If
                sReturn += ChrW(stemp)
                stemp = stream.Read
            End While
            Dim strx As String = sReturn.ToString.Trim
            strx = strx.Replace("""", "''")
            Return strx
        End Function

        Protected Function GetNumberOfColumn(ByVal val As String, ByVal delimiter As String) As Integer
            Dim intReturn As Integer = 0
            If Not val.Trim = "" Then
                Dim temp As String() = val.Split(delimiter)
                intReturn = temp.Length - 1
            End If
            Return intReturn
        End Function

        Protected Function GetGrammarParser()
            Delimited = ";"
            'Regex r = new Regex("\\t(?=([^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            'Regex r = new Regex(";(?=([^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            Dim sBuilder As New StringBuilder
            sBuilder.Append(";(?=([^\")
            sBuilder.Append("""")
            sBuilder.Append("]*\")
            sBuilder.Append("""")
            sBuilder.Append("[^\")
            sBuilder.Append("""")
            sBuilder.Append("]*\")
            sBuilder.Append("""")
            sBuilder.Append(")*(?![^\")
            sBuilder.Append("""")
            sBuilder.Append("]*\")
            sBuilder.Append("""")
            sBuilder.Append("))")
            Dim grammar As New Regex(sBuilder.ToString)
            Return grammar
        End Function

        Protected Function GetGrammarParser(ByVal _Delimited As String)
            'Regex r = new Regex("\\t(?=([^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            'Regex r = new Regex(";(?=([^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            Delimited = _Delimited
            Dim sBuilder As New StringBuilder
            sBuilder.Append(Delimited)
            sBuilder.Append("(?=([^\")
            sBuilder.Append("""")
            sBuilder.Append("]*\")
            sBuilder.Append("""")
            sBuilder.Append("[^\")
            sBuilder.Append("""")
            sBuilder.Append("]*\")
            sBuilder.Append("""")
            sBuilder.Append(")*(?![^\")
            sBuilder.Append("""")
            sBuilder.Append("]*\")
            sBuilder.Append("""")
            sBuilder.Append("))")
            Dim grammar As New Regex(sBuilder.ToString)
            Return grammar
        End Function

        Public Function ParseWithTransaction(ByVal fileName As String, ByVal user As String) Implements IParser.ParseWithTransaction
            Dim ParsingResult As Object = DoParse(fileName, user)
            Dim iResult As Integer = DoTransaction()
            'Return iResult
        End Function

        Public Function ParseWithTransactionWS(ByVal KeyName As String, ByVal Content As String, ByRef msg As String) Implements IParser.ParseWithTransactionWS
            Dim ParsingResult As Object = DoParse(KeyName, Content, msg) ' DoRead(KeyName, Content)
            Dim iResult As Integer = DoTransaction(msg)
            Return iResult
        End Function

        Public Function ParseWithTransactionWS(ByVal KeyName As String, ByVal Content As String) Implements IParser.ParseWithTransactionWS
            Dim ParsingResult As Object = DoParse(KeyName, Content) ' DoRead(KeyName, Content)
            Dim iResult As Integer = DoTransaction()
            Return iResult
        End Function

        Public Function ParsingNoTrasaction(ByVal fileName As String, ByVal user As String) Implements IParser.ParseNoTransaction
            Dim ParsingResult As Object = DoParse(fileName, user)
            Return ParsingResult
        End Function

        Public Function ParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object Implements IParser.ParseFixFormatFile
            Return DoParseFixFormatFile(fileName, user)
        End Function


        'start : donas 201609130323 for Web Service listen to SAP Request

        Private _ColSeparator As String
        Private _RowLineBreaker As String
        Private _IndicatorKey As String
        Private _IndicatorHeader As String
        Private _IndicatorDetail As String
        ' Third-level Detail
        Private _IndicatorDetailChild As String

        Protected ReadOnly Property RowLineBreaker As String
            Get
                _RowLineBreaker = System.Configuration.ConfigurationManager.AppSettings("ROW_LINEBREAKER").ToString()
                If IsNothing(_RowLineBreaker) OrElse _RowLineBreaker = String.Empty Then
                    _RowLineBreaker = "\n"
                End If
                Return _RowLineBreaker
            End Get
        End Property

        Protected Overridable ReadOnly Property ColSeparator As String
            Get
                _ColSeparator = System.Configuration.ConfigurationManager.AppSettings("COL_SEPARATOR").ToString()
                If IsNothing(_ColSeparator) OrElse _ColSeparator = String.Empty Then
                    _ColSeparator = ";"
                End If
                Return _ColSeparator
            End Get
        End Property

        Protected Overridable ReadOnly Property IndicatorKey As String
            Get
                _IndicatorKey = System.Configuration.ConfigurationManager.AppSettings("INDICATOR_KEY").ToString()
                If IsNothing(_IndicatorKey) OrElse _IndicatorKey = String.Empty Then
                    _IndicatorKey = "K"
                End If
                Return _IndicatorKey
            End Get
        End Property

        Protected Overridable ReadOnly Property IndicatorHeader As String
            Get
                _IndicatorHeader = System.Configuration.ConfigurationManager.AppSettings("INDICATOR_HEADER").ToString()
                If IsNothing(_IndicatorHeader) OrElse _IndicatorHeader = String.Empty Then
                    _IndicatorHeader = "H"
                End If
                Return _IndicatorHeader
            End Get
        End Property

        Protected Overridable ReadOnly Property IndicatorDetail As String
            Get
                _IndicatorDetail = System.Configuration.ConfigurationManager.AppSettings("INDICATOR_DETAIL").ToString()
                If IsNothing(_IndicatorDetail) OrElse _IndicatorDetail = String.Empty Then
                    _IndicatorDetail = "D"
                End If
                Return _IndicatorDetail
            End Get
        End Property

        Protected Overridable ReadOnly Property IndicatorDetailChild As String
            Get
                _IndicatorDetailChild = System.Configuration.ConfigurationManager.AppSettings("INDICATOR_DETAIL_CHILD").ToString()
                If IsNothing(_IndicatorDetailChild) OrElse _IndicatorDetailChild = String.Empty Then
                    _IndicatorDetailChild = "DD"
                End If
                Return _IndicatorDetailChild
            End Get
        End Property

        Protected Function GetLines(ByRef str As String) As String()
            Dim lines As String() = str.Split(Me.RowLineBreaker)
            Dim nRow As Integer = lines.Length

            If lines(nRow - 1).Substring(1).Trim() = String.Empty Then nRow = nRow - 1
            Dim ls(nRow - 1) As String

            For i As Integer = 0 To nRow - 1 ' lines.Length - 1
                If i = 0 Then
                    ls(i) = lines(i)
                Else
                    ls(i) = lines(i).Substring(1)
                End If
            Next

            Return ls
        End Function

        Protected Function GetDateShort(ByVal str As String) As Date
            Dim dt As Date 'YYYYMMdd

            Try
                dt = New Date(Integer.Parse(str.Substring(0, 4)), Integer.Parse(str.Substring(4, 2)), Integer.Parse(str.Substring(6, 2)))
            Catch ex As Exception
                dt = DateSerial(1900, 1, 1)
            End Try
            Return dt
        End Function

        Protected Function GetDateLong(ByVal str As String) As DateTime
            Dim dt As Date 'YYYYMMddHHmmss

            Try
                dt = New Date(Integer.Parse(str.Substring(0, 4)), Integer.Parse(str.Substring(4, 2)), Integer.Parse(str.Substring(6, 2)), Integer.Parse(str.Substring(8, 2)), Integer.Parse(str.Substring(10, 2)), Integer.Parse(str.Substring(12, 2)))
            Catch ex As Exception
                dt = DateSerial(1900, 1, 1)
            End Try
            Return dt
        End Function

        Protected Function GetCurrency(ByVal str As String) As Decimal
            Try
                Return Decimal.Parse(str)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Protected Function GetNumber(ByVal str As String) As Integer
            Try
                Return Integer.Parse(str)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'end : donas 201609130323 for Web Service listen to SAP Request


    End Class

    Public MustInherit Class AbstractExcelParser
        Implements IExcelParser
        Protected ExcelObj As Excel.Application = Nothing
        Protected StrConnection As String = "Driver={Microsoft Excel Driver (*.xls)};DBQ="
        Protected objConn As OdbcConnection
        Protected objCmd As OdbcCommand
        Protected objDA As OdbcDataAdapter
        Protected objDs As DataSet
        Protected objReader As OdbcDataReader
        Protected DataCollection As ArrayList

        Protected MustOverride Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object

        Protected Overridable Function ParsingExcelNoTransactionWithValidDate(ByVal fileName As String, ByVal sheetName As String, ByVal user As String, ByVal ValidFrom As DateTime, ByVal ValidTo As DateTime) As Object
        End Function

        Public Shared ContentTypeExcel As String

        ''' <summary>
        ''' Parse Excell to DataSet
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <param name="sheetName"></param>
        ''' <param name="user"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ParseExcelDataSet(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As System.Data.DataSet Implements IExcelParser.ParseExcelDataSet
            Try

                Dim strConn As String = ""
                Dim parts() As String = fileName.Split(".")
                Dim ext As String = parts(parts.Length - 1).Trim().ToLower()
                Dim reader As IExcelDataReader = Nothing
                Dim objDS As DataSet
                sheetName = sheetName.Replace("[", "").Replace("]", "").Replace("$", "")

                Using stream As FileStream = File.Open(fileName, FileMode.Open, FileAccess.Read)


                    If (ext = "xls") Then
                        reader = ExcelReaderFactory.CreateBinaryReader(stream)
                    ElseIf (ext = "xlsx") Then
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                    End If

                    If (Not IsNothing(reader)) Then
                        Dim ObjDS2 As New DataSet
                        reader.IsFirstRowAsColumnNames = True
                        objDS = reader.AsDataSet()
                        'objDS = New DataSet

                        'For Each dt As System.Data.DataTable In ObjDS2.Tables
                        '    If (dt.TableName.ToLower.Contains(sheetName.ToLower)) Then
                        '        Dim DtC As System.Data.DataTable = New Data.DataTable()
                        '        DtC = dt.Copy()
                        '        objDS.Tables.Add(DtC)
                        '        Exit For
                        '    End If
                        'Next


                    End If

                End Using

                Return objDS
            Catch ex As Exception
                Dim str As String = ex.Message
                Return Nothing
            End Try
        End Function

        Public Function ParseExcelMultiSheetDataSet(ByVal fileName As String, ByVal sheetName() As String, ByVal user As String) As System.Data.DataSet
            ''Implements IExcelParser.ParseExcelDataSet
            Try

                Dim strConn As String = ""
                Dim parts() As String = fileName.Split(".")
                Dim ext As String = parts(parts.Length - 1).Trim().ToLower()
                Dim reader As IExcelDataReader = Nothing
                Dim objDS As DataSet
                'sheetName = sheetName.Replace("[", "").Replace("]", "").Replace("$", "")

                Using stream As FileStream = File.Open(fileName, FileMode.Open, FileAccess.Read)


                    If (ext = "xls") Then
                        reader = ExcelReaderFactory.CreateBinaryReader(stream)
                    ElseIf (ext = "xlsx") Then
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                    End If

                    If (Not IsNothing(reader)) Then
                        Dim ObjDS2 As New DataSet
                        reader.IsFirstRowAsColumnNames = True
                        objDS = reader.AsDataSet()
                        'objDS = New DataSet

                        'For Each dt As System.Data.DataTable In ObjDS2.Tables
                        '    If (dt.TableName.ToLower.Contains(sheetName.ToLower)) Then
                        '        Dim DtC As System.Data.DataTable = New Data.DataTable()
                        '        DtC = dt.Copy()
                        '        objDS.Tables.Add(DtC)
                        '        Exit For
                        '    End If
                        'Next


                    End If

                End Using

                Return objDS
            Catch ex As Exception
                Dim str As String = ex.Message
                Return Nothing
            End Try
        End Function


#Region "Old ParseExcelDataSet"
        'Public Function ParseExcelDataSet(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As System.Data.DataSet Implements IExcelParser.ParseExcelDataSet
        '    Try
        '        Dim strConn As String = ""
        '        Dim parts() As String = fileName.Split(".")
        '        Dim ext As String = parts(parts.Length - 1).Trim().ToLower()
        '        If ContentTypeExcel <> "" Then
        '            If ContentTypeExcel = "application/vnd.ms-excel" _
        '                OrElse ContentTypeExcel = "application/octet-stream" _
        '                OrElse ext = "xls" Then
        '                strConn = StrConnection & fileName
        '            Else
        '                strConn = KTB.DNet.Lib.WebConfig.GetValue("ExcelConnection") & fileName
        '            End If
        '        Else
        '            strConn = StrConnection & fileName
        '        End If


        '        Dim objConn = New OdbcConnection(strConn)
        '        objConn.Open()
        '        Dim objCmd = New OdbcCommand("SELECT * FROM " & sheetName, objConn)
        '        Dim objDA = New OdbcDataAdapter
        '        objDA.SelectCommand = objCmd
        '        Dim objDS = New DataSet
        '        objDA.Fill(objDS)
        '        objConn.Close()
        '        Return objDS
        '    Catch ex As Exception
        '        Dim str As String = ex.Message
        '        Return Nothing
        '    End Try
        'End Function
#End Region



        Public Function ParseExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object Implements IExcelParser.ParseExcelNoTransaction
            Return ParsingExcelNoTransaction(fileName, sheetName, user)
        End Function

        Protected Overridable Function ParseExcelNoTransactionWithValidDate(ByVal fileName As String, ByVal sheetName As String, ByVal user As String, ByVal ValidFrom As Date, ByVal ValidTo As Date) As Object Implements IExcelParser.ParseExcelNoTransactionWithValidDate
            Return ParsingExcelNoTransactionWithValidDate(fileName, sheetName, user, ValidFrom, ValidTo)
        End Function

    End Class

End Namespace