#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region
#Region " .NET Namespace "
Imports System.IO
Imports System.Text
Imports System.Collections.Generic
Imports OfficeOpenXml
Imports OfficeOpenXml.Style

#End Region
Public Class frmSPPOListForKTB
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlOrderType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents icPODateStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPODateEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlProcessCode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dtgSPPO As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblIstransfer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTelahDitransfer As System.Web.UI.WebControls.DropDownList
    Private sessHelper As SessionHelper = New SessionHelper
    Protected WithEvents btnProcess As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents btnSend As System.Web.UI.WebControls.Button
    Protected WithEvents chkPODate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkSenDate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents icSendDateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icSendDateTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlTermOfPayment As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNomorPesanan As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblGrandTotal As System.Web.UI.WebControls.Label
    'chkPODate

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private _sessHelper As SessionHelper = New SessionHelper
    Private objDealer As Dealer
#End Region

#Region "Custom Method"

    Private Sub checkDealer()
        'If Session("DEALER") Is Nothing Then
        '    Response.Redirect("..\SessionExpired.htm")
        'End If
    End Sub

    Private Sub BindOrderType()
        ddlOrderType.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For Each liOrderType As ListItem In LookUp.ArraySPOrderTypeKTBDealer
            ddlOrderType.Items.Add(New ListItem(liOrderType.Text, liOrderType.Value))
        Next
    End Sub

    Private Sub BindProccessCode()
        ddlProcessCode.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For Each liOrderType As ListItem In LookUp.ArraySPPOProccessCode
            'If (liOrderType.Value <> "" AndAlso liOrderType.Value <> "C") Then
            ddlProcessCode.Items.Add(New ListItem(liOrderType.Text, liOrderType.Value))
            'End If
        Next
    End Sub

    Private Sub BindHeader()
        BindOrderType()
        BindProccessCode()
        bindistransfer()
    End Sub

    Private Sub bindistransfer()
        ddlTelahDitransfer.Items.Clear()
        ddlTelahDitransfer.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        ddlTelahDitransfer.Items.Add(New ListItem("", "0"))
        ddlTelahDitransfer.Items.Add(New ListItem("Telah Ditransfer", "1"))
    End Sub

    Private Sub BindTodtgSPPOAll(ByVal pageIndex As Integer)
        checkDealer()

        'Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
        Dim ListSPPO As New ArrayList
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'criterias.opAnd(New Criteria(GetType(SparePartPO), "Dealer.ID", MatchType.Exact, CType(Session("DEALER"), Dealer).ID))
        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPO), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If String.IsNullOrEmpty(txtNomorPesanan.Text) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPO), "PONumber", MatchType.Partial, txtNomorPesanan.Text.Trim))
        End If

        If txtDealerCode.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPO), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
        End If

        If chkPODate.Checked Then
            If icPODateStart.Value <= icPODateEnd.Value Then
                criterias.opAnd(New Criteria(GetType(SparePartPO), "PODate", MatchType.GreaterOrEqual, Format(icPODateStart.Value, "yyyy/MM/dd")))
                criterias.opAnd(New Criteria(GetType(SparePartPO), "PODate", MatchType.LesserOrEqual, Format(icPODateEnd.Value, "yyyy/MM/dd")))

            Else
                criterias.opAnd(New Criteria(GetType(SparePartPO), "PODate", MatchType.GreaterOrEqual, Format(Date.Now, "yyyy/MM/dd")))
                criterias.opAnd(New Criteria(GetType(SparePartPO), "PODate", MatchType.LesserOrEqual, Format(Date.Now, "yyyy/MM/dd")))
                icPODateStart.Value = Date.Now
                icPODateEnd.Value = Date.Now
            End If
        End If

        If chkSenDate.Checked Then
            If icSendDateFrom.Value <= icSendDateTo.Value Then
                criterias.opAnd(New Criteria(GetType(SparePartPO), "SentPODate", MatchType.GreaterOrEqual, Format(icSendDateFrom.Value, "yyyy/MM/dd")))
                criterias.opAnd(New Criteria(GetType(SparePartPO), "SentPODate", MatchType.LesserOrEqual, Format(icSendDateTo.Value, "yyyy/MM/dd")))

            Else
                criterias.opAnd(New Criteria(GetType(SparePartPO), "SentPODate", MatchType.GreaterOrEqual, Format(Date.Now, "yyyy/MM/dd")))
                criterias.opAnd(New Criteria(GetType(SparePartPO), "SentPODate", MatchType.LesserOrEqual, Format(Date.Now, "yyyy/MM/dd")))
                icSendDateFrom.Value = Date.Now
                icSendDateTo.Value = Date.Now
            End If
        End If

        If Not ddlTermOfPayment.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPO), "TermOfPayment.ID", MatchType.Exact, ddlTermOfPayment.SelectedValue))
        End If

        'criterias.opAnd(New Criteria(GetType(SparePartPO), "ProcessCode", MatchType.No, String.Empty))
        'criterias.opAnd(New Criteria(GetType(SparePartPO), "ProcessCode", MatchType.No, "C"))

        If ddlOrderType.SelectedValue <> "-1" Then criterias.opAnd(New Criteria(GetType(SparePartPO), "OrderType", MatchType.Exact, ddlOrderType.SelectedValue))
        If ddlProcessCode.SelectedValue <> "-1" Then criterias.opAnd(New Criteria(GetType(SparePartPO), "ProcessCode", MatchType.Exact, ddlProcessCode.SelectedValue))
        If ddlTelahDitransfer.SelectedValue <> "-1" Then criterias.opAnd(New Criteria(GetType(SparePartPO), "IsTransfer", MatchType.Exact, ddlTelahDitransfer.SelectedValue))

        If Not String.IsNullOrEmpty(txtNomorPesanan.Text) Then criterias.opAnd(New Criteria(GetType(SparePartPO), "PONumber", MatchType.Exact, txtNomorPesanan.Text))

        'Dim sql As String = "(select id from SparePartPO where ( ProcessCode = 'C') and OrderType not in ('Z','Y'))" ' changed from (ProcessCode = '' or ProcessCode = 'C') 
        Dim sql As String = "(select id from SparePartPO where (ProcessCode = '' or ProcessCode = 'C') and OrderType not in ('Z','Y'))"

        criterias.opAnd(New Criteria(GetType(SparePartPO), "ID", MatchType.NotInSet, sql))

        ListSPPO = New SparePartPOFacade(User).RetrieveActiveListByCriteria(criterias, pageIndex, dtgSPPO.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
        If ListSPPO.Count > 0 Then
            'DoDownload(ListSPPO)
            DoDownloadNew(ListSPPO)
        End If
        sessHelper.SetSession("sesSPPO", ListSPPO)
    End Sub

    Private Sub BindTodtgSPPO(ByVal pageIndex As Integer)
        checkDealer()

        'Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
        Dim ListSPPO As New ArrayList
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'criterias.opAnd(New Criteria(GetType(SparePartPO), "Dealer.ID", MatchType.Exact, CType(Session("DEALER"), Dealer).ID))
        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPO), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If String.IsNullOrEmpty(txtNomorPesanan.Text) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPO), "PONumber", MatchType.Partial, txtNomorPesanan.Text.Trim))
        End If

        If txtDealerCode.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPO), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
        End If

        If chkPODate.Checked Then
            If icPODateStart.Value <= icPODateEnd.Value Then
                criterias.opAnd(New Criteria(GetType(SparePartPO), "PODate", MatchType.GreaterOrEqual, Format(icPODateStart.Value, "yyyy/MM/dd")))
                criterias.opAnd(New Criteria(GetType(SparePartPO), "PODate", MatchType.LesserOrEqual, Format(icPODateEnd.Value, "yyyy/MM/dd")))

            Else
                criterias.opAnd(New Criteria(GetType(SparePartPO), "PODate", MatchType.GreaterOrEqual, Format(Date.Now, "yyyy/MM/dd")))
                criterias.opAnd(New Criteria(GetType(SparePartPO), "PODate", MatchType.LesserOrEqual, Format(Date.Now, "yyyy/MM/dd")))
                icPODateStart.Value = Date.Now
                icPODateEnd.Value = Date.Now
            End If
        End If

        If chkSenDate.Checked Then
            If icSendDateFrom.Value <= icSendDateTo.Value Then
                criterias.opAnd(New Criteria(GetType(SparePartPO), "SentPODate", MatchType.GreaterOrEqual, Format(icSendDateFrom.Value, "yyyy/MM/dd")))
                criterias.opAnd(New Criteria(GetType(SparePartPO), "SentPODate", MatchType.LesserOrEqual, Format(icSendDateTo.Value, "yyyy/MM/dd")))

            Else
                criterias.opAnd(New Criteria(GetType(SparePartPO), "SentPODate", MatchType.GreaterOrEqual, Format(Date.Now, "yyyy/MM/dd")))
                criterias.opAnd(New Criteria(GetType(SparePartPO), "SentPODate", MatchType.LesserOrEqual, Format(Date.Now, "yyyy/MM/dd")))
                icSendDateFrom.Value = Date.Now
                icSendDateTo.Value = Date.Now
            End If
        End If

        If Not ddlTermOfPayment.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPO), "TermOfPayment.ID", MatchType.Exact, ddlTermOfPayment.SelectedValue))
        End If

        'criterias.opAnd(New Criteria(GetType(SparePartPO), "ProcessCode", MatchType.No, String.Empty))
        'criterias.opAnd(New Criteria(GetType(SparePartPO), "ProcessCode", MatchType.No, "C"))

        If ddlOrderType.SelectedValue <> "-1" Then criterias.opAnd(New Criteria(GetType(SparePartPO), "OrderType", MatchType.Exact, ddlOrderType.SelectedValue))
        If ddlProcessCode.SelectedValue <> "-1" Then criterias.opAnd(New Criteria(GetType(SparePartPO), "ProcessCode", MatchType.Exact, ddlProcessCode.SelectedValue))
        If ddlTelahDitransfer.SelectedValue <> "-1" Then criterias.opAnd(New Criteria(GetType(SparePartPO), "IsTransfer", MatchType.Exact, ddlTelahDitransfer.SelectedValue))
        If Not String.IsNullOrEmpty(txtNomorPesanan.Text) Then criterias.opAnd(New Criteria(GetType(SparePartPO), "PONumber", MatchType.Exact, txtNomorPesanan.Text))
        'Dim sql As String = "(select id from SparePartPO where ( ProcessCode = 'C') and OrderType not in ('Z','Y'))" ' changed from (ProcessCode = '' or ProcessCode = 'C') 
        Dim sql As String = "(select id from SparePartPO where (ProcessCode = '' or ProcessCode = 'C') and OrderType not in ('Z','Y'))"

        criterias.opAnd(New Criteria(GetType(SparePartPO), "ID", MatchType.NotInSet, sql))

        ListSPPO = New SparePartPOFacade(User).RetrieveActiveListByCriteria(criterias, pageIndex, dtgSPPO.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
        If ListSPPO.Count > 0 Then
            dtgSPPO.DataSource = ListSPPO
            dtgSPPO.VirtualItemCount = totalRow
            'dtgSPPO.Enabled = True
        Else
            'dtgSPPO.Enabled = False
            dtgSPPO.DataSource = New ArrayList
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Pesanan Spare Part"))
            End If
        End If
        ViewState("GrandTotal") = 0
        sessHelper.SetSession("sesSPPO", ListSPPO)
        dtgSPPO.DataBind()

        lblGrandTotal.Text = "Grand Total : " & getGrandTotal(criterias)
    End Sub

    Private Sub BindDdlPaymentType()
        Dim listOfPayments As ArrayList = New TermOfPaymentFacade(User).RetrieveActivePaymentTypeList()
        ddlTermOfPayment.DataSource = listOfPayments
        ddlTermOfPayment.DataValueField = "ID"
        ddlTermOfPayment.DataTextField = "Description"
        ddlTermOfPayment.DataBind()
        ddlTermOfPayment.Items.Insert(0, New ListItem("Pilih Cara Pembayaran", ""))
    End Sub

    Private Function GetSentItem() As ArrayList
        Dim objSparePartPO As SparePartPO
        Dim arlItemCanceled As ArrayList = New ArrayList
        dtgSPPO.DataSource = CType(Session("sesSPPO"), ArrayList)

        Dim ArrDealer As New ArrayList
        Dim StrQuery As String
        StrQuery = "(SELECT DISTINCT x.DealerID FROM dbo.SparePartPOScheduleDealer X WHERE )"

        'Objcrit.opAnd(New Criteria(GetType(SparePartPOSchedule), "ID", MatchType.InSet, "(" & strQuery & ")"))
        ' Objcrit.opAnd(New Criteria(GetType(SparePartPOScheduleTime), "ScheduleTime", MatchType.Exact, GetTime(Me.CurrentTime)))

        Dim ObjSPOSDFacade As New SparePartPOScheduleDealerFacade(User)

        For Each dgItem As DataGridItem In dtgSPPO.Items
            Dim intItemIndex As Integer = dgItem.ItemIndex
            objSparePartPO = New SparePartPOFacade(User).Retrieve(CType(dgItem.Cells(0).Text, Integer)) 'CType(CType(dtgSPPO.DataSource, ArrayList)(intItemIndex), SparePartPO)
            If CType(dgItem.Cells(9).FindControl("chkCanceled"), CheckBox).Checked Then

                If objSparePartPO.OrderType = "I" Then
                    Continue For
                End If

                If objSparePartPO.OrderType = "Z" OrElse objSparePartPO.OrderType = "R" Then ' AndAlso objSparePartPO.CancelRequestBy <> String.Empty AndAlso Left 


                    If objSparePartPO.OrderType = "R" AndAlso objSparePartPO.IsTransfer = 0 AndAlso objSparePartPO.ProcessCode = "S" Then

                        If IsValidSPOS(objSparePartPO.OrderType, objSparePartPO.Dealer.ID) Then

                            objSparePartPO.IsTransfer = True
                            arlItemCanceled.Add(objSparePartPO)
                        End If

                    ElseIf objSparePartPO.OrderType = "Z" AndAlso objSparePartPO.IsTransfer = 0 AndAlso objSparePartPO.ProcessCode = "" Then
                        If IsValidSPOS(objSparePartPO.OrderType, objSparePartPO.Dealer.ID) Then
                            objSparePartPO.SentPODate = Date.Today
                            objSparePartPO.ProcessCode = "S"
                            objSparePartPO.IsTransfer = True
                            arlItemCanceled.Add(objSparePartPO)
                        End If

                    End If
                Else
                    If objSparePartPO.ProcessCode = String.Empty AndAlso objSparePartPO.IsTransfer = 0 Then
                        objSparePartPO.SentPODate = Date.Today
                        objSparePartPO.ProcessCode = "S"
                        objSparePartPO.IsTransfer = True
                        arlItemCanceled.Add(objSparePartPO)
                    End If


                End If
            End If
        Next
        If arlItemCanceled.Count > 0 Then
            Return arlItemCanceled
        Else
            Return Nothing
        End If

    End Function

    Private Function GetCanceledItem() As ArrayList

        Dim objSparePartPO As SparePartPO
        Dim arlItemCanceled As ArrayList = New ArrayList
        dtgSPPO.DataSource = CType(Session("sesSPPO"), ArrayList)
        For Each dgItem As DataGridItem In dtgSPPO.Items
            Dim intItemIndex As Integer = dgItem.ItemIndex
            objSparePartPO = New SparePartPOFacade(User).Retrieve(CType(dgItem.Cells(0).Text, Integer)) 'CType(CType(dtgSPPO.DataSource, ArrayList)(intItemIndex), SparePartPO)
            If CType(dgItem.Cells(9).FindControl("chkCanceled"), CheckBox).Checked Then
                If objSparePartPO.ProcessCode = String.Empty OrElse objSparePartPO.ProcessCode = "S" Then ' AndAlso objSparePartPO.CancelRequestBy <> String.Empty AndAlso Left(objSparePartPO.CancelRequestBy, 1) <> "-" Then
                    arlItemCanceled.Add(objSparePartPO)
                End If
            End If
        Next
        If arlItemCanceled.Count > 0 Then
            Return arlItemCanceled
        Else
            Return Nothing
        End If

    End Function

    Private Function GetIntWeek() As Integer

        Select Case Date.Now.DayOfWeek
            Case DayOfWeek.Monday
                Return 0
            Case DayOfWeek.Tuesday
                Return 1
            Case DayOfWeek.Wednesday
                Return 2
            Case DayOfWeek.Thursday
                Return 3
            Case DayOfWeek.Friday
                Return 4
            Case DayOfWeek.Saturday
                Return 5
            Case DayOfWeek.Sunday
                Return 6

        End Select

        Return -1
    End Function

    Private Function IsValidSPOS(ByVal parOrderType As String, ByVal parDealerID As Integer) As Boolean

        Dim Objcrit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPOScheduleDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Objcrit.opAnd(New Criteria(GetType(SparePartPOScheduleDealer), "SparePartPOSchedule.Status", MatchType.Exact, 1))
        Objcrit.opAnd(New Criteria(GetType(SparePartPOScheduleDealer), "Dealer.ID", MatchType.Exact, parDealerID))
        Objcrit.opAnd(New Criteria(GetType(SparePartPOScheduleDealer), "SparePartPOSchedule.OrderDay", MatchType.Exact, GetIntWeek()))
        Objcrit.opAnd(New Criteria(GetType(SparePartPOScheduleDealer), "SparePartPOSchedule.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Objcrit.opAnd(New Criteria(GetType(SparePartPOScheduleDealer), "SparePartPOSchedule.OrderType", MatchType.Exact, parOrderType))

        Dim arrSched As New ArrayList
        Dim ObjF As New SparePartPOScheduleDealerFacade(User)
        arrSched = ObjF.Retrieve(Objcrit)

        If Not IsNothing(Objcrit) AndAlso arrSched.Count > 0 Then
            Return True
        End If
        Return False

    End Function

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindDdlPaymentType()
            If Not SecurityProvider.Authorize(Context.User, SR.ViewSPPO_KTBCancel_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Pemesanan")
            End If
            ViewState("currSortColumn") = ""
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            BindHeader()

            Dim criteria As Hashtable
            criteria = Session("CriteriaFormSPPOList")
            If Not criteria Is Nothing Then
                criteria = CType(criteria, Hashtable)
                Me.ddlOrderType.SelectedValue = CType(criteria.Item("OrderType"), String)
                Me.ddlProcessCode.SelectedValue = CType(criteria.Item("ProcessCode"), String)
                Me.icPODateStart.Value = CType(criteria.Item("PODateStart"), Date)
                Me.icPODateEnd.Value = CType(criteria.Item("PODateEnd"), Date)

                Me.chkPODate.Checked = CType(criteria.Item("chkPODate"), Boolean)
                Me.chkSenDate.Checked = CType(criteria.Item("chkSenDate"), Boolean)

                Me.icSendDateFrom.Value = CType(criteria.Item("SendDateFrom"), Date)
                Me.icSendDateTo.Value = CType(criteria.Item("SendDateTo"), Date)
            End If

            BindTodtgSPPO(1)
            lblSearchDealer.Attributes("onClick") = "ShowPPDealerSelection();"
            btnProcess.Attributes.Add("onClick", "return confirm('Yakin Pesanan yg dipilih akan dibatalkan ?');")
            'dtgSPPO.DataSource = New ArrayList
            'dtgSPPO.DataBind()
        End If
        ActivateUserPrivilege()
    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click


        If chkPODate.Checked = False AndAlso chkSenDate.Checked = False Then
            MessageBox.Show("Harus memlih salah satu parameter tanggal")
            Return
        End If
        Dim criteria As Hashtable = New Hashtable(8)

        criteria.Add("OrderType", Me.ddlOrderType.SelectedValue)
        criteria.Add("ProcessCode", Me.ddlProcessCode.SelectedValue)
        criteria.Add("PODateStart", Me.icPODateStart.Value)
        criteria.Add("PODateEnd", Me.icPODateEnd.Value)

        criteria.Add("chkPODate", Me.chkPODate.Checked)

        criteria.Add("SendDateFrom", Me.icSendDateFrom.Value)
        criteria.Add("SendDateTo", Me.icSendDateTo.Value)
        criteria.Add("chkSenDate", Me.chkSenDate.Checked)

        '_sessHelper.SetSession("CriteriaFormSPPOList", criteria)
        'Todo session
        Session.Add("CriteriaFormSPPOList", criteria)
        dtgSPPO.CurrentPageIndex = 0
        BindTodtgSPPO(dtgSPPO.CurrentPageIndex)
    End Sub

    Private Sub dtgSPPO_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSPPO.PageIndexChanged
        dtgSPPO.CurrentPageIndex = e.NewPageIndex
        BindTodtgSPPO(e.NewPageIndex + 1)
    End Sub

    Private Function CheckStatusPesananDealer(ByVal arrItems As ArrayList) As Boolean
        Dim item As SparePartPO
        Dim ProcessCodeDesc As String
        For Each item In arrItems
            If item.ProcessCodeDesc.ToLower <> "baru" Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        checkDealer()
        Dim arlCanceledItems As ArrayList = GetCanceledItem()
        'If CheckStatusPesananDealer(arlCanceledItems) Then
        '    MessageBox.Show("Pembatalan hanya dapat dilakukan untuk Pesanan dengan status Baru")
        'Else
        If Not IsNothing(arlCanceledItems) Then
            Dim transResult As Integer = New SparePartPOFacade(User).UpdateSparePartPO(arlCanceledItems, "X")
            If transResult = 1 Then
                MessageBox.Show(SR.UpdateSucces)
                BindTodtgSPPO(1)
            Else
                MessageBox.Show(SR.UploadFail("Batal PO"))
            End If
        Else
            MessageBox.Show(SR.DataProcessNotFound("Sparepart PO", "Batal oleh MMKSI"))
        End If
        'End If
    End Sub

    Private Sub btnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSend.Click
        checkDealer()
        Dim arlSentItems As ArrayList = GetSentItem()
        If Not IsNothing(arlSentItems) Then
            If CreateTextFileForKTB(arlSentItems) Then
                Dim transResult As Integer = New SparePartPOFacade(User).UpdateSparePartPO(arlSentItems, "S")
                If transResult = 1 Then
                    MessageBox.Show(SR.UpdateSucces)
                    BindTodtgSPPO(1)
                Else
                    MessageBox.Show(SR.UploadFail("Batal PO"))
                End If
            End If
        Else
            MessageBox.Show(SR.DataProcessNotFound("Sparepart PO", "kirim"))
        End If

    End Sub

    Private Sub dtgSPPO_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSPPO.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dtgSPPO.PageSize * dtgSPPO.CurrentPageIndex)).ToString
        End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblPopUp As Label = CType(e.Item.Cells(9).FindControl("lblDetail"), Label)
            lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpSPPODetail.aspx?poid=" + e.Item.Cells(0).Text, "", 510, 700, "SparePartPO")

            Dim chkCancel As CheckBox = CType(e.Item.Cells(10).FindControl("chkCanceled"), CheckBox)
            Dim objPO As SparePartPO = New SparePartPOFacade(User).Retrieve(CType(e.Item.Cells(0).Text, Integer))
            Dim lblIstransfer As Label = CType(e.Item.Cells(9).FindControl("lblIstransfer"), Label)

            If Year(objPO.SentPODate) <> 1753 Then
                If objPO.IsTransfer = 0 Then
                    lblIstransfer.Text = ""
                Else
                    lblIstransfer.Text = "Telah Ditransfer"
                End If
            End If
            If objPO.OrderType = "I" Then
                If Year(objPO.SentPODate) = 1753 Then
                    'If objPO.IsTransfer = 0 Then
                    lblIstransfer.Text = ""
                Else
                    lblIstransfer.Text = "Telah Ditransfer"
                    'End If
                End If
                chkCancel.Enabled = False
            End If
            'Dim lblStatus As Label = CType(e.Item.Cells(7).FindControl("lblProcessCode"), Label)
            'lblStatus.Text.Trim.ToUpper = "TELAH DIKIRIM" Then
            'If objPO.ProcessCode = "S" Then AndAlso objPO.CancelRequestBy <> String.Empty AndAlso Left(objPO.CancelRequestBy, 1) <> "-" Then
            If (objPO.ProcessCode = String.Empty OrElse objPO.ProcessCode = "S") AndAlso Left(objPO.CancelRequestBy, 1) <> "-" Then
                chkCancel.Enabled = True
                'e.Item.ForeColor = System.Drawing.Color.YellowGreen
                e.Item.BackColor = System.Drawing.Color.FromName("#FFFF99")
                If objPO.OrderType = "I" Then
                    chkCancel.Enabled = False
                End If
            Else
                chkCancel.Enabled = False
                e.Item.ForeColor = System.Drawing.Color.Black
            End If

            'anh 20111116
            Dim lblPODate As Label = CType(e.Item.Cells(5).FindControl("lblPODate"), Label)
            lblPODate.Text = objPO.PODate.ToString("dd/MM/yyyy")

            Dim lblSentPODate As Label = CType(e.Item.Cells(6).FindControl("lblSentPODate"), Label)
            If objPO.SentPODate.Year > 1900 Then
                lblSentPODate.Text = objPO.SentPODate.ToString("dd/MM/yyyy")
            End If

            Dim OrderedAmount As Decimal = 0
            Dim arlDetail As ArrayList = objPO.SparePartPODetails
            For Each spPODet As SparePartPODetail In arlDetail
                OrderedAmount += spPODet.Amount
            Next

            Dim lblNilaiPemesanan As Label = CType(e.Item.FindControl("lblNilaiPemesanan"), Label)
            lblNilaiPemesanan.Text = OrderedAmount.ToString("#,##0")
        End If
    End Sub

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        If chkPODate.Checked = False AndAlso chkSenDate.Checked = False Then
            MessageBox.Show("Harus memlih salah satu parameter tanggal")
            Return
        End If
        Dim criteria As Hashtable = New Hashtable(8)

        criteria.Add("OrderType", Me.ddlOrderType.SelectedValue)
        criteria.Add("ProcessCode", Me.ddlProcessCode.SelectedValue)
        criteria.Add("PODateStart", Me.icPODateStart.Value)
        criteria.Add("PODateEnd", Me.icPODateEnd.Value)

        criteria.Add("chkPODate", Me.chkPODate.Checked)

        criteria.Add("SendDateFrom", Me.icSendDateFrom.Value)
        criteria.Add("SendDateTo", Me.icSendDateTo.Value)
        criteria.Add("chkSenDate", Me.chkSenDate.Checked)

        Session.Add("CriteriaFormSPPOList", criteria)
        BindTodtgSPPOAll(dtgSPPO.CurrentPageIndex)
    End Sub

    Private Sub ddlTelahDitransfer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTelahDitransfer.SelectedIndexChanged
        ViewState("DdlTelahDitransfer") = ddlOrderType.SelectedValue
    End Sub

    Private Sub dtgSPPO_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSPPO.SortCommand
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
        dtgSPPO.CurrentPageIndex = 0
        BindTodtgSPPO(dtgSPPO.CurrentPageIndex + 1)
    End Sub

    Private Sub ActivateUserPrivilege()
        '--exclude  this privilege from Asra (BA)
        'btnFind.Visible = SecurityProvider.Authorize(Context.User, SR.SearchSPPO_KTBCancel_Privilege)
        btnProcess.Visible = SecurityProvider.Authorize(Context.User, SR.ProcessSPPO_KTBCancel_Privilege)
        btnSend.Visible = SecurityProvider.Authorize(Context.User, SR.KirimDaftarPemesananOthers_Privilege)
    End Sub

    Private Function CreateTextFileForKTB(ByVal _arlSparePartPO As ArrayList) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _sapServer As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _sapServer)
        Dim succes As Boolean = False
        Try
            succes = imp.Start
            If succes Then
                For Each objPO As SparePartPO In _arlSparePartPO
                    Dim FOLDER_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("DNetServerFolder") & objPO.PONumber.Substring(1, 4)
                    Dim FILE_NAME As String = FOLDER_NAME + "\" + objPO.PONumber + IIf(objPO.OrderType = "R", ".DAT", ".SPC") '".SPC"
                    objPO.ProcessCode = "S"
                    Dim nResult As Integer = New SparePartPOFacade(User).UpdateSPPOProcessCode(objPO)
                    If nResult <> -1 Then
                        CreateFolder(FOLDER_NAME)
                        If System.IO.File.Exists(FILE_NAME) Then
                            System.IO.File.Delete(FILE_NAME)
                        End If
                        '/   If Not (objPO.OrderType.ToLower().Equals("r") OrElse objPO.OrderType.ToLower().Equals("z")) Then
                        Dim fs As System.IO.FileStream = New System.IO.FileStream(FILE_NAME, System.IO.FileMode.CreateNew)
                        Dim w As System.IO.StreamWriter = New System.IO.StreamWriter(fs)

                        WritePOHeaderToFile(w, objPO)
                        WritePODetailToFile(w, objPO)

                        w.Close()
                        fs.Close()
                        'End If


                    Else
                        MessageBox.Show("Proses tidak berhasil, silahkan beberapa saat lagi.")
                    End If
                Next
                imp.StopImpersonate()
                imp = Nothing
                Return True
                'MessageBox.Show(ChangeSPPOStatus("S"))
            Else
                MessageBox.Show("Gagal Login ke SAP Server.")
                Return False
            End If

        Catch ex As Exception
            MessageBox.Show(SR.DataSendFail & ex.Message)
            Return False
        End Try
    End Function

    Private Sub DoDownloadNew(ByVal datasAllo As ArrayList)
        Dim pck As New ExcelPackage

        pck = GenerateREport(datasAllo)

        Dim Apendix As String = Guid.NewGuid().ToString()
        Dim fileDownloadName = "REportCSPerf" & DateTime.Now.ToString("yyyyMMddHHmm") + Apendix.Substring(0, 4) + ".xlsx"
        Dim contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        Dim fileStream As New MemoryStream()
        'pck.SaveAs(fileStream)
        'fileStream.Position = 0

        Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & fileDownloadName



        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)


        If imp.Start() Then
            pck.SaveAs(New FileInfo(Server.MapPath("~/DataTemp/" & fileDownloadName)))
            imp.StopImpersonate()
            imp = Nothing
        Else
            'imp.StopImpersonate()
            imp = Nothing
            MessageBox.Show(SR.DownloadFail("Report"))
        End If


        Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & fileDownloadName)

    End Sub

    Private Function GenerateREport(ByVal ds As ArrayList) As ExcelPackage

        Dim pck As New ExcelPackage

        GenerateReportGeneral(ds, "Header SparePartPO", pck)
        GenerateReportGeneralDetail(ds, "Detail SparePartPO", pck)
        Return pck

    End Function

    Private Sub GenerateReportGeneral(ByVal arr As ArrayList, ByVal TitleName As String, ByRef pck As ExcelPackage)

        Dim ws = pck.Workbook.Worksheets.Add(TitleName)

        'Report Title
        ws.Cells(1, 1).Value = "Daftar SparePart PO"


        Dim i As Integer = 3
        Dim a As Integer = 1

        'Report Header
        'Formating CommonHeader
        'For x As Integer = 1 To 9
        '    ws.Column(x).Width = autofi
        'Next

        'Formating Header
        ws.Cells(i, 1, i, 9).Style.Font.Bold = True
        ws.Cells(i, 1, i, 9).Style.Fill.PatternType = ExcelFillStyle.Solid
        ws.Cells(i, 1, i, 9).Style.Fill.BackgroundColor.SetColor(Color.LightGray)
        ws.Cells(i, 1, i, 9).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
        ws.Cells(i, 1, i, 9).Style.VerticalAlignment = ExcelVerticalAlignment.Center


        ws.Cells(i, 1).Value = "No" ' row 3 col 1
        ws.Cells(i, 2).Value = "Kode Dealer"
        ws.Cells(i, 3).Value = "Nama Dealer"
        ws.Cells(i, 4).Value = "Nomor Pesanan"
        ws.cells(i, 5).value = "Tanggal Pesanan"
        ws.cells(i, 6).value = "Tanggal Kirim"
        ws.cells(i, 7).value = "Jenis Pesanan"
        ws.cells(i, 8).value = "Cara Pembayaran"
        ws.cells(i, 9).value = "Telah Ditransfer"

        'Header row2

        For Each row As SparePartPO In arr
            i = i + 1
            ws.cells(i, 1).value = a
            ws.cells(i, 2).value = row.Dealer.DealerCode
            ws.cells(i, 3).value = row.Dealer.DealerName
            ws.cells(i, 4).value = row.PONumber
            ws.cells(i, 5).value = row.CreatedTime.ToString("dd/MM/yyyy")
            ws.cells(i, 6).value = row.SentPODate.ToString("dd/MM/yyyy")
            ws.cells(i, 7).value = CommonFunction.GetEnumDescriptionChar(row.OrderType, "SPPOOrderType.EnumOrderType")
            ws.cells(i, 8).value = row.TermOfPayment.Description
            ws.cells(i, 9).value = IIf(row.IsTransfer = 0, "", "Telah DiTransfer")
            a = a + 1
        Next


        'Formating Common
        For cw As Integer = 1 To 9
            ws.Column(cw).AutoFit()
        Next


        ''Formating Score

        'ws.Cells(10, 7, idxD, ILastCol).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right
        'ws.Cells(10, 7, idxD, ILastCol).Style.VerticalAlignment = ExcelVerticalAlignment.Center
        'ws.Cells(10, 7, idxD, ILastCol).Style.Numberformat.Format = "#,##0.0000"


        'Formating Table
        Dim modelTable = ws.Cells(4, 1, i, 9)
        modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin
        modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin
        modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin
        modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin
        modelTable.AutoFitColumns()

    End Sub


    Private Sub GenerateReportGeneralDetail(ByVal arr As ArrayList, ByVal TitleName As String, ByRef pck As ExcelPackage)

        Dim ws = pck.Workbook.Worksheets.Add(TitleName)
        Dim arrDtl As New ArrayList
        For Each row As SparePartPO In arr
            For Each rowdtl As SparePartPODetail In row.SparePartPODetails
                arrDtl.Add(rowdtl)
            Next
        Next


        'Report Title
        ws.Cells(1, 1).Value = "Daftar Detail SparePart PO"

        Dim i As Integer = 3
        Dim a As Integer = 1

        'Report Header
        'Formating CommonHeader
        For x As Integer = 1 To 9
            ws.Column(x).Width = 50
        Next

        'Formating Header
        ws.Cells(i, 1, i, 7).Style.Font.Bold = True
        ws.Cells(i, 1, i, 7).Style.Fill.PatternType = ExcelFillStyle.Solid
        ws.Cells(i, 1, i, 7).Style.Fill.BackgroundColor.SetColor(Color.LightGray)
        ws.Cells(i, 1, i, 7).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
        ws.Cells(i, 1, i, 7).Style.VerticalAlignment = ExcelVerticalAlignment.Center


        ws.Cells(i, 1).Value = "No" ' row 3 col 1
        ws.Cells(i, 2).Value = "Nomor PO"
        ws.Cells(i, 3).Value = "Part Number"
        ws.Cells(i, 4).Value = "Part Name"
        ws.Cells(i, 5).Value = "QTY"
        ws.cells(i, 6).value = "Harga Retail"
        ws.cells(i, 7).value = "SubTotal"

        'Header row2
        For Each rowSppo As SparePartPODetail In arrDtl
            i = i + 1
            ws.cells(i, 1).value = a
            ws.cells(i, 2).value = rowSppo.SparePartPO.PONumber
            ws.cells(i, 3).value = rowSppo.SparePartMaster.PartNumber
            ws.cells(i, 4).value = rowSppo.SparePartMaster.PartName
            ws.cells(i, 5).value = rowSppo.Quantity.ToString("N0")
            ws.cells(i, 6).value = rowSppo.RetailPrice.ToString("N0")
            ws.cells(i, 7).value = (rowSppo.RetailPrice * rowSppo.Quantity).ToString("N0")
            a = a + 1
        Next


        'Formating Common
        For cw As Integer = 1 To 9
            ws.Column(cw).AutoFit()
        Next

        'Formating Header
        'ws.Cells(1, 1, idRHStart + 2, ILastCol).Style.Font.Bold = True
        'ws.Cells(idRHStart, 1, idRHStart + 2, ILastCol).Style.Fill.PatternType = ExcelFillStyle.Solid
        'ws.Cells(idRHStart, 1, idRHStart + 2, ILastCol).Style.Fill.BackgroundColor.SetColor(Color.LightGray)
        'ws.Cells(idRHStart, 1, idRHStart + 2, ILastCol).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
        'ws.Cells(idRHStart, 1, idRHStart + 2, ILastCol).Style.VerticalAlignment = ExcelVerticalAlignment.Center

        ''Formating Score

        'ws.Cells(10, 7, idxD, ILastCol).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right
        'ws.Cells(10, 7, idxD, ILastCol).Style.VerticalAlignment = ExcelVerticalAlignment.Center

        'ws.Cells(10, 7, idxD, ILastCol).Style.Numberformat.Format = "#,##0.0000"


        ''Formating Table
        'Dim modelTable = ws.Cells(idRHStart, 1, idxD, ILastCol)
        'modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin
        'modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin
        'modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin
        'modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin
        'modelTable.AutoFitColumns()

    End Sub


    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        Dim strFileNmHeader As String
        Dim strFileNm As String = "CSEmployee"

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
            WriteListData(sw, data)
            sw.Close()
            fs.Close()
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")
        Catch ex As Exception
            Dim dummy As Integer = 0
            MessageBox.Show("Download data gagal. " & ex.Message)
        End Try
    End Sub

    Private Sub WriteListData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Dim err As String
        Dim itemLine As StringBuilder = New StringBuilder
        Dim objSmanFacade As New SparePartPOFacade(User)

        Try


            If Not IsNothing(data) Then
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append("Daftar SparePart PO")
                sw.WriteLine(itemLine)
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(" " & tab & tab)
                sw.WriteLine(itemLine)
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append("No" & tab)
                itemLine.Append("Kode Dealer" & tab)
                itemLine.Append("Nama Dealer" & tab)
                itemLine.Append("Nomor Pesanan " & tab)
                itemLine.Append("Tanggal Pesanan" & tab)
                itemLine.Append("Tanggal Kirim" & tab)
                itemLine.Append("Jenis Pesanan" & tab)
                itemLine.Append("Term Of Payment" & tab)
                itemLine.Append("Telah diTransfer" & tab)
                sw.WriteLine(itemLine.ToString())

                Dim i As Integer = 1
                For Each item As SparePartPO In data
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(i.ToString & tab)
                    itemLine.Append(item.Dealer.DealerCode & tab)
                    itemLine.Append(item.Dealer.DealerName & tab)
                    itemLine.Append(item.PONumber & tab)
                    itemLine.Append(Format(item.CreatedTime, "dd/MM/yyyy").ToString & tab)
                    itemLine.Append(Format(item.SentPODate, "dd/MM/yyyy").ToString & tab)
                    itemLine.Append(item.OrderType & tab)
                    itemLine.Append(item.TermOfPayment.Description & tab)
                    itemLine.Append(IIf(item.IsTransfer = 0, "", "Telah DiTransfer"))
                    sw.WriteLine(itemLine.ToString())
                    i = i + 1

                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message + "  //  " + " Data Berikut :  " + err + "  Invalid")
        End Try
    End Sub

    Private Sub CreateFolder(ByVal folderName As String)
        Dim dirInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(folderName)
        If Not dirInfo.Exists Then
            dirInfo.Create()
        End If
    End Sub

    Private Sub WritePOHeaderToFile(ByRef w As System.IO.StreamWriter, ByVal objSPO As SparePartPO)
        Dim sbSetARecord As StringBuilder = New StringBuilder
        Dim pad As Char = " "
        sbSetARecord.Append("T")
        sbSetARecord.Append(objSPO.PONumber.PadRight(15, pad))
        sbSetARecord.Append(Left(objSPO.Dealer.DealerName, 25).PadRight(25, pad))
        sbSetARecord.Append(String.Format("{0:yyyyMMdd}", objSPO.PODate))
        sbSetARecord.Append(objSPO.SparePartPODetails.Count.ToString.PadLeft(4, "0"))

        If objSPO.OrderType = "Z" Then
            sbSetARecord.Append("0".ToString.PadLeft(22, "0"))
            sbSetARecord.Append(String.Format("{0:yyyyMMdd}", objSPO.DeliveryDate))
            If objSPO.PickingTicket.Length > 30 Then
                sbSetARecord.Append(objSPO.PickingTicket.Substring(0, 30))
            Else
                sbSetARecord.Append(objSPO.PickingTicket.PadRight(30, pad))
            End If
        End If

        If objSPO.OrderType = "R" Or objSPO.OrderType = "I" Or objSPO.OrderType = "Z" Then
            If Not IsNothing(objSPO.TermOfPayment) Then
                sbSetARecord.Append(objSPO.TermOfPayment.TermOfPaymentCode)
            End If
        End If

        If objSPO.OrderType = "Y" Then
            sbSetARecord.Append("0".ToString.PadLeft(22, "0"))
            sbSetARecord.Append(String.Format("{0:yyyyMMdd}", objSPO.DeliveryDate))

            If objSPO.PickingTicket.Length > 30 Then
                sbSetARecord.Append(objSPO.PickingTicket.Substring(0, 30))
            Else
                sbSetARecord.Append(objSPO.PickingTicket.PadRight(30, pad))
            End If
        End If

        w.WriteLine(sbSetARecord.ToString)
    End Sub

    Private Function WritePODetailToFile(ByRef w As System.IO.StreamWriter, ByVal objSPO As SparePartPO)
        Dim sbSetARecord As StringBuilder = New StringBuilder
        Dim pad As Char = " "
        Dim indek As Integer = 0
        For Each objPODetail As SparePartPODetail In objSPO.SparePartPODetails
            indek = indek + 1
            sbSetARecord.Remove(0, sbSetARecord.Length)
            sbSetARecord.Append("D")
            sbSetARecord.Append(objPODetail.SparePartPO.PONumber.PadRight(15, pad))
            sbSetARecord.Append(objPODetail.SparePartMaster.PartNumber.PadRight(20, pad))
            sbSetARecord.Append(objPODetail.Quantity.ToString.PadLeft(5, "0"))
            sbSetARecord.Append(String.Format("{0:yyyyMMdd}", objPODetail.SparePartPO.PODate)) '(objPODetail.SparePartPO.PODate.ToString.Format("{0:yyyyMMdd}"))
            sbSetARecord.Append(indek.ToString.PadLeft(4, "0"))
            w.WriteLine(sbSetARecord.ToString)
        Next
    End Function

#End Region

    Protected Sub chkCheckAll_CheckedChanged(sender As Object, e As EventArgs)
        For Each row As DataGridItem In dtgSPPO.Items
            Dim chkbox As CheckBox = row.FindControl("chkCanceled")
            If (chkbox IsNot Nothing AndAlso chkbox.Enabled) Then
                chkbox.Checked = CType(sender, CheckBox).Checked
            End If
        Next
    End Sub

    Private Function getGrandTotal(criterias As CriteriaComposite) As String
        Dim _ret As Decimal = 0
        Dim ListSPPO As ArrayList = New SparePartPOFacade(User).Retrieve(criterias)
        For Each spPO As SparePartPO In ListSPPO
            Dim OrderedAmount As Decimal = 0
            Dim arlDetail As ArrayList = spPO.SparePartPODetails
            For Each spPODet As SparePartPODetail In arlDetail
                _ret += spPODet.Amount
            Next
        Next
        Return _ret.ToString("#,##0")
    End Function

End Class