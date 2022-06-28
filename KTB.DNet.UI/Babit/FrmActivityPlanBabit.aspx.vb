Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.BabitSalesComm
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Security

Public Class FrmActivityPlanBabit
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents fileUploadActivityPlan As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents txtBabitActivity As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlStartPeriod As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlEndPeriod As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealers As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTahunTo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnBack As System.Web.UI.HtmlControls.HtmlInputButton

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

#Region "Custom Metode"
    Private Sub BindYear()
        ddlYear.Items.Clear()
        ddlYear.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For i As Integer = DateTime.Today.Year - 5 To DateTime.Today.Year + 1
            ddlYear.Items.Add(New ListItem(i.ToString, i))
        Next

        ddlTahunTo.Items.Clear()
        ddlTahunTo.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For i As Integer = DateTime.Today.Year - 5 To DateTime.Today.Year + 1
            ddlTahunTo.Items.Add(New ListItem(i.ToString, i))
        Next
    End Sub
    Private Sub BindMonth()
        CommonFunction.BindFromEnum("Month", ddlStartPeriod, User, False, "NameStatus", "ValStatus")
        CommonFunction.BindFromEnum("Month", ddlEndPeriod, User, False, "NameStatus", "ValStatus")
        'ddlJenisKegiatan.Items.Insert(0, New ListItem("Silakan Pilih", "-1"))
        ddlStartPeriod.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        ddlEndPeriod.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
    End Sub

    Private Sub ClearControl()
        txtBabitActivity.Text = ""
        txtDescription.Text = ""
        BindYear()
        BindMonth()
    End Sub

    Private Sub MapToControl(ByVal objDealerActivity As DealerActivityPlanning)
        txtDealerCode.Text = objDealerActivity.Dealer.DealerCode
        txtBabitActivity.Text = objDealerActivity.BabitActivitiy
        txtDescription.Text = objDealerActivity.Description

        ddlStartPeriod.SelectedValue = objDealerActivity.StartPeriodMonth
        ddlEndPeriod.SelectedValue = objDealerActivity.EndPeriodMonth
        ddlYear.SelectedValue = objDealerActivity.PeriodYear
        ddlTahunTo.SelectedValue = objDealerActivity.PeriodYearEnd
    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.RencanaAkctivitasUploadView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Upload Rencana Aktivitas")
        End If
    End Sub

    Private Function CekAkctivitasUploadPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.RencanaAkctivitasUpload_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            Dim objDealerActivity As DealerActivityPlanning = sHelper.GetSession("objDealerActivity")
            Dim objDealer As Dealer = sHelper.GetSession("DEALER")

            If IsLoginAsDealer() Then
                txtDealerCode.Text = objDealer.DealerCode
                txtDealerCode.Enabled = False
                lblDealers.Visible = False
            Else
                lblDealers.Attributes("onclick") = "ShowPPDealerSelection()"
                txtDealerCode.Enabled = True
                lblDealers.Visible = True
            End If

            BindYear()
            BindMonth()

            If sHelper.GetSession("IEditMode") = 1 Then
                'model edit
                MapToControl(objDealerActivity)
                txtDealerCode.Enabled = False
                lblDealers.Visible = False
                btnBack.Visible = True
                btnUpload.Text = "Simpan"
                viewstate.Add("IEditMode", 1)
                btnBack.Visible = True
            Else
                'txtDealerCode.Enabled = True
                'lblDealers.Visible = True
                btnBack.Visible = False
                btnUpload.Text = "Upload"
                viewstate.Add("IEditMode", 0)
                btnBack.Visible = False
            End If

            ' add security
            If Not CekAkctivitasUploadPrivilege() Then
                fileUploadActivityPlan.Visible = False
                btnUpload.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        If txtDealerCode.Text = "" Then
            MessageBox.Show("Kode Dealer harus diisi")
            Exit Sub
        End If
        If (ddlStartPeriod.SelectedValue = -1 Or ddlEndPeriod.SelectedValue = -1) Then
            MessageBox.Show("Period harus diisi")
            Exit Sub
        End If
        If (ddlYear.SelectedValue = -1) Then
            MessageBox.Show("Tahun harus diisi")
            Exit Sub
        End If
        If (ddlTahunTo.SelectedValue = -1) Then
            MessageBox.Show("Tahun harus diisi")
            Exit Sub
        End If
        If (CInt(ddlYear.SelectedValue) <> -1 And CInt(ddlTahunTo.SelectedValue) <> -1) Then
            If (CInt(ddlYear.SelectedValue) = CInt(ddlTahunTo.SelectedValue)) Then
                If (CInt(ddlStartPeriod.SelectedValue) <> -1 And CInt(ddlEndPeriod.SelectedValue) <> -1) Then
                    If CInt(ddlStartPeriod.SelectedValue) > CInt(ddlEndPeriod.SelectedValue) Then
                        MessageBox.Show("Periode awal harus lebih kecil dari akhir periode")
                        Exit Sub
                    End If
                End If
            ElseIf (CInt(ddlYear.SelectedValue) > CInt(ddlTahunTo.SelectedValue)) Then
                MessageBox.Show("Periode awal harus lebih kecil dari akhir periode")
                Exit Sub
            End If
        End If

        'If (CInt(ddlYear.SelectedValue) > CInt(ddlTahunTo.SelectedValue)) Then
        '    MessageBox.Show("Tahun periode awal dari tidak bisa lebih besar dari tahun periode akhir")
        '    Exit Sub
        'ElseIf (CInt(ddlYear.SelectedValue) = CInt(ddlTahunTo.SelectedValue)) Then
        '    If CInt(ddlStartPeriod.SelectedValue) >= CInt(ddlEndPeriod.SelectedValue) Then
        '        MessageBox.Show("Bulan periode awal tidak boleh lebih besar atau sama dengan dari periode akhir.")
        '        Exit Sub
        '    End If
        'End If

        'If (CInt(ddlYear.SelectedValue) < DateTime.Now.Year) Then
        '    MessageBox.Show("Tahun periode tidak boleh lebih kecil dari tahun sekarang")
        '    Exit Sub
        'ElseIf (CInt(ddlYear.SelectedValue) = DateTime.Now.Year And CInt(ddlTahunTo.SelectedValue) = DateTime.Now.Year) Then
        '    If (CInt(ddlStartPeriod.SelectedValue) <= DateTime.Now.Month) Then
        '        If (CInt(ddlEndPeriod.SelectedValue) < DateTime.Now.Month) Then
        '            MessageBox.Show("Periode tidak bisa lebih kecil dari tanggal sekarang")
        '            Exit Sub
        '        End If
        '    End If
        'End If

        If viewstate("IEditMode") = 0 Then
            Dim kodeDealer() As String = txtDealerCode.Text.Split(";")
            Dim arlActivity As New ArrayList

            If fileUploadActivityPlan.Value = "" OrElse fileUploadActivityPlan.Value = Nothing Then
                MessageBox.Show("Masukkan file yang akan diupload")
            Else
                'cek size
                If fileUploadActivityPlan.PostedFile.ContentLength > CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize")) Then
                    MessageBox.Show("File tidak boleh melebihi 500kb")
                Else
                    For Each item As String In kodeDealer
                        Dim objDealer As New Dealer
                        objDealer = New DealerFacade(User).Retrieve(item)

                        Dim objDealerActivity As New DealerActivityPlanning
                        objDealerActivity.BabitActivitiy = txtBabitActivity.Text.Trim.ToUpper
                        objDealerActivity.StartPeriodMonth = ddlStartPeriod.SelectedValue
                        objDealerActivity.EndPeriodMonth = ddlEndPeriod.SelectedValue
                        objDealerActivity.PeriodYear = ddlYear.SelectedValue
                        objDealerActivity.PeriodYearEnd = ddlTahunTo.SelectedValue
                        objDealerActivity.Description = txtDescription.Text
                        objDealerActivity.Dealer = objDealer

                        'upload rencana aktivitas babit
                        Dim SrcFile As String = Path.GetFileName(fileUploadActivityPlan.PostedFile.FileName)  '-- Source file name
                        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("BABITActivity") & "\" & SrcFile     '-- Destination file
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
                                fileUploadActivityPlan.PostedFile.SaveAs(DestFile)
                                objDealerActivity.ActivityPlanning = KTB.DNet.Lib.WebConfig.GetValue("BABITActivity") & "\" & SrcFile
                                imp.StopImpersonate()
                                imp = Nothing
                            End If
                        Catch ex As Exception
                            Throw ex
                        End Try

                        arlActivity.Add(objDealerActivity)
                    Next

                    'try to insert to table
                    Dim objDealerActivityFacade As New DealerActivityPlanningFacade(User)
                    If (objDealerActivityFacade.InsertTransaction(arlActivity) = 1) Then
                        MessageBox.Show("Insert data berhasil")
                        ClearControl()
                    Else
                        MessageBox.Show("Insert Data gagal!")
                    End If
                End If

            End If
        Else
            'update data
            If fileUploadActivityPlan.Value = "" OrElse fileUploadActivityPlan.Value = Nothing Then
                'MessageBox.Show("Masukkan file yang akan diupload")
                'upload tanpa file baru
                Dim objDealerActivityE As DealerActivityPlanning = sHelper.GetSession("objDealerActivity")
                objDealerActivityE.BabitActivitiy = txtBabitActivity.Text.Trim.ToUpper
                objDealerActivityE.StartPeriodMonth = ddlStartPeriod.SelectedValue
                objDealerActivityE.EndPeriodMonth = ddlEndPeriod.SelectedValue
                objDealerActivityE.PeriodYear = ddlYear.SelectedValue
                objDealerActivityE.PeriodYearEnd = ddlTahunTo.SelectedValue
                objDealerActivityE.Description = txtDescription.Text

                'try to update the data
                Dim objDealerActivityFacadeE As New DealerActivityPlanningFacade(User)
                Try
                    objDealerActivityFacadeE.Update(objDealerActivityE)
                Catch ex As Exception
                    MessageBox.Show("Update Data Gagal")
                    Exit Sub
                End Try
                Response.Redirect("FrmListDealerActivityBabit.aspx", True)
            Else
                'cek size
                If fileUploadActivityPlan.PostedFile.ContentLength > CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize")) Then
                    MessageBox.Show("File tidak boleh melebihi 500kb")
                Else
                    'update dengan melakukan upload file baru
                    Dim objDealerActivityE As DealerActivityPlanning = sHelper.GetSession("objDealerActivity")
                    objDealerActivityE.BabitActivitiy = txtBabitActivity.Text.Trim.ToUpper
                    objDealerActivityE.StartPeriodMonth = ddlStartPeriod.SelectedValue
                    objDealerActivityE.EndPeriodMonth = ddlEndPeriod.SelectedValue
                    objDealerActivityE.PeriodYear = ddlYear.SelectedValue
                    objDealerActivityE.Description = txtDescription.Text

                    'upload rencana aktivitas babit
                    Dim SrcFileE As String = Path.GetFileName(fileUploadActivityPlan.PostedFile.FileName)  '-- Source file name
                    Dim DestFileE As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("BABITActivity") & "\" & SrcFileE     '-- Destination file
                    Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                    Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                    Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                    Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                    Dim success As Boolean = False
                    Dim finfo As New FileInfo(DestFileE)
                    Try
                        success = imp.Start()
                        If success Then
                            If Not finfo.Directory.Exists Then
                                Directory.CreateDirectory(finfo.DirectoryName)
                            End If
                            fileUploadActivityPlan.PostedFile.SaveAs(DestFileE)
                            objDealerActivityE.ActivityPlanning = KTB.DNet.Lib.WebConfig.GetValue("BABITActivity") & "\" & SrcFileE

                            imp.StopImpersonate()
                            imp = Nothing
                        End If
                    Catch ex As Exception
                        Throw ex
                    End Try

                    'try to update the data
                    Dim objDealerActivityFacadeE As New DealerActivityPlanningFacade(User)
                    Try
                        objDealerActivityFacadeE.Update(objDealerActivityE)
                    Catch ex As Exception
                        MessageBox.Show("Update Data Gagal")
                        Exit Sub
                    End Try
                    Response.Redirect("FrmListDealerActivityBabit.aspx", True)
                End If
            End If
        End If
    End Sub

    'Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
    '    Response.Redirect("FrmListDealerActivityBabit.aspx?isBack=true")
    'End Sub
End Class
