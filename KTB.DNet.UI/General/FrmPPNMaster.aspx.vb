#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.WebCC
Imports MMKSI.DNetUpload.Utility
Imports System.Collections.Generic
Imports OfficeOpenXml
Imports GlobalExtensions
Imports KTB.DNet.BusinessValidation

#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Reflection
Imports System.Globalization
#End Region

Public Class FrmPPNMaster
    Inherits System.Web.UI.Page

#Region " Private fields "
    Dim sessHelp As SessionHelper = New SessionHelper
    Dim InvoiceList As New ArrayList  '-- List of invoice
    Dim _PCAccessAllowed As Boolean = False
    Dim _CVAccessAllowed As Boolean = False
    Dim _LCVAccessAllowed As Boolean = False
    Private objDealer As Dealer
    Dim isDealerDMS As Boolean = False
    Private crit As CriteriaComposite
    Private stdCode As StandardCodeFacade = New StandardCodeFacade(User)
    Private mPPN As PPNMasterFacade = New PPNMasterFacade(User)
    'Private isDealerPiloting As Boolean = False

#End Region

#Region "Custom Method"
    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.General_Maintenance_TaxMaster) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Master Pajak")
        End If
    End Sub
    Private Sub InitData()
        Dim objDealer As Dealer = CType(sessHelp.GetSession("DEALER"), Dealer)
        Dim objUser As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)

        icStartDate.Value = "12:00:00 AM"
    End Sub

    Private Sub ReadData()
        '-- Read all data selected
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PPNMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        crit = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "TaxType"))

        If Not ddlTaxType.SelectedIndex > 0 Then
            Dim arrStdCode As ArrayList = stdCode.Retrieve(crit)
            ddlTaxType.Items.Clear()
            With ddlTaxType.Items
                For Each obj As StandardCode In arrStdCode
                    .Add(New ListItem(obj.ValueDesc, obj.ValueId))
                Next
            End With
            ddlTaxType.Items.Insert(0, "Silahkan Pilih")
        End If

        If (ddlTaxType.SelectedIndex <> 0) Then
            criterias.opAnd(New Criteria(GetType(PPNMaster), "TaxTypeID", MatchType.Exact, ddlTaxType.SelectedValue))
        End If

        If icStartDate.Value <> "12:00:00 AM" Then
            criterias.opAnd(New Criteria(GetType(PPNMaster), "StartDate", MatchType.Exact, icStartDate.Value))
        End If

        If txtPercentage.Text <> String.Empty Or txtPercentage.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(PPNMaster), "Percentage", MatchType.Exact, Convert.ToDecimal(txtPercentage.Text, New CultureInfo("id-ID")).ToString("0.00", New CultureInfo("en-US"))))
        End If

        Dim arrPPNMaster As ArrayList = New PPNMasterFacade(User).Retrieve(criterias)


        '-- Store InvoiceReqList into session for later use
        sessHelp.SetSession("PPNMasterList", arrPPNMaster)

    End Sub

    Private Sub BindPage(ByVal pageIndex As Integer)
        Dim arrPPNMaster As ArrayList = CType(sessHelp.GetSession("PPNMasterList"), ArrayList)
        Dim aStatus As New ArrayList
        If arrPPNMaster.Count <> 0 Then
            ' SortListControl(arrPPNMaster, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrPPNMaster, pageIndex, dtgPPNMaster.PageSize)
            dtgPPNMaster.DataSource = PagedList
            dtgPPNMaster.VirtualItemCount = arrPPNMaster.Count()
            dtgPPNMaster.DataBind()
        Else
            dtgPPNMaster.DataSource = New ArrayList
            dtgPPNMaster.VirtualItemCount = 0
            dtgPPNMaster.CurrentPageIndex = 0
            dtgPPNMaster.DataBind()
        End If
    End Sub

    Private Sub ClearData()
        txtPercentage.Text = String.Empty
        txtIDPPN.Text = String.Empty
        txtPercentage.ReadOnly = False
        ddlTaxType.Enabled = True
        icStartDate.Enabled = True
        icStartDate.Value = "12:00:00 AM"
        ddlTaxType.SelectedIndex = 0
        ReadData()
        dtgPPNMaster.CurrentPageIndex = 0
        BindPage(dtgPPNMaster.CurrentPageIndex)
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Function GetStdCodeDesc(ByVal Category As String, ByVal ValueID As Integer) As String
        Dim nResult As String = ""
        Dim criterias3 As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias3.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, Category))
        criterias3.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, ValueID))
        Dim arrDDL As ArrayList = New StandardCodeFacade(User).Retrieve(criterias3)
        Dim objStandardCode As New StandardCode
        If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
            objStandardCode = CType(arrDDL(0), StandardCode)
            nResult = objStandardCode.ValueDesc
        End If
        Return nResult
    End Function

    Private Function GetStdCodeID(ByVal Category As String, ByVal ValueDesc As String) As Integer
        Dim nResult As Integer = 0
        Dim criterias3 As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias3.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, Category))
        criterias3.opAnd(New Criteria(GetType(StandardCode), "ValueDesc", MatchType.Exact, ValueDesc))
        Dim arrDDL As ArrayList = New StandardCodeFacade(User).Retrieve(criterias3)
        Dim objStandardCode As New StandardCode
        If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
            objStandardCode = CType(arrDDL(0), StandardCode)
            nResult = objStandardCode.ValueId
        End If
        Return nResult
    End Function

    Private Sub ViewMasterPPN(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim mPPN As PPNMaster = New PPNMasterFacade(User).Retrieve(nID)
        If Not IsNothing(mPPN) Then
            txtIDPPN.Text = mPPN.ID
            ddlTaxType.SelectedValue = mPPN.TaxTypeID
            icStartDate.Value = mPPN.StartDate
            txtPercentage.Text = CType(mPPN.Percentage, String)
        End If
    End Sub

    Private Function Validate(ByVal TaxTypeID As Integer, ByVal TaxTypeDesc As String, ByVal StartDate As Date, ByVal Action As String) As String
        Dim nResult As String = ""
        crit = New CriteriaComposite(New Criteria(GetType(PPNMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(PPNMaster), "TaxTypeID", MatchType.Exact, TaxTypeID))
        crit.opAnd(New Criteria(GetType(PPNMaster), "StartDate", MatchType.Exact, StartDate))

        Dim arrPPN As ArrayList = mPPN.Retrieve(crit)
        If (arrPPN.Count > 0 And Action <> "Delete" And Action <> "Edit") Then
            nResult = nResult & TaxTypeDesc & " pada tanggal " & StartDate & " sudah ada, mohon gunakan data lain"
        ElseIf arrPPN.Count = 0 Or Action = "Delete" Then
            If StartDate <= DateTime.Now Then
                Dim objDealer As Dealer = CType(sessHelp.GetSession("DEALER"), Dealer)
                Dim isActiveTransactionControl As Boolean = TCHelper.GetActiveTCResult(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.BackdatePPN))
                If Not isActiveTransactionControl Then
                    nResult = "Tanggal Mulai harus lebih besar dari " & DateTime.Now
                End If
                If Not isActiveTransactionControl And Action = "Delete" Then
                    nResult = "Mohon aktifkan Transaction Control ‘BackDatePPN’ jika ingin menghapus data tersebut"
                End If
            End If

        End If
        Return nResult
    End Function

#Region "download excel"

    Private Function CreateSheet(pck As ExcelPackage, sheetName As String) As ExcelWorksheet
        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add(sheetName)
        ws.View.ShowGridLines = False
        Return ws
    End Function

    Private Sub CreateExcelFile(pck As ExcelPackage, fileName As String)
        Dim fileBytes = pck.GetAsByteArray()
        Response.Clear()

        Response.AppendHeader("Content-Length", fileBytes.Length.ToString())
        Response.AppendHeader("Content-Disposition", String.Format("attachment; filename=""{0}"";", fileName))
        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"  'xlsx
        Response.ContentType = "application/vnd.ms-excel" 'xls
        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()

    End Sub
#End Region

#End Region

#Region "Event Handlers"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            hdnMCPConfirmation.Value = -1
            ActivateUserPrivilege()
            InitData()
            ReadData()
            dtgPPNMaster.CurrentPageIndex = 0
            BindPage(dtgPPNMaster.CurrentPageIndex)
        End If
    End Sub

    Private Sub dtgPPNMaster_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPPNMaster.ItemCommand
        If e.CommandName = "View" Then
            ViewState.Add("vsProcess", "View")
            ViewMasterPPN(e.Item.Cells(0).Text, False)
            ddlTaxType.Enabled = False
            icStartDate.Enabled = False
            txtPercentage.ReadOnly = True
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewMasterPPN(e.Item.Cells(0).Text, False)
        ElseIf e.CommandName = "Delete" Then
            ViewMasterPPN(e.Item.Cells(0).Text, False)
            Dim msgValidasi As String = Validate((CType(ddlTaxType.SelectedValue, Integer)), ddlTaxType.SelectedItem.ToString(), icStartDate.Value, "Delete")
            If msgValidasi = "" Then
                Dim _nResult As Integer = 0
                Dim _mPPN As PPNMaster = mPPN.Retrieve(CType(txtIDPPN.Text, Integer))
                _mPPN.TaxTypeID = CType(ddlTaxType.SelectedValue, Integer)
                _mPPN.StartDate = icStartDate.Value
                _mPPN.Percentage = CType(txtPercentage.Text, Decimal)
                _mPPN.RowStatus = CType(DBRowStatus.Deleted, Short)
                mPPN.Delete(_mPPN)
                ClearData()
                MessageBox.Show("Sukses Delete")
            Else
                MessageBox.Show(msgValidasi)
            End If

        End If

    End Sub

    Private Sub dtgPPNMaster_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPPNMaster.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lbNo"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lbStatus"), Label)
            Dim lbStartDate As Label = CType(e.Item.FindControl("lbStartDate"), Label)
            Dim lbTaxType As Label = CType(e.Item.FindControl("lbTaxType"), Label)

            Dim dt As Date = Date.Parse(lbStartDate.Text)
            lbStartDate.Text = dt.ToString("dd/MM/yyyy")

            lblNo.Text = (dtgPPNMaster.CurrentPageIndex * dtgPPNMaster.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No
            If lblStatus.Text = 0 Then
                lblStatus.Text = "Aktif"
            Else
                lblStatus.Text = "Tidak Aktif"
            End If

            lbTaxType.Text = GetStdCodeDesc("TaxType", CType(lbTaxType.Text, Integer))
        End If
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs)
        ReadData()
        dtgPPNMaster.CurrentPageIndex = 0
        BindPage(dtgPPNMaster.CurrentPageIndex)
    End Sub

    Private Sub dtgPPNMaster_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPPNMaster.PageIndexChanged
        '-- Change datagrid page

        dtgPPNMaster.CurrentPageIndex = e.NewPageIndex
        BindPage(e.NewPageIndex)

    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If ddlTaxType.SelectedIndex = 0 Or txtPercentage.Text = String.Empty Or icStartDate.Value = "12:00:00 AM" Then
            MessageBox.Show("Tipe Pajak, Tanggal Mulai dan Persentase tidak boleh kosong")
            Return
        End If
        If txtIDPPN.Text = String.Empty Then
            Dim validateMsg As String = Validate((CType(ddlTaxType.SelectedValue, Integer)), ddlTaxType.SelectedItem.ToString(), icStartDate.Value, "Save")
            If validateMsg = "" Then
                Dim _mPPN As New PPNMaster
                Dim _nResult As Integer = 0
                _mPPN.TaxTypeID = CType(ddlTaxType.SelectedValue, Integer)
                _mPPN.StartDate = icStartDate.Value
                _mPPN.Percentage = Convert.ToDecimal(txtPercentage.Text, New CultureInfo("id-ID"))
                _nResult = mPPN.Insert(_mPPN)
                If _nResult = -1 Then
                    MessageBox.Show("Master Pajak " & SR.SaveFail)
                Else
                    MessageBox.Show("Master Pajak " & SR.SaveSuccess)
                End If
            Else
                MessageBox.Show(validateMsg)
            End If
        Else
            Dim validateMsg As String = Validate((CType(ddlTaxType.SelectedValue, Integer)), ddlTaxType.SelectedItem.ToString(), icStartDate.Value, "Edit")
            If validateMsg = "" Then
                Dim _nResult As Integer = 0
                Dim _mPPN As PPNMaster = mPPN.Retrieve(CType(txtIDPPN.Text, Integer))
                _mPPN.TaxTypeID = CType(ddlTaxType.SelectedValue, Integer)
                _mPPN.StartDate = icStartDate.Value
                _mPPN.Percentage = CType(txtPercentage.Text, Decimal)
                _nResult = mPPN.Update(_mPPN)
                If _nResult = -1 Then
                    MessageBox.Show("Master Pajak " & SR.UpdateFail)
                Else
                    MessageBox.Show("Master Pajak " & SR.UpdateSucces)
                End If
            Else
                MessageBox.Show(validateMsg)
            End If
        End If
        ClearData()
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs)
        'Dim arrPPNMaster As ArrayList = CType(sessHelp.GetSession("PPNMasterList"), ArrayList)
        ''Dim aStatus As New ArrayList
        'If arrPPNMaster.Count <> 0 Then
        '    '   DoDownload(arrPPNMaster)
        '    SetDownload()
        'End If
    End Sub
#End Region
End Class