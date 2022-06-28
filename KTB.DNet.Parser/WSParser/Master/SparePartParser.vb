#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Text

#End Region

Namespace KTB.DNet.Parser
    Public Class SparePartParser
        Inherits AbstractParser

#Region "Private Variables"
        Private sparePartMasterList As ArrayList
        Private sparePartConversionList As ArrayList
        Private sparePartMasterConversionHash As Hashtable
        Private Grammar As Regex
        Private errorMessage As StringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Dim lines As String() = MyBase.GetLines(Content)
            Dim ind As String
            Dim line As String
            Dim sparePartMaster As SparePartMaster = Nothing
            Dim sparePartConversion As SparePartConversion = Nothing
            sparePartMasterList = New ArrayList
            sparePartConversionList = New ArrayList

            Dim isDetailExist As Boolean = False
            For i As Integer = 0 To lines.Length - 1
                Try
                    line = lines(i)

                    ind = line.Split(MyBase.ColSeparator)(0)

                    If ind = MyBase.IndicatorHeader Then
                        If Not IsNothing(sparePartMaster) AndAlso isDetailExist = False Then
                            errorMessage = New StringBuilder()
                            ' create object spare part conversion
                            sparePartConversion = ParseDetail(line, sparePartMaster, False)

                            If Not IsNothing(sparePartConversion) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then
                                    sparePartConversion.ErrorMessage = errorMessage.ToString()

                                End If
                                sparePartConversionList.Add(sparePartConversion)
                                sparePartConversion = Nothing
                            End If

                        End If
                        sparePartMaster = Nothing
                        errorMessage = New StringBuilder()
                        ' create objek spare part master
                        sparePartMaster = ParseHeader(line)
                        ' insert to array objek spare part master
                        If Not IsNothing(sparePartMaster) Then
                            If errorMessage.ToString() <> String.Empty Then
                                sparePartMaster.ErrorMessage = errorMessage.ToString()
                            End If

                            sparePartMasterList.Add(sparePartMaster)
                        End If
                        isDetailExist = False
                    ElseIf ind = MyBase.IndicatorDetail Then
                        isDetailExist = True
                        errorMessage = New StringBuilder()
                        ' create object spare part conversion
                        sparePartConversion = ParseDetail(line, sparePartMaster, True)

                        If Not IsNothing(sparePartConversion) Then
                            If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then
                                sparePartConversion.ErrorMessage = errorMessage.ToString()

                            End If
                            sparePartConversionList.Add(sparePartConversion)
                            sparePartConversion = Nothing
                        End If
                    End If

                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "SparePartParser.vb", "Parsing", KTB.DNet.Lib.DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPMasterParser, BlockName)
                    Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    Throw e
                End Try
            Next
            sparePartMasterConversionHash = New Hashtable
            sparePartMasterConversionHash.Add("SparePartMaster", sparePartMasterList)
            sparePartMasterConversionHash.Add("SparePartConversion", sparePartConversionList)
            Return sparePartMasterConversionHash
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim sparePartMasterFacade As New SparePartMasterFacade(user)

            ' loop sparePartMaster array
            For Each sparePartMaster As SparePartMaster In sparePartMasterConversionHash("SparePartMaster")

                sparePartMasterFacade = New SparePartMasterFacade(user)
                Try
                    If sparePartMaster.ErrorMessage = String.Empty AndAlso IsNothing(sparePartMaster.ErrorMessage) Then
                        If sparePartMaster.ID <> 0 Then

                            ' update spare part conversion
                            Dim sparePartConversionList As ArrayList
                            Dim sparePartConversionUpdateList As ArrayList
                            sparePartConversionList = sparePartMasterConversionHash("SparePartConversion")
                            sparePartConversionUpdateList = New ArrayList

                            ' Map spare part conversion to spare part master
                            For Each sparePartConversion As SparePartConversion In sparePartConversionList
                                If Not IsNothing(sparePartConversion.SparePartMaster) Then
                                    If sparePartConversion.SparePartMaster.ID = sparePartMaster.ID Then
                                        sparePartConversionUpdateList.Add(sparePartConversion)

                                    End If
                                End If

                            Next

                            'update Spare Part Master and Spare Part conversion
                            If sparePartMasterFacade.UpdateWithTransactionManager(sparePartMaster, sparePartConversionUpdateList) < 0 Then
                                nError += 1
                            End If
                        Else
                            ' Spare Part master not found in database
                            ' insert spare part master and Spare Part Conversion

                            sparePartMaster.CreatedBy = user.Identity.Name
                            sparePartMaster.CreatedTime = DateTime.Now

                            Dim sparePartConversionList As ArrayList
                            Dim sparePartConversionInsertList As ArrayList
                            sparePartConversionList = sparePartMasterConversionHash("SparePartConversion")
                            sparePartConversionInsertList = New ArrayList

                            ' Map Spare Part Master to Spare Part Conversion
                            For Each sparePartConversion As SparePartConversion In sparePartConversionList
                                If sparePartConversion.SparePartMaster.ID = sparePartMaster.ID Then
                                    'If sparePartConversion.SparePartMaster.PartNumber = sparePartMaster.PartNumber Then
                                    sparePartConversion.CreatedBy = user.Identity.Name
                                    sparePartConversion.CreatedTime = DateTime.Now
                                    sparePartConversionInsertList.Add(sparePartConversion)
                                End If

                            Next

                            ' Insert Spare Part Master and Spare Part Conversion
                            If sparePartMasterFacade.InsertWithTransactionManager(sparePartMaster, sparePartConversionInsertList) < 0 Then
                                nError += 1
                            End If

                        End If
                    Else
                        nError += 1
                        sMsg &= sparePartMaster.ErrorMessage & ";"
                    End If

                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & sparePartMasterConversionHash("SparePartMaster").Count.ToString(), "ws-worker", "SparePartParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & sparePartMasterConversionHash("SparePartConversion").Count.ToString(), "ws-worker", "SparePartParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "SparePartParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As SparePartMaster
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim sparePartMaster As New SparePartMaster

            Dim PDCode As String
            Dim UPTCode As Boolean = False

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else

                ' Spare Part Code
                ' 2 Spare Part Code / H-2
                PDCode = cols(2).Trim
                If PDCode = String.Empty Then
                    writeError("Spare Part Code can't be empty")
                Else
                    Dim sparePartMasterCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    sparePartMasterCriteria.opAnd(New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.Exact, PDCode))
                    Dim sparePartMasterList As ArrayList = New SparePartMasterFacade(user).Retrieve(sparePartMasterCriteria)

                    If sparePartMasterList.Count > 1 Then
                        writeError("More Than 1 Spare Part Master for the same code: " & PDCode)
                    ElseIf sparePartMasterList.Count = 1 Then
                        sparePartMaster = sparePartMasterList(0)
                    ElseIf sparePartMasterList.Count = 0 Then
                        sparePartMaster = New SparePartMaster
                        sparePartMaster.MarkLoaded()
                        sparePartMaster.PartNumber = PDCode
                    End If
                End If
                ' Product Category ID
                ' 1 Product Category Code / H-1
                PDCode = cols(1).Trim
                If PDCode = String.Empty Then
                    writeError("Product Category Code can't be empty")
                Else
                    Dim productCategoryCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProductCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    productCategoryCriteria.opAnd(New Criteria(GetType(ProductCategory), "Code", MatchType.Exact, PDCode))

                    Dim productCategoryList As ArrayList = New ProductCategoryFacade(user).Retrieve(productCategoryCriteria)

                    If productCategoryList.Count > 1 Then
                        writeError("More Than 1 Spare Part Master for the same code:" & PDCode)
                    ElseIf productCategoryList.Count = 1 Then
                        sparePartMaster.ProductCategory = productCategoryList(0)
                    ElseIf productCategoryList.Count = 0 Then
                        writeError("Product Category Code not valid")
                    End If

                End If

                ' Part Name
                ' 3 Part Name / H-3
                PDCode = cols(3).Trim
                If (sparePartMaster.PartName <> PDCode) Then
                    UPTCode = True
                End If
                sparePartMaster.PartName = PDCode

                ' 4 Part Number Reff / H-4
                PDCode = cols(4).Trim
                If Not IsNothing(PDCode) AndAlso PDCode <> "" Then
                    If (sparePartMaster.PartNumberReff <> PDCode) Then
                        UPTCode = True
                    End If
                    sparePartMaster.PartNumberReff = PDCode
                ElseIf Not IsNothing(sparePartMaster.PartNumber) Then
                    If (sparePartMaster.PartNumberReff <> sparePartMaster.PartNumber) Then
                        UPTCode = True
                    End If
                    sparePartMaster.PartNumberReff = sparePartMaster.PartNumber
                Else
                    writeError("Part Number Reff and Part Number can't be empty")
                End If

                ' 5 UoM / H-5
                PDCode = cols(5).Trim
                If (sparePartMaster.UoM <> PDCode) Then
                    UPTCode = True
                End If
                sparePartMaster.UoM = PDCode

                ' 6 Alt Part Number / H-6
                PDCode = cols(6).Trim
                If (sparePartMaster.AltPartNumber <> PDCode) Then
                    UPTCode = True
                End If
                sparePartMaster.AltPartNumber = PDCode

                ' 7 Alt Part Name / H-7
                PDCode = cols(7).Trim
                If (sparePartMaster.AltPartName <> PDCode) Then
                    UPTCode = True
                End If
                sparePartMaster.AltPartName = PDCode

                ' 8 Part Code / H-8
                PDCode = cols(8).Trim
                If (sparePartMaster.PartCode <> PDCode) Then
                    UPTCode = True
                End If
                sparePartMaster.PartCode = PDCode

                ' 9 Model Code / H-9
                PDCode = cols(9).Trim
                If (sparePartMaster.ModelCode <> PDCode) Then
                    UPTCode = True
                End If
                sparePartMaster.ModelCode = PDCode

                ' 10 Supplier Code / H-10
                PDCode = cols(10).Trim
                If (sparePartMaster.SupplierCode <> PDCode) Then
                    UPTCode = True
                End If
                sparePartMaster.SupplierCode = PDCode

                ' 11 Type Code / H-11
                PDCode = cols(11).Trim
                If (sparePartMaster.TypeCode <> PDCode) Then
                    UPTCode = True
                End If
                sparePartMaster.TypeCode = PDCode

                ' 12 Stock / H-12
                PDCode = cols(12).Trim
                If Not IsNothing(PDCode) AndAlso PDCode <> "" Then
                    If IsNumeric(PDCode) Then
                        If (sparePartMaster.Stock <> PDCode) Then
                            UPTCode = True
                        End If
                        sparePartMaster.Stock = PDCode
                    Else
                        writeError("Stock must be numeric")
                    End If
                Else
                    sparePartMaster.Stock = 0
                End If



                ' 13 Retail Price / H-13
                PDCode = cols(13).Trim
                If Not IsNothing(PDCode) AndAlso PDCode <> "" Then
                    If IsNumeric(PDCode) Then
                        'Cannot be updated only can be updated on WS SPPRICE
                        '---------------------------------------------------
                        'If (sparePartMaster.RetalPrice <> CType(PDCode, Decimal)) Then
                        '    UPTCode = True
                        'End If
                        'sparePartMaster.RetalPrice = CType(PDCode, Decimal)
                    Else
                        writeError("Retail Price must be numeric")
                    End If
                Else
                    If sparePartMaster.ID = 0 Then
                        sparePartMaster.RetalPrice = 0
                    End If
                End If


                ' 14 Part Status / H-14
                PDCode = cols(14).Trim
                If (sparePartMaster.PartStatus <> PDCode) Then
                    UPTCode = True
                End If
                sparePartMaster.PartStatus = PDCode

                ' 15 Active Status / H-15
                ' X = 1, "" = 0
                PDCode = cols(15).Trim
                If PDCode.ToUpper = "X" Or PDCode = "" Or PDCode = String.Empty Then
                    'If PDCode.ToUpper = "X" Then
                    '    sparePartMaster.ActiveStatus = 1
                    'Else
                    '    sparePartMaster.ActiveStatus = 0
                    'End If
                    Dim _ActiveStatus As Short
                    'if data spareparts WS not in database Dnet then set nonactive
                    If sparePartMaster.ID = 0 Then
                        _ActiveStatus = 1
                    Else
                        If sparePartMaster.RetalPrice > 0 Then
                            If PDCode.ToUpper = "X" Then
                                _ActiveStatus = 1
                            Else
                                _ActiveStatus = 0
                            End If
                        Else
                            _ActiveStatus = 1
                        End If
                    End If

                    If _ActiveStatus <> sparePartMaster.ActiveStatus Then
                        sparePartMaster.ActiveStatus = _ActiveStatus
                        UPTCode = True
                    End If

                Else
                    writeError("Invalid input for Active Status;")
                End If


                ' 16 Accessories Type / H-16
                PDCode = cols(16).Trim
                If (sparePartMaster.AccessoriesType <> PDCode) Then
                    UPTCode = True
                End If
                sparePartMaster.AccessoriesType = PDCode

                ' 17 Product Type / H-17
                PDCode = cols(17).Trim
                If (sparePartMaster.ProductType <> PDCode) Then
                    UPTCode = True
                End If
                sparePartMaster.ProductType = PDCode

                ' 18 IsWarranty / H-18
                ' X = 1, "" = 0
                PDCode = cols(18).Trim
                If PDCode.ToUpper = "X" Or PDCode = "" Or PDCode = String.Empty Then
                    'If PDCode.ToUpper = "X" Then
                    '    sparePartMaster.IsWarranty = 1
                    'Else
                    '    sparePartMaster.IsWarranty = 0
                    'End If
                    Dim _IsWarranty As Short
                    If PDCode.ToUpper = "X" Then
                        _IsWarranty = 1
                    Else
                        _IsWarranty = 0
                    End If

                    If _IsWarranty <> sparePartMaster.IsWarranty Then
                        sparePartMaster.IsWarranty = _IsWarranty
                        UPTCode = True
                    End If

                Else
                    writeError("Invalid input for IsWarranty;")
                End If


                ' etc
                If (sparePartMaster.ID = 0) Then
                    sparePartMaster.ActiveStatus = 1
                End If

                sparePartMaster.RowStatus = 0
                If (UPTCode) Then
                    sparePartMaster.LastUpdateBy = user.Identity.Name
                    sparePartMaster.LastUpdateTime = DateTime.Now
                Else
                    sparePartMaster.LastUpdateBy = "Not Update"
                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    sparePartMaster.ErrorMessage = errorMessage.ToString() & vbCrLf & line

                End If
            End If

            Return sparePartMaster

        End Function

        Private Function ParseDetail(ByVal line As String, ByVal sparePartMaster As SparePartMaster, ByVal isDetailExist As Boolean) As SparePartConversion
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim sparePartConversion As New SparePartConversion

            Dim PDCode As String
            Dim UPTCode As Boolean = False

            errorMessage = New StringBuilder()
            If Not isDetailExist Then ' validasi colom Count
                If sparePartMaster.ID <> 0 Then

                    Dim sparePartConversionCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartConversion), "SparePartMaster", MatchType.Exact, sparePartMaster.ID))
                    Dim sparePartConversionList As ArrayList = New SparePartConversionFacade(user).Retrieve(sparePartConversionCriteria)
                    If sparePartConversionList.Count > 0 Then
                        If sparePartConversionList.Count = 1 Then
                            sparePartConversion = sparePartConversionList(0)
                            If (sparePartConversion.SparePartMaster.ID <> sparePartMaster.ID) Then
                                UPTCode = True
                            End If
                            sparePartConversion.SparePartMaster = sparePartMaster
                        Else
                            writeError("Terdapat lebih dari 2 row di database SparePartConversion sparePartMasterID: " & sparePartMaster.ID)
                        End If
                    Else
                        sparePartConversion = New SparePartConversion
                        sparePartConversion.SparePartMaster = sparePartMaster
                        sparePartConversion.MarkLoaded()
                    End If
                End If

                If (sparePartConversion.UoMTo <> sparePartMaster.UoM) Then
                    UPTCode = True
                End If
                sparePartConversion.UoMTo = sparePartMaster.UoM

                sparePartConversion.Qty = 1

                sparePartConversion.RowStatus = 0
                If (UPTCode) Then
                    sparePartConversion.LastUpdateBy = user.Identity.Name
                    sparePartConversion.LastUpdateTime = DateTime.Now
                Else
                    sparePartConversion.LastUpdateBy = "Not Update"
                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    sparePartConversion.ErrorMessage = errorMessage.ToString() & vbCrLf & line

                End If
                Return sparePartConversion
            Else
                ' 1 Spare Part Conversion
                ' check if data already exist in database
                If sparePartMaster.ID <> 0 Then
                    Dim sparePartConversionCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartConversion), "SparePartMaster", MatchType.Exact, sparePartMaster.ID))
                    Dim sparePartConversionList As ArrayList = New SparePartConversionFacade(user).Retrieve(sparePartConversionCriteria)
                    If sparePartConversionList.Count > 0 Then
                        If sparePartConversionList.Count = 1 Then
                            sparePartConversion = sparePartConversionList(0)
                            If (sparePartConversion.SparePartMaster.ID <> sparePartMaster.ID) Then
                                UPTCode = True
                            End If
                            sparePartConversion.SparePartMaster = sparePartMaster
                        Else
                            writeError("Terdapat lebih dari 2 row di database sparePartMaster ID: " & sparePartMaster.ID)
                        End If
                    Else
                        sparePartConversion.SparePartMaster = sparePartMaster
                    End If
                Else
                    sparePartConversion.SparePartMaster = sparePartMaster
                End If

                ' 2 UoMto / D-1
                PDCode = cols(1).Trim
                If Not IsNothing(PDCode) AndAlso PDCode <> "" Then
                    sparePartConversion.UoMTo = PDCode
                ElseIf Not IsNothing(sparePartMaster.UoM) Then
                    If (sparePartConversion.UoMTo <> sparePartMaster.UoM) Then
                        UPTCode = True
                    End If
                    sparePartConversion.UoMTo = sparePartMaster.UoM
                Else
                    writeError("UoM and UoMTo can't be empty")
                End If

                ' 3 Quantity / D-2
                PDCode = cols(2).Trim
                If (sparePartConversion.Qty <> MyBase.GetNumber(PDCode)) Then
                    UPTCode = True
                End If
                sparePartConversion.Qty = PDCode

                sparePartConversion.RowStatus = 0

                If (UPTCode) Then
                    sparePartConversion.LastUpdateBy = user.Identity.Name
                    sparePartConversion.LastUpdateTime = DateTime.Now
                Else
                    sparePartConversion.LastUpdateBy = "Not Update"
                End If

            End If

            If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                sparePartConversion.ErrorMessage = errorMessage.ToString() & vbCrLf & line

            End If
            Return sparePartConversion
        End Function

        Private Function isValidNumeric(ByVal stemp As String) As Boolean
            '-- Validate numeric field.
            '-- If stemp is a numeric and its value >= 0 then return True else return False
            Try
                Dim x As Long = CLng(stemp)
                If x >= 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function



#End Region

#Region "Public Properties"



#End Region

    End Class

End Namespace
