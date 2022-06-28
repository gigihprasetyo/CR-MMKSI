<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDaftarEventNational.aspx.vb" Inherits=".FrmDaftarEventNational" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmDaftarEventNational</title>
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

            showPopUp('../PopUp/PopUpEventNational.aspx?m=d', '', 430, 800, EventSelection);
        }

        function EventSelection(selectedRefNumber) {
            var hdnTemporaryEvent = document.getElementById("hdnTempEvent");
            var txtKodeEvent = document.getElementById("txtKodeEvent");
            var lblNamaEvent = document.getElementById("lblNamaEvent");
            hdnTemporaryEvent.value = selectedRefNumber;
            txtKodeEvent.value = selectedRefNumber.split(";")[0];
            lblNamaEvent.innerHTML = trim(selectedRefNumber.split(";")[3]);
            __doPostBack("txtKodeEvent", "");

        }

        function trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }

        function ShowPopUpDealer() {
            showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 430, 800, DealerSSS);
        }

        function DealerSSS(selectedRefNumber) {
            var txtKodeDealer = document.getElementById("txtKodeDealer");
            txtKodeDealer.value = selectedRefNumber.split(";")[0];

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">EVENT NASIONAL -&nbsp; DAFTAR EVENT NASIONAL</td>
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
                                    <tr id="trKodeDealer" runat="server">
                                        <td class="titleField" width="20%" valign="top">
                                            <asp:Label ID="lblDealerSearch" runat="server">Kode Dealer</asp:Label></td>
                                        <td width="1%">:</td>
                                        <td width="34%">
                                            <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodeDealer" runat="server" MaxLength="10"></asp:TextBox>
                                            <asp:HiddenField ID="hdnDealer" runat="server" />
                                            <asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
                                            <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                            </asp:Label>
                                            <asp:Label ID="lblKodeDealer" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" valign="top">Kode Event</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodeEvent" runat="server"
                                                MaxLength="10" OnTextChanged="txtKodeEvent_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:HiddenField ID="hdnTempEvent" runat="server" />
                                            <asp:Label ID="lblPopUpTO" runat="server" Width="16px">
                                            <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td class="titleField" style="height: 18px">Nama Event</td>
                                        <td style="height: 18px">:</td>
                                        <td style="height: 18px">
                                            <asp:Label ID="lblNamaEvent" runat="server" Text="[Auto Generate]"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="trKota" runat="server">
                                        <td class="titleField">
                                            <asp:Label ID="lblKota" runat="server">Kota/Kab</asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlCity" runat="server" Width="140px" AutoPostBack="true"></asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%" valign="top">
                                <table id="Table4" cellspacing="1" cellpadding="2" width="70%" border="0">
                                    <tr id="trVenue" runat="server">
                                        <td class="titleField">
                                            <asp:Label ID="lblVenue" runat="server">Venue</asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlVenue" runat="server" Width="140px"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="trPeriode" runat="server">
                                        <td class="titleField" style="height: 18px">Periode</td>
                                        <td>:</td>
                                        <td>
                                            <table cellspacing="0" cellpadding="0" border="0" width="80%">
                                                <tr valign="top">
                                                    <td>
                                                        <asp:CheckBox ID="cbDate" runat="server" Width="2px"></asp:CheckBox>
                                                    </td>
                                                    <td>
                                                        <cc1:IntiCalendar ID="icPeriodeStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                                    <td valign="bottom">&nbsp;&nbsp;s.d&nbsp;&nbsp;</td>
                                                    <td>
                                                        <cc1:IntiCalendar ID="icPeriodeEnd" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trStatus" runat="server">
                                        <td class="titleField">Status Registrasi</td>
                                        <td>:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <hr />
                    <asp:Button ID="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:Button>
                    <asp:Button ID="btnDownloadExcel" runat="server" Text=" Download Excel " Style="margin-left: 15px" Visible="false"></asp:Button>
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>
        <br />
        <div id="div1" style="overflow: auto; height: 300px">
            <asp:DataGrid ID="dgNationalEvent" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" GridLines="Horizontal"
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
                    <%--<asp:TemplateColumn HeaderText="Status" SortExpression="Status">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>--%>
                    <asp:TemplateColumn HeaderText="Kode Event" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblKodeEvent" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Event" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle ForeColor="White" Width="15%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNamaEvent" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kota" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblCityName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Venue" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle ForeColor="White" Width="15%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblVenueName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Periode Event Mulai">
                        <HeaderStyle ForeColor="White" Width="6%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPeriodStart" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Periode Event Selesai">
                        <HeaderStyle ForeColor="White" Width="6%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPeriodEnd" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Target Prospek">
                        <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTargetProspek" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Target SPK">
                        <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTargetSPK" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Status Registrasi">
                        <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
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
                            <asp:LinkButton ID="lnkbtnMapping" runat="server" CommandName="Mapping" CausesValidation="False">
												            <img src="../images/assigntocustomer.png" border="0" alt="Mapping"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>
    </form>
</body>
</html>
