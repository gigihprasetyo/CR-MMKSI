Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions
Public Class PopUpInputCatatan
    Inherits System.Web.UI.Page
    Private _sessHelper As SessionHelper = New SessionHelper
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents LblNoReg As System.Web.UI.WebControls.Label
    Protected WithEvents LblNamaSiswa As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtCatatan As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnClose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()

    End Sub

#End Region

    Private ReadOnly Property AreaId As String
        Get
            Return Request.QueryString("area")
        End Get
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("id")) Then

                Dim objtrClassReg As TrClassRegistration = New TrClassRegistrationFacade(User).Retrieve( _
                    CInt(Request.QueryString("id")))
                _sessHelper.SetSession("sessObjTrClassRegistration", objtrClassReg)

                If AreaId.IsNullorEmpty Then
                    LblNoReg.Text = objtrClassReg.TrTrainee.ID
                Else
                    If AreaId.Equals("1") Or AreaId.Equals("3") Then
                        LblNoReg.Text = objtrClassReg.TrTrainee.ListTrTraineeSalesmanHeader.FirstOrDefault(Function(x) _
                                        x.JobPositionAreaID = CInt(AreaId)).SalesmanHeader.SalesmanCode
                    Else
                        LblNoReg.Text = objtrClassReg.TrTrainee.ID
                    End If
                End If

                LblNamaSiswa.Text = objtrClassReg.TrTrainee.Name
                txtCatatan.Text = objtrClassReg.Notes
            End If

        End If

    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim nResult = -1
        If txtCatatan.IsNotEmpty Then
            Dim objtrClassReg As TrClassRegistration = CType(_sessHelper.GetSession("sessObjTrClassRegistration"), TrClassRegistration)
            objtrClassReg.Notes = txtCatatan.Text
            nResult = New TrClassRegistrationFacade(User).Update(objtrClassReg)
            If nResult <> -1 Then
                MessageBox.Show(SR.SaveSuccess)
                'Refresh()
                _sessHelper.SetSession("SessIsUpdateCatatan", True)
                UpdateClassRegColl(objtrClassReg.ID, txtCatatan.Text)
                RegisterStartupScript("CloseWindow", "<script languange=javascript>window.close()</script>")
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        End If

    End Sub

    Private Sub UpdateClassRegColl(ByVal ClassRegID As Integer, ByVal Notes As String)
        Dim arlRegistration As ArrayList = CType(Session.Item("SessClassRegColl"), ArrayList)
        If arlRegistration.Count > 0 Then
            For Each objRegistration As TrClassRegistration In arlRegistration
                If objRegistration.ID = ClassRegID Then
                    objRegistration.Notes = Notes
                    Exit Sub
                End If
            Next
        End If
    End Sub

    Private Sub Refresh()
        Dim objtrClassReg As TrClassRegistration = New TrClassRegistrationFacade(User).Retrieve(CType(LblNoReg.Text, Integer))
        If Not IsNothing(objtrClassReg) Then
            LblNamaSiswa.Text = objtrClassReg.TrTrainee.Name
            txtCatatan.Text = objtrClassReg.Notes
        End If
    End Sub
End Class
