Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports System.IO
Imports System.Text
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
Imports KTB.DNet.UI.Helper

Public Class FrmMSPUpgrade
    Inherits System.Web.UI.Page

    Private _sessHelper As New SessionHelper
    Private _strSessMSPRegistrationHistoryID As String = "MSPRegistrationHistoryID"
    Private objMSPRegistrationHistory As New MSPRegistrationHistory
    Dim arr As ArrayList
    Private objLoginDealer As Dealer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       If Not IsPostBack Then
            FillForm()
        End If
    End Sub

    Private Sub FillForm()
        Dim mspRegistrationHistoryID As Integer = _sessHelper.GetSession(_strSessMSPRegistrationHistoryID)
        objMSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(mspRegistrationHistoryID)

        lblDealer.Text = objMSPRegistrationHistory.MSPRegistration.Dealer.DealerCode
        lblDealerName.Text = objMSPRegistrationHistory.MSPRegistration.Dealer.DealerName
        lblName.Text = objMSPRegistrationHistory.MSPRegistration.MSPCustomer.Name1
        lblAge.Text = objMSPRegistrationHistory.MSPRegistration.MSPCustomer.Age
        lblAddress.Text = objMSPRegistrationHistory.MSPRegistration.MSPCustomer.Alamat
        lblKecamatan.Text = objMSPRegistrationHistory.MSPRegistration.MSPCustomer.Kecamatan
        lblKelurahan.Text = objMSPRegistrationHistory.MSPRegistration.MSPCustomer.Kelurahan
        lblPreArea.Text = objMSPRegistrationHistory.MSPRegistration.MSPCustomer.PreArea
        If Not IsNothing(objMSPRegistrationHistory.MSPRegistration.MSPCustomer.Province) Then
            lblProvince.Text = objMSPRegistrationHistory.MSPRegistration.MSPCustomer.Province.ProvinceName
        End If

        If objMSPRegistrationHistory.MSPRegistration.Dealer.DealerCode <> CType(_sessHelper.GetSession("DEALER"), Dealer).DealerCode Then
            lblNamaDealerUpgrade.Text = CType(_sessHelper.GetSession("DEALER"), Dealer).DealerName
            lblDealerCodeUpgrade.Text = CType(_sessHelper.GetSession("DEALER"), Dealer).DealerCode

        Else
            lblDealerCodeUpgrade.Visible = False
            lblNameDealerNameUpgrade.Visible = False
            lblNamaDealerUpgrade.Visible = False
            lblNameDealerCodeUpgrade.Visible = False
        End If

        lblPhoneNo.Text = objMSPRegistrationHistory.MSPRegistration.MSPCustomer.PhoneNo
        lblEmail.Text = objMSPRegistrationHistory.MSPRegistration.MSPCustomer.Email
        lblRegistrationDate.Text = objMSPRegistrationHistory.RegistrationDate.ToString("dd/MM/yyyy")
        lblSoldBy.Text = objMSPRegistrationHistory.SoldBy
        lblUpgradeDate.Text = Date.Now().ToString("dd/MM/yyyyy")
        lblMSPNumber.Text = objMSPRegistrationHistory.MSPRegistration.MSPCode

        Dim dtSet As DataSet = New MSPRegistrationFacade(User).RetrieveSp("EXEC sp_MSP_GetMSPMasterList '" + objMSPRegistrationHistory.MSPRegistration.ChassisMaster.ChassisNumber + "'")
        Dim dtTbl As DataTable = dtSet.Tables(0)

        If dtTbl.Rows.Count > 0 Then
            If (Convert.ToDateTime(dtTbl.Rows(0)("PKTDate")) = CType("1753-01-01 00:00:00.000", DateTime)) Then
                lblTglPkt.Text = String.Empty
                lblValidUntil.Text = String.Empty
            Else
                Dim pktDate As DateTime = Convert.ToDateTime(dtTbl.Rows(0)("PKTDate"))
                lblTglPkt.Text = pktDate.ToString("dd/MM/yyyy")
                lblValidUntil.Text = pktDate.AddYears(objMSPRegistrationHistory.MSPMaster.Duration).ToString("dd/MM/yyyy")
            End If
        End If

        ' bind data grid
        Dim arr As New ArrayList
        arr.Add(objMSPRegistrationHistory)
        dtgMSPUpgrade.DataSource = arr
        dtgMSPUpgrade.DataBind()

    End Sub

    Private Sub dtgMSPUpgrade_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMSPUpgrade.ItemDataBound
        ' set lblNo
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1
        End If

        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            Dim strMSPMasterID As String = String.Empty
            Dim rowValue As MSPRegistrationHistory = CType(e.Item.DataItem, MSPRegistrationHistory)

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Then
                ' MSP Registration History ID
                Dim lblMSPRegistrationHistoryID As Label = CType(e.Item.FindControl("lblMSPRegistrationHistoryID"), Label)
                If Not IsNothing(lblMSPRegistrationHistoryID) Then
                    lblMSPRegistrationHistoryID.Text = rowValue.ID
                End If
                ' no rangka
                Dim lblChassisNumber As Label = CType(e.Item.FindControl("lblChassisNumber"), Label)
                If Not IsNothing(lblChassisNumber) Then
                    lblChassisNumber.Text = rowValue.MSPRegistration.ChassisMaster.ChassisNumber
                End If
                ' tipe kendaraan
                Dim lblVehicleType As Label = CType(e.Item.FindControl("lblVehicleType"), Label)
                If Not IsNothing(lblVehicleType) Then
                    lblVehicleType.Text = rowValue.MSPMaster.VehicleType.VechileTypeCode
                End If
                ' tipe MSP lama
                Dim lblMSPType As Label = CType(e.Item.FindControl("lblMSPType"), Label)
                If Not IsNothing(lblMSPType) Then
                    lblMSPType.Text = rowValue.MSPMaster.MSPType.Description
                End If
                ' Durasi MSP lama
                Dim lblDuration As Label = CType(e.Item.FindControl("lblDuration"), Label)
                If Not IsNothing(lblDuration) Then
                    lblDuration.Text = rowValue.MSPMaster.Duration
                End If
                ' harga awal
                Dim lblAmount As Label = CType(e.Item.FindControl("lblAmount"), Label)
                Dim lblAmountHide As Label = CType(e.Item.FindControl("lblAmountHide"), Label)
                If Not IsNothing(lblAmount) Then
                    lblAmount.Text = (rowValue.MSPMaster.Amount * 1.1).ToString("C")
                    lblAmountHide.Text = rowValue.MSPMaster.Amount * 1.1
                End If
                ' tipe msp baru
                Dim dtSet As DataSet = New MSPRegistrationFacade(User).RetrieveSp("EXEC sp_MSP_GetMSPMasterList '" + rowValue.MSPRegistration.ChassisMaster.ChassisNumber + "'")
                Dim dtTbl As DataTable = dtSet.Tables(0)
                If dtTbl.Rows.Count > 0 Then
                    For Each row As DataRow In dtTbl.Rows
                        strMSPMasterID += "," & row("MSPMasterID").ToString
                    Next

                    Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPMaster), "ID", MatchType.InSet, "(" + strMSPMasterID.Substring(1, strMSPMasterID.Length - 1) + ")"))
                    crt.opAnd(New Criteria(GetType(MSPMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    arr = New MSPMasterFacade(User).Retrieve(crt)

                    Dim newArrObjMSPMaster = From a As MSPMaster In arr
                                         Group By a.MSPType.ID, a.MSPType.Description Into Group
                                    Select ID, Description

                    Dim ddlNewMSPType As DropDownList = CType(e.Item.FindControl("ddlNewMSPType"), DropDownList)
                    If Not IsNothing(ddlNewMSPType) Then
                        ddlNewMSPType.DataSource = newArrObjMSPMaster
                        ddlNewMSPType.DataTextField = "Description"
                        ddlNewMSPType.DataValueField = "ID"
                        ddlNewMSPType.DataBind()
                        ddlNewMSPType.Items.Insert(0, New ListItem("Silahkan Pilih", 0))
                        ddlNewMSPType.SelectedIndex = 0
                        ddlGridNewMSPType_SelectedIndexChanged(Me, EventArgs.Empty)
                    End If
                End If

            End If
        End If
    End Sub

    Protected Sub ddlGridNewMSPType_SelectedIndexChanged(sender As Object, e As EventArgs)
        For Each item As DataGridItem In dtgMSPUpgrade.Items
            Dim ddlNewMSPType As DropDownList = CType(item.FindControl("ddlNewMSPType"), DropDownList)
            Dim ddlNewDuration As DropDownList = CType(item.FindControl("ddlNewDuration"), DropDownList)
            Dim lblChassisNumber As Label = CType(item.FindControl("lblChassisNumber"), Label)

            ddlNewDuration.Items.Clear()
            If ddlNewMSPType.SelectedIndex <> 0 Then
                Dim strMSPMasterID As String = String.Empty
                Dim dtSet As DataSet = New MSPRegistrationFacade(User).RetrieveSp("EXEC sp_MSP_GetMSPMasterList '" + lblChassisNumber.Text + "'")
                Dim dtTbl As DataTable = dtSet.Tables(0)
                If dtTbl.Rows.Count > 0 Then
                    For Each row As DataRow In dtTbl.Rows
                        strMSPMasterID += "," & row("MSPMasterID").ToString
                    Next

                    Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crt.opAnd(New Criteria(GetType(MSPMaster), "MSPType", MatchType.Exact, ddlNewMSPType.SelectedValue))
                    crt.opAnd(New Criteria(GetType(MSPMaster), "ID", MatchType.InSet, "(" + strMSPMasterID.Substring(1, strMSPMasterID.Length - 1) + ")"))
                    Dim sorts As SortCollection = New SortCollection
                    sorts.Add(New Sort(GetType(MSPMaster), "Duration", Sort.SortDirection.ASC))
                    sorts.Add(New Sort(GetType(MSPMaster), "Amount", Sort.SortDirection.ASC))
                    arr = New MSPMasterFacade(User).Retrieve(crt, sorts)
                    Dim newArr = From a As MSPMaster In arr
                                              Select New With {.ID = a.ID, .DurationAmount = a.Duration.ToString + " Thn - " + String.Format("{0:#,##0}", Convert.ToDouble(a.MSPKm)) + " KM"}
                    ddlNewDuration.Items.Clear()
                    ddlNewDuration.DataSource = newArr
                    ddlNewDuration.DataTextField = "DurationAmount"
                    ddlNewDuration.DataValueField = "ID"
                    ddlNewDuration.DataBind()
                    ddlNewDuration.Items.Insert(0, New ListItem("Silahkan Pilih", 0))
                    ddlNewDuration.SelectedIndex = 0
                    ddlGridNewDuration_SelectedIndexChanged(Me, EventArgs.Empty)
                End If

            End If

        Next
    End Sub

    Protected Sub ddlGridNewDuration_SelectedIndexChanged(sender As Object, e As EventArgs)
        For Each item As DataGridItem In dtgMSPUpgrade.Items
            Dim ddlNewDuration As DropDownList = CType(item.FindControl("ddlNewDuration"), DropDownList)
            Dim lblNewAmount As Label = CType(item.FindControl("lblNewAmount"), Label)
            Dim lblDiffAmount As Label = CType(item.FindControl("lblDiffAmount"), Label)
            Dim lblAmountHide As Label = CType(item.FindControl("lblAmountHide"), Label)

            lblNewAmount.Text = String.Empty
            lblDiffAmount.Text = String.Empty

            If ddlNewDuration.SelectedIndex <> 0 Then
                Dim objMSPMaster As MSPMaster = New MSPMasterFacade(User).Retrieve(CInt(ddlNewDuration.SelectedValue))
                lblNewAmount.Text = (objMSPMaster.Amount * 1.1).ToString("C")
                Dim diffAmount As Integer = objMSPMaster.Amount * 1.1 - CDec(lblAmountHide.Text)
                lblDiffAmount.Text = diffAmount.ToString("C")
            End If

        Next
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("FrmMSPRegistrationList.aspx")
    End Sub

    Protected Sub btnUpgrade_Click(sender As Object, e As EventArgs) Handles btnUpgrade.Click
        Dim str As String = String.Empty
        If dtgMSPUpgrade.Items.Count > 0 Then
            For Each item As DataGridItem In dtgMSPUpgrade.Items
                Dim lblMSPRegistrationHistoryID As Label = CType(item.FindControl("lblMSPRegistrationHistoryID"), Label)
                If Not IsNothing(lblMSPRegistrationHistoryID) Then
                    Dim objMSPRegHistory As MSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(CInt(lblMSPRegistrationHistoryID.Text))
                    Dim newObjMSPRegHIstory As New MSPRegistrationHistory
                    Dim ddlNewMSPType As DropDownList = CType(item.FindControl("ddlNewMSPType"), DropDownList)
                    Dim ddlNewDuration As DropDownList = CType(item.FindControl("ddlNewDuration"), DropDownList)

                    If ddlNewMSPType.SelectedIndex < 1 Then
                        str += "\n" & "Tipe MSP Baru belum dipilih."
                    End If
                    If ddlNewDuration.SelectedIndex < 1 Then
                        str += "\n" & "Durasi Baru belum dipilih"
                    Else
                        newObjMSPRegHIstory.MSPMaster = New MSPMasterFacade(User).Retrieve(CInt(ddlNewDuration.SelectedValue))
                        newObjMSPRegHIstory.MSPRegistration = objMSPRegHistory.MSPRegistration
                        newObjMSPRegHIstory.RegistrationDate = CDate(Date.Now().ToString("yyyy-MM-dd"))
                        newObjMSPRegHIstory.RequestType = EnumStatusMSP.StatusType.Upgrade
                        newObjMSPRegHIstory.Status = EnumStatusMSP.Status.Baru
                        newObjMSPRegHIstory.SoldBy = objMSPRegHistory.SoldBy

                        Dim _MSPHelper As New MSPHelper()
                        str = _MSPHelper.ValidateUpgradeMSP(objMSPRegHistory, newObjMSPRegHIstory)

                    End If

                    If str = String.Empty Then
                        newObjMSPRegHIstory.Sequence = objMSPRegHistory.Sequence + 1
                        newObjMSPRegHIstory.SelisihAmount = newObjMSPRegHIstory.MSPMaster.Amount - objMSPRegHistory.MSPMaster.Amount

                        Dim resInt As Integer = New MSPRegistrationHistoryFacade(User).Insert(newObjMSPRegHIstory)
                        If resInt > 0 Then
                            MessageBox.Show("Tipe MSP berhasil diupgrade")

                            ddlNewMSPType.Enabled = False
                            ddlNewDuration.Enabled = False
                            btnUpgrade.Visible = False

                            'add popup info status payment belum selesai
                            Dim dtRegNo As New StringBuilder
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPTransferPayment), "Dealer.DealerCode", MatchType.Exact, lblDealer.Text))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPTransferPayment), "Status", MatchType.No, "6"))
                            Dim arrMSPTPayment As ArrayList = New MSPTransferPaymentFacade(User).Retrieve(criterias)
                            If arrMSPTPayment.Count > 0 Then
                                dtRegNo = New StringBuilder
                                For Each regNo As MSPTransferPayment In arrMSPTPayment
                                    If Not String.IsNullorEmpty(regNo.RegNumber) Then
                                        dtRegNo.Append(regNo.RegNumber)
                                        dtRegNo.Append(";")
                                    End If
                                Next
                                MessageBox.Show("Anda belum melakukan pembayaran dengan no registration: " & dtRegNo.ToString & " silahkan lakukan pembayaran")
                            End If

                        Else
                            MessageBox.Show("Gagal upgrade Tipe MSP")
                        End If
                    Else
                        MessageBox.Show(str.Substring(2, str.Length - 2))
                    End If
                End If
            Next
        End If
    End Sub

End Class