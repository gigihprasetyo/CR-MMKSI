<%@ Import Namespace="KTB.DNet.Domain" %>

<%@ Page Language="vb" AutoEventWireup="False" CodeBehind="FrmSPDODetail.aspx.vb" Inherits="FrmSPDODetail" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>PEMESANAN - SUMMARY PACKING LIST</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript">
        var indexRow;
        function BackToPrev() {
            var url = document.getElementById("txtUrlToBack").value;
            window.location = url;
        }
    </script>
</head>
<body onfocus="return checkModal()" onclick="checkModal()" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server" onfocus="return checkModal()" onclick="checkModal()">
        <table id="Table2" cellspacing="4" cellpadding="0" width="100%" border="0">
            <tr>
                <td height="20">
                    <table id="Table5" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage">Pemesanan - Sumary Packing List</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor.gif" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="Table1" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="24%">
                                <asp:Label ID="Label1" runat="server"> Kode Dealer</asp:Label></td>
                            <td width="1%">
                                <asp:Label ID="Label6" runat="server">:</asp:Label></td>
                            <td width="34%">
                                <asp:Label ID="lblDealerCode" runat="server"></asp:Label></td>
                            <td class="titleField" width="20%">
                                <asp:Label ID="Label11" runat="server">ETD (Estimation Time Delivery)</asp:Label></td>
                            <td width="1%">
                                <asp:Label ID="Label14" runat="server">:</asp:Label></td>
                            <td width="20%">
                                <asp:Label ID="lblETD" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label19" runat="server">Nama Dealer</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblDealerName" runat="server"></asp:Label>&nbsp;/
									<asp:Label ID="lblDealerTerm" runat="server"></asp:Label></td>
                            <td class="titleField">
                                <asp:Label ID="Label12" runat="server">Picking Date</asp:Label></td>
                            <td>
                                <asp:Label ID="Label18" runat="server">:</asp:Label></td>
                            <td>
                                <asp:Label ID="lblPickingDate" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label2" runat="server">Tipe Order</asp:Label></td>
                            <td>
                                <asp:Label ID="Label7" runat="server">:</asp:Label></td>
                            <td>
                                <asp:Label ID="lblOrderType" runat="server"></asp:Label></td>
                            <td class="titleField">
                                <asp:Label ID="Label13" runat="server">Packing Date</asp:Label></td>
                            <td>
                                <asp:Label ID="Label16" runat="server">:</asp:Label></td>
                            <td>
                                <asp:Label ID="lblPackingDate" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label3" runat="server">Nomor DO MMKSI - Tanggal</asp:Label></td>
                            <td>
                                <asp:Label ID="Label8" runat="server">:</asp:Label></td>
                            <td>
                                <asp:Label ID="lblDO" runat="server"></asp:Label></td>
                            <td class="titleField">Goods Issue Date</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblGoodIssueDate" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                           <td class="titleField">
                            <%--     <asp:Label ID="Label4" runat="server"></asp:Label></td>
                            Expired Claim Date
                            <td>
                                <asp:Label ID="Label9" runat="server">&nbsp</asp:Label></td>
                            <td>
                                <asp:Label ID="lblExpClaimDate" runat="server"></asp:Label></td>
                            
                            <td>--%>
                                <asp:Label ID="Label22" runat="server" Style="font-weight: 700">Cara Pembayaran</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblCaraPembayaran" runat="server"></asp:Label>
                                <asp:Label ID="lblExpClaimDate" runat="server"></asp:Label>

                            </td>
                            <td class="titleField">
                                <asp:Label ID="Label17" runat="server" Width="191px">Payment Date</asp:Label></td>
                            <td>
                                <asp:Label ID="Label15" runat="server">:</asp:Label></td>
                            <td>
                                <asp:Label ID="lblPaymentDate" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Style="font-weight: 700">Ready For Delivery Date</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblReadyForDeliveryDate" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <div id="div1" style="overflow: auto;">
                        <asp:DataGrid ID="dgSPDO" runat="server" Width="100%" AutoGenerateColumns="False" BorderWidth="0px"
                            CellSpacing="1" CellPadding="3" PageSize="10" BackColor="White" BorderColor="Black"
                            AllowPaging="True" AllowCustomPaging="True" AllowSorting="True">
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" Height="30px" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Surat Jalan" SortExpression="ExpeditionNo">
                                    <HeaderStyle HorizontalAlign="Center" Width="12%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkExpeditionNo" runat="server" Text='<%# Bind("ExpeditionNo")%>' CommandName="BindPackingList"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="ExpeditionName" HeaderText="Ekpedisi" SortExpression="ExpeditionName">
                                    <HeaderStyle Width="23%" CssClass="titleTableParts"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ATD" HeaderText="ATD" SortExpression="ATD" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                    <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="ETA">
                                    <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblETADate" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ATA">
                                    <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <cc1:IntiCalendar ID="calATADate" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSave" runat="server" CausesValidation="False" CommandName="Save">
												<img src="../images/download.gif" border="0" alt="Simpan ATA"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <table>
                        <tr>
                            <td colspan="3" ><asp:Label ID="Label23" runat="server">Keterangan background :</asp:Label></td>
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="TextBox2" Width="20px" runat="server" BackColor="white" BorderStyle="Solid" Enabled="False" ></asp:TextBox></td>
                            <td>:</td>
                            <td><asp:Label ID="Label24" runat="server">ATA belum terisi, tanggal hari ini < ETA</asp:Label></td>
                        </tr>
                            <tr>
                            <td><asp:TextBox ID="TextBox3" Width="20px" runat="server" BackColor="red" BorderStyle="Solid" Enabled="False" ></asp:TextBox></td>
                            <td>:</td>
                            <td><asp:Label ID="Label29" runat="server">ATA belum terisi, tanggal hari ini >= ETA</asp:Label></td>
                        </tr>
                            <tr>
                            <td><asp:TextBox ID="TextBox4" Width="20px" runat="server" BackColor="LightGreen" BorderStyle="Solid" Enabled="False" ></asp:TextBox></td>
                            <td>:</td>
                            <td><asp:Label ID="Label30" runat="server">ATA sudah terisi</asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="Table1" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="24%"></td>
                            <td width="1%"></td>
                            <td width="34%"></td>
                            <td class="titleField" width="20%">&nbsp;</td>
                            <td width="1%">&nbsp;</td>
                            <td width="20%" align="right">
                                <asp:Button ID="btnSaveATA" runat="server" Width="100px" Text="Simpan ATA" Visible="false"></asp:Button></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%">
                                <asp:Label ID="Label10" runat="server">Surat Jalan</asp:Label></td>
                            <td width="1%">
                                <asp:Label ID="Label20" runat="server">:</asp:Label></td>
                            <td width="34%">
                                <asp:TextBox ID="txtExpeditionNo" runat="server"></asp:TextBox></td>

                            <td class="titleField" width="20%">&nbsp;</td>
                            <td width="1%">&nbsp;</td>
                            <td width="20%" align="right">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label25" runat="server">Part Number</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtPartNumber" runat="server"></asp:TextBox></td>
                            <td class="titleField">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td align="right">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="titleField">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="btnCari" runat="server" Text="    Cari    " />
                            </td>
                            <td class="titleField">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td align="right">&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <div id="div1" style="overflow: auto;">
                        <asp:DataGrid ID="dtgPackingList" runat="server" Width="100%" AutoGenerateColumns="False" BorderWidth="0px"
                            CellSpacing="1" CellPadding="3" PageSize="50" BackColor="Gainsboro" BorderColor="Gainsboro">
                            <%--AllowPaging="True" AllowCustomPaging="True" AllowSorting="True">--%>
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                            <HeaderStyle Font-Bold="True" Height="30px" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Surat Jalan" SortExpression="ExpeditionNo">
                                    <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="LOT / CASE" SortExpression="LotCase">
                                    <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label21" runat="server" Text='<%# Eval("LotCase")%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="PackMaterial" HeaderText="Material Packing" SortExpression="PackMaterial">
                                    <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PackMaterialDesc" HeaderText="Description" SortExpression="PackMaterialDesc">
                                    <HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TotalQty" HeaderText="QTY" SortExpression="TotalQty">
                                    <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Weight" HeaderText="KG" SortExpression="Weight">
                                    <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Volume" HeaderText="M3" SortExpression="Volume">
                                    <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Detail">
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblDetail" runat="server" Text="Detail"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                </asp:TemplateColumn>
                            </Columns>
                            <%--<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>--%>
                        </asp:DataGrid>
                    </div>

                    <asp:Button ID="btnDownload" runat="server" Text="Download" />
                    <input onclick="BackToPrev();" type="button" value="Kembali">
                    <input style="width: 64px; height: 24px; display: none" onclick="window.close()" type="button" value="Tutup">
                    <asp:TextBox ID="txtUrlToBack" Style="visibility: hidden" ReadOnly="True" Text="" runat="server"></asp:TextBox>
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
