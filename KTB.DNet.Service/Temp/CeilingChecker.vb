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


Public Class CeilingChecker
    Inherits Job


    'Private User As New System.Security.Principal.GenericPrincipal(New GenericIdentity("DNetService"), Nothing)

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

        'Automatic hourly email to certain DNET PIC for Negative Ceiling (Over Ceiling) occured
        'and log for the last n hour transactions
        If 1 = 1 Then
            Try
                Dim dtCheck As DateTime = Now
                Dim dtRestore1 As DateTime
                Dim dtRestore2 As DateTime = dtCheck

                If Me.Frequency = Library.Frequency.CustomDay Then
                    dtRestore1 = dtCheck.AddDays(-1)
                ElseIf Me.Frequency = Library.Frequency.CustomHour Then
                    dtRestore1 = dtCheck.AddHours(-1 * Me.NumOfFrequency)
                End If

                'ToDo Ali
                'Find CC list


                Dim sContents() As String = {Now.ToString("yyyyMMdd HHmmss") _
                    , str.ToString() _
                    , dtRestore1.ToString("yyyyMMdd_HH00") _
                    , dtRestore2.ToString("yyyyMMdd_HH00")}
                '0:now - datetime checking
                '1:list of credit account
                '2:first restore point date
                '3:second restore point date

                sTo = ConfigurationSettings.AppSettings("CeilingChecker1To")
                sCC = ConfigurationSettings.AppSettings("CeilingChecker1CC")
                Me.SendEmail("D:\EmailTemplate\CeilingChecker.htm", sTo, sCC, "[Over Ceiling] Daftar Credit Account yang Over Ceiling", sContents)

                LogHelper.WriteLog("Send Email CeilingChecker Success")
            Catch ex As Exception
                LogHelper.WriteLog("error : " & ex.Message)
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
