<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpUniform.aspx.vb" Inherits="PopUpUniform" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>PopUpUniform</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
  		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<base target="_self">
		
	</HEAD>
	
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">SALESMAN UNIFORM - Panduan Ukuran 
						Seragam</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%"></TD>
								<TD width="1%"></TD>
								<TD width="25%"></TD>
								<TD class="titleField" width="20%"></TD>
								<TD width="1%"></TD>
								<TD width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Kode Seragam</TD>
								<TD width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD width="25%">
<asp:label id=lblUniformCode runat="server" Width="136px"></asp:label></TD>
								<TD class="titleField" width="20%"></TD>
								<TD width="1%"></TD>
								<TD width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px" width="24%">Keterangan</TD>
								<TD width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD width="25%"><asp:label id="lblKeterangan" runat="server"></asp:label></TD>
								<TD class="titleField" width="20%"></TD>
								<TD width="1%"></TD>
								<TD width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<td valign="top">
									<DIV id="divPhoto" style="OVERFLOW: auto; WIDTH: 184px; HEIGHT: 295px" align="center">
										<asp:image id="photoView" runat="server" Width="158px"></asp:image></DIV>
								</td>
								<TD vAlign="top" colSpan="6" width="70%">
									<div id="div1" style="OVERFLOW: auto">
										<asp:datagrid id="dtgUniform" runat="server" Width="100%" PageSize="15" GridLines="None"
											CellPadding="3" BackColor="#CDCDCD" AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px"
											BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True">
											<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE" ></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No.">
													<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Uraian">
													<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="S">
													<HeaderStyle Width="8%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblS" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SSize") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="M">
													<HeaderStyle Width="8%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblM" Text='<%# DataBinder.Eval(Container, "DataItem.MSize") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="L">
													<HeaderStyle Width="8%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblL" Text='<%# DataBinder.Eval(Container, "DataItem.LSize") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="XL">
													<HeaderStyle Width="8%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblXL" Text='<%# DataBinder.Eval(Container, "DataItem.XLSize") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
										</asp:datagrid>
									</div>
									<P>
										<asp:Button id="btnClose" runat="server" Text="Kembali"></asp:Button></P>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
