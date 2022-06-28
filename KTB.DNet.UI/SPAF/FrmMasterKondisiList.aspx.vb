#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.SPAF
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Drawing.Color
#End Region

Public Class FrmMasterKondisiList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblFileName As System.Web.UI.WebControls.Label
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private _sessHelper As SessionHelper = New SessionHelper
    Protected WithEvents dgMasterKondisi As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents chkTglBerlaku As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents dfMasterKondisi As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents ddlDocumentType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTipeDocument As System.Web.UI.WebControls.Label
    Protected WithEvents lblTipe As System.Web.UI.WebControls.Label
    Protected WithEvents icTglBerlakuFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglBerlakuTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents tblInput As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents ctlLstTipeKendaraan As System.Web.UI.WebControls.ListBox
    Private bIsError As Boolean = False

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private dt As DateTime = DateTime.Now
    Private Suffix As String = CType(dt.Year, String) & CType(dt.Month, String) & CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & CType(dt.Second, String) & CType(dt.Millisecond, String)

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.SPAFConditionListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SPAF - Daftar Kondisi")
        End If
    End Sub

    Private Function CheckBtnUpPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.SPAFConditionListUpload_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CheckBtnDownPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.SPAFConditionListDownload_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CheckBtnGridPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.SPAFConditionListDelete_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Custom Method"

    Private Sub BindDropDownTipe()

        ctlLstTipeKendaraan.Items.Clear()
        ctlLstTipeKendaraan.DataSource = New VechileTypeFacade(User).RetrieveActiveList()
        ctlLstTipeKendaraan.DataTextField = "VechileCodeDesc"
        ctlLstTipeKendaraan.DataValueField = "ID"
        ctlLstTipeKendaraan.DataBind()
        ctlLstTipeKendaraan.Items.Insert(0, New ListItem("Pilih Semua", -1))


    End Sub
    Private Sub BindDropDownDocumentType()
        ddlDocumentType.Items.Clear()
        ddlDocumentType.DataSource = EnumDocumentType.RetrieveDocumentType
        ddlDocumentType.DataTextField = "Name"
        ddlDocumentType.DataValueField = "ID"
        ddlDocumentType.DataBind()
        ddlDocumentType.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub
    Private Sub BindDropDownStatus()
        ddlStatus.Items.Clear()
        ddlStatus.Items.Add(New ListItem("Active", DBRowStatus.Active))
        ddlStatus.Items.Add(New ListItem("Not Active", DBRowStatus.Deleted))
    End Sub
    Private Sub Initialize()
        btnSimpan.Enabled = False
        btnDownload.Enabled = False
        icTglBerlakuFrom.Value = DateTime.Today
        icTglBerlakuTo.Value = DateTime.Today
        chkTglBerlaku.Checked = False
        icTglBerlakuFrom.Enabled = chkTglBerlaku.Checked
        icTglBerlakuTo.Enabled = chkTglBerlaku.Checked
    End Sub
    Private Sub BindUpload()

        Dim objUpload As New UploadToWebServer

        _sessHelper.SetSession("dgType", "Upload")

        If (Not dfMasterKondisi.PostedFile Is Nothing) And (dfMasterKondisi.PostedFile.ContentLength > 0) And _
            ((dfMasterKondisi.PostedFile.ContentType.ToLower() = "text/plain") Or _
            (dfMasterKondisi.PostedFile.ContentType.ToLower() = "text/csv")) Then

            '       Or (dfMasterKondisi.PostedFile.ContentType.ToLower() = "application/octet-stream") Or (dfMasterKondisi.PostedFile.ContentType.ToLower() = "application/vnd.ms-excel")) 
            Dim Extension As String = Path.GetExtension(dfMasterKondisi.PostedFile.FileName)

            If Extension.ToUpper = ".TXT" Then

                'cek maxFileSize first
                Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

                If dfMasterKondisi.PostedFile.ContentLength > maxFileSize Then
                    MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                    Exit Sub
                End If

                Dim SrcFile As String = Path.GetFileName(dfMasterKondisi.PostedFile.FileName)
                Dim DestFile As String = Server.MapPath("") & "\..\DataTemp\" & SrcFile
                Try

                    ''---------- Pake UploadToWebServer saja -----------
                    objUpload.Upload(dfMasterKondisi.PostedFile.InputStream, DestFile)

                    Dim parser As IParser = New ConditionMasterParser
                    Dim arList As ArrayList = CType(parser.ParseNoTransaction(DestFile, "User"), ArrayList)

                    'periksa tidak boleh ada chassis# dan kind# yang sama di tabel ConditionMaster
                    Dim NewList As ArrayList = New ArrayList
                    NewList = CheckExistingInMasterKondisiList(arList)
                    AddUserCreator(NewList)

                    'periksa kalo ada record rangkap (chassis# dan kind# yang sama) di arraylist
                    CheckDoubleRows(NewList)

                    _sessHelper.SetSession("sessMasterKondisi", NewList)
                    BinddgMasterKondisi(0)

                Catch Exc As Exception
                    MessageBox.Show(SR.UploadFail(SrcFile))
                    _sessHelper.SetSession("sessError", True)
                Finally
                    objUpload.DeleteFile(DestFile)
                End Try
            Else
                _sessHelper.SetSession("sessError", True)
                MessageBox.Show("Jenis file tidak sesuai")
            End If
        Else
            If Not IsNothing(dfMasterKondisi.PostedFile) And (dfMasterKondisi.PostedFile.ContentLength > 0) Then
                MessageBox.Show("Jenis file tidak sesuai")
            Else
                MessageBox.Show("Pilih file yang akan di-upload.")
            End If
            _sessHelper.SetSession("sessError", True)
        End If

    End Sub
    Private Sub AddUserCreator(ByRef arrL As ArrayList)
        Dim pph As Decimal = (New PPhFacade(User)).RetrievePPh
        For Each objConditionMaster As V_LeasingDaftarKondisi In arrL
            objConditionMaster.PPhPercent = pph
            'objConditionMaster.PPh = (pph / 100) * objConditionMaster.RetailPrice
            objConditionMaster.PPh = objConditionMaster.PPh
            objConditionMaster.CreatedBy = CType(User.Identity.Name, String)
        Next
    End Sub
    Private Sub CheckDoubleRows(ByRef NewList As ArrayList)
        Dim nIndex As Integer
        Dim nIterate As Integer = 1
        For Each objConditionMaster As V_LeasingDaftarKondisi In NewList
            Try
                If objConditionMaster.VechileTypeID > 0 And Not IsNothing(objConditionMaster.ValidFrom) Then
                    For nIndex = nIterate To NewList.Count - 1
                        Dim objConditionMaster2 As V_LeasingDaftarKondisi
                        objConditionMaster2 = NewList(nIndex)
                        If Not IsNothing(objConditionMaster2.VechileType) And Not IsNothing(objConditionMaster2.ValidFrom) Then
                            Dim sVechileTypeCode2 = objConditionMaster2.VechileType.VechileTypeCode
                            Dim sVechileTypeCode1 = objConditionMaster.VechileType.VechileTypeCode

                            Dim sValidDate1 = objConditionMaster.ValidFrom
                            Dim sValidDate2 = objConditionMaster2.ValidFrom

                            If sVechileTypeCode1 = sVechileTypeCode2 And sValidDate1 = sValidDate2 Then
                                If objConditionMaster2.ErrorMessage = "" Then
                                    objConditionMaster2.ErrorMessage = "Data Master Kondisi Ganda dg Record " + CType(nIterate, String)
                                Else
                                    objConditionMaster2.ErrorMessage = objConditionMaster2.ErrorMessage + ";<br> Data Master Kondisi Ganda dg Record " + CType(nIterate, String)
                                End If
                            End If
                        End If
                    Next
                End If
                If nIterate = 1 Then
                    ChangeDisplayColumn(objConditionMaster.DocumentType)
                End If
                nIterate = nIterate + 1
            Catch ex As Exception

            End Try
        Next

    End Sub
    Private Function CheckExistingInMasterKondisiList(ByVal arList As ArrayList) As ArrayList
        Dim TmpList As ArrayList = New ArrayList
        For Each objConditionMaster As V_LeasingDaftarKondisi In arList
            Try
                If objConditionMaster.VechileType.ID > 0 And Not IsNothing(objConditionMaster.ValidFrom) Then
                    Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.V_LeasingDaftarKondisi), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_LeasingDaftarKondisi), "VechileType", MatchType.Exact, objConditionMaster.VechileType.ID))
                    criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_LeasingDaftarKondisi), "ValidFrom", MatchType.Exact, objConditionMaster.ValidFrom))

                    Dim ConditionMasterCollection As ArrayList = New V_LeasingDaftarKondisiFacade(User).Retrieve(criterias2)

                    If ConditionMasterCollection.Count > 0 Then
                        If objConditionMaster.ErrorMessage = "" Then
                            objConditionMaster.ErrorMessage = "Data Master Kondisi Sudah Terdaftar"
                        Else
                            objConditionMaster.ErrorMessage = objConditionMaster.ErrorMessage + ";<br> Data Master Kondisi Sudah Terdaftar"
                        End If
                        TmpList.Add(objConditionMaster)
                    Else
                        TmpList.Add(objConditionMaster)
                    End If

                Else
                    If objConditionMaster.ErrorMessage = "" Then
                        objConditionMaster.ErrorMessage = "Data Master Kondisi Sudah Terdaftar.<br />Tanggal tidak valid."
                    Else
                        objConditionMaster.ErrorMessage = objConditionMaster.ErrorMessage + ";<br> Data Master Kondisi Sudah Terdaftar.<br />Tanggal tidak valid."
                    End If
                    TmpList.Add(objConditionMaster)
                End If
            Catch ex As Exception

            End Try
        Next
        Return TmpList

    End Function
    Private Sub BinddgMasterKondisi(ByVal pageIndeks As Integer)
        Dim totalRow As Integer = 0
        If (CType(_sessHelper.GetSession("dgType"), String) = "Upload") Then
            totalRow = CType(_sessHelper.GetSession("sessMasterKondisi"), ArrayList).Count
            dgMasterKondisi.DataSource = CType(_sessHelper.GetSession("sessMasterKondisi"), ArrayList)
            dgMasterKondisi.Columns(dgMasterKondisi.Columns.Count - 2).Visible = True
            dgMasterKondisi.Columns(dgMasterKondisi.Columns.Count - 1).Visible = False
            btnDownload.Enabled = False
        ElseIf (CType(_sessHelper.GetSession("dgType"), String) = "Retrieve") Then

            Dim criterias As New CriteriaComposite(New Criteria(GetType(V_LeasingDaftarKondisi), "RowStatus", MatchType.Exact, ddlStatus.SelectedValue))
            If ddlDocumentType.SelectedValue <> -1 Then
                criterias.opAnd(New Criteria(GetType(V_LeasingDaftarKondisi), "DocumentType", MatchType.Exact, ddlDocumentType.SelectedValue))
            End If
         
            Dim _strType As StringBuilder = New StringBuilder
            For i As Integer = 0 To (ctlLstTipeKendaraan.Items.Count - 1)
                If (ctlLstTipeKendaraan.Items(i).Selected) Then
                    If ctlLstTipeKendaraan.Items(i).Value <> -1 Then

                        _strType.Append("'" & ctlLstTipeKendaraan.Items(i).Value & "',")
                    Else
                        If (_strType.Length > 0) Then
                            _strType.Remove(0, _strType.Length - 1)

                        End If

                    End If
                End If
            Next
            If (_strType.Length > 0) Then
                _strType.Remove(_strType.Length - 1, 1)
                criterias.opAnd(New Criteria(GetType(V_LeasingDaftarKondisi), "VechileType.ID", MatchType.InSet, "(" & _strType.ToString() & ")"))

            End If
            If chkTglBerlaku.Checked Then
                criterias.opAnd(New Criteria(GetType(V_LeasingDaftarKondisi), "ValidFrom", MatchType.LesserOrEqual, icTglBerlakuFrom.Value))
                criterias.opAnd(New Criteria(GetType(V_LeasingDaftarKondisi), "ValidTo", MatchType.GreaterOrEqual, icTglBerlakuTo.Value))
            End If

            Dim ConditionMasterColl As ArrayList = New ArrayList
            If chkTglBerlaku.Checked Then
                ConditionMasterColl = New V_LeasingDaftarKondisiFacade(User).Retrieve(criterias)
                ConditionMasterColl = CommonFunction.SortArraylist(ConditionMasterColl, GetType(V_LeasingDaftarKondisi), "VechileType.VechileTypeCode", Sort.SortDirection.ASC)

                Dim arlSource As ArrayList = New ArrayList

                Dim idx As Integer = 0
                For i As Integer = 0 To ConditionMasterColl.Count - 2
                    Dim obj As V_LeasingDaftarKondisi
                    obj = ConditionMasterColl(i)

                    For j As Integer = i + 1 To ConditionMasterColl.Count - 1
                        Dim objToCompare As V_LeasingDaftarKondisi
                        objToCompare = ConditionMasterColl(j)
                        If (obj.VechileType.VechileTypeCode.Trim.ToLower = objToCompare.VechileType.VechileTypeCode.Trim.ToLower) Then
                            If obj.ValidFrom >= objToCompare.ValidFrom Then
                                idx = i

                                If j >= ConditionMasterColl.Count - 1 Then
                                    arlSource.Add(ConditionMasterColl(idx))
                                    arlSource.Add(ConditionMasterColl(j))
                                    i = ConditionMasterColl.Count - 2
                                Else
                                    i = j
                                End If
                            Else
                                idx = j
                                If j >= ConditionMasterColl.Count - 1 Then
                                    arlSource.Add(ConditionMasterColl(idx))
                                    i = ConditionMasterColl.Count - 2
                                Else
                                    i = j
                                End If
                            End If
                        Else
                            idx = i
                            arlSource.Add(ConditionMasterColl(idx))
                            If j >= ConditionMasterColl.Count - 1 Then
                                arlSource.Add(ConditionMasterColl(j))
                            Else
                                j = ConditionMasterColl.Count - 1
                            End If
                        End If
                    Next
                Next

                Dim _id As String = String.Empty
                arlSource = CommonFunction.PageAndSortArraylist(arlSource, pageIndeks, dgMasterKondisi.PageSize, _
                GetType(V_LeasingDaftarKondisi), "VechileType.VechileTypeCode", Sort.SortDirection.ASC)

                'ConditionMasterColl = arlSource

            Else

            End If

            Dim arlDownload As ArrayList = New V_LeasingDaftarKondisiFacade(User).RetrieveActiveList(criterias, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            _sessHelper.SetSession("sessMasterKondisiDB2", arlDownload)

            ConditionMasterColl = New V_LeasingDaftarKondisiFacade(User).RetrieveActiveList(criterias, pageIndeks + 1, dgMasterKondisi.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            _sessHelper.SetSession("sessMasterKondisiDB", ConditionMasterColl)
            'totalRow = ConditionMasterColl.Count
            dgMasterKondisi.DataSource = ConditionMasterColl
            dgMasterKondisi.Columns(dgMasterKondisi.Columns.Count - 2).Visible = False
            If totalRow > 0 Then
                btnDownload.Enabled = CheckBtnDownPriv()
            Else
                btnDownload.Enabled = False
            End If
        End If
        dgMasterKondisi.VirtualItemCount = totalRow
        dgMasterKondisi.CurrentPageIndex = pageIndeks
        dgMasterKondisi.DataBind()
    End Sub
    Public Function getLastValidDate(ByVal pDate As DateTime) As DateTime
        Dim criterias As New CriteriaComposite(New Criteria(GetType(V_LeasingDaftarKondisi), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(V_LeasingDaftarKondisi), "ValidFrom", MatchType.LesserOrEqual, pDate))

        Dim sortColl As New SortCollection
        sortColl.Add(New Sort(GetType(V_LeasingDaftarKondisi), "ValidFrom", Sort.SortDirection.DESC))

        Dim al As ArrayList = New V_LeasingDaftarKondisiFacade(User).Retrieve(criterias, sortColl)

        If al.Count > 0 Then
            Return CType(al(0), V_LeasingDaftarKondisi).ValidFrom
        Else
            Return pDate
        End If
    End Function
    Private Sub checkFileExistenceToDownload()
        Dim finfo As FileInfo = New FileInfo(Me.Server.MapPath("") & "\..\DataTemp\ConditionMaster" & Suffix & ".txt")
        If finfo.Exists Then
            finfo.Delete()
        End If
    End Sub
    Private Sub saveToTextFile(ByVal str As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Try
            If imp.Start() Then

                Dim objFileStream As New FileStream(Me.Server.MapPath("") & "\..\DataTemp\ConditionMaster" & Suffix & ".txt", FileMode.Append, FileAccess.Write)
                Dim objStreamWriter As New StreamWriter(objFileStream)

                objStreamWriter.WriteLine(str)
                objStreamWriter.Close()

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub
    Private Sub download()
        Dim strText As StringBuilder
        Dim objAl As New ArrayList
        Dim delimiter As String = Chr(9)
        checkFileExistenceToDownload()

        objAl = CType(_sessHelper.GetSession("sessMasterKondisiDB"), ArrayList)

        If objAl Is Nothing OrElse objAl.Count = 0 Then
            MessageBox.Show("Tidak ada data yg bisa di download")
            Return
        End If

        'Dim strField As StringBuilder = New StringBuilder
        'strField.Append("Tipe")
        'strField.Append(delimiter)
        'strField.Append("Tanggal")
        'strField.Append(delimiter)
        'strField.Append("Harga")
        'strField.Append(delimiter)
        'strField.Append("SPAF")
        'strField.Append(delimiter)
        'strField.Append("Subsidi per Unit")
        'strField.Append(delimiter)

        'strText = New StringBuilder
        'strText.Append(strField)
        'Try
        '    saveToTextFile(strText.ToString())
        'Catch
        '    MessageBox.Show("Persiapan Proses Download gagal")
        '    Return
        'End Try

        'strText = New StringBuilder

        For i As Integer = 0 To objAl.Count
            strText = New StringBuilder
            If i = 0 Then
                strText.Append("Tipe")
                strText.Append(delimiter)
                strText.Append("  Tanggal ")
                strText.Append(delimiter)
                strText.Append("Harga")
                strText.Append(delimiter)
                strText.Append("SPAF")
                strText.Append(delimiter)
                strText.Append("Subsidi")
                strText.Append(delimiter)

            Else
                Dim obj As V_LeasingDaftarKondisi = objAl(i - 1)

                'Tipe
                If IsNothing(obj.VechileType) Then
                    strText.Append(delimiter)
                Else
                    strText.Append(obj.VechileType.VechileTypeCode)
                    strText.Append(delimiter)
                End If

                'TglBerlaku
                strText.Append(obj.ValidFrom.ToString("dd/MM/yyyy"))
                strText.Append(delimiter)

                'Harga Retail
                strText.Append(obj.RetailPrice.ToString("###0"))
                strText.Append(delimiter)


                'SPAF
                strText.Append(obj.SPAF.ToString("##0,##"))
                strText.Append(delimiter)

                'Subsidi
                strText.Append(obj.Subsidi.ToString("###0"))

            End If


            Try
                saveToTextFile(strText.ToString())
            Catch
                MessageBox.Show("Persiapan Proses Download gagal")
                Return
            End Try

        Next

        'For Each obj As ConditionMaster In objAl

        '    'Tipe
        '    If IsNothing(obj.VechileType) Then
        '        strText.Append(delimiter)
        '    Else
        '        strText.Append(obj.VechileType.VechileTypeCode)
        '        strText.Append(delimiter)
        '    End If

        '    'TglBerlaku
        '    strText.Append(obj.ValidFrom.ToString("dd/MM/yyyy"))
        '    strText.Append(delimiter)

        '    'Harga Retail
        '    strText.Append(obj.RetailPrice.ToString("###0"))
        '    strText.Append(delimiter)


        '    'SPAF
        '    strText.Append(obj.SPAF.ToString("##0"))
        '    strText.Append(delimiter)

        '    'Subsidi
        '    strText.Append(obj.Subsidi.ToString("###0"))

        '    Try
        '        saveToTextFile(strText.ToString())
        '    Catch
        '        MessageBox.Show("Persiapan Proses Download gagal")
        '        Return
        '    End Try

        'Next
        Response.Redirect("../downloadlocal.aspx?file=DataTemp\ConditionMaster" & Suffix & ".txt")

        MessageBox.Show("Data Telah Disimpan")
    End Sub
    Private Function IsSearchValid() As Boolean
        Dim IsValid As Boolean = True
        Dim strMessageBuilt As New StringBuilder("")
        If ddlDocumentType.SelectedValue = -1 Then
            IsValid = False
            strMessageBuilt.Append("Tipe dokumen harus diisi")
        End If
        If icTglBerlakuFrom.Value > icTglBerlakuTo.Value Then
            IsValid = False
            MessageBox.Show(SR.InvalidRangeDate)
        End If
        If strMessageBuilt.Length > 0 Then
            MessageBox.Show(strMessageBuilt.ToString)
        End If
        Return IsValid
    End Function
    Private Sub ChangeDisplayColumn(ByVal docType As EnumDocumentType.DocumentType)
        dgMasterKondisi.Columns(5).HeaderText = String.Format("Nilai {0} (%)", docType.ToString)
        dgMasterKondisi.Columns(6).HeaderText = String.Format("{0} per unit (Rp)", docType.ToString)
        dgMasterKondisi.Columns(8).HeaderText = String.Format("{0} setelah PPh (Rp)", docType.ToString)
    End Sub
#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        If Not IsPostBack Then
            ViewState("currSortColumn") = "ID"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            BindDropDownTipe()
            BindDropDownDocumentType()
            BindDropDownStatus()
            Initialize()
            _sessHelper.SetSession("dgType", "Retrieve")

            tblInput.Visible = CheckBtnUpPriv()
            btnUpload.Enabled = CheckBtnUpPriv()
            btnDownload.Enabled = CheckBtnDownPriv()
        End If
    End Sub
    Private Sub chkTglBerlaku_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTglBerlaku.CheckedChanged
        icTglBerlakuFrom.Enabled = chkTglBerlaku.Checked
        icTglBerlakuTo.Enabled = chkTglBerlaku.Checked
    End Sub
    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        bIsError = False
        BindUpload()
        bIsError = CType(_sessHelper.GetSession("sessError"), Boolean)
        If Not bIsError And Path.GetFileName(dfMasterKondisi.PostedFile.FileName).ToString.Trim <> String.Empty Then
            If CheckBtnUpPriv() = False Then
                btnSimpan.Enabled = False
            Else
                btnSimpan.Enabled = True
            End If
        Else
            _sessHelper.SetSession("sessError", Nothing)
            btnSimpan.Enabled = False
        End If
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If IsSearchValid() Then
            _sessHelper.SetSession("dgType", "Retrieve")
            ChangeDisplayColumn(ddlDocumentType.SelectedValue)
            dgMasterKondisi.Columns(dgMasterKondisi.Columns.Count - 1).Visible = ddlStatus.SelectedValue = DBRowStatus.Active
            BinddgMasterKondisi(0)
        End If
    End Sub
    Private Sub dgMasterKondisi_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgMasterKondisi.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As V_LeasingDaftarKondisi = CType(e.Item.DataItem, V_LeasingDaftarKondisi)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1 + (dgMasterKondisi.CurrentPageIndex * dgMasterKondisi.PageSize)

                Dim lblTglBerlaku As Label = CType(e.Item.FindControl("lblTglBerlaku"), Label)
                If RowValue.ValidFrom = New DateTime(1900, 1, 1) Or RowValue.ValidFrom = System.Data.SqlTypes.SqlDateTime.MinValue.Value.ToString() Then
                    lblTglBerlaku.Text = ""
                Else
                    If RowValue.ValidFrom < New Date(1900, 1, 1) Then
                        lblTglBerlaku.Text = ""
                    Else
                        lblTglBerlaku.Text = RowValue.ValidFrom
                    End If
                End If

                Dim lblMessage As Label = CType(e.Item.FindControl("lblMessage"), Label)
                If Not RowValue.ErrorMessage <> "" Then
                    lblMessage.Text = "OK"
                    lblMessage.BackColor = GreenYellow
                Else
                    lblMessage.BackColor = LightSalmon
                    bIsError = True
                End If
                Dim _lbtnDelete As ImageButton = CType(e.Item.FindControl("lnkbtnDelete"), ImageButton)
                If RowValue.RowStatus = DBRowStatus.Active Then
                    _lbtnDelete.ToolTip = "Hapus"
                    _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                ElseIf RowValue.RowStatus = DBRowStatus.Deleted Then
                    Dim _str As String = String.Empty
                    If Not IsNothing(RowValue.LastUpdateBy) Then
                        _str = RowValue.LastUpdateBy
                        Dim odealer As Dealer = New DealerFacade(User).Retrieve(CInt(_str.Substring(0, 6)))
                        If Not IsNothing(odealer) Then
                            _lbtnDelete.ToolTip = odealer.DealerCode & " - " & _str.Substring(6) & " - " & RowValue.LastUpdateTime.ToString("dd/MM/yyyy")
                        End If
                        '                        _lbtnDelete.ToolTip = RowValue.LastUpdateBy & Chr(13) & RowValue.LastUpdateTime
                        _lbtnDelete.Attributes.Add("OnClick", "return false;")
                    End If
                End If

                If CheckBtnGridPriv() = False Then
                    _lbtnDelete.Visible = False
                Else
                    _lbtnDelete.Visible = True
                End If
                'bug 1652
                Dim lblSPAF As Label = e.Item.FindControl("lblSPAF")
                If RowValue.SPAF = 0 Then
                    lblSPAF.Text = RowValue.SPAF.ToString("#,##0")
                End If
            End If
            _sessHelper.SetSession("sessError", bIsError)
        End If
    End Sub
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim nResult As Integer
        Dim objConditionMasterFacade As New V_LeasingDaftarKondisiFacade(User)
        Dim bCheckSuccess As Integer
        Dim arList As ArrayList = CType(_sessHelper.GetSession("sessMasterKondisi"), ArrayList)

        bCheckSuccess = objConditionMasterFacade.Insert(arList)

        If bCheckSuccess > 0 Then
            MessageBox.Show("Proses simpan berhasil!")
        Else
            MessageBox.Show("Proses simpan gagal!")
        End If
        btnSimpan.Enabled = False
    End Sub
    Private Sub dgMasterKondisi_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgMasterKondisi.ItemCommand
        Select Case e.CommandName
            Case "Delete" 'Delete this datagrid item 
                Dim _ConditionMasterFacade As New V_LeasingDaftarKondisiFacade(User)
                Dim oConditionMaster As V_LeasingDaftarKondisi = _ConditionMasterFacade.Retrieve(CType(e.CommandArgument, Integer))
                _ConditionMasterFacade.Delete(oConditionMaster)
                BinddgMasterKondisi(dgMasterKondisi.CurrentPageIndex)
        End Select

    End Sub
    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Response.Redirect("FrmMasterKondisiDownload.aspx")
        'download()
    End Sub

#End Region

    Private Sub dgMasterKondisi_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgMasterKondisi.SortCommand
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
        BinddgMasterKondisi(dgMasterKondisi.CurrentPageIndex)
    End Sub

    Private Sub dgMasterKondisi_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgMasterKondisi.PageIndexChanged
        dgMasterKondisi.CurrentPageIndex = e.NewPageIndex
        BinddgMasterKondisi(dgMasterKondisi.CurrentPageIndex)
    End Sub
End Class