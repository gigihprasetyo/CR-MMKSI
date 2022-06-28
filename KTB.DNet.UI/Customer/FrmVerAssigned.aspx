<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmVerAssigned.aspx.vb" Inherits="FrmVerAssigned"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmVerAssigned</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx?x=Territory','',500,760,DealerSelection);
			}

			function ShowPPDealerVerSelection()
			{
			alert("Invoke");
				showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx?x=Territory','',500,760,DealerVerSelection);
			}

			function ValidateRecord()
			{
				var haveReference =false;
				var aspCheckBoxID ='chkItemChecked';
				re = new RegExp(':' + aspCheckBoxID + '$')  

				for(i = 0; i < document.forms[0].elements.length; i++) 
				{
					elm = document.forms[0].elements[i]
					if (elm.type == 'checkbox') 
					{
						if (re.test(elm.name)) 
						{
							if (elm.checked)
							{
								if(navigator.appName == "Microsoft Internet Explorer")
								{
									if (elm.parentNode.childNodes[2].childNodes[0].checked)
									{	
										haveReference=true;
									}
								}
								else
								{
									if (elm.nextSibling.nextSibling.childNodes[0].checked)
									{	
										haveReference=true;
									}
								}
							} 
						}
					}
				}
				
				if (haveReference)
				{
					return confirm('Ada data referensi. Anda yakin untuk dilanjutkan?');
				}
				else
				{
					return true;
				}
				
				
			}

			function DealerSelection(selectedDealer)
			{
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = selectedDealer;
			}
			
			function DealerVerSelection(selectedDealer)
			{
				var txtDealerVerSelection = document.getElementById("txtKodeDealerVer");
				txtDealerVerSelection.value = selectedDealer;
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
			<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<asp:panel id="pnlSearch" Runat="server">
					<TBODY>
						<TR>
							<TD class="titlePage">KONSUMEN - Daftar Verifikasi</TD>
						</TR>
						<TR>
							<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
						</TR>
						<TR>
							<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
						</TR>
						<TR>
							<TD>
								<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
									<TR>
										<TD class="titleField">
											<asp:label id="lbl1" runat="server">Tipe Pengajuan</asp:label></TD>
										<TD>
											<asp:label id="lblColon1" runat="server">:</asp:label></TD>
										<TD class="titleField">
											<asp:dropdownlist id="ddlTipePengajuan" runat="server"></asp:dropdownlist></TD>
										<TD class="titleField">
											<asp:label id="lblCodeDealer" runat="server">Kode Dealer</asp:label></TD>
										<TD>
											<asp:label id="Label1" runat="server">:</asp:label></TD>
										<TD class="titleField">
											<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
												runat="server"></asp:textbox>
											<asp:label id="lblSearchDealer" runat="server">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
									</TR>
									<TR>
										<TD class="titleField">
											<asp:label id="Label2" runat="server">Tgl Pengajuan</asp:label></TD>
										<TD>
											<asp:label id="lblColon2" runat="server">:</asp:label></TD>
										<TD>
											<TABLE id="Table10" cellSpacing="0" cellPadding="0" border="0">
												<TR>
													<TD>
														<cc1:inticalendar id="icTglPengajuanFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
													<TD>&nbsp;s/d&nbsp;
													</TD>
													<TD>
														<cc1:inticalendar id="icTglPengajuanUntil" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
												</TR>
											</TABLE>
										</TD>
										<TD class="titleField">
											<asp:label id="Label3" runat="server">No Pengajuan</asp:label></TD>
										<TD>
											<asp:label id="lblColon3" runat="server">:</asp:label></TD>
										<TD class="titleField">
											<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtNoPengajuan" onblur="omitSomeCharacter('txtNoPengajuan','<>?*%$;')"
												runat="server"></asp:textbox>&nbsp;&nbsp;
										</TD>
									</TR>
									<TR>
										<TD class="titleField">Status</TD>
										<TD></TD>
										<TD>
											<asp:dropdownlist id="ddlStatus" runat="server"></asp:dropdownlist></TD>
										<TD class="titleField">Tipe Konsumen</TD>
											
										<TD></TD>
										<TD class="titleField"><asp:dropdownlist id="ddlCostumerType" runat="server"></asp:dropdownlist></TD>
									</TR>
                                    <TR>
										<TD></TD>
										<TD></TD>
										<TD><asp:button id="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:button></TD>
										<TD></TD>
										<TD></TD>
										<TD></TD>
									</TR>
                                    <TR>
										<TD vAlign="top" colSpan="6">
											<DIV id="div1" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 300px">
												<asp:datagrid id="dtgPengajuan" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD"
													BorderColor="Gainsboro" CellPadding="3" CellSpacing="1" AllowSorting="True" AutoGenerateColumns="False"
													DataKeyField="ID">
													<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
													<ItemStyle ForeColor="Black" VerticalAlign="Top" BackColor="#F1F6FB"></ItemStyle>
													<HeaderStyle Font-Bold="True" ForeColor="#FFFFFF" BackColor="#F28625"></HeaderStyle>
													<Columns>
														<asp:TemplateColumn>
															<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
															<HeaderTemplate>
																<asp:CheckBox ID="chkAllItems" Runat="server" />
															</HeaderTemplate>
															<ItemTemplate>
																<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
																<asp:CheckBox id="chkhavereference" runat="server" style="display:none"></asp:CheckBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:BoundColumn DataField="ID" SortExpression="ID" HeaderText="Request ID">
															<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="CustomerCode" SortExpression="CustomerCode" HeaderText="Ref Kode Plgn">
															<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="RequestNo" SortExpression="RequestNo" HeaderText="No Pengajuan">
															<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
														</asp:BoundColumn>
														<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
															<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
															<ItemTemplate>
																<asp:Label runat="server" Tooltip = '<%# DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1")  %>' Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>' ID="lblDealer" NAME="lblDealer">
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Nama" SortExpression="Name1">
															<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
															<ItemTemplate>
																<asp:Label id="lblName" runat="server"></asp:Label>
															</ItemTemplate>
															<EditItemTemplate>
																<asp:TextBox id="TextBox1" runat="server"></asp:TextBox>
															</EditItemTemplate>
														</asp:TemplateColumn>
														<asp:BoundColumn DataField="Name3" SortExpression="Name2" HeaderText="Gedung">
															<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="Alamat" SortExpression="Alamat" HeaderText="Alamat">
															<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="Kelurahan" SortExpression="Kelurahan" HeaderText="Kelurahan" Visible="False">
															<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="Kecamatan" SortExpression="Kecamatan" HeaderText="Kecamatan" Visible="False">
															<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
														</asp:BoundColumn>
														<asp:TemplateColumn SortExpression="CityID" HeaderText="Kota">
															<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
															<ItemTemplate>
																<asp:Label id="lblCity" runat="server"></asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="No. KTP">
															<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
															<ItemTemplate>
																<asp:Label id="lblKTP" runat="server"></asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:BoundColumn DataField="PostalCode" SortExpression="PostalCode" HeaderText="Kode Pos" Visible="False">
															<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
														</asp:BoundColumn>
														<asp:TemplateColumn HeaderText="Propinsi" Visible="False">
															<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
															<ItemTemplate>
																<asp:Label id="lblProvince" runat="server"></asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Tipe Konsumen" Visible="True">
															<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
															<ItemTemplate>
																<asp:Label id="lblCustumerType" runat="server"></asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn>
															<HeaderStyle HorizontalAlign="Center" Width="15px" CssClass="titleTableSales"></HeaderStyle>
															<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
															<ItemTemplate>
																<asp:LinkButton id=lbtnDetails Runat="server" CausesValidation="False" text="Lihat" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="detail">
																	<img src="../images/detail.gif" border="0" alt="Lihat Data Referensi"></asp:LinkButton>
																<asp:LinkButton id=lnkAttach Runat="server" CausesValidation="False" text="Attach" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="download">
																	<img src="../images/download.gif" border="0"></asp:LinkButton>
															</ItemTemplate>
														</asp:TemplateColumn>
													</Columns>
												</asp:datagrid></DIV>
										</TD>
									</TR>
									<TR>
										<TD vAlign="top" colSpan="6">
											<asp:Button id="btnSend" runat="server" Text="Transfer to SAP" Visible="False"></asp:Button>
											<asp:Button id="btnResend" runat="server" Text="Transfer ulang to SAP" Visible="False"></asp:Button></TD>
									</TR>
									<asp:Panel id="pnlRef" Runat="server" Visible="False">
										<TR>
											<TD class="titleField" vAlign="top" colSpan="6">DATA REFERENSI</TD>
										</TR>
										<TR>
											<TD class="titleField">Kode Konsumen</TD>
											<TD>:</TD>
											<TD class="titleField">
												<asp:TextBox id="txtKodeKonsumenVer" runat="server"></asp:TextBox></TD>
											<TD class="titleField">Kode Dealer</TD>
											<TD>:</TD>
											<TD class="titleField">
												<asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtKodeDealerVer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
													runat="server"></asp:TextBox>
												<asp:label id="lblSearchDealerVer" runat="server">
													<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
										</TR>
										<TR>
											<TD class="titleField">Nama Konsumen</TD>
											<TD>:</TD>
											<TD class="titleField">
												<asp:TextBox onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');"
													id="txtNamaKonsumenVer" onblur="omitSomeCharacterExcludeSingleQuote('txtNamaKonsumenVer','<>?*%$;');"
													runat="server"></asp:TextBox></TD>
											<TD class="titleField">Alamat</TD>
											<TD>:</TD>
											<TD class="titleField">
												<asp:TextBox id="txtAlamatVer" runat="server"></asp:TextBox></TD>
										</TR>
										<TR>
											<TD class="titleField"></TD>
											<TD>:</TD>
											<TD class="titleField"></TD>
											<TD class="titleField">Kota</TD>
											<TD>:</TD>
											<TD class="titleField">
												<asp:TextBox id="txtKotaVer" runat="server"></asp:TextBox></TD>
										</TR>
										<TR>
											<TD class="titleField"></TD>
											<TD></TD>
											<TD class="titleField">
												<asp:Button id="btnCariVer" runat="server" Width="60px" Text="Cari"></asp:Button></TD>
											<TD class="titleField"></TD>
											<TD></TD>
											<TD class="titleField"></TD>
										</TR>
										<TR>
											<TD vAlign="top" colSpan="6">
												<asp:datagrid id="dtgRef" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
													CellPadding="3" CellSpacing="1" AllowSorting="True" AutoGenerateColumns="False" DataKeyField="ID"
													Visible="False" AllowCustomPaging="True" PageSize="25" AllowPaging="True">
													<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
													<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
													<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#F28625"></HeaderStyle>
													<Columns>
														<asp:BoundColumn DataField="Code" SortExpression="Code" HeaderText="Kode Plgn">
															<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
														</asp:BoundColumn>
														<asp:TemplateColumn HeaderText="Nama1" SortExpression="Name1">
															<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
															<ItemTemplate>
																<asp:Label id=lblName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name1") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Nama2" SortExpression="Name2">
															<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
															<ItemTemplate>
																<asp:Label id="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name2") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:BoundColumn DataField="Name3" SortExpression="Name3" HeaderText="Gedung">
															<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="Alamat" SortExpression="Alamat" HeaderText="Alamat">
															<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="Kelurahan" SortExpression="Kelurahan" HeaderText="Kelurahan">
															<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="Kecamatan" SortExpression="Kecamatan" HeaderText="Kecamatan">
															<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
														</asp:BoundColumn>
														<asp:TemplateColumn SortExpression="City.CityID" HeaderText="Kota">
															<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
															<ItemTemplate>
																<asp:Label id="lblCity" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.City.CityName") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:BoundColumn DataField="PostalCode" SortExpression="PostalCode" HeaderText="Kode Pos">
															<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
														</asp:BoundColumn>
														<asp:TemplateColumn Visible="False" SortExpression="City.Province.ProvinceName" HeaderText="Propinsi">
															<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
															<ItemTemplate>
																<asp:Label id="lblProvince" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.City.Province.ProvinceName") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn>
															<HeaderStyle HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
															<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
															<ItemTemplate>
																<asp:LinkButton id=lbtnDetails Runat="server" CausesValidation="False" text="Lihat" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="detail">
																	<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
																<asp:LinkButton id=lnkAttach Runat="server" CausesValidation="False" text="Lihat" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="download">
																	<img src="../images/download.gif" border="0" alt="Attach"></asp:LinkButton>
															</ItemTemplate>
														</asp:TemplateColumn>
													</Columns>
													<PagerStyle Mode="NumericPages"></PagerStyle>
												</asp:datagrid></TD>
										</TR>
									</asp:Panel></TABLE>
							</TD>
						</TR>
				</asp:panel><asp:panel id="pnlCompare" Runat="server" Visible="False">
					<TR>
						<TD class="titlePage">VERIFIKASI DATA PELANGGAN</TD>
					</TR>
					<TR>
						<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
					</TR>
					<TR>
						<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
					</TR>
					<TR>
						<TD>
							<TABLE id="Table5" cellSpacing="1" cellPadding="2" width="100%" border="0">
								<TR>
									<TD class="titleField" width="20%">Tipe Pengajuan</TD>
									<TD width="1%">:</TD>
									<TD width="29%"><asp:label id="lblTipePengajuan" runat="server"></asp:label></TD>
									<TD class="titleField" width="20%">Kode Dealer</TD>
									<TD width="1%">:</TD>
									<TD width="29%"><asp:label id="lblKodeDealer" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">Tgl Pengajuan</TD>
									<TD>:</TD>
									<TD><asp:label id="lblTglPengajuan" runat="server"></asp:label></TD>
									<TD class="titleField">Diajukan Oleh</TD>
									<TD>:</TD>
									<TD><asp:label id="lblDiajukanOleh" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">No Pengajuan</TD>
									<TD>:</TD>
									<TD><asp:label id="lblNoPengajuan" runat="server"></asp:label></TD>
									<TD class="titleField"></TD>
									<TD></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD class="titlePage">&nbsp;</TD>
									<TD></TD>
									<TD class="titleField"></TD>
									<TD class="titlePage"></TD>
									<TD></TD>
									<TD></TD>
								</TR>
								<TR>
									<td colSpan="3">
										<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD class="titlePanel">DATA PENGAJUAN</TD>
											</TR>
											<TR>
												<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
											</TR>
										</TABLE>
									</td>
									<td colSpan="3">
										<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD class="titlePanel">DATA REFERENSI</TD>
											</TR>
											<TR>
												<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
											</TR>
										</TABLE>
									</td>
								</TR>
								<TR>
									<TD class="titleField" style="HEIGHT: 18px">Reff Kode Pelanggan</TD>
									<TD style="HEIGHT: 18px">:</TD>
									<TD class="titleField" style="HEIGHT: 18px"><asp:label id="lblKodePelanggan" runat="server" Visible="False"></asp:label></TD>
									<TD class="titleField" style="HEIGHT: 18px">Kode Pelanggan</TD>
									<TD style="HEIGHT: 18px">:</TD>
									<TD style="HEIGHT: 18px"><asp:label id="lblRefCode" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField" style="HEIGHT: 17px">Nama 1</TD>
									<TD style="HEIGHT: 17px">:</TD>
									<TD style="HEIGHT: 17px"><asp:label id="lblName1Reff" runat="server"></asp:label></TD>
									<TD class="titleField" style="HEIGHT: 17px">Nama 1</TD>
									<TD style="HEIGHT: 17px">:</TD>
									<TD style="HEIGHT: 17px"><asp:label id="lblName1" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField" style="HEIGHT: 17px">Nama 2</TD>
									<TD style="HEIGHT: 17px">:</TD>
									<TD style="HEIGHT: 17px">
										<asp:label id="lblName2Reff" runat="server"></asp:label></TD>
									<TD class="titleField" style="HEIGHT: 17px">Nama 2</TD>
									<TD style="HEIGHT: 17px">:</TD>
									<TD style="HEIGHT: 17px">
										<asp:label id="lblName2" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField" style="HEIGHT: 15px">Gedung</TD>
									<TD style="HEIGHT: 15px">:</TD>
									<TD style="HEIGHT: 15px"><asp:label id="lblGedung1" runat="server"></asp:label></TD>
									<TD class="titleField" style="HEIGHT: 15px">Gedung</TD>
									<TD style="HEIGHT: 15px">:</TD>
									<TD style="HEIGHT: 15px"><asp:label id="lblGedung2" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">Alamat</TD>
									<TD>:</TD>
									<TD><asp:label id="lblAlamat1" runat="server"></asp:label></TD>
									<TD class="titleField">Alamat</TD>
									<TD>:</TD>
									<TD><asp:label id="lblAlamat2" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">Kelurahan</TD>
									<TD>:</TD>
									<TD><asp:label id="lblKelurahan1" runat="server"></asp:label></TD>
									<TD class="titleField">Kelurahan</TD>
									<TD>:</TD>
									<TD><asp:label id="lblKelurahan2" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">Kecamatan</TD>
									<TD>:</TD>
									<TD><asp:label id="lblKecamatan1" runat="server"></asp:label></TD>
									<TD class="titleField">Kecamatan</TD>
									<TD>:</TD>
									<TD><asp:label id="lblKecamatan2" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">Kota</TD>
									<TD>:</TD>
									<TD><asp:label id="lblKota1" runat="server"></asp:label></TD>
									<TD class="titleField">Kota</TD>
									<TD>:</TD>
									<TD><asp:label id="lblKota2" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">Kode Pos</TD>
									<TD>:</TD>
									<TD><asp:label id="lblKodePos1" runat="server"></asp:label></TD>
									<TD class="titleField">Kode Pos</TD>
									<TD>:</TD>
									<TD><asp:label id="lblKodePos2" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField">Propinsi</TD>
									<TD>:</TD>
									<TD><asp:label id="lblPropinsi1" runat="server"></asp:label></TD>
									<TD class="titleField">Propinsi</TD>
									<TD>:</TD>
									<TD><asp:label id="lblPropinsi2" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titlePanel">&nbsp;</TD>
									<TD></TD>
									<TD class="titleField"></TD>
									<TD class="titleField"></TD>
									<TD></TD>
									<TD class="titleField"></TD>
								</TR>
								<TR>
									<td colSpan="6">
										<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD class="titlePanel">INFORMASI TAMBAHAN
												</TD>
											</TR>
											<TR>
												<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
											</TR>
										</TABLE>
									</td>
								</TR>
								<TR>
									<TD vAlign="top" colSpan="6">
										<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
											<TBODY>
												<TR>
													<TD vAlign="top">
														<table id="Table8" width="100%" border="0">
															<TBODY>
																<tr>
																	<td vAlign="top" width="200"><asp:panel id="Panel1" runat="server" Visible="True">
																			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="300" border="0">
																				<TR>
																					<TD vAlign="top">
																						<asp:Panel id="pnlPerorangan0" runat="server">
																							<BR>
																							<TABLE id="titlePanel1" cellSpacing="0" cellPadding="0" width="100%" border="0">
																								<TR>
																									<TD class="titlePanel"><B>PERORANGAN :</B></TD>
																								</TR>
																								<TR>
																									<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
																								</TR>
																							</TABLE>
																						</asp:Panel>
																						<asp:Panel id="pnlPerusahaan0" runat="server">
																							<BR>
																							<TABLE id="titlePanel2" cellSpacing="0" cellPadding="0" width="100%" border="0">
																								<TR>
																									<TD class="titlePanel"><B>PERUSAHAAN :</B></TD>
																								</TR>
																								<TR>
																									<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
																								</TR>
																							</TABLE>
																						</asp:Panel>
																						<asp:Panel id="PnlBUMN0" runat="server">
																							<BR>
																							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
																								<TR>
																									<TD class="titlePanel"><B>BUMN&nbsp;:</B></TD>
																								</TR>
																								<TR>
																									<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
																								</TR>
																							</TABLE>
																						</asp:Panel>
																						<asp:panel id="PnlLainnya0" runat="server">
																							<BR>
																							<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%" border="0">
																								<TR>
																									<TD class="titlePanel"><B>Lainnya&nbsp;:</B></TD>
																								</TR>
																								<TR>
																									<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
																								</TR>
																							</TABLE>
																						</asp:panel>
																						<asp:Panel id="pnlTambahan0" runat="server">
																							<BR>
																							<TABLE id="titlePanel3" cellSpacing="0" cellPadding="0" width="100%" border="0">
																								<TR>
																									<TD class="titlePanel"><B>TAMBAHAN :</B></TD>
																								</TR>
																								<TR>
																									<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
																								</TR>
																							</TABLE>
																						</asp:Panel></TD>
																				</TR>
																			</TABLE>
																		</asp:panel></td>
													</TD>
												</TR>
											</TBODY>
										</TABLE>
									</TD>
									<TD vAlign="top">
										<table id="Table9" width="100%">
											<TBODY>
												<tr>
													<td vAlign="top" width="200"><asp:panel id="Panel2" runat="server" Visible="True">
															<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="300" border="0">
																<TR>
																	<TD vAlign="top">
																		<asp:Panel id="pnlPerorangan1" runat="server">
																			<BR>
																			<TABLE id="titlePanel1" cellSpacing="0" cellPadding="0" width="100%" border="0">
																				<TR>
																					<TD class="titlePanel"><B>PERORANGAN :</B></TD>
																				</TR>
																				<TR>
																					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
																				</TR>
																			</TABLE>
																		</asp:Panel>
																		<asp:Panel id="pnlPerusahaan1" runat="server">
																			<BR>
																			<TABLE id="titlePanel2" cellSpacing="0" cellPadding="0" width="100%" border="0">
																				<TR>
																					<TD class="titlePanel"><B>PERUSAHAAN :</B></TD>
																				</TR>
																				<TR>
																					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
																				</TR>
																			</TABLE>
																		</asp:Panel>
																		<asp:Panel id="PnlBUMN1" runat="server">
																			<BR>
																			<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
																				<TR>
																					<TD class="titlePanel"><B>BUMN&nbsp;:</B></TD>
																				</TR>
																				<TR>
																					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
																				</TR>
																			</TABLE>
																		</asp:Panel>
																		<asp:panel id="PnlLainnya1" runat="server">
																			<BR>
																			<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%" border="0">
																				<TR>
																					<TD class="titlePanel"><B>Lainnya&nbsp;:</B></TD>
																				</TR>
																				<TR>
																					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
																				</TR>
																			</TABLE>
																		</asp:panel>
																		<asp:Panel id="pnlTambahan1" runat="server">
																			<BR>
																			<TABLE id="titlePanel3" cellSpacing="0" cellPadding="0" width="100%" border="0">
																				<TR>
																					<TD class="titlePanel"><B>TAMBAHAN :</B></TD>
																				</TR>
																				<TR>
																					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
																				</TR>
																			</TABLE>
																		</asp:Panel></TD>
																</TR>
															</TABLE>
														</asp:panel></td>
									</TD>
								</TR>
							</TABLE>
						</TD>
						</TD></TR>
					<TR>
						<TD colSpan="6"><asp:button id="btnSave" runat="server" Text="Simpan" CausesValidation="False"></asp:button><asp:button id="btnBlock" runat="server" Text="Blok" CausesValidation="False"></asp:button><asp:button id="btnCancel" runat="server" Text="Kembali" CausesValidation="False"></asp:button></TD>
					</TR></TBODY></TABLE>
			</TD></TR></asp:panel></TBODY></TABLE></form>
		</TD></TR></TBODY></TABLE>
	</body>
</HTML>
