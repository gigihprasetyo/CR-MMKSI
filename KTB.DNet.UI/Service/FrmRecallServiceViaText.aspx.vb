#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
Imports Excel
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports System.Collections.Generic
Imports System.Linq


#End Region

Public Class FrmRecallServiceViaText
    Inherits System.Web.UI.Page

    Protected WithEvents FileText As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents lblNo As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoRangka As System.Web.UI.WebControls.Label
    Protected WithEvents txtMileAge As System.Web.UI.WebControls.Label
    Protected WithEvents txtServiceDate As System.Web.UI.WebControls.Label
    Protected WithEvents txtCrTime As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents txtRecalRegNo As System.Web.UI.WebControls.Label
    Protected WithEvents txtWONo As System.Web.UI.WebControls.Label

    Private ReadOnly varSession As String = "sessFrmRecallServiceViaText"
    Private sesHelper As New SessionHelper
    Dim arlUserGroup As New ArrayList
    Dim _sesUpload As String = "frmRecall.upload"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        CheckPriv()
        If Not IsPostBack() Then
            dgRecallUpload.DataSource = New ArrayList
            dgRecallUpload.DataBind()
            Me.sesHelper.SetSession(varSession, New ArrayList)
            'Me.btnStore.Visible = False
        End If
    End Sub

    Private Sub CheckPriv()
        Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        'If IsNothing(objDealer) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=Service - Field Fix Campaign - Upload Pencapaian Field Fix Campaign")
        'End If

        'If Not SecurityProvider.Authorize(Context.User, SR.Recall_UploadService_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=Service - Field Fix Campaign - Upload Pencapaian Field Fix Campaign")
        'End If

        lblDealerCOde.Text = objDealer.DealerCode
        lblDealerName.Text = objDealer.DealerName
    End Sub

    Private Sub btnDonloadTmp_Click(sender As Object, e As EventArgs) Handles btnDonloadTmp.Click
        Dim strName As String = "Templates-Field Fix UploadDataPencapaian.xlsx"
        Response.Redirect("../downloadlocal.aspx?file=DataFile\Recall\" & strName)
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If Not Page.IsValid Then  '-- Postback validation
            Exit Sub
        End If
        Upload()

    End Sub

    Private Sub Upload()
        If (Not DataFile.PostedFile Is Nothing) AndAlso (DataFile.PostedFile.ContentLength > 0) Then
            ViewState("vsUpload") = "InsertUpload"

            Dim fileExt As String = Path.GetExtension(DataFile.PostedFile.FileName)
            If Not (fileExt.ToLower() = ".xls" OrElse fileExt.ToLower() = ".xlsx") Then
                MessageBox.Show("Hanya Menerima File Excell")
                Return
            End If

            dgRecallUpload.DataSource = New ArrayList
            dgRecallUpload.DataBind()
            dgRecallUpload.Visible = False

            Me.btnStore.Enabled = False

            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate

            Dim msg As String = ""
            Try
                Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName)
                Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & DateTime.Now.ToString("dd/MM/yyy HHmmss") & SrcFile

                imp = New SAPImpersonate(_user, _password, _webServer)

                If imp.Start() Then
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(DataFile.PostedFile.InputStream, targetFile)
                    Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
                    Dim parser As UploadRecallServiceParser = New UploadRecallServiceParser(DataFile.PostedFile.ContentType.ToString, companyCode)

                    '-- Parse data file and store result into arraylist
                    Dim arlChassisCampaign As ArrayList = CType(parser.ParseExcelNoTransaction(targetFile, "[test]", "User"), ArrayList)

                    Dim i As Integer
                    If arlChassisCampaign.Count <= 0 Then
                        MessageBox.Show("Chassis No tidak boleh Kosong")
                        btnStore.Enabled = False
                    End If
                    sesHelper.SetSession(_sesUpload, arlChassisCampaign)

                    dgRecallUpload.DataSource = arlChassisCampaign '-- Reset datagrid first
                    dgRecallUpload.CurrentPageIndex = 0
                    BindUpload()
                    dgRecallUpload.Visible = True
                End If
            Catch ex As Exception
                MessageBox.Show("Fail To Process " & ex.Message)
            Finally
                imp.StopImpersonate()
                imp = Nothing
            End Try
        Else
            MessageBox.Show("Berkas Upload tidak boleh Kosong")
        End If

    End Sub

    Private Sub BindUpload()
        Dim totalRow As Integer = 0
        Dim _arlFSChassisCampaign = New ArrayList
        Dim _arlValid As ArrayList = New ArrayList
        Dim _arlInValid As ArrayList = New ArrayList

        Try
            _arlFSChassisCampaign = sesHelper.GetSession(_sesUpload)

            If Not IsNothing(_arlFSChassisCampaign) AndAlso _arlFSChassisCampaign.Count > 0 Then
                btnStore.Enabled = True
                totalRow = _arlFSChassisCampaign.Count
                dgRecallUpload.DataSource = _arlFSChassisCampaign
                Dim iError As Integer = 0
                For Each _c As RecallService In _arlFSChassisCampaign
                    If _c.ErrorMessage <> String.Empty And _c.ErrorMessage <> "OK" Then
                        btnStore.Enabled = False
                        'Exit For
                    End If

                Next

                dgRecallUpload.VirtualItemCount = totalRow
                dgRecallUpload.DataBind()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnStore_Click(sender As Object, e As EventArgs) Handles btnStore.Click
        Try
            Dim IsUpload As Boolean = False
            If ViewState("vsUpload") = "InsertUpload" Then
                If Not IsNothing(sesHelper.GetSession(_sesUpload)) AndAlso CType(sesHelper.GetSession(_sesUpload), ArrayList).Count > 0 Then
                    IsUpload = True
                    SaveData(ViewState("vsProcess"))
                End If
            End If

            ClearData()

            'If IsUpload Then
            '    SaveData(ViewState("vsProcess"))
            'Else
            '    MessageBox.Show("Jenis service harus diisi")
            'End If

            dgRecallUpload.CurrentPageIndex = 0
            dgRecallUpload.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show(ex.Message & SR.SaveFail)
        End Try
    End Sub

    Private Sub SaveData(ByVal vsProses As String)
        Dim nResult As Integer = -1
        Try
            Dim ObjRecallServiceFacade As RecallServiceFacade
            Dim data As ArrayList = New ArrayList
            If ViewState("vsUpload") = "InsertUpload" Then
                For Each ObjRecallService As RecallService In sesHelper.GetSession(_sesUpload)
                    'cek row if exist in RecallService -> update else insert
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.RecallService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallService), "RecallChassisMaster.ChassisNo", MatchType.Exact, ObjRecallService.RecallChassisMaster.ChassisNo))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallService), "RecallChassisMaster.RecallCategory.RecallRegNo", MatchType.Exact, ObjRecallService.RecallChassisMaster.RecallCategory.RecallRegNo))

                    data = New RecallServiceFacade(User).Retrieve(criterias)
                    ObjRecallServiceFacade = New RecallServiceFacade(User)

                    If data.Count > 0 Then
                        'nResult = ObjRecallServiceFacade.Update(ObjFSChassisCampaign)
                    Else
                        
                        nResult = ObjRecallServiceFacade.Insert(ObjRecallService)
                    End If
                Next
                ViewState("vsUpload") = ""
            End If

            If nResult <= 0 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub ClearData()
        dgRecallUpload.DataSource = Nothing
        dgRecallUpload.DataBind()
    End Sub

    Private Sub dgRecallUpload_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgRecallUpload.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim objDomain As RecallService = CType(e.Item.DataItem, RecallService)

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                CType(e.Item.FindControl("lblNo"), System.Web.UI.WebControls.Label).Text = CType(e.Item.ItemIndex + 1 + (dgRecallUpload.CurrentPageIndex * dgRecallUpload.PageSize), String)
            End If

            'Dim txtDealerCode As Label = CType(e.Item.FindControl("txtDealerCode"), Label)
            'Dim txtRecalRegNo As Label = CType(e.Item.FindControl("txtRecalRegNo"), Label)

            'Dim objRecall As RecallService = New RecallServiceFacade(User).RetrieveByRM(objDomain.RecallChassisMaster.ID)
            'If Not IsNothing(objRecall.Dealer) Then
            '    txtDealerCode.Text = objRecall.Dealer.DealerCode
            'Else
            '    txtDealerCode.Text = ""
            'End If

            'If Not IsNothing(objRecall.RecallChassisMaster) Then
            '    If Not IsNothing(objRecall.RecallChassisMaster.RecallCategory) Then
            '        txtRecalRegNo.Text = objRecall.RecallChassisMaster.RecallCategory.RecallRegNo
            '    Else
            '        txtRecalRegNo.Text = ""
            '    End If
            'Else
            '    txtRecalRegNo.Text = ""
            'End If
        End If
    End Sub
End Class