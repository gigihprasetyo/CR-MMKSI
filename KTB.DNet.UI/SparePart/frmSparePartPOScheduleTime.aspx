<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmSparePartPOScheduleTime.aspx.vb" Inherits=".frmSparePartPOScheduleTime" SmartNavigation="False" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
    <title>Pemesanan - Jadwal Proses Pemesanan (JAM)</title>
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

        function ClearDealerSelection() {
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
                    <th class="titlePage" style="text-align: left">Pemesanan &nbsp;-&nbsp; Jadwal Proses Pemesanan&nbsp;(JAM)</th>
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
                                <tr runat="server" id="row1">
                                    <td class="auto-style1" width="24%" valign="top"><asp:label runat="server" ID="lblHkd">Kode Dealer</asp:label> </td>
                                    <td width="1%" class="auto-style2"><asp:label runat="server" ID="lblHkdt">:</asp:label> </td>
                                    <td width="75%" class="auto-style2">
                                        <asp:TextBox ID="txtDealerName" runat="server" Width="222px" MaxLength="50000"
                                            TextMode="MultiLine" Height="43px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titleField" width="24%">Jenis Order</td>
                                    <td width="1%">:</td>
                                    <td width="75%">
                                        <asp:DropDownList ID="lboxOrderType" runat="server" Height="16px" Width="184px"></asp:DropDownList>
                                        <%--<asp:listbox id="lboxOrderType" runat="server" Width="184px" Rows="2" SelectionMode="Multiple"></asp:listbox>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titleField" width="24%">Hari</td>
                                    <td width="1%">:</td>
                                    <td width="75%">
                                        <asp:DropDownList ID="lboxHari" runat="server" Height="16px" Width="184px"></asp:DropDownList>
                                        <%--<asp:listbox id="lboxHari" runat="server" Width="184px" Rows="3" SelectionMode="Multiple"></asp:listbox>--%>
                                    </td>
                                </tr>

                                <tr runat="server" id ="row2">
                                    <td class="titleField" width="24%"><asp:label runat="server" ID="lblHour">Jam</asp:label></td>
                                    <td width="1%">:</td>
                                  
                                    <td width="75%">
                                        <Table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>  <asp:DropDownList ID="ddlHour" runat="server" Height="16px" Width="184px"></asp:DropDownList></td>
                                                  <td><asp:label runat="server" ID="lblHours">:</asp:label></td>
                                                  <td>  <asp:DropDownList ID="ddlMinute" runat="server" Height="16px" Width="184px"></asp:DropDownList></td>
                                            </tr>
                                        </Table>
                                      
                                        <%--<asp:listbox id="lboxHari" runat="server" Width="184px" Rows="3" SelectionMode="Multiple"></asp:listbox>--%>
                                    </td>
                                </tr>


                                <tr>
                                    <td colspan="3"></td>
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
                                          &nbsp;
											<asp:Button ID="BtnBack" runat="server" Width="64px" Text="Kembali" CausesValidation="False"></asp:Button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <div id="div1" style="height: 250px; overflow: auto">
                            <asp:DataGrid ID="dtgSPOT" runat="server" Width="50%" AutoGenerateColumns="False" BorderStyle="None"
                                BorderWidth="0px" BackColor="#CDCDCD" GridLines="None" BorderColor="#CDCDCD" CellPadding="3" AllowCustomPaging="True" AllowPaging="True" AllowSorting="True" CellSpacing="1"
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


                                    <asp:TemplateColumn HeaderText="JAM" SortExpression="ScheduleTime">
                                        <HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text="" ID="lblJAM"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
 

                                    <asp:TemplateColumn>
                                        <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
													<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                            <asp:LinkButton ID="lbtnDelete" runat="server" Width="16px" Text="Hapus" CausesValidation="False"
                                                CommandName="Delete">
													<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>

                                             <asp:LinkButton ID="lbtnActive" CausesValidation="False" runat="server" Text="Aktif" CommandName="aktif"
                                                ToolTip="Aktivkan">
												<img border="0" src="../images/in-aktif.gif" alt="Aktifkan" style="cursor:hand"></asp:LinkButton>

                                            <asp:LinkButton ID="lbtnInactive" CausesValidation="False" runat="server" Text="Non-aktif" CommandName="inaktif"
                                                ToolTip="Non Aktivkan">
												<img border="0" src="../images/aktif.gif" alt="Non-aktifkan" style="cursor:hand"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr><td>
                    <div id="div1" style="OVERFLOW: auto; HEIGHT: 260px"><asp:datagrid id="dtgDealerSelection" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="2" 
											AllowSorting="True">
											<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
											<HeaderStyle ForeColor="white" BackColor="#CC3333" Font-Bold=True HorizontalAlign="Center"></HeaderStyle>
											<Columns>
												  <asp:TemplateColumn HeaderText="No">
                                        <HeaderStyle Width="2%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNo"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="DealerCode" HeaderText="Kode Dealer">
													<HeaderStyle Width="7%" ></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblDealerCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="DealerName" SortExpression="DealerName" HeaderText="Nama Dealer">
													<HeaderStyle Width="21%" ></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="City.CityName" HeaderText="Kota">
													<HeaderStyle Width="10%" ></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblCity" runat="server" Text=""></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="DealerGroup.GroupName" HeaderText="Grup">
													<HeaderStyle Width="10%" ></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblGroup" runat="server" Text=""></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="SearchTerm1" SortExpression="SearchTerm1" HeaderText="Term Cari 1">
													<HeaderStyle Width="9%" ></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="SearchTerm2" SortExpression="SearchTerm2" HeaderText="Term Cari 2">
													<HeaderStyle Width="13%" ></HeaderStyle>
												</asp:BoundColumn>
                                                <asp:TemplateColumn SortExpression="Area1.Description" HeaderText="Area 1">
													<HeaderStyle Width="7%" ></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblAre1e runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Area1.Description") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></div>
                    </td></tr>
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
