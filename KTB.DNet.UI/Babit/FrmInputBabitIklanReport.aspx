<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInputBabitIklanReport.aspx.vb" Inherits=".FrmInputBabitIklanReport" SmartNavigation="False" MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmInputBabitIklan</title>
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
            showPopUp('../PopUp/PopUpSearchBabitIklan.aspx', '', 430, 800, TemporaryOutlet);
        }

        function TemporaryOutlet(selectedRefNumber) {
            //var txtTemporaryOutlet = document.getElementById("txtTemporaryOutlet");
            var hdnNoReg = document.getElementById("hdnNoReg");
            //hdnNoReg.value = selectedRefNumber;

            //if (navigator.appName == "Microsoft Internet Explorer") {
            //    hdnNoReg.blur();
            //}
            //else {
            //    hdnNoReg.onchange();
            //}
            var txtNoReg = document.getElementById("txtNoReg");
            hdnNoReg.value = selectedRefNumber;
            txtNoReg.value = selectedRefNumber;
            var btnNoRegChange = document.getElementById("btnNoRegChange");
            btnNoRegChange.click();
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 18px">
                    <asp:Label ID="lblTitle" runat="server" Text="BABIT - INPUT LAPORAN BABIT IKLAN"></asp:Label></td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr valign="top">
                <td valign="top">
                    <table>
                        <tr>
                            <td valign="top">
                                <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                                    <tr>
                                        <td class="titleField" style="width: 33%; height: 10px">Nomor Registrasi</td>
                                        <td style="height: 3px" width="4px">
                                            <asp:Label ID="Label2" runat="server">:</asp:Label></td>
                                        <td style="width: 375px; height: 3px">
                                            <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtNoReg" ReadOnly="true"
                                                onblur="omitSomeCharacter('txtNoReg','<>?*%$')" runat="server" ToolTip="Nomor Registrasi Search 1" Width="128px" AutoPostBack="true"></asp:TextBox>
                                            <asp:label ID="lblPopUpRegNumber" runat="server" Width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                            </asp:label>
                                            <asp:HiddenField runat="server" ID="hdnNoReg" />
                                            <asp:Button ID="btnNoRegChange" runat="server" Text="..." Style="display: none"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Kode Dealer</td>
                                        <td style="height: 10px" width="4px">
                                            <asp:Label ID="Label1" runat="server">:</asp:Label></td>
                                        <td style="width: 375px; height: 10px">
                                            <asp:Label ID="lblDealerCodeName" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Temporary Outlet</td>
                                        <td style="height: 10px" width="4px">
                                            <asp:Label ID="Label3" runat="server">:</asp:Label></td>
                                        <td style="width: 375px; height: 10px">
                                            <asp:TextBox ID="txtTemporaryOutlet" runat="server" Width="128px" Enabled="false"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Nama Temporary Outlet</td>
                                        <td style="height: 10px" width="4px">
                                            <asp:Label ID="Label7" runat="server">:</asp:Label></td>
                                        <td style="width: 375px; height: 10px">
                                            <asp:Label ID="lblNamaCabang" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Area</td>
                                        <td style="height: 10px" width="4px">
                                            <asp:Label ID="Label4" runat="server">:</asp:Label></td>
                                        <td style="width: 375px; height: 10px">
                                            <asp:Label ID="lblArea" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table id="Table12" cellspacing="1" cellpadding="2" width="100%" border="0">
                                    <tr>
                                        <td class="titleField" style="width: 33%; height: 10px">Nomor Surat</td>
                                        <td style="height: 10px" width="4px">
                                            <asp:Label ID="Label6" runat="server">:</asp:Label></td>
                                        <td style="width: 375px; height: 10px">
                                            <asp:TextBox ID="txtNomorSurat" runat="server" Width="128px" MaxLength="30"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Bulan Periode</td>
                                        <td width="1%">:</td>
                                        <td style="width: 375px; height: 10px">
                                            <asp:DropDownList ID="ddlMonth" runat="server" Width="100px"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlMonth" InitialValue=" "
                                                Width="16px" EnableClientScript="False" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Tahun Periode</td>
                                        <td width="1%">:</td>
                                        <td style="width: 375px; height: 10px">
                                            <asp:DropDownList ID="ddlYear" runat="server" Width="100px"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlYear" InitialValue=" "
                                                Width="16px" EnableClientScript="False" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <span style="height: 20px"></span>
                </td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:Label ID="Label5" runat="server" Text="Daftar Media" Font-Size="15px" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="dgIklan" runat="server" Width="90%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
                        CellPadding="3" CellSpacing="1" AutoGenerateColumns="False">
                        <FooterStyle ForeColor="#4A3C8C" BackColor="#DEDEDE"></FooterStyle>
                        <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                        <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                        <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                <HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server" Text='<%# container.itemindex+1 %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Media">
                                <HeaderStyle ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblMedia" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BabitParameterHeader.ParameterName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Nama Media">
                                <HeaderStyle ForeColor="White" CssClass="titleTablePromo" Width="20%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNamaMedia" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MediaName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Tipe">
                                <HeaderStyle ForeColor="White" CssClass="titleTablePromo" Width="13%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTipe" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BabitParameterDetail.ParameterDetailName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Category">
                                <HeaderStyle ForeColor="White" CssClass="titleTablePromo" Width="13%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblCategory" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Category.CategoryCode")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Ukuran">
                                <HeaderStyle ForeColor="White" CssClass="titleTablePromo" Width="10%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblUkuran" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Size")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Periode Mulai">
                                <HeaderStyle ForeColor="White" CssClass="titleTablePromo" Width="5%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblPeriodIklanStart" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PeriodIklanStart", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Periode Selesai">
                                <HeaderStyle ForeColor="White" CssClass="titleTablePromo" Width="5%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblPeriodIklanEnd" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PeriodIklanEnd", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Jumlah Tayang">
                                <HeaderStyle ForeColor="White" CssClass="titleTablePromo" Width="5%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblJmlTayang" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.ViewNumber"), "N0")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Nilai Pengajuan">
                                <HeaderStyle ForeColor="White" CssClass="titleTablePromo" Width="10%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNilaiPengajuan" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.SubmissionAmount"), "N0")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <span style="height: 20px"></span>
                </td>
            </tr>
            <tr>
                <td class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 75%; padding-top: 2px; text-align: Left">
                    <asp:Label ID="Label8" runat="server" Text="Lampiran Bukti Iklan" Font-Size="15px" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="dgUploadFile" runat="server" Width="70%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1"
                        AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                        <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                        <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                        <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                        <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="No.">
                                <HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <%# container.itemindex+1 %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Nama File">
                                <HeaderStyle Width="30%" CssClass="titleTablePromo"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnFileName" runat="server" CommandName="Download" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Path")%>'>
                                        <asp:Label ID="lblFileName" runat="server" alt="Download" Text='<%#DataBinder.Eval(Container, "DataItem.FileName")%>'></asp:Label>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                <FooterTemplate>
                                    <input id="UploadFile" onkeydown="return false;" style="width: 267px; height: 20px" type="file" size="25" name="File1" runat="server">
                                    <asp:Label ID="lblFileUpload" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Keterangan">
                                <HeaderStyle Width="40%" CssClass="titleTablePromo"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblKeterangan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FileDescription")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtKeterangan" runat="server" Width="350px" />
                                </FooterTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn>
                                <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
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
                </td>
            </tr>
            <tr>
                <th></th>
            </tr>
            <tr>
                <td style="height: 50px">
                    <asp:Button ID="btnSave" OnClientClick="return confirm('Anda yakin mau simpan?');" runat="server" Text="Simpan" Style="margin-right: 10px" class="hideButtonOnPrint" />
                    <asp:Button ID="btnBack" runat="server" Text="Kembali" Visible="false" class="hideButtonOnPrint" Style="margin-right: 10px" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
