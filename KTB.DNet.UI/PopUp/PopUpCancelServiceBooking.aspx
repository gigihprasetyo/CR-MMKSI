<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpCancelServiceBooking.aspx.vb" Inherits=".PopUpCancelServiceBooking" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PopUpCancelServiceBooking</title>
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
	            window.returnValue = "Service Booking Dibatalkan.";
	        else {
	            window.close();
	            window.opener.dialogWin.returnFunc("Service Booking Dibatalkan.");
	        }

	        window.close();
	    }
	    var rbtnResponses = document.getElementById('rbtnResponses')
	    rbtnResponses.addEventListener('change', function () {
	        alert("berubah");
	    });
	</script>
</head>
<body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table2" cellspacing="3" cellpadding="3" width="100%" border="0">
            <tr>
                <td class="titleTableParts3" colspan="3">Popup Cancel Service Booking</td>
            </tr>
            <tr id="trResponse" runat="server" valign="top">
                                <td class="titleField" height="20" width="18%">Alasan Cancel Booking</td>
                                <td class="titleField" height="20" width="1%">:</td>
                                <td valign="top">
                                    <table border="0">
                                        <tr>
                                            <td style="border:1px solid" width="20%" valign="top">
                                                                                             
                                                <asp:RadioButtonList ID="rbtnResponses" runat="server" AutoPostBack="True">
                                                    <%--<asp:ListItem Value="1" Selected="True">respon 1</asp:ListItem>
                                                    <asp:ListItem Value="2">respon 1</asp:ListItem>
                                                    <asp:ListItem Value="3">respon 1</asp:ListItem>
                                                    <asp:ListItem Value="4">respon 1</asp:ListItem>
                                                    <asp:ListItem Value="5">respon 1</asp:ListItem>
                                                    <asp:ListItem Value="6">respon 1</asp:ListItem>
                                                    <asp:ListItem Value="7">Respon lain</asp:ListItem>--%>
                                                </asp:RadioButtonList>
                                                <%--<asp:Repeater ID="rptDealerResp" runat="server" OnItemDataBound="rptDealerResp_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <table cellpadding="3" cellspacing="1" border="0">
                                                        <th align="left"></th>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButton ID ="rbDealer" runat="server" CssClass="Space"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem, "ValueDesc")%>' 
                                                                    GroupName="response" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>--%>
                                            </td>
                                            
                                        </tr>
                                    </table>
                                </td>
            </tr>    
            
            <tr valign="top">
                <td class="titleField" width="24%">Keterangan</td>
                <td width="1%">:</td>
                <td width="75%">
                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="5" MaxLength="100" Width="450px" onkeypress="return alphaNumericExcept(event,'<>?*%$;%^&@#!')" onblur="omitSomeCharacter('txtComment','<>?*%$;%^&@#!')"></asp:TextBox>
                                    &nbsp;&nbsp<asp:Label ID="lblValidtxtComment" runat="server" ForeColor="Red" EnableViewState="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="24%"></td>
                <td width="1%"></td>
                <td width="75%">
                    <asp:Button ID="btnSave" Text="Simpan" runat="server" />
                </td>
            </tr>
        
    </table>
    </form>
</body>
</html>
