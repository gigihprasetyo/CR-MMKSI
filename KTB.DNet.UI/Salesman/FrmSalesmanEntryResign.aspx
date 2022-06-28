<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmSalesmanEntryResign.aspx.vb" Inherits="FrmSalesmanEntryResign" SmartNavigation="False" %>

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
        /* Deddy H	validasi value *********************************** */
        /* ini untuk handle char yg tdk diperbolehkan, saat paste */
        function TxtBlur(objTxt) {
            omitSomeCharacter(objTxt, '<>?*%$;');
        }
        /* ini untuk handle char yg tdk diperbolehkan, saat keypress */
        function TxtKeypress() {
            return alphaNumericExcept(event, '<>?*%$;')
        }

        function ShowPopUpSubordinate(id) {
            showPopUp('../PopUp/PopUpSalesmanSubOrdinate.aspx?id=' + id, '', 600, 600, null);
        }

        function ShowPopUpSalesmanbyDealer(dealerSalesman) {
            showPopUp('../PopUp/PopUpSalesman.aspx?IsPosition=0&DealerSalesman=' + dealerSalesman, '', 600, 600, SalesmanSelection);
        }
        function SalesmanSelection(result) {

            var tempParam = result.split(';');
            var txtSuperior = document.getElementById("txtSalesmanCode");
            var txtSuperiorName = document.getElementById("lblName");
            var hdnSalesmanCode = document.getElementById("hdnSalesmanCode");
            //alert(txtSuperior);

            if (navigator.appName == "Microsoft Internet Explorer") {
                txtSuperior.innerText = tempParam[0];
                txtSuperiorName.innerText = tempParam[1];
            }
            else {
                txtSuperior.value = tempParam[0];
                txtSuperiorName.value = tempParam[1];
            }
            hdnSalesmanCode.value = tempParam[0];
            var clickButton = document.getElementById("btnTriggerSalesman");
            clickButton.click();
        }

    </script>
    <style type="text/css">
        .hidden {
            display: none;
        }
        .auto-style1 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            height: 25px;
        }
        .auto-style2 {
            width: 14px;
            height: 25px;
        }
        .auto-style3 {
            height: 25px;
        }
        .auto-style4 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            height: 9px;
        }
        .auto-style5 {
            width: 14px;
            height: 9px;
        }
        .auto-style6 {
            height: 9px;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px">
                    <asp:Label ID="lblPageTitle" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="height: 5px" width="20%">
                                <asp:Label ID="lblText" runat="server" Visible="False">Kode Dealer</asp:Label></td>
                            <td style="width: 14px; height: 5px" width="14">
                                <asp:Label ID="lblsemicolon" runat="server" Width="3px" Visible="False">:</asp:Label></td>
                            <td class="titleField" style="height: 5px" width="20%">
                                <asp:Label ID="lblKodeDealer" runat="server" Width="152px" Visible="False"></asp:Label></td>
                            <td class="titleField" style="height: 5px" width="20%"></td>
                            <td style="height: 5px" width="1%"></td>
                            <td style="height: 5px" width="29%"></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="20%" style="height: 5px">Kode</td>
                            <td style="width: 14px; height: 5px" width="14">
                                <asp:Label ID="lblColon3" runat="server">:</asp:Label></td>
                            <td class="titleField" style="height: 5px" width="20%">
                                <asp:DropDownList ID="ddlSalesmanCode" runat="server" Visible="false" AutoPostBack="True" Width="152px"></asp:DropDownList>
                                <asp:TextBox ID="txtSalesmanCode" Width="150px" runat="server"></asp:TextBox>
                                <asp:Label ID="lblPopUpSalesman" runat="server" Width="10"> 
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>
                                <asp:HiddenField ID="hdnSalesmanCode" runat="server" />
                                <asp:Button ID="btnTriggerSalesman" runat="server" CssClass="hidden" CausesValidation="false" />
                            </td>
                            <td class="titleField" style="height: 5px" width="20%"></td>
                            <td width="1%" style="height: 5px"></td>
                            <td style="height: 5px" width="29%"></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="20%">Nama</td>
                            <td width="14" style="width: 14px">
                                <asp:Label ID="Label1" runat="server">:</asp:Label></td>
                            <td class="titleField" width="20%">
                                <asp:Label ID="lblName" runat="server"></asp:Label></td>
                            <td class="titleField" width="20%"></td>
                            <td width="1%"></td>
                            <td width="40%"></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="20%">Posisi</td>
                            <td width="14" style="width: 14px">
                                <asp:Label ID="Label2" runat="server">:</asp:Label></td>
                            <td class="titleField" width="20%">
                                <asp:Label ID="lblPosition" runat="server"></asp:Label></td>
                            <td class="titleField" width="20%"></td>
                            <td width="1%"></td>
                            <td width="29%"></td>
                        </tr>
                        <tr id="trSubOrdinate" runat="server" visible="false">
                            <td class="titleField" width="20%">Daftar Sub Ordinate</td>
                            <td width="14" style="width: 14px">
                                <asp:Label ID="Label3" runat="server">:</asp:Label></td>
                            <td class="titleField" width="20%">
                                <asp:Label ID="lblPopupsubordinate" runat="server" Width="10"> 
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>
                            </td>
                            <td class="titleField" width="20%"></td>
                            <td width="1%"></td>
                            <td width="29%"></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="20%">Tgl Keluar</td>
                            <td style="width: 14px; height: 20px" width="14">
                                <asp:Label ID="Label5" runat="server">:</asp:Label></td>
                            <td style="width: 80px; height: 20px" nowrap width="80">
                                <table border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkResign" runat="server" AutoPostBack="True"></asp:CheckBox></td>
                                        <td>
                                            <cc1:IntiCalendar ID="icResignDate" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                            <td class="titleField" style="height: 20px" width="20%"></td>
                            <td width="1%"></td>
                            <td style="height: 20px" width="29%"></td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Alasan</td>
                            <td class="auto-style2">
                                <asp:Label ID="Label6" runat="server">:</asp:Label></td>
                            <td class="auto-style1">
                                <asp:DropDownList ID="ddlResignReason" runat="server" AutoPostBack="True" Height="20px" Width="258px">
                                </asp:DropDownList>
                            </td>
                            <td class="auto-style1"></td>
                            <td width="1%" class="auto-style3"></td>
                            <td class="auto-style3"></td>
                        </tr>
                        <tr>
                            <td class="auto-style4"></td>
                            <td class="auto-style5">
                                </td>
                            <td class="auto-style4">
                                <asp:TextBox onkeypress="TxtKeypress();" ID="txtResignReason" onblur="TxtBlur('txtResignReason');"
                                    runat="server" Width="208px" MaxLength="700" size="22"></asp:TextBox></td>
                            <td class="auto-style4"></td>
                            <td width="1%" class="auto-style6"></td>
                            <td class="auto-style6"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 141px; height: 24px"></td>
                            <td style="width: 14px; height: 24px"></td>
                            <td class="titleField" style="height: 24px">
                                <asp:Button ID="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:Button><asp:Button ID="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:Button>
                                <asp:Button ID="btnBack" runat="server" Text="Kembali"></asp:Button>
                                <asp:HiddenField ID="hdnConfirm" runat="server" />
                            </td>
                            <td class="titleField" style="height: 24px"></td>
                            <td style="height: 24px"></td>
                        </tr>
                        <tr>
                            <td class="titleField" colspan="6">
                                <div id="div1" style="overflow: auto; height: 250px">
                                    <asp:DataGrid ID="dgSalesmanHeader" runat="server" Width="100%" CellPadding="3" BorderWidth="0px"
                                        CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True" PageSize="25" AllowPaging="True">
                                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="SalesmanCode" SortExpression="SalesmanCode" HeaderText="Kode">
                                                <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama">
                                                <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Posisi">
                                                <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPosisi" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="ResignDate" HeaderText="Tgl Keluar">
                                                <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblResignDate" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="ResignReason" HeaderText="Alasan">
                                                <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
                                                        CommandName="View">
															<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False"
                                                        Visible="False" CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Batal Status"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 141px; height: 24px" colspan="6"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 8px"></td>
            </tr>
        </table>
    </form>
</body>
</html>
