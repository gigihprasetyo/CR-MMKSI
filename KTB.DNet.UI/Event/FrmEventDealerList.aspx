<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmEventDealerList.aspx.vb" Inherits=".FrmEventDealerList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmBabitList</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
        function ShowPopUpTO() {
            var txtKodeDealer = document.getElementById("txtKodeDealer");
            var txtKodeTempOut = document.getElementById("txtKodeTempOut");
            showPopUp('../PopUp/PopUpDealerBranchEventMultipleSelection.aspx?DealerCode=' + txtKodeDealer.value + '&DealerBranchCode=' + txtKodeTempOut.value + '&f=event', '', 430, 800, TemporaryOutlet);
        }

        function TemporaryOutlet(selectedDealerBranch) {
            var txtKodeTempOut = document.getElementById("txtKodeTempOut");
            var hdnTempOut = document.getElementById("hdnTempOut");
            txtKodeTempOut.value = selectedDealerBranch;
            hdnTempOut.value = selectedDealerBranch;
        }

        function ShowPopUpDealer() {
            var ddlEventCategory = document.getElementById("ddlCategory");
            var index = ddlEventCategory.selectedIndex;
            var valCategory = ddlEventCategory.options[index].value;
            var txtDealerCode = document.getElementById("txtKodeDealer");
            showPopUp('../PopUp/PopUpDealerSelectionEvent.aspx?DealerCode=' + txtDealerCode.value + '&Category=' + valCategory, '', 430, 800, GetDealer);
            //showPopUp('../PopUp/PopUpDealerSelectionEvent.aspx', '', 430, 800, GetDealer);
        }

        function GetDealer(selectedDealerCode) {
            var txtKodeDealer = document.getElementById("txtKodeDealer");
            var hdnDealer = document.getElementById("hdnDealer");
            var txtKodeTempOut = document.getElementById("txtKodeTempOut");
            var hdnTempOut = document.getElementById("hdnTempOut");

            hdnDealer.value = selectedDealerCode;
            txtKodeDealer.value = selectedDealerCode;
            if (txtKodeDealer == "") {
                txtKodeTempOut.value = ''
                hdnTempOut.value = ''
            }
            //if (navigator.appName == "Microsoft Internet Explorer") {
            //    hdnDealer.blur();
            //}
            //else {
            //    hdnDealer.onchange();
            //}
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">EVENT -&nbsp; DAFTAR EVENT DEALER</td>
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
                    <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td align="left" style="width: 50%">
                                <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                                    <tr>
                                        <td class="titleField">Kategori Dealer</td>
                                        <td>:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="20%" valign="top">
                                            <asp:Label ID="lblDealerSearch" runat="server">Kode Dealer</asp:Label></td>
                                        <td width="1%">:</td>
                                        <td width="34%">
                                            <asp:TextBox ID="txtKodeDealer" runat="server" Width="200px" TextMode="MultiLine" Height="30px"></asp:TextBox>
                                            <asp:Label ID="lblKodeDealer" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hdnDealer" runat="server" />
                                            <asp:label ID="lblPopUpDealer" runat="server" Width="16px">
                                            <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                            </asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" valign="top">Kode Temporary Outlet</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtKodeTempOut" runat="server" Width="200px" TextMode="MultiLine" Height="30px"></asp:TextBox>
                                            <asp:HiddenField ID="hdnTempOut" runat="server" />
                                            <asp:label ID="lblPopUpTO" runat="server" Width="16px">
                                            <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                            </asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="height: 18px">Periode Event</td>
                                        <td>:</td>
                                        <td>
                                            <table cellspacing="0" cellpadding="0" border="0" width="80%">
                                                <tr valign="top">
                                                    <td>
                                                        <asp:CheckBox ID="cbDate" runat="server" Width="2px"></asp:CheckBox>
                                                    </td>
                                                    <td>
                                                        <cc1:IntiCalendar ID="icPeriodeStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                                    <td valign="top">&nbsp;&nbsp;s.d&nbsp;&nbsp;</td>
                                                    <td>
                                                        <cc1:IntiCalendar ID="icPeriodeEnd" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td class="titleField" style="height: 18px">Nama Event</td>
                                        <td style="height: 18px">:</td>
                                        <td style="height: 18px">
                                            <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtEventName"
                                                runat="server" MaxLength="50" Width="280px"></asp:TextBox>
                                            <br />
                                            <br />
                                            <asp:Button ID="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:Button>
                                            <asp:Button ID="btnClear" runat="server" Width="60px" Text=" Batal "></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="left" style="width: 50%"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <hr />
        <div id="div1" style="overflow: auto; height: 300px">
            <asp:DataGrid ID="dgEventDealerList" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" GridLines="Horizontal"
                CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True"
                PageSize="25" AllowPaging="True" BorderStyle="None" DataKeyField="ID" ShowFooter="false">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="White" HorizontalAlign="Center"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="No">
                        <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kategori Dealer">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblCategory" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Category.CategoryCode")%>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Dealer">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealer" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Temporary Outlet">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTempOut" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Event">
                        <HeaderStyle ForeColor="White" Width="40%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblEventDealerName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tgl Periode Mulai">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPeriodStart" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tgl Periode Selesai">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPeriodEnd" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnDetail" runat="server" CommandName="Detail" CausesValidation="False">
												            <img src="../images/detail.gif" border="0" alt="Detail"></asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="Edit" CausesValidation="False">
												            <img src="../images/edit.gif"  border="0" alt="Ubah"></asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnDelete" OnClientClick="return confirm('Anda yakin mau hapus?');" CausesValidation="False"
                                CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Delete" Text="Hapus" runat="server">
                                        <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>

                        </ItemTemplate>
                    </asp:TemplateColumn>

                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>
    </form>
</body>
</html>
