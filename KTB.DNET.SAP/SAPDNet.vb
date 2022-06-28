Imports SAP.Middleware.Connector
Imports KTB.DNET.Domain
Imports KTB.DNET.BusinessFacade.FinishUnit
Imports System.Linq

Public Class SAPDNet

    Private connectionName As String
    Public Sub New(ByVal SAPconnectionName As String)
        connectionName = SAPconnectionName
    End Sub
    Public Sub New(ByVal connString As String, UserName As String, Passsword As String)
        connectionName = String.Format(connString, UserName, Passsword)
    End Sub

    Public Function GetMaterialToDataTable(ByVal arrSparepartMaster As List(Of SparePartMaster)) As DataTable

        Try

            'Dim SapRfcDestination As RfcDestination = RfcDestinationManager.GetDestination(connectionName)
            Dim conn As SAPSystemConnect = New SAPSystemConnect()
            Dim SapRfcDestination As RfcDestination = RfcDestinationManager.GetDestination(conn.GetParameters(connectionName))

            'RfcSessionManager.BeginContext(SapRfcDestination)

            Dim SapRfcRepository As RfcRepository = SapRfcDestination.Repository
            Dim bapiGetMaterialList = SapRfcRepository.CreateFunction("ZRFC_CHECK_MATERIAL")

            Dim mainPart As IRfcTable = bapiGetMaterialList.GetTable("MAIN_PART")
            Dim subtitutionPart As IRfcTable = bapiGetMaterialList.GetTable("SUBSTITUTION_PART")

            For i As Integer = 0 To arrSparepartMaster.Count - 1
                Dim sp As SparePartMaster = arrSparepartMaster(i)
                Dim mainStructure As IRfcStructure = mainPart.Metadata.LineType.CreateStructure()
                mainStructure.SetValue("MATNR", sp.PartNumber)
                mainStructure.SetValue("RQQTY", sp.MaxStock)
                mainPart.Append(mainStructure)
            Next

            bapiGetMaterialList.SetValue("MAIN_PART", mainPart)
            bapiGetMaterialList.SetValue("SUBSTITUTION_PART", subtitutionPart)

            bapiGetMaterialList.Invoke(SapRfcDestination)

            Dim tableReturn As IRfcTable = bapiGetMaterialList.GetTable("MAIN_PART")
            Dim subtitutionPartReturn As IRfcTable = bapiGetMaterialList.GetTable("SUBSTITUTION_PART")

            Return GetDataTableFromRFCTable(tableReturn)
        Catch ex As RfcBaseException
            Throw New Exception("RFC Error " + ex.Message)
        Finally
            'RfcSessionManager.EndContext(SapRfcDestination)
        End Try

    End Function

    Public Function GetMaterialPriceToDataTable(ByVal arrEstimationEquipDetail As List(Of ZKTB_INQ_IN)) As DataTable
        Try
            'Dim SapRfcDestination As RfcDestination = RfcDestinationManager.GetDestination(connectionName)
            Dim conn As SAPSystemConnect = New SAPSystemConnect()
            Dim SapRfcDestination As RfcDestination = RfcDestinationManager.GetDestination(conn.GetParameters(connectionName))

            Dim SapRfcRepository As RfcRepository = SapRfcDestination.Repository
            Dim bapiGetMaterialPriceList = SapRfcRepository.CreateFunction("ZKTB_DNET_INQUIRY")

            Dim inqTable As IRfcTable = bapiGetMaterialPriceList.GetTable("ORDER_ITEM_IN")
            'Dim subtitutionPart As IRfcTable = bapiGetMaterialList.GetTable("ORDER_ITEM_OUT")

            For i As Integer = 0 To arrEstimationEquipDetail.Count - 1
                Dim objIN As ZKTB_INQ_IN = arrEstimationEquipDetail(i)
                Dim objStruct As IRfcStructure = inqTable.Metadata.LineType.CreateStructure()
                objStruct.SetValue("CUSTOMER", objIN.CUSTOMER)
                objStruct.SetValue("MATERIAL", objIN.MATERIAL)
                inqTable.Append(objStruct)
            Next

            bapiGetMaterialPriceList.SetValue("ORDER_ITEM_IN", inqTable)
            'bapiGetMaterialList.SetValue("ORDER_ITEM_OUT", subtitutionPart)

            bapiGetMaterialPriceList.Invoke(SapRfcDestination)
            'Dim inReturn As IRfcTable = bapiGetMaterialPriceList.GetTable("ORDER_ITEM_IN")
            Dim outReturn As IRfcTable = bapiGetMaterialPriceList.GetTable("ORDER_ITEM_OUT")

            Return GetDataTableFromRFCTable(outReturn)

        Catch ex As RfcBaseException
            Throw New Exception("RFC Error " + ex.Message)
        End Try
    End Function

    Public Function GetCreditControlToDataTable(ByVal arrCreditControl As List(Of SAPCreditCeiling)) As DataTable
        Try
            'Dim SapRfcDestination As RfcDestination = RfcDestinationManager.GetDestination(connectionName)
            Dim conn As SAPSystemConnect = New SAPSystemConnect()
            Dim SapRfcDestination As RfcDestination = RfcDestinationManager.GetDestination(conn.GetParameters(connectionName))

            Dim SapRfcRepository As RfcRepository = SapRfcDestination.Repository
            Dim bapiGetCrediCeilingList = SapRfcRepository.CreateFunction("ZRFC_CREDIT_CEILING")

            Dim mainPart As IRfcTable = bapiGetCrediCeilingList.GetTable("CR_CEILING")

            For i As Integer = 0 To arrCreditControl.Count - 1
                Dim cc As SAPCreditCeiling = arrCreditControl(i)
                Dim mainStructure As IRfcStructure = mainPart.Metadata.LineType.CreateStructure()
                mainStructure.SetValue("KNKLI", cc.DealerCode.ToString)
                mainStructure.SetValue("SPLNM", cc.SPLNumber.ToString)
                mainStructure.SetValue("KLYEAR", cc.PeriodYear.ToString)
                mainStructure.SetValue("KLMONTH", cc.PeriodMonth.ToString)
                mainPart.Append(mainStructure)
            Next

            bapiGetCrediCeilingList.SetValue("CR_CEILING", mainPart)

            bapiGetCrediCeilingList.Invoke(SapRfcDestination)
            Dim tableReturn As IRfcTable = bapiGetCrediCeilingList.GetTable("CR_CEILING")

            Return GetCreditControlToDataTable(tableReturn)

        Catch ex As Exception
            Throw New Exception("RFC Error " + ex.Message)
        Finally

        End Try
    End Function

    Public Function GetMaterial(ByVal arrSparepartMaster As ArrayList, ByRef arrMainPart As ArrayList, ByRef arrSubtitutionPart As ArrayList) As String

        Try
            'Dim SapRfcDestination As RfcDestination = RfcDestinationManager.GetDestination(connectionName)
            Dim conn As SAPSystemConnect = New SAPSystemConnect()
            Dim SapRfcDestination As RfcDestination = RfcDestinationManager.GetDestination(conn.GetParameters(connectionName))

            Dim SapRfcRepository As RfcRepository = SapRfcDestination.Repository
            Dim bapiGetMaterialList = SapRfcRepository.CreateFunction("ZRFC_CHECK_MATERIAL")

            Dim mainPart As IRfcTable = bapiGetMaterialList.GetTable("MAIN_PART")
            Dim subtitutionPart As IRfcTable = bapiGetMaterialList.GetTable("SUBSTITUTION_PART")

            'start test payment transfer
            'Dim connPT As SAPSystemConnect = New SAPSystemConnect()
            'Dim SapRfcDestinationPT As RfcDestination = RfcDestinationManager.GetDestination(conn.GetParameters(connectionName))

            'Dim SapRfcRepositoryPT As RfcRepository = SapRfcDestinationPT.Repository
            'Dim bapiGetMaterialListPT = SapRfcRepositoryPT.CreateFunction("ZFUFM_CREATE_SO_DEALER_DNET")

            'Dim mainPartPT As IRfcTable = bapiGetMaterialListPT.GetTable("ZFUST0075")
            'Dim piye As String = ""

            'end  test payment transfer



            For i As Integer = 0 To arrSparepartMaster.Count - 1
                Dim sp As SparePartMaster = arrSparepartMaster(i)
                Dim mainStructure As IRfcStructure = mainPart.Metadata.LineType.CreateStructure()
                mainStructure.SetValue("MATNR", sp.PartNumber)
                mainStructure.SetValue("RQQTY", sp.MaxStock)
                mainPart.Append(mainStructure)
            Next

            bapiGetMaterialList.SetValue("MAIN_PART", mainPart)
            bapiGetMaterialList.SetValue("SUBSTITUTION_PART", subtitutionPart)

            bapiGetMaterialList.Invoke(SapRfcDestination)
            Dim mainPartReturn As IRfcTable = bapiGetMaterialList.GetTable("MAIN_PART")
            Dim subtitutionPartReturn As IRfcTable = bapiGetMaterialList.GetTable("SUBSTITUTION_PART")

            For Each row As IRfcStructure In mainPartReturn
                Dim z As ZSPST0028_01 = New ZSPST0028_01()
                z.MATNR = row.GetValue("MATNR")
                z.RQQTY = row.GetInt("RQQTY")
                z.MAKTX = row.GetValue("MAKTX")
                z.PCODE = row.GetValue("PCODE")
                z.MATKL = row.GetValue("MATKL")
                z.RTLPR = row.GetDecimal("RTLPR")
                z.STOCK = row.GetValue("STOCK")
                z.NORMT = row.GetValue("NORMT")
                z.SUBNR = row.GetValue("SUBNR")
                z.MESSG = row.GetValue("MESSG")

                arrMainPart.Add(z)
            Next

            For Each row As IRfcStructure In subtitutionPartReturn
                Dim z As ZSPST0028_02 = New ZSPST0028_02()
                z.Matnr1 = row.GetValue("MATNR1")
                z.Matnr2 = row.GetValue("MATNR2")
                z.Maktx = row.GetValue("MAKTX")
                z.Matkl = row.GetValue("MATKL")
                z.Rtlpr = row.GetDecimal("RTLPR")
                z.Stock = row.GetValue("STOCK")
                z.Normt = row.GetValue("NORMT")

                arrSubtitutionPart.Add(z)
            Next

            Return String.Empty
        Catch ex As RfcBaseException
            Return ("RFC Error " + ex.Message)
        Finally

        End Try


    End Function
    Public Function GetMaterialPrice(ByVal arrEstimationEquipDetail As ArrayList) As ArrayList
        Dim arrReturn As New ArrayList
        Try
            'Dim SapRfcDestination As RfcDestination = RfcDestinationManager.GetDestination(connectionName)
            Dim conn As SAPSystemConnect = New SAPSystemConnect()
            Dim SapRfcDestination As RfcDestination = RfcDestinationManager.GetDestination(conn.GetParameters(connectionName))

            Dim SapRfcRepository As RfcRepository = SapRfcDestination.Repository
            Dim bapiGetMaterialPriceList = SapRfcRepository.CreateFunction("ZKTB_DNET_INQUIRY")


            Dim inqTable As IRfcTable = bapiGetMaterialPriceList.GetTable("ORDER_ITEM_IN")
            'Dim subtitutionPart As IRfcTable = bapiGetMaterialList.GetTable("ORDER_ITEM_OUT")

            For i As Integer = 0 To arrEstimationEquipDetail.Count - 1
                Dim objIN As EstimationEquipDetail = arrEstimationEquipDetail(i)
                Dim objStruct As IRfcStructure = inqTable.Metadata.LineType.CreateStructure()
                objStruct.SetValue("CUSTOMER", objIN.EstimationEquipHeader.Dealer.DealerCode)
                objStruct.SetValue("MATERIAL", objIN.SparePartMaster.PartNumber)
                inqTable.Append(objStruct)
            Next

            bapiGetMaterialPriceList.SetValue("ORDER_ITEM_IN", inqTable)
            'bapiGetMaterialList.SetValue("ORDER_ITEM_OUT", subtitutionPart)

            bapiGetMaterialPriceList.Invoke(SapRfcDestination)
            'Dim inReturn As IRfcTable = bapiGetMaterialPriceList.GetTable("ORDER_ITEM_IN")
            Dim outReturn As IRfcTable = bapiGetMaterialPriceList.GetTable("ORDER_ITEM_OUT")

            For Each row As IRfcStructure In outReturn
                Dim objOut As ZKTB_INQ_OUT = New ZKTB_INQ_OUT()
                objOut.Customer = row.GetValue("CUSTOMER")
                objOut.Material = row.GetValue("MATERIAL")
                objOut.Retail_Price = row.GetValue("RETAIL_PRICE")
                objOut.Discount = row.GetValue("DISCOUNT")
                objOut.Currency = row.GetValue("CURRENCY")

                arrReturn.Add(objOut)
            Next

            Return arrReturn

        Catch ex As RfcBaseException
            Throw New Exception("RFC Error " + ex.Message)
        End Try
    End Function

    Public Function GetMaterialPrice(ByVal arrEstimationEquipDetail As List(Of ZKTB_INQ_IN)) As ArrayList
        Dim arrReturn As New ArrayList
        Try
            'Dim SapRfcDestination As RfcDestination = RfcDestinationManager.GetDestination(connectionName)
            Dim conn As SAPSystemConnect = New SAPSystemConnect()
            Dim SapRfcDestination As RfcDestination = RfcDestinationManager.GetDestination(conn.GetParameters(connectionName))

            Dim SapRfcRepository As RfcRepository = SapRfcDestination.Repository
            Dim bapiGetMaterialPriceList = SapRfcRepository.CreateFunction("ZKTB_DNET_INQUIRY")

            Dim inqTable As IRfcTable = bapiGetMaterialPriceList.GetTable("ORDER_ITEM_IN")
            'Dim subtitutionPart As IRfcTable = bapiGetMaterialList.GetTable("ORDER_ITEM_OUT")

            For i As Integer = 0 To arrEstimationEquipDetail.Count - 1
                Dim objIN As ZKTB_INQ_IN = arrEstimationEquipDetail(i)
                Dim objStruct As IRfcStructure = inqTable.Metadata.LineType.CreateStructure()
                objStruct.SetValue("CUSTOMER", objIN.CUSTOMER)
                objStruct.SetValue("MATERIAL", objIN.MATERIAL)
                inqTable.Append(objStruct)
            Next

            bapiGetMaterialPriceList.SetValue("ORDER_ITEM_IN", inqTable)
            'bapiGetMaterialList.SetValue("ORDER_ITEM_OUT", subtitutionPart)

            bapiGetMaterialPriceList.Invoke(SapRfcDestination)
            'Dim inReturn As IRfcTable = bapiGetMaterialPriceList.GetTable("ORDER_ITEM_IN")
            Dim outReturn As IRfcTable = bapiGetMaterialPriceList.GetTable("ORDER_ITEM_OUT")

            For Each row As IRfcStructure In outReturn
                Dim objOut As ZKTB_INQ_OUT = New ZKTB_INQ_OUT()
                objOut.Customer = row.GetValue("CUSTOMER")
                objOut.Material = row.GetValue("MATERIAL")
                objOut.Retail_Price = row.GetValue("RETAIL_PRICE")
                objOut.Discount = row.GetValue("DISCOUNT")
                objOut.Currency = row.GetValue("CURRENCY")

                arrReturn.Add(objOut)
            Next

            Return arrReturn

        Catch ex As RfcBaseException
            Throw New Exception("RFC Error " + ex.Message)
        End Try
    End Function

    Public Function SendBillsViaRFC(ByVal iBill As TrBillingHeader, ByVal descTujuan As String, ByRef DNNumber As String, ByRef Message As String) As Boolean
        Dim arrReturn As New ArrayList
        Try
            Dim conn As SAPSystemConnect = New SAPSystemConnect()
            Dim sapDest As RfcDestination = RfcDestinationManager.GetDestination(conn.GetParameters(connectionName))
            Dim amont As String = iBill.Total.ToString().Split(",")(0)
            Dim sapRepo As RfcRepository = sapDest.Repository
            Dim bapiFunc = sapRepo.CreateFunction("ZFM_PARK_TRAINING_DN")
            bapiFunc.SetValue("ID_BUDAT", iBill.PostedDate.ToString("yyyyMMdd"))
            bapiFunc.SetValue("ID_DEALR", iBill.DealerCode)
            bapiFunc.SetValue("ID_AMOUN", amont)
            bapiFunc.SetValue("ID_DEPT", "ZG03")
            bapiFunc.SetValue("ID_KOSTL", "CC1000000")
            bapiFunc.SetValue("ID_ORDER", "ZG03_42_0002")
            bapiFunc.SetValue("ID_BMODE", " ")
            bapiFunc.SetValue("ID_SGTXT", descTujuan)

            bapiFunc.Invoke(sapDest)
            DNNumber = bapiFunc.GetValue("ED_ACDOC")
            Message = bapiFunc.GetValue("ED_MESSG")

            Return True
        Catch ex As Exception
            Message = ex.Message
            Return False
        End Try
    End Function

    Public Function SendPOViaRFC(ByVal oPOH As POHeader, ByRef SONumber As String, ByRef Message As String, ByVal isTransControlPO As Boolean, ByVal arrPKDetail As ArrayList)
        'Public Function SendPOViaRFC(ByVal oPOH As POHeader, ByRef SONumber As String, ByRef Message As String)

        Dim arrReturn As New ArrayList
        Try
            'Dim SapRfcDestination As RfcDestination = RfcDestinationManager.GetDestination(connectionName)
            Dim conn As SAPSystemConnect = New SAPSystemConnect()
            Dim sapDest As RfcDestination = RfcDestinationManager.GetDestination(conn.GetParameters(connectionName))

            Dim sapRepo As RfcRepository = sapDest.Repository
            Dim bapiFunc = sapRepo.CreateFunction("ZFUFM_CREATE_SO_DEALER_DNET")
            'Dim sapStrPOD As IRfcStructure = bapiFunc.GetStructure("ZFUST0075")
            Dim sapTabPOD As IRfcTable ' = bapiFunc.GetTable("IT_CITM")

            bapiFunc.SetValue("ID_CTRNO", oPOH.ContractHeader.ContractNumber)
            If oPOH.TermOfPayment.TermOfPaymentCode.ToUpper = "RTGS" Then
                bapiFunc.SetValue("ID_ZTERM", "Z000")
            Else
                bapiFunc.SetValue("ID_ZTERM", oPOH.TermOfPayment.TermOfPaymentCode)
            End If
            If oPOH.TermOfPayment.TermOfPaymentCode.ToUpper = "RTGS" Then
                bapiFunc.SetValue("ID_ZLSCH", "T")
            Else
                bapiFunc.SetValue("ID_ZLSCH", "")
            End If
            bapiFunc.SetValue("ID_VDATU", oPOH.ReqAllocationDateTime.ToString("yyyyMMdd"))
            bapiFunc.SetValue("ID_BSTDK", oPOH.CreatedTime.ToString("yyyyMMdd"))
            bapiFunc.SetValue("ID_PO1", oPOH.DealerPONumber)
            bapiFunc.SetValue("ID_PO2", oPOH.PONumber)
            bapiFunc.SetValue("ID_NOPPH", IIf(oPOH.FreePPh22Indicator.ToString = "0", "x", ""))
            If Not IsNothing(oPOH.PODestination) Then
                bapiFunc.SetValue("ID_DESTCODE", oPOH.PODestination.Code)
            Else
                bapiFunc.SetValue("ID_DESTCODE", oPOH.Dealer.DealerCode)
            End If

            If oPOH.POType = CInt(LookUp.EnumJenisOrder.Tambahan) Then
                bapiFunc.SetValue("ID_ABRVW", "ADD")
            Else
                bapiFunc.SetValue("ID_ABRVW", "")
            End If

            sapTabPOD = bapiFunc.GetTable("IT_CITM")

            For Each oPOD As PODetail In oPOH.PODetails
                If oPOD.AllocQty > 0 Then
                    Dim FreeDays As Integer = 0
                    Dim sapRow As IRfcStructure = sapTabPOD.Metadata.LineType.CreateStructure()
                    sapRow.SetValue("ITEM", oPOD.ContractDetail.LineItem)
                    sapRow.SetValue("QTY", oPOD.AllocQty)
                    If isTransControlPO Then
                        FreeDays = oPOD.FreeDays
                    Else
                        For Each oPK As PKDetail In arrPKDetail
                            If oPK.VechileColor.ID = oPOD.ContractDetail.VechileColor.ID Then
                                FreeDays = oPK.FreeDays
                            End If
                        Next
                    End If
                    sapRow.SetValue("FDAYS", FreeDays)
                    sapTabPOD.Append(sapRow)
                End If

            Next
            bapiFunc.SetValue("IT_CITM", sapTabPOD)

            bapiFunc.Invoke(sapDest)
            SONumber = bapiFunc.GetValue("ED_VBELN")
            Message = bapiFunc.GetValue("ED_MESSG")

            Return True
        Catch ex As RfcBaseException
            Throw New Exception("RFC Error " + ex.Message)
            Return False
        End Try
    End Function

    Private Function ConvertDate(ByVal dt As Date, Optional ByVal digit As Integer = 0) As String
        Dim strDate As New Text.StringBuilder
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

    Public Function SendPKViaRFC(ByVal pk As PKHeader, ByRef Message As String, ByVal isDetailAllZZZZ As Boolean, _
                                 ByVal objSPL As SPL, ByVal vechileColorID As Integer, ByVal discounts As ArrayList, ByRef result As ArrayList, ByVal sapDiscountCode As ArrayList)
        Try
            Dim newline1 As String = Chr(10)
            Dim newline2 As String = Chr(13)
            Dim conn As SAPSystemConnect = New SAPSystemConnect()
            Dim sapDest As RfcDestination = RfcDestinationManager.GetDestination(conn.GetParameters(connectionName))

            Dim sapRepo As RfcRepository = sapDest.Repository
            Dim bapiFunc = sapRepo.CreateFunction("ZFUFM_DNET_PK")
            'Dim sapStrPOD As IRfcStructure = bapiFunc.GetStructure("ZFUST0075")
            Dim sapTabPKD As IRfcTable

            Dim headStruct As IRfcStructure = bapiFunc.GetStructure("IX_HEAD")

            If Not isDetailAllZZZZ Then
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

                headStruct.SetValue("BSARK", pk.Category.CategoryCode)
                If pk.OrderType = LookUp.EnumJenisPesanan.Bulanan Then
                    headStruct.SetValue("AUART", "ZA21")
                Else
                    headStruct.SetValue("AUART", "ZA22")
                End If
                headStruct.SetValue("KUNNR", pk.Dealer.DealerCode)
                headStruct.SetValue("PKDAT", ConvertDate(pk.PKDate))
                headStruct.SetValue("PKDLR", pk.DealerPKNumber)
                headStruct.SetValue("ORDPR", DateSerial(pk.RequestPeriodeYear, pk.RequestPeriodeMonth, pk.RequestPeriodeDay).ToString("MMyyyy"))
                headStruct.SetValue("PRCPR", DateSerial(pk.PricingPeriodeYear, pk.PricingPeriodeMonth, pk.PricingPeriodeDay).ToString("ddMMyyyy"))
                headStruct.SetValue("PKNUM", pk.PKNumber)
                headStruct.SetValue("PRDYR", pk.ProductionYear)
                If Not objSPL Is Nothing Then
                    headStruct.SetValue("SPLIN", pk.FreeIntIndicator)
                Else
                    headStruct.SetValue("SPLIN", 1)
                End If
                headStruct.SetValue("SPLNO", pk.SPLNumber)
                headStruct.SetValue("PRJNM", pk.ProjectName)
                headStruct.SetValue("DESCR", pk.Description.Replace(newline1, " ").Replace(newline2, " ").ToString)
                If Not IsNothing(pk.DealerBranch) Then
                    headStruct.SetValue("BRNCH", pk.DealerBranch.DealerBranchCode)
                Else
                    headStruct.SetValue("BRNCH", "")
                End If

                sapTabPKD = bapiFunc.GetTable("IT_ITEM")

                For i As Integer = 0 To pk.PKDetails.Count - 1
                    Dim item As PKDetail = pk.PKDetails(i)
                    If item.VechileColor.ID <> vechileColorID Then
                        Dim discount As ArrayList = discounts(i)
                        Dim sapRow As IRfcStructure = sapTabPKD.Metadata.LineType.CreateStructure()
                        sapRow.SetValue("MATNR", item.VechileColor.HeaderBOM)
                        sapRow.SetValue("TARGT", item.TargetQty)
                        sapRow.SetValue("RESPN", item.ResponseQty)
                        sapRow.SetValue("RSDIS", Convert.ToInt64(item.ResponseDiscount))
                        sapRow.SetValue("RSSUR", Convert.ToInt64(item.ResponseSalesSurcharge))
                        sapRow.SetValue("FREDY", item.FreeDays)
                        'sapRow.SetValue("D_FLEET", discount(0))
                        'sapRow.SetValue("D_ASSY", discount(1))
                        'sapRow.SetValue("D_DCGLS", discount(2))
                        'sapRow.SetValue("D_ULTI", discount(3))
                        'sapRow.SetValue("D_SCHDX", discount(4))
                        'sapRow.SetValue("D_WS", discount(5))
                        'sapRow.SetValue("D_EXPRO", discount(6))
                        'sapRow.SetValue("D_SPC", discount(7))
                        'sapRow.SetValue("D_PRCDIFF", discount(8))
                        'sapRow.SetValue("D_ALPHA", discount(9))
                        For j As Integer = 0 To sapDiscountCode.Count - 1
                            sapRow.SetValue(sapDiscountCode(j).ToString(), discount(j))
                        Next
                        sapTabPKD.Append(sapRow)
                    End If
                Next

                bapiFunc.SetValue("IT_ITEM", sapTabPKD)
                bapiFunc.Invoke(sapDest)

                Message = bapiFunc.GetValue("ED_MESSG")
                Dim inquiryNo As String = bapiFunc.GetValue("ED_INQNO")
                Dim contractNo As String = bapiFunc.GetValue("ED_CTRNO")
                Dim materialNo As ArrayList = New ArrayList()
                Dim lineNo As ArrayList = New ArrayList()

                Dim cItemTable As IRfcTable = bapiFunc.GetTable("ET_CITEM")
                Dim cItemRow As IRfcStructure = cItemTable.Metadata.LineType.CreateStructure()
                For Each row As IRfcStructure In cItemTable
                    Dim tempMaterialNo As String = row.GetValue("MATNR")
                    Dim tempLineNo As Integer = row.GetValue("POSNR")
                    materialNo.Add(tempMaterialNo)
                    lineNo.Add(tempLineNo)
                Next

                result.Add(inquiryNo)
                result.Add(contractNo)
                result.Add(materialNo)
                result.Add(lineNo)

                Return True
            End If
        Catch ex As Exception

            Throw New Exception("RFC Error " + ex.Message)
            Return False
        End Try

    End Function

    Public Function CheckConnection() As Boolean
        Dim bool As Boolean = False
        Dim conn As SAPSystemConnect = New SAPSystemConnect()
        Dim sapDest As RfcDestination = RfcDestinationManager.GetDestination(conn.GetParameters(connectionName))
        Try
            Dim sapRepo As RfcRepository = sapDest.Repository
            bool = True
        Catch ex As Exception
            bool = False
        End Try

        Return bool
    End Function

    Public Function SendMSPRegistrationViaRFC(ByVal objMspRegistrationHistory As MSPRegistrationHistory, ByVal index As Integer, ByRef SONumber As String, ByRef Message As String)
        Dim arrReturn As New ArrayList
        Try
            Dim conn As SAPSystemConnect = New SAPSystemConnect()
            Dim sapDest As RfcDestination = RfcDestinationManager.GetDestination(conn.GetParameters(connectionName))

            Dim sapRepo As RfcRepository = sapDest.Repository
            Dim bapiFunc = sapRepo.CreateFunction("ZFUFM_CREATE_SO_SMART_PACKAGE")
            Dim sapTab As IRfcTable


            sapTab = bapiFunc.GetTable("IT_MSP")

            Dim sapRow As IRfcStructure = sapTab.Metadata.LineType.CreateStructure()
            sapRow.SetValue("IDX", index)
            sapRow.SetValue("MSPNO", objMspRegistrationHistory.MSPRegistration.MSPCode)
            sapRow.SetValue("EQUNR", objMspRegistrationHistory.MSPRegistration.ChassisMaster.ChassisNumber)
            sapRow.SetValue("PROCS", CType(objMspRegistrationHistory.RequestType, EnumStatusMSP.StatusType).ToString) ' baru / upgrade
            sapRow.SetValue("PROCT", If(objMspRegistrationHistory.BenefitMasterHeaderID = 0, "Paid", "Promo")) 'paid / promo

            Dim refCustomerCode = String.Empty
            If Not IsNothing(objMspRegistrationHistory.MSPRegistration.MSPCustomer.RefCustomer) Then
                refCustomerCode = objMspRegistrationHistory.MSPRegistration.MSPCustomer.RefCustomer.Code
            End If
            sapRow.SetValue("KUNNR", refCustomerCode)
            sapRow.SetValue("NAME", objMspRegistrationHistory.MSPRegistration.MSPCustomer.Name1)
            sapRow.SetValue("AGE", objMspRegistrationHistory.MSPRegistration.MSPCustomer.Age)
            sapRow.SetValue("STREET", objMspRegistrationHistory.MSPRegistration.MSPCustomer.Alamat)
            sapRow.SetValue("TEL_NUMBER", objMspRegistrationHistory.MSPRegistration.MSPCustomer.PhoneNo)
            sapRow.SetValue("KTP_NO", objMspRegistrationHistory.MSPRegistration.MSPCustomer.KTPNo)
            sapRow.SetValue("KELURAHAN", objMspRegistrationHistory.MSPRegistration.MSPCustomer.Kelurahan)
            sapRow.SetValue("KECAMATAN", objMspRegistrationHistory.MSPRegistration.MSPCustomer.Kecamatan)
            If Not IsNothing(objMspRegistrationHistory.MSPRegistration.MSPCustomer.Province) AndAlso objMspRegistrationHistory.MSPRegistration.MSPCustomer.Province.ID > 0 Then
                sapRow.SetValue("PROVINSI", objMspRegistrationHistory.MSPRegistration.MSPCustomer.Province.ProvinceName)
            Else
                sapRow.SetValue("PROVINSI", String.Empty)
            End If

            sapRow.SetValue("KABUPATEN", objMspRegistrationHistory.MSPRegistration.MSPCustomer.PreArea)
            sapRow.SetValue("EMAIL", objMspRegistrationHistory.MSPRegistration.MSPCustomer.Email)
            sapRow.SetValue("DEALER", objMspRegistrationHistory.MSPRegistration.Dealer.DealerCode)
            sapRow.SetValue("SPACK", objMspRegistrationHistory.MSPMaster.MSPType.Code)
            sapRow.SetValue("DURAT", objMspRegistrationHistory.MSPMaster.Duration)
            sapRow.SetValue("KMPM", objMspRegistrationHistory.MSPMaster.MSPKm)
            sapRow.SetValue("MODEL", objMspRegistrationHistory.MSPMaster.VehicleType.VechileTypeCode)
            sapRow.SetValue("VALFR", objMspRegistrationHistory.MSPRegistration.ChassisMaster.EndCustomer.OpenFakturDate.ToString("yyyyMMdd"))
            sapRow.SetValue("AMOUNT", objMspRegistrationHistory.SelisihAmount)
            sapRow.SetValue("VALTO", objMspRegistrationHistory.MSPRegistration.ChassisMaster.EndCustomer.OpenFakturDate.AddYears(objMspRegistrationHistory.MSPMaster.Duration).ToString("yyyyMMdd"))
            sapRow.SetValue("SALBY", objMspRegistrationHistory.SoldBy)
            sapRow.SetValue("REGDT", objMspRegistrationHistory.RegistrationDate.ToString("yyyyMMdd"))

            sapTab.Append(sapRow)

            bapiFunc.SetValue("IT_MSP", sapTab)

            'IT_RETMSP
            ' SMPNO, EQUNR /CHASSIS, VBELN/NO SO, STATUS
            bapiFunc.Invoke(sapDest)
            Message = bapiFunc.GetValue("ED_MESSG")

            ' if Paid then set SONumber
            If objMspRegistrationHistory.BenefitMasterHeaderID = 0 Then
                If String.IsNullOrEmpty(Message) Then
                    Dim strResult As String = bapiFunc.GetValue("IT_RETMSP").ToString().Split(" ")(10)
                    SONumber = strResult.Substring(strResult.Length - 10, 10)
                End If
            End If


            Return True
        Catch ex As RfcBaseException
            Throw New Exception("RFC Error " + ex.Message)
            Return False
        End Try
    End Function

    Public Function GetCreditControl(ByVal arrCreditControl As ArrayList) As ArrayList
        Dim arrReturn As New ArrayList
        Try
            'Dim SapRfcDestination As RfcDestination = RfcDestinationManager.GetDestination(connectionName)
            Dim conn As SAPSystemConnect = New SAPSystemConnect()
            Dim SapRfcDestination As RfcDestination = RfcDestinationManager.GetDestination(conn.GetParameters(connectionName))

            Dim SapRfcRepository As RfcRepository = SapRfcDestination.Repository
            Dim bapiGetCrediCeilingList = SapRfcRepository.CreateFunction("ZRFC_CREDIT_CEILING")

            Dim mainPart As IRfcTable = bapiGetCrediCeilingList.GetTable("CR_CEILING")

            For i As Integer = 0 To arrCreditControl.Count - 1
                Dim cc As SAPCreditCeiling = arrCreditControl(i)
                Dim mainStructure As IRfcStructure = mainPart.Metadata.LineType.CreateStructure()
                mainStructure.SetValue("KNKLI", cc.DealerCode.ToString)
                mainStructure.SetValue("SPLNM", cc.SPLNumber.ToString)
                mainStructure.SetValue("KLYEAR", cc.PeriodYear.ToString)
                mainStructure.SetValue("KLMONTH", cc.PeriodMonth.ToString)
                mainPart.Append(mainStructure)
            Next

            bapiGetCrediCeilingList.SetValue("CR_CEILING", mainPart)

            bapiGetCrediCeilingList.Invoke(SapRfcDestination)
            Dim tableReturn As IRfcTable = bapiGetCrediCeilingList.GetTable("CR_CEILING")

            For Each row As IRfcStructure In tableReturn
                Dim z As ZFUST0042 = New ZFUST0042()
                z.Knkli = row.GetValue("KNKLI")
                z.Splnm = row.GetValue("SPLNM")
                z.Klyear = row.GetValue("KLYEAR")
                z.Klmonth = row.GetValue("KLMONTH")
                z.Tmcod = row.GetValue("TMCOD")
                z.Tmtyp = row.GetValue("TMTYP")
                z.Tmknd = row.GetValue("TMKND")
                z.Klimk = row.GetValue("KLIMK")
                z.Blimk = row.GetValue("BLIMK")
                z.Klfrm = row.GetValue("KLFRM")
                z.Kldto = row.GetValue("KLDTO")
                z.Blkst = row.GetValue("BLKST")
                z.Bldat = row.GetValue("BLDAT")
                z.Blnam = row.GetValue("BLNAM")
                z.Mfdat = row.GetValue("MFDAT")
                z.Mftim = row.GetValue("MFTIM")
                z.Mfnam = row.GetValue("MFNAM")

                arrReturn.Add(z)
            Next

            Return arrReturn

        Catch ex As RfcBaseException
            Throw New Exception("RFC Error " + ex.Message)
        Finally

        End Try
        Return arrReturn
    End Function

    Private Function GetDataTableFromRFCTable(ByVal rfcTable As IRfcTable) As DataTable
        '//sapnco_util
        Dim dataTable As New DataTable
        Try
            '//... Create ADO.Net table.
            For liElement As Integer = 0 To rfcTable.ElementCount - 1
                Dim metadata As RfcElementMetadata = rfcTable.GetElementMetadata(liElement)
                dataTable.Columns.Add(metadata.Name)
            Next

            '//... Transfer rows from lrfcTable to ADO.Net table.
            Dim dataRow As DataRow
            For Each row As IRfcStructure In rfcTable
                dataRow = dataTable.NewRow
                For liElement As Integer = 0 To rfcTable.ElementCount - 1
                    Dim metadata As RfcElementMetadata = rfcTable.GetElementMetadata(liElement)
                    dataRow(liElement) = row.GetString(metadata.Name)
                Next
                dataTable.Rows.Add(dataRow)
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dataTable
    End Function

    Public Function SendFakturRevViaRFC(ByVal oRF As RevisionFaktur, ByRef SONumber As String, ByRef Message As String, ByRef objRevSAPDoc As RevisionSAPDoc)

        Dim arrReturn As New ArrayList
        Try
            'Dim SapRfcDestination As RfcDestination = RfcDestinationManager.GetDestination(connectionName)
            Dim conn As SAPSystemConnect = New SAPSystemConnect()
            Dim sapDest As RfcDestination = RfcDestinationManager.GetDestination(conn.GetParameters(connectionName))

            Dim sapRepo As RfcRepository = sapDest.Repository

            Dim bapiFunc = sapRepo.CreateFunction("ZFUFM_CREATE_SO_IR")
            Dim sapTab As IRfcTable

            sapTab = bapiFunc.GetTable("IT_IR")
            Dim sapRow As IRfcStructure = sapTab.Metadata.LineType.CreateStructure()

            sapRow.SetValue("IR_PENNO", oRF.RegNumber)
            sapRow.SetValue("IR_DEALER", oRF.ChassisMaster.Dealer.DealerCode)
            sapRow.SetValue("IR_PENDT", oRF.CreatedTime.ToString("yyyyMMdd"))
            sapRow.SetValue("IR_CATEG", oRF.ChassisMaster.Category.CategoryCode)
            sapRow.SetValue("IR_TYPE", oRF.ChassisMaster.VechileType)
            sapRow.SetValue("IR_EQUNR", oRF.ChassisMaster.ChassisNumber)

            If Not IsNothing(oRF.RevisionType) Then

                If oRF.RevisionType.ID = 1 Or oRF.RevisionType.ID = 2 Then
                    sapRow.SetValue("IR_TREV", oRF.RevisionType.RevisionCode & "-" & oRF.ChassisMaster.Category.CategoryCode)
                Else
                    sapRow.SetValue("IR_TREV", oRF.RevisionType.RevisionCode)
                End If


                Dim revPriceList = (From e As RevisionPrice In oRF.RevisionType.RevisionPrices
                                Where e.RowStatus = 0 And e.ValidFrom <= oRF.CreatedTime And e.Category.ID = oRF.ChassisMaster.Category.ID
                                Select e Order By e.ValidFrom Descending).ToList()

                Dim revPrice As RevisionPrice = New RevisionPrice

                If revPriceList.Count > 0 Then
                    revPrice = CType(revPriceList(0), RevisionPrice)
                End If

                If Not IsNothing(revPrice) Then
                    sapRow.SetValue("IR_AMOUNT", revPrice.Amount)
                End If
            End If

            sapRow.SetValue("IR_CUR", "IDR")
            sapRow.SetValue("IR_PREDT", oRF.CreatedTime.ToString("yyyyMMdd"))
            sapRow.SetValue("IR_CONDT", oRF.NewConfirmationDate.ToString("yyyyMMdd"))
            sapTab.Append(sapRow)

            bapiFunc.SetValue("IT_IR", sapTab)

            bapiFunc.Invoke(sapDest)

            Message = bapiFunc.GetValue("ED_MESSG")

            If String.IsNullOrEmpty(Message) Then
                Dim strResult As String = bapiFunc.GetValue("IT_RETIR").ToString()
                Dim strRegNo As String = ""

                strResult = strResult.Replace("}]", "")
                strResult = strResult.Substring(strResult.IndexOf("FIELD"), strResult.Length - (strResult.IndexOf("FIELD") + 1))
                strResult = strResult.Replace(" FIELD ", "|").Replace("=", "|")

                strRegNo = strResult.Split("|")(1)
                SONumber = strResult.Split("|")(5)

                'If strOrderRevNo.Trim = oRF.RegNumber.Trim Then
                '    objRevSAPDoc.DebitChargeStatus = 1
                'Else
                '    objFakturRevDN.DebitChargeStatus = 0
                'End If

                objRevSAPDoc.DebitChargeNo = SONumber
                objRevSAPDoc.RevisionFaktur = oRF
                objRevSAPDoc.DebitMemoNo = strResult.Split("|")(11)
                objRevSAPDoc.DCAmount = strResult.Split("|")(7)
                objRevSAPDoc.DMAmount = strResult.Split("|")(13)
            End If

            Return True
        Catch ex As RfcBaseException
            Throw New Exception("RFC Error " + ex.Message)
            Return False
        End Try
    End Function

#Region "CBUReturn"
    Public Function CBUReturnCheckClaim(ByVal datas As ArrayList, ByRef objCBUReturnSendSAP As CBUReturnSendSAP) As Boolean
        Try
            Dim conn As SAPSystemConnect = New SAPSystemConnect()
            Dim sapDest As RfcDestination = RfcDestinationManager.GetDestination(conn.GetParameters(connectionName))
            Dim sapRepo As RfcRepository = sapDest.Repository
            Dim bapiFunc = sapRepo.CreateFunction("ZFUFM_EQUI_REPLACEMENT")
            Dim params As IRfcTable = bapiFunc.GetTable("IT_PARAM")

            For Each item As DataRow In objCBUReturnSendSAP.SAPParameters.Rows
                Dim sapRow As IRfcStructure = params.Metadata.LineType.CreateStructure()
                sapRow.SetValue("EQUNR", item("ChassisDefect").ToString)
                sapRow.SetValue("VBELN", item("BillingNumberVH").ToString)
                sapRow.SetValue("EQUNR_R", item("NewChassis").ToString)
                params.Append(sapRow)
            Next

            bapiFunc.SetValue("IT_PARAM", params)
            bapiFunc.Invoke(sapDest)

            Dim results As IRfcTable = bapiFunc.GetTable("ET_RESULT")
            For Each row As IRfcStructure In results
                objCBUReturnSendSAP.AddBodyResponse(String.Format("K;{0}_{1}\nH;{2}", "CheckClaim", DateTime.Now.ToString("yyyyMMddHHmmss"), row.ToString()))

                Dim cBUReturnSAPResponse As CBUReturnSAPResponse = New CBUReturnSAPResponse
                cBUReturnSAPResponse.ChassisLama = row.GetValue("EQUNR")
                cBUReturnSAPResponse.BillingNumber = row.GetValue("VBELN")
                cBUReturnSAPResponse.ChassisPengganti = row.GetValue("EQUNR_R")
                cBUReturnSAPResponse.KodeStatus = row.GetValue("FAKST")
                cBUReturnSAPResponse.Message = row.GetValue("MESSG")
                cBUReturnSAPResponse.Status = row.GetValue("STATS")
                objCBUReturnSendSAP.SapResponses.Add(cBUReturnSAPResponse)

                If cBUReturnSAPResponse.Status = "S" Then
                    For Each data As ChassisMasterClaimHeader In datas
                        If data.ChassisMaster.ChassisNumber.Equals(cBUReturnSAPResponse.ChassisLama) Then
                            data.StatusProcessRetur = Short.Parse(cBUReturnSAPResponse.KodeStatus)
                            'data.ChassisNumberReplacement = cBUReturnSAPResponse.ChassisPengganti
                        End If
                    Next
                Else
                    objCBUReturnSendSAP.Message += IIf(objCBUReturnSendSAP.Message = "", cBUReturnSAPResponse.Message, String.Concat("\n", cBUReturnSAPResponse.Message))
                End If
            Next

            Return True
        Catch ex As Exception
            objCBUReturnSendSAP.Message = ex.Message.Replace("""", "")
            Return False
        End Try
    End Function

    Public Function CBUReturnStatusCancelFaktur(ByVal datas As ArrayList, ByRef objCBUReturnSendSAP As CBUReturnSendSAP) As Boolean
        Try
            Dim conn As SAPSystemConnect = New SAPSystemConnect()
            Dim sapDest As RfcDestination = RfcDestinationManager.GetDestination(conn.GetParameters(connectionName))
            Dim sapRepo As RfcRepository = sapDest.Repository
            Dim bapiFunc = sapRepo.CreateFunction("ZFUFM_CANCEL_FAKTUR")
            Dim params As IRfcTable = bapiFunc.GetTable("IT_PARAM")

            For Each item As DataRow In objCBUReturnSendSAP.SAPParameters.Rows
                Dim sapRow As IRfcStructure = params.Metadata.LineType.CreateStructure()
                sapRow.SetValue("EQUNR", item("ChassisDefect").ToString)
                params.Append(sapRow)
            Next

            bapiFunc.SetValue("IT_PARAM", params)
            bapiFunc.Invoke(sapDest)

            Dim results As IRfcTable = bapiFunc.GetTable("ET_RESULT")
            For Each row As IRfcStructure In results
                objCBUReturnSendSAP.AddBodyResponse(String.Format("K;{0}_{1}\nH;{2}", "CancelFaktur", DateTime.Now.ToString("yyyyMMddHHmmss"), row.ToString()))

                Dim cBUReturnSAPResponse As CBUReturnSAPResponse = New CBUReturnSAPResponse
                cBUReturnSAPResponse.ChassisLama = row.GetValue("EQUNR")
                cBUReturnSAPResponse.KodeStatus = row.GetValue("FAKST")
                cBUReturnSAPResponse.Message = row.GetValue("MESSG")
                cBUReturnSAPResponse.Status = row.GetValue("STATS")
                objCBUReturnSendSAP.SapResponses.Add(cBUReturnSAPResponse)

                If cBUReturnSAPResponse.Status = "S" Then
                    For Each data As ChassisMasterClaimHeader In datas
                        If data.ChassisMaster.ChassisNumber.Equals(cBUReturnSAPResponse.ChassisLama) Then
                            data.StatusProcessRetur = Short.Parse(cBUReturnSAPResponse.KodeStatus)
                        End If
                    Next
                Else
                    objCBUReturnSendSAP.Message += IIf(objCBUReturnSendSAP.Message = "", cBUReturnSAPResponse.Message, String.Concat("\n", cBUReturnSAPResponse.Message))
                End If
            Next

            Return True
        Catch ex As Exception
            objCBUReturnSendSAP.Message = ex.Message.Replace("""", "")
            Return False
        End Try
    End Function

    Public Function CBUReturnStatusCancelBilling(ByVal datas As ArrayList, ByRef objCBUReturnSendSAP As CBUReturnSendSAP) As Boolean
        Try
            Dim conn As SAPSystemConnect = New SAPSystemConnect()
            Dim sapDest As RfcDestination = RfcDestinationManager.GetDestination(conn.GetParameters(connectionName))
            Dim sapRepo As RfcRepository = sapDest.Repository
            Dim bapiFunc = sapRepo.CreateFunction("ZFUFM_CANCELBILL_SLSRETURN")
            Dim params As IRfcTable = bapiFunc.GetTable("IT_IN")

            For Each item As DataRow In objCBUReturnSendSAP.SAPParameters.Rows
                Dim sapRow As IRfcStructure = params.Metadata.LineType.CreateStructure()
                
                sapRow.SetValue("EQUNR", item("ChassisDefect").ToString)
                sapRow.SetValue("NEWEQUNR", item("NewChassis").ToString)
                sapRow.SetValue("OLDDO", item("DONumber").ToString)
                sapRow.SetValue("OLDBLVH", item("BillingNumberVH").ToString)
                sapRow.SetValue("OLDBLDEP", item("BillingNumberDep").ToString)
                params.Append(sapRow)
            Next

            bapiFunc.SetValue("IT_IN", params)
            bapiFunc.Invoke(sapDest)

            Dim results As IRfcTable = bapiFunc.GetTable("ET_OUT")
            For Each row As IRfcStructure In results
                objCBUReturnSendSAP.AddBodyResponse(String.Format("K;{0}_{1}\nH;{2}", "CancelBilling", DateTime.Now.ToString("yyyyMMddHHmmss"), row.ToString()))

                Dim cBUReturnSAPResponse As CBUReturnSAPResponse = New CBUReturnSAPResponse
                cBUReturnSAPResponse.ChassisLama = row.GetValue("EQUNR")
                cBUReturnSAPResponse.SORetur = row.GetValue("SORET")
                cBUReturnSAPResponse.DORetur = row.GetValue("DORET")
                cBUReturnSAPResponse.BillingRetur = row.GetValue("BLRET")
                cBUReturnSAPResponse.KodeStatus = row.GetValue("STFAK")
                cBUReturnSAPResponse.Message = row.GetValue("MESSG")
                cBUReturnSAPResponse.Status = IIf(cBUReturnSAPResponse.KodeStatus <> "", "S", "E")
                objCBUReturnSendSAP.SapResponses.Add(cBUReturnSAPResponse)

                If cBUReturnSAPResponse.Status = "S" Then
                    For Each data As ChassisMasterClaimHeader In datas
                        If data.ChassisMaster.ChassisNumber.Equals(cBUReturnSAPResponse.ChassisLama) Then
                            data.SORetur = cBUReturnSAPResponse.SORetur
                            data.DORetur = cBUReturnSAPResponse.DORetur
                            data.BillingRetur = cBUReturnSAPResponse.BillingRetur
                            data.StatusProcessRetur = Short.Parse(cBUReturnSAPResponse.KodeStatus)
                        End If
                    Next
                Else
                    objCBUReturnSendSAP.Message += IIf(objCBUReturnSendSAP.Message = "", cBUReturnSAPResponse.Message, String.Concat("\n", cBUReturnSAPResponse.Message))
                End If
            Next

            Return True
        Catch ex As Exception
            objCBUReturnSendSAP.Message = ex.Message.Replace("""", "")
            Return False
        End Try
    End Function

    Public Function CBUReturnStatusReserveDO(ByVal datas As ArrayList, ByRef objCBUReturnSendSAP As CBUReturnSendSAP) As Boolean
        Try
            Dim conn As SAPSystemConnect = New SAPSystemConnect()
            Dim sapDest As RfcDestination = RfcDestinationManager.GetDestination(conn.GetParameters(connectionName))
            Dim sapRepo As RfcRepository = sapDest.Repository
            Dim bapiFunc = sapRepo.CreateFunction("ZFUFM_CHANGE_UNIT")
            Dim params As IRfcTable = bapiFunc.GetTable("IT_IN")

            Dim EQUNRPairDODAT As New Hashtable

            For Each item As DataRow In objCBUReturnSendSAP.SAPParameters.Rows
                Dim sapRow As IRfcStructure = params.Metadata.LineType.CreateStructure()
                
                sapRow.SetValue("EQUNR", item("NewChassis").ToString)
                sapRow.SetValue("OLDDO", item("DONumber").ToString)
                params.Append(sapRow)
                EQUNRPairDODAT.Add(item("NewChassis").ToString, item("DODate").ToString)
            Next

            bapiFunc.SetValue("IT_IN", params)
            bapiFunc.Invoke(sapDest)

            Dim results As IRfcTable = bapiFunc.GetTable("ET_OUT")
            For Each row As IRfcStructure In results
                objCBUReturnSendSAP.AddBodyResponse(String.Format("K;{0}_{1}\nH;{2}", "ReserveDO", DateTime.Now.ToString("yyyyMMddHHmmss"), row.ToString()))

                Dim cBUReturnSAPResponse As CBUReturnSAPResponse = New CBUReturnSAPResponse
                cBUReturnSAPResponse.ChassisPengganti = row.GetValue("EQUNR")
                cBUReturnSAPResponse.DONumber = row.GetValue("OLDDO")
                cBUReturnSAPResponse.BillingVH = row.GetValue("BILVH")
                cBUReturnSAPResponse.BillingDEP = row.GetValue("BILDP")
                If String.IsNullOrEmpty(row.GetValue("DODAT").ToString) Then
                    cBUReturnSAPResponse.DoDate = CDate(EQUNRPairDODAT.Item(row.GetValue("EQUNR")))
                ElseIf CType(row.GetValue("DODAT").ToString, Integer) = 0 Then
                    cBUReturnSAPResponse.DoDate = CDate(EQUNRPairDODAT.Item(row.GetValue("EQUNR")))
                Else
                    Dim DODAT As String = row.GetValue("DODAT")
                    cBUReturnSAPResponse.DoDate = New Date(DODAT.Substring(4, 4), DODAT.Substring(2, 2), DODAT.Substring(0, 2))
                End If
                cBUReturnSAPResponse.EngineNumber = row.GetValue("MAPAR")
                cBUReturnSAPResponse.SerialNumber = row.GetValue("INVNR")
                cBUReturnSAPResponse.KodeStatus = row.GetValue("STFAK")
                cBUReturnSAPResponse.Message = row.GetValue("MESSG")
                cBUReturnSAPResponse.Status = IIf(cBUReturnSAPResponse.KodeStatus <> "", "S", "E")


                If cBUReturnSAPResponse.Status = "S" Then
                    For Each data As ChassisMasterClaimHeader In datas
                        If data.ChassisNumberReplacement.Equals(cBUReturnSAPResponse.ChassisPengganti) Then
                            data.BillingNormalRetur = String.Format("{0};{1}", cBUReturnSAPResponse.BillingVH, cBUReturnSAPResponse.BillingDEP)
                            cBUReturnSAPResponse.ChassisLama = data.ChassisMaster.ChassisNumber
                        End If
                    Next
                Else
                    objCBUReturnSendSAP.Message += IIf(objCBUReturnSendSAP.Message = "", cBUReturnSAPResponse.Message, String.Concat("\n", cBUReturnSAPResponse.Message))
                End If

                objCBUReturnSendSAP.SapResponses.Add(cBUReturnSAPResponse)
            Next

            Return True
        Catch ex As Exception
            objCBUReturnSendSAP.Message = ex.Message.Replace("""", "")
            Return False
        End Try
    End Function

    Public Function CBUReturnStatusSalesReplacement(ByVal datas As ArrayList, ByRef objCBUReturnSendSAP As CBUReturnSendSAP) As Boolean
        Try
            Dim conn As SAPSystemConnect = New SAPSystemConnect()
            Dim sapDest As RfcDestination = RfcDestinationManager.GetDestination(conn.GetParameters(connectionName))
            Dim sapRepo As RfcRepository = sapDest.Repository
            Dim bapiFunc = sapRepo.CreateFunction("ZFUFM_SLS_NORM_RETUR")
            Dim params As IRfcTable = bapiFunc.GetTable("IT_IN")

            Dim EQUNRPairDODAT As New Hashtable

            For Each item As DataRow In objCBUReturnSendSAP.SAPParameters.Rows
                Dim sapRow As IRfcStructure = params.Metadata.LineType.CreateStructure()
                sapRow.SetValue("EQUNR_O", item("ChassisDefect").ToString)
                sapRow.SetValue("EQUNR", item("NewChassis").ToString)
                sapRow.SetValue("BLRET", item("BillingRetur").ToString)
                params.Append(sapRow)

                EQUNRPairDODAT.Add(item("NewChassis").ToString, item("DODate").ToString)
            Next

            bapiFunc.SetValue("IT_IN", params)
            bapiFunc.Invoke(sapDest)

            Dim results As IRfcTable = bapiFunc.GetTable("ET_OUT")
            For Each row As IRfcStructure In results
                objCBUReturnSendSAP.AddBodyResponse(String.Format("K;{0}_{1}\nH;{2}", "SalesReplacement", DateTime.Now.ToString("yyyyMMddHHmmss"), row.ToString()))

                Dim cBUReturnSAPResponse As CBUReturnSAPResponse = New CBUReturnSAPResponse
                cBUReturnSAPResponse.ChassisPengganti = row.GetValue("EQUNR")
                cBUReturnSAPResponse.SOReplacement = row.GetValue("SOREP")
                cBUReturnSAPResponse.DOReplacement = row.GetValue("DOREP")
                cBUReturnSAPResponse.BillingReplacement = row.GetValue("BLREP")

                If String.IsNullOrEmpty(row.GetValue("DODAT").ToString) Then
                    cBUReturnSAPResponse.DoDate = CDate(EQUNRPairDODAT.Item(row.GetValue("EQUNR")))
                ElseIf CType(row.GetValue("DODAT").ToString, Integer) = 0 Then
                    cBUReturnSAPResponse.DoDate = CDate(EQUNRPairDODAT.Item(row.GetValue("EQUNR")))
                Else
                    Dim DODAT As String = row.GetValue("DODAT")
                    cBUReturnSAPResponse.DoDate = New Date(DODAT.Substring(4, 4), DODAT.Substring(2, 2), DODAT.Substring(0, 2))
                End If
                cBUReturnSAPResponse.EngineNumber = row.GetValue("MAPAR")
                cBUReturnSAPResponse.SerialNumber = row.GetValue("INVNR")
                cBUReturnSAPResponse.KodeStatus = row.GetValue("STFAK")
                cBUReturnSAPResponse.Message = row.GetValue("MESSG")
                cBUReturnSAPResponse.Status = IIf(cBUReturnSAPResponse.KodeStatus <> "", "S", "E")

                If cBUReturnSAPResponse.Status = "S" Then
                    For Each data As ChassisMasterClaimHeader In datas
                        If data.ChassisNumberReplacement.Equals(cBUReturnSAPResponse.ChassisPengganti) Then
                            data.SONormalRetur = cBUReturnSAPResponse.SOReplacement
                            data.DONormalRetur = cBUReturnSAPResponse.DOReplacement
                            data.BillingNormalRetur = cBUReturnSAPResponse.BillingReplacement
                            cBUReturnSAPResponse.ChassisLama = data.ChassisMaster.ChassisNumber
                        End If
                    Next
                Else
                    objCBUReturnSendSAP.Message += IIf(objCBUReturnSendSAP.Message = "", cBUReturnSAPResponse.Message, String.Concat("\n", cBUReturnSAPResponse.Message))
                End If
                objCBUReturnSendSAP.SapResponses.Add(cBUReturnSAPResponse)
            Next
            Return True
        Catch ex As Exception
            objCBUReturnSendSAP.Message = ex.Message.Replace("""", "")
            Return False
        End Try
    End Function

#End Region
End Class
