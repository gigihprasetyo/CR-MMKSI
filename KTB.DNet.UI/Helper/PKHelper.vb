Imports KTB.DNet.SAP
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Parser.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PK
Imports System.Security.Principal
Imports System.Collections.Generic
Imports Newtonsoft.Json





Namespace KTB.DNet.UI.Helper
    Public Class PKHelper

        Dim User As IPrincipal

        Public Sub New(user As IPrincipal)
            user = user
        End Sub

        Private Function getDiscounts(ByVal PKHeaderDetails As ArrayList) As ArrayList
            Dim discount As ArrayList = New ArrayList()
            For Each detail As PKDetail In PKHeaderDetails
                Dim datas As DataSet = New PKHeaderFacade(User).RetrieveSp("exec sp_PKHeader_getDiscount @ID=" & detail.ID)
                If datas.Tables.Count > 0 Then
                    Dim dataTable As DataTable = datas.Tables(0)
                    If dataTable.Rows.Count > 0 Then
                        Dim discountCount As Integer = dataTable.Columns.Count
                        Dim tempDiscount As New ArrayList
                        For i As Integer = 1 To discountCount
                            Dim colStr = "discount" & i
                            If Not IsDBNull(dataTable.Rows(0)(colStr)) Then
                                tempDiscount.Add(CInt(dataTable.Rows(0)(colStr)))
                            End If
                        Next

                        discount.Add(tempDiscount)
                    End If
                End If
            Next

            Return discount
        End Function

        Private Function getPKDetails(ByVal materialNo As String, ByVal pkNo As String) As ArrayList
            Dim arrPKDetail As ArrayList = New ArrayList()
            Dim rawData As DataSet = New PKHeaderFacade(User).RetrieveSp("exec sp_PKHeader_getPKDetail @MaterialNo='" & materialNo & "',@PKNo='" & pkNo & "'")
            If rawData.Tables.Count > 0 Then
                If rawData.Tables(0).Rows.Count > 0 Then
                    For Each row As DataRow In rawData.Tables(0).Rows
                        Dim id As Integer = CInt(row("ID"))
                        Dim pkDetail As PKDetail = New PKDetailFacade(User).Retrieve(id)
                        arrPKDetail.Add(pkDetail)
                    Next
                End If
            End If

            Return arrPKDetail
        End Function

        Private Function getSAPBAPIDiscountCode()
            Dim result As New ArrayList()
            Dim crit1 As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, 0))
            crit1.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "SAPBAPIDiscountCode"))

            Dim arrObjSAPDiscCode As ArrayList = New StandardCodeFacade(User).Retrieve(crit1)
            For Each obj As StandardCode In arrObjSAPDiscCode
                result.Add(obj.ValueCode)
            Next

            Return result
        End Function

        Private Sub generateContract(ByVal pk As PKHeader, ByVal contractNo As String, ByVal lineItem As ArrayList, ByVal materialNo As ArrayList)
            Dim contractHeader As KTB.DNet.Parser.Domain.ContractJson = New KTB.DNet.Parser.Domain.ContractJson()
            contractHeader.ContractNo = contractNo
            contractHeader.DealerCode = pk.Dealer.DealerCode
            contractHeader.Description = pk.ProjectName
            contractHeader.PKNumber = pk.PKNumber
            contractHeader.ContractPeriod = pk.RequestPeriodeDay.ToString("D2") & pk.RequestPeriodeMonth.ToString("D2") & pk.RequestPeriodeYear.ToString("D4")
            contractHeader.ContractPricingPeriod = pk.PricingPeriodeDay.ToString("D2") & pk.PricingPeriodeMonth.ToString("D2") & pk.PricingPeriodeYear.ToString("D4")

            contractHeader.Detail = New List(Of ContractDetailJson)

            For i As Integer = 0 To materialNo.Count - 1

                Dim arrPKDetail As ArrayList = getPKDetails(materialNo(i), pk.PKNumber)
                Dim detail As KTB.DNet.Parser.Domain.ContractDetailJson = New KTB.DNet.Parser.Domain.ContractDetailJson()
                detail.LineItem = lineItem(i)
                detail.MaterialCode = materialNo(i)
                If arrPKDetail.Count > 0 Then
                    detail.ContractQty = CType(arrPKDetail(0), PKDetail).ResponseQty
                End If

                contractHeader.Detail.Add(detail)
            Next

            Dim strJson As String = JsonConvert.SerializeObject(contractHeader)
            Dim JSONWorker As KTB.DNet.Parser.JSONWorker = New KTB.DNet.Parser.JSONWorker()
            JSONWorker.JSONProses(strJson, "OCCREATE")
            Exit Sub
        End Sub

        Public Function TransferPKs(al As ArrayList, UserName As String, Password As String, ByRef messages As String, ByRef arrNewPK As ArrayList) As Boolean
            Try
                arrNewPK = New ArrayList()
                Dim sapConStr As String = KTB.DNet.Lib.WebConfig.GetValue("SAPConnectionStringEmpty") ' User "SAPConnectionString" and prompt user to enter password first
                Dim oSAPDnet As SAPDNet
                Dim SONumber As String = "", Msg As String = ""
                Dim aErrors As New ArrayList
                Dim oPKH As PKHeader
                messages = String.Empty

                oSAPDnet = New SAPDNet(sapConStr, UserName, Password)
                'oSAPDnet = New SAPDNet(sapConStr)
                For i As Integer = 0 To al.Count - 1
                    oPKH = CType(al(i), PKHeader)

                    SONumber = ""
                    Msg = ""

                    Dim isDetailAllZZZZ As Boolean = True
                    For Each item As PKDetail In oPKH.PKDetails
                        If item.VechileColor.ColorCode <> "ZZZZ" Then
                            isDetailAllZZZZ = False
                            Exit For
                        End If
                    Next

                    Dim objSPL As SPL

                    If Not isDetailAllZZZZ Then
                        objSPL = New SPLFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(oPKH.SPLNumber)
                    End If

                    Dim vechileColorID As Integer = New VechileColorFacade(System.Threading.Thread.CurrentPrincipal).Retrieve("ZZZZ").ID
                    Dim result As ArrayList = New ArrayList() ''[inquiryNo, contractNo, materialNo, lineItem]
                    Dim discounts As ArrayList = getDiscounts(oPKH.PKDetails)
                    Dim discountCode As ArrayList = getSAPBAPIDiscountCode()

                    If discounts.Count = discountCode.Count Then

                    End If
                    
                    oSAPDnet.SendPKViaRFC(oPKH, Msg, isDetailAllZZZZ, objSPL, vechileColorID, discounts, result, discountCode)
                    If Msg <> String.Empty Then
                        aErrors.Add("Error PK : " & oPKH.PKNumber & ". " & Msg)
                    Else
                        If result.Count > 0 Then
                            If result(1) <> "" OrElse Not IsNothing(result(1)) Then
                                generateContract(oPKH, result(1), result(3), result(2))
                                Dim tempPKHeader As PKHeader = New PKHeaderFacade(User).Retrieve(oPKH.ID)
                                arrNewPK.Add(tempPKHeader)
                            End If
                        End If

                        'oPKH.PKStatus = 9
                        'Dim updateResult As Integer = New PKHeaderFacade(User).Update(oPKH)
                    End If
                Next
                If aErrors.Count > 0 Then
                    Msg = ""
                    For Each erm As String In aErrors
                        Msg = Msg & erm & ";"
                    Next
                    messages = "Transfer Gagal. " & Msg
                    Return False
                Else
                    messages = "Transfer Berhasil."
                    Return True
                End If
            Catch ex As Exception
                messages = "Transfer Gagal. " & ex.Message
                Return False
            End Try
        End Function

        Public Function PKDetailMatchingCCVCValidation(ByVal txtColorCode As String, ByVal txtVechileTypeCode As String, ByVal assyYear As String) As Boolean
            Dim ret As Boolean = False
            Dim crit As New CriteriaComposite(New Criteria(GetType(VechileColorIsActiveOnPK), "RowStatus", MatchType.Exact, 0))
            crit.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "Status", MatchType.Exact, 1))
            crit.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "ProductionYear", MatchType.Exact, assyYear))
            crit.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "VechileColor.ColorCode", MatchType.Exact, txtColorCode))
            crit.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "VechileColor.Status", MatchType.No, "X"))
            crit.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "VechileColor.VechileType.VechileTypeCode", MatchType.Exact, txtVechileTypeCode))

            Dim arrData As ArrayList = New VechileColorIsActiveOnPKFacade(User).Retrieve(crit)
            If arrData.Count > 0 Then
                ret = True
            End If

            Return ret
        End Function
    End Class
End Namespace
