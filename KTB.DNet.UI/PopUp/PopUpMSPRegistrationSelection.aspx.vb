Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement

Public Class PopUpMSPRegistrationSelection
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

            If Not String.IsNullOrEmpty(dealerID) Then
                Dim objDealer As Dealer = New DealerFacade(User).Retrieve(CInt(dealerID))
                Dim strSql As String = "select MSPRegistrationID from MSPRegistrationHistory where BenefitMasterHeaderID = 0 AND Status=" & CInt(EnumStatusMSP.Status.Selesai).ToString

                crt = New CriteriaComposite(New Criteria(GetType(MSPRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crt.opAnd(New Criteria(GetType(MSPRegistration), "ID", MatchType.InSet, ("(" & strSql & ")")))
                crt.opAnd(New Criteria(GetType(MSPRegistration), "Dealer.ID", MatchType.InSet, ("(SELECT ID FROM Dealer WHERE CreditAccount ='" & objDealer.CreditAccount.ToString & "')")))

                ' exclude msp registration that already created payment
                GetAlreadyCreatedPayment()

                ' exclude selected item
                If Not String.IsNullOrEmpty(ChassisNumberList) Then
                    _sessHelper.SetSession(_strSessChassisNumberList, ChassisNumberList)

                    Dim newStr() As String = ChassisNumberList.Split(";")
                    Dim str As String = String.Empty
                    For i As Integer = 0 To newStr.Length - 1
                        str += "," & "'" & newStr(i) & "'"
                    Next
                    crt.opAnd(New Criteria(GetType(MSPRegistration), "ChassisMaster.ChassisNumber", MatchType.NotInSet, str.Substring(1, str.Length - 1)))
                End If

                Dim arr As ArrayList = New MSPRegistrationFacade(User).Retrieve(crt)

                If arr.Count > 0 Then
                    BindDataGrid(arr)
                End If

                If dtgMSPRegistration.Items.Count > 0 Then
                    btnChoose.Disabled = False
                Else
                    btnChoose.Disabled = True
                End If

            End If
        End If
    End Sub

    Private Function BindDataGrid(ByVal arr As ArrayList)
        dtgMSPRegistration.DataSource = arr
        dtgMSPRegistration.DataBind()
    End Function

    Private Function GetAlreadyCreatedPayment()
        Dim strSql As String = "SELECT a.ID FROM MSPRegistration a OUTER APPLY(SELECT COUNT(MSPRegistrationHistoryID) Total " &
                                "FROM MSPTransferPayment c INNER JOIN MSPTransferPaymentDetail d ON d.MSPTransferPaymentID = c.ID " &
                                "INNER JOIN MSPRegistrationHistory e ON e.ID = d.MSPRegistrationHistoryID " &
                                "INNER JOIN MSPRegistration f ON f.ID = e.MSPRegistrationID " &
                                "WHERE c.RowStatus = 0 AND d.RowStatus = 0 and e.RowStatus = 0 and f.RowStatus = 0 and f.id = a.ID and e.BenefitMasterHeaderID = 0)TotalPayment " &
                                "OUTER APPLY(SELECT count(1) as total FROM MSPRegistrationHistory g where g.RowStatus = 0 and g.MSPRegistrationID = a.ID and BenefitMasterHeaderID = 0)TotalHistory " &
                                "WHERE a.RowStatus = 0 AND TotalPayment.Total = TotalHistory.total"
        crt.opAnd(New Criteria(GetType(MSPRegistration), "ID", MatchType.NotInSet, strSql))
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

        crt = New CriteriaComposite(New Criteria(GetType(MSPRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim str As String = "select MSPRegistrationID from MSPRegistrationHistory where BenefitMasterHeaderID = 0 AND Status=" & CInt(EnumStatusMSP.Status.Selesai).ToString
        crt.opAnd(New Criteria(GetType(MSPRegistration), "ID", MatchType.InSet, "(" & str & ")"))

        Dim objDealer As Dealer = New DealerFacade(User).Retrieve(CInt(_sessHelper.GetSession(_strSessDealerID)))
        crt.opAnd(New Criteria(GetType(MSPRegistration), "Dealer.ID", MatchType.InSet, ("(SELECT ID FROM Dealer WHERE CreditAccount ='" & objDealer.CreditAccount.ToString & "')")))

        If Not String.IsNullOrEmpty(txtChassisNumber.Text) Then
            crt.opAnd(New Criteria(GetType(MSPRegistration), "ChassisMaster.ChassisNumber", MatchType.Partial, txtChassisNumber.Text))
        End If

        If Not String.IsNullOrEmpty(txtDealerCode.Text) Then
            Dim strSql As String = "SELECT ID FROM Dealer WHERE DealerCode = '" & txtDealerCode.Text & "'"
            crt.opAnd(New Criteria(GetType(MSPRegistration), "Dealer.ID", MatchType.InSet, "(" & strSql & ")"))
        End If

        ' exclude msp registration that already created payment
        GetAlreadyCreatedPayment()

        ' exclude MSP Registration already selected
        If Not String.IsNullOrEmpty(ChassisNumberList) Then
            Dim newStr() As String = ChassisNumberList.Split(";")
            Dim strChassis As String = String.Empty
            For i As Integer = 0 To newStr.Length - 1
                strChassis += "," & "'" & newStr(i) & "'"
            Next
            crt.opAnd(New Criteria(GetType(MSPRegistration), "ChassisMaster.ChassisNumber", MatchType.NotInSet, strChassis.Substring(1, strChassis.Length - 1)))
        End If

        _arr = New MSPRegistrationFacade(User).Retrieve(crt)

        If _arr.Count > 0 Then
            If indexPage >= 0 Then
                BindDataGrid(_arr)
            End If
        Else
            dtgMSPRegistration.DataSource = New ArrayList
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
                'Dim objMSPRegHistory As MSPRegistrationHistory = CType(e.Item.DataItem, MSPRegistrationHistory)
                'Dim RowValue As MSPRegistration = objMSPRegHistory.
                Dim RowValue As MSPRegistration = CType(e.Item.DataItem, MSPRegistration)
                Dim objMSPRegHistory As MSPRegistrationHistory = RowValue.MSPRegistrationHistorys(RowValue.MSPRegistrationHistorys.Count - 1)
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

                ' set lblNoMSP 
                Dim lblNoMSP As Label = CType(e.Item.FindControl("lblNoMSP"), Label)
                If Not IsNothing(lblNoMSP) Then
                    lblNoMSP.Text = RowValue.MSPCode
                End If

                ' set lblRequestType 
                Dim lblRequestType As Label = CType(e.Item.FindControl("lblRequestType"), Label)
                If Not IsNothing(lblRequestType) Then
                    lblRequestType.Text = CType(objMSPRegHistory.RequestType, EnumStatusMSP.StatusType).ToString
                End If

                ' set lblMSPCategory 
                Dim lblMSPCategory As Label = CType(e.Item.FindControl("lblMSPCategory"), Label)
                If Not IsNothing(lblMSPCategory) Then
                    If objMSPRegHistory.BenefitMasterHeaderID = 0 Then
                        lblMSPCategory.Text = "Paid"
                    Else
                        lblMSPCategory.Text = "Promo"
                    End If
                End If

            End If

        End If
    End Sub
End Class