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
    Public Class SparePartPriceParser
        Inherits AbstractParser

#Region "Private Variables"
        Private sparePartMasterPriceList As ArrayList
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
            Dim sparePartMasterPrice As SparePartMasterPrice = Nothing
            sparePartMasterPriceList = New ArrayList

            For i As Integer = 0 To lines.Length - 1
                Try
                    line = lines(i)
                    ind = line.Split(MyBase.ColSeparator)(0)
                    If ind = MyBase.IndicatorHeader Then
                        sparePartMasterPrice = Nothing
                        errorMessage = New StringBuilder()
                        ' create objek spare part master
                        sparePartMasterPrice = ParseHeader(line)
                        ' insert to array objek spare part master
                        If Not IsNothing(sparePartMasterPrice) Then
                            If errorMessage.ToString() <> String.Empty Then
                                sparePartMasterPrice.ErrorMessage = errorMessage.ToString()
                            End If
                            sparePartMasterPriceList.Add(sparePartMasterPrice)
                        End If
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "SparePartPriceParser.vb", "Parsing", KTB.DNet.Lib.DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartPriceParser, BlockName)
                    Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next
            Return sparePartMasterPriceList
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim sparePartMasterPriceFacade As New SparePartMasterPriceFacade(user)
            Dim sparePartMasterPriceListInsertOrUpdate As New ArrayList

            ' loop sparePartMaster array
            For Each SPPrice As SparePartMasterPrice In sparePartMasterPriceList
                Try
                    If IsNothing(SPPrice.ErrorMessage) OrElse SPPrice.ErrorMessage = String.Empty Then
                        Dim PartPriceCrit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        'PartPriceCrit.opAnd(New Criteria(GetType(SparePartMasterPrice), "ValidFrom", MatchType.Exact, SPPrice.ValidFrom))
                        PartPriceCrit.opAnd(New Criteria(GetType(SparePartMasterPrice), "SparePartMaster.ID", MatchType.Exact, SPPrice.SparePartMaster.ID))
                        Dim sparePartMasterList As ArrayList = New SparePartMasterPriceFacade(user).Retrieve(PartPriceCrit)

                        If sparePartMasterList.Count > 0 Then
                            Dim idx As Integer = 0
                            Dim isUpdate As Boolean = False
                            For Each oSPPrice As SparePartMasterPrice In sparePartMasterList
                                If oSPPrice.ValidFrom = SPPrice.ValidFrom Then
                                    isUpdate = True
                                    Exit For
                                End If
                                idx += 1
                            Next
                            Dim res As Integer = 0
                            If isUpdate Then
                                Dim oldSPPrice As SparePartMasterPrice = CType(sparePartMasterList(idx), SparePartMasterPrice)
                                oldSPPrice.ValidFrom = SPPrice.ValidFrom
                                oldSPPrice.RetailPrice = SPPrice.RetailPrice
                                oldSPPrice.ValidTo = SPPrice.ValidTo
                                res = sparePartMasterPriceFacade.Update(oldSPPrice)
                            Else
                                res = sparePartMasterPriceFacade.Insert(SPPrice)
                            End If

                            If res < 0 Then
                                nError += 1
                            Else
                                If SPPrice.ValidFrom <= Date.Now Then
                                    Dim result As Boolean = False
                                    result = New SparePartMasterPriceFacade(user).UpdateSparePartMaster(SPPrice.SparePartMaster.ID, SPPrice.RetailPrice, user.Identity.Name, SPPrice.ValidFrom)
                                End If
                            End If
                        Else
                            Dim res As Integer = sparePartMasterPriceFacade.Insert(SPPrice)
                            If res < 0 Then
                                nError += 1
                            Else
                                If SPPrice.ValidFrom <= Date.Now Then
                                    Dim result As Boolean = False
                                    result = New SparePartMasterPriceFacade(user).UpdateSparePartMaster(SPPrice.SparePartMaster.ID, SPPrice.RetailPrice, user.Identity.Name, SPPrice.ValidFrom)
                                End If
                            End If
                        End If

                        'If sparePartMasterPrice.IsNotChange = False Then
                        '    If sparePartMasterPriceFacade.BatchInsertOrUpdateWithTransactionManager(sparePartMasterPriceListInsertOrUpdate) < 0 Then
                        '        nError += 1
                        '    End If
                        'End If
                    Else
                        nError += 1
                        sMsg &= SPPrice.ErrorMessage & ";"
                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & sparePartMasterPriceList.Count.ToString(), "ws-worker", "SparePartPriceParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartPriceParser, BlockName)

                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "SparePartPriceParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartPriceParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As SparePartMasterPrice
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim sparePartMasterPrice As New SparePartMasterPrice

            Dim PDCode As String

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                ' Spare Part Number
                ' 1 Spare Part Code / H-1
                PDCode = cols(1).Trim
                If PDCode = String.Empty Then
                    writeError("Spare Part Code can't be empty")
                Else
                    Dim sparePartMasterCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    sparePartMasterCriteria.opAnd(New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.Exact, PDCode))
                    Dim sparePartMasterList As ArrayList = New SparePartMasterFacade(user).Retrieve(sparePartMasterCriteria)

                    If sparePartMasterList.Count > 1 Then
                        writeError("More Than 1 Spare Part Master for the same code:" & PDCode)
                    ElseIf sparePartMasterList.Count = 1 Then
                        sparePartMasterPrice.SparePartMaster = sparePartMasterList(0)
                        'sparePartMaster.PartNumberReff = PDCode
                    ElseIf sparePartMasterList.Count = 0 Then
                        writeError("Spare Part Master Not Found :" & PDCode)
                        'sparePartMaster = New SparePartMaster
                        'sparePartMaster.MarkLoaded()
                        'sparePartMaster.PartNumber = PDCode
                        'sparePartMaster.PartNumberReff = PDCode
                    End If
                End If
                ' Retail Price
                ' Retail Price / H-2
                PDCode = cols(2).Trim
                If PDCode = String.Empty Then
                    writeError("Retail Price can't be empty")
                Else
                    If isValidNumeric(PDCode) Then
                        sparePartMasterPrice.RetailPrice = PDCode
                    Else
                        writeError("Retail Price normal not valid")
                    End If
                End If

                ' Valid From
                PDCode = cols(3).Trim
                If PDCode = String.Empty Then
                    writeError("Valid From can't be empty")
                Else
                    sparePartMasterPrice.ValidFrom = MyBase.GetDateShort(PDCode)
                End If

                ' Valid To
                PDCode = cols(4).Trim
                If PDCode = String.Empty Then
                    writeError("Valid To Code can't be empty")
                Else
                    sparePartMasterPrice.ValidTo = MyBase.GetDateShort(PDCode)
                End If

                sparePartMasterPrice.Status = 0

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    sparePartMasterPrice.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                End If
            End If

            Return sparePartMasterPrice

        End Function

        Private Function isValidNumeric(ByVal stemp As String) As Boolean
            '-- Validate numeric field.
            '-- If stemp is a numeric and its value >= 0 then return True else return False
            Try
                Dim x As Long = CLng(stemp)
                If x >= 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

#End Region

#Region "Public Properties"



#End Region

    End Class

End Namespace
