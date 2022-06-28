#Region "Custom NameSpace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

Public Class frmSparePartPOScheduleTime
    Inherits System.Web.UI.Page

#Region "variable"
    Dim _sessHelper As SessionHelper = New SessionHelper
    Private arlAppConfig As ArrayList
    Private sessCriterias As String = "frmSparePartPOScheduleTime.sessCriterias"
    Private strProsess As String = "VsProses"
    Private arDSSPO As String = "arDSSPO"

    Private Enum Mode
        Edit
        Insert
        View
    End Enum
#End Region

#Region "Custom Method"

    Private Enum pageMode
        Read
        Edit
        Insert
    End Enum

    Private Sub InitComponent()
        lboxHari.DataSource = Nothing
        lboxHari.Items.Clear()
        Dim aa As New SortedList
        aa = EnumSparePartPOSchedule.RetrieveHari(False)
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


        ddlHour.DataSource = Nothing
        ddlHour.Items.Clear()
        For i As Integer = 23 To 0 Step -1
            ddlHour.Items.Insert(0, New ListItem(Right("0" & i.ToString(), 2), Right("0" & i.ToString(), 2)))
        Next
      
        ddlHour.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        ddlHour.DataBind()

        ddlMinute.DataSource = Nothing
        ddlMinute.Items.Clear()
        ddlMinute.Items.Insert(0, New ListItem("30", "30"))
        ddlMinute.Items.Insert(0, New ListItem("00", "00"))
        ddlMinute.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        ddlMinute.DataBind()

        ViewState("CurrentSortColumn") = "ID"
        ViewState("ID") = "0"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        ViewState.Add(Me.strProsess, Mode.Insert)

        ViewState.Add("SortColDealer", "DealerCode")
        ViewState.Add("SortDirDealer", Sort.SortDirection.ASC)
    End Sub


    Private Sub BindHeader()
        If Request.QueryString("ID") <> "" Then
            ViewState("HeaderID") = CInt(Request.QueryString("ID"))
            Dim ObjSPOSchedule As SparePartPOSchedule = New SparePartPOScheduleFacade(User).Retrieve(CInt(ViewState("HeaderID")))
            lboxHari.SelectedValue = ObjSPOSchedule.OrderDay
            lboxHari.Enabled = False
            lboxOrderType.SelectedValue = ObjSPOSchedule.OrderType
            lboxOrderType.Enabled = False

            Dim strDealer As String = String.Empty
            For Each objSPOD As SparePartPOScheduleDealer In ObjSPOSchedule.SparePartPOScheduleDealers
                If strDealer = "" Then
                    strDealer = objSPOD.Dealer.DealerCode
                Else
                    strDealer = strDealer & ";" & objSPOD.Dealer.DealerCode
                End If

            Next
            txtDealerName.Text = strDealer

            If Request.QueryString("Mode") = "View" Then
                btnSimpan.Enabled = False
                btnSearch.Enabled = False
                btnBatal.Enabled = False
                dtgSPOT.Columns(3).Visible = False

                lblHkd.Visible = False
                lblHkdt.Visible = False
                txtDealerName.Visible = False

                lblHour.Visible = False
                lblHours.Visible = False
                ddlHour.Visible = False
                ddlMinute.Visible = False

                row1.Visible = False
                row2.Visible = False
                BindSearch()
            End If
        End If
    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim TotalRow As Integer = 0
        If indexPage >= 0 Then
            Dim arlAppConfig = New SparePartPOScheduleTimeFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession(Me.sessCriterias), CriteriaComposite), indexPage + 1, dtgSPOT.PageSize, TotalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            _sessHelper.SetSession(Me.arDSSPO, arlAppConfig)
            dtgSPOT.DataSource = arlAppConfig
            dtgSPOT.VirtualItemCount = TotalRow
            dtgSPOT.DataBind()
        End If
    End Sub

    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)

        Dim bHH As Boolean = (ddlHour.SelectedValue.Length > 0 AndAlso ddlHour.SelectedValue.ToString <> "-1")
        Dim bDD As Boolean = (ddlMinute.SelectedValue.Length > 0 AndAlso ddlMinute.SelectedValue.ToString <> "-1")

        If bHH AndAlso bDD Then
            Dim ObjDT As DateTime
            ObjDT = New DateTime(1900, 1, 1, CInt(ddlHour.SelectedValue), CInt(ddlMinute.SelectedValue), 0)

            criterias.opAnd(New Criteria(GetType(SparePartPOScheduleTime), "ScheduleTime", MatchType.Exact, ObjDT.ToString("yyyy/MM/dd HH:mm")))

        ElseIf bHH AndAlso bDD = False Then
            MessageBox.Show("Pencarian waktu harus mengisi Jam & Menit")
        ElseIf bDD AndAlso bHH = False Then
            MessageBox.Show("Pencarian waktu harus mengisi Jam & Menit")
        End If



    End Sub

    Private Function Insert(ByRef strMsg As String) As Boolean
        strMsg = SR.SaveSuccess

        Try
            Dim ObjSPOST As New SparePartPOScheduleTime
            Dim ObjSPOSs As New SparePartPOSchedule(CInt(ViewState("HeaderID")))
            ObjSPOST.Status = True
            'Validation
            Dim bHH As Boolean = (ddlHour.SelectedValue.Length > 0 AndAlso ddlHour.SelectedValue.ToString <> "-1")
            Dim bDD As Boolean = (ddlMinute.SelectedValue.Length > 0 AndAlso ddlMinute.SelectedValue.ToString <> "-1")

            If bHH Then

            Else
                strMsg = "Jam harus diisi"
                Return False
            End If

            If bDD Then

            Else
                strMsg = "Menit Harus Diisi"
                Return False
            End If



            Dim ObjDT As DateTime
            ObjDT = New DateTime(1900, 1, 1, CInt(ddlHour.SelectedValue), CInt(ddlMinute.SelectedValue), 0)

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPOScheduleTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartPOScheduleTime), "SparePartPOSchedule.ID", MatchType.Exact, ViewState("HeaderID")))
            criterias.opAnd(New Criteria(GetType(SparePartPOScheduleTime), "ScheduleTime", MatchType.Exact, ObjDT.ToString("yyyy/MM/dd HH:mm")))

            Dim FSPOST As New SparePartPOScheduleTimeFacade(User)
            Dim arrResult As New ArrayList
            arrResult = FSPOST.Retrieve(criterias)
            If Not IsNothing(arrResult) AndAlso arrResult.Count > 0 Then
                strMsg = "Data Sudah Ada"
                Return False
            End If

            ObjSPOST.ScheduleTime = ObjDT
            'Dim FSparePartPOSchedule As New SparePartPOScheduleFacade(User)
            ''ObjSPOSs = FSparePartPOSchedule.Retrieve(CInt(ViewState("HeaderID")))
            ObjSPOST.SparePartPOSchedule = ObjSPOSs
            FSPOST.Insert(ObjSPOST)
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

            Dim ObjSPOST As SparePartPOScheduleTime = New SparePartPOScheduleTimeFacade(User).Retrieve(CInt(ViewState("ID")))
            Dim ObjSPOSs As New SparePartPOSchedule(CInt(ViewState("HeaderID")))
            'Validation
            Dim bHH As Boolean = (ddlHour.SelectedValue.Length > 0 AndAlso ddlHour.SelectedValue.ToString <> "-1")
            Dim bDD As Boolean = (ddlMinute.SelectedValue.Length > 0 AndAlso ddlMinute.SelectedValue.ToString <> "-1")

           

            If bHH Then

            Else
                strMsg = "Jam harus diisi"
                Return False
            End If

            If bDD Then

            Else
                strMsg = "Menit Harus Diisi"
                Return False
            End If



            Dim ObjDT As DateTime
            ObjDT = New DateTime(1900, 1, 1, CInt(ddlHour.SelectedValue), CInt(ddlMinute.SelectedValue), 0)

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.SparePartPOScheduleTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartPOScheduleTime), "SparePartPOSchedule.ID", MatchType.Exact, ViewState("HeaderID")))
            criterias.opAnd(New Criteria(GetType(SparePartPOScheduleTime), "ID", MatchType.No, ViewState("ID")))
            criterias.opAnd(New Criteria(GetType(SparePartPOScheduleTime), "ScheduleTime", MatchType.Exact, ObjDT.ToString("yyyy/MM/dd HH:mm")))

            Dim FSPOST As New SparePartPOScheduleTimeFacade(User)
            Dim arrResult As New ArrayList
            arrResult = FSPOST.Retrieve(criterias)
            If Not IsNothing(arrResult) AndAlso arrResult.Count > 0 Then
                strMsg = "Data Sudah Ada"
                Return False
            End If

            ObjSPOST.ScheduleTime = ObjDT
            ObjSPOST.SparePartPOSchedule = ObjSPOSs
            FSPOST.Update(ObjSPOST)
            Return True

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

            Dim ObjSPOSchedule As SparePartPOScheduleTime = New SparePartPOScheduleTimeFacade(User).Retrieve(ID)

            ObjSPOSchedule.RowStatus = CInt(DBRowStatus.Deleted)

            Dim FSparePartPOSchedule As New SparePartPOScheduleTimeFacade(User)
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

            Dim ObjSPOSchedule As SparePartPOScheduleTime = New SparePartPOScheduleTimeFacade(User).Retrieve(CInt(ViewState("ID")))
            ObjSPOSchedule.Status = Aktifkan
            Dim FSparePartPOSchedule As New SparePartPOScheduleTimeFacade(User)
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
        dtgSPOT.SelectedIndex = e.Item.ItemIndex
        dtgSPOT.Enabled = False
        Dim ObjSPO As SparePartPOScheduleTime = CType(_sessHelper.GetSession(Me.arDSSPO)(e.Item.ItemIndex), SparePartPOScheduleTime)
        ViewState("ID") = ObjSPO.ID

        ddlHour.SelectedValue = Right("0" & ObjSPO.ScheduleTime.Hour.ToString(), 2)
        ddlMinute.SelectedValue = Right("0" & ObjSPO.ScheduleTime.Minute.ToString(), 2)

        btnSearch.Enabled = False
        btnBatal.Enabled = True
    End Sub



    Private Sub ClearData()
        dtgSPOT.Enabled = True
        dtgSPOT.SelectedIndex = -1
        ddlHour.SelectedValue = "-1"
        ddlMinute.SelectedValue = "-1"
        ' txtDealerName.Text = ""
        ' lboxHari.SelectedValue = "-1"
        ' lboxOrderType.SelectedValue = "-1"
        ViewState(Me.strProsess) = Mode.Insert
        btnSearch.Enabled = True

    End Sub


    Public Sub BindSearch()

        Dim strQUery As String = ""
        strQUery = String.Format("SELECT x.DealerID FROM dbo.SparePartPOScheduleDealer x WHERE x.SparePartPOScheduleID={0} AND x.RowStatus=0 ", ViewState("HeaderID").ToString())

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ID", MatchType.InSet, "(" & strQUery & ")"))


        dtgDealerSelection.DataSource = New DealerFacade(User).RetrieveActiveList(criterias, ViewState("SortColDealer"), ViewState("SortDirDealer"))
        dtgDealerSelection.DataBind()
    End Sub


#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtDealerName.Attributes.Add("readonly", "readonly")
        If Not IsPostBack Then
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Me.InitComponent()
            Me.BindHeader()
            Me.btnSearch_Click(Me, Nothing)
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPOScheduleTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SparePartPOScheduleTime), "SparePartPOSchedule.ID", MatchType.Exact, ViewState("HeaderID")))
        CreateCriteria(criterias)
        _sessHelper.SetSession(Me.sessCriterias, criterias)
        Me.dtgSPOT.CurrentPageIndex = 0
        BindDatagrid(dtgSPOT.CurrentPageIndex)
       

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
        Select Case ViewState(Me.strProsess)
            Case Mode.Edit
                If Me.Edit(strMsg) Then
                    ClearData()
                    dtgSPOT.CurrentPageIndex = 0
                    BindDatagrid(dtgSPOT.CurrentPageIndex)
                End If

                MessageBox.Show(strMsg)

            Case Else
                If Me.Insert(strMsg) Then
                    ClearData()
                    dtgSPOT.CurrentPageIndex = 0
                    BindDatagrid(dtgSPOT.CurrentPageIndex)
                End If

                MessageBox.Show(strMsg)

        End Select
    End Sub

    Protected Sub dtgSPOT_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgSPOT.ItemCommand
        Dim strMsg As String = ""
        Select Case e.CommandName.ToLower()
            Case "view"
            Case "edit"
                Me.EditHeader(e)
            Case "delete"
                If Me.Delete(strMsg, CInt(e.Item.Cells(0).Text)) Then
                    ClearData()
                    dtgSPOT.CurrentPageIndex = 0
                    BindDatagrid(dtgSPOT.CurrentPageIndex)
                End If

                MessageBox.Show(strMsg)

            Case "editjam"
            Case "aktif"
                ViewState("ID") = CInt(e.Item.Cells(0).Text)
                If Me.Aktif(strMsg, True) Then
                    ClearData()
                    dtgSPOT.CurrentPageIndex = 0
                    BindDatagrid(dtgSPOT.CurrentPageIndex)
                End If
                MessageBox.Show(strMsg)
            Case "inaktif"
                ViewState("ID") = CInt(e.Item.Cells(0).Text)
                If Me.Aktif(strMsg, False) Then
                    ClearData()
                    dtgSPOT.CurrentPageIndex = 0
                    BindDatagrid(dtgSPOT.CurrentPageIndex)
                End If
                MessageBox.Show(strMsg)
        End Select

    End Sub

    Protected Sub dtgSPOT_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgSPOT.ItemDataBound
        '.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        If Not e.Item.DataItem Is Nothing Then
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgSPOT.CurrentPageIndex * dtgSPOT.PageSize)
            End If
            Dim ObjSPOSM As SparePartPOScheduleTime = CType(e.Item.DataItem, SparePartPOScheduleTime)

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

           
            Dim lblHari As Label = CType(e.Item.FindControl("lblJAM"), Label)


            lblHari.Text = Right("0" & ObjSPOSM.ScheduleTime.Hour.ToString(), 2) & " : " & Right("0" & ObjSPOSM.ScheduleTime.Minute.ToString(), 2)


        End If
    End Sub

    Protected Sub dtgSPOT_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgSPOT.PageIndexChanged

        dtgSPOT.SelectedIndex = -1
        dtgSPOT.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgSPOT.CurrentPageIndex)
    End Sub

    Protected Sub dtgSPOT_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgSPOT.SortCommand

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

        dtgSPOT.SelectedIndex = -1
        dtgSPOT.CurrentPageIndex = 0
        BindDatagrid(dtgSPOT.CurrentPageIndex)

    End Sub

    Protected Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Response.Redirect("frmSparePartPOScheduleManage.aspx", True)
    End Sub

    Private Sub dtgDealerSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDealerSelection.SortCommand
        If e.SortExpression = viewstate("SortColDealer") Then
            If viewstate("SortDirDealer") = Sort.SortDirection.ASC Then
                viewstate.Add("SortDirDealer", Sort.SortDirection.DESC)
            Else
                viewstate.Add("SortDirDealer", Sort.SortDirection.ASC)
            End If
        End If
        viewstate.Add("SortColDealer", e.SortExpression)
        BindSearch()
    End Sub

    Private Sub dtgDealerSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDealerSelection.PageIndexChanged
        dtgDealerSelection.CurrentPageIndex = e.NewPageIndex
        BindSearch()
    End Sub


    Private Sub dtgDealerSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDealerSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As Dealer = CType(e.Item.DataItem, Dealer)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 '+ (dtgDealerSelection.CurrentPageIndex * dtgDealerSelection.PageSize)

                Dim lblGroup As Label = CType(e.Item.FindControl("lblGroup"), Label)
                If Not IsNothing(RowValue.DealerGroup) Then
                    lblGroup.Text = RowValue.DealerGroup.GroupName
                End If
                Dim lblCity As Label = CType(e.Item.FindControl("lblCity"), Label)
                If Not IsNothing(RowValue.City) Then
                    lblCity.Text = RowValue.City.CityName
                End If
            End If
        End If
    End Sub

End Class