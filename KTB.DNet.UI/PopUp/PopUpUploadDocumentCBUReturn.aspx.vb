Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessValidation
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq

Public Class PopUpUploadDocumentCBUReturn
    Inherits System.Web.UI.Page

    Dim crit As CriteriaComposite
    Private sessHelper As New SessionHelper
    Private Const confExt As String = "ChassisMasterClaim.FileExt"
    Private Const confFileSize As String = "ChassisMasterClaim.MaxFileSize"
    Dim headerID As Integer = 0
    Dim claimHeader As ChassisMasterClaimHeader

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNothing(Request.QueryString("HeaderID")) Then
            headerID = CInt(Request.QueryString("HeaderID"))
        End If
        claimHeader = New ChassisMasterClaimHeaderFacade(User).Retrieve(headerID)

        If Not IsPostBack Then
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DocumentUpload), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(DocumentUpload), "Type", MatchType.Exact, "1"))
            crit.opAnd(New Criteria(GetType(DocumentUpload), "DocRegNumber", MatchType.Exact, claimHeader.ClaimNumber))
            Dim arlDoc As ArrayList = New DocumentUploadFacade(User).Retrieve(crit)
            sessHelper.SetSession("PopUpUploadDocumentCBUReturn.DocumentUpload", arlDoc)
            dgUploadFile.DataSource = arlDoc
            dgUploadFile.DataBind()
        End If

    End Sub

    Protected Sub dgUploadFile_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgUploadFile.ItemDataBound
        Dim RowValue As DocumentUpload = CType(e.Item.DataItem, DocumentUpload)
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim hdDocID As HiddenField = CType(e.Item.FindControl("hdDocID"), HiddenField)
        Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
        Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            lblNo = CType(e.Item.FindControl("lblNo"), Label)
            hdDocID = CType(e.Item.FindControl("hdDocID"), HiddenField)

            Dim lblDeskripsi As Label = CType(e.Item.FindControl("lblDeskripsi"), Label)
            Dim lnkbtnDownload As LinkButton = CType(e.Item.FindControl("lnkbtnDownload"), LinkButton)

            lblNo.Text = e.Item.ItemIndex + 1 + (dgUploadFile.PageSize * dgUploadFile.CurrentPageIndex)
            lblDeskripsi.Text = RowValue.FileDescription
            lnkbtnDownload.Text = RowValue.FileName
            hdDocID.Value = RowValue.ID

        ElseIf e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            lblNo = CType(e.Item.FindControl("lblNo"), Label)
            hdDocID = CType(e.Item.FindControl("hdDocID"), HiddenField)
            Dim txtDeskripsi As TextBox = CType(e.Item.FindControl("txtDeskripsiEdit"), TextBox)

            lblNo.Text = e.Item.ItemIndex + 1 + (dgUploadFile.PageSize * dgUploadFile.CurrentPageIndex)
            txtDeskripsi.Text = RowValue.FileDescription
            hdDocID.Value = RowValue.ID
        End If
    End Sub

    Protected Sub dgUploadFile_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgUploadFile.ItemCommand
        Dim txtDeskripsi As TextBox
        Dim fileUpload As FileUpload
        Dim result As String = String.Empty
        Dim hdDocID As HiddenField = CType(e.Item.FindControl("hdDocID"), HiddenField)
        Dim objDocs As ArrayList
        If (CType(sessHelper.GetSession("PopUpUploadDocumentCBUReturn.DocumentUpload"), ArrayList) Is Nothing) Then
            objDocs = New ArrayList
        Else
            objDocs = CType(sessHelper.GetSession("PopUpUploadDocumentCBUReturn.DocumentUpload"), ArrayList)
        End If

        Select Case e.CommandName
            Case "add"
                txtDeskripsi = CType(e.Item.FindControl("txtDeskripsiNew"), TextBox)
                fileUpload = CType(e.Item.FindControl("fuUploadNew"), FileUpload)
                Dim ext As String = Path.GetExtension(fileUpload.PostedFile.FileName)

                If txtDeskripsi.Text = "" Then
                    MessageBox.Show("Remark harus diisi.")
                    Return
                ElseIf fileUpload.PostedFile.ContentLength = 0 Then
                    MessageBox.Show("Tidak ada file yang akan di upload.")
                    Return
                ElseIf Not CBUReturnValidation.IsValidFileUploadDocument(confExt, ext, result) Then
                    MessageBox.Show(String.Format("Upload File Gagal. Gunakan format file {0}", result))
                    Return
                ElseIf Not CBUReturnValidation.IsValidFileUploadSize(confFileSize, fileUpload.PostedFile.ContentLength, result) Then
                    MessageBox.Show(String.Format("Upload File Gagal. Maksimum per file {0} MB", result))
                    Return
                End If

                Dim objDoc As New DocumentUpload

                If objDocs Is Nothing Then
                    objDoc.ID = 0
                Else
                    objDoc.ID = objDocs.Count + 1
                End If

                objDoc.FileDescription = txtDeskripsi.Text
                objDoc.FileName = fileUpload.FileName
                objDoc.Type = EnumDocumentUpload.DocumentType.Chassis_Master_Claim
                objDoc.Path = UploadTemp(fileUpload.PostedFile.FileName, fileUpload.PostedFile.InputStream)
                objDoc.AttachmentData = fileUpload.PostedFile
                objDoc.DocRegNumber = claimHeader.ClaimNumber
                objDocs.Add(objDoc)
                Dim nResult As Integer = New DocumentUploadFacade(User).Insert(objDoc)
                If nResult > 0 Then
                    MessageBox.Show(SR.UploadSucces(fileUpload.FileName))
                Else
                    MessageBox.Show(SR.UploadFail(fileUpload.FileName))
                End If

            Case "delete"
                For Each objDoc As DocumentUpload In objDocs
                    If objDoc.ID = CType(hdDocID.Value, Integer) Then
                        objDocs.Remove(objDoc)
                        objDoc.RowStatus = CType(DBRowStatus.Deleted, Short)
                        Dim nResult As Integer = New DocumentUploadFacade(User).Update(objDoc)
                        If nResult > 0 Then
                            MessageBox.Show(SR.DeleteSucces)
                        Else
                            MessageBox.Show(SR.DeleteFail)
                        End If
                        Exit For
                    End If
                Next

            Case "download"
                For Each objDoc As DocumentUpload In objDocs
                    If objDoc.ID = CType(hdDocID.Value, Integer) Then
                        If objDoc.AttachmentData Is Nothing Then
                            crit = New CriteriaComposite(New Criteria(GetType(DocumentUpload), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crit.opAnd(New Criteria(GetType(DocumentUpload), "ID", MatchType.Exact, objDoc.ID))
                            crit.opAnd(New Criteria(GetType(DocumentUpload), "Type", MatchType.Exact, 1))
                            Dim temp As ArrayList = New DocumentUploadFacade(User).Retrieve(crit)
                            If temp Is Nothing Then
                                Response.Redirect("../downloadlocal.aspx?file=" & objDoc.Path)
                            Else
                                Response.Redirect("../download.aspx?file=" & objDoc.Path)
                            End If
                        Else
                            Response.Redirect("../downloadlocal.aspx?file=" & objDoc.Path)
                        End If
                        Exit For
                    End If
                Next
        End Select

        sessHelper.SetSession("PopUpUploadDocumentCBUReturn.DocumentUpload", objDocs)
        RefreshGridAttachment()
        dgUploadFile.Focus()
    End Sub

    Private Function UploadTemp(ByVal dir As String, ByVal srcFile As Stream) As String
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate
        Try
            Dim timeStamp As String = Now.ToString("yyyyMMddHmmss")
            Dim ext As String = Path.GetExtension(dir)
            Dim dirTarget As String = timeStamp & Guid.NewGuid.ToString() & ext
            Dim targetFile As String = Server.MapPath("~/DataTemp/" & dirTarget)

            imp = New SAPImpersonate(_user, _password, _webServer)

            If imp.Start() Then
                Dim objUpload As New UploadToWebServer
                objUpload.Upload(srcFile, targetFile)
            End If

            Return String.Format("DataTemp/{0}", dirTarget)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return ""
        End Try
    End Function

    Private Sub RefreshGridAttachment(Optional objChassisMasterClaimHeader As ChassisMasterClaimHeader = Nothing, Optional indexPage As Integer = 0)
        Dim totalRow As Integer
        Dim data As ArrayList = New ArrayList

        If IsNothing(CType(sessHelper.GetSession("PopUpUploadDocumentCBUReturn.DocumentUpload"), ArrayList)) Then
            If Not objChassisMasterClaimHeader Is Nothing And ViewState("Mode").ToString <> "New" Then
                data = objChassisMasterClaimHeader.DocumentUploads
                sessHelper.SetSession("PopUpUploadDocumentCBUReturn.DocumentUpload", data)
            End If
        Else
            data = CType(sessHelper.GetSession("PopUpUploadDocumentCBUReturn.DocumentUpload"), ArrayList)
        End If

        totalRow = data.Count

        If data.Count = 0 Then
            indexPage = 0
            dgUploadFile.CurrentPageIndex = 0
        Else
            dgUploadFile.CurrentPageIndex = indexPage
        End If

        dgUploadFile.DataSource = data
        dgUploadFile.VirtualItemCount = totalRow
        dgUploadFile.DataBind()
    End Sub
End Class