Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit

Public Class FrmSettingEventCategory
    Inherits System.Web.UI.Page

    Private objDealer As Dealer
    Private objUserInfo As UserInfo
    Private sessHelper As New SessionHelper
    Dim SessionG1 As String = "EventCategoryGrid1"
    Dim SessionG2 As String = "EventCategoryGrid2"
    Dim DelSessionG1 As String = "DelEventCategoryGrid1"
    Dim DelSessionG2 As String = "DelEventCategoryGrid2"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objDealer = CType(Session("DEALER"), Dealer)
        objUserInfo = CType(Session("LOGINUSERINFO"), UserInfo)

        If Not IsPostBack Then
            Page_Init()
        End If
        Privilege_Auth()
    End Sub

    Private Sub Page_Init()
        ClearAll()
        sessHelper.SetSession(SessionG1, New ArrayList)
        sessHelper.SetSession(SessionG2, New ArrayList)
        sessHelper.SetSession(DelSessionG1, New ArrayList)
        sessHelper.SetSession(DelSessionG2, New ArrayList)
        bindDG1()
        bindDG2()
        bindDG3()
        lnkBtnPopUpDealer.Attributes("onclick") = "ShowPopUpDealerCategory()"
    End Sub

    Private Sub Privilege_Auth()
        If Not SecurityProvider.Authorize(Context.User, SR.EVENT_Input_Event_Category_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT - SETTING EVENT CATEGORY")
        End If
    End Sub

    Private Sub ClearAll()
        txtCategoryName.Text = ""
        ddlStatus.SelectedIndex = 0
        If Not IsNothing(ViewState("cmd")) Then
            ViewState("cmd") = Nothing
        End If
        If Not IsNothing(ViewState("header")) Then
            ViewState("header") = Nothing
        End If
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If btnSimpan.Text = "Kembali" Then
            Page_Init()
            DisableControl(True)
            btnSimpan.Text = "Simpan"
            Exit Sub
        End If
        Dim objCategoryHeader As New EventCategoryHeader
        objCategoryHeader.CategoryName = txtCategoryName.Text
        objCategoryHeader.Status = IIf(ddlStatus.SelectedValue = 1, True, False)
        Dim details As ArrayList = getAllDetails()
        Dim objCategoryDetailType0 As ArrayList = sessHelper.GetSession(SessionG1)
        Dim objCategoryDetailType1 As ArrayList = sessHelper.GetSession(SessionG2)
        Dim objDelCategoryDetailType0 As ArrayList = sessHelper.GetSession(DelSessionG1)
        Dim objDelCategoryDetailType1 As ArrayList = sessHelper.GetSession(DelSessionG2)
        Dim _result As Integer = 0
        If details.Count > 0 Then
            If ViewState("cmd") = "edit" Then
                objCategoryHeader = New EventCategoryHeaderFacade(User).Retrieve(CType(ViewState("header"), Integer))
                _result = New EventCategoryDetailFacade(User).UpdateTransaction(objCategoryHeader, objCategoryDetailType0, objDelCategoryDetailType0, objCategoryDetailType1, objDelCategoryDetailType1)
            Else
                _result = New EventCategoryDetailFacade(User).InsertTransaction(objCategoryHeader, details)
            End If
        End If
        If _result > 0 Then
            Page_Init()
            DisableControl(True)
            MessageBox.Show("Simpan Data Berhasil !")
        Else
            MessageBox.Show("Simpan Data Gagal")
        End If
        bindDG3()
    End Sub

    Protected Sub DataGrid1_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        Dim arlDg1 As ArrayList = CType(sessHelper.GetSession(SessionG1), ArrayList)
        Dim index = e.Item.ItemIndex
        Dim oDetail As New EventCategoryDetail
        Select Case e.CommandName
            Case "Add"
                Dim txtCats1 As TextBox = CType(e.Item.FindControl("txtCatsG1"), TextBox)
                If txtCats1.Text <> String.Empty Then
                    oDetail.DocumentName = txtCats1.Text
                    oDetail.DocumentType = 0
                    arlDg1.Add(oDetail)
                Else
                    MessageBox.Show("Isi kolom terlebih dahulu")
                End If
            Case "Delete"
                If IsNothing(ViewState("cmd")) Then
                    arlDg1.RemoveAt(index)
                ElseIf ViewState("cmd") = "edit" Then
                    Dim delArl As EventCategoryDetail = CType(arlDg1(index), EventCategoryDetail)
                    If delArl.ID > 0 Then
                        Dim deletedArrLst As ArrayList
                        deletedArrLst = CType(sessHelper.GetSession(DelSessionG1), ArrayList)
                        deletedArrLst.Add(delArl)
                        sessHelper.SetSession(DelSessionG1, deletedArrLst)
                    End If
                    arlDg1.RemoveAt(index)
                End If

                'arlDg1.RemoveAt(e.Item.ItemIndex)
        End Select
        sessHelper.SetSession(SessionG1, arlDg1)
        bindDG1()
    End Sub

    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        Dim arlDg1 As ArrayList = CType(sessHelper.GetSession(SessionG1), ArrayList)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblFileIklanG1 As Label = CType(e.Item.FindControl("lblFileIklanG1"), Label)
            lblFileIklanG1.Text = CType(arlDg1(e.Item.ItemIndex), EventCategoryDetail).DocumentName
            Dim lnkbtnDeleteParts As LinkButton = CType(e.Item.FindControl("lnkbtnDeleteParts"), LinkButton)
            If ViewState("cmd") = "detail" Then
                lnkbtnDeleteParts.Visible = False
            End If
        End If
    End Sub

    Protected Sub dgFiles_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgFiles.ItemCommand
        Dim arlDg2 As ArrayList = CType(sessHelper.GetSession(SessionG2), ArrayList)
        Dim index = e.Item.ItemIndex
        Dim oDetail As New EventCategoryDetail
        Select Case e.CommandName
            Case "Add"
                Dim txtCats1 As TextBox = CType(e.Item.FindControl("txtCatsG2"), TextBox)
                If txtCats1.Text <> String.Empty Then
                    oDetail.DocumentName = txtCats1.Text
                    oDetail.DocumentType = 1
                    arlDg2.Add(oDetail)
                Else
                    MessageBox.Show("Isi kolom terlebih dahulu")
                End If
            Case "Delete"
                If IsNothing(ViewState("cmd")) Then
                    arlDg2.RemoveAt(index)
                ElseIf ViewState("cmd") = "edit" Then
                    Dim delArl As EventCategoryDetail = CType(arlDg2(index), EventCategoryDetail)
                    If delArl.ID > 0 Then
                        Dim deletedArrLst As ArrayList
                        deletedArrLst = CType(sessHelper.GetSession(DelSessionG2), ArrayList)
                        deletedArrLst.Add(delArl)
                        sessHelper.SetSession(DelSessionG2, deletedArrLst)
                    End If
                    arlDg2.RemoveAt(index)
                End If
                'arlDg2.RemoveAt(e.Item.ItemIndex)
        End Select
        sessHelper.SetSession(SessionG2, arlDg2)
        bindDG2()
    End Sub

    Protected Sub dgFiles_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgFiles.ItemDataBound
        Dim arlDg2 As ArrayList = CType(sessHelper.GetSession(SessionG2), ArrayList)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblFileIklanG2 As Label = CType(e.Item.FindControl("lblFileIklanG2"), Label)
            lblFileIklanG2.Text = CType(arlDg2(e.Item.ItemIndex), EventCategoryDetail).DocumentName
            Dim lnkbtnDeleteParts As LinkButton = CType(e.Item.FindControl("lnkbtnDeleteParts"), LinkButton)
            If ViewState("cmd") = "detail" Then
                lnkbtnDeleteParts.Visible = False
            End If
        End If
    End Sub

    Private Sub bindDG1()
        DataGrid1.DataSource = CType(Session(SessionG1), ArrayList)
        DataGrid1.DataBind()
    End Sub

    Private Sub bindDG2()
        dgFiles.DataSource = CType(Session(SessionG2), ArrayList)
        dgFiles.DataBind()
    End Sub

    Private Sub bindDG3()
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventCategoryHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arl As ArrayList = New EventCategoryHeaderFacade(User).Retrieve(crit)
        If arl.Count > 0 Then
            DataGrid2.DataSource = arl
            DataGrid2.DataBind()
        Else
            DataGrid2.DataSource = New ArrayList
            DataGrid2.DataBind()
        End If
    End Sub

    Private Function getAllDetails() As ArrayList
        Dim objCategoryDetailType0 As ArrayList = sessHelper.GetSession(SessionG1)
        Dim objCategoryDetailType1 As ArrayList = sessHelper.GetSession(SessionG2)
        Dim arl As New ArrayList
        arl.AddRange(objCategoryDetailType0)
        arl.AddRange(objCategoryDetailType1)
        Return arl
    End Function

    Protected Sub DataGrid2_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DataGrid2.ItemCommand
        Select Case e.CommandName
            Case "Detail"
                ViewState("cmd") = "detail"
                LoadData(CInt(e.Item.Cells(0).Text))
                DisableControl(False)
                btnSimpan.Text = "Kembali"
            Case "Edit"
                ViewState("cmd") = "edit"
                LoadData(CInt(e.Item.Cells(0).Text))
                DisableControl(True)
                btnSimpan.Text = "Simpan"
            Case "Delete"
                Dim q As New EventCategoryHeaderFacade(User)
                Dim ech As EventCategoryHeader = q.Retrieve(CInt(e.Item.Cells(0).Text))
                If Not IsNothing(ech) Then
                    q.Delete(ech)
                End If
        End Select
        bindDG3()
    End Sub

    Protected Sub DataGrid2_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles DataGrid2.ItemDataBound
        Dim owscDet As EventCategoryHeader = CType(e.Item.DataItem, EventCategoryHeader)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblCategoryName As Label = CType(e.Item.FindControl("lblCategoryName"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)

            lblNo.Text = e.Item.ItemIndex + 1 + (DataGrid2.CurrentPageIndex * DataGrid2.PageSize)
            lblCategoryName.Text = owscDet.CategoryName
            lblStatus.Text = IIf(owscDet.Status = True, "Aktif", "Tidak Aktif")
        End If

    End Sub

    Private Sub LoadData(ByVal id As Integer)
        Dim oHeader As EventCategoryHeader = New EventCategoryHeaderFacade(User).Retrieve(id)
        ViewState("header") = oHeader.ID
        txtCategoryName.Text = oHeader.CategoryName
        ddlStatus.SelectedValue = IIf(oHeader.Status = True, "1", "0")
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventCategoryDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(EventCategoryDetail), "EventCategoryHeader.ID", MatchType.Exact, oHeader.ID))
        Dim arrDetail As ArrayList = New EventCategoryDetailFacade(User).Retrieve(crit)
        Dim arrDg1 As New ArrayList
        Dim arrDg2 As New ArrayList
        For Each oDetail As EventCategoryDetail In arrDetail
            If oDetail.DocumentType = 0 Then
                arrDg1.Add(oDetail)
            Else
                arrDg2.Add(oDetail)
            End If
        Next
        Session(SessionG1) = arrDg1
        Session(SessionG2) = arrDg2

        bindDG1()
        bindDG2()
    End Sub

    Private Sub DisableControl(ByVal state As Boolean)
        DataGrid1.ShowFooter = state
        dgFiles.ShowFooter = state
        txtCategoryName.Enabled = state
        ddlStatus.Enabled = state
    End Sub

    Protected Sub hdnDealerCode_ValueChanged(sender As Object, e As EventArgs) Handles hdnDealerCode.ValueChanged
        lblECategory.Text = New CategoryFacade(User).Retrieve(CInt(hdnDealerCode.Value.Trim.Split(";")(0))).CategoryCode
        txtDealerCode.Text = hdnDealerCode.Value.Remove(0, 2).Replace(";", ",")
    End Sub
End Class