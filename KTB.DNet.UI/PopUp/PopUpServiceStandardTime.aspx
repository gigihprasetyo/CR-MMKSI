<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpServiceStandardTime.aspx.vb" Inherits=".PopUpServiceStandardTime" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<HEAD runat="server">
		<title>FrmCalculateServiceStdTime</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
     <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/FormFunctions.js"></script>
		<LINK href="../WebResources/BasicStyle.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript">


		function GetVariable2()
		{
			var table;
			var bcheck =false;
			var PeriodFrom = '';
			var JenisKegiatan = '';
			PeriodFrom = ICKedatangan.value;
			if (PeriodFrom.value != '')
			{
                bcheck = true
			}
			if (bcheck)
			  {
				window.close();
			  }
			else
			  {
				alert("Silahkan pilih dealer");	
			  }
		}

		function GetVariable()
		{
		    var bcheck = false;
		    var value = document.getElementById("ddlJenisKegiatan");
		    var getvalue = value.options[value.selectedvalue].value;
		    var gettext = value.options[value.selectedIndex].text;
		    if (navigator.appName == "Microsoft Internet Explorer")
		    {
		        if (getvalue != 0)
		        {
		            window.returnValue = getvalue;
		            bcheck = true;
		        }
		        else
		        {
		            alert("Silahkan pilih dealer");
		        }
		    }
		    else
		    {
		        if (getvalue != 0)
		        {
		            window.opener.dialogWin.returnFunc(getvalue);
		            bcheck = true;
		        }
		        else
		        {
		            alert("Silahkan pilih dealer");
		        }
		    }

		    if (bcheck) {
		        window.close();
		    }
		    else {
		        alert("Silahkan pilih dealer");
		    }

		}
		
		function dconfirm() {
		    var e = document.getElementById("ddlJenisKegiatan");
		    var w = document.getElementById("ICKedatangan");
		    var selVal = e.options[e.selectedIndex].value;
		    //var selectedTOP = w.options[w].value;

		    window.close();
		    window.opener.dialogWin.returnFunc(selVal);
		    //alert("test");
		}



		</script>
	</HEAD>
<body >
    <form id="form1" method="post" runat="server">
    <TABLE id="Table1" cellSpacing="0" cellPadding="4" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" colSpan="7">Service Standard Time -&nbsp;Filter Calculate &nbsp;</td>
							</tr>
							<tr>
								<td background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
							<TR valign="top">
								<TD class="titleField" width="20%">Period From</TD>
								<TD width="1%">:</TD>
								<TD width="25%"><cc1:inticalendar id="ICKedatangan" runat="server" TextBoxWidth="70" Visible="true" ></cc1:inticalendar></TD>
								<td></td>
							</TR>
                            <tr valign="top">
                               
								<TD class="titleField" width="20%">Jenis Kegiatan</TD>
								<TD width="1%">:</TD>
								<TD  width="25%">          <asp:DropDownList ID="ddlJenisKegiatan" runat="server" ></asp:DropDownList></TD>
                                <td></td>
                            </tr>
                            </TABLE>
                        </td>
                    </tr>
        <TR>
								<TD align="center" colspan="7">
								<INPUT id="btnChoose" style="WIDTH: 60px" onclick="dconfirm()" type="button"
										value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
										name="btnCancel">
                                    <%--<asp:Button ID="btnPilih" runat="server" Text="Pilih" Width="80px" CausesValidation="False" OnClick="btnPilih_Click"></asp:Button>
                           
                                    <asp:Button ID="btnTutup" runat="server" Text="Tutup" Width="80px" CausesValidation="False" OnClientClick="javaScript:window.close(); return false;" ></asp:Button>--%>

								</TD>
							</TR>
        </TABLE>
    </form>
</body>
</html>
