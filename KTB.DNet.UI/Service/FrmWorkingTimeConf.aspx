<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmWorkingTimeConf.aspx.vb" Inherits=".FrmWorkingTimeConf" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>

<%--<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>--%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FrmWorkingTimeConf</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/scheduler_traditional.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/css/jquery-ui.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/select2.min.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/jquery-clockpicker.min.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/jquery.1.11.0.min.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/jquery-ui.min.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/select2.min.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/jquery-clockpicker.min.js"></script>

    <script type="text/javascript">
        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var data = selectedDealer.split(";");
            var txtKodeDealer = document.getElementById("txtKodeDealer");
            var txtNamaDealer = document.getElementById("txtNamaDealer");
            var btn = document.getElementById("btnTampilkan")
            txtKodeDealer.value = data[0];
            txtNamaDealer.value = data[1];

            btn.click();
        }

        //function timeRangeSelected(start, end, resource) {
        //    showPopUp("../PopUp/PopUpTimeWork.aspx?mode=new&start=" + start + "&end=" + end + "&res=" + resource, '', 500, 760, AfterSave);
        //}

        function eventClick(id) {
            var txtKodeDealer = document.getElementById("txtKodeDealer");
            showPopUp("../PopUp/PopUpTimeWork.aspx?id=" + id + "&dealerCode=" + txtKodeDealer.value, '', 500, 760, AfterSave);
        }

        function AfterSave(msg) {
            alert(msg);
            var btn = document.getElementById("btnTampilkan")
            btn.click();
        }

        function afterRender(data, isCallBack) {
        }

        function ShowConfirm(msg, id) {
            var btn = document.getElementById(id);
            var hdConfirm = document.getElementById("hdConfirm");
            if (confirm(msg)) {
                hdConfirm.value = "0";
                btn.click();
            }
        }

        $(function () {
            $(".textboxCalendar").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: '../images/calendar.gif'
            });

            $('.timepicker').clockpicker({
                placement: 'bottom',
                align: 'left',
                autoclose: true
            });

            $("#stallList").select2();
        });
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage"><asp:Label ID="Label1" runat="server" Text="Stall - Setting Kalender Kerja"></asp:Label></td>
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
        <table id="Table2" cellspacing="3" cellpadding="3" border="0">
            <%--<tr>
                <td class="titleTableParts3" colspan="5"><asp:Label ID="lblTitle" runat="server" Text="Setting Kalender Kerja"></asp:Label></td>
            </tr>--%>
            <tr>
                <td class="titleField" style="width: 200px" valign="top">Kode Dealer</td>
                <td style="width: 2px" valign="top">:</td>
                <td style="width: 340px" valign="top">
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" runat="server" ToolTip="Dealer Search"></asp:TextBox>
                    <asp:Label ID="lblPopupDealer" onclick="ShowPPDealerSelection();" runat="server" Style="z-index: 0">
					            <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                </td>
                <td rowspan="8" valign="top" align="left">
                    <asp:Calendar ID="CalWork" runat="server"
                        Font-Names="Verdana" Font-Size="7pt" ForeColor="WhiteSmoke"
                        Height="340px" NextPrevFormat="FullMonth"
                        Width="410px" OnSelectionChanged="CalWork_SelectionChanged" OnVisibleMonthChanged="CalWork_VisibleMonthChanged"
                        OnDayRender="CalWork_DayRender" >
                        <DayHeaderStyle Font-Bold="True" Font-Size="6pt" ForeColor="Black" BackColor="Aquamarine" BorderStyle="Solid"  />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="WhiteSmoke"
                            VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle ForeColor="Black" />
                        <TitleStyle BackColor="DarkSlateGray" BorderStyle="Solid"
                            Font-Bold="True" Font-Size="10pt" ForeColor="WhiteSmoke" CssClass="LabelSet" />
                    </asp:Calendar>
                </td>
                <td rowspan="7" valign="top" align="left">
                    <table cellpadding="4" cellspacing="1" border="0">
                        <tr>
                            <td colspan="2"><p>Legend Hari :</p></td>
                        </tr>
                        <tr>
                            <td>
                                <svg width="20" height="20">
                                    <rect width="20" height="20" style="fill:darkseagreen;" />
                                </svg>
                            </td>
                            <td><label>Kerja</label></td>
                        </tr>
                        <tr>
                            <td>
                                <svg width="20" height="20">
                                    <rect width="20" height="20" style="fill:#ff3300;" />
                                </svg>
                            </td>
                            <td><label>Libur</label></td>
                        </tr>
                        <tr>
                            <td>
                                <svg width="20" height="20">
                                    <rect width="20" height="20" style="fill:darkslategray;" />
                                </svg>
                            </td>
                            <td><label>Belum Generate</label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="titleField" valign="top">Nama Dealer</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top"><asp:TextBox ID="txtNamaDealer" runat="server" Width="275px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="titleField" valign="top">Periode</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <table cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <%--<cc1:IntiCalendar ID="icTglPeriodeFrom" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>--%>
                                <asp:TextBox ID="txtTglPeriodeFrom" runat="server" CssClass="textboxCalendar"></asp:TextBox>
                            </td>
                            <td>&nbsp;s/d&nbsp;</td>
                            <td>
                                <%--<cc1:IntiCalendar ID="icTglPeriodeTo" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>--%>
                                <asp:TextBox ID="txtTglPeriodeTo" runat="server" CssClass="textboxCalendar"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="titleField" valign="top">Waktu Operasional</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <table cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtOptFrom" runat="server" TextMode="Time" CssClass="timepicker"></asp:TextBox>
                            </td>
                            <td>&nbsp;s/d&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtOptTo" runat="server" TextMode="Time" CssClass="timepicker"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="titleField" valign="top">Waktu Istirahat</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <table cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtBreakFrom" runat="server" TextMode="Time" CssClass="timepicker"></asp:TextBox>
                            </td>
                            <td>&nbsp;s/d&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtBreakTo" runat="server" TextMode="Time" CssClass="timepicker"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="titleField" valign="top">Stall</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:ListBox ID="stallList" runat="server" SelectionMode="Multiple" Width="270"></asp:ListBox>
                </td>
            </tr>
            <tr>
                <td class="titleField" valign="top">Sebagai Hari Libur ?</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <asp:RadioButton ID="rbYes" runat="server" GroupName="generate" Text="Ya" CssClass="setMargin"></asp:RadioButton>
                    <asp:RadioButton ID="rbNo" runat="server" GroupName="generate" Text="Tidak" Checked="true" CssClass="setMargin"></asp:RadioButton>
                </td>
            </tr>
            <tr>
                <td class="titleField" valign="top">Hari</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <%--<asp:Calendar ID="calHoliday" runat="server" BackColor="White" BorderColor="Black"
                        BorderWidth="1px" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black"
                        Height="170px" NextPrevFormat="FullMonth"
                        Width="250px" OnSelectionChanged="calHoliday_SelectionChanged" 
                        OnDayRender="calHoliday_DayRender" 
                        OnVisibleMonthChanged="calHoliday_VisibleMonthChanged"
                        OnPreRender="calHoliday_PreRender">
                        <DayHeaderStyle Font-Bold="True" Font-Size="6pt" />
                        <NextPrevStyle Font-Bold="True" Font-Size="6pt" ForeColor="#333333"
                            VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TitleStyle BackColor="White" BorderStyle="None"
                            Font-Bold="True" Font-Size="10pt" ForeColor="#333399" />
                    </asp:Calendar>
                    <asp:ListBox ID="tglList" runat="server" Visible="false"></asp:ListBox>--%>
                    <asp:CheckBoxList ID="chkHolidayList" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal" CssClass="setMargin">
                        <asp:ListItem Text="Sen" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Sel" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Rab" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Kam" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Jum" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Sab" Value="6"></asp:ListItem>
                        <asp:ListItem Text="Min" Value="7"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td colspan="2"></td>
                <td align="left" valign="top">
                    <input id="hdConfirm" type="hidden" value="-1" runat="server">
                    <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click" />
                    <asp:Button ID="btnTampilkan" runat="server" Text="Tampilkan" OnClick="btnTampilkan_Click" />
                </td>
            </tr>
        </table>
        <br />
        <div>
            <DayPilot:DayPilotScheduler ID="DayPilotScheduler1" runat="server"
                BusinessBeginsHour="7"
                BusinessEndsHour="17" 
                HeaderFontSize="8pt" 
                HeaderHeight="20" 
                DataStartField="start" 
                DataEndField="end" 
                DataTextField="name" 
                DataValueField="id" 
                DataResourceField="resource" 
                EventFontSize="11px" 
                CellDuration="60" 
                CellWidth="40"
                Days="1"
                EventClickHandling="JavaScript"
                EventClickJavaScript="eventClick('{0}');" 
                CssOnly="true"
                CssClassPrefix="scheduler_traditional"
                OnPreRender="DayPilotScheduler1_PreRender"
                EventHeight="47">
            </DayPilot:DayPilotScheduler>
        </div>
        <div>
            <table cellpadding="4" cellspacing="4" border="0">
                <tr>
                    <td colspan="2"><p>Legend Tipe Stall :</p></td>
                </tr>
                <tr>
                    <td>
                        <svg width="50" height="20">
                            <rect width="50" height="20" style="fill:darkcyan;" />
                        </svg>
                    </td>
                    <td><label>MQP</label></td>
                    <td>
                        <svg width="50" height="20">
                            <rect width="50" height="20" style="fill:darkolivegreen;" />
                        </svg>
                    </td>
                    <td><label>Booking</label></td>
                    <td>
                        <svg width="50" height="20">
                            <rect width="50" height="20" style="fill:blueviolet;" />
                        </svg>
                    </td>
                    <td><label>Walk In</label></td>
                    <td>
                        <svg width="50" height="20">
                            <rect width="50" height="20" style="fill:sandybrown;" />
                        </svg>
                    </td>
                    <td><label>Washing</label></td>
                    <td>
                        <svg width="50" height="20">
                            <rect width="50" height="20" style="fill:blue;" />
                        </svg>
                    </td>
                    <td><label>Real Time Service</label></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
