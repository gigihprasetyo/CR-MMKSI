#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Sparepart
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Buletin
Imports System.Security.Principal

#End Region

Public Class FrmFieldFixStockControl
    Inherits System.Web.UI.Page

#Region "Variable"
    Private _sessHelper As SessionHelper = New SessionHelper
    Private bPrivilegeChangesCity As Boolean
    Private arlStockControl As ArrayList
    Private sessCriterias As String = "FrmStockControl.sessCriterias"
    Private _EditTable As Boolean = False

    Dim rilisItem As SparePartForecastMaster

#End Region

#Region "Custom Method"
    Private Sub InitiateAuthorization()
        Dim oD As Dealer = CType(Session("DEALER"), Dealer)

        'If Not SecurityProvider.Authorize(Context.User, SR.FieldFix_StockControl_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=Service - Field Fix Managemen Stock Control")
        'End If

        If oD.Title <> EnumDealerTittle.DealerTittle.KTB Then
            _EditTable = False
        Else
            _EditTable = SecurityProvider.Authorize(Context.User, SR.Recall_InputCategory_Privilege)
        End If

        btnSave.Visible = _EditTable
    End Sub

    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)
        If txtPartNo.Text.Trim().Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastMaster), "SparePartMaster.PartNumber", MatchType.Partial, txtPartNo.Text))
        End If
        If txtRecallRegNo.Text().Trim().Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastMaster), "NoReCallCategory", MatchType.Partial, txtRecallRegNo.Text))
        End If
        If txtNoBuletin.Text().Trim().Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastMaster), "NoBulletinService", MatchType.Partial, txtNoBuletin.Text))
        End If

        If txtStock.Text().Trim().Length > 0 Then
            For i As Integer = Len(txtStock.Text.Trim) - 1 To 1 Step -1
                If Not IsNumeric(txtStock.Text.Trim.Chars(i)) Then
                    MessageBox.Show("Stock harus numerik !")
                    Exit For
                Else
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastMaster), "Stock", MatchType.Exact, txtStock.Text.Trim()))
                End If
            Next


        End If
        If txtMaxOrder.Text().Trim().Length > 0 Then
            For i As Integer = 0 To Len(txtMaxOrder.Text) - 1
                If Not IsNumeric(txtMaxOrder.Text.Trim.Chars(i)) Then
                    MessageBox.Show("Max Order harus numerik !")
                    Exit For
                Else
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastMaster), "MaxOrder", MatchType.Exact, txtMaxOrder.Text.Trim()))
                End If
            Next
        End If

    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim TotalRow As Integer = 0
        If indexPage >= 0 Then
            arlStockControl = New SparePartForecastMasterFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession(Me.sessCriterias), CriteriaComposite), indexPage + 1, dtgStockPart.PageSize, TotalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgStockPart.DataSource = arlStockControl
            dtgStockPart.VirtualItemCount = TotalRow
            dtgStockPart.DataBind()
        End If
    End Sub

    Private Sub ViewStockControl(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objStockControl As SparePartForecastMaster = New SparePartForecastMasterFacade(User).Retrieve(nID)
        If Not objStockControl Is Nothing Then
            _sessHelper.SetSession("vsStockControl", objStockControl)
            txtPartNo.Text = objStockControl.SparePartMaster.PartNumber
            txtPartNo.ReadOnly = False
            txtPartNo.Enabled = False
            lblPartName.Text = objStockControl.SparePartMaster.PartName
            txtRecallRegNo.Text = objStockControl.NoReCallCategory.ToString()
            txtNoBuletin.Text = objStockControl.NoBulletinService
            txtRecallRegNo.ReadOnly = False
            txtRecallRegNo.Enabled = True
            txtNoBuletin.ReadOnly = False
            txtNoBuletin.Enabled = True
            lblSearchPart.Visible = False
            lblRecallRegNo.Visible = True
            lblNoBulletin.Visible = True
            txtMaxOrder.Text = objStockControl.MaxOrder
            txtMaxOrder.ReadOnly = False
            txtMaxOrder.Enabled = True
            Me.btnSave.Enabled = EditStatus
        Else
            MessageBox.Show(SR.ViewFail)
        End If
    End Sub

    Private Sub DeleteStockControl(ByVal nID As Integer)
        Try
            Dim objStockControl As SparePartForecastMaster = New SparePartForecastMasterFacade(User).Retrieve(nID)
            'objCity.RowStatus = DBRowStatus.Deleted
            If Not objStockControl Is Nothing Then

                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastMaster), "ID", MatchType.Exact, nID))

                Dim objFRCM As SparePartForecastMasterFacade = New SparePartForecastMasterFacade(User)
                Dim arr As ArrayList = New ArrayList()
                arr = New SparePartForecastMasterFacade(User).Retrieve(criterias)

                If Not IsNothing(arr) AndAlso arr.Count > 0 Then
                    MessageBox.Show(SR.DeleteFail & ", Data sudah terpakai")
                    Return
                End If
                Dim objStockControlFacade As SparePartForecastMasterFacade = New SparePartForecastMasterFacade(User)
                objStockControlFacade.Delete(objStockControl)
                ClearData()
                dtgStockPart.CurrentPageIndex = 0
                BindDatagrid(dtgStockPart.CurrentPageIndex)
            Else
                MessageBox.Show(SR.DeleteFail)
            End If

        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
        End Try

    End Sub

    Private Function UpdateStockControl() As Integer
        Dim objStockControl As SparePartForecastMaster = CType(Session.Item("vsStockControl"), SparePartForecastMaster)
        objStockControl.Stock = txtStock.Text
        objStockControl.MaxOrder = txtMaxOrder.Text

        Dim objStockControlFacade As SparePartForecastMasterFacade = New SparePartForecastMasterFacade(User)
        Try
            Return objStockControlFacade.Update(objStockControl)
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Private Sub ClearData()
        txtPartNo.Text() = String.Empty
        txtPartNo.Enabled = True
        txtPartNo.ReadOnly = False
        lblSearchPart.Visible = True
        lblPartName.Text() = String.Empty
        lblPartName.Enabled = True
        txtRecallRegNo.Text() = String.Empty
        txtRecallRegNo.Enabled = True
        txtRecallRegNo.ReadOnly = False
        lblRecallRegNo.Visible = True
        txtNoBuletin.Text() = String.Empty
        txtNoBuletin.Enabled = True
        txtNoBuletin.ReadOnly = False
        lblNoBulletin.Visible = True
        txtStock.Text() = String.Empty
        txtMaxOrder.Text() = String.Empty
        txtMaxOrder.Enabled = True
        txtMaxOrder.ReadOnly = False
        btnSave.Enabled = True
        dtgStockPart.SelectedIndex = -1
        btnSearch.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        'lblREgCallNo.Text = "[AUTONUMBER]"
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            ClearData()
            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

            lblSearchPart.Attributes("onclick") = "ShowPPSparePartSelection();"
            lblRecallRegNo.Attributes("onclick") = "ShowPPRecallCategorySelection();"
            lblNoBulletin.Attributes("onclick") = "ShowPPNoBulletinSelection();"

            Search()
        End If
    End Sub

    Private Sub Search()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        CreateCriteria(criterias)
        _sessHelper.SetSession(Me.sessCriterias, criterias)
        dtgStockPart.CurrentPageIndex = 0
        BindDatagrid(dtgStockPart.CurrentPageIndex)
        btnSave.Enabled = True
        txtMaxOrder.ReadOnly = False
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim objSparePartForecastMasterFacade As SparePartForecastMasterFacade = New SparePartForecastMasterFacade(User)
        Dim objSparePartForecastMaster As SparePartForecastMaster = New SparePartForecastMaster
        Dim objSparePartForecastStock As SparePartForecastStock = New SparePartForecastStock

        If CType(ViewState("vsProcess"), String) = "Insert" Then
            InsertObjSparePartForecastMaster(objSparePartForecastMaster)
            dtgStockPart.SelectedIndex = -1
        Else
            Dim objUpdateSparePartForecastMaster As SparePartForecastMaster = CType(Session.Item("vsStockControl"), SparePartForecastMaster)
            UpdateObjSparePartForecastMaster(objUpdateSparePartForecastMaster)
            dtgStockPart.SelectedIndex = -1
        End If

        'ClearData()

    End Sub

    Private Sub InsertObjSparePartForecastMaster(ByVal objSparePartForecastMaster As SparePartForecastMaster)
        Dim objSparePartForecastMasterFacade As SparePartForecastMasterFacade = New SparePartForecastMasterFacade(User)
        Dim objSparePartForecastStock As SparePartForecastStock = New SparePartForecastStock
        Dim objSFStockFacade As SparePartForecastStockFacade = New SparePartForecastStockFacade(User)
        Dim nResult As Integer = -1
        Dim bCheck As Boolean = True

        If bCheck Then
            If txtPartNo.Text.Trim() = "" Then
                MessageBox.Show("Part Number tidak boleh kosong !")
                bCheck = False
                'Return
            End If
        End If

        If bCheck Then
            If txtRecallRegNo.Text.Trim() = "" Then
                MessageBox.Show("No Reg tidak boleh kosong !")
                bCheck = False
                'Return
            End If
        End If

        Dim dtReg As New ArrayList
        dtReg.AddRange(txtRecallRegNo.Text.Split(";".ToCharArray))
        For Each arrReg As String In dtReg
            Dim ctrReg As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            ctrReg.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "RecallRegNo", MatchType.Exact, arrReg))
            Dim RecallCategory As ArrayList = New RecallCategoryFacade(User).Retrieve(ctrReg)
            If RecallCategory.Count = 0 Then
                MessageBox.Show("No Recall Category  " & arrReg & " tidak terdaftar")
                'Return
                bCheck = False
            End If
        Next

        If bCheck Then
            If txtNoBuletin.Text.Trim() = "" Then
                MessageBox.Show("No Service Bulletin tidak boleh kosong !")
                bCheck = False
                'Return
            End If
        End If

        Dim dtBllt As New ArrayList
        dtBllt.AddRange(txtNoBuletin.Text.Split(";".ToCharArray))
        For Each arrBlt As String In dtBllt
            Dim ctrBulletin As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Buletin), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            ctrBulletin.opAnd(New Criteria(GetType(KTB.DNet.Domain.Buletin), "Title", MatchType.Exact, arrBlt))
            Dim Bulletin As ArrayList = New BuletinFacade(User).Retrieve(ctrBulletin)
            If Bulletin.Count = 0 Then
                MessageBox.Show("No Buletin  " & arrBlt & " tidak terdaftar")
                'Return
                bCheck = False
            End If
        Next

        If bCheck Then
            If txtStock.Text.Trim() = "" Or txtStock.Text.Trim() = "0" Then
                MessageBox.Show("Stock tidak boleh kosong atau nol !")
                bCheck = False
                'Return
            End If
        End If

        If bCheck Then
            For i As Integer = Len(txtStock.Text.Trim) - 1 To 1 Step -1
                If Not IsNumeric(txtStock.Text.Trim.Chars(i)) Then
                    bCheck = False
                    MessageBox.Show("Stock harus numerik !")
                    Exit For
                End If
            Next
        End If

        If bCheck Then
            If txtMaxOrder.Text.Trim() = "" Or txtMaxOrder.Text.Trim() = "0" Then
                MessageBox.Show("Max Order tidak boleh kosong atau nol !")
                bCheck = False
                'Return
            End If
        End If

        If bCheck Then
            For i As Integer = 0 To Len(txtMaxOrder.Text) - 1
                If Not IsNumeric(txtMaxOrder.Text.Trim.Chars(i)) Then
                    bCheck = False
                    MessageBox.Show("Max Order harus numerik !")
                    Exit For
                End If
            Next
        End If

        'check parts existing
        Dim objSPFM As SparePartForecastMaster = New SparePartForecastMasterFacade(User).Retrieve(txtPartNo.Text.Trim())
        If objSPFM.ID <> 0 Then
            MessageBox.Show("Part Number sudah ada")
            'Return
            bCheck = False
        End If

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartMaster), "PartNumber", MatchType.Exact, txtPartNo.Text.Trim()))
        Dim SparePart As ArrayList = New SparePartMasterFacade(User).Retrieve(criterias)
        If SparePart.Count > 0 Then
            objSparePartForecastMaster.SparePartMaster = SparePart(0)
        Else
            MessageBox.Show("Part Number tidak terdaftar!")
            'Return
            bCheck = False
        End If

        If bCheck Then
            objSparePartForecastMaster.NoReCallCategory = txtRecallRegNo.Text().Trim
            objSparePartForecastMaster.NoBulletinService = txtNoBuletin.Text().Trim
            objSparePartForecastMaster.Stock = CInt(txtStock.Text)
            objSparePartForecastMaster.MaxOrder = txtMaxOrder.Text().Trim

            nResult = objSparePartForecastMasterFacade.Insert(objSparePartForecastMaster)
        End If

        If nResult <> -1 Then
            Dim ctr As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            ctr.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastMaster), "ID", MatchType.Exact, nResult))
            Dim FMaster As ArrayList = New SparePartForecastMasterFacade(User).Retrieve(ctr)

            objSparePartForecastStock.SparePartForecastMaster = FMaster(0)
            objSparePartForecastStock.Qty = txtStock.Text().Trim
            nResult = objSFStockFacade.Insert(objSparePartForecastStock)
        End If

        If nResult = -1 Then
            'MessageBox.Show("Simpan Gagal")
        Else
            BindDatagrid(dtgStockPart.CurrentPageIndex)
            ClearData()
            MessageBox.Show("Simpan Sukses")
            Session.Add("vsStockControl", objSparePartForecastMaster)
            Dim strScript As String
            strScript = "<script>document.all.txtPartNo.focus();</script>"
            Page.RegisterStartupScript("", strScript)
        End If

    End Sub

    Private Sub UpdateObjSparePartForecastMaster(ByVal ObjSparePartForecastMaster As SparePartForecastMaster)
        Dim objSparePartForecastMasterFacade As SparePartForecastMasterFacade = New SparePartForecastMasterFacade(User)
        Dim objSparePartForecastStock As SparePartForecastStock = New SparePartForecastStock
        Dim objSFStockFacade As SparePartForecastStockFacade = New SparePartForecastStockFacade(User)
        Dim nResult As Integer = -1
        Dim bCheck As Boolean = True

        If bCheck Then
            If txtRecallRegNo.Text.Trim() = "" Then
                MessageBox.Show("No Reg tidak boleh kosong !")
                bCheck = False
                'Return
            End If
        End If

        Dim dtReg As New ArrayList
        dtReg.AddRange(txtRecallRegNo.Text.Split(";".ToCharArray))
        For Each arrReg As String In dtReg
            Dim ctrReg As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            ctrReg.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "RecallRegNo", MatchType.Exact, arrReg))
            Dim RecallCategory As ArrayList = New RecallCategoryFacade(User).Retrieve(ctrReg)
            If RecallCategory.Count = 0 Then
                MessageBox.Show("No Recall Category  " & arrReg & " tidak terdaftar")
                'Return
                bCheck = False
            End If
        Next

        If bCheck Then
            If txtNoBuletin.Text.Trim() = "" Then
                MessageBox.Show("No Service Bulletin tidak boleh kosong !")
                bCheck = False
                'Return
            End If
        End If

        Dim dtBllt As New ArrayList
        dtBllt.AddRange(txtNoBuletin.Text.Split(";".ToCharArray))
        For Each arrBlt As String In dtBllt
            Dim ctrBulletin As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Buletin), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            ctrBulletin.opAnd(New Criteria(GetType(KTB.DNet.Domain.Buletin), "Title", MatchType.Exact, arrBlt))
            Dim Bulletin As ArrayList = New BuletinFacade(User).Retrieve(ctrBulletin)
            If Bulletin.Count = 0 Then
                MessageBox.Show("No Buletin  " & arrBlt & " tidak terdaftar")
                'Return
                bCheck = False
            End If
        Next
        
        If bCheck Then
            If txtStock.Text.Trim() = "" Then
                txtStock.Text = 0
            Else
                For i As Integer = Len(txtStock.Text.Trim) - 1 To 1 Step -1
                    If Not IsNumeric(txtStock.Text.Trim.Chars(i)) Then
                        bCheck = False
                        MessageBox.Show("Stock harus numerik !")
                        Exit For
                    End If
                Next
            End If
        End If

        If bCheck Then
            If ObjSparePartForecastMaster.Stock + CInt(txtStock.Text) < 0 Then
                MessageBox.Show("Jumlah Stock tidak boleh minus !")
                bCheck = False
                'Return
            End If
        End If

        If bCheck Then
            If txtMaxOrder.Text.Trim() = "" Or txtMaxOrder.Text.Trim() = "0" Then
                MessageBox.Show("Max Order tidak boleh kosong !")
                bCheck = False
                'Return
            End If
        End If

        If bCheck Then
            For i As Integer = 0 To Len(txtMaxOrder.Text) - 1
                If Not IsNumeric(txtMaxOrder.Text.Trim.Chars(i)) Then
                    bCheck = False
                    MessageBox.Show("Max Order harus numerik !")
                    Exit For
                End If
            Next
        End If

        If bCheck Then
            ObjSparePartForecastMaster.NoBulletinService = txtNoBuletin.Text().Trim
            ObjSparePartForecastMaster.NoReCallCategory = txtRecallRegNo.Text().Trim
            ObjSparePartForecastMaster.Stock = ObjSparePartForecastMaster.Stock + CInt(txtStock.Text)
            ObjSparePartForecastMaster.MaxOrder = txtMaxOrder.Text().Trim
            nResult = objSparePartForecastMasterFacade.Update(ObjSparePartForecastMaster)
        End If


        If nResult <> -1 Then
            Dim ctr As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            ctr.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastMaster), "ID", MatchType.Exact, ObjSparePartForecastMaster.ID))
            Dim FMaster As ArrayList = New SparePartForecastMasterFacade(User).Retrieve(ctr)

            objSparePartForecastStock.SparePartForecastMaster = FMaster(0)
            objSparePartForecastStock.Qty = txtStock.Text().Trim
            nResult = objSFStockFacade.Insert(objSparePartForecastStock)
        End If

        If nResult = -1 Then
            'MessageBox.Show("Update Gagal")
        Else
            BindDatagrid(dtgStockPart.CurrentPageIndex)
            ClearData()
            MessageBox.Show("Update Sukses")
            Dim strScript As String
            strScript = "<script>document.all.txtPartNo.focus();</script>"
            Page.RegisterStartupScript("", strScript)
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Search()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearData()
        dtgStockPart.DataSource = New ArrayList
        dtgStockPart.DataBind()
    End Sub

    Private Sub dtgStockPart_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgStockPart.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            dtgStockPart.SelectedIndex = e.Item.ItemIndex
            ViewStockControl(e.Item.Cells(0).Text, False)
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewStockControl(e.Item.Cells(0).Text, True)
            btnSearch.Enabled = False
            dtgStockPart.SelectedIndex = e.Item.ItemIndex
        ElseIf e.CommandName = "Delete" Then
            Try
                DeleteStockControl(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
        End If
    End Sub

    Private Sub dtgStockPart_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgStockPart.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As SparePartForecastMaster = CType(e.Item.DataItem, SparePartForecastMaster)
            rilisItem = RowValue
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblInitialStock As Label = CType(e.Item.FindControl("lblInitialStock"), Label)
            Dim lblSendQty As Label = CType(e.Item.FindControl("lblSendQty"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dtgStockPart.CurrentPageIndex * dtgStockPart.PageSize) 'getDataGridItemIndex()

            Dim ctrS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            ctrS.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastStock), "SparePartForecastMaster.ID", MatchType.Exact, rilisItem.ID))
            Dim FStock As ArrayList = New SparePartForecastStockFacade(User).Retrieve(ctrS)
            If FStock.Count > 0 Then
                Dim appStock As Integer
                For Each FS As SparePartForecastStock In FStock
                    appStock += FS.Qty
                Next
                lblInitialStock.Text = appStock
            End If

            Dim ctr As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            ctr.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastDetail), "SparePartForecastMaster.ID", MatchType.Exact, rilisItem.ID))
            Dim FDetail As ArrayList = New SparePartForecastDetailFacade(User).Retrieve(ctr)
            If FDetail.Count > 0 Then
                Dim appQty As Integer
                For Each FD As SparePartForecastDetail In FDetail
                    appQty += FD.ApprovedQty
                Next
                lblSendQty.Text = appQty
            End If
            'privilege CRUD
            'If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            '    e.Item.FindControl("lbtnDelete").Visible = m_bFreeServiceDataUpdate_Privilege
            '    Dim strMsg As String = "Yakin Data dengan nomor rangka " & CType(e.Item.FindControl("lblChassisNo"), Label).Text & "  ini akan dihapus? "
            '    CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & strMsg & "');")
            'End If

            'If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
            '    e.Item.FindControl("lbtnEdit").Visible = m_bFreeServiceDataUpdate_Privilege
            'End If

        End If
    End Sub

    Private Sub dtgStockPart_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgStockPart.PageIndexChanged
        dtgStockPart.SelectedIndex = -1
        dtgStockPart.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgStockPart.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgStockPart_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgStockPart.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        End If

        dtgStockPart.SelectedIndex = -1
        dtgStockPart.CurrentPageIndex = 0
        BindDatagrid(dtgStockPart.CurrentPageIndex)
        ClearData()
    End Sub
End Class