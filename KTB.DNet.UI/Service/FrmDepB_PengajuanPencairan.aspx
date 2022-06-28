<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDepB_PengajuanPencairan.aspx.vb" Inherits=".FrmDepB_PengajuanPencairan" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Pengajuan Pencarian Deposit B</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPDealerSelection() {
            //showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
            showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx?x=Territory', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer.split(';');
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            var lblDealerName = document.getElementById("lblDealerName");
            txtDealerSelection.value = tempParam[0];
            lblDealerName.innerHTML = tempParam[1] + " - " + tempParam[3];
            var imgDealer = document.getElementById("imgDealer");
            //imgDealer.onclick();
        }




        function ShowPPDealerBankAccountSelection(DealerID) {
            var kodeDealer = document.getElementById("txtKodeDealer");
            if (kodeDealer.value != null && kodeDealer.value.length > 0) {
                showPopUp('../PopUp/PopUpDealerBankAccountSelectionOne.aspx?DealerCode=' + kodeDealer.value, '', 500, 760, DealerBankAccountSelection);
            }
            else {
                alert('Tentukan dealer terlebih dahulu !');
            }
        }

        function DealerBankAccountSelection(selectedAccount) {
            var txtDealerBankAccountSelection = document.getElementById("txtNomorRekening");
            txtDealerBankAccountSelection.value = selectedAccount;
        }

        function numberFormat(nStr, prefix) {
            var prefix = prefix || '';
            nStr += '';
            x = nStr.split('.');
            x1 = x[0];
            x2 = x.length > 1 ? ',' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1))
                x1 = x1.replace(rgx, '$1' + '.' + '$2');
            return prefix + x1 + x2;
        }

        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        }

        function RadioCheckInterest(rb) {
            gv = document.getElementById("<%=dgInterest.ClientID%>");
            var rbs = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }

        function RadioCheckOffset(rb) {
            gv = document.getElementById("<%=dgOffset.ClientID%>");
            var rbs = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
            var row = rb.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var pengajuan = row.cells[3].innerText;
            var lblTotal = document.getElementById("lblTotal");
            lblTotal.innerHTML = pengajuan;
        }

        function RadioCheckProject(rb) {
            gv = document.getElementById("<%=dgProject.ClientID%>");
            var rbs = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
            var row = rb.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var pengajuan = row.cells[3].innerText;
            var lblTotal = document.getElementById("lblTotal");
            lblTotal.innerHTML = pengajuan;
        }

        function RadioCheckKewajiban(rb) {
            var gv = document.getElementById("<%=dgKewajiban.ClientID%>");
            var rbs = gv.getElementsByTagName("input");
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }

            var row = rb.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var ppn = 0;//row.cells[11].innerText;
            var pengajuan = row.cells[10].innerText;
            var lblTotal = document.getElementById("lblTotal");
            //lblTotal.innerHTML = formatNumber(parseFloat(pengajuan) + (parseFloat(pengajuan) * 0.1));
            lblTotal.innerHTML = formatNumber(parseFloat(pengajuan) + (parseFloat(ppn)));
            return false;
        }

        function formatNumber(num) {
            //return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,")
            return num.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,")
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Service - Deposit B - Pengajuan Pencairan</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
        </table>
        <table cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="titleField">Kode Dealer</td>
                <td>:</td>
                <td>
                    <asp:Literal ID="ltrDealerCode" runat="server"></asp:Literal>
                    <asp:TextBox ID="txtKodeDealer" runat="server" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"></asp:TextBox>
                    <asp:Label ID="lblDealerS" runat="server" Width="16px"> 
                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"> </asp:Label>

                    <%--<img id="imgDealerS" src="../images/popup.gif" alt="" style="cursor: hand" onclick="ShowPPDealerSelection();" >--%>
                    <asp:ImageButton Style="display: none" ID="imgDealer" runat="server" ImageUrl="../images/popup.gif"></asp:ImageButton>
                    <asp:Button ID="btnImgDealer" runat="server" Text="" Style="display: none"></asp:Button>
                </td>
                <td class="titleField" style="height: 32px">Tanggal Pengajuan</td>
                <td style="height: 32px">:</td>
                <td style="height: 32px">
                    <asp:Label ID="lblPostingDate" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 32px">Nama Dealer</td>
                <td style="height: 32px">:</td>
                <td style="height: 32px">
                    <asp:Literal ID="ltrDealerName" runat="server"></asp:Literal><asp:Label ID="lblDealerName" runat="server"></asp:Label></td>
                <td class="titleField">No. Ref&nbsp;Surat Pengajuan</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtNomerSuratPengajuan" runat="server" MaxLength="18"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="titleField" style="width: 146px">
                    <asp:Label ID="lblCode" runat="server">Tipe Pengajuan</asp:Label></td>
                <td style="width: 2px">:</td>
                <td class="titleField">
                    <asp:DropDownList ID="ddlTipePengajuan" runat="server" AutoPostBack="True"></asp:DropDownList></td>
                <td class="titleField">
                    <asp:Label ID="lblNomorRekening" runat="server" Text="Nomor Rekening"></asp:Label></td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtNomorRekening" onblur="omitSomeCharacter('txtNomorRekening','<>?*%$')" runat="server"></asp:TextBox>
                    <!--<asp:linkbutton id="lnkAccount" runat="server" Visible="True">
							<img src="../images/popup.gif" border="0" style="cursor:hand" alt="Klik popup">
						</asp:linkbutton>-->
                    <asp:Label ID="lblBankAccount" runat="server">
							<img src="../images/popup.gif" style="CURSOR:hand" border="0" alt="Klik popup">
                    </asp:Label>
                    <asp:TextBox ID="txtDealerID" runat="server" Width="1px" Style="display: none"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:Label ID="lblProduk" runat="server">Kategori Produk</asp:Label></td>
                <td>
                    <asp:Label ID="rbProduk" runat="server">:</asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlProductCategory" runat="server" AutoPostBack="True" Visible="true" Width="104px"></asp:DropDownList></td>
                <td class="titleField" style="width: 146px">
                    <asp:Label ID="lblNoRegTitle" runat="server">No. Reg pengajuan</asp:Label></td>
                <td>
                    <asp:Label ID="rbNoRegTitle" runat="server">:</asp:Label></td>
                <td>
                    <asp:Label ID="lblNoRegValue" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:Label ID="rbPeriode" runat="server">Periode</asp:Label></td>
                <td>
                    <asp:Label ID="lblPeriode" runat="server">:</asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlYear" runat="server" Visible="False"></asp:DropDownList>
                </td>                
            </tr>
            <tr>
                <td class="titleField">
                    <asp:Label ID="rbBulan" runat="server">Bulan</asp:Label></td>
                <td>
                    <asp:Label ID="lblBulan" runat="server">:</asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlPeriode" runat="server" AutoPostBack="True" Visible="False" Width="104px"></asp:DropDownList></td>
            </tr>
        </table>

        <div id="divTransfer">
            <asp:DataGrid ID="dgTransfer" runat="server" Width="100%" BackColor="#CDCDCD" ShowFooter="True"
                AutoGenerateColumns="False" AllowSorting="False" CellSpacing="1" CellPadding="3" BorderColor="Gainsboro"
                BorderWidth="0px">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="No">
                        <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server" Text="1"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Size="Small"></FooterStyle>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Jumlah Pencairan">
                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <FooterStyle HorizontalAlign="Right"></FooterStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblJumlahPencairan" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Amount", "{0:#,###}")%>'>
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtJumlahPencairanEdit" runat="server" onkeypress="return NumberOnly()" Text='<%# DataBinder.Eval(Container.DataItem, "Amount", "{0:#,###}")%>' >
                            </asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtJumlahPencairan" runat="server" Width="100%" Style="text-align: right" onkeypress="return NumberOnly()"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Penjelasan">
                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <FooterStyle HorizontalAlign="Left"></FooterStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPenjelasan" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Desc")%>'>
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPenjelasanEntryEdit" runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "Desc") %>' MaxLength="50"/>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtPenjelasanEntry" runat="server" Width="100%" MaxLength="50"/>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="5%" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="true" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus">
                            </asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lbtnSave" TabIndex="40" runat="server" CausesValidation="True" CommandName="Update"
                                Text="Simpan">
															<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnCancel" TabIndex="50" runat="server" CausesValidation="True" CommandName="Cancel"
                                Text="Batal">
															<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                        </EditItemTemplate>
                        <FooterStyle HorizontalAlign="Center" Width="2%" VerticalAlign="Top"></FooterStyle>
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnAdd" runat="server" CausesValidation="true" CommandName="Add">
															<img src="../images/add.gif" border="0" alt="Tambah">
                            </asp:LinkButton>
                        </FooterTemplate>

                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>

        <div id="divInterest">
            <asp:DataGrid ID="dgInterest" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
                AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" AllowPaging="True"
                AllowCustomPaging="True" BackColor="#CDCDCD" CellPadding="3" GridLines="None" PageSize="10">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn>
                        <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:RadioButton ID="rbInterest" runat="server"  onclick = "RadioCheckInterest(this);"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="ID" Visible="false">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Interest Amount">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblInterestAmount" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.InterestAmount"), "#,##0")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Netto Amount">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNettoAmount" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.NettoAmount"), "#,##0")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tax Amount">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTaxAmount" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.TaxAmount"), "#,##0")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Deskripsi">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>
        
        <div id="divOffset">
            <asp:DataGrid ID="dgOffset" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
                AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" AllowPaging="True"
                AllowCustomPaging="True" BackColor="#CDCDCD" CellPadding="3" GridLines="None" PageSize="10">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn>
                        <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:RadioButton ID="rbOffset" runat="server" onclick = "RadioCheckOffset(this);"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No.">
                        <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateColumn>
                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
                    <asp:BoundColumn DataField="RequestNo" SortExpression="RequestNo" HeaderText="Nomor Pengajuan">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Total Order">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblTotalTagihan"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tipe Pembayaran">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PaymentTypeDesc")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    
                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>

        <div id="divProject">
            <asp:DataGrid ID="dgProject" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
                AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" AllowPaging="True"
                AllowCustomPaging="True" BackColor="#CDCDCD" CellPadding="3" GridLines="None" PageSize="10">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn>
                        <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:RadioButton ID="rbProject" runat="server" onclick = "RadioCheckProject(this);" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="ID" Visible="false">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="DNNumber" SortExpression="DNNumber" HeaderText="DN Number">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Assignment" SortExpression="Assignment" HeaderText="Assignment">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Amount">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Amount"), "#,##0")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Deskripsi">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>

        <div id="divKewajiban">
            <asp:DataGrid ID="dgKewajiban" runat="server" BorderWidth="0px" CellSpacing="1"
                BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" ShowFooter="False"
                AllowPaging="True" AllowCustomPaging="True" PageSize="15" AllowSorting="True">
                <ItemStyle BackColor="White"></ItemStyle>
                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                <Columns>
                    <%--<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>--%>
                    <asp:TemplateColumn Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No.">
                        <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                            <asp:Button ID="btnDepositAInterestHID" runat="server" Visible="false"></asp:Button>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle Width="3%" CssClass="titleTableService" HorizontalAlign="Center"></HeaderStyle>
                        <HeaderTemplate></HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:RadioButton ID="rbKewajiban" runat="server"  onclick = "RadioCheckKewajiban(this);" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                        <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="PeriodYear" HeaderText="Tahun">
                        <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPeriodYear" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PeriodYear")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="TipeKewajiban" HeaderText="Tipe Kewajiban">
                        <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTipeKewajiban" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="NoRegKewajiban" HeaderText="No. Reg">
                        <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNoReg" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoRegKewajiban")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Qty">
                        <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblQty" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Total Harga">
                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTotalHarga" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="PPN" visible="false">
                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPpn" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn >
                        <HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTotalHargaHide" runat="server" style="display:none"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn >
                        <HeaderStyle Width="1%" CssClass="titleTableService" ></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPpnHide" runat="server" style="display:none"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>

        <table>
            <tr valign="top">
                <td class="titleField" width="5%">Total</td>
                <td width="2%">:</td>
                <td align="right" width="20%">
                    <asp:Label ID="lblTotal" name="lblTotal" runat="server" Text="0" Font-Bold="True"></asp:Label></td>
                <td colspan="3"></td>
            </tr>
        </table>
        <br>
        <table>
            <tr>
                <td class="titleField" colspan="3">Catatan:
                </td>
            </tr>
            <tr>
                <td class="titleField" colspan="3">1.
						<asp:Label ID="lblNote1" runat="server">Dokumen ini merupakan bagian tidak terpisahkan dari Akta Perjanjian Penunjukan Dealer</asp:Label></td>
            </tr>
            <tr>
                <td class="titleField" colspan="3">&nbsp;&nbsp;&nbsp;
						<asp:Label ID="lblPersetujuan" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="titleField" colspan="3">2.
						<asp:Label ID="Label2" runat="server">Dokumen ini dibuat dalam bentuk elektronik dan diperlakukan  sebagai alat bukti yang sah</asp:Label></td>
            </tr>
            <tr>
                <td class="titleField" colspan="3">&nbsp;&nbsp;&nbsp;
						<asp:Label ID="lblPersetujuan2" runat="server"></asp:Label></td>
            </tr>
        </table>
        <br>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Simpan"></asp:Button>
                    <asp:Button ID="btnNew" runat="server" Text="Pengajuan Baru"></asp:Button>
                    <asp:Button ID="btnValidasi" runat="server" Text="Validasi"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Kembali" Visible="false"></asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                    <input id="hdnMCPConfirmation" type="hidden" runat="server" name="hdnMCPConfirmation">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
