Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports System.Text

Public Class FrmInsertEvent
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents icDateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents icDateUntil As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents lblPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents ddlJenisEvent As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtEventNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSum As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtArea2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblEventNo As System.Web.UI.WebControls.Label
    Protected WithEvents hdn As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblNoEvent As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblEventType As System.Web.UI.WebControls.Label
    Protected WithEvents lblAudience As System.Web.UI.WebControls.Label
    Protected WithEvents lblArea1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblArea2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtArea1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblJadwalEventFrom As System.Web.UI.WebControls.Label
    Protected WithEvents lblJadwalEventUntil As System.Web.UI.WebControls.Label
    Protected WithEvents lblNo As System.Web.UI.WebControls.Label
    Protected WithEvents txtBabitAlocation As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblBabitAlocation As System.Web.UI.WebControls.Label
    Protected WithEvents txtPersetujuanBiaya As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPersetujuanBiaya As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variables Declaration"

#End Region

#Region "Custom Methods"

    Sub BindJenisEvent()
        Dim arlJenisEvent As New ArrayList
        arlJenisEvent = New [Event].EventTypeFacade(User).RetrieveActiveList()
        ddlJenisEvent.DataTextField = "Description"
        ddlJenisEvent.DataValueField = "ID"
        ddlJenisEvent.DataSource = arlJenisEvent
        ddlJenisEvent.DataBind()
    End Sub

    Private Function ValidateData() As String
        Dim sb As New StringBuilder

        If Request.QueryString("Mode") <> "Edit" Then
            If Not lblNo.Text.Trim().ToLower().Equals("auto generated") Then
                sb.Append("Data sudah tersimpan\n")
            End If
        End If

        If (txtEventNo.Text.Trim() = String.Empty) Then
            sb.Append("No Event harus diisi\n")
        End If
        If (txtKodeDealer.Text.Trim() = String.Empty) Then
            sb.Append("Kode dealer harus diisi\n")
        End If
        If (icDateUntil.Value < icDateFrom.Value) Then
            sb.Append("Jadwal Event dari harus lebih kecil atau sama dengan jadwal event sampai\n")
        End If
        'If (txtLocation.Text.Trim() = String.Empty) Then
        '    sb.Append("Lokasi harus diisi\n")
        'End If
        If (txtSum.Text.Trim() = String.Empty) Then
            sb.Append("Jumlah undangan harus diisi\n")
        End If
        If (txtBabitAlocation.Text.Trim() = String.Empty) Then
            sb.Append("Alokasi Biaya harus diisi\n")
        End If
        If (txtPersetujuanBiaya.Text.Trim() = String.Empty) Then
            sb.Append("Persetujuan Biaya harus diisi\n")
        End If
        If (txtArea1.Text.Trim() = String.Empty) Then
            sb.Append("Area koordinator harus diisi\n")
        End If
        If (txtArea2.Text.Trim() = String.Empty) Then
            sb.Append("Management MMKSI / Observer harus diisi\n")
        End If
        Return sb.ToString()
    End Function

    Sub ViewData(ByVal id As Integer)
        Dim oInfo As New EventInfo
        oInfo = New [Event].EventInfoFacade(User).Retrieve(id)
        lblNo.Text = oInfo.EventRequestNo
        txtEventNo.Text = oInfo.EventMaster.EventNo
        lblNoEvent.Text = oInfo.EventMaster.EventNo
        lblPeriode.Text = oInfo.EventMaster.Period
        txtKodeDealer.Text = oInfo.Dealer.DealerCode
        lblDealerCode.Text = oInfo.Dealer.DealerCode
        ddlJenisEvent.SelectedValue = oInfo.EventType.ID
        lblEventType.Text = oInfo.EventType.Description
        icDateFrom.Value = oInfo.DateStart
        lblJadwalEventFrom.Text = oInfo.DateStart.ToString("dd/MM/yyyy")
        icDateUntil.Value = oInfo.DateEnd
        lblJadwalEventUntil.Text = oInfo.DateEnd.ToString("dd/MM/yyyy")
        'txtLocation.Text = oInfo.Location
        ' lblLocation.Text = oInfo.Location
        txtSum.Text = oInfo.NumOfInvitation
        lblAudience.Text = oInfo.NumOfInvitation
        txtBabitAlocation.Text = oInfo.RequestTotalCost
        lblBabitAlocation.Text = oInfo.RequestTotalCost
        txtPersetujuanBiaya.Text = oInfo.ApprovalCost
        lblPersetujuanBiaya.Text = oInfo.ApprovalCost
        txtArea1.Text = oInfo.AreaCoordinator
        lblArea1.Text = oInfo.AreaCoordinator
        txtArea2.Text = oInfo.Observer
        lblArea2.Text = oInfo.Observer
        btnSave.Text = "Ubah"
    End Sub

    Sub Mode(ByVal isView As Boolean)
        txtEventNo.Visible = Not isView
        txtKodeDealer.Visible = Not isView
        ddlJenisEvent.Visible = Not isView
        icDateFrom.Visible = Not isView
        icDateUntil.Visible = Not isView
        '  txtLocation.Visible = Not isView
        txtSum.Visible = Not isView
        txtArea1.Visible = Not isView
        txtArea2.Visible = Not isView
        txtBabitAlocation.Visible = Not isView
        txtPersetujuanBiaya.Visible = Not isView
        lblNoEvent.Visible = isView
        lblDealerCode.Visible = isView
        lblEventType.Visible = isView
        lblJadwalEventFrom.Visible = isView
        lblJadwalEventUntil.Visible = isView
        '  lblLocation.Visible = isView
        lblAudience.Visible = isView
        lblBabitAlocation.Visible = isView
        lblPersetujuanBiaya.Visible = isView
        lblArea1.Visible = isView
        lblArea2.Visible = isView
        lblEventNo.Visible = Not isView
        lblSearchDealer.Visible = Not isView
        ' lblBabitAlocationPopUp.Visible = Not isView
    End Sub

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If (Not IsPostBack) Then
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            lblEventNo.Attributes("onclick") = "ShowPPNoEvent();"
            '   lblBabitAlocationPopUp.Attributes("onclick") = "ShowPPBabitAllocation();"
            BindJenisEvent()
            If (Request.QueryString("Mode") = "Edit") Then
                ViewData(Convert.ToInt64(Request.QueryString("id")))
                Mode(False)
            ElseIf (Request.QueryString("Mode") = "View") Then
                ViewData(Convert.ToInt64(Request.QueryString("id")))
                Mode(True)
                btnSave.Visible = False
            Else
                btnCancel.Visible = False
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim str As String = ValidateData()
        If (str.Length > 0) Then
            lblPeriode.Text = hdn.Value
            MessageBox.Show(str)
            Exit Sub
        End If
        Dim oEventInfo As New EventInfo

        'Dim oBabitAllocation As New BabitAllocation
        'oBabitAllocation = New BabitSalesComm.BabitFacade(User).RetrieveBabitAlocationByNoPerjanjian(txtBabitAlocation.Text.Trim())

        'If (oBabitAllocation.ID > 0) Then
        '    If (oBabitAllocation.Dealer.DealerCode <> txtKodeDealer.Text.Trim()) Then
        '        lblPeriode.Text = hdn.Value
        '        MessageBox.Show("Kode Dealer tidak sesuai dengan Kode Dealer yg ada pada Alokasi Babit yd dipilih")
        '        Exit Sub
        '    End If
        'Else
        '    MessageBox.Show("Alokasi babit yg dimasukkan salah")
        '    Exit Sub
        'End If

        Dim oDealer As New Dealer
        oDealer = New General.DealerFacade(User).GetDealer(txtKodeDealer.Text.Trim())
        If (oDealer.ID = 0) Then
            MessageBox.Show("Kode dealer yg dimasukkan salah")
            Exit Sub
        End If

        Dim oEventMaster As New EventMaster
        oEventMaster = New [Event].EventMasterFacade(User).Retrieve(txtEventNo.Text.Trim())
        If (oEventMaster.ID = 0) Then
            MessageBox.Show("No Event yg dimasukkan salah")
            Exit Sub
        End If

        Dim oEventType As New EventType
        oEventType = New [Event].EventTypeFacade(User).Retrieve(CInt(ddlJenisEvent.SelectedValue))
        If (oEventType.ID = 0) Then
            MessageBox.Show("Jenis Event yg dimasukkan salah")
            Exit Sub
        End If

        If (Request.QueryString("Mode") = "Edit") Then
            oEventInfo = New [Event].EventInfoFacade(User).Retrieve(CInt(Request.QueryString("id")))
            oEventInfo.AreaCoordinator = txtArea1.Text.Trim()
            oEventInfo.Observer = txtArea2.Text.Trim()
            oEventInfo.DateStart = icDateFrom.Value
            oEventInfo.DateEnd = icDateUntil.Value
            oEventInfo.Dealer = oDealer
            oEventInfo.EventMaster = oEventMaster
            oEventInfo.EventType = oEventType
            oEventInfo.Location = String.Empty 'txtLocation.Text.Trim()
            oEventInfo.NumOfInvitation = txtSum.Text.Trim()
            oEventInfo.RequestTotalCost = txtBabitAlocation.Text
            oEventInfo.RealTotalCost = txtBabitAlocation.Text
            oEventInfo.ApprovalCost = txtPersetujuanBiaya.Text
            oEventInfo.RealApprovalCost = txtPersetujuanBiaya.Text

            If (New [Event].EventInfoFacade(User).Update(oEventInfo) > 0) Then
                MessageBox.Show(SR.UpdateSucces)
            Else
                MessageBox.Show(SR.UpdateFail)
            End If
        Else
            oEventInfo.AreaCoordinator = txtArea1.Text.Trim()
            oEventInfo.Observer = txtArea2.Text.Trim()
            oEventInfo.DateStart = icDateFrom.Value
            oEventInfo.DateEnd = icDateUntil.Value
            oEventInfo.Dealer = oDealer
            oEventInfo.EventMaster = oEventMaster
            oEventInfo.EventType = oEventType
            oEventInfo.Location = String.Empty 'txtLocation.Text.Trim()
            oEventInfo.NumOfInvitation = txtSum.Text.Trim()
            'oEventInfo.BabitAllocation = oBabitAllocation
            oEventInfo.RequestTotalCost = txtBabitAlocation.Text
            oEventInfo.RealTotalCost = txtBabitAlocation.Text
            oEventInfo.ApprovalCost = txtPersetujuanBiaya.Text
            oEventInfo.RealApprovalCost = txtPersetujuanBiaya.Text
            oEventInfo.RequestDate = Now

            Dim result As Integer = New [Event].EventInfoFacade(User).InsertTransaction(oEventInfo)

            If (result > 0) Then
                MessageBox.Show(SR.SaveSuccess)
                Dim objInserted As EventInfo = New [Event].EventInfoFacade(User).Retrieve(result)
                lblNo.Text = objInserted.EventRequestNo
            Else
                MessageBox.Show(SR.SaveFail)
            End If
            lblPeriode.Text = hdn.Value
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("../Event/FrmListEventInfo.aspx", True)
    End Sub

#End Region

End Class
