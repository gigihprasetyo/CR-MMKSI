<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmSPKMatching.aspx.vb" Inherits="FrmSPKMatching" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Register TagPrefix="uc1" TagName="Clock" Src="../UserControl/Clock.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title>FrmSPKMatching</title>
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function TxtBlur(objTxt) {
            omitSomeCharacter(objTxt, '<>?*%$');
        }
        function TxtKeypress() {
            return alphaNumericExcept(event, '<>?*%$;')
        }
        function ShowPPDealer() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }
        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }
        function ShowPPTipe(tipe) {
            showPopUp('../PopUp/PopUpVechileType.aspx?CategoryID=0&IsActive=A', '', 500, 760, VechileTypeSelection);
        }
        function VechileTypeSelection(SelectedVechileType) {
            var tempParam = SelectedVechileType.split(';');
            var selectedType = replace(tempParam[0], ' ', '');
            var txtVehicleTypeSelection = document.getElementById("txtVehicleType");
            txtVehicleTypeSelection.value = selectedType;
            var txtKodeWarnaSelection = document.getElementById("txtKodeWarna");
            txtKodeWarnaSelection.value = '';
        }
        function ShowPPKodeWarna() {
            var txtVehicleTypeSelection = document.getElementById("txtVehicleType");
            showPopUp('../General/FrmKodeWarna.aspx?type=' + txtVehicleTypeSelection.value + '&pktype=0', '', 400, 400, KodeWarnaSelection)
        }
        function KodeWarnaSelection(selectedColor) {
            var tempParam = selectedColor.split(';');
            var warna = replace(tempParam[0], ' ', '');
            var txtKodeWarnaSelection = document.getElementById("txtKodeWarna");
            txtKodeWarnaSelection.value = warna;
        }
        function ShowPPCustomerList() {
            showPopUp('../PopUp/PopUpCustomerList.aspx?Tyjiuy678=customercode', '', 500, 760, CustomerSelection);
        }        
        function CustomerSelection(selectedCustomer) {
            var txtRefKodePelanggan = document.getElementById("txtKodeKonsumen");
            txtRefKodePelanggan.value = selectedCustomer;
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" enctype="multipart/form-data" runat="server">
        <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td colspan="10">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage">Display SPK Matching</td>
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
                <td class="titleField" width="20%">Dealer</td>
                <td style="width: 13px" width="13">:</td>
                <td width="40%">
                    <asp:TextBox ID="txtKodeDealer" onkeypress="TxtKeypress();" onblur="TxtBlur('txtKodeDealer');"
                        runat="server" Width="152px"></asp:TextBox>&nbsp;<asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
							<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                </td>
                <td class="titleField" width="15%">Nomor SPK</td>
                <td width="1%">:</td>
                <td width="28%">
                    <asp:TextBox ID="txtNoSPK" onkeypress="TxtKeypress();" onblur="TxtBlur('txtNoSPK');" runat="server" Width="152px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="titleField">Nomor Rangka</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtNoChassis" onkeypress="TxtKeypress();" onblur="TxtBlur('txtNoChassis');"
                        runat="server" Width="152px"></asp:TextBox>
                </td>
                <td class="titleField">Kode Konsumen</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtKodeKonsumen" onkeypress="TxtKeypress();" onblur="TxtBlur('txtKodeKonsumen');"
                        runat="server" Width="152px"></asp:TextBox>&nbsp;
                        <asp:Label ID="lblPopupKodeKonsumen" runat="server" Width="16px">
							<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"/>
                        </asp:Label>
                </td>
            </tr>
            <tr>
                <td class="titleField">Nomor Mesin</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtNoMesin" onkeypress="TxtKeypress();" onblur="TxtBlur('txtNoMesin');"
                        runat="server" Width="152px"></asp:TextBox>
                </td>
                <td class="titleField">Nama</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtNama" onkeypress="TxtKeypress();" onblur="TxtBlur('txtNama');"
                        runat="server" Width="152px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField">Nomor Kunci</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtNoKunci" onkeypress="TxtKeypress();" onblur="TxtBlur('txtNoKunci');"
                        runat="server" Width="152px"></asp:TextBox>
                </td>
                <td class="titleField">Match No</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtMatchNo" onkeypress="TxtKeypress();" onblur="TxtBlur('txtMatchNo');"
                        runat="server" Width="152px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField">Kode Tipe</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtVehicleType" onkeypress="TxtKeypress();" onblur="TxtBlur('txtVehicleType');"
                        runat="server" Width="152px"></asp:TextBox>&nbsp;<asp:Label ID="lblPopupVehicleType" runat="server" Width="16px">
							<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                </td>
                <td class="titleField">Referensi No.</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtRefNo" onkeypress="TxtKeypress();" onblur="TxtBlur('txtRefNo');"
                        runat="server" Width="152px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField">Kode Warna</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtKodeWarna" onkeypress="TxtKeypress();" onblur="TxtBlur('txtKodeWarna');"
                        runat="server" Width="152px"></asp:TextBox>&nbsp;<asp:Label ID="lblPopupKodeWarna" runat="server" Width="16px">
							<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                </td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td class="titleField">Tanggal Matching</td>
                <td>:</td>
                <td>
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td style="width: 100px">
                                <cc1:IntiCalendar ID="ICDari" runat="server" TextBoxWidth="80"></cc1:IntiCalendar></td>
                            <td class="titleField">&nbsp;s.d&nbsp;</td>
                            <td>
                                <cc1:IntiCalendar ID="ICSampai" runat="server" TextBoxWidth="80"></cc1:IntiCalendar></td>
                        </tr>
                    </table>
                </td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Button ID="btnCari" runat="server" Width="50px" Text="Cari"></asp:Button>
                    <asp:Button ID="btnMatch" runat="server" Width="50px" Text="Match"></asp:Button>
                    <asp:Button ID="btnUnMatch" runat="server" Width="70px" Text="UnMatch"></asp:Button>
                </td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="6">
                    <div id="div1" style="overflow: auto; height: 260px">
                        <asp:DataGrid ID="dgSPKMatching" runat="server" Width="100%" CellSpacing="1" AllowPaging="True"
                            PageSize="25" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Horizontal"
                            AllowSorting="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkChooseItem" runat="server" Width="5px"></asp:CheckBox>
                                        <%--<asp:TextBox ID="txtSONumber" runat="server" Width="0px" Style="visibility: hidden;" Text='<%# DataBinder.Eval(Container, "DataItem.SONumber") %>'>
                                        </asp:TextBox>--%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No.">
                                    <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndex" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="DealerCode" SortExpression="DealerCode" HeaderText="Dealer">
                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MatchingDate" SortExpression="MatchingDate" HeaderText="Tanggal Matching" DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <%--<asp:BoundColumn DataField="CustomerCode" HeaderText="Kode Konsumen" SortExpression="CustomerCode">
                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Name" HeaderText="Nama" SortExpression="Name">
                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>--%>
                                <asp:BoundColumn DataField="SPKNumber" HeaderText="Nomor SPK" SortExpression="SPKNumber">
                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ChassisNumber" HeaderText="Nomor Rangka" SortExpression="ChassisNumber">
                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="KeyNumber" HeaderText="Nomor Kunci" SortExpression="KeyNumber">
                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="VehicleTypeCode" HeaderText="Kode Tipe" SortExpression="VehicleTypeCode">
                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ColorCode" HeaderText="Kode Warna" SortExpression="ColorCode">
                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Description" HeaderText="Deskripsi Produk" SortExpression="Description">
                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MatchingNumber" HeaderText="Match No" SortExpression="MatchingNumber">
                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MatchingType" HeaderText="Match Type" SortExpression="MatchingType">
                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ReferenceNumber" HeaderText="Referensi No." SortExpression="ReferenceNumber">
                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:button id="btnDownload" runat="server" Width="80px" Text="Download" Enabled="False"></asp:button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
