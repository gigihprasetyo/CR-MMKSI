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
Imports KTB.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser

    Public Class EndCustomerParser
        Inherits AbstractParser

#Region "Private Variables"

        Private status As String
        Private _Stream As StreamReader
        Private EndCustomers As ArrayList
        Private Grammar As Regex
        Private _filename As String

#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _Stream = New StreamReader(fileName, True)
            EndCustomers = New ArrayList
            _filename = fileName
            Dim val As String = MyBase.NextLine(_Stream).Trim()

            While (Not val = "")
                Try
                    ParseEndCustomer(val + ";")
                 Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "EndCustomerParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.endCustomerParser, BlockName)
                    Dim e As Exception = New Exception(_filename & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return EndCustomers
        End Function

        Protected Overrides Function DoTransaction() As Integer
            If EndCustomers.Count > 0 Then
                For Each ECustomer As EndCustomer In EndCustomers
                    Try
                        If Not IsExistCode2(ECustomer.ChassisMaster.ID) Then
                            InsertEndCustomer(ECustomer)  '-- Insert 
                        Else
                            UpdateEndCustomer(ECustomer)  '-- Update
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "EndCustomerParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.endCustomerParser, BlockName)
                        Dim e As Exception = New Exception(_filename & Chr(13) & Chr(10) & ECustomer.Customer.Name1 & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    End Try
                Next
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Sub ParseEndCustomer(ByVal ValParser As String)
            Dim _EndCustomer As EndCustomer = New EndCustomer
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            Dim StatusHeader As String
            Dim arlist, arlist2, arlist3 As ArrayList
            sStart = 0
            nCount = 0
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()

                Select Case (nCount)
                    Case Is = 0
                        StatusHeader = sTemp.Trim
                    Case Is = 1
                        _EndCustomer.ChassisMaster.ChassisNumber = sTemp.Trim
                    Case Is = 3
                        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.dnet.domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opand(New Criteria(GetType(KTB.dnet.Domain.VechileColor), "MaterialNumber", MatchType.Exact, sTemp.Trim))
                        arlist3 = New VechileColorFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                    Case Is = 13
                        '_EndCustomer.Name1 = sTemp.Trim
                    Case Is = 14
                        '_EndCustomer.Name2 = sTemp.Trim
                    Case Is = 15
                        '_EndCustomer.Name3 = sTemp.Trim
                    Case Is = 16
                        '_EndCustomer.Alamat = sTemp.Trim
                    Case Is = 17
                        '_EndCustomer.Kelurahan = sTemp.Trim
                    Case Is = 18
                        '_EndCustomer.Kecamatan = sTemp.Trim
                    Case Is = 21
                        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Province), "ProvinceCode", MatchType.Exact, sTemp.Trim))
                        arlist2 = New ProvinceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                        Dim objProvince As Province = New ProvinceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                    Case Is = 22
                        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.City), "AreaCode", MatchType.Exact, sTemp.Trim))
                        arlist = New CityFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                        If Not IsNothing(arlist) Then
                            For Each ObjArea1 As City In arlist
                                _EndCustomer.Customer.City = ObjArea1
                            Next
                        End If
                    Case Is = 19
                        ' _EndCustomer.PostalCode = sTemp.Trim
                    Case Is = 20
                        '_EndCustomer.PreArea = sTemp.Trim
                    Case Is = 23
                        '_EndCustomer.PrintRegion = sTemp.Trim
                    Case Is = 24
                        '_EndCustomer.Email = sTemp.Trim
                    Case Is = 25
                        '_EndCustomer.Phone = sTemp.Trim
                    Case Else
                End Select

                sStart = m.Index + 1
                nCount += 1

            Next
            If StatusHeader = "X" And arlist2.Count > 0 And arlist.Count > 0 And arlist3.Count > 0 Then
                _EndCustomer.OpenFakturDate = New Date(1900, 1, 1)
                _EndCustomer.FakturDate = New Date(1900, 1, 1)
                _EndCustomer.CreatedTime = New Date(1900, 1, 1)
                EndCustomers.Add(_EndCustomer)
            End If



        End Sub

        Private Function GetEndCustomer(ByVal code As String)
            Dim userPrinciple As IPrincipal
            Dim VEndCustomerfacade As New EndCustomerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim _endCustomer As EndCustomer = VEndCustomerfacade.Retrieve(code)
            Return _endCustomer
        End Function

        Private Function IsExistCode2(ByVal ChassisId As Integer) As Boolean
            Dim objEndCustomerFacade As EndCustomerFacade = New EndCustomerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            'Periksa agar tidak ada key ganda 
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EndCustomer), "ChassisMaster.ID", MatchType.Exact, ChassisId))
            Dim TExist As ArrayList = New EndCustomerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            If TExist.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Private Sub UpdateEndCustomer(ByVal ECustomer As EndCustomer)
            'Dim criterias As CriteriaComposite
            'Dim objEndCustomer2 As EndCustomer
            'Dim objEndCustomerRet As ArrayList
            Dim objEndCustumerFacade As EndCustomerFacade = New EndCustomerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            'criterias = New CriteriaComposite(New Criteria(GetType(EndCustomer), "ChassisMaster.ID", MatchType.Exact, ECustomer.ChassisMaster.ID))
            'objEndCustomerRet = New EndCustomerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            'If objEndCustomerRet.Count > 0 Then
            '    objEndCustomer2 = objEndCustomerRet(0)
            'End If
            'ECustomer.ID = objEndCustomer2.ID
            objEndCustumerFacade.Update(ECustomer)

        End Sub

        Private Sub InsertEndCustomer(ByVal ECustomer As EndCustomer)
            Dim objEndCustomerFacade As EndCustomerFacade = New EndCustomerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            objEndCustomerFacade.Insert(ECustomer)
        End Sub

#End Region

    End Class

End Namespace