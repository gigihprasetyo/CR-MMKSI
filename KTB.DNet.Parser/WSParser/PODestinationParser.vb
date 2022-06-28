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
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic

#End Region

Namespace KTB.DNet.Parser

    Public Class PODestinationParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _aPDHs As ArrayList
        Private _oPDH As PODestination
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try
                Dim lines As String() = MyBase.GetLines(Content)
                Dim ind As String
                Dim line As String

                _aPDHs = New ArrayList()
                _oPDH = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(_oPDH) Then
                                _aPDHs.Add(_oPDH)
                                _oPDH = Nothing
                            End If

                            errorMessage = New StringBuilder()
                            _oPDH = ParseHeader(line)

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "PODestinationParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PODestinationParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _oPDH = Nothing
                    End Try
                Next

                If Not IsNothing(_oPDH) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _oPDH.ErrorMessage = errorMessage.ToString()
                    _aPDHs.Add(_oPDH)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try


            Return _aPDHs
        End Function

        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer


            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)

            Try
                'Group Data Per Dealer
                Dim objArrPoDealer = (From ObjPF As PODestination In _aPDHs
                                      Where Not IsNothing(ObjPF.Dealer) AndAlso ObjPF.Dealer.ID > 0
                        Group By ObjPF.Dealer.DealerCode Into Group
                        Select DealerCode)

                For Each strDealerCode As String In objArrPoDealer

                    'Chek Ada Error Per dealer 
                    Dim ObjCheckError = (From ObjPF As PODestination In _aPDHs
                          Where Not IsNothing(ObjPF.Dealer) AndAlso ObjPF.Dealer.ID > 0 AndAlso Not (ObjPF.ErrorMessage = String.Empty) AndAlso ObjPF.Dealer.DealerCode = strDealerCode
                          Select ObjPF).Count()

                    Dim ObjError = (From ObjPF As PODestination In _aPDHs
                        Where Not (ObjPF.ErrorMessage = String.Empty) AndAlso ObjPF.Dealer.DealerCode = strDealerCode
                        Select ObjPF)
                    If Not IsNothing(ObjCheckError) AndAlso CInt(ObjCheckError) >= 1 Then
                        nError = 1

                        For Each obj As PODestination In ObjError
                            sMsg = sMsg & obj.ErrorMessage.ToString() & vbCrLf
                        Next

                        Continue For
                    End If

                    'Filter Data per Dealer
                    Dim ObjToProcess = (From ObjPF As PODestination In _aPDHs
                       Where (ObjPF.ErrorMessage = String.Empty) AndAlso ObjPF.Dealer.DealerCode = strDealerCode
                       Select ObjPF)


                    Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODestination), "Dealer.DealerCode", MatchType.Exact, strDealerCode))


                    Dim objPODeInDB As ArrayList = New PODestinationFacade(user).Retrieve(criterias)

                    If IsNothing(objPODeInDB) OrElse objPODeInDB.Count = 0 Then
                        'Insert All ObjToProcess
                        For Each objPODestination As PODestination In ObjToProcess
                            Dim nrow = New PODestinationFacade(user).Insert(objPODestination)
                        Next

                    Else
                        'Merge
                        Dim ArrData As ArrayList = New ArrayList

                        For Each objPODestination As PODestination In ObjToProcess

                            'CHEK Code PosDest
                            Try
                                'Dim objChekExist = (From ObjPF As PODestination In objPODeInDB
                                '              Where (ObjPF.Code = objPODestination.Code)
                                '             Select ObjPF).Single() bikin error kalo pencarian nggak ada diambil data pertama

                                Dim objChekExist As PODestination = objPODeInDB.Cast(Of PODestination).FirstOrDefault(Function(x) _
                                    x.Code = objPODestination.Code)


                                If Not IsNothing(objChekExist) OrElse CType(objChekExist, PODestination).ID > 0 Then
                                    'Update Data Existing
                                    Dim obPoDInDB As New PODestination
                                    obPoDInDB = CType(objChekExist, PODestination)
                                    obPoDInDB.Nama = objPODestination.Nama
                                    obPoDInDB.Alamat = objPODestination.Alamat
                                    obPoDInDB.City = objPODestination.City
                                    obPoDInDB.RegionCode = objPODestination.RegionCode
                                    obPoDInDB.RegionDesc = objPODestination.RegionDesc
                                    obPoDInDB.LeadTime = objPODestination.LeadTime
                                    obPoDInDB.DealerDestinationCode = objPODestination.DealerDestinationCode
                                    obPoDInDB.LastUpdateBy = "WS"
                                    obPoDInDB.RowStatus = CType(DBRowStatus.Active, Short)
                                    Dim nrow = New PODestinationFacade(user).Update(obPoDInDB)
                                    ArrData.Add(obPoDInDB)
                                End If

                            Catch ex As Exception
                                'Insert Code baru
                                Dim nrow = New PODestinationFacade(user).Insert(objPODestination)
                                ArrData.Add(objPODestination)
                            End Try

                        Next


                        'Flag Deleted bagi yang tidak ada di Parser
                        Dim objDel = From objG As PODestination In objPODeInDB
                                     Where Not (From od As PODestination In ArrData
                                                 Select od.Code).Contains(objG.Code)
                                             Select objG

                        For Each objPODestination As PODestination In objDel
                            objPODestination.RowStatus = CType(DBRowStatus.Deleted, Short)
                            objPODestination.LastUpdateBy = "WS"
                            Dim nrow = New PODestinationFacade(user).Update(objPODestination)
                        Next


                    End If

                Next
            Catch ex As Exception
                nError = 1
                sMsg = ex.Message.ToString()
            End Try

            If nError = 0 Then
                Try
                    Dim ObjCheckError = (From ObjPF As PODestination In _aPDHs
                          Where (ObjPF.ErrorMessage <> String.Empty)
                          Select ObjPF).Count()

                    If Not IsNothing(ObjCheckError) AndAlso CInt(ObjCheckError) > 0 Then
                        nError = 1
                        For Each objPOD As PODestination In _aPDHs
                            If objPOD.ErrorMessage <> "" Then
                                sMsg = sMsg & vbCrLf & objPOD.ErrorMessage
                            End If
                        Next
                    End If
                Catch ex As Exception

                End Try
            End If
          
 
            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _aPDHs.Count.ToString(), "ws-worker", "PODestinationParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PODestinationParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "PODestinationParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PODestinationParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Return Nothing
        End Function

#End Region

#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String) As PODestination
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objPODestination As New PODestination
            Dim objPODestinationFac As New PODestinationFacade(user)


            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else

                Dim strData As String = String.Empty

                Try '1 Dealer COde
                    Dim PDCode As String = cols(1).Trim
                    strData = PDCode
                    If PDCode = String.Empty Then
                        objPODestination.Dealer = Nothing
                        Throw New Exception("Invalid Dealer Code " & strData)
                    Else

                        Dim ObjArr As Dealer = New DealerFacade(user).Retrieve(PDCode)
                        If Not IsNothing(ObjArr) AndAlso ObjArr.ID > 0 Then
                            objPODestination.Dealer = ObjArr
                        Else
                            Throw New Exception("Invalid Dealer Code " & strData)
                        End If

                    End If
                Catch ex As Exception
                    writeError("Dealer Code error: " & ex.Message)
                End Try

                '2 Po Dest Number
                If cols(2).Trim = String.Empty Then
                    writeError("PODestination Code can't be empty")
                Else
                    Try ' Code
                        Dim PDCode As String = cols(2).Trim
                        objPODestination.Code = cols(2).Trim
                        'Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODestination), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODestination), "Code", MatchType.Exact, PDCode))

                        'Dim ObjArr As ArrayList = objPODestinationFac.Retrieve(criterias)
                        'If Not IsNothing(ObjArr) AndAlso ObjArr.Count > 0 Then
                        '    objPODestination = CType(ObjArr(0), PODestination)
                        '    objPODestination.RowStatus = CType(DBRowStatus.Active, Short)
                        'Else
                        '    objPODestination.Code = cols(1).Trim
                        '    objPODestination.CreatedBy = "WS"
                        '    objPODestination.RowStatus = CType(DBRowStatus.Active, Short)
                        'End If
                    Catch ex As Exception
                        writeError("PoDestination COde error: " & ex.Message)
                    End Try


                    Try '3 Name
                        If cols(3).Trim = String.Empty Then
                            writeError("Name PODestination can't be empty")
                        End If

                        Dim PDCode As String = cols(3).Trim

                        objPODestination.Nama = PDCode

                    Catch ex As Exception
                        writeError("Nama error: " & ex.Message)
                    End Try

                    Try '4 Alamat
                        Dim PDCode As String = cols(4).Trim

                        objPODestination.Alamat = PDCode
                        If IsNothing(objPODestination.City) Then
                            objPODestination.Alamat = objPODestination.Alamat & " , " & cols(5).Trim()
                        End If


                    Catch ex As Exception
                        writeError("Dealer Code error: " & ex.Message)
                    End Try

                    Try '5 City COde
                        Dim PDCode As String = cols(5).Trim

                        If PDCode = String.Empty Then
                            ''    writeError("City can't be empty")
                        End If

                        If PDCode = String.Empty Then


                        Else
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.City), "Status", MatchType.Exact, "A"))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.City), "CityName", MatchType.Exact, PDCode))

                            Dim ObjArr As ArrayList = New CityFacade(user).Retrieve(criterias)


                            If Not IsNothing(ObjArr) AndAlso ObjArr.Count > 0 Then
                                objPODestination.City = CType(ObjArr(0), City)
                            Else
                                objPODestination.City = Nothing
                                'Throw New Exception("Invalid City Code")
                            End If

                        End If
                    Catch ex As Exception
                        writeError("City Code error: " & ex.Message)
                    End Try


                    Try '6 Region Code
                        Dim PDCode As String = cols(6).Trim

                        objPODestination.RegionCode = PDCode
                    Catch ex As Exception
                        writeError("Region Code error: " & ex.Message)
                    End Try


                    Try '7 Region Desc
                        Dim RDesc As String = cols(6).Trim
                        Dim lPrice As LogisticPrice = New LogisticPriceFacade(user).RetrieveByRegionCode(RDesc)
                        objPODestination.RegionDesc = lPrice.RegionDescription
                    Catch ex As Exception
                        writeError("Region Desc error: " & ex.Message)
                    End Try

                    Try '8 Lead Time
                        Dim RDesc As String = cols(7).Trim

                        objPODestination.LeadTime = RDesc
                    Catch ex As Exception
                        writeError("Region Desc error: " & ex.Message)
                    End Try

                    Dim strDealer As String = String.Empty
                    Try '9 Dealer Destination Code
                        Dim PDCode As String = cols(8).Trim
                        strDealer = PDCode
                        If PDCode = String.Empty Then
                            objPODestination.DealerDestinationCode = Nothing
                            'Throw New Exception("Invalid Dealer Destination Code " & strDealer)
                        Else

                            Dim ObjArr As Dealer = New DealerFacade(user).Retrieve(PDCode)
                            If Not IsNothing(ObjArr) AndAlso ObjArr.ID > 0 Then
                                objPODestination.DealerDestinationCode = New VWI_Dealer(ObjArr.ID)
                            Else
                                Throw New Exception("Invalid Dealer Destination Code " & strData)
                            End If

                        End If
                    Catch ex As Exception
                        writeError("Dealer Code Destination error: " & ex.Message)
                    End Try

                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then

                    objPODestination.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    objPODestination.LastUpdateBy = "WS"
                End If
            End If

            Return objPODestination
        End Function

#End Region

    End Class
End Namespace
