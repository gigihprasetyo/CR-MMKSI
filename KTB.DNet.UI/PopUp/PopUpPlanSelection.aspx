<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpPlanSelection.aspx.vb" Inherits=".PopUpPlanSelection" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PopUpPlanSelection</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
	<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
	<meta content="JavaScript" name="vs_defaultClientScript"/>
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
    <link href="../WebResources/scheduler_traditional.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/css/jquery-ui.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/jquery-clockpicker.min.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/jquery.1.11.0.min.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/jquery-ui.min.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/jquery-clockpicker.min.js"></script>

	<base target="_self" />
	<script type="text/javascript" language="javascript">
	    function onSuccess(result)
	    {
	        if (navigator.appName == "Microsoft Internet Explorer")
	            window.returnValue = result;
	        else {
	            window.close();
	            window.opener.dialogWin.returnFunc(result);
	        }

	        window.close();
	    }

	    function PopUpOpen() {
	        $("#PopupDetail").dialog({
	            modal: true,
	            closeOnEscape: false,
	            closeText: "",
	            width: 575,
	            open: function (type, data) {
	                $(this).parent().appendTo("form");
	            }
	        });
	    }

	    function PopUpClose() {
	        $('#PopupDetail').dialog('close');
	    }

	    function Calculate() {
	        var hours = 0;

	        if ($('#hdCalculate').val() !== '0') {
	            var obj = JSON.parse($('#hdCalculate').val());
	            var ddl = $('#ddlJnsService').val();

	            for (var i = 0; i < obj.length; i++) {
	                if (obj[i].JenisService == ddl) {
	                    //if (hours = 0)
	                    //{
	                    //    hours = obj[i].Result;
	                    //}
	                    //else
	                    //{
	                        hours = hours + obj[i].Result;
	                    //}
	                    $('#hdStandardTime').val(hours.toFixed(2).toString().replace(".", ","));
	                }
	            }
	        }
	        //alert(hours)

	        var time;

	        if (navigator.appName === "Microsoft Internet Explorer" || navigator.appName === "Netscape"){
                time = new Date($('#hdTgl').val() + 'T' + $('#txtBookFrom').val());
            }
            else {
                time = new Date($('#hdTgl').val() + ' ' + $('#txtBookFrom').val());
            }
                
	        time.setMinutes(time.getMinutes() + (hours * 60));

	        var hh = time.getHours();
	        var mm = time.getMinutes();

	        hh = hh < 10 ? '0' + hh : hh;
	        mm = mm < 10 ? '0' + mm : mm;
	        var result = hh + ':' + mm;
	        if (hours === 0) {
	            document.getElementById("lblEstimasi").innerText = "N/A";
	        }
	        else {
	            document.getElementById("lblEstimasi").innerText = hours.toFixed(2).toString().replace(".", ",") + " jam";
	        }

	        return result;
	    }

	    $(function () {
	        $('.timepicker').clockpicker({
	            placement: 'bottom',
	            align: 'left',
	            autoclose: true
	        });

	        $('#txtBookFrom').change(function () {
	            $('#txtBookTo').val(Calculate());
	        });

	        $('#ddlJnsService').change(function () {
	            $('#txtBookTo').val(Calculate());
	        });
	    });
	    
	</script>
</head>
<body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
		<table id="Table2" cellspacing="3" cellpadding="3" width="100%" border="0">
			<tr>
                <td class="titleTableParts3" colspan="2">Popup Setting Rencana Pengerjaan</td>
            </tr>
            <tr>
                <td style="width:380px">
                    <asp:Calendar ID="CalWork" runat="server"
                        Font-Names="Verdana" Font-Size="7pt" ForeColor="WhiteSmoke"
                        Height="240px" NextPrevFormat="FullMonth"
                        Width="380px" OnSelectionChanged="CalWork_SelectionChanged" OnVisibleMonthChanged="CalWork_VisibleMonthChanged"
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
                <td valign="top" align="left">
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
                    <div style="position:relative; top:115px">
                        <asp:Label ID="lblInfoBooking" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
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
                            ShowNonBusiness="false"
                            TimeRangeSelectedHandling="PostBack"
                            EventClickHandling="PostBack"
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
                                        <rect width="50" height="20" style="fill:blue;" />
                                    </svg>
                                </td>
                                <td><label>Real Time Service</label></td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div id="PopupDetail" title="Popup Service Booking" style="display:none;">
            <table width="100%" cellpadding="3" cellspacing="3" border="0">
				<tr valign="top">
					<td class="titleField" style="width: 200px">Tanggal</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="lblTgl" runat="server"></asp:Label>
                        <asp:HiddenField ID="hdTgl" runat="server" />
                    </td>
				</tr>
                <tr valign="top">
					<td class="titleField">Kode Stall</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="lblKodeStall" runat="server"></asp:Label>
                    </td>
				</tr>
                <tr valign="top">
					<td class="titleField">Jam Operasional Stall</td>
                    <td>:</td>
                    <td><asp:Label ID="lblJamOp" runat="server"></asp:Label></td>
				</tr>
                <tr valign="top">
					<td class="titleField">Jam Istirahat Stall</td>
                    <td>:</td>
                    <td><asp:Label ID="lblJamBreak" runat="server"></asp:Label></td>
				</tr>
                <tr valign="top">
					<td class="titleField">Jenis Service</td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList ID="ddlJnsService" runat="server">
                            <asp:ListItem Value="MQP" Text="MQP"></asp:ListItem>
                            <asp:ListItem Value="SB" Text="Standard/None/SB"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
				</tr>
                <tr valign="top">
					<td class="titleField">Estimasi Pengerjaan</td>
                    <td>:</td>
                    <td><asp:Label ID="lblEstimasi" runat="server"></asp:Label></td>
				</tr>
                <tr valign="top">
					<td class="titleField">Rencana Jam Pengerjaan</td>
                    <td>:</td>
                    <td>
                        <table cellspacing="1" cellpadding="1">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtBookFrom" runat="server" TextMode="Time" CssClass="timepicker"></asp:TextBox>
                                    <asp:HiddenField ID="hdCalculate" runat="server" />
                                    <asp:HiddenField ID="hdStandardTime" runat="server" />
                                </td>
                                <td>&nbsp;s/d&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtBookTo" runat="server" TextMode="Time" CssClass="timepicker"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
				</tr>
                <tr>
                    <td colspan="2"></td>
					<td align="left">
                        <asp:Button ID="btnSave" runat="server" Text="Simpan" OnClick="btnSave_Click" />
                        <input id="btnCancel" style="width: 60px" onclick="PopUpClose();" type="button" value="Tutup" name="btnCancel" />
					</td>
				</tr>
            </table>
        </div>
    </form>
</body>
</html>
