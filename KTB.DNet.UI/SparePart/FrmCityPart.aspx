<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmCityPart.aspx.vb" Inherits="FrmCityPart"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmCityPart</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="FrmCityPart" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PARTSHOP -&nbsp;Entry Daftar Kota</td>
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
								<TD class="titleField" style="WIDTH: 88px" width="88">Provinsi ID</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 262px" width="262">
									<asp:DropDownList id="ddlProvince" runat="server" AutoPostBack="True"></asp:DropDownList></TD>
							</TR>
                            <TR>
								<TD class="titleField" style="WIDTH: 88px" width="88">Kota ID</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 262px" width="262">
									<asp:DropDownList id="ddlCity" runat="server"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 88px" width="88">Kode Kota Part</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 262px" width="262">
									<asp:TextBox id="txtCityCode" runat="server" Width="200px" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
										onblur="omitSomeCharacter(this.id,'<>?*%$;')"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 88px" width="88">Nama Kota Part</TD>
								<TD width="1%">:</TD>
								<td style="WIDTH: 262px" width="262"><asp:textbox id="txtCityName" Width="200px" Runat="server" Rows="3"></asp:textbox></td>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 88px" width="88"></TD>
								<TD width="1%"></TD>
								<td style="WIDTH: 262px" width="262"><asp:button id="btnSave" runat="server" Text="Simpan" width="70px"></asp:button>&nbsp;
									<asp:button id="btnBack" runat="server" Text="Batal" width="70px"></asp:button>&nbsp;<asp:button id="btnCari" runat="server" Text="Cari" width="70px"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 390px">
							<asp:datagrid id="dgCityPart" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
								AutoGenerateColumns="False" BorderColor="Gainsboro" BorderWidth="0px" AllowPaging="True" AllowCustomPaging="True"
								BackColor="#CDCDCD" CellPadding="3" PageSize="15">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="CityCode" HeaderText="Kode">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCode" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CityCode")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="CityName" HeaderText="Kota Part">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDescription" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CityName")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Province.ProvinceName" HeaderText="Provinsi">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblProvince" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Province.ProvinceName")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="City.CityName" HeaderText="Kota">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCity" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.City.CityName")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="LastUpdateTime" HeaderText="Tanggal Update">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCreatedTime" Runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.LastUpdateTime"),"dd/MM/yyyy")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False"
												CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus" onclick="return confirm('Apakah anda akan menghapus kota ini ?')"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
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
