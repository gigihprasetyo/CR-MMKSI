<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmCcCSTeamList.aspx.vb" Inherits=".FrmCcCSTeamList" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmSalesmanList</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript">
        function ShowPPDealerSelection() {
            showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx', '', 500, 600, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealer = document.getElementById("txtDealerCode");
            txtDealer.value = selectedDealer;
        }

        /* ini untuk handle char yg tdk diperbolehkan, saat paste */
        function TxtBlur(objTxt) {
            omitSomeCharacter(objTxt, '<>?*%$;');
        }
        /* ini untuk handle char yg tdk diperbolehkan, saat keypress */
        function TxtKeypress() {
            return alphaNumericExcept(event, '<>?*%$;')
        }

        function ShowSalesmanSelection() {
            var lblSalesmanCode = document.getElementById("lblShowSalesman");
            showPopUp('../PopUp/PopUpCsTeam.aspx?IsGroupDealer=1&IsSales=0&IsResign=0', '', 470, 600, SalesmanSelection);
        }

        function ShowChangesCSResigne(shID) {
            showPopUp('../PopUp/PopUpChangesResigneDate.aspx?shID=' + shID, '', 470, 600, null);
        }

        function SalesmanSelection(SelectedSalesman) {
            var tempParam = SelectedSalesman.split(';');
            var txtSalesmanCode = document.getElementById("txtID");
            var txtNama = document.getElementById("txtNama");
            txtSalesmanCode.value = tempParam[0]
            txtNama.value = tempParam[1];
        }

        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Apakah Anda Yakin Menghapus Data Ini?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="lblPageTitle" runat="server"></asp:Label></td>
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
                <td align="left">
                    <table id="Table2" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="20%">Kode Dealer</td>
                            <td style="height: 17px" width="1%">:</td>
                            <td width="79%">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
                                    runat="server" Width="191px"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img style="cursor:hand" alt="Klik Disini untuk memilih Nomor Permintaan" src="../images/popup.gif"
											border="0"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblSalesmanID" runat="server" Width="220px"></asp:Label></td>
                            <td style="height: 28px" width="1%">:</td>
                            <td>
                                <asp:TextBox onkeypress="TxtKeypress();" ID="txtID" onblur="TxtBlur('txtID');" runat="server"
                                    MaxLength="15"></asp:TextBox>
                                <asp:Label ID="lblShowSalesman" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Nama</td>
                            <td style="height: 23px" width="1%">:</td>
                            <td>
                                <asp:TextBox onkeypress="TxtKeypress();" ID="txtNama" onblur="TxtBlur('txtNama');" runat="server"
                                    MaxLength="15"></asp:TextBox>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="titleField">Status</td>
                            <td style="height: 28px" width="1%">:</td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList></td>
                        </tr>

                        <tr>
                            <td class="titleField">Posisi</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:DropDownList ID="ddlPosisi" TabIndex="12" runat="server" AutoPostBack="True"></asp:DropDownList></td>
                        </tr>
                          <tr id="trTglMasuk" runat="server" visible="false">
                            <td class="titleField">Tanggal Masuk</td>
                            <td width="1%">:</td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="ICTanggalMasukFrom" runat="server" Value=""></cc1:IntiCalendar></td>
                                        <td>s/d</td>
                                        <td>
                                            <cc1:IntiCalendar ID="ICTanggalMasukTo" runat="server" Value=""></cc1:IntiCalendar></td>
                                    </tr>
                                </table></td>
                        </tr>
                        <tr id="trTglKeluar" runat="server" visible="false">
                            <td class="titleField">Tanggal Keluar</td>
                            <td width="1%">:</td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="ICTanggalKeluarFrom" runat="server" Value=""></cc1:IntiCalendar></td>
                                        <td>s/d</td>
                                        <td>
                                            <cc1:IntiCalendar ID="ICTanggalKeluarTo" runat="server" Value=""></cc1:IntiCalendar></td>
                                    </tr>
                                </table></td>
                        </tr>


                        <tr>
                            <td class="titleField"></td>
                            <td width="1%"></td>
                            <td>
                                <asp:Button ID="btnCari" runat="server" Width="60px" Text="Cari"></asp:Button><asp:Button ID="btnCancel" runat="server" Width="60px" Text="Batal"></asp:Button>
                                <asp:Button ID="btnDownloadExcel" Text="Download Excel" runat="server"></asp:Button></td>
                        </tr>

                        <tr>
                            <td class="titleField">&nbsp;</td>
                            <td width="1%">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="titleField" colspan="3">
                                <div id="div1" style="height: 240px; overflow: auto">
                                    <asp:DataGrid ID="dgSalesmanHeader" runat="server" Width="100%" CellPadding="3" BackColor="#CDCDCD"
                                        PageSize="25" AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False" CellSpacing="1"
                                        AllowSorting="True" DESIGNTIMEDRAGDROP="57">
                                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                                <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodeDealer" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="SalesmanCode" SortExpression="SalesmanCode" HeaderText="Kode">
                                                <HeaderStyle Width="18%" CssClass="titleTableParts" Wrap="False"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama">
                                                <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Posisi">
                                                <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPosisi" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <%--<asp:TemplateColumn HeaderText="Level">
													<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblLevel" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>--%>
                                            <asp:BoundColumn DataField="HireDate" SortExpression="HireDate" HeaderText="Tanggal Masuk" DataFormatString="{0:dd/MM/yyyy}">
                                                <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn SortExpression="DateOfBirth" HeaderText="Tanggal Lahir">
                                                <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblResignDate" Text='<%# Format(DataBinder.Eval(Container, "DataItem.DateOfBirth"),"dd/MM/yyyy") %>' runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                                                <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
                                                        CommandName="View">
															<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnKonfirmasi" runat="server" Width="20px" Text="Konfirmasi" CausesValidation="False"
                                                        CommandName="Konfirmasi">
															<img src="../images/04icon_rsd_02.gif" border="0" alt="Konfirmasi"></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnGenerateCode" runat="server" CausesValidation="False" CommandName="GenerateCode">
											                <img src="../images/reload.gif" border="0" alt="Generate Code">
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                                    <asp:Label ID="lbtnresignedate" runat="server" Width="20px" Text="Ubah Data Pengunduran diri" CausesValidation="False">
															<img src="../images/reset_password.gif" border="0" alt="Ubah Data Pengunduran diri"></asp:Label>
                                                    <asp:LinkButton ID="lbtnResign" runat="server" Width="20px" Text="Resign" CausesValidation="False" CommandName="Resign" Visible="false">
															<img src="../images/Resign.png" border="0" alt="Resign"></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnAsigne" runat="server" Width="20px" Text="AsigntoDealer" CausesValidation="False" CommandName="asignetodealer">
															<img src="../images/assigntodealer.png" border="0" alt="Asigne to Dealer"></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False"
                                                        CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>

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
        </table>
    </form>
   
</body>
</html>
