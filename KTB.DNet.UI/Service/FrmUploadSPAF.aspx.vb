Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports System.IO

Public Class FrmUploadSPAF
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ltrTitle As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrCompanyName As System.Web.UI.WebControls.Label
    Protected WithEvents fuUpload As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents dgUploadData As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variables"

    Private ObjDealer As Dealer
    Private isSPAF As Boolean
    Private bIsError As Boolean = False

#End Region

#Region "Custome Methods"

    Private Sub CheckDuplicateChassisNumber(ByRef NewList As ArrayList)
        Dim nIndex As Integer
        Dim nIterate As Integer = 1
        For Each item As SPAFDoc In NewList
            If Not IsNothing(item.ChassisMaster) Then
                For nIndex = nIterate To NewList.Count - 1
                    Dim item2 As SPAFDoc
                    item2 = NewList(nIndex)
                    If Not IsNothing(item2.ChassisMaster) Then
                        Dim sChassisNumber2 = item2.ChassisMaster.ChassisNumber
                        Dim sChassisNumber1 = item.ChassisMaster.ChassisNumber

                        If sChassisNumber1 = sChassisNumber2 Then
                            If item2.ErrorMessage = "" Then
                                item2.ErrorMessage = "Data No Rangka Ganda dg Record " + CType(nIterate, String)
                            Else
                                item2.ErrorMessage = item2.ErrorMessage + ";<br> Data No Rangka Ganda dg Record " + CType(nIterate, String)
                            End If
                        End If
                    End If
                Next
            End If
            nIterate = nIterate + 1
        Next
    End Sub

    Private Sub AddUserCreatorAndStatus(ByRef arrL As ArrayList)
        For Each item As SPAFDoc In arrL
            item.CreatedBy = CType(User.Identity.Name, String)
            item.Status = EnumSPAFSubsidy.SPAFDocStatus.Baru
        Next
    End Sub

    Private Sub AddDocType(ByRef arrL As ArrayList)
        For Each item As SPAFDoc In arrL
            item.DocType = IIf(isSPAF, EnumSPAFSubsidy.DocumentType.SPAF, EnumSPAFSubsidy.DocumentType.Subsidy)
        Next
    End Sub

    Private Function GetValueConditionMaster(ByRef arrL As ArrayList) As ConditionMaster
        Dim dt1 As New Date
        Dim dt2 As New Date
        Dim dttmp As New Date
        Dim i As Integer = 0
        Dim CM As New ConditionMaster
        For Each item As SPAFDoc In arrL
            If (Not item.ChassisMaster Is Nothing) Then
                Dim arrlCM As New ArrayList
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ConditionMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ConditionMaster), "VechileType.ID", MatchType.Exact, item.ChassisMaster.VechileColor.VechileType.ID))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ConditionMaster), "ValidFrom", MatchType.LesserOrEqual, item.DateLetter))

                arrlCM = New SPAF.ConditionMasterFacade(User).Retrieve(criterias)

                If (arrlCM.Count = 1) Then
                    item.RetailPrice = CType(arrlCM(0), ConditionMaster).RetailPrice
                    item.SPAF = IIf(isSPAF, (CType(arrlCM(0), ConditionMaster).SPAF / 100) * CType(arrlCM(0), ConditionMaster).RetailPrice, 0)
                    item.Subsidi = IIf(isSPAF, 0, CType(arrlCM(0), ConditionMaster).Subsidi)
                Else
                    For Each item2 As ConditionMaster In arrlCM
                        dt1 = item2.ValidFrom
                        If (i > 0) Then
                            dt1 = dttmp
                        End If
                        dt2 = item2.ValidFrom

                        If (item.DateLetter = item2.ValidFrom) Then
                            item.RetailPrice = item2.RetailPrice
                            item.SPAF = IIf(isSPAF, (item2.SPAF / 100) * item2.RetailPrice, 0)
                            item.Subsidi = IIf(isSPAF, 0, item2.Subsidi)
                            Exit For
                        Else
                            If (item.DateLetter >= dt1 And item.DateLetter <= dt2.AddDays(1)) Then
                                item.RetailPrice = item2.RetailPrice
                                item.SPAF = IIf(isSPAF, (item2.SPAF / 100) * item2.RetailPrice, 0)
                                item.Subsidi = IIf(isSPAF, 0, item2.Subsidi)
                            End If
                        End If
                        dttmp = item2.ValidFrom
                        CM = item2
                        i = i + 1
                    Next
                End If
            End If
        Next
    End Function

    Private Sub BindUpload()
        If (Not fuUpload.PostedFile Is Nothing) And (fuUpload.PostedFile.ContentLength > 0) And _
        ((fuUpload.PostedFile.ContentType.ToLower() = "text/plain") Or (fuUpload.PostedFile.ContentType.ToLower() = "text/csv") _
        Or (fuUpload.PostedFile.ContentType.ToLower() = "application/octet-stream") Or (fuUpload.PostedFile.ContentType.ToLower() = "application/vnd.ms-excel")) Then
            Dim Extension As String = Path.GetExtension(fuUpload.PostedFile.FileName)

            If Extension.ToUpper = ".TXT" Then
                Dim SrcFile As String = Path.GetFileName(fuUpload.PostedFile.FileName)
                Dim DestFile As String = Server.MapPath("") & "\..\DataTemp\" & SrcFile
                Try
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(fuUpload.PostedFile.InputStream, DestFile)

                    Dim parser As IParser = New SPAFParser
                    Dim arList As ArrayList = CType(parser.ParseNoTransaction(DestFile, "User"), ArrayList)
                    AddDocType(arList)
                    AddUserCreatorAndStatus(arList)
                    GetValueConditionMaster(arList)
                    CheckDuplicateChassisNumber(arList)
                    Session("DataUpload") = arList
                    dgUploadData.DataSource = arList
                    dgUploadData.DataBind()
                    File.Delete(DestFile)
                Catch Exc As Exception
                    MessageBox.Show(SR.UploadFail(SrcFile))
                End Try
            Else
                MessageBox.Show("Jenis file tidak sesuai")
            End If
        Else
            MessageBox.Show("Pilih file yang akan di-upload.")
        End If
    End Sub

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ObjDealer = CType(Session.Item("DEALER"), Dealer)
        isSPAF = IIf(Convert.ToBoolean(Request.QueryString("isSPAF")) = True, True, False)
        If Not IsPostBack Then
            btnSimpan.Enabled = False
            ltrCompanyName.Text = ObjDealer.DealerCode + " / " + ObjDealer.DealerName
            ltrTitle.Text = IIf(isSPAF, "SPAF", "Subsidi")
            dgUploadData.Columns(7).Visible = IIf(isSPAF, True, False)
            dgUploadData.Columns(8).Visible = IIf(isSPAF, False, True)
        End If
    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        bIsError = False
        BindUpload()
        bIsError = CType(Session("isError"), Boolean)
        If Not bIsError And Path.GetFileName(fuUpload.PostedFile.FileName).ToString.Trim <> String.Empty Then
            btnSimpan.Enabled = True
        Else
            btnSimpan.Enabled = False
        End If
    End Sub

    Private Sub dgUploadData_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgUploadData.ItemDataBound
        If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            e.Item.Cells(0).Controls.Add(lNum)
            Dim oSPAF As SPAFDoc = CType(e.Item.DataItem, SPAFDoc)

            Dim lblMessage As Label = CType(e.Item.FindControl("lblMessage"), Label)
            Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
            Dim oDealer As New Dealer
            oDealer = New General.DealerFacade(User).GetDealer(oSPAF.OrderDealer)
            lblDealerName.Text = oDealer.DealerName

            If Not oSPAF.ErrorMessage <> "" Then
                lblMessage.Text = "OK"
            Else
                lblMessage.Text = oSPAF.ErrorMessage
                bIsError = True
            End If
        End If
        Session("isError") = bIsError
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If (New SPAF.SPAFFacade(User).InsertTransactionSPAFAndSubsidiFromTextFile(CType(Session("DataUpload"), ArrayList)) = 1) Then
            MessageBox.Show(SR.UploadSucces(IIf(isSPAF, "SPAF", "Subsidi")))
        Else
            MessageBox.Show(SR.UploadFail(IIf(isSPAF, "SPAF", "Subsidi")))
        End If
    End Sub

#End Region

End Class
