#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Parser
Imports KTB.DNet.Utility
Imports System.IO
Imports KTB.DNet.Security

Imports KTB.DNet.BusinessFacade
#End Region

Public Class FrmEquipmentBOMMaintenance
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents dtgBOMUpload As System.Web.UI.WebControls.DataGrid
    Protected WithEvents DataFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
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
        If Not SecurityProvider.Authorize(Context.User, SR.EquipmentMasterUploadBomView_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Master BOM")
        End If
        btnSave.Visible = SecurityProvider.Authorize(Context.User, SR.EquipmentMasterUploadBomSave_Privilege)
    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        If DataFile.PostedFile.ContentType.ToString = "text/plain" Then
            Try
                UploadParsingFile()
            Catch ex As Exception
                MessageBox.Show(SR.UploadFail("BOM"))
            End Try
        Else
            MessageBox.Show("File Text Yang Dimasukkan Salah")
        End If
    End Sub

    Private Sub dtgBOMUpload_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgBOMUpload.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If Not e.Item.DataItem Is Nothing Then
                e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgBOMUpload.CurrentPageIndex * dtgBOMUpload.PageSize)
                Dim bom As HeaderBOM = CType(e.Item.DataItem, HeaderBOM)
                If bom.DetailBOMs.Count > 0 Then
                    Dim dtgBOMDetails As DataGrid = New DataGrid
                    dtgBOMDetails.Width = Unit.Percentage(100)
                    dtgBOMDetails.BorderWidth = Unit.Pixel(1)
                    dtgBOMDetails.CellPadding = 2
                    dtgBOMDetails.CellSpacing = 0
                    dtgBOMDetails.GridLines = GridLines.Horizontal
                    dtgBOMDetails.BorderColor = Color.FromName("Black")
                    dtgBOMDetails.HeaderStyle.BackColor = Color.FromName("White")
                    dtgBOMDetails.HeaderStyle.ForeColor = Color.FromName("Black")
                    dtgBOMDetails.HeaderStyle.Font.Bold = True
                    dtgBOMDetails.HeaderStyle.Font.Size = FontUnit.XSmall
                    dtgBOMDetails.ItemStyle.Font.Name = "Verdana"
                    dtgBOMDetails.ItemStyle.Font.Size = FontUnit.XSmall
                    dtgBOMDetails.AlternatingItemStyle.BackColor = Color.FromName("Gainsboro")
                    dtgBOMDetails.AutoGenerateColumns = False

                    'Number
                    Dim _boundColumn As BoundColumn = New BoundColumn
                    _boundColumn.HeaderText = "Kode Komponen"
                    _boundColumn.DataField = "EquipmentNumber"
                    dtgBOMDetails.Columns.Add(_boundColumn)


                    'Description
                    _boundColumn = New BoundColumn
                    _boundColumn.HeaderText = "Nama Komponen"
                    _boundColumn.DataField = "EquipmentDescription"
                    dtgBOMDetails.Columns.Add(_boundColumn)

                    'Quantity
                    _boundColumn = New BoundColumn
                    _boundColumn.HeaderText = "Jumlah"
                    _boundColumn.DataField = "Quantity"
                    dtgBOMDetails.Columns.Add(_boundColumn)

                    'Quantity
                    _boundColumn = New BoundColumn
                    _boundColumn.HeaderText = "Keterangan"
                    _boundColumn.DataField = "ErrorMessage"
                    dtgBOMDetails.Columns.Add(_boundColumn)
                    dtgBOMDetails.DataSource = bom.DetailBOMs
                    dtgBOMDetails.DataBind()
                    e.Item.Cells(2).Controls.Add(dtgBOMDetails)
                End If
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim ssHelper As SessionHelper = New SessionHelper
        Dim bomColl As ArrayList = CType(ssHelper.GetSession("BOMCollection"), ArrayList)
        Try
            InsertCollection(bomColl)
            btnSave.Enabled = False
            MessageBox.Show(SR.SaveSuccess)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        dtgBOMUpload.DataSource = bomColl
        dtgBOMUpload.DataBind()
    End Sub


    Public Sub InsertCollection(ByVal BOMCollection As ArrayList)
        Dim bomFacade As BOMMaintenanceFacade
        For Each item As HeaderBOM In BOMCollection
            bomFacade = New BOMMaintenanceFacade(User)
            bomFacade.Update(item)
        Next
    End Sub

#End Region

#Region "Private Method"

    Private Sub CheckFolderExist(ByVal fileName As String)
        Dim fiinfo As FileInfo = New FileInfo(fileName)
        If Not fiinfo.Directory.Exists Then
            fiinfo.Directory.Create()
        End If
    End Sub

    Private Sub UploadParsingFile()
        Dim parser As IParser
        Dim _BOMParser As BOMParser = New BOMParser
        parser = _BOMParser
        Dim collBOM As New ArrayList
        Dim dayFolder As String = Now.Year & Now.Month & Now.Day
        If (Not DataFile.PostedFile Is Nothing) And (DataFile.PostedFile.ContentLength > 0) Then
            Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName)  '-- Source file name
            Dim DestFile As String = Server.MapPath("") & "\..\DataTemp\Bom\" & dayFolder & "\" & SrcFile & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond    '-- Destination file
            CheckFolderExist(DestFile)
            DataFile.PostedFile.SaveAs(DestFile)
            collBOM = CType(parser.ParseNoTransaction(DestFile, User.Identity.Name.ToString), ArrayList)
           
         End If
        InitializePage(collBOM)
        dtgBOMUpload.DataSource = collBOM
        dtgBOMUpload.DataBind()
    End Sub

    Private Sub InitializePage(ByVal collBom As ArrayList)
        If isValidBOM(collBom) Then
            Dim ssHelper As SessionHelper = New SessionHelper
            ssHelper.SetSession("BOMCollection", collBom)
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If

        If collBom.Count < 1 Then
            btnSave.Enabled = False
        End If

    End Sub

    Private Function isValidBOM(ByVal collBom As ArrayList) As Boolean
        Dim _result As Boolean = True
        Dim i As Integer
        For Each bom As HeaderBOM In collBom
            If Not bom.ErrorMessage Is Nothing Then
                If bom.ErrorMessage.Length > 0 Then
                    'Return False
                    _result = False
                End If
            Else
                If bom.DetailBOMs.Count > 0 Then
                    For Each item As DetailBOM In bom.DetailBOMs
                        If Not item.ErrorMessage Is Nothing Then
                            If item.ErrorMessage.Length > 0 Then
                                'Return False
                                _result = False
                            End If
                        End If
                        i = 0
                        For Each items As DetailBOM In bom.DetailBOMs
                            If (Not item.EquipmentMaster Is Nothing) And (Not items.EquipmentMaster Is Nothing) Then
                                If item.EquipmentMaster.EquipmentNumber = items.EquipmentMaster.EquipmentNumber Then
                                    'Return False
                                    i += 1
                                    If i > 1 Then
                                        item.ErrorMessage = " Duplikasi Equipment Number"
                                        _result = False
                                    End If
                                End If
                            End If

                        Next
                    Next
                End If
            End If
        Next
        Return _result
    End Function

#End Region

End Class