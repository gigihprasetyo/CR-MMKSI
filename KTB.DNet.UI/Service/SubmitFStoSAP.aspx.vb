#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.UI
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.Text
Imports System.IO
Imports System.Collections.Generic
Imports System.Linq
Imports KTB.DNet.BusinessFacade

#End Region

Public Class SubmitFStoSAP
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents dtgFStoSAP As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents ICSampai As KTB.DNet.WebCC.IntiCalendar

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Private bPrivilegeSubmitFStoSAP As Boolean = False

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Dim total, dealerName As String
    Dim totRow As Integer
    Dim sHDownFS As SessionHelper = New SessionHelper

    'agus puts code
    '-- Generate timestamp untuk nama file FSData[timestamp].txt
    Dim dt As DateTime = DateTime.Now
    Dim sSuffix As String = CType(dt.Year, String) & CType(dt.Month, String) & CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & CType(dt.Second, String) & CType(dt.Millisecond, String)
    Private objDealer As Dealer
    Private companyCode As String
#End Region

#Region "Custom Method"

    Private Sub AssignAttributeControl()
        Dim lblPopUp As Label = CType(Page.FindControl("lblPopUpDealer"), Label)
        lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpDealerSelection.aspx", "", 500, 760, "DealerSelection")
    End Sub

    Private Function ConvertKodeDealer(ByVal sKodeDealerColl As String)
        Dim sKodeDealerTemp() As String = sKodeDealerColl.Split(New Char() {";"})
        Dim sKodeDealer As String = ""
        For i As Integer = 0 To sKodeDealerTemp.Length - 1
            sKodeDealer = sKodeDealer & "'" & sKodeDealerTemp(i).Trim() & "'"

            If Not (i = sKodeDealerTemp.Length - 1) Then
                sKodeDealer = sKodeDealer & ","
            End If
        Next
        sKodeDealer = "(" & sKodeDealer & ")"
        Return sKodeDealer
    End Function

    Private Sub bindGrid()
        Dim arlist As ArrayList
        Dim arlist2 As New ArrayList
        Dim arlist3 As New ArrayList
        Dim objFreeService As FreeService
        Dim hit As Integer
        Dim srcDate As New DateTime(ICSampai.Value.Year, ICSampai.Value.Month, ICSampai.Value.Day, 23, 59, 59, 999)

        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "Status", MatchType.Exact, CType(EnumFSStatus.FSStatus.Proses, String)))

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If Not (txtKodeDealer.Text.Trim() = "") Then
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "Dealer.DealerCode", MatchType.InSet, ConvertKodeDealer(txtKodeDealer.Text.Trim())))
        End If

        criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ReleaseDate", MatchType.LesserOrEqual, srcDate))
        If Me.ddlCategory.SelectedValue > -1 Then
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.Category.ID", MatchType.Exact, Me.ddlCategory.SelectedValue))
        End If
        arlist = New FreeServiceFacade(User).Retrieve(criterias2)
        For count As Integer = 0 To arlist.Count - 1
            hit += 1
            objFreeService = arlist.Item(count)
            arlist3.Add(objFreeService.Dealer.ID)
            Dim retDat As String = arlist3.Item(count).ToString().Trim + " "
            Dim itemFound As Integer = 0
            For count2 As Integer = 0 To arlist2.Count - 1
                If objFreeService.Dealer.ID = arlist2.Item(count2).ToString().Trim Then
                    itemFound += 1
                End If
            Next
            If itemFound <= 0 Then
                arlist2.Add(objFreeService.Dealer.ID)
            End If
        Next
        total = hit.ToString.Trim
        ViewState("total") = total


        Dim whereCond As String
        If hit = 0 Then
            'If chkdownload.Checked Then
            'MessageBox.Show(SR.DataNotFoundByStatus("Free Service", "Proses"))
            'Else
            'MessageBox.Show(SR.DataNotFoundByStatus("Free Service", "Rilis"))
            'End If
            whereCond = "('')"
        Else

            whereCond = "("
            For count As Integer = 0 To arlist2.Count - 1
                whereCond += arlist2.Item(count).ToString.Trim + ","
            Next
            whereCond = whereCond.Substring(0, whereCond.Length - 1) + ")"
        End If

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not (txtKodeDealer.Text.Trim() = "") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.InSet, ConvertKodeDealer(txtKodeDealer.Text.Trim())))
        End If

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ID", MatchType.InSet, whereCond))
        sHDownFS.SetSession("DealerFac", criterias)
        dtgFStoSAP.DataSource = New DealerFacade(User).Retrieve(criterias)
        dtgFStoSAP.DataBind()
    End Sub

    Private Function hitungFS(ByVal id As Integer) As Integer
        Dim arlist As ArrayList
        Dim srcDate As New DateTime(ICSampai.Value.Year, ICSampai.Value.Month, ICSampai.Value.Day, 23, 59, 59, 999)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "Dealer.ID", MatchType.Exact, id))


        'agus puts code
        'If chkdownload.Checked Then
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "Status", MatchType.Exact, CType(EnumFSStatus.FSStatus.Proses, String)))
        'Else
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "Status", MatchType.Exact, CType(EnumFSStatus.FSStatus.Rilis, String)))
        'End If

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ReleaseDate", MatchType.LesserOrEqual, srcDate))

        If Me.ddlCategory.SelectedValue > -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.Category.ID", MatchType.Exact, Me.ddlCategory.SelectedValue))
        End If

        arlist = New FreeServiceFacade(User).Retrieve(criterias)
        Return arlist.Count - 1
    End Function

    Private Sub bindFreeService()
        Dim srcDate As New DateTime(ICSampai.Value.Year, ICSampai.Value.Month, ICSampai.Value.Day, 23, 59, 59, 999)
        Dim arListFS As ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'agus puts code
        'If chkdownload.Checked Then
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "Status", MatchType.Exact, CType(EnumFSStatus.FSStatus.Proses, String)))
        'Else
        'criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.FreeService), "Status", MatchType.Exact, CType(EnumFSStatus.FSStatus.Rilis, String)))
        'End If

        If Not (txtKodeDealer.Text.Trim() = "") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "Dealer.DealerCode", MatchType.InSet, ConvertKodeDealer(txtKodeDealer.Text.Trim())))
        End If

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ReleaseDate", MatchType.LesserOrEqual, srcDate))
        If Me.ddlCategory.SelectedValue > -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.Category.ID", MatchType.Exact, Me.ddlCategory.SelectedValue))
        End If
        arListFS = New FreeServiceFacade(User).Retrieve(criterias)
        totRow = arListFS.Count - 1
        sHDownFS.SetSession("DowloadFS", arListFS)
    End Sub

    Private Sub checkFileExistenceToDownload()

        Dim finfo As FileInfo = New FileInfo(Server.MapPath("") & "\..\DataTemp\FSData" & sSuffix & ".txt")

        If finfo.Exists Then
            finfo.Delete()
        End If

    End Sub

    Private Sub appendText()
        Dim strText As New StringBuilder
        Dim objAl As New ArrayList
        Dim ObjFreeServiceFacade As FreeServiceFacade = New FreeServiceFacade(User)

        If totRow <> -1 Then
            checkFileExistenceToDownload()
            For count As Integer = 0 To totRow
                objAl = CType(sHDownFS.GetSession("DowloadFS"), ArrayList)
                Dim RowValue As FreeService = CType(objAl.Item(count), FreeService)
                strText = New StringBuilder

                Dim arlistDealer As ArrayList
                Dim objDealer As Dealer
                Dim criteriasDealer As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriasDealer.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ID", MatchType.Exact, RowValue.Dealer.ID))
                arlistDealer = New DealerFacade(User).Retrieve(criteriasDealer)
                Dim listTitle As New EnumDealerTittle
                Dim al2 As ArrayList = listTitle.RetrieveTitle
                For Each objDealer In arlistDealer

                    'Modifikasi, karena yg diminta code dealer bukan nama dealer
                    strText.Append(objDealer.DealerCode)
                Next
                strText.Append(",")

                Dim arlistChassis As ArrayList
                Dim objChassisMaster As ChassisMaster
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ID", MatchType.Exact, RowValue.ChassisMaster.ID))
                arlistChassis = New ChassisMasterFacade(User).Retrieve(criterias)
                For Each objChassisMaster In arlistChassis
                    strText.Append(objChassisMaster.ChassisNumber)
                Next
                strText.Append(",")

                Dim arlistFSKind As ArrayList
                Dim objFSKind As FSKind
                Dim criteriasFSKind As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriasFSKind.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKind), "ID", MatchType.Exact, RowValue.FSKind.ID))
                arlistFSKind = New FSKindFacade(User).Retrieve(criteriasFSKind)
                For Each objFSKind In arlistFSKind
                    strText.Append(objFSKind.KindCode)
                Next
                strText.Append(",")

                'Modifikasi, untuk format tanggal service ddmmyyyy

                If RowValue.ServiceDate.Date = "1/1/1900" Then
                    strText.Append(",")
                Else
                    'Dim tgl As String = RowValue.ServiceDate.Date.ToShortDateString
                    'Dim delimStr As String = "/"
                    'Dim delimeter As Char() = delimStr.ToCharArray
                    'Dim strtmp As String() = tgl.Split(delimeter)
                    'If Len(strtmp(0)) = 1 Then strtmp(0) = "0" & strtmp(0)
                    'If Len(strtmp(1)) = 1 Then strtmp(1) = "0" & strtmp(1)
                    'strText.Append(strtmp(1) & strtmp(0) & strtmp(2))
                    Dim tgl As Date = RowValue.ServiceDate.Date '.ToShortDateString
                    Dim tahun As String = tgl.Year.ToString
                    Dim bulan As String = tgl.Month.ToString
                    Dim tanggal As String = tgl.Day.ToString
                    If Len(bulan) = 1 Then bulan = "0" & bulan
                    If Len(tanggal) = 1 Then tanggal = "0" & tanggal
                    strText.Append(tanggal & bulan & tahun)
                    strText.Append(",")
                End If

                'Modifikasi, untuk format tanggal penjualan ddmmyyyy
                If RowValue.SoldDate.Date = "1/1/1900" Then
                    strText.Append(",")
                Else
                    'Dim tgl2 As String = RowValue.SoldDate.Date.ToShortDateString
                    'Dim strtmp2 As String() = tgl2.Split(delimeter)
                    'If Len(strtmp2(0)) = 1 Then strtmp2(0) = "0" & strtmp2(0)
                    'If Len(strtmp2(1)) = 1 Then strtmp2(1) = "0" & strtmp2(1)
                    'strText.Append(strtmp2(1) & strtmp2(0) & strtmp2(2))
                    Dim tgl As Date = RowValue.SoldDate.Date '.ToShortDateString
                    Dim tahun As String = tgl.Year.ToString
                    Dim bulan As String = tgl.Month.ToString
                    Dim tanggal As String = tgl.Day.ToString
                    If Len(bulan) = 1 Then bulan = "0" & bulan
                    If Len(tanggal) = 1 Then tanggal = "0" & tanggal
                    strText.Append(tanggal & bulan & tahun)

                    strText.Append(",")
                End If

                strText.Append(RowValue.MileAge.ToString)
                strText.Append(",")

                'strText.Append(User.Identity.Name)
                strText.Append(UserInfo.Convert(RowValue.CreatedBy))

                download(strText.ToString())
            Next
            Dim objFreeServisColl As ArrayList = CType(sHDownFS.GetSession("DowloadFS"), ArrayList)
            If objFreeServisColl.Count > 0 Then
                For Each ObjFreeServise As FreeService In objFreeServisColl
                    ObjFreeServise.ReleaseDate = Today
                    ObjFreeServise.Status = CType(EnumFSStatus.FSStatus.Proses, String)
                    ObjFreeServiceFacade.Update(ObjFreeServise)
                Next

            End If
            saveToFile()
        End If
    End Sub
    Private Function GetProductCategoryCode() As String
        'Dim aFreeServices As ArrayList = CType(Session("FreeService"), ArrayList)'
        Dim aFreeServices As ArrayList = CType(Session("DowloadFS"), ArrayList)
        Dim product As String = ""
        If Not IsNothing(aFreeServices) Then
            For Each oFreeService As FreeService In aFreeServices
                If product = "" Then
                    product = oFreeService.ChassisMaster.Category.ProductCategory.Code
                Else
                    If product <> oFreeService.ChassisMaster.Category.ProductCategory.Code Then
                        Return ""
                    End If
                End If
            Next

        End If
        Return product
    End Function

    Private Sub appendTextToDownload()
        Dim strText As New StringBuilder
        Dim objAl As New ArrayList
        Dim ObjFreeServiceFacade As FreeServiceFacade = New FreeServiceFacade(User)

        If totRow <> -1 Then
            checkFileExistenceToDownload()
            Dim LMSPExFl As List(Of MSPExMappingtoFSKind) = New MSPExMappingtoFSKindFacade(User).RetrieveActiveList().Cast(Of MSPExMappingtoFSKind).ToList()
            Dim MSPExFl As Integer = 0
            For count As Integer = 0 To totRow
                objAl = CType(sHDownFS.GetSession("DowloadFS"), ArrayList)
                Dim RowValue As FreeService = CType(objAl.Item(count), FreeService)
                strText = New StringBuilder
                'Dim Product As String = ""
                'Product = Me.GetProductCategoryCode()
                'If Product = "" Then
                '    MessageBox.Show("Produk yang akan didownload ulang harus sama")
                '    Exit Sub
                'End If
                Dim arlistDealer As ArrayList
                Dim objDealer As Dealer
                Dim criteriasDealer As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriasDealer.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ID", MatchType.Exact, RowValue.Dealer.ID))
                arlistDealer = New DealerFacade(User).Retrieve(criteriasDealer)
                Dim listTitle As New EnumDealerTittle
                Dim al2 As ArrayList = listTitle.RetrieveTitle
                For Each objDealer In arlistDealer

                    'Modifikasi, karena yg diminta code dealer bukan nama dealer
                    strText.Append(objDealer.DealerCode)
                Next
                strText.Append(",")

                Dim arlistChassis As ArrayList
                Dim objChassisMaster As ChassisMaster
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ID", MatchType.Exact, RowValue.ChassisMaster.ID))
                arlistChassis = New ChassisMasterFacade(User).Retrieve(criterias)
                For Each objChassisMaster In arlistChassis
                    strText.Append(objChassisMaster.ChassisNumber)
                Next
                strText.Append(",")

                Dim arlistFSKind As ArrayList
                Dim objFSKind As FSKind
                Dim criteriasFSKind As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriasFSKind.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKind), "ID", MatchType.Exact, RowValue.FSKind.ID))
                arlistFSKind = New FSKindFacade(User).Retrieve(criteriasFSKind)
                For Each objFSKind In arlistFSKind
                    MSPExFl = LMSPExFl.Where(Function(x) x.FSKind.KindCode = objFSKind.KindCode).Count
                    strText.Append(objFSKind.KindCode)
                Next
                strText.Append(",")

                'Modifikasi, untuk format tanggal service ddmmyyyy

                If RowValue.ServiceDate.Date = "1/1/1900" Then
                    strText.Append(",")
                Else
                    'Dim tgl As String = RowValue.ServiceDate.Date.ToShortDateString
                    'Dim delimStr As String = "/"
                    'Dim delimeter As Char() = delimStr.ToCharArray
                    'Dim strtmp As String() = tgl.Split(delimeter)
                    'If Len(strtmp(0)) = 1 Then strtmp(0) = "0" & strtmp(0)
                    'If Len(strtmp(1)) = 1 Then strtmp(1) = "0" & strtmp(1)
                    'strText.Append(strtmp(1) & strtmp(0) & strtmp(2))
                    Dim tgl As Date = RowValue.ServiceDate.Date '.ToShortDateString
                    Dim tahun As String = tgl.Year.ToString
                    Dim bulan As String = tgl.Month.ToString
                    Dim tanggal As String = tgl.Day.ToString
                    If Len(bulan) = 1 Then bulan = "0" & bulan
                    If Len(tanggal) = 1 Then tanggal = "0" & tanggal
                    strText.Append(tanggal & bulan & tahun)
                    strText.Append(",")
                End If

                'Modifikasi, untuk format tanggal penjualan ddmmyyyy
                If RowValue.SoldDate.Date = "1/1/1900" Then
                    strText.Append(",")
                Else
                    Dim tgl As Date = RowValue.SoldDate.Date '.ToShortDateString
                    Dim tahun As String = tgl.Year.ToString
                    Dim bulan As String = tgl.Month.ToString
                    Dim tanggal As String = tgl.Day.ToString
                    If Len(bulan) = 1 Then bulan = "0" & bulan
                    If Len(tanggal) = 1 Then tanggal = "0" & tanggal
                    strText.Append(tanggal & bulan & tahun)

                    strText.Append(",")
                End If

                strText.Append(RowValue.MileAge.ToString)
                strText.Append(",")


                strText.Append(UserInfo.Convert(RowValue.CreatedBy))

                strText.Append(",")
                Dim tglRelease As Date = RowValue.ReleaseDate
                Dim tahunRelease As String = tglRelease.Year.ToString
                Dim bulanRelease As String = tglRelease.Month.ToString
                Dim tanggalRelease As String = tglRelease.Day.ToString
                If Len(bulanRelease) = 1 Then bulanRelease = "0" & bulanRelease
                If Len(tanggalRelease) = 1 Then tanggalRelease = "0" & tanggalRelease
                strText.Append(tanggalRelease & bulanRelease & tahunRelease)
                If MSPExFl > 0 Then
                    strText.Append(",")
                    strText.Append(CommonFunction.FSGetMSPRegNumber(RowValue))
                End If


                download(strText.ToString())
            Next

            saveToFile()
        End If
    End Sub


    Private Function download(ByVal str As String)
        Dim Product As String = Me.GetProductCategoryCode()
        Dim ChassisData As String = Server.MapPath("") & "\..\DataTemp\FSData" & sSuffix & "_" & Product.ToLower() & ".txt"
        Dim objFileStream As New FileStream(ChassisData, FileMode.Append, FileAccess.Write)
        Dim objStreamWriter As New StreamWriter(objFileStream)
        objStreamWriter.WriteLine(str)
        objStreamWriter.Close()

    End Function

    Private Sub saveToFile()
        Dim Product As String = Me.GetProductCategoryCode()
        Response.Redirect("../downloadlocal.aspx?file=DataTemp\FSData" & sSuffix & "_" & Product.ToLower() & ".txt")
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not Page.IsPostBack Then
            companyCode = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
            bindDdlCategory()
            btnDownload.Enabled = False
            btnDownload.Attributes.Add("onClick", "return userConfirm()")
            AssignAttributeControl()
            SetControlPrivilege()
            'chkdownload.Checked = False
        End If

    End Sub

    Private Sub bindDdlCategory()

        Dim aCs As ArrayList = New CategoryFacade(User).RetrieveActiveList(companyCode)
        Me.ddlCategory.Items.Clear()
        'Me.ddlCategory.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each oC As Category In aCs
            Me.ddlCategory.Items.Add(New ListItem(oC.CategoryCode, oC.ID))
        Next
    End Sub

    Private Sub SetControlPrivilege()
        btnDownload.Visible = bPrivilegeSubmitFStoSAP
    End Sub
    Private Sub ActivateUserPrivilege()
        bPrivilegeSubmitFStoSAP = SecurityProvider.Authorize(Context.User, SR.FreeServiceReleaseDownload_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.FreeServiceReleaseView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Service - Form Transfer Free Service to SAP")
        End If
    End Sub
    Private Sub dtgFStoSAP_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgFStoSAP.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As Dealer = CType(e.Item.DataItem, Dealer)

            '-- get number
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dtgFStoSAP.CurrentPageIndex * dtgFStoSAP.PageSize)

            '-- get city
            Dim lblCity As Label = CType(e.Item.FindControl("lblCity"), Label)
            If Not IsNothing(RowValue.City) Then
                lblCity.Text = RowValue.City.CityName
            End If

            '--get total free service
            Dim RetHit As Integer = hitungFS(RowValue.ID) + 1
            Dim lblTotalFS As Label = CType(e.Item.FindControl("lblTotalFS"), Label)
            lblTotalFS.Text = RetHit.ToString.Trim

            '--Call enum title and return complete name of dealer
            Dim listTitle As New EnumDealerTittle
            Dim al2 As ArrayList = listTitle.RetrieveTitle(companyCode)
            For Each item As EnumTitle In al2
                If item.ValTitle = RowValue.Title Then
                    Dim lblName As Label = CType(e.Item.FindControl("lblName"), Label)
                    lblName.Text = item.NameTitle + ". " + RowValue.DealerName
                End If
            Next
        End If

        '--Get footer item and fill
        If e.Item.ItemType = ListItemType.Footer Then
            Dim lblTotal As Label = CType(e.Item.FindControl("lblTotal"), Label)
            lblTotal.Text = ViewState("total")
        End If

    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        bindFreeService()
        appendTextToDownload()
        'chkdownload.Checked = False
        btnSearch_Click(sender, e)
    End Sub

    Private Sub dtgFStoSAP_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgFStoSAP.PageIndexChanged
        dtgFStoSAP.CurrentPageIndex = e.NewPageIndex
        'dtgFStoSAP.DataBind()
        btnSearch_Click(source, e)
    End Sub
    ' Private Function IsValidDate(ByVal strdate As String) As Boolean
    '    Dim strtgl As String = strdate.Substring(2, 2).ToString & "/" & strdate.Substring(0, 2) & "/" & strdate.Substring(4, 4)
    '    If IsDate(strtgl) Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        'If IsValidDate(ICSampai.Value) Then
        bindGrid()
        If dtgFStoSAP.Items.Count = 0 Then
            btnDownload.Enabled = False
        Else
            btnDownload.Enabled = True
        End If
        'Else
        ' MessageBox.Show(SR.InvalidCreateDate("Free service"))
        'End If
    End Sub

#End Region

    Private Sub dtgFStoSAP_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgFStoSAP.SortCommand
        If CType(ViewState("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currentSortColumn") = e.SortExpression
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
        End If

        dtgFStoSAP.SelectedIndex = -1
        dtgFStoSAP.CurrentPageIndex = 0
        bindGridSorting(dtgFStoSAP.CurrentPageIndex)
    End Sub
    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dtgFStoSAP.DataSource = New DealerFacade(User).RetrieveActiveList(CType(sHDownFS.GetSession("DealerFac"), CriteriaComposite), indexPage + 1, dtgFStoSAP.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dtgFStoSAP.VirtualItemCount = totalRow
            dtgFStoSAP.DataBind()
        End If
    End Sub
End Class