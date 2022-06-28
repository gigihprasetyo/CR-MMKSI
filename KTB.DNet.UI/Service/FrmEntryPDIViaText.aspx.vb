#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessValidation

#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Drawing.Color
Imports System.Text
#End Region

Public Class FrmEntryPDIViaText
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents dfChassis As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents dtgPDIUpload As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Private _sessHelper As SessionHelper = New SessionHelper
    Protected WithEvents lblFileName As System.Web.UI.WebControls.Label
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Private bIsError As Boolean = False
    Private isDealerPiloting As Boolean = False

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Method"

    Private Sub BindUpload()
        dtgPDIUpload.CurrentPageIndex = 0
        ViewState.Add("dtgPageIndex", 0)

        If (Not dfChassis.PostedFile Is Nothing) And (dfChassis.PostedFile.ContentLength > 0) And _
        ((dfChassis.PostedFile.ContentType.ToLower() = "text/plain") Or (dfChassis.PostedFile.ContentType.ToLower() = "text/csv") _
        Or (dfChassis.PostedFile.ContentType.ToLower() = "application/octet-stream") Or (dfChassis.PostedFile.ContentType.ToLower() = "application/vnd.ms-excel")) Then
            Dim Extension As String = Path.GetExtension(dfChassis.PostedFile.FileName)



            If Extension.ToUpper = ".CSV" Or Extension.ToUpper = ".TXT" Then


                Dim SrcFile As String = Path.GetFileName(dfChassis.PostedFile.FileName)
                Dim DestFile As String = Server.MapPath("") & "\..\DataTemp\" & Guid.NewGuid().ToString().Substring(0, 4) & SrcFile
                Dim parser As IParser = New PDIParser
                Try

                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(dfChassis.PostedFile.InputStream, DestFile)


                    Dim arList As ArrayList = CType(parser.ParseNoTransaction(DestFile, "User"), ArrayList)

                    'periksa tidak boleh ada chassis#  yang sama di tabel PDI
                    Dim NewList As ArrayList = New ArrayList
                    NewList = CheckExistingInPDI(arList)
                    NewList = Me.filterByRetur(NewList)
                    AddUserCreator(NewList)

                    'periksa dealer piloting
                    CheckPilotingPDI(arList)

                    'periksa kalo ada record rangkap (chassis# yang sama) di NewList
                    CheckDoubleRows(NewList)

                    CheckChassisCategory(NewList)

                    _sessHelper.SetSession("sessPDI", NewList)
                    dtgPDIUpload.DataSource = NewList
                    dtgPDIUpload.DataBind()

                Catch Exc As Exception
                    MessageBox.Show("Error: " & Exc.Message)
                Finally
                    parser = Nothing

                End Try

            Else
                MessageBox.Show("Jenis file tidak sesuai")
            End If
        Else
            MessageBox.Show("Pilih file yang akan di-upload.")
        End If
    End Sub

    Private Function CheckChassisCategory(ByVal arList As ArrayList) As ArrayList
        Dim TmpList As ArrayList = New ArrayList
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        For Each objPDI As PDI In arList
            If IsNothing(objPDI.ErrorMessage) OrElse objPDI.ErrorMessage.ToString() = "" Then

                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, objPDI.ChassisMaster.ChassisNumber))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "EngineNumber", MatchType.Exact, objPDI.ChassisMaster.EngineNumber))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "Category.ProductCategory.Code", MatchType.Exact, companyCode))
                Dim ChassisMasterCollection As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)
                If ChassisMasterCollection.Count = 0 Then
                    objPDI.ErrorMessage = "Chassis tidak terdaftar di " + companyCode + " atau No Mesin tidak sesuai"
                    TmpList.Add(objPDI)
                Else
                    TmpList.Add(objPDI)
                End If
            End If
        Next
        Return TmpList
    End Function

    Private Function filterByRetur(ByVal aPDIs As ArrayList)
        Dim aValids As New ArrayList
        Dim sErr As String

        For Each oPDI As PDI In aPDIs
            sErr = "Diretur"
            If Not IsNothing(oPDI) _
                AndAlso Not IsNothing(oPDI.ChassisMaster) _
                AndAlso oPDI.ChassisMaster.isValidToCreateFaktur() Then
                sErr = String.Empty
            End If
            If IsNothing(oPDI.ErrorMessage) Then oPDI.ErrorMessage = String.Empty
            If sErr <> String.Empty Then oPDI.ErrorMessage &= IIf(oPDI.ErrorMessage.Trim = "", "", ";<br>") & sErr
            aValids.Add(oPDI)
        Next
        Return aValids
    End Function

    Private Sub CheckDoubleRows(ByVal arrL As ArrayList)
        Dim nIndex As Integer
        Dim nIterate As Integer = 1
        For Each objPDI As PDI In arrL
            If Not IsNothing(objPDI.ChassisMaster) Then
                For nIndex = nIterate To arrL.Count - 1
                    Dim objPDI2 As PDI
                    objPDI2 = arrL(nIndex)
                    If Not IsNothing(objPDI2.ChassisMaster) Then
                        Dim sChassisNumber2 = objPDI2.ChassisMaster.ChassisNumber
                        Dim sChassisNumber1 = objPDI.ChassisMaster.ChassisNumber

                        If sChassisNumber1 = sChassisNumber2 Then
                            If objPDI2.ErrorMessage = "" Then
                                objPDI2.ErrorMessage = "Data PDI Ganda dg Record " + CType(nIterate, String)
                            Else
                                objPDI2.ErrorMessage = objPDI2.ErrorMessage + ";<br> Data PDI Ganda dg Record " + CType(nIterate, String)
                            End If
                        End If
                    End If
                Next
            End If
            nIterate = nIterate + 1
        Next
    End Sub

    Private Sub AddUserCreator(ByRef arrL As ArrayList)
        For Each objPDI As PDI In arrL
            objPDI.CreatedBy = User.Identity.Name
        Next
    End Sub

    Private Function CheckExistingInPDI(ByVal arList As ArrayList) As ArrayList
        Dim TmpList As ArrayList = New ArrayList
        For Each objPDI As PDI In arList
            If Not IsNothing(objPDI.ChassisMaster) Then
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, objPDI.ChassisMaster.ChassisNumber))
                Dim ChassisMasterCollection As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)
                If ChassisMasterCollection.Count > 0 Then
                    Dim objChassisMaster As ChassisMaster = New ChassisMaster
                    objChassisMaster = ChassisMasterCollection(0)
                    Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "ChassisMaster.ID", MatchType.Exact, objChassisMaster.ID))
                    Dim PDICollection As ArrayList = New PDIFacade(User).Retrieve(criterias2)
                    If PDICollection.Count > 0 Then
                        If objPDI.ErrorMessage = "" Then
                            objPDI.ErrorMessage = "No. Rangka Telah PDI"
                        Else
                            objPDI.ErrorMessage = objPDI.ErrorMessage + ";<br> No. Rangka Telah PDI"
                        End If
                        TmpList.Add(objPDI)
                    Else
                        TmpList.Add(objPDI)
                    End If
                Else
                    TmpList.Add(objPDI)
                End If
            Else
                TmpList.Add(objPDI)
            End If
        Next
        Return TmpList
    End Function

    Private Function CheckPilotingPDI(ByVal arList As ArrayList) As ArrayList
        Dim TmpList As ArrayList = New ArrayList
        For Each objPDI As PDI In arList
            If Not IsNothing(objPDI.Dealer) Then
                isDealerPiloting = TCHelper.GetActiveTCResult(objPDI.Dealer.ID, CInt(EnumDealerTransType.DealerTransKind.PilotingPDI))
                If (isDealerPiloting = True And objPDI.WorkOrderNumber.ToString() = "") Then
                    'bPiloting = False
                    'Exit For
                    objPDI.ErrorMessage = "Record ini adalah Dealer Piloting, Work Order Number harus diisi !"
                    TmpList.Add(objPDI)
                End If
                'If Not IsNothing(objPDI.ChassisMaster) Then
                '    Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, objPDI.ChassisMaster.ChassisNumber))
                '    Dim ChassisMasterCollection As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)
                '    If ChassisMasterCollection.Count > 0 Then
                '        Dim objChassisMaster As ChassisMaster = New ChassisMaster
                '        objChassisMaster = ChassisMasterCollection(0)
                '        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                '        criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "ChassisMaster.ID", MatchType.Exact, objChassisMaster.ID))
                '        Dim PDICollection As ArrayList = New PDIFacade(User).Retrieve(criterias2)
                '        If PDICollection.Count > 0 Then
                '            If objPDI.ErrorMessage = "" Then
                '                objPDI.ErrorMessage = "No. Rangka Telah PDI"
                '            Else
                '                objPDI.ErrorMessage = objPDI.ErrorMessage + ";<br> No. Rangka Telah PDI"
                '            End If
                '            TmpList.Add(objPDI)
                '        Else
                '            TmpList.Add(objPDI)
                '        End If
                '    Else
                '        TmpList.Add(objPDI)
                '    End If
                'Else
                '    TmpList.Add(objPDI)
                'End If

            End If
        Next
        Return TmpList
    End Function

    Private Sub BinddtgPDIUpload(ByVal pageIndeks As Integer)
        Dim totalRow As Integer = 0
        dtgPDIUpload.DataSource = CType(_sessHelper.GetSession("sessPDI"), ArrayList)
        dtgPDIUpload.VirtualItemCount = totalRow
        dtgPDIUpload.DataBind()
    End Sub

    Private Function IsExistCode(ByVal ChassisID As Integer) As Boolean
        Dim objPDIFacade As PDIFacade = New PDIFacade(User)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "ChassisMaster.ID", MatchType.Exact, ChassisID))
        Dim TestExist As ArrayList = New PDIFacade(User).Retrieve(criterias)
        If TestExist.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function InsertPDI(ByVal objPDI As PDI) As Integer
        Dim objPDIFacade As PDIFacade = New PDIFacade(User)
        objPDI.ChassisMaster = New ChassisMasterFacade(User).Retrieve(objPDI.ChassisMaster.ID)
        objPDI.Dealer = New DealerFacade(User).Retrieve(objPDI.Dealer.ID)
        Return objPDIFacade.Insert(objPDI)
    End Function

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            If Not IsNothing(Session("DEALER")) Then
                Dim ObjDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
                ''CR Tutup Menu
                '' by ali
                '' 2014 - 09 -30

                If (DateTime.Now >= New DateTime(2014, 10, 1) AndAlso DateTime.Now <= New DateTime(2014, 10, 11).AddMinutes(-1) AndAlso ObjDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                    Dim MSgClose As String = IIf(Not IsNothing(KTB.DNet.Lib.WebConfig.GetValue("CloseMessage")), KTB.DNet.Lib.WebConfig.GetValue("CloseMessage"), "Module ini sedang di tutup, sampai dengan 10 Oktober 2014")
                    Server.Transfer("../ClossingMessage.htm")
                End If
                ''END CR Tutup Menu
                lblDealerCode.Text = ObjDealer.DealerCode + " / " + ObjDealer.SearchTerm1
                lblDealerName.Text = ObjDealer.DealerName
                _sessHelper.SetSession("sessDealer", ObjDealer)
                _sessHelper.SetSession("sessDealerLogin", ObjDealer.DealerCode)
            Else
                'Response.Redirect("../SessionExpired.htm")
            End If

        End If
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.PDIUploadView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PDI - Upload Data PDI")
        End If

        'PDIUploadSave_Privilege 
        btnSave.Visible = SecurityProvider.Authorize(Context.User, SR.PDIUploadSave_Privilege)
    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        bIsError = False
        BindUpload()
        bIsError = CType(_sessHelper.GetSession("sessError"), Boolean)

        If Not bIsError And Path.GetFileName(dfChassis.PostedFile.FileName).ToString.Trim <> String.Empty And dtgPDIUpload.Items.Count > 0 Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If

        'ini nanti dikomen
        'btnSave.Enabled = True

        
    End Sub

    Private Sub dtgPDIUpload_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPDIUpload.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As PDI = CType(e.Item.DataItem, PDI)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then

                Dim lblDealerBranch As Label = CType(e.Item.FindControl("lblDealerBranch"), Label)
                If Not IsNothing(RowValue.DealerBranch) Then
                    lblDealerBranch.Text = RowValue.DealerBranch.DealerBranchCode
                Else
                    lblDealerBranch.Text = RowValue.DealerBranchCodeMsg
                End If

                Dim lblChassisNo As Label = CType(e.Item.FindControl("lblChassisNo"), Label)
                If Not IsNothing(RowValue.ChassisMaster) Then
                    lblChassisNo.Text = RowValue.ChassisMaster.ChassisNumber
                Else
                    lblChassisNo.Text = RowValue.ChassisNumberMsg
                End If

                Dim lblStatusPDI As Label = CType(e.Item.FindControl("lblStatusPDI"), Label)
                If RowValue.PDIKindMsg <> "" And lblStatusPDI.Text = "" Then
                    lblStatusPDI.Text = RowValue.PDIKindMsg
                End If

                Dim lblTglPDI As Label = CType(e.Item.FindControl("lblTglPDI"), Label)
                If RowValue.PDIDateMsg <> "" Then
                    lblTglPDI.Text = RowValue.PDIDateMsg
                End If


                If RowValue.PDIDate < New Date(1900, 1, 1) Then
                    lblTglPDI.Text = ""
                End If

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1 + (dtgPDIUpload.CurrentPageIndex * dtgPDIUpload.PageSize)


                Dim lblMessage As Label = CType(e.Item.FindControl("lblMessage"), Label)
                If Not RowValue.ErrorMessage <> "" Then
                    lblMessage.Text = "OK"
                    lblMessage.BackColor = GreenYellow
                Else
                    lblMessage.BackColor = LightSalmon
                    bIsError = True
                End If
            End If
            _sessHelper.SetSession("sessError", bIsError)
        End If
    End Sub

    Private Sub dtgPDIUpload_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPDIUpload.PageIndexChanged
        ViewState.Remove("dtgPageIndex")
        dtgPDIUpload.CurrentPageIndex = e.NewPageIndex
        ViewState.Add("dtgPageIndex", e.NewPageIndex)
        BinddtgPDIUpload(e.NewPageIndex + 1)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim bCheckSuccess As Boolean = True
        Dim bPiloting As Boolean = True
        Dim arList As ArrayList = CType(_sessHelper.GetSession("sessPDI"), ArrayList)
        For Each objPDI As PDI In arList
            
            If Not IsExistCode(objPDI.ChassisMaster.ID) Then
                Dim nResult As Integer = InsertPDI(objPDI)
                If nResult = -1 Then
                    bCheckSuccess = False
                End If
            End If
        Next
       

        If bCheckSuccess Then
            MessageBox.Show("Semua record berhasil disimpan !")
        Else
            MessageBox.Show("Ada record yang gagal disimpan !")
        End If
        btnSave.Enabled = False
    End Sub

#End Region

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Dim sb As StringBuilder = New StringBuilder
        Dim sample1 As String = "Kode Dealer, Nomor Rangka, Nomor Mesin, Tanggal PDI, Kode Cabang Dealer, Nomor WO"
        Dim sample2 As String = "100XX,MK2NXWHARHXXXX,4AX1CXXX58,A,01012018,300XXX,WO-00001"
        Dim sample3 As String = "100XX,MK2NCWXANHJXXXXX,4AX1CXXX9,A,01052018,,"
        sb.Append(sample1 & vbCrLf & sample2 & vbCrLf & sample3)
        sb.Append("" & vbCrLf)
        Dim text As String = sb.ToString
        Response.Clear()
        Response.ClearHeaders()
        Response.AddHeader("Content-Length", text.Length.ToString)
        Response.ContentType = "text/plain"
        Response.AppendHeader("content-disposition", "attachment;filename=""PDISample.txt""")
        Response.Write(text)
        Response.End()
    End Sub
End Class