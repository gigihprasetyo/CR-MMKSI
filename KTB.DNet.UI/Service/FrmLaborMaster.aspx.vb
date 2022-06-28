#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

Public Class FrmLaborMaster
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents txtLaborCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtWorkCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents ddlVehicleCode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgLabor As System.Web.UI.WebControls.DataGrid
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents lblSearchPosisi As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchKerja As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private alVehicleType As ArrayList = New ArrayList
    Private sesHelper As SessionHelper = New SessionHelper
#End Region

#Region "Custom Method"

    Private Sub RetrieveVehicleType()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'alVehicleType = New VechileTypeFacade(User).Retrieve(criterias)

        Dim sortColl As SortCollection = New SortCollection
        If (Not IsNothing("VechileTypeCode")) Then
            sortColl.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))
        Else
            sortColl = Nothing
        End If
        alVehicleType = New VechileTypeFacade(User).Retrieve(criterias, sortColl)

        sesHelper.SetSession("VehicleType", alVehicleType)
    End Sub

    Private Sub GetDdlVehicleType()
        Dim i As Integer
        RetrieveVehicleType()
        ddlVehicleCode.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        If alVehicleType.Count > 0 Then
            For i = 0 To alVehicleType.Count - 1
                ddlVehicleCode.Items.Add(New ListItem(CType(alVehicleType(i), VechileType).VechileTypeCode, (CType(alVehicleType(i), VechileType).ID).ToString()))
            Next
        End If
        ddlVehicleCode.SelectedValue = "-1"
    End Sub

    Private Sub BindData()
        GetDdlVehicleType()
        BindDG(0)
    End Sub

    Private Sub BindDG(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            'dgLabor.DataSource = New LaborMasterFacade(User).RetrieveActiveList(indexPage + 1, dgLabor.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
            dgLabor.DataSource = New LaborMasterFacade(User).RetrieveActiveList(CreateCriteriaCari(CType(ViewState("FindIDVehicleCode"), Integer), CType(ViewState("FindLaborCode"), String), CType(ViewState("FindWorkCode"), String)), indexPage + 1, dgLabor.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortTable"), System.Type), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dgLabor.VirtualItemCount = totalRow
            dgLabor.DataBind()
        End If
    End Sub

    Private Sub InitiatePage()
        ClearData()
        ViewState("CurrentSortTable") = GetType(LaborMaster)
        ViewState("CurrentSortColumn") = "LaborCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub ClearData()
        txtLaborCode.Text = String.Empty
        txtWorkCode.Text = String.Empty
        btnSave.Enabled = True
        ddlVehicleCode.SelectedValue = "-1"
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub InsertLabor()
        Dim objLaborFacade As LaborMasterFacade = New LaborMasterFacade(User)
        Dim objLabor As LaborMaster = New LaborMaster
        Dim nResult As Integer
        nResult = objLaborFacade.ValidateCode(txtLaborCode.Text, txtWorkCode.Text, Me.ddlVehicleCode.SelectedValue)
        If nResult = 0 Then
            If ddlVehicleCode.SelectedValue <> "-1" Then
                objLabor.LaborCode = txtLaborCode.Text
                objLabor.WorkCode = txtWorkCode.Text
                objLabor.VechileType = CType(CType(Session("VehicleType"), ArrayList)(ddlVehicleCode.SelectedIndex - 1), VechileType)
                nResult = New LaborMasterFacade(User).Insert(objLabor)
                If nResult = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    objLabor.ID = nResult
                    'UpdateWSCDetail(objLabor)
                    MessageBox.Show(SR.SaveSuccess)
                    InitiatePage()
                    dgLabor.CurrentPageIndex = 0
                End If
            End If
        ElseIf nResult = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            MessageBox.Show(SR.DataIsExist("Kombinasi Kode Posisi, Kode Kerja dan Kode Tipe Kendaraan"))
        End If
    End Sub

    Private Sub UpdateWSCDetail(ByVal LaborMaster As LaborMaster)
        Dim objWSCFacade As WSCDetailFacade = New WSCDetailFacade(User)
        Dim nResult As Integer
        nResult = objWSCFacade.ValidateCode(txtLaborCode.Text, txtWorkCode.Text)
        If nResult > 0 Then
            If ddlVehicleCode.SelectedValue <> "-1" Then
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCDetail), "Status", MatchType.Exact, 0))
                crit.opAnd(New Criteria(GetType(WSCDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(WSCDetail), "WorkCode", MatchType.Exact, txtWorkCode.Text))
                crit.opAnd(New Criteria(GetType(WSCDetail), "PositionCode", MatchType.Exact, txtLaborCode.Text))
                Dim arrWSCDetail As ArrayList = objWSCFacade.Retrieve(crit)
                For Each item As WSCDetail In arrWSCDetail
                    item.LaborMaster = LaborMaster
                    nResult = New WSCDetailFacade(User).Update(item)
                Next

                If nResult = 0 Then
                    MessageBox.Show(SR.UpdateFail)
                Else
                    MessageBox.Show(SR.UpdateSucces)
                    InitiatePage()
                    dgLabor.CurrentPageIndex = 0
                End If
            End If
        ElseIf nResult = 0 Then
            MessageBox.Show(SR.UpdateFail)
        Else
            MessageBox.Show(SR.DataIsExist("Kombinasi Kode Posisi, Kode Kerja dan Kode Tipe Kendaraan"))
        End If
    End Sub

    Private Function ViewLabor(ByVal nID As Integer, ByVal EditStatus As Boolean) As Boolean
        Dim objLabor As LaborMaster
        objLabor = New LaborMasterFacade(User).Retrieve(nID)
        If Not objLabor Is Nothing Then
            sesHelper.SetSession("vsLabor", objLabor)
            txtLaborCode.Text = objLabor.LaborCode
            txtWorkCode.Text = objLabor.WorkCode
            ddlVehicleCode.SelectedValue = objLabor.VechileType.ID
            Me.btnSave.Enabled = EditStatus
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub UpdateLabor()
        Dim objLaborFacade As LaborMasterFacade = New LaborMasterFacade(User)
        Dim objLabor As LaborMaster = CType(Session("vsLabor"), LaborMaster)
        Dim nResult As Integer
        nResult = objLaborFacade.ValidateCode(txtLaborCode.Text, txtWorkCode.Text, Me.ddlVehicleCode.SelectedValue)
        If nResult = 0 Then
            If ddlVehicleCode.SelectedValue <> "-1" Then
                objLabor.LaborCode = txtLaborCode.Text
                objLabor.WorkCode = txtWorkCode.Text
                objLabor.VechileType = CType(CType(Session("VehicleType"), ArrayList)(ddlVehicleCode.SelectedIndex - 1), VechileType)

                nResult = New LaborMasterFacade(User).Update(objLabor)

                If nResult = 0 Then
                    MessageBox.Show(SR.UpdateFail)
                Else
                    MessageBox.Show(SR.UpdateSucces)
                    InitiatePage()
                    dgLabor.CurrentPageIndex = 0
                End If
            End If
        ElseIf nResult = 0 Then
            MessageBox.Show(SR.UpdateFail)
        Else
            MessageBox.Show(SR.DataIsExist("Kombinasi Kode Posisi, Kode Kerja dan Kode Tipe Kendaraan"))
        End If
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
            ByVal LaborID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "LaborMaster", MatchType.Exact, LaborID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Sub DeleteLabor(ByVal nID As Integer)
        Dim iRecordCount As Integer = 0

        If New HelperFacade(User, GetType(WSCDetail)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(WSCDetail), nID), _
            CreateAggreateForCheckRecord(GetType(WSCDetail))) Then
            iRecordCount = iRecordCount + 1
        End If

        If iRecordCount > 0 Then
            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objLabor As LaborMaster
            objLabor = New LaborMasterFacade(User).Retrieve(nID)
            If Not objLabor Is Nothing Then
                Dim facade As LaborMasterFacade = New LaborMasterFacade(User)
                If facade.DeleteFromDB(objLabor) > 0 Then
                    dgLabor.CurrentPageIndex = 0
                    BindDG(dgLabor.CurrentPageIndex)
                    MessageBox.Show(SR.DeleteSucces)
                Else
                    MessageBox.Show(SR.DeleteFail)
                End If
            Else
                MessageBox.Show(SR.DeleteFail)
            End If
        End If
    End Sub

    Private Function CreateCriteriaCari(ByVal IDVehicleType As Integer, ByVal KodeLabor As String, ByVal KodeWork As String) As CriteriaComposite
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(LaborMaster), "RowStatus", _
        'MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim criterias As New CriteriaComposite(New Criteria(GetType(LaborMaster), "VechileType.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If IDVehicleType <> -1 Then criterias.opAnd(New Criteria(GetType(LaborMaster), "VechileType", MatchType.Exact, IDVehicleType))
        If KodeLabor <> "" And KodeLabor <> String.Empty Then criterias.opAnd(New Criteria(GetType(LaborMaster), "LaborCode", MatchType.StartsWith, KodeLabor))
        If KodeWork <> "" And KodeWork <> String.Empty Then criterias.opAnd(New Criteria(GetType(LaborMaster), "WorkCode", MatchType.StartsWith, KodeWork))

        Return criterias
    End Function

#End Region

#Region "EventHandler"

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ServiceKodePosisiView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=WSC - Kode Posisi ")
        End If
        If Not SecurityProvider.Authorize(context.User, SR.ServiceKodePosisiUpdate_Privilege) Then
            Me.btnSave.Visible = False
            Me.btnCancel.Visible = False
            Me.dgLabor.Columns(6).Visible = False
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        If Not IsPostBack Then
            lblSearchPosisi.Attributes("onclick") = "ShowPosisi()"
            lblSearchKerja.Attributes("onclick") = "ShowKerja()"

            ViewState("CurrentSortTable") = GetType(LaborMaster)
            ViewState("CurrentSortColumn") = "LaborCode"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

            ViewState("FindIDVehicleCode") = "-1"
            ViewState("FindLaborCode") = ""
            ViewState("FindWorkCode") = ""

            BindData()
            InitiatePage()
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click

        ViewState("CurrentSortTable") = GetType(LaborMaster)
        ViewState("CurrentSortColumn") = "LaborCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

        ViewState("FindIDVehicleCode") = CType(ddlVehicleCode.SelectedValue, Integer)
        ViewState("FindLaborCode") = txtLaborCode.Text.Trim
        ViewState("FindWorkCode") = txtWorkCode.Text.Trim

        dgLabor.CurrentPageIndex = 0
        BindDG(dgLabor.CurrentPageIndex)

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        ddlVehicleCode.Enabled = True
        txtLaborCode.ReadOnly = False
        txtWorkCode.ReadOnly = False

        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(VechileType), "ID", MatchType.Exact, ddlVehicleCode.SelectedValue))
        criterias.opAnd(New Criteria(GetType(VechileType), "ProductCategory.Code", MatchType.Exact, companyCode))

        Dim categoryArr As ArrayList = New VechileTypeFacade(User).Retrieve(criterias)
        If categoryArr.Count = 0 Then
            MessageBox.Show("Kategori tidak sesuai")
            Return
        End If

        If CType(ViewState("vsProcess"), String) = "Insert" Then
            InsertLabor()
        Else
            UpdateLabor()
        End If
        BindDG(dgLabor.CurrentPageIndex)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ddlVehicleCode.Enabled = True
        txtLaborCode.ReadOnly = False
        txtWorkCode.ReadOnly = False
        ddlVehicleCode.SelectedValue = "-1"
        ClearData()
    End Sub

    Private Function GetLaborCode(ByVal code As String) As String
        Dim kodePosisiFacade As DeskripsiPositionCodeFacade = New DeskripsiPositionCodeFacade(User)
        Dim kode As DeskripsiKodePosisi = kodePosisiFacade.Retrieve(code)
        If kode.ID > 0 Then
            Return kode.Description
        Else
            Return "Tidak terdifinisi"
        End If
    End Function

    Private Function GetWorkCode(ByVal code As String) As String
        Dim kodePosisiFacade As DeskripsiWorkPositionFacade = New DeskripsiWorkPositionFacade(User)
        Dim kode As DeskripsiKodeKerja = kodePosisiFacade.Retrieve(code)
        If kode.ID > 0 Then
            Return kode.Description
        Else
            Return "Tidak terdifinisi"
        End If
    End Function

    Private Sub dgLabor_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgLabor.ItemDataBound
        If Not e.Item.FindControl("lnkDelete") Is Nothing Then
            CType(e.Item.FindControl("lnkDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dgLabor.CurrentPageIndex * dgLabor.PageSize)
        End If

        If e.Item.ItemIndex <> -1 Then
            Try
                Dim row As LaborMaster = CType(e.Item.DataItem, LaborMaster)
                Dim lblKodePosisi As Label = CType(e.Item.FindControl("lblKodePosisi"), Label)
                Dim lblKodeKerja As Label = CType(e.Item.FindControl("lblKodeKerja"), Label)

                lblKodePosisi.Text = row.LaborCode
                lblKodeKerja.Text = row.WorkCode
                lblKodePosisi.ToolTip = GetLaborCode(row.LaborCode)
                lblKodeKerja.ToolTip = GetWorkCode(row.WorkCode)
                If row.RowStatus = -1 Then
                    e.Item.BackColor = Color.Yellow
                End If
            Catch ex As Exception
                e.Item.Cells(2).Text = ""
                e.Item.Cells(4).Text = ""
            End Try
        End If

    End Sub

    Private Sub dgLabor_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgLabor.PageIndexChanged
        dgLabor.CurrentPageIndex = e.NewPageIndex
        BindDG(dgLabor.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dgLabor_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgLabor.ItemCommand
        Select Case e.CommandName
            Case "View"
                If ViewLabor(e.Item.Cells(0).Text, False) Then
                    ddlVehicleCode.Enabled = False
                    txtLaborCode.ReadOnly = True
                    txtWorkCode.ReadOnly = True
                    ViewState.Add("vsProcess", "View")
                    dgLabor.SelectedIndex = e.Item.ItemIndex
                Else
                    MessageBox.Show(SR.ViewFail)
                End If
            Case "Edit"
                If ViewLabor(e.Item.Cells(0).Text, True) Then
                    ddlVehicleCode.Enabled = True
                    txtLaborCode.ReadOnly = False
                    txtWorkCode.ReadOnly = False
                    ViewState.Add("vsProcess", "Edit")
                    dgLabor.SelectedIndex = e.Item.ItemIndex
                Else
                    MessageBox.Show(SR.ViewFail)
                End If
            Case "Delete"
                DeleteLabor(e.Item.Cells(0).Text)
                ClearData()
        End Select
    End Sub

    Private Sub dgLabor_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgLabor.SortCommand
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
            Select Case CType(ViewState("CurrentSortColumn"), String)
                Case "VechileTypeCode"
                    ViewState("CurrentSortTable") = GetType(VechileType)
                Case "LaborCode", "WorkCode"
                    ViewState("CurrentSortTable") = GetType(LaborMaster)
            End Select
        End If

        dgLabor.CurrentPageIndex = 0
        BindDG(dgLabor.CurrentPageIndex)
        ClearData()
    End Sub

#End Region


End Class