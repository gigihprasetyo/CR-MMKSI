<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMSPPaymentHistory.aspx.vb" Inherits=".FrmMSPPaymentHistory" smartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head runat="server">
    <title>History MSP</title>
	<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
	<meta name="vs_defaultClientScript" content="JavaScript">
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
	<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
	<script language="javascript" src="../WebResources/InputValidation.js"></script>
</head>
<body MS_POSITIONING="GridLayout">
    <form id="FrmMSPPaymentHistory" method="post" runat="server">
        <table id="tblMSPPaymentHistory" cellSpacing="0" cellPadding="0" width="100%" border="0">
            <tr>
				<td class="titlePage">MSP - History Pembayaran</td>
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
							<td class="titleField" width="23%">Tujuan Pembayaran</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblDealer" runat="server" Text="MSP"></asp:label>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%"></td>
							<td width="1%"></td>
							<td width="25%">
                               
							</td>
						</tr>
                        <tr>
							<td class="titleField" width="23%">Nomor Registrasi Pembayaran</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblPaymentRegNumber" runat="server"></asp:label>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%">Credit Account</td>
							<td width="1%">:</td>
							<td width="25%">
                                <asp:Label ID="lblCreditAccount" runat="server"></asp:Label>
							</td>
						</tr>
                        
                        
                        <tr><td colspan="7"></td></tr>
                        <tr>
                            <td colspan="7" valign="top">
                               <div id="div1" style="OVERFLOW: auto">
                                    <asp:datagrid id="dtgMSPTrfHistory" runat="server" Width="100%" CellSpacing="1" AllowSorting="True" PageSize="15" AllowPaging="false" GridLines="Vertical" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#E7E7FF" AutoGenerateColumns="False" ShowFooter="False">
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
									            <asp:Label id="lblNo" runat="server"></asp:Label>
								            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        
                                        <asp:TemplateColumn HeaderText="Tanggal Transfer">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblTrasferDate" runat="server"></asp:Label>
								            </ItemTemplate>
							            </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Amount">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblAmount" runat="server"></asp:Label>
								            </ItemTemplate>
							            </asp:TemplateColumn>
                                        
							        </Columns>
                                </asp:datagrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
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
