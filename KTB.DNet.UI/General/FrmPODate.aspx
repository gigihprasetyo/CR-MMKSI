<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmPODate.aspx.vb" Inherits=".FrmPODate" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmCity</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
		    function ShowCriteriaDescription() {
		        showPopUp('../PopUp/PopUpCriteriaDescription.aspx', '', 500, 760);
		    }
		</script>
        <style type="text/css">
            .auto-style1 {
                font-family: Sans-Serif, Arial;
                font-size: 11pt;
                color: #990000;
                margin: 0px;
                font-weight: bold;
                height: 18px;
            }
        </style>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td><asp:Literal ID="ltrSQl" Runat="server" visible="false"></asp:Literal></td>
				</tr>
				<tr>
					<td class="auto-style1">Purchase Order - Setting Tanggal PO</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%" vAlign="top">Tanggal PO EOM</TD>
								<TD width="1%" vAlign="top">:</TD>
								<TD><cc1:inticalendar id="lbldate" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
								
							</TR>
							<TR>
								<td colspan="3">&nbsp;</td>
							</TR>
							
                            <TR>
                                <td colspan="3">&nbsp;</td>
                            </TR>

                            </tr>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD>
									<asp:button id="btnSave" runat="server" Width="60px" Text="Simpan"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
		    if (window.parent == window) {
		        if (!navigator.appName == "Microsoft Internet Explorer") {
		            self.opener = null;
		            self.close();
		        }
		        else {
		            this.name = "origWin";
		            origWin = window.open(window.location, "origWin");
		            window.opener = top;
		            window.close();
		        }
		    }
		</script>
		<!-- 
<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
														CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; &quot; alt=&quot;Batal&quot;&gt;"
														EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
														<HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
													</asp:EditCommandColumn>
-->
	</body>
</HTML>