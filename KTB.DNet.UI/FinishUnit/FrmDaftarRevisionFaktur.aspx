<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDaftarRevisionFaktur.aspx.vb" Inherits=".FrmDaftarRevisionFaktur" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Register TagPrefix="domain" Namespace="KTB.DNet.Domain" Assembly="KTB.DNet.Domain" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
    <title>REVISI FAKTUR - DAFTAR REVISI FAKTUR</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js" type="text/javascript"></script>
    <script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js" type="text/javascript"></script>
    <script language="javascript" src="../WebResources/jquery-1.10.2.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var SomeChecked;
        var listtext = [];
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

        function popupopened() { }

        function InputPasswordPlease() {
            //alert("Silahkan Masukkan Password SAP Anda")
            showPPPassword();
        }

        function showPPPassword() {
            showPopUp('../General/../PopUp/PopupInputPassword.aspx', '', 200, 600, GotPassword);
        }
        function GotPassword(result) {
            var txtUser = document.getElementById("txtUser");
            var txtPwd = document.getElementById("txtPass");
            var btn = document.getElementById("btnTransfertoSAP");
            var str = result;
            var username = '', pwd = '';

            username = str.split(';')[0];
            pwd = str.split(';')[1];

            txtUser.value = username;
            txtPwd.value = pwd;
            btn.click();
        }

        function promptPassword() {
            var txt = document.getElementById("txtPass");
            var div = document.getElementById("divPassword");

            if (txt.value)

                div.style.display = "inherit";
            alert("Please, Enter Your SAP Password First!")
            txt.focus();
        }
        function onLoad() {
            var div = document.getElementById("divPassword");
            div.style.display = "none";
        }

        function Remark(Text) {
            var txtRemark = document.getElementById("txtRemark");
            if (navigator.appName == "Microsoft Internet Explorer") {
                txtRemark.innerText = Text;
            }
            else {
                txtRemark.value = Text;
            }
        }

        function ShowPPRemark() {
            var txtRemark = document.getElementById("txtRemark");
            var enter = 13;
            var feedline = 10;
            var newstring = replace(txtRemark.value, String.fromCharCode(enter), '@');
            newstring = replace(newstring, String.fromCharCode(feedline), '*');
            var opentag = 60;
            newstring = replace(newstring, String.fromCharCode(opentag), '|');
            showPopUp('../FinishUnit/FrmInvoiceRevisionRemark.aspx?text=' + newstring, '', 400, 400, Remark)
        }

        function SetRemark() {
            var RbtnRemark1 = document.getElementById("RbtnRemark1");
            var RbtnRemark2 = document.getElementById("RbtnRemark2");
            var RbtnRemark3 = document.getElementById("RbtnRemark3");
            var RbtnRemark5 = document.getElementById("RbtnRemark5");
            var RbtnRemark4 = document.getElementById("RbtnRemark4");
            var lblRemark1 = document.getElementById("lblRemark1");
            var lblRemark2 = document.getElementById("lblRemark2");
            var lblRemark3 = document.getElementById("lblRemark3");
            var lblRemark5 = document.getElementById("lblRemark5");
            var txtRemark = document.getElementById("txtRemark");
            var lblSearchRemark = document.getElementById("lblSearchRemark");

            setRemarkText(RbtnRemark1.checked, lblRemark1.innerHTML);
            setRemarkText(RbtnRemark2.checked, lblRemark2.innerHTML);
            setRemarkText(RbtnRemark3.checked, lblRemark3.innerHTML);
            setRemarkText(RbtnRemark5.checked, lblRemark5.innerHTML);

            txtRemark.value = listtext.join(", ");

            //if (RbtnRemark4.checked) {
            //    lblSearchRemark.style.visibility = "visible";
            //}
        }

        function setRemarkText(ischecked, val) {
            if (ischecked) {
                //lblSearchRemark.style.visibility = "hidden";
                if (listtext.indexOf(val) == -1) {
                    listtext.push(val);
                }
            } else {
                if (listtext.indexOf(val) > -1) {
                    listtext.splice(listtext.indexOf(val), 1);
                }
            }
        }

        if (!Array.prototype.indexOf) {

            Array.prototype.indexOf = function (elem, startFrom) {

                var startFrom = startFrom || 0;

                if (startFrom > this.length) return -1; for (var i = 0; i < this.length; i++) {

                    if (this[i] == elem && startFrom <= i) {

                        return i;

                    } else if (this[i] == elem && startFrom > i) {

                        return -1;

                    }

                }

                return -1;

            }

        }
    </script>
</head>
<body ms_positioning="GridLayout" onload="onLoad();">
    <form id="form1" method="post" runat="server">
        <table id="table1" cellspacing="1" cellpadding="1" width="100%">
            <tr>
                <td>
                    <table id="table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" style="height: 17px">REVISI FAKTUR - DAFTAR REVISI FAKTUR</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor.gif" height="1">
                                <img height="1" alt="" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td style="height: 6px" height="6">
                                <img height="1" alt="" src="../images/dot.gif" border="0"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="Table3" cellspacing="1" cellpadding="2" width="100%">
                        <tr>
                            <td class="titleField" style="height: 11px" width="10%">
                                <asp:Label ID="lblDealer" runat="server">Kode Dealer</asp:Label></td>
                            <td width="1%">
                                <asp:Label ID="Label1" runat="server">:</asp:Label></td>
                            <td width="24%">
                                <asp:TextBox ID="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$;')" runat="server"></asp:TextBox>
                                <asp:Label ID="lblSearchDealer" runat="server">
									<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label>
                                <asp:Label ID="lblDealerCode" runat="server" Visible="false"></asp:Label>
                            </td>

                            <td class="titleField" style="height: 11px" width="10%">
                                <asp:Label ID="lblCategory" runat="server"> Kategori</asp:Label></td>
                            <td style="height: 11px" width="1%">
                                <asp:Label ID="lblColon4" runat="server">:</asp:Label></td>
                            <td style="height: 11px">
                                <asp:DropDownList ID="ddlCategory" runat="server" Width="150px" AutoPostBack="true"></asp:DropDownList>
                                <br />
                                <br />
                                <asp:DropDownList ID="ddlSubCategory" runat="server" Width="150px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 11px" width="10%">
                                <asp:Label ID="Label2" runat="server">No Pengajuan Revisi</asp:Label></td>
                            <td width="1%">
                                <asp:Label ID="Label3" runat="server">:</asp:Label></td>
                            <td width="24%">
                                <asp:TextBox ID="txtNoRequest" runat="server" Width="150px" onkeypress="return alphaNumericExcept(event,'<>?*%$;{},+=');"
                                    ToolTip="No Pengajuan Revisi"></asp:TextBox>
                            </td>

                            <td class="titleField">
                                <asp:Label ID="Label11" runat="server">Opsi Pembayaran</asp:Label>
                            </td>
                            <td width="1%" style="height: 11px">
                                <asp:Label ID="Label12" runat="server">:</asp:Label>
                            </td>
                            <td width="24%">
                                <asp:DropDownList ID="ddlPembayaran" runat="server">
                                    <asp:ListItem Value="-1">Silahkan Pilih</asp:ListItem>
                                    <asp:ListItem Value="1">Bayar</asp:ListItem>
                                    <asp:ListItem Value="0">Tidak Bayar</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 11px" width="10%">
                                <asp:Label ID="lblRevisionFakturNo" runat="server">No Faktur Revisi</asp:Label></td>
                            <td width="1%">
                                <asp:Label ID="Label5" runat="server">:</asp:Label></td>
                            <td width="24%">
                                <asp:TextBox ID="txtRevisionFakturNo" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtChassisNo','<>?*%$;')"
                                    runat="server" Width="150px" size="22" MaxLength="20"></asp:TextBox>
                            </td>

                            <td class="titleField">
                                <asp:Label ID="Label13" runat="server">Is Temporary</asp:Label>
                            </td>
                            <td width="1%" style="height: 11px">
                                <asp:Label ID="Label14" runat="server">:</asp:Label>
                            </td>
                            <td width="24%">
                                <asp:DropDownList ID="ddlIsTemporary" runat="server">
                                    <asp:ListItem Value="-1">Silahkan Pilih</asp:ListItem>
                                    <asp:ListItem Value="0">Normal</asp:ListItem>
                                    <asp:ListItem Value="1">Temporary</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 11px" width="10%">
                                <asp:Label ID="lblChassisNo" runat="server">No Rangka</asp:Label></td>
                            <td width="1%">
                                <asp:Label ID="Label6" runat="server">:</asp:Label></td>
                            <td width="24%">
                                <asp:TextBox ID="txtChassisNo" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtChassisNo','<>?*%$;')"
                                    runat="server" Width="150px" size="22" MaxLength="20"></asp:TextBox>
                            </td>

                            <td class="titleField" style="height: 11px" width="10%">
                                <asp:CheckBox ID="chkRequest" runat="server" Text="Tgl Pengajuan Revisi"></asp:CheckBox></td>
                            <td style="height: 11px" width="1%">
                                <asp:Label ID="Label7" runat="server">:</asp:Label></td>
                            <td style="height: 11px">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="icRequestStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                        </td>
                                        <td>&nbsp;s/d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="icRequestEnd" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 11px" width="10%">
                                <asp:Label ID="Label9" runat="server">Tipe Revisi</asp:Label></td>
                            <td width="1%">
                                <asp:Label ID="Label10" runat="server">:</asp:Label></td>
                            <td width="24%">
                                <asp:DropDownList ID="ddlRevisionType" runat="server"></asp:DropDownList>
                            </td>

                            <td class="titleField" style="height: 11px" width="10%">
                                <asp:CheckBox ID="chkValidPeriod" runat="server" Text="Tgl Validasi Revisi"></asp:CheckBox></td>
                            <td>
                                <asp:Label ID="Label8" runat="server">:</asp:Label></td>
                            <td width="34%" nowrap>
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="icStartValid" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s/d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="icEndValid" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblStatus" runat="server">Status</asp:Label>
                            </td>
                            <td width="1%" style="height: 11px">
                                <asp:Label ID="Label4" runat="server">:</asp:Label>
                            </td>
                            <td width="24%">
                                <asp:ListBox ID="lboxStatus" runat="server" Width="140px" Rows="3" SelectionMode="Multiple"></asp:ListBox>
                            </td>

                            <td class="titleField">
                                <asp:Label ID="lblRemark" runat="server" Visible="False">Remark</asp:Label></td>
                            <td></td>
                            <td>
                                <asp:Panel ID="pnlRemark" runat="server" Visible="false">
                                    <table>
                                        <tr>
                                            <td valign="top">
                                                <asp:CheckBox ID="RbtnRemark1" onclick="SetRemark();" runat="server" Text=""></asp:CheckBox>
                                                <asp:Label ID="lblRemark1" runat="server">Dokumen lama belum ada / tidak lengkap</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:CheckBox ID="RbtnRemark2" onclick="SetRemark();" runat="server" Text=""></asp:CheckBox>
                                                <asp:Label ID="lblRemark2" runat="server">Dokumen / data / info pendukung kurang</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:CheckBox ID="RbtnRemark3" onclick="SetRemark();" runat="server" Text=""></asp:CheckBox>
                                                <asp:Label ID="lblRemark3" runat="server">Diskon belum dikembalikan</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:CheckBox ID="RbtnRemark5" onclick="SetRemark();" runat="server" Text=""></asp:CheckBox>
                                                <asp:Label ID="lblRemark5" runat="server">Biaya revisi belum diterima</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:CheckBox ID="RbtnRemark4" onclick="SetRemark();" runat="server" Text=" "></asp:CheckBox>
                                                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" BackColor="#E0E0E0"
                                                    Rows="5" Width="152px"></asp:TextBox><asp:Label ID="lblSearchRemark" runat="server">
													<img id="imgSearchRemark" style="cursor:hand" src="../images/popup.gif" border="0"
														alt="Klik Popup"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label15" runat="server">Status Pembayaran</asp:Label>
                            </td>
                            <td width="1%" style="height: 11px">
                                <asp:Label ID="Label16" runat="server">:</asp:Label>
                            </td>
                            <td width="24%">
                                <asp:DropDownList ID="ddlPaymentStatus" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td colspan="2"></td>
                            <td>
                                <asp:Button ID="btnSaveRemark" runat="server" Width="142px" Text="Simpan Remark"
                                    Visible="False"></asp:Button></td>
                        </tr>



                          <tr>
                            <td class="titleField">
                                
                                Is matching</td>
                            <td width="1%" style="height: 11px">
                                <asp:Label ID="Label18" runat="server">:</asp:Label>
                            </td>
                            <td width="24%">
                                <asp:DropDownList ID="ddlMatching" runat="server">
                                    <asp:ListItem Text="Silahkan Pilih" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Matching" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Matching" Value="0"></asp:ListItem>

                                </asp:DropDownList>
                            </td>
                            <td colspan="2"></td>
                            <td>
                                
                        </tr>

                        <tr>
                            <td colspan="6"></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: right;">
                                <asp:Button ID="btnSearch" runat="server" Width="60px" Text="Cari"></asp:Button></td>
                            <td colspan="3"></td>
                        </tr>
                        <tr>
                            <td colspan="6" class="titleField">
                                <asp:Label ID="lblJumRecord" runat="server"></asp:Label></td>
                        </tr>

                        <tr>
                            <td valign="top" colspan="6">
                                <div id="divInvoiceRevisionList" style="overflow: auto; height: 240px">
                                    <asp:DataGrid ID="dgInvoiceRevisionList" runat="server" Width="100%" AllowSorting="True" CellPadding="3"
                                        BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" PageSize="100" AllowPaging="True"
                                        AllowCustomPaging="True">
                                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                                <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn Visible="False" DataField="RevisionTypeID" ReadOnly="True" HeaderText="RevisionType">
                                                <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
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
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="ChassisMaster.Dealer.DealerCode" HeaderText="Kode Dealer">
                                                <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.Dealer.DealerCode") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="RevisionStatus" HeaderText="Status Revisi Faktur">
                                                <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRevisionStatus" runat="server" Text='<%# GetRevisionStatusName(DataBinder.Eval(Container, "DataItem.RevisionStatus")) %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="IsPay" HeaderText="Opsi Pembayaran">
                                                <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# GetRevisionOpsiPayment(DataBinder.Eval(Container, "DataItem.IsPay")) %>' ID="lblOpsiPembayaran">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="RegNumber" SortExpression="RegNumber" ReadOnly="True" HeaderText="No Pengajuan Revisi">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn SortExpression="RevisionType.Description" HeaderText="Tipe Revisi">
                                                <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRevisionType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RevisionType.Description")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="ChassisMaster.ChassisNumber" HeaderText="Nomor Rangka">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblChassisNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="ChassisMaster.VechileColor.MaterialNumber" HeaderText="Tipe/Warna">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.VechileColor.MaterialNumber") %>' ID="Label2">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="EndCustomer.FakturNumber" HeaderText="Nomor Faktur">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFakturNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EndCustomer.FakturNumber") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="CreatedTime" HeaderText="Tgl Pengajuan Revisi">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRevisionCreatedTime" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.CreatedTime"),"dd/MM/yyyy") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="NewConfirmationDate" HeaderText="Tgl Validasi Revisi">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRevisionValidasi" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.NewConfirmationDate"),"dd/MM/yyyy") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="EndCustomer.PrintedTime" HeaderText="Tgl Selesai">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSelesaiTime" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.EndCustomer.PrintedTime"), "dd/MM/yyyy")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="OldEndCustomer.ValidateTime" HeaderText="Tgl Validasi">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblValidateTime" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.OldEndCustomer.ValidateTime"), "dd/MM/yyyy")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="EndCustomer.Customer.Name1" HeaderText="Nama Customer">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EndCustomer.Customer.Name1")%>' ID="lblNamaCustomer">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="EndCustomer.FakturDate" HeaderText="Tgl Faktur">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.EndCustomer.FakturDate"),"dd/MM/yyyy") %>' ID="Label7">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Debit Charge">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <%-- RevisionSAPDoc.DebitChargeNo --%>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RevisionSAPDoc.DebitChargeNo")%>' ID="lblDebitChargeNo">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Amount">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <%-- RevisionSAPDoc.DCAmount --%>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RevisionSAPDoc.DCAmount", "{0:N0}")%>' ID="lblDCAmount">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Status Pembayaran">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <%-- RevisionPaymentHeader.Status --%>
                                                    <asp:Label runat="server" Text='<%# If(CType(Eval("RevisionPaymentDetails.Count"), Integer) > 0, GetPaymentRevisionStatusName(Eval("RevisionPaymentDetails(0).RevisionPaymentHeader.Status")), "")%>' ID="lblPaymentHeaderStatus">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="No Reg Pembayaran">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <%-- RevisionPaymentHeader.RegNumber --%>
                                                    <asp:Label runat="server" Text='<%# If(CType(Eval("RevisionPaymentDetails.Count"), Integer) > 0, Eval("RevisionPaymentDetails(0).RevisionPaymentHeader.RegNumber").ToString, "")%>' ID="lblPaymentHeaderRegNumber">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Tgl Actual Bayar">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <%-- RevisionPaymentHeader.ActualPaymentDate --%>
                                                    <asp:Label runat="server" Text='<%# If(CType(Eval("RevisionPaymentDetails.Count"), Integer) > 0, If(Format(DataBinder.Eval(Container, "DataItem.RevisionPaymentDetails(0).RevisionPaymentHeader.ActualPaymentDate"), "dd/MM/yyyy") = "01/01/1753", "", Format(DataBinder.Eval(Container, "DataItem.RevisionPaymentDetails(0).RevisionPaymentHeader.ActualPaymentDate"), "dd/MM/yyyy")), "")%>' ID="lblActualPaymentDate">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="EndCustomer.IsTemporary" HeaderText="Temporary Faktur">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# If(Eval("EndCustomer.IsTemporary") > -1, EnumEndCustomer.TemporaryFakturDesc(Eval("EndCustomer.IsTemporary")), "")%>' ID="lblTemporary">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Remark">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkRemarkGrid" runat="server" CommandName="view_desc" Visible="False">
															<img src="../images/tanya.gif" border="0" style="cursor:hand">
                                                    </asp:LinkButton>
                                                    <asp:Label runat="server" Visible="false" ID="lblRemarkMatching">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Details">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkReviseInvoice" runat="server" CommandName="ReviseInvoice" Visible="false" ToolTip="Revisi Faktur">
															<img src="../images/edit.gif" border="0" style="cursor:hand" alt="Edit data"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkDetail" runat="server" CommandName="lnkDetail">
															<img src="../images/detail.gif" border="0" style="cursor:hand" alt="Lihat detil"></asp:LinkButton>
                                                    <asp:LinkButton ID="lblHistoryStatus" runat="server">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Lihat Perubahan Status"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkShowHistory" runat="server" CommandName="ShowHistory" ToolTip="History Revisi Faktur">
															<img src="../images/alur_flow2.gif" border="0" style="cursor:hand" alt="Lihat history revisi"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <asp:DropDownList ID="ddlKategoriPembayaran" runat="server">
                        <asp:ListItem Value="1">Bayar</asp:ListItem>
                        <asp:ListItem Value="0">Tidak Bayar</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnProcess" runat="server" Width="60px" Text="Proses"></asp:Button>&nbsp;
                    <asp:Button ID="btnConfirm" runat="server" Width="96px" Text="Konfirmasi"></asp:Button>&nbsp;
					<asp:Button ID="btnTransfer" runat="server" Width="96px" Text="Transfer"></asp:Button>&nbsp;
					<asp:Button ID="btnRetransfer" runat="server" Width="96px" Text="Transfer Ulang"></asp:Button>&nbsp;
                    <asp:Button Visible="false" ID="btnDownload" runat="server" Width="96px" Text="Download" CausesValidation="False"></asp:Button>&nbsp;
                    <asp:Button ID="btnTransfertoSAP" runat="server" Width="96px" Text="" Style="display: none;"></asp:Button>
                    <div id="divPassword" style="display: none;">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>SAP Password</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="txtUser" runat="server" Width="171px"></asp:TextBox>
                                    <asp:TextBox ID="txtPass" runat="server" TextMode="Password" Width="171px"></asp:TextBox>
                                </td>
                            </tr>

                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height: 8px"></td>
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
