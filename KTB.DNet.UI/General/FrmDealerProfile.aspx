<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDealerProfile.aspx.vb" Inherits="FrmDealerProfile" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>DEALER - Dealer Maintenance</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function ShowPPDealerSelection()
		{
			showPopUp('../PopUp/PopUpSelectingDealer.aspx?multi=true','',600,600,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			var txtDealerCodeSelection = document.getElementById("txtDealerCode");
			var arrValue = selectedDealer.split(';');
			
			//alert(txtDealerCodeSelection);
			//alert(arrValue[0]);
			
			/* melakukan postback , parameter dealer code, untuk retrieve data lainnya */
			window.location.href='../General/FrmDealerProfile.aspx?DealerCode=' + arrValue[0];
			
			/*alert(arrValue[2]);*/
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">DEALER&nbsp;-
						<asp:label id="lblTitle" runat="server">Dealer Profile</asp:label></td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="28%">Kode Dealer</TD>
								<td style="WIDTH: 85px" width="85">:
								</td>
								<TD width="71%"><asp:textbox id="txtDealerCode" onblur="HtmlCharBlur(txtDealerCode)" runat="server" MaxLength="6"
										Width="112px"></asp:textbox>&nbsp;
									<asp:label id="lblPopUpDealer" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label>&nbsp;
								</TD>
							</TR>
							<TR>
								<TD class="titleField">Nama Dealer</TD>
								<td style="WIDTH: 85px" width="85">:
								</td>
								<TD><asp:label id="lblDealerName" runat="server" Width="352px"></asp:label></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField">Alamat</TD>
								<td style="WIDTH: 85px" width="85">:
								</td>
								<TD><asp:label id="lblAddress" runat="server" Width="352px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Kota</TD>
								<td style="WIDTH: 85px" width="85">:
								</td>
								<TD><asp:label id="lblCity" runat="server" Width="352px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Area</TD>
								<td style="WIDTH: 85px" width="85">:
								</td>
								<TD><asp:label id="lblArea" runat="server" Width="352px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Group</TD>
								<td style="WIDTH: 85px" width="85">:
								</td>
								<TD><asp:label id="lblGroup" runat="server" Width="352px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Telepon</TD>
								<td style="WIDTH: 85px" width="85">:
								</td>
								<TD><asp:label id="lblTelephone" runat="server" Width="352px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 26px">Fax</TD>
								<td style="WIDTH: 85px" width="85">:
								</td>
								<TD style="HEIGHT: 26px"><asp:label id="lblFax" runat="server" Width="352px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 26px">Email</TD>
								<td style="WIDTH: 85px" width="85">:
								</td>
								<TD style="HEIGHT: 26px"><asp:label id="lblEmail" runat="server" Width="352px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 25px">Status Dealer</TD>
								<td style="WIDTH: 85px; HEIGHT: 25px" width="85">:
								</td>
								<TD style="HEIGHT: 25px"><asp:label id="lblStatus" runat="server" Width="144px"></asp:label><asp:label id="lblStatusAdd" runat="server" Width="144px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 26px">Klasifikasi Dealer</TD>
								<td style="WIDTH: 85px; HEIGHT: 26px" width="85">:
								</td>
								<TD style="HEIGHT: 26px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtClassification" onblur="omitSomeCharacter('txtClassification','<>?*%$;')"
										runat="server" MaxLength="50" Width="112px"></asp:textbox>
									<asp:Label id="lblClasification" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 26px">Tahun Berdiri</TD>
								<td style="WIDTH: 85px" width="85">:
								</td>
								<TD style="HEIGHT: 26px"><asp:textbox id="txtHeldYear" onkeypress="return NumericOnlyWith(event,'');" onblur="NumOnlyBlurWithOnGridTxt(this,'');"
										runat="server" MaxLength="4" Width="112px"></asp:textbox>
									<asp:Label id="lblHeldYear" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 26px"></TD>
								<td style="WIDTH: 85px" width="85">&nbsp;
								</td>
								<TD style="HEIGHT: 26px"></TD>
							</TR>
							<TR>
								<TD class="titleField" colSpan="3"><asp:datagrid id="dgDealerProfilePhoto" runat="server" Width="100%" AllowPaging="True" PageSize="25"
										AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1"
										BorderWidth="0px" CellPadding="3" ShowFooter="True">
										<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
										<ItemStyle BackColor="White"></ItemStyle>
										<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
										<FooterStyle BackColor="#efefef"></FooterStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="No">
												<HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="File Name">
												<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
												<ItemTemplate>
													<asp:Label ID="lblInitialFileName" Runat="server"></asp:Label>
												</ItemTemplate>
												<FooterTemplate>
													<input type="file" id="txtAddInitialFileName" runat="server"> Ukuran Gambar 250 
													X 250 pixel, Berat file Maksimal 500 kb
												</FooterTemplate>
												<EditItemTemplate>
													<input type="file" id="txtEditInitialFileName" runat="server">
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn>
												<HeaderStyle Width="10%" CssClass="titleTableRsd"></HeaderStyle>
												<ItemStyle Wrap="False"></ItemStyle>
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
									</asp:datagrid></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD colSpan="2"></TD>
							</TR>
							<TR>
								<TD class="titleField" colspan="3"><asp:panel id="PnlManajemen" runat="server">Profile</asp:panel></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblUploadShowroom" runat="server">Upload File Dealer Identity</asp:label></TD>
								<TD></TD>
								<TD><input id="txtShowroomFile" type="file" name="File1" runat="server"></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblStrukturOrg" runat="server" Visible="False">Upload File Struktur Organisasi</asp:label></TD>
								<TD></TD>
								<TD><input id="txtStuctureFile" type="file" runat="server"></TD>
							</TR>
							<TR valign="top">
								<TD class="titleField"><asp:label id="lblUploadSalesForce" runat="server" Visible="False">Upload File Sales Force</asp:label></TD>
								<TD></TD>
								<TD><input id="txtSalesForceFile" type="file" name="File1" runat="server" visible="False"></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp;</TD>
							</TR>
						</TABLE>
						<asp:validationsummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"
							DisplayMode="SingleParagraph"></asp:validationsummary></TD>
				</TR>
				<TR>
				</TR>
			</TABLE>
			</TD></TR><tr>
				<td><asp:button id="btnSave" runat="server" Width="60px" Text="Simpan"></asp:button>
					<asp:button id="btnUpdate" runat="server" Text="Ubah"></asp:button>
					<asp:button id="btnBack" runat="server" Text="Kembali" CausesValidation="False"></asp:button></td>
			</tr>
			</TABLE></form>
	</body>
</HTML>
