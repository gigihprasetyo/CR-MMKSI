<%@ Page Language="vb" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeBehind="FrmDaftarPembayaran.aspx.vb" Inherits="FrmDaftarPembayaran" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Daftar Pembayaran</title>
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
            var dtgrid = document.getElementById('<%=dtgPayment.ClientID%>');
            var div = document.getElementById('<%= div2.ClientID%>');
            if (dtgrid.clientHeight < div.clientHeight) {
                div.style.height = dtgrid.clientHeight
            }

        };
        function popUpClassInformation(kode) {
            var url = '../PopUp/PopUpClassInformation.aspx?kode=' + kode;
            showPopUp(url, '', 320, 440, null);
        }

        function ShowPPDealerSelection() {
            //alert('bisa');
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 600, 600, DealerSelection);
        }

        function ShowInformation() {
            //alert('bisa');
            showPopUp('../PopUp/PopUpPaymentInformation.aspx', '', 400, 600, null);
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


    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" enctype="multipart/form-data" method="post" runat="server">
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
                        <tr id="trMks" runat="server">
                            <td width="150" class="titleField" style="height: 23px">Kode Dealer</td>
                            <td style="height: 22px">:</td>
                            <td>
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtKodeDealer" runat="server" Width="250px"></asp:TextBox>&nbsp;
										<asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label></td>

                        </tr>
                        <tr id="tr1" runat="server">
                            <td width="150" class="titleField" style="height: 23px">No Reg Pembayaran</td>
                            <td>:</td>
                            <td style="height: 22px">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txNoReg" runat="server" Width="250px"></asp:TextBox>
                            </td>


                        </tr>
                        <tr>
                            <td width="150" class="titleField" style="height: 23px">Jenis Pembayaran</td>
                            <td style="height: 22px">:</td>
                            <td>
                                <asp:DropDownList ID="ddlTipePembayaran" Width="120px" AutoPostBack="true" runat="server" TabIndex="9"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td width="150" class="titleField" style="height: 23px">Bukti Pembayaran</td>
                            <td style="height: 22px">:</td>
                            <td>
                                <asp:DropDownList ID="ddlbuktiPembayaran" Width="120px" AutoPostBack="true" runat="server" TabIndex="9"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td width="150" class="titleField" style="height: 23px">Status</td>
                            <td style="height: 22px">:</td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" Width="120px" AutoPostBack="true" runat="server" TabIndex="9"></asp:DropDownList></td>
                        </tr>
                        <tr id="trCari" runat="server">
                            <td width="150" class="titleField" style="height: 23px"></td>
                            <td height="22px"></td>
                            <td height="30px">
                                <asp:Button ID="btnCari" runat="server" Width="60px" Text="Cari" CausesValidation="False" TabIndex="14"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td width="150" class="titleField" style="height: 23px"></td>
                            <td height="22px"></td>
                            <td height="30px"></td>
                            <td class="titleField">
                                <asp:HyperLink runat="server" Style="cursor: hand" alt="Klik Popup" ID="hylInfo">Prosedur Pembayaran Transfer</asp:HyperLink></td>
                        </tr>
                    </table>
                </td>
            </tr>
            
            <tr>
                <td>
                    <div id="div2" runat="server" style="overflow: auto; height: 320px">
                        <asp:DataGrid ID="dtgPayment" runat="server" Width="100%" AutoGenerateColumns="False"
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
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdnID" runat="server" />
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
                                <asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="Dealer.DealerCode">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodeDealer" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No Reg Pembayaran" SortExpression="ID">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoReg" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jenis Pembayaran">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltipePembayaran" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jumlah Tagihan" SortExpression="">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblJumlahTagihan" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tgl Dibuat" SortExpression="CreatedTime">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltglDibuat" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Total Amount" SortExpression="TotalAmount">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tgl Actual" SortExpression="ActualPaymentDate">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltglActual" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Amount Actual" Visible="false" SortExpression="ActualPaymentAmount">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblActualAmount" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DN Number">
                                    <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDNNumber" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="TR Number">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txTrNumber" Width="65%" runat="server"></asp:TextBox>
                                        <asp:LinkButton ID="btnSimpantr" runat="server" CausesValidation="False" CommandName="simpantr">
										    <img src="../images/simpan.gif" border="0" alt="Simpan"/>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnEdittr" runat="server" CausesValidation="False" CommandName="edittr">
										    <img src="../images/icon_list-add.png" border="0" alt="Edit"/>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnCanceltr" runat="server" CausesValidation="False" CommandName="canceltr">
										    <img src="../images/batal.gif" border="0" alt="Batal"/>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Bukti Transfer">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDownloadBukti" runat="server" CausesValidation="False" CommandName="downloadbukti" Text="Download">
										    <img src="../images/icon_mail.gif" border="0" alt="Download"/>
                                        </asp:LinkButton>
                                        <asp:HiddenField ID="hdnSourceFile" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDetail" runat="server" CausesValidation="False" CommandName="detail" Text="Detail">
										    <img src="../images/detail.gif" border="0" alt="Detail"/>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="False" CommandName="edit" Text="Edit">
										    <img src="../images/unduh.png" border="0" alt="Edit"/>
                                        </asp:LinkButton>

                                    </ItemTemplate>
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
            <tr id="trAction" runat="server">
                <td align="center">
                    <asp:Button ID="btnDelete" runat="server" Width="75px" Text="Hapus" TabIndex="15"></asp:Button>&nbsp;
                    <asp:Button ID="btnValidasi" runat="server" Width="100px" Text="Validasi" TabIndex="15"></asp:Button>&nbsp;
                    <asp:Button ID="btnProses" runat="server" Width="75px" Text="Proses" TabIndex="15"></asp:Button>&nbsp;
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

