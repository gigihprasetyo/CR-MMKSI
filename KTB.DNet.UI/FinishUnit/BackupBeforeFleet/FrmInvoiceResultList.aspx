<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInvoiceResultList.aspx.vb" Inherits="FrmInvoiceResultList" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>ListContract</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
        var SomeChecked;
        function MakeValid() {
            SomeChecked = true;
        }

        function IsChecked() {
            if (IsAnyCheckedCheckBox('chkSelect')) {
                SomeChecked = true;
                return true;
            }
            else {
                SomeChecked = false;
                alert("Anda belum memilih faktur");
                return false;
            }
        }

        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
        }
        function DealerSelection(selectedDealer) {
            var temp = selectedDealer.split(';');
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = temp[0];
        }

        function ShowFleetReqSelection() {
            showPopUp('../PopUp/PopUpFleetReqTersedia.aspx?IsGroupDealer=1', '', 500, 800, FleetReqSelection);
        }

        function FleetReqSelection(selectedFleetReq) {
            var temp = selectedFleetReq.split(";")
            var txtNoFleetReq = document.getElementById('txtNoFleetReq');
            txtNoFleetReq.value = temp[0];
        }

    </script>
</head>
<body onfocus="return checkModal()" onclick="checkModal()" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px">FAKTUR KENDARAAN&nbsp;-&nbsp;Daftar 
						Status Faktur Kendaraan</td>
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
                            <td class="titleField" width="20%" style="height: 11px">
                                <asp:Label ID="lblDealerCode" runat="server">Kode Dealer</asp:Label></td>
                            <td width="1%" style="height: 11px">
                                <asp:Label ID="Label1" runat="server">:</asp:Label></td>
                            <td width="24%" style="height: 11px">
                                <asp:TextBox ID="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$;')"
                                    runat="server"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>
                            <td class="titleField" width="20%" style="height: 11px">
                                <asp:Label ID="lblCategory" runat="server"> Kategori</asp:Label></td>
                            <td width="1%" style="height: 11px">
                                <asp:Label ID="lblColon4" runat="server">:</asp:Label></td>
                            <td style="height: 11px">
                                <asp:DropDownList ID="ddlCategory" runat="server" Width="140px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblChassisNo" runat="server">Nomor Rangka</asp:Label></td>
                            <td>
                                <asp:Label ID="Label5" runat="server">:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtChassisNo" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtChassisNo','<>?*%$;')"
                                    runat="server" Width="140px" size="22" MaxLength="20"></asp:TextBox></td>
                            <td class="titleField">
                                <asp:CheckBox ID="chkValidPeriod" runat="server" Checked="True" Text="Periode Validasi"></asp:CheckBox></td>
                            <td>
                                <asp:Label ID="Label4" runat="server">:</asp:Label></td>
                            <td width="34%" nowrap>
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td>
                                            <cc1:inticalendar id="icStartValid" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                        <td>&nbsp;s/d&nbsp;</td>
                                        <td>
                                            <cc1:inticalendar id="icEndValid" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblInvoiceNo" runat="server">Nomor Faktur</asp:Label></td>
                            <td>
                                <asp:Label ID="Label3" runat="server">:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtInvoiceNo" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtInvoiceNo','<>?*%$;')"
                                    runat="server" Width="140px" size="22" MaxLength="20"></asp:TextBox></td>
                            <td class="titleField">
                                <asp:CheckBox ID="chkConfirmPeriod" runat="server" Text="Periode Konfirmasi"></asp:CheckBox></td>
                            <td>
                                <asp:Label ID="Label6" runat="server">:</asp:Label></td>
                            <td nowrap>
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td>
                                            <cc1:inticalendar id="icStartConfirm" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                        <td>&nbsp;s/d&nbsp;</td>
                                        <td>
                                            <cc1:inticalendar id="icEndConfirm" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="20%" style="height: 11px">
                                <asp:Label ID="Label9" runat="server">Nomor MCP</asp:Label></td>
                            <td width="1%" style="height: 11px">
                                <asp:Label ID="Label10" runat="server">:</asp:Label></td>
                            <td width="24%" style="height: 11px">
                                <asp:TextBox ID="txtMCPNumber" runat="server" Width="140px"></asp:TextBox>
                            </td>
                            <td class="titleField">
                                <asp:CheckBox ID="chkHandoverDate" runat="server" Text="Tgl PKT"></asp:CheckBox></td>
                            <td>
                                <asp:Label ID="Label11" runat="server">:</asp:Label></td>
                            <td nowrap>
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td>
                                            <cc1:inticalendar id="icHandoverDateStart" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                        <td>&nbsp;s/d&nbsp;</td>
                                        <td>
                                            <cc1:inticalendar id="icHandoverDateEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                    </tr>
                                </table>
                            </td>
                           
                        </tr>
                        <tr>
                            <td class="titleField" width="20%" style="height: 11px">Nomor Pengadaan</td>
                            <td width="1%" style="height: 11px">
                                <asp:Label ID="Label12" runat="server">:</asp:Label></td>
                            <td width="24%" style="height: 11px">
                                <asp:TextBox ID="txtLKPPNumber" runat="server" Width="140px"></asp:TextBox>
                            </td>
                            <td class="titleField" width="20%" style="height: 11px">No Extended Free Service</td>
                            <td width="1%" style="height: 11px">:</td>
                            <td style="height: 11px"><asp:TextBox ID="txtNoFleetReq" runat="server" Width="150px"></asp:TextBox>
                                <asp:Label ID="lblNoFleetReq" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label></td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField">
                                <asp:Label ID="lblStatus" runat="server">Status</asp:Label></td>
                            <td>
                                <asp:Label ID="lblttkduaPendingReason" runat="server" Visible="False">:</asp:Label></td>
                            <td>
                                <asp:ListBox ID="lboxStatus" runat="server" Width="140px" Rows="3" SelectionMode="Multiple"></asp:ListBox></td>
                            <td class="titleField">
                                <asp:Label ID="lblPendingReason" runat="server" Visible="False">Pending Reason</asp:Label></td>
                            <td></td>
                            <td>
                                <asp:TextBox ID="txtPendingReason" runat="server" Width="240px" Rows="5" Height="48px" TextMode="MultiLine"
                                    Visible="False"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td class="titleField"></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Width="60px" Text="Cari"></asp:Button></td>
                            <td class="titleField"></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSavePendingReason" runat="server" Width="142px" Text="Simpan Pending Reason"
                                    Visible="False"></asp:Button></td>
                        </tr>
                        <tr>
                            <td style="height: 8px" colspan="5">
                                <table>
                                    <tr>
                                        <td>
                                            <div style="background-color: aquamarine; width: 15px; height: 10px; border: 1px solid black;"></div>
                                        </td>
                                        <td>Tanggal PKT belum diisi</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblJumRecord" runat="server"></asp:Label></td>
                            <td></td>
                            <td></td>
                            <td class="titleField"></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="6">
                                <div id="div1" style="overflow: auto; height: 240px">
                                    <asp:DataGrid ID="dgInvoiceList" runat="server" Width="100%" AllowSorting="True" CellPadding="3"
                                        BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" PageSize="100" AllowPaging="True"
                                        AllowCustomPaging="True">
                                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                                <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Check">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <HeaderTemplate>
                                                    <input id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('chkSelect', document.all.chkAllItems.checked)"
                                                        type="checkbox">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="FakturStatusDesc" SortExpression="FakturStatus" ReadOnly="True" HeaderText="Status">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="EndCustomer.SPKFaktur.SPKHeader.SPKNumber" HeaderText="No. Reg. SPK">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EndCustomer.SPKFaktur.SPKHeader.SPKNumber") %>' ID="Label8">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="EndCustomer.SPKFaktur.SPKHeader.CreatedTime" HeaderText="Tgl Pengajuan SPK">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSPKCreatedDate" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.EndCustomer.SPKFaktur.SPKHeader.CreatedTime"),"dd/MM/yyyy") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="ChassisNumber" SortExpression="ChassisNumber" ReadOnly="True" HeaderText="Nomor Rangka">
                                                <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn SortExpression="VechileColor.MaterialNumber" HeaderText="Tipe/Warna">
                                                <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileColor.MaterialNumber") %>' ID="Label2">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="ValidateDateText" SortExpression="EndCustomer.ValidateTime" ReadOnly="True"
                                                HeaderText="Tgl Validasi">
                                                <HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ConfirmDateText" SortExpression="EndCustomer.ConfirmTime" ReadOnly="True"
                                                HeaderText="Tgl Konfirmasi">
                                                <HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="PrintedTimeText" SortExpression="EndCustomer.PrintedTime" ReadOnly="True"
                                                HeaderText="Tgl Selesai">
                                                <HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="FakturNumberText" SortExpression="EndCustomer.FakturNumber" ReadOnly="True"
                                                HeaderText="Nomor Faktur Kendaraan">
                                                <HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn SortExpression="EndCustomer.Customer.Name1" HeaderText="Nama Konsumen">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EndCustomer.Customer.Name1") %>' ID="Label7">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Kepemilikan Kendaraan">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKepemilikan" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Tipe">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKendaraan" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Detail">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDetail" runat="server" CommandName="lnkDetail">
															<img src="../images/detail.gif" border="0" style="cursor:hand" alt="Lihat detil">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nomor MCP">
                                                <HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMCPNumber" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="No Extended Free Service">
                                                <HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoFleetReq" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Nomor Pengadaan">
                                                <HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLKPPNumber" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Pending Reason">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkPending" runat="server" CommandName="view_desc" Visible="False">
															<img src="../images/tanya.gif" border="0" style="cursor:hand">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Tgl PKT">
                                                <HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <cc1:inticalendar id="icHandoverDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkSaveHandoverDate" runat="server" CommandName="SaveHandoverDate" Visible="true" ToolTip="Simpan Tgl PKT">
															            <img src="../images/simpan.gif" border="0" style="cursor:hand">
                                                                </asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnConfirm" runat="server" Width="60px" Text="Konfirmasi"></asp:Button>&nbsp;
						<asp:Button ID="btnCancel" runat="server" Width="88px" Text="Batal Konfirmasi"></asp:Button>&nbsp;
						<asp:Button ID="btnTransfer" runat="server" Width="96px" Text="Transfer Ke SAP"></asp:Button>&nbsp;
						<asp:Button ID="btnDnLoad" runat="server" Width="96px" Text="Download"></asp:Button>&nbsp;
						<asp:Button ID="btnDownloadFaktur" runat="server" Width="110px" Text="Download Faktur SPK"></asp:Button>&nbsp;
						<asp:Button ID="btnRetransfer" runat="server" Width="96px" Text="Transfer Ulang"></asp:Button>
                    <asp:TextBox ID="txtDownload" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="height: 8px"></td>
            </tr>
        </table>
    </form>
    <script language="javascript">

        document.getElementById("txtDownload").style.visibility = "hidden";

        if (document.getElementById("txtDownload").value != "") {
            var downloadURL = document.getElementById("txtDownload").value
            document.getElementById("txtDownload").value = ""
            document.location.href = "../DownloadContainer.aspx?" + downloadURL

            /*var downloadURL = document.getElementById("txtDownload").value;
            var width = 200;
            var height = 200;
            var left = (screen.width/2) - 100;
            var top = (screen.height/2) - 100;				

            document.getElementById("txtDownload").value = "";
                
            var strFeature = 'height=' + height + ',';	
            strFeature += 'width=' + width + ',';
            strFeature += 'center=yes,';	
            strFeature += 'status=no,';
            strFeature += 'help=no,';
            strFeature += 'resizable=yes,';
            strFeature += 'fullscreen=no';
            strFeature += 'left=' + left + ',';
            strFeature += 'top=' + top;
                    
            window.open('../DownloadContainer.aspx?'+downloadURL,'_blank',strFeature);		
            */
        }
    </script>
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
