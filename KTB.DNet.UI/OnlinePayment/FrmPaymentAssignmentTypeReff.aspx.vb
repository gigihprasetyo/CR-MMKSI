#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.OnlinePayment
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

#End Region

Public Class FrmPaymentAssignmentTypeReff
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dtgReff As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button

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
    Dim _paymentAssignmentTypeID As Integer
    Private arrReff As ArrayList = New ArrayList
    Private _sesshelper As New SessionHelper

#End Region
#Region "EventHandler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            _paymentAssignmentTypeID = CInt(Request.QueryString("Id"))
            If Not IsPostBack Then
                If _paymentAssignmentTypeID = 0 Then
                    MessageBox.Show("Data Tidak Ada")
                    Return
                Else
                    GetDataReff(_paymentAssignmentTypeID)
                End If
            End If

       
    End Sub

#End Region

#Region "Custom Method"
    Private Sub GetDataReff(ByVal _id As Integer)
        Dim objPaymentAssignmentType As PaymentAssignmentType = New PaymentAssignmentType
        Dim arrResult As New ArrayList

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentAssignmentType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PaymentAssignmentType), "ID", MatchType.Exact, CInt(_id)))

        arrResult = New PaymentAssignmentTypeFacade(User).Retrieve(criterias)
        If arrResult.Count > 0 Then
            _sesshelper.SetSession("sesPaymentAssigntmentType", CType(arrResult(0), PaymentAssignmentType))

            Dim criteriasReff As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentAssignmentTypeReff), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriasReff.opAnd(New Criteria(GetType(PaymentAssignmentTypeReff), "PaymentAssignmentType.ID", MatchType.Exact, CInt(_id)))
            arrResult = New PaymentAssignmentTypeReffFacade(User).Retrieve(criteriasReff)
            If arrResult.Count > 0 Then
                _sesshelper.SetSession("sessPaymentAssignmentTypeReff", arrResult)
                dtgReff.DataSource = arrResult
                dtgReff.ShowFooter = True
            Else
                _sesshelper.SetSession("sessPaymentAssignmentTypeReff", Nothing)
                dtgReff.DataSource = New ArrayList
                dtgReff.ShowFooter = True

            End If
        Else
            MessageBox.Show(SR.DataNotFound("Payment Assignmet Type"))
            _sesshelper.SetSession("sesPaymentAssigntmentType", Nothing)
            dtgReff.DataSource = New ArrayList
            btnSimpan.Enabled = False
            btnBatal.Enabled = False
            dtgReff.ShowFooter = False
        End If
        dtgReff.DataBind()

    End Sub

    Private Sub setPAT(ByVal _id As String)
        Dim objPaymentAssignmentTypeReff As PaymentAssignmentType = New PaymentAssignmentType
        Dim arrSess As New ArrayList

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentAssignmentType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PaymentAssignmentType), "ID", MatchType.Exact, CInt(_id)))

        arrSess = New PaymentAssignmentTypeFacade(User).Retrieve(criterias)
        If arrSess.Count > 0 Then
            _sesshelper.SetSession("sesPaymentAssigntmentType", CType(arrSess(0), PaymentAssignmentType))

        End If
    End Sub

    Private Sub SetItemFooter(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objReff As PaymentAssignmentTypeReff = e.Item.DataItem


        Dim ftxtUsertID As TextBox = CType(e.Item.FindControl("ftxtUsertID"), TextBox)
        Dim flblUserSearch As Label = CType(e.Item.Cells(1).FindControl("flblUserSearch"), Label)
        flblUserSearch.Attributes("onclick") = "ShowPPUserInfo();"
    End Sub

    Private Sub SetItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objReff As PaymentAssignmentTypeReff = e.Item.DataItem

        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        lblNo.Text = (e.Item.ItemIndex + 1 + (dtgReff.CurrentPageIndex * dtgReff.PageSize)).ToString

        Dim lbtnDeleteNew As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
        lbtnDeleteNew.Attributes.Add("OnClick", New CommonFunction().PreventDoubleClickAtGrid(CType(e.Item.FindControl("lbtnDelete"), LinkButton), "Yakin Data ini akan dibatalkan?"))

        Dim lblUserID As Label = CType(e.Item.FindControl("lblUserID"), Label)
        Dim objUserInfo As UserInfo = New UserInfoFacade(User).Retrieve(CInt(lblUserID.Text))
        lblUserID.Text = objUserInfo.Dealer.DealerCode & "-" & objUserInfo.UserName

    End Sub

    Private Sub SetItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objReff As PaymentAssignmentTypeReff = e.Item.DataItem

        Dim lblNo As Label = CType(e.Item.FindControl("elblNo"), Label)
        lblNo.Text = (e.Item.ItemIndex + 1 + (dtgReff.CurrentPageIndex * dtgReff.PageSize)).ToString


        Dim etxtUsertID As TextBox = CType(e.Item.FindControl("etxtUsertID"), TextBox)
        Dim objUserInfo As UserInfo = New UserInfoFacade(User).Retrieve(CInt(etxtUsertID.Text))
        etxtUsertID.Text = objUserInfo.Dealer.DealerCode & "-" & objUserInfo.UserName
        Dim elblUserSearch As Label = CType(e.Item.Cells(1).FindControl("elblUserSearch"), Label)
        elblUserSearch.Attributes("onclick") = "ShowPPUserInfo();"
    End Sub

    Private Function isValidID(ByVal _id As String, ByRef objUser As UserInfo) As Boolean
        Try
            Dim arrUserInfo As ArrayList = New ArrayList
            Dim strUserDealer() As String = _id.Split("-")
            Dim objUserInfo As UserInfo = New UserInfoFacade(User).RetrievebyUserNameAndDealerCode(strUserDealer(1), strUserDealer(0))

            'Dim nresult As Integer = 0
            'nresult = New KTB.DNet.BusinessFacade.UserManagement.UserInfoFacade(User).Retrieve(_id)

            Dim criteriaValid As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriaValid.opAnd(New Criteria(GetType(UserInfo), "Email", MatchType.No, String.Empty))
            criteriaValid.opAnd(New Criteria(GetType(UserInfo), "ID", MatchType.Exact, CInt(objUserInfo.ID)))
            criteriaValid.opAnd(New Criteria(GetType(UserInfo), "UserStatus", MatchType.Exact, CByte(EnumUserStatus.UserStatus.Aktif)))
            criteriaValid.opAnd(New Criteria(GetType(UserInfo), "Dealer.Title", MatchType.Exact, CByte(EnumDealerTittle.DealerTittle.KTB)))

            arrUserInfo = New KTB.DNet.BusinessFacade.UserManagement.UserInfoFacade(User).Retrieve(criteriaValid)
            If arrUserInfo.Count > 0 Then
                objUser = CType(arrUserInfo(0), UserInfo)
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function isExist(ByVal _id As String, ByVal arlItem As ArrayList, ByVal nIndeks As Integer) As Boolean
        Try
            Dim i As Integer
            Dim bResult As Boolean = False
            For i = 0 To arlItem.Count - 1
                If CType(arlItem(i), PaymentAssignmentTypeReff).UserInfo.ID.ToString.Trim.ToUpper() = _id.Trim().ToUpper() AndAlso nIndeks <> i Then
                    bResult = True
                    Exit For
                End If
            Next
            Return bResult
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function IsExist(ByVal _id As String, ByVal arlItem As ArrayList) As Boolean
        Dim bResult As Boolean = False
        For Each item As PaymentAssignmentTypeReff In arlItem
            If item.UserInfo.ID.ToString.Trim().ToUpper() = _id.Trim().ToUpper() Then
                bResult = True
                Exit For
            End If
        Next
        Return bResult
    End Function


    Private Sub bindGrid()
        arrReff = CType(_sesshelper.GetSession("sessPaymentAssignmentTypeReff"), ArrayList)
        dtgReff.DataSource = arrReff
        dtgReff.DataBind()
    End Sub

    Private Sub RenderItem(ByVal objUserInfo As UserInfo, _
   ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim flblUserName As Label = CType(e.Item.FindControl("flblUserName"), Label)
        Dim flblEmail As Label = CType(e.Item.FindControl("flblEmail"), Label)
        flblUserName.Text = objUserInfo.UserName
        flblEmail.Text = objUserInfo.Email
    End Sub

    Private Sub setSessionReff(ByVal _user As String)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentAssignmentTypeReff), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PaymentAssignmentTypeReff), "UserInfo.ID", MatchType.Exact, CInt(_user)))

        Dim arrResult As ArrayList = New ArrayList
        arrResult = New PaymentAssignmentTypeReffFacade(User).Retrieve(criterias)
        If arrResult.Count > 0 Then
            _sesshelper.SetSession("sessPaymentAssignmentTypeReffItem", CType(arrResult(0), PaymentAssignmentTypeReff))
        Else
            _sesshelper.SetSession("sessPaymentAssignmentTypeReffItem", Nothing)
        End If

    End Sub

    Private Sub MergeItem(ByVal nID As Integer)
        Dim objPAT As PaymentAssignmentType = New PaymentAssignmentTypeFacade(User).Retrieve(nID)
        arrReff = CType(Session("sessPaymentAssignmentTypeReff"), ArrayList)
        For Each objItemOrig As PaymentAssignmentTypeReff In objPAT.PaymentAssignmentTypeReffs
            objItemOrig.RowStatus = DBRowStatus.Deleted
        Next

        Dim found As Boolean
        For Each objItem As PaymentAssignmentTypeReff In arrReff
            found = False
            For Each objItemOrig As PaymentAssignmentTypeReff In objPAT.PaymentAssignmentTypeReffs
                If objItem.UserInfo.ID.ToString.Trim().ToUpper() = objItemOrig.UserInfo.ID.ToString.Trim().ToUpper() Then
                    objItemOrig.RowStatus = DBRowStatus.Active
                    found = True
                    Exit For
                End If
                found = False
            Next
            If Not found Then
                objPAT.PaymentAssignmentTypeReffs.Add(objItem)
            End If
        Next
        _sesshelper.SetSession("sessPaymentAssignmentTypeReff", objPAT.PaymentAssignmentTypeReffs)
    End Sub

#End Region

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        If dtgReff.EditItemIndex <> -1 Then
            dtgReff.EditItemIndex = -1
            dtgReff.ShowFooter = True
        Else
            _sesshelper.SetSession("sessPaymentAssignmentTypeReff", Nothing)
            arrReff = New ArrayList
        End If
        GetDataReff(_paymentAssignmentTypeID)
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("../OnlinePayment/FrmpaymentAssignmentType.aspx", True)
    End Sub

    Private Sub dtgReff_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgReff.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            SetItem(e)
        End If
        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            SetItemEdit(e)
        End If
        If e.Item.ItemType = ListItemType.Footer Then
            SetItemFooter(e)
        End If
    End Sub

    Private Sub dtgReff_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgReff.ItemCommand
        If IsNothing(_sesshelper.GetSession("sessPaymentAssignmentTypeReff")) Then
            arrReff = New ArrayList
        Else
            arrReff = CType(_sesshelper.GetSession("sessPaymentAssignmentTypeReff"), ArrayList)
        End If
        Select Case e.CommandName
            Case "add"
                Try
                    Dim objUser As UserInfo = New UserInfo

                    Dim txtUserID As TextBox = CType(e.Item.FindControl("ftxtUsertID"), TextBox)
                    If txtUserID.Text <> String.Empty Then
                        If isValidID(txtUserID.Text.Trim, objUser) Then
                            If isExist(txtUserID.Text.Trim, arrReff) Then
                                MessageBox.Show(SR.DataIsExist("User"))
                                Return
                            End If
                            RenderItem(objUser, e)
                            Dim objNewReff As PaymentAssignmentTypeReff = New PaymentAssignmentTypeReff
                            objNewReff.UserInfo = objUser
                            setPAT(_paymentAssignmentTypeID)
                            objNewReff.PaymentAssignmentType = CType(_sesshelper.GetSession("sesPaymentAssigntmentType"), PaymentAssignmentType)
                            arrReff.Add(objNewReff)


                            'If CType(ViewState("vsAccess"), String) = "edit" Then

                            'End If

                            '_sesshelper.SetSession("sessPaymentAssignmentTypeReff", arrReff)
                            Page.RegisterStartupScript("test", "<script language=JavaScript> focusSave(); </script>")

                        Else
                            MessageBox.Show("User Tidak Valid")
                            Return
                        End If
                    Else
                        MessageBox.Show("Data User Harus Diisi")
                        Return
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try


            Case "edit"
                Try
                    'If _sesshelper.GetSession("sessPaymentAssignmentTypeReffItem") <> Nothing Then
                    '    MessageBox.Show("Masih Ada Data Yang Masih Diedit")
                    '    Return
                    'End If

                    'Dim lblUser As Label = CType(e.Item.FindControl("lblUserID"), Label)
                    'setSessionReff(lblUser.Text.Trim)
                    dtgReff.ShowFooter = False
                    btnSimpan.Enabled = False
                    dtgReff.EditItemIndex = e.Item.ItemIndex
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try


            Case "delete"
                Try
                    arrReff = CType(_sesshelper.GetSession("sessPaymentAssignmentTypeReff"), ArrayList)
                    Dim objItem As PaymentAssignmentTypeReff = arrReff(e.Item.ItemIndex)
                    If objItem.ID > 0 Then
                        'objItem.RowStatus = CType(DBRowStatus.Deleted, Short)
                        Dim result As Integer = New PaymentAssignmentTypeReffFacade(User).DeleteFromDB(objItem)
                        If result = -1 Then
                            MessageBox.Show(SR.DeleteFail)
                            Exit Sub
                        End If
                    End If
                    arrReff.RemoveAt(e.Item.ItemIndex)
                    _sesshelper.SetSession("sessPaymentAssignmentTypeReff", arrReff)
                    bindGrid()
                Catch ex As Exception
                End Try

            Case "save"
                Try
                    Dim txtUserID As TextBox = CType(e.Item.FindControl("etxtUsertID"), TextBox)
                    Dim objUser As UserInfo = New UserInfo

                    If txtUserID.Text <> String.Empty Then
                        If isValidID(txtUserID.Text.Trim, objUser) Then
                            If isExist(txtUserID.Text.Trim, arrReff, e.Item.ItemIndex) Then
                                MessageBox.Show("Data User Sudah Ada")
                                Return
                            Else
                                Dim objReff As PaymentAssignmentTypeReff = New PaymentAssignmentTypeReff
                                objReff = CType(arrReff(e.Item.ItemIndex), PaymentAssignmentTypeReff)
                                objReff.UserInfo = objUser
                                dtgReff.EditItemIndex = -1
                                dtgReff.ShowFooter = True
                                btnSimpan.Enabled = True
                            End If
                        Else
                            MessageBox.Show("Data User Tidak Valid")
                            Return
                        End If
                    Else
                        MessageBox.Show("Data User Harus Diisi")
                        Return
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try


            Case "cancel"
                dtgReff.EditItemIndex = -1
                dtgReff.ShowFooter = True
                btnSimpan.Enabled = True

        End Select
        _sesshelper.SetSession("sessPaymentAssignmentTypeReff", arrReff)
        bindGrid()
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If CType(Session("sessPaymentAssignmentTypeReff"), ArrayList).Count > 0 Then
            Try
                Dim nResult As Integer
                nResult = New PaymentAssignmentTypeReffFacade(User).ReffTransaction(CType(Session("sessPaymentAssignmentTypeReff"), ArrayList))
                If nResult > 0 Then
                    MessageBox.Show(SR.SaveSuccess)
                Else
                    MessageBox.Show(SR.SaveFail)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Else
            MessageBox.Show(SR.GridIsEmpty("Payment Assignment Type Reff"))
        End If
    End Sub
End Class
