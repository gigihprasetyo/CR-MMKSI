#Region ".NET Base Class Namespace Imports"

Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Reflection
Imports System.Configuration
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.BusinessForum
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.Training
Imports System.Security.Principal
Imports KTB.DNet.BusinessFacade.IndentPartEquipment
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.MDP
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Collections.Generic
Imports KTB.DNet.WebCC

#End Region

Namespace KTB.DNet.Utility
    Public Class POFunction
        Public Shared User As New System.Security.Principal.GenericPrincipal(New GenericIdentity("dnet"), Nothing)

        Public Shared Function ValidatePengiriman(ByVal arrPODetail As ArrayList, ByRef msg As String, ByVal isByMKSChecked As Boolean) As Boolean
            Dim isTritonFilterActive As Boolean = False
            Dim isFilteredTriton As Boolean = False
            Dim isNonFilteredTriton As Boolean = False
            Dim Result As Boolean = False
            Dim VehicleDesc As String = String.Empty

            Dim TritonConf As AppConfig = New AppConfigFacade(User).Retrieve("LogisticTriton")
            isTritonFilterActive = CType(TritonConf.Value, Boolean)

            If isTritonFilterActive Then
                Dim arlStdCodeTriton As ArrayList = New StandardCodeFacade(User).RetrieveByCategory("LogisticTriton")
                If arlStdCodeTriton.Count > 0 Then
                    For Each oPD As PODetail In arrPODetail
                        Dim i As Integer = 0
                        For Each oSC As StandardCode In arlStdCodeTriton
                            If oPD.ReqQty > 0 Then
                                If oPD.ContractDetail.VechileColor.VechileType.VechileTypeCode = oSC.ValueCode Then
                                    isFilteredTriton = True
                                    VehicleDesc = oPD.ContractDetail.VechileColor.VechileType.VechileCodeDesc
                                    Exit For
                                Else
                                    If i = arlStdCodeTriton.Count - 1 Then
                                        isNonFilteredTriton = True
                                    End If
                                    i += 1
                                End If
                            End If
                        Next
                    Next

                    If isNonFilteredTriton AndAlso isFilteredTriton Then
                        Result = False
                        msg = "Silahkan membuat PO terpisah untuk type kendaraan " & VehicleDesc
                        Return Result
                    End If

                    If Not isByMKSChecked AndAlso isFilteredTriton Then
                        Result = False
                        msg = VehicleDesc & " harus melalui pengiriman oleh MMKSI"
                        Return Result
                    End If

                    Return True
                Else
                    Return True
                End If
            Else
                Return True
            End If

        End Function

    End Class
End Namespace