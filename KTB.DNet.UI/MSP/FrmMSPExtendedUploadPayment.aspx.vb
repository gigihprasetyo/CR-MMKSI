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
Imports KTB.DNet.BusinessFacade
Imports System.Collections.Generic
Imports System.Linq


#End Region

Public Class FrmMSPExtendedUploadPayment
    Inherits System.Web.UI.Page
    Private sesHelper As New SessionHelper
    Dim arlUserGroup As New ArrayList

    Private ReadOnly varSession As String = "sessFrmMSPExUploadPayment"
    Dim _sesUpload As String = "frmMSPExPayment.upload"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        CheckPriv()
        If Not IsPostBack() Then
            dgPaymentUpload.DataSource = New ArrayList
            dgPaymentUpload.DataBind()
            Me.sesHelper.SetSession(varSession, New ArrayList)
        End If
    End Sub

    Private Sub CheckPriv()
        Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        If IsNothing(objDealer) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Service - MSP Extended - Upload Payment")
        End If

        If Not SecurityProvider.Authorize(Context.User, SR.MSPEx_UploadPayment_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Service - MSP Extended - Upload Payment")
        End If

        lblDealerCode.Text = objDealer.DealerCode
        lblDealerName.Text = objDealer.DealerName
    End Sub

    Private Sub btnDonloadTmp_Click(sender As Object, e As EventArgs) Handles btnDonloadTmp.Click
        Dim strName As String = "Template-MSP Extended UploadPayment.xlsx"
        Response.Redirect("../downloadlocal.aspx?file=DataFile\Template\MSPExt\" & strName)
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

            dgPaymentUpload.DataSource = New ArrayList
            dgPaymentUpload.DataBind()
            dgPaymentUpload.Visible = False

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
                    Dim parser As UploadMSPExPaymentParser = New UploadMSPExPaymentParser(DataFile.PostedFile.ContentType.ToString, companyCode)

                    '-- Parse data file and store result into arraylist
                    Dim arlChassisCampaign As ArrayList = CType(parser.ParseExcelNoTransaction(targetFile, "[test]", "User"), ArrayList)

                    Dim i As Integer
                    If arlChassisCampaign.Count <= 0 Then
                        MessageBox.Show("Dealer Code tidak boleh Kosong")
                        btnStore.Enabled = False
                    End If
                    sesHelper.SetSession(_sesUpload, arlChassisCampaign)

                    dgPaymentUpload.DataSource = arlChassisCampaign '-- Reset datagrid first
                    dgPaymentUpload.CurrentPageIndex = 0
                    BindUpload()
                    dgPaymentUpload.Visible = True
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
        Dim _arlMSPExPayment = New ArrayList
        Dim _arlValid As ArrayList = New ArrayList
        Dim _arlInValid As ArrayList = New ArrayList

        Try
            _arlMSPExPayment = sesHelper.GetSession(_sesUpload)

            If Not IsNothing(_arlMSPExPayment) AndAlso _arlMSPExPayment.Count > 0 Then
                btnStore.Enabled = True
                totalRow = _arlMSPExPayment.Count
                dgPaymentUpload.DataSource = _arlMSPExPayment
                Dim iError As Integer = 0
                For Each _c As MSPExPayment In _arlMSPExPayment
                    If _c.ErrorMessage <> String.Empty And _c.ErrorMessage <> "OK" Then
                        btnStore.Enabled = False
                    End If
                Next

                dgPaymentUpload.VirtualItemCount = totalRow
                dgPaymentUpload.DataBind()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dgPaymentUpload_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgPaymentUpload.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim objDomain As MSPExPayment = CType(e.Item.DataItem, MSPExPayment)

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                CType(e.Item.FindControl("lblNo"), System.Web.UI.WebControls.Label).Text = CType(e.Item.ItemIndex + 1 + (dgPaymentUpload.CurrentPageIndex * dgPaymentUpload.PageSize), String)
            End If
        End If
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

            dgPaymentUpload.CurrentPageIndex = 0
            dgPaymentUpload.SelectedIndex = -1

        Catch ex As Exception
            MessageBox.Show(ex.Message & SR.SaveFail)
        End Try
    End Sub

    Private Sub SaveData(ByVal vsProses As String)
        Dim nResult As Integer = -1
        Try
            Dim ObjMSPExPaymentFacade As New MSPExPaymentFacade(User)
            Dim ObjMSPExRegFacade As New MSPExRegistrationFacade(User)
            Dim data As ArrayList = New ArrayList
            If ViewState("vsUpload") = "InsertUpload" Then
                For Each ObjMSPExPayment As MSPExPayment In sesHelper.GetSession(_sesUpload)
                    Dim oMSPExPayment As MSPExPayment
                    oMSPExPayment = New MSPExPaymentFacade(User).RetrieveDC(ObjMSPExPayment.Dealer.DealerCode, ObjMSPExPayment.RegNumber)
                    If Not IsNothing(oMSPExPayment) Then
                        oMSPExPayment.Status = EnumMSPEx.MSPExStatusPayment.Selesai
                        oMSPExPayment.ActualTransferDate = ObjMSPExPayment.PlanTransferDate
                        oMSPExPayment.ActualTotalAmount = ObjMSPExPayment.ActualTotalAmount

                        nResult = ObjMSPExPaymentFacade.Update(oMSPExPayment)

                        If nResult >= 0 Then
                            Dim ctr As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPExPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            ctr.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPExPaymentDetail), "MSPExPayment.ID", MatchType.Exact, oMSPExPayment.ID))
                            Dim datas As ArrayList = New MSPExPaymentDetailFacade(User).Retrieve(ctr)
                            If datas.Count > 0 Then
                                For Each oMSPExPD As MSPExPaymentDetail In datas
                                    Dim oMSPExRegist As MSPExRegistration
                                    ObjMSPExRegFacade = New MSPExRegistrationFacade(User)
                                    oMSPExRegist = New MSPExRegistrationFacade(User).Retrieve(oMSPExPD.MSPExRegistration.ID)
                                    oMSPExRegist.Status = EnumMSPEx.MSPExStatusPayment.Selesai
                                    nResult = ObjMSPExRegFacade.Update(oMSPExRegist)
                                Next
                            End If
                        End If
                    Else
                        'nResult = ObjMSPExPaymentFacade.Insert(ObjMSPExPayment)
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
        dgPaymentUpload.DataSource = Nothing
        dgPaymentUpload.DataBind()
    End Sub

End Class