Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.BabitSalesComm
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports System.IO
Public Class FrmPengajuanDesignIklan
    Inherits System.Web.UI.Page

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanDesignIklanView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Pengajuan Design Iklan")
        End If
    End Sub

    Private Function CekDesignIklanCreatePrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanDesignIklanCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents UploadFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtNamaIklanDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDesc As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlNamaIklanKTB As System.Web.UI.WebControls.DropDownList
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
    Private sHelper As New SessionHelper
    Private obj As Dealer

    Private Sub bindDdl()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SampleIklan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim sortColl As SortCollection = New SortCollection

        sortColl.Add(New Sort(GetType(SampleIklan), "NoIklan", Sort.SortDirection.ASC))

        Dim arl As ArrayList = New SampleIklanFacade(User).Retrieve(criterias, sortColl)

        ddlNamaIklanKTB.DataSource = arl
        ddlNamaIklanKTB.DataTextField = "NoIklan"
        ddlNamaIklanKTB.DataTextField = "NoIklan"
        ddlNamaIklanKTB.DataBind()

        ddlNamaIklanKTB.Items.Insert(0, New ListItem("Silakan Pilih", ""))
    End Sub
    Private Sub ViewPengajuanDesignIklan(ByVal id As Integer)
        Dim obj As New PengajuanDesignIklan
        Dim objFacade As New PengajuanDesignIklanFacade(User)
        obj = objFacade.Retrieve(id)
        txtDealerCode.Text = obj.Dealer.DealerCode
        ddlNamaIklanKTB.SelectedItem.Text = obj.NamaIklanKTB.Trim
        ddlNamaIklanKTB.Enabled = False
        txtNamaIklanDealer.Text = obj.NamaIklanDealer
        txtNamaIklanDealer.Enabled = False
        txtDesc.Text = obj.Description
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        obj = CType(sHelper.GetSession("Dealer"), Dealer)
        InitiateAuthorization()
        If (Not obj Is Nothing) Then
            If obj.Title = EnumDealerTittle.DealerTittle.DEALER Then
                txtDealerCode.Text = obj.DealerCode
                txtDealerCode.Enabled = False
                lblSearchDealer.Visible = False
            Else
                txtDealerCode.Enabled = True
                lblSearchDealer.Visible = True
            End If
        End If

        lblSearchDealer.Attributes("onclick") = "ShowDealerSelection();"

        If Not IsPostBack Then
            bindDdl()
            If Not IsNothing(Request.QueryString("id")) Then
                ViewPengajuanDesignIklan(CType(Request.QueryString("id"), Integer))
                btnBack.Visible = True
            Else
                btnBack.Visible = False
            End If

            ' add security
            If Not CekDesignIklanCreatePrivilege() Then
                UploadFile.Visible = False
                btnSave.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If Not IsNothing(Request.QueryString("id")) Then
            'Modified by : Diana - Bugs 1316
            'Edit
            Dim obj As New PengajuanDesignIklan
            Dim objFacade As New PengajuanDesignIklanFacade(User)
            obj = objFacade.Retrieve(CType(Request.QueryString("id"), Integer))
            obj.Description = txtDesc.Text
            If UploadFile.PostedFile.FileName <> String.Empty Then
                Dim _filename As String = System.IO.Path.GetFileName(UploadFile.PostedFile.FileName)
                obj.UploadeIklan = Date.Now.ToString("ddMMyyhhmmss") & "\" & _filename
                If SaveFile(obj.UploadeIklan) Then
                    objFacade.Update(obj)
                    MessageBox.Show("Dokumen berhasil diupload")
                Else
                    MessageBox.Show("File gagal diupload")
                End If
            Else
                'File tetap menggunakan yang lama, hanya deskripsi saja yang diubah
                objFacade.Update(obj)
                MessageBox.Show("Dokumen berhasil diupload")
            End If
        Else
            'Insert
            If ValidateUpload() Then
                Dim obj As New PengajuanDesignIklan
                Dim objFacade As New PengajuanDesignIklanFacade(User)
                obj.Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text)
                obj.Description = txtDesc.Text.Trim
                obj.NamaIklanDealer = txtNamaIklanDealer.Text.Trim
                obj.NamaIklanKTB = ddlNamaIklanKTB.SelectedItem.Text.Trim
                obj.Status = EnumBabit.PengajuanDesignIklanStatus.Baru

                Dim _filename As String = System.IO.Path.GetFileName(UploadFile.PostedFile.FileName)
                obj.UploadeIklan = Date.Now.ToString("ddMMyyhhmmss") & "\" & _filename
                If SaveFile(obj.UploadeIklan) Then
                    Dim _nResult As Integer = 0
                    _nResult = objFacade.Insert(obj)
                    If _nResult >= 0 Then
                        ClearData()
                        MessageBox.Show("Dokumen berhasil diupload")
                    Else
                        MessageBox.Show("Dokumen gagal diupload")
                    End If
                Else
                    MessageBox.Show("File gagal diupload")
                End If

            End If
        End If
    End Sub
    Private Sub ClearData()
        If obj.Title <> EnumDealerTittle.DealerTittle.DEALER Then
            txtDealerCode.Text = String.Empty
        End If

        txtDesc.Text = String.Empty
        txtNamaIklanDealer.Text = String.Empty
        'txtNamaIklanKTB.Text = String.Empty
        ddlNamaIklanKTB.SelectedIndex = 0
    End Sub
    Private Function SaveFile(ByVal _filename As String) As Boolean
        Dim nResult As Boolean = False
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("IklanBABIT") & "\" & _filename      '-- Destination file
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

                Dim ext As String = System.IO.Path.GetExtension(UploadFile.PostedFile.FileName)
                If CheckExt(ext.Substring(1)) Then
                    'MaximumIklanFileSize
                    Dim MaxAllowedFileSize As Integer
                    MaxAllowedFileSize = 5120000

                    If UploadFile.PostedFile.ContentLength > MaxAllowedFileSize Then

                        Dim str As String = String.Empty
                        Dim DivValue As Decimal = 0

                        If MaxAllowedFileSize / 1000000 >= 1 Then
                            str = "MB"
                            DivValue = MaxAllowedFileSize / 1000000
                        Else
                            If MaxAllowedFileSize / 1000 >= 1 Then
                                str = "KB"
                                DivValue = MaxAllowedFileSize / 1000
                            Else
                                str = "Bytes"
                                DivValue = MaxAllowedFileSize
                            End If
                        End If

                        MessageBox.Show("File Gambar tidak boleh melebihi " + DivValue.ToString("#,##0") + " " + str)
                    Else
                        If finfo.Exists Then
                            finfo.Delete()
                        End If

                        UploadFile.PostedFile.SaveAs(DestFile)
                        nResult = True
                    End If
                Else
                    MessageBox.Show("Tipe file gambar invalid")
                End If

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            nResult = False
            Throw ex
        End Try
        Return nResult
    End Function
    Private Function CheckExt(ByVal ext As String) As Boolean
        Dim retValue As Boolean = False
        If ext.ToUpper() = "JPG" Or ext.ToUpper() = "GIF" Or ext.ToUpper() = "PNG" Or ext.ToUpper() = "BMP" Then
            retValue = True
        Else
            retValue = False
        End If
        Return retValue
    End Function
    Private Function ValidateUpload() As Boolean
        If txtDealerCode.Text = String.Empty Then
            MessageBox.Show("Kode dealer belum dipilih")
            Return False
        End If

        If ddlNamaIklanKTB.SelectedIndex = 0 Then
            MessageBox.Show("Iklan Ktb belum dipilih")
            Return False
        End If

        If (txtDealerCode.Text <> "") Then
            Dim arlDealer As ArrayList
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, txtDealerCode.Text))

            arlDealer = New DealerFacade(User).Retrieve(crit)
            If arlDealer.Count = 0 Then
                MessageBox.Show("Dealer " & txtDealerCode.Text & " tidak ada")
                Return False
            End If
        End If

        If txtDesc.Text.Trim = String.Empty Then
            MessageBox.Show("Keterangan belum diisi")
            Return False
        End If
        If txtNamaIklanDealer.Text.Trim = String.Empty Then
            MessageBox.Show("Nama Iklan Dealer belum diisi")
            Return False
        End If
        If UploadFile.Value = String.Empty Then
            MessageBox.Show("Tidak ada file yg di pilih!")
            Return False
        End If

        If (UploadFile.PostedFile.ContentLength = 0) Then
            MessageBox.Show("Path file yang Anda upload salah!")
            Return False
        End If

        'cek filesize
        Dim fileSize As Integer = 5120000
        If UploadFile.PostedFile.ContentLength > fileSize Then
            MessageBox.Show("Ukuran file tidak boleh melebihi 5 MB")
            Return False
        End If

        If IsExist() Then
            MessageBox.Show("Nama iklan dengan dealer sudah ada")
            Return False
        End If

        Return True
    End Function
    Private Function IsExist() As String
        Dim criterias As New CriteriaComposite(New Criteria(GetType(PengajuanDesignIklan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PengajuanDesignIklan), "NamaIklanKTB", MatchType.Exact, ddlNamaIklanKTB.SelectedItem.Text.Trim))
        criterias.opAnd(New Criteria(GetType(PengajuanDesignIklan), "NamaIklanDealer", MatchType.Exact, txtNamaIklanDealer.Text.Trim))
        criterias.opAnd(New Criteria(GetType(PengajuanDesignIklan), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Trim))
        Dim _arr As ArrayList = New PengajuanDesignIklanFacade(User).RetrieveByCriteria(criterias)
        If _arr.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    'Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
    '    Response.Redirect("../Babit/FrmDaftarPengajuanIklan.aspx")
    'End Sub
End Class
