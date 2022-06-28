#Region "Custom Namespace Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Imports KTB.DNet.BusinessValidation
#End Region


#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
#End Region


Public Class FrmSalesmanPartRegister
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents ddlSalesmanUnit As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealerBranchCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopUpDealerBranch As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgResult As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents lblSalesmanUnit As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    Private _SalesmanHeaderFacade As New SalesmanHeaderFacade(User)
    Private _SalesmanTrainingParticipantFacade As New SalesmanTrainingParticipantFacade(User)
    Private _SalesmanUniformAssignedFacade As New SalesmanUniformAssignedFacade(User)

    Private _downloadPriv As Boolean = False
    Private sessHelper As New SessionHelper
    Private strDefDate As String = "1753/01/01"

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.lihat_buatid_salesman_part_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Salesman - Buat ID Salesman")
        End If
    End Sub

    Dim bCekBtnGridPriv As Boolean = SecurityProvider.Authorize(context.User, SR.buat_buatid_salesman_part_privilege)
    Dim bCekBtnDLPriv As Boolean = SecurityProvider.Authorize(context.User, SR.download_buat_salesman_part_privilege)
#End Region

#Region "PrivateCustomMethods"
    ' Untuk bind data yg bersangkutan - related
    Private Sub BindDropDownLists()
        CommonFunction.BindFromEnum("SalesmanUnit", ddlSalesmanUnit, Me.User, True, "NameStatus", "ValStatus")
        CommonFunction.BindFromEnum("SalesmanRegisterStatus", ddlStatus, Me.User, True, "NameStatus", "ValStatus")
    End Sub
    ' penambahan untuk initialize data
    Private Sub ClearData()
        CheckModule()
        txtDealerCode.Text = String.Empty
        txtDealerBranchCode.Text = String.Empty
        ddlStatus.SelectedIndex = -1
        txtName.Text = String.Empty
    End Sub
    ' untuk update data yg sdh ada sebelumnya - register salesman id
    Private Sub Update(ByVal nID As Integer)
        ' hanya yg belum saja, yg bisa diregister
        Dim intMaxCode As Integer
        intMaxCode = 3
        Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(nID)
        If objSalesmanHeader.SalesmanAdditionalInfo.Count < 1 Then
            MessageBox.Show("Kategori dan Posisi Salesman belum di tentukan")
            Exit Sub
        End If
        If Not objSalesmanHeader Is Nothing Then
            If objSalesmanHeader.RegisterStatus = EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Belum_Register Then

                'update
                objSalesmanHeader.SalesmanCode = "request_part"
                objSalesmanHeader.RegisterStatus = EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Sudah_Register ' set supaya diregister
                objSalesmanHeader.Status = EnumSalesmanStatus.SalesmanStatus.Aktif

                If objSalesmanHeader.JobPosition Is Nothing Then
                    UpdateJobPositionNull(objSalesmanHeader)
                End If

                Try

                    Dim vr As ValidResult = New SalesmanHeaderValidation().ValidateKTPSalesmanHeader(objSalesmanHeader)

                    If vr.IsValid = False Then
                        MessageBox.Show(vr.Message)
                        Exit Sub
                    End If

                    Dim nResult = New SalesmanHeaderFacade(User).Update(objSalesmanHeader)
                    If nResult < 0 Then
                        MessageBox.Show("Record Gagal Diupdate")
                    Else
                        Dim oNewSalesHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(objSalesmanHeader.ID)
                        nResult = SaveSalesmanPartHistory(oNewSalesHeader, EnumSalesmanStatus.SalesmanStatus.Aktif)

                        MessageBox.Show("Data berhasil diregister")
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
                
            Else
                MessageBox.Show("Record Salesman tdk bisa diupdate, karena sudah terregister sebelumnya")
            End If
        End If

    End Sub

    Private Function SaveSalesmanPartHistory(ByVal salemanHeader As SalesmanHeader, ByVal status As EnumSalesmanStatus.SalesmanStatus) As Integer
        Dim iReturn As Integer
        Dim oSalesHistoryFacade As SalesmanPartHistoryFacade = New SalesmanPartHistoryFacade(User)
        Dim oSalesmanCategoryLevelFacade As SalesmanCategoryLevelFacade = New SalesmanCategoryLevelFacade(User)
        Dim oSalemenLevelFacade As SalesmanLevelFacade = New SalesmanLevelFacade(User)

        Dim oSalesHistory As SalesmanPartHistory

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanPartHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanPartHistory), "SalesmanHeader.ID", MatchType.Exact, salemanHeader.ID))
        Dim arlSalesHistory As ArrayList = oSalesHistoryFacade.Retrieve(criterias)
        If arlSalesHistory.Count < 1 Then
            oSalesHistory = New SalesmanPartHistory
        Else
            oSalesHistory = CType(arlSalesHistory(0), SalesmanPartHistory)
        End If
        oSalesHistory.Status = status
        oSalesHistory.Dealer = salemanHeader.Dealer
        oSalesHistory.SalesmanHeader = salemanHeader
        oSalesHistory.SalesmanCategoryLevel = CType(salemanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanCategoryLevel ' oSalesmanCategoryLevelFacade.Retrieve(salemanHeader.SalesmanLevel.ID)
        oSalesHistory.SalesmanLevel = CType(salemanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanLevel
        If salemanHeader.SalesmanCode = "request_part" Then
            Dim oSalesHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(salemanHeader.ID)
            If Not IsNothing(oSalesHeader) Then
                oSalesHistory.SalesmanCode = oSalesHeader.SalesmanCode
            End If
        Else
            oSalesHistory.SalesmanCode = salemanHeader.SalesmanCode
        End If

        oSalesHistory.ChangedDate = Date.Now

        iReturn = oSalesHistoryFacade.Insert(oSalesHistory)
        If iReturn <> -1 Then
            sessHelper.SetSession("SALESHISTORY", oSalesHistory)
        Else
            sessHelper.SetSession("SALESHISTORY", Nothing)
        End If
        Return iReturn
    End Function

    Private Sub Initialize()
        ClearData()
    End Sub
    ' melakukan pengupdate, reset resign & ubah status menjadi aktif (asal dari status konfirmasi)
    Private Sub Konfirmasi(ByVal nID As Integer)
        Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(nID)
        If Not IsNothing(objSalesmanHeader) Then
            objSalesmanHeader.Status = EnumSalesmanStatus.SalesmanStatus.Aktif
            objSalesmanHeader.ResignDate = Date.Parse(strDefDate)
            objSalesmanHeader.ResignReason = ""

            Dim vr As ValidResult = New SalesmanHeaderValidation().ValidateKTPSalesmanHeader(objSalesmanHeader)

            If vr.IsValid = False Then
                MessageBox.Show(vr.Message)
                Exit Sub
            End If

            Dim nResult = New SalesmanHeaderFacade(User).Update(objSalesmanHeader)
            If nResult < 0 Then
                MessageBox.Show("Status Konfirmasi gagal")
            Else
                nResult = SaveSalesmanPartHistory(objSalesmanHeader, EnumSalesmanStatus.SalesmanStatus.Konfirmasi)
                MessageBox.Show("Status telah menjadi Aktif")
            End If
        End If
    End Sub
    ' penambahan untuk delete data - pembatalan register [update]
    Private Sub Delete(ByVal nID As Integer)
        Dim totalRow As Integer = 0
        Dim arrListSalesmanTrainingParticipant As New ArrayList
        Dim criteriasSalesmanTrainingParticipant As New CriteriaComposite(New Criteria(GetType(SalesmanTrainingParticipant), "SalesmanHeader.ID", MatchType.Exact, nID))

        ' check if data SalesmanTrainingParticipant & SalesmanUniformAssigned exist or not
        ' if exist cann't be delete
        arrListSalesmanTrainingParticipant = _SalesmanTrainingParticipantFacade.Retrieve(criteriasSalesmanTrainingParticipant)


        If arrListSalesmanTrainingParticipant.Count > 0 Then
            MessageBox.Show("Data Training Participant sudah ada untuk salesman ini, status pembatalan register tdk bisa dilakukan")
            Return
        End If

        Dim arrListSalesmanUniformAssigned As New ArrayList
        Dim criteriaSalesmanUniformAssigned As New CriteriaComposite(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanHeader.ID", MatchType.Exact, nID))

        arrListSalesmanUniformAssigned = _SalesmanUniformAssignedFacade.Retrieve(criteriaSalesmanUniformAssigned)

        If arrListSalesmanUniformAssigned.Count > 0 Then
            MessageBox.Show("Data Uniform Assigned sudah ada untuk salesman ini, status pembatalan register tdk bisa dilakukan")
            Return
        End If

        Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(nID)
        If Not objSalesmanHeader Is Nothing Then
            If objSalesmanHeader.SalesmanCode = "" And objSalesmanHeader.RegisterStatus = EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Belum_Register Then
                MessageBox.Show("Tidak bisa dibatalkan, karena belum diregister")
                Return
            End If

            If objSalesmanHeader.ResignDate <> New Date(1753, 1, 1) And objSalesmanHeader.Status = EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif Then
                MessageBox.Show("Tidak bisa dibatalkan, karena salesman sudah resign.")
                Return
            End If

            objSalesmanHeader.RegisterStatus = EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Belum_Register ' set supaya diregister
            objSalesmanHeader.SalesmanCode = ""
            objSalesmanHeader.Status = EnumSalesmanStatus.SalesmanStatus.Baru

            Dim vr As ValidResult = New SalesmanHeaderValidation().ValidateKTPSalesmanHeader(objSalesmanHeader)

            If vr.IsValid = False Then
                MessageBox.Show(vr.Message)
                Exit Sub
            End If

            Dim nResult = New SalesmanHeaderFacade(User).Update(objSalesmanHeader)
            If nResult < 0 Then
                MessageBox.Show("Status Pembatalan Register gagal")
            Else
                nResult = SaveSalesmanPartHistory(objSalesmanHeader, EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif)
                MessageBox.Show("Status Register telah dibatalkan")
            End If
        End If
    End Sub
    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)
        ' default criteria
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        ' menentukan sales unit
        If ddlSalesmanUnit.SelectedItem.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, ddlSalesmanUnit.SelectedValue))
        End If

        ' menentukan dealer kode
        If (txtDealerCode.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
        End If

        ' menentukan dealer branch kode
        If (txtDealerBranchCode.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "DealerBranch.DealerBranchCode", MatchType.InSet, "('" + Replace(txtDealerBranchCode.Text, ";", "','") + "')"))
        End If


        ' menentukan status register salesman
        If ddlStatus.SelectedValue <> 99 Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "RegisterStatus", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        ' apakah udah diajukan request ID
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "IsRequestID", MatchType.No, CInt(EnumSalesmanIsRequest.SalesmanIsRequest.Belum_Request)))

        ' menentukan nama salesman
        If txtName.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Name", MatchType.[Partial], txtName.Text))
        End If
        sessHelper.SetSession("CRITERIAS", criterias)
    End Sub
    Private Sub BindDataGrid(ByVal idxPage As Integer, Optional ByVal changePage As Boolean = False)
        Dim totalRow As Integer = 0

        

        If Not changePage Then
            Dim allData As New ArrayList
            allData = _SalesmanHeaderFacade.RetrieveByCriteria(CType(sessHelper.GetSession("CRITERIAS"), CriteriaComposite), _
                                                           CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            sessHelper.SetSession("DATADOWNLOAD", allData)
        End If

        Dim arrList As New ArrayList
        arrList = _SalesmanHeaderFacade.RetrieveByCriteria(CType(sessHelper.GetSession("CRITERIAS"), CriteriaComposite), idxPage + 1, dgResult.PageSize, totalRow, _
                                                           CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgResult.DataSource = arrList
        dgResult.VirtualItemCount = totalRow
        dgResult.DataBind()
    End Sub
    Private Sub WriteTraineeData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("TENAGA PENJUAL - Pendaftaran Tenaga Penjual")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No" & tab)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Nama Dealer" & tab)
            itemLine.Append("Kode Cabang Dealer" & tab)
            itemLine.Append("Nama Salesman" & tab)
            itemLine.Append("Tanggal Lahir" & tab)
            itemLine.Append("Status" & tab)
            itemLine.Append("Salesman ID" & tab)
            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            For Each item As SalesmanHeader In data

                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(i.ToString & tab)
                itemLine.Append(item.Dealer.DealerCode & tab)
                itemLine.Append(item.Dealer.DealerName & tab)
                If Not IsNothing(item.DealerBranch) Then
                    itemLine.Append(item.DealerBranch.DealerBranchCode & tab)
                Else
                    itemLine.Append("" & tab)
                End If
                itemLine.Append(item.Name & tab)
                itemLine.Append(item.DateOfBirth & tab)
                itemLine.Append(CType(IIf(item.RegisterStatus = " ", 0, item.RegisterStatus), EnumSalesmanRegisterStatus.SalesmanRegisterStatus).ToString.Replace("_", " ") & tab)
                itemLine.Append(item.SalesmanCode & tab)

                sw.WriteLine(itemLine.ToString())
                i = i + 1
            Next

        End If
    End Sub
    Private Sub SetDownload()
        Dim data As ArrayList = CType(sessHelper.GetSession("DATADOWNLOAD"), ArrayList)
        If IsNothing(data) Then
            MessageBox.Show("Tidak ada data yang di download")
        Else
            DoDownload(data)
        End If
    End Sub
    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "RegisterSalesman" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls

        Dim TraineeData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(TraineeData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(TraineeData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteTraineeData(sw, data)
                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub
    Private Sub CheckModule()
        Select Case Request.QueryString("Mode")
            Case "part"
                ddlSalesmanUnit.SelectedValue = EnumSalesmanUnit.SalesmanUnit.Sparepart
                lblPageTitle.Text = "PART EMPLOYEE- Generate Part Employee ID"
                lblSalesmanUnit.Text = "Employee Part Unit"
                lblNamaSalesman.Text = "Nama Employee"
        End Select
    End Sub
    Private Sub BindControlsAttribute()
        lblPopUpDealer.Attributes("onClick") = "ShowPPDealerSelection()"
        lblPopUpDealerBranch.Attributes("onClick") = "ShowPPDealerBranchSelection()"
    End Sub
#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            BindDropDownLists()
            Initialize()
            BindControlsAttribute()
            CheckModule()
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "Dealer.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            CreateCriteria(criterias)
            BindDataGrid(0)
        End If
        btnDownload.Enabled = bCekBtnDLPriv
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "Dealer.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(criterias)
        dgResult.CurrentPageIndex = 0
        BindDataGrid(0)
    End Sub
    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ClearData()
    End Sub
    Private Sub dgResult_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgResult.SortCommand
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
        dgResult.SelectedIndex = -1
        dgResult.CurrentPageIndex = 0
        BindDataGrid(dgResult.CurrentPageIndex)
    End Sub
    Private Sub dgResult_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgResult.PageIndexChanged
        dgResult.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgResult.CurrentPageIndex)
    End Sub
    Private Sub dgResult_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgResult.ItemCommand
        If e.CommandName = "Register" Then
            Update(e.Item.Cells(0).Text)        ' mengambil id salesman header ybs
            dgResult.SelectedIndex = e.Item.ItemIndex
            BindDataGrid(0)
        ElseIf e.CommandName = "Delete" Then
            Delete(e.Item.Cells(0).Text)
            ClearData()
            BindDataGrid(0)
        ElseIf e.CommandName = "Konfirmasi" Then
            Konfirmasi(e.Item.Cells(0).Text)
            ClearData()
            BindDataGrid(0)
        End If
    End Sub
    Private Sub dgResult_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResult.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim checkVal As Boolean = False
            Dim objSalesmanHeader As SalesmanHeader = e.Item.DataItem

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgResult.CurrentPageIndex * dgResult.PageSize)

            Dim lblDealerCodeNew As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            lblDealerCodeNew.Text = objSalesmanHeader.Dealer.DealerCode

            Dim lblDealerBranchCode As Label = CType(e.Item.FindControl("lblDealerBranchCode"), Label)
            If Not IsNothing(objSalesmanHeader.DealerBranch) Then
                lblDealerBranchCode.Text = objSalesmanHeader.DealerBranch.DealerBranchCode
            End If

            Dim lblDealerNameNew As Label = CType(e.Item.FindControl("lblDealerName"), Label)
            lblDealerNameNew.Text = objSalesmanHeader.Dealer.DealerName

            Dim lblNameNew As Label = CType(e.Item.FindControl("lblName"), Label)
            lblNameNew.Text = objSalesmanHeader.Name

            ' Modified by Ikhsan, 7 Agustus 2008
            ' Requested by Rina, as part of CR
            ' To Fill DateOfBirth Column with data from SalesmanHeader
            ' ---------------------------------------------------------------------------------
            Dim lblDateOfBirth As Label = CType(e.Item.FindControl("lblDateOfBirth"), Label)
            lblDateOfBirth.Text = objSalesmanHeader.DateOfBirth
            ' ---------------------------------------------------------------------------------

            Dim lblStatusNew As Label = CType(e.Item.FindControl("lblRegisterStatus"), Label)
            lblStatusNew.Text = CType(IIf(objSalesmanHeader.RegisterStatus = " ", 0, objSalesmanHeader.RegisterStatus), EnumSalesmanRegisterStatus.SalesmanRegisterStatus).ToString.Replace("_", " ")

            Dim lblSalesmanCodeNew As Label = CType(e.Item.FindControl("lblSalesmanCode"), Label)
            lblSalesmanCodeNew.Text = objSalesmanHeader.SalesmanCode

            Dim btnRegisterNew As Button = CType(e.Item.FindControl("btnRegister"), Button)
            If objSalesmanHeader.RegisterStatus = EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Sudah_Register Then
                btnRegisterNew.Enabled = False
            Else
                btnRegisterNew.Enabled = bCekBtnGridPriv
            End If

            Dim lbtnKonfirmasi As LinkButton = CType(e.Item.FindControl("lbtnKonfirmasi"), LinkButton)
            If objSalesmanHeader.Status = EnumSalesmanStatus.SalesmanStatus.Konfirmasi Then
                'IIf(checkVal, lbtnKonfirmasi.Visible = True, lbtnKonfirmasi.Visible = False)
                lbtnKonfirmasi.Visible = bCekBtnGridPriv
            Else
                lbtnKonfirmasi.Visible = False
            End If

            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dibatalkan status registernya?');")
            'IIf(checkVal, lbtnDelete.Visible = True, lbtnDelete.Visible = False)
            lbtnDelete.Visible = bCekBtnGridPriv
        End If

    End Sub
    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        SetDownload()
    End Sub
#End Region

    Private Sub UpdateJobPositionNull(ByRef objSalesmanHeader As SalesmanHeader)
        Dim jobpositionFacade As New JobPositionFacade(User)
        objSalesmanHeader.JobPosition = jobpositionFacade.GetJobPositionBySalesmanHeader(objSalesmanHeader.ID)
    End Sub

End Class
