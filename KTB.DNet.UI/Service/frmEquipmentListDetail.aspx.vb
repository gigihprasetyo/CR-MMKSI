#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
#End Region

Public Class frmEquipmentListDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents chkDeletePhoto As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents Label14 As System.Web.UI.WebControls.Label
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents lblEquipmentNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblDescription As System.Web.UI.WebControls.Label
    Protected WithEvents imgPhoto As System.Web.UI.WebControls.Image
    Protected WithEvents filePhoto As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents txtSpecification As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblLastUpdateBy As System.Web.UI.WebControls.Label
    Protected WithEvents lblLastUpdateTime As System.Web.UI.WebControls.Label
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents Label17 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents TRPhoto As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents TRUploadPhoto As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents TRDeletePhoto As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents TRSpecification As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents dgDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents OpStatus As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button

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
    Private _BOMArray As ArrayList
    Private runningTotalQty As Integer = 0
    Private runningTotalPrice As Double = 0

    Private sessionHelper As New SessionHelper
    'Private datenow As String = DateTime.Today.Year & DateTime.Today.Month & DateTime.Today.Day
#End Region

#Region "Custom Method"

    Private Sub RetrieveEquipment(ByVal id As Integer, ByVal status As String)
        Dim objEquipmentMaster As New EquipmentMaster
        Dim objEquipmentMasterFacade As New EquipmentMasterFacade(User)

        objEquipmentMaster = objEquipmentMasterFacade.Retrieve(id)

        lblEquipmentNumber.Text = objEquipmentMaster.EquipmentNumber
        lblDescription.Text = objEquipmentMaster.Description
        txtSpecification.Text = objEquipmentMaster.Specification

        If (objEquipmentMaster.PhotoFileName = "") OrElse (objEquipmentMaster.PhotoFileName = Nothing) Then
            TRPhoto.Visible = False
            TRDeletePhoto.Visible = False
        Else
            Dim dest As String = Server.MapPath("") & "\..\DataFile\Equipment\Image\" & objEquipmentMaster.PhotoFileName

            Dim paths As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & objEquipmentMaster.PhotoPath & "\" & objEquipmentMaster.PhotoFileName
            Dim exist As Boolean = CheckFileExist(New FileInfo(paths), dest)
            imgPhoto.ImageUrl = "..\DataFile\Equipment\Image\" & objEquipmentMaster.PhotoFileName

            TRPhoto.Visible = True
            'TRDeletePhoto.Visible = True
        End If

        If status = "detail" Then
            If (objEquipmentMaster.Specification = "") OrElse (objEquipmentMaster.Specification = Nothing) Then
                TRSpecification.Visible = False
            End If
        Else
            TRSpecification.Visible = True
        End If

        'lblPrice.Text = runningTotalPrice 'objEquipmentMaster.Price
        ddlStatus.SelectedValue = objEquipmentMaster.Status
        lblLastUpdateBy.Text = UserInfo.Convert(objEquipmentMaster.LastUpdateBy)
        lblLastUpdateTime.Text = Format(objEquipmentMaster.LastUpdateTime, "dd/MM/yyyy")
    End Sub
    Private Function CheckFileExist(ByVal fileinfo As FileInfo, ByVal dest As String) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Try
            success = imp.Start()
            If success Then
                fileinfo.CopyTo(dest, True)
                Return fileinfo.Exists
            End If
        Catch ex As Exception
            Return False
        Finally
            Try
                imp.StopImpersonate()
                imp = Nothing
            Catch ex As Exception

            End Try
        End Try

    End Function

    Private Sub InitializePage(ByVal _status As String)
        If _status = "detail" Then
            TRDeletePhoto.Visible = False
            TRUploadPhoto.Visible = False
            txtSpecification.ReadOnly = True
            btnSave.Enabled = False
            ddlStatus.Enabled = False
        Else
            TRSpecification.Visible = True
            TRDeletePhoto.Visible = True
            TRUploadPhoto.Visible = True
            txtSpecification.ReadOnly = False
            btnSave.Enabled = True
            ddlStatus.Enabled = True
        End If
    End Sub

    Sub ComputeSum(ByVal sender As Object, ByVal e As DataGridItemEventArgs)

        If (e.Item.ItemType = ListItemType.Item) OrElse (e.Item.ItemType = ListItemType.AlternatingItem) Then
            '-- sum total quantity
            runningTotalQty += Integer.Parse(e.Item.Cells(4).Text)

            '-- sum total price
            runningTotalPrice += Double.Parse(e.Item.Cells(5).Text)

        ElseIf e.Item.ItemType = ListItemType.Footer Then
            e.Item.Cells(3).Text = "Total"
            e.Item.Cells(4).Text = runningTotalQty
            e.Item.Cells(5).Text = "Rp " & String.Format("{0:#,###}", runningTotalPrice)
        End If
    End Sub

    Private Sub BindGrid(ByVal id As Integer)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DetailBOM), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DetailBOM), "HeaderBOM.EquipmentMaster.ID", MatchType.Exact, id))

        '_BOMArray = New DetailBOMFacade(User).Retrieve(criterias)

        _BOMArray = New DetailBOMFacade(User).Retrieve(criterias, CType(ViewState("CurrentSortColumn"), String), _
                CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgDetail.DataSource = _BOMArray
        dgDetail.DataBind()

        'If _BOMArray.Count > 0 Then
        '    lblPrice.Text = "Rp " & String.Format("{0:#,###}", runningTotalPrice)
        'Else
        '    Dim objEquipmentMaster As EquipmentMaster = New EquipmentMasterFacade(User).Retrieve(id)
        '    lblPrice.Text = "Rp " & String.Format("{0:#,###}", objEquipmentMaster.Price)
        'End If
    End Sub

    Private Sub UpdateEquipment(ByVal id As Integer)
        Dim _Eqold As New EquipmentMaster
        Dim objEquipmentMasterFacade As New EquipmentMasterFacade(User)

        _Eqold = objEquipmentMasterFacade.Retrieve(id)

        _Eqold.Specification = txtSpecification.Text
        _Eqold.Status = ddlStatus.SelectedValue

        If chkDeletePhoto.Checked = True Then
            _Eqold.PhotoFileName = Nothing
            _Eqold.PhotoPath = Nothing
        Else
            If filePhoto.Value <> "" OrElse filePhoto.Value <> Nothing Then
                Dim SrcFile As String = Path.GetFileName(filePhoto.PostedFile.FileName)  '-- Source file name
                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "\DataFile\Equipment\Image\" & _Eqold.EquipmentNumber & "\" & SrcFile  '-- Destination file
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False
                Dim finfo As New FileInfo(DestFile)
                Try
                    success = imp.Start()
                    If success Then
                        If Not finfo.Directory.Exists Then
                            Directory.CreateDirectory(finfo.DirectoryName)
                        End If
                        filePhoto.PostedFile.SaveAs(DestFile)
                        imp.StopImpersonate()
                        imp = Nothing
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
                _Eqold.PhotoFileName = SrcFile 'Path.GetFileName(filePhoto.PostedFile.FileName)
                _Eqold.PhotoPath = "\DataFile\Equipment\Image\" & _Eqold.EquipmentNumber 'Path.GetDirectoryName(filePhoto.PostedFile.FileName)
            End If
        End If
        objEquipmentMasterFacade.Update(_Eqold)
    End Sub

    Private Sub CopytoAnOtherWebServer(ByVal finfo As FileInfo, ByVal EqId As String)
        Dim helper As FileHelper = New FileHelper
        'Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        'Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        'Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        'Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        'Dim success As Boolean = False
        Dim otherFolder As String = "Image\" & EqId
        Try
            'success = imp.Start
            'If success Then
            helper.TransferToListWebServer(finfo, otherFolder, True, "EquipmentDirectory")
            'imp.StopImpersonate()
            'imp = Nothing
            'End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim _unit As Unit = New Unit(KTB.DNet.Lib.WebConfig.GetValue("ImageHeight"))
        ActivateUserPrivilege()
        imgPhoto.Height = _unit
        If Not IsPostBack Then
            'ViewState("Count") = 0


            Dim id As Integer = Request.QueryString("Eq")
            Dim _status As String = Request.QueryString("Status")
            viewState("id") = id
            InitializePage(_status)
            RetrieveEquipment(id, _status)
            InitiatePage()
            BindGrid(id)
        End If

        'Hidden1.Value = CInt(ViewState("Count")) + 1
        'ViewState("Count") = Hidden1.Value

    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.EquipmentMasterViewListDetail_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Detail Equipment")
        End If
        btnSave.Visible = SecurityProvider.Authorize(Context.User, SR.EquipmentMasterUpdateListDetail_Privilege)
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "Quantity"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub dgDetail_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDetail.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If

        dgDetail.SelectedIndex = -1
        BindGrid(CInt(viewState("id")))
    End Sub

    Private Sub dgDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDetail.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As DetailBOM = CType(e.Item.DataItem, DetailBOM)

            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                e.Item.Cells(2).Text = RowValue.EquipmentMaster.EquipmentNumber
                e.Item.Cells(3).Text = RowValue.EquipmentMaster.Description
                Dim pricetotal As Double
                pricetotal = RowValue.Quantity * RowValue.EquipmentMaster.Price
                e.Item.Cells(5).Text = FormatNumber(pricetotal, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            End If

        End If

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim id As Integer = Request.QueryString("Eq")
        Dim _status As String = Request.QueryString("Status")
        Try
            If filePhoto.Value <> "" OrElse filePhoto.Value <> Nothing Then
                If (filePhoto.PostedFile.ContentType = "image/pjpeg") OrElse (filePhoto.PostedFile.ContentType = "image/jpeg") Then
                    If (filePhoto.PostedFile.ContentLength <= 153600) Then
                        UpdateEquipment(id)
                        RetrieveEquipment(id, _status)
                        MessageBox.Show(SR.SaveSuccess)
                        TRDeletePhoto.Visible = True
                        TRPhoto.Visible = True
                        chkDeletePhoto.Checked = False
                    Else
                        MessageBox.Show("Size File Gambar Melebihi dari 150 kb")
                    End If
                Else
                    MessageBox.Show("Bukan File Gambar")
                    'Exit Try
                End If
            Else
                UpdateEquipment(id)
                RetrieveEquipment(id, _status)
                MessageBox.Show(SR.SaveSuccess)
            End If

        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)
        End Try
        TRUploadPhoto.Visible = True
    End Sub

    Private Sub chkDeletePhoto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDeletePhoto.CheckedChanged
        If chkDeletePhoto.Checked Then
            TRUploadPhoto.Visible = False
        Else
            TRUploadPhoto.Visible = True
        End If
    End Sub

#End Region

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If Not sessionHelper.GetSession("PrevPage") Is Nothing AndAlso Not sessionHelper.GetSession("PrevPage") = String.Empty Then
            Response.Redirect(sessionHelper.GetSession("PrevPage").ToString())
        Else
            Response.Redirect("../login.aspx")
        End If
    End Sub
End Class