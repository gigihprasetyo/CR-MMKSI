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

    Public Class PDIParser
        Inherits AbstractParser

#Region "Private Variables"
        Private status As String
        Private _Stream As StreamReader
        Private PDI As ArrayList
        Private Grammar As Regex
        Private _sessHelper As SessionHelper = New SessionHelper
        Private _fileName
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser(",")
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _Stream = New StreamReader(fileName, True)
            PDI = New ArrayList
            _fileName = fileName
            Dim val As String = MyBase.NextLine(_Stream).Trim()

            '--> Change Code (10/23/2017)
            'Ambil karakter terakhir dan cek apakah sama dgn Delimiter, Jika sama hapus karakternya
            Delimited = ","
            Dim lastChar As Char = val.Substring(val.Length - 1, 1)
            If lastChar = Delimited Then
                val = val.Remove(val.LastIndexOf(Delimited))
            End If

            While (Not val = "")
                'ParsePDI(val + delimited)          
                Try
                    '--> Start Change Code (10/23/2017)
                    Dim strArr() As String = val.Split(Delimited)
                    Select Case (strArr.Length)
                        Case Is >= 6
                            ParsePDI(val + Delimited)
                        Case Is = 5
                            ParsePDI(val + Delimited + "-" + Delimited)
                        Case Is = 4
                            ParsePDI(val + Delimited + "-" + Delimited + "-" + Delimited)
                        Case Is = 3
                            ParsePDI(val + Delimited + "-" + Delimited + "-" + Delimited + "-" + Delimited)
                        Case Is = 2
                            ParsePDI(val + Delimited + "-" + Delimited + "-" + Delimited + "-" + Delimited + "-" + Delimited)
                        Case Is = 1
                            ParsePDI(val + Delimited + "-" + Delimited + "-" + Delimited + "-" + Delimited + "-" + Delimited + "-" + Delimited)
                    End Select
                    '--> End Change Code (10/23/2017)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "PDIParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.PDIParser, BlockName)
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return PDI
        End Function

        Protected Overrides Function DoTransaction() As Integer
            If PDI.Count > 0 Then
                'Do Business - Like save to database
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Function SetErrorMessage(ByVal variable As String, ByVal value As String) As String
            If IsNothing(variable) Then
                Return value
            End If
            If variable.Trim = String.Empty Then
                Return value
            End If
            Return variable + ";<br>" + value
        End Function

        Private Sub ParsePDI(ByVal ValParser As String)
            Dim _PDI As PDI = New PDI
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            Dim nDefaultKM As Integer = 50000
            Dim ObjDealerTmp As Dealer = New Dealer

            Dim strDealerCode As String = CType(_sessHelper.GetSession("sessDealerLogin"), String)
            sStart = 0
            nCount = 0

            Dim critDealer As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critDealer.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, strDealerCode))
            Dim ArryListDealer As ArrayList = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critDealer)
            If ArryListDealer.Count > 0 Then
                ObjDealerTmp = CType(ArryListDealer(0), Dealer)
            End If
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        If sTemp.Trim <> strDealerCode Then
                            _PDI.ErrorMessage = SetErrorMessage(_PDI.ErrorMessage, "Kode Dealer Tidak Cocok")
                        Else
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, sTemp.Trim))
                            Dim ArryList As ArrayList = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                            If ArryList.Count > 0 Then
                                For Each ObjDealer As Dealer In ArryList

                                    _PDI.Dealer = ObjDealer

                                Next
                            End If
                        End If

                    Case Is = 1
                        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, sTemp.Trim))
                        Dim ArryList As ArrayList = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                        If ArryList.Count > 0 Then

                            For Each ObjChassisMaster As ChassisMaster In ArryList
                                'tambahan change request UAT
                                'If ObjDealerTmp.ID = ObjChassisMaster.Dealer.ID Then
                                _PDI.ChassisMaster = ObjChassisMaster
                                'Else
                                '    If _PDI.ErrorMessage = "" Then
                                '        _PDI.ErrorMessage = "Dealer Tidak Berhak PDI"
                                '    Else
                                '        _PDI.ErrorMessage = _PDI.ErrorMessage + " ;<br> Dealer Tidak Berhak PDI"
                                '    End If
                                '    _PDI.ChassisNumberMsg = sTemp.Trim
                                'End If
                                'akhir dari tambahan change request UAT
                            Next

                        Else
                            _PDI.ErrorMessage = SetErrorMessage(_PDI.ErrorMessage, "No. Rangka Tidak Terdaftar")
                            _PDI.ChassisNumberMsg = sTemp.Trim
                        End If

                    Case Is = 2
                        If sTemp.Length > 0 AndAlso Not IsNothing(_PDI.ChassisMaster) Then
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, _PDI.ChassisMaster.ChassisNumber))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "EngineNumber", MatchType.Exact, sTemp.Trim()))

                            Dim arrChassisMaster As ArrayList = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                            If arrChassisMaster.Count = 0 Then
                                _PDI.ErrorMessage = SetErrorMessage(_PDI.ErrorMessage, "No Mesin tidak sesuai")
                            End If
                        Else
                            _PDI.ErrorMessage = SetErrorMessage(_PDI.ErrorMessage, "No Mesin tidak sesuai")
                        End If

                    Case Is = 3
                        If sTemp.Trim <> "" Then
                            ' only accept for PDI type A previously A and D
                            If (sTemp.Trim.ToUpper = "A") Then
                                _PDI.Kind = sTemp.Trim.ToUpper
                            Else
                                _PDI.ErrorMessage = SetErrorMessage(_PDI.ErrorMessage, "Jenis PDI Tidak Terdaftar")
                                _PDI.PDIKindMsg = sTemp.Trim
                            End If
                        Else
                            _PDI.ErrorMessage = SetErrorMessage(_PDI.ErrorMessage, "Jenis PDI Kosong")
                            _PDI.PDIKindMsg = sTemp.Trim
                        End If
                    Case Is = 4
                        Dim tgl As String
                        If Len(sTemp.Trim) <> 8 Then
                            'Start Change Code 10/23/2017
                            If sTemp.Trim = "-" Then
                                'karakter (-) sebagai tanda kalau ada isi text yang kosong
                                _PDI.ErrorMessage = SetErrorMessage(_PDI.ErrorMessage, "Tgl PDI Tidak Boleh Kosong")
                                _PDI.PDIDateMsg = sTemp.Trim
                                'End Change Code 10/23/2017
                            Else
                                _PDI.ErrorMessage = SetErrorMessage(_PDI.ErrorMessage, "Format Tgl PDI Tidak Valid")
                            End If
                            _PDI.PDIDateMsg = sTemp.Trim
                        Else
                            'tgl = sTemp.Substring(2, 2).ToString & "-" & sTemp.Substring(0, 2) & "-" & sTemp.Substring(4, 4)
                            'yang jadi dipakai adalah setting tanggal indonesia
                            tgl = sTemp.Substring(0, 2).ToString & "-" & sTemp.Substring(2, 2) & "-" & sTemp.Substring(4, 4)
                            If Not IsDate(tgl) Then
                                _PDI.ErrorMessage = SetErrorMessage(_PDI.ErrorMessage, "Format Tgl PDI Tidak Valid")
                                _PDI.PDIDateMsg = sTemp.Trim
                            Else
                                If CompareDateWithCurrentDate(tgl) Then
                                    _PDI.PDIDate = tgl
                                    'start take out request dari miyuki 14/01/2020
                                    'If Not _PDI.ChassisMaster Is Nothing AndAlso Not _PDI.ChassisMaster.EndCustomer Is Nothing Then
                                    '    If _PDI.ChassisMaster.EndCustomer.FakturDate <> "1900-01-01 00:00:00.000" Then
                                    '        If tgl >= _PDI.ChassisMaster.EndCustomer.FakturDate Then
                                    '            _PDI.PDIDate = tgl
                                    '        Else
                                    '            _PDI.ErrorMessage = _PDI.ErrorMessage + " ;<br> Tgl PDI < tgl Faktur"
                                    '            _PDI.PDIDate = tgl
                                    '        End If
                                    '    Else
                                    '        _PDI.ErrorMessage = _PDI.ErrorMessage + " Tanggal Faktur kosong"
                                    '        _PDI.PDIDate = tgl
                                    '    End If

                                    '    If _PDI.ChassisMaster.EndCustomer.OpenFakturDate.Year > 1900 Then

                                    '        If tgl >= _PDI.ChassisMaster.EndCustomer.OpenFakturDate Then
                                    '            _PDI.PDIDate = tgl
                                    '        Else
                                    '            _PDI.ErrorMessage = SetErrorMessage(_PDI.ErrorMessage, "Tgl PDI < tgl Buka Faktur")
                                    '            _PDI.PDIDate = tgl
                                    '        End If
                                    '    Else
                                    '        _PDI.ErrorMessage = SetErrorMessage(_PDI.ErrorMessage, "Tanggal buka Faktur kosong")
                                    '        _PDI.PDIDate = tgl
                                    '    End If
                                    'Else
                                    '    _PDI.ErrorMessage = SetErrorMessage(_PDI.ErrorMessage, "Faktur tidak ditemukan")
                                    '    _PDI.PDIDate = tgl
                                    'End If
                                    'end take out request dari miyuki 14/01/2020
                                Else
                                    _PDI.ErrorMessage = SetErrorMessage(_PDI.ErrorMessage, "Tgl PDI melebihi hari ini")
                                    _PDI.PDIDate = tgl
                                End If

                            End If
                        End If
                    Case Is = 5
                        If (sTemp.Trim <> "") AndAlso (sTemp.Trim <> "-") AndAlso (Not IsNothing(_PDI.Dealer)) Then
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "DealerBranchCode", MatchType.Exact, sTemp.Trim))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "Dealer.DealerCode", MatchType.Exact, _PDI.Dealer.DealerCode))
                            Dim ArryList As ArrayList = New DealerBranchFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                            If ArryList.Count > 0 Then
                                For Each ObjDealerBranch As DealerBranch In ArryList
                                    _PDI.DealerBranch = ObjDealerBranch
                                Next
                            Else
                                _PDI.ErrorMessage = SetErrorMessage(_PDI.ErrorMessage, "Kode Cabang tidak terdaftar di dealer")
                                _PDI.DealerBranchCodeMsg = sTemp.Trim
                            End If
                        End If
                    Case Is = 6
                        If Not String.IsNullOrEmpty(sTemp.Trim) AndAlso (sTemp.Trim <> "-") Then
                            _PDI.WorkOrderNumber = sTemp.Trim
                        End If
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            Dim NumberOfColumn As Integer = GetNumberOfColumn(ValParser, Delimited)

            '5 is number of column PDI DATA 
            If NumberOfColumn < 5 Then
                _PDI.ErrorMessage = SetErrorMessage(_PDI.ErrorMessage, "Format Data tidak lengkap")
            End If

            _PDI.PDIStatus = EnumFSStatus.FSStatus.Baru
            PDI.Add(_PDI)
        End Sub

        Private Function GetVechileInformationSystem(ByVal code As String)
            Dim userPrinciple As IPrincipal
            Dim VInformationSystemfacade As New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim _vechileType As ChassisMaster = VInformationSystemfacade.Retrieve(code)
            Return _vechileType
        End Function

        Private Function CompareDateWithCurrentDate(ByVal entryDate As Date) As Boolean
            Dim currentDate As Date = New Date(Now.Year, Now.Month, Now.Day, 0, 0, 0)
            currentDate = currentDate.AddDays(1)
            If entryDate >= currentDate Then
                Return False
            Else
                Return True
            End If
        End Function



#End Region

    End Class

End Namespace