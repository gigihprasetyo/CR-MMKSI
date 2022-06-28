<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmVehicleInformationSystem.aspx.vb" Inherits="FrmVehicleInformationSystem" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>UMUM - Informasi Kendaraan</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript">

        function ShowPPInfoWSCSelection() {
            var txtChassisNumber = document.getElementById("txtChassisNumber");
            showPopUp('../PopUp/PopUpChangeStatusHistoryPKT.aspx?DocType=10&Chassis=' + txtChassisNumber.value, '', 500, 600, '');
        }

        function firstFocus() {
            document.all.txtChassisNumber.focus();
        }

        function enter(controlAfter) {

            var charPressed = event.keyCode;
            if (charPressed == 13) {
                controlAfter.focus();
                return false;
            }

        }
        function ShowPopUp() {
        }

        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }

        function ManageControlByLeasing() {
            var trEngineNumber = document.getElementById("trEngineNumber");

            if (!IsLeasing()) {
                trEngineNumber.style.visibility = 'hidden';
                return false;
            }
            var tbl = document.getElementById("Table1");
            var i = 0;
            //var dgPMStatus = document.getElementById("dgPMStatus");//trPMStatus
            var trPMStatus = document.getElementById("trPMStatus");
            //var dtgServiceData = document.getElementById("dtgServiceData");//id="trServiceData"
            var trServiceData = document.getElementById("trServiceData");
            var trFreeServiceTitle = document.getElementById("trFreeServiceTitle");
            var trMaintenancePeriod = document.getElementById("trMaintenancePeriod");

            //dgPMStatus.style.visibility = 'hidden';
            //dtgServiceData.style.visibility = 'hidden';
            trPMStatus.style.visibility = 'hidden';
            trServiceData.style.visibility = 'hidden';
            trFreeServiceTitle.style.visibility = 'hidden';
            trMaintenancePeriod.style.visibility = 'hidden';
            trEngineNumber.style.visibility = 'visible';

            for (i = 5; i < tbl.rows.length - 1; i++) {
                tbl.rows[i].style.visibility = 'hidden';
            }
        }
        function IsLeasing() {
            var query = window.location.search.substring(1);
            var vars = query.split("&");

            for (var i = 0; i < vars.length; i++) {
                var pair = vars[i].split("=");
                if (pair[0] == "IsLeasing") {
                    return true;
                }
            }
            return false;
        }
    </script>
</head>
<body onload="firstFocus()" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td colspan="6">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage">UMUM - Informasi Kendaraan</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor.gif" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="24%">Nomor Rangka</td>
                <td width="1%">:</td>
                <td width="75%" colspan="4">
                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtChassisNumber" runat="server"></asp:TextBox><asp:Button ID="btnSearch" runat="server" Text="Cari" Width="50px"></asp:Button></td>
            </tr>
            <tr id="trEngineNumber">
                <td class="titleField" width="24%">Nomor Mesin</td>
                <td width="1%">:</td>
                <td colspan="2">
                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtNoEngine" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearchEngine" runat="server" Width="50px" Text="Cari"></asp:Button></td>
                <td width="1%"></td>
                <td width="29%"></td>
            </tr>
            <tr>
                <td class="titleField" width="24%">Model / Tipe / Warna</td>
                <td width="1%">:</td>
                <td width="25%">
                    <asp:Label ID="lblMaterial" runat="server"></asp:Label></td>
                <td class="titleField" width="20%">No Serial</td>
                <td width="1%">:</td>
                <td width="29%">
                    <asp:Label ID="lblNoSerial" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="titleField">No Rangka</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblNoChassis" runat="server"></asp:Label></td>
                <td class="titleField">Tanggal Buka Faktur</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblFakturOpenDate" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:Label ID="lblNoEngineTitle" runat="server" Text="No Mesin"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblNoEngineColon" runat="server" Text=":"></asp:Label></td>
                <td>
                    <asp:Label ID="lblNoEngine" runat="server"></asp:Label></td>
                <td class="titleField">Tanggal Faktur</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblFakturDate" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="titleField">Dealer Alokasi</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblDealerSold" runat="server"></asp:Label></td>
                <td class="titleField">Tanggal Cetak DO</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblDOPrintDate" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="titleField">Dealer Pelaksana PDI</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblDealerPDI" runat="server"></asp:Label></td>
                <td class="titleField">Tanggal Unit Keluar MKS</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblStockOutDate" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 18px"></td>
                <td style="height: 18px"></td>
                <td style="height: 18px"></td>
                <td class="titleField" style="height: 18px">Tanggal PDI</td>
                <td style="height: 18px">:</td>
                <td style="height: 18px">
                    <asp:Label ID="lblTglPDI" runat="server"></asp:Label><asp:Label ID="lblPDIIndicator" runat="server"></asp:Label>
                    <asp:Button ID="BtnDeletePDI" runat="server" Text="Hapus PDI" Visible="False"></asp:Button>
                    <asp:LinkButton ID="lbtnDeletePDI" runat="server" CommandName="DeletePM" Text="Hapus" CausesValidation="False"
                        Visible="False">
							<img border="0" src="../images/trash.gif" alt="Hapus" style="cursor:hand"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="titleField" style="height: 18px"></td>
                <td style="height: 18px"></td>
                <td style="height: 18px"></td>
                <td class="titleField" style="height: 18px">Program FS yang di dapat</td>
                <td style="height: 18px">:</td>
                <td style="height: 18px">
                    <asp:Label ID="lblFSType" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="titleField">Tgl PKT</td>
                <td style="height: 18px">:</td>
                <td class="auto-style2">
                    <asp:Label ID="lblHandoverDate" runat="server" Visible="false"></asp:Label>
                    <asp:LinkButton Style="margin-top: -20px;" ID="btnEdit" runat="server"
                        Text="Lihat" OnClick="btnEdit_Click" Visible="false">
						    <img alt="Lihat/Ubah" border="0" src="../images/edit.gif"></img>
                    </asp:LinkButton>
                    <cc1:IntiCalendar ID="ICHandoverDate" runat="server" TextBoxWidth="70" Visible="false"></cc1:IntiCalendar>
                    <asp:LinkButton Style="margin-top: -20px;" ID="btnSimpan" runat="server"
                        CausesValidation="False" CommandName="Detail" Text="Lihat" Visible="false">
                            <img alt="Lihat/Ubah" border="0" src="../images/simpan.gif"></img>
                    </asp:LinkButton>
                    <asp:LinkButton Style="margin-top: -17px;" ID="lnkbtnPopUpRefClaimNumber" runat="server"
                        CausesValidation="False" CommandName="Detail" Visible="false">
                            <img alt="Lihat/Ubah" border="0" src="../images/popup.gif"></img>
                    </asp:LinkButton>
                </td>
                <td class="titleField">Program Service Field Fix Campaign</td>
                <td style="height: 18px">:</td>
                <td style="height: 18px">
                    <asp:Label ID="lblRecallChassisMaster" runat="server"></asp:Label>
                </td>
            </tr>

            <tr style="display: none;">
                <td class="titleField">No Extended Free Service</td>
                <td style="height: 18px">:</td>
                <td style="height: 18px">
                    <asp:Label ID="lblNoRegRequest" runat="server"></asp:Label></td>
                <td class="titleField">&nbsp;</td>
                <td style="height: 18px">&nbsp;</td>
                <td style="height: 18px"></td>
            </tr>
            <tr>
                <td class="titleField" valign="top">Keterangan</td>
                <td valign="top">:</td>
                <td>
                    <asp:Label ID="lblKeteranganFleet" runat="server"></asp:Label></td>
                <td class="titleField"></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="6"></td>
            </tr>

            <tr id="trServiceDataCampaign">
                <td colspan="6"><em><strong><font size="2">Service Data Campaign</font></strong></em></td>
            </tr>
            <tr id="trServiceDataCampaignGrid">
                <td colspan="6">
                    <div id="div2" style="overflow: auto">
                        <asp:DataGrid ID="dgServiceDataCampaign" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
                            PageSize="50" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px" CellPadding="3" BackColor="#CDCDCD">
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle ForeColor="White"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Dealer Pelaksana">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:BoundColumn DataField="MileAge" SortExpression="MileAge" HeaderText="Jarak Tempuh" DataFormatString="{0:#,###}">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="ServiceDate" HeaderText="Tanggal Pengerjaan">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblFS" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate", "{0:dd/MM/yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tanggal Proses">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTglPro" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate", "{0:dd/MM/yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Field Fix Reg No">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblRecallRegNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RecallChassisMaster.RecallCategory.RecallRegNo")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Deskripsi">
                                    <HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RecallChassisMaster.RecallCategory.Description") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn Visible="false">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" Text="Del" CommandName="Delete">
												<img src="../images/trash.gif" alt="Hapus" border="0"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                            </Columns>
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>


            <tr id="trFreeServiceTitle">
                <td colspan="6"><em><strong><font size="2">Free Service Data</font></strong></em></td>
            </tr>
            <tr id="trServiceData">
                <td colspan="6">
                    <div id="div1" style="overflow: auto">
                        <asp:DataGrid ID="dtgServiceData" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
                            PageSize="50" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px" CellPadding="3" BackColor="#CDCDCD">
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle ForeColor="White"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="FSKind.KindCode" HeaderText="Kind">
                                    <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKind" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FSKind.ID") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="MileAge" SortExpression="MileAge" HeaderText="Jarak Tempuh" DataFormatString="{0:#,###}">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="ServiceDate" HeaderText="Tanggal FS">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblFS" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate", "{0:dd/MM/yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="CreatedTime" HeaderText="Tanggal Proses">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTglPro" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate", "{0:dd/MM/yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer Pelaksana">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.ID") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Status">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" Text="Hapus" CausesValidation="False">
												<img border="0" src="../images/trash.gif" alt="Hapus" style="cursor:hand"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="False" HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr id="trMaintenancePeriod">
                <td colspan="6"><em><strong><font size="2">Periodical Maintenance&nbsp;Data</font></strong></em></td>
                </TD>
            </tr>
            <tr id="trPMStatus">
                <td colspan="6">
                    <asp:DataGrid ID="dgPMStatus" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
                        BorderColor="#E7E7FF" CellSpacing="1" BorderWidth="0px" CellPadding="3" BackColor="#CDCDCD"
                        BorderStyle="None" GridLines="Vertical" PageSize="100">
                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                        <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                        <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                        <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="StandKM" HeaderText="Kind">
                                <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblJenis" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PMKind.KindCode")%>' ToolTip='<%# DataBinder.Eval(Container, "DataItem.PMKind.KindCode") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="StandKM" SortExpression="StandKM" HeaderText="Jarak Tempuh" DataFormatString="{0:#,###}">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn SortExpression="ServiceDate" HeaderText="Tanggal PM">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTglPM" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate","{0:dd/MM/yyyy}") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="ReleaseDate" HeaderText="Tanggal Proses">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTglRilis" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ReleaseDate","{0:dd/MM/yyyy}") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer Pelaksana">
                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblKodeDealer" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1") %>' Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn SortExpression="EntryType" HeaderText="Sumber">
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblEntryType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EntryType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>


                            <asp:TemplateColumn HeaderText="Keterangan" Visible="false">
                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                <ItemTemplate>


                                    <asp:Label ID="lblMSPDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Remarks")%>' ToolTip='<%# DataBinder.Eval(Container, "DataItem.Remarks")%>'>
                                    </asp:Label>

                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn SortExpression="PMStatus" HeaderText="Status">
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblPopUpDetail" runat="server">
											<img src="../images/detail.gif" border="0" alt="Replacement Part"></asp:Label>
                                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="DeletePM" Text="Hapus" CausesValidation="False">
											<img border="0" src="../images/trash.gif" alt="Hapus" style="cursor:hand"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn Visible="False" HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblPMID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid></td>
            </tr>

            <%---- add by rudi--%>
            <tr id="trWarrantyServiceClaim">
                <td colspan="6"><em><strong><font size="2">Warranty Service Claim</font></strong></em></td>
            </tr>
            <tr id="trGridWarrantyServiceClaim">
                <td colspan="6">
                    <asp:DataGrid ID="dgWarrantyServiceClaim" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
                        BorderColor="#E7E7FF" CellSpacing="1" BorderWidth="0px" CellPadding="3" BackColor="#CDCDCD"
                        BorderStyle="None" GridLines="Vertical" PageSize="100">
                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                        <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                        <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                        <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="ClaimType" HeaderText="Jenis WSC">
                                <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblJenisWSC" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ClaimType")%>' ToolTip='<%# DataBinder.Eval(Container, "DataItem.ClaimType")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="PQR" HeaderText="Nomor PQR">
                                <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblPQRNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PQR")%>' ToolTip='<%# DataBinder.Eval(Container, "DataItem.PQR")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="ClaimNumber" HeaderText="Nomor WSC">
                                <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblClaimNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ClaimNumber")%>' ToolTip='<%# DataBinder.Eval(Container, "DataItem.ClaimNumber")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="Miliage" SortExpression="Miliage" HeaderText="Odometer" DataFormatString="{0:#,###}">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn SortExpression="CreatedTime" HeaderText="Tanggal Kirim">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedTime" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CreatedTime", "{0:dd/MM/yyyy}")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer Pelaksana">
                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>' ToolTip='<%# DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="ClaimStatus" HeaderText="Status">
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblClaimStatus" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn Visible="False" HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid></td>
            </tr>


            <%-- Append MSP --%>
            <tr id="trMSP" runat="server">
                <td colspan="6"><em><strong><font size="2">Mitsubishi Smart Package Claim</font></strong></em></td>
            </tr>
            <tr id="trGridMSP" runat="server">
                <td colspan="6">

                    <asp:DataGrid ID="dgMSP" runat="server" Width="100%" AllowSorting="False" AutoGenerateColumns="False"
                        BorderColor="#E7E7FF" CellSpacing="1" BorderWidth="0px" CellPadding="3" BackColor="#CDCDCD"
                        BorderStyle="None" GridLines="Vertical" PageSize="100">
                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                        <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                        <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                        <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="StandKM" HeaderText="Kind">
                                <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblJenis" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PMKind.KindCode")%>' ToolTip='<%# DataBinder.Eval(Container, "DataItem.PMKind.KindCode") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="StandKM" SortExpression="StandKM" HeaderText="Jarak Tempuh" DataFormatString="{0:#,###}">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn SortExpression="ServiceDate" HeaderText="Tanggal PM">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTglPM" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate","{0:dd/MM/yyyy}") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="ReleaseDate" HeaderText="Tanggal Proses">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTglRilis" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ReleaseDate","{0:dd/MM/yyyy}") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer Pelaksana">
                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblKodeDealer" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1") %>' Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>



                            <asp:TemplateColumn HeaderText="Keterangan">
                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                <ItemTemplate>


                                    <asp:Label ID="lblMSPDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Remarks")%>' ToolTip='<%# DataBinder.Eval(Container, "DataItem.Remarks")%>'>
                                    </asp:Label>

                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn Visible="false">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblPopUpDetail" runat="server">
											<img src="../images/detail.gif" border="0" alt="Replacement Part"></asp:Label>
                                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="DeletePM" Text="Hapus" CausesValidation="False">
											<img border="0" src="../images/trash.gif" alt="Hapus" style="cursor:hand"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn Visible="False" HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblPMID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>

                </td>
            </tr>

        </table>
    </form>
    <script language="javascript">
        ManageControlByLeasing();
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
