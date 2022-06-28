<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDepB_Plafon.aspx.vb" Inherits=".FrmDepB_Plafon" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmMaterialPromotionUpload</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">

        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer;
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = tempParam;
        }

        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        }



    </script>
    <style type="text/css">
        .HiddenItem {
            display: none;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Service - Plafon Deposit B</td>
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
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="width: 146px">Kode Dealer</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtKodeDealer" Width="140px" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
                                    runat="server"></asp:TextBox>
                                <asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Produk</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:DropDownList ID="ddlProductCategory" runat="server" Width="140px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Grade</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:DropDownList ID="ddlGrade" runat="server" Width="140px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Tahun</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Total Plafon</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtPlafon" runat="server" Width="140px" onkeypress="return NumberOnly()"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px"></td>
                            <td style="width: 2px"></td>
                            <td>
                                <asp:Button ID="btnSimpan" runat="server" Text="Simpan" Width="70px"></asp:Button>
                                <asp:Button ID="btnBatal" runat="server" Text="Batal" Width="70px"></asp:Button>
                                <asp:Button ID="btnCari" runat="server" Text="Cari" Width="70px"></asp:Button>
                                <asp:Button ID="btnDownload" runat="server" Text="Download" Width="70px"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="20%"></td>
                            <td width="1%"></td>
                            <td width="80%">
                                <asp:Label ID="lblError" runat="server" Width="200px" ForeColor="Red" EnableViewState="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div id="div1" style="overflow: auto; width: 100%; height: 260px">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:DataGrid ID="dgPlafonList" runat="server" BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro"
                                                    BackColor="#CDCDCD" AutoGenerateColumns="False" ShowFooter="True" AllowPaging="True" AllowCustomPaging="True"
                                                    PageSize="25" AllowSorting="True">
                                                    <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                                    <ItemStyle BackColor="White"></ItemStyle>
                                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                                    <Columns>
                                                        <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                                            <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                        </asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="No.">
                                                            <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                                            <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
                                                            <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn SortExpression="ProductCategory.Code" HeaderText="Produk">
                                                            <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProduct" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn SortExpression="GradeDealer" HeaderText="Grade">
                                                            <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGrade" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn DataField="PeriodePlafon" SortExpression="PeriodePlafon" ReadOnly="True" HeaderText="Tahun">
                                                            <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="JumlahPlafon" ReadOnly="True" HeaderText="Total Plafon" DataFormatString="{0:#,###}">
                                                            <HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <asp:TemplateColumn>
                                                            <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnView" runat="server" Text="Lihat" CommandName="View" CausesValidation="False">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnEdit" runat="server" Text="Ubah" CommandName="Edit" CausesValidation="False">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnDelete" runat="server" Text="Hapus" CommandName="Delete" CausesValidation="False">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input id="hdnMCPConfirmation" type="hidden" runat="server" name="hdnMCPConfirmation">
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <br>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
