#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports KTB.DNet.Utility
Imports System.Configuration
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.DataMapper
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade

Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser
    Public Class EquipmentPriceParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _EquipmentPrices As ArrayList
        Private Grammar As Regex
        Private mapper As IMapper
        Private _fileName As String
        Private ErrorMessage As StringBuilder
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
            _EquipmentPrices = New ArrayList
            Dim val As String = MyBase.NextLine(_Stream).Trim()
         
            While (Not val = "")
                 Try
                    ParseEquipmentPrice(val + ";")
                 Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "EquipmentPriceParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.EquipmentPriceParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try

                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return _EquipmentPrices
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim _EquipmentmasterFacade As EquipmentMasterFacade = New EquipmentMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            For Each item As EquipmentMaster In _EquipmentPrices
                Try
                    _EquipmentmasterFacade.Update(item)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "EquipmentPriceParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.EquipmentPriceParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.EquipmentNumber & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            Try
                SendEquipmentMail(_EquipmentPrices)
            Catch ex As Exception
                Dim e As Exception = New Exception("Error Sending email " & _fileName & ex.Message)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
            End Try
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Sub ParseEquipmentPrice(ByVal ValParser As String)
            Dim _EquipmentPrice As EquipmentMaster
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            ErrorMessage = New StringBuilder
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        Dim equipmentNumber As String = sTemp
                        Dim equiptMasterFacade As EquipmentMasterFacade = New EquipmentMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        _EquipmentPrice = equiptMasterFacade.Retrieve(equipmentNumber)
                        If _EquipmentPrice.ID < 1 Then
                            ErrorMessage.Append("Invalid Equipment Number" & Chr(13) & Chr(10))
                            'Throw New Exception("Invalid Equipment Number")
                        End If
                    Case Is = 1
                        If _EquipmentPrice.ID > 0 Then
                            Try
                                _EquipmentPrice.Price = sTemp
                            Catch ex As Exception
                                ErrorMessage.Append("Invalid Price" & Chr(13) & Chr(10))
                                'Throw New Exception("Invalid Price")
                            End Try
                        End If
                    Case Else
                End Select

                sStart = m.Index + 1
                nCount += 1
            Next
            If ErrorMessage.Length > 0 Then
                Throw New Exception(ErrorMessage.ToString)
            End If
            If _EquipmentPrice.ID > 0 Then
                _EquipmentPrices.Add(_EquipmentPrice)
            End If
        End Sub

        Private Function BuildBodyEmail(ByVal EquipColl As ArrayList) As StringBuilder
            Dim sb As StringBuilder = New StringBuilder
            sb.Append("<!-- saved from url=(0022)http://internet.e-mail -->")
            sb.Append("<html><body><table width=600px border=0>")
            sb.Append("<tr>")
            sb.Append("<td>Yth Service Planning Team,</td>")
            sb.Append("</tr>")
            sb.Append("<tr>")
            sb.Append("<td><br>Berikut ini data<b> Equipment Price List</b> yang telah kami lakukan update per hari ini.</td>")
            sb.Append("</tr>")
            sb.Append("<tr><td><br>")
            sb.Append("<table width=600px border=0 bgcolor=black cellspacing=1>")
            sb.Append("<tr bgcolor=white>")
            sb.Append("<td width=30%><div align=center><b>Kode Equipment</b></div></td>")
            sb.Append("<td width=40%><div align=center><b>Nama Equipment</b></div></td>")
            sb.Append("<td width=30%><div align=center><b>Harga</b></div></td>")
            sb.Append("</tr>")

            For Each item As EquipmentMaster In EquipColl
                sb.Append("<tr bgcolor=white>")
                sb.Append("    <td width=30%><div align=left> " & item.EquipmentNumber & " </div></td>")
                sb.Append("    <td width=40%><div align=left>" & item.Description & "</div></td>")
                sb.Append("    <td width=30%><div align=right>" & String.Format("{0:#,###}", item.Price) & "</div></td>")
                sb.Append("</tr>")
            Next

            sb.Append("</table></td></tr>")
            sb.Append("<tr><td><br><br><br>")
            sb.Append("<table width=600px border=0 >")
            sb.Append("<tr><td align=left>Sekian Terima Kasih,</td></tr>")

            sb.Append("<tr><td align=left><br>Jakarta, " & Now.Day & " " & ConvertMonth(Now.Month) & " " & Now.Year & "</td></tr>")
            sb.Append("<tr><td align=left>&nbsp;</td></tr>")
            sb.Append("<tr><td align=left>General Affrair Dept.</td></tr>")
            sb.Append("</tr>")
            sb.Append("</table>")
            sb.Append("</td> </tr>")
            sb.Append("</tr>")
            sb.Append("</table></html><body>")
            Return sb
        End Function

        Private Sub SendEquipmentMail(ByVal EquipColl As ArrayList)
            Dim MailSubject As String = "[MMKSI-DNET] Parts - Equipment Price Updated " & Now.ToString("dd/MM/yyyy")
            Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
            Dim MailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EquipmentSender")
            Dim MailTo As String = KTB.DNet.Lib.WebConfig.GetValue("EquipmentReceiver")
            Dim mail As DNetMail = New DNetMail(smtp)
            Dim mailBody As StringBuilder = BuildBodyEmail(EquipColl)
            Try
                mail.sendMail(MailTo, "", MailFrom, MailSubject, Web.Mail.MailFormat.Html, mailBody.ToString)
            Catch ex As Exception
                Throw ex
            End Try
      
        End Sub

        Private Function ConvertMonth(ByVal _month As Integer) As String
            Select Case _month
                Case Is = 1
                    Return "Januari"
                Case Is = 2
                    Return "Februari"
                Case Is = 3
                    Return "Maret"
                Case Is = 4
                    Return "April"
                Case Is = 5
                    Return "Mei"
                Case Is = 6
                    Return "Juni"
                Case Is = 7
                    Return "Juli"
                Case Is = 8
                    Return "Agustus"
                Case Is = 9
                    Return "September"
                Case Is = 10
                    Return "Oktober"
                Case Is = 11
                    Return "November"
                Case Is = 12
                    Return "Desember"
            End Select
        End Function

#End Region

    End Class
End Namespace
