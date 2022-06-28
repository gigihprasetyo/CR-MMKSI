#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Service
Imports System.Security.Principal
Imports System.Text
Imports KTB.DNet.SAP
Imports System.Text.RegularExpressions 'validate email
#End Region

Namespace KTB.DNet.UI.Helper
    Public Class MSPHelper
        Inherits System.Web.UI.Page

        Private _sessHelper As New SessionHelper
        Private _strSessSearch As String = "CRITERIAS"
        Private _strSessMSPRegistrationHistoryID As String = "MSPRegistrationHistoryID"
        Private _strSessStatusInput As String = "StatusInput"
        Private objLoginDealer As Dealer
        Private objUserInfo As UserInfo
        Private objMSPRegistration As MSPRegistration
        Private objMSPRegistrationHistory As MSPRegistrationHistory
        Private objMSPCustomer As MSPCustomer
        Dim crt As CriteriaComposite
        Dim arr As ArrayList
        Dim sorts As SortCollection
        Dim strMsg As String = String.Empty
        Dim strMSPMasterID As String = String.Empty

        Public Function ValidateChassisNumber(ByVal chassisNumber As String, Optional ByVal joinDate As DateTime = Nothing) As String
            If joinDate = Nothing Then
                joinDate = DateTime.Now
            End If
            ' chassis number yg teregistrasi tidak dapat dibuat lagi
            Dim str As String = String.Empty
            Dim mspRegistrationHistoryID As Integer = _sessHelper.GetSession(_strSessMSPRegistrationHistoryID)
            If IsNothing(mspRegistrationHistoryID) Then
                mspRegistrationHistoryID = 0
            End If
            objMSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(mspRegistrationHistoryID)

            crt = New CriteriaComposite(New Criteria(GetType(MSPRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisNumber))
            crt.opAnd(New Criteria(GetType(MSPRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If Not IsNothing(objMSPRegistrationHistory) Then
                crt.opAnd(New Criteria(GetType(MSPRegistration), "ID", MatchType.No, objMSPRegistrationHistory.MSPRegistration.ID))
            End If

            arr = New MSPRegistrationFacade(User).Retrieve(crt)
            If arr.Count > 0 Then
                str = "\n" + "Chassis number " + chassisNumber + " sudah terdaftar sebelumnya."
            End If

            ' cek chassis number yang terkait dengan msp master dan memiliki tanggal pkt
            If str = String.Empty Then
                Dim dtSet As DataSet = New MSPRegistrationFacade(User).RetrieveSp("EXEC sp_MSP_GetMSPMasterList '" + chassisNumber + "','" & joinDate.ToString("yyyy-MM-dd") & "'")
                Dim dtTbl As DataTable = dtSet.Tables(0)
                If dtTbl.Rows.Count > 0 Then
                    If (Convert.ToDateTime(dtTbl.Rows(0)("PKTDate")) = CType("1753-01-01 00:00:00.000", DateTime)) Then
                        str += "\n" & "Chassis Number belum memiliki tanggal Open Faktur. Silahkan melakukan Open Faktur kendaraan"
                    End If
                Else
                    str += "\n" & "Tidak ada MSP Master yang terhubung dengan Chassis Number " & chassisNumber & "."
                End If
            End If

            Return str
        End Function

        Public Function GetDateShort(ByVal str As String) As Date
            Dim dt As Date 'YYYYMMdd

            Try
                dt = New Date(Integer.Parse(str.Substring(0, 4)), Integer.Parse(str.Substring(4, 2)), Integer.Parse(str.Substring(6, 2)))
            Catch ex As Exception
                dt = DateSerial(1900, 1, 1)
            End Try
            Return dt
        End Function

        Public Function GetDateLong(ByVal str As String) As DateTime
            Dim dt As Date 'YYYYMMddHHmmss

            Try
                dt = New Date(Integer.Parse(str.Substring(0, 4)), Integer.Parse(str.Substring(4, 2)), Integer.Parse(str.Substring(6, 2)), Integer.Parse(str.Substring(8, 2)), Integer.Parse(str.Substring(10, 2)), Integer.Parse(str.Substring(12, 2)))
            Catch ex As Exception
                dt = DateSerial(1900, 1, 1)
            End Try
            Return dt
        End Function

        Public Function CheckConnection(ByVal Username As String, ByVal Pass As String) As Boolean
            Dim bool As Boolean = False

            Dim sapConStr As String = KTB.DNet.Lib.WebConfig.GetValue("SAPConnectionStrMSP")
            Dim oSAPDnet As SAPDNet
            oSAPDnet = New SAPDNet(sapConStr, Username, Pass)
            bool = oSAPDnet.CheckConnection()

            Return bool
        End Function

        Public Function TransfersMSPRegistrationtoSAP(al As ArrayList, ByVal Username As String, ByVal Pass As String) As String
            Dim strMessage As String = String.Empty

            Dim sapConStr As String = KTB.DNet.Lib.WebConfig.GetValue("SAPConnectionStrMSP")
            Dim oSAPDnet As SAPDNet
            Dim SONumber As String = String.Empty, Msg As String = String.Empty
            Dim aErrors As New ArrayList
            Dim int As Integer = 0

            Try
                oSAPDnet = New SAPDNet(sapConStr, Username, Pass)
                For i As Integer = 0 To al.Count - 1
                    objMSPRegistration = CType(al(i), MSPRegistration)
                    ' load msp reg history untuk ditransfer ke sap
                    If objMSPRegistration.MSPRegistrationHistorys.Count > 0 Then

                        For Each item As MSPRegistrationHistory In objMSPRegistration.MSPRegistrationHistorys
                            If item.Status = EnumStatusMSP.Status.Proses Or item.Status = EnumStatusMSP.Status.Selesai Then
                                Continue For
                            End If
                            SONumber = String.Empty
                            Msg = String.Empty
                            Dim oldStatusHistory As Integer = -1

                            oSAPDnet.SendMSPRegistrationViaRFC(item, item.Sequence, SONumber, Msg)
                            If Msg <> String.Empty Then
                                aErrors.Add("Error Transfer Registrasi MSP : Nomor rangka " & objMSPRegistration.ChassisMaster.ChassisNumber & ", tipe pengajuan " & CType(item.RequestType, EnumStatusMSP.StatusType).ToString & " & tipe MSP " & item.MSPMaster.MSPType.Description & ". " & Msg)
                            Else
                                aErrors.Add("Sukses Transfer Registrasi MSP : " & objMSPRegistration.ChassisMaster.ChassisNumber & ", tipe pengajuan " & CType(item.RequestType, EnumStatusMSP.StatusType).ToString & " & tipe MSP " & item.MSPMaster.MSPType.Description & ".")

                                ' set old status to history status
                                oldStatusHistory = item.Status
                                ' update MSPRegistrationHitory status to Proses if Paid
                                item.Status = CInt(EnumStatusMSP.Status.Proses)
                                '  If al.Count = 1 Then
                                If item.BenefitMasterHeaderID > 0 Then
                                    ' if status request promo than set status = selesai
                                    item.Status = CInt(EnumStatusMSP.Status.Selesai)
                                End If
                                'End If
                                item.DebitChargeNo = SONumber
                                If New MSPRegistrationHistoryFacade(User).Update(item) = -1 Then
                                    int += 1
                                    aErrors.Add("Sukses Transfer Registrasi MSP dan Gagal Update Status menjadi Proses : " & objMSPRegistration.ChassisMaster.ChassisNumber & ", tipe pengajuan " & CType(item.RequestType, EnumStatusMSP.StatusType).ToString & " & tipe MSP " & item.MSPMaster.MSPType.Description & ".")
                                Else
                                    ' add to history status
                                    Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                                    objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.MSP_Registration), item.ID, oldStatusHistory, item.Status)
                                End If
                            End If

                        Next
                    End If
                Next

                If aErrors.Count > 0 Then
                    Msg = String.Empty
                    For Each erm As String In aErrors
                        strMessage += "\n" & erm
                    Next
                End If
            Catch ex As Exception
                strMessage = "\n" & "Transfer Gagal. " & ex.Message
            End Try

            Return strMessage
        End Function

        Public Function EmailAddressCheck(ByVal emailAddress As String) As Boolean
            Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]" & _
            "*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
            Dim emailAddressMatch As Match = Regex.Match(emailAddress, pattern)
            If emailAddressMatch.Success Then
                EmailAddressCheck = True

            Else
                EmailAddressCheck = False

            End If
        End Function

        Public Function ValidateUpgradeMSP(ByVal oldObjMSPRegHistory As MSPRegistrationHistory, ByVal newObjMSPRegHIstory As MSPRegistrationHistory) As String
            Dim str As String = String.Empty

            If oldObjMSPRegHistory.MSPMaster.MSPType.Sequence > newObjMSPRegHIstory.MSPMaster.MSPType.Sequence Then
                str += "\n" & "Tipe MSP Baru harus lebih dari Tipe MSP Lama"
            ElseIf oldObjMSPRegHistory.MSPMaster.MSPType.Sequence = newObjMSPRegHIstory.MSPMaster.MSPType.Sequence And oldObjMSPRegHistory.MSPMaster.Duration > newObjMSPRegHIstory.MSPMaster.Duration Then
                str += "\n" & "Durasi Tipe MSP Baru harus lebih dari Tipe MSP Lama"
            ElseIf oldObjMSPRegHistory.MSPMaster.MSPType.Sequence = newObjMSPRegHIstory.MSPMaster.MSPType.Sequence And oldObjMSPRegHistory.MSPMaster.Duration = newObjMSPRegHIstory.MSPMaster.Duration Then
                str += "\n" & "Tipe dan Durasi MSP Baru tidak boleh sama"
            ElseIf oldObjMSPRegHistory.MSPMaster.MSPType.Sequence < newObjMSPRegHIstory.MSPMaster.MSPType.Sequence And oldObjMSPRegHistory.MSPMaster.Duration > newObjMSPRegHIstory.MSPMaster.Duration Then
                str += "\n" & "Durasi MSP Baru tidak boleh kurang dari Durasi MSP Lama"
            End If

            Return str
        End Function

        Public Function CheckStatusMSPIFPMKIND0(ByVal chassisNumber As String, ByVal PMKindID As Integer, ByRef objPMheader As PMHeader, ByVal inputKM As Integer, ByVal serviceDate As DateTime) As String
            ' untuk cek status MSP based on chassisnumber dan PMKindID yang terregister sebagai MSP
            Dim str As String = String.Empty
            Dim dtSet As DataSet = New PMHeaderFacade(User).RetrieveSp("EXEC sp_MSP_GetMSPStatus " & chassisNumber & "," & PMKindID)

            Dim dtTbl As DataTable = dtSet.Tables(0)
            Dim strMSPStatus As String = String.Empty
            str = "PM"
            If dtTbl.Rows.Count > 0 Then

                'IsTrfPaymentID
                If (Not IsDBNull(dtTbl.Rows(0)("MSPRegHistoryID")) AndAlso CInt(dtTbl.Rows(0)("MSPRegHistoryID")) > 0) AndAlso _
                    (Not IsNothing(dtTbl.Rows(0)("MSPRegHistoryID")) AndAlso CInt(dtTbl.Rows(0)("MSPRegHistoryID")) > 0) AndAlso _
                         (Not IsDBNull(dtTbl.Rows(0)("IsTrfPaymentID"))) AndAlso _
                    (Not IsNothing(dtTbl.Rows(0)("IsTrfPaymentID")) AndAlso CInt(dtTbl.Rows(0)("IsTrfPaymentID"))) AndAlso _
                    dtTbl.Rows(0)("MSPStatus") <> "MSP EXPIRED" Then

                    str = "No Rangka " & chassisNumber & "Terdeteksi MSP. Silahkan Input PMKind"

                End If
            End If





            Return str
        End Function


        Public Function CheckStatusMSP(ByVal chassisNumber As String, ByVal PMKindID As Integer, ByRef objPMheader As PMHeader, ByVal inputKM As Integer, ByVal serviceDate As DateTime) As String
            ' untuk cek status MSP based on chassisnumber dan PMKindID yang terregister sebagai MSP
            Dim str As String = String.Empty
            Dim dtSet As DataSet = New PMHeaderFacade(User).RetrieveSp("EXEC sp_MSP_GetMSPStatus " & chassisNumber & "," & PMKindID)
            If dtSet.Tables.Count > 0 Then
                Dim dtTbl As DataTable = dtSet.Tables(0)
                Dim strMSPStatus As String = String.Empty
                If dtTbl.Rows.Count > 0 Then
                    Dim _objMSPRegHistory As MSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(CInt(dtTbl.Rows(0)("MSPRegHistoryID")))
                    If dtTbl.Rows(0)("MSPStatus") = "Need Payment" Then
                        Dim objPMKIndVal As PMKind = New PMKindFacade(User).Retrieve(PMKindID)
                        Dim crtMspPM As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPDurationPMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crtMspPM.opAnd(New Criteria(GetType(MSPDurationPMKind), "Duration", MatchType.Exact, _objMSPRegHistory.MSPMaster.Duration))
                        crtMspPM.opAnd(New Criteria(GetType(MSPDurationPMKind), "PMKindCode", MatchType.Exact, objPMKIndVal.KindCode))
                        Dim arrMspPm As ArrayList = New MSPDurationPMKindFacade(User).Retrieve(crtMspPM)
                        If arrMspPm.Count > 0 Then
                            str = "No Rangka " & chassisNumber & " belum melakukan pembayaran MSP."
                        Else
                            str = "PM"
                        End If
                    Else
                        If dtTbl.Rows(0)("MSPStatus") <> "MSP EXPIRED" Then
                            ' validasi jika masih ada upgrade registrasi dengan status belum selesai
                            If Not IsNothing(_objMSPRegHistory) Then
                                For Each item As MSPRegistrationHistory In _objMSPRegHistory.MSPRegistration.MSPRegistrationHistorys
                                    If (item.Status <> EnumStatusMSP.Status.Selesai And item.RequestType = EnumStatusMSP.StatusType.Upgrade) Then
                                        str = "Status Upgade belum selesai pada registrasi MSP dengan No Rangka " & chassisNumber & "."
                                    End If
                                Next

                                If str = String.Empty Then
                                    Dim regDate As DateTime = CDate(dtTbl.Rows(0)("RegistrationDate"))
                                    If serviceDate <= regDate Then
                                        str = "Tanggal Service harus lebih dari tanggal pendaftaran MSP(" & CDate(dtTbl.Rows(0)("RegistrationDate")).ToString("yyyy/MM/dd") & ")."


                                    End If
                                End If
                            End If
                        End If

                        If str = String.Empty And dtTbl.Rows(0)("MSPStatus") <> "PM" Then
                            objPMheader = New PMHeader
                            objPMheader.Remarks = dtTbl.Rows(0)("MSPStatus")
                            objPMheader.MSPRegistrationHistoryID = CInt(dtTbl.Rows(0)("MSPRegHistoryID"))
                            If dtTbl.Rows(0)("MSPStatus") = "MSP EXPIRED" Then
                                objPMheader.IsValidMSP = False
                            End If
                        End If
                    End If
                Else
                    str = "PM"
                End If
            Else
                str = "PM"
            End If

            Return str
        End Function

    End Class

End Namespace
