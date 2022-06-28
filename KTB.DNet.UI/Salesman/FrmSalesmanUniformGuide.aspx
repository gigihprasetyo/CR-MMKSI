<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanUniformGuide.aspx.vb" Inherits="FrmSalesmanUniformGuide" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAplikasiHeader</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		function ShowPPSalesmanUniformImage()
		{
			var ddlUnifDistributionCode = document.getElementById("ddlUnifDistributionCode");
			if (ddlUnifDistributionCode.value==-1)
			{
				alert("Silakan Pilih Kode Pesanan, terlebih dahulu");
				return;
			}
			// mengambil dropdown text dengan javascript
			var strVal = ddlUnifDistributionCode.options[ddlUnifDistributionCode.selectedIndex].text;
			showPopUp('../PopUp/PopUpSalesmanUniformImage.aspx?Distribution='+strVal,'',680,620,'');
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">SERAGAM TENAGA PENJUAL - Panduan Ukuran 
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
								<TD class="titleField" style="HEIGHT: 17px" width="24%">Kode Pesanan</TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 17px" width="25%"><asp:dropdownlist id="ddlUnifDistributionCode" runat="server" AutoPostBack="True" Width="152px"></asp:dropdownlist></TD>
								<TD class="titleField" style="HEIGHT: 17px" width="20%"></TD>
								<TD style="HEIGHT: 17px" width="1%"></TD>
								<TD style="HEIGHT: 17px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px" width="24%">Keterangan</TD>
								<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" noWrap width="25%"><asp:label id="lblKeterangan" runat="server">Keterangan</asp:label></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="20%"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 25px">
									<asp:Label id="lblBrowse" runat="server">Upload File</asp:Label></TD>
								<TD style="HEIGHT: 25px">:</TD>
								<TD style="HEIGHT: 25px"><INPUT onkeypress="return false;" id="photoSrc" type="file" size="29" name="File1" runat="server"></TD>
								<TD class="titleField" style="HEIGHT: 25px">
									<asp:button id="btnUpload" runat="server" Width="60px" Text="Upload"></asp:button></TD>
								<TD style="HEIGHT: 25px"></TD>
								<TD style="HEIGHT: 25px"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD colspan="4"><i>Ukuran Gambar 500 X 500 pixel, Berat file maksimal 500 kb</i>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR valign="top">
								<td align="center">
									<DIV id="divPhoto" style="OVERFLOW: auto; WIDTH: 184px" align="center">
										<asp:label id="lblPopImage" runat="server" Visible="False" ForeColor="blue">
											<span style="cursor:hand">Klik disini untuk memperbesar gambar</span></asp:label><br>
										<asp:image id="photoView" runat="server" Width="160px"></asp:image></DIV>
								</td>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px" DESIGNTIMEDRAGDROP="238"><asp:datagrid id="dgSalesmanUnifGuide" runat="server" GridLines="None" Width="100%" AllowPaging="True"
											PageSize="25" AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
											CellPadding="3" ShowFooter="True">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Uraian">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblDescription" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox ID="txtAddDescription" Width="100px" Runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox ID="txtEditDescription" Width="200px" Runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="S">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<ItemTemplate>
														<asp:Label ID="lblSSize" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox ID="txtAddSSize" MaxLength="3" Width="50px" Runat="server" onkeypress="return NumericOnlyWith(event,'');"
															onblur="NumOnlyBlurWithOnGridTxt(this,'');"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox ID="txtEditSSize" MaxLength="3" Width="50px" Runat="server" onkeypress="return NumericOnlyWith(event,'');"
															onblur="NumOnlyBlurWithOnGridTxt(this,'');"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="M">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<ItemTemplate>
														<asp:Label ID="lblMSize" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox ID="txtAddMSize" MaxLength="3" Width="50px" Runat="server" onkeypress="return NumericOnlyWith(event,'');"
															onblur="NumOnlyBlurWithOnGridTxt(this,'');"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox ID="txtEditMSize" MaxLength="3" Width="50px" Runat="server" onkeypress="return NumericOnlyWith(event,'');"
															onblur="NumOnlyBlurWithOnGridTxt(this,'');"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="L">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<ItemTemplate>
														<asp:Label ID="lblLSize" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox ID="txtAddLSize" MaxLength="3" Width="50px" Runat="server" onkeypress="return NumericOnlyWith(event,'');"
															onblur="NumOnlyBlurWithOnGridTxt(this,'');"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox ID="txtEditLSize" MaxLength="3" Width="50px" Runat="server" onkeypress="return NumericOnlyWith(event,'');"
															onblur="NumOnlyBlurWithOnGridTxt(this,'');"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="XL">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<ItemTemplate>
														<asp:Label ID="lblXLSize" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox ID="txtAddXLSize" MaxLength="3" Width="50px" Runat="server" onkeypress="return NumericOnlyWith(event,'');"
															onblur="NumOnlyBlurWithOnGridTxt(this,'');"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox ID="txtEditXLSize" MaxLength="3" Width="50px" Runat="server" onkeypress="return NumericOnlyWith(event,'');"
															onblur="NumOnlyBlurWithOnGridTxt(this,'');"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="XXL">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<ItemTemplate>
														<asp:Label ID="lblXXLSize" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox ID="txtAddXXLSize" MaxLength="3" Width="50px" Runat="server" onkeypress="return NumericOnlyWith(event,'');"
															onblur="NumOnlyBlurWithOnGridTxt(this,'');"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox ID="txtEditXXLSize" MaxLength="3" Width="50px" Runat="server" onkeypress="return NumericOnlyWith(event,'');"
															onblur="NumOnlyBlurWithOnGridTxt(this,'');"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False"
															CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
													</ItemTemplate>
													<FooterTemplate>
														<asp:LinkButton id="lbtnAdd" runat="server" CausesValidation="False" CommandName="Add">
															<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:LinkButton id="lbtnSave" tabIndex="40" CommandName="Save" text="Simpan" Runat="server" CausesValidation="False">
															<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
														<asp:LinkButton id="lbtnCancel" tabIndex="50" CommandName="Cancel" text="Batal" Runat="server" CausesValidation="False">
															<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
													</EditItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 8px"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
