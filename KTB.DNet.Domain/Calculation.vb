' TODO: Please validate the operands (input/argument) before do calculations
Namespace KTB.DNet.Domain

    Public Class Calculation

        Private Shared Function CountPKDiscount(ByVal discountBase As Double, ByVal PPN_BM As Double, ByVal PPN As Double) As Double
            Return discountBase / (1 + ((PPN_BM + PPN) / 100))
        End Function

        Public Shared Function CountPKVehiclePrice(ByVal discountBase As Double, ByVal PPN_BM As Double, ByVal PPN As Double, ByVal BasePrice As Double, ByVal OptionPrice As Double, ByVal SalesSurcharge As Double) As Double
            Return ((((BasePrice - (CountPKDiscount(discountBase, PPN_BM, PPN))) * (1 + ((PPN_BM + PPN) / 100))) + (OptionPrice - SalesSurcharge)))
        End Function

        Public Shared Function CountPKPPh22(ByVal BasePrice As Double, ByVal discountBase As Double, ByVal PPN_BM As Double, ByVal PPN As Double, ByVal PPh22 As Double) As Double
            Return ((BasePrice - CountPKDiscount(discountBase, PPN_BM, PPN)) * (PPh22 / 100))
        End Function

        Public Shared Function CountPKHargaTotal(ByVal arrList As ArrayList) As Double
            Dim Total As Double = 0
            For Each item As PKDetail In arrList
                If Not item.PKHeader Is Nothing Then
                    If item.PKHeader.PKStatus = enumStatusPK.Status.Rilis _
                        OrElse item.PKHeader.PKStatus = enumStatusPK.Status.Tidak_Setuju _
                        OrElse item.PKHeader.PKStatus = enumStatusPK.Status.Setuju _
                        OrElse item.PKHeader.PKStatus = enumStatusPK.Status.DiBlok _
                        OrElse item.PKHeader.PKStatus = enumStatusPK.Status.Selesai Then
                        'Total = Total + (CInt(item.ResponseQty) * (System.Convert.ToDouble(item.ResponseAmount) + System.Convert.ToDouble(item.ResponsePPh22)))
                        'Total = Total + (CInt(item.ResponseQty) * (System.Convert.ToDouble(item.ResponseAmount) + System.Convert.ToDouble(item.ResponsePPh22) - System.Convert.ToDouble(item.ResponseDiscount) - System.Convert.ToDouble(item.ResponseSalesSurcharge)))
                        Total += (CInt(item.ResponseQty) * (System.Convert.ToDouble(item.ResponseAmount) + System.Convert.ToDouble(item.ResponsePPh22)))
                        'ElseIf item.PKHeader.PKStatus = enumStatusPK.Status.Konfirmasi Then
                        '    Total = Total + (CInt(item.TargetQty) * (System.Convert.ToDouble(item.ResponseAmount) + System.Convert.ToDouble(item.ResponsePPh22)))
                    Else
                        Total = Total + (CInt(item.TargetQty) * (System.Convert.ToDouble(item.TargetAmount) + System.Convert.ToDouble(item.TargetPPh22) - System.Convert.ToDouble(item.ResponseDiscount) - System.Convert.ToDouble(item.ResponseSalesSurcharge)))
                    End If
                Else
                    Total = Total + (CInt(item.TargetQty) * (System.Convert.ToDouble(item.TargetAmount) + System.Convert.ToDouble(item.TargetPPh22)))
                End If

            Next
            Return Total
        End Function

        Public Shared Function CountPKUnit(ByVal arrList As ArrayList) As Integer
            Dim Total As Integer = 0
            For Each item As PKDetail In arrList
                If Not item.PKHeader Is Nothing Then
                    If item.PKHeader.PKStatus = enumStatusPK.Status.Rilis OrElse item.PKHeader.PKStatus = enumStatusPK.Status.Setuju OrElse item.PKHeader.PKStatus = enumStatusPK.Status.Selesai Then
                        Total = Total + System.Convert.ToInt32(item.ResponseQty)
                    Else
                        Total = Total + System.Convert.ToInt32(item.TargetQty)
                    End If
                Else
                    Total = Total + System.Convert.ToInt32(item.TargetQty)
                End If
            Next
            Return Total
        End Function

        Public Shared Function CountPESubTotal(ByVal arrList As ArrayList) As Double
            Dim _total As Double = 0
            For Each item As EquipmentSalesDetail In arrList
                If item.Price <> 0 Then
                    _total += (item.Price - ((item.Discount * item.Price) / 100)) * item.Quantity
                Else
                    _total += (item.PriceFromEquipmentMaster - ((item.Discount * item.PriceFromEquipmentMaster) / 100)) * item.Quantity
                End If
            Next
            Return _total
        End Function

        Public Shared Function CountPESubsidi(ByVal arrList As ArrayList) As Double
            Dim _total As Double = 0
            For Each item As EquipmentSalesDetail In arrList
                If item.Price <> 0 Then
                    _total += ((item.Discount * item.Price) / 100) * item.Quantity
                Else
                    _total += ((item.Discount * item.PriceFromEquipmentMaster) / 100) * item.Quantity
                End If
            Next
            Return _total
        End Function

        Public Shared Function CountPEPPN(ByVal arrList As ArrayList) As Double
            Dim _total As Double = 0
            _total = CountPESubTotal(arrList) * 10 / 100
            Return _total
        End Function

        Public Shared Function CountPETotal(ByVal arrList As ArrayList) As Double
            Dim _total As Double = 0
            _total = CountPESubTotal(arrList) * 110 / 100
            Return _total
        End Function
        'add by FWA 20190316 --start--
        Public Shared Function CountInterest(ByVal nFreeDays As Integer, ByVal nTOP As Integer, ByVal nMonth As Integer, ByVal interest As Double, ByVal price As Double, ByVal pPH23 As Double) As Double
            'Return ((nTOP / nMonth) * (interest / 100) * price * ((100 - pPH23) / 100))
            Dim flInterest As Double = 0

            flInterest = (((nTOP - nFreeDays) / nMonth) * (interest / 100) * price * ((100 - pPH23) / 100))

            If flInterest <= 0 Then
                Return 0
            Else
                Return flInterest
            End If
        End Function
        '--end--

        Public Shared Function CountInterest(ByVal nTOP As Integer, ByVal nMonth As Integer, ByVal interest As Double, ByVal price As Double, ByVal pPH23 As Double) As Double
            Return ((nTOP / nMonth) * (interest / 100) * price * ((100 - pPH23) / 100))
        End Function

        '' CR Sirkular Rewards
        '' by : ali Akbar
        '' 2014-09-24

        Private Shared Function CountVehicleDiscount(ByVal ObjContractDetail As ContractDetail, ByVal ObjPrice As Price) As Double
            Return ObjContractDetail.Discount / (1 + ((ObjPrice.PPN_BM + ObjPrice.PPN) / 100))
        End Function

        Public Shared Function CountRewardAmount(ByVal ObjContractDetail As ContractDetail, ByVal ObjPrice As Price, ByVal TOPDays As Integer, ByVal DaysOfMonth As Integer) As Double
            Dim _VehicleDiscount As Double = CountVehicleDiscount(ObjContractDetail, ObjPrice)
            Dim _NettPrice As Double = ObjPrice.BasePrice - _VehicleDiscount
            Dim _RewardAmount As Double = (_NettPrice * (ObjPrice.DiscountReward / 100) * (TOPDays / DaysOfMonth))
            'CountRewardAmount(_NettPrice, ObjPrice.DiscountReward, TOPDays, DaysOfMonth)

            Return _RewardAmount
        End Function

        Private Shared Function CountNettPrice_BeforeTax(ByVal ObjContractDetail As ContractDetail, ByVal ObjPrice As Price, ByVal TOPDays As Integer, ByVal DaysOfMonth As Integer) As Double
            Dim _VehicleDiscount As Double = CountVehicleDiscount(ObjContractDetail, ObjPrice)
            Dim _RewardAmount As Double = CountRewardAmount(ObjContractDetail, ObjPrice, TOPDays, DaysOfMonth)
            Dim _RewardAmountDepA As Double = CountRewardAmountDepositA(ObjPrice, TOPDays, DaysOfMonth)
            Return ObjPrice.BasePrice - _VehicleDiscount - (_RewardAmount + _RewardAmountDepA)
        End Function

        Private Shared Function CountNettPrice_AfterTax(ByVal ObjContractDetail As ContractDetail, ByVal ObjPrice As Price, ByVal TOPDays As Integer, ByVal DaysOfMonth As Integer) As Double
            Dim _NettPrice_BeforeTax As Double = CountNettPrice_BeforeTax(ObjContractDetail, ObjPrice, TOPDays, DaysOfMonth)
            Dim _NettPrice_AfterTax As Double = _NettPrice_BeforeTax + (ObjPrice.PPN * _NettPrice_BeforeTax / 100) + (ObjPrice.PPN_BM * _NettPrice_BeforeTax / 100)
            Return _NettPrice_AfterTax
        End Function

        ''Pakai
        Public Shared Function CountRewardsVehiclePrice(ByVal ObjContractDetail As ContractDetail, ByVal ObjPrice As Price, ByVal TOPDays As Integer, ByVal DaysOfMonth As Integer) As Double
            Dim _NettPrice_AfterTax As Double = CountNettPrice_AfterTax(ObjContractDetail, ObjPrice, TOPDays, DaysOfMonth)
            Dim VehicleNettPrice As Double = 0
            VehicleNettPrice = _NettPrice_AfterTax + ObjPrice.OptionPrice

            Return Math.Round(VehicleNettPrice)
        End Function


        Public Shared Function CountRewardPPh22(ByVal ObjContractDetail As ContractDetail, ByVal ObjPrice As Price, ByVal TOPDays As Integer, ByVal DaysOfMonth As Integer) As Double
            Dim _NettPrice_BeforeTax As Double = CountNettPrice_BeforeTax(ObjContractDetail, ObjPrice, TOPDays, DaysOfMonth)
            Dim _RewardPPh22 As Double = 0
            _RewardPPh22 = _NettPrice_BeforeTax * (ObjPrice.PPh22 / 100)
            Return Math.Round(_RewardPPh22)
        End Function


        Private Shared Function CountRewardsGrossInterest(ByVal ObjContractDetail As ContractDetail, ByVal ObjPrice As Price, ByVal TOPDays As Integer, ByVal DaysOfMonth As Integer) As Double
            Dim _VehiclePrice As Double = CountRewardsVehiclePrice(ObjContractDetail, ObjPrice, TOPDays, DaysOfMonth)
            Dim _GrossInterest As Double = 0
            _GrossInterest = _VehiclePrice * (ObjPrice.FactoringInt / 100) * (TOPDays / DaysOfMonth)
            Return _GrossInterest
        End Function



        Public Shared Function CountRewardsInterest(ByVal ObjContractDetail As ContractDetail, ByVal ObjPrice As Price, ByVal TOPDays As Integer, ByVal DaysOfMonth As Integer) As Double
            Dim _GrossInterest As Double = CountRewardsGrossInterest(ObjContractDetail, ObjPrice, TOPDays, DaysOfMonth)
            Dim _PPh23 As Double = _GrossInterest * (ObjPrice.PPh23 / 100)
            Dim _RewardsInterest As Double = _GrossInterest - _PPh23

            Return _RewardsInterest
        End Function

        Public Shared Function CountRewardsInterest(ByVal ObjContractDetail As ContractDetail, ByVal ObjPrice As Price, ByVal TOPDays As Integer, ByVal DaysOfMonth As Integer, ByVal freeIntIndicator As Integer) As Double
            Dim _GrossInterest As Double = CountRewardsGrossInterest(ObjContractDetail, ObjPrice, TOPDays, DaysOfMonth)
            Dim _PPh23 As Double = _GrossInterest * (ObjPrice.PPh23 / 100)
            Dim _RewardsInterest As Double = _GrossInterest - _PPh23

            Return _RewardsInterest * freeIntIndicator
        End Function


        Public Shared Function CountRewardAmountDepositA(ByVal ObjPrice As Price, ByVal TOPDays As Integer, ByVal DaysOfMonth As Integer) As Double
            'RewardAmountDepA = (Price.OptionPrice * (Price.DiscountReward / 100) * (TOP Days / Days of Month)) / (1 + ((Price.PPN_BM + Price.PPN) / 100)
            Dim RewardAmountDeposit As Double = 0
            RewardAmountDeposit = (ObjPrice.OptionPrice * (ObjPrice.DiscountReward / 100) * (TOPDays / DaysOfMonth)) / (1 + ((ObjPrice.PPN_BM + ObjPrice.PPN) / 100))
            Return RewardAmountDeposit

            'Dim RewardAmountDeposit As Double = 0
            'RewardAmountDeposit = ObjPrice.OptionPrice * (ObjPrice.DiscountReward / 100) * (TOPDays / DaysOfMonth)
            'Return Math.Round(RewardAmountDeposit)
        End Function



        '




        '' END OF CR Sirkular Rewards



    End Class

End Namespace