<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmFieldFixRequestPartOrderList.aspx.vb" Inherits=".FrmFieldFixRequestPartOrderList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daftar Kategori Field Fix Campaign</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">

        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerCode = document.getElementById("txtDealerCode");
            txtDealerCode.value = selectedDealer;
        }


        function ShowPPRecallCategorySelection() {
            showPopUp('../General/../PopUp/PopUpReacallCategorySelection.aspx', '', 500, 760, RecallCategorySelection);
        }

        function RecallCategorySelection(selectedRecallRegNo) {
            var RecallRegNo = document.getElementById("txtRecallRegNo");
            RecallRegNo.value = selectedRecallRegNo;
        }

        function ShowPPNoRangkaSelection() {
            showPopUp('../General/../PopUp/PopUpChassisMasterMultiSelection.aspx', '', 500, 760, NoRangkaSelection);
        }

        function NoRangkaSelection(selectedNoRangka) {
            var txtNoRangka = document.getElementById("txtNorangka");
            txtNoRangka.value = selectedNoRangka;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" ms_positioning="GridLayout">
        <div>
            <table id="Table1" border="0" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td class="titlePage">Field Fix Campaign - Daftar Request Part Order</td>
                </tr>
                <tr>
                    <td height="1" background="../images/bg_hor.gif">
                        <img border="0" src="../images/bg_hor.gif" height="1"></td>
                </tr>
                <tr>
                    <td height="10">
                        <img border="0" src="../images/dot.gif" height="1"></td>
                </tr>
                <tr>
                    <td valign="top" align="left">
                        <table id="Table2" border="0" cellspacing="2" cellpadding="1" width="100%">
                            <tbody>

                                <tr>
                                    <td class="titleField">Kode Dealer</td>
                                    <td width="1%">:</td>
                                    <td width="75%">
                                        <asp:TextBox ID="txtKdDealer" runat="server" size="22" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                            Width="200px"></asp:TextBox>

                                        <asp:Label ID="Label1" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                        </asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="titleField" width="24%">Nomor Part</td>
                                    <td width="1%">:</td>
                                    <td width="75%">
                                        <asp:TextBox ID="txtPartNo" runat="server" size="22" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                            onblur="omitSomeCharacter('txtPartName','<>?*%$')" Width="200px"></asp:TextBox>

                                        <asp:Label ID="lblSearchPart" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                        </asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="titleField">Nama Part</td>
                                    <td width="1%">:</td>
                                    <td>
                                        <asp:Label ID="lblPartName" runat="server"></asp:Label></td>
                                    <td></td>
                                    <td></td>
                                    <td width="25%"></td>
                                </tr>

                                <tr>
                                    <td class="titleField">Tanggal Permintaan</td>
                                    <td width="1%">:</td>

                                    <td style="height: 20px; white-space: nowrap;" width="75%">
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <tr>
                                                <td>
                                                    <input id="chkTglReq" type="checkbox" name="chkTglReq" checked="true"></td>
                                                <td>
                                                    <cc1:IntiCalendar ID="icStartDate" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                                <td>
                                                &nbsp;s.d&nbsp;            
                                                <td>
                                                    <cc1:IntiCalendar ID="icEndDate" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="titleField">Tanggal Pengiriman</td>
                                    <td width="1%">:</td>

                                    <td style="height: 20px; white-space: nowrap;" width="75%">
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <tr>
                                                <td>
                                                    <input id="chkTglKirim" type="checkbox" name="chkTglKirim" checked="true"></td>
                                                <td>
                                                    <cc1:IntiCalendar ID="IntiCalendar1" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                                <td>
                                                &nbsp;s.d&nbsp;            
                                                <td>
                                                    <cc1:IntiCalendar ID="IntiCalendar2" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="titleField">Status</td>
                                    <td>:</td>
                                    <td>
                                        <select name="ddlStatus" id="ddlStatus" style="width: 150px;">
                                            <option selected="selected" value="0">Silahkan pilih Status</option>
                                            <option value="1">Baru</option>
                                            <option value="2">Validasi</option>
                                        </select>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                    <td width="75%">&nbsp; &nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Width="64px" Text="Batal" CausesValidation="False"></asp:Button>
                                        <asp:Button ID="btnSearch" runat="server" Width="64px" Text="Cari" CausesValidation="False"></asp:Button>
                                    </td>
                                </tr>
                            </tbody>

                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <div id="div1" style="height: 360px; overflow: auto">
                            <asp:DataGrid ID="dtgSentPart" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="3" BackColor="#E0E0E0" AllowSorting="True" AllowCustomPaging="True" PageSize="50"
                                AllowPaging="True" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px">
                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
                                <FooterStyle ForeColor="Black" BackColor="#ededed"></FooterStyle>
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                        <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle ForeColor="Gray" BackColor="LightPink"></ItemStyle>
                                    </asp:BoundColumn>
                                    <%--<asp:TemplateColumn Visible="false">
                                        <HeaderStyle Width="3%" CssClass="titleTableService" ></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderTemplate>
                                            <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkSelection',
														document.forms[0].chkAllItems.checked)" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelection" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelection_CheckedChanged"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>--%>

                                    <asp:TemplateColumn HeaderText="No" >
                                        <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNo"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    
                                    <asp:TemplateColumn Visible="true" HeaderText="Kode Dealer">
                                        <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliKdDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn Visible="true" HeaderText="Nomor Part">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliPartNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn Visible="true" HeaderText="Nama Part">
                                        <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliPartName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn Visible="true" HeaderText="No Service Bulletin ">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliNoBulletin" runat="server" > </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="CreatedTime" HeaderText="Tgl Permintaan (ddmmyyyy)">
                                        <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CreatedTime", "{0:dd/MM/yyyy}")%>' CssClass="textRight">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    

                                    <asp:BoundColumn DataField="MaxOrder" HeaderText="Jumlah Permintaan" Visible="true"  DataFormatString="{0:#,##0}" ItemStyle-HorizontalAlign="Right">
										<HeaderStyle Width="7%" CssClass="titleTableService" HorizontalAlign="Right"></HeaderStyle>
									</asp:BoundColumn>
                                                                         
                                    <asp:BoundColumn DataField="MileAge" HeaderText="Jumlah Disetujui" Visible="true"  DataFormatString="{0:#,##0}" ItemStyle-HorizontalAlign="Right">
										<HeaderStyle Width="7%" CssClass="titleTableService" HorizontalAlign="Right"></HeaderStyle>
									</asp:BoundColumn>

                                    <asp:TemplateColumn Visible="true" HeaderText="Status">
                                        <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliStatus" runat="server" > </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="height: 40px" align="left"></td>
                </tr>
                <tr>
                    <td width="75%">
                        <asp:Button ID="btnDownload" runat="server" Text="Download" Width="60px" Enabled="False"></asp:Button>&nbsp;
                    </td>

                </tr> 
            </table>
        </div>
    </form>
</body>
</html>
