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
Imports KTB.DNet.BusinessFacade.MDP
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
#End Region

Namespace KTB.DNet.Parser
    Public Class MDPMasterVehicleParser
        Inherits AbstractParser
#Region "Private Variables"
        Private arrMDPMasterVehicle As ArrayList
        Private Grammar As Regex
        Private errorMessage As StringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"
        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub
#End Region

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Dim lines As String() = MyBase.GetLines(Content)
            Dim ind As String
            Dim line As String
            Dim objMDPMasterVehicle As MDPMasterVehicle = Nothing
            arrMDPMasterVehicle = New ArrayList

            For i As Integer = 0 To lines.Length - 1
                Try
                    line = lines(i)

                    ind = line.Split(MyBase.ColSeparator)(0)
                    If ind = MyBase.IndicatorHeader Then
                        objMDPMasterVehicle = Nothing
                        errorMessage = New StringBuilder()
                        objMDPMasterVehicle = ParseHeader(line)
                        If Not IsNothing(objMDPMasterVehicle) Then
                            If errorMessage.ToString() <> String.Empty Then
                                objMDPMasterVehicle.ErrorMessage = errorMessage.ToString()
                            End If

                            arrMDPMasterVehicle.Add(objMDPMasterVehicle)
                        End If
                    End If

                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "MDPMasterVehicleParser.vb", "Parsing", KTB.DNet.Lib.DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MDPMasterVehicleParser, BlockName)
                    Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    Throw e
                End Try
            Next
            Return arrMDPMasterVehicle
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)

            Dim MDPMasterVehicleList As New ArrayList

            For Each MDPMasterVehicleItem As MDPMasterVehicle In arrMDPMasterVehicle
                Try
                    If MDPMasterVehicleItem.ErrorMessage = String.Empty AndAlso IsNothing(MDPMasterVehicleItem.ErrorMessage) Then
                        Dim MDPMasterVehicleFacade As New MDPMasterVehicleFacade(user)
                        If MDPMasterVehicleFacade.InsertFromWebSevice(MDPMasterVehicleItem) < 0 Then
                            nError += 1
                        End If
                    Else
                        nError += 1
                        sMsg &= MDPMasterVehicleItem.ErrorMessage & ";"
                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & MDPMasterVehicleList.Count.ToString(), "ws-worker", "MDPMasterVehicleParser.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MDPMasterVehicleParser, BlockName)

                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "MDPMasterVehicleParser.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MDPMasterVehicleParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String) As MDPMasterVehicle
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objMDPMasterVehicle As New MDPMasterVehicle

            Dim PDCode As String
            Dim UPTCode As Boolean = False

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                'Period Month / H-1
                'Period Start Date / H-6
                'Period End Date / H-7
                '1 Material Number
                PDCode = cols(1).Trim
                If PDCode = String.Empty Then
                    writeError("Material Number can't be empty")
                Else
                    Dim objVechileColor As New VechileColor

                    objVechileColor = New VechileColorFacade(user).RetrieveByMaterialNumber(cols(1).Trim)
                    If objVechileColor.ID = 0 Then
                        writeError("Material Number Not Found " & cols(1).Trim)
                    End If
                    objMDPMasterVehicle.VehicleColor = objVechileColor
                End If
                PDCode = cols(2).Trim

                If PDCode.ToUpper() = "X" Then
                    objMDPMasterVehicle.Status = 1
                Else
                    objMDPMasterVehicle.Status = 0
                End If
                objMDPMasterVehicle.RowStatus = 0
            End If

            Return objMDPMasterVehicle
        End Function

#End Region

    End Class


End Namespace
