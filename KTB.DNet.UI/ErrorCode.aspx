<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ErrorCode.aspx.vb" Inherits="ErrorCode" smartNavigation="False" %>

<HTML>
  <HEAD>
		<title>ErrorCode</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <script language=javascript src="./WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
  </HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div valign="middle" align="center">
				<br>
				<br>
				<br>
				<br>
				<br>
				<TABLE id="Table1" align="center" cellSpacing="4" bgcolor="black" cellPadding="0" width="448"
					border="0">
					<TR>
						<TD width="148"><IMG src="images/UnderConstruction3.jpg" border="0"></TD>
						<TD width="300" bgColor="#cc0001" align="center"><asp:label id="lblErrorCode" runat="server" Font-Bold="True" ForeColor="White" BorderWidth="0px"
								BackColor="#CC0001" Font-Size="Large">Error Code</asp:label></TD>
					</TR>
				</TABLE>
                <table style="background-color:yellow">
                    <tr>
                        <td>
                            <asp:Label ID="lblErrorDesc" runat="server" ></asp:Label>
                        </td>
                    </tr>
                </table>               
				<b>Please Contact Administrator</b>
				<br>
			</div>
		</form>
	</body>
</HTML>
