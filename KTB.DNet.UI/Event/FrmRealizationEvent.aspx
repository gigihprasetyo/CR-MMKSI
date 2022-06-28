<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmRealizationEvent.aspx.vb" Inherits="FrmRealizationEvent" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmRealizationEvent</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function ShowPopUpEventInfo()
		{
			showPopUp('../PopUp/PopUpEventInfo.aspx','',500,760,Event);
		}
		function Event(selectedCode)
		{
			var tempParam = selectedCode.split(';');
			var txtEventID = document.getElementById("txtID");
				
			if(navigator.appName == "Microsoft Internet Explorer")
			{
			txtEventID.innerText = tempParam[0];
			}
			else
			{
			txtEventID.value = tempParam[0];
			}
			
			
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 19px">EVENT - Realisasi EVENT</td>
				</tr>
				<tr>
					<td style="HEIGHT: 2px" background="../images/bg_hor.gif" height="2"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="1"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 93px">
						<table cellSpacing="1" cellPadding="2" width="100%" border="0" id="Table2">
							<TR>
								<td class="titleField" width="20%">No Pengajuan Event</td>
								<TD width="1%">:</TD>
								<TD width="69%"><asp:textbox id="txtID" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtID','<>?*%$;')"
										runat="server"></asp:textbox>&nbsp;<asp:button id="btnCari" runat="server" Text="Cari"></asp:button></TD>
							</TR>
							<TR>
								<td class="titleField" width="20%">Dealer</td>
								<TD width="1%">:</TD>
								<TD width="69%"><asp:label id="lblDealer" Runat="server"></asp:label>&nbsp;/&nbsp;
									<asp:label id="lblDealerName" runat="server"></asp:label><asp:label id="lblDealerID" Runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px" width="20%">Tanggal Pelaksanaan</td>
								<TD style="HEIGHT: 19px" width="1%">:</TD>
								<TD style="HEIGHT: 19px" width="69%">
									<table id="Table3">
										<tr>
											<td><cc1:inticalendar id="icStartDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>s/d</td>
											<td><cc1:inticalendar id="icEndDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 19px" width="20%">Tempat Acara</TD>
								<TD style="HEIGHT: 19px" width="1%">:</TD>
								<TD style="HEIGHT: 19px" width="69%">
									<asp:TextBox id="txtTempatAcara" runat="server" Width="224px"></asp:TextBox></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 18px" width="20%">Jumlah Undangan</td>
								<TD style="HEIGHT: 18px" width="1%">:</TD>
								<TD style="HEIGHT: 18px" width="69%"><asp:textbox onkeypress="return numericOnlyUniv(event)" id="txtRealNumOfInvitation" onkeyup="pic(this,this.value,'999999999','N')"
										Runat="server" CssClass="textRight"></asp:textbox></TD>
							</TR>
							<TR>
								<td class="titleField" width="20%">Jumlah Peserta</td>
								<TD width="1%">:</TD>
								<TD width="69%"><asp:textbox onkeypress="return numericOnlyUniv(event)" id="txtRealNumOfParticipants" onkeyup="pic(this,this.value,'999999999','N')"
										Runat="server" CssClass="textRight"></asp:textbox></TD>
							</TR>
							<TR>
								<td class="titleField" vAlign="top" width="20%">Jenis Kendaraan Display</td>
								<TD vAlign="top" width="1%">:</TD>
								<TD width="69%">
									<div id="div1" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 200px"><asp:datagrid id="dtgEventSales" runat="server" CellSpacing="1" AllowSorting="True" ShowFooter="True"
											Width="100%" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#efefef"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblNo" runat="server" text= '<%# container.itemindex+1 %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="VechileType.Description" HeaderText="Jenis Kendaraan">
													<HeaderStyle Width="20%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=Label3 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Description") %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Left" VerticalAlign="Middle"></FooterStyle>
													<FooterTemplate>
														<asp:DropDownList ID="ddlIVehicleDesc" Runat="server"></asp:DropDownList>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:DropDownList ID="ddlEVehicleDesc" Runat="server"></asp:DropDownList>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Type Kendaraan">
													<HeaderStyle Width="20%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle Width="20%" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Category.CategoryCode") %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle Width="20%" HorizontalAlign="Center" VerticalAlign="Middle"></FooterStyle>
													<FooterTemplate>
														<asp:Label id="lblFType" runat="server"></asp:Label>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:Label id="lblEType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Category.CategoryCode") %>'>
														</asp:Label>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Qty" HeaderText="Hasil Penjualan">
													<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle Width="10%" HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblQty" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Qty") %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle Width="10%" HorizontalAlign="Right" VerticalAlign="Middle"></FooterStyle>
													<FooterTemplate>
														<asp:TextBox onkeypress="return numericOnlyUniv(event)" id="txtFQty" runat="server" CssClass="textRight"
															MaxLength="9"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox CssClass="textRight" onkeypress="return numericOnlyUniv(event)" id="txtEQty" MaxLength="9" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Qty") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="aksi">
													<HeaderStyle HorizontalAlign="Center" Width="20%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle Width="20%" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnEdit" CausesValidation="False" CommandName="edit" text="Ubah" Runat="server">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDelete" CausesValidation="False" CommandName="delete" text="Hapus" Runat="server">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
													</ItemTemplate>
													<FooterStyle Width="20%" HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:LinkButton id="lbtnAdd" tabIndex="40" CommandName="add" text="Tambah" Runat="server">
															<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:LinkButton id="lbtnSave" tabIndex="49" CommandName="save" text="Simpan" Runat="server">
															<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
														<asp:LinkButton id="lbtnCancel" tabIndex="50" CommandName="cancel" text="Batal" Runat="server">
															<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
													</EditItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<td class="titleField" width="20%">Aktual Biaya Pengajuan</td>
								<TD width="1%">:</TD>
								<TD width="69%"><asp:textbox onkeypress="return numericOnlyUniv(event)" id="txtRealTotalCost" onkeyup="pic(this,this.value,'999999999','N')"
										Runat="server" CssClass="textRight"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%">Aktual Biaya Persetujuan</TD>
								<TD width="1%">:</TD>
								<TD width="69%">
									<asp:TextBox id="txtRealApprovalCost" onkeypress="return numericOnlyUniv(event)"  onkeyup="pic(this,this.value,'999999999','N')" runat="server"></asp:TextBox></TD>
							</TR>
							<TR valign=top>
								<td class="titleField" width="20%">Upload Detail Biaya</td>
								<TD width="1%">:</TD>
								<TD width="69%"><INPUT onkeypress="return false;" id="fileRealCostDetail" style="WIDTH: 320px; HEIGHT: 20px"
										type="file" size="34" name="fileBiaya" runat="server">
									<asp:button id="btnUploadRealCostDetail" runat="server" Text="Upload" Enabled="False"></asp:button><br>
									<asp:textbox id="txtCostDetail" runat="server" Width="432px" ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR valign=top>
								<td class="titleField" width="20%">Upload Video Streaming</td>
								<TD width="1%">:</TD>
								<TD width="69%"><INPUT onkeypress="return false;" id="fileRealVideo" style="WIDTH: 320px; HEIGHT: 20px"
										type="file" size="34" name="fileVideo" runat="server">
									<asp:button id="btnUploadRealVideo" runat="server" Text="Upload" Enabled="False"></asp:button><br>
									<asp:textbox id="txtVideo" runat="server" Width="432px" ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR valign=top>
								<td class="titleField" width="20%">Upload Material Promotion</td>
								<TD width="1%">:</TD>
								<TD width="69%"><INPUT onkeypress="return false;" id="fileRealMatPromo" style="WIDTH: 320px; HEIGHT: 20px"
										type="file" size="34" name="fileMaterial" runat="server">
									<asp:button id="btnUploadRealMatPromo" runat="server" Text="Upload" Enabled="False"></asp:button><br>
									<asp:textbox id="txtMatPromo" runat="server" Width="432px" ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR valign="top">
								<td class="titleField" width="30%">Komentar Dealer</td>
								<TD width="1%">:</TD>
								<TD width="69%"><asp:textbox id="txtRealComment" Runat="server" Width="312px" TextMode="MultiLine" Rows="4"></asp:textbox></TD>
							</TR>
							<tr>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td></td>
								<td></td>
								<td><asp:button id="btnSimpan" runat="server" Text="Simpan" Width="64px"></asp:button><asp:button id="btnCancel" runat="server" Text="Batal"></asp:button>
									<asp:button id="btnBack" runat="server" Text="Kembali" Visible="False"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
			</TABLE>
		</form>
		</TR></TABLE></TR></TABLE></FORM>
	</body>
</HTML>
