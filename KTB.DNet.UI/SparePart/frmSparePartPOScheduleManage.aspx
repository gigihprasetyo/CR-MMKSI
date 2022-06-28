<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmSparePartPOScheduleManage.aspx.vb" Inherits=".frmSparePartPOScheduleManage"  smartNavigation="False" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
    <title>Pemesanan - Jadwal Proses Pemesanan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js" type="text/javascript"></script>
    <script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
    <script>
        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelectionArea.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerName = document.getElementById("txtDealerName");
            txtDealerName.value = selectedDealer;
        }

        function ClearDealerSelection()
        {
            var txtDealerName = document.getElementById("txtDealerName");
            txtDealerName.value = "";
        }
    </script>
    <style type="text/css">
        .CustomGrid td {
            max-width: 320px;
            word-break: break-all;
        }
        .auto-style1 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            height: 61px;
        }
        .auto-style2 {
            height: 61px;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <thead>
                <tr>
                    <th class="titlePage" style="text-align: left">Pemesanan &nbsp;-&nbsp; Jadwal Proses Pemesanan</th>
                </tr>
                <tr>
                    <td background="../images/bg_hor.gif" height="1">
                        <img height="1" src="../images/bg_hor.gif" border="0"></td>
                </tr>
                <tr>
                    <td height="10">
                        <img height="1" src="../images/dot.gif" border="0"></td>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td valign="top" align="left">
                        <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="auto-style1" width="24%" valign="top">Kode Dealer</td>
                                    <td width="1%" class="auto-style2">:</td>
                                    <td width="75%" class="auto-style2">
                                        <asp:TextBox ID="txtDealerName" runat="server" Width="222px" MaxLength="50000"
                                            TextMode="MultiLine" Height="41px"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server" onclick="ShowPPDealerSelection()">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                            </asp:Label>

                                        <asp:Label ID="lblClear" runat="server" onclick="ClearDealerSelection()">
										<img src="../images/reload.gif" style="cursor:hand" border="0" alt="Clear">
                                            </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titleField" width="24%">Jenis Order</td>
                                    <td width="1%">:</td>
                                    <td width="75%">
                                        <asp:DropDownList id="lboxOrderType" runat="server" Height="16px" Width="184px"></asp:DropDownList>
                                        <%--<asp:listbox id="lboxOrderType" runat="server" Width="184px" Rows="2" SelectionMode="Multiple"></asp:listbox>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titleField" width="24%">Hari</td>
                                    <td width="1%">:</td>
                                    <td width="75%">
                                          <asp:DropDownList id="lboxHari" runat="server" Height="16px" Width="184px"></asp:DropDownList>
                                        <%--<asp:listbox id="lboxHari" runat="server" Width="184px" Rows="3" SelectionMode="Multiple"></asp:listbox>--%>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="3"></td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                    <td width="75%">
                                        <asp:Button ID="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:Button>
                                        &nbsp;
											<asp:Button ID="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:Button>
                                        &nbsp;
											<asp:Button ID="btnSearch" runat="server" Width="64px" Text="Cari" CausesValidation="False"></asp:Button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <div id="div1" style="height: 360px; overflow: auto">
                            <asp:DataGrid ID="dtgSparePartPOSchedule" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="None"
                                BorderWidth="0px" BackColor="#CDCDCD" GridLines="None" BorderColor="#CDCDCD" CellPadding="3"
                                PageSize="50" AllowCustomPaging="True" AllowPaging="True" AllowSorting="True" CellSpacing="1"
                                Font-Names="Microsoft Sans Serif" CssClass="CustomGrid">
                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                <AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
                                <ItemStyle ForeColor="Black" BackColor="#FDF1F2" Wrap="True"></ItemStyle>
                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BorderColor="#E0E0E0"
                                    BackColor="#CC3333"></HeaderStyle>
                                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                        <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle ForeColor="Gray" BackColor="LightPink"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="No">
                                        <HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNo"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>


                                    <asp:TemplateColumn HeaderText="Kode Dealer">
                                        <HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text="" ID="lblDealerCode"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                     
                                    <asp:TemplateColumn HeaderText="Tipe Order" SortExpression="OrderType">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text="" ID="lblOrderType"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                      <asp:TemplateColumn HeaderText="Hari" SortExpression="OrderDay">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text="" ID="lblHari"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn>
                                        <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
                                                CommandName="View" ToolTip="Lihat">
													<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
                                            <asp:LinkButton ID="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" 
                                                CommandName="Edit" ToolTip="Ubah">
													<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>

                                              <asp:LinkButton ID="LinkButton1" runat="server" Width="20px" Text="Ubah" CausesValidation="False" 
                                                  CommandName="EditJam" ToolTip="Jam">
													<img src="../images/alarm.png" border="0" alt="JAM"></asp:LinkButton>

                                            <asp:LinkButton ID="lbtnDelete" runat="server" Width="16px" Text="Hapus" CausesValidation="False"
                                                CommandName="Delete" ToolTip="Hapus">
													<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>

                                            <asp:LinkButton ID="lbtnActive" CausesValidation="False" runat="server" Text="Aktif" CommandName="aktif"
                                                ToolTip="Aktifkan">
												<img border="0" src="../images/in-aktif.gif" alt="Aktifkan" style="cursor:hand"></asp:LinkButton>

                                            <asp:LinkButton ID="lbtnInactive" CausesValidation="False" runat="server" Text="Non-aktif" CommandName="inaktif"
                                                ToolTip="Non Aktifkan">
												<img border="0" src="../images/aktif.gif" alt="Non-aktifkan" style="cursor:hand"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr valign="top">
                    <td valign="top"> <asp:Label ID="lblInfo" runat="server"></asp:Label> </td>
                </tr>
            </tbody>
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
