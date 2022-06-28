#Region "Custom NameSpace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

Public Class frmSparePartPOScheduleManage
    Inherits System.Web.UI.Page

#Region "variable"
    Dim _sessHelper As SessionHelper = New SessionHelper
    Private arlAppConfig As ArrayList
    Private sessCriterias As String = "frmSparePartPOScheduleManage.sessCriterias"
    Private strProsess As String = "VsProses"
    Private arDSSPO As String = "arDSSPO"

    Dim List_priv As Boolean
    Dim Edit_priv As Boolean

    Private Enum Mode
        Edit
        Insert
        View
    End Enum
#End Region

#Region "Custom Method"

    Private Sub CheckPriv()
        Edit_priv = SecurityProvider.Authorize(Context.User, SR.RO_Input_Privilege)

        If Not Edit_priv Then
            If Not SecurityProvider.Authorize(Context.User, SR.RO_List_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Jadwal Daftar Pemesanan")
            End If

        End If
    End Sub
    Private Enum pageMode
        Read
        Edit
        Insert
    End Enum

    Private Sub InitComponent()
        lboxHari.DataSource = Nothing
        lboxHari.Items.Clear()
        Dim aa As New SortedList
        aa = EnumSparePartPOSchedule.RetrieveHari(True)
        aa.Add(-1, "Silahkan Pilih")
        lboxHari.DataSource = aa
        lboxHari.DataTextField = "value"
        lboxHari.DataValueField = "key"
        lboxHari.DataBind()

        lboxOrderType.DataSource = Nothing
        lboxOrderType.Items.Clear()
        lboxOrderType.Items.Insert(0, New ListItem("Others Regular", "Z"))
        lboxOrderType.Items.Insert(0, New ListItem("Regular", "R"))
        lboxOrderType.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        lboxOrderType.DataBind()

        ViewState("CurrentSortColumn") = "ID"
        ViewState("ID") = "0"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        ViewState.Add(Me.strProsess, Mode.Insert)
    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim TotalRow As Integer = 0
        If indexPage >= 0 Then
            Dim arlAppConfig = New SparePartPOScheduleFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession(Me.sessCriterias), CriteriaComposite), indexPage + 1, dtgSparePartPOSchedule.PageSize, TotalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            _sessHelper.SetSession(Me.arDSSPO, arlAppConfig)
            dtgSparePartPOSchedule.DataSource = arlAppConfig
            dtgSparePartPOSchedule.VirtualItemCount = TotalRow
            dtgSparePartPOSchedule.DataBind()
        End If
    End Sub

    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)

        If txtDealerName.Text().Trim().Length > 0 Then
            'zss criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOSchedule), "ID", MatchType.Partial, txtDealerName.Text().Trim()))

            Dim list() As String = txtDealerName.Text.Split(";")
            Dim strDealerID As String = String.Empty
            If list.Length > 0 Then
                For Each item As String In txtDealerName.Text.Split(";")
                    Dim objDealer As Dealer = New DealerFacade(User).Retrieve(item)
                    If Not IsNothing(objDealer) Then
                        If strDealerID = String.Empty Then
                            strDealerID = objDealer.ID
                        Else
                            strDealerID = strDealerID & "," & objDealer.ID
                        End If
                    End If
                Next
            End If
            If strDealerID <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(SparePartPOSchedule), "ID", MatchType.InSet, "(SELECT x.SparePartPOScheduleID FROM dbo.SparePartPOScheduleDealer x   WHERE x.RowStatus=0 AND x.DealerID IN   (" & strDealerID & "))"))
            End If

        End If

        If lboxOrderType.SelectedValue.Length > 0 AndAlso lboxOrderType.SelectedValue.ToString <> "-1" Then

            'Dim strOrderType As String = String.Empty
            'For Each item As ListItem In lboxOrderType.Items
            '    If item.Selected Then
            '        strOrderType &= CType(item.Value, String) & ","
            '    End If
            'Next

            criterias.opAnd(New Criteria(GetType(SparePartPOSchedule), "OrderType", MatchType.Exact, lboxOrderType.SelectedValue))
        End If

        If lboxHari.SelectedValue.Length > 0 AndAlso lboxHari.SelectedValue.ToString <> "-1" Then

            'Dim strHari As String = String.Empty
            'For Each item As ListItem In lboxHari.Items
            '    If item.Selected Then
            '        strHari &= CType(item.Value, String) & ","
            '    End If
            'Next

            criterias.opAnd(New Criteria(GetType(SparePartPOSchedule), "OrderDay", MatchType.Exact, lboxHari.SelectedValue))
        End If

    End Sub

    Private Function Insert(ByRef strMsg As String) As Boolean
        strMsg = SR.SaveSuccess
        lblInfo.Text = String.Empty
        Try
            Dim ObjSPOSchedule As New SparePartPOSchedule
            Dim ObjSPOScheduleDealer As New ArrayList
            ObjSPOSchedule.Status = False
            'Validation

            If lboxOrderType.SelectedValue.Length > 0 AndAlso lboxOrderType.SelectedValue.ToString <> "-1" Then
                ObjSPOSchedule.OrderType = lboxOrderType.SelectedValue
            Else
                strMsg = "Tipe Order harus diisi"
                Return False
            End If

            If lboxHari.SelectedValue.Length > 0 AndAlso lboxHari.SelectedValue.ToString <> "-1" Then
                ObjSPOSchedule.OrderDay = CInt(lboxHari.SelectedValue)
            Else
                strMsg = "hari harus diisi"
                Return False
            End If

            If txtDealerName.Text.Trim() <> "" Then
                Dim objdealers() As String = txtDealerName.Text.Split(";")

                For Each strDealer As String In objdealers
                    Dim objDealerTemp As Dealer = New DealerFacade(User).Retrieve(strDealer)
                    Dim objTSparePartPOScheduleDealer As New SparePartPOScheduleDealer
                    objTSparePartPOScheduleDealer.Dealer = objDealerTemp
                    objTSparePartPOScheduleDealer.SparePartPOSchedule = ObjSPOSchedule
                    ObjSPOScheduleDealer.Add(objTSparePartPOScheduleDealer)
                Next


            Else

                strMsg = "Dealer Wajib di isi"
                Return False
            End If

            Dim FSparePartPOSchedule As New SparePartPOScheduleFacade(User)
            Dim id As Integer = FSparePartPOSchedule.Insert(ObjSPOSchedule, ObjSPOScheduleDealer)
            Dim Objcrit As CriteriaComposite
            Dim strQuery As String = String.Format("SELECT	x.DealerID FROM	dbo.SparePartPOScheduleDealer x INNER JOIN dbo.SparePartPOSchedule y ON x.SparePartPOScheduleID = y.ID   WHERE	x.RowStatus = 0 AND y.RowStatus = 0 AND x.SparePartPOScheduleID <> {0} AND y.OrderType = '{1}' AND x.DealerID IN ( SELECT	z.DealerID FROM	dbo.SparePartPOScheduleDealer z WHERE	z.SparePartPOScheduleID = {2} AND   z.RowStatus=0  )", id.ToString(), ObjSPOSchedule.OrderType, id.ToString())

            Objcrit = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Objcrit.opAnd(New Criteria(GetType(Dealer), "ID", MatchType.InSet, "(" & strQuery & ")"))

            Dim FDealer As New DealerFacade(User)
            Dim arrDealer As New ArrayList
            arrDealer = FDealer.Retrieve(Objcrit)

            If Not IsNothing(arrDealer) AndAlso arrDealer.Count > 0 Then
                strQuery = String.Empty
                For Each objD As Dealer In arrDealer
                    strQuery = strQuery & " - " & objD.DealerCode & vbCrLf
                Next
                lblInfo.Text = "Dealer Sudah terdapat pada jadwal Lain " & vbCrLf & strQuery
            End If

            Return True
        Catch ex As Exception
            strMsg = "Data Gagal di Simpan"
            Return False
        End Try

        Return False
    End Function

    Private Function Edit(ByRef strMsg As String) As Boolean
        strMsg = SR.SaveSuccess

        Try

            Dim ObjSPOSchedule As SparePartPOSchedule = New SparePartPOScheduleFacade(User).Retrieve(CInt(ViewState("ID")))
            Dim ObjSPOScheduleDealer As New ArrayList
            ' ObjSPOSchedule.Status = True
            'Validation

            If lboxOrderType.SelectedValue.Length > 0 AndAlso lboxOrderType.SelectedValue.ToString <> "-1" Then
                ObjSPOSchedule.OrderType = lboxOrderType.SelectedValue
            Else
                strMsg = "Tipe Order harus diisi"
                Return False
            End If

            If lboxHari.SelectedValue.Length > 0 AndAlso lboxHari.SelectedValue.ToString <> "-1" Then
                ObjSPOSchedule.OrderDay = CInt(lboxHari.SelectedValue)
            Else
                strMsg = "hari harus diisi"
                Return False
            End If

            If txtDealerName.Text.Trim() <> "" Then
                Dim objdealers() As String = txtDealerName.Text.Split(";")

                For Each strDealer As String In objdealers
                    Dim objDealerTemp As Dealer = New DealerFacade(User).Retrieve(strDealer)
                    Dim objTSparePartPOScheduleDealer As New SparePartPOScheduleDealer
                    objTSparePartPOScheduleDealer.Dealer = objDealerTemp
                    objTSparePartPOScheduleDealer.SparePartPOSchedule = ObjSPOSchedule
                    ObjSPOScheduleDealer.Add(objTSparePartPOScheduleDealer)
                Next


            Else

                strMsg = "Dealer Wajib di isi"
                Return False
            End If



            Dim FSparePartPOSchedule As New SparePartPOScheduleFacade(User)
            FSparePartPOSchedule.Update(ObjSPOSchedule, ObjSPOSchedule.SparePartPOScheduleDealers, ObjSPOScheduleDealer)


            Return True
        Catch ex As Exception
            strMsg = "Data Gagal di Simpan"
            Return False
        End Try

        Return False
    End Function


    Private Function Delete(ByRef strMsg As String, ByVal ID As Integer) As Boolean
        strMsg = SR.SaveSuccess

        Try

            Dim ObjSPOSchedule As SparePartPOSchedule = New SparePartPOScheduleFacade(User).Retrieve(ID)

            If ObjSPOSchedule.OrderDay = EnumSparePartPOSchedule.GetIntWeek Then
                strMsg = "Tidak bisa hapus dihari sama"
                Return False
            End If

            ObjSPOSchedule.RowStatus = CInt(DBRowStatus.Deleted)

            Dim FSparePartPOSchedule As New SparePartPOScheduleFacade(User)
            FSparePartPOSchedule.Update(ObjSPOSchedule)

            Return True
        Catch ex As Exception
            strMsg = "Data Gagal di Simpan"
            Return False
        End Try

        Return False
    End Function

    Private Function Aktif(ByRef strMsg As String, ByVal Aktifkan As Boolean) As Boolean
        strMsg = SR.SaveSuccess

        Try

            Dim ObjSPOSchedule As SparePartPOSchedule = New SparePartPOScheduleFacade(User).Retrieve(CInt(ViewState("ID")))
            If Aktifkan Then
                If IsNothing(ObjSPOSchedule.SparePartPOScheduleTimes) OrElse ObjSPOSchedule.SparePartPOScheduleTimes.Count <= 0 Then
                    strMsg = "Jadwal Belum ada Detail Waktu"
                    Return False
                End If
            End If
            ObjSPOSchedule.Status = Aktifkan
            Dim FSparePartPOSchedule As New SparePartPOScheduleFacade(User)
            FSparePartPOSchedule.Update(ObjSPOSchedule)

            Return True
        Catch ex As Exception
            strMsg = "Data Gagal di Simpan"
            Return False
        End Try

        Return False
    End Function


    Private Sub EditHeader(e As DataGridCommandEventArgs)
        ViewState.Add(Me.strProsess, Me.Mode.Edit)
        dtgSparePartPOSchedule.SelectedIndex = e.Item.ItemIndex
        dtgSparePartPOSchedule.Enabled = False
        Dim ObjSPO As SparePartPOSchedule = CType(_sessHelper.GetSession(Me.arDSSPO)(e.Item.ItemIndex), SparePartPOSchedule)
        ViewState("ID") = ObjSPO.ID
        txtDealerName.Text = ""
        lboxHari.SelectedValue = ObjSPO.OrderDay
        lboxOrderType.SelectedValue = ObjSPO.OrderType
        Dim strDealer As String = String.Empty
        For Each objSPOD As SparePartPOScheduleDealer In ObjSPO.SparePartPOScheduleDealers
            If strDealer = "" Then
                strDealer = objSPOD.Dealer.DealerCode
            Else
                strDealer = strDealer & ";" & objSPOD.Dealer.DealerCode
            End If

        Next
        txtDealerName.Text = strDealer

        btnSearch.Enabled = False
        btnBatal.Enabled = True
    End Sub



    Private Sub ClearData()
        dtgSparePartPOSchedule.Enabled = True
        dtgSparePartPOSchedule.SelectedIndex = -1
        txtDealerName.Text = ""
        lboxHari.SelectedValue = "-1"
        lboxOrderType.SelectedValue = "-1"
        ViewState(Me.strProsess) = Mode.Insert
        btnSearch.Enabled = True

    End Sub

    Private Sub CHecking()
        btnSimpan.Visible = Edit_priv
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtDealerName.Attributes.Add("readonly", "readonly")

        CheckPriv()


        If Not IsPostBack Then
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Me.InitComponent()
            If Request.QueryString("Mode") = "New" Then
                btnSearch_Click(Me, Nothing)
            Else
                If Not IsNothing(_sessHelper.GetSession(Me.sessCriterias)) Then
                    Me.dtgSparePartPOSchedule.CurrentPageIndex = 0
                    BindDatagrid(dtgSparePartPOSchedule.CurrentPageIndex)
                    btnSimpan.Enabled = True
                End If
            End If

        End If

        CHecking()
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPOSchedule), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(criterias)
        _sessHelper.SetSession(Me.sessCriterias, criterias)
        Me.dtgSparePartPOSchedule.CurrentPageIndex = 0
        BindDatagrid(dtgSparePartPOSchedule.CurrentPageIndex)
        btnSimpan.Enabled = True


    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Select Case ViewState(Me.strProsess)
            Case Mode.Edit
                ClearData()
            Case Else
                ClearData()
        End Select
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim strMsg As String = ""
        lblInfo.Text = String.Empty
        Select Case ViewState(Me.strProsess)
            Case Mode.Edit
                If Me.Edit(strMsg) Then
                    ClearData()
                    dtgSparePartPOSchedule.CurrentPageIndex = 0
                    BindDatagrid(dtgSparePartPOSchedule.CurrentPageIndex)
                End If

                MessageBox.Show(strMsg)

            Case Else
                If Me.Insert(strMsg) Then
                    ClearData()
                    dtgSparePartPOSchedule.CurrentPageIndex = 0
                    BindDatagrid(dtgSparePartPOSchedule.CurrentPageIndex)
                End If

                MessageBox.Show(strMsg)

        End Select
    End Sub

    Protected Sub dtgSparePartPOSchedule_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgSparePartPOSchedule.ItemCommand
        Dim strMsg As String = ""
        Select Case e.CommandName.ToLower()
            Case "view"
                Response.Redirect("frmSparePartPOScheduleTime.aspx?ID=" & e.Item.Cells(0).Text & "&mode=View")
            Case "edit"
                Me.EditHeader(e)
            Case "delete"
                If Me.Delete(strMsg, CInt(e.Item.Cells(0).Text)) Then
                    ClearData()
                    dtgSparePartPOSchedule.CurrentPageIndex = 0
                    BindDatagrid(dtgSparePartPOSchedule.CurrentPageIndex)
                End If

                MessageBox.Show(strMsg)

            Case "editjam"
                Dim id = CInt(e.Item.Cells(0).Text)
                Response.Redirect("frmSparePartPOScheduleTime.aspx?ID=" & e.Item.Cells(0).Text & "&mode=Edit")
            Case "aktif"
                ViewState("ID") = CInt(e.Item.Cells(0).Text)
                If Me.Aktif(strMsg, True) Then
                    ClearData()
                    dtgSparePartPOSchedule.CurrentPageIndex = 0
                    BindDatagrid(dtgSparePartPOSchedule.CurrentPageIndex)
                End If
                MessageBox.Show(strMsg)
            Case "inaktif"
                ViewState("ID") = CInt(e.Item.Cells(0).Text)
                If Me.Aktif(strMsg, False) Then
                    ClearData()
                    dtgSparePartPOSchedule.CurrentPageIndex = 0
                    BindDatagrid(dtgSparePartPOSchedule.CurrentPageIndex)
                End If
                MessageBox.Show(strMsg)
        End Select

    End Sub

    Protected Sub dtgSparePartPOSchedule_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgSparePartPOSchedule.ItemDataBound
        '.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        If Not e.Item.DataItem Is Nothing Then
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgSparePartPOSchedule.CurrentPageIndex * dtgSparePartPOSchedule.PageSize)
            End If
            Dim ObjSPOSM As SparePartPOSchedule = CType(e.Item.DataItem, SparePartPOSchedule)

            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")

            If ObjSPOSM.Status = True Then
                CType(e.Item.FindControl("lbtnInactive"), LinkButton).Visible = True
                CType(e.Item.FindControl("lbtnInactive"), LinkButton).Attributes.Add("OnClick", "return confirm('" & "Non Aktifkan Jadwal ?" & "');")
                CType(e.Item.FindControl("lbtnActive"), LinkButton).Visible = False
            Else
                CType(e.Item.FindControl("lbtnActive"), LinkButton).Visible = True
                CType(e.Item.FindControl("lbtnActive"), LinkButton).Attributes.Add("OnClick", "return confirm('" & "Aktifkan Jadwal ?" & "');")
                CType(e.Item.FindControl("lbtnInactive"), LinkButton).Visible = False
            End If

            If Not IsNothing(ObjSPOSM.SparePartPOScheduleDealers) AndAlso ObjSPOSM.SparePartPOScheduleDealers.Count > 0 Then
                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                For _I As Integer = 0 To ObjSPOSM.SparePartPOScheduleDealers.Count - 1

                    If lblDealerCode.Text = "" Then
                        lblDealerCode.Text = CType(ObjSPOSM.SparePartPOScheduleDealers(_I), SparePartPOScheduleDealer).Dealer.DealerCode
                    Else
                        If _I > 4 Then
                            lblDealerCode.Text = lblDealerCode.Text & ";...."
                            Exit For
                        End If
                        lblDealerCode.Text = lblDealerCode.Text & ";" & CType(ObjSPOSM.SparePartPOScheduleDealers(_I), SparePartPOScheduleDealer).Dealer.DealerCode
                    End If

                Next
            End If
            Dim lblHari As Label = CType(e.Item.FindControl("lblHari"), Label)

            lblHari.Text = CType(ObjSPOSM.OrderDay, EnumSparePartPOSchedule.Hari).ToString()
            Dim lblOrderType As Label = CType(e.Item.FindControl("lblOrderType"), Label)

            Select Case ObjSPOSM.OrderType.ToLower()
                Case "r"
                    lblOrderType.Text = "Regular"
                Case "z"
                    lblOrderType.Text = "Other Regular"

            End Select

            If Not Edit_priv Then
                CType(e.Item.FindControl("lbtnInactive"), LinkButton).Visible = False
                CType(e.Item.FindControl("lbtnActive"), LinkButton).Visible = False
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = False
                CType(e.Item.FindControl("LinkButton1"), LinkButton).Visible = False
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False
            End If


        End If
    End Sub

    Protected Sub dtgSparePartPOSchedule_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgSparePartPOSchedule.PageIndexChanged
        dtgSparePartPOSchedule.SelectedIndex = -1
        dtgSparePartPOSchedule.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgSparePartPOSchedule.CurrentPageIndex)
    End Sub

    Protected Sub dtgSparePartPOSchedule_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgSparePartPOSchedule.SortCommand

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

        dtgSparePartPOSchedule.SelectedIndex = -1
        dtgSparePartPOSchedule.CurrentPageIndex = 0
        BindDatagrid(dtgSparePartPOSchedule.CurrentPageIndex)

    End Sub
End Class