<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpSparePartPO.aspx.vb" Inherits="PopUpSparePartPO" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>PopUpSparePartPO</title>
        <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
        <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
        <meta name="vs_defaultClientScript" content="JavaScript">
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
        <base target="_self">
        <script language="javascript">
		
		function ShowPPDealerSelection()
		{
			showPopUp('../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);
		}
		
		function DealerSelection(selectedDealer)
		{
			var tempParam = selectedDealer.split(';');
			var txtDealer = document.getElementById("txtDealerCode");
			txtDealer.value = tempParam[0];
		}
		
		function GetSelectedPONOMany()
		{	
			var table;
			var bcheck =false;
			table = document.getElementById("dtgSparePartPO");
			
			var Indent ='';
			for (i = 1; i < table.rows.length; i++)
			{
				var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (CheckBox != null && CheckBox.checked)
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{
						if (Indent == '')
						{
							Indent = replace(table.rows[i].cells[2].innerText,' ','');
						}
						else
						{
							Indent = Indent + ';' + replace(table.rows[i].cells[2].innerText,' ','');
						}
					    window.returnValue = Indent;
					    bcheck=true;
					}
					else if (navigator.appName == "Netscape") {
					    if (Indent == '') {
					        Indent = replace(table.rows[i].cells[2].innerText, ' ', '');
					    }
					    else {
					        Indent = Indent + ';' + replace(table.rows[i].cells[2].innerText, ' ', '');
					    }
					    bcheck = true;
					}
					else
					{
						if (Indent == '')
						{
							Indent = replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML,' ','');
						}
						else
						{
							Indent = Indent + ';' + replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML,' ','');
						}
					    bcheck=true;
					}
				}
			}
			
			if(navigator.appName != "Microsoft Internet Explorer")
			{
				opener.dialogWin.returnFunc(Indent);
			}

			if (bcheck)
	        {
			    window.close();
			}
			else
     	    {
				alert("Silahkan Pilih Data terlebih dahulu");	
			}
		}
		
		function CheckAll(aspCheckBoxID, checkVal) 
		{
			re = new RegExp(':' + aspCheckBoxID + '$')  
			for(i = 0; i < document.forms[0].elements.length; i++) 
			{
				elm = document.forms[0].elements[i]
				if (elm.type == 'checkbox') 
				{
					if (re.test(elm.name)) 
					{
						elm.checked = checkVal
					}
				}
			}
		}
		
        </script>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <table width="100%">
                <tr>
                    <td class="titleField" vAlign="top">Kode Dealer
                    </td>
                    <td>:
                    </td>
                    <td><asp:textbox id="txtDealerCode" runat="server" Width="152px" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                            onblur="omitSomeCharacter('txtDealerCode','<>?*%$;')"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
                            <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></td>
                </tr>
                <TR>
                    <TD class="titleField" vAlign="top">Tanggal Pengajuan</TD>
                    <TD vAlign="top">:</TD>
                    <td>
                        <table id="Table2" cellSpacing="0" cellPadding="0" border="0">
                            <tr>
                                <td><cc1:inticalendar id="icPODateFrom" runat="server"></cc1:inticalendar></td>
                                <td>&nbsp;s/d&nbsp;</td>
                                <td><cc1:inticalendar id="icPODateUntil" runat="server"></cc1:inticalendar></td>
                            </tr>
                        </table>
                    </td>
                </TR>
                <tr>
                    <td></td>
                    <td></td>
                    <td><asp:button id="btnSearch" runat="server" width="60px" Text="Cari"></asp:button></td>
                </tr>
                <tr>
                    <td colSpan="3">
                        <div id="div1" style="OVERFLOW: auto; HEIGHT: 310px">
                            <asp:datagrid id="dtgSparePartPO" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
                                AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" AllowPaging="True"
                                AllowCustomPaging="True" BackColor="#CDCDCD" CellPadding="3" GridLines="None" PageSize="15">
                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                                <ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
                                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
                                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                <Columns>
                                    <asp:TemplateColumn>
                                        <HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderTemplate>
                                            <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked', document.forms[0].chkAllItems.checked)" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="No.">
                                        <HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:label id="lblNo" Runat="server"></asp:label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="PONumber" HeaderText="Nomor Pengajuan">
                                        <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label id=lblNoPO runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PONumber") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="CreatedTime" SortExpression="CreatedTime" HeaderText="Tanggal Pengajuan"
                                        DataFormatString="{0:dd/MM/yyyy}">
                                        <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                        <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label id=lblDealerCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
                                        <HeaderStyle Width="45%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label id=lblDealerName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                            </asp:datagrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colSpan="3" align="center">
                        <INPUT id="btnChoose" disabled onclick="GetSelectedPONOMany()" type="button" value="Pilih"
                            name="btnChoose" runat="server"> <INPUT id="btnCancel" onclick="window.close();" type="button" value="Tutup" name="btnCancel">
                    </td>
                </tr>
            </table>
        </form>
    </body>
</HTML>
