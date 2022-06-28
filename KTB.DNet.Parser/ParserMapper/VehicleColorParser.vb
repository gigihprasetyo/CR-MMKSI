#Region " Summary "
'-------------------------------------------------'
'-- Program Code : VehicleColorParser.vb        --'
'-- Program Name :                              --'
'-- Description  :                              --'
'-------------------------------------------------'
'-- Programmer   : Agus Pirnadi                 --'
'-- Start Date   : Sep 27 2005                  --'
'-- Update By    :                              --'
'-- Last Update  : Feb 17 2005                  --'
'-------------------------------------------------'
'-- Copyright © 2005 by Intimedia               --'
'-------------------------------------------------'
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
#End Region

Namespace KTB.DNet.Parser
    Public Class VehicleColorParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private VehicleColors As ArrayList
        Private Grammar As Regex
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _Stream = New StreamReader(fileName, True)
            VehicleColors = New ArrayList
            Dim val As String = MyBase.NextLine(_Stream).Trim()
            While (Not val = "")
                'ParseVehicleColor(val + ";")
                Try
                    ParseVehicleColor(val + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "VehicleColorParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.VehicleColorParser, BlockName)
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return VehicleColors
        End Function

        Protected Overrides Function DoTransaction() As Integer
            If VehicleColors.Count > 0 Then
                'Do Business - Like save to database
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Sub ParseVehicleColor(ByVal ValParser As String)
            Dim _VehicleColor As VechileColor = New VechileColor
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0

            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()

                Select Case (nCount)
                    Case Is = 0  '-- Status
                        If UCase(sTemp.Trim) = "X" Or sTemp.Trim = "" Then
                            _VehicleColor.Status = UCase(sTemp.Trim)
                        Else
                            _VehicleColor.ErrorMessage = _VehicleColor.ErrorMessage & "Status invalid;"
                        End If
                    Case Is = 1  '-- Special flag
                        If UCase(sTemp.Trim) = "X" Or sTemp.Trim = "" Then
                            _VehicleColor.SpecialFlag = UCase(sTemp.Trim)
                        Else
                            _VehicleColor.ErrorMessage = _VehicleColor.ErrorMessage & "Warna khusus invalid;"
                        End If
                    Case Is = 2  '-- Material number, vehicle type, & color code
                        If sTemp.Length = 0 Then
                            _VehicleColor.ErrorMessage = _VehicleColor.ErrorMessage & "Kode Warna Kosong;"
                        Else
                            Dim vt As VechileType = GetVechileType(sTemp.Trim.Substring(0, 4))

                            If vt.VechileTypeCode = "" Then
                                _VehicleColor.ErrorMessage = _VehicleColor.ErrorMessage & "Tipe Kosong;"
                            Else
                                Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
                                If vt.ProductCategory.Code.Trim <> companyCode Then
                                    _VehicleColor.ErrorMessage = _VehicleColor.ErrorMessage & "Tipe tidak terdapat pada Kategori Produk " & companyCode & ";"
                                Else
                                    _VehicleColor.MaterialNumber = sTemp.Trim
                                    _VehicleColor.VechileType = vt
                                    _VehicleColor.ColorCode = sTemp.Trim.Substring(4, 4)
                                End If
                            End If
                        End If
                    Case Is = 3  '-- Market code
                        _VehicleColor.MarketCode = sTemp.Trim
                    Case Is = 4  '-- Header BOM
                        If sTemp.Length = 0 Then
                            _VehicleColor.ErrorMessage = _VehicleColor.ErrorMessage & "Header Kosong;"
                        Else
                            _VehicleColor.HeaderBOM = sTemp.Trim
                        End If
                    Case Is = 5  '-- English name
                        _VehicleColor.ColorEngName = sTemp.Trim
                    Case Is = 6  '-- Indonesia name
                        _VehicleColor.ColorIndName = sTemp.Trim
                    Case Is = 7  '-- Description
                        _VehicleColor.MaterialDescription = sTemp.Trim
                    Case Else
                End Select

                sStart = m.Index + 1
                nCount += 1
            Next

            VehicleColors.Add(_VehicleColor)
        End Sub

        Private Function GetVechileType(ByVal code As String)
            Dim VTypefacade As New VechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim _vechileType As VechileType = VTypefacade.Retrieve(code)
            Try
                Dim Catfacade As New CategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                Dim _category As Category = Catfacade.Retrieve(_vechileType.Category.ID)
                _vechileType.Category = _category
            Catch ex As Exception
            End Try

            Return _vechileType
        End Function

#End Region

    End Class

End Namespace