#Region " Customer Name Space "

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports System.IO
Imports System.Text
Imports System.Linq
Imports System.Collections
Imports System.Collections.Generic
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
Imports PDFHelper
Imports PdfSharp
Imports PdfSharp.Pdf
Imports PdfSharp.Pdf.IO
#End Region


Public Class FrmMSPExtendedPayment
    Inherits System.Web.UI.Page

#Region "Private Variables"
    Private _sessHelper As New SessionHelper
    Private _strSessSearch As String = "CRITERIAS"
    Private _strSessMSPExTransferPaymentID As String = "MSPExTrfPaymentID"
    Private _strSessArrListMSPRegHistory As String = "ArrayListMSPRegHistory"
    Private _strSessDelete As String = "toBeDelete"
    Private _strSessStatusInput As String = "StatusInput"
    Dim _userInfo As UserInfo
    Private objLoginDealer As Dealer
    Private objUserInfo As UserInfo
    Private objMSPPayment As MSPExPayment
    Dim path As String = String.Empty
    Dim totalTransfer As Double = 0
    Private _view As Boolean = False
    Private _input As Boolean = False
    Private _edit As Boolean = False
#End Region

#Region "Custom Func/sub"

    Private Sub CheckPrivilege()
        _input = SecurityProvider.Authorize(Context.User, SR.MSPExtendedPayment_Input)
        _edit = SecurityProvider.Authorize(Context.User, SR.MSPExtendedPayment_Ubah)
        btnSave.Visible = _edit
        btnNew.Visible = _input
        If Not _input Then
            lblSearchParameter.Visible = False
            lblDoubleDot.Visible = False
            lblParameterPencarianPopUp.Visible = False
            btnSave.Visible = False

            txtTransferDate.Visible = False
            lblTransferDate.Visible = True

            btnNew.Visible = False
        End If
    End Sub

    Private Sub FillForm()
        SetHeaderData()
        Dim idMSPTrfPaymant As Integer = _sessHelper.GetSession(_strSessMSPExTransferPaymentID)
        Dim arr As New ArrayList
        If Not IsNothing(_sessHelper.GetSession(_strSessMSPExTransferPaymentID)) Then
            objMSPPayment = New MSPExPaymentFacade(User).Retrieve(idMSPTrfPaymant)
            If objMSPPayment.MSPExPaymentDetails.Count > 0 Then
                For Each item As MSPExPaymentDetail In objMSPPayment.MSPExPaymentDetails
                    arr.Add(item.MSPExRegistration)
                    If lblTipeMSP.Text.Trim.Length = 0 Then
                        Dim prefixMSP As PrefixMSPRegistration = New PrefixMSPRegistrationFacade(User).RetrieveByMSPExType(item.MSPExRegistration.MSPExMaster.MSPExType)
                        Dim stdCode As StandardCode = New StandardCodeFacade(User).GetByCategoryValueCode("EnumMSPProgram", prefixMSP.ProgramName)
                        lblTipeMSP.Text = stdCode.ValueDesc
                    End If
                Next
            End If

            'update header data if edit or view mode
            lblDealerCodeName.Text = objMSPPayment.Dealer.DealerCode & "/" & objMSPPayment.Dealer.SearchTerm1
            lblCreditAccount.Text = objMSPPayment.Dealer.CreditAccount
            lblCreatedDate.Text = objMSPPayment.CreatedTime.ToString("dd/MM/yyyy")
            lblStatus.Text = EnumMSPEx.GetStringValue(objMSPPayment.Status)
            hdnDealerID.Value = objMSPPayment.Dealer.ID
            If objMSPPayment.RegNumber <> String.Empty Then
                lblPaymentRegNo.Text = objMSPPayment.RegNumber
            End If

            If Convert.ToString(_sessHelper.GetSession(_strSessStatusInput)) = "VIEW" Then
                lblSearchParameter.Visible = False
                lblDoubleDot.Visible = False
                lblParameterPencarianPopUp.Visible = False
                btnSave.Visible = False

                txtTransferDate.Visible = False
                lblTransferDate.Visible = True
                lblTransferDate.Text = objMSPPayment.PlanTransferDate.ToString("dd/MM/yyyy")

                If objLoginDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    btnNew.Visible = False
                End If
            End If

            If objLoginDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If lblStatus.Text = "Baru" Or lblStatus.Text = "Batal_Validasi" Then
                    btnValidasi.Visible = True
                End If
            Else
                If lblStatus.Text = "Validasi" Then
                    btnConfirm.Visible = True
                End If
            End If

        End If

        BindDatagrid(arr)
    End Sub

    Private Sub BindDatagrid(ByVal arrayList As ArrayList)
        txtChassisNumberList.Value = String.Empty
        lblTotalTransfer.Text = "0"

        dtgMSPPayment.DataSource = arrayList
        dtgMSPPayment.DataBind()

        _sessHelper.SetSession(_strSessArrListMSPRegHistory, arrayList)
    End Sub

    Private Sub SetHeaderData()
        lblDealerCodeName.Text = objLoginDealer.DealerCode & "/" & objLoginDealer.SearchTerm1
        lblCreditAccount.Text = objLoginDealer.CreditAccount
        lblCreatedDate.Text = Date.Now.ToString("dd/MM/yyyy")
        lblStatus.Text = EnumMSPEx.GetStringValue(EnumMSPEx.MSPExStatusPayment.Baru)
        hdnDealerID.Value = objLoginDealer.ID
    End Sub

    Private Sub RemoveSession()
        _sessHelper.RemoveSession(_strSessArrListMSPRegHistory)
        _sessHelper.RemoveSession(_strSessMSPExTransferPaymentID)
        _sessHelper.RemoveSession(_strSessSearch)
        _sessHelper.RemoveSession(_strSessStatusInput)
    End Sub

    Private Function generateRegNumber(ByVal objMSPExPayment As MSPExPayment, ByVal type As String) As String
        Dim runningTime As DateTime = DateTime.Now()
        Dim newRegNumber As String = String.Empty
        Dim dealerCode As String = objMSPExPayment.Dealer.DealerCode
        'Dim seqStr As String = "6" & dealerCode.Substring(dealerCode.Length - 3, 3) & runningTime.ToString("yy") & runningTime.ToString("MM")
        Dim header As String = String.Empty
        Select Case type
            Case "MSPExtended"
                header = "6"
            Case "FleetPackage"
                header = "7"
            Case Else
                header = ""
        End Select
        Dim seqStr As String = header & dealerCode.Substring(dealerCode.Length - 3, 3) & runningTime.ToString("yy") & runningTime.ToString("MM")
        Dim strSQL As String = "select top 1 a.ID from MSPExPayment a " &
                                "where a.RowStatus = 0 " &
                                "and a.DealerID = " & objMSPExPayment.Dealer.ID &
                                " and a.RegNumber like '" & seqStr & "%' " &
                                "order by a.RegNumber desc"
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExPayment), "ID", MatchType.InSet, "(" & strSQL & ")"))

        Dim arr As ArrayList = New MSPExPaymentFacade(User).Retrieve(crit)
        If arr.Count > 0 Then
            Dim lastObj As MSPExPayment = CType(arr(0), MSPExPayment)
            Dim regNumber As String = lastObj.RegNumber
            Dim lastSeq As Integer = CInt(regNumber.Substring(regNumber.Length - 4, 4))
            lastSeq += 1
            newRegNumber = seqStr & lastSeq.ToString("d4")
            Return newRegNumber
        Else
            newRegNumber = seqStr & "0001"
            Return newRegNumber
        End If
    End Function

    Private Function saveDetail(ByVal obj As MSPExPayment) As Integer
        Dim result As Integer = 0
        If obj.ID > 0 Then
            Dim facadeObj As New MSPExPaymentDetailFacade(User)
            For Each detail As MSPExPaymentDetail In obj.MSPExPaymentDetails
                detail.MSPExPayment = obj
                result = facadeObj.Insert(detail)
            Next
        End If

        Return result
    End Function

    Private Sub commandDelete(ByVal e As DataGridCommandEventArgs)
        Dim toBeDelete As New ArrayList()
        If Not IsNothing(_sessHelper.GetSession(_strSessDelete)) Then
            toBeDelete = CType(_sessHelper.GetSession(_strSessDelete), ArrayList)
        End If
        Dim oldArr As ArrayList = CType(_sessHelper.GetSession(_strSessArrListMSPRegHistory), ArrayList)
        Dim objMspEx As MSPExRegistration = CType(oldArr(e.Item.ItemIndex), MSPExRegistration)
        If objMspEx.ID > 0 Then
            Dim crit As New CriteriaComposite(New Criteria(GetType(MSPExPaymentDetail), "RowStatus", MatchType.Exact, 0))
            crit.opAnd(New Criteria(GetType(MSPExPaymentDetail), "MSPExRegistration.ID", MatchType.Exact, objMspEx.ID))
            Dim objFacade As New MSPExPaymentDetailFacade(User)
            Dim arr As ArrayList = objFacade.Retrieve(crit)
            If Not IsNothing(arr) And arr.Count > 0 Then
                Dim objDel As MSPExPaymentDetail = CType(arr(0), MSPExPaymentDetail)
                objDel.RowStatus = -1
                toBeDelete.Add(objDel)
                _sessHelper.SetSession(_strSessDelete, toBeDelete)
            End If
            oldArr.RemoveAt(e.Item.ItemIndex)
        Else
            oldArr.RemoveAt(e.Item.ItemIndex)
        End If
        If oldArr.Count = 0 Then
            lblTipeMSP.Text = String.Empty
        End If
        BindDatagrid(oldArr)
    End Sub

    Private Function CommandDownload(ByVal e As DataGridCommandEventArgs, ByVal downloadAs As String)
        Dim arr As ArrayList
        Dim crt As CriteriaComposite
        Dim lblMSPRegistrationHistoryID As Label = CType(e.Item.FindControl("lblMSPRegistrationHistoryID"), Label)
        Dim lblDebitChargeNo As Label = CType(e.Item.FindControl("lblDebitChargeNo"), Label)

        If downloadAs.ToUpper = "DM" Then
            crt = New CriteriaComposite(New Criteria(GetType(MSPExDebitMemo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crt.opAnd(New Criteria(GetType(MSPExDebitMemo), "MSPExRegistration.ID", MatchType.Exact, lblMSPRegistrationHistoryID.Text))
            arr = New MSPExDebitMemoFacade(User).Retrieve(crt)

            If arr.Count > 0 Then
                Dim pathBaseDirectory As String = KTB.DNet.Lib.WebConfig.GetValue("MSPExDirectory")
                path = pathBaseDirectory & "\" & CType(arr(0), MSPExDebitMemo).FileName
            End If
        ElseIf downloadAs.ToUpper = "DC" Then
            crt = New CriteriaComposite(New Criteria(GetType(MSPExDebitCharge), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crt.opAnd(New Criteria(GetType(MSPExDebitCharge), "MSPExRegistration.ID", MatchType.Exact, lblMSPRegistrationHistoryID.Text))
            arr = New MSPExDebitChargeFacade(User).Retrieve(crt)

            If arr.Count > 0 Then
                Dim pathBaseDirectory As String = KTB.DNet.Lib.WebConfig.GetValue("MSPExDirectory")
                path = pathBaseDirectory & "\" & CType(arr(0), MSPExDebitCharge).FileName
            End If
        End If

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Try
            success = imp.Start()
            If success Then
                Dim fileInfo As New FileInfo(path)
                If (fileInfo.Exists) Then
                    Response.Redirect("../Download.aspx?file=" & path)
                    imp.StopImpersonate()
                    imp = Nothing
                Else
                    MessageBox.Show(SR.FileNotFound(fileInfo.Name))
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Download file tidak berhasil.")
        End Try

    End Function

    Private Function ValidateData() As String
        Dim str As String = String.Empty
        Dim now As DateTime = Date.Now().ToString("yyyyy-MM-dd")

        If txtTransferDate.Value = "#12:00:00 AM#" Then
            str += "\n" & "Tanggal transfer tidak boleh kosong."
        ElseIf (CDate(Format(txtTransferDate.Value, "yyyy-MM-dd")) < now) Then
            str += "\n" & "Tanggal transfer tidak boleh kurang dari tanggal saat ini."
        End If

        If dtgMSPPayment.Items.Count < 1 Then
            str += "\n" & "Belum ada no rangka yang dipilih."
        End If

        Return str
    End Function


#End Region

#Region "Event Handler"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        objLoginDealer = CType(_sessHelper.GetSession("DEALER"), Dealer)
        objUserInfo = CType(_sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If Not IsPostBack Then
            FillForm()
        End If
    End Sub

    Protected Sub btnLoaddtFromPopUp_Click(sender As Object, e As EventArgs) Handles btnLoaddtFromPopUp.Click
        Dim crt As CriteriaComposite
        Dim arr As ArrayList
        Dim strSql As String = String.Empty
        Dim strSqlRegNumber As String = String.Empty
        Dim split() As String = txtChassisNumberList.Value.ToString.Split(";")
        If split.Count > 0 Then
            For i As Integer = 0 To split.Count - 1
                strSqlRegNumber += ",'" & split(i).Split("|")(1) & "'"
                strSql += ",'" & split(i) & "'"
            Next
        End If
        'If strSql <> String.Empty Then
        '    crt = New CriteriaComposite(New Criteria(GetType(MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.InSet, "(" & strSql.Substring(1, strSql.Length - 1) & ")"))
        'End If
        If strSqlRegNumber <> String.Empty Then
            crt = New CriteriaComposite(New Criteria(GetType(MSPExRegistration), "RegNumber", MatchType.InSet, "(" & strSqlRegNumber.Substring(1, strSqlRegNumber.Length - 1) & ")"))
        End If

        arr = New MSPExRegistrationFacade(User).Retrieve(crt)
        Dim newArr As ArrayList = CType(_sessHelper.GetSession(_strSessArrListMSPRegHistory), ArrayList)
        If arr.Count > 0 Then
            For Each item As MSPExRegistration In arr
                Dim prefixMSP As PrefixMSPRegistration = New PrefixMSPRegistrationFacade(User).RetrieveByMSPExType(item.MSPExMaster.MSPExType)
                Dim stdCode As StandardCode = New StandardCodeFacade(User).GetByCategoryValueCode("EnumMSPProgram", prefixMSP.ProgramName)
                Dim isHavePayment As Boolean = False
                crt = New CriteriaComposite(New Criteria(GetType(MSPExPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crt.opAnd(New Criteria(GetType(MSPExPaymentDetail), "MSPExPayment.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crt.opAnd(New Criteria(GetType(MSPExPaymentDetail), "MSPExRegistration.ID", MatchType.Exact, item.ID))
                Dim arrTrfDetail As ArrayList = New MSPExPaymentDetailFacade(User).Retrieve(crt)
                If Not IsNothing(arrTrfDetail) AndAlso arrTrfDetail.Count > 0 Then

                    For Each objTrfdetail As MSPExPaymentDetail In arrTrfDetail
                        If item.ID = objTrfdetail.MSPExRegistration.ID Then
                            isHavePayment = True
                        End If
                    Next
                End If
                If lblTipeMSP.Text.Trim.Length = 0 Then
                    lblTipeMSP.Text = stdCode.ValueDesc
                End If
                If lblTipeMSP.Text.Trim.ToLower <> stdCode.ValueDesc.Trim.ToLower Then
                    MessageBox.Show("Chassis yg di pilih harus 1 Tipe Program")
                Else
                    If isHavePayment = False Then
                        newArr.Add(item)
                    End If
                End If
            Next
        End If
        BindDatagrid(newArr)

    End Sub

    Private Sub dtgMSPPayment_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgMSPPayment.ItemCommand
        Dim arr As ArrayList
        If (e.CommandName.ToUpper = "DELETE") Then
            commandDelete(e)
        ElseIf (e.CommandName.ToUpper = "DOWNLOADDC") Then
            CommandDownload(e, "DC")
        ElseIf (e.CommandName.ToUpper = "DOWNLOADDM") Then
            CommandDownload(e, "DM")
        ElseIf (e.CommandName.ToUpper = "DOWNLOADFAKTURPAJAK") Then
            Dim item As EFakturItem = New EFakturItemFacade(User).Retrieve(CInt(e.CommandArgument))
            DownloadSingleEFaktur(item)
        End If
    End Sub

    Private Sub DownloadSingleEFaktur(ByVal data As EFakturItem)
        Dim pdfDoc As New PdfDocument()
        pdfDoc.Options.FlateEncodeMode = PdfFlateEncodeMode.BestCompression
        pdfDoc.Options.UseFlateDecoderForJpegImages = PdfUseFlateDecoderForJpegImages.Automatic
        pdfDoc.Options.NoCompression = False
        pdfDoc.Options.CompressContentStreams = True
        Try
            Dim page As PdfPage = Nothing
            If data.PageNumber.Contains("-") Then
                Dim startPage As Integer = CInt(data.PageNumber.Split("-")(0))
                Dim endPage As Integer = CInt(data.PageNumber.Split("-")(1))
                For i As Integer = startPage To endPage
                    page = GetPagePerEFaktur(data.EFakturHeader.PdfFilePath, i)
                    pdfDoc.AddPage(page)
                Next
            Else
                page = GetPagePerEFaktur(data.EFakturHeader.PdfFilePath, data.PageNumber)
                pdfDoc.AddPage(page)
            End If
        Catch
            Return
        End Try
        Dim fileName As String = Guid.NewGuid().ToString().Substring(0, 5) & ".pdf"
        SaveFileToTempAndDownload(pdfDoc, fileName)
    End Sub

    Private Function GetPagePerEFaktur(ByVal mainFilePath As String, ByVal pageNumber As Integer) As PdfPage
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim filePath As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") + mainFilePath
        Dim fileInfo As New FileInfo(filePath)
        Dim destFilePath As String = fileInfo.FullName
        Dim success As Boolean = False

        Try
            success = imp.Start()
            If success Then
                If (fileInfo.Exists) Then
                    Dim pdfDoc As PdfDocument = PdfReader.Open(filePath, PdfDocumentOpenMode.Import)
                    Dim page As PdfPage = pdfDoc.Pages(pageNumber - 1)
                    Return page
                Else
                    MessageBox.Show(SR.FileNotFound(fileInfo.Name))
                End If
                imp.StopImpersonate()
            End If
        Catch ex As Exception
            MessageBox.Show(SR.DownloadFail(fileInfo.Name))
        End Try
    End Function

    Private Sub SaveFileToTempAndDownload(pdfDoc As PdfDocument, fileName As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Try
            success = imp.Start()
            If success Then
                pdfDoc.Save(Server.MapPath("~/DataTemp/" & fileName))
                imp.StopImpersonate()
            End If
        Catch ex As Exception
            MessageBox.Show(SR.SaveFail(fileName))
        End Try
        Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & fileName)

    End Sub

    Private Sub dtgMSPPayment_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMSPPayment.ItemDataBound
        Dim arr As ArrayList
        Dim crt As CriteriaComposite
        If Convert.ToString(_sessHelper.GetSession(_strSessStatusInput)) = "VIEW" Then
            dtgMSPPayment.Columns(dtgMSPPayment.Columns.Count - 1).Visible = False
        End If

        ' set lblNo
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgMSPPayment.CurrentPageIndex * dtgMSPPayment.PageSize)
        End If

        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            Dim rowValue As MSPExRegistration = CType(e.Item.DataItem, MSPExRegistration)
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Then

                ' set txtChassisNumberList
                txtChassisNumberList.Value += ";" & rowValue.ChassisMaster.ChassisNumber

                ' set MSP Registration History ID
                Dim lblMSPRegistrationHistoryID As Label = CType(e.Item.FindControl("lblMSPRegistrationHistoryID"), Label)
                If Not IsNothing(lblMSPRegistrationHistoryID) Then
                    lblMSPRegistrationHistoryID.Text = rowValue.ID
                End If

                ' set No Rangka
                Dim lblChassisNumber As Label = CType(e.Item.FindControl("lblChassisNumber"), Label)
                If Not IsNothing(lblChassisNumber) Then
                    lblChassisNumber.Text = rowValue.ChassisMaster.ChassisNumber
                End If

                ' set dealer code
                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                If Not IsNothing(lblDealerCode) Then
                    lblDealerCode.Text = rowValue.Dealer.DealerCode
                End If

                ' set MSP No
                Dim lblNoMSP As Label = CType(e.Item.FindControl("lblNoMSP"), Label)
                If Not IsNothing(lblNoMSP) Then
                    lblNoMSP.Text = rowValue.RegNumber
                End If

                '' set Request type
                'Dim lblRequestType As Label = CType(e.Item.FindControl("lblRequestType"), Label)
                'If Not IsNothing(lblRequestType) Then
                '    lblRequestType.Text = rowValue.Status
                'End If

                ' set lblMSPType
                Dim lblMSPType As Label = CType(e.Item.FindControl("lblMSPType"), Label)
                If Not IsNothing(lblMSPType) Then
                    lblMSPType.Text = rowValue.MSPExMaster.MSPExType.Description
                End If

                ' set debit charge no & set amount
                Dim lblAmount As Label = CType(e.Item.FindControl("lblAmount"), Label)
                Dim lblDebitChargeNo As Label = CType(e.Item.FindControl("lblDebitChargeNo"), Label)
                If Not IsNothing(lblDebitChargeNo) Then
                    crt = New CriteriaComposite(New Criteria(GetType(MSPExDebitCharge), "MSPExRegistration.ID", MatchType.Exact, rowValue.ID))
                    crt.opAnd(New Criteria(GetType(MSPExDebitCharge), "DebitChargeNo", MatchType.No, String.Empty))
                    arr = New MSPExDebitChargeFacade(User).Retrieve(crt)
                    If arr.Count > 0 Then
                        Dim objMSPDC As MSPExDebitCharge = CType(arr(0), MSPExDebitCharge)
                        lblDebitChargeNo.Text = objMSPDC.DebitChargeNo

                        Dim amount As Decimal = objMSPDC.Amount
                        lblAmount.Text = amount.ToString("C")
                        ' set header data [Total Transfer]
                        totalTransfer += CType(lblAmount.Text, Decimal)
                        lblTotalTransfer.Text = (totalTransfer).ToString("C")
                        lblTotalTransferHdn.Text = totalTransfer
                    End If
                End If

                Dim lbtnDownloadDM As LinkButton = CType(e.Item.FindControl("lbtnDownloadDM"), LinkButton)
                lbtnDownloadDM.Visible = False
                If Not IsNothing(lbtnDownloadDM) Then
                    Dim crtMSPDM As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExDebitMemo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crtMSPDM.opAnd(New Criteria(GetType(MSPExDebitMemo), "MSPExRegistration.ID", MatchType.Exact, rowValue.ID))
                    Dim arrmspdm As ArrayList = New MSPExDebitMemoFacade(User).Retrieve(crtMSPDM)
                    If arrmspdm.Count > 0 Then
                        Dim objMSPDM As MSPExDebitMemo = CType(arrmspdm(0), MSPExDebitMemo)
                        If objMSPDM.FileName.Trim = String.Empty Then
                            lbtnDownloadDM.Visible = False
                        Else
                            lbtnDownloadDM.Visible = True
                        End If
                    End If
                End If

                'set lblFakturPajak & lblTotalAmount
                Dim lblFakturPajak As Label = CType(e.Item.FindControl("lblFakturPajak"), Label)
                Dim lblFakturPajakTotalAmount As Label = CType(e.Item.FindControl("lblFakturPajakTotalAmount"), Label)
                Dim oFakturPajak As MSPExFakturPajak = New MSPExFakturPajakFacade(User).RetrieveByRegNumber(rowValue.RegNumber)
                If oFakturPajak.ID > 0 Then
                    lblFakturPajak.Text = oFakturPajak.FakturPajakNo
                    lblFakturPajakTotalAmount.Text = oFakturPajak.Amount.ToString("N0")
                End If

                'set lbtnDownloadFakturPajak
                Dim lbtnDownloadFakturPajak As LinkButton = CType(e.Item.FindControl("lbtnDownloadFakturPajak"), LinkButton)
                Dim oMSPExDebitMemo As MSPExDebitMemo = New MSPExDebitMemoFacade(User).RetrieveByRegistration(rowValue)
                Dim oEFakturItem As EFakturItem = New EFakturItemFacade(User).Retrieve(oMSPExDebitMemo.DebitMemoNo, EnumEFaktur.EnumEFakturTransactionType.MSPEXTENDED)
                If oEFakturItem.ID > 0 Then
                    lbtnDownloadFakturPajak.CommandArgument = oEFakturItem.ID
                    lbtnDownloadFakturPajak.Visible = True
                Else
                    oEFakturItem = New EFakturItemFacade(User).Retrieve(oFakturPajak.FakturPajakNo, EnumEFaktur.EnumEFakturTransactionType.MSPEXTENDED)
                    If oEFakturItem.ID > 0 Then
                        lbtnDownloadFakturPajak.CommandArgument = oEFakturItem.ID
                        lbtnDownloadFakturPajak.Visible = True
                    End If
                End If

                Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                lbtnDelete.Attributes.Add("OnClick", "return confirm('Hapus chassis " & rowValue.ChassisMaster.ChassisNumber & "?');")
                If Not IsNothing(lbtnDelete) Then
                    lbtnDelete.Visible = _input
                End If
            End If
            End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim str As String = ValidateData()
        Dim type As String = String.Empty
        If str = String.Empty Then
            objMSPPayment = New MSPExPayment
            objMSPPayment.Dealer = objLoginDealer
            objMSPPayment.PlanTransferDate = CDate(Format(txtTransferDate.Value, "yyyy-MM-dd"))
            objMSPPayment.TotalAmount = lblTotalTransferHdn.Text

            For Each item As DataGridItem In dtgMSPPayment.Items
                Dim objPaymentDetail As New MSPExPaymentDetail
                Dim lblMSPRegistrationID As Label = CType(item.FindControl("lblMSPRegistrationHistoryID"), Label)
                Dim objMSPRegHistory As MSPExRegistration = New MSPExRegistrationFacade(User).Retrieve(CInt(lblMSPRegistrationID.Text))
                Dim oPrefix As PrefixMSPRegistration = New PrefixMSPRegistrationFacade(User).RetrieveByMSPExType(objMSPRegHistory.MSPExMaster.MSPExType)
                If oPrefix.ID > 0 Then
                    type = oPrefix.ProgramName
                End If
                objPaymentDetail.MSPExRegistration = objMSPRegHistory
                Dim lblAmount As Label = CType(item.FindControl("lblAmount"), Label)
                If Not IsNothing(lblAmount) Then
                    If lblAmount.Text.Trim = "" Then
                        MessageBox.Show("Invalid Amount")
                        Return
                    End If
                End If
                objPaymentDetail.Amount = If(Not IsNothing(lblAmount), CType(lblAmount.Text, Decimal), 0)
                Dim lblDebitChargeNo As Label = CType(item.FindControl("lblDebitChargeNo"), Label)
                If IsNothing(lblDebitChargeNo) Then
                    If lblDebitChargeNo.Text = String.Empty Then
                        MessageBox.Show("Invalid Debit Charge Number")
                        Return
                    End If
                End If
                objPaymentDetail.DebitChargeNo = lblDebitChargeNo.Text
                ' add to MSPPaymentDetail to list
                objMSPPayment.MSPExPaymentDetails.Add(objPaymentDetail)
            Next

            Dim int As Integer
            If IsNothing(_sessHelper.GetSession(_strSessMSPExTransferPaymentID)) Then
                ' insert
                If objMSPPayment.RegNumber = String.Empty Then
                    objMSPPayment.RegNumber = generateRegNumber(objMSPPayment, type)
                End If

                objMSPPayment.Status = EnumMSPEx.MSPExStatusPayment.Baru
                int = New MSPExPaymentFacade(User).Insert(objMSPPayment)
                If int > 0 Then
                    objMSPPayment.ID = int
                    saveDetail(objMSPPayment)
                    MessageBox.Show("Data pembayaran berhasil tersimpan.")
                    btnValidasi.Visible = True
                    ' set id MSP Payment untuk proses update
                    _sessHelper.SetSession(_strSessMSPExTransferPaymentID, int)

                    ' add to history status
                    Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                    objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.MSP_TransferPayment), "", -1, objMSPPayment.Status)
                    _sessHelper.SetSession(_strSessMSPExTransferPaymentID, int)
                    lblPaymentRegNo.Text = objMSPPayment.RegNumber
                End If
            Else
                ' update
                Dim oldMSPPayment As MSPExPayment = New MSPExPaymentFacade(User).Retrieve(CInt(_sessHelper.GetSession(_strSessMSPExTransferPaymentID)))
                oldMSPPayment.Dealer = objLoginDealer
                oldMSPPayment.PlanTransferDate = CDate(Format(txtTransferDate.Value, "yyyy-MM-dd"))
                oldMSPPayment.TotalAmount = lblTotalTransferHdn.Text
                int = New MSPExPaymentFacade(User).Update(oldMSPPayment)
                If int > 0 Then
                    ' delete all detail in old data
                    For Each itemOld As MSPExPaymentDetail In oldMSPPayment.MSPExPaymentDetails
                        Dim facTrfDetail As New MSPExPaymentDetailFacade(User)
                        itemOld.RowStatus = -1
                        facTrfDetail.Update(itemOld)
                    Next

                    ' insert all new input detail data
                    For Each itemNew As MSPExPaymentDetail In objMSPPayment.MSPExPaymentDetails
                        itemNew.MSPExPayment = oldMSPPayment
                        int = New MSPExPaymentDetailFacade(User).Insert(itemNew)
                    Next

                    If Not IsNothing(_sessHelper.GetSession(_strSessDelete)) Then
                        Dim tempDel As ArrayList = CType(_sessHelper.GetSession(_strSessDelete), ArrayList)
                        If tempDel.Count > 0 Then
                            For Each item As MSPExPaymentDetail In tempDel
                                Dim res As Integer = New MSPExPaymentDetailFacade(User).Update(item)
                            Next
                        End If
                        _sessHelper.RemoveSession(_strSessDelete)
                    End If

                    MessageBox.Show("Data pembayaran berhasil terupdate.")
                Else
                    MessageBox.Show("Gagal update data pembayaran.")
                End If
            End If
        Else
            MessageBox.Show(str)
        End If
    End Sub

    Private Sub btnValidasi_Click(sender As Object, e As EventArgs) Handles btnValidasi.Click
        Dim objMSPPayment As MSPExPayment = New MSPExPaymentFacade(User).Retrieve(CInt(_sessHelper.GetSession(_strSessMSPExTransferPaymentID)))

        If Not IsNothing(objMSPPayment) Then
            'If (objMSPPayment.Status = EnumStatusMSP.Status.Baru Or objMSPPayment.Status = EnumStatusMSP.Status.Batal_Validasi) Then
            If (objMSPPayment.Status = 0) Then
                objMSPPayment.Status = EnumMSPEx.MSPExStatusPayment.Validasi 'status validasi, belum masih ke standard code
                objMSPPayment.IsValidation = True

                If (New MSPExPaymentFacade(User).Update(objMSPPayment)) = -1 Then
                    MessageBox.Show("Gagal validasi data pembayaran MSP.")
                    Return
                End If

                ' add to history status
                Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                'objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.MSP_TransferPayment), objMSPPayment.RegNumber, -1, objMSPPayment.Status)
                objStatusChangeHistoryFacade.Insert(20, objMSPPayment.RegNumber, -1, objMSPPayment.Status)

                MessageBox.Show("Sukses validasi data pembayaran MSP Extended.")
                _sessHelper.SetSession(_strSessStatusInput, "VIEW")
                _sessHelper.SetSession(_strSessMSPExTransferPaymentID, objMSPPayment.ID)
                FillForm()
                btnValidasi.Visible = False
                btnSave.Visible = False

            End If
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        RemoveSession()
        Response.Redirect("FrmMSPExtendedPaymentList.aspx")
    End Sub

    Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        RemoveSession()
        Response.Redirect("FrmMSPExtendedPayment.aspx")
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Dim objMSPPayment As MSPExPayment = New MSPExPaymentFacade(User).Retrieve(CInt(_sessHelper.GetSession(_strSessMSPExTransferPaymentID)))

        If Not IsNothing(objMSPPayment) Then
            If (objMSPPayment.Status = 1) Then 'status validasi
                objMSPPayment.Status = EnumMSPEx.MSPExStatusPayment.Konfirmasi  'EnumStatusMSP.Status.Konfirmasi

                If (New MSPExPaymentFacade(User).Update(objMSPPayment)) = -1 Then
                    MessageBox.Show("Gagal validasi data pembayaran MSP Extended.")
                    Return
                End If

                ' add to history status
                Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                'objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.MSP_TransferPayment), objMSPPayment.RegNumber, -1, objMSPPayment.Status)
                objStatusChangeHistoryFacade.Insert(20, objMSPPayment.RegNumber, -1, objMSPPayment.Status)

                MessageBox.Show("Sukses validasi data pembayaran MSP.")
                _sessHelper.SetSession(_strSessStatusInput, "VIEW")
                Response.Redirect("FrmMSPPayment.aspx")
            End If
        End If
    End Sub

#End Region

End Class