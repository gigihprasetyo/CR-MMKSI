Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Parser

Imports System.IO.Path
Imports System.IO
Imports System.Text
Imports Excel

Public Class FrmInputRetailTarget
    Inherits System.Web.UI.Page

    Private sessHelper As New SessionHelper
    Private oDealer As Dealer
    Private sessBMRTEdit As String = "sessBMRTEdit"
    Private sessUpload As String = "sessBMRTUpload"
    Private displayPriv As Boolean
    Private editPriv As Boolean
    Private deletePriv As Boolean
    Private uploadPriv As Boolean
    Private tambahPriv As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ViewState("currSortColumn") = "ID"
        ViewState("currSortDirection") = Sort.SortDirection.ASC

        Authorization()
        If Not IsPostBack Then
            BindDDLPeriod()
            BindDDLCategory()
            ClearAll()
            BindGrid()
        End If
        lblPopUpDealer.Attributes("onclick") = "ShowPopUpDealer()"
        lblPopUpTO.Attributes("onclick") = "ShowPopUpTO()"
    End Sub

    Private Sub Authorization()
        If Not SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Input_Retail_Target_Display_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=MASTER - INPUT RETAIL TARGET")
        Else
            displayPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Input_Retail_Target_Display_Privilege)
            editPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Input_Retail_Target_Edit_Privilege)
            deletePriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Input_Retail_Target_Delete_Privilege)
            uploadPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Input_Retail_Target_Upload_Privilege)
            tambahPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Input_Retail_Target_Tambah_Privilege)
        End If
    End Sub

    Protected Sub hdnDealer_ValueChanged(sender As Object, e As EventArgs) Handles hdnDealer.ValueChanged
        Dim data As String() = hdnDealer.Value.Trim.Split(";")
        txtKodeDealer.Text = data(0)
        lblPopUpTO.Visible = True
    End Sub

    Protected Sub hdnTempOut_ValueChanged(sender As Object, e As EventArgs) Handles hdnTempOut.ValueChanged
        Dim data As String() = hdnTempOut.Value.Trim.Split(";")
        txtKodeTempOut.Text = data(0)
    End Sub

    Private Sub BindDDLPeriod()
        ddlPeriodeMonth.Items.Clear()
        ddlPeriodeMonth.DataSource = enumMonthGet.RetriveMonth()
        ddlPeriodeMonth.DataValueField = "ValStatus"
        ddlPeriodeMonth.DataTextField = "NameStatus"
        ddlPeriodeMonth.DataBind()

        ddlPeriodeYear.Items.Clear()
        With ddlPeriodeYear.Items
            Dim _date As Integer = Date.Now.Year - 1
            For i As Integer = 0 To 4 Step 1
                .Add(New ListItem(_date, _date))
                _date = _date + 1
            Next
        End With
    End Sub

    Private Sub BindDDLCategory()
        ddlModel.Visible = False
        ddlCategory.Items.Clear()
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(Category), "ProductCategory.ID", MatchType.Exact, 1))
        Dim oData As ArrayList = New CategoryFacade(User).Retrieve(crit)
        With ddlCategory.Items
            .Add(New ListItem("Silahkan Pilih", -1))
            For Each MB As Category In oData
                .Add(New ListItem(MB.CategoryCode, MB.ID))
            Next
            'ddlCategory.DataSource = oData
            'ddlCategory.DataValueField = "ID"
            'ddlCategory.DataTextField = "CategoryCode"
            'ddlCategory.DataBind()
        End With
    End Sub

    Private Sub BindDDLModel(Optional ByVal ddlCatValue As Integer = 0)
        ddlModel.Items.Clear()
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.ID", MatchType.Exact, ddlCatValue))
        Dim oData As ArrayList = New SubCategoryVehicleFacade(User).Retrieve(crit)
        With ddlModel.Items
            .Add(New ListItem("Silahkan Pilih", -1))
            For Each MB As SubCategoryVehicle In oData
                .Add(New ListItem(MB.Name, MB.ID))
            Next
            'ddlModel.DataSource = oData
            'ddlModel.DataValueField = "ID"
            'ddlModel.DataTextField = "Name"
            'ddlModel.DataBind()
        End With
    End Sub

    Protected Sub ddlCategory_SelectedIndexChange(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        BindDDLModel(ddlCategory.SelectedValue)
        ddlModel.Visible = True
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearAll()
    End Sub

    Private Sub ClearAll()
        hdnDealer.Value = ""
        txtKodeDealer.Text = ""
        txtKodeTempOut.Text = ""
        txtRetailTarget.Text = ""
        ddlCategory.SelectedIndex = 0
        ddlModel.Visible = False
        lblPopUpTO.Visible = False
        lblInfoUpload.Visible = False

        btnSimpan.Visible = tambahPriv
        btnSimpan.Text = " Tambah "

        btnCancel.Visible = False
        btnSearch.Visible = True
        btnUpload.Visible = uploadPriv
        trUpload.Visible = uploadPriv
        dgListBabit.Columns(dgListBabit.Columns.Count - 1).Visible = True
        dgListBabit.Columns(dgListBabit.Columns.Count - 2).Visible = False
        Try
            FileUploadIklan.Value = Nothing
        Catch ex As Exception
        End Try

        ViewState.Remove("Upload")
        ViewState.Remove("Edit")
        '-- Bind page-1
        dgListBabit.CurrentPageIndex = 0
        BindGrid(dgListBabit.CurrentPageIndex)
    End Sub

    Private Sub BindGrid(Optional ByVal pageIndex As Integer = 0)
        Dim total As Integer = 0
        Dim crit As CriteriaComposite
        'If Not IsNothing(ViewState("Back")) Then
        '    crit = sessHelper.GetSession("criteriadownload")
        '    ViewState.Remove("Back")
        'Else
        crit = SearchCriteria()
        'End If

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(BabitMasterRetailTarget), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        Dim listSource As ArrayList = New BabitMasterRetailTargetFacade(User).Retrieve(crit, sortColl)
        If listSource.Count <> 0 Then
            CommonFunction.SortListControl(listSource, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(listSource, pageIndex, dgListBabit.PageSize)
            dgListBabit.DataSource = PagedList
            dgListBabit.VirtualItemCount = listSource.Count
            dgListBabit.CurrentPageIndex = pageIndex
            dgListBabit.DataBind()
        Else
            dgListBabit.DataSource = New ArrayList
            dgListBabit.VirtualItemCount = 0
            dgListBabit.CurrentPageIndex = 0
            dgListBabit.DataBind()
        End If
    End Sub

    Private Function SearchCriteria() As CriteriaComposite
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterRetailTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtKodeDealer.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(BabitMasterRetailTarget), "Dealer.DealerCode", MatchType.Partial, txtKodeDealer.Text))
        End If
        If txtKodeTempOut.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(BabitMasterRetailTarget), "DealerBranch.DealerBranchCode", MatchType.InSet, "('" & txtKodeTempOut.Text.Replace(";", "','") & "')"))
        End If
        If ddlCategory.SelectedIndex > 0 Then
            crit.opAnd(New Criteria(GetType(BabitMasterRetailTarget), "SubCategoryVehicle.Category.ID", MatchType.Exact, ddlCategory.SelectedValue))
        End If
        If ddlModel.SelectedIndex > 0 Then
            crit.opAnd(New Criteria(GetType(BabitMasterRetailTarget), "SubCategoryVehicle.ID", MatchType.Exact, ddlModel.SelectedValue))
        End If

        If cbDate.Checked Then
            crit.opAnd(New Criteria(GetType(BabitMasterRetailTarget), "MonthPeriod", MatchType.Exact, ddlPeriodeMonth.SelectedValue))
            crit.opAnd(New Criteria(GetType(BabitMasterRetailTarget), "YearPeriod", MatchType.Exact, ddlPeriodeYear.SelectedValue))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(BabitMasterRetailTarget), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        '-- Retrieve recordset
        Dim arrBabitMasterRetailTargetList As ArrayList = New BabitMasterRetailTargetFacade(User).RetrieveByCriteria(crit, sortColl)
        If arrBabitMasterRetailTargetList.Count <= 0 Then
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        End If

        Return crit
    End Function

    Private Function ValidateData() As String
        Dim sb As StringBuilder = New StringBuilder

        If (txtKodeDealer.Text.Trim = String.Empty) Then
            sb.Append("Kode Dealer Harus Diisi\n")
        End If

        If (ddlCategory.SelectedIndex = 0 OrElse ddlModel.SelectedIndex = 0) Then
            sb.Append("Kategory / Model Harus Diisi\n")
        End If

        If (ddlPeriodeMonth.SelectedItem.Text = "" OrElse ddlPeriodeYear.SelectedItem.Text = "") Then
            sb.Append("Periode Harus Diisi\n")
        End If

        If (txtRetailTarget.Text.Trim = String.Empty OrElse txtRetailTarget.Text.Trim = "0") Then
            sb.Append("Retail Target Harus Diisi\n")
        End If

        Return sb.ToString()
    End Function

    Protected Sub dgListBabit_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgListBabit.ItemDataBound
        Try

            Dim lblID As Label = e.Item.FindControl("lblID")
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim lblDealerCode As Label = e.Item.FindControl("lblDealerCode")
            Dim lblDealerName As Label = e.Item.FindControl("lblDealerName")
            Dim lblTempOut As Label = e.Item.FindControl("lblTempOut")
            Dim lblModel As Label = e.Item.FindControl("lblModel")
            Dim lblTargetRetail As Label = e.Item.FindControl("lblTargetRetail")
            Dim lblPeriode As Label = e.Item.FindControl("lblPeriode")
            Dim lblStatusUpload As Label = e.Item.FindControl("lblStatusUpload")
            Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
            Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)

            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim oData As BabitMasterRetailTarget = CType(e.Item.DataItem, BabitMasterRetailTarget)

                lblID.Text = oData.ID
                lblNo.Text = (dgListBabit.PageSize * dgListBabit.CurrentPageIndex) + e.Item.ItemIndex + 1
                lblDealerCode.Text = oData.Dealer.DealerCode
                lblDealerName.Text = oData.Dealer.DealerName
                Try
                    If Not IsNothing(oData.DealerBranch) Then
                        lblTempOut.Text = oData.DealerBranch.DealerBranchCode
                    Else
                        lblTempOut.Text = ""
                    End If
                Catch
                    lblTempOut.Text = ""
                End Try
                lblModel.Text = oData.SubCategoryVehicle.Name
                lblTargetRetail.Text = oData.RetailTarget
                lblPeriode.Text = enumMonthGet.GetName(oData.MonthPeriod) & " - " & oData.YearPeriod.ToString.Substring(2, 2)
                Dim message As String = ""
                If oData.ErrorMessage = "" Then
                    message = "OK"
                    lblStatusUpload.Attributes("style") = "color: Green"
                Else
                    message = oData.ErrorMessage
                    lblStatusUpload.Attributes("style") = "color: Red"
                End If
                lblStatusUpload.Text = message

                lnkbtnEdit.Visible = editPriv
                lnkbtnDelete.Visible = deletePriv
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub dgListBabit_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgListBabit.ItemCommand
        Select Case e.CommandName
            Case "Edit"
                If btnSimpan.Text = " Tambah " Then
                    ClearAll()
                    btnUpload.Visible = False
                    btnSearch.Visible = False
                    btnCancel.Visible = tambahPriv
                    btnSimpan.Text = " Simpan "
                    dgListBabit.Columns(dgListBabit.Columns.Count - 1).Visible = False
                    trUpload.Visible = False
                End If
                ViewState("Edit") = True
                BindDDLPeriod()
                BindDDLCategory()
                Dim lblID As Label = e.Item.FindControl("lblID")
                Dim oData As BabitMasterRetailTarget = New BabitMasterRetailTargetFacade(User).Retrieve(CType(lblID.Text, Integer))
                'Dim oData As BabitMasterRetailTarget = New BabitMasterRetailTargetFacade(User).Retrieve(CType(e.Item.Cells(0).Text, Integer))
                sessHelper.SetSession(sessBMRTEdit, oData)
                txtKodeDealer.Text = oData.Dealer.DealerCode
                If Not IsNothing(oData.DealerBranch) Then
                    txtKodeTempOut.Text = oData.DealerBranch.DealerBranchCode
                Else
                    txtKodeTempOut.Text = ""
                End If
                lblPopUpTO.Visible = True
                ddlCategory.SelectedValue = oData.SubCategoryVehicle.Category.ID
                BindDDLModel(oData.SubCategoryVehicle.Category.ID)
                ddlModel.Visible = True
                ddlModel.SelectedValue = oData.SubCategoryVehicle.ID
                txtRetailTarget.Text = oData.RetailTarget
                ddlPeriodeMonth.SelectedValue = oData.MonthPeriod
                ddlPeriodeYear.SelectedValue = oData.YearPeriod
            Case "Delete"
                Dim arrUpload As ArrayList = sessHelper.GetSession(Me.sessUpload)
                Dim lblID As Label = e.Item.FindControl("lblID")
                Dim oData As BabitMasterRetailTarget = New BabitMasterRetailTargetFacade(User).Retrieve(CType(lblID.Text, Integer))
                'Dim oData As BabitMasterRetailTarget = New BabitMasterRetailTargetFacade(User).Retrieve(CType(e.Item.Cells(0).Text, Integer))
                oData.RowStatus = CType(DBRowStatus.Deleted, Short)
                Dim BMRTF = New BabitMasterRetailTargetFacade(User).Update(oData)
                BindGrid()
        End Select
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If btnSimpan.Text = " Tambah " Then
            ClearAll()
            btnUpload.Visible = False
            btnSearch.Visible = False
            btnCancel.Visible = tambahPriv
            btnSimpan.Text = " Simpan "
            dgListBabit.Columns(dgListBabit.Columns.Count - 1).Visible = False
            trUpload.Visible = False
            Exit Sub
        End If

        Dim oData As New BabitMasterRetailTarget
        Dim str As String
        Dim result As Integer
        If ViewState("Upload") Then
            Dim arlExcel As ArrayList = sessHelper.GetSession(sessUpload)
            For Each bmrt As BabitMasterRetailTarget In arlExcel
                result = New BabitMasterRetailTargetFacade(User).Insert(bmrt)
            Next
            ViewState.Remove("Upload")
            'ClearAll()
            'BindGrid()
        Else
            str = ValidateData()
            If (str.Length > 0) Then
                MessageBox.Show(str)
                Exit Sub
            End If

            If ViewState("Edit") Then
                oData = sessHelper.GetSession(sessBMRTEdit)
            End If
            oData.Dealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text)
            oData.DealerBranch = New DealerBranchFacade(User).Retrieve(txtKodeTempOut.Text)
            oData.SubCategoryVehicle = New SubCategoryVehicleFacade(User).Retrieve(CType(ddlModel.SelectedValue, Short))
            oData.MonthPeriod = CType(ddlPeriodeMonth.SelectedValue, Byte)
            oData.YearPeriod = CType(ddlPeriodeYear.SelectedValue, Short)
            oData.RetailTarget = CType(txtRetailTarget.Text, Integer)
            oData.Status = 1
            If ViewState("Edit") Then
                result = New BabitMasterRetailTargetFacade(User).Update(oData)
                ViewState.Remove("Edit")
            Else
                result = New BabitMasterRetailTargetFacade(User).Insert(oData)
            End If
        End If
        If result > 0 Then
            MessageBox.Show("Simpan Berhasil")
        Else
            MessageBox.Show("Simpan Gagal")
        End If
        ClearAll()
        BindGrid()
    End Sub

    Private Sub Upload()
        If (Not FileUploadIklan.PostedFile Is Nothing) AndAlso (FileUploadIklan.PostedFile.ContentLength > 0) Then
            Dim fileExt As String = Path.GetExtension(FileUploadIklan.PostedFile.FileName)
            If Not (fileExt.ToLower() = ".xls" OrElse fileExt.ToLower() = ".xlsx") Then
                MessageBox.Show("Hanya Menerima File Excell")
                Return
            End If
            dgListBabit.DataSource = New ArrayList
            dgListBabit.DataBind()

            'Me.btnSaveUpload.Enabled = True

            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate

            Try
                Dim SrcFile As String = Path.GetFileName(FileUploadIklan.PostedFile.FileName)
                Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & DateTime.Now.ToString("dd/MM/yyy HHmmss") & SrcFile

                imp = New SAPImpersonate(_user, _password, _webServer)

                If imp.Start() Then
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(FileUploadIklan.PostedFile.InputStream, targetFile)

                    Dim parser As IParser = New PriceParser  '-- Declare parser Price
                    '-- Parse data file and store result into list
                    Dim arList As ArrayList = CType(parser.ParseNoTransaction(targetFile, "User"), ArrayList)

                    Dim i As Integer = 0
                    Dim objReader As IExcelDataReader = Nothing
                    Dim ArrUpload As New ArrayList
                    Dim ArrUploadOK As New ArrayList
                    Dim isOK As Boolean = True

                    Using stream As FileStream = File.Open(targetFile, FileMode.Open, FileAccess.Read)

                        If fileExt.ToLower.Contains("xlsx") Then
                            objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                        Else
                            objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                        End If

                        If (Not IsNothing(objReader)) Then
                            While objReader.Read()

                                If i >= 1 Then
                                    Dim objRetailTarget As New BabitMasterRetailTarget

                                    'Dealer
                                    Dim strDealerCode As String = ""
                                    Dim objDealer As Dealer = New Dealer

                                    If IsNothing(objReader.GetString(1)) Then Exit While

                                    Try
                                        Try
                                            strDealerCode = objReader.GetString(1).Trim()
                                        Catch exx As Exception
                                            Throw New Exception("Kode Dealer Tidak Valid")
                                        End Try

                                        If strDealerCode.Trim <> "" Then
                                            Dim arlDealer As Dealer = New DealerFacade(User).Retrieve(strDealerCode)
                                            If arlDealer.ID > 0 Then
                                                objDealer = arlDealer
                                            Else
                                                Throw New Exception("Dealer tidak terdaftar")
                                            End If
                                        End If

                                    Catch ex As Exception
                                        objRetailTarget.ErrorMessage = ex.Message
                                    Finally
                                        objRetailTarget.Dealer = objDealer
                                    End Try

                                    'Temp Out
                                    Dim strTempOut As String = ""
                                    Dim objTempOut As DealerBranch = New DealerBranch


                                    Try
                                        If Not IsNothing(objReader.GetString(2)) Then
                                            Try
                                                strTempOut = objReader.GetString(2).Trim()
                                            Catch exx As Exception
                                                Throw New Exception("Kode TempOut Tidak Valid")
                                            End Try
                                        End If

                                        If strTempOut.Trim <> "" Then
                                            Dim arlTempOut As DealerBranch = New DealerBranchFacade(User).Retrieve(strTempOut)
                                            If Not IsNothing(arlTempOut) Then
                                                If arlTempOut.ID > 0 Then
                                                    objTempOut = arlTempOut
                                                End If
                                            Else
                                                Throw New Exception("TempOut tidak terdaftar")
                                            End If
                                        End If

                                    Catch ex As Exception
                                        objRetailTarget.ErrorMessage = ex.Message
                                    Finally
                                        objRetailTarget.DealerBranch = objTempOut
                                    End Try

                                    'Model
                                    Dim StrModelName As String = ""
                                    Dim objSubCategoryVehicle As SubCategoryVehicle = New SubCategoryVehicle

                                    If IsNothing(objReader.GetString(3)) Then Exit While

                                    Try
                                        Try
                                            StrModelName = objReader.GetString(3).Trim()
                                        Catch exx As Exception
                                            Throw New Exception("Nama Model Tidak Valid")
                                        End Try

                                        If StrModelName.Trim <> "" Then
                                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                            criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Name", MatchType.Exact, StrModelName))
                                            Dim arlSubCategoryVehicle As ArrayList = New SubCategoryVehicleFacade(User).Retrieve(criterias)
                                            If arlSubCategoryVehicle.Count > 0 Then
                                                objSubCategoryVehicle = CType(arlSubCategoryVehicle(0), SubCategoryVehicle)
                                            Else
                                                Throw New Exception("Model tidak terdaftar di Master Kategori Kendaraan")
                                            End If
                                        End If

                                    Catch ex As Exception
                                        objRetailTarget.ErrorMessage = ex.Message
                                    Finally
                                        objRetailTarget.SubCategoryVehicle = objSubCategoryVehicle
                                    End Try

                                    'RetailTarget
                                    Dim RetailTarget As Decimal = 0
                                    Try
                                        Dim strRetailTarget As String = objReader.GetString(4).Trim()
                                        Try
                                            RetailTarget = CDec(strRetailTarget)
                                        Catch
                                            Throw New Exception("Format retail target salah")
                                        End Try

                                    Catch ex As Exception
                                        objRetailTarget.ErrorMessage = ex.Message
                                    Finally
                                        objRetailTarget.RetailTarget = RetailTarget
                                    End Try

                                    'Periode
                                    Dim PeriodeMonth As Integer = 0
                                    Dim PeriodeYear As Integer = 0

                                    If IsNothing(objReader.GetString(5)) Then Exit While

                                    Try
                                        Dim strPeriode As Date = objReader.GetString(5).Trim()
                                        Try
                                            PeriodeMonth = strPeriode.Month
                                            PeriodeYear = strPeriode.Year
                                        Catch exx As Exception
                                            Throw New Exception("Periode Tidak Valid")
                                        End Try

                                    Catch ex As Exception
                                        objRetailTarget.ErrorMessage = ex.Message
                                    Finally
                                        objRetailTarget.MonthPeriod = PeriodeMonth
                                        objRetailTarget.YearPeriod = PeriodeYear
                                    End Try

                                    If objRetailTarget.ErrorMessage <> "" Then
                                        isOK = False
                                    End If

                                    ArrUpload.Add(objRetailTarget)
                                End If
                                i = i + 1
                            End While
                        End If
                    End Using

                    If ArrUpload.Count <> 0 Then
                        CommonFunction.SortListControl(ArrUpload, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
                        Dim PagedList As ArrayList = ArrayListPager.DoPage(ArrUpload, dgListBabit.CurrentPageIndex, dgListBabit.PageSize)
                        dgListBabit.DataSource = PagedList
                        dgListBabit.VirtualItemCount = ArrUpload.Count
                        dgListBabit.CurrentPageIndex = dgListBabit.CurrentPageIndex
                        dgListBabit.DataBind()
                    Else
                        dgListBabit.DataSource = New ArrayList
                        dgListBabit.VirtualItemCount = 0
                        dgListBabit.CurrentPageIndex = 0
                        dgListBabit.DataBind()
                    End If
                    dgListBabit.ShowFooter = False
                    sessHelper.SetSession(Me.sessUpload, ArrUpload)

                    lblInfoUpload.Text = "Status data berdasarkan File, sebelum data disimpan"
                    lblInfoUpload.Visible = True
                    btnUpload.Visible = False
                    dgListBabit.Columns(dgListBabit.Columns.Count - 1).Visible = False
                    dgListBabit.Columns(dgListBabit.Columns.Count - 2).Visible = True
                    If Not isOK Then
                        btnSimpan.Visible = False
                    Else
                        ViewState("Upload") = True
                        btnSimpan.Text = " Simpan "
                        btnUpload.Visible = False
                        btnSearch.Visible = False
                        btnCancel.Visible = True
                    End If
                End If

            Catch ex As Exception
                MessageBox.Show("Process Gagal")
            Finally

                imp.StopImpersonate()
                imp = Nothing
            End Try

        Else
            MessageBox.Show("Berkas Upload tidak boleh Kosong")
        End If
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Upload()
    End Sub

    Protected Sub lbtnDownloadExcel_Click(sender As Object, e As EventArgs) Handles lbtnDownloadExcel.Click
        Dim strName As String = "Template_Retail_Target.xlsx"
        Response.Redirect("../downloadlocal.aspx?file=DataFile\Babit\" & strName)
    End Sub

    Private Sub dgListBabit_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgListBabit.PageIndexChanged
        dgListBabit.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
    End Sub

    Private Sub dgListBabit_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgListBabit.SortCommand
        '-- Sort datagrid rows based on a column header clicked

        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If

        '-- Bind page-1
        dgListBabit.CurrentPageIndex = 0
        BindGrid(dgListBabit.CurrentPageIndex)
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        dgListBabit.CurrentPageIndex = 0 '-- Read all data matching criteria
        BindGrid(dgListBabit.CurrentPageIndex)  '-- Bind page-1
    End Sub
End Class