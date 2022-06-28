<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmListRealizationEvent.aspx.vb" Inherits="FrmListRealizationEvent" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmListRealizationEvent</title>
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
				showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx?x=Territory','',500,760,DealerSelection);
			}
			
			function KTBNote(selectedCode)
			{
			
			}		
			
			function DealerSelection(selectedDealer)
			{
				
				var txtDealerSelection = document.getElementById("txtDealerCode");
				txtDealerSelection.value = selectedDealer;
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
					<td class="titlePage" style="HEIGHT: 19px">EVENT - Daftar Realisasi EVENT</td>
				</tr>
				<tr>
					<td style="HEIGHT: 2px" background="../images/bg_hor.gif" height="2"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="1"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 93px">
						<table cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<td class="titleField" width="20%">Kode Dealer</td>
								<TD width="1%">:</TD>
								<TD width="69%"><asp:textbox id="txtDealerCode" runat="server" Width="300px"></asp:textbox>
									<asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
							</TR>
							<TR>
								<td class="titleField" width="20%">No Realisasi Event</td>
								<TD width="1%">:</TD>
								<TD width="69%"><asp:textbox id="txtRealizationNo" runat="server" Width="184px"></asp:textbox></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px" width="20%">No Pengajuan Event</td>
								<TD style="HEIGHT: 19px" width="1%">:</TD>
								<TD style="HEIGHT: 19px" width="69%"><asp:textbox id="txtRequestNo" runat="server" Width="184px"></asp:textbox></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 18px" width="20%">Jenis Event</td>
								<TD style="HEIGHT: 18px" width="1%">:</TD>
								<TD style="HEIGHT: 18px" width="69%">
									<asp:DropDownList id="ddlEventType" runat="server" Width="184px"></asp:DropDownList></TD>
							</TR>
							<TR>
								<td class="titleField" width="20%">Jadwal Event</td>
								<TD width="1%">:</TD>
								<TD width="69%">
									<table>
										<tr>
											<td><cc1:inticalendar id="icStartDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>s/d</td>
											<td><cc1:inticalendar id="icEndDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							</TR>
							<tr>
								<td></td>
								<td></td>
								<td vAlign="middle"><asp:button id="btnSearch" runat="server" Text="Cari" Width="64px"></asp:button>
									<asp:button id="btnCancel" runat="server" Text="Batal" Width="64px"></asp:button></td>
							</tr>
							<TR>
								<td vAlign="top" width="100%" colSpan="3">
									<div>
										<asp:datagrid id="dtgRealEvent" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD"
											BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" Width="100%" AllowSorting="True" CellSpacing="1"
											PageSize="25" AllowCustomPaging="True" AllowPaging="True" GridLines="None" BorderStyle="None">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle"
												BackColor="#CC3333"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No.">
													<HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
													<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="Label1" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>' runat="server">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
													<HeaderStyle Width="25%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="Label2" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>' runat="server">
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
												<asp:BoundColumn DataField="EventRealizationNo" SortExpression="EventRealizationNo" ReadOnly="True"
													HeaderText="Nomor Realisasi Event">
													<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="EventRequestNo" SortExpression="EventRequestNo" ReadOnly="True" HeaderText="No Pengajuan Event">
													<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="EventType.Description" HeaderText="Jenis Event">
													<HeaderStyle Width="25%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblEventTypeDesc" Text='<%# DataBinder.Eval(Container, "DataItem.EventType.Description") %>' runat="server">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="RealDateStart" SortExpression="RealDateStart" ReadOnly="True" HeaderText="Mulai"
													DataFormatString="{0:dd/MM/yyyy}">
													<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="RealDateEnd" SortExpression="RealDateEnd" ReadOnly="True" HeaderText="Selesai"
													DataFormatString="{0:dd/MM/yyyy}">
													<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Location" SortExpression="Location" ReadOnly="True" HeaderText="Tempat Acara">
													<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="RealNumOfInvitation" SortExpression="RealNumOfInvitation" ReadOnly="True"
													HeaderText="Jumlah Undangan" DataFormatString="{0:N0}">
													<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="CV">
													<HeaderStyle Width="25%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="Label3" Text='<%# DataBinder.Eval(Container, "DataItem.NumOfSalesCV") %>' runat="server">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="LCV">
													<HeaderStyle Width="25%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="Label4" Text='<%# DataBinder.Eval(Container, "DataItem.NumOfSalesLCV") %>' runat="server">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="PC">
													<HeaderStyle Width="25%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="Label5" Text='<%# DataBinder.Eval(Container, "DataItem.NumOfSalesPC") %>' runat="server">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Total">
													<HeaderStyle Width="25%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblTotal" Text='<%# DataBinder.Eval(Container, "DataItem.TotalofSales") %>' runat="server">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="RealTotalCost" SortExpression="RealTotalCost" ReadOnly="True" HeaderText="Aktual Biaya"
													DataFormatString="{0:N0}">
													<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Komentar Dealer">
													<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Button ID="btnShowComment" Runat="server" Text="Lihat"></asp:Button>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnDetails" CausesValidation="False" CommandName="detail" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' text="Lihat" Runat="server">
															<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
														<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CausesValidation="False">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnVideo" runat="server" Text="Video" CommandName="Video" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CausesValidation="False">
															<img src="../images/icon_video.gif" border="0" alt="File Video"></asp:LinkButton>
														<asp:LinkButton id="lbtnMatpromo" runat="server" Text="MatPromo" CommandName="MatPromo" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CausesValidation="False">
															<img src="../images/alur_flow.gif" border="0" alt="File Material Promo"></asp:LinkButton>
														<asp:LinkButton id="lbtnCost" runat="server" Text="Detail_Biaya" CommandName="DetailBiaya" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CausesValidation="False">
															<img src="../images/rp.gif" border="0" alt="File Detail Biaya"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</td>
							</TR>
							<TR>
								<td class="titleField" width="20%"></td>
								<TD width="1%"></TD>
								<TD width="69%"></TD>
							</TR>
							<TR>
								<td class="titleField" width="30%"></td>
								<TD width="1%"></TD>
								<TD width="69%"></TD>
							</TR>
						</table>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
