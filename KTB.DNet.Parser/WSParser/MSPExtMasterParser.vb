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
    Public Class MSPExtMasterParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private objMSPExMaster As MSPExMaster
        Private _arrMSPExMaster As ArrayList
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

                _arrMSPExMaster = New ArrayList()
                objMSPExMaster = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            errorMessage = New StringBuilder()
                            ' create objek MSPExMaster
                            objMSPExMaster = ParseHeader(line)
                            ' insert to array objek MSPExMaster
                            If Not IsNothing(objMSPExMaster) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objMSPExMaster.ErrorMessage = errorMessage.ToString()
                                _arrMSPExMaster.Add(objMSPExMaster)
                                objMSPExMaster = Nothing
                            End If

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "MSPExMasterParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPExMasterParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        objMSPExMaster = Nothing
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return _arrMSPExMaster
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim facMSPExMaster As New MSPExMasterFacade(user)
            For Each objMSPExMaster As MSPExMaster In _arrMSPExMaster
                Try
                    If Not IsNothing(objMSPExMaster) Then
                        If objMSPExMaster.ErrorMessage = String.Empty Then
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(MSPExMaster), "MSPExType", MatchType.Exact, objMSPExMaster.MSPExType.ID))
                            criterias.opAnd(New Criteria(GetType(MSPExMaster), "VechileType.ID", MatchType.Exact, objMSPExMaster.VechileType.ID))
                            criterias.opAnd(New Criteria(GetType(MSPExMaster), "MSPExKM", MatchType.Exact, objMSPExMaster.MSPExKM))
                            criterias.opAnd(New Criteria(GetType(MSPExMaster), "Duration", MatchType.Exact, objMSPExMaster.Duration))
                            criterias.opAnd(New Criteria(GetType(MSPExMaster), "StartDate", MatchType.Exact, objMSPExMaster.StartDate))
                            Dim sortColl As SortCollection = New SortCollection
                            sortColl.Add(New Sort(GetType(MSPExMaster), "ID", Sort.SortDirection.DESC))
                            Dim MSPExMasterList As ArrayList = New MSPExMasterFacade(user).RetrieveByCriteria(criterias, sortColl)
                            ' update data jika hanya ada perubahan status
                            If MSPExMasterList.Count > 0 Then
                                Dim old As MSPExMaster = MSPExMasterList(0)
                                old.Amount = objMSPExMaster.Amount
                                old.EndDate = objMSPExMaster.EndDate
                                If facMSPExMaster.Update(old) < 0 Then
                                    nError += 1
                                End If
                            Else
                                ' update to TIDAK AKTIF
                                ' jika MSPExMaster dengan param (MSPType,VehicleTypeID,MSPKm,Duration) tapi StartDate kurang dari data yang dikirim
                                criterias = New CriteriaComposite(New Criteria(GetType(MSPExMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                criterias.opAnd(New Criteria(GetType(MSPExMaster), "MSPExType", MatchType.Exact, objMSPExMaster.MSPExType.ID))
                                criterias.opAnd(New Criteria(GetType(MSPExMaster), "VechileType.ID", MatchType.Exact, objMSPExMaster.VechileType.ID))
                                criterias.opAnd(New Criteria(GetType(MSPExMaster), "MSPExKM", MatchType.Exact, objMSPExMaster.MSPExKM))
                                criterias.opAnd(New Criteria(GetType(MSPExMaster), "Duration", MatchType.Exact, objMSPExMaster.Duration))
                                criterias.opAnd(New Criteria(GetType(MSPExMaster), "StartDate", MatchType.Lesser, objMSPExMaster.StartDate))
                                sortColl = New SortCollection
                                sortColl.Add(New Sort(GetType(MSPExMaster), "ID", Sort.SortDirection.DESC))
                                MSPExMasterList = New MSPExMasterFacade(user).RetrieveByCriteria(criterias, sortColl)
                                If MSPExMasterList.Count > 0 Then
                                    For Each item As MSPExMaster In MSPExMasterList
                                        Dim old As MSPExMaster = item
                                        old.Status = EnumMSPMasterStatus.MSPMasterStatus.Tidak_Aktif
                                        If facMSPExMaster.Update(old) < 0 Then
                                            nError += 1
                                        End If
                                    Next
                                End If
                                ' insert new data
                                objMSPExMaster.Status = EnumMSPMasterStatus.MSPMasterStatus.Aktif
                                If facMSPExMaster.Insert(objMSPExMaster) < 0 Then
                                    nError += 1
                                End If
                            End If
                        Else
                            Throw New Exception(objMSPExMaster.ErrorMessage)
                            nError += 1
                        End If

                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try

            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrMSPExMaster.Count.ToString(), "ws-worker", "MSPExMasterParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPExMasterParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "MSPExMasterParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPExMasterParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object

        End Function
#End Region

#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String) As MSPExMaster
            'K;MSPExtMaster_[TimeStamp]\nH;Model;MSPExCode;StartDate[ddmmyyyy];EndDate[ddmmyyyy];Duration;KM;Amount;Status
            'K;MSPExtMaster_202009141000\nH;RN;EX01;01092020;31122099;1;10000;1200000;1

            'MSP MASTER[Confirmed]
            'MSPExtMaster_[TimeStamp]\nH;Type;MSPExCode;StartDate[ddmmyyyy];EndDate[ddmmyyyy];Duration;KM;Amount
            'Sample MSP MASTER :
            'MSPExtMaster_202009141000\nH;NA01;2XPM;01092020;31122099;1;10000;1200000;
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objMSPExMaster As New MSPExMaster
            Dim PDCode As String

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                '1 Type
                PDCode = cols(1).Trim
                If PDCode = String.Empty Then
                    Throw New Exception("Vehicle type Code can't be empty")
                Else
                    Try
                        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crt.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, PDCode))
                        Dim arVT As ArrayList = New VechileTypeFacade(user).Retrieve(crt)
                        If arVT.Count > 0 Then
                            Dim objVechileType As VechileType = arVT(0)
                            If Not IsNothing(objVechileType) AndAlso objVechileType.ID > 0 Then
                                objMSPExMaster.VechileType = objVechileType
                            Else
                                Throw New Exception("Invalid Vehicle type " & PDCode)
                            End If
                        Else
                            Throw New Exception("Invalid Vehicle type " & PDCode)
                        End If
                    Catch ex As Exception
                        writeError("Vehicle type error: " & PDCode)
                    End Try
                End If


                '2 MSPExCode
                PDCode = cols(2).Trim
                If PDCode = String.Empty Then
                    Throw New Exception("MSP Extended Type Code can't be empty")
                Else
                    Try
                        Dim objMSPExType As MSPExType = New MSPExTypeFacade(user).Retrieve(PDCode)
                        If Not IsNothing(objMSPExType) AndAlso objMSPExType.ID > 0 Then
                            objMSPExMaster.MSPExType = objMSPExType
                        Else
                            Throw New Exception("MSP Extended Type Code " & PDCode & " tidak terdaftar.")
                        End If
                    Catch ex As Exception
                        writeError("MSP Extended Type Code error: " & PDCode)
                    End Try

                End If

                '3 StartDate
                Try
                    objMSPExMaster.StartDate = GetShortDate(cols(3))
                Catch ex As Exception
                    errorMessage.Append("Invalid Start Date")
                End Try

                '4 EndDate
                Try
                    objMSPExMaster.EndDate = GetShortDate(cols(4))
                Catch ex As Exception
                    errorMessage.Append("Invalid End Date")
                End Try

                '5 Duration
                If cols(5).Trim = String.Empty Then
                    writeError("Duration can't be empty")
                Else
                    objMSPExMaster.Duration = cols(5).Trim
                End If


                '6 KM
                If cols(6).Trim = String.Empty Then
                    writeError("MSP Km can't be empty")
                Else
                    Try
                        objMSPExMaster.MSPExKM = cols(6).Trim
                    Catch ex As Exception
                        writeError("MSP Km error: " & PDCode)
                    End Try
                End If

                '7 Amount
                Try
                    objMSPExMaster.Amount = MyBase.GetCurrency(cols(7))
                Catch ex As Exception
                    errorMessage.Append("Invalid Amount")
                End Try

                ''8 Status
                'If cols(8).Trim = String.Empty Then
                '    writeError("Status can't be empty")
                'Else
                '    Try
                '        objMSPExMaster.Status = cols(8).Trim
                '    Catch ex As Exception
                '        writeError("Status error: " & ex.Message)
                '    End Try
                'End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    objMSPExMaster.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    objMSPExMaster.LastUpdateBy = "WS"
                End If
            End If

            Return objMSPExMaster
        End Function

        Protected Function GetShortDate(ByVal str As String) As Date
            Dim dt As Date 'ddMMYYYY

            Try
                dt = New Date(Integer.Parse(str.Substring(4, 4)), Integer.Parse(str.Substring(2, 2)), Integer.Parse(str.Substring(0, 2)))
            Catch ex As Exception
                dt = DateSerial(1900, 1, 1)
            End Try
            Return dt
        End Function
#End Region

    End Class
End Namespace