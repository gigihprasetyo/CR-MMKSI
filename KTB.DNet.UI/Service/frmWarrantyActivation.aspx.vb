Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessValidation
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.Text
Imports System.Configuration
Imports System.IO
Imports SpireDoc = Spire.Doc
Imports SpireDocument = Spire.Doc.Document
Imports System.Collections.Generic
Imports OfficeOpenXml
Imports System.Linq

Public Class frmWarrantyActivation
    Inherits System.Web.UI.Page
    Private _view As Boolean = False
    Private _sessHelper As New SessionHelper
    Private _strSessSearch As String = "CRITERIAS"
    Private _arlWarrantyActivation As ArrayList = New ArrayList
    Dim crt As CriteriaComposite
    Dim arr As ArrayList
    Dim sorts As SortCollection
    Dim TempFile As String
    Dim SrcFile As String
    Dim objWA As WarrantyActivation = New WarrantyActivation
    Private m_bInputPrivilege As Boolean = False
    Private isDealerPiloting As Boolean = False
    Dim objDealer As New Dealer
    Dim dealerFacade As KTB.DNet.BusinessFacade.General.DealerFacade = New KTB.DNet.BusinessFacade.General.DealerFacade(User)
    Private oLoginUser As New UserInfo

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        objDealer = CType(_sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(_sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        If Not IsPostBack Then
            BindInformationType(ddlStatus)
            txtDealerCode.Attributes.Add("readonly", True)
            ResetControl()
            BindWarrantyActivation()
        End If
    End Sub

    Private Sub CheckPrivilege()
        m_bInputPrivilege = SecurityProvider.Authorize(Context.User, SR.AktivasiWarranti_View_Privilage)
        isDealerPiloting = TCHelper.GetActiveTCResult(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.PilotingWA))
        If Not m_bInputPrivilege Or (Not isDealerPiloting And Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Service - Umum - Warranty Activation")
        End If
    End Sub

    Private Sub ResetControl()
        txtDealerCode.Text = ""
        ccTglPDIAkhir.Value = Nothing 'New Date().Now.Date
        ccTglPDIMulai.Value = Nothing 'New Date().Now.Date
        ccTglPermintaan.Value = Nothing 'New Date().Now.Date
        ccTglPermintaanEnd.Value = Nothing 'New Date().Now.Date
        ccTglPKTAkhir.Value = Nothing 'New Date().Now.Date
        ccTglPKTMulai.Value = Nothing 'New Date().Now.Date
        ddlStatus.SelectedIndex = 0
        _sessHelper.SetSession("WarrantyActivation", Nothing)
    End Sub

    Private Sub BindInformationType(ByVal ddl As DropDownList)
        Dim arrList As ArrayList = EnumWarrantyActivationOp.RetriveInformationType(True)
        For Each item As EnumInformationTypeOp In arrList
            Dim listItem As New ListItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
        ddl.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs)
        _arlWarrantyActivation = _sessHelper.GetSession("sessWarrantyActivation")

        dgWarrantyActivation.CurrentPageIndex = 0
        BindWarrantyActivation()
    End Sub

    Private Function CheckDealerGroup() As Dealer
        Dim dEaler As Dealer
        Dim arrExist As ArrayList = New ArrayList
        Dim crit As New CriteriaComposite(New Criteria(GetType(Dealer), "ID", MatchType.Exact, objDealer.ID))
        arrExist = dealerFacade.Retrieve(crit)

        If arrExist.Count > 0 Then
            dEaler = CType(arrExist(0), Dealer)
        Else
            dEaler = Nothing
        End If

        Return dEaler
    End Function

    Private Sub BindWarrantyActivation(Optional indexPage As Integer = 0)
        Dim totalRow As Integer = 0
        _arlWarrantyActivation = New ArrayList
        Dim _arlValid As ArrayList = New ArrayList
        Dim _arlInValid As ArrayList = New ArrayList
        Dim intStatus As Integer = 0
        Try
            _arlWarrantyActivation = _sessHelper.GetSession("sessWarrantyActivation")
            Dim criterias As New CriteriaComposite(New Criteria(GetType(WarrantyActivation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "PDI.PDIStatus", MatchType.InSet, "('2','3')"))

            If Not IsNothing(objDealer.DealerGroup) Then
                'filter dealer group
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "PDI.Dealer.DealerGroup", MatchType.Exact, objDealer.DealerGroup.ID))
            End If

            If ccTglPermintaan.Value.ToString <> "01/01/0001 0:00:00" And ccTglPermintaanEnd.Value.ToString <> "01/01/0001 0:00:00" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "WADate", MatchType.GreaterOrEqual, ccTglPermintaan.Value))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "WADate", MatchType.Lesser, ccTglPermintaanEnd.Value.AddDays(1)))
            End If

            If ccTglPDIMulai.Value.ToString <> "01/01/0001 0:00:00" And ccTglPDIAkhir.Value.ToString <> "01/01/0001 0:00:00" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "PDI.PDIDate", MatchType.GreaterOrEqual, ccTglPDIMulai.Value))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "PDI.PDIDate", MatchType.Lesser, ccTglPDIAkhir.Value.AddDays(1)))
            End If

            If ccTglPKTMulai.Value.ToString <> "01/01/0001 0:00:00" And ccTglPKTAkhir.Value.ToString <> "01/01/0001 0:00:00" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "ChassisMasterPKT.PKTDate", MatchType.GreaterOrEqual, ccTglPKTMulai.Value))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "ChassisMasterPKT.PKTDate", MatchType.Lesser, ccTglPKTAkhir.Value.AddDays(1)))
            End If

            If txtDealerCode.Text <> "" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "PDI.Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Split("-")(0).Trim))
            End If

            If txtCustName.Text <> "" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "CustomerName", MatchType.Exact, "'" + txtCustName.Text + "'"))
            End If

            If txtNomorPolisi.Text <> "" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "PlateNumber", MatchType.Exact, "'" + txtNomorPolisi.Text + "'"))
            End If

            If txtNomorRangka.Text <> "" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "ChassisMaster.ChassisNumber", MatchType.Exact, txtNomorRangka.Text))
            End If

            If ddlStatus.SelectedIndex > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "Status", MatchType.Exact, ddlStatus.SelectedValue))
            End If


            Dim objWAAl = New WarrantyActivationFacade(User).Retrieve(criterias, sorts)
            _sessHelper.SetSession("WarrantyActivationAl", objWAAl)
            Dim data As ArrayList = New WarrantyActivationFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dgWarrantyActivation.PageSize, totalRow, ViewState.Item("SortCol"), ViewState.Item("SortDirection"))
            If data.Count = 0 Then
                dgWarrantyActivation.CurrentPageIndex = 0
            Else
                dgWarrantyActivation.CurrentPageIndex = indexPage
            End If

            dgWarrantyActivation.DataSource = data
            dgWarrantyActivation.VirtualItemCount = totalRow
            dgWarrantyActivation.DataBind()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            lblMessage.Text = ""
        End Try
    End Sub

    Private Sub bindItemDgWA(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lblTanggalPKT As Label = CType(e.Item.FindControl("lblTanggalPKT"), Label)
            Dim lbDownload As LinkButton = CType(e.Item.FindControl("lbDownload"), LinkButton)

            objWA = CType(e.Item.DataItem, WarrantyActivation)
            lblNo.Text = (e.Item.ItemIndex + 1) + (dgWarrantyActivation.CurrentPageIndex * dgWarrantyActivation.PageSize)
            If IsNothing(objWA.ChassisMasterPKT) Then
                lblTanggalPKT.Text = ""
            End If

            lblStatus.Text = EnumWarrantyActivation.GetStringInformationType(objWA.Status)

            lbDownload.Visible = objWA.Status = EnumWarrantyActivation.WAStatus.Aktif

        End If

    End Sub

    Protected Sub dgWarrantyActivation_ItemCommand(source As Object, e As DataGridCommandEventArgs)
        Dim hdID As HiddenField = CType(e.Item.FindControl("hdID"), HiddenField)
        Select Case e.CommandName
            Case "download"
                Dim _SANstring As String = KTB.DNet.Lib.WebConfig.GetValue("SAN")
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204

                Dim waFacade As New WarrantyActivationFacade(User)
                Dim objWA As WarrantyActivation = waFacade.Retrieve(CInt(hdID.Value))
                Dim waValidation As WarrantyActivationValidation = New WarrantyActivationValidation(_SANstring, _user, _password, _webServer)
                Dim filename As String = objWA.FileName
                Dim validationResults As List(Of ValidResult) = New List(Of ValidResult)
                Dim isUpdate As Boolean = False

                If waValidation.GenerateCertificate(objWA, isUpdate, filename, Nothing, validationResults, False, True) Then
                    If isUpdate Then
                        objWA.FileName = filename
                        waFacade.Update(objWA)
                    End If
                End If

                If validationResults.Count = 0 Then
                    Response.Redirect("../download.aspx?file=" & filename)
                Else
                    MessageBox.Show(validationResults.FirstOrDefault().Message)
                End If
        End Select
    End Sub

    Protected Sub dgWarrantyActivation_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
        bindItemDgWA(e)
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs)
        Dim totalRow As Integer = 0
        _arlWarrantyActivation = New ArrayList
        Dim _arlValid As ArrayList = New ArrayList
        Dim _arlInValid As ArrayList = New ArrayList
        Dim intStatus As Integer = 0


        Try
            _arlWarrantyActivation = _sessHelper.GetSession("sessWarrantyActivation")
            Dim criterias As New CriteriaComposite(New Criteria(GetType(WarrantyActivation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.WarrantyActivation), "PDI.PDIStatus", MatchType.Exact, "3"))

            If Not IsNothing(objDealer.DealerGroup) Then
                'filter dealer group
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "PDI.Dealer.DealerGroup", MatchType.Exact, objDealer.DealerGroup.ID))
            End If

            If ccTglPermintaan.Value.ToString <> "01/01/0001 0:00:00" And ccTglPermintaanEnd.Value.ToString <> "01/01/0001 0:00:00" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "WADate", MatchType.GreaterOrEqual, ccTglPermintaan.Value))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "WADate", MatchType.Lesser, ccTglPermintaanEnd.Value.AddDays(1)))
            End If

            If ccTglPDIMulai.Value.ToString <> "01/01/0001 0:00:00" And ccTglPDIAkhir.Value.ToString <> "01/01/0001 0:00:00" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "PDI.PDIDate", MatchType.GreaterOrEqual, ccTglPDIMulai.Value))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "PDI.PDIDate", MatchType.Lesser, ccTglPDIAkhir.Value.AddDays(1)))
            End If

            If ccTglPKTMulai.Value.ToString <> "01/01/0001 0:00:00" And ccTglPKTAkhir.Value.ToString <> "01/01/0001 0:00:00" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "ChassisMasterPKT.PKTDate", MatchType.GreaterOrEqual, ccTglPKTMulai.Value))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "ChassisMasterPKT.PKTDate", MatchType.Lesser, ccTglPKTAkhir.Value.AddDays(1)))
            End If

            If txtDealerCode.Text <> "" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "PDI.Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Split("-")(0).Trim))
            End If

            If txtNomorRangka.Text <> "" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "ChassisMaster.ChassisNumber", MatchType.Exact, txtNomorRangka.Text))
            End If

            If txtCustName.Text <> "" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "CustomerName", MatchType.Exact, txtCustName.Text.Split("-")(0).Trim))
            End If

            If txtNomorPolisi.Text <> "" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "PlateNumber", MatchType.Exact, txtNomorPolisi.Text.Split("-")(0).Trim))
            End If


            If ddlStatus.SelectedIndex > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WarrantyActivation), "Status", MatchType.Exact, ddlStatus.SelectedValue))
            End If

            Dim objWAAl = New WarrantyActivationFacade(User).Retrieve(criterias, sorts)
            _sessHelper.SetSession("WarrantyActivationAl", objWAAl)
            Dim data As ArrayList = New WarrantyActivationFacade(User).Retrieve(criterias, sorts)
            _sessHelper.SetSession("WarrantyActivationA1", data)

            If data.Count > 0 Then
                CreateExcel("WarrantyActivationList", data)
            Else
                MessageBox.Show("Tidak ada data yang di download")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            lblMessage.Text = ""
        End Try
    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim oD As Dealer
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = FileName
            ws.Cells("A3").Value = "No"
            ws.Cells("B3").Value = "Nomor Rangka"
            ws.Cells("C3").Value = "Nama Customer"
            ws.Cells("D3").Value = "Nomor Polisi"
            ws.Cells("E3").Value = "Tanggal Permintaan"
            ws.Cells("F3").Value = "Tanggal PDI"
            ws.Cells("G3").Value = "Dealer Pelaksana PDI "
            ws.Cells("H3").Value = "Tanggal PKT"
            ws.Cells("I3").Value = "Status"

            For i As Integer = 0 To Data.Count - 1
                Dim item As WarrantyActivation = Data(i)
                ws.Cells(i + 4, 1).Value = i + 1
                ws.Cells(i + 4, 2).Value = item.ChassisMaster.ChassisNumber

                If Not IsNothing(item.CustomerName) Then
                    ws.Cells(i + 4, 3).Value = item.CustomerName
                Else
                    ws.Cells(i + 4, 3).Value = ""
                End If

                If Not IsNothing(item.PlateNumber) Then
                    ws.Cells(i + 4, 4).Value = item.PlateNumber
                Else
                    ws.Cells(i + 4, 4).Value = ""
                End If

                ws.Cells(i + 4, 5).Style.Numberformat.Format = "DD-MM-YYYY"
                ws.Cells(i + 4, 5).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                ws.Cells(i + 4, 5).Value = item.WADate
                If Not IsNothing(item.PDI.PDIDate) Then
                    ws.Cells(i + 4, 6).Style.Numberformat.Format = "DD-MM-YYYY"
                    ws.Cells(i + 4, 6).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    ws.Cells(i + 4, 6).Value = item.PDI.PDIDate
                Else
                    ws.Cells(i + 4, 6).Value = ""
                End If
                If Not IsNothing(item.PDI.Dealer.DealerName) Then
                    ws.Cells(i + 4, 7).Value = item.PDI.Dealer.DealerName
                Else
                    ws.Cells(i + 4, 7).Value = ""
                End If
                If Not IsNothing(item.ChassisMasterPKT) Then
                    ws.Cells(i + 4, 8).Style.Numberformat.Format = "DD-MM-YYYY"
                    ws.Cells(i + 4, 8).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    ws.Cells(i + 4, 8).Value = item.ChassisMasterPKT.PKTDate
                Else
                    ws.Cells(i + 4, 8).Value = ""
                End If

                ws.Cells(i + 4, 9).Value = EnumWarrantyActivation.GetStringInformationType(item.Status)

            Next

            CreateExcelFile(pck, FileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xls")
        End Using

    End Sub

    Private Function CreateSheet(pck As ExcelPackage, sheetName As String) As ExcelWorksheet
        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add(sheetName)
        ws.View.ShowGridLines = False
        Return ws
    End Function

    Private Sub CreateExcelFile(pck As ExcelPackage, fileName As String)
        Dim fileBytes = pck.GetAsByteArray()

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Dim success As Boolean = False

        Try
            success = imp.Start()
            If success Then
                File.WriteAllBytes(Server.MapPath("~/DataTemp/" & fileName), fileBytes)
                imp.StopImpersonate()
            End If

        Catch ex As Exception
            Exit Sub

        End Try

        Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & fileName)


    End Sub

    Protected Sub dgWarrantyActivation_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs)
        BindWarrantyActivation(e.NewPageIndex)
    End Sub

    Protected Sub dgWarrantyActivation_SortCommand(source As Object, e As DataGridSortCommandEventArgs)
        If e.SortExpression = ViewState.Item("SortCol") Then
            If ViewState.Item("SortDirection") = Sort.SortDirection.ASC Then
                ViewState.Item("SortDirection") = Sort.SortDirection.DESC
            Else
                ViewState.Item("SortDirection") = Sort.SortDirection.ASC
            End If
        End If
        ViewState.Item("SortCol") = e.SortExpression
        dgWarrantyActivation.SelectedIndex = -1
        dgWarrantyActivation.CurrentPageIndex = 0
        BindWarrantyActivation(0)
    End Sub
End Class