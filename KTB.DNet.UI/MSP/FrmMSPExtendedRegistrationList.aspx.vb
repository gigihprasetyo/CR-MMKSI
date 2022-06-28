Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports System.Linq
Imports System.IO
Imports DocumentFormat.OpenXml.Packaging
Imports OfficeOpenXml
Imports System.Text
Imports System.Collections.Generic
Imports Spire.Doc
Imports KTB.DNet.Security
Imports System



Public Class FrmMSPExtendedRegistrationList
    Inherits System.Web.UI.Page

    Private _view As Boolean = False
    Private _sessHelper As New SessionHelper
    Private _searchSess As String = "FrmMSPExtendedRegistrationList.Criteria"
    Private _dataSess As String = "FrmMSPExtendedRegistrationList.Data"
    Dim crt As CriteriaComposite
    Dim arr As ArrayList
    Dim sortCols As SortCollection
    Private objMSPExRegistration As MSPExRegistration
    Private objDealer As Dealer
    Dim _path As String = String.Empty
    Dim PrivView As Boolean = SecurityProvider.Authorize(Context.User, SR.MSPExtended_View)
    Dim PrivEdit As Boolean = SecurityProvider.Authorize(Context.User, SR.MSPExtended_Ubah)
    Dim PrivDelete As Boolean = SecurityProvider.Authorize(Context.User, SR.MSPExtended_Hapus)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objDealer = CType(_sessHelper.GetSession("DEALER"), Dealer)
        If Not IsPostBack Then
            BindDropDown()
            ViewState("CurrentSortColumn") = "CreatedTime"
            ViewState("CurrentSortDirect") = KTB.DNet.Domain.Search.Sort.SortDirection.DESC

            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                lblSearchDealer.Attributes("OnClick") = "showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);"
            Else
                lblSearchDealer.Attributes("OnClick") = "ShowPPDealerSelection('" & objDealer.DealerGroup.ID & "','" & objDealer.DealerCode & "');"
            End If

            dtgMSPRegistrationList.DataSource = New ArrayList
            dtgMSPRegistrationList.DataBind()
            hdConfirm.Value = "-1"
        End If

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            lblChangeStatus.Visible = False
            ddlProses.Visible = False
            btnProses.Visible = False
            lblKodeDealer.Visible = False
        Else
            lblChangeStatus.Visible = PrivEdit
            ddlProses.Visible = PrivEdit
            btnProses.Visible = PrivEdit

            lblKodeDealer.Visible = True
            lblKodeDealer.Text = objDealer.DealerCode
            txtKodeDealer.Visible = False
            lblSearchDealer.Visible = False
        End If
    End Sub

    Private Sub BindDropDown()
        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        arr = New MSPExMasterFacade(User).Retrieve(crt)

        Dim newArrObjMSPMaster = From a As MSPExMaster In arr
                                 Group By a.MSPExType.ID, a.MSPExType.Description Into Group
                                 Select ID, Description

        ddlMSPType.Items.Clear()
        ddlMSPType.DataSource = newArrObjMSPMaster
        ddlMSPType.DataTextField = "Description"
        ddlMSPType.DataValueField = "ID"
        ddlMSPType.DataBind()
        ddlMSPType.Items.Insert(0, New ListItem("Silahkan Pilih", 0))
        ddlMSPType.SelectedIndex = 0

        ' dropdown status
        lboxStatus.Items.Clear()
        Dim arrStatus As ArrayList = New EnumMSPEx().RetrieveStatus()
        'updated 07012021 by irfan (add status "baru" untuk dealer dan MKS)
        'If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
        '    arrStatus.RemoveAt(0)
        'End If
        lboxStatus.DataSource = arrStatus
        lboxStatus.DataTextField = "Value"
        lboxStatus.DataValueField = "ID"
        lboxStatus.DataBind()

        Dim crt2 = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crt2.opAnd(New Criteria(GetType(Category), "CategoryCode", MatchType.No, "CV"))
        Dim sortss As SortCollection = New SortCollection
        sortss.Add(New Sort(GetType(Category), "CategoryCode", Search.Sort.SortDirection.ASC))
        arr = New CategoryFacade(User).RetrieveByCriteria(crt2, sortss)

        ddlCategoryV.Items.Clear()
        ddlCategoryV.DataSource = arr
        ddlCategoryV.DataTextField = "CategoryCode".ToUpper
        ddlCategoryV.DataValueField = "ID"
        ddlCategoryV.DataBind()
        ddlCategoryV.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlCategoryV.SelectedIndex = 0
        ddlCategoryV_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub BindGrid(ByVal index As Integer)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            'criterias.opAnd(New Criteria(GetType(MSPExRegistration), "Dealer.DealerGroup.ID", MatchType.Exact, objDealer.DealerGroup.ID))
            criterias.opAnd(New Criteria(GetType(MSPExRegistration), "Dealer.DealerCode", MatchType.Exact, objDealer.DealerCode))
        End If
        SearchCriteria(criterias)
        Dim totalRow As Integer = 0
        Dim arlData As ArrayList = New MSPExRegistrationFacade(User).RetrieveActiveList(index + 1, dtgMSPRegistrationList.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection), criterias)
        If arlData.Count = 0 Then
            MessageBox.Show("Data tidak ditemukan")
            dtgMSPRegistrationList.DataSource = New ArrayList
            dtgMSPRegistrationList.DataBind()
            Exit Sub
        End If
        _sessHelper.SetSession(_dataSess, arlData)
        _sessHelper.SetSession(_searchSess, criterias)

        dtgMSPRegistrationList.CurrentPageIndex = index
        dtgMSPRegistrationList.DataSource = arlData
        dtgMSPRegistrationList.VirtualItemCount = totalRow
        dtgMSPRegistrationList.DataBind()
    End Sub

    Private Sub SearchCriteria(ByRef criteria As CriteriaComposite)
        If txtKodeDealer.Text.Trim.Length > 0 Then
            Dim dealerCode As String = txtKodeDealer.Text.Trim.Replace(";", "','")
            criteria.opAnd(New Criteria(GetType(MSPExRegistration), "Dealer.DealerCode", MatchType.InSet, "('" & dealerCode & "')"))
        End If

        If txtRegNo.Text.Trim.Length > 0 Then
            Dim strSql As String = "select msr.ID from  MSPExPaymentDetail mspd  " _
                    & " inner join MSPExPayment msp on msp.ID=mspd.MSPExPaymentID " _
                    & " inner join MSPExRegistration msr on msr.ID= mspd.MSPExRegistrationID " _
                    & " where msp.RegNumber like '%" & txtRegNo.Text.Trim & "%' "
            criteria.opAnd(New Criteria(GetType(MSPExRegistration), "ID", MatchType.InSet, "(" & strSql & ")"))
        End If

        If txtMSPNo.Text.Trim.Length > 0 Then
            criteria.opAnd(New Criteria(GetType(MSPExRegistration), "RegNumber", MatchType.Partial, txtMSPNo.Text.Trim))
        End If
        If txtChassisNumber.Text.Trim.Length > 0 Then
            criteria.opAnd(New Criteria(GetType(MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Partial, txtChassisNumber.Text.Trim))
        End If
        If ddlCategoryV.SelectedIndex > 0 Then
            criteria.opAnd(New Criteria(GetType(MSPExRegistration), "MSPExMaster.VechileType.Category.ID", MatchType.Exact, ddlCategoryV.SelectedValue))
            If ddlVechileModel.SelectedIndex > 0 Then
                Dim strSql2 As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlVechileModel.SelectedValue
                criteria.opAnd(New Criteria(GetType(MSPExRegistration), "MSPExMaster.VechileType.VechileModel.ID", MatchType.InSet, "(" & strSql2 & ")"))
                If ddlVechileType.SelectedIndex <> 0 Then
                    criteria.opAnd(New Criteria(GetType(MSPExRegistration), "MSPExMaster.VechileType.ID", MatchType.Exact, ddlVechileType.SelectedValue))
                End If
            End If
        End If
        If ddlMSPType.SelectedIndex > 0 Then
            criteria.opAnd(New Criteria(GetType(MSPExRegistration), "MSPExMaster.MSPExType.ID", MatchType.Exact, ddlMSPType.SelectedValue))
        End If
        If lboxStatus.SelectedIndex <> -1 Then
            Dim SelectedStatus As String = GetSelectedItem(lboxStatus)
            criteria.opAnd(New Criteria(GetType(MSPExRegistration), "Status", MatchType.InSet, "(" & SelectedStatus & ")"))
        Else
            Dim SelectedStatus As String = "'0','1','3','4'"
            criteria.opAnd(New Criteria(GetType(MSPExRegistration), "Status", MatchType.InSet, "(" & SelectedStatus & ")"))
        End If

        If chkRequestDate.Checked Then
            criteria.opAnd(New Criteria(GetType(MSPExRegistration), "CreatedTime", MatchType.GreaterOrEqual, Format(DateFrom.Value, "yyyy/MM/dd 00:00:00")))
            'DateTo.Value = DateAdd(DateInterval.Day, 1, DateTo.Value)
            criteria.opAnd(New Criteria(GetType(MSPExRegistration), "CreatedTime", MatchType.LesserOrEqual, Format(DateTo.Value, "yyyy/MM/dd 23:59:59")))
        End If

        'If ddlVechileType.SelectedIndex > 0 Then


    End Sub

    Protected Sub dtgMSPRegistrationList_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMSPRegistrationList.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
            Dim lblMSPCode As Label = CType(e.Item.FindControl("lblMSPCode"), Label)
            Dim lblChassisNumber As Label = CType(e.Item.FindControl("lblChassisNumber"), Label)
            Dim lblVehicleDescription As Label = CType(e.Item.FindControl("lblVehicleDescription"), Label)
            Dim lblMSPType As Label = CType(e.Item.FindControl("lblMSPType"), Label)
            Dim lblRequestDate As Label = CType(e.Item.FindControl("lblRequestDate"), Label)
            Dim lblDocumentDate As Label = CType(e.Item.FindControl("lblDocumentDate"), Label)
            Dim lblDebitMemoNo As Label = CType(e.Item.FindControl("lblDebitMemoNo"), Label)
            'Dim lblfskindtype As Label = CType(e.Item.FindControl("lblfskindtype"), Label)
            Dim lbtnDownload As LinkButton = CType(e.Item.FindControl("lbtnDownload"), LinkButton)
            Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnHistory As LinkButton = CType(e.Item.FindControl("lbtnHistory"), LinkButton)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

            Dim rowValue As MSPExRegistration = CType(e.Item.DataItem, MSPExRegistration)
            lblNo.Text = e.Item.ItemIndex + 1 + (dtgMSPRegistrationList.CurrentPageIndex * dtgMSPRegistrationList.PageSize)
            lblStatus.Text = EnumMSPEx.GetStringValue(rowValue.Status)
            lblDealer.Text = rowValue.Dealer.DealerCode
            lblMSPCode.Text = rowValue.RegNumber
            lblChassisNumber.Text = rowValue.ChassisMaster.ChassisNumber
            lblVehicleDescription.Text = rowValue.MSPExMaster.VechileType.Description
            lblMSPType.Text = rowValue.MSPExMaster.MSPExType.Code
            lblRequestDate.Text = rowValue.CreatedTime.ToString("dd/MM/yyyy")

            Dim debitMemo As MSPExDebitMemo = New MSPExDebitMemoFacade(User).RetrieveByRegistration(rowValue)
            If Not debitMemo Is Nothing Then
                lblDocumentDate.Text = debitMemo.DocumentDate.ToString("dd/MM/yyyy")
                lblDebitMemoNo.Text = debitMemo.DebitMemoNo
            End If

            'lblfskindtype.Text = rowValue.FreeService.FSKind.KindCode
            lbtnView.CommandArgument = rowValue.RegNumber
            lbtnEdit.CommandArgument = rowValue.RegNumber
            lbtnDownload.CommandArgument = rowValue.RegNumber

            If rowValue.Status <> EnumMSPEx.MSPExStatus.Baru Then
                lbtnDelete.Visible = False
                lbtnEdit.Visible = False
                lbtnDownload.Visible = True
            Else
                If objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                    lbtnEdit.Visible = PrivEdit
                Else
                    lbtnEdit.Visible = False
                End If
                lbtnDelete.Visible = PrivDelete
            End If

            Dim lblRegNo As Label = CType(e.Item.FindControl("lblRegNo"), Label)
            Dim dtMPayment As MSPExPaymentDetail = New MSPExPaymentDetailFacade(User).RetrieveID(rowValue.ID)
            If dtMPayment.ID <> 0 Then
                lblRegNo.Text = dtMPayment.MSPExPayment.RegNumber
            End If

            lbtnHistory.Attributes("OnClick") = "showPopUp('../PopUp/PopUpStatusChangeMSPExtended.aspx?Id=" & rowValue.ID & " ','',500,760,'');"

            lbtnView.Visible = PrivView
        End If
    End Sub

    Protected Sub dtgMSPRegistrationList_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgMSPRegistrationList.PageIndexChanged
        dtgMSPRegistrationList.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
    End Sub

    Protected Sub dtgMSPRegistrationList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgMSPRegistrationList.ItemCommand
        Select Case e.CommandName
            Case "View"
                Response.Redirect("../MSP/FrmMSPExtendedRegistration.aspx?MOD=View&REGN=" & e.CommandArgument)
            Case "Edit"
                Response.Redirect("../MSP/FrmMSPExtendedRegistration.aspx?MOD=Edit&REGN=" & e.CommandArgument)
            Case "Download"
                Dim oMSPExReg As MSPExRegistration = New MSPExRegistrationFacade(User).RetrieveByRegNumber(e.CommandArgument.ToString())
                Dim oMSPExMaxPM As MSPExMaxPM = New MSPExMaxPMFacade(User).Retrieve(oMSPExReg.MSPExMaster.MSPExType.Code)
                If oMSPExMaxPM.ID = 0 Then
                    DownloadCert_Normal(e.CommandArgument.ToString())
                Else
                    DownloadCert_Static(e.CommandArgument.ToString())
                End If
            Case "Delete"
                DeleteData(e.CommandArgument)
                BindGrid(0)
        End Select
    End Sub

    Private Sub DeleteData(ByVal IDMSP As Integer)
        Dim MSPFacade As MSPExRegistrationFacade = New MSPExRegistrationFacade(User)
        Dim oMSPExReg As MSPExRegistration = MSPFacade.Retrieve(IDMSP)
        Try
            If Not IsNothing(oMSPExReg) AndAlso oMSPExReg.ID > 0 Then
                MSPFacade.Delete(oMSPExReg)
                MessageBox.Show("Hapus Data Berhasil")
            End If
        Catch
            MessageBox.Show("Hapus Data Gagal")
        End Try
    End Sub

    Private Function CommandDownload(ByVal lblMSPRegistrationID As String, ByVal downloadAs As String)
        Dim arr As ArrayList
        Dim crt As CriteriaComposite

        If downloadAs.ToUpper = "DM" Then
            crt = New CriteriaComposite(New Criteria(GetType(MSPExDebitMemo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crt.opAnd(New Criteria(GetType(MSPExDebitMemo), "MSPExRegistration.ID", MatchType.Exact, lblMSPRegistrationID))
            arr = New MSPExDebitMemoFacade(User).Retrieve(crt)

            If arr.Count > 0 Then
                Dim pathBaseDirectory As String = KTB.DNet.Lib.WebConfig.GetValue("MSPExDirectory")
                _path = pathBaseDirectory & "\" & CType(arr(0), MSPExDebitMemo).FileName
            End If
        ElseIf downloadAs.ToUpper = "DC" Then
            crt = New CriteriaComposite(New Criteria(GetType(MSPExDebitCharge), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crt.opAnd(New Criteria(GetType(MSPExDebitCharge), "MSPExRegistration.ID", MatchType.Exact, lblMSPRegistrationID))
            arr = New MSPExDebitChargeFacade(User).Retrieve(crt)

            If arr.Count > 0 Then
                Dim pathBaseDirectory As String = KTB.DNet.Lib.WebConfig.GetValue("MSPExDirectory")
                _path = pathBaseDirectory & "\" & CType(arr(0), MSPExDebitCharge).FileName
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
                Dim fileInfo As New FileInfo(_path)
                If (fileInfo.Exists) Then
                    Response.Redirect("../Download.aspx?file=" & _path)
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

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindGrid(0)
    End Sub

    Protected Sub ValidateSearch(ByRef mess As String)
        If Not chkRequestDate.Checked Then
            mess = mess & "Tanggal pengajuan harus di centang\n"
        End If

        If DateDiff(DateInterval.Day, DateFrom.Value, DateTo.Value) > 65 Then
            mess = mess & "Rentang tanggal pengajuan maksimal 65 Hari\n"
        End If
    End Sub

    Private Sub DownloadCert_Static(ByVal RegNumber As String)
        Dim oMSPExReg As MSPExRegistration = New MSPExRegistrationFacade(User).RetrieveByRegNumber(RegNumber)
        Dim oMSPExMaxPM As MSPExMaxPM = New MSPExMaxPMFacade(User).RetrieveByMSPExModelType(oMSPExReg.ChassisMaster.VechileColor.VechileType.VechileModel.VechileModelIndCode, oMSPExReg.MSPExMaster.MSPExType.Code)
        Dim MSPHist As MSPRegistrationHistory = DataMSPLama(oMSPExReg.ChassisMaster.ID)

        Dim filePath As String = Server.MapPath("~\DataFile\Template\MSPExt\" & oMSPExMaxPM.TemplateFileName)
        Dim directoryTemp As String = Server.MapPath("~\DataFile\Template\MSPExt\")

        Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(directoryTemp)
        If Not directoryInfo.Exists Then
            directoryInfo.Create()
        End If
        Dim finfo As FileInfo = New FileInfo(filePath)
        If Not finfo.Exists Then
            MessageBox.Show("File template MSP Extended tidak ditemukan")
            Exit Sub
        End If

        Dim filebytes As Byte() = File.ReadAllBytes(filePath)
        Using Stream As MemoryStream = New MemoryStream()
            Stream.Write(filebytes, 0, filebytes.Length)
            Using doc As WordprocessingDocument = WordprocessingDocument.Open(Stream, True)
                Dim body As DocumentFormat.OpenXml.Wordprocessing.Body = doc.MainDocumentPart.Document.Body
                Dim _paragraph As List(Of DocumentFormat.OpenXml.Wordprocessing.Paragraph) = body.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Paragraph)().ToList()
                Dim tables As List(Of DocumentFormat.OpenXml.Wordprocessing.Table) = body.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Table)().ToList()
                For Each table As DocumentFormat.OpenXml.Wordprocessing.Table In tables
                    Dim rows As List(Of DocumentFormat.OpenXml.Wordprocessing.TableRow) = table.Elements(Of DocumentFormat.OpenXml.Wordprocessing.TableRow)().ToList()
                    For Each row As DocumentFormat.OpenXml.Wordprocessing.TableRow In rows
                        For Each cell As DocumentFormat.OpenXml.Wordprocessing.TableCell In row.Descendants(Of DocumentFormat.OpenXml.Wordprocessing.TableCell)().Where(Function(x) x.InnerText.Contains("Var"))
                            Dim texts As List(Of DocumentFormat.OpenXml.Wordprocessing.Text) = cell.SelectMany(Function(p) p.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Run)()).SelectMany(Function(r) r.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Text)()).ToList()
                            Dim word As DocumentFormat.OpenXml.Wordprocessing.Text = New DocumentFormat.OpenXml.Wordprocessing.Text
                            Dim wordGabungan As String = ""
                            Dim i% = 0
                            For Each word2 As DocumentFormat.OpenXml.Wordprocessing.Text In texts
                                wordGabungan += word2.InnerText
                                If texts.Count > 1 Then
                                    If i = 0 Then
                                        word2.Text = ""
                                    End If
                                End If
                                word = word2
                                i += 1
                            Next
                            word.Text = wordGabungan
                            If word.Text.ToLower.Contains("var") Then
                                Select Case word.Text.ToLower
                                    Case "var1" 'No Cert
                                        'word.Text = oMSPExReg.ID.ToString("D6")
                                        word.Text = oMSPExReg.RegNumber.ToString()
                                    Case "var2" 'Kota, Tanggal Klik
                                        word.Text = oMSPExReg.Dealer.City.CityName & ", " & Date.Now.ToString("dd MMMM yyyy")
                                    Case "var3" 'Nama Cust
                                        word.Text = oMSPExReg.MSPCustomer.Name1
                                    Case "var4" 'Chas Num
                                        word.Text = oMSPExReg.ChassisMaster.ChassisNumber
                                    Case "var5" 'Engin Num
                                        word.Text = oMSPExReg.ChassisMaster.EngineNumber
                                    Case "var6" 'Created Time
                                        If MSPHist.ID <> 0 Then
                                            Dim expiredDateMSPLama As Date = DateAdd(DateInterval.Year, MSPHist.MSPMaster.Duration, MSPHist.RegistrationDate)
                                            If expiredDateMSPLama >= Date.Now AndAlso MSPHist.MSPMaster.MSPKm >= oMSPExReg.MileAge Then
                                                word.Text = "Setelah MSP Original Berakhir (4 Tahun / 50.000 KM)"
                                            Else
                                                word.Text = oMSPExReg.CreatedTime.ToString("dd/MM/yyyy")
                                            End If
                                        Else
                                            word.Text = oMSPExReg.CreatedTime.ToString("dd/MM/yyyy")
                                        End If
                                    Case "var7" 'ValidDate to
                                        If MSPHist.ID <> 0 Then
                                            Dim expiredDateMSPLama As Date = DateAdd(DateInterval.Year, MSPHist.MSPMaster.Duration, MSPHist.RegistrationDate)
                                            If expiredDateMSPLama >= Date.Now AndAlso MSPHist.MSPMaster.MSPKm >= oMSPExReg.MileAge Then
                                                word.Text = oMSPExReg.MSPExMaster.Duration & " Tahun setelah Extended Smart Package Aktif"
                                            Else
                                                word.Text = oMSPExReg.ValidDateTo.ToString("dd/MM/yyyy")
                                            End If
                                        Else
                                            word.Text = oMSPExReg.ValidDateTo.ToString("dd/MM/yyyy")
                                        End If
                                End Select
                            End If
                        Next
                    Next
                Next
            End Using
            Dim bytes As Byte() = Stream.ToArray()
            'Dim strFileName As String = oMSPExReg.ID.ToString("D6") + ".docx"
            Dim strFileName As String = oMSPExReg.RegNumber.ToString() + ".docx"
            Dim tempPath As String = Server.MapPath("~\DataTemp\") + strFileName
            'Dim tempPath As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") + "DataTemp\" + strFileName

            If Not System.IO.Directory.Exists(Path.GetDirectoryName(tempPath)) Then
                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(tempPath))
            End If

            If System.IO.File.Exists(tempPath) Then
                System.IO.File.Delete(tempPath)
            End If
            Try
                Dim wFile As System.IO.FileStream
                wFile = New FileStream(tempPath, FileMode.Append)
                wFile.Write(bytes, 0, bytes.Length)
                wFile.Close()
            Catch ex As IOException
                Dim debugs = ""
            End Try

            Dim docs As New Document()
            docs.LoadFromFile(tempPath)

            docs.SaveToFile(tempPath, Spire.Doc.FileFormat.Docx)

            DownloadPDFFile(tempPath, oMSPExReg.RegNumber.ToString())
        End Using
    End Sub

    Private Sub DownloadCert_Normal(ByVal RegNumber As String)
        Dim oMSPExReg As MSPExRegistration = New MSPExRegistrationFacade(User).RetrieveByRegNumber(RegNumber)
        Dim MSPHist As MSPRegistrationHistory = DataMSPLama(oMSPExReg.ChassisMaster.ID)

        'Dim filePath2 As String = "MSPCertTemplate2.docx"
        'Dim filePath4 As String = "MSPCertTemplate4.docx"
        'Dim filePathDP As String = "Fleet_Discount_Template.docx"

        Dim filePath As String = Server.MapPath("~\DataFile\Template\MSPExt\MSPCertTemplate.docx")
        Dim directoryTemp As String = Server.MapPath("~\DataFile\Template\MSPExt\")

        Dim objAppConfigFacade As AppConfigFacade = New AppConfigFacade(User)
        Dim objAppConfig As AppConfig = objAppConfigFacade.Retrieve("MSPExtendedQX")

        If objAppConfig.Value.Contains(",") Then
            Dim QXVal As String() = objAppConfig.Value.Split(",")
            For Each Vals As String In QXVal
                If oMSPExReg.ChassisMaster.VechileColor.VechileType.VechileModel.VechileModelCode = Vals Then
                    filePath = Server.MapPath("~\DataFile\Template\MSPExt\MSPCertTemplateQX.docx")
                    Exit For
                End If
            Next
        Else
            If oMSPExReg.ChassisMaster.VechileColor.VechileType.VechileModel.VechileModelCode = objAppConfig.Value Then
                filePath = Server.MapPath("~\DataFile\Template\MSPExt\MSPCertTemplateQX.docx")
            End If
        End If

        Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(directoryTemp)
        If Not directoryInfo.Exists Then
            directoryInfo.Create()
        End If
        Dim finfo As FileInfo = New FileInfo(filePath)
        If Not finfo.Exists Then
            MessageBox.Show("File template MSP Extended tidak ditemukan")
            Exit Sub
        End If

        Dim filebytes As Byte() = File.ReadAllBytes(filePath)
        Using Stream As MemoryStream = New MemoryStream()
            Stream.Write(filebytes, 0, filebytes.Length)
            Using doc As WordprocessingDocument = WordprocessingDocument.Open(Stream, True)
                Dim body As DocumentFormat.OpenXml.Wordprocessing.Body = doc.MainDocumentPart.Document.Body
                Dim _paragraph As List(Of DocumentFormat.OpenXml.Wordprocessing.Paragraph) = body.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Paragraph)().ToList()
                Dim tables As List(Of DocumentFormat.OpenXml.Wordprocessing.Table) = body.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Table)().ToList()
                For Each table As DocumentFormat.OpenXml.Wordprocessing.Table In tables
                    Dim rows As List(Of DocumentFormat.OpenXml.Wordprocessing.TableRow) = table.Elements(Of DocumentFormat.OpenXml.Wordprocessing.TableRow)().ToList()
                    For Each row As DocumentFormat.OpenXml.Wordprocessing.TableRow In rows
                        For Each cell As DocumentFormat.OpenXml.Wordprocessing.TableCell In row.Descendants(Of DocumentFormat.OpenXml.Wordprocessing.TableCell)().Where(Function(x) x.InnerText.Contains("Var"))
                            Dim texts As List(Of DocumentFormat.OpenXml.Wordprocessing.Text) = cell.SelectMany(Function(p) p.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Run)()).SelectMany(Function(r) r.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Text)()).ToList()
                            Dim word As DocumentFormat.OpenXml.Wordprocessing.Text = New DocumentFormat.OpenXml.Wordprocessing.Text
                            Dim wordGabungan As String = ""
                            Dim i% = 0
                            For Each word2 As DocumentFormat.OpenXml.Wordprocessing.Text In texts
                                wordGabungan += word2.InnerText
                                If texts.Count > 1 Then
                                    If i = 0 Then
                                        word2.Text = ""
                                    End If
                                End If
                                word = word2
                                i += 1
                            Next
                            word.Text = wordGabungan
                            If word.Text.ToLower.Contains("var") Then
                                Select Case word.Text.ToLower
                                    Case "var0" 'No Cert
                                        word.Text = oMSPExReg.RegNumber.Substring(4)
                                        'word.Text = oMSPExReg.ID.ToString("D6")
                                    Case "var1" 'Kota, Tanggal Klik
                                        word.Text = oMSPExReg.Dealer.City.CityName & ", " & Date.Now.ToString("dd MMMM yyyy")
                                    Case "var2" 'Nama Cust
                                        word.Text = oMSPExReg.MSPCustomer.Name1
                                    Case "var3" 'Chas Num
                                        word.Text = oMSPExReg.ChassisMaster.ChassisNumber
                                    Case "var4" 'Engin Num
                                        word.Text = oMSPExReg.ChassisMaster.EngineNumber
                                    Case "var5" 'Created Time
                                        If MSPHist.ID <> 0 Then
                                            Dim expiredDateMSPLama As Date = DateAdd(DateInterval.Year, MSPHist.MSPMaster.Duration, MSPHist.RegistrationDate)
                                            If expiredDateMSPLama >= Date.Now AndAlso MSPHist.MSPMaster.MSPKm >= oMSPExReg.MileAge Then
                                                word.Text = "Setelah MSP Original Berakhir (4 Tahun / 50.000 KM)"
                                            Else
                                                word.Text = oMSPExReg.CreatedTime
                                            End If
                                        Else
                                            word.Text = oMSPExReg.CreatedTime
                                        End If
                                    Case "var6" 'ValidDate to
                                        If MSPHist.ID <> 0 Then
                                            Dim expiredDateMSPLama As Date = DateAdd(DateInterval.Year, MSPHist.MSPMaster.Duration, MSPHist.RegistrationDate)
                                            If expiredDateMSPLama >= Date.Now AndAlso MSPHist.MSPMaster.MSPKm >= oMSPExReg.MileAge Then
                                                word.Text = oMSPExReg.MSPExMaster.Duration & " Tahun setelah Extended Smart Package Aktif / " & oMSPExReg.MSPExMaster.MSPExKM.ToString("N0") & " km setelah Extended Smart Package Aktif"
                                            Else
                                                word.Text = oMSPExReg.ValidDateTo & " atau " & oMSPExReg.MSPExMaster.MSPExKM.ToString("N0") & " km dari odometer saat pendaftaran"
                                            End If
                                        Else
                                            'word.Text = oMSPExReg.ValidDateTo
                                            word.Text = oMSPExReg.ValidDateTo & " atau " & oMSPExReg.MSPExMaster.MSPExKM.ToString("N0") & " km dari odometer saat pendaftaran"
                                        End If
                                    Case "var7"
                                        word.Text = GetMaxPM(oMSPExReg.MSPExMaster.MSPExType).Count & "X"
                                End Select
                            End If
                        Next
                    Next
                Next
            End Using
            Dim bytes As Byte() = Stream.ToArray()
            Dim strFileName As String = oMSPExReg.RegNumber.Substring(4) + ".docx"
            'Dim strFileName As String = oMSPExReg.ID.ToString("D6") + ".docx"
            Dim tempPath As String = Server.MapPath("~\DataTemp\") + strFileName
            'Dim tempPath As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") + "DataTemp\" + strFileName

            If Not System.IO.Directory.Exists(Path.GetDirectoryName(tempPath)) Then
                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(tempPath))
            End If

            If System.IO.File.Exists(tempPath) Then
                System.IO.File.Delete(tempPath)
            End If
            Try
                Dim wFile As System.IO.FileStream
                wFile = New FileStream(tempPath, FileMode.Append)
                wFile.Write(bytes, 0, bytes.Length)
                wFile.Close()
            Catch ex As IOException
                Dim debugs = ""
            End Try

            Dim docs As New Document()
            docs.LoadFromFile(tempPath)
            Dim savePDFOK As Boolean = False
            CreateTableCap(docs, RegNumber, 0, savePDFOK)
            If savePDFOK Then
                docs.SaveToFile(tempPath, Spire.Doc.FileFormat.Docx)
                DownloadPDFFile(tempPath, oMSPExReg.RegNumber.Substring(4))
            End If
        End Using
    End Sub

    Function CreateTableCap(ByVal doc As Document, ByVal RegNumber As String, ByVal j As Integer, ByRef savePDFOK As Boolean)
        Dim section As Spire.Doc.Section = doc.Sections(j)
        'Dim selection As Spire.Doc.Documents.TextSelection = doc.FindString("gridData_Vehicle_Owned", True, True)
        Dim selection As Spire.Doc.Documents.TextSelection = doc.FindString("varcap", True, True)
        Dim range As Spire.Doc.Fields.TextRange = selection.GetAsOneRange()
        Dim paragraph As Spire.Doc.Documents.Paragraph = range.OwnerParagraph
        Dim body As Spire.Doc.Body = paragraph.OwnerTextBody
        Dim index As Integer = body.ChildObjects.IndexOf(paragraph)
        Dim table As Spire.Doc.Table = section.AddTable(True)
        table.TableFormat.IsBreakAcrossPages = False

        Dim oMSPExReg As MSPExRegistration = New MSPExRegistrationFacade(User).RetrieveByRegNumber(RegNumber)

        'Create Header and Data
        'Dim Header() As String = {"Cap PM 1", "Cap PM 2", "Cap PM 3", "Cap PM 4", "Cap PM 5", "Cap PM 6"}
        Dim MaxPM As Integer = GetMaxPM(oMSPExReg.MSPExMaster.MSPExType).Count
        'Dim MaxPM As Integer = 16
        If MaxPM > 0 Then
            'table.ResetCells(2, Header.Length)
            table.ResetCells(2, MaxPM)
            table.TableFormat.Positioning.HorizPositionAbs = Spire.Doc.HorizontalPosition.Center
            table.TableFormat.Borders.BorderType = Spire.Doc.Documents.BorderStyle.None

            Dim width As Spire.Doc.PreferredWidth = New Spire.Doc.PreferredWidth(Spire.Doc.WidthType.Percentage, MaxPM * 15)
            table.PreferredWidth = width

            'Header Row
            Dim FRow As Spire.Doc.TableRow = table.Rows(0)

            'Row Height
            FRow.Height = 5
            'Header Format
            'For i As Integer = 0 To Header.Length - 1
            For i As Integer = 0 To MaxPM - 1
                'Cell Alignment
                Dim p As Spire.Doc.Documents.Paragraph = FRow.Cells(i).AddParagraph()
                FRow.Cells(i).CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Top
                FRow.Cells(i).CellFormat.Borders.BorderType = Spire.Doc.Documents.BorderStyle.Hairline
                p.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center
                'Data Format
                Dim TR As Spire.Doc.Fields.TextRange = p.AppendText("Cap PM " & i + 1)
                TR.CharacterFormat.FontName = "MMC Office"
                TR.CharacterFormat.FontSize = 8
                TR.CharacterFormat.TextColor = Color.Black
                TR.CharacterFormat.Bold = True
                FRow.Cells(i).Width = 10
                FRow.Cells(i).CellFormat.BackColor = Color.White

            Next i

            'Data Row
            Dim DataRow As Spire.Doc.TableRow = table.Rows(1)

            'Row Height
            DataRow.Height = 30

            'C Represents Column.
            For c As Integer = 0 To MaxPM - 1
                'Cell Alignment
                DataRow.Cells(c).CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Bottom
                'Fill Data in Rows
                Dim p2 As Spire.Doc.Documents.Paragraph = DataRow.Cells(c).AddParagraph()
                Dim TR2 As Spire.Doc.Fields.TextRange = p2.AppendText("Date:")
                'Format Cells
                p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left
                TR2.CharacterFormat.FontName = "MMC Office"
                TR2.CharacterFormat.FontSize = 5
                TR2.CharacterFormat.TextColor = Color.Black
                DataRow.Cells(c).CellFormat.Borders.BorderType = Spire.Doc.Documents.BorderStyle.Hairline
                DataRow.Cells(c).CellFormat.BackColor = Color.White
            Next c


            body.ChildObjects.Remove(paragraph)
            body.ChildObjects.Insert(index, table)
            savePDFOK = True
        Else
            MessageBox.Show("Nomor rangka " & oMSPExReg.ChassisMaster.ChassisNumber & " tidak mendapatkan sertifikat")
        End If
    End Function

    Private Sub DownloadPDFFile(ByVal tempPath As String, ByVal strFileName As String)
        Try
            strFileName = strFileName & ".pdf"
            Dim strDestFile As String = Server.MapPath("~\DataTemp\PDF\") & strFileName
            Dim document As Document = New Document()
            document.LoadFromFile(tempPath)
            document.SaveToFile(strDestFile, Spire.Doc.FileFormat.PDF, Response, Spire.Doc.HttpContentType.Attachment)
            'document.SaveToFile(strDestFile, Spire.Doc.FileFormat.PDF)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function GetMaxPM(ByVal oMSPExType As MSPExType) As ArrayList
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExMappingtoFSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(MSPExMappingtoFSKind), "MSPExType.ID", MatchType.Exact, oMSPExType.ID))
        Return New MSPExMappingtoFSKindFacade(User).Retrieve(crit)
    End Function

    Private Function DataMSPLama(ByVal ChassisMasterID As Integer) As MSPRegistrationHistory
        Dim critMSP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPRegistrationHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critMSP.opAnd(New Criteria(GetType(MSPRegistrationHistory), "MSPRegistration.ChassisMaster.ID", MatchType.Exact, ChassisMasterID))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(MSPRegistrationHistory), "CreatedTime", Sort.SortDirection.DESC))

        Dim arlMSP As ArrayList = New MSPRegistrationHistoryFacade(User).Retrieve(critMSP, sortColl)
        If arlMSP.Count > 0 Then
            Return New MSPRegistrationHistoryFacade(User).Retrieve(critMSP, sortColl)(0)
        End If
        Return New MSPRegistrationHistory
    End Function

    Protected Sub ddlVechileModel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVechileModel.SelectedIndexChanged
        BindddlTipe(ddlCategoryV.SelectedItem.ToString)
    End Sub

    Protected Sub ddlCategoryV_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategoryV.SelectedIndexChanged
        ddlVechileModel.Items.Clear()
        ddlVechileType.Items.Clear()
        If ddlCategoryV.SelectedIndex <> 0 Then
            CommonFunction.BindVehicleSubCategoryToDDL2(ddlVechileModel, ddlCategoryV.SelectedItem.Text)
            BindddlTipe(ddlCategoryV.SelectedItem.ToString)
            ddlVechileModel.SelectedIndex = 0
        End If
    End Sub

    Private Sub BindddlTipe(ByVal Category As String)
        ddlVechileType.Items.Clear()
        If ddlVechileModel.SelectedIndex <> 0 Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "Category.CategoryCode", MatchType.Exact, Category))
            'CommonFunction.SetVehicleSubCategoryCriterias(ddlVechileModel, ddlCategoryV.SelectedItem.Text, criterias, "VechileType")

            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlVechileModel.SelectedValue
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("VechileTypeCode")) Then
                sortColl.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            ddlVechileType.DataSource = New VechileTypeFacade(User).Retrieve(criterias, sortColl)
            ddlVechileType.DataTextField = "VechileTypeCode"
            ddlVechileType.DataValueField = "ID"
            ddlVechileType.DataBind()
        End If
        ddlVechileType.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Private Function GetSelectedItem(ByVal listboxStatus As ListBox) As String
        '-- Items selected in listbox

        Dim _strStatus As String = String.Empty
        For Each item As ListItem In listboxStatus.Items
            If item.Selected Then
                If _strStatus = String.Empty Then
                    _strStatus = item.Value
                Else
                    _strStatus = _strStatus & "," & item.Value
                End If
            End If
        Next
        Return _strStatus
    End Function

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Dim mess As String = String.Empty
        ValidateSearch(mess)
        If mess.Trim.Length > 0 Then
            MessageBox.Show(mess)
            Exit Sub
        End If
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        Dim crits As CriteriaComposite
        If dtgMSPRegistrationList.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        If Not IsNothing(_sessHelper.GetSession(_searchSess)) Then
            Dim criteria As CriteriaComposite = _sessHelper.GetSession(_searchSess)
            arrData = New MSPExRegistrationFacade(User).Retrieve(criteria)
        Else

        End If
        'Dim SQLQuery As String = "SELECT a.ID, ( " _
        '                        + "SELECT TOP 1 " _
        '                        + "fk.KindCode " _
        '                        + "FROM dbo.FreeService fs " _
        '                        + "INNER JOIN dbo.FSKind fk ON fk.ID = fs.FSKindID " _
        '                        + "WHERE 1 = 1 " _
        '                        + "AND fs.RowStatus = 0 " _
        '                        + "AND fs.ChassisMasterID = a.ChassisMasterID " _
        '                        + "AND fs.CreatedTime <= a.CreatedTime " _
        '                        + "AND EXISTS " _
        '                        + "( " _
        '                        + "SELECT " _
        '                        + "                'CUmi' " _
        '                        + "FROM dbo.MappingMSPtoFSKind mk1 " _
        '                        + "WHERE 1 = 1 " _
        '                        + "AND mk1.RowStatus = 0 " _
        '                        + "AND mk1.FSKindID = fs.FSKindID " _
        '                        + "UNION " _
        '                        + "SELECT " _
        '                        + "                    'CUmi' " _
        '                        + "FROM dbo.MSPExMappingtoFSKind mk2 " _
        '                        + "WHERE 1 = 1 " _
        '                        + "AND mk2.RowStatus = 0 " _
        '                        + "AND mk2.FSKindID = fs.FSKindID " _
        '                        + ") " _
        '                        + "ORDER BY FSKindID DESC  " _
        '                        + ") AS [KKASR] " _
        '                        + "FROM dbo.MSPExRegistration a "


        'Dim dtSet As DataSet = New MSPExRegistrationFacade(User).RetrieveSpDS(SQLQuery)
        'Dim dtTbl As DataTable = dtSet.Tables(0)

        If arrData.Count > 0 Then
            'CreateExcel("DaftarMSPExtendedDaftarRegistration", arrData, dtTbl)
            CreateExcel("DaftarMSPExtendedDaftarRegistration", arrData)
        End If

    End Sub

    'Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList, ByVal FsData As DataTable)
    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim oD As Dealer
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = FileName
            ws.Cells("A3").Value = "No" '1
            ws.Cells("B3").Value = "Status" '2
            ws.Cells("C3").Value = "Dealer Code" '3
            ws.Cells("D3").Value = "No. Reg Pembayaran" '4
            ws.Cells("E3").Value = "No. MSP Extended" '5
            ws.Cells("F3").Value = "No. Rangka" '6
            ws.Cells("G3").Value = "Nama Kendaraan" '7
            ws.Cells("H3").Value = "Tipe MSP Extended" '8
            ws.Cells("I3").Value = "Tanggal Pengajuan" '9
            ws.Cells("J3").Value = "Kilometer Pendaftaran" '10
            ws.Cells("K3").Value = "Tanggal Faktur Kendaraan" '11
            ws.Cells("L3").Value = "Tanggal Expired ESP" '12
            ws.Cells("M3").Value = "Nama" '13
            ws.Cells("N3").Value = "Alamat" '14
            ws.Cells("O3").Value = "Kelurahan" '15
            ws.Cells("P3").Value = "Kecamatan" '16
            ws.Cells("Q3").Value = "Kota/Kabupaten" '17
            ws.Cells("R3").Value = "No Telpon" '18
            ws.Cells("S3").Value = "Valid Sampai KM" '19
            ws.Cells("T3").Value = "Harga" '20
            ws.Cells("U3").Value = "Total Harga + PPN" '21
            ws.Cells("V3").Value = "Kind Code Akhir" '22
            ws.Cells("W3").Value = "Kind Code Akhir Saat Registrasi" '23
            ws.Cells("X3").Value = "Status" '24



            Dim idx As Integer = 0
            Dim dealerCode As String
            Dim checked As String = ""
            Dim ddlCategory As String = ""
            Dim ddlVModel As String = ""
            Dim ddlVType As String = ""
            Dim ddlMsType As String = ""
            If chkRequestDate.Checked Then
                checked = "True"
            End If
            If ddlCategoryV.SelectedIndex > 0 Then ddlCategory = ddlCategoryV.SelectedValue
            If ddlMSPType.SelectedIndex > 0 Then ddlMsType = ddlMSPType.SelectedValue
            If ddlVechileModel.SelectedIndex > 0 Then ddlVModel = ddlVechileModel.SelectedValue
            If ddlVechileType.SelectedIndex > 0 Then ddlVType = ddlVechileType.SelectedValue
            Dim SelectedStatus As String = ""
            If lboxStatus.SelectedIndex <> -1 Then
                SelectedStatus = GetSelectedItem(lboxStatus)
            End If

            If objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                dealerCode = objDealer.DealerCode
            Else
                If txtKodeDealer.Text.Trim.Length > 0 Then
                    dealerCode = txtKodeDealer.Text.Trim.Replace(";", ",")
                End If
            End If

            Dim sts As String = ""
            If chkRequestDate.Checked Then
                sts = "1"
            End If


            'Dim sql As String = "EXEC SP_DownloadAllMSPExDaftarRegistration '" + dealerCode + "','" + txtRegNo.Text + "','" + txtMSPNo.Text + "','" + txtChassisNumber.Text + "','" + ddlCategory + "','" + ddlVModel + "','" + ddlVType + "','" + ddlMsType + "','" + DateFrom.Value.ToString("yyyy-MM-dd 00:00:00") + "','" + DateTo.Value.ToString("yyyy-MM-dd 23:59:59") + "','" + SelectedStatus + "','" + sts + "'"
            Dim sql As String = "EXEC SP_DownloadAllMSPExDaftarRegistration '" + dealerCode + "','" + txtRegNo.Text + "','" + txtMSPNo.Text + "','" + txtChassisNumber.Text + "','" + ddlCategory + "','" + ddlVModel + "','" + ddlVType + "','" + ddlMsType + "','" + DateFrom.Value.ToString("yyyy/MM/dd") + "','" + DateTo.Value.ToString("yyyy/MM/dd") + "','" + SelectedStatus + "','" + sts + "'"
            Dim arrDataAll As DataTable = New MSPExRegistrationFacade(User).RetrieveDataTable(sql)

            If arrDataAll.Rows.Count > 0 Then
                For Each item As DataRow In arrDataAll.Rows
                    ws.Cells(idx + 4, 1).Value = idx + 1
                    ws.Cells(idx + 4, 2).Value = EnumMSPEx.GetStringValue(item("Status"))
                    ws.Cells(idx + 4, 3).Value = item("DealerCode").ToString
                    ws.Cells(idx + 4, 4).Value = item("RegPembayaran").ToString
                    ws.Cells(idx + 4, 5).Value = item("RegNumber").ToString
                    ws.Cells(idx + 4, 6).Value = item("ChassisNumber").ToString
                    ws.Cells(idx + 4, 7).Value = item("VechileModelIndCode").ToString
                    ws.Cells(idx + 4, 8).Value = item("Code").ToString
                    ws.Cells(idx + 4, 9).Value = item("CreatedTime").ToString
                    ws.Column(9).Style.Numberformat.Format = "DD/MM/YYYY"
                    ws.Cells(idx + 4, 10).Value = item("MileAge").ToString
                    Try
                        ws.Cells(idx + 4, 11).Value = item("FakturDate").ToString
                        ws.Column(11).Style.Numberformat.Format = "DD/MM/YYYY"
                    Catch
                    End Try
                    ws.Cells(idx + 4, 12).Value = item("ValidDateTo").ToString
                    ws.Column(12).Style.Numberformat.Format = "DD/MM/YYYY"
                    ws.Cells(idx + 4, 13).Value = item("Name1").ToString
                    ws.Cells(idx + 4, 14).Value = item("Alamat").ToString
                    ws.Cells(idx + 4, 15).Value = item("Kelurahan").ToString
                    ws.Cells(idx + 4, 16).Value = item("Kecamatan").ToString
                    Try
                        ws.Cells(idx + 4, 17).Value = item("CityName").ToString
                    Catch
                    End Try
                    ws.Cells(idx + 4, 18).Value = item("PhoneNo").ToString
                    ws.Cells(idx + 4, 19).Value = item("ValidKMTo").ToString
                    ws.Cells(idx + 4, 20).Value = item("Amount").ToString
                    ws.Cells(idx + 4, 21).Value = (item("Amount") * 110 / 100)
                    ws.Cells(idx + 4, 22).Value = item("FS").ToString
                    'ws.Cells(idx + 4, 23).Value = item("FS").ToString

                    'For Each t As DataRow In FsData.Rows
                    '    If CInt(t("ID")) = item("ID") Then
                    '        ws.Cells(idx + 4, 23).Value = t("KKASR").ToString
                    '        Exit For
                    '    Else
                    '        ws.Cells(idx + 4, 23).Value = ""
                    '    End If
                    'Next
                    'ws.Cells(idx + 4, 24).Value = StatusStoryService(item("RegNumber").ToString)

                    ws.Cells(idx + 4, 23).Value = item("KKASR").ToString

                    ws.Cells(idx + 4, 24).Value = item("iStatus").ToString
                    idx = idx + 1
                Next
            End If


            'For i As Integer = 0 To Data.Count - 1
            '    Dim item As MSPExRegistration = Data(i)
            '    ws.Cells(idx + 4, 1).Value = idx + 1
            '    ws.Cells(idx + 4, 2).Value = EnumMSPEx.GetStringValue(item.Status)
            '    ws.Cells(idx + 4, 3).Value = item.Dealer.DealerCode

            '    Dim dtMPayment As MSPExPaymentDetail = New MSPExPaymentDetailFacade(User).RetrieveID(item.ID)
            '    If Not IsNothing(dtMPayment) AndAlso dtMPayment.ID <> 0 Then
            '        ws.Cells(idx + 4, 4).Value = dtMPayment.MSPExPayment.RegNumber
            '    Else
            '        ws.Cells(idx + 4, 4).Value = ""
            '    End If

            '    ws.Cells(idx + 4, 5).Value = item.RegNumber
            '    ws.Cells(idx + 4, 6).Value = item.ChassisMaster.ChassisNumber
            '    ws.Cells(idx + 4, 7).Value = item.MSPExMaster.VechileType.VechileModel.VechileModelIndCode
            '    ws.Cells(idx + 4, 8).Value = item.MSPExMaster.MSPExType.Code
            '    ws.Cells(idx + 4, 9).Value = item.CreatedTime
            '    ws.Column(9).Style.Numberformat.Format = "DD/MM/YYYY"
            '    ws.Cells(idx + 4, 10).Value = item.MileAge
            '    Try
            '        ws.Cells(idx + 4, 11).Value = item.ChassisMaster.EndCustomer.FakturDate
            '        ws.Column(11).Style.Numberformat.Format = "DD/MM/YYYY"
            '    Catch
            '    End Try
            '    ws.Cells(idx + 4, 12).Value = item.ValidDateTo
            '    ws.Column(12).Style.Numberformat.Format = "DD/MM/YYYY"
            '    ws.Cells(idx + 4, 13).Value = item.MSPCustomer.Name1    'ChassisMaster.EndCustomer.Name1
            '    ws.Cells(idx + 4, 14).Value = item.MSPCustomer.Alamat  'ChassisMaster.EndCustomer.Customer.Alamat
            '    ws.Cells(idx + 4, 15).Value = item.MSPCustomer.Kelurahan 'ChassisMaster.EndCustomer.Customer.Kelurahan
            '    ws.Cells(idx + 4, 16).Value = item.MSPCustomer.Kecamatan   'ChassisMaster.EndCustomer.Customer.Kecamatan
            '    Try
            '        ws.Cells(idx + 4, 17).Value = item.MSPCustomer.City.CityName   'ChassisMaster.EndCustomer.Customer.City.CityName
            '    Catch
            '    End Try
            '    ws.Cells(idx + 4, 18).Value = item.MSPCustomer.PhoneNo  'ChassisMaster.EndCustomer.Customer.PhoneNo
            '    ws.Cells(idx + 4, 19).Value = item.ValidKMTo
            '    ws.Cells(idx + 4, 20).Value = item.MSPExMaster.Amount
            '    ' ws.Column(19).Style.Numberformat.Format = "#,##0"
            '    ws.Cells(idx + 4, 21).Value = (item.MSPExMaster.Amount * 110 / 100)
            '    'Dim strSql3 As String = "SELECT FS.kindcode FROM MSPExregistration a INNER Join Chassismaster cm on a.chassismasterid = cm.ID OUTER APPLY " _
            '    '                        & " (SELECT TOP 1 fsk.kindcode FROM dbo.FreeService fs INNER Join FSKind fsk ON [FS].[FSKindiD] = fsk.ID WHERE a.chassismasterid = FS.chassismasterid " _
            '    '                        & " AND fs.RowStatus = 0 ORDER BY  ServiceDate DESC) FS WHERE a.ID = '" & item.ID & "' AND cm.ChassisNumber = '" & item.ChassisMaster.ChassisNumber & "' "
            '    Dim strSQL As String =
            '    " SELECT " +
            '    " FS.kindcode as [FS] " +
            '    " FROM MSPExregistration a " +
            '    " INNER Join Chassismaster cm on a.chassismasterid = cm.id " +
            '    " OUTER APPLY " +
            '    " (SELECT TOP 1 KindCode " +
            '    " FROM dbo.FreeService fs " +
            '    " INNER Join FSKind ON [FS].[FSKindiD] = FSkind.ID " +
            '    " WHERE a.chassismasterid = FS.chassismasterid " +
            '    " AND fs.RowStatus = 0 " +
            '    " ORDER BY ServiceDate DESC) FS " +
            '    " WHERE a.ID = '" & item.ID & "' AND cm.ChassisNumber = '" & item.ChassisMaster.ChassisNumber & "' "
            '    Dim dS As DataSet = New MSPExRegistrationFacade(User).DoRetrieveDataset(strSQL)
            '    'If dS.ToString <> "" Then
            '    '    ws.Cells(idx + 4, 21).Value = dS.ToString
            '    'Else
            '    '    ws.Cells(idx + 4, 21).Value = ""
            '    ''End If
            '    'For Each drJumlah As DataRow In dS.Tables(0).Rows
            '    '    If drJumlah("FS").ToString <> "" Then
            '    '        ws.Cells(idx + 4, 22).Value = drJumlah("FS").ToString
            '    '    Else
            '    '        ws.Cells(idx + 4, 22).Value = ""
            '    '    End If
            '    'Next
            '    ws.Cells(idx + 4, 22).Value = From drJumlah As DataRow In dS.Tables(0).Rows
            '                                    Where drJumlah.Field(Of String)("FS") <> ""
            '                                    Select drJumlah.Field(Of String)("FS")

            '    ' ws.Column(20).Style.Numberformat.Format = "#,##0"
            '    'For Each t As DataRow In FsData.Rows
            '    '    If CInt(t("ID")) = item.ID Then
            '    '        ws.Cells(idx + 4, 23).Value = t("KKASR").ToString
            '    '        Exit For
            '    '    Else
            '    '        ws.Cells(idx + 4, 23).Value = ""
            '    '    End If
            '    'Next
            '    ws.Cells(idx + 4, 22).Value = From t As DataRow In FsData.Rows
            '                                    Where t.Field(Of Integer)("ID") <> item.ID
            '                                    Select t.Field(Of String)("KKASR")

            '    ws.Cells(idx + 4, 24).Value = StatusStoryService(item.RegNumber)
            '    idx = idx + 1
            'Next

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
        Response.Clear()

        'Response.AppendHeader("Content-Length", fileBytes.Length.ToString())
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName)
        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"  'xlsx
        Response.ContentType = "application/vnd.ms-excel" 'xls
        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()

    End Sub

    Protected Sub btnProses_Click(sender As Object, e As EventArgs) Handles btnProses.Click
        If ddlProses.SelectedIndex > 0 Then
            If hdConfirm.Value = "-1" Then
                RegisterStartupScript("Confirm", "<script>ShowConfirm('Apakah yakin ingin menvalidasi?', 'btnProses');</script>")
                Return
            Else
                hdConfirm.Value = "-1"
            End If
            SendWSM()
            BindGrid(0)
        End If
    End Sub

    Private Function sSuffix() As String
        Return DateTime.Now.ToString("yyyyMMddHHmmss")
    End Function

    Private Sub SendWSM()
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer) 'Just TEST

        Dim success As Boolean = False
        Dim sTimestamp As String = sSuffix()
        Dim FileNameSAP As String = KTB.DNet.Lib.WebConfig.GetValue("SAPSERVERFOLDER") & "\Service\MSPEXT\Registration\MSPExtRegistration" & sTimestamp & ".txt"
        Dim FileNameLocal As String = Server.MapPath("") & "\..\DataTemp\MSPExtRegistration" & sTimestamp & ".txt"

        Try
            success = imp.Start
            If success Then
                Dim CheckedWSCItemColl As ArrayList = New ArrayList
                Dim arlTransferedToSAP As New ArrayList
                CheckedWSCItemColl = GetCheckedItems()

                Dim nSavedData As Integer = AppendText(CheckedWSCItemColl, FileNameLocal, FileNameSAP, arlTransferedToSAP)
                If nSavedData < 1 Then
                    Dim sIndicator As String = ""
                    sIndicator = IIf(nSavedData = -1, ".", IIf(nSavedData = -1, ",", ""))
                    MessageBox.Show("Rilis data gagal" & sIndicator)
                End If

                Dim objMSPExRegColl As ArrayList = New ArrayList
                If arlTransferedToSAP.Count > 0 Then
                    For Each ObjMSPExRegistration As MSPExRegistration In CheckedWSCItemColl
                        If ObjMSPExRegistration.Status = CType(EnumMSPEx.MSPExStatus.Baru, Short) Then
                            ObjMSPExRegistration.Status = EnumMSPEx.MSPExStatus.Validasi
                            ObjMSPExRegistration.IsTransfertoSAP = 1
                            objMSPExRegColl.Add(ObjMSPExRegistration)
                        End If
                    Next
                    Dim nResult = New MSPExRegistrationFacade(User).UpdateRegUploadedToSAP(objMSPExRegColl)
                    If nResult = 0 Then
                        MessageBox.Show("Send To SAP Sukses")
                        InsertStatusChangeHistory(CheckedWSCItemColl, EnumMSPEx.MSPExStatus.Validasi, EnumMSPEx.MSPExStatus.Baru)
                    Else
                        MessageBox.Show("Send To SAP gagal")
                    End If
                End If

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Send To SAP gagal !")
        End Try
    End Sub

    Private Sub InsertStatusChangeHistory(ByVal _regNumbers As ArrayList, ByVal newStatus As String, ByVal oldStatus As String)
        Try
            For Each item As MSPExRegistration In _regNumbers
                Dim objNewStatus As New StatusChangeHistory
                objNewStatus.DocumentType = 1
                objNewStatus.DocumentRegNumber = item.RegNumber
                objNewStatus.OldStatus = CInt(oldStatus)
                objNewStatus.NewStatus = CInt(newStatus)
                objNewStatus.RowStatus = 0

                Dim iReturn As Integer = New StatusChangeHistoryFacade(User).Insert(objNewStatus)
            Next
        Catch ex As Exception
            Throw New Exception("Gagal dalam menginput history")
        End Try
    End Sub

    Private Function GetCheckedItems() As ArrayList
        Dim _return As New ArrayList
        For Each item As DataGridItem In dtgMSPRegistrationList.Items
            Dim chk As CheckBox = CType(item.FindControl("chkSelect"), CheckBox)
            If (chk.Checked) Then
                Dim lblMSPCode As Label = CType(item.FindControl("lblMSPCode"), Label)
                Dim obj As MSPExRegistration = New MSPExRegistrationFacade(User).RetrieveByRegNumber(lblMSPCode.Text)
                If obj.IsTransfertoSAP = 0 Then
                    _return.Add(obj)
                End If
            End If
        Next
        Return _return
    End Function

    Private Function AppendText(ByVal ArrCheckedItems As ArrayList, ByVal FileNameLocal As String, ByVal filename As String, ByRef arlTransferedToSAP As ArrayList) As Integer ' Number of data sent to SAP
        Dim strText As New StringBuilder
        Dim objAl As New ArrayList
        Dim nData As Integer = 0
        Dim delimiter As String = ";"
        Dim sMessage As String = ""
        'Sample Data
        'EX00000001;10102020;MK2NCLTARLJ001154;Okta;27;Jl merdeka;08129182833;3306127112930000;Rawasari;Cempaka Putih;DKI Jakarta;Jakarta Pusat;okta@gmail.com;100001;2XPM;1;190000;10102021;4000000
        'EX00000002;10112020;MK2NCLTARLJ001159;Oktavia;27;Jl merdeka;08129180000;3306127112940043;Rawasari;Cempaka Putih;DKI Jakarta;Jakarta Pusat;oktavia@gmail.com;100001;4XPM;2;200000;10112021;6000000

        'Format:
        'RegistrationNumber;RegistrationDate;ChassisNumber;CustomerName;Age;Address;PhoneNumber;IdentityNumber;Kelurahan;Kecamatan;Provinsi;Kabupaten;Email;DealerCode;MSPExTypeCode;Duration;LastKM;ExpiredDate;Amount

        Try
            nData = 0
            If ArrCheckedItems.Count > 0 Then
                strText = New StringBuilder
                For Each oReg As MSPExRegistration In ArrCheckedItems
                    strText.Append(oReg.RegNumber)
                    strText.Append(delimiter)
                    strText.Append(Format(oReg.CreatedTime, "ddMMyyyy"))
                    strText.Append(delimiter)
                    strText.Append(oReg.ChassisMaster.ChassisNumber)
                    strText.Append(delimiter)
                    strText.Append(oReg.MSPCustomer.Name1)
                    strText.Append(delimiter)
                    strText.Append(oReg.MSPCustomer.Age)
                    strText.Append(delimiter)
                    strText.Append(oReg.MSPCustomer.Alamat)
                    strText.Append(delimiter)
                    strText.Append(oReg.MSPCustomer.PhoneNo)
                    strText.Append(delimiter)
                    strText.Append(oReg.MSPCustomer.KTPNo)
                    strText.Append(delimiter)
                    strText.Append(oReg.MSPCustomer.Kelurahan)
                    strText.Append(delimiter)
                    strText.Append(oReg.MSPCustomer.Kecamatan)
                    strText.Append(delimiter)
                    If IsNothing(oReg.MSPCustomer.Province) Then
                        strText.Append("")
                    Else
                        strText.Append(oReg.MSPCustomer.Province.ProvinceName)
                    End If
                    strText.Append(delimiter)
                    If IsNothing(oReg.MSPCustomer.City) Then
                        strText.Append("")
                    Else
                        strText.Append(oReg.MSPCustomer.City.CityName)
                    End If
                    strText.Append(delimiter)
                    strText.Append(oReg.MSPCustomer.Email)
                    strText.Append(delimiter)
                    strText.Append(oReg.Dealer.DealerCode)
                    strText.Append(delimiter)
                    strText.Append(oReg.MSPExMaster.MSPExType.Code)
                    strText.Append(delimiter)
                    strText.Append(oReg.MSPExMaster.Duration)
                    strText.Append(delimiter)
                    strText.Append(oReg.MSPExMaster.MSPExKM)
                    strText.Append(delimiter)
                    strText.Append(oReg.ValidKMTo)
                    strText.Append(delimiter)
                    strText.Append(Format(oReg.ValidDateTo, "ddMMyyyy"))
                    strText.Append(delimiter)
                    strText.Append(CDbl(oReg.MSPExMaster.Amount))
                    strText.Append(delimiter)
                    Dim oAppConfig As AppConfig = New AppConfigFacade(User).Retrieve("TermOfPaymentMSPEx")
                    If Not IsNothing(oAppConfig) Then
                        strText.Append(oAppConfig.Value)
                    Else
                        strText.Append("")
                    End If
                    strText.Append(vbNewLine)

                    arlTransferedToSAP.Add(strText)
                    nData += 1
                Next

                If nData > 0 Then
                    If Not SaveToSAP(FileNameLocal, filename, strText) Then
                        nData = -2
                    End If
                End If
            End If
        Catch ex As Exception
            nData = -1 ' -1 means error occured
        End Try

        Return nData
    End Function

    Private Function SaveToSAP(ByVal DestFileLocal As String, ByVal DestFile As String, ByRef sb As StringBuilder) As Boolean
        Dim success As Boolean = False
        Dim sw As StreamWriter
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim fInfoLocal As New FileInfo(DestFile)
        Dim finfo As New FileInfo(DestFile)
        Try
            success = imp.Start()
            If success Then
                'Local
                If Not fInfoLocal.Directory.Exists Then Directory.CreateDirectory(fInfoLocal.DirectoryName)
                If fInfoLocal.Exists() Then fInfoLocal.Delete()
                Dim fs As FileStream = New FileStream(DestFileLocal, FileMode.CreateNew)
                sw = New StreamWriter(fs)
                sw.Write(sb.ToString)
                sw.Close()
                fs.Close()

                'Server
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If
                If finfo.Exists Then
                    finfo.Delete()
                End If
                System.IO.File.Copy(DestFileLocal, DestFile)
                'System.IO.File.Copy(DestFileLocal, DestFile & ".wts")
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            success = False
            sw.Close()
        End Try
        Return success
    End Function

    Private Function StatusStoryService(ByVal MSPExRegNumber As String) As String
        Dim MSPExReg As MSPExRegistration = New MSPExRegistrationFacade(User).RetrieveByRegNumber(MSPExRegNumber)
        'Aktif = ValidDate >= hari ini dan MaxPM > ServiceCount
        'Tidak aktif = ValidDate < dari hari ini atau MaxPM <= ServiceCount
        Dim critFS As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critFS.opAnd(New Criteria(GetType(FreeService), "CreatedTime", MatchType.GreaterOrEqual, MSPExReg.CreatedTime))
        critFS.opAnd(New Criteria(GetType(FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, MSPExReg.ChassisMaster.ChassisNumber))
        Dim fsKindIDs As String = String.Empty
        Dim MaxPM As ArrayList = GetMaxPM(MSPExReg.MSPExMaster.MSPExType)
        For Each item As MSPExMappingtoFSKind In MaxPM
            If fsKindIDs.Trim.Length > 0 Then
                fsKindIDs = fsKindIDs & "," & item.FSKind.ID
            Else
                fsKindIDs = item.FSKind.ID
            End If
        Next
        If fsKindIDs.Trim.Length > 0 Then
            critFS.opAnd(New Criteria(GetType(FreeService), "FSKind.ID", MatchType.InSet, "(" & fsKindIDs & ")"))
        End If
        Dim HistoryService As ArrayList = New FreeServiceFacade(User).Retrieve(critFS)
        'If MSPExReg.ValidDateTo.Date >= Date.Now.Date AndAlso MaxPM.Count > HistoryService.Count Then
        '    Return "Aktif"
        'Else
        '    Return "Tidak Aktif"
        'End If
        If Date.Now.Date > MSPExReg.ValidDateTo.Date AndAlso MaxPM.Count > HistoryService.Count Then
            Return "Expired"
        ElseIf MaxPM.Count <= HistoryService.Count Then
            Return "Selesai"
        Else
            Return "Aktif"
        End If

    End Function

End Class