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
    Public Class MSPMasterParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private objMSPMaster As MSPMaster
        Private _arrMSPMaster As ArrayList
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

                _arrMSPMaster = New ArrayList()
                objMSPMaster = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            errorMessage = New StringBuilder()
                            ' create objek mspmaster
                            objMSPMaster = ParseHeader(line)
                            ' insert to array objek MSPMaster
                            If Not IsNothing(objMSPMaster) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objMSPMaster.ErrorMessage = errorMessage.ToString()
                                _arrMSPMaster.Add(objMSPMaster)
                                objMSPMaster = Nothing
                            End If

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "MSPMasterParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPMasterParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        objMSPMaster = Nothing
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return _arrMSPMaster
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim facMSPMaster As New MSPMasterFacade(user)
            For Each objMSPMaster As MSPMaster In _arrMSPMaster
                Try
                    If Not IsNothing(objMSPMaster) Then
                        If objMSPMaster.ErrorMessage = String.Empty Then
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(MSPMaster), "MSPType", MatchType.Exact, objMSPMaster.MSPType.ID))
                            criterias.opAnd(New Criteria(GetType(MSPMaster), "VehicleType", MatchType.Exact, objMSPMaster.VehicleType.ID))
                            criterias.opAnd(New Criteria(GetType(MSPMaster), "MSPKm", MatchType.Exact, objMSPMaster.MSPKm))
                            criterias.opAnd(New Criteria(GetType(MSPMaster), "Duration", MatchType.Exact, objMSPMaster.Duration))
                            criterias.opAnd(New Criteria(GetType(MSPMaster), "StartDate", MatchType.Exact, objMSPMaster.StartDate))
                            Dim sortColl As SortCollection = New SortCollection
                            sortColl.Add(New Sort(GetType(MSPMaster), "ID", Sort.SortDirection.DESC))
                            Dim MSPMasterList As ArrayList = New MSPMasterFacade(user).RetrieveByCriteria(criterias, sortColl)
                            ' update data jika hanya ada perubahan status
                            If MSPMasterList.Count > 0 Then
                                Dim old As MSPMaster = MSPMasterList(0)
                                old.Amount = objMSPMaster.Amount
                                old.EndDate = objMSPMaster.EndDate
                                If facMSPMaster.Update(old) < 0 Then
                                    nError += 1
                                End If
                            Else
                                ' update to TIDAK AKTIF
                                ' jika MSPMaster dengan param (MSPType,VehicleTypeID,MSPKm,Duration) tapi StartDate kurang dari data yang dikirim
                                criterias = New CriteriaComposite(New Criteria(GetType(MSPMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                criterias.opAnd(New Criteria(GetType(MSPMaster), "MSPType", MatchType.Exact, objMSPMaster.MSPType.ID))
                                criterias.opAnd(New Criteria(GetType(MSPMaster), "VehicleType", MatchType.Exact, objMSPMaster.VehicleType.ID))
                                criterias.opAnd(New Criteria(GetType(MSPMaster), "MSPKm", MatchType.Exact, objMSPMaster.MSPKm))
                                criterias.opAnd(New Criteria(GetType(MSPMaster), "Duration", MatchType.Exact, objMSPMaster.Duration))
                                criterias.opAnd(New Criteria(GetType(MSPMaster), "StartDate", MatchType.Lesser, objMSPMaster.StartDate))
                                sortColl = New SortCollection
                                sortColl.Add(New Sort(GetType(MSPMaster), "ID", Sort.SortDirection.DESC))
                                MSPMasterList = New MSPMasterFacade(user).RetrieveByCriteria(criterias, sortColl)
                                If MSPMasterList.Count > 0 Then
                                    For Each item As MSPMaster In MSPMasterList
                                        Dim old As MSPMaster = item
                                        old.Status = New EnumMSPMasterStatus.MSPMasterStatus().Tidak_Aktif
                                        If facMSPMaster.Update(old) < 0 Then
                                            nError += 1
                                        End If
                                    Next
                                End If
                                ' insert new data
                                objMSPMaster.Status = New EnumMSPMasterStatus.MSPMasterStatus().Aktif
                                If facMSPMaster.Insert(objMSPMaster) < 0 Then
                                    nError += 1
                                End If
                            End If
                        Else
                            Throw New Exception(objMSPMaster.ErrorMessage)
                            nError += 1
                        End If
                        
                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try

            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrMSPMaster.Count.ToString(), "ws-worker", "MSPMasterParser.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPMasterParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "MSPMasterParser.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPMasterParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function
#End Region

#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String) As MSPMaster
            ' K;MSPMaster_TimeStamp\nH;MSPType;VehicleTypeCode;MSPKm;Duration;Amount;StartDate;EndDate
            ' K;MSPMaster_20161011\nH;GOLD;NB01;40000;3;1500000;20180101;20180131;1\nH;SILVER;NB01;40000;3;1500000;20180101;20180131
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objMSPMaster As New MSPMaster
            Dim PDCode As String

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                '1 MSPType
                PDCode = cols(1).Trim
                If PDCode = String.Empty Then
                    Throw New Exception("Vehicle Type Code can't be empty")
                Else
                    Try
                        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crt.opAnd(New Criteria(GetType(MSPType), "Description", MatchType.Exact, PDCode))
                        Dim objMSPType As MSPType = New MSPTypeFacade(user).Retrieve(crt)(0)
                        If Not IsNothing(objMSPType) AndAlso objMSPType.ID > 0 Then
                            objMSPMaster.MSPType = objMSPType
                        Else
                            Throw New Exception("Invalid MSPType " & PDCode)
                        End If
                    Catch ex As Exception
                        writeError("MSPType  error: " & ex.Message)
                    End Try
                End If



                '2 Vehicle Type Code
                PDCode = cols(2).Trim
                If PDCode = String.Empty Then
                    Throw New Exception("Vehicle Type Code can't be empty")
                Else
                    Try
                        Dim objVehicleType As VechileType = New VechileTypeFacade(user).Retrieve(PDCode)
                        If Not IsNothing(objVehicleType) AndAlso objVehicleType.ID > 0 Then
                            objMSPMaster.VehicleType = objVehicleType
                        Else
                            Throw New Exception("Vehicle Type Code " & PDCode & " tidak terdaftar.")
                        End If
                    Catch ex As Exception
                        writeError("Vehicle Type  error: " & ex.Message)
                    End Try

                End If

                '3 MSPKm
                If cols(3).Trim = String.Empty Then
                    writeError("MSP Km can't be empty")
                Else
                    Try
                        objMSPMaster.MSPKm = cols(3).Trim
                    Catch ex As Exception
                        writeError("MSP Km error: " & ex.Message)
                    End Try
                End If

                '4 Duration
                If cols(4).Trim = String.Empty Then
                    writeError("Duration can't be empty")
                Else
                    objMSPMaster.Duration = cols(4).Trim
                End If

                '5 Amount
                Try
                    objMSPMaster.Amount = MyBase.GetCurrency(cols(5))
                Catch ex As Exception
                    errorMessage.Append("Invalid Amount")
                End Try

                '6 Start Date
                If objMSPMaster.StartDate.Year = 1900 Then
                    errorMessage.Append("Invalid Start Date")
                Else
                    Try
                        objMSPMaster.StartDate = MyBase.GetDateShort(cols(6))
                    Catch ex As Exception
                        errorMessage.Append("Invalid Start Date")
                    End Try
                End If

                '7 End Date
                If objMSPMaster.EndDate.Year = 1900 Then
                    errorMessage.Append("Invalid End Date")
                Else
                    Try
                        objMSPMaster.EndDate = MyBase.GetDateShort(cols(7))
                    Catch ex As Exception
                        errorMessage.Append("Invalid End Date")
                    End Try
                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    objMSPMaster.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    objMSPMaster.LastUpdateBy = "WS"
                End If
            End If

            Return objMSPMaster
        End Function
#End Region

    End Class
End Namespace