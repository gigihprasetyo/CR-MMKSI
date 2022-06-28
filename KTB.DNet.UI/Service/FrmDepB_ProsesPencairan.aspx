<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDepB_ProsesPencairan.aspx.vb" Inherits=".FrmDepB_ProsesPencairan" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Daftar Proses Pengajuan Pencairan</title>
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
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer;
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = tempParam;
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

        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        }

        function Barcode(var1) {
            var AsciiValue = event.keyCode
            var txtNoRegKuitansi = document.getElementById('txtNoRegKuitansi');
            

            if (AsciiValue == 68 || AsciiValue == 100)   
            {
                if (var1.value != '')
                {
                    var1.value = var1.value + ';';
                } 
            }

            if (AsciiValue == 13 || AsciiValue == 9) {
                if (var1.value != '') {
                    event.returnValue = false;
                }
            }
            
            return true;
        }

        function Barcode2(var1) {

            var txtNoRegKuitansi = document.getElementById('txtNoRegKuitansi');
            //console.log(txtNoRegKuitansi.value);
            return true;
        }

    </script>

        <script type="text/javascript">

            (function () {

               

            }());

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Service - Deposit B - Proses Pengajuan Pencairan</td>
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
                            <td class="titleField" style="width: 146px">
                                <asp:Label ID="lblCode" runat="server">Kode Dealer*</asp:Label></td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:TextBox ID="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" runat="server"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="CURSOR:hand" border="0" alt="Klik popup">
                                </asp:Label></td>
                            <td class="titleField" style="width: 146px">Periode</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="cbPeriode" runat="server" AutoPostBack="True"></asp:CheckBox></td>
                                        <td>
                                            <cc1:inticalendar id="icPeriodeFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                        <td>s/d</td>
                                        <td>
                                            <cc1:inticalendar id="icPeriodeTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Tipe Pengajuan</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:DropDownList ID="ddlTipePengajuan" runat="server"></asp:DropDownList></td>
                            <td class="titleField" style="width: 146px">No. Ref Surat Pengajuan</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtNoPengajuan" runat="server" MaxLength="18"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Status</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:DropDownList ID="ddlStatusPengajuan" runat="server"></asp:DropDownList></td>
                            <td class="titleField">No. Reg. Pengajuan&nbsp;</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtNoReg" runat="server" MaxLength="18"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Produk</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">

                                <asp:DropDownList Style="z-index: 0" ID="ddlProductCategory" runat="server" Width="140px"></asp:DropDownList>
                            </td>
                            <td class="titleField">No. Reg. Kuitansi&nbsp;</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtNoRegKuitansi" runat="server" MaxLength="400" width="50%"    onkeypress="return Barcode(this)" TextMode="MultiLine" ></asp:TextBox></td>
                        </tr>
                         <tr>
                            <td class="titleField" style="width: 146px">No.&nbsp; Pengajuan&nbsp;
                               </td>
                            
                            <td style="width: 2px">:</td>
                            <td class="titleField"> <asp:TextBox ID="txtNoPeng" runat="server" MaxLength="18"></asp:TextBox></td>
                            <td></td>
                            <td colspan="2"></td>
                        </tr>

                        <tr>
                            <td class="titleField" style="width: 146px">
                                <asp:Button ID="btnSearch" runat="server" Width="72px" Text="Cari"></asp:Button></td>
                            <td style="width: 2px"></td>
                            <td class="titleField"></td>
                            <td></td>
                            <td colspan="2"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="div1" style="overflow: auto; height: 270px">
                        <asp:DataGrid ID="dgDaftarPengajuanPencairanDepositB" runat="server" Width="100%" AllowSorting="True"
                            PageSize="25" AllowCustomPaging="True" AllowPaging="True" ShowFooter="False" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro"
                            CellSpacing="1" CellPadding="3" BorderWidth="0px">
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderTemplate>
                                        <input id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('chkItem', document.all.chkAllItems.checked)"
                                            type="checkbox">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkItem" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No." Visible="False">
                                    <HeaderStyle Width="0%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ProductCategory.Code" HeaderText="Produk">
                                    <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblProdukDetail" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProductCategory.Code") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Type" HeaderText="Tipe">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipe" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="NoReferensi" SortExpression="NoSurat" ReadOnly="True" HeaderText="No.Ref">
                                    <HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No. Reg">
                                    <HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoReg" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoReg")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No. Reg. Kuitansi">
                                    <HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoRegKuitansi" runat="server" >
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                   <asp:TemplateColumn HeaderText="No. Pengajuan">
                                    <HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoPengajuan" runat="server" >
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>


                                <asp:TemplateColumn HeaderText="No. JV">
                                    <HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblJVNumber" runat="server" >
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Status">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn  >
                                  <asp:BoundColumn DataField="CreatedTime" Visible="true" ReadOnly="True" HeaderText="Tanggal" DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                </asp:BoundColumn>

                                <asp:TemplateColumn HeaderText="Tanggal Pelunasan">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaymentDate" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn> 


                                <asp:BoundColumn Visible="false" DataField="DealerAmount" ReadOnly="True" HeaderText="Jumlah Pengajuan" DataFormatString="{0:#,###}">
                                    <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundColumn>

                                 <asp:TemplateColumn HeaderText="Jumlah Pengajuan">
                                    <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblJumlahPengajuan" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Jumlah Disetujui">
                                    <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtJumlahDisetujui" runat="server" Width="100%" ReadOnly="true" onkeypress="return NumberOnly()" style="text-align:right"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Alasan">
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAlasan" runat="server" Width="100%"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbViewDetail" runat="server" CommandName="ViewDetail" Visible="True">
												<img src="../images/detail.gif" border="0" style="cursor:hand">
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="lbViewFlow" runat="server" CommandName="lbViewFlow" Visible="false">
												<img src="../images/alur_flow.gif" border="0" style="cursor:hand">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbViewStatus" runat="server" CommandName="lbViewStatus" Visible="True">
												<img src="../images/popup.gif" border="0" style="cursor:hand">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbDelete" runat="server" CommandName="Delete" Visible="False">
												<img src="../images/trash.gif" border="0" style="cursor:hand">
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
                <td>
                    <asp:Label ID="lblUbahStatus" runat="server" Visible="true" Font-Italic="True" Font-Bold="True">Mengubah Status :</asp:Label>
                    <asp:DropDownList ID="ddlAction" runat="server" Visible="true"></asp:DropDownList>
                    <asp:Button ID="btnProses" runat="server" Text="Ubah Status" Visible="true"></asp:Button>
                    <asp:Button ID="btnTransfer" runat="server" Text="Transfer" Visible="False"></asp:Button>
                    <asp:Button ID="btnDownload" runat="server" Text="Download"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
