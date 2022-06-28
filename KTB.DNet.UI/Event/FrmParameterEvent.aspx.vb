#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Event
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class FrmParameterEvent
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlSalesmanArea As System.Web.UI.WebControls.DropDownList
    Protected WithEvents calDari As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents calSampai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlJenisKegiatan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlModel As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNamaKegiatan As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents fuFileMaterial As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents fuJuklak As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents tblCategoryModelType As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RegUpload1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegUpload2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegUpload3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents SearchTitle As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents rfvFileMaterial As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfvJulkak As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lnbFileMaterial As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lnbJuklak As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btnBack As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents lnbFilePendukung10 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents fuFilePendukung2 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents lnbFilePendukung1 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Regularexpressionvalidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lnbFilePendukung2 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Regularexpressionvalidator2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lnbFilePendukung3 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Regularexpressionvalidator3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lnbFilePendukung4 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Regularexpressionvalidator4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lnbFilePendukung5 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Regularexpressionvalidator5 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lnbFilePendukung6 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Regularexpressionvalidator6 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lnbFilePendukung7 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Regularexpressionvalidator7 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lnbFilePendukung8 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Regularexpressionvalidator8 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lnbFilePendukung9 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Regularexpressionvalidator9 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents fuFilePendukung1 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents fuFilePendukung3 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents fuFilePendukung4 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents fuFilePendukung5 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents fuFilePendukung6 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents fuFilePendukung7 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents fuFilePendukung8 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents fuFilePendukung9 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents fuFilePendukung10 As System.Web.UI.HtmlControls.HtmlInputFile

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"

#End Region

#Region " Private Variables"
    Private DealerCollection As ArrayList
#End Region

#Region "Custom Method"
    Private Sub FillSalesmanArea()
        ddlSalesmanArea.Items.Clear()
        Dim objArea As New SalesmanAreaFacade(User)
        ddlSalesmanArea.DataSource = objArea.RetrieveActiveList
        ddlSalesmanArea.DataTextField = "AreaDesc"
        ddlSalesmanArea.DataValueField = "ID"
        ddlSalesmanArea.DataBind()
        ddlSalesmanArea.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub
    Private Sub FillJenisKegiatan()
        ddlJenisKegiatan.Items.Clear()
        Dim objActivityType As New ActivityTypeFacade(User)
        ddlJenisKegiatan.DataSource = objActivityType.RetrieveActiveList()
        ddlJenisKegiatan.DataTextField = "ActivityName"
        ddlJenisKegiatan.DataValueField = "ID"
        ddlJenisKegiatan.DataBind()
        ddlJenisKegiatan.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub
    Private Sub ShowModelCategoryType(ByVal IsVisible As Boolean)
        tblCategoryModelType.Visible = IsVisible
    End Sub
    Private Sub FillCategory()
        ddlCategory.Items.Clear()
        Dim objCategory As New CategoryFacade(User)
        ddlCategory.DataSource = objCategory.RetrieveActiveList
        ddlCategory.DataTextField = "Description"
        ddlCategory.DataValueField = "ID"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub
    Private Sub FillModel(ByVal CategoryID As Integer)
        ddlModel.Items.Clear()
        If CategoryID > -1 Then
            Dim objModel As New VechileModelFacade(User)
            Dim crit As New CriteriaComposite(New Criteria(GetType(VechileModel), "RowStatus", CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(VechileModel), "Category", CategoryID))
            ddlModel.DataSource = objModel.RetrieveList("Description", Sort.SortDirection.ASC, crit)
            ddlModel.DataTextField = "Description"
            ddlModel.DataValueField = "ID"
            ddlModel.DataBind()
            ddlModel.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        Else
            ddlModel.Items.Insert(0, New ListItem("Pilih Kategori", -1))
        End If
    End Sub
    Private Sub FillType(ByVal ModelID As Integer)
        If ModelID > -1 Then
            Dim objType As New VechileTypeFacade(User)
            Dim crit As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", CType(DBRowStatus.Active, Short)))
            Dim sorts As New SortCollection
            sorts.Add(New Sort(GetType(VechileType), "Description", Sort.SortDirection.ASC))
            crit.opAnd(New Criteria(GetType(VechileType), "VechileModel", ModelID))
            ddlType.DataSource = objType.RetrieveByCriteria(crit, sorts)
            ddlType.DataTextField = "Description"
            ddlType.DataValueField = "ID"
            ddlType.DataBind()
            ddlType.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        Else
            ddlType.Items.Insert(0, New ListItem("Pilih Model", -1))
        End If
    End Sub
    Private Sub FillYear()
        ddlYear.Items.Clear()
        For i As Int32 = Now.Year - 5 To Now.Year + 5
            ddlYear.Items.Add(i)
        Next
        ddlYear.SelectedValue = Now.Year
    End Sub
    Private Function IsValidFileContent(ByVal fuUpload As HttpPostedFile) As Boolean
        Dim IsValid As Boolean = True
        If fuUpload.ContentLength > 0 AndAlso (fuUpload.ContentType.ToLower = "text/plain" OrElse _
            fuUpload.ContentType.ToLower = "text/csv" OrElse fuUpload.ContentType.ToLower = _
            "application/msword" OrElse fuUpload.ContentType.ToLower = "application/vnd.ms-excel" OrElse _
            fuUpload.ContentType.ToLower = "application/zip" OrElse fuUpload.ContentType.ToLower = _
            "application/pdf") OrElse fuUpload.ContentType.ToLower = "application/x-zip-compressed" Then
            If fuUpload.ContentLength > KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize") Then
                IsValid = False
                MessageBox.Show(String.Format("Ukuran file \'{0}\' melebihi batas. Kecilkan ukuran file lalu upload kembali", Path.GetFileName(fuUpload.FileName).Replace("'", "\'")))
            End If
        Else
            MessageBox.Show(String.Format("Tipe file tidak diijinkan untuk digunakan({0})", _
                Path.GetExtension(fuUpload.FileName)))
            IsValid = False
        End If
        Return IsValid
    End Function
    Private Function IsFilePendukungValid() As Boolean
        Dim IsValid As Boolean = True
        If fuFilePendukung1.PostedFile.ContentLength > 0 Then
            If Not IsValidFileContent(fuFilePendukung1.PostedFile) Then
                IsValid = False
            End If
        End If
        If fuFilePendukung2.PostedFile.ContentLength > 0 Then
            If Not IsValidFileContent(fuFilePendukung2.PostedFile) Then
                IsValid = False
            End If
        End If
        If fuFilePendukung3.PostedFile.ContentLength > 0 Then
            If Not IsValidFileContent(fuFilePendukung3.PostedFile) Then
                IsValid = False
            End If
        End If
        If fuFilePendukung4.PostedFile.ContentLength > 0 Then
            If Not IsValidFileContent(fuFilePendukung4.PostedFile) Then
                IsValid = False
            End If
        End If
        If fuFilePendukung5.PostedFile.ContentLength > 0 Then
            If Not IsValidFileContent(fuFilePendukung5.PostedFile) Then
                IsValid = False
            End If
        End If
        If fuFilePendukung6.PostedFile.ContentLength > 0 Then
            If Not IsValidFileContent(fuFilePendukung6.PostedFile) Then
                IsValid = False
            End If
        End If
        If fuFilePendukung7.PostedFile.ContentLength > 0 Then
            If Not IsValidFileContent(fuFilePendukung7.PostedFile) Then
                IsValid = False
            End If
        End If
        If fuFilePendukung8.PostedFile.ContentLength > 0 Then
            If Not IsValidFileContent(fuFilePendukung8.PostedFile) Then
                IsValid = False
            End If
        End If
        If fuFilePendukung9.PostedFile.ContentLength > 0 Then
            If Not IsValidFileContent(fuFilePendukung9.PostedFile) Then
                IsValid = False
            End If
        End If
        If fuFilePendukung10.PostedFile.ContentLength > 0 Then
            If Not IsValidFileContent(fuFilePendukung10.PostedFile) Then
                IsValid = False
            End If
        End If
        Return IsValid
    End Function
    Private Function RetrieveEventParameter() As EventParameter
        Dim objFacade As New EventParameterFacade(User)
        Dim crits As New CriteriaComposite(New Criteria(GetType(EventParameter), _
            "RowStatus", CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(EventParameter), "ActivityType", ddlJenisKegiatan.SelectedValue))
        crits.opAnd(New Criteria(GetType(EventParameter), "EventName", txtNamaKegiatan.Text))
        crits.opAnd(New Criteria(GetType(EventParameter), "EventYear", ddlYear.SelectedValue))
        Dim arr As ArrayList = objFacade.Retrieve(crits)
        If arr.Count > 0 Then
            Return arr(0)
        End If
    End Function
    Private Function IsPageValid() As Boolean
        Dim IsValid As Boolean = True
        Dim strBuilder As New StringBuilder("")
        If ddlJenisKegiatan.SelectedValue = -1 Then
            MessageBox.Show("Harap pilih jenis kegiatan")
            IsValid = False
        ElseIf Not IsNothing(RetrieveEventParameter) Then
            IsValid = False
            MessageBox.Show(String.Format("Sudah ada parameter untuk kegiatan \'{0}\' pada tahun {1}", _
                txtNamaKegiatan.Text, ddlYear.SelectedValue))
        End If
        If (fuFileMaterial.PostedFile.FileName.Length > 0 AndAlso Not IsValidFileContent(fuFileMaterial.PostedFile)) Or _
            Not IsFilePendukungValid() Or _
            (fuJuklak.PostedFile.FileName.Length > 0 AndAlso Not IsValidFileContent(fuJuklak.PostedFile)) Then
            IsValid = False
        End If
        DealerCollection = New ArrayList
        For Each DealerCode As String In txtDealerCode.Text.Split(";")
            Dim objDealer As Dealer = GetDealer(DealerCode)
            If Not IsNothing(objDealer) Then
                DealerCollection.Add(objDealer)
            Else
                MessageBox.Show(String.Format("Kode dealer tidak dikenali '{0}'", DealerCode))
            End If
        Next
        If calDari.Value > calSampai.Value Then
            IsValid = False
            MessageBox.Show(SR.InvalidRangeDate)
        End If
        Return IsValid
    End Function
    Private Function GetDealer(ByVal DealerCode As String) As Dealer
        Dim objFacade As New DealerFacade(User)
        Return objFacade.Retrieve(DealerCode)
    End Function
    Private Function GetSalesmanArea() As SalesmanArea
        Dim objFacade As New SalesmanAreaFacade(User)
        Return objFacade.Retrieve(CInt(ddlSalesmanArea.SelectedValue))
    End Function
    Private Function GetActivityType() As ActivityType
        Dim objFacade As New ActivityTypeFacade(User)
        Return objFacade.Retrieve(CInt(ddlJenisKegiatan.SelectedValue))
    End Function
    Private Function GetCategory() As Category
        Dim objFacade As New CategoryFacade(User)
        Return objFacade.Retrieve(CInt(ddlCategory.SelectedValue))
    End Function
    Private Function GetVehicleType() As VechileType
        Dim objFacade As New VechileTypeFacade(User)
        Return objFacade.Retrieve(CInt(ddlType.SelectedValue))
    End Function
    Private Sub UploadFile(ByVal fuUpload As HttpPostedFile, ByVal fileTarget As FileInfo)
        Dim objUpload As New UploadToWebServer
        objUpload.Upload(fuUpload.InputStream, fileTarget.FullName)
    End Sub
    Private ReadOnly Property GetID() As Integer
        Get
            Return Request.QueryString("id")
        End Get
    End Property
    Private Sub GetData()
        Dim objFacade As New EventParameterFacade(User)
        Dim eventParam As EventParameter = objFacade.Retrieve(GetID)
        txtDealerCode.Text = eventParam.Dealer.DealerCode
        If Not IsNothing(eventParam.SalesmanArea) Then
            ddlSalesmanArea.SelectedValue = eventParam.SalesmanArea.ID
        End If
        calDari.Value = eventParam.EventDateStart
        calSampai.Value = eventParam.EventDateEnd
        ddlJenisKegiatan.SelectedValue = eventParam.ActivityType.ID
        ddlJenisKegiatan_SelectedIndexChanged(Nothing, Nothing)
        If Not IsNothing(eventParam.Category) Then
            ddlCategory.SelectedValue = eventParam.Category.ID
            ddlCategory_SelectedIndexChanged(Nothing, Nothing)
        End If
        If Not IsNothing(eventParam.VechileType) Then
            ddlModel.SelectedValue = eventParam.VechileType.VechileModel.ID
            ddlModel_SelectedIndexChanged(Nothing, Nothing)
            ddlType.SelectedValue = eventParam.VechileType.ID
        End If
        txtNamaKegiatan.Text = eventParam.EventName
        ddlYear.SelectedValue = eventParam.EventYear
        rfvFileMaterial.Enabled = False
        rfvJulkak.Enabled = False
        lnbFileMaterial.Visible = True
        lnbJuklak.Visible = True
        lnbFileMaterial.Text = eventParam.FileNameMaterial
        lnbFileMaterial.CommandArgument = String.Format("{0}\{1}", eventParam.DirTarget, eventParam.FileNameMaterial)
        lnbJuklak.Text = eventParam.FileNameJuklak
        lnbJuklak.CommandArgument = String.Format("{0}\{1}", eventParam.DirTarget, eventParam.FileNameJuklak)
        If eventParam.FileNamePendukung1.Length > 0 Then
            lnbFilePendukung1.Text = eventParam.FileNamePendukung1
            lnbFilePendukung1.Visible = True
            lnbFilePendukung1.CommandArgument = String.Format("{0}\{1}", eventParam.DirTarget, eventParam.FileNamePendukung1)
        End If
        If eventParam.FileNamePendukung2.Length > 0 Then
            lnbFilePendukung2.Text = eventParam.FileNamePendukung2
            lnbFilePendukung2.Visible = True
            lnbFilePendukung2.CommandArgument = String.Format("{0}\{1}", eventParam.DirTarget, eventParam.FileNamePendukung2)
        End If
        If eventParam.FileNamePendukung3.Length > 0 Then
            lnbFilePendukung3.Text = eventParam.FileNamePendukung3
            lnbFilePendukung3.Visible = True
            lnbFilePendukung3.CommandArgument = String.Format("{0}\{1}", eventParam.DirTarget, eventParam.FileNamePendukung3)
        End If
        If eventParam.FileNamePendukung4.Length > 0 Then
            lnbFilePendukung4.Text = eventParam.FileNamePendukung4
            lnbFilePendukung4.Visible = True
            lnbFilePendukung4.CommandArgument = String.Format("{0}\{1}", eventParam.DirTarget, eventParam.FileNamePendukung4)
        End If
        If eventParam.FileNamePendukung5.Length > 0 Then
            lnbFilePendukung5.Text = eventParam.FileNamePendukung5
            lnbFilePendukung5.Visible = True
            lnbFilePendukung5.CommandArgument = String.Format("{0}\{1}", eventParam.DirTarget, eventParam.FileNamePendukung5)
        End If
        If eventParam.FileNamePendukung6.Length > 0 Then
            lnbFilePendukung6.Text = eventParam.FileNamePendukung6
            lnbFilePendukung6.Visible = True
            lnbFilePendukung6.CommandArgument = String.Format("{0}\{1}", eventParam.DirTarget, eventParam.FileNamePendukung6)
        End If
        If eventParam.FileNamePendukung7.Length > 0 Then
            lnbFilePendukung7.Text = eventParam.FileNamePendukung7
            lnbFilePendukung7.Visible = True
            lnbFilePendukung7.CommandArgument = String.Format("{0}\{1}", eventParam.DirTarget, eventParam.FileNamePendukung7)
        End If
        If eventParam.FileNamePendukung8.Length > 0 Then
            lnbFilePendukung8.Text = eventParam.FileNamePendukung8
            lnbFilePendukung8.Visible = True
            lnbFilePendukung8.CommandArgument = String.Format("{0}\{1}", eventParam.DirTarget, eventParam.FileNamePendukung8)
        End If
        If eventParam.FileNamePendukung9.Length > 0 Then
            lnbFilePendukung9.Text = eventParam.FileNamePendukung9
            lnbFilePendukung9.Visible = True
            lnbFilePendukung9.CommandArgument = String.Format("{0}\{1}", eventParam.DirTarget, eventParam.FileNamePendukung9)
        End If
        If eventParam.FileNamePendukung10.Length > 0 Then
            lnbFilePendukung10.Text = eventParam.FileNamePendukung10
            lnbFilePendukung10.Visible = True
            lnbFilePendukung10.CommandArgument = String.Format("{0}\{1}", eventParam.DirTarget, eventParam.FileNamePendukung10)
        End If
    End Sub
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        If Not IsPostBack Then
            FillSalesmanArea()
            FillJenisKegiatan()
            ShowModelCategoryType(False)
            FillCategory()
            ddlCategory_SelectedIndexChanged(Nothing, Nothing)
            ddlModel_SelectedIndexChanged(Nothing, Nothing)
            FillYear()
            If GetID > 0 Then
                GetData()
                txtDealerCode.ReadOnly = True
                lblSearchDealer.Enabled = False
                btnBack.Visible = True
            Else
                btnBack.Visible = False
            End If
        End If
    End Sub
    Private Sub ddlJenisKegiatan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlJenisKegiatan.SelectedIndexChanged
        ShowModelCategoryType(ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Small_Gathering, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Launching_Gathering, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Exhibition_Khusus, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Showroom_Event, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Others, Integer))
    End Sub
    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        FillModel(ddlCategory.SelectedValue)
    End Sub
    Private Sub ddlModel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlModel.SelectedIndexChanged
        FillType(ddlModel.SelectedValue)
    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If IsPageValid() Then
            Dim objFacade As New EventParameterFacade(User)
            Dim objEventParameterCollection As New ArrayList
            Dim objSalesmanArea As SalesmanArea = GetSalesmanArea()
            Dim objJenisKegiatan As ActivityType = GetActivityType()

            Dim objCategory As Category
            If CInt(ddlCategory.SelectedValue) > 0 Then
                objCategory = GetCategory()
            End If

            Dim objType As VechileType
            If CInt(ddlType.SelectedValue) > 0 Then
                objType = GetVehicleType()
            End If

            Dim dateUpload As DateTime = DateTime.Now
            Dim objEventParameter As EventParameter
            If GetID > 0 Then
                objEventParameter = objFacade.Retrieve(GetID)
            End If
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            Dim dirTarget As New DirectoryInfo(String.Format("{0}{1}\{2}({3:dd MMM yyyy} - {4:dd MMM yyyy})", _
                KTB.DNet.Lib.WebConfig.GetValue("SAN"), _
                KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), _
                ddlJenisKegiatan.SelectedItem.Text, calDari.Value, calSampai.Value))
            If imp.Start() Then
                If Not dirTarget.Exists Then
                    dirTarget.Create()
                End If
                For Each objDealer As Dealer In DealerCollection
                    If GetID = 0 Then
                        objEventParameter = New EventParameter
                    End If
                    objEventParameter.ActivityType = objJenisKegiatan
                    objEventParameter.Category = objCategory
                    objEventParameter.Dealer = objDealer
                    objEventParameter.EventDateEnd = calSampai.Value
                    objEventParameter.EventDateStart = calDari.Value
                    objEventParameter.EventName = txtNamaKegiatan.Text
                    objEventParameter.EventYear = ddlYear.SelectedValue
                    If fuJuklak.PostedFile.FileName.Length > 0 Then
                        objEventParameter.FileNameJuklak = String.Format("{0}({1}){2}", _
                            Path.GetFileNameWithoutExtension(fuJuklak.PostedFile.FileName), _
                            dateUpload.ToString("yyyyMMdd HHmmss"), _
                            Path.GetExtension(fuJuklak.PostedFile.FileName))
                    Else 'kalo beda pindahkan file ke dir baru
                        If objEventParameter.DirTarget <> dirTarget.Name Then
                            Dim dirTargetSource As New DirectoryInfo(String.Format("{0}{1}\{2}", _
                                KTB.DNet.Lib.WebConfig.GetValue("SAN"), _
                                KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), _
                                objEventParameter.DirTarget))
                            Dim objFileSource As New FileInfo(String.Format("{0}\{1}", _
                                dirTargetSource.FullName, objEventParameter.FileNameJuklak))
                            objFileSource.CopyTo(String.Format("{0}\{1}", _
                                dirTarget.FullName, objEventParameter.FileNameJuklak))
                        End If
                    End If
                    If fuFileMaterial.PostedFile.FileName.Length > 0 Then
                        objEventParameter.FileNameMaterial = String.Format("{0}({1}){2}", _
                            Path.GetFileNameWithoutExtension(fuFileMaterial.PostedFile.FileName), _
                            dateUpload.ToString("yyyyMMdd HHmmss"), _
                            Path.GetExtension(fuFileMaterial.PostedFile.FileName))
                    Else 'kalo beda pindahkan file ke dir baru
                        If objEventParameter.DirTarget <> dirTarget.Name Then
                            Dim dirTargetSource As New DirectoryInfo(String.Format("{0}{1}\{2}", _
                                KTB.DNet.Lib.WebConfig.GetValue("SAN"), _
                                KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), _
                                objEventParameter.DirTarget))
                            Dim objFileSource As New FileInfo(String.Format("{0}\{1}", _
                                dirTargetSource.FullName, objEventParameter.FileNameMaterial))
                            objFileSource.CopyTo(String.Format("{0}\{1}", _
                                dirTarget.FullName, objEventParameter.FileNameMaterial))
                        End If
                    End If
                    If fuFilePendukung1.PostedFile.FileName.Length > 0 Then
                        objEventParameter.FileNamePendukung1 = String.Format("{0}({1}){2}", _
                            Path.GetFileNameWithoutExtension(fuFilePendukung1.PostedFile.FileName), _
                            dateUpload.ToString("yyyyMMdd HHmmss"), _
                            Path.GetExtension(fuFilePendukung1.PostedFile.FileName))
                    Else 'kalo beda pindahkan file ke dir baru
                        If objEventParameter.FileNamePendukung1.Length > 0 AndAlso _
                            objEventParameter.DirTarget <> dirTarget.Name Then
                            Dim dirTargetSource As New DirectoryInfo(String.Format("{0}{1}\{2}", _
                                KTB.DNet.Lib.WebConfig.GetValue("SAN"), _
                                KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), _
                                objEventParameter.DirTarget))
                            Dim objFileSource As New FileInfo(String.Format("{0}\{1}", _
                                dirTargetSource.FullName, objEventParameter.FileNamePendukung1))
                            objFileSource.CopyTo(String.Format("{0}\{1}", _
                                dirTarget.FullName, objEventParameter.FileNamePendukung1))
                        End If
                    End If
                    If fuFilePendukung2.PostedFile.FileName.Length > 0 Then
                        objEventParameter.FileNamePendukung2 = String.Format("{0}({1}){2}", _
                            Path.GetFileNameWithoutExtension(fuFilePendukung2.PostedFile.FileName), _
                            dateUpload.ToString("yyyyMMdd HHmmss"), _
                            Path.GetExtension(fuFilePendukung2.PostedFile.FileName))
                    Else 'kalo beda pindahkan file ke dir baru
                        If objEventParameter.FileNamePendukung2.Length > 0 AndAlso _
                            objEventParameter.DirTarget <> dirTarget.Name Then
                            Dim dirTargetSource As New DirectoryInfo(String.Format("{0}{1}\{2}", _
                                KTB.DNet.Lib.WebConfig.GetValue("SAN"), _
                                KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), _
                                objEventParameter.DirTarget))
                            Dim objFileSource As New FileInfo(String.Format("{0}\{1}", _
                                dirTargetSource.FullName, objEventParameter.FileNamePendukung2))
                            objFileSource.CopyTo(String.Format("{0}\{1}", _
                                dirTarget.FullName, objEventParameter.FileNamePendukung2))
                        End If
                    End If
                    If fuFilePendukung3.PostedFile.FileName.Length > 0 Then
                        objEventParameter.FileNamePendukung3 = String.Format("{0}({1}){2}", _
                            Path.GetFileNameWithoutExtension(fuFilePendukung3.PostedFile.FileName), _
                            dateUpload.ToString("yyyyMMdd HHmmss"), _
                            Path.GetExtension(fuFilePendukung3.PostedFile.FileName))
                    Else 'kalo beda pindahkan file ke dir baru
                        If objEventParameter.FileNamePendukung3.Length > 0 AndAlso _
                            objEventParameter.DirTarget <> dirTarget.Name Then
                            Dim dirTargetSource As New DirectoryInfo(String.Format("{0}{1}\{2}", _
                                KTB.DNet.Lib.WebConfig.GetValue("SAN"), _
                                KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), _
                                objEventParameter.DirTarget))
                            Dim objFileSource As New FileInfo(String.Format("{0}\{1}", _
                                dirTargetSource.FullName, objEventParameter.FileNamePendukung3))
                            objFileSource.CopyTo(String.Format("{0}\{1}", _
                                dirTarget.FullName, objEventParameter.FileNamePendukung3))
                        End If
                    End If
                    If fuFilePendukung4.PostedFile.FileName.Length > 0 Then
                        objEventParameter.FileNamePendukung4 = String.Format("{0}({1}){2}", _
                            Path.GetFileNameWithoutExtension(fuFilePendukung4.PostedFile.FileName), _
                            dateUpload.ToString("yyyyMMdd HHmmss"), _
                            Path.GetExtension(fuFilePendukung4.PostedFile.FileName))
                    Else 'kalo beda pindahkan file ke dir baru
                        If objEventParameter.FileNamePendukung4.Length > 0 AndAlso _
                            objEventParameter.DirTarget <> dirTarget.Name Then
                            Dim dirTargetSource As New DirectoryInfo(String.Format("{0}{1}\{2}", _
                                KTB.DNet.Lib.WebConfig.GetValue("SAN"), _
                                KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), _
                                objEventParameter.DirTarget))
                            Dim objFileSource As New FileInfo(String.Format("{0}\{1}", _
                                dirTargetSource.FullName, objEventParameter.FileNamePendukung4))
                            objFileSource.CopyTo(String.Format("{0}\{1}", _
                                dirTarget.FullName, objEventParameter.FileNamePendukung4))
                        End If
                    End If
                    If fuFilePendukung5.PostedFile.FileName.Length > 0 Then
                        objEventParameter.FileNamePendukung5 = String.Format("{0}({1}){2}", _
                            Path.GetFileNameWithoutExtension(fuFilePendukung5.PostedFile.FileName), _
                            dateUpload.ToString("yyyyMMdd HHmmss"), _
                            Path.GetExtension(fuFilePendukung5.PostedFile.FileName))
                    Else 'kalo beda pindahkan file ke dir baru
                        If objEventParameter.FileNamePendukung5.Length > 0 AndAlso _
                            objEventParameter.DirTarget <> dirTarget.Name Then
                            Dim dirTargetSource As New DirectoryInfo(String.Format("{0}{1}\{2}", _
                                KTB.DNet.Lib.WebConfig.GetValue("SAN"), _
                                KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), _
                                objEventParameter.DirTarget))
                            Dim objFileSource As New FileInfo(String.Format("{0}\{1}", _
                                dirTargetSource.FullName, objEventParameter.FileNamePendukung5))
                            objFileSource.CopyTo(String.Format("{0}\{1}", _
                                dirTarget.FullName, objEventParameter.FileNamePendukung5))
                        End If
                    End If
                    If fuFilePendukung6.PostedFile.FileName.Length > 0 Then
                        objEventParameter.FileNamePendukung6 = String.Format("{0}({1}){2}", _
                            Path.GetFileNameWithoutExtension(fuFilePendukung6.PostedFile.FileName), _
                            dateUpload.ToString("yyyyMMdd HHmmss"), _
                            Path.GetExtension(fuFilePendukung6.PostedFile.FileName))
                    Else 'kalo beda pindahkan file ke dir baru
                        If objEventParameter.FileNamePendukung6.Length > 0 AndAlso _
                            objEventParameter.DirTarget <> dirTarget.Name Then
                            Dim dirTargetSource As New DirectoryInfo(String.Format("{0}{1}\{2}", _
                                KTB.DNet.Lib.WebConfig.GetValue("SAN"), _
                                KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), _
                                objEventParameter.DirTarget))
                            Dim objFileSource As New FileInfo(String.Format("{0}\{1}", _
                                dirTargetSource.FullName, objEventParameter.FileNamePendukung6))
                            objFileSource.CopyTo(String.Format("{0}\{1}", _
                                dirTarget.FullName, objEventParameter.FileNamePendukung6))
                        End If
                    End If
                    If fuFilePendukung7.PostedFile.FileName.Length > 0 Then
                        objEventParameter.FileNamePendukung7 = String.Format("{0}({1}){2}", _
                            Path.GetFileNameWithoutExtension(fuFilePendukung7.PostedFile.FileName), _
                            dateUpload.ToString("yyyyMMdd HHmmss"), _
                            Path.GetExtension(fuFilePendukung1.PostedFile.FileName))
                    Else 'kalo beda pindahkan file ke dir baru
                        If objEventParameter.FileNamePendukung7.Length > 0 AndAlso _
                            objEventParameter.DirTarget <> dirTarget.Name Then
                            Dim dirTargetSource As New DirectoryInfo(String.Format("{0}{1}\{2}", _
                                KTB.DNet.Lib.WebConfig.GetValue("SAN"), _
                                KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), _
                                objEventParameter.DirTarget))
                            Dim objFileSource As New FileInfo(String.Format("{0}\{1}", _
                                dirTargetSource.FullName, objEventParameter.FileNamePendukung7))
                            objFileSource.CopyTo(String.Format("{0}\{1}", _
                                dirTarget.FullName, objEventParameter.FileNamePendukung7))
                        End If
                    End If
                    If fuFilePendukung8.PostedFile.FileName.Length > 0 Then
                        objEventParameter.FileNamePendukung8 = String.Format("{0}({1}){2}", _
                            Path.GetFileNameWithoutExtension(fuFilePendukung8.PostedFile.FileName), _
                            dateUpload.ToString("yyyyMMdd HHmmss"), _
                            Path.GetExtension(fuFilePendukung8.PostedFile.FileName))
                    Else 'kalo beda pindahkan file ke dir baru
                        If objEventParameter.FileNamePendukung8.Length > 0 AndAlso _
                            objEventParameter.DirTarget <> dirTarget.Name Then
                            Dim dirTargetSource As New DirectoryInfo(String.Format("{0}{1}\{2}", _
                                KTB.DNet.Lib.WebConfig.GetValue("SAN"), _
                                KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), _
                                objEventParameter.DirTarget))
                            Dim objFileSource As New FileInfo(String.Format("{0}\{1}", _
                                dirTargetSource.FullName, objEventParameter.FileNamePendukung8))
                            objFileSource.CopyTo(String.Format("{0}\{1}", _
                                dirTarget.FullName, objEventParameter.FileNamePendukung8))
                        End If
                    End If
                    If fuFilePendukung9.PostedFile.FileName.Length > 0 Then
                        objEventParameter.FileNamePendukung9 = String.Format("{0}({1}){2}", _
                            Path.GetFileNameWithoutExtension(fuFilePendukung1.PostedFile.FileName), _
                            dateUpload.ToString("yyyyMMdd HHmmss"), _
                            Path.GetExtension(fuFilePendukung1.PostedFile.FileName))
                    Else 'kalo beda pindahkan file ke dir baru
                        If objEventParameter.FileNamePendukung9.Length > 0 AndAlso _
                            objEventParameter.DirTarget <> dirTarget.Name Then
                            Dim dirTargetSource As New DirectoryInfo(String.Format("{0}{1}\{2}", _
                                KTB.DNet.Lib.WebConfig.GetValue("SAN"), _
                                KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), _
                                objEventParameter.DirTarget))
                            Dim objFileSource As New FileInfo(String.Format("{0}\{1}", _
                                dirTargetSource.FullName, objEventParameter.FileNamePendukung9))
                            objFileSource.CopyTo(String.Format("{0}\{1}", _
                                dirTarget.FullName, objEventParameter.FileNamePendukung9))
                        End If
                    End If
                    If fuFilePendukung10.PostedFile.FileName.Length > 0 Then
                        objEventParameter.FileNamePendukung10 = String.Format("{0}({1}){2}", _
                            Path.GetFileNameWithoutExtension(fuFilePendukung10.PostedFile.FileName), _
                            dateUpload.ToString("yyyyMMdd HHmmss"), _
                            Path.GetExtension(fuFilePendukung10.PostedFile.FileName))
                    Else 'kalo beda pindahkan file ke dir baru
                        If objEventParameter.FileNamePendukung10.Length > 0 AndAlso _
                            objEventParameter.DirTarget <> dirTarget.Name Then
                            Dim dirTargetSource As New DirectoryInfo(String.Format("{0}{1}\{2}", _
                                KTB.DNet.Lib.WebConfig.GetValue("SAN"), _
                                KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), _
                                objEventParameter.DirTarget))
                            Dim objFileSource As New FileInfo(String.Format("{0}\{1}", _
                                dirTargetSource.FullName, objEventParameter.FileNamePendukung10))
                            objFileSource.CopyTo(String.Format("{0}\{1}", _
                                dirTarget.FullName, objEventParameter.FileNamePendukung10))
                        End If
                    End If
                    If objEventParameter.DirTarget <> dirTarget.Name Then
                        objEventParameter.DirTarget = dirTarget.Name
                    End If
                    objEventParameter.RowStatus = CType(DBRowStatus.Active, Short)
                    objEventParameter.SalesmanArea = objSalesmanArea
                    objEventParameter.VechileType = objType

                    If (objFacade.IsExist(objEventParameter)) Then
                        MessageBox.Show("Data yang anda masukkan telah ada")
                        Return
                    End If

                    objEventParameterCollection.Add(objEventParameter)
                Next

                If fuFileMaterial.PostedFile.FileName.Length > 0 Then
                    UploadFile(fuFileMaterial.PostedFile, New FileInfo(String.Format("{0}\{1}({2}){3}", _
                        dirTarget.FullName, Path.GetFileNameWithoutExtension(fuFileMaterial.PostedFile.FileName), _
                        dateUpload.ToString("yyyyMMdd HHmmss"), _
                        Path.GetExtension(fuFileMaterial.PostedFile.FileName))))
                End If
                If fuFilePendukung1.PostedFile.FileName.Length > 0 Then
                    UploadFile(fuFilePendukung1.PostedFile, New FileInfo(String.Format("{0}\{1}({2}){3}", _
                        dirTarget.FullName, Path.GetFileNameWithoutExtension(fuFilePendukung1.PostedFile.FileName), _
                        dateUpload.ToString("yyyyMMdd HHmmss"), _
                        Path.GetExtension(fuFilePendukung1.PostedFile.FileName))))
                End If
                If fuFilePendukung2.PostedFile.FileName.Length > 0 Then
                    UploadFile(fuFilePendukung2.PostedFile, New FileInfo(String.Format("{0}\{1}({2}){3}", _
                        dirTarget.FullName, Path.GetFileNameWithoutExtension(fuFilePendukung2.PostedFile.FileName), _
                        dateUpload.ToString("yyyyMMdd HHmmss"), _
                        Path.GetExtension(fuFilePendukung2.PostedFile.FileName))))
                End If
                If fuFilePendukung3.PostedFile.FileName.Length > 0 Then
                    UploadFile(fuFilePendukung3.PostedFile, New FileInfo(String.Format("{0}\{1}({2}){3}", _
                        dirTarget.FullName, Path.GetFileNameWithoutExtension(fuFilePendukung3.PostedFile.FileName), _
                        dateUpload.ToString("yyyyMMdd HHmmss"), _
                        Path.GetExtension(fuFilePendukung3.PostedFile.FileName))))
                End If
                If fuFilePendukung4.PostedFile.FileName.Length > 0 Then
                    UploadFile(fuFilePendukung4.PostedFile, New FileInfo(String.Format("{0}\{1}({2}){3}", _
                        dirTarget.FullName, Path.GetFileNameWithoutExtension(fuFilePendukung4.PostedFile.FileName), _
                        dateUpload.ToString("yyyyMMdd HHmmss"), _
                        Path.GetExtension(fuFilePendukung4.PostedFile.FileName))))
                End If
                If fuFilePendukung5.PostedFile.FileName.Length > 0 Then
                    UploadFile(fuFilePendukung5.PostedFile, New FileInfo(String.Format("{0}\{1}({2}){3}", _
                        dirTarget.FullName, Path.GetFileNameWithoutExtension(fuFilePendukung5.PostedFile.FileName), _
                        dateUpload.ToString("yyyyMMdd HHmmss"), _
                        Path.GetExtension(fuFilePendukung5.PostedFile.FileName))))
                End If
                If fuFilePendukung6.PostedFile.FileName.Length > 0 Then
                    UploadFile(fuFilePendukung6.PostedFile, New FileInfo(String.Format("{0}\{1}({2}){3}", _
                        dirTarget.FullName, Path.GetFileNameWithoutExtension(fuFilePendukung6.PostedFile.FileName), _
                        dateUpload.ToString("yyyyMMdd HHmmss"), _
                        Path.GetExtension(fuFilePendukung6.PostedFile.FileName))))
                End If
                If fuFilePendukung7.PostedFile.FileName.Length > 0 Then
                    UploadFile(fuFilePendukung7.PostedFile, New FileInfo(String.Format("{0}\{1}({2}){3}", _
                        dirTarget.FullName, Path.GetFileNameWithoutExtension(fuFilePendukung7.PostedFile.FileName), _
                        dateUpload.ToString("yyyyMMdd HHmmss"), _
                        Path.GetExtension(fuFilePendukung7.PostedFile.FileName))))
                End If
                If fuFilePendukung8.PostedFile.FileName.Length > 0 Then
                    UploadFile(fuFilePendukung8.PostedFile, New FileInfo(String.Format("{0}\{1}({2}){3}", _
                        dirTarget.FullName, Path.GetFileNameWithoutExtension(fuFilePendukung8.PostedFile.FileName), _
                        dateUpload.ToString("yyyyMMdd HHmmss"), _
                        Path.GetExtension(fuFilePendukung8.PostedFile.FileName))))
                End If
                If fuFilePendukung9.PostedFile.FileName.Length > 0 Then
                    UploadFile(fuFilePendukung9.PostedFile, New FileInfo(String.Format("{0}\{1}({2}){3}", _
                        dirTarget.FullName, Path.GetFileNameWithoutExtension(fuFilePendukung9.PostedFile.FileName), _
                        dateUpload.ToString("yyyyMMdd HHmmss"), _
                        Path.GetExtension(fuFilePendukung9.PostedFile.FileName))))
                End If
                If fuFilePendukung10.PostedFile.FileName.Length > 0 Then
                    UploadFile(fuFilePendukung10.PostedFile, New FileInfo(String.Format("{0}\{1}({2}){3}", _
                        dirTarget.FullName, Path.GetFileNameWithoutExtension(fuFilePendukung10.PostedFile.FileName), _
                        dateUpload.ToString("yyyyMMdd HHmmss"), _
                        Path.GetExtension(fuFilePendukung10.PostedFile.FileName))))
                End If
                If fuJuklak.PostedFile.FileName.Length > 0 Then
                    UploadFile(fuJuklak.PostedFile, New FileInfo(String.Format("{0}\{1}({2}){3}", _
                        dirTarget.FullName, Path.GetFileNameWithoutExtension(fuJuklak.PostedFile.FileName), _
                        dateUpload.ToString("yyyyMMdd HHmmss"), _
                        Path.GetExtension(fuJuklak.PostedFile.FileName))))
                End If
                If GetID = 0 AndAlso objFacade.Insert(objEventParameterCollection) = objEventParameterCollection.Count Then
                    MessageBox.Show("Simpan berhasil")
                ElseIf GetID > 0 AndAlso objEventParameterCollection.Count = 1 Then
                    If objFacade.Update(objEventParameter) <> -1 Then
                        MessageBox.Show("Simpan berhasil")
                    Else
                        MessageBox.Show("Simpan gagal")
                    End If
                Else
                    MessageBox.Show("Simpan gagal")
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
        End If
    End Sub
    Private Sub lnbFile_Command(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles lnbFileMaterial.Command, lnbJuklak.Command
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim objFileInfo As New FileInfo(String.Format("{0}{1}\{2}", _
            KTB.DNet.Lib.WebConfig.GetValue("SAN"), _
            KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), _
            e.CommandArgument))
        If imp.Start() Then
            Response.Redirect(String.Format("~/Download.aspx?file={0}", objFileInfo.FullName))
            imp.StopImpersonate()
            imp = Nothing
        End If
    End Sub
#End Region

End Class