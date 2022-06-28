<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmAccessRoles.aspx.vb" Inherits="FrmAccessRoles" smartNavigation="False"%>
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
		var senderpopup;
		function ShowPPDealerSelection(sender)
		{
			senderpopup=sender.id;
			showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx?x=Territory','',500,760,DealerSelection);
		}

		function DealerSelection(selectedDealer)
		{
			if (senderpopup=="imgassigned")
			{
			var txtDealer= document.getElementById("txtOrganisasiAssigned");
			}
			else
			{
			var txtDealer= document.getElementById("txtOrganisasi");
			}
			txtDealer.value = selectedDealer;				
		}
		
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
					<td class="titlePage">TOOLS REPORT - Daftar User</td>
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
								<td class="titleField" width="24%">Report</td>
								<td width="1%">:</td>
								<td width="75%"><asp:label id="lblCode" runat="server" Width="264px"></asp:label></td>
							</tr>
							<tr>
								<td class="titleField" width="24%">Nama File</td>
								<td width="1%">:</td>
								<td width="75%"><asp:label id="lblDescription" runat="server" Width="272px"></asp:label></td>
							</tr>
							<tr>
								<td class="titleField" width="24%"></td>
								<td width="1%"></td>
								<td width="75%"><asp:button id="btnBack" Width="72px" Text="Kembali" Runat="server"></asp:button></td>
							</tr>
						</table>
						<br>
					</td>
				</tr>
				<TR>
					<TD class="titleField" vAlign="top" align="left">User Authorized
						<table cellSpacing="1" cellPadding="0" width="100%">
							<tr>
								<td>
									<DIV id="div2" style="OVERFLOW: auto; HEIGHT: 160px"><asp:datagrid id="dgMember" runat="server" Width="100%" PageSize="50" AllowSorting="True" AutoGenerateColumns="False"
											BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#E0E0E0" CellPadding="3" GridLines="Horizontal" CellSpacing="1" AllowCustomPaging="True"
											AllowPaging="True">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="WhiteSmoke" BackColor="OrangeRed"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn Visible="False" HeaderText="ID">
													<ItemTemplate>
														<asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblNo" runat="server" text= '<%# container.itemindex+1 %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="UserRole.UserInfo.Dealer.DealerCode" HeaderText="Dealer">
													<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="Label2" runat="server" text= '<%# DataBinder.Eval(Container, "DataItem.UserRole.UserInfo.Dealer.DealerCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="UserRole.UserInfo.UserName" HeaderText="User ID">
													<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="Label5" runat="server" text= '<%# DataBinder.Eval(Container, "DataItem.UserRole.UserInfo.UserName") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="UserRole.UserInfo.JobPosition.Description" HeaderText="Posisi/Jabatan">
													<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="Label7" runat="server" text= '<%# DataBinder.Eval(Container, "DataItem.UserRole.UserInfo.JobPosition.Description") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="UserRole.UserInfo.FirstName" HeaderText="First Name">
													<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="Label8" runat="server" text= '<%# DataBinder.Eval(Container, "DataItem.UserRole.UserInfo.FirstName") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="UserRole.UserInfo.LastName" HeaderText="Last Name">
													<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="Label9" runat="server" text= '<%# DataBinder.Eval(Container, "DataItem.UserRole.UserInfo.LastName") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Aksi">
													<HeaderStyle width="105" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="Linkbutton1" runat="server" Width="20px" Text="Hapus" CausesValidation="False" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID")%>' >
															<img src="../images/trash.gif" border="0" alt="Hapus" OnClick="return confirm('Yakin ingin menghapus data ini?');"></asp:LinkButton>
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
				<tr>
					<td>
						<table cellSpacing="1" cellPadding="2" width="100%" border="0">
							<tr>
								<td class="titleField" width="24%">Organisasi</td>
								<td width="1%">:</td>
								<td width="75%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtOrganisasi" onblur="omitSomeCharacter('txtDeskripsi','<>?*%$;')"
										runat="server" Width="192px"></asp:textbox><asp:label id="lblDealer" runat="server" width="16px">
										<img id="imgchoose" style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"
											onclick="ShowPPDealerSelection(this)"></asp:label></td>
							</tr>
							<tr>
								<td class="titleField" width="24%">Nama User</td>
								<td width="1%">:</td>
								<td width="75%">&nbsp;<asp:TextBox ID="txtUserName" Runat="server"></asp:TextBox></td>
							</tr>
							<tr>
								<td class="titleField" width="24%"></td>
								<td width="1%"></td>
								<td width="75%">&nbsp;
									<asp:button id="btnSearch" Width="72px" Text="Cari" Runat="server"></asp:button></td>
							</tr>
							<tr>
								<td align="center" colSpan="3">
									<table cellSpacing="1" cellPadding="0" width="100%">
										<tr>
											<td>
												<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 160px"><asp:datagrid id="dgUser" runat="server" Width="100%" PageSize="50" AllowSorting="True" AutoGenerateColumns="False"
														BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#E0E0E0" CellPadding="3" GridLines="Horizontal" CellSpacing="1" AllowCustomPaging="True"
														AllowPaging="True">
														<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
														<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
														<ItemStyle BackColor="White"></ItemStyle>
														<HeaderStyle Font-Bold="True" ForeColor="WhiteSmoke" BackColor="OrangeRed"></HeaderStyle>
														<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
														<Columns>
															<asp:TemplateColumn Visible="False" HeaderText="ID">
																<ItemTemplate>
																	<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn>
																<HeaderStyle Width="5%" CssClass="titleTableGeneral" HorizontalAlign="Center"></HeaderStyle>
																<HeaderTemplate>
																	<input id="chkAllItems" type="checkbox" onclick="CheckAll('chk',
														document.forms[0].chkAllItems.checked)" />
																</HeaderTemplate>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:CheckBox Runat="server" ID="chk"></asp:CheckBox>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="UserInfo.Dealer.DealerCode" HeaderText="Dealer">
																<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label id="Label3" runat="server" text= '<%# DataBinder.Eval(Container, "DataItem.UserInfo.Dealer.DealerCode") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="Role.RoleName" HeaderText="Role">
																<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label id="Label12" runat="server" text= '<%# DataBinder.Eval(Container, "DataItem.Role.RoleName") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="Role.Description" HeaderText="Description">
																<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label id="Label13" runat="server" text= '<%# DataBinder.Eval(Container, "DataItem.Role.Description") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="UserInfo.UserName" HeaderText="User ID">
																<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label id="Label4" runat="server" text= '<%# DataBinder.Eval(Container, "DataItem.UserInfo.UserName") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="UserInfo.JobPosition.Description" HeaderText="Posisi/Jabatan">
																<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label id="Label6" runat="server" text= '<%# DataBinder.Eval(Container, "DataItem.UserInfo.JobPosition.Description") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="UserInfo.FirstName" HeaderText="First Name">
																<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label id="Label10" runat="server" text= '<%# DataBinder.Eval(Container, "DataItem.UserInfo.FirstName") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="UserInfo.LastName" HeaderText="Last Name">
																<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label id="Label11" runat="server" text= '<%# DataBinder.Eval(Container, "DataItem.UserInfo.LastName") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
														</Columns>
														<PagerStyle HorizontalAlign="Right" ForeColor="#404040" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
													</asp:datagrid></DIV>
											</td>
										</tr>
									</table>
									<asp:button id="btnSimpan" Width="72px" Text="Simpan" Runat="server"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
