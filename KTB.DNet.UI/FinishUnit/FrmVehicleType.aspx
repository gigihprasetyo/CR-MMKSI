<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmVehicleType.aspx.vb" Inherits="FrmVehicleType" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Form Vehicle Type</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">MAINTENANCE - Tipe Kendaraan</td>
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
                    <table id="Table2" width="100%" cellpadding="2" border="0">
                        <tbody>
                            <tr>
                                <td class="titleField" style="height: 23px" width="24%">Kategori</td>
                                <td style="height: 23px" width="1%">:</td>
                                <td style="height: 23px" width="75%">
                                    <asp:DropDownList ID="ddlCategory" runat="server" Width="140" AutoPostBack="True"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="titleField">Model</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="ddlModel" runat="server" Width="140" AutoPostBack="True"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="24%">Kelas</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:DropDownList ID="ddlVehicleClass" runat="server" Width="140"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="24%">Kode</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtKode)" ID="txtKode"
                                        runat="server" size="22" MaxLength="4"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtKode" Display="Dynamic"
                                            ErrorMessage="* " EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td class="titleField">Deskripsi</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(TxtDeskripsi)" ID="TxtDeskripsi"
                                        runat="server" size="66" MaxLength="40"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtDeskripsi" Display="Dynamic"
                                            ErrorMessage="* " EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>

                            <tr>
                                <td class="titleField">Tipe Segmen</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(TxtDeskripsi)" ID="txtSegmentType"
                                        runat="server" size="66" MaxLength="40"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="rfvSegmentType" runat="server" ControlToValidate="txtSegmentType" Display="Dynamic"
                                            ErrorMessage="* " EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td class="titleField">Tipe Varian</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(TxtDeskripsi)" ID="txtVariantType"
                                        runat="server" size="66" MaxLength="40"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="rfvVariantType" runat="server" ControlToValidate="txtVariantType" Display="Dynamic"
                                            ErrorMessage="* " EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td class="titleField">Tipe Transmisi</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(TxtDeskripsi)" ID="txtTransmitType"
                                        runat="server" size="66" MaxLength="40"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="rfvTransmitType" runat="server" ControlToValidate="txtTransmitType" Display="Dynamic"
                                            ErrorMessage="* " EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td class="titleField">Tipe Sistem Penggerak</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(TxtDeskripsi)" ID="txtDriveSystemType"
                                        runat="server" size="66" MaxLength="40"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="rfvDriveSystemType" runat="server" ControlToValidate="txtDriveSystemType" Display="Dynamic"
                                            ErrorMessage="* " EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td class="titleField">Tipe Kecepatan</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(TxtDeskripsi)" ID="txtSpeedType"
                                        runat="server" size="66" MaxLength="40"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="rfvSpeedType" runat="server" ControlToValidate="txtSpeedType" Display="Dynamic"
                                            ErrorMessage="* " EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td class="titleField">Tipe Bahan Bakar</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(TxtDeskripsi)" ID="txtFuelType"
                                        runat="server" size="66" MaxLength="40"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="rfvFuelType" runat="server" ControlToValidate="txtFuelType" Display="Dynamic"
                                            ErrorMessage="* " EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td class="titleField">Jumlah TOP Maksimum (Hari)</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox onkeypress="return numericOnlyUniv(event)" ID="txtMaxTOPDays" runat="server" Width="54px"
                                        CssClass="textRight">0</asp:TextBox>
                                    <asp:CheckBox ID="chkCriteriaTOP" runat="server" Text="Kriteria Pencarian"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="titleField"></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnSimpan" runat="server" Width="60px" Text="Simpan" Enabled="False"></asp:Button>&nbsp;
										<asp:Button ID="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:Button>&nbsp;
										<asp:Button ID="btnCari" runat="server" Width="60px" Text="Cari" CausesValidation="False"></asp:Button></td>
                            </tr>
                            <tr>
                                <td class="titleField"></td>
                                <td></td>
                                <td>
                                    <asp:Label Style="z-index: 0" ID="Label3" runat="server" Width="464px" ForeColor="Red" Font-Italic="True"> * Nilai Max TOP 0 (NOL) berarti mengikuti Jumlah Hari di bulan berjalan</asp:Label></td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="div1" style="height: 280px; overflow: auto">
                        <asp:DataGrid ID="dtgType" runat="server" Width="100%" PageSize="25" AllowCustomPaging="True"
                            AllowSorting="True" AllowPaging="True" CellSpacing="1" GridLines="None" CellPadding="3" BackColor="Gainsboro"
                            BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AutoGenerateColumns="False" ToolTip="Tabel Vehicle Type">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" VerticalAlign="Middle"
                                BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNo"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Category" HeaderText="Kategori">
                                    <HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Category.Description") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Category.Description") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="VechileModel" HeaderText="Model">
                                    <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileModel.Description") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileModel.Description") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kelas">
                                    <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VehicleClass.Description") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VehicleClass.Description") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="VechileTypeCode" SortExpression="VechileTypeCode" HeaderText="Kode">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
                                    <HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SegmentType" SortExpression="SegmentType" HeaderText="Tipe Segmen">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="VariantType" SortExpression="VariantType" HeaderText="Tipe Varian">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TransmitType" SortExpression="TransmitType" HeaderText="Tipe Transmisi">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DriveSystemType" SortExpression="DriveSystemType" HeaderText="Tipe Sistem Penggerak">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SpeedType" SortExpression="SpeedType" HeaderText="Tipe Kecepatan">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FuelType" SortExpression="FuelType" HeaderText="Tipe Bahan Bakar">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MaxTOPDays" SortExpression="MaxTOPDays" HeaderText="Max TOP Day">
                                    <HeaderStyle Width="12%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                                    <HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblStatus">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnView" runat="server" Text="Lihat" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" title="Lihat"></asp:LinkButton>&nbsp;
											<asp:LinkButton ID="lbtnEdit" runat="server" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" title="Ubah"></asp:LinkButton>&nbsp;
											<asp:LinkButton ID="lbtnDelete" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" title="Hapus"></asp:LinkButton>&nbsp;
											<asp:LinkButton ID="linkButonActive" runat="server" Width="16px" CausesValidation="False" CommandName="Activate">
												<img src="../images/in-aktif.gif" border="0" title="Klik untuk Aktifkan data"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButtonNonActive" runat="server" Width="16px" CausesValidation="False" CommandName="Deactivate">
												<img src="../images/aktif.gif" border="0" title="Klik untuk non-Aktifkan data"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    </form>
    <script language="javascript">
        if (window.parent == window) {
            if (!navigator.appName == "Microsoft Internet Explorer") {
                self.opener = null;
                self.close();
            }
            else {
                this.name = "origWin";
                origWin = window.open(window.location, "origWin");
                window.opener = top;
                window.close();
            }
        }
    </script>
</body>
</html>
