<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpMultipleService.aspx.vb" Inherits=".PopUpMultipleService" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PopUpService</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
	<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
	<meta content="JavaScript" name="vs_defaultClientScript"/>
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/FormFunctions.js"></script>
   

	<base target="_self" />
	<script type="text/javascript" language="javascript">
	    function onSuccess() {
	        if (navigator.appName == "Microsoft Internet Explorer")
	            window.returnValue = "Simpan berhasil.";
	        else {
	            window.close();
	            window.opener.dialogWin.returnFunc("Simpan berhasil.");
	        }

	        window.close();
	    }

	    function GetSelectedChassis() {
	        var ddljnsKegiatan;
	        var valJnsKegiatan;
	        var ddljnsService;
	        var valJnsService;
	       
	        ddljnsKegiatan = document.getElementById("ddlJnsKegiatan");
	        var valJnsKegiatan = ddljnsKegiatan.options[ddljnsKegiatan.selectedIndex].text;
	       
	        ddljnsService = document.getElementById("ddlJnsService");
	        var valJnsService = ddljnsService.options[ddljnsService.selectedIndex].text;
	        //alert(valJnsKegiatan + ';' + valJnsService)
	        if (navigator.appName == "Microsoft Internet Explorer")

	            window.returnValue = valJnsKegiatan + ';' + valJnsService ;
	        else {
	            window.close();
	            window.opener.dialogWin.returnFunc(valJnsKegiatan + ';' + valJnsService);
	        }

            window.close();
	        //var ChassisMaster = '';
	        //for (i = 1; i < table.rows.length; i++) {
	        //    var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
	        //    if (radioBtn != null && radioBtn.checked) {
	        //        if (navigator.appName == "Microsoft Internet Explorer") {
	        //            SelectedData = table.rows[i].cells[1].innerText;
	        //            window.returnValue = SelectedData;
	        //            bcheck = true;
	        //            break;
	        //        }
	        //        else if (navigator.appName == "Netscape") {
	        //            SelectedData = table.rows[i].cells[1].innerText;
	        //            opener.dialogWin.returnFunc(SelectedData);
	        //            bcheck = true;
	        //            break;
	        //        }
	        //        else {
	        //            if (ChassisMaster == '') {
	        //                ChassisMaster = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
	        //            }
	        //            window.close();
	        //            opener.dialogWin.returnFunc(ChassisMaster);
	        //            bcheck = true;
	        //            break;
	        //        }
	        //    }
	        //}

	        //if (bcheck) {
	        //    window.close();
	        //}
	        //else {
	        //    alert("Silahkan pilih No Rangka");
	        //}
	    }
	    
	</script>
</head>
<body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table2" cellspacing="3" cellpadding="3" width="100%" border="0">
			<tr>
                <td class="titleTableParts3" colspan="2">Popup Service</td>
            </tr>

    </table>
    <table id="Table1" cellspacing="0" cellpadding="0" border="0">
			<tr>
                <td valign="top" align="left" width="50%">
                    <table id="Table_left" cellspacing="1" cellpadding="2" border="0">
                        <tr>
                            <td class="auto-style1" colspan="2">Jenis Kegiatan</td>
                            <td colspan="2">:
                                <asp:DropDownList ID="ddlJnsKegiatan" runat="server"  OnSelectedIndexChanged="ddlJnsKegiatan_SelectedIndexChanged" AutoPostBack="true" Width="180"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1" colspan="2">Jenis Service</td>
                            <td colspan="2">:
                                <asp:DropDownList ID="ddlJnsService" runat="server" Width="250"></asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td class="titleField" width="24%"></td>
                            <td width="1%"></td>
                            <td width="75%">
                                <INPUT id="btnChoose" style="WIDTH: 60px" onclick="GetSelectedChassis()" type="button"
							value="Simpan" name="btnChoose" runat="server">
                            </td>
                        </tr>

                        </table>
                    </td>
                </tr>
        </table>
    </form>
</body>
</html>
