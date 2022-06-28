<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ListMaterialPromotionRequest.aspx.vb" Inherits="ListMaterialPromotionRequest" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>ListMaterialPromotionRequest</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
		function ShowPPDealerSelection()
		{
			showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			var txtDealer = document.getElementById("txtDealerCode");
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
				<TBODY>
					<tr>
						<td class="titlePage" style="HEIGHT: 31px">MATERIAL PROMOSI&nbsp;- Daftar 
							Permintaan Material Promosi</td>
					</tr>
					<tr>
						<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
					</tr>
					<tr>
						<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
					</tr>
					<tr>
						<td style="HEIGHT: 93px">
							<table cellSpacing="1" cellPadding="2" width="100%" border="0">
								<TR>
									<td class="titleField" style="HEIGHT: 18px" width="30%">Kode Dealer</td>
									<TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
									<TD style="HEIGHT: 18px" width="69%"><asp:textbox id="txtDealerCode" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
											onblur="omitCharsOnCompsTxt(this,'<>?*%$')" Width="312px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
											<img style="cursor:hand" alt="Klik Disini untuk memilih Nomor Permintaan" src="../images/popup.gif"
												border="0"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField" width="30%">Nomor Pesanan</TD>
									<TD style="WIDTH: 1px" width="1">:</TD>
									<TD width="69%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;','');" id="txtRequestNo" runat="server"
											Width="240px"></asp:textbox></TD>
								</TR>
								<TR valign="top">
									<td class="titleField" style="HEIGHT: 18px" width="30%">Status</td>
									<TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
									<TD style="HEIGHT: 18px" width="69%"><asp:listbox id="lstStatus" runat="server" Width="142px" SelectionMode="Multiple"></asp:listbox></TD>
								</TR>
								<TR>
									<td class="titleField" style="HEIGHT: 19px" width="30%">Status Good Issue</td>
									<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
									<TD style="HEIGHT: 19px" width="69%">
										<asp:dropdownlist id="ddlStatusGI" runat="server" Width="142px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<td class="titleField" width="30%"></td>
									<TD style="WIDTH: 1px" width="1"></TD>
									<TD width="69%"><asp:button id="btnSeacrh" runat="server" Width="64px" Text="Cari"></asp:button></TD>
								</TR>
							</table>
						</td>
					</tr>
					<TR>
						<TD valign="top"><div id="div1" style="OVERFLOW: auto; HEIGHT: 220px">
								<asp:datagrid id="dtgMaterialPromotionList" runat="server" Width="100%" AutoGenerateColumns="False"
									PageSize="25" AllowCustomPaging="True" AllowPaging="True" AllowSorting="True">
									<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
									<HeaderStyle ForeColor="white"></HeaderStyle>
									<ItemStyle BackColor="White"></ItemStyle>
									<Columns>
										<asp:TemplateColumn HeaderText="No">
											<HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<HeaderTemplate>
												<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
											</HeaderTemplate>
											<ItemTemplate>
												<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle HorizontalAlign="left"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblStatus" runat="server"></asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
											<HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:Label ID="lblDealerCode" Runat = "server" Text = '<%#DataBinder.Eval(Container,"DataItem.Dealer.DealerCode")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
											<HeaderStyle Width="30%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle HorizontalAlign="left"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblDealer" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Dealer.DealerName")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="Dealer.City.CityName" HeaderText="Kota">
											<HeaderStyle CssClass="titleTablePromo" />
											<ItemTemplate>
												<asp:Label ID="lblDealerCity" Runat = "server" Text = '<%#DataBinder.Eval(Container,"DataItem.Dealer.City.CityName")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="RequestNo" HeaderText="Nomor Permintaan">
											<HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblRequestNo" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.RequestNo")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="RequestDate" HeaderText="Tanggal Permintaan">
											<HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblRequestDate" runat="server" text='<%# Format(DataBinder.Eval(Container,"DataItem.RequestDate"),"dd/MM/yyyy") %>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:LinkButton id="lbtnView" runat="server" Text="Lihat" Width="20px" CausesValidation="False" 
 CommandName="View">
													<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
												<asp:LinkButton id="lbtnEdit" runat="server" Text="Edit" Width="20px" CausesValidation="False" CommandName="Edit">
													<img src="../images/edit.gif" border="0" alt="Edit"></asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
								</asp:datagrid></div>
						</TD>
					</TR>
					<TR>
						<TD><EM></EM></TD>
					</TR>
					<tr>
						<td><EM>&nbsp; </EM>
							<asp:label id="lblChangeStatus" runat="server" Font-Italic="True" Visible="False">Mengubah Status :</asp:label>
							<asp:dropdownlist id="ddlStatus" runat="server" Visible="False"></asp:dropdownlist>
							<asp:button id="btnProses" runat="server" Width="64px" Text="Proses" Visible="False"></asp:button></td>
					</tr>
				</TBODY>
			</TABLE>
		</form></TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
