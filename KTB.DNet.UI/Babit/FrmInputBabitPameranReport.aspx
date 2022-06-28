<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInputBabitPameranReport.aspx.vb" Inherits=".FrmInputBabitPameranReport" SmartNavigation="False" MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmInputBabitPameranReport</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" language="javascript">
        function ShowPopUpTO() {
            showPopUp('../PopUp/PopUpSearchBabitPameran.aspx', '', 430, 800, TemporaryOutlet);
        }

        function TemporaryOutlet(selectedRefNumber) {
            //var txtTemporaryOutlet = document.getElementById("txtTemporaryOutlet");
            var hdnNoReg = document.getElementById("hdnNoReg");
            hdnNoReg.value = selectedRefNumber;

            //if (navigator.appName == "Microsoft Internet Explorer") {
            //    hdnNoReg.blur();
            //}
            //else {
            //    hdnNoReg.onchange();
            //}
            var txtNoReg = document.getElementById("txtNoReg");
            txtNoReg.value = selectedRefNumber;
            var btnNoRegChange = document.getElementById("btnNoRegChange");
            btnNoRegChange.click();
        }

        function ShowPPEventProposalSelection() {
            showPopUp('../PopUp/PopUpEventProposalSelectionOne.aspx', '', 500, 760, EventProposalSelection);
        }

        function EventProposalSelection(selectedEvent) {
            var data = selectedEvent.split(";");
            //var txtEventRegNumber = document.getElementById("txtEventRegNumber");
            var hdnEventDealerHeaderID = document.getElementById("hdnEventDealerHeaderID");
            var hdnBabitEventProposalHeaderID = document.getElementById("hdnBabitEventProposalHeaderID");
            hdnBabitEventProposalHeaderID.value = data[0];
            hdnEventDealerHeaderID.value = data[1];
            //txtEventRegNumber.value = data[2];

            //if (navigator.appName == "Microsoft Internet Explorer") {
            //    txtEventRegNumber.focus();
            //    txtEventRegNumber.blur();
            //}
            //else {
            //    txtEventRegNumber.onchange();
            //}
        }

        function Location(selectedRefNumber) {
            var HFLocation = document.getElementById("HFLocation");
            HFLocation.value = selectedRefNumber;

            //if (navigator.appName == "Microsoft Internet Explorer") {
            //    HFLocation.blur();
            //}
            //else {
            //    HFLocation.onchange();
            //}
            var txtKodeTempOut = document.getElementById("txtKodeTempOut");
            txtKodeTempOut.value = selectedRefNumber;
        }
    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <input id="hdnBabitEventReportHeaderID" type="hidden" value="0" runat="server">

        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">BABIT - INPUT LAPORAN BABIT PAMERAN</td>
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
                <td style="width: 50%">
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="width: 146px">Nomor Registrasi</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
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
                            <td class="titleField" style="width: 146px">Kode Dealer</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:Label ID="lblDealerCodeName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Kode Temporary Outlet</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtTOCode" ReadOnly="true"
                                    onblur="omitSomeCharacter('txtTOCode','<>?*%$')" runat="server" ToolTip="TO Search 1" Width="128px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Nama Temporary Outlet</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:Label ID="lblTOName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Area</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:Label ID="lblArea" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Nomor Surat</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtNomorSurat" runat="server" Width="150px">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label3" runat="server">Lokasi</asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlLocationType" runat="server" AutoPostBack="true"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <span style="width: 50px"></span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLocation" runat="server" Width="128px" Visible="false" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trProvinsi" visible="false">
                            <td class="titleField">
                                <asp:Label ID="lblProvinsi" runat="server" Visible="false">Provinsi</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblProvinsiTitik2" runat="server" Visible="false">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlProvinsi" runat="server" Width="140px" AutoPostBack="true" Visible="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblKota" runat="server" Visible="false">Kota/Kab</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblKotaTitik2" runat="server" Visible="false">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlKota" runat="server" Width="140px" Visible="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblFactoring" runat="server">Tanggal Pameran</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFactoringColon" runat="server">:</asp:Label>
                            </td>
                            <td>
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="ICPameranStart" runat="server" TextBoxWidth="70" CanPostBack="True"></cc1:IntiCalendar>
                                        </td>
                                        <td>&nbsp;s.d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="ICPameranEnd" runat="server" TextBoxWidth="70" CanPostBack="True"></cc1:IntiCalendar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label9" runat="server">Periode Pameran</asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblPeriodePameran" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Target Prospek</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtProspectTarget" Style="text-align: right; vertical-align: middle;" runat="server"
                                    onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="60px" />
                                &nbsp;<label style="vertical-align: middle;">Prospek</label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <th></th>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label22" runat="server" Text="Kategori Display dan Target Penjualan" Font-Bold="true" Font-Size="X-Small"></asp:Label>
                    <div style="margin-top: 10px; margin-bottom: 10px; overflow-y: scroll; width: 80%">
                        <asp:DataGrid ID="dgDisplayAndTarget" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
                            CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" PageSize="1000000">
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
                                <asp:TemplateColumn HeaderText="Kategori">
                                    <HeaderStyle Width="20%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKategoriKendaraan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubCategoryVehicle.Category.CategoryCode")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Model">
                                    <HeaderStyle Width="25%" CssClass="titleTablePromo"></HeaderStyle>
                                    <%--<ItemStyle HorizontalAlign="Right"></ItemStyle>--%>
                                    <ItemTemplate>
                                        <asp:Label ID="lblModelKendaraan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubCategoryVehicle.Name")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Qty Display">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQtyDisplay" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Qty")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Target Penjualan">
                                    <HeaderStyle Width="20%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTargetPenjualan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesTarget")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Test Drive">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTestDrive" runat="server" Text='<%# IIf(DataBinder.Eval(Container, "DataItem.IsTestDrive"), "Ya", "Tidak")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <th></th>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Evaluasi Marbox dan Pameran  (Kisah Sukses, Hambatan, Komentar, dll):" Font-Bold="true" Font-Size="X-Small"></asp:Label>
                    <div style="margin-top: 10px; margin-bottom: 10px;">
                        <asp:TextBox ID="txtNotes" runat="server" MaxLength="5000" Width="500px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <th></th>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Daftar Prospek Yang di Dapat Saat Acara" Font-Bold="true" Font-Size="X-Small"></asp:Label>
                    <div style="margin-top: 10px; margin-bottom: 10px; overflow-y: scroll; width: 80%">
                        <asp:DataGrid ID="dgBabitEventSPKProspek" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
                            CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" PageSize="1000000">
                            <AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C" HorizontalAlign="Center"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No.">
                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jenis Kendaraan" SortExpression="VechileTypeKind">
                                    <HeaderStyle Width="120px" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVechileTypeKind" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileTypeKind")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tipe Kendaraan" SortExpression="VechileTypeName">
                                    <HeaderStyle Width="200px" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVechileTypeName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileTypeName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode Dealer" HeaderStyle-Width="100px" SortExpression="DealerCode">
                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nama Dealer" SortExpression="DealerName">
                                    <HeaderStyle Width="200px" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Unit" SortExpression="QtyUnit">
                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQtyUnit" runat="server" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <th></th>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Open Sales (SPK Saat Acara)" Font-Bold="true" Font-Size="X-Small"></asp:Label>
                    <div style="margin-top: 10px; margin-bottom: 10px; overflow-y: scroll; width: 80%">
                        <asp:DataGrid ID="dgBabitEventSPK" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
                            CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" PageSize="1000000">
                            <AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C" HorizontalAlign="Center"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No.">
                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jenis Kendaraan" SortExpression="VechileTypeKind">
                                    <HeaderStyle Width="120px" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVechileTypeKind" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileTypeKind")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tipe Kendaraan" SortExpression="VechileTypeName">
                                    <HeaderStyle Width="200px" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVechileTypeName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileTypeName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode Dealer" HeaderStyle-Width="100px" SortExpression="DealerCode">
                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nama Dealer" SortExpression="DealerName">
                                    <HeaderStyle Width="200px" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Unit" SortExpression="QtyUnit">
                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQtyUnit" runat="server" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <th></th>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Lampiran" Font-Bold="true" Font-Size="X-Small"></asp:Label>
                    <div style="margin-top: 10px; margin-bottom: 10px; overflow-y: scroll; width: 80%">
                        <asp:DataGrid ID="dgUploadFile" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1"
                            AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                            <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No.">
                                    <HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <%# container.itemindex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nama File">
                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnFileName" runat="server" CommandName="Download" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Path")%>'>
                                            <asp:Label ID="lblFileName" runat="server" alt="Download" Text ='<%#DataBinder.Eval(Container, "DataItem.FileName")%>'></asp:Label>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                    <FooterTemplate>
                                        <input id="UploadFile" onkeydown="return false;" style="width: 267px; height: 20px" type="file" size="25" name="File1" runat="server">
                                        <asp:Label ID="lblFileUpload" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Keterangan">
                                    <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
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
                </td>
            </tr>
            <tr>
                <th></th>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSave" OnClientClick="return confirm('Anda yakin mau simpan?');" runat="server" Text="Simpan" Style="margin-right: 10px" class="hideButtonOnPrint" />
                    <asp:Button ID="btnBack" runat="server" Text="Kembali" Visible="false" class="hideButtonOnPrint" Style="margin-right: 10px" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
