<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmReadMessage.aspx.vb" Inherits="FrmReadMessage" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmReadMessage</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 19px">FORUM&nbsp;- Read Message</td>
				</tr>
				<tr>
					<td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 27px" width="24%" colSpan="3">
									<asp:Label id="lblListTitle" runat="server" Width="664px"></asp:Label></TD>
							</TR>
						</TABLE>
						<asp:label id="lblDate" runat="server" Width="264px">Label</asp:label>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 390px"><asp:datagrid id="dtgMessagePosting" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
								AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="Gainsboro" CellPadding="3" BorderColor="#E0E0E0" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<u>
												<asp:Label id="lblUser" runat="server"></asp:Label></u><br>
											<asp:Image id="lbtnImage" runat="server" Width="50px" Height="50px"></asp:Image><br>
											<asp:Label id="lblJoinDate" runat="server"></asp:Label><br>
											<asp:Label id="lblPosting" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="75%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<b>
												<asp:Label id="lblTittle" runat="server"></asp:Label></b><br>
											<div style="OVERFLOW: auto; HEIGHT: 100px"><asp:Label id="lblMessagePosting" runat="server"></asp:Label></div>
											Attachment :
											<asp:LinkButton id="lbtnFileName" runat="server" CommandName="Download">
												<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD height="40"><asp:button id="btnReply" runat="server" width="88px" Text="Reply"></asp:button>
						<asp:Button id="btnBack" runat="server" Width="88px" Text="Kembali"></asp:Button></TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
