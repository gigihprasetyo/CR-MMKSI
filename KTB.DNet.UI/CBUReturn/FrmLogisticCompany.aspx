<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmLogisticCompany.aspx.vb" Inherits=".FrmLogisticCompany" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Logistic Company</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>

    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script>
        function ChangeToUpper(elem) {
            elem.value = elem.value.toUpperCase();
        }

        // popUp City
        function ShowCitySelection() {
            showPopUp('../PopUp/PopUpCity.aspx', '', 600, 600, CitySelection);
        }
        function CitySelection(selectedCity) {
            var hdnKota = document.getElementById("hdnKota");
            var txtKota = document.getElementById("txtKota");
            var arrValue = selectedCity.split(';');
            hdnKota.value = arrValue[0];
            txtKota.value = arrValue[1];
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Klaim Pengembalian Kendaraan -&nbsp; Logistic Company</td>
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
                <td align="left">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="24%">Nama Vendor</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:HiddenField ID="hdnID" runat="server" />
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" onKeyup="ChangeToUpper(this);" onblur="HtmlCharUniv(txtVendorName)" ID="txtVendorName" runat="server" Width="170px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%">Short List</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" onKeyup="ChangeToUpper(this);" ID="txtKode" runat="server" Width="170px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%">No Telp</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox onkeypress="return numericOnlyUniv(event)" onKeyup="ChangeToUpper(this);" ID="txtNoTelp" runat="server" Width="170px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%">Kota</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <%--<asp:DropDownList ID="ddlKota" runat="server"></asp:DropDownList>--%>
                                <asp:HiddenField ID="hdnKota" runat="server" />
                                <asp:TextBox ID="txtKota" runat="server" MaxLength="50" Width="150px"></asp:TextBox>
                                <asp:LinkButton ID="lnkBtnPopUpKota" runat="server" Width="16px">
                                    <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%">Alamat</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" onKeyup="ChangeToUpper(this);" ID="txtAlamat" runat="server" Width="250px" Height="80px" MaxLength="50" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField"></td>
                            <td></td>
                            <td>&nbsp;
									<asp:Button ID="btnSearch" runat="server" Width="60px" Text="Cari"></asp:Button>
                                <asp:Button ID="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:Button>
                                <asp:Button ID="btnBatal" runat="server" Width="60px" Text="Batal"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 350px">
                        <asp:DataGrid ID="dgLogistic" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" GridLines="Horizontal"
                            CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True"
                            PageSize="25" AllowPaging="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama Vendor">
                                    <HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Kode" SortExpression="Kode" HeaderText="Short List">
                                    <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Address" SortExpression="Address" HeaderText="Alamat">
                                    <HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="City.CityName" HeaderText="Kota">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.City.CityName") %>' ID="Label2">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="NoTelfon" SortExpression="NoTelfon" HeaderText="No. Telp">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False" CommandName="View">
												<img alt="Lihat Detil" src="../images/detail.gif" border="0"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img alt="Hapus" src="../images/trash.gif" border="0"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    </form>
    <script language="javascript">
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
