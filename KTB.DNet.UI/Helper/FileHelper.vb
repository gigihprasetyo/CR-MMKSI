#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2005

'Intimedia grants you the right to use and modify the code in this Persistence Framework
'(code under the Framework Namespace) but 
'(i) only for the solutions that are developed by Intimedia for you 
'(ii) or in solutions that are developed in join development between you and KTB.DNet.

'All rights not expressly granted, are reserved.
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Security.Principal
Imports System.Security.Permissions
Imports System.Collections.Generic
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade

#End Region

Namespace KTB.DNet.UI.Helper

    Public Class FileHelper
        Inherits System.Web.UI.Page

#Region "Private Variables"
        Private objStreamWriter As StreamWriter
        Private objStreamReader As StreamReader
        Private delimiter As String = Chr(9) 'Tab
        Private newline1 As String = Chr(10)
        Private newline2 As String = Chr(13)
        Private SemiColondelimiter As String = ";"
        Private PKFileName As String = "inq_fu" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Second & Now.Millisecond & ".txt"
        Private PKFileNameDownload As String = "Pesanan" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Second & Now.Millisecond & ".txt"
        Private POFileName As String = "sounit" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Second & Now.Millisecond & ".txt"
        Private TOPSPFileName As String = "TOPSPPaymentConf" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Second & Now.Millisecond & ".txt"
        Private DPFileName As String = "so_payment" & Now.TimeOfDay.TotalSeconds.ToString & ".txt"
        Private WSCFileName As String = "WSCEvidence_" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Second & Now.Millisecond & ".txt"
        Private WSCFileNameBB As String = "WSCEvidenceBB_" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Second & Now.Millisecond & ".txt"
        Private AnnualFileName As String = "HasilAnnlDisc_"
        Private POProposeFileName As String = "Order" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Second & Now.Millisecond & ".txt"
        Private UploadRencanaProduksiFileName As String = "RENCANA_PRODUKSI" & Now.TimeOfDay.TotalSeconds.ToString & ".txt"
        Private OutStandingMOFileName As String = "REMAINMO" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Second & Now.Millisecond & ".txt"
        Private UploadEquipmentFileName As String = "EqMaster [" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Second & Now.Millisecond & "]" & ".txt"
        Private PEFileName As String = "inq_pe" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Second & Now.Millisecond & ".txt"
        Private AnnualDiscountFileName As String = "AnnlDisc_"
        Private GyroFileName As String = "GyroTransfer" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Second & Now.Millisecond & ".txt"
        Private GyroPercepatanFileName As String = "AGyroTransfer" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Second & Now.Millisecond & ".txt"
        Private GyroCessieFileName As String = "GyroTransfer" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Second & Now.Millisecond & ".txt"
        Private FactoringFileName As String = String.Format("factceiling{0}.txt", DateTime.Now.ToString("yyyyMMddHHmmss")) '"factceiling" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Second & Now.Millisecond & ".txt"
        Private FactoringPaymentFileName As String = "factstat" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Second & Now.Millisecond & ".txt"
        Private PaymentConfName As String = "PaymentConf" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Second & Now.Millisecond & ".txt"

        Private DepBFileName As String = "JVDEPB" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Second & Now.Millisecond & ".txt"

        Private IRPaymentGyroFileName As String = "IRPaymentGyro" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Second & Now.Millisecond & ".txt"
        Private IRPaymentTransFileName As String = "IRPaymentTransfer" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Second & Now.Millisecond & ".txt"
        Private IRPaymentConfFileName As String = "IRPaymentConf" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Second & Now.Millisecond & ".txt"
        Private IRCancelFileName As String = "IRCancel" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Second & Now.Millisecond & ".txt"

        Dim impersonateToken As System.IntPtr
        Dim ImpersonationCtx As WindowsImpersonationContext
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()

        End Sub

#End Region

#Region "Utility Method"

        Private Function CreateFile(ByVal fileName As String) As FileInfo
            Dim finfo As New FileInfo(fileName)
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            Try
                If imp.Start() Then
                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If
                    If finfo.Exists Then
                        finfo.Delete()
                    End If
                    objStreamWriter = New StreamWriter(fileName)
                    imp.StopImpersonate()
                    imp = Nothing
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return finfo
        End Function

        Private Sub CloseStreamWriter()
            objStreamWriter.Flush()
            objStreamWriter.Close()
        End Sub

        Private Sub StartImpersonet()
            Dim id As WindowsIdentity = System.Threading.Thread.CurrentPrincipal.Identity
            id.GetCurrent()

            Dim ImpersonationCtx As WindowsImpersonationContext = id.Impersonate()

        End Sub

        Private Sub StopImpersonet()
            If Not ImpersonationCtx Is Nothing Then
                ImpersonationCtx.Undo()
            End If
        End Sub

        Private Function ConvertDate(ByVal dt As Date, Optional ByVal digit As Integer = 0) As String
            Dim strDate As New StringBuilder
            If dt.Day.ToString.Length = 1 Then
                strDate.Append("0")
            End If
            strDate.Append(dt.Day)
            If dt.Month.ToString.Length = 1 Then
                strDate.Append("0")
            End If
            strDate.Append(dt.Month)
            If digit = 2 Then
                strDate.Append(dt.Year.ToString.Substring(2, 2))
            Else
                strDate.Append(dt.Year)
            End If
            Return strDate.ToString
        End Function

        Private Function ConstructPeriode(ByVal month As Short, ByVal year As Short) As String
            Dim sb As New StringBuilder
            sb.Append("01")
            If month.ToString.Length = 1 Then
                sb.Append("0")
            End If
            sb.Append(month)
            sb.Append(year)
            Return sb.ToString
        End Function

#End Region

#Region "Public Method"
        'Public Sub Periode()
        '    Dim PeriodeFrom As DateTime = Request.QueryString("From")
        '    Dim PeriodeTo As DateTime = Request.QueryString("To")
        'End Sub

        Public Shared Function IsExecutableFile(ByVal strFileName As String) As Boolean
            If Right(strFileName.Trim, 4).ToUpper = ".EXE" Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Sub WriteSalesOrderToFile(ByVal fileName As String, ByVal al As ArrayList)
            'CreateFile(fileName)
            'For Each item As DailyPO In al
            '    Dim oType As TOPEnum = item.TOP
            '    objStreamWriter.WriteLine("H" & delimiter & item.Contract_Number & delimiter & item.DealerPONumber & delimiter & item.DailyPONumber & delimiter & oType.ToString)
            '    For Each item_detail As DailyPODetails In item.DailyPODetails
            '        If item_detail.AllocationQuantity > 0 Then
            '            objStreamWriter.WriteLine("D" & delimiter & item_detail.ContractLineItem & delimiter & item_detail.AllocationQuantity)
            '        End If
            '    Next
            'Next
            'CloseStreamWriter()
        End Sub

        Public Function TransferWSCtoSAP(ByVal ListWSC As ArrayList) As FileInfo
            Dim objStringBuilder As StringBuilder
            Dim sFileName As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFOLDER") & WSCFileName
            Dim objFileInfo As FileInfo = CreateFile(sFileName)
            Dim delimiter As String = ""","""
            For Each wsc As WSCHeader In ListWSC
                For Each wscDetail As WSCDetail In wsc.WSCDetails

                    objStringBuilder = New StringBuilder
                    objStringBuilder.Append("""")
                    objStringBuilder.Append(wsc.ClaimType)
                    objStringBuilder.Append(delimiter)
                    objStringBuilder.Append(wsc.Dealer.DealerCode)
                    objStringBuilder.Append(delimiter)
                    objStringBuilder.Append(wsc.ClaimNumber)
                    objStringBuilder.Append(delimiter)
                    objStringBuilder.Append(wsc.RefClaimNumber)
                    objStringBuilder.Append(delimiter)
                    objStringBuilder.Append(wsc.ChassisMaster.ChassisNumber)
                    objStringBuilder.Append(delimiter)
                    objStringBuilder.Append(wsc.ServiceDate.Day.ToString.PadLeft(2, "0") & _
                                            wsc.ServiceDate.Month.ToString.PadLeft(2, "0") & _
                                            wsc.ServiceDate.Year.ToString.Substring(2, 2))
                    objStringBuilder.Append(delimiter)
                    objStringBuilder.Append(wsc.Miliage.ToString)
                    objStringBuilder.Append(delimiter)
                    objStringBuilder.Append(wsc.PQR)
                    objStringBuilder.Append(delimiter)
                    objStringBuilder.Append(wsc.PQRStatus)
                    objStringBuilder.Append(delimiter)
                    objStringBuilder.Append(wsc.CodeA)
                    objStringBuilder.Append(delimiter)
                    objStringBuilder.Append(wsc.CodeB)
                    objStringBuilder.Append(delimiter)
                    objStringBuilder.Append(wsc.CodeC)
                    objStringBuilder.Append(delimiter)
                    objStringBuilder.Append(wsc.Description)
                    objStringBuilder.Append(delimiter)
                    objStringBuilder.Append(wsc.EvidencePhoto)
                    objStringBuilder.Append(delimiter)
                    objStringBuilder.Append(wsc.EvidenceInvoice)
                    objStringBuilder.Append(delimiter)
                    objStringBuilder.Append(wsc.EvidenceDmgPart)
                    objStringBuilder.Append(delimiter)

                    objStringBuilder.Append(wscDetail.WSCType)
                    objStringBuilder.Append(delimiter)
                    If wscDetail.WSCType = "L" Then
                        objStringBuilder.Append(wscDetail.LaborMaster.LaborCode)
                        objStringBuilder.Append(delimiter)
                        objStringBuilder.Append(wscDetail.LaborMaster.WorkCode)
                        objStringBuilder.Append(delimiter)
                        objStringBuilder.Append(wscDetail.Quantity.ToString.Replace( _
                            System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator, ","))

                    ElseIf wscDetail.WSCType = "P" Then
                        objStringBuilder.Append(wscDetail.SparePartMaster.PartNumber)
                        objStringBuilder.Append(delimiter)
                        objStringBuilder.Append(wscDetail.Quantity.ToString.Replace( _
                            System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator, ","))
                        objStringBuilder.Append(delimiter)
                        objStringBuilder.Append(String.Format("{0:#}", wscDetail.PartPrice))

                    End If
                    objStringBuilder.Append("""")
                    objStreamWriter.WriteLine(objStringBuilder.ToString)
                Next
            Next
            CloseStreamWriter()
            'CopyFileToSAPServer(objFileInfo.FullName)
            Return objFileInfo
        End Function

        Public Function TransferPKtoSAP(ByVal pkColl As ArrayList) As FileInfo
            Dim sb As StringBuilder
            Dim fileName As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFOLDER") & PKFileName
            Dim _fileInfo As FileInfo = CreateFile(fileName)
            Dim oSPLFac As New SPLFacade(User)

            For Each pk As PKHeader In pkColl
                Dim isDetailAllZZZZ As Boolean = True
                For Each item As PKDetail In pk.PKDetails
                    If item.VechileColor.ColorCode <> "ZZZZ" Then
                        isDetailAllZZZZ = False
                        Exit For
                    End If
                Next
                If Not isDetailAllZZZZ Then
                    Dim parentPK As PKHeader
                    If pk.HeadPKNumber > 0 Then
                        parentPK = New PKHeaderFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(pk.HeadPKNumber)
                    End If
                    If pk.OrderType = CType(LookUp.EnumJenisPesanan.Bulanan, Short) Then
                        Dim Tanggal As Date = Now
                        pk.PricingPeriodeDay = 1
                        pk.PricingPeriodeMonth = pk.RequestPeriodeMonth
                        pk.PricingPeriodeYear = pk.RequestPeriodeYear
                    Else
                        Dim Tanggal As Date = Now
                        pk.PricingPeriodeDay = Tanggal.Day '  pk.RequestPeriodeDay
                        pk.PricingPeriodeMonth = Tanggal.Month '  pk.RequestPeriodeMonth
                        pk.PricingPeriodeYear = Tanggal.Year '  pk.RequestPeriodeYear
                    End If
                    sb = New StringBuilder
                    sb.Append("H")
                    sb.Append(delimiter)
                    sb.Append(pk.Category.CategoryCode)
                    sb.Append(delimiter)
                    If pk.OrderType = LookUp.EnumJenisPesanan.Bulanan Then
                        sb.Append("ZA21")
                    Else
                        sb.Append("ZA22")
                    End If
                    sb.Append(delimiter)
                    sb.Append(pk.Dealer.DealerCode)
                    sb.Append(delimiter)
                    sb.Append(ConvertDate(pk.PKDate))
                    sb.Append(delimiter)
                    sb.Append(pk.DealerPKNumber)
                    sb.Append(delimiter)
                    sb.Append(DateSerial(pk.RequestPeriodeYear, pk.RequestPeriodeMonth, pk.RequestPeriodeDay).ToString("MMyyyy"))
                    sb.Append(delimiter)
                    sb.Append(DateSerial(pk.PricingPeriodeYear, pk.PricingPeriodeMonth, pk.PricingPeriodeDay).ToString("ddMMyyyy"))
                    sb.Append(delimiter)
                    sb.Append(pk.PKNumber)
                    sb.Append(delimiter)
                    If pk.HeadPKNumber < 1 Then
                        sb.Append("")
                    Else
                        sb.Append(parentPK.PKNumber)
                    End If
                    sb.Append(delimiter)
                    sb.Append(pk.ProductionYear)
                    sb.Append(delimiter)
                    Dim objSPL As SPL = New SPLFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(pk.SPLNumber)
                    If Not objSPL Is Nothing Then
                        sb.Append(pk.FreeIntIndicator)
                    Else
                        sb.Append("1")
                    End If
                    sb.Append(delimiter)
                    sb.Append(pk.SPLNumber)
                    sb.Append(delimiter)
                    sb.Append(pk.ProjectName)
                    ' Modified by Ikhsan, 31 Juli 2008
                    ' Requested by Peggy as a Part Of CR
                    ' Adding Description and KTB Response of PKHeader in the text file.
                    '---------------------------------------------------------------
                    ' Commented by Ikhsan To make sure that the changes has been approved by Users
                    sb.Append(delimiter)
                    sb.Append(pk.Description.Replace(newline1, " ").Replace(newline2, " ").ToString)
                    sb.Append(delimiter)
                    sb.Append("")
                    '---------------------------------------------------------------
                    sb.Append(delimiter)
                    If pk.SPLNumber.Trim <> String.Empty Then
                        Dim oSPL As SPL = oSPLFac.Retrieve(pk.SPLNumber)
                        If Not IsNothing(oSPL) AndAlso oSPL.ID > 0 AndAlso oSPL.NumOfInstallment > 1 Then
                            sb.Append(oSPL.NumOfInstallment)
                        End If
                    Else
                        sb.Append("1")
                    End If
                    sb.Append(delimiter)
                    If Not IsNothing(pk.DealerBranch) Then
                        sb.Append(pk.DealerBranch.DealerBranchCode)
                    Else
                        sb.Append("")
                    End If

                    objStreamWriter.WriteLine(sb.ToString.Trim)
                    Dim oJaminan As Jaminan = Nothing
                    Dim oJFac As JaminanFacade = New JaminanFacade(System.Threading.Thread.CurrentPrincipal)
                    If pk.JaminanID > 0 Then oJaminan = oJFac.Retrieve(pk.JaminanID)
                    For Each item As PKDetail In pk.PKDetails
                        If item.VechileColor.ID <> New VechileColorFacade(System.Threading.Thread.CurrentPrincipal).Retrieve("ZZZZ").ID Then
                            sb = New StringBuilder
                            sb.Append("D")
                            sb.Append(delimiter)
                            sb.Append(item.VechileColor.HeaderBOM)
                            sb.Append(delimiter)
                            sb.Append(item.TargetQty)
                            sb.Append(delimiter)
                            sb.Append(item.ResponseQty)
                            sb.Append(delimiter)
                            sb.Append(Convert.ToInt64(item.ResponseDiscount))
                            sb.Append(delimiter)
                            sb.Append(Convert.ToInt64(item.ResponseSalesSurcharge))
                            sb.Append(delimiter)
                            Dim JaminanAmount As Double = 0
                            If Not IsNothing(oJaminan) AndAlso oJaminan.ID > 0 AndAlso oJaminan.Status = 0 Then
                                For Each oJD As JaminanDetail In oJaminan.JaminanDetailIn(pk.RequestPeriodeMonth, pk.RequestPeriodeYear)
                                    If oJD.VehicleTypeCode = item.VehicleTypeCode And IIf(oJD.Purpose = LookUp.enumPurpose.Semua, True, oJD.Purpose = item.PKHeader.Purpose) Then
                                        JaminanAmount = oJD.Amount
                                        Exit For
                                    End If
                                Next
                            End If
                            sb.Append(JaminanAmount)
                            sb.Append(delimiter)
                            sb.Append(item.FreeDays)
                            objStreamWriter.WriteLine(sb.ToString.Trim())
                        End If
                    Next
                End If
            Next
            CloseStreamWriter()
            CopyFileToSAPServer(_fileInfo.FullName, EnumSAPFileType.SAPFileType.FinishUnitPK)
            Return _fileInfo
        End Function

        Public Sub CopyFileToSAPServer(ByVal _fileName As String, ByVal SAPType As Integer, Optional ByVal PreFolder As String = "")
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _sapServer As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServer") '172.17.104.204
            Dim _sapServerFolder As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") '\\172.17.104.204\ZDnet\SAP
            Select Case (SAPType)
                Case Is = EnumSAPFileType.SAPFileType.FinishUnitFR
                    _sapServerFolder = _sapServerFolder & "\FinishUnit\FR" & PreFolder
                Case Is = EnumSAPFileType.SAPFileType.FinishUnitPK
                    _sapServerFolder = _sapServerFolder & "\FinishUnit\PK" & PreFolder
                Case Is = EnumSAPFileType.SAPFileType.FinishUnitPO
                    _sapServerFolder = _sapServerFolder & "\FinishUnit\PO" & PreFolder
                Case Is = EnumSAPFileType.SAPFileType.ServiceEquipment
                    _sapServerFolder = _sapServerFolder & "\Service\Equi" & PreFolder
                Case Is = EnumSAPFileType.SAPFileType.ServiceFS
                    _sapServerFolder = _sapServerFolder & "\Service\FS" & PreFolder
                Case Is = EnumSAPFileType.SAPFileType.ServicePDI
                    _sapServerFolder = _sapServerFolder & "\Service\PDI" & PreFolder
                Case Is = EnumSAPFileType.SAPFileType.FinishUnit
                    _sapServerFolder = _sapServerFolder & "\FinishUnit\" & PreFolder
                Case Is = EnumSAPFileType.SAPFileType.FinishUnitFactoring
                    '_sapServerFolder = _sapServerFolder & "\FinishUnit" & PreFolder
                    _sapServerFolder = _sapServerFolder & "\FinishUnit\Factoring" & PreFolder 'will be activated on 2013.05.13
                Case Is = EnumSAPFileType.SAPFileType.ServiceDepositBEquipment
                    _sapServerFolder = _sapServerFolder & "\Service\"
                Case Is = EnumSAPFileType.SAPFileType.ServiceDepositBPengajuan
                    _sapServerFolder = _sapServerFolder & "\Service\"
            End Select

            Dim _file As KTB.DNet.Utility.TransferFile
            Try
                _file = New KTB.DNet.Utility.TransferFile(_user, _password, _sapServer)
                _file.Transfer(_fileName, _sapServerFolder)


                If Not IsNothing(SAPType) AndAlso SAPType = EnumSAPFileType.SAPFileType.FinishUnitFactoring Then

                End If
                If PreFolder.Trim <> "" Then
                    Try
                        _file.CreateDirectory(_sapServerFolder & "History\")
                        '_file.Transfer(_fileName & ".xxx", _sapServerFolder & "History\") 'xxx biar error
                    Catch ex As Exception

                    End Try


                    '_file.CreateDirectory(_sapServerFolder & "History")

                    'Dim oFI As New FileInfo(_fileName)
                    '_file.deleteFile(_sapServerFolder & "History\" & oFI.Name)
                    'If oFI.Directory.Exists = False Then
                    '    oFI.Directory.Create()
                    'End If
                End If
            Catch ex As Exception
                Throw ex
            End Try

        End Sub

        Public Sub CopyFileToSAPServer(ByVal _fileName As String, ByVal folderName As String)
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _sapServer As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServer") '172.17.104.204

            Dim _file As KTB.DNet.Utility.TransferFile
            Try
                _file = New KTB.DNet.Utility.TransferFile(_user, _password, _sapServer)
                _file.Transfer(_fileName, folderName)
            Catch Ex As Exception
                Throw Ex
            End Try
        End Sub


        Public Function TransferPOtoSAP(ByVal poColl As ArrayList) As FileInfo
            Dim sb As StringBuilder
            Dim fileName As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & POFileName
            Dim _fileInfo As FileInfo
            If poColl.Count > 0 Then
                _fileInfo = CreateFile(fileName)
                For Each po As POHeader In poColl
                    sb = New StringBuilder
                    sb.Append("H")
                    sb.Append(delimiter)
                    sb.Append(po.ContractHeader.ContractNumber)
                    sb.Append(delimiter)
                    sb.Append(po.DealerPONumber)
                    sb.Append(delimiter)
                    sb.Append(ConvertDate(po.CreatedTime, 2))
                    sb.Append(delimiter)
                    sb.Append(po.PONumber)
                    sb.Append(delimiter)
                    'sb.Append(po.TermOfPayment.TermOfPaymentCode)
                    If po.TermOfPayment.TermOfPaymentCode.ToUpper = "RTGS" Then
                        sb.Append("Z000")
                    Else
                        sb.Append(po.TermOfPayment.TermOfPaymentCode)
                    End If
                    sb.Append(delimiter)
                    'Add new column , inform whether RTGS or not
                    If po.TermOfPayment.TermOfPaymentCode.ToUpper = "RTGS" Then
                        sb.Append("T")
                    Else
                        sb.Append("")
                    End If
                    sb.Append(delimiter)
                    'end add new column
                    sb.Append(Format(po.ReqAllocationDateTime, "ddMMyy"))
                    sb.Append(delimiter)
                    If po.POType = enumOrderType.OrderType.Tambahan Then
                        sb.Append("ADD")
                        sb.Append(delimiter)
                    Else
                        sb.Append("")
                        sb.Append(delimiter)
                    End If
                    'Start  :RemainModule-DailyPO:FreePPh
                    sb.Append(IIf(po.FreePPh22Indicator.ToString = "0", "x", ""))
                    'sb.Append(delimiter)
                    'End    :RemainModule-DailyPO:FreePPh
                    'Start  : Not Implemented Yet
                    sb.Append(delimiter)
                    sb.Append(IIf(po.IsFactoring = 1, "F", ""))
                    'End    : Not Implemented Yet

                    If IsNothing(po.PODestination) Then
                        sb.Append(delimiter)
                        sb.Append(po.Dealer.DealerCode)
                    Else
                        sb.Append(delimiter)
                        sb.Append(po.PODestination.Code)
                    End If

                    objStreamWriter.WriteLine(sb.ToString)
                    'Dim oJaminan As Jaminan = Nothing
                    'Dim oJFac As JaminanFacade = New JaminanFacade(System.Threading.Thread.CurrentPrincipal)

                    'If po.ContractHeader.PKHeader.JaminanID > 0 Then oJaminan = oJFac.Retrieve(po.ContractHeader.PKHeader.JaminanID)
                    For Each item As PODetail In po.PODetails
                        If item.AllocQty > 0 Then
                            sb = New StringBuilder
                            sb.Append("D")
                            sb.Append(delimiter)
                            sb.Append(item.ContractDetail.LineItem)
                            sb.Append(delimiter)
                            sb.Append(item.AllocQty)
                            sb.Append(delimiter)
                            Dim JaminanAmount As Double = 0
                            'If Not IsNothing(oJaminan) AndAlso oJaminan.ID > 0 AndAlso oJaminan.Status = 0 Then
                            '    For Each oJD As JaminanDetail In oJaminan.JaminanDetailIn(po.ContractHeader.PKHeader.RequestPeriodeMonth, po.ContractHeader.PKHeader.RequestPeriodeYear)
                            '        If oJD.VehicleTypeCode = item.ContractDetail.VechileColor.VechileType.VechileTypeCode And IIf(oJD.Purpose = LookUp.enumPurpose.Semua, True, oJD.Purpose = item.POHeader.ContractHeader.PKHeader.Purpose) Then
                            '            JaminanAmount = oJD.Amount
                            '            Exit For
                            '        End If
                            '    Next
                            'End If
                            If item.POHeader.TermOfPayment.PaymentType = KTB.DNet.Domain.enumPaymentType.PaymentType.TOP Then
                                JaminanAmount = item.ContractDetail.GuaranteeAmount
                            End If
                            sb.Append("CBU" & JaminanAmount)
                            sb.Append(delimiter)
                            sb.Append("DP-0")
                            sb.Append(delimiter)
                            sb.Append("OP-0")

                            '' CR Sirkular Rewards
                            '' by : ali Akbar
                            '' 2014-09-24
                            sb.Append(delimiter)

                            sb.Append(delimiter)

                            If po.IsFactoring Then
                                Dim AmountRewr As Double = item.AmountReward + item.AmountRewardDepA
                                sb.Append(Math.Round(AmountRewr, 0))
                            Else
                                sb.Append("0")
                            End If
                            '' END CR


                            objStreamWriter.WriteLine(sb.ToString)

                        End If
                    Next
                Next
                CloseStreamWriter()
                CopyFileToSAPServer(_fileInfo.FullName, EnumSAPFileType.SAPFileType.FinishUnitPO)
                Return _fileInfo
            Else
                Return New FileInfo(fileName)
            End If

        End Function

        Public Function TransferTOPSPTransferPaymenttoSAP(ByVal arrTOPSP As List(Of TOPSPTransferPayment)) As FileInfo
            Dim FOLDER_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("DNetServerFolderTOP")
            Dim sb As StringBuilder
            Dim _fileInfo As FileInfo
            Dim arr As New ArrayList
            Dim objTOPSPdtlFac As New TOPSPTransferPaymentDetailFacade(User)
            Dim cri As CriteriaComposite
            Dim rowdtl As TOPSPTransferPaymentDetail

            Try
                FOLDER_NAME = FOLDER_NAME & "\" & TOPSPFileName
                _fileInfo = CreateFile(FOLDER_NAME)
                sb = New StringBuilder
                For Each row As TOPSPTransferPayment In arrTOPSP

                    cri = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPaymentDetail), "RowStatus", CType(DBRowStatus.Active, Short)))
                    cri.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment.ID", row.ID))
                    arr = objTOPSPdtlFac.Retrieve(cri)

                    For x As Integer = 0 To arr.Count - 1
                        rowdtl = New TOPSPTransferPaymentDetail
                        rowdtl = arr.Item(x)

                        sb.Append(row.RegNumber)
                        sb.Append(SemiColondelimiter)

                        sb.Append(rowdtl.SparePartBilling.BillingNumber)
                        sb.Append(SemiColondelimiter)

                        sb.Append(row.TransferPlanDate.ToString("yyyyMMdd"))
                        sb.Append(SemiColondelimiter)

                        If row.Status = EnumStatusTOPSPTransferPayment.TOPSPStatus.Batal Then
                            sb.Append("0")
                            sb.Append(SemiColondelimiter)
                        Else
                            sb.Append(CType((rowdtl.Amount), Long).ToString())
                            sb.Append(SemiColondelimiter)
                        End If

                        sb.Append(row.PaymentPurpose.PaymentPurposeCode)
                        If x <> arr.Count Then
                            sb.Append(Environment.NewLine)
                        End If
                    Next
                Next

                objStreamWriter.Write(sb.ToString)
                CloseStreamWriter()
                'CopyFileToSAPServer(_fileInfo.FullName, EnumSAPFileType.SAPFileType.FinishUnitPO)
                Return _fileInfo
                'Else
                '    Return New FileInfo(fileName)
                'End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try


        End Function

        Public Function TransferPOtoSAP(ByVal poColl As ArrayList, ByRef Message As String) As FileInfo
            Dim sb As StringBuilder
            Dim fileName As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & POFileName
            Dim _fileInfo As FileInfo
            Message = String.Empty
            If poColl.Count > 0 Then
                _fileInfo = CreateFile(fileName)
                For Each po As POHeader In poColl
                    sb = New StringBuilder
                    sb.Append("H")
                    sb.Append(delimiter)
                    sb.Append(po.ContractHeader.ContractNumber)
                    sb.Append(delimiter)
                    sb.Append(po.DealerPONumber)
                    sb.Append(delimiter)
                    sb.Append(ConvertDate(po.CreatedTime, 2))
                    sb.Append(delimiter)
                    sb.Append(po.PONumber)
                    sb.Append(delimiter)
                    'sb.Append(po.TermOfPayment.TermOfPaymentCode)
                    If po.TermOfPayment.TermOfPaymentCode.ToUpper = "RTGS" Then
                        sb.Append("Z000")
                    Else
                        sb.Append(po.TermOfPayment.TermOfPaymentCode)
                    End If
                    sb.Append(delimiter)
                    'Add new column , inform whether RTGS or not
                    If po.TermOfPayment.TermOfPaymentCode.ToUpper = "RTGS" Then
                        sb.Append("T")
                    Else
                        sb.Append("")
                    End If
                    sb.Append(delimiter)
                    'end add new column
                    sb.Append(Format(po.ReqAllocationDateTime, "ddMMyy"))
                    sb.Append(delimiter)
                    If po.POType = enumOrderType.OrderType.Tambahan Then
                        sb.Append("ADD")
                        sb.Append(delimiter)
                    Else
                        sb.Append("")
                        sb.Append(delimiter)
                    End If
                    'Start  :RemainModule-DailyPO:FreePPh
                    sb.Append(IIf(po.FreePPh22Indicator.ToString = "0", "x", ""))
                    'sb.Append(delimiter)
                    'End    :RemainModule-DailyPO:FreePPh
                    'Start  : Not Implemented Yet
                    sb.Append(delimiter)
                    sb.Append(IIf(po.IsFactoring = 1, "F", ""))
                    'End    : Not Implemented Yet
                    objStreamWriter.WriteLine(sb.ToString)
                    'Dim oJaminan As Jaminan = Nothing
                    'Dim oJFac As JaminanFacade = New JaminanFacade(System.Threading.Thread.CurrentPrincipal)

                    'If po.ContractHeader.PKHeader.JaminanID > 0 Then oJaminan = oJFac.Retrieve(po.ContractHeader.PKHeader.JaminanID)
                    For Each item As PODetail In po.PODetails
                        If item.AllocQty > 0 Then
                            sb = New StringBuilder
                            sb.Append("D")
                            sb.Append(delimiter)
                            sb.Append(item.ContractDetail.LineItem)
                            sb.Append(delimiter)
                            sb.Append(item.AllocQty)
                            sb.Append(delimiter)
                            Dim JaminanAmount As Double = 0
                            'If Not IsNothing(oJaminan) AndAlso oJaminan.ID > 0 AndAlso oJaminan.Status = 0 Then
                            '    For Each oJD As JaminanDetail In oJaminan.JaminanDetailIn(po.ContractHeader.PKHeader.RequestPeriodeMonth, po.ContractHeader.PKHeader.RequestPeriodeYear)
                            '        If oJD.VehicleTypeCode = item.ContractDetail.VechileColor.VechileType.VechileTypeCode And IIf(oJD.Purpose = LookUp.enumPurpose.Semua, True, oJD.Purpose = item.POHeader.ContractHeader.PKHeader.Purpose) Then
                            '            JaminanAmount = oJD.Amount
                            '            Exit For
                            '        End If
                            '    Next
                            'End If
                            If item.POHeader.TermOfPayment.PaymentType = KTB.DNet.Domain.enumPaymentType.PaymentType.TOP Then
                                JaminanAmount = item.ContractDetail.GuaranteeAmount
                            End If
                            sb.Append("CBU" & JaminanAmount)
                            sb.Append(delimiter)
                            sb.Append("DP-0")
                            sb.Append(delimiter)
                            sb.Append("OP-0")

                            ''validation request from mr angga
                            'Interest
                            If (po.TermOfPayment.TermOfPaymentValue <> 0 AndAlso item.Interest > 0) Then
                                Message = Message + "PO : " + po.PONumber + " ,MaterialNumber : " + item.ContractDetail.VechileColor.MaterialNumber + " => MissMatch Interest" + vbCrLf

                            End If

                            '' CR Sirkular Rewards
                            '' by : ali Akbar
                            '' 2014-09-24

                            sb.Append(delimiter)
                            If po.IsFactoring Then

                                If (po.TermOfPayment.TermOfPaymentValue <> 0 AndAlso item.DiscountReward = 0 AndAlso CType(New PriceFacade(User).RetrieveByCriteria(item.ContractDetail), Price).DiscountReward > 0) Then

                                    Message = Message + "PO : " + po.PONumber + " ,MaterialNumber : " + item.ContractDetail.VechileColor.MaterialNumber + " => MissMatch DiscountReward" + vbCrLf
                                End If

                                sb.Append(item.AmountReward)
                            Else
                                sb.Append("0")
                            End If
                            '' END CR


                            objStreamWriter.WriteLine(sb.ToString)

                        End If
                    Next
                Next
                If (Message.Equals(String.Empty)) Then
                    CloseStreamWriter()
                    CopyFileToSAPServer(_fileInfo.FullName, EnumSAPFileType.SAPFileType.FinishUnitPO)
                Else
                    Return New FileInfo(fileName)
                End If

                Return _fileInfo
            Else
                Return New FileInfo(fileName)
            End If

        End Function

        Public Function DownloadPKRequest(ByVal pkColl As ArrayList) As FileInfo
            Dim sb As StringBuilder
            Dim fileName As String = KTB.DNet.Lib.WebConfig.GetValue("PKFOLDER") & PKFileNameDownload
            Dim _fileInfo As FileInfo = CreateFile(fileName)
            For Each pk As PKHeader In pkColl
                For Each item As PKDetail In pk.PKDetails
                    sb = New StringBuilder
                    If Not pk.Category Is Nothing Then
                        sb.Append(pk.Category.CategoryCode)
                    Else
                        sb.Append("")
                    End If
                    sb.Append(SemiColondelimiter)
                    sb.Append(pk.PKNumber)
                    sb.Append(SemiColondelimiter)
                    If Not pk.Dealer Is Nothing Then
                        sb.Append(pk.Dealer.DealerCode)
                    Else
                        sb.Append("")
                    End If
                    sb.Append(SemiColondelimiter)
                    Dim jenisPesanan As String = CType(pk.OrderType, enumOrderType.OrderType).ToString
                    sb.Append(jenisPesanan)
                    sb.Append(SemiColondelimiter)
                    sb.Append(pk.ProductionYear)
                    sb.Append(SemiColondelimiter)
                    sb.Append(pk.ProjectName)
                    sb.Append(SemiColondelimiter)
                    sb.Append(item.VechileColor.MaterialNumber)
                    sb.Append(SemiColondelimiter)
                    sb.Append(item.TargetQty)
                    objStreamWriter.WriteLine(sb.ToString)
                Next
            Next
            CloseStreamWriter()

            Return _fileInfo
        End Function

        Public Function TransferPOProposetoSAP(ByVal poProposeColl As ArrayList, ByVal servermap As FileInfo) As FileInfo
            Dim sb As StringBuilder
            Dim fileName As String = servermap.Directory.FullName & "\" & KTB.DNet.Lib.WebConfig.GetValue("PPQtyDestFileDirectory") & POProposeFileName
            'Dim i As Integer
            Dim _fileInfo As FileInfo = CreateFile(fileName)
            For Each pp As PPQty In poProposeColl
                'i = 0
                sb = New StringBuilder
                sb.Append(pp.PeriodeDate & "/" & pp.PeriodeMonth & "/" & pp.PeriodeYear)
                sb.Append(delimiter)
                sb.Append(pp.MaterialNumber)
                sb.Append(delimiter)
                sb.Append(pp.ProductionYear)
                sb.Append(delimiter)
                sb.Append(pp.TotalATP)
                sb.Append(delimiter)
                sb.Append(pp.TotalPermintaan)
                sb.Append(delimiter)
                sb.Append(pp.VechileColor.VechileType.ProductCategory.Code)
                objStreamWriter.WriteLine(sb.ToString)
            Next
            CloseStreamWriter()

            Return _fileInfo
        End Function

        Public Function TransferDPtoSAP(ByVal dpColl As ArrayList, ByVal servermap As FileInfo, Optional ByVal IsAccelerate As Boolean = False) As FileInfo
            Dim sb As StringBuilder
            Dim fileName As String = servermap.Directory.FullName & "\" & KTB.DNet.Lib.WebConfig.GetValue("DailyPaymentDestFileDirectory") & DPFileName
            'Dim i As Integer
            Dim _fileInfo As FileInfo = CreateFile(fileName)
            'write Header
            sb = New StringBuilder
            sb.Append("Dealer")
            sb.Append(delimiter)
            sb.Append("No P/O")
            sb.Append(delimiter)
            sb.Append("No S/O")
            sb.Append(delimiter)
            sb.Append("Account")
            sb.Append(delimiter)
            sb.Append("No Kwitansi")
            sb.Append(delimiter)
            sb.Append("Tujuan Pembayaran")
            sb.Append(delimiter)
            sb.Append("No Giro/Slip")
            sb.Append(delimiter)
            sb.Append("")
            sb.Append("Tgl Posting")
            sb.Append(delimiter)
            If IsAccelerate = False Then
                sb.Append("Tgl Jatuh Tempo")
            Else
                sb.Append("Tgl Percepatan")

                sb.Append(delimiter)
                sb.Append("TOP Days(Awal)")
                sb.Append(delimiter)
                sb.Append("Hari Terpakai (Days Used)")
            End If
            sb.Append(delimiter)
            sb.Append("Jumlah")
            sb.Append(delimiter)
            If IsAccelerate Then
                sb.Append("Ref Gyro Awal")
                sb.Append(delimiter)
                sb.Append("Nilai Gyro Awal")
                sb.Append(delimiter)
                sb.Append("IT Rekalkulasi")
                sb.Append(delimiter)
                sb.Append("Selisih")
                sb.Append(delimiter)
                sb.Append("PPh Awal")
                sb.Append(delimiter)
                sb.Append("PPh Rekalkulasi")
                sb.Append(delimiter)
            End If
            sb.Append("Diproses")
            sb.Append(delimiter)
            sb.Append("Tolakan")
            sb.Append(delimiter)
            sb.Append("User Validasi")
            sb.Append(delimiter)
            sb.Append("Waktu Validasi")
            objStreamWriter.WriteLine(sb.ToString)

            For Each dp As DailyPayment In dpColl
                'i = 0
                sb = New StringBuilder
                sb.Append(dp.POHeader.ContractHeader.Dealer.DealerCode)
                sb.Append(delimiter)
                sb.Append(dp.POHeader.PONumber)
                sb.Append(delimiter)
                sb.Append(dp.POHeader.SONumber)
                sb.Append(delimiter)
                sb.Append(dp.DocNumber)
                sb.Append(delimiter)
                sb.Append(dp.ReceiptNumber)
                sb.Append(delimiter)
                sb.Append(dp.PaymentPurpose.Description)
                sb.Append(delimiter)
                sb.Append(dp.SlipNumber)
                sb.Append(delimiter)
                If IsAccelerate = True Then
                    If Not IsNothing(dp.OldDailyPayment) AndAlso dp.OldDailyPayment.ID > 0 Then
                        sb.Append(Format(dp.OldDailyPayment.DocDate, "dd/MM/yyyy"))
                    Else
                        sb.Append(Format(dp.DocDate, "dd/MM/yyyy"))
                    End If
                Else
                    sb.Append(Format(dp.DocDate, "dd/MM/yyyy"))
                End If
                sb.Append(delimiter)
                sb.Append(Format(dp.BaselineDate, "dd/MM/yyyy"))
                If IsAccelerate = True Then
                    If Not IsNothing(dp.OldDailyPayment) AndAlso dp.OldDailyPayment.ID > 0 Then
                        If dp.GyroType = EnumGyroType.GyroType.Percepatan Then
                            sb.Append(delimiter)
                            sb.Append("" & dp.POHeader.TermOfPayment.TermOfPaymentValue.ToString)
                            sb.Append(delimiter)
                            sb.Append("" & Math.Abs(DateDiff(DateInterval.Day, dp.POHeader.ReqAllocationDateTime, dp.BaselineDate)).ToString)
                        Else
                            sb.Append(delimiter)
                            sb.Append("") '& dp.POHeader.TermOfPayment.TermOfPaymentValue.ToString)
                            sb.Append(delimiter)
                            sb.Append("" & "0")
                        End If
                    Else
                        sb.Append(delimiter)
                        sb.Append("") '& dp.POHeader.TermOfPayment.TermOfPaymentValue.ToString)
                        sb.Append(delimiter)
                        sb.Append("" & "0")
                    End If
                End If
                sb.Append(delimiter)
                sb.Append(FormatNumber(dp.Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.False))
                sb.Append(delimiter)
                If IsAccelerate = True Then
                    Dim SlipNumberA As String = "", GyroAmountA As Decimal = 0, IntRecalculation As Decimal = 0, IntDifference As Decimal = 0, PPhDifference As Decimal = 0
                    If Not IsNothing(dp.OldDailyPayment) AndAlso dp.OldDailyPayment.ID > 0 Then
                        Dim oDPFac As New DailyPaymentFacade(System.Threading.Thread.CurrentPrincipal)

                        SlipNumberA = dp.OldDailyPayment.SlipNumber
                        GyroAmountA = dp.OldDailyPayment.Amount
                        If dp.GyroType = EnumGyroType.GyroType.Percepatan Then
                            oDPFac.SetInterestDiffOfAccelerate(dp.OldDailyPayment, IntDifference, PPhDifference, dp.GyroType, dp.OldDailyPayment.AcceleratedDate, dp.BaselineDate, IntRecalculation)
                        Else
                            IntDifference = 0
                            PPhDifference = 0
                            IntRecalculation = 0
                        End If
                        'IntRecalculation = dp.OldDailyPayment.Amount - dp.Amount ' - IntDifference
                    End If
                    sb.Append(SlipNumberA)
                    sb.Append(delimiter)
                    sb.Append(FormatNumber(GyroAmountA, 0, TriState.UseDefault, TriState.UseDefault, TriState.False))
                    sb.Append(delimiter)

                    sb.Append(FormatNumber(IntRecalculation, 0, TriState.UseDefault, TriState.UseDefault, TriState.False))
                    sb.Append(delimiter)
                    sb.Append(FormatNumber(IntDifference, 0, TriState.UseDefault, TriState.UseDefault, TriState.False))
                    sb.Append(delimiter)

                    'sb.Append(FormatNumber(PPhDifference, 0, TriState.UseDefault, TriState.UseDefault, TriState.False))
                    sb.Append(FormatNumber((IntRecalculation + IntDifference) * 0.15 / 0.85, 0, TriState.UseDefault, TriState.UseDefault, TriState.False))
                    sb.Append(delimiter)
                    sb.Append(FormatNumber((IntRecalculation / 0.85 * 0.15), 0, TriState.UseDefault, TriState.UseDefault, TriState.False))
                    sb.Append(delimiter)
                End If
                sb.Append(dp.SAPCreator)
                sb.Append(delimiter)
                If dp.RejectStatus <> 0 Then
                    sb.Append(CType(dp.RejectStatus, RejectStatusPayment.RejectStatusEnum).ToString)
                End If
                sb.Append(delimiter)
                sb.Append(dp.UserValidator)
                sb.Append(delimiter)
                sb.Append(IIf(dp.TimeValidation.Year < 2000, "", dp.TimeValidation.ToString("dd/MM/yyyy")))
                objStreamWriter.WriteLine(sb.ToString)
            Next
            CloseStreamWriter()
            'CopyFileToSAPServer(_fileInfo.FullName)
            Return _fileInfo
        End Function

        Public Function TransferWSCtoSAP(ByVal wscColl As ArrayList, ByVal servermap As FileInfo) As FileInfo
            Dim sb As StringBuilder
            Dim fileName As String = servermap.Directory.FullName & "\" & KTB.DNet.Lib.WebConfig.GetValue("WSCEvidenceFileDirectory") & "\" & WSCFileName
            Dim _fileInfo As FileInfo = CreateFile(fileName)
            For Each wsc As WSCEvidence In wscColl
                sb = New StringBuilder
                sb.Append(wsc.WSCHeader.Dealer.DealerCode)
                sb.Append(delimiter)
                sb.Append(wsc.WSCHeader.ClaimNumber)
                sb.Append(delimiter)
                sb.Append(wsc.WSCHeader.ChassisMaster.ChassisNumber)
                objStreamWriter.WriteLine(sb.ToString)
            Next
            CloseStreamWriter()
            Return _fileInfo
        End Function


        Public Function TransferWSCBBtoSAP(ByVal wscColl As ArrayList, ByVal servermap As FileInfo) As FileInfo
            Dim sb As StringBuilder
            Dim fileName As String = servermap.Directory.FullName & "\" & KTB.DNet.Lib.WebConfig.GetValue("WSCEvidenceBBFileDirectory") & "\" & WSCFileNameBB
            Dim _fileInfo As FileInfo = CreateFile(fileName)
            For Each wsc As WSCEvidenceBB In wscColl
                sb = New StringBuilder
                sb.Append(wsc.WSCHeaderBB.Dealer.DealerCode)
                sb.Append(delimiter)
                sb.Append(wsc.WSCHeaderBB.ClaimNumber)
                sb.Append(delimiter)
                sb.Append(wsc.WSCHeaderBB.ChassisMasterBB.ChassisNumber)
                objStreamWriter.WriteLine(sb.ToString)
            Next
            CloseStreamWriter()
            Return _fileInfo
        End Function

        Public Function TransferAnnualtofile(ByVal annual As ArrayList, ByVal servermap As FileInfo, ByVal ValidateFrom As String, ByVal ValidateTo As String) As FileInfo
            Dim sb As StringBuilder
            Dim s As New StringBuilder
            Dim ValFrom As String = Mid((ValidateFrom.Replace("/", "")), 3, 2) & Right((ValidateFrom.Replace("/", "")), 2)
            Dim ValTo As String = Mid((ValidateTo.Replace("/", "")), 3, 2) & Right((ValidateTo.Replace("/", "")), 2)
            Dim filename As String = servermap.Directory.FullName & "\" & KTB.DNet.Lib.WebConfig.GetValue("AnnualDiscountAchivementFileDirectory") & "\" & AnnualFileName & ValFrom & "-" & ValTo & ".xls"
            Dim _fileinfo As FileInfo = CreateFile(filename)
            s.Append("Nomor Barang")
            s.Append(delimiter)
            s.Append("Nama Barang")
            s.Append(delimiter)
            s.Append("Point")
            s.Append(delimiter)
            s.Append("Minimum Qty")
            s.Append(delimiter)
            s.Append("Jumlah Pembelian Bulan Ini")
            s.Append(delimiter)
            s.Append("Jumlah Pembelian")
            s.Append(delimiter)
            s.Append("Jumlah Annual Discount")
            s.Append(delimiter)
            s.Append("Nilai Annual Discount")
            s.Append(delimiter)
            s.Append("Sisa")
            s.Append(delimiter)
            objStreamWriter.WriteLine(s.ToString)
            For Each _Ann As AnnualDiscountAchievement In annual
                sb = New StringBuilder
                sb.Append(_Ann.MaterialCode)
                sb.Append(delimiter)
                sb.Append(_Ann.MaterialDescription)
                sb.Append(delimiter)
                sb.Append(_Ann.Point)
                sb.Append(delimiter)
                sb.Append(_Ann.MinimumQty)
                sb.Append(delimiter)
                sb.Append(_Ann.BillQtyThisMonth)
                sb.Append(delimiter)
                sb.Append(_Ann.BillQtyThisPeriod)
                sb.Append(delimiter)
                sb.Append(_Ann.RebateQtyThisPeriod)
                sb.Append(delimiter)
                sb.Append(_Ann.RebateAmountThisPeriod)
                sb.Append(delimiter)
                sb.Append(_Ann.RemainQty)
                objStreamWriter.WriteLine(sb.ToString)
            Next
            CloseStreamWriter()
            Return _fileinfo
        End Function


        Public Function TransferURPtoText(ByVal URPColl As ArrayList, ByVal servermap As FileInfo) As FileInfo
            Dim sb As StringBuilder
            Dim _delimiter As String = ";"
            Dim fileName As String = servermap.Directory.FullName & "\" & KTB.DNet.Lib.WebConfig.GetValue("URPDestFileDirectory") & UploadRencanaProduksiFileName
            'Dim i As Integer
            Dim _fileInfo As FileInfo = CreateFile(fileName)
            For Each urp As PKProductionPlan In URPColl
                'i = 0
                sb = New StringBuilder
                sb.Append(urp.PeriodMonth)
                sb.Append(_delimiter)
                sb.Append(urp.PeriodYear)
                sb.Append(_delimiter)
                sb.Append(urp.ProductionYear)
                sb.Append(_delimiter)
                sb.Append(urp.VechileColor.MaterialNumber)
                sb.Append(_delimiter)
                sb.Append(urp.PlanQty)
                sb.Append(_delimiter)
                sb.Append(urp.CarryOverPreviousQty)
                sb.Append(_delimiter)
                sb.Append(urp.UnselledStock)
                sb.Append(_delimiter)
                sb.Append(urp.TotalPermintaan)
                sb.Append(_delimiter)
                sb.Append(urp.TotalAlokasi)
                sb.Append(_delimiter)
                sb.Append(urp.TotalReleaseAndAgree)
                sb.Append(_delimiter)
                sb.Append(urp.TotalSelesai)
                objStreamWriter.WriteLine(sb.ToString)
            Next
            CloseStreamWriter()
            Return _fileInfo
        End Function

        Public Function TransferEquipmenttoText(ByVal EqColl As ArrayList, ByVal servermap As FileInfo) As FileInfo
            Dim sb As StringBuilder
            Dim _delimiter As String = ";"
            Dim fileName As String = servermap.Directory.FullName & "\" & KTB.DNet.Lib.WebConfig.GetValue("EqDestFileDirectory") & UploadEquipmentFileName
            'Dim i As Integer
            Dim _fileInfo As FileInfo = CreateFile(fileName)
            For Each eq As EquipmentMaster In EqColl
                'i = 0
                sb = New StringBuilder
                If eq.Status = 1 Then
                    sb.Append("X")
                Else
                    sb.Append("")
                End If
                sb.Append(_delimiter)
                sb.Append(eq.EquipmentNumber)
                sb.Append(_delimiter)

                If eq.Kind = EquipmentKind.EquipmentKindEnum.Pembelian Then
                    sb.Append("EQUIP")
                Else
                    If eq.Kind = EquipmentKind.EquipmentKindEnum.Perbaikan Then
                        sb.Append("REPAIR")
                    Else
                        sb.Append(eq.Kind)
                    End If
                End If

                sb.Append(_delimiter)
                sb.Append(eq.Description)
                objStreamWriter.WriteLine(sb.ToString)
            Next
            CloseStreamWriter()
            Return _fileInfo
        End Function

        Public Function TransferKewajibanDepositBToSAP(ByVal pEColl As ArrayList) As FileInfo
            ' StartImpersonet()
            'H DealerCode No P3B Tgl Pengajuan Tgl Kirim No Reg PO UserID 
            'D EquipmentCode Qty DiscType DiscValue 
            Dim sb As StringBuilder
            Dim _dealerFacade As DealerFacade
            Dim fileName As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFOLDER") & PEFileName
            'Dim i As Integer
            Dim _fileInfo As FileInfo = CreateFile(fileName)

            For Each pE As DepositBKewajibanHeader In pEColl
                'i = 0
                sb = New StringBuilder
                sb.Append("H")
                sb.Append(delimiter)
                sb.Append(pE.Dealer.DealerCode)
                sb.Append(delimiter)
                sb.Append(pE.NoRegKewajiban)
                sb.Append(delimiter)
                sb.Append(ConvertDate(pE.CreatedTime))
                sb.Append(delimiter)
                sb.Append(ConvertDate(Date.Now))
                sb.Append(delimiter)
                sb.Append(pE.NoRegKewajiban)
                sb.Append(delimiter)
                Try
                    Dim DealerCode As Integer = Convert.ToInt16(pE.CreatedBy.Substring(0, 6).TrimStart("0"))
                    Dim objDealer As Dealer = New DealerFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(DealerCode)
                    If objDealer.ID > 0 Then
                        sb.Append(objDealer.DealerCode & "-" & pE.CreatedBy.Remove(0, 6))
                    Else
                        sb.Append(" " & "-" & pE.CreatedBy.Remove(0, 6))
                    End If
                Catch ex As Exception
                    sb.Append(" ")
                End Try

                sb.Append(delimiter)
                objStreamWriter.WriteLine(sb.ToString)
                For Each item As DepositBKewajibanDetail In pE.DepositBKewajibanDetails
                    sb = New StringBuilder
                    sb.Append("D")
                    sb.Append(delimiter)
                    sb.Append(item.EquipmentMaster.EquipmentNumber)
                    sb.Append(delimiter)
                    sb.Append(item.Qty)
                    sb.Append(delimiter)
                    sb.Append(0) 'item.Discount
                    objStreamWriter.WriteLine(sb.ToString)
                    'i = i + 10
                Next
            Next
            CloseStreamWriter()
            CopyFileToSAPServer(_fileInfo.FullName, EnumSAPFileType.SAPFileType.ServiceDepositBEquipment)
            Return _fileInfo
        End Function

        Public Function TransferPencairanDepositBToSAP(ByVal arrList As ArrayList) As FileInfo
            Dim sb As StringBuilder
            Dim _dealerFacade As DealerFacade
            Dim fileName As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFOLDER") & DepBFileName
            Dim _fileInfo As FileInfo = CreateFile(fileName)

            Dim delimiter = ";"

            For Each objDeposit As DepositBPencairanHeader In arrList
                sb = New StringBuilder
                sb.Append(objDeposit.ProductCategory.Code.ToUpper) 'MMC / MFTBC
                sb.Append(delimiter)
                'sb.Append(DepositBEnum.GetStringValueTipePengajuan(objDeposit.TipePengajuan).ToUpper()) 'MMC / MFTBC
                'sb.Append(delimiter)
                Select Case objDeposit.TipePengajuan 'ID : PAY, OFF, REC
                    Case DepositBEnum.TipePengajuan.Transfer '1
                        sb.Append("PAY")
                    Case DepositBEnum.TipePengajuan.ProjectService '2
                        sb.Append("OFF")
                    Case DepositBEnum.TipePengajuan.Offset_SP '4
                        sb.Append("REC")
                    Case DepositBEnum.TipePengajuan.Kewajiban_NonReguler '6
                        sb.Append("REC") 'update from OFF to REC by anh 20161206 email nadya/PakHardi
                    Case DepositBEnum.TipePengajuan.KewajibanReguler '5
                        sb.Append("OFF") 'update from REC to OFF by anh 20161206 email nadya/PakHardi
                    Case DepositBEnum.TipePengajuan.Interest '3
                        sb.Append("INT")
                End Select
                sb.Append(delimiter)
                sb.Append(objDeposit.Dealer.DealerCode) ' Dealer Code
                sb.Append(delimiter)

                'remarks by anh 2017-02-01 'req by yurike
                'sb.Append(FormatNumber(objDeposit.ApprovalAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.False))
                'untuk intereset ambil dari netto
                Select Case objDeposit.TipePengajuan '
                    Case DepositBEnum.TipePengajuan.Interest
                        sb.Append(FormatNumber(objDeposit.DepositBInterestHeader.NettoAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.False))
                    Case Else
                        sb.Append(FormatNumber(objDeposit.ApprovalAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.False))
                End Select

                Dim objKuitansi As DepositBReceipt = New DepositBReceipt
                If objDeposit.DepositBReceipts.Count > 0 Then
                    objKuitansi = CType(objDeposit.DepositBReceipts(0), DepositBReceipt)
                End If
                If objKuitansi.ID > 0 Then
                    sb.Append(delimiter)
                    sb.Append(objKuitansi.TanggalKuitansi.ToString("yyyyMMdd")) 'Doc Date
                    sb.Append(delimiter)
                    'sb.Append(objKuitansi.TanggalTransfer.ToString("yyyyMMdd")) 'Due Date
                    sb.Append(Date.Now.ToString("yyyyMMdd")) 'Due Date
                Else
                    sb.Append(delimiter)
                    sb.Append("") 'Doc Date
                    sb.Append(delimiter)
                    sb.Append("") 'Due Date
                End If

                sb.Append(delimiter)
                Select Case objDeposit.TipePengajuan 'Assignment
                    Case DepositBEnum.TipePengajuan.Transfer '1
                        sb.Append("Transfer")
                    Case DepositBEnum.TipePengajuan.ProjectService '2
                        sb.Append(objDeposit.DepositBDebitNote.DNNumber)
                    Case DepositBEnum.TipePengajuan.Offset_SP '4
                        If Not IsNothing(objDeposit.IndentPartHeader) AndAlso Not IsNothing(objDeposit.DepositBKewajibanHeader) Then
                            Dim arlEstimation As ArrayList
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "DepositBKewajibanHeader.ID", MatchType.Exact, objDeposit.DepositBKewajibanHeader.ID))
                            arlEstimation = New KTB.DNet.BusinessFacade.IndentPartEquipment.EstimationEquipHeaderFacade(User).Retrieve(criterias)
                            If arlEstimation.Count > 0 Then
                                Dim objEstimation As EstimationEquipHeader = CType(arlEstimation(0), EstimationEquipHeader)
                                'sb.Append(objDeposit.IndentPartHeader.RequestNo)
                                sb.Append(objEstimation.EstimationNumber)
                            End If
                        Else
                            sb.Append("")
                        End If
                    Case DepositBEnum.TipePengajuan.Kewajiban_NonReguler '6
                        If Not IsNothing(objDeposit.IndentPartHeader) Then
                            Dim arlEstimation As ArrayList
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "DepositBKewajibanHeader.ID", MatchType.Exact, objDeposit.DepositBKewajibanHeader.ID))
                            arlEstimation = New KTB.DNet.BusinessFacade.IndentPartEquipment.EstimationEquipHeaderFacade(User).Retrieve(criterias)
                            If arlEstimation.Count > 0 Then
                                Dim objEstimation As EstimationEquipHeader = CType(arlEstimation(0), EstimationEquipHeader)
                                'sb.Append(objDeposit.IndentPartHeader.RequestNo)
                                sb.Append(objEstimation.EstimationNumber)
                            End If
                        Else
                            sb.Append("")
                        End If
                    Case DepositBEnum.TipePengajuan.KewajibanReguler '5
                        sb.Append(objDeposit.DepositBKewajibanHeader.NoSalesorder)
                    Case DepositBEnum.TipePengajuan.Interest '3
                        sb.Append("Deposit B Interest")
                End Select
                sb.Append(delimiter)
                If objKuitansi.ID > 0 Then
                    sb.Append(objKuitansi.NoRegKuitansi) 'NoRegKuitansi
                Else
                    sb.Append("") 'NoRegKuitansi
                End If
                sb.Append(delimiter)
                If Not IsNothing(objDeposit.DepositBPencairanDetails) AndAlso objDeposit.DepositBPencairanDetails.Count > 0 Then
                    Dim objDepBdetail As DepositBPencairanDetail
                    objDepBdetail = CType(objDeposit.DepositBPencairanDetails(0), DepositBPencairanDetail)
                    sb.Append(objDepBdetail.Description)
                Else
                    sb.Append(String.Empty)
                End If

                sb.Append(delimiter)
                If Not IsNothing(objDeposit.DealerBankAccount) Then
                    sb.Append(objDeposit.DealerBankAccount.BankAccount)
                Else
                    sb.Append(String.Empty)
                End If

                'sb.Append(objDeposit.Keterangan)
                objStreamWriter.WriteLine(sb.ToString)

            Next
            CloseStreamWriter()
            CopyFileToSAPServer(_fileInfo.FullName, EnumSAPFileType.SAPFileType.ServiceDepositBPengajuan)
            Return _fileInfo
        End Function
        Public Function TransferPEtoSAP(ByVal pEColl As ArrayList) As FileInfo
            ' StartImpersonet()
            'H DealerCode No P3B Tgl Pengajuan Tgl Kirim No Reg PO UserID 
            'D EquipmentCode Qty DiscType DiscValue 
            Dim sb As StringBuilder
            Dim _dealerFacade As DealerFacade
            Dim fileName As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFOLDER") & PEFileName
            'Dim i As Integer
            Dim _fileInfo As FileInfo = CreateFile(fileName)
            For Each pE As EquipmentSalesHeader In pEColl
                'i = 0
                sb = New StringBuilder
                sb.Append("H")
                sb.Append(delimiter)
                sb.Append(pE.Dealer.DealerCode)
                sb.Append(delimiter)
                sb.Append(pE.PONumber)
                sb.Append(delimiter)
                sb.Append(ConvertDate(pE.CreatedTime))
                sb.Append(delimiter)
                sb.Append(ConvertDate(pE.ReqDeliveryDate))
                sb.Append(delimiter)
                sb.Append(pE.RegPONumber)
                sb.Append(delimiter)
                Try
                    Dim DealerCode As Integer = Convert.ToInt16(pE.CreatedBy.Substring(0, 6).TrimStart("0"))
                    Dim objDealer As Dealer = New DealerFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(DealerCode)
                    If objDealer.ID > 0 Then
                        sb.Append(objDealer.DealerCode & "-" & pE.CreatedBy.Remove(0, 6))
                    Else
                        sb.Append(" " & "-" & pE.CreatedBy.Remove(0, 6))
                    End If
                Catch ex As Exception
                    sb.Append(" ")
                End Try

                sb.Append(delimiter)
                objStreamWriter.WriteLine(sb.ToString)
                For Each item As EquipmentSalesDetail In pE.EquipmentSalesDetails
                    sb = New StringBuilder
                    sb.Append("D")
                    sb.Append(delimiter)
                    sb.Append(item.EquipmentMaster.EquipmentNumber)
                    sb.Append(delimiter)
                    sb.Append(item.Quantity)
                    sb.Append(delimiter)
                    sb.Append(item.Discount)
                    objStreamWriter.WriteLine(sb.ToString)
                    'i = i + 10
                Next
                'Remark By heru dari Pak arry
                'For Each item As EquipmentSalesPayment In pE.EquipmentSalesPayments
                '    sb = New StringBuilder
                '    sb.Append("D")
                '    sb.Append(delimiter)
                '    'sb.Append(item.VechileColor.MaterialNumber)
                '    sb.Append(item.KwitansiNumber)
                '    sb.Append(delimiter)
                '    sb.Append(item.Amount)
                '    sb.Append(delimiter)
                '    sb.Append(item.CreatedBy)
                '    sb.Append(delimiter)
                '    sb.Append(item.CreatedTime)
                '    objStreamWriter.WriteLine(sb.ToString)
                '    'i = i + 10
                'Next
            Next
            CloseStreamWriter()
            CopyFileToSAPServer(_fileInfo.FullName, EnumSAPFileType.SAPFileType.ServiceEquipment)
            Return _fileInfo
        End Function

        Public Function TransferAnnualDiscountToXLS(ByVal arlAnnual As ArrayList, ByVal servermap As FileInfo, ByVal ValidFrom As String, ByVal ValidTo As String) As FileInfo
            Dim sb As StringBuilder
            Dim s As New StringBuilder
            Dim fileName As String = servermap.Directory.FullName & "\" & KTB.DNet.Lib.WebConfig.GetValue("AnnualDiscountFileDirectory") & "\" & AnnualDiscountFileName & Mid(ValidFrom, 3, 2) & Right(ValidFrom, 2) & "-" & Mid(ValidTo, 3, 2) & Right(ValidTo, 2) & ".xls"
            Dim _fileInfo As FileInfo = CreateFile(fileName)
            s.Append("Nomor Barang")
            s.Append(delimiter)
            s.Append("Nama Barang")
            s.Append(delimiter)
            s.Append("Model")
            s.Append(delimiter)
            s.Append("Minimum Qty")
            s.Append(delimiter)
            s.Append("Point")
            objStreamWriter.WriteLine(s.ToString)
            For Each Annual As AnnualDiscount In arlAnnual
                sb = New StringBuilder
                sb.Append(Annual.PartNo)
                sb.Append(delimiter)
                sb.Append(Annual.PartName)
                sb.Append(delimiter)
                sb.Append(Annual.Model)
                sb.Append(delimiter)
                sb.Append(Annual.MinimumQty)
                sb.Append(delimiter)
                sb.Append(Annual.Point)
                objStreamWriter.WriteLine(sb.ToString)
            Next
            CloseStreamWriter()
            Return _fileInfo
        End Function

        Public Sub TransferToListWebServer(ByVal SourceFileInfo As FileInfo, ByVal variableFolder As String)
            If Not SourceFileInfo Is Nothing Then
                Dim finfo As FileInfo
                Dim fName As String
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False
                Try
                    success = imp.Start()
                    If success Then
                        For Each item As String In KTB.DNet.Lib.WebConfig.GetValue("WSCDirectory").Split(";")
                            fName = item & variableFolder & "\" & SourceFileInfo.Name
                            finfo = New FileInfo(fName)
                            If Not finfo.Directory.Exists Then
                                finfo.Directory.Create()
                            End If
                            SourceFileInfo.CopyTo(fName, True)
                        Next
                        imp.StopImpersonate()
                        imp = Nothing
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
            Else
                Throw New Exception("File info belum dipilih")
            End If

        End Sub

        Public Sub TransferToListWebServer(ByVal SourceFileInfo As FileInfo, ByVal variableFolder As String, ByVal isImpersonate As Boolean, ByVal configKey As String)
            If Not SourceFileInfo Is Nothing Then
                Dim finfo As FileInfo
                Dim fName As String
                Try
                    For Each item As String In KTB.DNet.Lib.WebConfig.GetValue(configKey).Split(";")
                        fName = item & variableFolder & "\" & SourceFileInfo.Name
                        finfo = New FileInfo(fName)
                        If Not finfo.Directory.Exists Then
                            finfo.Directory.Create()
                        End If
                        SourceFileInfo.CopyTo(fName, True)
                    Next
                Catch ex As Exception
                    Throw ex
                End Try
            Else
                Throw New Exception("File info belum dipilih")
            End If

        End Sub

        Public Sub DeleteFileInWebServer(ByVal SourceFileInfo As FileInfo, ByVal variableFolder As String, ByVal isImpersonate As Boolean, ByVal configKey As String)
            If Not SourceFileInfo Is Nothing Then
                Dim finfo As FileInfo
                Dim fName As String
                Try
                    For Each item As String In KTB.DNet.Lib.WebConfig.GetValue(configKey).Split(";")
                        fName = item & variableFolder & "\" & SourceFileInfo.Name
                        finfo = New FileInfo(fName)
                        If finfo.Exists Then
                            finfo.Delete()
                        End If
                    Next
                Catch ex As Exception
                    Throw ex
                End Try
            Else
                Throw New Exception("Gagal delete file")
            End If
        End Sub

        Public Function TransferOutStandingMOtoText(ByVal MODetailColl As ArrayList, ByVal servermap As FileInfo) As FileInfo
            Dim sb As StringBuilder
            Dim _delimiter As String = Chr(9)
            Dim fileName As String = servermap.Directory.FullName & "\" & KTB.DNet.Lib.WebConfig.GetValue("OutStandingMODestFileDirectory") & OutStandingMOFileName
            'Dim i As Integer
            Dim _fileInfo As FileInfo = CreateFile(fileName)
            For Each item As ContractDetail In MODetailColl
                'i = 0
                sb = New StringBuilder
                sb.Append(item.ContractHeader.Dealer.DealerCode)
                sb.Append(_delimiter)
                sb.Append(item.ContractHeader.ContractNumber)
                sb.Append(_delimiter)
                sb.Append(item.VechileColor.MaterialNumber)
                sb.Append(_delimiter)
                sb.Append(item.ContractHeader.ProjectName)
                sb.Append(_delimiter)
                sb.Append(item.TargetQty)
                sb.Append(_delimiter)
                sb.Append(item.SisaUnit)
                sb.Append(_delimiter)
                ' Modified by Ikhsan, 20081216
                ' Requested by Yurike/Peggy
                ' To distribute the amount of VH and PPH22
                'Dim sisaTebus As Double = CDbl(item.SisaUnit) * (CDbl(item.Amount) + CDbl(item.PPh22))
                'sb.Append(FormatNumber(sisaTebus, 0, TriState.False, TriState.False, TriState.False))
                'sb.Append(_delimiter)
                ' Start -----
                Dim sisaTebusVH As Double = CDbl(item.SisaUnit) * CDbl(item.Amount)
                sb.Append(FormatNumber(sisaTebusVH, 0, TriState.False, TriState.False, TriState.False))
                sb.Append(_delimiter)
                Dim sisaTebusPP As Double = CDbl(item.SisaUnit) * CDbl(item.PPh22)
                sb.Append(FormatNumber(sisaTebusPP, 0, TriState.False, TriState.False, TriState.False))
                sb.Append(_delimiter)
                ' End -------
                objStreamWriter.WriteLine(sb.ToString)
            Next
            CloseStreamWriter()
            Return _fileInfo
        End Function

        Private Sub ManageDataGyroBeforeTransafer(ByRef aDP As ArrayList)
            Dim aDPH As New ArrayList
            Dim aDPNew As New ArrayList
            Dim PrevRegNumber As String = String.Empty
            Dim IsExist As Boolean = False

            For Each oDP As DailyPayment In aDP
                If oDP.DailyPaymentHeader.RegNumber <> PrevRegNumber Then
                    IsExist = False
                    For Each oDPH As DailyPaymentHeader In aDPH
                        If oDP.DailyPaymentHeader.RegNumber = oDPH.RegNumber Then
                            IsExist = True
                            Exit For
                        End If
                    Next
                    If Not IsExist Then
                        aDPH.Add(oDP.DailyPaymentHeader)
                    End If
                    PrevRegNumber = oDP.DailyPaymentHeader.RegNumber
                End If
            Next
            For Each oDPH As DailyPaymentHeader In aDPH
                For Each oDP As DailyPayment In oDPH.DailyPayments
                    aDPNew.Add(oDP)
                Next
            Next
            aDP = aDPNew
        End Sub

        Public Function TransferGyroToSAP(ByVal sGyroType As String, ByVal arlData As ArrayList, ByVal PreFolder As String) As FileInfo
            Dim sb As StringBuilder
            Dim fileName As String
            Dim _fileInfo As FileInfo

            If sGyroType.Trim.ToUpper = "Normal".ToUpper Then
                fileName = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & PreFolder & "\" & GyroFileName
            ElseIf sGyroType.Trim.ToUpper = "Accelerate".ToUpper Then
                fileName = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & PreFolder & "\" & GyroPercepatanFileName
            End If

            ManageDataGyroBeforeTransafer(arlData) 'Transfer per Gyro not per assignment

            If arlData.Count > 0 Then
                _fileInfo = CreateFile(fileName)
                Dim i As Integer = 0

                Dim KeyDealer As String = ""
                Dim KeySlip As String = ""
                Dim KeyPP As Integer = 0
                Dim Keybaseline As DateTime = New Date(1902, 1, 1)
                Dim KeyRegNumber As String = ""

                For Each oDP As DailyPayment In arlData
                    'If KeyDealer <> oDP.POHeader.Dealer.DealerCode OrElse _
                    '    KeySlip <> oDP.SlipNumber OrElse _
                    '    KeyPP <> oDP.PaymentPurpose.ID OrElse _
                    '    Keybaseline <> oDP.BaselineDate Then
                    If KeyRegNumber <> oDP.DailyPaymentHeader.RegNumber Then
                        'Header
                        sb = New StringBuilder
                        sb.Append("H")
                        sb.Append(delimiter)
                        sb.Append(oDP.POHeader.Dealer.DealerCode)
                        sb.Append(delimiter)
                        sb.Append(oDP.BaselineDate.ToString("ddMMyy"))
                        sb.Append(delimiter)
                        sb.Append(oDP.PaymentPurpose.PaymentPurposeCode)
                        sb.Append(delimiter)
                        'YRK 2018-10-01
                        'sb.Append(oDP.SlipNumber)
                        If oDP.SlipNumber.Trim() = "" AndAlso Not IsNothing(oDP.DailyPaymentHeader) Then
                            sb.Append(oDP.DailyPaymentHeader.RegNumber)
                        Else
                            sb.Append(oDP.SlipNumber)
                        End If
                        'EOF YRK 2018-10-01

                        sb.Append(delimiter)
                        If oDP.PaymentPurpose.PaymentPurposeCode.ToUpper() = "LC" Then
                            sb.Append("")
                        Else
                            sb.Append(IIf(oDP.POHeader.IsFactoring = 1, "X", ""))
                        End If
                        sb.Append(delimiter)
                        Dim sGLIndicator As String = ""
                        If oDP.EntryType = EnumGyroEntryType.EntryType.Gyro Then
                            If oDP.PaymentPurpose.PaymentPurposeCode = "IT" OrElse oDP.PaymentPurpose.PaymentPurposeCode = "PP" OrElse oDP.PaymentPurpose.PaymentPurposeCode = "VH" OrElse oDP.PaymentPurpose.PaymentPurposeCode = "LC" Then
                                sGLIndicator = "U"
                                If oDP.PaymentPurpose.PaymentPurposeCode = "VH" AndAlso oDP.POHeader.TermOfPayment.PaymentType = enumPaymentType.PaymentType.TOP Then
                                    sGLIndicator = "G"
                                End If
                                If oDP.PaymentPurpose.PaymentPurposeCode = "LC" AndAlso oDP.POHeader.TermOfPayment.PaymentType = enumPaymentType.PaymentType.TOP Then
                                    sGLIndicator = "G"
                                End If
                            ElseIf oDP.PaymentPurpose.PaymentPurposeCode = "VH+PP" Then
                                sGLIndicator = "U"
                            ElseIf oDP.PaymentPurpose.PaymentPurposeCode = "VH+PP+IT" Then
                                sGLIndicator = "G"
                            End If
                        ElseIf oDP.EntryType = EnumGyroEntryType.EntryType.Transfer Then
                            sGLIndicator = "A"
                        End If
                        sb.Append(sGLIndicator)
                        sb.Append(delimiter)
                        Dim sGL As String = ""
                        If sGLIndicator = "G" Then
                            sGL = "11041000"
                        ElseIf sGLIndicator = "U" Then
                            sGL = "11041000"
                        ElseIf sGLIndicator = "A" Then
                            Dim sBankCode As String = oDP.GetBankCode
                            If sBankCode = "BTMU" Then
                                sGL = "11021909"
                            ElseIf sBankCode = "SUMI" Then
                                sGL = "11021904"
                            ElseIf sBankCode = "HSBC" Then
                                sGL = "11021909"
                            ElseIf sBankCode = "BCA" Then
                                sGL = "11021014"
                            ElseIf sBankCode = "MNDIRI" Then
                                sGL = "11021003"
                            End If
                        End If
                        sb.Append(sGL)
                        sb.Append(delimiter)
                        Dim sOldDocNumber As String = ""
                        Dim sFiscalYear As String = ""
                        If Not IsNothing(oDP.OldDailyPayment) AndAlso oDP.OldDailyPayment.ID > 0 Then
                            sOldDocNumber = oDP.OldDailyPayment.DocNumber
                            sFiscalYear = oDP.OldDailyPayment.FiscalYear.ToString
                        End If
                        sb.Append(IIf(sOldDocNumber = "", "", "X"))
                        sb.Append(delimiter)
                        sb.Append(sOldDocNumber)
                        sb.Append(delimiter)
                        sb.Append(sFiscalYear)
                        objStreamWriter.WriteLine(sb.ToString)
                        KeyRegNumber = oDP.DailyPaymentHeader.RegNumber
                    End If

                    'Detail
                    sb = New StringBuilder
                    sb.Append("D")
                    sb.Append(delimiter)
                    If oDP.PaymentPurpose.PaymentPurposeCode.ToLower().Equals("lc") Then

                        If Not IsNothing(oDP.POHeader.SalesOrders) AndAlso oDP.POHeader.SalesOrders.Count > 0 Then

                            Dim objSOLC As SalesOrder = CType(oDP.POHeader.SalesOrders(0), SalesOrder)
                            If Not IsNothing(objSOLC) AndAlso Not IsNothing(objSOLC.LogisticDCHeader) Then
                                sb.Append(objSOLC.LogisticDCHeader.DebitChargeNo)
                            Else
                                sb.Append("")
                            End If

                        Else
                            sb.Append("")
                        End If

                    Else
                        If oDP.POHeader.Status = enumStatusPO.Status.Baru Then
                            sb.Append(oDP.POHeader.PONumber)
                        Else
                            sb.Append(oDP.POHeader.SONumber)
                        End If
                    End If

                    sb.Append(delimiter)
                    sb.Append(FormatNumber(oDP.Amount, 0, TriState.False, TriState.False, TriState.False))
                    sb.Append(delimiter)
                    sb.Append(oDP.Ref1)
                    sb.Append(delimiter)
                    sb.Append(oDP.Ref2)
                    sb.Append(delimiter)
                    'sb.Append(oDP.Ref3)
                    Dim RegNumber As String
                    Try
                        RegNumber = oDP.DailyPaymentHeader.RegNumber
                    Catch ex As Exception
                        RegNumber = ""
                    End Try
                    sb.Append(RegNumber)
                    sb.Append(delimiter)
                    sb.Append(oDP.Reason)
                    objStreamWriter.WriteLine(sb.ToString)
                    i += 1
                Next
                CloseStreamWriter()
                CopyFileToSAPServer(_fileInfo.FullName, EnumSAPFileType.SAPFileType.FinishUnitPO, "\" & PreFolder & "\")
                Return _fileInfo
            Else
                Return New FileInfo(fileName)
            End If

        End Function

        Public Function TransferCessieToSAP(ByVal arlData As ArrayList) As FileInfo
            Dim sb As StringBuilder
            Dim fileName As String
            Dim _fileInfo As FileInfo

            fileName = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & GyroCessieFileName

            If arlData.Count > 0 Then
                _fileInfo = CreateFile(fileName)
                Dim i As Integer = 0

                For Each oCessie As Cessie In arlData
                    sb = New StringBuilder
                    sb.Append("H")
                    sb.Append(delimiter) 'DealerCode
                    sb.Append("180002")
                    sb.Append(delimiter)
                    Dim TransferDate As Date = DateSerial(1900, 1, 1)
                    If oCessie.CessieDetails.Count > 0 Then
                        Try
                            TransferDate = CType(oCessie.CessieDetails(0), CessieDetail).TransferDate
                        Catch ex As Exception
                            TransferDate = DateSerial(1900, 1, 1)
                        End Try
                    End If
                    sb.Append(TransferDate.ToString("ddMMyy")) 'TransferDate
                    sb.Append(delimiter)
                    sb.Append("VH+PP+IT") 'PaymentPurpose
                    sb.Append(delimiter)
                    Dim SlipNumber As String = ""
                    If oCessie.CessieDetails.Count > 0 Then
                        Try
                            SlipNumber = CType(oCessie.CessieDetails(0), CessieDetail).BankName & " " & CType(oCessie.CessieDetails(0), CessieDetail).RefNumber
                        Catch ex As Exception
                            SlipNumber = ""
                        End Try
                    End If
                    sb.Append(SlipNumber) 'SlipNumber
                    sb.Append(delimiter)
                    sb.Append("") 'FactoringIndicator
                    sb.Append(delimiter)
                    sb.Append("A") 'GLIndicator
                    sb.Append(delimiter)
                    'sb.Append("11021909") 'GLValue new 11021901
                    sb.Append(IIf(oCessie.CessieDate < New DateTime(2021, 12, 1), "11021909", "11021901")) 'GLValue
                    sb.Append(delimiter)
                    sb.Append("") 'AccelerateIndicator
                    sb.Append(delimiter)
                    sb.Append("") 'OldDocNumber
                    sb.Append(delimiter)
                    sb.Append("") 'FiscalYea

                    objStreamWriter.WriteLine(sb.ToString)

                    If oCessie.CessieDetails.Count > 0 Then
                        Dim oCessieDetail As CessieDetail
                        Try
                            oCessieDetail = CType(oCessie.CessieDetails(0), CessieDetail)
                        Catch ex As Exception
                            oCessieDetail = Nothing
                        End Try

                        If Not IsNothing(oCessieDetail) Then
                            sb = New StringBuilder
                            sb.Append("D")
                            sb.Append(delimiter)
                            sb.Append(oCessieDetail.Cessie.CessieNumber)  'PONumber
                            sb.Append(delimiter)
                            sb.Append(FormatNumber(oCessieDetail.Cessie.DifferenceAmount, 0, TriState.False, TriState.False, TriState.False))  'Amount
                            sb.Append(delimiter)
                            sb.Append("") 'Ref1
                            sb.Append(delimiter)
                            sb.Append("") 'Ref2
                            sb.Append(delimiter)
                            sb.Append(oCessieDetail.RegNumber) 'RegNumber->Ref3
                            sb.Append(delimiter)
                            sb.Append("") 'Reason
                            objStreamWriter.WriteLine(sb.ToString)
                        End If
                    End If
                    i += 1
                Next
                CloseStreamWriter()
                CopyFileToSAPServer(_fileInfo.FullName, EnumSAPFileType.SAPFileType.FinishUnitPO)
                Return _fileInfo
            Else
                Return Nothing ' New FileInfo(fileName)
            End If

        End Function

        Public Function TransferDPByDealer(ByVal dpColl As ArrayList, ByVal servermap As FileInfo) As FileInfo
            Dim sb As StringBuilder
            'Dim fileName As String = servermap.Directory.FullName & "\" & KTB.DNet.Lib.WebConfig.GetValue("DailyPaymentDestFileDirectory") & DPFileName
            Dim fileName As String = servermap.Directory.FullName & "\" & "DataTemp\" & "DaftarPembayaran" & Now.ToString("yyyyMMdd") & ".txt"

            'Dim i As Integer
            Dim _fileInfo As FileInfo = CreateFile(fileName)
            'write Header
            sb = New StringBuilder
            sb.Append("No")
            sb.Append(delimiter)
            sb.Append("Kode Dealer")
            sb.Append(delimiter)
            sb.Append("Status")
            sb.Append(delimiter)
            sb.Append("Dealer PO Number")
            sb.Append(delimiter)
            sb.Append("Assignment")
            sb.Append(delimiter)
            sb.Append("Tujuan Pembayaran")
            sb.Append(delimiter)
            sb.Append("Bank")
            sb.Append(delimiter)
            sb.Append("No BG/Cek")
            sb.Append(delimiter)
            sb.Append("Tgl Jatuh Tempo")
            sb.Append(delimiter)
            sb.Append("Jumlah")
            sb.Append(delimiter)
            sb.Append("User Validasi")
            sb.Append(delimiter)
            sb.Append("Waktu Validasi")
            objStreamWriter.WriteLine(sb.ToString)
            Dim i As Integer = 1
            For Each dp As DailyPayment In dpColl
                'i = 0
                sb = New StringBuilder
                sb.Append(i.ToString)
                sb.Append(delimiter)
                sb.Append(dp.POHeader.Dealer.DealerCode)
                sb.Append(delimiter)
                sb.Append(EnumPaymentStatus.GetStringValue(dp.Status))
                sb.Append(delimiter)
                sb.Append(dp.POHeader.DealerPONumber) 'Dealer PO Number
                sb.Append(delimiter)
                sb.Append(IIf(dp.POHeader.SONumber.Trim = "", dp.POHeader.PONumber, dp.POHeader.SONumber)) 'Assignment?
                sb.Append(delimiter)
                sb.Append(dp.PaymentPurpose.PaymentPurposeCode) 'PaymentPurpose
                sb.Append(delimiter)
                If dp.BankID = 0 Then
                    sb.Append(dp.SlipNumber)    'Bank 
                Else
                    sb.Append(dp.Bank.BankName)    'Bank 
                End If
                sb.Append(delimiter)
                sb.Append(dp.SlipNumber) '"No BG/Cek"
                sb.Append(delimiter)
                sb.Append(dp.BaselineDate.ToString("ddMMMyy")) ' "Tgl Jatuh Tempo"
                sb.Append(delimiter)
                sb.Append(FormatNumber(dp.Amount, 0, TriState.False, TriState.False, TriState.False))
                sb.Append(delimiter)
                sb.Append(dp.UserValidator)
                sb.Append(delimiter)
                sb.Append(IIf(dp.TimeValidation.Year < 2000, "", dp.TimeValidation.ToString("ddMMMyy")))
                objStreamWriter.WriteLine(sb.ToString)
                i += 1
            Next
            CloseStreamWriter()
            'CopyFileToSAPServer(_fileInfo.FullName)
            Return _fileInfo
        End Function

        Public Function TransferFactoringToSAP(ByVal aFMs As ArrayList) As FileInfo
            Dim sb As StringBuilder
            Dim fileName As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & "\" & FactoringFileName
            Dim _fileInfo As FileInfo
            If aFMs.Count > 0 Then
                _fileInfo = CreateFile(fileName)
                For Each oFM As FactoringMaster In aFMs
                    sb = New StringBuilder
                    sb.Append(oFM.CreditAccount)
                    sb.Append(delimiter)
                    sb.Append(oFM.ProductCategory.Name) 'name: mftbc:s101, mmc = s102
                    sb.Append(delimiter)
                    sb.Append(FormatNumber(oFM.FactoringCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.False))

                    objStreamWriter.WriteLine(sb.ToString)
                Next
                CloseStreamWriter()
                CopyFileToSAPServer(_fileInfo.FullName, EnumSAPFileType.SAPFileType.FinishUnit)
                Return _fileInfo
            Else
                Return New FileInfo(fileName)
            End If

        End Function


        Public Function TransferFactoringPaymentToSAP(ByVal aDPs As ArrayList) As FileInfo
            Dim sb As StringBuilder
            Dim fileName As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & "\" & FactoringPaymentFileName
            Dim _fileInfo As FileInfo

            If aDPs.Count > 0 Then
                _fileInfo = CreateFile(fileName)
                For Each oDP As DailyPayment In aDPs
                    sb = New StringBuilder
                    sb.Append(oDP.POHeader.SONumber)
                    sb.Append(delimiter)
                    sb.Append(oDP.DailyPaymentHeader.RegNumber)
                    sb.Append(delimiter)
                    sb.Append(oDP.EffectiveDate.ToString("ddMMyyyy"))

                    objStreamWriter.WriteLine(sb.ToString)
                Next
                CloseStreamWriter()
                CopyFileToSAPServer(_fileInfo.FullName, EnumSAPFileType.SAPFileType.FinishUnitFactoring)
                Return _fileInfo
            Else
                Return New FileInfo(fileName)
            End If

        End Function


        Public Function TransferPaymentTransferToSAP(ByVal oTP As TransferPayment) As FileInfo
            Dim sb As StringBuilder
            Dim fileName As String = String.Empty

            Dim _fileInfo As FileInfo
            Dim delimiter = ";"

            If oTP.PaymentPurpose.PaymentPurposeCode = "LC" Then
                fileName = KTB.DNet.Lib.WebConfig.GetValue("FinishUnitFileDirectory") & "\LC\" & PaymentConfName
            Else
                fileName = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & "\TransferPayment\" & PaymentConfName
            End If
            _fileInfo = CreateFile(fileName)
            sb = New StringBuilder()
            For Each oTPD As TransferPaymentDetail In oTP.TransferPaymentDetails
                If oTPD.TransferPayment.IsNotOnTime = 1 AndAlso oTPD.LastTransferPaymentDetail.ID > 0 Then
                    Dim oTPDOld As TransferPaymentDetail = oTPD.LastTransferPaymentDetail

                    sb.Append(oTPDOld.TransferPayment.RegNumber & delimiter)
                    If 1 = 1 AndAlso oTP.PaymentPurpose.PaymentPurposeCode.ToLower() = "lc" AndAlso Not IsNothing(oTPD.SalesOrder.LogisticDCHeader) Then
                        sb.Append(oTPD.SalesOrder.LogisticDCHeader.DebitChargeNo & delimiter)
                    Else
                        sb.Append(oTPD.SalesOrder.SONumber & delimiter)
                    End If
                    sb.Append(oTPDOld.TransferPayment.PlanTransferDate.ToString("yyyyMMdd") & delimiter)
                    sb.Append(CType(0, Integer) & delimiter) ' SAP will delete it when the amount is 0
                    sb.Append(oTPDOld.TransferPayment.PaymentPurpose.PaymentPurposeCode)
                    sb.AppendLine("")
                End If

                sb.Append(oTP.RegNumber & delimiter)
                If 1 = 1 AndAlso oTP.PaymentPurpose.PaymentPurposeCode.ToLower() = "lc" AndAlso Not IsNothing(oTPD.SalesOrder.LogisticDCHeader) Then
                    sb.Append(oTPD.SalesOrder.LogisticDCHeader.DebitChargeNo & delimiter)
                Else
                    sb.Append(oTPD.SalesOrder.SONumber & delimiter)
                End If

                sb.Append(oTP.PlanTransferDate.ToString("yyyyMMdd") & delimiter)
                sb.Append(FormatNumber(oTPD.Amount, 0, , , TriState.False) & delimiter)
                'sb.Append(CType(oTPD.Amount, Integer) & delimiter)
                sb.Append(oTP.PaymentPurpose.PaymentPurposeCode)
                sb.AppendLine("")
            Next
            objStreamWriter.WriteLine(sb.ToString)

            CloseStreamWriter()
            CopyFileToSAPServer(_fileInfo.FullName, EnumSAPFileType.SAPFileType.FinishUnitFactoring)

            Try
                Dim strCopy As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder")
                strCopy = strCopy + "\FinishUnit\Factoring\DNetHist\" + "Hist_" + _fileInfo.Name + ".Hist"

                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                If imp.Start() Then
                    _fileInfo.CopyTo(strCopy, True)
                    imp.StopImpersonate()
                    imp = Nothing
                End If
            Catch ex As Exception

            End Try

            Return _fileInfo
        End Function

        Public Function TransferVAIRToSAP(ByVal arlData As ArrayList, ByVal PreFolder As String) As FileInfo
            Dim sb As StringBuilder
            Dim fileName As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & PreFolder & "\" & IRPaymentConfFileName
            Dim _fileInfo As FileInfo
            Dim delimiter = ";"

            _fileInfo = CreateFile(fileName)
            For Each oRPH As RevisionPaymentHeader In arlData
                If oRPH.RevisionPaymentDetails.Count > 0 Then
                    For Each oRPD As RevisionPaymentDetail In oRPH.RevisionPaymentDetails
                        sb = New StringBuilder()
                        sb.Append(oRPH.RegNumber & delimiter)
                        sb.Append(oRPD.RevisionSAPDoc.DebitChargeNo & delimiter)
                        sb.Append(oRPH.GyroDate.ToString("yyyyMMdd") & delimiter)
                        sb.Append(IIf(FormatNumber(oRPD.RevisionSAPDoc.DCAmount, 0, TriState.False, TriState.False, TriState.False) = "", "0", FormatNumber(oRPD.RevisionSAPDoc.DCAmount, 0, TriState.False, TriState.False, TriState.False)) & delimiter)
                        sb.Append("IR")     'PaymentPurposeCode
                        objStreamWriter.WriteLine(sb.ToString)
                    Next
                End If
            Next

            CloseStreamWriter()
            CopyFileToSAPServer(_fileInfo.FullName, EnumSAPFileType.SAPFileType.FinishUnit, "\" & PreFolder & "\")

            Return _fileInfo

        End Function

        Public Function TransferGyroIRToSAP(ByVal sGyroType As String, ByVal arlData As ArrayList, ByVal PreFolder As String) As FileInfo
            Dim sb As StringBuilder
            Dim fileName As String
            Dim _fileInfo As FileInfo

            If sGyroType.Trim.ToUpper = "Gyro".ToUpper Then
                fileName = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & PreFolder & "\" & IRPaymentGyroFileName
            ElseIf sGyroType.Trim.ToUpper = "Transfer".ToUpper Then
                fileName = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & PreFolder & "\" & IRPaymentTransFileName
            End If

            If arlData.Count > 0 Then
                _fileInfo = CreateFile(fileName)

                Dim KeyDealer As String = ""
                Dim KeySlip As String = ""
                Dim KeyPP As Integer = 0
                Dim Keybaseline As DateTime = New Date(1902, 1, 1)
                Dim KeyRegNumber As String = ""
                Dim strAmount As String = "0"
                Dim sGLIndicator As String = ""

                For Each oRPH As RevisionPaymentHeader In arlData
                    If KeyRegNumber <> oRPH.RegNumber Then
                        'Header
                        sb = New StringBuilder
                        sb.Append("H")
                        sb.Append(delimiter)
                        sb.Append(oRPH.Dealer.DealerCode)
                        sb.Append(delimiter)
                        sb.Append(oRPH.GyroDate.ToString("ddMMyy"))
                        sb.Append(delimiter)
                        sb.Append("IR")     'PaymentPurposeCode
                        sb.Append(delimiter)
                        sb.Append(oRPH.SlipNumber)
                        sb.Append(delimiter)
                        sb.Append("")
                        sb.Append(delimiter)

                        If sGyroType.Trim.ToUpper = "Gyro".ToUpper Then
                            sGLIndicator = "G"
                        ElseIf sGyroType.Trim.ToUpper = "Transfer".ToUpper Then
                            sGLIndicator = "A"
                        Else
                            sGLIndicator = ""
                        End If

                        sb.Append(sGLIndicator)
                        sb.Append(delimiter)
                        Dim sGL As String = ""
                        If sGLIndicator = "G" Then
                            sGL = "11041000"
                        ElseIf sGLIndicator = "A" Then
                            sGL = "11021909"
                        ElseIf sGLIndicator = "U" Then
                            sGL = "11041000"
                        End If

                        sb.Append(sGL)
                        objStreamWriter.WriteLine(sb.ToString)
                        KeyRegNumber = oRPH.RegNumber
                    End If

                    strAmount = "0"
                    'Detail
                    For Each oRPD As RevisionPaymentDetail In oRPH.RevisionPaymentDetails
                        If oRPD.IsCancel = 0 Then
                            If IsNothing(oRPD.RevisionSAPDoc) Then oRPD.RevisionSAPDoc = New RevisionSAPDoc
                            If oRPD.RevisionSAPDoc.DCAmount = 0 OrElse oRPD.RevisionSAPDoc.DCAmount = vbNull Then
                                strAmount = "0"
                            Else
                                strAmount = FormatNumber(oRPD.RevisionSAPDoc.DCAmount, 0, TriState.False, TriState.False, TriState.False)
                            End If

                            sb = New StringBuilder
                            sb.Append("D")
                            sb.Append(delimiter)
                            sb.Append(oRPD.RevisionSAPDoc.DebitChargeNo)
                            sb.Append(delimiter)
                            sb.Append(strAmount)
                            sb.Append(delimiter)
                            sb.Append(delimiter)
                            sb.Append(delimiter)
                            Dim RegNumber As String
                            Try
                                RegNumber = oRPH.RegNumber
                            Catch ex As Exception
                                RegNumber = ""
                            End Try
                            sb.Append(RegNumber)
                            objStreamWriter.WriteLine(sb.ToString)
                        End If
                    Next
                Next
                CloseStreamWriter()
                CopyFileToSAPServer(_fileInfo.FullName, EnumSAPFileType.SAPFileType.FinishUnit, "\" & PreFolder & "\")
                Return _fileInfo
            Else
                Return New FileInfo(fileName)
            End If

        End Function

        Public Function TransferIRCancelToSAP(ByVal sGyroType As String, ByVal arlData As ArrayList, ByVal PreFolder As String) As FileInfo
            Dim sb As StringBuilder
            Dim fileName As String
            Dim _fileInfo As FileInfo

            If sGyroType.Trim.ToUpper = "Normal".ToUpper Then
                fileName = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & PreFolder & "\" & IRCancelFileName
            End If

            If arlData.Count > 0 Then
                _fileInfo = CreateFile(fileName)

                'Header
                Dim oRevisionPaymentHeader As RevisionPaymentHeader = CType(arlData(0), RevisionPaymentDetail).RevisionPaymentHeader
                sb = New StringBuilder
                sb.Append("H")
                sb.Append(delimiter)
                sb.Append(oRevisionPaymentHeader.RegNumber)
                sb.Append(delimiter)
                sb.Append(DateTime.Now.ToString("ddMMyy"))
                objStreamWriter.WriteLine(sb.ToString)

                'Detail
                For Each oRPD As RevisionPaymentDetail In arlData
                    sb = New StringBuilder
                    sb.Append("D")
                    sb.Append(delimiter)
                    sb.Append(oRPD.RevisionFaktur.ChassisMaster.ChassisNumber)
                    sb.Append(delimiter)
                    sb.Append(oRPD.RevisionSAPDoc.DebitChargeNo)
                    sb.Append(delimiter)
                    sb.Append(FormatNumber(oRPD.RevisionSAPDoc.DCAmount, 0, TriState.False, TriState.False, TriState.False))
                    sb.Append(delimiter)
                    sb.Append(oRPD.CancelReason)
                    objStreamWriter.WriteLine(sb.ToString)
                Next

                CloseStreamWriter()
                CopyFileToSAPServer(_fileInfo.FullName, EnumSAPFileType.SAPFileType.FinishUnit, "\" & PreFolder & "\")
                Return _fileInfo
            Else
                Return New FileInfo(fileName)
            End If
        End Function

        Public Function CopyToCustomerRequest(ByVal _header As SPKHeader, ByRef Msg As String, ByVal objUserInfo As UserInfo,
                                          ByVal objDealer As Dealer, ByVal AutoCustomerStatus As Boolean) As ArrayList
            Dim objCustomerRequestFacade As New CustomerRequestFacade(User)
            Dim objCustomerRequest As CustomerRequest = New CustomerRequest
            Dim _customer As SPKCustomer = _header.SPKCustomer
            'Dim objUserInfo As UserInfo = CType(SessionHelper.GetSession("LOGINUSERINFO"), UserInfo)
            'Dim objDealer As Dealer = SessionHelper.GetSession("DEALER")
            Dim oSPKDetailCustomerFac As New SPKDetailCustomerFacade(User)
            Dim _fileHelper As New FileHelper
            Dim arlCustRequest As ArrayList = New ArrayList
            'Dim AutoCustomerStatus As Boolean = SessionHelper.GetSession("AutoCustomerStatus")

            Try
                If _header.CustomerRequestID = 0 Then
                    objCustomerRequest.CustomerCode = String.Empty
                    objCustomerRequest.RequestNo = String.Empty
                    objCustomerRequest.RequestDate = DateTime.Today
                    objCustomerRequest.RequestType = EnumTipePengajuanCustomerRequest.TipePengajuanCustomerRequest.Baru
                    objCustomerRequest.RequestUserID = objUserInfo.ID
                    objCustomerRequest.Dealer = objDealer
                    objCustomerRequest.RefRequestNo = String.Empty
                    objCustomerRequest.Status1 = _header.SPKCustomer.TipeCustomer
                    objCustomerRequest.Name1 = _customer.Name1
                    objCustomerRequest.Name2 = _customer.Name2
                    objCustomerRequest.Name3 = _customer.Name3
                    objCustomerRequest.Alamat = _customer.Alamat
                    objCustomerRequest.Kelurahan = _customer.Kelurahan
                    objCustomerRequest.Kecamatan = _customer.Kecamatan
                    objCustomerRequest.CityID = _customer.City.ID
                    objCustomerRequest.PhoneNo = _customer.PhoneNo
                    objCustomerRequest.ReffCode = String.Empty
                    objCustomerRequest.PrintRegion = _customer.PrintRegion
                    objCustomerRequest.Email = _customer.Email
                    objCustomerRequest.PreArea = _customer.PreArea
                    objCustomerRequest.TipePerusahaan = _customer.TipePerusahaan
                    objCustomerRequest.PostalCode = _customer.PostalCode
                    objCustomerRequest.Attachment = String.Empty

                    Dim arrProfiles As ArrayList = _customer.SPKCustomerProfiles

                    Dim iReturn As Integer = 0
                    If _header.CustomerRequestID < 1 Then
                        iReturn = objCustomerRequestFacade.InsertFromSPKCustomer(objCustomerRequest, arrProfiles)
                    End If

                    If iReturn > 0 Then
                        Dim objSPKHeaderFacade As New SPKHeaderFacade(User)
                        _header.CustomerRequestID = iReturn
                        objSPKHeaderFacade.Update(_header)

                        objCustomerRequest = New CustomerRequest
                        objCustomerRequest = objCustomerRequestFacade.Retrieve(iReturn)
                        _fileHelper.UpdateStatusCustomerRequest(objCustomerRequest, AutoCustomerStatus, Msg)
                        If Not IsNothing(objCustomerRequest) Then
                            arlCustRequest.Add(objCustomerRequest)
                        End If
                    End If
                End If

                For Each oSPKD As SPKDetail In _header.SPKDetails
                    Dim arlResult As ArrayList = New ArrayList
                    arlResult = SendToCustomerRequest(objDealer, objUserInfo, AutoCustomerStatus, Msg, oSPKD.SPKDetailCustomers)
                    For Each oCustReq As CustomerRequest In arlResult
                        arlCustRequest.Add(oCustReq)
                    Next
                Next
                Return arlCustRequest
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Function

        Public Function SendToCustomerRequest(ByVal oDealer As Dealer, ByVal oUserInfo As UserInfo, ByVal AutoCustomerStatus As Boolean,
                                         ByRef Msg As String, ByVal oSPKDetailCustList As ArrayList) As ArrayList
            Dim objCustomerRequestFacade As New CustomerRequestFacade(User)
            Dim oSPKDetailCustomerFac As New SPKDetailCustomerFacade(User)
            Dim arlCustRequest As ArrayList = New ArrayList

            Try
                For Each oSPKDetailCust As SPKDetailCustomer In oSPKDetailCustList
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(SPKProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SPKProfile), "SPKDetailCustomer.ID", MatchType.Exact, oSPKDetailCust.ID))
                    Dim arrSPKProfile As ArrayList = New SPKProfileFacade(User).Retrieve(criterias)

                    If arrSPKProfile.Count = 0 Then
                        Msg = Msg + "SPK " + oSPKDetailCust.SPKDetail.SPKHeader.SPKNumber + ": Konsumen " + oSPKDetailCust.Name1 + "  belum mengisi profile kendaraan. \n"
                        Continue For
                    End If
                    If Not IsNothing(oSPKDetailCust.CustomerRequest) AndAlso oSPKDetailCust.CustomerRequest.ID > 0 Then
                        Continue For
                    End If

                    Dim objCustomerRequest As CustomerRequest = New CustomerRequest
                    objCustomerRequest.CustomerCode = String.Empty
                    objCustomerRequest.RequestNo = String.Empty
                    objCustomerRequest.RequestDate = DateTime.Today
                    objCustomerRequest.RequestType = EnumTipePengajuanCustomerRequest.TipePengajuanCustomerRequest.Baru
                    objCustomerRequest.RequestUserID = oUserInfo.ID
                    objCustomerRequest.Dealer = oDealer
                    objCustomerRequest.RefRequestNo = String.Empty
                    objCustomerRequest.ReffCode = String.Empty
                    objCustomerRequest.Attachment = String.Empty
                    objCustomerRequest.Status1 = oSPKDetailCust.TipeCustomer
                    objCustomerRequest.Name1 = oSPKDetailCust.Name1
                    objCustomerRequest.Name2 = oSPKDetailCust.Name2
                    objCustomerRequest.Name3 = oSPKDetailCust.Name3
                    objCustomerRequest.Alamat = oSPKDetailCust.Alamat
                    objCustomerRequest.Kelurahan = oSPKDetailCust.Kelurahan
                    objCustomerRequest.Kecamatan = oSPKDetailCust.Kecamatan
                    objCustomerRequest.CityID = oSPKDetailCust.City.ID
                    objCustomerRequest.PhoneNo = oSPKDetailCust.PhoneNo
                    objCustomerRequest.PrintRegion = oSPKDetailCust.PrintRegion
                    objCustomerRequest.Email = oSPKDetailCust.Email
                    objCustomerRequest.PreArea = oSPKDetailCust.PreArea
                    objCustomerRequest.TipePerusahaan = oSPKDetailCust.TipePerusahaan
                    objCustomerRequest.PostalCode = oSPKDetailCust.PostalCode
                    'cr spk
                    objCustomerRequest.TypePerorangan = oSPKDetailCust.TypePerorangan
                    objCustomerRequest.TypeIdentitas = oSPKDetailCust.TypeIdentitas
                    objCustomerRequest.CountryCode = oSPKDetailCust.CountryCode

                    '

                    Dim NoKTP As String, NoTelp As String

                    CommonFunction.GetKTPAndPhone(oSPKDetailCust, NoKTP, NoTelp)
                    If NoKTP = String.Empty OrElse NoKTP Is Nothing Then
                        Msg += "SPK " + oSPKDetailCust.SPKDetail.SPKHeader.SPKNumber + ": Konsumen " + oSPKDetailCust.Name1 + "No KTP tidak ada. \n"
                        Continue For
                    End If

                    Dim arrProfiles As ArrayList = oSPKDetailCust.SPKDetailCustomerProfiles

                    Dim iReturn As Integer = New CustomerRequestFacade(User).InsertFromSPKCustomer(objCustomerRequest, arrProfiles, pForSPKDetailCustomer:=True)

                    If iReturn > 0 Then
                        Dim objSPKHeaderFacade As New SPKHeaderFacade(User)
                        Dim oSPKDetailCustFac As New SPKDetailCustomerFacade(User)
                        objCustomerRequest = New CustomerRequest
                        objCustomerRequest = objCustomerRequestFacade.Retrieve(iReturn)

                        oSPKDetailCust.CustomerRequest = objCustomerRequest
                        oSPKDetailCustFac.Update(oSPKDetailCust)

                        Dim oCustReq As CustomerRequest = New CustomerRequest
                        oCustReq = UpdateStatusCustomerRequest(objCustomerRequest, AutoCustomerStatus, Msg)
                        If Not IsNothing(oCustReq) Then
                            arlCustRequest.Add(oCustReq)
                        End If
                    End If
                Next
                Return arlCustRequest
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Function

        Public Function UpdateStatusCustomerRequest(ByVal objCustomerRequest As CustomerRequest, ByVal AutoCustomerStatus As Boolean, ByRef Msg As String) As CustomerRequest
            'Dim arlCustRequest As ArrayList = New ArrayList
            'Cek status. Jika status <> Proses & autoCustomer = Aktif
            If objCustomerRequest.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Proses And AutoCustomerStatus = True Then
                Msg = "Data telah ditransfer"
                Return Nothing
            Else
                Dim _oldStatus = objCustomerRequest.Status
                objCustomerRequest.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Validasi
                Dim nResult As Integer = New CustomerRequestFacade(User).Update(objCustomerRequest)
                If nResult <> -1 Then
                    objCustomerRequest = New CustomerRequestFacade(User).Retrieve(nResult)

                    Return objCustomerRequest
                End If
            End If

        End Function

        Public Sub TransferProcess(ByVal arlCustRequest As ArrayList, ByVal objUser As UserInfo, ByRef Msg As String)
            Dim i As Integer = 0
            Dim arl As ArrayList = New ArrayList
            'Dim objUser As UserInfo = CType(SessionHelper.GetSession("LOGINUSERINFO"), UserInfo)
            Dim success As Boolean = False

            Dim IsCheck As Boolean = False
            Dim sw As StreamWriter
            Dim sb As New StringBuilder
            Dim filename = String.Format("{0}{1}{2}", "cusreq", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")
            Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\FinishUnit\" & filename  '-- Destination file
            Dim LocalDest As String = Server.MapPath("") & "\..\DataTemp\" & filename
            Dim tmp As Integer = 0
            Dim NoKTP As String, NoTelp As String
            Dim _oldStatus = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Baru

            For Each CR As CustomerRequest In arlCustRequest
                If CR.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Validasi Then
                    IsCheck = True
                    CR.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Proses
                    CR.ProcessUserID = objUser.ID

                    Dim ObjCity As New City
                    ObjCity = New General.CityFacade(User).Retrieve(CR.CityID)
                    Dim preRegion As String
                    If CR.PrintRegion = "0" Then
                        preRegion = "X"
                    Else
                        preRegion = ""
                    End If

                    'handle sementara untuk prearea
                    If CR.PreArea.ToLower = "blank" Then
                        CR.PreArea = ""
                    End If

                    'Untuk preArea dan kota dipisahkan dengan spasi dan bukan dengan Delimiter chr(13) (Enter)
                    'Konfirmasi dari Heru
                    'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(13) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & Chr(10))
                    'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion )
                    CommonFunction.GetKTPAndPhone(CR, NoKTP, NoTelp) 'CR:for:Rina;by:dna:on:20110323
                    'cr spk
                    If CR.Status1 > 0 Then
                        'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KTP" & Chr(9) & NoKTP & Chr(9) & NoTelp)
                        If CR.TypeIdentitas < 5 Then
                            If (NoKTP.Trim <> "") Then
                                If arl.Count > 0 Then sb.Append(vbNewLine)
                                arl.Add(CR)
                                sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "TDP" & Chr(9) & NoKTP)
                            End If
                        ElseIf CR.TypeIdentitas = 5 Then
                            If (NoKTP.Trim <> "") Then
                                If arl.Count > 0 Then sb.Append(vbNewLine)
                                arl.Add(CR)
                                sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "TDY" & Chr(9) & NoKTP)
                            End If
                        Else
                            If (NoKTP.Trim <> "") Then
                                If arl.Count > 0 Then sb.Append(vbNewLine)
                                arl.Add(CR)
                                sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "SIK" & Chr(9) & NoKTP)
                            End If

                        End If

                    Else
                        'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KTP" & Chr(9) & NoKTP & Chr(9) & NoTelp)
                        If CR.TypeIdentitas = 0 Then 'KTP
                            If (NoKTP.Trim <> "") Then
                                If arl.Count > 0 Then sb.Append(vbNewLine)
                                arl.Add(CR)
                                sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KTP" & Chr(9) & NoKTP)
                            End If
                        ElseIf CR.TypeIdentitas = 1 Then 'SIM
                            If (NoKTP.Trim <> "") Then
                                If arl.Count > 0 Then sb.Append(vbNewLine)
                                arl.Add(CR)
                                sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "SIM" & Chr(9) & NoKTP)
                            End If
                        ElseIf CR.TypeIdentitas = 2 Then 'KITAS
                            If (NoKTP.Trim <> "") Then
                                If arl.Count > 0 Then sb.Append(vbNewLine)
                                arl.Add(CR)
                                sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KITAS" & Chr(9) & NoKTP)
                            End If
                        Else
                            If (NoKTP.Trim <> "") Then 'KITAP
                                If arl.Count > 0 Then sb.Append(vbNewLine)
                                arl.Add(CR)
                                sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KITAP" & Chr(9) & NoKTP)
                            End If
                        End If

                    End If
                    '



                End If
            Next

            If IsCheck Then
                If (sb.Length > 0) Then
                    If Transfer(DestFile, sb.ToString(), arl) Then         '>> Code utk upload data lsg ke SAP Folder

                        For Each CR As CustomerRequest In arlCustRequest
                            Dim objCustomerRequest = New CustomerRequestFacade(User).Retrieve(CR.ID)

                            'Insert To CustomerStatusHistory status by proses
                            Dim custHistory As New CustomerStatusHistory
                            custHistory.CustomerRequest = objCustomerRequest
                            custHistory.OldStatus = _oldStatus
                            custHistory.NewStatus = objCustomerRequest.Status
                            custHistory.RowStatus = 0
                            Dim _custHistFacade2 As New CustomerStatusHistoryFacade(User)
                            _custHistFacade2.Insert(custHistory)
                        Next

                        Msg = "Data berhasil di upload ke SAP"
                    Else
                        Msg = "Upload data to SAP gagal"
                    End If
                End If
            End If
        End Sub

        Public Function Transfer(ByVal DestFile As String, ByVal Val As String, ByVal arl As ArrayList) As Boolean
            Dim success As Boolean = False
            Dim sw As StreamWriter
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            Dim finfo As New FileInfo(DestFile)
            Try
                success = imp.Start()
                If success Then
                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If

                    If (New Service.CustomerRequestFacade(User).UpdateTransaction(arl) <> -1) Then
                        sw = New StreamWriter(DestFile)
                        sw.Write(Val)
                        sw.Close()
                    Else
                        success = False
                    End If
                End If
            Catch ex As Exception
                sw.Close()
                Throw ex
                Return success
            End Try
            Return success
        End Function
#End Region

    End Class
End Namespace