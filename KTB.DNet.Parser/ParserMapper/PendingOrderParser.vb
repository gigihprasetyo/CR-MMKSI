
#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.DataMapper
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Security.Principal
#End Region

Namespace KTB.DNet.Parser

    Public Class PendingOrderParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _pendingOrders As ArrayList
        Private Grammar As Regex
        Private _fileName As String
        Private strDealerlist As ArrayList
        Private _ErrorList As String = ""
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"
        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _fileName = fileName
            _Stream = New StreamReader(fileName, True)
            _pendingOrders = New ArrayList
            strDealerlist = New ArrayList
            Dim val As String = MyBase.NextLine(_Stream).Trim()
            While (Not val = "")
                'ParsePendingOrder(val + ";")
                Try
                    ParsePendingOrder(val + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "PendingOrderParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.PendingOrderParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return _pendingOrders
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

        Private Function DeleteExistingPendingOrders() As Integer
            Dim objPendingOrderFacade As PendingOrderFacade = New PendingOrderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim list As ArrayList = objPendingOrderFacade.RetrieveActiveList
            'Dim newList As ArrayList = New ArrayList
            'For Each item As PendingOrder In list
            '    If item.RowStatus = DBRowStatus.Active Then
            '        If strDealerlist.Contains(item.Dealer.DealerCode) Then
            '            newList.Add(item)
            '        End If
            '    End If
            'Next
            'Return objPendingOrderFacade.DeletePendingOrders(newList)
            Return objPendingOrderFacade.DeletePendingOrders(list)
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim i As Integer = DeleteExistingPendingOrders()
            If _pendingOrders.Count > 0 Then
                System.Threading.Thread.CurrentThread.Sleep(2000)
                If i = 1 Then
                    For Each item As PendingOrder In _pendingOrders
                        Dim objPendingOrderFacade As PendingOrderFacade = New PendingOrderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        Try
                            objPendingOrderFacade.InsertFromWSM(item)
                        Catch ex As Exception
                            Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.BillingNumber & Chr(13) & Chr(10) & ex.Message)
                            Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        End Try
                    Next
                End If
            End If

            If _ErrorList <> "" Then
                Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & _ErrorList & Chr(13) & Chr(10))
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")

            End If

        End Function

#End Region

#Region "Private Methods"

        
        Private Sub ParsePendingOrder(ByVal ValParser As String)
            Dim _pendingOrder As PendingOrder = New PendingOrder
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            _pendingOrder.ErrorMessage = ""
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        If sTemp.Trim.Length > 0 Then
                            Dim objDealerFacade As DealerFacade = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                            Dim objDealer As Dealer = objDealerFacade.Retrieve(sTemp)
                            If objDealer.ID > 0 Then
                                _pendingOrder.Dealer = objDealer
                                strDealerlist.Add(sTemp)
                            Else
                                _pendingOrder.ErrorMessage = _pendingOrder.ErrorMessage & " Dealer tidak ditemukan."
                            End If
                        Else
                            _pendingOrder.ErrorMessage = _pendingOrder.ErrorMessage & " Dealer tidak ditemukan."
                        End If
                    Case Is = 1
                        If sTemp.Trim = "" Then
                            _pendingOrder.ErrorMessage = _pendingOrder.ErrorMessage & " Sparepart PO Number Kosong"
                        Else
                            Dim objSpPoFacade As SparePartPOFacade = New SparePartPOFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                            Dim objSppo As SparePartPO = objSpPoFacade.Retrieve(sTemp)
                            If Not objSppo Is Nothing Then
                                If objSppo.ID > 0 Then
                                    _pendingOrder.SparePartPO = objSppo
                                Else
                                    _pendingOrder.ErrorMessage = _pendingOrder.ErrorMessage & " Sparepart PO tidak ditemukan : " & sTemp
                                End If
                            Else
                                _pendingOrder.ErrorMessage = _pendingOrder.ErrorMessage & " Sparepart PO tidak ditemukan : " & sTemp
                            End If
                        End If
                    Case Is = 2
                        If sTemp.Length > 0 Then
                            If IsNumeric(sTemp) Then
                                _pendingOrder.Retail = CLng(sTemp)
                            Else
                                _pendingOrder.ErrorMessage = _pendingOrder.ErrorMessage & "Retail format salah"
                            End If
                        End If
                    Case Is = 3
                        If sTemp.Length > 0 Then
                            If IsNumeric(sTemp) Then
                                _pendingOrder.PPN = CLng(sTemp)
                            Else
                                _pendingOrder.ErrorMessage = _pendingOrder.ErrorMessage & "PPN format salah"
                            End If
                        End If
                    Case Is = 4
                        If sTemp.Length > 0 Then
                            If IsNumeric(sTemp) Then
                                _pendingOrder.DepositC2 = System.Math.Abs(CLng(sTemp))
                            Else
                                _pendingOrder.ErrorMessage = _pendingOrder.ErrorMessage & "Deposit format salah"
                            End If
                        End If
                    Case Is = 5
                        If sTemp.Length > 0 Then
                            If IsNumeric(sTemp) Then
                                _pendingOrder.TotalAmount = CLng(sTemp)
                            Else
                                _pendingOrder.ErrorMessage = _pendingOrder.ErrorMessage & "Total Amount format salah"
                            End If
                        End If
                    Case Is = 6
                        If sTemp.Length = 8 Then
                            Try
                                Dim _date As Date = New Date(sTemp.Substring(4, 4), sTemp.Substring(2, 2), sTemp.Substring(0, 2), 0, 0, 1)
                                _pendingOrder.IssueDate = _date
                            Catch ex As Exception
                                _pendingOrder.ErrorMessage = _pendingOrder.ErrorMessage & "Issue Date format salah"
                            End Try
                        Else
                            _pendingOrder.ErrorMessage = _pendingOrder.ErrorMessage & "Issue Date format salah"
                        End If
                    Case Is = 7
                        If sTemp.Length > 0 Then
                            _pendingOrder.SONumber = sTemp
                        End If
                    Case Is = 8
                        If sTemp.Length > 0 Then
                            If IsNumeric(sTemp) Then
                                _pendingOrder.AvailableDeposit = System.Math.Abs(CLng(sTemp))
                            Else
                                _pendingOrder.AvailableDeposit = 0
                            End If
                        Else
                            _pendingOrder.AvailableDeposit = 0
                        End If
                    Case Is = 9
                        If sTemp.Length > 0 Then
                            If IsNumeric(sTemp) Then
                                _pendingOrder.GiroReceive = System.Math.Abs(CLng(sTemp))
                            Else
                                _pendingOrder.GiroReceive = 0
                            End If
                        Else
                            _pendingOrder.GiroReceive = 0
                        End If
                        '--start anh 20140910 req by anq
                    Case Is = 10
                        If sTemp.Length > 0 Then
                            If IsNumeric(sTemp) Then
                                _pendingOrder.RO = System.Math.Abs(CLng(sTemp))
                            Else
                                _pendingOrder.RO = 0
                            End If
                        Else
                            _pendingOrder.RO = 0
                        End If
                    Case Is = 11
                        If sTemp.Length > 0 Then
                            If IsNumeric(sTemp) Then
                                _pendingOrder.Service = System.Math.Abs(CLng(sTemp))
                            Else
                                _pendingOrder.Service = 0
                            End If
                        Else
                            _pendingOrder.Service = 0
                        End If
                        '--end anh 20140910 req by anq
                End Select
                sStart = m.Index + 1
                nCount += 1

            Next

            If _pendingOrder.ErrorMessage.Length > 0 Then
                _ErrorList &= Chr(13) & Chr(10) & _pendingOrder.ErrorMessage.ToString
            Else
                _pendingOrders.Add(_pendingOrder)
            End If

        End Sub
#End Region

    End Class

End Namespace
