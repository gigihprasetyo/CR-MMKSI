 
#Region ".NET Base Class Namespace Imports"

Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Text
#End Region

#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Security.Principal
#End Region

Namespace KTB.DNet.Parser

    Public Class CustomerDataParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _fileName As String
        Private _stream As StreamReader
        Private ErrorMessage As StringBuilder
        Private grammar As Regex
        Private _ChassisMasterProfile As ChassisMasterProfile  '-- ChassisMasterProfile header & its details
        Private _ChassisMasterProfiles As ArrayList
        Private _CMFacade As New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
        Private _ProfHeaderFacade As New ProfileHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
        Private _ProfGroupFacade As New ProfileGroupFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _stream = New StreamReader(fileName, True)
            _ChassisMasterProfiles = New ArrayList
            _fileName = fileName
            Dim val As String = MyBase.NextLine(_stream).Trim()
            While (Not val = "")
                Try
                    ParseChassisMasterProfile(val + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "CustomerDataParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.CustomerDataParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_stream)
            End While
            _stream.Close()
            _stream = Nothing
            Return _ChassisMasterProfiles
        End Function

        Protected Overrides Function DoTransaction() As Integer
            For Each item As ChassisMasterProfile In _ChassisMasterProfiles
                Try
                    Dim _chassisMasterProfileFacade As ChassisMasterProfileFacade = New ChassisMasterProfileFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                    Dim history As ChassisMasterProfileHistory = New ChassisMasterProfileHistory
                    history.ChassisMasterProfile = item
                    history.ProvileValue = item.ProfileValue
                    item.ChassisMasterProfileHistorys.Add(history)
                    _chassisMasterProfileFacade.InsertWSM(item)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "CustomerDataParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.CustomerDataParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.ProfileHeader.Code & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
        End Function

#End Region

#Region "Private Methods"

        Private Sub ParseChassisMasterProfile(ByVal streamLine As String)
            _ChassisMasterProfile = New ChassisMasterProfile
            ErrorMessage = New StringBuilder
            Dim sStart As Integer = 0
            Dim nCount As Integer = 0
            Dim sColumn As String
            For Each m As Match In grammar.Matches(streamLine)
                sColumn = streamLine.Substring(sStart, m.Index - sStart)
                sColumn = sColumn.Trim()
                Select Case nCount
                    Case Is = 0
                        If sColumn.Length > 0 Then
                            Try
                                Dim _chassisMaster As ChassisMaster = _CMFacade.Retrieve(sColumn)
                                If _chassisMaster.ID > 0 Then
                                    _ChassisMasterProfile.ChassisMaster = _chassisMaster
                                Else
                                    ErrorMessage.Append("Chassis Master tidak terdefinisi." & Chr(13) & Chr(10))
                                End If
                            Catch
                                ErrorMessage.Append("Chassis Master tidak terdefinisi." & Chr(13) & Chr(10))
                            End Try
                        Else
                            ErrorMessage.Append("Chassis Master tidak terdefinisi." & Chr(13) & Chr(10))
                        End If
                    Case Is = 1
                        If sColumn.Length > 0 Then
                            Dim _profileHeader As ProfileHeader = _ProfHeaderFacade.Retrieve(sColumn)
                            If _profileHeader.ID > 0 Then
                                _ChassisMasterProfile.ProfileHeader = _profileHeader
                            Else
                                ErrorMessage.Append("Profile Header tidak ditemukan" & Chr(13) & Chr(10))
                            End If
                        Else
                            ErrorMessage.Append("Profile Header tidak ditemukan" & Chr(13) & Chr(10))
                        End If

                    Case Is = 2
                        _ChassisMasterProfile.ProfileValue = sColumn
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            If ErrorMessage.Length > 0 Then
                Throw New Exception(ErrorMessage.ToString)
            Else
                If _ChassisMasterProfile.ChassisMaster.Category.CategoryCode = "PC" Then
                    _ChassisMasterProfile.ProfileGroup = _ProfGroupFacade.Retrieve("cust_prf_pc")
                End If
                If _ChassisMasterProfile.ChassisMaster.Category.CategoryCode = "LCV" Then
                    _ChassisMasterProfile.ProfileGroup = _ProfGroupFacade.Retrieve("cust_prf_lcv")
                End If
                If _ChassisMasterProfile.ChassisMaster.Category.CategoryCode = "CV" Then
                    _ChassisMasterProfile.ProfileGroup = _ProfGroupFacade.Retrieve("cust_prf_cv")
                End If
                _ChassisMasterProfiles.Add(_ChassisMasterProfile)
            End If
        End Sub
#End Region

    End Class

End Namespace
