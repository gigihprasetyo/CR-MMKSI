Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security

Public Class FrmSpOrderRestriction
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents rbtnDate As System.Web.UI.WebControls.RadioButton
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents chkActive As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlOrderType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgOrderRestriction As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents txtNote As System.Web.UI.WebControls.TextBox
    Protected WithEvents icPeriodeStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPeriodeEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents pnlTanggal As System.Web.UI.WebControls.Panel
    Protected WithEvents rbtnDay As System.Web.UI.WebControls.RadioButton
    Protected WithEvents ddlDays As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHStart As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMStart As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSStart As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHEnd As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMEnd As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSEnd As System.Web.UI.WebControls.TextBox
    Protected WithEvents RangeValidator1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents RangeValidator2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents RangeValidator3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents RangeValidator4 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents RangeValidator5 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents RangeValidator6 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator4 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator6 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator7 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnDeleteAll As System.Web.UI.WebControls.Button

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
    Private sessHelper As SessionHelper = New SessionHelper
    Private _edit As Boolean
#End Region

#Region "PrivateCustomMethods"
    Private Sub EnableControl(ByVal isEnable As Boolean)
        ddlOrderType.Enabled = isEnable
        lblSearchDealer.Enabled = isEnable
        pnlTanggal.Enabled = isEnable
        ddlDays.Enabled = isEnable
        txtHStart.ReadOnly = Not isEnable
        txtMStart.ReadOnly = Not isEnable
        txtSStart.ReadOnly = Not isEnable
        txtHEnd.ReadOnly = Not isEnable
        txtMEnd.ReadOnly = Not isEnable
        txtSEnd.ReadOnly = Not isEnable
        txtNote.ReadOnly = Not isEnable
        chkActive.Enabled = isEnable
        If isEnable Then
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection()"
        Else
            lblSearchDealer.Attributes.Clear()
        End If
    End Sub
    Private Sub ViewOrderRestriction(ByVal id As Integer)
        Dim time As String = ""
        Dim tPart As String = ""
        Dim ctString As Integer = 0
        Dim ObjOrderRestriction As OrderRestriction
        Dim _OrderRestrictionFacade As OrderRestrictionFacade = New OrderRestrictionFacade(User)
        ObjOrderRestriction = _OrderRestrictionFacade.Retrieve(id)
        ddlOrderType.SelectedValue = ObjOrderRestriction.OrderType
        txtDealerCode.Text = ObjOrderRestriction.Dealer.DealerCode
        If ObjOrderRestriction.RestrictedType = "T" Then
            rbtnDate.Checked = True
            rbtnDay.Checked = False
            pnlTanggal.Style("display") = "block"
            ddlDays.Style("display") = "none"
            icPeriodeStart.Value = ObjOrderRestriction.DateFrom
            icPeriodeEnd.Value = ObjOrderRestriction.DateTo
        ElseIf ObjOrderRestriction.RestrictedType = "Y" Then
            rbtnDate.Checked = False
            rbtnDay.Checked = True
            pnlTanggal.Style("display") = "none"
            ddlDays.Style("display") = "block"
            ddlDays.SelectedValue = CType(ObjOrderRestriction.Days, String)
        End If
        tPart = ""
        ctString = 0
        time = ObjOrderRestriction.TimeFrom & ":"
        For i As Integer = 0 To time.Length - 1
            If Not time.Substring(i, 1) = ":" Then
                tPart = tPart & time.Substring(i, 1)
            Else
                If ctString = 0 Then
                    txtHStart.Text = tPart
                    tPart = ""
                    ctString = ctString + 1
                ElseIf ctString = 1 Then
                    txtMStart.Text = tPart
                    tPart = ""
                    ctString = ctString + 1
                ElseIf ctString = 2 Then
                    txtSStart.Text = tPart
                    tPart = ""
                    ctString = ctString + 1
                End If
            End If
        Next i

        tPart = ""
        ctString = 0
        time = ObjOrderRestriction.TimeTO & ":"
        For i As Integer = 0 To time.Length - 1
            If Not time.Substring(i, 1) = ":" Then
                tPart = tPart & time.Substring(i, 1)
            Else
                If ctString = 0 Then
                    txtHEnd.Text = tPart
                    tPart = ""
                    ctString = ctString + 1
                ElseIf ctString = 1 Then
                    txtMEnd.Text = tPart
                    tPart = ""
                    ctString = ctString + 1
                ElseIf ctString = 2 Then
                    txtSEnd.Text = tPart
                    tPart = ""
                    ctString = ctString + 1
                End If
            End If
        Next i

        txtNote.Text = ObjOrderRestriction.Note
        If ObjOrderRestriction.IsActive = 0 Then
            chkActive.Checked = False
        Else
            chkActive.Checked = True
        End If

    End Sub
    Private Function MergeDateAndTime(ByVal tDate As Date, ByVal sTime As String) As DateTime
        Dim tDatetime As Date
        Dim tStringArray() As String = sTime.Split(":")

        tDatetime = tDate.Date
        tDatetime.AddHours(CType(tStringArray(0), Double))
        tDatetime.AddMinutes(CType(tStringArray(1), Double))
        tDatetime.AddSeconds(CType(tStringArray(2), Double))

        Return tDatetime

    End Function
    Private Function Update(ByVal id As Integer) As Integer
        Dim ObjOrderRestriction As OrderRestriction = New OrderRestriction
        Dim _OrderRestrictionFacade As OrderRestrictionFacade = New OrderRestrictionFacade(User)
        Dim result As Integer

        ObjOrderRestriction = _OrderRestrictionFacade.Retrieve(id)
        ObjOrderRestriction.OrderType = ddlOrderType.SelectedValue
        If rbtnDate.Checked Then
            ObjOrderRestriction.RestrictedType = "T"
            ObjOrderRestriction.DateFrom = MergeDateAndTime(icPeriodeStart.Value, HourStart())
            ObjOrderRestriction.DateTo = MergeDateAndTime(icPeriodeEnd.Value, HourEnd())
            ObjOrderRestriction.TimeFrom = HourStart()
            ObjOrderRestriction.TimeTO = HourEnd()
        ElseIf rbtnDay.Checked Then
            ObjOrderRestriction.RestrictedType = "Y"
            ObjOrderRestriction.Days = CType(ddlDays.SelectedValue, Integer)
            ObjOrderRestriction.TimeFrom = HourStart()
            ObjOrderRestriction.TimeTO = HourEnd()
        End If
        ObjOrderRestriction.Note = txtNote.Text.Trim()
        If chkActive.Checked Then
            ObjOrderRestriction.IsActive = 1
        Else
            ObjOrderRestriction.IsActive = 0
        End If
        ObjOrderRestriction.RowStatus = 0

        result = _OrderRestrictionFacade.Update(ObjOrderRestriction)
        Return result

    End Function
    Private Function Delete(ByVal id As Integer) As Integer
        Dim _orderRestrictionFacade As OrderRestrictionFacade = New OrderRestrictionFacade(User)
        Dim ObjOrderRestriction As OrderRestriction
        Dim result As Integer = -2
        ObjOrderRestriction = _orderRestrictionFacade.Retrieve(id)
        Try
            _orderRestrictionFacade.Delete(ObjOrderRestriction)
        Catch ex As Exception
            result = -1
        End Try
        Return result
    End Function
    Private Function Insert(ByVal dealerCode As String) As Integer
        Dim _OrderRestricFacade As OrderRestrictionFacade = New OrderRestrictionFacade(User)
        Dim _DealerFacade As DealerFacade = New DealerFacade(User)
        Dim ObjDealer As Dealer = _DealerFacade.Retrieve(dealerCode)
        Dim ObjOrderRestriction As OrderRestriction = New OrderRestriction
        Dim result As Integer

        ObjOrderRestriction.Dealer = ObjDealer
        ObjOrderRestriction.OrderType = ddlOrderType.SelectedValue
        If rbtnDate.Checked Then
            ObjOrderRestriction.RestrictedType = "T"
            ObjOrderRestriction.DateFrom = MergeDateAndTime(icPeriodeStart.Value, HourStart())
            ObjOrderRestriction.DateTo = MergeDateAndTime(icPeriodeEnd.Value, HourEnd())
            ObjOrderRestriction.TimeFrom = HourStart()
            ObjOrderRestriction.TimeTO = HourEnd()
        ElseIf rbtnDay.Checked Then
            ObjOrderRestriction.RestrictedType = "Y"
            ObjOrderRestriction.Days = CType(ddlDays.SelectedValue, Integer)
            ObjOrderRestriction.TimeFrom = HourStart()
            ObjOrderRestriction.TimeTO = HourEnd()
        End If
        ObjOrderRestriction.Note = txtNote.Text.Trim()
        If chkActive.Checked Then
            ObjOrderRestriction.IsActive = 1
        Else
            ObjOrderRestriction.IsActive = 0
        End If

        result = _OrderRestricFacade.Insert(ObjOrderRestriction)
        Return result
    End Function
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.ENHBatasWaktuPemesananView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PEMESANAN - Batas Waktu Pemesanan")
        End If
        _edit = SecurityProvider.Authorize(context.User, SR.ENHBatasWaktuPemesananEdit_Privilege)
        btnSave.Visible = _edit
    End Sub
    Private Function HourStart() As String
        Dim hour As String = txtHStart.Text.Trim()
        Dim minute As String = txtMStart.Text.Trim()
        Dim second As String = txtSStart.Text.Trim()
        If hour.Length < 2 Then
            hour = "0" & hour
        End If
        If minute.Length < 2 Then
            minute = "0" & minute
        End If
        If second.Length < 2 Then
            second = "0" & second
        End If

        Dim sTemp As String = hour & ":" & minute & ":" & second
        Return sTemp
    End Function
    Private Function HourEnd() As String
        Dim hour As String = txtHEnd.Text.Trim()
        Dim minute As String = txtMEnd.Text.Trim()
        Dim second As String = txtSEnd.Text.Trim()
        If hour.Length < 2 Then
            hour = "0" & hour
        End If
        If minute.Length < 2 Then
            minute = "0" & minute
        End If
        If second.Length < 2 Then
            second = "0" & second
        End If

        Dim sTemp As String = hour & ":" & minute & ":" & second
        Return sTemp
    End Function
    Private Sub BindDdlOrderType()
        ddlOrderType.Items.Clear()
        'ddlOrderType.Items.Add(New ListItem("Emergency", "E"))
        'ddlOrderType.Items.Add(New ListItem("Reguler", "R"))
        'ddlOrderType.Items.Add(New ListItem("Khusus", "K"))
        'ddlOrderType.Items.Add(New ListItem("Indent", "I"))

        ddlOrderType.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For Each liOrderType As ListItem In LookUp.ArraySPOrderTypeKTBDealer
            ddlOrderType.Items.Add(New ListItem(liOrderType.Text, liOrderType.Value))
        Next
        ddlOrderType.SelectedValue = "-1"

        ddlOrderType.DataBind()


    End Sub
    Private Sub BindDdlDays()
        ddlDays.Items.Clear()
        ddlDays.Items.Add(New ListItem("Senin", "1"))
        ddlDays.Items.Add(New ListItem("Selasa", "2"))
        ddlDays.Items.Add(New ListItem("Rabu", "3"))
        ddlDays.Items.Add(New ListItem("Kamis", "4"))
        ddlDays.Items.Add(New ListItem("Jumat", "5"))
        ddlDays.Items.Add(New ListItem("Sabtu", "6"))
        ddlDays.Items.Add(New ListItem("Minggu", "7"))
    End Sub
    Private Sub BindDtgAll(ByVal indexPage As Integer)
        Dim totalrow As Integer = 0
        dgOrderRestriction.CurrentPageIndex = indexPage
        dgOrderRestriction.DataSource = New OrderRestrictionFacade(User).RetrieveActiveList(indexPage + 1, dgOrderRestriction.PageSize, totalrow)
        dgOrderRestriction.VirtualItemCount = totalrow
        dgOrderRestriction.DataBind()
    End Sub



    Private Sub BindDtg(ByVal indexpage As Integer)
        Dim totalRow As Integer = 0
        If (indexpage >= 0) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(OrderRestriction), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            If Not CType(ViewState("FirstLoad"), Boolean) Then
                If ddlOrderType.SelectedValue <> "" AndAlso ddlOrderType.SelectedValue <> "-1" Then
                    criterias.opAnd(New Criteria(GetType(OrderRestriction), "OrderType", MatchType.Exact, ddlOrderType.SelectedValue))
                End If

                If txtDealerCode.Text.Length > 0 Then
                    criterias.opAnd(New Criteria(GetType(OrderRestriction), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Trim().Replace(";", "','") & "')"))
                End If

                If rbtnDate.Checked Then
                    'criterias.opAnd(New Criteria(GetType(OrderRestriction), "DateFrom", MatchType.GreaterOrEqual, Format(icPeriodeStart.Value, "yyyy/MM/dd")))
                    'criterias.opAnd(New Criteria(GetType(OrderRestriction), "DateTo", MatchType.LesserOrEqual, Format(icPeriodeEnd.Value, "yyyy/MM/dd")))

                    criterias.opAnd(New Criteria(GetType(OrderRestriction), "DateFrom", MatchType.LesserOrEqual, icPeriodeStart.Value), " ( ", True)
                    criterias.opAnd(New Criteria(GetType(OrderRestriction), "DateTo", MatchType.GreaterOrEqual, icPeriodeStart.Value))

                    criterias.opOr(New Criteria(GetType(OrderRestriction), "DateFrom", MatchType.LesserOrEqual, icPeriodeEnd.Value))
                    criterias.opAnd(New Criteria(GetType(OrderRestriction), "DateTo", MatchType.GreaterOrEqual, icPeriodeEnd.Value), ")", False)

                ElseIf rbtnDay.Checked Then
                    criterias.opAnd(New Criteria(GetType(OrderRestriction), "Days", MatchType.Exact, CType(ddlDays.SelectedValue, Integer)))
                End If

                If chkActive.Checked Then
                    criterias.opAnd(New Criteria(GetType(OrderRestriction), "IsActive", MatchType.Exact, 1))
                Else
                    criterias.opAnd(New Criteria(GetType(OrderRestriction), "IsActive", MatchType.Exact, 0))

                End If


            End If


            Dim arrList As System.Collections.ArrayList = New OrderRestrictionFacade(User).RetrieveActiveList(criterias, _
                indexpage + 1, dgOrderRestriction.PageSize, totalRow, CType(sessHelper.GetSession("CurrentSortColumn"), String), _
                CType(sessHelper.GetSession("CurrentSortDirection"), Sort.SortDirection))

            If arrList.Count > 0 Then
                dgOrderRestriction.DataSource = arrList
                dgOrderRestriction.VirtualItemCount = totalRow
                dgOrderRestriction.DataBind()
            Else
                Dim emptyList As ArrayList = New ArrayList
                dgOrderRestriction.SelectedIndex = -1
                dgOrderRestriction.DataSource = emptyList
                dgOrderRestriction.VirtualItemCount = 0
                dgOrderRestriction.CurrentPageIndex = 0
                dgOrderRestriction.DataBind()


            End If

        End If

    End Sub
    Private Sub CheckRadioButton()
        If rbtnDate.Checked Then
            pnlTanggal.Style("display") = "block"
            ddlDays.Style("display") = "none"
        ElseIf rbtnDay.Checked Then
            pnlTanggal.Style("display") = "none"
            ddlDays.Style("display") = "block"
        End If
    End Sub
    Private Sub InitializePage()
        ddlOrderType.SelectedValue = "-1"
        txtDealerCode.Text = ""
        rbtnDate.Checked = True
        rbtnDay.Checked = False
        ddlDays.Style("display") = "none"
        pnlTanggal.Style("display") = "block"
        txtHStart.Text = ""
        txtHEnd.Text = ""
        txtMStart.Text = ""
        txtMEnd.Text = ""
        txtSStart.Text = ""
        txtSEnd.Text = ""
        txtNote.Text = ""
        chkActive.Checked = False
        icPeriodeStart.Value = Date.Now
        icPeriodeEnd.Value = Date.Now
        sessHelper.SetSession("CurrentSortColumn", "OrderType")
        sessHelper.SetSession("CurrentSortDirection", Sort.SortDirection.ASC)
        sessHelper.SetSession("Status", "Insert")
        EnableControl(True)
        btnSave.Enabled = True
        btnBatal.Enabled = True
        btnCari.Enabled = True
    End Sub
#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        CheckRadioButton()
        btnDeleteAll.Attributes.Add("OnClick", "return confirm('Anda yakin akan hapus semua data?');")
        rbtnDate.Attributes("onclick") = "ManageRadButton(this)"
        rbtnDay.Attributes("onclick") = "ManageRadButton(this)"
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection()"
        txtHEnd.Attributes("onkeypress") = "numericOnlyUniv(event)"
        txtHStart.Attributes("onkeypress") = "numericOnlyUniv(event)"
        txtMEnd.Attributes("onkeypress") = "numericOnlyUniv(event)"
        txtMStart.Attributes("onkeypress") = "numericOnlyUniv(event)"
        txtSEnd.Attributes("onkeypress") = "numericOnlyUniv(event)"
        txtSStart.Attributes("onkeypress") = "numericOnlyUniv(event)"
        If Not IsPostBack Then
            ViewState("FirstLoad") = True
            BindDdlOrderType()
            BindDdlDays()
            InitializePage()
            ViewAllData()
        End If
        txtDealerCode.Attributes.Add("readonly", "readonly")
    End Sub
    Private Function ValidateRangeTime() As Boolean
        Dim calFrom As DateTime = New DateTime(2000, 1, 1, txtHStart.Text.Trim, txtMStart.Text.Trim, txtSStart.Text.Trim)
        Dim calTo As DateTime = New DateTime(2000, 1, 1, txtHEnd.Text.Trim, txtMEnd.Text.Trim, txtSEnd.Text.Trim)
        If calFrom > calTo Then
            MessageBox.Show("Periode waktu yang dimasukan tidak valid.")
            Return False
        Else
            Return True
        End If

    End Function
    Private Sub SaveOrderRestriction()
        If ValidateRangeTime() Then
            If CType(sessHelper.GetSession("Status"), String) = "Insert" Then
                If txtDealerCode.Text.Trim() <> "" Then
                    Dim dealerCodes As String() = txtDealerCode.Text.Trim.Split(";")
                    Try
                        For Each item As String In dealerCodes
                            Insert(item)
                        Next
                        MessageBox.Show(SR.SaveSuccess)
                    Catch ex As Exception
                        MessageBox.Show(SR.SaveFail)
                    End Try
                Else
                    MessageBox.Show("Kode Dealer tidak boleh kosong")
                End If
            ElseIf CType(sessHelper.GetSession("Status"), String) = "Update" Then
                Dim idUpdate As Integer = CType(sessHelper.GetSession("IDUpdate"), Integer)
                If Update(idUpdate) = -1 Then
                    MessageBox.Show(SR.UpdateFail)
                Else
                    MessageBox.Show(SR.UpdateSucces)
                End If
            End If
            'InitializePage()
            BindDtg(0)
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If ddlOrderType.SelectedValue = "-1" Then
            MessageBox.Show("Tentukan Tipe Order")
            Exit Sub
        End If
        If txtNote.Text.Trim <> "" Then
            SaveOrderRestriction()
        Else
            MessageBox.Show("Catatan tidak boleh kosong")
        End If
    End Sub
    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        InitializePage()
    End Sub
    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        ViewState("FirstLoad") = False
        ViewAllData()
    End Sub

    Private Sub ViewAllData()
        dgOrderRestriction.CurrentPageIndex = 0
        sessHelper.SetSession("CurrentSortColumn", "OrderType")
        sessHelper.SetSession("CurrentSortDirect", Sort.SortDirection.ASC)
        BindDtg(0)
    End Sub

    Private Sub dgOrderRestriction_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgOrderRestriction.ItemDataBound
        If Not e.Item.ItemIndex = -1 Then
            Dim _or As OrderRestriction = CType(e.Item.DataItem, OrderRestriction)
            Dim lDel As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim lEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)

            Dim lblAktive As Label = CType(e.Item.FindControl("lblAktive"), Label)
            Dim lblTipeOrder As Label = CType(e.Item.FindControl("lblTipeOrder"), Label)
            Dim lblHarian As Label = CType(e.Item.FindControl("lblHarian"), Label)
            Dim lblMulai As Label = CType(e.Item.FindControl("lblMulai"), Label)
            Dim lblSampai As Label = CType(e.Item.FindControl("lblSampai"), Label)
            If _or.IsActive = 1 Then
                lblAktive.Text = "Aktif"
            Else
                lblAktive.Text = "Tidak Aktif"
            End If
            'SPPOOrderType
            lblTipeOrder.Text = SPPOOrderType.OrderType(_or.OrderType)
            
            If _or.RestrictedType = "Y" Then
                lblHarian.Text = CType(_or.Days, LookUp.EnumHari).ToString & " " & _or.TimeFrom & " s/d " & _or.TimeTO
                lblMulai.Text = ""
                lblSampai.Text = ""
            Else
                Dim fromTime As String = _or.TimeFrom
                Dim ToTime As String = _or.TimeTO
                If _or.TimeTO.Length < 3 Then
                    ToTime = "00:00:00"
                End If
                If _or.TimeFrom.Length < 3 Then
                    fromTime = "00:00:00"
                End If
                lblMulai.Text = _or.DateFrom.Date.ToShortDateString & " " & fromTime
                lblSampai.Text = _or.DateTo.Date.ToShortDateString & " " & ToTime
                lblHarian.Text = ""
            End If

            lDel.Attributes.Add("onclick", "return confirm('" & SR.DeleteConfirmation & "')")
            lDel.Visible = _edit
            lEdit.Visible = _edit
            e.Item.Cells(1).Text = CType((e.Item.ItemIndex + 1 + (dgOrderRestriction.CurrentPageIndex * dgOrderRestriction.PageSize)), String)
            e.Item.Cells(3).Text = CType(e.Item.DataItem, OrderRestriction).Dealer.DealerCode
        End If
    End Sub
    Private Sub dgOrderRestriction_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgOrderRestriction.SortCommand
        If CType(sessHelper.GetSession("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(sessHelper.GetSession("CurrentSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    sessHelper.SetSession("CurrentSortDirection", Sort.SortDirection.DESC)
                Case Sort.SortDirection.DESC
                    sessHelper.SetSession("CurrentSortDirection", Sort.SortDirection.ASC)
            End Select
        Else
            sessHelper.SetSession("CurrentSortColumn", e.SortExpression)
            sessHelper.SetSession("CurrentSortDirection", Sort.SortDirection.ASC)
        End If

        dgOrderRestriction.SelectedIndex = -1
        dgOrderRestriction.CurrentPageIndex = 0
        BindDtg(dgOrderRestriction.CurrentPageIndex)
    End Sub
    Private Sub dgOrderRestriction_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgOrderRestriction.PageIndexChanged

        dgOrderRestriction.CurrentPageIndex = e.NewPageIndex

        BindDtg(dgOrderRestriction.CurrentPageIndex)
    End Sub
    Private Sub dgOrderRestriction_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgOrderRestriction.ItemCommand
        If (e.CommandName = "View") Then
            ViewOrderRestriction(e.Item.Cells(0).Text)
            EnableControl(False)
            btnSave.Enabled = False
            btnCari.Enabled = False
        ElseIf e.CommandName = "Edit" Then
            sessHelper.SetSession("Status", "Update")
            sessHelper.SetSession("IDUpdate", CType(e.Item.Cells(0).Text, Integer))
            ViewOrderRestriction(e.Item.Cells(0).Text)
            EnableControl(True)
            btnSave.Enabled = True
            btnCari.Enabled = False
        ElseIf e.CommandName = "Delete" Then
            If Not Delete(CType(e.Item.Cells(0).Text, Integer)) = -1 Then
                MessageBox.Show(SR.DeleteSucces)
                InitializePage()
                BindDtgAll(0)
            Else
                MessageBox.Show(SR.DeleteFail)
            End If
        End If
    End Sub
#End Region

    Private Sub DeleteAll()
        Dim arrList As System.Collections.ArrayList = New orderRestrictionFacade(User).RetrieveList()
        If arrList.Count > 0 Then
            For Each item As OrderRestriction In arrList
                Dim orderRestrictionFacade As orderRestrictionFacade = New orderRestrictionFacade(User)
                orderRestrictionFacade.DeleteFromDB(item)
            Next
        End If
    End Sub

    Private Sub btnDeleteAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteAll.Click
        dgOrderRestriction.CurrentPageIndex = 0
        DeleteAll()
        BindDtg(0)
    End Sub
End Class
