Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Lib
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SAP
Imports KTB.DNet.Utility

Public Class FrmSAPGradeWeight
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgProspecting As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgPKT As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgSupervisor As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgSales As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Deklarasi"
    Private criterias As CriteriaComposite
#End Region

#Region "Custom Method"
    Private Sub CreateCriteria(ByVal tipeEnum As Integer)
        criterias = New CriteriaComposite(New Criteria(GetType(SAPGradeWeight), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SAPGradeWeight), "Type", MatchType.Exact, tipeEnum))
    End Sub

    Private Function CekFieldAdd(ByVal e As DataGridCommandEventArgs, ByVal tipeEnum As Integer) As Integer
        Dim nResult As Integer = 1
        Dim kode As TextBox
        Dim desc As TextBox
        Dim bobot As TextBox
        Dim strKeterangan As String = ""
        Select Case tipeEnum
            Case EnumSAPGradeWeight.SAPGradeType.SalesWeight
                kode = CType(e.Item.FindControl("txtFooterKodeSales"), TextBox)
                desc = CType(e.Item.FindControl("txtFooterDeskripsiSales"), TextBox)
                bobot = CType(e.Item.FindControl("txtFooterBobotSales"), TextBox)
                strKeterangan = "Penjualan"
            Case EnumSAPGradeWeight.SAPGradeType.ProspectingWeight
                kode = CType(e.Item.FindControl("txtFooterKodeProspect"), TextBox)
                desc = CType(e.Item.FindControl("txtFooterDeskripsiProspect"), TextBox)
                bobot = CType(e.Item.FindControl("txtFooterBobotProspect"), TextBox)
                strKeterangan = "Prospect"
            Case EnumSAPGradeWeight.SAPGradeType.PKTWeight
                kode = CType(e.Item.FindControl("txtFooterKodePKT"), TextBox)
                desc = CType(e.Item.FindControl("txtFooterDeskripsiPKT"), TextBox)
                bobot = CType(e.Item.FindControl("txtFooterBobotPKT"), TextBox)
                strKeterangan = "PKT"
            Case EnumSAPGradeWeight.SAPGradeType.SupervisorWeight
                kode = CType(e.Item.FindControl("txtFooterKodeSPV"), TextBox)
                desc = CType(e.Item.FindControl("txtFooterDeskripsiSPV"), TextBox)
                bobot = CType(e.Item.FindControl("txtFooterBobotSPV"), TextBox)
                strKeterangan = "Supervisor"
        End Select

        If kode.Text = String.Empty And desc.Text = String.Empty And bobot.Text = String.Empty Then
            MessageBox.Show("Silahkan masukkan field Kode, " & strKeterangan & " dan Bobot")
            nResult = -1
        ElseIf kode.Text <> String.Empty And desc.Text = String.Empty And bobot.Text = String.Empty Then
            MessageBox.Show("Silahkan masukkan field " & strKeterangan & " dan Bobot")
            nResult = -1
        ElseIf kode.Text <> String.Empty And desc.Text <> String.Empty And bobot.Text = String.Empty Then
            MessageBox.Show("Silahkan masukkan field Bobot")
            nResult = -1
        End If

        Return nResult
    End Function

    Private Function Counter(ByVal arlList As ArrayList) As Integer
        Dim CounterAll As Integer = 0
        For Each item As SAPGradeWeight In arlList
            CounterAll += item.Bobot
        Next

        Return CounterAll
    End Function

    Private Function InsertData(ByVal objDomain As SAPGradeWeight, ByVal tipeEnum As Integer) As Integer
        Dim iResult As Integer = 1
        If (New SAPGradeWeightFacade(User).ValidateCode(objDomain.Code, tipeEnum) > 0) Then
            MessageBox.Show("Kode: " & objDomain.Code & " sudah pernah digunakan")
            iResult = -1
        Else
            If (New SAPGradeWeightFacade(User).Insert(objDomain) = 1) Then
                MessageBox.Show("Insert data berhasil")
                iResult = 1
            Else
                MessageBox.Show("Insert data gagal")
                iResult = -1
            End If
        End If

        Return iResult
    End Function

    'region penjualan
#Region "Penjualan"
    Private Sub BindSales()
        CreateCriteria(EnumSAPGradeWeight.SAPGradeType.SalesWeight)
        Dim arlSales As ArrayList = New SAPGradeWeightFacade(User).Retrieve(criterias)
        If arlSales.Count > 0 Then
            dtgSales.DataSource = arlSales
        Else
            dtgSales.DataSource = New ArrayList
        End If

        dtgSales.DataBind()
    End Sub
#End Region

    'region prospecting
#Region "Prospecting"
    Private Sub BindProspecting()
        CreateCriteria(EnumSAPGradeWeight.SAPGradeType.ProspectingWeight)
        Dim arlProspecting As ArrayList = New SAPGradeWeightFacade(User).Retrieve(criterias)
        If arlProspecting.Count > 0 Then
            dtgProspecting.DataSource = arlProspecting
        Else
            dtgProspecting.DataSource = New ArrayList
        End If

        dtgProspecting.DataBind()
    End Sub
#End Region

    'region PKT
#Region "PKT"
    Private Sub BindPKT()
        CreateCriteria(EnumSAPGradeWeight.SAPGradeType.PKTWeight)
        Dim arlPKT As ArrayList = New SAPGradeWeightFacade(User).Retrieve(criterias)
        If arlPKT.Count > 0 Then
            dtgPKT.DataSource = arlPKT
        Else
            dtgPKT.DataSource = New ArrayList
        End If

        dtgPKT.DataBind()
    End Sub
#End Region

    'region Supervisor
#Region "Supervisor"
    Private Sub BindSPV()
        CreateCriteria(EnumSAPGradeWeight.SAPGradeType.SupervisorWeight)
        Dim arlSPV As ArrayList = New SAPGradeWeightFacade(User).Retrieve(criterias)
        If arlSPV.Count > 0 Then
            dtgSupervisor.DataSource = arlSPV
        Else
            dtgSupervisor.DataSource = New ArrayList
        End If

        dtgSupervisor.DataBind()
    End Sub
#End Region


#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            BindSales()
            BindProspecting()
            BindPKT()
            BindSPV()
        End If
    End Sub

    Private Sub dtgSales_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgSales.ItemCommand
        If e.CommandName = "Add" Then
            If CekFieldAdd(e, EnumSAPGradeWeight.SAPGradeType.SalesWeight) <> -1 Then
                CreateCriteria(EnumSAPGradeWeight.SAPGradeType.SalesWeight)
                Dim arlSales As ArrayList = New SAPGradeWeightFacade(User).Retrieve(criterias)
                If Counter(arlSales) > 100 Then
                    MessageBox.Show("Bobot penilaian tidak bisa melebihi 100%")
                Else
                    Dim kode As TextBox = CType(e.Item.FindControl("txtFooterKodeSales"), TextBox)
                    Dim desc As TextBox = CType(e.Item.FindControl("txtFooterDeskripsiSales"), TextBox)
                    Dim bobot As TextBox = CType(e.Item.FindControl("txtFooterBobotSales"), TextBox)
                    Dim objSales As New SAPGradeWeight
                    objSales.Code = kode.Text.Trim
                    objSales.Type = EnumSAPGradeWeight.SAPGradeType.SalesWeight
                    objSales.Description = desc.Text.Trim
                    objSales.Bobot = CInt(bobot.Text.Trim)
                    Dim totTemp As Integer = Counter(arlSales) + objsales.Bobot
                    If Not totTemp > 100 Then
                        If InsertData(objSales, objSales.Type) = 1 Then
                            BindSales()
                        End If
                    Else
                        MessageBox.Show("Bobot penilaian tidak bisa melebihi 100%")
                    End If
                End If
            Else
                MessageBox.Show("Gagal memasukkan parameter Penjualan")
            End If
        ElseIf e.CommandName = "Delete" Then
            Dim lblKodeSales As Label = CType(e.Item.FindControl("lblKodeSales"), Label)
            Dim objSales As SAPGradeWeight = New SAPGradeWeightFacade(User).Retrieve(lblKodeSales.Text)
            If (New SAPGradeWeightFacade(User).DeleteDB(objsales) = 1) Then
                MessageBox.Show("Hapus data berhasil")
                BindSales()
            Else
                MessageBox.Show("Hapus data gagal")
            End If
        ElseIf e.CommandName = "Edit" Then
            dtgSales.EditItemIndex = e.Item.ItemIndex
            dtgSales.ShowFooter = False
            BindSales()
        ElseIf e.CommandName = "Save" Then
            Dim lblKodeSales As Label = CType(e.Item.FindControl("lblEditKodeSales"), Label)
            Dim desc As TextBox = CType(e.Item.FindControl("txtEditDeskripsiSales"), TextBox)
            Dim bobot As TextBox = CType(e.Item.FindControl("txtEditBobotSales"), TextBox)
            Dim countTemp As Integer = New SAPGradeWeightFacade(User).CountBobotEdit(EnumSAPGradeWeight.SAPGradeType.SalesWeight, lblkodesales.Text)
            If Not (countTemp > 100) Then
                Dim objSales As SAPGradeWeight = New SAPGradeWeightFacade(User).Retrieve(lblKodeSales.Text.Trim)
                objSales.Description = desc.Text
                objSales.Bobot = CInt(bobot.Text)

                Dim totalTemp As Integer = countTemp + objsales.Bobot
                If Not totalTemp > 100 Then
                    If (New SAPGradeWeightFacade(User).Update(objsales) = 1) Then
                        MessageBox.Show("Update berhasil dilakukan")
                        dtgSales.EditItemIndex = -1
                        dtgSales.ShowFooter = True
                        BindSales()
                    Else
                        MessageBox.Show("Update gagal dilakukan")
                    End If
                Else
                    MessageBox.Show("Bobot penilaian tidak bisa melebihi 100%")
                End If
            Else
                MessageBox.Show("Bobot penilaian tidak bisa melebihi 100%")
            End If
        ElseIf e.CommandName = "Cancel" Then
            If e.Item.ItemIndex <> -1 Then
                dtgSales.EditItemIndex = -1
                dtgSales.ShowFooter = True
                BindSales()
            End If
        End If
    End Sub

    Private Sub dtgProspecting_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgProspecting.ItemCommand
        If e.CommandName = "Add" Then
            If CekFieldAdd(e, EnumSAPGradeWeight.SAPGradeType.ProspectingWeight) <> -1 Then
                CreateCriteria(EnumSAPGradeWeight.SAPGradeType.ProspectingWeight)
                Dim arlProspect As ArrayList = New SAPGradeWeightFacade(User).Retrieve(criterias)
                If Counter(arlProspect) > 100 Then
                    MessageBox.Show("Bobot penilaian tidak bisa melebihi 100%")
                Else
                    Dim kode As TextBox = CType(e.Item.FindControl("txtFooterKodeProspect"), TextBox)
                    Dim desc As TextBox = CType(e.Item.FindControl("txtFooterDeskripsiProspect"), TextBox)
                    Dim bobot As TextBox = CType(e.Item.FindControl("txtFooterBobotProspect"), TextBox)
                    Dim objProspect As New SAPGradeWeight
                    objProspect.Code = kode.Text.Trim
                    objProspect.Type = EnumSAPGradeWeight.SAPGradeType.ProspectingWeight
                    objProspect.Description = desc.Text.Trim
                    objProspect.Bobot = CInt(bobot.Text.Trim)

                    Dim totTemp As Integer = Counter(arlProspect) + objprospect.Bobot
                    If Not totTemp > 100 Then
                        If InsertData(objProspect, objProspect.Type) = 1 Then
                            BindProspecting()
                        End If
                    Else
                        MessageBox.Show("Bobot penilaian tidak bisa melebihi 100%")
                    End If
                End If
            Else
                MessageBox.Show("Gagal memasukkan parameter Prospecting")
            End If
        ElseIf e.CommandName = "Delete" Then
            Dim lblKodeProspect As Label = CType(e.Item.FindControl("lblKodeProspect"), Label)
            Dim objProspect As SAPGradeWeight = New SAPGradeWeightFacade(User).Retrieve(lblKodeProspect.Text)
            If (New SAPGradeWeightFacade(User).DeleteDB(objProspect) = 1) Then
                MessageBox.Show("Hapus data berhasil")
                BindProspecting()
            Else
                MessageBox.Show("Hapus data gagal")
            End If
        ElseIf e.CommandName = "Edit" Then
            dtgProspecting.EditItemIndex = e.Item.ItemIndex
            dtgProspecting.ShowFooter = False
            BindProspecting()
        ElseIf e.CommandName = "Save" Then
            Dim lblKodeProspect As Label = CType(e.Item.FindControl("lblEditKodeProspect"), Label)
            Dim desc As TextBox = CType(e.Item.FindControl("txtEditDeskripsiProspect"), TextBox)
            Dim bobot As TextBox = CType(e.Item.FindControl("txtEditBobotProspect"), TextBox)
            Dim countTemp As Integer = New SAPGradeWeightFacade(User).CountBobotEdit(EnumSAPGradeWeight.SAPGradeType.ProspectingWeight, lblKodeProspect.Text)
            If Not countTemp > 100 Then
                Dim objProspect As SAPGradeWeight = New SAPGradeWeightFacade(User).Retrieve(lblKodeProspect.Text.Trim)
                objProspect.Description = desc.Text
                objProspect.Bobot = CInt(bobot.Text)

                Dim totalTemp As Integer = countTemp + objProspect.Bobot
                If Not totalTemp > 100 Then
                    If (New SAPGradeWeightFacade(User).Update(objProspect) = 1) Then
                        MessageBox.Show("Update berhasil dilakukan")
                        dtgProspecting.EditItemIndex = -1
                        dtgProspecting.ShowFooter = True
                        BindProspecting()
                    Else
                        MessageBox.Show("Update gagal dilakukan")
                    End If
                Else
                    MessageBox.Show("Bobot penilaian tidak bisa melebihi 100%")
                End If
            Else
                MessageBox.Show("Bobot penilaian tidak bisa melebihi 100%")
            End If
        ElseIf e.CommandName = "Cancel" Then
            If e.Item.ItemIndex <> -1 Then
                dtgProspecting.EditItemIndex = -1
                dtgProspecting.ShowFooter = True
                BindProspecting()
            End If
        End If
    End Sub

    Private Sub dtgPKT_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPKT.ItemCommand
        If e.CommandName = "Add" Then
            If CekFieldAdd(e, EnumSAPGradeWeight.SAPGradeType.PKTWeight) <> -1 Then
                CreateCriteria(EnumSAPGradeWeight.SAPGradeType.PKTWeight)
                Dim arlPKT As ArrayList = New SAPGradeWeightFacade(User).Retrieve(criterias)
                If Counter(arlPKT) > 100 Then
                    MessageBox.Show("Bobot penilaian tidak bisa melebihi 100%")
                Else
                    Dim kode As TextBox = CType(e.Item.FindControl("txtFooterKodePKT"), TextBox)
                    Dim desc As TextBox = CType(e.Item.FindControl("txtFooterDeskripsiPKT"), TextBox)
                    Dim bobot As TextBox = CType(e.Item.FindControl("txtFooterBobotPKT"), TextBox)
                    Dim objPKT As New SAPGradeWeight
                    objPKT.Code = kode.Text.Trim
                    objPKT.Type = EnumSAPGradeWeight.SAPGradeType.PKTWeight
                    objPKT.Description = desc.Text.Trim
                    objPKT.Bobot = CInt(bobot.Text.Trim)

                    Dim totTemp As Integer = Counter(arlPKT) + objpkt.Bobot
                    If Not totTemp > 100 Then
                        If InsertData(objPKT, objPKT.Type) = 1 Then
                            BindPKT()
                        End If
                    Else
                        MessageBox.Show("Bobot penilaian tidak bisa melebihi 100%")
                    End If
                End If
            Else
                MessageBox.Show("Gagal memasukkan parameter Prospecting")
            End If
        ElseIf e.CommandName = "Delete" Then
            Dim lblKodePKT As Label = CType(e.Item.FindControl("lblKodePKT"), Label)
            Dim objPKT As SAPGradeWeight = New SAPGradeWeightFacade(User).Retrieve(lblKodePKT.Text)
            If (New SAPGradeWeightFacade(User).DeleteDB(objPKT) = 1) Then
                MessageBox.Show("Hapus data berhasil")
                BindPKT()
            Else
                MessageBox.Show("Hapus data gagal")
            End If
        ElseIf e.CommandName = "Edit" Then
            dtgPKT.EditItemIndex = e.Item.ItemIndex
            dtgPKT.ShowFooter = False
            BindPKT()
        ElseIf e.CommandName = "Save" Then
            Dim lblKodePKT As Label = CType(e.Item.FindControl("lblEditKodePKT"), Label)
            Dim desc As TextBox = CType(e.Item.FindControl("txtEditDeskripsiPKT"), TextBox)
            Dim bobot As TextBox = CType(e.Item.FindControl("txtEditBobotPKT"), TextBox)
            Dim sumTemp As Integer = New SAPGradeWeightFacade(User).CountBobotEdit(EnumSAPGradeWeight.SAPGradeType.PKTWeight, lblKodePKT.Text)
            If sumTemp <= 100 Then
                Dim objPKT As SAPGradeWeight = New SAPGradeWeightFacade(User).Retrieve(lblKodePKT.Text.Trim)
                objPKT.Description = desc.Text
                objPKT.Bobot = CInt(bobot.Text)

                Dim totalTemp As Integer = sumTemp + objpkt.Bobot
                If Not totalTemp > 100 Then
                    If (New SAPGradeWeightFacade(User).Update(objPKT) = 1) Then
                        MessageBox.Show("Update berhasil dilakukan")
                        dtgPKT.EditItemIndex = -1
                        dtgPKT.ShowFooter = True
                        BindPKT()
                    Else
                        MessageBox.Show("Update gagal dilakukan")
                    End If
                Else
                    MessageBox.Show("Bobot penilaian tidak bisa melebihi 100%")
                End If
            Else
                MessageBox.Show("Bobot penilaian tidak bisa melebihi 100%")
            End If
        ElseIf e.CommandName = "Cancel" Then
            If e.Item.ItemIndex <> -1 Then
                dtgPKT.EditItemIndex = -1
                dtgPKT.ShowFooter = True
                BindPKT()
            End If
        End If
    End Sub

    Private Sub dtgSupervisor_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgSupervisor.ItemCommand
        If e.CommandName = "Add" Then
            If CekFieldAdd(e, EnumSAPGradeWeight.SAPGradeType.SupervisorWeight) <> -1 Then
                CreateCriteria(EnumSAPGradeWeight.SAPGradeType.SupervisorWeight)
                Dim arlSPV As ArrayList = New SAPGradeWeightFacade(User).Retrieve(criterias)
                If Counter(arlSPV) > 100 Then
                    MessageBox.Show("Bobot penilaian tidak bisa melebihi 100%")
                Else
                    Dim kode As TextBox = CType(e.Item.FindControl("txtFooterKodeSPV"), TextBox)
                    Dim desc As TextBox = CType(e.Item.FindControl("txtFooterDeskripsiSPV"), TextBox)
                    Dim bobot As TextBox = CType(e.Item.FindControl("txtFooterBobotSPV"), TextBox)
                    Dim objSPV As New SAPGradeWeight
                    objSPV.Code = kode.Text.Trim
                    objSPV.Type = EnumSAPGradeWeight.SAPGradeType.SupervisorWeight
                    objSPV.Description = desc.Text.Trim
                    objSPV.Bobot = CInt(bobot.Text.Trim)

                    Dim totTemp As Integer = Counter(arlSPV) + objspv.Bobot
                    If Not totTemp > 100 Then
                        If InsertData(objSPV, objSPV.Type) = 1 Then
                            BindSPV()
                        End If
                    Else
                        MessageBox.Show("Bobot penilaian tidak bisa melebihi 100%")
                    End If
                End If
            Else
                MessageBox.Show("Gagal memasukkan parameter Prospecting")
            End If
        ElseIf e.CommandName = "Delete" Then
            Dim lblKodeSPV As Label = CType(e.Item.FindControl("lblKodeSPV"), Label)
            Dim objSPV As SAPGradeWeight = New SAPGradeWeightFacade(User).Retrieve(lblKodeSPV.Text)
            If (New SAPGradeWeightFacade(User).DeleteDB(objSPV) = 1) Then
                MessageBox.Show("Hapus data berhasil")
                BindSPV()
            Else
                MessageBox.Show("Hapus data gagal")
            End If
        ElseIf e.CommandName = "Edit" Then
            dtgSupervisor.EditItemIndex = e.Item.ItemIndex
            dtgSupervisor.ShowFooter = False
            BindSPV()
        ElseIf e.CommandName = "Save" Then
            Dim lblKodeSPV As Label = CType(e.Item.FindControl("lblEditKodeSPV"), Label)
            Dim desc As TextBox = CType(e.Item.FindControl("txtEditDeskripsiSPV"), TextBox)
            Dim bobot As TextBox = CType(e.Item.FindControl("txtEditBobotSPV"), TextBox)
            Dim sumTemp As Integer = New SAPGradeWeightFacade(User).CountBobotEdit(EnumSAPGradeWeight.SAPGradeType.SupervisorWeight, lblKodeSPV.Text)
            If Not sumTemp > 100 Then
                Dim objSPV As SAPGradeWeight = New SAPGradeWeightFacade(User).Retrieve(lblKodeSPV.Text.Trim)
                objSPV.Description = desc.Text
                objSPV.Bobot = CInt(bobot.Text)

                Dim totalTemp As Integer = sumTemp + objspv.Bobot
                If Not totalTemp > 100 Then
                    If (New SAPGradeWeightFacade(User).Update(objSPV) = 1) Then
                        MessageBox.Show("Update berhasil dilakukan")
                        dtgSupervisor.EditItemIndex = -1
                        dtgSupervisor.ShowFooter = True
                        BindSPV()
                    Else
                        MessageBox.Show("Update gagal dilakukan")
                    End If
                Else
                    MessageBox.Show("Bobot penilaian tidak bisa melebihi 100%")
                End If
            Else
                MessageBox.Show("Bobot penilaian tidak bisa melebihi 100%")
            End If
        ElseIf e.CommandName = "Cancel" Then
            If e.Item.ItemIndex <> -1 Then
                dtgSupervisor.EditItemIndex = -1
                dtgSupervisor.ShowFooter = True
                BindSPV()
            End If
        End If
    End Sub
End Class
