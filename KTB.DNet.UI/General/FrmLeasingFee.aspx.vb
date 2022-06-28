Imports Ktb.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports System.IO
Imports System.Text

Public Class FrmLeasingFee
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtmFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents dtmTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlVehicleType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlVehicleCAtegory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtFee As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private shelp As SessionHelper = New SessionHelper
    Private objDealer As Dealer
    Private Const SDEALER As String = "DEALER"
    Private Const QueryStringID As String = "id"
    Private lff As LeasingFeeFacade
    Private vtf As KTB.DNet.BusinessFacade.FinishUnit.VechileTypeFacade

    Private Function getId() As Integer
        If (Not IsNothing(Request.QueryString(QueryStringID))) Then
            Return CInt(Request.QueryString(QueryStringID))
        End If
        Return 0
    End Function

    Private Function isReadOnly() As Boolean
        If (Not IsNothing(Request.QueryString("mode"))) Then
            Try
                Return Convert.ToBoolean(Request.QueryString("mode"))
            Catch ex As Exception
                Return False
            End Try
        End If
        Return False
    End Function

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.General_manual_doc_privilege) Then
            'Server.Transfer("../FrmAccessDenied.aspx?modulName=Estimation Indent Part Equipment - Daftar Status PO")
        End If
    End Sub

    Private Sub BindVehicleCategory()
        Dim vmf As KTB.DNet.BusinessFacade.FinishUnit.VechileModelFacade = New KTB.DNet.BusinessFacade.FinishUnit.VechileModelFacade(User)
        Dim arl As ArrayList = vmf.RetrieveList("Description", Sort.SortDirection.ASC)
        ddlVehicleCAtegory.DataValueField = "ID"
        ddlVehicleCAtegory.DataTextField = "Description"
        ddlVehicleCAtegory.DataSource = arl
        ddlVehicleCAtegory.DataBind()
        ddlVehicleCAtegory.Items.Insert(0, New ListItem("All Variant", "0"))
    End Sub

    Private Sub BindVehicleType()
        ddlVehicleType.DataTextField = "Description"
        ddlVehicleType.DataValueField = "ID"
        ddlVehicleType.DataSource = vtf.RetrieveByVehicleModelId(CInt(ddlVehicleCAtegory.SelectedValue), "Description", Sort.SortDirection.ASC)
        ddlVehicleType.DataBind()
        ddlVehicleType.Items.Insert(0, New ListItem("All Type", "0"))
    End Sub

    Private Sub dataToUI()
        Dim obj As LeasingFee = lff.Retrieve(getId())
        dtmFrom.Value = obj.DateFrom
        dtmTo.Value = obj.DateTo
        txtFee.Text = obj.Fee.ToString()
        ddlVehicleCAtegory.SelectedValue = obj.VechileType.VechileModel.ID
        BindVehicleType()
        ddlVehicleType.SelectedValue = obj.VechileType.ID
    End Sub

    Private Sub setDisplayMode()
        txtFee.Enabled = isReadOnly()
        dtmFrom.Enabled = isReadOnly()
        dtmTo.Enabled = isReadOnly()
        ddlVehicleCAtegory.Enabled = isReadOnly()
        ddlVehicleType.Enabled = isReadOnly()
    End Sub

    Private Function batchInsert(ByVal arl As ArrayList) As Integer
        Dim n As Integer = 0
        Dim msg As String = ""
        For Each objVehicle As VechileType In arl
            Dim obj As LeasingFee = New LeasingFee
            If (lff.IsPeriodeExist(objVehicle.ID, dtmFrom.Value, dtmTo.Value, obj.ID)) Then
                MessageBox.Show(String.Format("Data untuk type kendaraan {0} sudah ada untuk periode ini", objVehicle.Description))
            Else
                obj.Fee = Convert.ToDecimal(txtFee.Text)
                obj.DateFrom = dtmFrom.Value
                obj.DateTo = dtmTo.Value
                obj.Status = 0
                obj.VechileType = objVehicle
                If (lff.Insert(obj) = -1) Then
                    msg += objVehicle.VechileTypeCode + ","
                End If
            End If
        Next
        If (msg.Length > 1) Then
            msg = msg.Substring(0, msg.Length - 1)
            MessageBox.Show(String.Format("Variant kendaraan dengan tipe berikut gagal disimpan : {0}.", msg))
            Return -1
        Else
            MessageBox.Show("Data berhasil disimpan")
            Return 1
        End If
    End Function

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckUserPrivilege()
        lff = New LeasingFeeFacade(User)
        vtf = New KTB.DNet.BusinessFacade.FinishUnit.VechileTypeFacade(User)
        objDealer = CType(shelp.GetSession(SDEALER), Dealer)
        btnSave.Attributes.Add("onclick", "return confirm('Yakin mau simpan ?');")

        If (IsPostBack) Then Return

        BindVehicleCategory()
        If (getId() > 0) Then
            dataToUI()
            setDisplayMode()
        Else
            btnBack.Visible = False
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not Page.IsValid() Then Return

        If (dtmTo.Value < dtmFrom.Value) Then
            MessageBox.Show("Periode awal tidak bisa lebih kecil dari periode akhir")
            Return
        End If

        If (Convert.ToDecimal(txtFee.Text) >= 100) Then
            MessageBox.Show("Fee tidak boleh lebih besar dari 100")
            Return
        End If

        Dim n As Integer = 0
        If (getId() <> 0) Then
            Dim obj As LeasingFee = lff.Retrieve(getId())
            obj.Fee = Convert.ToDecimal(txtFee.Text)
            obj.DateFrom = dtmFrom.Value
            obj.DateTo = dtmTo.Value
            obj.Status = 0
            obj.VechileType = vtf.Retrieve(CInt(ddlVehicleType.SelectedValue))
            If (lff.IsPeriodeExist(obj.VechileType.ID, dtmFrom.Value, dtmTo.Value, obj.ID)) Then
                MessageBox.Show(String.Format("Data untuk type kendaraan {0} sudah ada untuk periode ini", obj.VechileType.Description))
                Return
            End If
            n = lff.Update(obj)
        Else
            If (dtmFrom.Value < DateTime.Now.AddDays(-1)) Then
                MessageBox.Show("Periode awal tidak bisa lebih kecil dari tanggal sekarang")
                Return
            End If

            If ddlVehicleCAtegory.SelectedValue = "0" Then
                Dim arlVehicle As ArrayList = vtf.RetrieveList("ID", Sort.SortDirection.ASC)
                n = batchInsert(arlVehicle)
            Else
                If (ddlVehicleType.SelectedValue = "0") Then
                    Dim arlVehicle As ArrayList = vtf.RetrieveByVehicleModelId(CInt(ddlVehicleCAtegory.SelectedValue), "Description", Sort.SortDirection.ASC)
                    n = batchInsert(arlVehicle)
                Else
                    Dim obj As LeasingFee = New LeasingFee
                    obj.Fee = Convert.ToDecimal(txtFee.Text)
                    obj.DateFrom = dtmFrom.Value
                    obj.DateTo = dtmTo.Value
                    obj.VechileType = vtf.Retrieve(CInt(ddlVehicleType.SelectedValue))
                    If (lff.IsPeriodeExist(obj.VechileType.ID, dtmFrom.Value, dtmTo.Value, obj.ID)) Then
                        MessageBox.Show(String.Format("Data untuk type kendaraan {0} sudah ada untuk periode ini", obj.VechileType.Description))
                        Return
                    End If
                    n = lff.Insert(obj)
                End If
            End If
        End If

        If (n <> -1) Then
            MessageBox.Show("Data berhasil disimpan")
        Else
            MessageBox.Show("Data gagal disimpan")
        End If
    End Sub

    Private Sub ddlVehicleCAtegory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlVehicleCAtegory.SelectedIndexChanged
        If (ddlVehicleCAtegory.SelectedValue = "0") Then
            ddlVehicleType.Enabled = False
            ddlVehicleType.DataSource = New ArrayList
            ddlVehicleType.DataBind()
            Return
        End If
        BindVehicleType()
        'ddlVehicleType.Enabled = Not isReadOnly()
    End Sub

    Private Sub btnBack_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.ServerClick
        Response.Redirect("~/General/FrmLeasingFeeList.aspx?isback=true")
    End Sub
End Class
