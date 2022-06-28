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
    Public Class VechileModelParser
        Inherits AbstractParser

        'Kesalahan PENULISAN NAMA Dikarenakan menjaga konsistensi


#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private objVechileModel As VechileModel
        Private _arrVechileModel As ArrayList
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
                Dim nError As Integer = 0
                Dim errMsgSummary As String = String.Empty

                _arrVechileModel = New ArrayList()
                objVechileModel = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind.Trim = MyBase.IndicatorHeader Then
                            errorMessage = New StringBuilder()
                            ' create objek mspmaster
                            objVechileModel = ParseHeader(line)
                            ' insert to array objek MSPMaster
                            If Not IsNothing(objVechileModel) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objVechileModel.ErrorMessage = errorMessage.ToString()
                                _arrVechileModel.Add(objVechileModel)
                                objVechileModel = Nothing
                            End If

                        End If
                    Catch ex As Exception
                        nError += 1
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "VechileModelParser.vb", "Parsing", KTB.DNet.Lib.DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPMasterParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _arrVechileModel = Nothing
                        errMsgSummary = ex.Message & ";"
                    End Try
                Next

                If nError > 0 Then
                    Throw New Exception(errMsgSummary)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return _arrVechileModel
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim facVehicleModel As New VechileModelFacade(user)
            For Each objVechileModel As VechileModel In _arrVechileModel
                Try
                    If Not IsNothing(objVechileModel) Then
                        If objVechileModel.ErrorMessage = String.Empty Then
                            'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileModel), "VechileModelCode", MatchType.Exact, objVechileModel.VechileModelCode))
                            criterias.opAnd(New Criteria(GetType(VechileModel), "Description", MatchType.Exact, objVechileModel.Description))
                            'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileModel), "SAPCode", MatchType.Exact, objVechileModel.SAPCode))
                            Dim VechileModelOldList As ArrayList = New VechileModelFacade(user).RetrieveByCriteria(criterias, Nothing)

                            If VechileModelOldList.Count > 0 Then
                                If VechileModelOldList.Count = 1 Then
                                    Dim vechileModelOld As VechileModel = VechileModelOldList(0)
                                    'vechileModelOld.SAPCode = objVechileModel.SAPCode
                                    vechileModelOld.VechileModelCode = objVechileModel.VechileModelCode 
                                    vechileModelOld.Description = objVechileModel.Description
                                    vechileModelOld.VechileModelIndCode = objVechileModel.VechileModelIndCode
                                    vechileModelOld.IndDescription = objVechileModel.IndDescription
                                    If vechileModelOld.RowStatus = DBRowStatus.Deleted Then
                                        vechileModelOld.RowStatus = DBRowStatus.Active
                                    End If

                                    If facVehicleModel.Update(vechileModelOld) < 0 Then
                                        nError += 1
                                    End If
                                Else
                                    sMsg &= "Data Vehicle Model Code " & objVechileModel.VechileModelCode & " terdapat lebih dari satu row" & ";"
                                    nError += 1
                                End If
                            Else
                                If facVehicleModel.Insert(objVechileModel) < 0 Then
                                    nError += 1
                                End If
                            End If
                        Else
                            Throw New Exception(objVechileModel.ErrorMessage)
                            nError += 1
                        End If

                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try

            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrVechileModel.Count.ToString(), "ws-worker", "VechileModelParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.VechileModelParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "VechileModelParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.VechileModelParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As VechileModel
            ' K;MASTERPRODUCTHIERARCHY_timestamp\nH;StringH-1;StringH-2;StringH-3;StringH-4;StringH-5;\n
            ' K;MASTERPRODUCTHIERARCHY_20180810112801\nH;001001018;QX;PAJERO SPORT;PS;PAJERO SPORT;\n
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objVechileModel As New VechileModel

            Dim PDCode As String

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                '1 SAPCode and Category
                PDCode = cols(1).Trim
                If PDCode = String.Empty Then
                    writeError("SAP Code can't be empty")
                Else
                    'objVechileModel.SAPCode = PDCode.Trim()

                    Try
                        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crt.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, "MMC"))
                        crt.opAnd(New Criteria(GetType(Category), "ID", MatchType.Exact, PDCode.Substring(5, 1)))
                        Dim objCategory As Category = New CategoryFacade(user).Retrieve(crt)(0)
                        If Not IsNothing(objCategory) Then
                            objVechileModel.Category = objCategory

                        Else
                            Throw New Exception("Invalid Category " & PDCode.Substring(5))
                        End If
                    Catch ex As Exception
                        writeError("Category  error: " & ex.Message)
                    End Try
                End If

                '2 Vehicle Model Code
                PDCode = cols(2).Trim
                If PDCode = String.Empty Then
                    writeError("Vehicle Model Code can't be empty")
                Else
                    objVechileModel.VechileModelCode = PDCode.Trim()

                End If

                '3 Description
                PDCode = cols(3).Trim
                If PDCode = String.Empty Then
                    writeError("Vehicle Model Description can't be empty")
                Else
                    objVechileModel.Description = PDCode
                End If

                '4 VechileIndModel
                PDCode = cols(4).Trim
                If PDCode = String.Empty Then
                    writeError("Vehicle Model Ind Code can't be empty")
                Else
                    objVechileModel.VechileModelIndCode = PDCode
                End If

                '5 IndDescription
                PDCode = cols(5).Trim
                If PDCode = String.Empty Then
                    writeError("Vehicle Model Ind Description can't be empty")
                Else
                    objVechileModel.IndDescription = PDCode
                End If


                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    objVechileModel.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    objVechileModel.LastUpdateBy = user.Identity.Name
                End If
            End If

            Return objVechileModel
        End Function
#End Region
    End Class
End Namespace
