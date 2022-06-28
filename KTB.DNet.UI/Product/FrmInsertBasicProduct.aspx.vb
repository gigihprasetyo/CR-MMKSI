#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.Product
Imports KTB.DNet.Domain
#End Region

Public Class FrmInsertBasicProduct
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents txt_BasicProductCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txt_BasicProductName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txt_BasicProductDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents btn_Save As System.Web.UI.WebControls.Button
    Protected WithEvents btn_Update As System.Web.UI.WebControls.Button
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents txt_BasicProductID As System.Web.UI.WebControls.TextBox
    Protected WithEvents btn_Delete As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Sub"

    Sub ObjectToTextBoxesEntry()

    End Sub

    Sub TextBoxesToObjectEntry(ByVal objBasicProduct As BasicProduct)
        objBasicProduct.BasicProductCode = txt_BasicProductCode.Text
        objBasicProduct.BasicProductName = txt_BasicProductName.Text
        objBasicProduct.Description = txt_BasicProductDescription.Text
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

    Private Sub btn_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save.Click
        Dim objBasicProductFacade As New BasicProductFacade(User)
        Dim objBasicProduct As New BasicProduct

        TextBoxesToObjectEntry(objBasicProduct)
        objBasicProductFacade.Insert(objBasicProduct)

    End Sub

    Private Sub btn_Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Update.Click
        Dim objBasicProductFacade As New BasicProductFacade(User)
        Dim objBasicProduct As New BasicProduct

        objBasicProduct.ID = txt_BasicProductID.Text
        TextBoxesToObjectEntry(objBasicProduct)

        objBasicProductFacade.Update(objBasicProduct)

    End Sub

    Private Sub btn_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Delete.Click
        Dim objBasicProductFacade As New BasicProductFacade(User)
        Dim objBasicProduct As New BasicProduct

        objBasicProduct.ID() = txt_BasicProductID.Text
        objBasicProductFacade.DeleteFromDB(objBasicProduct)
    End Sub
End Class