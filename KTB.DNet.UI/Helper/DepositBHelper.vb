Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.IndentPartEquipment
Imports KTB.DNet.BusinessFacade.Service

Public Class DepositBHelper
    Inherits System.Web.UI.Page

    Public Function PlafonChecking(ByVal dealerID As Integer, ByVal productCategoryID As Integer, _
                    ByVal tipePengajuan As Integer, ByVal periode As Integer, ByVal totalAmount As Decimal, ByRef msg As String) As Boolean

        'Public Function PlafonChecking(ByVal objDepositBPencairanH As DepositBPencairanHeader, ByVal periode As Integer, ByRef msg As String) As Boolean
        'Dim TotalAmount As Decimal = objDepositBPencairanH.DealerAmount
        'Dim dealerID As Integer = objDepositBPencairanH.Dealer.ID
        'Dim productCategoryID As Integer = objDepositBPencairanH.ProductCategory.ID
        'Dim tipePengajuan As Integer = objDepositBPencairanH.TipePengajuan


        Dim totalDeposit As Decimal = GetTotalPDepositB(dealerID, productCategoryID, periode)
        If Not (totalDeposit > 0) Then
            msg = "Dealer tidak mempunyai Deposit B "
            Return False
        End If

        Dim totalPlafon = GetTotalPlafon(dealerID, productCategoryID, periode)
        If totalPlafon = 0 Then
            msg = "Plafon belum dibuat."
            Return False
        End If

        Dim totalDone = GetTotalYangSudahDiAjukan(dealerID, tipePengajuan, productCategoryID)

        Dim maxPencairan = totalDeposit - totalPlafon

        Dim totalMaxPengajuan As Decimal = 0

        totalMaxPengajuan = maxPencairan - totalDone

        If totalAmount > totalMaxPengajuan Then
            'msg = "Plafon tidak mencukupi, maximal nilai pengajuan : " & FormatNumber(totalMaxPengajuan, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault) + ". "
            msg = "Plafon tidak mencukupi, maximal total pengajuan : " & FormatNumber(maxPencairan, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault) + ". \n"
            msg = msg + " Nilai pengajuan dalam proses : " & FormatNumber(totalDone, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault) + ". \n"
            msg = msg + " Sehingga maximal pengajuan : " & FormatNumber(totalMaxPengajuan, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault) + ". \n"

            Return True
        Else
            msg = String.Empty
            Return True
        End If

    End Function


    Protected Function GetTotalPDepositB(ByVal dealerID As Integer, ByVal productCategoryID As Integer, ByVal periode As Integer) As Decimal
        Dim totalDepositB As Decimal = 0
        Dim transactioDateStart As DateTime = New DateTime(periode, 1, 1, 0, 0, 0)
        Dim transactioDateTo As DateTime = New DateTime(periode, 12, 31, 23, 59, 59)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "Dealer.ID", MatchType.Exact, dealerID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "ProductCategory.ID", MatchType.Exact, productCategoryID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "TransactionDate", MatchType.GreaterOrEqual, transactioDateStart))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "TransactionDate", MatchType.Lesser, transactioDateTo))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(DepositBHeader), "TransactionDate", Sort.SortDirection.DESC))

        Dim arlDepositB As ArrayList = New DepositBHeaderFacade(User).Retrieve(criterias, sortColl)
        If arlDepositB.Count > 0 Then
            Dim objDepositB As DepositBHeader = CType(arlDepositB(0), DepositBHeader)
            totalDepositB = objDepositB.EndBalance + totalDepositB
        End If
        Return totalDepositB
    End Function


    Protected Function GetTotalPlafon(ByVal dealerID As Integer, ByVal productCategoryID As Integer, ByVal periode As Integer) As Decimal
        Dim totalPlafon As Decimal = 0

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "Dealer.ID", MatchType.Exact, dealerID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "ProductCategory.ID", MatchType.Exact, productCategoryID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "PeriodePlafon", MatchType.Exact, periode))

        Dim arlPlafon As ArrayList = New DepositBPlafonFacade(User).Retrieve(criterias)
        For Each plafon As DepositBPlafon In arlPlafon
            totalPlafon = plafon.JumlahPlafon + totalPlafon
        Next
        Return totalPlafon
    End Function


    Protected Function GetTotalYangSudahDiAjukan(ByVal dealerID As Integer, ByVal iTipePengajuan As Integer, ByVal productCategoryID As Integer) As Decimal
        Dim totalPengajuan As Decimal = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "Dealer.ID", MatchType.Exact, dealerID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "ProductCategory.ID", MatchType.Exact, productCategoryID))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "Status", MatchType.InSet, "(0, 1, 4, 6 )"))

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "Status", MatchType.InSet, "(0, 1, 4)"))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "TipePengajuan", MatchType.No, "3"))  'Tipe Interest not include

        Dim arlPencairan As ArrayList = New DepositBPencairanHeaderFacade(User).Retrieve(criterias)
        For Each pencairan As DepositBPencairanHeader In arlPencairan
            If pencairan.Status = DepositBEnum.StatusPencairan.Baru Or _
                pencairan.Status = DepositBEnum.StatusPencairan.Validasi Then
                totalPengajuan = pencairan.DealerAmount + totalPengajuan
            Else
                totalPengajuan = pencairan.ApprovalAmount + totalPengajuan
            End If
        Next
        Return totalPengajuan

    End Function
End Class
