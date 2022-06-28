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
#End Region

Public Class FrmInputMasterEventType
    Inherits System.Web.UI.Page

#Region "Custom Variables Declaration"

    Dim objDealer As New Dealer
    Dim objLoginUser As UserInfo
    Dim objBabitMasterEventType As BabitMasterEventType
    Dim arrBabitMasterEventType As ArrayList = New ArrayList
    Private sesHelper As New SessionHelper
    Private intBabitMasterEventTypeID As Integer = 0
    Private intItemIndex As Integer = 0
    Private blnEditPriv As Boolean
    Private blnSavePriv As Boolean
    Private blnDeletePriv As Boolean
    Private blnDisplayPriv As Boolean

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
        Me.txtTypeCode.Enabled = isEnb
        Me.ddlFormType.Enabled = isEnb
        Me.txtTypeName.Enabled = isEnb
        Me.ddlStatus.Enabled = isEnb
    End Sub

    Private Sub BindGridListParam(ByVal index As Integer, Optional ByVal sortColoum As String = Nothing, Optional ByVal sortType As Sort.SortDirection = Sort.SortDirection.ASC)
        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterEventType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtTypeCode.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(BabitMasterEventType), "TypeCode", MatchType.[Partial], txtTypeCode.Text.Trim))
        End If
        If txtTypeName.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(BabitMasterEventType), "TypeName", MatchType.Exact, CType(txtTypeName.Text, String)))
        End If
        If ddlStatus.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(BabitMasterEventType), "Status", MatchType.Exact, CType(ddlStatus.SelectedValue, String)))
        End If
        If ddlFormType.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(BabitMasterEventType), "FormType", MatchType.Exact, CType(ddlFormType.SelectedValue, String)))
        End If

        sortColoum = ViewState("currentSortColumn")
        sortType = ViewState("currentSortDirection")
        _arrList = New BabitMasterEventTypeFacade(User).RetrieveActiveList(index + 1, dgBabitMasterEventTypeList.PageSize, totalRow, sortColoum, sortType, criterias)

        Dim arForDisplay As New ArrayList
        arForDisplay.Add(txtTypeCode.Text)
        arForDisplay.Add(txtTypeName.Text)
        arForDisplay.Add(ddlStatus.SelectedValue)
        arForDisplay.Add(ddlFormType.SelectedValue)
        sesHelper.SetSession("SessionForDisplayMasterEventType", arForDisplay)

        dgBabitMasterEventTypeList.VirtualItemCount = totalRow
        dgBabitMasterEventTypeList.DataSource = _arrList
        dgBabitMasterEventTypeList.DataBind()
    End Sub

    Private Sub LoadDataMasterEventType(intBabitMasterEventTypeID As Integer)
        Dim objBabitMasterEventType As BabitMasterEventType

        objBabitMasterEventType = New BabitMasterEventTypeFacade(User).Retrieve(intBabitMasterEventTypeID)
        If Not IsNothing(objBabitMasterEventType) Then
            Me.txtTypeCode.Text = objBabitMasterEventType.TypeCode
            Me.ddlFormType.SelectedValue = objBabitMasterEventType.FormType
            Me.txtTypeName.Text = objBabitMasterEventType.TypeName
            Me.ddlStatus.SelectedValue = objBabitMasterEventType.Status.Trim
            sesHelper.SetSession("sessBabitMasterEventType", objBabitMasterEventType)
        End If
    End Sub

    Private Sub ClearAll()
        hdnBabitMasterEventTypeID.Value = ""
        txtTypeCode.Text = ""
        txtTypeName.Text = ""
        ddlFormType.SelectedIndex = 1
        ddlStatus.SelectedIndex = 1
    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Sub BindddlStatus()
        ddlStatus.Items.Clear()
        ddlStatus.Items.Add(New ListItem("Aktif", 1))
        ddlStatus.Items.Add(New ListItem("Tidak Aktif", 0))
        ddlStatus.DataBind()
        ddlStatus.Items.Insert(0, New ListItem("Silakan Piih ", -1))
        ddlStatus.SelectedIndex = 1
    End Sub

    Sub BindFormType()
        ddlFormType.Items.Clear()
        ddlFormType.DataSource = New StandardCodeFacade(User).RetrieveByCategory("EnumBabit.FormType")
        ddlFormType.DataTextField = "ValueDesc"
        ddlFormType.DataValueField = "ValueId"
        ddlFormType.DataBind()
        ddlFormType.Items.Insert(0, New ListItem("Silakan Piih ", -1))
        ddlFormType.SelectedIndex = 1
    End Sub

    Private Function ValidateData() As String
        Dim sb As StringBuilder = New StringBuilder

        If (txtTypeCode.Text.Trim = String.Empty) Then
            sb.Append("Kode Harus Diisi\n")
        End If

        If (ddlFormType.SelectedValue = String.Empty) Then
            sb.Append("Tipe Form Harus Diisi\n")
        End If
        If (txtTypeName.Text = String.Empty) Then
            sb.Append("Nama Kategori Kegiatan Harus Diisi\n")
        End If
        If (ddlStatus.SelectedValue = "-1") Then
            sb.Append("Status Harus Diisi\n")
        End If

        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitMasterEventType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitMasterEventType), "TypeCode", MatchType.Exact, Me.txtTypeCode.Text.Trim))
        criterias.opAnd(New Criteria(GetType(BabitMasterEventType), "FormType", MatchType.Exact, Me.ddlFormType.SelectedValue))
        If Request.QueryString("Mode") = "Edit" Then
            criterias.opAnd(New Criteria(GetType(BabitMasterEventType), "ID", MatchType.No, Me.hdnBabitMasterEventTypeID.Value))
        End If
        Dim arrBabitMasterEventType As ArrayList = New BabitMasterEventTypeFacade(User).Retrieve(criterias)
        If Not IsNothing(arrBabitMasterEventType) AndAlso arrBabitMasterEventType.Count > 0 Then
            sb.Append("Kode kategori sudah ada. Silahkan masukkan kode kategori baru\n")
        End If

        Return sb.ToString()
    End Function

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If Not SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Tipe_Babit_Display_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=BABIT - INPUT JENIS KEGIATAN")
            End If
        Else
            ' case from dealer
            If Not SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Tipe_Babit_Display_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=BABIT - INPUT JENIS KEGIATAN")
            End If
        End If

        blnDisplayPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Tipe_Babit_Display_Privilege)
        blnEditPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Tipe_Babit_Edit_Privilege)
        blnSavePriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Tipe_Babit_Simpan_Privilege)
        blnDeletePriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Tipe_Babit_Delete_Privilege)
    End Sub

#End Region

#Region "Event Handlers"

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        objDealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        objLoginUser = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        Dim intId As Integer = 0
        If (Not IsPostBack) Then
            BindFormType()
            BindddlStatus()

            ViewState("currentSortColumn") = "TypeName"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC

            BindGridListParam(dgBabitMasterEventTypeList.CurrentPageIndex)
            btnDelete.Visible = blnDeletePriv
            btnBaru.Visible = True
            btnSave.Visible = blnSavePriv

            If Not IsNothing(Request.QueryString("Mode")) AndAlso CType(Request.QueryString("Mode"), String).Trim <> "" Then
                If Not IsNothing(Request.QueryString("BabitMasterEventTypeID")) Then
                    hdnBabitMasterEventTypeID.Value = Request.QueryString("BabitMasterEventTypeID")
                    LoadDataMasterEventType(hdnBabitMasterEventTypeID.Value)
                End If

                btnBaru.Visible = False
                btnSave.Visible = blnSavePriv
                btnDelete.Visible = blnDeletePriv
                btnSearch.Visible = True
                btnSearch.Text = "Batal"
                If Request.QueryString("Mode") = "View" Then
                    disableform(False)
                    btnSave.Visible = False
                ElseIf Request.QueryString("Mode") = "Edit" Then
                    disableform(True)
                    btnSave.Visible = blnSavePriv
                ElseIf Request.QueryString("Mode") = "Delete" Then
                    disableform(False)
                    btnSave.Visible = False
                    btnDelete.Visible = blnDeletePriv
                    btnDelete_Click(Nothing, Nothing)
                End If
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim str As String
        str = ValidateData()
        If (str.Length > 0) Then
            MessageBox.Show(str)
            Exit Sub
        End If

        Dim _BabitMasterEventType As BabitMasterEventType
        If Request.QueryString("Mode") <> "Edit" Then
            _BabitMasterEventType = New BabitMasterEventType
        Else
            _BabitMasterEventType = CType(sesHelper.GetSession("sessBabitMasterEventType"), BabitMasterEventType)
        End If
        _BabitMasterEventType.TypeCode = txtTypeCode.Text
        _BabitMasterEventType.TypeName = Me.txtTypeName.Text
        _BabitMasterEventType.FormType = Me.ddlFormType.SelectedValue
        _BabitMasterEventType.Status = Me.ddlStatus.SelectedValue

        Dim _result As Integer = 0
        If Request.QueryString("Mode") = "Edit" Then
            _result = New BabitMasterEventTypeFacade(User).Update(_BabitMasterEventType)
        Else
            _result = New BabitMasterEventTypeFacade(User).Insert(_BabitMasterEventType)
        End If

        If _result > 0 Then
            ClearAll()
            MessageBox.Show("Simpan Data Berhasil !")
            Server.Transfer("~/Babit/FrmInputMasterEventType.aspx?BabitMasterEventTypeID=" & _result)
        Else
            MessageBox.Show("Simpan Data Gagal")
        End If
    End Sub

    Private Sub btnBaru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBaru.Click
        Me.ClearAll()
    End Sub

    Private Sub dgBabitMasterEventTypeList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgBabitMasterEventTypeList.ItemCommand
        Select Case e.CommandName
            Case "View"
                Response.Redirect("~/Babit/FrmInputMasterEventType.aspx?Mode=View&BabitMasterEventTypeID=" & CInt(e.CommandArgument))
            Case "Edit"
                Response.Redirect("~/Babit/FrmInputMasterEventType.aspx?Mode=Edit&BabitMasterEventTypeID=" & CInt(e.CommandArgument))
            Case "Delete"
                Response.Redirect("~/Babit/FrmInputMasterEventType.aspx?Mode=Delete&BabitMasterEventTypeID=" & CInt(e.CommandArgument))
        End Select
    End Sub

    Private Sub dgBabitMasterEventTypeList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgBabitMasterEventTypeList.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            Dim objDomain As BabitMasterEventType = CType(e.Item.DataItem, BabitMasterEventType)

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (e.Item.ItemIndex + 1 + (dgBabitMasterEventTypeList.CurrentPageIndex * dgBabitMasterEventTypeList.PageSize)).ToString()

            Dim lblTypeCode As Label = CType(e.Item.FindControl("lblTypeCode"), Label)
            lblTypeCode.Text = objDomain.TypeCode

            Dim lblTypeName As Label = CType(e.Item.FindControl("lblTypeName"), Label)
            lblTypeName.Text = objDomain.TypeName

            Dim lblFormType As Label = CType(e.Item.FindControl("lblFormType"), Label)
            Dim arrFormType As ArrayList = New StandardCodeFacade(User).RetrieveByValueId(objDomain.FormType, "EnumBabit.FormType")
            lblFormType.Text = CType(arrFormType(0), StandardCode).ValueDesc

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
            lnkbtnView.Visible = blnDisplayPriv
            lnkbtnEdit.Visible = blnEditPriv
            lnkbtnDelete.Visible = blnDeletePriv

        End If
    End Sub

    Private Sub dgBabitMasterEventTypeList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgBabitMasterEventTypeList.PageIndexChanged
        dgBabitMasterEventTypeList.CurrentPageIndex = e.NewPageIndex
        BindGridListParam(dgBabitMasterEventTypeList.CurrentPageIndex)
    End Sub

    Private Sub dgBabitMasterEventTypeList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgBabitMasterEventTypeList.SortCommand
        Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)
            Case Sort.SortDirection.ASC
                ViewState("currentSortDirection") = Sort.SortDirection.DESC

            Case Sort.SortDirection.DESC
                ViewState("currentSortDirection") = Sort.SortDirection.ASC
        End Select

        ViewState("currentSortColumn") = e.SortExpression

        dgBabitMasterEventTypeList.SelectedIndex = -1
        dgBabitMasterEventTypeList.CurrentPageIndex = 0
        BindGridListParam(dgBabitMasterEventTypeList.CurrentPageIndex, e.SortExpression, ViewState("currentSortDirection"))
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If btnSearch.Text = "Batal" Then
            Response.Redirect("~/Babit/FrmInputMasterEventType.aspx")
            Exit Sub
        End If
        dgBabitMasterEventTypeList.CurrentPageIndex = 0
        BindGridListParam(dgBabitMasterEventTypeList.CurrentPageIndex)
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        '----- Proses Validasi Delete
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitHeader), "BabitMasterEventType.ID", MatchType.Exact, Me.hdnBabitMasterEventTypeID.Value))
        Dim arrBabitHeader As ArrayList = New BabitHeaderFacade(User).Retrieve(criterias)
        If Not IsNothing(arrBabitHeader) AndAlso arrBabitHeader.Count > 0 Then
            MessageBox.Show("Kode Kegiatan ini sudah dipakai di Daftar Pengajuan Babit")
            Exit Sub
        End If

        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(BabitParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.ID", MatchType.Exact, Me.hdnBabitMasterEventTypeID.Value))
        Dim arrBabitParameterHeader As ArrayList = New BabitParameterHeaderFacade(User).Retrieve(criterias2)
        If Not IsNothing(arrBabitParameterHeader) AndAlso arrBabitParameterHeader.Count > 0 Then
            MessageBox.Show("Kode Kegiatan ini sudah dipakai di Daftar Parameter Babit")
            Exit Sub
        End If
        '--------------------------

        Dim objBabitMasterEventTypeFacade As BabitMasterEventTypeFacade = New BabitMasterEventTypeFacade(User)
        Dim objBabitMasterEventType As BabitMasterEventType = objBabitMasterEventTypeFacade.Retrieve(CType(Me.hdnBabitMasterEventTypeID.Value, Integer))
        objBabitMasterEventType.RowStatus = -1
        objBabitMasterEventTypeFacade.Update(objBabitMasterEventType)

        Response.Redirect("~/Babit/FrmInputMasterEventType.aspx")
    End Sub

#End Region

End Class
