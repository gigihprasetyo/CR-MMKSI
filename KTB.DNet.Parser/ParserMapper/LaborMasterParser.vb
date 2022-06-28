#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Text
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.DataMapper
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser
    Public Class LaborMasterParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _LaborMasters As ArrayList
        Private Grammar As Regex
        Private mapper As IMapper
        Private objTransactionManager As TransactionManager
        Private _fileName As String
        Private sBuilder As StringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser(",")
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _Stream = New StreamReader(fileName, True)
            _LaborMasters = New ArrayList
            _fileName = fileName
            Dim val As String = MyBase.NextLine(_Stream).Trim()
            While (Not val = "")
                Try
                    ParseLaborMaster(val + ",")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "LaborMasterParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.LaborMasterParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return _LaborMasters
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Try
                If _LaborMasters.Count > 0 Then
                    Dim objLaborMasterFacade As LaborMasterFacade = New LaborMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                    Dim insertResult As Integer
                    For Each item As LaborMaster In _LaborMasters
                        Try
                            Dim oldLabor As LaborMaster = GetLaborMaster(item)
                            If oldLabor.ID > 0 Then
                                oldLabor.RowStatus = item.RowStatus
                                insertResult = objLaborMasterFacade.Update(oldLabor)
                                If insertResult < 0 Then
                                    Throw New Exception("Failed Update into Database")
                                End If
                            Else
                                insertResult = objLaborMasterFacade.Insert(item)
                                If insertResult < 0 Then
                                    Throw New Exception("Failed insert into Database")
                                End If
                            End If
                        Catch ex As Exception
                            Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.VechileType.VechileTypeCode & "," & item.LaborCode & "," & item.WorkCode & Chr(13) & Chr(10) & ex.Message)
                            Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        End Try
                    Next
                End If
            Catch ex As Exception
                'Throw ex
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "LaborMasterParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.LaborMasterParser, BlockName)
            End Try
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"


        Private Function GetLaborMaster(ByVal labor As LaborMaster) As LaborMaster
            Dim mLabor As LaborMaster = New LaborMaster
            If Not IsNothing(labor.VechileType) Then
                mLabor = New LaborMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).RetrieveByPrimaryKey(labor.LaborCode, labor.WorkCode, labor.VechileType.ID)
            End If
            Return mLabor
        End Function

        Private Function IsValidForInsert(ByVal lbr As LaborMaster) As Boolean
            Dim retValue As Boolean = False
            If Not IsNothing(lbr.VechileType) Then
                Dim ret As Integer = New LaborMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).ValidateCode(lbr.LaborCode, lbr.WorkCode, lbr.VechileType.ID)
                retValue = (ret = 0)
            End If
            Return retValue
        End Function

        Private Sub ParseLaborMaster(ByVal ValParser As String)
            Dim _Lbr As LaborMaster = New LaborMaster
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            sBuilder = New StringBuilder
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        Dim objVechileType As VechileType = New VechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp)
                        If objVechileType.ID > 0 Then
                            _Lbr.VechileType = objVechileType
                        Else
                            sBuilder.Append("Invalid VechileType" & Chr(13) & Chr(10))
                        End If
                    Case Is = 1
                        If sTemp.Length > 0 Then
                            _Lbr.LaborCode = sTemp
                        Else
                            sBuilder.Append("Invalid Labor Code" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2
                        If sTemp.Length > 0 Then
                            _Lbr.WorkCode = sTemp
                        Else
                            sBuilder.Append("Invalid Work Code" & Chr(13) & Chr(10))
                        End If
                    Case Is = 3
                        If sTemp.Length > 0 Then
                            _Lbr.RowStatus = -1
                        Else
                            _Lbr.RowStatus = 0
                        End If

                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            If sBuilder.Length > 0 Then
                Throw New Exception(sBuilder.ToString)
            Else
                _Lbr.CreatedBy = "WSM"

                _LaborMasters.Add(_Lbr)
            End If

        End Sub

#End Region

    End Class
End Namespace
