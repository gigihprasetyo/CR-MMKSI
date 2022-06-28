#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.IndentPart
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
#End Region

Public Class PopUpIndentDescription
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents btnExit As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private _domain As Object
    Private _val As String
    Private _frm As String
    Dim ObjDetail As IndentPartDetail
    Dim fcd As IndentPartDetailFacade = New IndentPartDetailFacade(User)
    Private _sesshelper As SessionHelper = New SessionHelper

#Region "Add Privilege"
    Dim bCekKTBNotePriv As Boolean = SecurityProvider.Authorize(context.User, SR.Edit_popup_daftar_status_indent_part_Privilege)
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Dim objDealer As Dealer = _sesshelper.GetSession("DEALER")

        _val = Request.QueryString("ID")
        btnExit.Attributes.Add("onClick", "window.close()")
        If _val = "0" Then
            txtDescription.Text = "Data Detail Order Harus Disimpan Terlebih Dahulu !"
            btnSave.Enabled = False
            btnCancel.Enabled = False
        Else
            ObjDetail = fcd.Retrieve(CInt(_val))
            If Not IsPostBack Then
                If _val <> String.Empty Then
                    If ObjDetail.Description = String.Empty Then
                        txtDescription.Text = String.Empty
                    Else
                        txtDescription.Text = ObjDetail.Description
                    End If
                End If
            End If
        End If

        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            btnSave.Enabled = False
            btnCancel.Enabled = False
        Else
            If btnSave.Enabled Then btnSave.Enabled = bCekKTBNotePriv
            If btnCancel.Enabled Then btnCancel.Enabled = bCekKTBNotePriv
        End If


    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim nResult As Integer = -1
        Try

            ObjDetail.Description = txtDescription.Text
            nResult = fcd.Update(ObjDetail)
            If nResult > 0 Then
                MessageBox.Show(SR.UpdateSucces)
            Else
                MessageBox.Show(SR.UpdateFail)
            End If
        Catch ex As Exception
            MessageBox.Show("Proses Simpan Gagal!")
        End Try
    End Sub


    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If ObjDetail.Description = String.Empty Then
            txtDescription.Text = String.Empty
        Else
            txtDescription.Text = ObjDetail.Description
        End If
    End Sub


End Class
