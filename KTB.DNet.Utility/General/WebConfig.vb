Imports System.Configuration
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General

Namespace KTB.DNet.Utility

    Public Class WebConfig

        Public Shared Function GetValue(ByVal KeyName As String) As String
            Try
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "Name", MatchType.Exact, KeyName))


                Dim DataRow As ArrayList
                DataRow = New AppConfigFacade(Nothing).RetrieveByCriteria(crit)
                If DataRow.Count = 0 Then
                    Return String.Empty
                Else
                    Dim Row As AppConfig = CType(DataRow.Item(0), AppConfig)
                    Return Row.Value
                End If

            Catch ex As Exception
                Return String.Empty
            End Try
        End Function




    End Class

End Namespace
