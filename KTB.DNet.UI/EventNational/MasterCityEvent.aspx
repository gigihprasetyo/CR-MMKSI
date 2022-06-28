<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MasterCityEvent.aspx.vb" Inherits=".MasterCityEvent" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>MasterCityEvent</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
        function ShowPopUpMultipleCity() {
            var txtCityCode = document.getElementById("txtKota");

            showPopUp('../PopUp/PopUpCityMultiple.aspx?CityCode=' + txtCityCode.value, '', 600, 600, CitySelection);
        }

        function CitySelection(SelectedCity) {
            var tempParam = SelectedCity.split(';');
            var valueCityCode = SelectedCity;
            var txtCityCode = document.getElementById("txtKota");
            if (navigator.appName == "Microsoft Internet Explorer") {
                txtCityCode.innerText = valueCityCode; //replace(tempParam[0], ' ', '');
            }
            else {
                txtCityCode.value = valueCityCode; //replace(tempParam[0], ' ', '');
            }
            __doPostBack("txtKota", "");
        }

        function trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <input id="hdnNationalEventID" type="hidden" value="0" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">MASTER -&nbsp; KOTA EVENT NASIONAL</td>
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
                    <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td align="left" style="width: 100%">
                                <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                                    <tr id="trMultiCity" runat="server">
                                        <td class="titleField" style="width: 146px">Pilih Beberapa Kota</td>
                                        <td style="width: 2px">:</td>
                                        <td>
                                            <asp:TextBox ID="txtKota" runat="server" Width="200px" MaxLength="5000" Height="50px"
                                                TextMode="MultiLine" AutoPostBack="true" Style="display: none"></asp:TextBox>
                                            <asp:TextBox ID="txtKotaShow" runat="server" Width="200px" MaxLength="5000" Height="100px"
                                                TextMode="MultiLine" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                                            <asp:Label ID="lblSearchKota" runat="server">
										        <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="width: 146px">Nama Kota</td>
                                        <td style="width: 2px">:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlKota" Width="100px" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="width: 146px">Status</td>
                                        <td style="width: 2px">:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlStatus" Width="100px" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <hr />
                    <asp:Button ID="btnSave" OnClientClick="return confirm('Anda yakin mau simpan?');" runat="server" Text="Simpan"></asp:Button>
                    <asp:Button ID="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:Button>
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>
        <br />
        <div id="div1" style="overflow: auto; height: 300px">
            <asp:DataGrid ID="dgEventCity" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" GridLines="Horizontal"
                CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True"
                PageSize="25" AllowPaging="True" BorderStyle="None" DataKeyField="ID" ShowFooter="false">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="White" HorizontalAlign="Center"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="No">
                        <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kode Kota" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblKodeKota" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Kota" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle ForeColor="White" Width="20%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNamaKota" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
                        <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Style="display: none"></asp:Label>
                            <img id="imgActif" runat="server" alt="Aktif" src="../images/aktif.gif" border="0">
                            <img id="imgNonActif" runat="server" alt="Non-aktif" src="../images/in-aktif.gif" border="0">
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnDetail" runat="server" CommandName="Detail" CausesValidation="False" Visible="false">
												            <img src="../images/detail.gif" border="0" alt="Detail"></asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="Edit" CausesValidation="False">
												            <img src="../images/edit.gif"  border="0" alt="Ubah"></asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnDelete" OnClientClick="return confirm('Anda yakin mau hapus?');" CausesValidation="False"
                                CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Delete" Text="Hapus" runat="server">
                                        <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>
    </form>
</body>
</html>
