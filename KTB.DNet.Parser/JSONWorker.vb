
#Region "System Imports"
Imports System
Imports System.IO
Imports System.Diagnostics
Imports System.Threading
Imports System.Configuration
Imports System.Security.Principal
#End Region

#Region "Custom Imports"
Imports KTB.DNet.Parser
Imports KTB.DNet.Utility
#End Region

Namespace KTB.DNet.Parser

    Public Class JSONWorker

#Region "Declare Parser"
        Private _contractCreateParser As ContractParserJson
        Private _flatRateMasterFSParser As FlatRateMasterFSParser
        Private _flatRateMasterPMParser As FlatRateMasterPMParser
        Private _flatRateMasterFieldFixParser As FlatRateMasterFieldFixParser
        Private _serviceTemplateFSPartParser As ServiceTemplateFSPartParser
        Private _serviceTemplateFSLaborParser As ServiceTemplateFSLaborParser
        Private _serviceTemplatePMPartParser As ServiceTemplatePMPartParser
        Private _serviceTemplatePMLaborParser As ServiceTemplatePMLaborParser
        Private _serviceTemplateFFPartParser As ServiceTemplateFFPartParser
        Private _serviceTemplateFFLaborParser As ServiceTemplateFFLaborParser
        Private _serviceTemplateSOPPHOnlineParser As SOPPHOnlineMasterParser
        Private _serviceTemplateSOPPHOnlineStatusParser As SOPPHOnlineStatusParser
#End Region

        Private Function Distribute(ByVal KeyName As String, ByVal content As String, ByRef msg As String) As Boolean
            Select Case KeyName.ToUpper()
                Case "OCCREATE"
                    _contractCreateParser = New ContractParserJson()
                    _contractCreateParser.ParseWithTransactionWS(KeyName, content)
                Case "FLATRATETIMEFS"
                    _flatRateMasterFSParser = New FlatRateMasterFSParser()
                    Return _flatRateMasterFSParser.ParseWithTransactionWS(KeyName, content, msg)
                Case "FLATRATETIMEPM"
                    _flatRateMasterPMParser = New FlatRateMasterPMParser()
                    Return _flatRateMasterPMParser.ParseWithTransactionWS(KeyName, content, msg)
                Case "FLATRATETIMEFF"
                    _flatRateMasterFieldFixParser = New FlatRateMasterFieldFixParser()
                    Return _flatRateMasterFieldFixParser.ParseWithTransactionWS(KeyName, content, msg)
                Case "SERVICETEMPLATEFSPART"
                    _serviceTemplateFSPartParser = New ServiceTemplateFSPartParser()
                    Return _serviceTemplateFSPartParser.ParseWithTransactionWS(KeyName, content, msg)
                Case "SERVICETEMPLATEFSLABOR"
                    _serviceTemplateFSLaborParser = New ServiceTemplateFSLaborParser()
                    Return _serviceTemplateFSLaborParser.ParseWithTransactionWS(KeyName, content, msg)
                Case "SERVICETEMPLATEPMPART"
                    _serviceTemplatePMPartParser = New ServiceTemplatePMPartParser()
                    Return _serviceTemplatePMPartParser.ParseWithTransactionWS(KeyName, content, msg)
                    'Case "SERVICETEMPLATEPMLABOR"
                    '    _serviceTemplatePMLaborParser = New ServiceTemplatePMLaborParser()
                    '    Return _serviceTemplatePMLaborParser.ParseWithTransactionWS(KeyName, content, msg)
                Case "SERVICETEMPLATEFFPART"
                    _serviceTemplateFFPartParser = New ServiceTemplateFFPartParser()
                    Return _serviceTemplateFFPartParser.ParseWithTransactionWS(KeyName, content, msg)
                Case "SERVICETEMPLATEFFLABOR"
                    _serviceTemplateFFLaborParser = New ServiceTemplateFFLaborParser()
                    Return _serviceTemplateFFLaborParser.ParseWithTransactionWS(KeyName, content, msg)
                Case "SOPPHDATA"
                    _serviceTemplateSOPPHOnlineParser = New SOPPHOnlineMasterParser()
                    Return _serviceTemplateSOPPHOnlineParser.ParseWithTransactionWS(KeyName, content, msg)
                Case "SOPPHSTATUS"
                    _serviceTemplateSOPPHOnlineStatusParser = New SOPPHOnlineStatusParser()
                    Return _serviceTemplateSOPPHOnlineStatusParser.ParseWithTransactionWS(KeyName, content, msg)
                Case Else
                    Return False
            End Select

            Return True
        End Function


        Public Function JSONProses(ByVal body As String, ByVal keyName As String, Optional ByRef Msg As String = "") As Boolean
            Dim IsOk As Boolean = False

            If Not String.IsNullorEmpty(body) Then
                If keyName <> String.Empty Then
                    Try
                        IsOk = Distribute(keyName, body, Msg)
                        If IsOk Then
                            Msg = "Success"
                        End If
                    Catch ex As Exception
                        Msg = ex.Message
                    End Try

                    Return IsOk
                End If

                Msg = "Invalid Data"
                Return False
            Else
                Msg = "Invalid Data"
                Return False
            End If


            Return True
        End Function
    End Class

End Namespace

