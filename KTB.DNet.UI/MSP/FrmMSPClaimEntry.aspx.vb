#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports System.Web.UI.WebControls
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports System.Text
Imports System.IO
Imports System.Collections.Generic
#End Region

Public Class FrmMSPClaimEntry
    Inherits System.Web.UI.Page

#Region "Var"
    Dim _sessHelper As SessionHelper = New SessionHelper
    Private m_bPMUpdate_Privilege As Boolean = False
    Private m_bPMRelease_Privilege As Boolean = False
    Private m_bPMSave_Privilege As Boolean = False
    Private _strMSPStatus As String = String.Empty
    Const _strMode As String = "Mode"
    Private _tempObjPMHeader As PMHeader
    Const _strAllowBackward As String = "MSPBackwardInput"
    Private _strSesName As String = "SesName"
#End Region


#Region "Priv FUnction"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.MSPClaimList_view_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PERIODICAL MAINTENANCE - Data PM")
        End If
    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)

        Dim totRow As Integer = 0

        Dim obDealer As Dealer = Session("Dealer")


        If Not IsNothing(obDealer) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPClaim), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPClaim), "Status", MatchType.Exact, CInt(EnumStatusMSP.Status.Baru)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPClaim), "Dealer.ID", MatchType.Exact, obDealer.ID))

            '_sessHelper.SetSession("SortViewMSPClaim", criterias)
            dgEntryPM.DataSource = New MSPClaimFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dgEntryPM.PageSize, totRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dgEntryPM.VirtualItemCount = totRow
            If (indexPage = 0) Then
                dgEntryPM.CurrentPageIndex = indexPage
            End If
            Dim al As ArrayList = dgEntryPM.DataSource
            Session(ViewState(_strSesName)) = al
            dgEntryPM.DataBind()

        End If




    End Sub

    Private Sub clearInput()
        txtChassisMaster.Text = ""
        txtEngineNo.Text = ""
        txtKM.Text = ""
        txtChassisMaster.ReadOnly = False

    End Sub

    Private Function isValid(ByRef msg As String) As Boolean
        msg = String.Empty
        Dim result As Boolean = True
        If txtChassisMaster.Text.Trim = "" Then
            msg = "Masukan No Rangka"
            result = False
        Else

            If txtEngineNo.Text.Trim = "" Then
                msg = "Masukan No Mesin"
                result = False
            Else

                Dim chm As New ChassisMaster
                chm = New ChassisMasterFacade(User).Retrieve(txtChassisMaster.Text.Trim())
                If IsNothing(chm) OrElse chm.ID = 0 Then
                    msg = "No Rangka tidak terdaftar"
                    result = False
                ElseIf chm.EngineNumber <> txtEngineNo.Text.Trim() Then

                    msg = "No Mesin tidak sesuai"
                    result = False
                End If
            End If

        End If

        If calTglService.Value > DateTime.Now Then
            msg = msg + vbCrLf + "Tanggal Service tidak boleh Melebihi Hari ini! "
            result = False
        End If
        If txtKM.Text.Trim() = "0" Then
            txtKM.Text = "0"
        End If

        If txtKM.Text.Trim() = "" OrElse Not IsNumeric(txtKM.Text.Trim()) Then
            msg = msg + vbCrLf + " KM Harus di Isi Angka "
            result = False
        ElseIf CInt(txtKM.Text.Trim()) <= 0 Then

            msg = msg + vbCrLf + " KM Harus di Isi Angka "
            result = False
        Else
            Dim objPMKind As PMKindFacade = New PMKindFacade(User)
            If objPMKind.IsPMKindFoundForMSP(CInt(txtKM.Text.Trim)) = False Then
                msg = msg + vbCrLf + " KM tidak Terdaftar "
                result = False
            End If

        End If

        Return result
    End Function


    Private Function IsChassisAndKMEquals(ByVal parPmKindCode As String, Optional ByVal parID As Integer = 0) As Boolean
        '---cek Chassis yang sama dengan KM yang sama
        Dim StrFunction As String = String.Format("(SELECT ID FROM dbo.fn_MSPClaimChecking('{0}','{1}',{2}))", txtChassisMaster.Text.Trim(), parPmKindCode, parID.ToString())
        Dim checkRuleChassis_No As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPClaim), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        checkRuleChassis_No.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPClaim), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))
        checkRuleChassis_No.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPClaim), "ID", MatchType.InSet, StrFunction))
        Dim arlRuleChassis As ArrayList = New MSPClaimFacade(User).Retrieve(checkRuleChassis_No)
        If arlRuleChassis.Count > 0 Then
            Return True
        Else
            Return False
        End If

        'Dim objMSPClaim As MSPClaim = arlRuleChassis(0)
        'If txtChassisMaster.Text.Trim = objMSPClaim.ChassisMaster.ChassisNumber And CInt(txtKM.Text) = objMSPClaim.StandKM Then
        '   Return True
        'Else
        '   Return False
        'End If
    End Function


    Private Function IsExistCodeForInsert(ByVal ChassisID As Integer, ByVal PMKindID As Integer, ByRef strMsg As String, Optional ByVal MSpClaimID As Integer = 0) As Boolean

        strMsg = ""
        Dim criterias As New CriteriaComposite(New Criteria(GetType(MSPClaim), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(MSPClaim), "ChassisMaster.ID", MatchType.Exact, ChassisID))
        criterias.opAnd(New Criteria(GetType(MSPClaim), "PMKind.ID", MatchType.Exact, PMKindID))

        If MSpClaimID > 0 Then
            criterias.opAnd(New Criteria(GetType(MSPClaim), "ID", MatchType.No, MSpClaimID))
        End If

        Dim TestExist As ArrayList = New MSPClaimFacade(User).Retrieve(criterias)
        If TestExist.Count > 0 Then
            Dim obClaim As MSPClaim = CType(TestExist(0), MSPClaim)
            strMsg = String.Format("Sudah Ada Claim Untuk No Rangka {0} \n Pada KM {1} - KindCode {2}", obClaim.ChassisMaster.ChassisNumber, obClaim.StandKM.ToString("#,##0"), obClaim.PMKind.KindCode)
            Return True
        Else
            Return False
        End If
    End Function

    Private Function LockBackward() As Boolean
        Dim result As Boolean = False
        Try
            result = CBool(KTB.DNet.Lib.WebConfig.GetString(_strAllowBackward))
        Catch ex As Exception

        End Try

        Return result
    End Function

    Private Function IsBackWardPMExist(ByVal ChassisID As Integer, ByVal obPMKIND As PMKind, ByRef strMsg As String, Optional ByVal MSpClaimID As Integer = 0) As Boolean

        strMsg = ""
        Dim criterias As New CriteriaComposite(New Criteria(GetType(MSPClaim), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(MSPClaim), "ChassisMaster.ID", MatchType.Exact, ChassisID))
        criterias.opAnd(New Criteria(GetType(MSPClaim), "PMKind.KM", MatchType.Greater, obPMKIND.KM))

        If MSpClaimID > 0 Then
            criterias.opAnd(New Criteria(GetType(MSPClaim), "ID", MatchType.No, MSpClaimID))
        End If

        Dim TestExist As ArrayList = New MSPClaimFacade(User).Retrieve(criterias)
        If TestExist.Count > 0 Then
            Dim obClaim As MSPClaim = CType(TestExist(0), MSPClaim)
            strMsg = String.Format("Sudah Ada Claim lebih besar Untuk No Rangka {0} \n Pada KM {1} - KindCode {2}", obClaim.ChassisMaster.ChassisNumber, obClaim.StandKM.ToString("#,##0"), obClaim.PMKind.KindCode)
            Return True
        Else
            Return False
        End If
    End Function


    Private Sub InsertobjMSPClaim()
        Dim objMSPFacade As MSPClaimFacade = New MSPClaimFacade(User)
        Dim nResult As Integer = -1
        Dim objPMDealer As New Dealer
        Dim objMSPClaim As MSPClaim = New MSPClaim

        Dim obDealer As Dealer = Session("Dealer")



        objMSPClaim.Dealer = obDealer

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, txtChassisMaster.Text))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "EngineNumber", MatchType.Exact, txtEngineNo.Text))
        Dim ChassisColl As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)

        If ChassisColl.Count > 0 Then
            Dim _objPMKind As PMKind = New PMKindFacade(User).PMKindForMSP(txtKM.Text)

            objMSPClaim.ChassisMaster = ChassisColl(0)
            objMSPClaim.StandKM = Integer.Parse(txtKM.Text)
            objMSPClaim.ServiceDate = calTglService.Value
            objMSPClaim.Status = EnumStatusMSP.Status.Baru
            objMSPClaim.VisitType = ddlVisitType.SelectedValue
            objMSPClaim.ClaimDate = DateTime.Now
            objMSPClaim.Remarks = If(Not IsNothing(_tempObjPMHeader), _tempObjPMHeader.Remarks, String.Empty)
            objMSPClaim.PMKind = _objPMKind
            objMSPClaim.MSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(CInt(_tempObjPMHeader.MSPRegistrationHistoryID))

            Dim _arrPMDetails As New ArrayList
            Dim strMSG As String = ""
            'Validasi Check PM yang sama
            If Not IsExistCodeForInsert(ChassisColl(0).ID, _objPMKind.ID, strMSG) Then
                strMSG = ""
                If LockBackward() AndAlso IsBackWardPMExist(ChassisColl(0).ID, _objPMKind, strMSG) Then
                    MessageBox.Show(strMSG)
                    Return
                End If


                nResult = objMSPFacade.Insert(objMSPClaim)
                If nResult = -1 Then
                    MessageBox.Show("Simpan Gagal")
                Else
                    BindDatagrid(0)
                    MessageBox.Show("Simpan Sukses")

                    clearInput()

                End If
            Else
                MessageBox.Show(strMSG)
                Return
            End If
        End If
    End Sub

    Private Sub UpdateObjMSPClaim(ByVal objMSPClaim As MSPClaim)
        Dim objMSPFacade As MSPClaimFacade = New MSPClaimFacade(User)
        Dim nResult As Integer = -1
        Dim objPMDealer As New Dealer


        Dim obDealer As Dealer = Session("Dealer")



        objMSPClaim.Dealer = obDealer

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, txtChassisMaster.Text))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "EngineNumber", MatchType.Exact, txtEngineNo.Text))
        Dim ChassisColl As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)

        If ChassisColl.Count > 0 Then
            Dim _objPMKind As PMKind = New PMKindFacade(User).PMKindForMSP(txtKM.Text)

            objMSPClaim.ChassisMaster = ChassisColl(0)
            objMSPClaim.StandKM = Integer.Parse(txtKM.Text)
            objMSPClaim.ServiceDate = calTglService.Value
            objMSPClaim.Status = EnumStatusMSP.Status.Baru
            objMSPClaim.VisitType = ddlVisitType.SelectedValue
            objMSPClaim.ClaimDate = DateTime.Now
            objMSPClaim.Remarks = If(Not IsNothing(_tempObjPMHeader), _tempObjPMHeader.Remarks, String.Empty)
            objMSPClaim.PMKind = _objPMKind
            objMSPClaim.MSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(CInt(_tempObjPMHeader.MSPRegistrationHistoryID))

            Dim _arrPMDetails As New ArrayList
            Dim strMSG As String = ""
            'Validasi Check PM yang sama
            If Not IsExistCodeForInsert(ChassisColl(0).ID, _objPMKind.ID, strMSG, objMSPClaim.ID) Then


                strMSG = ""
                If LockBackward() AndAlso IsBackWardPMExist(ChassisColl(0).ID, _objPMKind, strMSG, objMSPClaim.ID) Then
                    MessageBox.Show(strMSG)
                    Return
                End If

                nResult = objMSPFacade.Update(objMSPClaim)
                If nResult = -1 Then
                    MessageBox.Show("Simpan Gagal")
                Else
                    BindDatagrid(0)
                    MessageBox.Show("Simpan Sukses")

                    clearInput()

                End If
            Else
                MessageBox.Show("Klaim sudah ada")
                Return
            End If
        End If
    End Sub

    Private Sub DeleteMSPClaim(ByVal ID As Integer)
        Dim objMSPClaimFacade As New MSPClaimFacade(User)
        Dim objMSPClaim As MSPClaim = objMSPClaimFacade.Retrieve(ID)
        objMSPClaimFacade.Delete(objMSPClaim)
        MessageBox.Show("Item Claim sudah dihapus")
        BindDatagrid(0)
        clearInput()

    End Sub

    Private Sub ActivateControlEdit(ByVal ID As Integer)
        Dim objMSPClaim As MSPClaim = New MSPClaimFacade(User).Retrieve(ID)
        txtChassisMaster.Text = objMSPClaim.ChassisMaster.ChassisNumber
        txtEngineNo.Text = objMSPClaim.ChassisMaster.EngineNumber
        txtKM.Text = objMSPClaim.StandKM.ToString
        calTglService.Value = objMSPClaim.ServiceDate
        ddlVisitType.SelectedValue = objMSPClaim.VisitType
        Dim _temp As String = String.Empty

        ViewState("ID") = ID

    End Sub

#End Region

#Region "Release"
    Private Sub TransferToSAP(ByVal objPMColl As ArrayList, ByVal type As String, Optional ByVal parCategory As String = "")
        Dim NewArl As ArrayList = New ArrayList
        Dim sb As StringBuilder = New StringBuilder
        Dim Product As String = "mmc"
        Dim filename = String.Format("{0}{1}{2}{3}", "statusPM", Date.Now.ToString("ddMMyyyyHHmmss"), "_" & parCategory, ".txt") ' "_" & Product.ToLower()
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder").ToString & "\Service\PM\" & Product & "\" & filename    '-- Destination file to local"
        Dim HistoryFolderSAP As String = String.Empty
        'Start  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180122
        If type = "msp" Then
            filename = "CLMSP" & Date.Now.ToString("ddMMyyyyHHmmss") & "_MmC.txt"
            DestFile = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder").ToString & "\Service\MSP\Claim\" & filename
            HistoryFolderSAP = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder").ToString & "\Service\MSP\Claim\History"
        End If
        'End  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180122

        For Each item As MSPClaim In objPMColl

            'Start  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180122
            If type.ToLower() = "msp" Then
                Dim MP As New MSPClaim
                MP = New MSPClaimFacade(User).Retrieve(item.ID)
                If (CInt(MP.Status) = CInt(EnumStatusMSP.Status.Baru)) Then
                    MP.Status = CInt(EnumStatusMSP.Status.Proses)
                    MP.ReleaseDate = Today
                    MP.Status = EnumStatusMSP.Status.Proses
                    Dim intRes As Integer = New MSPClaimFacade(User).Update(MP)
                    If intRes > 0 Then

                        sb.Append(MP.ChassisMaster.ChassisNumber & Chr(9) & MP.ServiceDate.ToString("ddMMyyyy") & Chr(9) & MP.StandKM & Chr(9) & MP.Dealer.DealerCode & Chr(9) & "PM" & MP.PMKind.KindCode & Chr(9) & MP.ReleaseDate.ToString("ddMMyyyy") & Chr(9) & MP.VisitType.ToString() & Chr(9) & MP.ClaimNumber & Chr(9) & vbNewLine) ' & Chr(13) & Chr(10))
                    End If
                End If
             


            End If

        Next

     


        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        success = imp.Start()


        Dim objFileStream As New FileStream(DestFile, FileMode.Append, FileAccess.Write)
        Dim objStreamWriter As New StreamWriter(objFileStream)

        Try
            If success Then
                Try
                    Dim DestFileInfo As New FileInfo(DestFile)
                    If Not DestFileInfo.Directory.Exists Then
                        Directory.CreateDirectory(DestFileInfo.DirectoryName)
                    End If

                    If HistoryFolderSAP <> String.Empty Then
                        Dim directoryHistory As New DirectoryInfo(HistoryFolderSAP)
                        If Not directoryHistory.Exists Then
                            Directory.CreateDirectory(HistoryFolderSAP)
                        End If
                    End If

                    objStreamWriter.WriteLine(sb)
                   
                Catch ex As Exception
                    MessageBox.Show("Gagal kirim file ke SAP.")
                End Try
            Else
                MessageBox.Show("Gagal akses file ke SAP.")
            End If
        Catch ex As Exception

        Finally
            objStreamWriter.Close()
            objFileStream.Close()
            imp.StopImpersonate()
            imp = Nothing
        End Try

    End Sub

    Private Function GetProductCategoryCode(ByVal aPMIs As ArrayList) As String
        Dim product As String = ""

        For Each oPMI As PMHeader In aPMIs
            If product = "" Then
                product = oPMI.ChassisMaster.Category.ProductCategory.Code
            Else
                If product <> oPMI.ChassisMaster.Category.ProductCategory.Code Then
                    Return ""
                End If
            End If
        Next
        Return product
    End Function

    Private Function GetCheckedPMItem() As ArrayList

        Dim arlCheckedPMItem As ArrayList = New ArrayList
        Dim nIndeks As Integer
        For Each dtgItem As DataGridItem In dgEntryPM.Items
            nIndeks = dtgItem.ItemIndex
            Dim objPM As MSPClaim = New MSPClaimFacade(User).Retrieve(CInt(dtgItem.Cells(0).Text)) 'CType(CType(dgEntryPM.DataSource, ArrayList)(nIndeks), MSPClaim)
            If CType(dtgItem.FindControl("cbSelect"), CheckBox).Checked Then
                If Not IsNothing(objPM) AndAlso objPM.Status = CInt(EnumStatusMSP.Status.Baru) Then

                    arlCheckedPMItem.Add(objPM)

                End If
            End If
        Next
        Return arlCheckedPMItem
    End Function


#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        If Not IsPostBack() Then
            ViewState(_strSesName) = Guid.NewGuid.ToString()
            ViewState(_strMode) = CInt(enumMode.Mode.NewItemMode)
            Session(ViewState(_strSesName)) = Nothing
            dgEntryPM.AllowSorting = False
            If Not IsNothing(Session("DEALER")) Then
                ViewState("currentSortColumn") = "LastUpdateTime"
                ViewState("currentSortDirection") = Sort.SortDirection.DESC
                Dim obDealer As Dealer = Session("Dealer")
                lblDealerCode.Text = obDealer.DealerCode
                lblDealerName.Text = obDealer.DealerName

                BindDatagrid(0)
            End If
        End If

    End Sub

    Private Sub btnSimpan_ServerClick(sender As Object, e As EventArgs) Handles btnSimpan.ServerClick

        Dim msg As String = ""
        Dim _strMSPStatus As String = ""
        If Not isValid(msg) Then
            'MessageBox.Show(msg)
            hdnMsg.Value = msg
        Else

            Dim _objPMKind As PMKind = New PMKindFacade(User).PMKindForMSP(txtKM.Text)
            _tempObjPMHeader = New PMHeader
            _tempObjPMHeader.PMKind = _objPMKind

            Dim _MSPHelper As New MSPHelper
            _strMSPStatus = _MSPHelper.CheckStatusMSP(txtChassisMaster.Text, _objPMKind.ID, _tempObjPMHeader, If(String.IsNullOrEmpty(txtKM.Text), 0, CInt(txtKM.Text)), calTglService.Value)

            If _strMSPStatus = "" AndAlso _tempObjPMHeader.IsValidMSP Then
                If ViewState(_strMode) = CInt(enumMode.Mode.NewItemMode) Then
                    InsertobjMSPClaim()
                Else
                    Dim obClaim As MSPClaim = New MSPClaimFacade(User).Retrieve(ViewState("ID"))
                    UpdateObjMSPClaim(obClaim)
                End If
            Else
                If _strMSPStatus <> "" Then
                    '  MessageBox.Show(_strMSPStatus)
                    hdnMsg.Value = _strMSPStatus
                Else
                    ' MessageBox.Show("Tidak Terdaftar Sebagai MSP")
                    hdnMsg.Value = "Tidak Terdaftar Sebagai MSP"
                End If

            End If

        End If
    End Sub

    Private Sub dgEntryPM_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgEntryPM.ItemCommand
        Select Case (e.CommandName)
            Case "Edit"
                ViewState.Add(_strMode, CInt(enumMode.Mode.EditMode))
                ActivateControlEdit(Integer.Parse(e.Item.Cells(0).Text))
            Case "Delete"
                DeleteMSPClaim(Integer.Parse(e.Item.Cells(0).Text))
        End Select
    End Sub

    Private Sub dgEntryPM_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgEntryPM.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            Dim oMClaim As MSPClaim = e.Item.DataItem

            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim lblPMKind As Label = e.Item.FindControl("lblPMKind")
            Dim lblMSPDescription As Label = e.Item.FindControl("lblMSPDescription")

            lblNo.Text = (e.Item.ItemIndex + 1) + (dgEntryPM.CurrentPageIndex * dgEntryPM.PageSize)
            lblPMKind.Text = oMClaim.PMKind.KindCode
            lblMSPDescription.Text = oMClaim.Remarks

            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                Dim strMsg As String = "Yakin Data dengan nomor rangka " & CType(e.Item.FindControl("lblChassisNo"), Label).Text & "  ini akan dihapus? "
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & strMsg & "');")
            End If
        End If
    End Sub

    Private Sub btnRelease_Click(sender As Object, e As EventArgs) Handles btnRelease.Click
        Dim bcheck As Boolean = False
        Dim success As Boolean = False

        dgEntryPM.DataSource = CType(Session(ViewState(_strSesName)), ArrayList)
        For Each dtgItem As DataGridItem In dgEntryPM.Items
            If CType(dtgItem.FindControl("cbSelect"), CheckBox).Checked Then ' If CType(dtgItem.Cells(0).FindControl("cbSelect"), CheckBox).Checked Then
                bcheck = True
                Exit For
            End If
        Next
        Try
            If bcheck Then
                Dim CheckedPMItemColl As ArrayList = New ArrayList
                CheckedPMItemColl = GetCheckedPMItem()



                Dim aPMA As New ArrayList
                Dim aPMB As New ArrayList
                Dim arrMSP As New ArrayList

                If CheckedPMItemColl.Count > 0 Then
                    For Each ObjPMHeader As MSPClaim In CheckedPMItemColl
                        ObjPMHeader.ReleaseDate = Today
                        ObjPMHeader.Status = EnumStatusMSP.Status.Proses

                        arrMSP.Add(ObjPMHeader)

                    Next

                    If 1 = 1 Then

                        'Start  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20171206
                        If (arrMSP.Count > 0) Then
                            TransferToSAP(arrMSP, "msp", "mmc")
                        End If
                        'End  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20171206

                        MessageBox.Show("Update Rilis Sukses")
                    Else
                        MessageBox.Show("Update Rilis gagal")
                    End If
                End If
            Else
                MessageBox.Show("Record Claim belum dipilih !")
            End If
            BindDatagrid(0)
            Dim strScript As String
            strScript = "<script>document.all.txtChassisMaster.focus();</script>"
            Page.RegisterStartupScript("", strScript)
        Catch ex As Exception
            BindDatagrid(0)
            MessageBox.Show("Gagal Kirim")
        End Try
    End Sub

    Private Sub btnBatal_ServerClick(sender As Object, e As EventArgs) Handles btnBatal.ServerClick
        ViewState.Add(_strMode, CInt(enumMode.Mode.NewItemMode))
        clearInput()
        BindDatagrid(0)

    End Sub

    Private Sub dgEntryPM_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgEntryPM.PageIndexChanged
        dgEntryPM.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dgEntryPM.CurrentPageIndex)
    End Sub

    Private Sub dgEntryPM_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgEntryPM.SortCommand
        If CType(ViewState("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currentSortColumn") = e.SortExpression
            ViewState("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dgEntryPM.SelectedIndex = -1
        dgEntryPM.CurrentPageIndex = 0
        BindDatagrid(dgEntryPM.CurrentPageIndex)
    End Sub
End Class