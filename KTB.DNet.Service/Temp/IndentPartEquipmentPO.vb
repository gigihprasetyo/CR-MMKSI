Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web
Imports System.Web.Mail
Imports System.Security.Principal
Imports System.Security.Cryptography

Imports KTB.DNet.BusinessFacade.IndentPartEquipment
Imports KTB.DNet.BusinessFacade.IndentPart
Imports Ktb.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Parser
Imports KTB.DNet.Lib


Public Class IndentPartEquipmentPO
    Inherits Job


    Private User As New System.Security.Principal.GenericPrincipal(New GenericIdentity("DNetService"), Nothing)

    Protected Overrides Function executeJob() As Boolean
        '1)Automatic daily email to certain KTB PIC list of estimation order that already due 14 days and dealer cant order 
        '2)KTB already confirmed order but dealer hasn’t submit order at H-11 after Confirmation date
        '3)Dealer already submit order but haven't send Deposit B receipt at H-11 after they submit

        LogHelper.WriteLog("executeJob- start...")
        Dim Sql As StringBuilder
        Dim dt As DateTime = Now
        Dim crit As CriteriaComposite
        Dim srt As SortCollection
        Dim oEEHFac As New EstimationEquipHeaderFacade(User)
        Dim aEEHs As ArrayList
        Dim oEEH As EstimationEquipHeader
        Dim oEEDFac As New EstimationEquipDetailFacade(User)
        Dim aEEDs As ArrayList
        Dim oEED As EstimationEquipDetail
        Dim oIPHFac As New IndentPartHeaderFacade(User)
        Dim aIPHs As ArrayList
        Dim oIPH As IndentPartHeader
        Dim oVEPFac As New v_EquipPOFacade(User)
        Dim oVEP As v_EquipPO
        Dim aVEPs As ArrayList
        Dim str As StringBuilder
        Dim tab As Char = Chr(9)
        Dim line As String = "<br>" ' Environment.NewLine
        Dim sRow As String
        Dim sTo As String = Me.EmailTo, sCC As String = Me.EmailCC
        Dim aDs As ArrayList
        Dim oDFac As New DealerFacade(User)
        Dim cD As CriteriaComposite

        '1)Automatic daily email to certain KTB PIC list of estimation order that already due 14 days and dealer cant order 
        If 1 = 1 Then
            Try
                dt = Now.AddDays(-14)

                'Sql = New StringBuilder
                'Sql.Append(" select eeh.ID from EstimationEquipHeader eeh ")
                'Sql.Append(" join EstimationEquipDetail eed on eeh.ID=eed.EstimationEquipHeaderID ")
                'Sql.Append(" where eeh.RowStatus=0 ")
                'Sql.Append(" and eeh.CreatedTime>='" & dt.ToString("yyyy/MM/dd") & "' ")
                'Sql.Append(" and eeh.CreatedTime<'" & dt.AddDays(1).ToString("yyyy/MM/dd") & "' ")
                'Sql.Append(" and eed.ID not in ( ")
                'Sql.Append(" select eepo.EstimationEquipDetailID ")
                'Sql.Append(" from EstimationEquipPO eepo ")
                'Sql.Append(" where 1=1 ")
                'Sql.Append(" and eepo.RowStatus=0 ")
                'Sql.Append(" ) ")

                Sql = New StringBuilder
                Sql.Append(" select eed.ID ")
                Sql.Append(" from EstimationEquipDetail eed ")
                Sql.Append(" join EstimationEquipHeader eeh on eeh.ID=eed.EstimationEquipHeaderID ")
                Sql.Append(" where 1=1 ")
                Sql.Append(" and year(eed.ConfirmedDate)<1900 ")
                Sql.Append(" and eeh.CreatedTime>='" & dt.ToString("yyyy/MM/dd") & "' ")
                Sql.Append(" and eeh.CreatedTime<'" & dt.AddDays(1).ToString("yyyy/MM/dd") & "' ")
                'Sql.Append(" order by eeh.EstimationNumber ")
                crit = New CriteriaComposite(New Criteria(GetType(EstimationEquipDetail), "ID", MatchType.InSet, "(" & Sql.ToString() & ")"))
                Dim srt1 As SortCollection
                srt1 = New SortCollection
                srt1.Add(New Sort(GetType(EstimationEquipDetail), "EstimationEquipHeader.EstimationNumber", Sort.SortDirection.ASC))
                aEEDs = oEEDFac.Retrieve(crit, srt1)

                str = New StringBuilder
                For i As Integer = 0 To aEEDs.Count - 1
                    oEED = aEEDs(i)
                    oEEH = oEED.EstimationEquipHeader
                    str.Append("<tr><td>" & (i + 1).ToString() & "</td><td>" & oEEH.EstimationNumber & "</td><td>" & oEEH.CreatedTime.ToString("dd MMM yyyy") & "</td><td>" & oEED.SparePartMaster.PartNumber & "</td><td>" & oEED.SparePartMaster.PartName & "</td><td>" & FormatNumber(oEED.EstimationUnit, 3) & "</td></tr>")
                Next
                'CommonFunction.SetIndentPartEmailRecipient(EquipUser.EquipUserGroup.Pengajuan_Estimasi, sTo, sCC, New Dealer)

                Dim sContents() As String = {str.ToString(), Now.ToString("dd MMMM yyyy")}
                sTo = ConfigurationSettings.AppSettings("IndentPartEquipmentPO1To")
                sCC = ConfigurationSettings.AppSettings("IndentPartEquipmentPO1CC")
                Me.SendEmail("D:\EmailTemplate\IndentPartEquipEstimationKTB.htm", sTo, sCC, "[Indent Part Equipment] Daftar Estimasi Yang Sudah 14 Hari Dan Belum Dikonfirmasi", sContents)

                LogHelper.WriteLog("Send Email IndentPartEquipmentPO (1) Success")
            Catch ex As Exception
                LogHelper.WriteLog("error 1 : " & ex.Message)
            End Try

        End If

        '2)KTB already confirmed order but dealer hasn’t SUBMIT order at H+7 after Confirmation date
        If 2 = 2 Then
            Try

                dt = Now.AddDays(-7)

                Sql = New StringBuilder
                Sql.Append(" select distinct(eeh.DealerID) DealerID ")
                Sql.Append(" from EstimationEquipHeader eeh ")
                Sql.Append(" join EstimationEquipDetail eed on eed.EstimationEquipHeaderID=eeh.ID ")
                Sql.Append(" where eeh.RowStatus=0 ")
                Sql.Append(" and eed.RowStatus=0 ")
                Sql.Append(" and eed.ConfirmedDate>='" & dt.ToString("yyyy/MM/dd") & "' ")
                Sql.Append(" and eed.ConfirmedDate<'" & dt.AddDays(1).ToString("yyyy/MM/dd") & "' ")

                'SUDAH DIBUAT ORDER
                Sql.Append(" and eed.ID in ( ")
                Sql.Append(" select eepo.EstimationEquipDetailID ")
                Sql.Append(" from EstimationEquipPO eepo ")
                Sql.Append(" where 1=1 ")
                Sql.Append(" and eepo.RowStatus=0 ")
                Sql.Append(" ) ")

                'BELUM DIKLIK TOMBOL KIRIM
                Sql.Append(" and eed.ID not in ( ")
                Sql.Append(" select eepo.EstimationEquipDetailID ")
                Sql.Append(" from EstimationEquipPO eepo ")
                Sql.Append(" join IndentPartDetail ipd on ipd.ID=eepo.IndentPartDetailID ")
                Sql.Append(" join IndentPartHeader iph on iph.ID=ipd.IndentPartHeaderID ")
                Sql.Append(" where 1=1 ")
                Sql.Append(" and iph.RowStatus=0 ")
                Sql.Append(" and iph.Status>0 ")
                Sql.Append(" ) ")

                cD = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                cD.opAnd(New Criteria(GetType(Dealer), "ID", MatchType.InSet, "(" & Sql.ToString() & ")"))
                srt = New SortCollection
                srt.Add(New Sort(GetType(Dealer), "DealerCode", Sort.SortDirection.ASC))
                aDs = oDFac.Retrieve(cD, srt)

                For Each oD As Dealer In aDs
                    Dim crit2 As CriteriaComposite
                    Dim srt2 As SortCollection
                    Sql = New StringBuilder

                    Sql.Append(" select iph.ID ")
                    Sql.Append(" from IndentPartHeader iph ")
                    Sql.Append(" join IndentPartDetail ipd on ipd.IndentPartHeaderID=iph.ID ")
                    Sql.Append(" join EstimationEquipPO eepo on eepo.IndentPartDetailID=ipd.ID ")
                    Sql.Append(" join EstimationEquipDetail eed on eed.ID=eepo.EstimationEquipDetailID ")
                    Sql.Append(" where 1=1 ")
                    Sql.Append(" and eed.ConfirmedDate>='" & dt.ToString("yyyy/MM/dd") & "' ")
                    Sql.Append(" and eed.ConfirmedDate<'" & dt.AddDays(1).ToString("yyyy/MM/dd") & "' ")
                    Sql.Append(" and iph.Status=0 ")
                    Sql.Append(" and iph.DealerID=" & od.ID.ToString() & " ")

                    crit2 = New CriteriaComposite(New Criteria(GetType(IndentPartHeader), "ID", MatchType.InSet, "(" & Sql.ToString() & ")"))

                    srt2 = New SortCollection
                    srt2.Add(New Sort(GetType(IndentPartHeader), "RequestNo", Sort.SortDirection.ASC))
                    aIPHs = oIPHFac.Retrieve(crit2, srt2)

                    str = New StringBuilder
                    For i As Integer = 0 To aIPHs.Count - 1
                        oIPH = aIPHs(i)
                        Dim TotalOrder As Decimal = 0
                        For Each oIPD As IndentPartDetail In oIPH.IndentPartDetails
                            TotalOrder += (oIPD.Qty * oIPD.Price)
                        Next
                        str.Append("<tr><td>" & (i + 1).ToString() & "</td><td>" & oIPH.RequestNo & "</td><td>" & oIPH.RequestDate.ToString("dd MMM yyyy") & "</td><td>" & oIPH.TotalQty.ToString() & "</td><td>" & FormatNumber(TotalOrder, 3) & "</td></tr>")
                    Next
                    Dim sContents() As String = {od.DealerCode, od.DealerName, od.City.CityName, str.ToString(), Now.ToString("dd MMMM yyyy")}
                    sTo = ""
                    sCC = ""
                    Me.SetIndentPartEmailRecipient(EquipUser.EquipUserGroup.Pengajuan_Estimasi, sTo, sCC, oD)
                    sTo &= ";" & ConfigurationSettings.AppSettings("IndentPartEquipmentPO2To")
                    sCC &= ";" & ConfigurationSettings.AppSettings("IndentPartEquipmentPO2CC")
                    Me.SendEmail("D:\EmailTemplate\IndentPartEquipEstimationDealer.htm", sTo, sCC, "[Indent Part Equipment] (" & oD.DealerCode & ") Daftar Order Yang Belum Melakukan Proses Kirim", sContents)
                Next
                LogHelper.WriteLog("Send Email IndentPartEquipmentPO (2) Success")
            Catch ex As Exception
                LogHelper.WriteLog("error 2 : " & ex.Message)
            End Try
        End If

        '3)Dealer already submit order but haven't send Deposit B receipt at H-11 after they submit
        If 3 = 3 Then
            Try
                Dim crit3 As CriteriaComposite
                Dim srt3 As SortCollection
                Dim srtD3 As SortCollection
                Dim cD3 As CriteriaComposite

                dt = Now.AddDays(-7)

                Sql = New StringBuilder
                Sql.Append(" select distinct(iph.DealerID) DealerID ")
                Sql.Append(" from IndentPartHeader iph ")
                Sql.Append(" where iph.RowStatus=0 ")
                Sql.Append(" and iph.PaymentType=1 ")
                Sql.Append(" and iph.RequestDate>='" & dt.ToString("yyyy/MM/dd") & "' ")
                Sql.Append(" and iph.RequestDate<'" & dt.AddDays(1).ToString("yyyy/MM/dd") & "' ")
                Sql.Append(" and iph.StatusKTB=" & CType(EnumIndentPartEquipStatus.EnumStatusKTB.Baru, Short).ToString())

                cD3 = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                cD3.opAnd(New Criteria(GetType(Dealer), "ID", MatchType.InSet, "(" & Sql.ToString() & ")"))
                srtD3 = New SortCollection
                srtD3.Add(New Sort(GetType(Dealer), "DealerCode", Sort.SortDirection.ASC))
                aDs = oDFac.Retrieve(cD3, srtD3)

                For Each oD As Dealer In aDs
                    Sql = New StringBuilder

                    Sql.Append(" select iph.ID ")
                    Sql.Append(" from IndentPartHeader iph ")
                    Sql.Append(" where iph.RowStatus=0 ")
                    Sql.Append(" and iph.PaymentType=1 ")
                    Sql.Append(" and iph.DealerID=" & od.ID.ToString() & " ")
                    Sql.Append(" and iph.RequestDate>='" & dt.ToString("yyyy/MM/dd") & "' ")
                    Sql.Append(" and iph.RequestDate<'" & dt.AddDays(1).ToString("yyyy/MM/dd") & "' ")
                    Sql.Append(" and iph.StatusKTB=" & CType(EnumIndentPartEquipStatus.EnumStatusKTB.Baru, Short).ToString())

                    crit3 = New CriteriaComposite(New Criteria(GetType(v_EquipPO), "ID", MatchType.InSet, "(" & Sql.ToString() & ")"))

                    srt3 = New SortCollection
                    srt3.Add(New Sort(GetType(v_EquipPO), "RequestNo", Sort.SortDirection.ASC))
                    aVEPs = oVEPFac.Retrieve(crit3, srt3)
                    If aVEPs.Count > 0 Then
                        str = New StringBuilder

                        For i As Integer = 0 To aVEPs.Count - 1
                            oVEP = aVEPs(i)
                            Dim TotalOrder3 As Decimal = 0
                            oIPH = oIPHFac.Retrieve(oVEP.ID)
                            If Not IsNothing(oIPH) AndAlso oIPH.ID > 0 Then
                                For Each oIPD As IndentPartDetail In oIPH.IndentPartDetails
                                    Dim oEstED As EstimationEquipDetail = oipd.EstimationEquipDetail
                                    Dim Harga As Decimal = oipd.Price

                                    If IsNothing(oEstED) = False AndAlso oEstED.ID > 0 Then
                                        Harga = oEstED.Harga
                                        Harga = ((100.0 - oEstED.Discount) / 100.0) * Harga
                                        TotalOrder3 += (oIPD.Qty * Harga)
                                    Else
                                        TotalOrder3 += (oipd.Qty * (oipd.Price - 0))
                                    End If
                                Next
                            End If

                            str.Append("<tr><td>" & (i + 1).ToString() & "</td><td>" & oVEP.RequestNo & "</td><td>" & oVEP.EstimationNumber & "</td><td>" & oVEP.CreatedTime.ToString("dd MMM yyyy") & "</td><td>" & oVEP.CreatedTime.AddDays(14).ToString("dd MMM yyyy") & "</td><td>" & oVEP.TotalItem.ToString() & "</td><td>" & FormatNumber(TotalOrder3, 3) & "</td></tr>")
                        Next
                        sTo = ""
                        sCC = ""
                        'LogHelper.WriteLog("before set recipient1")
                        Me.SetIndentPartEmailRecipient(EquipUser.EquipUserGroup.Deposit_B, sTo, sCC, oD)
                        sTo &= ";" & ConfigurationSettings.AppSettings("IndentPartEquipmentPO3To")
                        sCC &= ";" & ConfigurationSettings.AppSettings("IndentPartEquipmentPO3CC")
                        'LogHelper.WriteLog("before set recipient2")
                        Dim sContents() As String = {od.DealerCode, od.DealerName, od.City.CityName, str.ToString(), Now.ToString("dd MMM yyyy")}
                        'LogHelper.WriteLog("before send mail, TO:" & sTo & " ;CC:" & sCC & "")
                        Me.SendEmail("D:\EmailTemplate\IndentPartEquipOrderNoDepositB.htm", sTo, sCC, "[Indent Part Equipment] (" & oD.DealerCode & ") Daftar Order Yang Belum Mengirimkan Kwitansi Asli Deposit B", sContents)
                    End If
                Next
                LogHelper.WriteLog("Send Email IndentPartEquipmentPO (3) Success")
            Catch ex As Exception
                LogHelper.WriteLog("error 3 : " & ex.Message)
            End Try
        End If
    End Function

    Protected Overrides ReadOnly Property ModuleName() As String
        Get
            Return Me.GetType.Name
        End Get
    End Property

    Private Function SetIndentPartEmailRecipient(ByVal GroupType As Integer, ByRef sTo As String, ByRef sCC As String, ByRef objDealer As Dealer) As String
        Dim objEUFac As EquipUserFacade = New EquipUserFacade(User)
        Dim crtEU As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlEU As New ArrayList


        crtEU.opAnd(New Criteria(GetType(EquipUser), "UserName", MatchType.Exact, objDealer.DealerCode))
        crtEU.opAnd(New Criteria(GetType(EquipUser), "GroupType", MatchType.Exact, CType(GroupType, Short)))
        crtEU.opAnd(New Criteria(GetType(EquipUser), "Tipe", MatchType.Exact, CType(EquipUser.EquipUserTipe.TO_SENT, Integer).ToString))
        arlEU = objEUFac.Retrieve(crtEU)
        'If arlEU.Count > 0 Then sTo = CType(arlEU(0), EquipUser).Email
        If arlEU.Count > 0 Then sTo = ""
        For Each oEU As EquipUser In arlEU
            sTo &= IIf(sTo.Trim = "", "", ";") & oEU.Email
        Next

    End Function
End Class
