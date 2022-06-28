#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade
#End Region

Namespace KTB.DNet.Parser

    Public Class PaymentStatusParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private PaymentStatuss As ArrayList
        Private Grammar As Regex
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _Stream = New StreamReader(fileName, True)
            PaymentStatuss = New ArrayList
            Dim val As String = MyBase.NextLine(_Stream).Trim()
            While (Not val = "")
                'ParsePaymentStatus(val + ";")
                Try
                    ParsePaymentStatus(val + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "PaymentStatusParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.PaymentStatusParser, BlockName)
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return PaymentStatuss
        End Function

        Protected Overrides Function DoTransaction() As Integer
            If PaymentStatuss.Count > 0 Then
                'Do Business - Like save to database
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Sub ParsePaymentStatus(ByVal ValParser As String)
            Dim _PaymentStatus As PaymentStatus = New PaymentStatus
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0

            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    'Case Is = 0
                    '    _PaymentStatus.RowStatus = sTemp.Trim
                Case Is = 1
                        '_PaymentStatus.ChassisNumber = sTemp.Trim
                    Case Is = 2
                        '_PaymentStatus.Category = sTemp.Trim
                        'Case Is = 2
                        '    If sTemp.Length = 0 Then
                        '        _PaymentStatus.Category = _PaymentStatus.ErrorMessage & ";Kode Warna Kosong"
                        '    Else
                        '        Dim vt As VechileType = GetVechileType(sTemp.Trim.Substring(0, 4))

                        '        If vt.VechileTypeCode = "" Then
                        '            _PaymentStatus.ErrorMessage = _PaymentStatus.ErrorMessage & ";Tipe Kosong"
                        '        Else
                        '            _PaymentStatus.MaterialNumber = sTemp.Trim
                        '            Dim _vt As VechileType = New VechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim.Substring(0, 4))
                        '            _PaymentStatus.VechileType = _vt
                        '            _PaymentStatus.ColorCode = sTemp.Trim.Substring(4, 4)
                        '        End If

                        '    End If
                    Case Is = 3
                        '_PaymentStatus.MaterialNumber = sTemp.Trim
                    Case Is = 4
                        If sTemp.Length = 0 Then
                            _PaymentStatus.ErrorMessage = _PaymentStatus.ErrorMessage & ";Header Kosong"
                        Else
                            Dim objDealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                            _PaymentStatus.Dealer = objDealer
                        End If
                    Case Is = 5
                        '_PaymentStatus.DODate = sTemp.Trim
                    Case Is = 6
                        '_PaymentStatus.DONumber = sTemp.Trim
                    Case Is = 7
                        '_PaymentStatus.SONumber = sTemp.Trim
                    Case Is = 8
                        '_PaymentStatus.d = sTemp.Trim

                    Case Else
                End Select

                sStart = m.Index + 1
                nCount += 1
            Next

            PaymentStatuss.Add(_PaymentStatus)
        End Sub

        Private Function GetVechileType(ByVal code As String)
            Dim userPrinciple As IPrincipal
            Dim VTypefacade As New VechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim _vechileType As VechileType = VTypefacade.Retrieve(code)
            Return _vechileType
        End Function

#End Region

    End Class

End Namespace