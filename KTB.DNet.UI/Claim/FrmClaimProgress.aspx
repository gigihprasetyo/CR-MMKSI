<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmClaimProgress.aspx.vb" Inherits="FrmClaimProgress" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmClaimProgress</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">CLAIM - Parameter Claim Progress</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</TABLE>
			<table>
				<TR>
					<TD class="titleField" style="WIDTH: 130px"><FONT color="crimson"><FONT color="#000000">Progress</FONT></FONT></TD>
					<TD>:</TD>
					<td><asp:textbox CssClass="mandatory" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtProgress"
							onblur="omitSomeCharacter('txtProgress','<>?*%$;')" runat="server" Width="344px" MaxLength="40"></asp:textbox></td>
				</TR>
				<tr>
					<td></td>
					<td></td>
					<td><asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button>
						<asp:Button id="btnBack" runat="server" Text="Batal" Visible="False" Width="56px"></asp:Button></td>
				</tr>
			</table>
			<table cellSpacing="1" cellPadding="0" width="100%">
				<tr>
					<td><div id="div1" style="OVERFLOW: auto; HEIGHT: 380px"><asp:datagrid id="dtgClaimProgress" runat="server" Width="100%" AllowPaging="True" AllowCustomPaging="True"
								AllowSorting="True" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#E0E0E0" CellPadding="3"
								GridLines="Horizontal" CellSpacing="1" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="WhiteSmoke" BackColor="OrangeRed"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titletableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="Progress" HeaderText="Progress">
										<HeaderStyle CssClass="titletableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblProgress" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Progress") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="10%" CssClass="titletableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titletableParts"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
												CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" CommandName="Delete" CausesValidation="False">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" SortExpression="ID" HeaderText="ID">
										<ItemTemplate>
											<asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#404040" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</td>
				</tr>
				<tr height="40">
					<td align="center"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
