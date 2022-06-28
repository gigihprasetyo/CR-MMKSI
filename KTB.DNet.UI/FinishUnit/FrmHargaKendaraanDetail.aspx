<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmHargaKendaraanDetail.aspx.vb" Inherits=".FrmHargaKendaraanDetail" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmHargaKendaraanDetail</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
        function ShowPopUpTO() {

            showPopUp('../PopUp/PopUpEventNational.aspx?m=d', '', 430, 800, EventSelection);
        }

        function EventSelection(selectedRefNumber) {
            var hdnTemporaryEvent = document.getElementById("hdnTempEvent");
            var txtKodeEvent = document.getElementById("txtKodeEvent");
            var lblNamaEvent = document.getElementById("lblNamaEvent");
            hdnTemporaryEvent.value = selectedRefNumber;
            txtKodeEvent.value = selectedRefNumber.split(";")[0];
            lblNamaEvent.innerHTML = trim(selectedRefNumber.split(";")[3]);
            __doPostBack("txtKodeEvent", "");

        }

        function trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }

        function ShowPopUpDealer() {
            showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 430, 800, DealerSSS);
        }

        function DealerSSS(selectedRefNumber) {
            var txtKodeDealer = document.getElementById("txtKodeDealer");
            txtKodeDealer.value = selectedRefNumber.split(";")[0];

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Marketing -&nbsp; Harga Kendaraan Dealer Detail</td>
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
                            <td align="left" style="width: 50%">
                                <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                                    <tr valign="top">
                                        <td class="titleField" style="height: 18px">Kode Dealer</td>
                                        <td style="height: 18px">:</td>
                                        <td style="height: 18px">
                                            <asp:Label ID="lblKodeDealer" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td class="titleField" style="height: 18px">Nama Dealer</td>
                                        <td style="height: 18px">:</td>
                                        <td style="height: 18px">
                                            <asp:Label ID="lblNamaDealer" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td class="titleField" style="height: 18px">Customer CLass</td>
                                        <td style="height: 18px">:</td>
                                        <td style="height: 18px">
                                            <asp:Label ID="lblCustomerClass" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td class="titleField" style="height: 18px">Company</td>
                                        <td style="height: 18px">:</td>
                                        <td style="height: 18px">
                                            <asp:Label ID="lblCompany" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td class="titleField" style="height: 18px">Mata Uang</td>
                                        <td style="height: 18px">:</td>
                                        <td style="height: 18px">
                                            <asp:Label ID="lblMataUang" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%" valign="top">
                                <table id="Table4" cellspacing="1" cellpadding="2" width="70%" border="0">
                                    <tr valign="top">
                                        <td class="titleField" style="height: 18px">Tanggal Mulai Berlaku</td>
                                        <td style="height: 18px">:</td>
                                        <td style="height: 18px">
                                            <asp:Label ID="lblTanggalBerlaku" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td class="titleField" style="height: 18px">Tipe Konsumen DMS</td>
                                        <td style="height: 18px">:</td>
                                        <td style="height: 18px">
                                            <asp:Label ID="lblTipeDMS" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td class="titleField" style="height: 18px">Tipe Konsumen DNet</td>
                                        <td style="height: 18px">:</td>
                                        <td style="height: 18px">
                                            <asp:Label ID="lblTipeDNet" runat="server"></asp:Label>
                                        </td>
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
                    <%--<asp:Button ID="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:Button>--%>
                    <asp:Button ID="btnBack" runat="server" Width="60px" Text=" Kembali "></asp:Button>
                    <asp:Button ID="btnDownloadExcel" runat="server" Text=" Download Excel " Style="margin-left: 15px" ></asp:Button>
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>
        <br />
        <div id="div1" style="overflow: auto; height: 300px">
            <asp:DataGrid ID="dgVehiclePriceDetail" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" 
                CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True"
                PageSize="25" AllowPaging="True" BorderStyle="None" DataKeyField="ID" ShowFooter="false" >
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
                    <%--<asp:TemplateColumn HeaderText="Status" SortExpression="Status">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>--%>
                    <asp:TemplateColumn HeaderText="Kode Kendaraan" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblKodeKendaraan" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Kendaraan" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle ForeColor="White" Width="25%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNamaKendaraan" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Warna Kendaraan" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblWarnaKendaraan" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Harga Dasar" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblHargaDasar" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Harga Off The Road">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblOffTheRoad" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="BBN" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblBBN" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Harga On The Road">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblOnTheRoad" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Harga Warna Spesial" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblWarnaSpesial" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Down Payment" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDownPayment" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Pajak Konsumsi 1" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblKonsumsi1" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Jumlah Pajak Konsumsi 1" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblJumlahKonsumsi1" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Pajak Konsumsi 2" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblKonsumsi2" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Jumlah Pajak Konsumsi 2" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblJumlahKonsumsi2" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>
    </form>
</body>
</html>
