<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmSettingCreditAccount.aspx.vb" Inherits="FrmSettingCreditAccount" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmSettingCreditAccount</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function DealerSelection(selectedCode) {
            var txtDealer = document.getElementById("txtKodeDealer");
            txtDealer.value = selectedCode;
            txtDealer.focus();
        }
    </script>
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">ADMIN SISTEM&nbsp;- Daftar Credit Account</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="14%">Kode Organisasi</td>
                            <td width="1%">:</td>
                            <td width="17%">
                                <asp:TextBox ID="txtKodeDealer" Width="160px" runat="server"></asp:TextBox>
                                <asp:Label ID="lblPopUpDealer" runat="server" Width="10">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>
                            </td>
                            <td width="45%" style="display: none">
                                <asp:CheckBox ID="cbSalesUnit" runat="server" Text=" Sales Unit" Font-Bold="True"></asp:CheckBox>&nbsp;&nbsp; 
									&nbsp;
									<asp:CheckBox ID="cbService" runat="server" Text=" Service" Font-Bold="True"></asp:CheckBox>&nbsp;&nbsp;&nbsp;
									<strong>
                                        <asp:CheckBox ID="cbSparePart" runat="server" Text=" Spare Part"></asp:CheckBox>
                                    </strong>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Nama Group</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlGroup" runat="server" Width="160px"></asp:DropDownList>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField">Credit Account</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtCreditAccount" Text="" Width="160px"></asp:TextBox>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField">Status</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:DropDownList ID="ddlstatus" runat="server" Width="160px" Visible="True"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Cara Pembayaran</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:DropDownList ID="ddlTermOfPayment" runat="server" Width="160px" Visible="True"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Cara Pembayaran Sebelumnya</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:DropDownList ID="ddlPrevTermOfPayment" runat="server" Width="160px" Visible="True"></asp:DropDownList>
                            </td>
                        </tr>
						<TR>
							<td class="titleField">Propinsi</td>
                            <td width="1%">:</td>
							<td><asp:dropdownlist id="ddlPropinsi" runat="server" Width="160px" Visible="True"></asp:dropdownlist></td>
						</TR>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text=" Cari " Font-Bold="True"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Kelipatan Pembayaran</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtKelipatanPembayaran" Text="" Width="45px"></asp:TextBox>
                                <asp:Button ID="btnSaveKelipatanPembayaran" runat="server" Text=" Simpan " Font-Bold="True"></asp:Button>
                            </td>
                            <td>

                            </td>
                        </tr>
                    </table>
                    <%# DataBinder.Eval(Container, "DataItem.SearchTerm1")+"/"+DataBinder.Eval(Container, "DataItem.SearchTerm2") %>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="div1" style="height: 380px; overflow: auto">
                        <asp:DataGrid ID="dtgDealerList" runat="server" Width="764px" PageSize="25" AllowPaging="False"
                            AutoGenerateColumns="False" AllowCustomPaging="True" AllowSorting="True" BorderColor="#E0E0E0" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD"
                            CellPadding="3" GridLines="Vertical" CellSpacing="1">
                            <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White" VerticalAlign="Top"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
                            <FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                    <HeaderStyle Width="1%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn Visible="False" DataField="DealerId" ReadOnly="True" HeaderText="DealerId">
                                    <HeaderStyle Width="1%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="1%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server" Width="20px" Font-Size="Smaller"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="SelectAll" HeaderText="" Visible="True">
                                    <HeaderTemplate>
                                     <input id="cbAllSelected" name="cbAllSelected" type="checkbox" onclick="CheckAll(this)" runat="server" />
                                    </HeaderTemplate>
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbSelected" runat="server" BackColor="Transparent" Enabled="True" Checked='False'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="DealerCode" SortExpression="DealerCode" ReadOnly="True" HeaderText="Kode Org">
                                    <HeaderStyle Width="8%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DealerName" SortExpression="DealerName" ReadOnly="True" HeaderText="Nama Org">
                                    <HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="City.CityName" HeaderText="Kota">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Width="68px" Text='<%# DataBinder.Eval(Container, "DataItem.CityName")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="City.CityName" HeaderText="Propinsi">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelPropinsi" runat="server" Width="68px" Text='<%# DataBinder.Eval(Container, "DataItem.ProvinceName")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="DealerGroup.GroupName" HeaderText="Grup">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Width="68px" Text='<%# DataBinder.Eval(Container, "DataItem.GroupName")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <%--<asp:TemplateColumn SortExpression="SearchTerm1" HeaderText="Term Cari 1/2" Visible="False">
                                    <HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Width="52px" Text='<%# DataBinder.Eval(Container, "DataItem.SearchTerm1")+" / "+DataBinder.Eval(Container, "DataItem.SearchTerm2") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>--%>
                                
                                <asp:TemplateColumn SortExpression="SalesUnitFlag" HeaderText="Sales Unit" Visible="False">
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbSalesUnitdtg" runat="server" BackColor="Transparent" Enabled="false" Checked='<%# DataBinder.Eval(Container, "DataItem.SalesUnitFlag") %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ServiceFlag" HeaderText="Service" Visible="False">
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbServicesdtg" runat="server" Enabled="false" Checked='<%# DataBinder.Eval(Container, "DataItem.ServiceFlag") %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="SparepartFlag" HeaderText="Spare Part" Visible="False">
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbSparePartdtg" runat="server" Enabled="false" Checked='<%# DataBinder.Eval(Container, "DataItem.SparepartFlag") %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="User Aktif" Visible="False">
                                    <HeaderStyle Width="6%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserActive" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Cara Pembayaran">
                                    <HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <%--<asp:TextBox runat="server" ID="txtCreditAccountE" Width="90px" MaxLength="6" Text='<%# DataBinder.Eval(Container, "DataItem.CreditAccount") %>'>
                                        </asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlTermOfPaymentGrid" runat="server" Width="160px"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Cara Pembayaran Sebelumnya">
                                    <HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <%--<asp:TextBox runat="server" ID="txtCreditAccountE" Width="90px" MaxLength="6" Text='<%# DataBinder.Eval(Container, "DataItem.CreditAccount") %>'>
                                        </asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlPrevTermOfPaymentGrid" runat="server" Width="160px"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Credit Account" SortExpression="CreditAccount">
                                    <HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtCreditAccountE" Width="90px" MaxLength="6" Text='<%# DataBinder.Eval(Container, "DataItem.CreditAccount") %>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# IIf(DataBinder.Eval(Container, "DataItem.Status") <> "1", "Tidak Aktif", "Aktif")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <%--<asp:TemplateColumn HeaderText="Kelipatan Pembayaran" SortExpression="KelipatanPembayaran">
                                    <HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtKelipatanPembayaranE" Width="90px" MaxLength="6" Text='<%# DataBinder.Eval(Container, "DataItem.KelipatanPembayaran")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>--%>
                                <asp:TemplateColumn  HeaderText="Diubah Tgl" >
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblLastUpdateTime" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Diubah Oleh" >
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblLastUpdateBy" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnSave" runat="server" Width="16px" Text="Save" ToolTip="Simpan Credit Account"
                                            CommandName="Save">
												<img src="../images/download.gif" border="0">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" Width="16px" Text="Ubah" ToolTip="Ubah Profile Organisasi"
                                            CommandName="Edit" Style="display: none;">
												<img src="../images/edit.gif" border="0"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnHakAkses" runat="server" ToolTip="Ubah Hak Akses Organisasi" CommandName="HakAkses"
                                            CausesValidation="False" Style="display: none;">
												<img src="../images/lock.jpg" border="0"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center"></td>
            </tr>
        </table>
        <table>
            <tr>
                <td class="titleField">Mengubah Status</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="ddlStatusProses" runat="server" Width="160px" Visible="True"></asp:DropDownList>
                </td>
                <td class="titleField">Cara Pembayaran</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="ddlCaraPembayaranProses" runat="server" Width="160px" Visible="True"></asp:DropDownList>
                    <asp:Button ID="btnProses" runat="server" Text=" Proses " Font-Bold="True"></asp:Button>
                    <asp:Button ID="btnDownload" runat="server" Text=" Download " Font-Bold="True"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
    <script language="javascript">
        function CheckAll(oCheckbox) {
            var GridView2 = document.getElementById("dtgDealerList");
            for (i = 1; i < GridView2.rows.length; i++) {
                //console.log(GridView2.rows[i])
                 GridView2.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
             }
        }

        if (window.parent == window) {
            if (!navigator.appName == "Microsoft Internet Explorer") {
                self.opener = null;
                self.close();
            }
            else {
                this.name = "origWin";
                origWin = window.open(window.location, "origWin");
                window.opener = top;
                window.close();
            }
        }
    </script>
</body>
</html>
