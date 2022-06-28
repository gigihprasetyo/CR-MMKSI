#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
Imports Excel
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports System.Collections.Generic
Imports System.Linq


#End Region

Public Class FrmRecallService
    Inherits System.Web.UI.Page

#Region "variable"
    Private ReadOnly varSession As String = "sessFrmRecallService"
    Private sesHelper As New SessionHelper
    Dim arlUserGroup As New ArrayList
    Dim IsKTB As Boolean

    Dim _arlPODetail As ArrayList
#End Region

#Region "Function"
    Private Sub CheckPriv()
        Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        If IsNothing(objDealer) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Service - Field Fix Campaign - Service Campaign")
        End If

        If Not SecurityProvider.Authorize(Context.User, SR.Recall_InputService_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Service - Field Fix Campaign - Service Campaign")
        End If

        lblDealerCOde.Text = objDealer.DealerCode
        lblDealerName.Text = objDealer.DealerName
    End Sub


    Private Sub Upload()


        If (Not DataFile.PostedFile Is Nothing) AndAlso (DataFile.PostedFile.ContentLength > 0) Then

            Dim fileExt As String = Path.GetExtension(DataFile.PostedFile.FileName)
            If Not (fileExt.ToLower() = ".xls" OrElse fileExt.ToLower() = ".xlsx") Then
                MessageBox.Show("Hanya Menerima File Excell")
                Return
            End If

            dgRecallMaster.DataSource = New ArrayList
            dgRecallMaster.DataBind()


            Me.btnStore.Enabled = False


            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate

            Try
                Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName)
                Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & DateTime.Now.ToString("dd/MM/yyy HHmmss") & SrcFile

                imp = New SAPImpersonate(_user, _password, _webServer)

                If imp.Start() Then
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(DataFile.PostedFile.InputStream, targetFile)

                    Dim parser As IParser = New PriceParser  '-- Declare parser Price

                    '-- Parse data file and store result into list
                    Dim arList As ArrayList = CType(parser.ParseNoTransaction(targetFile, "User"), ArrayList)

                    Dim i As Integer = 0
                    Dim objReader As IExcelDataReader = Nothing
                    Dim ArrUpload As New ArrayList
                    Dim ArrUploadOK As New ArrayList
                    Dim isOK As Boolean = True

                    Using stream As FileStream = File.Open(targetFile, FileMode.Open, FileAccess.Read)

                        '   objReader = ExcelReaderFactory.CreateBinaryReader(stream)

                        If fileExt.ToLower.Contains("xlsx") Then
                            objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                        Else
                            objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                        End If

                        If (Not IsNothing(objReader)) Then
                            While objReader.Read()

                                If i >= 4 Then
                                    Dim ObjRCS As New RecallService
                                    Dim ObjCM As New ChassisMaster
                                    Dim ObjCMBB As New ChassisMasterBB

                                    'Chassis Number objReader.GetString(1).Trim()
                                    Dim StrCHassis As String = ""
                                    Dim strRegCallNo As String = ""
                                    ObjCM.ChassisNumber = ""

                                    Try
                                        Try
                                            StrCHassis = objReader.GetString(1).Trim()
                                        Catch exx As Exception
                                            Throw New Exception("No Rangka Tidak Valid")
                                        End Try

                                        Try
                                            strRegCallNo = objReader.GetString(4).Trim()
                                        Catch exx As Exception
                                            Throw New Exception("No Field Fix Campaign Tidak Valid")
                                        End Try

                                        ObjRCS.RecallRegNo = strRegCallNo

                                        'Check Exist In Recall Master
                                        Dim ObjFaca As RecallChassisMasterFacade = New RecallChassisMasterFacade(User)
                                        Dim ObjRCM As New RecallChassisMaster
                                        ObjRCM = ObjFaca.Retrieve(StrCHassis, strRegCallNo, isForService:=True)

                                        Dim ObjCFaca As ChassisMasterFacade = New ChassisMasterFacade(User)
                                        Dim ObjCBBFaca As ChassisMasterBBFacade = New ChassisMasterBBFacade(User)

                                        ObjCM = ObjCFaca.Retrieve(StrCHassis)
                                        ObjCM.ChassisNumber = StrCHassis
                                        ObjRCS.ChassisNumber = StrCHassis
                                        If ObjRCM.ID <= 0 Then
                                            Throw New Exception("No Rangka tidak terdaftar di Campaign Chassis Master")
                                        End If
                                        ObjRCS.RecallChassisMaster = ObjRCM
                                        ObjRCS.BuletinNo = ObjRCM.RecallCategory.BuletinDescription

                                        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")

                                        If ObjCM.ID <= 0 Then

                                            ObjCMBB = ObjCBBFaca.Retrieve(StrCHassis)

                                            If ObjCMBB.ID <= 0 Then
                                                Throw New Exception("No Rangka Tidak Terdaftar di Chassis Master")
                                            Else
                                                If ObjCMBB.Category.ProductCategory.Code <> companyCode Then
                                                    Throw New Exception("No Rangka Tidak Terdaftar di " + IIf(companyCode.ToLower().Equals("mmc"), "MMKSI", "MFTBC"))
                                                End If

                                                ObjRCS.ChassisBBID = ObjCMBB.ID
                                                ObjRCS.ChassisMasterBB = ObjCMBB
                                                ObjRCS.ChassisID = 0

                                            End If

                                        Else
                                            If ObjCM.Category.ProductCategory.Code <> companyCode Then
                                                Throw New Exception("No Rangka Tidak Terdaftar di " + IIf(companyCode.ToLower().Equals("mmc"), "MMKSI", "MFTBC"))
                                            End If

                                            ObjRCS.ChassisID = ObjCM.ID
                                            ObjRCS.ChassisMaster = ObjCM
                                            ObjRCS.ChassisBBID = 0

                                        End If

                                        Dim ObjSFaca As RecallServiceFacade = New RecallServiceFacade(User)

                                        Dim ObjRS As RecallService = ObjSFaca.RetrieveByRM(ObjRCM.ID)

                                        ObjRS.RecallRegNo = strRegCallNo
                                        If ObjRS.ID > 0 Then
                                            Throw New Exception("Campaign atas No Rangka ini telah diinput")
                                        End If

                                        For Each ObjdRS As RecallService In ArrUpload
                                            If ObjdRS.ChassisID > 0 Then
                                                If ObjdRS.ChassisMaster.ChassisNumber = StrCHassis AndAlso ObjdRS.RecallRegNo = strRegCallNo Then
                                                    Throw New Exception("Campaign atas No Rangka ini telah diinput")
                                                End If
                                            End If

                                            If ObjdRS.ChassisBBID > 0 Then
                                                If ObjdRS.ChassisMasterBB.ChassisNumber = StrCHassis AndAlso ObjdRS.RecallRegNo = strRegCallNo Then
                                                    Throw New Exception("Campaign atas No Rangka ini telah diinput")
                                                End If
                                            End If
                                        Next

                                    Catch ex As Exception
                                        ObjRCS.ErrorMessage = ex.Message
                                    Finally


                                    End Try

                                    'Mile Age 
                                    Dim MileAge As Integer = 0
                                    Try
                                        MileAge = CInt(objReader.GetString(2).Trim())

                                        If MileAge <= 0 Then
                                            Throw New Exception("Jarak tempuh tidak valid")
                                        End If
                                    Catch ex As Exception
                                        ObjRCS.ErrorMessage = ObjRCS.ErrorMessage & ", " & "Jarak tempuh tidak valid"
                                    Finally
                                        ObjRCS.MileAge = MileAge
                                    End Try



                                    'Service Date
                                    Dim ServiceDate As DateTime = New DateTime(1973, 1, 1)
                                    Try
                                        Dim StrDate As String = objReader.GetString(3).Trim()

                                        ServiceDate = New DateTime(CInt(StrDate.Substring(0, 4)), CInt(StrDate.Substring(4, 2)), CInt(StrDate.Substring(6, 2)))
                                        ObjRCS.ServiceDate = ServiceDate
                                        If (Year(ServiceDate) <= Date.Today.Year - 20) Then
                                            Throw New Exception("Tanggal Invalid")
                                        End If

                                        If ObjRCS.ServiceDate > Date.Today Then
                                            ObjRCS.ErrorMessage = "Tanggal Service tidak boleh lebih dari hari ini "
                                        End If

                                        If Not IsNothing(ObjRCS.RecallChassisMaster) Then
                                            If ObjRCS.ServiceDate < ObjRCS.RecallChassisMaster.RecallCategory.ValidStartDate Then
                                                ObjRCS.ErrorMessage = "Tanggal Service  lebih kecil dari Tanggal Validasi"
                                            End If
                                        End If

                                    Catch ex As Exception
                                        ObjRCS.ErrorMessage = ObjRCS.ErrorMessage & ", Tanggal Invalid"
                                    Finally
                                        ObjRCS.ServiceDate = ServiceDate
                                    End Try

                                    If ObjRCS.ErrorMessage <> "" Then
                                        isOK = False
                                    End If
                                    ArrUpload.Add(ObjRCS)

                                End If
                                i = i + 1

                            End While
                        End If
                    End Using

                    dgRecallMaster.DataSource = ArrUpload
                    dgRecallMaster.DataBind()
                    dgRecallMaster.Visible = True
                    dgRecallMaster.ShowFooter = False
                    sesHelper.SetSession(Me.varSession, ArrUpload)
                    If isOK = False Then
                        btnStore.Enabled = False
                    Else
                        btnStore.Enabled = True
                    End If
                End If


            Catch ex As Exception
                MessageBox.Show("Fail To Process")
            Finally

                imp.StopImpersonate()
                imp = Nothing
            End Try

        Else
            MessageBox.Show("Berkas Upload tidak boleh Kosong")
        End If

    End Sub

    Private Function CheckCHassis(ByVal StrCHassis As String, ByVal strRC As String) As RecallService
        Dim ObjRS As New RecallService
        Dim ObjCM As New ChassisMaster
        Dim objCMBB As New ChassisMasterBB
        Dim objCMBBFac As New ChassisMasterBBFacade(User)

        ObjCM.ChassisNumber = StrCHassis


        Try
            'Check Exist In Recall Master
            Dim ObjFaca As RecallChassisMasterFacade = New RecallChassisMasterFacade(User)
            Dim ObjRCM As New RecallChassisMaster

            ObjRS.RecallRegNo = strRC

            'Check Exist In CHassis Master
            Dim ObjCFaca As ChassisMasterFacade = New ChassisMasterFacade(User)
            Dim companyCode As String = KTB.DNET.Lib.WebConfig.GetValue("CompanyCode")
            ObjCM = ObjCFaca.RetrieveByCompany(StrCHassis, companyCode)
            ObjCM.ChassisNumber = StrCHassis
            If ObjCM.ID <= 0 Then
                'ObjRS.ErrorMessage = "Chassis Tidak Terdaftar di Chassis Master"

                objCMBB = objCMBBFac.RetrieveByCompany(StrCHassis, companyCode)
                If objCMBB.ID <= 0 Then
                    ObjRS.ErrorMessage = "Chassis Tidak Terdaftar di Chassis Master atau Chassis Master BB"
                    Throw New Exception("")
                End If

                ObjRS.ChassisMaster = Nothing
                ObjRS.ChassisMasterBB = objCMBB
                ObjRCM = ObjFaca.Retrieve(StrCHassis, strRC, isForService:=True)
                ObjRS.ChassisBBID = objCMBB.ID
                If ObjRCM.ID <= 0 Then
                    ObjRS.ErrorMessage = "Chassis Tidak Terdaftar di Campaign Master"
                    Throw New Exception("")
                End If

                ObjRS.RecallChassisMaster = ObjRCM
                ObjRS.BuletinNo = ObjRCM.RecallCategory.BuletinDescription

                'Throw New Exception("")
            Else
                ObjRS.ChassisMaster = ObjCM
                ObjRS.ChassisMasterBB = Nothing
                ObjRCM = ObjFaca.Retrieve(StrCHassis, strRC, isForService:=True)
                ObjRS.ChassisID = ObjCM.ID
                If ObjRCM.ID <= 0 Then
                    ObjRS.ErrorMessage = "Chassis Tidak Terdaftar di Campaign Master"
                    Throw New Exception("")
                End If

                ObjRS.RecallChassisMaster = ObjRCM
                ObjRS.BuletinNo = ObjRCM.RecallCategory.BuletinDescription
            End If


            Dim ObjSFaca As RecallServiceFacade = New RecallServiceFacade(User)

            Dim ObjRST As RecallService = ObjSFaca.RetrieveByRM(ObjRCM.ID)
            ObjCM.ChassisNumber = StrCHassis

            If ObjRST.ID > 0 Then
                ObjRS.ErrorMessage = "Campaign atas No Rangka ini telah diinput"
                Throw New Exception("")
            End If
        Catch ex As Exception

        Finally
            ObjRS.ChassisNumber = StrCHassis
        End Try



        Return ObjRS

    End Function

    Private Sub AddData(ByRef _ObjArray As ArrayList, ByVal e As DataGridCommandEventArgs)
        Dim txtFChassisNumber As TextBox = CType(e.Item.FindControl("txtFChassisNumber"), TextBox)
        Dim txtFMileAge As TextBox = CType(e.Item.FindControl("txtFMileAge"), TextBox)
        Dim txtFServiceDate As TextBox = e.Item.FindControl("txtFServiceDate")
        Dim fddRecallRegNo As DropDownList = e.Item.FindControl("fddRecallRegNo")


        If fddRecallRegNo.Items.Count = 0 OrElse fddRecallRegNo.SelectedValue.ToString() = "0" Then
            dgRecallMaster.DataSource = _arlPODetail
            dgRecallMaster.DataBind()
            MessageBox.Show("Silahkan isi No Field Fix No")
            Return

        End If
        If txtFServiceDate.Text.Trim() = "" OrElse txtFChassisNumber.Text.Trim() = "" OrElse txtFMileAge.Text.Trim = "" Then
            dgRecallMaster.DataSource = _arlPODetail
            dgRecallMaster.DataBind()
            MessageBox.Show("Data tidak lengkap")
            Return
        End If

        Dim ObjRS As New RecallService
        ObjRS = CheckCHassis(txtFChassisNumber.Text.Trim(), fddRecallRegNo.SelectedValue.ToString())

        For Each ObjdRS As RecallService In _ObjArray
            If ObjdRS.ChassisMaster IsNot Nothing Then
                If ObjdRS.ChassisNumber = txtFChassisNumber.Text.Trim() AndAlso ObjdRS.RecallRegNo = fddRecallRegNo.SelectedValue.ToString() Then
                    ObjRS.ErrorMessage = ObjRS.ErrorMessage + ", Campaign atas No Rangka ini telah diinput"
                    ' Throw New Exception("Recall atas No Rangka ini telah diinput")
                End If
            Else

                If ObjdRS.ChassisMasterBB IsNot Nothing Then
                    If ObjdRS.ChassisNumber = txtFChassisNumber.Text.Trim() AndAlso ObjdRS.RecallRegNo = fddRecallRegNo.SelectedValue.ToString() Then
                        ObjRS.ErrorMessage = ObjRS.ErrorMessage + ", Campaign atas No Rangka ini telah diinput"
                        ' Throw New Exception("Recall atas No Rangka ini telah diinput")
                    End If
                End If

            End If

           
        Next


        Try
            ObjRS.MileAge = CInt(txtFMileAge.Text.Trim())
            If ObjRS.MileAge <= 0 Then
                Throw New Exception("")
            End If
        Catch ex As Exception
            ObjRS.ErrorMessage = ObjRS.ErrorMessage + ", Format Jarak tempuh salah"
        End Try

        Try
            Dim strDate As String = txtFServiceDate.Text
            ObjRS.ServiceDate = New DateTime(CInt(strDate.Substring(4, 4)), CInt(strDate.Substring(2, 2)), CInt(strDate.Substring(0, 2)))

            If (Year(ObjRS.ServiceDate) <= Date.Today.Year - 20) Then
                ObjRS.ErrorMessage = ObjRS.ErrorMessage + "Tanggal Invalid"
            End If

            If ObjRS.ServiceDate > Date.Today Then
                ObjRS.ErrorMessage = ObjRS.ErrorMessage + ";Tanggal Service tidak boleh lebih dari hari ini "
            End If

            If Not IsNothing(ObjRS.RecallChassisMaster) Then
                If ObjRS.ServiceDate < ObjRS.RecallChassisMaster.RecallCategory.ValidStartDate Then
                    ObjRS.ErrorMessage = ObjRS.ErrorMessage + ";Tanggal Service  lebih kecil dari Tanggal Validasi"
                End If
            End If

        Catch ex As Exception
            ObjRS.ErrorMessage = ObjRS.ErrorMessage + ",Format Tanggal Service salah"
        End Try

        Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        ObjRS.Dealer = objDealer

        _arlPODetail.Add(ObjRS)

        ViewState("txtFChassisNumber") = txtFChassisNumber.Text

        dgRecallMaster.DataSource = _arlPODetail
        dgRecallMaster.DataBind()

        If ObjRS.ErrorMessage = "" Then
            btnStore.Enabled = True
        Else
            btnStore.Enabled = False
        End If

    End Sub

    Private Sub EditData(ByRef ObjRS As RecallService, ByVal e As DataGridCommandEventArgs)
        Dim txtFChassisNumber As TextBox = CType(e.Item.FindControl("txtEChassisNumber"), TextBox)
        Dim txtFMileAge As TextBox = CType(e.Item.FindControl("txtEMileAge"), TextBox)
        Dim txtFServiceDate As TextBox = e.Item.FindControl("txtEServiceDate")
        Dim eddRecallRegNo As DropDownList = e.Item.FindControl("eddRecallRegNo")

        If eddRecallRegNo.Items.Count = 0 OrElse eddRecallRegNo.SelectedValue.ToString() = "0" Then
            MessageBox.Show("Silahkan isi No Field Fix No")
            Return

        End If

        ObjRS = CheckCHassis(txtFChassisNumber.Text.Trim(), eddRecallRegNo.SelectedValue.ToString())

        Try
            ObjRS.MileAge = CInt(txtFMileAge.Text.Trim())
            If ObjRS.MileAge <= 0 Then
                Throw New Exception("")
            End If
        Catch ex As Exception
            ObjRS.ErrorMessage = "Format Jarak tempuh salah"
        End Try


        Try
            Dim strDate As String = txtFServiceDate.Text
            ObjRS.ServiceDate = New DateTime(CInt(strDate.Substring(4, 4)), CInt(strDate.Substring(2, 2)), CInt(strDate.Substring(0, 2)))

            If ObjRS.ServiceDate > Date.Today Then
                ObjRS.ErrorMessage = "Tanggal Service tidak boleh lebih dari hari ini "
            End If

            If Not IsNothing(ObjRS.RecallChassisMaster) Then
                If ObjRS.ServiceDate < ObjRS.RecallChassisMaster.RecallCategory.ValidStartDate Then
                    ObjRS.ErrorMessage = "Tanggal Service  lebih kecil dari Tanggal Validasi"
                End If
            End If
        Catch ex As Exception
            ObjRS.ErrorMessage = "Format Tanggal Service salah"
        End Try

        Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        ObjRS.Dealer = objDealer





    End Sub

    Private Sub SetDtgPODetailItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)

        Dim txtEServiceDate As TextBox = e.Item.FindControl("txtEServiceDate")
        txtEServiceDate.Text = CType(e.Item.DataItem, RecallService).ServiceDate.ToString("ddMMyyyy")


        Dim eddRecallRegNo As DropDownList = e.Item.FindControl("eddRecallRegNo")
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        ' criterias.opAnd(New Criteria(GetType(RecallCategory), "ValidStartDate", MatchType.LesserOrEqual, DateTime.Now.ToString("yyyy/MM/dd")))
        Dim arr As New ArrayList
        arr = New RecallCategoryFacade(User).Retrieve(criterias)
        eddRecallRegNo.DataSource = arr
        eddRecallRegNo.DataValueField = "RecallRegNo"
        eddRecallRegNo.DataTextField = "RecallRegNo"

        eddRecallRegNo.DataBind()
        eddRecallRegNo.Items.Insert(0, New ListItem("Select Field Fix No", "0"))

        eddRecallRegNo.SelectedValue = CType(e.Item.DataItem, RecallService).RecallRegNo


    End Sub

    Private Sub SetDtgPODetailItemFooter(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)

        Dim fddRecallRegNo As DropDownList = e.Item.FindControl("fddRecallRegNo")
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '   criterias.opAnd(New Criteria(GetType(RecallCategory), "ValidStartDate", MatchType.LesserOrEqual, DateTime.Now.ToString("yyyy/MM/dd")))
        Dim arr As New ArrayList
        arr = New RecallCategoryFacade(User).Retrieve(criterias)
        fddRecallRegNo.DataSource = arr
        fddRecallRegNo.DataValueField = "RecallRegNo"
        fddRecallRegNo.DataTextField = "RecallRegNo"

        fddRecallRegNo.DataBind()
        fddRecallRegNo.Items.Insert(0, New ListItem("Select Field Fix No", "0"))
    End Sub

    Private Sub SetDtgPODetailItemFooterOnChange(ByRef e As System.Web.UI.WebControls.DataGridCommandEventArgs)

        Try
            Dim fddRecallRegNo As DropDownList = e.Item.FindControl("fddRecallRegNo")
            Dim txtFChassisNumber As TextBox = e.Item.FindControl("txtFChassisNumber")
            If txtFChassisNumber.Text.ToString().Trim() <> "" Then

                Dim strSQL As String = ""
                strSQL = String.Format("SELECT RecallCategoryID FROM [RecallChassisMaster] WHERE RowStatus=0 and ChassisNo ='{0}'", txtFChassisNumber.Text.ToString().Trim())
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(RecallCategory), "ID", MatchType.InSet, "( " & strSQL & ")"))
                Dim arr As New ArrayList
                arr = New RecallCategoryFacade(User).Retrieve(criterias)
                fddRecallRegNo.DataSource = arr
                fddRecallRegNo.DataValueField = "RecallRegNo"
                fddRecallRegNo.DataTextField = "RecallRegNo"

                fddRecallRegNo.DataBind()
                fddRecallRegNo.Items.Insert(0, New ListItem("Select Field Fix No", "0"))

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub SetDtgRecallItemFooterOnChange(ByRef e As System.Web.UI.WebControls.DataGridCommandEventArgs)

        Try
            Dim fddRecallRegNo As DropDownList = e.Item.FindControl("fddRecallRegNo")
            Dim TlblBuletingNo As Label = e.Item.FindControl("TlblBuletingNo")
            Dim FlblBuletingNo As Label = e.Item.FindControl("FlblBuletingNo")
            If fddRecallRegNo.SelectedValue.ToString().Trim() <> "" Then
               
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(RecallCategory), "RecallRegNo", MatchType.Exact, fddRecallRegNo.SelectedValue.ToString().Trim()))
                Dim arr As New ArrayList
                arr = New RecallCategoryFacade(User).Retrieve(criterias)

                If Not IsNothing(arr) AndAlso arr.Count > 0 Then
                    Try
                        TlblBuletingNo.Text = (CType(arr(0), RecallCategory).BuletinDescription)
                    Catch ex As Exception

                    End Try
                    Try
                        FlblBuletingNo.Text = (CType(arr(0), RecallCategory).BuletinDescription)
                    Catch ex As Exception

                    End Try


                End If

            Else
                Try
                    TlblBuletingNo.Text = ""
                Catch ex As Exception

                End Try
                Try
                    FlblBuletingNo.Text = ""
                Catch ex As Exception

                End Try
            End If

        Catch ex As Exception

        End Try

    End Sub


    Private Sub SetDtgPODetailItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", _
                New CommonFunction().PreventDoubleClickAtGrid(CType(e.Item.FindControl("lbtnDelete"), LinkButton), "Yakin Data ini akan dihapus?"))
        End If
        Dim lblChassisNumber As Label = CType(e.Item.FindControl("lblChassisNumber"), Label)
        lblChassisNumber.Text = CType(e.Item.DataItem, RecallService).ChassisNumber

    End Sub

    Private Sub SaveDate(ByVal _objarray As ArrayList)
        Dim IsDataOK As Boolean = True

        Try
            'Check Error
            For Each ObjRS As RecallService In _objarray
                If ObjRS.ErrorMessage <> "" Then
                    IsDataOK = False
                    Exit For
                End If
            Next
            Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)

            If Not IsDataOK Then
                Throw New Exception("Masih ada data bermasalah")
            End If

            Dim ObjFRS As RecallServiceFacade

            For Each ObjRS As RecallService In _objarray
                ObjFRS = New RecallServiceFacade(User)

                If ObjFRS.RetrieveByRM(ObjRS.RecallChassisMaster.ID).ID > 0 Then

                Else

                    ObjRS.Dealer = objDealer
                    'If Not IsNothing(ObjRS.ChassisMaster) Then
                    '    ObjRS.ChassisID = ObjRS.ChassisMaster.ID
                    '    ObjRS.ChassisBBID = 0
                    'Else
                    '    ObjRS.ChassisID = 0
                    '    ObjRS.ChassisBBID = ObjRS.ChassisMasterBB.ID
                    'End If
                    Dim OK = ObjFRS.Insert(ObjRS)
                End If
            Next
            btnStore.Enabled = False
            MessageBox.Show(SR.SaveSuccess)

            Dim ArrUpload As New ArrayList
            sesHelper.SetSession(varSession, ArrUpload)
            dgRecallMaster.EditItemIndex = -1
            dgRecallMaster.ShowFooter = True
            dgRecallMaster.DataSource = New ArrayList
            dgRecallMaster.DataBind()
            btnStore.Enabled = True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        CheckPriv()
        If Not IsPostBack() Then
            dgRecallMaster.DataSource = New ArrayList
            dgRecallMaster.DataBind()
            Me.sesHelper.SetSession(varSession, New ArrayList)
        End If
    End Sub

    Protected Sub LnkTemplate_Click(sender As Object, e As EventArgs) Handles LnkTemplate.Click
        Dim strName As String = "Templates-UploadDataServiceCampaign.xlsx"
        Response.Redirect("../downloadlocal.aspx?file=DataFile\Recall\" & strName)
    End Sub

    Private Sub dgRecallMaster_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgRecallMaster.ItemCommand
        _arlPODetail = CType(sesHelper.GetSession(varSession), ArrayList)
        Select Case e.CommandName.ToString().ToLower()
            Case "add" 'Insert New item to datagrid
                AddData(_arlPODetail, e)
                sesHelper.SetSession(varSession, _arlPODetail)

            Case "edit" 'Edit mode activated
                '  dgRecallMaster.ShowFooter = False
                dgRecallMaster.ShowFooter = False
                dgRecallMaster.EditItemIndex = e.Item.ItemIndex

                dgRecallMaster.DataSource = _arlPODetail
                dgRecallMaster.DataBind()


            Case "delete" 'Delete this datagrid item 
                _arlPODetail.RemoveAt(e.Item.ItemIndex)
                dgRecallMaster.DataSource = _arlPODetail
                dgRecallMaster.DataBind()
                sesHelper.SetSession(varSession, _arlPODetail)
                If _arlPODetail.Count > 0 Then
                    btnStore.Enabled = True
                    For Each ObjRS As RecallService In _arlPODetail
                        If ObjRS.ErrorMessage <> "" Then
                            btnStore.Enabled = False
                            Exit For
                        End If
                    Next

                Else
                    btnStore.Enabled = False
                End If
            Case "save" 'Update this datagrid item 
                Dim ObjRS As RecallService
                ObjRS = CType(_arlPODetail(e.Item.ItemIndex), RecallService)
                EditData(ObjRS, e)
                _arlPODetail(e.Item.ItemIndex) = ObjRS
                dgRecallMaster.EditItemIndex = -1
                dgRecallMaster.ShowFooter = True

                dgRecallMaster.DataSource = _arlPODetail
                dgRecallMaster.DataBind()
                sesHelper.SetSession(varSession, _arlPODetail)

                If ObjRS.ErrorMessage = "" Then
                    btnStore.Enabled = True
                Else
                    btnStore.Enabled = False
                End If

            Case "cancel" 'Cancel Update this datagrid item 
                dgRecallMaster.EditItemIndex = -1
                dgRecallMaster.ShowFooter = True
                dgRecallMaster.DataSource = _arlPODetail
                dgRecallMaster.DataBind()

            Case "RebindRecallCategory".ToLower()
                SetDtgPODetailItemFooterOnChange(e)

            Case "RebindRegNo".ToLower()
                SetDtgRecallItemFooterOnChange(e)


        End Select


    End Sub

    Private Sub dtgPODetail_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgRecallMaster.ItemDataBound
        If Not e.Item.ItemIndex = -1 Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1
        End If
        If e.Item.ItemType = ListItemType.Footer Then
            SetDtgPODetailItemFooter(e)
        End If
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.EditItem Then
            SetDtgPODetailItem(e)
        End If
        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            SetDtgPODetailItemEdit(e)
        End If
    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim ArrUpload As New ArrayList
        sesHelper.SetSession(varSession, ArrUpload)
        dgRecallMaster.EditItemIndex = -1
        dgRecallMaster.ShowFooter = True
        dgRecallMaster.DataSource = New ArrayList
        dgRecallMaster.DataBind()
        btnStore.Enabled = True

    End Sub

    Protected Sub btnStore_Click(sender As Object, e As EventArgs) Handles btnStore.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim ArrUpload As New ArrayList
        ArrUpload = CType(sesHelper.GetSession(varSession), ArrayList)

        If Not IsNothing(ArrUpload) AndAlso ArrUpload.Count > 0 Then
            SaveDate(ArrUpload)
        Else
            MessageBox.Show("Data masih kosong")
        End If
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If Not Page.IsValid Then  '-- Postback validation
            Exit Sub
        End If

        Upload()
    End Sub


End Class