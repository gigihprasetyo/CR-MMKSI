Imports KTB.DNet.Parser
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.BusinessFacade.SparePart

Imports KTB.DNet.Utility
Imports System.io
Imports System.Security
Imports System.Security.Permissions
Imports System.Net
Imports System.Threading
Imports Scripting
Imports System.Security.Principal
Imports System.Text
 

Public Class Logout
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Button3 As System.Web.UI.WebControls.Button
    Protected WithEvents Button4 As System.Web.UI.WebControls.Button
    Protected WithEvents Button5 As System.Web.UI.WebControls.Button
    Protected WithEvents Button6 As System.Web.UI.WebControls.Button
    Protected WithEvents btnSpMaster As System.Web.UI.WebControls.Button
    Protected WithEvents btnSPPOEstimate As System.Web.UI.WebControls.Button
    Protected WithEvents btnSPPOChecklist As System.Web.UI.WebControls.Button
    Protected WithEvents Button2 As System.Web.UI.WebControls.Button
    Protected WithEvents CheckBoxList1 As System.Web.UI.WebControls.CheckBoxList
    Protected WithEvents ListBox1 As System.Web.UI.WebControls.ListBox
    Protected WithEvents DropDownList1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents DropDownList2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Button7 As System.Web.UI.WebControls.Button
    Protected WithEvents txtpwd1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtpwd2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Button8 As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ListBox1.SelectionMode = ListSelectionMode.Multiple
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim parser As IParser
        Dim vc As New ContractParser
        parser = vc
        Dim al As ArrayList = CType(parser.ParseWithTransaction("D:\Phasetwo\misc\2005122113423109fu_contract20051202113141.txt", "System"), ArrayList)
    End Sub

    Private Sub btnSPPOEstimate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSPPOEstimate.Click
        Dim parser As IParser
        Dim ppParser As New SPPOEEstimateParser
        parser = ppParser
        parser.ParseWithTransaction("D:\BSI\SyncMaster\SDG.DLR", "system")

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        'Dim dt As Date = New KTB.DNet.BusinessFacade.General.NationalHolidayFacade(User).RetrieveNextDay(Now)
        'MessageBox.Show(dt.ToString)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim subject As String = "MMKSI - Part Request WSC " & "0001"
        Dim email As DNetMail = New DNetMail(smtp)
        Dim sb As StringBuilder = New StringBuilder
        sb.Append("<html><body>")
        sb.Append("Kepada Yth : User dnet")
        sb.Append("<br>")
        sb.Append("           : Dealer Dnet")
        sb.Append("<br>")
        sb.Append("body message")
        sb.Append("<br>")
        sb.Append("PT Mitsubishi Motors Krama Yudha Sales Indonesia")
        sb.Append("<br>")
        sb.Append("Service Dept")
        sb.Append("</body></html>")

        Dim strBody As String = sb.ToString
        'email.sendMail("heru.d-net@bsi.co.id", "susanto-Dnet@bsi.co.id", "", "binarto@yahoo.com", subject, Mail.MailFormat.Html, strBody)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

       

        Dim parsersx As IParser
        Dim ppParserx As New AnnualDiscountAchievementParser
        parsersx = ppParserx
        parsersx.ParseWithTransaction("D:\PhaseTwo\misc\sp_annual.rbt", "system")

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'Dim _file As KTB.DNet.Utility.TransferFile
        '_file = New KTB.DNet.Utility.TransferFile("heru", "heru", "172.17.104.24")
        '_file.Transfer("D:\ProductionPlan.txt", "\\172.17.104.204\zdnet")
        'Dim parser As IParser
        'Dim pParser As New PurchaseOrderParser
        'parser = pParser
        'parser.ParseWithTransaction("c:\fusale.txt", "system")
        Dim imp As SAPImpersonate = New SAPImpersonate("sap", "sap", "172.17.104.24")
        If imp.Start() Then
            'Do something
            imp.StopImpersonate()
        End If
       
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        'Dim _file As KTB.DNet.Utility.TransferFile
        '_file = New KTB.DNet.Utility.TransferFile("heru", "heru", "172.17.104.204")
        '_file.Transfer("D:\ProductionPlan.txt", "\\172.17.104.204\zdnet")

    End Sub

    Private Sub btnSpMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSpMaster.Click
        'Dim prsSP As IParser
        ''Dim sp As New SparePartMasterParser
        'prsSP = sp
        'prsSP.ParseWithTransaction("D:\BSI\SyncMaster\materialDNet.txt", "System")

    End Sub

    Private Sub btnSPPOChecklist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSPPOChecklist.Click
        Dim parser As IParser = New SPPOChecklistParser
        parser.ParseWithTransaction("D:\temp\Checklist_valid.txt", "system")
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim Parser As IParser
        Dim dp As New ContractParser
        Parser = dp
        Parser.ParseWithTransaction("D:\NFS_Dnet\fu_contractsxff35790r0900valid.txt", "system")
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim a As New AnnualDiscountFacade(User)
        Dim arrlist As ArrayList = a.RetrieveDistinct()

    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        'Dim pwd1 As Byte() = DNetEncryption.Encrypt(txtpwd1.Text.Trim)
        'Dim pwd2 As Byte() = DNetEncryption.Encrypt(txtpwd2.Text.Trim)
        'MessageBox.Show(DNetEncryption.ComparePassword(pwd1, pwd2).ToString)
    End Sub
End Class
