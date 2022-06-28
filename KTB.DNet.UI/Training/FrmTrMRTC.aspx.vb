#Region "Custom Namespace Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports System.Collections.Generic
Imports System.Linq

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.UI.Helper
Imports GlobalExtensions
#End Region

Public Class FrmTrMRTC
    Inherits System.Web.UI.Page

    Dim gridColNo As Integer = 0
    Private helpers As New TrainingHelpers(Me.Page)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        helpers.CheckPrivilege("priv8B")
        Try
            If Not Page.IsPostBack Then
                InitForm()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Protected Sub InitForm()
        ddlStatus.ClearSelection()

        rbNo.Checked = True
        trListDealer.Visible = True
        btnSimpan.Enabled = True
        btnCari.Enabled = True
        btnBatal.Enabled = True
        ViewState("CurrentSortColumn") = "Code"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        txtMRTCCode.Text = String.Empty
        txtName.Text = String.Empty
        txtDealerCode.Text = String.Empty
        txtAlamat.Text = String.Empty
        txtHead.Text = String.Empty
        txtInstruktur.Text = String.Empty
        txtListDealer.Text = String.Empty
        txtPricePerDay.Text = GetDefaultPricePerDay()
        SetControlEnabled(True)
        FillDropDownList()
        BindDataGrid(0)
        SetActiveControl(helpers.IsEdit)
    End Sub

    Private Sub SetActiveControl(ByVal isActive As Boolean)
        btnSimpan.Visible = isActive
        btnBatal.Visible = isActive
    End Sub

    Private Sub SetControlEnabled(ByVal value As Boolean)

        'HARDCODE sementara karena berada di dalam tr
        txtListDealer.Enabled = value
        lblPopUpListDealer.Visible = value

        For Each ctrl As Control In pnlInput.Controls

            If TypeOf ctrl Is TextBox Then
                TryCast(ctrl, TextBox).Enabled = value
                Continue For
            End If

            If TypeOf ctrl Is RadioButton Then
                TryCast(ctrl, RadioButton).Enabled = value
                Continue For
            End If

            If TypeOf ctrl Is DropDownList Then
                TryCast(ctrl, DropDownList).Enabled = value
                Continue For
            End If

            If TypeOf ctrl Is Label Then
                If ctrl.ID.Contains("PopUp") Then
                    TryCast(ctrl, Label).Visible = value
                    Continue For
                End If
            End If

        Next
    End Sub

    Private Function GetDefaultPricePerDay() As String
        Try
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Reference), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Reference), "Type", MatchType.Exact, "TRASS"))
            criterias.opAnd(New Criteria(GetType(Reference), "Code", MatchType.Exact, "HDT"))

            Dim defaultPrice As Reference = New ReferenceFacade(User).Retrieve(criterias)(0)
            Return defaultPrice.Description.AddThousandDelimiter()

        Catch ex As Exception
            Return String.Empty
        End Try

    End Function

    Private Sub FillDropDownList()
        BindDdlMainArea()
        BindDdlArea1()
        BindDdlProvince()
        BindDdlCity()
        ddlStatus.SelectedValue = "1"
    End Sub

    Private Sub BindDdlMainArea()
        ddlMainArea.Items.Clear()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MainArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        ddlMainArea.DataSource = New MainAreaFacade(User).RetrieveActiveList(criterias, "Description", Sort.SortDirection.ASC)
        ddlMainArea.DataTextField = "Description"
        ddlMainArea.DataValueField = "ID"
        ddlMainArea.DataBind()
        ddlMainArea.Items.Insert(0, New ListItem("Silakan Pilih", "0"))
    End Sub

    Private Sub BindDdlArea1(Optional ByVal selectedMainArea As String = "0")
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area1), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Area1), "MainArea.ID", MatchType.Exact, CType(selectedMainArea, Integer)))
        Dim arrArea1 As ArrayList = New Area1Facade(User).RetrieveActiveList(criterias, "Description", Sort.SortDirection.ASC)
        ddlArea1.BindDDLFromList(arrArea1, "ID", "Description")

    End Sub


    Private Sub BindDdlProvince()
        ddlPropinsi.Items.Clear()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        ddlPropinsi.DataSource = New ProvinceFacade(User).RetrieveActiveList(criterias, "ProvinceName", Sort.SortDirection.ASC)
        ddlPropinsi.DataTextField = "ProvinceName"
        ddlPropinsi.DataValueField = "ID"
        ddlPropinsi.DataBind()
        ddlPropinsi.Items.Insert(0, New ListItem("Silahkan Pilih", "0"))
    End Sub

    Private Sub BindDdlCity(Optional ByVal selectedProvince As String = "0")
        ddlKota.Items.Clear()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A")) 'A = Aktif; X = Tidak Aktif
        criterias.opAnd(New Criteria(GetType(City), "Province.ID", MatchType.Exact, CType(selectedProvince, Integer)))

        ddlKota.DataSource = New CityFacade(User).RetrieveActiveList(criterias, "CityName", Sort.SortDirection.ASC)
        ddlKota.DataTextField = "CityName"
        ddlKota.DataValueField = "ID"
        ddlKota.DataBind()
        ddlKota.Items.Insert(0, New ListItem("Silahkan Pilih", "0"))
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        gridColNo = 0
        dtgMRTC.DataSource = New TrMRTCFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, dtgMRTC.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgMRTC.VirtualItemCount = totalRow
        dtgMRTC.DataBind()
    End Sub

    Private Function CriteriaSearch() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrMRTC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If rbNo.Checked Then
            criterias.opAnd(New Criteria(GetType(TrMRTC), "IsMainDealer", MatchType.Exact, 0))
        End If

        If rbYes.Checked Then
            criterias.opAnd(New Criteria(GetType(TrMRTC), "IsMainDealer", MatchType.Exact, 1))
        End If

        If txtMRTCCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(TrMRTC), "Code", MatchType.[Partial], txtMRTCCode.Text))
        End If

        If txtName.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(TrMRTC), "Name", MatchType.[Partial], txtName.Text))
        End If

        If txtDealerCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(TrMRTC), "Dealer.DealerCode", MatchType.[Partial], txtDealerCode.Text))
        End If

        If ddlMainArea.SelectedValue <> "0" Then
            criterias.opAnd(New Criteria(GetType(TrMRTC), "MainArea.ID", MatchType.Exact, ddlMainArea.SelectedValue))
        End If

        If ddlArea1.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(TrMRTC), "Area1.ID", MatchType.Exact, ddlArea1.SelectedValue))
        End If

        If ddlKota.SelectedValue <> "0" Then
            criterias.opAnd(New Criteria(GetType(TrMRTC), "City.ID", MatchType.Exact, ddlKota.SelectedValue))
        End If

        If ddlPropinsi.SelectedValue <> "0" Then
            criterias.opAnd(New Criteria(GetType(TrMRTC), "City.Province.ID", MatchType.Exact, ddlPropinsi.SelectedValue))
        End If

        If txtAlamat.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(TrMRTC), "Address", MatchType.[Partial], txtAlamat.Text))
        End If

        If ddlStatus.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(TrMRTC), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If


        'If txtReplacementCode.Text <> "" Then
        '    Dim strInset As String = "(SELECT ID FROM TrReplacementDetail detail"
        '    strInset &= " INNER JOIN TrCourse course ON detail.TrCourseID = course.ID"
        '    strInset &= " WHERE course.ID IN ('" + Replace(txtReplacementCode.Text, ";", "','") + "'))"
        '    criterias.opAnd(New Criteria(GetType(TrReplacementHeader), "ID", MatchType.InSet, strInset))
        'End If




        Return criterias
    End Function

    Private Sub ddlMainArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMainArea.SelectedIndexChanged
        Try
            BindDdlArea1(ddlMainArea.SelectedValue)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ddlPropinsi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPropinsi.SelectedIndexChanged
        Try
            BindDdlCity(ddlPropinsi.SelectedValue)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Protected Sub cvMainArea_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try
            If ddlMainArea.SelectedIndex = 0 Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            args.IsValid = False
        End Try
    End Sub

    Protected Sub cvArea1_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try
            If ddlArea1.SelectedIndex = 0 Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            args.IsValid = False
        End Try
    End Sub

    Protected Sub cvPropinsi_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try
            If ddlPropinsi.SelectedIndex = 0 Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            args.IsValid = False
        End Try
    End Sub

    Protected Sub cvKota_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try
            If ddlKota.SelectedIndex = 0 Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            args.IsValid = False
        End Try
    End Sub

    Protected Sub cvDealer_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try
            If txtDealerCode.Text.Trim = String.Empty Then
                cvDealer.ErrorMessage = "* Dealer harus dipilih"
                args.IsValid = False
            ElseIf IsDealerCodeExist(txtDealerCode.Text.Trim) = False Then
                cvDealer.ErrorMessage = "* Dealer tidak terdaftar dalam database"
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            args.IsValid = False
        End Try
    End Sub

    Private Function IsDealerCodeExist(ByVal dealerCode As String) As Boolean
        Try
            Dim dealer As Dealer = New DealerFacade(User).Retrieve(dealerCode)
            If dealer.ID = 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function


    Protected Sub cvHead_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try
            If txtHead.Text.Trim = String.Empty Then
                cvHead.ErrorMessage = "* Head MRTC harus dipilih"
                args.IsValid = False
                Exit Sub
            Else
                Dim splitString() As String = txtHead.Text.Trim.Split("-")
                Dim traineeID As String = ""
                Dim traineeName As String = ""
                Dim errMessage As String = String.Empty

                If splitString.Count > 0 Then
                    traineeID = splitString(0).Trim()
                End If

                If IsTraineeValid(traineeID, errMessage) = False Then
                    If errMessage <> "" Then
                        cvHead.ErrorMessage = "* " + errMessage
                        args.IsValid = False
                    Else
                        args.IsValid = True
                    End If
                Else
                    args.IsValid = True
                    'cvHead.ErrorMessage = "* Data Head MRTC dengan kode " + traineeID + " tidak terdaftar dalam database"
                    'args.IsValid = False
                End If
            End If
        Catch ex As Exception
            args.IsValid = False
        End Try
    End Sub

    Private Function IsTraineeValid(ByVal id As String, Optional ByRef errMessage As String = "") As Boolean
        Try
            Dim trainee As TrTrainee = New TrTraineeFacade(User).Retrieve(CType(id.Trim(), Integer))
            If trainee.ID = 0 Then
                errMessage = "Trainee dengan Nomor registrasi " & id & " tidak ditemukan"
                Return False
            End If

            If trainee.Dealer.DealerCode.ToLower() <> txtDealerCode.Text.Trim.ToLower() Then
                errMessage = "Trainee dengan Nomor registrasi " & id & " terdaftar pada Dealer Lain"
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub cvInstruktur_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try
            If txtInstruktur.Text.Trim = String.Empty Then
                cvInstruktur.ErrorMessage = "* Instruktur MRTC harus dipilih"
                args.IsValid = False
                Exit Sub
            Else
                Dim listTraineeId As List(Of String) = New List(Of String)
                Dim splitTrainee() As String = txtInstruktur.Text.Split(";")

                For Each trainee As String In splitTrainee
                    Dim traineeId = trainee.Trim()

                    If trainee <> String.Empty Then
                        listTraineeId.Add(trainee)
                        Dim errMessage As String = String.Empty
                        If IsTraineeValid(trainee, errMessage) = False Then
                            If errMessage <> "" Then
                                cvHead.ErrorMessage = "* " + errMessage
                                args.IsValid = False
                            End If
                        Else
                            cvInstruktur.ErrorMessage = "* Data Instruktur dengan kode " + trainee + " tidak terdaftar dalam database"
                            args.IsValid = False
                        End If
                    End If
                Next

                If listTraineeId.Count = 0 Then
                    cvInstruktur.ErrorMessage = "* Instruktur MRTC harus dipilih"
                    args.IsValid = False
                Else
                    args.IsValid = True
                End If

            End If
        Catch ex As Exception
            args.IsValid = False
        End Try
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Try
            If Not Page.IsValid Then
                Exit Sub
            End If

            Dim dataMRTC As TrMRTC = MappingFromUiToDataMRTC()
            Dim listPIC As List(Of TrMRTCPIC) = MappingFromUiToListPIC()
            Dim listDealer As List(Of TrMRTCDealer) = MappingFromUiToListDealer()

            ValidateListDealer(dataMRTC, listDealer)
            'Dim errMessage As String = GetErrorMessage(dataHeader, arlDataDetail)

            'If errMessage <> String.Empty Then
            '    MessageBox.Show(errMessage)
            '    Exit Sub
            'End If

            Dim mrtcFacade As TrMRTCFacade = New TrMRTCFacade(User)
            mrtcFacade.Save(dataMRTC, listPIC, listDealer)

            MessageBox.Show("Data berhasil disimpan")
            InitForm()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function MappingFromUiToDataMRTC() As TrMRTC
        Dim data As TrMRTC

        Dim mrtcFacade As TrMRTCFacade = New TrMRTCFacade(User)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrMRTC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrMRTC), "Code", MatchType.Exact, txtMRTCCode.Text.Trim()))

        Dim arlResult As ArrayList = mrtcFacade.Retrieve(criterias)

        If arlResult.Count > 0 Then
            data = arlResult(0)
        Else
            data = New TrMRTC
            data.Code = txtMRTCCode.Text.Trim()
        End If

        data.Name = txtName.Text.Trim()
        data.IsMainDealer = rbYes.Checked
        data.Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim())
        data.MainArea = New MainAreaFacade(User).Retrieve(CType(ddlMainArea.SelectedValue, Integer))
        data.Area1 = New Area1Facade(User).Retrieve(CType(ddlArea1.SelectedValue, Integer))
        data.City = New CityFacade(User).Retrieve(CType(ddlKota.SelectedValue, Integer))
        data.Address = txtAlamat.Text.Trim()
        data.Status = CType(ddlStatus.SelectedValue, Short)
        data.PricePerDay = txtPricePerDay.Text.RemoveThousandDelimeter()

        Return data
    End Function

    Private Function MappingFromUiToListPIC() As List(Of TrMRTCPIC)
        Dim result As New List(Of TrMRTCPIC)

        result.Add(GetHeadMRTC())
        result.AddRange(GetListInstrukturMRTC())

        Return result
    End Function

    Private Function MappingFromUiToListDealer() As List(Of TrMRTCDealer)
        Dim result As New List(Of TrMRTCDealer)

        If rbYes.Checked = False Then
            result.AddRange(MappingListDealerMRTC())
        End If

        Return result
    End Function

    Private Function GetHeadMRTC() As TrMRTCPIC
        Dim headData As New TrMRTCPIC
        Dim splitString() As String = txtHead.Text.Trim.Split("-")
        Dim traineeID As String = ""

        If splitString.Count > 0 Then
            traineeID = splitString(0)
        Else
            Throw New Exception("Format Head Salah")
        End If

        Dim headTrainee As TrTrainee = New TrTraineeFacade(User).Retrieve(CType(traineeID, Integer))
        headData.TrTrainee = headTrainee
        headData.Type = EnumTrMRTC.TypePIC.Head
        headData.Status = 1
        Return headData
    End Function

    Private Function GetListInstrukturMRTC() As List(Of TrMRTCPIC)
        Dim result As New List(Of TrMRTCPIC)
        Dim splitTrainee() As String = txtInstruktur.Text.Split(";")

        For Each trainee As String In splitTrainee
            If trainee.Trim <> String.Empty Then
                Dim instrukturData As New TrMRTCPIC
                Dim splitString() As String = trainee.Split(New Char() {"-"}, StringSplitOptions.RemoveEmptyEntries)

                Dim traineeID As String = splitString(0).Trim
                Dim instrukturTrainee As TrTrainee = New TrTraineeFacade(User).Retrieve(CType(traineeID, Integer))
                instrukturData.TrTrainee = instrukturTrainee
                instrukturData.Type = EnumTrMRTC.TypePIC.Instruktur
                instrukturData.Status = 1
                result.Add(instrukturData)
            End If
        Next
        Return result
    End Function

    Private Function MappingListDealerMRTC() As List(Of TrMRTCDealer)
        Dim result As New List(Of TrMRTCDealer)
        Dim splitDealer() As String = txtListDealer.Text.Split(";")

        For Each dealerCode As String In splitDealer
            Dim data As New TrMRTCDealer

            Dim dealerData As Dealer = New DealerFacade(User).Retrieve(dealerCode)
            data.Dealer = dealerData
            data.Status = 1
            result.Add(data)

        Next

        'add dealer mrtc tersebut
        Dim dataCurrent As New TrMRTCDealer

        Dim currentDealerData As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text)
        dataCurrent.Dealer = currentDealerData
        dataCurrent.Status = 1
        result.Add(dataCurrent)

        Return result
    End Function

    Private Sub dtgMRTC_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgMRTC.ItemCommand
        If (e.CommandName = "View") Then
            SendGridItemToField(e.Item.Cells(0).Text)
            btnSimpan.Enabled = False
            SetControlEnabled(False)
        ElseIf (e.CommandName = "Edit") Then
            SetControlEnabled(True)
            SendGridItemToField(e.Item.Cells(0).Text)
            rbYes.Enabled = False
            rbNo.Enabled = False
            btnSimpan.Enabled = True
            txtMRTCCode.Enabled = False
        ElseIf (e.CommandName = "Delete") Then
            DeleteMRTCData(e.Item.Cells(0).Text)
        End If
    End Sub

    Private Sub dtgMRTC_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMRTC.ItemDataBound
        Try

            If Not e.Item.DataItem Is Nothing Then
                Dim data As TrMRTC = CType(e.Item.DataItem, TrMRTC)
                gridColNo += 1

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                Dim lblMRTCCode As Label = CType(e.Item.FindControl("lblMRTCCode"), Label)
                Dim lblMRTCName As Label = CType(e.Item.FindControl("lblMRTCName"), Label)
                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                Dim lblMainAreaCode As Label = CType(e.Item.FindControl("lblMainAreaCode"), Label)
                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                Dim lblLeader As Label = CType(e.Item.FindControl("lblLeader"), Label)
                Dim lnkInstruktur As HyperLink = CType(e.Item.FindControl("lnkInstruktur"), HyperLink)
                Dim lblListDealer As Label = CType(e.Item.FindControl("lblListDealer"), Label)
                Dim lblPricePerDay As Label = CType(e.Item.FindControl("lblPricePerDay"), Label)
                Dim lblIsMainDealer As Label = CType(e.Item.FindControl("lblIsMainDealer"), Label)
                Dim btnHapus As LinkButton = CType(e.Item.FindControl("btnHapus"), LinkButton)
                Dim btnUbah As LinkButton = CType(e.Item.FindControl("btnUbah"), LinkButton)

                lblNo.Text = gridColNo
                lblMRTCCode.Text = data.Code
                lblMRTCName.Text = data.Name
                lblDealerCode.Text = data.Dealer.DealerCode + " - " + data.Dealer.DealerName
                lblMainAreaCode.Text = data.MainArea.AreaCode + " - " + data.MainArea.Description

                If data.Status = 1 Then
                    lblStatus.Text = "AKTIF"
                ElseIf data.Status = 0 Then
                    lblStatus.Text = "TIDAK AKTIF"
                End If

                Dim listPic As List(Of TrMRTCPIC) = data.ListOfDetail.Cast(Of TrMRTCPIC).ToList()

                Dim headData As TrMRTCPIC = listPic.FirstOrDefault(Function(x) x.Type = EnumTrMRTC.TypePIC.Head)
                lblLeader.Text = headData.TrTrainee.ID & " - " & headData.TrTrainee.Name

                Dim actionValue As String = "popUpListInstruktur('" & data.ID & "');"
                lnkInstruktur.NavigateUrl = "javascript:" & actionValue

                Dim stringListDealer As String = String.Empty

                For Each item As TrMRTCDealer In data.ListOfDealer
                    stringListDealer = stringListDealer & item.Dealer.DealerCode & "; "
                Next

                If stringListDealer.Length > 0 Then
                    lblListDealer.Text = stringListDealer.Remove(stringListDealer.Length - 2)
                End If

                lblPricePerDay.Text = data.PricePerDay.ToString().AddThousandDelimiter()

                If data.IsMainDealer Then
                    lblIsMainDealer.Text = "YA"
                Else
                    lblIsMainDealer.Text = "TIDAK"
                End If

                btnUbah.Visible = helpers.IsEdit
                btnHapus.Visible = helpers.IsEdit
                If helpers.IsEdit Then
                    If IsMRTCHaveActiveClass(data.Code) = True Then
                        btnHapus.Visible = False
                    Else
                        btnHapus.Visible = True
                    End If
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dtgMRTC_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgMRTC.PageIndexChanged
        Try
            dtgMRTC.CurrentPageIndex = e.NewPageIndex
            BindDataGrid(dtgMRTC.CurrentPageIndex)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub dtgMRTC_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgMRTC.SortCommand
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

            dtgMRTC.CurrentPageIndex = 0
            BindDataGrid(dtgMRTC.CurrentPageIndex)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub SendGridItemToField(id As Integer)
        Dim data As TrMRTC = New TrMRTCFacade(User).Retrieve(id)
        txtMRTCCode.Text = data.Code
        txtName.Text = data.Name
        txtDealerCode.Text = data.Dealer.DealerCode

        ddlMainArea.ClearSelection()
        ddlMainArea.Items.FindByValue(data.MainArea.ID).Selected = True
        ddlMainArea_SelectedIndexChanged(Nothing, Nothing)

        ddlArea1.ClearSelection()
        ddlArea1.Items.FindByValue(data.Area1.ID).Selected = True

        ddlPropinsi.ClearSelection()
        ddlPropinsi.Items.FindByValue(data.City.Province.ID).Selected = True
        ddlPropinsi_SelectedIndexChanged(Nothing, Nothing)

        ddlKota.ClearSelection()
        ddlKota.Items.FindByValue(data.City.ID).Selected = True

        txtAlamat.Text = data.Address
        ddlStatus.ClearSelection()

        ddlStatus.Items.FindByValue(data.Status).Selected = True

        Dim listPic As List(Of TrMRTCPIC) = data.ListOfDetail.Cast(Of TrMRTCPIC).ToList()

        Dim headData As TrMRTCPIC = listPic.FirstOrDefault(Function(x) x.Type = EnumTrMRTC.TypePIC.Head)
        txtHead.Text = headData.TrTrainee.ID & " - " & headData.TrTrainee.Name

        Dim listInstrukturData As List(Of TrMRTCPIC) = listPic.Where(Function(x) x.Type = EnumTrMRTC.TypePIC.Instruktur).ToList()

        Dim stringInstruktur As String = String.Empty
        For Each instruktur As TrMRTCPIC In listInstrukturData
            stringInstruktur = stringInstruktur & instruktur.TrTrainee.ID & " - " & instruktur.TrTrainee.Name & ";"
        Next

        If stringInstruktur.Length > 0 Then
            txtInstruktur.Text = stringInstruktur.Remove(stringInstruktur.Length - 1)
        End If

        Dim stringListDealer As String = String.Empty

        For Each item As TrMRTCDealer In data.ListOfDealer
            stringListDealer = stringListDealer & item.Dealer.DealerCode & ";"
        Next

        If stringListDealer.Length > 0 Then
            stringListDealer = stringListDealer.Remove(stringListDealer.Length - 1)
        End If

        txtListDealer.Text = stringListDealer

        txtPricePerDay.Text = data.PricePerDay.ToString().AddThousandDelimiter()

        If data.IsMainDealer = True Then
            rbYes.Checked = True
            rbNo.Checked = False
            trListDealer.Visible = False
        Else
            rbYes.Checked = False
            rbNo.Checked = True
            trListDealer.Visible = True
        End If

        Dim bAllowEditPriceAndDealer As Boolean = GetPrivilegeEditPriceAndDealer()

        txtPricePerDay.Enabled = bAllowEditPriceAndDealer




    End Sub

    Private Sub DeleteMRTCData(id As Integer)
        Try
            Dim mrtcFacade As TrMRTCFacade = New TrMRTCFacade(User)
            Dim data As TrMRTC = mrtcFacade.Retrieve(id)
            data.RowStatus = CType(DBRowStatus.Deleted, Short)
            mrtcFacade.Delete(data)
            DeleteDealerMRTCDealer(id)

            MessageBox.Show("Data berhasil dihapus")
            InitForm()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DeleteDealerMRTCDealer(ByVal mrtcId As Integer)
        Dim arlMRTCDealer As ArrayList = New ArrayList
        Dim mrtcDealerFacade As TrMRTCDealerFacade = New TrMRTCDealerFacade(User)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrMRTCDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrMRTCDealer), "TrMRTC.ID", MatchType.Exact, mrtcId))

        arlMRTCDealer = mrtcDealerFacade.Retrieve(criterias)

        For Each mrtcDealer As TrMRTCDealer In arlMRTCDealer
            mrtcDealer.RowStatus = CType(DBRowStatus.Deleted, Short)
            mrtcDealerFacade.Delete(mrtcDealer)
        Next

    End Sub

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        InitForm()
    End Sub

    Private Sub txtDealerCode_TextChanged(sender As Object, e As EventArgs) Handles txtDealerCode.TextChanged
        Try
            Dim dealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text)
            If dealer.ID <> 0 Then

                ddlMainArea.ClearSelection()
                ddlMainArea.Items.FindByValue(dealer.MainArea.ID).Selected = True

                ddlMainArea_SelectedIndexChanged(Nothing, Nothing)
                ddlArea1.ClearSelection()
                ddlArea1.Items.FindByValue(dealer.Area1.ID).Selected = True

                ddlPropinsi.ClearSelection()
                ddlPropinsi.Items.FindByValue(dealer.Province.ID).Selected = True
                ddlPropinsi_SelectedIndexChanged(Nothing, Nothing)

                ddlKota.ClearSelection()
                ddlKota.Items.FindByValue(dealer.City.ID).Selected = True

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnTriggerDealer_Click(sender As Object, e As EventArgs) Handles btnTriggerDealer.Click
        txtDealerCode_TextChanged(Nothing, Nothing)
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click

        Try
            BindDataGrid(0)
            btnSimpan.Enabled = True

            If dtgMRTC.Items.Count < 0 Then
                MessageBox.Show("Data tidak ditemukan")
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub cvListDealer_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try

            If rbNo.Checked = True Then
                If txtListDealer.Text.Trim = String.Empty Then
                    Throw New Exception("List Dealer MRTC harus diisi")
                Else
                    Dim splitDealer() As String = txtListDealer.Text.Split(";")

                    For Each dealer As String In splitDealer
                        Dim objDealer As Dealer = New DealerFacade(User).Retrieve(dealer)
                        If objDealer.ID = 0 Then
                            Throw New Exception("Kode dealer " + dealer + " tidak terdaftar dalam database")
                        Else
                            'If IsDealerExistOnOtherMRTC(dealer) Then
                            '    Throw New Exception("Kode dealer " + dealer + " sudah terdaftar pada MRTC lain")
                            'End If
                        End If
                    Next
                End If
                args.IsValid = True
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            cvListDealer.ErrorMessage = ex.Message
            args.IsValid = False
        End Try
    End Sub

    Private Function IsDealerExistOnOtherMRTC(ByVal dealerCode As String)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrMRTCDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrMRTCDealer), "TrMRTC.Code", MatchType.NotLike, txtMRTCCode.Text.Trim()))
        criterias.opAnd(New Criteria(GetType(TrMRTCDealer), "Dealer.DealerCode", MatchType.Exact, dealerCode))

        Dim arlResult As ArrayList = New TrMRTCDealerFacade(User).Retrieve(criterias)

        If arlResult.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub rbNo_CheckedChanged(sender As Object, e As EventArgs) Handles rbNo.CheckedChanged
        If rbYes.Checked = True Then
            trListDealer.Visible = False
        Else
            trListDealer.Visible = True
        End If
    End Sub

    Private Sub rbYes_CheckedChanged(sender As Object, e As EventArgs) Handles rbYes.CheckedChanged
        If rbYes.Checked = True Then
            trListDealer.Visible = False
        Else
            trListDealer.Visible = True
        End If
    End Sub

    Protected Sub cvPricePerDay_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Dim errPricePerday As String = "Harga per hari harus lebih dari 0"
        Try
            If CInt(txtPricePerDay.Text.RemoveThousandDelimeter) <= 0 Then
                cvPricePerDay.ErrorMessage = errPricePerday
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            cvPricePerDay.ErrorMessage = errPricePerday
            args.IsValid = False
        End Try
    End Sub

    Private Function IsMRTCHaveActiveClass(ByVal mrtcCode As String) As Boolean
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClass), "TrMRTC.Code", MatchType.Exact, mrtcCode))
        criterias.opAnd(New Criteria(GetType(TrClass), "FinishDate", MatchType.GreaterOrEqual, DateTime.Now.AddDays(1)))

        Dim arlResult As ArrayList = New TrClassFacade(User).Retrieve(criterias)

        If arlResult.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    Protected Sub cvStatus_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try
            If ddlStatus.SelectedValue = "-1" Then
                cvStatus.ErrorMessage = "Status harus dipilih"
                args.IsValid = False
            Else
                If ddlStatus.SelectedValue = 0 And IsMRTCHaveActiveClass(txtMRTCCode.Text) = True Then
                    cvStatus.ErrorMessage = "MRTC ini memiliki kelas yang aktif"
                    args.IsValid = False
                Else
                    args.IsValid = True
                End If
            End If
        Catch ex As Exception
            cvPricePerDay.ErrorMessage = ex.Message
            args.IsValid = False
        End Try
    End Sub

    Private Function GetPrivilegeEditPriceAndDealer() As Boolean
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Reference), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Reference), "Type", MatchType.Exact, "TRASS"))
        criterias.opAnd(New Criteria(GetType(Reference), "Code", MatchType.Exact, "EMRTC"))

        Dim ref As Reference = New ReferenceFacade(User).Retrieve(criterias)(0)

        If ref.Description.ToLower = "ya" Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub ValidateListDealer(dataMRTC As TrMRTC, listDealer As List(Of TrMRTCDealer))
        If dataMRTC.ID <> 0 Then
            Dim bAllowEditPriceAndDealer As Boolean = GetPrivilegeEditPriceAndDealer()

            If bAllowEditPriceAndDealer = False Then
                For Each oldDealer As TrMRTCDealer In dataMRTC.ListOfDealer
                    Dim foundDealer As TrMRTCDealer = listDealer.FirstOrDefault(Function(x) x.Dealer.DealerCode = oldDealer.Dealer.DealerCode)

                    If IsNothing(foundDealer) Then
                        Throw New Exception("Kode dealer " & oldDealer.Dealer.DealerCode & " tidak dapat dihapus dari MRTC ini")
                    End If

                Next
            End If

        End If


    End Sub

End Class