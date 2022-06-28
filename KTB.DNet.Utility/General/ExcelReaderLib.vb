#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2005

'Intimedia grants you the right to use and modify the code in this Persistence Framework
'(code under the Framework Namespace) but 
'(i) only for the solutions that are developed by Intimedia for you 
'(ii) or in solutions that are developed in join development between you and Intimedia.

'All rights not expressly granted, are reserved.
#End Region

#Region "Summary"
'///////////////////////////////////////////////////////////////////////////////////////
'// Author Name: 
'// PURPOSE       : Enter summary here after generation.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 8/18/2005 - 11:51:00 AM
'//
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"

Imports System
Imports System.Data
Imports System.Data.OleDb

#End Region

Namespace KTB.DNet.Utility
    Public Class ExcelReaderLib
        Implements IDisposable

#Region "Private Variables"

        Private _PKCol As Integer()
        Private _strExcelFilename As String
        Private _blnMixedData As Boolean = True
        Private _blnHeaders As Boolean = False
        Private _strSheetName As String
        Private _strSheetRange As String
        Private _blnKeepConnectionOpen As Boolean = False
        Private _oleConn As OleDbConnection
        Private _oleCmdSelect As OleDbCommand
        Private _oleCmdUpdate As OleDbCommand

#End Region

#Region "Constructor"

        Public Sub New()
        End Sub

#End Region

#Region "Properties"

        Property PKCols() As Integer()
            Get
                Return _PKCol
            End Get
            Set(ByVal Value As Integer())
                _PKCol = Value
            End Set
        End Property

        Public Function ColName(ByVal intCol As Int16)
            Dim sColName As String = String.Empty
            If (intCol < 26) Then
                sColName = Convert.ToString(Convert.ToChar((Convert.ToByte(CType("A", Char)) + intCol)))
            Else
                Dim intFirst As Integer = CInt(intCol / 26)
                Dim intSecond As Integer = CInt(intCol Mod 26)
                sColName = Convert.ToString(Convert.ToByte(CType("A", Char)) + intFirst)
                sColName += Convert.ToString(Convert.ToByte(CType("A", Char)) + intSecond)
            End If

            Return sColName
        End Function

        Public Function ColNumber(ByVal strCol As String) As Integer
            strCol = strCol.ToUpper
            Dim intColNumber As Integer = 0
            If (strCol.Length > 1) Then
                intColNumber = Convert.ToInt16(Convert.ToByte(strCol.Chars(1)) - 65)
                intColNumber += Convert.ToInt16(Convert.ToByte(strCol.Chars(1)) - 64) * 26

            Else
                intColNumber = Convert.ToInt16(Convert.ToByte(strCol.Chars(0)) - 65)
            End If

            Return intColNumber
        End Function

        Public Function GetExcelSheetNames() As String()
            Dim dt As System.Data.DataTable = Nothing
            Try
                If _oleConn Is Nothing Then
                    Open()
                End If
                dt = _oleConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                If dt Is Nothing Then
                    Return Nothing
                End If
                Dim excelSheets As String() = New String(dt.Rows.Count) {}

                Dim i As Integer = 0
                For Each row As DataRow In dt.Rows
                    Dim strSheetTableName As String = row("TABLE_NAME").ToString()
                    excelSheets(i) = strSheetTableName.Substring(0, strSheetTableName.Length - 1)
                    i = i + 1

                Next
                Return excelSheets
            Catch ex As Exception
                Return Nothing
            Finally
                If Me.KeepConnectionOpen = False Then
                    Close()
                End If
                If Not dt Is Nothing Then
                    dt.Dispose()
                    dt = Nothing
                End If
            End Try
        End Function

        Property ExcelFilename() As String
            Get
                Return _strExcelFilename
            End Get
            Set(ByVal Value As String)
                _strExcelFilename = Value
            End Set
        End Property

        Property SheetName() As String
            Get
                Return _strSheetName
            End Get
            Set(ByVal Value As String)
                _strSheetName = Value
            End Set
        End Property

        Property KeepConnectionOpen() As Boolean
            Get
                Return _blnKeepConnectionOpen
            End Get
            Set(ByVal Value As Boolean)
                _blnKeepConnectionOpen = Value
            End Set
        End Property

        Property SheetRange() As String
            Get
                Return _strSheetRange
            End Get
            Set(ByVal Value As String)
                If Value.IndexOf(":") = -1 Then
                    Throw New Exception("Invalid range length")
                End If
                _strSheetRange = Value
            End Set
        End Property

        Property Headers() As Boolean
            Get
                Return _blnHeaders
            End Get
            Set(ByVal Value As Boolean)
                _blnHeaders = Value
            End Set
        End Property

        Property MixedData() As Boolean
            Get
                Return _blnMixedData
            End Get
            Set(ByVal Value As Boolean)
                _blnMixedData = Value
            End Set
        End Property

#End Region

#Region "Excel Connection"

        Private Function ExcelConnectionOptions() As String
            Dim strOpts As String = String.Empty
            If Me.MixedData = True Then
                strOpts += "Imex=2;"
            End If
            If Me.Headers = True Then
                strOpts += "HDR=Yes;"
            Else
                strOpts += "HDR=No;"
            End If
            Return strOpts
        End Function

        Private Function ExcelConnection() As String
            Return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _strExcelFilename + ";Extended Properties=" + Convert.ToChar(34).ToString() + "Excel 8.0;" + ExcelConnectionOptions() + Convert.ToChar(34).ToString()
        End Function

#End Region

#Region "Close/Open"

        Public Sub Open()
            Try
                If Not (_oleConn Is Nothing) Then
                    If _oleConn.State = ConnectionState.Open Then
                        _oleConn.Close()
                    End If
                    _oleConn = Nothing
                End If
                If System.IO.File.Exists(_strExcelFilename) = False Then
                    Throw New Exception("Excel file " + _strExcelFilename + "could not be found.")
                End If
                _oleConn = New OleDbConnection(ExcelConnection())
                _oleConn.Open()

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub Close()
            If Not _oleConn Is Nothing Then
                If Not _oleConn.State = ConnectionState.Closed Then
                    _oleConn.Close()
                End If
                _oleConn.Dispose()
                _oleConn = Nothing
            End If
        End Sub

#End Region

#Region "Dispose / Destructor"

        Public Sub Dispose() Implements System.IDisposable.Dispose
            If Not _oleConn Is Nothing Then
                _oleConn.Dispose()
                _oleConn = Nothing
            End If

            If Not _oleCmdSelect Is Nothing Then
                _oleCmdSelect.Dispose()
                _oleCmdSelect = Nothing
            End If
        End Sub

#End Region

#Region "Command Select"

        Private Function SetSheetQuerySelect() As Boolean
            Try
                If _oleConn Is Nothing Then
                    Throw New Exception("Connection is unassigned or closed.")
                End If
                If (_strSheetName.Length = 0) Then
                    Throw New Exception("Sheetname was not assigned.")
                End If
                _oleCmdSelect = New OleDbCommand("SELECT * FROM [" + _strSheetName + "$" + _strSheetRange + "]", _oleConn)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "simple utilities"

        Private Function AddWithComma(ByVal strSource As String, ByVal strAdd As String) As String
            If Not strSource = "" Then
                strSource += ", "
            End If
            Return strSource + strAdd
        End Function

        Private Function AddWithAnd(ByVal strSource As String, ByVal strAdd As String) As String
            If Not strSource = "" Then
                strSource += " and "
            End If
            Return strSource + strAdd
        End Function

#End Region

        Private Function SetSheetQueryAdapter(ByVal dt As DataTable) As OleDbDataAdapter
            Try
                If _oleConn Is Nothing Then
                    Throw New Exception("Connection is unassigned or closed.")
                End If
                If (_strSheetName.Length = 0) Then
                    Throw New Exception("Sheetname was not assigned.")
                End If
                If (PKCols Is Nothing) Then
                    Throw New Exception("Cannot update excel sheet with no primarykey set.")
                End If

                If (PKCols.Length < 1) Then
                    Throw New Exception("Cannot update excel sheet with no primarykey set.")
                End If

                Dim oleda As OleDbDataAdapter = New OleDbDataAdapter(_oleCmdSelect)
                Dim strUpdate As String = ""
                Dim strInsertPar As String = ""
                Dim strInsert As String = ""
                Dim strWhere As String = ""
                For iPk As Integer = 0 To PKCols.Length
                    strWhere = AddWithAnd(strWhere, dt.Columns(iPk).ColumnName + "=?")
                Next
                strWhere = " Where " + strWhere
                For iCol As Integer = 0 To dt.Columns.Count
                    strInsert = AddWithComma(strInsert, dt.Columns(iCol).ColumnName)
                    strInsertPar = AddWithComma(strInsertPar, "?")
                    strUpdate = AddWithComma(strUpdate, dt.Columns(iCol).ColumnName) + "=?"
                Next
                Dim strtable As String = "[" + Me.SheetName + "$" + Me.SheetRange + "]"
                strInsert = "INSERT INTO " + strtable + "(" + strInsert + ") Values (" + strInsertPar + ")"
                strUpdate = "Update " + strtable + " Set " + strUpdate + strWhere

                oleda.InsertCommand = New OleDbCommand(strInsert, _oleConn)
                oleda.UpdateCommand = New OleDbCommand(strUpdate, _oleConn)
                Dim oleParIns As OleDbParameter = Nothing
                Dim oleParUpd As OleDbParameter = Nothing
                For iCol As Integer = 0 To dt.Columns.Count
                    oleParIns = New OleDbParameter("?", dt.Columns(iCol).DataType.ToString())
                    oleParUpd = New OleDbParameter("?", dt.Columns(iCol).DataType.ToString())
                    oleParIns.SourceColumn = dt.Columns(iCol).ColumnName
                    oleParUpd.SourceColumn = dt.Columns(iCol).ColumnName
                    oleda.InsertCommand.Parameters.Add(oleParIns)
                    oleda.UpdateCommand.Parameters.Add(oleParUpd)
                    oleParIns = Nothing
                    oleParUpd = Nothing
                Next

                For iPk As Integer = 0 To PKCols.Length
                    oleParUpd = New OleDbParameter("?", dt.Columns(iPK).DataType.ToString())
                    oleParUpd.SourceColumn = dt.Columns(iPK).ColumnName
                    oleParUpd.SourceVersion = DataRowVersion.Original
                    oleda.UpdateCommand.Parameters.Add(oleParUpd)
                Next

                Return oleda
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#Region "command Singe Value Update"

        Private Function SetSheetQuerySingelValUpdate(ByVal strVal As String) As Boolean
            Try
                If _oleConn Is Nothing Then
                    Throw New Exception("Connection is unassigned or closed.")
                End If
                If _strSheetName.Length = 0 Then
                    Throw New Exception("Sheetname was not assigned.")
                End If
                _oleCmdUpdate = New OleDbCommand(" Update [" + _strSheetName + "$" + _strSheetRange + "] set F1=" + strVal, _oleConn)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

        Public Sub SetPrimaryKey(ByVal intCol As Integer)
            _PKCol = New Integer() {intCol}
        End Sub

        Public Function GetTable() As DataTable
            Return GetTable("ExcelTable")
        End Function

        Private Sub SetPrimaryKey(ByVal dt As DataTable)
            Try
                If Not PKCols Is Nothing Then
                    If PKCols.Length > 0 Then
                        Dim dc As DataColumn()
                        For i As Integer = 0 To PKCols.Length
                            dc(i) = dt.Columns(PKCols(i))
                        Next
                        dt.PrimaryKey = dc
                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function GetTable(ByVal strTableName As String) As DataTable
            Try
                If _oleConn Is Nothing Then
                    Open()
                End If
                If Not _oleConn.State = ConnectionState.Open Then
                    Throw New Exception("Connection cannot open error.")
                End If
                If SetSheetQuerySelect() = False Then
                    Return Nothing
                End If
                Dim oleAdapter As OleDbDataAdapter = New OleDbDataAdapter
                oleAdapter.SelectCommand = _oleCmdSelect
                Dim dt As DataTable = New DataTable(strTableName)
                oleAdapter.FillSchema(dt, SchemaType.Source)
                oleAdapter.Fill(dt)
                If Me.Headers = True Then
                    If (_strSheetRange.IndexOf(":") > 0) Then
                        Dim FirstCol As String = _strSheetRange.Substring(0, _strSheetRange.IndexOf(":") - 1)
                        Dim intCol As Integer = Me.ColNumber(FirstCol)
                        For intI As Integer = 0 To dt.Columns.Count - 1
                            dt.Columns(intI).Caption = ColName(intCol + intI)
                        Next
                    End If
                End If
                SetPrimaryKey(dt)
                dt.DefaultView.AllowDelete = False
                _oleCmdSelect.Dispose()
                _oleCmdSelect = Nothing
                oleAdapter.Dispose()
                oleAdapter = Nothing
                If (KeepConnectionOpen = False) Then
                    Close()
                End If
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Private Sub CheckPKExists(ByVal dt As DataTable)
            If (dt.PrimaryKey.Length = 0) Then
                If Not Me.PKCols Is Nothing Then
                    SetPrimaryKey(dt)
                Else
                    Throw New Exception("Provide an primary key to the datatable")
                End If
            End If
        End Sub

        Public Function SetTable(ByVal dt As DataTable) As DataTable
            Dim dtChanges As DataTable = dt.GetChanges
            If dtChanges Is Nothing Then
                Throw New Exception("There are no changes to be saved!")
            End If
            CheckPKExists(dt)
            If _oleConn Is Nothing Then
                Open()
            End If
            If Not _oleConn.State = ConnectionState.Open Then
                Throw New Exception("Connection cannot open error.")
            End If
            If SetSheetQuerySelect() = False Then
                Return Nothing
            End If
            Dim oleAdapter As OleDbDataAdapter = SetSheetQueryAdapter(dtChanges)
            oleAdapter.Update(dtChanges)

            _oleCmdSelect.Dispose()
            _oleCmdSelect = Nothing
            oleAdapter.Dispose()
            oleAdapter = Nothing
            If KeepConnectionOpen = False Then
                Close()
            End If
            Return dt
        End Function

#Region "Get/Set Single Value"

        Public Sub SetSingleCellRange(ByVal strCell As String)
            _strSheetRange = strCell + ":" + strCell
        End Sub

        Public Function GetValue(ByVal strCell As String) As Object
            SetSingleCellRange(strCell)
            Dim objValue As Object = Nothing
            If _oleConn Is Nothing Then
                Open()
            End If
            If Not _oleConn.State = ConnectionState.Open Then
                Throw New Exception("Connection is not open error.")
            End If
            If SetSheetQuerySelect() = False Then
                Return Nothing
            End If
            objValue = _oleCmdSelect.ExecuteScalar
            _oleCmdSelect.Dispose()
            _oleCmdSelect = Nothing
            If KeepConnectionOpen = False Then
                Close()
            End If
            Return objValue

        End Function

        Public Sub SetValue(ByVal strCell As String, ByVal objValue As Object)
            Try
                SetSingleCellRange(strCell)
                If _oleConn Is Nothing Then
                    Open()
                End If
                If Not _oleConn.State = ConnectionState.Open Then
                    Throw New Exception("Connection is not open error.")
                End If
                If SetSheetQuerySingelValUpdate(objValue.ToString() = False) Then
                    Return
                End If
                objValue = _oleCmdUpdate.ExecuteNonQuery()
                _oleCmdUpdate.Dispose()
                _oleCmdUpdate = Nothing
                If KeepConnectionOpen = False Then
                    Close()
                End If
            Catch ex As Exception
                Throw ex
            Finally
                If Not _oleCmdUpdate Is Nothing Then
                    _oleCmdUpdate.Dispose()
                    _oleCmdUpdate = Nothing
                End If
            End Try
        End Sub

#End Region

    End Class

End Namespace