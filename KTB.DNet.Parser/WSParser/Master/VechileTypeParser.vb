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
    Public Class VechileTypeParser
        Inherits AbstractParser

        'Kesalahan PENULISAN NAMA Dikarenakan menjaga konsistensi


#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _arrVechileType As ArrayList
        Private _arrVechileColor As ArrayList
        Private _hashVechileTypeColor As Hashtable


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

                _arrVechileType = New ArrayList()
                _arrVechileColor = New ArrayList()
                _hashVechileTypeColor = New Hashtable()

                Dim objVechileColor As VechileColor = Nothing
                Dim objVechileType As VechileType = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            objVechileType = Nothing
                            errorMessage = New StringBuilder()
                            ' create objek mspmaster
                            objVechileType = ParseHeader(line)
                            ' insert to array objek MSPMaster
                            If Not IsNothing(objVechileType) Then
                                If errorMessage.ToString() <> String.Empty Then
                                    objVechileType.ErrorMessage = errorMessage.ToString()
                                End If

                                _arrVechileType.Add(objVechileType)
                            End If
                        ElseIf ind = MyBase.IndicatorDetail Then
                            errorMessage = New StringBuilder()

                            ' create object vehicle color
                            objVechileColor = ParseDetail(line, objVechileType)

                            If Not IsNothing(objVechileColor) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then
                                    objVechileColor.ErrorMessage = errorMessage.ToString()

                                End If
                                _arrVechileColor.Add(objVechileColor)
                                objVechileColor = Nothing
                            End If
                        End If


                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "VechileTypeParser.vb", "Parsing", KTB.DNet.Lib.DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPMasterParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        Throw e
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            _hashVechileTypeColor.Add("VehicleType", _arrVechileType)
            _hashVechileTypeColor.Add("VehicleColor", _arrVechileColor)

            Return _hashVechileTypeColor
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)


            'loop Header (Vehicle Type)
            For Each objVechileType As VechileType In _hashVechileTypeColor("VehicleType")
                Dim facVehicleType As New VechileTypeFacade(user)
                Try
                    If Not IsNothing(objVechileType) Then
                        If IsNothing(objVechileType) OrElse objVechileType.ErrorMessage = String.Empty Then
                            ' Check if vehicle type is already in database
                            ' Vehicle Type found in Database

                            If objVechileType.ID <> 0 Then

                                ' update vehicle color
                                Dim colorList As ArrayList
                                Dim colorListUpdate As ArrayList
                                colorList = _hashVechileTypeColor("VehicleColor")
                                colorListUpdate = New ArrayList

                                ' Map Color to Type
                                For Each vechileColor As VechileColor In colorList
                                    If Not IsNothing(vechileColor.VechileType) Then
                                        If vechileColor.VechileType.VechileTypeCode = objVechileType.VechileTypeCode Then
                                            colorListUpdate.Add(vechileColor)

                                        End If
                                    End If

                                Next

                                'update VehicleType and VehicleColor
                                If facVehicleType.UpdateWithTransactionManager(objVechileType, colorListUpdate) < 0 Then
                                    nError += 1
                                End If

                            Else
                                ' Vehicle Type not found in database
                                ' insert vehicle type and vehicle color
                                objVechileType.Status = "A"
                                objVechileType.VehicleClass = Nothing
                                objVechileType.IsVehicleKind1 = 0
                                objVechileType.IsVehicleKind2 = 0
                                objVechileType.IsVehicleKind3 = 0
                                objVechileType.IsVehicleKind4 = 0
                                objVechileType.MaxTOPDays = 0

                                objVechileType.CreatedBy = user.Identity.Name
                                objVechileType.CreatedTime = DateTime.Now

                                Dim colorList As ArrayList
                                Dim colorListInsert As ArrayList
                                colorList = _hashVechileTypeColor("VehicleColor")
                                colorListInsert = New ArrayList

                                ' Map Color to Type
                                For Each vechileColor As VechileColor In colorList
                                    If vechileColor.VechileType.VechileTypeCode = objVechileType.VechileTypeCode Then
                                        vechileColor.CreatedBy = user.Identity.Name
                                        vechileColor.CreatedTime = DateTime.Now
                                        colorListInsert.Add(vechileColor)
                                    End If

                                Next

                                ' Insert Vehicle Type and Vehicle Color
                                If facVehicleType.InsertWithTransactionManager(objVechileType, colorListInsert) < 0 Then
                                    nError += 1
                                End If

                            End If
                        Else
                            sMsg &= "Data Vehicle Type Code " & objVechileType.VechileTypeCode & " " & objVechileType.ErrorMessage & ";\n"
                            nError += 1
                        End If
                    Else
                        Throw New Exception(objVechileType.ErrorMessage)
                        nError += 1
                    End If


                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try

            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrVechileType.Count.ToString(), "ws-worker", "VechileTypeParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.VechileTypeParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrVechileType.Count.ToString(), "ws-worker", "VechileTypeParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.VechileTypeParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "VechileTypeParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.VechileTypeParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                '//Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String) As VechileType
            ' K;MASTERVARIANT_timestamp\nH;StringH-1;StringH-2;StringH-3;StringH-4;StringH-5;StringH-6;StringH-7;StringH-8;
            ' StringH-9;StringH-10;StringH-11\nD;StringD-1.1;StringD-1.2;StringD-1.3;StringD-1.4;StringD-1.5;
            ' StringD-1.6;StringD-1.7;StringD-1.8\nD;StringD-2.1;StringD-2.2;StringD-2.3;StringD-2.4;StringD-2.5;StringD-2.6;StringD-2.7;StringD-2.8\n

            ' K;MASTERVARIANT_20180810112801\nH;BK11;001001018;MMC;PAJERO SPORT 2.5L EXCEED 4X2 5M/T;LC-PAJERO;SUV;EXCEED;A/T;
            ' 4x2;5;DIESEL\nD;BK11MDBM;HITAM KIKA;DIAMOND BLACK MIKA;PAJERO SPORT 2.5L EXCEED(4X2) 5M/T BLACK;BK11MDBM;;;\nD;BK11MPWP;PUTIH MUTIARA;WHITE PEARL;PAJERO SPORT 2.5L EXCEED(4X2) 5M/T WHITE;BK11MPWP;;X;X\n
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objVechileType As VechileType

            Dim PDCode As String
            Dim PDDesc As String
            Dim UPTCode As Boolean = False

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                ' VehicleType
                ' 1 VechileTypeCode / H-1
                PDCode = cols(1).Trim
                If PDCode = String.Empty Then
                    objVechileType = New VechileType
                    objVechileType.ErrorMessage = "Vehicle Type Code can't be empty"
                    'Throw New Exception("Vehicle Type Code can't be empty")
                Else
                    Dim vechileTypeCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, PDCode))
                    vechileTypeCriteria.opAnd(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, 0))
                    Dim objVechileTypeList As ArrayList = New VechileTypeFacade(user).Retrieve(vechileTypeCriteria)

                    If objVechileTypeList.Count > 1 Then
                        writeError("Vehicle Type Code Data More Than 1 for the same code:" & PDCode)
                        objVechileType = New VechileType()
                        objVechileType.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                        Return objVechileType
                    ElseIf objVechileTypeList.Count = 1 Then
                        objVechileType = objVechileTypeList(0)
                        objVechileType.IsActiveOnPK = objVechileType.IsActiveOnPK
                    ElseIf objVechileTypeList.Count = 0 Then
                        objVechileType = New VechileType
                        objVechileType.MarkLoaded()
                        objVechileType.VechileTypeCode = PDCode
                        If objVechileType.Status.ToUpper = "A" Then
                            objVechileType.IsActiveOnPK = 1
                        Else
                            objVechileType.IsActiveOnPK = 0
                        End If
                    End If
                End If


                If (Not IsNothing(objVechileType)) Then
                    ' 2 Model ID/SAPCode in VehicleModel + 3 Category / H-2
                    PDCode = cols(2).Trim
                    ' 3 Vehicle Mode Description / H-3
                    PDDesc = cols(3).Trim
                    If PDCode = String.Empty And PDDesc = String.Empty Then
                        objVechileType.ErrorMessage = "Vehicle Model Code:" & PDCode & "And Description Model:" & PDDesc & "can't be empty"
                        'Throw New Exception("Vehicle Model Code:" & PDCode & "And Description Model:" & PDDesc & "can't be empty")
                    Else
                        Try
                            Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crt.opAnd(New Criteria(GetType(VechileModel), "VechileModelCode", MatchType.Exact, PDCode.Trim))
                            crt.opAnd(New Criteria(GetType(VechileModel), "Description", MatchType.Exact, PDDesc.Trim))
                            Dim vechileModelList As ArrayList = New VechileModelFacade(user).Retrieve(crt)
                            If vechileModelList.Count > 0 Then
                                If vechileModelList.Count = 1 Then
                                    Dim objVechileModel As VechileModel = CType(vechileModelList(0), VechileModel) 'New VechileModelFacade(user).Retrieve(crt)(0)
                                    If Not IsNothing(objVechileModel) Then
                                        objVechileType.VechileModel = objVechileModel
                                        ' Category
                                        objVechileType.Category = objVechileModel.Category
                                        objVechileType.ProductCategory = objVechileModel.Category.ProductCategory
                                    Else
                                        Throw New Exception("Invalid Vehicle Model Code " & PDCode.Trim)
                                    End If
                                Else
                                    Throw New Exception("Terdapat lebih dari 1 row untuk Vehicle Model Code " & PDCode.Trim)
                                End If
                            Else
                                Throw New Exception("Invalid Vehicle Model Code " & PDCode.Trim)
                            End If

                        Catch ex As Exception
                            writeError("Vehicle Model  error: " & ex.Message)
                        End Try
                    End If

                    ' 4 Product Category ID / H-3
                    ' Not used, using Product Category from Vehicle Model
                    'PDCode = cols(3).Trim
                    'If PDCode = String.Empty Then
                    '    writeError("Product Category can't be empty")
                    'Else
                    '    Try
                    '        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProductCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    '        crt.opAnd(New Criteria(GetType(ProductCategory), "Code", MatchType.Exact, PDCode.Trim))
                    '        Dim objProductCategory As ProductCategory = New ProductCategoryFacade(user).Retrieve(crt)(0)
                    '        If Not IsNothing(objProductCategory) Then
                    '            objVechileType.ProductCategory = objProductCategory
                    '        Else
                    '            Throw New Exception("Invalid Product Category" & PDCode.Trim)
                    '        End If
                    '    Catch ex As Exception
                    '        writeError("Product Category  error: " & ex.Message)
                    '    End Try


                    'End If

                    ' 5 Description / H-5
                    PDCode = cols(5).Trim
                    'If (PDCode <> String.Empty) Then
                    If (objVechileType.Description <> PDCode) Then
                        UPTCode = True
                        objVechileType.Description = PDCode
                    End If
                    'End If

                    ' 13 SAP Model / H-6
                    PDCode = cols(6).Trim
                    'If (PDCode <> String.Empty) Then
                    If (objVechileType.SAPModel <> PDCode) Then
                        UPTCode = True
                        objVechileType.SAPModel = PDCode
                    End If
                    'End If

                    ' 14 Segment Type / H-7
                    PDCode = cols(7).Trim
                    'If (PDCode <> String.Empty) Then
                    If (objVechileType.SegmentType <> PDCode) Then
                        UPTCode = True
                        objVechileType.SegmentType = PDCode
                    End If
                    'End If

                    ' 15 Variant Type / H-8
                    PDCode = cols(8).Trim
                    'If (PDCode <> String.Empty) Then
                    If (objVechileType.VariantType <> PDCode) Then
                        UPTCode = True
                        objVechileType.VariantType = PDCode
                    End If
                    'End If

                    ' 16 Transmit Type / H-9
                    PDCode = cols(9).Trim
                    'If (PDCode <> String.Empty) Then
                    If (objVechileType.TransmitType <> PDCode) Then
                        UPTCode = True
                        objVechileType.TransmitType = PDCode
                    End If
                    'End If

                    ' 17 Drive System Type / H-10
                    PDCode = cols(10).Trim
                    'If (PDCode <> String.Empty) Then
                    If (objVechileType.DriveSystemType <> PDCode) Then
                        UPTCode = True
                        objVechileType.DriveSystemType = PDCode
                    End If
                    'End If

                    ' 18 Speed Type / H-11
                    PDCode = cols(11).Trim
                    'If (PDCode <> String.Empty) Then
                    If (objVechileType.SpeedType <> PDCode) Then
                        UPTCode = True
                        objVechileType.SpeedType = PDCode
                    End If
                    'End If
                    ' 19 Fuel Type / H-12
                    PDCode = cols(12).Trim
                    'If (PDCode <> String.Empty) Then
                    If (objVechileType.FuelType <> PDCode) Then
                        UPTCode = True
                        objVechileType.FuelType = PDCode
                    End If
                    'End If
                End If
                ' 23 24
                objVechileType.RowStatus = 0
                If (UPTCode) Then
                    objVechileType.LastUpdateBy = user.Identity.Name
                    objVechileType.LastUpdateTime = DateTime.Now
                Else
                    'If Data objVechileType Same Not Update
                    objVechileType.LastUpdateBy = "Not Update"
                End If


                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    objVechileType.ErrorMessage = errorMessage.ToString() & vbCrLf & line

                End If
            End If

            Return objVechileType
        End Function

        Private Function ParseDetail(ByVal line As String, ByVal vechileType As VechileType) As VechileColor
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objVechileColor As VechileColor

            Dim PDCode As String
            Dim UPTCode As Boolean = False

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                ' VehicleColor
                PDCode = cols(1).Trim
                Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "MaterialNumber", MatchType.Exact, PDCode))
                crt.opAnd(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, 0))
                Dim objVechileColorOld As ArrayList = New VechileColorFacade(user).Retrieve(crt)

                If objVechileColorOld.Count > 1 Then
                    writeError("Vehicle Color Data More Than 1 for the same code : " & PDCode)
                    objVechileColor = New VechileColor()
                    objVechileColor.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                    Return objVechileColor
                ElseIf objVechileColorOld.Count = 1 Then
                    objVechileColor = objVechileColorOld(0)
                    If (objVechileColor.VechileType.ID <> vechileType.ID) Then
                        UPTCode = True
                    End If
                    objVechileColor.VechileType = vechileType
                ElseIf objVechileColorOld.Count = 0 Then
                    objVechileColor = New VechileColor
                    objVechileColor.VechileType = vechileType

                End If

                If (Not IsNothing(objVechileColor)) Then
                    ' 2 Color Code / D-n.1 Last 4 Digit
                    PDCode = cols(1).Trim
                    'If (PDCode <> String.Empty) Then
                    If (objVechileColor.ColorCode <> PDCode.Substring(4)) Then
                        UPTCode = True
                        objVechileColor.ColorCode = PDCode.Substring(4)
                    End If
                    'End If

                    ' 3 Color Ind Name / D-n.2 
                    PDCode = cols(2).Trim
                    'If (PDCode <> String.Empty) Then
                    If (objVechileColor.ColorIndName <> PDCode) Then
                        UPTCode = True
                        objVechileColor.ColorIndName = PDCode
                    End If
                    'End If

                    ' 4 Color Eng Name / D-n.3
                    PDCode = cols(3).Trim
                    'If (PDCode <> String.Empty) Then
                    If (objVechileColor.ColorEngName <> PDCode) Then
                        UPTCode = True
                        objVechileColor.ColorEngName = PDCode
                    End If
                    'End If

                    ' 5 Material Number / D-n.1
                    PDCode = cols(1).Trim
                    If PDCode = String.Empty Then
                        writeError("Material Number can't be empty")
                    Else
                        If (objVechileColor.MaterialNumber <> PDCode) Then
                            UPTCode = True
                            objVechileColor.MaterialNumber = PDCode
                        End If
                    End If

                    ' 6 Material Description / D-n.4
                    PDCode = cols(4).Trim
                    If (objVechileColor.MaterialDescription <> PDCode) Then
                        UPTCode = True
                    End If
                    objVechileColor.MaterialDescription = PDCode

                    ' 7 Header BOM / D-n.5
                    PDCode = cols(5).Trim

                    PDCode = cols(5).Trim
                    If Not String.IsNullOrEmpty(PDCode) Then
                        objVechileColor.HeaderBOM = PDCode
                        UPTCode = True
                    Else
                        If String.IsNullOrEmpty(objVechileColor.HeaderBOM) Then
                            objVechileColor.HeaderBOM = objVechileColor.MaterialNumber
                        End If
                        UPTCode = True
                    End If

                    ' 8 Market Code / D-n.6
                    PDCode = cols(6).Trim
                    'If (PDCode <> String.Empty) Then
                    If (objVechileColor.MarketCode <> PDCode) Then
                        UPTCode = True
                        objVechileColor.MarketCode = PDCode
                    End If
                    'End If

                    ' 9 Special Flag / D-n.7
                    PDCode = cols(7).Trim
                    'If (PDCode <> String.Empty) Then
                    If (objVechileColor.SpecialFlag <> PDCode) Then
                        UPTCode = True
                        objVechileColor.SpecialFlag = PDCode
                    End If
                    'End If

                    ' 10 Status / D-n.8
                    PDCode = cols(8).Trim
                    'If (PDCode <> String.Empty) Then
                    If (objVechileColor.Status <> PDCode) Then
                        UPTCode = True
                        objVechileColor.Status = PDCode
                    End If
                    'End If
                    '' 14 15
                    objVechileColor.RowStatus = 0
                    If (UPTCode) Then
                        objVechileColor.LastUpdateBy = user.Identity.Name
                        objVechileColor.LastUpdateTime = DateTime.Now
                    Else
                        'If Data objVechileColor Same Not Update
                        objVechileColor.LastUpdateBy = "Not Update"
                    End If
            End If
            End If

            If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                objVechileColor.ErrorMessage = errorMessage.ToString() & vbCrLf & line
            End If
            Return objVechileColor

        End Function
#End Region
    End Class
End Namespace
