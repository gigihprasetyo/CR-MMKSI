#Region ".NET Base Class Namespace Imports"

Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports KTB.DNet.Domain
#End Region

Namespace KTB.DNet.Parser
    Public Class CustomerOrderParser
        Inherits AbstractParser

#Region "Private Variables"
        Private stream As StreamReader
        Private Customers As ArrayList
        Private grammar As Regex

        Private _Customer As Customer 'Header
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            stream = New StreamReader(fileName, True)
            Customers = New ArrayList
            Dim val As String = MyBase.NextLine(stream).Trim()
            While (Not val = "")
                'If val.Substring(0, 1).ToUpper.Equals("H") Then
                '    If Not _Customer Is Nothing Then
                '        Customers.Add(_Customer) 'customer header input text
                '    End If
                '    _Customer = ParseCustomer(val + ";")
                'Else
                '    If Not _Customer Is Nothing Then
                '        ParseOrders(val + ";", _Customer) 'Order detail input
                '   End If
                'End If

                Try
                    If val.Substring(0, 1).ToUpper.Equals("H") Then
                        If Not _Customer Is Nothing Then
                            Customers.Add(_Customer) 'customer header input text
                        End If
                        _Customer = ParseCustomer(val + ";")
                    Else
                        If Not _Customer Is Nothing Then
                            ParseOrders(val + ";", _Customer) 'Order detail input
                        End If
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "CustomerOrderParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.CustomerOrderParser, BlockName)
                End Try
                val = MyBase.NextLine(stream)
            End While

            If Not _Customer Is Nothing Then
                Customers.Add(_Customer)
            End If

            stream.Close()
            stream = Nothing
            Return Customers
        End Function

        Protected Overrides Function DoTransaction() As Integer
            'Do Business
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Function ParseCustomer(ByVal ValParser As String) As Customer
            '_Customer = New Customer
            'Dim sStart As Integer
            'Dim nCount As Integer
            'Dim sTemp As String
            'sStart = 0
            'nCount = 0
            'For Each m As Match In grammar.Matches(ValParser)
            '    sTemp = ValParser.Substring(sStart, m.Index - sStart)
            '    sTemp = sTemp.Trim("""")
            '    sTemp = sTemp.Trim()
            '    Select Case (nCount)
            '        Case Is = 1
            '            _Customer.Id = sTemp
            '        Case Is = 2
            '            _Customer.Name = sTemp
            '        Case Else
            '            'do nothing
            '    End Select
            '    sStart = m.Index + 1
            '    nCount += 1
            'Next
            'Return _Customer
        End Function

        Private Sub ParseOrders(ByVal ValParser As String, ByVal _objCustomer As Customer)
            'Dim _order As Order = New Order
            'Dim sStart As Integer
            'Dim nCount As Integer
            'Dim sTemp As String
            'sStart = 0
            'nCount = 0
            'For Each m As Match In grammar.Matches(ValParser)
            '    sTemp = ValParser.Substring(sStart, m.Index - sStart)
            '    sTemp = sTemp.Trim("""")
            '    sTemp = sTemp.Trim()
            '    Select Case (nCount)
            '        Case Is = 1
            '            _order.CustomerID = sTemp
            '        Case Is = 2
            '            _order.ProductID = sTemp
            '        Case Is = 3
            '            _order.Quantity = Int32.Parse(sTemp)
            '        Case Else
            '            'Do Nothing else
            '    End Select
            '    sStart = m.Index + 1
            '    nCount += 1
            'Next
            '_objCustomer.Orders.Add(_order)
        End Sub

#End Region

    End Class

End Namespace