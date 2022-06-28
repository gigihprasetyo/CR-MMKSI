#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
#End Region

Namespace KTB.DNet.Parser

    Public Class MasterVehicleKindParser
        Inherits AbstractParser


#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private _arrVehicleKindGroup As ArrayList
        Private objVehicleKindGroup As VehicleKindGroup
        Private objVehicleKind As VehicleKind

        Private intNo As Short = 0
        Const chrSplitDel As String = "||"
#End Region

#Region "Constructors/Destructors/Finalizers"
        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub
#End Region

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try
                Dim lines As String() = MyBase.GetLines(Content)
                Dim ind As String
                Dim line As String

                _arrVehicleKindGroup = New ArrayList()
                objVehicleKindGroup = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(objVehicleKindGroup) Then
                                _arrVehicleKindGroup.Add(objVehicleKindGroup)
                                objVehicleKindGroup = Nothing
                            End If
                            errorMessage = New StringBuilder()
                            objVehicleKindGroup = ParseHeader(line)

                        ElseIf ind = MyBase.IndicatorDetail Then
                            If IsNothing(objVehicleKindGroup) OrElse Not IsNothing(objVehicleKindGroup.ErrorMessage) Then
                            Else
                                objVehicleKind = ParseDetail(line)
                                If Not IsNothing(objVehicleKind) Then
                                    objVehicleKind.VehicleKindGroup = objVehicleKindGroup
                                    objVehicleKindGroup.VehicleKinds.Add(objVehicleKind)
                                    If Not IsNothing(objVehicleKind.ErrorMessage) AndAlso objVehicleKind.ErrorMessage.Trim <> String.Empty Then
                                        objVehicleKindGroup.ErrorMessage = objVehicleKindGroup.ErrorMessage & ";" & objVehicleKind.ErrorMessage
                                    End If
                                End If
                            End If
                        End If

                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "MasterVehicleKindGroupParser.vb", "Parsing", KTB.DNet.Lib.DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MasterVehicleKindGroupParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _arrVehicleKindGroup = Nothing
                    End Try
                Next

                If Not IsNothing(objVehicleKindGroup) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objVehicleKindGroup.ErrorMessage = errorMessage.ToString()
                    _arrVehicleKindGroup.Add(objVehicleKindGroup)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return _arrVehicleKindGroup
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim oVehicleKindGroupFacade As VehicleKindGroupFacade
            Dim nResult As Integer = -1
            intNo = 0

            If Not IsNothing(_arrVehicleKindGroup) AndAlso _arrVehicleKindGroup.Count > 0 Then
                'Update All RowStatus to -10
                oVehicleKindGroupFacade = New VehicleKindGroupFacade(user)
                oVehicleKindGroupFacade.UpdateRowStatusVehicleKind(0, -10)

                For Each _objVehicleKindGroup As VehicleKindGroup In _arrVehicleKindGroup
                    Try
                        If _objVehicleKindGroup.ErrorMessage = String.Empty Then
                            oVehicleKindGroupFacade = New VehicleKindGroupFacade(user)
                            oVehicleKindGroupFacade.InsertFromWebSevice(_objVehicleKindGroup)
                        Else
                            Throw New Exception(_objVehicleKindGroup.ErrorMessage)
                        End If

                    Catch ex As Exception
                        If ex.Message.Length > 0 Then
                            Dim exMsg() As String = ex.Message.ToString().Split(chrSplitDel.ToCharArray, StringSplitOptions.RemoveEmptyEntries)
                            If exMsg.Length > 0 Then
                                For Each strmsg As String In exMsg
                                    If strmsg.Trim <> "" Then
                                        If Not sMsg.ToString().Trim().Contains(strmsg.Trim) Then
                                            intNo += 1
                                            sMsg &= Chr(13) & intNo.ToString & ". " & strmsg.Trim
                                        End If
                                    End If
                                Next
                            End If
                        End If

                        nError += 1
                    End Try
                Next

                'Reverse Update RowStatus to -1
                oVehicleKindGroupFacade = New VehicleKindGroupFacade(user)
                oVehicleKindGroupFacade.UpdateRowStatusVehicleKind(-10, -1)

            End If

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrVehicleKindGroup.Count.ToString(), "ws-worker", "MasterVehicleKindParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MasterVehicleKindParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "MasterVehicleKindParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MasterVehicleKindParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

#Region "Private Methods"
        Private Sub writeError(str As String)
            If errorMessage.Length = 0 Then
                errorMessage.Append(Chr(13) & str.Trim & ";")
            Else
                errorMessage.Append(Chr(13) & chrSplitDel & str.Trim & ";")
            End If
        End Sub

        Private Function ParseHeader(ByVal line As String) As VehicleKindGroup
            'K;MASTERVEHICLEKIND_timestamp\nnH;StringH-1;StringH-2;\nD;StringD-n.1;StringD-n.2;\n
            'K; MASTERVEHICLEKIND_20180810112801\nH;MPG;MOBILPENUMPANG\nD;SDN;SEDAN\nD;JP;JEEP\n

            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objVehicleKindGroup As New VehicleKindGroup
            Dim PDCode As String

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                '1. Code
                PDCode = cols(1).Trim()
                If PDCode = String.Empty Then
                    writeError("Code can't be empty")
                Else
                    objVehicleKindGroup.Code = PDCode
                End If

                '2. Description
                PDCode = cols(2).Trim()
                If PDCode = String.Empty Then
                    objVehicleKindGroup.Description = Nothing
                Else
                    objVehicleKindGroup.Description = PDCode
                End If

                objVehicleKindGroup.RowStatus = 0

            End If

            Return objVehicleKindGroup
        End Function

        Private Function ParseDetail(ByVal line As String) As VehicleKind
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim PDCode As String

            objVehicleKind = New VehicleKind

            If cols.Length <> 3 Then
                writeError("Invalid Detail Format")
            Else
                '1. Code
                PDCode = cols(1).Trim()
                If PDCode = String.Empty Then
                    writeError("Code can't be empty")
                Else
                    objVehicleKind.Code = PDCode
                End If

                '2. Description
                PDCode = cols(2).Trim()
                If PDCode = String.Empty Then
                    writeError("Description can't be empty")
                Else
                    objVehicleKind.Description = PDCode
                End If

                objVehicleKind.RowStatus = 0

            End If

            Return objVehicleKind
        End Function

#End Region

    End Class
End Namespace