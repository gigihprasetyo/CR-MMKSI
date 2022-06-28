#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.OnlinePayment
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
#End Region

#Region ".NET Base Class Namespace Imports"

#End Region


Public Class FrmPaymentObligationManual
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
    Protected WithEvents icOrderDate As Intimedia.WebCC.IntiCalendar
    Protected WithEvents listParrent As System.Web.UI.WebControls.DataList
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtAssignment As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlPaymentType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents icFromDocDate As Intimedia.WebCC.IntiCalendar
    Protected WithEvents icToDocDate As Intimedia.WebCC.IntiCalendar

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
    Private _sesshelper As New SessionHelper
    Private ArlPaymentObl As ArrayList = New ArrayList
    Private ArlPaymentOblParent As ArrayList = New ArrayList

#End Region


#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            lblSearchDealer.Attributes.Add("onclick", "ShowPPDealerSelection();return false;")
            BindDDL()

        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        If isvalidPage() Then
            BindToGrid(0)
        End If

    End Sub

    Private Sub listParrent_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles listParrent.ItemDataBound
        If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
            Dim oPaymentObl As PaymentObligation = e.Item.DataItem
            Dim dtgDetail As DataGrid = CType(e.Item.FindControl("dtgListPaymentObligation"), DataGrid)
            Dim arlDetails As New ArrayList
            Dim criteriaDetails As CriteriaComposite = GetCriteriasForDetails(oPaymentObl.Dealer.ID, oPaymentObl.Assignment, oPaymentObl.PaymentObligationType.ID.ToString)
            arlDetails = New PaymentObligationFacade(User).Retrieve(criteriaDetails)
            dtgDetail.DataSource = arlDetails
            dtgDetail.DataBind()

        End If
    End Sub

    Private Sub Process()
        'Dim isChecked As Boolean = False
        'Dim i As Integer = 0
        'Dim arlToUpdate As ArrayList = New ArrayList
        'Dim arlHistory As ArrayList = New ArrayList
        'If Not ddlProcess.SelectedIndex > 0 Then
        '    MessageBox.Show("Pilih Status untuk Proses ")
        '    Return
        'End If
        'For Each item As DataListItem In listParrent.Items
        '    isChecked = True
        '    Dim chkItemChecked As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
        '    If (chkItemChecked.Checked) Then
        '        Dim lblAsgn As Label = CType(item.FindControl("lblAssignmentP"), Label)
        '        Dim lblDealer As Label = CType(item.FindControl("lblDealerP"), Label)
        '        Dim lblpaymentType As Label = CType(item.FindControl("lblDescriptionP"), Label)
        '        Dim arlDetail As ArrayList = New ArrayList

        '        Dim criteriatoUpdate As CriteriaComposite = GetCriteriasForDetails(lblDealer.Text, lblAsgn.Text)
        '        arlDetail = New PaymentObligationFacade(User).Retrieve(criteriatoUpdate)
        '        If arlDetail.Count > 0 Then
        '            For Each oPaymentObligation As PaymentObligation In arlDetail
        '                Select Case ddlProcess.SelectedValue
        '                    Case 0
        '                        If oPaymentObligation.Status = EnumOnlinePayment.StatusOnlinePayment.Baru Then
        '                            oPaymentObligation.Status = EnumOnlinePayment.StatusOnlinePayment.Validasi
        '                        Else
        '                            MessageBox.Show("Gagal Proses Ubah Status")
        '                            Return
        '                        End If

        '                    Case 1
        '                        If oPaymentObligation.Status = EnumOnlinePayment.StatusOnlinePayment.Validasi Then
        '                            oPaymentObligation.Status = EnumOnlinePayment.StatusOnlinePayment.Proses
        '                        Else
        '                            MessageBox.Show("Gagal Proses Ubah Status")
        '                            Return
        '                        End If

        '                    Case 2
        '                        If oPaymentObligation.Status = EnumOnlinePayment.StatusOnlinePayment.Proses Then
        '                            oPaymentObligation.Status = EnumOnlinePayment.StatusOnlinePayment.Selesai
        '                        Else
        '                            MessageBox.Show("Gagal Proses Ubah Status")
        '                            Return
        '                        End If
        '                End Select
        '                arlToUpdate.Add(oPaymentObligation)
        '            Next
        '        End If
        '    End If
        'Next
        'If arlToUpdate.Count > 0 Then
        '    Try
        '        Dim nresult As Integer = 0
        '        Dim _userData As String = String.Empty
        '        _userData = CType(_sesshelper.GetSession("LOGINUSERINFO"), UserInfo).Dealer.DealerCode & CType(_sesshelper.GetSession("LOGINUSERINFO"), UserInfo).UserName
        '        nresult = New PaymentObligationFacade(User).Update(arlToUpdate)
        '        If nresult > 0 Then
        '            For Each objPayment As PaymentObligation In arlToUpdate
        '                Dim oHistory As PaymentObligationHistory = New PaymentObligationHistory
        '                oHistory.PaymentObligation = objPayment
        '                oHistory.ProcessDate = Now
        '                oHistory.ProcessBy = _userData
        '                oHistory.Status = objPayment.Status
        '                arlHistory.Add(oHistory)
        '            Next
        '            nresult = New PaymentObligationHistoryFacade(User).Insert(arlHistory)
        '            If nresult > 0 Then
        '                MessageBox.Show(SR.UpdateSucces)
        '            Else
        '                MessageBox.Show(SR.UpdateFail)
        '            End If

        '        Else
        '            MessageBox.Show(SR.UpdateFail)
        '        End If
        '    Catch ex As Exception
        '        MessageBox.Show(SR.UpdateFail)
        '    End Try
        'Else
        '    MessageBox.Show("Tidak Ada Data Yang Bisa Diproses")
        '    Return
        'End If
    End Sub

    Private Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Process()
    End Sub

#End Region

#Region "Custhom Method"

    Private Sub BindToGrid(ByVal idx As Integer)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtKodeDealer.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(PaymentObligation), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        End If

        If ddlStatus.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(PaymentObligation), "Status", MatchType.Exact, CInt(ddlStatus.SelectedValue)))
        End If

        If ddlPaymentType.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(PaymentObligation), "IsTOP", MatchType.Exact, CInt(ddlPaymentType.SelectedValue)))
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

        If (ArlPaymentOblParent.Count > 0) Then
            listParrent.Visible = True
            listParrent.DataSource = ArlPaymentOblParent
            listParrent.DataBind()
            btnProcess.Enabled = True
            'ddlProcess.Enabled = True
        Else
            listParrent.Visible = False
            MessageBox.Show("Data tidak ditemukan")
            btnProcess.Enabled = False
            ' ddlProcess.Enabled = False
        End If

    End Sub

    Private Sub BindDDL()
        'Dim _paymentList As New EnumOnlinePayment
        'ddlStatus.Items.Add(New ListItem("Pilih Status", "-1"))
        'For Each item As OnlinePaymentItem In _paymentList.StatusOnlinePaymentList
        '    Dim _temp As New ListItem(item.OPCode, item.OPValue)
        '    ddlStatus.Items.Add(_temp)
        'Next
        'ddlStatus.SelectedIndex = -1
        '_paymentList = New EnumOnlinePayment
        'ddlProcess.Items.Add(New ListItem("Pilih Status", "-1"))
        'For Each item As OnlinePaymentItem In _paymentList.ActionOnlinePaymentList
        '    Dim _temp As New ListItem(item.OPCode, item.OPValue)
        '    ddlProcess.Items.Add(_temp)
        'Next
        'ddlProcess.SelectedIndex = -1
        '_paymentList = New EnumOnlinePayment
        'ddlPaymentType.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        'For Each item As OnlinePaymentItem In _paymentList.PaymentTypeList
        '    Dim _temp As New ListItem(item.OPCode, item.OPValue)
        '    ddlPaymentType.Items.Add(_temp)
        'Next
        'ddlPaymentType.SelectedIndex = -1
    End Sub

    Private Function IsExist(ByVal objPaymentObl As PaymentObligation, ByVal arl As ArrayList) As Boolean
        Dim bResult As Boolean = False
        For Each item As PaymentObligation In arl
            If item.Dealer.DealerCode.Trim().ToUpper() = objPaymentObl.Dealer.DealerCode.Trim.Trim _
                And item.Assignment.Trim.ToUpper = objPaymentObl.Assignment.Trim.ToUpper Then
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

    Private Function GetCriteriasForDetails(ByVal _dealerID As String, ByVal _Assignment As String, ByVal _PaymentType As String) As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PaymentObligation), "Dealer.ID", MatchType.Exact, CInt(_dealerID)))
        criterias.opAnd(New Criteria(GetType(PaymentObligation), "Assignment", MatchType.Exact, _Assignment))
        Return criterias
    End Function

    Private Function GetCriteriasForDetails(ByVal _dealerCode As String, ByVal _Assignment As String) As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PaymentObligation), "Dealer.DealerCode", MatchType.Exact, _dealerCode))
        criterias.opAnd(New Criteria(GetType(PaymentObligation), "Assignment", MatchType.Exact, _Assignment))
        Return criterias
    End Function


#End Region


   
End Class
