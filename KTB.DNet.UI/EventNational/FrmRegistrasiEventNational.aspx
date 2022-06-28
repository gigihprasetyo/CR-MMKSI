<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmRegistrasiEventNational.aspx.vb" Inherits=".FrmRegistrasiEventNational" SmartNavigation="False" MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmRegistrasiEvent</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        
        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var data = selectedDealer.split(";");
            var txtDealerCodeSelection = document.getElementById("txtDealerCode");
            txtDealerCodeSelection.value = data[0];
            var lblDealerCodeName = document.getElementById("lblDealerCodeName");
            lblDealerCodeName.innerHTML = data[0] + ' / ' + data[1];
            var btnGetInfoDealer = document.getElementById("btnGetInfoDealer");
            btnGetInfoDealer.click();

            //if (navigator.appName == "Microsoft Internet Explorer") {
            //    txtDealerCodeSelection.focus();
            //    txtDealerCodeSelection.blur();
            //}
            //else {
            //    txtDealerCodeSelection.onchange();
            //}
        }

        function ShowPopUpSalesman() {
            //var ddlJobPositionDesc = document.getElementById("ddlJobPositionDesc");
            //var Position = ddlJobPositionDesc.value;
            var lblDealerCodes = document.getElementById("txtDealerCode");
            var oDealerSalesman = lblDealerCodes.innerHTML
            var txtSalesmanCode = document.getElementById("txtSalesman");

            showPopUp('../PopUp/PopUpSalesmanMultiple.aspx?DealerSalesman=' + oDealerSalesman + '&SalesmanCode=' + txtSalesmanCode.value, '', 600, 600, SalesmanSelection);
        }

        function SalesmanSelection(SelectedSalesman) {
            var tempParam = SelectedSalesman.split(';');
            var valueSalesmanCode = SelectedSalesman;
            var txtSalesmanCode = document.getElementById("txtSalesman");
            if (navigator.appName == "Microsoft Internet Explorer") {
                txtSalesmanCode.innerText = valueSalesmanCode; //replace(tempParam[0], ' ', '');
            }
            else {
                txtSalesmanCode.value = valueSalesmanCode; //replace(tempParam[0], ' ', '');
            }
            __doPostBack("txtDealerCityID", "");
        }

        function trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }

    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <input id="hdnNationalEventID" type="hidden" value="0" runat="server">
        <input id="hdnNationalEventDetailID" type="hidden" value="0" runat="server">

        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">EVENT - REGISTRASI EVENT NASIONAL</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1" colspan="2">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10" colspan="2">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr valign="top">
                <td style="width: 50%">
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="width: 146px">Dealer</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
                                    Style="display: none" runat="server" ToolTip="Dealer Search 1" Width="128px" ReadOnly="true">
                                </asp:TextBox>
                                <asp:Label ID="lblPopUpDealer" runat="server" Width="16px" Style="display: none">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>&nbsp;<asp:Label ID="lblDealerCodeName" runat="server"></asp:Label>
                                <asp:Button ID="btnGetInfoDealer" runat="server" Text="..." Style="display: none"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Nama Event</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:Label ID="lblNamaEvent" runat="server"></asp:Label>
                                <input id="hdnLblNamaEvent" type="hidden" value="" runat="server">
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Periode</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:Label ID="lblPeriode" runat="server"></asp:Label>
                                <input id="hdnLblPeriode" type="hidden" value="" runat="server">
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Jadwal Dealer</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <table cellspacing="3" cellpadding="3">
                                    <tr>
                                        <td rowspan="2" valign="top">
                                            <%--<cc1:IntiCalendar ID="icTglKegiatanFrom" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>--%>
                                            <asp:Calendar ID="CalKegiatan" runat="server" BackColor="White" BorderColor="White"
                                                BorderWidth="1px" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black"
                                                Height="100px" NextPrevFormat="FullMonth"
                                                Width="200px" OnVisibleMonthChanged="CalKegiatan_VisibleMonthChanged" OnPreRender="CalKegiatan_PreRender" OnSelectionChanged="CalKegiatan_SelectionChanged">
                                                <DayHeaderStyle Font-Bold="True" Font-Size="6pt" />
                                                <NextPrevStyle Font-Bold="True" Font-Size="6pt" ForeColor="#333333"
                                                    VerticalAlign="Bottom" />
                                                <OtherMonthDayStyle ForeColor="#999999" />
                                                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px"
                                                    Font-Bold="True" Font-Size="10pt" ForeColor="#333399" />
                                            </asp:Calendar>
                                        </td>
                                        <td class="titleField" align="center" valign="top">
                                            <%--<cc1:IntiCalendar ID="icTglKegiatanTo" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>--%>
                                            Jumlah Hari :
                                            <asp:Label ID="lblJumlahHari" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:ListBox ID="tglList" runat="server" Width="90" Height="150"></asp:ListBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%" valign="top">
                    <table id="Table3" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="width: 146px">Nama Dealer PIC</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtNamaDealerPIC" runat="server" Width="178px" /></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">No Handphone</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtNoHandphone" runat="server" Width="178px" MaxLength="60" onkeypress="return numericOnlyUniv(event)"
                                    onblur="numericOnlyBlur(txtNoHandphone)" /></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Alamat Email</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtAlamatEmail" runat="server" Width="178px" TextMode="Email" 
                                    onkeypress="return HtmlCharUniv(event)"  onblur="HtmlCharBlur(txtAlamatEmail)" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="Silahkan isi email dealer dengan format email"
                                        ControlToValidate="txtAlamatEmail" Display="None" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>&nbsp;

                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" style="width: 146px">Salesman</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtSalesman" runat="server" Width="200px" MaxLength="5000" Height="50px"
                                    TextMode="MultiLine" AutoPostBack="true" style="display:none"></asp:TextBox>
                                <asp:TextBox ID="txtSalesmanShow" runat="server" Width="200px" MaxLength="5000" Height="100px"
                                    TextMode="MultiLine" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                                <asp:Label ID="lblSearchSalesman" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <div>
            <hr />
        </div>

        <div>
            <br />
        </div>

        <asp:Button ID="btnSave" OnClientClick="return confirm('Anda yakin mau simpan?');" runat="server" Text="Simpan"></asp:Button>&nbsp;
        <asp:Button ID="btnBack" runat="server" Text="Kembali" Visible="false"></asp:Button>&nbsp;
    </form>
</body>
</html>
