#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports System.Drawing.Color
Imports KTB.DNet.Utility.CommonFunction
Imports KTB.DNet.DataMapper
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq

#End Region

#Region ".NET Base Class Namespace Imports"

Imports System.IO
Imports System.Text

#End Region

Public Class FrmSPKMatchingMatch
    Inherits System.Web.UI.Page

    Private _isMatching As Boolean
    Private _validChassisNumber As Boolean
    Private _validSPKNumber As Boolean

    Private _chassisMaster As ChassisMaster
    Private _spkHeader As SPKHeader
    Private _spkDetail As SPKDetail

    Private objDealer As Dealer
    Dim dt As DateTime = DateTime.Now

#Region "Custom Method"

    Private Sub CheckUserPrivelege()
        If Not SecurityProvider.Authorize(Context.User, SR.SPKChassisMatchingMatch_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SPK Chassis Matching Match/Unmatch")
        End If

        btnMatch.Visible = SecurityProvider.Authorize(Context.User, SR.SPKChassisMatchingMatch_Privilege)
    End Sub
#End Region

#Region "Event Handler"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Dim mode As String = Request.QueryString("mode")
        If mode = "match" Then
            _isMatching = True
        Else
            _isMatching = False

            _validSPKNumber = True
            Dim objSessionHelper As New SessionHelper
            objSessionHelper.SetSession("_validSPKNumber_for_matching", _validSPKNumber)
        End If

        GetDataFromSession()

        If Not Page.IsPostBack Then
            _chassisMaster = Nothing
            _validChassisNumber = False

            SetInputAvailability(_isMatching)
            CheckUserPrivelege()
        End If

        'lblSPKNumber.Attributes("onClick") = "ShowSPKSelection();"
    End Sub

#End Region

    Private Sub GetDataFromSession()
        Dim objSessionHelper As New SessionHelper
        _chassisMaster = objSessionHelper.GetSession("_chassisMaster_for_matching")
        _spkHeader = objSessionHelper.GetSession("_spkHeader_for_matching")
        _spkDetail = objSessionHelper.GetSession("_spkDetail_for_matching")
        _validChassisNumber = objSessionHelper.GetSession("_validChassisNumber_for_matching")
        _validSPKNumber = objSessionHelper.GetSession("_validSPKNumber_for_matching")

    End Sub

    Protected Sub btnMatch_Click(sender As Object, e As EventArgs) Handles btnMatch.Click
        Dim result As Integer
        Try
            If _validChassisNumber And _validSPKNumber Then
                Dim spkChassis As New SPKChassis

                spkChassis.ChassisMaster = _chassisMaster
                spkChassis.SPKDetail = _spkDetail
                spkChassis.KeyNumber = txtKeyNo.Text.Trim()
                spkChassis.MatchingDate = matchingDate.Value
                'spkChassis.MatchingNumber = txtMatchNo.Text.Trim()
                spkChassis.ReferenceNumber = txtRefNo.Text.Trim()
                spkChassis.MatchingType = If(_isMatching, 1, 2)
                spkChassis.RowStatus = CType(DBRowStatus.Active, Short)

                Dim spkChassisFacade As SPKChassisFacade = New SPKChassisFacade(User)

                Dim criterias As New CriteriaComposite(New Criteria(GetType(SPKChassis), "ChassisMaster.ID", MatchType.Exact, _chassisMaster.ID))
                criterias.opAnd(New Criteria(GetType(SPKChassis), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim spkChassisList As ArrayList = spkChassisFacade.Retrieve(criterias, "ID", Sort.SortDirection.DESC)
                If spkChassisList.Count > 0 Then
                    Dim oldSpkChassis As SPKChassis = CType(spkChassisList(0), SPKChassis)
                    oldSpkChassis.RowStatus = DBRowStatus.Deleted
                    If Not _isMatching Then
                        spkChassis.ReferenceNumber = oldSpkChassis.MatchingNumber
                    End If
                    result = spkChassisFacade.Insert(spkChassis, oldSpkChassis)
                Else
                    result = spkChassisFacade.Insert(spkChassis)
                End If

                If result > -1 Then
                    spkChassis = spkChassisFacade.Retrieve(result)
                    If _isMatching Then
                        MessageBox.Show(String.Format("Matching SPK dan Chassis dengan matching number {0} telah berhasil dilakukan.", spkChassis.MatchingNumber))
                    Else
                        MessageBox.Show(String.Format("Unmatching SPK dan Chassis dengan matching number {0} telah berhasil dilakukan.", spkChassis.MatchingNumber))
                    End If
                Else
                    MessageBox.Show("Gagal menyimpan data.")
                End If

                If _isMatching Then
                    Dim objSessionHelper As New SessionHelper
                    Dim objRevisionFaktur As RevisionFaktur = CType(objSessionHelper.GetSession("_RevisionFaktur_for_matching"), RevisionFaktur)
                    If Not IsNothing(objRevisionFaktur) Then
                        Dim revFakFacade As RevisionFakturFacade = New RevisionFakturFacade(User)
                        revFakFacade.Update(objRevisionFaktur)
                    End If
                End If
            Else
                MessageBox.Show("Tidak dapat melanjutkan proses. Chassis Number atau SPK Number tidak valid.")
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal menyimpan data.")
        End Try

        If result <> -1 Then
            Server.Transfer("~/FinishUnit/FrmSPKMatching.aspx")
        End If
    End Sub

    Public Sub New()

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Server.Transfer("~/FinishUnit/FrmSPKMatching.aspx")
    End Sub

    Protected Sub btnCheckNoSPK_Click(sender As Object, e As EventArgs) Handles btnCheckNoSPK.Click
        If IsSPKNumberValid() Then
            SetButtonSubmitAvailability()
        End If
    End Sub

    Protected Sub btnCheckNoChassis_Click(sender As Object, e As EventArgs) Handles btnCheckNoChassis.Click

        If IsChassisNumberValid() Then
            lblDescription.Text = _chassisMaster.VechileColor.MaterialDescription
            SetButtonSubmitAvailability()
        End If

    End Sub

    Private Sub SetInputAvailability(isMatching As Boolean)
        If isMatching Then
            txtNoSPK.Enabled = True
            btnCheckNoSPK.Enabled = True
            btnCheckNoSPK.Visible = True
            txtKeyNo.Enabled = True
            btnMatch.Text = "Matching"
        Else
            txtNoSPK.Enabled = False
            btnCheckNoSPK.Enabled = False
            btnCheckNoSPK.Visible = False
            txtKeyNo.Enabled = False
            txtRefNo.Visible = False
            btnMatch.Text = "Unmatching"
        End If

        SetButtonSubmitAvailability()
    End Sub

    Private Sub SetButtonSubmitAvailability()
        If _validChassisNumber And _validSPKNumber Then
            btnMatch.Enabled = True
        Else
            btnMatch.Enabled = False
        End If
    End Sub

    Private Function IsChassisNumberValid() As Boolean

        If IsChassisMasterValid() Then
            If IsSPKChassisValid() Then
                If Not _isMatching Then
                    If Not IsValidFaktur() Then
                        Return False
                    End If
                End If

                _validChassisNumber = True

                Dim objSessionHelper As New SessionHelper
                objSessionHelper.SetSession("_validChassisNumber_for_matching", _validChassisNumber)

                SetButtonSubmitAvailability()
                Return True
            End If
        End If
        Return False
    End Function

    Private Function IsSPKNumberValid() As Boolean
        If IsSPKValid() Then
            If IsSPKDetailValid() Then
                If IsValidFaktur() Then
                    _validSPKNumber = True

                    Dim objSessionHelper As New SessionHelper
                    objSessionHelper.SetSession("_validSPKNumber_for_matching", _validSPKNumber)

                    SetButtonSubmitAvailability()

                    Return True
                End If
            End If
        End If
        Return False
    End Function

    Private Function IsSPKValid() As Boolean

        Try
            Dim spkNumber As String = txtNoSPK.Text.Trim()
            Dim objSessionHelper As New SessionHelper
            objSessionHelper.RemoveSession("_spkHeader_for_matching")

            Dim criterias As New CriteriaComposite(New Criteria(GetType(SPKHeader), "SPKNumber", MatchType.Exact, spkNumber))
            criterias.opAnd(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            objDealer = Session("DEALER")
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                criterias.opAnd(New Criteria(GetType(SPKHeader), "Dealer.DealerGroup.ID", MatchType.Exact, objDealer.DealerGroup.ID))
            End If

            Dim listOfSPK As ArrayList = New ArrayList
            listOfSPK = New SPKHeaderFacade(User).Retrieve(criterias)

            If Not (listOfSPK Is Nothing) And listOfSPK.Count > 0 Then
                _spkHeader = DirectCast(listOfSPK(0), SPKHeader)

                'Check Status != Batal
                If _spkHeader.Status = EnumStatusSPK.Status.Batal Then
                    MessageBox.Show("Tidak dapat melakukan matching untuk SPK dengan status Batal.")
                    Return False
                End If
                'Dim isValid As Boolean = IsValidSPKDate(_spkHeader)
                If IsValid Then
                    objSessionHelper.SetSession("_spkHeader_for_matching", _spkHeader)
                End If

                Return IsValid
            End If

            MessageBox.Show("Data SPK tidak ditemukan.")
        Catch ex As Exception
            MessageBox.Show("Gagal melakukan pengecekan pada No SPK.")
        End Try

        Return False
    End Function

    'Private Function IsValidSPKDate(_spkHeader As SPKHeader) As Boolean
    '    Dim criteriasDealerSystems As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSystems), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    criteriasDealerSystems.opAnd(New Criteria(GetType(DealerSystems), "Dealer.ID", MatchType.Exact, _spkHeader.Dealer.ID))
    '    Dim DealerSystemsFacade As DealerSystemsFacade = New DealerSystemsFacade(User)
    '    Dim arlDealerSystem As ArrayList = DealerSystemsFacade.Retrieve(criteriasDealerSystems)
    '    For Each objDealerSystem As DealerSystems In arlDealerSystem
    '        If objDealerSystem.isSPKDNET Then
    '            'listSPKFilter.Add(objListSPK)
    '        Else
    '            If CType(_spkHeader.CreatedTime, Date) < objDealerSystem.GoLiveDate Then
    '                'listSPKFilter.Add(objListSPK)
    '            Else

    '                MessageBox.Show("Matching/Unmatching SPK " + _spkHeader.SPKNumber + " tidak bisa diproses di DNET")
    '                'listSPKYana.Add(objListSPK)
    '                Return False

    '            End If
    '        End If
    '    Next
    '    Return True
    'End Function

    Private Function IsValidFaktur() As Boolean
        Try
            Dim objSessionHelper As New SessionHelper
            objSessionHelper.RemoveSession("_RevisionFaktur_for_matching")

            Dim criterias As New CriteriaComposite(New Criteria(GetType(VWI_OpenFaktur), "ChassisNumber", MatchType.Exact, _chassisMaster.ChassisNumber))
            Dim listFaktur As ArrayList = New VWI_OpenFakturFacade(User).RetrieveByCriteria(criterias)
            If listFaktur.Count > 0 Then
                Dim faktur As VWI_OpenFaktur = CType(listFaktur(0), VWI_OpenFaktur)
                If faktur.SPKNumber <> "0000000000" And Not String.IsNullorEmpty(faktur.SPKNumber) Then
                    criterias = New CriteriaComposite(New Criteria(GetType(VWI_InvoiceRevision), "ChassisNumber", MatchType.Exact, _chassisMaster.ChassisNumber))
                    Dim sort As Sort = New Sort(GetType(VWI_InvoiceRevision), "ID", sort.SortDirection.DESC)
                    Dim sorts As SortCollection = New SortCollection
                    sorts.Add(sort)
                    Dim invoiceList As List(Of VWI_InvoiceRevision) = New VWI_InvoiceRevisionFacade(User).RetrieveByCriteria(criterias, sorts).Cast(Of VWI_InvoiceRevision).ToList

                    If invoiceList.Count > 0 Then
                        Dim invRev As VWI_InvoiceRevision = invoiceList.FirstOrDefault()
                        If String.IsNullorEmpty(invRev.SPKNumber) Then
                            MessageBox.Show("SPKNumber tidak ditemukan.")
                            Return False
                        ElseIf _isMatching And Not (invRev.SPKNumber.Equals(faktur.SPKNumber, StringComparison.OrdinalIgnoreCase) And invRev.ChassisNumber.Equals(_chassisMaster.ChassisNumber, StringComparison.OrdinalIgnoreCase)) Then
                            MessageBox.Show(String.Format("Nomor SPK sama dengan Nomor SPK Revisi {0}.", faktur.SPKNumber))
                            Return False
                        ElseIf Not invRev.RevisionStatusID = 2 Then
                            MessageBox.Show(String.Format("Mohon menyelesaikan revisi faktur menjadi konfirmasi terlebih dahulu atas nomor SPK {0}.", faktur.SPKNumber))
                            Return False
                        End If

                        If _isMatching Then
                            'update remarks = blank on matching
                            Dim _revFak As RevisionFaktur = New RevisionFakturFacade(User).Retrieve(invRev.ID)
                            If Not IsNothing(_revFak) Then
                                _revFak.Remark = String.Empty
                                objSessionHelper.SetSession("_RevisionFaktur_for_matching", _revFak)
                            End If
                        End If
                    ElseIf _isMatching And faktur.SPKNumber <> txtNoSPK.Text Then
                        MessageBox.Show(String.Format("Nomor SPK tidak sama dengan SPK Faktur chassis {0}.", txtNoSPK.Text, _chassisMaster.ChassisNumber))
                        Return False
                    ElseIf Not _isMatching Then
                        MessageBox.Show(String.Format("Mohon melakukan revisi faktur terlebih dahulu atas nomor SPK {0}.", faktur.SPKNumber))
                        Return False
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show("Gagal melakukan pengecekkan faktur.")
            Return False
        End Try
    End Function

    Private Function IsSPKDetailValid() As Boolean

        Try
            Dim listOfSPKDetail As ArrayList = _spkHeader.SPKDetails
            Dim objSessionHelper As New SessionHelper
            objSessionHelper.RemoveSession("_spkDetail_for_matching")
            If Not (listOfSPKDetail Is Nothing) And listOfSPKDetail.Count > 0 Then
                Dim spkDetail As SPKDetail
                Dim err As Integer = 0
                For Each spkDetail In listOfSPKDetail
                    If spkDetail.VehicleColorCode = _chassisMaster.VechileColor.ColorCode _
                        And spkDetail.VehicleTypeCode = _chassisMaster.VechileType Then
                        _spkDetail = spkDetail

                        Dim totalMatched As Integer = New SPKChassisFacade(User).RetrieveTotalMathedSPKDetail(_spkDetail.ID)

                        If _spkDetail.Quantity = totalMatched Then
                            Continue For
                        ElseIf _spkDetail.Quantity > totalMatched Then
                            txtVehicleType.Text = _chassisMaster.VechileType
                            txtKodeWarna.Text = String.Format("{0}({1})", _chassisMaster.VechileColor.ColorCode, _chassisMaster.VechileColor.ColorIndName)
                            objSessionHelper.SetSession("_spkDetail_for_matching", _spkDetail)
                            Return True
                        Else
                            MessageBox.Show(
                                String.Format("Tidak dapat melanjutkan proses. Semua SPK Detail dengan tipe ({0}) dan warna ({1}) telah memiliki Chassis.",
                                              _chassisMaster.VechileType,
                                              _chassisMaster.VechileColor.ColorCode))
                            Return False
                        End If
                    Else
                        err = err + 1
                    End If
                Next
                If err > 0 Then
                    MessageBox.Show("Kode warna pada chassis dan spk tidak sama, silahkan cek kembali.")
                End If
            Else
                MessageBox.Show("Kode warna pada chassis dan spk tidak sama, silahkan cek kembali.")
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show("Gagal melakukan pengecekan Vehicle Color and Vehicle Type pada SPK Detail.")
        End Try

        Return False
    End Function

    Private Function IsChassisMasterValid() As Boolean

        Try
            Dim chassisNumber As String = txtNoChassis.Text.Trim()
            Dim objSessionHelper As New SessionHelper
            objSessionHelper.RemoveSession("_chassisMaster_for_matching")

            Dim criterias As New CriteriaComposite(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, chassisNumber))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            objDealer = Session("DEALER")
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "Dealer.ID", MatchType.Exact, objDealer.ID))
            End If

            Dim listOfChassisMaster As ArrayList = New ArrayList
            listOfChassisMaster = New ChassisMasterFacade(User).RetrieveByCriteria(criterias, Nothing)

            If Not (listOfChassisMaster Is Nothing) And listOfChassisMaster.Count > 0 Then
                _chassisMaster = DirectCast(listOfChassisMaster(0), ChassisMaster)
                objSessionHelper.SetSession("_chassisMaster_for_matching", _chassisMaster)
                Return True
            End If

            MessageBox.Show("ChassisMaster tidak ditemukan atau tidak sesuai dengan login dealer.")
        Catch ex As Exception
            MessageBox.Show("Gagal melakukan pengecekan pada ChassisMaster.")
        End Try

        _chassisMaster = Nothing

        Return False
    End Function

    Private Function IsSPKChassisValid() As Boolean

        Try
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SPKChassis), "ChassisMaster.ID", MatchType.Exact, _chassisMaster.ID))
            criterias.opAnd(New Criteria(GetType(SPKChassis), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            ' Get the last MatchingDate Data
            Dim sortCol As SortCollection = New SortCollection
            sortCol.Add(New Sort(GetType(SPKChassis), "MatchingDate", Sort.SortDirection.DESC))
            sortCol.Add(New Sort(GetType(SPKChassis), "CreatedTime", Sort.SortDirection.DESC))

            Dim listOfSPKChassis As ArrayList = New ArrayList
            listOfSPKChassis = New SPKChassisFacade(User).Retrieve(criterias, sortCol)

            If Not (listOfSPKChassis Is Nothing) And listOfSPKChassis.Count > 0 Then

                Dim spkChassis As SPKChassis = DirectCast(listOfSPKChassis(0), SPKChassis)
                ' To Match Chassis, Check on SPKChassis that it should never been matched ( matchingtype not 1 or 3)
                ' To UnMatch Chassis, Check on SPKChassis that it should been matched ( matchingtype = 1 or 3)

                If _isMatching Then
                    If spkChassis.MatchingType <> 1 And spkChassis.MatchingType <> 3 Then
                        'txtVehicleType.Text = _chassisMaster.VechileType
                        'txtKodeWarna.Text = _chassisMaster.VechileColor.ColorCode
                        Return True
                    End If
                    MessageBox.Show("Chassis Number tidak valid untuk dilakukan Matching. Chassis Number telah di matching dengan spk lain.")
                Else
                    If spkChassis.MatchingType = 1 Or spkChassis.MatchingType = 3 Then
                        Dim objSessionHelper As New SessionHelper
                        'Dim isValid As Boolean = IsValidSPKDate(spkChassis.SPKDetail.SPKHeader)

                        If isValid Then
                            _spkDetail = spkChassis.SPKDetail
                            objSessionHelper.SetSession("_spkDetail_for_matching", _spkDetail)
                            txtNoSPK.Text = spkChassis.SPKDetail.SPKHeader.SPKNumber
                            txtVehicleType.Text = _chassisMaster.VechileType
                            txtKodeWarna.Text = String.Format("{0}({1})", _chassisMaster.VechileColor.ColorCode, _chassisMaster.VechileColor.ColorIndName)
                            txtKeyNo.Text = spkChassis.KeyNumber
                        End If

                        Return isValid
                    End If
                    MessageBox.Show("Chassis Number tidak valid untuk dilakukan UnMatching. Tidak ada SPK yang Match dengan Chassis Number.")
                End If

            Else
                If _isMatching Then
                    Return True
                End If

                MessageBox.Show("Chassis Number tidak valid untuk dilakukan UnMatching.")
            End If


        Catch ex As Exception
            MessageBox.Show("Gagal melakukan pengecekan Chassis Number pada SPKChassis.")
        End Try

        Return False
    End Function

    Protected Sub txtNoChassis_TextChanged(sender As Object, e As EventArgs) Handles txtNoChassis.TextChanged
        _validChassisNumber = False
        Dim objSessionHelper As New SessionHelper
        objSessionHelper.SetSession("_validChassisNumber_for_matching", _validChassisNumber)
    End Sub

    Protected Sub txtNoSPK_TextChanged(sender As Object, e As EventArgs) Handles txtNoSPK.TextChanged
        _validSPKNumber = False
        Dim objSessionHelper As New SessionHelper
        objSessionHelper.SetSession("_validSPKNumber_for_matching", _validSPKNumber)
    End Sub
End Class