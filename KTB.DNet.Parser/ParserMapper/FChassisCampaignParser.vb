#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Text
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
#End Region

Namespace KTB.DNet.Parser

    Public Class FSChassisCampaignParser
        Inherits AbstractParser

#Region "Private Variables"
        Private errMessage As String
        Private status As String
        Private _Stream As StreamReader
        Private FSChassisCampaign As ArrayList
        Private Grammar As Regex
        Private _sessHelper As SessionHelper = New SessionHelper
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser(";")
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _Stream = New StreamReader(fileName, True)
            FSChassisCampaign = New ArrayList
            Dim val As String = MyBase.NextLine(_Stream).Trim()

            While (Not val = "")
                'ParseFSChassisCampaign(val + delimited)
                Try
                    ParseFSChassisCampaign(val + delimited)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "FSChassisCampaignParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.FSChassisCampaignParser, BlockName)
                End Try

                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return FSChassisCampaign
        End Function

        Protected Overrides Function DoTransaction() As Integer
            If FSChassisCampaign.Count > 0 Then
                'Do Business - Like save to database
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Sub ParseFSChassisCampaign(ByVal ValParser As String)
            Dim _FSChassisCampaign As FSChassisCampaign = New FSChassisCampaign
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            Dim mUser As IPrincipal
            Dim nDefaultKM As Integer = 50000
            Dim FSKind As FSKind = New FSKind
            Dim Rangka As ChassisMaster
            Dim objFSChassisCampaignFacade As FSChassisCampaignFacade = New FSChassisCampaignFacade(mUser)

            'Dim strDealerCode As String = String.Empty --0
            Dim strChasis As String = String.Empty '--1
            Dim strFSKind As String = String.Empty '--2
            'Dim strTglService As String = String.Empty '--3
            'Dim strTglJual As String = String.Empty '--4
            'Dim strKm As String = String.Empty '--5


            'Dim strDealerCode As String = CType(_sessHelper.GetSession("sessDealerLogin"), String)
            sStart = 0
            nCount = 0

            'cek jumlah kolom file
            Dim NumberOfColumn As Integer = GetNumberOfColumn(ValParser, ";")
            If NumberOfColumn < 2 Then
                If _FSChassisCampaign.ErrorMessage = "" Then
                    _FSChassisCampaign.ChassisMaster = New ChassisMaster(0)
                    _FSChassisCampaign.FSKind = New FSKind(0)
                    _FSChassisCampaign.ErrorMessage = "Format Data tidak lengkap"
                Else
                    _FSChassisCampaign.ErrorMessage = _FSChassisCampaign.ErrorMessage & ";<BR>Format Data tidak lengkap"
                End If
            Else
                For Each m As Match In Grammar.Matches(ValParser)
                    sTemp = ValParser.Substring(sStart, m.Index - sStart)
                    sTemp = sTemp.Trim("""")
                    sTemp = sTemp.Trim()
                    Select Case (nCount)
                        Case Is = 0
                            strFSKind = sTemp.Trim
                            If sTemp.Trim <> "" Then
                                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKind), "KindCode", MatchType.Exact, sTemp.Trim))
                                Dim ArryList = New FSKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                                If ArryList.Count > 0 Then
                                    For Each ObjFSKind As FSKind In ArryList
                                        _FSChassisCampaign.FSKind = ObjFSKind
                                        If ValidateFSKindOnVehicleType(ObjFSKind, Rangka) Then
                                            _sessHelper.SetSession("sessFSKindKM", ObjFSKind.KM)
                                        Else
                                            _sessHelper.SetSession("sessFSKindKM", nDefaultKM)
                                            If _FSChassisCampaign.ErrorMessage = "" Then
                                                _FSChassisCampaign.ErrorMessage = "Jenis FS Tidak Terdaftar"
                                            Else
                                                _FSChassisCampaign.ErrorMessage = _FSChassisCampaign.ErrorMessage + ";<br> Jenis FS Tidak Terdaftar"
                                            End If
                                            _FSChassisCampaign.FSKindMsg = sTemp.Trim
                                        End If
                                    Next
                                Else
                                    _sessHelper.SetSession("sessFSKindKM", nDefaultKM)
                                    If _FSChassisCampaign.ErrorMessage = "" Then
                                        _FSChassisCampaign.ErrorMessage = "Jenis FS Tidak Terdaftar"
                                    Else
                                        _FSChassisCampaign.ErrorMessage = _FSChassisCampaign.ErrorMessage + ";<br> Jenis FS Tidak Terdaftar"
                                    End If
                                    _FSChassisCampaign.FSKindMsg = sTemp.Trim
                                End If
                            Else
                                If _FSChassisCampaign.ErrorMessage = "" Then
                                    _FSChassisCampaign.ErrorMessage = "Jenis FS Kosong"
                                Else
                                    _FSChassisCampaign.ErrorMessage = _FSChassisCampaign.ErrorMessage + ";<br> Jenis FS Kosong"
                                End If
                                _FSChassisCampaign.FSKindMsg = sTemp.Trim
                            End If
                        Case Is = 1
                            strChasis = sTemp.Trim
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, sTemp.Trim))
                            Dim ArryList As ArrayList = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                            If ArryList.Count > 0 Then
                                Rangka = CType(ArryList(0), ChassisMaster)
                                For Each ObjChassisMaster As ChassisMaster In ArryList
                                    _FSChassisCampaign.ChassisMaster = ObjChassisMaster
                                    'akhir dari tambahan change request UAT
                                Next
                            Else
                                If _FSChassisCampaign.ErrorMessage = "" Then
                                    _FSChassisCampaign.ErrorMessage = "No Rangka Tidak Terdaftar"
                                Else
                                    _FSChassisCampaign.ErrorMessage = _FSChassisCampaign.ErrorMessage + ";<br> No Rangka Tidak Terdaftar"
                                End If
                                _FSChassisCampaign.ChassisNumberMsg = sTemp.Trim
                            End If
                    End Select
                    sStart = m.Index + 1
                    nCount += 1
                Next
                'end added
            End If
            FSChassisCampaign.Add(_FSChassisCampaign)
        End Sub

        Private Function GetVechileInformationSystem(ByVal code As String)
            Dim userPrinciple As IPrincipal
            Dim VInformationSystemfacade As New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim _vechileType As ChassisMaster = VInformationSystemfacade.Retrieve(code)
            Return _vechileType
        End Function

        Private Function ValidateFSKindOnVehicleType(ByVal objFSChassisCampaign As FSChassisCampaign) As Boolean
            Try
                Dim VechileTypeID As Integer = objFSChassisCampaign.ChassisMaster.VechileColor.VechileType.ID
                Dim FSKindID As Integer = objFSChassisCampaign.FSKind.ID
                Dim critComp As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "FSKind.ID", MatchType.Exact, FSKindID))
                critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "VechileType.ID", MatchType.Exact, VechileTypeID))

                Return New FSKindOnVechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critComp).Count > 0
            Catch e As Exception
                Return False
            End Try
        End Function

        Private Function ValidateFSKindOnVehicleType(ByVal _fsKind As FSKind, ByVal _chMaster As ChassisMaster) As Boolean
            Try
                Dim critComp As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "FSKind.ID", MatchType.Exact, _fsKind.ID))
                critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "VechileType.ID", MatchType.Exact, _chMaster.VechileColor.VechileType.ID))

                Return New FSKindOnVechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critComp).Count > 0
            Catch e As Exception
                Return False
            End Try
        End Function

        Private Function ValidateLBUMBengkulu(ByVal _dealerCode As String, ByVal _chmaster As ChassisMaster, ByVal _fsKind As String) As Boolean
            Dim vReturn As Boolean = False
            If _fsKind = "6" _
                AndAlso (_chmaster.VechileColor.VechileType.Description.Substring(0, 3).ToUpper = "FE7" _
                Or _chmaster.VechileColor.VechileType.Description.Substring(0, 3).ToUpper = "FE8") _
                Then
                If _dealerCode = "100016" _
                    AndAlso (_chmaster.EndCustomer.OpenFakturDate >= DateSerial(2010, 2, 1) _
                    And _chmaster.EndCustomer.OpenFakturDate <= DateSerial(2010, 6, 30)) _
                    Then
                    vReturn = True
                End If
            End If
            Return vReturn
        End Function

#End Region

#Region "Public method"
        Public Function IsAllowToSave() As Boolean
            If errMessage = String.Empty Then
                Return True
            Else
                Return False
            End If
        End Function
#End Region

    End Class

End Namespace