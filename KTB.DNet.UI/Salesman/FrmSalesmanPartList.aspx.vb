#Region "Custom Namespace Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.BusinessFacade.Training

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.BusinessValidation
Imports System.IO
Imports System.Text
#End Region

Public Class FrmSalesmanPartList
    Inherits System.Web.UI.Page


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents txtID As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNama As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealerBranchCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblShowSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealerBranch As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalesmanID As System.Web.UI.WebControls.Label
    Protected WithEvents lblLevelSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents lblSep As System.Web.UI.WebControls.Label
    Protected WithEvents dgSalesmanHeader As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownloadExcel As System.Web.UI.WebControls.Button
    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPosisi As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlGrade As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSalesmanHeaderID As System.Web.UI.WebControls.Label
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    Private _SalesmanHeaderFacade As New SalesmanHeaderFacade(User)
    Private _SalesmanTrainingParticipantFacade As New SalesmanTrainingParticipantFacade(User)
    Private _SalesmanUniformAssignedFacade As New SalesmanUniformAssignedFacade(User)
    Private sessHelper As New SessionHelper
    Private objDealer As New Dealer
    Private strDefDate As String = "1753/01/01"
    'Dim criterias As CriteriaComposite
    Dim strFileNm As String
    Dim strFileNmHeader As String
    Private _downloadPriv As Boolean = False
#End Region

#Region "Privilege"
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.lihat_daftar_salesman_part_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Part Employee - Daftar Part Employee")
        End If
    End Sub

    Dim bCheckEditDetailKTBPrivilege As Boolean = SecurityProvider.Authorize(context.User, SR.buat_buatid_salesman_part_privilege)
    Dim bCheckEditDetailPrivilege As Boolean = SecurityProvider.Authorize(context.User, SR.edit_detail_daftar_salesman_part_privilege)
    Dim bCekDLPriv As Boolean = SecurityProvider.Authorize(context.User, SR.download_daftar_salesman_part_privilege)
    Dim bCekViewDetailsPriv As Boolean = SecurityProvider.Authorize(context.User, SR.lihat_detail_daftar_salesman_part_privilege)
    Dim bCekDeletePriv As Boolean = SecurityProvider.Authorize(context.User, SR.Buat_buatid_salesman_part_privilege)
    Dim Priv_Partshop As Boolean = SecurityProvider.Authorize(context.User, SR.Input_data_salesman_part_privilege)
    Dim Priv_RequestID As Boolean = SecurityProvider.Authorize(context.User, SR.Input_data_salesman_part_privilege)
#End Region

#Region "Custom Method"
    Private Sub SetSetting()
        lblPageTitle.Text = "PART EMPLOYEE- Daftar Part Employee"
        lblSalesmanID.Text = "Employee Part ID"
        sessHelper.SetSession("strFileNm", "SalesmanPartUnit")
        sessHelper.SetSession("strFileNmHeader", "Salesman Part List")
    End Sub
    ' untuk menampung kriteria yg sebelumnya
    Private Sub SaveCriteria()
        Dim crits As Hashtable = New Hashtable
        crits.Add("SalesmanCode", txtID.Text)
        crits.Add("Name", txtNama.Text)
        crits.Add("DivisiID", ddlKategori.SelectedValue)
        crits.Add("PosisiID", ddlPosisi.SelectedValue)
        crits.Add("LevelID", ddlGrade.SelectedValue)
        crits.Add("Status", ddlStatus.SelectedValue)
        sessHelper.SetSession("frmSalesmanList", crits)
    End Sub
    ' untuk menampilkan data kriteria sebelumnya
    Private Sub ReadCriteria()
        Dim crits As Hashtable
        crits = CType(sessHelper.GetSession("frmSalesmanList"), Hashtable)
        If Not IsNothing(crits) Then
            txtID.Text = CStr(crits.Item("SalesmanCode"))
            txtNama.Text = CStr(crits.Item("Name"))
            Dim i As Integer
            For i = 0 To ddlKategori.Items.Count - 1
                If CType(ddlKategori.Items(i).Value, Integer) = CType(crits.Item("DivisiID"), Integer) Then
                    ddlKategori.SelectedIndex = i
                End If
            Next
            For i = 0 To ddlPosisi.Items.Count - 1
                If CType(ddlPosisi.Items(i).Value, Integer) = CType(crits.Item("PosisiID"), Integer) Then
                    ddlPosisi.SelectedIndex = i
                End If
            Next
            'ddlPosisi.SelectedValue = IIf(CInt(crits.Item("PosisiID")) > 0, CInt(crits.Item("PosisiID")), 0)
            'ddlPosisi_SelectedIndexChanged(Me, Nothing)
            For i = 0 To ddlGrade.Items.Count - 1
                If CType(ddlGrade.Items(i).Value, Integer) = CType(crits.Item("LevelID"), Integer) Then
                    ddlGrade.SelectedIndex = i
                End If
            Next
            'ddlGrade.SelectedValue = IIf(CInt(crits.Item("LevelID")) > 0, CInt(crits.Item("LevelID")), 0) 'CInt(crits.Item("LevelID"))

            For i = 0 To ddlStatus.Items.Count - 1
                If CType(ddlStatus.Items(i).Value, Integer) = CType(crits.Item("LevelID"), Integer) Then
                    ddlStatus.SelectedIndex = i
                End If
            Next
            'ddlStatus.SelectedValue = IIf(CInt(crits.Item("Status")) > 0, CInt(crits.Item("Status")), 0) 'CInt(crits.Item("Status"))
        End If
    End Sub
    Private Sub ClearData()
        ' kalau dealer tdk bs dihapus
        ' 23-11-2007    Deddy H     Fix bug 1611
        objDealer = (CType(sessHelper.GetSession("Dealer"), Dealer))
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            txtDealerCode.Text = String.Empty
        End If
        txtID.Text = String.Empty
        txtNama.Text = String.Empty
        BindDropDown()
        SetSetting()
    End Sub
    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)
        Dim CriteriaDownload As New CriteriaComposite(New Criteria(GetType(V_SalesmanPart), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        ' khusus spare part
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, 0))

        objDealer = (CType(sessHelper.GetSession("Dealer"), Dealer))

        If (txtDealerCode.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
            CriteriaDownload.opAnd(New Criteria(GetType(V_SalesmanPart), "DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
        End If

        If (txtDealerBranchCode.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "DealerBranch.DealerBranchCode", MatchType.InSet, "('" + Replace(txtDealerBranchCode.Text, ";", "','") + "')"))
            CriteriaDownload.opAnd(New Criteria(GetType(V_SalesmanPart), "DealerBranchCode", MatchType.InSet, "('" + Replace(txtDealerBranchCode.Text, ";", "','") + "')"))
        End If

        If txtID.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesmanCode", MatchType.[Partial], txtID.Text.Trim()))
            CriteriaDownload.opAnd(New Criteria(GetType(V_SalesmanPart), "SalesmanCode", MatchType.[Partial], txtID.Text.Trim()))
        End If

        If txtNama.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Name", MatchType.[Partial], txtNama.Text.Trim()))
            CriteriaDownload.opAnd(New Criteria(GetType(V_SalesmanPart), "Name", MatchType.[Partial], txtNama.Text.Trim()))
        End If

        If ddlKategori.SelectedValue <> -1 Then
            Dim sqlDiv As String = "select ID from V_SalesmanPart where DivisiID =" & CType(ddlKategori.SelectedValue, Integer)
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "ID", MatchType.InSet, "(" & sqlDiv & ")"))
            CriteriaDownload.opAnd(New Criteria(GetType(V_SalesmanPart), "DivisiID", MatchType.Exact, ddlKategori.SelectedValue))
        End If

        If ddlPosisi.SelectedValue <> -1 Then
            Dim sqlPos As String = "select ID from V_SalesmanPart where PosisiID =" & CType(ddlPosisi.SelectedValue, Integer)
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "ID", MatchType.InSet, "(" & sqlPos & ")"))
            CriteriaDownload.opAnd(New Criteria(GetType(V_SalesmanPart), "PosisiID", MatchType.Exact, ddlPosisi.SelectedValue))
        End If

        If ddlGrade.SelectedValue <> 99 Then
            Dim sqlGrade As String = "select ID from V_SalesmanPart where LevelID =" & CType(ddlGrade.SelectedValue, Integer)
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "ID", MatchType.InSet, "(" & sqlGrade & ")"))
            CriteriaDownload.opAnd(New Criteria(GetType(V_SalesmanPart), "LevelID", MatchType.Exact, ddlGrade.SelectedValue))
        End If

        If ddlStatus.SelectedItem.Text <> "Silahkan Pilih" Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.Exact, ddlStatus.SelectedValue))
            CriteriaDownload.opAnd(New Criteria(GetType(V_SalesmanPart), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If


        sessHelper.SetSession("criterias", criterias)
        sessHelper.SetSession("criteriadownload", CriteriaDownload)
    End Sub
    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        'Dim intMode As Integer
        'intMode = CreateCriteria()
        'If intMode <> 0 Then
        arrList = New SalesmanHeaderFacade(User).RetrieveByCriteria(CType(sessHelper.GetSession("criterias"), CriteriaComposite), idxPage + 1, dgSalesmanHeader.PageSize, totalRow, _
                CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgSalesmanHeader.CurrentPageIndex = idxPage
        dgSalesmanHeader.DataSource = arrList
        dgSalesmanHeader.VirtualItemCount = totalRow
        'Else
        '    dgSalesmanHeader.DataSource = Nothing
        '    MessageBox.Show("Kode dealer tidak valid.")
        'End If
        dgSalesmanHeader.DataBind()
        sessHelper.SetSession("idxPage", dgSalesmanHeader.CurrentPageIndex)

    End Sub
    Private Sub BindDropDown()
        CommonFunction.BindFromEnum("SalesmanStatus", ddlStatus, Me.User, True, "NameStatus", "ValStatus")

        Dim crits As Hashtable
        crits = CType(sessHelper.GetSession("frmSalesmanList"), Hashtable)
        If Not IsNothing(crits) Then
            Dim i As Integer
            For i = 0 To ddlStatus.Items.Count - 1
                If CType(ddlStatus.Items(i).Value, Integer) = CType(crits.Item("Status"), Integer) Then
                    ddlStatus.SelectedIndex = i
                End If
            Next
        End If

        CommonFunction.BindFromEnum("SalesmanPartLevel", ddlGrade, User, True, "NameStatus", "ValStatus")

        ddlKategori.Items.Clear()
        Dim arlCtgLevel As ArrayList = New SalesmanCategoryLevelFacade(User).RetrieveActiveList()
        ddlKategori.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each obj As SalesmanCategoryLevel In arlCtgLevel
            If obj.LevelNumber = 1 Then
                ddlKategori.Items.Add(New ListItem(obj.PositionName, obj.ID))
            End If
        Next
        If ddlKategori.Items.Count > 0 Then
            If Not IsNothing(crits) Then
                txtID.Text = CStr(crits.Item("SalesmanCode"))
                txtNama.Text = CStr(crits.Item("Name"))
                Dim i As Integer
                For i = 0 To ddlKategori.Items.Count - 1
                    If CType(ddlKategori.Items(i).Value, Integer) = CType(crits.Item("DivisiID"), Integer) Then
                        ddlKategori.SelectedIndex = i
                    End If
                Next
            Else
                ddlKategori.SelectedIndex = 0
            End If
            ddlKategori_SelectedIndexChanged(Nothing, Nothing)
        End If


    End Sub

    Private Sub ddlKategori_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlKategori.SelectedIndexChanged
        ddlPosisi.Items.Clear()
        ddlPosisi.Items.Add(New ListItem("Silahkan Pilih", -1))
        Dim arlSalesmanLevel As ArrayList = New SalesmanCategoryLevelFacade(User).RetrieveActiveList()
        If arlSalesmanLevel.Count > 0 Then
            For Each obj As SalesmanCategoryLevel In arlSalesmanLevel
                If Not obj.SalesmanCategoryLevel Is Nothing Then
                    If obj.SalesmanCategoryLevel.ID = CType(ddlKategori.SelectedValue, Integer) Then
                        ddlPosisi.Items.Add(New ListItem(obj.PositionName, obj.ID))
                    End If
                End If
            Next
        End If
        If ddlPosisi.Items.Count > 0 Then
            Dim crits As Hashtable
            crits = CType(sessHelper.GetSession("frmSalesmanList"), Hashtable)
            If Not IsNothing(crits) Then
                Dim i As Integer
                For i = 0 To ddlPosisi.Items.Count - 1
                    If CType(ddlPosisi.Items(i).Value, Integer) = CType(crits.Item("PosisiID"), Integer) Then
                        ddlPosisi.SelectedIndex = i
                    End If
                Next
            Else
                ddlPosisi.SelectedIndex = 0
            End If

            ddlPosisi_SelectedIndexChanged(Nothing, Nothing)
        End If
    End Sub

    Private Sub ddlPosisi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPosisi.SelectedIndexChanged
        Dim crits As Hashtable
        crits = CType(sessHelper.GetSession("frmSalesmanList"), Hashtable)
        If Not IsNothing(crits) Then
            Dim i As Integer
            For i = 0 To ddlGrade.Items.Count - 1
                If CType(ddlGrade.Items(i).Value, Integer) = CType(crits.Item("LevelID"), Integer) Then
                    ddlGrade.SelectedIndex = i
                End If
            Next
        End If
        If CType(ddlPosisi.SelectedValue, Integer) = 19 Then
            lblLevelSalesman.Visible = True
            lblSep.Visible = True
            ddlGrade.Visible = True
        Else
            lblLevelSalesman.Visible = False
            lblSep.Visible = False
            ddlGrade.Visible = False
        End If
    End Sub

    Private Sub Delete(ByVal nID As Integer)

        Dim totalRow As Integer = 0
        Dim arrListSalesmanTrainingParticipant As New ArrayList
        Dim criteriasSalesmanTrainingParticipant As New CriteriaComposite(New Criteria(GetType(SalesmanTrainingParticipant), "SalesmanHeader.ID", MatchType.Exact, nID))
        Dim strAddMsg As String = "status penghapusan Salesman tdk bisa dilakukan"

        ' check if data SalesmanTrainingParticipant & SalesmanUniformAssigned exist or not
        ' if exist cann't be delete
        arrListSalesmanTrainingParticipant = _SalesmanTrainingParticipantFacade.RetrieveByCriteria(criteriasSalesmanTrainingParticipant, 1, dgSalesmanHeader.PageSize, totalRow)

        If arrListSalesmanTrainingParticipant.Count > 0 Then
            MessageBox.Show("Data Uniform Assigned sudah ada untuk salesman ini, " & strAddMsg)
            Return
        End If

        Dim arrListSalesmanUniformAssigned As New ArrayList
        Dim criteriaSalesmanUniformAssigned As New CriteriaComposite(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanHeader.ID", MatchType.Exact, nID))

        arrListSalesmanUniformAssigned = _SalesmanUniformAssignedFacade.RetrieveByCriteria(criteriaSalesmanUniformAssigned, 1, dgSalesmanHeader.PageSize, totalRow)

        If arrListSalesmanUniformAssigned.Count > 0 Then
            MessageBox.Show("Data Uniform Assigned sudah ada untuk salesman ini, " & strAddMsg)
            Return
        End If

        'update salesmanAdditionalInfo (20121112)
        Dim arrListSalesmanAdditionalInfo As New ArrayList
        Dim criteriaSalesmanAdditionalInfo As New CriteriaComposite(New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanHeader.ID", MatchType.Exact, nID))

        arrListSalesmanAdditionalInfo = New SalesmanAdditionalInfoFacade(User).Retrieve(criteriaSalesmanAdditionalInfo)

        If arrListSalesmanAdditionalInfo.Count > 0 Then
            Dim iAddInfo As Integer = -1
            For Each item As SalesmanAdditionalInfo In arrListSalesmanAdditionalInfo
                item.RowStatus = CType(DBRowStatus.Deleted, Short)
                iAddInfo = New SalesmanAdditionalInfoFacade(User).Update(item)
            Next
        End If

        ' --- if valid proc, then update ---
        Dim iRecordCount As Integer = 0
        Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(nID)

        ' update field RowStatus
        If Not objSalesmanHeader Is Nothing Then
            objSalesmanHeader.RowStatus = CType(DBRowStatus.Deleted, Short)
        End If

        Dim facade As SalesmanHeaderFacade = New SalesmanHeaderFacade(User)
        Dim iReturn As Integer = -1

        'iReturn = facade.DeleteTransaction(objSalesmanArea)
        'iReturn = facade.DeleteFromDB(objSalesmanHeader)
        iReturn = facade.Update(objSalesmanHeader)
        If iReturn < 0 Then
            MessageBox.Show(SR.DeleteFail)
        Else
            MessageBox.Show(SR.DeleteSucces)
        End If

        dgSalesmanHeader.CurrentPageIndex = 0
        BindDataGrid(dgSalesmanHeader.CurrentPageIndex)
    End Sub
    Private Sub SetDownload()
        Dim arrData As New ArrayList
        Dim crits As CriteriaComposite
        If dgSalesmanHeader.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        If Not IsNothing(sessHelper.GetSession("criteriadownload")) Then
            crits = CType(sessHelper.GetSession("criteriadownload"), CriteriaComposite)
        End If
        ' mengambil data yang dibutuhkan
        arrData = New SalesmanHeaderFacade(User).RetrieveV_SalesmanPart(crits)
        If arrData.Count > 0 Then
            DoDownload(arrData)
        End If

    End Sub
    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        If Not IsNothing(sessHelper.GetSession("strFileNm")) Then
            strFileNm = CType(sessHelper.GetSession("strFileNm"), String)
        End If
        If Not IsNothing(sessHelper.GetSession("strFileNmHeader")) Then
            strFileNmHeader = CType(sessHelper.GetSession("strFileNmHeader"), String)
        End If

        sFileName = strFileNm & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond
        Dim ListData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        Dim _user As String
        _user = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String
        _password = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String
        _webServer = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            'If imp.Start() Then

            Dim finfo As FileInfo = New FileInfo(ListData)
            If finfo.Exists Then
                finfo.Delete()
            End If

            Dim fs As FileStream = New FileStream(ListData, FileMode.CreateNew)
            Dim sw As StreamWriter = New StreamWriter(fs)



            'If ListData Is Nothing Then
            '    MessageBox.Show("listdata is nothing")
            'Else
            '    MessageBox.Show(ListData.ToString)
            'End If



            'If sw Is Nothing Then
            '    MessageBox.Show("sw is nothing")
            'End If

            'If data Is Nothing Then
            '    MessageBox.Show("data is nothing")
            'End If
            'sw.WriteLine("x")
            WriteListData(sw, data)
            sw.Close()
            fs.Close()
            '    imp.StopImpersonate()
            '    imp = Nothing
            'End If

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")
        Catch ex As Exception
            Dim dummy As Integer = 0
            MessageBox.Show("Download data gagal. " & ex.Message)
        End Try
    End Sub
    ' Modified by Ikhsan, 7 AGustus 2008
    ' Requested by Rina
    ' As Part of CR, to add several Column, to improve formatted excel 
    '------------------------------------------------------------------
    Private Function SlmProfile(ByVal ID As Integer, ByVal Type As String) As ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.ID", MatchType.Exact, ID))

        If Type = "PENDIDIKAN" Then
            criterias.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, 31))
        ElseIf Type = "EMAIL" Then
            criterias.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, 26))
        ElseIf Type = "NO_HP" Then
            criterias.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, 33))
        ElseIf Type = "NOKTP" Then
            criterias.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, 29))

        End If

        Dim ArrSlmProf As ArrayList = New SalesmanProfileFacade(User).Retrieve(criterias)

        Return ArrSlmProf
        '------------------------------------------------------------------
    End Function


    Private Sub WriteListData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)

        Dim itemLine As StringBuilder = New StringBuilder
        Dim objSmanFacade As New SalesmanHeaderFacade(User)

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(strFileNmHeader)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No" & tab)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Kode Cabang" & tab)
            itemLine.Append("Kode " & tab)
            itemLine.Append("Nama" & tab)
            itemLine.Append("Tempat Lahir" & tab)
            itemLine.Append("Tanggal Lahir" & tab)
            itemLine.Append("Jenis Kelamin" & tab)
            itemLine.Append("Status Perkawinan" & tab)
            itemLine.Append("Alamat" & tab)
            itemLine.Append("Propinsi" & tab)
            itemLine.Append("Kota" & tab)
            itemLine.Append("Pendidikan" & tab)
            itemLine.Append("E-Mail" & tab)
            itemLine.Append("No HP" & tab)
            itemLine.Append("NO KTP" & tab)
            itemLine.Append("Kategori" & tab)
            itemLine.Append("Posisi" & tab)
            itemLine.Append("Level" & tab)
            'itemLine.Append("Superior Code" & tab)
            'itemLine.Append("Superior Name" & tab)
            itemLine.Append("Status" & tab)
            itemLine.Append("Tanggal Masuk" & tab)
            itemLine.Append("Area" & tab)
            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            For Each item As V_SalesmanPart In data
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(i.ToString & tab)
                itemLine.Append(item.DealerCode & tab)
                itemLine.Append(item.DealerBranchCode & tab)
                itemLine.Append(item.SalesmanCode & tab)
                itemLine.Append(item.Name & tab)
                itemLine.Append(item.PlaceOfBirth.ToString & tab)
                itemLine.Append(Format(item.DateOfBirth, "dd/MM/yyyy").ToString & tab)
                Dim gender As EnumGender.Gender = item.Gender
                itemLine.Append(gender.ToString & tab)
                Dim married As EnumSalesmanMarriedStatus.MarriedStatus = CType(item.MarriedStatus, Integer)
                itemLine.Append(married.ToString & tab)
                If Not IsNothing(item.Address) Then
                    Dim Address1 As String = item.Address.Replace(LF, "")
                    Dim Address2 As String = Address1.Replace(CR, " ")
                    itemLine.Append(Address2.Trim & tab)
                Else
                    itemLine.Append("" & tab)
                End If
                itemLine.Append(item.ProvinceName & tab)
                itemLine.Append(item.City & tab)
                itemLine.Append(item.PENDIDIKAN & tab)
                itemLine.Append(item.EMAIL & tab)
                itemLine.Append(item.NO_HP & tab)
                itemLine.Append(item.NOKTP & tab)
                itemLine.Append(item.DivisiName & tab)
                itemLine.Append(item.PosisiName & tab)
                itemLine.Append(item.LevelName & tab)
                'itemLine.Append(item.LeaderCode & tab)
                'itemLine.Append(item.LeaderName & tab)
                itemLine.Append(CType(item.Status, EnumSalesmanStatus.SalesmanStatus).ToString.Replace("_", " ") & tab)
                If Not IsNothing(item.HireDate) Then
                    itemLine.Append(item.HireDate & tab)
                Else
                    itemLine.Append("" & tab)
                End If
                itemLine.Append(item.AreaDesc & tab)
                sw.WriteLine(itemLine.ToString())
                i = i + 1
            Next
        End If
    End Sub

#End Region

#Region "event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'InitiateAuthorization()
        CheckPrivilege()
        objDealer = (CType(sessHelper.GetSession("Dealer"), Dealer))
        If Not IsPostBack Then
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                txtDealerCode.Text = objDealer.DealerCode
                txtDealerCode.Enabled = False
                lblSearchDealer.Visible = False
            End If

            BindDropDown()
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            lblSearchDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"
            lblShowSalesman.Attributes("onclick") = "ShowSalesmanSelection();"
            ViewState("CurrentSortColumn") = "Dealer.DealerCode"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            'ReadCriteria()
            SetSetting()
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            CreateCriteria(criterias)
            If Not IsNothing(sessHelper.GetSession("idxPage")) Then
                BindDataGrid(sessHelper.GetSession("idxPage"))
            Else
                BindDataGrid(0)
            End If
        End If
        btnDownloadExcel.Enabled = bCekDLPriv
    End Sub
    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        SaveCriteria()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(criterias)
        dgSalesmanHeader.CurrentPageIndex = 0
        BindDataGrid(0)
    End Sub
    Private Sub dgSalesmanHeader_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesmanHeader.ItemCommand
        Dim strMode As String = "part"
        Dim lblKodeDealer As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)

        If (e.CommandName = "View") Then
            Response.Redirect("FrmSalesmanPart.aspx?ID=" + e.Item.Cells(0).Text + "&Mode=" + strMode)
        ElseIf e.CommandName = "Edit" Then
            Response.Redirect("FrmSalesmanPart.aspx?ID=" + e.Item.Cells(0).Text + "&edit=true" + "&Mode=" + strMode)
        ElseIf e.CommandName = "Delete" Then
            Delete(e.Item.Cells(0).Text)
        ElseIf (e.CommandName = "History") Then
            Response.Redirect("FrmSalesmanPartHistory.aspx?ID=" + e.Item.Cells(0).Text + "&dealer=" + lblKodeDealer.Text + "&code=" + e.Item.Cells(4).Text + "&nama=" + e.Item.Cells(6).Text)
        ElseIf (e.CommandName = "Partshop") Then
            Response.Redirect("FrmSalesmanPartShop.aspx?ID=" + e.Item.Cells(0).Text + "&dealer=" + lblKodeDealer.Text + "&code=" + e.Item.Cells(4).Text + "&nama=" + e.Item.Cells(6).Text)
        ElseIf (e.CommandName = "RequestID") Then
            RequestID(e.Item.Cells(0).Text)
        End If

    End Sub

    Private Sub RequestID(ByVal empID As Integer)
        Dim facade As SalesmanHeaderFacade = New SalesmanHeaderFacade(User)
        Dim objSalesmanHeader As SalesmanHeader = facade.Retrieve(empID)
        objSalesmanHeader.IsRequestID = EnumSalesmanIsRequest.SalesmanIsRequest.Sudah_Request
        'objSalesmanHeader.Status = EnumSalesmanStatus.SalesmanStatus.Konfirmasi
        Try

            Dim vr As ValidResult = New SalesmanHeaderValidation().ValidateKTPSalesmanHeader(objSalesmanHeader)

            If vr.IsValid = False Then
                MessageBox.Show(vr.Message)
                Exit Sub
            End If

            Dim nResult As Integer = New SalesmanHeaderFacade(User).Update(objSalesmanHeader)

            If nResult = -1 Then
                MessageBox.Show("Proses request ID gagal.")
            ElseIf nResult = -3 Then
                MessageBox.Show("Mohon maaf no KTP yang diproses telah terdaftar pada siswa training aktif di dealer yang berbeda.")
            Else
                MessageBox.Show("Proses request ID berhasil.")
                SendEmail(objSalesmanHeader, True)
                BindDataGrid(dgSalesmanHeader.CurrentPageIndex)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub SendEmail(ByVal objSalesmanHeader As SalesmanHeader, ByVal bStatus As Boolean) ' bStatus = New (true) or Update(false) Employee
        Dim appConfigFacade As New AppConfigFacade(User)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Dim emailFrom As String = appConfigFacade.Retrieve(TrainingCenterEmail.EMAIL_FROM_ASS).Value
        Dim emailTo As String = appConfigFacade.Retrieve(TrainingCenterEmail.EMAIL_TO_SP_ADMIN).Value
        Dim UrlPartEmpGenerate As String = KTB.DNet.Lib.WebConfig.GetValue("UrlPartEmpGenerate")
        Dim urlPartEmpList As String = KTB.DNet.Lib.WebConfig.GetValue("UrlPartEmpList")

        Dim valueEmail As String
        If bStatus Then
            valueEmail = GenerateEmailNewEmployee(objSalesmanHeader, UrlPartEmpGenerate)
            ObjEmail.sendMail(emailTo, "", emailFrom, "[MMKSI-DNet] Parts - Request Employee Part Code ", Mail.MailFormat.Html, valueEmail)
        End If

    End Sub

    Private Function GenerateEmailNewEmployee(ByVal objSalesmanHeader As SalesmanHeader, ByVal urlRequest As String) As String

        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder("")
        sb.Append("<FONT face=Arial size=1>")
        sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 align=center><b>Request Part Employee Code</b></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=50></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=50>")
        sb.Append("Dengan hormat,&nbsp;")
        sb.Append("<br><br>Berikut data Part Employee baru :")
        sb.Append("</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=10></td>")
        sb.Append("</tr>")
        sb.Append("</table>")

        sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'>")
        sb.Append("<tr>")

        sb.Append("<tr>")
        sb.Append("<td width='35%'>Dealer</td>")
        sb.Append("<td width='5%'>:</td>")
        sb.Append("<td width='60%'><b>" & objSalesmanHeader.Dealer.DealerCode & " / " & objSalesmanHeader.Dealer.SearchTerm2 & "</b></td>")
        sb.Append("</tr>")

        sb.Append("<tr>")
        sb.Append("<td width='35%'>Nama</td>")
        sb.Append("<td width='5%'>:</td>")
        sb.Append("<td width='60%'><b>" & objSalesmanHeader.Name & "</b></td>")
        sb.Append("</tr>")

        If objSalesmanHeader.SalesmanAdditionalInfo.Count > 0 Then
            sb.Append("<tr>")
            sb.Append("<td width='35%'>Kategori</td>")
            sb.Append("<td width='5%'>:</td>")
            sb.Append("<td width='60%'><b>" & CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanCategoryLevel.SalesmanCategoryLevel.PositionName & "</b></td>")
            sb.Append("</tr>")

            sb.Append("<tr>")
            sb.Append("<td width='35%'>Posisi</td>")
            sb.Append("<td width='5%'>:</td>")
            sb.Append("<td width='60%'><b>" & CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanCategoryLevel.PositionName & "</b></td>")
            sb.Append("</tr>")

            Dim EnumLevel As EnumSalesmanPartLevel.Level = CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanLevel
            If EnumLevel <> 99 Then
                sb.Append("<tr>")
                sb.Append("<td width='35%'>Level</td>")
                sb.Append("<td width='5%'>:</td>")
                sb.Append("<td width='60%'><b>" & EnumLevel.ToString & "</b></td>")
                sb.Append("</tr>")
            End If

        End If

        sb.Append("<tr>")
        sb.Append("<td width='35%'>Tanggal Masuk</td>")
        sb.Append("<td width='5%'>:</td>")
        sb.Append("<td width='60%'><b>" & objSalesmanHeader.HireDate.ToString("dd MMM yyyy") & "</b></td>")
        sb.Append("</tr>")
        Dim objUser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        sb.Append("<tr>")
        sb.Append("<td width='35%'>Diajukan oleh</td>")
        sb.Append("<td width='5%'>:</td>")
        sb.Append("<td width='60%'><b>" & objUser.Dealer.DealerCode & " - " & objUser.UserName & "</b></td>")
        sb.Append("</tr>")

        sb.Append("</table>")

        sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'>")
        sb.Append("<tr>")
        sb.Append("<td width='100%'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<tr>")
        sb.Append("<td width='100%'>Untuk registrasi data Part Employee diatas, dapat diakses pada link dibawah ini :</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td width='100%'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td width='100%'><font color='blue'><a href='" & urlRequest & "'>MMKSI DNet Sparepart</a></font></td>")
        sb.Append("</tr>")
        sb.Append("</table>")
        sb.Append("</FONT>")

        Return sb.ToString

    End Function

    'Add by Deni Firdaus, 14 September 2017
    Private Function GetTrTraineeId(ByVal salesmanHeaderId As Integer) As ArrayList
        Dim criteria As New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        criteria.opAnd(New Criteria(GetType(TrTrainee), "SalesmanHeader.ID", MatchType.Exact, salesmanHeaderId))
        Dim arrItem As ArrayList = New TrTraineeFacade(User).Retrieve(criteria)

        Return arrItem

    End Function

    Private Sub dgSalesmanHeader_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSalesmanHeader.ItemDataBound

        If Not e.Item.DataItem Is Nothing Then
            Dim objSalesmanHeader As SalesmanHeader = e.Item.DataItem
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgSalesmanHeader.CurrentPageIndex * dgSalesmanHeader.PageSize)

            Dim lblKodeDealerNew As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)
            lblKodeDealerNew.Text = objSalesmanHeader.Dealer.DealerCode

            Dim lblKodeDealerBranch As Label = CType(e.Item.FindControl("lblKodeDealerBranch"), Label)
            If Not IsNothing(objSalesmanHeader.DealerBranch) Then
                lblKodeDealerBranch.Text = objSalesmanHeader.DealerBranch.DealerBranchCode
            End If

            Dim lblJobPositionId_MainNew As Label = CType(e.Item.FindControl("lblJobPositionId_Main"), Label)
            If objSalesmanHeader.SalesmanAdditionalInfo.Count > 0 Then
                If Not IsNothing(CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo)) Then
                    lblJobPositionId_MainNew.Text = CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanCategoryLevel.SalesmanCategoryLevel.PositionName
                Else
                    lblJobPositionId_MainNew.Text = String.Empty
                End If
            Else
                lblJobPositionId_MainNew.Text = String.Empty
            End If

            Dim lblPosisi As Label = CType(e.Item.FindControl("lblPosisi"), Label)
            If objSalesmanHeader.SalesmanAdditionalInfo.Count > 0 Then
                lblPosisi.Text = CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanCategoryLevel.PositionName
            Else
                lblPosisi.Text = String.Empty
            End If

            Dim lblLevel As Label = CType(e.Item.FindControl("lblLevel"), Label)
            If objSalesmanHeader.SalesmanAdditionalInfo.Count > 0 Then
                Dim oSAi As SalesmanAdditionalInfo = CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo)
                If oSAi.SalesmanCategoryLevel.ID = 12 Then
                    Dim iLevel As Integer = CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanLevel
                    If iLevel <> 99 Then
                        Dim EnumLevel As EnumSalesmanPartLevel.Level = iLevel
                        lblLevel.Text = EnumLevel.ToString
                    Else
                        lblLevel.Text = String.Empty
                    End If
                Else
                    lblLevel.Text = String.Empty
                End If

            Else
                lblLevel.Text = String.Empty
            End If

            Dim lblStatusNew As Label = CType(e.Item.FindControl("lblStatus"), Label)
            lblStatusNew.Text = CType(objSalesmanHeader.Status, EnumSalesmanStatus.SalesmanStatus).ToString.Replace("_", " ")

            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                e.Item.FindControl("lbtnDelete").Visible = False
            End If

            ' case Dealer saja yang bisa mengedit, refer bug 1393
            If objSalesmanHeader.Status = CType(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif, String) Then
                'If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                e.Item.FindControl("lbtnEdit").Visible = False
                'End If
            Else
                If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    e.Item.FindControl("lbtnEdit").Visible = bCheckEditDetailPrivilege
                Else
                    e.Item.FindControl("lbtnEdit").Visible = bCheckEditDetailKTBPrivilege
                End If
            End If

            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                e.Item.FindControl("lbtnDelete").Visible = bCekDeletePriv
                e.Item.FindControl("lbtnRegister").Visible = False
            Else
                e.Item.FindControl("lbtnDelete").Visible = False
                If DateTime.op_LessThanOrEqual(objSalesmanHeader.ResignDate, New DateTime(1900, 1, 1)) Then
                    e.Item.FindControl("lbtnEdit").Visible = bCheckEditDetailPrivilege 'True
                Else
                    e.Item.FindControl("lbtnEdit").Visible = False
                End If
                'Register
                If objSalesmanHeader.Status = CType(EnumSalesmanStatus.SalesmanStatus.Baru, String) Then
                    e.Item.FindControl("lbtnRegister").Visible = Priv_RequestID 'True
                Else
                    e.Item.FindControl("lbtnRegister").Visible = False
                End If
            End If

            e.Item.FindControl("lbtnView").Visible = bCekViewDetailsPriv

            'Partshop
            If objSalesmanHeader.SalesmanAdditionalInfo.Count > 0 Then
                Dim salesInfo As SalesmanAdditionalInfo = CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo)
                If salesInfo.SalesmanCategoryLevel.ID = 12 AndAlso objSalesmanHeader.SalesmanCode <> "" Then
                    e.Item.FindControl("lbtnPartshop").Visible = Priv_Partshop 'True
                Else
                    e.Item.FindControl("lbtnPartshop").Visible = False
                End If
            End If

            'bind No Registrasi(source : TraineeID)
            Dim dataList = GetTrTraineeId(objSalesmanHeader.ID)
            Dim lblTraineeID As Label = e.Item.FindControl("lblSalesmanHeaderID")
            For Each item As TrTrainee In dataList
                lblTraineeID.Text = item.ID
            Next item
        End If

        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If
    End Sub
    Private Sub dgSalesmanHeader_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSalesmanHeader.PageIndexChanged
        dgSalesmanHeader.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgSalesmanHeader.CurrentPageIndex)

    End Sub
    Private Sub dgSalesmanHeader_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSalesmanHeader.SortCommand
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
        dgSalesmanHeader.SelectedIndex = -1
        BindDataGrid(dgSalesmanHeader.CurrentPageIndex)
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClearData()
    End Sub
    Private Sub btnDownloadExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownloadExcel.Click
        SetDownload()
    End Sub

#End Region


End Class
