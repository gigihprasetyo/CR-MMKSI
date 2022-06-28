<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpRecipientEmailDPSelection.aspx.vb" Inherits="PopUpRecipientEmailDPSelection"  smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
	
		<title>PopUp Pencarian Penerima Email</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		
		function getSelectedEmail()
		{
			var table;
			var bcheck = false;
			var _value = '';
			table = document.getElementById("dtgEmailUser");
			for (i = 1; i < table.rows.length; i++)
			{
				var RadioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (RadioButton != null && RadioButton.checked)
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{
					    _value = table.rows[i].cells[1].innerText + ';' + table.rows[i].cells[2].innerText + ';' + table.rows[i].cells[3].innerText + ';' + table.rows[i].cells[4].innerText;
					    window.returnValue = _value;
						bcheck=true;
						break;
					}
					else if (navigator.appName == "Netscape") {
					    _value = table.rows[i].cells[1].innerText + ';' + table.rows[i].cells[2].innerText + ';' + table.rows[i].cells[3].innerText + ';' + table.rows[i].cells[4].innerText;
					    bcheck = true;
					    break;
					}
					else
					  {
					    _value = table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML + ';' + table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML + ';' + table.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML + ';' + table.rows[i].cells[4].getElementsByTagName("span")[0].innerHTML;
					  	bcheck=true;
						break;
					  }	
				}
			}
			if (bcheck)
			  {
			    window.close();
			    if (navigator.appName != "Microsoft Internet Explorer")
			    {
			        window.opener.dialogWin.returnFunc(_value);
			    }
            }
			else
			  {
				alert("Silahkan pilih Nama Penerima Email");	
			  }	
		}
		</script>

        <style type="text/css">
          .hiddencol
          {
            display: none;
          }
        </style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="6" width="480px" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="700px" border="0">
							<tr>
								<td class="titlePage" style="HEIGHT: 18px" colSpan="7">
									Pencarian Penerima Email</td>
							</tr>
							<tr>
								<td style="HEIGHT: 1px" background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
							<TR>
								<TD class="titleField" style="HEIGHT: 22px" width="15%">Nama</TD>
								<TD style="HEIGHT: 22px" width="1%">:</TD>
								<TD style="HEIGHT: 22px" width="25%"><asp:textbox id="txtRecipientName" runat="server" Width="152px"></asp:textbox></TD>
								<TD style="HEIGHT: 22px" width="1%"></TD>
								<TD class="titleField" style="HEIGHT: 22px" width="15%">Email</TD>
								<TD style="HEIGHT: 22px" width="1%">:</TD>
								<TD style="HEIGHT: 22px" width="25%"><asp:textbox id="txtEmail" runat="server" Width="152px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 22px" width="15%">Jabatan</TD>
								<TD style="HEIGHT: 22px" width="1%">:</TD>
								<TD style="HEIGHT: 22px" width="25%"><asp:textbox id="txtRecipientPosition" runat="server" Width="152px"></asp:textbox></TD>
								<TD style="HEIGHT: 22px" width="1%"></TD>
								<TD class="titleField" style="HEIGHT: 22px" width="15%"></TD>
								<TD style="HEIGHT: 22px" width="1%"></TD>
								<TD style="HEIGHT: 22px" width="25%"><asp:button id="btnSearch" runat="server" Text=" Cari " Width="80px"></asp:button></TD>
							</TR>
							<TR align="center">
								<TD colspan="8">
									<hr /></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>

			<div id="div1" style="OVERFLOW: auto; HEIGHT: auto; width:690px">
                <asp:datagrid id="dtgEmailUser" runat="server" Width="100%" BackColor="#E0E0E0" AutoGenerateColumns="False" PageSize="10"
					BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" CellPadding="3" ShowFooter="False" AllowPaging="True" AllowSorting="True" AllowCustomPaging="True" >
					<FooterStyle ForeColor="#003399" VerticalAlign="Top" BackColor="White"></FooterStyle>
					<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
					<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
					<ItemStyle BackColor="White"></ItemStyle>
					<HeaderStyle VerticalAlign="Top"></HeaderStyle>
						<Columns>
							<asp:TemplateColumn>
								<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" VerticalAlign="Top"/>
                                <ItemTemplate>
                                    <asp:Label ID="lblNoGridRow" runat="server" Text="1"></asp:Label>
                                </ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ID">
                                <HeaderStyle Width="1%" CssClass="hiddencol"></HeaderStyle>
                                <ItemStyle Width="1%" CssClass="hiddencol"/>
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Nama">
                                <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" VerticalAlign="Top"/>
                                <ItemTemplate>
                                    <asp:Label ID="lblRecipientName" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Jabatan">
								<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblRecipientPosition" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Alamat Email">
                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" VerticalAlign="Top"/>
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
						</Columns>
						<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
					</asp:datagrid>
			</div>
            <table width="100%">
				<TR>
					<TD align=center>
                        <INPUT id="btnChoose" style="WIDTH: 60px" onclick="getSelectedEmail()" type="button" disabled="disabled"
							value="Pilih" name="btnChoose" runat="server">&nbsp;
                        <INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel"></TD>
				</TR>
            </table>
		</form>
	</body>
</HTML>
