<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInputEventNational.aspx.vb" Inherits="FrmInputEventNational" SmartNavigation="False" MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmInputEventNational</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPDealerCityMultiSelection() {            
            var txtDealerCityID = document.getElementById("txtDealerCityID")
            var ddlArea1 = document.getElementById("ddlArea1")

            var selectedddlArea1Value = ddlArea1.options[ddlArea1.selectedIndex].value;
            showPopUp('../General/../PopUp/PopUpDealerCitySelectionArea.aspx?Area1ID=' + selectedddlArea1Value + '&DealerCityID=' + txtDealerCityID.value, '', 400, 560, DealerCitySelection);
        }

        function DealerCitySelection(selectedDealerCity) {
            var txtDealerCityID = document.getElementById("txtDealerCityID");
            txtDealerCityID.value = selectedDealerCity;
            __doPostBack("txtDealerCityID", "");
        }

        function ShowPPDealerMultiSelection() {
            showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var data = selectedDealer.split(";");
            var txtDealerCodeSelection = document.getElementById("txtDealerCode");
            txtDealerCodeSelection.value = data[0];
            var lblDealerCodeName = document.getElementById("lblDealerCodeName");
            lblDealerCodeName.innerHTML = data[0] + ' / ' + data[1];
        }

        function toCommas(value) {
            return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
        }
        function trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }

    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <input id="hdnNationalEventID" type="hidden" value="0" runat="server">

        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" colspan="2">EVENT NASIONAL - Input Event Nasional</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1" colspan="2">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10" colspan="2">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td style="width: 50%" valign="top">
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr valign="top">
                            <td valign="top" class="titleField" style="width: 138px">Nomor Registrasi</td>
                            <td style="width: 2px">:</td>
                            <td valign="top">
                                &nbsp;&nbsp;<asp:Label ID="lblRegNumber" Font-Bold="true" runat="server">[Auto Generated]</asp:Label></td>
                        </tr>
                        <tr valign="top">
                            <td valign="top" class="titleField" style="width: 138px">Tipe Event</td>
                            <td style="width: 2px">:</td>
                            <td valign="top">
                                &nbsp;&nbsp;<asp:DropDownList ID="ddlEventType" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr valign="top">
                            <td valign="top" class="titleField" style="width: 138px">Kota / Venue</td>
                            <td style="width: 2px">:</td>
                            <td valign="top">
                                &nbsp;&nbsp;<asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="true"></asp:DropDownList>&nbsp;
                                <asp:DropDownList ID="ddlVenue" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" style="width: 138px">Peserta Dealer</td>
                            <td style="width: 2px">:</td>
                            <td valign="top">
                                <table>
                                    <tr valign="top">
                                        <td>&nbsp;<asp:DropDownList ID="ddlArea1" runat="server" AutoPostBack="true"></asp:DropDownList>&nbsp;</td>
                                        <td><asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtDealerCityName" runat="server" 
                                                TextMode="MultiLine" Rows="5" AutoPostBack="true" ReadOnly="true"
                                                ToolTip="Dealer City Search 1" Width="230px"></asp:TextBox>
                                            <asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
                                                    <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                            </asp:Label>

                                            <asp:TextBox ID="txtDealerCityID" AutoPostBack="true" runat="server" Width="400px" style="display:none" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%" valign="top">
                    <table id="Table3" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td valign="top" class="titleField" style="width: 138px">Periode</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="icPeriodStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                        </td>
                                        <td>&nbsp;s.d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="icPeriodEnd" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="titleField" style="width: 138px">Target Prospek</td>
                            <td style="width: 2px">:</td>
                            <td valign="top">
                                <asp:TextBox ID="txtTargetProspect" Style="text-align: right" runat="server"  Text="0"
                                    onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="60px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 138px">Target SPK</td>
                            <td style="width: 2px">:</td>
                            <td valign="top">
                                <asp:TextBox ID="txtTargetSPK" Style="text-align: right" runat="server" Text="0"
                                    onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="60px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <div>
            <hr />
        </div>
        <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 75%; padding-top: 2px; text-align: Left">
            <asp:Label ID="Label1" runat="server" Font-Size="15px" Text="Dokumen Pelaksanaan Acara :" Font-Bold="True"></asp:Label>
        </div>
        <div style="width: 75%">
            <asp:DataGrid ID="dgUploadFile" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1"
                AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="No.">
                        <HeaderStyle Width="10px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="10px" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Dokumen">
                        <HeaderStyle Width="20%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblKeterangan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FileDescription")%>'>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Left"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtKeterangan" runat="server" Width="400px" />
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Upload File">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnFileName" runat="server" CommandName="Download" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Path")%>'>
                                <asp:Label ID="lblFileName" runat="server" alt="Download"></asp:Label>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Left"></FooterStyle>
                        <FooterTemplate>
                            <input id="UploadFile" onkeydown="return false;" style="width: 367px; height: 20px" type="file" size="25" name="File1" runat="server">
                            <asp:Label ID="lblFileUpload" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Anda yakin mau hapus?');" CausesValidation="False" CommandName="Delete" Text="Hapus" runat="server">
                                    <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnAdd" CommandName="add" Text="Tambah" runat="server">
                                    <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <div>
            <hr />
        </div>
        <asp:Button ID="btnBaru" runat="server" Text="Baru" Style="display: none"></asp:Button>
        <asp:Button ID="btnSave" OnClientClick="return confirm('Apakah anda yakin akan menyimpan Data Event Nasional ini?');" runat="server" Text="Simpan"></asp:Button>&nbsp;&nbsp;
        <asp:Button ID="btnBack" runat="server" Text="Kembali" Visible="false"></asp:Button>
    </form>
</body>
</html>
