<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmEventParticipantProcessList.aspx.vb" Inherits="FrmEventParticipantProcessList" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmAplikasiHeader</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPDealerSelection() {
            //showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
            showPopUp('../General/../Benefit/PopUpDealerSelectionBenefit.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }

        function ShowPPBenefitRegNoSelection() {
            var selectbox = getElement("input", "txtKodeDealer")
            showPopUp('../General/../Benefit/PopUpBenefitDealer.aspx?dealer=' + selectbox.value + "&event=1", '', 500, 760, BenefitRegNoSelection);
        }


        function BenefitRegNoSelection(selectedBenefitRegNo) {
            var txtBenefitRegNoSelection = document.getElementById("txtBenefitRegNo");
            txtBenefitRegNoSelection.value = selectedBenefitRegNo;
        }

        function getElement(tipeElement, IdElement) {
            var selectbox;
            var inputs = document.getElementsByTagName(tipeElement);

            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].id.indexOf(IdElement) > -1) {
                    selectbox = inputs[i]
                    break;
                }
            }
            return selectbox;
        }
        function CheckAll(aspCheckBoxID) {
            var selectbox = getElement('input', 'chkAllItems')
            var inputs = document.getElementsByTagName("input");
            var stringlist = ""
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].id.indexOf(aspCheckBoxID) > -1) {
                    if (inputs[i].type == 'checkbox') {
                        if (selectbox.checked == true) {
                            inputs[i].checked = "checked"

                        }

                        else
                            inputs[i].checked = ""
                    }
                }
            }

            var table = document.getElementById('dgTable');
            var exitsno = '';
            for (i = 1; i < table.rows.length - 1; i++) {

                //stringlist = stringlist + ";" + table.rows[i].cells[0].getElementsByTagName("input")[0].checked;
                if (table.rows[i].cells[0].getElementsByTagName("input")[0].checked == true)
                    stringlist = stringlist + ";" + i

            }

            var arrayCheck = getElement('input', 'arrayCheck')
            if (selectbox.checked == true) {
                arrayCheck.value = stringlist
            } else arrayCheck.value = ""
        }

        function generateCheckBoxClick() {
            var inputs = document.getElementsByTagName("input");
            var stringlist = ""
            for (var i = 0; i < inputs.length; i++) {

                if (inputs[i].id.indexOf('cbAllGrid') > -1) {
                    if (inputs[i].type == 'checkbox') {

                        inputs[i].onclick = function () { setValueCheckBox(); };
                    }
                }
            }
        }

        function setValueCheckBox() {
            var table = document.getElementById('dgTable');
            var stringlist = '';
            for (i = 1; i < table.rows.length - 1; i++) {

                if (table.rows[i].cells[0].getElementsByTagName("input")[0].checked == true)
                    stringlist = stringlist + ";" + i

            }
            var arrayCheck = getElement('input', 'arrayCheck')

            arrayCheck.value = stringlist

        }

        setTimeout(function () { generateCheckBoxClick(); }, 2000);


    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="5" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px" colspan="2">SALES CAMPAIGN - Daftar Peserta Event </td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" colspan="2" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" colspan="2" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td class="titleField" width="20%">Kode Dealer&nbsp;</td>
                <td>
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodeDealer" onblur="omitSomeCharacter('txtKeyWords','<>?*%$;')"
                        runat="server" Width="242px"></asp:TextBox>
                    &nbsp;<asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
							        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                    <asp:Label ID="lblDelerSession" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="20%">Benefit Reg No&nbsp;</td>
                <td>
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtBenefitRegNo" onblur="omitSomeCharacter('txtKeyWords','<>?*%$;')"
                        runat="server" Width="242px"></asp:TextBox>
                    &nbsp;<asp:Label ID="lblPopUpBenefitRegNo" runat="server" Width="16px">
							        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                    <asp:Label ID="lblBenefitRegNo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="20%">No Reg Event&nbsp;</td>
                <td>
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtRegEventNo" onblur="omitSomeCharacter('txtRegEventNo','<>?*%$;')"
                        runat="server" Width="242px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="20%">Nama Event&nbsp;</td>
                <td>
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtEventName" onblur="omitSomeCharacter('txtEventName','<>?*%$;')"
                        runat="server" Width="242px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="20%">Tanggal Event&nbsp;</td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:CheckBox ID="cbDate" runat="server" Width="2px"></asp:CheckBox>
                            </td>
                            <td>
                                <cc1:IntiCalendar ID="icEventDate" runat="server" TextBoxWidth="60"></cc1:IntiCalendar></td>
                            <td>
                                <asp:Label runat="server">&nbsp;s.d&nbsp;</asp:Label></td>
                            <td>
                                <cc1:IntiCalendar ID="icEventDateTo" runat="server" TextBoxWidth="60"></cc1:IntiCalendar></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="20%">Status&nbsp;</td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <%--<asp:ListItem Value="0" Text="Baru"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Validasi"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Batal Validasi"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Disetujui"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Batal Disetujui"></asp:ListItem>
                            <asp:ListItem Value="5" Text="Ditolak"></asp:ListItem>  --%>
                    </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td class="titleField" width="20%">&nbsp;</td>
                <td>
                    <asp:Button ID="btnCari" runat="server" Text="Cari" Width="60px"></asp:Button>&nbsp;
						  <asp:Button ID="btnTambah" runat="server" Text="Tambah" Width="60px" Visible="false"></asp:Button>&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td valign="top" colspan="6">
                                <div id="div1" style="overflow: auto; max-height: 440px">
                                    <asp:DataGrid ID="dgTable" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
                                        PageSize="15" AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD"
                                        BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
                                        CellPadding="3" DataKeyField="ID">
                                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <HeaderTemplate>
                                                    <input id="chkAllItems" onclick="CheckAll('cbAllGrid')"
                                                        type="checkbox">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbAllGrid" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                                <HeaderStyle Width="2%" CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="Dealer.DealerCode">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodeDealer" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Benefit Reg No" SortExpression="BenefitMasterHeader.BenefitRegNo">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBenefitRegNo" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="No Reg Event" SortExpression="EventRegNo">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoRegEvent" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nama Event" SortExpression="EventName">
                                                <HeaderStyle Width="40%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNamaEvent" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Tanggal Event" SortExpression="EventDate">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTanggalEvent" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnView" runat="server" Width="20px" Text="Detail" CausesValidation="False" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
															<img alt="Detail" src="../images/Detail.gif" border="0"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
															<img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
															<img alt="Delete"  src="../images/trash.gif"
																border="0"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="20%">Mengubah Status&nbsp;</td>
                <td>
                    <asp:DropDownList ID="ddlstatus2" runat="server">
                        <%-- <asp:ListItem Value="0" Text="Baru"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Validasi"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Batal Validasi"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Disetujui"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Batal Disetujui"></asp:ListItem>
                            <asp:ListItem Value="5" Text="Ditolak"></asp:ListItem>     --%>
                    </asp:DropDownList>
                    &nbsp;
                        <asp:Button ID="btnStatus" runat="server" Text="Proses" Width="60px"></asp:Button>
                    <asp:Button ID="btnDownload" runat="server" Text="Download" Width="90px"></asp:Button>
                    <asp:HiddenField ID="arrayCheck" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>




</html>
