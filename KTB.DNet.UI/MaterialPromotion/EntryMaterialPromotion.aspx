<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EntryMaterialPromotion.aspx.vb" Inherits="EntryMaterialPromotion" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EntryMaterialPromotion</title>
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
					<td class="titlePage">MATERIAL PROMOSI - Master Material Promosi</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</TABLE>
			<table cellSpacing="2" cellPadding="1" border="0">
				<tr>
					<td class="titleField" width="24%">Kode&nbsp;Barang <FONT color="crimson">*</FONT></td>
					<TD style="HEIGHT: 17px" width="1%">:</TD>
					<td style="HEIGHT: 17px" width="75%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtNoBarang" onblur="omitSomeCharacter('txtNoBarang','<>?*%$;')"
							runat="server" MaxLength="20" Width="104px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Nomor Barang harus diisi"
							ControlToValidate="txtNoBarang" Font-Bold="True">*</asp:requiredfieldvalidator></td>
				</tr>
				<TR>
					<TD class="titleField" style="WIDTH: 130px"><FONT color="crimson"><FONT color="#000000">Nama 
								Barang</FONT>*</FONT>&nbsp;</TD>
					<TD>:</TD>
					<td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtNamaBarang" onblur="omitSomeCharacter('txtNamaBarang','<>?*%$;')"
							runat="server" MaxLength="30" Width="300px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Nama Barang harus diisi"
							ControlToValidate="txtNamaBarang" Font-Bold="True">*</asp:requiredfieldvalidator></td>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 130px">Satuan</TD>
					<TD>:</TD>
					<td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtSatuan" onblur="omitSomeCharacter('txtSatuan','<>?*%$;')"
							runat="server" MaxLength="15" Width="104px"></asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" ErrorMessage="Satuan harap diisi" ControlToValidate="txtSatuan"
							Font-Bold="True">*</asp:requiredfieldvalidator></td>
				<TR>
					<TD class="titleField" style="WIDTH: 125px">Harga</TD>
					<TD>:</TD>
					<td><asp:textbox onkeypress="return NumericOnlyWith(event,'')" id="txtHarga" onkeyup="pic(this,this.value,'9999999999','N')"
							runat="server" MaxLength="13" Width="104px"></asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" ErrorMessage="Harga Barang harus diisi"
							ControlToValidate="txtHarga" Font-Bold="True">*</asp:requiredfieldvalidator></td>
				</TR>
				<TR>
					<asp:panel id="pnlDescription" Visible="False" Runat="server">
						<TD class="titleField" style="WIDTH: 125px">Keterangan</TD>
						<TD>:</TD>
						<TD>
							<asp:TextBox id="txtKeterangan" runat="server" Width="248px" MaxLength="50"></asp:TextBox></TD>
					</asp:panel></TR>
				<TR>
					<TD class="titleField" style="WIDTH: 125px">Status</TD>
					<TD>:</TD>
					<TD><asp:dropdownlist id="ddlStatus" runat="server" Width="104px"></asp:dropdownlist></TD>
				</TR>
				<tr>
					<td></td>
					<td></td>
					<td><asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button><asp:button id="btnNew" runat="server" Width="64px" Text="Baru" CausesValidation="False"></asp:button>
						<asp:Button id="btnSearch" runat="server" Width="64px" Text="Cari" CausesValidation="False"></asp:Button></td>
				</tr>
			</table>
			<table cellSpacing="1" cellPadding="0" width="100%">
				<tr>
					<td>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dtgMaterialPromotion" runat="server" Width="100%" PageSize="25" AllowPaging="True"
								AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#E0E0E0"
								CellPadding="3" GridLines="Horizontal" CellSpacing="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="WhiteSmoke" BackColor="OrangeRed"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titletablePromo"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="GoodNo" HeaderText="Kode Barang">
										<HeaderStyle Width="10%" CssClass="titletablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNoBrg" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GoodNo") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Name" HeaderText="Nama Barang">
										<HeaderStyle Width="25%" CssClass="titletablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNamaBrg" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="ProductCategory.Code" HeaderText="Produk">
										<HeaderStyle Width="25%" CssClass="titletablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblProduk" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProductCategory.Code") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Unit" HeaderText="Satuan">
										<HeaderStyle Width="10%" CssClass="titletablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblSatuan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Unit") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Price" HeaderText="Harga">
										<HeaderStyle Width="15%" CssClass="titletablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblHarga" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Price") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
										<HeaderStyle Width="15%" CssClass="titletablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titletablePromo"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnHistory" runat="server" Width="20px" Text="Lihat Catatan Harga" CausesValidation="False"
												CommandName="vHistory">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:LinkButton>
											<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
												CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False"
												CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
											<asp:LinkButton id="lbtnActive" runat="server" Width="20px" Text="Aktif" CausesValidation="False"
												CommandName="Activate">
												<img src="../images/aktif.gif" border="0" alt="Ubah ke Status Aktif"></asp:LinkButton>
											<asp:LinkButton id="lbtnNonActive" runat="server" Width="20px" Text="Non Aktif" CausesValidation="False"
												CommandName="DeActivate" Visible="False">
												<img src="../images/in-aktif.gif" border="0" alt="Ubah ke Status Tidak Aktif"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<ItemTemplate>
											<asp:Label ID="lblID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
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
