#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Parser
Imports System.IO
Imports System.Text
#End Region

Public Class FrmFactoringDownload
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtCreditAccount As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents dtgMain As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlFactoringStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlProductCategory As System.Web.UI.WebControls.DropDownList

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
    Private _sortingField As String = "SortingField"
    Private _sortingMode As String = "SortingMode"
    Private _sessHelper As New SessionHelper
    Private _sessData As String = "DataToDisplay"
    Private objUser As UserInfo
#End Region

#Region "Event handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckUserPrivilege()
        If Not IsPostBack Then
            Initialization()
            btnDownload.Enabled = False
        End If
    End Sub

    Private Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
        BindDTG()
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim arlFM As New ArrayList
        arlFM = Me._sessHelper.GetSession(Me._sessData)
        DownloadData(arlFM)
    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        objUser = CType(_sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If e.Item.ItemType = ListItemType.Header Then

        ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oFM As FactoringMaster = CType(CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)(e.Item.ItemIndex), FactoringMaster)
            Dim oFMFac As FactoringMasterFacade = New FactoringMasterFacade(User)

            Dim objCMFac As CreditMasterFacade = New CreditMasterFacade(User)
            Dim objCM As CreditMaster
            objCM = objCMFac.Retrieve(oFM.ProductCategory, oFM.CreditAccount, enumPaymentType.PaymentType.TOP)

            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim lblCreditAccount As Label = e.Item.FindControl("lblCreditAccount")
            Dim lblAccountName As Label = e.Item.FindControl("lblAccountName")
            Dim lblTotalCeiling As Label = e.Item.FindControl("lblTotalCeiling")
            Dim lblStandardCeiling As Label = e.Item.FindControl("lblStandardCeiling")
            Dim lblFactoringCeiling As Label = e.Item.FindControl("lblFactoringCeiling")
            Dim lblSpaceForTop As Label = e.Item.FindControl("lblSpaceForTop")
            Dim lblTopCeiling As Label = e.Item.FindControl("lblTopCeiling")
            Dim lblAdditionalCeiling As Label = e.Item.FindControl("lblAdditionalCeiling")
            Dim lblMaxTOPDate As Label = e.Item.FindControl("lblMaxTOPDate")
            Dim lblErrorMessage As Label = e.Item.FindControl("lblErrorMessage")
            Dim lblProductCategory As Label = e.Item.FindControl("lblProductCategory")
            Dim arlFactComponent As ArrayList = oFMFac.GetCeilingComponent(oFM.ProductCategory, oFM.CreditAccount, oFM)

            lblNo.Text = e.Item.ItemIndex + 1
            lblCreditAccount.Text = oFM.CreditAccount
            lblProductCategory.Text = oFM.ProductCategory.Code
            lblAccountName.Text = ""
            Dim oD As Dealer = New DealerFacade(User).Retrieve(oFM.CreditAccount)
            If Not IsNothing(oD) AndAlso oD.ID > 0 Then
                lblAccountName.Text = oD.DealerName
            End If
            lblTotalCeiling.Text = FormatNumber(oFM.TotalCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblStandardCeiling.Text = FormatNumber(oFM.StandardCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblFactoringCeiling.Text = FormatNumber(oFM.FactoringCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblSpaceForTop.Text = FormatNumber(oFM.TotalCeiling - oFM.FactoringCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'lblTopCeiling.Text = FormatNumber(oFM.Outstanding, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblTopCeiling.Text = FormatNumber(objCM.Plafon, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Dim addCeiling As Decimal = objCM.Plafon - (oFM.TotalCeiling - oFM.FactoringCeiling)
            lblAdditionalCeiling.Text = FormatNumber(IIf(addCeiling > 0, addCeiling, 0), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblMaxTOPDate.Text = IIf(oFM.MaxTOPDate.Year < 1990, DateSerial(1900, 1, 1).ToString("dd/MMM/yyyy"), oFM.MaxTOPDate.ToString("dd/MMM/yyyy"))

            If oFM.FactoringCeiling > 0 Then
                If oFM.MaxTOPDate < Date.Now Then
                    lblErrorMessage.Text = "Tanggal validitas < tanggal hari ini"
                    e.Item.BackColor = System.Drawing.Color.LightPink
                ElseIf oFM.MaxTOPDate >= Date.Now And oFM.MaxTOPDate < Date.Now.AddDays(42) Then
                    lblErrorMessage.Text = "Tanggal validitas kurang dari 6 minggu."
                    e.Item.BackColor = System.Drawing.Color.LightPink
                End If
            End If

            If oFM.StandardCeiling < oFM.FactoringCeiling Then
                lblErrorMessage.Text = "Nilai Standard Ceiling < Factoring Ceiling"
                e.Item.BackColor = System.Drawing.Color.Yellow
            End If
            If oFM.FactoringCeiling < oFM.Outstanding Then
                lblErrorMessage.Text = "Over Ceiling"
                e.Item.BackColor = System.Drawing.Color.Yellow
            End If
        End If

    End Sub
#End Region

#Region "Custom"
    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.factoring_ceiling_standard_lihat_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Ceiling Master")
        End If
        btnDownload.Enabled = SecurityProvider.Authorize(Context.User, SR.factoring_ceiling_standard_download_privilege)
    End Sub

    Private Sub Initialization()
        viewstate.Add(Me._sortingField, "ID")
        viewstate.Add(Me._sortingMode, Sort.SortDirection.ASC)
        Me._sessHelper.SetSession(Me._sessData, New ArrayList)

        With Me.ddlFactoringStatus.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", 0))
            .Add(New ListItem("Aktif", 1))
            .Add(New ListItem("Non-Aktif", 2))
        End With

        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True, companyCode)
        ddlProductCategory.Items.RemoveAt(0)

        dtgMain.DataSource = New ArrayList
        dtgMain.DataBind()
    End Sub

    Private Sub BindDTG()
        Dim arlFM As New ArrayList
        arlFM = Me.GetDataFromDB()
        Me._sessHelper.SetSession(Me._sessData, arlFM)

        dtgMain.DataSource = arlFM
        dtgMain.DataBind()
        If arlFM.Count > 0 Then
            btnDownload.Enabled = True
        Else
            btnDownload.Enabled = False
        End If
    End Sub
    Private Function GetDataFromDB() As ArrayList
        Dim cFM As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FactoringMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sFM As SortCollection = New SortCollection
        Dim oFMFac As FactoringMasterFacade = New FactoringMasterFacade(User)
        Dim Sql As String = ""

        sFM.Add(New Sort(GetType(FactoringMaster), viewstate.Item(Me._sortingField), viewstate.Item(Me._sortingMode)))
        If Me.txtCreditAccount.Text.Trim <> "" Then
            Dim sCAs As String = Me.txtCreditAccount.Text.Trim.Replace(";", "','")
            cFM.opAnd(New Criteria(GetType(FactoringMaster), "CreditAccount", MatchType.InSet, "('" & sCAs & "')"))
        End If
        If CType(Me.ddlFactoringStatus.SelectedValue, Integer) = 1 Then 'Aktif
            Sql &= " ( select tc.Status from TransactionControl tc, Dealer d where tc.RowStatus=" & CType(DBRowStatus.Active, Short).ToString & " and tc.DealerID=d.ID and tc.Kind=" & CType(EnumDealerTransType.DealerTransKind.Factoring, Short).ToString & " and d.DealerCode=FactoringMaster.CreditAccount ) "
            cFM.opAnd(New Criteria(GetType(FactoringMaster), "RowStatus", MatchType.No, Sql))
        ElseIf CType(Me.ddlFactoringStatus.SelectedValue, Integer) = 2 Then 'Non-Aktif
            Sql &= " ( select tc.Status from TransactionControl tc, Dealer d where tc.RowStatus=" & CType(DBRowStatus.Active, Short).ToString & " and tc.DealerID=d.ID and tc.Kind=" & CType(EnumDealerTransType.DealerTransKind.Factoring, Short).ToString & " and d.DealerCode=FactoringMaster.CreditAccount ) "
            cFM.opAnd(New Criteria(GetType(FactoringMaster), "RowStatus", MatchType.Exact, Sql))
        End If
        Dim PCID As Short = Me.GetProductCategoryID()
        If PCID > 0 Then
            cFM.opAnd(New Criteria(GetType(FactoringMaster), "ProductCategory.ID", MatchType.Exact, PCID))
        End If

        Return oFMFac.Retrieve(cFM, sFM)
    End Function

    Private Function GetProductCategoryID() As Short
        Dim PCID As Short = CType(Me.ddlProductCategory.SelectedValue, Short)
        If PCID < 1 Then PCID = 0

        Return PCID
    End Function

    Private Function GetDetailName(ByVal strFullName As String) As String
        If strFullName.Length >= 6 AndAlso IsNumeric(strFullName.Substring(0, 6)) Then
            Return strFullName.Substring(5).Trim
        Else
            Return strFullName
        End If
    End Function

    Private Sub DownloadData(ByVal arlFM As ArrayList)

        Dim sFileName As String
        sFileName = "Daftar Factoring [" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & "]"

        '-- Temp file must be a randomly named file!
        Dim FMData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(FMData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(FMData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteDataFactoringMaster(sw, arlFM)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls", True)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub WriteDataFactoringMaster(ByVal sw As StreamWriter, ByVal arlFM As ArrayList)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        itemLine.Remove(0, itemLine.Length)  '-- Empty line
        itemLine.Append("FACTORING - Daftar Standard Factoring Ceiling")
        sw.WriteLine(itemLine)
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)
        sw.WriteLine(" ")

        If (arlFM.Count > 0) Then

            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("NO" & tab)
            itemLine.Append("CREDIT ACCOUNT" & tab)
            itemLine.Append("PRODUK" & tab)
            itemLine.Append("NAMA DEALER" & tab)
            itemLine.Append("TOTAL CEILING" & tab)
            itemLine.Append("STANDARD CEILING" & tab)
            itemLine.Append("FACTORING CEILING" & tab)
            itemLine.Append("OUTSTANDING" & tab)
            itemLine.Append("SPACE FOR TOP" & tab)
            itemLine.Append("TOP CEILING" & tab)
            itemLine.Append("ADDITIONAL CEILING" & tab)
            itemLine.Append("TOP OUTSTANDING" & tab)
            itemLine.Append("VALIDATY DATE FACTORING" & tab)
            itemLine.Append("VALIDATY DATE TOP CEILING" & tab)
            itemLine.Append("KETERANGAN")

            sw.WriteLine(itemLine.ToString())
            Dim i As Integer = 1
            Dim spaceForTOP As Decimal = 0
            For Each fm As FactoringMaster In arlFM
                Dim objCMFac As CreditMasterFacade = New CreditMasterFacade(User)
                Dim objCM As CreditMaster

                objCM = objCMFac.Retrieve(fm.ProductCategory, fm.CreditAccount, enumPaymentType.PaymentType.TOP)

                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(i.ToString & tab)
                'itemLine.Append(Date.Now.ToString("dd-MM-yyyy") & tab)
                itemLine.Append(fm.CreditAccount & tab)
                itemLine.Append(fm.ProductCategory.Code & tab)
                Dim oD As Dealer = New DealerFacade(User).Retrieve(fm.CreditAccount)
                If Not IsNothing(oD) AndAlso oD.ID > 0 Then
                    itemLine.Append(oD.DealerName & tab)
                End If
                itemLine.Append(Decimal.Round(fm.TotalCeiling, 2) & tab) 'Total Ceiling; 
                itemLine.Append(Decimal.Round(fm.StandardCeiling, 2) & tab) 'Standard Factoring Ceiling; 
                itemLine.Append(Decimal.Round(fm.FactoringCeiling, 2) & tab) 'Actual Factoring Ceiling; 
                itemLine.Append(Decimal.Round(fm.Outstanding, 2) & tab) 'Factoring Outstanding;
                spaceForTOP = fm.TotalCeiling - fm.FactoringCeiling
                itemLine.Append(Decimal.Round(spaceForTOP, 2) & tab) 'space for top
                itemLine.Append(Decimal.Round(objCM.Plafon, 2) & tab) 'top ceiling
                If objCM.Plafon > spaceForTOP Then
                    itemLine.Append(Decimal.Round(objCM.Plafon - spaceForTOP, 2) & tab)  'additional ceiling
                Else
                    itemLine.Append(0 & tab) 'additional ceiling
                End If
                itemLine.Append(Decimal.Round(objCM.OutStanding, 2) & tab) 'top outstanding
                itemLine.Append(fm.MaxTOPDate.ToString("dd-MM-yyyy") & tab) 'validaty date factoring
                itemLine.Append(objCM.MaxTOPDate.ToString("dd-MM-yyyy") & tab) 'validity date top ceiling

                If fm.FactoringCeiling > 0 Then
                    If fm.MaxTOPDate < Date.Now Then
                        itemLine.Append("Tanggal validitas < tanggal hari ini" & tab)
                    ElseIf fm.MaxTOPDate >= Date.Now And fm.MaxTOPDate < Date.Now.AddDays(42) Then
                        itemLine.Append("Tanggal validitas kurang dari 6 minggu.")
                    End If
                End If

                If fm.StandardCeiling < fm.FactoringCeiling Then
                    itemLine.Append("Nilai Standard Ceiling < Factoring Ceiling")
                End If
                If fm.FactoringCeiling < fm.Outstanding Then
                    itemLine.Append("Over Ceiling")
                End If

                sw.WriteLine(itemLine.ToString())
                i = i + 1
            Next
        End If

        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)

    End Sub

#End Region

End Class
