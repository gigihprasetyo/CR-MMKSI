#Region " Summary "
'--------------------------------------------------'
'-- Program Code : FrmFSChassisCampaign.aspx            --'
'-- Program Name : Daftar Parameter Free Service --'
'-- Description  :                               --'
'--------------------------------------------------'
'-- Programmer   : Anna Nurhayanto               --'
'-- Start Date   : Aug 27, 2010                  --'
'-- Update By    :                               --'
'-- Last Update  : Aug 27, 2010                  --'
'--------------------------------------------------'

#End Region

#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
Imports Excel
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports System.Text.RegularExpressions
Imports KTB.DNet.BusinessFacade.Profile

#End Region

Public Class frmFSChassisCampaign
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnDnLoad As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgFSChassis As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtNoRangka As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMessage As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKeterangan As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlIsAllow As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lboxFSType As System.Web.UI.WebControls.ListBox
    Protected WithEvents FileText As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents hdnMsg As HiddenField
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Private Variables"
    Private sessionHelper As New SessionHelper
    Private idxPage As Integer
    Dim _sesUpload As String = "frmFSChassisCampaign.upload"
    Dim _sesData As String = "_frmSparepartMaster.download"

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Page.Server.ScriptTimeout = 300
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ActivateCampaignPrivilege()
            ViewState("ProsesAwal") = True
            Initialize()
        End If
    End Sub

    Private Sub ActivateCampaignPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.service_chassis_campaign_lihat_privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar Chassis Campaign")
        End If
        Dim IsAuthorizedSave As Boolean = SecurityProvider.Authorize(Context.User, SR.service_chassis_campaign_simpan_privilege)
        btnSave.Enabled = IsAuthorizedSave
    End Sub

    Private Sub Initialize()
        ClearData()
        ViewState("currSortColumn") = "FSKind.ID"
        ViewState("currSortDirection") = Sort.SortDirection.ASC

        BindDataGrid(1)
    End Sub

    Private Sub ClearData()
        ddlIsAllow.SelectedValue = "-"
        txtNoRangka.Text = String.Empty
        txtKeterangan.Text = String.Empty
        dtgFSChassis.DataSource = Nothing
        dtgFSChassis.DataBind()
        BindLbFSType()
    End Sub

    Private Sub BindLbFSType()
        lboxFSType.Items.Clear()
        Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(FSKind), "KM", Sort.SortDirection.ASC))
        Dim objFSKind As ArrayList = New FSKindFacade(User).Retrieve(critCol, sortCol)

        Dim li As ListItem
        For Each oneFSKind As FSKind In objFSKind
            li = New ListItem(oneFSKind.KindCode & " - " & oneFSKind.KindDescription, oneFSKind.ID.ToString)
            lboxFSType.Items.Add(li)
        Next
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim IsUpload As Boolean = False
            If ViewState("vsUpload") = "InsertUpload" Then
                If Not IsNothing(sessionHelper.GetSession(_sesUpload)) AndAlso CType(sessionHelper.GetSession(_sesUpload), ArrayList).Count > 0 Then
                    IsUpload = True
                End If

            End If
            If (lboxFSType.SelectedValue.Length > 0) OrElse IsUpload Then
                If (txtNoRangka.Text.Trim.Length > 0) OrElse IsUpload Then
                    If (ddlIsAllow.SelectedValue <> "-") OrElse IsUpload Then
                        SaveData(ViewState("vsProcess"))
                    Else
                        hdnMsg.Value = "Status harus diisi"
                    End If
                Else
                    hdnMsg.Value = "No Rangka harus diisi"
                End If
            Else
                hdnMsg.Value = "Jenis service harus diisi"
            End If
            ClearData()
            ViewState("vsProcess") = "Search"
            dtgFSChassis.CurrentPageIndex = 0
            dtgFSChassis.SelectedIndex = -1
            BindDataGrid(dtgFSChassis.CurrentPageIndex)
        Catch ex As Exception
            hdnMsg.Value = ex.Message & SR.SaveFail
        End Try
    End Sub

    Private Sub SaveData(ByVal vsProses As String)
        Dim nResult As Integer = -1
        Dim Ndata As Integer = 0
        Dim NSucces As Integer = 0
        Try
            Dim ObjFSChassisCampaignFacade As FSChassisCampaignFacade
            Dim objChassisMaster As ChassisMaster = New ChassisMaster
            Dim FSKindCode As String = String.Empty


            If ViewState("vsUpload") = "InsertUpload" Then
                Ndata = CType(sessionHelper.GetSession(_sesUpload), ArrayList).Count()
                For Each ObjFSChassisCampaign As FSChassisCampaign In sessionHelper.GetSession(_sesUpload)
                    'cek row if exist in FSChassisCampaign -> update else insert
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSChassisCampaign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSChassisCampaign), "ChassisMaster.ID", MatchType.Exact, ObjFSChassisCampaign.ChassisMaster.ID))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSChassisCampaign), "FSKind.ID", MatchType.Exact, ObjFSChassisCampaign.FSKind.ID))

                    Dim data As ArrayList = New ArrayList
                    data = New FSChassisCampaignFacade(User).Retrieve(criterias)
                    ObjFSChassisCampaignFacade = New FSChassisCampaignFacade(User)
                    Try
                        If Not IsNothing(ObjFSChassisCampaign.ErrorMessage) AndAlso _
                            (ObjFSChassisCampaign.ErrorMessage.ToString() = "" OrElse ObjFSChassisCampaign.ErrorMessage.ToString().ToUpper().Equals("OK")) Then

                            If data.Count > 0 Then
                                ObjFSChassisCampaign = CType(data(0), FSChassisCampaign)
                                ObjFSChassisCampaign.IsAllow = CType(data(0), FSChassisCampaign).IsAllow
                                nResult = ObjFSChassisCampaignFacade.Update(ObjFSChassisCampaign)
                            Else
                                nResult = ObjFSChassisCampaignFacade.Insert(ObjFSChassisCampaign)
                                FSKindCode = Regex.Replace(ObjFSChassisCampaign.FSKind.KindCode, "[^A-Za-z]", "")
                                If Not String.IsNullOrEmpty(FSKindCode) Then
                                    InsertDataMSP(ObjFSChassisCampaign.ChassisMaster, FSKindCode)
                                End If
                            End If
                            NSucces = NSucces + 1
                        End If
                    Catch ex2 As Exception
                        Dim dd = ex2.Message
                    End Try


                Next
                ViewState("vsUpload") = ""
            Else
                If lboxFSType.SelectedValue.Length >= 1 And txtNoRangka.Text.Trim <> String.Empty Then
                    Dim objFsChassisCampaign As FSChassisCampaign = New FSChassisCampaign
                    objFsChassisCampaign.Remarks = txtKeterangan.Text.Trim
                    objFsChassisCampaign.IsAllow = ddlIsAllow.SelectedValue

                    'fskind
                    Dim strFSType As String
                    For Each item As ListItem In lboxFSType.Items
                        If item.Selected Then
                            If strFSType <> String.Empty Then
                                strFSType &= ","
                            End If
                            strFSType &= CType(item.Value, String)
                        End If
                    Next
                    Dim cek_insert As Boolean = True
                    For Each fskind_data As String In strFSType.Split(",")
                        Dim dataFSKind As ArrayList = New ArrayList
                        Dim criteriaFSKind As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteriaFSKind.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKind), "ID", MatchType.Exact, fskind_data))
                        dataFSKind = New FSKindFacade(User).Retrieve(criteriaFSKind)

                        Dim fskindid As String
                        objFsChassisCampaign.FSKind = CType(dataFSKind(0), FSKind)

                        For Each oFSKind As FSKind In dataFSKind
                            fskindid = oFSKind.ID
                        Next

                        'chassis
                        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
                        Dim arlChassisList As ArrayList = New ArrayList
                        Dim oChassisFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
                        Dim criteriasChassis As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteriasChassis.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, txtNoRangka.Text.Trim))
                        criteriasChassis.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "Category.ProductCategory.Code", MatchType.Exact, companyCode))

                        arlChassisList = oChassisFacade.Retrieve(criteriasChassis)

                        If arlChassisList.Count = 1 Then
                            objFsChassisCampaign.ChassisMaster = CType(arlChassisList(0), ChassisMaster)
                            Dim NoRangka As String = String.Empty

                            For Each oChassisMaster As ChassisMaster In arlChassisList
                                NoRangka = oChassisMaster.ID
                                objChassisMaster = oChassisMaster
                            Next

                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSChassisCampaign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSChassisCampaign), "ChassisMaster.ID", MatchType.Exact, NoRangka))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSChassisCampaign), "FSKind.ID", MatchType.Exact, fskindid))

                            Dim data As ArrayList = New ArrayList
                            data = New FSChassisCampaignFacade(User).Retrieve(criterias)
                            ObjFSChassisCampaignFacade = New FSChassisCampaignFacade(User)

                            If data.Count > 0 Then
                                objFsChassisCampaign = CType(data(0), FSChassisCampaign)
                                objFsChassisCampaign.IsAllow = ddlIsAllow.SelectedValue
                                If (txtKeterangan.Text.Trim.Length > 0) Then
                                    objFsChassisCampaign.Remarks = txtKeterangan.Text
                                End If
                                nResult = ObjFSChassisCampaignFacade.Update(objFsChassisCampaign)
                            Else
                                nResult = ObjFSChassisCampaignFacade.Insert(objFsChassisCampaign)

                                FSKindCode = Regex.Replace(objFsChassisCampaign.FSKind.KindCode, "[^A-Za-z]", "")
                                If Not String.IsNullOrEmpty(FSKindCode) Then
                                    InsertDataMSP(objChassisMaster, FSKindCode)
                                End If
                            End If
                        Else
                            hdnMsg.Value = "Chassis '" & txtNoRangka.Text.Trim & "' tidak terdaftar"
                            Exit For
                        End If
                    Next
                End If
            End If





        Catch ex As Exception
            hdnMsg.Value = ex.Message

        Finally
            If Ndata > 0 Then
                hdnMsg.Value = String.Format("Simpan data {0} dari {1} Data berhasil ", NSucces, Ndata)
            Else
                If nResult <= 0 Then
                    hdnMsg.Value = SR.SaveFail
                Else
                    hdnMsg.Value = SR.SaveSuccess
                End If
            End If

        End Try

    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        Page_Load(sender, e)
        ActivateCampaignPrivilege()
        ClearData()
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        ViewState("vsProcess") = "Search"
        ViewState("vsUpload") = ""

        dtgFSChassis.CurrentPageIndex = 0
        BindDataGrid(dtgFSChassis.CurrentPageIndex)
        ViewState("vsProcess") = "Edit"
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        idxPage = indexPage 'ini buat simpen session pas balik lagi ke halaman ini
        Dim data As ArrayList = New ArrayList
        Dim dataDownload As ArrayList = New ArrayList
        Dim totalRow As Integer = 0
        Try
            If ViewState("vsUpload") = "InsertUpload" Then
                Dim i As Integer
                For i = (indexPage - 1) * dtgFSChassis.PageSize To ((indexPage - 1) * dtgFSChassis.PageSize) + (dtgFSChassis.PageSize - 1)
                    If i = CType(Session.Item(_sesUpload), ArrayList).Count Then
                        Exit For
                    End If
                    data.Add(CType(Session.Item(_sesUpload), ArrayList).Item(i))
                Next
                totalRow = CType(Session.Item(_sesUpload), ArrayList).Count

            ElseIf ViewState("vsProcess") = "Search" Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSChassisCampaign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'FSKind
                Dim strFSType As String = String.Empty
                If lboxFSType.SelectedValue.Length > 0 Then
                    For Each item As ListItem In lboxFSType.Items
                        If item.Selected Then
                            strFSType &= "'" & CType(item.Value, String) & "',"
                        End If
                    Next
                    If strFSType <> String.Empty Then
                        strFSType = strFSType.Substring(0, strFSType.Length - 1)
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSChassisCampaign), "FSKind.ID", MatchType.InSet, "(" & strFSType & ")"))
                    End If
                End If

                'Chassis Number
                If txtNoRangka.Text.Trim <> String.Empty Then
                    Dim arlChassisList As ArrayList = New ArrayList
                    Dim oChassisFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
                    Dim criteriasChassis As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criteriasChassis.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Partial, txtNoRangka.Text.Trim))

                    arlChassisList = oChassisFacade.Retrieve(criteriasChassis)
                    Dim NoRangka As String = String.Empty

                    If arlChassisList.Count > 0 Then
                        Dim icountList As Integer = 0
                        For Each oChassisMaster As ChassisMaster In arlChassisList
                            icountList = icountList + 1
                            If icountList <= arlChassisList.Count Then
                                NoRangka &= "'" & oChassisMaster.ID & "',"
                            End If
                        Next
                        NoRangka = NoRangka.Substring(0, NoRangka.Length - 1)
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSChassisCampaign), "ChassisMaster.ID", MatchType.InSet, "(" & NoRangka & ")"))
                    Else
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSChassisCampaign), "ChassisMaster.ID", MatchType.InSet, "(" & NoRangka & ")"))
                    End If
                End If

                'keterangan
                If txtKeterangan.Text.Trim.Length > 0 Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSChassisCampaign), "Remarks", MatchType.Partial, txtKeterangan.Text.Trim))
                End If

                'status
                If (ddlIsAllow.SelectedValue <> "-") Then
                    Dim statIsAllow As Integer = 0
                    If ddlIsAllow.SelectedValue = "True" Then
                        statIsAllow = 1
                    End If

                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSChassisCampaign), "IsAllow", MatchType.Exact, statIsAllow))
                End If

                If (Session.Item("SortColumn") <> Nothing) Then
                    Dim sortColl As SortCollection = New SortCollection
                    sortColl.Add(New Sort(GetType(FSChassisCampaign), Session.Item("SortColumn"), Session.Item("SortDirection")))
                    data = New FSChassisCampaignFacade(User).RetrieveByCriteriaSort(criterias, sortColl, indexPage, dtgFSChassis.PageSize, totalRow)
                    dataDownload = New FSChassisCampaignFacade(User).Retrieve(criterias, sortColl)
                Else
                    data = New FSChassisCampaignFacade(User).RetrieveByCriteria(criterias, indexPage, dtgFSChassis.PageSize, totalRow)
                    dataDownload = New FSChassisCampaignFacade(User).Retrieve(criterias)
                End If
            ElseIf CType(ViewState("ProsesAwal"), Boolean) Then
                data = New FSChassisCampaignFacade(User).RetrieveActiveList(indexPage, dtgFSChassis.PageSize, totalRow)
                dataDownload = New FSChassisCampaignFacade(User).RetrieveList()
            End If
            dtgFSChassis.DataSource = data
            dtgFSChassis.VirtualItemCount = totalRow
            sessionHelper.SetSession(_sesData, dataDownload)
            Session.Add("DataSourceDtg", CType(dtgFSChassis.DataSource, ArrayList).Count)
            dtgFSChassis.DataBind()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            dtgFSChassis.DataSource = Nothing
            dtgFSChassis.DataBind()
        End Try
    End Sub

    Private Sub ViewFSChassisCampaign(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objFSChassisCampaign As FSChassisCampaign = New FSChassisCampaignFacade(User).Retrieve(nID)
        sessionHelper.SetSession("vsFSChassisCampaign", objFSChassisCampaign)


        For Each itemList As ListItem In lboxFSType.Items
            If (objFSChassisCampaign.FSKind.ID.ToString() = itemList.Value) Then
                itemList.Selected = True
            Else
                itemList.Selected = False
            End If
        Next

        Dim objFSChassisMaster As ChassisMaster = objFSChassisCampaign.ChassisMaster
        txtNoRangka.Text = objFSChassisMaster.ChassisNumber
        txtKeterangan.Text = objFSChassisCampaign.Remarks
        ddlIsAllow.SelectedValue = objFSChassisCampaign.IsAllow
        txtMessage.Text = objFSChassisCampaign.ErrorMessage
        Me.btnSave.Enabled = EditStatus
    End Sub

    Private Sub dtgFSChassis_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgFSChassis.ItemCommand
        ViewState("vsProcess") = "Search"
        If e.CommandName = "Edit" Then
            dtgFSChassis.SelectedIndex = e.Item.ItemIndex
            ViewFSChassisCampaign(e.Item.Cells(0).Text, True)
        ElseIf e.CommandName = "Delete" Then
            Try
                Dim nResult = DeleteFSChassisCampaign(e.Item.Cells(0).Text)
                If nResult = 2 Then
                    MessageBox.Show(SR.CannotDelete)
                ElseIf nResult = -1 Then
                    MessageBox.Show(SR.DeleteFail)
                Else
                    MessageBox.Show(SR.DeleteSucces)
                End If
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearData()
            BindDataGrid(dtgFSChassis.CurrentPageIndex)
        ElseIf e.CommandName = "Active" Then
            ActivateChassisCampaign(e.Item.Cells(0).Text)
            BindDataGrid(Session.Item("IndexPage"))  '-- Bind page-1
        ElseIf e.CommandName = "Inactive" Then
            InactivateChassisCampaign(e.Item.Cells(0).Text)
            BindDataGrid(Session.Item("IndexPage"))  '-- Bind page-1
        ElseIf e.CommandName = "View" Then
            dtgFSChassis.SelectedIndex = e.Item.ItemIndex
            ViewFSChassisCampaign(e.Item.Cells(0).Text, False)
        End If
    End Sub

    Private Function DeleteFSChassisCampaign(ByVal nID As Integer) As Integer
        Dim objFSChassisCampaign As FSChassisCampaign = New FSChassisCampaignFacade(User).Retrieve(nID)
        Dim Facade As FSChassisCampaignFacade = New FSChassisCampaignFacade(User)
        Facade.Delete(objFSChassisCampaign)
        dtgFSChassis.CurrentPageIndex = 0
        BindDataGrid(dtgFSChassis.CurrentPageIndex)
    End Function

    Private Sub ActivateChassisCampaign(ByVal nID As Integer)
        '-- Activate Parameter
        Dim oFSChassisCampaign As FSChassisCampaign = New FSChassisCampaignFacade(User).Retrieve(nID)
        oFSChassisCampaign.IsAllow = True  '-- Parameter Aktif

        Dim nResult = New FSChassisCampaignFacade(User).Update(oFSChassisCampaign)
    End Sub

    Private Sub InactivateChassisCampaign(ByVal nID As Integer)
        '-- Inactivate Parameter
        Dim oFSChassisCampaign As FSChassisCampaign = New FSChassisCampaignFacade(User).Retrieve(nID)
        oFSChassisCampaign.IsAllow = False  '-- Parameter Tidak Aktif
        Dim nResult = New FSChassisCampaignFacade(User).Update(oFSChassisCampaign)
    End Sub

    Private Sub dtgFSChassis_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgFSChassis.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim lbtnActive As LinkButton = CType(e.Item.FindControl("lbtnActive"), LinkButton)
            Dim lbtnInactive As LinkButton = CType(e.Item.FindControl("lbtnInactive"), LinkButton)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim lblBlock As Label = CType(e.Item.FindControl("lblBlock"), Label)

            If ViewState("vsUpload") = "InsertUpload" Then
                dtgFSChassis.Columns(7).Visible = False
                dtgFSChassis.Columns(6).Visible = True
                dtgFSChassis.Columns(5).Visible = True
                lbtnDelete.Visible = False
            ElseIf ViewState("vsProcess") = "Search" Or ViewState("ProsesAwal") = True Then
                dtgFSChassis.Columns(7).Visible = True
                dtgFSChassis.Columns(6).Visible = False
                dtgFSChassis.Columns(5).Visible = False
                lbtnDelete.Visible = True
            End If

            Dim RowValue As FSChassisCampaign = CType(e.Item.DataItem, FSChassisCampaign)  '-- Current record

            If RowValue.IsAllow = False Then
                lbtnActive.Visible = True
                lbtnInactive.Visible = False
                lblBlock.Text = "Block"
            ElseIf RowValue.IsAllow = True Then
                lbtnInactive.Visible = True
                lbtnActive.Visible = False
                lblBlock.Text = "UnBlock"
            End If

            'If sessHelper.GetSession("UpdateCampaign") Then
            '    lbtnDelete.Visible = True
            'Else
            '    lbtnDelete.Visible = False
            'End If

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                CType(e.Item.FindControl("lblNo"), Label).Text = CType(e.Item.ItemIndex + 1 + (dtgFSChassis.CurrentPageIndex * dtgFSChassis.PageSize), String)
            End If

            If CType(e.Item.FindControl("lblChassisNumber"), Label).Text.Length = 0 Then
                e.Item.Cells(3).Text = CType(e.Item.FindControl("lblErrorMessage"), Label).Text.Split("'")(1)
                CType(e.Item.FindControl("lblErrorMessage"), Label).Text = CType(e.Item.FindControl("lblErrorMessage"), Label).Text.Replace("'" & e.Item.Cells(3).Text & "'", "")
                'CType(e.Item.FindControl("lblChassisNumber"), TextBox).Text = "error cuy"
            End If

            If Not e.Item.FindControl("lbtnActive") Is Nothing Then
                '-- Confirm activation
                CType(e.Item.FindControl("lbtnActive"), LinkButton).ToolTip = "Unblock"
                CType(e.Item.FindControl("lbtnActive"), LinkButton).Attributes.Add("OnClick", "return confirm('Unblock record ini?');")
            End If
            If Not e.Item.FindControl("lbtnInactive") Is Nothing Then
                '-- Confirm inactivation
                CType(e.Item.FindControl("lbtnInactive"), LinkButton).ToolTip = "Block"
                CType(e.Item.FindControl("lbtnInactive"), LinkButton).Attributes.Add("OnClick", "return confirm('Block record ini?');")
            End If
            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                '-- Confirm deletion
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).ToolTip = "Hapus"
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Hapus record ini?');")
            End If

        End If
    End Sub
    'berubah
    Private Sub dtgFSChassis_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgFSChassis.SortCommand
        If Session.Item("DataSourceDtg") > 0 Then
            If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
                Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                    Case Sort.SortDirection.ASC
                        ViewState("currSortDirection") = Sort.SortDirection.DESC
                    Case Sort.SortDirection.DESC
                        ViewState("currSortDirection") = Sort.SortDirection.ASC
                End Select
            Else
                ViewState("currSortColumn") = e.SortExpression
                ViewState("currSortDirection") = Sort.SortDirection.ASC
            End If
            'Todo session
            Session.Add("SortColumn", CType(ViewState("currSortColumn"), String))
            'Todo session
            Session.Add("SortDirection", CType(ViewState("currSortDirection"), Sort.SortDirection))
            dtgFSChassis.SelectedIndex = -1
            dtgFSChassis.CurrentPageIndex = 0
            BindDataGrid(1)
        End If
    End Sub

    Private Sub dtgFSChassis_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgFSChassis.PageIndexChanged
        dtgFSChassis.CurrentPageIndex = e.NewPageIndex
        'Todo session
        Session.Add("IndexPage", e.NewPageIndex + 1)
        BindDataGrid(e.NewPageIndex + 1)
    End Sub

    Private Sub CheckFolderExist(ByVal fileName As String)
        Dim fiinfo As FileInfo = New FileInfo(fileName)
        If Not fiinfo.Directory.Exists Then
            fiinfo.Directory.Create()
        End If
    End Sub

    Private Function isValidChassisMaster(ByVal collFSChassisCampaign As ArrayList) As Boolean
        For Each item As FSChassisCampaign In collFSChassisCampaign
            If Not item.ErrorMessage Is Nothing Then
                If item.ErrorMessage.Length > 0 Then
                    Return False
                End If
            End If
        Next
        Return True
    End Function

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If (Not FileText.PostedFile Is Nothing) AndAlso (FileText.PostedFile.ContentLength > 0) Then
            ViewState("vsUpload") = "InsertUpload"

            Dim fileExt As String = Path.GetExtension(FileText.PostedFile.FileName)
            If Not (fileExt.ToLower() = ".xls" OrElse fileExt.ToLower() = ".xlsx") Then
                MessageBox.Show("Hanya Menerima File Excell")
                Return
            End If

            dtgFSChassis.DataSource = New ArrayList
            dtgFSChassis.DataBind()
            dtgFSChassis.Visible = False

            Me.btnSave.Enabled = False

            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate

            Dim msg As String = ""
            Try
                Dim SrcFile As String = Path.GetFileName(FileText.PostedFile.FileName)
                Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & DateTime.Now.ToString("dd/MM/yyy HHmmss") & SrcFile

                imp = New SAPImpersonate(_user, _password, _webServer)

                If imp.Start() Then
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(FileText.PostedFile.InputStream, targetFile)
                    Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
                    Dim parser As UploadChassisCampaignParser = New UploadChassisCampaignParser(FileText.PostedFile.ContentType.ToString, companyCode)

                    '-- Parse data file and store result into arraylist
                    Dim arlChassisCampaign As ArrayList = CType(parser.ParseExcelNoTransaction(targetFile, "[Sheet1$A1:C152]", "User"), ArrayList)

                    Dim i As Integer
                    If arlChassisCampaign.Count <= 0 Then
                        btnSave.Enabled = False
                    End If
                    sessionHelper.SetSession(_sesUpload, arlChassisCampaign)

                    dtgFSChassis.DataSource = arlChassisCampaign '-- Reset datagrid first
                    dtgFSChassis.CurrentPageIndex = 0
                    BindUpload()
                    dtgFSChassis.Visible = True
                End If
            Catch ex As Exception
                MessageBox.Show("Fail To Process " & ex.Message)
            Finally
                imp.StopImpersonate()
                imp = Nothing
            End Try
        Else
            MessageBox.Show("Berkas Upload tidak boleh Kosong")
        End If
    End Sub

    Private Sub BindUpload()
        Dim totalRow As Integer = 0
        Dim _arlFSChassisCampaign = New ArrayList
        Dim _arlValid As ArrayList = New ArrayList
        Dim _arlInValid As ArrayList = New ArrayList

        Try
            _arlFSChassisCampaign = sessionHelper.GetSession(_sesUpload)

            If Not IsNothing(_arlFSChassisCampaign) Then
                btnSave.Enabled = True
                totalRow = _arlFSChassisCampaign.Count
                dtgFSChassis.DataSource = _arlFSChassisCampaign
                Dim iError As Integer = 0
                'For Each _c As FSChassisCampaign In _arlFSChassisCampaign
                '    If _c.ErrorMessage <> String.Empty And _c.ErrorMessage <> "OK" Then
                '        btnSave.Enabled = False
                '        'Exit For
                '    End If
                'Next

                dtgFSChassis.VirtualItemCount = totalRow
                dtgFSChassis.DataBind()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Protected Sub btnDnLoad_Click(sender As Object, e As EventArgs) Handles btnDnLoad.Click
        Dim data As ArrayList = CType(sessionHelper.GetSession(_sesData), ArrayList)

        If IsNothing(data) Then
            MessageBox.Show("Tidak ada data yang di download")
        Else
            DoDownload(data)
        End If
    End Sub

    Private Sub DoDownload(ByVal data As ArrayList, Optional ByVal isAll As Boolean = False)
        Dim sFileName As String
        sFileName = "ChassisCampaign" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls

        Dim DepositAData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(DepositAData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(DepositAData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                'If isAll Then
                WriteData(sw, data)
                'Else
                '    WriteSparePart(sw, data)
                'End If


                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal" & ex.Message)
        End Try
    End Sub

    Private Sub WriteData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("FREE SERVICE -  Daftar Chassis Campaign")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No" & tab)
            itemLine.Append("Jenis Free Service" & tab)
            itemLine.Append("No Rangka" & tab)
            itemLine.Append("Keterangan" & tab)
            itemLine.Append("Status" & tab)
            sw.WriteLine(itemLine.ToString())

            'Dim i As Integer = 1
            'Dim DealerCode As String = ""
            Dim Number As Integer


            For Each item As FSChassisCampaign In data
                If item.RowStatus = 0 Then
                    Dim status As String = ""
                    If (item.IsAllow = False) Then
                        status = "Block"
                    Else
                        status = "Unblock"
                    End If
                    Number += 1
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(Number.ToString & tab)
                    itemLine.Append(item.FSKind.KindDescription & tab)
                    itemLine.Append(item.ChassisMaster.ChassisNumber & tab)
                    itemLine.Append(item.Remarks & tab)
                    itemLine.Append(status & tab)

                    sw.WriteLine(itemLine.ToString())
                End If
            Next
        End If
    End Sub

    Private Function InsertDataMSP(ByVal objChassisMaster As ChassisMaster, ByVal FSKindCode As String)
        Dim objMSPMaster As MSPMaster
        Dim MSPTypeID As Integer
        Dim objMSPRegExisting As MSPRegistration = New MSPRegistration
        Dim isValidInsertMSP As Boolean = False
        Dim isValidInsertRegistration As Boolean = False
        Dim arlMSPTypeList As ArrayList = New ArrayList
        Dim MSPRegistrationID As Integer = 0

        If FSKindCode = "C" Then
            MSPTypeID = 1
        ElseIf FSKindCode = "D" Then
            MSPTypeID = 2
        ElseIf FSKindCode = "E" Then
            MSPTypeID = 3
        Else
            Exit Function
        End If

        Dim critMSPMaster As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critMSPMaster.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPMaster), "Status", MatchType.Exact, 1))
        critMSPMaster.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPMaster), "VehicleType.ID", MatchType.Exact, objChassisMaster.VechileColor.VechileType.ID))
        critMSPMaster.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPMaster), "MSPType.ID", MatchType.Exact, MSPTypeID))

        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(MSPMaster), "ID", Sort.SortDirection.DESC))

        Dim arlMSPMaster As ArrayList = New MSPMasterFacade(User).Retrieve(critMSPMaster, sortCol)
        If arlMSPMaster.Count > 0 Then
            objMSPMaster = CType(arlMSPMaster(0), MSPMaster)
        End If

        Dim critMSPReg As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critMSPReg.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPRegistration), "ChassisMaster.ID", MatchType.Exact, objChassisMaster.ID))
        critMSPReg.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPRegistration), "Dealer.ID", MatchType.Exact, objChassisMaster.Dealer.ID))

        Dim arlMSPReg As ArrayList = New MSPRegistrationFacade(User).Retrieve(critMSPReg)
        If arlMSPReg.Count > 0 Then
            objMSPRegExisting = CType(arlMSPReg(0), MSPRegistration)
            MSPRegistrationID = objMSPRegExisting.ID
            For Each oMSPRegHistory As MSPRegistrationHistory In objMSPRegExisting.MSPRegistrationHistorys
                arlMSPTypeList.Add(oMSPRegHistory.MSPMaster.MSPType.ID)
            Next
            If arlMSPTypeList.IndexOf(MSPTypeID) < 0 Then
                isValidInsertMSP = True
            End If
        Else
            isValidInsertMSP = True
            isValidInsertRegistration = True
        End If

        If isValidInsertMSP Then
            DoInsertMSP(objChassisMaster, isValidInsertRegistration, MSPRegistrationID, objMSPMaster)
        End If

    End Function

    Private Function DoInsertMSP(ByVal objChassisMaster As ChassisMaster, ByVal isValidInsertRegistration As Boolean, ByVal MSPRegistrationID As Integer, ByVal objMSPMaster As MSPMaster)
        Dim objMSPRegistration As MSPRegistration = New MSPRegistration
        Dim objMSPRegistrationHistory As MSPRegistrationHistory = New MSPRegistrationHistory
        Dim objMSPCustomer As MSPCustomer = New MSPCustomer
        Dim objCustomerReqProfile As CustomerRequestProfile = New CustomerRequestProfile
        Dim arlMSPRegistrationHistory As ArrayList

        Dim critCustomerReq As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CustomerRequestProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critCustomerReq.opAnd(New Criteria(GetType(KTB.DNet.Domain.CustomerRequestProfile), "ProfileHeader.Code", MatchType.Exact, "NOKTP"))
        critCustomerReq.opAnd(New Criteria(GetType(KTB.DNet.Domain.CustomerRequestProfile), "CustomerRequest.CustomerCode", MatchType.Exact, objChassisMaster.EndCustomer.Customer.Code))

        Dim arlCustomerReq As ArrayList = New CustomerRequestProfileFacade(User).Retrieve(critCustomerReq)
        If arlCustomerReq.Count > 0 Then
            objCustomerReqProfile = CType(arlCustomerReq(0), CustomerRequestProfile)
        End If

        objMSPRegistrationHistory.MSPMaster = objMSPMaster
        objMSPRegistrationHistory.RequestType = EnumStatusMSP.StatusType.Baru
        objMSPRegistrationHistory.Sequence = 1
        objMSPRegistrationHistory.SelisihAmount = 0
        objMSPRegistrationHistory.Status = EnumStatusMSP.Status.Selesai
        objMSPRegistrationHistory.RegistrationDate = DateTime.Now
        objMSPRegistrationHistory.BenefitMasterHeaderID = 197

        If isValidInsertRegistration Then
            objMSPRegistration.ChassisMaster = objChassisMaster
            objMSPRegistration.Dealer = objChassisMaster.Dealer

            Dim oMSPRegistrationID As Integer = New MSPRegistrationFacade(User).Insert(objMSPRegistration, objMSPRegistrationHistory, objMSPCustomer)

            objMSPRegistration.MSPCode = "MSP" & oMSPRegistrationID.ToString.PadLeft(7, "0")
            Dim oUpdateMSP As Integer = New MSPRegistrationFacade(User).Update(objMSPRegistration, objMSPRegistrationHistory, objMSPCustomer)


            Dim critMSPRegistrationHist As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPRegistrationHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critMSPRegistrationHist.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPRegistrationHistory), "MSPRegistration.ID", MatchType.Exact, oMSPRegistrationID))
            critMSPRegistrationHist.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPRegistrationHistory), "MSPMaster.ID", MatchType.Exact, objMSPRegistrationHistory.MSPMaster.ID))
            critMSPRegistrationHist.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPRegistrationHistory), "BenefitMasterHeader.ID", MatchType.Exact, objMSPRegistrationHistory.BenefitMasterHeaderID))

            arlMSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(critMSPRegistrationHist)
            For Each oMSPRegistrationHistory As MSPRegistrationHistory In arlMSPRegistrationHistory
                objMSPRegistrationHistory.ID = oMSPRegistrationHistory.ID
            Next
        Else
            If MSPRegistrationID > 0 Then
                objMSPRegistrationHistory.MSPRegistration = New MSPRegistrationFacade(User).Retrieve(MSPRegistrationID)
                Dim oMSPRegistrationHistoryID As Integer = New MSPRegistrationHistoryFacade(User).Insert(objMSPRegistrationHistory)
            End If
        End If

        Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
        objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.MSP_Registration), objMSPRegistrationHistory.ID, 0, EnumStatusMSP.Status.Selesai)

    End Function
End Class
