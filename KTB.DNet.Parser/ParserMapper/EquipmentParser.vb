#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.DataMapper
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.PO
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Security.Principal
#End Region

Namespace KTB.DNet.Parser

    Public Class EquipmentParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _Equipment As ArrayList
        Private Grammar As Regex
        'Private mapper As IMapper
        Private _fileName As String
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"
        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _Stream = New StreamReader(fileName, True)
            _Equipment = New ArrayList
            Dim val As String = MyBase.NextLine(_Stream).Trim()
            While (Not val = "")
                'ParseEquipment(val + ";")
                Try
                    ParseEquipment(val + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "EquipmentParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.EquipmentParser, BlockName)
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return _Equipment
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

        Protected Overrides Function DoTransaction() As Integer
            If _Equipment.Count > 0 Then
                For Each item As EquipmentMaster In _Equipment
                    If item.ErrorMessage Is Nothing Then
                        Dim objEquipmentFacade As EquipmentMasterFacade = New EquipmentMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        objEquipmentFacade.AddEquipment(item)
                    End If
                Next
            Else
                Throw New Exception("Parsing Equipment file : " & _fileName & " dengan 0 record / terdapat duplikasi.")
            End If
        End Function

#End Region

#Region "Private Methods"

        Private Function isValidEquipmentMaster(ByVal collEquipmentMaster As ArrayList) As Boolean
            For Each item As EquipmentMaster In collEquipmentMaster
                If Not item.ErrorMessage Is Nothing Then
                    If item.ErrorMessage.Length > 0 Then
                        Return False
                    End If
                End If
            Next
            Return True
        End Function

        Private Sub ParseEquipment(ByVal ValParser As String)
            Dim _Eq As EquipmentMaster = New EquipmentMaster
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
                    Case Is = 0
                        If sTemp.Trim.ToUpper = "X" Then
                            _Eq.Status = 1
                        Else
                            _Eq.Status = 0
                        End If
                    Case Is = 1
                        If sTemp.Length > 0 Then
                            _Eq.EquipmentNumber = sTemp
                        Else
                            _Eq.ErrorMessage = _Eq.ErrorMessage & " Data tidak Lengkap"
                        End If
                    Case Is = 2
                        If sTemp.Length > 0 Then
                            If sTemp.ToUpper = "EQUIP" Then
                                _Eq.Kind = EquipmentKind.EquipmentKindEnum.Pembelian
                            Else
                                If sTemp.ToUpper = "REPAIR" Then
                                    _Eq.Kind = EquipmentKind.EquipmentKindEnum.Perbaikan
                                Else
                                    '_Eq.Kind = ""
                                    _Eq.ErrorMessage = _Eq.ErrorMessage & "Kind Salah"
                                End If
                            End If
                        End If
                    Case Is = 3
                        If sTemp.Length > 0 Then
                            _Eq.Description = sTemp
                        End If
                End Select

                sStart = m.Index + 1
                nCount += 1
            Next

            If isEquipmentExist(_Eq) Then
                _Eq.ErrorMessage = _Eq.ErrorMessage & " Duplikasi Kode Equipment "
            End If

            _Equipment.Add(_Eq)
        End Sub

        Private Function isEquipmentExist(ByVal eq As EquipmentMaster) As Boolean
            For Each item As EquipmentMaster In _Equipment
                If eq.EquipmentNumber = item.EquipmentNumber Then
                    Return True
                End If
            Next
            Return False
        End Function


#End Region

    End Class

End Namespace