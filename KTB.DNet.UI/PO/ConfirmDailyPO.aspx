<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ConfirmDailyPO.aspx.vb" Inherits="ConfirmDailyPO" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>ConfirmDailyPO</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">

        function ConfirmDialog() {
            var ddl = document.getElementById("ddlAction");
            return confirm("Yakin mau melakukan proses" + ddl.outerText);
        }

        function InputPasswordPlease() {
            //alert("Silahkan Masukkan Password SAP Anda")
            showPPPassword();
        }
        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function showPPPassword() {
            showPopUp('../General/../PopUp/PopupInputPassword.aspx', '', 200, 600, GotPassword);
        }
        function GotPassword(result) {
            var txtUser = document.getElementById("txtUser");
            var txtPwd = document.getElementById("txtPass");
            var btn = document.getElementById("btnTransfer");
            var str = result;
            var username = '', pwd = '';

            username = str.split(';')[0];
            pwd = str.split(';')[1];

            txtUser.value = username;
            txtPwd.value = pwd;
            btn.click();
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }

        function ViewDailyPKFlow()
        { }

        function promptPassword() {
            var txt = document.getElementById("txtPass");
            var div = document.getElementById("divPassword");

            if (txt.value)

                div.style.display = "inherit";
            alert("Please, Enter Your SAP Password First!")
            txt.focus();
        }
        function onLoad() {
            var div = document.getElementById("divPassword");
            div.style.display = "none";
        }

    </script>
    <style type="text/css">
        .auto-style1 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            width: 239px;
            height: 26px;
        }

        .auto-style2 {
            width: 12px;
            height: 26px;
        }

        .auto-style3 {
            height: 26px;
        }
    </style>
</head>
<body onfocus="return checkModal()" onload="onLoad();" onclick="checkModal()" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <!--td class="titlePage">PurchasePO Harian -&nbsp;Proses Status PO</td-->
                <td class="titlePage">Purchase Order -&nbsp;Proses Status PO</td>
            </tr>
            <tr>
                <td height="1" background="../images/bg_hor.gif">
                    <img border="0" src="../images/bg_hor.gif" height="1"></td>
            </tr>
            <tr>
                <td height="10">
                    <img border="0" src="../images/dot.gif" height="1"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table2" border="0" cellspacing="1" cellpadding="2" width="100%">
                        <tr>
                            <td class="titleField" width="22%"></td>
                            <td width="1%"></td>
                            <td width="35%"></td>
                            <td style="width: 239px" class="titleField" width="239">Produk</td>
                            <td width="12" style="width: 12px">:</td>
                            <td width="20%">
                                <asp:DropDownList Style="z-index: 0" ID="ddlProductCategory" runat="server" Width="130px" AutoPostBack="True"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="22%">
                                <asp:Label ID="Label1" runat="server">Kode Dealer</asp:Label></td>
                            <td width="1%">
                                <asp:Label ID="Label4" runat="server">:</asp:Label></td>
                            <td width="35%">
                                <asp:TextBox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" ID="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                    runat="server"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>
                            <td style="width: 239px" class="titleField" width="239">
                                <asp:Label ID="Label8" runat="server">Kategori</asp:Label></td>
                            <td width="12" style="width: 12px">
                                <asp:Label ID="Label10" runat="server">:</asp:Label></td>
                            <td width="20%">
                                <asp:DropDownList ID="ddlCategory" runat="server" Width="140px"></asp:DropDownList></td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" rowspan="2">
                                <asp:Label ID="Label3" runat="server">Status</asp:Label></td>
                            <td rowspan="2">
                                <asp:Label ID="Label5" runat="server">:</asp:Label></td>
                            <td rowspan="2">
                                <asp:ListBox ID="lboxStatus" runat="server" Width="136px" Rows="3" SelectionMode="Multiple"></asp:ListBox>
                                <asp:Button ID="btnTest" runat="server" Text="TEST RFC PO" Style="display: none;" />
                            </td>
                            <td style="width: 239px" class="titleField">
                                <asp:Label ID="Label16" runat="server">Jenis Order</asp:Label></td>
                            <td style="width: 12px">
                                <asp:Label ID="Label11" runat="server">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlOrderType" runat="server" Width="140px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="auto-style1">
                                <asp:Label ID="Label7" runat="server"> Nomor PO</asp:Label></td>
                            <td class="auto-style2">
                                <asp:Label ID="Label17" runat="server">:</asp:Label></td>
                            <td class="auto-style3">
                                <asp:TextBox onblur="alphaNumericPlusSpaceBlur(txtPONumber)" ID="txtPONumber" onkeypress="return alphaNumericPlusSpaceUniv(event)"
                                    runat="server" Width="140px" MaxLength="20"></asp:TextBox></td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField">
                                <asp:Label ID="Label2" runat="server">Permintaan Kirim</asp:Label></td>
                            <td>
                                <asp:Label ID="Label6" runat="server">:</asp:Label></td>
                            <td>
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <cc1:inticalendar id="calDari" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                        <td>&nbsp;s.d&nbsp;</td>
                                        <td>
                                            <cc1:inticalendar id="calSampai" runat="server" TextBoxWidth="70" CanPostBack="False"></cc1:inticalendar></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 239px" class="titleField">
                                <asp:Label ID="Label13" runat="server" Font-Bold="True"> Nomor Reg PO</asp:Label></td>
                            <td style="width: 12px">:</td>
                            <td>
                                <asp:TextBox onblur="return alphaNumericPlusBlur(txtNoRegPO)" ID="txtNoRegPO" onkeypress="return alphaNumericPlusUniv(event)"
                                    runat="server" Width="140px" MaxLength="20"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><strong>Total Quantity</strong></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblQuantity" runat="server" Font-Bold="True"></asp:Label></td>
                            <td style="width: 239px" class="titleField">
                                <asp:Label ID="lblNoMO" runat="server"> Nomor O/C</asp:Label>&nbsp;</td>
                            <td style="width: 12px">:</td>
                            <td>
                                <asp:TextBox onblur="alphaNumericPlusBlur(txtNoMO)" ID="txtNoMO" onkeypress="return alphaNumericPlusUniv(event)"
                                    runat="server" Width="140px" MaxLength="20"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><strong>
                                <asp:Label ID="lblTotalHargaTebus" runat="server" Font-Bold="True"> Total Harga Tebus VH</asp:Label>&nbsp;</strong></td>
                            <td>
                                <asp:Label ID="Label15" runat="server">:</asp:Label></td>
                            <td>
                                <asp:Label ID="Label12" runat="server" Font-Bold="True">Rp</asp:Label>&nbsp;<asp:Label ID="lblTotalHargaTebusValue" runat="server" Font-Bold="True"></asp:Label></td>
                            <td style="width: 239px" class="titleField">
                                <asp:Label ID="Label18" runat="server">Cara Pembayaran</asp:Label></td>
                            <td style="width: 12px">
                                <asp:Label ID="Label14" runat="server">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlPaymentType" runat="server" Width="140px">
                                    <asp:ListItem Value="0" Selected="True">Silahkan Pilih</asp:ListItem>
                                    <asp:ListItem Value="1">COD</asp:ListItem>
                                    <asp:ListItem Value="2">TOP</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="height: 2px">
                                <asp:Label ID="Label19" runat="server" Font-Bold="True"> Total Harga Tebus PP</asp:Label></td>
                            <td style="height: 2px">
                                <asp:Label ID="Label20" runat="server">:</asp:Label></td>
                            <td style="height: 2px">
                                <asp:Label ID="Label21" runat="server" Font-Bold="True">Rp</asp:Label>&nbsp;<asp:Label ID="lblTotalHargaTebusPP" runat="server" Font-Bold="True"></asp:Label></td>
                            <td style="width: 239px; height: 2px"><strong>Bebas PPh</strong></td>
                            <td style="width: 12px; height: 2px">:</td>
                            <td style="height: 2px">
                                <asp:DropDownList ID="ddlFreePPh" runat="server" Width="140px">
                                    <asp:ListItem Value="0" Selected="True">Silahkan Pilih</asp:ListItem>
                                    <asp:ListItem Value="1">COD</asp:ListItem>
                                    <asp:ListItem Value="2">TOP</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="height: 2px">
                                <asp:Label ID="Label22" runat="server" Font-Bold="True"> Total Harga Tebus IT</asp:Label></td>
                            <td style="height: 2px">
                                <asp:Label ID="Label23" runat="server">:</asp:Label></td>
                            <td style="height: 2px">
                                <asp:Label ID="Label24" runat="server" Font-Bold="True">Rp</asp:Label>&nbsp;
									<asp:Label ID="lblTotalHargaTebusIT" runat="server" Font-Bold="True"></asp:Label></td>
                            <td style="width: 239px; height: 2px" width="239"><strong>
                                <asp:Label ID="lblFactoring" runat="server">Factoring</asp:Label></strong></STRONG></td>
                            <td style="height: 2px">
                                <asp:Label ID="lblFactoringColon" runat="server">:</asp:Label></td>
                            <td style="height: 2px">
                                <asp:DropDownList ID="ddlFactoring" runat="server" Width="140px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="height: 2px">
                                <asp:Label ID="Label25" runat="server" Font-Bold="True"> Total Harga Tebus LC</asp:Label></td>
                            <td style="height: 2px">
                                <asp:Label ID="Label27" runat="server">:</asp:Label></td>
                            <td style="height: 2px">
                                <asp:Label ID="Label28" runat="server" Font-Bold="True">Rp</asp:Label>&nbsp;
									<asp:Label ID="lblTotalHargaTebusLC" runat="server" Font-Bold="True"></asp:Label></td>
                            <td style="width: 239px; height: 2px" width="239"><strong>
                                <asp:Label ID="Label29" runat="server">Skema Pembayaran</asp:Label></strong></STRONG>
                            </td>
                            <td style="height: 2px">
                                <asp:Label ID="Label30" runat="server">:</asp:Label></td>
                            <td style="height: 2px">
                                <asp:DropDownList ID="ddlPaymentScheme" runat="server" Width="140px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="height: 2px"></td>
                            <td style="height: 2px"></td>
                            <td style="height: 2px">&nbsp;</td>
                            <td style="width: 239px; height: 2px" width="239"><strong>
                                <asp:Label Style="z-index: 0" ID="lblRegion" runat="server">Region</asp:Label></strong></td>
                            <td style="height: 2px">
                                <asp:Label Style="z-index: 0" ID="Label26" runat="server">:</asp:Label></td>
                            <td style="height: 2px">
                                <asp:DropDownList Style="z-index: 0" ID="ddlRegion" runat="server" Width="140px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="height: 2px"></td>
                            <td style="height: 2px"></td>
                            <td style="height: 2px">&nbsp;</td>
                            <td style="width: 239px; height: 2px" width="239"><strong>
                                <asp:Label Style="z-index: 0" ID="lblTahanDO" runat="server">Pernah Tahan DO</asp:Label></strong></td>
                            <td style="height: 2px">
                                <asp:Label Style="z-index: 0" ID="lblTahanDOColon" runat="server">:</asp:Label></td>
                            <td style="height: 2px">
                                <asp:DropDownList Style="z-index: 0" ID="ddlPernahTahanDO" runat="server" Width="140px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDuration" Style="color: #ffffff" runat="server"></asp:Label></td>
                            <td></td>
                            <td>
                                <asp:TextBox Style="visibility: visible" ID="txtColor" runat="server" Width="32px" BackColor="LightSkyBlue"></asp:TextBox>&nbsp;<strong>Pengecekan 
										Ceiling&nbsp;oleh NCD</strong></td>
                            <td style="width: 239px">
                                <asp:TextBox Style="visibility: visible" ID="txtColorGreen" runat="server" Width="32px" BackColor="PaleGreen"></asp:TextBox>&nbsp;<strong>
										Tahan DO</strong></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnFind" runat="server" Width="60px" Text="Cari"></asp:Button>

                                <br>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="6">
                                <div style="height: 270px; overflow: auto" id="div1">
                                    <asp:DataGrid ID="dtgPO" runat="server" Width="100%" BackColor="#CDCDCD" OnItemCommand="dtgPO_ItemCommand"
                                        OnItemDataBound="dtgPO_ItemDataBound" AutoGenerateColumns="False" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px" CellPadding="3"
                                        AllowSorting="True" AllowCustomPaging="True" PageSize="50" AllowPaging="True" OnPageIndexChanged="dtgPO_PageIndexChanged">
                                        <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="id" HeaderText="Id">
                                                <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <HeaderTemplate>
                                                    <input id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('chkSelect', document.all.chkAllItems.checked)"
                                                        type="checkbox">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn SortExpression="Status" HeaderText="Status">
                                                <HeaderStyle ForeColor="White" Width="6%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn SortExpression="ContractHeader.Dealer.DealerCode" HeaderText="Dealer">
                                                <HeaderStyle ForeColor="White" Width="6%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodeDealer" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="PONumber" SortExpression="PONumber" HeaderText="No Reg PO">
                                                <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="DealerPONumber" SortExpression="DealerPONumber" HeaderText="Nomor PO">
                                                <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn SortExpression="CreatedTime" HeaderText="Tgl Pengajuan">
                                                <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn SortExpression="ReqAllocationDateTime" HeaderText="Tgl Permintaan Kirim">
                                                <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn SortExpression="ContractHeader.ProjectName" HeaderText="Nama Pesanan Khusus">
                                                <HeaderStyle ForeColor="White" Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn SortExpression="ContractHeader.Category.CategoryCode" HeaderText="Kategori">
                                                <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn SortExpression="POType" HeaderText="Jenis Order">
                                                <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn SortExpression="TermOfPayment.Description" HeaderText="Cara Pembayaran">
                                                <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn HeaderText="Bebas PPh">
                                                <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn HeaderText="Pembayaran Tunai">
                                                <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn HeaderText="Harga Tebus VH (Rp)">
                                                <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn HeaderText="Harga Tebus PP (Rp)">
                                                <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn HeaderText="Harga Tebus IT (Rp)">
                                                <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn HeaderText="Harga Tebus LC (Rp)">
                                                <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Factoring">
                                                <HeaderStyle ForeColor="White" Width="6%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFactoring" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Skema">
                                                <HeaderStyle ForeColor="White" Width="6%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPaymentScheme" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="RemarkStatus" HeaderText="Keterangan">
                                                <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemark" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn Visible="False" HeaderText="Nama Dealer">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn Visible="False" HeaderText="Nomor Kontrak">
                                                <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn Visible="False">
                                                <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnFileName" runat="server" CommandName="Download">
															<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
                                                    <asp:Label ID="lblString" runat="server" Visible="False"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnOperator" runat="server" CommandName="Detail">
															<img src="../images/detail.gif" border="0" alt="Lihat Detail"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHistoryStatus" runat="server">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Lihat Perubahan Status"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFlow" runat="server">
															<img src="../images/alur_flow2.gif" style="cursor:hand" border="0" alt="Lihat Alur PO"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn Visible="false" HeaderText="POHeaderStatus">
                                                <HeaderStyle ForeColor="White" Width="6%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <table id="Table3" border="0" cellspacing="1" cellpadding="1" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Italic="True">Mengubah Status :</asp:Label>&nbsp;&nbsp;<asp:DropDownList ID="ddlAction" runat="server">
                                                <asp:ListItem Value="-1">Silahkan Pilih</asp:ListItem>
                                                <asp:ListItem Value="0">Hapus</asp:ListItem>
                                                <asp:ListItem Value="1">Konfirmasi</asp:ListItem>
                                                <asp:ListItem Value="2">Batal Konfirmasi</asp:ListItem>
                                                <asp:ListItem Value="3">Tolak</asp:ListItem>
                                                <asp:ListItem Value="4">Batal Tolak</asp:ListItem>
                                                <asp:ListItem Value="5">Rilis</asp:ListItem>
                                                <asp:ListItem Value="6">Batal Rilis</asp:ListItem>
                                                <asp:ListItem Value="7">Setuju</asp:ListItem>
                                                <asp:ListItem Value="8">Tidak Setuju</asp:ListItem>
                                                <asp:ListItem Value="9">Blok</asp:ListItem>
                                                <asp:ListItem Value="10">Batal Blok</asp:ListItem>
                                            </asp:DropDownList><asp:Button ID="btnProses" runat="server" Text="Proses"></asp:Button>

                                            <asp:Button ID="btnTransfer" runat="server" Text="Transfer"></asp:Button>
                                            <asp:Button ID="btnTransferUlang" runat="server" Text="Transfer Ulang"></asp:Button>
                                            <div id="divPassword" style="display: none;">
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td>SAP Password</td>
                                                        <td>:</td>
                                                        <td>
                                                            <asp:TextBox ID="txtUser" runat="server" Width="171px"></asp:TextBox>
                                                            <asp:TextBox ID="txtPass" runat="server" TextMode="Password" Width="171px"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </div>

                                            <asp:Button ID="btnPassTOP" runat="server" Text="Pass TOP"></asp:Button><asp:Button ID="CancelTOP" runat="server" Text="Batal Pass TOP"></asp:Button><asp:Button ID="btnDownload" runat="server" Text="Download"></asp:Button>
                                            <asp:Button Style="z-index: 0" ID="btnDownloadGyro" runat="server" Text="Download Status Gyro"></asp:Button>
                                            <asp:Button ID="btnBlock" runat="server" Text="Ceiling Block"></asp:Button></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
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
