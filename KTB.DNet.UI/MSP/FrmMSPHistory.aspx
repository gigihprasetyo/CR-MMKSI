<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMSPHistory.aspx.vb" Inherits=".FrmMSPHistory" smartNavigation="False" %>

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
    <form id="FrmMSPHistory" method="post" runat="server">
        <table id="tblMSPRegistration" cellSpacing="0" cellPadding="0" width="100%" border="0">
            <tr>
				<td class="titlePage">MSP - History MSP</td>
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
							<td class="titleField" width="23%">Jenis Dokumen</td>
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
							<td class="titleField" width="23%">Nomor MSP</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblMSPCode" runat="server"></asp:label>
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
                                    <asp:datagrid id="dtgMSPHistory" runat="server" Width="100%" CellSpacing="1" AllowSorting="True" PageSize="15" AllowPaging="false" GridLines="Vertical" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#E7E7FF" AutoGenerateColumns="False" ShowFooter="False">
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
                                        <asp:TemplateColumn HeaderText="No Rangka">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblChassisNumber" runat="server"></asp:Label>
								            </ItemTemplate>
							            </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Kategori Lama">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblOldCategory" runat="server"></asp:Label>
								            </ItemTemplate>
							            </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Kategori Baru">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblNewCategory" runat="server"></asp:Label>
								            </ItemTemplate>
							            </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Tipe Pengajuan Lama">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblOldRequestType" runat="server"></asp:Label>
								            </ItemTemplate>
							            </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Tipe Pengajuan Baru">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblNewRequestType" runat="server"></asp:Label>
								            </ItemTemplate>
							            </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Tipe MSP Lama">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblOldMSPType" runat="server"></asp:Label>
								            </ItemTemplate>
							            </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Tipe MSP Baru">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblNewMSPType" runat="server"></asp:Label>
								            </ItemTemplate>
							            </asp:TemplateColumn>
                                         <asp:TemplateColumn HeaderText="Durasi Lama">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblOldDuration" runat="server"></asp:Label>
								            </ItemTemplate>
							            </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Durasi Baru">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblNewDuration" runat="server"></asp:Label>
								            </ItemTemplate>
							            </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Tanggal Pengajuan">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:label runat="server" id="lblRequestDate"></asp:label>
								            </ItemTemplate>
							            </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Diproses Oleh">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:label runat="server" id="lblCreatedBy"></asp:label>
								            </ItemTemplate>
							            </asp:TemplateColumn>
                                         <asp:TemplateColumn HeaderText="Status">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:label runat="server" id="lblStatus"></asp:label>
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
