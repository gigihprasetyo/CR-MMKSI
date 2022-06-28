#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Linq
Imports System.Collections.Generic
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.Parser

    Public Class SparePartPackingParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _aPackingHs As ArrayList
        Private _aPackingHs2 As New ArrayList
        Private _oPackingH As SparePartPacking
        Private _oPackingD As SparePartPackingDetail
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try
                Dim lines As String() = MyBase.GetLines(Content)
                Dim ind As String
                Dim line As String

                _aPackingHs = New ArrayList()
                _oPackingH = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(_oPackingH) Then
                                _aPackingHs.Add(_oPackingH)
                                _oPackingH = Nothing
                            End If

                            errorMessage = New StringBuilder()
                            _oPackingH = ParseHeader(line)
                        ElseIf ind = MyBase.IndicatorDetail Then
                            If IsNothing(_oPackingH) OrElse Not IsNothing(_oPackingH.ErrorMessage) Then
                            Else
                                _oPackingD = ParseDetail(line)

                                If Not IsNothing(_oPackingD) Then
                                    _oPackingD.SparePartPacking = _oPackingH
                                    _oPackingH.SparePartPackingDetails.Add(_oPackingD)
                                    If Not IsNothing(_oPackingD.ErrorMessage) AndAlso _oPackingD.ErrorMessage.Trim <> String.Empty Then
                                        _oPackingH.ErrorMessage = _oPackingH.ErrorMessage & ";" & _oPackingD.ErrorMessage
                                    End If
                                Else
                                    ''remakrks untuk do yang nggak ada   di dkip
                                    'If Not IsNothing(_oPackingD.ErrorMessage) Then
                                    '    _oPackingH.ErrorMessage = _oPackingH.ErrorMessage & ";" & "there are Packing not found"
                                    'End If
                                    'Exit For ' gak ketemua packing (DO) langsung keluar
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "SparePartPackingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartPackingParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _oPackingH = Nothing
                    End Try
                Next

                If Not IsNothing(_oPackingH) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _oPackingH.ErrorMessage = errorMessage.ToString()
                    _aPackingHs.Add(_oPackingH)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Dim aDatas As New ArrayList
            Dim nError As Integer = 0
            Dim sMsg As String = ""

            For i As Integer = 0 To _aPackingHs.Count - 1
                _oPackingH = CType(_aPackingHs(i), SparePartPacking)
                If Not IsNothing(_oPackingH.ErrorMessage) AndAlso _oPackingH.ErrorMessage.Trim() <> String.Empty Then
                    If nError = 0 Then
                        SysLogParameter.LogErrorToSyslog("Start : Log Error of SparePartPackingParser", "ws-worker", "SparePartPackingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartPackingParser, BlockName)
                    End If
                    sMsg = sMsg & _oPackingH.ErrorMessage.Trim() & ";"
                    nError += 1
                    SysLogParameter.LogErrorToSyslog(_oPackingH.ErrorMessage, "ws-worker", "SparePartPackingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartPackingParser, BlockName)
                    'Else
                    'aDatas.Add(_oPackingH)
                End If
                aDatas.Add(_oPackingH)
            Next
            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Number of Error " & nError.ToString() & " of " & _aPackingHs.Count.ToString() & " Data", "ws-worker", "SparePartPackingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartPackingParser, BlockName)
                SysLogParameter.LogErrorToSyslog("End : Log Error of SparePartPackingParser", "ws-worker", "SparePartPackingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartPackingParser, BlockName)

                Dim e As Exception = New Exception("Number of Error " & nError.ToString() & " of " & _aPackingHs.Count.ToString() & " Data. Message : " & sMsg)
                'Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            _aPackingHs = New ArrayList()
            _aPackingHs = aDatas

            Return _aPackingHs
        End Function

        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer

            Dim doFacade As SparePartPackingFacade = New SparePartPackingFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            _aPackingHs2 = New ArrayList()

            ' hapus packing existing untuk DO yang dimaksud
            Try
                Dim packingDetailFacade As SparePartPackingDetailFacade = New SparePartPackingDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                For Each objSparePartPacking As SparePartPacking In _aPackingHs
                    If objSparePartPacking.SparePartPackingDetails.Count > 0 Then
                        Dim strSql As String = "select ID from SparePartPacking where InternalHUNo = '" + objSparePartPacking.InternalHUNo + "'"

                        For Each detail As SparePartPackingDetail In objSparePartPacking.SparePartPackingDetails
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPackingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPackingDetail), "SparePartDO.ID", MatchType.Exact, detail.SparePartDO.ID))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPackingDetail), "SparePartPacking.ID", MatchType.InSet, "(" & strSql & ")"))

                            Dim arlExisting As ArrayList = packingDetailFacade.Retrieve(criterias)
                            If arlExisting.Count > 0 Then
                                Dim obSparePartPackingDetailToDelete As SparePartPackingDetail = New SparePartPackingDetail
                                obSparePartPackingDetailToDelete = CType(arlExisting(0), SparePartPackingDetail)
                                If Not IsNothing(obSparePartPackingDetailToDelete) AndAlso obSparePartPackingDetailToDelete.ID > 0 Then
                                    'doFacade.DeleteFromWebSevice(obSparePartPackingDetailToDelete.SparePartPacking)

                                    '-- start changes
                                    If Not IsNothing(obSparePartPackingDetailToDelete.SparePartPacking) Then
                                        If Not IsNothing(obSparePartPackingDetailToDelete.SparePartPacking.SparePartDOExpedition) Then
                                            _aPackingHs2.Add(obSparePartPackingDetailToDelete.SparePartPacking)
                                        Else
                                            Dim strSql2 As String = "select ID from SparePartPacking where InternalHUNo = '" + objSparePartPacking.InternalHUNo + "' and SparePartDOExpeditionID is not null"
                                            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPacking), "RowStatus", MatchType.Exact, CType(DBRowStatus.Deleted, Short)))
                                            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPacking), "ID", MatchType.InSet, "(" & strSql2 & ")"))

                                            '-- Sorted by
                                            Dim sortColl As SortCollection = New SortCollection
                                            sortColl.Add(New Sort(GetType(SparePartPacking), "ID", Sort.SortDirection.DESC))

                                            Dim arlSPPacking As ArrayList = doFacade.Retrieve(criterias2, sortColl)
                                            If Not IsNothing(arlSPPacking) AndAlso arlSPPacking.Count > 0 Then
                                                Dim oSparePartPacking As SparePartPacking = CType(arlSPPacking(0), SparePartPacking)
                                                _aPackingHs2.Add(oSparePartPacking)
                                            End If
                                        End If
                                    End If
                                    '-- end changes

                                    doFacade.Delete(obSparePartPackingDetailToDelete.SparePartPacking)
                                    For Each detailDel As SparePartPackingDetail In obSparePartPackingDetailToDelete.SparePartPacking.SparePartPackingDetails
                                        packingDetailFacade.Delete(detailDel)
                                    Next
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                Next

            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog("Error on delete SparePartPacking detail and header " & ex.Message.ToString(), "ws-worker", "SparePartPackingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartPackingParser, BlockName)
                Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & ex.Message & " error on delete SparePartPacking detail and header")
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
            End Try

            ' Add new packing
            For Each objSparePartPacking As SparePartPacking In _aPackingHs
                '--- start changes
                Dim objsPP As SparePartPacking = (From sPP As SparePartPacking In _aPackingHs2
                                                    Where sPP.InternalHUNo = objSparePartPacking.InternalHUNo And sPP.SparePartDOExpedition IsNot Nothing
                                                    Select sPP).FirstOrDefault()
                If Not IsNothing(objsPP) AndAlso objsPP.ID > 0 Then
                    objSparePartPacking.SparePartDOExpedition = objsPP.SparePartDOExpedition
                End If
                '-- end changes

                Try
                    If Not IsNothing(objSparePartPacking.ErrorMessage) AndAlso objSparePartPacking.ErrorMessage <> "" Then
                        nError += 1
                        sMsg = sMsg & objSparePartPacking.ErrorMessage.ToString() & ";"
                    Else
                        doFacade = New SparePartPackingFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        doFacade.InsertFromWebSevice(objSparePartPacking)
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "SparePartPackingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objSparePartPacking.ID.ToString & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _aPackingHs.Count.ToString(), "ws-worker", "SparePartPackingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "SparePartPackingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
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

        Private Function ParseHeader(ByVal line As String) As SparePartPacking
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objSparePartPacking As New SparePartPacking
            Dim objSparePartPackingFac As New SparePartPackingFacade(user)


            errorMessage = New StringBuilder()
            If cols.Length <> 9 Then
                writeError("Invalid Spare Part Packing Header Format")
            Else
                'Internal HU Number
                If cols(1).Trim <> String.Empty Then
                    objSparePartPacking.InternalHUNo = cols(1).Trim
                End If

                'Packing Material
                If cols(2).Trim = String.Empty Then
                    writeError("Packing Material can't be empty")
                Else
                    objSparePartPacking.PackMaterial = cols(2).Trim
                End If

                'Packing Material Desc
                If cols(3).Trim <> String.Empty Then
                    objSparePartPacking.PackMaterialDesc = cols(3).Trim
                End If

                'LotCase
                If cols(4).Trim <> String.Empty Then
                    objSparePartPacking.LotCase = cols(4).Trim
                End If

                'Total Berat
                If cols(5).Trim <> String.Empty Then
                    objSparePartPacking.Weight = MyBase.GetCurrency(GetCurrencyByCurrentCulture(cols(5).Trim)) 'CType(cols(5).Trim, Decimal)
                End If

                'Total Volume
                If cols(6).Trim <> String.Empty Then
                    objSparePartPacking.Volume = MyBase.GetCurrency(GetCurrencyByCurrentCulture(cols(6).Trim)) 'CType(cols(6).Trim, Decimal)
                End If

                'Total Item
                If cols(7).Trim <> String.Empty Then
                    objSparePartPacking.TotalItem = MyBase.GetCurrency(GetCurrencyByCurrentCulture(cols(7).Trim)) 'CType(cols(7).Trim, Decimal)
                End If

                'Total Qty
                If cols(8).Trim <> String.Empty Then
                    objSparePartPacking.TotalQty = MyBase.GetCurrency(GetCurrencyByCurrentCulture(cols(8).Trim)) 'CType(cols(8).Trim, Decimal)
                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    If IsNothing(objSparePartPacking) Then objSparePartPacking = New SparePartPacking()
                    objSparePartPacking.ErrorMessage = errorMessage.ToString()
                Else
                    objSparePartPacking.CreatedBy = "SAP"
                End If
            End If

            Return objSparePartPacking
        End Function

        Private Function ParseDetail(ByVal line As String) As SparePartPackingDetail
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim cols As String() = line.Split(MyBase.ColSeparator)

            _oPackingD = New SparePartPackingDetail

            If (cols.Length <> 7) Then
                errorMessage.Append("Invalid Detail Format" & Chr(13) & Chr(10))
            Else
                'Internal HU No
                If cols(1).Trim <> String.Empty Then
                    _oPackingD.InternalHUItemNo = MyBase.GetNumber(cols(1).Trim)
                End If

                'DO Number
                If cols(2).Trim = String.Empty Then
                    writeError("DO Number Can't Be Empty")
                Else
                    Dim DONUmber As String = cols(2).Trim
                    Dim objSPDOFac As SparePartDOFacade = New SparePartDOFacade(user)
                    Dim objSPDO As SparePartDO = objSPDOFac.Retrieve(DONUmber)
                    If Not IsNothing(objSPDO) AndAlso objSPDO.ID > 0 Then
                        _oPackingD.SparePartDO = objSPDO
                    Else
                        'writeError("Invalid DO Number")
                        Return Nothing
                    End If
                End If

                'SO Item No
                If cols(3).Trim <> String.Empty Then
                    _oPackingD.DOItemNo = cols(3).Trim
                End If

                'SparepartMaster
                If cols(4).Trim = String.Empty Then
                    writeError("Spare part number Can't Be Empty")
                Else
                    Dim SparePartNumber As String = cols(4).Trim
                    Dim objSparePartMasterFacade As SparePartMasterFacade = New SparePartMasterFacade(user)
                    Dim objSparePartMaster As SparePartMaster = objSparePartMasterFacade.Retrieve(SparePartNumber)
                    If Not IsNothing(objSparePartMaster) AndAlso objSparePartMaster.ID > 0 Then
                        _oPackingD.SparePartMaster = objSparePartMaster
                    Else
                        writeError("Invalid Spare Part Number")
                    End If
                End If

                'Quantity
                Try
                    If cols(5).Trim <> String.Empty Then
                        _oPackingD.Qty = CType(cols(5).Trim, Decimal)
                    End If
                Catch ex As Exception
                    writeError("Invalid Quantity")
                End Try
                'UoM
                If cols(6).Trim <> String.Empty Then
                    _oPackingD.UoM = CType(cols(6).Trim, String)
                End If

            End If

            If Not IsNothing(errorMessage) Then
                _oPackingD.ErrorMessage = errorMessage.ToString()
            End If
            Return _oPackingD
        End Function

#End Region

#Region "Custome Methods"
        Protected Function GetCurrencyByCurrentCulture(ByVal str As String) As Decimal
            Try
                Dim strCDS As String = ","
                Dim strCurrDecSeparator As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator
                If strCurrDecSeparator.Trim = "." Then
                    strCDS = ","
                ElseIf strCurrDecSeparator.Trim = "," Then
                    strCDS = "."
                End If
                Return Decimal.Parse(str.ToString().Replace(strCDS, strCurrDecSeparator))
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

    End Class
End Namespace
