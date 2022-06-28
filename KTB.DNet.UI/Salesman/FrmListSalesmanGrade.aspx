<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmListSalesmanGrade.aspx.vb" Inherits=".FrmListSalesmanGrade" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Salesman Grade</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/jquery-1.8.3.min.js"></script>

    <script type="text/javascript">
       
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
        
    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" enctype="multipart/form-data" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="lblPageTitle" runat="server"></asp:Label>
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
                    <table id="tbl7" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td>
                                <table id="table5">
                                    <tr id="trMks" runat="server">
                                        <td width="150" class="titleField">Kode Dealer</td>
                                        <td>:</td>
                                        <td nowrap="nowrap">
                                            <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtKodeDealer" runat="server" Width="200px"></asp:TextBox>&nbsp;
										    <asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                                        </td>
                                        <td width="150" class="titleField">Tahun Fiskal</td>
                                        <td>:</td>
                                        <td nowrap="nowrap">
                                            <asp:TextBox onkeypress="return numericOnlyUniv(event)" ID="txttahun" runat="server" Width="150px"></asp:TextBox> &nbsp;
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*" ValidationExpression="^[\s\S]{0,250}$"
                                        ControlToValidate="txttahun" Display="Dynamic"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr id="tr1" runat="server">
                                        <td width="150" class="titleField">Kode</td>
                                        <td >:</td>
                                        <td nowrap="nowrap" width="300px" >
                                            <asp:HiddenField ID="hdnGradeID" runat="server" />
                                            <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtSalesmanCode" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                        <td width="150" class="titleField">Periode</td>
                                        <td>:</td>
                                        <td nowrap="nowrap" >
                                            <asp:DropDownList ID="ddlPeriode" Width="150px" runat="server" TabIndex="9"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField">Nama</td>
                                        <td>:</td>
                                        <td nowrap="nowrap">
                                           <asp:TextBox  ID="txtSalesmanName" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                        <td width="150" class="titleField">Grade</td>
                                        <td>:</td>
                                        <td nowrap="nowrap" >
                                            <asp:DropDownList ID="ddlGrade" Width="150px" runat="server" TabIndex="9"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField">&nbsp;</td>
                                        <td></td>
                                        <td nowrap="nowrap">
                                           &nbsp;
                                        </td>
                                        <td width="150" class="titleField">Productivity</td>
                                        <td>:</td>
                                        <td nowrap="nowrap" >
                                            <asp:TextBox  ID="txtScore" runat="server" Width="50px"></asp:TextBox></td>
                                    </tr>

                                    <tr id="trCari" runat="server">
                                        <td width="150"  class="titleField"></td>
                                        <td></td>
                                        <td height="37px" nowrap="nowrap" >
                                            <asp:Button ID="btnSimpan" runat="server" Width="75px" Text="Simpan" CausesValidation="true" TabIndex="14"></asp:Button> &nbsp;
                                            <asp:Button ID="btnBatal" runat="server" Width="75px" Text="Batal" CausesValidation="False" TabIndex="14"></asp:Button>&nbsp;
                                            <asp:Button ID="btnCari" runat="server" Width="75px" Text="Cari" CausesValidation="False" TabIndex="14"></asp:Button>
                                        </td>
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
                <td>
                    <div id="div2" runat="server" style="overflow: auto; height: 320px">
                        <asp:DataGrid ID="dtgSalesmanGrade" runat="server" Width="100%" AutoGenerateColumns="False"
                            GridLines="Horizontal" CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD"
                            AllowCustomPaging="True" AllowPaging="true" AllowSorting="True" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No" SortExpression="">
                                    <HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdnID" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="SalesmanHeader.Dealer.DealerCode">
                                    <HeaderStyle Width="7%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodeDealer" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode" SortExpression="SalesmanHeader.SalesmanCode">
                                    <HeaderStyle Width="7%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSalesmanCode" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nama" SortExpression="SalesmanHeader.Name">
                                    <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNama" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn HeaderText="Posisi" SortExpression="SalesmanHeader.JobPosition.Description">
                                    <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPosisi" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tahun Fiskal" SortExpression="Year">
                                    <HeaderStyle Width="7%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTahun" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Periode" SortExpression="Period">
                                    <HeaderStyle Width="7%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPeriode" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Grade" SortExpression="Grade">
                                    <HeaderStyle Width="7%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrade" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Produktivity" SortExpression="Grade">
                                    <HeaderStyle Width="7%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblProduktivity" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="4%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDetail" runat="server" CausesValidation="False" CommandName="detail" Text="Detail">
										    <img src="../images/detail.gif" border="0" alt="Detail"/>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="False" CommandName="edit" Text="Edit">
										    <img src="../images/edit.gif" border="0" alt="Edit"/>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnHapus" runat="server" Width="8px" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
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
                    <asp:Button ID="btnDownload" runat="server" Width="75px" Text="Download" TabIndex="15"></asp:Button>&nbsp;
                    
                </td>
            </tr>
        </table>
    </form>
</body>
</html>


