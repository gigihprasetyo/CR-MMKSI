<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmCustomerCase.aspx.vb" Inherits="FrmCustomerCase" SmartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Case Management</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script type="text/javascript" language="javascript">
        function ShowPopUp() {
        }
        function ShowPPDealerSelection() {
            showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx?x=Territory', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer.split(';');
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = tempParam[0];
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Customer Satisfaction&nbsp;- Case Management</td>
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
                            <td class="titleField" width="18%">
                                <asp:Label ID="lblDealerSearch" runat="server">Kode Dealer</asp:Label></td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
                                    runat="server"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:Label></td>
                            <td class="titleField" width="18%">
                                <asp:Label ID="Label1" runat="server">Kategori</asp:Label></td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:DropDownList ID="ddlCategory" Width="200px" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" height="20">
                                <asp:Label ID="lblTglDeliverySearch" runat="server">Tanggal Case</asp:Label></td>
                            <td>:</td>
                            <td>
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="top">
                                        <td>
                                            <cc1:inticalendar id="icStartDate" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
                                        <td valign="bottom">&nbsp;&nbsp;s.d&nbsp;&nbsp;</td>
                                        <td>
                                            <cc1:inticalendar id="icEndDate" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
                                    </tr>
                                </table>
                            </td>
                            <td class="titleField">
                                <asp:Label ID="lblReffDO" runat="server">Sub Kategori 1</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlCategory1" Width="200px" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="18%">
                                <asp:Label ID="Label2" runat="server">Nama Konsumen</asp:Label></td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtKonsumen" runat="server" MaxLength="25"
                                            Width="152px" ToolTip="Dealer Search 1"></asp:textbox></td>
                            <td class="titleField" width="18%">
                                <asp:Label ID="lblStatusTitle" runat="server">Sub Kategori 2</asp:Label></td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:DropDownList ID="ddlCategory2" Width="200px" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="18%">
                                <asp:Label ID="Label3" runat="server">Nomor Polisi</asp:Label></td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:TextBox ID="txtNoPolisi" runat="server" MaxLength="25" onkeypress="return HtmlCharUniv(event)" ToolTip="Dealer Search 1" Width="152px"></asp:TextBox>
                            </td>
                            <td class="titleField" width="18%">
                                <asp:Label ID="Label4" runat="server">Sub Kategori 3</asp:Label></td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:DropDownList ID="ddlCategory3" Width="200px" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="18%">
                                <asp:Label ID="Label5" runat="server">Nomor Rangka</asp:Label></td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:TextBox ID="txtNoRangka" runat="server" MaxLength="25" onkeypress="return HtmlCharUniv(event)" ToolTip="Dealer Search 1" Width="152px"></asp:TextBox>
                            </td>
                            <td class="titleField" width="18%">
                                <asp:Label ID="Label6" runat="server">Sub Kategori 4</asp:Label></td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:DropDownList ID="ddlCategory4" Width="200px" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="18%">
                                <asp:Label ID="Label7" runat="server">Nomor Mesin</asp:Label></td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:TextBox ID="txtNoMesin" runat="server" MaxLength="25" onkeypress="return HtmlCharUniv(event)" ToolTip="Dealer Search 1" Width="152px"></asp:TextBox>
                            </td>
                            <td class="titleField" width="18%">
                                <asp:Label ID="Label8" runat="server" visible="true">Status</asp:Label></td>
                            <td width="1%"></td>
                            <td width="30%">
                                <asp:DropDownList ID="ddlStatus" Width="200px" runat="server" visible="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="18%">
                                <asp:Label ID="Label9" runat="server">Nomor Case</asp:Label></td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:TextBox ID="txtNoCase" runat="server" MaxLength="25" onkeypress="return HtmlCharUniv(event)" ToolTip="Dealer Search 1" Width="152px"></asp:TextBox>
                            </td>
                            <td class="titleField" width="18%"></td>
                            <td width="1%"></td>
                            <td width="30%"></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:Button>
                                <asp:Button ID="btnDownload" runat="server" Width="60px" Text=" Download "></asp:Button>
                            </td>
                            <td align="right" colspan="3">
                                <asp:Label class="titleField" ID="lblTotalUnit" runat="server">Jumlah Data : </asp:Label><asp:Label ID="lblTotalData" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="div1" style="overflow: auto; height: 320px">
            <asp:DataGrid ID="dgList" runat="server" Width="100%" DataKeyField="ID" BorderStyle="None"
                AllowPaging="True" PageSize="25" AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px"
                BackColor="White" CellPadding="3" GridLines="Horizontal" ForeColor="Gray" CellSpacing="1">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="#F1F6FB"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" VerticalAlign="Top" BackColor="White"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="titleTableSales" BackColor="#000084"></HeaderStyle>
                <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
                    <asp:BoundColumn DataField="SalesforceID" SortExpression="SalesforceID" Visible="false">
                        <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="No">
                        <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <%--<asp:BoundColumn DataField="Dealer.DealerCode" SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                        <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                    </asp:BoundColumn>--%>
                    <asp:BoundColumn DataField="CaseNumber" SortExpression="CaseNumber" HeaderText="Nomor Case">
                        <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="CaseDate" SortExpression="CaseDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Tanggal">
                        <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="CustomerName" SortExpression="CustomerName" HeaderText="Nama Konsumen">
                        <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Phone" SortExpression="Phone" HeaderText="Phone">
                        <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Email" SortExpression="Email" HeaderText="Email">
                        <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Category" SortExpression="Category" HeaderText="Kategori">
                        <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn SortExpression="Status" HeaderText="Status" visible="true">
                        <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle Width="1%" CssClass="titleTableMrk"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <%--<asp:Label ID="lblDetail" runat="server" Text="Detail"> <img alt="Data Detail" src="../images/popup.gif" style="cursor:hand" border="0"></asp:Label>--%>
                            <asp:LinkButton ID="lnkbtnEdit" runat="server" Text="Ubah" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Edit" CausesValidation="False">
									<img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnView" runat="server" Width="20px" Text="Lihat" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="View" CausesValidation="False">
									<img alt="Lihat" src="../images/detail.gif" border="0"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>
    </form>
</body>
</html>
