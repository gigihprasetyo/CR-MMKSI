<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmUserActivationCode.aspx.vb" Inherits="FrmUserActivationCode"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmUserGroup</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">

			function ShowPPDealerSelection()
			{
				showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = selectedDealer;			
			}

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">ADMIN SISTEM - User Activation Code</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td vAlign="top" align="left">
						<table cellSpacing="1" cellPadding="2" width="100%" border="0">
							<tr>
								<td class="titleField" width="24%">Kode Dealer</td>
								<td width="1%">:</td>
								<td width="75%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server" Width="176px"></asp:textbox><asp:label id="lblPopUpDealer" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></td>
							</tr>
							<tr>
								<td class="titleField" width="24%">Nama User</td>
								<td width="1%">:</td>
								<td width="75%"><asp:textbox id="txtUserName" runat="server" Width="192px"></asp:textbox></td>
							</tr>
							<tr>
								<td class="titleField" width="24%">Nama Awal</td>
								<td width="1%">:</td>
								<td width="75%"><asp:textbox id="txtFirstName" runat="server" Width="192px"></asp:textbox></td>
							</tr>
							<tr>
								<td class="titleField" width="24%">Nama Akhir</td>
								<td width="1%">:</td>
								<td width="75%"><asp:textbox id="txtLastName" runat="server" Width="192px"></asp:textbox></td>
							</tr>
							<tr>
								<td class="titleField" width="24%">Email</td>
								<td width="1%">:</td>
								<td width="75%"><asp:textbox id="txtEmail" runat="server" Width="192px"></asp:textbox></td>
							</tr>
							<tr>
								<td class="titleField" width="24%">No. Handphone</td>
								<td width="1%">:</td>
								<td width="75%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtHandphone" onblur="omitSomeCharacter('txtDeskripsi','<>?*%$;')"
										runat="server" Width="192px"></asp:textbox></td>
							</tr>
							<tr>
								<td width="24%"></td>
								<td width="1%"></td>
								<td width="75%"><asp:button id="btnCari" runat="server" Width="48px" Text="Cari" CausesValidation="False"></asp:button><asp:button id="btnBatal" Width="48px" Runat="server" Text="Batal"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<table cellSpacing="1" cellPadding="0" width="100%">
							<tr>
								<td>
									<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 360px">
										<asp:datagrid id="dgUserList" runat="server" Width="100%" AllowPaging="True" AllowCustomPaging="True"
											CellSpacing="1" GridLines="Horizontal" CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px"
											BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" PageSize="25">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="WhiteSmoke" BackColor="OrangeRed"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn Visible="False" HeaderText="ID">
													<ItemTemplate>
														<asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserInfo.ID") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="0%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="true" HeaderText="Kode Dealer" SortExpression="UserInfo.Dealer.DealerCode">
													<HeaderStyle Width="7%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserInfo.Dealer.DealerCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="true" HeaderText="Nama Dealer" SortExpression="UserInfo.Dealer.DealerName">
													<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserInfo.Dealer.DealerName") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="true" HeaderText="Nama User" SortExpression="UserInfo.UserName">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserInfo.UserName") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="true" HeaderText="Nama Awal" SortExpression="UserInfo.FirstName">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserInfo.FirstName") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="true" HeaderText="Nama Akhir" SortExpression="UserInfo.LastName">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserInfo.LastName") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="true" HeaderText="Handphone" SortExpression="UserInfo.HandPhone">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserInfo.Handphone") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="true" HeaderText="Email" SortExpression="UserInfo.Email">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserInfo.Email") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="ActivationCode" SortExpression="ActivationCode" HeaderText="Kode Aktivasi">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Aksi" Visible="False">
													<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
															CommandName="View">
															<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" ForeColor="#404040" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></DIV>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
