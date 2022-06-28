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
Imports KTB.DNet.BusinessFacade.SparePart
Imports System.Collections.Generic
Imports System.Linq


#End Region

Public Class FrmTOPSPUploadPayment
    Inherits System.Web.UI.Page

    Private ReadOnly varSession As String = "sessFrmTOPSparepartUploadPayment"
    Private sesHelper As New SessionHelper
    Dim arlUserGroup As New ArrayList
    Dim _sesUpload As String = "frmTOPSparepart.upload"

    Private Sub CheckPriv()
        Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)

        If Not SecurityProvider.Authorize(Context.User, SR.TOPSPPayment_UploadTOPSPPayment_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=TOP Saprepart- TOPSP Payment - Upload TOP Saprepart Payment")
        End If

        lblDealerCOde.Text = objDealer.DealerCode
        lblDealerName.Text = objDealer.DealerName
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        CheckPriv()
        If Not IsPostBack() Then
            dgTopPartUpload.DataSource = New ArrayList
            dgTopPartUpload.DataBind()
            Me.sesHelper.SetSession(varSession, New ArrayList)
        End If
    End Sub

    Private Sub btnDonloadTmp_Click(sender As Object, e As EventArgs) Handles btnDonloadTmp.Click
        Dim strName As String = "Templates-TOP SparepartPayment.xlsx"
        Response.Redirect("../downloadlocal.aspx?file=DataFile\Template\Parts\" & strName)
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

            dgTopPartUpload.DataSource = New ArrayList
            dgTopPartUpload.DataBind()
            dgTopPartUpload.Visible = False

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
                    Dim parser As UploadTOPSparepartPaymentParser = New UploadTOPSparepartPaymentParser(DataFile.PostedFile.ContentType.ToString, companyCode)

                    '-- Parse data file and store result into arraylist
                    Dim arlTOPSparepart As ArrayList = CType(parser.ParseExcelNoTransaction(targetFile, "[test]", "User"), ArrayList)

                    Dim i As Integer
                    If arlTOPSparepart.Count <= 0 Then
                        MessageBox.Show("Data tidak boleh Kosong")
                        btnStore.Enabled = False
                    End If
                    sesHelper.SetSession(_sesUpload, arlTOPSparepart)

                    dgTopPartUpload.DataSource = arlTOPSparepart '-- Reset datagrid first
                    dgTopPartUpload.CurrentPageIndex = 0
                    BindUpload()
                    dgTopPartUpload.Visible = True
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
        Dim _arlTOPSparepart = New ArrayList
        Dim _arlValid As ArrayList = New ArrayList
        Dim _arlInValid As ArrayList = New ArrayList

        Try
            _arlTOPSparepart = sesHelper.GetSession(_sesUpload)

            If Not IsNothing(_arlTOPSparepart) AndAlso _arlTOPSparepart.Count > 0 Then
                btnStore.Enabled = True
                totalRow = _arlTOPSparepart.Count
                dgTopPartUpload.DataSource = _arlTOPSparepart
                Dim iError As Integer = 0
                For Each _c As TOPSPTransferPayment In _arlTOPSparepart
                    If _c.ErrorMessage <> String.Empty And _c.ErrorMessage <> "OK" Then
                        btnStore.Enabled = False
                        'Exit For
                    End If
                Next

                dgTopPartUpload.VirtualItemCount = totalRow
                dgTopPartUpload.DataBind()
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

            dgTopPartUpload.CurrentPageIndex = 0
            dgTopPartUpload.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show(ex.Message & SR.SaveFail)
        End Try
    End Sub

    Private Sub ClearData()
        dgTopPartUpload.DataSource = Nothing
        dgTopPartUpload.DataBind()
    End Sub

    Private Sub SaveData(ByVal vsProses As String)
        Dim nResult As Integer = -1
        Try
            Dim ObjTOPSPTransferPaymentFacade As TOPSPTransferPaymentFacade
            Dim data As ArrayList = New ArrayList
            If ViewState("vsUpload") = "InsertUpload" Then
                For Each ObjTOPSP As TOPSPTransferPayment In sesHelper.GetSession(_sesUpload)
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TOPSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TOPSPTransferPayment), "RegNumber", MatchType.Exact, ObjTOPSP.RegNumber))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TOPSPTransferPayment), "Dealer.DealerCode", MatchType.Exact, ObjTOPSP.KodeDealer))

                    Dim dtTOP As TOPSPTransferPayment = New TOPSPTransferPaymentFacade(User).RetrieveByCode(ObjTOPSP.RegNumber, ObjTOPSP.KodeDealer)
                    If dtTOP.ID <> 0 Then
                        Dim facade As New TOPSPTransferPaymentFacade(User)
                        Dim resultUpdate = facade.UpdateTOPTransferPayment(dtTOP, ObjTOPSP)
                        'Dim resultUpdate = facade.UpdateTOPTransferPayment(dtTOP, ObjTOPSP.TransferAmount, ObjTOPSP.ActualDate, ObjTOPSP.ReffBank, ObjTOPSP.KliringDate, ObjTOPSP.TotalKliring, ObjTOPSP.DocClearing)

                        If resultUpdate Then
                            nResult = 1
                        End If
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

    Private Sub dgTopPartUpload_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgTopPartUpload.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim objDomain As TOPSPTransferPayment = CType(e.Item.DataItem, TOPSPTransferPayment)

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                CType(e.Item.FindControl("lblNo"), System.Web.UI.WebControls.Label).Text = CType(e.Item.ItemIndex + 1 + (dgTopPartUpload.CurrentPageIndex * dgTopPartUpload.PageSize), String)
            End If
        End If
    End Sub
End Class