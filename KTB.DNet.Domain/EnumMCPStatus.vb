Imports System.Web.UI.WebControls


Namespace KTB.DNet.Domain
    Public Class EnumMCPStatus
        Public Enum MCPStatus
            NonMCP = 0
            NotVerifiedMCP = 1
            VerifiedMCP = 2
        End Enum

        Public Shared Function GetStringValue(ByVal pMCPStatus As Integer) As String
            Dim str As String = ""
            If pMCPStatus = MCPStatus.NonMCP Then str = "NonMCP"
            If pMCPStatus = MCPStatus.NotVerifiedMCP Then str = "Not Verified MCP"
            If pMCPStatus = MCPStatus.VerifiedMCP Then str = "Verified MCP"
            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sMCPStatus As String) As Integer
            Dim Rsl As Integer = 0
            If sMCPStatus.ToUpper = GetStringValue(MCPStatus.NonMCP) Then Rsl = MCPStatus.NonMCP
            If sMCPStatus.ToUpper = GetStringValue(MCPStatus.NotVerifiedMCP) Then Rsl = MCPStatus.NotVerifiedMCP
            If sMCPStatus.ToUpper = GetStringValue(MCPStatus.VerifiedMCP) Then Rsl = MCPStatus.VerifiedMCP
            Return Rsl
        End Function

        Public Shared Function GetList() As ArrayList
            Dim arl As ArrayList = New ArrayList

            arl.Add(New ListItem(GetStringValue(MCPStatus.NonMCP), MCPStatus.NonMCP))
            arl.Add(New ListItem(GetStringValue(MCPStatus.NotVerifiedMCP), MCPStatus.NotVerifiedMCP))
            arl.Add(New ListItem(GetStringValue(MCPStatus.VerifiedMCP), MCPStatus.VerifiedMCP))

            Return arl
        End Function



    End Class
End Namespace
