<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmFleetCustomerList.aspx.vb" Inherits=".DaftarFleetCustomer" smartNavigation="False"%>

<html>
<head>
    <title>Daftar Fleet Customer</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
	<script language="javascript" src="../WebResources/InputValidation.js"></script>

<body MS_POSITIONING="GridLayout">
    <form id="form1" runat="server">
    <table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
        <tr>
			<td class="titlePage">FLEET MANAGEMENT - Daftar Fleet Customer</td>
		</tr>
		<tr>
			<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
		</tr>
		<tr>
			<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
		</tr>
        <tr>
			<td vAlign="top" align="left">
                <table id="Table2" cellSpacing="1" width="100%" cellPadding="1" border="0">	
                    <tr>
						<td class="titleField" width="24%">Kode</td>
						<td width="1%">:</td>
						<td width="75%">
                            <asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtCode" onblur="HtmlCharBlur(txtCode)" runat="server" Width="161px"></asp:textbox>
						</td>
					</tr>
                    <tr>
						<td class="titleField" width="24%">Nama</td>
						<td width="1%">:</td>
						<td width="75%">
                            <asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtName" onblur="HtmlCharBlur(txtName)" runat="server" Width="161px"></asp:textbox>
						</td>
					</tr>
                    <tr>
						<td class="titleField" width="24%">Nama Grup</td>
						<td width="1%">:</td>
						<td width="75%">
                            <asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtGroupName" onblur="HtmlCharBlur(txtGroupName)" runat="server" Width="161px"></asp:textbox>
						</td>
					</tr>
                    <tr>
						<td class="titleField" width="24%">Provinsi</td>
						<td width="1%">:</td>
						<td width="75%">
                            <asp:dropdownlist id="ddlProvince" runat="server" AutoPostBack="True"></asp:dropdownlist>
						</td>
					</tr>
                     <tr>
						<td class="titleField" width="24%">Kota/Kabupaten</td>
						<td width="1%">:</td>
						<td width="75%">
                            <asp:dropdownlist id="ddlCity" runat="server" AutoPostBack="True"></asp:dropdownlist>
						</td>
					</tr>                   
                    <tr>
						<td></td>
						<td></td>
						<td>
                            <asp:button id="btnSearch" runat="server" Width="64px" Text="Cari" CausesValidation="False"></asp:button>
                        </td>
					</tr>
                    <tr>
                        <td valign="top" colspan="3">
                            <div id="div1" style="OVERFLOW: auto; HEIGHT: 360px">
                                <asp:Datagrid ID="dtgFleetCustomerList" runat="server" Width="100%" CellSpacing="1" GridLines="None"
                                CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0"
                                AutoGenerateColumns="False" PageSize="15" AllowCustomPaging="true" AllowPaging="true" AllowSorting="True"
                                DataKeyField="ID" ShowFooter="false">
                                    
                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                                <ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
                                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								    <Columns>
                                        <asp:BoundColumn Visible="false" DataField="ID" HeaderText="ID">
									        <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
									        <ItemStyle ForeColor="Gray" BackColor="LightPink"></ItemStyle>
								        </asp:BoundColumn>

									    <asp:TemplateColumn HeaderText="No">
										    <HeaderStyle Width="3%"  CssClass="titleTableSales"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
										    <ItemTemplate>
											    <asp:Label Runat="server" ID="lblNo"></asp:Label>
										    </ItemTemplate>
									    </asp:TemplateColumn>
                                    
                                        <asp:BoundColumn DataField="Code" SortExpression="Code" HeaderText="Kode Fleet Customer">
										    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
									    </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama Fleet Customer">
										    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
									    </asp:BoundColumn>

                                        <asp:TemplateColumn SortExpression="CustomerGroup.Name" HeaderText="Nama Konsumen Grup">
										    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
										    <ItemTemplate>
											    <asp:Label id="lblCustomerGroup" runat="server" Text=""></asp:Label>
										    </ItemTemplate>
									    </asp:TemplateColumn>

                                         <asp:TemplateColumn SortExpression="City.CityName" HeaderText="Kota">
										    <HeaderStyle Width="10%" CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
										    <ItemTemplate>
											    <asp:Label id="lblCity" runat="server" Text=""></asp:Label>
										    </ItemTemplate>
									    </asp:TemplateColumn>

<%--                                          <asp:TemplateColumn  HeaderText="Grade">
										    <HeaderStyle Width="10%" CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
										    <ItemTemplate>
											    <asp:Label id="lblScoreGrade" runat="server" Text=""></asp:Label>
										    </ItemTemplate>
									    </asp:TemplateColumn>--%>

                                       <%-- <asp:TemplateColumn HeaderText="Dealer yang Harus di Petakan">
                                            <HeaderStyle CssClass="titleTableSales" ForeColor="White" />
                                            <ItemTemplate>
                                                <div style="width:98%">
                                                    <asp:Label id="lblNeedtobeMapped" runat="server"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>--%>

                                        <asp:TemplateColumn>
                                            <HeaderStyle ForeColor="White" Width="12%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                            <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>  
                                                <div style="width:95px;align-content:center">
                                                <asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False" CommandName="view">
											        <img src="../images/detail.gif" border="0" alt="Lihat">
                                                </asp:LinkButton>
										        <asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="edit">
											        <img src="../images/edit.gif" border="0" alt="Ubah">
										        </asp:LinkButton>
										        <asp:LinkButton id="lbtnDelete" runat="server" Width="16px" Text="Hapus" CausesValidation="False" CommandName="Delete">
											        <img src="../images/trash.gif" border="0" alt="Hapus">
										        </asp:LinkButton>
                                                <asp:LinkButton id="lbtnAssignToCustomer" runat="server" Width="16px" Text="Hapus" CausesValidation="False" CommandName="assignToCustomer">
											        <img src="../images/assigntocustomer.png" border="0" alt="Assign To Customer">
										        </asp:LinkButton>
                                                <asp:LinkButton id="lbtnAssignToDealer" runat="server" Width="16px" Text="Hapus" CausesValidation="False" CommandName="assignToDealer">
											        <img src="../images/assigntodealer.png" border="0" alt="Assign To Dealer">
										        </asp:LinkButton>
                                                    </div>
                                            </ItemTemplate>
                                            
                                        </asp:TemplateColumn>
								    </Columns>
                                    <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							    </asp:Datagrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

    </table>
    </form>
</body>
</html>
