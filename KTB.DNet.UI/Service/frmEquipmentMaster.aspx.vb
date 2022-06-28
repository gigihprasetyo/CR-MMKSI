#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class frmEquipmentMaster
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents FileText As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents dgEquipmentUpload As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private EquipmentArrayList As ArrayList
    Private sessionHelper As New SessionHelper
#End Region

#Region "Custom Method"

    Private Function isValidEquipmentMaster(ByVal collEquipmentMaster As ArrayList) As Boolean
        For Each item As EquipmentMaster In collEquipmentMaster
            If Not item.ErrorMessage Is Nothing Then
                If item.ErrorMessage.Length > 0 Then
                    Return False
                End If
            End If
        Next
        Return True
    End Function

    Private Sub BindUpload()
        If (Not FileText.PostedFile Is Nothing) And (FileText.PostedFile.ContentLength > 0) Then
            Dim SrcFile As String = Path.GetFileName(FileText.PostedFile.FileName)  '-- Source file name
            Dim DestFile As String = Server.MapPath("") & "\..\DataTemp\EquipmentMaster\" & SrcFile & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond  '-- Destination file
            CheckFolderExist(DestFile)
            Try
                FileText.PostedFile.SaveAs(DestFile)
                Dim parser As IParser = New EquipmentParser
                EquipmentArrayList = CType(parser.ParseNoTransaction(DestFile, User.Identity.Name.ToString), ArrayList)
                'dgEquipmentUpload.DataSource = EquipmentArrayList
                'dgEquipmentUpload.DataBind()
                sessionHelper.SetSession("objEquipment", EquipmentArrayList)
                btnSave.Enabled = isValidEquipmentMaster(EquipmentArrayList)
            Catch Exc As Exception
                MessageBox.Show(SR.UploadFail(DestFile))
            End Try
          End If

        dgEquipmentUpload.DataSource = EquipmentArrayList
        dgEquipmentUpload.DataBind()

    End Sub

    Private Sub CheckFolderExist(ByVal fileName As String)
        Dim fiinfo As FileInfo = New FileInfo(fileName)
        If Not fiinfo.Directory.Exists Then
            fiinfo.Directory.Create()
        End If
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ActivateUserPrivilege()
        If Not IsPostBack Then

        End If
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.EquipmentMasterUploadView_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Equipment Master")
        End If
        btnSave.Visible = SecurityProvider.Authorize(Context.User, SR.EquipmentMasterUploadSave_Privilege)
    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        If FileText.PostedFile.ContentType.ToString = "text/plain" Then
            Try
                BindUpload()
            Catch ex As Exception
                MessageBox.Show(SR.UploadFail("Equipment Master"))
            End Try
        Else
            MessageBox.Show("File Text Yang Dimasukkan Salah")
        End If

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        EquipmentArrayList = sessionHelper.GetSession("objEquipment")

        For Each item As EquipmentMaster In EquipmentArrayList
            If item.ErrorMessage Is Nothing Then
                Dim objEquipmentFacade As EquipmentMasterFacade = New EquipmentMasterFacade(User)
                objEquipmentFacade.AddEquipment(item)
            End If
        Next

        MessageBox.Show(SR.SaveSuccess)
        sessionHelper.RemoveSession("objEquipment")
        btnSave.Enabled = False
    End Sub

    Private Sub dgEquipmentUpload_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgEquipmentUpload.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If Not e.Item.DataItem Is Nothing Then
                e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgEquipmentUpload.CurrentPageIndex * dgEquipmentUpload.PageSize)
                Dim _equipmentMaster As EquipmentMaster = CType(e.Item.DataItem, EquipmentMaster)
                Dim lblGroup As Label = CType(e.Item.FindControl("lblGroup"), Label)
                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                lblStatus.Text = CType(_equipmentMaster.Status, EquipmentMasterStatus.EquipmentMasterStatusEnum).ToString
                lblGroup.Text = CType(_equipmentMaster.Kind, EquipmentKind.EquipmentKindEnum).ToString
            End If
        End If
    End Sub

#End Region

End Class