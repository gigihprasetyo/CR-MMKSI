Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement

Public Class PopUpMSPExtendedRegistrationSelection
    Inherits System.Web.UI.Page

    Dim crt As CriteriaComposite
    Private _sessHelper As SessionHelper = New SessionHelper
    Private _strSessChassisNumberList As String = "ChassisNumberList"
    Private _strSessDealerID As String = "DealerID"
    Dim ChassisNumberList As String = String.Empty


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            Dim indexPage As Integer = 0
            Dim dealerID As String = Request.QueryString("Tyjiuy678")
            ChassisNumberList = Request.QueryString("ChassisNumberList")
            _sessHelper.SetSession(_strSessDealerID, dealerID)
            BindDropDown()
            If Not String.IsNullOrEmpty(dealerID) Then
                Dim objDealer As Dealer = New DealerFacade(User).Retrieve(CInt(dealerID))

                Dim strSql1 As String = "SELECT MSPExRegistration.ID " &
                                        "FROM MSPExRegistration with (nolock) " &
                                        "INNER JOIN Dealer ON MSPExRegistration.DealerID = Dealer.ID " &
                                        "INNER JOIN ChassisMaster ON ChassisMaster.ID = MSPExRegistration.ChassisMasterID " &
                                        "INNER JOIN MSPExMaster ON MSPExMaster.ID = MSPExRegistration.MSPExMasterID " &
                                        "INNER JOIN MSPExType ON MSPExType.ID = MSPExMaster.MSPExTypeID " &
                                        "INNER JOIN MSPExDebitCharge ON MSPExDebitCharge.MSPExRegistrationID = MSPExRegistration.ID " &
                                        "INNER JOIN MSPExDebitMemo ON MSPExDebitMemo.MSPExRegistrationID = MSPExRegistration.ID " &
                                        "WHERE MSPExRegistration.RowStatus = 0 " &
                                        "AND MSPExDebitCharge.Amount is not NULL " &
                                        "AND MSPExDebitCharge.Amount != 0 " &
                                        "AND MSPExDebitCharge.DebitChargeNo is not null " &
                                        "AND MSPExDebitCharge.DebitChargeNo != '' " &
                                        "AND MSPExRegistration.Status = 1 " &
                                        "AND MSPExDebitMemo.DebitMemoNo is not null " &
                                        "AND MSPExDebitMemo.DebitMemoNo != '' " &
                                        "AND MSPExRegistration.Status = 1 " &
                                        "AND Dealer.ID in (" &
                                        "Select ID " &
                                        "FROM Dealer " &
                                        "WHERE CreditAccount ='" & objDealer.CreditAccount & "') " &
                                        "AND MSPExRegistration.ID not in (" &
                                        "SELECT MSPExPaymentDetail.MSPExRegistrationID " &
                                        "FROM MSPExPaymentDetail " &
                                        "WHERE RowStatus = 0" &
                                        ")"


                ' exclude selected item
                If Not String.IsNullorEmpty(ChassisNumberList) Then
                    _sessHelper.SetSession(_strSessChassisNumberList, ChassisNumberList)

                    Dim newStr() As String = ChassisNumberList.Split(";")
                    Dim str As String = String.Empty
                    For i As Integer = 0 To newStr.Length - 1
                        str += "," & "'" & newStr(i) & "'"
                    Next
                    ChassisNumberList = str.Substring(1, str.Length - 1)
                    strSql1 += "AND ChassisMaster.ChassisNumber not in(" & ChassisNumberList & ")"
                End If

                crt = New CriteriaComposite(New Criteria(GetType(MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crt.opAnd(New Criteria(GetType(MSPExRegistration), "ID", MatchType.InSet, ("(" & strSql1 & ")")))
                Dim arr As ArrayList = New MSPExRegistrationFacade(User).Retrieve(crt)

                'If arr.Count > 0 Then
                '    BindDataGrid(arr)
                'End If

                If dtgMSPRegistration.Items.Count > 0 Then
                    btnChoose.Disabled = False
                Else
                    btnChoose.Disabled = True
                End If

            End If
        End If
    End Sub

    Private Sub BindDropDown()
        ddlTipeProgram.Items.Clear()
        Dim sMSPProgram As ArrayList = New StandardCodeFacade(User).RetrieveByCategory("EnumMSPProgram")
        ddlTipeProgram.DataSource = sMSPProgram
        ddlTipeProgram.DataTextField = "ValueDesc"
        ddlTipeProgram.DataValueField = "ValueCode"
        ddlTipeProgram.DataBind()
        ddlTipeProgram.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
    End Sub

    Private Function BindDataGrid(ByVal arr As ArrayList)
        dtgMSPRegistration.DataSource = arr
        dtgMSPRegistration.DataBind()
    End Function

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindSearch(0)
        If dtgMSPRegistration.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Public Sub BindSearch(ByVal indexPage As Integer)
        dtgMSPRegistration.CurrentPageIndex = indexPage
        Dim totalRow As Integer = 0
        Dim _arr As New ArrayList
        ChassisNumberList = _sessHelper.GetSession(_strSessChassisNumberList)
        Dim dealerID As String = Request.QueryString("Tyjiuy678")
        Dim objDealer As Dealer = New DealerFacade(User).Retrieve(CInt(dealerID))

        crt = New CriteriaComposite(New Criteria(GetType(MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim strSql1 As String = "SELECT MSPExRegistration.ID " &
                                        "FROM MSPExRegistration with (nolock) " &
                                        "INNER JOIN Dealer ON MSPExRegistration.DealerID = Dealer.ID " &
                                        "INNER JOIN ChassisMaster ON ChassisMaster.ID = MSPExRegistration.ChassisMasterID " &
                                        "INNER JOIN MSPExMaster ON MSPExMaster.ID = MSPExRegistration.MSPExMasterID " &
                                        "INNER JOIN MSPExType ON MSPExType.ID = MSPExMaster.MSPExTypeID " &
                                        "INNER JOIN MSPExDebitCharge ON MSPExDebitCharge.MSPExRegistrationID = MSPExRegistration.ID " &
                                        "INNER JOIN MSPExDebitMemo ON MSPExDebitMemo.MSPExRegistrationID = MSPExRegistration.ID " &
                                        "INNER JOIN PrefixMSPRegistration ON PrefixMSPRegistration.MSPExTypeID = MSPExType.ID " &
                                        "WHERE MSPExRegistration.RowStatus = 0 " &
                                        "AND MSPExDebitCharge.Amount is not NULL " &
                                        "AND MSPExDebitCharge.Amount != 0 " &
                                        "AND MSPExDebitCharge.DebitChargeNo is not null " &
                                        "AND MSPExDebitCharge.DebitChargeNo != '' " &
                                        "AND MSPExRegistration.Status = 1 " &
                                        "AND MSPExDebitMemo.DebitMemoNo is not null " &
                                        "AND MSPExDebitMemo.DebitMemoNo != '' " &
                                        "AND MSPExRegistration.Status = 1 " &
                                        "AND Dealer.ID in (" &
                                        "Select ID " &
                                        "FROM Dealer " &
                                        "WHERE CreditAccount ='" & objDealer.CreditAccount & "') " &
                                        "AND MSPExRegistration.ID not in (" &
                                        "SELECT MSPExPaymentDetail.MSPExRegistrationID " &
                                        "FROM MSPExPaymentDetail " &
                                        "WHERE RowStatus = 0" &
                                        ") "


        ' exclude selected item
        If Not String.IsNullorEmpty(ChassisNumberList) Then
            _sessHelper.SetSession(_strSessChassisNumberList, ChassisNumberList)

            Dim newStr() As String = ChassisNumberList.Split(";")
            Dim str As String = String.Empty
            For i As Integer = 0 To newStr.Length - 1
                str += "," & "'" & newStr(i) & "'"
            Next
            ChassisNumberList = str.Substring(1, str.Length - 1)
            strSql1 += "AND ChassisMaster.ChassisNumber not in(" & ChassisNumberList & ") "
        End If

        If Not String.IsNullorEmpty(txtDealerCode.Text) Then
            strSql1 += "AND Dealer.DealerCode = '" & txtDealerCode.Text & "' "
        End If

        If Not String.IsNullorEmpty(txtChassisNumber.Text) Then
            strSql1 += "AND ChassisMaster.ChassisNumber like '%" & txtChassisNumber.Text & "%' "
        End If

        If Not String.IsNullorEmpty(txtDealerReg.Text) Then
            strSql1 += "AND MSPExRegistration.DealerID = (SELECT ID FROM Dealer WHERE DealerCode = '" & txtDealerReg.Text & "') "
        End If

        If ddlTipeProgram.SelectedIndex > 0 Then
            strSql1 += "AND PrefixMSPRegistration.ProgramName = '" & ddlTipeProgram.SelectedValue & "' "
        End If

        strSql1 += "AND MSPExDebitMemo.DocumentDate = '" & Format(DateFrom.Value, "yyyy/MM/dd") & "' "

        crt.opAnd(New Criteria(GetType(MSPExRegistration), "ID", MatchType.InSet, ("(" & strSql1 & ")")))

        _arr = New MSPExRegistrationFacade(User).Retrieve(crt)

        If _arr.Count > 0 Then
            If indexPage >= 0 Then
                BindDataGrid(_arr)
            End If
        Else
            dtgMSPRegistration.DataSource = New ArrayList
            dtgMSPRegistration.DataBind()
            MessageBox.Show("Data tidak ditemukan")
        End If


        If dtgMSPRegistration.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub dtgMSPRegistration_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMSPRegistration.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim term As String = Request.QueryString("Tyjiuy678")
        If Not e.Item.DataItem Is Nothing Then
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim RowValue As MSPExRegistration = CType(e.Item.DataItem, MSPExRegistration)
                'Dim objMSPRegHistory As MSPRegistrationHistory = RowValue.MSPRegistrationHistorys(RowValue.MSPRegistrationHistorys.Count - 1)

                ' set MSPRegID
                Dim lblMSPRegID As Label = CType(e.Item.FindControl("lblMSPRegID"), Label)
                If Not IsNothing(lblMSPRegID) Then
                    lblMSPRegID.Text = RowValue.ID
                End If

                ' set lblChassisNumber 
                Dim lblChassisNumber As Label = CType(e.Item.FindControl("lblChassisNumber"), Label)
                If Not IsNothing(lblChassisNumber) Then
                    lblChassisNumber.Text = RowValue.ChassisMaster.ChassisNumber
                End If

                ' set lblDealerCode 
                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                If Not IsNothing(lblDealerCode) Then
                    lblDealerCode.Text = RowValue.Dealer.CreditAccount
                End If

                'set lblDealerReg
                Dim lblDealerReg As Label = CType(e.Item.FindControl("lblDealerReg"), Label)
                If Not IsNothing(lblDealerReg) Then
                    lblDealerReg.Text = RowValue.Dealer.DealerCode
                End If

                Dim lblRegistrationNo As Label = CType(e.Item.FindControl("lblRegistrationNo"), Label)
                If Not IsNothing(lblRegistrationNo) Then
                    lblRegistrationNo.Text = RowValue.RegNumber
                End If

                ' set lblRequestType 
                Dim lblRequestType As Label = CType(e.Item.FindControl("lblRequestType"), Label)
                If Not IsNothing(lblRequestType) Then
                    lblRequestType.Text = RowValue.MSPExMaster.MSPExType.Description
                End If

                'set lblRequestDate
                Dim lblRequestDate As Label = CType(e.Item.FindControl("lblRequestDate"), Label)
                Dim oDebitMemo As MSPExDebitMemo = New MSPExDebitMemoFacade(User).RetrieveByRegistration(RowValue)
                If Not IsNothing(lblRequestDate) AndAlso oDebitMemo.ID > 0 Then
                    lblRequestDate.Text = oDebitMemo.DocumentDate.ToString("dd/MM/yyyy")
                End If

            End If

        End If
    End Sub

End Class