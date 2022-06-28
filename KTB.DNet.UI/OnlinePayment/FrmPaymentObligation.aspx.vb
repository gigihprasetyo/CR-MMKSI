#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.OnlinePayment
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.UserManagement
Imports System.Text
Imports System.Web.Mail
Imports System.Security.Principal
Imports System.Web.Security
Imports System.Configuration
Imports System.IO
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region


Public Class FrmPaymentObligation
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents btnProcess As System.Web.UI.WebControls.Button
    Protected WithEvents dtgListPaymentObligation As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents icOrderDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents listParrent As System.Web.UI.WebControls.DataList
    Protected WithEvents txtAssignment As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlPaymentType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents icFromDocDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icToDocDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents ddlAssignmentType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents TabCol0 As System.Web.UI.HtmlControls.HtmlTableCell
    Private identity As IIdentity

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private _sesshelper As New SessionHelper
    Private ArlPaymentObl As ArrayList = New ArrayList
    Private ArlPaymentOblParent As ArrayList = New ArrayList
    Private TIPEDOC As String = String.Empty
    Private oLoginUserInfo As UserInfo
    Private TotalAmmount As Long

    Private bValidatePriv As Boolean
#End Region


#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        TIPEDOC = Request.QueryString("TIPE")

        InitiateAuthorization()

        If TIPEDOC.ToUpper.Trim = "MANUAL" Then
            lblPageTitle.Text = "INFORMASI PEMBAYARAN - Daftar Pembayaran Manual"
        ElseIf TIPEDOC.ToUpper.Trim = "SAP" Then
            lblPageTitle.Text = "INFORMASI PEMBAYARAN - Daftar Pembayaran"
        End If

        oLoginUserInfo = CType(_sesshelper.GetSession("LOGINUSERINFO"), UserInfo)

        If oLoginUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            txtKodeDealer.Visible = False
            lblSearchDealer.Visible = False

            lblKodeDealer.Visible = True
            txtKodeDealer.Text = oLoginUserInfo.Dealer.DealerCode
            lblKodeDealer.Text = txtKodeDealer.Text
        Else
            txtKodeDealer.Visible = True
            lblSearchDealer.Visible = True

            lblKodeDealer.Visible = False
            lblKodeDealer.Text = ""
        End If

        If Not IsPostBack Then
            lblSearchDealer.Attributes.Add("onclick", "ShowPPDealerSelection();return false;")
            BindDDList()
            GetSessionCriteria()
            BindToGrid(0, TIPEDOC)
        End If
    End Sub
    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        If isvalidPage() Then
            BindToGrid(0, TIPEDOC)
        End If
    End Sub
    Private Sub listParrent_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles listParrent.ItemDataBound
        If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then

            Dim oPaymentObl As PaymentObligation = e.Item.DataItem
            Dim dtgDetail As DataGrid = CType(e.Item.FindControl("dtgListPaymentObligation"), DataGrid)
            Dim arlDetails As New ArrayList
            Dim criteriaDetails As CriteriaComposite = GetCriteriasForDetails(oPaymentObl.Dealer.ID, oPaymentObl.Assignment, oPaymentObl.PaymentAssignmentType.ID, oPaymentObl.SourceDocument, oPaymentObl.IsTOP)
            arlDetails = New PaymentObligationFacade(User).Retrieve(criteriaDetails)
            dtgDetail.DataSource = arlDetails
            dtgDetail.DataBind()

            TotalAmmount += oPaymentObl.TotalAmount
        ElseIf (e.Item.ItemType = ListItemType.Header) Then


        End If
    End Sub
    Private Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Dim i As Integer = 0
        Dim arrListParent As New ArrayList
        Dim objParent As PaymentObligation

        For Each item As DataListItem In listParrent.Items
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If chk.Checked Then
                objParent = CType(CType(_sesshelper.GetSession("ListPaymentOblParent"), ArrayList)(i), PaymentObligation)
                arrListParent.Add(objParent)
            End If
            i += 1
        Next

        If arrListParent.Count > 0 Then
            _sesshelper.SetSession("ListSelectedPaymentOblParent", arrListParent)
            'show pop up form
            SetSessionCriteria()
            Server.Transfer("~/OnlinePayment/FrmValidatePaymentObligation.aspx?tipe=" & TIPEDOC & "&tipeassign=" & ddlAssignmentType.SelectedItem.Text)
            'MessageBox.Show("Show pop up Process Form")
        Else
            MessageBox.Show("Tidak ada data yg di pilih")
        End If
    End Sub

#End Region

#Region "Custhom Method"
#Region "Check Privilage"
    Private Sub InitiateAuthorization()
        If TIPEDOC.ToUpper.Trim = "MANUAL" Then
            If Not SecurityProvider.Authorize(context.User, SR.Pembayaran_daftar_pembayaran_manual_lihat_privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=INFORMASI PEMBAYARAN - Daftar Pembayaran Manual")
            End If
        ElseIf TIPEDOC.ToUpper.Trim = "SAP" Then
            If Not SecurityProvider.Authorize(context.User, SR.Pembayaran_daftar_pembayaran_sap_lihat_privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=INFORMASI PEMBAYARAN - Daftar Pembayaran")
            End If
        End If

        bValidatePriv = SecurityProvider.Authorize(context.User, SR.Pembayaran_daftar_pembayaran_sap_validasi_privilege)

    End Sub

#End Region

    Private Sub BindToGrid(ByVal idx As Integer, ByVal Tipe As String)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Tipe.ToUpper.Trim = "MANUAL" Then
            criterias.opAnd(New Criteria(GetType(PaymentObligation), "SourceDocument", MatchType.Exact, CInt(EnumOnlinePayment.SourceDocument.MANUAL)))
        ElseIf Tipe.ToUpper.Trim = "SAP" Then
            criterias.opAnd(New Criteria(GetType(PaymentObligation), "SourceDocument", MatchType.Exact, CInt(EnumOnlinePayment.SourceDocument.SAP)))
        End If

        If oLoginUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(PaymentObligation), "Dealer.DealerCode", MatchType.Exact, lblKodeDealer.Text.Trim))
        ElseIf oLoginUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If txtKodeDealer.Text <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(PaymentObligation), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text.Trim()))
            End If
        End If


        'If ddlStatus.SelectedIndex > 0 Then
        '    criterias.opAnd(New Criteria(GetType(PaymentObligation), "Status", MatchType.Exact, CInt(ddlStatus.SelectedValue)))
        'End If
        criterias.opAnd(New Criteria(GetType(PaymentObligation), "Status", MatchType.Exact, CInt(EnumOnlinePayment.StatusOnlinePayment.Baru)))

        If ddlPaymentType.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(PaymentObligation), "IsTOP", MatchType.Exact, CInt(ddlPaymentType.SelectedValue)))
        End If

        If ddlAssignmentType.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(PaymentObligation), "PaymentAssignmentType.ID", MatchType.Exact, CInt(ddlAssignmentType.SelectedValue)))
        End If

        criterias.opAnd(New Criteria(GetType(PaymentObligation), "DocDate", MatchType.GreaterOrEqual, icFromDocDate.Value))
        criterias.opAnd(New Criteria(GetType(PaymentObligation), "DocDate", MatchType.LesserOrEqual, icToDocDate.Value))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PaymentObligation), "DocDate", Sort.SortDirection.ASC))
        sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PaymentObligation), "Status", Sort.SortDirection.ASC))

        ArlPaymentObl = New PaymentObligationFacade(User).Retrieve(criterias, sortColl)
        ArlPaymentOblParent = New ArrayList

        For Each item As PaymentObligation In ArlPaymentObl
            If (Not IsExist(item, ArlPaymentOblParent)) Then
                ArlPaymentOblParent.Add(item)
            End If
        Next

        _sesshelper.SetSession("ListPaymentOblDetail", ArlPaymentObl)
        _sesshelper.SetSession("ListPaymentOblParent", ArlPaymentOblParent)

        TotalAmmount = 0
        If (ArlPaymentOblParent.Count > 0) Then
            listParrent.DataSource = ArlPaymentOblParent
            listParrent.DataBind()
            If bValidatePriv Then
                btnProcess.Visible = True
            Else
                btnProcess.Visible = bValidatePriv
            End If
        Else
            'MessageBox.Show("Data tidak ditemukan")
            listParrent.DataSource = New ArrayList
            listParrent.DataBind()
            If bValidatePriv Then
                btnProcess.Visible = False
            Else
                btnProcess.Visible = bValidatePriv
            End If
        End If
        lblTotal.Text = TotalAmmount.ToString("#,##0")

        If oLoginUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER And ddlAssignmentType.SelectedIndex > 0 Then
            btnProcess.Enabled = True
        Else
            btnProcess.Enabled = False
        End If

    End Sub

    Private Sub BindDDList()
        Dim _paymentList As New EnumOnlinePayment
        ddlStatus.Items.Add(New ListItem("Pilih Status", "-1"))
        For Each item As OnlinePaymentItem In _paymentList.StatusOnlinePaymentList
            Dim _temp As New ListItem(item.OPCode, item.OPValue)
            ddlStatus.Items.Add(_temp)
        Next
        ddlStatus.SelectedIndex = -1

        '_paymentList = New EnumOnlinePayment
        'ddlProcess.Items.Add(New ListItem("Pilih Status", "-1"))
        'For Each item As OnlinePaymentItem In _paymentList.ActionOnlinePaymentList
        '    Dim _temp As New ListItem(item.OPCode, item.OPValue)
        '    ddlProcess.Items.Add(_temp)
        'Next
        'ddlProcess.SelectedIndex = -1

        _paymentList = New EnumOnlinePayment
        ddlPaymentType.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For Each item As OnlinePaymentItem In _paymentList.PaymentTypeList
            Dim _temp As New ListItem(item.OPCode, item.OPValue)
            ddlPaymentType.Items.Add(_temp)
        Next
        ddlPaymentType.SelectedIndex = -1

        ddlAssignmentType.Items.Clear()
        ddlAssignmentType.DataSource = New PaymentAssignmentTypeFacade(User).RetrieveActiveListDDL()
        ddlAssignmentType.DataTextField = "Description"
        ddlAssignmentType.DataValueField = "ID"
        ddlAssignmentType.DataBind()
        ddlAssignmentType.Items.Insert(0, New ListItem("Silahkan Pilih", -1))

    End Sub

    Private Function IsExist(ByVal objPaymentObl As PaymentObligation, ByVal arl As ArrayList) As Boolean
        Dim bResult As Boolean = False
        For Each item As PaymentObligation In arl
            If item.Dealer.DealerCode.Trim().ToUpper() = objPaymentObl.Dealer.DealerCode.Trim.ToUpper _
                And item.Assignment.Trim.ToUpper = objPaymentObl.Assignment.Trim.ToUpper And item.PaymentAssignmentType.ID = objPaymentObl.PaymentAssignmentType.ID _
                And item.IsTOP = objPaymentObl.IsTOP Then
                bResult = True
                Exit For
            End If
        Next
        Return bResult
    End Function
    Private Function isvalidPage() As Boolean
        If icFromDocDate.Value > icToDocDate.Value Then
            MessageBox.Show("Tanggal Mulai Tidak Boleh melebihi Tanggal Sampai")
            Return False
        End If

        Return True
    End Function
    Private Function GetCriteriasForDetails(ByVal _dealerID As Integer, ByVal _Assignment As String, ByVal _PaymentAssignmentTypeID As Integer, ByVal _SourceDocument As Integer, ByVal _isTOP As Integer) As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PaymentObligation), "Status", MatchType.Exact, CInt(EnumOnlinePayment.StatusOnlinePayment.Baru)))
        criterias.opAnd(New Criteria(GetType(PaymentObligation), "Dealer.ID", MatchType.Exact, _dealerID))
        criterias.opAnd(New Criteria(GetType(PaymentObligation), "Assignment", MatchType.Exact, _Assignment))
        criterias.opAnd(New Criteria(GetType(PaymentObligation), "SourceDocument", MatchType.Exact, _SourceDocument))
        criterias.opAnd(New Criteria(GetType(PaymentObligation), "PaymentAssignmentType.ID", MatchType.Exact, _PaymentAssignmentTypeID))
        criterias.opAnd(New Criteria(GetType(PaymentObligation), "IsTOP", MatchType.Exact, _isTOP))
        'criterias.opAnd(New Criteria(GetType(PaymentObligation), "DocDate", MatchType.GreaterOrEqual, icFromDocDate.Value))
        'criterias.opAnd(New Criteria(GetType(PaymentObligation), "DocDate", MatchType.LesserOrEqual, icToDocDate.Value))
        Return criterias
    End Function

    Private Sub SetSessionCriteria()
        Dim arrLastState As ArrayList = New ArrayList
        arrLastState.Add(txtKodeDealer.Text)
        arrLastState.Add(txtAssignment.Text)
        arrLastState.Add(ddlAssignmentType.SelectedIndex)
        arrLastState.Add(ddlPaymentType.SelectedIndex)
        arrLastState.Add(icFromDocDate.Value)
        arrLastState.Add(icToDocDate.Value)

        _sesshelper.SetSession("PAYMENTOBLIGATIONSESSIONLASTSTATE", arrLastState)
    End Sub
    Private Sub GetSessionCriteria()
        Dim arrLastState As ArrayList = _sesshelper.GetSession("PAYMENTOBLIGATIONSESSIONLASTSTATE")
        If Not arrLastState Is Nothing Then
            txtKodeDealer.Text = arrLastState.Item(0)
            txtAssignment.Text = arrLastState.Item(1)
            ddlAssignmentType.SelectedIndex = arrLastState.Item(2)
            ddlPaymentType.SelectedIndex = arrLastState.Item(3)
            icFromDocDate.Value = arrLastState.Item(4)
            icToDocDate.Value = arrLastState.Item(5)
        End If
    End Sub

#End Region

End Class
