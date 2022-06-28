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
Imports KTB.DNET.SAP
Imports KTB.DNet.UI.Helper

Public Class FrmMSPRegistrationList
    Inherits System.Web.UI.Page

    Private _view As Boolean = False
    Private _sessHelper As New SessionHelper
    Private _strSessSearch As String = "CRITERIAS"
    Private _strSessMSPRegistrationHistoryID As String = "MSPRegistrationHistoryID"
    Private _strSessMSPRegistrationList As String = "MSPRegistrationList"
    Private _strSessMSPRegistrationListall As String = "MSPRegistrationListall"
    Private _strSessStatusInput As String = "StatusInput"
    Dim crt As CriteriaComposite
    Dim arr As ArrayList
    Dim sortCols As SortCollection
    Private objLoginDealer As Dealer
    Private objMSPRegistration As MSPRegistration
    Private objMSPRegistrationHistory As MSPRegistrationHistory
    Private oldStatusHistory As Integer = -1

    Private Sub CheckPrivilege()
        _view = SecurityProvider.Authorize(Context.User, SR.MSPRegistration_view_privilege)
        If Not _view Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=MSP - Daftar Registrasi")
        End If

        btnSearch.Visible = _view
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        objLoginDealer = CType(_sessHelper.GetSession("DEALER"), Dealer)
        If Not IsPostBack Then
            BindDropDown()
            ViewState("CurrentSortColumn") = "CreatedTime"
            ViewState("CurrentSortDirect") = KTB.DNet.Domain.Search.Sort.SortDirection.DESC

            If GetSessionCriteria() Then
                CreateCriteria()
                BindDatagrid(dtgMSPRegistrationList.CurrentPageIndex)
            End If
        End If

    End Sub

    Private Sub SetSessionCriteria()
        Dim objSrcForm As ArrayList = New ArrayList
        objSrcForm.Add(txtKodeDealer.Text.Trim) '0
        objSrcForm.Add(txtMSPNo.Text.Trim) '1
        objSrcForm.Add(txtChassisNumber.Text.Trim) '2
        objSrcForm.Add(ddlCategory.SelectedValue) '3
        objSrcForm.Add(ddlRequestType.SelectedValue) '4
        objSrcForm.Add(ddlMSPType.SelectedValue) '5
        objSrcForm.Add(lboxStatus.SelectedValue) '6
        objSrcForm.Add(DateFrom.Value) '7
        objSrcForm.Add(DateTo.Value) '8
        objSrcForm.Add(dtgMSPRegistrationList.CurrentPageIndex) '9
        objSrcForm.Add(CType(ViewState("CurrentSortColumn"), String)) '10
        objSrcForm.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection)) '11

        Session("SessionCutomerRegistrationList") = objSrcForm
    End Sub

    Private Function GetSessionCriteria() As Boolean
        Dim objSrcForm As ArrayList = Session("SessionCutomerRegistrationList")
        If Not objSrcForm Is Nothing Then
            txtKodeDealer.Text = objSrcForm.Item(0)
            txtMSPNo.Text = objSrcForm.Item(1)
            txtChassisNumber.Text = objSrcForm.Item(2)
            ddlCategory.SelectedValue = objSrcForm.Item(3)
            ddlRequestType.SelectedValue = objSrcForm.Item(4)
            ddlMSPType.SelectedValue = objSrcForm.Item(5)
            lboxStatus.SelectedValue = objSrcForm.Item(6)
            DateFrom.Value = CType(objSrcForm.Item(7), Date)
            DateTo.Value = CType(objSrcForm.Item(8), Date)
            dtgMSPRegistrationList.CurrentPageIndex = CType(objSrcForm.Item(9), Integer)
            ViewState("CurrentSortColumn") = objSrcForm.Item(10)
            ViewState("CurrentSortDirect") = objSrcForm.Item(11)

            Return True
        End If
        Return False
    End Function

    Private Sub BindDropDown()
        'dropdown request type
        ddlRequestType.Items.Clear()
        ddlRequestType.DataSource = New EnumStatusMSP().RetrieveStatusType()
        ddlRequestType.DataTextField = "NameTipe"
        ddlRequestType.DataValueField = "ValTipe"
        ddlRequestType.DataBind()
        ddlRequestType.Items.Insert(0, New ListItem("Silakan Pilih", -1))
        ddlRequestType.SelectedIndex = 0

        ' dropdown msptype
        crt = New CriteriaComposite(New Criteria(GetType(MSPType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(MSPType), "Sequence", Search.Sort.SortDirection.ASC))
        ddlMSPType.Items.Clear()
        ddlMSPType.DataSource = New MSPTypeFacade(User).Retrieve(crt, sortColl)
        ddlMSPType.DataTextField = "Description"
        ddlMSPType.DataValueField = "ID"
        ddlMSPType.DataBind()
        ddlMSPType.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlMSPType.SelectedIndex = 0

        ' dropdown status
        lboxStatus.Items.Clear()
        Dim arrStatus As ArrayList = New EnumStatusMSP().RetrieveStatus()
        If objLoginDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            arrStatus.RemoveAt(0)
        End If
        lboxStatus.DataSource = arrStatus
        lboxStatus.DataTextField = "NameTipe"
        lboxStatus.DataValueField = "ValTipe"
        lboxStatus.DataBind()


        Dim crt2 = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crt2.opAnd(New Criteria(GetType(Category), "CategoryCode", MatchType.No, "CV"))
        Dim sortss As SortCollection = New SortCollection
        sortss.Add(New Sort(GetType(Category), "CategoryCode", Search.Sort.SortDirection.ASC))
        arr = New CategoryFacade(User).RetrieveByCriteria(crt2, sortss)

        ddlCategoryV.Items.Clear()
        ddlCategoryV.DataSource = arr
        ddlCategoryV.DataTextField = "CategoryCode".ToUpper
        ddlCategoryV.DataValueField = "ID"
        ddlCategoryV.DataBind()
        ddlCategoryV.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlCategoryV.SelectedIndex = 0
        ddlCategoryV_SelectedIndexChanged(Me, System.EventArgs.Empty)


    End Sub

    Private Sub BindddlTipe(ByVal Category As String)
        ddlVechileType.Items.Clear()
        If ddlVechileModel.SelectedIndex <> 0 Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "Category.CategoryCode", MatchType.Exact, Category))
            'CommonFunction.SetVehicleSubCategoryCriterias(ddlVechileModel, ddlCategoryV.SelectedItem.Text, criterias, "VechileType")

            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlVechileModel.SelectedValue
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("VechileTypeCode")) Then
                sortColl.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            ddlVechileType.DataSource = New VechileTypeFacade(User).Retrieve(criterias, sortColl)
            ddlVechileType.DataTextField = "VechileTypeCode"
            ddlVechileType.DataValueField = "ID"
            ddlVechileType.DataBind()
        End If
        ddlVechileType.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        CreateCriteria()
        dtgMSPRegistrationList.CurrentPageIndex = 0
        BindDatagrid(dtgMSPRegistrationList.CurrentPageIndex)
    End Sub

    Private Sub CreateCriteria()
        Dim str As String = String.Empty
        Dim isStrSql As Integer = 0
        Dim strSql As String = "SELECT MSPRegistrationID FROM VwMSPLastRegistrationHistory WHERE 0=0 "

        crt = New CriteriaComposite(New Criteria(GetType(MSPRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtKodeDealer.Text.Trim <> String.Empty Then
            Dim strDealerCode() As String = txtKodeDealer.Text.Split(";")
            Dim newStrDealerCode As String
            For i As Integer = 0 To strDealerCode.Length - 1
                newStrDealerCode += "," & strDealerCode(i)
            Next
            'crt.opAnd(New Criteria(GetType(MSPRegistration), "Dealer.DealerCode", MatchType.InSet, "(" & newStrDealerCode.Substring(1, newStrDealerCode.Length - 1) & ")"))

            crt.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPRegistration), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))

        End If

        If txtMSPNo.Text.Trim <> String.Empty Then
            crt.opAnd(New Criteria(GetType(MSPRegistration), "MSPCode", MatchType.Partial, txtMSPNo.Text.Trim))
        End If

        If txtChassisNumber.Text.Trim <> String.Empty Then
            crt.opAnd(New Criteria(GetType(MSPRegistration), "ChassisMaster.ChassisNumber", MatchType.Partial, txtChassisNumber.Text.Trim))
        End If

        If ddlCategory.SelectedIndex <> 0 Then
            If ddlCategory.SelectedValue = "PAID" Then
                strSql += " AND BenefitMasterHeaderID = 0"
                isStrSql += 1
            ElseIf ddlCategory.SelectedValue = "PROMO" Then
                strSql += " AND BenefitMasterHeaderID > 0"
                isStrSql += 1
            End If
        End If

        If ddlRequestType.SelectedIndex <> 0 Then
            strSql += " AND RequestType = '" + ddlRequestType.SelectedValue + "'"
            isStrSql += 1
        End If

        If ddlMSPType.SelectedIndex <> 0 Then
            strSql += " AND MSPMasterID IN (SELECT Z.ID FROM MSPMaster Z WHERE Z.MSPTypeID = '" + ddlMSPType.SelectedValue + "')"
            isStrSql += 1
        End If

        If chkRequestDate.Checked Then
            If DateFrom.Value <> "#12:00:00 AM#" And DateTo.Value <> "#12:00:00 AM#" Then
                strSql += " AND (RegistrationDate BETWEEN '" + Format(DateFrom.Value, "yyyy-MM-dd") + "' AND '" + Format(DateTo.Value, "yyyy-MM-dd") + "')"
                isStrSql += 1
            ElseIf DateFrom.Value = "#12:00:00 AM#" Then
                str += "\n" + "Tanggal pengajuan mulai tidak boleh kosong."
            ElseIf DateTo.Value = "#12:00:00 AM#" Then
                str += "\n" + "Tanggal pengajuan sampai tidak boleh kosong."
            End If
        End If

        If objLoginDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            strSql += " AND Status <> 0 AND Status <> 2"
            isStrSql += 1
        Else
            crt.opAnd(New Criteria(GetType(MSPRegistration), "Dealer.DealerGroup.ID", MatchType.Exact, objLoginDealer.DealerGroup.ID))
        End If

        If lboxStatus.SelectedIndex <> -1 Then
            Dim SelectedStatus As String = GetSelectedItem(lboxStatus)
            strSql += " AND Status IN(" & SelectedStatus & ")"
            isStrSql += 1
        End If

        If isStrSql > 0 Then
            crt.opAnd(New Criteria(GetType(MSPRegistration), "ID", MatchType.InSet, "(" + strSql + ")"))
        End If

        If ddlCategoryV.SelectedIndex <> 0 Then
            'Dim str2 As String = "(SELECT ID FROM VechileType WHERE CategoryID = " + ddlCategoryV.SelectedValue + ")"
            'crt.opAnd(New Criteria(GetType(MSPRegistration), "ChassisMaster.VechileColor.VechileType.ID", MatchType.InSet, str2))

            If ddlVechileModel.SelectedIndex <> 0 Then
                'str2 = "(SELECT ID FROM VechileType WHERE ModelID = " + ddlVechileModel.SelectedValue + ")"
                'crt.opAnd(New Criteria(GetType(MSPRegistration), "ChassisMaster.VechileColor.VechileType.ID", MatchType.InSet, str2))

                'CommonFunction.SetVehicleSubCategoryCriterias(ddlVechileModel, ddlCategoryV.SelectedItem.Text, crt, "MSPRegistration")

                Dim strSql2 As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlVechileModel.SelectedValue
                crt.opAnd(New Criteria(GetType(MSPRegistration), "ChassisMaster.VechileColor.VechileType.VechileModel.ID", MatchType.InSet, "(" & strSql2 & ")"))

                If ddlVechileType.SelectedIndex <> 0 Then
                    crt.opAnd(New Criteria(GetType(MSPRegistration), "ChassisMaster.VechileColor.VechileType.ID", MatchType.Exact, ddlVechileType.SelectedValue))
                End If
            Else
                Dim str2 As String = "(SELECT ID FROM VechileType WHERE CategoryID = " + ddlCategoryV.SelectedValue + ")"
                crt.opAnd(New Criteria(GetType(MSPRegistration), "ChassisMaster.VechileColor.VechileType.ID", MatchType.InSet, str2))
            End If
        End If

        If str <> String.Empty Then
            MessageBox.Show(str.Substring(2, str.Length - 2))
            Return
        End If

        _sessHelper.SetSession(_strSessSearch, crt)
    End Sub

    Private Function GetSelectedItem(ByVal listboxStatus As ListBox) As String
        Dim _strStatus As String = String.Empty
        For Each item As ListItem In listboxStatus.Items
            If item.Selected Then
                If _strStatus = String.Empty Then
                    _strStatus = item.Value
                Else
                    _strStatus = _strStatus & "," & item.Value
                End If
            End If
        Next
        Return _strStatus
    End Function

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim newArr As New ArrayList
        arr = New MSPRegistrationFacade(User).RetrieveByCriteria(CType(_sessHelper.GetSession(_strSessSearch), CriteriaComposite), indexPage + 1, dtgMSPRegistrationList.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dtgMSPRegistrationList.DataSource = arr
        dtgMSPRegistrationList.VirtualItemCount = totalRow
        dtgMSPRegistrationList.DataBind()
        ' set button in process area
        If arr.Count > 0 Then
            SetProcessSection()
        End If

        ' set session array data
        _sessHelper.SetSession(_strSessMSPRegistrationList, arr)
    End Sub

    Private Function BindDatagrid() As ArrayList

        Dim lArr As New ArrayList

        Dim sortColl As SortCollection = New SortCollection

        If (Not IsNothing(CType(ViewState("CurrentSortColumn"), String))) Then
            sortColl.Add(New Sort(GetType(MSPRegistration), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
        Else
            sortColl = Nothing
        End If

        lArr = New MSPRegistrationFacade(User).Retrieve(CType(_sessHelper.GetSession(_strSessSearch), CriteriaComposite), sortcoll)


        Return lArr
    End Function

    Private Function SetProcessSection()
        lblChangeStatus.Visible = True
        btnProses.Visible = True
        ddlProses.Visible = True
        ddlProses.Items.Clear()
        If objLoginDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            btnTransfertoSAP.Visible = True
            'btnTransferUlangtoSAP.Visible = True
            Dim arrProses As ArrayList = New EnumStatusMSP().RetrieveStatusForProsesKTB
            arrProses.RemoveAt(arrProses.Count - 1)
            ddlProses.DataSource = arrProses
        Else
            'btnDownload.Visible = True
            ddlProses.DataSource = New EnumStatusMSP().RetrieveStatusForProsesDealer
        End If

        ddlProses.DataTextField = "NameTipe"
        ddlProses.DataValueField = "ValTipe"
        ddlProses.DataBind()
        ddlProses.Items.Insert(0, New ListItem("Silakan Pilih", -1))
        ddlProses.SelectedIndex = 0

    End Function

    Private Sub dtgMSPRegistrationList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgMSPRegistrationList.ItemCommand
        If (e.CommandName.ToUpper = "VIEW") Then
            SetSessionCriteria()
            CommandViewData(e)
        ElseIf (e.CommandName.ToUpper = "EDIT") Then
            SetSessionCriteria()
            CommandEditData(e)
        ElseIf (e.CommandName.ToUpper = "DELETE") Then
            CommandDeleteData(e)
        ElseIf (e.CommandName.ToUpper = "UPGRADE") Then
            SetSessionCriteria()
            CommandUpgradePackage(e)
        ElseIf (e.CommandName.ToUpper = "HISTORY") Then
            SetSessionCriteria()
            CommandHistory(e)
        End If
    End Sub

    Private Function CommandHistory(ByVal e As DataGridCommandEventArgs)
        Dim lblMSPRegistrationHistoryID As Label = CType(e.Item.FindControl("lblMSPRegistrationHistoryID"), Label)
        _sessHelper.SetSession(_strSessMSPRegistrationHistoryID, lblMSPRegistrationHistoryID.Text)
        Response.Redirect("FrmMSPHistory.aspx")
    End Function

    Private Function CommandUpgradePackage(ByVal e As DataGridCommandEventArgs)
        Dim lblMSPRegistrationHistoryID As Label = CType(e.Item.FindControl("lblMSPRegistrationHistoryID"), Label)
        _sessHelper.SetSession(_strSessMSPRegistrationHistoryID, lblMSPRegistrationHistoryID.Text)
        Response.Redirect("FrmMSPUpgrade.aspx")
    End Function

    Private Function CommandViewData(ByVal e As DataGridCommandEventArgs)
        Dim lblMSPRegistrationHistoryID As Label = CType(e.Item.FindControl("lblMSPRegistrationHistoryID"), Label)
        _sessHelper.SetSession(_strSessStatusInput, "VIEW")
        _sessHelper.SetSession(_strSessMSPRegistrationHistoryID, lblMSPRegistrationHistoryID.Text)
        Response.Redirect("FrmMSPRegistration.aspx")
    End Function

    Private Function CommandEditData(ByVal e As DataGridCommandEventArgs)
        Dim lblMSPRegistrationHistoryID As Label = CType(e.Item.FindControl("lblMSPRegistrationHistoryID"), Label)
        _sessHelper.SetSession(_strSessStatusInput, "UPDATE")
        _sessHelper.SetSession(_strSessMSPRegistrationHistoryID, lblMSPRegistrationHistoryID.Text)
        Response.Redirect("FrmMSPRegistration.aspx")
    End Function

    Private Function CommandDeleteData(ByVal e As DataGridCommandEventArgs)
        Dim lblMSPRegistrationHistoryID As Label = CType(e.Item.FindControl("lblMSPRegistrationHistoryID"), Label)
        objMSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(CInt(lblMSPRegistrationHistoryID.Text))
        objMSPRegistration = objMSPRegistrationHistory.MSPRegistration
        arr = CType(_sessHelper.GetSession(_strSessMSPRegistrationList), ArrayList)

        ' jika request type yang dihapus = upgrade maka yg dihapus hanya history upgradenya saja
        ' dan jika yg dihapus request type = baru maka yang dihapus keseluruhan data registrasinya
        If objMSPRegistration.MSPRegistrationHistorys.Count = 1 Then
            Dim facMSPRegistration As New MSPRegistrationFacade(User)
            facMSPRegistration.Delete(objMSPRegistration)
            ' index dihapus karena status = baru
            arr.RemoveAt(e.Item.ItemIndex)
            BindDatagrid(0)
            MessageBox.Show("Data berhasil dihapus.")

        ElseIf (objMSPRegistration.MSPRegistrationHistorys.Count > 1) Then
            Dim facMSPRegistrationHistory As New MSPRegistrationHistoryFacade(User)
            facMSPRegistrationHistory.Delete(objMSPRegistrationHistory)
            ' index tidak dihapus kerena yang dihapus status = update
            BindDatagrid(0)
            MessageBox.Show("Data berhasil dihapus.")
        End If

    End Function

    Private Sub dtgMSPRegistrationList_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMSPRegistrationList.ItemDataBound
        ' set lblNo
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgMSPRegistrationList.CurrentPageIndex * dtgMSPRegistrationList.PageSize)
        End If

        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            Dim rowValue As MSPRegistration = CType(e.Item.DataItem, MSPRegistration)
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Then
                ' get latest history
                arr = rowValue.MSPRegistrationHistorys
                Dim objMSPRegistrationHistory As MSPRegistrationHistory = CType(arr(arr.Count - 1), MSPRegistrationHistory)

                ' set Dealer
                Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
                If Not IsNothing(lblDealer) Then
                    lblDealer.Text = rowValue.Dealer.DealerCode
                End If

                ' set No MSP
                Dim lblMSPCode As Label = CType(e.Item.FindControl("lblMSPCode"), Label)
                If Not IsNothing(lblMSPCode) Then
                    lblMSPCode.Text = rowValue.MSPCode
                End If

                ' set No Rangka
                Dim lblChassisNumber As Label = CType(e.Item.FindControl("lblChassisNumber"), Label)
                If Not IsNothing(lblChassisNumber) Then
                    lblChassisNumber.Text = rowValue.ChassisMaster.ChassisNumber
                End If

                ' set tipe kendaraan
                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                If Not IsNothing(lblStatus) Then
                    lblStatus.Text = CType(objMSPRegistrationHistory.Status, EnumStatusMSP.Status).ToString
                End If

                ' set Kategori
                Dim lblCategory As Label = CType(e.Item.FindControl("lblCategory"), Label)
                If Not IsNothing(lblCategory) Then
                    If objMSPRegistrationHistory.BenefitMasterHeaderID = 0 Then
                        lblCategory.Text = "Paid"
                    Else
                        lblCategory.Text = "Promo"
                    End If
                End If

                ' set tipe pengajuan
                Dim lblRequestType As Label = CType(e.Item.FindControl("lblRequestType"), Label)
                If Not IsNothing(lblRequestType) Then
                    lblRequestType.Text = CType(objMSPRegistrationHistory.RequestType, EnumStatusMSP.StatusType).ToString
                End If

                ' set MSP Type
                Dim lblMSPType As Label = CType(e.Item.FindControl("lblMSPType"), Label)
                If Not IsNothing(lblMSPType) Then
                    lblMSPType.Text = objMSPRegistrationHistory.MSPMaster.MSPType.Description
                End If

                ' set Tanggal pengajuan
                Dim lblRequestDate As Label = CType(e.Item.FindControl("lblRequestDate"), Label)
                If Not IsNothing(lblRequestDate) Then
                    lblRequestDate.Text = objMSPRegistrationHistory.RegistrationDate.ToString("dd/MM/yyyy")
                End If

                ' set MSP Registration History ID
                Dim lblMSPRegistrationHistoryID As Label = CType(e.Item.FindControl("lblMSPRegistrationHistoryID"), Label)
                If Not IsNothing(lblMSPRegistrationHistoryID) Then
                    lblMSPRegistrationHistoryID.Text = objMSPRegistrationHistory.ID
                End If

                Dim lblVehicleDescription As Label = e.Item.FindControl("lblVehicleDescription")
                If Not IsNothing(lblVehicleDescription) AndAlso Not IsNothing(rowValue.ChassisMaster.VechileColor) Then
                    lblVehicleDescription.Text = rowValue.ChassisMaster.VechileColor.VechileType.Description
                End If

                Dim strClaim As String = String.Empty

                ' set Claim Status
                Dim lblClaimStatus As Label = CType(e.Item.FindControl("lblClaimStatus"), Label)
                If Not IsNothing(lblClaimStatus) Then
                    crt = New CriteriaComposite(New Criteria(GetType(MSPClaim), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crt.opAnd(New Criteria(GetType(MSPClaim), "MSPRegistrationHistory.ID", MatchType.Exact, objMSPRegistrationHistory.ID))
                    Dim newArr As ArrayList = New MSPClaimFacade(User).Retrieve(crt)
                    If newArr.Count > 0 Then
                        For Each itemObjMSPClaim As MSPClaim In newArr
                            strClaim += ", " & itemObjMSPClaim.PMKind.KindDescription 'itemObjMSPClaim.PMHeader.PMKind.KindDescription
                        Next
                    End If

                    If strClaim <> String.Empty Then
                        strClaim = strClaim.Substring(2, strClaim.Length - 2)
                    End If
                    lblClaimStatus.Text = strClaim
                End If

                ' set download sertifikat jika status validasi dst.
                If Not (objMSPRegistrationHistory.Status = EnumStatusMSP.Status.Baru OrElse objMSPRegistrationHistory.Status = EnumStatusMSP.Status.Batal_Validasi) Then
                    ' label download
                    Dim lblDownload As Label = CType(e.Item.FindControl("lblDownload"), Label)
                    If Not IsNothing(lblDownload) Then
                        lblDownload.Visible = True
                        lblDownload.ToolTip = "Cetak Sertifikat"
                        lblDownload.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpMSPCertificate.aspx?MSPRegistrationHistoryID=" & objMSPRegistrationHistory.ID, "scrollbars=auto", 1000, 900, "DealerSelection")
                    End If

                    ' button upgrade
                    If objLoginDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                        'start pencabutan validasi dealer, msp bisa di upgrade dimanapun, cr enhancement service doni gc
                        'If objLoginDealer.ID = rowValue.ChassisMaster.Dealer.ID Then 'cek sold dealer 
                        'If objLoginDealer.ID = rowValue.Dealer.ID Then
                        Dim lbtnUpgrade As LinkButton = CType(e.Item.FindControl("lbtnUpgrade"), LinkButton)
                        If Not IsNothing(lbtnUpgrade) Then
                            ' jika MSP sudah diclaim dengan status baru dst. btnupgrade disembunyikan
                            If strClaim = String.Empty Then
                                lbtnUpgrade.Visible = True
                            End If
                        End If
                        'End If
                        'End If
                        'end pencabutan validasi dealer, msp bisa di upgrade dimanapun, cr enhancement service doni gc
                    End If
                End If

                ' show button edit if status = validasi and login as mks
                If objMSPRegistrationHistory.Status = EnumStatusMSP.Status.Validasi Then
                    If objLoginDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                        ' button edit
                        If objMSPRegistrationHistory.BenefitMasterHeaderID = 0 Then
                            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                            If Not IsNothing(lbtnEdit) Then
                                lbtnEdit.Visible = True
                            End If
                        End If
                    End If
                End If

                If objMSPRegistrationHistory.Status = EnumStatusMSP.Status.Baru Or objMSPRegistrationHistory.Status = EnumStatusMSP.Status.Batal_Validasi Then
                    ' button edit
                    If objLoginDealer.ID = rowValue.Dealer.ID Then
                        If objMSPRegistrationHistory.BenefitMasterHeaderID = 0 Then
                            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                            If Not IsNothing(lbtnEdit) Then
                                lbtnEdit.Visible = True
                            End If
                        End If
                    End If

                    ' button delete
                    If objLoginDealer.ID = rowValue.Dealer.ID Then
                        If objMSPRegistrationHistory.BenefitMasterHeaderID = 0 Then
                            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                            If Not IsNothing(lbtnDelete) Then
                                lbtnDelete.Visible = True
                                lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                            End If
                        End If
                    End If

                End If

                ' button view
                Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
                If Not IsNothing(lbtnView) Then
                    lbtnView.Visible = True
                End If

                ' button History 
                Dim lbtnHistory As LinkButton = CType(e.Item.FindControl("lbtnHistory"), LinkButton)
                If Not IsNothing(lbtnHistory) Then
                    lbtnHistory.Visible = True
                End If

                ' button History Status
                Dim lblHistoryStatus As Label = CType(e.Item.FindControl("lblHistoryStatus"), Label)
                lblHistoryStatus.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpChangeStatusHistoryMSPRegistration.aspx?DocType=" & LookUp.DocumentType.MSP_Registration & "&DocNumber=" & objMSPRegistrationHistory.ID, "", 400, 400, "DealerSelection")

            End If
        End If
    End Sub

    Protected Sub btnProses_Click(sender As Object, e As EventArgs) Handles btnProses.Click
        Select Case ddlProses.SelectedValue
            Case 1 'Validasi
                btnValidasi_Click()
            Case 2 'Batal Validasi
                btnCancelValidasi_Click()
            Case 3 'Konfirmasi
                btnConfirm_Click()
            Case 4 'Batal konfirmasi
                btnRejected_Click()
        End Select

    End Sub

    Private Function btnValidasi_Click()
        Dim strMsg As String = String.Empty
        ' get all checked data
        arr = New ArrayList
        arr = PopulateCustomerRegistrationValidasi()

        If arr.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("Registrasi Konsumen", "Baru"))
        Else
            For i As Integer = 0 To arr.Count - 1
                objMSPRegistrationHistory = New MSPRegistrationHistory
                objMSPRegistrationHistory = CType(arr(i), MSPRegistrationHistory)

                'If objLoginDealer.ID <> objMSPRegistrationHistory.MSPRegistration.Dealer.ID Then
                '    strMsg += "\n" & "Anda tidak punya akses Validasi pada No Rangka " & objMSPRegistrationHistory.MSPRegistration.ChassisMaster.ChassisNumber
                'Else
                If (New MSPRegistrationFacade(User).Update(objMSPRegistrationHistory.MSPRegistration, objMSPRegistrationHistory, objMSPRegistrationHistory.MSPRegistration.MSPCustomer) = -1) Then
                    strMsg += "\n" & "No Rangka " & objMSPRegistrationHistory.MSPRegistration.ChassisMaster.ChassisNumber & " gagal validasi."
                Else
                    strMsg += "\n" & "No Rangka " & objMSPRegistrationHistory.MSPRegistration.ChassisMaster.ChassisNumber & " berhasil validasi."

                    ' add to history status
                    Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                    objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.MSP_Registration), objMSPRegistrationHistory.ID, oldStatusHistory, objMSPRegistrationHistory.Status)
                End If
                'End If

            Next
            BindDatagrid(0)
            If strMsg <> String.Empty Then
                MessageBox.Show(strMsg.Substring(2, strMsg.Length - 2))
            End If
        End If
    End Function

    Private Function PopulateCustomerRegistrationValidasi() As ArrayList
        Dim dtgItem As DataGridItem
        arr = New ArrayList

        For Each dtgItem In dtgMSPRegistrationList.Items
            Dim chkSelect As CheckBox = dtgItem.FindControl("chkSelect")
            If chkSelect.Checked Then
                Dim idMSPRegistrationHistory As Label = CType(dtgItem.FindControl("lblMSPRegistrationHistoryID"), Label)
                objMSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(CInt(idMSPRegistrationHistory.Text))

                ' set MSPCode
                Dim strCode = "MSP" & objMSPRegistrationHistory.MSPRegistration.ID.ToString.PadLeft(7, "0")

                If objMSPRegistrationHistory.Status = EnumStatusMSP.Status.Baru Or objMSPRegistrationHistory.Status = EnumStatusMSP.Status.Batal_Validasi Then
                    ' set old status to history status
                    oldStatusHistory = objMSPRegistrationHistory.Status
                    objMSPRegistrationHistory.Status = EnumStatusMSP.Status.Validasi
                    objMSPRegistrationHistory.MSPRegistration.MSPCode = strCode
                    arr.Add(objMSPRegistrationHistory)
                End If
            End If
        Next
        Return arr
    End Function

    Private Function btnCancelValidasi_Click()
        Dim strMsg As String = String.Empty
        ' get all checked data
        arr = New ArrayList
        arr = PopulateCustomerRegistrationCancelValidasi()

        If arr.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("Registrasi Konsumen", "Validasi"))
        Else
            For i As Integer = 0 To arr.Count - 1
                objMSPRegistrationHistory = New MSPRegistrationHistory
                objMSPRegistrationHistory = CType(arr(i), MSPRegistrationHistory)

                If objMSPRegistrationHistory.IsDownloadCertificate Then
                    strMsg += "\n" & "Gagal batal validasi!. Print sertifikat sudah dilakukan pada No Rangka " & objMSPRegistrationHistory.MSPRegistration.ChassisMaster.ChassisNumber
                    Continue For
                End If

                If objLoginDealer.ID <> objMSPRegistrationHistory.MSPRegistration.Dealer.ID Then
                    strMsg += "\n" & "Anda tidak punya akses Batal Validasi pada No Rangka " & objMSPRegistrationHistory.MSPRegistration.ChassisMaster.ChassisNumber
                Else
                    If (New MSPRegistrationFacade(User).Update(objMSPRegistrationHistory.MSPRegistration, objMSPRegistrationHistory, objMSPRegistrationHistory.MSPRegistration.MSPCustomer) = -1) Then
                        strMsg += "\n" & "No Rangka " & objMSPRegistrationHistory.MSPRegistration.ChassisMaster.ChassisNumber & " gagal batal validasi."
                    Else
                        strMsg += "\n" & "No Rangka " & objMSPRegistrationHistory.MSPRegistration.ChassisMaster.ChassisNumber & " berhasil batal validasi."

                        ' add to history status
                        Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                        objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.MSP_Registration), objMSPRegistrationHistory.ID, oldStatusHistory, objMSPRegistrationHistory.Status)
                    End If
                End If

            Next
            BindDatagrid(0)
            If strMsg <> String.Empty Then
                MessageBox.Show(strMsg.Substring(2, strMsg.Length - 2))
            End If
        End If
    End Function

    Private Function PopulateCustomerRegistrationCancelValidasi() As ArrayList
        Dim dtgItem As DataGridItem
        arr = New ArrayList

        For Each dtgItem In dtgMSPRegistrationList.Items
            Dim chkSelect As CheckBox = dtgItem.FindControl("chkSelect")
            If chkSelect.Checked Then
                Dim idMSPRegistrationHistory As Label = CType(dtgItem.FindControl("lblMSPRegistrationHistoryID"), Label)
                objMSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(CInt(idMSPRegistrationHistory.Text))

                If objMSPRegistrationHistory.Status = EnumStatusMSP.Status.Validasi Then
                    ' set old status to history status
                    oldStatusHistory = objMSPRegistrationHistory.Status
                    objMSPRegistrationHistory.Status = EnumStatusMSP.Status.Baru
                    objMSPRegistrationHistory.MSPRegistration.MSPCode = String.Empty
                    arr.Add(objMSPRegistrationHistory)
                End If
            End If
        Next
        Return arr
    End Function

    Private Function btnConfirm_Click()
        Dim strMsg As String = String.Empty
        ' get all checked data
        arr = New ArrayList
        arr = PopulateCustomerRegistrationConfirm()

        If arr.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("Registrasi Konsumen", "Validasi"))
        Else
            For i As Integer = 0 To arr.Count - 1
                objMSPRegistrationHistory = New MSPRegistrationHistory
                objMSPRegistrationHistory = CType(arr(i), MSPRegistrationHistory)

                If (New MSPRegistrationHistoryFacade(User).Update(objMSPRegistrationHistory) = -1) Then
                    strMsg += "\n" & "No Rangka " & objMSPRegistrationHistory.MSPRegistration.ChassisMaster.ChassisNumber & " gagal konfirmasi."
                Else
                    strMsg += "\n" & "No Rangka " & objMSPRegistrationHistory.MSPRegistration.ChassisMaster.ChassisNumber & " berhasil konfirmasi."

                    ' add to history status
                    Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                    objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.MSP_Registration), objMSPRegistrationHistory.ID, oldStatusHistory, objMSPRegistrationHistory.Status)
                End If

            Next
            BindDatagrid(0)
            If strMsg <> String.Empty Then
                MessageBox.Show(strMsg.Substring(2, strMsg.Length - 2))
            End If
        End If
    End Function

    Private Function PopulateCustomerRegistrationConfirm() As ArrayList
        Dim dtgItem As DataGridItem
        arr = New ArrayList

        For Each dtgItem In dtgMSPRegistrationList.Items
            Dim chkSelect As CheckBox = dtgItem.FindControl("chkSelect")
            If chkSelect.Checked Then
                Dim idMSPRegistrationHistory As Label = CType(dtgItem.FindControl("lblMSPRegistrationHistoryID"), Label)
                objMSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(CInt(idMSPRegistrationHistory.Text))

                If objMSPRegistrationHistory.Status = EnumStatusMSP.Status.Validasi Then
                    ' set old status to history status
                    oldStatusHistory = objMSPRegistrationHistory.Status
                    objMSPRegistrationHistory.Status = EnumStatusMSP.Status.Konfirmasi
                    arr.Add(objMSPRegistrationHistory)
                End If
            End If
        Next
        Return arr
    End Function

    Private Function btnRejected_Click()
        Dim strMsg As String = String.Empty
        ' get all checked data
        arr = New ArrayList
        arr = PopulateCustomerRegistrationRejected()

        If arr.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("Registrasi Konsumen", "Konfirmasi"))
        Else
            For i As Integer = 0 To arr.Count - 1
                objMSPRegistrationHistory = New MSPRegistrationHistory
                objMSPRegistrationHistory = CType(arr(i), MSPRegistrationHistory)

                If (New MSPRegistrationHistoryFacade(User).Update(objMSPRegistrationHistory) = -1) Then
                    strMsg += "\n" & "No Rangka " & objMSPRegistrationHistory.MSPRegistration.ChassisMaster.ChassisNumber & " gagal batal konfirmasi."
                Else
                    strMsg += "\n" & "No Rangka " & objMSPRegistrationHistory.MSPRegistration.ChassisMaster.ChassisNumber & " berhasil batal konfirmasi."

                    ' add to history status
                    Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                    objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.MSP_Registration), objMSPRegistrationHistory.ID, oldStatusHistory, objMSPRegistrationHistory.Status)
                End If

            Next
            BindDatagrid(0)
            If strMsg <> String.Empty Then
                MessageBox.Show(strMsg.Substring(2, strMsg.Length - 2))
            End If
        End If
    End Function

    Private Function PopulateCustomerRegistrationRejected() As ArrayList
        Dim dtgItem As DataGridItem
        arr = New ArrayList

        For Each dtgItem In dtgMSPRegistrationList.Items
            Dim chkSelect As CheckBox = dtgItem.FindControl("chkSelect")
            If chkSelect.Checked Then
                Dim idMSPRegistrationHistory As Label = CType(dtgItem.FindControl("lblMSPRegistrationHistoryID"), Label)
                objMSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(CInt(idMSPRegistrationHistory.Text))

                If objMSPRegistrationHistory.Status = EnumStatusMSP.Status.Konfirmasi Then
                    ' set old status to history status
                    oldStatusHistory = objMSPRegistrationHistory.Status
                    objMSPRegistrationHistory.Status = EnumStatusMSP.Status.Validasi
                    arr.Add(objMSPRegistrationHistory)
                End If
            End If
        Next
        Return arr
    End Function

    Protected Sub btnTransfertoSAP_Click(sender As Object, e As EventArgs) Handles btnTransfertoSAP.Click
        ' get all checked data
        arr = New ArrayList
        arr = PopulateTransferData()

        If arr.Count > 0 Then
            If txtPass.Text = String.Empty Then
                'MessageBox.Show("Silahkan Masukkan Password SAP Anda!")
                RegisterStartupScript("OpenWindow", "<script>InputPasswordPlease();</script>")
            Else
                Dim _helper As New MSPHelper()
                ' cek username & password
                If (_helper.CheckConnection(txtUser.Text, txtPass.Text)) Then
                    Dim str As String = _helper.TransfersMSPRegistrationtoSAP(arr, txtUser.Text, txtPass.Text)
                    If str <> String.Empty Then
                        MessageBox.Show(str.Substring(2, str.Length - 2))
                    Else
                        MessageBox.Show("Transfer Berhasil.")
                    End If
                    dtgMSPRegistrationList.CurrentPageIndex = 0
                    BindDatagrid(dtgMSPRegistrationList.CurrentPageIndex)
                Else
                    MessageBox.Show("User atau Password SAP salah.")
                End If

            End If
        Else
            MessageBox.Show("Tidak Ada Registrasi MSP yang ditransfer ke SAP.")
        End If
    End Sub

    'Protected Sub btnTransferUlangtoSAP_Click(sender As Object, e As EventArgs) Handles btnTransferUlangtoSAP.Click
    '    ' get all checked data
    '    arr = New ArrayList
    '    arr = PopulateTransferData()

    '    If arr.Count > 0 Then
    '        Transfers(arr)
    '    Else
    '        MessageBox.Show("Tidak Ada Registrasi MSP yang ditransfer ke SAP.")
    '    End If
    'End Sub

    'Private Function Transfers(al As ArrayList)
    '    If al.Count > 0 Then
    '        Dim sapConStr As String = KTB.DNET.Lib.WebConfig.GetValue("SAPConnectionStrMSP")
    '        Dim oSAPDnet As SAPDNet
    '        Dim SONumber As String = String.Empty, Msg As String = String.Empty
    '        Dim aErrors As New ArrayList
    '        Dim int As Integer = 0

    '        Try
    '            If Me.txtPass.Text = String.Empty Then
    '                'MessageBox.Show("Silahkan Masukkan Password SAP Anda!")
    '                RegisterStartupScript("OpenWindow", "<script>InputPasswordPlease();</script>")
    '                Return False
    '            End If

    '            oSAPDnet = New SAPDNet(sapConStr, txtUser.Text, txtPass.Text)
    '            For i As Integer = 0 To al.Count - 1
    '                objMSPRegistration = CType(al(i), MSPRegistration)
    '                ' load msp reg history untuk ditransfer ke sap
    '                If objMSPRegistration.MSPRegistrationHistorys.Count > 0 Then
    '                    Dim index As Integer = 1
    '                    For Each item As MSPRegistrationHistory In objMSPRegistration.MSPRegistrationHistorys
    '                        SONumber = String.Empty
    '                        Msg = String.Empty

    '                        oSAPDnet.SendMSPRegistrationViaRFC(item, index, SONumber, Msg)
    '                        If Msg <> String.Empty Then
    '                            aErrors.Add("Error Transfer Registrasi MSP : Nomor rangka " & objMSPRegistration.ChassisMaster.ChassisNumber & ", tipe pengajuan " & CType(item.RequestType, EnumStatusMSP.StatusType).ToString & " & tipe MSP " & item.MSPMaster.MSPType.Description & ". " & Msg)
    '                        Else
    '                            aErrors.Add("Sukses Transfer Registrasi MSP : " & objMSPRegistration.ChassisMaster.ChassisNumber & ", tipe pengajuan " & CType(item.RequestType, EnumStatusMSP.StatusType).ToString & " & tipe MSP " & item.MSPMaster.MSPType.Description & ".")

    '                            ' update MSPRegistrationHitory status to Proses
    '                            item.Status = EnumStatusMSP.Status.Proses
    '                            If New MSPRegistrationHistoryFacade(User).Update(item) = -1 Then
    '                                int += 1
    '                                aErrors.Add("Sukses Transfer Registrasi MSP dan Gagal Update Status menjadi Proses : " & objMSPRegistration.ChassisMaster.ChassisNumber & ", tipe pengajuan " & CType(item.RequestType, EnumStatusMSP.StatusType).ToString & " & tipe MSP " & item.MSPMaster.MSPType.Description & ".")
    '                            Else
    '                                ' add to history status
    '                                Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
    '                                objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.MSP_Registration), item.ID, -1, item.Status)
    '                            End If

    '                            ' insert debit charge /SO
    '                            Dim facMSPDC As New MSPDCFacade(User)
    '                            Dim objMSPDC As New MSPDC
    '                            objMSPDC.DebitChargeNo = SONumber
    '                            objMSPDC.MSPRegistrationHistoryID = item.ID

    '                            facMSPDC.Insert(objMSPDC)
    '                        End If

    '                        index += 1
    '                    Next
    '                End If
    '            Next

    '            If aErrors.Count > 0 Then
    '                Msg = String.Empty
    '                For Each erm As String In aErrors
    '                    Msg += "\n" & erm
    '                Next
    '                MessageBox.Show(Msg.Substring(2, Msg.Length - 2))
    '                Return False
    '            Else
    '                MessageBox.Show("Transfer Berhasil.")

    '                Return True
    '            End If
    '        Catch ex As Exception
    '            MessageBox.Show("Transfer Gagal. " & ex.Message)
    '            Return False
    '        End Try

    '    Else
    '        MessageBox.Show("Tidak ada data registrasi MSP untuk Proses Transfer")
    '        Return False
    '    End If
    'End Function

    Private Function PopulateTransferData() As ArrayList
        Dim dtgItem As DataGridItem
        arr = New ArrayList

        For Each dtgItem In dtgMSPRegistrationList.Items
            Dim chkSelect As CheckBox = dtgItem.FindControl("chkSelect")
            If chkSelect.Checked Then
                Dim idMSPRegistrationHistory As Label = CType(dtgItem.FindControl("lblMSPRegistrationHistoryID"), Label)
                objMSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(CInt(idMSPRegistrationHistory.Text))

                If objMSPRegistrationHistory.Status = EnumStatusMSP.Status.Konfirmasi Then
                    arr.Add(objMSPRegistrationHistory.MSPRegistration)
                End If
            End If
        Next
        Return arr
    End Function

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        ' get all checked data
        arr = New ArrayList
        arr = BindDatagrid()
        'arr = PopulateDownloadData()
        If arr.Count > 0 Then
            DoDownload(arr)
        Else
            MessageBox.Show("Tidak ada data yang dipilih.")
        End If
    End Sub

    Private Function PopulateDownloadData() As ArrayList
        ' get array list registration history
        Dim dtgItem As DataGridItem
        arr = New ArrayList

        For Each dtgItem In dtgMSPRegistrationList.Items
            Dim chkSelect As CheckBox = dtgItem.FindControl("chkSelect")
            If chkSelect.Checked Then
                Dim idMSPRegistrationHistory As Label = CType(dtgItem.FindControl("lblMSPRegistrationHistoryID"), Label)
                objMSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(CInt(idMSPRegistrationHistory.Text))

                arr.Add(objMSPRegistrationHistory)
            End If
        Next
        Return arr
    End Function

    Private Sub DoDownload(ByVal MSPHistoryList As ArrayList)
        Dim sFileName As String
        sFileName = "Daftar MSP [" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & "]"

        '-- Temp file must be a randomly named file!
        Dim MSPData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNET.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNET.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNET.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(MSPData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(MSPData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteMSPData(sw, MSPHistoryList)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls", True)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub WriteMSPData(ByVal sw As StreamWriter, ByVal MSPHistoryData As ArrayList)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        itemLine.Remove(0, itemLine.Length)  '-- Empty line
        itemLine.Append("MSP - DAFTAR MSP")
        sw.WriteLine(itemLine)

        For Each row As MSPRegistration In MSPHistoryData

            '======MSP History Data=======
            If (row.MSPRegistrationHistorys.Count > 0) Then
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(" " & tab)
                sw.WriteLine(itemLine)
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append("NO" & tab)
                itemLine.Append("STATUS" & tab)
                itemLine.Append("DEALER" & tab)
                itemLine.Append("NO MSP" & tab)
                itemLine.Append("NO RANGKA" & tab)
                itemLine.Append("KATEGORI" & tab)
                itemLine.Append("TIPE PENGAJUAN" & tab)
                itemLine.Append("TIPE MSP" & tab)
                itemLine.Append("TANGGAL PENGAJUAN (dd-MM-yyyy)" & tab)

                sw.WriteLine(itemLine.ToString())
                Dim i As Integer = 1
                Try
                    For Each item As MSPRegistrationHistory In row.MSPRegistrationHistorys
                        itemLine.Remove(0, itemLine.Length)
                        itemLine.Append(i.ToString & tab)
                        itemLine.Append(CType(CInt(item.Status), EnumStatusMSP.Status).ToString & tab)
                        itemLine.Append(item.MSPRegistration.Dealer.DealerCode & tab)
                        itemLine.Append(item.MSPRegistration.MSPCode & tab)
                        itemLine.Append(item.MSPRegistration.ChassisMaster.ChassisNumber & tab)
                        itemLine.Append(If(item.BenefitMasterHeaderID = 0, "Paid", "Promo") & tab)
                        itemLine.Append(CType(CInt(item.RequestType), EnumStatusMSP.StatusType).ToString & tab)
                        itemLine.Append(item.MSPMaster.MSPType.Description & tab)
                        itemLine.Append(item.RegistrationDate.ToString("dd-MM-yyyy") & tab)

                        sw.WriteLine(itemLine.ToString())
                        i = i + 1
                    Next
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try

            End If
        Next
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)

    End Sub

    Private Sub dtgMSPRegistrationList_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgMSPRegistrationList.PageIndexChanged
        dtgMSPRegistrationList.SelectedIndex = -1
        dtgMSPRegistrationList.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgMSPRegistrationList.CurrentPageIndex)
    End Sub

    Private Sub dtgMSPRegistrationList_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgMSPRegistrationList.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If

        CreateCriteria()
        BindDatagrid(dtgMSPRegistrationList.CurrentPageIndex)
    End Sub

    Protected Sub ddlCategoryV_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategoryV.SelectedIndexChanged
        ddlVechileModel.Items.Clear()
        ddlVechileType.Items.Clear()
        If ddlCategoryV.SelectedIndex <> 0 Then
            'crt = New CriteriaComposite(New Criteria(GetType(VechileModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'crt.opAnd(New Criteria(GetType(VechileModel), "Category.ID", MatchType.Exact, ddlCategoryV.SelectedValue))

            'ddlVechileModel.DataSource = New VechileModelFacade(User).Retrieve(crt)
            'ddlVechileModel.DataTextField = "Description".ToUpper
            'ddlVechileModel.DataValueField = "ID"
            'ddlVechileModel.DataBind()
            'ddlVechileModel_SelectedIndexChanged(Me, System.EventArgs.Empty)

            CommonFunction.BindVehicleSubCategoryToDDL2(ddlVechileModel, ddlCategoryV.SelectedItem.Text)
            BindddlTipe(ddlCategoryV.SelectedItem.ToString)

            ddlVechileModel.SelectedIndex = 0
        End If

        'ddlVechileModel.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        'ddlVechileModel.SelectedIndex = 0
    End Sub

    Protected Sub ddlVechileModel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVechileModel.SelectedIndexChanged
        ddlVechileType.Items.Clear()
        'If ddlVechileModel.SelectedIndex <> 0 Then
        '    crt = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    crt.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.Exact, ddlVechileModel.SelectedValue))

        '    ddlVechileType.DataSource = New VechileTypeFacade(User).Retrieve(crt)
        '    ddlVechileType.DataTextField = "VechileTypeCode".ToUpper
        '    ddlVechileType.DataValueField = "ID"
        '    ddlVechileType.DataBind()
        'End If
        'ddlVechileType.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        'ddlVechileType.SelectedIndex = 0

        BindddlTipe(ddlCategoryV.SelectedItem.ToString)
        ddlVechileType.DataBind()
        ddlVechileType.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlVechileType.SelectedIndex = 0
    End Sub

End Class