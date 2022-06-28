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
Imports KTB.DNet.SAP
Imports KTB.DNet.UI.Helper
Public Class FrmMSPClaimList
    Inherits System.Web.UI.Page

    Private _view As Boolean = False
    Private _sessHelper As New SessionHelper
    Private _strSessSearch As String = "CRITERIAS"
    Private _strSessMSPClaimList As String = "MSPClaimList"
    Dim crt As CriteriaComposite
    Dim arr As ArrayList
    Dim sortCols As SortCollection
    Private objLoginDealer As Dealer

    Private Sub CheckPrivilege()
        _view = SecurityProvider.Authorize(Context.User, SR.MSPClaimList_view_privilege)
        If Not _view Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=MSP - Daftar Claim")
        End If

        btnSearch.Visible = _view
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        objLoginDealer = CType(_sessHelper.GetSession("DEALER"), Dealer)
        If objLoginDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            txtKodeDealer.Visible = True
            lblSearchDealer.Visible = True
        Else
            lblDealerName.Visible = True
            lblDealerName.Text = objLoginDealer.DealerName
        End If

        If Not IsPostBack Then
            BindDropDown()
            ViewState("CurrentSortColumn") = "CreatedTime"
            ViewState("CurrentSortDirect") = KTB.DNet.Domain.Search.Sort.SortDirection.DESC
        End If
    End Sub

    Private Sub BindDropDown()
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
        lboxStatus.DataSource = New EnumStatusMSP().RetrieveStatusClaim()
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
        If ddlVechileModel.SelectedValue <> "-1" Then
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

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        CreateCriteria()
        dtgMSPClaimList.CurrentPageIndex = 0
        BindDatagrid(dtgMSPClaimList.CurrentPageIndex)
    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        arr = New MSPClaimFacade(User).RetrieveByCriteria(CType(_sessHelper.GetSession(_strSessSearch), CriteriaComposite), indexPage + 1, dtgMSPClaimList.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgMSPClaimList.DataSource = arr
        dtgMSPClaimList.VirtualItemCount = totalRow
        dtgMSPClaimList.DataBind()
    End Sub

    Private Sub CreateCriteria()
        Dim str As String = String.Empty

        crt = New CriteriaComposite(New Criteria(GetType(MSPClaim), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If objLoginDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If txtKodeDealer.Text <> String.Empty Then
                Dim strDealerCodeList() As String = txtKodeDealer.Text.Trim.Split(";")
                Dim newStrDealerCodeList As String
                For i As Integer = 0 To strDealerCodeList.Length - 1
                    'newStrDealerCodeList += ",'" & strDealerCodeList(i) & "'"
                    newStrDealerCodeList += "," & strDealerCodeList(i)
                Next

                'crt.opAnd(New Criteria(GetType(MSPClaim), "Dealer.DealerCode", MatchType.InSet, "(" & newStrDealerCodeList.Substring(1, newStrDealerCodeList.Length - 1) & ")"))

                crt.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPClaim), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))

            End If
        Else
            crt.opAnd(New Criteria(GetType(MSPClaim), "Dealer.ID", MatchType.Exact, objLoginDealer.ID))
        End If


        If txtClaimNo.Text.Trim <> String.Empty Then
            crt.opAnd(New Criteria(GetType(MSPClaim), "ClaimNumber", MatchType.Partial, txtClaimNo.Text))
        End If

        If txtChassisNumber.Text.Trim <> String.Empty Then
            crt.opAnd(New Criteria(GetType(MSPClaim), "MSPRegistrationHistory.MSPRegistration.ChassisMaster.ChassisNumber", MatchType.Partial, txtChassisNumber.Text))
        End If

        If txtMSPNo.Text.Trim <> String.Empty Then
            crt.opAnd(New Criteria(GetType(MSPClaim), "MSPRegistrationHistory.MSPRegistration.MSPCode", MatchType.Partial, txtMSPNo.Text))
        End If

        If ddlMSPType.SelectedIndex <> 0 Then
            crt.opAnd(New Criteria(GetType(MSPClaim), "MSPRegistrationHistory.MSPMaster.MSPType.ID", MatchType.Exact, ddlMSPType.SelectedValue))
        End If

        If txtPackegeKM.Text <> String.Empty Then
            crt.opAnd(New Criteria(GetType(MSPClaim), "MSPRegistrationHistory.MSPMaster.MSPKm", MatchType.Exact, txtPackegeKM.Text))
        End If

        If chkClaimDate.Checked Then
            If DateFrom.Value <> "#12:00:00 AM#" And DateTo.Value <> "#12:00:00 AM#" Then
                crt.opAnd(New Criteria(GetType(MSPClaim), "ClaimDate", MatchType.GreaterOrEqual, Format(DateFrom.Value, "yyyy-MM-dd")))
                crt.opAnd(New Criteria(GetType(MSPClaim), "ClaimDate", MatchType.LesserOrEqual, Format(DateTo.Value, "yyyy-MM-dd")))
            ElseIf DateFrom.Value = "#12:00:00 AM#" Then
                str += "\n" + "Tanggal claim mulai tidak boleh kosong."
            ElseIf DateTo.Value = "#12:00:00 AM#" Then
                str += "\n" + "Tanggal claim sampai tidak boleh kosong."
            End If
        End If

        If lboxStatus.SelectedIndex <> -1 Then
            Dim SelectedStatus As String = GetSelectedItem(lboxStatus)
            crt.opAnd(New Criteria(GetType(MSPClaim), "Status", MatchType.InSet, "(" & SelectedStatus & ")"))
        End If


        If ddlCategoryV.SelectedIndex <> 0 Then
            'Dim str2 As String = "(SELECT ID FROM VechileType WHERE CategoryID = " + ddlCategoryV.SelectedValue + ")"
            'crt.opAnd(New Criteria(GetType(MSPClaim), "ChassisMaster.VechileColor.VechileType.ID", MatchType.InSet, str2))

            If ddlVechileModel.SelectedValue <> "-1" Then
                'str2 = "(SELECT ID FROM VechileType WHERE ModelID = " + ddlVechileModel.SelectedValue + ")"
                'crt.opAnd(New Criteria(GetType(MSPClaim), "ChassisMaster.VechileColor.VechileType.ID", MatchType.InSet, str2))

                'CommonFunction.SetVehicleSubCategoryCriterias(ddlVechileModel, ddlCategoryV.SelectedItem.Text, crt, "MSPClaim")

                Dim strSql2 As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlVechileModel.SelectedValue
                crt.opAnd(New Criteria(GetType(MSPClaim), "ChassisMaster.VechileColor.VechileType.VechileModel.ID", MatchType.InSet, "(" & strSql2 & ")"))

                If ddlVechileType.SelectedIndex <> 0 Then
                    crt.opAnd(New Criteria(GetType(MSPClaim), "ChassisMaster.VechileColor.VechileType.ID", MatchType.Exact, ddlVechileType.SelectedValue))
                End If
            Else
                Dim str2 As String = "(SELECT ID FROM VechileType WHERE CategoryID = " + ddlCategoryV.SelectedValue + ")"
                crt.opAnd(New Criteria(GetType(MSPClaim), "ChassisMaster.VechileColor.VechileType.ID", MatchType.InSet, str2))
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

    Private Sub dtgMSPClaimList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgMSPClaimList.ItemCommand
        If (e.CommandName.ToUpper = "DOWNLOADKUITANSI") Then
            CommandDownload(e, "KUI")
        ElseIf (e.CommandName.ToUpper = "DOWNLOADLETTER") Then
            CommandDownload(e, "LTR")
        End If
    End Sub

    Private Function CommandDownload(ByVal e As DataGridCommandEventArgs, ByVal downloadAs As String)
        Dim path As String = String.Empty
        Dim pathBaseDirectory As String = KTB.DNet.Lib.WebConfig.GetValue("MSPDirectory")
        Dim lblClaimDocumentID As Label = CType(e.Item.FindControl("lblClaimDocumentID"), Label)
        Dim objMSPClaimDoc As MSPClaimDocument = New MSPClaimDocumentFacade(User).Retrieve(CInt(lblClaimDocumentID.Text))

        If downloadAs.ToUpper = "KUI" Then
            path = pathBaseDirectory & "\" & objMSPClaimDoc.FileNameKuitansi
        ElseIf downloadAs.ToUpper = "LTR" Then
            path = pathBaseDirectory & "\" & objMSPClaimDoc.FileNameLetter
        End If

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Try
            success = imp.Start()
            If success Then
                Dim fileInfo As New FileInfo(Path)
                If (fileInfo.Exists) Then
                    Response.Redirect("../Download.aspx?file=" & Path)
                    imp.StopImpersonate()
                    imp = Nothing
                Else
                    MessageBox.Show(SR.FileNotFound(fileInfo.Name))
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Download file tidak berhasil.")
        End Try
        

    End Function


    Private Sub dtgMSPClaimList_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMSPClaimList.ItemDataBound
        ' set lblNo
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgMSPClaimList.CurrentPageIndex * dtgMSPClaimList.PageSize)
        End If

        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            Dim rowValue As MSPClaim = CType(e.Item.DataItem, MSPClaim)
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Then
                ' set Status
                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                If Not IsNothing(lblStatus) Then
                    lblStatus.Text = CType(rowValue.Status, EnumStatusMSP.Status).ToString
                End If

                ' lblClaimNo
                Dim lblClaimNo As Label = CType(e.Item.FindControl("lblClaimNo"), Label)
                If Not IsNothing(lblClaimNo) Then
                    lblClaimNo.Text = rowValue.ClaimNumber
                End If

                ' lblDealerCode
                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                If Not IsNothing(lblDealerCode) Then
                    lblDealerCode.Text = rowValue.Dealer.DealerCode
                End If

                ' lblDealerName
                Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
                If Not IsNothing(lblDealerName) Then
                    lblDealerName.Text = rowValue.Dealer.DealerName
                End If

                ' lblChassisNumber
                Dim lblChassisNumber As Label = CType(e.Item.FindControl("lblChassisNumber"), Label)
                If Not IsNothing(lblChassisNumber) Then
                    lblChassisNumber.Text = rowValue.MSPRegistrationHistory.MSPRegistration.ChassisMaster.ChassisNumber
                End If

                ' set No MSP
                Dim lblMSPCode As Label = CType(e.Item.FindControl("lblMSPCode"), Label)
                If Not IsNothing(lblMSPCode) Then
                    lblMSPCode.Text = rowValue.MSPRegistrationHistory.MSPRegistration.MSPCode
                End If

                ' lblMSPType
                Dim lblMSPType As Label = CType(e.Item.FindControl("lblMSPType"), Label)
                If Not IsNothing(lblMSPType) Then
                    lblMSPType.Text = rowValue.MSPRegistrationHistory.MSPMaster.MSPType.Description
                End If

                ' lblActualKM
                Dim lblActualKM As Label = CType(e.Item.FindControl("lblActualKM"), Label)
                If Not IsNothing(lblActualKM) Then
                    lblActualKM.Text = String.Format("{0:#,##0}", Convert.ToDouble(rowValue.StandKM))
                End If

                ' lblPackageKM
                Dim lblPackageKM As Label = CType(e.Item.FindControl("lblPackageKM"), Label)
                If Not IsNothing(lblPackageKM) Then
                    lblPackageKM.Text = String.Format("{0:#,##0}", Convert.ToDouble(rowValue.MSPRegistrationHistory.MSPMaster.MSPKm))
                End If

                ' lblPKTDate
                Dim lblPKTDate As Label = CType(e.Item.FindControl("lblPKTDate"), Label)
                If Not IsNothing(lblPKTDate) Then
                    'lblPKTDate.Text = rowValue.MSPRegistrationHistory.MSPRegistration.ChassisMaster.EndCustomer.HandoverDate.ToString("dd/MM/yyyy")


                    If Not IsNothing(rowValue.ChassisMaster.EndCustomer) Then
                        lblPKTDate.Text = rowValue.ChassisMaster.EndCustomer.OpenFakturDate.ToString("dd/MM/yyyy")
                    End If

                End If



                Dim lblVehicleDescription As Label = e.Item.FindControl("lblVehicleDescription")
                If Not IsNothing(lblVehicleDescription) AndAlso Not IsNothing(rowValue.ChassisMaster.VechileColor) Then
                    lblVehicleDescription.Text = rowValue.ChassisMaster.VechileColor.VechileType.Description
                End If



                ' lblClaimDate
                Dim lblClaimDate As Label = CType(e.Item.FindControl("lblClaimDate"), Label)
                If Not IsNothing(lblClaimDate) Then
                    lblClaimDate.Text = rowValue.ClaimDate.ToString("dd/MM/yyyy")
                End If

                Dim crtClaimDoc As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPClaimDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crtClaimDoc.opAnd(New Criteria(GetType(MSPClaimDocument), "MSPClaim.ID", MatchType.Exact, rowValue.ID))
                Dim arrClaimDoc As ArrayList = New MSPClaimDocumentFacade(User).Retrieve(crtClaimDoc)
                If (arrClaimDoc.Count > 0) Then
                    Dim objClaimDoc As MSPClaimDocument = CType(arrClaimDoc(0), MSPClaimDocument)

                    'lblClaimDocumentID
                    Dim lblClaimDocumentID As Label = CType(e.Item.FindControl("lblClaimDocumentID"), Label)
                    If Not IsNothing(lblClaimDocumentID) Then
                        lblClaimDocumentID.Text = objClaimDoc.ID
                    End If

                    ' lblTotalClaimPPn
                    Dim lblTotalClaimPPn As Label = CType(e.Item.FindControl("lblTotalClaimPPn"), Label)
                    If Not IsNothing(lblTotalClaimPPn) Then
                        lblTotalClaimPPn.Text = objClaimDoc.TotalAmount.ToString("C")
                    End If
                    ' lblNoKwitansi
                    Dim lblNoKwitansi As Label = CType(e.Item.FindControl("lblNoKwitansi"), Label)
                    If Not IsNothing(lblNoKwitansi) Then
                        lblNoKwitansi.Text = objClaimDoc.KuitansiNumber
                    End If
                    '' lblNoLetter
                    'Dim lblNoLetter As Label = CType(e.Item.FindControl("lblNoLetter"), Label)
                    'If Not IsNothing(lblNoLetter) Then
                    '    lblNoLetter.Text = objClaimDoc.LetterNumber
                    'End If

                    Dim lbtnDownloadKuitansi As LinkButton = CType(e.Item.FindControl("lbtnDownloadKuitansi"), LinkButton)
                    lbtnDownloadKuitansi.Visible = True

                    'Dim lbtnDownloadLetter As LinkButton = CType(e.Item.FindControl("lbtnDownloadLetter"), LinkButton)
                    'lbtnDownloadLetter.Visible = True
                End If

                End If
            End If
    End Sub

    Private Sub dtgMSPClaimList_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgMSPClaimList.PageIndexChanged
        dtgMSPClaimList.SelectedIndex = -1
        dtgMSPClaimList.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgMSPClaimList.CurrentPageIndex)
    End Sub

    Private Sub dtgMSPClaimList_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgMSPClaimList.SortCommand
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
        BindDatagrid(dtgMSPClaimList.CurrentPageIndex)
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

        BindddlTipe(ddlCategoryV.SelectedItem.ToString)
        ddlVechileType.DataBind()
        ddlVechileType.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlVechileType.SelectedIndex = 0
    End Sub
End Class