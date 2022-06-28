#Region "Custom Namespace Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.Profile

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

Public Class FrmCcCSTeamList
    Inherits System.Web.UI.Page



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
    Private EmptyDate As String = "01/01/0001 0:00:00"
    Private _downloadPriv As Boolean = False

    Private helpers As TrainingHelpers = New TrainingHelpers(Me.Page, "DAFTAR CS EMPLOYEE")
#End Region

#Region "Privilege"
    Private Sub CheckPrivilege()
        'If Not SecurityProvider.Authorize(Context.User, SR.CSO_View_CS_Employee_privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=CS Employee - Daftar CS Employee")
        'End If
    End Sub

    'Dim bCheckEditDetailKTBPrivilege As Boolean = SecurityProvider.Authorize(Context.User, SR.CSO_View_CS_Employee_privilege)
    'Dim bCheckEditDetailPrivilege As Boolean = SecurityProvider.Authorize(Context.User, SR.CSO_View_CS_Employee_privilege)
    'Dim bCekDLPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.CSO_View_CS_Employee_privilege)
    'Dim bCekViewDetailsPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.Download_daftar_salesman_part_privilege)

    Dim bCheckEditDetailKTBPrivilege As Boolean = True
    Dim bCheckEditDetailPrivilege As Boolean = True
    Dim bCekDLPriv As Boolean = True
    Dim bCekViewDetailsPriv As Boolean = True

#End Region

#Region "Custom Method"
    Private Sub SetSetting()
        lblPageTitle.Text = "Daftar CS Team"
        lblSalesmanID.Text = "Employee CS ID"
        sessHelper.SetSession("strFileNm", "SalesmanPartUnit")
        sessHelper.SetSession("strFileNmHeader", "CS Employee List")
    End Sub
    ' untuk menampung kriteria yg sebelumnya
    Private Sub SaveCriteria()
        Dim crits As Hashtable = New Hashtable
        crits.Add("SalesmanCode", txtID.Text)
        crits.Add("Name", txtNama.Text)
        crits.Add("PosisiID", ddlPosisi.SelectedValue)
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

            For i = 0 To ddlPosisi.Items.Count - 1
                If CType(ddlPosisi.Items(i).Value, Integer) = CType(crits.Item("PosisiID"), Integer) Then
                    ddlPosisi.SelectedIndex = i
                End If
            Next
            'ddlPosisi.SelectedValue = IIf(CInt(crits.Item("PosisiID")) > 0, CInt(crits.Item("PosisiID")), 0)
            'ddlPosisi_SelectedIndexChanged(Me, Nothing)

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
        ICTanggalMasukFrom.Text = ""
        ICTanggalMasukTo.Text = ""
        ICTanggalKeluarFrom.Text = ""
        ICTanggalKeluarTo.Text = ""
    End Sub
    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)
        Dim CriteriaDownload As New CriteriaComposite(New Criteria(GetType(V_SalesmanPart), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        ' khusus spare part
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, CType(EnumSalesmanUnit.SalesmanUnit.CS, Short)), "(", True)
        criterias.opOr(New Criteria(GetType(SalesmanHeader), "JobPosition.ID", MatchType.Exact, 6), ")", False) 'Branch Manager

        objDealer = (CType(sessHelper.GetSession("Dealer"), Dealer))

        If (txtDealerCode.Text.Trim <> String.Empty) Then
            'criterias.opAnd(New Criteria(GetType(SalesmanHeader), "DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"), "(", True)
            'criterias.opOr(New Criteria(GetType(SalesmanHeader), "SalesmanHeaderToDealer.Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"), ")", False)
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
        End If

        If txtID.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesmanCode", MatchType.[Partial], txtID.Text.Trim()))

        End If

        If txtNama.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Name", MatchType.[Partial], txtNama.Text.Trim()))
        End If

        If ddlPosisi.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "JobPosition.ID", MatchType.Exact, ddlPosisi.SelectedValue))
        End If

        If ddlStatus.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        Try
            If ICTanggalMasukFrom.Value.ToString() <> EmptyDate Or ICTanggalMasukTo.Value.ToString <> EmptyDate Then
                ValidateDateMasuk()
                criterias.opAnd(New Criteria(GetType(SalesmanHeader), "HireDate", MatchType.GreaterOrEqual, ICTanggalMasukFrom.Value))
                criterias.opAnd(New Criteria(GetType(SalesmanHeader), "HireDate", MatchType.LesserOrEqual, ICTanggalMasukTo.Value.AddDays(1)))
            End If


            If ICTanggalKeluarFrom.Value.ToString() <> EmptyDate Or ICTanggalKeluarTo.Value.ToString() <> EmptyDate Then
                ValidateDateKeluar()
                criterias.opAnd(New Criteria(GetType(SalesmanHeader), "ResignDate", MatchType.GreaterOrEqual, ICTanggalKeluarFrom.Value))
                criterias.opAnd(New Criteria(GetType(SalesmanHeader), "ResignDate", MatchType.LesserOrEqual, ICTanggalKeluarTo.Value.AddDays(1)))
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return
        End Try
       


        sessHelper.SetSession("criterias", criterias)

    End Sub

    Private Sub ValidateDateMasuk()
        Try

            If ICTanggalMasukFrom.Value.ToString() = EmptyDate Or ICTanggalMasukTo.Value.ToString = EmptyDate Then
                Throw New Exception("")
            End If

            If Not ICTanggalMasukFrom.Value <= ICTanggalMasukTo.Value Then
                Throw New Exception("")
            End If
        Catch ex As Exception
            Throw New Exception("Tanggal masuk dari harus lebih kecil dari tanggal masuk sampai")
        End Try
    End Sub

    Private Sub ValidateDateKeluar()
        Try

            If ICTanggalKeluarFrom.Value.ToString() = EmptyDate Or ICTanggalKeluarTo.Value.ToString = EmptyDate Then
                Throw New Exception("")
            End If

            If Not ICTanggalKeluarFrom.Value <= ICTanggalKeluarTo.Value Then
                Throw New Exception("")
            End If
        Catch ex As Exception
            Throw New Exception("Tanggal keluar dari harus lebih kecil dari tanggal keluar sampai")
        End Try
    End Sub

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList

        arrList = New SalesmanHeaderFacade(User).RetrieveByCriteria(CType(sessHelper.GetSession("criterias"), CriteriaComposite), idxPage + 1, dgSalesmanHeader.PageSize, totalRow, _
                CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgSalesmanHeader.CurrentPageIndex = idxPage
        dgSalesmanHeader.DataSource = arrList
        dgSalesmanHeader.VirtualItemCount = totalRow
        dgSalesmanHeader.DataBind()
        sessHelper.SetSession("idxPage", dgSalesmanHeader.CurrentPageIndex)

    End Sub
    Private Sub BindDropDown()

        Dim CommFucntion As New CommonFunction

        CommFucntion.BindEnumDetailToDDL(ddlStatus, "EMP_STT")

        CommFucntion.BindEnumDetailToDDL(ddlPosisi, "EMP_POS_CS")

        ddlPosisi.Items.Insert(1, New ListItem("Branch manager", 6))


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





    End Sub




    Private Sub Delete(ByVal nID As Integer)

        Dim totalRow As Integer = 0
        Dim arrListSalesmanTrainingParticipant As New ArrayList
        Dim criteriasSalesmanTrainingParticipant As New CriteriaComposite(New Criteria(GetType(SalesmanTrainingParticipant), "SalesmanHeader.ID", MatchType.Exact, nID))
        Dim strAddMsg As String = "status penghapusan CS Member tdk bisa dilakukan"

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

        'Dim confirmValue As String = Request.Form("confirm_value")
        'Dim cV As String
        'MessageBox.Confirm("Apakah Anda Yakin Menghapus", cV)
        'If confirmValue = "Yes" Then

        iReturn = facade.Update(objSalesmanHeader) 'no need KTP validation
        If iReturn < 0 Then
            MessageBox.Show(SR.DeleteFail)
        Else
            DeleteAllSalesmanProfile(objSalesmanHeader.ID)
            MessageBox.Show(SR.DeleteSucces)
        End If

        dgSalesmanHeader.CurrentPageIndex = 0
        BindDataGrid(dgSalesmanHeader.CurrentPageIndex)

        ' End If
    End Sub

    Private Sub DeleteAllSalesmanProfile(ByVal salesmanHeaderId As Integer)
        Dim facade As SalesmanProfileFacade = New SalesmanProfileFacade(User)
        Dim criteria As New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.ID", MatchType.Exact, salesmanHeaderId))

        Dim arlProfile As ArrayList = facade.Retrieve(criteria)

        If arlProfile.Count > 0 Then

            For Each Profile As SalesmanProfile In arlProfile
                Profile.RowStatus = CShort(DBRowStatus.Deleted)
                facade.Update(Profile)
            Next
        End If

    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        Dim crits As CriteriaComposite
        If dgSalesmanHeader.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        'If Not IsNothing(sessHelper.GetSession("criteriadownload")) Then
        '    crits = CType(sessHelper.GetSession("criteriadownload"), CriteriaComposite)
        'End If

        crits = New CriteriaComposite(New Criteria(GetType(V_SalesmanCSTeam), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(V_SalesmanCSTeam), "SalesIndicator", MatchType.Exact, CType(EnumSalesmanUnit.SalesmanUnit.CS, Short)), "(", True)
        crits.opOr(New Criteria(GetType(V_SalesmanCSTeam), "JobPositionId_Main", MatchType.Exact, 6), ")", False) 'Branch Manager

        objDealer = (CType(sessHelper.GetSession("Dealer"), Dealer))

        If (txtDealerCode.Text.Trim <> String.Empty) Then
            crits.opAnd(New Criteria(GetType(V_SalesmanCSTeam), "DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))

        End If

        If txtID.Text.Trim <> "" Then
            crits.opAnd(New Criteria(GetType(V_SalesmanCSTeam), "SalesmanCode", MatchType.[Partial], txtID.Text.Trim()))

        End If

        If txtNama.Text.Trim <> "" Then
            crits.opAnd(New Criteria(GetType(V_SalesmanCSTeam), "Name", MatchType.[Partial], txtNama.Text.Trim()))
        End If

        If ddlPosisi.SelectedIndex <> 0 Then
            crits.opAnd(New Criteria(GetType(V_SalesmanCSTeam), "JobPositionId_Main", MatchType.Exact, ddlPosisi.SelectedValue))
            'Dim sqlPos As String = "select ID from V_SalesmanPart where PosisiID =" & CType(ddlPosisi.SelectedValue, Integer)
            'criterias.opAnd(New Criteria(GetType(SalesmanHeader), "ID", MatchType.InSet, "(" & sqlPos & ")"))
        End If

        If ddlStatus.SelectedIndex <> 0 Then
            crits.opAnd(New Criteria(GetType(V_SalesmanCSTeam), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        ' mengambil data yang dibutuhkan
        arrData = New SalesmanHeaderFacade(User).RetrieveV_SalesmanCS(crits)
        If arrData.Count > 0 Then
            DoDownload(arrData)
        End If

    End Sub
    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String

        strFileNm = "CSEmployee"

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
        Dim err As String
        Dim itemLine As StringBuilder = New StringBuilder
        Dim objSmanFacade As New SalesmanHeaderFacade(User)

        Try


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
                'itemLine.Append("Kategori" & tab)
                itemLine.Append("Posisi" & tab)
                itemLine.Append("User DNET" & tab)
                'itemLine.Append("Level" & tab)
                'itemLine.Append("Superior Code" & tab)
                'itemLine.Append("Superior Name" & tab)
                itemLine.Append("Status" & tab)
                itemLine.Append("Tanggal Masuk" & tab)
                'itemLine.Append("Area" & tab)
                itemLine.Append("Tanggal Input" & tab)
                sw.WriteLine(itemLine.ToString())

                Dim i As Integer = 1
                For Each item As V_SalesmanCSTeam In data
                    err = item.SalesmanCode
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(i.ToString & tab)
                    itemLine.Append(item.DealerCode & tab)
                    itemLine.Append(item.SalesmanCode & tab)
                    itemLine.Append(item.Name & tab)
                    itemLine.Append(item.PlaceOfBirth.ToString & tab)
                    itemLine.Append(Format(item.DateOfBirth, "dd/MM/yyyy").ToString & tab)

                    itemLine.Append(CommonFunction.GetEnumDescription(item.Gender, "JK").ToString & tab)

                    itemLine.Append(CommonFunction.GetEnumDescription(item.MarriedStatus, "STAKWIN") & tab)
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
                    'itemLine.Append(item.DivisiName & tab)
                    If Not IsNothing(item.User_DNET) Then
                        itemLine.Append(item.User_DNET & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    itemLine.Append(item.PosisiName & tab)
                    'itemLine.Append(item.LevelName & tab)
                    'itemLine.Append(item.LeaderCode & tab)
                    'itemLine.Append(item.LeaderName & tab)
                    itemLine.Append(CType(item.Status, EnumSalesmanStatus.SalesmanStatus).ToString.Replace("_", " ") & tab)
                    If Not IsNothing(item.HireDate) Then
                        itemLine.Append(item.HireDate & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    'itemLine.Append(item.AreaDesc & tab)
                    itemLine.Append(Format(item.CreatedTime, "dd/MM/yyyy").ToString & tab)
                    sw.WriteLine(itemLine.ToString())
                    i = i + 1

                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message + "  //  " + " Data Berikut :  " + err + "  Invalid")
        End Try
    End Sub

    Private Function GenerateCode(ByVal ParamId As Integer) As String
        Dim objSalesmanHeader As New SalesmanHeader
        objSalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(ParamId)
        objSalesmanHeader.RegisterStatus = CType(EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Sudah_Register, String)
        objSalesmanHeader.Status = CType(EnumSalesmanStatus.SalesmanStatus.Aktif, String)
        objSalesmanHeader.SalesIndicator = CType(EnumSalesmanUnit.SalesmanUnit.CS, Short)
        objSalesmanHeader.SalesmanCode = "CS_Employee"


        Dim vr As ValidResult = New SalesmanHeaderValidation().ValidateKTPSalesmanHeader(objSalesmanHeader)

        If vr.IsValid = False Then
            MessageBox.Show(vr.Message)
            Exit Function
        End If

        Dim nResult As Integer = New SalesmanHeaderFacade(User).Update(objSalesmanHeader)


        If nResult = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            MessageBox.Show(SR.SaveSuccess)
            objSalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(nResult)
            BindDataGrid(0)
        End If


    End Function

#End Region

#Region "event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'InitiateAuthorization()
        helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.view, SR.TrainingCsViewListTeam_Privilege)
        helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.fullAccess, SR.TrainingCsEditListTeam_Privilege)
        helpers.Privilage()
        ' CheckPrivilege()
        objDealer = (CType(sessHelper.GetSession("Dealer"), Dealer))
        If Not IsPostBack Then
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                txtDealerCode.Text = objDealer.DealerCode
                txtDealerCode.ReadOnly = False
                lblSearchDealer.Visible = False
                txtDealerCode.Enabled = False
                trTglMasuk.Visible = True
                trTglKeluar.Visible = True
            End If

            BindDropDown()
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
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
        Dim strMode As String = "CS"
        Dim lblKodeDealer As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)

        Select Case e.CommandName.ToLower()
            Case "view"
                Dim salesmanheaderData As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
                If salesmanheaderData.JobPosition.ID = 6 Then
                    Response.Redirect("~/Salesman/FrmSalesmanHeader.aspx?ID=" + e.Item.Cells(0).Text + "&Mode=unit&Source=CSList")
                Else
                    Response.Redirect("FrmCcInputCSTeam.aspx?ID=" + e.Item.Cells(0).Text + "&view=true&Mode=" + strMode)
                End If

            Case "edit"
                Dim salesmanheaderData As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
                If salesmanheaderData.JobPosition.ID = 6 Then
                    Response.Redirect("~/Salesman/FrmSalesmanHeader.aspx?ID=" + e.Item.Cells(0).Text + "&edit=true" + "&Mode=unit&Source=CSList")
                Else
                    Response.Redirect("FrmCcInputCSTeam.aspx?ID=" + e.Item.Cells(0).Text + "&edit=true" + "&Mode=" + strMode)
                End If

            Case "generatecode"
                GenerateCode(Val(e.Item.Cells(0).Text))

            Case "konfirmasi"
                GenerateCode(Val(e.Item.Cells(0).Text))

            Case "resigne"
                Resign(Val(e.Item.Cells(0).Text))

            Case "delete"
                Delete(e.Item.Cells(0).Text)

            Case "asignetodealer"
                AsigneToDealer(CInt(e.Item.Cells(0).Text))

            
        End Select
    End Sub

    Private Sub AsigneToDealer(ByVal shID As Integer)
        Response.Redirect("FrmCcCSAsigneToDealer.aspx?shID=" + shID.ToString())
    End Sub

    Private Sub Resign(ByVal id As Integer)
        Dim objSalHea As SalesmanHeader
        objSalHea = New SalesmanHeaderFacade(User).Retrieve(id)

        objSalHea.ResignDate = Now.Date
        objSalHea.ResignReason = "Pindah Dealer"
        objSalHea.ResignType = 1
        objSalHea.Status = CType(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif, String)

        Dim nResult = New SalesmanHeaderFacade(User).Update(objSalHea)
        If nResult < 0 Then
            MessageBox.Show(SR.UpdateFail)
        Else
            MessageBox.Show(SR.UpdateSucces)
            ViewState.Add("vsProcess", "Default")
            BindDataGrid(0)
        End If

    End Sub

    Private Sub RequestID(ByVal empID As Integer)
        Dim facade As SalesmanHeaderFacade = New SalesmanHeaderFacade(User)
        Dim objSalesmanHeader As SalesmanHeader = facade.Retrieve(empID)
        objSalesmanHeader.IsRequestID = EnumSalesmanIsRequest.SalesmanIsRequest.Sudah_Request
        'objSalesmanHeader.Status = EnumSalesmanStatus.SalesmanStatus.Konfirmasi

        Dim vr As ValidResult = New SalesmanHeaderValidation().ValidateKTPSalesmanHeader(objSalesmanHeader)

        If vr.IsValid = False Then
            MessageBox.Show(vr.Message)
            Exit Sub
        End If

        Dim nResult As Integer = New SalesmanHeaderFacade(User).Update(objSalesmanHeader)

        If nResult = -1 Then
            MessageBox.Show("Proses request ID gagal.")
        Else
            MessageBox.Show("Proses request ID berhasil.")
            SendEmail(objSalesmanHeader, True)
            BindDataGrid(dgSalesmanHeader.CurrentPageIndex)
        End If
    End Sub

    Private Sub SendEmail(ByVal objSalesmanHeader As SalesmanHeader, ByVal bStatus As Boolean) ' bStatus = New (true) or Update(false) Employee
        Dim appConfigFacade As New AppConfigFacade(User)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Dim emailFrom As String = appConfigFacade.Retrieve(TrainingCenterEmail.EMAIL_FROM_CS).Value
        Dim emailTo As String = appConfigFacade.Retrieve(TrainingCenterEmail.EMAIL_TO_CS_ADMIN).Value
        Dim UrlPartEmpGenerate As String = KTB.DNet.Lib.WebConfig.GetValue("UrlCSEmpGenerate")
        Dim urlPartEmpList As String = KTB.DNet.Lib.WebConfig.GetValue("UrlCSEmpList")

        Dim valueEmail As String
        If bStatus Then
            valueEmail = GenerateEmailNewEmployee(objSalesmanHeader, UrlPartEmpGenerate)
            ObjEmail.sendMail(emailTo, "", emailFrom, "[KTB-DNet] CS - Request Employee CS Code ", Mail.MailFormat.Html, valueEmail)
        End If

    End Sub

    Private Function GenerateEmailNewEmployee(ByVal objSalesmanHeader As SalesmanHeader, ByVal urlRequest As String) As String

        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder("")
        sb.Append("<FONT face=Arial size=1>")
        sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 align=center><b>Request CS Employee Code</b></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=50></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=50>")
        sb.Append("Dengan hormat,&nbsp;")
        sb.Append("<br><br>Berikut data CS Employee baru :")
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
        sb.Append("<td width='100%'>Untuk registrasi data CS Employee diatas, dapat diakses pada aplikasi D-NET</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td width='100%'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        ' sb.Append("<td width='100%'><font color='blue'><a href='" & urlRequest & "'>MMKSI DNet Call Center</a></font></td>")
        sb.Append("</tr>")
        sb.Append("</table>")
        sb.Append("</FONT>")

        Return sb.ToString

    End Function

    Private Sub dgSalesmanHeader_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSalesmanHeader.ItemDataBound
        Dim CommFunction As CommonFunction

        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As SalesmanHeader = e.Item.DataItem
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgSalesmanHeader.CurrentPageIndex * dgSalesmanHeader.PageSize)

            Dim lblKodeDealerNew As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)
            lblKodeDealerNew.Text = RowValue.Dealer.DealerCode

            CommFunction = New CommonFunction

            Dim lblPosisi As Label = CType(e.Item.FindControl("lblPosisi"), Label)

            If RowValue.JobPosition.ID = 6 Then
                lblPosisi.Text = "Branch Manager"
            Else
                lblPosisi.Text = CommFunction.GetEnumDescription(CType(RowValue.JobPosition.ID, Integer), "EMP_POS_CS")
            End If


            Dim lbtnKonfirmasi As LinkButton = e.Item.FindControl("lbtnKonfirmasi")
            Dim lbtnGenerateCode As LinkButton = e.Item.FindControl("lbtnGenerateCode")
            Dim lbtnResign As LinkButton = e.Item.FindControl("lbtnResign")
            Dim lbtnAsigne As LinkButton = e.Item.FindControl("lbtnAsigne")
            Dim lbtnresignedate As Label = e.Item.FindControl("lbtnresignedate")
            lbtnresignedate.Visible = False
            lbtnKonfirmasi.Visible = False
            lbtnGenerateCode.Visible = False
            lbtnResign.Visible = False
            lbtnAsigne.Visible = False
           

            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                If RowValue.Status = CType(EnumSalesmanStatus.SalesmanStatus.Konfirmasi, String) And RowValue.SalesIndicator = 4 Then
                    lbtnKonfirmasi.Visible = True
                End If
                If RowValue.Status = CType(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif, String) And RowValue.SalesIndicator = 4 Then
                    lbtnresignedate.Attributes.Add("OnClick", String.Format("ShowChangesCSResigne('{0}')", RowValue.ID.ToString))
                    lbtnresignedate.Visible = True
                End If
                If RowValue.Status = CType(EnumSalesmanStatus.SalesmanStatus.Baru, String) And RowValue.IsRequestID = EnumSalesmanIsRequest.SalesmanIsRequest.Sudah_Request Then
                    lbtnGenerateCode.Visible = True
                End If
            End If

            If RowValue.Status = CType(EnumSalesmanStatus.SalesmanStatus.Aktif, String) And lblPosisi.Text <> "Branch Manager" Then
                lbtnAsigne.Visible = True
            End If

            Dim lblStatusNew As Label = CType(e.Item.FindControl("lblStatus"), Label)
            lblStatusNew.Text = CommFunction.GetEnumDescription(RowValue.Status, "EMP_STT")

            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Or RowValue.JobPosition.ID = 6 Then
                e.Item.FindControl("lbtnEdit").Visible = False
            Else
                e.Item.FindControl("lbtnEdit").Visible = bCheckEditDetailPrivilege
            End If

            e.Item.FindControl("lbtnView").Visible = bCekViewDetailsPriv

            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                If RowValue.Status = EnumSalesmanStatus.SalesmanStatus.Baru And objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    If RowValue.JobPosition.ID <> 6 Then
                        CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = True
                        CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "if(!confirm('Yakin data ini akan dihapus?')) return false;")
                    Else
                        CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = False
                    End If
                Else
                    CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = False
                End If
            End If

            If RowValue.JobPosition.ID = 6 Then
                lbtnGenerateCode.Visible = False
                lbtnKonfirmasi.Visible = False
            End If

            If Not helpers.IsEdit Then

                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = False
                e.Item.FindControl("lbtnEdit").Visible = False
                e.Item.FindControl("lbtnResign").Visible = False
                e.Item.FindControl("lbtnAsigne").Visible = False
                e.Item.FindControl("lbtnKonfirmasi").Visible = False
                e.Item.FindControl("lbtnGenerateCode").Visible = False
            End If

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