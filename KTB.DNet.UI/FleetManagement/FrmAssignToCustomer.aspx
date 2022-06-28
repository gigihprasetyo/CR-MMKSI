<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmAssignToCustomer.aspx.vb" Inherits=".FrmAssignToCustomer" smartNavigation="False" %>

<html>
<head runat="server">
    <title>Assign To Customer</title>
    <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
	<script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
        function ShowPPCustomerSelection() {
            var fleetID = document.getElementById("hdnFleetCustomerID");
            showPopUp('../PopUp/PopUpCustomerSelection.aspx?Tyjiuy678=' + fleetID.value + '', '', 500, 760, CustomerSelection);
        }

        function CustomerSelection(selectedCustomer) {
            var txtCustomerSelection = document.getElementById("txtCustomerCodeList");
            txtCustomerSelection.value = selectedCustomer;
            var btnCustomerHelper = document.getElementById('btnCustomerHelper');
            if (btnCustomerHelper) btnCustomerHelper.click();

        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" runat="server">
        <table cellspacing="0" id="Table2" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 16px">&nbsp;
					<asp:Label ID="lblTitle" runat="server">FLEET MANAGEMENT - Assign to Customer</asp:Label>
                    <asp:Label ID="SessionStatus" runat="server" Visible="false"></asp:Label>
                    <asp:HiddenField ID="hdnFleetCustomerID" runat="server" />
                </td>
            </tr>
            <tr style="height: 1px;">
                <td style="height: 1px; background-image: url('../images/bg_hor.gif'); background-size: auto;">

                </td>
            </tr>
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                         <tr height="24px"><td colspan="6"></td></tr>
                        <tr>
                            <td class="titleField" width="24%">Kode Fleet Customer</td>
                            <td width="1%">:</td>
                            <td width="15%"><asp:Label ID="lblFleetCustomerCode" runat="server"></asp:Label></td>

                            <td class="titleField" width="24%">Nama Fleet Customer</td>
                            <td width="1%">:</td>
                             <td width="15%"><asp:Label ID="lblFleetCustomerName" runat="server"></asp:Label></td>
                        </tr>
                        <tr height="24px"><td colspan="6"></td></tr>
                        <tr>
                            <td colspan="6" class="titlePanel">
                                <div style="vertical-align:central">
                                    Pencarian Konsumen :
                                    <asp:label id="lblPopUpCustomer" onclick="ShowPPCustomerSelection();" Runat="server"> <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"> </asp:label>
                                    <asp:HiddenField  ID="txtCustomerCodeList" runat="server"></asp:HiddenField>
                                </div>
                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <div id="div1" style="OVERFLOW: auto;">
                                  
                                    <asp:Datagrid ID="dtgCustomerSelection" runat="server" Width="100%" CellSpacing="1" GridLines="Horizontal"
                                        CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0"
                                        AutoGenerateColumns="False" PageSize="25" AllowCustomPaging="true" AllowPaging="false" AllowSorting="True"
                                        DataKeyField="ID" ShowFooter="False">
                                    
                                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                        <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                                        <ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                        <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								            <Columns>
									            <asp:TemplateColumn HeaderText="No">
                                                    <HeaderStyle Width="2%" CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
											            <asp:Label id="lblNo" runat="server">
											            </asp:Label>
										            </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false">
													<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
												</asp:BoundColumn>

									            <asp:TemplateColumn HeaderText="Kode">
										            <HeaderStyle Width="7%" CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
										            <ItemTemplate>
											            <asp:Label id=lblCustomerCode runat="server">
											            </asp:Label>
										            </ItemTemplate>
									            </asp:TemplateColumn>
									            
                                                 <asp:TemplateColumn HeaderText="Nama">
										            <HeaderStyle Width="7%" CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
										            <ItemTemplate>
											            <asp:Label id=lblCustomerName runat="server">
											            </asp:Label>
										            </ItemTemplate>
									            </asp:TemplateColumn>

                                                 <asp:TemplateColumn HeaderText="Alamat">
										            <HeaderStyle Width="7%" CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
										            <ItemTemplate>
											            <asp:Label id=lblCustomerAddress runat="server">
											            </asp:Label>
										            </ItemTemplate>
									            </asp:TemplateColumn>

                                                 <asp:TemplateColumn HeaderText="Kelurahan">
										            <HeaderStyle Width="7%" CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
										            <ItemTemplate>
											            <asp:Label id=lblCustomerKelurahan runat="server">
											            </asp:Label>
										            </ItemTemplate>
									            </asp:TemplateColumn>

                                                 <asp:TemplateColumn HeaderText="Kecamatan">
										            <HeaderStyle Width="7%" CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
										            <ItemTemplate>
											            <asp:Label id=lblCustomerKecamatan runat="server">
											            </asp:Label>
										            </ItemTemplate>
									            </asp:TemplateColumn>

                                                 <asp:TemplateColumn HeaderText="Kota">
										            <HeaderStyle Width="7%" CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
										            <ItemTemplate>
											            <asp:Label id=lblCustomerCity runat="server">
											            </asp:Label>
										            </ItemTemplate>
									            </asp:TemplateColumn>

                                                <asp:TemplateColumn>
                                                    <HeaderStyle ForeColor="White" Width="3%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    <ItemTemplate>
                                                        &nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lbtnIsDefault" CausesValidation="false" runat="server" CommandName="default" Visible="true">
                                                            <asp:Image ImageUrl="../images/aktif.gif" runat="server" ID="imgIsDefault" AlternateText="Cancel Default" Visible="false" />
                                                            <asp:Image ImageUrl="../images/in-aktif.gif" runat="server" ID="imgCancelDefault" AlternateText="Set as Default" Visible="false" />
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnDelete" CausesValidation="false" runat="server" CommandName="delete" Visible="true">
												            <img src="../images/trash.gif" border="0" alt="Hapus" >
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>
								            </Columns>
							            </asp:datagrid>

                                </div>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
            
        </table><br />
        <asp:Button ID="btnSave" runat="server" Text="Simpan" />
        <asp:Button ID="btnCustomerHelper" style="display:none;" runat="server"  Text="" CausesValidation="false" />
        <asp:Button ID="btnBack" runat="server" Text="Kembali" />
    </form>
</body>
</html>
