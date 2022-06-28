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

    Public Class MasterDealerParser
        Inherits AbstractParser


#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private _arrDealer As ArrayList
        Private objDealer As Dealer
        Private objDealerPajak As DealerPajak
        Private objDealerBankAccount As DealerBankAccount
        Private objBusinessArea As BusinessArea
        Dim arrDealerCtg As New ArrayList

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

                _arrDealer = New ArrayList()
                objDealer = Nothing
                objDealerPajak = Nothing
                objDealerBankAccount = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(objDealer) Then
                                _arrDealer.Add(objDealer)
                                objDealer = Nothing
                            End If

                            objDealerPajak = Nothing
                            objDealerBankAccount = Nothing
                            errorMessage = New StringBuilder()
                            objDealer = ParseHeader(line, objDealerPajak)

                            If Not IsNothing(objDealerPajak) Then
                                objDealer.DealerPajaks.Add(objDealerPajak)
                                If Not IsNothing(objDealerPajak.ErrorMessage) AndAlso objDealerPajak.ErrorMessage.Trim <> String.Empty Then
                                    objDealer.ErrorMessage = objDealer.ErrorMessage & ";" & objDealerPajak.ErrorMessage
                                End If
                            End If

                            'If arrDealerCtg.Count > 0 Then
                            '    objDealer.DealerCategorys.AddRange(arrDealerCtg)

                            'End If

                            'If Not IsNothing(objDealerBankAccount) Then
                            '    objDealer.DealerBankAccounts.Add(objDealerBankAccount)
                            '    If Not IsNothing(objDealerBankAccount.ErrorMessage) AndAlso objDealerBankAccount.ErrorMessage.Trim <> String.Empty Then
                            '        objDealer.ErrorMessage = objDealer.ErrorMessage & ";" & objDealerBankAccount.ErrorMessage
                            '    End If
                            'End If

                        ElseIf ind = MyBase.IndicatorDetail Then
                            If IsNothing(objDealer) OrElse Not IsNothing(objDealer.ErrorMessage) Then
                            Else
                                Dim objBankAcount As DealerBankAccount
                                objBankAcount = ParseNewDetail(line)
                                If Not IsNothing(objBankAcount) Then
                                    objBankAcount.Dealer = objDealer
                                    objDealer.DealerBankAccounts.Add(objBankAcount)
                                    If Not IsNothing(objBankAcount.ErrorMessage) AndAlso objBankAcount.ErrorMessage.Trim <> String.Empty Then
                                        objDealer.ErrorMessage = objDealer.ErrorMessage & ";" & objBankAcount.ErrorMessage
                                    End If
                                End If
                            End If
                        End If

                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "MasterDealerParser.vb", "Parsing", KTB.DNet.Lib.DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MasterDealerParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _arrDealer = Nothing
                    End Try
                Next


                If Not IsNothing(objDealer) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objDealer.ErrorMessage = errorMessage.ToString()
                    _arrDealer.Add(objDealer)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return _arrDealer
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim oDealerFacade As New DealerFacade(user)

            If Not IsNothing(_arrDealer) AndAlso _arrDealer.Count > 0 Then
                Dim doFacade As DealerFacade

                'Req Miyuki

                For Each _objDealer As Dealer In _arrDealer
                    Try
                        Dim oDBA As Integer = New DealerBankAccountFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)).UpdateExistingBankAccount(_objDealer)
                        intNo = 0
                        If IsNothing(_objDealer.ErrorMessage) OrElse (Not IsNothing(_objDealer.ErrorMessage) AndAlso _objDealer.ErrorMessage = String.Empty) Then
                            doFacade = New DealerFacade(user)
                            doFacade.InsertFromWebSevice(_objDealer)
                        Else
                            Throw New Exception(_objDealer.ErrorMessage)
                        End If

                    Catch ex As Exception
                        If ex.Message.Length > 0 Then
                            Dim exMsg() As String = ex.Message.ToString().Split(chrSplitDel.ToCharArray, StringSplitOptions.RemoveEmptyEntries)
                            If exMsg.Length > 0 Then
                                sMsg &= Chr(13) & "Dealer Code: " & _objDealer.DealerCode
                                For Each strmsg As String In exMsg
                                    If strmsg.Trim <> "" Then
                                        'If Not sMsg.ToString().Trim().Contains(strmsg.Trim) Then
                                        intNo += 1
                                        sMsg &= Chr(13) & intNo.ToString & ". " & strmsg.Trim
                                        'End If
                                    End If
                                Next
                            End If
                        End If

                        nError += 1
                    End Try
                Next
            End If

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrDealer.Count.ToString(), "ws-worker", "MasterDealerParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MasterDealerParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "MasterDealerParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MasterDealerParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String, ByRef objDealerPajak As DealerPajak) As Dealer
            ' K;MASTERDEALER_timestamp\nH;StringH-1;StringH-2;StringH-3;StringH-4;StringH-5;StringH-6;StringH-7;StringH-8;StringH-9;StringH-10;StringH-11;StringH-12;StringH-13;StringH-14;StringH-15;StringH-16;StringH-17;StringH-18;StringH-19;StringH-20;StringH-21;StringH-22;StringH-23;StringH-24;StringH-25;StringH-26;StringH-27;StringH-28;StringH-29; StringH-30;StringH-31;StringH-32;StringH-33\nD;StringD-n.1;StringD-n.2;StringD-n.3;StringD-n.4;StringD-n.5;StringD-n.6;StringD-n.7\n

            ' K;MASTERDEALER_20180810112801\nH;100001;;SUMATERA BERLIAN MOTORS, PT;X;SUMATRA MEDAN;ABA0;01;JL. RAYA MEDAN T.MORAWA KM.7/34;SU;SUMDN;20147;021123456789;-;www.tes.com;benny@bsi.co.id;X;X;X; IBT;SMT1;473 Notaris Sri Ismiyati, SH., MKn.;20170222;X;20180918;20180918;100001;X;123;20180918;01.135.554.2-123.000;02201363;027.01.05214.002;BANK NIAGA\nD;0;Mr;Dicky Rizaldi;dickyrizaldi.sbm@sbmgrp.com;023-23875;023-23876;081546546\n

            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objDealer As New Dealer
            objDealerPajak = New DealerPajak
            'objDealerBankAccount = New DealerBankAccount

            Dim PDCode As String

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                '1. DealerCode
                PDCode = cols(1).Trim()
                If PDCode = String.Empty Then
                    writeError("Dealer Code can't be empty")
                Else
                    objDealer.DealerCode = PDCode
                End If

                '2. MainDealerID
                PDCode = cols(2).Trim()
                'MainDealer di set Nothing dulu
                If PDCode = String.Empty Then
                    objDealer.CreditAccount = String.Empty
                Else
                    objDealer.CreditAccount = PDCode

                    'Dim objMainDealer As Dealer = New DealerFacade(user).Retrieve(PDCode)
                    'If Not IsNothing(objMainDealer) AndAlso objMainDealer.ID > 0 Then
                    '    objDealer.MainDealer = objMainDealer
                    'Else
                    '    'writeError("Main Dealer Code: " & PDCode & " doesn't exist")
                    'End If
                End If

                '3. Organization Branch Type
                PDCode = cols(3).Trim
                If String.IsNullorEmpty(PDCode) Then
                    writeError("Organization Type Branch can't be empty")
                Else
                    objDealer.OrganizationBranchType = Me.GetValueID("OrganizationBranchType", PDCode)
                    If objDealer.OrganizationBranchType = 0 Then
                        writeError("Organization Type Branch: " & PDCode & " doesn't exist")
                    End If

                End If

                '4. Parent Dealer
                PDCode = cols(4).Trim
                If Not String.IsNullorEmpty(PDCode) And objDealer.OrganizationBranchType <> 1 Then
                    objDealer.ParentDealer = New DealerFacade(user).Retrieve(PDCode)
                    objDealer.MainDealer = objDealer.ParentDealer
                End If


                '5. DealerName
                PDCode = cols(5).Trim
                If PDCode = String.Empty Then
                    writeError("Dealer Name can't be empty")
                Else
                    objDealer.DealerName = PDCode
                End If

                '6. Status - INACTIVE = 0, ACTIVE = 1
                PDCode = cols(6).Trim
                objDealer.Status = IIf(PDCode = "ACTIVE", 1, 0)
                If objDealer.Status = 0 Then
                    objDealer.Publish = False
                End If
                objDealer.Title = 0

                '7. SearchTerm1
                PDCode = cols(7).Trim
                If PDCode = String.Empty Then
                    writeError("SearchTerm1 can't be empty")
                Else
                    objDealer.SearchTerm1 = PDCode
                End If

                '8. SearchTerm2
                PDCode = cols(8).Trim
                If PDCode = String.Empty Then
                    'writeError("SearchTerm2 can't be empty")
                    PDCode = "XX" & Right(cols(1).Trim(), 2)
                End If
                objDealer.SearchTerm2 = PDCode

                '9. DealerGroup
                PDCode = cols(9).Trim
                If PDCode = String.Empty Then
                    PDCode = 98    '--> set to DelaerGroupCode = 20   '--> request Mas Benny
                End If
                If PDCode <> String.Empty Then
                    'If PDCode.Length = 1 Then
                    '    PDCode = "0" & PDCode
                    'End If
                    Dim objDealerGroup As DealerGroup = New DealerGroupFacade(user).Retrieve(PDCode)
                    If Not IsNothing(objDealerGroup) AndAlso objDealerGroup.ID > 0 Then
                        If 1 = 1 Then
                            objDealer.DealerGroup = objDealerGroup
                        End If
                    Else
                        writeError("Dealer Group Code: " & PDCode & " doesn't exist")
                    End If
                End If


                '10. Address
                PDCode = cols(10).Trim
                If PDCode = String.Empty Then
                    'writeError("Address can't be empty")
                Else
                    objDealer.Address = PDCode
                End If

                '11. ProvinceCode
                PDCode = cols(11).Trim
                If PDCode = String.Empty Then
                    writeError("Province Code can't be empty")
                Else
                    Dim objProvince As Province = New ProvinceFacade(user).Retrieve(PDCode)
                    If Not IsNothing(objProvince) AndAlso objProvince.ID > 0 Then
                        objDealer.Province = objProvince
                    Else
                        writeError("Province Code: " & PDCode & " doesn't exist")
                    End If
                End If

                '12. CityCode
                PDCode = cols(12).Trim
                If PDCode = String.Empty Then
                    'writeError("City Code can't be empty")
                Else
                    Dim objCity As City = New CityFacade(user).Retrieve(PDCode)
                    If Not IsNothing(objCity) AndAlso objCity.ID > 0 Then
                        objDealer.City = objCity
                    Else
                        writeError("City Code: " & PDCode & " doesn't exist")
                    End If
                End If

                '13. ZipCode
                PDCode = cols(13).Trim
                If PDCode = String.Empty Then
                    'writeError("ZipCode can't be empty")
                    objDealer.ZipCode = "00000"
                Else
                    objDealer.ZipCode = PDCode
                End If

                '14. Phone
                PDCode = cols(14).Trim
                If PDCode = String.Empty Then
                    'writeError("Phone can't be empty")
                Else
                    objDealer.Phone = PDCode
                End If

                '15. Fax
                PDCode = cols(15).Trim
                If PDCode = String.Empty Then
                    'writeError("Fax can't be empty")
                Else
                    objDealer.Fax = PDCode
                End If

                '16. Website
                PDCode = cols(16).Trim
                If PDCode = String.Empty Then
                    'writeError("Website can't be empty")
                Else
                    objDealer.Website = PDCode
                End If

                '17. Email
                PDCode = cols(17).Trim
                If PDCode = String.Empty Then
                    'writeError("Email can't be empty")
                Else
                    objDealer.Email = PDCode
                End If

                '18. SalesUnitFlag  -  X = 1, blank = 0
                PDCode = cols(18).Trim
                If String.IsNullorEmpty(PDCode) Then
                    objDealer.SalesUnitFlag = 0
                Else
                    Dim objOprBuss As DealerOperationAreaBussiness
                    objDealer.SalesUnitFlag = 1
                    Dim strBusinessOpr As String() = PDCode.Split("/")
                    For Each itemStr As String In strBusinessOpr
                        objOprBuss = New DealerOperationAreaBussiness()
                        objOprBuss.AreaBusiness = 0
                        objOprBuss.DealerOperation = Me.GetValueID("DealerOperationBusiness", itemStr)
                        objDealer.DealerOperationAreaBusiness.Add(objOprBuss)
                    Next
                End If


                '19. ServiceFlag  -  X = 1, blank = 0
                PDCode = cols(19).Trim
                If String.IsNullorEmpty(PDCode) Then
                    objDealer.ServiceFlag = 0
                Else
                    Dim objOprBuss As DealerOperationAreaBussiness
                    objDealer.ServiceFlag = 1
                    Dim strBusinessOpr As String() = PDCode.Split("/")
                    For Each itemStr As String In strBusinessOpr
                        objOprBuss = New DealerOperationAreaBussiness()
                        objOprBuss.AreaBusiness = 1
                        objOprBuss.DealerOperation = Me.GetValueID("DealerOperationBusiness", itemStr)
                        objDealer.DealerOperationAreaBusiness.Add(objOprBuss)
                    Next
                End If

                '20. SparepartFlag  -  X = 1, blank = 0
                PDCode = cols(20).Trim
                If String.IsNullorEmpty(PDCode) Then
                    objDealer.SparepartFlag = 0
                Else
                    Dim objOprBuss As DealerOperationAreaBussiness
                    objDealer.SparepartFlag = 1
                    Dim strBusinessOpr As String() = PDCode.Split("/")
                    For Each itemStr As String In strBusinessOpr
                        objOprBuss = New DealerOperationAreaBussiness()
                        objOprBuss.AreaBusiness = 2
                        objOprBuss.DealerOperation = Me.GetValueID("DealerOperationBusiness", itemStr)
                        objDealer.DealerOperationAreaBusiness.Add(objOprBuss)
                    Next
                End If

                '21. Area1ID
                PDCode = cols(21).Trim
                If PDCode = String.Empty Then
                    'writeError("Area1 Code can't be empty")
                Else
                    Dim objArea1 As Area1 = New Area1Facade(user).Retrieve(PDCode)
                    If Not IsNothing(objArea1) AndAlso objArea1.ID > 0 Then
                        objDealer.Area1 = objArea1
                        objDealer.MainArea = objArea1.MainArea
                    Else
                        writeError("Area1 Code: " & PDCode & " doesn't exist")
                    End If
                End If

                '22. Area2ID
                PDCode = cols(22).Trim
                If PDCode = String.Empty Then
                    'writeError("Area2 Code can't be empty")
                Else
                    Dim objArea2 As Area2 = New Area2Facade(user).Retrieve(PDCode)
                    If Not IsNothing(objArea2) AndAlso objArea2.ID > 0 Then
                        objDealer.Area2 = objArea2
                    Else
                        writeError("Area2 Code: " & PDCode & " doesn't exist")
                    End If
                End If

                '23. SPANumber
                PDCode = cols(23).Trim
                If PDCode = String.Empty Then
                    'writeError("SPA Number can't be empty")
                Else
                    objDealer.SPANumber = PDCode
                End If

                '24. SPADate
                PDCode = cols(24).Trim
                If PDCode = String.Empty Then
                    'writeError("SPA Date can't be empty")
                Else
                    Dim _SPADate As DateTime
                    Try
                        _SPADate = New Date(PDCode.Substring(0, 4), PDCode.Substring(4, 2), PDCode.Substring(6, 2))
                        objDealer.SPADate = _SPADate
                    Catch ex As Exception
                        writeError("Invalid [SPA Date] " & PDCode)
                    End Try
                End If

                '25. FreePPh22Indicator  -  X = 0, blank = 1
                PDCode = cols(25).Trim
                objDealer.FreePPh22Indicator = IIf(PDCode = "X", 0, 1)

                '26. FreePPh22From
                PDCode = cols(26).Trim
                If PDCode = String.Empty Then
                    'writeError("Free PPh22 From can't be empty")
                Else
                    Dim _FreePPh22From As DateTime
                    Try
                        _FreePPh22From = New Date(PDCode.Substring(0, 4), PDCode.Substring(4, 2), PDCode.Substring(6, 2))
                        objDealer.FreePPh22From = _FreePPh22From
                    Catch ex As Exception
                        writeError("Invalid [Free PPh22 From] " & PDCode)
                    End Try
                End If

                '27. FreePPh22To
                PDCode = cols(27).Trim
                If PDCode = String.Empty Then
                    'writeError("Free PPh22 To can't be empty")
                Else
                    Dim _FreePPh22To As DateTime
                    Try
                        _FreePPh22To = New Date(PDCode.Substring(0, 4), PDCode.Substring(4, 2), PDCode.Substring(6, 2))
                        objDealer.FreePPh22To = _FreePPh22To
                    Catch ex As Exception
                        writeError("Invalid [Free PPh22 To] " & PDCode)
                    End Try
                End If

                '28. LegalStatus
                PDCode = cols(28).Trim
                'If PDCode = String.Empty Then
                '    'writeError("Legal Status can't be empty")
                '    objDealer.LegalStatus = String.Empty
                'Else
                '    Try
                '        objDealer.CustomerLegal = CInt(PDCode)
                '        objDealer.LegalStatus = objDealer.DealerCode
                '    Catch
                '    End Try

                'End If
                Dim obD As Dealer = New Dealer()
                obD = New DealerFacade(user).Retrieve(objDealer.DealerCode)

                Select Case cols(3).Trim() 'organization branchType
                    Case "D"
                        Try
                            objDealer.LegalStatus = objDealer.DealerCode
                            objDealer.CustomerLegal = CInt(PDCode)
                        Catch
                        End Try
                    Case Else
                        If Not IsNothing(obD) AndAlso obD.ID > 0 Then
                            If obD.OrganizationBranchType = 1 Then
                                Try
                                    objDealer.LegalStatus = objDealer.DealerCode
                                    objDealer.CustomerLegal = CInt(PDCode)
                                Catch
                                End Try
                            End If
                        Else
                            If Not PDCode = String.Empty Then
                                Try
                                    objDealer.LegalStatus = objDealer.DealerCode
                                    objDealer.CustomerLegal = CInt(PDCode)
                                Catch

                                End Try
                            Else
                                objDealer.LegalStatus = ""
                                objDealer.CustomerLegal = Nothing
                            End If
                        End If

                End Select

                If Not IsNothing(obD) AndAlso obD.ID > 0 Then
                    If obD.CreditAccount.Trim() <> "" AndAlso objDealer.CreditAccount.Trim() = "" Then
                        objDealer.CreditAccount = obD.CreditAccount
                    End If
                End If

                ''27. DueDate
                'PDCode = cols(27).Trim
                'objDealer.DueDate = IIf(PDCode = "X", 1, 0)

                '29. AgreementNo
                PDCode = cols(29).Trim
                If PDCode = String.Empty Then
                    'writeError("Agreement Number can't be empty")
                Else
                    objDealer.AgreementNo = PDCode
                End If

                '30. AgreementDate
                PDCode = cols(30).Trim
                If PDCode = String.Empty Then
                    'writeError("Agreement Date can't be empty")
                Else
                    Dim _AgreementDate As DateTime
                    Try
                        _AgreementDate = New Date(PDCode.Substring(0, 4), PDCode.Substring(4, 2), PDCode.Substring(6, 2))
                        objDealer.AgreementDate = _AgreementDate
                    Catch ex As Exception
                        writeError("Invalid [Agreement Date] " & PDCode)
                    End Try
                End If

                objDealer.RowStatus = 0
                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    If IsNothing(objDealer) Then objDealer = New Dealer()
                    objDealer.ErrorMessage = errorMessage.ToString()
                Else
                    objDealer.CreatedBy = "SAP"
                End If

                errorMessage = New StringBuilder()
                '----------------------------------------

                Dim objDealerPajakOld As DealerPajak = New DealerPajak
                Dim objDealerBankAccountOld As DealerBankAccount = New DealerBankAccount

                Dim objDealerFacade As DealerFacade = New DealerFacade(user)
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, objDealer.DealerCode))
                Dim arlDealer As ArrayList = objDealerFacade.Retrieve(criterias)
                If Not IsNothing(arlDealer) AndAlso arlDealer.Count > 0 Then
                    Dim objDealerOld As Dealer = CType(arlDealer(0), Dealer)

                    Dim objDealerPajakFacade As DealerPajakFacade = New DealerPajakFacade(user)
                    Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerPajak), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerPajak), "Dealer.ID", MatchType.Exact, objDealerOld.ID))
                    Dim arlDealerPajak As ArrayList = objDealerPajakFacade.Retrieve(criterias2)
                    If Not IsNothing(arlDealerPajak) AndAlso arlDealerPajak.Count > 0 Then
                        objDealerPajakOld = CType(arlDealerPajak(0), DealerPajak)
                    End If
                    objDealerPajak.Dealer = objDealerOld

                    '    '32. BankAccount
                    '    Dim strBankAccount As String = String.Empty
                    '    PDCode = cols(32).Trim
                    '    strBankAccount = PDCode

                    '    Dim objDealerBankAccountFacade As DealerBankAccountFacade = New DealerBankAccountFacade(user)
                    '    Dim criterias3 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    '    criterias3.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBankAccount), "Dealer.ID", MatchType.Exact, objDealerOld.ID))
                    '    If strBankAccount <> "" Then
                    '        criterias3.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBankAccount), "BankAccount", MatchType.Exact, strBankAccount))
                    '    End If
                    '    Dim arlDealerBankAccount As ArrayList = objDealerBankAccountFacade.Retrieve(criterias3)
                    '    If Not IsNothing(arlDealerBankAccount) AndAlso arlDealerBankAccount.Count > 0 Then
                    '        objDealerBankAccountOld = CType(arlDealerBankAccount(0), DealerBankAccount)
                    '    End If
                    '    objDealerBankAccount.Dealer = objDealerOld
                End If

                '31. NPWP
                PDCode = cols(31).Trim
                If PDCode = String.Empty Then
                    'writeError("NPWP can't be empty")
                    objDealerPajak.NPWP = objDealerPajakOld.NPWP
                Else
                    objDealerPajak.NPWP = PDCode
                End If
                objDealerPajak.KPP = objDealerPajakOld.KPP
                objDealerPajak.Pejabat1 = objDealerPajakOld.Pejabat1
                objDealerPajak.Jabatan1 = objDealerPajakOld.Jabatan1
                objDealerPajak.Pejabat2 = objDealerPajakOld.Pejabat2
                objDealerPajak.Jabatan2 = objDealerPajakOld.Jabatan2
                objDealerPajak.Pejabat3 = objDealerPajakOld.Pejabat3
                objDealerPajak.Jabatan3 = objDealerPajakOld.Jabatan3
                objDealerPajak.RowStatus = 0

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    If IsNothing(objDealerPajak) Then objDealerPajak = New DealerPajak()
                    objDealerPajak.ErrorMessage = errorMessage.ToString()
                Else
                    objDealerPajak.CreatedBy = "SAP"
                End If

                '32. DealerCategory
                PDCode = cols(32).Trim
                Dim organizationBranchType As String = cols(3).Trim
                If Not organizationBranchType = "P" And Not organizationBranchType = "A" Then 'CR Body&Paint, untuk tipe 5(BP) category boleh kosong, CR PartShop, tipe 6(PS) juga boleh kosong
                    If PDCode = String.Empty Then
                        writeError("Dealer Category can't be empty")
                    Else
                        Dim func As New CategoryFacade(user)
                        Dim strValue As String = Me.GetValueDesc("DealerCategorySAP", PDCode)
                        For Each itemCode As String In strValue.Split("/")
                            Dim objDealerCategory As New DealerCategory
                            objDealerCategory.Category = func.Retrieve(itemCode.Trim)
                            objDealer.DealerCategorys.Add(objDealerCategory)
                        Next
                    End If
                End If

                '33. WSCNo
                PDCode = cols(33).Trim
                If Not PDCode = String.Empty Then
                    objDealer.WSCNO = PDCode
                Else
                    objDealer.WSCNO = String.Empty
                End If

                '34. ReconAccount
                PDCode = cols(34).Trim
                If Not PDCode = String.Empty Then
                    objDealer.ReconAccount = PDCode
                Else
                    objDealer.ReconAccount = String.Empty
                End If

                '35. SortKey
                PDCode = cols(35).Trim
                If Not PDCode = String.Empty Then
                    objDealer.SortKey = PDCode
                Else
                    objDealer.SortKey = String.Empty
                End If

                '36. CashManagementGroup
                PDCode = cols(36).Trim
                If Not PDCode = String.Empty Then
                    objDealer.CashManagementGroup = PDCode
                Else
                    objDealer.CashManagementGroup = String.Empty
                End If


                '37. PaymentBlock
                PDCode = cols(37).Trim
                If Not PDCode = String.Empty Then
                    objDealer.PaymentBlock = PDCode
                Else
                    objDealer.PaymentBlock = String.Empty
                End If

                '38. TaxCode
                PDCode = cols(38).Trim
                If Not PDCode = String.Empty Then
                    objDealer.TaxCode1 = PDCode
                Else
                    objDealer.TaxCode1 = String.Empty
                End If

                '39. EquipmentClass
                PDCode = cols(39).Trim
                If Not PDCode = String.Empty Then
                    objDealer.EquipmentClass = PDCode
                Else
                    objDealer.EquipmentClass = String.Empty
                End If

                '40. Dealer Service Grade
                PDCode = cols(40).Trim
                If Not PDCode = String.Empty Then
                    objDealer.ServiceGrade = PDCode
                Else
                    objDealer.ServiceGrade = String.Empty
                End If

                '41. Paymet Method
                PDCode = cols(41).Trim
                If Not PDCode = String.Empty Then
                    objDealer.DealerPaymentMethod = PDCode
                Else
                    objDealer.DealerPaymentMethod = String.Empty
                End If

                '42. Dealer Facility
                PDCode = cols(42).Trim
                If Not PDCode = String.Empty Then
                    objDealer.DealerFacility = PDCode
                Else
                    objDealer.DealerFacility = String.Empty
                End If

                '43. Dealer Stall Equipment
                PDCode = cols(43).Trim
                If Not PDCode = String.Empty Then
                    objDealer.DealerStallEquipment = PDCode
                Else
                    objDealer.DealerStallEquipment = String.Empty
                End If

                '44. NickNameDigital
                PDCode = cols(44).Trim
                If Not PDCode = String.Empty Then
                    objDealer.NickNameDigital = PDCode
                Else
                    objDealer.NickNameDigital = String.Empty
                End If

                '45. NickNameEcommerce
                PDCode = cols(45).Trim
                If Not PDCode = String.Empty Then
                    objDealer.NickNameEcommerce = PDCode
                Else
                    objDealer.NickNameEcommerce = String.Empty
                End If

                '46. Longitude
                PDCode = cols(46).Trim
                If Not PDCode = String.Empty Then
                    objDealer.Longitude = PDCode
                Else
                    objDealer.Longitude = String.Empty
                End If

                '47. Latitude
                PDCode = cols(47).Trim
                If Not PDCode = String.Empty Then
                    objDealer.Latitude = PDCode
                Else
                    objDealer.Latitude = String.Empty
                End If

                '48. Publish
                'P = 1
                'NP = 0
                'PDCode = cols(48).Trim
                'If Not PDCode = String.Empty Then
                '    objDealer.Publish = If(PDCode = "P", 1, 0)
                'Else
                '    objDealer.Publish = 0
                'End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    If IsNothing(objDealer) Then objDealer = New Dealer()
                    objDealer.ErrorMessage = errorMessage.ToString()
                Else
                    objDealer.CreatedBy = "SAP"
                End If



                'errorMessage = New StringBuilder()
                '----------------------------------------

                ''31. BankKey
                'PDCode = cols(31).Trim
                'If PDCode = String.Empty Then
                '    'writeError("Bank Key can't be empty")
                '    If Not IsNothing(objDealerBankAccountOld.BankKey) AndAlso objDealerBankAccountOld.BankKey.Trim <> "" Then
                '        objDealerBankAccount.BankKey = objDealerBankAccountOld.BankKey
                '    End If
                'Else
                '    objDealerBankAccount.BankKey = PDCode
                'End If
                ''32. BankAccount
                'PDCode = cols(32).Trim
                'If PDCode = String.Empty Then
                '    'writeError("Bank Account can't be empty")
                '    If Not IsNothing(objDealerBankAccountOld.BankAccount) AndAlso objDealerBankAccountOld.BankAccount.Trim <> "" Then
                '        objDealerBankAccount.BankAccount = objDealerBankAccountOld.BankAccount
                '    End If
                'Else
                '    objDealerBankAccount.BankAccount = PDCode
                'End If
                ''33. BankName
                'PDCode = cols(33).Trim
                'If PDCode = String.Empty Then
                '    'writeError("Bank Name can't be empty")
                '    If Not IsNothing(objDealerBankAccountOld.BankName) AndAlso objDealerBankAccountOld.BankName.Trim <> "" Then
                '        objDealerBankAccount.BankName = objDealerBankAccountOld.BankName
                '    End If
                'Else
                '    objDealerBankAccount.BankName = PDCode
                'End If
                'objDealerBankAccount.RowStatus = 0

                'If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                '    If IsNothing(objDealerBankAccount) Then objDealerBankAccount = New DealerBankAccount()
                '    objDealerBankAccount.ErrorMessage = errorMessage.ToString()
                'Else
                '    objDealerBankAccount.CreatedBy = "SAP"
                'End If
                'errorMessage = New StringBuilder()
                ''----------------------------------------

            End If

            Return objDealer
        End Function



        Private Function GetValueID(ByVal category As String, ByVal code As String) As Integer
            Dim result As Integer = 0
            Dim func As New StandardCodeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StandardCode), "ValueCode", MatchType.Exact, code))
            criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, category))

            Dim arrFacility As ArrayList = func.Retrieve(criterias)
            If arrFacility.Count > 0 Then
                result = CType(arrFacility(0), StandardCode).ValueId
            Else
                Return 0
            End If

            Return result
        End Function

        Private Function GetValueDesc(ByVal category As String, ByVal code As String) As String
            Dim result As String = String.Empty
            Dim func As New StandardCodeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StandardCode), "ValueCode", MatchType.Exact, code))
            criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, category))

            Dim arrFacility As ArrayList = func.Retrieve(criterias)
            If arrFacility.Count > 0 Then
                result = CType(arrFacility(0), StandardCode).ValueDesc
            Else
                Throw New Exception("Dealer Category not found.")
            End If

            Return result
        End Function

        Private Function ParseNewDetail(ByVal line As String) As DealerBankAccount
            objDealerBankAccount = New DealerBankAccount()
            Dim cols As String() = line.Split(MyBase.ColSeparator)

            If cols.Length < 3 Then
                writeError("Invalid Detail Format")
            Else
                Try
                    If Not String.IsNullorEmpty(cols(1).Trim) Then
                        objDealerBankAccount.BankKey = cols(1).Trim
                    Else
                        writeError("Bank Key can't be empty")
                    End If

                    If Not String.IsNullorEmpty(cols(2).Trim) Then
                        objDealerBankAccount.BankAccount = cols(2).Trim
                    Else
                        writeError("Bank Account can't be empty")
                    End If

                    If Not String.IsNullorEmpty(cols(3).Trim) Then
                        objDealerBankAccount.BankName = cols(3).Trim
                    Else
                        writeError("Bank Name can't be empty")
                    End If

                    If Not String.IsNullorEmpty(cols(4).Trim) Then
                        objDealerBankAccount.BeneficiaryName = cols(4).Trim

                    End If

                Catch ex As Exception
                    writeError("Bank Account is failed")
                End Try
                If Not IsNothing(errorMessage) Then
                    objDealerBankAccount.ErrorMessage = errorMessage.ToString()
                End If

                Return objDealerBankAccount
            End If

        End Function

        Private Function ParseDetail(ByVal line As String) As BusinessArea
            Dim cols As String() = line.Split(MyBase.ColSeparator)

            objBusinessArea = New BusinessArea

            If cols.Length <> 8 Then
                writeError("Invalid Detail Format")
            Else
                '1. Kind
                objBusinessArea.Kind = cols(1).Trim()

                '2. Title
                objBusinessArea.Title = cols(2).Trim()

                '3. ContactPerson
                objBusinessArea.ContactPerson = cols(3).Trim()

                '4. Email
                objBusinessArea.Email = cols(4).Trim()

                '5. Phone
                objBusinessArea.Phone = cols(5).Trim()

                '6. Fax
                objBusinessArea.Fax = cols(6).Trim()

                '7. HP
                objBusinessArea.HP = cols(7).Trim()

            End If

            If Not IsNothing(errorMessage) Then
                objBusinessArea.ErrorMessage = errorMessage.ToString()
            End If

            Return objBusinessArea
        End Function

#End Region

    End Class
End Namespace