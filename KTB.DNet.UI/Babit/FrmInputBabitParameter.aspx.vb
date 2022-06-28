#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.Helper
Imports OfficeOpenXml
#End Region

Public Class FrmInputBabitParameter
    Inherits System.Web.UI.Page

#Region "Custom Variables Declaration"

    Private objDealer As New Dealer
    Private objLoginUser As UserInfo
    Private objBabitParameterHeader As BabitParameterHeader
    Private objBabitParameterDetail As BabitParameterDetail
    Private arrBabitParameterDetail As ArrayList = New ArrayList
    Private arrDeleteBabitParameterDetail As ArrayList = New ArrayList
    Private sesHelper As New SessionHelper
    Private sessBabitParameterDetail As String = "FrmInputBabitParameter.sessBabitParameterDetail"
    Private sessBabitParameterHeader As String = "FrmInputBabitParameter.sessBabitParameterHeader"
    Private SessionCriteriaParameter As String = "FrmInputBabitParameter.CriteriaGridParameter"
    Private SessionCriteriaExcel As String = "FrmInputBabitParameter.CriteriaDownloadExcel"

    Private intBabitParameterHeaderID As Integer = 0
    Private intItemIndex As Integer = 0
    Private displayPriv As Boolean = False
    Private editPriv As Boolean = False
    Private deletePriv As Boolean = False
    Private tambahPriv As Boolean = False

    Private Property SesDealer() As Dealer
        Get
            Return CType(sesHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sesHelper.SetSession("DEALER", Value)
        End Set
    End Property
#End Region

#Region "Custom Methods"
    Private Sub disableform(isEnb As Boolean)
        Me.txtParameterName.Enabled = isEnb
        Me.ddlBabitMasterEventType.Enabled = isEnb
        Me.ddlJenisPengajuan.Enabled = isEnb
        Me.ddlParameterCategory.Enabled = isEnb
        Me.ddlStatus.Enabled = isEnb
        Me.chkIsMandatory.Enabled = isEnb
        Me.ddlIsMandatory.Enabled = isEnb
        Me.dgParameterDetail.Columns(dgParameterDetail.Columns.Count - 1).Visible = isEnb
        Me.dgParameterDetail.ShowFooter = isEnb
        Me.btnSave.Enabled = isEnb
    End Sub

    Private Sub RestoreCriteria()
        Dim crit As Hashtable
        crit = CType(sesHelper.GetSession(SessionCriteriaParameter), Hashtable)
        If Not crit Is Nothing Then
            ddlBabitMasterEventType.SelectedValue = CStr(crit.Item("BabitMasterEventTypeID"))
            ddlParameterCategory.SelectedValue = CStr(crit.Item("ParameterCategory"))
            txtParameterName.Text = CStr(crit.Item("ParameterName"))
            ddlStatus.SelectedValue = CStr(crit.Item("Status"))
            ddlIsMandatory.SelectedValue = CStr(crit.Item("IsMandatory"))

            ViewState("currSortColumn") = CStr(crit.Item("currSortColumn"))
            ViewState("currSortDirection") = CStr(crit.Item("currSortDirection"))
            dgListParam.CurrentPageIndex = CInt(crit.Item("PageIndex"))
        End If
    End Sub

    Private Sub StoreCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("BabitMasterEventTypeID", ddlBabitMasterEventType.SelectedValue)
        crit.Add("ParameterCategory", ddlParameterCategory.SelectedValue)
        crit.Add("ParameterName", txtParameterName.Text)
        crit.Add("Status", ddlStatus.SelectedValue)
        crit.Add("IsMandatory", ddlIsMandatory.SelectedValue)
        crit.Add("PageIndex", dgListParam.CurrentPageIndex)
        crit.Add("currSortColumn", ViewState("currSortColumn"))
        crit.Add("currSortDirection", ViewState("currSortDirection"))

        sesHelper.SetSession(SessionCriteriaParameter, crit) '-- Store in session
    End Sub
    Private Sub BindGridListParam(ByVal index As Integer, Optional ByVal sortColoum As String = Nothing, Optional ByVal sortType As Sort.SortDirection = Sort.SortDirection.ASC)

        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtParameterName.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "ParameterName", MatchType.[Partial], txtParameterName.Text.Trim))
        End If
        If ddlStatus.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "Status", MatchType.Exact, CType(ddlStatus.SelectedValue, String)))
        End If
        If ddlJenisPengajuan.SelectedValue = "Babit" Then
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.FormType", MatchType.InSet, "(1, 2, 3)"))
        ElseIf ddlJenisPengajuan.SelectedValue = "Event" Then
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.FormType", MatchType.InSet, "(4, 5)"))
        End If
        If ddlBabitMasterEventType.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.ID", MatchType.Exact, CType(ddlBabitMasterEventType.SelectedValue, String)))
        End If
        If ddlParameterCategory.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "ParameterCategory", MatchType.Exact, CType(ddlParameterCategory.SelectedValue, String)))
        End If
        If ddlIsMandatory.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "IsMandatory", MatchType.Exact, CType(chkIsMandatory.Checked, Short)))
        End If
        sesHelper.SetSession(SessionCriteriaExcel, criterias)
        _arrList = New BabitParameterHeaderFacade(User).RetrieveActiveList(criterias, index + 1, dgListParam.PageSize, totalRow, sortColoum, sortType)

        dgListParam.VirtualItemCount = totalRow
        dgListParam.DataSource = _arrList
        dgListParam.DataBind()
    End Sub

    Private Sub LoadDataBabitParameter(intBabitParameterHeaderID As Integer)
        objBabitParameterHeader = New BabitParameterHeaderFacade(User).Retrieve(intBabitParameterHeaderID)
        If Not IsNothing(objBabitParameterHeader) Then
            Me.txtParameterName.Text = objBabitParameterHeader.ParameterName
            If Not IsNothing(objBabitParameterHeader.BabitMasterEventType) Then
                Me.ddlBabitMasterEventType.SelectedValue = objBabitParameterHeader.BabitMasterEventType.ID
            End If
            Me.ddlParameterCategory.SelectedValue = objBabitParameterHeader.ParameterCategory
            Me.ddlStatus.SelectedValue = objBabitParameterHeader.Status
            Me.chkIsMandatory.Checked = objBabitParameterHeader.IsMandatory
            If objBabitParameterHeader.IsMandatory = True Then
                ddlIsMandatory.SelectedValue = 1
            Else
                ddlIsMandatory.SelectedValue = 0
            End If
            sesHelper.SetSession(sessBabitParameterHeader, objBabitParameterHeader)

            Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ID", MatchType.Exact, intBabitParameterHeaderID))
            arrBabitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(criterias)
            sesHelper.SetSession(sessBabitParameterDetail, arrBabitParameterDetail)
            BindGridBabitParameter()
        End If
    End Sub

    Private Sub ClearAll()
        hdnBabitParameterHeaderID.Value = ""
        txtParameterName.Text = ""
        ddlJenisPengajuan.SelectedIndex = 0
        ddlBabitMasterEventType.SelectedIndex = 0
        ddlParameterCategory.SelectedIndex = 0
        ddlStatus.SelectedIndex = 0
        chkIsMandatory.Checked = False
        sesHelper.SetSession(sessBabitParameterDetail, New ArrayList)
        BindGridBabitParameter()
    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Sub BindCategoryParam()
        ddlParameterCategory.Items.Clear()
        ddlParameterCategory.DataSource = New StandardCodeFacade(User).RetrieveByCategory("EnumBabit.BabitParameterCategory")
        ddlParameterCategory.DataTextField = "ValueDesc"
        ddlParameterCategory.DataValueField = "ValueId"
        ddlParameterCategory.DataBind()
        ddlParameterCategory.Items.Insert(0, New ListItem("Silakan Piih ", -1))
        ddlParameterCategory.SelectedIndex = 0
    End Sub

    Sub BindStatusParam()
        ddlStatus.Items.Clear()
        ddlStatus.Items.Add(New ListItem("Aktif", 1))
        ddlStatus.Items.Add(New ListItem("Tidak Aktif", 0))
        ddlStatus.DataBind()
        ddlStatus.Items.Insert(0, New ListItem("Silakan Piih ", -1))
        ddlStatus.SelectedIndex = 0
    End Sub

    Sub BindIsMandatory()
        ddlIsMandatory.Items.Clear()
        ddlIsMandatory.Items.Add(New ListItem("Wajib", 1))
        ddlIsMandatory.Items.Add(New ListItem("Tidak Wajib", 0))
        ddlIsMandatory.DataBind()
        ddlIsMandatory.Items.Insert(0, New ListItem("Silakan Piih ", -1))
        ddlIsMandatory.SelectedIndex = 0
    End Sub

    Sub BindJenisPengajuan()
        With ddlJenisPengajuan
            .Items.Clear()
            'Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitMasterEventType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(BabitMasterEventType), "Status", MatchType.Exact, 1))
            'Dim arrayDDl As ArrayList = New BabitMasterEventTypeFacade(User).Retrieve(criterias)

            '.DataSource = arrayDDl
            '.DataTextField = "TypeName"
            '.DataValueField = "ID"
            '.DataBind()
            .Items.Insert(0, New ListItem("Silakan Pilih ", "-1"))
            .Items.Insert(1, New ListItem("Babit ", "Babit"))
            .Items.Insert(2, New ListItem("Event ", "Event"))
            .SelectedIndex = 0
        End With
    End Sub

    Sub BindBabitMasterEventType()
        With ddlBabitMasterEventType
            .Items.Clear()
            If ddlJenisPengajuan.SelectedIndex <> 0 Then
                Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitMasterEventType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BabitMasterEventType), "Status", MatchType.Exact, 1))
                Select Case ddlJenisPengajuan.SelectedIndex
                    Case 1
                        criterias.opAnd(New Criteria(GetType(BabitMasterEventType), "FormType", MatchType.InSet, "(1, 2, 3)"))
                    Case 2
                        criterias.opAnd(New Criteria(GetType(BabitMasterEventType), "FormType", MatchType.InSet, "(4, 5)"))
                End Select

                Dim arrayDDl As ArrayList = New BabitMasterEventTypeFacade(User).Retrieve(criterias)

                .DataSource = arrayDDl
                .DataTextField = "TypeName"
                .DataValueField = "ID"
                .DataBind()
            End If
            .Items.Insert(0, New ListItem("Silakan Piih ", -1))
            .SelectedIndex = 0
        End With
    End Sub

    Sub BindGridBabitParameter()
        arrBabitParameterDetail = CType(sesHelper.GetSession(sessBabitParameterDetail), ArrayList)
        If IsNothing(arrBabitParameterDetail) Then
            arrBabitParameterDetail = GetArrayGridBabitParam(hdnBabitParameterHeaderID.Value)
        End If
        dgParameterDetail.DataSource = arrBabitParameterDetail
        dgParameterDetail.DataBind()
        sesHelper.SetSession(sessBabitParameterDetail, arrBabitParameterDetail)
    End Sub

    Private Function GetArrayGridBabitParam(ByVal _BabitParameterHeaderID As Integer) As ArrayList
        Dim arr As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ID", MatchType.Exact, _BabitParameterHeaderID))
        arr = New BabitParameterDetailFacade(User).Retrieve(criterias)
        If IsNothing(arr) Then arr = New ArrayList
        Return arr
    End Function

    Private Function ValidateData() As String
        Dim sb As StringBuilder = New StringBuilder

        If (txtParameterName.Text.Trim = String.Empty) Then
            sb.Append("Nama Parameter Harus Diisi\n")
        End If

        If (ddlBabitMasterEventType.SelectedValue = -1) Then
            sb.Append("Tipe Parameter Harus Diisi\n")
        End If
        If (ddlParameterCategory.SelectedValue = -1) Then
            sb.Append("Kategori Parameter Harus Diisi\n")
        End If
        If (ddlStatus.SelectedValue = -1) Then
            sb.Append("Status Parameter Harus Diisi\n")
        End If
        If (sesHelper.GetSession(sessBabitParameterDetail) Is Nothing) Then
            sb.Append("Data Babit Parameter Detail belum ada\n")
        Else
            If CType(Session(sessBabitParameterDetail), ArrayList).Count = 0 Then
                sb.Append("Data Babit Parameter Detail belum ada\n")
            End If
        End If

        Return sb.ToString()
    End Function

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Setting_Parameter_Display_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=MASTER - SETTING PARAMETER")
        End If
        displayPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Setting_Parameter_Display_Privilege)
        editPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Setting_Parameter_Edit_Privilege)
        deletePriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Setting_Parameter_Delete_Privilege)
        tambahPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Setting_Parameter_Tambah_Privilege)
    End Sub
#End Region

#Region "Event Handlers"

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        objDealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        objLoginUser = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Dim intId As Integer = 0
        InitiateAuthorization()

        If (Not IsPostBack) Then
            BindJenisPengajuan()
            BindBabitMasterEventType()
            BindIsMandatory()
            BindStatusParam()
            BindCategoryParam()
            btnDelete.Visible = False
            btnBaru.Visible = tambahPriv
            btnSave.Visible = False
            divDetailParam.Attributes("style") = "display:none"
            DivListParam.Attributes("style") = "display:table-row"

            If Not IsNothing(Request.QueryString("Mode")) AndAlso CType(Request.QueryString("Mode"), String).Trim <> "" Then
                If Not IsNothing(Request.QueryString("BabitParameterHeaderID")) Then
                    hdnBabitParameterHeaderID.Value = Request.QueryString("BabitParameterHeaderID")
                    LoadDataBabitParameter(hdnBabitParameterHeaderID.Value)
                End If

                btnBaru.Visible = False
                btnSave.Visible = False
                btnDelete.Visible = False
                btnSearch.Visible = True
                btnSearch.Text = "Batal"
                divDetailParam.Attributes("style") = "display:table-row"
                DivListParam.Attributes("style") = "display:none"
                If Request.QueryString("Mode") = "View" Then
                    disableform(False)
                ElseIf Request.QueryString("Mode") = "Edit" Then
                    disableform(True)
                    btnSave.Visible = True
                ElseIf Request.QueryString("Mode") = "Delete" Then
                    disableform(False)
                    btnDelete.Visible = deletePriv
                End If
            Else
                RestoreCriteria()
                BindGridBabitParameter()
                BindGridListParam(dgListParam.CurrentPageIndex)
            End If
        End If
    End Sub

    Private Sub dgParameterDetail_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgParameterDetail.ItemCommand
        Dim txtFParameterDetailName As TextBox
        Dim txtEParameterDetailName As TextBox
        Dim oBabitParameterDetail As BabitParameterDetail

        objBabitParameterHeader = CType(sesHelper.GetSession(sessBabitParameterHeader), BabitParameterHeader)
        arrBabitParameterDetail = CType(sesHelper.GetSession(sessBabitParameterDetail), ArrayList)

        Select Case e.CommandName
            Case "add"
                oBabitParameterDetail = New BabitParameterDetail
                txtFParameterDetailName = CType(e.Item.FindControl("txtFParameterDetailName"), TextBox)
                If txtFParameterDetailName.Text.Trim = "" Then
                    MessageBox.Show("Nama Parameter harus diisi.")
                    Return
                End If

                oBabitParameterDetail.BabitParameterHeader = objBabitParameterHeader
                oBabitParameterDetail.ParameterDetailName = txtFParameterDetailName.Text.Trim
                arrBabitParameterDetail.Add(oBabitParameterDetail)

            Case "save" 'Update this datagrid item   
                txtEParameterDetailName = CType(e.Item.FindControl("txtEParameterDetailName"), TextBox)
                If txtEParameterDetailName.Text.Trim = "" Then
                    MessageBox.Show("Nama Parameter harus diisi.")
                    Return
                End If

                oBabitParameterDetail = CType(arrBabitParameterDetail(e.Item.ItemIndex), BabitParameterDetail)
                oBabitParameterDetail.BabitParameterHeader = objBabitParameterHeader
                oBabitParameterDetail.ParameterDetailName = txtEParameterDetailName.Text.Trim
                dgParameterDetail.EditItemIndex = -1
                dgParameterDetail.ShowFooter = True

            Case "edit" 'Edit mode activated
                dgParameterDetail.ShowFooter = False
                dgParameterDetail.EditItemIndex = e.Item.ItemIndex

            Case "delete" 'Delete this datagrid item 
                Dim objDeleteBabitParameterDetail As BabitParameterDetail = CType(arrBabitParameterDetail(e.Item.ItemIndex), BabitParameterDetail)
                If CheckIsExistFromInputBabitByDetail(objDeleteBabitParameterDetail) = False Then
                    Exit Sub
                End If

                Try
                    If objDeleteBabitParameterDetail.ID > 0 Then
                        Dim arrDelete As ArrayList
                        arrDelete = CType(sesHelper.GetSession("sessDeleteBabitParameterDetail"), ArrayList)
                        If IsNothing(arrDelete) Then arrDelete = New ArrayList
                        arrDelete.Add(objDeleteBabitParameterDetail)
                        sesHelper.SetSession("sessDeleteBabitParameterDetail", arrDelete)
                    End If
                    arrBabitParameterDetail.RemoveAt(e.Item.ItemIndex)
                Catch
                End Try
            Case "cancel" 'Cancel Update this datagrid item 
                dgParameterDetail.EditItemIndex = -1
                dgParameterDetail.ShowFooter = True
        End Select

        sesHelper.SetSession(sessBabitParameterDetail, arrBabitParameterDetail)
        BindGridBabitParameter()
    End Sub

    Function CheckIsExistFromInputBabitByDetail(ByVal objParamDtl As BabitParameterDetail)
        If Request.QueryString("Mode") = "Edit" Then
            'Untuk Babit event
            If objParamDtl.BabitParameterHeader.BabitMasterEventType.FormType = "1" Then
                Dim objBabitEventDetailFacade As BabitEventDetailFacade = New BabitEventDetailFacade(User)
                Dim criterias3 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias3.opAnd(New Criteria(GetType(BabitEventDetail), "BabitParameterDetail.ID", MatchType.Exact, objParamDtl.ID))
                Dim arrBabitEventDetail As ArrayList = objBabitEventDetailFacade.Retrieve(criterias3)
                If arrBabitEventDetail.Count > 0 Then
                    MessageBox.Show("Hapus data gagal, Parameter Babit ini sudah pernah dipakai di Input Babit Event.")
                    Return False
                End If
            End If

            'Untuk Babit Pameran
            If objParamDtl.BabitParameterHeader.BabitMasterEventType.FormType = "2" Then
                Dim objBabitPameranDetailFacade As BabitPameranDetailFacade = New BabitPameranDetailFacade(User)
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitPameranDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BabitPameranDetail), "BabitParameterDetail.ID", MatchType.Exact, objParamDtl.ID))
                Dim arrBabitPameranDetail As ArrayList = objBabitPameranDetailFacade.Retrieve(criterias)
                If arrBabitPameranDetail.Count > 0 Then
                    MessageBox.Show("Hapus data gagal, Parameter Babit ini sudah pernah dipakai di Input Babit Pameran.")
                    Return False
                End If
            End If

            'Untuk Babit Iklan
            If objParamDtl.BabitParameterHeader.BabitMasterEventType.FormType = "3" Then
                Dim objBabitIklanDetailFacade As BabitIklanDetailFacade = New BabitIklanDetailFacade(User)
                Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitIklanDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias2.opAnd(New Criteria(GetType(BabitIklanDetail), "BabitParameterDetail.ID", MatchType.Exact, objParamDtl.ID))
                Dim arrBabitIklanDetail As ArrayList = objBabitIklanDetailFacade.Retrieve(criterias2)
                If arrBabitIklanDetail.Count > 0 Then
                    MessageBox.Show("Hapus data gagal, Parameter Babit ini sudah pernah dipakai di Input Babit Iklan.")
                    Return False
                End If
            End If

            'Untuk Proposal Event 
            If objParamDtl.BabitParameterHeader.BabitMasterEventType.FormType = "4" Then
                Dim objBabitEventProposalDetailFacade As BabitEventProposalDetailFacade = New BabitEventProposalDetailFacade(User)
                Dim criterias4 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventProposalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias4.opAnd(New Criteria(GetType(BabitEventProposalDetail), "BabitParameterDetail.ID", MatchType.Exact, objParamDtl.ID))
                Dim arrBabitEventProposalDetail As ArrayList = objBabitEventProposalDetailFacade.Retrieve(criterias4)
                If arrBabitEventProposalDetail.Count > 0 Then
                    MessageBox.Show("Hapus data gagal, Parameter Babit ini sudah pernah dipakai di Input Event Proposal.")
                    Return False
                End If
            End If

            'Untuk Laporan Event
            If objParamDtl.BabitParameterHeader.BabitMasterEventType.FormType = "5" Then
                Dim objBabitEventReportDetailFacade As BabitEventReportDetailFacade = New BabitEventReportDetailFacade(User)
                Dim criterias5 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias5.opAnd(New Criteria(GetType(BabitEventReportDetail), "BabitParameterDetail.ID", MatchType.Exact, objParamDtl.ID))
                Dim arrBabitEventReportDetail As ArrayList = objBabitEventReportDetailFacade.Retrieve(criterias5)
                If arrBabitEventReportDetail.Count > 0 Then
                    MessageBox.Show("Hapus data gagal, Parameter Babit ini sudah pernah dipakai di Input Event Laporan.")
                    Return False
                End If
            End If

            If objParamDtl.BabitParameterHeader.BabitMasterEventType.FormType = "2" Then
                Dim objBabitPameranExpenseFacade As BabitPameranExpenseFacade = New BabitPameranExpenseFacade(User)
                Dim criterias6 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitPameranExpense), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias6.opAnd(New Criteria(GetType(BabitPameranExpense), "BabitParameterDetail.ID", MatchType.Exact, objParamDtl.ID))
                Dim arrBabitPameranExpense As ArrayList = objBabitPameranExpenseFacade.Retrieve(criterias6)
                If arrBabitPameranExpense.Count > 0 Then
                    MessageBox.Show("Hapus data gagal, Parameter Babit ini sudah pernah dipakai di Input Pameran Expense.")
                    Return False
                End If
            End If

        End If
        Return True

    End Function

    Function CheckIsExistFromInputBabitByHeader(ByVal objParamHdr As BabitParameterHeader, ByVal strRequestQueryString As String)
        If Request.QueryString("Mode") = strRequestQueryString Then
            'untuk Babit Event
            If objParamHdr.BabitMasterEventType.FormType = "1" Then
                Dim objBabitEventDetailFacade As BabitEventDetailFacade = New BabitEventDetailFacade(User)
                Dim criterias3 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias3.opAnd(New Criteria(GetType(BabitEventDetail), "BabitParameterDetail.BabitParameterHeader.ID", MatchType.Exact, Me.hdnBabitParameterHeaderID.Value))
                Dim arrBabitEventDetail As ArrayList = objBabitEventDetailFacade.Retrieve(criterias3)
                If arrBabitEventDetail.Count > 0 Then
                    MessageBox.Show(strRequestQueryString & " data gagal, Parameter Babit ini sudah pernah dipakai di Input Babit Event.")
                    Return False
                End If
            End If

            'Untuk Babit Pameran
            If objParamHdr.BabitMasterEventType.FormType = "2" Then
                Dim objBabitPameranDetailFacade As BabitPameranDetailFacade = New BabitPameranDetailFacade(User)
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitPameranDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BabitPameranDetail), "BabitParameterDetail.BabitParameterHeader.ID", MatchType.Exact, Me.hdnBabitParameterHeaderID.Value))
                Dim arrBabitPameranDetail As ArrayList = objBabitPameranDetailFacade.Retrieve(criterias)
                If arrBabitPameranDetail.Count > 0 Then
                    MessageBox.Show(strRequestQueryString & " data gagal, Parameter Babit ini sudah pernah dipakai di Input Babit Pameran.")
                    Return False
                End If
            End If

            'Untuk Babit Iklan
            If objParamHdr.BabitMasterEventType.FormType = "3" Then
                Dim objBabitIklanDetailFacade As BabitIklanDetailFacade = New BabitIklanDetailFacade(User)
                Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitIklanDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias2.opAnd(New Criteria(GetType(BabitIklanDetail), "BabitParameterDetail.BabitParameterHeader.ID", MatchType.Exact, Me.hdnBabitParameterHeaderID.Value))
                Dim arrBabitIklanDetail As ArrayList = objBabitIklanDetailFacade.Retrieve(criterias2)
                If arrBabitIklanDetail.Count > 0 Then
                    MessageBox.Show(strRequestQueryString & " data gagal, Parameter Babit ini sudah pernah dipakai di Input Babit Iklan.")
                    Return False
                End If
            End If


            'Untuk Event Proposal
            If objParamHdr.BabitMasterEventType.FormType = "4" Then
                Dim objBabitEventProposalDetailFacade As BabitEventProposalDetailFacade = New BabitEventProposalDetailFacade(User)
                Dim criterias4 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventProposalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias4.opAnd(New Criteria(GetType(BabitEventProposalDetail), "BabitParameterDetail.BabitParameterHeader.ID", MatchType.Exact, Me.hdnBabitParameterHeaderID.Value))
                Dim arrBabitEventProposalDetail As ArrayList = objBabitEventProposalDetailFacade.Retrieve(criterias4)
                If arrBabitEventProposalDetail.Count > 0 Then
                    MessageBox.Show(strRequestQueryString & " data gagal, Parameter Babit ini sudah pernah dipakai di Input Event Proposal.")
                    Return False
                End If
            End If

            If objParamHdr.BabitMasterEventType.FormType = "5" Then
                Dim objBabitEventReportDetailFacade As BabitEventReportDetailFacade = New BabitEventReportDetailFacade(User)
                Dim criterias5 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias5.opAnd(New Criteria(GetType(BabitEventReportDetail), "BabitParameterDetail.BabitParameterHeader.ID", MatchType.Exact, Me.hdnBabitParameterHeaderID.Value))
                Dim arrBabitEventReportDetail As ArrayList = objBabitEventReportDetailFacade.Retrieve(criterias5)
                If arrBabitEventReportDetail.Count > 0 Then
                    MessageBox.Show(strRequestQueryString & " data gagal, Parameter Babit ini sudah pernah dipakai di Input Event Laporan.")
                    Return False
                End If
            End If

            If objParamHdr.BabitMasterEventType.FormType = "2" Then
                Dim objBabitPameranExpenseFacade As BabitPameranExpenseFacade = New BabitPameranExpenseFacade(User)
                Dim criterias6 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitPameranExpense), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias6.opAnd(New Criteria(GetType(BabitPameranExpense), "BabitParameterDetail.BabitParameterHeader.ID", MatchType.Exact, Me.hdnBabitParameterHeaderID.Value))
                Dim arrBabitPameranExpense As ArrayList = objBabitPameranExpenseFacade.Retrieve(criterias6)
                If arrBabitPameranExpense.Count > 0 Then
                    MessageBox.Show(strRequestQueryString & " data gagal, Parameter Babit ini sudah pernah dipakai di Input Event Pameran Expense.")
                    Return False
                End If
            End If
        End If

        Return True

    End Function

    Private Sub dgParameterDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgParameterDetail.ItemDataBound
        Dim oBabitParameterDetail As New BabitParameterDetail
        Dim txtFParameterDetailName As TextBox
        Dim txtEParameterDetailName As TextBox
        Dim lblParameterDetailName As Label

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            oBabitParameterDetail = CType(e.Item.DataItem, BabitParameterDetail)

            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgParameterDetail.CurrentPageIndex * dgParameterDetail.PageSize)

            lblParameterDetailName = CType(e.Item.FindControl("lblParameterDetailName"), Label)
            lblParameterDetailName.Text = oBabitParameterDetail.ParameterDetailName()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim str As String
        str = ValidateData()
        If (str.Length > 0) Then
            MessageBox.Show(str)
            Exit Sub
        End If
        arrBabitParameterDetail = CType(sesHelper.GetSession(sessBabitParameterDetail), ArrayList)
        arrDeleteBabitParameterDetail = CType(sesHelper.GetSession("sessDeleteBabitParameterDetail"), ArrayList)
        If Request.QueryString("Mode") <> "Edit" Then
            objBabitParameterHeader = New BabitParameterHeader
        Else
            objBabitParameterHeader = CType(sesHelper.GetSession(sessBabitParameterHeader), BabitParameterHeader)
        End If

        'Cek Validasi Status IsExist in Input Babit
        If Request.QueryString("Mode") = "Edit" Then
            If ddlStatus.SelectedValue = "0" Then  '--Update Status tidak aktif
                If CheckIsExistFromInputBabitByHeader(objBabitParameterHeader, Request.QueryString("Mode")) = False Then
                    Exit Sub
                End If
            End If
        End If

        objBabitParameterHeader.ParameterName = txtParameterName.Text
        objBabitParameterHeader.ParameterCategory = Me.ddlParameterCategory.SelectedValue
        objBabitParameterHeader.BabitMasterEventType = New BabitMasterEventTypeFacade(User).Retrieve(CType(ddlBabitMasterEventType.SelectedValue, Integer))
        objBabitParameterHeader.Status = Me.ddlStatus.SelectedValue
        objBabitParameterHeader.IsMandatory = Me.chkIsMandatory.Checked

        Dim _result As Integer = 0
        If Request.QueryString("Mode") = "Edit" Then
            If IsNothing(arrDeleteBabitParameterDetail) Then arrDeleteBabitParameterDetail = New ArrayList
            _result = New BabitParameterDetailFacade(User).UpdateTransaction(objBabitParameterHeader, arrBabitParameterDetail, arrDeleteBabitParameterDetail)
        Else
            _result = New BabitParameterDetailFacade(User).InsertTransaction(objBabitParameterHeader, arrBabitParameterDetail)
        End If

        If _result > 0 Then
            ClearAll()
            MessageBox.Show("Simpan Data Berhasil !")
            Server.Transfer("~/Babit/FrmInputBabitParameter.aspx?BabitParameterHeaderID=" & _result)
        Else
            MessageBox.Show("Simpan Data Gagal")
        End If
    End Sub

    Private Sub btnBaru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBaru.Click
        Me.ClearAll()
        Me.btnSave.Visible = True
        Me.btnDelete.Visible = False
        Me.btnBaru.Visible = False
        Me.btnSearch.Text = "Batal"
        divDetailParam.Attributes("style") = "display:table-row"
        DivListParam.Attributes("style") = "display:none"
    End Sub

    Private Sub dgListParam_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgListParam.ItemCommand
        Select Case e.CommandName
            Case "View"
                Response.Redirect("~/Babit/FrmInputBabitParameter.aspx?Mode=View&BabitParameterHeaderID=" & CInt(e.CommandArgument))
            Case "Edit"
                Response.Redirect("~/Babit/FrmInputBabitParameter.aspx?Mode=Edit&BabitParameterHeaderID=" & CInt(e.CommandArgument))
            Case "Delete"
                Response.Redirect("~/Babit/FrmInputBabitParameter.aspx?Mode=Delete&BabitParameterHeaderID=" & CInt(e.CommandArgument))
        End Select
    End Sub

    Private Sub dgListparam_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgListParam.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim objDomain As BabitParameterHeader = CType(e.Item.DataItem, BabitParameterHeader)

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = (e.Item.ItemIndex + 1 + (dgListParam.CurrentPageIndex * dgListParam.PageSize)).ToString()

                Dim lblBabitMasterEventType As Label = CType(e.Item.FindControl("lblBabitMasterEventType"), Label)
                lblBabitMasterEventType.Text = objDomain.BabitMasterEventType.TypeName

                Dim lblParameterCategory As Label = CType(e.Item.FindControl("lblParameterCategory"), Label)
                Dim arrParamcat As ArrayList = New StandardCodeFacade(User).RetrieveByValueId(objDomain.ParameterCategory, "EnumBabit.BabitParameterCategory")
                lblParameterCategory.Text = CType(arrParamcat(0), StandardCode).ValueDesc

                Dim lblParameterName As Label = CType(e.Item.FindControl("lblParameterName"), Label)
                lblParameterName.Text = objDomain.ParameterName

                Dim lblIsMandatory As Label = CType(e.Item.FindControl("lblIsMandatory"), Label)
                lblIsMandatory.Text = IIf(objDomain.IsMandatory = "1", "Ya", "Tidak")

                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                Dim imgActif As HtmlImage = CType(e.Item.FindControl("imgActif"), HtmlImage)
                Dim imgNonActif As HtmlImage = CType(e.Item.FindControl("imgNonActif"), HtmlImage)
                If objDomain.Status = 0 Then
                    lblStatus.Text = "Tidak Aktif"
                    imgActif.Visible = False
                    imgNonActif.Visible = True
                Else
                    imgActif.Visible = True
                    imgNonActif.Visible = False
                    lblStatus.Text = "Aktif"
                End If

                Dim lnkbtnView As LinkButton = CType(e.Item.FindControl("lnkbtnView"), LinkButton)
                Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
                Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)

                'add privilige
                lnkbtnView.Visible = displayPriv
                lnkbtnEdit.Visible = editPriv
                lnkbtnDelete.Visible = deletePriv

            End If
        End If
    End Sub

    Private Sub dgListparam_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgListParam.PageIndexChanged
        dgListParam.CurrentPageIndex = e.NewPageIndex
        BindGridListParam(dgListParam.CurrentPageIndex)
        StoreCriteria()
    End Sub

    Private Sub dgListparam_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgListParam.SortCommand
        Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)
            Case Sort.SortDirection.ASC
                ViewState("currentSortDirection") = Sort.SortDirection.DESC

            Case Sort.SortDirection.DESC
                ViewState("currentSortDirection") = Sort.SortDirection.ASC
        End Select

        ViewState("currentSortColumn") = e.SortExpression

        dgListParam.SelectedIndex = -1
        dgListParam.CurrentPageIndex = 0
        BindGridListParam(dgListParam.CurrentPageIndex, e.SortExpression, ViewState("currentSortDirection"))
        StoreCriteria()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If btnSearch.Text = "Batal" Then
            Response.Redirect("~/Babit/FrmInputBabitParameter.aspx")
            Exit Sub
        End If
        dgListParam.CurrentPageIndex = 0
        BindGridListParam(dgListParam.CurrentPageIndex)
        StoreCriteria()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        objBabitParameterHeader = CType(sesHelper.GetSession(sessBabitParameterHeader), BabitParameterHeader)
        If CheckIsExistFromInputBabitByHeader(objBabitParameterHeader, Request.QueryString("Mode")) = False Then
            Exit Sub
        End If

        Dim objBabitParameterHeaderFacade As BabitParameterHeaderFacade = New BabitParameterHeaderFacade(User)
        Dim objBabitParameterDetailFacade As BabitParameterDetailFacade = New BabitParameterDetailFacade(User)
        objBabitParameterHeader = objBabitParameterHeaderFacade.Retrieve(CType(Me.hdnBabitParameterHeaderID.Value, Integer))
        objBabitParameterHeader.RowStatus = -1
        objBabitParameterHeaderFacade.Update(objBabitParameterHeader)

        arrBabitParameterDetail = CType(sesHelper.GetSession(sessBabitParameterDetail), ArrayList)
        For Each obj As BabitParameterDetail In arrBabitParameterDetail
            obj.RowStatus = -1
            objBabitParameterDetailFacade.Update(obj)
        Next

        Response.Redirect("~/Babit/FrmInputBabitParameter.aspx")
    End Sub

    Private Sub ddlIsMandatory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlIsMandatory.SelectedIndexChanged
        If ddlIsMandatory.SelectedValue = "-1" OrElse ddlIsMandatory.SelectedValue = "0" Then
            chkIsMandatory.Checked = False
        Else
            chkIsMandatory.Checked = True
        End If
    End Sub

    Private Sub ddlJenisPengajuan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenisPengajuan.SelectedIndexChanged
        BindBabitMasterEventType()
    End Sub

    Protected Sub btnDownloadExcel_Click(sender As Object, e As EventArgs) Handles btnDownloadExcel.Click
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        Dim crits As CriteriaComposite
        If dgListParam.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        If Not IsNothing(sesHelper.GetSession(SessionCriteriaExcel)) Then
            crits = CType(sesHelper.GetSession(SessionCriteriaExcel), CriteriaComposite)
        End If
        ' mengambil data yang dibutuhkan
        arrData = New BabitParameterHeaderFacade(User).Retrieve(crits)
        If arrData.Count > 0 Then
            CreateExcel("DaftarParameterBabit", arrData)
        End If

    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim oD As Dealer
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = FileName
            ws.Cells("A3").Value = "No"
            ws.Cells("B3").Value = "Nama Parameter"
            ws.Cells("C3").Value = "Jenis Pengajuan"
            ws.Cells("D3").Value = "Mandatory"
            ws.Cells("E3").Value = "Status"
            ws.Cells("F3").Value = "Kategori"

            For i As Integer = 0 To Data.Count - 1
                Dim item As BabitParameterHeader = Data(i)
                ws.Cells(i + 4, 1).Value = i + 1
                ws.Cells(i + 4, 2).Value = item.ParameterName
                Select Case item.BabitMasterEventType.ID
                    Case 1, 2, 3
                        ws.Cells(i + 4, 3).Value = "Babit"
                    Case 4, 5
                        ws.Cells(i + 4, 3).Value = "Event"
                End Select

                Select Case item.IsMandatory
                    Case 0
                        ws.Cells(i + 4, 4).Value = "Tidak"
                    Case 1
                        ws.Cells(i + 4, 4).Value = "Ya"
                End Select

                Select Case item.Status
                    Case -1
                        ws.Cells(i + 4, 5).Value = "Tidak Aktif"
                    Case 1
                        ws.Cells(i + 4, 5).Value = "Aktif"
                End Select

                Dim sCode As ArrayList = New StandardCodeFacade(User).RetrieveByValueId(item.ParameterCategory, "EnumBabit.BabitParameterCategory")
                Dim Category As New StandardCode
                If sCode.Count > 0 Then
                    Category = sCode(0)
                    ws.Cells(i + 4, 6).Value = Category.ValueDesc
                Else
                    ws.Cells(i + 4, 6).Value = ""
                End If
            Next

            CreateExcelFile(pck, FileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xls")
        End Using

    End Sub


    Private Function CreateSheet(pck As ExcelPackage, sheetName As String) As ExcelWorksheet
        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add(sheetName)
        ws.View.ShowGridLines = False
        Return ws
    End Function

    Private Sub CreateExcelFile(pck As ExcelPackage, fileName As String)
        Dim fileBytes = pck.GetAsByteArray()
        Response.Clear()

        'Response.AppendHeader("Content-Length", fileBytes.Length.ToString())
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName)
        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"  'xlsx
        Response.ContentType = "application/vnd.ms-excel" 'xls
        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()

    End Sub


#End Region
End Class
