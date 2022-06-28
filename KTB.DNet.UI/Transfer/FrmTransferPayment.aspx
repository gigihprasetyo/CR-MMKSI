<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="FrmTransferPayment.aspx.vb" Inherits=".FrmTransferPayment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Transfer Payment</title>
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
	<script language="javascript" src="../WebResources/FormFunctions.js"></script>

    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet" />

    <style type="text/css">
        .auto-style1 {
            height: 33px;
        }
    </style>

</head>
<body ms_positioning="GridLayout" style="margin: 5px;">
    <form id="form1" runat="server">
        <div>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage" style="height: 16px">&nbsp;
						<asp:Label ID="lblTitle" runat="server">TRANSFER - Pembayaran Transfer</asp:Label></td>
                </tr>
                <tr style="height: 1px;">
                    <td style="height: 1px; background-image: url('../images/bg_hor.gif'); background-size: auto;"></td>
                </tr>
                <tr>
                    <td>
                        <table cellspacing="8" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td width="150px"></td>
                                <td width="1px"></td>
                                <td width="200px"></td>
                                <td width="150px"></td>
                                <td width="1px"></td>
                                <td width="300px"></td>
                                <td width="1px"></td>
                            </tr>
                            <tr>
                                <td><b>Kode Dealer</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                                    <div style="display:none;">
                                        <asp:TextBox ID="txtDealerID" runat="server" Visible ="False"></asp:TextBox>
                                    </div>
                                </td>
                                <td><b>Tujuan Pembayaran</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <asp:DropDownList ID="ddlPaymentPurpose" runat="server" AutoPostBack="True" Width="142px" ></asp:DropDownList>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td><b>No. Reg</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <asp:Label ID="lblRegNumber" runat="server"></asp:Label>
                                </td>
                                <td><b>Tgl Dibuat</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td><b>Tgl Jatuh Tempo</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <cc1:IntiCalendar id="calDueDate" runat="server" CanPostBack="True"></cc1:IntiCalendar>

                                    <div style="display:nonexxx;">
                                        <script>
                                            function calDueDate_LostFocus() {
                                                var btn = document.getElementById('btnRefresSO');
                                                btn.click();
                                            }

                                        </script>
                                        <asp:button runat="server" ID="btnRefresSO" Text="Refresh SO" />
                                    </div>
                                </td>
                                <td><b>Tgl Transfer</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <cc1:IntiCalendar id="calPlanTransferDate" runat="server"></cc1:IntiCalendar>
                                </td> 
                                <td></td>
                            </tr>
                            <tr>
                                <td><b>Status</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                </td>
                                <td><b>Total Transfer</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="6">

                                    <div id="divHidden" style="overflow: auto; width: 100%; height: 200px">
                                        <asp:DataGrid ID="dtgMain" runat="server" Width="100%" CellSpacing="1" GridLines="Horizontal"
                                            CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0"
                                            AutoGenerateColumns="False" PageSize="25" AllowCustomPaging="true" AllowPaging="false" AllowSorting="True"
                                            DataKeyField="ID" ShowFooter="True">
                                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                            <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                                            <ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
                                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                            <Columns>

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
                                                <asp:TemplateColumn HeaderText="SO Number">
                                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblSONumber"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label runat="server" ID="lblSONumberE"></asp:Label>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList runat="server" ID="ddlSOF" Width="80%" OnChange="ddlSOChanged(this);" AutoPostBack="true"></asp:DropDownList>
                                                        <asp:Button runat="server" ID="btnSOF" CommandName="SOChangeF" Style="visibility: hidden;"></asp:Button>
                                                        <script>
                                                            function ddlSOChanged(ddl) {
                                                                var id = ddl.id.replace('ddlSOF', 'btnSOF'); //dtgMain__ctl2_ddlSOF
                                                                var btn = document.getElementById(id);

                                                                btn.click();
                                                            }
                                                        </script>
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Debit Number">
                                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Top"/>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDebitNumber">&nbsp;</asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label runat="server" ID="lblDebitNumberE">&nbsp;</asp:Label>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblDebitNumberF" ></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Amount">
                                                    <HeaderStyle ForeColor="White" Width="15%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Top"/>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblAmount">&nbsp;</asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label runat="server" ID="lblAmountE">&nbsp;</asp:Label>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblAmountF" ></asp:Label>
                                                        <div style="display:none;">
                                                        <asp:TextBox runat="server" ID="txtAmountF" Width="100%" onkeypress="return numericOnlyUniv(event)"
                                                            CssClass="textRight" onkeyup="AutoThousandSeparator(this,event);"></asp:TextBox>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Desc">
                                                    <HeaderStyle ForeColor="White" Width="15%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Top"/>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDesc">&nbsp;</asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label runat="server" ID="lblDescE">&nbsp;</asp:Label>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblDescF">&nbsp;</asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Factoring">
                                                    <HeaderStyle ForeColor="White" Width="5%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Top"/>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblFactoring">&nbsp;</asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label runat="server" ID="lblFactoringE">&nbsp;</asp:Label>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblFactoringF" ></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>

                                                <asp:EditCommandColumn visible="false" ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
                                                    CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; &quot; alt=&quot;Batal&quot;&gt;"
                                                    EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
                                                    <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:EditCommandColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderStyle ForeColor="White" Width="10%" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Del" Visible="False">
															<img src="../images/trash.gif" border="0" alt="Hapus">
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="Add">
															<img src="../images/add.gif" border="0" alt="Tambah">
                                                        </asp:LinkButton>
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                        <asp:button id="btnRemove" runat="server" Width="150px" Text="Hapus Pilihan SO"></asp:button>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    
                                    </div>
                                </td>

                                <%--
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                --%>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="6" class="auto-style1">
                                    <asp:button id="BtnKembali" runat="server" Width="100px" Text="Kembali" Visible="false"></asp:button> &nbsp;&nbsp;
                                    <asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button>
                                    &nbsp;&nbsp;
                                    <asp:button id="btnValidasi" runat="server" Width="60px" Text="Validasi"></asp:button>
                                    &nbsp;&nbsp;
                                    <asp:button id="btnPercepatan" runat="server" Width="100px" Text="Percepatan"></asp:button>
                                    &nbsp;&nbsp;
                                    <asp:button id="btnKonfirmasi" runat="server" Width="100px" Text="Konfirmasi"></asp:button>
&nbsp;&nbsp;
                                    <asp:button id="btnBatalKonfirmasi" runat="server" Width="100px" Text="Batal Konfirmasi"></asp:button>
                                   
                                     

                                </td>
                                <%--
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                --%>
                                <td class="auto-style1"></td>
                            </tr>
                            <tr>
                                <td colspan="6"></td>

                                <%--
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                --%>
                                <td></td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
