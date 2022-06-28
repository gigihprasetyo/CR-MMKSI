Public Class FrmDealerCodeSelection
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmdExtend As System.Web.UI.WebControls.Button

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
        'BindCodeSelection()
    End Sub
    Public Sub BindCodeSelection()

    End Sub

    Private Sub cmdExtend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExtend.Click
        Dim msg1 As String
        msg1 = "<script language='javascript'>" + "window.open('FrmTemplateDealerSelection.aspx?src=txtDealerCode','CustomPopUp','scrollbars=1, Height=500, Width =595');</script>"
        'window.open('DatePicker.aspx?field=' + strField,'calendarPopup','width=250,height=190,resizable=yes');

        Page.RegisterStartupScript("warning", msg1)
    End Sub
End Class
