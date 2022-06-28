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
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.Parser
Imports KTB.DNet.Lib


Public Class CeilingChecker
    Inherits Job


    'Private User As New System.Security.Principal.GenericPrincipal(New GenericIdentity("DNetService"), Nothing)

    Protected Overrides Function executeJob() As Boolean
        '1)Automatic daily email to certain KTB PIC list of estimation order that already due 14 days and dealer cant order 
        '2)KTB already confirmed order but dealer hasn’t submit order at H-11 after Confirmation date
        '3)Dealer already submit order but haven't send Deposit B receipt at H-11 after they submit

        LogHelper.WriteLog("executeJob Ceiling - start...")

        Dim dt As DateTime = Now
        Dim crit As CriteriaComposite
        Dim srt As SortCollection

        Dim aVEPs As ArrayList
        Dim str As StringBuilder = New StringBuilder
        Dim str2 As StringBuilder = New StringBuilder

        Dim tab As Char = Chr(9)
        Dim line As String = "<br>" ' Environment.NewLine
        Dim block As String = "block"
        Dim none As String = "block"

        Dim sRow As String
        Dim sTo As String = Me.EmailTo, sCC As String = Me.EmailCC
        Dim aDs As ArrayList

        Dim cD As CriteriaComposite

        'Automatic hourly email to certain DNET PIC for Negative Ceiling (Over Ceiling) occured
        'and log for the last n hour transactions
        If 1 = 1 Then
            Try
                Dim dtRestore1 As DateTime
                Dim dtRestore2 As DateTime = dt
                Dim row As Integer = 0
                Dim MinimumCeiling As Double = CType(KTB.DNet.Lib.WebConfig.GetValue("CeilingMinimumValue"), Double)
                Dim TresHoldOutStanding As Double = CType(KTB.DNet.Lib.WebConfig.GetValue("CeilingOutStandingTreshold"), Double)

                If Me.Frequency = Library.Frequency.CustomDay Then
                    dtRestore1 = dt.AddDays(-1)
                ElseIf Me.Frequency = Library.Frequency.CustomHour Then
                    dtRestore1 = dt.AddHours(-1 * Me.NumOfFrequency)
                End If

                'ToDo Ali
                'Find CC list
                ''GEt CC LIST
                Dim ObjCreditAccount As ArrayList = New ArrayList
                Dim ObjDistinctCreditAccount As ArrayList = New ArrayList
                Dim Objv_CreditAccountFacade As v_CreditAccountFacade = New v_CreditAccountFacade(User)
                Dim ObjCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_CreditAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim ObjSortCollection As SortCollection = New SortCollection
                ObjSortCollection.Add(New Sort(GetType(Dealer), "CreditAccount", Sort.SortDirection.ASC))

                ObjCreditAccount = Objv_CreditAccountFacade.RetrieveList("CreditAccount", Sort.SortDirection.ASC)

                ''DISTINCT CC LIST
                For Each item As v_CreditAccount In ObjCreditAccount
                    If item.CreditAccount.Length > 0 AndAlso _
                        item.CreditAccount <> "000000" Then
                        ObjDistinctCreditAccount.Add(item.CreditAccount)
                    End If
                    row = row + 1

                Next

                ''Get Data Each Credit Account
                Dim No As Integer = 1
                Dim No2 As Integer = 1
                Dim oSCFac As New sp_CeilingFacade(User)
                Dim oCM As CreditMaster
                Dim OutStanding As Double = 0
                Dim OutStandingSAP As Double = 0


                For Each item As String In ObjDistinctCreditAccount
                    Dim aSCs As ArrayList
                    ''retrieve Ceiling
                    aSCs = oSCFac.RetrieveFromSP(New ProductCategory(-1), Now.ToString("yyyy/MM/dd"), Now.ToString("yyyy/MM/dd"), item, 0)

                    If (aSCs.Count > 0) Then
                        For Each oSC As sp_Ceiling In aSCs
                            Dim AvCeiling As Decimal = oSC.Ceiling - oSC.OutStanding - oSC.ProposedPO + oSC.LiquifiedPO

                            If oSC.PaymentType = CType(enumPaymentType.PaymentType.RTGS, Byte) Then AvCeiling = 0
                            oCM = New CreditMasterFacade(User).Retrieve(oSC.ID)

                            OutStanding = oSC.OutStanding

                            If Not IsNothing(oCM) AndAlso oCM.ID > 0 Then
                                If oCM.PaymentType = CType(enumPaymentType.PaymentType.RTGS, Short) Then
                                    OutStandingSAP = 0
                                Else
                                    OutStandingSAP = oCM.OutStanding
                                End If
                            Else
                                OutStandingSAP = 0
                            End If

                            If (oCM.PaymentType <> CType(enumPaymentType.PaymentType.RTGS, Short)) AndAlso Math.Abs(OutStanding - OutStandingSAP) > TresHoldOutStanding Then
                                str2.Append("<tr>" + "<td>" + No2.ToString() + "</td>" _
                              + "<td>" + oSC.CreditAccount + "</td>" _
                              + "<td>" + oCM.ProductCategory.Code + "</td>" _
                              + "<td>" + enumPaymentType.GetStringValue(oSC.PaymentType) + "</td>" _
                              + "<td>" + FormatNumber(OutStanding, 2, TriState.UseDefault, TriState.UseDefault, TriState.True) + "</td>" _
                              + "<td>" + FormatNumber(OutStandingSAP, 2, TriState.UseDefault, TriState.UseDefault, TriState.True) + "</td></tr>")
                                No2 = No2 + 1
                            End If

                            If (AvCeiling < MinimumCeiling) Then

                                Dim RPlafon As String = FormatNumber(AvCeiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                                str.Append("<tr>" + "<td>" + No.ToString() + "</td>" _
                                + "<td>" + oSC.CreditAccount + "</td>" _
                                + "<td>" + oCM.ProductCategory.Code + "</td>" _
                                + "<td>" + enumPaymentType.GetStringValue(oSC.PaymentType) + "</td>" _
                                + "<td>" + RPlafon + "</td></tr>")
                                No = No + 1

                            End If

                        Next



                    End If




                Next




                Dim sContents() As String = {IIf(str.Length > 0, block, none) _
                , dt.ToString("yyyy-MM-dd HH:mm:ss") _
                , IIf(str.Length > 0, block, none) _
               , IIf(str.Length > 0, str.ToString(), "<tr><td></td><td></td><td></td><td></td><td></td></tr>") _
               , dtRestore1.ToString("yyyy-MM-dd_HH:00") _
               , dtRestore2.ToString("yyyy-MM-dd_HH:00") _
               , IIf(str2.Length > 0, block, none) _
               , dt.ToString("yyyy-MM-dd HH:mm:ss") _
                 , IIf(str2.Length > 0, block, none) _
                , IIf(str2.Length > 0, str2.ToString(), "<tr><td></td><td></td><td></td><td></td><td></td><td></td></tr>")}
                '0:now - datetime checking
                '1:list of credit account
                '2:first restore point date
                '3:second restore point date

                Dim subject As String = "[MMKSI-DNet] Over Ceiling Checking"

                If (str.Length = 0 AndAlso str2.Length = 0) Then
                    subject = subject & " OK "
                ElseIf (str.Length <> 0) Then
                    subject = subject & " Alert "
                ElseIf (str.Length = 0 AndAlso str2.Length <> 0) Then
                    subject = subject & " Warning "
                End If
                sTo = KTB.DNet.Lib.WebConfig.GetValue("CeilingChecker1To")
                sCC = KTB.DNet.Lib.WebConfig.GetValue("CeilingChecker1CC")
                Dim Dir As String = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)

                Dir = Dir + "\EmailTemplate\CeilingChecker.htm"
                'Me.SendEmail(Dir, sTo, sCC, subject & Library.GetIPv4Address(), sContents)
                Me.SendEmail(Dir, sTo, sCC, subject, sContents)

                LogHelper.WriteLog("Send Email CeilingChecker Success")



            Catch ex As Exception
                Try
                    LogHelper.WriteLog("error Ceiling : " & ex.Message _
              & vbCrLf & "Error StackTrace Line : " & IIf(IsNothing(ex.StackTrace), "", ex.StackTrace.ToString()) _
               & vbCrLf & "Error InnerException Line : " & IIf(IsNothing(ex.InnerException), "", ex.InnerException.ToString()) _
              & vbCrLf & "Error Source Line : " & IIf(IsNothing(ex.Source), "", ex.Source.ToString()))
                Catch ex2 As Exception
                    LogHelper.WriteLog("error Ceiling : " & ex.Message)
                End Try



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
