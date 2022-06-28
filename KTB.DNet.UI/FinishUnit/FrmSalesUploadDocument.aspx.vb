Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports System.IO

Public Class FrmSalesUploadDocument
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblNoSurat As System.Web.UI.WebControls.Label
    Protected WithEvents ddlDepartemen As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTanggal As System.Web.UI.WebControls.Label
    Protected WithEvents txtKepada As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPerihal As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlJenisSurat As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents lblUploadBy As System.Web.UI.WebControls.Label
    Protected WithEvents UploadFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents lblSearchUserGroup As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "private Variable"
    Private sessHelper As New SessionHelper
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.SalesDocUpload_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FINISH UNIT - Upload Sales Document")
        End If
    End Sub
#End Region

    Private Sub Initialize()
        ClearData()
        ViewState("CurrentSortColumn") = "SalesmanHeader.SalesmanCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub
    Private Sub ClearData()
        lblNoSurat.Text = "-"
        lblTanggal.Text = DateTime.Today.ToString("dd/MM/yyyy")
        ddlDepartemen.SelectedIndex = 0
        'txtDealerCodeMultiple.Text = String.Empty
        txtKepada.Text = String.Empty
        txtPerihal.Text = String.Empty
        ddlJenisSurat.SelectedIndex = 0
        lblUploadBy.Text = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo).UserName
    End Sub
    Private Sub BindDepartmentDropDownLists()
        Dim arlDep As ArrayList = New DepartmentFacade(User).RetrieveActiveList
        Dim arlList As New ArrayList
        For Each item As Department In arlDep
            If SecurityProvider.Authorize(Context.User, item.Privilege) Then
                arlList.Add(item)
            End If
        Next

        ddlDepartemen.Items.Clear()
        ddlDepartemen.DataSource = arlList
        ddlDepartemen.DataTextField = "Code"
        ddlDepartemen.DataValueField = "ID"
        ddlDepartemen.DataBind()
        ddlDepartemen.Items.Insert(0, New ListItem("Silahkan Pilih", "0"))
    End Sub
    Private Sub BindLetterKinfDropDownLists(ByVal DeptID As Integer)
        ddlJenisSurat.Items.Clear()
        ddlJenisSurat.DataSource = New KindOfLetterFacade(User).RetrieveActiveListByDepartment(DeptID, "Description", Sort.SortDirection.ASC)
        ddlJenisSurat.DataTextField = "Description"
        ddlJenisSurat.DataValueField = "ID"
        ddlJenisSurat.DataBind()
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            BindDepartmentDropDownLists()
            BindLetterKinfDropDownLists(CInt(ddlDepartemen.SelectedValue))
            Initialize()
            'lblPopUpDealer.Attributes("onClick") = "ShowPPDealerSelection()"
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClearData()
    End Sub

    Private Sub ddlDepartemen_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlDepartemen.SelectedIndexChanged
        BindLetterKinfDropDownLists(CInt(ddlDepartemen.SelectedValue))
    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Dim nResultF As Integer
        Dim Ids As ArrayList

        If ValidateUpload() Then
            Ids = SaveData()
            'nResultF = SaveFile(nResultD)
            'If nResultD > 0 And nResultF > 0 Then
            '    MessageBox.Show("Upload Complete")
            '    BindData(nResultD)

            If Ids.Count > 0 Then
                MessageBox.Show("Upload Complete")
                BindData(Ids)
            Else
                MessageBox.Show("Upload Fail")
            End If
        End If
    End Sub

    Private Sub BindData(ByVal IDs As ArrayList)
        'For Each ID As Integer In IDs
        '    Dim obj As Letter = New LetterFacade(User).Retrieve(ID)
        '    lblNoSurat.Text += obj.NomorSurat + " "
        '    'lblTanggal.Text = obj.UploadDate.ToString("dd/MM/yyyy")
        '    'ddlDepartemen.SelectedValue = obj.Department.ID
        '    'txtDealerCode.Text = obj.Dealer.DealerCode & ";" & obj.Dealer.SearchTerm1
        '    'txtKepada.Text = obj.Penerima
        '    'txtPerihal.Text = obj.Perihal
        '    'ddlJenisSurat.SelectedValue = obj.KindOfLetter.ID
        '    lblUploadBy.Text = obj.UploadBy
        'Next
        'lblNoSurat.Text = lblNoSurat.Text.Trim("-")
    End Sub

    Private Function SaveData() As ArrayList
        Dim nResult As String = String.Empty
        Dim obj As Letter
        Dim dealerCodes As String() = txtKepada.Text.Split(";")
        Dim SelectedDept As Department = New DepartmentFacade(User).Retrieve(CInt(ddlDepartemen.SelectedValue))
        Dim SelectedKindOfLetter As KindOfLetter = New KindOfLetterFacade(User).Retrieve(CInt(ddlJenisSurat.SelectedValue))
        Dim IDs As New ArrayList(2)
        Dim list As ArrayList = New ArrayList
        Dim objDealer As Dealer
        Try
            For Each item As String In dealerCodes
                Dim SelectedDealer As Dealer = New DealerFacade(User).Retrieve(item)
                objDealer = New DealerFacade(User).Retrieve(item.Split("-")(0).ToString)
                obj = New Letter
                obj.Dealer = objDealer
                obj.Department = SelectedDept
                obj.KindOfLetter = SelectedKindOfLetter
                obj.Penerima = item
                obj.Perihal = txtPerihal.Text
                obj.UploadDate = CType(lblTanggal.Text, DateTime)
                obj.UploadBy = lblUploadBy.Text
                obj.FileName = SelectedDept.Code.Substring(0, 3) & Now.Year.ToString.Substring(2, 2) & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Path.GetExtension(UploadFile.PostedFile.FileName)
                list.Add(obj)
            Next
            nResult = New LetterFacade(User).InsertTransaction(list) & Path.GetExtension(UploadFile.PostedFile.FileName)
            If nResult.Length > 0 Then
                SaveFile(nResult)
                Dim nomor As String = nResult.Substring(0, nResult.Length - 4)
                If nResult.Length > 4 Then
                    lblNoSurat.Text = nomor
                End If
                IDs.Add(nResult)
                Return IDs
            End If
        Catch ex As Exception
            IDs.Add(-1)
            Return IDs
        End Try

    End Function

    Private Function SaveFile(ByVal nomorSurat As String) As Integer
        Dim nResult As Integer = -1
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("SALESDOCUMENT") & nomorSurat
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo
        Try
            success = imp.Start()
            If success Then
                finfo = New FileInfo(DestFile)

                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If

                If finfo.Exists Then
                    finfo.Delete()
                End If

                UploadFile.PostedFile.SaveAs(DestFile)
                nResult = 1
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            nResult = -1
            Throw ex
        End Try
        Return nResult
    End Function
    Private Function ValidateUpload() As Boolean
        If ddlDepartemen.SelectedIndex = 0 Then
            MessageBox.Show("Silakan pilih departemen!")
            Return False
        End If

        'If txtDealerCodeMultiple.Text = String.Empty Then
        '    MessageBox.Show("Silakan pilih dealer!")
        '    Return False
        'End If

        If txtKepada.Text = String.Empty Then
            MessageBox.Show("Silakan isi kepada!")
            Return False
        End If

        If txtPerihal.Text = String.Empty Then
            MessageBox.Show("Silakan isi perihal!")
            Return False
        End If

        If UploadFile.Value = String.Empty Then
            MessageBox.Show("Tidak ada file yg di pilih!")
            Return False
        End If
        If UploadFile.PostedFile.ContentLength = 0 Then
            MessageBox.Show("File tidak memiliki data")

        End If
        'If UploadFile.PostedFile.ContentLength > 1024000 Then
        '    MessageBox.Show("File Gambar melebihi 1MB")
        'End If
        'Dim dealerCode As String() = txtDealerCodeMultiple.Text.Split(";")
        'For Each item As String In dealerCode
        '    Dim SelectedDealer As Dealer = New DealerFacade(User).Retrieve(item)
        '    If SelectedDealer Is Nothing Or SelectedDealer.ID = 0 Then
        '        MessageBox.Show("DealerInvalid")
        '        Return False
        '    End If
        'Next

        Return True
    End Function
End Class
