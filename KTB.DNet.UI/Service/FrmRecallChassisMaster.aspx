<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmRecallChassisMaster.aspx.vb" Inherits=".FrmRecallChassisMaster" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Recall Master Data</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <link rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">

    <script type="text/javascript" language="javascript">
        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerAlokasi = document.getElementById("txtDealerAlokasi");
            txtDealerAlokasi.value = selectedDealer;
        }

        function ShowPPDealerSelectionService() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx?All', '', 500, 760, DealerSelectionService);
        }

        function DealerSelectionService(selectedDealerService) {
            var txtDealerService = document.getElementById("txtDealerService");
            txtDealerService.value = selectedDealerService;
        }

        function ShowPPRecallCategorySelection() {
            showPopUp('../General/../PopUp/PopUpReacallCategorySelection.aspx', '', 500, 760, RecallCategorySelection);
        }

        function RecallCategorySelection(selectedRecallRegNo) {
            var RecallRegNo = document.getElementById("txtRecallRegNo");
            RecallRegNo.value = selectedRecallRegNo;
        }

        function CheckAll(aspCheckBoxID, checkVal) {
            re = new RegExp(':' + aspCheckBoxID + '$')
            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox') {
                    if (re.test(elm.name)) {
                        elm.checked = checkVal
                    }
                }
            }
        }
    </script>
    <style type="text/css">       
        .DataGridFixedHeader {background-color: white; position:relative; top:expression(this.offsetParent.scrollTop);}
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" runat="server">
        <div>
            <table id="Table1" border="0" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td class="titlePage">Field Fix Campaign – Master Data Campaign</td>
                </tr>
                <tr>
                    <td height="1" background="../images/bg_hor.gif">
                        <img border="0" src="../images/bg_hor.gif" height="1"/></td>
                </tr>
                <tr>
                    <td height="10">
                        <img border="0" src="../images/dot.gif" height="1"/></td>
                </tr>
                <tr>
                    <td valign="top" align="left">
                        <table id="Table2" border="0" cellspacing="2" cellpadding="1" width="100%">
                            <tr>
                                <td class="titleField" width="20%">Lokasi File</td>
                                <td width="1%">:</td>
                                <td width="79%" colspan="4">
                                    <input id="DataFile" onkeypress="return false;" size="29" type="file" name="File1" runat="server">
                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" Width="60px"></asp:Button></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="LnkTemplate" runat="server">Download Template</asp:LinkButton></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="BtnCancel" runat="server" Text="Batal" Width="60px"></asp:Button>
                                    <asp:Button ID="btnStore" runat="server" Text="Simpan" Width="60px" Enabled="False"></asp:Button></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>

                            <tr>
                                <td class="titleField">&nbsp;</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>

                            <tr>
                                <td class="titleField">Dealer Alokasi</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="txtDealerAlokasi" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                        runat="server" Width="219px" ></asp:TextBox>
                                    <asp:Label ID="lblSearchDealerAlokasi" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                </td>
                                
                                <td>

                                <td></td>
                                <td></td>
                            </tr>

                            <tr>
                                <td class="titleField">Dealer Service</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="txtDealerService" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                        runat="server" Width="219px" ></asp:TextBox>
                                    <asp:Label ID="lblSearchDealerService" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                </td>
                                
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>

                            <tr>
                                <td class="titleField">Field Fix Campaign Reg No</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="txtRecallRegNo" runat="server" Width="219px" ></asp:TextBox>
                                    <asp:Label ID="lblRecallRegNo" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                </td>
                                <td>

                                <td></td>
                                <td></td>
                            </tr>

                            <tr>
                                <td class="titleField">No Rangka</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="txtChassisNumber"
                                        runat="server" Width="219px" ></asp:TextBox></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="titleField">Status Service</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="ddlIsService" runat="server">
                                        <asp:ListItem Value="-1" Text="-Silahkan Pilih-"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="Sudah Service"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Belum Service"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan="2">
                                    <br />
                                    <asp:Button Style="z-index: 0" ID="btnSearch" runat="server" Text="Cari" Width="60px"></asp:Button></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <div style="overflow: auto; height: 330px" id="div1">
                            <asp:DataGrid ID="dgRecallUpload" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#E0E0E0"
                                BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Horizontal"
                                Visible="False" CellSpacing="1" AllowSorting="false">
                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                                <ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
                                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                <Columns>
                                    <asp:TemplateColumn HeaderText="No">
                                        <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="ChassisNo" HeaderText="No Rangka">
                                        <HeaderStyle Width="50%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisNo")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Field Fix Campaign Reg No">
                                        <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RecallRegNo")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:BoundColumn DataField="ErrorMessage" HeaderText="Pesan">
                                        <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    </asp:BoundColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                            <asp:DataGrid ID="dgRecallMaster" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="3" 
                                BackColor="#E0E0E0" AllowSorting="True" AllowCustomPaging="True" PageSize="25"
                                AllowPaging="True" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px">
                                <%--runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#E0E0E0"
                                BorderStyle="None" BorderWidth="0px" BackColor="#E0E0E0" CellPadding="3" GridLines="Horizontal"
                                CellSpacing="1" AllowSorting="True" AllowPaging="True" AllowCustomPaging="True" PageSize="25">--%>
                                <%--<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                                <ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
                                <HeaderStyle CssClass="ms-formlabel DataGridFixedHeader" Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>--%>
                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
                                <FooterStyle ForeColor="Black" BackColor="#ededed"></FooterStyle>
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                        <HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderTemplate>
                                            <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkSelection',
														document.forms[0].chkAllItems.checked)" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelection" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelection_CheckedChanged"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="No">
                                        <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                                            <asp:Label ID="lblID" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>' runat="server" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="ChassisNo" HeaderText="No Rangka" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisNo")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="RecallCategory.RecallRegNo" HeaderText="Field Fix Campaign Reg No" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RecallCategory.RecallRegNo")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="RecallCategory.BuletinDescription" HeaderText="No Buletin">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RecallCategory.BuletinDescription")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn  HeaderText="Dealer Alokasi">
                                        <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDealerAlokasi">
                                            <%--<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RecallCategory.BuletinDescription")%>'>--%>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Dealer Service">
                                        <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDealerService">
                                            <%--<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RecallService.ServiceDealerID")%>'>--%>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Is Service">
                                        <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbIsService" runat="server" Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Aksi">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnDelete" runat="server" Text="Del" CommandName="Delete">
												<img src="../images/trash.gif" alt="Hapus" border="0"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="height: 40px" align="left">
                        <asp:Button ID="btnDownload" runat="server" Width="100px" Text="Download"></asp:Button>
                        &nbsp;&nbsp;<asp:Label ID="lblLoading" ForeColor="Red" runat="server"></asp:Label>
                        <asp:Button ID="btnDownloadAll" runat="server" Width="100px" Text="Download All"></asp:Button>
                        &nbsp;&nbsp;<asp:Label ID="lblLoadingAll" ForeColor="Red" runat="server"></asp:Label>
                    </td>                    
                </tr>                
            </table>
        </div>
    </form>

    <script type="text/C#" language="javascript">
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
