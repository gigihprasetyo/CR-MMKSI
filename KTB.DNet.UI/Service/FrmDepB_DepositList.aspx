<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDepB_DepositList.aspx.vb"
    Inherits=".FrmDepB_DepositList" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Daftar Deposit B</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">

    <style>
        .hide {
            DISPLAY: none;
        }
    </style>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>

    <script language="javascript">
        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer;
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = tempParam;
        }
        function getNextSibling(startBrother) {
            endBrother = startBrother.nextSibling;
            while (endBrother.nodeType != 1) {
                endBrother = endBrother.nextSibling;
            }
            return endBrother;
        }
        var isshown = false;
        function toggleDetail(elm) {
            if (elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rows[elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex + 1].style.display == "none") {
                isshown = false;
            }
            if (elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rows[elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex + 1].style.display == "") {
                isshown = true;
            }
            if (!isshown) {
                elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rows[elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex + 1].style.display = "block";
                elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rows[elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex + 1].style.display = "";
                isshown = true;
            }
            else {
                elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rows[elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex + 1].style.display = "none";
                elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rows[elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex + 1].style.display = "";
                elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rows[elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex + 1].style.display = "none";
                isshown = false;
            }
            if (elm.childNodes[2].tagName == 'IMG') {
                if (elm.childNodes[2].style.display == 'none') {
                    elm.childNodes[2].style.display = 'block';
                }
                else {
                    elm.childNodes[2].style.display = 'none';
                }
            }
            else {
                if (elm.childNodes[3].style.display == 'none') {
                    elm.childNodes[3].style.display = 'block';
                }
                else {
                    elm.childNodes[3].style.display = 'none';
                }
            }
            if (elm.childNodes[0].tagName == 'IMG') {
                if (elm.childNodes[0].style.display == 'none') {
                    elm.childNodes[0].style.display = 'block';
                }
                else {
                    elm.childNodes[0].style.display = 'none';
                }
            }
            else {
                if (elm.childNodes[1].style.display == 'none') {
                    elm.childNodes[1].style.display = 'block';
                }
                else {
                    elm.childNodes[1].style.display = 'none';
                }
            }

        }
        function toggleDepositDetail(elm) {
            var tr = elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode;
            var rows = tr.parentNode.rows;

            if (elm.childNodes[1].style.display == 'none') {
                elm.childNodes[1].style.display = 'block';
            }
            else {
                elm.childNodes[1].style.display = 'none';
            }

            if (elm.childNodes[0].style.display == 'none') {
                elm.childNodes[0].style.display = 'block';
            }
            else {
                elm.childNodes[0].style.display = 'none';
            }
            var suffix = (getNextSibling(elm.parentNode).innerHTML); // innerHTML ,handle mozilla req
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].id == "tr" + suffix) {
                    if (rows[i].style.display == "none") {
                        rows[i].style.display = ""; // handle mozilla req
                    }
                    else {
                        rows[i].style.display = "none";
                    }
                }
            }
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table style="id: 'Table2'" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Service - Daftar Deposit B</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0" alt=""></td>
            </tr>
            <asp:Panel ID="pnlSearch" runat="server">
                <tr>
                    <td>
                        <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                            <tr>
                                <td class="titleField" style="width: 146px">Kode Dealer</td>
                                <td style="width: 2px">:</td>
                                <td class="titleField">
                                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                        ID="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" runat="server"
                                        Text=""></asp:TextBox>
                                    <asp:Label ID="lblSearchDealer" runat="server"><img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="width: 146px">Periode</td>
                                <td style="width: 2px">:</td>
                                <td class="titleField">
                                    <table cellspacing="0">
                                        <tr>
                                            <td><cc1:IntiCalendar ID="icPeriodeFrom" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                            <td>s/d</td>
                                            <td><cc1:IntiCalendar ID="icPeriodeTo" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="width: 146px">Produk</td>
                                <td style="width: 2px">:</td>
                                <td class="titleField">
                                    <asp:DropDownList Style="z-index: 0" ID="ddlProductCategory" runat="server" Width="140px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="width: 146px"></td>
                                <td style="width: 2px"></td>
                                <td class="titleField">
                                    <asp:Button ID="btnSearch" runat="server" Text="Cari" Width="72px"></asp:Button>
                                    <asp:Button ID="BtnDownload" runat="server" Text="Download" Width="72px"></asp:Button>
                                    <asp:Button ID="BtnDownloadHeader" runat="server" Text="Download Header" Width="100px"></asp:Button>
                                    <asp:Button ID="BtnDownloadAllDetail" runat="server" Text="Download Detail" Width="100px"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="tblLegend">
                            <tr>
                                <td style="width:36%"></td>
                                <td style="width:30%"></td>
                                <td>
                                    <ul>
                                          <li>Kolom Total Kredit Menambahkan Saldo Deposit</li>
                                          <li>Kolom Total Debet Mengurangi Saldo Deposit</li>
                                          <li>Kolom Saldo Akhir Minus (-) = Saldo Deposit Tidak Mencukupi</li>
                                    </ul>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DataList ID="dtlDepositA" runat="server" Width="100%" BorderWidth="0px"
                            BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="1" CellSpacing="1" ShowFooter="True">
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                            <HeaderTemplate>
                                No
                                <td class="titleTableService" style="width: 100px">
                                    <asp:HyperLink ID="hypKodeDealer" runat="server" ForeColor="white">Kode Dealer</asp:HyperLink>
                                </td>
                                <td class="titleTableService" style="width: 80px">Produk
                                </td>
                                <td class="titleTableService">Nama Dealer
                                </td>
                                <td class="titleTableService" style="width: 100px">Saldo Awal
                                </td>
                                <td class="titleTableService" style="width: 100px">Total Debet
                                </td>
                                <td class="titleTableService" style="width: 100px">Total Kredit
                                </td>
                                <td class="titleTableService" style="width: 100px">Saldo Akhir					
                                </td>
                                <td class="titleTableService" style="width: 40px"></td>

                            </HeaderTemplate>
                            <ItemTemplate>
                                <table cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" onclick="javascript:toggleDetail(this)">
				                                <img src="../images/plus.gif">
				                                <img style="display:none" src="../images/minus.gif">
                                            </asp:Label>
                                        </td>
                                        <td><%# DataBinder.Eval(Container, "ItemIndex") + 1 %></td>
                                    </tr>
                                </table>
                                <td
                                    style="color: Black; background-color: #F1F6FB" align="center">
                                    <asp:Label ID="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                    </asp:Label>
                                </td>
                                <td style="color: Black; background-color: #F1F6FB" align="center">
                                    <asp:Label ID="lblProductCategoryHeader" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProductCategory.Code") %>'>
                                    </asp:Label>
                                </td>
                                <td style="color: Black; background-color: #F1F6FB" align="left">
                                    <asp:Label ID="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>></asp:Label>
                                </td>
                                <td style="color: Black; background-color: #F1F6FB" align="right">
                                    <asp:Label ID="lblSaldoAwal" runat="server"></asp:Label>
                                </td>
                                <td style="color: Black; background-color: #F1F6FB" align="right">
                                    <asp:Label ID="lblDebet" runat="server"></asp:Label>
                                </td>
                                <td style="color: Black; background-color: #F1F6FB" align="right">
                                    <asp:Label ID="lblKredit" runat="server"></asp:Label>
                                </td>
                                <td style="color: Black; background-color: #F1F6FB" align="right">
                                    <asp:Label ID="lblSaldoAkhir" runat="server"></asp:Label>
                                </td>
                                <td style="color: Black; background-color: #F1F6FB" align="center">
                                    <asp:LinkButton ID="Linkbutton1" CausesValidation="False" CommandName="detail"
                                        CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>' Text="Lihat"
                                        runat="server"><img src="../images/detail.gif" border="0" alt="Lihat">
                                    </asp:LinkButton>
                                </td>
                                <tr style="display: none">
                                    <td />
                                    <td colspan="7">
                                        <table style="width: 100%" cellspacing="0" cellpadding ="0">
                                            <tr>
                                                <td style="width: 100%">
                                                    <asp:DataGrid runat="server" Width="100%" AutoGenerateColumns="False"
                                                        ID="dtgDetail" CellSpacing="1" CellPadding="3" BorderColor="Gainsboro" BackColor="#CDCDCD"
                                                        BorderWidth="0px">
                                                        <AlternatingItemStyle ForeColor="Black"
                                                            BackColor="White"></AlternatingItemStyle>
                                                        <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                                                        <HeaderStyle ForeColor="black" BackColor="#edd0b5"
                                                            CssClass="titleTableService2" HorizontalAlign="Center"></HeaderStyle>
                                                        <Columns>
                                                            <asp:BoundColumn ItemStyle-HorizontalAlign="Left"
                                                                DataField="Periode" HeaderText="Periode"></asp:BoundColumn>
                                                            <asp:BoundColumn ItemStyle-HorizontalAlign="Right" ItemStyle-Width="97px"
                                                                DataField="SaldoAwal" HeaderText="Saldo Awal"
                                                                DataFormatString="{0:#,###}"></asp:BoundColumn>
                                                            <asp:BoundColumn ItemStyle-HorizontalAlign="Right" ItemStyle-Width="95px"
                                                                DataField="Debet" HeaderText="Debet" DataFormatString="{0:#,###}"></asp:BoundColumn>
                                                            <asp:BoundColumn ItemStyle-HorizontalAlign="Right" ItemStyle-Width="97px"
                                                                DataField="Kredit" HeaderText="Kredit" DataFormatString="{0:#,###}"></asp:BoundColumn>
                                                            <asp:BoundColumn ItemStyle-HorizontalAlign="Right" ItemStyle-Width="93px"
                                                                DataField="SaldoAkhir" HeaderText="Saldo Akhir" DataFormatString="{0:#,###}"></asp:BoundColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <table cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" onclick="toggleDetail(this)">
                                                <img src="../images/plus.gif">
												<img style="display:none" src="../images/minus.gif">
                                            </asp:Label>
                                        </td>
                                        <td><%# DataBinder.Eval(Container, "ItemIndex") + 1 %></td>
                                    </tr>
                                </table>
                                <td
                                    style="color: Black; background-color: white" align="center">
                                    <asp:Label ID="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                    </asp:Label>
                                </td>
                                <td style="color: Black; background-color: white" align="center">
                                    <asp:Label ID="lblProductCategoryHeader" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProductCategory.Code") %>'>
                                    </asp:Label>
                                </td>
                                <td style="color: Black; background-color: white" align="left">
                                    <asp:Label ID="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>></asp:Label>
                                </td>
                                <td style="color: Black; background-color: white" align="right">
                                    <asp:Label ID="lblSaldoAwal" runat="server"></asp:Label>
                                </td>
                                <td style="color: Black; background-color: white" align="right">
                                    <asp:Label ID="lblDebet" runat="server"></asp:Label>
                                </td>
                                <td style="color: Black; background-color: white" align="right">
                                    <asp:Label ID="lblKredit" runat="server"></asp:Label>
                                </td>
                                <td style="color: black; background-color: white;" align="right">
                                    <asp:Label ID="lblSaldoAkhir" runat="server"></asp:Label>
                                </td>
                                <td style="color: black; background-color: white;" align="center">
                                    <asp:LinkButton ID="Linkbutton2" CausesValidation="False" CommandName="detail"
                                        CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>' Text="Lihat"
                                        runat="server"><img src="../images/detail.gif" border="0" alt="Lihat">
                                    </asp:LinkButton>
                                </td>
                                <tr style="display: none">
                                    <td></td>
                                    <td colspan="7">
                                        <table style="width: 100%" cellspacing="0" cellpadding ="0">
                                            <tr>
                                                <td style="width: 100%">
                                                    <asp:DataGrid runat="server" Width="100%" AutoGenerateColumns="False"
                                                        ID="dtgDetail" CellSpacing="1" CellPadding="3" BorderColor="Gainsboro" BackColor="#CDCDCD"
                                                        BorderWidth="0px">
                                                        <AlternatingItemStyle ForeColor="Black"
                                                            BackColor="White"></AlternatingItemStyle>
                                                        <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                                                        <HeaderStyle ForeColor="black" BackColor="#edd0b5"
                                                            CssClass="titleTableService2" HorizontalAlign="Center"></HeaderStyle>
                                                        <Columns>
                                                            <asp:BoundColumn ItemStyle-HorizontalAlign="Left"
                                                                DataField="Periode" HeaderText="Periode"></asp:BoundColumn>
                                                            <asp:BoundColumn ItemStyle-HorizontalAlign="Right" ItemStyle-Width="97px"
                                                                DataField="SaldoAwal" HeaderText="Saldo Awal"
                                                                DataFormatString="{0:#,###}"></asp:BoundColumn>
                                                            <asp:BoundColumn ItemStyle-HorizontalAlign="Right" ItemStyle-Width="95px"
                                                                DataField="Debet" HeaderText="Debet" DataFormatString="{0:#,###}"></asp:BoundColumn>
                                                            <asp:BoundColumn ItemStyle-HorizontalAlign="Right" ItemStyle-Width="97px"
                                                                DataField="Kredit" HeaderText="Kredit" DataFormatString="{0:#,###}"></asp:BoundColumn>
                                                            <asp:BoundColumn ItemStyle-HorizontalAlign="Right" ItemStyle-Width="93px"
                                                                DataField="SaldoAkhir" HeaderText="Saldo Akhir" DataFormatString="{0:#,###}"></asp:BoundColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                                <td
                                    align="center">
                                    <strong>Total:</strong>
                                </td>
                                <td align="right"></td>
                                <td align="right"></td>
                                <td align="right">
                                    <asp:Label ID="lblTotalSaldoAwal" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblTotalDebet" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblTotalKredit" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblTotalSaldoAkhir" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td align="center">
                            </FooterTemplate>
                        </asp:DataList>
                    </td>
                </tr>
            </asp:Panel>
            <asp:Panel ID="pnlDetails" runat="server" Visible="False">
                <tr>
                    <td>
                        <br>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellspacing="1" cellpadding="3" width="100%" border="0">
                            <tr class="titleField">
                                <td width="20%" height="20">Periode </td>
                                <td width="30%">:&nbsp; <asp:Label ID="lblPeriode" runat="server"></asp:Label></td>
                                <td>Dealer</td>
                                <td>:&nbsp;<asp:Label ID="lblDealerDetail" runat="server"></asp:Label>
                                </td>
                                <tdstyle="width: 20%;"></td>
                            </tr>
                            <tr class="titleField">
                                <td height="20">Total Debet </td>
                                <td width="30%">:&nbsp;<asp:Label ID="lblTotalDebetAll" runat="server"></asp:Label></td>
                                <td>Produk</td>
                                <td>:&nbsp;<asp:Label ID="lblProdukDetail" runat="server"></asp:Label></td>
                                <td style="width: 20%;"></td>
                            </tr>
                            <tr class="titleField">
                                <td height="20">Total Kredit </td>
                                <td width="30%">:&nbsp;<asp:Label ID="lblTotalKreditAll" runat="server"></asp:Label></td>
                                <td colspan="3"></td>
                            </tr>
                        </table>
                        <table cellspacing="1" cellpadding="3" width="100%" border="0">
                            <tr>
                                <td>
                                    <table cellspacing="1" cellpadding="3" width="100%" border="0">
                                        <asp:Repeater ID="dtlDetails" runat="server">
                                            <HeaderTemplate>
                                                <tr>
                                                    <td class="titleTableService" width="13%">Tanggal Transaksi</td>
                                                    <td class="titleTableService" width="15%">Keterangan</td>
                                                    <td class="titleTableService" width="10%">No Dokumen</td>
                                                    <td class="titleTableService" width="15%">Reference</td>
                                                    <td class="titleTableService" width="25%">Text</td>
                                                    <td class="titleTableService" width="11%">Debet (Rp)</td>
                                                    <td class="titleTableService" width="11%">Kredit (Rp)</td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <td style="color: Black; background-color: #F1F6FB; align-content:center" align="center">
                                                    <asp:Label ID="lblTransactionDate" runat="server"></asp:Label>
                                                </td>
                                                <td style="color: Black; background-color: #F1F6FB">
                                                    <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Tipe") %>'></asp:Label>
                                                </td>
                                                <td align="center" style="color: Black; background-color: #F1F6FB">
                                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DocumentNumber") %>'></asp:Label>
                                                </td>
                                                <td align="center" style="color: Black; background-color: #F1F6FB">
                                                    <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Reff") %>'></asp:Label>
                                                </td>
                                                <td style="color: Black; background-color: #F1F6FB">
                                                    <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:Label>
                                                </td>
                                                <td align="right" style="color: Black; background-color: #F1F6FB">
                                                    <asp:Label ID="lblDebet" runat="server"></asp:Label>
                                                </td>
                                                <td align="right" style="color: Black; background-color: #F1F6FB">
                                                    <asp:Label ID="lblKredit" runat="server"></asp:Label>
                                                </td>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <td align="center">
                                                    <asp:Label ID="lblTransactionDate" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Tipe") %>'></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DocumentNumber") %>'></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Reff") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:Label>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="lblDebet" runat="server"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="lblKredit" runat="server"></asp:Label>
                                                </td>
                                            </AlternatingItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnBack" runat="server" Text="Kembali"></asp:Button>
                                    <asp:Button ID="BtnDownloadDtl" runat="server" Text="Download"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </asp:Panel>
        </table>
    </form>
</body>
</html>
