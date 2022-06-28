<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TopicList.aspx.vb" Inherits="TopicList" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TopicList</title>
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
					<td class="titlePage" style="HEIGHT: 19px">FORUM&nbsp;-
						<asp:Label id="lblTitle" runat="server">Topik Forum</asp:Label></td>
				</tr>
				<tr>
					<td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD style="HEIGHT: 20px" align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">Kategori</TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:dropdownlist id="ddlCategory" runat="server" AutoPostBack="True" Width="152px"></asp:dropdownlist></td>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Forum</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:dropdownlist id="ddlForum" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Deskripsi Topik</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtTopicDescription"
										onblur="omitSomeCharacter('txtTopicDescription','<>?*%$;')" runat="server" Width="320px" MaxLength="100"></asp:textbox></TD>
							</TR>
							<TR  > 
								<TD class="titleField" width="24%">Status</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:dropdownlist id="ddlStatus" runat="server" Enabled="false">
										<asp:ListItem Value="1">Aktif</asp:ListItem>
										<asp:ListItem Value="0">Tidak aktif</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField">Tanggal Posting</TD>
								<TD width="1%">:</TD>
								<TD width="75%">
									<TABLE id="Table3" cellSpacing="0" cellPadding="3" border="0">
										<TR>
											<TD><asp:checkbox id="chkTgl" runat="server"></asp:checkbox>&nbsp;
											</TD>
											<TD><cc1:inticalendar id="icTglCreate" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<TD>
												s/d
											</TD>
											<TD><cc1:inticalendar id="icTglCreate2" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Diposting Oleh</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtPostingBy" onblur="omitSomeCharacter('txtPostingBy','<>?*%$;')"
										runat="server"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%"></TD>
								<TD width="1%"></TD>
								<TD width="75%"><asp:button id="ctnSearch" runat="server" Text="Cari" width="60px"></asp:button><asp:button id="btnNeTopic" runat="server" Text="Topik Baru" width="88px"></asp:button><asp:button id="btnKembali" runat="server" Text="Kembali"></asp:button></TD>
							</TR>
						</TABLE>
						<asp:label id="lblListTitle" runat="server" Width="664px"></asp:label></TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 240px"><asp:datagrid id="dtgTopicList" runat="server" Width="100%" PageSize="25" BorderColor="#E0E0E0"
								CellPadding="3" BackColor="Gainsboro" AutoGenerateColumns="False" AllowCustomPaging="True" AllowPaging="True" AllowSorting="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" VerticalAlign="Top" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnWarning" runat="server" CausesValidation="False">
												<img src="../images/seru.gif" border="0"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Forum.Title" HeaderText="Topik">
										<HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<u>
												<asp:LinkButton id="lbtnTopic" runat="server" CausesValidation="False" CommandName="Topic"></asp:LinkButton></u><br>
											<asp:Label id="lblDescription" runat="server"></asp:Label>
											<asp:Label id="lblCreatedBy" runat="server"></asp:Label><br>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Terakhir Posting" SortExpression="LastPostDate">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblLastPostingDate" runat="server"></asp:Label><br>
											<asp:Label id="lblLastPostingBy" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tanggapan">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblTanggapan" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
				<TR>
					<TD height="40"></TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
