<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopupMPPeriod.aspx.vb" Inherits="PopupMPPeriod"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopupMPPeriod</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<base target="_self">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">

		function trim(str)
		{
			if(!str || typeof str != 'string')
				return null;

			return str.replace(/^[\s]+/,'').replace(/[\s]+$/,'').replace(/[\s]{2,}/,' ');
		}		
		function GetSelectedMP()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgMPMaster");
			var MPMaster ='';
			for (i = 1; i < table.rows.length; i++){
				var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (radioBtn != null && radioBtn.checked)			
					{
						if(navigator.appName == "Microsoft Internet Explorer")
						{	
							goodno = trim(table.rows[i].cells[1].innerText)+";"+trim(table.rows[i].cells[2].innerText);
							goodno = table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML+";"+table.rows[i].cells[1].getElementsByTagName("span")[1].innerHTML;							
							window.returnValue = goodno;
							bcheck=true;
							break;
						}
						else
						{							
							if (MPMaster == '')
							{
								MPMaster = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','') + ';' + replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML,' ','');
							}
							window.close();
							opener.dialogWin.returnFunc(MPMaster);
							bcheck=true;	
							break;
							}
					}
			}
			
			if (bcheck)
			  {
				window.close();
			  }
			else
			  {
				alert("Silahkan pilih Periode Material Promotion");	
			  }			
		}

		function ClosePopUp(){
			window.close();
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<table border="0" width="100%" cellpadding="0" cellspacing="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 21px">
						Material Promotion - Period
					</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
					</TD>
				</TR>
				<tr>
					<td><div id="div1" style="OVERFLOW: auto; HEIGHT: 320px">
							<asp:datagrid id="dtgMPMaster" runat="server" Width="100%" CellPadding="3" BackColor="#CDCDCD"
								BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True"
								AllowCustomPaging="True" AllowPaging="True" PageSize="25">
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#FFCC66"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Pilih">
										<HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderTemplate>
											&nbsp;Pilih
										</HeaderTemplate>
										<ItemTemplate>
											<input type="radio" id="x" name="y" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PeriodName" HeaderText="Nama">
										<HeaderStyle Width="40%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblID" style="visibility:hidden;" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
											<asp:Label id="lblPeriodName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PeriodName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="StartDate" HeaderText="Periode Mulai">
										<HeaderStyle Width="20%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStartDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StartDate") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="EndDate" HeaderText="Periode Akhir">
										<HeaderStyle Width="20%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblEndDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EndDate") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</td>
				</tr>
				<tr>
					<td align="center"><br>
						<INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedMP()" type="button"
							value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel">
					</td>
				</tr>
			</table>
		</FORM>
	</body>
</HTML>
