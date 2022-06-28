Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain.enumMode
Imports System.Security.Principal
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Collections.Generic
Imports KTB.DNet.BusinessFacade.PK

Namespace KTB.DNet.Utility

    Public Class SPLFunction
        Public Shared User As New System.Security.Principal.GenericPrincipal(New GenericIdentity("dnet"), Nothing)

        Public Shared Function IsDBNull(ByVal obj As Object, ByVal replacement As Decimal) As Decimal
            If obj Is DBNull.Value Then
                Return replacement
            End If
            Return CType(obj, Decimal)
        End Function

        Public Shared Function GetResponseQtyPKDetail(ByVal obj As SPLDetail) As Integer
            Dim _total As Integer = 0
            Dim criterias As New CriteriaComposite(New Criteria(GetType(PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.SPLNumber", MatchType.Exact, obj.SPL.SPLNumber))
            criterias.opAnd(New Criteria(GetType(PKDetail), "VehicleTypeCode", MatchType.Exact, obj.VechileType.VechileTypeCode))
            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, obj.PeriodMonth))
            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, obj.PeriodYear))
            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.PKStatus", MatchType.InSet, "(" & CType(enumStatusPK.Status.Rilis, Integer) _
                    & "," & CType(enumStatusPK.Status.Selesai, Integer) & "," & CType(enumStatusPK.Status.Konfirmasi, Integer) & "," & CType(enumStatusPK.Status.Setuju, Integer) & ")"))
            Dim aggregateSum As Aggregate = New Aggregate(GetType(PKDetail), "ResponseQty", AggregateType.Sum)
            _total = IsDBNull(New PKDetailFacade(User).RetrieveScalar(criterias, aggregateSum), 0)

            Return _total
        End Function

    End Class

End Namespace
