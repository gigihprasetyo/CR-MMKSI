<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDepB_KuitansiPencairanList.aspx.vb" Inherits=".FrmDepB_KuitansiPencairanList" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Daftar Kuitansi Pencairan Deposit B</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
    <script language="javascript">
        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer;
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = tempParam;
        }

        function GetSelectedKuitansi() {
            var table;
            var bcheck = false;
            table = document.getElementById("dgDaftarPengajuanKuitansiDepositB");
            var Kuitansi = '';
            for (i = 1; i < table.rows.length; i++) {
                var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (CheckBox != null && CheckBox.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        if (Kuitansi == '') {
                            Kuitansi = replace(table.rows[i].cells[1].innerText, ' ', '');
                        }
                        else {
                            Kuitansi = Kuitansi + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
                        }
                        window.returnValue = Kuitansi;
                        bcheck = true;
                    }
                    else {
                        if (Kuitansi == '') {
                            Kuitansi = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        }
                        else {
                            Kuitansi = Kuitansi + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        }
                        bcheck = true;
                    }
                }
            }
            if (bcheck) {
                //window.close();

                if (navigator.appName != "Microsoft Internet Explorer") {	//opener.dialogWin.returnFunc(Kuitansi);
                    showPopUp('../PopUp/PopUpUpload2SAP.aspx?KuitansiID=' + Kuitansi, '', 500, 760, UploadStatus);
                }
            }
            else {
                alert("Silahkan Pilih Kuitansi terlebih dahulu");
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
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Service - Deposit B - Daftar Kuitansi Pencairan</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="width: 20%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCode" runat="server">Kode Dealer*</asp:Label></td>
                            <td style="width: 2%">:</td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" runat="server"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="CURSOR:hand" border="0" alt="Klik popup">
                                </asp:Label></td>
                            <td class="titleField" style="width: 20%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label5" runat="server">Nomor Kuitansi</asp:Label></td>
                            <td style="width: 2%">:</td>
                            <td class="titleField" style="width: 29%">
                                <asp:TextBox ID="txtNoKuitansi" runat="server" MaxLength="18"></asp:TextBox>
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 20%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:Label ID="Label2" runat="server">Tipe Pengajuan</asp:Label></td>
                            <td style="width: 2%">:</td>
                            <td class="titleField" style="width: 29%">
                                <asp:DropDownList ID="ddlTipePengajuan" runat="server" Width="112px"></asp:DropDownList></td>
                            <td class="titleField" style="width: 20%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label4" runat="server">NoReg</asp:Label></td>
                            <td style="width: 2px;">:</td>
                            <td class="titleField" style="width: 29%">
                                <asp:TextBox runat="server" ID="txtNoReg" placeholder="NoReg"></asp:TextBox>
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 20%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Produk</td>
                            <td style="width: 2%">:</td>
                            <td class="titleField" style="width: 29%">
                                <asp:DropDownList Style="z-index: 0" ID="ddlProductCategory" runat="server" Width="140px"></asp:DropDownList></td>
                            <td class="titleField" style="width: 20%">
                                <asp:RadioButton ID="rbPeriodeKuitansi" runat="server" AutoPostBack="True" GroupName="KuitansiSearch"></asp:RadioButton>Tanggal 
									Kuitansi</td>
                            <td style="width: 2%">:
                            </td>
                            <td class="titleField" style="width: 29%">
                                <table>
                                    <tr>
                                        <td>
                                            <cc1:inticalendar id="icPeriodeFromKuitansi" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                        <td>s/d</td>
                                        <td>
                                            <cc1:inticalendar id="icPeriodeToKuitansi" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 20%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server">Status</asp:Label></td>
                            <td style="width: 2%">:</td>
                            <td class="titleField" style="width: 29%">
                                <asp:DropDownList ID="ddlJenisStatus" runat="server" Width="112px"></asp:DropDownList></td>
                            
                            <td class="titleField" style="width: 20%">
                                <asp:RadioButton ID="rbPeriodePengajuan" runat="server" AutoPostBack="True" GroupName="KuitansiSearch" ></asp:RadioButton>Tanggal 
									Pengajuan</td>
                            <td style="width: 2%">:</td>
                            <td class="titleField" style="width: 29%">
                                <table>
                                    <tr>
                                        <td>
                                            <cc1:inticalendar id="icPeriodeFromPengajuan" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                        <td>s/d</td>
                                        <td>
                                            <cc1:inticalendar id="icPeriodeToPengajuan" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                    </tr>
                                </table>
                            </td>
                            
                        </tr>
                        <tr>
                            <td colspan="2"></td>
                            <td style="width: 29%">
                                <asp:Button ID="btnSearch" runat="server" Width="72px" Text="Cari"></asp:Button></td>
                            <td colspan="3"></td>
                        </tr>
                        <tr >
                            <td></td>
                            <td></td>
                            <td></td>
                            <td class="titleField" style="width: 20%">
                                <asp:CheckBox ID="chkTglPencairan" runat="server" Visible="false"></asp:CheckBox><asp:Label ID="Label3" runat="server" Visible="false">Tgl Pencairan</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td style="width: 2%"></td>
                            <td class="titleField" style="width: 29%">&nbsp;
									<cc1:inticalendar id="icTglPencairan" runat="server" TextBoxWidth="70" Visible="false"></cc1:inticalendar></td>

                        </tr>
                        
                        
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="div1" style="overflow: auto; height: 270px">
                        <asp:DataGrid ID="dgDaftarPengajuanKuitansiDepositB" runat="server" Width="100%" DataKeyField="ID"
                            AllowSorting="True" PageSize="15" AllowCustomPaging="True" AllowPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro"
                            CellSpacing="1" CellPadding="3" BorderWidth="0px">
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderTemplate>
                                        <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked', document.forms[0].chkAllItems.checked)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkItemChecked" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No.">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DepositBPencairanHeader.Dealer.DealerCode")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tipe">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipe" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ProductCategory.Code" HeaderText="Produk">
                                    <HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblProdukDetail" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DepositBPencairanHeader.ProductCategory.Code")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Status">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="NomorKuitansi" ReadOnly="True" HeaderText="Nomor Kuitansi">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NoRegKuitansi" ReadOnly="True" HeaderText="Nomor Reg. Kuitansi">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TanggalKuitansi" ReadOnly="True" HeaderText="Tanggal Kuitansi" DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="DepositBPencairanHeader.CreatedTime" HeaderText="Tanggal Pengajuan">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTglpengajuan" runat="server" >
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="JVNumber" ReadOnly="True" HeaderText="JV Number">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>

                                <asp:BoundColumn DataField="Keterangan" ReadOnly="True" HeaderText="Untuk Pembayaran">
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbViewDetail" runat="server" CommandName="ViewDetail" Visible="True" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
												<img src="../images/detail.gif" border="0" style="cursor:hand">
                                        </asp:LinkButton>
                                        <%--<asp:LinkButton ID="lbViewFlow" runat="server" CommandName="lbViewFlow" Visible="True">
												<img src="../images/alur_flow.gif" border="0" style="cursor:hand">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbViewStatus" runat="server" CommandName="lbViewStatus" Visible="True">
												<img src="../images/popup.gif" border="0" style="cursor:hand">
                                        </asp:LinkButton>--%>
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
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnDownload" runat="server" Width="100" Text="Download"></asp:Button>
                            <td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
