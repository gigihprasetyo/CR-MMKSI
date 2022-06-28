#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Text

#End Region

Namespace KTB.DNet.Parser
    Public Class SparePartMasterAltParser
        Inherits AbstractParser

#Region "Private Variables"
        Private sparePartMasterList As ArrayList
        Private Grammar As Regex
        Private errorMessage As StringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Dim lines As String() = MyBase.GetLines(Content)
            Dim ind As String
            Dim line As String
            Dim sparePartMaster As SparePartMaster = Nothing
            sparePartMasterList = New ArrayList


            For i As Integer = 0 To lines.Length - 1
                Try
                    line = lines(i)

                    ind = line.Split(MyBase.ColSeparator)(0)
                    If ind = MyBase.IndicatorHeader Then
                        sparePartMaster = Nothing
                        errorMessage = New StringBuilder()
                        ' create objek spare part master
                        sparePartMaster = ParseHeader(line)
                        ' insert to array objek spare part master
                        If Not IsNothing(sparePartMaster) Then
                            If errorMessage.ToString() <> String.Empty Then
                                sparePartMaster.ErrorMessage = errorMessage.ToString()
                            End If

                            sparePartMasterList.Add(sparePartMaster)
                        End If
                    End If

                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "SparePartParser.vb", "Parsing", KTB.DNet.Lib.DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPMasterParser, BlockName)
                    Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")

                End Try
            Next
            Return sparePartMasterList
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim sparePartMasterFacade As New SparePartMasterFacade(user)
            Dim sparePartMasterListUpdate As New ArrayList

            ' loop sparePartMaster array
            For Each sparePartMaster As SparePartMaster In sparePartMasterList
                Try
                    If sparePartMaster.ErrorMessage = String.Empty AndAlso IsNothing(sparePartMaster.ErrorMessage) Then
                        If sparePartMaster.ID <> 0 Then
                            'update Spare Part Master
                            sparePartMasterListUpdate.Add(sparePartMaster)
                        End If

                        If sparePartMasterFacade.BatchUpdateWithTransactionManager(sparePartMasterListUpdate) < 0 Then
                            nError += 1
                        End If

                    Else
                        nError += 1
                        sMsg &= sparePartMaster.ErrorMessage & ";"
                    End If

                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & sparePartMasterList.Count.ToString(), "ws-worker", "SparePartMasterAltParser.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartPriceParser, BlockName)

                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "SparePartMasterAltParser.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartPriceParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Return Nothing
        End Function

#End Region

#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String) As SparePartMaster
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim sparePartMaster As New SparePartMaster

            Dim PDCode As String
            Dim UPTCode As Boolean = False

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else

                ' Spare Part Number
                ' 1 Spare Part Code / H-1
                PDCode = cols(1).Trim
                If PDCode = String.Empty Then
                    writeError("Spare Part Number can't be empty")
                Else
                    Dim sparePartMasterCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    sparePartMasterCriteria.opAnd(New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.Exact, PDCode))
                    Dim sparePartMasterList As ArrayList = New SparePartMasterFacade(user).Retrieve(sparePartMasterCriteria)

                    If sparePartMasterList.Count > 1 Then
                        writeError("More Than 1 Spare Part Master for the same code Part Number:" & PDCode)
                    ElseIf sparePartMasterList.Count = 1 Then
                        sparePartMaster = sparePartMasterList(0)
                        sparePartMaster.PartNumberReff = PDCode
                    ElseIf sparePartMasterList.Count = 0 Then
                        writeError("Spare Part Master Part Number Not Found :" & PDCode)
                    End If
                End If

                If (sparePartMaster.ID > 0) Then
                    ' Part Name
                    ' Part Name / H-2
                    'PDCode = cols(2).Trim
                    'If PDCode = String.Empty Then
                    '    writeError("Part Name can't be empty")
                    'Else
                    '    If (sparePartMaster.PartName <> PDCode) Then
                    '        UPTCode = True
                    '    End If
                    '    sparePartMaster.PartName = PDCode
                    'End If
                    'Alt Part Number
                    PDCode = cols(3).Trim
                    If PDCode = String.Empty Then
                        writeError("Alt Part Number can't be empty")
                    Else
                        If (sparePartMaster.AltPartNumber <> PDCode) Then
                            UPTCode = True
                        End If
                        sparePartMaster.AltPartNumber = PDCode
                    End If
                    'Alt Part Name
                    PDCode = cols(4).Trim
                    If PDCode = String.Empty Then
                        writeError("Alt Part Name can't be empty")
                    Else
                        If (sparePartMaster.AltPartName <> PDCode) Then
                            UPTCode = True
                        End If
                        sparePartMaster.AltPartName = PDCode
                    End If

                    If (UPTCode) Then
                        sparePartMaster.LastUpdateBy = user.Identity.Name
                        sparePartMaster.LastUpdateTime = DateTime.Now
                    Else
                        sparePartMaster.LastUpdateBy = "Not Update"
                    End If

                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    sparePartMaster.ErrorMessage = errorMessage.ToString() & vbCrLf & line

                End If
            End If

            Return sparePartMaster

        End Function

#End Region

#Region "Public Properties"



#End Region

    End Class

End Namespace
