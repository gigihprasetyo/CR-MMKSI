#Region "Custom Namespace Imports"
Imports KTB.DNet.Parser
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General

#End Region

Public Class frmSalesPlanMeeting
    Inherits System.Web.UI.Page



#Region "Custom Variable Declaration"
    Private SalesPlanMeetingArrayList As ArrayList
    Private sessionHelper As New SessionHelper

    Private currentPageMode As PageMode = PageMode.NONE

    Private Enum PageMode
        EDIT_MODE
        SAVE_MODE
        NONE
    End Enum
#End Region

#Region "Custom Method"



    Private Sub BindToGrid()
        SalesPlanMeetingArrayList = sessionHelper.GetSession("SalesPlanMeeting")
        If Not ((SalesPlanMeetingArrayList Is Nothing) OrElse (SalesPlanMeetingArrayList.Count <= 0)) Then
            dtgSalesPlanMeeting.DataSource = SalesPlanMeetingArrayList
            dtgSalesPlanMeeting.DataBind()
            For Each item As SalesPlanMeeting In SalesPlanMeetingArrayList
                If item.ErrorMessage <> String.Empty Then
                    btnSimpan.Enabled = False
                    btnBatal.Enabled = False
                    Exit Sub
                End If
            Next
        Else
            btnSimpan.Enabled = False
            btnBatal.Enabled = False
        End If
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        'Put user code to initialize the page here
        ActivateUserPrivilege()
        If Not IsPostBack Then

            FillCategory()
            ViewState("CurrentSortColumn") = "DateTime"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

            '--load all valid data no past
            BindData(True)
        End If
    End Sub





    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.Sales_Plan_Meeting_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Sales Plan Meeting")
        End If

    End Sub

    Private Sub HapusRow(source As Object, e As DataGridCommandEventArgs)
        SalesPlanMeetingArrayList = sessionHelper.GetSession("SalesPlanMeeting")
        Try
            Dim _SalesPlanMeeting As SalesPlanMeeting = CType(SalesPlanMeetingArrayList(e.Item.ItemIndex), SalesPlanMeeting) ' CType(e.Item.DataItem, SalesPlanMeeting)
            Dim _SalesPlanMeetingFacade As SalesPlanMeetingFacade = New SalesPlanMeetingFacade(User)
            _SalesPlanMeetingFacade.Delete(_SalesPlanMeeting)
            'SalesPlanMeetingArrayList = sessionHelper.GetSession("SalesPlanMeeting")
            'SalesPlanMeetingArrayList.Remove(_SalesPlanMeeting)
            'sessionHelper.SetSession("SalesPlanMeeting", SalesPlanMeetingArrayList)
            'BindToGrid()
            BindData(True)
            MessageBox.Show(SR.DeleteSucces())
        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail())
        End Try
    End Sub

    'Sub dtgSalesPlanMeeting_DeleteCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgSalesPlanMeeting.DeleteCommand
    '    Try
    '        Dim _SalesPlanMeeting As SalesPlanMeeting = CType(e.Item.DataItem, SalesPlanMeeting)
    '        Dim _SalesPlanMeetingFacade As SalesPlanMeetingFacade = New SalesPlanMeetingFacade(User)
    '        _SalesPlanMeetingFacade.Delete(_SalesPlanMeeting)
    '        SalesPlanMeetingArrayList = sessionHelper.GetSession("SalesPlanMeeting")
    '        SalesPlanMeetingArrayList.Remove(_SalesPlanMeeting)
    '        sessionHelper.SetSession("SalesPlanMeeting", SalesPlanMeetingArrayList)
    '        BindToGrid()
    '        MessageBox.Show(SR.DeleteSucces())
    '    Catch ex As Exception
    '        MessageBox.Show(SR.DeleteFail())
    '    End Try
    'End Sub

    Private Sub RubahRow(source As Object, e As DataGridCommandEventArgs)
        SalesPlanMeetingArrayList = sessionHelper.GetSession("SalesPlanMeeting")
        Dim lbl1 As Label = e.Item.Cells(0).FindControl("lblNo")

        Dim _SalesPlanMeeting As SalesPlanMeeting = CType(SalesPlanMeetingArrayList(e.Item.ItemIndex), SalesPlanMeeting) '  CType(SalesPlanMeetingArrayList.Item(CType(lbl1.Text, Integer) - 1), SalesPlanMeeting)
        If (Not IsNothing(_SalesPlanMeeting)) Then
            currentPageMode = PageMode.EDIT_MODE
            sessionHelper.SetSession("currentPageMode", currentPageMode)

            ccMeetingDate.Value = _SalesPlanMeeting.DateTime
            txtDescription.Text = _SalesPlanMeeting.Description


            btnSimpan.Enabled = True
            btnBatal.Enabled = True
            ddlCategory.SelectedValue = _SalesPlanMeeting.Category.ID
            dtgSalesPlanMeeting.EditItemIndex = e.Item.ItemIndex


        End If
    End Sub

    'Sub dtgSalesPlanMeeting_EditCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgSalesPlanMeeting.EditCommand
    '    SalesPlanMeetingArrayList = sessionHelper.GetSession("SalesPlanMeeting")
    '    Dim lbl1 As Label = e.Item.Cells(0).FindControl("lblNo")

    '    Dim _SalesPlanMeeting As SalesPlanMeeting = CType(SalesPlanMeetingArrayList.Item(CType(lbl1.Text, Integer) - 1), SalesPlanMeeting)
    '    If (Not IsNothing(_SalesPlanMeeting)) Then
    '        currentPageMode = PageMode.EDIT_MODE
    '        sessionHelper.SetSession("currentPageMode", currentPageMode)

    '        ccMeetingDate.Value = _SalesPlanMeeting.DateTime
    '        txtDescription.Text = _SalesPlanMeeting.Description


    '        btnSimpan.Enabled = True
    '        btnBatal.Enabled = True
    '        ddlCategory.SelectedValue = _SalesPlanMeeting.Category.ID
    '        dtgSalesPlanMeeting.EditItemIndex = e.Item.ItemIndex


    '    End If
    'End Sub

    Sub dtgSalesPlanMeeting_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgSalesPlanMeeting.ItemCommand
        Select Case (CType(e.CommandSource, LinkButton)).CommandName

            Case "Hapus"
                HapusRow(source, e)
                'dtgSalesPlanMeeting_DeleteCommand(source, e)

            Case "Rubah"
                RubahRow(source, e)
                'dtgSalesPlanMeeting_EditCommand(source, e)

            Case Else
                ' Do nothing.

        End Select

    End Sub

    Private Sub dtgSalesPlanMeeting_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSalesPlanMeeting.SortCommand
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

        BindData()
    End Sub


    Sub dtgSalesPlanMeeting_ItemDataBound(ByVal Sender As Object, ByVal E As DataGridItemEventArgs) Handles dtgSalesPlanMeeting.ItemDataBound
        SalesPlanMeetingArrayList = sessionHelper.GetSession("SalesPlanMeeting")
        If Not (SalesPlanMeetingArrayList.Count = 0 Or E.Item.ItemIndex = -1) Then
            Dim lblNo As Label = CType(E.Item.FindControl("lblNo"), Label)
            Dim objSalesPlanMeeting As SalesPlanMeeting = SalesPlanMeetingArrayList(E.Item.ItemIndex)
            Dim lbtnEdit As LinkButton = CType(E.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnDelete As LinkButton = CType(E.Item.FindControl("lbtnDelete"), LinkButton)
            '--in case the controls not found
            If IsNothing(lblNo) Or IsNothing(objSalesPlanMeeting) Or IsNothing(lbtnEdit) Or IsNothing(lbtnDelete) Then
                Exit Sub
            End If
            lbtnDelete.Text = "<img src=""../images/trash.gif"" border=""0"" alt=""Hapus"">"
            lbtnEdit.Text = "<img src=""../images/edit.gif"" border=""0"" alt=""Edit"">"
            lblNo.Text = (E.Item.ItemIndex + 1 + (dtgSalesPlanMeeting.PageSize * dtgSalesPlanMeeting.CurrentPageIndex)).ToString


            If Date.Now >= objSalesPlanMeeting.DateTime OrElse (btnSimpan.Enabled AndAlso btnCari.Enabled = False) Then
                lbtnEdit.Visible = False
                lbtnEdit.Enabled = False
                lbtnDelete.Visible = False
                lbtnDelete.Enabled = False
            Else
                lbtnEdit.Visible = True
                lbtnEdit.Enabled = True
                lbtnDelete.Visible = True
                lbtnDelete.Enabled = True
            End If
        End If


    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim errorList As StringBuilder = New StringBuilder
        SalesPlanMeetingArrayList = sessionHelper.GetSession("SalesPlanMeeting")
        currentPageMode = sessionHelper.GetSession("currentPageMode")


        If ccMeetingDate.Value < DateSerial(Now.Year, Now.Month, Now.Day).AddDays(1) Then
            MessageBox.Show(SR.SaveFail & ". Minimal Input tanggal Sales Plan Meeting adalah H+1 (Besok)")
            Exit Sub
            'donas 20151209
            'utk editing, misal : sebelumnya tgl 3 Dec, 
            'ternyata tgl 3 tidak jadi meeting, 
            'waktu tanggal 4, dg suatu alasan, ktb memutuskan S.P.M diundur ke tgl 5 Dec (besok),
            'maka tetap diizinkan, asalah tgl adalah >=besok
        End If

        If (currentPageMode = PageMode.SAVE_MODE) Then

            Dim _SalesPlanMeeting As New SalesPlanMeeting
            Dim objSalesPlanMeetingFacade As SalesPlanMeetingFacade = New SalesPlanMeetingFacade(User)
            _SalesPlanMeeting.RowStatus = DBRowStatus.Active
            Try
                If (ddlCategory.SelectedValue = -1) Then
                    Throw New Exception
                End If
                Dim _category As Category = New CategoryFacade(User).Retrieve(CType(ddlCategory.SelectedValue, Integer))
                If Me.IsAlreadyExist(ccMeetingDate.Value, _category.ID, -1) Then
                    MessageBox.Show(SR.SaveFail & " : Data untuk periode tersebut sudah ada")
                    Exit Sub
                End If
                'Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SalesPlanMeeting), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                'If (ddlCategory.SelectedValue <> -1) Then
                '    criterias.opAnd(New Criteria(GetType(SalesPlanMeeting), "Category.ID", MatchType.Exact, CType(ddlCategory.SelectedValue, Integer)))
                'End If

                'Dim startdate As Date = New Date(ccMeetingDate.Value.Year, ccMeetingDate.Value.Month, 1)
                'Dim enddate As Date = startdate.AddMonths(1) ' New Date(ccMeetingDate.Value.Year, ccMeetingDate.Value.Month, Date.DaysInMonth(ccMeetingDate.Value.Year, ccMeetingDate.Value.Month))
                'criterias.opAnd(New Criteria(GetType(SalesPlanMeeting), "DateTime", MatchType.GreaterOrEqual, startdate))
                'criterias.opAnd(New Criteria(GetType(SalesPlanMeeting), "DateTime", MatchType.Lesser, enddate))

                'SalesPlanMeetingArrayList = New SalesPlanMeetingFacade(User).Retrieve(criterias, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

                'If Not IsNothing(SalesPlanMeetingArrayList) AndAlso SalesPlanMeetingArrayList.Count > 0 Then

                '    MessageBox.Show(SR.SaveFail & " : Data untuk periode tersebut sudah ada")
                '    Exit Sub
                'End If

                _SalesPlanMeeting.DateTime = ccMeetingDate.Value
                _SalesPlanMeeting.Description = txtDescription.Text.ToString
                _SalesPlanMeeting.Category = _category


                objSalesPlanMeetingFacade.Insert(_SalesPlanMeeting)
            Catch ex As Exception
                errorList.Append("Insert failed Category: #" & _SalesPlanMeeting.Category.Description & "#")
            End Try

        ElseIf currentPageMode = PageMode.EDIT_MODE Then  'from edit
            Dim objSalesPlanMeetingFacade As SalesPlanMeetingFacade = New SalesPlanMeetingFacade(User)

            'Dim lbl1 As Label = dtgSalesPlanMeeting.Items.Item(dtgSalesPlanMeeting.EditItemIndex).Cells(0).FindControl("lblNo")

            Dim _SalesPlanMeeting As SalesPlanMeeting = CType(SalesPlanMeetingArrayList.Item(dtgSalesPlanMeeting.EditItemIndex), SalesPlanMeeting)

            Try
                Dim _category As Category = New CategoryFacade(User).Retrieve(CType(ddlCategory.SelectedValue, Integer))

                If Me.IsAlreadyExist(ccMeetingDate.Value, _category.ID, _SalesPlanMeeting.ID) Then
                    MessageBox.Show(SR.SaveFail & " : Data untuk periode tersebut sudah ada")
                    Exit Sub
                End If

                _SalesPlanMeeting.DateTime = ccMeetingDate.Value
                _SalesPlanMeeting.Description = txtDescription.Text.ToString
                _SalesPlanMeeting.Category = _category
                objSalesPlanMeetingFacade.Update(_SalesPlanMeeting)
            Catch ex As Exception
                errorList.Append("Update failed ID: #" & _SalesPlanMeeting.ID & "#")
            End Try
            dtgSalesPlanMeeting.EditItemIndex = -1
        End If
        If errorList.Length > 0 Then
            MessageBox.Show(SR.SaveFail & " : " & errorList.ToString)
        Else
            ClearForm()
            BindData(True)
            MessageBox.Show(SR.SaveSuccess)
        End If
    End Sub

    Private Function IsAlreadyExist(MonthPeriod As Date, CategorId As Integer, DataID As Integer) As Boolean
        Dim oSPMFac As New SalesPlanMeetingFacade(User)
        Dim cSPM As New CriteriaComposite(New Criteria(GetType(SalesPlanMeeting), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim startD As Date = DateSerial(MonthPeriod.Year, MonthPeriod.Month, 1)
        Dim endD As Date = startD.AddMonths(1)
        Dim aSPMs As New ArrayList

        cSPM.opAnd(New Criteria(GetType(SalesPlanMeeting), "Category.ID", MatchType.Exact, CategorId))
        cSPM.opAnd(New Criteria(GetType(SalesPlanMeeting), "DateTime", MatchType.GreaterOrEqual, startD))
        cSPM.opAnd(New Criteria(GetType(SalesPlanMeeting), "DateTime", MatchType.Lesser, endD))
        cSPM.opAnd(New Criteria(GetType(SalesPlanMeeting), "ID", MatchType.No, DataID))
        
        aSPMs = oSPMFac.Retrieve(cSPM)
        Return (aSPMs.Count > 0)
    End Function

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        'btnSimpan.Enabled = False
        '--set to none page mode
        dtgSalesPlanMeeting.AllowSorting = True
        btnSimpan.Enabled = False
        btnBatal.Enabled = False
        currentPageMode = PageMode.NONE
        sessionHelper.SetSession("currentPageMode", currentPageMode)
        dtgSalesPlanMeeting.EditItemIndex = -1

        '--remove all datas
        dtgSalesPlanMeeting.DataSource = Nothing
        dtgSalesPlanMeeting.DataBind()

        sessionHelper.RemoveSession("SalesPlanMeeting")

        '--start search query and bind new data to grid
        BindData()

    End Sub

    Private Sub BindData(Optional noPast As Boolean = False)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SalesPlanMeeting), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If (ddlCategory.SelectedValue <> -1) Then
            criterias.opAnd(New Criteria(GetType(SalesPlanMeeting), "Category.ID", MatchType.Exact, CType(ddlCategory.SelectedValue, Integer)))
        End If
        If noPast Then
            criterias.opAnd(New Criteria(GetType(SalesPlanMeeting), "DateTime", MatchType.GreaterOrEqual, Date.Now))
        

        End If

        SalesPlanMeetingArrayList = New SalesPlanMeetingFacade(User).Retrieve(criterias, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        sessionHelper.SetSession("SalesPlanMeeting", SalesPlanMeetingArrayList)
        If SalesPlanMeetingArrayList.Count > 0 Then
            BindToGrid()
        Else
            dtgSalesPlanMeeting.DataSource = SalesPlanMeetingArrayList
            dtgSalesPlanMeeting.DataBind()
            MessageBox.Show("Data Sales Plan Meeting Tidak Ditemukan")
        End If

    End Sub



    Private Function SalesPlanMeetingTransferData() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim oExArgs As New System.Collections.ArrayList
        Dim objSalesPlanMeeting As New SalesPlanMeetingFacade(User)

        For Each oDataGridItem In dtgSalesPlanMeeting.Items
            Dim _SalesPlanMeeting As New KTB.DNet.Domain.SalesPlanMeeting
            _SalesPlanMeeting.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
            _SalesPlanMeeting = objSalesPlanMeeting.Retrieve(_SalesPlanMeeting.ID)
            oExArgs.Add(_SalesPlanMeeting)
        Next
        Return oExArgs
    End Function

    Private Sub FillCategory()
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")

        ddlCategory.Items.Clear()
        Dim objCategory As New CategoryFacade(User)
        ddlCategory.DataSource = objCategory.RetrieveActiveList(companyCode)
        ddlCategory.DataTextField = "Description"
        ddlCategory.DataValueField = "ID"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub


#End Region

    
    Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        If (sessionHelper.GetSession("currentPageMode") IsNot Nothing) Then
            currentPageMode = sessionHelper.GetSession("currentPageMode")

        End If
        If (ddlCategory.SelectedValue = -1) Then
            If (currentPageMode <> PageMode.EDIT_MODE) Then
                currentPageMode = PageMode.NONE
                sessionHelper.SetSession("currentPageMode", currentPageMode)

            End If

            btnSimpan.Enabled = False
            btnBatal.Enabled = False
        Else
            If (currentPageMode <> PageMode.EDIT_MODE) Then
                currentPageMode = PageMode.SAVE_MODE
                sessionHelper.SetSession("currentPageMode", currentPageMode)
            End If
            btnSimpan.Enabled = True
            btnBatal.Enabled = True
        End If

    End Sub
    Private Sub ClearForm()
        ccMeetingDate.Value = Now
        txtDescription.Text = String.Empty
        ddlCategory.SelectedValue = -1

        btnSimpan.Enabled = False
        btnBatal.Enabled = False

        btnCari.Enabled = True

        If (sessionHelper.GetSession("currentPageMode") IsNot Nothing) Then
            currentPageMode = sessionHelper.GetSession("currentPageMode")

        End If
        If (currentPageMode <> PageMode.EDIT_MODE) Then
            sessionHelper.RemoveSession("SalesPlanMeeting")
            dtgSalesPlanMeeting.DataSource = Nothing
            dtgSalesPlanMeeting.DataBind()
            SalesPlanMeetingArrayList = Nothing
        End If


        currentPageMode = PageMode.NONE
        sessionHelper.RemoveSession("currentPageMode")
    End Sub
    Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        ClearForm()
    End Sub
End Class