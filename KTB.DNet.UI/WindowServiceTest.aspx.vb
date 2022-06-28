Imports KTB.DNet.Parser
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart

Imports System.io
Imports System.Security
Imports System.Security.Permissions
Imports System.Net
Imports System.Threading
Imports Scripting
Imports System.Security.Principal

Public Class WindowServiceTest
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents Button2 As System.Web.UI.WebControls.Button
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Button3 As System.Web.UI.WebControls.Button
    Protected WithEvents Button4 As System.Web.UI.WebControls.Button
    Protected WithEvents DF1 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents DF2 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents DF3 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents DF4 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents Button5 As System.Web.UI.WebControls.Button
    Protected WithEvents Button6 As System.Web.UI.WebControls.Button
    Protected WithEvents fWSCUpdate As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnWSCStatus As System.Web.UI.WebControls.Button
    Protected WithEvents btnInvoiceSync As System.Web.UI.WebControls.Button
    Protected WithEvents fbInvoiceSync As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnDepositSync As System.Web.UI.WebControls.Button
    Protected WithEvents File1 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents fileDepositSync As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents fInvoiceSync As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnDepositC2Sync As System.Web.UI.WebControls.Button
    Protected WithEvents File2 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents fileDepositC2Sync As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnUpSpecialItem As System.Web.UI.WebControls.Button
    Protected WithEvents fUpSpecialItem As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents fDepoC2 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents fUpBillingSPPO As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnUploadBillingSPPO As System.Web.UI.WebControls.Button
    Protected WithEvents Button7 As System.Web.UI.WebControls.Button
    Protected WithEvents fUpLabor As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents fUpBillingSPPOs As System.Web.UI.HtmlControls.HtmlInputFile

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
        Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim prsSP As IParser
        Dim sp As New SparePartMasterParser
        prsSP = sp
        prsSP.ParseWithTransaction(DF1.PostedFile.FileName, "System")

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim parser As IParser
        Dim ppParser As New SPPOEEstimateParser
        parser = ppParser
        parser.ParseWithTransaction(DF3.PostedFile.FileName, "system")
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim parser As IParser
        Dim ppParser As New SPPOChecklistParser
        parser = ppParser
        parser.ParseWithTransaction(DF2.PostedFile.FileName, "system")
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim parser As IParser
        Dim ppParser As New SPPOStatusParser
        parser = ppParser
        parser.ParseWithTransaction(DF4.PostedFile.FileName, "system")
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        MessageBox.Show(SR.Raw)
    End Sub

    Private Sub btnWSCStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWSCStatus.Click
        Dim parser As IParser
        Dim ppParser As New WSCSStatusParser
        parser = ppParser
        parser.ParseWithTransaction(fWSCUpdate.PostedFile.FileName, "system")
    End Sub

    Private Sub btnInvoiceSync_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInvoiceSync.Click
        Dim parser As IParser
        parser = New InvoiceParser
        parser.ParseWithTransaction(fInvoiceSync.PostedFile.FileName, "system")
    End Sub

    Private Sub btnDepositSync_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDepositSync.Click
        Dim parser As IParser
        parser = New DepositParser
        parser.ParseWithTransaction(fileDepositSync.PostedFile.FileName, "system")
    End Sub

    Private Sub btnDepositC2Sync_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDepositC2Sync.Click
        Dim parser As IParser
        parser = New DepositC2Parser
        parser.ParseWithTransaction(fDepoC2.PostedFile.FileName, "system")
    End Sub

    Private Sub btnUpSpecialItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpSpecialItem.Click
        Dim parser As IExcelParser = New UploadSPSpecialItemParser
        Dim siHeader As SpecialItemHeader = CType(parser.ParseExcelNoTransaction(fUpSpecialItem.PostedFile.FileName, "[Sheet1$]", "User"), SpecialItemHeader)

        Dim SPSIFacade As New SpecialItemFacade(System.Threading.Thread.CurrentPrincipal)
        Dim Status As Integer = SPSIFacade.InsertSPSI(siHeader)
    End Sub

    Private Sub btnUploadBillingSPPO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUploadBillingSPPO.Click
        Dim parser As IParser
        parser = New SparePartPOBillingRecapParser
        parser.ParseWithTransaction(Me.fUpBillingSPPOs.PostedFile.FileName, "system")
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim parser As IParser
        parser = New LaborMasterParser
        parser.ParseWithTransaction(Me.fUpLabor.PostedFile.FileName, "system")
    End Sub
End Class
