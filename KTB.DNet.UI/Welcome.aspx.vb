Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.AlertManagement
Imports KTB.DNet.Utility

Imports System.Reflection

Public Class Welcome
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblSelamatDatang As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblSeachTerm As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblAlert As System.Web.UI.WebControls.Label
    Protected WithEvents phDashboardAlertTransaction As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents phTextEffectsScript As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents phOneTimeAlert As System.Web.UI.WebControls.PlaceHolder

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim UIFacade As New UserManagement.UserInfoFacade(User)
    Dim UAFacade As New AlertManagement.UserAlertFacade(User)
    Dim AMFacade As New AlertManagement.AlertMasterFacade(User)

    Private _AlertTransactionDashboardHash As New Hashtable
    Private Function GetAlertAnnouncement(ByVal objAlert As AlertMaster) As String
        Dim strAnnouncement As String = String.Empty
        Dim strDesc As String = String.Empty
        Dim interval As Integer
        Dim iModDash As Integer
        Dim iModAlert As Integer

        If objAlert.ViaDashboardFreqType = 2 Or objAlert.ViaAlertBoxFreqType = 2 Then 'day interval
            interval = DateDiff(DateInterval.Day, Date.Now, objAlert.DateValidFrom)
        ElseIf objAlert.ViaDashboardFreqType = 3 Or objAlert.ViaAlertBoxFreqType = 3 Then  'week interval
            interval = DateDiff(DateInterval.WeekOfYear, Date.Now, objAlert.DateValidFrom)
        ElseIf objAlert.ViaDashboardFreqType = 4 Or objAlert.ViaAlertBoxFreqType = 4 Then  'month interval
            interval = DateDiff(DateInterval.Month, Date.Now, objAlert.DateValidFrom)
        ElseIf objAlert.ViaDashboardFreqType = 5 Or objAlert.ViaAlertBoxFreqType = 5 Then  'year interval
            interval = DateDiff(DateInterval.Year, Date.Now, objAlert.DateValidFrom)
        End If
        If objAlert.ViaDashboardFrequency > 0 Then
            iModDash = Math.Abs(interval Mod objAlert.ViaDashboardFrequency)
            If iModDash = 0 Then
                strDesc = GetAlertDescription(objAlert.Desc)
            End If
        End If
        If objAlert.ViaAlertBoxFrequency > 0 Then
            If strDesc = String.Empty Then
                iModAlert = Math.Abs(interval Mod objAlert.ViaAlertBoxFrequency)
                If iModAlert = 0 Then
                    strDesc = GetAlertDescription(objAlert.Desc)
                End If
            End If
        End If

        If strDesc <> String.Empty Then
            strAnnouncement = "<li>" + strDesc
        End If

        Return strAnnouncement
    End Function
    Private Function GetAlertDescription(ByVal strDesc As String) As String
        Dim strReturn As String = String.Empty

        If strDesc.Trim().StartsWith("<p>") Then
            strDesc = strDesc.Substring(3, strDesc.Length - 3)
            strDesc = strDesc.Substring(0, strDesc.Length - 4)
            strReturn = strDesc
        Else
            strReturn = strDesc
        End If

        Return strReturn
    End Function
    Private Sub GenerateDashboard()

        Dim modcat As String = Request.QueryString("modcat")
        If modcat Is Nothing Then
            modcat = "general" 'String.Empty
        End If

        Dim arlAlerts As ArrayList = RetrieveDashboardAlerts(modcat)

        Dim str As String = String.Empty
        _AlertTransactionDashboardHash.Clear()
        Dim strAnnouncementList As String = String.Empty
        For Each objAlert As AlertMaster In arlAlerts
            If CType(objAlert.AlertType, EnumAlertManagement.AlertManagementType) = EnumAlertManagement.AlertManagementType.Transactional Then
                Dim objDashboardInfo As New DashboarAlertTransactionInfo
                objDashboardInfo.ModuleName = objAlert.AlertModul.Description
                objDashboardInfo.FontEffect = CType(objAlert.FontEffect, EnumAlertManagement.TextEffects)

                Dim objEnum As Object = Activator.CreateInstance(objAlert.AlertModul.EnumAssemblyName, objAlert.AlertModul.EnumClassName).Unwrap
                Dim methodInfoRetrieveStatusList As System.Reflection.MethodInfo = objEnum.GetType().GetMethod(objAlert.AlertModul.EnumMethodToCall)
                Dim objResult As Object = methodInfoRetrieveStatusList.Invoke(objEnum, Nothing)
                Dim arrStatuses As ArrayList = CType(objResult, ArrayList)

                Dim isadd As Boolean = False
                For Each objStatus As AlertStatus In objAlert.AlertStatuss
                    Dim objStatusCount As New StatusCountInfo

                    Dim arlStatus As New ArrayList
                    arlStatus.Add(objStatus)
                    Dim arl As ArrayList = CommonFunction.RetrieveList(objAlert.AlertModul.Description.ToLower(), arlStatus, System.Threading.Thread.CurrentPrincipal)
                    'Dim objEnumItem As Object = arrStatuses.(objStatus.Status)
                    Dim objEnumItem As Object
                    For Each itemArrStatuses As Object In arrStatuses
                        Dim intStatus As Integer = itemArrStatuses.GetType().GetProperty(objAlert.AlertModul.EnumStatusIDPropertName).GetValue(itemArrStatuses, Nothing)
                        If intStatus = objStatus.Status Then
                            objEnumItem = itemArrStatuses
                        End If
                    Next


                    objStatusCount.StatusName = objEnumItem.GetType().GetProperty(objAlert.AlertModul.EnumStatusNamePropertyName).GetValue(objEnumItem, Nothing).ToString()
                    objStatusCount.Count = arl.Count

                    If objStatusCount.Count > 0 Then
                        isadd = True
                        'str += objStatusCount.StatusName + "<BR>" + objStatusCount.Count.ToString()
                        If objDashboardInfo.StatusCounts(objStatusCount.StatusName) Is Nothing Then
                            objDashboardInfo.StatusCounts.Add(objStatusCount.StatusName, objStatusCount)
                        Else
                            objDashboardInfo.StatusCounts(objStatusCount.StatusName) = objStatusCount
                        End If
                    End If
                Next
                If isadd Then
                    If _AlertTransactionDashboardHash(objAlert.AlertModul.Description) Is Nothing Then
                        _AlertTransactionDashboardHash.Add(objAlert.AlertModul.Description, objDashboardInfo)
                    Else
                        _AlertTransactionDashboardHash(objAlert.AlertModul.Description) = objDashboardInfo
                    End If
                End If
            ElseIf modcat.Length > 0 Then

                If objAlert.AlertModul.AlertCategory.Description.Trim().ToLower() = modcat.Trim().ToLower() Then
                    'remarks by anh req by rna 20100810 - for peridical announcement
                    'Dim strDesc As String = objAlert.Desc
                    '    If strDesc.Trim().StartsWith("<p>") Then
                    '        strDesc = strDesc.Substring(3, strDesc.Length - 3)
                    '        strDesc = strDesc.Substring(0, strDesc.Length - 4)
                    '    End If
                    'strAnnouncementList += "<li>" + strDesc
                    'end remarks
                    strAnnouncementList += GetAlertAnnouncement(objAlert)
                End If

            End If
        Next

        If strAnnouncementList.Length > 0 Then
            Dim strAnnouncement As String = String.Empty
            modcat = modcat.Substring(0, 1).ToUpper + modcat.Substring(1, modcat.Length - 1)

            'strAnnouncement += "<table cellSpacing=""1"" cellPadding=""6"" width=""100%"" border=""0"">"
            'strAnnouncement += "<tr vAlign=""top"">"
            'strAnnouncement += "<td width=""80%"" height=""200"">"
            strAnnouncement += String.Format("<font class=""red""><h3>Informasi Penting di Modul {0} !</h3></font>", modcat)
            strAnnouncement += "<B><ul type=""1"">"

            strAnnouncement += strAnnouncementList
            strAnnouncement += "</ul></B>"
            'strAnnouncement += "</td>"

            phOneTimeAlert.Controls.Add(New LiteralControl(strAnnouncement))

        End If

        Dim enumerator As IDictionaryEnumerator = _AlertTransactionDashboardHash.GetEnumerator()
        str = String.Empty
        Const ID_PREFIX As String = "tblAlertTrans"
        Dim counterForID As Integer = 0
        Dim strAnts As String = String.Empty
        Dim strVegas As String = String.Empty
        Dim strBlinking As String = String.Empty
        Dim currentID As String = String.Empty
        While enumerator.MoveNext
            Dim objCurrentDashboardInfo As DashboarAlertTransactionInfo = CType(enumerator.Value, DashboarAlertTransactionInfo)
            counterForID += 1

            If str.Length > 0 Then
                str += "<tr>"
                str += "<td background=""images/bg_hor.gif"" colSpan=""3"" height=""1""><IMG height=""1"" src=""images/bg_hor.gif"" border=""0""></td>"
                str += "</tr>"
            End If

            currentID = ID_PREFIX + counterForID.ToString()
            Dim strOuterRow As String = String.Format("<tr><td colspan=""3""><table cellSpacing=""0"" cellPadding=""0"" id=""{0}"">", currentID)
            strOuterRow += "<tr><td colspan=""3"" style=""height:6px;background-repeat:repeat-x""></td></tr>"

            strOuterRow += String.Format("<tr><td></td><td><table>{0}</table></td><td></td></tr>", GenerateNewModuleRow(objCurrentDashboardInfo))
            strOuterRow += "<tr><td colspan=""3"" style=""height:6px;""></td></tr></table>"

            strOuterRow += "</td></tr>"

            str += strOuterRow

            If objCurrentDashboardInfo.FontEffect = EnumAlertManagement.TextEffects.LasVegasLights Then
                strVegas += String.Format("vegas.AddTable{0};", "(document.all." + currentID + ")")
            ElseIf objCurrentDashboardInfo.FontEffect = EnumAlertManagement.TextEffects.Marching Then
                strAnts += String.Format("ant.AddTable{0};", "(document.all." + currentID + ")")
            ElseIf objCurrentDashboardInfo.FontEffect = EnumAlertManagement.TextEffects.Blinking Then
                strBlinking += String.Format("blink.AddTable{0};", "(document.all." + currentID + ")")
            End If
        End While
        If str.Length > 0 Then
            Dim strTable As String = "<table cellSpacing=""0"" cellPadding=""0"" width=""180"" border=""0"">"
            strTable += "<tr>"
            strTable += "<td align=""center"" colSpan=""3""><b><font color=""red"">CEK HARI INI</font></b></td>"
            strTable += "</tr>"

            str = strTable + str + "</table>"
            phDashboardAlertTransaction.Controls.Add(New LiteralControl(str))

            Dim strTextEffectsScript As String = String.Empty
            If strAnts.Length > 0 Then
                strTextEffectsScript += "var ant = new AntsEffect(300);"
                strTextEffectsScript += strAnts
                strTextEffectsScript += "ant.Start();"
            End If
            If strVegas.Length > 0 Then
                strTextEffectsScript += "var vegas = new LasVegasLightsEffect(300);"
                strTextEffectsScript += strVegas
                strTextEffectsScript += "vegas.Start();"
            End If
            If strBlinking.Length > 0 Then
                strTextEffectsScript += "var blink = new BlinkingEffect(300);"
                strTextEffectsScript += strBlinking
                strTextEffectsScript += "blink.Start();"
            End If

            If strTextEffectsScript.Length > 0 Then
                strTextEffectsScript = String.Format("<script language=""javascript"">{0}</script>", strTextEffectsScript)
                phTextEffectsScript.Controls.Add(New LiteralControl(strTextEffectsScript))
            End If
        End If

    End Sub


    Private Function RetrieveDashboardAlerts(ByVal moduleCategory As String) As ArrayList
        Dim _shelper As New SessionHelper
        Dim facade As New AlertMasterFacade(User)
        Dim objuserinfo As UserInfo = CType(_shelper.GetSession("loginuserinfo"), UserInfo)
        Dim arl As ArrayList = facade.RetrieveValidDashboardAlerts(objuserinfo, moduleCategory)

        Return arl
    End Function



    Private Function GenerateNewModuleRow(ByVal objDashboardInfo As DashboarAlertTransactionInfo) As String
        Dim str As String = String.Empty
        str += "<tr>"
        str += "<td height=""10""><IMG height=""10"" src=""images/blank.gif"" border=""0""></td>"
        str += "</tr>"
        str += "<tr>"
        str += String.Format("<td colSpan=""3""><b>{0}, Dengan Status:</b></td>", objDashboardInfo.ModuleName)
        str += "</tr>"

        Dim enumerator As IDictionaryEnumerator = objDashboardInfo.StatusCounts.GetEnumerator()
        While enumerator.MoveNext
            Dim objStatusInfo As StatusCountInfo = CType(enumerator.Value, StatusCountInfo)
            str += "<tr>"
            str += "<td align=""center"" width=""30""><IMG src=""images/icon_red.gif"" width=""6"" border=""0""></td>"
            str += String.Format("<td width=""100""><font color=""red"">{0}</font></td>", objStatusInfo.StatusName)
            str += String.Format("<td align=""center"" width=""50""><font color=""blue"">{0}</font></td>", objStatusInfo.Count)
            str += "</tr>"
        End While

        str += "<tr>"
        str += String.Format("<td colSpan=""3"">Mohon untuk segera diproses {0} hari ini</td>", objDashboardInfo.ModuleName)
        str += "</tr>"

        Return str
    End Function

    Private Function IsEnableAlert() As Boolean
        Return True
    End Function



    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If IsEnableAlert() Then
            GenerateDashboard()
        End If

        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
        'Dim objUserInfo As UserInfo = CType(Session("LOGINUSERINFO"), UserInfo)

        If Not objDealer Is Nothing Then
            'lblDealerName.Text = CType(New EnumDealerTittle().RetrieveTitle(objDealer.Title), EnumTitle).NameTitle & " " & objDealer.DealerName
            lblDealerName.Text = objDealer.DealerName
            'lblDealerName.Text = User.Identity.Name.Substring(6)
            lblSeachTerm.Text = objDealer.SearchTerm1 & " / " & objDealer.SearchTerm2
            lblDealerCode.Text = "(" & objDealer.DealerCode & ")"
        End If

        'If objUserInfo.UserAlerts.Count > 0 Then

        '    'Menampilkan Data
        '    Dim number As Integer = 1
        '    Dim tanda1 As Boolean = False
        '    Dim UserAlertEx As ArrayList = objUserInfo.UserAlerts

        '    For Each x As UserAlert In UserAlertEx
        '        If x.AlertMaster.AnnouncementAlertType = 0 Then
        '            If (tanda1 = False) Then
        '                lblAlert.Text = "<TABLE cellSpacing=""1"" cellPadding=""1"" width=""300"" border=""0"">"
        '                tanda1 = True
        '            End If
        '            lblAlert.Text = lblAlert.Text & "<TR><TD valign=""top"" style=""WIDTH: 10px; TEXT-ALIGN: left"">" & number & ".</TD>"
        '            lblAlert.Text = lblAlert.Text & "<TD style=""TEXT-ALIGN: left"">" & x.AlertMaster.Desc & "</TD></TR>"

        '            'menghapus data setelah ditampilkan
        '            UAFacade.Delete(x)
        '            '---

        '            number = number + 1
        '        End If
        '    Next
        '    If (tanda1 = True) Then
        '        lblAlert.Text = lblAlert.Text & "</TABLE>"
        '    End If
        '    '---
        'End If


    End Sub

End Class

Public Class DashboarAlertTransactionInfo
    Public ModuleName As String = String.Empty
    Public StatusCounts As New Hashtable
    Public ID As Integer
    Public FontEffect As EnumAlertManagement.TextEffects
End Class

Public Class StatusCountInfo
    Public StatusName As String
    Public Count As Integer
End Class