<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDepC_InterestList.aspx.vb" Inherits=".FrmDepC_InterestList" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Daftar Interest Deposit C2</title>
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

        function Toggle(commId, imageId) {

            var div = document.getElementById(commId);
            var GetImg = document.getElementById(imageId);
            if (document.all[commId].style.display == 'none') {
                document.all[commId].style.display = 'block';
                document.all[imageId].src = '../Images/minus.gif';
            }
            else {
                document.all[commId].style.display = 'none';
                document.all[imageId].src = '../Images/plus.gif';
            }
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Deposit – Daftar Interest Deposit C</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="width: 146px">Kode Dealer</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
                                    runat="server"></asp:TextBox>
                                <asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label>
                            </td>
                            <td></td>
                            <td class="titleField" style="width: 146px">Produk</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">

                                <asp:DropDownList Style="z-index: 0" ID="ddlProductCategory" runat="server" Width="140px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Periode</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:DropDownList ID="ddlPeriode" runat="server"></asp:DropDownList>
                            </td>
                            <td></td>
                            <td class="titleField" style="width: 146px"></td>
                            <td style="width: 2px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Tahun</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
                            </td>
                            <td colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px"></td>
                            <td style="width: 2px"></td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Cari" Width="72px"></asp:Button>
                                <asp:Button ID="BtnDownload" runat="server" Text="Download" Width="72px"></asp:Button>
                            </td>
                            <td colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px"></td>
                            <td style="width: 2px"></td>
                            <td style="font-style: italic; font-size: 10px; color: red;">*Interest Amount Automatic Transfer to Dealer Deposit
                            (Dokumen Kwitansi tidak dikirim ke MMKSI)</td>
                            <td colspan="4"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="dgDaftarInterest" runat="server" BorderWidth="0px" CellSpacing="1"
                        BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" ShowFooter="False"
                        AllowPaging="True" AllowCustomPaging="True" PageSize="25" AllowSorting="True">
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Details">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemStyle Width="10px" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <img id="image_" runat="server" src="../images/plus.gif" border="0" style="cursor: hand">
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="No.">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                    <asp:Button ID="btnDepositAInterestHID" runat="server" Visible="False"></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerCode" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerName" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="ProductCategory.Code" HeaderText="Produk">
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblProduk" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="InterestAmount" SortExpression="InterestAmount" ReadOnly="True" HeaderText="Interest"
                                DataFormatString="{0:#,###}">
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TaxAmount" SortExpression="TaxAmount" ReadOnly="True" HeaderText="Tax (15%)"
                                DataFormatString="{0:#,###}">
                                <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Netto" SortExpression="NettoAmount">
                                <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.NettoAmount"),"0,000") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Status">
                                <HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>                                    
                                </ItemTemplate>
                            </asp:TemplateColumn>                            
                            <asp:TemplateColumn Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepositAInterestHID" runat="server" Text='<%# DataBinder.Eval (Container.DataItem, "Id") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn Visible="False" HeaderText="FileName">
                                <ItemTemplate>
                                    <asp:Label ID="lblFullNameKwitansi" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FilePathKwitansi")%>'></asp:Label>
                                    <asp:Label ID="lblFullNameLetter" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FilePathLetter")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>  
                            <asp:TemplateColumn HeaderText="File Kwitansi">  
                                <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>                              
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnSimpanKwitansi" CausesValidation="False" CommandName="SimpanKwitansi" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'
                                        Text="Simpan Kwitansi" runat="server" ToolTip="Simpan Kwitansi">
									<img src="../images/download.gif" border="0" alt="Simpan"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="File Letter" >  
                                <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>                              
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnSimpanLetter" CausesValidation="False" CommandName="SimpanLetter" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'
                                        Text="Simpan Letter" runat="server" ToolTip="Simpan Letter">
									<img src="../images/download.gif" border="0" alt="Simpan"></asp:LinkButton>
                                    </td>
				</tr>
				<tr>
                    <td colspan="3"></td>
                    <td colspan="2">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td colspan="4">
                                    <div id="divDepositAInterestD" runat="server" style="display: none">
                                        <asp:DataGrid ID="dgDaftarInterestDetail" runat="server" BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro"
                                            BackColor="#CDCDCD" AutoGenerateColumns="False" ShowFooter="False" AllowPaging="False" AllowCustomPaging="True"
                                            PageSize="25" AllowSorting="False" ShowHeader="True" CellPadding="0" BorderStyle="None" Visible="True"
                                            Width="100%">
                                            <ItemStyle BackColor="White"></ItemStyle>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                            <Columns>
                                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Month" SortExpression="Month">
                                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Month") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="InterestAmount" SortExpression="InterestAmount" ReadOnly="True" HeaderText="Interest"
                                                    DataFormatString="{0:#,###}">
                                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:BoundColumn Visible="False" DataField="NettoAmount" SortExpression="NettoAmount" ReadOnly="True"
                                                    HeaderText="Netto" DataFormatString="{0:#,###}">
                                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                        </table>
                                </ItemTemplate>
                            </asp:TemplateColumn> 
                            <%--<asp:TemplateColumn Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepositAInterestHID" runat="server" Text='<%# DataBinder.Eval (Container.DataItem, "Id") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn> --%>                        
                        </Columns>
                    </asp:DataGrid>




                </td>
            </tr>
        </table>
    </form>
</body>
</html>
