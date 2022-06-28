<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmListServiceBooking.aspx.vb" Inherits=".FrmListServiceBooking" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FrmListServiceBooking</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/scheduler_traditional.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/FormFunctions.js"></script>

    <script type="text/javascript">
        function ShowCancelServiceBooking(id, mode) {
            //alert(id + ' ' + mode);
            showPopUp('../PopUp/PopUpCancelServiceBooking.aspx?id=' + id + '&mode=' + mode , '', 600, 600, AfterSave);
        }

        function AfterSave(msg) {
            alert(msg);
            var btn = document.getElementById("btnCari")
            btn.click();
        }

        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var temp = selectedDealer.split(';');
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            var txtNamaDealerSelection = document.getElementById("txtNamaDealer");
            var hdnterm1 = document.getElementById("HiddenField1");
            var hdnterm2 = document.getElementById("HiddenField2");

            txtDealerSelection.value = temp[0];
            txtNamaDealerSelection.value = temp[1];
            hdnterm1.value = temp[0];
            hdnterm2.value = temp[1];

        }
    </script>
    <style type="text/css">
        select {
            width: 150px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="Label1" runat="server" Text="Stall - Daftar Service Booking"></asp:Label></td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1" colspan="2">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10" colspan="2">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
        </table>
        <table id="Table2" cellspacing="3" cellpadding="3" width="100%" border="0">
             <tr>
                <td class="titleField" style="width: 200px;">Kode Dealer</td>
                <td style="width: 5px;">:</td>
                <td>
                    <asp:TextBox ID="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$;')"
                        runat="server" ReadOnly="true"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>
            </tr>
            <tr>
                <td class="titleField">Nama Dealer</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtNamaDealer" runat="server" Width="160px" ReadOnly="true"></asp:TextBox></td>
            </tr>
            
            <tr>
                <td class="titleField" width="24%">Nomor Plat Mobil</td>
                <td width="1%">:</td>
                <td width="75%">
                    <asp:TextBox ID="txtPlatNomor" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="24%">Nama Konsumen</td>
                <td width="1%">:</td>
                <td width="75%">
                    <asp:TextBox ID="txtNamaKonsumen" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="24%">Nomor Telp. Konsumen</td>
                <td width="1%">:</td>
                <td width="75%">
                    <asp:TextBox ID="txtNoTelp" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="24%">Rencana Kedatangan</td>
                <td width="1%">:</td>
                <td width="75%">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:CheckBox ID="cbKedatangan" runat="server" />
                            </td>
                            <td>
                                <cc1:IntiCalendar ID="ICKedatangan" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                            <td>&nbsp;s.d&nbsp;</td>
                            <td>
                                <cc1:IntiCalendar ID="ICKedatanganTo" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="24%">Rencana Pengerjaan</td>
                <td width="1%">:</td>
                <td width="75%">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:CheckBox ID="cbPengerjaan" runat="server" />
                            </td>
                            <td>
                                <cc1:IntiCalendar ID="ICPengerjaan" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                            <td>&nbsp;s.d&nbsp;</td>
                            <td>
                                <cc1:IntiCalendar ID="ICPengerjaanTo" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:Label ID="Label4" runat="server">Status</asp:Label></td>
                <td>:</td>
                <td>
                    <%--<div style="display: none;">--%>
                    <asp:DropDownList runat="server" ID="ddlStatus" Width="120">
                    </asp:DropDownList>
                    <%--</div>--%>
                </td>

            </tr>
            <tr>
                <td class="titleField" width="24%"></td>
                <td width="1%"></td>
                <td width="75%">
                    <asp:Button ID="btnCari" runat="server" Text="Cari" OnClick="btnCari_Click" Height="22px" Width="87px" />
                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click" Height="22px" Width="87px" Visible="false" />
                </td>
            </tr>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <asp:HiddenField ID="HiddenField2" runat="server" />
        </table>

        <table id="Table3" cellspacing="3" cellpadding="3" width="100%" border="0">
            <tr>
                <td>
                    <asp:DataGrid ID="dtgServiceBooking" runat="server" Width="100%" BackColor="#E0E0E0" AutoGenerateColumns="False"
                        BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" CellPadding="3" ShowFooter="false" AllowSorting="True" AllowCustomPaging="True" PageSize="25"
                        AllowPaging="True">
                        <SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
                        <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle VerticalAlign="Top"></HeaderStyle>
                        <FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbNo" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <%--<asp:TemplateColumn HeaderText="No Reservasi" Visible="false">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbNoReservasi" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceBookingCode")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Model Kendaraan" Visible="false">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbModelKendaraan" runat="server" Text='<%# String.Format("{0} - {1}", DataBinder.Eval(Container, "DataItem.VechileModel.VechileModelCode"), DataBinder.Eval(Container, "DataItem.VechileModel.IndDescription"))%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Tipe Kendaraan" Visible="false">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbTipeKendaraan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Description")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Model Kendaraan" Visible="false">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbVechileModelID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileModel.ID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="No Rangka" Visible="false">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbNoRangka" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>--%>
                            <asp:TemplateColumn HeaderText="No Plat Mobil">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbNoPlat" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PlateNumber")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Nama Konsumen">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbNamaKonsumen" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CustomerName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <%--<asp:TemplateColumn HeaderText="Odometer" Visible="false">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="Odometer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Odometer")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>--%>
                            <asp:TemplateColumn HeaderText="No Telp Konsumen">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbNoTelpKonsumen" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CustomerPhoneNumber")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <%--<asp:TemplateColumn HeaderText="Stall" Visible="false">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbStall" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StallMaster.StallName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Catatan" Visible="false">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbNotes" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Notes")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Tipe Konsumen" Visible="false">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbPickUpType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PickUpType")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>--%>
                            <asp:TemplateColumn HeaderText="Rencana Kedatangan">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbRencanaKedatangan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IncomingDateStart", "{0:yyyy-MM-dd HH:mm}")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <%--<asp:TemplateColumn HeaderText="Rencana Pengambilan" Visible="false">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbRencanaPengambilan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IncomingDateEnd", "{0:yyyy-MM-dd HH:mm}")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>--%>
                            <asp:TemplateColumn HeaderText="Rencana Pengerjaan">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbRencanaPengerjaan" runat="server" Text='<%# String.Format("{0} - {1}", DataBinder.Eval(Container, "DataItem.WorkingTimeStart", "{0:yyyy-MM-dd HH:mm}"), DataBinder.Eval(Container, "DataItem.WorkingTimeEnd", "{0: HH:mm}"))%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Status">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StatusStr")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPreview" runat="server" CommandName="lnkPreview">
										<img src="../images/detail.gif" border="0" style="cursor:hand" alt="Preview Service Booking">
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="lnkEdit">
										<img src="../images/edit.gif" border="0" style="cursor:hand" alt="Edit Service Booking">
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkDetail" runat="server" CommandName="lnkDetail" OnClientClick="return confirm('Apakah anda yakin ingin membatalkan transaksi ini?');">
										<img src="../images/in-aktif.gif" border="0" style="cursor:hand" alt="Batalkan Service Booking">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td>
                     <asp:Button ID="btnHidden" runat="server" Text="" onClick="btnHidden_Click" ClientIDMode="Static" Style="display:none"></asp:Button>
                    <asp:Button ID="btnDownload" runat="server" Text=" Download " Width="80px" CausesValidation="False" OnClick="btnDownload_Click"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
