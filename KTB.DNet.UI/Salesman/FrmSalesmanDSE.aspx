<%@ Page Language="vb" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeBehind="FrmSalesmanDSE.aspx.vb" Inherits=".FrmSalesmanDSE" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Salesman DSE</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/jquery-1.8.3.min.js"> </script>

    <script type="text/javascript">
        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer.split(";");
            var txtDealerCodeSelection = document.getElementById("txtDealerCode");
            var lblDealerName = document.getElementById("lblDealerName");

            if (navigator.appName == "Microsoft Internet Explorer") {
                txtDealerCodeSelection.innerText = tempParam[0];
            }
            else {
                txtDealerCodeSelection.value = tempParam[0];
            }
            var clickButton = document.getElementById("btnTriggerDealer");
            clickButton.click();
        }

        function ShowPopUpSalesmanbyDealer(dealerSalesman) {
            showPopUp('../PopUp/PopUpSalesman.aspx?IsPosition=0&DealerSalesman=' + dealerSalesman, '', 600, 600, SalesmanSelection);
        }
        function SalesmanSelection(result) {

            var tempParam = result.split(';');
            var txtSuperior = document.getElementById("txtSalesmanCode");
            var txtSuperiorName = document.getElementById("lblNama");
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



        function CheckAll(aspCheckBoxID, checkVal) {
            re = new RegExp(':' + aspCheckBoxID + '$')
            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox') {
                    if (re.test(elm.name)) {
                        elm.checked = checkVal
                    }
                }
            }
        }

    </script>
    <style type="text/css">
        .hidden {
            display: none;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <asp:HiddenField ID="isValidAdd" runat="server" />
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="lblPageTitle" runat="server"></asp:Label>
                    <asp:HiddenField ID="hdnCategory" runat="server" />
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
                    <table>
                        <tr>
                            <td width="150" class="titleField" height="1">Kode Dealer</td>
                            <td height="1" width="1%">:</td>
                            <td width="80%" nowrap="nowrap">
                                <asp:TextBox ID="txtDealerCode" Width="150px" runat="server"></asp:TextBox>
                                <asp:Label ID="lblPopUpDealer" runat="server" Width="10"> 
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>
                                <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                                <asp:Button ID="btnTriggerDealer" runat="server" CssClass="hidden" CausesValidation="false" />
                            </td>
                        </tr>
                        <tr id="trDealername" runat="server">
                            <td width="150" class="titleField" height="1">&nbsp;</td>
                            <td height="1" width="1%">&nbsp;</td>
                            <td width="80%" nowrap="nowrap">
                                <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="150" class="titleField" height="1">Sisa</td>
                            <td height="1" width="1%">:</td>
                            <td width="80%" nowrap="nowrap">
                                <asp:Label ID="lblSisa" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="150" class="titleField" height="1">Kode Salesman</td>
                            <td height="1" width="1%">:</td>
                            <td width="80%" nowrap="nowrap">
                                <asp:TextBox ID="txtSalesmanCode" Width="150px" runat="server"></asp:TextBox>
                                <asp:Label ID="lblPopUpSalesman" runat="server" Width="10"> 
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>
                                <asp:HiddenField ID="hdnSalesmanCode" runat="server" />
                                <asp:Button ID="btnTriggerSalesman" runat="server" CssClass="hidden" CausesValidation="false" />
                            </td>
                        </tr>
                        <tr>
                            <td width="150" class="titleField" height="1">Nama</td>
                            <td height="1" width="1%">:</td>
                            <td width="80%" nowrap="nowrap">
                                <asp:Label ID="lblNama" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="150" class="titleField" height="1">Posisi</td>
                            <td height="1" width="1%">:</td>
                            <td width="80%" nowrap="nowrap">
                                <asp:Label ID="lblPosisi" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="150" class="titleField" height="1">Grade</td>
                            <td height="1" width="1%">:</td>
                            <td width="80%" nowrap="nowrap">
                                <asp:Label ID="lblGrade" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="150" class="titleField" height="1">No Handphone</td>
                            <td height="1" width="1%">:</td>
                            <td width="80%" nowrap="nowrap">
                                <asp:TextBox ID="txtNoPhone" Width="150px"  onkeypress="return numericOnlyUniv(event)" runat="server"></asp:TextBox>
                                <%--<asp:Label ID="lblNoPhone" runat="server"></asp:Label>--%>
                            </td>
                        </tr>
                        <tr>
                            <td width="150" class="titleField" height="1">Urutan</td>
                            <td height="1" width="1%">:</td>
                            <td width="80%" nowrap="nowrap">
                                <asp:TextBox ID="txtUrutan" Width="150px" Enabled="false" runat="server"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td width="150" class="titleField" height="1"></td>
                            <td height="1" width="1%">:</td>
                            <td width="80%" nowrap="nowrap">
                                <asp:Button ID="btnSimpan" runat="server" Width="60px" Text="Simpan" TabIndex="15"></asp:Button>
                                &nbsp;
                                <asp:Button ID="btnBatal" runat="server" Width="60px" CausesValidation="false" Text="Batal" TabIndex="15"></asp:Button>
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
                    <script type="text/javascript">
                        window.onload = function () {
                            var div = document.getElementById('<%=dvBooking.ClientID%>');
                            var div_position = document.getElementById('<%= div_position.ClientID%>');
                            var position = parseInt('<%=Request.Form("div_position") %>');
                            if (isNaN(position)) {
                                position = 0;
                            }
                            div.scrollTop = position;
                            div.onscroll = function () {
                                div_position.value = div.scrollTop;
                            };
                        };

                    </script>
                    <input type="hidden" id="div_position" runat="server" />
                    <div id="dvBooking" runat="server" style="overflow: auto; height: 300px">
                        <asp:DataGrid ID="dtgSalesmanDSE" runat="server" Width="100%" AutoGenerateColumns="False"
                            GridLines="Horizontal" CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD"
                            AllowCustomPaging="True" AllowSorting="True" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn SortExpression="">
                                    <HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lbtnUp" runat="server" CausesValidation="False" Text="Up" CommandName="upList">
												<img src="../images/icon_up.png" border="0" alt="Up"></asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lbtnDown" runat="server" CausesValidation="False" Text="Down" CommandName="downList">
												<img src="../images/icon_down.png" border="0" alt="Down"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Urutan Prioritas" SortExpression="">
                                    <HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Kode">
                                    <HeaderStyle Width="7%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSalesmanCode" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Nama" SortExpression="Name">
                                    <HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNama" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Nomer Kontak" SortExpression="StartWorkingDate">
                                    <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoPhone" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Posisi">
                                    <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPosisi" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Grade">
                                    <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrade" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Productivity">
                                    <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblScore" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Status" SortExpression="">
                                    <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                        <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                                        <asp:LinkButton ID="lbtnAktif" runat="server" Text="" CausesValidation="False" CommandName="aktif">
									                    <img src="../images/aktif.gif" border="0" alt="Aktifkan"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnInAktif" runat="server" Text="" CausesValidation="False" CommandName="inaktif">
									                    <img src="../images/in-aktif.gif" border="0" alt="Non Aktifkan"></asp:LinkButton>
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
            <%-- <tr>
                <td align="center">
                    <asp:Button ID="btnSimpan" runat="server" Width="60px" Text="Simpan" TabIndex="15"></asp:Button>
                    &nbsp;
                    <asp:Button ID="btnValidasi" runat="server" Width="100px" CausesValidation="false" Text="Validasi" TabIndex="15"></asp:Button> &nbsp;
                    <asp:Button ID="btnBatal" runat="server" Width="100px" CausesValidation="false" Text="Batal Validasi" TabIndex="15"></asp:Button> &nbsp;
                    <asp:Button ID="btnKembali" runat="server" Text="Kembali" CausesValidation="False" TabIndex="14"></asp:Button>

                </td>
            </tr>--%>
        </table>
    </form>

</body>
</html>


