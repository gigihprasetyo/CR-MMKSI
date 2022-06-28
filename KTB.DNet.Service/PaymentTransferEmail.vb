Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web
Imports System.Web.Mail
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.Linq
Imports System.Data


Imports KTB.DNet.BusinessFacade.IndentPartEquipment
Imports KTB.DNet.BusinessFacade.IndentPart
Imports KTB.DNet.BusinessFacade.Sparepart
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
Imports KTB.DNet.BusinessFacade


Public Class PaymentTransferEmail
    Inherits Job

    Protected Overrides Function executeJob() As Boolean

        LogHelper.WriteLog("executeJob PaymentTransferEmail - start...")
        If 1 = 1 Then
            Try
                Dim TOP_1_time As String = KTB.DNet.Lib.WebConfig.GetValue("PaymentTransferEmail_TOP_1_Time")
                Dim COD_1_time As String = KTB.DNet.Lib.WebConfig.GetValue("PaymentTransferEmail_COD_1_Time")
                Dim TOPCOD_time As String = KTB.DNet.Lib.WebConfig.GetValue("PaymentTransferEmail_TOP_COD_Time")

                ''PaymentTransferNotfiticationEmail(iDatatType,iPaymentType, -1)
                ''iDatatType = Kind of Data
                ''1 = Notification TOP-COD
                ''2 = PaymentTransfer created not in TransferCeiling
                ''3 = 

                ''iPaymentType : enumPaymentType.PaymentType.TOP
                ''enumPaymentType.PaymentType.TOP
                ''enumPaymentType.PaymentType.COD

                ''iDays : Due date
                'Dim n As Integer = Math.Abs(DateDiff(DateInterval.Second, Me.ScheduleDate, Me.CurrentTime))
                Dim dt As DateTime = New DateTime(Now.Year, Now.Month, Now.Day, GetTime(Me.CurrentTime).Hour, GetTime(Me.CurrentTime).Minute, 0)
                If dt.ToString("HH:mm") = TOP_1_time Then
                    PaymentTransferNotfiticationEmail1(1, enumPaymentType.PaymentType.TOP, 1)
                End If

                If dt.ToString("HH:mm") = COD_1_time Then
                    PaymentTransferNotfiticationEmail1(1, enumPaymentType.PaymentType.COD, 1)
                End If

                If dt.ToString("HH:mm") = TOPCOD_time Then
                    PaymentTransferNotfiticationEmail1(1, enumPaymentType.PaymentType.TOP, 0)
                    PaymentTransferNotfiticationEmail1(1, enumPaymentType.PaymentType.COD, 0)
                End If

                'PaymentTransferNotfiticationEmail2(2, enumPaymentType.PaymentType.COD, 0)
                '   PaymentTransferNotfiticationEmail(3, enumPaymentType.PaymentType.COD, 0)

            Catch ex As Exception
                Try
                    LogHelper.WriteLog("error Payment Transer : " & ex.Message _
                        & vbCrLf & "Error StackTrace Line : " & IIf(IsNothing(ex.StackTrace), "", ex.StackTrace.ToString()) _
                        & vbCrLf & "Error InnerException Line : " & IIf(IsNothing(ex.InnerException), "", ex.InnerException.ToString()) _
                        & vbCrLf & "Error Source Line : " & IIf(IsNothing(ex.Source), "", ex.Source.ToString()))
                Catch ex2 As Exception
                    LogHelper.WriteLog("error Payment Transer : " & ex.Message)
                End Try



            End Try

        End If

    End Function

    Private Sub PaymentTransferNotfiticationEmailOld(ByVal iDatatType As Integer, ByVal iPaymentType As Integer, ByVal iDays As Integer)
        Dim sTO As String = ""
        Dim sCC As String = ""
        Dim subject As String = "[MMKSI-DNet] Sales - Payment Transfer Notification"
        Dim Dir As String = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)
        Try
            Dim objFacade As sp_PaymentTransferFacade = New sp_PaymentTransferFacade(User)
            Dim arlPT As ArrayList
            arlPT = objFacade.RetrieveFromSP(iDatatType, iPaymentType, iDays)
            'iDatatType()
            '1 = Notification TOP-COD
            '2 = PaymentTransfer created not in TransferCeiling
            '3 = 
            Dir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)
            If arlPT.Count > 0 Then
                Select Case iDatatType
                    Case 1
                        subject = subject + " - Tanggal Jatuh Tempo"
                        Dir = Dir + "\EmailTemplate\PaymentTransferEmailDueDate.htm"
                    Case 2
                        subject = subject + " - Daftar Konfirmasi Pembayaran Yang Belum Berhasil"
                        Dir = Dir + "\EmailTemplate\PaymentTransferFailed.htm"
                    Case 3
                        subject = subject + ""
                        Dir = Dir + "\EmailTemplate\PaymentTransferFailed.htm"
                End Select

                If (arlPT.Count > 0) Then
                    Dim i As Integer = 0
                    For Each oPT As sp_PaymentTransfer In arlPT
                        sTO = ""
                        sCC = ""

                        Dim str = New StringBuilder

                        str.Append("<tr><td align='center'>" & (i + 1).ToString() & "</td><td align='center'>" & oPT.SONumber & "</td><td align='right'>" & FormatNumber(oPT.TotalVH, 0).ToString & "</td><td align='right'>" & FormatNumber(oPT.TotalPP, 0).ToString & "</td><td align='right'>" & FormatNumber(oPT.TotalIT, 0).ToString & "</td></tr>")

                        Dim sContents() As String = {oPT.DealerCode, oPT.DealerName, oPT.CityName, oPT.DueDate.ToString("dd MMMM yyyy"), str.ToString(), Now.ToString("dd MMMM yyyy")}

                        Me.SetEmailRecipient(sTO, sCC, oPT.DealerID)

                        Me.SendEmail(Dir, sTO, sCC, subject, sContents)
                    Next

                End If

                LogHelper.WriteLog("Send Email Payment Transfer Success")
            End If


        Catch ex As Exception
            LogHelper.WriteLog("Send Email Payment Transfer Failed ! Error : " + ex.Message)
        End Try

    End Sub

    Private Sub PaymentTransferNotfiticationEmail(ByVal iDatatType As Integer, ByVal iPaymentType As Integer, ByVal iDays As Integer)
        Dim sTO As String = ""
        Dim sCC As String = ""
        Dim subject As String = "[MMKSI-DNet] Sales - Payment Transfer Notification - " & enumPaymentType.GetStringValue(iDatatType) & "]"
        Dim Dir As String = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)
        Try
            
            Dim objFacadeDealer As sp_PaymentTransferFacade = New sp_PaymentTransferFacade(User)
            Dim arlDealer As ArrayList
            arlDealer = objFacadeDealer.RetrieveFromSP(iDatatType, iPaymentType, iDays)

            If arlDealer.Count > 0 Then
                For Each objDealer As sp_PaymentTransfer In arlDealer

                    Dim objFacade As sp_PaymentTransferFacade = New sp_PaymentTransferFacade(User)
                    Dir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)

                    Dim arlPT As ArrayList
                    arlPT = objFacade.RetrieveFromSPByDealer(iDatatType, iPaymentType, iDays, objDealer.DealerID)

                    'iDatatType()
                    '1 = Notification TOP-COD
                    '2 = PaymentTransfer created not in TransferCeiling
                    '3 = 

                    If (arlPT.Count > 0 AndAlso iDatatType = 2) OrElse iDatatType = 1 Then
                        Select Case iDatatType
                            Case 1
                                subject = subject + " - Tanggal Jatuh Tempo"
                                Dir = Dir + "\EmailTemplate\PaymentTransferEmailDueDate.htm"
                            Case 2
                                subject = subject + " - Daftar Konfirmasi Pembayaran Yang Belum Berhasil"
                                Dir = Dir + "\EmailTemplate\PaymentTransferFailed.htm"
                            Case 3
                                subject = subject + ""
                                Dir = Dir + "\EmailTemplate\PaymentTransferEmailDueDate.htm"
                        End Select

                        sTO = ""
                        sCC = ""

                        Dim str = New StringBuilder
                        Dim i As Integer = 0
                        Select Case iDatatType
                            Case 1
                                For Each oPT As sp_PaymentTransfer In arlPT
                                    i = i + 1
                                    str.Append("<tr><td align='center'>" & i.ToString() & "</td><td align='center'>" & oPT.SONumber & "</td><td align='right'>" & FormatNumber(oPT.TotalVH, 0).ToString & "</td><td align='right'>" & FormatNumber(oPT.TotalPP, 0).ToString & "</td><td align='right'>" & FormatNumber(oPT.TotalIT, 0).ToString & "</td></tr>")
                                Next

                            Case 2
                                For Each oPT As sp_PaymentTransfer In arlPT
                                    i = i + 1
                                    str.Append("<tr><td align='center'>" & i.ToString() & "</td><td align='center'>" & oPT.SONumber & "</td><td align='right'>" & FormatNumber(oPT.TotalVH, 0).ToString & "</td></tr>")
                                Next

                            Case 3
                                For Each oPT As sp_PaymentTransfer In arlPT
                                    i = i + 1
                                    str.Append("<tr><td align='center'>" & i.ToString() & "</td><td align='center'>" & oPT.SONumber & "</td><td align='right'>" & FormatNumber(oPT.TotalVH, 0).ToString & "</td><td align='right'>" & FormatNumber(oPT.TotalPP, 0).ToString & "</td><td align='right'>" & FormatNumber(oPT.TotalIT, 0).ToString & "</td></tr>")
                                Next

                        End Select

                        Dim sContents() As String = {objDealer.DealerCode, objDealer.DealerName, objDealer.CityName, Date.Now.AddDays(iDays).ToString("dd MMMM yyyy"), str.ToString(), Now.ToString("dd MMMM yyyy")}

                        Me.SetEmailRecipient(sTO, sCC, objDealer.DealerID)

                        If (sTO <> "" Or sCC <> "") Then
                            Me.SendEmail(Dir, sTO, sCC, subject, sContents)
                        End If

                        LogHelper.WriteLog("Send Email Payment Transfer Success")
                    End If
                Next
            End If

        Catch ex As Exception
            LogHelper.WriteLog("Send Email Payment Transfer Failed ! Error : " + ex.Message)
        End Try

    End Sub

    Private Sub PaymentTransferNotfiticationEmail2(ByVal iDatatType As Integer, ByVal iPaymentType As Integer, ByVal iDays As Integer)
        Dim sTO As String = ""
        Dim sCC As String = ""
        Dim subject As String = "[MMKSI-Dnet] Sales - Payment Transfer Notification - " & enumPaymentType.GetStringValue(iDatatType) & "]"
        Dim Dir As String = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)
        Try

            Dim objFacadeDealer As sp_PaymentTransferFacade = New sp_PaymentTransferFacade(User)
            Dim arlDealer As ArrayList
            arlDealer = objFacadeDealer.RetrieveFromSP(iDatatType, iPaymentType, iDays)

            If arlDealer.Count > 0 Then
                For Each objDealer As sp_PaymentTransfer In arlDealer

                    Dim objFacade As sp_PaymentTransferFacade = New sp_PaymentTransferFacade(User)
                    Dir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)

                    Dim arlPT As ArrayList
                    arlPT = objFacade.RetrieveFromSPByDealer(iDatatType, iPaymentType, iDays, objDealer.DealerID)

                    'iDatatType()
                    '1 = Notification TOP-COD
                    '2 = PaymentTransfer created not in TransferCeiling
                    '3 = 

                    If (arlPT.Count > 0 AndAlso iDatatType = 2) OrElse iDatatType = 1 Then
                        Select Case iDatatType
                            Case 1
                                subject = subject + " - Tanggal Jatuh Tempo"
                                Dir = Dir + "\EmailTemplate\PaymentTransferEmailDueDate.htm"
                            Case 2
                                subject = subject + " - Daftar Konfirmasi Pembayaran Yang Belum Berhasil"
                                Dir = Dir + "\EmailTemplate\PaymentTransferFailed.htm"
                            Case 3
                                subject = subject + ""
                                Dir = Dir + "\EmailTemplate\PaymentTransferEmailDueDate.htm"
                        End Select

                        sTO = ""
                        sCC = ""

                        Dim str = New StringBuilder
                        Dim i As Integer = 0
                        Select Case iDatatType
                            Case 1
                                For Each oPT As sp_PaymentTransfer In arlPT
                                    i = i + 1
                                    str.Append("<tr><td align='center'>" & i.ToString() & "</td><td align='center'>" & oPT.SONumber & "</td><td align='right'>" & FormatNumber(oPT.TotalVH, 0).ToString & "</td><td align='right'>" & FormatNumber(oPT.TotalPP, 0).ToString & "</td><td align='right'>" & FormatNumber(oPT.TotalIT, 0).ToString & "</td></tr>")
                                Next

                            Case 2
                                For Each oPT As sp_PaymentTransfer In arlPT
                                    i = i + 1
                                    str.Append("<tr><td align='center'>" & i.ToString() & "</td><td align='center'>" & oPT.SONumber & "</td><td align='right'>" & FormatNumber(oPT.TotalVH, 0).ToString & "</td></tr>")
                                Next

                            Case 3
                                For Each oPT As sp_PaymentTransfer In arlPT
                                    i = i + 1
                                    str.Append("<tr><td align='center'>" & i.ToString() & "</td><td align='center'>" & oPT.SONumber & "</td><td align='right'>" & FormatNumber(oPT.TotalVH, 0).ToString & "</td><td align='right'>" & FormatNumber(oPT.TotalPP, 0).ToString & "</td><td align='right'>" & FormatNumber(oPT.TotalIT, 0).ToString & "</td></tr>")
                                Next

                        End Select

                        Dim sContents() As String = {objDealer.DealerCode, objDealer.DealerName, objDealer.CityName, Date.Now.AddDays(iDays).ToString("dd MMMM yyyy"), str.ToString(), Now.ToString("dd MMMM yyyy")}

                        Me.SetEmailRecipient(sTO, sCC, objDealer.DealerID)

                        If (sTO <> "" Or sCC <> "") Then
                            Me.SendEmail(Dir, sTO, sCC, subject, sContents)
                        End If

                        LogHelper.WriteLog("Send Email Payment Transfer Success")
                    End If
                Next
            End If

        Catch ex As Exception
            LogHelper.WriteLog("Send Email Payment Transfer Failed ! Error : " + ex.Message)
        End Try

    End Sub


    ''' <summary>
    ''' (H-1/) tanggal efektif pembayaran - TOP/COD
    ''' </summary>
    ''' <param name="iDatatType"></param>
    ''' <param name="iPaymentType"></param>
    ''' <param name="iDays"></param>
    ''' <remarks></remarks>
    Private Sub PaymentTransferNotfiticationEmail1(ByVal iDatatType As Integer, ByVal iPaymentType As Integer, ByVal iDays As Integer)
        Dim sTO As String = ""
        Dim sCC As String = ""
        Dim subject As String = "[MMKSI-DNet] Sales - Payment Transfer Notification - " & enumPaymentType.GetStringValue(iPaymentType) & "]"
        Dim Dir As String = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)
        Try

            subject = subject + " - Tanggal Jatuh Tempo"
            Dir = Dir + "\EmailTemplate\PaymentTransferEmailDueDate.htm"

            Dim objFacadeDealer As sp_PaymentTransferFacade = New sp_PaymentTransferFacade(User)
            Dim arlDealer As ArrayList = New ArrayList
            'Retrieve Data
            arlDealer = objFacadeDealer.RetrieveFromSP(iDatatType, iPaymentType, iDays)
            If arlDealer.Count = 0 Then
                LogHelper.WriteLog(String.Format("No Data : {0} , {1}, {2}", iDatatType, iPaymentType, iDays))
                Return
            End If
            'Grouping Data per Dealer
            Dim ObjArTDealer = From ob As sp_PaymentTransfer In arlDealer
                            Group By ob.CreditAccount Into Group
                            Select CreditAccount

            '/X/Select Data Per Dealer
            '/X/Select Data Per CreditAccount
            For Each ObjDealerCreditAccount As String In ObjArTDealer
                'Filter Data Per Dealer
                Dim objDealer As Dealer
                objDealer = New DealerFacade(User).GetDealerByCreditAccount(ObjDealerCreditAccount)
                Dim _arrPayment = From ob As sp_PaymentTransfer In arlDealer
                           Where ob.CreditAccount = ObjDealerCreditAccount
                           Select ob

                sTO = ""
                sCC = ""

                Dim str = New StringBuilder
                Dim i As Integer = 0

                For Each oPT As sp_PaymentTransfer In _arrPayment
                    i = i + 1
                    str.Append("<tr><td align='center'>" & i.ToString() & "</td><td align='center'>" & oPT.DealerCode & "</td><td align='center'>" & oPT.SONumber & "</td><td align='center'>" & oPT.PaymentPurposeCode & "</td><td align='right'>" & FormatNumber(oPT.TotalAmount, 0).ToString & "</td></tr>")
                Next

                Dim sContents() As String = {objDealer.DealerCode, objDealer.DealerName, objDealer.City.CityName, Date.Now.AddDays(iDays).ToString("dd MMMM yyyy"), str.ToString(), Now.ToString("dd MMMM yyyy")}
                sCC = KTB.DNet.Lib.WebConfig.GetValue("PaymentTransferEmailCC")
                Me.SetEmailRecipientBYCA(sTO, sCC, objDealer.CreditAccount)

                If (sTO <> "" Or sCC <> "") Then
                    Me.SendEmail(Dir, sTO, sCC, subject, sContents)
                End If

                LogHelper.WriteLog("Send Email Payment Transfer Success")

            Next

        Catch ex As Exception
            LogHelper.WriteLog("Send Email Payment Transfer Failed ! Error : " + ex.Message)
        End Try

    End Sub


    Protected Overrides ReadOnly Property ModuleName() As String
        Get
            Return Me.GetType.Name
        End Get
    End Property



    Private Sub SetEmailRecipientBYCA(ByRef sTo As String, ByRef sCC As String, ByVal objDealerCreditAccount As String)

        Dim objEUFac As PaymentNotifEmailFacade = New PaymentNotifEmailFacade(User)
        Dim crtEU As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentNotifEmail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtEU.opAnd(New Criteria(GetType(PaymentNotifEmail), "Dealer.CreditAccount", MatchType.Exact, objDealerCreditAccount))
        'crtEU.opAnd(New Criteria(GetType(PaymentNotifEmail), "GroupType", MatchType.Exact, CType(GroupType, Short)))

        Dim arlEU As New ArrayList
        arlEU = objEUFac.Retrieve(crtEU)
        If arlEU.Count > 0 Then sTo = ""
        For Each oEU As PaymentNotifEmail In arlEU
            If oEU.ReceiverType = 1 Then
                sTo &= IIf(sTo.Trim = "", "", ";") & oEU.Email
            Else
                sCC &= IIf(sCC.Trim = "", "", ";") & oEU.Email
            End If
        Next

    End Sub


    Private Function SetEmailRecipient(ByRef sTo As String, ByRef sCC As String, ByRef objDealerID As Integer) As String

        Dim objEUFac As PaymentNotifEmailFacade = New PaymentNotifEmailFacade(User)
        Dim crtEU As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentNotifEmail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtEU.opAnd(New Criteria(GetType(PaymentNotifEmail), "Dealer.ID", MatchType.Exact, objDealerID))
        'crtEU.opAnd(New Criteria(GetType(PaymentNotifEmail), "GroupType", MatchType.Exact, CType(GroupType, Short)))

        Dim arlEU As New ArrayList
        arlEU = objEUFac.Retrieve(crtEU)
        If arlEU.Count > 0 Then sTo = ""
        For Each oEU As PaymentNotifEmail In arlEU
            If oEU.ReceiverType = 1 Then
                sTo &= IIf(sTo.Trim = "", "", ";") & oEU.Email
            Else
                sCC &= IIf(sCC.Trim = "", "", ";") & oEU.Email
            End If
        Next

    End Function

    Private Function GetTime(ByVal pDt As DateTime) As DateTime

        Dim nMinute As Integer = pDt.Minute

        'For I As Integer = 0 To 60 Step nDiff
        '    If pDt.Minute > 0 Then

        '    End If
        'Next

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
End Class
