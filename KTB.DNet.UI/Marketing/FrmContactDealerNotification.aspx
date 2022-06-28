<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmContactDealerNotification.aspx.vb" Inherits="FrmContactDealerNotification" SmartNavigation="False" %>
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
    <style type="text/css">
        .auto-style1 {
            width: 219px;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Kontak Dealer Notifiaksi Customer Case&nbsp;</td>
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
                                &nbsp;</td>
                            <td width="1%">&nbsp;</td>
                            <td width="30%">
                                &nbsp;</td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" height="20">
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="top">
                                        <td>
                                            &nbsp;</td>
                                        <td valign="bottom">&nbsp;&nbsp;&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblDealerInfo" runat="server" Text="Dealer info"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="titleField">
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="titleField" width="18%">
                                <asp:Label ID="lblddlPosition" runat="server">Posisi</asp:Label></td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:DropDownList ID="ddlPosition" Width="152px" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </td>
                            <td class="titleField" width="18%">
                                &nbsp;</td>
                            <td width="1%">&nbsp;</td>
                            <td width="30%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="titleField" width="18%">
                                <asp:Label ID="Label5" runat="server">Nomor Handphone</asp:Label></td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:TextBox ID="txtPhoneNumber" runat="server" MaxLength="25" 
                                    onkeypress="return alphaNumericExcept(event,'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ)" 
                                    ToolTip="Dealer Search 1" Width="152px" Height="20px"></asp:TextBox>
                            </td>
                            <td class="titleField" width="18%">
                                &nbsp;</td>
                            <td width="1%">&nbsp;</td>
                            <td width="30%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="18%">Lokasi ubah :</td>
                            <td width="1%"></td>
                            <td width="40%"></td>
                        </tr>
                        <tr>
                            <td width="18%"> Sales Employee</td>
                            <td width="1%">=</td>
                            <td width="40%">Ubah data di SFD - Tenaga Penjual - Daftar Tenaga Penjual</td>
                        </tr>
                        <tr>
                            <td width="18%">Customer Satisfaction Employee</td>
                            <td width="1%">=</td>
                            <td width="40%">Ubah data di Training - Customer Satisfaction Team - Daftar CS Team</td>
                        </tr>
                        <tr>
                            <td width="18%"> Blank</td>
                            <td width="1%">=</td>
                            <td width="40%">Silahkan menghubungi (021) 4786-7575 atau admin.d-net@mitsubishi-motors.co.id</td>
                        </tr>
                    </table>
                    <table>
                        
                    </table>
                </td>
            </tr>
        </table>
        <div id="div1" style="overflow: auto; height: 320px">
            <asp:DataGrid ID="dgContactDealer" runat="server" Width="80%" DataKeyField="ID" BorderStyle="None"
                AllowPaging="True" PageSize="25" AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px"
                BackColor="White" CellPadding="3" GridLines="Horizontal" ForeColor="Gray" CellSpacing="1">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="#F1F6FB"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" VerticalAlign="Top" BackColor="White"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="titleTableSales" BackColor="#000084"></HeaderStyle>
                <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="Kode Dealer">
                        <HeaderStyle CssClass="titleTableMrk" Width="10%"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Dealer">
                        <HeaderStyle CssClass="titleTableMrk" Width="30%"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No. Telepon">
                        <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPhone" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Posisi">
                        <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPosition" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Lokasi Ubah">
                        <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblChgLocation" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>
    </form>
</body>
</html>
