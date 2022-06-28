<%@ Page Language="vb" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeBehind="FrmDataSiswaBerbayar.aspx.vb" Inherits="FrmDataSiswaBerbayar" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Data Status Siswa</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/jquery-1.8.3.min.js"></script>

    <script type="text/javascript">
        window.onload = function () {
            var dtgrid = document.getElementById('<%=dtgBooking.ClientID%>');
            var div = document.getElementById('<%= div2.ClientID%>');
            if (dtgrid.clientHeight < div.clientHeight) {
                div.style.height = dtgrid.clientHeight
            }

        };

        function popUpClassInformation(kode) {
            var url = '../PopUp/PopUpClassInformation.aspx?kode=' + kode;
            showPopUp(url, '', 320, 440, null);
        }


    </script>
    <style type="text/css">
        .hidden {
            display: none;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="lblPageTitle" runat="server"></asp:Label>
                    <asp:HiddenField ID="hdnCategory" runat="server" />
                </td>
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
                    <asp:HiddenField ID="hdnCheck" runat="server" />
                    <table id="tbl7" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td width="60%">
                                <table id="table5">
                                    <tr id="trReqId" runat="server" visible="false">
                                        <td class="titleField" width="150">No. Tagihan</td>
                                        <td style="height: 22px" width="20px">:</td>
                                        <td style="height: 22px" nowrap="nowrap">
                                            <asp:TextBox ID="txtReqID" runat="server" Width="125px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trTxtTahunFiscal" runat="server">
                                        <td width="150" class="titleField">Tahun Fiskal</td>
                                        <td style="height: 10px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:TextBox ID="txtTahunFiskal" runat="server" Width="125px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trddlTahunFiscal" runat="server">
                                        <td width="150" class="titleField">Tahun Fiskal</td>
                                        <td style="height: 10px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:DropDownList ID="ddlTahunFiscal" AutoPostBack="true" OnSelectedIndexChanged="ddlTahunFiscal_SelectedIndexChanged" runat="server" Width="125px"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="trDealer" runat="server">
                                        <td class="titleField" width="200px" height="22">Kode Dealer</td>
                                        <td style="height: 22px" width="20px">:</td>
                                        <td style="height: 22px" nowrap="nowrap">
                                            <asp:TextBox ID="txtDealerCode" runat="server" Width="264px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trPaymenttype" runat="server" visible="false">
                                        <td class="titleField" width="200px" height="22">Tipe Pembayaran</td>
                                        <td style="height: 22px" width="20px">:</td>
                                        <td style="height: 22px" nowrap="nowrap">
                                            <asp:TextBox ID="txtPaymenttype" runat="server" Width="264px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trStatus" runat="server" visible="false">
                                        <td class="titleField" width="200px" height="22">Status</td>
                                        <td style="height: 22px" width="20px">:</td>
                                        <td style="height: 22px" nowrap="nowrap">
                                            <asp:TextBox ID="txtStatus" runat="server" Width="264px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trUpload" runat="server" visible="false">
                                        <td class="titleField" width="200px" height="22">Faktur Pajak</td>
                                        <td style="height: 22px" width="20px">:</td>
                                        <td style="height: 22px" nowrap="nowrap" id="tdUpload" runat="server">
                                            <input onkeypress="return false;" id="photoSrc" tabindex="19" type="file" size="29" name="File1"
                                                runat="server">
                                            &nbsp;
                                            <asp:Button ID="btnUpload" runat="server" Text="Upload" CausesValidation="false"></asp:Button>
                                            <asp:HiddenField ID="hdnFilePath" runat="server" />
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr id="trDownload" runat="server" visible="false">
                                        <td class="titleField" width="200px" height="22">Faktur Pajak</td>
                                        <td style="height: 22px" width="20px">:</td>
                                        <td style="height: 22px" nowrap="nowrap">
                                            <asp:LinkButton ID="lbtnDownload" runat="server" Text="Download" CausesValidation="false"></asp:LinkButton>
                                            &nbsp;
                                              <asp:LinkButton ID="lbnDelete" runat="server" Text="Hapus" CausesValidation="false"></asp:LinkButton>

                                        </td>
                                    </tr>
                                    <tr id="trUploadDN" runat="server" visible="false">
                                        <td class="titleField" width="200px" height="22">Debit Note</td>
                                        <td style="height: 22px" width="20px">:</td>
                                        <td style="height: 22px" nowrap="nowrap" id="td1" runat="server">
                                            <input onkeypress="return false;" id="FileDN" tabindex="19" type="file" size="29" name="FileDN"
                                                runat="server">
                                            &nbsp;
                                            <asp:Button ID="btnUploadDN" runat="server" Text="Upload" CausesValidation="false"></asp:Button>
                                            <asp:HiddenField ID="hdnFilePath2" runat="server" />
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr id="trDownloadDN" runat="server" visible="false">
                                        <td class="titleField" width="200px" height="22">Debit Note</td>
                                        <td style="height: 22px" width="20px">:</td>
                                        <td style="height: 22px" nowrap="nowrap">
                                            <asp:LinkButton ID="lbtnDonwloadDN" runat="server" Text="Download" CausesValidation="false"></asp:LinkButton>
                                            &nbsp;
                                              <asp:LinkButton ID="lbtnDeleteDN" runat="server" Text="Hapus" CausesValidation="false"></asp:LinkButton>

                                        </td>
                                    </tr>
                                    <tr id="tr1" runat="server">
                                        <td class="titleField" height="22">Free Pass Training</td>
                                        <td style="height: 22px" width="20px">:</td>
                                        <td style="height: 22px" nowrap="nowrap">
                                            <asp:TextBox ID="txJumlahVoucher" Style="text-align: right" Text="0" ReadOnly="true" Width="50px" runat="server"></asp:TextBox>
                                            &nbsp;<asp:Label runat="server" ID="lblhari1" Text="kelas"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="tr2" runat="server">
                                        <td class="titleField" width="200px" height="22">Sisa Free Pass Training</td>
                                        <td style="height: 22px" width="20px">:</td>
                                        <td style="height: 22px" nowrap="nowrap">
                                            <asp:TextBox ID="txSisaVoucher" Style="text-align: right" Text="0" ReadOnly="true" Width="50px" runat="server"></asp:TextBox>
                                            &nbsp;<asp:Label runat="server" ID="Label1" Text="kelas"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField"></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label class="titleField" ID="lblNotifSaldo" runat="server" Text="Saldo Deposit B : Rp. "></asp:Label>&nbsp;&nbsp;
                    <asp:Label class="titleField" ID="lblNotifPlafon" runat="server" Text="Plafon Deposit B : Rp. "></asp:Label>&nbsp;&nbsp;
                    <asp:Label class="titleField" ID="lblNominalPencairan" runat="server" Text="Transaksi Nominal Pencairan : Rp. "></asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <div id="div2" runat="server" style="overflow: auto; height: 320px">
                        <asp:DataGrid ID="dtgBooking" runat="server" Width="100%" AutoGenerateColumns="False"
                            GridLines="Horizontal" CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD"
                            AllowCustomPaging="True" AllowSorting="True" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="" SortExpression="">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAllItems" runat="server" AutoPostBack="true" OnCheckedChanged="chkAllItems_CheckedChanged"></asp:CheckBox></HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkItem" AutoPostBack="true" OnCheckedChanged="chkItem_CheckedChanged" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No" SortExpression="">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdnIDBooking" runat="server" />
                                        <asp:HiddenField ID="hdnID" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="No. Reg" SortExpression="ID">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoReg" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Nama Siswa" SortExpression="Name">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNamaSiswa" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Posisi">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPosisi" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Kategori">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKategori" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode Kelas">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hKodeKelas" runat="server">
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Free Pass Training">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkItemChecked" OnCheckedChanged="chkItemChecked_CheckedChanged" AutoPostBack="true" CausesValidation="false" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jumlah Kelas Invest" SortExpression="">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaidDay" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Invest Per Kelas" SortExpression="">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPricePerDay" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Total Invest" SortExpression="">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalPrice" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>

                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>

            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    <table>
                        <tr align="right">
                            <td class="titleField" height="22">Total</td>
                            <td style="height: 22px" width="20px">: Rp</td>
                            <td style="height: 22px" nowrap="nowrap">
                                <asp:TextBox ID="txtTotalPrice" Style="text-align: right" runat="server"></asp:TextBox>

                            </td>
                        </tr>
                        <tr align="right">
                            <td class="titleField" height="22">Free Pass</td>
                            <td style="height: 22px" width="20px">: Rp</td>
                            <td style="height: 22px" nowrap="nowrap">
                                <asp:TextBox ID="txtVoucher" Text="0" Style="text-align: right" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="right">
                            <td class="titleField" height="22">PPN</td>
                            <td style="height: 22px" width="20px">: Rp</td>
                            <td style="height: 22px" nowrap="nowrap">
                                <asp:TextBox ID="txtppn" Text="0" Style="text-align: right" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="right">
                            <td class="titleField" height="22">Total yang harus dibayar</td>
                            <td style="height: 22px" width="20px">: Rp</td>
                            <td style="height: 22px" nowrap="nowrap">
                                <asp:TextBox ID="txtTotal" Text="0" Style="text-align: right" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblDealer" runat="server" CssClass="titleField">Dealer Pembayar</asp:Label>&nbsp;&nbsp;:&nbsp;
                    <asp:DropDownList Width="100px" ID="ddlDealerGroup" runat="server"></asp:DropDownList>&nbsp;&nbsp;
                    <asp:Label ID="Label2" runat="server" CssClass="titleField">Upload Dokumen Persetujuan</asp:Label>&nbsp;&nbsp;:&nbsp;
                    <input onkeypress="return false;" id="FileSuratKuasa" tabindex="19" type="file" size="29" name="FileSuratKuasa"
                        runat="server">&nbsp; <span id="extDoc" runat="server" style="color:red">pdf, png, jpg, jpeg</span>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr id="trInfo" runat="server">
                <td align="center">
                    <asp:CheckBox ID="cbxInfo" runat="server" OnCheckedChanged="cbxInfo_CheckedChanged" AutoPostBack="true" />
                    <%--<asp:Label ID="lblinfo" Text="Test" runat="server"><span class="titleField">Dengan ini dealer menyatakan setuju untuk melakukan pembayaran training sejumlah Rp. {0} dengan Deposit B atau melalui transfer jika saldo tidak mencukupi. Terima kasih</span> </asp:Label>--%>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr id="trAction2" runat="server">
                <td align="center">
                    <asp:Button ID="btnApprove" runat="server" Text="Approve" CausesValidation="False" TabIndex="14"></asp:Button>
                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan" CausesValidation="False" Visible="false" TabIndex="14"></asp:Button>&nbsp;
                    <asp:Button ID="btnSubmit" runat="server" Text="DiSetujui" CausesValidation="False" Visible="false" TabIndex="14"></asp:Button>&nbsp;
                    <asp:Button ID="btnKembali" runat="server" Text="Kembali" CausesValidation="False" TabIndex="14"></asp:Button>
                </td>
            </tr>
        </table>
    </form>

</body>
</html>

