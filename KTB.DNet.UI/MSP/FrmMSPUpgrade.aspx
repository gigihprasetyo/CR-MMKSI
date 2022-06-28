<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMSPUpgrade.aspx.vb" Inherits=".FrmMSPUpgrade" smartNavigation="False"  %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head runat="server">
    <title>Upgrade MSP</title>
	<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
	<meta name="vs_defaultClientScript" content="JavaScript">
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
	<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
	<script language="javascript" src="../WebResources/InputValidation.js"></script>
</head>
<body MS_POSITIONING="GridLayout">
    <form id="FrmMSPRegistration" method="post" runat="server">
        <table id="tblMSPRegistration" cellSpacing="0" cellPadding="0" width="100%" border="0">
            <tr>
				<td class="titlePage">MSP - Upgrade Tipe</td>
			</tr>
            <tr>
				<td height="1" background="../images/bg_hor.gif"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
			</tr>
            <tr>
				<td height="10"><IMG border="0" src="../images/dot.gif" height="1"></td>
			</tr>
            <tr>
                <td align="left">
                    <table id="Table2" border="0" cellSpacing="1" cellPadding="2" width="100%">
						<tr>
							<td class="titleField" width="23%">Kode Dealer</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblDealer" runat="server"></asp:label>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%">Tgl PKT</td>
							<td width="1%">:</td>
							<td width="25%">
                                <asp:label id="lblTglPkt" runat="server"></asp:label>
							</td>
						</tr>
                        <tr>
							<td class="titleField" width="23%">Nama Dealer</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblDealerName" runat="server"></asp:label>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%">Tgl Join MSP</td>
							<td width="1%">:</td>
							<td width="25%">
                                <asp:label id="lblRegistrationDate" runat="server"></asp:label>
							</td>
						</tr>
                         <tr>
							<td class="titleField" width="23%">Nama</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblName" runat="server"></asp:label>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%">Valid Sampai Dengan</td>
							<td width="1%">:</td>
							<td width="25%">
                                <asp:label id="lblValidUntil" runat="server"></asp:label>
							</td>
						</tr>
                         <tr>
							<td class="titleField" width="23%">Usia</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblAge" runat="server"></asp:label>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%">Dijual Oleh</td>
							<td width="1%">:</td>
							<td width="25%">
                                <asp:label id="lblSoldBy" runat="server"></asp:label>
							</td>
						</tr>
                          <tr>
							<td class="titleField" width="23%">Alamat</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblAddress" runat="server"></asp:label>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%">Tgl Upgrade</td>
							<td width="1%">:</td>
							<td width="25%">
                                <asp:label id="lblUpgradeDate" runat="server"></asp:label>
							</td>
						</tr>
                         <tr>
							<td class="titleField" width="23%">Kelurahan</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblKelurahan" runat="server"></asp:label>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%">No MSP</td>
							<td width="1%">:</td>
							<td width="25%">
                                <asp:Label ID="lblMSPNumber" runat="server"></asp:Label>
							</td>
						</tr>
                        <tr>
							<td class="titleField" width="23%">kecamatan</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblKecamatan" runat="server"></asp:label>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%"><asp:label id="lblNameDealerCodeUpgrade" runat="server">Kode Dealer [Upgrade]</asp:label> </td>
							<td width="1%">:</td>
							<td width="25%">
                                 <asp:label id="lblDealerCodeUpgrade" runat="server"></asp:label>
							</td>
						</tr>
                        <tr>
							<td class="titleField" width="23%">Kodya/Kabupaten</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblPreArea" runat="server"></asp:label>
                            </td>
							<td width="1%"></td>    
							<td class="titleField" width="23%"><asp:label id="lblNameDealerNameUpgrade" runat="server">Nama Dealer [Upgrade]</asp:label></td>
							<td width="1%">:</td>
							<td width="25%">
                                <asp:label id="lblNamaDealerUpgrade" runat="server"></asp:label>
							</td>
						</tr>
                        <tr>
							<td class="titleField" width="23%">Propinsi</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblProvince" runat="server"></asp:label>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%"></td>
							<td width="1%"></td>
							<td width="25%">
                                
							</td>
						</tr>
                        <tr>
							<td class="titleField" width="23%">No Tlp/HP</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblPhoneNo" runat="server"></asp:label>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%"></td>
							<td width="1%"></td>
							<td width="25%">
                                
							</td>
						</tr>
                        <tr>
							<td class="titleField" width="23%">Email</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblEmail" runat="server"></asp:label>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%"></td>
							<td width="1%"></td>
							<td width="25%">
                                
							</td>
						</tr>
                        <tr><td colspan="7"></td></tr>
                        <tr>
                            <td colspan="7" valign="top">
                               <div id="div1" style="OVERFLOW: auto">
                                    <asp:datagrid id="dtgMSPUpgrade" runat="server" Width="100%" CellSpacing="1" AllowSorting="True" PageSize="15" AllowPaging="false" GridLines="Vertical" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#E7E7FF" AutoGenerateColumns="False" ShowFooter="False">
						            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
						            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
						            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
						            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
						            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
						            <Columns>
                                        <asp:TemplateColumn HeaderText="No">
								            <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblMSPRegistrationHistoryID" runat="server" Visible="false"></asp:Label>
									            <asp:Label id="lblNo" runat="server"></asp:Label>
								            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="No Rangka">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblChassisNumber" runat="server"></asp:Label>
								            </ItemTemplate>
							            </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Tipe Kendaraan">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblVehicleType" runat="server"></asp:Label>
								            </ItemTemplate>
							            </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Tipe MSP Lama">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblMSPType" runat="server"></asp:Label>
								            </ItemTemplate>
							            </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Durasi">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblDuration" runat="server"></asp:Label>
								            </ItemTemplate>
							            </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Harga Awal(Incl. PPN 10%)">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblAmountHide" runat="server" Visible="false"></asp:Label>
									            <asp:Label id="lblAmount" runat="server"></asp:Label>
								            </ItemTemplate>
							            </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Tipe MSP Baru">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:dropdownlist runat="server" id="ddlNewMSPType" OnSelectedIndexChanged="ddlGridNewMSPType_SelectedIndexChanged" AutoPostBack = "true"></asp:dropdownlist>
								            </ItemTemplate>
							            </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Durasi Baru">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:dropdownlist runat="server" ID="ddlNewDuration" OnSelectedIndexChanged="ddlGridNewDuration_SelectedIndexChanged" AutoPostBack = "true"></asp:dropdownlist>
								            </ItemTemplate>
							            </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Harga Baru(Incl. PPN 10%)">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:label runat="server" id="lblNewAmount"></asp:label>
								            </ItemTemplate>
							            </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Selisih Harga(Incl. PPN 10%)">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:label runat="server" id="lblDiffAmount"></asp:label>
								            </ItemTemplate>
							            </asp:TemplateColumn>
							        </Columns>
                                </asp:datagrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <asp:Button runat="server" ID="btnUpgrade" text="Upgrade" />
                                <asp:Button runat="server" ID="btnBack" text="Kembali"/>
                            </td>
                        </tr>
                    </table>    
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
