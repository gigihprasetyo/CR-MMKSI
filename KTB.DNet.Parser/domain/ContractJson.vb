Imports System.Collections
Imports System.Collections.Generic


Namespace KTB.DNet.Parser.Domain

    Public Class ContractJson
        Public ContractNo As String
        Public DealerCode As String
        Public Description As String
        Public PKNumber As String
        Public RefContract As String
        Public ContractPeriod As String
        Public ContractPricingPeriod As String
        Public Detail As List(Of ContractDetailJson)
    End Class

End Namespace
