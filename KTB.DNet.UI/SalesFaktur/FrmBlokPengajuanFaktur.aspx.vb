Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Security

Public Class FrmBlokPengajuanFaktur
    Inherits System.Web.UI.Page

    Private _sessHelper As New SessionHelper
    Private blockFaktur As String = "Block_Faktur_"
    Private unBlockFaktur As String = "unBlokFaktur_"
    Private defaultText As String = "HARAP DEALER MENGHUBUNGI PETUGAS MMKSI ( BAGIAN WHOLES SALES AND RETAIL SALES) DENGAN IBU ENUNG DI EXT. 1532 UNTUK BISA PROSES LEBIH LANJUT."

    Private sesHelper As New SessionHelper

    Private Sub CheckPrivilege()
        Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        If (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            If Not SecurityProvider.Authorize(Context.User, SR.BlockFaktur_Input_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=FinishUnit - Block faktur")
            End If
        Else
           


            Server.Transfer("../FrmAccessDenied.aspx?modulName=FinishUnit - Block faktur")


        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        If Not IsPostBack Then
            btnSimpan.Enabled = False
        End If
        InitializePopUp()
    End Sub

    Private Sub InitializePopUp()
        lblSearchChassisNumber.Attributes("onclick") = "ShowPPChassisMasterSelection();"
    End Sub

    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)
        If txtChassisNumber.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Partial, txtChassisNumber.Text.Trim))
        End If

        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "FakturStatus", MatchType.Exact, 0))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, 0))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "PendingDesc", MatchType.Partial, blockFaktur))
        criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "PendingDesc", MatchType.Partial, unBlockFaktur))
    End Sub

    Private Sub DisplayDataSearch(ByVal indexPage As Integer)
        Dim arrList As ArrayList
        Dim dataSource As New ArrayList
        Dim totalRow As Integer = 0

        If indexPage >= 0 Then
            arrList = New ChassisMasterFacade(User).RetrieveByCriteria(CType(_sessHelper.GetSession("CHASSISMASTER"), CriteriaComposite), indexPage + 1, dtgChassisMaster.PageSize, totalRow)

            For Each model As ChassisMaster In arrList
                Dim obj As New ChassisMaster
                obj.ID = model.ID
                obj.ChassisNumber = model.ChassisNumber
                obj.FakturStatus = model.FakturStatus
                Dim statusPending = ExtractPendingDesc(model.PendingDesc)
                If statusPending = blockFaktur Then
                    obj.ErrorMessage = "Block"
                Else
                    obj.ErrorMessage = "Un-Block"
                End If

                If model.PendingDesc.Contains(blockFaktur) Then
                    obj.PendingDesc = RemoveCharacterBlock(model.PendingDesc, blockFaktur)
                Else
                    obj.PendingDesc = RemoveCharacterBlock(model.PendingDesc, unBlockFaktur)
                End If

                dataSource.Add(obj)
            Next

            dtgChassisMaster.VirtualItemCount = totalRow
            dtgChassisMaster.DataSource = dataSource
            dtgChassisMaster.DataBind()
        End If
    End Sub

    Private Sub DisplayDataGrid()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        CreateCriteria(criterias)
        _sessHelper.SetSession("CHASSISMASTER", criterias)
        dtgChassisMaster.CurrentPageIndex = 0
        DisplayDataSearch(dtgChassisMaster.CurrentPageIndex)
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        DisplayDataGrid()
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If Not isEmptyText() Then
            'If isChassisNumberAlreadySave(txtChassisNumber.Text) Then
            If isChassisNumberCanBlock(txtChassisNumber.Text.Trim) Then
                Dim pendingDesc = ConcenatePendingDesc(txtRemarkNotification.Text, blockFaktur)
                InsertChassisNumber(txtChassisNumber.Text.Trim, pendingDesc)
            Else
                MessageBox.Show("Chassis Number tidak bisa di block .!")
            End If
            'Else
            '    MessageBox.Show("Chassis Number tidak bisa di block !")
            'End If
        Else
            MessageBox.Show("Mohon lengkapi semua kolom !")
        End If
    End Sub

    Private Sub dtgChassisMaster_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgChassisMaster.ItemCommand
        If e.CommandName = "Delete" Then
            Dim chassisNumber = GetChassisNumberByID(e.Item.Cells(0).Text)
            DeleteChassisNumber(chassisNumber, "")
        ElseIf e.CommandName = "View" Then
            Dim a = e.Item.Cells(0).Text
            Dim chassisNumber = GetChassisNumberByID(e.Item.Cells(0).Text)
            ViewChassisNumber(chassisNumber, False)
        ElseIf e.CommandName = "Edit" Then
            Dim chassisNumber = GetChassisNumberByID(e.Item.Cells(0).Text)
            ViewChassisNumber(chassisNumber, True)
        ElseIf e.CommandName = "UnBlock" Then
            UnBlockChassisNumber(e.Item.Cells(0).Text)
            DisplayDataGrid()
        ElseIf e.CommandName = "Block" Then
            BlockChassisNumber(e.Item.Cells(0).Text)
            DisplayDataGrid()
        End If
    End Sub

    Private Sub CleatText()
        txtChassisNumber.Text = ""
        txtRemarkNotification.Text = ""
    End Sub

    Private Sub InsertChassisNumber(ByVal chassisNumber As String, ByVal pendingDesc As String)
        Dim facade As New ChassisMasterFacade(User)
        Dim lastUpdateProfile As String = String.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd"), "15:00:00")
        Dim lprofile As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 0, 0)
        Dim result = facade.ExecuteSPChassisMasterProfile(chassisNumber, pendingDesc, lprofile)
        If result = 0 Then
            MessageBox.Show(SR.SaveSuccess)
            DisplayDataGrid()
            txtChassisNumber.Text = ""
            txtRemarkNotification.Text = ""
            btnTambah.Enabled = True
            btnSimpan.Enabled = False
        Else
            MessageBox.Show(SR.SaveFail)
        End If
    End Sub

    Private Function GetPendingDesc(ByVal chassisNumber As String) As String
        Dim pendingDesc As String = ""
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, chassisNumber))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, 0))
        Dim dataResult = New ChassisMasterFacade(User).Retrieve(criterias)

        For Each model As ChassisMaster In dataResult
            pendingDesc = model.PendingDesc
        Next
        Return pendingDesc
    End Function

    Private Function ExtractPendingDesc(ByVal str As String) As String
        Dim valuestring As String
        If str.Contains(blockFaktur) Then
            valuestring = str.Substring(0, 13)
        Else
            valuestring = str.Substring(0, 13)
        End If
        Return valuestring
    End Function

    Private Sub UnBlockChassisNumber(ByVal chassisId As Integer)
        Dim facade As New ChassisMasterFacade(User)
        Dim chassisNumber = GetChassisNumberByID(chassisId)
        Dim lastUpdateProfile As String = String.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd"), "15:00:00")
        Dim pendingDesc = GetPendingDesc(chassisNumber).Replace(blockFaktur, unBlockFaktur)

        Dim result = facade.ExecuteSPChassisMasterProfile(chassisNumber, pendingDesc, Convert.ToDateTime(lastUpdateProfile))
        If result = 0 Then
            MessageBox.Show(SR.SaveSuccess)
        Else
            MessageBox.Show(SR.SaveFail)
        End If
    End Sub

    Private Sub BlockChassisNumber(ByVal chassisId As Integer)
        Dim facade As New ChassisMasterFacade(User)
        Dim chassisNumber = GetChassisNumberByID(chassisId)
        Dim lastUpdateProfile As String = String.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd"), "15:00:00")
        Dim Pendingresult As String = GetPendingDesc(chassisNumber)
        Dim pendingDesc = Pendingresult.Replace(unBlockFaktur, blockFaktur)

        Dim result = facade.ExecuteSPChassisMasterProfile(chassisNumber, pendingDesc, lastUpdateProfile)
        If result = 0 Then
            MessageBox.Show(SR.SaveSuccess)
        Else
            MessageBox.Show(SR.SaveFail)
        End If
    End Sub

    Private Sub DeleteChassisNumber(ByVal chassisNumber As String, ByVal pendingDesc As String)
        Dim facade As New ChassisMasterFacade(User)
        Dim result = facade.ExecuteSPChassisMaster(chassisNumber, pendingDesc)
        If Not result Then
            MessageBox.Show(SR.DeleteSucces)
            txtChassisNumber.Text = ""
            txtRemarkNotification.Text = ""
            DisplayDataGrid()
        Else
            MessageBox.Show(SR.DeleteFail)
        End If
    End Sub

    Private Function GetChassisMasterId(ByVal chassisNumber As String) As Integer
        Dim chassisId As Integer
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, chassisNumber))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, 0))
        Dim dataResult = New ChassisMasterFacade(User).Retrieve(criterias)

        For Each model As ChassisMaster In dataResult
            chassisId = model.ID
        Next
        Return chassisId
    End Function

    Private Function ConcenatePendingDesc(ByVal pendingDesc As String, ByVal block As String) As String
        Dim textResult As String
        textResult = String.Format("{0}{1}", block, pendingDesc)
        Return textResult
    End Function

    Private Function isEmptyText() As Boolean
        Dim isEmpty As Boolean = False
        If txtChassisNumber.Text = "" Or txtRemarkNotification.Text = "" Then
            isEmpty = True
        End If
        Return isEmpty
    End Function

    Private Function CheckContainBlock(ByVal pendingDesc As String) As String
        Dim value As String
        If pendingDesc.Contains(blockFaktur) Then
            value = blockFaktur
        ElseIf pendingDesc.Contains(unBlockFaktur) Then
            value = unBlockFaktur
        End If
        Return value
    End Function

    Private Function RemoveCharacterBlock(ByVal pendingDesc As String, ByVal block As String) As String
        Dim strValue As String
        If Not pendingDesc = "" Then
            strValue = pendingDesc.Replace(block, "")
        End If
        Return strValue
    End Function

    Private Sub ViewChassisNumber(ByVal chassisId As String, ByVal isenable As Boolean)
        Dim dataResult As ChassisMaster = New ChassisMasterFacade(User).Retrieve(chassisId)

        If Not dataResult Is Nothing Then
            txtChassisNumber.Text = dataResult.ChassisNumber
            If dataResult.PendingDesc.Contains(blockFaktur) Then
                txtRemarkNotification.Text = RemoveCharacterBlock(dataResult.PendingDesc, blockFaktur)
            Else
                txtRemarkNotification.Text = RemoveCharacterBlock(dataResult.PendingDesc, unBlockFaktur)
            End If
        End If
        btnSimpan.Enabled = isenable
        txtChassisNumber.Enabled = isenable
        txtRemarkNotification.Enabled = isenable
    End Sub

    Private Function isChassisNumberAlreadySave(ByVal chassisNumber As String) As Boolean
        Dim isAlready As Boolean = False
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, chassisNumber))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, 0))
        Dim dataResult = New ChassisMasterFacade(User).Retrieve(criterias)

        If dataResult.count >= 1 Then
            isAlready = True
        End If
        Return isAlready
    End Function

    Private Function isChassisNumberCanBlock(ByVal chassisNumber As String) As Boolean
        Dim isCanBlock As Boolean = False
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, chassisNumber))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "FakturStatus", MatchType.Exact, 0))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, 0))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "EndCustomerID", MatchType.IsNull, ""))

        Dim dataResult = New ChassisMasterFacade(User).Retrieve(criterias)

        If dataResult.count >= 1 Then
            isCanBlock = True
        End If
        Return isCanBlock
    End Function

    Private Function GetChassisNumberByID(ByVal chassisId As Integer) As String
        Dim chassisNumber As String
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ID", MatchType.Exact, chassisId))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, 0))

        Dim chassisData = New ChassisMasterFacade(User).Retrieve(criterias)
        For Each model As ChassisMaster In chassisData
            chassisNumber = model.ChassisNumber
        Next
        Return chassisNumber
    End Function

    Private Sub dtgChassisMaster_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgChassisMaster.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As ChassisMaster = CType(e.Item.DataItem, ChassisMaster)

            'CType(e.Item.FindControl("lbtnUnblock"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan diAktifkan?');")
            'CType(e.Item.FindControl("lbtnBlock"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan diNonAktifkan?');")
            Dim UnBlock As LinkButton = CType(e.Item.FindControl("lbtnUnblock"), LinkButton) 'ceklis
            Dim Block As LinkButton = CType(e.Item.FindControl("lbtnBlock"), LinkButton) 'silang

            Dim pendingDesc = GetPendingDesc(RowValue.ChassisNumber)
            If CheckContainBlock(pendingDesc) = blockFaktur Then
                UnBlock.Visible = True
                Block.Visible = False
            ElseIf CheckContainBlock(pendingDesc) = unBlockFaktur Then
                Block.Visible = True
                UnBlock.Visible = False
            End If

            If RowValue.FakturStatus <> "0" Then
                UnBlock.Visible = False
            End If
        End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgChassisMaster.CurrentPageIndex * dtgChassisMaster.PageSize)
        End If
    End Sub

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        txtRemarkNotification.Text = defaultText
        btnSimpan.Enabled = True
        btnTambah.Enabled = False
    End Sub
End Class