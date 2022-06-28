<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmWSCStatusList.aspx.vb" Inherits="FrmWSCStatusList" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>ListContract</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/ImagePopup.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/ImagePopup.js"></script>
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function ShowReason(ID) {
            showPopUp('../General/../PopUp/PopUpFreeServiceReason.aspx?ID=' + ID, '', 280, 460);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }

        function ShowPPDealerBranchSelection() {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            showPopUp('../General/../PopUp/PopUpDealerBranchMultipleSelection.aspx?DealerCode=' + txtDealerSelection.value, '', 500, 760, DealerBranchSelection);
        }

        function DealerBranchSelection(selectedDealerBranch) {
            var txtDealerBranchSelection = document.getElementById("txtKodeDealerBranch");
            txtDealerBranchSelection.value = selectedDealerBranch;
        }

        function SetPath(obj) {
            if (navigator.appName === "Microsoft Internet Explorer") {
                document.getElementById("lblPath").innerText = obj.lowsrc;
            } else {
                document.getElementById("lblPath").value = obj.lowsrc;
            }
            //document.getElementById("lblPath").innerText = obj.lowsrc;
        }

        function ShowEvidenceImage(obj) {
            var fraImageTest = document.getElementById("fraImageTest");
            fraImageTest.src = "../WebResources/GetImageGlobal.aspx?file=" + obj.lowsrc + "&hg=200&wd=200&type=ImageFile";

            var divImageTest = document.getElementById("imgBox");
            if (navigator.appName != "Microsoft Internet Explorer") {
                divImageTest = obj.parentNode.parentNode.childNodes[1];
            }
            divImageTest.style.visibility = "visible";
            divImageTest.innerHTML = '';
            divImageTest.appendChild(fraImageTest);
            divImageTest.style.left = (getElementLeft(obj)) + 'px';
            divImageTest.style.top = (getElementTop(obj)) + 'px';

            if (navigator.appName === "Microsoft Internet Explorer") {
                document.getElementById("lblPath").innerText = obj.lowsrc;
            } else {
                document.getElementById("lblPath").value = obj.lowsrc;
            }
            //document.getElementById("lblPath").innerText = obj.lowsrc;
        }

        function HideEvidenceImage(obj) {
            var divImageTest = document.getElementById("imgBox");
            if (navigator.appName != "Microsoft Internet Explorer") {
                divImageTest = obj.parentNode.parentNode.childNodes[1];
            }
            divImageTest.style.visibility = "hidden";
        }

        function getElementLeft(elm) {
            var x = 0;
            x = elm.offsetLeft;
            elm = elm.offsetParent;
            while (elm != null) {
                x = parseInt(x) + parseInt(elm.offsetLeft) - 34;
                elm = elm.offsetParent;
            }
            return x;
        }

        function getElementTop(elm) {
            var y = 0;
            y = elm.offsetTop;
            elm = elm.offsetParent;
            while (elm != null) {
                y = parseInt(y) + parseInt(elm.offsetTop) - 24;
                elm = elm.offsetParent;
            }
            return y;
        }
    </script>
</head>
<body onfocus="return checkModal()" onclick="checkModal()" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">WSC - Daftar Status WSC</td>
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
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="24%">
                                <asp:Label ID="lblDealer" runat="server">Kode Dealer</asp:Label></td>
                            <td width="1%">
                                <asp:Label ID="lblColon1" runat="server">:</asp:Label></td>
                            <td width="35%">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
                                    runat="server"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>
                            <td class="titleField" width="15%">
                                <asp:Label ID="lblClaimNo" runat="server">Nomor Klaim</asp:Label></td>
                            <td width="1%">
                                <asp:Label ID="lblColon4" runat="server">:</asp:Label></td>
                            <td width="24%">
                                <asp:TextBox onkeypress="return alphaNumericPlusUniv(event)" ID="txtClaimNo" onblur="alphaNumericPlusBlur(txtClaimNo)"
                                    runat="server" size="22" MaxLength="6"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField">Kode Cabang</td>
                            <td>
                                <asp:Label ID="lblColon2" runat="server">:</asp:Label></td>
                            <td>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtKodeDealerBranch" onblur="omitSomeCharacter('txtKodeDealerBranch','<>?*%$')"
                                    runat="server"></asp:TextBox><asp:Label ID="lblSearchDealerBranch" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                            </td>
                            <td class="titleField">
                                <asp:Label ID="lblVehicleType" runat="server">Tipe Kendaraan</asp:Label></td>
                            <td>
                                <asp:Label ID="lblColon5" runat="server">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlVehicleType" runat="server" Width="140"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:CheckBox ID="chkKirim" runat="server" Checked="True" Text="Periode WSC"></asp:CheckBox></td>
                            <td>
                                <asp:Label ID="lblColon3" runat="server">:</asp:Label></td>
                            <td nowrap>
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="icStartKirim" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s/d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="icEndKirim" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                            <td class="titleField">
                                <asp:Label ID="lblStatus2" runat="server">Status</asp:Label></td>
                            <td>
                                <asp:Label ID="lblColon6" runat="server">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="140"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:CheckBox ID="chkProses" runat="server" Text="Periode Proses"></asp:CheckBox></td>
                            <td>:</td>
                            <td nowrap>
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="icStartProses" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s/d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="icEndProses" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                            <td class="titleField">
                                <asp:Label ID="Label6" runat="server">Jenis WSC</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlJenisWSC" runat="server" Width="140">
                                    <asp:ListItem Text="Pilih" Value="Pilih" />
                                    <asp:ListItem Text="Z2" Value="Z2" />
                                    <asp:ListItem Text="Z4" Value="Z4" />
                                    <asp:ListItem Text="ZA" Value="ZA" />
                                    <asp:ListItem Text="ZB" Value="ZB" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:CheckBox ID="chkRelease" runat="server" Text="Periode Release"></asp:CheckBox></td>
                            <td>:</td>
                            <td nowrap>
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="icStartRelease" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s/d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="icEndRelease" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                            <td class="titleField">
                                <asp:Label ID="Label1" runat="server">Tipe Bukti WSC</asp:Label></td>
                            <td>
                                <asp:Label ID="Label3" runat="server">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlEvidenceType" runat="server" Width="140"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td><span class="titleField">
                                <asp:Label ID="lblCategory" runat="server" Visible="False"> Kategori</asp:Label></span>
                            </td>
                            <td>
                                <asp:Label ID="lblCategory2" runat="server" Visible="False">:</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCategory" runat="server" Width="140px" Visible="False"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td style="width: 226px">
                                <asp:Button ID="btnSearch" runat="server" Text=" Cari " Width="60px"></asp:Button></td>
                            <td class="titleField">
                                <asp:Label ID="Label7" runat="server">Kode Penolakan</asp:Label></td>
                            <td>
                                <asp:Label ID="Label8" runat="server">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlStatusWSC" runat="server" Width="140">
                                    <asp:ListItem Text="Pilih" Value="Pilih" />
                                    <asp:ListItem Text="APP" Value="APP" />
                                    <asp:ListItem Text="DAPP" Value="DAPP" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="right">
                                <div id="divPath" runat="server">
                                    <asp:TextBox ID="lblPath" Visible="true" runat="server" Width="0px"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="6">
                                <div id="div1" style="height: 360px; overflow: auto">
                                    <asp:DataGrid ID="dgStatusList" runat="server" Width="100%" CellPadding="3" BorderWidth="0px"
                                        CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowCustomPaging="True" AllowPaging="True" AllowSorting="True"
                                        PageSize="50" ShowFooter="True">
                                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                                <HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                                                <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></FooterStyle>
                                                <FooterTemplate>
                                                    <b>Baru:</b>
                                                    <asp:Label ID="lblBaru" runat="server"></asp:Label><br>
                                                    <b>Proses:</b>
                                                    <asp:Label ID="lblProses" runat="server"></asp:Label><br>
                                                    <b>Disetujui:</b>
                                                    <asp:Label ID="lblApprove" runat="server"></asp:Label><br>
                                                    <b>Ditolak:</b>
                                                    <asp:Label ID="lblDisapprv" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="RefClaimNumber" SortExpression="RefClaimNumber" HeaderText="WSC Referensi No">
                                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn SortExpression="Reason.Description" HeaderText="Alasan">
                                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.Reason.Description") %>' Text='<%# DataBinder.Eval(Container, "DataItem.Reason.ReasonCode") %>' ID="btnAlasan">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="ClaimStatus" SortExpression="ClaimStatus" ReadOnly="True" HeaderText="A/D">
                                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="NotificationNumber" SortExpression="NotificationNumber" ReadOnly="True"
                                                HeaderText="Notifikasi">
                                                <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ClaimType" SortExpression="ClaimType" ReadOnly="True" HeaderText="Jenis WSC">
                                                <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
                                                <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="DealerBranch.DealerBranchCode" HeaderText="Cabang">
                                                <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerBranchCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerBranch.DealerBranchCode")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="ClaimNumber" HeaderText="Nomor WSC">
                                                <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <%--<asp:LinkButton ID="lnkClaimNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ClaimNumber") %>' CommandName="lnkClaimNumber">
                                                    </asp:LinkButton>--%>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ClaimNumber")%>' ID="lblClaimNumber">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="ChassisMaster.ChassisNumber" HeaderText="Nomor Rangka">
                                                <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber") %>' ID="Label2">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="ChassisMaster.Category.CategoryCode" HeaderText="Kategori">
                                                <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.Category.CategoryCode") %>' ID="Label5">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="CreateDateText" SortExpression="CreatedTime" ReadOnly="True" HeaderText="Tgl WSC">
                                                <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="DecideDateText" SortExpression="DecideDate" ReadOnly="True" HeaderText="Tgl Proses">
                                                <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn SortExpression="ReleaseDate" HeaderText="Tgl Rilis">
                                                <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ReleaseDate") %>' ID="lblReleaseDate">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="PartAmount" HeaderText="Penggantian Parts">
                                                <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartAmnt" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></FooterStyle>
                                                <FooterTemplate>
                                                    <b>Part (APP):</b><br>
                                                    <asp:Label ID="lblAPPPartAmnt" runat="server"></asp:Label><br>
                                                    <b>Part (DAPP):</b><br>
                                                    <asp:Label ID="lblDAPPPartAmnt" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="LaborAmount" HeaderText="Ongkos Kerja">
                                                <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLaborAmnt" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></FooterStyle>
                                                <FooterTemplate>
                                                    <b>Ongkos (APP):</b><br>
                                                    <asp:Label ID="lblAPPLaborAmnt" runat="server"></asp:Label><br>
                                                    <b>Ongkos (DAPP):</b><br>
                                                    <asp:Label ID="lblDAPPLaborAmnt" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Email" Visible="false">
                                                <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEmail" runat="server" CommandName="lnkEmail">
															<img src="../images/icon_mail.gif" border="0" style="cursor:hand" alt="Send email">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="WorkOrderNumber" HeaderText="WO Number">
                                                <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWONumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WorkOrderNumber") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Lampiran WSC">
                                                <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <div id="imgbox">
                                                        <iframe id="fraImageTest" width="200px" height="200px" frameborder="0"></iframe>
                                                    </div>
                                                    <asp:LinkButton ID="lnkDownloadLampiran" runat="server" CommandName="lnkDownloadLampiran"  CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                                        <img src="../images/download.gif" style="cursor:hand" border="0" alt="Download Lampiran">
															<!--<img id='imgKwitansi' style="width:20px; height:20px;" alt="" onmouseout="Out();" src='\\172.17.31.26\d$\KTB.DNET.Phase4\KTB.DNet.UI\DataFile\WSC\100001\114100\SS2006316103711859.JPG' onmouseover="Large(this)" />-->
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Kwitansi WSC">
                                                <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <div id="imgbox">
                                                        <iframe id="fraImageTest" width="200px" height="200px" frameborder="0"></iframe>
                                                    </div>
                                                    <asp:LinkButton ID="lnkKwitansi" runat="server" CommandName="lnkKwitansi">
															<!--<img id='imgKwitansi' style="width:20px; height:20px;" alt="" onmouseout="Out();" src='\\172.17.31.26\d$\KTB.DNET.Phase4\KTB.DNet.UI\DataFile\WSC\100001\114100\SS2006316103711859.JPG' onmouseover="Large(this)" />--></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Surat WSC" Visible="false">
                                                <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <div id="imgbox">
                                                        <iframe id="fraImageTest" width="200px" height="200px" frameborder="0"></iframe>
                                                    </div>
                                                    <asp:LinkButton ID="lnkSurat" runat="server" CommandName="lnkSurat"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Teknikal WSC" Visible="false">
                                                <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <div id="imgbox">
                                                        <iframe id="fraImageTest" width="200px" height="200px" frameborder="0"></iframe>
                                                    </div>
                                                    <asp:LinkButton ID="lnkTeknikal" runat="server" CommandName="lnkTeknikal">
															<!--<img id='imgKwitansi' style="width:20px; height:20px;" alt="" onmouseout="Out();" src='\\172.17.31.26\d$\KTB.DNET.Phase4\KTB.DNet.UI\DataFile\WSC\100001\114100\SS2006316103711859.JPG' onmouseover="Large(this)" />--></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Repair/WO" Visible="false">
                                                <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <div id="imgbox">
                                                        <iframe id="fraImageTest" width="200px" height="200px" frameborder="0"></iframe>
                                                    </div>
                                                    <asp:LinkButton ID="lnkRepairWO" runat="server" CommandName="lnkRepairWO"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Part Bekas" Visible="false">
                                                <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <div id="imgbox">
                                                        <iframe id="fraImageTest" width="200px" height="200px" frameborder="0"></iframe>
                                                    </div>
                                                    <asp:LinkButton ID="lnkPartBekas" runat="server" CommandName="lnkPartBekas"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Photo" Visible="false">
                                                <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <div id="imgbox">
                                                        <iframe id="fraImageTest" width="200px" height="200px" frameborder="0"></iframe>
                                                    </div>
                                                    <asp:LinkButton ID="lnkPhoto" runat="server" CommandName="lnkPhoto"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
									                    <img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
									                    <img alt="Lihat" src="../images/detail.gif" border="0"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="">
                                                <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbnHapus" runat="server" CommandName="Hapus"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                            <td valign="top"></td>
                        </tr>
                    </table>
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
    <!--

-->
</body>
</html>
