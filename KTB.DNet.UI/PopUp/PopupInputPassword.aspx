<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopupInputPassword.aspx.vb" Inherits="PopupInputPassword" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopupInputPassword</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/BasicStyle.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript">
				
		function GetPassword()
		{
		    var ret = '';
		    var txtUserName = document.getElementById("txtUserName");
		    var txtPassword = document.getElementById("txtPassword");

		    if (txtPassword.value == "") {
		        alert("Please Input Your Password!")
		        txtPassword.focus();
		        return false;
		    }

		    ret = txtUserName.value + ';' + txtPassword.value;
		    if (navigator.appName == "Microsoft Internet Explorer") {
		        
		        window.returnValue = ret;
		    }
		    else {
		        window.opener.dialogWin.returnFunc(ret);
		    }

		    window.close();

		}

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="4" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" colSpan="7">CREDENTIAL -&nbsp;Input SAP Password</td>
							</tr>
							<tr>
								<td background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
							<TR valign="top">
								<TD class="titleField" width="20%">UserName</TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:textbox id="txtUserName"  runat="server" Width="152px"  ></asp:textbox></TD>
								<TD style="WIDTH: 17px" width="2%"></TD>
								<TD class="titleField" width="20%">Password</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 225px" width="225">
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <asp:textbox id="txtPassword" runat="server" TextMode="Password"></asp:textbox>
                                            </td>
                                            <td width="1px"></td>
                                            <td>
                                                <INPUT id="btnOK" style="WIDTH: 60px" onclick="GetPassword()" type="button"
										value="OK" name="btnChoose" runat="server">

                                            </td>
                                        </tr>
                                    </table>
                                    </TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
