Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.Collections.Generic
Imports System.Linq
Imports System.IO
Imports System.Text

Public Class FrmSPLHeader
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSPLNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblCustName As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSPLNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustName As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblColon4 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dgSPLHeader As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtBerlakuPada As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    ' Protected WithEvents lbtnNew As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lboxStatusPersetujuan As System.Web.UI.WebControls.ListBox
    Protected WithEvents txtDiscountCategory As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpCategoryDiscount As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatusProses As System.Web.UI.WebControls.DropDownList
    Protected WithEvents hdnDiscountMasterID As System.Web.UI.WebControls.HiddenField
    Protected WithEvents btnProses As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents btnTransfer As System.Web.UI.WebControls.Button

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
    Private _SPLFacade As New SPLFacade(User)
    Private _create As Boolean
    Private _edit As Boolean
    Private _view As Boolean
    Private _download As Boolean
    Private sessHelper As New SessionHelper
    Private _sesCriteria As String = "SPLCriterias"
    Private Shared _arrayStatusPersetujuan As List(Of ListItem)
    Private Shared Property ArrayStatusPersetujuan() As List(Of ListItem)
        Get
            Return _arrayStatusPersetujuan
        End Get
        Set(ByVal value As List(Of ListItem))
            _arrayStatusPersetujuan = value
        End Set
    End Property
#End Region

#Region "PrivateCustomMethods"
    Private Property SesDealer() As Dealer
        Get
            Return CType(sessHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sessHelper.SetSession("DEALER", Value)
        End Set
    End Property

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Private Function IsValidToDelete(ByVal idSPL As Integer) As Boolean
        Dim IsValid As Boolean = True
        Dim arrList As New ArrayList
        Dim ObjSPL As SPL = _SPLFacade.Retrieve(idSPL)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PKHeader), "SPLNumber", MatchType.Exact, ObjSPL.SPLNumber))
        arrList = New PKHeaderFacade(User).Retrieve(criterias)
        If arrList.Count > 0 Then
            IsValid = False
            Return IsValid
        End If

        criterias = New CriteriaComposite(New Criteria(GetType(ContractHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ContractHeader), "SPLNumber", MatchType.Exact, ObjSPL.SPLNumber))
        arrList = New ContractHeaderFacade(User).Retrieve(criterias)
        If arrList.Count > 0 Then
            IsValid = False
            Return IsValid
        End If

        Return IsValid
    End Function
    Private Function CreateCriterias(ByVal criterias As CriteriaComposite)
        If txtSPLNumber.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(SPL), "SPLNumber", MatchType.[Partial], txtSPLNumber.Text.Trim()))
        End If
        If txtDealerName.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(SPL), "DealerName", MatchType.[Partial], txtDealerName.Text.Trim()))
        End If
        If txtCustName.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(SPL), "CustomerName", MatchType.[Partial], txtCustName.Text.Trim()))
        End If
        If txtBerlakuPada.Text.Length > 0 Then
            Try
                Dim tanggal As New DateTime(CInt(txtBerlakuPada.Text.Substring(2, 4)), CInt(txtBerlakuPada.Text.Substring(0, 2)), 1)
                criterias.opAnd(New Criteria(GetType(SPL), "ValidFrom", MatchType.LesserOrEqual, tanggal.ToString("yyyy-MM-dd")))
                criterias.opAnd(New Criteria(GetType(SPL), "ValidTo", MatchType.GreaterOrEqual, tanggal.ToString("yyyy-MM-dd")))
            Catch ex As Exception

            End Try
        End If

        Dim strSQL As String = String.Empty
        If hdnDiscountMasterID.Value.Trim <> "" Then
            strSQL = "Select distinct a.ID from SPL a join SPLDetail b on a.ID = b.SPLID and b.RowStatus = 0 "
            strSQL += "join SPLDetailtoSPL c on b.ID = c.SPLDetailID and c.RowStatus = 0 "
            strSQL += "where a.RowStatus = 0 And c.DiscountMasterID = " & hdnDiscountMasterID.Value
            criterias.opAnd(New Criteria(GetType(SPL), "ID", MatchType.InSet, "(" & strSQL & ")"))
        End If

        Dim sStatusPersetujuanCollection As String = ""
        Dim critStatus As Integer() = lboxStatusPersetujuan.GetSelectedIndices()
        If critStatus.Length > 1 Then
            'sStatusPersetujuanCollection = "(" + String.Join(",", critStatus) + ")"
            sStatusPersetujuanCollection = getSelectedValuesFromListBox(lboxStatusPersetujuan)
            criterias.opAnd(New Criteria(GetType(SPL), "ApprovalStatus", MatchType.InSet, "(" & sStatusPersetujuanCollection & ")"))
        Else
            If critStatus.Length = 1 Then
                sStatusPersetujuanCollection = getSelectedValuesFromListBox(lboxStatusPersetujuan)
                criterias.opAnd(New Criteria(GetType(SPL), "ApprovalStatus", MatchType.Exact, sStatusPersetujuanCollection))
            End If
        End If

        criterias.opAnd(New Criteria(GetType(SPL), "Status", MatchType.Exact, CInt(ddlStatus.SelectedValue)))
        SaveCriteria()
        Return criterias
    End Function

    Public Shared Function getSelectedValuesFromListBox(ByVal objListBox As ListBox) As String
        Dim listOfIndices As List(Of Integer) = objListBox.GetSelectedIndices().ToList()
        Dim values As String = String.Empty

        For Each indice As Integer In listOfIndices
            values &= "," & objListBox.Items(indice).Value
        Next indice
        If Not String.IsNullorEmpty(values) Then
            values = values.Substring(1)
        End If
        Return values
    End Function

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        arrList = _SPLFacade.RetrieveByCriteria(CType(sessHelper.GetSession("CRITERIAS"), CriteriaComposite), idxPage + 1, dgSPLHeader.PageSize, totalRow, _
    CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        If arrList.Count > 0 Then
            dgSPLHeader.CurrentPageIndex = idxPage
        End If

        dgSPLHeader.DataSource = arrList
        dgSPLHeader.VirtualItemCount = totalRow
        dgSPLHeader.DataBind()
    End Sub
    Private Sub BindDdlStatus()
        ddlStatus.Items.Clear()
        ddlStatus.DataSource = EnumStatusSPL.RetrieveStatus
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataBind()

        StatusPersetujuan()
        StatusProses()

    End Sub
    Private Sub StatusPersetujuan()
        Dim arrList As ArrayList = New ArrayList
        Dim list As List(Of ListItem) = New List(Of ListItem)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumSPLStatus.Status"))
        arrList = New StandardCodeFacade(User).Retrieve(criterias)
        lboxStatusPersetujuan.Items.Clear()
        ArrayStatusPersetujuan = New List(Of ListItem)
        For Each i As StandardCode In arrList
            If i.ValueId <> 2 Then
                lboxStatusPersetujuan.Items.Add(New ListItem(i.ValueDesc, i.ValueId))
            End If
            ArrayStatusPersetujuan.Add(New ListItem(i.ValueDesc, i.ValueId))
        Next
    End Sub
    Private Sub StatusProses()
        ddlStatusProses.Items.Add(New ListItem("Konfirmasi", 1))
        ddlStatusProses.Items.Add(New ListItem("Batal Konfirmasi", 2))
        ddlStatusProses.Items.Add(New ListItem("Tolak", 6))
    End Sub
    Private Sub Initialize()
        txtSPLNumber.Text = ""
        txtDealerName.Text = ""
        txtCustName.Text = ""
        txtBerlakuPada.Text = Format(DateTime.Now, "MMyyyy")
        BindDdlStatus()
        ViewState("CurrentSortColumn") = "SPLNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ViewState("idxPage") = 0
    End Sub
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.ENHPKDaftarAplikasiLihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=UMUM - Daftar SPL")
        End If

        _create = SecurityProvider.Authorize(Context.User, SR.ENHPKDaftarAplikasiBuat_Privilege)
        _edit = SecurityProvider.Authorize(Context.User, SR.ENHPKDaftarAplikasiUbah_Privilege)
        _view = SecurityProvider.Authorize(Context.User, SR.ENHPKDaftarAplikasiDetail_Privilege)
        _download = SecurityProvider.Authorize(Context.User, SR.ENHPKDaftarAplikasiDownload_Privilege)


        btnSearch.Visible = _view

    End Sub

    Private Sub REstoreCriteria()
        If Not IsNothing(sessHelper.GetSession(_sesCriteria)) Then
            Dim ObjCriteria As New ArrayList
            ObjCriteria = CType(sessHelper.GetSession(_sesCriteria), ArrayList)
            txtSPLNumber.Text = ObjCriteria(0).ToString()
            txtDealerName.Text = ObjCriteria(1).ToString()
            txtCustName.Text = ObjCriteria(2).ToString()
            ddlStatus.SelectedValue = ObjCriteria(3).ToString()
            txtBerlakuPada.Text = ObjCriteria(4).ToString()
            ViewState("idxPage") = CInt(ObjCriteria(5))
            ViewState("CurrentSortColumn") = ObjCriteria(6)
            ViewState("CurrentSortDirect") = ObjCriteria(7)
            'sessHelper.RemoveSession(_sesCriteria)

        End If

    End Sub

    Private Sub SaveCriteria()
        Dim ObjCriteria As New ArrayList
        ObjCriteria.Add(txtSPLNumber.Text.Trim())
        ObjCriteria.Add(txtDealerName.Text.Trim())
        ObjCriteria.Add(txtCustName.Text.Trim())
        ObjCriteria.Add(ddlStatus.SelectedValue)
        ObjCriteria.Add(txtBerlakuPada.Text.Trim())

        ObjCriteria.Add(ViewState("idxPage"))
        ObjCriteria.Add(ViewState("CurrentSortColumn"))
        ObjCriteria.Add(ViewState("CurrentSortDirect"))
        sessHelper.SetSession(_sesCriteria, ObjCriteria)
    End Sub

    Private Sub SaveCriteria2()
        If Not IsNothing(sessHelper.GetSession(_sesCriteria)) Then
            Dim ObjCriteria As New ArrayList
            ObjCriteria = CType(sessHelper.GetSession(_sesCriteria), ArrayList)
            'ObjCriteria.Add(txtSPLNumber.Text.Trim())
            'ObjCriteria.Add(txtDealerName.Text.Trim())
            'ObjCriteria.Add(txtCustName.Text.Trim())
            'ObjCriteria.Add(ddlStatus.SelectedValue)
            'ObjCriteria.Add(txtBerlakuPada.Text.Trim())

            ObjCriteria(5) = dgSPLHeader.CurrentPageIndex
            ObjCriteria(6) = ViewState("CurrentSortColumn") '"SPLNumber"
            ObjCriteria(7) = ViewState("CurrentSortDirect")
            sessHelper.SetSession(_sesCriteria, ObjCriteria)
        End If

    End Sub

    Private Sub DoTransfer()
        Dim checkChecked As Boolean = False
        Dim arrSPLToProcess As ArrayList = New ArrayList()
        Dim arrSPLOldStatus As ArrayList = New ArrayList()
        Dim result As Integer = -1
        Dim newApprovalStatus As Integer = 3 '--- Status Proses

        For i As Integer = 0 To dgSPLHeader.Items.Count - 1
            If CType(dgSPLHeader.Items(i).FindControl("cbxDetail"), CheckBox).Checked Then
                checkChecked = True
                Dim dgi As DataGridItem = dgSPLHeader.Items(i)
                Dim _id As Integer = CType(dgi.Cells(1).Text, Integer)
                Dim objNew As SPL = New SPLFacade(User).Retrieve(_id)

                Dim objOld As SPL = New SPL
                With objOld
                    .ID = objNew.ID
                    .DealerName = objNew.DealerName
                    .SPLNumber = objNew.SPLNumber
                    .CustomerName = objNew.CustomerName
                    .Description = objNew.Description
                    .ValidFrom = objNew.ValidFrom
                    .ValidTo = objNew.ValidTo
                    .Attachment = objNew.Attachment
                    .NumOfInstallment = objNew.NumOfInstallment
                    .MaxTOPDay = objNew.MaxTOPDay
                    .Status = objNew.Status
                    .IsAutoApprovedDealer = objNew.IsAutoApprovedDealer
                    .ApprovalStatus = objNew.ApprovalStatus
                    .FinalApproval = objNew.FinalApproval
                    .Comment = objNew.Comment
                End With
                arrSPLOldStatus.Add(objOld)

                objNew.ApprovalStatus = newApprovalStatus
                arrSPLToProcess.Add(objNew)
            End If
        Next
        If checkChecked = False Then
            MessageBox.Show("Check list data minimal satu")
            Return
        End If

        Dim list As List(Of SPL) = arrSPLOldStatus.Cast(Of SPL)().ToList()
        Dim listStatus As List(Of Integer) = list.Select(Function(i) i.ApprovalStatus).GroupBy(Function(g) g).Select(Function(x) x.Key).ToList()

        Dim strSPLNoValid As String = String.Empty
        Dim strSPLNoNotValid As String = String.Empty
        If newApprovalStatus = 3 Then   '--- Status Proses
            If listStatus.Count > 0 Then
                Dim arrSPLOldStatusTemp As ArrayList = _
                            New System.Collections.ArrayList(
                                (From obj As SPL In arrSPLOldStatus.OfType(Of SPL)()
                                    Order By obj.ApprovalStatus
                                    Select obj).ToList())

                '0 = Status Baru
                '1 = Status Konfirmasi
                '7 = Status Revisi
                For Each oSPL As SPL In arrSPLOldStatusTemp
                    If oSPL.ApprovalStatus <> 0 _
                        AndAlso oSPL.ApprovalStatus <> 1 _
                        AndAlso oSPL.ApprovalStatus <> 7 Then
                        If strSPLNoNotValid = "" Then
                            strSPLNoNotValid = oSPL.SPLNumber
                        Else
                            strSPLNoNotValid += ", " & oSPL.SPLNumber
                        End If
                    Else
                        If strSPLNoValid = "" Then
                            strSPLNoValid = oSPL.SPLNumber
                        Else
                            strSPLNoValid += ", " & oSPL.SPLNumber
                        End If
                    End If
                Next

                Dim strMessageValid As String = String.Empty
                Dim strMessageNotValid As String = String.Empty
                If strSPLNoNotValid.Trim <> "" Then
                    strMessageNotValid = "Nomor SPL : " & strSPLNoNotValid & " statusnya bukan Baru, Konfirmasi atau Revisi"
                End If
                If strSPLNoValid.Trim <> "" Then
                    strMessageValid = "Nomor SPL : " & strSPLNoValid & " akan di Transfer ke Groupware"
                End If
                If strSPLNoValid.Trim <> "" OrElse strSPLNoNotValid.Trim <> "" Then
                    MessageBox.Show(strMessageValid & If(strMessageNotValid.Trim <> "", "\n", "") & strMessageNotValid)
                End If
                If strSPLNoValid.Trim = "" Then
                    Return
                End If
            Else
                MessageBox.Show("Tidak ada Status yang Valid")
                Return
            End If
        End If

        result = New SPLFacade(User).UpdateStatus(arrSPLToProcess, arrSPLOldStatus)
        If result = -1 Then
            MessageBox.Show("Update Status gagal")
            Return
        End If
        MessageBox.Show("Update Status berhasil")
        BindDataGrid(0)
    End Sub

    Private Sub DoProcess()
        Dim checkChecked As Boolean = False
        Dim arrSPLToProcess As ArrayList = New ArrayList()
        Dim arrSPLOldStatus As ArrayList = New ArrayList()
        Dim result As Integer = -1
        Dim newApprovalStatus As Integer = CType(ddlStatusProses.SelectedValue, Integer)

        For i As Integer = 0 To dgSPLHeader.Items.Count - 1
            If CType(dgSPLHeader.Items(i).FindControl("cbxDetail"), CheckBox).Checked Then
                checkChecked = True
                Dim dgi As DataGridItem = dgSPLHeader.Items(i)
                Dim _id As Integer = CType(dgi.Cells(1).Text, Integer)
                Dim objNew As SPL = New SPLFacade(User).Retrieve(_id)
                Dim objOld As SPL = New SPL
                With objOld
                    .ID = objNew.ID
                    .DealerName = objNew.DealerName
                    .SPLNumber = objNew.SPLNumber
                    .CustomerName = objNew.CustomerName
                    .Description = objNew.Description
                    .ValidFrom = objNew.ValidFrom
                    .ValidTo = objNew.ValidTo
                    .Attachment = objNew.Attachment
                    .NumOfInstallment = objNew.NumOfInstallment
                    .MaxTOPDay = objNew.MaxTOPDay
                    .Status = objNew.Status
                    .IsAutoApprovedDealer = objNew.IsAutoApprovedDealer
                    .ApprovalStatus = objNew.ApprovalStatus
                    .FinalApproval = objNew.FinalApproval
                    .Comment = objNew.Comment
                End With
                arrSPLOldStatus.Add(objOld)
                Select Case newApprovalStatus
                    Case 2
                        objNew.ApprovalStatus = 0
                    Case Else
                        objNew.ApprovalStatus = newApprovalStatus
                End Select
                arrSPLToProcess.Add(objNew)
            End If
        Next
        If checkChecked = False Then
            MessageBox.Show("Check list data minimal satu")
            Return
        End If

        Dim list As List(Of SPL) = arrSPLOldStatus.Cast(Of SPL)().ToList()
        Dim listStatus As List(Of Integer) = list.Select(Function(i) i.ApprovalStatus).GroupBy(Function(g) g).Select(Function(x) x.Key).ToList()
        If listStatus.Count > 0 Then
            'Status Baru dan Status Konfirmasi dan Status Revisi
            Dim strDescStatusAll As String = ""
            For Each intApprovalStatus As Integer In listStatus
                If intApprovalStatus <> 0 And
                   intApprovalStatus <> 1 And
                   intApprovalStatus <> 7 Then
                    Dim strDescStatus As String = New StandardCodeFacade(User).GetByCategoryValue("EnumSPLStatus.Status", intApprovalStatus).ValueDesc
                    If strDescStatusAll = "" Then
                        strDescStatusAll = "[" & strDescStatus & "]"
                    Else
                        strDescStatusAll += ", [" & strDescStatus & "]"
                    End If
                End If
            Next
            If strDescStatusAll <> "" Then
                MessageBox.Show("Status " & strDescStatusAll & " tidak bisa di rubah")
                Return
            End If
        Else
            MessageBox.Show("Tidak ada Status yang Valid")
            Return
        End If

        Dim _statusValidasi As String = String.Empty
        Dim strResult As String = String.Empty

        If newApprovalStatus = 1 Then               '--- Status Konfirmasi
            _statusValidasi = "0"       ' Status Baru
        ElseIf newApprovalStatus = 2 Then           '--- Status Batal Konfirmasi
            _statusValidasi = "1"       ' Status Konfirmasi
        ElseIf newApprovalStatus = 3 Then           '--- Status Proses
            _statusValidasi = "1"       ' Status Konfirmasi
        ElseIf newApprovalStatus = 6 Then           '--- Status Tolak
            _statusValidasi = "1"       ' Status Konfirmasi
        End If

        Dim isSuccessUpdate As Boolean = False
        strResult = CekStatusValidation(arrSPLOldStatus, arrSPLToProcess, _statusValidasi, newApprovalStatus.ToString, isSuccessUpdate)
        If strResult <> "" Then
            MessageBox.Show(strResult)
        End If
        If strResult = "" OrElse isSuccessUpdate = False Then
            Return
        End If

        result = New SPLFacade(User).UpdateStatus(arrSPLToProcess, arrSPLOldStatus)
        If result = -1 Then
            MessageBox.Show("Update Status gagal")
            Return
        End If
        MessageBox.Show("Update Status berhasil")
        BindDataGrid(0)
    End Sub

    Function CekStatusValidation(ByRef arrSPLOldStatus As ArrayList, ByRef arrSPLToProcess As ArrayList, ByVal _statusValidasi As String, ByVal _statusNew As String, ByRef isSuccessUpdate As Boolean) As String
        Dim strResult As String = String.Empty
        Dim strSPLNumberValid As String = String.Empty
        Dim strSPLNumberNotValid As String = String.Empty
        arrSPLOldStatus = _
                    New System.Collections.ArrayList(
                        (From obj As SPL In arrSPLOldStatus.OfType(Of SPL)()
                            Order By obj.ID, obj.ApprovalStatus
                            Select obj).ToList())

        arrSPLToProcess = _
                    New System.Collections.ArrayList(
                        (From obj As SPL In arrSPLToProcess.OfType(Of SPL)()
                            Order By obj.ID, obj.ApprovalStatus
                            Select obj).ToList())

        For Each oSPL As SPL In arrSPLOldStatus
            If InStr(_statusValidasi.Replace(";", ""), oSPL.ApprovalStatus) = 0 Then
                If strSPLNumberNotValid = "" Then
                    strSPLNumberNotValid = oSPL.SPLNumber
                Else
                    strSPLNumberNotValid += ", " & oSPL.SPLNumber
                End If
            Else
                If strSPLNumberValid = "" Then
                    strSPLNumberValid = oSPL.SPLNumber
                Else
                    strSPLNumberValid += ", " & oSPL.SPLNumber
                End If
            End If
        Next
        For j As Integer = arrSPLOldStatus.Count - 1 To 0 Step -1
            Dim obj As SPL = CType(arrSPLOldStatus(j), SPL)
            If strSPLNumberNotValid.Trim <> "" Then
                For Each regNo As String In strSPLNumberNotValid.Split(",")
                    If obj.SPLNumber.Trim() = regNo.Trim() Then
                        arrSPLOldStatus.Remove(obj)
                        arrSPLToProcess.RemoveAt(j)
                        Exit For
                    End If
                Next
            End If
        Next

        Dim strStatusName As String = String.Empty
        Dim strWarningStatus As String = String.Empty
        For Each strStatusID As String In _statusValidasi.Split(";")
            strStatusName = CType(New StandardCodeFacade(User).RetrieveByValueId(strStatusID, "EnumSPLStatus.Status")(0), StandardCode).ValueDesc
            If strWarningStatus = "" Then
                strWarningStatus = "[" & strStatusName & "]"
            Else
                strWarningStatus += " atau [" & strStatusName & "]"
            End If
        Next

        Dim strMessageValid As String = String.Empty
        Dim strMessageNotValid As String = String.Empty
        If strSPLNumberNotValid.Trim <> "" Then
            strMessageNotValid = "No Aplikasi : " & strSPLNumberNotValid & " statusnya bukan " & strWarningStatus
        End If
        If strSPLNumberValid.Trim <> "" Then
            strStatusName = CType(New StandardCodeFacade(User).RetrieveByValueId(_statusNew, "EnumSPLStatus.Status")(0), StandardCode).ValueDesc
            strMessageValid = "No Aplikasi : " & strSPLNumberValid & " selanjutnya akan di update ke Status " & strStatusName
        End If
        If strSPLNumberValid.Trim <> "" AndAlso strSPLNumberNotValid.Trim <> "" Then
            strResult = strMessageValid & "\n" & strMessageNotValid
            isSuccessUpdate = True
        ElseIf strSPLNumberValid.Trim = "" AndAlso strSPLNumberNotValid.Trim <> "" Then
            strResult = strMessageNotValid
            isSuccessUpdate = False
        ElseIf strSPLNumberValid.Trim <> "" AndAlso strSPLNumberNotValid.Trim = "" Then
            strResult = strMessageValid
            isSuccessUpdate = True
        End If

        Return strResult
    End Function

    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "DaftarAplikasi_" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls

        Dim fileTemp As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim directoryTemp As String = Server.MapPath("") & "\..\DataTemp\"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        imp.Start()
        Try

            Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(directoryTemp)

            If Not directoryInfo.Exists Then
                directoryInfo.Create()
            End If

            Dim finfo As FileInfo = New FileInfo(fileTemp)
            If finfo.Exists Then
                finfo.Delete()
            End If

            Dim fs As FileStream = New FileStream(fileTemp, FileMode.CreateNew)
            Dim sw As StreamWriter = New StreamWriter(fs)

            WriteDownloadFileData(sw, data)

            sw.Close()
            fs.Close()

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub
    Private Sub WriteDownloadFileData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim sbHeader As StringBuilder = New StringBuilder
        Dim sbDetail As StringBuilder = New StringBuilder
        If Not IsNothing(data) Then
            sbHeader.Append("No" & tab)
            sbHeader.Append("Nomor Aplikasi" & tab)
            sbHeader.Append("Nama Customer" & tab)
            sbHeader.Append("Periode" & tab)
            sbHeader.Append("Status SPL" & tab)
            sbHeader.Append("Status Persetujuan" & tab)
            sw.WriteLine(sbHeader.ToString())

            Dim _no As Integer = 1
            For Each item As SPL In data
                Dim statusSPL = If(item.Status = 0, "Aktif", "Tidak Aktif")
                Dim objListItemStatusPersetujuan As ListItem = (From i As ListItem In ArrayStatusPersetujuan Where CType(i.Value, Short) = item.ApprovalStatus).FirstOrDefault()

                sbDetail.Clear()
                sbDetail.Append(_no & tab)
                sbDetail.Append(item.SPLNumber & tab)
                sbDetail.Append(item.CustomerName & tab)
                sbDetail.Append(Format(item.ValidFrom, "MMM yyyy") & " s/d " & Format(item.ValidTo, "MMM yyyy") & tab)
                sbDetail.Append(statusSPL & tab)
                sbDetail.Append(objListItemStatusPersetujuan.Text & tab)
                sw.WriteLine(sbDetail.ToString())

                _no += 1
            Next
        End If
    End Sub
#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblPopUpCategoryDiscount.Attributes("onclick") = "ShowPPCategoryDiscountSelection();"
        CheckPrivilege()
        If Not IsPostBack Then
            Initialize()
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            REstoreCriteria()

            CreateCriterias(criterias)
            sessHelper.SetSession("CRITERIAS", criterias)

            If Not IsNothing(sessHelper.GetSession(_sesCriteria)) Then
                BindDataGrid(CInt(ViewState("idxPage")))
            Else
                BindDataGrid(0)
            End If
            'sessHelper.RemoveSession(_sesCriteria)


        End If
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        sessHelper.SetSession("Status", "Insert")
        Response.Redirect("FrmSPLDetail.aspx")
    End Sub
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriterias(criterias)
        sessHelper.SetSession("CRITERIAS", criterias)
        dgSPLHeader.CurrentPageIndex = 0
        BindDataGrid(0)
    End Sub
    Private Sub dgSPLHeader_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSPLHeader.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSPL As SPL = e.Item.DataItem
            Dim lblPeriode As Label = e.Item.FindControl("lblPeriode")
            lblPeriode.Text = Format(objSPL.ValidFrom, "MMM yyyy") & " s/d " & Format(objSPL.ValidTo, "MMM yyyy")
            Dim lblFasilitasTOP As Label = e.Item.FindControl("lblFasilitasTOP")
            Dim lbtnDealer As Label = e.Item.FindControl("lbtnDealer")
            lbtnDealer.Attributes("onclick") = "showPopUp('../General/../PopUp/PopUpDealerInSPL.aspx?SPLID= " & Integer.Parse(e.Item.Cells(1).Text) & "','',500,600,'');"
            e.Item.Cells(2).Text = e.Item.ItemIndex + 1 + (dgSPLHeader.CurrentPageIndex * dgSPLHeader.PageSize)
            Dim lbtnVw As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            Dim lbtnEd As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            'Dim lbtnDel As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim lbtnDown As LinkButton = CType(e.Item.FindControl("lbtnDownload"), LinkButton)

            Dim lblStatusSPL As Label = CType(e.Item.FindControl("lblStatusSPL"), Label)
            lblStatusSPL.Text = If(objSPL.Status = 0, "Aktif", "Tidak Aktif")

            Dim objListItemStatusPersetujuan As ListItem = (From i As ListItem In ArrayStatusPersetujuan Where CType(i.Value, Short) = objSPL.ApprovalStatus).FirstOrDefault()
            Dim lblStatusPersetujuan As Label = CType(e.Item.FindControl("lblStatusPersetujuan"), Label)
            lblStatusPersetujuan.Text = objListItemStatusPersetujuan.Text

            Dim isEnabledEdit As Boolean = _edit
            If Not IsLoginAsDealer() Then
                'Status Baru, Konfirmasi, Revisi
                If objSPL.ApprovalStatus <> 0 AndAlso _
                    objSPL.ApprovalStatus <> 1 AndAlso _
                    objSPL.ApprovalStatus <> 5 AndAlso _
                    objSPL.ApprovalStatus <> 7 Then

                    isEnabledEdit = False
                End If
            End If

            lbtnVw.Visible = _view
            lbtnEd.Visible = isEnabledEdit
            'lbtnDel.Visible = _edit
            'lbtnDel.Attributes("onclick") = "return confirm('" & SR.DeleteConfirmation() & "')"
            If _download Then
                If Not CType(e.Item.DataItem, SPL).Attachment = "" Then
                    lbtnDown.CommandArgument = CType(e.Item.DataItem, SPL).Attachment
                Else
                    lbtnDown.Visible = False
                End If
            Else
                lbtnDown.Visible = _download
            End If
        End If
    End Sub
    Private Sub dgSPLHeader_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSPLHeader.ItemCommand
        SaveCriteria2()
        If e.CommandName = "View" Then
            sessHelper.SetSession("Status", "View")
            sessHelper.SetSession("IDSPLHeader", CInt(e.Item.Cells(1).Text))
            Response.Redirect("FrmSPLDetail.aspx")
        ElseIf e.CommandName = "Edit" Then
            sessHelper.SetSession("Status", "Update")
            sessHelper.SetSession("IDSPLHeader", CInt(e.Item.Cells(1).Text))
            Response.Redirect("FrmSPLDetail.aspx")
        ElseIf e.CommandName = "Delete" Then
            If IsValidToDelete(CInt(e.Item.Cells(1).Text)) Then
                Dim ObjSPL As SPL = _SPLFacade.Retrieve(CInt(e.Item.Cells(1).Text))
                Try
                    _SPLFacade.Delete(ObjSPL)
                    Initialize()
                    BindDataGrid(0)
                    MessageBox.Show(SR.DeleteSucces)
                Catch ex As Exception
                    MessageBox.Show(SR.DeleteFail)
                End Try
            Else
                MessageBox.Show(SR.CannotDelete())
            End If
        ElseIf e.CommandName = "Download" Then
            Dim file As String = e.CommandArgument
            Dim fInfo As New System.IO.FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN") & file)
            Try
                Response.Redirect("../Download.aspx?file=" & file)
            Catch ex As Exception
                MessageBox.Show(SR.DownloadFail(file))
            End Try
        End If


    End Sub
    Private Sub dgSPLHeader_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSPLHeader.PageIndexChanged
        dgSPLHeader.CurrentPageIndex = e.NewPageIndex

        BindDataGrid(dgSPLHeader.CurrentPageIndex)
    End Sub
    Private Sub dgSPLHeader_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSPLHeader.SortCommand
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
        dgSPLHeader.SelectedIndex = -1
        dgSPLHeader.CurrentPageIndex = 0

        BindDataGrid(dgSPLHeader.CurrentPageIndex)
    End Sub

    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        DoTransfer()
    End Sub
    Protected Sub btnProses_Click(sender As Object, e As EventArgs) Handles btnProses.Click
        DoProcess()
    End Sub
    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Dim _arrList As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        _arrList = New SPLFacade(User).Retrieve(CreateCriterias(criterias))
        DoDownload(_arrList)
    End Sub
#End Region
    
    'Private Sub lbtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnNew.Click
    '    sessHelper.SetSession("Status", "Insert")
    '    Response.Redirect("FrmSPLDetail.aspx")
    'End Sub

    
End Class
