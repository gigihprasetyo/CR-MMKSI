#Region "import"
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web
Imports System.Web.Mail
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.Linq
Imports System.Data


Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Lib
#End Region

Public Class TOPSPEnhancementEmail
    Inherits Job

    Protected Overrides ReadOnly Property ModuleName As String
        Get
            Return Me.GetType.Name
        End Get
    End Property


    Protected Overrides Function executeJob() As Boolean
        Dim _procID As String = Guid.NewGuid().ToString()
        LogHelper.WriteLog(String.Format("executeJob {0} - start \n ID {1}", Me.ModuleName, _procID))
        Dim dt As DateTime = Now

        Try
            dt = New DateTime(Now.Year, Now.Month, Now.Day, GetTime(Me.CurrentTime).Hour, GetTime(Me.CurrentTime).Minute, 0)
            Dim gs As JobTitle = GetSchedule(dt)
            Select Case gs
                Case JobTitle.Confirmation
                    LogHelper.WriteLog(String.Format("executeJob  {0} : \n ID {1} \n {2}", Me.ModuleName, _procID, gs.ToString()))
                    ConfirmationEmail(_procID)
                Case JobTitle.DueDate
                    LogHelper.WriteLog(String.Format("executeJob  {0} : \n ID {1} \n {2}", Me.ModuleName, _procID, gs.ToString()))
                    DueDateEmail(_procID)
                    'Case JobTitle.Selesai
                    '    LogHelper.WriteLog(String.Format("executeJob  {0} : \n ID {1} \n {2}", Me.ModuleName, _procID, gs.ToString()))
                    '    SelesaiEmail(_procID)
                Case Else
                    LogHelper.WriteLog(String.Format("executeJob  {0} : \n ID {1} \n {2}", Me.ModuleName, _procID, gs.ToString()))
            End Select

        Catch ex As Exception
            LogHelper.WriteLog(String.Format("executeJob {0} \n ID  {1} \n Error \n :{2} ", Me.ModuleName, _procID, ex.Message.ToString()))
        Finally
            LogHelper.WriteLog(String.Format("executeJob {0}   \n ID {1} END...", Me.ModuleName, _procID))

        End Try
        Return True
    End Function

    Public Enum JobTitle
        Confirmation
        DueDate
        Selesai
        None
    End Enum

    Private Function GetTime(ByVal pDt As DateTime) As DateTime
        Dim nMinute As Integer = pDt.Minute

        Dim ObjDt As DateTime
        If pDt.Minute >= 15 AndAlso pDt.Minute <= 40 Then
            ObjDt = New DateTime(1900, 1, 1, pDt.Hour, 30, 0)
        Else
            If nMinute >= 0 AndAlso nMinute <= 28 Then
                ObjDt = New DateTime(1900, 1, 1, pDt.Hour, 0, 0)
            Else
                ObjDt = New DateTime(1900, 1, 1, pDt.Hour, 0, 0).AddHours(1)
            End If

        End If
        Return ObjDt
    End Function

    Private Function GetSchedule(ByVal dt As DateTime) As JobTitle
        Dim res As JobTitle
        res = JobTitle.None
        Dim strKey As String = String.Format("TOPSPEnhancementEmail_{0}", dt.ToString("HHmm"))
        Try
            Dim strVal = System.Configuration.ConfigurationManager.AppSettings(strKey)
            If Not IsNothing(strVal) Then
                If strVal.ToString().ToLower() = "confirmation" Then
                    res = JobTitle.Confirmation
                End If
                If strVal.ToString().ToLower() = "duedate" Then
                    res = JobTitle.DueDate
                End If
                'If strVal.ToString().ToLower() = "selesai" Then
                '    res = JobTitle.Selesai
                'End If
            End If
        Catch ex As Exception
            res = JobTitle.None
        End Try

        Return res
    End Function

    Private Sub SetEmailRecipient(ByRef sTo As String, ByRef sCC As String, ByVal objDealerCreditAccount As String)

        Dim objEUFac As SparePartDueDateNotificationFacade = New SparePartDueDateNotificationFacade(User)
        Dim crtEU As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartDueDateNotification), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtEU.opAnd(New Criteria(GetType(SparePartDueDateNotification), "Dealer.CreditAccount", MatchType.Exact, objDealerCreditAccount))
        crtEU.opAnd(New Criteria(GetType(SparePartDueDateNotification), "EmailNotificationKind", MatchType.Exact, 1))
        'crtEU.opAnd(New Criteria(GetType(PaymentNotifEmail), "GroupType", MatchType.Exact, CType(GroupType, Short)))

        Dim arlEU As New ArrayList
        arlEU = objEUFac.Retrieve(crtEU)
        If arlEU.Count > 0 Then sTo = ""
        For Each oEU As SparePartDueDateNotification In arlEU
            If oEU.PositionRecipient.ToLower() = "to" Then
                If Not sTo.Contains(oEU.EmailDealer.ToLower()) Then
                    sTo &= IIf(sTo.Trim = "", "", ";") & oEU.EmailDealer.ToLower()
                End If

            Else
                If Not sCC.Contains(oEU.EmailDealer.ToLower()) Then
                    sCC &= IIf(sCC.Trim = "", "", ";") & oEU.EmailDealer.ToLower()
                End If

            End If
        Next

    End Sub

    Private Sub ConfirmationEmail(d As String)
        Dim dt As DateTime = DateTime.Now
        Dim ds As New DataSet
        Dim nF As SparePartDueDateNotificationFacade = New SparePartDueDateNotificationFacade(User)
        Dim Dir As String
        Dim strConsHeader As String



        ds = nF.GetNotifForConfirmation(dt)


        Const htmlRow As String = " <tr> <td width='40' align='RIGHT'>{No}</td> <td width='150' align='LEFT'>{DealerCode}</td> <td width='150' align='LEFT'>{BillingNumber}</td> <td width='150' align='center'>{BillingDate}</td> <td width='150' align='center'>{DueDate}</td> <td width='100' align='RIGHT'>{TotalAmount}</td> </tr>"

        If Not (Not IsNothing(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0) Then
            LogHelper.WriteLog(String.Format("executeJob {0}   \n ID {1} \n No data...", Me.ModuleName, d))
            Exit Sub
        End If


        Dir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)
        Dir = Dir + "\EmailTemplate\SparePartConf.htm"
        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(Dir)
        strConsHeader = sr.ReadToEnd()
        sr.Close()

        'Group Per Credit Account

        Dim drCR As DataTable = ds.Tables(0).DefaultView.ToTable(True, "DealerCode")

        For Each drC As DataRow In drCR.Rows
            Dim objDealer As Dealer
            'objDealer = New DealerFacade(User).GetDealerByCreditAccount(drC("CreditAccount").ToString())
            objDealer = New DealerFacade(User).GetDealer(drC("DealerCode").ToString())

            Dim srEmailTemplate As String = strConsHeader
            Dim strDetail As New StringBuilder



            'fill Header Value
            srEmailTemplate = srEmailTemplate.Replace("{DealerCode}", objDealer.DealerCode)
            srEmailTemplate = srEmailTemplate.Replace("{DealerName}", objDealer.DealerName)
            Try
                srEmailTemplate = srEmailTemplate.Replace("{DealerCity}", objDealer.City.CityName)
            Catch ex As Exception

            End Try



            'Build Detail Value
            Dim drDealer = ds.Tables(0).Select(String.Format("DealerCode='{0}'", drC("DealerCode").ToString()), "DealerCode ASC")
            Dim iNo As Integer = 1

            For Each drD As DataRow In drDealer
                Dim strRow As String = htmlRow
                strRow = strRow.Replace("{No}", iNo.ToString())
                strRow = strRow.Replace("{BillingNumber}", drD("BillingNumber"))
                strRow = strRow.Replace("{DealerCode}", drD("DealerCode"))
                strRow = strRow.Replace("{BillingDate}", Convert.ToDateTime(drD("BillingDate")).ToString("yyyy/MM/dd"))
                strRow = strRow.Replace("{DueDate}", Convert.ToDateTime(drD("DueDate")).ToString("yyyy/MM/dd"))
                strRow = strRow.Replace("{TotalAmount}", FormatNumber(Convert.ToDouble(drD("TotalAmount")), 2, TriState.UseDefault, TriState.UseDefault, TriState.True))

                strDetail.AppendLine(strRow)
                iNo += 1

            Next

            'fill Detail Value
            srEmailTemplate = srEmailTemplate.Replace("{DealerRow}", strDetail.ToString())


            'fill Footer Value
            srEmailTemplate = srEmailTemplate.Replace("{Tanggal}", DateTime.Now.ToString("dd MMMM yyyy"))

            'Send Email
            Dim strBCC As String = System.Configuration.ConfigurationManager.AppSettings("TOPSPEnhancementEmailBCC").ToString()
            Dim strCC As String = System.Configuration.ConfigurationManager.AppSettings("TOPSPEnhancementEmailCC").ToString()
            Dim strTo As String = ""

            Me.SetEmailRecipient(strTo, strCC, drC("DealerCode").ToString())
            Me.SendEmail(strTo, strCC, strBCC, "Reminder Penginputan Pembayaran TOP Spare Part", srEmailTemplate)
        Next
    End Sub

    Private Sub DueDateEmail(d As String)
        Dim dt As DateTime = DateTime.Now
        Dim ds As New DataSet
        Dim nF As New SparePartDueDateNotificationFacade(User)
        ds = nF.GetNotifForDueDate(dt)

        Const htmlRow As String = " <tr> <td width='40' align='RIGHT'>{No}</td> <td width='150' align='LEFT'>{DealerCode}</td> <td width='150' align='LEFT'>{BillingNumber}</td>  <td width='150' align='LEFT'>{RegNumber}</td>  <td width='150' align='center'>{BillingDate}</td> <td width='150' align='center'>{DueDate}</td> <td width='100' align='RIGHT'>{TotalAmount}</td> </tr>"

        If Not (Not IsNothing(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0) Then
            LogHelper.WriteLog(String.Format("executeJob {0}   \n ID {1} \n No data...", Me.ModuleName, d))
            Exit Sub
        End If

        Dim Dir As String
        Dim strConsHeader As String

        Dir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)
        Dir = Dir + "\EmailTemplate\SparePartDueDate.htm"
        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(Dir)
        strConsHeader = sr.ReadToEnd()
        sr.Close()


        'Group Per Credit Account


        Dim drCR As DataTable = ds.Tables(0).DefaultView.ToTable(True, "DealerCode")

        For Each drC As DataRow In drCR.Rows
            Dim objDealer As Dealer
            objDealer = New DealerFacade(User).GetDealer(drC("DealerCode").ToString())

            Dim srEmailTemplate As String = strConsHeader
            Dim strDetail As New StringBuilder

            'fill Header Value
            srEmailTemplate = srEmailTemplate.Replace("{CreditAccount}", drC("DealerCode").ToString())
            srEmailTemplate = srEmailTemplate.Replace("{CreditName}", objDealer.DealerName)
            Try
                srEmailTemplate = srEmailTemplate.Replace("{CreditCity}", objDealer.City.CityName)
            Catch ex As Exception

            End Try

            'Build Detail Value
            Dim drDealer = ds.Tables(0).Select(String.Format("DealerCode='{0}'", drC("DealerCode").ToString()), "DealerCode ASC")
            Dim iNo As Integer = 1

            For Each drD As DataRow In drDealer
                Dim strRow As String = htmlRow
                strRow = strRow.Replace("{No}", iNo.ToString())
                strRow = strRow.Replace("{BillingNumber}", drD("BillingNumber"))
                strRow = strRow.Replace("{DealerCode}", drD("DealerCode"))
                Dim varDetail As TOPSPTransferPaymentDetail = New TOPSPTransferPaymentDetailFacade(User).Retrieve(drD("BillingNumber").ToString(), "")
                If IsNothing(varDetail) Then
                    strRow = strRow.Replace("{RegNumber}", "")
                Else
                    strRow = strRow.Replace("{RegNumber}", varDetail.TOPSPTransferPayment.RegNumber)
                End If
                strRow = strRow.Replace("{BillingDate}", Convert.ToDateTime(drD("BillingDate")).ToString("yyyy/MM/dd"))
                strRow = strRow.Replace("{DueDate}", Convert.ToDateTime(drD("DueDate")).ToString("yyyy/MM/dd"))
                strRow = strRow.Replace("{TotalAmount}", FormatNumber(Convert.ToDouble(drD("TotalAmount")), 2, TriState.UseDefault, TriState.UseDefault, TriState.True))

                strDetail.AppendLine(strRow)
                iNo += 1

            Next

            'fill Detail Value
            srEmailTemplate = srEmailTemplate.Replace("{DealerRow}", strDetail.ToString())


            'fill Footer Value
            srEmailTemplate = srEmailTemplate.Replace("{Tanggal}", DateTime.Now.ToString("dd MMMM yyyy"))

            'Send Email
            Dim strBCC As String = System.Configuration.ConfigurationManager.AppSettings("TOPSPEnhancementEmailBCC").ToString()
            Dim strCC As String = System.Configuration.ConfigurationManager.AppSettings("TOPSPEnhancementEmailCC").ToString()
            Dim strTo As String = ""

            Me.SetEmailRecipient(strTo, strCC, drC("DealerCode").ToString())
            Me.SendEmail(strTo, strCC, strBCC, "Reminder Pelunasan TOP Spare Part", srEmailTemplate)
        Next
    End Sub

    Private Sub SelesaiEmail(d As String)
        Dim dt As DateTime = DateTime.Now
        Dim ds As New DataSet
        Dim nF As New SparePartDueDateNotificationFacade(User)
        ds = nF.GetNotifForDueDate(dt)

        Const htmlRow As String = " <tr> <td width='40' align='RIGHT'>{No}</td> <td width='150' align='LEFT'>{DealerCode}</td> <td width='150' align='LEFT'>{BillingNumber}</td>  <td width='150' align='center'>{BillingDate}</td> <td width='150' align='center'>{DueDate}</td> <td width='100' align='RIGHT'>{TotalAmount}</td> </tr>"

        If Not (Not IsNothing(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0) Then
            LogHelper.WriteLog(String.Format("executeJob {0}   \n ID {1} \n No data...", Me.ModuleName, d))
            Exit Sub
        End If

        Dim Dir As String
        Dim strConsHeader As String

        Dir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)
        Dir = Dir + "\EmailTemplate\SparePartDueDate.htm"
        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(Dir)
        strConsHeader = sr.ReadToEnd()
        sr.Close()


        'Group Per Credit Account


        Dim drCR As DataTable = ds.Tables(0).DefaultView.ToTable(True, "CreditAccount")

        For Each drC As DataRow In drCR.Rows
            Dim objDealer As Dealer
            objDealer = New DealerFacade(User).GetDealerByCreditAccount(drC("CreditAccount").ToString())

            Dim srEmailTemplate As String = strConsHeader
            Dim strDetail As New StringBuilder

            'fill Header Value
            srEmailTemplate = srEmailTemplate.Replace("{CreditAccount}", drC("CreditAccount").ToString())
            srEmailTemplate = srEmailTemplate.Replace("{CreditName}", objDealer.DealerName)
            Try
                srEmailTemplate = srEmailTemplate.Replace("{CreditCity}", objDealer.City.CityName)
            Catch ex As Exception

            End Try

            'Build Detail Value
            Dim drDealer = ds.Tables(0).Select(String.Format("CreditAccount='{0}'", drC("CreditAccount").ToString()), "DealerCode ASC")
            Dim iNo As Integer = 1

            For Each drD As DataRow In drDealer
                Dim strRow As String = htmlRow
                strRow = strRow.Replace("{No}", iNo.ToString())
                strRow = strRow.Replace("{BillingNumber}", drD("BillingNumber"))
                strRow = strRow.Replace("{DealerCode}", drD("DealerCode"))
                'strRow = strRow.Replace("{Factoring}", IIf(Convert.ToBoolean(drD("Factoring")), "Factoring", "TOP"))
                strRow = strRow.Replace("{BillingDate}", Convert.ToDateTime(drD("BillingDate")).ToString("yyyy/MM/dd"))
                strRow = strRow.Replace("{DueDate}", Convert.ToDateTime(drD("DueDate")).ToString("yyyy/MM/dd"))
                strRow = strRow.Replace("{TotalAmount}", FormatNumber(Convert.ToDouble(drD("TotalAmount")), 2, TriState.UseDefault, TriState.UseDefault, TriState.True))

                strDetail.AppendLine(strRow)
                iNo += 1

            Next

            'fill Detail Value
            srEmailTemplate = srEmailTemplate.Replace("{DealerRow}", strDetail.ToString())


            'fill Footer Value
            srEmailTemplate = srEmailTemplate.Replace("{Tanggal}", DateTime.Now.ToString("dd MMMM yyyy"))

            'Send Email
            Dim strBCC As String = System.Configuration.ConfigurationManager.AppSettings("TOPSPEnhancementEmailBCC").ToString()
            Dim strCC As String = System.Configuration.ConfigurationManager.AppSettings("TOPSPEnhancementEmailCC").ToString()
            Dim strTo As String = ""

            Me.SetEmailRecipient(strTo, strCC, drC("CreditAccount").ToString())
            Me.SendEmail(strTo, strCC, strBCC, "Spare Part Payment Clear", srEmailTemplate)
        Next
    End Sub

End Class
