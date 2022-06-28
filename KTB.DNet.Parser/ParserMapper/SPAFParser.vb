#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Text
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Configuration
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SPAF
Imports KTB.DNet.DataMapper
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser
    Public Class SPAFParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _SPAFDocs As ArrayList
        Private Grammar As Regex
        Private mapper As IMapper
        Private objTransactionManager As TransactionManager
        Private _fileName As String
        Private sBuilder As StringBuilder
        Private _sesshelper As New SessionHelper
        Dim _Dealer As String = String.Empty
        Private objCMFacade As ChassisMasterFacade
        Private _objDealer As Dealer
        Private pphf As PPhFacade = New PPhFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
        Private _NumUploadError As Integer = 0
        Private _objSDUH As SPAFDoc_UploadHeader = New SPAFDoc_UploadHeader
        Dim _objSDUHFac As SPAFDoc_UploadHeaderFacade = New SPAFDoc_UploadHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
        Dim _objSDUDFac As SPAFDoc_UploadDetailFacade = New SPAFDoc_UploadDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser(";")
            objCMFacade = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            _objDealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve("dsf") 's '  CType(_sesshelper.GetSession("Dealer"), Dealer)
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            Dim oSPAFDoc As SPAFDoc
            Dim oSDUD As SPAFDoc_UploadDetail
            Dim arlTemp As New ArrayList
            _Stream = New StreamReader(fileName, True)
            _SPAFDocs = New ArrayList
            _fileName = fileName


            _Dealer = _objDealer.DealerCode.Trim.ToLower
            If Not SaveHeader() Then Exit Function
            Dim val As String = MyBase.NextLine(_Stream).Trim()
            While (Not val = "")
                Try
                    oSPAFDoc = New SPAFDoc
                    oSPAFDoc = ParseSPAFDoc(val + ";")
                    arlTemp = New ArrayList
                    arlTemp.Add(oSPAFDoc)
                    AddDetail(arlTemp)
                    oSDUD = Me.ConvertSPAFDocToSPAFDoc_UploadDetail(oSPAFDoc)
                    Me.InsertSDUD(oSDUD)
                    If Not IsNothing(oSDUD.ErrorMessage) AndAlso oSDUD.ErrorMessage.Trim <> "" Then
                        '_NumUploadError += 1
                        Me._objSDUH.NumberOfError += 1 'Me._NumUploadError
                    Else
                        Me._objSDUH.NumberOfValid += 1
                    End If

                    Me._objSDUH.NumberOfData = Me._objSDUH.NumberOfError + Me._objSDUH.NumberOfValid
                    Me._objSDUHFac.Update(Me._objSDUH)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "SPAFParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.SPAFParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing

            Me._objSDUH.Status = enumSPAFUpload.SPAFUpload.Finish
            Me._objSDUHFac.Update(_objSDUH)

            Return _SPAFDocs
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Return 0
            Exit Function

            Dim oSDUHFac As SPAFDoc_UploadHeaderFacade = New SPAFDoc_UploadHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim oSDUDFac As SPAFDoc_UploadDetailFacade = New SPAFDoc_UploadDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim cSDUH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPAFDoc_UploadHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim oSDUH As SPAFDoc_UploadHeader
            Dim oSDUD As SPAFDoc_UploadDetail
            Dim arlSDUH As New ArrayList
            Dim ID As Integer
            Dim finfo As FileInfo = New FileInfo(_fileName)
            Dim nError As Integer

            cSDUH.opAnd(New Criteria(GetType(SPAFDoc_UploadHeader), "Dealer.ID", MatchType.Exact, Me._objDealer.ID))
            cSDUH.opAnd(New Criteria(GetType(SPAFDoc_UploadHeader), "Filename", MatchType.Exact, finfo.Name))
            arlSDUH = oSDUHFac.Retrieve(cSDUH)
            If arlSDUH.Count > 0 Then
                oSDUH = CType(arlSDUH(0), SPAFDoc_UploadHeader)
            Else
                Exit Function
            End If
            'If arlSDUH.Count > 0 Then
            '    oSDUH = CType(arlSDUH(0), SPAFDoc_UploadHeader)
            '    oSDUH.Status = enumSPAFUpload.SPAFUpload.Initialization
            '    oSDUHFac.Update(oSDUH)
            '    For Each oSDUDTemp As SPAFDoc_UploadDetail In oSDUH.SPAFDoc_UploadDetails
            '        oSDUDFac.DeleteFromDB(oSDUDTemp)
            '    Next
            'Else
            '    oSDUH = New SPAFDoc_UploadHeader
            '    oSDUH.Filename = finfo.Name
            '    oSDUH.Dealer = Me._objDealer
            '    oSDUH.NumberOfData = _SPAFDocs.Count
            '    ID = oSDUHFac.Insert(oSDUH)
            '    If ID < 1 Then
            '        Exit Function
            '    Else
            '        oSDUH.ID = ID
            '        oSDUH.Status = enumSPAFUpload.SPAFUpload.Initialization
            '    End If
            'End If
            nError = 0
            AddDetail(_SPAFDocs)
            For Each oSD As SPAFDoc In _SPAFDocs
                oSDUD = New SPAFDoc_UploadDetail
                With oSDUD
                    .AlasanPenolakan = oSD.AlasanPenolakan
                    .ChassisMaster = oSD.ChassisMaster
                    .CreatedBy = oSD.CreatedBy
                    .CreatedTime = oSD.CreatedTime
                    .CustomerName = oSD.CustomerName
                    .DateLetter = oSD.DateLetter
                    .Dealer = oSD.Dealer
                    .DealerLeasing = oSD.DealerLeasing
                    .DocType = oSD.DocType
                    .LastUpdateBy = oSD.LastUpdateBy
                    .LastUpdateTime = oSD.LastUpdateTime
                    .OrderDealer = oSD.OrderDealer
                    .PostingDate = oSD.PostingDate
                    .PPhPercent = oSD.PPhPercent
                    .ReffLetter = oSD.ReffLetter
                    .RetailPrice = oSD.RetailPrice
                    .RowStatus = oSD.RowStatus
                    .SellingType = oSD.SellingType
                    .SPAF = oSD.SPAF
                    .SPAFDoc_UploadHeader = oSDUH
                    .SPAFFType = oSD.SPAFFType
                    .Status = oSD.Status
                    .Subsidi = oSD.Subsidi
                    .TglSetuju = oSD.TglSetuju
                    .UploadBy = oSD.UploadBy
                    .UploadDate = oSD.UploadDate
                    .UploadFile = oSD.UploadFile
                    .ErrorMessage = oSD.ErrorMessage
                    If Not IsNothing(.ErrorMessage) AndAlso .ErrorMessage.Trim <> "" Then
                        nError += 1
                    End If
                End With
                oSDUDFac.Insert(oSDUD)
            Next

            oSDUH.NumberOfData = _SPAFDocs.Count
            oSDUH.NumberOfError = nError
            oSDUH.NumberOfValid = _SPAFDocs.Count - nError
            oSDUH.Status = enumSPAFUpload.SPAFUpload.Finish
            oSDUHFac.Update(oSDUH)

            'Start  :Old Script
            'Dim sFileName As String
            'Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            'Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            'Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
            'Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            'Dim SPAFFileName As String = String.Empty
            'Dim SPAFFolder As String = String.Empty
            'Dim webMechine As String
            'Dim _file As TransferFile
            'Dim finfo As New FileInfo(_fileName)

            'For Each item As String In KTB.DNet.Lib.WebConfig.GetValue("WebServerFolder").Split(";")
            '    SPAFFolder = item & KTB.DNet.Lib.WebConfig.GetValue("SPAF_FOLDER")
            'Next

            'Try
            '    If imp.Start() Then
            '        'save original/source in repository
            '        finfo = New FileInfo(_fileName)
            '        SPAFFileName = SPAFFolder & "\" & finfo.Name
            '        finfo.CopyTo(SPAFFileName, True)

            '        'save result in repository
            '        If finfo.Name.Trim.ToUpper.StartsWith("SPAF_IN_") Then
            '            SPAFFileName = SPAFFolder & "\" & "SPAF_OUT_" & finfo.Name.Substring(8)
            '        End If
            '        finfo = New FileInfo(SPAFFileName)
            '        If finfo.Exists Then
            '            finfo.Delete()
            '        End If

            '        Dim fs As FileStream = New FileStream(SPAFFileName, FileMode.CreateNew)
            '        Dim sw As StreamWriter = New StreamWriter(fs)
            '        WriteSPAFDocs(sw, _SPAFDocs)
            '        sw.Close()
            '        fs.Close()
            '        imp.StopImpersonate()
            '        imp = Nothing
            '    End If
            'Catch ex As Exception
            '    Throw ex ' MessageBox.Show("Download data gagal")
            'End Try
            'Return 0
            'End    :Old Script
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Function ParseSPAFDoc(ByVal ValParser As String) As SPAFDoc
            Dim _SPAFDoc As SPAFDoc = New SPAFDoc
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            sBuilder = New StringBuilder
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        If sTemp.Trim.Length > 0 Then
                            _SPAFDoc.SPAFFType = sTemp.Trim.ToUpper
                        Else
                            _SPAFDoc.SPAFFType = sTemp.Trim
                        End If
                    Case Is = 1
                        If sTemp.Trim.Length > 0 Then
                            If _Dealer <> sTemp.Trim.ToLower Then
                                Dim oDealer As Dealer = New Dealer
                                oDealer.ID = 0
                                _SPAFDoc.Dealer = oDealer
                                _SPAFDoc.ErrorMessage = _SPAFDoc.ErrorMessage & "Kode Perusahaan Tidak Valid. <br> "
                            Else
                                _SPAFDoc.Dealer = _objDealer
                            End If
                            'Dim objDealerFacade As DealerFacade = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                            'Dim objDealer As Dealer = objDealerFacade.Retrieve(sTemp)
                            'If objDealer.ID > 0 Then
                            '    _SPAFDoc.Dealer = objDealer
                            'Else
                            '    _SPAFDoc.ErrorMessage = _SPAFDoc.ErrorMessage & " Kode Perusahaan tidak ditemukan."
                            'End If
                        Else
                            Dim oDealer As Dealer = New Dealer
                            oDealer.ID = 0
                            _SPAFDoc.Dealer = oDealer
                            _SPAFDoc.ErrorMessage = _SPAFDoc.ErrorMessage & "Kode Perusahaan Tidak Boleh Kosong. <br>"
                        End If
                    Case Is = 2
                        If sTemp.Length > 0 Then
                            _SPAFDoc.ReffLetter = sTemp.Trim
                        Else
                            _SPAFDoc.ErrorMessage = _SPAFDoc.ErrorMessage & "No. Surat Tidak Boleh Kosong. <br>"
                        End If
                    Case Is = 3
                        If sTemp.Length = 10 Then
                            Dim sVals() As String = sTemp.Split("/")
                            If sVals.Length = 3 Then
                                Try
                                    _SPAFDoc.DateLetter = DateSerial(CType(sVals(2), Integer), CType(sVals(1), Integer), CType(sVals(0), Integer))
                                Catch ex As Exception
                                    _SPAFDoc.ErrorMessage = _SPAFDoc.ErrorMessage & "Tanggal Tidak Valid. <br>"
                                End Try
                            Else
                                _SPAFDoc.ErrorMessage = _SPAFDoc.ErrorMessage & "Tanggal Tidak Valid. <br>"
                            End If
                        Else
                            _SPAFDoc.ErrorMessage = _SPAFDoc.ErrorMessage & "Tanggal Tidak Valid. <br>"
                        End If

                    Case Is = 4
                        If sTemp.Trim.Length > 0 Then
                            _SPAFDoc.CustomerName = sTemp
                        Else
                            _SPAFDoc.CustomerName = String.Empty
                            _SPAFDoc.ErrorMessage = _SPAFDoc.ErrorMessage & "Nama Pelanggan Masih Kosong. <br>"
                        End If

                    Case Is = 5
                        If sTemp.Trim.Length > 0 Then

                            Dim objCM As ChassisMaster = objCMFacade.Retrieve(sTemp)
                            objcm.MarkLoaded()
                            If objCM.ID > 0 Then
                                _SPAFDoc.ChassisMaster = objCM
                                'Check if on SPAFDOC ChassisMasterID already Exist
                                'SetMsgIfChasisNumExist(objCM.ID, _SPAFDoc)
                            Else
                                objCM = New ChassisMaster
                                objCM.ID = 0
                                objcm.ChassisNumber = sTemp.Trim
                                _SPAFDoc.ChassisMaster = objCM
                                _SPAFDoc.ErrorMessage = _SPAFDoc.ErrorMessage & "Nomor Rangka Tidak Terdaftar." & sTemp.Trim & ".<br>"
                            End If
                        Else
                            Dim objCM As ChassisMaster = New ChassisMaster
                            objCM.ID = 0
                            _SPAFDoc.ErrorMessage = _SPAFDoc.ErrorMessage & "Nomor Rangka Kosong. <br>"
                        End If

                        'Case Is = 6
                        '    If sTemp.Trim.Length > 0 Then
                        '        Dim objDealerFacade As DealerFacade = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        '        Dim objDealer As Dealer = objDealerFacade.Retrieve(sTemp)
                        '        If objDealer.ID > 0 Then
                        '            _SPAFDoc.OrderDealer = objDealer.DealerCode
                        '        Else
                        '            _SPAFDoc.ErrorMessage = _SPAFDoc.ErrorMessage & "Kode Penjual Tidak Terdaftar.  <br>"
                        '        End If
                        '    Else
                        '        _SPAFDoc.ErrorMessage = _SPAFDoc.ErrorMessage & "Kode Penjual Tidak Terdaftar. <br>"
                        '    End If
                    Case Is = 6
                        If sTemp.Trim.Length > 0 Then
                            _SPAFDoc.DealerLeasing = sTemp
                            'If _SPAFDoc.SPAFFType.ToUpper = "SPAF" Then
                            _SPAFDoc.LeasingDealerName = _SPAFDoc.DealerLeasing
                            'Else
                            '    Dim objDealer As New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                            '    Dim itsDeal As Dealer = objdealer.Retrieve(sTemp)
                            '    If itsDeal Is Nothing OrElse itsDeal.DealerCode.ToUpper <> sTemp.ToUpper Then
                            '        _SPAFDoc.ErrorMessage = String.Format("Kode dealer tidak dikenali '{0}'", sTemp)
                            '    Else
                            '        _SPAFDoc.LeasingDealerName = itsDeal.DealerName
                            '    End If
                            'End If
                        Else
                            _SPAFDoc.ErrorMessage = "Masukkan nama leasing dealer"
                        End If
                    Case Is = 7
                        If sTemp.Trim.Length > 0 Then
                            If sTemp.ToUpper = EnumSellingType.SellingType.Avalis Then
                                _SPAFDoc.SellingType = CType(EnumSellingType.SellingType.Avalis, Short)
                            ElseIf sTemp.ToUpper = EnumSellingType.SellingType.NonAvalis Then
                                _SPAFDoc.SellingType = CType(EnumSellingType.SellingType.NonAvalis, Short)
                            Else
                                _SPAFDoc.ErrorMessage = String.Format("Selling type tidak dikenali '{0}'", sTemp)
                            End If

                        End If

                        'Add New Column for Price(24/12/2009)
                    Case Is = 8
                        If sTemp.Trim.Length > 0 Then
                            Try
                                _SPAFDoc.RetailPrice = CType(sTemp, Decimal)
                            Catch ex As Exception
                                _SPAFDoc.ErrorMessage = String.Format("Format harga tidak dikenali '{0}'", sTemp)

                            End Try
                        Else
                            _SPAFDoc.RetailPrice = CType(-99999, Decimal)
                        End If
                End Select
                '_SPAFDoc.PPhPercent = pphf.RetrievePPh
                sStart = m.Index + 1
                nCount += 1
            Next

            _SPAFDocs.Add(_SPAFDoc)
            Return _SPAFDoc
        End Function

        Private Sub WriteSPAFDocs(ByVal SW As StreamWriter, ByVal arlData As ArrayList)
            Dim tab As String = ";" 'Char = Chr(9)
            Dim itemLine As StringBuilder = New StringBuilder
            'Start  :This List is referenced to SPAFParser.DoParse() :by:dna;Optimize SPAF Upload;20100525
            '_SPAFDoc.SPAFFType=String
            '_SPAFDoc.Dealer=Object
            '_SPAFDoc.ReffLetter=String
            '            _SPAFDoc.DateLetter = CDate(sTemp)
            '            _SPAFDoc.CustomerName = sTemp
            '_SPAFDoc.ChassisMaster = Object->ChassisNumber
            '_SPAFDoc.DealerLeasing=String
            '_SPAFDoc.SellingType = String ->CType	(EnumSellingType.SellingType.Avalis, Short)
            '_SPAFDoc.RetailPrice = String->CType(sTemp, Decimal)

            '_SPAFDoc.ErrorMessage=String
            'End    :This List is referenced to SPAFParser.DoParse() :by:dna;Optimize SPAF Upload;20100525

            If arlData.Count > 0 Then
                For Each oSD As SPAFDoc In arlData
                    itemLine.Remove(0, itemLine.Length)

                    itemLine.Append(oSD.SPAFFType & tab)
                    itemLine.Append(oSD.Dealer.DealerCode & tab)
                    itemLine.Append(oSD.ReffLetter & tab)
                    itemLine.Append(oSD.DateLetter.ToString("dd/MM/yyyy") & tab)
                    itemLine.Append(oSD.CustomerName & tab)
                    itemLine.Append(oSD.ChassisMaster.ChassisNumber & tab)
                    itemLine.Append(oSD.DealerLeasing & tab)
                    itemLine.Append(oSD.SellingType & tab)
                    itemLine.Append(oSD.RetailPrice & tab)
                    itemLine.Append(oSD.ErrorMessage & tab)

                    SW.WriteLine(itemLine.ToString())

                Next
            End If
        End Sub

        Private Sub AddDetail(ByRef arrL As ArrayList)
            Dim User As Object = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim nIndex As Integer
            Dim nIterate As Integer = 1
            Dim pph As Decimal = (New PPhFacade(User)).RetrievePPh
            Dim isSPAF As Boolean = True

            For Each item As SPAFDoc In arrL
                item.PPhPercent = pph
                item.CreatedBy = CType(User.Identity.Name, String)
                item.Status = EnumSPAFSubsidy.SPAFDocStatus.Baru
                item.DocType = IIf(isSPAF, EnumSPAFSubsidy.DocumentType.SPAF, EnumSPAFSubsidy.DocumentType.Subsidi)

                Try
                    If (item.ChassisMaster.ID > 0) Then

                        item.OrderDealer = item.ChassisMaster.Dealer.DealerCode
                        Dim arrlCM As New ArrayList
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ConditionMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ConditionMaster), "VechileType.ID", MatchType.Exact, item.ChassisMaster.VechileColor.VechileType.ID))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ConditionMaster), "ValidFrom", MatchType.LesserOrEqual, item.DateLetter))

                        Dim sortColl As SortCollection = New SortCollection
                        sortColl.Add(New Search.Sort(GetType(ConditionMaster), "ValidFrom", Sort.SortDirection.DESC))

                        arrlCM = New SPAF.ConditionMasterFacade(User).Retrieve(criterias, sortColl)

                        'Get Retail Price From Database

                        If arrlCM.Count = 0 Then
                            'item.RetailPrice = 0 'Asked by DonSet;by:dna;on:20100726;let the amount refer to uploaded file
                            item.SPAF = 0
                            item.Subsidi = 0
                            item.ErrorMessage = item.ErrorMessage & "Harga Tidak Valid. "

                        ElseIf item.RetailPrice <> CType(arrlCM(0), ConditionMaster).RetailPrice Then
                            Dim _retailPrice = CType(arrlCM(0), ConditionMaster).RetailPrice
                            If item.RetailPrice = CType(-99999, Decimal) Then
                                'item.RetailPrice = CType(arrlCM(0), ConditionMaster).RetailPrice
                                'item.SPAF = IIf(isSPAF, (CType(arrlCM(0), ConditionMaster).SPAF / 100) * CType(arrlCM(0), ConditionMaster).RetailPrice, 0)
                                item.SPAF = IIf(isSPAF, (CType(arrlCM(0), ConditionMaster).SPAF / 100) * item.RetailPrice, 0)
                                item.Subsidi = IIf(isSPAF, 0, CType(arrlCM(0), ConditionMaster).Subsidi)

                            Else
                                'item.RetailPrice = CType(arrlCM(0), ConditionMaster).RetailPrice
                                'item.SPAF = IIf(isSPAF, (CType(arrlCM(0), ConditionMaster).SPAF / 100) * CType(arrlCM(0), ConditionMaster).RetailPrice, 0)
                                item.SPAF = IIf(isSPAF, (CType(arrlCM(0), ConditionMaster).SPAF / 100) * item.RetailPrice, 0)
                                item.ErrorMessage = item.ErrorMessage & "Harga Tidak Valid. "
                            End If
                        Else
                            'item.RetailPrice = CType(arrlCM(0), ConditionMaster).RetailPrice
                            'item.SPAF = IIf(isSPAF, (CType(arrlCM(0), ConditionMaster).SPAF / 100) * CType(arrlCM(0), ConditionMaster).RetailPrice, 0)
                            item.SPAF = IIf(isSPAF, (CType(arrlCM(0), ConditionMaster).SPAF / 100) * item.RetailPrice, 0)
                            item.Subsidi = IIf(isSPAF, 0, CType(arrlCM(0), ConditionMaster).Subsidi)
                        End If

                        '---cek chassis dengan database sesuai tipe dokumen dan statusnya <> (ditolak,dihapus)
                        '-- kalo data ditemukan --> error message data sudah ada 
                        Dim _arrTmp As ArrayList = New ArrayList
                        Dim _status As String = String.Empty
                        _status = CInt(EnumSPAFSubsidy.SPAFDocStatus.Deleted).ToString & "," & CInt(EnumSPAFSubsidy.SPAFDocStatus.Ditolak).ToString

                        '---cek No kontrak di file teks
                        'Dim _refx As String = item.ReffLetter
                        'For j As Integer = nIterate To arrL.Count - 1
                        '    Dim _refy As String = CType(arrL(j), SPAFDoc).ReffLetter
                        '    If _refx = _refy Then
                        '        If CType(arrL(j), SPAFDoc).ErrorMessage = "" Then
                        '            CType(arrL(j), SPAFDoc).ErrorMessage = "No Kontrak Sudah Ada / Duplikat dg Record " & +CType(nIterate, String) & ". <br/>"
                        '        Else
                        '            CType(arrL(j), SPAFDoc).ErrorMessage = CType(arrL(j), SPAFDoc).ErrorMessage & "No Kontrak Sudah Ada / Duplikat dg Record " & +CType(nIterate, String) & ". <br/>"
                        '        End If
                        '    End If
                        'Next

                        '--cek no kontrak di database
                        Dim criteriaKontrak As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteriaKontrak.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "ReffLetter", MatchType.Exact, item.ReffLetter))
                        criteriaKontrak.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "Status", MatchType.NotInSet, _status))

                        If isSPAF Then
                            criteriaKontrak.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "DocType", CInt(EnumSPAFSubsidy.DocumentType.SPAF)))
                        Else
                            criteriaKontrak.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "DocType", CInt(EnumSPAFSubsidy.DocumentType.Subsidi)))
                        End If

                        'Modify by Ags on 25 Maret 2010 for DonSet's Request
                        '_arrTmp = New SPAF.SPAFFacade(User).Retrieve(criteriaKontrak)

                        'If _arrTmp.Count > 0 Then
                        '    item.ErrorMessage = item.ErrorMessage & "No Kontrak Sudah Ada Di Database. "
                        'End If

                        For nIndex = nIterate To arrL.Count - 1
                            Dim item2 As SPAFDoc
                            item2 = arrL(nIndex)
                            If Not IsNothing(item2.ChassisMaster) Then
                                Dim sChassisNumber2 = item2.ChassisMaster.ChassisNumber
                                Dim sChassisNumber1 = item.ChassisMaster.ChassisNumber

                                If (sChassisNumber1 = sChassisNumber2) Then
                                    If item2.ErrorMessage = "" Then
                                        item2.ErrorMessage = "No Rangka Sudah Ada / Duplikat dg Record " + CType(nIterate, String) & ". "
                                    Else
                                        item2.ErrorMessage = item2.ErrorMessage + "No Rangka Sudah Ada / Duplikat dg Record " + CType(nIterate, String) & ". "
                                    End If
                                End If
                            End If
                        Next

                        '---cek chassis dengan database sesuai tipe dokumen dan statusnya <> (ditolak,dihapus)
                        '-- kalo data ditemukan --> error message data sudah ada 
                        Dim criteriaforChassis As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteriaforChassis.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "ChassisMaster.ID", MatchType.Exact, item.ChassisMaster.ID))
                        criteriaforChassis.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "Status", MatchType.NotInSet, _status))

                        criteriaforChassis.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "DocType", CInt(EnumSPAFSubsidy.DocumentType.SPAF)))

                        If isSPAF Then
                            criteriaforChassis.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "DocType", CInt(EnumSPAFSubsidy.DocumentType.SPAF)))
                        Else
                            criteriaforChassis.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "DocType", CInt(EnumSPAFSubsidy.DocumentType.Subsidi)))
                        End If

                        _arrTmp = New SPAF.SPAFFacade(User).Retrieve(criteriaforChassis)

                        If _arrTmp.Count > 0 Then

                            item.ErrorMessage = StrIfChasisNumExist(_arrTmp) '"No Rangka Sudah Ada Di Database. <br/>" '---data sudah ada di database                   
                        End If
                    End If
                Catch ex As Exception
                    'do nothing to skip error
                End Try

                nIterate = nIterate + 1
            Next
        End Sub

        Private Function StrIfChasisNumExist(ByVal _arrTmp As ArrayList) As String
            Dim _strMessage As New StringBuilder
            'GetDealerCode
            Dim _spafDocTemp As New SPAFDoc
            _spafDocTemp = CType(_arrTmp(0), SPAFDoc)

            _strMessage.Append("Nomor rangka sudah disimpan oleh ")
            _strMessage.Append(_spafDocTemp.Dealer.DealerCode)
            _strMessage.Append("-")
            _strMessage.Append(_spafDocTemp.CreatedBy.Substring(6, _spafDocTemp.CreatedBy.Length - 6))
            _strMessage.Append(", tgl ")
            _strMessage.Append(_spafDocTemp.CreatedTime.Day)
            _strMessage.Append(" ")
            _strMessage.Append(_spafDocTemp.CreatedTime.Month)
            _strMessage.Append(" ")
            _strMessage.Append(_spafDocTemp.CreatedTime.Year)
            _strMessage.Append(" ")
            _strMessage.Append(_spafDocTemp.CreatedTime.Hour)
            _strMessage.Append(".")
            _strMessage.Append(_spafDocTemp.CreatedTime.Minute)
            _strMessage.Append(" WIB.")
            'Nomor rangka sudah disimpan oleh ABC-userX, tgl 22 Dec 2009 15.50 WIB.
            Return _strMessage.ToString()
        End Function

        Private Function SaveHeader() As Boolean
            Dim oSDUHFac As SPAFDoc_UploadHeaderFacade = New SPAFDoc_UploadHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim oSDUDFac As SPAFDoc_UploadDetailFacade = New SPAFDoc_UploadDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim cSDUH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPAFDoc_UploadHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim oSDUH As SPAFDoc_UploadHeader
            Dim oSDUD As SPAFDoc_UploadDetail
            Dim arlSDUH As New ArrayList
            Dim ID As Integer
            Dim finfo As FileInfo = New FileInfo(_fileName)
            Dim nError As Integer

            cSDUH.opAnd(New Criteria(GetType(SPAFDoc_UploadHeader), "Dealer.ID", MatchType.Exact, Me._objDealer.ID))
            cSDUH.opAnd(New Criteria(GetType(SPAFDoc_UploadHeader), "Filename", MatchType.Exact, finfo.Name))
            arlSDUH = oSDUHFac.Retrieve(cSDUH)
            If arlSDUH.Count > 0 Then
                oSDUH = CType(arlSDUH(0), SPAFDoc_UploadHeader)
                oSDUH.Status = enumSPAFUpload.SPAFUpload.Initialization
                oSDUHFac.Update(oSDUH)
                For Each oSDUDTemp As SPAFDoc_UploadDetail In oSDUH.SPAFDoc_UploadDetails
                    oSDUDFac.DeleteFromDB(oSDUDTemp)
                Next
            Else
                oSDUH = New SPAFDoc_UploadHeader
                oSDUH.Filename = finfo.Name
                oSDUH.Dealer = Me._objDealer
                oSDUH.NumberOfData = -1
                ID = oSDUHFac.Insert(oSDUH)
                If ID < 1 Then
                    Return False
                Else
                    oSDUH.ID = ID
                    oSDUH.Status = enumSPAFUpload.SPAFUpload.Initialization
                    oSDUHFac.Update(oSDUH)
                End If
            End If
            _objSDUH = oSDUH
            Return True
        End Function

        Private Function ConvertSPAFDocToSPAFDoc_UploadDetail(ByRef oSD As SPAFDoc) As SPAFDoc_UploadDetail
            Dim oSDUD As SPAFDoc_UploadDetail = New SPAFDoc_UploadDetail

            With oSDUD
                .AlasanPenolakan = oSD.AlasanPenolakan
                .ChassisMaster = oSD.ChassisMaster
                .CreatedBy = oSD.CreatedBy
                .CreatedTime = oSD.CreatedTime
                .CustomerName = oSD.CustomerName
                .DateLetter = oSD.DateLetter
                .Dealer = oSD.Dealer
                .DealerLeasing = oSD.DealerLeasing
                .DocType = oSD.DocType
                .LastUpdateBy = oSD.LastUpdateBy
                .LastUpdateTime = oSD.LastUpdateTime
                .OrderDealer = oSD.OrderDealer
                .PostingDate = oSD.PostingDate
                .PPhPercent = oSD.PPhPercent
                .ReffLetter = oSD.ReffLetter
                .RetailPrice = oSD.RetailPrice
                .RowStatus = oSD.RowStatus
                .SellingType = oSD.SellingType
                .SPAF = oSD.SPAF
                .SPAFDoc_UploadHeader = _objSDUH
                .SPAFFType = oSD.SPAFFType
                .Status = oSD.Status
                .Subsidi = oSD.Subsidi
                .TglSetuju = oSD.TglSetuju
                .UploadBy = oSD.UploadBy
                .UploadDate = oSD.UploadDate
                .UploadFile = oSD.UploadFile
                .ErrorMessage = oSD.ErrorMessage
                'If Not IsNothing(.ErrorMessage) AndAlso .ErrorMessage.Trim <> "" Then
                '    _NumUploadError += 1
                'End If
            End With

            Return oSDUD
        End Function

        Private Function InsertSDUD(ByRef oSDUD As SPAFDoc_UploadDetail) As Boolean
            _objSDUDFac.Insert(oSDUD)
            
            'If _objSDUDFac.Insert(oSDUD) < 0 Then
            '    Return False
            'Else
            '    Return True
            'End If
        End Function

#End Region

      
    End Class
End Namespace

