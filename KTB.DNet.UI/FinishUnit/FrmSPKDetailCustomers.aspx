<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmSPKDetailCustomers.aspx.vb" Inherits=".FrmSPKDetailCustomers" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>MARKETING - Daftar Konsumen Faktur</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script language="javascript" src="../WebResources/FormFunctions.js" type="text/javascript"></script>
    <script language="javascript">
        var indexRow;
        function BackToPrev() {
            var url = document.getElementById("txtUrlToBack").value;
            window.location = url;
        }



        function GetCurrentInputIndex() {
            var dtgConsumentFaktur = document.getElementById("dtgConsumentFaktur");
            var currentRow;
            var index = 0;
            var inputs;
            var indexInput;
            var i;

            for (index = 0; index < dtgConsumentFaktur.rows.length; index++) {
                inputs = dtgConsumentFaktur.rows[index].getElementsByTagName("INPUT");
                if (inputs != null && inputs.length > 0) {
                    for (indexInput = 0; indexInput < inputs.length; indexInput++) {
                        if (inputs[indexInput].type != "hidden")
                            return index;
                    }
                }
            }
            return -1;
        }

        function GetIndex(CtlID) {
            if (!navigator.appName == "Microsoft Internet Explorer") {
                var row = CtlID.parentElement.parentElement;
                indexRow = row.rowIndex;
                return row.rowIndex;
            }
            else {
                var row = CtlID.parentNode.parentNode;
                indexRow = row.rowIndex;
                return row.rowIndex;
            }
        }



    </script>
</head>
<body onfocus="return checkModal()" onclick="checkModal()" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">MARKETING - Daftar Konsumen Faktur</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" colspan="3" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="width: 24%; height: 24px">Kode Dealer</td>
                            <td style="width: 1%">:</td>
                            <td style="width: 25%" class="titleField">
                                <asp:Label ID="lblDealer" runat="server"></asp:Label></td>
                            <td class="titleField" style="width: 24%">Tanggal Buka SPK</td>
                            <td style="width: 1%">:</td>
                            <td style="width: 25%" class="titleField">
                                <asp:Label ID="lblSPKDate" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 24%; height: 24px">Model / Tipe / Warna</td>
                            <td>:</td>
                            <td class="titleField">
                                <asp:Label ID="lblVehcile" runat="server"></asp:Label></td>
                            <td class="titleField">Total Unit</td>
                            <td>:</td>
                            <td class="titleField">
                                <asp:Label ID="lblQtyDetail" runat="server"></asp:Label>
                            </td>
                        </tr>

                          <tr>
                            <td class="titleField" style="width: 24%; height: 24px">Reg SPK
                            <td>:</td>
                            <td class="titleField">
                                <asp:Label ID="lblRegSPK" runat="server"></asp:Label></td>
                            <td class="titleField">Salesman</td>
                            <td>:</td>
                            <td class="titleField">
                                <asp:Label ID="lblSalesman" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <div id="div1" style="overflow: auto; width: 100%; height: 100px">
                                    <asp:DataGrid ID="dtgConsumentFaktur" runat="server" Width="100%" AutoGenerateColumns="False"
                                        BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" CellPadding="1" ShowFooter="True"
                                        BackColor="#E0E0E0">
                                        <SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
                                        <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ID" HeaderText="id">
                                                <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                            </asp:BoundColumn>

                                            <asp:TemplateColumn Visible="true">
                                                <HeaderStyle Width="3%" CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <HeaderTemplate>
                                                    <input id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('chkSelect', document.all.chkAllItems.checked)"
                                                        type="CheckBox">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" ViewStateMode="Enabled"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="3%" CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server" NAME="lblNo" Text='<%# container.itemindex+1 %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Size="Small"></FooterStyle>
                                            </asp:TemplateColumn>


                                            <asp:TemplateColumn HeaderText="Nama Konsumen Faktur">
                                                <HeaderStyle Width="20%" CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblConsumentName" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Size="Small"></FooterStyle>
                                            </asp:TemplateColumn>


                                            <asp:TemplateColumn HeaderText="Alamat">
                                                <HeaderStyle Width="30%" CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblConsumentAddress" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Size="Small"></FooterStyle>
                                            </asp:TemplateColumn>

                                               <asp:TemplateColumn HeaderText="Jumlah">
                                                <HeaderStyle Width="7%" CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Size="Small"></FooterStyle>
                                            </asp:TemplateColumn>

                                               <asp:TemplateColumn HeaderText="Jadi Konsumen">
                                                <HeaderStyle Width="10%" CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                <asp:CheckBox ID="chkKonsumen" Enabled="false" runat="server" />
                                                </ItemTemplate>
                                                <FooterStyle Font-Size="Small"></FooterStyle>
                                            </asp:TemplateColumn>


                                            <asp:TemplateColumn HeaderText="Profile Kendaraan">
                                                <HeaderStyle Width="8%" CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>

                                                    <asp:LinkButton ID="lblVehicleProvile" runat="server" CommandName="vProfile" Visible="True">
												<img src="../images/popup.gif" border="0" style="cursor:hand" runat="server" id="imgVProfile"/> 
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterStyle Font-Size="Small"></FooterStyle>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Data Konsumen">
                                                <HeaderStyle Width="15%" CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" Visible="true">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                                    &nbsp;
                                                      <asp:LinkButton ID="lbtnDetail" runat="server" CommandName="Detail" Visible="true">
															<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
                                                    &nbsp;
                                                      <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" Visible="true">
															<img src="../images/edit.gif" border="0" alt="Edit"></asp:LinkButton>
                                                    &nbsp;
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                <FooterTemplate>

                                                    <asp:LinkButton ID="lbtnAdd" ToolTip="Tambah Data Konsumen Faktur" runat="server" Text="Tambah Data Konsumen Faktur" CommandName="AddConsumentFaktur">
											<img src="../images/add.gif" border="0" alt="Tambah Data Konsumen Faktur" align="center" align="middle" style="Cursor:hand"></asp:LinkButton>
                                                     
                                                </FooterTemplate>
                                            </asp:TemplateColumn>


                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>

                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblError" runat="server" Width="624px" EnableViewState="False" ForeColor="Red"></asp:Label><br>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server"></asp:ValidationSummary>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="40">
                    <asp:Button ID="btnJadiKonsumen" runat="server" Text="Jadi Konsumen" Visible="false"></asp:Button>
                     <asp:Button ID="btnBack" runat="server" CausesValidation="False" Text="Kembali"></asp:Button>
                    <asp:TextBox ID="txtUrlToBack" Style="visibility: hidden" ReadOnly="True" Text="" runat="server"></asp:TextBox>
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
