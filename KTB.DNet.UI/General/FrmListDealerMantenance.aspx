<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmListDealerMantenance.aspx.vb" Inherits="FrmListDealerMantenance" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmListDealerMantenance</title>
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
                <td class="titlePage">ADMIN SISTEM&nbsp;- Daftar Organisasi</td>
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
                            <td width="40%">
                                <asp:TextBox ID="txtKodeDealer" Width="250px" runat="server"></asp:TextBox><asp:Label ID="lblPopUpDealer" runat="server" Width="10">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label></td>
                            <td class="titleField" width="14%">Template Dokumen Upload</td>
                            <td width="1%">:</td>
                            <td><asp:LinkButton ID="LnkDownloadTemplate" runat="server" ToolTip="Download Template Excell">Template File Update Status Publish</asp:LinkButton></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="14%">Nama Dealer</td>
                            <td width="1%">:</td>
                            <td width="40%">
                                <asp:TextBox ID="txtDealerName" Width="250px" runat="server"></asp:TextBox></td>
                            <td class="titleField" width="14%">Update Status Publish</td>
                            <td width="1%">:</td>
                            <td width="45%"><asp:FileUpload ID="fileUploadExcel" runat="server" ></asp:FileUpload>
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" Width="70px" OnClientClick="return confirm('Apakah anda yakin akan melanjutkan proses upload data?');"></asp:Button></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="14%">Status</td>
                            <td width="1%">:</td>
                            <td width="40%">
                                <asp:DropDownList ID="ddlstatus" runat="server" Width="160px"></asp:DropDownList></td>
                            <td colspan="3"></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="14%">Area Bisnis</td>
                            <td width="1%">:</td>
                            <td width="40%">
                                <asp:CheckBox ID="cbSalesUnit" runat="server" Text=" Sales Unit" Font-Bold="True"></asp:CheckBox>&nbsp;&nbsp; 
									&nbsp;<asp:CheckBox ID="cbService" runat="server" Text=" Service" Font-Bold="True"></asp:CheckBox>&nbsp;&nbsp;&nbsp;<strong><asp:CheckBox ID="cbSparePart" runat="server" Text=" Spare Part"></asp:CheckBox></strong></td>
                            <td colspan="3"></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="14%">Status Publish</td>
                            <td width="1%">:</td>
                            <td width="40%">
                                <asp:DropDownList ID="ddlstatuspublish" runat="server" Width="160px"></asp:DropDownList></td>
                            <td colspan="3"></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="14%"></td>
                            <td width="1%"></td>
                            <td width="40%">
                                <asp:Button ID="btnSearch" runat="server" Text=" Cari " Font-Bold="True"></asp:Button></td>
                            <td colspan="3"></td>
                        </tr>
                    </table>
                    <%# DataBinder.Eval(Container, "DataItem.SearchTerm1")+"/"+DataBinder.Eval(Container, "DataItem.SearchTerm2") %>
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <div id="div1" style="overflow: auto; width: 100%; height: 380px">
                        <asp:DataGrid ID="dtgDealerList" runat="server" Width="100%" PageSize="25" AllowPaging="True"
                            AutoGenerateColumns="False" AllowCustomPaging="True" AllowSorting="True" BorderColor="#E0E0E0"
                            BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Vertical"
                            CellSpacing="1">
                            <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White" VerticalAlign="Top"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
                            <FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                    <HeaderStyle Width="1%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="1%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server" Width="20px" Font-Size="Smaller"></asp:Label>
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
                                        <asp:Label ID="Label2" runat="server" Width="68px" Text='<%# DataBinder.Eval(Container, "DataItem.City.CityName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="DealerGroup.GroupName" HeaderText="Grup">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Width="68px" Text='<%# DataBinder.Eval(Container, "DataItem.DealerGroup.GroupName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="SearchTerm1" HeaderText="Term Cari 1/2">
                                    <HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Width="52px" Text='<%# DataBinder.Eval(Container, "DataItem.SearchTerm1")+" / "+DataBinder.Eval(Container, "DataItem.SearchTerm2") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# IIf(CType(DataBinder.Eval(Container, "DataItem.Status"),string) = 0, "Tidak Aktif", "Aktif") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Publish" HeaderText="Status Publish">
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnNonActive" runat="server" Width="16px" OnClientClick="return confirm('Apakah anda akan mengubah status Publish data ini?');" CausesValidation="False" CommandName="Activate">
									<img src="../images/in-aktif.gif" border="0" alt="Klik untuk Publish data"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnActive" runat="server" Width="16px" OnClientClick="return confirm('Apakah anda akan mengubah status Publish data ini?');" CausesValidation="False" CommandName="Deactivate">
									<img src="../images/aktif.gif" border="0" alt="Klik untuk non-Publish data"></asp:LinkButton>                                        
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="SalesUnitFlag" HeaderText="Sales Unit">
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbSalesUnitdtg" runat="server" BackColor="Transparent" Enabled="false" Checked='<%# DataBinder.Eval(Container, "DataItem.SalesUnitFlag") %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ServiceFlag" HeaderText="Service">
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbServicesdtg" runat="server" Enabled="false" Checked='<%# DataBinder.Eval(Container, "DataItem.ServiceFlag") %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="SparepartFlag" HeaderText="Spare Part">
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbSparePartdtg" runat="server" Enabled="false" Checked='<%# DataBinder.Eval(Container, "DataItem.SparepartFlag") %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="User Aktif">
                                    <HeaderStyle Width="6%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserActive" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="LastUpdateTime" SortExpression="LastUpdateTime" ReadOnly="True" HeaderText="Diubah Tgl"
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" Width="16px" Text="Lihat" ToolTip="Lihat Profile Organisasi"
                                            CommandName="view">
												<img src="../images/detail.gif" border="0"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnUpdate" Visible="false" runat="server" Width="16px" Text="Lihat" ToolTip="Lihat Profile Organisasi"
                                            CommandName="edit">
												<img src="../images/edit.gif" border="0"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnHakAkses" runat="server" ToolTip="Ubah Hak Akses Organisasi" CommandName="HakAkses"
                                            CausesValidation="False">
												<img src="../images/lock.jpg" border="0"></asp:LinkButton>
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
