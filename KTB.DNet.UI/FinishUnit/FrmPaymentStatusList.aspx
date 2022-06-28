<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPaymenyStatusList.aspx.vb" Inherits="FrmPaymenyStatusList" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPaymenyStatusList</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 16px; WIDTH: 762px; POSITION: absolute; TOP: 16px; HEIGHT: 72px"
				cellSpacing="1" cellPadding="1" width="762" border="1">
				<TR>
					<TD colSpan="10">Payment Status List</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 99px">Dealer Code</TD>
					<TD style="WIDTH: 2px">:</TD>
					<TD>
						<asp:DropDownList id="ddlDealerCode" runat="server"></asp:DropDownList></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD>Nomor PO</TD>
					<TD>:</TD>
					<TD>
						<asp:TextBox id="txtNomerPO" runat="server"></asp:TextBox></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 99px">Receipt Date</TD>
					<TD style="WIDTH: 2px">:</TD>
					<TD>
						<asp:DropDownList id="ddlReceiptDate" runat="server"></asp:DropDownList></TD>
					<TD>to</TD>
					<TD>
						<asp:DropDownList id="ddlReceiptDateTo" runat="server"></asp:DropDownList></TD>
					<TD></TD>
					<TD>Gyro/Slip Transfer</TD>
					<TD>:</TD>
					<TD>
						<asp:TextBox id="txtGyroSlipTransfer" runat="server"></asp:TextBox></TD>
					<TD>
						<asp:Button id="btnRefresh" runat="server" Text="Refresh"></asp:Button></TD>
				</TR>
				<TR>
					<TD colSpan="10">
						<asp:DataGrid id="dtgPaymentStatusList" runat="server" AutoGenerateColumns="False" Width="752px">
							<Columns>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:CheckBox id="cbPaymentStatusList" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="No"></asp:TemplateColumn>
								<asp:BoundColumn HeaderText="No PO"></asp:BoundColumn>
								<asp:BoundColumn HeaderText="No SO"></asp:BoundColumn>
								<asp:BoundColumn HeaderText="No Receipt"></asp:BoundColumn>
								<asp:BoundColumn HeaderText="Purpose"></asp:BoundColumn>
								<asp:BoundColumn HeaderText="Gyro/Slip Transfer"></asp:BoundColumn>
								<asp:BoundColumn HeaderText="Amount (Rp)"></asp:BoundColumn>
								<asp:BoundColumn HeaderText="Receipt Data"></asp:BoundColumn>
								<asp:BoundColumn HeaderText="Baseline Date"></asp:BoundColumn>
								<asp:BoundColumn HeaderText="TOP (days)"></asp:BoundColumn>
								<asp:BoundColumn HeaderText="Created By"></asp:BoundColumn>
							</Columns>
						</asp:DataGrid></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 99px">
						<asp:Button id="btnDownload" runat="server" Text="Download"></asp:Button></TD>
					<TD style="WIDTH: 2px"></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
			</TABLE>
			&nbsp;
		</form>
		<script language="javascript">
			if (window.parent==window)
			{
				if (!navigator.appName=="Microsoft Internet Explorer")
				{
				  self.opener = null;
				  self.close();
				}
				else
				{
				   this.name = "origWin";
				   origWin= window.open(window.location, "origWin");
				   window.opener = top;
                   window.close();
				}
			}	
		</script>
	</body>
</HTML>
