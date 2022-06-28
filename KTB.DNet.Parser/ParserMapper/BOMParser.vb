 

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Security.Principal
#End Region

Namespace KTB.DNet.Parser
    Public Class BOMParser
        Inherits AbstractParser

#Region "Private Variables"
        Private stream As StreamReader
        Private grammar As Regex
        Private BOMHeaders As ArrayList
        Private BOMDetails As ArrayList
        Private _fileName As String
        Private _BOMHeader As HeaderBOM 'Header
        Private _BOMDetail As DetailBOM
        Private _bomMaintenanceFacade As BOMMaintenanceFacade
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            Dim strTransactionMessage As String = " :  sudah dipergunakan dalam Transaksi"
            Try

                _fileName = fileName
                Dim val As String
                BOMHeaders = New ArrayList
                stream = New StreamReader(fileName, True)
                val = MyBase.NextLine(stream).Trim()
                While (Not val = "")
                    Try
                        Dim indikator As String = val.Substring(0, 1).ToUpper
                        If indikator.Equals("H") Then
                            If Not _BOMHeader Is Nothing Then
                                _bomMaintenanceFacade = New BOMMaintenanceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                                If _bomMaintenanceFacade.IsBOMUsedTransaction(_BOMHeader.EquipmentNumber) Then
                                    _BOMHeader.ErrorMessage = _BOMHeader.ErrorMessage & Chr(13) & Chr(10)
                                    _BOMHeader.ErrorMessage = _BOMHeader.EquipmentNumber & strTransactionMessage
                                End If
                                BOMHeaders.Add(_BOMHeader)   'customer header input text
                            End If
                            _BOMHeader = ParseBOMHeader(val + delimited)
                        Else
                            If Not _BOMHeader Is Nothing Then
                                ParseBOMDetail(val + delimited, _BOMHeader)  'Order detail input
                            End If
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "BOMParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.BOMParser, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _BOMHeader = Nothing
                    End Try

                    val = MyBase.NextLine(stream)

                End While
                If Not _BOMHeader Is Nothing Then
                    _bomMaintenanceFacade = New BOMMaintenanceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                    If _bomMaintenanceFacade.IsBOMUsedTransaction(_BOMHeader.EquipmentNumber) Then
                        _BOMHeader.ErrorMessage = _BOMHeader.ErrorMessage & Chr(13) & Chr(10)
                        _BOMHeader.ErrorMessage = _BOMHeader.EquipmentNumber & strTransactionMessage
                    End If
                    BOMHeaders.Add(_BOMHeader)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                stream.Close()
                stream = Nothing
            End Try
            Return BOMHeaders

        End Function

       

        'Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
        '    Try
        '        _fileName = fileName
        '        Dim val As String
        '        Dim sStart As Integer
        '        Dim nCount As Integer
        '        Dim sTemp As String
        '        BOMHeaders = New ArrayList

        '        stream = New StreamReader(fileName, True)
        '        val = MyBase.NextLine(stream).Trim()
        '        While (Not val = "")
        '            Try
        '                sStart = 0
        '                nCount = 0
        '                For Each m As Match In grammar.Matches(val)
        '                    sTemp = val.Substring(sStart, m.Index - sStart)
        '                    sTemp = sTemp.Trim("""")
        '                    If (nCount = 0) Then
        '                        Select Case (nCount)
        '                            Case Is = 0
        '                                If sTemp.Equals("H") Then
        '                                    If Not _BOMHeader Is Nothing Then
        '                                        BOMHeaders.Add(_BOMHeader)   'customer header input text
        '                                    End If
        '                                    _BOMHeader = ParseBOMHeader(val + delimited)
        '                                Else
        '                                    If Not _BOMHeader Is Nothing Then
        '                                        ParseBOMDetail(val + delimited, _BOMHeader)  'Order detail input
        '                                    End If
        '                                End If
        '                            Case Else
        '                                'Do Nothing
        '                        End Select
        '                    End If
        '                    nCount += 1
        '                Next

        '            Catch ex As Exception
        '                Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
        '                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
        '                _BOMHeader = Nothing
        '            End Try

        '            val = MyBase.NextLine(stream)

        '        End While

        '        If Not _BOMHeader Is Nothing Then
        '            BOMHeaders.Add(_BOMHeader)
        '        End If
        '    Catch ex As Exception
        '        Throw ex
        '    Finally
        '        stream.Close()
        '        stream = Nothing
        '    End Try
        '    Return BOMHeaders

        'End Function

        Protected Overrides Function DoTransaction() As Integer
            If BOMHeaders.Count > 0 Then
                InsertCollection(BOMHeaders)
            Else
                Throw New Exception("Parsing file : " & _fileName & " with 0 record ")
            End If
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Sub InsertCollection(ByVal BOMCollection As ArrayList)
            Dim bomFacade As BOMMaintenanceFacade
            For Each item As HeaderBOM In BOMCollection
                If isValidBOM(item) Then
                    bomFacade = New BOMMaintenanceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                    Try
                        bomFacade.Update(item)
                    Catch ex As Exception
                        Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.EquipmentNumber & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    End Try
                Else
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.EquipmentNumber & Chr(13) & Chr(10) & "Duplicate Data")
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End If
            Next
        End Sub

        Private Function isValidBOM(ByVal bom As HeaderBOM) As Boolean
            Dim _result As Boolean = True
            Dim i As Integer

            If Not bom.ErrorMessage Is Nothing Then
                If bom.ErrorMessage.Length > 0 Then
                    _result = False
                End If
            Else
                If bom.DetailBOMs.Count > 0 Then
                    For Each item As DetailBOM In bom.DetailBOMs
                        If Not item.ErrorMessage Is Nothing Then
                            If item.ErrorMessage.Length > 0 Then
                                 _result = False
                            End If
                        End If
                        i = 0
                        For Each items As DetailBOM In bom.DetailBOMs
                            If (Not item.EquipmentMaster Is Nothing) And (Not items.EquipmentMaster Is Nothing) Then
                                If item.EquipmentMaster.EquipmentNumber = items.EquipmentMaster.EquipmentNumber Then
                                     i += 1
                                    If i > 1 Then
                                        item.ErrorMessage = " Duplikasi Equipment Number"
                                        _result = False
                                    End If
                                End If
                            End If
                        Next
                    Next
                End If
            End If
            Return _result
        End Function

        Private Function isValidBOM(ByVal collBom As ArrayList) As Boolean
            Dim _result As Boolean = True
            Dim i As Integer
            For Each bom As HeaderBOM In collBom
                If Not bom.ErrorMessage Is Nothing Then
                    If bom.ErrorMessage.Length > 0 Then
                        'Return False
                        _result = False
                    End If
                Else
                    If bom.DetailBOMs.Count > 0 Then
                        For Each item As DetailBOM In bom.DetailBOMs
                            If Not item.ErrorMessage Is Nothing Then
                                If item.ErrorMessage.Length > 0 Then
                                    'Return False
                                    _result = False
                                End If
                            End If
                            i = 0
                            For Each items As DetailBOM In bom.DetailBOMs
                                If (Not item.EquipmentMaster Is Nothing) And (Not items.EquipmentMaster Is Nothing) Then
                                    If item.EquipmentMaster.EquipmentNumber = items.EquipmentMaster.EquipmentNumber Then
                                        'Return False
                                        i += 1
                                        If i > 1 Then
                                            item.ErrorMessage = " Duplikasi Equipment Number"
                                            _result = False
                                        End If
                                    End If
                                End If

                            Next
                        Next
                    End If
                End If
            Next
            Return _result
        End Function

        Private Function ParseBOMHeader(ByVal ValParser As String) As HeaderBOM
            _BOMHeader = New HeaderBOM
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0

            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 1
                        If sTemp.Trim.Length > 0 Then
                            Dim eqNumber As String = sTemp
                            Dim eqMaster As EquipmentMaster = RetrieveEquipmentMaster(eqNumber)
                            If eqMaster.ID > 0 Then
                                _BOMHeader.EquipmentMaster = eqMaster
                            Else
                                _BOMHeader.ErrorMessage = sTemp & " Equipment Tidak ketemu "
                            End If
                        Else
                            _BOMHeader.ErrorMessage = sTemp & " Equipment Tidak valid "
                        End If

                    Case Else
                        'do nothing
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            Return _BOMHeader
        End Function

        Private Sub ParseBOMDetail(ByVal ValParser As String, ByVal _objBOMHeader As HeaderBOM)
            _BOMDetail = New DetailBOM
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0

            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 1
                        If sTemp.Trim.Length > 0 Then
                            Dim eqNumber As String = sTemp
                            Dim eqMaster As EquipmentMaster = RetrieveEquipmentMaster(eqNumber)
                            If eqMaster.ID > 0 Then
                                _BOMDetail.EquipmentMaster = eqMaster
                            Else
                                _BOMDetail.ErrorMessage = sTemp & " Equipment Tidak ketemu "
                            End If
                        Else
                            _BOMDetail.ErrorMessage = sTemp & " Equipment Tidak valid "
                        End If
                    Case Is = 2
                        Try
                            Dim qty As Integer = Convert.ToInt64(sTemp)
                            _BOMDetail.Quantity = qty
                            If _BOMDetail.Quantity < 1 Then
                                Throw New Exception("Quantity tidak valid")
                            End If
                        Catch ex As Exception
                            If Not _BOMDetail.ErrorMessage Is Nothing Then
                                If _BOMDetail.ErrorMessage.Length > 0 Then
                                    _BOMDetail.ErrorMessage = _BOMDetail.ErrorMessage & " - " & sTemp & " Quantity tidak valid "
                                End If
                            Else
                                _BOMDetail.ErrorMessage = sTemp & " Quantity tidak valid "
                            End If
                        End Try

                    Case Else
                        'Do Nothing else
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            _objBOMHeader.DetailBOMs.Add(_BOMDetail)
        End Sub

        Private Function RetrieveEquipmentMaster(ByVal code As String) As EquipmentMaster
            Dim eqFacade As EquipmentMasterFacade = New EquipmentMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim eqMaster As EquipmentMaster = eqFacade.Retrieve(code)
            Return eqMaster
        End Function

#End Region

    End Class

End Namespace