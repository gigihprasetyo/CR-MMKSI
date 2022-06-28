#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.OnlinePayment
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
#End Region

Public Class FrmRegisteredDoc
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgObligationPayment As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents icFromRegDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icToRegDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dlPaymentRegDoc As System.Web.UI.WebControls.DataList
    Protected WithEvents txtRegNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Varible"
    Private oLoginUserInfo As UserInfo
    Private _sesshelper As New SessionHelper

    Private bEditPriv As Boolean
#End Region

#Region "Custom Method"
#Region "Check Privilage"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.Pembayaran_daftar_register_doc_lihat_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=INFORMASI PEMBAYARAN - Daftar Register Doc")
        End If

        bEditPriv = SecurityProvider.Authorize(context.User, SR.Pembayaran_daftar_register_doc_edit_privilege)


    End Sub

#End Region

    'Private Sub BindDDL()
    '    Dim _paymentList As New EnumOnlinePayment
    '    ddlType.Items.Add(New ListItem("Pilih Status", "-1"))
    '    For Each item As OnlinePaymentItem In _paymentList.StatusOnlinePaymentList
    '        Dim _temp As New ListItem(item.OPCode, item.OPValue)
    '        ddlType.Items.Add(_temp)
    '    Next
    '    ddlType.SelectedIndex = -1
    'End Sub

    Private Function isExist(ByVal objPO As PaymentObligation, ByVal arlmaster As ArrayList) As Boolean
        Dim bResult As Boolean = False
        For Each item As PaymentRegDoc In arlmaster
            If objPO.PaymentRegDoc.ID = item.ID Then
                bResult = True
                Exit For
            End If
        Next

        Return bResult
    End Function

    Sub BindToGrid()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(PaymentRegDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If oLoginUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(PaymentRegDoc), "Dealer.DealerCode", MatchType.Exact, lblKodeDealer.Text))
        ElseIf oLoginUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If txtKodeDealer.Text <> "" Then
                criterias.opAnd(New Criteria(GetType(PaymentRegDoc), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text))
            End If
        End If


        If txtRegNo.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(PaymentRegDoc), "ID", MatchType.Exact, CInt(txtRegNo.Text)))
        End If

        criterias.opAnd(New Criteria(GetType(PaymentRegDoc), "CreatedTime", MatchType.GreaterOrEqual, New Date(icFromRegDate.Value.Year, icFromRegDate.Value.Month, icFromRegDate.Value.Day, 0, 0, 0)))
        criterias.opAnd(New Criteria(GetType(PaymentRegDoc), "CreatedTime", MatchType.LesserOrEqual, New Date(icToRegDate.Value.Year, icToRegDate.Value.Month, icToRegDate.Value.Day, 23, 59, 59)))

        Dim arlData As ArrayList = New PaymentRegDocFacade(User).Retrieve(criterias)

        If arlData.Count > 0 Then
            dlPaymentRegDoc.DataSource = arlData
            dlPaymentRegDoc.DataBind()
        Else
            dlPaymentRegDoc.DataSource = New ArrayList
            dlPaymentRegDoc.DataBind()
            MessageBox.Show("Data tidak ditemukan")
        End If


    End Sub

    Private Function GetCriteriasForDetails(ByVal id As Integer) As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PaymentObligation), "PaymentRegDoc.ID", MatchType.Exact, id))
        Return criterias
    End Function
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'BindDDL()

        oLoginUserInfo = CType(_sesshelper.GetSession("LOGINUSERINFO"), UserInfo)
        InitiateAuthorization()

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
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        BindToGrid()
    End Sub

    Private Sub dlPaymentRegDoc_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlPaymentRegDoc.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim objPaymentRegDoc As PaymentRegDoc = e.Item.DataItem
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lbtnEdit.Visible = bEditPriv

            Dim lblProcessBy As Label = CType(e.Item.FindControl("lblProcessBy"), Label)
            lblProcessBy.Text = CommonFunction.FormatSavedUser(objPaymentRegDoc.LastUpdateBy, User)

            Dim lblRegDate As Label = CType(e.Item.FindControl("lblRegDate"), Label)
            If objPaymentRegDoc.CreateTime < New Date(1900, 1, 1) Then
                lblRegDate.Text = ""
            Else
                lblRegDate.Text = objPaymentRegDoc.CreateTime.ToString("dd/MM/yyyy")
            End If


            Dim total As Decimal = 0.0
            Dim dtgDetail As DataGrid = CType(e.Item.FindControl("dtgPaymentObligation"), DataGrid)
            Dim arlDetails As New ArrayList
            Dim criteriaDetails As CriteriaComposite = GetCriteriasForDetails(objPaymentRegDoc.ID)
            arlDetails = New PaymentObligationFacade(User).Retrieve(criteriaDetails)

            'count for total
            For Each item As PaymentObligation In arlDetails
                total = total + item.Amount
            Next
            Dim lblJumlah As Label = e.Item.FindControl("lblJumlah")
            lblJumlah.Text = total.ToString("#,##0")

            dtgDetail.DataSource = arlDetails
            dtgDetail.DataBind()
        End If
        If e.Item.ItemType = ListItemType.EditItem Then
            Dim objEPaymentRegDoc As PaymentRegDoc = e.Item.DataItem
            Dim arlEDetails As New ArrayList
            Dim criteriaDetails As CriteriaComposite = GetCriteriasForDetails(objEPaymentRegDoc.ID)
            arlEDetails = New PaymentObligationFacade(User).Retrieve(criteriaDetails)

            'count for total
            Dim totalE As Decimal = 0.0
            For Each item As PaymentObligation In arlEDetails
                totalE = totalE + item.Amount
            Next
            Dim lblEJumlah As Label = e.Item.FindControl("lblEJumlah")
            lblEJumlah.Text = totalE.ToString("#,##0")

            Dim lblELastUpdate As Label = CType(e.Item.FindControl("lblELastUpdate"), Label)
            lblELastUpdate.Text = CommonFunction.FormatSavedUser(objEPaymentRegDoc.LastUpdateBy, User)

            Dim lblECreateTime As Label = CType(e.Item.FindControl("lblECreateTime"), Label)
            If objEPaymentRegDoc.CreateTime < New Date(1900, 1, 1) Then
                lblECreateTime.Text = ""
            Else
                lblECreateTime.Text = objEPaymentRegDoc.CreateTime.ToString("dd/MM/yyyy")
            End If
        End If
    End Sub

    Private Sub dlPaymentRegDoc_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlPaymentRegDoc.ItemCommand
        If e.CommandName = "Edit" Then
            dlPaymentRegDoc.EditItemIndex = e.Item.ItemIndex
            BindToGrid()
        End If

        If e.CommandName = "Batal" Then
            dlPaymentRegDoc.EditItemIndex = -1
            BindToGrid()
        End If

        If e.CommandName = "Simpan" Then
            Dim lblEID As Label = CType(e.Item.FindControl("lblEID"), Label)
            Dim objPaymentRegDoc As PaymentRegDoc = New PaymentRegDocFacade(User).Retrieve(CInt(lblEID.Text))
            Dim txtNoBOR As TextBox = CType(e.Item.FindControl("txtNoBOR"), TextBox)
            If Not objPaymentRegDoc Is Nothing Then
                objPaymentRegDoc.BORNumber = txtNoBOR.Text
                If (New PaymentRegDocFacade(User).UpdateOP(objPaymentRegDoc) <> -1) Then
                    dlPaymentRegDoc.EditItemIndex = -1
                    MessageBox.Show("Data berhasil diupdate")
                    BindToGrid()
                Else
                    MessageBox.Show("Data gagal diupdate")
                End If
            Else
                MessageBox.Show("Data tidak ditemukan")
            End If
        End If
    End Sub
End Class
