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
Imports KTB.DNet.BusinessFacade.SparePart
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
Imports System.Collections.Generic


Public Class SparePartScheduleChecker
    Inherits Job
    'ali

    'edit 12
    'edit lagi

    Private ErrSPO As New ArrayList
    Private OkSPO As New ArrayList
    'Private User As New System.Security.Principal.GenericPrincipal(New GenericIdentity("DNetService"), Nothing)

    Protected Overrides Function executeJob() As Boolean
        LogHelper.WriteLog("executeJob SPPO - start...")
        Dim dt As DateTime = Now
        Dim Objcrit As CriteriaComposite
        Dim ObjcritSPO As CriteriaComposite
        Dim arrSPOS As New ArrayList
        Dim str As StringBuilder = New StringBuilder
        '   Dim Objsrt As SortCollection
        ErrSPO = New ArrayList
        OkSPO = New ArrayList
        dt = New DateTime(Now.Year, Now.Month, Now.Day, GetTime(Me.CurrentTime).Hour, GetTime(Me.CurrentTime).Minute, 0)
        Try
            Dim strQuery As String = String.Format("SELECT	dbo.SparePartPOScheduleTime.SparePartPOScheduleID FROM	dbo.SparePartPOScheduleTime INNER JOIN dbo.SparePartPOSchedule ON SparePartPOScheduleTime.SparePartPOScheduleID =SparePartPOSchedule.ID AND SparePartPOSchedule.RowStatus=0  WHERE	dbo.SparePartPOScheduleTime.RowStatus = {0}	AND dbo.SparePartPOScheduleTime.Status = 1 AND dbo.SparePartPOSchedule.status= 1	AND dbo.SparePartPOScheduleTime.ScheduleTime = '{1}' GROUP BY   dbo.SparePartPOScheduleTime.SparePartPOScheduleID ", CType(DBRowStatus.Active, Short).ToString(), GetTime(DateTime.Now).ToString("yyyy/MM/dd HH:mm"))

            Objcrit = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPOSchedule), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Objcrit.opAnd(New Criteria(GetType(SparePartPOSchedule), "Status", MatchType.Exact, 1))
            Objcrit.opAnd(New Criteria(GetType(SparePartPOSchedule), "OrderDay", MatchType.Exact, GetIntWeek()))
            Objcrit.opAnd(New Criteria(GetType(SparePartPOSchedule), "ID", MatchType.InSet, "(" & strQuery & ")"))
            ' Objcrit.opAnd(New Criteria(GetType(SparePartPOScheduleTime), "ScheduleTime", MatchType.Exact, GetTime(Me.CurrentTime)))


            Dim arrSPOSTFacade As New SparePartPOScheduleFacade(User)
            arrSPOS = arrSPOSTFacade.Retrieve(Objcrit)

            If IsNothing(arrSPOS) OrElse arrSPOS.Count <= 0 Then
                LogHelper.WriteLog("executeJob SPPO - End(No data)..." & strQuery)
                Return True
            Else
                LogHelper.WriteLog("executeJob SPPO -  " & strQuery)
            End If

            'Group BY OrderType
            Dim ObjOrderType = From ob As SparePartPOSchedule In arrSPOS
                               Group By ob.OrderType Into Group
                               Select OrderType

            'Select By Order Type
            Dim strSPOSID As String = String.Empty
            Dim strQuery2 As String = String.Empty

            For Each ot As String In ObjOrderType
                Dim _arrSPOS = From ob As SparePartPOSchedule In arrSPOS
                              Where ob.OrderType = ot
                              Select ob
                strSPOSID = String.Empty
                For Each obSPOS As SparePartPOSchedule In _arrSPOS
                    If strSPOSID = String.Empty Then
                        strSPOSID = obSPOS.ID.ToString()
                    Else
                        strSPOSID = strSPOSID & "," & obSPOS.ID.ToString()
                    End If
                Next
                strQuery2 = String.Format("SELECT  y.DealerID FROM dbo.SparePartPOSchedule x INNER JOIN dbo.SparePartPOScheduleDealer y ON y.SparePartPOScheduleID = x.ID WHERE 1=1 AND y.RowStatus=0 AND x.ID IN ({0}) AND x.OrderType = '{1}' ", strSPOSID, ot)
                ObjcritSPO = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                ObjcritSPO.opAnd(New Criteria(GetType(SparePartPO), "OrderType", MatchType.Exact, ot))
                ObjcritSPO.opAnd(New Criteria(GetType(SparePartPO), "IsTransfer", MatchType.Exact, 0))
                If ot.ToUpper() = "R" Then
                    ObjcritSPO.opAnd(New Criteria(GetType(SparePartPO), "ProcessCode", MatchType.Exact, "S"))
                End If

                If ot.ToUpper() = "Z" Then
                    ObjcritSPO.opAnd(New Criteria(GetType(SparePartPO), "ProcessCode", MatchType.Exact, ""))
                End If

                ObjcritSPO.opAnd(New Criteria(GetType(SparePartPO), "Dealer.ID", MatchType.InSet, "(" & strQuery2 & ")"))

                Dim arrSPOFacade As New SparePartPOFacade(User)
                Dim arrSPO As New ArrayList
                arrSPO = arrSPOFacade.Retrieve(ObjcritSPO)

                If Not IsNothing(arrSPO) AndAlso arrSPO.Count > 0 Then


                    Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                    Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                    Dim _sapServer As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServer") '172.17.104.204
                    Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _sapServer)
                    Dim succes As Boolean = False
                    Try
                        succes = imp.Start()
                        If succes Then
                            For Each objSPO As SparePartPO In arrSPO
                                Me.CreateTextFileForKTB(objSPO)

                            Next
                        Else
                            LogHelper.WriteLog("executeJob SPPO - End(Fail to Impersonate)...")
                        End If

                        imp.StopImpersonate()
                        imp = Nothing
                    Catch ex As Exception
                        imp = Nothing
                        LogHelper.WriteLog("executeJob SPPO - End(Fail to Impersonate)...")
                    End Try





                End If
            Next

            If ErrSPO.Count > 0 OrElse OkSPO.Count > 0 Then
                Dim no2 As Integer = 1
                For Each obP As SparePartPO In ErrSPO
                    str.Append("<tr>" + "<td>" + no2.ToString() + "</td>" _
                           + "<td>" + obP.PONumber + "</td> </tr>")
                    no2 = no2 + 1
                Next
                str.Append("<tr><td> </td></tr>")

                Dim str2 As StringBuilder = New StringBuilder
                no2 = 1
                For Each obP As SparePartPO In OkSPO
                    str2.Append("<tr>" + "<td>" + no2.ToString() + "</td>" _
                           + "<td>" + obP.PONumber + "</td> </tr>")
                    no2 = no2 + 1
                Next

                Dim sTo = KTB.DNet.Lib.WebConfig.GetValue("SparePartScheduleCheckerTo")
                Dim sCC = KTB.DNet.Lib.WebConfig.GetValue("SparePartScheduleCheckerCC")
                Dim Dir As String = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)

                Dim sContents() As String
                sContents = {dt.ToString("yyyy/MM/dd HH:mm"), str.ToString(), dt.ToString("yyyy/MM/dd HH:mm"), str2.ToString()}
                Dir = Dir + "\EmailTemplate\SparePartPOSchedule.htm"
                Me.SendEmail(Dir, sTo, sCC, "[MMKSI-DNet] Parts - Report SparepartPO Scheduler", sContents)
            End If

        Catch ex As Exception
            LogHelper.WriteLog("executeJob SPPO - End(Fail)..." & ex.Message)
        End Try

        Return True

    End Function

    Protected Overrides ReadOnly Property ModuleName() As String
        Get
            Return Me.GetType.Name
        End Get
    End Property




    Private Sub CreateTextFileForKTB(ByRef ObjPO As SparePartPO)
        Dim FOLDER_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("DNetServerFolder") & ObjPO.PONumber.Substring(1, 4)
        Dim FILE_NAME As String = FOLDER_NAME + "\" + ObjPO.PONumber + IIf(ObjPO.OrderType = "Z", ".SPC", ".DAT")

        Try

            ObjPO.IsTransfer = True
            If ObjPO.OrderType.ToUpper() = "Z" Then
                ObjPO.ProcessCode = "S"
                ObjPO.SentPODate = Date.Today
            End If
            Dim nResult As Integer = New SparePartPOFacade(User).UpdateSPPOProcessCode(ObjPO)
            If nResult <> -1 Then

                CreateFolder(FOLDER_NAME)
                If File.Exists(FILE_NAME) Then
                    File.Delete(FILE_NAME)
                End If

                Dim fs As FileStream = New FileStream(FILE_NAME, FileMode.CreateNew)
                Dim w As StreamWriter = New StreamWriter(fs)
                WritePOHeaderToFile(w, ObjPO)
                WritePODetailToFile(w, ObjPO)
                w.Close()
                fs.Close()
                OkSPO.Add(ObjPO)
            End If

        Catch ex As Exception
            ErrSPO.Add(ObjPO)
            LogHelper.WriteLog("executeJob SPPO - End(Fail)..." & ObjPO.PONumber)
        End Try
    End Sub

    Private Sub CreateFolder(ByVal folderName As String)
        Dim dirInfo As DirectoryInfo = New DirectoryInfo(folderName)
        If Not dirInfo.Exists Then
            dirInfo.Create()
        End If
    End Sub

    Private Sub WritePOHeaderToFile(ByRef w As StreamWriter, ByRef objSPO As SparePartPO)
        'Dim sbSetARecord As StringBuilder = New StringBuilder
        'Dim pad As Char = " "
        'sbSetARecord.Append("T")
        'sbSetARecord.Append(ObjPO.PONumber.PadRight(15, pad))
        'sbSetARecord.Append(Left(ObjPO.Dealer.DealerName, 25).PadRight(25, pad))
        'sbSetARecord.Append(String.Format("{0:yyyyMMdd}", ObjPO.PODate))
        'sbSetARecord.Append((ObjPO.SparePartPODetails).Count.ToString.PadLeft(4, "0"))
        'If ObjPO.OrderType.ToUpper() = "Z" Then
        '    sbSetARecord.Append("0".PadLeft(22, "0"))
        'End If
        'If Not IsNothing(ObjPO.TermOfPayment) Then
        '    sbSetARecord.Append(ObjPO.TermOfPayment.TermOfPaymentCode)
        'End If
        'w.WriteLine(sbSetARecord.ToString)

        Dim sbSetARecord As StringBuilder = New StringBuilder
        Dim pad As Char = " "
        sbSetARecord.Append("T")
        sbSetARecord.Append(objSPO.PONumber.PadRight(15, pad))
        sbSetARecord.Append(Left(objSPO.Dealer.DealerName, 25).PadRight(25, pad))
        sbSetARecord.Append(String.Format("{0:yyyyMMdd}", objSPO.PODate))
        sbSetARecord.Append(objSPO.SparePartPODetails.Count.ToString.PadLeft(4, "0"))

        If objSPO.OrderType = "Z" Then
            sbSetARecord.Append("0".ToString.PadLeft(22, "0"))
            sbSetARecord.Append(String.Format("{0:yyyyMMdd}", objSPO.DeliveryDate))
            If objSPO.PickingTicket.Length > 30 Then
                sbSetARecord.Append(objSPO.PickingTicket.Substring(0, 30))
            Else
                sbSetARecord.Append(objSPO.PickingTicket.PadRight(30, pad))
            End If
        End If

        If objSPO.OrderType = "R" Or objSPO.OrderType = "I" Or objSPO.OrderType = "Z" Then
            If Not IsNothing(objSPO.TermOfPayment) Then
                sbSetARecord.Append(objSPO.TermOfPayment.TermOfPaymentCode)
            End If
        End If

        If objSPO.OrderType = "Y" Then
            sbSetARecord.Append("0".ToString.PadLeft(22, "0"))
            sbSetARecord.Append(String.Format("{0:yyyyMMdd}", objSPO.DeliveryDate))

            If objSPO.PickingTicket.Length > 30 Then
                sbSetARecord.Append(objSPO.PickingTicket.Substring(0, 30))
            Else
                sbSetARecord.Append(objSPO.PickingTicket.PadRight(30, pad))
            End If
        End If

        w.WriteLine(sbSetARecord.ToString)
    End Sub

    Private Function WritePODetailToFile(ByRef w As StreamWriter, ByRef ObjPO As SparePartPO)
        Dim _arlPODetail As New ArrayList
        _arlPODetail = ObjPO.SparePartPODetails
        Dim sbSetARecord As StringBuilder = New StringBuilder
        Dim pad As Char = " "
        Dim indek As Integer = 0
        For Each objPODetail As SparePartPODetail In _arlPODetail
            indek = indek + 1
            sbSetARecord.Remove(0, sbSetARecord.Length)
            sbSetARecord.Append("D")
            sbSetARecord.Append(objPODetail.SparePartPO.PONumber.PadRight(15, pad))
            sbSetARecord.Append(objPODetail.SparePartMaster.PartNumber.PadRight(20, pad))
            sbSetARecord.Append(objPODetail.Quantity.ToString.PadLeft(5, "0"))
            sbSetARecord.Append(String.Format("{0:yyyyMMdd}", objPODetail.SparePartPO.PODate)) '(objPODetail.SparePartPO.PODate.ToString.Format("{0:yyyyMMdd}"))
            sbSetARecord.Append(indek.ToString.PadLeft(4, "0"))
            w.WriteLine(sbSetARecord.ToString)
        Next
    End Function


    Private Function GetIntWeek() As Integer

        Select Case Date.Now.DayOfWeek
            Case DayOfWeek.Monday
                Return 0
            Case DayOfWeek.Tuesday
                Return 1
            Case DayOfWeek.Wednesday
                Return 2
            Case DayOfWeek.Thursday
                Return 3
            Case DayOfWeek.Friday
                Return 4
            Case DayOfWeek.Saturday
                Return 5
            Case DayOfWeek.Sunday
                Return 6

        End Select

        Return -1
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
