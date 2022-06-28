<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmServiceIncoming.aspx.vb" Inherits="FrmServiceIncoming" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<html>
<head>
    <title>FrmServiceIncoming</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script>
        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelectionArea.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer;
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = tempParam;
        }

        function openWindowDownload() {
            var h = document.getElementById('hDownloadURL');
            var url = h.value
            window.location = url;
        }

        function ShowPPDealerBranchSelection() {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            showPopUp('../General/../PopUp/PopUpDealerBranchMultipleSelection.aspx?DealerCode=' + txtDealerSelection.value, '', 500, 760, DealerBranchSelection);
        }

        function DealerBranchSelection(selectedDealerBranch) {
            var txtDealerBranchSelection = document.getElementById("txtKodeDealerBranch");
            txtDealerBranchSelection.value = selectedDealerBranch;
        }

    </script>
</head>
<body leftmargin="0" topmargin="0" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Data Upload - SVC Incoming</td>
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
                <td style="padding: 0">
                    <table id="Table2" cellspacing="2" cellpadding="2">
                        <tr>
                            <td class="titleField" width="150px">
                                <asp:Label ID="lblKodeDealerDetail" Text="Kode Dealer" runat="server" /></td>
                            <td width="1%">
                                <asp:Label ID="lblKodeDealerDetailSeparator" Text=":" runat="server" /></td>
                            <td width="250px">
                                <asp:Label ID="lblDealerCode" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="150px">
                                <asp:Label ID="lblNamaDealerDetail" Text="Nama Dealer" runat="server" /></td>
                            <td>
                                <asp:Label ID="lblNamaDealerDetailSeparator" Text=":" runat="server" /></td>
                            <td>
                                <p>
                                    <asp:Label ID="lblDealerName" runat="server"></asp:Label><asp:HiddenField ID="hAssistUploadLogID" runat="server" />
                                </p>
                                <asp:HiddenField ID="hDownloadURL" runat="server" />
                                <asp:HiddenField ID="hQuery" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 150px">
                                <asp:Label ID="lblKodeDealerMenu" Text="Kode Dealer" runat="server"></asp:Label></td>
                            <td width="1%">
                                <asp:Label ID="lblKodeDealerSeparator" Text=":" runat="server" /></td>
                            <td style="width: 250px">
                                <asp:TextBox ID="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" runat="server"
                                    Width="152px"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 150px">
                                <asp:Label ID="lblKodeDealerBranch" Text="Kode Cabang" runat="server"></asp:Label></td>
                            <td width="1%">
                                <asp:Label ID="lblKodeDealerBranchSeparator" Text=":" runat="server" /></td>
                            <td style="width: 250px">
                                <asp:TextBox ID="txtKodeDealerBranch" runat="server" TextMode="MultiLine" Width="152px"></asp:TextBox>
                                <asp:Label ID="lblSearchDealerBranch" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:CheckBox ID="chkTanggalBukaTransaksi" runat="server" Text="Tanggal Buka Transaksi" />
                            </td>
                            <td>:</td>
                            <td style="width: 250px">
                                <table>
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="bukaFrom" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s.d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="bukaTo" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:CheckBox ID="chkTanggalTutupTransaksi" runat="server" Text="Tanggal Tutup Transaksi" />
                            </td>
                            <td>:</td>
                            <td style="width: 250px">
                                <table>
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="tutupFrom" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s.d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="tutupTo" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 100px" class="titleField" />
                            <td rowspan="3">
                                <asp:Panel ID="pnlMonitoring" runat="server">
                                    <table width="100%" cellspacing="2" cellpadding="2">
                                        <tr>
                                            <td style="width: 150px" class="titleField">
                                                <asp:Label ID="Label16" runat="server"> Monitoring Upload Data</asp:Label>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px" class="titleField">
                                                <asp:Label ID="lblSIU" runat="server"> SIU</asp:Label>&nbsp;</td>
                                            <td style="width: 12px">:</td>
                                            <td>
                                                <asp:Label ID="lblSIUValue" runat="server" Font-Bold="True"></asp:Label>&nbsp;<asp:Label ID="Label12" runat="server" Font-Bold="True">Unit</asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px" class="titleField">
                                                <asp:Label ID="lblMSI" runat="server"> MSI</asp:Label>&nbsp;</td>
                                            <td style="width: 12px">:</td>
                                            <td>
                                                <asp:Label ID="lblMSIValue" runat="server" Font-Bold="True"></asp:Label>&nbsp;<asp:Label ID="Label18" runat="server" Font-Bold="True">Unit</asp:Label></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td width="150px"><strong>Nomor Work Order</strong></td>
                            <td>:</td>
                            <td width="250px">
                                <asp:TextBox onblur="alphaNumericPlusSpaceBlur(txtNoWorkOrder)" ID="txtNoWorkOrder" onkeypress="return alphaNumericPlusSpaceUniv(event)"
                                    runat="server" MaxLength="50"></asp:TextBox></td>
                            <td style="width: 100px" class="titleField" />

                        </tr>
                        <tr>
                            <td width="150px" class="titleField">
                                <asp:Label ID="Label1" runat="server">Nomor Chassis</asp:Label></td>
                            <td class="auto-style2">
                                <asp:Label ID="Label9" runat="server">:</asp:Label></td>
                            <td class="auto-style3">
                                <asp:TextBox onblur="alphaNumericPlusSpaceBlur(txtNoChassis)" ID="txtNoChassis" onkeypress="return alphaNumericPlusSpaceUniv(event)"
                                    runat="server" Width="140px" MaxLength="20"></asp:TextBox></td>
                            <td style="width: 100px" class="titleField" />
                        </tr>
                        <tr>
                            <td class="titleField" width="150px">Kategori Work Order</td>
                            <td width="1%">:</td>
                            <td width="250px">
                                <asp:DropDownList ID="ddlWorkOrderKategory" runat="server" Width="180px"></asp:DropDownList>
                            </td>
                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator4" runat="server" ErrorMessage="*" ControlToValidate="ddlWorkOrderKategory" InitialValue=" "
                                Width="16px" EnableClientScript="False" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="150px">Tempat Pengerjaan</td>
                <td width="1%">:</td>
                <td width="250px">
                    <asp:DropDownList ID="ddlTempatPengerjaan" runat="server" Width="180px"></asp:DropDownList></td>
                <asp:RequiredFieldValidator ID="Requiredfieldvalidator5" runat="server" ErrorMessage="*" ControlToValidate="ddlTempatPengerjaan" InitialValue=" "
                    Width="16px" EnableClientScript="False" Display="Dynamic"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="titleField" width="150px">Layanan</td>
                <td width="1%">:</td>
                <td width="250px">
                    <asp:DropDownList ID="ddlLayanan" runat="server" Width="180px"></asp:DropDownList></td>
                <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlLayanan" InitialValue=" "
                    Width="16px" EnableClientScript="False" Display="Dynamic"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>

                <td class="titleField" style="width: 150px">Status WO</td>
                <td width="1%">:</td>
                <td width="250px">
                    <asp:DropDownList ID="ddlWOStatus" runat="server" Width="180px"></asp:DropDownList></td>
                <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlWOStatus" InitialValue=" "
                    Width="16px" EnableClientScript="False" Display="Dynamic"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="titleField"></td>
                <td></td>
                <td>
                    <asp:Button ID="btnCari" runat="server" Text=" Cari " Width="60px" CausesValidation="False"></asp:Button>
                    <asp:Button ID="btnDownload" runat="server" Text=" Download " Width="80px" CausesValidation="False"></asp:Button></td>
            </tr>
        </table>
        </TD>
				</TR>
				<tr>
                    <td>
                        <div id="div1" style="overflow: auto; height: 360px">
                            <asp:DataGrid ID="dtgServiceIncoming" runat="server" Width="100%" PageSize="100" AllowCustomPaging="True"
                                AllowPaging="True" CellSpacing="1" ToolTip="Module" AutoGenerateColumns="False" CellPadding="3" GridLines="None" BackColor="#E0E0E0"
                                BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AllowSorting="True">
                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                <AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
                                <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                                <HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
                                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                <Columns>
                                    <asp:TemplateColumn Visible="False" HeaderText="ID">
                                        <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="No">
                                        <HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNo"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="TglBukaTransaksi" HeaderText="Tgl Buka Transaksi">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%#Format( DataBinder.Eval(Container, "DataItem.TglBukaTransaksi") ,"dd/MM/yyyy")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="WaktuMasuk" HeaderText="Waktu Masuk">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WaktuMasuk")%>' ID="Label2" NAME="Label2">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="TglTutupTransaksi" HeaderText="Tgl Tutup Transaksi">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#Format( DataBinder.Eval(Container, "DataItem.TglTutupTransaksi") ,"dd/MM/yyyy")%>' ID="Label3" NAME="Label3">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="WaktuKeluar" HeaderText="Waktu Keluar">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# String.Concat(DataBinder.Eval(Container, "DataItem.WaktuKeluar"))%>' ID="Label8" NAME="Label8">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="DealerCode" HeaderText="Kode Dealer">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode")%>' ID="Label5" NAME="Label5">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="DealerBranch.DealerBranchCode" HeaderText="Cabang Dealer">
                                        <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerBranch.DealerBranchCode")%>' ID="lbldealerbranchcodeitem" NAME="lbldealerbranchcodeitem">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="KodeMekanik" HeaderText="Kode Mekanik">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.KodeMekanik")%>' ID="Label6" NAME="Label6">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="NoWorkOrder" HeaderText="No Work Order">
                                        <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemStyle Wrap="false" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoWorkOrder")%>' ID="Label7" NAME="Label7">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="KodeChassis" HeaderText="No Chassis">
                                        <HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.KodeChassis")%>' ID="Label10" NAME="Label10">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="WorkOrderCategoryCode" HeaderText="Work Order Kategory">
                                        <HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WorkOrderCategoryCode")%>' ID="Label23" NAME="Label23">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="KMService" HeaderText="KM Service">
                                        <HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.KMService")%>' ID="Label11" NAME="Label11">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="ServicePlaceCode" HeaderText="Tempat Pengerjaan">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServicePlaceCode")%>' ID="Label12" NAME="Label12">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="ServiceTypeCode" HeaderText="Layanan">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceTypeCode")%>' ID="Label13" NAME="Label13">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="TotalLC" HeaderText="Total LC">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#Format( DataBinder.Eval(Container, "DataItem.TotalLC") ,"0.00")%>' ID="lblTotalLC" NAME="lblTotalLC">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="MetodePembayaran" HeaderText="Metode Pembayaran">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MetodePembayaran")%>' ID="Label15" NAME="Label15">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="RemarksSpecial" HeaderText="Remarks">
                                        <HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle Wrap="false" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RemarksSpecial")%>' ID="lblRemarsksSpecial" NAME="lblRemarsksSpecial">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="RemarksBM" HeaderText="Adjusment">
                                        <HeaderStyle Width="50%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle Wrap="false" />
                                        <ItemTemplate>
                                            <%--<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RemarksBM")%>' ID="txtRemarksBM" NAME="txtRemarksBM">
											</asp:TextBox>--%>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RemarksBM")%>' ID="lblRemarksBMHide" NAME="lblRemarksBMHide">
                                            </asp:Label>
                                            <asp:DropDownList ID="ddlModel" runat="server" Width="250px"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Model" HeaderText="Model">
                                        <HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Model")%>' ID="lblModel" NAME="lblModel">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="DriveSystem" HeaderText="Roda Penggerak">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DriveSystem")%>' ID="Label20" NAME="Label20">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Transmition" HeaderText="Transmisi">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Transmition")%>' ID="Label21" NAME="Label21">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Status">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemStyle Wrap="false" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AssistUploadLog.StatusDescription")%>' ID="Label22" NAME="Label22">
                                            </asp:Label>
                                            <asp:Label runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.AssistUploadLog.ValidateStatus")%>' ID="lblStatusValue" NAME="lblStatusValue">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
        <tr>
            <td colspan="6" valign="bottom">
                <table id="tblOperator" style="width: 192px; height: 46px" cellspacing="1" cellpadding="1"
                    width="192" border="0" runat="server">
                    <tr>
                        <td valign="top">
                            <asp:Button ID="btnSave" runat="server" Width="60px" Text="Simpan"></asp:Button></td>
                        <td valign="top">
                            <asp:Button ID="btnBack" Text="Kembali" runat="server"></asp:Button></td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
        </TABLE>

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
