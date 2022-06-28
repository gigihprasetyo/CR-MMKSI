#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.SAP
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmSAPPeriod
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents icTglCreate2 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents icTglCreate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnNew As System.Web.UI.WebControls.Button
    Protected WithEvents dtgSAPPeriod As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtSAPNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents icEndConfirmedDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtEndConfirmHour As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private objSAPPeriod As SAPPeriod
    Private arlSAPPeriod As ArrayList
    Private sHelper As SessionHelper = New SessionHelper
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.SAPPeriodView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Setting Period SAP")
        End If
    End Sub

    Private Function CekSAPPeriodCreatePrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.SAPPeriodCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            ClearData()
            sHelper.SetSession("SortColPeriod", "SAPNumber")
            sHelper.SetSession("SortDirectionPeriod", Sort.SortDirection.ASC)
            BindDatagrid(0)

        End If
        ' add security
        If Not CekSAPPeriodCreatePrivilege() Then
            btnSave.Enabled = False
            btnNew.Enabled = False
        End If
    End Sub
    Function ValidateInput() As Boolean
        If txtSAPNo.Text = String.Empty Then
            MessageBox.Show("Input No. SAP")
            Return False
        End If

        If txtEndConfirmHour.Text = String.Empty Then
            MessageBox.Show("Input Batas waktu akhir")
            Return False
        End If

        Dim Time As String = txtEndConfirmHour.Text
        If Time <> String.Empty Then
            If (Time.IndexOf(":") > 0) Then
                Dim arrTime As Array
                arrTime = Time.Split(":")

                If ((CType(arrTime(0), Integer) > 23) Or (CType(arrTime(1), Integer) > 60)) Then
                    MessageBox.Show("Input waktu salah")
                    Return False
                End If
            Else
                MessageBox.Show("Input waktu salah. Format hh:mm")
                Return False
            End If
        End If

        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim result As Boolean

        If ValidateInput() Then
            result = InsertSAPPeriod()
            BindDatagrid(dtgSAPPeriod.CurrentPageIndex)
            If result = True Then
                ClearData()
            End If
        End If

    End Sub
    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            arlSAPPeriod = New SAPPeriodFacade(User).RetrieveActiveList(indexPage + 1, dtgSAPPeriod.PageSize, totalRow, sHelper.GetSession("SortColPeriod"), sHelper.GetSession("SortDirectionPeriod"))
            dtgSAPPeriod.DataSource = arlSAPPeriod
            dtgSAPPeriod.VirtualItemCount = totalRow
            If totalRow <= dtgSAPPeriod.PageSize * (dtgSAPPeriod.CurrentPageIndex) Then
                If dtgSAPPeriod.CurrentPageIndex <> 0 Then
                    dtgSAPPeriod.CurrentPageIndex = dtgSAPPeriod.CurrentPageIndex - 1
                    BindDatagrid(dtgSAPPeriod.CurrentPageIndex)
                End If
            End If
            dtgSAPPeriod.DataBind()
        End If
    End Sub
    Private Sub ClearData()
        txtSAPNo.Text = String.Empty
        txtEndConfirmHour.Text = String.Empty
        icTglCreate.Value = DateTime.Now
        icTglCreate2.Value = DateTime.Now
        If CekSAPPeriodCreatePrivilege() Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
        dtgSAPPeriod.SelectedIndex = -1
        ViewState.Add("vsProcess", "Insert")
    End Sub
    Private Function InsertSAPPeriod() As Boolean
        Dim oSAPPeriodFacade As SAPPeriodFacade = New SAPPeriodFacade(User)
        Dim oSAPPeriod As SAPPeriod = New SAPPeriod
        Dim nResult As Integer

        Dim Idedit As Integer
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            Idedit = 0
        ElseIf CType(ViewState("vsProcess"), String) = "Edit" Then
            Idedit = CType(sHelper.GetSession("objedit"), SAPPeriod).ID
        End If
        Dim codeIsValid As Integer = New SAPPeriodFacade(User).ValidateCode(txtSAPNo.Text, Idedit)


        If codeIsValid > 0 Then
            MessageBox.Show(SR.DataIsExist("No SAP"))
            Return False
        Else
            oSAPPeriod.SAPNumber = txtSAPNo.Text.Trim.ToUpper
            oSAPPeriod.StartDate = icTglCreate.Value
            oSAPPeriod.EndDate = icTglCreate2.Value
            oSAPPeriod.EndConfirmedDate = icEndConfirmedDate.Value
            oSAPPeriod.EndConfirmHour = txtEndConfirmHour.Text

            If CType(ViewState("vsProcess"), String) = "Insert" Then
                'nResult = New SAPPeriodFacade(User).Insert(oSAPPeriod)

                Dim arlSAPRegister As New ArrayList
                Dim arlSalesmanHeader As New ArrayList

                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(SalesmanHeader), "RegisterStatus", MatchType.Exact, 1))
                crit.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.No, CShort(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif)))

                arlSalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(crit)

                For Each objSalesmanHeader As SalesmanHeader In arlSalesmanHeader
                    Dim objRegister As SAPRegister = New SAPRegister

                    objRegister.SAPPeriod = oSAPPeriod
                    objRegister.SalesmanHeader = objSalesmanHeader
                    arlSAPRegister.Add(objRegister)
                Next

                nResult = New SAPPeriodFacade(User).InsertSAPPeriod(oSAPPeriod, arlSAPRegister)

            ElseIf CType(ViewState("vsProcess"), String) = "Edit" Then
                oSAPPeriod.ID = Idedit
                nResult = New SAPPeriodFacade(User).Update(oSAPPeriod)
            End If

            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
                Return False
            Else
                MessageBox.Show(SR.SaveSuccess)
                Return True
            End If
        End If
    End Function
    Private Sub ViewSAPPeriod(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim oSAPPeriod As SAPPeriod = New SAPPeriodFacade(User).Retrieve(nID)
        txtSAPNo.Text = oSAPPeriod.SAPNumber
        icTglCreate.Value = oSAPPeriod.StartDate
        icTglCreate2.Value = oSAPPeriod.EndDate
        icEndConfirmedDate.Value = oSAPPeriod.EndConfirmedDate
        txtEndConfirmHour.Text = oSAPPeriod.EndConfirmHour
        If CekSAPPeriodCreatePrivilege() Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If

    End Sub

    Private Sub dtgSAPPeriod_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSAPPeriod.PageIndexChanged
        dtgSAPPeriod.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgSAPPeriod.CurrentPageIndex)
    End Sub

    Private Sub dtgSAPPeriod_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSAPPeriod.SortCommand
        If e.SortExpression = sHelper.GetSession("SortColPeriod") Then
            If sHelper.GetSession("SortDirectionPeriod") = Sort.SortDirection.ASC Then
                sHelper.SetSession("SortDirectionPeriod", Sort.SortDirection.DESC)
            Else
                sHelper.SetSession("SortDirectionPeriod", Sort.SortDirection.ASC)
            End If
        End If
        sHelper.SetSession("SortColPeriod", e.SortExpression)
        dtgSAPPeriod.SelectedIndex = -1
        'dtgSAPPeriod.CurrentPageIndex = 0
        BindDatagrid(dtgSAPPeriod.CurrentPageIndex)
    End Sub

    Private Sub dtgSAPPeriod_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgSAPPeriod.ItemCommand
        If e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            dtgSAPPeriod.SelectedIndex = e.Item.ItemIndex
            Dim objSAPPeriod As SAPPeriod = New SAPPeriodFacade(User).Retrieve(CInt(e.CommandArgument))
            sHelper.SetSession("objedit", objSAPPeriod)
            ViewSAPPeriod(e.Item.Cells(0).Text, True)

            
        ElseIf e.CommandName = "Delete" Then
            DeleteSAPPeriod(e.Item.Cells(0).Text)
            ClearData()

        ElseIf e.CommandName = "Synchronize" Then
            ViewState.Add("vsProcess", "Synchronize")
            Dim objSAPPeriod As SAPPeriod = New SAPPeriodFacade(User).Retrieve(CInt(e.CommandArgument))
            sHelper.SetSession("objSynch", objSAPPeriod)
            doSyncrhonize()
            ViewState.Add("vsProcess", "Insert")
        End If
    End Sub

    Private Sub DeleteSAPPeriod(ByVal nID As Integer)
        Dim oSAPPeriod As SAPPeriod = New SAPPeriodFacade(User).Retrieve(nID)
        Try
            Dim objSAPPeriodFacade As SAPPeriodFacade = New SAPPeriodFacade(User)
            objSAPPeriodFacade.DeleteFromDB(oSAPPeriod)
        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
        End Try
        BindDatagrid(dtgSAPPeriod.CurrentPageIndex)
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        ClearData()
    End Sub

    Private Sub dtgSAPPeriod_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSAPPeriod.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            If Not (arlSAPPeriod Is Nothing) Then
                objSAPPeriod = arlSAPPeriod(e.Item.ItemIndex)

                Dim _lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                _lblNo.Text = e.Item.ItemIndex + 1 + (dtgSAPPeriod.CurrentPageIndex * dtgSAPPeriod.PageSize)

                If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                    CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                End If

                Dim _lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                _lbtnEdit.CommandArgument = objSAPPeriod.ID

                Dim _lbtnSynch As LinkButton = CType(e.Item.FindControl("lbtnSynch"), LinkButton)

                ' Di Komen Oleh Ikhsan, 12 Juni 2008
                ' Untuk memunculkan tombol synch tanpa validasi tanggal

                'Dim currentDate As Date = New Date().Now.Date
                'If currentDate > objSAPPeriod.EndConfirmedDate.Date Then
                '    If Not IsNothing(_lbtnSynch) Then
                '        _lbtnSynch.Visible = False
                '    End If
                'Else
                '    If Not IsNothing(_lbtnSynch) Then
                        _lbtnSynch.Visible = True
                        _lbtnSynch.CommandArgument = objSAPPeriod.ID
                '    End If
                'End If
            End If
        End If
    End Sub

    'Private Sub btnSyncSales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'Dim result As Boolean

    '    ''If ValidateInput() Then
    '    ''    result = InsertSAPPeriod()
    '    ''    BindDatagrid(dtgSAPPeriod.CurrentPageIndex)
    '    ''    If result = True Then
    '    ''        ClearData()
    '    ''    End If
    '    ''End If
    '    'If CType(ViewState("vsProcess"), String) = "Synchonize" Then
    '    '    Dim objSAPPeriod As SAPPeriod = New SAPPeriod
    '    '    objSAPPeriod = New SAPPeriodFacade(User).Retrieve(CType(sHelper.GetSession("objedit"), SAPPeriod).ID)
    '    '    If objSAPPeriod.ID > 0 Then
    '    '        ''---Start Get ArrayList SAPRegister Based SAP Period----
    '    '        Dim arrlstNewSAPRegister As ArrayList = New ArrayList
    '    '        Dim arrlstSAPRegister As ArrayList = New ArrayList
    '    '        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPRegister), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    '        crit.opAnd(New Criteria(GetType(SAPRegister), "SAPPeriod.ID", MatchType.Exact, CType(sHelper.GetSession("objedit"), SAPPeriod).ID))
    '    '        arrlstSAPRegister = New SAPRegisterFacade(User).Retrieve(crit)
    '    '        ''---End Get ArrayList SAPRegister Based SAP Period----

    '    '        For Each item As SAPRegister In arrlstSAPRegister
    '    '            item.RowStatus = CShort(DBRowStatus.Deleted)
    '    '        Next


    '    '        ''---Start Get ArrayList SalesmanHeader Registered and Aktif----
    '    '        Dim arrlstSalesmanHeader As ArrayList = New ArrayList
    '    '        Dim critSH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    '        critSH.opAnd(New Criteria(GetType(SalesmanHeader), "RegisterStatus", MatchType.Exact, 1))
    '    '        critSH.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.No, CShort(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif)))
    '    '        arrlstSalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(critSH)
    '    '        ''---End Get ArrayList SAPRegister Based SAP Period----

    '    '        For Each objSalesmanHeader As SalesmanHeader In arrlstSalesmanHeader
    '    '            Dim isExist As Boolean = False
    '    '            For Each objSAPReg As SAPRegister In arrlstSAPRegister
    '    '                If objSalesmanHeader.ID = objSAPReg.SalesmanHeader.ID Then
    '    '                    objSAPReg.RowStatus = CShort(DBRowStatus.Active)
    '    '                    isExist = True
    '    '                    Exit For
    '    '                End If
    '    '                If Not isExist Then
    '    '                    Dim objNewSAPRegister As SAPRegister = New SAPRegister
    '    '                    objNewSAPRegister.SAPPeriod = objSAPPeriod
    '    '                    objNewSAPRegister.SalesmanHeader = objSalesmanHeader

    '    '                    arrlstNewSAPRegister.Add(objNewSAPRegister)
    '    '                End If
    '    '            Next
    '    '        Next

    '    '        result = New SAPRegisterFacade(User).InsertUpdate(arrlstNewSAPRegister, arrlstSAPRegister)
    '    '        If result = True Then
    '    '            ClearData()
    '    '        End If
    '    '    End If
    '    'End If
    'End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    MessageBox.Show("sadas")
    'End Sub

    Private Sub doSyncrhonize()
        Dim IdSynch As Integer = 0
        If CType(ViewState("vsProcess"), String) = "Synchronize" Then
            Dim oSAPPeriod As SAPPeriod = New SAPPeriod

            IdSynch = CType(sHelper.GetSession("objSynch"), SAPPeriod).ID

            oSAPPeriod = New SAPPeriodFacade(User).Retrieve(IdSynch)

            If oSAPPeriod.ID > 0 Then
                Dim arlSAPRegister As New ArrayList
                Dim arlSalesmanHeader As New ArrayList

                'Dim LastEmpDate As DateTime = oSAPPeriod.EndConfirmedDate.AddMonths(-3)

                ' dikomen oleh Ikhsan, 12 Juni 2008
                ' untuk melakukan testing thd filter data berdasarkan SAPPeriod

                'Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'crit.opAnd(New Criteria(GetType(SalesmanHeader), "RegisterStatus", MatchType.Exact, 1))
                'crit.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.No, CShort(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif)))

                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'crit.opAnd(New Criteria(GetType(SalesmanHeader), "RegisterStatus", MatchType.Exact, 1))
                crit.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.InSet, "(2,3)"))
                crit.opAnd(New Criteria(GetType(SalesmanHeader), "HireDate", MatchType.LesserOrEqual, oSAPPeriod.EndConfirmedDate.AddMonths(-3)))

                arlSalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(crit)

                For Each objSalesmanHeader As SalesmanHeader In arlSalesmanHeader

                    Dim objRegister As SAPRegister = New SAPRegister

                    objRegister.SAPPeriod = oSAPPeriod
                    objRegister.SalesmanHeader = objSalesmanHeader

                    ' di tambahkan oleh Ikhsan, 12 Juni 2008
                    ' untuk melakukan validasi apakah Salesman tidak aktif sebelum masa EndConfirmedDate

                    ' Dim SalesmanName As String = objRegister.SalesmanHeader.Name.ToString

                    If objRegister.SalesmanHeader.Status = 3 Then
                        If objRegister.SalesmanHeader.ResignDate > objRegister.SAPPeriod.EndConfirmedDate Then
                            arlSAPRegister.Add(objRegister)
                        End If
                    Else
                        arlSAPRegister.Add(objRegister)
                    End If

                Next

                IdSynch = New SAPRegisterFacade(User).SynchronizeSapRegister(arlSAPRegister)
                'If IdSynch < 0 Then
                '    MessageBox()
                'End If

            End If

        End If






        'nResult = New SAPPeriodFacade(User).InsertSAPPeriod(oSAPPeriod, arlSAPRegister)

    End Sub
End Class
