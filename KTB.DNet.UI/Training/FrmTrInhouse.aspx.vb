Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports System.IO
Imports System.Globalization


Public Class FrmTrInhouse
    Inherits System.Web.UI.Page

#Region "Private Variables"

    'Dim LocalStorage As String
    Protected WithEvents ufAttendance As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents ufEvaluation As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents ufReport As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents lblFileEvaluation As System.Web.UI.WebControls.Label
    Protected WithEvents lblFileReport As System.Web.UI.WebControls.Label
    Protected WithEvents lblFileAttendance As System.Web.UI.WebControls.Label
    Protected WithEvents btnDownloadAttendance As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownloadEvaluation As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownloadReport As System.Web.UI.WebControls.Button
    Protected WithEvents txtPosition1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnUploadAttendance As System.Web.UI.WebControls.Button
    Protected WithEvents btnUploadEvaluation As System.Web.UI.WebControls.Button
    Protected WithEvents btnUploadReport As System.Web.UI.WebControls.Button
    Protected WithEvents dtgMember As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtClassCodeE As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtClassCode As System.Web.UI.WebControls.TextBox

#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtCourseCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCourseName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDescCourse As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkRequireWorkDate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator

    Protected WithEvents txtBtsLulus As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RangeValidator1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtNotes As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkRoom As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkBoard As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkDesk As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkComputer As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkOHP As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkSoftDNet As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkHard As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkSoftExt As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents icReportDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtApproval1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtApproval2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtApproval3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPosition2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPosition3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgInformation As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnAdd As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents chkChalk As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopUpTraining As System.Web.UI.WebControls.Label
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

End Class

