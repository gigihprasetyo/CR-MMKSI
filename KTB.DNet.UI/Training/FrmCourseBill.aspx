<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmCourseBill.aspx.vb" Inherits="FrmCourseBill" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmTrTrainee</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/jscript">

        function popUpClassInformation(kode) {
            var url = '../PopUp/PopUpClassInformation.aspx?kode=' + kode;
            showPopUp(url, '', 320, 440, null);
        }

        function ShowPPDealerSelection() {
            //alert('bisa');
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 600, 600, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerCodeSelection = document.getElementById("txtKodeDealer");
            txtDealerCodeSelection.value = selectedDealer;
            if (navigator.appName == "Microsoft Internet Explorer") {
                txtDealerCodeSelection.focus();
                txtDealerCodeSelection.blur();
            }

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

        function InputPasswordPlease() {
            showPPPassword();
        }

        function showPPPassword() {
            showPopUp('../General/../PopUp/PopupInputPassword.aspx', '', 200, 600, GotPassword);
        }

        function GotPassword(result) {
            var txtUser = document.getElementById("txtUser");
            var txtPwd = document.getElementById("txtPass");
            var btn = document.getElementById("btnSend");
            var str = result;
            var username = '', pwd = '';

            username = str.split(';')[0];
            pwd = str.split(';')[1];

            txtUser.value = username;
            txtPwd.value = pwd;
            btn.click();
        }

    </script>
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
                                    <tr id="trDealer" runat="server">
                                        <td class="titleField" style="height: 23px">Kode Dealer</td>
                                        <td style="height: 22px">:</td>
                                        <td style="height: 22px" nowrap="nowrap">
                                            <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtDealerCode" runat="server" Width="300px"></asp:TextBox>&nbsp;
                                        </td>
                                    </tr>
                                    <tr id="trFyearMKS" runat="server">
                                        <td width="150" class="titleField">Tahun Fiskal</td>
                                        <td style="height: 10px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:DropDownList ID="ddlTahunFiscal" Width="120px" AutoPostBack="true" runat="server" TabIndex="9"></asp:DropDownList><asp:Label ID="Label1" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr id="trFyearDealer" runat="server">
                                        <td width="150" class="titleField">Tahun Fiskal</td>
                                        <td style="height: 10px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:DropDownList ID="ddlFiscalYear" OnSelectedIndexChanged="ddlFiscalYear_SelectedIndexChanged" Width="120px" AutoPostBack="true" runat="server" TabIndex="9"></asp:DropDownList><asp:Label ID="Label2" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr id="trMks" runat="server">
                                        <td class="titleField" style="height: 23px">Kode Dealer</td>
                                        <td style="height: 22px">:</td>
                                        <td style="height: 22px" nowrap="nowrap">
                                            <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtKodeDealer" runat="server" Width="300px"></asp:TextBox>&nbsp;
										<asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField">Status</td>
                                        <td style="height: 22px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:DropDownList ID="ddlStatus" Width="120px" AutoPostBack="true" runat="server" TabIndex="9"></asp:DropDownList><asp:Label ID="Label3" runat="server"></asp:Label></td>
                                    </tr>

                                    <tr id="trCari" runat="server">
                                        <td class="titleField" style="height: 23px"></td>
                                        <td style="height: 22px"></td>
                                        <td style="height: 22px" nowrap="nowrap">
                                            <asp:Button ID="btnCari" runat="server" Width="60px" Text="Cari" CausesValidation="False" TabIndex="14"></asp:Button>
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
                <td>
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
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="background-color: LightSalmon; width: 50px">&nbsp;&nbsp;</td>
                            <td>Sudah jatuh tempo</td>
                            <td style="width: 30px"></td>
                        </tr>

                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="div2" runat="server" style="overflow: auto; height: 320px">
                        <asp:DataGrid ID="dtgBilling" runat="server" Width="100%" AutoGenerateColumns="False"
                            GridLines="Horizontal" CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD"
                            AllowCustomPaging="True" AllowPaging="true" AllowSorting="True" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <HeaderTemplate>
                                        <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkItemChecked" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No" SortExpression="">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdnID" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="No. Tagihan" SortExpression="ID">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoReg" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Kode Dealer">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodeDealer" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Dealer Pembayar">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerPembayar" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Posted date">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPosteddate" runat="server"></asp:Label>
                                        <cc1:IntiCalendar ID="ICPostedDate" runat="server" TextBoxWidth="70" EnableViewState="True" CanPostBack="False" Thursday="True" Tuesday="True" Wednesday="True" Friday="True" Monday="True" Saturday="True"></cc1:IntiCalendar>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jatuh Tempo">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDueDate" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tahun Fiskal" SortExpression="FiscalYear">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTahunFiskal" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tipe Pembayaran">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltipePembayaran" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jumlah Siswa" SortExpression="">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblJumlahSiswa" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jumlah Pembayaran" SortExpression="">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblJumlahBayar" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Free Pass Training" SortExpression="">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalVoucher" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="PPN" SortExpression="">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPPN" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Total Pembayaran" SortExpression="">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalPrice" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Maksimal Deposit B" SortExpression="">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaxDepositB" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DN Number">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDNNumber" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="JV Number">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblJVNumber" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Surat Kuasa">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDownloadSuratKuasa" runat="server" CausesValidation="False" CommandName="downloadsuratkuasa" Text="Download">
										    <img src="../images/icon_mail.gif" border="0" alt="Download"/>
                                        </asp:LinkButton>
                                        <asp:HiddenField ID="hdnsuratkuasa" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Debit Note">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDownloadDN" runat="server" CausesValidation="False" CommandName="downloadDN" Text="Download">
										    <img src="../images/icon_mail.gif" border="0" alt="Download"/>
                                        </asp:LinkButton>
                                        <asp:HiddenField ID="hdnSourceFileDN" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Faktur Pajak">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDownloadFaktur" runat="server" CausesValidation="False" CommandName="downloadfaktur" Text="Download">
										    <img src="../images/icon_mail.gif" border="0" alt="Download"/>
                                        </asp:LinkButton>
                                        <asp:HiddenField ID="hdnSourceFile" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Bukti Transfer">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDownloadBuktiTF" runat="server" CausesValidation="False" CommandName="downloadbuktitf" Text="Download">
										    <img src="../images/icon_mail.gif" border="0" alt="Download"/>
                                        </asp:LinkButton>
                                        <asp:HiddenField ID="hdnbuktitransfer" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDetail" runat="server" CausesValidation="False" CommandName="detail" Text="Detail">
										    <img src="../images/unduh.png" border="0" alt="Detail"/>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnHapus" runat="server" Width="8px" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                        <asp:Label ID="lblHistoryStatus" runat="server">
												<img src="../images/popup.gif" style="cursor:hand" alt="Perubahan Status"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="background-color: gainsboro; width: 100%">
                        <tr>
                            <td class="titleField">Validasi</td>
                            <td style="height: 10px">=></td>
                            <td class="titleField">Approval siswa berbayar telah di setujui, Tagihan terbentuk</td>
                            <td style="height: 10px"></td>
                            <td class="titleField">Proses Pencairan Deposit B</td>
                            <td style="height: 10px">=></td>
                            <td class="titleField">Pembayaran akan dilakukan melalui Deposit B</td>
                        </tr>
                        <tr>
                            <td class="titleField">Konfirmasi</td>
                            <td style="height: 10px">=></td>
                            <td class="titleField">Tagihan telah di kirim ke SAP</td>
                            <td style="height: 10px"></td>
                            <td class="titleField">Proses Transfer</td>
                            <td style="height: 10px">=></td>
                            <td class="titleField">Menunggu proses pelunasan via Transfer</td>
                        </tr>
                        <tr>
                            <td class="titleField">Disetujui </td>
                            <td style="height: 10px">=></td>
                            <td class="titleField">Tagihan telah disetujui MKS dan telah dilengkapi Faktur pajak dan Debit note</td>
                            <td style="height: 10px"></td>
                            <td class="titleField">Selesai</td>
                            <td style="height: 10px">=></td>
                            <td class="titleField">Tagihan Sudah Lunas</td>
                        </tr>
                        <tr>
                            <td class="titleField">Pembayaran Transfer</td>
                            <td style="height: 10px">=></td>
                            <td class="titleField">Pembayaran akan dilakukan melaui transfer</td>
                        </tr>

                    </table>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr id="trAction" runat="server">
                <td align="center">
                    <asp:Button ID="btnSend" runat="server" Width="100px" Text="Send to SAP" TabIndex="15"></asp:Button>&nbsp;
                    <asp:Button ID="btnSubmit" runat="server" Width="100px" Text="Disetujui" TabIndex="15"></asp:Button>&nbsp;
                    <asp:Button ID="btnProses" runat="server" Width="100px" Text="Proses" TabIndex="15"></asp:Button>
                </td>
            </tr>
        </table>
    </form>

</body>
</html>
