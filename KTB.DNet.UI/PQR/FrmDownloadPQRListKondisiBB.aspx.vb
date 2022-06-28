#Region "Import Statement"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.UI.Helper
Imports System.IO
Imports System.Text

Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.Security

#End Region

Public Class FrmDownloadPQRListKondisiBB
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgListPQR As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim sHTrainee As SessionHelper = New SessionHelper

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetDownload()
    End Sub

    Private Sub BindDataGrid()
        Dim data As ArrayList = sHTrainee.GetSession("PQRDafterKondisi")
        If IsNothing(data) Then
            Response.Write("<script>alert('Tidak ada data');window.close();</script>")
            Return
        End If

        dgListPQR.DataSource = data
        dgListPQR.DataBind()

    End Sub

    Private Sub dtgTrainee_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)

        If e.Item.ItemIndex >= 0 Then

            Dim objPQRHeaderBB As PQRHeaderBB = CType(e.Item.DataItem, PQRHeaderBB)

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Select Case CType(objPQRHeaderBB.RowStatus, EnumPQR.PQRStatus)
                Case EnumPQR.PQRStatus.Baru
                    lblStatus.Text = EnumPQR.PQRStatus.Baru.ToString
                Case EnumPQR.PQRStatus.Batal
                    lblStatus.Text = EnumPQR.PQRStatus.Batal.ToString
                Case EnumPQR.PQRStatus.Proses
                    lblStatus.Text = EnumPQR.PQRStatus.Proses.ToString
                Case EnumPQR.PQRStatus.Rilis
                    lblStatus.Text = EnumPQR.PQRStatus.Rilis.ToString
                Case EnumPQR.PQRStatus.Selesai
                    lblStatus.Text = EnumPQR.PQRStatus.Selesai.ToString
                Case EnumPQR.PQRStatus.Validasi
                    lblStatus.Text = EnumPQR.PQRStatus.Validasi.ToString
            End Select

            'TotalBobot = TotalBobot + objPQRHeaderBB.Bobot

            Dim d As New DateTime(1753, 1, 1)
            Dim ts As TimeSpan
            ts = objPQRHeaderBB.IntervalProcess.Subtract(d)

            Dim lblInterval As Label = CType(e.Item.FindControl("lblInterval"), Label)
            lblInterval.Text = ts.Hours.ToString & " Jam " & ts.Minutes.ToString & " Menit"

            'TotalInterval = TotalInterval + ts.Ticks
        End If

    End Sub

    Private Sub SetDownload()
        Dim data As ArrayList = sHTrainee.GetSession("PQRDafterKondisi")
        If IsNothing(data) Then
            MessageBox.Show("Tidak ada data yang di download")
        Else
            DoDownload(data)
        End If
    End Sub

    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "DaftarKondisi" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls

        '-- Temp file must be a randomly named file!
        Dim TraineeData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(TraineeData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(TraineeData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteTraineeData(sw, data)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub WriteTraineeData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        Dim sProfileConfig As String = KTB.DNet.Lib.WebConfig.GetValue("DownloadPQRFormatProfile")
        Dim header As String

        If Not IsNothing(data) Then
            '-- Write column header
            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("PQR - Daftar Kondisi")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Tgl Pembuatan" & tab)
            itemLine.Append("Tgl Kerusakan" & tab)
            itemLine.Append("Tgl Delivery" & tab)
            itemLine.Append("Tgl Buka Faktur" & tab)
            itemLine.Append("Tahun Produksi" & tab)
            itemLine.Append("No. Rangka" & tab)
            itemLine.Append("No. Mesin" & tab)
            itemLine.Append("Penggunaan" & tab)
            itemLine.Append("Bentuk Body Sekarang" & tab)
            itemLine.Append("Muatan" & tab)
            itemLine.Append("Berat Muatan (Kg)" & tab)
            itemLine.Append("Perawatan" & tab)
            itemLine.Append("Frekwensi" & tab)
            itemLine.Append("Prioritas" & tab)
            itemLine.Append("Kota Operational" & tab)
            itemLine.Append("Kode Posisi" & tab)
            itemLine.Append("Part Name/No" & tab)
            itemLine.Append("Kecepatan" & tab)
            itemLine.Append("Subject" & tab)
            itemLine.Append("Gejala" & tab)
            itemLine.Append("Penyebab" & tab)
            itemLine.Append("Perbaikan" & tab)
            itemLine.Append("Catatan" & tab)
            itemLine.Append("Dealer" & tab)
            itemLine.Append("Odometer" & tab)
            itemLine.Append("No. PQR" & tab)
            itemLine.Append("No. PQR Ref" & tab)
            itemLine.Append("Tipe/Warna" & tab)
            itemLine.Append("Nama Pemilik" & tab)
            itemLine.Append("Area Oper. Jalan Tol (%)" & tab)
            itemLine.Append("Area Oper. Dalam Kota (%)" & tab)
            itemLine.Append("Area Oper. Off Road (%)" & tab)
            itemLine.Append("Area Oper. Pegunungan (%)" & tab)
            itemLine.Append("Kond. Jln. Asphalt (%)" & tab)
            itemLine.Append("Kond. Jln. Lumpur (%)" & tab)
            itemLine.Append("Kond. Jln. Semen (%)" & tab)
            itemLine.Append("Kond. Jln. Tanah (%)" & tab)
            itemLine.Append("Kode Kerusakan A" & tab)
            itemLine.Append("Kode Kerusakan B" & tab)
            itemLine.Append("Kode Kerusakan C" & tab)
            itemLine.Append("Penjelasan MMKSI" & tab)
            itemLine.Append("Bobot" & tab)
            itemLine.Append("Tgl. Selesai" & tab)
            itemLine.Append("Informasi Tambahan" & tab)
            sw.WriteLine(itemLine.ToString())

            Dim objPQRProfileBBFacade As PQRProfileBBFacade = New PQRProfileBBFacade(User)
            For Each oPQRHeaderBB As PQRHeaderBB In data
                Try
                    itemLine.Remove(0, itemLine.Length)
                    'New Format
                    itemLine.Append(oPQRHeaderBB.DocumentDate.ToString("dd/MM/yyyy") & tab) 'itemLine.Append("Tgl Pembuatan" & tab)
                    itemLine.Append(oPQRHeaderBB.PQRDate.ToString("dd/MM/yyyy") & tab) 'itemLine.Append("Tgl Kerusakan" & tab)
                    itemLine.Append(oPQRHeaderBB.ChassisMasterBB.DODate.ToString("dd/MM/yyyy") & tab) 'itemLine.Append("Tgl Delivery" & tab)
                    If oPQRHeaderBB.ChassisMasterBB.EndCustomer Is Nothing Then
                        itemLine.Append("" & tab) 'itemLine.Append("Tgl Buka Faktur" & tab)
                    Else
                        itemLine.Append(oPQRHeaderBB.ChassisMasterBB.EndCustomer.OpenFakturDate.ToString("dd/MM/yyyy") & tab) 'itemLine.Append("Tgl Buka Faktur" & tab)
                    End If
                    itemLine.Append(oPQRHeaderBB.ChassisMasterBB.ProductionYear.ToString() & tab) 'itemLine.Append("Tahun Produksi" & tab)
                    itemLine.Append(oPQRHeaderBB.ChassisMasterBB.ChassisNumber & tab) 'itemLine.Append("No. Rangka" & tab)
                    itemLine.Append(oPQRHeaderBB.ChassisMasterBB.EngineNumber & tab) 'itemLine.Append("No. Mesin" & tab)

                    itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 1, "PQR_GUNA") & tab) 'itemLine.Append("Penggunaan" & tab)
                    itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 1, "PQR_BODY") & tab) 'itemLine.Append("Bentuk Body Sekarang" & tab)
                    itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 1, "PQR_MUATAN") & tab) 'itemLine.Append("Muatan" & tab)
                    itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 1, "PQR_Beban") & tab) 'itemLine.Append("Berat Muatan (Kg)" & tab)
                    itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 1, "PQR_RAWAT") & tab) 'itemLine.Append("Perawatan" & tab)
                    itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 1, "PQR_FREKWENSI") & tab) 'itemLine.Append("Frekwensi" & tab)
                    itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 1, "PQR_PRIORITAS") & tab) 'itemLine.Append("Prioritas" & tab)
                    itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 1, "PQR_KOTA_OPERATIONAL") & tab) 'itemLine.Append("Kota Operational" & tab)

                    'itemLine.Append("" & tab) 'itemLine.Append("Kode Posisi" & tab)
                    If oPQRHeaderBB.PQRDamageCodeBBs.Count > 0 Then
                        Dim sKodePosisi As String = String.Empty
                        For Each it As PQRDamageCodeBB In oPQRHeaderBB.PQRDamageCodeBBs
                            sKodePosisi += it.DeskripsiKodePosisi.KodePosition & " - " & it.DeskripsiKodePosisi.Description & ""
                        Next
                        itemLine.Append(sKodePosisi & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    'itemLine.Append("" & tab) 'itemLine.Append("Part Name/No" & tab)
                    If oPQRHeaderBB.PQRPartsCodeBBs.Count > 0 Then
                        Dim sPart As String = String.Empty
                        For Each it As PQRPartsCodeBB In oPQRHeaderBB.PQRPartsCodeBBs
                            sPart += it.SparePartMaster.PartNumber & " - " & it.SparePartMaster.PartName & "<br>"
                        Next
                        itemLine.Append(sPart & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    itemLine.Append(oPQRHeaderBB.Velocity.ToString() & tab) 'itemLine.Append("Kecepatan" & tab)
                    itemLine.Append(oPQRHeaderBB.Subject & tab) 'itemLine.Append("Subject" & tab)
                    itemLine.Append(oPQRHeaderBB.Symptomps & tab) 'itemLine.Append("Gejala" & tab)
                    itemLine.Append(oPQRHeaderBB.Causes & tab)  'itemLine.Append("Penyebab" & tab)
                    itemLine.Append(oPQRHeaderBB.Results & tab) 'itemLine.Append("Perbaikan" & tab)
                    itemLine.Append(oPQRHeaderBB.Notes & tab) 'itemLine.Append("Catatan" & tab)
                    itemLine.Append(oPQRHeaderBB.Dealer.DealerCode & " - " & oPQRHeaderBB.Dealer.SearchTerm1 & tab) 'itemLine.Append("Dealer" & tab)
                    itemLine.Append(oPQRHeaderBB.OdoMeter.ToString("#,##0") & tab) 'itemLine.Append("Odometer" & tab)
                    itemLine.Append(oPQRHeaderBB.PQRNo & tab) 'itemLine.Append("No. PQR" & tab)
                    itemLine.Append(oPQRHeaderBB.RefPQRNo & tab) 'itemLine.Append("No. PQR Ref" & tab)
                    itemLine.Append(oPQRHeaderBB.ChassisMasterBB.VechileColor.MaterialNumber & " - " & oPQRHeaderBB.ChassisMasterBB.VechileColor.MaterialDescription & tab) 'itemLine.Append("Tipe/Warna" & tab)
                    'itemLine.Append("" & tab) 'itemLine.Append("Nama Pemilik" & tab)
                    If IsNothing(oPQRHeaderBB.ChassisMasterBB.EndCustomer) OrElse oPQRHeaderBB.ChassisMasterBB.EndCustomer.Customer Is Nothing Then
                        itemLine.Append("" & tab)
                    Else
                        itemLine.Append(oPQRHeaderBB.ChassisMasterBB.EndCustomer.Customer.Name1 & " - " & oPQRHeaderBB.ChassisMasterBB.EndCustomer.Customer.Alamat & tab)
                    End If
                    itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 2, "PQR_OP_TOL") & tab) 'itemLine.Append("Area Oper. Jalan Tol (%)" & tab)
                    itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 2, "PQR_OP_DKOTA") & tab) 'itemLine.Append("Area Oper. Dalam Kota (%)" & tab)
                    itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 2, "PQR_OP_OFRO") & tab) 'itemLine.Append("Area Oper. Off Road (%)" & tab)
                    itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 2, "PQR_OP_PGUNUNG") & tab) 'itemLine.Append("Area Oper. Pegunungan (%)" & tab)
                    itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 2, "PQR_KJ_ASP") & tab) 'itemLine.Append("Kond. Jln. Asphalt (%)" & tab)
                    itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 2, "PQR_KJ_LUMPUR") & tab) 'itemLine.Append("Kond. Jln. Lumpur (%)" & tab)
                    itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 2, "PQR_KJ_SEMEN") & tab) 'itemLine.Append("Kond. Jln. Semen (%)" & tab)
                    itemLine.Append(GetPQRProfileBBValue(oPQRHeaderBB.ID, 2, "PQR_KJ_SOIL") & tab) 'itemLine.Append("Kond. Jln. Tanah (%)" & tab)

                    itemLine.Append(oPQRHeaderBB.CodeA & tab) 'itemLine.Append("No. PQR" & tab)
                    itemLine.Append(oPQRHeaderBB.CodeB & tab) 'itemLine.Append("No. PQR" & tab)
                    itemLine.Append(oPQRHeaderBB.CodeC & tab) 'itemLine.Append("No. PQR" & tab)
                    itemLine.Append(oPQRHeaderBB.Solutions & tab)   'itemLine.Append("Penjelasan KTB" & tab)
                    itemLine.Append(oPQRHeaderBB.Bobot & tab)  'itemLine.Append("Bobot" & tab)
                    itemLine.Append(oPQRHeaderBB.FinishDate.ToString("dd/MM/yyyy") & tab) 'itemLine.Append("Tgl Selesai" & tab)
                    Dim strAddInfo As String = ""
                    For Each oPQRAI As PQRAdditionalInfoBB In oPQRHeaderBB.PQRAdditionalInfoBBs
                        strAddInfo &= IIf(strAddInfo.Trim = "", "", ";") & GetAdditionalInfoString(oPQRAI)
                    Next
                    itemLine.Append(strAddInfo & tab)
                    itemLine.Replace(Chr(13) & Chr(10), "")
                    sw.WriteLine(itemLine.ToString)
                Catch ex As Exception
                    Dim sError As String = ex.Message
                End Try
            Next
        End If
    End Sub

    Private Function GetAdditionalInfoString(ByRef oPQRAI As PQRAdditionalInfoBB) As String
        Dim str As String = ""
        Dim oDFac As DealerFacade = New DealerFacade(User)
        Dim oD As Dealer
        Dim UserName As String = ""

        If oPQRAI.LastUpdateBy.Trim.Length <= "000000".Length Then '"000000UserName"
        Else
            If oPQRAI.CreatedBy = oPQRAI.LastUpdateBy Then
            Else
                oPQRAI.CreatedBy = oPQRAI.LastUpdateBy 'temporary
                oPQRAI.CreatedTime = oPQRAI.LastUpdateTime
            End If
        End If
        oD = oDFac.Retrieve(CType(oPQRAI.CreatedBy.Substring(0, 6), Integer))
        UserName = oPQRAI.CreatedBy.Substring(6)
        str = oD.DealerCode & "." & UserName & "(" & oPQRAI.CreatedTime.ToString("ddMMMyy") & "):" & oPQRAI.Sender

        Return str
    End Function


    Private Function GetPQRProfileBBValue(ByVal PQRHeaderBBID As Integer, ByVal ProfileGroup As Integer, ByVal ProfileHeaderName As String) As String
        'Return ""
        'Exit Function
        Dim Sql As String = ""
        Dim oPQRPFac As PQRProfileBBFacade = New PQRProfileBBFacade(User)
        Dim cPQRP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRProfileBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlPQRP As New ArrayList
        Dim oPQRP As PQRProfileBB
        Dim Rsl As String = ""

        Sql &= " select distinct(pp.ID)" ' pfh.Code, pfh.Description, *  "
        Sql &= " from PQRHeaderBB ph "
        Sql &= " , PQRProfileBB pp"
        Sql &= " , ProfileGroup pg"
        Sql &= " , ProfileHeaderToGroup phg"
        Sql &= " , ProfileHeader pfh"
        Sql &= " where 1=1 "
        Sql &= " and ph.ID=" & PQRHeaderBBID.ToString
        Sql &= " and pp.PQRHeaderBBID=ph.ID"
        Sql &= " and pg.Code='pqr_prf" & IIf(ProfileGroup = 1, "", "_2") & "'"
        Sql &= " and pp.GroupID=pg.ID"
        Sql &= " and phg.ProfileGroupID=pg.ID"
        Sql &= " and phg.ProfileHeaderID=pfh.ID"
        Sql &= " and pp.ProfileHeaderID=pfh.ID"
        Sql &= " and pfh.Code='" & ProfileHeaderName & "'"

        cPQRP.opAnd(New Criteria(GetType(PQRProfileBB), "ID", MatchType.InSet, "(" & Sql & ")"))
        arlPQRP = oPQRPFac.Retrieve(cPQRP)
        If arlPQRP.Count > 0 Then
            oPQRP = CType(arlPQRP(0), PQRProfileBB)

            If ProfileHeaderName.Trim.ToUpper = "PQR_GUNA" OrElse ProfileHeaderName.Trim.ToUpper = "PQR_BODY" OrElse ProfileHeaderName.Trim.ToUpper = "PQR_RAWAT" OrElse ProfileHeaderName.Trim.ToUpper = "PQR_FREKWENSI" OrElse ProfileHeaderName.Trim.ToUpper = "PQR_PRIORITAS" Then
                Dim oPDFac As ProfileDetailFacade = New ProfileDetailFacade(User)
                Dim cPD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim aPD As New ArrayList
                Dim oPD As ProfileDetail

                cPD.opAnd(New Criteria(GetType(ProfileDetail), "ProfileHeader.ID", MatchType.Exact, oPQRP.ProfileHeader.ID))
                cPD.opAnd(New Criteria(GetType(ProfileDetail), "Code", MatchType.Exact, oPQRP.ProfileValue))
                aPD = oPDFac.Retrieve(cPD)
                If aPD.Count = 0 Then
                    Rsl = oPQRP.ProfileValue
                Else
                    oPD = CType(aPD(0), ProfileDetail)
                    Rsl = oPD.Description
                End If
            Else
                Rsl = oPQRP.ProfileValue
            End If
        Else
            Rsl = ""
        End If
        Return Rsl
    End Function

    Private Sub WriteTraineeDataOld(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        Dim sProfileConfig As String = KTB.DNet.Lib.WebConfig.GetValue("DownloadPQRFormatProfile")
        Dim header As String

        If Not IsNothing(data) Then
            '-- Write column header
            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("PQR - Daftar Kondisi")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)

            itemLine.Append("No PQR" & tab)
            itemLine.Append("Subject" & tab)
            itemLine.Append("No Rangka" & tab)
            itemLine.Append("No Mesin" & tab)
            itemLine.Append("Tgl Kerusakan" & tab)
            itemLine.Append("Odometer" & tab)

            'Profile data
            For Each s As String In sProfileConfig.Split(";")
                Dim _HeaderProfile As ProfileHeader = New KTB.DNet.BusinessFacade.Service.ProfileHeaderFacade(User).Retrieve(s)
                itemLine.Append(_HeaderProfile.Description & tab)
            Next

            itemLine.Append("Dealer." & tab)
            itemLine.Append("Tgl Pembuatan" & tab)
            itemLine.Append("Tgl Selesai" & tab)
            itemLine.Append("Penjelasan MMKSI" & tab)
            itemLine.Append("Bobot" & tab)
            itemLine.Append("K" & tab)
            itemLine.Append("P" & tab)
            itemLine.Append("Q" & tab)
            itemLine.Append("Code" & tab)
            itemLine.Append("Tgl Kerusakan QRS" & tab)
            itemLine.Append("Odometer QRS" & tab)
            itemLine.Append("Catatan QRS" & tab)
            itemLine.Append("Tgl Delivery Rangka QRS" & tab)
            itemLine.Append("Tgl Faktur Rangka QRS" & tab)
            itemLine.Append("No Mesin QRS" & tab)

            sw.WriteLine(itemLine.ToString())
            'lblJr.Text = 
            'lblSr.Text = GetPassDateByCategory(passClassCourse, "SENIOR")
            'lblMr.Text = GetPassDateByCategory(passClassCourse, "MASTER")
            For Each item As PQRHeaderBB In data
                itemLine.Remove(0, itemLine.Length)  '-- Empty line
                itemLine.Append(item.PQRNo & tab)
                itemLine.Append(item.Subject & tab)
                itemLine.Append(item.ChassisMasterBB.ChassisNumber & tab)
                itemLine.Append(item.ChassisMasterBB.EngineNumber & tab)

                Dim _pqrdate As String
                If item.PQRDate < New Date(1900, 1, 1) Then
                    _pqrdate = ""
                Else
                    _pqrdate = item.PQRDate.ToString("dd/MM/yyyy")
                End If
                itemLine.Append(_pqrdate & tab)
                itemLine.Append(item.OdoMeter & tab)

                For Each s As String In sProfileConfig.Split(";")
                    If item.PQRProfileBBs.Count > 0 Then

                        Dim isFound As Boolean = False
                        For Each _PQRProfileBB As PQRProfileBB In item.PQRProfileBBs
                            If _PQRProfileBB.ProfileHeader.Code = s Then
                                isFound = True
                                If _PQRProfileBB.ProfileValue = String.Empty OrElse _PQRProfileBB.ProfileValue Is Nothing Then
                                    itemLine.Append(" " & tab)
                                Else
                                    itemLine.Append(_PQRProfileBB.ProfileValue & tab)
                                End If
                                Exit For
                            End If
                        Next
                        If isFound = False Then
                            itemLine.Append(" " & tab)
                        End If
                    Else
                        itemLine.Append(" " & tab)
                    End If

                Next

                itemLine.Append(item.Dealer.DealerCode & " " & item.Dealer.DealerName & tab)

                Dim _documentdate As String
                If item.DocumentDate < New Date(1900, 1, 1) Then
                    _documentdate = ""
                Else
                    _documentdate = item.DocumentDate.ToString("dd/MM/yyyy")
                End If
                itemLine.Append(_documentdate & tab)

                Dim _finishdate As String
                If item.FinishDate < New Date(1900, 1, 1) Then
                    _finishdate = ""
                Else
                    _finishdate = item.FinishDate.ToString("dd/MM/yyyy")
                End If
                itemLine.Append(_finishdate & tab)
                itemLine.Append(item.Solutions.Replace(Chr(13) & Chr(10), " ") & tab)
                itemLine.Append(item.Bobot & tab)

                header = itemLine.ToString()

                If item.PQRPartsCodeBBs.Count > 0 Then
                    For Each Parts As PQRPartsCodeBB In item.PQRPartsCodeBBs
                        sw.WriteLine(header & Replace("K", "K", "") & tab & "P" & tab & Replace("Q", "Q", "") & tab & Parts.SparePartMaster.PartNumber & tab & " " & tab & " " & tab & " " & tab)
                    Next
                End If

                If item.PQRDamageCodeBBs.Count > 0 Then
                    For Each DamageCode As PQRDamageCodeBB In item.PQRDamageCodeBBs
                        sw.WriteLine(header & "K" & tab & Replace("P", "P", "") & tab & Replace("Q", "Q", "") & tab & DamageCode.DeskripsiKodePosisi.KodePosition & tab & " " & tab & " " & tab & " " & tab)
                    Next
                End If

                Dim _tglfaktur As String
                Dim _tglkerusakan As String
                Dim _dodate As String
                If item.PQRQRSBBs.Count > 0 Then
                    For Each QRS As PQRQRSBB In item.PQRQRSBBs
                        _tglfaktur = String.Empty
                        _tglkerusakan = String.Empty
                        _dodate = String.Empty

                        If QRS.ChassisMasterBB.EndCustomer Is Nothing Then
                            _tglfaktur = ""
                        Else
                            If QRS.ChassisMasterBB.EndCustomer.OpenFakturDate < New Date(1900, 1, 1) Then
                                _tglfaktur = ""
                            Else
                                _tglfaktur = QRS.ChassisMasterBB.EndCustomer.OpenFakturDate.ToString("dd/MM/yyyy")
                            End If
                        End If

                        _tglkerusakan = String.Empty
                        If QRS.TglKerusakan < New Date(1900, 1, 1) Then
                            _tglkerusakan = ""
                        Else
                            _tglkerusakan = QRS.TglKerusakan.ToString("dd/MM/yyyy")
                        End If

                        _dodate = String.Empty
                        If QRS.ChassisMasterBB.DODate < New Date(1900, 1, 1) Then
                            _dodate = ""
                        Else
                            _dodate = QRS.TglKerusakan.ToString("dd/MM/yyyy")
                        End If

                        sw.WriteLine(header & Replace("K", "K", "") & tab & Replace("P", "P", "") & tab & "Q" & tab & QRS.ChassisMasterBB.ChassisNumber & tab & _tglkerusakan & tab & QRS.Odometer.ToString("#,##0") & tab & QRS.Note & tab & _dodate & tab & _tglfaktur & tab & QRS.ChassisMasterBB.EngineNumber & tab)
                    Next
                End If

                If item.PQRDamageCodeBBs.Count = 0 And item.PQRPartsCodeBBs.Count = 0 And item.PQRQRSBBs.Count = 0 Then
                    sw.WriteLine(header & Replace("K", "K", "") & tab & Replace("P", "P", "") & tab & Replace("Q", "Q", "") & tab & " " & tab & " " & tab & " " & tab & " " & tab)
                End If
            Next
        End If
    End Sub

    Private Function GetStatus(ByVal obj As PQRHeaderBB) As String
        Dim result As String
        Select Case CType(obj.RowStatus, EnumPQR.PQRStatus)
            Case EnumPQR.PQRStatus.Baru
                result = EnumPQR.PQRStatus.Baru.ToString
            Case EnumPQR.PQRStatus.Batal
                result = EnumPQR.PQRStatus.Batal.ToString
            Case EnumPQR.PQRStatus.Proses
                result = EnumPQR.PQRStatus.Proses.ToString
            Case EnumPQR.PQRStatus.Rilis
                result = EnumPQR.PQRStatus.Rilis.ToString
            Case EnumPQR.PQRStatus.Selesai
                result = EnumPQR.PQRStatus.Selesai.ToString
            Case EnumPQR.PQRStatus.Validasi
                result = EnumPQR.PQRStatus.Validasi.ToString
        End Select
        Return result
    End Function
    Private Function GetIntervalTime(ByVal obj As PQRHeaderBB) As String
        Dim d As New DateTime(1753, 1, 1)
        Dim ts As TimeSpan
        Dim result As String
        ts = obj.IntervalProcess.Subtract(d)

        result = ts.Hours.ToString & " Jam " & ts.Minutes.ToString & " Menit"
        Return result
    End Function
End Class
