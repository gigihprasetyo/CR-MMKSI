<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpClaim.aspx.vb" Inherits="PopUpClaim" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Merek Kompetitor</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		
		function GetSelectedValue()
		{
			var table;
			var bcheck =false;
			table = document.getElementById('dgCompetitorType');
			var val ='';
			for (i = 1; i < table.rows.length; i++)
			{
			
			var radioBtn = table.rows[i].cells[0].getElementsByTagName('INPUT')[0];
			if (radioBtn != null && radioBtn.checked)			
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{	
						val = table.rows[i].cells[1].innerText;						
						window.returnValue = val;
						bcheck=true;
					}
					else
					{
						val = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
						opener.dialogWin.returnFunc(val);
						bcheck=true;
					}
					break;
				}
			}
			
			if (bcheck)
			  {
				window.close();
			  }
			else
			  {
				alert("Silahkan pilih merek");	
			  }
		}

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="FrmSalesmanLevel" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">
						Sales- List Faktur</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
                <tr>
					<td>
                        <asp:DropDownList ID="DropDownList1" runat="server">                                        
                                        <asp:ListItem Value="0" Text="Aktif">Semua</asp:ListItem>
                                        <asp:ListItem Value="1" Text="Nomor Chassis"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Tgl Validasi Faktur"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Nama Customer"></asp:ListItem>
                                    </asp:DropDownList>   
                        <asp:DropDownList ID="DropDownList2" runat="server">                                        
                                        <asp:ListItem Value="0" Text="Aktif">Sama Dengan</asp:ListItem>
                                        <asp:ListItem Value="1" Text="Tidak Sama Dengan"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Diawali Dengan"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Diakhiri Dengan"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Aktif">Lebih Besar Dari</asp:ListItem>
                            <asp:ListItem Value="5" Text="Lebih Kecil Dari"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Lebih Besar / Sama Dengan"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Lebih Kecil / Sama Dengan"></asp:ListItem>
                        </asp:DropDownList> 
                        <asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtName" onblur="omitSomeCharacter('txtKeyWords','<>?*%$;')"
							runat="server" Width="242px"></asp:textbox> 
                        <INPUT id="Button1" style="WIDTH: 60px" onclick="GetSelectedValue()" type="button"
							value="Cari" name="btnChoose" runat="server">
					</td>
				</tr>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 390px">
                            <asp:datagrid id="dgCompetitorType" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
								AutoGenerateColumns="False" BorderColor="Gainsboro" BorderWidth="0px" AllowPaging="True" AllowCustomPaging="True" BackColor="#CDCDCD" CellPadding="3">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											&nbsp;
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Code" HeaderText="Nomor Chassis">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCode" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Code") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Description" HeaderText="Tgl Validasi Faktur">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblDescription Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Description" HeaderText="Nama Customer">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblDescription Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD height="40" align="center">
                        <INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedValue()" type="button"
							value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel"></TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
