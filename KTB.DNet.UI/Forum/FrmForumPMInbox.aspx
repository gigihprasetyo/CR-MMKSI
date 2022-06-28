<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmForumPMInbox.aspx.vb" Inherits="FrmForumPMInbox" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmForumPMInbox</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function CheckAll(aspCheckBoxID, checkVal) 
		{
			re = new RegExp(':' + aspCheckBoxID + '$')  
			for(i = 0; i < document.forms[0].elements.length; i++) {
				elm = document.forms[0].elements[i]
				if (elm.type == 'checkbox') {
					if (re.test(elm.name)) {
						elm.checked = checkVal
					}
				}
			}
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">FORUM&nbsp;-&nbsp;Private Message</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</TABLE>
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td valign="top"><asp:linkbutton id="lbtnInbox" Runat="server"><img src="../images/icon_inbox2.gif" align="left" border="0"> Inbox</asp:linkbutton>
						&nbsp;&nbsp;
						<asp:linkbutton id="lbtnOutbox" Runat="server"><img src="../images/icon_outbox.gif" border="0"> Outbox</asp:linkbutton>
						&nbsp;&nbsp;
						<asp:linkbutton id="lbtnCompose" Runat="server"><img src="../images/icon_compos.gif" border="0">Compose</asp:linkbutton></td>
				</tr>
			</TABLE>
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td><asp:datagrid id="dtgForumPM" runat="server" PageSize="25" AllowSorting="True" CellSpacing="1"
							AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" AllowPaging="True"
							AllowCustomPaging="True" BackColor="#CDCDCD" CellPadding="3" GridLines="None" Width="100%">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" VerticalAlign="Top" BackColor="#FDF1F2"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:TemplateColumn Visible="False" HeaderText="ID">
									<ItemTemplate>
										<asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Subject" HeaderText="Subject">
									<ItemTemplate>
										<asp:LinkButton id="lbtnSubject" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Subject") %>' CommandName="ShowMSG">
										</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="UserFrom" HeaderText="From">
									<ItemTemplate>
										<asp:Label id="lblFrom" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserFrom") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Sent To" SortExpression="UserInfo.ID">
									<ItemTemplate>
										<asp:Label id="lblSentTo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserInfo.ID") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="CreatedTime" HeaderText="Received">
									<ItemTemplate>
										<asp:Label id="lblReceived" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CreatedTime") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderTemplate>
										<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
									</HeaderTemplate>
									<ItemTemplate>
										<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="IsRead" Visible="False">
									<ItemTemplate>
										<asp:Label id="lblIsRead" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IsRead") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<TR>
					<TD align="right"><asp:button id="btnDelete" runat="server" Text="Hapus"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
