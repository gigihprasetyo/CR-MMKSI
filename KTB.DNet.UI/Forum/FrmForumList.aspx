<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmForumList.aspx.vb" Inherits="FrmForumList" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmForumList</title>
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
					<td class="titlePage">FORUM&nbsp;-&nbsp;
						<asp:Label id="lblTitle" runat="server">Daftar Forum</asp:Label></td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">Kategori</TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:dropdownlist id="ddlKategori" runat="server" Width="152px"></asp:dropdownlist></td>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 28px" width="24%">Title Forum</TD>
								<TD style="HEIGHT: 28px" width="1%">:</TD>
								<TD style="HEIGHT: 28px" width="75%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtTitle" onblur="omitSomeCharacter('txtTitle','<>?*%$;')"
										runat="server" Width="408px" MaxLength="40"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Kata Kunci</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtKey" onblur="omitSomeCharacter('txtKey','<>?*%$;')"
										runat="server" MaxLength="15"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 22px" width="24%">Status</TD>
								<TD style="HEIGHT: 22px" width="1%">:</TD>
								<TD style="HEIGHT: 22px" width="75%"><asp:dropdownlist id="ddlStaus" runat="server">
										<asp:ListItem Value="1">Aktif</asp:ListItem>
										<asp:ListItem Value="0">Tidak Aktif</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 34px" width="24%">Tanggal Posting</TD>
								<TD style="HEIGHT: 34px" width="1%">:</TD>
								<TD style="HEIGHT: 34px" width="300">
									<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="300" border="0">
										<TR>
											<TD style="WIDTH: 34px">&nbsp;
												<asp:checkbox id="chkTanggal" runat="server"></asp:checkbox></TD>
											<TD style="WIDTH: 125px"><cc1:inticalendar id="icTglCreate" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<TD style="WIDTH: 34px">&nbsp;s/d
											</TD>
											<TD><cc1:inticalendar id="icTglCreate2" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 26px" width="24%">Diposting&nbsp;oleh</TD>
								<TD style="HEIGHT: 26px" width="1%">:</TD>
								<td style="HEIGHT: 26px" width="75%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtCreateBy" onblur="omitSomeCharacter('txtCreateBy','<>?*%$;')"
										runat="server" MaxLength="15"></asp:textbox></td>
							</TR>
							<TR>
								<TD class="titleField" width="24%"></TD>
								<TD width="1%"></TD>
								<td width="75%"><asp:button id="btnSearch" runat="server" Text="Cari" width="60px"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 250px"><asp:datagrid id="dtgForumList" runat="server" Width="100%" PageSize="25" AllowSorting="True"
								CellSpacing="1" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" AllowPaging="True" AllowCustomPaging="True"
								BackColor="#CDCDCD" CellPadding="3" GridLines="None">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#FDF1F2" VerticalAlign="Top"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="2%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" CommandName="Edit" CausesValidation="False">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Forum" SortExpression="Title">
										<HeaderStyle Width="50%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<u>
												<asp:LinkButton id="lblTitleForum" runat="server" CommandName="Forum" CausesValidation="False"></asp:LinkButton></u><br>
											<asp:Label Runat="server" ID="lblDescription"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tanggal Create" SortExpression="CreatedTime">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblDate"></asp:Label><br>
											<asp:Label Runat="server" ID="lblBy"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Topik">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblTopic"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Posting">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblPosting"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
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
