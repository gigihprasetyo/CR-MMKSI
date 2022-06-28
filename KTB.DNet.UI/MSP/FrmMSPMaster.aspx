<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMSPMaster.aspx.vb" Inherits=".FrmMSPMaster" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>MSP Master</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="FrmMSPMaster" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">MSP - Master MSP</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td align="left">
						<table id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<tr>
								<td class="titleField" width="14%">Kategori</td>
								<td width="1%">:</td>
								<td width="35%">
									<asp:dropdownlist runat="server" ID="ddlCategory" width="161px" AutoPostBack="True"></asp:dropdownlist>
									<asp:dropdownlist runat="server" ID="ddlVechileModel" width="161px" AutoPostBack="True"></asp:dropdownlist>
								</td>
                                <td class="titleField" width="14%"><asp:checkbox id="chkStartDate" Runat="server"></asp:checkbox>Mulai Berlaku</td>
								<td width="1%">:</td>
								<td width="35%">
									<table cellpadding="0" cellspacing="0">
								        <tr>
									        <td>
										        <cc1:inticalendar id="StartDateFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
									        <td>
										        &nbsp;s.d&nbsp;</td>
									        <td>
										        <cc1:inticalendar id="StartDateTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
									        <td>
									        </td>
								        </tr>
							        </table>
								</td>
							</tr>
							<tr>
								<td class="titleField" width="14%">Tipe</td>
								<td width="1%">:</td>
								<td width="35%">
									<asp:dropdownlist runat="server" ID="ddlVechileType" width="161px" AutoPostBack="True"></asp:dropdownlist>
								</td>
                                <td class="titleField" width="14%"><asp:checkbox id="chkEndDate" Runat="server"></asp:checkbox>Berlaku Sampai Dengan</td>
								<td width="1%">:</td>
								<td width="35%">
									<table cellpadding="0" cellspacing="0">
								<tr>
									<td>
										<cc1:inticalendar id="EndDateFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
									<td>
										&nbsp;s.d&nbsp;</td>
									<td>
										<cc1:inticalendar id="EndDateTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
									<td>
									</td>
								</tr>
							</table>
								</td>
							</tr>
                            <tr>
								<td class="titleField" width="14%">Tipe MSP</td>
								<td width="1%">:</td>
								<td width="35%">
									<asp:dropdownlist runat="server" ID="ddlMSPType" width="161px" AutoPostBack="True"></asp:dropdownlist>
								</td>
                                <td class="titleField" width="14%">Status</td>
								<td width="1%">:</td>
								<td width="35%">
									<asp:dropdownlist runat="server" ID="ddlStatus" width="161px" AutoPostBack="true"></asp:dropdownlist>
								</td>
							</tr>
                            <tr>
								<td class="titleField" width="14%">Durasi</td>
								<td width="1%">:</td>
								<td width="35%">
									<asp:TextBox id="txtDuration" runat="server" Width="200px" onblur="numericOnlyBlur(txtDuration)" onkeypress="return numericOnlyUniv(event)"></asp:TextBox>
								</td>
							</tr>
                            <tr>
								<td class="titleField" width="14%">KM</td>
								<td width="1%">:</td>
								<td width="35%">
									<asp:TextBox id="txtKm" runat="server" Width="200px" onblur="numericOnlyBlur(txtKm)" onkeypress="return numericOnlyUniv(event)"></asp:TextBox>
								</td>
							</tr>

							<tr>
								<td class="titleField" width="14%"></td>
								<td width="1%"></td>
								<td style="WIDTH: 262px" width="35%">
									<asp:button id="btnSearch" runat="server" Text="Cari" width="70px"></asp:button>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 350px">
                            <asp:datagrid id="dtgMSPMaster" runat="server" Width="100%" AllowSorting="True" CellSpacing="1" AutoGenerateColumns="False" BorderColor="Gainsboro" BorderWidth="0px" AllowPaging="True" AllowCustomPaging="True" BackColor="#CDCDCD" CellPadding="3" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="2%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign ="Center" />
										<ItemTemplate>
											<asp:Label ID="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="VehicleType.VechileTypeCode" HeaderText="Tipe Kendaraan">
										<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign ="Center" />
										<ItemTemplate>
											<asp:Label id="lblVechileTypeCode" Runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="VehicleType.Description" HeaderText="Nama Kendaraan">
										<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDescription" Runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                     <asp:TemplateColumn SortExpression="MSPType" HeaderText="Tipe MSP">
                                        <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                         <ItemStyle HorizontalAlign ="Center" />
                                         <ItemTemplate>
											<asp:Label id="lblMSPType" Runat="server">
											</asp:Label>
										</ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="StartDate" HeaderText="Tgl.Mulai Berlaku">
										<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign ="Center" />
										<ItemTemplate>
											<asp:Label id="lblStartDate" Runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="EndDate" HeaderText="Berlaku Sampai Dengan">
										<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign ="Center" />
										<ItemTemplate>
											<asp:Label id="lblEndDate" Runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:BoundColumn DataField="Duration" SortExpression="Duration" HeaderText="Durasi(Thn)">
                                        <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign ="Center" />
                                    </asp:BoundColumn>
                                    <asp:templatecolumn SortExpression="MSPKm" HeaderText="KM">
                                        <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign ="Right" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" id="lblKm"></asp:Label>
                                        </ItemTemplate>
                                    </asp:templatecolumn>
                                    <asp:TemplateColumn SortExpression="Amount" HeaderText="Harga MSP(Rp)">
										<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign ="Right" />
										<ItemTemplate>
											<asp:Label id="lblAmount" Runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                     <asp:TemplateColumn HeaderText="PPN">
								        <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
								        <ItemStyle HorizontalAlign="Center"></ItemStyle>
								        <ItemTemplate>
									        <asp:Label id="lblPPN" runat="server"></asp:Label>
								        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Total Biaya(Incl.PPN)">
								        <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
								        <ItemStyle HorizontalAlign="Center"></ItemStyle>
								        <ItemTemplate>
									        <asp:Label id="lblTotalAmountPPN" runat="server"></asp:Label>
								        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
										<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign ="Center" />
										<ItemTemplate>
											<asp:Label id="lblStatus" Runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>

								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</td>
				</tr>
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

