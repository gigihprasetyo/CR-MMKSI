Imports System.Web.UI.WebControls
Imports System.IO
Imports KTB.DNet.Security

#Region "Custom Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.ShowroomAudit
Imports KTB.DNet.Utility
#End Region

Public Class FrmParameterAudit
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgPhotoList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents iDirection As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents iItemScore As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents txtAuditCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlYearPeriod As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnRilis As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents pnlHeader As System.Web.UI.WebControls.Panel

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Deklarasi"
    Dim sHelper As New SessionHelper
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.PetunjukPelaksanaanViewInput_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SHOWROOM AUDIT - Input Petunjuk Pelaksanaan ")
        End If
    End Sub

    Private Function CekJuklakInputDataPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.JuklakInputData_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

    Private Property SesDealer() As Dealer
        Get
            Return CType(sHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)

            sHelper.SetSession("DEALER", Value)
        End Set
    End Property
#Region "Custom Method"
    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Private Sub AddDataToGrid(ByVal e As DataGridCommandEventArgs)
        Dim txtDesc As TextBox = e.Item.FindControl("txtFooterDesc")

        If viewstate("IDEdit") <> "" Then
            'update
            Dim objAuditParam As AuditParameter = sHelper.GetSession("objAuditParameter")
            If txtDesc.Text = "" Then
                MessageBox.Show("Silahkan isi informasi Keterangan")
            Else
                Dim objParamFoto As New AuditParameterPhoto
                objParamFoto.Description = txtDesc.Text
                objParamFoto.AuditParameter = objAuditParam
                If (New AuditParameterPhotoFacade(User).Insert(objParamFoto) <> -1) Then
                    BindToControl(CInt(viewstate("IDEdit")))
                Else
                    MessageBox.Show("Gagal insert deskripsi")
                End If
            End If
        Else
            'insert new
            If txtDesc.Text = "" Then
                MessageBox.Show("Silahkan isi informasi Keterangan")
            Else
                Dim arlListPhoto As ArrayList = sHelper.GetSession("arlListPhoto")
                Dim objAuditfoto As New AuditParameterPhoto
                objAuditfoto.Description = txtDesc.Text

                arlListPhoto.Add(objAuditfoto)

                sHelper.SetSession("arlListPhoto", arlListPhoto)
                BindToGridTemp()
            End If
        End If

    End Sub

    Private Sub BindToGridTemp()
        Dim arlFotoTemp As ArrayList
        If viewstate("isEditMode") = "1" Then
            Dim crits As New CriteriaComposite(New Criteria(GetType(AuditParameterPhoto), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(AuditParameterPhoto), "AuditParameter.ID", MatchType.Exact, CInt(viewstate("IDEdit"))))
            arlFotoTemp = New AuditParameterPhotoFacade(User).Retrieve(crits)
            dtgPhotoList.DataSource = arlFotoTemp
            dtgPhotoList.DataBind()
        Else
            arlFotoTemp = sHelper.GetSession("arlListPhoto")
            If Not arlFotoTemp Is Nothing Then
                dtgPhotoList.DataSource = arlFotoTemp
                sHelper.SetSession("arlListPhoto", arlFotoTemp)
            Else
                dtgPhotoList.DataSource = New ArrayList
                sHelper.SetSession("arlListPhoto", New ArrayList)
            End If
            'sHelper.SetSession("arlListPhoto", arlFotoTemp)
            dtgPhotoList.DataBind()

        End If
    End Sub

    Private Sub BindYear()
        Dim i As Integer
        Dim yearMax As Integer = Year(Date.Now) + 5
        For i = Year(Date.Now) - 5 To yearMax
            ddlYearPeriod.Items.Add(i.ToString)
        Next
        ddlYearPeriod.SelectedIndex = -1
    End Sub

    Private Sub BindToControl(ByVal IDEdit As Integer)
        Dim objAuditParameter As AuditParameter = New AuditParameterFacade(User).Retrieve(IDEdit)
        txtAuditCode.Enabled = False
        txtAuditCode.Text = objAuditParameter.Code
        ddlYearPeriod.SelectedValue = objAuditParameter.Period

        'If objAuditParameter.IsRilis <> 0 Then
        '    btnSimpan.Enabled = False
        '    btnRilis.Enabled = False
        'Else
        '    btnSimpan.Enabled = True
        '    btnRilis.Enabled = True
        'End If

        If objAuditParameter.AuditParameterPhotos Is Nothing Then
            dtgPhotoList.DataSource = New ArrayList
        Else
            dtgPhotoList.DataSource = objAuditParameter.AuditParameterPhotos
        End If

        sHelper.SetSession("objAuditParameter", objAuditParameter)
        sHelper.SetSession("arlListPhoto", objAuditParameter.AuditParameterPhotos)
        'viewstate.Add("IDEdit", IDEdit)
        dtgPhotoList.DataBind()
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then

            If IsLoginAsDealer() Then
                pnlHeader.Visible = False
                btnSimpan.Visible = False
                btnRilis.Visible = False
                dtgPhotoList.ShowFooter = False
            Else
                dtgPhotoList.ShowFooter = True
            End If
            Dim IDEdit As String = CStr(sHelper.GetSession("IDEdit"))
            Dim isEditMode As String = CStr(sHelper.GetSession("isEditMode"))
            If IDEdit <> "" Then
                viewstate.Add("IDEdit", IDEdit)
                viewstate.Add("isEditMode", isEditMode)
                BindYear()
                BindToControl(CInt(IDEdit))
                btnBack.Visible = True
            Else
                btnBack.Visible = False
                BindYear()
                BindToGridTemp()
            End If
            'sHelper.RemoveSession("IDEdit")

            ' add security
            If Not CekJuklakInputDataPrivilege() Then
                btnSimpan.Enabled = False
                btnRilis.Enabled = False
                iDirection.Visible = False
                iItemScore.Visible = False
                dtgPhotoList.ShowFooter = False
                dtgPhotoList.Columns(2).Visible = False     'icon edit / modify
                dtgPhotoList.Columns(3).Visible = False     'icon delete
            End If
        End If
    End Sub

    Private Sub dtgPhotoList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPhotoList.ItemCommand
        If e.CommandName = "Add" Then
            AddDataToGrid(e)
        ElseIf e.CommandName = "Delete" Then
            If viewstate("IDEdit") <> "" Then
                Dim lblID As Label = CType(e.Item.FindControl("ID"), Label)
                'memeriksa data AuditScheduleDealerReport, foreign key related = AuditParameterPhotoID
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealerReport), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(AuditScheduleDealerReport), "AuditParameterPhoto.ID", MatchType.Exact, CType(lblID.Text, Integer)))

                Dim arrTmp As ArrayList = New AuditScheduleDealerReportFacade(User).Retrieve(criterias)
                If arrTmp.Count > 0 Then
                    MessageBox.Show("Data ini tidak didelete, karena sudah memiliki data AuditScheduleDealerReport")
                    Return
                End If

                Dim objAuditParam As AuditParameter = sHelper.GetSession("objAuditParameter")
                Dim objFotoParam As AuditParameterPhoto = objAuditParam.AuditParameterPhotos(e.Item.ItemIndex)
                Dim facade As AuditParameterPhotoFacade = New AuditParameterPhotoFacade(User)
                facade.DeleteFromDB(objFotoParam)
                BindToControl(CInt(viewstate("IDEdit")))
            Else
                Dim arlFotoTemp As ArrayList = sHelper.GetSession("arlListPhoto")
                arlFotoTemp.RemoveAt(e.Item.ItemIndex)
                sHelper.SetSession("arlListPhoto", arlFotoTemp)
                BindToGridTemp()
            End If
        End If

    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If viewstate("IDEdit") <> "" Then
            'update
            Dim objAuditParam As AuditParameter = sHelper.GetSession("objAuditParameter")
            objAuditParam.Period = ddlYearPeriod.SelectedValue

            If iDirection.Value <> "" OrElse iItemScore.Value <> "" Then
                If iDirection.Value <> "" Then
                    'cek maxFileSize first
                    Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

                    If iDirection.PostedFile.ContentLength > maxFileSize Then
                        MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                        Exit Sub
                    End If

                    'get file JukLak
                    Dim SrcFile As String = Path.GetFileName(iDirection.PostedFile.FileName)  '-- Source file name
                    Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("Audit") & "\" & SrcFile     '-- Destination file
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
                            iDirection.PostedFile.SaveAs(DestFile)
                            objAuditParam.JukLakFile = KTB.DNet.Lib.WebConfig.GetValue("Audit") & "\" & SrcFile
                            imp.StopImpersonate()
                            imp = Nothing
                        End If
                    Catch ex As Exception
                        Throw ex
                    End Try
                End If


                If iItemScore.Value <> "" Then
                    'cek maxFileSize first
                    Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

                    If iItemScore.PostedFile.ContentLength > maxFileSize Then
                        MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                        Exit Sub
                    End If

                    'get file Assessment Item
                    Dim SrcFileS As String = Path.GetFileName(iItemScore.PostedFile.FileName)  '-- Source file name
                    Dim DestFileS As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("Audit") & "\" & SrcFileS     '-- Destination file
                    Dim _userI As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                    Dim _passwordI As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                    Dim _webServerI As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                    Dim imps As SAPImpersonate = New SAPImpersonate(_userI, _passwordI, _webServerI)
                    Dim successI As Boolean = False
                    Dim finfoI As New FileInfo(DestFileS)
                    Try
                        successI = imps.Start()
                        If successI Then
                            If Not finfoI.Directory.Exists Then
                                Directory.CreateDirectory(finfoI.DirectoryName)
                            End If
                            iItemScore.PostedFile.SaveAs(DestFileS)
                            objAuditParam.AssessmentItem = KTB.DNet.Lib.WebConfig.GetValue("Audit") & "\" & SrcFileS
                            imps.StopImpersonate()
                            imps = Nothing
                        End If
                    Catch ex As Exception
                        Throw ex
                    End Try
                End If
                If (New AuditParameterFacade(User).Update(objAuditParam) <> -1) Then
                    MessageBox.Show("Data berhasil diupdate")
                Else
                    MessageBox.Show("Data gagal diupdate")
                End If
            Else
                Dim arlTempPhoto As ArrayList = sHelper.GetSession("arlListPhoto")
                If (New AuditParameterFacade(User).Update(objAuditParam, arlTempPhoto) <> -1) Then
                    MessageBox.Show("Update berhasil dilakukan")
                Else
                    MessageBox.Show("Update gagal dilakukan")
                End If
            End If
        Else
            'insert
            If txtAuditCode.Text <> "" Then
                Dim objDomain As New AuditParameter
                objDomain.Code = txtAuditCode.Text.Trim
                objDomain.Period = ddlYearPeriod.SelectedValue

                Dim facade As New AuditParameterFacade(User)
                If facade.ValidateCode(objDomain.Code) > 0 Then
                    MessageBox.Show("Kode sudah pernah digunakan sebelumnya")
                Else
                    If iDirection.Value <> "" OrElse iItemScore.Value <> "" Then

                        If iDirection.Value <> "" Then
                            'cek maxFileSize first
                            Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

                            If iDirection.PostedFile.ContentLength > maxFileSize Then
                                MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                                Exit Sub
                            End If

                            'get file JukLak
                            Dim SrcFile As String = Path.GetFileName(iDirection.PostedFile.FileName)  '-- Source file name
                            Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("Audit") & "\" & SrcFile     '-- Destination file
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
                                    iDirection.PostedFile.SaveAs(DestFile)
                                    objDomain.JukLakFile = KTB.DNet.Lib.WebConfig.GetValue("Audit") & "\" & SrcFile
                                    imp.StopImpersonate()
                                    imp = Nothing
                                End If
                            Catch ex As Exception
                                Throw ex
                            End Try
                        Else
                            MessageBox.Show("File Petunjuk Pelaksanaan tidak ditemukan. Silakan upload lebih dulu.")
                            Exit Sub
                        End If

                        If iItemScore.Value <> "" Then
                            'cek maxFileSize first
                            Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

                            If iItemScore.PostedFile.ContentLength > maxFileSize Then
                                MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                                Exit Sub
                            End If

                            'get file Assessment Item
                            Dim SrcFileS As String = Path.GetFileName(iItemScore.PostedFile.FileName)  '-- Source file name
                            Dim DestFileS As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("Audit") & "\" & SrcFileS     '-- Destination file
                            Dim _userI As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                            Dim _passwordI As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                            Dim _webServerI As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                            Dim imps As SAPImpersonate = New SAPImpersonate(_userI, _passwordI, _webServerI)
                            Dim successI As Boolean = False
                            Dim finfoI As New FileInfo(DestFileS)
                            Try
                                successI = imps.Start()
                                If successI Then
                                    If Not finfoI.Directory.Exists Then
                                        Directory.CreateDirectory(finfoI.DirectoryName)
                                    End If
                                    iItemScore.PostedFile.SaveAs(DestFileS)
                                    objDomain.AssessmentItem = KTB.DNet.Lib.WebConfig.GetValue("Audit") & "\" & SrcFileS
                                    imps.StopImpersonate()
                                    imps = Nothing
                                End If
                            Catch ex As Exception
                                Throw ex
                            End Try
                        Else
                            MessageBox.Show("File Item Penilaian tidak ditemukan. Silakan upload lebih dulu.")
                            Exit Sub
                        End If


                        Dim arlTempPhoto As ArrayList = sHelper.GetSession("arlListPhoto")
                        For Each item As AuditParameterPhoto In arlTempPhoto
                            objDomain.AuditParameterPhotos.Add(item)
                        Next

                        If facade.Insert(objDomain) <> -1 Then
                            MessageBox.Show("Data berhasil disimpan")
                        Else
                            MessageBox.Show("Data gagal disimpan")
                        End If
                    Else
                        MessageBox.Show("File Petunjuk Pelaksanaan atau Item Penilaian tidak ditemukan. Silakan upload lebih dulu.")
                    End If
                End If
            Else
                MessageBox.Show("Kode Audit harus dimasukkan!")
            End If
        End If

    End Sub

    Private Sub btnRilis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRilis.Click
        If txtAuditCode.Text <> "" Then
            Dim kodeAudit As String = txtAuditCode.Text.Trim
            Dim iFacade As New AuditParameterFacade(User)
            Dim objAuditParam As AuditParameter = iFacade.Retrieve(kodeAudit)
            If Not objAuditParam Is Nothing Then
                If objAuditParam.ID = 0 Then
                    MessageBox.Show("Data harus disimpan terlebih dahulu")
                Else
                    objAuditParam.IsRilis = 1
                    If iFacade.Update(objAuditParam) <> -1 Then
                        MessageBox.Show("Data berhasil di-Rilis")
                    Else
                        MessageBox.Show("Data tidak berhasil di-Rilis")
                    End If
                End If
            End If
        Else
            MessageBox.Show("Kode Audit harus diisi!")
        End If
    End Sub

    Private Sub dtgPhotoList_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPhotoList.UpdateCommand

        If viewstate("IDEdit") <> "" Then
            Dim id As Integer = CType(viewstate("IDDesc"), Integer)
            Dim txtDesc As TextBox = CType(e.Item.FindControl("txtEditDesc"), TextBox)
            Dim objAuditParamDetailEdit As AuditParameterPhoto = New AuditParameterPhotoFacade(User).Retrieve(id)
            objAuditParamDetailEdit.Description = txtDesc.Text
            If (New AuditParameterPhotoFacade(User).Update(objAuditParamDetailEdit) <> -1) Then
                dtgPhotoList.EditItemIndex = -1
                BindToGridTemp()
                MessageBox.Show("Update berhasil")
            Else
                dtgPhotoList.EditItemIndex = -1
                BindToGridTemp()
                MessageBox.Show("Update Gagal")
            End If
        Else
            dtgPhotoList.ShowFooter = True
            Dim arrDataUpd As ArrayList = sHelper.GetSession("arlListPhoto")
            Dim txtDesc As TextBox = CType(e.Item.FindControl("txtEditDesc"), TextBox)

            Dim objAuditParamDetail As AuditParameterPhoto = arrDataUpd(e.Item.ItemIndex)
            objAuditParamDetail.Description = txtDesc.Text

            arrDataUpd.RemoveAt(e.Item.ItemIndex)
            arrDataUpd.Insert(e.Item.ItemIndex, objAuditParamDetail)
            sHelper.SetSession("arlListPhoto", arrDataUpd)
            dtgPhotoList.EditItemIndex = -1
            BindToGridTemp()
        End If

    End Sub

    Private Sub dtgPhotoList_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPhotoList.EditCommand
        Dim no As Integer = e.Item.ItemIndex
        Dim id As Label = CType(e.Item.FindControl("ID"), Label)
        viewstate.Add("IDDesc", id.Text)
        dtgPhotoList.EditItemIndex = no
        dtgPhotoList.ShowFooter = False
        BindToGridTemp()
    End Sub

    Private Sub dtgPhotoList_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPhotoList.CancelCommand
        dtgPhotoList.EditItemIndex = -1
        If viewstate("IDEdit") <> "" Then
            dtgPhotoList.ShowFooter = False
        Else
            dtgPhotoList.ShowFooter = True
        End If
        BindToGridTemp()
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If Not IsLoginAsDealer() Then
            Response.Redirect("../ShowroomAudit/FrmParameterAuditList.aspx", True)
        Else
            Response.Redirect("FrmListAuditSchedule.aspx", True)
        End If

    End Sub
End Class
