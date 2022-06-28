<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDealerPOTargetList.aspx.vb" Inherits=".FrmDealerPOTargetList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Purchase Order -&nbsp; Program TOP Khusus</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
        function ShowPopUpDealer() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 430, 800, DealerSSS);
        }

        function DealerSSS(selectedRefNumber) {
            var hdnDealer = document.getElementById("hdnDealer");
            hdnDealer.value = selectedRefNumber;

            if (navigator.appName == "Microsoft Internet Explorer") {
                hdnDealer.blur();
            }
            else {
                hdnDealer.onchange();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Purchase Order -&nbsp; Program TOP Khusus</td>
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
                <td align="Left" style="width: 50%">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="50%" border="0">
                        <tr>
                            <td class="titleField" width="20%" valign="top" align="right">
                                <asp:Label ID="lblDealerSearch" runat="server">Kode Dealer</asp:Label></td>
                            <td width="1%">:</td>
                            <td width="34%">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtDealerCode" runat="server"></asp:TextBox>
                                <asp:HiddenField ID="hdnDealer" runat="server" />
                                <asp:LinkButton ID="lnkBtnPopUpDealer" runat="server" Width="16px">
                                            <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:LinkButton>
                                <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Kategori</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlCategory" AutoPostBack="true" runat="server"></asp:DropDownList>&nbsp;&nbsp;
                                            <asp:DropDownList ID="ddlVehicleModel" runat="server" Width="120px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px">Bulan Berlaku</td>
                            <td>:</td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="cbDate" runat="server" ></asp:CheckBox></td>
                                        <td>
                                            <cc1:IntiCalendar ID="icTglBerlaku" runat="server" TextBoxWidth="70" Visible="false"></cc1:IntiCalendar>
                                            <asp:DropDownList ID="ddlMonth" runat="server" Width="60px"></asp:DropDownList>&nbsp;
                                            <asp:DropDownList ID="ddlYear" runat="server" Width="70px"></asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px"></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <hr />
                </td>
            </tr>
        </table>
        <div id="div1" style="overflow: auto; height: 300px">
            <asp:DataGrid ID="dgDealerPOTarget" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" GridLines="Horizontal"
                CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True"
                PageSize="25" AllowPaging="True" BorderStyle="None" DataKeyField="ID" ShowFooter="false">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="White" HorizontalAlign="Center"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                        <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="No">
                        <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Dealer" SortExpression="Dealer.DealerCode">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealer" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Model Kendaraan" SortExpression="VechileModel.IndDescription">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblVechileModelDesc" Text='<%# DataBinder.Eval(Container, "DataItem.VechileModel.IndDescription")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Sequence" SortExpression="Sequence">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblSequence" Text='<%# DataBinder.Eval(Container, "DataItem.Sequence")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Periode Awal" SortExpression="ValidFrom">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblValidFrom" Text='<%# DataBinder.Eval(Container, "DataItem.ValidFrom", "{0:dd/MM/yyyy}")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Periode Akhir" SortExpression="ValidTo">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblValidTo" Text='<%# DataBinder.Eval(Container, "DataItem.ValidTo", "{0:dd/MM/yyyy}")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Free Days" SortExpression="FreeDays">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblFreeDays" Text='<%# DataBinder.Eval(Container, "DataItem.FreeDays")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Max TOP Day" SortExpression="MaxTOPDay">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblMaxTOPDay" Text='<%# DataBinder.Eval(Container, "DataItem.MaxTOPDay")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="MaxQuantity" SortExpression="MaxQuantity">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblMaxQuantity" Text='<%# DataBinder.Eval(Container, "DataItem.MaxQuantity")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Terpakai">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTerpakai" Text='' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Sisa">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblSisa" Text='' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Default" SortExpression="IsDefault">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbIsDefault" runat="server" Enabled="false"></asp:CheckBox>
                            <%--<asp:Label ID="lblIsDefault" Text='<%# DataBinder.Eval(Container, "DataItem.IsDefault")%>' runat="server"></asp:Label>--%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>
    </form>
</body>
</html>
