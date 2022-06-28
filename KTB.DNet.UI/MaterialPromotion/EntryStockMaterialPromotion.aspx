<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EntryStockMaterialPromotion.aspx.vb" Inherits="EntryStockMaterialPromotion" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EntryStockMaterialPromotion</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">

		function ShowPPMPMaster() {
			showPopUp('../PopUp/PopUpMPMaster.aspx','',600,600,MaterialPromotion);
		}
		
		function MaterialPromotion(selectedMP)
		{				
			var splited = selectedMP.split(';');
			var txtMPMaster = document.getElementById("txtKodeBarang");
			var txtMPMaster2= document.getElementById("txtNmBarang");
			txtMPMaster.value = splited[0];
			txtMPMaster2.value = splited[1];
			if(navigator.appName == "Microsoft Internet Explorer")
			{
				txtMPMaster.focus();
				txtMPMaster.blur();
			}
			else
			{
				txtMPMaster.onchange();
			}
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">MATERIAL PROMOSI - Stock Material Promotion</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</TABLE>
			<table>
				<tr>
					<td class="titleField" width="24%">Tipe Adjustment</td>
					<TD style="HEIGHT: 17px" width="1%">:</TD>
					<td style="WIDTH: 223px; HEIGHT: 17px" width="223"><asp:dropdownlist id="ddlAdjType" runat="server"></asp:dropdownlist></td>
					<td vAlign="top" rowSpan="5">
						<table border="0">
							<tr vAlign="top">
								<TD class="titleField" width="24%">Penjelasan</TD>
								<td style="HEIGHT: 17px" width="1%">:</td>
								<td style="HEIGHT: 17px" width="75%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtPenjelasan" onblur="omitSomeCharacter('txtPenjelasan','<>?*%$;')"
										runat="server" Height="88px" TextMode="MultiLine" Width="216px"></asp:textbox></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD class="titleField" style="WIDTH: 130px">Kode&nbsp;Barang</TD>
					<TD>:</TD>
					<td style="WIDTH: 223px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtKodeBarang" onblur="omitSomeCharacter('txtKodeBarang','<>?*%$;')"
							runat="server" Width="192px"></asp:textbox><asp:label id="lblPopUpMPMaster" runat="server" width="16px"><img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtKodeBarang" ErrorMessage="*"></asp:requiredfieldvalidator></td>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 130px; HEIGHT: 28px">Nama Barang</TD>
					<TD style="HEIGHT: 28px">:</TD>
					<td style="WIDTH: 223px; HEIGHT: 28px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtNmBarang" onblur="omitSomeCharacter('txtNmBarang','<>?*%$;')"
							runat="server" Width="192px"  ></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ControlToValidate="txtNmBarang" ErrorMessage="*"></asp:requiredfieldvalidator></td>
				<TR>
					<TD class="titleField" style="WIDTH: 125px">Jumlah</TD>
					<TD>:</TD>
					<td style="WIDTH: 223px"><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtJumlah" onblur="NumOnlyBlurWithOnGridTxt(this,'');"
							onkeyup="pic(this,this.value,'9999999999','N')" runat="server" Width="192px" MaxLength="9"></asp:textbox></td>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 125px">Keterangan</TD>
					<TD>:</TD>
					<td style="WIDTH: 223px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtKeterangan" onblur="omitSomeCharacter('txtKeterangan','<>?*%$;')"
							runat="server" Width="192px" ReadOnly="True" MaxLength="30"></asp:textbox></td>
				</TR>
				<tr>
					<td align="center" colSpan="4"><asp:button id="btnCari" runat="server" Width="64px" CausesValidation="False" Text="Cari"></asp:button><asp:button id="btnBaru" runat="server" Width="60px" Text="Baru"></asp:button><asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button></td>
				</tr>
			</table>
			<table cellSpacing="1" cellPadding="0" width="100%">
				<tr>
					<td>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dtgMPStock" runat="server" Width="100%" PageSize="25" AllowSorting="True" AutoGenerateColumns="False"
								BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#E0E0E0" CellPadding="3" GridLines="Horizontal" CellSpacing="1" AllowCustomPaging="True"
								AllowPaging="True">
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="WhiteSmoke" BackColor="OrangeRed"></HeaderStyle>
								<Columns>
									<asp:BoundColumn HeaderText="No">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="GoodNo" HeaderText="Kode&#160;Barang">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblNoBrg runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GoodNo") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Name" HeaderText="Nama Barang">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNamaBrg" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Unit" HeaderText="Satuan">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblSatuan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Unit") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Stock" HeaderText="Stok">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStock" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Stock"),"#,##0") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnHistory" runat="server" Width="20px" CausesValidation="False" Text="Lihat Catatan Harga"
												CommandName="vHistory">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:LinkButton>
											<asp:LinkButton id="lbtnView" runat="server" Width="20px" CausesValidation="False" Text="Lihat"
												CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" CausesValidation="False" Text="Edit" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Edit"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
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
