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
Imports KTB.DNet.BusinessFacade.Training
Imports System.IO
Imports System.Text
Imports OfficeOpenXml
Imports System.Collections.Generic

#End Region

Public Class frmSalesmanList
    Inherits System.Web.UI.Page


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents ddlUnit As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtID As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNama As System.Web.UI.WebControls.TextBox
    'Protected WithEvents ddlPosisi As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalesmanID As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalesmanUnit As System.Web.UI.WebControls.Label
    Protected WithEvents dgSalesmanHeader As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents ddlSalesmanLevel As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnDownloadExcel As System.Web.UI.WebControls.Button
    Protected WithEvents ddlSalesLevel2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnChange As System.Web.UI.WebControls.Button

    Protected WithEvents lblTotalRecord As System.Web.UI.WebControls.Label
    Protected WithEvents chkHireDate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkResignDate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkDateOfBirth As System.Web.UI.WebControls.CheckBox
    Protected WithEvents icHireDateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icHireDateUntil As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icResignDateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icResignDateUntil As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icDateOfBirthFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icDateOfBirthUntil As KTB.DNet.WebCC.IntiCalendar

    Protected WithEvents lblColonHireDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblUntilHireDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblColonResignDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblUntilResignDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblColonDOB As System.Web.UI.WebControls.Label
    Protected WithEvents lblUntilDOB As System.Web.UI.WebControls.Label

    Protected WithEvents chkPosisi As System.Web.UI.WebControls.CheckBoxList
    Protected WithEvents chkStatus As System.Web.UI.WebControls.CheckBoxList



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
        If Not SecurityProvider.Authorize(Context.User, SR.TPViewList_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Tenaga Penjual - Daftar Tenaga Penjual")
        End If
    End Sub

    Dim bCheckEditDetailPrivilege As Boolean = SecurityProvider.Authorize(Context.User, SR.TPListEditDetail_Privilege)
    Dim bCekDLPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.SalesmanListDownload_Privilege)
    Dim bCekViewDetailsPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.TPListViewDetail_Privilege)
#End Region

#Region "Custom Method"
    Private Sub SetSetting()
        If Not IsNothing(Request.QueryString("Mode")) Then
            Select Case Request.QueryString("Mode")
                Case "unit"
                    lblPageTitle.Text = "TENAGA PENJUAL - Daftar Tenaga Penjual "
                    ddlUnit.SelectedValue = EnumSalesmanUnit.SalesmanUnit.Unit
                    lblSalesmanID.Text = "Salesman ID"
                    lblSalesmanUnit.Text = "Salesman Unit"
                    sessHelper.SetSession("strFileNm", "SalesmanUnit")
                    sessHelper.SetSession("strFileNmHeader", "Salesman Unit List")
                Case "part"
                    lblPageTitle.Text = "SALESMAN PART - Daftar Salesman Part "
                    ddlUnit.SelectedValue = EnumSalesmanUnit.SalesmanUnit.Sparepart
                    lblSalesmanID.Text = "Salesman Part ID"
                    lblSalesmanUnit.Text = "Salesman Part Unit"
                    sessHelper.SetSession("strFileNm", "SalesmanPartUnit")
                    sessHelper.SetSession("strFileNmHeader", "Salesman Part List")
                Case "servis"
                    lblPageTitle.Text = "STAFF SERVICE - Daftar Staff Service"
                    ddlUnit.SelectedValue = EnumSalesmanUnit.SalesmanUnit.Mekanik
                    lblSalesmanID.Text = "Staff Servis ID"
                    lblSalesmanUnit.Text = "Servis Unit"
                    sessHelper.SetSession("strFileNm", "ServisUnit")
                    sessHelper.SetSession("strFileNmHeader", "Servis Unit List")
            End Select
            ddlUnit.Enabled = False
        Else
            lblPageTitle.Text = "UMUM - Daftar Salesman ..."
        End If
    End Sub
    ' untuk menampung kriteria yg sebelumnya
    Private Sub SaveCriteria()
        Dim crits As Hashtable = New Hashtable
        crits.Add("DealerCode", txtDealerCode.Text)
        crits.Add("UnitIndicator", ddlUnit.SelectedValue)
        crits.Add("SalesmanCode", txtID.Text)
        crits.Add("Name", txtNama.Text)
        Dim valueStatus As String = chkStatus.GetSelectedValue()
        Dim valuePosisi As String = chkPosisi.GetSelectedValue()
        crits.Add("JobPositionID", valuePosisi)
        crits.Add("Status", valueStatus)
        crits.Add("chkHireDate", IIf(chkHireDate.Checked, True, False))
        crits.Add("chkResignDate", IIf(chkResignDate.Checked, True, False))
        crits.Add("chkDateOfBirth", IIf(chkDateOfBirth.Checked, True, False))
        crits.Add("HireDateFrom", icHireDateFrom.Value)
        crits.Add("HireDateUntil", icHireDateUntil.Value)
        crits.Add("ResignDateFrom", icResignDateFrom.Value)
        crits.Add("ResignDateUntil", icResignDateUntil.Value)
        crits.Add("DateOfBirthFrom", icDateOfBirthFrom.Value)
        crits.Add("DateOfBirthUntil", icDateOfBirthUntil.Value)
        sessHelper.SetSession("frmSalesmanList", crits)
    End Sub
    ' untuk menampilkan data kriteria sebelumnya
    Private Sub ReadCriteria()
        Dim crits As Hashtable
        crits = CType(sessHelper.GetSession("frmSalesmanList"), Hashtable)
        If Not IsNothing(crits) Then
            txtDealerCode.Text = CStr(crits.Item("DealerCode"))
            ddlUnit.SelectedValue = CStr(crits.Item("UnitIndicator"))
            txtID.Text = CStr(crits.Item("SalesmanCode"))
            txtNama.Text = CStr(crits.Item("Name"))
            Dim posisiValue As String = CStr(crits.Item("JobPositionID"))
            Dim statusValue As String = CStr(crits.Item("Status"))
            chkHireDate.Checked = CBool(crits.Item("chkHireDate"))
            chkResignDate.Checked = CBool(crits.Item("chkResignDate"))
            chkDateOfBirth.Checked = CBool(crits.Item("chkDateOfBirth"))
            icHireDateFrom.Value = CDate(crits.Item("HireDateFrom"))
            icHireDateUntil.Value = CDate(crits.Item("HireDateUntil"))
            icResignDateFrom.Value = CDate(crits.Item("ResignDateFrom"))
            icResignDateUntil.Value = CDate(crits.Item("ResignDateUntil"))
            icDateOfBirthFrom.Value = CDate(crits.Item("DateOfBirthFrom"))
            icDateOfBirthUntil.Value = CDate(crits.Item("DateOfBirthUntil"))

            chkPosisi.ClearSelection()
            If Not String.IsNullorEmpty(posisiValue) Then
                posisiValue = posisiValue.Replace("(", "").Replace(")", "")
                Dim arrPosisi() As String = posisiValue.Split(", ")
                For Each iValue As String In arrPosisi
                    Dim iItem As ListItem = chkPosisi.Items.FindByValue(iValue.Trim)
                    iItem.Selected = True
                Next
            End If
            chkStatus.ClearSelection()
            If Not String.IsNullorEmpty(statusValue) Then
                statusValue = statusValue.Replace("(", "").Replace(")", "")
                Dim arrStatus() As String = statusValue.Split(", ")
                For Each iValue As String In arrStatus
                    Dim iItem As ListItem = chkStatus.Items.FindByValue(iValue)
                    iItem.Selected = True
                Next
            End If
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

        'Must Include Criteria For V_SalesmanDownload
        Dim CriteriaDownload As New CriteriaComposite(New Criteria(GetType(V_SalesmanDownload), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        objDealer = (CType(sessHelper.GetSession("Dealer"), Dealer))

        'Based on bug 1257 hanya muncul dealer ybs, bukan groupnya
        'If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        '    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        'End If

        If (txtDealerCode.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
            CriteriaDownload.opAnd(New Criteria(GetType(V_SalesmanDownload), "DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
        End If

        'Based on bug 1257 hanya muncul dealer ybs, bukan groupnya
        'If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
        '    If (txtDealerCode.Text.Trim <> String.Empty) Then
        '        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
        '    End If
        'Else
        '    If (txtDealerCode.Text.Trim <> String.Empty) Then
        '        If New DataOwner().IsdealerExistInGroup(txtDealerCode.Text.Trim, objDealer) Then
        '            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
        '        Else
        '            mode = 0
        '        End If
        '    Else
        '        Dim strCrit As String = New DataOwner().GenerateDealerCodeSelection(objDealer, User)
        '        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.InSet, strCrit))
        '    End If
        'End If

        If ddlUnit.SelectedItem.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, ddlUnit.SelectedValue))
            CriteriaDownload.opAnd(New Criteria(GetType(V_SalesmanDownload), "SalesIndicator", MatchType.Exact, ddlUnit.SelectedValue))
        End If

        If txtID.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesmanCode", MatchType.[Partial], txtID.Text.Trim()))
            CriteriaDownload.opAnd(New Criteria(GetType(V_SalesmanDownload), "SalesmanCode", MatchType.[Partial], txtID.Text.Trim()))
        End If

        If txtNama.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Name", MatchType.[Partial], txtNama.Text.Trim()))
            CriteriaDownload.opAnd(New Criteria(GetType(V_SalesmanDownload), "Name", MatchType.[Partial], txtNama.Text.Trim()))
        End If


        Dim posisiValue As String = chkPosisi.GetSelectedValue()
        If Not String.IsNullorEmpty(posisiValue) Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "JobPosition.ID", MatchType.InSet, posisiValue))
            CriteriaDownload.opAnd(New Criteria(GetType(V_SalesmanDownload), "JobPositionId_Main", MatchType.InSet, posisiValue))
        End If

        Dim statusValue As String = chkStatus.GetSelectedValue()
        If Not String.IsNullorEmpty(statusValue) Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.InSet, statusValue))
            CriteriaDownload.opAnd(New Criteria(GetType(V_SalesmanDownload), "Status", MatchType.InSet, statusValue))
        End If

        If ddlSalesmanLevel.SelectedValue <> "" Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesmanLevel.ID", MatchType.Exact, ddlSalesmanLevel.SelectedValue))
            CriteriaDownload.opAnd(New Criteria(GetType(V_SalesmanDownload), "SalesmanLevelID", MatchType.Exact, ddlSalesmanLevel.SelectedValue))
        End If

        If chkHireDate.Checked Then
            If icHireDateFrom.Value = #12:00:00 AM# Then
                MessageBox.Show("Periode registrasi dari harus diisi")
                Exit Sub
            End If
            If icHireDateUntil.Value = #12:00:00 AM# Then
                MessageBox.Show("Periode registrasi sampai harus diisi")
                Exit Sub
            End If
            If icHireDateFrom.Value <= icHireDateUntil.Value Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesmanHeader), "CreatedTime", MatchType.GreaterOrEqual, icHireDateFrom.Value))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesmanHeader), "CreatedTime", MatchType.LesserOrEqual, icHireDateUntil.Value.AddDays(1)))
                CriteriaDownload.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_SalesmanDownload), "CreatedTime", MatchType.GreaterOrEqual, icHireDateFrom.Value))
                CriteriaDownload.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_SalesmanDownload), "CreatedTime", MatchType.LesserOrEqual, icHireDateUntil.Value.AddDays(1)))
            Else
                MessageBox.Show("Periode registrasi sampai harus lebih besar atau sama dengan periode registrasi dari")
                Exit Sub
            End If
        End If

        If chkResignDate.Checked Then
            Dim valueStatus As String = chkStatus.GetSelectedValue()
            If valueStatus.IndexOf("3") = -1 Then
                MessageBox.Show("Status salesman harus tidak aktif")
            End If

            If icResignDateFrom.Value = #12:00:00 AM# Then
                MessageBox.Show("Periode pengunduran diri dari harus diisi")
                Exit Sub
            End If
            If icResignDateUntil.Value = #12:00:00 AM# Then
                MessageBox.Show("Periode pengunduran diri sampai harus diisi")
                Exit Sub
            End If
            If icResignDateFrom.Value <= icResignDateUntil.Value Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesmanHeader), "ResignDate", MatchType.GreaterOrEqual, icResignDateFrom.Value))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesmanHeader), "ResignDate", MatchType.LesserOrEqual, icResignDateUntil.Value.AddDays(1)))
                CriteriaDownload.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_SalesmanDownload), "ResignDate", MatchType.GreaterOrEqual, icResignDateFrom.Value))
                CriteriaDownload.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_SalesmanDownload), "ResignDate", MatchType.LesserOrEqual, icResignDateUntil.Value.AddDays(1)))
            Else
                MessageBox.Show("Periode pengunduran diri sampai harus lebih besar atau sama dengan periode pengunduran diri dari")
                Exit Sub
            End If
        End If

        If chkDateOfBirth.Checked Then
            If icDateOfBirthFrom.Value = #12:00:00 AM# Then
                MessageBox.Show("Tanggal lahir dari harus diisi")
                Exit Sub
            End If
            If icDateOfBirthUntil.Value = #12:00:00 AM# Then
                MessageBox.Show("Tanggal lahir sampai harus diisi")
                Exit Sub
            End If
            If icDateOfBirthFrom.Value <= icDateOfBirthUntil.Value Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesmanHeader), "DateOfBirth", MatchType.GreaterOrEqual, icDateOfBirthFrom.Value))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesmanHeader), "DateOfBirth", MatchType.LesserOrEqual, icDateOfBirthUntil.Value.AddDays(1)))
                CriteriaDownload.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_SalesmanDownload), "DateOfBirth", MatchType.GreaterOrEqual, icDateOfBirthFrom.Value))
                CriteriaDownload.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_SalesmanDownload), "DateOfBirth", MatchType.LesserOrEqual, icDateOfBirthUntil.Value.AddDays(1)))
            Else
                MessageBox.Show("Tanggal lahir sampai harus lebih besar atau sama dengan tanggal lahir dari")
                Exit Sub
            End If
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

        lblTotalRecord.Text = totalRow

    End Sub
    Private Sub BindDropDown()
        CommonFunction.BindFromEnum("SalesmanUnit", ddlUnit, Me.User, False, "NameStatus", "ValStatus")
        'CommonFunction.BindFromEnum("SalesmanStatus", ddlStatus, Me.User, True, "NameStatus", "ValStatus")
        CommonFunction.BindSalesmanLevel(ddlSalesmanLevel, User, True)

        CommonFunction.BindSalesmanLevel(ddlSalesLevel2, User, True)

        Dim arrStatus = New EnumSalesmanStatus().RetriveSalesmanStatus(False)
        chkStatus.BindChkList(arrStatus, "ValStatus", "NameStatus")

        If Not IsNothing(Request.QueryString("Menu")) Then
            Dim iMenu As Integer = CType(Request.QueryString("Menu"), Integer)
            If iMenu > 0 Then
                Dim arlJobPosition As ArrayList = New JobPositionFacade(User).RetriveListByMenuAssigned(iMenu)
                chkPosisi.BindChkList(arlJobPosition)
            End If
        Else
            Dim cJP As New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sJP As New SortCollection
            sJP.Add(New Sort(GetType(JobPosition), "Description", Sort.SortDirection.ASC))
            Dim arlJobPosition As ArrayList = New JobPositionFacade(User).Retrieve(cJP, sJP) '.RetrieveList()

            chkPosisi.BindChkList(arlJobPosition)
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
        arrData = New SalesmanHeaderFacade(User).RetrieveV_SalesmanDownloadNew(crits)
        If arrData.Count > 0 Then
            'DoDownload(arrData)
            If Not IsNothing(sessHelper.GetSession("strFileNm")) Then
                strFileNm = CType(sessHelper.GetSession("strFileNm"), String)
            End If
            CreateExcel(strFileNm, arrData)
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
            If imp.Start() Then

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
                imp.StopImpersonate()
                imp = Nothing
            End If

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
        Dim oDFac As New DealerFacade(User)
        Dim oD As Dealer
        Dim sDealerName As String

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
            itemLine.Append("Nama Dealer" & tab)
            itemLine.Append("Kota Dealer" & tab)
            itemLine.Append("Alamat Dealer" & tab)
            itemLine.Append("Group Dealer" & tab)
            itemLine.Append("Area Dealer" & tab)
            itemLine.Append("Cabang Dealer" & tab) 'added by anh 20130503
            itemLine.Append("Cabang Dealer Term" & tab) 'added by Benny 20190401
            itemLine.Append("Kode " & tab)
            itemLine.Append("Nama" & tab)

            ' Modified by Ikhsan, 7 AGustus 2008
            ' Requested by Rina
            ' As Part of CR, to add several Column, to improve formatted excel 
            ' Modify Start
            '------------------------------------------------------------------
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
            itemLine.Append("Kategori Tim" & tab)
            '------------------------------------------------------------------
            itemLine.Append("Kategori" & tab)
            itemLine.Append("Level" & tab)
            itemLine.Append("Superior Code" & tab)
            itemLine.Append("Superior Name" & tab)
            itemLine.Append("Status" & tab)

            ' Modify Start
            '------------------------------------------------------------------
            itemLine.Append("Tanggal Masuk" & tab)
            itemLine.Append("Area" & tab)
            '------------------------------------------------------------------
            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            For Each item As V_SalesmanDownload In data
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(i.ToString & tab)
                itemLine.Append(item.DealerCode & tab)
                oD = oDFac.Retrieve(item.DealerCode)
                If Not IsNothing(oD) AndAlso oD.ID > 0 Then
                    itemLine.Append(oD.DealerName & tab)
                    itemLine.Append(oD.City.CityName & tab)  'Kota
                    itemLine.Append(oD.Address & tab)  'Alamat
                    itemLine.Append(oD.DealerGroup.GroupName & tab)  'Grup
                    If Not IsNothing(oD.Area1) Then
                        itemLine.Append(oD.Area1.Description & tab)     'Area
                    Else
                        itemLine.Append("" & tab)     'Area
                    End If
                Else
                    itemLine.Append("" & tab)
                    itemLine.Append("" & tab) 'Kota
                    itemLine.Append("" & tab) 'Alamat
                    itemLine.Append("" & tab) 'Grup
                    itemLine.Append("" & tab) 'Area
                End If
                itemLine.Append(item.DealerBranchName & tab) 'added by anh 20130503
                itemLine.Append(item.Term1 & tab) 'added by Benny 20190401
                itemLine.Append(item.SalesmanCode & tab)
                itemLine.Append(item.Name & tab)

                'Modified Part
                '------------------------------------------------------------------
                itemLine.Append(item.PlaceOfBirth.ToString & tab)
                itemLine.Append(Format(item.DateOfBirth, "dd/MM/yyyy").ToString & tab)
                'If Format(item.DateOfBirth, "hh:mm:ss") = "00:00:00" Then
                '    itemLine.Append(Format(item.DateOfBirth, "dd/MMM/yyyy") & tab)
                'Else
                '    itemLine.Append(item.DateOfBirth.ToString & tab)
                'End If
                itemLine.Append(IIf(item.Gender = EnumGender.Gender.Pria, "Pria", "Wanita") & tab)
                Dim sMarriedStatus As String = ""
                sMarriedStatus = CommonFunction.GetEnumDescription(item.MarriedStatus, "EnumSalesmanMarriedStatus.MarriedStatus")
                'If item.MarriedStatus = EnumSalesmanMarriedStatus.MarriedStatus.Belum_Menikah Then
                '    sMarriedStatus = "Belum Menikah"
                'ElseIf item.MarriedStatus = EnumSalesmanMarriedStatus.MarriedStatus.Menikah Then
                '    sMarriedStatus = "Menikah"
                'ElseIf item.MarriedStatus = EnumSalesmanMarriedStatus.MarriedStatus.Janda Then
                '    sMarriedStatus = "Janda"
                'ElseIf item.MarriedStatus = EnumSalesmanMarriedStatus.MarriedStatus.Duda Then
                '    sMarriedStatus = "Duda"
                'End If
                itemLine.Append(sMarriedStatus & tab)
                If Not IsNothing(item.Address) Then
                    Dim Address1 As String = item.Address.Replace(LF, "")
                    Dim Address2 As String = Address1.Replace(CR, " ")

                    'Address.Replace("*", " ")
                    itemLine.Append(Address2.Trim & tab)
                Else
                    itemLine.Append("" & tab)
                End If
                itemLine.Append(item.ProvinceName & tab)
                itemLine.Append(item.City & tab)

                'Dim ObjSalesmanProfile As ArrayList = SlmProfile(item.ID, "PENDIDIKAN")

                'If Not ObjSalesmanProfile Is Nothing Then
                '    Dim SalesmanProfile As SalesmanProfile = ObjSalesmanProfile.Item(0)
                '    itemLine.Append(SalesmanProfile.ProfileValue & tab)
                'Else
                '    itemLine.Append("" & tab)
                'End If

                itemLine.Append(item.PENDIDIKAN & tab)

                'ObjSalesmanProfile = SlmProfile(item.ID, "EMAIL")

                'If Not ObjSalesmanProfile Is Nothing Then
                '    Dim SalesmanProfile As SalesmanProfile = ObjSalesmanProfile.Item(0)
                '    itemLine.Append(SalesmanProfile.ProfileValue & tab)
                'Else
                '    itemLine.Append("" & tab)
                'End If

                itemLine.Append(item.EMAIL & tab)


                'ObjSalesmanProfile = SlmProfile(item.ID, "NO_HP")

                'If Not ObjSalesmanProfile Is Nothing Then
                '    Dim SalesmanProfile As SalesmanProfile = ObjSalesmanProfile.Item(0)
                '    itemLine.Append(SalesmanProfile.ProfileValue & tab)
                'Else
                '    itemLine.Append("" & tab)
                'End If

                itemLine.Append(item.NO_HP & tab)

                'ObjSalesmanProfile = SlmProfile(item.ID, "NOKTP")

                'If Not ObjSalesmanProfile Is Nothing Then
                '    Dim SalesmanProfile As SalesmanProfile = ObjSalesmanProfile.Item(0)
                '    itemLine.Append(SalesmanProfile.ProfileValue & tab)
                'Else
                '    itemLine.Append("" & tab)
                'End If
                itemLine.Append(item.NOKTP & tab)
                itemLine.Append(item.KATEGORI_TIM & tab)
                '------------------------------------------------------------------

                itemLine.Append(item.JobDescription & tab)
                'If Not IsNothing(item.SalesmanLevel) Then
                '    If item.SalesmanLevel.Description <> "" Or Not IsNothing(item.SalesmanLevel.Description) Then
                '        itemLine.Append(item.SalesmanLevel.Description & tab)
                '    Else
                '        itemLine.Append("" & tab)
                '    End If
                'Else
                '    itemLine.Append("" & tab)
                'End If
                itemLine.Append(item.LevelDescription & tab)

                'If item.LeaderId = 0 Then
                '    itemLine.Append("" & tab)
                'Else
                '    itemLine.Append(objLeader.SalesmanCode & " " & objLeader.Name & tab)
                'End If

                itemLine.Append(item.LeaderCode & tab)
                itemLine.Append(item.LeaderName & tab)

                ' Start Modifying-------------------
                itemLine.Append(CType(item.Status, EnumSalesmanStatus.SalesmanStatus).ToString.Replace("_", " ") & tab)
                ' End Modifying---------------------
                '---------------------------------------------------------------------
                If Not IsNothing(item.HireDate) Then
                    itemLine.Append(item.HireDate & tab)
                Else
                    itemLine.Append("" & tab)
                End If

                'If Not IsNothing(item.SalesmanArea) Then
                '    itemLine.Append(item.SalesmanArea.AreaDesc & tab)
                'Else
                '    itemLine.Append("" & tab)
                'End If

                itemLine.Append(item.AreaDesc & tab)

                ' Modify Start
                '---------------------------------------------------------------------
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
            ViewState("CurrentSortColumn") = "Dealer.DealerCode"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            ReadCriteria()
            SetSetting()
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            CreateCriteria(criterias)
            If Not IsNothing(sessHelper.GetSession("idxPage")) Then
                BindDataGrid(sessHelper.GetSession("idxPage"))
            Else
                BindDataGrid(0)
            End If

            SetDateFilterVisibility()

        End If
        btnDownloadExcel.Enabled = bCekDLPriv
    End Sub

    Private Sub SetDateFilterVisibility()
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            chkHireDate.Visible = True
            icHireDateFrom.Visible = True
            icHireDateUntil.Visible = True
            chkResignDate.Visible = True
            icResignDateFrom.Visible = True
            icResignDateUntil.Visible = True
            chkDateOfBirth.Visible = False
            icDateOfBirthFrom.Visible = False
            icDateOfBirthUntil.Visible = False
            lblColonDOB.Visible = False
            lblColonResignDate.Visible = True
            lblColonHireDate.Visible = True
            lblUntilHireDate.Visible = True
            lblUntilResignDate.Visible = True
            lblUntilDOB.Visible = False

        Else
            chkHireDate.Visible = False
            icHireDateFrom.Visible = False
            icHireDateUntil.Visible = False
            chkResignDate.Visible = False
            icResignDateFrom.Visible = False
            icResignDateUntil.Visible = False
            chkDateOfBirth.Visible = False
            icDateOfBirthFrom.Visible = False
            icDateOfBirthUntil.Visible = False
            lblColonDOB.Visible = False
            lblColonResignDate.Visible = False
            lblColonHireDate.Visible = False
            lblUntilHireDate.Visible = False
            lblUntilResignDate.Visible = False
            lblUntilDOB.Visible = False
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        SaveCriteria()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(criterias)
        dgSalesmanHeader.CurrentPageIndex = 0
        BindDataGrid(0)
    End Sub
    Private Sub dgSalesmanHeader_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesmanHeader.ItemCommand
        Dim strMode As String
        If Not IsNothing(Request.QueryString("Mode")) Then
            strMode = Request.QueryString("Mode")
        Else
            strMode = "unit"
        End If

        If (e.CommandName = "View") Then
            Response.Redirect("FrmSalesmanHeader.aspx?ID=" + e.Item.Cells(0).Text + "&Mode=" + strMode)
        ElseIf e.CommandName = "Edit" Then
            Response.Redirect("FrmSalesmanHeader.aspx?ID=" + e.Item.Cells(0).Text + "&edit=true" + "&Mode=" + strMode)
        ElseIf e.CommandName = "Delete" Then
            Delete(e.Item.Cells(0).Text)
        End If
    End Sub
    Private Sub dgSalesmanHeader_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSalesmanHeader.ItemDataBound

        If Not e.Item.DataItem Is Nothing Then
            Dim objSalesmanHeader As SalesmanHeader = e.Item.DataItem
            e.Item.Cells(2).Text = e.Item.ItemIndex + 1 + (dgSalesmanHeader.CurrentPageIndex * dgSalesmanHeader.PageSize)

            Dim lblKodeDealerNew As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)
            lblKodeDealerNew.Text = objSalesmanHeader.Dealer.DealerCode

            Dim lblJobPositionId_MainNew As Label = CType(e.Item.FindControl("lblJobPositionId_Main"), Label)
            lblJobPositionId_MainNew.Text = objSalesmanHeader.JobPosition.Description

            Dim lblStatusNew As Label = CType(e.Item.FindControl("lblStatus"), Label)
            lblStatusNew.Text = CType(objSalesmanHeader.Status, EnumSalesmanStatus.SalesmanStatus).ToString.Replace("_", " ")

            Dim lblCategoryTeam As Label = CType(e.Item.FindControl("lblCategoryTeam"), Label)

            Dim lblKodeAtasan As Label = CType(e.Item.FindControl("lblKodeAtasan"), Label)
            lblKodeAtasan.Text = "-"

            Dim lblNamaAtasan As Label = CType(e.Item.FindControl("lblNamaAtasan"), Label)
            lblNamaAtasan.Text = "-"

            Dim lblPosisiAtasan As Label = CType(e.Item.FindControl("lblPosisiAtasan"), Label)
            lblPosisiAtasan.Text = "-"

            If objSalesmanHeader.LeaderId > 0 Then
                Dim atasan As SalesmanHeader = New SalesmanHeaderFacade(Me.User).Retrieve(objSalesmanHeader.LeaderId)

                lblKodeAtasan.Text = atasan.SalesmanCode
                lblNamaAtasan.Text = atasan.Name
                lblPosisiAtasan.Text = atasan.JobPosition.Description
            End If



            'Dim lblResignDateNew As Label = CType(e.Item.FindControl("lblResignDate"), Label)
            ' Modified by Ikhsan, 8 Agustus 2008
            ' Requested by Rina, as Part of CR
            ' Change ResignDate to DateOfBirth
            '---------------------------------------------------------------------------------
            'lblResignDateNew.Text = ""
            'If objSalesmanHeader.ResignDate > New Date(1900, 1, 1) Then
            '    lblResignDateNew.Text = objSalesmanHeader.ResignDate.ToString("dd/MM/yyyy")
            'End If
            'lblResignDateNew.Text = objSalesmanHeader.DateOfBirth.ToString("dd/MM/yyyy")

            ' Commented by Ikhsan, 8 AGustus 2008
            ' yg aktif, dibuatkan disable labelnya - belum resign, related bug 695
            'If objSalesmanHeader.ResignDate = Date.Parse(strDefDate) Then
            '    lblResignDateNew.Enabled = False
            'Else
            '    lblResignDateNew.Enabled = True
            'End If
            '---------------------------------------------------------------------------------
            'Remarks by anh 20111011 
            'If Request.QueryString("Mode").ToString = "unit" And objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If Request.QueryString("Mode").ToString = "unit" Then
                e.Item.FindControl("lbtnEdit").Visible = False
                e.Item.Cells(11).Visible = True
                For Each _salesmanProfile As SalesmanProfile In objSalesmanHeader.SalesmanProfiles
                    If _salesmanProfile.ProfileHeader.ID = 45 Then
                        lblCategoryTeam.Text = _salesmanProfile.ProfileValue
                    End If
                Next
            Else
                e.Item.Cells(11).Visible = False
            End If
            ' end rmearks 

            'lbtnEdit
            '09-Nov-2007    Deddy H     Fix bug 1386    Dealer tdk bs delete data
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                e.Item.FindControl("lbtnDelete").Visible = False
            End If

            ' case Dealer saja yang bisa mengedit, refer bug 1393
            'remarks by anh 20111011 -> refer to statement above, ktb also has authority to edit 
            If objSalesmanHeader.Status = CType(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif, String) Then
                'If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                e.Item.FindControl("lbtnEdit").Visible = False
                'End If
            Else
                'If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                e.Item.FindControl("lbtnEdit").Visible = bCheckEditDetailPrivilege
                'End If
            End If

            'If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If DateTime.op_LessThanOrEqual(objSalesmanHeader.ResignDate, New DateTime(1900, 1, 1)) Then
                e.Item.FindControl("lbtnEdit").Visible = True
            Else
                e.Item.FindControl("lbtnEdit").Visible = False
            End If
            'End If
            'end remarks by anh

            e.Item.FindControl("lbtnView").Visible = bCekViewDetailsPriv
        End If

        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If
        Dim oD = (CType(sessHelper.GetSession("Dealer"), Dealer))
        'If oD.Title = CType(EnumDealerTittle.DealerTittle.DEALER, Short) Then
        '    Dim dtStart As DateTime = New DateTime(2013, 5, 1)
        '    Dim dtEnd As DateTime = New DateTime(2013, 6, 1)
        '    Dim dtNow As DateTime = Now
        '    Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
        '    Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)

        '    If dtNow >= dtStart AndAlso dtNow < dtEnd Then
        '        If IsNothing(lbtnEdit) = False Then lbtnEdit.Visible = False
        '        If IsNothing(lbtnDelete) = False Then lbtnDelete.Visible = False
        '    End If
        'End If
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

    Protected Sub btnChange_Click(sender As Object, e As EventArgs) Handles btnChange.Click
        PopulateDeleteConfirm()
    End Sub

    Private Sub PopulateDeleteConfirm()
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim objSHFacade As New SalesmanHeaderFacade(User)
        Dim Succes As Boolean = False
        Dim Deleted As Boolean = False
        Dim SL As Integer = 0


        If ddlSalesLevel2.SelectedValue.ToString() = "" Then

            MessageBox.Show(SR.DataNotChooseYet("Level Sales"))
            Return
        End If
        SL = CInt(ddlSalesLevel2.SelectedValue.ToString())
        Dim objSL As SalesmanLevel = New SalesmanLevel(CInt(ddlSalesLevel2.SelectedValue))

        For Each oDataGridItem In Me.dgSalesmanHeader.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then

                Dim lblID As Label = CType(oDataGridItem.FindControl("lblID"), Label)
                Dim varID As Integer = CInt(lblID.Text)
                Dim _SH As New KTB.DNet.Domain.SalesmanHeader

                _SH.ID = varID
                _SH = objSHFacade.Retrieve(_SH.ID)



                If 1 = 1 Then
                    Deleted = True
                    Try
                        _SH.SalesmanLevel = objSL

                        objSHFacade.Update(_SH)
                        Succes = True
                    Catch ex As Exception
                        Succes = False
                    End Try

                Else
                    Succes = False
                    btnCari_Click(Me, Nothing)
                    Return
                End If




            End If
        Next


        If Succes Then
            btnCari_Click(Me, Nothing)
            MessageBox.Show("Data  berhasil diubah")

        End If

        If Deleted = False Then
            MessageBox.Show("Tidak ada  data yang diubah")

        End If
    End Sub

    Protected Sub dgSalesmanHeader_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub chkResignDate_CheckedChanged(sender As Object, e As EventArgs) Handles chkResignDate.CheckedChanged
        Try

            If chkResignDate.Checked Then
                chkStatus.ClearSelection()
                chkStatus.Items.FindByValue(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif).Selected = True
                chkStatus.Enabled = False
            Else
                chkStatus.Enabled = True
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim oD As Dealer
        Dim oDFac As New DealerFacade(User)
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Dim sgFac As New SalesmanGradeFacade(User)
        Dim gradeDictionary As Dictionary(Of String, String) = GetDictionaryGrade()
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = FileName
            ws.Cells("A3").Value = "No"
            ws.Cells("B3").Value = "Kode Dealer"
            ws.Cells("C3").Value = "Nama Dealer"
            ws.Cells("D3").Value = "Kota Dealer"
            ws.Cells("E3").Value = "Alamat Dealer"
            ws.Cells("F3").Value = "Group Dealer"
            ws.Cells("G3").Value = "Area Dealer"
            ws.Cells("H3").Value = "Cabang Dealer"
            ws.Cells("I3").Value = "Cabang Dealer Term"   'added by Benny 20190401
            ws.Cells("J3").Value = "Kode"
            ws.Cells("K3").Value = "Nama"
            ws.Cells("L3").Value = "Tempat Lahir"
            ws.Cells("M3").Value = "Tanggal Lahir"
            ws.Cells("N3").Value = "Jenis Kelamin"
            ws.Cells("O3").Value = "Status Perkawinan"
            ws.Cells("P3").Value = "Alamat"
            ws.Cells("Q3").Value = "Propinsi"
            ws.Cells("R3").Value = "Kota"
            ws.Cells("S3").Value = "Pendidikan"
            ws.Cells("T3").Value = "E-Mail"
            ws.Cells("U3").Value = "No HP"
            ws.Cells("V3").Value = "No KTP"
            ws.Cells("W3").Value = "Kategori Tim"
            ws.Cells("X3").Value = "Kategori"
            ws.Cells("Y3").Value = "Level"
            ws.Cells("Z3").Value = "Superior Code"
            ws.Cells("AA3").Value = "Superior Name"
            ws.Cells("AB3").Value = "Status"
            ws.Cells("AC3").Value = "Tanggal Masuk"
            ws.Cells("AD3").Value = "Area"
            ws.Cells("AE3").Value = "Grade Q4 Tahun Lalu"
            ws.Cells("AF3").Value = "Productivity Tahun Lalu"
            ws.Cells("AG3").Value = "Grade Saat Ini"
            ws.Cells("AH3").Value = "Productivity Saat Ini"

            For i As Integer = 0 To Data.Count - 1
                Dim item As V_SalesmanDownload = Data(i)
                ws.Cells(i + 4, 1).Value = i + 1
                ws.Cells(i + 4, 2).Value = item.DealerCode
                ws.Cells(i + 4, 3).Value = item.DealerName
                ws.Cells(i + 4, 4).Value = item.CityName
                ws.Cells(i + 4, 5).Value = item.DealerAddress
                ws.Cells(i + 4, 6).Value = item.GroupName
                ws.Cells(i + 4, 7).Value = item.Area1Description
                'oD = oDFac.Retrieve(item.DealerCode)
                'If Not IsNothing(oD) AndAlso oD.ID > 0 Then
                '    ws.Cells(i + 4, 3).Value = oD.DealerName
                '    ws.Cells(i + 4, 4).Value = oD.City.CityName
                '    ws.Cells(i + 4, 5).Value = oD.Address
                '    ws.Cells(i + 4, 6).Value = oD.DealerGroup.GroupName
                '    If Not IsNothing(oD.Area1) Then
                '        ws.Cells(i + 4, 7).Value = oD.Area1.Description
                '    Else
                '        ws.Cells(i + 4, 7).Value = ""
                '    End If
                'Else
                '    ws.Cells(i + 4, 3).Value = ""
                '    ws.Cells(i + 4, 4).Value = ""
                '    ws.Cells(i + 4, 5).Value = ""
                '    ws.Cells(i + 4, 6).Value = ""
                '    ws.Cells(i + 4, 7).Value = ""
                'End If
                ws.Cells(i + 4, 8).Value = item.DealerBranchName
                ws.Cells(i + 4, 9).Value = item.Term1  'added by Benny 20190401
                ws.Cells(i + 4, 10).Value = item.SalesmanCode
                ws.Cells(i + 4, 11).Value = item.Name
                ws.Cells(i + 4, 12).Value = item.PlaceOfBirth.ToString
                ws.Cells(i + 4, 13).Value = Format(item.DateOfBirth, "dd/MM/yyyy").ToString
                ws.Cells(i + 4, 14).Value = item.GenderDescription
                ws.Cells(i + 4, 15).Value = item.MarriedStatusDesc
                'ws.Cells(i + 4, 13).Value = IIf(item.Gender = EnumGender.Gender.Pria, "Pria", "Wanita")
                'ws.Cells(i + 4, 14).Value = CommonFunction.GetEnumDescription(item.MarriedStatus, "EnumSalesmanMarriedStatus.MarriedStatus")
                If Not IsNothing(item.Address) Then
                    Dim Address1 As String = item.Address.Replace(LF, "")
                    Dim Address2 As String = Address1.Replace(CR, " ")

                    ws.Cells(i + 4, 16).Value = Address2.Trim
                Else
                    ws.Cells(i + 4, 16).Value = ""
                End If

                ws.Cells(i + 4, 17).Value = item.ProvinceName
                ws.Cells(i + 4, 18).Value = item.City
                ws.Cells(i + 4, 19).Value = item.PENDIDIKAN
                ws.Cells(i + 4, 20).Value = item.EMAIL
                ws.Cells(i + 4, 21).Value = item.NO_HP
                ws.Cells(i + 4, 22).Value = item.NOKTP
                ws.Cells(i + 4, 23).Value = item.KATEGORI_TIM
                ws.Cells(i + 4, 24).Value = item.JobDescription
                ws.Cells(i + 4, 25).Value = item.LevelDescription
                ws.Cells(i + 4, 26).Value = item.LeaderCode
                ws.Cells(i + 4, 27).Value = item.LeaderName
                ws.Cells(i + 4, 28).Value = CType(item.Status, EnumSalesmanStatus.SalesmanStatus).ToString.Replace("_", " ")
                If Not IsNothing(item.HireDate) Then
                    ws.Cells(i + 4, 29).Value = Format(item.HireDate, "dd/MM/yyyy").ToString
                Else
                    ws.Cells(i + 4, 29).Value = ""
                End If

                ws.Cells(i + 4, 30).Value = item.AreaDesc
                Try
                    ws.Cells(i + 4, 31).Value = item.YearLastGrade 'gradeDictionary.Item(sgFac.LastYearGrade(item.SalesmanCode).Grade.ToString())
                    ws.Cells(i + 4, 32).Value = item.YearLastScore
                Catch ex As Exception
                    ws.Cells(i + 4, 31).Value = ""
                    ws.Cells(i + 4, 32).Value = ""
                End Try

                Try
                    ws.Cells(i + 4, 33).Value = item.LastGrade 'gradeDictionary.Item(sgFac.LastGrade(item.SalesmanCode).Grade.ToString())
                    ws.Cells(i + 4, 34).Value = item.LastScore
                Catch ex As Exception
                    ws.Cells(i + 4, 33).Value = ""
                    ws.Cells(i + 4, 34).Value = ""
                End Try


            Next

            CreateExcelFile(pck, FileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xls")
        End Using

    End Sub

    Private Function GetDictionaryGrade() As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.StandardCode), "Category", MatchType.Exact, "SalesmanGrade"))

        Dim arlGrade As ArrayList = New StandardCodeFacade(User).Retrieve(criterias)

        If arlGrade.Count > 0 Then

            For Each grade As StandardCode In arlGrade
                result.Add(grade.ValueId.ToString(), grade.ValueDesc)
            Next

        End If

        Return result
    End Function


    Private Function CreateSheet(pck As ExcelPackage, sheetName As String) As ExcelWorksheet
        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add(sheetName)
        ws.View.ShowGridLines = False
        Return ws
    End Function

    Private Sub CreateExcelFile(pck As ExcelPackage, fileName As String)
        Dim fileBytes = pck.GetAsByteArray()

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Dim success As Boolean = False

        Try
            success = imp.Start()
            If success Then
                File.WriteAllBytes(Server.MapPath("~/DataTemp/" & fileName), fileBytes)
                imp.StopImpersonate()
            End If

        Catch ex As Exception
            Exit Sub

        End Try

        Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & fileName)


    End Sub

End Class
