#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
Imports System.Collections.Generic
Imports System.Linq
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.WebCC
Imports GlobalExtensions
Imports PdfSharp
Imports PdfSharp.Pdf
Imports PdfSharp.Pdf.IO
Imports PdfSharp.Drawing



#End Region

Public Class FrmDownloadESRUTDealer
    Inherits System.Web.UI.Page

    Dim gridColNo As Integer = 0
    Private Const EmptyDate As String = "01/01/0001 0:00:00"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        InitiateAuthorization()
        If Not Page.IsPostBack Then
            InitForm()
        End If

    End Sub

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.ESRUT_View_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=ESRUT - Download ESRUT")
        End If
    End Sub

    Private Sub InitForm()

        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

        Dim objDealer As Dealer = CType(Session("Dealer"), Dealer)

        txtKodeDealer.Text = objDealer.DealerCode
        ' txtNomorPengajuan.Clear()
        txtChassisNumber.Clear()
        txtEngineNumber.Clear()
        txtTipe.Clear()
        ICDOFrom.Text = ""
        ICDOTo.Text = ""
        BindDdlCategory()
        BindDdlSubCategory()
        BindDdlStatus()
        BindDataGrid(0)
    End Sub

    Private Sub BindDdlCategory()
        ddlCategory.Items.Clear()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.ID", MatchType.Exact, "1"))

        Dim arlResult As ArrayList = New CategoryFacade(User).Retrieve(criterias)

        For Each cat As Category In arlResult
            ddlCategory.Items.Add(New ListItem(cat.CategoryCode, cat.ID))
        Next
        ddlCategory.Items.Insert(0, New ListItem("Silakan Pilih", "-1"))
    End Sub

    Private Sub BindDdlSubCategory(Optional ByVal categoryId As String = "")
        ddlSubCategory.Items.Clear()
        If categoryId <> "" Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.ID", MatchType.Exact, categoryId))

            Dim arlResult As ArrayList = New SubCategoryVehicleFacade(User).Retrieve(criterias)

            For Each subCat As SubCategoryVehicle In arlResult
                ddlSubCategory.Items.Add(New ListItem(subCat.Name, subCat.ID))
            Next

        End If

        ddlSubCategory.Items.Insert(0, New ListItem("Silakan Pilih", "-1"))


    End Sub


    Private Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        BindDdlSubCategory(ddlCategory.SelectedValue.ToString())
    End Sub

    Private Sub BindDdlStatus()
        ddlStatus.Items.Clear()
        ddlStatus.Items.Add(New ListItem(EnumESRUT.GetRemarkByEnumDealer(EnumESRUT.ESRUTItemStatusDealer.READY_TO_PRINT), EnumESRUT.ESRUTItemStatusDealer.READY_TO_PRINT))
        ddlStatus.Items.Add(New ListItem(EnumESRUT.GetRemarkByEnumDealer(EnumESRUT.ESRUTItemStatusDealer.NOT_AVAILABLE), EnumESRUT.ESRUTItemStatusDealer.NOT_AVAILABLE))
      
        ddlStatus.Items.Insert(0, New ListItem("Silakan Pilih", "-1"))

    End Sub

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Response.Redirect("FrmDownloadESRUTDealer.aspx")

    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        gridColNo = dgList.PageSize * indexPage
        dgList.DataSource = New ChassisMasterFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, dgList.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgList.VirtualItemCount = totalRow
        dgList.CurrentPageIndex = indexPage
        dgList.DataBind()
    End Sub

    Private Function CriteriaSearch() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))



        criterias.opAnd(New Criteria(GetType(ChassisMaster), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text))


        'If txtNomorPengajuan.Text <> String.Empty Then
        '    criterias.opAnd(New Criteria(GetType(ChassisMaster), "ESRUTHeader.NoPengajuan", MatchType.InSet, "('" & txtNomorPengajuan.Text.Replace(";", "','") & "')"))
        'End If

        If txtChassisNumber.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.InSet, "('" & txtChassisNumber.Text.Replace(";", "','") & "')"))
        End If

        If txtEngineNumber.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EngineNumber", MatchType.InSet, "('" & txtEngineNumber.Text.Replace(";", "','") & "')"))
        End If

        If txtTipe.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "VechileColor.VechileType.VechileTypeCode", MatchType.InSet, "('" & txtTipe.Text.Replace(";", "','") & "')"))
        End If

        If ddlCategory.SelectedValue.ToString() <> "-1" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "Category.ID", MatchType.Exact, ddlCategory.SelectedValue))
        End If

        If ddlSubCategory.SelectedValue.ToString() <> "-1" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "VechileColor.VechileType.VechileModel.SubCategoryVehicleToModel.SubCategoryVehicle.ID", MatchType.Exact, ddlSubCategory.SelectedValue))
        End If

        If ICDOFrom.Value.ToString() <> EmptyDate Or ICDOTo.Value.ToString <> EmptyDate Then
            ValidateDODate()
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "DODate", MatchType.GreaterOrEqual, ICDOFrom.Value))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "DODate", MatchType.Lesser, ICDOTo.Value.AddDays(1)))
        End If

        If ddlStatus.SelectedValue.ToString() <> "-1" Then
            If ddlStatus.SelectedValue = EnumESRUT.ESRUTItemStatusDealer.READY_TO_PRINT Then
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "ID", MatchType.InSet, "(SELECT ChassisMasterID FROM ESRUTItem WHERE Status=1 AND RowStatus= 0)"))
            Else
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "ID", MatchType.NotInSet, "(SELECT ChassisMasterID FROM ESRUTItem WHERE Status=1 AND RowStatus= 0)"))
            End If

        End If


        Return criterias
    End Function

    Private Sub dgList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgList.ItemCommand
        Try
            If (e.CommandName = "Download") Then
                Dim id As Integer = CInt(e.Item.Cells(0).Text)
                Dim item As ESRUTItem = GetESRUTItemByChassisID(id)
                DownloadSingleESRUT(item)
                'DownloadSingleESRUTWithZetPDF(item)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub dgList_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgList.ItemDataBound
        Try
            If Not e.Item.DataItem Is Nothing Then
                Dim data As ChassisMaster = CType(e.Item.DataItem, ChassisMaster)
                gridColNo += 1

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                Dim chkDownload As CheckBox = CType(e.Item.FindControl("chkDownload"), CheckBox)
                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
                Dim lblChassisNumber As Label = CType(e.Item.FindControl("lblChassisNumber"), Label)
                Dim lblEngineNumber As Label = CType(e.Item.FindControl("lblEngineNumber"), Label)
                Dim lblNomorSRUT As Label = CType(e.Item.FindControl("lblNomorSRUT"), Label)
                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                Dim lblDODate As Label = CType(e.Item.FindControl("lblDODate"), Label)
                Dim lnkDownload As LinkButton = CType(e.Item.FindControl("lnkDownload"), LinkButton)
                Dim lblPopUpLog As Label = CType(e.Item.FindControl("lblPopUpLog"), Label)

                lblNo.Text = gridColNo


                lblDODate.Text = data.DODate
                If Not IsNothing(data.Dealer) Then
                    lblDealerCode.Text = data.Dealer.DealerCode
                    lblDealerName.Text = data.Dealer.DealerName
                End If


                lblChassisNumber.Text = data.ChassisNumber
                lblEngineNumber.Text = data.EngineNumber


                Dim ESRUTItem As ESRUTItem = GetESRUTItemByChassisID(data.ID)

                If ESRUTItem.ID <> 0 Then
                    lblStatus.Text = EnumESRUT.GetRemarkByEnumDealer(EnumESRUT.ESRUTItemStatusDealer.READY_TO_PRINT)
                    lblNomorSRUT.Text = ESRUTItem.NomorSRUT
                    lblPopUpLog.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpESRUTDownloadLog.aspx?ID=" & ESRUTItem.ID & "&Source=dealer', '', 500, 760, '');")
                Else
                    chkDownload.Visible = False
                    lnkDownload.Visible = False
                    lblPopUpLog.Visible = False
                    lblStatus.Text = EnumESRUT.GetRemarkByEnumDealer(EnumESRUT.ESRUTItemStatusDealer.NOT_AVAILABLE)
                End If

                'lblStatus.Text = EnumESRUT.GetRemarkByEnumDealer(data.Status)




            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function GetESRUTItemByChassisID(ByVal chassisMasterID As Integer) As ESRUTItem
        Dim result As New ESRUTItem

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ESRUTItem), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ESRUTItem), "Status", MatchType.Exact, CInt(EnumESRUT.ESRUTItemStatus.VALID)))
        criterias.opAnd(New Criteria(GetType(ESRUTItem), "ChassisMaster.ID", MatchType.Exact, chassisMasterID))

        Dim arl As ArrayList = New ESRUTItemFacade(User).Retrieve(criterias)
        If arl.Count > 0 Then
            result = CType(arl(0), ESRUTItem)
        End If

        Return result
    End Function

    Private Function GetPagePerESRUT(ByVal mainFilePath As String, ByVal pageNumber As Integer) As PdfPage
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

   

    Private Sub DownloadSingleESRUT(ByVal data As ESRUTItem)
        Dim pdfDoc As New PdfDocument()
        pdfDoc.Options.FlateEncodeMode = PdfFlateEncodeMode.BestCompression
        pdfDoc.Options.UseFlateDecoderForJpegImages = PdfUseFlateDecoderForJpegImages.Automatic
        pdfDoc.Options.NoCompression = False
        pdfDoc.Options.CompressContentStreams = True
        Dim page As PdfPage = GetPagePerESRUT(data.ESRUTHeader.PdfFilePath, data.PageNumber)
        pdfDoc.AddPage(page)
        SaveDownloadLog(data)
        Dim fileName As String = Guid.NewGuid().ToString().Substring(0, 5) & ".pdf"
        SaveFileToTempAndDownload(pdfDoc, fileName)
        'JustDownloadFile(pdfDoc, data.NomorSRUT)

    End Sub

    Private Sub JustDownloadFile(ByVal pdfDoc As PdfDocument, ByVal fileName As String)
        Dim stream As MemoryStream = New MemoryStream()
        pdfDoc.Save(stream, False)
        Dim bytes As Byte() = stream.ToArray()
        'Response.AppendHeader("Content-Length", stream.Length.ToString())
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName & ".pdf")
        Response.ContentType = "application/pdf" 'xls
        Response.BinaryWrite(bytes)
        Response.Flush()
        Response.[End]()
    End Sub

    Private Sub DownloadMultipeESRUT(ByVal listData As List(Of ESRUTItem))
        Dim pdfDoc As New PdfDocument()
        pdfDoc.Options.FlateEncodeMode = PdfFlateEncodeMode.BestCompression
        pdfDoc.Options.UseFlateDecoderForJpegImages = PdfUseFlateDecoderForJpegImages.Automatic
        pdfDoc.Options.NoCompression = False
        pdfDoc.Options.CompressContentStreams = True

        For Each item As ESRUTItem In listData
            Dim page As PdfPage = GetPagePerESRUT(item.ESRUTHeader.PdfFilePath, item.PageNumber)
            pdfDoc.AddPage(page)
            SaveDownloadLog(item)
        Next
        Dim fileName As String = Guid.NewGuid().ToString().Substring(0, 5) & ".pdf"
        SaveFileToTempAndDownload(pdfDoc, fileName)
        ' JustDownloadFile(pdfDoc, "ListESRUT_MKS_" & DateTime.Now.ToString())
    End Sub


    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click

        Try
            btnDownload.Enabled = False
            Dim listESRUT As List(Of ESRUTItem) = GetListToDownload()

            If listESRUT.Count = 0 Then
                Throw New Exception("Harap pilih ESRUT yang akan di download")
            Else
                DownloadMultipeESRUT(listESRUT)
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            btnDownload.Enabled = True
        End Try

    End Sub

    Protected Function GetListToDownload() As List(Of ESRUTItem)
        Dim result As New List(Of ESRUTItem)

        For Each item As DataGridItem In dgList.Items
            Dim chkDownload As CheckBox = CType(item.FindControl("chkDownload"), CheckBox)
            If chkDownload.Checked And chkDownload.Visible = True Then
                Dim esrutItem As ESRUTItem = GetESRUTItemByChassisID(CInt(item.Cells(0).Text))
                result.Add(esrutItem)
            End If
        Next

        Return result
    End Function

    Private Sub SaveDownloadLog(item As ESRUTItem)
        Dim log As New ESRUTDownloadLog
        log.ESRUTItem = item

        Dim result As Integer = New ESRUTDownloadLogFacade(User).Insert(log)
    End Sub

    Protected Sub chkDownloadAll_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim chkAll As CheckBox = CType(sender, CheckBox)

            For Each item As DataGridItem In dgList.Items
                Dim chkDownload As CheckBox = CType(item.FindControl("chkDownload"), CheckBox)
                chkDownload.Checked = chkAll.Checked
            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgList_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgList.PageIndexChanged
        Try
            ' dgList.CurrentPageIndex = e.NewPageIndex
            BindDataGrid(e.NewPageIndex)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgList_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgList.SortCommand
        Try
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

            dgList.CurrentPageIndex = 0
            BindDataGrid(dgList.CurrentPageIndex)
        Catch ex As Exception

        End Try
    End Sub



    Private Sub ValidateDODate()
        Try

            If ICDOFrom.Value.ToString() = EmptyDate Or ICDOTo.Value.ToString = EmptyDate Then
                Throw New Exception("Tanggal DO dari / sampai harus diisi")
            End If

            If Not ICDOFrom.Value <= ICDOTo.Value Then
                Throw New Exception("")
            End If
        Catch ex As Exception
            Throw New Exception("Tanggal DO dari harus lebih kecil dari tanggal DO sampai")
        End Try
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Try
            BindDataGrid(0)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

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

  
   

End Class

